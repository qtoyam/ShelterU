using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Maintance.TableAutomation.DbModelAttributes;

namespace Maintance.TableAutomation.Models
{
	public record TableColumnInfo(PropertyInfo PropertyInfo, ViewColumnAttribute ViewColumnAttribute);
}
