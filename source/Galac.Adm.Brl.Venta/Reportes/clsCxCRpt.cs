using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Brl.Venta.Reportes {

    public class clsCxCRpt: ILibReportInfo, ICxCInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }

        private Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
        #endregion //Propiedades
        #region Constructores

        public clsCxCRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("CxC", CxCInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> CxCInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Adm.Gp_CxCSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable ICxCInformes.BuildCxCPendientesEntreFechas(int valConsecutivoCompania, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, DateTime valFechaDesde, DateTime valFechaHasta, bool valUsaCantacto) {
            string vSql = "";
            clsCxCSql insCxCSql = new clsCxCSql();
            LibGalac.Aos.Base.ILibDataRpt insCxCPendientesEntreFechas = new Galac.Adm.Dal.Venta.clsCXCDat();
            string vCodigoMonedaLocal = vMonedaLocal.GetHoyCodigoMoneda();

            vSql = insCxCSql.SqlCxCPendientesEntreFechas(valConsecutivoCompania, valMonedaDelReporte, valFechaDesde, valFechaHasta,valUsaCantacto);
            return insCxCPendientesEntreFechas.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados


    } //End of class clsCxCRpt

} //End of namespace Galac.Adm.Brl.Venta

