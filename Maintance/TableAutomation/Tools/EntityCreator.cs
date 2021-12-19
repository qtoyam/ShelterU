﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

using Maintance.Converters;
using WPFCoreEx.ValidationRules;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using Maintance.TableAutomation.Views;
using Maintance.TableAutomation;

namespace Maintance.TableAutomation.Tools
{
	//public sealed class EntityCreator<T> : IDisposable
	//	where T : class, new()
	//{
	//	private readonly TableCreationWindow _tableCreate;
	//	private T? _value;

	//	public EntityCreator(DbSet<T> dbSet, DbContext context)
	//	{
	//		_tableCreate = new(() =>
	//		{
	//			try
	//			{
	//				dbSet.Add(_value!);
	//				context.SaveChanges();
	//				return true;
	//			}
	//			catch (Exception ex)
	//			{
	//				return false;
	//			}
	//		});
	//		FillFieldsPanel();
	//	}

	//	private void FillFieldsPanel()
	//	{
	//		var c = _tableCreate.FieldsPanel.Children;
	//		var defForeground = Application.Current.Resources["DefaultAutoGeneratedContentForeground"] as SolidColorBrush;
	//		foreach (var vcp in AutomationHelper.GetTableColumnInfos(typeof(T)).Where(v => !v.attr.IsAutoFill))
	//		{
	//			Type? t = Nullable.GetUnderlyingType(vcp.prop.PropertyType);
	//			bool isNullable = t != null;
	//			t ??= vcp.prop.PropertyType;
	//			Control child;
	//			Binding binding = new(vcp.prop.Name)
	//			{
	//				Mode = BindingMode.TwoWay
	//			};
	//			if (!isNullable)
	//			{
	//				binding.ValidationRules.Add(new StringNotEmptyValidationRule()
	//				{
	//					MessageIfEmpty = "Обязательно к заполнению!"
	//				});
	//			}
	//			if (t == typeof(DateOnly))
	//			{
	//				var dp = new DatePicker();
	//				binding.Converter = new DateOnlyToDateTimeConverter();
	//				BindingOperations.SetBinding(dp, DatePicker.SelectedDateProperty, binding);
	//				child = dp;
	//			}
	//			else
	//			{
	//				var tb = new TextBox();
	//				binding.Converter = new EmptyStringToNullConverter();
	//				BindingOperations.SetBinding(tb, TextBox.TextProperty, binding);
	//				child = tb;
	//			}
	//			if (!string.IsNullOrEmpty(vcp.attr.ViewColumnName))
	//			{
	//				MaterialDesignThemes.Wpf.HintAssist.SetHint(child, vcp.attr.ViewColumnName);
	//			}
	//			child.Margin = new(0, 20, 0, 20);
	//			if (defForeground != null) child.Foreground = defForeground;
	//			c.Add(child);
	//		}
	//	}

	//	public bool TryGetResult(out T? result)
	//	{
	//		//idk mb bindings will fail
	//		_value = new();
	//		_tableCreate.FieldsPanel.DataContext = _value;

	//		_tableCreate.ShowDialog();
	//		if (_tableCreate.Created == true)
	//		{
	//			result = _value;
	//			return true;
	//		}
	//		result = null;
	//		return false;
	//	}

	//	public void Dispose()
	//	{
	//		_tableCreate.Close();
	//	}
	//}
}
