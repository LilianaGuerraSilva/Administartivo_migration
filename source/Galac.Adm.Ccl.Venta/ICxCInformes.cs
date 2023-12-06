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
        DataTable BuildCxCPendientesEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio);
        DataTable BuildCxCPorCliente(int valConsecutivoCompania, string valCodigoDelCliente, string valZonaCobranza, DateTime valFechaDesde, DateTime valFechaHasta, eClientesOrdenadosPor valClientesOrdenadosPor, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio);
        DataTable BuildCxCEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eInformeStatusCXC valStatusCxC, eInformeAgruparPor valAgruparPor, string valZonaDeCobranza, string valSectorDeNegocio, eMonedaDelInformeMM valMonedaDelInforme, string valMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, bool valMostrarNroComprobanteContable);
        ObservableCollection<string> ListaDeSectoresDeNegocioParaInformes();
        ObservableCollection<string> ListaDeZonasDeCobranzaParaInformes();
        ObservableCollection<string> ListaDeMonedasActivasParaInformes();
    }
} //End of namespace Galac..Ccl.ComponenteNoEspecificado

