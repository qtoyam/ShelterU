using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintance.DbModels
{
	public enum Gender
	{
		[Description("М")]
		Male,
		[Description("Ж")]
		Female
	}
}
