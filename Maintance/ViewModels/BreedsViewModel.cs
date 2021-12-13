using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MvvmGen;

using Maintance.Helpers;

using Maintance.Models;
using MaterialDesignThemes.Wpf;
using Maintance.Services;

namespace Maintance.ViewModels
{
	[ViewModel]
	[Inject(typeof(DBService))]
	[Inject(typeof(SelectorLocator))]
	public partial class BreedsViewModel
	{
		public ObservableCollection<Breed> ViewColl { get; private set; }
		public ICollectionView View { get; private set; }

		#region Grouping
		[PropertyCallMethod(nameof(HandleGroupIndex), MethodArgs = "value")]
		[Property] private int _currentGroupPropIndex = -1;
		private void HandleGroupIndex(int value)
		{
			value--; //cauze first is string.Empty
			var gd = View.GroupDescriptions;
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

		[Property] private int _currentIndex = -1;

		[Property] private Breed _editItem;

		public ObservableCollection<string> Genuses
		{
			get
			{
				//TODO: from bd
				return new() { "Debil", "Joskii" };
			}
		}

		[Property] private bool _isEditing = false;

		[Command]
		private void ToggleEditing()
		{
			if (IsEditing)
			{
				EditItem = null;
				IsEditing = false;
			}
			else
			{
				EditItem = new();
				IsEditing = true;
			}
		}

		[Command]
		private void SelectGenus()
		{
			var t = SelectorLocator.TrySelectGenus();
			if (t != null)
			{
				EditItem.GenusName = t.Name;
				EditItem.GenusId = t.ID;
			}
		}

		[Command]
		private void Create()
		{
			DBService.AddBreed(EditItem);
			ViewColl.Add(EditItem);
			EditItem = null;
			IsEditing=false;
		}

		[Command(CanExecuteMethod = nameof(CanDelete))]
		private void Delete()
		{

		}
		[CommandInvalidate(nameof(CurrentIndex))]
		private bool CanDelete() => CurrentIndex >= 0;

		partial void OnInitialize()
		{
			#region grouping,filtering, etc..
			_groupPropDescrs = AttributeHelper.GetGroupingProperties(typeof(Breed), out var descrs);
			GroupProps = descrs;
			_filterPropNames = AttributeHelper.GetFilteringProperties(typeof(Breed), out descrs);
			FilterProps = descrs;

			#endregion //grouping,filtering, etc..
			ViewColl = new(DBService.GetBreeds());

			View = CollectionViewSource.GetDefaultView(ViewColl);

		}
	}
}
