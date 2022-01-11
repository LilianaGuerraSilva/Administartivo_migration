using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {
    public interface IResumenDiarioDeVentasInformes {
        System.Data.DataTable BuildResumenDiarioDeVentasEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, bool valAgruparPorMaquinaFiscal, string valConsecutivoMaquinaFiscal);
    }
} //End of namespace Galac.Adm.Ccl.Venta

