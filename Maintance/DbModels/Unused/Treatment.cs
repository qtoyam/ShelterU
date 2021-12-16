using System;

namespace Maintance.DbModels
{
	public partial class Treatment
    {
        public int TreatmentId { get; set; }
        public int ClinicId { get; set; }
        public int AnimalId { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public string Goal { get; set; } = null!;
        public string? Problem { get; set; }
        public string Result { get; set; } = null!;

        public virtual Animal Animal { get; set; } = null!;
        public virtual Clinic Clinic { get; set; } = null!;
    }
}
