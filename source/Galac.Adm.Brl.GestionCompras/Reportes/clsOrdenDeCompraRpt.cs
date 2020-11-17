using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Brl.GestionCompras.Reportes {

    public class clsOrdenDeCompraRpt: ILibReportInfo, IOrdenDeCompraInformes  {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsOrdenDeCompraRpt() {
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


        System.Data.DataTable IOrdenDeCompraInformes.BuildCompra(int valConsecutivoCompania, int valConsecutivoCompra)
        {
            string vSql = "";
            clsOrdenDeCompraSql insOrdenDeCompraSql = new clsOrdenDeCompraSql();
            LibGalac.Aos.Base.ILibDataRpt insOrdenDeCompras = new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraDat();
            vSql = insOrdenDeCompraSql.SqlCompra(valConsecutivoCompania, valConsecutivoCompra);
            return insOrdenDeCompras.GetDt(vSql, 0);
        }

        System.Data.DataTable IOrdenDeCompraInformes.BuildOrdenesDeCompras(int valConsecutivoCompania, DateTime FechaInicial, DateTime FechaFinal, eStatusDeOrdenDeCompra StatusDeOrdenDeCompra) {
            string vSql = "";
            clsOrdenDeCompraSql insOrdenDeCompraSql = new clsOrdenDeCompraSql();
            LibGalac.Aos.Base.ILibDataRpt insOrdenDeCompras = new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraDat();
            vSql = insOrdenDeCompraSql.ContruirSQLOrdenDeCompras(valConsecutivoCompania, FechaInicial, FechaFinal, StatusDeOrdenDeCompra);
            return insOrdenDeCompras.GetDt(vSql, 0);   
        }

        System.Data.DataTable IOrdenDeCompraInformes.BuildImprimirCotizacionOrdenDeCompra(int valConsecutivoCompania, string NumeroCotizacion) {
            string vSql = "";
            clsOrdenDeCompraSql insOrdenDeCompraSql = new clsOrdenDeCompraSql();
            LibGalac.Aos.Base.ILibDataRpt insOrdenDeCompras = new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraDat();
            vSql = insOrdenDeCompraSql.ConstruirSQLCotizacionOrdendeCompra(valConsecutivoCompania, NumeroCotizacion);
            return insOrdenDeCompras.GetDt(vSql, 0);   
        }

    } //End of class clsCompraRpt

} //End of namespace Galac.Adm.Brl.GestionCompras

