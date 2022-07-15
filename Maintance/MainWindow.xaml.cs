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
using Maintance.TableAutomation;
using Maintance.TableAutomation.Views;
using Maintance.Views;

using Microsoft.EntityFrameworkCore;

using WPFCoreEx.Abstractions.Services;
using WPFCoreEx.Services;

namespace Maintance
{
	public record NavigationItem(string Name, DrawingImage? Image);

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly EventMessageService _ems;
		private readonly TableManagerSelector _tableManagerSelector;
		private readonly ShelterContext _sc;

		public MainWindow(IMessageService ims, TableManagerSelector tableManagerSelector, DbContext sc)
		{
			InitializeComponent();
			_ems = (EventMessageService)ims;
			_tableManagerSelector = tableManagerSelector;
			_sc = (ShelterContext)sc;
			this.TitleTb.Text = $"Shelter | {_sc._username}";
			_ems.RegisterAllDefault(this);
			Navigation_list.ItemsSource = _tableManagerSelector.TableNames.Select(x => new NavigationItem(x,
				Application.Current.TryFindResource(x) as DrawingImage));
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
			FrameView.Content = e.AddedItems.Count > 0 && e.AddedItems[0] is NavigationItem ni ?
				_tableManagerSelector.GetViewPage(ni.Name)
				: null;
		}

		private void Image_MouseDown(object sender, MouseButtonEventArgs e)
		{
			new ChangePasswordWindow(_sc) { Owner = this }.ShowDialog();
		}

		private void TitleTb_MouseDown(object sender, MouseButtonEventArgs e)
		{
			_ems.SendMessage(
				@"Лиза, я лишь хочу признаваться тебе,
						Что я с детства влюблен, дать тепло батарей.
						Холод осени с Лизой заменим весной;
						Майский парк на скамейках - сидим мы с тобой.
						Она мне говорит, как отлично, что я
						Ей сказал, что взаимности толком не ждал;
						Просто у меня есть уже план в голове,
						Ведь с четвертого класса я думал о ней!
						Лиза, ты будешь моей!
						Лиза - май среди январей!
						Лиза, я лишь хочу признаваться тебе,
						Что я с детства влюблен, дать тепло батарей.
						Холод осени с Лизой заменим весной;
						Майский парк на скамейках - сидим мы с тобой;
						Сидим мы с тобой.");
		}
	}
}
