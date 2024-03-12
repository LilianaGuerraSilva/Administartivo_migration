using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using System.Data;
using System.Collections.ObjectModel;
using Galac.Saw.Lib;

namespace Galac.Adm.Ccl.Venta {
    public interface ICxCInformes {
        DataTable BuildCxCPendientesEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eMonedaDelInformeMM valMonedaDelReporte, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda);
        DataTable BuildCxCPorCliente(int valConsecutivoCompania, string valCodigoDelCliente, string valZonaCobranza, DateTime valFechaDesde, DateTime valFechaHasta, eClientesOrdenadosPor valClientesOrdenadosPor, eMonedaParaImpresion valMonedaDelReporte, eTasaDeCambioParaImpresion valTipoTasaDeCambio);
        DataTable BuildCxCEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eInformeStatusCXC_CXP valStatusCxC, eInformeAgruparPor valAgruparPor, string valZonaDeCobranza, string valSectorDeNegocio, eMonedaDelInformeMM valMonedaDelInforme, string valCodigoMoneda, string valNombreMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, bool valMostrarNroComprobanteContable);
        ObservableCollection<string> ListaDeSectoresDeNegocioParaInformes();
        ObservableCollection<string> ListaDeZonasDeCobranzaParaInformes();
    }
} //End of namespace Galac..Ccl.ComponenteNoEspecificado

