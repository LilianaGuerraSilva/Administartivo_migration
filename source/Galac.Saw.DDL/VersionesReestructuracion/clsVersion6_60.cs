using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_60 : clsVersionARestructurar {
        public clsVersion6_60(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.60";
        }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
			ModificarTablasWincont();
			ActualizaNombresInformes();
			DisposeConnectionNoTransaction();
            return true;
        }

		private void ActualizaNombresInformes() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("UPDATE Contab.ParametrosGen SET NombreM)ayorAnaliticoDetallado = ");
			vSql.AppendLine(InsSql.ToSqlValue("Libro Mayor"));
			vSql.AppendLine(" WHERE NombreMayorAnaliticoDeta)llado = ");
			vSql.AppendLine(InsSql.ToSqlValue("Mayor Analítico"));
			Execute(vSql.ToString(), 0);
			vSql.Clear();

			vSql.AppendLine("UPDATE Contab.ParametrosGen SET NombreDiarioDeComprobante = ");
			vSql.AppendLine(InsSql.ToSqlValue("Libro Diario"));
			vSql.AppendLine(" WHERE NombreDiarioDeComprobante = ");
			vSql.AppendLine(InsSql.ToSqlValue("Diario De Comprobantes"));
			Execute(vSql.ToString(), 0);
			vSql.Clear();

			vSql.AppendLine("UPDATE Contab.ParametrosGen SET NombreBalanceGeneral = ");
			vSql.AppendLine(InsSql.ToSqlValue("Estado de Situación Financiera"));
			vSql.AppendLine(" WHERE NombreBalanceGeneral = ");
			vSql.AppendLine(InsSql.ToSqlValue("Balance General"));
			Execute(vSql.ToString(), 0);
		}

		private void ModificarTablasWincont() {
			string vSql = string.Empty;
			if (ColumnExists("Contab.ParametrosConciliacion", "ExpresarBalancesEnDiferentesMonedas")) {
				vSql = vSql + "UPDATE Contab.ParametrosConciliacion SET ExpresarBalancesEnDiferentesMonedas = (CASE WHEN ExpresarBalancesEnDiferentesMonedas = 'N' THEN '" + (int)Galac.Contab.Ccl.WinCont.eExpresarBalancesEnMonedaExtrangera.NoAplicar + "' WHEN ExpresarBalancesEnDiferentesMonedas = 'S' THEN '" + (int)Galac.Contab.Ccl.WinCont.eExpresarBalancesEnMonedaExtrangera.ConDifCambiria + "' ELSE ExpresarBalancesEnDiferentesMonedas END) ";
				Execute(vSql, 0);
			}
			AlterColumnIfExist("dbo.ASIENTO", "TasaDeCambio", InsSql.DecimalTypeForDb(30, 5), "", "");
		}
	}
}