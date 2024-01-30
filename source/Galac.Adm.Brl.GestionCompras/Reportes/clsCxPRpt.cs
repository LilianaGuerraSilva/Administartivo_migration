using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Saw.Lib;
using System.Collections.ObjectModel;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using LibGalac.Aos.Base;

namespace Galac.Adm.Brl.GestionCompras.Reportes {

    public class clsCxPRpt: ILibReportInfo, ICxPInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCxPRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Cx P", CxPInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> CxPInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "dbo.Gp_CxPSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable ICxPInformes.BuildCxPEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eInformeStatusCXC_CXP valStatusCxP, eMonedaDelInformeMM valMonedaDelInforme, string valCodigoMoneda,string valNombreMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, bool valMostrarNroComprobanteContable) {
            string vSql = "";
            clsCxPSql insCxPSql = new clsCxPSql();
            LibGalac.Aos.Base.ILibDataRpt insCuentasPorPagarEntreFechas = new Galac.Adm.Dal.GestionCompras.clsCxPDat();
            vSql = insCxPSql.SqlCxPEntreFechas(valConsecutivoCompania, valFechaDesde, valFechaHasta, valStatusCxP, valMonedaDelInforme, valCodigoMoneda, valNombreMoneda, valTasaDeCambio, valMostrarNroComprobanteContable);
            return insCuentasPorPagarEntreFechas.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados

    } //End of class clsCxPRpt

} //End of namespace Galac..Brl.ComponenteNoEspecificado

