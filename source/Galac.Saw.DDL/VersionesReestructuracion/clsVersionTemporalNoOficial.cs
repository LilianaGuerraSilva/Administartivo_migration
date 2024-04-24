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
			AgregarValorPorDefecto();
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

		private void AgregarValorPorDefecto() {
			try {
				if (ColumnExists("Adm.ListaDeMateriales", "CodigoArticuloInventario")) {
					StringBuilder vSql = new StringBuilder();
					vSql.AppendLine("UPDATE Adm.ListaDeMateriales");
					vSql.AppendLine("SET CodigoArticuloInventario = " + _insSql.ToSqlValue(""));
					LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), null, "", 0);
				}
			} catch (Exception) {
				throw;
			}
		}
	}
}
