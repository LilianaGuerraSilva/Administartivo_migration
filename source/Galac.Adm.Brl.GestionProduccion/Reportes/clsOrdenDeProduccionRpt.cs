using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl. GestionProduccion;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl. GestionProduccion.Reportes {

    public class clsOrdenDeProduccionRpt: ILibReportInfo, IOrdenDeProduccionInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsOrdenDeProduccionRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Orden de Producción", OrdenDeProduccionInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> OrdenDeProduccionInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Adm.Gp_OrdenDeProduccionSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable IOrdenDeProduccionInformes.BuildOrdenDeProduccionRpt(int valConsecutivoCompania, string valCodigoOrden, DateTime valFechaInicial, DateTime valFechaHasta, eGeneradoPor valGeneradoPor) {
            string vSql = "";
            System.Data.DataTable vDt = new System.Data.DataTable();
            clsOrdenDeProduccionSql insOrdenDeProduccionSql = new clsOrdenDeProduccionSql();
            LibGalac.Aos.Base.ILibDataRpt insOrdenDeProduccionRpt = new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionDat();
            vSql = insOrdenDeProduccionSql.SqlOrdenDeProduccionRpt(valConsecutivoCompania, valCodigoOrden, valFechaInicial, valFechaHasta, valGeneradoPor);
            return insOrdenDeProduccionRpt.GetDt(vSql, 0);
        }

        System.Data.DataTable IOrdenDeProduccionInformes.BuildRequisicionDeMateriales(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, bool valMostrarSoloExistenciaInsuficiente, string valCodigoOrden, eGeneradoPor valGeneradoPor) {
            string vSql = "";
            System.Data.DataTable vDt = new System.Data.DataTable();
            clsOrdenDeProduccionSql insOrdenDeProduccionSql = new clsOrdenDeProduccionSql();
            LibGalac.Aos.Base.ILibDataRpt insRequisicionDeMateriales = new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionDat();
            vSql = insOrdenDeProduccionSql.SqlRequisicionDeMateriales(valConsecutivoCompania, valFechaInicial, valFechaFinal, valMostrarSoloExistenciaInsuficiente, valCodigoOrden, valGeneradoPor);
            return insRequisicionDeMateriales.GetDt(vSql, 0);
        }

        System.Data.DataTable IOrdenDeProduccionInformes.BuildCostoProduccionInventarioEntreFechas(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eCantidadAImprimir valCantidadAImprimir, string valCodigoInventarioAProducir, eGeneradoPor valGeneradoPor, string valCodigoOrden, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
            string vSql = "";
            System.Data.DataTable vDt = new System.Data.DataTable();
            clsOrdenDeProduccionSql insOrdenDeProduccionSql = new clsOrdenDeProduccionSql();
            LibGalac.Aos.Base.ILibDataRpt insCostoProduccionInventarioEntreFechas = new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionDat();
            vSql = insOrdenDeProduccionSql.SqlCostoProduccionInventarioEntreFechas(valConsecutivoCompania, valFechaInicial, valFechaFinal,  valCantidadAImprimir, valCodigoInventarioAProducir, valGeneradoPor, valCodigoOrden, valMonedaDelInforme, valTasaDeCambio, valCodigoMoneda, valNombreMoneda);
            return insCostoProduccionInventarioEntreFechas.GetDt(vSql, 0);
        }

        System.Data.DataTable IOrdenDeProduccionInformes.BuildCostoMatServUtilizadosEnProduccionInv(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, string valCodigoOrden, eGeneradoPor valGeneradoPor) {
            string vSql = "";
            System.Data.DataTable vDt = new System.Data.DataTable();
            clsOrdenDeProduccionSql insOrdenDeProduccionSql = new clsOrdenDeProduccionSql();
            LibGalac.Aos.Base.ILibDataRpt insCostoMatServUtilizadosEnProduccionInv = new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionDat();
            vSql = insOrdenDeProduccionSql.SqlCostoMatServUtilizadosEnProduccionInv(valConsecutivoCompania, valFechaInicial, valFechaFinal, valCodigoOrden, valGeneradoPor);
            return insCostoMatServUtilizadosEnProduccionInv.GetDt(vSql, 0);
        }

        System.Data.DataTable IOrdenDeProduccionInformes.BuildProduccionPorEstatusEntreFecha(int valConsecutivoCompania, eTipoStatusOrdenProduccion valStatus, DateTime valFechaInicial, DateTime valFechaFinal, eGeneradoPor valGeneradoPor, string valCodigoOrden) {
            string vSql = "";
            System.Data.DataTable vDt = new System.Data.DataTable();
            clsOrdenDeProduccionSql insOrdenDeProduccionSql = new clsOrdenDeProduccionSql();
            LibGalac.Aos.Base.ILibDataRpt insProduccionPorEstatusEntreFecha = new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionDat();
            vSql = insOrdenDeProduccionSql.SqlProduccionPorEstatusEntreFecha(valConsecutivoCompania, valStatus, valFechaInicial, valFechaFinal, valGeneradoPor, valCodigoOrden );
            return insProduccionPorEstatusEntreFecha.GetDt(vSql, 0);
        }

        System.Data.DataTable IOrdenDeProduccionInformes.BuildDetalleDeCostoDeProduccion(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eSeleccionarOrdenPor valSeleccionarOrdenPor, int valConsecutivoOrden, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valCodigoMoneda, string valNombreMoneda){
            string vSql = "";
            System.Data.DataTable vDt = new System.Data.DataTable();
            clsOrdenDeProduccionSql insOrdenDeProduccionSql = new clsOrdenDeProduccionSql();
            LibGalac.Aos.Base.ILibDataRpt insProduccionPorEstatusEntreFecha = new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionDat();
            vSql = insOrdenDeProduccionSql.SqlDetalleDeCostoDeProduccion(valConsecutivoCompania, valFechaInicial, valFechaFinal, valSeleccionarOrdenPor, valConsecutivoOrden,valMonedaDelInforme,valTasaDeCambio,valCodigoMoneda,valNombreMoneda );
            return insProduccionPorEstatusEntreFecha.GetDt(vSql, 0);
        }

        System.Data.DataTable IOrdenDeProduccionInformes.BuildDetalleDeCostoDeProduccionSalida(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eSeleccionarOrdenPor valSeleccionarOrdenPor, int valConsecutivoOrden, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
            string vSql = "";
            System.Data.DataTable vDt = new System.Data.DataTable();
            clsOrdenDeProduccionSql insOrdenDeProduccionSql = new clsOrdenDeProduccionSql();
            LibGalac.Aos.Base.ILibDataRpt insProduccionPorEstatusEntreFecha = new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionDat();
            vSql = insOrdenDeProduccionSql.SqlDetalleDeCostoDeProduccionSalida(valConsecutivoCompania, valFechaInicial, valFechaFinal, valSeleccionarOrdenPor, valConsecutivoOrden, valMonedaDelInforme, valTasaDeCambio,valCodigoMoneda, valNombreMoneda);
            return insProduccionPorEstatusEntreFecha.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados

    } //End of class clsOrdenDeProduccionRpt

} //End of namespace Galac.Adm.Brl. GestionProduccion

