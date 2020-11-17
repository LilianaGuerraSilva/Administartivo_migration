using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal.Usal;
using LibGalac.Aos.Base.Dal;
using Galac.Comun.Ccl.TablasLey;

namespace Galac.Saw.DDL.VersionesReestructuracion {
   class clsVersion5_71 : clsVersionARestructurar {

      public clsVersion5_71(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.71";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         AgregarParametrosAplicaAlicuotaEspecial();
         AjusteDeParametrosSegunDatos();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void AgregarParametrosAplicaAlicuotaEspecial() {
         AgregarNuevoParametro("AplicacionAlicuotaEspecial", "DatosGenerales", 1, "1.1.-Compania", 1, "",'0', "",'N', "0");
         AgregarNuevoParametro("AplicarIVAEspecial", "DatosGenerales", 1, "1.1.-Compania", 1, "", '2', "", 'N', "N");
         AgregarNuevoParametro("FacturarPorDefectoIvaEspecial", "DatosGenerales", 1, "1.1.-Compania", 1, "", '2', "", 'N', "N");

      }
      private void AjusteDeParametrosSegunDatos() {
         StringBuilder vSqlSb = new StringBuilder();
         vSqlSb.AppendLine("UPDATE Comun.SettValueByCompany");
         vSqlSb.AppendLine("SET Value = 'S'");
         vSqlSb.AppendLine("WHERE NameSettDefinition = 'AplicarIVAEspecial'");
         vSqlSb.AppendLine("AND ConsecutivoCompania IN (");
         vSqlSb.AppendLine("SELECT ConsecutivoCompania FROM factura");
         vSqlSb.AppendLine("WHERE AplicaDecretoIvaEspecial = 'S')");
         Execute(vSqlSb.ToString());
         vSqlSb.Clear();
         vSqlSb.AppendLine("UPDATE Comun.SettValueByCompany");
         vSqlSb.AppendLine("SET Value = '2'");
         vSqlSb.AppendLine("WHERE NameSettDefinition = 'AplicacionAlicuotaEspecial'");
         vSqlSb.AppendLine("AND ConsecutivoCompania IN (");
         vSqlSb.AppendLine("SELECT ConsecutivoCompania FROM factura");
         vSqlSb.AppendLine("WHERE AplicaDecretoIvaEspecial = 'S')");
         Execute (vSqlSb.ToString());
      }



   }
}
