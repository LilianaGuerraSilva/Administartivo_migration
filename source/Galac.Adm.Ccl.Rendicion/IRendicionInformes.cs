using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.CajaChica {

    public interface IRendicionInformes {
        System.Data.DataTable BuildReposicionesEntreFechas(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, bool valImprimeUna, string valCodigoCtaBancariaCajaChica);
    }
} //End of namespace Galac.Adm.Ccl.CajaChica

