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
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Brl.Venta.Reportes {

	public class clsNotaDeEntregaSql {
		QAdvSql insUtilSql;
		public clsNotaDeEntregaSql() {
			insUtilSql = new QAdvSql("");
		}

		#region Metodos Generados		
		public string SqlNotaDeEntregaEntreFechasPorCliente(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, bool valIncluirNotasDeEntregasAnuladas, eCantidadAImprimir valCantidadAImprimir, string valCodigoCliente) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhereBetweenDates = string.Empty;
			vSQLWhereBetweenDates = insUtilSql.SqlDateValueBetween(vSQLWhereBetweenDates, "Factura.Fecha", valFechaDesde, valFechaHasta);

			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			//string vSqlCambioDelDia;
			//string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "Factura.CambioABolivares";
			string vSqlMontoTotal = "Factura.TotalFactura";
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;

			//if (valMonedaDelReporte == eMonedaDelInformeMM.EnBolivares) {
			//	if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
			//		vSqlCambio = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = Factura.CodigoMoneda AND FechaDeVigencia <= " + insUtilSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
			//	} else {
			//		vSqlCambio = vSqlCambioOriginal;
			//	}
			//	vSqlMontoTotal = insUtilSql.RoundToNDecimals(vSqlMontoTotal + " * " + vSqlCambio, 2);
			//} else if (valMonedaDelReporte == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
			//	vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insUtilSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insUtilSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
			//	vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insUtilSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= Factura.Fecha ORDER BY FechaDeVigencia DESC), 1)";
			//	if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
			//		vSqlCambio = vSqlCambioDelDia;
			//	} else {
			//		vSqlCambio = vSqlCambioMasCercano;
			//	}
			//	vSqlCambio = insUtilSql.IIF("Factura.CodigoMoneda = " + insUtilSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
			//	vSqlMontoTotal = insUtilSql.RoundToNDecimals(vSqlMontoTotal + " / " + vSqlCambio, 2);
			//} else if (valMonedaDelReporte == eMonedaDelInformeMM.EnMonedaOriginal) {
			//}
			/* FIN */

			vSql.AppendLine("SELECT	");
			vSql.AppendLine("   Factura.CodigoCliente,");
			vSql.AppendLine("	Cliente.Nombre	AS Cliente,");
			//if (valMonedaDelReporte == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
			//	vSql.AppendLine($"	(SELECT Nombre FROM Moneda WHERE Codigo = {insUtilSql.ToSqlValue(valCodigoMoneda)}) AS Moneda,");
			//} else if (valMonedaDelReporte == eMonedaDelInformeMM.EnBolivares) {
			//	vSql.AppendLine($"	(SELECT Nombre FROM Moneda WHERE Codigo = {insUtilSql.ToSqlValue(vCodigoMonedaLocal)}) AS Moneda,");
			//} else {
			vSql.AppendLine("	Factura.Moneda AS Moneda,");
			//}
			vSql.AppendLine("   Factura.Fecha,");
			vSql.AppendLine("   Factura.Numero,");
			vSql.AppendLine("	Factura.Moneda AS MonedaDoc,");
			vSql.AppendLine($"   {vSqlCambio} AS Cambio,");
			vSql.AppendLine($"   {insUtilSql.IIF("Factura.StatusFactura = '0'", "''", "'Anulada'", true)} AS EsAnulada,");
			vSql.AppendLine($"   {insUtilSql.IIF("Factura.StatusFactura = '0'", vSqlMontoTotal, "0", true)} AS TotalFactura");
			vSql.AppendLine("FROM Factura INNER JOIN Cliente");
			vSql.AppendLine("ON Cliente.Codigo = Factura.CodigoCliente ");
			vSql.AppendLine("AND Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania");

			vSql.AppendLine("WHERE Factura.TipoDeDocumento = '8'");
			vSql.AppendLine($"AND {vSQLWhereBetweenDates}");
			if (valCantidadAImprimir == eCantidadAImprimir.One) {
				vSql.AppendLine($"AND Factura.CodigoCliente = {insUtilSql.ToSqlValue(valCodigoCliente)}");
			}
			if (valIncluirNotasDeEntregasAnuladas) {
				vSql.AppendLine("AND (Factura.StatusFactura = '0' OR Factura.StatusFactura = '1')");
			} else {
				vSql.AppendLine("AND Factura.StatusFactura = '0'");
			}
			vSql.AppendLine($"AND Factura.ConsecutivoCompania = {valConsecutivoCompania}");
			vSql.AppendLine("ORDER BY ");
			vSql.AppendLine("   Factura.CodigoCliente,");
			vSql.AppendLine("	Factura.Moneda,");
			vSql.AppendLine("   Factura.Fecha,");
			vSql.AppendLine("   Factura.Numero");
			return vSql.ToString();
		}

		public string SqlNotaDeEntregaEntreFechasPorClienteDetallado(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eCantidadAImprimir valCantidadAImprimir, string valCodigoCliente) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhereBetweenDates = string.Empty;
			vSQLWhereBetweenDates = insUtilSql.SqlDateValueBetween(vSQLWhereBetweenDates, "Factura.Fecha", valFechaDesde, valFechaHasta);

			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			//string vSqlCambioDelDia;
			//string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "Factura.CambioABolivares";
			string vSqlCambio = vSqlCambioOriginal;
			string vSqlMontoTotal = "Factura.TotalFactura";
			string vSqlMontoPrecio = "RenglonFactura.PrecioConIVA";
			string vSqlMontoTotalRenglon = "(RenglonFactura.PrecioConIVA * RenglonFactura.Cantidad)";
			vSqlMontoTotalRenglon += " * (1 + RenglonFactura.PorcentajeDescuento/100)";
			vSqlMontoTotalRenglon += " * (1 + Factura.PorcentajeDescuento/100)";
			vSqlMontoTotalRenglon = insUtilSql.RoundToNDecimals(vSqlMontoTotalRenglon, 2);
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");

			//if (valMonedaDelReporte == eMonedaDelInformeMM.EnBolivares) {
			//	if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
			//		vSqlCambio = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = Factura.CodigoMoneda AND FechaDeVigencia <= " + insUtilSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
			//	} else {
			//		vSqlCambio = vSqlCambioOriginal;
			//	}
			//	vSqlMontoTotal = insUtilSql.RoundToNDecimals(vSqlMontoTotal + " * " + vSqlCambio, 2);
			//	vSqlMontoPrecio = insUtilSql.RoundToNDecimals(vSqlMontoPrecio + " * " + vSqlCambio, 2);
			//	vSqlMontoTotalRenglon = insUtilSql.RoundToNDecimals(vSqlMontoTotalRenglon + " * " + vSqlCambio, 2);
			//} else if (valMonedaDelReporte == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
			//	vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insUtilSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insUtilSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
			//	vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insUtilSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= Factura.Fecha ORDER BY FechaDeVigencia DESC), 1)";
			//	if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
			//		vSqlCambio = vSqlCambioDelDia;
			//	} else {
			//		vSqlCambio = vSqlCambioMasCercano;
			//	}
			//	vSqlCambio = insUtilSql.IIF("Factura.CodigoMoneda = " + insUtilSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
			//	vSqlMontoTotal = insUtilSql.RoundToNDecimals(vSqlMontoTotal + " / " + vSqlCambio, 2);
			//	vSqlMontoPrecio = insUtilSql.RoundToNDecimals(vSqlMontoPrecio + " / " + vSqlCambio, 2);
			//	vSqlMontoTotalRenglon = insUtilSql.RoundToNDecimals(vSqlMontoTotalRenglon + " / " + vSqlCambio, 2);
			//} else if (valMonedaDelReporte == eMonedaDelInformeMM.EnMonedaOriginal) {
			//}
			/* FIN */

			vSql.AppendLine("SELECT	");
			//if (valMonedaDelReporte == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
			//	vSql.AppendLine($"	(SELECT Nombre FROM Moneda WHERE Codigo = {insUtilSql.ToSqlValue(valCodigoMoneda)}) AS Moneda,");
			//} else if (valMonedaDelReporte == eMonedaDelInformeMM.EnBolivares) {
			//	vSql.AppendLine($"	(SELECT Nombre FROM Moneda WHERE Codigo = {insUtilSql.ToSqlValue(vCodigoMonedaLocal)}) AS Moneda,");
			//} else {
				vSql.AppendLine("	Factura.Moneda AS Moneda,");
			//}
			vSql.AppendLine("   Factura.Fecha,");
			vSql.AppendLine("	Factura.Numero,");
			vSql.AppendLine("   Factura.CodigoCliente,");
			vSql.AppendLine("	Cliente.Nombre	AS Cliente,");
			vSql.AppendLine("	Factura.Moneda AS MonedaDoc,");
			vSql.AppendLine($"  {vSqlCambio} AS Cambio,");
			vSql.AppendLine("	RenglonFactura.Articulo AS CodigoArticulo,");
			vSql.AppendLine("	RenglonFactura.Descripcion,");
			vSql.AppendLine("	RenglonFactura.Cantidad,");
			vSql.AppendLine($"	{vSqlMontoPrecio} AS Precio,");
			vSql.AppendLine($"	{insUtilSql.IIF("Factura.StatusFactura = '0'", vSqlMontoTotalRenglon, "0", true)} AS TotalRenglon,");
			vSql.AppendLine($"  {insUtilSql.IIF("Factura.StatusFactura = '0'", vSqlMontoTotal, "0", true)} AS TotalFactura");

			vSql.AppendLine("FROM Factura INNER JOIN RenglonFactura ");
			vSql.AppendLine("ON RenglonFactura.NumeroFactura = Factura.Numero ");
			vSql.AppendLine("AND RenglonFactura.ConsecutivoCompania = Factura.ConsecutivoCompania ");
			vSql.AppendLine("AND Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento");
			vSql.AppendLine("INNER JOIN Cliente	");
			vSql.AppendLine("ON Cliente.Codigo = Factura.CodigoCliente ");
			vSql.AppendLine("AND Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania");

			vSql.AppendLine("WHERE Factura.TipoDeDocumento = '8'");
			vSql.AppendLine($"AND {vSQLWhereBetweenDates}");
			if (valCantidadAImprimir == eCantidadAImprimir.One) {
				vSql.AppendLine($"AND Factura.CodigoCliente = {insUtilSql.ToSqlValue(valCodigoCliente)}");
			}
			vSql.AppendLine("AND Factura.StatusFactura = '0'");
			vSql.AppendLine($"AND Factura.ConsecutivoCompania = {valConsecutivoCompania}");
			vSql.AppendLine("ORDER BY");
			vSql.AppendLine("   Factura.Moneda,");
			vSql.AppendLine("   Factura.CodigoCliente,");
			vSql.AppendLine("   Factura.Fecha,");
			vSql.AppendLine("	Factura.Numero");
			return vSql.ToString();
		}
		
		public string SqlNotasDeEntregaNoFacturadas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta) {
            StringBuilder vSql = new StringBuilder();
            //'Notas de Entregas Emitidas y no Generadas
            vSql.AppendLine("SELECT Factura.CodigoCliente, Cliente.Nombre, Factura.Fecha, Factura.Numero ");
            vSql.AppendLine("FROM Factura inner join Cliente ON Cliente.ConsecutivoCompania = factura.ConsecutivoCompania AND Cliente.Codigo = factura.CodigoCliente");
            vSql.AppendLine(" WHERE (Factura.EmitidaEnFacturaNumero = " + insUtilSql.ToSqlValue("") + " OR Factura.EmitidaEnFacturaNumero IS NULL)");
            vSql.AppendLine(" AND Factura.Fecha  >= " + insUtilSql.ToSqlValue(valFechaDesde));
            vSql.AppendLine(" AND Factura.Fecha  <= " + insUtilSql.ToSqlValue(valFechaHasta));
            vSql.AppendLine(" AND Factura.StatusFactura = " + insUtilSql.EnumToSqlValue((int)eStatusFactura.Emitida));
            vSql.AppendLine(" AND Factura.TipoDeDocumento = " + insUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaEntrega));
            vSql.AppendLine(" AND Factura.ConsecutivoCompania = " + insUtilSql.ToSqlValue(valConsecutivoCompania));
			//'Facturas Generadas desde Notas de Entrega y no Emitidas
			vSql.AppendLine(" UNION SELECT Factura.CodigoCliente, Cliente.Nombre, Factura.Fecha, Factura.Numero ");
            vSql.AppendLine("FROM Factura inner join Cliente ON Cliente.ConsecutivoCompania = factura.ConsecutivoCompania AND Cliente.Codigo = factura.CodigoCliente");
            vSql.AppendLine(" WHERE Factura.GeneradaPorNotaEntrega = " + insUtilSql.EnumToSqlValue(1));//enum_FacturaGeneraNotaEntrega.eFGNE_Generada
            vSql.AppendLine(" AND Factura.Fecha  >= " + insUtilSql.ToSqlValue(valFechaDesde));
            vSql.AppendLine(" AND Factura.Fecha  <= " + insUtilSql.ToSqlValue(valFechaHasta));
            vSql.AppendLine(" AND Factura.StatusFactura = " + insUtilSql.EnumToSqlValue((int)eStatusFactura.Borrador));
            vSql.AppendLine(" AND (Factura.TipoDeDocumento = " + insUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "OR Factura.TipoDeDocumento = " + insUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + ")");
            vSql.AppendLine(" AND Factura.ConsecutivoCompania = " + insUtilSql.ToSqlValue(valConsecutivoCompania));
			vSql.AppendLine("ORDER BY Factura.Fecha, Factura.Numero");
			return vSql.ToString();
		}
		#endregion //Metodos Generados


	} //End of class clsNotaDeEntregaSql
} //End of namespace Galac..Brl.ComponenteNoEspecificado