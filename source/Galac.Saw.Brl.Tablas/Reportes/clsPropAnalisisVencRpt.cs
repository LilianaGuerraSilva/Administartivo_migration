using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Brl.Tablas.Reportes {

    public class clsPropAnalisisVencRpt: ILibReportInfo {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsPropAnalisisVencRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Prop Analisis Venc", PropAnalisisVencInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> PropAnalisisVencInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Saw.Gp_PropAnalisisVencSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsPropAnalisisVencRpt

} //End of namespace Galac.Saw.Brl.Tablas

