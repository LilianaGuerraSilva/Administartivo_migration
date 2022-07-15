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
			CrearTablaTransferenciaEntreCuentasBancarias();
			AgregaColumnasReglasDeContabilizacion();
            DisposeConnectionNoTransaction();
            return true;
        }
		
        private void ActivaModoMejoradoPorDefecto() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine($"UPDATE Adm.Caja SET UsarModoDotNet={InsSql.ToSqlValue(true)}");
            vSql.AppendLine("WHERE Consecutivo <> 0 ");
            vSql.AppendLine($"AND FamiliaImpresoraFiscal IN({InsSql.ToSqlValue("0")},{InsSql.ToSqlValue("2")},{InsSql.ToSqlValue("3")},{InsSql.ToSqlValue("6")}) ");
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
				AddColumnEnumerative("Saw.ReglasDeContabilizacion", "TipoContabilizacionTransfCtas", "",LibConvert.EnumToDbValue(0));
				AddColumnEnumerative("Saw.ReglasDeContabilizacion", "ContabIndividualTransfCtas", "", LibConvert.EnumToDbValue(1));
				AddColumnEnumerative("Saw.ReglasDeContabilizacion", "ContabPorLoteTransfCtas", "", LibConvert.EnumToDbValue(0));
				AddColumnString("Saw.ReglasDeContabilizacion", "CuentaTransfCtasBancoDestino", 30, "", "");
				AddColumnString("Saw.ReglasDeContabilizacion", "CuentaTransfCtasGastoComOrigen", 30, "", "");
				AddColumnString("Saw.ReglasDeContabilizacion", "CuentaTransfCtasGastoComDestino", 30, "", "");
				AddColumnString("Saw.ReglasDeContabilizacion", "CuentaTransfCtasBancoOrigen", 30, "", "");
				AddColumnString("Saw.ReglasDeContabilizacion", "TransfCtasSigasTipoComprobante", 2, "", "");
				AddColumnBoolean("Saw.ReglasDeContabilizacion", "EditarComprobanteAfterInsertTransfCtas", "",false);
			}
		}
	}
}
