using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Galac.Saw.DDL.VersionesReestructuracion {
   class clsVersion5_57 :clsVersionARestructurar {

      public clsVersion5_57(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.57";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         StringBuilder vSql = new StringBuilder();
         vSql.AppendLine("UPDATE Comun.SettDefinition SET IsSetForAllEnterprise = 'N'");
         vSql.AppendLine("WHERE Name = 'PermitirSobregiro'");
         Execute(vSql.ToString(), -1);
         vSql.AppendLine("UPDATE Comun.SettDefinition SET Name = 'NumeroDeDigitosEnFactura'");
         vSql.AppendLine("WHERE Name = 'NumeroDeCerosALaIzquierda'");
         Execute(vSql.ToString(), -1);
         DisposeConnectionNoTransaction();
         return true;
      }

   }
}
