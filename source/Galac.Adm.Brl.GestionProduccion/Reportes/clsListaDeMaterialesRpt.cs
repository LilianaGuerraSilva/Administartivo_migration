using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Brl.GestionProduccion.Reportes {

    public class clsListaDeMaterialesRpt: ILibReportInfo, IListaDeMaterialesInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsListaDeMaterialesRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Lista de Materiales", ListaDeMaterialesInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> ListaDeMaterialesInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Adm.Gp_ListaDeMaterialesSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable IListaDeMaterialesInformes.BuildListaDeMaterialesDeInventarioAProducir(int valConsecutivoCompania, string valCodigoInventarioAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir) {
            string vSql = "";
            clsListaDeMaterialesSql insListaDeMaterialesSql = new clsListaDeMaterialesSql();
            LibGalac.Aos.Base.ILibDataRpt insListaDeMaterialesDeInventarioAProducir = new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesDat();
            vSql = insListaDeMaterialesSql.SqlListaDeMaterialesDeInventarioAProducir(valConsecutivoCompania, valCodigoInventarioAProducir, valCantidadAImprimir, valCantidadAProducir);
            return insListaDeMaterialesDeInventarioAProducir.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados

    } //End of class clsListaDeMaterialesRpt

} //End of namespace Galac.Adm.Brl.GestionProduccion

