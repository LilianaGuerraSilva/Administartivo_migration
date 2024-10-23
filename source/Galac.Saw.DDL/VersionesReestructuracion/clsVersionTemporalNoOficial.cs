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
            DisposeConnectionNoTransaction();
			return true;
		}

		private void CrearCampoManejaMerma () {
            AddColumnBoolean("Adm.ListaDeMateriales", "ManejaMerma", "CONSTRAINT nnLisDeMatManejaMerm NOT NULL", false);
            
            if (AddColumnNumeric("Adm.ListaDeMaterialesDetalleArticulo", "MermaNormal", 25, 8, "", 0)) {
                ActualizaCantidadMermaDetalleArticulo();
                AddDefaultConstraint("Adm.ListaDeMaterialesDetalleArticulo", "nnLisDeMatDetArtMermaNorma", "0", "MermaNormal");
            }

            if (AddColumnNumeric("Adm.ListaDeMaterialesDetalleArticulo", "PorcentajeMermaNormal", 25, 8, "", 0)) {
                ActualizaPorcentajeMermaDetalleArticulo();
                AddDefaultConstraint("Adm.ListaDeMaterialesDetalleArticulo", "nnLisDeMatDetArtPorcentaje", "0", "PorcentajeMermaNormal");
            }

            if (AddColumnNumeric("Adm.ListaDeMaterialesDetalleSalidas", "MermaNormal", 25, 8, "", 0)) {
                ActualizaCantidadMermaDetalleASalidas();
                AddDefaultConstraint("Adm.ListaDeMaterialesDetalleSalidas", "nnLisDeMatDetSalMermaNorma", "0", "MermaNormal");
            }

            if (AddColumnNumeric("Adm.ListaDeMaterialesDetalleSalidas", "PorcentajeMermaNormal", 25, 8, "", 0)) {
                ActualizaPorcentajeMermaDetalleSalidas();
                AddDefaultConstraint("Adm.ListaDeMaterialesDetalleSalidas", "nnLisDeMatDetSalPorcentaje", "0", "PorcentajeMermaNormal");
            }
        }
        private void ActualizaCantidadMermaDetalleArticulo() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Adm.ListaDeMaterialesDetalleArticulo");
            vSql.AppendLine("SET Adm.ListaDeMaterialesDetalleArticulo.MermaNormal = 0");
            Execute(vSql.ToString(), 0);
        }

        private void ActualizaPorcentajeMermaDetalleArticulo() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Adm.ListaDeMaterialesDetalleArticulo");
            vSql.AppendLine("SET Adm.ListaDeMaterialesDetalleArticulo.PorcentajeMermaNormal = 0");
            Execute(vSql.ToString(), 0);
        }

        private void ActualizaCantidadMermaDetalleASalidas() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Adm.ListaDeMaterialesDetalleSalidas");
            vSql.AppendLine("SET Adm.ListaDeMaterialesDetalleSalidas.MermaNormal = 0");
            Execute(vSql.ToString(), 0);
        }

        private void ActualizaPorcentajeMermaDetalleSalidas() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Adm.ListaDeMaterialesDetalleSalidas");
            vSql.AppendLine("SET Adm.ListaDeMaterialesDetalleSalidas.PorcentajeMermaNormal = 0");
            Execute(vSql.ToString(), 0);
        }

    }
}
