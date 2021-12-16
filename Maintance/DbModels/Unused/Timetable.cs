namespace Maintance.DbModels
{
	public partial class Timetable
    {
        public int EmployeeId { get; set; }
        public int TimetId { get; set; }
        public bool? EvenWeek { get; set; }
        public bool? OddWeek { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Work Timet { get; set; } = null!;
    }
}
