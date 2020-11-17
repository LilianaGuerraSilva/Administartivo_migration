using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using Galac.Contab.Dal.Tablas;
using System.Threading;


namespace Galac.Saw.DDL.VersionesReestructuracion {
   class clsVersion5_62 :clsVersionARestructurar {

      public clsVersion5_62(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.62";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         CrearCamposGeneraImpuestoBancarioEnRendicion();
         AjustaTotalesCajaChica();
         DisposeConnectionNoTransaction();
         return true;
      }
      
      private void CrearCamposGeneraImpuestoBancarioEnRendicion() {
         if(!ColumnExists("Adm.Rendicion", "GeneraImpuestoBancario")) {
            AddColumnBoolean("Adm.Rendicion", "GeneraImpuestoBancario", "GeneraImpRen  NULL", false);
         }
      }
      private void AjustaTotalesCajaChica() {
         StringBuilder vSqlSb = new StringBuilder();
         vSqlSb.AppendLine("UPDATE Adm.Rendicion SET TotalGastos = TotalAdelantos");
         vSqlSb.AppendLine("WHERE TotalAdelantos > 0");
         Execute(vSqlSb.ToString());
         StringBuilder vSqlSbAdelantos = new StringBuilder();
         vSqlSbAdelantos.AppendLine("UPDATE Adm.Rendicion SET  TotalAdelantos = 0");
         vSqlSbAdelantos.AppendLine("WHERE TotalAdelantos > 0");
         Execute(vSqlSbAdelantos.ToString());
      }

   }
}
