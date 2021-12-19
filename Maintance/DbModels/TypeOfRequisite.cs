using System;
using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Тип продукта")]
	public partial class TypeOfRequisite : IDBModel
	{
        public TypeOfRequisite()
        {
            Provisions = new HashSet<Provision>();
        }

		[PropertyInfo(displayName: "", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public int TypeReqId { get; set; }

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Name { get; set; } = null!;

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Genus Genus { get; set; } = null!;

		#region Hidden props
		public virtual ICollection<Provision> Provisions { get; set; }
        public int GenusId { get; set; }
		#endregion //Hidden props
	}
}
