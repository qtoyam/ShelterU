using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows.Data;
using System.Reflection;
using Maintance.TableAutomation.Models;

namespace Maintance.TableAutomation
{
	public static class AutomationHelper
	{
		public static IEnumerable<TableColumnInfo> GetTableColumnInfos<T>()
		{
			foreach (var prop in typeof(T).GetProperties())
			{
				var attrs = prop.GetCustomAttributes(typeof(ViewColumnAttribute), true);
				if (attrs.Length > 0)
				{
					yield return new(prop, (ViewColumnAttribute)attrs[0]);
				}
			}
		}
	}
}
