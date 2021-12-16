using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

using Maintance.DbModels;
using Maintance.ViewModels;

namespace Maintance.ExViews
{
	/// <summary>
	/// Interaction logic for GenusSelectorWindow.xaml
	/// </summary>
	public partial class GenusSelectorWindow : Window
	{
		public GenusSelectorWindow(GenusSelectorVM vm)
		{
			InitializeComponent();
			DataContext = vm;
		}
	}
}