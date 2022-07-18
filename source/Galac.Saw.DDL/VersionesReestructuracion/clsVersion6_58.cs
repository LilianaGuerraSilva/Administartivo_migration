using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_58 : clsVersionARestructurar {
        public clsVersion6_58(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.58";
        }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            EliminarTasaDeCambioIgualCero();
            DisposeConnectionNoTransaction();
            return true;
        }
        private void EliminarTasaDeCambioIgualCero() {
            Execute("DELETE FROM comun.cambio WHERE CambioAMonedaLocal = 0 ", 0);
        }
    }
}