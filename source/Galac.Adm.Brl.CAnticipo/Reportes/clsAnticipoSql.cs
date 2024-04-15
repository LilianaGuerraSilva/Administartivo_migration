using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Lib;
using Galac.Adm.Ccl.CAnticipo;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Brl.CAnticipo.Reportes {
	public class clsAnticipoSql {

		private QAdvSql insSql;

		public clsAnticipoSql() {
			insSql = new QAdvSql("");
		}
		#region Metodos Generados
		public string SqlAnticipoPorClienteProveedor(int valConsecutivoCompania, eCantidadAImprimir valCantidadAImprimirStatus, eStatusAnticipo valStatusAnticipo, eCantidadAImprimir valCantidadAImprimirClienteProveedor, string valCodigoClienteProveedor, bool valOrdenarPorStatus, eMonedaDelInformeMM valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda, bool valEsCliente) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine(SqlAnticipoNoDevolucion(valEsCliente, valConsecutivoCompania, valCantidadAImprimirStatus, valStatusAnticipo, valCantidadAImprimirClienteProveedor,valCodigoClienteProveedor, valMonedaDelInformeMM, valTipoTasaDeCambio, valCodigoMoneda, valNombreMoneda));
			vSql.AppendLine("UNION");
			vSql.AppendLine(SqlAnticipoDevolucion(valEsCliente, valConsecutivoCompania, valCantidadAImprimirStatus, valStatusAnticipo, valCantidadAImprimirClienteProveedor, valCodigoClienteProveedor, valMonedaDelInformeMM, valTipoTasaDeCambio, valCodigoMoneda, valNombreMoneda));
			if (valCantidadAImprimirStatus == eCantidadAImprimir.All || valStatusAnticipo == eStatusAnticipo.Anulado) {
				vSql.AppendLine("UNION");
				vSql.AppendLine(SqlAnticipoAnulado(valEsCliente, valConsecutivoCompania, valCantidadAImprimirClienteProveedor, valCodigoClienteProveedor, valMonedaDelInformeMM, valTipoTasaDeCambio, valCodigoMoneda, valNombreMoneda));
			}
			vSql.AppendLine("ORDER BY MonedaReporte, ");
			if (valOrdenarPorStatus) {
				vSql.AppendLine("CodigoClienteProveedor, Status, ConsecutivoAnticipo");
			} else {
				vSql.AppendLine("CodigoClienteProveedor, Status, ConsecutivoAnticipo");
			}
			return vSql.ToString();
		}

		private string SqStatusStrAnticipos() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("(CASE Anticipo.Status ");
			vSql.AppendLine("WHEN " + insSql.EnumToSqlValue((int)eStatusAnticipo.Vigente) + " THEN " + insSql.ToSqlValue(eStatusAnticipo.Vigente.GetDescription()));
			vSql.AppendLine("WHEN " + insSql.EnumToSqlValue((int)eStatusAnticipo.Anulado) + " THEN " + insSql.ToSqlValue(eStatusAnticipo.Anulado.GetDescription()));
			vSql.AppendLine("WHEN " + insSql.EnumToSqlValue((int)eStatusAnticipo.ParcialmenteUsado) + " THEN " + insSql.ToSqlValue(eStatusAnticipo.ParcialmenteUsado.GetDescription()));
			vSql.AppendLine("WHEN " + insSql.EnumToSqlValue((int)eStatusAnticipo.CompletamenteUsado) + " THEN " + insSql.ToSqlValue(eStatusAnticipo.CompletamenteUsado.GetDescription()));
			vSql.AppendLine("WHEN " + insSql.EnumToSqlValue((int)eStatusAnticipo.CompletamenteDevuelto) + " THEN " + insSql.ToSqlValue(eStatusAnticipo.CompletamenteDevuelto.GetDescription()));
			vSql.AppendLine("WHEN " + insSql.EnumToSqlValue((int)eStatusAnticipo.ParcialmenteDevuelto) + " THEN " + insSql.ToSqlValue(eStatusAnticipo.ParcialmenteDevuelto.GetDescription()));
			vSql.AppendLine(" END) AS StatusStr,");
			return vSql.ToString();
		}

		private string SqlFromAnticipoClienteProveedor(bool valEsCliente) {
			StringBuilder vSql = new StringBuilder();
			if (valEsCliente) {
				vSql.AppendLine("FROM Anticipo INNER JOIN Cliente ON Anticipo.ConsecutivoCompania = cliente.ConsecutivoCompania AND Anticipo.CodigoCliente = cliente.Codigo");
			} else {
				vSql.AppendLine("FROM Anticipo INNER JOIN Adm.Proveedor AS Proveedor ON Anticipo.ConsecutivoCompania = Proveedor.ConsecutivoCompania AND Anticipo.CodigoProveedor = Proveedor.CodigoProveedor");
			}
			return vSql.ToString();
        }

		private string SqlWhereAnticipoClienteProveedor(bool valEsCliente, int valConsecutivoCompania, bool valEsUnaDevolucion, eCantidadAImprimir valCantidadAImprimirStatus, eStatusAnticipo valStatusAnticipo, eCantidadAImprimir valCantidadAImprimirClienteProveedor, string valCodigoClienteProveedor) {
			string vSqlWhere;
			vSqlWhere = insSql.SqlIntValueWithAnd("", "Anticipo.ConsecutivoCompania", valConsecutivoCompania);
			vSqlWhere = insSql.SqlBoolValueWithAnd(vSqlWhere, "Anticipo.EsUnaDevolucion ", valEsUnaDevolucion);
			if (valCantidadAImprimirStatus == eCantidadAImprimir.One) {
				if (valStatusAnticipo == eStatusAnticipo.Anulado) {
					vSqlWhere = insSql.SqlEnumValueWithAnd(vSqlWhere, "Anticipo.Status", (int)valStatusAnticipo);
                } else {
					vSqlWhere = insSql.SqlEnumValueWithAnd(vSqlWhere, "Anticipo.Status", (int)valStatusAnticipo);
					vSqlWhere = insSql.SqlEnumValueWithOperators(vSqlWhere, "Anticipo.Status", (int)eStatusAnticipo.Anulado, "", "<>");
				}
			} else {
                vSqlWhere = insSql.SqlEnumValueWithOperators(vSqlWhere, "Anticipo.Status", (int)eStatusAnticipo.Anulado, "", "<>");
            }
			if (valCantidadAImprimirClienteProveedor == eCantidadAImprimir.One) {
				if (valEsCliente) {
					vSqlWhere = insSql.SqlValueWithAnd(vSqlWhere, "Anticipo.CodigoCliente", valCodigoClienteProveedor);
                } else {
					vSqlWhere = insSql.SqlValueWithAnd(vSqlWhere, "Anticipo.CodigoProveedor", valCodigoClienteProveedor);
				}
			}
			vSqlWhere = insSql.WhereSql(vSqlWhere);
			return vSqlWhere;
		}

		public string SqlAnticipoNoDevolucion(bool valEsCliente, int valConsecutivoCompania, eCantidadAImprimir valCantidadAImprimirStatus, eStatusAnticipo valStatusAnticipo, eCantidadAImprimir valCantidadAImprimirClienteProveedor, string valCodigoClienteProveedor, eMonedaDelInformeMM valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "Anticipo.Cambio";
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;
			string vSqlMontoTotal = "Anticipo.MontoTotal";
			string vSqlMontoUsado = "Anticipo.MontoUsado";
			string vSqlMonedaTotales = "Anticipo.Moneda";

			if (valMonedaDelInformeMM == eMonedaDelInformeMM.EnBolivares) {
				if (valTipoTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = CxC.CodigoMoneda AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlMontoTotal = insSql.RoundToNDecimals(vSqlMontoTotal + " * " + vSqlCambio, 2);
				vSqlMontoUsado = insSql.RoundToNDecimals(vSqlMontoUsado + " * " + vSqlCambio, 2);
				vSqlMonedaTotales = "'Bolívares'";
			} else if (valMonedaDelInformeMM == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
				vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= CxC.Fecha ORDER BY FechaDeVigencia DESC), 1)";
				if (valTipoTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				vSqlCambio = insSql.IIF("Anticipo.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
				vSqlMontoTotal = insSql.RoundToNDecimals(vSqlMontoTotal + " / " + vSqlCambio, 2);
				vSqlMontoUsado = insSql.RoundToNDecimals(vSqlMontoUsado + " / " + vSqlCambio, 2);
				vSqlMonedaTotales = insSql.IIF("Anticipo.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), "'Bolívares expresados en " + valNombreMoneda + "'", "Anticipo.Moneda", true);
			} else if (valMonedaDelInformeMM == eMonedaDelInformeMM.EnMonedaOriginal) {
			}
			/* FIN */

			vSql.AppendLine("SELECT ");
			vSql.AppendLine("Anticipo.ConsecutivoAnticipo, ");
			vSql.AppendLine("Anticipo.Moneda, ");
			vSql.AppendLine("Anticipo.CodigoCliente AS CodigoClienteProveedor, ");
			vSql.AppendLine("cliente.Nombre AS NombreClienteProveedor, ");
			vSql.AppendLine("Anticipo.Status, ");
			vSql.AppendLine(SqStatusStrAnticipos());
			vSql.AppendLine("Anticipo.Fecha, ");
			vSql.AppendLine("Anticipo.Numero, ");
			vSql.AppendLine(vSqlCambio + " AS Cambio, ");
			vSql.AppendLine(vSqlMonedaTotales + " AS MonedaReporte, ");
			vSql.AppendLine("0 AS MontoAnulado, ");
			vSql.AppendLine(vSqlMontoTotal + " AS MontoTotal, ");
			vSql.AppendLine(vSqlMontoUsado + " AS MontoUsado, ");
			vSql.AppendLine("0 AS MontoDevuelto, ");
			vSql.AppendLine("0 AS MontoDiferenciaEnDevolucion, ");
			vSql.AppendLine("Anticipo.NumeroCheque");

			vSql.Append(SqlFromAnticipoClienteProveedor(valEsCliente));
			vSql.Append(SqlWhereAnticipoClienteProveedor(valEsCliente, valConsecutivoCompania, false, valCantidadAImprimirStatus, valStatusAnticipo, valCantidadAImprimirClienteProveedor, valCodigoClienteProveedor));
			return vSql.ToString();
		}

		public string SqlAnticipoDevolucion(bool valEsCliente, int valConsecutivoCompania, eCantidadAImprimir valCantidadAImprimirStatus, eStatusAnticipo valStatusAnticipo, eCantidadAImprimir valCantidadAImprimirClienteProveedor, string valCodigoClienteProveedor, eMonedaDelInformeMM valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "Anticipo.Cambio";
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;
			string vSqlMontoTotal = "Anticipo.MontoTotal";
			string vSqlMontoUsado = "Anticipo.MontoUsado";
			string vSqlMonedaTotales = "Anticipo.Moneda";

			if (valMonedaDelInformeMM == eMonedaDelInformeMM.EnBolivares) {
				if (valTipoTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = CxC.CodigoMoneda AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlMontoTotal = insSql.RoundToNDecimals(vSqlMontoTotal + " * " + vSqlCambio, 2);
				vSqlMontoUsado = insSql.RoundToNDecimals(vSqlMontoUsado + " * " + vSqlCambio, 2);
				vSqlMonedaTotales = "'Bolívares'";
			} else if (valMonedaDelInformeMM == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
				vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= CxC.Fecha ORDER BY FechaDeVigencia DESC), 1)";
				if (valTipoTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				vSqlCambio = insSql.IIF("Anticipo.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
				vSqlMontoTotal = insSql.RoundToNDecimals(vSqlMontoTotal + " / " + vSqlCambio, 2);
				vSqlMontoUsado = insSql.RoundToNDecimals(vSqlMontoUsado + " / " + vSqlCambio, 2);
				vSqlMonedaTotales = insSql.IIF("Anticipo.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), "'Bolívares expresados en " + valNombreMoneda + "'", "Anticipo.Moneda", true);
			} else if (valMonedaDelInformeMM == eMonedaDelInformeMM.EnMonedaOriginal) {
			}
			/* FIN */

			vSql.AppendLine("SELECT ");
			vSql.AppendLine("Anticipo.ConsecutivoAnticipo, ");
			vSql.AppendLine("Anticipo.Moneda, ");
			vSql.AppendLine("Anticipo.CodigoCliente AS CodigoClienteProveedor, ");
			vSql.AppendLine("cliente.Nombre AS NombreClienteProveedor, ");
			vSql.AppendLine("Anticipo.Status, ");
			vSql.AppendLine(SqStatusStrAnticipos());
			vSql.AppendLine("Anticipo.Fecha, ");
			vSql.AppendLine("Anticipo.Numero, ");
			vSql.AppendLine(vSqlCambio + " AS Cambio, ");
			vSql.AppendLine(vSqlMonedaTotales + " AS MonedaReporte, ");
			vSql.AppendLine("0 AS MontoAnulado, ");
			vSql.AppendLine(vSqlMontoTotal + " AS MontoTotal, ");
			vSql.AppendLine(vSqlMontoUsado + " AS MontoUsado, ");
			vSql.AppendLine("0 AS MontoDevuelto, ");
			vSql.AppendLine("0 AS MontoDiferenciaEnDevolucion, ");
			vSql.AppendLine("Anticipo.NumeroCheque");

			vSql.Append(SqlFromAnticipoClienteProveedor(valEsCliente));
			vSql.Append(SqlWhereAnticipoClienteProveedor(valEsCliente, valConsecutivoCompania, true, valCantidadAImprimirStatus, valStatusAnticipo, valCantidadAImprimirClienteProveedor, valCodigoClienteProveedor));

			return vSql.ToString();
		}

		public string SqlAnticipoAnulado(bool valEsCliente, int valConsecutivoCompania, eCantidadAImprimir valCantidadAImprimirClienteProveedor, string valCodigoClienteProveedor, eMonedaDelInformeMM valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			/* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
			string vSqlCambioDelDia;
			string vSqlCambioMasCercano;
			string vSqlCambioOriginal = "Anticipo.Cambio";
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
			string vSqlCambio = vSqlCambioOriginal;
			string vSqlMontoAnulado = "Anticipo.MontoTotal";
			string vSqlMontoDevuelto = "Anticipo.MontoDevuelto";
			string vSqlMontoDifEnDevolucion = "Anticipo.MontoDiferenciaEnDevolucion";
			string vSqlMonedaTotales = "Anticipo.Moneda";

			if (valMonedaDelInformeMM == eMonedaDelInformeMM.EnBolivares) {
				if (valTipoTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = CxC.CodigoMoneda AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
				} else {
					vSqlCambio = vSqlCambioOriginal;
				}
				vSqlMontoAnulado = insSql.RoundToNDecimals(vSqlMontoAnulado + " * " + vSqlCambio, 2);
				vSqlMontoDevuelto = insSql.RoundToNDecimals(vSqlMontoDevuelto + " * " + vSqlCambio, 2);
				vSqlMontoDifEnDevolucion = insSql.RoundToNDecimals(vSqlMontoDifEnDevolucion + " * " + vSqlCambio, 2);
				vSqlMonedaTotales = "'Bolívares'";
			} else if (valMonedaDelInformeMM == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
				vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + insSql.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
				vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + insSql.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= CxC.Fecha ORDER BY FechaDeVigencia DESC), 1)";
				if (valTipoTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
					vSqlCambio = vSqlCambioDelDia;
				} else {
					vSqlCambio = vSqlCambioMasCercano;
				}
				vSqlCambio = insSql.IIF("Anticipo.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
				vSqlMontoAnulado = insSql.RoundToNDecimals(vSqlMontoAnulado + " / " + vSqlCambio, 2);
				vSqlMontoDevuelto = insSql.RoundToNDecimals(vSqlMontoDevuelto + " / " + vSqlCambio, 2);
				vSqlMontoDifEnDevolucion = insSql.RoundToNDecimals(vSqlMontoDifEnDevolucion + " / " + vSqlCambio, 2);
				vSqlMonedaTotales = insSql.IIF("Anticipo.CodigoMoneda = " + insSql.ToSqlValue(vCodigoMonedaLocal), "'Bolívares expresados en " + valNombreMoneda + "'", "Anticipo.Moneda", true);
			} else if (valMonedaDelInformeMM == eMonedaDelInformeMM.EnMonedaOriginal) {
			}
			/* FIN */

			vSql.AppendLine("SELECT ");
			vSql.AppendLine("Anticipo.ConsecutivoAnticipo, ");
			vSql.AppendLine("Anticipo.Moneda, ");
			vSql.AppendLine("Anticipo.CodigoCliente AS CodigoClienteProveedor, ");
			vSql.AppendLine("cliente.Nombre AS NombreClienteProveedor, ");
			vSql.AppendLine("Anticipo.Status, ");
			vSql.AppendLine(SqStatusStrAnticipos());
			vSql.AppendLine("Anticipo.Fecha, ");
			vSql.AppendLine("Anticipo.Numero, ");
			vSql.AppendLine(vSqlCambio + " AS Cambio, ");
			vSql.AppendLine(vSqlMonedaTotales + " AS MonedaReporte, ");
			vSql.AppendLine(vSqlMontoAnulado + " AS MontoAnulado, ");
			vSql.AppendLine("0 AS MontoTotal, ");
			vSql.AppendLine("0 AS MontoUsado, ");
			vSql.AppendLine(vSqlMontoDevuelto + " AS MontoDevuelto, ");
			vSql.AppendLine(vSqlMontoDifEnDevolucion + " AS MontoDiferenciaEnDevolucion, ");
			vSql.AppendLine("Anticipo.NumeroCheque ");

			vSql.Append(SqlFromAnticipoClienteProveedor(valEsCliente));
			vSql.Append(SqlWhereAnticipoClienteProveedor(valEsCliente, valConsecutivoCompania, false, eCantidadAImprimir.One, eStatusAnticipo.Anulado, valCantidadAImprimirClienteProveedor, valCodigoClienteProveedor));

			return vSql.ToString();
		}
		#endregion //Metodos Generados
	} //End of class clsAnticipoSql
} //End of namespace Galac.Adm.Brl.CAnticipo

