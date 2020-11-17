using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Brl;
using System.Data;
using LibGalac.Aos.Dal.Usal;
using System.Collections.Generic;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_09:clsVersionARestructurar {
        public clsVersion6_09(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.09";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            DesactivarMonedaVEF();
            AgregarPermisoInformesDeCaja();
            AsignaValorAMonedaDeCobroVacias();
            AsignaValorAMonedaYCambioEnFormaDeCobroVacias();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void DesactivarMonedaVEF() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("IF EXISTS (SELECT TOP(1) Codigo FROM dbo.Moneda WHERE Codigo = 'VEF')");
            vSql.AppendLine("BEGIN");
            vSql.AppendLine("   UPDATE dbo.Moneda SET Activa = 'N' WHERE Codigo = 'VEF'");
            vSql.AppendLine("END");
            Execute(vSql.ToString(),0);
        }
        private void AgregarPermisoInformesDeCaja() {
            LibGUserReestScripts SqlSecurityLevel = new LibGUserReestScripts();
            StringBuilder vSql = new StringBuilder();
            List<string> vActions = new List<string>();
            System.Collections.Hashtable vFiltros = new System.Collections.Hashtable();
            vActions.Add("Informes");
            vSql.AppendLine(SqlSecurityLevel.SqlAddSecurityLevel("Caja Registradora", vActions, "Principal", 1, "SAW", null));
            foreach (var item in vActions) {
                vSql.AppendLine(SqlSecurityLevel.SqlUpdateSecurityLevel("Caja Registradora", item, true, vFiltros));
            }
            Execute(vSql.ToString(), -1);
        }

        private void AsignaValorAMonedaDeCobroVacias() {
            StringBuilder vSQL = new StringBuilder();
            if (ColumnExists("dbo.factura", "CodigoMonedaDeCobro")) {
                vSQL.AppendLine("WHILE EXISTS (SELECT CodigoMoneda FROM dbo.factura WHERE dbo.factura.CodigoMonedaDeCobro = '')");
                vSQL.AppendLine("BEGIN");
                vSQL.AppendLine("   UPDATE TOP (1000) dbo.factura ");
                vSQL.AppendLine("       SET    dbo.factura.CodigoMonedaDeCobro = dbo.factura.CodigoMoneda");
                vSQL.AppendLine("   FROM  dbo.factura");
                vSQL.AppendLine("   WHERE dbo.factura.CodigoMonedaDeCobro = ''");
                vSQL.AppendLine("END");
                Execute(vSQL.ToString(), 0);
            }
        }
        
        private void AsignaValorAMonedaYCambioEnFormaDeCobroVacias() {
            StringBuilder vSQL = new StringBuilder();
            if (ColumnExists("dbo.renglonCobroDeFactura", "CodigoMoneda")) {
                vSQL.AppendLine("WHILE EXISTS (SELECT CodigoMoneda FROM renglonCobroDeFactura WHERE CodigoMoneda IS NULL)");
                vSQL.AppendLine("BEGIN");
                vSQL.AppendLine("   UPDATE TOP(1500) renglonCobroDeFactura ");
                vSQL.AppendLine("       SET CodigoMoneda = 'VES'");
		        vSQL.AppendLine("       , CambioAMonedaLocal = 1");
                vSQL.AppendLine("   WHERE CodigoMoneda IS NULL");
                vSQL.AppendLine("END");
                Execute(vSQL.ToString(), 0);
                vSQL.AppendLine("WHILE EXISTS (SELECT CambioAMonedaLocal FROM renglonCobroDeFactura WHERE CambioAMonedaLocal IS NULL OR CambioAMonedaLocal = 0)");
                vSQL.AppendLine("BEGIN");
                vSQL.AppendLine("   UPDATE TOP(1500) renglonCobroDeFactura ");
                vSQL.AppendLine("       SET CodigoMoneda = 'VES'");
		        vSQL.AppendLine("       , CambioAMonedaLocal = 1");
                vSQL.AppendLine("   WHERE CambioAMonedaLocal IS NULL OR CambioAMonedaLocal = 0");
                vSQL.AppendLine("END");
                Execute(vSQL.ToString(), 0);
            }
        }

    }
       
}
