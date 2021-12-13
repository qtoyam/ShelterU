using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maintance.Models.Attributes;
namespace Maintance.Models
{
#nullable disable
	public class Breed : WPFCoreEx.Bases.NotifyPropBase
	{
		[FilterProperty]
		public int ID { get; set; }

		[FilterProperty]
		[Description("Название")]
		public string Name { get; set; }


		private string _genusName;
		[FilterProperty]
		[GroupProperty]
		[Description("Род")]
		public string GenusName
		{
			get => _genusName;
			set
			{
				if (_genusName != value)
				{
					_genusName = value;
					OnPropertyChanged();
				}
			}
		}

		public int GenusId;

		[GroupProperty]
		[Description("Пол")]
		public string Sex { get; set; }

		[FilterProperty]
		[Description("Описание")]
		public string Description { get; set; }
	}

	public enum Sex
	{
		Male, Female
	}
}
