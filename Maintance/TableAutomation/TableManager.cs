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
using Maintance.TableAutomation.DbModelAttributes;
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
		string ParentName { get; }
		bool TrySelectEntity(out object? res, Window owner);
		bool TryCreateEntity(Window owner);
		Task<bool> TryDeleteEntity(object entity);
		bool TryEditEntity(object entity, Window owner);
		ICollectionView CreateCollectionView();
		IReadOnlyCollection<TableColumnInfo> TableColumnInfos { get; }
		Task<bool> TrySaveWorkingEntityToDB(bool create);
		Task<bool> TryCancelEditWorkingEntity();
	}


	public sealed class TableManager<T> : ITableManager
		where T : class, new()
	{
		public string ParentName { get; private set; }
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
			ParentName = typeof(T).GetCustomAttributes(typeof(TableInfoAttribute), true).Cast<TableInfoAttribute>().FirstOrDefault()?.Name ?? "SPECIFY NAME";
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
					await foreach (var ent in _dbContext.Set<T>().AsAsyncEnumerable())//.AsNoTracking().AsAsyncEnumerable())
					{
						_synchronizationContext.Post(
							 (o) =>
							 _currentEntities.Add(ent),
							 null);
					}

				}
				catch (Exception ex)
				{
					_ims.SendException(ex);
				}
				finally
				{
					_semaphore.Release();
				}
			});
		}

		#region Table view page
		private TableViewPage? _viewPage;
		public TableViewPage ViewPage => _viewPage ??= new(this);
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

		bool ITableManager.TryEditEntity(object entity, Window owner) => TryEditEntity((T)entity, owner);
		public bool TryEditEntity(T entity, Window owner)
		{
			if (_workingEntity != null) throw new InvalidOperationException("Already creating entity!");
			_workingEntity = entity;

			var edited = CreationWindow.EditInDialog(entity, owner);

			_workingEntity = null;
			return edited;
		}

		#region Table creation window
		private TableCreationWindow? _creationWindow;
		private TableCreationWindow CreationWindow => _creationWindow ??= new(this, _tableManagerSelector);
		#endregion //Table creation window

		private T? _workingEntity;
		public async Task<bool> TrySaveWorkingEntityToDB(bool create)
		{
			if (_workingEntity == null) return false;
			try
			{
				await _semaphore.WaitAsync();
				var set = _dbContext.Set<T>();
				if (create)
				{
					await set.AddAsync(_workingEntity);
				}
				else
				{
					set.Update(_workingEntity);
				}
				await _dbContext.SaveChangesAsync(true);
				//_dbContext.ChangeTracker.Clear();
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

		Task<bool> ITableManager.TryCancelEditWorkingEntity() => TryCancelEditWorkingEntity();
		public async Task<bool> TryCancelEditWorkingEntity()
		{
			if (_workingEntity == null) return false;
			try
			{
				await _semaphore.WaitAsync();
				var en = _dbContext.Attach(_workingEntity);
				var index = _currentEntities.IndexOf(_workingEntity);
				await en.ReloadAsync();
				//_dbContext.ChangeTracker.Clear();
				_synchronizationContext.Send((o) =>
					_currentEntities[index] = en.Entity, null);
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

		Task<bool> ITableManager.TryDeleteEntity(object entity) => TryDeleteEntity((T)entity);
		public async Task<bool> TryDeleteEntity(T entity)
		{
			try
			{
				await _semaphore.WaitAsync();
				_dbContext.Remove(entity);
				await _dbContext.SaveChangesAsync();
				_currentEntities.Remove(entity);
				//_dbContext.ChangeTracker.Clear();
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

		public void Dispose()
		{
			_creationWindow?.Close();
			_selectionWindow?.Close();
		}

	}
}
