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

using Maintance.ViewModels;

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

		public MainWindow(MainViewModel viewModel, IMessageService ims)
		{
			InitializeComponent();
			DataContext = viewModel;
			_ems = (EventMessageService)ims;
			_ems.RegisterAllDefault(this);
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
	}
}
