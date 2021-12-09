using Galac.Saw.Ccl.SttDef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.Venta {

    public interface IFkFacturaViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          string Numero { get; set; }
          DateTime Fecha { get; set; }
          string NombreCliente { get; set; }
          string NombreVendedor { get; set; }
          eTipoDocumentoFactura TipoDeDocumento { get; set; }
          string NumeroComprobanteFiscal { get; set; }
        #endregion //Propiedades

    } //End of class IFkFacturaViewModel

} //End of namespace Galac.Adm.Ccl.Venta

