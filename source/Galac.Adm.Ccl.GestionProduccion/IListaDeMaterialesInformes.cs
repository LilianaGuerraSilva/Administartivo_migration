using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;

namespace Galac.Adm.Ccl.GestionProduccion {

    public interface IListaDeMaterialesInformes {
        System.Data.DataTable BuildListaDeMaterialesDeInventarioAProducir(int valConsecutivoCompania, string valCodigoInventarioAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir);
    }
} //End of namespace Galac.Adm.Ccl.GestionProduccion

