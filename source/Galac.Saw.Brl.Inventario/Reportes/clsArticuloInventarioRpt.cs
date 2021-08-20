using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Reconv;

namespace Galac.Saw.Brl.Inventario.Reportes {

    public class clsArticuloInventarioRpt : ILibReportInfo, IArticuloInventarioInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsArticuloInventarioRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Artículo Inventario", ArticuloInventarioInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> ArticuloInventarioInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Saw.Gp_ArticuloInventarioSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable IArticuloInventarioInformes.BuildListadoDePrecios(int valConsecutivoCompania, string valNombreLineaDeProducto, eMonedaPresentacionDeReporteRM valTipoMonedaReporte, decimal valTasaCambio) {
            string vSql = "";
            if (LibString.IsNullOrEmpty(valNombreLineaDeProducto,true)) {
                valNombreLineaDeProducto = string.Empty;
            }
            clsArticuloInventarioSql insArticuloInventarioSql = new clsArticuloInventarioSql();
            LibGalac.Aos.Base.ILibDataRpt insListadoDePrecios = new Galac.Saw.Dal.Inventario.clsArticuloInventarioDat();
            if (valTipoMonedaReporte == eMonedaPresentacionDeReporteRM.EnMonedaExtranjera) {
                if (LibDate.Today() >= clsUtilReconv.GetFechaReconversion()) {
                    vSql = insArticuloInventarioSql.SqlListadoDePreciosME(valConsecutivoCompania, valNombreLineaDeProducto, valTasaCambio);
                } else if (LibDate.Today() >= clsUtilReconv.GetFechaDisposicionesTransitorias()) {
                    vSql = insArticuloInventarioSql.SqlListadoDePreciosBsDME(valConsecutivoCompania, valNombreLineaDeProducto, valTasaCambio);
                }
            } else {
                if (LibDate.Today() >= clsUtilReconv.GetFechaReconversion()) {
                    vSql = insArticuloInventarioSql.SqlListadoDePrecios(valConsecutivoCompania, valNombreLineaDeProducto);
                } else if (LibDate.Today() >= clsUtilReconv.GetFechaDisposicionesTransitorias()) {
                    vSql = insArticuloInventarioSql.SqlListadoDePreciosBsD(valConsecutivoCompania, valNombreLineaDeProducto);
                }
            }
            return insListadoDePrecios.GetDt(vSql, 0);
        }

        System.Data.DataTable IArticuloInventarioInformes.BuildListdoDeArticulosBalanza(int valConsecutivoCompania, string valLineaDeProducto, bool valFiltrarPorLineaDeProducto) {
            string vSql = "";
            clsArticuloInventarioSql insArticuloInventarioSql = new clsArticuloInventarioSql();
            LibGalac.Aos.Base.ILibDataRpt insListarArticulosBalanza = new Galac.Saw.Dal.Inventario.clsArticuloInventarioDat();
            vSql = insArticuloInventarioSql.SqlListdoDeArticulosBalanza(valConsecutivoCompania, valLineaDeProducto, valFiltrarPorLineaDeProducto);
            return insListarArticulosBalanza.GetDt(vSql, 0);
        }

        System.Data.DataTable IArticuloInventarioInformes.BuildValoracionDeInventario(int valConsecutivoCompania, string valCodigoDesde, string valCodigoHasta, string valLineaDeProducto, decimal valCambioMoneda, bool valUsaPrecioConIva) {
            string vSql = "";
            clsArticuloInventarioSql insArticuloInventarioSql = new clsArticuloInventarioSql();
            LibGalac.Aos.Base.ILibDataRpt insValoracionDeInventario = new Galac.Saw.Dal.Inventario.clsArticuloInventarioDat();
            vSql = insArticuloInventarioSql.SqlValoracionDeInventario(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valLineaDeProducto, valCambioMoneda, valUsaPrecioConIva);
            return insValoracionDeInventario.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados
    } //End of class clsArticuloInventarioRpt
} //End of namespace Galac.Saw.Brl.Inventario