using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Comun.Ccl.SttDef;
namespace Galac.Adm.Ccl.Venta {

    public interface IFkRenglonCobroDeFacturaViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          string NumeroFactura { get; set; }
          eTipoDocumentoFactura TipoDeDocumento { get; set; }
          int ConsecutivoRenglon { get; set; }
        #endregion //Propiedades


    } //End of class IFkRenglonCobroDeFacturaViewModel

} //End of namespace Galac..Ccl.ComponenteNoEspecificado

