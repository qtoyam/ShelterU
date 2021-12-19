using System;

using Microsoft.EntityFrameworkCore;

namespace Maintance.DbModels
{
	public partial class ShelterContext : DbContext
	{
		public ShelterContext() : base()
		{

		}

		public ShelterContext(DbContextOptions<ShelterContext> options)
			: base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(@"server=localhost;database=shelter;uid=root;pwd=zxc123;",
				new MySqlServerVersion(new Version(8, 0, 27)));
		}

		public virtual DbSet<Animal> Animals { get; set; } = null!;
		public virtual DbSet<AnimalMovement> AnimalMovements { get; set; } = null!;
		public virtual DbSet<Breed> Breeds { get; set; } = null!;
		public virtual DbSet<Cage> Cages { get; set; } = null!;
		public virtual DbSet<Clinic> Clinics { get; set; } = null!;
		public virtual DbSet<Employee> Employees { get; set; } = null!;
		public virtual DbSet<Genus> Genuses { get; set; } = null!;
		public virtual DbSet<Maintenance> Maintenances { get; set; } = null!;
		public virtual DbSet<Post> Posts { get; set; } = null!;
		public virtual DbSet<Provision> Provisions { get; set; } = null!;
		public virtual DbSet<Timetable> Timetables { get; set; } = null!;
		public virtual DbSet<Treatment> Treatments { get; set; } = null!;
		public virtual DbSet<TypeOfMaintenance> Typeofmaintenances { get; set; } = null!;
		public virtual DbSet<TypeOfRequisite> Typeofrequisites { get; set; } = null!;
		public virtual DbSet<Visit> Visits { get; set; } = null!;
		public virtual DbSet<Visitor> Visitors { get; set; } = null!;
		public virtual DbSet<Work> Works { get; set; } = null!;
		public virtual DbSet<Workschedule> Workschedules { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
				.HasCharSet("utf8mb4");

			modelBuilder.Entity<Animal>(entity =>
			{
				entity.ToTable("animal");

				entity.HasIndex(e => e.BreedId, "breed_ID");

				entity.Property(e => e.AnimalId).HasColumnName("animal_ID");

				entity.Property(e => e.Age).HasColumnName("age");

				entity.Property(e => e.Birthday).HasColumnName("birthday");

				entity.Property(e => e.BreedId).HasColumnName("breed_ID");

				entity.Property(e => e.GettingIntoShelter).HasColumnName("getting_into_shelter");

				entity.Property(e => e.Height)
					.HasPrecision(4, 2)
					.HasColumnName("height");

				entity.Property(e => e.Name)
					.HasMaxLength(255)
					.HasColumnName("name");

				entity.Property(e => e.Weight)
					.HasPrecision(5, 2)
					.HasColumnName("weight");

				entity.HasOne(d => d.Breed)
					.WithMany(p => p.Animals)
					.HasForeignKey(d => d.BreedId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("animal_ibfk_1");

				//mine
				entity.Navigation(e => e.Breed).AutoInclude();
			});

			modelBuilder.Entity<AnimalMovement>(entity =>
			{
				entity.ToTable("animal_movement");

				entity.HasIndex(e => e.AnimalId, "animal_movement_ibfk_1");

				entity.HasIndex(e => e.VisitorId, "visitor_ID");

				entity.Property(e => e.AnimalMovementId)
					.ValueGeneratedNever()
					.HasColumnName("animal_movement_id");

				entity.Property(e => e.Address)
					.HasMaxLength(255)
					.HasColumnName("address");

				entity.Property(e => e.AnimalId).HasColumnName("animal_ID");

				entity.Property(e => e.ConditionOfAn)
					.HasColumnType("text")
					.HasColumnName("condition_of_an");

				entity.Property(e => e.Date)
					.HasColumnType("datetime")
					.HasColumnName("date");

				entity.Property(e => e.Recomendations)
					.HasColumnType("text")
					.HasColumnName("recomendations");

				entity.Property(e => e.Situation)
					.HasColumnType("text")
					.HasColumnName("situation");

				entity.Property(e => e.Status)
					.HasColumnType("enum('Found','GivenAway','Temporary')")
					.HasColumnName("status");

				entity.Property(e => e.VisitorId).HasColumnName("visitor_ID");

				entity.HasOne(d => d.Animal)
					.WithMany(p => p.AnimalMovements)
					.HasForeignKey(d => d.AnimalId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("animal_movement_ibfk_1");

				entity.HasOne(d => d.Visitor)
					.WithMany(p => p.AnimalMovements)
					.HasForeignKey(d => d.VisitorId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("animal_movement_ibfk_2");
			});

			modelBuilder.Entity<Breed>(entity =>
			{
				entity.ToTable("breed");

				entity.HasIndex(e => e.GenusId, "fk_breed_Genus1_idx");

				entity.Property(e => e.BreedId).HasColumnName("breed_ID");

				entity.Property(e => e.Description)
					.HasColumnType("text")
					.HasColumnName("description");

				entity.Property(e => e.Gender)
					.HasColumnType("enum('Male','Female')")
					.HasColumnName("gender");

				entity.Property(e => e.GenusId).HasColumnName("genus_ID");

				entity.Property(e => e.Name)
					.HasMaxLength(255)
					.HasColumnName("name");

				entity.HasOne(d => d.Genus)
					.WithMany(p => p.Breeds)
					.HasForeignKey(d => d.GenusId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_breed_Genus1");
			});

			modelBuilder.Entity<Cage>(entity =>
			{
				entity.HasKey(e => e.NumberOfCage)
					.HasName("PRIMARY");

				entity.ToTable("cage");

				entity.HasIndex(e => e.GenusId, "fk_Cage_Genus1_idx");

				entity.HasIndex(e => e.AnimalId, "fk_Cage_animal1_idx");

				entity.Property(e => e.NumberOfCage).HasColumnName("number_of_cage");

				entity.Property(e => e.AnimalId).HasColumnName("animal_ID");

				entity.Property(e => e.Area)
					.HasPrecision(10)
					.HasColumnName("area");

				entity.Property(e => e.GenusId).HasColumnName("genus_ID");

				entity.HasOne(d => d.Animal)
					.WithMany(p => p.Cages)
					.HasForeignKey(d => d.AnimalId)
					.HasConstraintName("fk_Cage_animal1");

				entity.HasOne(d => d.Genus)
					.WithMany(p => p.Cages)
					.HasForeignKey(d => d.GenusId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_Cage_Genus1");
			});

			modelBuilder.Entity<Clinic>(entity =>
			{
				entity.ToTable("clinic");

				entity.Property(e => e.ClinicId).HasColumnName("clinic_ID");

				entity.Property(e => e.Address)
					.HasMaxLength(255)
					.HasColumnName("address");

				entity.Property(e => e.Contact)
					.HasMaxLength(12)
					.HasColumnName("contact")
					.IsFixedLength();

				entity.Property(e => e.Name)
					.HasMaxLength(255)
					.HasColumnName("name");

				entity.Property(e => e.Specialization)
					.HasMaxLength(255)
					.HasColumnName("specialization");

				entity.Property(e => e.TimeOfWorking)
					.HasMaxLength(255)
					.HasColumnName("time_of_working");
			});

			modelBuilder.Entity<Employee>(entity =>
			{
				entity.ToTable("employee");

				entity.HasIndex(e => e.Document, "document_UNIQUE")
					.IsUnique();

				entity.HasIndex(e => e.PostId, "post_ID");

				entity.Property(e => e.EmployeeId).HasColumnName("employee_ID");

				entity.Property(e => e.Birthday).HasColumnName("birthday");

				entity.Property(e => e.Contact)
					.HasMaxLength(15)
					.HasColumnName("contact")
					.IsFixedLength();

				entity.Property(e => e.Document)
					.HasMaxLength(10)
					.HasColumnName("document")
					.IsFixedLength();

				entity.Property(e => e.FirstName)
					.HasMaxLength(255)
					.HasColumnName("first_name");

				entity.Property(e => e.Gender)
					.HasColumnType("enum('Male','Female')")
					.HasColumnName("gender");

				entity.Property(e => e.LastName)
					.HasMaxLength(255)
					.HasColumnName("last_name");

				entity.Property(e => e.MiddleName)
					.HasMaxLength(255)
					.HasColumnName("middle_name");

				entity.Property(e => e.PostId).HasColumnName("post_ID");

				entity.Property(e => e.Seniority).HasColumnName("seniority");

				entity.HasOne(d => d.Post)
					.WithMany(p => p.Employees)
					.HasForeignKey(d => d.PostId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("employee_ibfk_1");
			});

			modelBuilder.Entity<Genus>(entity =>
			{
				entity.HasKey(e => e.GenusId)
					.HasName("PRIMARY");

				entity.ToTable("genus");

				entity.HasIndex(e => e.Name, "name_UNIQUE")
					.IsUnique();

				entity.Property(e => e.GenusId).HasColumnName("genus_ID");

				entity.Property(e => e.Name)
					.HasMaxLength(63)
					.HasColumnName("name");
			});

			modelBuilder.Entity<Maintenance>(entity =>
			{
				entity.ToTable("maintenance");

				entity.HasIndex(e => e.AnimalId, "fk_maintenance_animal1");

				entity.HasIndex(e => e.EmployeeId, "fk_maintenance_employee1_idx");

				entity.HasIndex(e => e.RequisiteId, "fk_maintenance_provision1_idx");

				entity.HasIndex(e => e.TypeMainId, "fk_maintenance_typeofmaintenance1_idx");

				entity.Property(e => e.MaintenanceId)
					.ValueGeneratedNever()
					.HasColumnName("maintenance_ID");

				entity.Property(e => e.AnimalId).HasColumnName("animal_ID");

				entity.Property(e => e.Comment)
					.HasColumnType("text")
					.HasColumnName("comment");

				entity.Property(e => e.DateOfAction).HasColumnName("date_of_action");

				entity.Property(e => e.EmployeeId).HasColumnName("employee_ID");

				entity.Property(e => e.Problem)
					.HasColumnName("problem")
					.HasDefaultValueSql("'0'");

				entity.Property(e => e.RequisiteId).HasColumnName("requisite_ID");

				entity.Property(e => e.TimeOfAction)
					.HasColumnType("time")
					.HasColumnName("time_of_action");

				entity.Property(e => e.TypeMainId).HasColumnName("typeMain_ID");

				entity.Property(e => e.WasteOfResource).HasColumnName("waste_of_resource");

				entity.HasOne(d => d.Animal)
					.WithMany(p => p.Maintenances)
					.HasForeignKey(d => d.AnimalId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_maintenance_animal1");

				entity.HasOne(d => d.Employee)
					.WithMany(p => p.Maintenances)
					.HasForeignKey(d => d.EmployeeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_maintenance_employee1");

				entity.HasOne(d => d.Requisite)
					.WithMany(p => p.Maintenances)
					.HasForeignKey(d => d.RequisiteId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_maintenance_provision1");

				entity.HasOne(d => d.TypeMain)
					.WithMany(p => p.Maintenances)
					.HasForeignKey(d => d.TypeMainId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_maintenance_typeofmaintenance1");
			});

			modelBuilder.Entity<Post>(entity =>
			{
				entity.ToTable("post");

				entity.HasIndex(e => e.Name, "name_UNIQUE")
					.IsUnique();

				entity.Property(e => e.PostId).HasColumnName("post_ID");

				entity.Property(e => e.Name).HasColumnName("name");

				entity.Property(e => e.PositionLevel).HasColumnName("position_level");

				entity.Property(e => e.Salary)
					.HasPrecision(8, 2)
					.HasColumnName("salary");
			});

			modelBuilder.Entity<Provision>(entity =>
			{
				entity.HasKey(e => e.RequisiteId)
					.HasName("PRIMARY");

				entity.ToTable("provision");

				entity.HasIndex(e => e.TypeReqId, "fk_provision_typeofrequisite1_idx");

				entity.Property(e => e.RequisiteId).HasColumnName("requisite_ID");

				entity.Property(e => e.Description)
					.HasColumnType("text")
					.HasColumnName("description");

				entity.Property(e => e.ForWhichAnimals)
					.HasMaxLength(127)
					.HasColumnName("for_which_animals");

				entity.Property(e => e.Name)
					.HasMaxLength(255)
					.HasColumnName("name");

				entity.Property(e => e.NeedToBuy).HasColumnName("need_to_buy");

				entity.Property(e => e.Producer)
					.HasMaxLength(80)
					.HasColumnName("producer");

				entity.Property(e => e.Quantity).HasColumnName("quantity");

				entity.Property(e => e.TypeReqId).HasColumnName("typeReq_ID");

				entity.Property(e => e.ValidUntil).HasColumnName("valid_until");

				entity.HasOne(d => d.TypeReq)
					.WithMany(p => p.Provisions)
					.HasForeignKey(d => d.TypeReqId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_provision_typeofrequisite1");
			});

			modelBuilder.Entity<Timetable>(entity =>
			{
				entity.HasKey(e => new { e.EmployeeId, e.TimetId })
					.HasName("PRIMARY")
					.HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

				entity.ToTable("timetable");

				entity.HasIndex(e => e.TimetId, "timet_ID");

				entity.Property(e => e.EmployeeId).HasColumnName("employee_ID");

				entity.Property(e => e.TimetId).HasColumnName("timet_ID");

				entity.Property(e => e.EvenWeek)
					.HasColumnName("even_week")
					.HasDefaultValueSql("'1'");

				entity.Property(e => e.OddWeek)
					.HasColumnName("odd_week")
					.HasDefaultValueSql("'1'");

				entity.HasOne(d => d.Employee)
					.WithMany(p => p.Timetables)
					.HasForeignKey(d => d.EmployeeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("timetable_ibfk_1");

				entity.HasOne(d => d.Timet)
					.WithMany(p => p.Timetables)
					.HasForeignKey(d => d.TimetId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("timetable_ibfk_2");
			});

			modelBuilder.Entity<Treatment>(entity =>
			{
				entity.ToTable("treatment");

				entity.HasIndex(e => e.AnimalId, "animal_ID");

				entity.HasIndex(e => e.ClinicId, "treatment_ibfk_1");

				entity.Property(e => e.TreatmentId)
					.ValueGeneratedNever()
					.HasColumnName("treatment_ID");

				entity.Property(e => e.AnimalId).HasColumnName("animal_ID");

				entity.Property(e => e.ClinicId).HasColumnName("clinic_ID");

				entity.Property(e => e.DateOfEnd)
					.HasColumnType("datetime")
					.HasColumnName("date_of_end");

				entity.Property(e => e.DateOfStart)
					.HasColumnType("datetime")
					.HasColumnName("date_of_start");

				entity.Property(e => e.Goal)
					.HasMaxLength(100)
					.HasColumnName("goal");

				entity.Property(e => e.Problem)
					.HasColumnType("text")
					.HasColumnName("problem");

				entity.Property(e => e.Result)
					.HasColumnType("text")
					.HasColumnName("result");

				entity.HasOne(d => d.Animal)
					.WithMany(p => p.Treatments)
					.HasForeignKey(d => d.AnimalId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("treatment_ibfk_2");

				entity.HasOne(d => d.Clinic)
					.WithMany(p => p.Treatments)
					.HasForeignKey(d => d.ClinicId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("treatment_ibfk_1");
			});

			modelBuilder.Entity<TypeOfMaintenance>(entity =>
			{
				entity.HasKey(e => e.TypeMainId)
					.HasName("PRIMARY");

				entity.ToTable("typeofmaintenance");

				entity.HasIndex(e => e.Name, "name_UNIQUE")
					.IsUnique();

				entity.Property(e => e.TypeMainId).HasColumnName("typeMain_ID");

				entity.Property(e => e.Frequency)
					.HasColumnType("enum('OnNeed','Everyday','FewTimesWeek','OneTimeWeek','FewTimesMonth','OneTimeMonth')")
					.HasColumnName("frequency")
					.HasDefaultValueSql("'OnNeed'");

				entity.Property(e => e.Name)
					.HasMaxLength(127)
					.HasColumnName("name");
			});

			modelBuilder.Entity<TypeOfRequisite>(entity =>
			{
				entity.HasKey(e => e.TypeReqId)
					.HasName("PRIMARY");

				entity.ToTable("typeofrequisite");

				entity.HasIndex(e => e.GenusId, "genus_ID");

				entity.Property(e => e.TypeReqId).HasColumnName("typeReq_ID");

				entity.Property(e => e.GenusId).HasColumnName("genus_ID");

				entity.Property(e => e.Name)
					.HasMaxLength(150)
					.HasColumnName("name");

				entity.HasOne(d => d.Genus)
					.WithMany(p => p.Typeofrequisites)
					.HasForeignKey(d => d.GenusId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("typeofrequisite_ibfk_1");
			});

			modelBuilder.Entity<Visit>(entity =>
			{
				entity.HasKey(e => e.NumberOfVisit)
					.HasName("PRIMARY");

				entity.ToTable("visit");

				entity.HasIndex(e => e.ResponsEmployee, "respons_employee");

				entity.HasIndex(e => e.VisitorId, "visitor_ID");

				entity.Property(e => e.NumberOfVisit).HasColumnName("number_of_visit");

				entity.Property(e => e.DateOfVisit)
					.HasColumnType("datetime")
					.HasColumnName("date_of_visit");

				entity.Property(e => e.Goal)
					.HasColumnType("enum('Default','GiveAwayAnimal','TakeAnimal')")
					.HasColumnName("goal");

				entity.Property(e => e.ResponsEmployee).HasColumnName("respons_employee");

				entity.Property(e => e.Result)
					.HasColumnType("text")
					.HasColumnName("result");

				entity.Property(e => e.VisitorId).HasColumnName("visitor_ID");

				entity.HasOne(d => d.ResponsEmployeeNavigation)
					.WithMany(p => p.Visits)
					.HasForeignKey(d => d.ResponsEmployee)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("visit_ibfk_2");

				entity.HasOne(d => d.Visitor)
					.WithMany(p => p.Visits)
					.HasForeignKey(d => d.VisitorId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("visit_ibfk_1");
			});

			modelBuilder.Entity<Visitor>(entity =>
			{
				entity.ToTable("visitor");

				entity.Property(e => e.VisitorId).HasColumnName("visitor_ID");

				entity.Property(e => e.Birthday).HasColumnName("birthday");

				entity.Property(e => e.Contact)
					.HasMaxLength(15)
					.HasColumnName("contact")
					.IsFixedLength();

				entity.Property(e => e.FirstName)
					.HasMaxLength(255)
					.HasColumnName("first_name");

				entity.Property(e => e.LastName)
					.HasMaxLength(255)
					.HasColumnName("last_name");

				entity.Property(e => e.Status)
					.HasColumnType("enum('Default','LinkedWithAnimal')")
					.HasColumnName("status")
					.HasDefaultValueSql("'Default'");
			});

			modelBuilder.Entity<Work>(entity =>
			{
				entity.HasKey(e => e.TimetId)
					.HasName("PRIMARY");

				entity.ToTable("work");

				entity.Property(e => e.TimetId).HasColumnName("timet_ID");

				entity.Property(e => e.Beginning)
					.HasColumnType("time")
					.HasColumnName("beginning");

				entity.Property(e => e.DayOfWeek)
					.HasColumnType("enum('Monday','Tuesday','Wednesday','Thursday','Friday','Saturday','Sunday')")
					.HasColumnName("day_of_week");

				entity.Property(e => e.Ending)
					.HasColumnType("time")
					.HasColumnName("ending");

				entity.Property(e => e.Hours).HasColumnName("hours");
			});

			modelBuilder.Entity<Workschedule>(entity =>
			{
				entity.HasNoKey();

				entity.ToView("workschedule");

				entity.Property(e => e.Beginning)
					.HasColumnType("time")
					.HasColumnName("beginning");

				entity.Property(e => e.DayOfWeek)
					.HasColumnType("enum('Monday','Tuesday','Wednesday','Thursday','Friday','Saturday','Sunday')")
					.HasColumnName("day_of_week");

				entity.Property(e => e.EmployeeId).HasColumnName("employee_ID");

				entity.Property(e => e.Ending)
					.HasColumnType("time")
					.HasColumnName("ending");

				entity.Property(e => e.EvenWeek)
					.HasColumnName("even_week")
					.HasDefaultValueSql("'1'");

				entity.Property(e => e.FirstName)
					.HasMaxLength(255)
					.HasColumnName("first_name");

				entity.Property(e => e.LastName)
					.HasMaxLength(255)
					.HasColumnName("last_name");

				entity.Property(e => e.OddWeek)
					.HasColumnName("odd_week")
					.HasDefaultValueSql("'1'");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
