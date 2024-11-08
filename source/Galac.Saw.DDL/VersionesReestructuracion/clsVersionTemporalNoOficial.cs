using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;
using System.ComponentModel.DataAnnotations;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using System;
using System.Data;
using LibGalac.Aos.Cnf;
using Galac.Saw.Lib;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			CrearCampoManejaMerma();
            AmpliarColumnaCompaniaImprentaDigitalClave();
            DisposeConnectionNoTransaction();
			return true;
		}

		private void CrearCampoManejaMerma () {
            AddColumnBoolean("Adm.ListaDeMateriales", "ManejaMerma", "CONSTRAINT nnLisDeMatManejaMerm NOT NULL", false);

            if (AddColumnNumeric("Adm.ListaDeMaterialesDetalleArticulo", "MermaNormal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.ListaDeMaterialesDetalleArticulo", "nnLisDeMatDetArtMermaNorma", "0", "MermaNormal");
            }

            if (AddColumnNumeric("Adm.ListaDeMaterialesDetalleArticulo", "PorcentajeMermaNormal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.ListaDeMaterialesDetalleArticulo", "nnLisDeMatDetArtPorcentaje", "0", "PorcentajeMermaNormal");
            }

            if (AddColumnNumeric("Adm.ListaDeMaterialesDetalleSalidas", "MermaNormal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.ListaDeMaterialesDetalleSalidas", "nnLisDeMatDetSalMermaNorma", "0", "MermaNormal");
            }

            if (AddColumnNumeric("Adm.ListaDeMaterialesDetalleSalidas", "PorcentajeMermaNormal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.ListaDeMaterialesDetalleSalidas", "nnLisDeMatDetSalPorcentaje", "0", "PorcentajeMermaNormal");
            }
        }

        private void AmpliarColumnaCompaniaImprentaDigitalClave() {            
            ModifyLengthOfColumnString("Compania", "ImprentaDigitalClave", 1000, "");
        }
    }
}
