using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;

namespace Galac.Adm.Ccl. GestionProduccion {

    public interface IOrdenDeProduccionInformes {
        System.Data.DataTable BuildOrdenDeProduccionRpt(int valConsecutivoCompania, string valCodigoOrden, DateTime valFechaDesde, DateTime valFechaHasta, eGeneradoPor valGeneradoPor);
        System.Data.DataTable BuildRequisicionDeMateriales(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, bool valMostrarSoloExistenciaInsuficiente, string valCodigoOrden, eGeneradoPor valGeneradoPor);
        System.Data.DataTable BuildCostoProduccionInventarioEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eCantidadAImprimir valCantidadAImprimir, string valCodigoInventarioAProducir, eGeneradoPor valGeneradoPor, string valCodigoOrden);
        System.Data.DataTable BuildCostoMatServUtilizadosEnProduccionInv(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoOrden, eGeneradoPor valGeneradoPor);
        System.Data.DataTable BuildProduccionPorEstatusEntreFecha(int valConsecutivoCompania, eTipoStatusOrdenProduccion valEstatus, DateTime valFechaInicial, DateTime valFechaFinal, eGeneradoPor valGeneradoPor, string valCodigoOrden);
        System.Data.DataTable BuildDetalleDeCostoDeProduccion(int consecutivoCompania, DateTime fechaInicial, DateTime fechaFinal, eSeleccionarOrdenPor seleccionarOrdenPor, int consecutivoOrden, string monedaDelInforme, decimal tasaDeCambio, string[] listaMonedas);
        System.Data.DataTable BuildDetalleDeCostoDeProduccionSalida(int consecutivoCompania, DateTime fechaInicial, DateTime fechaFinal, eSeleccionarOrdenPor seleccionarOrdenPor, int consecutivoOrden, string monedaDelInforme, decimal tasaDeCambio, string[] listaMonedas);
    }
} //End of namespace Galac.Adm.Ccl. GestionProduccion

