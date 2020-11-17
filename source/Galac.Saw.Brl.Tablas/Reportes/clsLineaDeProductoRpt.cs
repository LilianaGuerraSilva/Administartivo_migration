using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Brl.Tablas.Reportes {

    public class clsLineaDeProductoRpt: ILibReportInfo {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsLineaDeProductoRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Línea de Producto", LineaDeProductoInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> LineaDeProductoInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Adm.Gp_LineaDeProductoSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsLineaDeProductoRpt

} //End of namespace Galac.Saw.Brl.Tablas

