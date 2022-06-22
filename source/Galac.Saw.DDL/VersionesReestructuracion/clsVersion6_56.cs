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
    class clsVersion6_56 : clsVersionARestructurar {

        public clsVersion6_56(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.56";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
			AgregarParametroNombrePlantillaSubFacturaConOtrosCargos();
			DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarParametroNombrePlantillaSubFacturaConOtrosCargos() {
            AgregarNuevoParametro("NombrePlantillaSubFacturaConOtrosCargos", "Factura", 2, " 2.4.- Modelo de Factura ", 4, "", '2', "", 'N', "rpxSubFacturaConOtrosCargos");
        }
    }
}