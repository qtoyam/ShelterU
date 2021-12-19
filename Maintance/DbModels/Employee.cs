using System;
using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Сотрудники")]
	public partial class Employee : IDBModel
	{
        public Employee()
        {
            Maintenances = new HashSet<Maintenance>();
            Timetables = new HashSet<Timetable>();
            Visits = new HashSet<Visit>();
        }

		[PropertyInfo(displayName: "", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public int EmployeeId { get; set; }

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string FirstName { get; set; } = null!;

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string LastName { get; set; } = null!;

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string? MiddleName { get; set; }

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public sbyte? Seniority { get; set; }

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public DateOnly Birthday { get; set; }

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Contact { get; set; } = null!;

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Gender { get; set; } = null!;
		//TODO: enum

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Document { get; set; } = null!;

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Post Post { get; set; } = null!;

		#region Hidden props
        public int PostId { get; set; }
		public virtual ICollection<Maintenance> Maintenances { get; set; }
        public virtual ICollection<Timetable> Timetables { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
		#endregion //Hidden props
	}
}
