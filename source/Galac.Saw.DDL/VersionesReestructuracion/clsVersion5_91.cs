using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using LibGalac.Aos.Base;
using System.Threading;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_91 : clsVersionARestructurar {
        public clsVersion5_91(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.91";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarCamposACompania();
            //AgregaNuevaUnidadMayoTributaria2018();
            AgregarColumnaACaja();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarCamposACompania() {
            if (!ColumnExists("dbo.Compania", "CodigoMoneda")) {
                AddColumnString ("dbo.Compania", "CodigoMoneda", 4, "", "VEF");
            }
            if (!ColumnExists("dbo.Compania", "StatusCompania")) {
                AddColumnEnumerative ("dbo.Compania", "StatusCompania",  "StatusComp NOT NULL", 0);
            }
            if (!ColumnExists("dbo.Compania", "StatusMonetaryReconversionProcess")) {
                AddColumnString("dbo.Compania", "StatusMonetaryReconversionProcess", 1, "StatusMone NOT NULL", "0");
            }
        }

        private void AgregaNuevaUnidadMayoTributaria2018() {
            new Galac.Comun.Dal.TablasLey.clsValorUTED().InstalarTabla();
            new Galac.Comun.Dal.TablasLey.clsTablaRetencionED().InstalarTabla();
        }

        private void AgregarColumnaACaja() {
            AddColumnBoolean("Caja", "UsarModoDotNet", "", false);
        }
    }
}
