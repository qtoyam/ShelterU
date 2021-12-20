using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintance.DbModels
{
	public enum VisitGoal
	{
		[Description("Обычное посещение")]
		Default,
		[Description("Отдать животное")]
		GiveAwayAnimal,
		[Description("Взять животное")]
		TakeAnimal
	}
}
