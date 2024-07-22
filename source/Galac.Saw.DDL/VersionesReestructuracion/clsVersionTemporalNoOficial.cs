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
			CrearLoteDeInventario();
			AjustesNotaEntradaSalida();
			CreacionDeParametros();
			DisposeConnectionNoTransaction();
			return true;
		}

		void CreacionDeParametros() {
			AgregarNuevoParametro("UsaLoteFechaDeVencimiento", "Inventario", 0, "5.1.- Inventario", 1, "", eTipoDeDatoParametros.Enumerativo, "", 'N', "N");
		}

        void AjustesNotaEntradaSalida() {
            if (TableExists("dbo.RenglonNotaES")) {
				DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.NotaDeEntradaSalida", "dbo.RenglonNotaES");
				DeletePrimaryKey("dbo.RenglonNotaES");
				AddPrimaryKey("dbo.RenglonNotaES", "ConsecutivoCompania,NumeroDocumento,ConsecutivoRenglon");
				AddForeignKey("dbo.NotaDeEntradaSalida", "dbo.RenglonNotaES", new string[] { "ConsecutivoCompania,NumeroDocumento" }, new string[] { "ConsecutivoCompania,NumeroDocumento" }, true, true);

				AddColumnString("dbo.RenglonNotaES", "LoteDeInventario", 30, "", "");
				AddColumnDate("dbo.RenglonNotaES", "FechaDeElaboracion", "", false, true);
				AddColumnDate("dbo.RenglonNotaES", "FechaDeVencimiento", "", false, true);
			}
		}

        void CrearLoteDeInventario() {
			if (!TableExists("Saw.LoteDeInventario")) {
				new Galac.Saw.Dal.Inventario.clsLoteDeInventarioED().InstalarTabla();
            }
        }
	}
}
