using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;

namespace Maintance.DbModels
{
	[TableInfo("Ветеринарные клиники")]
	public partial class Clinic : IDBModelAdmin
    {
        public Clinic()
        {
            Treatments = new HashSet<Treatment>();
        }

		[PropertyInfo(displayName: "ID", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public int ClinicId { get; set; }
		
		[PropertyInfo(displayName: "Название", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public string Name { get; set; } = null!;
		
		[PropertyInfo(displayName: "Адрес", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
        public string Address { get; set; } = null!;
		
		[PropertyInfo(displayName: "Контактный телефон", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
        public string Contact { get; set; } = null!;
		
		[PropertyInfo(displayName: "Специализация", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public string? Specialization { get; set; }
		
		[PropertyInfo(displayName: "График работы", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: false)]
        public string? TimeOfWorking { get; set; }

        public virtual ICollection<Treatment> Treatments { get; set; }

		public override string ToString() => Name;

	}
}
