using System;

namespace Maintance.TableAutomation.DbModelAttributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyInfoAttribute : Attribute
	{
		public readonly string DisplayName;
		public readonly bool IsAutofoFill;
		public readonly bool IsOptional;

		public PropertyInfoAttribute(string displayName, bool isAutofoFill = false, bool isOptional = false)
		{
			IsAutofoFill = isAutofoFill;
			IsOptional = isOptional;
			DisplayName = displayName;
		}
	}
}
