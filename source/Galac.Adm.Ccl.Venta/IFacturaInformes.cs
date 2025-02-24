using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {
    public interface IFacturaInformes {
        System.Data.DataTable BuildFacturacionPorArticulo(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoDelArticulo, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio, bool valIsInformeDetallado);
        System.Data.DataTable BuildFacturacionPorUsuario(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valNombreOperador, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio, bool valIsInformeDetallado);
        System.Data.DataTable BuildFacturaBorradorGenerico(int valConsecutivoCompania, string valNumeroDocumento, eTipoDocumentoFactura valTipoDocumento, eStatusFactura valStatusDocumento, eTalonario valTalonario, eFormaDeOrdenarDetalleFactura valFormaDeOrdenarDetalleFactura, bool valImprimirFacturaConSubtotalesPorLineaDeProducto, string valCiudadCompania, string valNombreOperador);
    }

} //End of namespace Galac.Adm.Ccl.Venta

