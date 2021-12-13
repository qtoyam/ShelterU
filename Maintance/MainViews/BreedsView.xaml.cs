using System;
using System.Collections.Generic;
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

namespace Maintance.MainViews
{
	/// <summary>
	/// Interaction logic for BreedsView.xaml
	/// </summary>
	public partial class BreedsView : UserControl
	{
		public BreedsView()
		{
			InitializeComponent();
			_changeFilterTBWidth = new(100, TimeSpan.FromSeconds(0.5));

		}

		private DataGridColumn? _hiddenCol;
		private void GroupBy_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var gp = (string)e.AddedItems[0]!;
			if (!string.IsNullOrEmpty(gp))
			{
				foreach (var c in DB_grid.Columns)
				{
					if (c.Header is string h && h == gp)
					{
						c.Visibility = Visibility.Collapsed;
						if (_hiddenCol != null)
						{
							_hiddenCol.Visibility = Visibility.Visible;
						}
						_hiddenCol = c;
						return;
					}
				}
			}
			else if (_hiddenCol != null)
			{
				_hiddenCol.Visibility = Visibility.Visible;
				_hiddenCol = null;
			}
		}

		readonly DoubleAnimation _changeFilterTBWidth;
		private void FilterBy_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var fp = (string)e.AddedItems[0]!;
			if (string.IsNullOrEmpty(fp))
			{
				_changeFilterTBWidth.To = 0;
			}
			else
			{
				_changeFilterTBWidth.To = 100;
			}
			FilterBy_TB.BeginAnimation(TextBox.WidthProperty, _changeFilterTBWidth);
		}
	}
}
