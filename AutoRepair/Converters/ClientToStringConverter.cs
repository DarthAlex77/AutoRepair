using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using AutoRepair.Model;

namespace AutoRepair.Converters
{
    public class ClientToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var client = value as Client;
            var sb = new StringBuilder();
            sb.Append(client?.FirstName + " ");
            if (!String.IsNullOrWhiteSpace(client?.Patronymic))
            {
                sb.Append(client.Patronymic + " ");
            }

            sb.Append(client?.LastName);
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}