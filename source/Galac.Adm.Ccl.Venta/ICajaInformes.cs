using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;

namespace Galac.Adm.Ccl.Venta {

    public interface ICajaInformes {
        System.Data.DataTable BuildCuadreCajaCobroMultimonedaDetallado(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eCantidadAImprimir valCantidadOperadorDeReporte, string valNombreDelOperador, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, bool valTotalesTipoCobro);
        System.Data.DataTable BuildCajasAperturadas(int valConsecutivoCompania);
        System.Data.DataTable BuildCuadreCajaPorTipoCobro(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTipoDeInforme valTipoDeInforme);
        System.Data.DataTable BuildCuadreCajaPorTipoCobroYUsuario(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eCantidadAImprimir valCantidadOperadorDeReporte, string valNombreDelOperador, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte);
        System.Data.DataTable BuildCuadreCajaConDetalleFormaPago(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTipoDeInforme valTipoDeInforme, bool valTotalesTipoPago);
        System.Data.DataTable BuildCuadreCajaPorUsuario(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eTipoDeInforme valTipoDeInforme, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, eCantidadAImprimir valCantidadOperadorDeReporte, string valNombreDelOperador);
        System.Data.DataTable BuildCajaCerrada(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta);
    }
} //End of namespace Galac.Adm.Ccl.Venta

