using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Должность")]
	public partial class Post : IDBModel
	{
        public Post()
        {
            Employees = new HashSet<Employee>();
        }


		[PropertyInfo(displayName: "", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public int PostId { get; set; }

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Name { get; set; } = null!;

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public decimal? Salary { get; set; }

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public sbyte PositionLevel { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
