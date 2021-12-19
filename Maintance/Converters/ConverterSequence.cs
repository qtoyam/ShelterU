using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Maintance.Converters
{
	internal class ConverterSequence : IValueConverter
	{
		public IValueConverter Converter1 { get; set; } = null!;
		public IValueConverter Converter2 { get; set; } = null!;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Converter2.Convert(Converter1.Convert(value, targetType, parameter, culture),
				targetType, parameter, culture);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Converter1.ConvertBack(Converter2.Convert(value, targetType, parameter, culture),
				targetType, parameter, culture);
		}
	}
}
