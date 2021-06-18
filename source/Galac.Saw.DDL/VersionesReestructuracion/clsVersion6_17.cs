using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_17 : clsVersionARestructurar {

        public clsVersion6_17(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.17";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            BorraVistaEnumPuertoBalanza();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void BorraVistaEnumPuertoBalanza() {
            new LibViews().Drop("Adm.Gv_EnumPuertoDeCaja");
        }
    }
}

