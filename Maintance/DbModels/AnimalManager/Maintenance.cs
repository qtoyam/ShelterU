using System;
using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Содержание животных", 8)]
	public partial class Maintenance : IDBModelAnimalManager
	{

		[PropertyInfo(displayName: "ID", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: false)]
		public int MaintenanceId { get; set; }

		[PropertyInfo(displayName: "Дата", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public DateOnly DateOfAction { get; set; } = DateOnly.FromDateTime(DateTime.Now);

		[PropertyInfo(displayName: "Время", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public TimeOnly TimeOfAction { get; set; }
		
		[PropertyInfo(displayName: "Тип взаимодействия", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual TypeOfMaintenance TypeMain { get; set; } = null!;

		[PropertyInfo(displayName: "Животное", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Animal Animal { get; set; } = null!;

		[PropertyInfo(displayName: "Сотрудник", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Employee Employee { get; set; } = null!;
		
		[PropertyInfo(displayName: "Использованный реквизит", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		public virtual Provision Requisite { get; set; } = null!;
		
		[PropertyInfo(displayName: "Расход", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: false)]
		public int? WasteOfResource { get; set; }
		
		[PropertyInfo(displayName: "Наличие проблемы", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public bool? Problem { get; set; }

		[PropertyInfo(displayName: "Комментарий", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: false)]
		public string? Comment { get; set; }


        public int TypeMainId { get; set; }
        public int RequisiteId { get; set; }
        public int AnimalId { get; set; }
        public int EmployeeId { get; set; }

		public override string ToString() => MaintenanceId.ToString();

	}
}
