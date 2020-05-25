using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace AutoRepair.Converters
{
    public class ColorToBackgroundColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                Color color = (Color) value;
                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A,color.R,color.G,color.B));
            }

            throw new ArgumentNullException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
