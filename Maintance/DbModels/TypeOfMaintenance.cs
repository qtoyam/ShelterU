using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Тип кто?")]
	public partial class TypeOfMaintenance : IDBModel
	{
        public TypeOfMaintenance()
        {
            Maintenances = new HashSet<Maintenance>();
        }

		[PropertyInfo(displayName: "", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public int TypeMainId { get; set; }

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Name { get; set; } = null!;

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string? Frequency { get; set; }

		#region Hidden props
		public virtual ICollection<Maintenance> Maintenances { get; set; }
		#endregion //Hidden props
	}
}
