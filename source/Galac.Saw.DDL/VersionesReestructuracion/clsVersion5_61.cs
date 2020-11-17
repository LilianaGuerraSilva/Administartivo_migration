using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using Galac.Contab.Dal.Tablas;
using System.Threading;


namespace Galac.Saw.DDL.VersionesReestructuracion {
   class clsVersion5_61 :clsVersionARestructurar {

      public clsVersion5_61(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.61";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         AgrgaParametroFormatoDeFechaTexto();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void AgrgaParametroFormatoDeFechaTexto() {
         AgregarNuevoParametro("FormatoDeFechaTexto", "Factura", 2, "2.3.- Impresión de Factura", 3, "", '0', "", 'N', "dd/mm/yyyy");
         StringBuilder vSqlSbFormato = new StringBuilder();
         vSqlSbFormato.AppendLine("UPDATE Comun.SettValueByCompany");
         vSqlSbFormato.AppendLine("SET Value = '1'");
         vSqlSbFormato.AppendLine("WHERE NameSettDefinition = 'FormatoDeFecha'");
         Execute(vSqlSbFormato.ToString());
         StringBuilder vSqlSbTexto = new StringBuilder();
         vSqlSbTexto.AppendLine("UPDATE Comun.SettValueByCompany");
         vSqlSbTexto.AppendLine("SET Value = 'dd-mm-yyyy'");
         vSqlSbTexto.AppendLine("WHERE NameSettDefinition = 'FormatoDeFechaTexto'");
         Execute(vSqlSbTexto.ToString());
      }


   }
}
