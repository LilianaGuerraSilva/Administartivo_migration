using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Base;
using System.Xml.Linq;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Brl.Venta.Reportes {

    public class clsCobranzaRpt: ILibReportInfo, ICobranzaInformes {
        #region Variables
        private Dictionary<string,Dictionary<string,string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string,Dictionary<string,string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        private Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
        #endregion //Propiedades
        #region Constructores

        public clsCobranzaRpt() {
            _PropertiesForReportList = new Dictionary<string,Dictionary<string,string>>();
            _PropertiesForReportList.Add("Cobranza",CobranzaInfo());
            vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string,string> CobranzaInfo() {
            Dictionary<string,string> vResult = new Dictionary<string,string>();
            vResult.Add("SpSearchName","Dbo.Gp_CobranzaSCH");
            vResult.Add("ConfigKeyForDbService",string.Empty);
            return vResult;
        }

        System.Data.DataTable ICobranzaInformes.BuildCobranzasEntreFechas(int valConsecutivoCompania,Saw.Lib.eMonedaParaImpresion valMonedaReporte,Galac.Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio,decimal valTasaDeCambio,DateTime valFechaDesde,DateTime valFechaHasta,string valNombreCobrador,string valNombreCliente,string valNombreCuentaBancaria,eFiltrarCobranzasPor valFiltrarCobranzasPor,bool valAgrupado,bool valUsaVentasConIvaDiferidos) {
            string vSql = "";
            clsCobranzaSql insCobranzaSql = new clsCobranzaSql();
            LibGalac.Aos.Base.ILibDataRpt insCobranzasEntreFechas = new Galac.Adm.Dal.Venta.clsCobranzaDat();
            vSql = insCobranzaSql.SqlCobranzasEntreFechas(valConsecutivoCompania,valMonedaReporte,valTipoTasaDeCambio,valTasaDeCambio,valFechaDesde,valFechaHasta,valNombreCobrador,valNombreCliente,valNombreCuentaBancaria,valFiltrarCobranzasPor,valAgrupado,valUsaVentasConIvaDiferidos);
            return insCobranzasEntreFechas.GetDt(vSql,0);
        }
        System.Data.DataTable ICobranzaInformes.BuildComisionDeVendedoresPorCobranzaMonto(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eTipoDeInforme valTipoDeInforme, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTasaDeCambioParaImpresion valTasaDeCambioImpresion, Saw.Lib.eCantidadAImprimir valCantidadAImprimir, string valCodigoVendedor) {
            string vSql = "";
            clsCobranzaSql insCobranzaSql = new clsCobranzaSql();
            LibGalac.Aos.Base.ILibDataRpt insComisionDeVendedoresPorCobranzaMonto = new Galac.Adm.Dal.Venta.clsCobranzaDat();
            string vCodigoMonedaLocal = vMonedaLocal.GetHoyCodigoMoneda();
            vSql = insCobranzaSql.SqlComisionDeVendedoresPorCobranzaMonto(valConsecutivoCompania, valFechaInicial, valFechaFinal, valTipoDeInforme,valMonedaDeReporte, valTasaDeCambioImpresion, valCantidadAImprimir, valCodigoVendedor, vCodigoMonedaLocal);
            return insComisionDeVendedoresPorCobranzaMonto.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados
    } //End of class clsCobranzaRpt

} //End of namespace Galac.Adm.Brl.Venta

