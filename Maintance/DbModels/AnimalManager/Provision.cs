using System;
using System.Collections.Generic;

using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Реквизиты для ухода", 4)]
	public partial class Provision : IDBModelAnimalManager
	{
		public Provision()
		{
			Maintenances = new HashSet<Maintenance>();
		}


		[PropertyInfo(displayName: "ID", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public int RequisiteId { get; set; }

		[PropertyInfo(displayName: "Название", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Name { get; set; } = null!;

		[PropertyInfo(displayName: "Текущее количество", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: false)]
		public int Quantity { get; set; }

		[PropertyInfo(displayName: "Производитель", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		public string? Producer { get; set; }

		[PropertyInfo(displayName: "Для каких животных", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string? ForWhichAnimals { get; set; }

		[PropertyInfo(displayName: "Годен до", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		public DateOnly? ValidUntil { get; set; }

		[PropertyInfo(displayName: "Описание", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		public string? Description { get; set; }

		[PropertyInfo(displayName: "Необходимо купить", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		public bool NeedToBuy { get; set; }

		[PropertyInfo(displayName: "Тип реквизита", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual TypeOfRequisite TypeReq { get; set; } = null!;


		public virtual ICollection<Maintenance> Maintenances { get; set; }
		public int TypeReqId { get; set; }

		public override string ToString() => Name;

	}
}
