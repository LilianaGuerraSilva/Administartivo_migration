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
		private QAdvSql vSqlUtil = new QAdvSql("");
		private Saw.Lib.clsLibSaw _LibSaw = new Saw.Lib.clsLibSaw();

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
					vSqlCambio = vSqlUtil.IIF("CxC.CambioABolivares IS NULL OR CxC.CambioABolivares = 0", "1", "CxC.CambioABolivares", true);
				}
			} else {
				vSqlCambio = "1";
			}

			bool vUsaModuloContabilidad = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaModuloDeContabiliad"));

			if (vUsaModuloContabilidad) {
				vSql.AppendLine(CteComprobantesSql(valConsecutivoCompania, valFechaDesde, valFechaHasta));
			}
			vSql.AppendLine("SELECT Cliente.Codigo,");
			vSql.AppendLine("Cliente.Nombre,");
			vSql.AppendLine("Cliente.Contacto,");
			vSql.AppendLine("CxC.Fecha,");
			vSql.AppendLine("CxC.Numero,");
			vSql.AppendLine((vIsInMonedaLocal ? vSqlUtil.ToSqlValue(vMonedaLocal) : "CxC.Moneda" ) + " AS Moneda,");
			vSql.AppendLine(SqlCampoStatusStrCxCPendientesEntreFechas());

			string vSqlCampoCxCMontoOriginal = vSqlUtil.IIF("CxC.Status = " + vSqlUtil.EnumToSqlValue((int) eStatusCXC.ANULADO), "0", "(CxC.MontoExento + CxC.MontoGravado + CxC.MontoIVA) - CxC.MontoAbonado", true);
			vSql.AppendLine(vSqlCampoCxCMontoOriginal + " * " + vSqlCambio + " AS Monto");

			if (vUsaModuloContabilidad) {
				vSql.AppendLine("," + vSqlUtil.IIF("CTE_Comprobante.NumeroComprobante <>" + vSqlUtil.ToSqlValue(""), "CTE_Comprobante.NumeroComprobante", vSqlUtil.ToSqlValue("No Aplica"), true) + "AS NumeroComprobante");
			}

			vSql.AppendLine("FROM");
			vSql.AppendLine("Cliente");
			vSql.AppendLine("INNER JOIN CxC ON (Cliente.Codigo = CxC.CodigoCliente AND Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania)");

			if (vUsaModuloContabilidad) {
				vSql.Append(SqlUsaContabilidad());
			}

			vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "Cliente.ConsecutivoCompania", valConsecutivoCompania);
			vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "CxC.Fecha", valFechaDesde, valFechaHasta);

			string vSqlCampoCxCStatusParaWhere = "";
			vSqlCampoCxCStatusParaWhere = vSqlUtil.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", (int) eStatusCXC.PORCANCELAR, "", "=");
			vSqlCampoCxCStatusParaWhere = vSqlUtil.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", (int) eStatusCXC.ABONADO, "OR", "=");

			vSQLWhere = vSQLWhere + "AND (" + vSqlCampoCxCStatusParaWhere + ")";

			vSQLWhere = vSQLWhere + "ORDER BY CxC.Status, Moneda, CxC.Fecha, CxC.Numero";
			vSQLWhere = vSqlUtil.WhereSql(vSQLWhere);

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
					vSqlCambio = vSqlUtil.IIF("CxC.CambioABolivares IS NULL OR CxC.CambioABolivares = 0", "1", "CxC.CambioABolivares", true);
				}
			}
			else {
				vSqlCambio = "1";
			}

			vSql.AppendLine("SELECT");
			vSql.AppendLine(SqlCampoEstatusCxCPorCliente());
			vSql.AppendLine("Cliente.Contacto,");
			vSql.AppendLine("CxC.Numero,");
			vSql.AppendLine("CxC.Descripcion,");
			vSql.AppendLine("CxC.FechaVencimiento,");
			vSql.AppendLine("Cliente.Codigo,");

			string vSqlCampoCxCMontoOriginal = vSqlUtil.IIF("CxC.Status = " + vSqlUtil.EnumToSqlValue((int) eStatusCXC.ANULADO), "0", "(CxC.MontoExento + CxC.MontoGravado + CxC.MontoIVA) - CxC.MontoAbonado", true);
			vSql.AppendLine(vSqlCampoCxCMontoOriginal + " * " + vSqlCambio + " AS Monto,");

			vSql.AppendLine("Cliente.Nombre AS NombreCliente,");
			vSql.AppendLine("Cliente.Telefono,");
			vSql.AppendLine("Cliente.Direccion,");
			vSql.AppendLine("Cliente.ZonaDeCobranza AS ZonaCobranza,");
			vSql.AppendLine((vIsInMonedaLocal ? vSqlUtil.ToSqlValue(vMonedaLocal) : "CxC.Moneda") + " AS Moneda,");
			vSql.AppendLine("CxC.NumeroComprobanteFiscal AS NumFiscal");
			vSql.AppendLine("FROM");
			vSql.AppendLine("Cliente");
			vSql.AppendLine("INNER JOIN CxC ON (Cliente.Codigo = CxC.CodigoCliente AND Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania)");
			vSql.AppendLine("RIGHT JOIN Moneda ON CxC.CodigoMoneda = Moneda.Codigo");

			vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "Cliente.ConsecutivoCompania", valConsecutivoCompania);
			vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "CxC.Fecha", valFechaDesde, valFechaHasta);

			string vSqlCampoCxCStatusParaWhere = "";
			vSqlCampoCxCStatusParaWhere = vSqlUtil.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", ( int ) eStatusCXC.PORCANCELAR, "", "=");
			vSqlCampoCxCStatusParaWhere = vSqlUtil.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", ( int ) eStatusCXC.CANCELADO, "OR", "=");
			vSqlCampoCxCStatusParaWhere = vSqlUtil.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", ( int ) eStatusCXC.ABONADO, "OR", "=");
			vSqlCampoCxCStatusParaWhere = vSqlUtil.SqlEnumValueWithOperators(vSqlCampoCxCStatusParaWhere, "CxC.Status", ( int ) eStatusCXC.ANULADO, "OR", "=");

			vSQLWhere = vSQLWhere + "AND (" + vSqlCampoCxCStatusParaWhere + ")";

			if (!LibString.IsNullOrEmpty(valCodigoDelCliente)) {
				vSQLWhere = vSqlUtil.SqlValueWithAnd(vSQLWhere, "Cliente.Codigo", valCodigoDelCliente);
			}

			if (!LibString.IsNullOrEmpty(valZonaCobranza)) {
				vSQLWhere = vSqlUtil.SqlValueWithAnd(vSQLWhere, "Cliente.ZonaDeCobranza", valZonaCobranza);
			}

			string vSqlClientesOrdenadosPor = "";
			if (valClientesOrdenadosPor == eClientesOrdenadosPor.PorCodigo) {
				vSqlClientesOrdenadosPor = "Cliente.Codigo";
			}
			if (valClientesOrdenadosPor == eClientesOrdenadosPor.PorNombre) {
				vSqlClientesOrdenadosPor = "Cliente.Nombre";
			}

			vSQLWhere = vSQLWhere + "ORDER BY Moneda, Cliente.ZonaDeCobranza, " + vSqlClientesOrdenadosPor + ", CxC.FechaVencimiento";
			vSQLWhere = vSqlUtil.WhereSql(vSQLWhere);

			vSql.AppendLine(vSQLWhere);

			return vSql.ToString();
		}
		#endregion //Metodos Generados

		#region Código Programador
		private string SqlCampoStatusStrCxCPendientesEntreFechas() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("(CASE");
			vSql.AppendLine("WHEN CxC.Status = " + vSqlUtil.EnumToSqlValue(( int ) eStatusCXC.PORCANCELAR) + " THEN " + vSqlUtil.ToSqlValue(eStatusCXC.PORCANCELAR.GetDescription(0)));
			vSql.AppendLine("WHEN CxC.Status = " + vSqlUtil.EnumToSqlValue(( int ) eStatusCXC.ABONADO) + " THEN " + vSqlUtil.ToSqlValue(eStatusCXC.ABONADO.GetDescription(0)) + " END");
			vSql.AppendLine(") AS StatusStr,");
			return vSql.ToString();
		}

		private string SqlCampoEstatusCxCPorCliente() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("(CASE");
			vSql.AppendLine("WHEN CxC.Status = " + vSqlUtil.EnumToSqlValue(( int ) eStatusCXC.PORCANCELAR) + " THEN " + vSqlUtil.ToSqlValue(eStatusCXC.PORCANCELAR.GetDescription(1)));
			vSql.AppendLine("WHEN CxC.Status = " + vSqlUtil.EnumToSqlValue(( int ) eStatusCXC.CANCELADO) + " THEN " + vSqlUtil.ToSqlValue(eStatusCXC.CANCELADO.GetDescription(1)));
			vSql.AppendLine("WHEN CxC.Status = " + vSqlUtil.EnumToSqlValue(( int ) eStatusCXC.ABONADO) + " THEN " + vSqlUtil.ToSqlValue(eStatusCXC.ABONADO.GetDescription(1)));
			vSql.AppendLine("WHEN CxC.Status = " + vSqlUtil.EnumToSqlValue(( int ) eStatusCXC.ANULADO) + " THEN " + vSqlUtil.ToSqlValue(eStatusCXC.ANULADO.GetDescription(1)) + " END");
			vSql.AppendLine(") AS Estatus,");
			return vSql.ToString();
		}

		private string SqlUsaContabilidad() {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("LEFT JOIN CTE_Comprobante ON");
			vSql.AppendLine("(CxC.TipoCxc + " + vSqlUtil.Char(9) + " + CxC.Numero) = CTE_Comprobante.NoDocumentoOrigen AND");
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
			vSqlWhere = vSqlUtil.SqlDateValueBetween(vSqlWhere, "Comprobante.Fecha", valFechaDesde, valFechaHasta);
			vSqlWhere = vSqlUtil.SqlIntValueWithAnd(vSqlWhere, "Periodo.ConsecutivoCompania", valConsecutivoCompania);
			vSqlWhere = vSqlUtil.WhereSql(vSqlWhere);
			vSql.Append(vSqlWhere + ")");
			return vSql.ToString();
		}
		#endregion //Código Programador

	} //End of class clsCxCSql

} //End of namespace Galac.Adm.Brl.Venta

