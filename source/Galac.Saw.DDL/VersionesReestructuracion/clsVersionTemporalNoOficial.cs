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
            AgregaParametroGeneral();
            DisposeConnectionNoTransaction();
			return true;
		}

        private void AgregaParametroGeneral() {
            AgregarNuevoParametro("SuscripcionGVentas", "DatosGenerales", 1, "1.2.-General", 2, "", '2', "", 'S', "1000;1001;1002;1003;1004;1005");
            AgregarNuevoParametro("SerialConectorGVentas", "DatosGenerales", 1, "1.2.-General", 2, "", '2', "", 'S', "");
            AgregarNuevoParametro("NumeroIDGVentas", "DatosGenerales", 1, "1.2.-General", 2, "", '2', "", 'N', "");
        }

    }

}
