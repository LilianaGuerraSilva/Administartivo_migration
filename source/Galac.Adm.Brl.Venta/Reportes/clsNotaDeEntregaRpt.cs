using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using System.Data;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.Venta.Reportes {

    public class clsNotaDeEntregaRpt: ILibReportInfo, INotaDeEntregaInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsNotaDeEntregaRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Nota De Entrega", NotaDeEntregaInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> NotaDeEntregaInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "dbo.Gp_NotaDeEntregaSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

       DataTable INotaDeEntregaInformes.BuildNotaDeEntregaEntreFechasPorCliente(int valConsecutivoCompania, DateTime valtFechaDesde, DateTime valFechaHasta, bool valIncluirNotasDeEntregasAnuladas, eCantidadAImprimir valCantidadAImprimir, string valCodigoCliente) {
            string vSql = "";
            clsNotaDeEntregaSql insNotaDeEntregaSql = new clsNotaDeEntregaSql();
            LibGalac.Aos.Base.ILibDataRpt insNotaDeEntregaEntreFechasPorCliente = new Galac.Adm.Dal.Venta.clsFacturaRapidaDat();
            vSql = insNotaDeEntregaSql.SqlNotaDeEntregaEntreFechasPorCliente(valConsecutivoCompania, valtFechaDesde, valFechaHasta, valIncluirNotasDeEntregasAnuladas, valCantidadAImprimir,  valCodigoCliente);
            return insNotaDeEntregaEntreFechasPorCliente.GetDt(vSql, 0);
        }
		
		DataTable INotaDeEntregaInformes.BuildNotaDeEntregaEntreFechasPorClienteDetallado(int valConsecutivoCompania, DateTime valtFechaDesde, DateTime valFechaHasta, eCantidadAImprimir valCantidadAImprimir, string valCodigoCliente) {
            string vSql = "";
            clsNotaDeEntregaSql insNotaDeEntregaSql = new clsNotaDeEntregaSql();
            LibGalac.Aos.Base.ILibDataRpt insNotaDeEntregaEntreFechasPorCliente = new Galac.Adm.Dal.Venta.clsFacturaRapidaDat();
            vSql = insNotaDeEntregaSql.SqlNotaDeEntregaEntreFechasPorClienteDetallado(valConsecutivoCompania, valtFechaDesde, valFechaHasta, valCantidadAImprimir, valCodigoCliente);
            return insNotaDeEntregaEntreFechasPorCliente.GetDt(vSql, 0);
        }        
        #endregion //Metodos Generados


    } //End of class clsNotaDeEntregaRpt

} //End of namespace Galac..Brl.ComponenteNoEspecificado

