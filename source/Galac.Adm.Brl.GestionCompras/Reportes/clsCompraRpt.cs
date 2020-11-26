using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.GestionCompras;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Brl.GestionCompras.Reportes {

    public class clsCompraRpt: ILibReportInfo, ICompraInformes  {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCompraRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Compra", CompraInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> CompraInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Adm.Gp_CompraSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }
        #endregion //Metodos Generados



        System.Data.DataTable ICompraInformes.BuildCompra(int valConsecutivoCompania, int valConsecutivoCompra)
        {
            string vSql = "";
            clsCompraSql insCompraSql = new clsCompraSql();
            LibGalac.Aos.Base.ILibDataRpt insCompras = new Galac.Adm.Dal.GestionCompras.clsCompraDat();
            vSql = insCompraSql.SqlCompra(valConsecutivoCompania, valConsecutivoCompra);
            return insCompras.GetDt(vSql, 0);
        }
        System.Data.DataTable ICompraInformes.BuildCompraEntreFechas(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, bool valCambioOriginal, bool valMostrarComprasAnuladas, bool valMuestraDetalle, eMonedaParaImpresion MonedaParaImpresion)
        {
            string vSql = "";
            clsCompraSql insCompraSql = new clsCompraSql();
            LibGalac.Aos.Base.ILibDataRpt insCompras = new Galac.Adm.Dal.GestionCompras.clsCompraDat();
            vSql = insCompraSql.SqlImprimirCompra(valConsecutivoCompania, valFechaInicial, valFechaFinal, valCambioOriginal, valMostrarComprasAnuladas, valMuestraDetalle, MonedaParaImpresion);
            return insCompras.GetDt(vSql, 0);
        }
        System.Data.DataTable ICompraInformes.BuildImprimirCostoDeCompraEntreFechas(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eReporteCostoDeCompras valLineasDeProductoCantidadAImprimir, string valCodigoProducto) {
            string vSql = "";
            clsCompraSql insCompraSql = new clsCompraSql();
            LibGalac.Aos.Base.ILibDataRpt insCompras = new Galac.Adm.Dal.GestionCompras.clsCompraDat();
            vSql = insCompraSql.ConstruirSQLCostoDeComprasEntreFechas(valConsecutivoCompania, valFechaInicial, valFechaFinal, valLineasDeProductoCantidadAImprimir, valCodigoProducto);
            return insCompras.GetDt(vSql, 0);
        }
        System.Data.DataTable ICompraInformes.BuildImprimirHistoricoDeCompras(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eReporteCostoDeCompras valLineasDeProductoCantidadAImprimir, string valCodigoProducto) {
            string vSql = "";
            clsCompraSql insCompraSql = new clsCompraSql();
            LibGalac.Aos.Base.ILibDataRpt insCompras = new Galac.Adm.Dal.GestionCompras.clsCompraDat();
            vSql = insCompraSql.ContruirSQLHistoricoCompras(valConsecutivoCompania, valFechaInicial, valFechaFinal, valLineasDeProductoCantidadAImprimir, valCodigoProducto);
            return insCompras.GetDt(vSql, 0);
        }
        System.Data.DataTable ICompraInformes.BuildImprimirMargenSobreCostoPromedioDeCompra(int valConsecutivoCompania, eNivelDePrecio valNivelDePrecio, eReporteCostoDeCompras valLineasDeProductoCantidadAImprimir, string valCodigoProducto) {
            string vSql = "";
            clsCompraSql insCompraSql = new clsCompraSql();
            LibGalac.Aos.Base.ILibDataRpt insCompras = new Galac.Adm.Dal.GestionCompras.clsCompraDat();
            vSql = insCompraSql.ConstruirSQLMargenSobreCostoPromedioDeComp(valConsecutivoCompania, valNivelDePrecio, valLineasDeProductoCantidadAImprimir, valCodigoProducto);
            return insCompras.GetDt(vSql, 0);
        }
        System.Data.DataTable ICompraInformes.BuildImpresionDeComprasEtiquetas(int valConsecutivoCompania, eNivelDePrecio valNivelDePrecio, string valNumero) {
            string vSql = "";
            clsCompraSql insCompraSql = new clsCompraSql();
            LibGalac.Aos.Base.ILibDataRpt insCompras = new Galac.Adm.Dal.GestionCompras.clsCompraDat();
            vSql = insCompraSql.ConstruirSQLImpresionDeComprasEtiquetas(valConsecutivoCompania, valNivelDePrecio, valNumero);
            return insCompras.GetDt(vSql, 0);
        }
    } //End of class clsCompraRpt

} //End of namespace Galac.Adm.Brl.GestionCompras

