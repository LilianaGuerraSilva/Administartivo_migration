using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Saw.Lib;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.GestionCompras {

    public interface IProveedorInformes {
        System.Data.DataTable BuildHistoricoDeProveedor(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoProveedor, eMonedaDelInformeMM valMonedaDelInforme, string valCodigoMoneda, string valNombreMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, eProveedorOrdenadosPor valProveedorOrdenarPor);
    }
} //End of namespace Galac.Saw.Ccl.Cliente

