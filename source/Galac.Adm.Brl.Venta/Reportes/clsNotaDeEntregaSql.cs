using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using LibGalac.Aos.DefGen;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.Venta.Reportes {

	public class clsNotaDeEntregaSql {
		QAdvSql insUtilSql;
		public clsNotaDeEntregaSql() {
			insUtilSql = new QAdvSql("");
		}

		#region Metodos Generados		
		public string SqlNotaDeEntregaEntreFechasPorCliente(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, bool valIncluirNotasDeEntregasAnuladas, eCantidadAImprimir valCantidadAImprimir, eMonedaDelInformeMM valMonedaDelReporte, string valCodigoCliente, eTasaDeCambioParaImpresion valTasaDeCambio, string valCodigoMoneda) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhereBetweenDates = string.Empty;
			vSQLWhereBetweenDates = insUtilSql.SqlDateValueBetween(vSQLWhereBetweenDates, "Factura.Fecha", valFechaDesde, valFechaHasta);

			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "Factura.CambioABolivares";
			string vSqlMontoTotal = "Factura.TotalFactura";
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;

			if (valMonedaDelReporte == eMonedaDelInformeMM.EnBolivares) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = Factura.CodigoMoneda AND FechaDeVigencia <= " + insUtilSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlMontoTotal = insUtilSql.RoundToNDecimals(vSqlMontoTotal + " * " + vSqlCambio, 2);
			} else if (valMonedaDelReporte == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insUtilSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insUtilSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
				vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insUtilSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= Factura.Fecha ORDER BY FechaDeVigencia DESC), 1)";
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				vSqlCambio = insUtilSql.IIF("Factura.CodigoMoneda = " + insUtilSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
				vSqlMontoTotal = insUtilSql.RoundToNDecimals(vSqlMontoTotal + " / " + vSqlCambio, 2);
			} else if (valMonedaDelReporte == eMonedaDelInformeMM.EnMonedaOriginal) {
			}
			/* FIN */
			vSql.AppendLine($"{insUtilSql.SetDateFormat()}");
			vSql.AppendLine("SET NOCOUNT ON");

			vSql.AppendLine("SELECT	Factura.CodigoCliente	AS CodigoCliente,");
			vSql.AppendLine("	Cliente.Nombre	AS Cliente,");
			vSql.AppendLine("   Factura.Fecha	AS Fecha,");
			vSql.AppendLine("   Factura.Numero	AS Numero,");
			if (valMonedaDelReporte == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSql.AppendLine($"	(SELECT Nombre FROM Moneda WHERE Codigo = {insUtilSql.ToSqlValue(valCodigoMoneda)}) AS Moneda,");
			} else if (valMonedaDelReporte == eMonedaDelInformeMM.EnBolivares) {
				vSql.AppendLine($"	(SELECT Nombre FROM Moneda WHERE Codigo = {insUtilSql.ToSqlValue(vCodigoMonedaLocal)}) AS Moneda,");
			} else {
				vSql.AppendLine("	Factura.Moneda AS Moneda,");
			}
			vSql.AppendLine("	 Factura.Moneda AS MonedaDoc,");
			vSql.AppendLine($"   {vSqlCambio} AS Cambio,");
			vSql.AppendLine("    IIF(Factura.StatusFactura = 1, 'Anulada', '') AS EsAnulada,");
			vSql.AppendLine($"   IIF(Factura.StatusFactura = 1, 0, {vSqlMontoTotal}) AS TotalFactura");
			vSql.AppendLine("FROM Factura ");
			vSql.AppendLine("INNER JOIN Cliente		ON Cliente.Codigo = Factura.CodigoCliente AND Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania");
			vSql.AppendLine("WHERE Factura.TipoDeDocumento = 8");
			vSql.AppendLine($"AND {vSQLWhereBetweenDates}");

			if (valCantidadAImprimir == eCantidadAImprimir.One) {
				vSql.AppendLine($"AND Factura.CodigoCliente = {insUtilSql.ToSqlValue(valCodigoCliente)}");
			}
			if (valIncluirNotasDeEntregasAnuladas) {
				vSql.AppendLine("AND (Factura.StatusFactura = 0 OR Factura.StatusFactura = 1)");

			} else {
				vSql.AppendLine("AND Factura.StatusFactura = 0");
			}
			vSql.AppendLine($"AND Factura.ConsecutivoCompania = {valConsecutivoCompania}");
			vSql.AppendLine("ORDER BY    Cliente.Nombre,");
			vSql.AppendLine("	Factura.Moneda,");
			vSql.AppendLine("   Factura.Fecha");
			return vSql.ToString();
		}

		public string SqlNotaDeEntregaEntreFechasPorClienteDetallado(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eCantidadAImprimir valCantidadAImprimir, eMonedaDelInformeMM valMonedaDelReporte, string valCodigoCliente, eTasaDeCambioParaImpresion valTasaDeCambio, string valCodigoMoneda) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhereBetweenDates = string.Empty;
			vSQLWhereBetweenDates = insUtilSql.SqlDateValueBetween(vSQLWhereBetweenDates, "Factura.Fecha", valFechaDesde, valFechaHasta);

			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "Factura.CambioABolivares";
			string vSqlCambio = vSqlCambioOriginal;
			string vSqlMontoTotal = "Factura.TotalFactura";
			string vSqlMontoPrecio = "RenglonFactura.PrecioConIVA";
			string vSqlMontoTotalRenglon = "(RenglonFactura.PrecioConIVA * RenglonFactura.Cantidad)";
			vSqlMontoTotalRenglon += " * (1 + RenglonFactura.PorcentajeDescuento/100)";
			vSqlMontoTotalRenglon += " * (1 + Factura.PorcentajeDescuento/100)";
			vSqlMontoTotalRenglon = insUtilSql.RoundToNDecimals(vSqlMontoTotalRenglon, 2);
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");

			if (valMonedaDelReporte == eMonedaDelInformeMM.EnBolivares) {
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = Factura.CodigoMoneda AND FechaDeVigencia <= " + insUtilSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlMontoTotal = insUtilSql.RoundToNDecimals(vSqlMontoTotal + " * " + vSqlCambio, 2);
				vSqlMontoPrecio = insUtilSql.RoundToNDecimals(vSqlMontoPrecio + " * " + vSqlCambio, 2);
				vSqlMontoTotalRenglon = insUtilSql.RoundToNDecimals(vSqlMontoTotalRenglon + " * " + vSqlCambio, 2);
			} else if (valMonedaDelReporte == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insUtilSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insUtilSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
				vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insUtilSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= Factura.Fecha ORDER BY FechaDeVigencia DESC), 1)";
				if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				vSqlCambio = insUtilSql.IIF("Factura.CodigoMoneda = " + insUtilSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
				vSqlMontoTotal = insUtilSql.RoundToNDecimals(vSqlMontoTotal + " / " + vSqlCambio, 2);
				vSqlMontoPrecio = insUtilSql.RoundToNDecimals(vSqlMontoPrecio + " / " + vSqlCambio, 2);
				vSqlMontoTotalRenglon = insUtilSql.RoundToNDecimals(vSqlMontoTotalRenglon + " / " + vSqlCambio, 2);
			} else if (valMonedaDelReporte == eMonedaDelInformeMM.EnMonedaOriginal) {
			}
			/* FIN */

			vSql.AppendLine($"{insUtilSql.SetDateFormat()}");
			vSql.AppendLine("SET NOCOUNT ON");
			vSql.AppendLine("SELECT	Factura.CodigoCliente	AS CodigoCliente,");
			vSql.AppendLine("	Cliente.Nombre	AS Cliente,");
			vSql.AppendLine("   Factura.Fecha	AS Fecha,");
			vSql.AppendLine("	Factura.Numero	AS Numero,");
			if (valMonedaDelReporte == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSql.AppendLine($"	(SELECT Nombre FROM Moneda WHERE Codigo = {insUtilSql.ToSqlValue(valCodigoMoneda)}) AS Moneda,");
			} else if (valMonedaDelReporte == eMonedaDelInformeMM.EnBolivares) {
				vSql.AppendLine($"	(SELECT Nombre FROM Moneda WHERE Codigo = {insUtilSql.ToSqlValue(vCodigoMonedaLocal)}) AS Moneda,");
			} else {
				vSql.AppendLine("	Factura.Moneda AS Moneda,");
			}
			vSql.AppendLine("	Factura.Moneda AS MonedaDoc,");
			vSql.AppendLine($"  {vSqlCambio} AS Cambio,");
			vSql.AppendLine("   IIF(Factura.StatusFactura = 1, 'Anulada', '') AS EsAnulada,");
			vSql.AppendLine("	RenglonFactura.Articulo AS CodigoArticulo,");
			vSql.AppendLine("	RenglonFactura.Descripcion AS Descripcion,");
			vSql.AppendLine("	RenglonFactura.Cantidad AS Cantidad,");
			vSql.AppendLine($"	{vSqlMontoPrecio} AS Precio,");
			vSql.AppendLine($"	IIF(Factura.StatusFactura = 1, 0, {vSqlMontoTotalRenglon}) AS TotalRenglon,");
			vSql.AppendLine($"  IIF(Factura.StatusFactura = 1, 0, {vSqlMontoTotal}) AS TotalFactura");
			vSql.AppendLine("FROM Factura	");
			vSql.AppendLine("INNER JOIN RenglonFactura ON RenglonFactura.NumeroFactura = Factura.Numero AND RenglonFactura.ConsecutivoCompania = Factura.ConsecutivoCompania");
			vSql.AppendLine("INNER JOIN Cliente		ON Cliente.Codigo = Factura.CodigoCliente AND Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania");
			vSql.AppendLine("WHERE Factura.TipoDeDocumento = 8");
			vSql.AppendLine($"AND {vSQLWhereBetweenDates}");
			if (valCantidadAImprimir == eCantidadAImprimir.One) {
				vSql.AppendLine($"AND Factura.CodigoCliente = {insUtilSql.ToSqlValue(valCodigoCliente)}");
			}
			vSql.AppendLine("AND Factura.StatusFactura = 0");
			vSql.AppendLine($"AND Factura.ConsecutivoCompania = {valConsecutivoCompania}");
			vSql.AppendLine("ORDER BY    Cliente.Nombre,");
			vSql.AppendLine("            Factura.Moneda,");
			vSql.AppendLine("            Factura.Fecha");
			return vSql.ToString();
		}
		#endregion //Metodos Generados


	} //End of class clsNotaDeEntregaSql
} //End of namespace Galac..Brl.ComponenteNoEspecificado