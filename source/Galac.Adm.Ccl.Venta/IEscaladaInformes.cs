using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {

    public interface IEscaladaInformes {
        DataTable BuildFacturacionEntreFechasVerificacion(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal);
    }
} //End of namespace Galac.Adm.Ccl.Venta

