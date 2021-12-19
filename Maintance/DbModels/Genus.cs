using System.Collections.Generic;

using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
    public partial class Genus
    {
        public Genus()
        {
            Breeds = new HashSet<Breed>();
            Cages = new HashSet<Cage>();
            Typeofrequisites = new HashSet<TypeOfRequisite>();
        }

		[ViewColumn("Id", false, false)]
		public int GenusId { get; set; }

		[ViewColumn("Название", false, false)]
		public string Name { get; set; } = null!;

        public virtual ICollection<Breed> Breeds { get; set; }
        public virtual ICollection<Cage> Cages { get; set; }
        public virtual ICollection<TypeOfRequisite> Typeofrequisites { get; set; }
    }
}
