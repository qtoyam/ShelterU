using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

using Maintance.DbModels;
using Maintance.Services;
using Maintance.TableAutomation.Models;
using Maintance.TableAutomation.Tools;
using Maintance.TableAutomation.Views;

using Microsoft.EntityFrameworkCore;

using WPFCoreEx.Abstractions.Services;

namespace Maintance.TableAutomation
{
	public interface ITableManager : IDisposable
	{
		TableViewPage ViewPage { get; }

		bool TrySelectEntity(out object? res, Window owner);
		bool TryCreateEntity(Window owner);
		ICollectionView CreateCollectionView();
		IReadOnlyCollection<TableColumnInfo> TableColumnInfos { get; }
		Task<bool> SaveWorkingEntityToDB();
	}


	public sealed class TableManager<T> : ITableManager
		where T : class, new()
	{
		private readonly IDbContextFactory<ShelterContext> _dbContextFactory;
		private readonly IMessageService _ims;
		private readonly TableManagerSelector _tableManagerSelector;

		public IReadOnlyCollection<TableColumnInfo> TableColumnInfos { get; } =
			new ReadOnlyCollection<TableColumnInfo>(AutomationHelper.GetTableColumnInfos<T>().ToList());

		private readonly ObservableCollection<T> _currentEntities = new();

		public ICollectionView CreateCollectionView() => new CollectionViewSource() { Source = _currentEntities }.View;

		public TableManager(IDbContextFactory<ShelterContext> dbContextFactory, IMessageService ims, TableManagerSelector tableManagerSelector)
		{
			_dbContextFactory = dbContextFactory;
			_ims = ims;
			_tableManagerSelector = tableManagerSelector;
		}

		#region Table view page
		private TableViewPage? _viewPage;
		public TableViewPage ViewPage => _viewPage ??= CreateViewPage();

		private TableViewPage CreateViewPage()
		{
			TableViewPage res = new(this);
			res.AddBtn.Click += (s, e) =>
			{
				TryCreateEntity(Application.Current.MainWindow);
			};
			res.IsEnabled = false;
			Task.Run(async () =>
			{
				await using (var context = await _dbContextFactory.CreateDbContextAsync())
				{
					foreach (var ent in context.Set<T>())
					{
						res.Dispatcher.Invoke(
							() => _currentEntities.Add(ent));
					}
				}
				res.Dispatcher.Invoke(() => res.IsEnabled = true);
			});
			return res;
		}
		#endregion //Table view page

		public bool TryCreateEntity(Window owner)
		{
			if (_workingEntity != null) throw new InvalidOperationException("Already creating entity!");
			_workingEntity = new();
			var created = CreationWindow.CreateInDialog(_workingEntity, owner);
			if (created)
			{
				_currentEntities.Add(_workingEntity);
			}
			_workingEntity = null;
			return created;
		}
		//TODO: dispatcher of highet level!!

		#region Table creation window
		private TableCreationWindow? _creationWindow;
		private TableCreationWindow CreationWindow => _creationWindow ??= new(this, _tableManagerSelector);

		#endregion //Table creation window

		private T? _workingEntity;
		public async Task<bool> SaveWorkingEntityToDB()
		{
			if (_workingEntity == null) return false;
			try
			{
				await using (var context = await _dbContextFactory.CreateDbContextAsync())
				{
					var set = context.Set<T>();
					await set.AddAsync(_workingEntity);
					await context.SaveChangesAsync();
					_currentEntities.Add(_workingEntity);
				}
				return true;
			}
			catch (Exception ex)
			{
				_ims.SendException(ex);
				return false;
			}
		}


		#region Table selection window
		private TableSelectionWindow? _selectionWindow;
		private TableSelectionWindow SelectionWindow => _selectionWindow ??= new(this);
		#endregion //Table selection window

		bool ITableManager.TrySelectEntity(out object? res, Window owner)
		{
			var selected = TrySelectEntity(out var tres, owner);
			res = tres;
			return selected;
		}
		public bool TrySelectEntity(out T? res, Window owner)
		{
			return SelectionWindow.TrySelectInDialog(out res, owner);
		}


		public void Dispose()
		{
			_creationWindow?.Close();
			_selectionWindow?.Close();
		}
	}
}
