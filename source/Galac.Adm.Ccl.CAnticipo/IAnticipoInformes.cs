using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Lib;

namespace Galac.Adm.Ccl.CAnticipo {

    public interface IAnticipoInformes {
        System.Data.DataTable BuildAnticipoPorProveedorOCliente(int valConsecutivoCompania, eStatusAnticipo valStatusAnticipo, eCantidadAImprimir valCantidadAImprimir, string valCodigoClienteProveedor, bool valOrdenamientoPorStatus, eMonedaDelInformeMM valMonedaDelInformeMM, bool valEsCliente, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda);
    }
} //End of namespace Galac.Adm.Ccl.CAnticipo

