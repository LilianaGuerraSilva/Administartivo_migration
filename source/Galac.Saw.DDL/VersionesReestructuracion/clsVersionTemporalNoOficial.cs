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
    class clsVersionTemporalNoOficial :clsVersionARestructurar {
        public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            ActivaModoMejoradoPorDefecto();
			//CrearTablaTransferenciaEntreCuentasBancarias(); Nota: se Oculta temporalmente
			//AgregaColumnasReglasDeContabilizacion();
            DisposeConnectionNoTransaction();
            return true;
        }
		
        private void ActivaModoMejoradoPorDefecto() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine($"UPDATE Adm.Caja SET UsarModoDotNet={InsSql.ToSqlValue(true)}");
            vSql.AppendLine("WHERE Consecutivo <> 0 ");
            vSql.AppendLine($"AND FamiliaImpresoraFiscal IN({InsSql.ToSqlValue("0")},{InsSql.ToSqlValue("2")},{InsSql.ToSqlValue("3")})");
            vSql.AppendLine($"AND UsaMaquinaFiscal ={InsSql.ToSqlValue(true)}");
            Execute(vSql.ToString(), 0);
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
	}
}
