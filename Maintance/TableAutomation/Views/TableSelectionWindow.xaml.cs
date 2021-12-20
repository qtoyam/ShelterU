using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Maintance.Converters;
using Maintance.TableAutomation.Models;

namespace Maintance.TableAutomation.Views
{
	/// <summary>
	/// Interaction logic for TableSelectionWindow.xaml
	/// </summary>
	public partial class TableSelectionWindow : Window
	{
		private readonly List<PropertyInfo> _filteringProperties = new();
		private readonly List<IReadOnlyDictionary<int, string>?> _enumStrProps = new();
		private readonly ICollectionView _entityViews;
		private readonly ITableManager _tableManager;
		private bool _isSelected = false;

		public TableSelectionWindow(ITableManager tableManager)
		{
			InitializeComponent();
			LabelTop.Content = tableManager.ParentName;
			var cols = DB_grid.Columns;
			_tableManager = tableManager;
			foreach (var vcp in _tableManager.TableColumnInfos)
			{
				if (vcp.SelectionColumnAttribute == null) continue;
				Binding b = new(vcp.PropertyInfo.Name)
				{
					Mode = BindingMode.OneWay
				};
				if (vcp.PropertyInfo.PropertyType.IsEnum)
				{
					b.Converter = new EnumToStrConverter(AutomationHelper.GetEnumDescriptions(vcp.PropertyInfo.PropertyType),
						vcp.PropertyInfo.PropertyType);
					_enumStrProps.Add(AutomationHelper.GetEnumDescriptions(vcp.PropertyInfo.PropertyType));
				}
				else
				{
					_enumStrProps.Add(null);
				}
				cols.Add(
					new MaterialDesignThemes.Wpf.DataGridTextColumn
					{
						Header = vcp.PropertyInfoAttribute.DisplayName,
						Binding = b,
						Width = new(1, DataGridLengthUnitType.Star),
						IsReadOnly = true
					});
				if (vcp.SelectionColumnAttribute.IsFilter)
				{
					_filteringProperties.Add(vcp.PropertyInfo);
				}
			}

			_entityViews = _tableManager.CreateCollectionView();
			DB_grid.ItemsSource = _entityViews;
		}

		private void SelectBtnClick(object sender, RoutedEventArgs e)
		{
			_isSelected = true;
			this.Hide();
		}

		private void CreateBtnClick(object sender, RoutedEventArgs e)
		{
			_tableManager.TryCreateEntity(this);
		}

		private void CancelBtnClick(object sender, RoutedEventArgs e)
		{
			_isSelected = false;
			this.Hide();
		}

		private void FilterTb_TextChanged(object sender, TextChangedEventArgs e)
		{
			var filterText = FilterTb.Text;
			if (!string.IsNullOrEmpty(filterText))
			{
				_entityViews.Filter = (entity) =>
				{
					foreach (var p_e in _filteringProperties.Zip(_enumStrProps))
					{
						if (p_e.Second != null)
						{
							if (p_e.Second.TryGetValue((int)p_e.First.GetValue(entity)!, out var str))
							{
								if (str.Contains(filterText, StringComparison.OrdinalIgnoreCase))
								{
									return true;
								}
							}
						}
						else
						{
							if (
								p_e.First
									.GetValue(entity)?
									.ToString()?
									.Contains(filterText, StringComparison.OrdinalIgnoreCase) == true)
							{
								return true;
							}
						}
					}
					return false;
				};
			}
			else
			{
				_entityViews.Filter = null;
			}
		}

		public bool TrySelectInDialog<T>(out T? res, Window owner)
			where T : class, new()
		{
			Owner = owner;
			_isSelected = false;
			this.ShowDialog();
			res = _isSelected ? (T)DB_grid.SelectedItem : null;
			return _isSelected;
		}
	}
}
