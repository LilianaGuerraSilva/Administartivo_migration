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
			CrearTablaTransferenciaEntreCuentasBancarias();
			AgregaColumnasReglasDeContabilizacion();
            DisposeConnectionNoTransaction();
            return true;
        }
		private void CrearTablaTransferenciaEntreCuentasBancarias() {
			if (!TableExists("Adm.TransferenciaEntreCuentasBancarias")) {
				new Galac.Adm.Dal.Banco.clsTransferenciaEntreCuentasBancariasED().InstalarTabla();
				new Galac.Adm.Dal.Banco.clsTransferenciaEntreCuentasBancariasED().BorrarVistasYSps();
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
