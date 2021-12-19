using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Maintance.DbModels;
using Maintance.Services;
using Maintance.TableAutomation;
using Maintance.TableAutomation.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
		//TODO: navigation HEADER AXAXAXA
		private readonly ServiceProvider _serviceProvider;

		public App()
		{
			this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			//this.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.WriteTo.Debug()
				//.WriteTo.File(@"logs\log.txt")
				.CreateLogger();
			ServiceCollection services = new();
			TableManagerSelector tms = new(services);
			_serviceProvider = services
				.AddSingleton<IMessageService, EventMessageService>()
				.AddSingleton<MainWindow>()
				.AddDbContextFactory<ShelterContext>(
				  o => o.UseMySql("server=localhost;database=shelter;uid=root;pwd=zxc123",
				  ServerVersion.Parse("8.0.27-mysql"))
				  )
				.BuildServiceProvider(
#if DEBUG
				new ServiceProviderOptions() { ValidateOnBuild = true, ValidateScopes = true }
#endif
				);
			tms.EnableSelecting(_serviceProvider);

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
			//idk bd is disposed?
			base.OnExit(e);
			_serviceProvider.Dispose();
			Log.Debug("App exited.");
			Log.CloseAndFlush();
			//this.DispatcherUnhandledException -= Dispatcher_UnhandledException;
		}
	}
}
