using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Maintance.TableAutomation;
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

		[ViewColumn("Id", true, false, true)]
		public int AnimalId { get; set; }

		[ViewColumn("Кличка", true, false)]
		public string Name { get; set; } = null!;

		[ViewColumn("Возраст", true, true)]
		public sbyte? Age { get; set; }
		public int BreedId { get; set; }

		[ViewColumn("Дата рождения", true, false)]
		public DateOnly? Birthday { get; set; }

		[ViewColumn("Рост", true, false)]
		public decimal? Height { get; set; }

		[ViewColumn("Вес", true, false)]
		public decimal? Weight { get; set; }

		[ViewColumn("Дата прибытия", true, false)]
		public DateOnly GettingIntoShelter { get; set; } = DateOnly.FromDateTime(DateTime.Now);

		[ViewColumn("Порода", true, true)]
		public Breed Breed { get; set; } = null!;
		public virtual ICollection<AnimalMovement> AnimalMovements { get; set; }
		public virtual ICollection<Cage> Cages { get; set; }
		public virtual ICollection<Maintenance> Maintenances { get; set; }
		public virtual ICollection<Treatment> Treatments { get; set; }

		public override string ToString() => Name + "" + GettingIntoShelter.ToString();
	}
}
