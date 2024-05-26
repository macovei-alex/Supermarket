using System;
using System.Windows.Data;

namespace Supermarket.ViewModel.Converters
{
	public class GenericBoolConverter : IValueConverter
	{
		public object TrueValue { get; set; }
		public object FalseValue { get; set; }

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if ((bool)value == true)
			{
				return TrueValue;
			}
			else
			{
				return FalseValue;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
