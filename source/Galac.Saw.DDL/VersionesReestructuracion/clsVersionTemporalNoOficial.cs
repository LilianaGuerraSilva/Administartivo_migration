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
            CrearParametroCostoTerminadoCalculadoAPartirDe();
            CrearCamposParaElManejoDeMonedaExtranjeraEnGestionProduccion();
            CrearColumnaRutaDeComercializacion();
            CrearTablaAdmVendedor();
			CrearTablaTransferenciaEntreCuentasBancarias();
			AgregaColumnasReglasDeContabilizacion();
			AgregarConceptosBancarioReversosTransferencia();
			AgregarParametroTransferenciaBancaria();
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
					if (TableExists("dbo.Vendedor")) {
						if (ForeignKeyNameExists("fk_CobranzaVend")) {
							DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.Cobranza");
							CrearColumnaConsecutivoVendedor("dbo.Cobranza", "ConsecutivoCobrador", " CONSTRAINT nnCobCoCo NOT NULL");
							LlenarColumnaConsecutivoVendedor("dbo.Cobranza", "ConsecutivoCobrador", "CodigoCobrador");
							AddForeignKey("Adm.Vendedor", "dbo.Cobranza", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoCobrador" }, false, false);
							DropColumnIfExist("dbo.Cobranza", "CodigoCobrador");
						}
						if (ForeignKeyNameExists("fk_ContratoVend")) {
							DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.Contrato");
							CrearColumnaConsecutivoVendedor("dbo.Contrato", "ConsecutivoVendedor", " CONSTRAINT nnConCoVe NOT NULL");
							LlenarColumnaConsecutivoVendedor("dbo.Contrato", "ConsecutivoVendedor", "CodigoVendedor");
							AddForeignKey("Adm.Vendedor", "dbo.Contrato", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
							//DropColumnIfExist("dbo.Contrato", "CodigoVendedor");
						}
						if (ForeignKeyNameExists("fk_CotizacionVend")) {
							DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.Cotizacion");
							CrearColumnaConsecutivoVendedor("dbo.Cotizacion", "ConsecutivoVendedor", " CONSTRAINT nnCotCoVe NOT NULL");
							LlenarColumnaConsecutivoVendedor("dbo.Cotizacion", "ConsecutivoVendedor", "CodigoVendedor");
							AddForeignKey("Adm.Vendedor", "dbo.Cotizacion", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
							DropColumnIfExist("dbo.Cotizacion", "CodigoVendedor");
						}
						if (ForeignKeyNameExists("fk_CxCVend")) {
							DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.CxC");
							CrearColumnaConsecutivoVendedor("dbo.CxC", "ConsecutivoVendedor", " CONSTRAINT nnCxCCoVe NOT NULL");
							LlenarColumnaConsecutivoVendedor("dbo.CxC", "ConsecutivoVendedor", "CodigoVendedor");
							AddForeignKey("Adm.Vendedor", "dbo.CxC", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
							DropColumnIfExist("dbo.CxC", "CodigoVendedor");
						}
						if (ForeignKeyNameExists("fk_facturaVend")) {
							DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.factura");
							CrearColumnaConsecutivoVendedor("dbo.factura", "ConsecutivoVendedor", " CONSTRAINT nnFacCoVe NOT NULL");
							LlenarColumnaConsecutivoVendedor("dbo.factura", "ConsecutivoVendedor", "CodigoVendedor");
							AddForeignKey("Adm.Vendedor", "dbo.factura", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
							//DropColumnIfExist("dbo.factura", "CodigoVendedor");
						}
						if (ForeignKeyNameExists("fk_RenglonComisionesDeVendedorVend")) {
							DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.RenglonComisionesDeVendedor");
						}
						if (ForeignKeyNameExists("fk_RetirosACuentaVend")) {
							DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.RetirosACuenta");
							CrearColumnaConsecutivoVendedor("dbo.RetirosACuenta", "ConsecutivoVendedor", " CONSTRAINT nnRetACuenCoVe NOT NULL");
							LlenarColumnaConsecutivoVendedor("dbo.RetirosACuenta", "ConsecutivoVendedor", "CodigoVendedor");
							AddForeignKey("Adm.Vendedor", "dbo.RetirosACuenta", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
							DropColumnIfExist("dbo.RetirosACuenta", "CodigoVendedor");
						}
						if (!ColumnExists("dbo.Cliente", "ConsecutivoVendedor")) {
							CrearColumnaConsecutivoVendedor("dbo.Cliente", "ConsecutivoVendedor", " CONSTRAINT nnConCliVe NOT NULL");
							LlenarColumnaConsecutivoVendedor("dbo.Cliente", "ConsecutivoVendedor", "CodigoVendedor");
						}
						ExecuteDropTable("dbo.Vendedor");
						ExecuteDropTable("dbo.RenglonComisionesDeVendedor");
						clsCompatViews.CrearVistaDboVendedor();
						clsCompatViews.CrearVistaDboVendedorDetalleComisiones();
					}
				} catch (GalacException vEx) {
					throw vEx;
				}
			}
		}
		private void CrearTablaTransferenciaEntreCuentasBancarias() {
			if (!TableExists("Adm.TransferenciaEntreCuentasBancarias")) {
				new Galac.Adm.Dal.Banco.clsTransferenciaEntreCuentasBancariasED().InstalarTabla();
			}
		}
		private void AgregaColumnasReglasDeContabilizacion() {
			if (!ColumnExists("Saw.ReglasDeContabilizacion", "TipoContabilizacionTransfCtas")) {
				if( AddColumnEnumerative("Saw.ReglasDeContabilizacion", "TipoContabilizacionTransfCtas", "", LibConvert.EnumToDbValue(0))) {
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConTiCoTrCt", "'0'", "TipoContabilizacionTransfCtas");
                }
				if (AddColumnEnumerative("Saw.ReglasDeContabilizacion", "ContabIndividualTransfCtas", "", LibConvert.EnumToDbValue(1))){
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCoInTrCt", "'1'", "ContabIndividualTransfCtas");
				}		
				if (AddColumnEnumerative("Saw.ReglasDeContabilizacion", "ContabPorLoteTransfCtas", "", LibConvert.EnumToDbValue(0))) {
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuTrCtBaDe", "'0'", "ContabPorLoteTransfCtas");
				}
				if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaTransfCtasBancoDestino", 30, "", "")) {
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCoPoLoTrCt", _insSql.ToSqlValue(""), "CuentaTransfCtasBancoDestino");
				}
				if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaTransfCtasGastoComOrigen", 30, "", "")) {
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuTrCtGaCoOr", _insSql.ToSqlValue(""), "CuentaTransfCtasGastoComOrigen");
				}
				if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaTransfCtasGastoComDestino", 30, "", "")) {
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuTrCtGaCoDe", _insSql.ToSqlValue(""), "CuentaTransfCtasGastoComDestino");
				}
				if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaTransfCtasBancoOrigen", 30, "", "")){
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuTrCtBaOr", _insSql.ToSqlValue(""), "CuentaTransfCtasBancoOrigen");
				}
				if (AddColumnString("Saw.ReglasDeContabilizacion", "TransfCtasSigasTipoComprobante", 2, "", "")) {
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConTrCtSiTiCo", _insSql.ToSqlValue(""), "TransfCtasSigasTipoComprobante");
				}
				if (AddColumnBoolean("Saw.ReglasDeContabilizacion", "EditarComprobanteAfterInsertTransfCtas", "", false)) {
					AddNotNullConstraint("Saw.ReglasDeContabilizacion", "EditarComprobanteAfterInsertTransfCtas", InsSql.CharTypeForDb(1)) ;
				}
			}
		}

		private void AgregarConceptosBancarioReversosTransferencia() {
			QAdvSql InsSql = new QAdvSql("");
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("INSERT INTO Adm.ConceptoBancario(Consecutivo, Codigo,Descripcion,Tipo,NombreOperador,FechaUltimaModificacion)");
			vSql.AppendLine(" SELECT (SELECT MAX(Consecutivo) + 1 FROM Adm.ConceptoBancario)," + InsSql.ToSqlValue("60347") + "," + InsSql.ToSqlValue("REV AUTOMATICO TRANSF INGRESO") + ", " + InsSql.ToSqlValue("0") + ", " + InsSql.ToSqlValue("JEFE") + ", " + InsSql.ToSqlValue(LibDate.Today()));
			vSql.AppendLine(" WHERE NOT EXISTS (SELECT Codigo FROM Adm.ConceptoBancario WHERE Codigo = '60347')");
			Execute(vSql.ToString());
			vSql.Clear();
			vSql.AppendLine("INSERT INTO Adm.ConceptoBancario(Consecutivo, Codigo,Descripcion,Tipo,NombreOperador,FechaUltimaModificacion)");
			vSql.AppendLine(" SELECT (SELECT MAX(Consecutivo) + 1 FROM Adm.ConceptoBancario)," + InsSql.ToSqlValue("60348") + "," + InsSql.ToSqlValue("REV AUTOMATICO TRANSF EGRESO") + ", " + InsSql.ToSqlValue("1") + ", " + InsSql.ToSqlValue("JEFE") + ", " + InsSql.ToSqlValue(LibDate.Today()));
			vSql.AppendLine(" WHERE NOT EXISTS (SELECT Codigo FROM Adm.ConceptoBancario WHERE Codigo = '60348')");
			Execute(vSql.ToString());
		}
		private void AgregarParametroTransferenciaBancaria() {
			if (AgregarNuevoParametro("ConceptoBancarioReversoTransfIngreso", "Bancos", 7, "7.5.- Transferencias Bancarias", 5, "", eTipoDeDatoParametros.Enumerativo, "", 'N', "")){
				if (RecordCountOfSql("SELECT * FROM Adm.ConceptoBancario WHERE Codigo = '60347' AND Descripcion = 'REV AUTOMATICO TRANSF INGRESO' AND Tipo = '0'") > 0) {
					Execute("UPDATE Comun.SettValueByCompany SET Value = '60347' WHERE NameSettDefinition = 'ConceptoBancarioReversoTransfIngreso'");
				}
			}
			if (AgregarNuevoParametro("ConceptoBancarioReversoTransfEgreso", "Bancos", 7, "7.5.- Transferencias Bancarias", 5, "", eTipoDeDatoParametros.Enumerativo, "", 'N', "")) {
				if (RecordCountOfSql("SELECT * FROM Adm.ConceptoBancario WHERE Codigo = '60348' AND Descripcion = 'REV AUTOMATICO TRANSF EGRESO' AND Tipo = '1'") > 0) {
					Execute("UPDATE Comun.SettValueByCompany SET Value = '60348' WHERE NameSettDefinition = 'ConceptoBancarioReversoTransfEgreso'");
				}
			}
		}
	}
}
