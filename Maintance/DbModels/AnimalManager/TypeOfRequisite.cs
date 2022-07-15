using System;
using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Типы реквизитов", 5)]
	public partial class TypeOfRequisite : IDBModelAnimalManager
	{
		public TypeOfRequisite()
		{
			Provisions = new HashSet<Provision>();
		}

		[PropertyInfo(displayName: "ID", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		public int TypeReqId { get; set; }

		[PropertyInfo(displayName: "Тип реквизита", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Name { get; set; } = null!;

		[PropertyInfo(displayName: "Род животного", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Genus Genus { get; set; } = null!;

		#region Hidden props
		public virtual ICollection<Provision> Provisions { get; set; }
		public int GenusId { get; set; }
		#endregion //Hidden props

		public override string ToString() => Name;
	}
}
