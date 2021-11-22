using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Base;

namespace Galac.Adm.Brl.Venta.Reportes {

    public class clsCxCRpt: ILibReportInfo, ICxCInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables

        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        private Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Comun.Brl.TablasGen.clsMonedaLocalActual();
        #endregion //Propiedades

        #region Constructores
        public clsCxCRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("CxC", CxCInfo());
            vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
        }
        #endregion //Constructores

        #region Metodos Generados
        private Dictionary<string, string> CxCInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Adm.Gp_CxCSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable ICxCInformes.BuildCxCPendientesEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eMonedaParaImpresion valMonedasAgrupadasPor) {
            string vSql;
            clsCxCSql insCxCSql = new clsCxCSql();
            ILibDataRpt insCxCPendientesEntreFechas = new Dal.Venta.clsCXCDat();
            vSql = insCxCSql.SqlCxCPendientesEntreFechas(valConsecutivoCompania, valFechaDesde, valFechaHasta, valMonedaDelReporte, valMonedasAgrupadasPor);
            return insCxCPendientesEntreFechas.GetDt(vSql, 0);
        }

        System.Data.DataTable ICxCInformes.BuildCxCPorCliente(int valConsecutivoCompania, string valCodigoDelCliente, string valZonaCobranza, DateTime valFechaDesde, DateTime valFechaHasta, eClientesOrdenadosPor valClientesOrdenadosPor, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eMonedaParaImpresion valMonedasAgrupadasPor) {
            string vSql;
            clsCxCSql insCxCSql = new clsCxCSql();
            ILibDataRpt insCxCPorCliente = new Dal.Venta.clsCXCDat();
            vSql = insCxCSql.SqlCxCPorCliente(valConsecutivoCompania, valCodigoDelCliente, valZonaCobranza, valFechaDesde, valFechaHasta, valClientesOrdenadosPor, valMonedaDelReporte, valMonedasAgrupadasPor);
            return insCxCPorCliente.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados

    } //End of class clsCxCRpt

} //End of namespace Galac.Adm.Brl.Venta

