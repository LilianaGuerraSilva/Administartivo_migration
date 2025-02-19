using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Ccl.Tablas.Reportes {

    public class clsOtrosCargosDeFacturaRpt: ILibReportInfo {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsOtrosCargosDeFacturaRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Otros Cargos de Factura", OtrosCargosDeFacturaInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> OtrosCargosDeFacturaInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "dbo.Gp_OtrosCargosDeFacturaSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsOtrosCargosDeFacturaRpt

} //End of namespace Galac.Saw.Ccl.Tablas

