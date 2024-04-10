using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;
using System.ComponentModel.DataAnnotations;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;

namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial: clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			AgregarColumnasIGTFEnCxP();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void AgregarColumnasIGTFEnCxP() {
			if (AddColumnCurrency("CxP", "BaseImponibleIGTFML", "", 0)) {
				AddDefaultConstraint("CxP", "cBiG", "0", "BaseImponibleIGTFML");
			}
			if (AddColumnCurrency("CxP", "AlicuotaIGTFML", "", 0)) {
				AddDefaultConstraint("CxP", "cAiG", "0", "AlicuotaIGTFML");
			}
			if (AddColumnCurrency("CxP", "MontoIGTFML", "", 0)) {
				AddDefaultConstraint("CxP", "cMiG", "0", "MontoIGTFML");
			}
		}

	}
}   