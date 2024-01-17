using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;

namespace Galac.Adm.Ccl.Venta {

    public interface ICobranzaInformes {
        System.Data.DataTable BuildCobranzasEntreFechas(int valConsecutivoCompania,Galac.Saw.Lib.eMonedaParaImpresion valMonedaReporte,Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio,decimal valTasaDeCambio,DateTime valFechaDesde,DateTime valFechaHasta,string valNombreCobrador,string valNombreCliente,string valNombreCuentaBancaria,eFiltrarCobranzasPor valFiltrarCobranzasPor,bool valAgrupado,bool valUsaVentasConIvaDiferidos);
        System.Data.DataTable BuildComisionDeVendedoresPorCobranzaMonto(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eTipoDeInforme valTipoDeInforme, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTasaDeCambioParaImpresion valTasaDeCambioImpresion, eCantidadAImprimir valCantidadAImprimir, string valCodigoVendedor);
    }
} //End of namespace Galac.Adm.Ccl.Venta

