using System;
using System.Globalization;
using System.Windows.Data;

namespace Galac.Saw.Lib.Uil {
    public class StringToCurrencyConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            decimal decimalValue;
            decimal.TryParse(value.ToString(), out decimalValue);
            return decimalValue.ToString("N");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
