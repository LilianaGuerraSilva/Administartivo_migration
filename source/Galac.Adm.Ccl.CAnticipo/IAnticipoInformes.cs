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
        DataTable BuildAnticipoClienteProveedor(bool valEsCliente, int valConsecutivoCompania, eCantidadAImprimir valCantidadAImprimir, eStatusAnticipo valStatusAnticipo, eCantidadAImprimir valCantidadAImprimirClienteProveedor, string valCodigoClienteProveedor, bool valOrderPorStatus, eMonedaDelInformeMM valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda);
    }
} //End of namespace Galac.Adm.Ccl.CAnticipo

