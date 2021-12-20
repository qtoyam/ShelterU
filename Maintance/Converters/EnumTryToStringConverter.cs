using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Maintance.Converters
{
	public class EnumTryToStringConverter :MarkupExtension, IValueConverter
	{
		//TODO: slow unoptimized parasha if enum
		public object Convert(object valueSource, Type targetType, object parameter, CultureInfo culture)
		{
			if (valueSource == null) return null;
			var t = valueSource.GetType();
			if (t.IsEnum)
			{
				return t.GetField(valueSource.ToString())
					.GetCustomAttributes(typeof(DescriptionAttribute), false)
					.Cast<DescriptionAttribute>()
					.Single()
					.Description;
			}
			return valueSource;
		}

		public object ConvertBack(object valueTarget, Type targetType, object parameter, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		public override object ProvideValue(IServiceProvider serviceProvider) => this;
	}
}
