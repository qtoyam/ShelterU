using System;
using System.Windows.Media;

namespace Maintance.Models
{
#nullable disable
	public class ViewInfo
	{
		public string Name { get; set; }
		public ImageSource Icon { get; set; }
		public Type ViewModelType { get; set; }
		public MvvmGen.ViewModels.ViewModelBase ViewModel { get; }
	}
}
