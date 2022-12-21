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

namespace Galac.Saw.DDL.VersionesReestructuracion {
	class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			CrearParametroCostoTerminadoCalculadoAPartirDe();
			CrearCamposParaElManejoDeMonedaExtranjeraEnGestionProduccion();
			CrearColumnaRutaDeComercializacion();
			CrearTablaAdmVendedor();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void CrearParametroCostoTerminadoCalculadoAPartirDe() {
			AgregarNuevoParametro("CostoTerminadoCalculadoAPartirDe", "Inventario", 5, "5.5.- Producción", 5, "", eTipoDeDatoParametros.Enumerativo, "", 'N', "0");
		}
		private void CrearCamposParaElManejoDeMonedaExtranjeraEnGestionProduccion() {
			QAdvSql InsSql = new QAdvSql("");
			if (AddColumnEnumerative("Adm.OrdenDeProduccion", "CostoTerminadoCalculadoAPartirDe", "", (int)eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaLocal)) {
				AddDefaultConstraint("Adm.OrdenDeProduccion", "d_OrdDeProCosTerCalAParDe", InsSql.ToSqlValue((int)eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaLocal), "CostoTerminadoCalculadoAPartirDe");
			}
			if (AddColumnString("Adm.OrdenDeProduccion", "CodigoMonedaCostoProduccion", 4, "", "VED")) {
				AddDefaultConstraint("Adm.OrdenDeProduccion", "d_OrdDeProCoMoCoPr", InsSql.ToSqlValue("VED"), "CodigoMonedaCostoProduccion");
			}
			if (AddColumnDecimal("Adm.OrdenDeProduccion", "CambioCostoProduccion", 25, 4, "", (decimal)1.0)) {
				AddDefaultConstraint("Adm.OrdenDeProduccion", "d_OrdDeProCaCoPr", InsSql.ToSqlValue((decimal)1.0), "CambioCostoProduccion");
			}
			if (AddColumnCurrency("dbo.RenglonNotaES", "CostoUnitarioME", "", (decimal)0.0)) {
				AddDefaultConstraint("dbo.RenglonNotaES", "d_RenNotESCoUnME", InsSql.ToSqlValue((decimal)1.0), "CostoUnitarioME");
				StringBuilder vSql = new StringBuilder();
				vSql.AppendLine("UPDATE dbo.RenglonNotaES");
				vSql.AppendLine("SET CostoUnitarioME = CostoUnitario");
				Execute(vSql.ToString());
			}
		}
		private void CrearColumnaRutaDeComercializacion() {
			QAdvSql InsSql = new QAdvSql("");
			if (AddColumnEnumerative("dbo.Vendedor", "RutaDeComercializacion", "", (int)eRutaDeComercializacion.Ninguna)) {
				AddDefaultConstraint("dbo.Vendedor", "d_VenRuDeCo", InsSql.ToSqlValue((int)eRutaDeComercializacion.Ninguna), "RutaDeComercializacion");
			}
		}
		private void CrearColumnaConsecutivoVendedor(string Tabla, string NombreColumna) {
			QAdvSql InsSql = new QAdvSql("");
			if (AddColumnInteger(Tabla, NombreColumna, "")) {
				AddNotNullConstraint("dbo.Vendedor", "d_VenRuDeCo", "RutaDeComercializacion");
			}
		}
		private void CrearTablaAdmVendedor() {
			if (!TableExists("Adm.Vendedor")) {
				new Galac.Adm.Dal.Vendedor.clsVendedorED().InstalarTabla();
				//DeleteAllRelationShipVendedor();
				new Galac.Saw.DbMigrator.clsMigrarData(new string[] { "Vendedor" }).MigrarData();
				if (TableExists("dbo.Vendedor")) {
					if (ForeignKeyNameExists("fk_CobranzaVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.Cobranza");
						AddForeignKey("Adm.Vendedor", "dbo.Cobranza", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "CodigoCobrador" }, false);
					}
					if (ForeignKeyNameExists("fk_ContratoVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.Contrato");
						AddForeignKey("Adm.Vendedor", "dbo.Contrato", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "CodigoVendedor" }, false);
					}
					if (ForeignKeyNameExists("fk_CotizacionVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.Contizacion");
						AddForeignKey("Adm.Vendedor", "dbo.Cotizacion", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "CodigoVendedor" }, false);
					}
					if (ForeignKeyNameExists("fk_CxCVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.CxC");
						AddForeignKey("Adm.Vendedor", "dbo.CxC", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "CodigoVendedor" }, false);
					}
					if (ForeignKeyNameExists("fk_facturaVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.factura");
						AddForeignKey("Adm.Vendedor", "dbo.factura", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "CodigoVendedor" }, false);
					}
					if (ForeignKeyNameExists("fk_RenglonComisionesDeVendedorVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.RenglonComisionesDeVendedor");
						AddForeignKey("Adm.Vendedor", "dbo.RenglonComisionesDeVendedor", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "CodigoVendedor" }, false);
					}
					if (ForeignKeyNameExists("fk_RetirosACuentaVend")) {
						DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.RetirosACuenta");
						AddForeignKey("Adm.Vendedor", "dbo.RetirosACuenta", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "CodigoVendedor" }, false);
					}
					ExecuteDropTable("dbo.Vendedor");
					Galac.Saw.DDL.clsCompatViews.CrearVistaDboVendedor();
				}
			}
			//CreateAllRelationShipProveedor();
			//ExecuteDropTable("dbo.Vendedor");
		}
		private bool DeleteAllRelationShipVendedor() {
			bool vResult = true;
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Vendedor", "Cobranza");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Vendedor", "Contrato");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Vendedor", "cxc");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Vendedor", "Cotizacion");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Vendedor", "factura");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Vendedor", "RenglonComisionesDeVendedor");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Vendedor", "RetirosACuenta");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Vendedor", "Cobranza");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Vendedor", "Contrato");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Vendedor", "cxc");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Vendedor", "Cotizacion");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Vendedor", "factura");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Vendedor", "RenglonComisionesDeVendedor");
			vResult = vResult && DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Vendedor", "RetirosACuenta");
			return vResult;
		}
		private bool CreateAllRelationShipProveedor() {
			bool vResult = true;
			vResult = vResult && AddForeignKey("Adm.Vendedor", "Cobranza", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "Codigo" }, false);
			vResult = vResult && AddForeignKey("Adm.Vendedor", "Contrato", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "Codigo" }, false);
			vResult = vResult && AddForeignKey("Adm.Vendedor", "cxc", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "Codigo" }, false);
			vResult = vResult && AddForeignKey("Adm.Vendedor", "Cotizacion", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "Codigo" }, false);
			vResult = vResult && AddForeignKey("Adm.Vendedor", "factura", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "Codigo" }, false);
			vResult = vResult && AddForeignKey("Adm.Vendedor", "RenglonComisionesDeVendedor", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "Codigo" }, false);
			vResult = vResult && AddForeignKey("Adm.Vendedor", "RetirosACuenta", new string[] { "ConsecutivoCompania", "Codigo" }, new string[] { "ConsecutivoCompania", "Codigo" }, false);
			return vResult;
		}
	}
}
