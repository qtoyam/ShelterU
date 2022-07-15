using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Должности", 7)]
	public partial class Post : IDBModelGeneral
	{
		public Post()
		{
			Employees = new HashSet<Employee>();
		}


		[PropertyInfo(displayName: "ID", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public int PostId { get; set; }

		[PropertyInfo(displayName: "Должность", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Name { get; set; } = null!;

		[PropertyInfo(displayName: "Зарплата", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: false)]
		public decimal? Salary { get; set; }

		[PropertyInfo(displayName: "Уровень должности", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public sbyte PositionLevel { get; set; }

		public virtual ICollection<Employee> Employees { get; set; }

		public override string ToString() => Name;

	}
}
