using System;
using System.Collections.Generic;

using Maintance.DbModels.Attributes;

namespace Maintance.DbModels
{
    public partial class Maintenance
    {

		[ViewColumn("Id", false, false)]
		public int MaintenanceId { get; set; }

		[ViewColumn("Начало", false, false)]
		public DateOnly DateOfAction { get; set; }

		[ViewColumn("Завершение", false, false)]
		public TimeOnly TimeOfAction { get; set; }

		[ViewColumn("Проблема", false, false)]
		public bool? Problem { get; set; }

		[ViewColumn("Комментарий", false, false)]
		public string? Comment { get; set; }

		[ViewColumn("Расход реквизита", false, false)]
		public int? WasteOfResource { get; set; }

		[ViewColumn("Животное", false, false)]
		public virtual Animal Animal { get; set; } = null!;

		[ViewColumn("Сотрудник", false, false)]
		public virtual Employee Employee { get; set; } = null!;

		[ViewColumn("Реквизит", false, false)]
		public virtual Provision Requisite { get; set; } = null!;

		[ViewColumn("Тип", false, false)]
		public virtual TypeOfMaintenance TypeMain { get; set; } = null!;


        public int TypeMainId { get; set; }
        public int RequisiteId { get; set; }
        public int AnimalId { get; set; }
        public int EmployeeId { get; set; }
    }
}
