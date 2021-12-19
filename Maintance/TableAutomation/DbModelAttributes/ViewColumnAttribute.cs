using System;

namespace Maintance.TableAutomation.DbModelAttributes
{
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public class ViewColumnAttribute : Attribute
	{
		public readonly bool IsFilter;
		public readonly bool IsGroup;

		public ViewColumnAttribute(bool isFilter = false, bool isGroup = false)
		{
			IsFilter = isFilter;
			IsGroup = isGroup;
		}
	}
}
