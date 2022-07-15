using System;

using Maintance.TableAutomation.DbModelAttributes;

namespace Maintance.DbModels
{
	[TableInfo("Лечение")]
	public partial class Treatment : IDBModelAdmin
	{
		[PropertyInfo(displayName: "ID", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: false)]
        public int TreatmentId { get; set; }
		
        public int ClinicId { get; set; }
		
        public int AnimalId { get; set; }
		
		[PropertyInfo(displayName: "Дата и время отправки в клинику", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
        public DateTime DateOfStart { get; set; } = DateTime.Now;

		[PropertyInfo(displayName: "Дата и время возвращения", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		public DateTime DateOfEnd { get; set; } = DateTime.Now;
		
		[PropertyInfo(displayName: "Цель визита", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public string Goal { get; set; } = null!;
		
		[PropertyInfo(displayName: "Проблема животного", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: false)]
        public string? Problem { get; set; }
		
		[PropertyInfo(displayName: "Результат лечения", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: false)]
        public string Result { get; set; } = null!;

		[PropertyInfo(displayName: "Животное", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Animal Animal { get; set; } = null!;

		[PropertyInfo(displayName: "Клиника", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Clinic Clinic { get; set; } = null!;

		public override string ToString() => TreatmentId.ToString();

	}
}
