using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Base;

namespace Galac.Adm.Brl.Venta.Reportes {

    public class clsResumenDiarioDeVentasRpt: ILibReportInfo, IResumenDiarioDeVentasInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables

        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades

        #region Constructores
        public clsResumenDiarioDeVentasRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Factura", FacturaInfo());
        }
        #endregion //Constructores

        #region Metodos Generados
        private Dictionary<string, string> FacturaInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Adm.Gp_FacturaSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable IResumenDiarioDeVentasInformes.BuildResumenDiarioDeVentasEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, bool valAgruparPorMaquinaFiscal, string valConsecutivoMaquinaFiscal) {
            string vSql = "";
            clsResumenDiarioDeVentasSql insResumenDiarioDeVentasSql = new clsResumenDiarioDeVentasSql();
            ILibDataRpt insResumenDiarioDeVentasEntreFechas = new Dal.Venta.clsFacturaDat();
            vSql = insResumenDiarioDeVentasSql.SqlResumenDiarioDeVentasEntreFechas(valConsecutivoCompania, valFechaDesde, valFechaHasta, valAgruparPorMaquinaFiscal, valConsecutivoMaquinaFiscal);
            return insResumenDiarioDeVentasEntreFechas.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados

    } //End of class clsFacturaRpt

} //End of namespace Galac.Adm.Brl.Venta

