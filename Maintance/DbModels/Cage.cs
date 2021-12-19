using Maintance.TableAutomation.Models;

using System;

namespace Maintance.DbModels
{
	public partial class Cage
    {

		[ViewColumn("Id", false, false)]
		public int NumberOfCage { get; set; }
        public int? AnimalId { get; set; }
        public int GenusId { get; set; }

		[ViewColumn("Площадь", false, false)]
		public decimal? Area { get; set; }

		[ViewColumn("Животное", false, false)]
		public virtual Animal? Animal { get; set; }

		[ViewColumn("Порода", false, false)]
		public virtual Genus Genus { get; set; } = null!;
    }
}
