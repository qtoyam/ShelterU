using System;
using System.Collections.Generic;

using Maintance.DbModels.Attributes;

namespace Maintance.DbModels
{
	public partial class Animal
    {
        public Animal()
        {
            AnimalMovements = new HashSet<AnimalMovement>();
            Cages = new HashSet<Cage>();
            Maintenances = new HashSet<Maintenance>();
            Treatments = new HashSet<Treatment>();
        }

		[ViewColumn("Id", false, false)]
		public int AnimalId { get; set; }

		[ViewColumn("Кличка", false, false)]
		public string Name { get; set; } = null!;

		[ViewColumn("Возраст", false, false)]
		public sbyte? Age { get; set; }
        public int BreedId { get; set; }

		[ViewColumn("Дата рождения", false, false)]
		public DateOnly? Birthday { get; set; }

		[ViewColumn("Рост", false, false)]
		public decimal? Height { get; set; }

		[ViewColumn("Вес", false, false)]
		public decimal? Weight { get; set; }

		[ViewColumn("Дата прибытия", false, false)]
		public DateOnly GettingIntoShelter { get; set; }

		[ViewColumn("Порода", false, false)]
		public virtual Breed Breed { get; set; } = null!;
        public virtual ICollection<AnimalMovement> AnimalMovements { get; set; }
        public virtual ICollection<Cage> Cages { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }

		public override string ToString() => Name;
	}
}
