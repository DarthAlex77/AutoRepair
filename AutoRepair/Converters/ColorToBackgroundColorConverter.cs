using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace AutoRepair.Converters
{
    public class ColorToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null) return new SolidColorBrush((Color) value);

            throw new ArgumentNullException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}