using System;
using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;

namespace Maintance.DbModels
{
	[TableInfo("График работы сотрудников")]
    public partial class Workschedule
	{
		[PropertyInfo(displayName: "ID сотрудника", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public int EmployeeId { get; set; }
		
		[PropertyInfo(displayName: "Фамилия", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public string LastName { get; set; } = null!;
		
		[PropertyInfo(displayName: "Имя", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public string FirstName { get; set; } = null!;
		
		[PropertyInfo(displayName: "День недели", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public string DayOfWeek { get; set; } = null!;
		
		[PropertyInfo(displayName: "С", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public TimeOnly Beginning { get; set; }
		
		[PropertyInfo(displayName: "До", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public TimeOnly Ending { get; set; }
		
		[PropertyInfo(displayName: "По четным неделям", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public bool? EvenWeek { get; set; }
		
		[PropertyInfo(displayName: "По нечетным неделям", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public bool? OddWeek { get; set; }
    }
}
