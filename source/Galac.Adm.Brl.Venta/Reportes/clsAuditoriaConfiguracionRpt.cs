using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Brl.Venta.Reportes {

    public class clsAuditoriaConfiguracionRpt: ILibReportInfo, IAuditoriaConfiguracionInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsAuditoriaConfiguracionRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Auditoria Configuracion", AuditoriaConfiguracionInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> AuditoriaConfiguracionInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "adm.Gp_AuditoriaConfiguracionSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable IAuditoriaConfiguracionInformes.BuildAuditoriaConfiguracionDeCaja(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta) {
            string vSql = "";
            clsAuditoriaConfiguracionSql insAuditoriaConfiguracionSql = new clsAuditoriaConfiguracionSql();
            LibGalac.Aos.Base.ILibDataRpt insAuditoriaConfiguracionDeCaja = new Galac.Adm.Dal.Venta.clsCajaDat();
            vSql = insAuditoriaConfiguracionSql.SqlAuditoriaConfiguracionDeCaja(valConsecutivoCompania, valFechaDesde, valFechaHasta);
            return insAuditoriaConfiguracionDeCaja.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados


    } //End of class clsAuditoriaConfiguracionRpt

} //End of namespace Galac.Saw.Brl.Venta

