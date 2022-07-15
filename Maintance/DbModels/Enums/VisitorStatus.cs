using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintance.DbModels
{
	public enum VisitorStatus
	{
		[Description("Обычный")]
		Default,
		[Description("Связан с животным")]
		LinkedWithAnimal
	}
}
