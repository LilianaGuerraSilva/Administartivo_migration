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
			ModificarTablasWincont();
			ActualizaNombresInformes();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void ActualizaNombresInformes() {
			string vSql = string.Empty;
			vSql = "UPDATE Contab.ParametrosGen SET NombreMayorAnaliticoDetallado = ";
			vSql = vSql + InsSql.ToSqlValue("Libro Mayor");
			vSql = vSql + " WHERE NombreMayorAnaliticoDetallado = ";
			vSql = vSql + InsSql.ToSqlValue("Mayor Analítico");
			if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
				Execute(vSql, 0);
			}
			vSql = "UPDATE Contab.ParametrosGen SET NombreDiarioDeComprobante = ";
			vSql = vSql + InsSql.ToSqlValue("Libro Diario");
			vSql = vSql + " WHERE NombreDiarioDeComprobante = ";
			vSql = vSql + InsSql.ToSqlValue("Diario De Comprobantes");
			if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
				Execute(vSql, 0);
			}

			vSql = "UPDATE Contab.ParametrosGen SET NombreBalanceGeneral = ";
			vSql = vSql + InsSql.ToSqlValue("Estado de Situación Financiera");
			vSql = vSql + " WHERE NombreBalanceGeneral = ";
			vSql = vSql + InsSql.ToSqlValue("Balance General");
			if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
				Execute(vSql, 0);
			}
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
