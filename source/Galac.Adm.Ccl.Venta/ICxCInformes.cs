using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {

    public interface ICxCInformes {
        System.Data.DataTable BuildCxCPendientesEntreFechas(int valConsecutivoCompania, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, DateTime valFechaDesde, DateTime valFechaHasta, bool valUsaCantacto);
    }
} //End of namespace Galac..Ccl.ComponenteNoEspecificado

