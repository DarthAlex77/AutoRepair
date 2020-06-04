using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using AutoRepair.Model;

namespace AutoRepair.Converters
{
    public class CarToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Car      car      = value as Car;
            CarModel carModel = car?.CarModel;
            return new StringBuilder(carModel?.Manufacturer + " " + carModel?.Model + " " + car?.CarNumber);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}