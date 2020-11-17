using LibGalac.Aos.Base;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Galac.Saw.Lib.Uil {
    public class LimitarTamanoCadenaConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string cadena = (string)value;
            int tamano = LibConvert.ToInt(parameter);
            return LibString.Mid(cadena, 0, tamano);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
