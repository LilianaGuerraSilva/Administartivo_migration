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
		public string SqlAnticipoPorProveedorOCliente(int valConsecutivoCompania, eStatusAnticipoInformes valStatusAnticipo, eCantidadAImprimir valCantidadAImprimir, string valCodigoClienteProveedor, bool valOrdenamientoPorStatus, eMonedaDelInformeMM valMonedaDelInformeMM, bool valEsCliente, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
			if (valEsCliente) {
				return SqlAnticipoPorCliente(valConsecutivoCompania, valStatusAnticipo, valCantidadAImprimir, valCodigoClienteProveedor, valOrdenamientoPorStatus, valMonedaDelInformeMM, valTipoTasaDeCambio, valCodigoMoneda, valNombreMoneda);
			} else {
				return SqlAnticipoPorProveedor(valConsecutivoCompania, valStatusAnticipo, valCantidadAImprimir, valCodigoClienteProveedor, valOrdenamientoPorStatus, valMonedaDelInformeMM, valTipoTasaDeCambio, valCodigoMoneda, valNombreMoneda);
			}
		}

		private string SqlCampoEstatusAnticipos() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("(CASE Anticipo.Status ");
			vSql.AppendLine("WHEN " + insSql.EnumToSqlValue((int)eStatusAnticipoInformes.Vigente) + " THEN " + insSql.ToSqlValue(eStatusAnticipoInformes.Vigente.GetDescription()));
			vSql.AppendLine("WHEN " + insSql.EnumToSqlValue((int)eStatusAnticipoInformes.Anulado) + " THEN " + insSql.ToSqlValue(eStatusAnticipoInformes.Anulado.GetDescription()));
			vSql.AppendLine("WHEN " + insSql.EnumToSqlValue((int)eStatusAnticipoInformes.ParcialmenteUsado) + " THEN " + insSql.ToSqlValue(eStatusAnticipoInformes.ParcialmenteUsado.GetDescription()));
			vSql.AppendLine("WHEN " + insSql.EnumToSqlValue((int)eStatusAnticipoInformes.CompletamenteUsado) + " THEN " + insSql.ToSqlValue(eStatusAnticipoInformes.CompletamenteUsado.GetDescription()));
			vSql.AppendLine("WHEN " + insSql.EnumToSqlValue((int)eStatusAnticipoInformes.CompletamenteDevuelto) + " THEN " + insSql.ToSqlValue(eStatusAnticipoInformes.CompletamenteDevuelto.GetDescription()));
			vSql.AppendLine("WHEN " + insSql.EnumToSqlValue((int)eStatusAnticipoInformes.ParcialmenteDevuelto) + " THEN " + insSql.ToSqlValue(eStatusAnticipoInformes.ParcialmenteDevuelto.GetDescription()));
			vSql.AppendLine(" END) AS StatusStr,");
			return vSql.ToString();
		}

		public string SqlAnticipoPorCliente(int valConsecutivoCompania, eStatusAnticipoInformes valStatusAnticipo, eCantidadAImprimir valCantidadAImprimir, string valCodigoClienteProveedor, bool valOrdenamientoPorStatus, eMonedaDelInformeMM valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine(SqlAnticipoPorClienteNo_Devolucion(valConsecutivoCompania, valStatusAnticipo, valCantidadAImprimir, valCodigoClienteProveedor, valOrdenamientoPorStatus, valMonedaDelInformeMM, valTipoTasaDeCambio, valCodigoMoneda, valNombreMoneda));
			vSql.AppendLine("UNION");
			vSql.AppendLine(SqlAnticipoPorCliente_Devolucion(valConsecutivoCompania, valStatusAnticipo, valCantidadAImprimir, valCodigoClienteProveedor, valOrdenamientoPorStatus, valMonedaDelInformeMM, valTipoTasaDeCambio, valCodigoMoneda, valNombreMoneda));
			if (valStatusAnticipo == eStatusAnticipoInformes.Anulado || valStatusAnticipo == eStatusAnticipoInformes.Todos) {
				vSql.AppendLine("UNION");
				vSql.AppendLine(SqlAnticipoAnuladoPorCliente(valConsecutivoCompania, valStatusAnticipo, valCantidadAImprimir, valCodigoClienteProveedor, valOrdenamientoPorStatus, valMonedaDelInformeMM, valTipoTasaDeCambio, valCodigoMoneda, valNombreMoneda));
			}
			vSql = vSql.AppendLine(" ORDER BY Anticipo.Moneda, " + (valOrdenamientoPorStatus ? "Status" : "CodigoCliente") + ", Anticipo.Fecha");
			return vSql.ToString();
		}

		public string SqlAnticipoPorClienteNo_Devolucion(int valConsecutivoCompania, eStatusAnticipoInformes valStatusAnticipo, eCantidadAImprimir valCantidadAImprimir, string valCodigoClienteProveedor, bool valOrdenamientoPorStatus, eMonedaDelInformeMM valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
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

			vSql.AppendLine("SELECT Anticipo.ConsecutivoAnticipo, Anticipo.Moneda, Anticipo.CodigoCliente AS CodigoClienteProveedor, cliente.Nombre AS NombreClienteProveedor, ");
			vSql.AppendLine("Anticipo.Status, ");
			vSql.AppendLine(SqlCampoEstatusAnticipos());
			vSql.AppendLine("Anticipo.Fecha, Anticipo.Numero, ");
			vSql.AppendLine(" " + vSqlCambio + " AS Cambio, ");
			vSql.AppendLine(" " + vSqlMonedaTotales + " AS MonedaReporte, 0 AS MontoAnulado, ");
			vSql.AppendLine(" " + vSqlMontoTotal + " AS MontoTotal,");
			vSql.AppendLine(" " + vSqlMontoUsado + " AS MontoUsado,");
			vSql.AppendLine(" 0 AS MontoDevuelto, 0 AS MontoDiferenciaEnDevolucion, ");
			vSql.AppendLine(" Anticipo.NumeroCheque");

			vSql.AppendLine("FROM Anticipo INNER JOIN Cliente");
			vSql.AppendLine("ON Anticipo.ConsecutivoCompania = cliente.ConsecutivoCompania AND Anticipo.CodigoCliente = cliente.Codigo");

			vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "Anticipo.ConsecutivoCompania", valConsecutivoCompania);
			vSQLWhere = insSql.SqlBoolValueWithAnd(vSQLWhere, "Anticipo.EsUnaDevolucion ", false);
			if (valStatusAnticipo == eStatusAnticipoInformes.Todos) {
				vSQLWhere = insSql.SqlEnumValueWithAnd(vSQLWhere, "Anticipo.Status", (int)valStatusAnticipo);
			}
			vSQLWhere = insSql.SqlEnumValueWithOperators(vSQLWhere, "Anticipo.Status", (int)eStatusAnticipoInformes.Anulado, "", "<>");
			if (valCantidadAImprimir == eCantidadAImprimir.One) {
				vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Anticipo.CodigoCliente", valCodigoClienteProveedor);
			}			
			vSQLWhere = insSql.WhereSql(vSQLWhere);
			vSql.AppendLine(vSQLWhere);
			return vSql.ToString();
		}

		public string SqlAnticipoPorCliente_Devolucion(int valConsecutivoCompania, eStatusAnticipoInformes valStatusAnticipo, eCantidadAImprimir valCantidadAImprimir, string valCodigoClienteProveedor, bool valOrdenamientoPorStatus, eMonedaDelInformeMM valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";

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

			vSql.AppendLine("SELECT Anticipo.ConsecutivoAnticipo, Anticipo.Moneda, Anticipo.CodigoCliente AS CodigoClienteProveedor, cliente.Nombre AS NombreClienteProveedor, ");
			vSql.AppendLine("Anticipo.Status, ");
			vSql.AppendLine(SqlCampoEstatusAnticipos());
			vSql.AppendLine("Anticipo.Fecha, Anticipo.Numero, ");
			vSql.AppendLine(" " + vSqlCambio + " AS Cambio, ");
			vSql.AppendLine(" " + vSqlMonedaTotales + " AS MonedaReporte, 0 AS MontoAnulado, ");
			vSql.AppendLine(" " + vSqlMontoTotal + " AS MontoTotal,");
			vSql.AppendLine(" " + vSqlMontoUsado + " AS MontoUsado,");
			vSql.AppendLine(" 0 AS MontoDevuelto, 0 AS MontoDiferenciaEnDevolucion, ");
			vSql.AppendLine(" Anticipo.NumeroCheque");
			vSql.AppendLine("FROM Anticipo INNER JOIN Cliente");
			vSql.AppendLine("ON Anticipo.ConsecutivoCompania = cliente.ConsecutivoCompania AND Anticipo.CodigoCliente = cliente.Codigo");
			vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "Anticipo.ConsecutivoCompania", valConsecutivoCompania);
			vSQLWhere = insSql.SqlBoolValueWithAnd(vSQLWhere, "Anticipo.EsUnaDevolucion ", true);
			if (valStatusAnticipo != eStatusAnticipoInformes.Todos) {
				vSQLWhere = insSql.SqlEnumValueWithAnd(vSQLWhere, "Anticipo.Status", (int)valStatusAnticipo);
			}
			vSQLWhere = insSql.SqlEnumValueWithOperators(vSQLWhere, "Anticipo.Status", (int)eStatusAnticipoInformes.Anulado, "", "<>");
			if (valCantidadAImprimir == eCantidadAImprimir.One) {
				vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Anticipo.CodigoCliente", valCodigoClienteProveedor);
			}
			//vSQLWhere = vSQLWhere + " ORDER BY Anticipo.Moneda, " + (valOrdenamientoPorStatus ? "Status" : "CodigoCliente") + ", Anticipo.Fecha";
			vSQLWhere = insSql.WhereSql(vSQLWhere);
			vSql.AppendLine(vSQLWhere);
			return vSql.ToString();
		}

		public string SqlAnticipoAnuladoPorCliente(int valConsecutivoCompania, eStatusAnticipoInformes valStatusAnticipo, eCantidadAImprimir valCantidadAImprimir, string valCodigoClienteProveedor, bool valOrdenamientoPorStatus, eMonedaDelInformeMM valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
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

			vSql.AppendLine("SELECT Anticipo.ConsecutivoAnticipo, Anticipo.Moneda, Anticipo.CodigoCliente AS CodigoClienteProveedor, cliente.Nombre AS NombreClienteProveedor, ");
			vSql.AppendLine("Anticipo.Status, ");
			vSql.AppendLine(SqlCampoEstatusAnticipos());
			vSql.AppendLine("Anticipo.Fecha, Anticipo.Numero, ");
			vSql.AppendLine(" " + vSqlCambio + " AS Cambio, ");
			vSql.AppendLine(" " + vSqlMonedaTotales + " AS MonedaReporte, ");
			vSql.AppendLine(" " + vSqlMontoAnulado + " AS MontoAnulado, ");
			vSql.AppendLine(" 0 AS MontoTotal,");
			vSql.AppendLine(" 0 AS MontoUsado,");
			vSql.AppendLine(" " + vSqlMontoDevuelto + " AS MontoDevuelto, ");
			vSql.AppendLine(" " + vSqlMontoDifEnDevolucion + " AS MontoDiferenciaEnDevolucion, ");
			vSql.AppendLine(" Anticipo.NumeroCheque");
			vSql.AppendLine("FROM Anticipo INNER JOIN Cliente");
			vSql.AppendLine("ON Anticipo.ConsecutivoCompania = cliente.ConsecutivoCompania AND Anticipo.CodigoCliente = cliente.Codigo");
			vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "Anticipo.ConsecutivoCompania", valConsecutivoCompania);
			vSQLWhere = insSql.SqlBoolValueWithAnd(vSQLWhere, "Anticipo.EsUnaDevolucion ", false);
			vSQLWhere = insSql.SqlEnumValueWithAnd(vSQLWhere, "Anticipo.Status", (int)eStatusAnticipoInformes.Anulado);
			if (valCantidadAImprimir == eCantidadAImprimir.One) {
				vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Anticipo.CodigoCliente", valCodigoClienteProveedor);
			}
			//vSQLWhere = vSQLWhere + " ORDER BY Anticipo.Moneda, " + (valOrdenamientoPorStatus ? "Status" : "CodigoCliente") + ", Anticipo.Fecha";
			vSQLWhere = insSql.WhereSql(vSQLWhere);
			vSql.AppendLine(vSQLWhere);
			return vSql.ToString();
		}

		public string SqlAnticipoPorProveedor(int valConsecutivoCompania, eStatusAnticipoInformes valStatusAnticipo, eCantidadAImprimir valCantidadAImprimir, string valCodigoClienteProveedor, bool valOrdenamientoPorStatus, eMonedaDelInformeMM valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "Anticipo.ConsecutivoCompania", valConsecutivoCompania);
			return vSql.ToString();
		}
		#endregion //Metodos Generados
	} //End of class clsAnticipoSql
} //End of namespace Galac.Adm.Brl.CAnticipo

