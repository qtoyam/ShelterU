using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Maintance.Converters
{
	public class EnumToStrConverter : IValueConverter
	{
		private readonly IReadOnlyDictionary<int, string> _enumStrDic;
		private readonly Type _enumType;

		public EnumToStrConverter(IReadOnlyDictionary<int, string> enumStrDic, Type enumType)
		{
			_enumStrDic = enumStrDic;
			_enumType = enumType;
		}

		public object Convert(object valueSource, Type targetType, object parameter, CultureInfo culture)
		{
			return _enumStrDic[(int)valueSource];
		}

		public object ConvertBack(object valueTarget, Type targetType, object parameter, CultureInfo culture)
		{
			if (valueTarget is string s)
			{
				foreach (var enumStrDic in _enumStrDic)
				{
					if (enumStrDic.Value == s) return Enum.ToObject(_enumType, enumStrDic.Key);
				}
			}
			return valueTarget;
		}
	}
}
