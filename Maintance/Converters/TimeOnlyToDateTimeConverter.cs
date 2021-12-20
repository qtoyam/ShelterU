using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Maintance.Converters
{
	[ValueConversion(typeof(TimeOnly), typeof(DateTime))]
	public class TimeOnlyToDateTimeConverter : IValueConverter
	{
		public object Convert(object valueSource, Type targetType, object parameter, CultureInfo culture)
		{
			if (valueSource is TimeOnly sdo) return DateTime.MinValue.Add(sdo.ToTimeSpan());
			return valueSource;
		}

		public object ConvertBack(object valueTarget, Type targetType, object parameter, CultureInfo culture)
		{
			if (valueTarget is DateTime dt) return TimeOnly.FromDateTime(dt);
			return valueTarget;
		}
	}
}
