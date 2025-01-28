using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Brl.Venta.Reportes {

    public class clsFacturaRpt: ILibReportInfo, IFacturaInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables

        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades

        #region Constructores
        public clsFacturaRpt() {
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

        System.Data.DataTable IFacturaInformes.BuildFacturacionPorArticulo(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoDelArticulo, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio, bool valIsInformeDetallado) {
            string vSql;
            clsFacturaSql insFacturaSql = new clsFacturaSql();
            ILibDataRpt insFacturacionPorArticulo = new Dal.Venta.clsFacturaDat();
            vSql = insFacturaSql.SqlFacturacionPorArticulo(valConsecutivoCompania, valFechaDesde, valFechaHasta, valCodigoDelArticulo, valMonedaDelReporte, valTipoTasaDeCambio, valIsInformeDetallado);
            return insFacturacionPorArticulo.GetDt(vSql, 0);
        }

        System.Data.DataTable IFacturaInformes.BuildFacturacionPorUsuario(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valNombreOperador, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio, bool valIsInformeDetallado) {
            string vSql;
            clsFacturaSql insFacturaSql = new clsFacturaSql();
            ILibDataRpt insFacturacionPorUsuario = new Dal.Venta.clsFacturaDat();
            vSql = insFacturaSql.SqlFacturacionPorUsuario(valConsecutivoCompania, valFechaDesde, valFechaHasta, valNombreOperador, valMonedaDelReporte, valTipoTasaDeCambio, valIsInformeDetallado);
            return insFacturacionPorUsuario.GetDt(vSql, 0);
        }

        System.Data.DataTable IFacturaInformes.BuildFacturaBorradorGenerico(int valConsecutivoCompania, string valNumeroDocumento, eTipoDocumentoFactura valTipoDocumento, eStatusFactura valStatusDocumento, eTalonario valTalonario, eFormaDeOrdenarDetalleFactura valFormaDeOrdenarDetalleFactura, bool valImprimirFacturaConSubtotalesPorLineaDeProducto, string valCiudadCompania, string valNombreOperador) {
            string vSql = "";
            clsFacturaSql insFacturaSql = new clsFacturaSql();
            ILibDataRpt insFacturaBorradorGenerico = new Dal.Venta.clsFacturaDat();
            vSql = insFacturaSql.SqlFacturaBorradorGenerico(valConsecutivoCompania, valNumeroDocumento, valTipoDocumento, valStatusDocumento, valTalonario, eFormaDeOrdenarDetalleFactura.Comofuecargada, false, valCiudadCompania, valNombreOperador);
            return insFacturaBorradorGenerico.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados

    } //End of class clsFacturaRpt

} //End of namespace Galac.Adm.Brl.Venta

