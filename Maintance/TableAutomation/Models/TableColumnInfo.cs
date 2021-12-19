using System;
using System.Reflection;

using Maintance.TableAutomation.DbModelAttributes;

namespace Maintance.TableAutomation.Models
{
	public record TableColumnInfo(PropertyInfo PropertyInfo, PropertyInfoAttribute PropertyInfoAttribute,
		ViewColumnAttribute? ViewColumnAttribute, SelectionColumnAttribute? SelectionColumnAttribute);
}
