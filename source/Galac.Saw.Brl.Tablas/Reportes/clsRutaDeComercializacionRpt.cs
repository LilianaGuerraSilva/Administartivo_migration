using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Brl.Tablas.Reportes {

    public class clsRutaDeComercializacionRpt: ILibReportInfo {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsRutaDeComercializacionRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Ruta de Comercialización", RutaDeComercializacionInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> RutaDeComercializacionInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Saw.Gp_RutaDeComercializacionSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsRutaDeComercializacionRpt

} //End of namespace Galac.Saw.Brl.Tablas

