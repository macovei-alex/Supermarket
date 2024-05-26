using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Supermarket.ViewModel.Converters
{
	internal class DateTimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			try
			{
				return ((DateTime)value).ToString("dd-MM-yyyy");
			}
			catch (FormatException)
			{
				return DateTime.Today.ToString("dd-MM-yyyy");
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			Console.WriteLine($"Convert back target type {targetType}");
			try
			{
				return DateTime.Parse(value.ToString());
			}
			catch (FormatException)
			{
				return DateTime.MinValue;
			}
		}
	}
}
