using Galac.Saw.Ccl.Inventario;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Galac.Saw.Lib.Uil {
    public class StatusArticuloToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            eStatusArticulo status = (eStatusArticulo)value;
            switch (status) {
                case eStatusArticulo.Vigente:
                    return new SolidColorBrush(Colors.Green);
                case eStatusArticulo.Desincorporado:
                    return new SolidColorBrush(Colors.Red);
                default:
                    return new SolidColorBrush(Colors.Black);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
