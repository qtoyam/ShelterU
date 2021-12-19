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


using Maintance.TableAutomation.Models;

namespace Maintance.TableAutomation.Views
{
	/// <summary>
	/// Interaction logic for TableSelectionWindow.xaml
	/// </summary>
	public partial class TableSelectionWindow : Window
	{
		private readonly List<PropertyInfo> _filteringProperties;
		private readonly ICollectionView _entityViews;
		private bool _isSelected = false;

		public TableSelectionWindow(ITableManager tableManager)
		{
			InitializeComponent();
			var cols = DB_grid.Columns;
			_filteringProperties = new();
			foreach (var vcp in tableManager.TableColumnInfos)
			{
				if (vcp.SelectionColumnAttribute == null) continue;
				cols.Add(
					new MaterialDesignThemes.Wpf.DataGridTextColumn
					{
						Header = vcp.PropertyInfoAttribute.DisplayName,
						Binding = new Binding(vcp.PropertyInfo.Name)
						{
							Mode = BindingMode.OneWay,
						},
						IsReadOnly = true
					});
				if (vcp.SelectionColumnAttribute.IsFilter)
				{
					_filteringProperties.Add(vcp.PropertyInfo);
				}
			}

			_entityViews = tableManager.CreateCollectionView();
			DB_grid.ItemsSource = _entityViews;
		}

		private void SelectBtnClick(object sender, RoutedEventArgs e)
		{
			_isSelected = true;
			this.Hide();
		}

		private void CreateBtnClick(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void CancelBtnClick(object sender, RoutedEventArgs e)
		{
			_isSelected = false;
			this.Hide();
		}

		private void FilterTb_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(FilterTb.Text))
			{
				_entityViews.Filter = (entity) =>
				{
					foreach(var p in _filteringProperties)
					{
						if(p.GetValue(entity)?.ToString() == FilterTb.Text)
						{
							return true;
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
