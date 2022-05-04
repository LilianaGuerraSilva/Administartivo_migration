using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using LibGalac.Aos.Dal;
using System;

namespace Galac.Saw.DDL.VersionesReestructuracion {
	class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			AgregarParametroNombrePlantillaSubFacturaConOtrosCargos();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void AgregarParametroNombrePlantillaSubFacturaConOtrosCargos(){
			AgregarNuevoParametro("NombrePlantillaSubFacturaConOtrosCargos", "Factura", 2, "2.4.- Modelo de Factura", 4 , "",'3', "", 'N', "rpxSubFacturaConOtrosCargos");
		}
	}
}
