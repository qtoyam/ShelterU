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
		private enum FilterType
		{
			Default,
			Enum,
			Date
		}

		private readonly ITableManager _tableManager;
		private readonly ICollectionView _entitiesView;

		private readonly List<(Predicate<object> filter, FilterType ftype, System.Collections.IEnumerable? itemsSource)> _filterPredicates = new();
		private readonly List<(DataGridColumn col, string propName)> _groupInfos = new();
		private readonly PropertyGroupDescription _currentGroupDescription = new();

		public TableViewPage(ITableManager tableManager)
		{
			InitializeComponent();
			_tableManager = tableManager;
			var cols = DB_grid.Columns;
			List<string> filterOptions = new() { "" };
			List<string> groupOptions = new() { "" };
			foreach (var vcp in _tableManager.TableColumnInfos)
			{
				if (vcp.ViewColumnAttribute == null) continue;
				Binding b = new(vcp.PropertyInfo.Name)
				{
					Mode = BindingMode.OneWay
				};
				var type = vcp.PropertyInfo.PropertyType;
				IReadOnlyDictionary<int, string>? enumDescrs = null;
				if (type.IsEnum)
				{
					enumDescrs = AutomationHelper.GetEnumDescriptions(type);
					b.Converter = new EnumToStrConverter(enumDescrs,
						vcp.PropertyInfo.PropertyType);
				}
				MaterialDesignThemes.Wpf.DataGridTextColumn col = new()
				{
					Header = vcp.PropertyInfoAttribute.DisplayName,
					Binding = b,
					IsReadOnly = true
				};
				cols.Add(col);
				if (vcp.ViewColumnAttribute.IsFilter)
				{
					var propGetter = vcp.PropertyInfo.GetGetMethod() ?? throw new NotSupportedException();
					filterOptions.Add(vcp.PropertyInfoAttribute.DisplayName);
					if (enumDescrs != null) //enums
					{
						_filterPredicates.Add(new((pv) =>
						{
							if (FilterEnum.SelectedIndex < 1) return true; //0, -1 index = no item
							var val = propGetter.Invoke(pv, null);
							if (val is not int valInt) return false;
							return enumDescrs[valInt].Equals(FilterEnum.SelectedItem);
						}, FilterType.Enum, enumDescrs.Values));
					}
					else //tostring()
					{
						_filterPredicates.Add(new((pv) =>
						{
							if (string.IsNullOrEmpty(FilterString.Text)) return true; //no filter
							if (pv != null && propGetter.Invoke(pv, null)?.ToString() is string pvString)
							{
								return pvString.Contains(FilterString.Text, StringComparison.InvariantCultureIgnoreCase);
							}
							return false;
						}, FilterType.Default, null));
					}
				}
				if (vcp.ViewColumnAttribute.IsGroup)
				{
					_groupInfos.Add(new(col, vcp.PropertyInfo.Name));
					groupOptions.Add(vcp.PropertyInfoAttribute.DisplayName);
				}

			}
			FilterBy_CB.ItemsSource = filterOptions;
			GroupBy_CB.ItemsSource = groupOptions;

			_entitiesView = _tableManager.CreateCollectionView();
			DB_grid.ItemsSource = _entitiesView;
			//TODO: mb find fix (350+ error bindings) PIZDEC KOSTIL AHXXDFShDUIASK
			HandleGrouping(0);
		}

		#region Grouping
		private DataGridColumn? _hiddenCol;
		private void GroupingChanged(object sender, SelectionChangedEventArgs e)
		{
			var si = GroupBy_CB.SelectedIndex - 1; //cauze first element is empty string
			HandleGrouping(si);
		}

		private void HandleGrouping(int index)
		{
			if (index >= 0)
			{
				var gInfo = _groupInfos[index];
				_currentGroupDescription.PropertyName = gInfo.propName;
				if (_hiddenCol != null) //currently grouping
				{
					_hiddenCol.Visibility = Visibility.Visible;
				}
				else //currently no grouping
				{
					_entitiesView.GroupDescriptions.Add(_currentGroupDescription);
				}
				_hiddenCol = gInfo.col;
				_hiddenCol.Visibility = Visibility.Collapsed;
			}
			else if (_hiddenCol != null) //currently grouping and we want to turn it off
			{
				_hiddenCol.Visibility = Visibility.Visible;
				_hiddenCol = null;
				_entitiesView.GroupDescriptions.Clear();
			}
		}
		#endregion //Grouping

		#region Filtering
		private readonly DoubleAnimation _changeWidthTo = new(200, TimeSpan.FromSeconds(0.5));
		private FrameworkElement? _currentFilterControl;
		private void FilterBy_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var index = FilterBy_CB.SelectedIndex - 1; //cauze first element is empty string
			if (index >= 0)
			{
				var filterInfo = _filterPredicates[index];
				_currentFilterControl = filterInfo.ftype switch
				{
					FilterType.Default => FilterString,
					FilterType.Enum => FilterEnum,
					FilterType.Date => FilterEnum,
					_ => throw new NotImplementedException(),
				};
				_changeWidthTo.To = 200;
				_currentFilterControl.BeginAnimation(FrameworkElement.WidthProperty, _changeWidthTo);

				_entitiesView.Filter = filterInfo.filter;
				FilterEnum.ItemsSource = filterInfo.itemsSource;
				FilterEnum.SelectedIndex = -1;
				_currentFilterControl.IsEnabled = true;
			}
			else
			{
				if (_currentFilterControl != null)
				{
					_currentFilterControl.IsEnabled = false;
					_changeWidthTo.To = 0;
					_currentFilterControl.BeginAnimation(FrameworkElement.WidthProperty, _changeWidthTo);
					_currentFilterControl = null;

					_entitiesView.Filter = null;
				}
			}
		}
		private void FilterEnum_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_entitiesView.Refresh();
		}

		private void FilterString_TextChanged(object sender, TextChangedEventArgs e)
		{
			_entitiesView.Refresh();
		}
		#endregion //Filtering

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
