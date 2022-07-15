using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintance.TableAutomation.Models
{
	public record DateFilterInfo (string Name, int MaxOffset)
	{
		public override string ToString() => Name;
	}
}
