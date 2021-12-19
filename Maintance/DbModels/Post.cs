using System.Collections.Generic;

using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
	public partial class Post
    {
        public Post()
        {
            Employees = new HashSet<Employee>();
        }


		[ViewColumn("Id", false, false)]
		public int PostId { get; set; }

		[ViewColumn("Название", false, false)]
		public string Name { get; set; } = null!;

		[ViewColumn("Зарплата", false, false)]
		public decimal? Salary { get; set; }

		[ViewColumn("Уровень", false, false)]
		public sbyte PositionLevel { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
