using System;
using System.Collections.Generic;

namespace Maintance.DbModels
{
	public partial class Visitor
    {
        public Visitor()
        {
            AnimalMovements = new HashSet<AnimalMovement>();
            Visits = new HashSet<Visit>();
        }

        public int VisitorId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public DateOnly? Birthday { get; set; }
        public VisitorStatus Status { get; set; }

        public virtual ICollection<AnimalMovement> AnimalMovements { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
