using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {
    public interface IFacturaInformes {
        System.Data.DataTable BuildFacturacionPorArticulo(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoDelArticulo, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio, bool valIsInformeDetallado);
    }
} //End of namespace Galac.Adm.Ccl.Venta

