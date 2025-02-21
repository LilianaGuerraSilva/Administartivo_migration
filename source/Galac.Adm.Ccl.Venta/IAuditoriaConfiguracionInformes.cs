using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {

    public interface IAuditoriaConfiguracionInformes {
        System.Data.DataTable BuildAuditoriaConfiguracionDeCaja(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta);
    }
} //End of namespace Galac.Saw.Ccl.Venta

