using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Brl.Venta.Reportes {
	public class clsCxCSql {
		const string NewLine = "\r\n";

		QAdvSql vSqlUtil = new QAdvSql("");

		#region Metodos Generados
		public string SqlCxCPendientesEntreFechas(int valConsecutivoCompania, Saw.Lib.eMonedaParaImpresion valMonedaReporte, DateTime valFechaDesde, DateTime valFechaHasta, bool valUsaCantacto) {

			Saw.Lib.clsLibSaw _LibSaw = new Saw.Lib.clsLibSaw();

			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			string vMonedaLocal;
			bool vIsInMonedaLocal;
			string vSqlCambio;
			string vSqlMonedaDeCobro;
			string vSqlCxCCodigoMoneda;
			string vSqlTotalCobrado = "";
			string vCodigoMoneda;
			string vSqlFechaDeLaCxC;

			Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Comun.Brl.TablasGen.clsMonedaLocalActual();

			vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
			vMonedaLocal = vMonedaLocalActual.NombreMoneda(LibDate.Today());
			vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocal, valMonedaReporte.GetDescription());
			vCodigoMoneda = vSqlUtil.ToSqlValue(vMonedaLocalActual.CodigoMoneda(LibDate.Today()));

			vSqlFechaDeLaCxC = "CxC.Fecha";
			vSqlMonedaDeCobro = "CxC.Moneda";
			vSqlCxCCodigoMoneda = "CxC.CodigoMoneda";

			bool vUsaModuloContabilidad = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaModuloDeContabiliad"));

			if (vIsInMonedaLocal) {
				vSqlCambio = new Saw.Lib.clsLibSaw().CampoMontoPorTasaDeCambioSql("CxC.CambioAbolivares", vSqlMonedaDeCobro, "1", false, "");

				vSqlTotalCobrado = vSqlUtil.IIF(vSqlCxCCodigoMoneda + " = " + vCodigoMoneda,
								   vMonedaLocalActual.SqlConvierteMontoSiAplica(vCodigoMoneda, vSqlTotalCobrado, vSqlFechaDeLaCxC),
									_LibSaw.CampoMontoPorTasaDeCambioSql(vSqlCambio, vSqlMonedaDeCobro, vSqlTotalCobrado, vIsInMonedaLocal, ""), true);
				vSqlMonedaDeCobro = vSqlUtil.ToSqlValue(vMonedaLocal);
			} else {
				vSqlMonedaDeCobro = "CxC.moneda";
				vSqlCambio = vSqlUtil.IIF("CxC.CambioABolivares IS NULL OR CxC.CambioABolivares = 0", "1", "CxC.CambioABolivares", true);
			}

			vSql.AppendLine("SET DATEFORMAT dmy");
			if (vUsaModuloContabilidad) {
				vSql.AppendLine(CteComprobantesSql(valConsecutivoCompania, valFechaDesde, valFechaHasta));
			}
			vSql.AppendLine("SELECT Cliente.Codigo,");
			vSql.AppendLine("Cliente.Nombre,");
			vSql.AppendLine("Cliente.Contacto,");
			vSql.AppendLine("CxC.Fecha,");
			vSql.AppendLine("CxC.Numero,");
			vSql.AppendLine("CxC.Moneda,");
			vSql.AppendLine("CxC.CambioABolivares,");
			vSql.AppendLine(SqlCampoCxCStatusStrDesdeEnum());
			vSql.AppendLine(vSqlCambio + " AS CambioAMonedaLocal,");

			string vSqlCampoCxCMontoOriginal = vSqlUtil.IIF("CxC.Status = " + vSqlUtil.EnumToSqlValue((int) eStatusCXC.ANULADO), "0", "(CxC.MontoExento + CxC.MontoGravado + CxC.MontoIVA) - CxC.MontoAbonado", true);
			vSql.AppendLine(vSqlCampoCxCMontoOriginal + " AS Monto,");
			vSql.AppendLine(vSqlCampoCxCMontoOriginal + " * (" + vSqlUtil.IIF("CxC.CodigoMoneda = " + vSqlUtil.ToSqlValue(vCodigoMoneda), "1", vSqlCambio, true) + ") AS MontoEnMonedaLocal,");

			vSql.AppendLine("CxC.Moneda AS MonedaParaGrupo");
			if (vUsaModuloContabilidad) {
				vSql.AppendLine("," + vSqlUtil.IIF("CTE_Comprobante.NumeroComprobante <>" + vSqlUtil.ToSqlValue(""), "CTE_Comprobante.NumeroComprobante", vSqlUtil.ToSqlValue("No Aplica"), true) + " AS NumeroComprobante ");
			}
			vSql.AppendLine("FROM");
			vSql.AppendLine("Cliente");
			vSql.AppendLine("INNER JOIN CxC ON Cliente.Codigo = CxC.CodigoCliente");
			vSql.AppendLine("AND Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania");

			if (vUsaModuloContabilidad) {
				vSql.Append(SqlUsaContabilidad());
			}

			vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "Cliente.ConsecutivoCompania", valConsecutivoCompania) + Environment.NewLine;
			vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "CxC.Fecha", valFechaDesde, valFechaHasta) + Environment.NewLine;

			string vSqlCampoCxCStatusParaWhere = "";
			vSqlCampoCxCStatusParaWhere = vSqlUtil.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", (int) eStatusCXC.PORCANCELAR, "", "=") + Environment.NewLine;
			vSqlCampoCxCStatusParaWhere = vSqlUtil.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", (int) eStatusCXC.ABONADO, "OR", "=") + Environment.NewLine;

			vSQLWhere = vSQLWhere + "AND (" + vSqlCampoCxCStatusParaWhere + ")" + Environment.NewLine;
			vSQLWhere = vSQLWhere + "ORDER BY CxC.Status, MonedaParaGrupo, CxC.Fecha, CxC.Numero";
			vSQLWhere = vSqlUtil.WhereSql(vSQLWhere);

			vSql.AppendLine(vSQLWhere);
			return vSql.ToString();
		}
		#endregion //Metodos Generados

		private string SqlCampoCxCStatusStrDesdeEnum() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("(CASE ");
			vSql.AppendLine("WHEN CxC.Status = " + vSqlUtil.EnumToSqlValue((int) eStatusCXC.PORCANCELAR) + " THEN 'Por Cancelar' ");
			vSql.AppendLine("WHEN CxC.Status = " + vSqlUtil.EnumToSqlValue((int) eStatusCXC.CANCELADO) + " THEN 'Cancelado' ");
			vSql.AppendLine("WHEN CxC.Status = " + vSqlUtil.EnumToSqlValue((int) eStatusCXC.CHEQUEDEVUELTO) + " THEN 'Cheque Devuelto' ");
			vSql.AppendLine("WHEN CxC.Status = " + vSqlUtil.EnumToSqlValue((int) eStatusCXC.ABONADO) + " THEN 'Abonado' ");
			vSql.AppendLine("WHEN CxC.Status = " + vSqlUtil.EnumToSqlValue((int) eStatusCXC.ANULADO) + " THEN 'Anulado' ");
			vSql.AppendLine("WHEN CxC.Status = " + vSqlUtil.EnumToSqlValue((int) eStatusCXC.REFINANCIADO) + " THEN 'Refinanciado' END");
			vSql.AppendLine(") AS StatusStr,");
			return vSql.ToString();
		}

		private string SqlUsaContabilidad() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("LEFT JOIN CTE_Comprobante ON");
			vSql.AppendLine("(CxC.TipoCxc + " + vSqlUtil.Char(9) + " + CxC.Numero) = CTE_Comprobante.NoDocumentoOrigen AND");
			vSql.AppendLine("CxC.ConsecutivoCompania = CTE_Comprobante.ConsecutivoCompania AND ");
			vSql.AppendLine("CxC.Fecha = CTE_Comprobante.FechaComprobante ");
			return vSql.ToString();
		}

		private string CteComprobantesSql(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta) {
			string vSqlWhere = "";
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine(";WITH CTE_Comprobante(ConsecutivoCompania,NumeroComprobante,NoDocumentoOrigen,FechaComprobante) ");
			vSql.AppendLine("AS ");
			vSql.AppendLine("(SELECT ");
			vSql.AppendLine("Periodo.ConsecutivoCompania,");
			vSql.AppendLine("Numero,");
			vSql.AppendLine("NoDocumentoOrigen,");
			vSql.AppendLine("Fecha ");
			vSql.AppendLine("FROM ");
			vSql.AppendLine("COMPROBANTE ");
			vSql.AppendLine("INNER JOIN PERIODO ON ");
			vSql.AppendLine("COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo AND ");
			vSql.AppendLine("COMPROBANTE.GeneradoPor ='9' AND ");
			vSql.AppendLine("COMPROBANTE.Fecha BETWEEN PERIODO.FechaAperturaDelPeriodo AND PERIODO.FechaCierreDelPeriodo ");
			vSqlWhere = vSqlUtil.SqlDateValueBetween(vSqlWhere, "Comprobante.Fecha", valFechaDesde, valFechaHasta);
			vSqlWhere = vSqlUtil.SqlIntValueWithAnd(vSqlWhere, "Periodo.ConsecutivoCompania", valConsecutivoCompania);
			vSqlWhere = vSqlUtil.WhereSql(vSqlWhere);
			vSql.Append(vSqlWhere + ")");
			return vSql.ToString();
		}

	} //End of class clsCxCSql

} //End of namespace Galac.Adm.Brl.Venta

