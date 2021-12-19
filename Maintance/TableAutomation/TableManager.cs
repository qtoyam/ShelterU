using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
		private readonly DbContext _dbContext;
		private readonly IMessageService _ims;
		private readonly TableManagerSelector _tableManagerSelector;
		private readonly SynchronizationContext _synchronizationContext;
		private readonly SemaphoreSlim _semaphore;

		public IReadOnlyCollection<TableColumnInfo> TableColumnInfos { get; } =
			new ReadOnlyCollection<TableColumnInfo>(AutomationHelper.GetTableColumnInfos<T>().ToList());

		private readonly ObservableCollection<T> _currentEntities = new();

		public ICollectionView CreateCollectionView() => new CollectionViewSource() { Source = _currentEntities }.View;

		public TableManager(DbContext dbContext, IMessageService ims, TableManagerSelector tableManagerSelector,
			SynchronizationContext synchronizationContext, SemaphoreSlim semaphore)
		{
			_ims = ims;
			_tableManagerSelector = tableManagerSelector;
			_synchronizationContext = synchronizationContext;
			_dbContext = dbContext;
			_semaphore = semaphore;
			Task.Run(async () =>
			{
				try
				{
					await _semaphore.WaitAsync();
					await foreach (var ent in _dbContext.Set<T>().AsAsyncEnumerable())
					{
						synchronizationContext.Post(
							 (o) =>
							 _currentEntities.Add(ent),
							 null);
					}

				}
				catch (Exception ex)
				{
					_ims.SendError(ex.Message);
				}
				finally
				{
					_semaphore.Release();
				}
			});
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
				await _semaphore.WaitAsync();
				var set = _dbContext.Set<T>();
				await set.AddAsync(_workingEntity);
				await _dbContext.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				_ims.SendException(ex);
				return false;
			}
			finally
			{
				_semaphore.Release();
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
