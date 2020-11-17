using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Galac.Saw.DDL.VersionesReestructuracion {
   class clsVersion5_56:clsVersionARestructurar {

      public clsVersion5_56(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.56";
      }

      public override bool UpdateToVersion() {
            return true;
        }
             
   }
}
