using System;

namespace Maintance.DbModels
{
	public partial class AnimalMovement
    {
        public int AnimalMovementId { get; set; }
        public int AnimalId { get; set; }
        public int VisitorId { get; set; }
        public AnimalMovementStatus Status { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; } = null!;
        public string ConditionOfAn { get; set; } = null!;
        public string? Recomendations { get; set; }
        public string? Situation { get; set; }

        public virtual Animal Animal { get; set; } = null!;
        public virtual Visitor Visitor { get; set; } = null!;
    }
}
