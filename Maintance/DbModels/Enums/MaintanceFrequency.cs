using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintance.DbModels
{
	public enum MaintanceFrequency
	{
		[Description("По необходимости")]
		OnNeed,
		[Description("Ежедневно")]
		Everyday,
		[Description("Несколько раз в неделю")]
		FewTimesWeek,
		[Description("Раз в неделю")]
		OneTimeWeek,
		[Description("Несколько раз в месяц")]
		FewTimesMonth,
		[Description("Раз в месяц")]
		OneTimeMonth
	}
}
