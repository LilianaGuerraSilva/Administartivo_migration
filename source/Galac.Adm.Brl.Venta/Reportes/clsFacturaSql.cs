using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Brl.Venta.Reportes {
    public class clsFacturaSql {
        private QAdvSql vSqlUtil = new QAdvSql("");

        #region Metodos Generados
        public string SqlFacturacionPorArticulo(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoDelArticulo, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio, bool valIsInformeDetallado) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";

            Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Comun.Brl.TablasGen.clsMonedaLocalActual();
            vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            string vMonedaLocal = vMonedaLocalActual.NombreMoneda(LibDate.Today());
            bool vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocal, valMonedaDelReporte.GetDescription());
            bool vIsInTasaDelDia = valTipoTasaDeCambio == Saw.Lib.eTasaDeCambioParaImpresion.DelDia;

            string vSqlCambio = SqlCambioParaReportesDeFactura(vIsInMonedaLocal, vIsInTasaDelDia);

            bool vUsaPrecioSinIva = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaPrecioSinIva"));

            string vSqlPrecioUnitario = vSqlCambio + " * RenglonFactura." + (vUsaPrecioSinIva ? "PrecioSinIVA" : "PrecioConIVA");
            string vSqlMonto = vSqlCambio + " * (RenglonFactura.TotalRenglon * (1 - Factura.PorcentajeDescuento / 100))";
            vSqlMonto = vSqlUtil.RoundToNDecimals(vSqlMonto, 2);

            vSql.AppendLine(CteArticuloInventarioSql());
            vSql.AppendLine("SELECT");
            vSql.AppendLine("CTE_ArticuloInventario.Codigo,");
            vSql.AppendLine("CTE_ArticuloInventario.Descripcion,");
            vSql.AppendLine("CTE_ArticuloInventario.UnidadDeVenta,");
            vSql.AppendLine((vIsInMonedaLocal ? vSqlUtil.ToSqlValue(vMonedaLocal) : "Factura.Moneda") + " AS Moneda,");
            vSql.AppendLine("Factura.Fecha,");

            if (valIsInformeDetallado) {
                vSql.AppendLine("Factura.CodigoCliente,");
                vSql.AppendLine("Cliente.Nombre,");
                vSql.AppendLine("Factura.Numero,");
                vSql.AppendLine("RenglonFactura.Cantidad,");
                vSql.AppendLine(vSqlPrecioUnitario + " AS PrecioUnitario,");
                vSql.AppendLine(vSqlMonto + " AS Monto");

                vSql.AppendLine("FROM");
                vSql.AppendLine("Cliente");
                vSql.AppendLine("INNER JOIN (");
            } else {
                vSql.AppendLine("SUM(RenglonFactura.Cantidad) AS Cantidad,");
                vSql.AppendLine("SUM(" + vSqlPrecioUnitario + ") AS PrecioUnitario,");
                vSql.AppendLine("SUM(" + vSqlMonto + ") AS Monto");

                vSql.AppendLine("FROM");
            }

            vSql.AppendLine("Factura");
            vSql.AppendLine("INNER JOIN (");
            vSql.AppendLine("CTE_ArticuloInventario");
            vSql.AppendLine("INNER JOIN RenglonFactura");
            vSql.AppendLine("ON CTE_ArticuloInventario.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania");
            vSql.AppendLine("AND CTE_ArticuloInventario.Codigo = RenglonFactura.Articulo");
            vSql.AppendLine("AND CTE_ArticuloInventario.Serial = RenglonFactura.Serial");
            vSql.AppendLine("AND CTE_ArticuloInventario.Rollo = RenglonFactura.Rollo");
            vSql.AppendLine(")");
            vSql.AppendLine("ON Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania");
            vSql.AppendLine("AND Factura.Numero = RenglonFactura.NumeroFactura");
            vSql.AppendLine("AND Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento");

            if (valIsInformeDetallado) {
                vSql.AppendLine(")");
                vSql.AppendLine("ON Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania");
                vSql.AppendLine("AND Cliente.Codigo = Factura.CodigoCliente");
            }

            vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "Factura.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "Factura.Fecha", valFechaDesde, valFechaHasta);
            vSQLWhere = vSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "Factura.StatusFactura", ( int ) Ccl.Venta.eStatusFactura.Emitida);
            vSQLWhere = vSqlUtil.SqlEnumValueWithOperators(vSQLWhere, "Factura.TipoDeDocumento", ( int ) Saw.Ccl.SttDef.eTipoDocumentoFactura.ResumenDiarioDeVentas, "AND", "<>");
            vSQLWhere = vSqlUtil.SqlEnumValueWithOperators(vSQLWhere, "Factura.TipoDeDocumento", ( int ) Saw.Ccl.SttDef.eTipoDocumentoFactura.NotaEntrega, "AND", "<>");

            if (!LibString.IsNullOrEmpty(valCodigoDelArticulo)) {
                vSQLWhere = vSqlUtil.SqlValueWithAnd(vSQLWhere, "CTE_ArticuloInventario.Codigo", valCodigoDelArticulo);
            }

            vSQLWhere = vSqlUtil.WhereSql(vSQLWhere);
            vSql.AppendLine(vSQLWhere);

            if (!valIsInformeDetallado) {
                vSql.AppendLine("GROUP BY");
                vSql.AppendLine("CTE_ArticuloInventario.Codigo,");
                vSql.AppendLine("CTE_ArticuloInventario.Descripcion,");
                vSql.AppendLine("CTE_ArticuloInventario.UnidadDeVenta,");
                if (!vIsInMonedaLocal) {
                    vSql.AppendLine("Moneda,");
                }
                vSql.AppendLine("Factura.Fecha");
            }

            vSql.AppendLine("ORDER BY");
            vSql.AppendLine("CTE_ArticuloInventario.Codigo,");
            vSql.AppendLine("Moneda,");
            vSql.AppendLine("Factura.Fecha");

            if (valIsInformeDetallado) {
                vSql.AppendLine(", Factura.TipoDeDocumento, Factura.Numero");
            }

            return vSql.ToString();
		}

        public string SqlFacturacionPorUsuario(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valNombreOperador, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio, bool valIsInformeDetallado) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";

            Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Comun.Brl.TablasGen.clsMonedaLocalActual();
            vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            string vMonedaLocal = vMonedaLocalActual.NombreMoneda(LibDate.Today());
            bool vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocal, valMonedaDelReporte.GetDescription());
            bool vIsInTasaDelDia = valTipoTasaDeCambio == Saw.Lib.eTasaDeCambioParaImpresion.DelDia;

            string vSqlCambio = SqlCambioParaReportesDeFactura(vIsInMonedaLocal, vIsInTasaDelDia);

            bool vUsaModuloContabilidad = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaModuloDeContabilidad"));
            bool vUsaPrecioSinIva = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaPrecioSinIva"));

            if (vUsaModuloContabilidad) {
                vSql.AppendLine(CteComprobantesSql(valConsecutivoCompania, valFechaDesde, valFechaHasta));
            }
            vSql.AppendLine("SELECT");
            vSql.AppendLine("Factura.NombreOperador,");
            vSql.AppendLine("Factura.Fecha,");
            vSql.AppendLine("Factura.Numero,");
            vSql.AppendLine("Factura.PorcentajeDescuento AS PorcentajeDescuentoFactura,");
            vSql.AppendLine("Factura.TipoDeDocumento,");
            vSql.AppendLine("Cliente.Nombre,");
            vSql.AppendLine(( vIsInMonedaLocal ? vSqlUtil.ToSqlValue(vMonedaLocal) : "Factura.Moneda" ) + " AS Moneda,");
            vSql.AppendLine(vSqlCambio + " As Cambio,");
            vSql.AppendLine(vSqlCambio + " * Factura.TotalFactura As TotalFactura");

            if (vUsaModuloContabilidad) {
                vSql.AppendLine("," + vSqlUtil.IIF("CTE_Comprobante.NumeroComprobante <>" + vSqlUtil.ToSqlValue(""), "CTE_Comprobante.NumeroComprobante", vSqlUtil.ToSqlValue("No Aplica"), true) + "AS NumeroComprobante");
            }

            if (valIsInformeDetallado) {
                vSql.AppendLine(", RenglonFactura.Articulo, RenglonFactura.Descripcion, RenglonFactura.PorcentajeDescuento AS PorcentajeDescuentoRenglon, RenglonFactura.Cantidad,");
                vSql.AppendLine(vSqlCambio + " * RenglonFactura." + ( vUsaPrecioSinIva ? "PrecioSinIVA" : "PrecioConIVA" ) + " As Precio,");
                vSql.AppendLine(vSqlCambio + " * RenglonFactura.TotalRenglon As TotalRenglon");
            }

            vSql.AppendLine("FROM Cliente INNER JOIN Factura ON (Cliente.Codigo = Factura.CodigoCliente AND Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania)");
            if (valIsInformeDetallado) {
                vSql.AppendLine("INNER JOIN RenglonFactura ON (Factura.Numero = RenglonFactura.NumeroFactura AND Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento AND Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)");
            }

            if (vUsaModuloContabilidad) {
                vSql.Append(SqlUsaContabilidad());
            }

            vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "Factura.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "Factura.Fecha", valFechaDesde, valFechaHasta);
            vSQLWhere = vSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "Factura.StatusFactura", ( int ) Ccl.Venta.eStatusFactura.Emitida);
            vSQLWhere = vSqlUtil.SqlEnumValueWithOperators(vSQLWhere, "Factura.TipoDeDocumento", ( int ) Saw.Ccl.SttDef.eTipoDocumentoFactura.ResumenDiarioDeVentas, "AND", "<>");
            vSQLWhere = vSqlUtil.SqlEnumValueWithOperators(vSQLWhere, "Factura.TipoDeDocumento", ( int ) Saw.Ccl.SttDef.eTipoDocumentoFactura.NotaEntrega, "AND", "<>");

            if (!LibString.IsNullOrEmpty(valNombreOperador)) {
                vSQLWhere = vSqlUtil.SqlValueWithAnd(vSQLWhere, "Factura.NombreOperador", valNombreOperador);
            }

            vSQLWhere = vSqlUtil.WhereSql(vSQLWhere);
            vSql.AppendLine(vSQLWhere);

            vSql.AppendLine("ORDER BY");
            vSql.AppendLine("Factura.NombreOperador,");
            vSql.AppendLine("Moneda,");
            vSql.AppendLine("Factura.Fecha,");
            vSql.AppendLine("Factura.TipoDeDocumento,");
            vSql.AppendLine("Factura.Numero");

            return vSql.ToString();
        }
        #endregion //Metodos Generados

        #region Código Programador
        private string SqlCambioParaReportesDeFactura(bool valIsInMonedaLocal, bool valIsInTasaDelDia) {
            string vSqlCambio;
            if (valIsInMonedaLocal) {
                if (valIsInTasaDelDia) {
                    vSqlCambio = new Saw.Lib.clsLibSaw().CampoMontoPorTasaDeCambioSql("Factura.CambioAbolivares", "Factura.Moneda", "1", false, "");
                } else {
                    vSqlCambio = vSqlUtil.IIF("Factura.CambioABolivares IS NULL OR Factura.CambioABolivares = 0", "1", "Factura.CambioABolivares", true);
                }
            } else {
                vSqlCambio = "1";
            }
            return vSqlCambio;
        }

        private string CteArticuloInventarioSql() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(";WITH CTE_ArticuloInventario(ConsecutivoCompania,Codigo,Descripcion,UnidadDeVenta,Serial,Rollo)");
            vSql.AppendLine("AS");
            vSql.AppendLine("(SELECT");
            vSql.AppendLine("ArticuloInventario.ConsecutivoCompania,");
            vSql.AppendLine("ArticuloInventario.Codigo,");
            vSql.AppendLine("ArticuloInventario.Descripcion,");
            vSql.AppendLine("ArticuloInventario.UnidadDeVenta,");
            vSql.AppendLine(vSqlUtil.IIF("ExistenciaPorGrupo.Serial IS NULL", vSqlUtil.ToSqlValue(0.ToString()), "ExistenciaPorGrupo.Serial", true) + " AS Serial,");
            vSql.AppendLine(vSqlUtil.IIF("ExistenciaPorGrupo.Rollo IS NULL", vSqlUtil.ToSqlValue(0.ToString()), "ExistenciaPorGrupo.Rollo", true) + " AS Rollo");

            vSql.AppendLine("FROM ArticuloInventario");
            vSql.AppendLine("LEFT OUTER JOIN ExistenciaPorGrupo");
            vSql.AppendLine("ON ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania");
            vSql.AppendLine("AND ArticuloInventario.CodigoGrupo = " + vSqlUtil.IIF("ExistenciaPorGrupo.CodigoGrupo = " + vSqlUtil.ToSqlValue(0.ToString()), vSqlUtil.ToSqlValue(""), "ExistenciaPorGrupo.CodigoGrupo", true));
            vSql.AppendLine("AND ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)");

            return vSql.ToString();
        }

        private string SqlUsaContabilidad() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("LEFT JOIN CTE_Comprobante ON");
            vSql.AppendLine("(Factura.TipoDeDocumento + " + vSqlUtil.Char(9) + " + Factura.Numero) = CTE_Comprobante.NoDocumentoOrigen AND");
            vSql.AppendLine("Factura.ConsecutivoCompania = CTE_Comprobante.ConsecutivoCompania AND");
            vSql.AppendLine("Factura.Fecha = CTE_Comprobante.FechaComprobante");
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
            vSql.AppendLine("COMPROBANTE.GeneradoPor = ':' AND");
            vSql.AppendLine("COMPROBANTE.Fecha BETWEEN PERIODO.FechaAperturaDelPeriodo AND PERIODO.FechaCierreDelPeriodo");
            vSqlWhere = vSqlUtil.SqlDateValueBetween(vSqlWhere, "Comprobante.Fecha", valFechaDesde, valFechaHasta);
            vSqlWhere = vSqlUtil.SqlIntValueWithAnd(vSqlWhere, "Periodo.ConsecutivoCompania", valConsecutivoCompania);
            vSqlWhere = vSqlUtil.WhereSql(vSqlWhere);
            vSql.Append(vSqlWhere + ")");
            return vSql.ToString();
        }
        #endregion //Código Programador

    } //End of class clsFacturaSql

} //End of namespace Galac.Adm.Brl.Venta

