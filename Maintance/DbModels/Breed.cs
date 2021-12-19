using System.Collections.Generic;

using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Породы")]
	public partial class Breed : IDBModel
    {
        public Breed()
        {
            Animals = new HashSet<Animal>();
        }

		[ViewColumn("Id", false, false, true, isSelectionVisible: true, isSelectionFilter: true)]
		public int BreedId { get; set; }

		[ViewColumn("Название", false, false)]
		public string Name { get; set; } = null!;
        public int GenusId { get; set; }

		[ViewColumn("Пол", false, false)]
		public string Gender { get; set; } = null!;
		//TODO: enum

		[ViewColumn("Описание", false, false)]
		public string? Description { get; set; }

		[ViewColumn("Род", false, false)]
		public virtual Genus Genus { get; set; } = null!;
        public virtual ICollection<Animal> Animals { get; set; }

		public override string ToString() => Name;
	}
}
