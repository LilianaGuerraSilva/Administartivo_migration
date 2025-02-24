using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Brl.Venta.Reportes {

    public class clsEscaladaRpt: ILibReportInfo, IEscaladaInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsEscaladaRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Escalada", EscaladaInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> EscaladaInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "dbo.Gp_EscaladaSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable IEscaladaInformes.BuildFacturacionEntreFechasVerificacion(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal) {
            string vSql = "";
            clsEscaladaSql insEscaladaSql = new clsEscaladaSql();
            LibGalac.Aos.Base.ILibDataRpt insFacturacionEntreFechasVerificacion = new Dal.Venta.clsEscaladaDat();
            vSql = insEscaladaSql.SqlFacturacionEntreFechasVerificacion(valConsecutivoCompania, valFechaInicial, valFechaFinal);
            return insFacturacionEntreFechasVerificacion.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados


    } //End of class clsEscaladaRpt

} //End of namespace Galac.Adm.Brl.Venta

