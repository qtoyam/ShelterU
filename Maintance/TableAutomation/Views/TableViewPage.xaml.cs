using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
	/// Interaction logic for TablePage.xaml
	/// </summary>
	public partial class TableViewPage : Page
	{
		private readonly List<string> _realFilteringPropNames;
		private readonly List<string> _realGroupingPropNames;
		private readonly List<DataGridColumn> _groupLinkedColumns = new();
		private readonly ICollectionView _entitiesView;

		public TableViewPage(ITableManager tableManager)
		{
			InitializeComponent();

			var cols = DB_grid.Columns;
			List<string> filteringOptions = new() { "" };
			_realFilteringPropNames = new() { "" };
			List<string> groupingOptions = new() { "" };
			_realGroupingPropNames = new() { "" };
			foreach (var vcp in tableManager.TableColumnInfos)
			{
				cols.Add(
					new MaterialDesignThemes.Wpf.DataGridTextColumn()
					{
						Header = vcp.ViewColumnAttribute.ViewColumnName,
						Binding = new Binding(vcp.PropertyInfo.Name)
						{
							Mode = BindingMode.OneWay,
						},
						IsReadOnly = true
					});
				if (vcp.ViewColumnAttribute.IsFilter)
				{
					filteringOptions.Add(vcp.ViewColumnAttribute.ViewColumnName);
					_realFilteringPropNames.Add(vcp.PropertyInfo.Name);
				}
				if (vcp.ViewColumnAttribute.IsGroup)
				{
					groupingOptions.Add(vcp.ViewColumnAttribute.ViewColumnName);
					_realGroupingPropNames.Add(vcp.PropertyInfo.Name);
					_groupLinkedColumns.Add(cols.Last());
				}
			}
			FilterBy_CB.ItemsSource = filteringOptions;
			GroupBy_CB.ItemsSource = groupingOptions;

			_entitiesView = tableManager.CreateCollectionView();
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
				gd.Add(new PropertyGroupDescription(_realGroupingPropNames[si]));
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
			if (FilterBy_CB.SelectedIndex < 1) return;
			if (!string.IsNullOrEmpty(FilterBy_TB.Text))
			{
				_entitiesView.Filter = x =>
				x.GetType().GetProperty(_realFilteringPropNames[FilterBy_CB.SelectedIndex])!.GetValue(x)!.ToString()!
				.Equals(FilterBy_TB.Text);
			}
			else
			{
				_entitiesView.Filter = null;
			}
		}

		private readonly DoubleAnimation _changeFilterTBWidth = new(100, TimeSpan.FromSeconds(0.5));
		private void FilterBy_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var fp = (string)e.AddedItems[0]!;
			_changeFilterTBWidth.To = string.IsNullOrEmpty(fp) ? 0 : 100;
			FilterBy_TB.BeginAnimation(TextBox.WidthProperty, _changeFilterTBWidth);
		}
	}
}
