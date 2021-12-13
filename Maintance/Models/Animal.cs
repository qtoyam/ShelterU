using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maintance.Models.Attributes;

namespace Maintance.Models
{
#nullable disable
	public class Animal
	{
		[FilterProperty]
		public int ID { get; set; }

		[FilterProperty]
		[Description("Кличка")]
		public string Name { get; set; }

		[Description("Возраст")]
		public int Age { get; set; }

		[Description("Порода")]
		[FilterProperty]
		[GroupProperty]
		public string BreedName { get; set; }

		[Description("Дата рождения")]
		public DateTime Birthday { get; set; }

		[Description("Рост")]
		public int Height { get; set; }

		[Description("Вес")]
		public int Weight { get; set; }


		private DateTime _arrival;
		[Description("Дата прибытия")]
		public DateTime Arrival { get; set; }
	}
}
