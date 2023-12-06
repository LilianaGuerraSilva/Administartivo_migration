using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Base;
using System.Xml.Linq;
using LibGalac.Aos.Brl;
using System.Collections.ObjectModel;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.Venta.Reportes {

    public class clsCxCRpt: ILibReportInfo, ICxCInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables

        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        private Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Comun.Brl.TablasGen.clsMonedaLocalActual();
        #endregion //Propiedades

        #region Constructores
        public clsCxCRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("CxC", CxCInfo());
            vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
        }
        #endregion //Constructores

        #region Metodos Generados
        private Dictionary<string, string> CxCInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Adm.Gp_CxCSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable ICxCInformes.BuildCxCPendientesEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio) {
            string vSql;
            clsCxCSql insCxCSql = new clsCxCSql();
            ILibDataRpt insCxCPendientesEntreFechas = new Dal.Venta.clsCXCDat();
            vSql = insCxCSql.SqlCxCPendientesEntreFechas(valConsecutivoCompania, valFechaDesde, valFechaHasta, valMonedaDelReporte, valTipoTasaDeCambio);
            return insCxCPendientesEntreFechas.GetDt(vSql, 0);
        }

        System.Data.DataTable ICxCInformes.BuildCxCPorCliente(int valConsecutivoCompania, string valCodigoDelCliente, string valZonaCobranza, DateTime valFechaDesde, DateTime valFechaHasta, eClientesOrdenadosPor valClientesOrdenadosPor, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio) {
            string vSql;
            clsCxCSql insCxCSql = new clsCxCSql();
            ILibDataRpt insCxCPorCliente = new Dal.Venta.clsCXCDat();
            vSql = insCxCSql.SqlCxCPorCliente(valConsecutivoCompania, valCodigoDelCliente, valZonaCobranza, valFechaDesde, valFechaHasta, valClientesOrdenadosPor, valMonedaDelReporte, valTipoTasaDeCambio);
            return insCxCPorCliente.GetDt(vSql, 0);
        }

        System.Data.DataTable ICxCInformes.BuildCxCEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eInformeStatusCXC_CXP valStatusCxC, eInformeAgruparPor valAgruparPor, string valZonaDeCobranza, string valSectorDeNegocio, eMonedaDelInformeMM valMonedaDelInforme, string valMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, bool valMostrarNroComprobanteContable) {
            string vSql = "";
            clsCxCSql insCxCSql = new clsCxCSql();
            ILibDataRpt insCxCEntreFechas = new Dal.Venta.clsCXCDat();
            vSql = insCxCSql.SqlCxCEntreFechas(valConsecutivoCompania, valFechaDesde, valFechaHasta, valStatusCxC, valAgruparPor, valZonaDeCobranza, valSectorDeNegocio, valMonedaDelInforme, valMoneda, valTasaDeCambio, valMostrarNroComprobanteContable);
            return insCxCEntreFechas.GetDt(vSql, 0);
        }

        ObservableCollection<string> ICxCInformes.ListaDeSectoresDeNegocioParaInformes() {
            ObservableCollection<string> vResult = new ObservableCollection<string>();
            vResult.Add("TODOS");
            string vSql = "SELECT Descripcion FROM Comun.SectorDeNegocio WHERE Descripcion <> '' ORDER BY Descripcion";
            XElement vResultSet = LibBusiness.ExecuteSelect(vSql, null, "", 0);
            if (vResultSet != null) {
                var vEntity = from vRecord in vResultSet.Descendants("GpResult") select vRecord;
                foreach (XElement vItem in vEntity) {
                    if (vItem != null) {
                        vResult.Add(LibConvert.ToStr(vItem.Element("Descripcion").Value));
                    }
                }
            }
            return vResult;
        }

        ObservableCollection<string> ICxCInformes.ListaDeZonasDeCobranzaParaInformes() {
            ObservableCollection<string> vResult = new ObservableCollection<string>();
            vResult.Add("TODAS");
            string vSql = "SELECT Nombre FROM Saw.ZonaCobranza WHERE ConsecutivoCompania = " + LibConvert.ToStr(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania")) + " AND Nombre <> '' ORDER BY Nombre";
            XElement vResultSet = LibBusiness.ExecuteSelect(vSql, null, "", 0);
            if (vResultSet != null) {
                var vEntity = from vRecord in vResultSet.Descendants("GpResult") select vRecord;
                foreach (XElement vItem in vEntity) {
                    if (vItem != null) {
                        vResult.Add(LibConvert.ToStr(vItem.Element("Nombre").Value));
                    }
                }
            }
            return vResult;
        }

        ObservableCollection<string> ICxCInformes.ListaDeMonedasActivasParaInformes() {
            ObservableCollection<string> vResult = new ObservableCollection<string>();
            string vSql = "SELECT Codigo, Nombre FROM Moneda WHERE Activa = 'S' AND Nombre NOT LIKE '%bolívar%' ORDER BY Codigo DESC";
            XElement vResultSet = LibBusiness.ExecuteSelect(vSql, null, "", 0);
            if (vResultSet != null) {
                var vEntity = from vRecord in vResultSet.Descendants("GpResult") select vRecord;
                string vNewItem;
                foreach (XElement vItem in vEntity) {
                    if (vItem != null) {
                        vNewItem = "(" + LibConvert.ToStr(vItem.Element("Codigo").Value) + ") " + LibConvert.ToStr(vItem.Element("Nombre").Value);
                        vResult.Add(vNewItem);
                    }
                }
            }
            return vResult;
        }

        #endregion //Metodos Generados
    } //End of class clsCxCRpt
} //End of namespace Galac.Adm.Brl.Venta
