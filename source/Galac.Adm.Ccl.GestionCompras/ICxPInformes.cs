using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using Galac.Saw.Lib;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.GestionCompras {

    public interface ICxPInformes {
        DataTable BuildCxPEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eInformeStatusCXC_CXP valStatusCxP, eMonedaDelInformeMM valMonedaDelInforme, string valCodigoMoneda,string valNombreMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, bool valMostrarNroComprobanteContable);
    }
} //End of namespace Galac..Ccl.ComponenteNoEspecificado