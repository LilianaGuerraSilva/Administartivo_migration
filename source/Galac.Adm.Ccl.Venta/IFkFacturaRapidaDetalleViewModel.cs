using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Saw.Ccl.SttDef;
namespace Galac.Adm.Ccl.Venta {

    public interface IFkFacturaRapidaDetalleViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          string NumeroFactura { get; set; }
          eTipoDocumentoFactura TipoDeDocumento { get; set; }
          int ConsecutivoRenglon { get; set; }
        #endregion //Propiedades


    } //End of class IFkFacturaRapidaDetalleViewModel

} //End of namespace Galac.Adm.Ccl.Venta

