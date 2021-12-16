using System;

namespace Maintance.DbModels.Attributes
{
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	internal class ViewColumnAttribute : Attribute
	{
		public readonly string ViewColumnName;
		public readonly bool IsFilter;
		public readonly bool IsGroup;

		public ViewColumnAttribute(string viewColumnName, bool isFilter = false, bool isGroup = false)
		{
			ViewColumnName = viewColumnName;
			IsFilter = isFilter;
			IsGroup = isGroup;
		}
	}
}
