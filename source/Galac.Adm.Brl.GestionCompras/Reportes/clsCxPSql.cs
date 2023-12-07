using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.DefGen;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.GestionCompras.Reportes {
    public class clsCxPSql {
		private QAdvSql insSql;

		public clsCxPSql() {
			insSql = new QAdvSql("");
        }

		#region Metodos Generados
		public string SqlCxPEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eInformeStatusCXC_CXP valStatusCxP, eMonedaDelInformeMM valMonedaDelInforme, string valMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, bool valMostrarNroComprobanteContable) {
            StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "cxP.ConsecutivoCompania", valConsecutivoCompania);
			vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "cxP.Fecha", valFechaDesde, valFechaHasta);
			if (valStatusCxP != eInformeStatusCXC_CXP.Todos) {
				vSQLWhere = insSql.SqlEnumValueWithAnd(vSQLWhere, "cxP.Status", (int)valStatusCxP);
			}

			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlMontoToal = "(CxP.MontoExento + CxP.MontoGravado + CxP.MontoIva)";
			string vSqlMontoOriginal = insSql.IIF("CxP.Status = " + insSql.EnumToSqlValue(2), "0", vSqlMontoToal, true);
			string vSqlMontoRestante = insSql.IIF("CxP.Status = " + insSql.EnumToSqlValue(2), "0", "(" + vSqlMontoToal + " - CxP.MontoAbonado)", true);
			string vSqlCambioOriginal = "CxP.CambioAbolivares";
			string vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
			string vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valMoneda) + " AND FechaDeVigencia <= CxP.Fecha ORDER BY FechaDeVigencia DESC), 1)";
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;

			if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlMontoOriginal = insSql.RoundToNDecimals(vSqlMontoOriginal + " * " + vSqlCambio, 2);
				vSqlMontoRestante = insSql.RoundToNDecimals(vSqlMontoRestante + " * " + vSqlCambio, 2);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				vSqlCambio = insSql.IIF("CxP.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
				vSqlMontoOriginal = insSql.RoundToNDecimals(vSqlMontoOriginal + " / " + vSqlCambio, 2);
				vSqlMontoRestante = insSql.RoundToNDecimals(vSqlMontoRestante + " / " + vSqlCambio, 2);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
			}
			/* FIN */

			string vSqlStatus = "(CASE CxP.Status WHEN '0' THEN 'Por Cancelar' WHEN '1' THEN 'Cancelado' WHEN '2' THEN 'Cheque Devuelto' WHEN '3' THEN 'Abonado' WHEN '4' THEN 'Anulado' WHEN '5' THEN 'Refinanciado' ELSE 'N/A' END)";

			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	CxP.Status, ");
			vSql.AppendLine("	" + vSqlStatus + " AS StatusStr, ");
			vSql.AppendLine("	CxP.CodigoMoneda, ");
			vSql.AppendLine("	CxP.Moneda, ");
			vSql.AppendLine("	CxP.Fecha, ");
			vSql.AppendLine("	CxP.Numero, ");
			vSql.AppendLine("	CxP.CodigoProveedor, ");
			vSql.AppendLine("	Adm.Proveedor.NombreProveedor, ");
			vSql.AppendLine("	" + vSqlMontoOriginal + " AS MontoOriginal, ");
			vSql.AppendLine("	" + vSqlMontoRestante + " AS MontoRestante, ");
			vSql.AppendLine("	" + vSqlCambio + " AS Cambio, ");
			vSql.AppendLine("	CxP.Observaciones ");
			if (valMostrarNroComprobanteContable && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "CaracteristicaDeContabilidadActiva")) {
				StringBuilder vSqlNroComprobanteContable = new StringBuilder();
				vSqlNroComprobanteContable.AppendLine(" (SELECT TOP 1 C.Numero ");
				vSqlNroComprobanteContable.AppendLine("FROM COMPROBANTE C INNER JOIN PERIODO P ON C.ConsecutivoPeriodo = P.ConsecutivoPeriodo ");
				vSqlNroComprobanteContable.AppendLine("WHERE P.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
				vSqlNroComprobanteContable.AppendLine(" AND C.NoDocumentoOrigen = CxP.ConsecutivoCxp ");
				vSqlNroComprobanteContable.AppendLine(" AND C.GeneradoPor = " + insSql.EnumToSqlValue((int)eComprobanteGeneradoPorVBSaw.eCG_CXP));
				vSqlNroComprobanteContable.AppendLine(" AND " + insSql.SqlDateValueBetween("", "CxP.Fecha", valFechaDesde, valFechaHasta) + ")");
				vSql.AppendLine("   , " + vSqlNroComprobanteContable.ToString() + " AS NroComprobanteContable");
			}
			vSql.AppendLine("FROM cxP INNER JOIN Adm.Proveedor ");
			vSql.AppendLine("	ON cxP.CodigoProveedor = Adm.Proveedor.CodigoProveedor ");
			vSql.AppendLine("	AND cxP.ConsecutivoCompania = Adm.Proveedor.ConsecutivoCompania");
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			vSql.AppendLine("ORDER BY cxP.Status, cxP.Moneda, cxP.Fecha, cxP.Numero, cxP.CodigoProveedor");

			return vSql.ToString();
		}
        #endregion //Metodos Generados


    } //End of class clsCxPSql

} //End of namespace Galac..Brl.ComponenteNoEspecificado

