using System;
using System.Collections.Generic;
using Maintance.TableAutomation.DbModelAttributes;
using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	[TableInfo("Животные")]
	public partial class Animal : IDBModel
	{
		public Animal()
		{
			AnimalMovements = new HashSet<AnimalMovement>();
			Cages = new HashSet<Cage>();
			Maintenances = new HashSet<Maintenance>();
			Treatments = new HashSet<Treatment>();
		}

		[PropertyInfo(displayName: "Id", isAutofoFill: true, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public int AnimalId { get; set; }

		[PropertyInfo(displayName: "Кличка", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: false)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public string Name { get; set; } = null!;

		[PropertyInfo(displayName: "Возраст", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public sbyte? Age { get; set; }

		[PropertyInfo(displayName: "Дата рождения", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: true, isGroup: false)]
		public DateOnly? Birthday { get; set; }

		[PropertyInfo(displayName: "Рост", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: false)]
		public decimal? Height { get; set; }

		[PropertyInfo(displayName: "Вес", isAutofoFill: false, isOptional: true)]
		[ViewColumn(isFilter: false, isGroup: false)]
		public decimal? Weight { get; set; }

		[PropertyInfo(displayName: "Прибытие", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: false, isGroup: false)]
		public DateOnly GettingIntoShelter { get; set; } = DateOnly.FromDateTime(DateTime.Now);

		[PropertyInfo(displayName: "Порода", isAutofoFill: false, isOptional: false)]
		[ViewColumn(isFilter: true, isGroup: true)]
		[SelectionColumn(isVisible: true, isFilter: true)]
		public virtual Breed Breed
		{
			get => _breed;
			set
			{
				_breed = value;
				BreedId = _breed.BreedId;
			}
		}

		private Breed _breed=null!;

		public int BreedId { get; set; }
		public virtual ICollection<AnimalMovement> AnimalMovements { get; set; }
		public virtual ICollection<Cage> Cages { get; set; }
		public virtual ICollection<Maintenance> Maintenances { get; set; }
		public virtual ICollection<Treatment> Treatments { get; set; }

		public override string ToString() => Name;
	}
}
