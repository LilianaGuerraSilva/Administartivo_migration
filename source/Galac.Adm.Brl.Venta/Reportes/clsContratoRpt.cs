using System.Collections.Generic;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using System;

namespace Galac.Adm.Brl.Venta.Reportes {

    public class clsContratoRpt : ILibReportInfo, IContratoInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsContratoRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Contrato", ContratoInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> ContratoInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "dbo.Gp_ContratoSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable IContratoInformes.BuildContratoPorNumero(int valConsecutivoCompania, string valNumeroContrato) {
            string vSql = "";
            clsContratoSql insContratoSql = new clsContratoSql();
            LibGalac.Aos.Base.ILibDataRpt insInformeDeContrato = new Dal.Venta.clsContratoDat();
            vSql = insContratoSql.SqlContratoPorNumero(valConsecutivoCompania, valNumeroContrato);
            return insInformeDeContrato.GetDt(vSql, 0);
        }

        System.Data.DataTable IContratoInformes.BuildContratoEntreFechas(int valConsecutivoCompania, bool valFiltrarPorStatus, bool valFiltrarPorFechaFinal, DateTime valFechaInicio, DateTime valFechaFinal, eStatusContrato valStatusContrato) {
            string vSql = "";
            clsContratoSql insContratoSql = new clsContratoSql();
            LibGalac.Aos.Base.ILibDataRpt insContratoEntreFechas = new Dal.Venta.clsContratoDat();
            vSql = insContratoSql.SqlContratoEntreFechas(valConsecutivoCompania, valFiltrarPorStatus, valFiltrarPorFechaFinal, valFechaInicio, valFechaFinal, valStatusContrato);
            return insContratoEntreFechas.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados
    } //End of class clsContratoRpt
} //End of namespace Galac.Dbo.Brl.Venta

