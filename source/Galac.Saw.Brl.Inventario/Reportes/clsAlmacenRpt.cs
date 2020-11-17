using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;

namespace Galac.Saw.Brl.Inventario.Reportes {

    public class clsAlmacenRpt: ILibReportInfo {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsAlmacenRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Almacén", AlmacenInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> AlmacenInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Saw.Gp_AlmacenSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsAlmacenRpt

} //End of namespace Galac.Saw.Brl.Inventario

