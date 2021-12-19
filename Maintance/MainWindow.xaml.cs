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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Maintance.DbModels;
using Maintance.Services;
using Maintance.TableAutomation;
using Maintance.TableAutomation.Views;

using Microsoft.EntityFrameworkCore;

using WPFCoreEx.Abstractions.Services;
using WPFCoreEx.Services;

namespace Maintance
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly EventMessageService _ems;
		private readonly TableManagerSelector _tableManagerSelector;

		public MainWindow(IMessageService ims, TableManagerSelector tableManagerSelector)
		{
			InitializeComponent();
			_ems = (EventMessageService)ims;
			_tableManagerSelector = tableManagerSelector;

			_ems.RegisterAllDefault(this);
			Navigation_list.ItemsSource = _tableManagerSelector.TableNames;
		}

		protected override void OnStateChanged(EventArgs e)
		{
			base.OnStateChanged(e);
			if (WindowState == WindowState.Maximized)
			{
				MainWindowBorder.BorderThickness = new Thickness(8);
			}
			else
			{
				MainWindowBorder.BorderThickness = new Thickness(0);
			}
		}

		protected override void OnClosed(EventArgs e)
		{
			_ems.UnregisterAll();
			base.OnClosed(e);
		}

		private void Navigation_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			FrameView.Content = e.AddedItems.Count > 0 && e.AddedItems[0] is string pn ?
				_tableManagerSelector.GetViewPage(pn)
				: null;
		}
	}
}
