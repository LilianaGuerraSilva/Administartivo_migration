using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using LibGalac.Aos.Dal;
using System;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
	class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			CrearParametroCostoTerminadoCalculadoAPartirDe();
			CrearCamposParaElManejoDeMonedaExtranjeraEnGestionProduccion();
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
