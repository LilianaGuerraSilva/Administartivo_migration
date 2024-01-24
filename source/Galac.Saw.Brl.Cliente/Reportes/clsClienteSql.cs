using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Lib;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.Brl.Cliente.Reportes {
	public class clsClienteSql {
		private QAdvSql insSql;
		Comun.Ccl.TablasGen.ICambioPdn insCambioMoneda;

		public clsClienteSql() {
			insSql = new QAdvSql("");
			insCambioMoneda = new Comun.Brl.TablasGen.clsCambioNav();
		}

		#region Metodos Generados
		public string SqlHistoricoDeCliente(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoCliente, eMonedaDelInformeMM valMonedaDelInforme, string valCodigoMoneda, string valNombreMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, eClientesOrdenadosPor valClienteOrdenarPor) {
			StringBuilder vSql = new StringBuilder();
			string vOrdenarPor = valClienteOrdenarPor == eClientesOrdenadosPor.PorCodigo ? "Codigo" : "Nombre";
			vSql.AppendLine(";WITH");
			vSql.AppendLine(SqlCTEInfoCxCHistoricoCliente(valConsecutivoCompania, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, valNombreMoneda));
			vSql.AppendLine(",");
			vSql.AppendLine(SqlCTECxCHistoricoClientes(valConsecutivoCompania, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, valNombreMoneda));
			vSql.AppendLine(",");
			vSql.AppendLine(SqlCTEAnticipoHistoricoCliente(valConsecutivoCompania, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, valNombreMoneda));
			vSql.AppendLine(",");
			vSql.AppendLine(SqlCTESaldoInicialCxCHistoricoCliente(valConsecutivoCompania, valFechaDesde, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, valNombreMoneda));
			vSql.AppendLine(",");
			vSql.AppendLine(SqlCTESaldoInicialAnticipoHistoricoCliente(valConsecutivoCompania, valFechaDesde, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, valNombreMoneda));
			vSql.AppendLine(SqlSelectInfoCxCHistoricoCliente(valFechaDesde, valFechaHasta, valCodigoCliente, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio));
			vSql.AppendLine(" UNION ");
			vSql.AppendLine(SqlSelectCxCHistoricoClientes(valFechaDesde, valFechaHasta, valCodigoCliente, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio));
			vSql.AppendLine(" UNION ");
			vSql.AppendLine(SqlSelectAnticipoHistoricoCliente(valFechaDesde, valFechaHasta, valCodigoCliente, valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio));
			vSql.AppendLine($" ORDER BY {vOrdenarPor}, TipoReporte, MonedaReporte, NumeroDocumentoGrupo, FechaDocumento, FechaCobranza, NumeroCobranza");

			string vPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Hcli.slq";
			LibFile.WriteLineInFile(vPath, vSql.ToString(), false);

			return vSql.ToString();
		}

		private string SqlSelectInfoCxCHistoricoCliente(DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoCliente, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "FechaDocumento", valFechaDesde, valFechaHasta);
			vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Codigo", valCodigoCliente);
			string vSaldoInicial = insSql.IsNull("(SELECT SaldoInicialOriginal FROM CTE_SaldoInicialCxCHistoricoCliente WHERE (CodigoCliente = CTE_InfoCxCHistoricoCliente.Codigo AND CodigoMoneda = CTE_InfoCxCHistoricoCliente.CodMoneda))", "0");
			string vCalcularSaldoInicialCxC = sqlCalcularSaldoInicialCxC(valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, vSaldoInicial);
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	'0' AS TipoReporte, ");
			vSql.AppendLine("	Codigo, ");
			vSql.AppendLine("	NombreCliente AS Nombre, ");
			vSql.AppendLine("	MonedaReporte, ");
			vSql.AppendLine("	MonedaReporte AS Moneda, ");
			vSql.AppendLine("	TituloTipoReporte,");
			vSql.AppendLine($"	{vCalcularSaldoInicialCxC} AS SaldoInicial,");
			vSql.AppendLine("	FechaDocumento, ");
			vSql.AppendLine("	FechaVencimiento, ");
			vSql.AppendLine("	TipoDeDocumento, ");
			vSql.AppendLine("	NumeroDocumento, ");
			vSql.AppendLine("	NumeroDocumentoGrupo, ");
			vSql.AppendLine("	MontoOriginal, ");
			vSql.AppendLine("	SaldoActual, ");
			vSql.AppendLine("	TipoDocumentoDetalle, ");
			vSql.AppendLine("	'' AS NumeroCobranza, ");
			vSql.AppendLine("	'' AS FechaCobranza, ");
			vSql.AppendLine("	0 AS MontoCobrado, ");
			vSql.AppendLine("	'' AS StatusCobranza");
			vSql.AppendLine("FROM CTE_InfoCxCHistoricoCliente");
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			return vSql.ToString();
		}

		private string SqlSelectCxCHistoricoClientes(DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoCliente, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "FechaDocumento", valFechaDesde, valFechaHasta);
			vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Codigo", valCodigoCliente);
			string vSaldoInicial = insSql.IsNull("( SELECT SaldoInicialOriginal FROM CTE_SaldoInicialCxCHistoricoCliente WHERE (CodigoCliente = CTE_CxCHistoricoClientes.Codigo AND CodigoMoneda = CTE_CxCHistoricoClientes.CodMoneda))", "0");
			string vCalcularSaldoInicialCxC = sqlCalcularSaldoInicialCxC(valCodigoMoneda, valMonedaDelInforme, valTasaDeCambio, vSaldoInicial);
			vSql.AppendLine("SELECT");
			vSql.AppendLine("	'0' AS TipoReporte, ");
			vSql.AppendLine("	Codigo, ");
			vSql.AppendLine("	Nombre, ");
			vSql.AppendLine("	MonedaReporte, ");
			vSql.AppendLine("	Moneda, ");
			vSql.AppendLine("	TituloTipoReporte,");
			vSql.AppendLine($"	{vCalcularSaldoInicialCxC} AS SaldoInicial,");
			vSql.AppendLine("	FechaDocumento, ");
			vSql.AppendLine("	FechaVencimiento, ");
			vSql.AppendLine("	TipoDeDocumento, ");
			vSql.AppendLine("	NumeroDocumento, ");
			vSql.AppendLine("	NumeroDocumentoGrupo, ");
			vSql.AppendLine("	0 AS MontoOriginal, ");
			vSql.AppendLine("	0 AS SaldoActual, ");
			vSql.AppendLine("	TipoDocumentoDetalle, ");
			vSql.AppendLine("	NumeroCobranza, ");
			vSql.AppendLine("	FechaCobranza, ");
			vSql.AppendLine("	MontoCobrado, ");
			vSql.AppendLine("	StatusCobranza");
			vSql.AppendLine("FROM CTE_CxCHistoricoClientes");
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			return vSql.ToString();
		}

		private string SqlSelectAnticipoHistoricoCliente(DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoCliente, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "FechaDocumento", valFechaDesde, valFechaHasta);
			vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Codigo", valCodigoCliente);
			string vSaldoInicial = insSql.IsNull("(SELECT SaldoInicialOriginal FROM CTE_SaldoInicialAnticipoHistoricoCliente WHERE (CodigoCliente = CTE_AnticipoHistoricoCliente.Codigo) AND (CodigoMoneda = CTE_AnticipoHistoricoCliente.CodigoMonedaAnticipo))", "0");
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
			vSql.AppendLine("	NumeroCobranza, ");
			vSql.AppendLine("	FechaCobranza, ");
			vSql.AppendLine("	(CASE WHEN StatusCobranza = '1' THEN 0 ELSE MontoCobrado END) AS MontoCobrado, ");
			vSql.AppendLine("	StatusCobranza");
			vSql.AppendLine("FROM CTE_AnticipoHistoricoCliente");
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			return vSql.ToString();
		}
		#endregion //Metodos Generados      
		private string SqlCTEInfoCxCHistoricoCliente(int valConsecutivoCompania, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "CxC.CambioAbolivares";
			string vSqlMontoTotal = "(CxC.MontoExento + CxC.MontoGravado + CxC.MontoIva)";
			string vSqlMontoOriginal = insSql.IIF("CxC.Status = " + insSql.EnumToSqlValue((int)eStatusCXC.ANULADO), "0", vSqlMontoTotal, true);
			string vSqlMontoRestante = insSql.IIF("CxC.Status = " + insSql.EnumToSqlValue((int)eStatusCXC.ANULADO), "0", "(" + vSqlMontoTotal + " - CxC.MontoAbonado)", true);
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;
			string vSqlMonedaTotales = "CxC.Moneda";	
			if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = CxC.CodigoMoneda AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlMontoOriginal = insSql.RoundToNDecimals(vSqlMontoOriginal + " * " + vSqlCambio, 2);
				vSqlMontoRestante = insSql.RoundToNDecimals(vSqlMontoRestante + " * " + vSqlCambio, 2);
				vSqlMonedaTotales = "'Bolívares'";
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				vSqlCambioMasCercano = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= CxC.Fecha ORDER BY FechaDeVigencia DESC)", "1");
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}				
                vSqlCambio = insSql.IIF("CxC.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);			
                vSqlMontoOriginal = insSql.IIF("CxC.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), insSql.RoundToNDecimals(vSqlMontoOriginal + " / " + vSqlCambio, 2),vSqlMontoOriginal,true);
				vSqlMontoRestante = insSql.IIF("CxC.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), insSql.RoundToNDecimals(vSqlMontoRestante + " / " + vSqlCambio, 2),vSqlMontoRestante,true);                
				vSqlMonedaTotales = insSql.IIF("CxC.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), "'Bolívares expresados en " + valNombreMoneda + "'", "CxC.Moneda", true);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
			}
			/* FIN */
			vSql.AppendLine("CTE_InfoCxCHistoricoCliente AS (");
			vSql.AppendLine("SELECT");
			vSql.AppendLine("	CxC.CodigoCliente AS Codigo, ");
			vSql.AppendLine("	Cliente.Nombre AS NombreCliente,");
			vSql.AppendLine("	CxC.Numero AS NumeroDocumento, ");
			vSql.AppendLine("	CxC.Fecha AS FechaDocumento, ");
			vSql.AppendLine("	CxC.FechaVencimiento, ");
			vSql.AppendLine("	CxC.Moneda AS MonedaReporte, ");
			vSql.AppendLine("	CxC.CodigoMoneda AS CodMoneda, ");
			vSql.AppendLine($"	{vSqlCambio} AS CambioABolivares, ");
			vSql.AppendLine($"	(CASE CxC.TipoCxC WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.FACTURA)} THEN 'FAC' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.GIRO)} THEN 'GRO' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.CHEQUEDEVUELTO)} THEN 'C/D' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTADECREDITO)} THEN 'N/C' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTADEDEBITO)} THEN 'N/D' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTAENTREGA)} THEN 'N/E' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOASIGNADO)} THEN 'N/A' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.BOLETADEVENTA)} THEN 'BOL' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.TICKETMAQUINAREGISTRADORA)} THEN 'TIC' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.RECIBOPORHONORARIOS)} THEN 'R/H' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.LIQUIDACIONDECOMPRA)} THEN 'L/C' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.OTROS)} THEN 'OTR' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTADECREDITOCOMPROBANTEFISCAL)} THEN 'N/C-CF' END) AS TipoDeDocumento, ");
			vSql.AppendLine("	'Cuentas por Cobrar' AS TituloTipoReporte, ");
			vSql.AppendLine("	'Cobro' AS TipoDocumentoDetalle, ");
			vSql.AppendLine("	CAST(CxC.CodigoCliente AS VARCHAR) + CHAR(9) + CAST(CxC.Numero AS VARCHAR) AS NumeroDocumentoGrupo, ");
			vSql.AppendLine($"	 {vSqlMontoOriginal} AS MontoOriginal, ");
			vSql.AppendLine($"	{vSqlMontoRestante} AS SaldoActual");
			vSql.AppendLine("FROM Cliente INNER JOIN CxC ON Cliente.Codigo = CxC.CodigoCliente AND Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania");
			vSql.AppendLine($"WHERE (CxC.Status <> '4') AND CxC.ConsecutivoCompania = {LibConvert.ToStr(valConsecutivoCompania)})");
			return vSql.ToString();
		}

		private string SqlCTECxCHistoricoClientes(int valConsecutivoCompania, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "DocumentoCobrado.CambioAMonedaDeCobranza";
			string vSqlMontoTotal = "DocumentoCobrado.MontoAbonado";
			string vSqlMontoCobrado = insSql.IIF("cobranza.StatusCobranza = " + insSql.EnumToSqlValue((int)eStatusCobranza.Anulada) + " OR cobranza.TipoDeDocumento =  " + insSql.EnumToSqlValue((int)eTipoDeDocumentoCobranza.CobranzaPorAplicacionDeRetencion), "0", vSqlMontoTotal, true);
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;
			string vSqlMonedaTotales = valNombreMoneda;
			StringBuilder vMontoCobradoCase = new StringBuilder();

			if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = cobranza.CodigoMoneda AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlMontoCobrado = insSql.RoundToNDecimals(vSqlMontoCobrado + " * " + vSqlCambio, 2);
				vSqlMonedaTotales = "'Bolívares'";
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				vSqlCambioMasCercano = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= Cobranza.Fecha ORDER BY FechaDeVigencia DESC)", "1");
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				string vSqlCobranzaCodigoMoneda = "Cobranza.CodigoMoneda";
				string vSqlDocCobradCodigoMonedaCxC = "DocumentoCobrado.CodigoMonedaDeCxC";
				string vSqlCobCodMonedaIgualML = vSqlCobranzaCodigoMoneda + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlCobCodMonedaDifML = vSqlCobranzaCodigoMoneda + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlDocCobCodMonedaIgualML = vSqlDocCobradCodigoMonedaCxC + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlDocCobCodMonedaDifML = vSqlDocCobradCodigoMonedaCxC + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);

                vSqlCambio = insSql.IIF("DocumentoCobrado.CodigoMonedaDeCxC = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);		
				vMontoCobradoCase.AppendLine($"(CASE WHEN {vSqlCobCodMonedaIgualML} AND {vSqlDocCobCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlMontoCobrado + " / " + vSqlCambio, 2)} ");				
				vMontoCobradoCase.AppendLine($" WHEN {vSqlCobCodMonedaIgualML} AND {vSqlDocCobCodMonedaDifML} THEN DocumentoCobrado.MontoAbonadoEnMonedaOriginal ");
                vMontoCobradoCase.AppendLine($" WHEN {vSqlCobCodMonedaDifML} AND {vSqlDocCobCodMonedaIgualML} THEN DocumentoCobrado.MontoAbonado ");
                vMontoCobradoCase.AppendLine($" ELSE DocumentoCobrado.MontoAbonado ");
                vMontoCobradoCase.AppendLine($" END)");
				vSqlMontoCobrado = vMontoCobradoCase.ToString();
                vSqlMonedaTotales = insSql.IIF("DocumentoCobrado.CodigoMonedaDeCxC = " + insSql.ToSqlValue(vCodigoMonedaLocal), "'Bolívares expresados en " + valNombreMoneda + "'", "CxC.Moneda", true);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {                               
                string vSqlCobranzaCodigoMoneda = "Cobranza.CodigoMoneda";
                string vSqlDocCobradCodigoMonedaCxC = "DocumentoCobrado.CodigoMonedaDeCxC";
                string vSqlCobCodMonedaIgualML = vSqlCobranzaCodigoMoneda + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
                string vSqlCobCodMonedaDifML = vSqlCobranzaCodigoMoneda + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
                string vSqlDocCobCodMonedaIgualML = vSqlDocCobradCodigoMonedaCxC + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
                string vSqlDocCobCodMonedaDifML = vSqlDocCobradCodigoMonedaCxC + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
				vMontoCobradoCase.AppendLine($"(CASE WHEN {vSqlDocCobradCodigoMonedaCxC} = {vSqlCobranzaCodigoMoneda} THEN {vSqlMontoCobrado} ");
                vMontoCobradoCase.AppendLine($" WHEN  {vSqlDocCobradCodigoMonedaCxC} <> {vSqlCobranzaCodigoMoneda} AND {vSqlDocCobCodMonedaIgualML} THEN DocumentoCobrado.MontoAbonadoEnMonedaOriginal ");
				vMontoCobradoCase.AppendLine($" WHEN {vSqlDocCobradCodigoMonedaCxC} <> {vSqlCobranzaCodigoMoneda} AND {vSqlDocCobCodMonedaDifML} THEN DocumentoCobrado.MontoAbonado ");
                vMontoCobradoCase.AppendLine($" ELSE 0 ");
                vMontoCobradoCase.AppendLine($" END)");
                vSqlMontoCobrado = vMontoCobradoCase.ToString();
                vSqlMonedaTotales = insSql.IIF("DocumentoCobrado.CodigoMonedaDeCxC = " + insSql.ToSqlValue(vCodigoMonedaLocal), "'Bolívares expresados en " + valNombreMoneda + "'", "CxC.Moneda", true);
            }
			/* FIN */
			vSql.AppendLine("CTE_CxCHistoricoClientes AS (");
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	CxC.CodigoCliente AS Codigo, ");
			vSql.AppendLine("	Cliente.Nombre, CxC.Moneda AS MonedaReporte, ");
			vSql.AppendLine("	CxC.CodigoMoneda AS CodMoneda, ");
			vSql.AppendLine("	CxC.Fecha AS FechaDocumento, ");
			vSql.AppendLine("	ISNULL(Cobranza.Moneda, cxC.Moneda) AS Moneda, ");
			vSql.AppendLine("	Cobranza.CambioAbolivares, ");
			vSql.AppendLine("	CxC.FechaVencimiento, ");
			vSql.AppendLine($"	(CASE CxC.TipoCxC WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.FACTURA)} THEN 'Factura' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.GIRO)} THEN 'Giro' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.CHEQUEDEVUELTO)} THEN 'Cheque Devuelto' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTADECREDITO)} THEN 'Nota de Crédito' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTADEDEBITO)} THEN 'Nota de Débito' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTAENTREGA)} THEN 'Nota de Entrega' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOASIGNADO)} THEN 'No Asignado' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.BOLETADEVENTA)} THEN 'Boleta de Venta' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.TICKETMAQUINAREGISTRADORA)} THEN 'Ticket Máquina Registradora' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.RECIBOPORHONORARIOS)} THEN 'Recibo por Honorarios' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.LIQUIDACIONDECOMPRA)} THEN 'Liquidación de Compra' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.OTROS)} THEN 'Otros' WHEN {insSql.EnumToSqlValue((int)eTipoDeTransaccion.NOTADECREDITOCOMPROBANTEFISCAL)} THEN 'Nota de Crédito Comprobante Fiscal' END) AS TipoDeDocumento, ");
			vSql.AppendLine("	CxC.Numero AS NumeroDocumento, ");
			vSql.AppendLine("	CAST(CxC.CodigoCliente AS VARCHAR) + CHAR(9) + CAST(CxC.Numero AS VARCHAR) AS NumeroDocumentoGrupo, ");
			vSql.AppendLine("	Cobranza.Numero AS NumeroCobranza, ");
			vSql.AppendLine("	Cobranza.Fecha AS FechaCobranza, ");
			vSql.AppendLine("	'Cuentas por Cobrar' AS TituloTipoReporte, ");
			vSql.AppendLine("	(CASE WHEN cobranza.TipoDeDocumento = '1' THEN 'Cobro(*)' ELSE 'Cobro' END) AS TipoDocumentoDetalle, ");
			vSql.AppendLine($" {vSqlMontoCobrado} AS MontoCobrado, ");
			vSql.AppendLine("	DocumentoCobrado.CambioAMonedaDeCobranza AS CambioMonedaCobranza, ");
			vSql.AppendLine("	DocumentoCobrado.CambioAMonedaLocal AS CambioALocalDesdeMonedaCxC, ");
			vSql.AppendLine("	Cobranza.StatusCobranza");
			vSql.AppendLine("FROM Cliente INNER JOIN");
			vSql.AppendLine("   CxC ON Cliente.Codigo = CxC.CodigoCliente AND Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania RIGHT JOIN");
			vSql.AppendLine("   Cobranza LEFT JOIN");
			vSql.AppendLine("   DocumentoCobrado ON Cobranza.Numero = DocumentoCobrado.NumeroCobranza AND Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania ON ");
			vSql.AppendLine("   CxC.Numero = DocumentoCobrado.NumeroDelDocumentoCobrado AND CxC.TipoCxc = DocumentoCobrado.TipoDeDocumentoCobrado AND CxC.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania");
			vSql.AppendLine($"WHERE (CxC.Status <> '4') AND CxC.ConsecutivoCompania = {LibConvert.ToStr(valConsecutivoCompania)})");
			return vSql.ToString();
		}

		private string SqlCTEAnticipoHistoricoCliente(int valConsecutivoCompania, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "Anticipo.Cambio";
			string vSqlMontoOriginal = "CASE WHEN AnticipoCobrado.MontoRestanteAlDia <= Anticipo.MontoTotal THEN 0 ELSE Anticipo.MontoTotal END";
			string vSqlSaldoActual = "CASE WHEN AnticipoCobrado.MontoRestanteAlDia <= Anticipo.MontoTotal THEN 0 ELSE (Anticipo.MontoTotal - (Anticipo.MontoUsado + Anticipo.MontoDevuelto + Anticipo.MontoDiferenciaEnDevolucion)) END";
			string vSqlMontoCobrado = insSql.IsNull("AnticipoCobrado.MontoAplicadoMonedaOriginal", "0");
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;
			string vSqlMonedaTotales = valNombreMoneda;

			if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = anticipo.CodigoMoneda AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlMontoOriginal = insSql.RoundToNDecimals(vSqlMontoOriginal + " * " + vSqlCambio, 2);
				vSqlMontoCobrado = insSql.RoundToNDecimals(" AnticipoCobrado.MontoAplicado * " + vSqlCambio, 2);
				vSqlSaldoActual = insSql.RoundToNDecimals(vSqlSaldoActual + " * " + vSqlCambio, 2);
				//vSqlMonedaTotales = "'Bolívares'";
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				vSqlCambioMasCercano = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= Anticipo.Fecha ORDER BY FechaDeVigencia DESC)", "1");
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				StringBuilder vMontoDetalleCase = new StringBuilder();

				string vSqlCobranzaCodigoMoneda = "Cobranza.CodigoMoneda";
				string vSqlAntCobradCodigoMonedaCxC = "AnticipoCobrado.CodigoMoneda";
				string vSqlCobCodMonedaIgualML = vSqlCobranzaCodigoMoneda + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlCobCodMonedaDifML = vSqlCobranzaCodigoMoneda + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlAntCobCodMonedaIgualML = vSqlAntCobradCodigoMonedaCxC + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlAntCobCodMonedaDifML = vSqlAntCobradCodigoMonedaCxC + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);

				vSqlCambio = insSql.IIF("anticipo.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);

				vMontoDetalleCase.AppendLine($"(CASE WHEN {vSqlCobCodMonedaIgualML} AND {vSqlAntCobCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlMontoCobrado + " / " + vSqlCambio, 2)} ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlCobCodMonedaIgualML} AND {vSqlAntCobCodMonedaDifML} THEN AnticipoCobrado.MontoAplicado ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlCobCodMonedaDifML} AND {vSqlAntCobCodMonedaIgualML} THEN AnticipoCobrado.MontoAplicado ");
				vMontoDetalleCase.AppendLine($" ELSE AnticipoCobrado.MontoAplicado ");
				vMontoDetalleCase.AppendLine($" END)");
				vSqlMontoCobrado = vMontoDetalleCase.ToString(); // insSql.IIF("anticipo.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), insSql.RoundToNDecimals(vSqlMontoCobrado + " / " + vSqlCambio, 2), vSqlMontoCobrado, true);

				vMontoDetalleCase = new StringBuilder();
				vMontoDetalleCase.AppendLine($"(CASE WHEN {vSqlCobCodMonedaIgualML} AND {vSqlAntCobCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlMontoOriginal + " / " + vSqlCambio, 2)} ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlCobCodMonedaIgualML} AND {vSqlAntCobCodMonedaDifML} THEN AnticipoCobrado.MontoOriginal ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlCobCodMonedaDifML} AND {vSqlAntCobCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlMontoOriginal + " / " + vSqlCambio, 2)} ");
				vMontoDetalleCase.AppendLine($" ELSE AnticipoCobrado.MontoOriginal ");
				vMontoDetalleCase.AppendLine($" END)");
				vSqlMontoOriginal = vMontoDetalleCase.ToString();//insSql.IIF("anticipo.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), insSql.RoundToNDecimals(vSqlMontoOriginal + " / " + vSqlCambio, 2), vSqlMontoOriginal, true);

				vMontoDetalleCase = new StringBuilder();
				vMontoDetalleCase.AppendLine($"(CASE WHEN {vSqlCobCodMonedaIgualML} AND {vSqlAntCobCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlSaldoActual + " / " + vSqlCambio, 2)} ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlCobCodMonedaIgualML} AND {vSqlAntCobCodMonedaDifML} THEN {vSqlSaldoActual} ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlCobCodMonedaDifML} AND {vSqlAntCobCodMonedaIgualML} THEN {insSql.RoundToNDecimals(vSqlSaldoActual + " / " + vSqlCambio, 2)} ");
				vMontoDetalleCase.AppendLine($" ELSE {vSqlSaldoActual} ");
				vMontoDetalleCase.AppendLine($" END)");
				vSqlSaldoActual = vMontoDetalleCase.ToString();//insSql.IIF("anticipo.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), insSql.RoundToNDecimals(vSqlSaldoActual + " / " + vSqlCambio, 2), vSqlSaldoActual, true);

				//vSqlMonedaTotales = insSql.IIF("anticipo.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), "'Bolívares expresados en " + valNombreMoneda + "'", "Anticipo.Moneda", true);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
				string vSqlCobranzaCodigoMoneda = "Cobranza.CodigoMoneda";
				string vSqlAntCobradCodigoMonedaAnticipo = "AnticipoCobrado.CodigoMoneda";
				string vSqlCobCodMonedaIgualML = vSqlCobranzaCodigoMoneda + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlCobCodMonedaDifML = vSqlCobranzaCodigoMoneda + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlAntCobCodMonedaIgualML = vSqlAntCobradCodigoMonedaAnticipo + " = " + insSql.ToSqlValue(vCodigoMonedaLocal);
				string vSqlAntCobCodMonedaDifML = vSqlAntCobradCodigoMonedaAnticipo + " <> " + insSql.ToSqlValue(vCodigoMonedaLocal);

				StringBuilder vMontoDetalleCase = new StringBuilder();
				vMontoDetalleCase.AppendLine($"(CASE WHEN {vSqlAntCobradCodigoMonedaAnticipo} = {vSqlCobranzaCodigoMoneda} AND {vSqlAntCobCodMonedaIgualML} THEN AnticipoCobrado.MontoAplicadoMonedaOriginal ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlAntCobradCodigoMonedaAnticipo} = {vSqlCobranzaCodigoMoneda}  AND {vSqlAntCobCodMonedaDifML} THEN AnticipoCobrado.MontoAplicado ");
				vMontoDetalleCase.AppendLine($" WHEN  {vSqlAntCobradCodigoMonedaAnticipo} <> {vSqlCobranzaCodigoMoneda} AND {vSqlAntCobCodMonedaIgualML} THEN AnticipoCobrado.MontoAplicadoMonedaOriginal ");
				vMontoDetalleCase.AppendLine($" WHEN {vSqlAntCobradCodigoMonedaAnticipo} <> {vSqlCobranzaCodigoMoneda} AND {vSqlAntCobCodMonedaDifML} THEN AnticipoCobrado.MontoAplicadoMonedaOriginal ");
				vMontoDetalleCase.AppendLine($" ELSE 0 ");
				vMontoDetalleCase.AppendLine($" END)");
				vSqlMontoCobrado = vMontoDetalleCase.ToString();
				//vSqlMonedaTotales = insSql.IIF("DocumentoCobrado.CodigoMonedaDeCxC = " + insSql.ToSqlValue(vCodigoMonedaLocal), "'Bolívares expresados en " + valNombreMoneda + "'", "CxC.Moneda", true);
			}
			/* FIN */
			vSql.AppendLine("CTE_AnticipoHistoricoCliente AS (");
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	'1' AS TipoReporte, ");
			vSql.AppendLine("	anticipo.CodigoCliente AS Codigo, ");
			vSql.AppendLine("	Cliente.Nombre, ");
			vSql.AppendLine("	anticipo.CodigoMoneda AS CodigoMonedaAnticipo,");
			vSql.AppendLine("	anticipo.Moneda AS MonedaAnticipo, ");
			vSql.AppendLine("	anticipo.Cambio, ");
			vSql.AppendLine("	Cobranza.Moneda, ");
			vSql.AppendLine("	Cobranza.CambioAbolivares, ");
			vSql.AppendLine("	anticipo.Moneda AS MonedaReporte, ");
			vSql.AppendLine("	'Anticipos' AS TituloTipoReporte, ");
			vSql.AppendLine("	anticipo.Fecha AS FechaDocumento, ");
			vSql.AppendLine("	'' AS FechaVencimiento, ");
			vSql.AppendLine("	'Cobrado' AS TipoDeDocumento, ");
			vSql.AppendLine("	anticipo.Numero AS NumeroDocumento, ");
			vSql.AppendLine("	CAST(anticipo.ConsecutivoAnticipo AS VARCHAR) AS NumeroDocumentoGrupo, ");
			vSql.AppendLine($"	{vSqlMontoOriginal} AS MontoOriginal, ");
			vSql.AppendLine($"	{vSqlSaldoActual} AS SaldoActual, ");
			vSql.AppendLine("	'Cobro' AS TipoDocumentoDetalle, ");
			vSql.AppendLine("	Cobranza.Numero AS NumeroCobranza, ");
			vSql.AppendLine("	Cobranza.Fecha AS FechaCobranza, ");
			vSql.AppendLine($"	{vSqlMontoCobrado} AS MontoCobrado, ");
			vSql.AppendLine("	anticipoCobrado.Cambio AS CambioCobrado, ");
			vSql.AppendLine("	Cobranza.StatusCobranza");
			vSql.AppendLine("FROM anticipo LEFT JOIN Cobranza RIGHT JOIN anticipoCobrado ");
			vSql.AppendLine("	ON Cobranza.Numero = anticipoCobrado.NumeroCobranza ");
			vSql.AppendLine("	AND Cobranza.ConsecutivoCompania = anticipoCobrado.ConsecutivoCompania ");
			vSql.AppendLine("	ON  anticipo.ConsecutivoAnticipo = anticipoCobrado.ConsecutivoAnticipoUsado ");
			vSql.AppendLine("	AND anticipo.ConsecutivoCompania = anticipoCobrado.ConsecutivoCompania ");
			vSql.AppendLine("	INNER JOIN Cliente ");
			vSql.AppendLine("	ON anticipo.CodigoCliente = Cliente.Codigo ");
			vSql.AppendLine("	AND anticipo.ConsecutivoCompania = Cliente.ConsecutivoCompania");
			vSql.AppendLine($"WHERE (anticipo.Status <> '1') AND (anticipo.EsUnaDevolucion = 'N') AND (anticipo.Tipo = '0') AND (Cobranza.StatusCobranza <> '1') AND anticipo.ConsecutivoCompania = {insSql.ToSqlValue(valConsecutivoCompania)})");
			return vSql.ToString();
		}

		private string SqlCTESaldoInicialCxCHistoricoCliente(int valConsecutivoCompania, DateTime valFechaDesde, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("CTE_SaldoInicialCxCHistoricoCliente AS (");
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	CodigoCliente, ");
			vSql.AppendLine("	CodigoMoneda, ");
			vSql.AppendLine("	SUM((MontoExento + MontoGravado + MontoIva)) AS SaldoInicialOriginal,");
			vSql.AppendLine("	SUM(ROUND(CambioAbolivares * (MontoExento + MontoGravado + MontoIva), 2)) AS SaldoInicialMonedaLocal");
			vSql.AppendLine("FROM CxC");
			vSql.AppendLine("WHERE (Status <> '4') AND (Fecha < " + insSql.ToSqlValue(valFechaDesde) + ") AND ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
			vSql.AppendLine("GROUP BY CodigoCliente, CodigoMoneda)");
			return vSql.ToString();
		}

		private string SqlCTESaldoInicialAnticipoHistoricoCliente(int valConsecutivoCompania, DateTime valFechaDesde, string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("CTE_SaldoInicialAnticipoHistoricoCliente AS (");
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	CodigoCliente, ");
			vSql.AppendLine("	CodigoMoneda, ");
			vSql.AppendLine("	SUM(MontoTotal) AS SaldoInicialOriginal,");
			vSql.AppendLine("	SUM(ROUND(Cambio * MontoTotal, 2)) AS SaldoInicialMonedaLocal");
			vSql.AppendLine("FROM anticipo");
			vSql.AppendLine("WHERE (Status <> '1') AND (Tipo = '0') AND (Fecha < " + insSql.ToSqlValue(valFechaDesde) + ") AND ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
			vSql.AppendLine("GROUP BY CodigoCliente, CodigoMoneda)");
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
				vSqlMontoInicial = insSql.RoundToNDecimals(vSqlMontoInicial + " * " + vSqlCambio, 2);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				vSqlCambioMasCercano = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= FechaDocumento ORDER BY FechaDeVigencia DESC)", "1");
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

		private string sqlCalcularSaldoInicialCxC(string valCodigoMoneda, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valSaldoInicial) {
			string vCalculaSaldoInicialCxC = "";
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
				vSqlMontoInicial = insSql.RoundToNDecimals(vSqlMontoInicial + " * " + vSqlCambio, 2);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC)", "1");
				vSqlCambioMasCercano = insSql.IsNull("(SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= FechaDocumento ORDER BY FechaDeVigencia DESC)", "1");
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
			vCalculaSaldoInicialCxC = vSqlMontoInicial;
			return vCalculaSaldoInicialCxC;
		}

	} //End of class clsClienteSql
} //End of namespace Galac.Saw.Brl.Cliente