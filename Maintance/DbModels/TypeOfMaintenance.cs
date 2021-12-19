using System.Collections.Generic;

using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	public partial class TypeOfMaintenance
    {
        public TypeOfMaintenance()
        {
            Maintenances = new HashSet<Maintenance>();
        }

		[ViewColumn("Id", false, false)]
		public int TypeMainId { get; set; }

		[ViewColumn("Назв", false, false)]
		public string Name { get; set; } = null!;

		[ViewColumn("Частота исп", false, false)]
		public string? Frequency { get; set; }

        public virtual ICollection<Maintenance> Maintenances { get; set; }
    }
}
