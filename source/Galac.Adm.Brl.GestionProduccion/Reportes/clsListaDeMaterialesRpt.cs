using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.GestionProduccion.Reportes {

    public class clsListaDeMaterialesRpt : ILibReportInfo, IListaDeMaterialesInformes {
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

        System.Data.DataTable IListaDeMaterialesInformes.BuildListaDeMaterialesSalida(int valConsecutivoCompania, string valCodigoListaAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir, string valMonedaDelInformeMM, decimal valTasaDeCambio, string[] valListaMoneda) {
            string vSql = "";
            clsListaDeMaterialesSql insListaDeMaterialesSql = new clsListaDeMaterialesSql();
            LibGalac.Aos.Base.ILibDataRpt insListaDeMaterialesDeInventarioAProducir = new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesDat();
            vSql = insListaDeMaterialesSql.SqlListaDeMaterialesSalida(valConsecutivoCompania, valCodigoListaAProducir, valCantidadAImprimir, valCantidadAProducir, valMonedaDelInformeMM, valTasaDeCambio, valListaMoneda);
            return insListaDeMaterialesDeInventarioAProducir.GetDt(vSql, 0);
        }

        System.Data.DataTable IListaDeMaterialesInformes.BuildListaDeMaterialesInsumos(int valConsecutivoCompania, string valCodigoListaAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir, string valMonedaDelInformeMM, decimal valTasaDeCambio, string[] valListaMoneda) {
            string vSql = "";
            clsListaDeMaterialesSql insListaDeMaterialesSql = new clsListaDeMaterialesSql();
            LibGalac.Aos.Base.ILibDataRpt insListaDeMaterialesDeInventarioAProducir = new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesDat();
            vSql = insListaDeMaterialesSql.SqlListaDeMaterialesInsumos(valConsecutivoCompania, valCodigoListaAProducir, valCantidadAImprimir, valCantidadAProducir, valMonedaDelInformeMM, valTasaDeCambio, valListaMoneda);
            vSql += " ORDER BY ListaDeMateriales.Codigo,ListaDeMateriales.FechaCreacion";
            return insListaDeMaterialesDeInventarioAProducir.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados
    } //End of class clsListaDeMaterialesRpt
} //End of namespace Galac.Adm.Brl.GestionProduccion

