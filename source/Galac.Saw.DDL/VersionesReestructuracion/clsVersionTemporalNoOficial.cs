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
			if (AddColumnCurrency("CxP", "BaseImponibleIGTFML", "")) {
				AddDefaultConstraint("CxP", "cBiG", "0", "BaseImponibleIGTFML");
			}
			if (AddColumnCurrency("CxP", "AlicuotaIGTFML", "")) {
				AddDefaultConstraint("CxP", "cAiG", "3", "AlicuotaIGTFML");
			}
			if (AddColumnCurrency("CxP", "MontoIGTFML", "")) {
				AddDefaultConstraint("CxP", "cMiG", "0", "MontoIGTFML");
			}
		}

	}
}   