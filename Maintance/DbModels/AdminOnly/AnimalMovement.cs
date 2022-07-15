using System;

using Maintance.TableAutomation.DbModelAttributes;

namespace Maintance.DbModels
{
	[TableInfo("Движение животных")]
	public partial class AnimalMovement : IDBModelAdmin
    {
		[PropertyInfo(displayName: "Id", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: false)]
        public int AnimalMovementId { get; set; }
		
        public int AnimalId { get; set; }
		
        public int VisitorId { get; set; }
		
		[PropertyInfo(displayName: "Статус движения", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public AnimalMovementStatus Status { get; set; }
		
		[PropertyInfo(displayName: "Дата", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
        public DateTime Date { get; set; } = DateTime.Now;
		
		[PropertyInfo(displayName: "Адрес", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: false)]
        public string Address { get; set; } = null!;
		
		[PropertyInfo(displayName: "Состояние животного", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: false)]
        public string ConditionOfAn { get; set; } = null!;
		
		[PropertyInfo(displayName: "Рекомендации по уходу", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: false)]
        public string? Recomendations { get; set; }
		
		[PropertyInfo(displayName: "Ситуация", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: false)]
        public string? Situation { get; set; }


		[PropertyInfo(displayName: "Животное", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Animal Animal { get; set; } = null!;

		[PropertyInfo(displayName: "Посетитель", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Visitor Visitor { get; set; } = null!;

		public override string ToString() => AnimalMovementId.ToString();
	}
}
