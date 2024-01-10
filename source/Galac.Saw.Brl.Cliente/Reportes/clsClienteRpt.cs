using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Ccl.Cliente;
using Galac.Saw.Lib;

namespace Galac.Saw.Brl.Cliente.Reportes {

    public class clsClienteRpt: ILibReportInfo, IClienteInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsClienteRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Cliente", ClienteInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> ClienteInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Saw.Gp_ClienteSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable IClienteInformes.BuildHistoricoDeCliente(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoCliente, eMonedaDelInformeMM valMonedaDelInforme, string valCodigoMoneda, string valNombreMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, eClientesOrdenadosPor valClienteOrdenarPor) {
            string vSql = "";
            clsClienteSql insClienteSql = new clsClienteSql();
            LibGalac.Aos.Base.ILibDataRpt insHistoricoDeCliente = new Galac.Saw.Dal.Cliente.clsClienteDat();
            vSql = insClienteSql.SqlHistoricoDeCliente(valConsecutivoCompania, valFechaDesde, valFechaHasta, valCodigoCliente, valMonedaDelInforme, valCodigoMoneda, valNombreMoneda, valTasaDeCambio, valClienteOrdenarPor);
            return insHistoricoDeCliente.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados


    } //End of class clsClienteRpt

} //End of namespace Galac.Saw.Brl.Cliente

