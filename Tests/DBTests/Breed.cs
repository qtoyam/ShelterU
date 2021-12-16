using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTests
{
#nullable disable
	[Table("breed")]
	internal class Breed
	{
		[Column("breed_ID")]
		[Key]
		public int ID { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[ForeignKey("genus_ID")]
		public Genus Genus { get; set; }

		[Column("gender")]
		public Gtype Gender { get; set; }

		[Column("description", TypeName = "VARCHAR(1)")]
		public string Description { get; set; }
	}

	public enum Sex
	{
		М,
		Ж
	}

	public enum Gtype
	{

	}
}
