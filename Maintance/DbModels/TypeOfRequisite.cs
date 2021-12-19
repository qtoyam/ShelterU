using System;
using System.Collections.Generic;

using Maintance.TableAutomation.Models;

namespace Maintance.DbModels
{
    public partial class TypeOfRequisite
    {
        public TypeOfRequisite()
        {
            Provisions = new HashSet<Provision>();
        }

		[ViewColumn("Id", false, false)]
		public int TypeReqId { get; set; }

		[ViewColumn("Название", false, false)]
		public string Name { get; set; } = null!;

		[ViewColumn("Для кого", false, false)]
		public virtual Genus Genus { get; set; } = null!;
        public virtual ICollection<Provision> Provisions { get; set; }
        public int GenusId { get; set; }
    }
}
