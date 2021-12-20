using System;
using System.Collections.Generic;

namespace Maintance.DbModels
{
    public partial class Visit
    {
        public int NumberOfVisit { get; set; }
        public int VisitorId { get; set; }
        public DateTime DateOfVisit { get; set; }
        public VisitGoal Goal { get; set; }
        public int ResponsEmployee { get; set; }
        public string? Result { get; set; }

        public virtual Employee ResponsEmployeeNavigation { get; set; } = null!;
        public virtual Visitor Visitor { get; set; } = null!;
    }
}
