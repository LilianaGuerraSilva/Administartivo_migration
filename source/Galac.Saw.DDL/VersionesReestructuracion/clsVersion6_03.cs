using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using LibGalac.Aos.Base;
using System.Threading;
using System.Data;
using LibGalac.Aos.Dal.Usal;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using System.Transactions;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_03:clsVersionARestructurar {
        public clsVersion6_03(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.03";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();           
            DisposeConnectionNoTransaction();
            return true;
        }          
    }
}
