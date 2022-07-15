using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Типы взаимодействия", 9)]
	public partial class TypeOfMaintenance : IDBModelAnimalManager
	{
		public TypeOfMaintenance()
		{
			Maintenances = new HashSet<Maintenance>();
		}

		[PropertyInfo(displayName: "ID", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		public int TypeMainId { get; set; }

		[PropertyInfo(displayName: "Тип взаимодействия", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Name { get; set; } = null!;

		[PropertyInfo(displayName: "Частота взаимодействия", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		public MaintanceFrequency? Frequency { get; set; }

		#region Hidden props
		public virtual ICollection<Maintenance> Maintenances { get; set; }
		#endregion //Hidden props

		public override string ToString() => Name;
	}
}
