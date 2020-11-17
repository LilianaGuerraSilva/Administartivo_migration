using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
//using Galac.Dbo.Ccl.CajaChica;

namespace Galac.Adm.Brl.Banco.Reportes {

    public class clsConceptoBancarioRpt: ILibReportInfo {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsConceptoBancarioRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Concepto Bancario", ConceptoBancarioInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> ConceptoBancarioInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Adm.Gp_ConceptoBancarioSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsConceptoBancarioRpt

} //End of namespace Galac.Dbo.Brl.CajaChica

