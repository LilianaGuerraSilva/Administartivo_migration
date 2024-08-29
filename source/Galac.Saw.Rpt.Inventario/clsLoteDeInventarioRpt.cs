using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Saw.Ccl.Inventario;
namespace Galac.Saw.Rpt.Inventario {

    public class clsLoteDeInventarioRpt : ILibReportInfo {

        #region variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion variables

        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion Propiedades
        #region Constructores
        public clsLoteDeInventarioRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Lote de Inventario", LoteDeInventarioInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> LoteDeInventarioInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Adm.Gp_LoteDeInventarioSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class clsLoteDeInventarioRpt
} //End of namespace Galac.Saw.Rpt.Inventario

