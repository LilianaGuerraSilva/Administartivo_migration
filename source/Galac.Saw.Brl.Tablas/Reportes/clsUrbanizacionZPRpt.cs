using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;

namespace Galac.Saw.Brl.Tablas.Reportes {

    public class clsUrbanizacionZPRpt: ILibReportInfo {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsUrbanizacionZPRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Urbanización - Zona Postal", UrbanizacionZPInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> UrbanizacionZPInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Saw.Gp_UrbanizacionZPSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsUrbanizacionZPRpt

} //End of namespace Galac.Saw.Brl.Tablas

