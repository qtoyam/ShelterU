using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using Maintance.Helpers;
using Maintance.DbModels;
using Maintance.Services;

using MvvmGen;

namespace Maintance.ViewModels
{
	[Inject(typeof(ShelterContext))]
	[ViewModel]
	public partial class AnimalsViewModel
	{
		public ObservableCollection<Animal> Animals { get; private set; }
		public ICollectionView AnimalsView { get; private set; }

		#region Grouping
		[PropertyCallMethod(nameof(HandleGroupIndex), MethodArgs = "value")]
		[Property] private int _currentGroupPropIndex = -1;
		private void HandleGroupIndex(int value)
		{
			value--; //cauze first is string.Empty
			var gd = AnimalsView.GroupDescriptions;
			gd.Clear();
			if (value >= 0)
			{
				gd.Add(_groupPropDescrs[value]);
			}
		}

		public IReadOnlyList<string> GroupProps { get; private set; }
		private IReadOnlyList<PropertyGroupDescription> _groupPropDescrs;
		#endregion //Grouping

		#region Filtering
		[Property] private int _currentFilterPropIndex = -1;

		[PropertyCallMethod(nameof(HandleFilter), MethodArgs = "value")]
		[Property] private string _currentFilter;

		private void HandleFilter(string filter)
		{
			//TODO: handle filter
			throw new NotImplementedException();
		}

		public IReadOnlyList<string> FilterProps { get; private set; }
		private IReadOnlyList<string> _filterPropNames;
		#endregion //Filtering

		[Property] private int _currentAnimalIndex = -1;


		[Command]
		private void Delete()
		{

		}

		[Command]
		private void EditCurrent()
		{
			EditItem = Animals[CurrentAnimalIndex];
			IsEditing = true;
		}

		partial void OnInitialize()
		{
			#region grouping,filtering, etc..
			_groupPropDescrs = AttributeHelper.GetGroupingProperties(typeof(Animal), out var descrs);
			GroupProps = descrs;
			_filterPropNames = AttributeHelper.GetFilteringProperties(typeof(Animal), out descrs);
			FilterProps = descrs;
			#endregion //grouping,filtering, etc..
			var an = DBService.GetAnimals().ToList();
			Animals = new(an);
			AnimalsView = CollectionViewSource.GetDefaultView(Animals);

			#region DEBUG
#if DEBUG
			//var br = "Кот1";
			//Animals.Add(new() { Name = "An1", BreedName = br});
			//Animals.Add(new() { Name = "An2", BreedName = br });
			//Animals.Add(new() { Name = "Brg", BreedName = br });
			//Animals.Add(new() { Name = "Bgdf", BreedName = br });
			//Animals.Add(new() { Name = "QWebb", BreedName = br });
			//Animals.Add(new() { Name = "sdSD", BreedName = br });
			//Animals.Add(new() { Name = "sadSD", BreedName = br });
			//Animals.Add(new() { Name = "Eoln", BreedName = br });
			//br = "Собака2";
			//Animals.Add(new() { Name = "Orsbnhy", BreedName = br });
			//Animals.Add(new() { Name = "Sqerbbv", BreedName = br });
			//Animals.Add(new() { Name = "Pfs", BreedName = br });
			//Animals.Add(new() { Name = "Bjhg", BreedName = br });
			//Animals.Add(new() { Name = "Sqwqe", BreedName = br });
			//Animals.Add(new() { Name = "bhj", BreedName = br });
			//Animals.Add(new() { Name = "zz", BreedName = br });
			//Animals.Add(new() { Name = "Пав", BreedName = br });
			//Animals.Add(new() { Name = "Вцук", BreedName = br });
			//Animals.Add(new() { Name = "Пимм", BreedName = br });
			//Animals.Add(new() { Name = "Акуа", BreedName = br });
			//Animals.Add(new() { Name = "ААА", BreedName = br });
#endif
			#endregion //DEBUG
		}

		[Property] private bool _isEditing = false;

		public ObservableCollection<string> Breeds
		{
			get
			{
				return new() { "Br1", "br2", "br3" };
			}
		} 

		[Command]
		private void CreateAnimal()
		{
			//TODO: bd create
			IsEditing = false;
		}

		[Command]
		private void ToggleEditing()
		{
			if (!IsEditing)
			{
				EditItem = new();
			}
			else
			{
				EditItem = null;
			}
			IsEditing = !IsEditing;
		}

		[Property] private Animal _editItem;
	}
}
