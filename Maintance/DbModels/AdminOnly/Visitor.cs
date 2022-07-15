using System;
using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;

namespace Maintance.DbModels
{
	[TableInfo("Посетители")]
	public partial class Visitor : IDBModelAdmin
	{
        public Visitor()
        {
            AnimalMovements = new HashSet<AnimalMovement>();
            Visits = new HashSet<Visit>();
        }

		[PropertyInfo(displayName: "ID", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
        public int VisitorId { get; set; }
		
		[PropertyInfo(displayName: "Фамилия", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public string LastName { get; set; } = null!;
		
		[PropertyInfo(displayName: "Имя", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public string FirstName { get; set; } = null!;
		
		[PropertyInfo(displayName: "Телефон", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
        public string Contact { get; set; } = null!;
		
		[PropertyInfo(displayName: "Дата рождения", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: false)]
        public DateOnly? Birthday { get; set; }
		
		[PropertyInfo(displayName: "Статус", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public VisitorStatus Status { get; set; }

        public virtual ICollection<AnimalMovement> AnimalMovements { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }

		public override string ToString() => $"{LastName} {FirstName[0]}.";

	}
}
