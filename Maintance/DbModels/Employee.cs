using System;
using System.Collections.Generic;

using Maintance.DbModels.Attributes;

namespace Maintance.DbModels
{
	public partial class Employee
    {
        public Employee()
        {
            Maintenances = new HashSet<Maintenance>();
            Timetables = new HashSet<Timetable>();
            Visits = new HashSet<Visit>();
        }

		[ViewColumn("Id", false, false)]
		public int EmployeeId { get; set; }

		[ViewColumn("Имя", false, false)]
		public string FirstName { get; set; } = null!;

		[ViewColumn("Фамилия", false, false)]
		public string LastName { get; set; } = null!;

		[ViewColumn("Отчество", false, false)]
		public string? MiddleName { get; set; }
        public int PostId { get; set; }

		[ViewColumn("Стаж", false, false)]
		public sbyte? Seniority { get; set; }

		[ViewColumn("Дата рождения", false, false)]
		public DateOnly Birthday { get; set; }

		[ViewColumn("Телефон", false, false)]
		public string Contact { get; set; } = null!;

		[ViewColumn("Пол", false, false)]
		public string Gender { get; set; } = null!;
		//TODO: enum

		[ViewColumn("Паспорт", false, false)]
		public string Document { get; set; } = null!;

		[ViewColumn("Должность", false, false)]
		public virtual Post Post { get; set; } = null!;
        public virtual ICollection<Maintenance> Maintenances { get; set; }
        public virtual ICollection<Timetable> Timetables { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
