using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Cib;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.CajaChica;
using Galac.Adm.Dal.CajaChica;

namespace Galac.Adm.Brl.CajaChica.Reportes {

    public class clsRendicionRpt: ILibReportInfo, IRendicionInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsRendicionRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Reposición de Caja Chica", RendicionInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> RendicionInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Adm.Gp_RendicionSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable IRendicionInformes.BuildReposicionesEntreFechas(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, bool valImprimeUna, string valCodigoCtaBancariaCajaChica) {
            string vSql = "";
            clsRendicionSql insRendicionSql = new clsRendicionSql();
            LibGalac.Aos.Base.ILibDataRpt insReposicionesEntreFechas = new Galac.Adm.Dal.CajaChica.clsRendicionDat();
            vSql = insRendicionSql.SqlReposicionesEntreFechas(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimeUna, valCodigoCtaBancariaCajaChica);
            return insReposicionesEntreFechas.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados


    } //End of class clsRendicionRpt

} //End of namespace Galac.Adm.Brl.CajaChica