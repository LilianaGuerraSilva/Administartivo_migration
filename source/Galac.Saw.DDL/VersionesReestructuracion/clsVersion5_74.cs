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
   class clsVersion5_74 : clsVersionARestructurar {

      public clsVersion5_74(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.74";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();         
         DisposeConnectionNoTransaction();
         return true;
      }      
   }
}
