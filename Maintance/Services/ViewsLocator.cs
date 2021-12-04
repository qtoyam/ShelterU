using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maintance.ViewModels;
using MvvmGen;
using Microsoft.Extensions.DependencyInjection;

namespace Maintance.Services
{
	public class ViewsLocator
	{
		private readonly IServiceProvider _isp;

		public ViewsLocator(IServiceProvider isp)
		{
			_isp = isp;
		}

		public MvvmGen.ViewModels.ViewModelBase GetVM(Type vmType) => (MvvmGen.ViewModels.ViewModelBase)_isp.GetRequiredService(vmType);
	}
}
