using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintance.DbModels
{
	public enum AnimalMovementStatus
	{
		[Description("Найден")]
		Found,
		[Description("Отдан")]
		GivenAway,
		[Description("Временно в приюте")]
		Temporary
	}
}
