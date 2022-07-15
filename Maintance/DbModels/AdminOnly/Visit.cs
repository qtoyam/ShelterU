using System;
using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;

namespace Maintance.DbModels
{
	[TableInfo("Посещаемость")]
	public partial class Visit : IDBModelAdmin
	{
		[PropertyInfo(displayName: "Номер визита", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		public int NumberOfVisit { get; set; }

		public int VisitorId { get; set; }

		[PropertyInfo(displayName: "Дата визита", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public DateTime DateOfVisit { get; set; } = DateTime.Now;

		[PropertyInfo(displayName: "Цель визита", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public VisitGoal Goal { get; set; }

		public int ResponsEmployee { get; set; }

		[PropertyInfo(displayName: "Результат визита", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: false)]
		public string? Result { get; set; }


		[PropertyInfo(displayName: "Посетитель", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Visitor Visitor { get; set; } = null!;

		[PropertyInfo(displayName: "Отвественный сотрудник", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: false)]
		public virtual Employee ResponsEmployeeNavigation { get; set; } = null!;

		public override string ToString() => NumberOfVisit.ToString();

	}
}
