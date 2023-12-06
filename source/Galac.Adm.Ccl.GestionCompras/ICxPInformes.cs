using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.GestionCompras {

    public interface ICxPInformes {
        System.Data.DataTable BuildCuentasPorPagarEntreFechas(int valConsecutivoCompania);
    }
} //End of namespace Galac..Ccl.ComponenteNoEspecificado