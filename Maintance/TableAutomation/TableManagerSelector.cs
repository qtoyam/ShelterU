using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System.Windows.Controls;

using Maintance.TableAutomation;

using Microsoft.Extensions.DependencyInjection;
using Maintance.TableAutomation.DbModelAttributes;
using Maintance.DbModels;
using System.Collections.Generic;

namespace Maintance.TableAutomation
{
	public class TableManagerSelector
	{
		private IServiceProvider _sp = null!;

		public ReadOnlyCollection<string> TableNames { get; }

		private readonly ReadOnlyCollection<Type> _tableTypes;

		public TableManagerSelector(ServiceCollection sc, string role)
		{
			Type searchingInterface;
			var generalType = typeof(IDBModelGeneral);
			if (role.Contains("admin"))
			{
				searchingInterface = typeof(IDBModelAdmin);
			}
			else if (role.Contains("animal_worker"))
			{
				searchingInterface = typeof(IDBModelAnimalManager);
			}
			else throw new ArgumentException("Unexpected role!");
			var mtype = typeof(TableManager<>);
			List<(string name, int index)> tinfos = new();
			List<Type> types = new();
			foreach (var tableType in Assembly.GetCallingAssembly().GetTypes())
			{
				if (tableType.GetInterfaces().Any(x=>x == searchingInterface || x == generalType))
				{
					var tinfo = tableType.GetCustomAttribute<TableInfoAttribute>();
					var genmtype = mtype.MakeGenericType(tableType);
					tinfos.Add(new(tinfo?.Name ?? tableType.Name, tinfo?.Index ?? int.MaxValue));
					types.Add(genmtype);
					sc.AddSingleton(genmtype);
				}
			}
			sc.AddSingleton(this);

			var t = tinfos.ToArray();
			var t2 = types.ToArray();
			Array.Sort(t, t2);
			tinfos = t.ToList();
			types = t2.ToList();
			TableNames = new ReadOnlyCollection<string>(tinfos.Select(x=>x.name).ToList());
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
