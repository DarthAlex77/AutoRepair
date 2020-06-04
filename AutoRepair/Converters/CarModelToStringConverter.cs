using System;
using System.Globalization;
using System.Windows.Data;
using AutoRepair.Model;

namespace AutoRepair.Converters
{
    internal class CarModelToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CarModel carModel = value as CarModel;
            return carModel?.Model;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}