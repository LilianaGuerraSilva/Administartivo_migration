using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using LibGalac.Aos.Base;
using System.Threading;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_92 : clsVersionARestructurar {
        public clsVersion5_92(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.92";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            //AgregaNuevaUnidadTributariaJulio2018();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregaNuevaUnidadTributariaJulio2018() {
            new Galac.Comun.Dal.TablasLey.clsValorUTED().InstalarTabla();
            new Galac.Comun.Dal.TablasLey.clsTablaRetencionED().InstalarTabla();
        }

    }
}
