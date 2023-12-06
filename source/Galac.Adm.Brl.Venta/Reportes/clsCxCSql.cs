using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.DefGen;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.Venta.Reportes {
	public class clsCxCSql {
		private QAdvSql insSql;

		public clsCxCSql() {
			insSql = new QAdvSql("");
		}

		#region Metodos Generados
		public string SqlCxCPendientesEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, Saw.Lib.eMonedaParaImpresion valMonedaReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			string vMonedaLocal;
			bool vIsInMonedaLocal;
			bool vIsInTasaDelDia;
			string vSqlCambio;

			Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Comun.Brl.TablasGen.clsMonedaLocalActual();

			vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
			vMonedaLocal = vMonedaLocalActual.NombreMoneda(LibDate.Today());
			vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocal, valMonedaReporte.GetDescription());
			vIsInTasaDelDia = valTipoTasaDeCambio == Saw.Lib.eTasaDeCambioParaImpresion.DelDia;

			if (vIsInMonedaLocal) {
				if (vIsInTasaDelDia) {
					vSqlCambio = new Saw.Lib.clsLibSaw().CampoMontoPorTasaDeCambioSql("CxC.CambioAbolivares", "CxC.Moneda", "1", false, "");
				} else {
					vSqlCambio = insSql.IIF("CxC.CambioABolivares IS NULL OR CxC.CambioABolivares = 0", "1", "CxC.CambioABolivares", true);
				}
			} else {
				vSqlCambio = "1";
			}

			bool vUsaModuloContabilidad = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaModuloDeContabilidad"));

			if (vUsaModuloContabilidad) {
				vSql.AppendLine(CteComprobantesSql(valConsecutivoCompania, valFechaDesde, valFechaHasta));
			}
			vSql.AppendLine("SELECT Cliente.Codigo,");
			vSql.AppendLine("Cliente.Nombre,");
			vSql.AppendLine("Cliente.Contacto,");
			vSql.AppendLine("CxC.Fecha,");
			vSql.AppendLine("CxC.Numero,");
			vSql.AppendLine((vIsInMonedaLocal ? insSql.ToSqlValue(vMonedaLocal) : "CxC.Moneda") + " AS Moneda,");
			vSql.AppendLine(SqlCampoStatusStrCxCPendientesEntreFechas());

			string vSqlCampoCxCMontoOriginal = insSql.IIF("CxC.Status = " + insSql.EnumToSqlValue((int)eStatusCXC.ANULADO), "0", "(CxC.MontoExento + CxC.MontoGravado + CxC.MontoIVA) - CxC.MontoAbonado", true);
			vSql.AppendLine(vSqlCampoCxCMontoOriginal + " * " + vSqlCambio + " AS Monto");

			if (vUsaModuloContabilidad) {
				vSql.AppendLine("," + insSql.IIF("CTE_Comprobante.NumeroComprobante <>" + insSql.ToSqlValue(""), "CTE_Comprobante.NumeroComprobante", insSql.ToSqlValue("No Aplica"), true) + "AS NumeroComprobante");
			}

			vSql.AppendLine("FROM");
			vSql.AppendLine("Cliente");
			vSql.AppendLine("INNER JOIN CxC ON (Cliente.Codigo = CxC.CodigoCliente AND Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania)");

			if (vUsaModuloContabilidad) {
				vSql.Append(SqlUsaContabilidad());
			}

			vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "Cliente.ConsecutivoCompania", valConsecutivoCompania);
			vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "CxC.Fecha", valFechaDesde, valFechaHasta);

			string vSqlCampoCxCStatusParaWhere = "";
			vSqlCampoCxCStatusParaWhere = insSql.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", (int) eStatusCXC.PORCANCELAR, "", "=");
			vSqlCampoCxCStatusParaWhere = insSql.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", (int) eStatusCXC.ABONADO, "OR", "=");

			vSQLWhere = vSQLWhere + "AND (" + vSqlCampoCxCStatusParaWhere + ")";

			vSQLWhere = vSQLWhere + "ORDER BY CxC.Status, Moneda, CxC.Fecha, CxC.Numero";
			vSQLWhere = insSql.WhereSql(vSQLWhere);

			vSql.AppendLine(vSQLWhere);
			return vSql.ToString();
		}

		public string SqlCxCPorCliente(int valConsecutivoCompania, string valCodigoDelCliente, string valZonaCobranza, DateTime valFechaDesde, DateTime valFechaHasta, eClientesOrdenadosPor valClientesOrdenadosPor, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			string vMonedaLocal;
			bool vIsInMonedaLocal;
			bool vIsInTasaDelDia;
			string vSqlCambio;

			Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Comun.Brl.TablasGen.clsMonedaLocalActual();

			vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
			vMonedaLocal = vMonedaLocalActual.NombreMoneda(LibDate.Today());
			vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocal, valMonedaDelReporte.GetDescription());
			vIsInTasaDelDia = valTipoTasaDeCambio == Saw.Lib.eTasaDeCambioParaImpresion.DelDia;

			if (vIsInMonedaLocal) {
				if (vIsInTasaDelDia) {
					vSqlCambio = new Saw.Lib.clsLibSaw().CampoMontoPorTasaDeCambioSql("CxC.CambioAbolivares", "CxC.Moneda", "1", false, "");
				} else {
					vSqlCambio = insSql.IIF("CxC.CambioABolivares IS NULL OR CxC.CambioABolivares = 0", "1", "CxC.CambioABolivares", true);
				}
			} else {
				vSqlCambio = "1";
			}

			vSql.AppendLine("SELECT");
			vSql.AppendLine(SqlCampoEstatusCxCPorCliente());
			vSql.AppendLine("Cliente.Contacto,");
			vSql.AppendLine("CxC.Numero,");
			vSql.AppendLine("CxC.Descripcion,");
			vSql.AppendLine("CxC.FechaVencimiento,");
			vSql.AppendLine("Cliente.Codigo,");

			string vSqlCampoCxCMontoOriginal = insSql.IIF("CxC.Status = " + insSql.EnumToSqlValue((int) eStatusCXC.ANULADO), "0", "(CxC.MontoExento + CxC.MontoGravado + CxC.MontoIVA) - CxC.MontoAbonado", true);
			vSql.AppendLine(vSqlCampoCxCMontoOriginal + " * " + vSqlCambio + " AS Monto,");

			vSql.AppendLine("Cliente.Nombre AS NombreCliente,");
			vSql.AppendLine("Cliente.Telefono,");
			vSql.AppendLine("Cliente.Direccion,");
			vSql.AppendLine("Cliente.ZonaDeCobranza AS ZonaCobranza,");
			vSql.AppendLine((vIsInMonedaLocal ? insSql.ToSqlValue(vMonedaLocal) : "CxC.Moneda") + " AS Moneda,");
			vSql.AppendLine("CxC.NumeroComprobanteFiscal AS NumFiscal");
			vSql.AppendLine("FROM");
			vSql.AppendLine("Cliente");
			vSql.AppendLine("INNER JOIN CxC ON (Cliente.Codigo = CxC.CodigoCliente AND Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania)");
			vSql.AppendLine("RIGHT JOIN Moneda ON CxC.CodigoMoneda = Moneda.Codigo");

			vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "Cliente.ConsecutivoCompania", valConsecutivoCompania);
			vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "CxC.Fecha", valFechaDesde, valFechaHasta);

			string vSqlCampoCxCStatusParaWhere = "";
			vSqlCampoCxCStatusParaWhere = insSql.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", ( int ) eStatusCXC.PORCANCELAR, "", "=");
			vSqlCampoCxCStatusParaWhere = insSql.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", ( int ) eStatusCXC.CANCELADO, "OR", "=");
			vSqlCampoCxCStatusParaWhere = insSql.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", ( int ) eStatusCXC.ABONADO, "OR", "=");
			vSqlCampoCxCStatusParaWhere = insSql.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", ( int ) eStatusCXC.ANULADO, "OR", "=");

			vSQLWhere = vSQLWhere + "AND (" + vSqlCampoCxCStatusParaWhere + ")";

			if (!LibString.IsNullOrEmpty(valCodigoDelCliente)) {
				vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Cliente.Codigo", valCodigoDelCliente);
			}

			if (!LibString.IsNullOrEmpty(valZonaCobranza)) {
				vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Cliente.ZonaDeCobranza", valZonaCobranza);
			}

			string vSqlClientesOrdenadosPor = "";
			if (valClientesOrdenadosPor == eClientesOrdenadosPor.PorCodigo) {
				vSqlClientesOrdenadosPor = "Cliente.Codigo";
			}
			if (valClientesOrdenadosPor == eClientesOrdenadosPor.PorNombre) {
				vSqlClientesOrdenadosPor = "Cliente.Nombre";
			}

			vSQLWhere = vSQLWhere + "ORDER BY Moneda, Cliente.ZonaDeCobranza, " + vSqlClientesOrdenadosPor + ", CxC.FechaVencimiento";
			vSQLWhere = insSql.WhereSql(vSQLWhere);

			vSql.AppendLine(vSQLWhere);

			return vSql.ToString();
		}

		public string SqlCxCEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eInformeStatusCXC valStatusCxC, eInformeAgruparPor valAgruparPor, string valZonaDeCobranza, string valSectorDeNegocio, eMonedaDelInformeMM valMonedaDelInforme, string valMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, bool valMostrarNroComprobanteContable) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "CxC.ConsecutivoCompania", valConsecutivoCompania);
			vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "CxC.Fecha", valFechaDesde, valFechaHasta);
			if (valStatusCxC != eInformeStatusCXC.Todos) {
				vSQLWhere = insSql.SqlEnumValueWithAnd(vSQLWhere, "CxC.Status", (int)valStatusCxC);
			}
			if (valAgruparPor == eInformeAgruparPor.SectorDeNegocio) {
				if (!LibString.S1IsEqualToS2(valSectorDeNegocio, "TODOS")) {
					vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Cliente.SectorDeNegocio", valSectorDeNegocio);
				}
			} else if (valAgruparPor == eInformeAgruparPor.ZonaDeCobranza) {
				if (!LibString.S1IsEqualToS2(valZonaDeCobranza, "")) {
					vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Cliente.ZonaDeCobranza", valZonaDeCobranza);
				}
			}
/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlMonto = insSql.IIF("CxC.Status = " + insSql.EnumToSqlValue((int)eStatusCXC.ANULADO), "0", "((CxC.MontoExento + CxC.MontoGravado + CxC.MontoIva) - CxC.MontoAbonado)", true);
			string vSqlCambioOriginal = "CxC.CambioAbolivares";
			string vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valMoneda) + " AND FechaDeVigencia <= "+ insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
			string vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valMoneda) + " AND FechaDeVigencia <= CxC.Fecha ORDER BY FechaDeVigencia DESC), 1)";
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;
/* FIN */

/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlMonto = insSql.RoundToNDecimals(vSqlMonto + " * " + vSqlCambio, 2);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.EnDivisasYBolivaresEnDivisas) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				vSqlCambio = insSql.IIF("CxC.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
				vSqlMonto = insSql.RoundToNDecimals(vSqlMonto + " / " + vSqlCambio, 2);
			} else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
			}
/* FIN */

			string vSqlStatus = "(CASE CxC.Status WHEN '0' THEN 'Por Cancelar' WHEN '1' THEN 'Cancelado' WHEN '2' THEN 'Cheque Devuelto' WHEN '3' THEN 'Abonado' WHEN '4' THEN 'Anulado' WHEN '5' THEN 'Refinanciado' ELSE 'N/A' END)";

			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	Cliente.SectorDeNegocio,");
			vSql.AppendLine("	Cliente.ZonaDeCobranza,");
			vSql.AppendLine("	CxC.Status, ");
			vSql.AppendLine("	" + vSqlStatus + " AS StatusStr, ");
			vSql.AppendLine("	CxC.CodigoMoneda, ");
			vSql.AppendLine("	CxC.Moneda, ");
			vSql.AppendLine("	CxC.Fecha, ");
			vSql.AppendLine("	CxC.Numero, ");
			vSql.AppendLine("	CxC.CodigoCliente, ");
			vSql.AppendLine("	Cliente.Nombre AS NombreCliente,");
			vSql.AppendLine("	" + vSqlMonto + " AS Monto,");
			vSql.AppendLine("	" + vSqlCambio + " AS Cambio, ");
			vSql.AppendLine("	CxC.Descripcion, ");
			vSql.AppendLine("	Cliente.Contacto");
			if (valMostrarNroComprobanteContable && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "CaracteristicaDeContabilidadActiva")) {
				StringBuilder vSqlNroComprobanteContable = new StringBuilder();
				vSqlNroComprobanteContable.AppendLine(" (SELECT TOP 1 C.Numero ");
				vSqlNroComprobanteContable.AppendLine("FROM COMPROBANTE C INNER JOIN PERIODO P ON C.ConsecutivoPeriodo = P.ConsecutivoPeriodo ");
				vSqlNroComprobanteContable.AppendLine("WHERE P.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
				vSqlNroComprobanteContable.AppendLine(" AND C.NoDocumentoOrigen = CxC.TipoCxC " + insSql.CharConcat() + "'" + LibText.StandardSeparator() + "'" + insSql.CharConcat() + " CxC.Numero ");
				vSqlNroComprobanteContable.AppendLine(" AND C.GeneradoPor = " + insSql.EnumToSqlValue((int)eComprobanteGeneradoPorVBSaw.eCG_CXC));
				vSqlNroComprobanteContable.AppendLine(" AND " + insSql.SqlDateValueBetween("", "CxC.Fecha", valFechaDesde, valFechaHasta) + ")");
				vSql.AppendLine("   , " + vSqlNroComprobanteContable.ToString() + " AS NroComprobanteContable");
			}
			vSql.AppendLine("FROM CxC INNER JOIN Cliente");
			vSql.AppendLine("	ON CxC.ConsecutivoCompania = Cliente.ConsecutivoCompania");
			vSql.AppendLine("	AND CxC.CodigoCliente = Cliente.Codigo");
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			vSql.AppendLine("ORDER BY ");
			if (valAgruparPor == eInformeAgruparPor.SectorDeNegocio) {
				vSql.AppendLine("	Cliente.SectorDeNegocio,");
			} else if (valAgruparPor == eInformeAgruparPor.ZonaDeCobranza) {
				vSql.AppendLine("	Cliente.ZonaDeCobranza,");
			}
			vSql.AppendLine("	CxC.Status, ");
			vSql.AppendLine("	CxC.Moneda, ");
			vSql.AppendLine("	CxC.Fecha, ");
			vSql.AppendLine("	CxC.Numero ");

			return vSql.ToString();
		}
		#endregion //Metodos Generados

		#region Código Programador
		private string SqlCampoStatusStrCxCPendientesEntreFechas() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("(CASE");
			vSql.AppendLine("WHEN CxC.Status = " + insSql.EnumToSqlValue(( int ) eStatusCXC.PORCANCELAR) + " THEN " + insSql.ToSqlValue(eStatusCXC.PORCANCELAR.GetDescription(0)));
			vSql.AppendLine("WHEN CxC.Status = " + insSql.EnumToSqlValue(( int ) eStatusCXC.ABONADO) + " THEN " + insSql.ToSqlValue(eStatusCXC.ABONADO.GetDescription(0)) + " END");
			vSql.AppendLine(") AS StatusStr,");
			return vSql.ToString();
		}

		private string SqlCampoEstatusCxCPorCliente() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("(CASE");
			vSql.AppendLine("WHEN CxC.Status = " + insSql.EnumToSqlValue(( int ) eStatusCXC.PORCANCELAR) + " THEN " + insSql.ToSqlValue(eStatusCXC.PORCANCELAR.GetDescription(1)));
			vSql.AppendLine("WHEN CxC.Status = " + insSql.EnumToSqlValue(( int ) eStatusCXC.CANCELADO) + " THEN " + insSql.ToSqlValue(eStatusCXC.CANCELADO.GetDescription(1)));
			vSql.AppendLine("WHEN CxC.Status = " + insSql.EnumToSqlValue(( int ) eStatusCXC.ABONADO) + " THEN " + insSql.ToSqlValue(eStatusCXC.ABONADO.GetDescription(1)));
			vSql.AppendLine("WHEN CxC.Status = " + insSql.EnumToSqlValue(( int ) eStatusCXC.ANULADO) + " THEN " + insSql.ToSqlValue(eStatusCXC.ANULADO.GetDescription(1)) + " END");
			vSql.AppendLine(") AS Estatus,");
			return vSql.ToString();
		}

		private string SqlUsaContabilidad() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("LEFT JOIN CTE_Comprobante ON");
			vSql.AppendLine("(CxC.TipoCxc + " + insSql.Char(9) + " + CxC.Numero) = CTE_Comprobante.NoDocumentoOrigen AND");
			vSql.AppendLine("CxC.ConsecutivoCompania = CTE_Comprobante.ConsecutivoCompania AND");
			vSql.AppendLine("CxC.Fecha = CTE_Comprobante.FechaComprobante");
			return vSql.ToString();
		}

		private string CteComprobantesSql(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta) {
			string vSqlWhere = "";
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine(";WITH CTE_Comprobante(ConsecutivoCompania,NumeroComprobante,NoDocumentoOrigen,FechaComprobante)");
			vSql.AppendLine("AS");
			vSql.AppendLine("(SELECT");
			vSql.AppendLine("Periodo.ConsecutivoCompania,");
			vSql.AppendLine("Numero,");
			vSql.AppendLine("NoDocumentoOrigen,");
			vSql.AppendLine("Fecha");
			vSql.AppendLine("FROM");
			vSql.AppendLine("COMPROBANTE");
			vSql.AppendLine("INNER JOIN PERIODO ON");
			vSql.AppendLine("COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo AND");
			vSql.AppendLine("COMPROBANTE.GeneradoPor = '9' AND");
			vSql.AppendLine("COMPROBANTE.Fecha BETWEEN PERIODO.FechaAperturaDelPeriodo AND PERIODO.FechaCierreDelPeriodo");
			vSqlWhere = insSql.SqlDateValueBetween(vSqlWhere, "Comprobante.Fecha", valFechaDesde, valFechaHasta);
			vSqlWhere = insSql.SqlIntValueWithAnd(vSqlWhere, "Periodo.ConsecutivoCompania", valConsecutivoCompania);
			vSqlWhere = insSql.WhereSql(vSqlWhere);
			vSql.Append(vSqlWhere + ")");
			return vSql.ToString();
		}
		#endregion //Código Programador


		/* SQL Análisis de Vencimiento
		SET DATEFORMAT dmy  
		DECLARE @ConsecutivoCompania as int
		DECLARE @FechaHoy as smalldatetime
		DECLARE @CodigoMoneda as varchar(4)
		DECLARE @1erVcto as int
		DECLARE @2doVcto as int
		DECLARE @3erVcto as int

		SET @ConsecutivoCompania = 1
		SET @FechaHoy = '10/11/2023'
		SET @CodigoMoneda = 'USD'
		SET @1erVcto = 30
		SET @2doVcto = 60
		SET @3erVcto = 90

		;WITH 
		CTE_BaseAnalisisDeVencimientoCxC AS (
		SELECT        
			Cliente.ZonaDeCobranza, 
			CxC.CodigoCliente, 
			Cliente.Nombre AS NombreCliente, 
			CxC.Moneda, 
			@CodigoMoneda AS CodigoMonedaReporte, 
			DATEDIFF(d, CxC.FechaVencimiento, @FechaHoy) AS DiasVencidos, 
			CxC.MontoExento + CxC.MontoGravado + CxC.MontoIva AS MtoTotal,
			(CxC.MontoExento + CxC.MontoGravado + CxC.MontoIva) - CxC.MontoAbonado AS MtoRestante, 
			CxC.FechaVencimiento, 
			CxC.Numero, 
			CxC.Fecha,
			CxC.CodigoVendedor,
			Vendedor.Nombre AS NombreVendedor, 
			ISNULL(CxC.CambioAbolivares, 1) AS CambioOriginal,
			ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE Cambio.CodigoMoneda = @CodigoMoneda AND Cambio.FechaDeVigencia <= CxC.Fecha ORDER BY FechaDeVigencia DESC), 1) AS CambioMasCercano,
			ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = @CodigoMoneda AND FechaDeVigencia <= @FechaHoy ORDER BY FechaDeVigencia DESC), 1) AS CambioDelDia

		FROM CxC INNER JOIN Cliente ON CxC.ConsecutivoCompania = Cliente.ConsecutivoCompania AND CxC.CodigoCliente = Cliente.Codigo 
		INNER JOIN Adm.Vendedor ON cxC.ConsecutivoCompania = Vendedor.ConsecutivoCompania AND cxC.ConsecutivoVendedor = Vendedor.Consecutivo
		WHERE        (CxC.ConsecutivoCompania = 1) AND (CxC.Status = '3' OR CxC.Status = '0') 
		--AND (CxC.Fecha BETWEEN '01/01/1900' AND '31/12/2023') --solo para cuando es entre fechas
		)

		SELECT        
			ZonaDeCobranza, 
			CodigoCliente, 
			NombreCliente, 
			Moneda, 
			CodigoMonedaReporte, 
			(CASE WHEN DiasVencidos <= 0 THEN MtoRestante ELSE 0 END) AS MtoNoVencido, 
			(CASE WHEN DiasVencidos > 0 AND DiasVencidos <= @1erVcto THEN MtoRestante ELSE 0 END) AS Mto1Vto, 
			(CASE WHEN DiasVencidos > @1erVcto AND DiasVencidos <= @2doVcto THEN MtoRestante ELSE 0 END) AS Mto2Vto, 
			(CASE WHEN DiasVencidos > @2doVcto AND DiasVencidos <= @3erVcto THEN MtoRestante ELSE 0 END) AS Mto3Vto, 
			(CASE WHEN DiasVencidos > @3erVcto THEN MtoRestante ELSE 0 END) AS Mto4Vto, 
			@1erVcto AS Vto1, 
			@2doVcto AS Vto2, 
			@3erVcto AS Vto3, 
			DiasVencidos, 
			MtoRestante, 
			FechaVencimiento, 
			Numero, 
			Fecha, 
			MtoTotal,
			CambioOriginal,
			CambioMasCercano,
			CambioDelDia
		FROM            CTE_BaseAnalisisDeVencimientoCxC
		ORDER BY CodigoMonedaReporte, ZonaDeCobranza, CodigoCliente, FechaVencimiento 


		 */

		/* SQL Análisis de Vencimiento CxC entre Fechas
		 CTE_BaseAnalisisDeVencimientoCxC
SELECT        
	ZonaDeCobranza, 
	Moneda, 
	Moneda AS MonedaReporte, 
	MONTH(Fecha) AS Mes, 
	YEAR(Fecha) AS Ano, 
	CodigoCliente, 
	NombreCliente, 
	Numero, 
	Fecha, 
	FechaVencimiento, 
	DiasVencidos, 
	CodigoVendedor, 
	NombreVendedor, 
	(CASE WHEN DiasVencidos <= 0 THEN MtoRestante ELSE 0 END) AS MtoNoVencido, 
	(CASE WHEN DiasVencidos > 0 AND DiasVencidos <= @1erVcto THEN MtoRestante ELSE 0 END) AS Mto1Vto, 
	(CASE WHEN DiasVencidos > @1erVcto AND DiasVencidos <= @2doVcto THEN MtoRestante ELSE 0 END) AS Mto2Vto, 
	(CASE WHEN DiasVencidos > @2doVcto AND DiasVencidos <= @3erVcto THEN MtoRestante ELSE 0 END) AS Mto3Vto, 
	(CASE WHEN DiasVencidos > @3erVcto THEN MtoRestante ELSE 0 END) AS Mto4Vto, 
	MtoTotal,
	CambioOriginal,
	CambioMasCercano,
	CambioDelDia
FROM CTE_BaseAnalisisDeVencimientoCxC
ORDER BY ZonaDeCobranza, Moneda, Fecha, CodigoCliente, FechaVencimiento 		 
		 */


		/* SQL Análisis de Vencimiento CxC a una Fecha
		SET DATEFORMAT dmy

		DECLARE @ConsecutivoCompania as int	
		DECLARE @FechaCorte as smalldatetime
		DECLARE @1erVcto as int
		DECLARE @2doVcto as int
		DECLARE @3erVcto as int
		DECLARE @CodigoMonedaLocal as varchar(4)

		SET @ConsecutivoCompania = 1
		SET @FechaCorte = '30/06/2020'
		SET @1erVcto = 30
		SET @2doVcto = 60
		SET @3erVcto = 90
		SET @CodigoMonedaLocal = 'VED'


		;WITH CTE_BaseAnalisisDeVencimientoAUnaFecha AS (
		SELECT 
			Numero,
			Status AS StatusCxC,
			CodigoCliente,
			Origen,
			TipoCxc,
			(CASE WHEN TipoCxc = '0' THEN 'Factura' WHEN TipoCxc = '1' THEN 'Giro' WHEN TipoCxc = '2' THEN 'Cheque Devuelto' WHEN TipoCxc = '3' THEN 'Nota de Crédito' WHEN TipoCxc = '4' THEN 'Nota de Débito' WHEN TipoCxc = '5' THEN 'Nota de Entrega' WHEN TipoCxc = '6' THEN 'No Asignado' WHEN TipoCxc = '7' THEN 'Boleta de Venta' WHEN TipoCxc = '8' THEN 'Ticket Máquina Registradora' WHEN TipoCxc = '9' THEN 'Recibo por Honorarios' WHEN TipoCxc = ':' THEN 'Liquidación de Compra' WHEN TipoCxc = ';' THEN 'Otros' WHEN TipoCxc = '<' THEN 'Nota de Crédito Comprobante Fiscal' END) AS TipoCxCStr,
			(CASE WHEN CodigoMoneda = 'VEB' THEN 0 ELSE MontoAbonado END) AS MontoAbonado,
			MontoExento + MontoGravado + MontoIva AS MontoTotal,
			Fecha,
			ISNULL(FechaCancelacion, '01/01/1900') AS FechaCancelacion,
			FechaVencimiento,
			ISNULL(FechaAnulacion, '01/01/1900') AS FechaAnulacion,
			Moneda,
			CambioAbolivares,
			CodigoMoneda,
			DATEDIFF(d, FechaVencimiento, @FechaCorte) AS DiasVencidos	
		FROM CxC
		WHERE (CxC.ConsecutivoCompania = @ConsecutivoCompania) AND cxC.Fecha <= @FechaCorte --AND (CxC.Status IN ('3', '0')) AND (CxC.Fecha <= @FechaCorte)
		)
		, CTE_BaseDocCobrado AS (
		SELECT 
			DC.NumeroDelDocumentoCobrado,
			DC.TipoDeDocumentoCobrado,
			C.StatusCobranza,
			DC.CodigoMonedaDeCxC,
			C.CodigoMoneda,
			DC.CambioAMonedaLocal,
			C.CambioAbolivares,
			(CASE WHEN C.CodigoMoneda = 'VEB' THEN 0 ELSE DC.MontoAbonado END) AS MontoAbonado	
		FROM documentoCobrado DC INNER JOIN cobranza C ON DC.ConsecutivoCompania = C.ConsecutivoCompania AND DC.NumeroCobranza = C.Numero
		WHERE DC.ConsecutivoCompania = @ConsecutivoCompania AND (C.Fecha > @FechaCorte OR (ISNULL(C.FechaAnulacion, '01/01/1900') > @FechaCorte AND C.StatusCobranza = '1')) 
		)
		, CTE_CobranzasAReversar AS (
		SELECT        
			NumeroDelDocumentoCobrado, 
			TipoDeDocumentoCobrado,
			SUM((CASE StatusCobranza WHEN '0' THEN 1 ELSE -1 END) * ROUND((CASE WHEN CodigoMonedaDeCxC = CodigoMoneda THEN MontoAbonado ELSE (CASE WHEN CodigoMonedaDeCxC = @CodigoMonedaLocal THEN MontoAbonado * CambioAMonedaLocal ELSE MontoAbonado / CambioAbolivares END) END), 2)) AS MontoAbonadoMonedaCxC
		FROM CTE_BaseDocCobrado
		GROUP BY NumeroDelDocumentoCobrado, TipoDeDocumentoCobrado
		)
		, CTE_BaseReversada AS (
		SELECT 
			BaseCxC.Numero,
			BaseCxC.StatusCxC,
			BaseCxC.CodigoCliente,
			BaseCxC.Origen,	
			BaseCxC.TipoCxC,
			BaseCxC.TipoCxCStr,
			BaseCxC.MontoTotal,
			BaseCxC.MontoAbonado - ISNULL(ReversoDC.MontoAbonadoMonedaCxC, 0) AS MontoAbonadoActualizado,
			BaseCxC.Fecha,
			BaseCxC.FechaCancelacion,
			BaseCxC.FechaVencimiento,
			BaseCxC.FechaAnulacion,
			BaseCxC.Moneda,
			BaseCxC.CambioAbolivares,
			BaseCxC.CodigoMoneda,
			BaseCxC.DiasVencidos
		FROM CTE_BaseAnalisisDeVencimientoAUnaFecha BaseCxC LEFT JOIN CTE_CobranzasAReversar ReversoDC ON BaseCxC.Numero = ReversoDC.NumeroDelDocumentoCobrado AND BaseCxC.TipoCxc = ReversoDC.TipoDeDocumentoCobrado
		)
		, CTE_CxCStatusActualizados AS (
		SELECT 
			Numero,
			(CASE WHEN FechaAnulacion > @FechaCorte THEN '0' ELSE StatusCxC END) AS StatusCxCActualizado,
			CodigoCliente,
			Origen,
			TipoCxC,
			TipoCxCStr AS TipoStr,
			MontoTotal,
			MontoTotal - MontoAbonadoActualizado AS MontoRestanteActualizado,
			Fecha,
			FechaCancelacion,
			FechaVencimiento,
			FechaAnulacion,
			Moneda,
			CambioAbolivares,
			CodigoMoneda,
			DiasVencidos
		FROM CTE_BaseReversada
		)
		SELECT
			CxCAct.Moneda, 
			CxCAct.TipoCxC,
			Cliente.ZonaDeCobranza,
			CxCAct.FechaVencimiento, 
			Cliente.Codigo,
			Cliente.Nombre,
			CxCAct.Numero, 
			(CASE WHEN CxCAct.DiasVencidos <= 0 THEN CxCAct.MontoRestanteActualizado ELSE 0 END) AS MtoNoVencido, 
			(CASE WHEN CxCAct.DiasVencidos > 0 AND CxCAct.DiasVencidos <= @1erVcto THEN CxCAct.MontoRestanteActualizado ELSE 0 END) AS Mto1Vto, 
			(CASE WHEN CxCAct.DiasVencidos > @1erVcto AND CxCAct.DiasVencidos <= @2doVcto THEN CxCAct.MontoRestanteActualizado ELSE 0 END) AS Mto2Vto, 
			(CASE WHEN CxCAct.DiasVencidos > @2doVcto AND CxCAct.DiasVencidos <= @3erVcto THEN CxCAct.MontoRestanteActualizado ELSE 0 END) AS Mto3Vto, 
			(CASE WHEN CxCAct.DiasVencidos > @3erVcto THEN CxCAct.MontoRestanteActualizado ELSE 0 END) AS Mto4Vto, 
			@1erVcto AS Vto1, 
			@2doVcto AS Vto2, 
			@3erVcto AS Vto3, 
			CxCAct.DiasVencidos, 
			CxCAct.MontoRestanteActualizado, 
			CxCAct.Fecha, 
			CxCAct.MontoTotal,
			CxCAct.TipoStr
		FROM CTE_CxCStatusActualizados AS CxCAct INNER JOIN Cliente ON CxCAct.CodigoCliente = Cliente.Codigo
		ORDER BY CxCAct.Moneda, CxCAct.TipoCxC, Cliente.ZonaDeCobranza, Cliente.Codigo, CxCAct.FechaVencimiento

		 */

		/* SQL_Cuentas por Pagar entre Fechas-ML
		 * SQL_Cuentas por Pagar Pendientes entre Fechas-ML
		 * Para el manejo multimoneda en CxP entre fechas, ver CxC para los valores de Cambio
		 * 
		 * CxP y CxP pendientes entre fechas usan el mismo query, revisar el manejo de pantalla para ver las diferencias.
		 * El informe parece ser el mismo, de ser todo igual, fusionarlos en 1 solo,
		 


		SET DATEFORMAT dmy  
		SELECT        
			CxP.Numero, 
			CxP.CodigoProveedor, 
			Proveedor.NombreProveedor, 
			CxP.Fecha, 
			CxP.FechaVencimiento, 
			(CASE WHEN CxP.Status = '0' THEN 'Por Cancelar' WHEN CxP.Status = '1' THEN 'Cancelado' WHEN CxP.Status = '2' THEN 'Cheque Devuelto' WHEN CxP.Status = '3' THEN 'Abonado' WHEN CxP.Status = '4' THEN 'Anulado' WHEN CxP.Status = '5' THEN 'Refinanciado' END) AS StatusStr, 
			ROUND((CxP.MontoExento + MontoGravado + MontoIva) * CxP.CambioABolivares, 2) AS MontoTotal, 
			ROUND(((CxP.MontoExento + MontoGravado + MontoIva) - CxP.MontoAbonado) * CxP.CambioABolivares, 2) AS MontoRestante, 
			'Bolívar' AS MonedaReporte, 
			CxP.Moneda AS MonedaDocumento, 
			CxP.CambioABolivares AS Cambio
		FROM            CxP INNER JOIN
								 Proveedor ON CxP.CodigoProveedor = Proveedor.CodigoProveedor AND CxP.ConsecutivoCompania = Proveedor.ConsecutivoCompania
		WHERE        (CxP.Fecha BETWEEN '01/01/2022' AND '31/12/2022') AND (CxP.ConsecutivoCompania = 1)
		ORDER BY CxP.Status, MonedaReporte, CxP.Fecha, CxP.Numero, CxP.CodigoProveedor


		 */


	} //End of class clsCxCSql

} //End of namespace Galac.Adm.Brl.Venta

