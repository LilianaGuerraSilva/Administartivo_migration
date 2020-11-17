using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Galac.Saw.DDL.VersionesReestructuracion {
   class clsVersion5_58 :clsVersionARestructurar {

      public clsVersion5_58(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.58";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         AgregarNuevoParametro("BloquearEmision", "Factura", 2, "2.2.- Facturación (Continuación) ", 2, "", '0', "", 'N', "0");
         AgregaCampoEnFactura();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void AgregaCampoEnFactura() {
         if(!ColumnExists("factura", "ImprimeFiscal")) {
            AddColumnBoolean("factura", "ImprimeFiscal", "ImprimieFi NOT NULL", false);
         }
      }

   }
}
