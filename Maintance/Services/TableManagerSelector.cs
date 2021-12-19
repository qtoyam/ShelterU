using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System.Windows.Controls;

using Maintance.TableAutomation;
using Maintance.TableAutomation.Models;

using Microsoft.Extensions.DependencyInjection;
using Maintance.DbModels;
using Maintance.TableAutomation.Views;
using Maintance.TableAutomation.DbModelAttributes;

namespace Maintance.Services
{
	public class TableManagerSelector
	{
		private IServiceProvider _sp = null!;

		public ReadOnlyCollection<string> TableNames { get; }

		private readonly ReadOnlyCollection<Type> _tableTypes;

		public TableManagerSelector(ServiceCollection sc)
		{
			var mtype = typeof(TableManager<>);
			Collection<string> names = new();
			Collection<Type> types = new();
			foreach (var tableType in Assembly.GetCallingAssembly().GetTypes())
			{
				if (tableType.GetInterfaces().Contains(typeof(IDBModel)))
				{
					var tinfo = tableType.GetCustomAttribute<TableInfoAttribute>();
					var genmtype = mtype.MakeGenericType(tableType);
					names.Add(tinfo?.Name ?? tableType.Name);
					types.Add(genmtype);
					sc.AddSingleton(genmtype);
				}
			}
			sc.AddSingleton(this);

			TableNames = new ReadOnlyCollection<string>(names);
			_tableTypes = new ReadOnlyCollection<Type>(types);
		}

		public Page GetViewPage(string name)
		{
			var index = TableNames.IndexOf(name);
			return ((ITableManager)_sp.GetRequiredService(_tableTypes[index])).ViewPage;
		}

		public ITableManager GetITableManager(Type dbModelType)
		{
			return (ITableManager)_sp.GetRequiredService(typeof(TableManager<>).MakeGenericType(dbModelType));
		}

		public void EnableSelecting(IServiceProvider sp) => _sp = sp;
	}
}
