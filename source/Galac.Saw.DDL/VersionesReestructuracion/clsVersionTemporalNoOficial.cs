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
            CrearCampoManejaMermaOP();
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

        private void CrearCampoManejaMermaOP() {
            AddColumnBoolean("Adm.OrdenDeProduccion", "ListaUsaMerma", "CONSTRAINT nnOrdDeProListaUsaMer NOT NULL", false);

            if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleArticulo", "PorcentajeMermaNormalOriginal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtPoMeNoOr", "0", "PorcentajeMermaNormalOriginal");
            }

            if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleArticulo", "CantidadMermaNormal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtCaMeNo", "0", "CantidadMermaNormal");
            }

            if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleArticulo", "PorcentajeMermaNormal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtPoMeNo", "0", "PorcentajeMermaNormal");
            }

            if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleArticulo", "CantidadMermaAnormal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtCaMeAn", "0", "CantidadMermaAnormal");
            }

            if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleArticulo", "PorcentajeMermaAnormal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtPoMeAn", "0", "PorcentajeMermaAnormal");
            }

            if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleMateriales", "PorcentajeMermaNormalOriginal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatPoMeNoOr", "0", "PorcentajeMermaNormalOriginal");
            }

            if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleMateriales", "CantidadMermaNormal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatCaMeNo", "0", "CantidadMermaNormal");
            }

            if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleMateriales", "PorcentajeMermaNormal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatPoMeNo", "0", "PorcentajeMermaNormal");
            }

            if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleMateriales", "CantidadMermaAnormal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatCaMeAn", "0", "CantidadMermaAnormal");
            }

            if (AddColumnNumeric("Adm.OrdenDeProduccionDetalleMateriales", "PorcentajeMermaAnormal", 25, 8, "", 0)) {
                AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatPoMeAn", "0", "PorcentajeMermaAnormal");
            }
        }
    }
}
