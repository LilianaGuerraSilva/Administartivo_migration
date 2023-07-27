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
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;

namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial: clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			CrearRutaDeComercializacion();
			CrearTablaAdmVendedor();
			AmpliarCampoUbicacion();			
			CamposMonedaExtranjeraEnCajaApertura();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void CamposMonedaExtranjeraEnCajaApertura() {
			if (AddColumnDecimal("Adm.CajaApertura", "MontoAperturaME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoApME", "0", "MontoAperturaME");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoCierreME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoCiME", "0", "MontoCierreME");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoEfectivoME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoEfME", "0", "MontoEfectivoME");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoTarjetaME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoTaME", "0", "MontoTarjetaME");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoChequeME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoChME", "0", "MontoChequeME");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoDepositoME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoDeME", "0", "MontoDepositoME");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoAnticipoME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoAnME", "0", "MontoAnticipoME");
			}
			Execute("UPDATE Adm.CajaApertura SET CodigoMoneda = 'VED', Cambio = 1 WHERE CodigoMoneda IS NULL OR CodigoMoneda = '' OR CodigoMoneda = 'VES' OR Cambio IS NULL OR Cambio = 0");
			if (!ForeignKeyNameExists("fk_CajaAperturaMoneda")) {
				AddForeignKey("dbo.Moneda", "Adm.CajaApertura", new string[] { "Codigo" }, new string[] { "CodigoMoneda" }, false, true);
			}
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

		private void CrearColumnaConsecutivoVendedor(string valTabla, string valNombreColumna, string valConstraint) {
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

		private void CrearRutaDeComercializacion() {
			if (new Saw.Dal.Tablas.clsRutaDeComercializacionED().InstalarTabla()) {
				LLenarRutaDeComercializacionPorCompania();
			}
		}

		private void LLenarRutaDeComercializacionPorCompania() {
			try {
				StringBuilder vSql = new StringBuilder();
				vSql.AppendLine("INSERT INTO Saw.RutaDeComercializacion (ConsecutivoCompania,Consecutivo,Descripcion,NombreOperador,FechaUltimaModificacion)");
				vSql.AppendLine("SELECT ConsecutivoCompania,1,'NO ASIGNADA','JEFE',GETDATE()");
				vSql.AppendLine("FROM Compania");
				LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), null, "", 0);
			} catch (Exception) {
				throw;
			}
		}

		private void AmpliarCampoUbicacion() {
			string vSQL = "ALTER TABLE " + LibDbo.ToFullDboName("dbo.ExistenciaPorAlmacen") + " ALTER COLUMN Ubicacion VARCHAR(100) NULL";
			Execute(vSQL, 0);
		}
	}
}   