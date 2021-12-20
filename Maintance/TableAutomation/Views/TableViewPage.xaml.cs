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
	/// Interaction logic for TablePage.xaml
	/// </summary>
	public partial class TableViewPage : Page
	{
		private readonly ITableManager _tableManager;
		private readonly ICollectionView _entitiesView;

		private readonly List<PropertyInfo> _realFilteringProps = new();
		private readonly List<PropertyInfo> _realGroupingProps = new();
		private readonly List<DataGridColumn> _groupLinkedColumns = new();

		private PropertyInfo? _currentFilteringProperty;
		private IReadOnlyDictionary<int, string>? _enumFilterValues;

		public TableViewPage(ITableManager tableManager)
		{
			InitializeComponent();
			_tableManager = tableManager;
			var cols = DB_grid.Columns;
			List<string> filteringOptions = new() { "" };
			List<string> groupingOptions = new() { "" };
			foreach (var vcp in _tableManager.TableColumnInfos)
			{
				if (vcp.ViewColumnAttribute == null) continue;
				Binding b = new(vcp.PropertyInfo.Name)
				{
					Mode = BindingMode.OneWay
				};
				if (vcp.PropertyInfo.PropertyType.IsEnum)
				{
					b.Converter = new EnumToStrConverter(AutomationHelper.GetEnumDescriptions(vcp.PropertyInfo.PropertyType),
						vcp.PropertyInfo.PropertyType);
				}
				cols.Add(
					new MaterialDesignThemes.Wpf.DataGridTextColumn()
					{
						Header = vcp.PropertyInfoAttribute.DisplayName,
						Binding = b,
						IsReadOnly = true
					});
				if (vcp.ViewColumnAttribute.IsFilter)
				{
					filteringOptions.Add(vcp.PropertyInfoAttribute.DisplayName);
					_realFilteringProps.Add(vcp.PropertyInfo);
				}
				if (vcp.ViewColumnAttribute.IsGroup)
				{
					groupingOptions.Add(vcp.PropertyInfoAttribute.DisplayName);
					_realGroupingProps.Add(vcp.PropertyInfo);
					_groupLinkedColumns.Add(cols.Last());
				}

			}
			FilterBy_CB.ItemsSource = filteringOptions;
			GroupBy_CB.ItemsSource = groupingOptions;

			_entitiesView = _tableManager.CreateCollectionView();
			DB_grid.ItemsSource = _entitiesView;
		}

		private DataGridColumn? _hiddenCol;
		private void GroupingChanged(object sender, SelectionChangedEventArgs e)
		{
			var si = GroupBy_CB.SelectedIndex;
			var gd = _entitiesView.GroupDescriptions;
			gd.Clear();
			if (si > 0)
			{
				var gCol = _groupLinkedColumns[si - 1];
				gCol.Visibility = Visibility.Collapsed;
				var t = _realGroupingProps[si - 1];
				gd.Add(new PropertyGroupDescription(t.Name));
				if (_hiddenCol != null)
				{
					_hiddenCol.Visibility = Visibility.Visible;
				}
				_hiddenCol = gCol;
			}
			else if (_hiddenCol != null)
			{
				_hiddenCol.Visibility = Visibility.Visible;
				_hiddenCol = null;
			}
		}

		private void FilteringChanged(object sender, TextChangedEventArgs e)
		{
			if (_currentFilteringProperty == null) return;
			var filterText = FilterBy_TB.Text;
			if (!string.IsNullOrEmpty(filterText))
			{
				if (_enumFilterValues != null)
				{
					_entitiesView.Filter = x =>
						{
							if (_enumFilterValues.TryGetValue((int)_currentFilteringProperty.GetValue(x)!, out var str))
							{
								return str.Contains(filterText, StringComparison.OrdinalIgnoreCase);
							}
							return false;
						};
				}
				else
				{
					_entitiesView.Filter = x =>
						_currentFilteringProperty
							.GetValue(x)?
							.ToString()?
							.Contains(filterText, StringComparison.OrdinalIgnoreCase) == true;
				}
			}
			else
			{
				_entitiesView.Filter = null;
			}
		}

		private readonly DoubleAnimation _changeFilterTBWidth = new(100, TimeSpan.FromSeconds(0.5));

		private void FilterBy_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var index = FilterBy_CB.SelectedIndex;
			if (index < 1)
			{
				_changeFilterTBWidth.To = 0;
				_currentFilteringProperty = null;
				_entitiesView.Filter = null;
				FilterBy_TB.Clear();
				_enumFilterValues = null;
			}
			else
			{
				_changeFilterTBWidth.To = 100;
				_currentFilteringProperty = _realFilteringProps[index - 1];
				_enumFilterValues = _currentFilteringProperty.PropertyType.IsEnum
					? AutomationHelper.GetEnumDescriptions(_currentFilteringProperty.PropertyType)
					: null;
			}
			FilterBy_TB.BeginAnimation(TextBox.WidthProperty, _changeFilterTBWidth);
		}

		private void AddBtn_Click(object sender, RoutedEventArgs e)
		{
			this.IsEnabled = false;
			_tableManager.TryCreateEntity(Application.Current.MainWindow);
			this.IsEnabled = true;
		}

		private void EditBtn_Click(object sender, RoutedEventArgs e)
		{
			var selectedEntity = DB_grid.SelectedItem;
			if (selectedEntity == null) return;
			this.IsEnabled = false;
			_tableManager.TryEditEntity(selectedEntity, Application.Current.MainWindow);
			this._entitiesView.Refresh();
			this.IsEnabled = true;
		}

		private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
		{
			var selectedEntity = DB_grid.SelectedItem;
			if (selectedEntity == null) return;
			this.IsEnabled = false;

			var res = await _tableManager.TryDeleteEntity(selectedEntity);
			if (res == true) MessageBox.Show($"Удалено: {selectedEntity}");
			this.IsEnabled = true;
		}
	}
}
