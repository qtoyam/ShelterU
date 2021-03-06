using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows.Data;
using System.Reflection;
using Maintance.TableAutomation.Models;
using Maintance.TableAutomation.DbModelAttributes;

namespace Maintance.TableAutomation
{
	public static class AutomationHelper
	{
		public static IEnumerable<TableColumnInfo> GetTableColumnInfos<T>()
		{
			foreach (var prop in typeof(T).GetProperties())
			{
				PropertyInfoAttribute? pia = null;
				ViewColumnAttribute? vca = null;
				SelectionColumnAttribute? sca = null;
				foreach (var attr in prop.GetCustomAttributes(true))
				{
					if(attr is PropertyInfoAttribute piat)
					{
						pia = piat;
					}
					else if(attr is SelectionColumnAttribute scat)
					{
						sca = scat;
					}
					else if (attr is ViewColumnAttribute vcat)
					{
						vca = vcat;
					}
				}
				if (pia == null) continue;
				if (string.IsNullOrEmpty(pia.DisplayName))
				{
					pia = new(prop.Name, pia.IsAutofoFill, pia.IsOptional);
				}
				yield return new(prop, pia, vca, sca);
			}
		}

		public static IReadOnlyDictionary<int, string> GetEnumDescriptions(Type enumType)
		{
			Dictionary<int, string> enumStrDict = new();
			foreach (var field in enumType.GetFields())
			{
				var attrs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (attrs.Length > 0 && attrs[0] is DescriptionAttribute attr && field.GetRawConstantValue() is int rawV)
				{
					enumStrDict.Add(rawV, attr.Description);
				}
			}
			return enumStrDict;
		}
	}
}
