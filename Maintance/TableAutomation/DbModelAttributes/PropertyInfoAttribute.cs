using System;

namespace Maintance.TableAutomation.DbModelAttributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyInfoAttribute : Attribute
	{
		public readonly string DisplayName;
		public readonly bool IsAutofoFill;
		public readonly bool IsOptional;
        public readonly bool IsReadonly;

        public PropertyInfoAttribute(string displayName, bool isAutofoFill = false, bool isOptional = false, bool isReadonly = false)
		{
			IsAutofoFill = isAutofoFill;
			IsOptional = isOptional;
            IsReadonly = isReadonly;
            DisplayName = displayName;
		}
	}
}
