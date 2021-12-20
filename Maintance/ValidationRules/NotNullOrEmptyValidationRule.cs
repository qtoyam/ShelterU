using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Maintance.ValidationRules
{
	public class NotNullOrEmptyValidationRule : ValidationRule
	{
		public string MessageIfFail { get; init; } = "Required!";

		public NotNullOrEmptyValidationRule()
		{

		}

		public NotNullOrEmptyValidationRule(string messageIfFail)
		{
			MessageIfFail = messageIfFail;
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (value is string s && string.IsNullOrEmpty(s) || value == null) return new(false, MessageIfFail);
			return ValidationResult.ValidResult;
		}
	}
}
