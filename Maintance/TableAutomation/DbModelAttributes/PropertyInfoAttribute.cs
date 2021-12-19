using System;

namespace Maintance.TableAutomation.DbModelAttributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyInfoAttribute : Attribute
	{
		public readonly bool IsAutfoFill;
		public readonly bool IsOptional;

		public PropertyInfoAttribute(bool isAutfoFill, bool isOptional)
		{
			IsAutfoFill = isAutfoFill;
			IsOptional = isOptional;
		}
	}
}
