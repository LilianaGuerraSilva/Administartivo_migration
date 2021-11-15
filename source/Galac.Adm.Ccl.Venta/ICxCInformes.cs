using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {

    public interface ICxCInformes {
        System.Data.DataTable BuildCxCPendientesEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte);
        System.Data.DataTable BuildCxCPorCliente(int valConsecutivoCompania, string valCodigoDelCliente, string valZonaCobranza, DateTime valFechaDesde, DateTime valFechaHasta, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, eClientesOrdenadosPor valClientesOrdenadosPor);
    }
} //End of namespace Galac..Ccl.ComponenteNoEspecificado

