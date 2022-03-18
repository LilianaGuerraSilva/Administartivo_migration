using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using LibGalac.Aos.Dal;
using System;

namespace Galac.Saw.DDL.VersionesReestructuracion {
	class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			CreacionCampoEnMovBancario();
			CrearNuevosCamposImpTransacBancarias();
			AgregaCamposCuentaBancaria();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void CreacionCampoEnMovBancario() {
			AddColumnNumeric("dbo.MovimientoBancario", "AlicuotaImpBancario", 12, 2, "", 0);
		}

		private void CrearNuevosCamposImpTransacBancarias() {
			if (AddColumnDecimal("Adm.ImpTransacBancarias", "AlicuotaC1Al4", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.ImpTransacBancarias", "d_ImpBanAlC14", "0", "AlicuotaC1Al4");
			}
			if (AddColumnDecimal("Adm.ImpTransacBancarias", "AlicuotaC5", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.ImpTransacBancarias", "d_ImpBanAlC5", "0", "AlicuotaC5");
			}
			if (AddColumnDecimal("Adm.ImpTransacBancarias", "AlicuotaC6", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.ImpTransacBancarias", "d_ImpBanAlC6", "0", "AlicuotaC6");
			}
		}

		private void AgregaCamposCuentaBancaria() {
			StringBuilder vSql = new StringBuilder();
			if (AddColumnEnumerative("Saw.CuentaBancaria", "TipoDeAlicuotaPorContribuyente", "", 0)) {
				AddDefaultConstraint("Saw.CuentaBancaria", "d_CueBanTiDeAlPoCo", "'0'", "TipoDeAlicuotaPorContribuyente");
			}
			if (AddColumnBoolean("Saw.CuentaBancaria", "GeneraMovBancarioPorIGTF", "", false)) {
				AddNotNullConstraint("Saw.CuentaBancaria", "GeneraMovBancarioPorIGTF", InsSql.CharTypeForDb(1));
			}
			vSql.AppendLine("UPDATE Saw.CuentaBancaria ");
			vSql.AppendLine("SET GeneraMovBancarioPorIGTF = 'S', ");
			vSql.AppendLine("    TipoDeAlicuotaPorContribuyente = '1' ");
			vSql.AppendLine("WHERE CodigoMoneda = 'VED' AND ConsecutivoCompania IN ");
			vSql.AppendLine("(SELECT ConsecutivoCompania ");
			vSql.AppendLine("FROM Comun.SettValueByCompany ");
			vSql.AppendLine("WHERE NameSettDefinition = 'ManejaDebitoBancario' ");
			vSql.AppendLine("AND VALUE = 'S')");
			Execute(vSql.ToString(), 0);
		}
	}
}
