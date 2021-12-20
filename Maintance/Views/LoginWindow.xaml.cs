using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
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

using Microsoft.EntityFrameworkCore;

using MySqlConnector;

namespace Maintance.Views
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();
		}
		public ShelterContext? Context { get; private set; }
		public string? Role { get; private set; }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Context = new(LoginTb.Text, PasswordTb.Password);
				Context.Database.OpenConnection();
				using (var command = Context.Database.GetDbConnection().CreateCommand())
				{
					command.CommandType = System.Data.CommandType.Text;
					command.CommandText = "SELECT CURRENT_ROLE();";
					Role = (string?)command.ExecuteScalar();
				}

				this.Close();
			}
			catch (Exception ex)
			{
				Context?.Dispose();
				MessageBox.Show(ex.Message);
				return;
			}
		}
	}
}
