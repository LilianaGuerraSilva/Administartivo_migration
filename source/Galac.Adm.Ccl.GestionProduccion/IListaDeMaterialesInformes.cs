using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Saw.Lib;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;

namespace Galac.Adm.Ccl.GestionProduccion {

    public interface IListaDeMaterialesInformes {        
        System.Data.DataTable BuildListaDeMateriales(int valConsecutivoCompania, string valCodigoListaAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir,eMonedaDelInformeMM valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valNombreMoneda, string valCodigoMoneda);
    }
} //End of namespace Galac.Adm.Ccl.GestionProduccion

