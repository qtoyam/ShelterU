using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Maintance.Converters
{
	[ValueConversion(typeof(object), typeof(string))]
	public class EmptyStringToNullConverter : IValueConverter
	{
		public object Convert(object valueSource, Type targetType, object parameter, CultureInfo culture)
		{
			return valueSource;
		}

		public object ConvertBack(object valueTarget, Type targetType, object parameter, CultureInfo culture)
		{
			if(valueTarget is string s)
			{
				return string.IsNullOrEmpty(s) ? null! : s;
			}
			return valueTarget;
		}
	}
}
