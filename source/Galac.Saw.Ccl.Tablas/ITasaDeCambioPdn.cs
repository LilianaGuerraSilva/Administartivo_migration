using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Ccl.Tablas {
    public interface ITasaDeCambioPdn {
        #region Metodos
        bool ExisteTasaDeCambioParaElDia(string valMoneda, DateTime valFecha, out decimal outTasa);
        bool InsertaTasaDeCambioParaElDia(string valMoneda, DateTime valFechaVigencia, string valNombre, decimal valCambioAbolivares);
        #endregion //Metodos
    }
}
