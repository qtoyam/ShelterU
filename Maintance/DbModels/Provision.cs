using System;
using System.Collections.Generic;

using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	public partial class Provision
    {
        public Provision()
        {
            Maintenances = new HashSet<Maintenance>();
        }


		[ViewColumn("Id", false, false)]
		public int RequisiteId { get; set; }

		[ViewColumn("Назв", false, false)]
		public string Name { get; set; } = null!;

		[ViewColumn("Кол-во", false, false)]
		public int Quantity { get; set; }

		[ViewColumn("Произв.", false, false)]
		public string? Producer { get; set; }

		[ViewColumn("Для кого", false, false)]
		public string? ForWhichAnimals { get; set; }

		[ViewColumn("Годен до", false, false)]
		public DateOnly? ValidUntil { get; set; }

		[ViewColumn("Описание", false, false)]
		public string? Description { get; set; }

		[ViewColumn("Нужно купить", false, false)]
		public bool NeedToBuy { get; set; }

		[ViewColumn("Тип", false, false)]
		public virtual TypeOfRequisite TypeReq { get; set; } = null!;
        public virtual ICollection<Maintenance> Maintenances { get; set; }
        public int TypeReqId { get; set; }
    }
}
