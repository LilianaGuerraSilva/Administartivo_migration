using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Lib;
using LibGalac.Aos.DefGen;
using Galac.Adm.Ccl.GestionCompras;
using eTipoDeTransaccion = Galac.Saw.Lib.eTipoDeTransaccion;

namespace Galac.Adm.Brl.GestionCompras.Reportes {
	public class clsProveedorSql {
		private QAdvSql insSql;
		Comun.Ccl.TablasGen.ICambioPdn insCambioMoneda;

		public clsProveedorSql() {
			insSql = new QAdvSql("");
			insCambioMoneda = new Comun.Brl.TablasGen.clsCambioNav();
		}

		#region Metodos Generados
		public string SqlHistoricoDeProveedor(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoProveedor, eMonedaDelInformeMM valMonedaDelInforme, string valCodigoMoneda, string valNombreMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, eProveedorOrdenadosPor valProveedorOrdenarPor) {
			StringBuilder vSql = new StringBuilder();
			string vOrdenarPor = valProveedorOrdenarPor == eProveedorOrdenadosPor.PorCodigo ? "Codigo" : "Nombre";
			vSql.AppendLine(";WITH");
			vSql.AppendLine(SqlCTEInfoCxPHistoricoProveedor(valConsecutivoCompania, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, valNombreMoneda, valFechaDesde, valFechaHasta));
			vSql.AppendLine(",");
			vSql.AppendLine(SqlCTECxPHistoricoProveedor(valConsecutivoCompania, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, valNombreMoneda, valFechaDesde, valFechaHasta));
			vSql.AppendLine(",");
			vSql.AppendLine(SqlCTEAnticipoHistoricoProveedore(valConsecutivoCompania, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, valNombreMoneda, valFechaDesde, valFechaHasta));
			vSql.AppendLine(",");
			vSql.AppendLine(SqlCTESaldoInicialCxPHistoricoProveedor(valConsecutivoCompania, valFechaDesde, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, valNombreMoneda));
			vSql.AppendLine(",");
			vSql.AppendLine(SqlCTE_SaldoInicialAnticipoHistoricoProveedor(valConsecutivoCompania, valFechaDesde, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, valNombreMoneda));
			vSql.AppendLine(SqlSelectInfocxPHistoricoProveedor(valCodigoProveedor, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio));
			vSql.AppendLine(" UNION ");
			vSql.AppendLine(SqlSelectcxPHistoricoProveedor(valCodigoProveedor, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio));
			vSql.AppendLine(" UNION ");
			vSql.AppendLine(SqlSelectAnticipoHistoricoProveedor(valCodigoProveedor, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio));
			vSql.AppendLine($" ORDER BY {vOrdenarPor}, TipoReporte, MonedaReporte, NumeroDocumentoGrupo, FechaDocumento, FechaPago, NumeroPago");
			return vSql.ToString();
		}

		private string SqlSelectInfocxPHistoricoProveedor(string valCodigoProveedor, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			if (!LibString.IsNullOrEmpty(valCodigoMoneda)) {
				vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Codigo", valCodigoProveedor);
			}
			string vSaldoInicial = insSql.IsNull("(SELECT SaldoInicialOriginal FROM CTE_SaldoInicialCxPHistoricoProveedor WHERE (CodigoProveedor = CTE_InfoCxPHistoricoProveedor.Codigo AND CodigoMoneda = CTE_InfoCxPHistoricoProveedor.CodMoneda))", "0");
			string vCalcularSaldoInicialcxP = sqlCalcularSaldoInicialcxP(valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, vSaldoInicial);
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	'0' AS TipoReporte, ");
			vSql.AppendLine("	Codigo, ");
			vSql.AppendLine("	Nombre, ");
			vSql.AppendLine("	MonedaReporte, ");
			vSql.AppendLine("	MonedaReporte AS Moneda, ");
			vSql.AppendLine("	TituloTipoReporte,");
			vSql.AppendLine($"	{vCalcularSaldoInicialcxP} AS SaldoInicial,");
			vSql.AppendLine("	FechaDocumento, ");
			vSql.AppendLine("	FechaVencimiento, ");
			vSql.AppendLine("	TipoDeDocumento, ");
			vSql.AppendLine("	NumeroDocumento, ");
			vSql.AppendLine("	NumeroDocumentoGrupo, ");
			vSql.AppendLine("	MontoOriginal, ");
			vSql.AppendLine("	SaldoActual, ");
			vSql.AppendLine("	TipoDocumentoDetalle, ");
			vSql.AppendLine("	'' AS NumeroPago, ");
			vSql.AppendLine("	'' AS FechaPago, ");
			vSql.AppendLine("	0 AS MontoPagado, ");
			vSql.AppendLine("	'' AS StatusPago");
			vSql.AppendLine("FROM CTE_InfoCxPHistoricoProveedor");
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			return vSql.ToString();
		}

		private string SqlSelectcxPHistoricoProveedor(string valCodigoProveedor, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			if (!LibString.IsNullOrEmpty(valCodigoProveedor)) {
				vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Codigo", valCodigoProveedor);
			}
			string vSaldoInicial = insSql.IsNull("( SELECT SaldoInicialOriginal FROM CTE_SaldoInicialcxPHistoricoProveedor WHERE (CodigoProveedor = CTE_cxPHistoricoProveedor.Codigo AND CodigoMoneda = CTE_cxPHistoricoProveedor.CodMoneda))", "0");
			string vCalcularSaldoInicialcxP = sqlCalcularSaldoInicialcxP(valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, vSaldoInicial);
			vSql.AppendLine("SELECT");
			vSql.AppendLine("	'0' AS TipoReporte, ");
			vSql.AppendLine("	Codigo, ");
			vSql.AppendLine("	Nombre, ");
			vSql.AppendLine("	MonedaReporte, ");
			vSql.AppendLine("	Moneda, ");
			vSql.AppendLine("	TituloTipoReporte,");
			vSql.AppendLine($"	{vCalcularSaldoInicialcxP} AS SaldoInicial,");
			vSql.AppendLine("	FechaDocumento, ");
			vSql.AppendLine("	FechaVencimiento, ");
			vSql.AppendLine("	TipoDeDocumento, ");
			vSql.AppendLine("	NumeroDocumento, ");
			vSql.AppendLine("	NumeroDocumentoGrupo, ");
			vSql.AppendLine("	0 AS MontoOriginal, ");
			vSql.AppendLine("	0 AS SaldoActual, ");
			vSql.AppendLine("	TipoDocumentoDetalle, ");
			vSql.AppendLine("	NumeroPago, ");
			vSql.AppendLine("	FechaPago, ");
			vSql.AppendLine("	MontoPagado, ");
			vSql.AppendLine("	StatusPago");
			vSql.AppendLine("FROM CTE_cxPHistoricoProveedor");
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			return vSql.ToString();
		}

		private string SqlSelectAnticipoHistoricoProveedor(string valCodigoProveedor, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			if (!LibString.IsNullOrEmpty(valCodigoProveedor)) {
				vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Codigo", valCodigoProveedor);
			}
			string vSaldoInicial = insSql.IsNull("(SELECT SaldoInicialOriginal FROM CTE_SaldoInicialAnticipoHistoricoProveedor WHERE (CodigoProveedor = CTE_AnticipoHistoricoProveedor.Codigo) AND (CodigoMoneda = CTE_AnticipoHistoricoProveedor.CodigoMonedaAnticipo))", "0");
			string vCalcularSaldoInicialAnticipo = sqlCalcularSaldoInicialAnticipo(valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, vSaldoInicial);
			vSql.AppendLine("SELECT");
			vSql.AppendLine("	TipoReporte, ");
			vSql.AppendLine("	Codigo, ");
			vSql.AppendLine("	Nombre, ");
			vSql.AppendLine("	MonedaAnticipo AS MonedaReporte, ");
			vSql.AppendLine("	MonedaReporte AS Moneda, ");
			vSql.AppendLine("	TituloTipoReporte,");
			vSql.AppendLine($"	{vCalcularSaldoInicialAnticipo} AS SaldoInicial,");
			vSql.AppendLine("	FechaDocumento, ");
			vSql.AppendLine("	'' AS FechaVencimiento, ");
			vSql.AppendLine("	TipoDeDocumento, ");
			vSql.AppendLine("	NumeroDocumento, ");
			vSql.AppendLine("	NumeroDocumentoGrupo, ");
			vSql.AppendLine("	MontoOriginal, ");
			vSql.AppendLine("	SaldoActual, ");
			vSql.AppendLine("	TipoDocumentoDetalle, ");
			vSql.AppendLine("	NumeroPago, ");
			vSql.AppendLine("	FechaPago, ");
			vSql.AppendLine("	(CASE WHEN StatusPago = '1' THEN 0 ELSE MontoPagado END) AS MontoCobrado, ");
			vSql.AppendLine("	StatusPago");
			vSql.AppendLine("FROM CTE_AnticipoHistoricoProveedor");
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			return vSql.ToString();
		}
		#endregion //Metodos Generados      
		private string SqlCTEInfoCxPHistoricoProveedor(int valConsecutivoCompania, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valNombreMoneda, DateTime valFechaDesde, DateTime valFechaHasta) {
			StringBuilder vSql = new StringBuilder();
			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "cxP.CambioAbolivares";
			string vSqlMontoTotal = "(cxP.MontoExento + cxP.MontoGravado + cxP.MontoIva)";
			string vSqlMontoOriginal = insSql.IIF("cxP.Status = " + insSql.EnumToSqlValue((int)eStatusDocumentoCxP.Anulado), "0", vSqlMontoTotal, true);
			string vSqlMontoRestante = insSql.IIF("cxP.Status = " + insSql.EnumToSqlValue((int)eStatusDocumentoCxP.Anulado), "0", "(" + vSqlMontoTotal + " - cxP.MontoAbonado)", true);
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;
			if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = cxP.CodigoMoneda AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlMontoOriginal = insSql.RoundToNDecimals(vSqlMontoOriginal + " * " + vSqlCambio, 2);
				vSqlMontoRestante = insSql.RoundToNDecimals(vSqlMontoRestante + " * " + vSqlCambio, 2);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				vSqlCambioMasCercano = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= cxP.Fecha ORDER BY FechaDeVigencia DESC)", "1");
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				vSqlCambio = insSql.IIF("cxP.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
				vSqlMontoOriginal = insSql.IIF("CxP.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), insSql.RoundToNDecimals(vSqlMontoOriginal + " / " + vSqlCambio, 2),vSqlMontoOriginal,true);
				vSqlMontoRestante = insSql.IIF("CxP.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), insSql.RoundToNDecimals(vSqlMontoRestante + " / " + vSqlCambio, 2),vSqlMontoRestante,true);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
			}
			/* FIN */
			vSql.AppendLine("CTE_InfoCxPHistoricoProveedor AS (");
			vSql.AppendLine("SELECT");
			vSql.AppendLine("	cxP.CodigoProveedor AS Codigo, ");
			vSql.AppendLine("	Proveedor.NombreProveedor AS Nombre,");
			vSql.AppendLine("	cxP.Numero AS NumeroDocumento, ");
			vSql.AppendLine("	cxP.Fecha AS FechaDocumento, ");
			vSql.AppendLine("	cxP.FechaVencimiento, ");
			vSql.AppendLine("	cxP.Moneda AS MonedaReporte, ");
			vSql.AppendLine("	cxP.CodigoMoneda AS CodMoneda, ");
			vSql.AppendLine($"	{vSqlCambio} AS CambioABolivares, ");
			vSql.AppendLine($"	(CASE cxP.TipoDeCxp WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.FACTURA)} THEN 'Factura' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.GIRO)} THEN 'Giro' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.CHEQUEDEVUELTO)} THEN 'Cheque Devuelto' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTADECREDITO)} THEN 'Nota de Crédito' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTADEDEBITO)} THEN 'Nota de Débito' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTAENTREGA)} THEN 'Nota de Entrega' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOASIGNADO)} THEN 'No Asignado' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.BOLETADEVENTA)} THEN 'Boleta de Venta' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.TICKETMAQUINAREGISTRADORA)} THEN 'Ticket Máquina Registradora' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.RECIBOPORHONORARIOS)} THEN 'Recibo por Honorarios' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.LIQUIDACIONDECOMPRA)} THEN 'Liquidación de Compra' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.OTROS)} THEN 'Otros' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTADECREDITOCOMPROBANTEFISCAL)} THEN 'Nota de Crédito Comprobante Fiscal' END) AS TipoDeDocumento, ");
			vSql.AppendLine("	'Cuentas por Pagar' AS TituloTipoReporte, ");
			vSql.AppendLine("	'CxP' AS TipoDocumentoDetalle, ");
			vSql.AppendLine("	CAST(cxP.CodigoProveedor AS VARCHAR) + CHAR(9) + CAST(cxP.Numero AS VARCHAR) AS NumeroDocumentoGrupo, ");
			vSql.AppendLine($"	 {vSqlMontoOriginal} AS MontoOriginal, ");
			vSql.AppendLine($"	{vSqlMontoRestante} AS SaldoActual");
			vSql.AppendLine("FROM Proveedor INNER JOIN cxP ON Proveedor.CodigoProveedor = cxP.CodigoProveedor AND Proveedor.ConsecutivoCompania = cxP.ConsecutivoCompania");
			vSql.AppendLine($"WHERE (cxP.Status <> '4') AND cxP.ConsecutivoCompania = {LibConvert.ToStr(valConsecutivoCompania)} AND ");
			vSql.AppendLine(insSql.SqlDateValueBetween("", "CxP.Fecha", valFechaDesde, valFechaHasta));
			vSql.AppendLine(")");
			return vSql.ToString();
		}

		private string SqlCTECxPHistoricoProveedor(int valConsecutivoCompania, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valNombreMoneda, DateTime valFechaDesde, DateTime valFechaHasta) {
			StringBuilder vSql = new StringBuilder();
			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
            string vSqlCambioOriginal = "DocumentoPagado.CambioAMonedaDelPago";			
			string vSqlMontoTotal = "DocumentoPagado.MontoAbonado";            
            string vSqlMontoPagado = insSql.IIF("Pago.StatusOrdenDePago = " + insSql.EnumToSqlValue((int)eStatusDocumentoCxP.Anulado), "0", vSqlMontoTotal, true);
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;
			StringBuilder vMontoPagadoCase = new StringBuilder();

			if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
                    vSqlCambio = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = DocumentoPagado.CodigoMonedaDecxP AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				string vSqlPagoCodigoMoneda = "Pago.CodigoMoneda";
				string vSqlDocPagCodigoMonedaCxP = "DocumentoPagado.CodigoMonedaDeCxP";
				string vSqlPagCodMonedaIgualML = vSqlPagoCodigoMoneda + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlPagCodMonedaDifML = vSqlPagoCodigoMoneda + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlDocPagCodMonedaIgualML = vSqlDocPagCodigoMonedaCxP + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlDocPagCodMonedaDifML = vSqlDocPagCodigoMonedaCxP + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
				vMontoPagadoCase.AppendLine($"(CASE WHEN {vSqlPagCodMonedaIgualML} AND {vSqlDocPagCodMonedaIgualML} THEN DocumentoPagado.MontoAbonado ");
				vMontoPagadoCase.AppendLine($" WHEN {vSqlPagCodMonedaIgualML} AND {vSqlDocPagCodMonedaDifML} THEN {insSql.RoundToNDecimals(vSqlMontoPagado + " * " + vSqlCambio, 2)} ");
				vMontoPagadoCase.AppendLine($" WHEN {vSqlPagCodMonedaDifML} AND {vSqlDocPagCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlMontoPagado + " * " + vSqlCambio, 2)} ");
				vMontoPagadoCase.AppendLine($" ELSE {insSql.RoundToNDecimals(vSqlMontoPagado + " * " + vSqlCambio, 2)} ");
				vMontoPagadoCase.AppendLine($" END)");
				vSqlMontoPagado = vMontoPagadoCase.ToString();
          
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				vSqlCambioMasCercano = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= Pago.Fecha ORDER BY FechaDeVigencia DESC)", "1");
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				string vSqlPagoCodigoMoneda = "Pago.CodigoMoneda";
				string vSqlDocPagCodigoMonedaCxP = "DocumentoPagado.CodigoMonedaDeCxP";
				string vSqlPagCodMonedaIgualML = vSqlPagoCodigoMoneda + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlPagCodMonedaDifML = vSqlPagoCodigoMoneda + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlDocPagCodMonedaIgualML = vSqlDocPagCodigoMonedaCxP + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlDocPagCodMonedaDifML = vSqlDocPagCodigoMonedaCxP + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
                vSqlCambio = insSql.IIF("DocumentoPagado.CodigoMonedaDecxP = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
				vMontoPagadoCase.AppendLine($"(CASE WHEN {vSqlPagCodMonedaIgualML} AND {vSqlDocPagCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlMontoPagado + " / " + vSqlCambio, 2)} ");				
				vMontoPagadoCase.AppendLine($" WHEN {vSqlPagCodMonedaIgualML} AND {vSqlDocPagCodMonedaDifML} THEN DocumentoPagado.MontoEnMonedaOriginalDeCxP ");
                vMontoPagadoCase.AppendLine($" WHEN {vSqlPagCodMonedaDifML} AND {vSqlDocPagCodMonedaIgualML} THEN DocumentoPagado.MontoAbonado ");
                vMontoPagadoCase.AppendLine($" ELSE DocumentoPagado.MontoAbonado ");
                vMontoPagadoCase.AppendLine($" END)");
				vSqlMontoPagado = vMontoPagadoCase.ToString();
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {                               
                string vSqlPagoCodigoMoneda = "Pago.CodigoMoneda";
                string vSqlDocPagCodigoMonedaCxP = "DocumentoPagado.CodigoMonedaDeCxP";
                string vSqlPagCodMonedaIgualML = vSqlPagoCodigoMoneda + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
                string vSqlPagCodMonedaDifML = vSqlPagoCodigoMoneda + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
                string vSqlDocPagCodMonedaIgualML = vSqlDocPagCodigoMonedaCxP + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
                string vSqlDocPagCodMonedaDifML = vSqlDocPagCodigoMonedaCxP + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
				vSqlCambio = insSql.IIF("DocumentoPagado.CodigoMonedaDeCxP = " + insSql.ToSqlValue(vCodigoMonedaLocal), " 1 ", " DocumentoPagado.CambioAMonedaDelPago ", true);
			 	string vSqlCambioPago = insSql.IIF("Pago.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), " 1 ", " Pago.CambioaBolivares ", true);
				vMontoPagadoCase.AppendLine($"(CASE WHEN {vSqlDocPagCodigoMonedaCxP} = {vSqlPagoCodigoMoneda} THEN {vSqlMontoPagado} ");
                vMontoPagadoCase.AppendLine($" WHEN  {vSqlDocPagCodigoMonedaCxP} <> {vSqlPagoCodigoMoneda} AND {vSqlDocPagCodMonedaIgualML} THEN {insSql.RoundToNDecimals("DocumentoPagado.MontoAbonado * " + vSqlCambioPago, 2)} ");
				vMontoPagadoCase.AppendLine($" WHEN {vSqlDocPagCodigoMonedaCxP} <> {vSqlPagoCodigoMoneda} AND {vSqlDocPagCodMonedaDifML} THEN {insSql.RoundToNDecimals("DocumentoPagado.MontoAbonado / " + vSqlCambio, 2)} ");
                vMontoPagadoCase.AppendLine($" ELSE 0 ");
                vMontoPagadoCase.AppendLine($" END)");
                vSqlMontoPagado = vMontoPagadoCase.ToString();
            }
			/* FIN */
            vSql.AppendLine("CTE_CxPHistoricoProveedor AS (");
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	cxP.CodigoProveedor AS Codigo, ");
			vSql.AppendLine("	Proveedor.NombreProveedor AS Nombre, cxP.Moneda AS MonedaReporte, ");
			vSql.AppendLine("	Pago.CodigoMoneda AS CodMoneda, ");
			vSql.AppendLine("	cxP.Fecha AS FechaDocumento, ");
			vSql.AppendLine("	ISNULL(Pago.Moneda, cxP.Moneda) AS Moneda, ");
			vSql.AppendLine("	Pago.CambioAbolivares, ");
			vSql.AppendLine("	cxP.FechaVencimiento, ");
			vSql.AppendLine($"	(CASE cxP.TipoDeCxp WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.FACTURA)} THEN 'Factura' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.GIRO)} THEN 'Giro' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.CHEQUEDEVUELTO)} THEN 'Cheque Devuelto' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTADECREDITO)} THEN 'Nota de Crédito' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTADEDEBITO)} THEN 'Nota de Débito' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTAENTREGA)} THEN 'Nota de Entrega' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOASIGNADO)} THEN 'No Asignado' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.BOLETADEVENTA)} THEN 'Boleta de Venta' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.TICKETMAQUINAREGISTRADORA)} THEN 'Ticket Máquina Registradora' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.RECIBOPORHONORARIOS)} THEN 'Recibo por Honorarios' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.LIQUIDACIONDECOMPRA)} THEN 'Liquidación de Compra' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.OTROS)} THEN 'Otros' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTADECREDITOCOMPROBANTEFISCAL)} THEN 'Nota de Crédito Comprobante Fiscal' END) AS TipoDeDocumento, ");
			vSql.AppendLine("	cxP.Numero AS NumeroDocumento, ");
			vSql.AppendLine("	CAST(cxP.CodigoProveedor AS VARCHAR) + CHAR(9) + CAST(cxP.Numero AS VARCHAR) AS NumeroDocumentoGrupo, ");
			vSql.AppendLine("	Pago.NumeroComprobante AS NumeroPago, ");
			vSql.AppendLine("	Pago.Fecha AS FechaPago, ");
			vSql.AppendLine("	'Cuentas por Pagar' AS TituloTipoReporte, ");
			vSql.AppendLine("	'Pago' AS TipoDocumentoDetalle, ");
			vSql.AppendLine($" {vSqlMontoPagado} AS MontoPagado, ");
			vSql.AppendLine("	DocumentoPagado.CambioAMonedaDelPago AS CambioMonedaPago, ");
			vSql.AppendLine("	cxP.CambioAbolivares AS CambioALocalDesdeMonedaCxP, ");
			vSql.AppendLine("	Pago.StatusOrdenDePago AS StatusPago");
			vSql.AppendLine("FROM Adm.Proveedor INNER JOIN");
			vSql.AppendLine("   cxP ON Proveedor.CodigoProveedor = cxP.CodigoProveedor AND Proveedor.ConsecutivoCompania = cxP.ConsecutivoCompania RIGHT JOIN");
			vSql.AppendLine("   Pago LEFT JOIN");
			vSql.AppendLine("   DocumentoPagado ON Pago.NumeroComprobante = DocumentoPagado.NumeroComprobante AND Pago.ConsecutivoCompania = DocumentoPagado.ConsecutivoCompania ON ");
			vSql.AppendLine("   cxP.ConsecutivoCxp = DocumentoPagado.ConsecutivoCxP AND cxP.ConsecutivoCompania = cxP.ConsecutivoCompania");
			vSql.AppendLine($"WHERE (cxP.Status <> '4') AND cxP.ConsecutivoCompania = {LibConvert.ToStr(valConsecutivoCompania)} AND ");
			vSql.AppendLine(insSql.SqlDateValueBetween("", "CxP.Fecha", valFechaDesde, valFechaHasta) + " AND " + insSql.SqlDateValueBetween("", "Pago.Fecha", valFechaDesde, valFechaHasta));
			vSql.AppendLine(")");
			return vSql.ToString();
		}

		private string SqlCTEAnticipoHistoricoProveedore(int valConsecutivoCompania, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valNombreMoneda, DateTime valFechaDesde, DateTime valFechaHasta) {
			StringBuilder vSql = new StringBuilder();			
			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "anticipo.Cambio";			
			string vSqlMontoOriginal = " Anticipo.MontoTotal";			
			string vSqlSaldoActual = insSql.RoundToNDecimals( "Anticipo.MontoTotal - (Anticipo.MontoUsado + Anticipo.MontoDevuelto + Anticipo.MontoDiferenciaEnDevolucion)", 2);
			string vSqlMontoPagado = insSql.IsNull("Anticipo.MontoUsado", "0");
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;
			string vSqlPagoCodigoMoneda = "Pago.CodigoMoneda";
			string vSqlAntPagCodigoMonedaAnticipo = "AnticipoPagado.CodigoMoneda";
			string vSqlPagCodMonedaIgualML = vSqlPagoCodigoMoneda + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
			string vSqlPagCodMonedaDifML = vSqlPagoCodigoMoneda + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
			string vSqlAntPagCodMonedaIgualML = vSqlAntPagCodigoMonedaAnticipo + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
			string vSqlAntPagCodMonedaDifML = vSqlAntPagCodigoMonedaAnticipo + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
			string vCambioPagoAnticipo = "AnticipoPagado.Cambio";

			if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = anticipo.CodigoMoneda AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
					vCambioPagoAnticipo  = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = AnticipoPagado.CodigoMoneda AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				} else {
					vSqlCambio = vSqlCambioOriginal;
					vCambioPagoAnticipo = "AnticipoPagado.Cambio";
				}
				vSqlMontoOriginal = insSql.RoundToNDecimals(vSqlMontoOriginal + " * " + vSqlCambio, 2);
				StringBuilder vCambioPago = new StringBuilder();
				vCambioPago.AppendLine($"(CASE WHEN {vSqlAntPagCodigoMonedaAnticipo} <> {vSqlPagoCodigoMoneda} THEN {vCambioPagoAnticipo} ");
				vCambioPago.AppendLine($" ELSE {vSqlCambio} ");
				vCambioPago.AppendLine($" END)");
				vSqlMontoPagado = insSql.RoundToNDecimals("Anticipo.MontoUsado * " + vCambioPago, 2);
				vSqlSaldoActual = insSql.RoundToNDecimals(vSqlSaldoActual + " * " + vSqlCambio, 2);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				vSqlCambioMasCercano = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= Anticipo.Fecha ORDER BY FechaDeVigencia DESC)", "1");
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				StringBuilder vMontoDetalleCase = new StringBuilder();
				vSqlCambio = insSql.IIF("anticipo.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
				vMontoDetalleCase.AppendLine($"(CASE WHEN {vSqlPagCodMonedaIgualML} AND {vSqlAntPagCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlMontoPagado + " / " + vSqlCambio, 2)} ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlPagCodMonedaIgualML} AND {vSqlAntPagCodMonedaDifML} THEN Anticipo.MontoUsado ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlPagCodMonedaDifML} AND {vSqlAntPagCodMonedaIgualML} THEN Anticipo.MontoUsado ");
				vMontoDetalleCase.AppendLine($" ELSE ISNULL(AnticipoPagado.MontoAplicado, 0) ");
				vMontoDetalleCase.AppendLine($" END)");
				vSqlMontoPagado = vMontoDetalleCase.ToString();

				vMontoDetalleCase = new StringBuilder();
				vMontoDetalleCase.AppendLine($"(CASE WHEN {vSqlPagCodMonedaIgualML} AND {vSqlAntPagCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlMontoOriginal + " / " + vSqlCambio, 2)} ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlPagCodMonedaIgualML} AND {vSqlAntPagCodMonedaDifML} THEN AnticipoPagado.MontoOriginal ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlPagCodMonedaDifML} AND {vSqlAntPagCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlMontoOriginal + " / " + vSqlCambio, 2)} ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlPagoCodigoMoneda} IS NULL THEN {insSql.RoundToNDecimals(vSqlMontoOriginal + " / " + vSqlCambio, 2)} ");//No ha sido usado
				vMontoDetalleCase.AppendLine($" ELSE AnticipoPagado.MontoOriginal ");
				vMontoDetalleCase.AppendLine($" END)");
				vSqlMontoOriginal = vMontoDetalleCase.ToString();

				vMontoDetalleCase = new StringBuilder();
				vMontoDetalleCase.AppendLine($"(CASE WHEN {vSqlPagCodMonedaIgualML} AND {vSqlAntPagCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlSaldoActual + " / " + vSqlCambio, 2)} ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlPagCodMonedaIgualML} AND {vSqlAntPagCodMonedaDifML} THEN {vSqlSaldoActual} ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlPagCodMonedaDifML} AND {vSqlAntPagCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlSaldoActual + " / " + vSqlCambio, 2)} ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlPagoCodigoMoneda} IS NULL THEN {insSql.RoundToNDecimals(vSqlSaldoActual + " / " + vSqlCambio, 2)} ");//No ha sido usado
				vMontoDetalleCase.AppendLine($" ELSE {vSqlSaldoActual} ");
				vMontoDetalleCase.AppendLine($" END)");
				vSqlSaldoActual = vMontoDetalleCase.ToString();

			} else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
				StringBuilder vMontoDetalleCase = new StringBuilder();
				vMontoDetalleCase.AppendLine($"(CASE WHEN {vSqlAntPagCodigoMonedaAnticipo} = {vSqlPagoCodigoMoneda} AND {vSqlAntPagCodMonedaIgualML} THEN AnticipoPagado.MontoAplicado ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlAntPagCodigoMonedaAnticipo} = {vSqlPagoCodigoMoneda}  AND {vSqlAntPagCodMonedaDifML} THEN Anticipo.MontoUsado ");
				vMontoDetalleCase.AppendLine($" WHEN  {vSqlAntPagCodigoMonedaAnticipo} <> {vSqlPagoCodigoMoneda} AND {vSqlAntPagCodMonedaIgualML} THEN AnticipoPagado.MontoAplicado ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlAntPagCodigoMonedaAnticipo} <> {vSqlPagoCodigoMoneda} AND {vSqlAntPagCodMonedaDifML} THEN Anticipo.MontoUsado ");
				vMontoDetalleCase.AppendLine($" ELSE 0 ");
				vMontoDetalleCase.AppendLine($" END)");
				vSqlMontoPagado = vMontoDetalleCase.ToString();
			}
			/* FIN */
			vSql.AppendLine("CTE_AnticipoHistoricoProveedor AS (");
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	'1' AS TipoReporte, ");
			vSql.AppendLine("	anticipo.CodigoProveedor AS Codigo, ");
			vSql.AppendLine("	Proveedor.NombreProveedor AS Nombre, ");
			vSql.AppendLine("	anticipo.CodigoMoneda AS CodigoMonedaAnticipo,");
			vSql.AppendLine("	anticipo.Moneda AS MonedaAnticipo, ");
			vSql.AppendLine("	anticipo.Cambio, ");
			vSql.AppendLine("	ISNULL(Pago.Moneda, '') AS Moneda, ");
			vSql.AppendLine("	ISNULL(Pago.CambioAbolivares, 1) AS CambioAbolivares, ");
			vSql.AppendLine("	anticipo.Moneda AS MonedaReporte, ");
			vSql.AppendLine("	'Anticipos' AS TituloTipoReporte, ");
			vSql.AppendLine("	anticipo.Fecha AS FechaDocumento, ");
			vSql.AppendLine("	'' AS FechaVencimiento, ");
			vSql.AppendLine("	'Anticipos' AS TipoDeDocumento, ");
			vSql.AppendLine("	anticipo.Numero AS NumeroDocumento, ");
			vSql.AppendLine("	CAST(anticipo.ConsecutivoAnticipo AS VARCHAR) AS NumeroDocumentoGrupo, ");
			vSql.AppendLine($"	{vSqlMontoOriginal} AS MontoOriginal, ");
			vSql.AppendLine($"	{vSqlSaldoActual} AS SaldoActual, ");
			vSql.AppendLine("	'Pago' AS TipoDocumentoDetalle, ");
			vSql.AppendLine("	ISNULL(Pago.NumeroComprobante, 0) AS NumeroPago, ");
			vSql.AppendLine("	ISNULL(Pago.Fecha, '01/01/1900') AS FechaPago, ");
			vSql.AppendLine($"	{vSqlMontoPagado} AS MontoPagado, ");
			vSql.AppendLine("	ISNULL(anticipoPagado.Cambio, 1) AS CambioPagado, ");
			vSql.AppendLine("	ISNULL(Pago.StatusOrdenDePago, '') AS StatusPago");
			vSql.AppendLine("FROM anticipo LEFT JOIN Pago INNER JOIN anticipoPagado ");
			vSql.AppendLine("	ON Pago.NumeroComprobante = anticipoPagado.NumeroComprobante ");
			vSql.AppendLine("	AND Pago.ConsecutivoCompania = anticipoPagado.ConsecutivoCompania ");
			vSql.AppendLine("	AND (Pago.StatusOrdenDePago = '0')");
			vSql.AppendLine("	ON anticipo.ConsecutivoAnticipo = anticipoPagado.ConsecutivoAnticipoUsado ");
			vSql.AppendLine("	AND anticipo.ConsecutivoCompania = anticipoPagado.ConsecutivoCompania ");
			vSql.AppendLine("	INNER JOIN Proveedor ");
			vSql.AppendLine("	ON anticipo.CodigoProveedor = Proveedor.CodigoProveedor ");
			vSql.AppendLine("	AND anticipo.ConsecutivoCompania = Proveedor.ConsecutivoCompania");
			vSql.AppendLine($"WHERE (anticipo.Status <> '1') AND (anticipo.EsUnaDevolucion = 'N') AND (anticipo.Tipo = '1')  AND anticipo.ConsecutivoCompania = {insSql.ToSqlValue(valConsecutivoCompania)} AND ");
			vSql.AppendLine(insSql.SqlDateValueBetween("", "anticipo.Fecha", valFechaDesde, valFechaHasta));
			vSql.AppendLine(")");
			return vSql.ToString();
		}

		private string SqlCTESaldoInicialCxPHistoricoProveedor(int valConsecutivoCompania,DateTime valFechaDesde, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("CTE_SaldoInicialCxPHistoricoProveedor AS (");
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	CodigoProveedor, ");
			vSql.AppendLine("	CodigoMoneda, ");
			vSql.AppendLine("	SUM((MontoExento + MontoGravado + MontoIva)) AS SaldoInicialOriginal,");
			vSql.AppendLine("	SUM(ROUND(CambioAbolivares * (MontoExento + MontoGravado + MontoIva), 2)) AS SaldoInicialMonedaLocal");
			vSql.AppendLine("FROM cxP");
			vSql.AppendLine("WHERE (Status <> '4') AND (Fecha < " + insSql.ToSqlValue(valFechaDesde) + ") AND ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
			vSql.AppendLine("GROUP BY CodigoProveedor, CodigoMoneda)");
			return vSql.ToString();
		}

		private string SqlCTE_SaldoInicialAnticipoHistoricoProveedor(int valConsecutivoCompania, DateTime valFechaDesde, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("CTE_SaldoInicialAnticipoHistoricoProveedor AS (");
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	CodigoProveedor, ");
			vSql.AppendLine("	CodigoMoneda, ");
			vSql.AppendLine("	SUM(MontoTotal) AS SaldoInicialOriginal,");
			vSql.AppendLine("	SUM(ROUND(Cambio * MontoTotal, 2)) AS SaldoInicialMonedaLocal");
			vSql.AppendLine("FROM anticipo");
			vSql.AppendLine("WHERE (Status <> '1') AND (Tipo = '1') AND (Fecha < " + insSql.ToSqlValue(valFechaDesde) + ") AND ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
			vSql.AppendLine("GROUP BY CodigoProveedor, CodigoMoneda)");
			return vSql.ToString();
		}

		private string sqlCalcularSaldoInicialAnticipo(string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valSaldoInicial) {
			string vCalculaSaldoInicialAnticipo = "";
			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "Cambio";
			string vSqlMontoInicial = valSaldoInicial;
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;

			if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = CodigoMoneda AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlCambio = insSql.IIF("CodigoMonedaAnticipo = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
				vSqlMontoInicial = insSql.IIF("CodigoMonedaAnticipo = " + insSql.ToSqlValue(vCodigoMonedaLocal), insSql.RoundToNDecimals(vSqlMontoInicial, 2), insSql.RoundToNDecimals(vSqlMontoInicial + " * " + vSqlCambio, 2), true);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				vSqlCambioMasCercano = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia < FechaDocumento ORDER BY FechaDeVigencia DESC)", "1");
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				vSqlCambio = insSql.IIF("CodigoMonedaAnticipo = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);				
                vSqlMontoInicial = insSql.IIF("CodigoMonedaAnticipo = " + insSql.ToSqlValue(vCodigoMonedaLocal), insSql.RoundToNDecimals(vSqlMontoInicial + " / " + vSqlCambio, 2), vSqlMontoInicial, true);
            } else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
			}
			/* FIN */
			vCalculaSaldoInicialAnticipo = vSqlMontoInicial;
			return vCalculaSaldoInicialAnticipo;
		}

		private string sqlCalcularSaldoInicialcxP(string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valSaldoInicial) {
			string vCalculaSaldoInicialcxP = "";
			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "CambioAbolivares";
			string vSqlMontoInicial = valSaldoInicial;
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;
			if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = CodigoMoneda AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlCambio = insSql.IIF("CodMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
				vSqlMontoInicial = insSql.IIF("CodMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), insSql.RoundToNDecimals(vSqlMontoInicial, 2), insSql.RoundToNDecimals(vSqlMontoInicial + " * " + vSqlCambio, 2), true);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				vSqlCambioMasCercano = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia < FechaDocumento ORDER BY FechaDeVigencia DESC)", "1");
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				vSqlCambio = insSql.IIF("CodMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);				
                vSqlMontoInicial = insSql.IIF("CodMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), insSql.RoundToNDecimals(vSqlMontoInicial + " / " + vSqlCambio, 2), vSqlMontoInicial, true);
            } else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
			}
			/* FIN */
			vCalculaSaldoInicialcxP = vSqlMontoInicial;
			return vCalculaSaldoInicialcxP;
		}
	} //End of class clsProveedorSql
} //End of namespace Galac.Adm.Brl.Proveedor