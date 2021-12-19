using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace DBTests
{
#nullable disable
	internal class ShelterContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(@"server=localhost;database=shelter;uid=root;pwd=zxc123;",
				new MySqlServerVersion(new Version(8, 0, 27)));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Breed>()
				.Navigation(e => e.Genus).AutoInclude();
		}



		public DbSet<Genus> Genuses { get; set; }

		public DbSet<Breed> Breeds { get; set; }
	}
}
