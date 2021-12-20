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
using System.Windows.Shapes;

using Maintance.DbModels;

namespace Maintance.Views
{
	/// <summary>
	/// Interaction logic for ChangePasswordWindow.xaml
	/// </summary>
	public partial class ChangePasswordWindow : Window
	{
		private readonly ShelterContext sc;

		public ChangePasswordWindow(ShelterContext sc)
		{
			InitializeComponent();
			LabelTop.Content = sc._username;
			this.sc = sc;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if(sc._pass == OldPassTb.Password)
			{
				sc.ChangePassword(NewPasswordTb.Password);
				MessageBox.Show("Пароль успешно изменён.");
				this.Close();
			}
			else
			{
				MessageBox.Show("Неверный пароль!");
			}
		}
	}
}
