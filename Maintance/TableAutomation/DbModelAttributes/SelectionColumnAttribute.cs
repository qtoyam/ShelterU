using System;

namespace Maintance.TableAutomation.DbModelAttributes
{
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public class SelectionColumnAttribute : Attribute
	{
		public readonly bool IsVisible;
		public readonly bool IsFilter;

		public SelectionColumnAttribute(bool isVisible, bool isFilter)
		{
			IsVisible = isVisible;
			IsFilter = isFilter;
		}
	}
}
