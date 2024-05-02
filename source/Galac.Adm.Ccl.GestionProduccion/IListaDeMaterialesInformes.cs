using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Saw.Lib;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;

namespace Galac.Adm.Ccl.GestionProduccion {

    public interface IListaDeMaterialesInformes {
        System.Data.DataTable BuildListaDeMaterialesSalida(int valConsecutivoCompania, string valCodigoListaAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir, string valMonedaDelInformeMM, decimal valTasaDeCambio, string[] valListaMoneda);
        System.Data.DataTable BuildListaDeMaterialesInsumos(int valConsecutivoCompania, string valCodigoListaAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir, string valMonedaDelInformeMM, decimal valTasaDeCambio, string[] valListaMoneda);
    }
} //End of namespace Galac.Adm.Ccl.GestionProduccion

