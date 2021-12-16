using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maintance.ExViews;
using Maintance.DbModels;
using Maintance.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace Maintance.Services
{
	public class SelectorLocator
	{
		private readonly IServiceProvider _isp;

		public SelectorLocator(IServiceProvider isp)
		{
			_isp = isp;
		}

		public Genus? TrySelectGenus()
		{
			var s = _isp.GetRequiredService<GenusSelectorWindow>();
			s.ShowDialog();
			return ((GenusSelectorVM)s.DataContext).Selected;
		}
	}
}
