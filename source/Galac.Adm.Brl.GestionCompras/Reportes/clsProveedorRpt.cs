using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.GestionCompras.Reportes {

    public class clsProveedorRpt: ILibReportInfo, IProveedorInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsProveedorRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Proveedor", ProveedorInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> ProveedorInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Adm.Gp_ProveedorSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable IProveedorInformes.BuildHistoricoDeProveedor(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoProveedor, eMonedaDelInformeMM valMonedaDelInforme, string valCodigoMoneda, string valNombreMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, eProveedorOrdenadosPor valProveedorOrdenarPor) {
            string vSql = "";
            clsProveedorSql insProveedorSql = new clsProveedorSql();
            LibGalac.Aos.Base.ILibDataRpt insHistoricoDeProveedor = new Dal.GestionCompras.clsProveedorDat();
            vSql = insProveedorSql.SqlHistoricoDeProveedor(valConsecutivoCompania, valFechaDesde, valFechaHasta, valCodigoProveedor, valMonedaDelInforme, valCodigoMoneda, valNombreMoneda, valTasaDeCambio, valProveedorOrdenarPor);
            return insHistoricoDeProveedor.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados


    } //End of class clsProveedorRpt

} //End of namespace Galac.Adm.Brl.GestionCompras

