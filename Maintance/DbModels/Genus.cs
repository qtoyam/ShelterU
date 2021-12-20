using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Роды)")]
	public partial class Genus : IDBModel
	{
        public Genus()
        {
            Breeds = new HashSet<Breed>();
            Cages = new HashSet<Cage>();
            Typeofrequisites = new HashSet<TypeOfRequisite>();
        }

		[PropertyInfo(displayName: "", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public int GenusId { get; set; }

		[PropertyInfo(displayName: "", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Name { get; set; } = null!;

        public virtual ICollection<Breed> Breeds { get; set; }
        public virtual ICollection<Cage> Cages { get; set; }
        public virtual ICollection<TypeOfRequisite> Typeofrequisites { get; set; }

		public override string ToString() => Name;
	}
}
