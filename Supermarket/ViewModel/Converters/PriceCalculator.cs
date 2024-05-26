using Supermarket.Model;
using System;
using System.Windows.Data;

namespace Supermarket.ViewModel.Converters
{
	internal class PriceCalculator : IValueConverter
	{
		private readonly decimal _vat = Cache.Instance.VAT;

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is string valueString)
			{
				if (valueString == null || valueString == string.Empty)
				{
					return (decimal)0;
				}

				return decimal.Parse(valueString) * (1 + _vat);
			}

			throw new ArgumentException("Value is not a string");
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
