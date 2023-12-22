using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Lib;

namespace Galac.Saw.Brl.Cliente.Reportes {
	public class clsClienteSql {
		private QAdvSql insSql;

		public clsClienteSql() {
			insSql = new QAdvSql("");
		}

		#region Metodos Generados
		public string SqlHistoricoDeCliente(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoCliente, eMonedaDelInformeMM valMonedaDelInforme, string valCodigoMoneda, string valNombreMoneda, eTasaDeCambioParaImpresion valTasaDeCambio) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine(";WITH");
			vSql.AppendLine(SqlCTEInfoCxCHistoricoCliente(valConsecutivoCompania));
			vSql.AppendLine(",");
			vSql.AppendLine(SqlCTECxCHistoricoClientes(valConsecutivoCompania));
			vSql.AppendLine(",");
			vSql.AppendLine(SqlCTEAnticipoHistoricoCliente(valConsecutivoCompania));
			vSql.AppendLine(",");
			vSql.AppendLine(SqlCTESaldoInicialCxCHistoricoCliente(valConsecutivoCompania,valFechaDesde));
			vSql.AppendLine(",");
			vSql.AppendLine(SqlCTE_SaldoInicialAnticipoHistoricoCliente(valConsecutivoCompania, valFechaDesde));	
			vSql.AppendLine(SqlSelectInfoCxCHistoricoCliente(valFechaDesde, valFechaHasta, valCodigoCliente));
			vSql.AppendLine(" UNION ");
			vSql.AppendLine(SqlSelectCxCHistoricoClientes(valFechaDesde, valFechaHasta, valCodigoCliente));
			vSql.AppendLine(" UNION ");
			vSql.AppendLine(SqlSelectAnticipoHistoricoCliente(valFechaDesde, valFechaHasta, valCodigoCliente));
			vSql.AppendLine();
			vSql.AppendLine(" ORDER BY Nombre, TipoReporte, MonedaReporte, NumeroDocumentoGrupo, FechaDocumento, FechaCobranza, NumeroCobranza");
			return vSql.ToString();
		}

		private string SqlSelectInfoCxCHistoricoCliente(DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoCliente) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "FechaDocumento", valFechaDesde, valFechaHasta);
			vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Codigo", valCodigoCliente);


			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	'0' AS TipoReporte, ");
			vSql.AppendLine("	Codigo, ");
			vSql.AppendLine("	NombreCliente AS Nombre, ");
			vSql.AppendLine("	MonedaReporte, ");
			vSql.AppendLine("	MonedaReporte AS Moneda, ");
			vSql.AppendLine("	TituloTipoReporte,");
			vSql.AppendLine("	(SELECT SaldoInicialOriginal FROM CTE_SaldoInicialCxCHistoricoCliente WHERE (CodigoCliente = CTE_InfoCxCHistoricoCliente.Codigo) AND CodigoMoneda = CTE_InfoCxCHistoricoCliente.CodMoneda) AS SaldoInicial,");
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

		private string SqlSelectCxCHistoricoClientes(DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoCliente) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "FechaDocumento", valFechaDesde, valFechaHasta);
			vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Codigo", valCodigoCliente);

			vSql.AppendLine("SELECT");
			vSql.AppendLine("	'0' AS TipoReporte, ");
			vSql.AppendLine("	Codigo, ");
			vSql.AppendLine("	Nombre, ");
			vSql.AppendLine("	MonedaReporte, ");
			vSql.AppendLine("	Moneda, ");
			vSql.AppendLine("	TituloTipoReporte,");
			vSql.AppendLine("	(SELECT SaldoInicialOriginal FROM CTE_SaldoInicialCxCHistoricoCliente WHERE (CodigoCliente = CTE_CxCHistoricoClientes.Codigo AND CodigoMoneda = CTE_CxCHistoricoClientes.CodMoneda)) AS SaldoInicial,");
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

		private string SqlSelectAnticipoHistoricoCliente(DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoCliente) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "FechaDocumento", valFechaDesde, valFechaHasta);
			vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Codigo", valCodigoCliente);

			vSql.AppendLine("SELECT");
			vSql.AppendLine("	TipoReporte, ");
			vSql.AppendLine("	Codigo, ");
			vSql.AppendLine("	Nombre, ");
			vSql.AppendLine("	MonedaAnticipo AS MonedaReporte, ");
			vSql.AppendLine("	MonedaReporte AS Moneda, ");
			vSql.AppendLine("	TituloTipoReporte,");
			vSql.AppendLine("	(SELECT SaldoInicialOriginal FROM CTE_SaldoInicialAnticipoHistoricoCliente WHERE (CodigoCliente = CTE_AnticipoHistoricoCliente.Codigo) AND (CodigoMoneda = CTE_AnticipoHistoricoCliente.CodigoMonedaAnticipo)) AS SaldoInicial,");
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
			vSql.AppendLine("	(CASE WHEN StatusCobranza = '1' THEN 0 ELSE (MontoCobrado / CambioCobrado) END) AS MontoCobrado, ");
			vSql.AppendLine("	StatusCobranza");
			vSql.AppendLine("FROM CTE_AnticipoHistoricoCliente");
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}


			return vSql.ToString();
		}
		#endregion //Metodos Generados

		private string SqlCTEInfoCxCHistoricoCliente(int valConsecutivoCompania) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("CTE_InfoCxCHistoricoCliente AS (");
			vSql.AppendLine("SELECT");
			vSql.AppendLine("	CxC.CodigoCliente AS Codigo, ");
			vSql.AppendLine("	Cliente.Nombre AS NombreCliente,");
			vSql.AppendLine("	CxC.Numero AS NumeroDocumento, ");
			vSql.AppendLine("	CxC.Fecha AS FechaDocumento, ");
			vSql.AppendLine("	CxC.FechaVencimiento, ");
			vSql.AppendLine("	CxC.Moneda AS MonedaReporte, ");
			vSql.AppendLine("	CxC.CodigoMoneda AS CodMoneda, ");
			vSql.AppendLine("	CxC.CambioAbolivares AS CambioABolivares, ");
			vSql.AppendLine("	(CASE WHEN CxC.TipoCxC = '0' THEN 'FAC' WHEN CxC.TipoCxC = '1' THEN 'GRO' WHEN CxC.TipoCxC = '2' THEN 'C/D' WHEN CxC.TipoCxC = '3' THEN 'N/C' WHEN CxC.TipoCxC = '4' THEN 'N/D' WHEN CxC.TipoCxC = '5' THEN  'N/E' WHEN CxC.TipoCxC = '6' THEN 'N/A' WHEN CxC.TipoCxC = '7' THEN 'BOL' WHEN CxC.TipoCxC = '8' THEN 'TIC' WHEN CxC.TipoCxC = '9' THEN 'R/H' WHEN CxC.TipoCxC = ':' THEN 'L/C' WHEN CxC.TipoCxC = ';' THEN 'OTR' WHEN CxC.TipoCxC = '<' THEN 'N/C-CF' END) AS TipoDeDocumento, ");
			vSql.AppendLine("	'Cuentas por Cobrar' AS TituloTipoReporte, ");
			vSql.AppendLine("	'Cobro' AS TipoDocumentoDetalle, ");
			vSql.AppendLine("	CAST(CxC.CodigoCliente AS VARCHAR) + CHAR(9) + CAST(CxC.Numero AS VARCHAR) AS NumeroDocumentoGrupo, ");
			vSql.AppendLine("	(CxC.MontoExento + CxC.MontoGravado + CxC.MontoIva) AS MontoOriginal, ");
			vSql.AppendLine("	((CxC.MontoExento + CxC.MontoGravado + CxC.MontoIva) - CxC.MontoAbonado) AS SaldoActual");
			vSql.AppendLine("FROM Cliente INNER JOIN CxC ON Cliente.Codigo = CxC.CodigoCliente AND Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania");
			vSql.AppendLine("WHERE (CxC.Status <> '4') AND CxC.ConsecutivoCompania = " + LibConvert.ToStr(valConsecutivoCompania));
			vSql.AppendLine(")");
			return vSql.ToString();
		}

		private string SqlCTECxCHistoricoClientes(int valConsecutivoCompania) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("CTE_CxCHistoricoClientes AS (");
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	CxC.CodigoCliente AS Codigo, ");
			vSql.AppendLine("	Cliente.Nombre, CxC.Moneda AS MonedaReporte, ");
			vSql.AppendLine("	CxC.CodigoMoneda AS CodMoneda, ");
			vSql.AppendLine("	CxC.Fecha AS FechaDocumento, ");
			vSql.AppendLine("	ISNULL(Cobranza.Moneda, cxC.Moneda) AS Moneda, ");
			vSql.AppendLine("	Cobranza.CambioAbolivares, ");
			vSql.AppendLine("	CxC.FechaVencimiento, ");
			vSql.AppendLine("	(CASE WHEN CxC.TipoCxC = '0' THEN 'Factura' WHEN CxC.TipoCxC = '1' THEN 'Giro' WHEN CxC.TipoCxC = '2' THEN 'Cheque Devuelto' WHEN CxC.TipoCxC = '3' THEN 'Nota de Crédito' WHEN CxC.TipoCxC = '4' THEN 'Nota de Débito' WHEN CxC.TipoCxC = '5' THEN 'Nota de Entrega' WHEN CxC.TipoCxC = '6' THEN 'No Asignado' WHEN CxC.TipoCxC = '7' THEN 'Boleta de Venta' WHEN CxC.TipoCxC = '8' THEN 'Ticket Máquina Registradora' WHEN CxC.TipoCxC = '9' THEN 'Recibo por Honorarios' WHEN CxC.TipoCxC = ':' THEN 'Liquidación de Compra' WHEN CxC.TipoCxC = ';' THEN 'Otros' WHEN CxC.TipoCxC = '<' THEN 'Nota de Crédito Comprobante Fiscal' END) AS TipoDeDocumento, ");
			vSql.AppendLine("	CxC.Numero AS NumeroDocumento, ");
			vSql.AppendLine("	CAST(CxC.CodigoCliente AS VARCHAR) + CHAR(9) + CAST(CxC.Numero AS VARCHAR) AS NumeroDocumentoGrupo, ");
			vSql.AppendLine("	Cobranza.Numero AS NumeroCobranza, ");
			vSql.AppendLine("	Cobranza.Fecha AS FechaCobranza, ");
			vSql.AppendLine("	'Cuentas por Cobrar' AS TituloTipoReporte, ");
			vSql.AppendLine("	(CASE WHEN cobranza.TipoDeDocumento = '1' THEN 'Cobro(*)' ELSE 'Cobro' END) AS TipoDocumentoDetalle, ");
			vSql.AppendLine("	(CASE WHEN (cobranza.StatusCobranza = '1' OR cobranza.TipoDeDocumento = '1') THEN 0 ELSE DocumentoCobrado.MontoAbonadoEnMonedaOriginal END) AS MontoCobrado, ");
			vSql.AppendLine("	DocumentoCobrado.CambioAMonedaDeCobranza AS CambioMonedaCobranza, ");
			vSql.AppendLine("	DocumentoCobrado.CambioAMonedaLocal AS CambioALocalDesdeMonedaCxC, ");
			vSql.AppendLine("	Cobranza.StatusCobranza");
			vSql.AppendLine("FROM Cliente INNER JOIN");
			vSql.AppendLine("   CxC ON Cliente.Codigo = CxC.CodigoCliente AND Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania RIGHT OUTER JOIN");
			vSql.AppendLine("   Cobranza LEFT OUTER JOIN");
			vSql.AppendLine("   DocumentoCobrado ON Cobranza.Numero = DocumentoCobrado.NumeroCobranza AND Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania ON ");
			vSql.AppendLine("   CxC.Numero = DocumentoCobrado.NumeroDelDocumentoCobrado AND CxC.TipoCxc = DocumentoCobrado.TipoDeDocumentoCobrado AND CxC.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania");
			vSql.AppendLine("WHERE (CxC.Status <> '4') AND CxC.ConsecutivoCompania = " + LibConvert.ToStr(valConsecutivoCompania));
			vSql.AppendLine(")");

			return vSql.ToString();
		}

		private string SqlCTEAnticipoHistoricoCliente(int valConsecutivoCompania) {
			StringBuilder vSql = new StringBuilder();
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
			vSql.AppendLine("	(CASE WHEN AnticipoCobrado.MontoRestanteAlDia < Anticipo.MontoTotal THEN 0 ELSE Anticipo.MontoTotal END) AS MontoOriginal, ");
			vSql.AppendLine("	(CASE WHEN AnticipoCobrado.MontoRestanteAlDia < Anticipo.MontoTotal THEN 0 ELSE (Anticipo.MontoTotal - (Anticipo.MontoUsado + Anticipo.MontoDevuelto + Anticipo.MontoDiferenciaEnDevolucion)) END) AS SaldoActual, ");
			vSql.AppendLine("	'Cobro' AS TipoDocumentoDetalle, ");
			vSql.AppendLine("	Cobranza.Numero AS NumeroCobranza, ");
			vSql.AppendLine("	Cobranza.Fecha AS FechaCobranza, ");
			vSql.AppendLine("	(CASE WHEN AnticipoCobrado.MontoAplicado IS NULL THEN 0 ELSE AnticipoCobrado.MontoAplicado END) AS MontoCobrado, ");
			vSql.AppendLine("	anticipoCobrado.Cambio AS CambioCobrado, ");
			vSql.AppendLine("	Cobranza.StatusCobranza");
			vSql.AppendLine("FROM anticipo LEFT OUTER JOIN Cobranza RIGHT OUTER JOIN anticipoCobrado ");
			vSql.AppendLine("	ON Cobranza.Numero = anticipoCobrado.NumeroCobranza ");
			vSql.AppendLine("	AND Cobranza.ConsecutivoCompania = anticipoCobrado.ConsecutivoCompania ");
			vSql.AppendLine("	ON  anticipo.ConsecutivoAnticipo = anticipoCobrado.ConsecutivoAnticipoUsado ");
			vSql.AppendLine("	AND anticipo.ConsecutivoCompania = anticipoCobrado.ConsecutivoCompania ");
			vSql.AppendLine("	INNER JOIN Cliente ");
			vSql.AppendLine("	ON anticipo.CodigoCliente = Cliente.Codigo ");
			vSql.AppendLine("	AND anticipo.ConsecutivoCompania = Cliente.ConsecutivoCompania");
			vSql.AppendLine("WHERE (anticipo.Status <> '1') AND (anticipo.EsUnaDevolucion = 'N') AND (anticipo.Tipo = '0') AND (Cobranza.StatusCobranza <> '1') AND anticipo.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
			vSql.AppendLine(")");

			return vSql.ToString();
		}

		private string SqlCTESaldoInicialCxCHistoricoCliente(int valConsecutivoCompania, DateTime valFechaDesde) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("CTE_SaldoInicialCxCHistoricoCliente AS (");
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	CodigoCliente, ");
			vSql.AppendLine("	CodigoMoneda, ");
			vSql.AppendLine("	SUM((MontoExento + MontoGravado + MontoIva)) AS SaldoInicialOriginal,");
			vSql.AppendLine("	SUM(ROUND(CambioAbolivares * (MontoExento + MontoGravado + MontoIva), 2)) AS SaldoInicialMonedaLocal");
			vSql.AppendLine("FROM CxC");
			vSql.AppendLine("WHERE (Status <> '4') AND (Fecha < " + insSql.ToSqlValue(valFechaDesde) + ") AND ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
			vSql.AppendLine("GROUP BY CodigoCliente, CodigoMoneda");
			vSql.AppendLine(")");
			return vSql.ToString();
		}

		private string SqlCTE_SaldoInicialAnticipoHistoricoCliente(int valConsecutivoCompania, DateTime valFechaDesde) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("CTE_SaldoInicialAnticipoHistoricoCliente AS (");
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	CodigoCliente, ");
			vSql.AppendLine("	CodigoMoneda, ");
			vSql.AppendLine("	SUM(MontoTotal) AS SaldoInicialOriginal,");
			vSql.AppendLine("	SUM(ROUND(Cambio * MontoTotal, 2)) AS SaldoInicial");
			vSql.AppendLine("FROM anticipo");
			vSql.AppendLine("WHERE (Status <> '1') AND (Tipo = '0') AND (Fecha < " + insSql.ToSqlValue(valFechaDesde) + ") AND ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
			vSql.AppendLine("GROUP BY CodigoCliente, CodigoMoneda");
			vSql.AppendLine(")");

			return vSql.ToString();
		}

	} //End of class clsClienteSql

} //End of namespace Galac.Saw.Brl.Cliente