using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Saw.Lib;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Cliente {

    public interface IClienteInformes {
        System.Data.DataTable BuildHistoricoDeCliente(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoCliente, eMonedaDelInformeMM valMonedaDelInforme, string valCodigoMoneda, string valNombreMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, eClientesOrdenadosPor valClienteOrdenarPor);
    }
} //End of namespace Galac.Saw.Ccl.Cliente

