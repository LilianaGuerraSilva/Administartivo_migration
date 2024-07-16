using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;
using System.ComponentModel.DataAnnotations;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using System;

namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			AgregarColumnasEnCompania();
            DisposeConnectionNoTransaction();
			return true;
		}

		private void AgregarColumnasEnCompania() {
			AddColumnString("Compania", "ImprentaDigitalUrl", 500,"","");			
            AddColumnString("Compania", "ImprentaDigitalNombreCampoUsuario", 50, "", "");
            AddColumnString("Compania", "ImprentaDigitalNombreCampoClave", 50, "", "");
            AddColumnString("Compania", "ImprentaDigitalUsuario", 100, "", "");
            AddColumnString("Compania", "ImprentaDigitalClave", 500, "", "");            
        }
	}
}
