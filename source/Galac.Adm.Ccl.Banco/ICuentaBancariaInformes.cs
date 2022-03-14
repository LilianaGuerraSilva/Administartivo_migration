using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Banco {
    public interface ICuentaBancariaInformes {
        System.Data.DataTable BuildSaldosBancarios(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, bool valSoloCuentasActivas);
    }
} //End of namespace Galac.Adm.Ccl.Banco
