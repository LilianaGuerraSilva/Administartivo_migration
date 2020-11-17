using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Comun.Ccl.SttDef;
namespace Galac.Adm.Ccl.Venta {

    public interface IFkFacturaRapidaViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          string Numero { get; set; }
          DateTime Fecha { get; set; }
          string NumeroRIF { get; set; }
          string NombreCliente { get; set; }
        #endregion //Propiedades


    } //End of class IFkFacturaRapidaViewModel

} //End of namespace Galac.Adm.Ccl.Venta

