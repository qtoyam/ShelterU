using System;

namespace Maintance.Models.Attributes
{
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class GroupPropertyAttribute : Attribute
	{
		public GroupPropertyAttribute() : base() { }
	}
}