using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;

namespace Galac.Adm.Brl.Banco.Reportes {

    public class clsCuentaBancariaRpt: ILibReportInfo {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCuentaBancariaRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Cuenta Bancaria", CuentaBancariaInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> CuentaBancariaInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Saw.Gp_CuentaBancariaSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCuentaBancariaRpt

} //End of namespace Galac.Dbo.Brl.CuentaBancaria

