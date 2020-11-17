using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using Galac.Comun.Ccl.TablasLey;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_66 : clsVersionARestructurar {

        public clsVersion5_66(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.66";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CrearCampoMostrarPermitirDescripcionDelArticuloExtendida();
            CrearCampoPermitirNombreDelClienteExtendido();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CrearCampoMostrarPermitirDescripcionDelArticuloExtendida() {
           if (!ColumnExists("dbo.Caja", "PermitirDescripcionDelArticuloExtendida")) {
              AddColumnBoolean("dbo.Caja", "PermitirDescripcionDelArticuloExtendida", "", false);
           }
        }

        private void CrearCampoPermitirNombreDelClienteExtendido() {
           if (!ColumnExists("dbo.Caja", "PermitirNombreDelClienteExtendido")) {
              AddColumnBoolean("dbo.Caja", "PermitirNombreDelClienteExtendido", "", false);
           }
        }        
    }
}
