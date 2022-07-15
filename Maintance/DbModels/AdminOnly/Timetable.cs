using Maintance.TableAutomation.DbModelAttributes;

namespace Maintance.DbModels
{
	[TableInfo("Распределение рабочих часов")]
	public partial class Timetable : IDBModelAdmin
	{
		public int EmployeeId { get; set; }

		public int TimetId { get; set; }

		[PropertyInfo(displayName: "По четным неделям", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public bool? EvenWeek { get; set; }

		[PropertyInfo(displayName: "По нечетным неделям", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public bool? OddWeek { get; set; }

		[PropertyInfo(displayName: "Сотрудник", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Employee Employee { get; set; } = null!;

		[PropertyInfo(displayName: "Время работы", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Work Timet { get; set; } = null!;

		public override string ToString() => (EmployeeId + TimetId).ToString();
	}
}
