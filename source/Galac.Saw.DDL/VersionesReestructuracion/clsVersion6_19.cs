using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_19 : clsVersionARestructurar {

        public clsVersion6_19(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.19";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            ActivarParametroMostrarTotalEnBolivarDigitaloSoberano();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void ActivarParametroMostrarTotalEnBolivarDigitaloSoberano() {
            string vSql;
            vSql = "UPDATE Comun.SettValueByCompany SET value = 'S' WHERE NameSettDefinition = 'MostrarMtoTotalBsFEnObservaciones'";
            Execute(vSql, 0);
        }
    }
}