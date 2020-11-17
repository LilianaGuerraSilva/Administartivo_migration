using System;
using Galac.Comun.Ccl.SttDef;

namespace Galac.Adm.Ccl.GestionCompras {
    public interface ICompraInformes {
        System.Data.DataTable BuildCompra(int valConsecutivoCompania, int valConsecutivoCompra);
        System.Data.DataTable BuildCompraEntreFechas(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, bool valCambioOriginal, bool valMostrarComprasAnuladas, bool valMuestraDetalle, eMonedaParaImpresion MonedaParaImpresion);
        System.Data.DataTable BuildImprimirCostoDeCompraEntreFechas(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eReporteCostoDeCompras valLineasDeProductoCantidadAImprimir, string valCodigoProducto);
        System.Data.DataTable BuildImprimirHistoricoDeCompras(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eReporteCostoDeCompras valLineasDeProductoCantidadAImprimir, string valCodigoProducto);
        System.Data.DataTable BuildImprimirMargenSobreCostoPromedioDeCompra(int valConsecutivoCompania, eNivelDePrecio valNivelDePrecio, eReporteCostoDeCompras valLineasDeProductoCantidadAImprimir, string valCodigoProducto);
        System.Data.DataTable BuildImpresionDeComprasEtiquetas(int valConsecutivoCompania, eNivelDePrecio valNivelDePrecio, string valNumero);
    }
}


