using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using Maintance.DbModels;
using Maintance.TableAutomation;
using Maintance.TableAutomation.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Serilog;
using Serilog.Core;

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
		private ServiceProvider? _serviceProvider;

		public App()
		{
			this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			this.Dispatcher.UnhandledException += Dispatcher_UnhandledException;

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.WriteTo.Debug()
				.WriteTo.File(@"logs\log.txt")
				.CreateLogger();

		}

		private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			Log.Error(e.Exception, "Unhandler error: {Err}", e.Exception.Message);
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			try
			{
				var loginWin = new Views.LoginWindow();
				loginWin.ShowDialog();
				if (loginWin.Context == null)
				{
					Application.Current?.Shutdown(4);
					return;
				}
				if (loginWin.Role == null)
				{
					loginWin.Context.Dispose();
					Application.Current.Shutdown(8);
					return;
				}
				ServiceCollection services = new();
				TableManagerSelector tms;
				tms = new(services, loginWin.Role);
				var context = loginWin.Context;
				loginWin = null;
				_serviceProvider = services
					.AddSingleton<IMessageService, EventMessageService>()
					.AddSingleton<MainWindow>()
					.AddSingleton<SynchronizationContext>(new DispatcherSynchronizationContext(Application.Current.Dispatcher))
					.AddSingleton<DbContext, ShelterContext>((s) => context)
					.AddSingleton<SemaphoreSlim>((s) => new(1))
					.BuildServiceProvider(
#if DEBUG
				new ServiceProviderOptions() { ValidateOnBuild = true, ValidateScopes = true }
#endif
				);
				tms.EnableSelecting(_serviceProvider);
				_serviceProvider.GetRequiredService<MainWindow>().Show();
				Log.Debug("App started.");
				base.OnStartup(e);
			}
			catch (Exception ex)
			{
				Log.Debug(ex.Message);
				MessageBox.Show(ex.Message);
				Application.Current?.Shutdown(-128);
				return;
			}
		}

		protected override void OnExit(ExitEventArgs e)
		{
			//idk bd is disposed?
			base.OnExit(e);
			_serviceProvider?.Dispose();
			Log.Debug("App exited.");
			Log.CloseAndFlush();
			this.DispatcherUnhandledException -= Dispatcher_UnhandledException;
		}
	}
}
