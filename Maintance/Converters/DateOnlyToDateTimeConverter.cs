using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Maintance.Converters
{
	[ValueConversion(typeof(DateOnly), typeof(DateTime))]
	public class DateOnlyToDateTimeConverter : IValueConverter
	{
		public object Convert(object valueSource, Type targetType, object parameter, CultureInfo culture)
		{
			if (valueSource is DateOnly sdo) return sdo.ToDateTime(TimeOnly.MinValue);
			return valueSource;
		}

		public object ConvertBack(object valueTarget, Type targetType, object parameter, CultureInfo culture)
		{
			if (valueTarget is DateTime dt) return DateOnly.FromDateTime(dt);
			return valueTarget;
		}
	}
}
