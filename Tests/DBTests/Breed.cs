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
	public class Breed
	{
		[Column("breed_ID")]
		public int ID { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[ForeignKey("genus_ID")]
		public virtual Genus Genus { get; set; }

		[Column("gender")]
		public string Gender { get; set; }

		[Column("description")]
		public string Description { get; set; }
	}
}
