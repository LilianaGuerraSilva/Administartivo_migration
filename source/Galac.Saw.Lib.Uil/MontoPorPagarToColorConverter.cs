using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Galac.Saw.Lib.Uil {
    /// <summary>
    /// Conversor que transforma el valor de Monto por Pagar en colores.
    /// Si el Monto por Pagar es > parameter entonces el color es "Rojo", caso contrario es "Verde".
    /// </summary>
    public class MontoPorPagarToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            decimal decimalValue = 0;
            decimal decimalParameter = 0;
            if (decimal.TryParse(value.ToString(), out decimalValue) && decimal.TryParse(parameter.ToString(), out decimalParameter)) {
                Color currentColor;
                if (decimalValue <= decimalParameter) {
                    currentColor = Colors.Green;
                } else {
                    currentColor = Colors.Red;
                }
                return new SolidColorBrush(currentColor);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
