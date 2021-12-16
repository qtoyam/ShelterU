using System;
using System.Collections.Generic;

namespace Maintance.DbModels
{
    public partial class Workschedule
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string DayOfWeek { get; set; } = null!;
        public TimeOnly Beginning { get; set; }
        public TimeOnly Ending { get; set; }
        public bool? EvenWeek { get; set; }
        public bool? OddWeek { get; set; }
    }
}
