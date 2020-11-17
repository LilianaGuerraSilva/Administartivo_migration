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
   class clsVersion5_70 : clsVersionARestructurar {

      public clsVersion5_70(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.70";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         AgregarCampoFacturaAplicaDecretoIvaEspecial();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void AgregarCampoFacturaAplicaDecretoIvaEspecial() {
         if (!ColumnExists("factura", "AplicaDecretoIvaEspecial")) {
            AddColumnBoolean("factura", "AplicaDecretoIvaEspecial", "AplicaDecretoIvaEsp NOT NULL", false);
         }
      }



   }
}
