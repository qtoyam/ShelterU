using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using Maintance.Models;

using MvvmGen;

namespace Maintance.ViewModels
{
	[ViewModel]
	public partial class AnimalsViewModel
	{
		public ObservableCollection<Animal> Animals { get; } = new();
#if DEBUG
		partial void OnInitialize()
		{
			Animals.Add(new() { Name = "Abobik", Type = "Dog" });
			Animals.Add(new() { Name = "Vladik", Type = "Dog" });
			Animals.Add(new() { Name = "Lagunova", Type = "Dog" });
			Animals.Add(new() { Name = "Pavlovich", Type = "Dog" });
			Animals.Add(new() { Name = "Pidorasi", Type = "Dog" });

			Animals.Add(new() { Name = "Leon", Type = "Cat" });
			Animals.Add(new() { Name = "Noel", Type = "Cat" });
			Animals.Add(new() { Name = "Eoln", Type = "Cat" });
			Animals.Add(new() { Name = "Orsbnhy", Type = "Cat" });
			Animals.Add(new() { Name = "Sqerbbv", Type = "Cat" });
			Animals.Add(new() { Name = "Sqerbbv", Type = "Cat" });
			Animals.Add(new() { Name = "Sqerbbv", Type = "Cat" }); Animals.Add(new() { Name = "Sqerbbv", Type = "Cat" }); Animals.Add(new() { Name = "Sqerbbv", Type = "Cat" }); Animals.Add(new() { Name = "Sqerbbv", Type = "Cat" }); Animals.Add(new() { Name = "Sqerbbv", Type = "Cat" }); Animals.Add(new() { Name = "Sqerbbv", Type = "Cat" }); Animals.Add(new() { Name = "Sqerbbv", Type = "Cat" }); Animals.Add(new() { Name = "Sqerbbv", Type = "Cat" }); Animals.Add(new() { Name = "Sqerbbv", Type = "Cat" });


		}
#endif

		public void GroupBy(string name)
		{
			var iv = CollectionViewSource.GetDefaultView(Animals);
			iv.GroupDescriptions.Clear();
			iv.GroupDescriptions.Add(new PropertyGroupDescription(name));
		}

		public void NoGroupBy()
		{
			var iv = CollectionViewSource.GetDefaultView(Animals);
			iv.GroupDescriptions.Clear();
		}
	}
}
