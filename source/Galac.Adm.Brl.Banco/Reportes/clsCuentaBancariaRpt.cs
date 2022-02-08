using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Banco;
using LibGalac.Aos.Base;

namespace Galac.Adm.Brl.Banco.Reportes {

    public class clsCuentaBancariaRpt: ILibReportInfo, ICuentaBancariaInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables

        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades

        #region Constructores
        public clsCuentaBancariaRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Cuenta Bancaria", CuentaBancariaInfo());
        }
        #endregion //Constructores

        #region Metodos Generados
        private Dictionary<string, string> CuentaBancariaInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Saw.Gp_CuentaBancariaSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable ICuentaBancariaInformes.BuildSaldosBancarios(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, bool valSoloCuentasActivas) {
            string vSql;
            clsCuentaBancariaSql insCuentaBancariaSql = new clsCuentaBancariaSql();
            ILibDataRpt insSaldosBancarios = new Dal.Banco.clsCuentaBancariaDat();
            vSql = insCuentaBancariaSql.SqlSaldosBancarios(valConsecutivoCompania, valFechaDesde, valFechaHasta, valSoloCuentasActivas);
            return insSaldosBancarios.GetDt(vSql, 0);
        }
        #endregion //Metodos Generados

    } //End of class clsCuentaBancariaRpt

} //End of namespace Galac.Adm.Brl.CuentaBancaria

