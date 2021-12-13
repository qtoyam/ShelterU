using System;

namespace Maintance.Models.Attributes
{
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class FilterPropertyAttribute : Attribute
	{
		public FilterPropertyAttribute() : base() { }
	}
}
