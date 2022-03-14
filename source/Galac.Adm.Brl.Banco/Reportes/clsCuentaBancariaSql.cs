using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Banco;


namespace Galac.Adm.Brl.Banco.Reportes {
    public class clsCuentaBancariaSql {
        private QAdvSql vSqlUtil = new QAdvSql("");

        #region Metodos Generados
        public string SqlSaldosBancarios(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, bool valSoloCuentasActivas) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";

            vSql.AppendLine("SELECT");
            vSql.AppendLine("CuentaBancaria.NombreDeLaMoneda,");
            vSql.AppendLine("CuentaBancaria.Codigo,");
            vSql.AppendLine("CuentaBancaria.NumeroCuenta,");
            vSql.AppendLine("CuentaBancaria.NombreCuenta,");
            vSql.AppendLine(SqlSaldoInicial(valConsecutivoCompania, valFechaDesde) + " AS SaldoInicial,");
            vSql.AppendLine(SqlIngresoOEgreso(valConsecutivoCompania, valFechaDesde, valFechaHasta, eIngresoEgreso.Ingreso) + " AS Ingresos,");
            vSql.AppendLine(SqlIngresoOEgreso(valConsecutivoCompania, valFechaDesde, valFechaHasta, eIngresoEgreso.Egreso) + " AS Egresos");
            vSql.AppendLine("FROM");
            vSql.AppendLine("CuentaBancaria");
            vSql.AppendLine("LEFT JOIN MovimientoBancario ON CuentaBancaria.Codigo = MovimientoBancario.CodigoCtaBancaria");
            vSql.AppendLine("AND CuentaBancaria.ConsecutivoCompania = MovimientoBancario.ConsecutivoCompania");

            vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "CuentaBancaria.ConsecutivoCompania", valConsecutivoCompania);

            if (valSoloCuentasActivas) {
                vSQLWhere = vSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "CuentaBancaria.Status", (int) eStatusCtaBancaria.Activo);
			}

            vSQLWhere = vSqlUtil.WhereSql(vSQLWhere);
            vSql.AppendLine(vSQLWhere);

            vSql.AppendLine("GROUP BY");
            vSql.AppendLine("CuentaBancaria.NombreDeLaMoneda,");
            vSql.AppendLine("CuentaBancaria.Codigo,");
            vSql.AppendLine("CuentaBancaria.NumeroCuenta,");
            vSql.AppendLine("CuentaBancaria.NombreCuenta");
            vSql.AppendLine("ORDER BY");
            vSql.AppendLine("CuentaBancaria.NombreDeLaMoneda,");
            vSql.AppendLine("CuentaBancaria.Codigo");

            return vSql.ToString();
        }
        #endregion //Metodos Generados

        #region Código Programador
        private string SqlSaldoInicial(int valConsecutivoCompania, DateTime valFechaDesde) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";

            vSql.AppendLine("(SELECT");
            vSql.AppendLine("ISNULL(SUM("+ vSqlUtil.IIF("MovimientoBancario.TipoConcepto = " + vSqlUtil.EnumToSqlValue(( int ) eIngresoEgreso.Ingreso), "MovimientoBancario.Monto", "0", false) +"),0)");
            vSql.AppendLine("-ISNULL(SUM(" + vSqlUtil.IIF("MovimientoBancario.TipoConcepto = " + vSqlUtil.EnumToSqlValue(( int ) eIngresoEgreso.Egreso), "MovimientoBancario.Monto", "0", false) + "),0)");
            vSql.AppendLine("FROM MovimientoBancario");

            vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "MovimientoBancario.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vSqlUtil.SqlDateValueWithOperators(vSQLWhere, "MovimientoBancario.Fecha", valFechaDesde, vSqlUtil.CurrentDateFormat, "AND", "<");
            vSQLWhere = vSqlUtil.SqlExpressionValueWithAnd(vSQLWhere, "MovimientoBancario.CodigoCtaBancaria", "CuentaBancaria.Codigo");

            vSQLWhere = vSqlUtil.WhereSql(vSQLWhere);
            vSql.AppendLine(vSQLWhere + ")");

            return vSql.ToString();
        }

        private string SqlIngresoOEgreso(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eIngresoEgreso valIngresoEgreso) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";

            vSql.AppendLine("(SELECT");
            vSql.AppendLine("ISNULL(SUM(" + vSqlUtil.IIF("MovimientoBancario.TipoConcepto = " + vSqlUtil.EnumToSqlValue(( int ) valIngresoEgreso), "MovimientoBancario.Monto", "0", false) + "),0)");
            vSql.AppendLine("FROM MovimientoBancario");

            vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "MovimientoBancario.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "MovimientoBancario.Fecha", valFechaDesde, valFechaHasta);
            vSQLWhere = vSqlUtil.SqlExpressionValueWithAnd(vSQLWhere, "MovimientoBancario.CodigoCtaBancaria", "CuentaBancaria.Codigo");

            vSQLWhere = vSqlUtil.WhereSql(vSQLWhere);
            vSql.AppendLine(vSQLWhere + ")");

            return vSql.ToString();
        }
		#endregion
	} //End of class clsCuentaBancariaSql

} //End of namespace Galac.Adm.Brl.Banco

