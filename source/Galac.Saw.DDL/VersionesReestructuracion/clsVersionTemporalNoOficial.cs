using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using LibGalac.Aos.Dal;
using System;
using LibGalac.Aos.Base.Dal;
using System.Xml.Linq;
using LibGalac.Aos.Brl;
using System.Collections.Generic;
using System.Linq;
using Galac.Adm.Ccl.Vendedor;
using LibGalac.Aos.Catching;
using Galac.Adm.Dal.Vendedor;

namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			CrearColumnaRutaDeComercializacion();
			CrearTablaAdmVendedor();
			CrearCampoCompania_EstaIntegradaG360();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void CrearColumnaRutaDeComercializacion() {
			QAdvSql InsSql = new QAdvSql("");
			if (TableExists("dbo.Vendedor")) {
				if (AddColumnEnumerative("dbo.Vendedor", "RutaDeComercializacion", "", (int)eRutaDeComercializacion.Ninguna)) {
					AddDefaultConstraint("dbo.Vendedor", "d_VenRuDeCo", InsSql.ToSqlValue((int)eRutaDeComercializacion.Ninguna), "RutaDeComercializacion");
				}
			}
		}

		private void CrearColumnaConsecutivoVendedor(string valTabla, string valNombreColumna, string valConstraint) {
			QAdvSql InsSql = new QAdvSql("");
			if (AddColumnInteger(valTabla, valNombreColumna, valConstraint)) {
				AddNotNullConstraint(valTabla, valNombreColumna, InsSql.NumericTypeForDb(10, 0));
			}
		}
		private void LlenarColumnaConsecutivoVendedor(string valTabla, string valColumnaNueva, string valColumnaAnterior) {
			StringBuilder vSQL = new StringBuilder();
			LibDataScope vDb = new LibDataScope();
			vSQL.AppendLine("UPDATE " + valTabla + " SET " + valTabla + "." + valColumnaNueva + " = Adm.Vendedor.Consecutivo ");
			vSQL.AppendLine("FROM " + valTabla);
			vSQL.AppendLine("INNER JOIN Adm.Vendedor ON " + valTabla + ".ConsecutivoCompania = Adm.Vendedor.ConsecutivoCompania ");
			vSQL.AppendLine("AND " + valTabla + "." + valColumnaAnterior + " = Adm.Vendedor.Codigo");
			vDb.ExecuteWithScope(vSQL.ToString());
		}
		private void CrearTablaAdmVendedor() {
			if (!TableExists("Adm.Vendedor")) {
				new clsVendedorED().InstalarTabla();
				new DbMigrator.clsMigrarData(new string[] { "Vendedor" }).MigrarData();
				try {
					if (ForeignKeyNameExists("fk_CobranzaVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.Cobranza");
						CrearColumnaConsecutivoVendedor("dbo.Cobranza", "ConsecutivoCobrador", " CONSTRAINT nnCobCoCo NOT NULL");
						LlenarColumnaConsecutivoVendedor("dbo.Cobranza", "ConsecutivoCobrador", "CodigoCobrador");
						AddForeignKey("Adm.Vendedor", "dbo.Cobranza", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoCobrador" }, false, false);
					}
					if (ForeignKeyNameExists("fk_ContratoVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.Contrato");
						CrearColumnaConsecutivoVendedor("dbo.Contrato", "ConsecutivoVendedor", " CONSTRAINT nnConCoVe NOT NULL");
						LlenarColumnaConsecutivoVendedor("dbo.Contrato", "ConsecutivoVendedor", "CodigoVendedor");
						AddForeignKey("Adm.Vendedor", "dbo.Contrato", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
					}
					if (ForeignKeyNameExists("fk_CotizacionVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.Cotizacion");
						CrearColumnaConsecutivoVendedor("dbo.Cotizacion", "ConsecutivoVendedor", " CONSTRAINT nnCotCoVe NOT NULL");
						LlenarColumnaConsecutivoVendedor("dbo.Cotizacion", "ConsecutivoVendedor", "CodigoVendedor");
						AddForeignKey("Adm.Vendedor", "dbo.Cotizacion", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
					}
					if (ForeignKeyNameExists("fk_CxCVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.CxC");
						CrearColumnaConsecutivoVendedor("dbo.CxC", "ConsecutivoVendedor", " CONSTRAINT nnCxCCoVe NOT NULL");
						LlenarColumnaConsecutivoVendedor("dbo.CxC", "ConsecutivoVendedor", "CodigoVendedor");
						AddForeignKey("Adm.Vendedor", "dbo.CxC", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
					}
					if (ForeignKeyNameExists("fk_facturaVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.factura");
						CrearColumnaConsecutivoVendedor("dbo.factura", "ConsecutivoVendedor", " CONSTRAINT nnFacCoVe NOT NULL");
						LlenarColumnaConsecutivoVendedor("dbo.factura", "ConsecutivoVendedor", "CodigoVendedor");
						AddForeignKey("Adm.Vendedor", "dbo.factura", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
					}
					if (ForeignKeyNameExists("fk_RenglonComisionesDeVendedorVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.RenglonComisionesDeVendedor");
					}
					if (ForeignKeyNameExists("fk_RetirosACuentaVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.RetirosACuenta");
						CrearColumnaConsecutivoVendedor("dbo.RetirosACuenta", "ConsecutivoVendedor", " CONSTRAINT nnRetACuenCoVe NOT NULL");
						LlenarColumnaConsecutivoVendedor("dbo.RetirosACuenta", "ConsecutivoVendedor", "CodigoVendedor");
						AddForeignKey("Adm.Vendedor", "dbo.RetirosACuenta", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
					}
					if (!ColumnExists("dbo.Cliente", "ConsecutivoVendedor")) {
						CrearColumnaConsecutivoVendedor("dbo.Cliente", "ConsecutivoVendedor", " CONSTRAINT nnConCliVe NOT NULL");
						LlenarColumnaConsecutivoVendedor("dbo.Cliente", "ConsecutivoVendedor", "CodigoVendedor");
					}
					if (TableExists("dbo.Vendedor")) {
						ExecuteDropTable("dbo.Vendedor");
					}
					if (TableExists("dbo.RenglonComisionesDeVendedor")) {
						ExecuteDropTable("dbo.RenglonComisionesDeVendedor");
					}
					clsCompatViews.CrearVistaDboVendedor();
					clsCompatViews.CrearVistaDboRenglonComisionesDeVendedor();
				} catch (GalacException vEx) {
					throw vEx;
				}
			}
		}
		private void CrearCampoCompania_EstaIntegradaG360() {
			AddColumnBoolean("dbo.Compania", "ConectadaConG360", "CONSTRAINT ConecConG360 NOT NULL", false);
		}
	}
}