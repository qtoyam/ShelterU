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

using Maintance.Models;
using Maintance.ViewModels;

namespace Maintance.MainViews
{
	/// <summary>
	/// Interaction logic for AnimalsView.xaml
	/// </summary>
	public partial class AnimalsView : UserControl
	{

		public AnimalsView()
		{
			InitializeComponent();
			GroupBy_CB.Items.Add(nameof(Animal.Name));
			GroupBy_CB.Items.Add(nameof(Animal.Type));
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
