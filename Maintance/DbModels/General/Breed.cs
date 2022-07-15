using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Породы", 1)]
	public partial class Breed : IDBModelGeneral
	{
        public Breed()
        {
            Animals = new HashSet<Animal>();
        }

		[PropertyInfo(displayName: "Id", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public int BreedId { get; set; }

		[PropertyInfo(displayName: "Порода", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Name { get; set; } = null!;


		[PropertyInfo(displayName: "Пол", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public Gender Gender { get; set; }

		[PropertyInfo(displayName: "Описание породы", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: false)]
		public string? Description { get; set; }

		[PropertyInfo(displayName: "Род животного", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Genus Genus { get; set; } = null!;

		#region Hidden props
		public int GenusId { get; set; }
        public virtual ICollection<Animal> Animals { get; set; }
		#endregion //Hidden props

		public override string ToString() => Name;
	}
}
