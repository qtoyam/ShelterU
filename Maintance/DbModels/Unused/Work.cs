using System;
using System.Collections.Generic;

namespace Maintance.DbModels
{
    public partial class Work
    {
        public Work()
        {
            Timetables = new HashSet<Timetable>();
        }

        public int TimetId { get; set; }
        public WeekDay DayOfWeek { get; set; }
        public TimeOnly Beginning { get; set; }
        public TimeOnly Ending { get; set; }
        public sbyte? Hours { get; set; }

        public virtual ICollection<Timetable> Timetables { get; set; }
    }
}
