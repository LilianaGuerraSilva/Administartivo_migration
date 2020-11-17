using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using LibGalac.Aos.Base;
using System.Threading;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_89 : clsVersionARestructurar {
        public clsVersion5_89(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.89";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();            
            DisposeConnectionNoTransaction();
            return true;
        }
    }
}
