using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;
using System.ComponentModel.DataAnnotations;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial: clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			AmpliarCampoObservacionesSolicitudDePago();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void AmpliarCampoObservacionesSolicitudDePago() {
			string vSQL = "ALTER TABLE " + LibDbo.ToFullDboName("Saw.SolicitudesDePago") + " ALTER COLUMN Observaciones VARCHAR(60) NULL";
			Execute(vSQL, 0);
		}
	}
}   