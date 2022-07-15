using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

using System;

namespace Maintance.DbModels
{
	[TableInfo("Клетки")]
	public partial class Cage : IDBModelAnimalManager
	{

		[PropertyInfo(displayName: "Номер", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public int NumberOfCage { get; set; }

		[PropertyInfo(displayName: "Площадь", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public decimal? Area { get; set; }

		[PropertyInfo(displayName: "Животное", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Animal? Animal { get; set; }

		[PropertyInfo(displayName: "Род животного", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Genus Genus { get; set; } = null!;

		#region Hidden props
        public int? AnimalId { get; set; }
        public int GenusId { get; set; }
		#endregion //Hidden props

		public override string ToString() => NumberOfCage.ToString();
	}
}
