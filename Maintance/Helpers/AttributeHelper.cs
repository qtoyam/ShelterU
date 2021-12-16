using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maintance.DbModels.Attributes;
using Maintance.DbModels;
using System.ComponentModel;
using System.Windows.Data;

namespace Maintance.Helpers
{
	public static class AttributeHelper
	{
		public static IReadOnlyList<PropertyGroupDescription> GetGroupingProperties(Type type, out IReadOnlyList<string> descriptions)
		{
			List<string> descrs = new()
			{
				string.Empty //as none
			};
			var res = type.GetProperties()
				.Where(p => Attribute.IsDefined(p, typeof(GroupPropertyAttribute)))
				.Select(x =>
				{
					descrs.Add(
						x.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() is DescriptionAttribute d ? 
						d.Description : x.Name);
					return new PropertyGroupDescription(x.Name);
				}
				)
				.ToList();
			descriptions = descrs;
			return res;
		}

		public static IReadOnlyList<string> GetFilteringProperties(Type type, out IReadOnlyList<string> descriptions)
		{
			List<string> descrs = new()
			{
				string.Empty //as none
			};
			var res = type.GetProperties()
				.Where(p => Attribute.IsDefined(p, typeof(FilterPropertyAttribute)))
				.Select(x =>
				{
					descrs.Add(
						x.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() is DescriptionAttribute d ?
						d.Description : x.Name);
					return x.Name;
				}
				)
				.ToList();
			descriptions = descrs;
			return res;

		}
	}
}
