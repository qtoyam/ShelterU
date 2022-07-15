using System;
using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Сотрудники", 6)]
	public partial class Employee : IDBModelGeneral
	{
		public Employee()
		{
			Maintenances = new HashSet<Maintenance>();
			Timetables = new HashSet<Timetable>();
			Visits = new HashSet<Visit>();
		}

		[PropertyInfo(displayName: "ID", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public int EmployeeId { get; set; }

		[PropertyInfo(displayName: "Имя", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string LastName { get; set; } = null!;

		[PropertyInfo(displayName: "Фамилия", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string FirstName { get; set; } = null!;

		[PropertyInfo(displayName: "Отчество", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: false)]
		public string? MiddleName { get; set; }

		[PropertyInfo(displayName: "Стаж работы", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: false)]
		public sbyte? Seniority { get; set; }

		[PropertyInfo(displayName: "Дата рождения", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: false)]
		public DateOnly Birthday { get; set; }

		[PropertyInfo(displayName: "Телефон", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		public string Contact { get; set; } = null!;

		[PropertyInfo(displayName: "Пол", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		public Gender Gender { get; set; }

		[PropertyInfo(displayName: "Серия и номер паспорта", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		public string Document { get; set; } = null!;

		[PropertyInfo(displayName: "Должность", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Post Post { get; set; } = null!;

		#region Hidden props
		public int PostId { get; set; }
		public virtual ICollection<Maintenance> Maintenances { get; set; }
		public virtual ICollection<Timetable> Timetables { get; set; }
		public virtual ICollection<Visit> Visits { get; set; }
		#endregion //Hidden props

		public override string ToString() => $"{LastName} {FirstName[0]}." +
			(string.IsNullOrEmpty(MiddleName) ? string.Empty : $" {FirstName[0]}.");
	}
}
