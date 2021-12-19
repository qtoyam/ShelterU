using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTests
{
	[Table("Genus")]
	public class Genus
	{
		[Column("genus_ID")]
		public int ID { get; set; }
		[Column("name")]
		public string? Name { get; set; }

		public override string ToString() => $"[{ID}] {Name}";
	}
}
