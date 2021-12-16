﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maintance.DbModels;
using Maintance.Services;

using MvvmGen;

using WPFCoreEx.Bases;

namespace Maintance.ViewModels
{ 
	[ViewModel]
	[Inject(typeof(ShelterContext))]
	public partial class GenusSelectorVM
	{

		partial void OnInitialize()
		{
			
		}

		[Property] private string _name = "";

		[Command]
		private void AddGenus()
		{
			Genus g = new() { Name = this.Name, ID = -1 };
			DBService.AddGenus(g);
			Name = "";
			Genuses.Add(g);
		}

		[Property] private Genus? _selected;

		public ObservableCollection<Genus> Genuses { get; private set; }

		[Command]
		private void ClearSelection() => _selected = null;
	}
}
