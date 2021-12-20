using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintance.DbModels
{
	public enum WeekDay
	{
		[Description("Пн")]
		Monday,
		[Description("Вт")]
		Tuesday,
		[Description("Ср")]
		Wednesday,
		[Description("Чт")]
		Thursday,
		[Description("Пт")]
		Friday,
		[Description("Сб")]
		Saturday,
		[Description("Пн")]
		Sunday
	}
}
