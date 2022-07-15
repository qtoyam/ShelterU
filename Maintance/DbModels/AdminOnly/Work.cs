using System;
using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;

namespace Maintance.DbModels
{
	[TableInfo("Рабочее время")]
    public partial class Work :  IDBModelAdmin
	{
        public Work()
        {
            Timetables = new HashSet<Timetable>();
        }
		
		[PropertyInfo(displayName: "ID", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: false)]
        public int TimetId { get; set; }
		
		[PropertyInfo(displayName: "День недели", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public WeekDay DayOfWeek { get; set; }

		private TimeOnly _beginning;
		[PropertyInfo(displayName: "Начало работы", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public TimeOnly Beginning
		{
			get => _beginning;
			set
			{
				_beginning = value;
				UpdateHours();
			}
		}

		private TimeOnly _ending;
		[PropertyInfo(displayName: "Завершение работы", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public TimeOnly Ending
        {
			get => _ending;
            set
            {
				_ending = value;
				UpdateHours();
            }
        }
		
		private void UpdateHours()
        {
			if (Ending > Beginning)
			{
				var diff = Ending - Beginning;
				if (diff.Hours > 0) Hours = (sbyte?)diff.Hours;
			}
			else Hours = null;
		}

		[PropertyInfo(displayName: "Рабочих часов", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: false)]
        public sbyte? Hours { get; set; }

        public virtual ICollection<Timetable> Timetables { get; set; }

		public override string ToString() => TimetId.ToString();

	}
}
