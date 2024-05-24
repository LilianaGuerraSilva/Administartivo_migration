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
			AgregarColumnasIGTFEnCxP();
			CrearListaDeMaterialesDetalleArticuloProducir();
			OrdenDeProduccionMultiplesSalidas();
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

		private void CrearListaDeMaterialesDetalleArticuloProducir() {
			DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.ArticuloInventario", "Adm.ListaDeMateriales");
			if (new Adm.Dal.GestionProduccion.clsListaDeMaterialesDetalleSalidasED().InstalarTabla()) {
				LLenarListaDeMaterialesProducir();
			}
		}

		private void LLenarListaDeMaterialesProducir() {
			try {
				StringBuilder vSql = new StringBuilder();
				vSql.AppendLine("INSERT INTO Adm.ListaDeMaterialesDetalleSalidas (ConsecutivoCompania,ConsecutivoListaDeMateriales,Consecutivo,CodigoArticuloInventario,Cantidad,PorcentajeDeCosto)");
				vSql.AppendLine("SELECT ConsecutivoCompania,Consecutivo,1,CodigoArticuloInventario,1,100");
				vSql.AppendLine("FROM Adm.ListaDeMateriales");
				LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), null, "", 0);
			} catch (Exception) {
				throw;
			}
		}

		void OrdenDeProduccionMultiplesSalidas() {
			DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Saw.Almacen", "Adm.OrdenDeProduccionDetalleArticulo");
			DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.ListaDeMateriales", "Adm.OrdenDeProduccionDetalleArticulo");
			if (AddColumnNumeric("Adm.OrdenDeProduccion", "ConsecutivoListaDeMateriales", 10, 0, "", 0)) {
				ActualizaConsecutivoListaMaterias();
				AddNotNullConstraint("Adm.OrdenDeProduccion", "ConsecutivoListaDeMateriales", InsSql.NumericTypeForDb(10, 0));
				AddForeignKey("Adm.ListaDeMateriales", "Adm.OrdenDeProduccion", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoListaDeMateriales", }, false, false);
			}
			AlterColumnIfExist("Adm.OrdenDeProduccionDetalleArticulo", "ConsecutivoListaDeMateriales", InsSql.NumericTypeForDb(10, 0), "", "0");
			AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "nnOrdDeProDetArtConsecutiv", "0", "ConsecutivoListaDeMateriales");
			DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.OrdenDeProduccionDetalleArticulo", "Adm.OrdenDeProduccionDetalleMateriales");
			DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Saw.Almacen", "Adm.OrdenDeProduccionDetalleMateriales");
			DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "ArticuloInventario", "Adm.OrdenDeProduccionDetalleMateriales");
			DeletePrimaryKey("Adm.OrdenDeProduccionDetalleMateriales");
			AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatCoDetArt", "0", "ConsecutivoOrdenDeProduccionDetalleArticulo");
			AddPrimaryKey("Adm.OrdenDeProduccionDetalleMateriales", "ConsecutivoCompania, ConsecutivoOrdenDeProduccion, Consecutivo");
			AddForeignKey("Adm.OrdenDeProduccion", "Adm.OrdenDeProduccionDetalleMateriales", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoOrdenDeProduccion" }, true, true);
			if (AddColumnDecimal("Adm.OrdenDeProduccionDetalleArticulo", "PorcentajeCostoEstimado", 25, 8, "", 0)) {
				AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtPoCoEs", "0", "PorcentajeCostoEstimado");
			}
			if (AddColumnDecimal("Adm.OrdenDeProduccionDetalleArticulo", "PorcentajeCostoCierre", 25, 8, "", 0)) {
				AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtPoCoCi", "0", "PorcentajeCostoCierre");
			}
			if (AddColumnDecimal("Adm.OrdenDeProduccionDetalleArticulo", "Costo", 25, 8, "", 0)) {
				AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtCo", "0", "Costo");
			}
			if (AddColumnDecimal("Adm.OrdenDeProduccionDetalleArticulo", "CantidadOriginalLista", 25, 8, "", 0)) {
				AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtCaOrLi", "0", "CantidadOriginalLista");
			}

			AlterColumnIfExist("Adm.OrdenDeProduccionDetalleArticulo", "CantidadSolicitada", InsSql.DecimalTypeForDb(25, 8), "", "0");
			AlterColumnIfExist("Adm.OrdenDeProduccionDetalleArticulo", "CantidadProducida", InsSql.DecimalTypeForDb(25, 8), "", "0");
			AlterColumnIfExist("Adm.OrdenDeProduccionDetalleArticulo", "CostoUnitario", InsSql.DecimalTypeForDb(25, 8), "", "0");
			AlterColumnIfExist("Adm.OrdenDeProduccionDetalleArticulo", "MontoSubTotal", InsSql.DecimalTypeForDb(25, 8), "", "0");
			AlterColumnIfExist("Adm.OrdenDeProduccionDetalleArticulo", "CantidadAjustada", InsSql.DecimalTypeForDb(25, 8), "", "0");

			AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtCaSo", "0", "CantidadSolicitada");
			AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtCaPr", "0", "CantidadProducida");
			AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtCoUn", "0", "CostoUnitario");
			AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtMoSuTo", "0", "MontoSubTotal");
			AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtCaAj", "0", "CantidadAjustada");

			AlterColumnIfExist("Adm.OrdenDeProduccionDetalleMateriales", "CantidadReservadaInventario", InsSql.DecimalTypeForDb(25, 8), "", "0");
			AlterColumnIfExist("Adm.OrdenDeProduccionDetalleMateriales", "CantidadConsumida", InsSql.DecimalTypeForDb(25, 8), "", "0");
			AlterColumnIfExist("Adm.OrdenDeProduccionDetalleMateriales", "CostoUnitarioArticuloInventario", InsSql.DecimalTypeForDb(25, 8), "", "0");
			AlterColumnIfExist("Adm.OrdenDeProduccionDetalleMateriales", "MontoSubtotal", InsSql.DecimalTypeForDb(25, 8), "", "0");
			AlterColumnIfExist("Adm.OrdenDeProduccionDetalleMateriales", "CantidadAjustada", InsSql.DecimalTypeForDb(25, 8), "", "0");

			AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatCaReIn", "0", "CantidadReservadaInventario");
			AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatCaCo", "0", "CantidadConsumida");
			AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatCoUnArIn", "0", "CostoUnitarioArticuloInventario");
			AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatMoSu", "0", "MontoSubtotal");
			AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatCaAj", "0", "CantidadAjustada");

			Execute("UPDATE Adm.OrdenDeProduccionDetalleMateriales SET CodigoArticulo = '' WHERE CodigoArticulo = NULL");
			AddNotNullConstraint("Adm.OrdenDeProduccionDetalleMateriales", "CodigoArticulo", InsSql.VarCharTypeForDb(30));

			if (AddColumnNumeric("Adm.OrdenDeProduccion", "CantidadAProducir", 25, 8, "", 0)) {
				ActualizaCantidadProducir();
				AddNotNullConstraint("Adm.OrdenDeProduccion", "CantidadAProducir", InsSql.NumericTypeForDb(25, 8));
			}

			if (AddColumnNumeric("Adm.OrdenDeProduccion", "CantidadProducida", 25, 8, "", 0)) {
				ActualizaCantidadProducida();
				AddNotNullConstraint("Adm.OrdenDeProduccion", "CantidadProducida", InsSql.NumericTypeForDb(25, 8));
			}

		}

		private void ActualizaConsecutivoListaMaterias() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("UPDATE Adm.OrdenDeProduccion");
			vSql.AppendLine("SET Adm.OrdenDeProduccion.ConsecutivoListaDeMateriales = OPDetalleArticulo.ConsecutivoListaDeMateriales");
			vSql.AppendLine("FROM Adm.OrdenDeProduccionDetalleArticulo AS OPDetalleArticulo INNER JOIN  ");
			vSql.AppendLine("Adm.OrdenDeProduccion ON Adm.OrdenDeProduccion.ConsecutivoCompania = OPDetalleArticulo.ConsecutivoCompania ");
			vSql.AppendLine("AND Adm.OrdenDeProduccion.Consecutivo = OPDetalleArticulo.ConsecutivoOrdenDeProduccion");
			Execute(vSql.ToString(), 0);
		}

		private void ActualizaCantidadProducir() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("UPDATE Adm.OrdenDeProduccion");
			vSql.AppendLine("SET Adm.OrdenDeProduccion.CantidadAProducir = OPDetalleArticulo.CantidadSolicitada");
			vSql.AppendLine("FROM Adm.OrdenDeProduccionDetalleArticulo AS OPDetalleArticulo INNER JOIN  ");
			vSql.AppendLine("Adm.OrdenDeProduccion ON Adm.OrdenDeProduccion.ConsecutivoCompania = OPDetalleArticulo.ConsecutivoCompania ");
			vSql.AppendLine("AND Adm.OrdenDeProduccion.Consecutivo = OPDetalleArticulo.ConsecutivoOrdenDeProduccion");
			Execute(vSql.ToString(), 0);
		}

		private void ActualizaCantidadProducida() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("UPDATE Adm.OrdenDeProduccion");
			vSql.AppendLine("SET Adm.OrdenDeProduccion.CantidadProducida = OPDetalleArticulo.CantidadProducida");
			vSql.AppendLine("FROM Adm.OrdenDeProduccionDetalleArticulo AS OPDetalleArticulo INNER JOIN  ");
			vSql.AppendLine("Adm.OrdenDeProduccion ON Adm.OrdenDeProduccion.ConsecutivoCompania = OPDetalleArticulo.ConsecutivoCompania ");
			vSql.AppendLine("AND Adm.OrdenDeProduccion.Consecutivo = OPDetalleArticulo.ConsecutivoOrdenDeProduccion");
			Execute(vSql.ToString(), 0);
		}
	}
}
