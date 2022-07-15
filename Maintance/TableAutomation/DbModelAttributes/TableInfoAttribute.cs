using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintance.TableAutomation.DbModelAttributes
{
	[AttributeUsage(AttributeTargets.Class)]
	internal class TableInfoAttribute : Attribute
	{
		public readonly string Name;
        public readonly int Index;

        public TableInfoAttribute(string name, int index = int.MaxValue)
		{
			Name = name;
            Index = index;
        }
	}
}
