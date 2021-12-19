using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintance.TableAutomation.Models
{
	[AttributeUsage(AttributeTargets.Class)]
	internal class TableInfoAttribute : Attribute
	{
		public readonly string Name;

		public TableInfoAttribute(string name)
		{
			Name = name;
		}
	}
}
