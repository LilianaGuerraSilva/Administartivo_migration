using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal.Usal;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
   class clsVersion5_77 : clsVersionARestructurar {

      public clsVersion5_77(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.77";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         CrearParametroUsaClienteGenerico();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void CrearParametroUsaClienteGenerico() {          
          AgregarNuevoParametro("UsaClienteGenericoAlFacturar", "Factura", 2, "2.6.- Punto De Venta", 6, "", '2', "", 'N', "N");
      }      
   }
}
