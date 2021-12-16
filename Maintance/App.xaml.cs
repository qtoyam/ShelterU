using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Maintance.DbModels;
using Maintance.ExViews;
using Maintance.Services;
using Maintance.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using MySql.Data.MySqlClient;

using Serilog;

using WPFCoreEx.Abstractions.Services;
using WPFCoreEx.Services;

namespace Maintance
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly ServiceProvider _serviceProvider;

		public App()
		{
			//this.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.WriteTo.Debug()
				.WriteTo.File(@"logs\log.txt")
				.CreateLogger();

			ServiceCollection services = new();
			_serviceProvider = services
				.AddSingleton<IMessageService, EventMessageService>()
				.AddSingleton<MainViewModel>()
				.AddSingleton<MainWindow>()
				.AddSingleton<ViewsLocator>()
				.AddSingleton<ShelterContext>()
				.AddSingleton<SelectorLocator>()

				.AddTransient<GenusSelectorVM>()
				.AddTransient<GenusSelectorWindow>()

				//Navigation VMS:
				.AddSingleton<AnimalsViewModel>()
				.AddSingleton<SettingsViewModel>()
				.AddSingleton<BreedsViewModel>()

				.BuildServiceProvider(
#if DEBUG
				new ServiceProviderOptions() { ValidateOnBuild = true, ValidateScopes = true }
#endif
				);
		}

		private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			Log.Error(e.Exception, "Unhandler error: {Err}", e.Exception.Message);
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			_serviceProvider.GetRequiredService<MainWindow>().Show();
			Log.Debug("App started.");
			base.OnStartup(e);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			_serviceProvider.Dispose();
			Log.Debug("App exited.");
			Log.CloseAndFlush();
			//this.DispatcherUnhandledException -= Dispatcher_UnhandledException;
		}
	}
}
