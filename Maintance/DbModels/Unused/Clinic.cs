using System.Collections.Generic;

namespace Maintance.DbModels
{
	public partial class Clinic
    {
        public Clinic()
        {
            Treatments = new HashSet<Treatment>();
        }

        public int ClinicId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string? Specialization { get; set; }
        public string? TimeOfWorking { get; set; }

        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
