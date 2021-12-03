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
        private Saw.Lib.clsLibSaw _LibSaw = new Saw.Lib.clsLibSaw();

        #region Metodos Generados
        public string SqlFacturacionPorArticulo(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoDelArticulo, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio, bool valIsInformeDetallado) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";

            Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Comun.Brl.TablasGen.clsMonedaLocalActual();
            vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            string vMonedaLocal = vMonedaLocalActual.NombreMoneda(LibDate.Today());
            bool vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocal, valMonedaDelReporte.GetDescription());
            bool vIsInTasaDelDia = valTipoTasaDeCambio == Saw.Lib.eTasaDeCambioParaImpresion.DelDia;

            string vSqlCambio;
            if (vIsInMonedaLocal) {
                if (vIsInTasaDelDia) {
                    vSqlCambio = new Saw.Lib.clsLibSaw().CampoMontoPorTasaDeCambioSql("Factura.CambioAbolivares", "Factura.Moneda", "1", false, "");
                } else {
                    vSqlCambio = vSqlUtil.IIF("Factura.CambioABolivares IS NULL OR Factura.CambioABolivares = 0", "1", "Factura.CambioABolivares", true);
                }
            } else {
                vSqlCambio = "1";
            }

            bool vUsaPrecioSinIva = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaPrecioSinIva"));

            string vSqlPrecioUnitario = "RenglonFactura." + (vUsaPrecioSinIva ? "PrecioSinIVA" : "PrecioConIVA");
            string vSqlMonto = "(RenglonFactura.TotalRenglon * (1 - Factura.PorcentajeDescuento / 100)) * " + vSqlCambio;
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
        #endregion //Metodos Generados

        #region Código Programador
        private string CteArticuloInventarioSql()
        {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(";WITH CTE_ArticuloInventario(ConsecutivoCompania,Codigo,Descripcion,UnidadDeVenta,Serial,Rollo)");
            vSql.AppendLine("AS");
            vSql.AppendLine("(SELECT");
            vSql.AppendLine("ArticuloInventario.ConsecutivoCompania,");
            vSql.AppendLine("ArticuloInventario.Codigo,");
            vSql.AppendLine("ArticuloInventario.Descripcion,");
            vSql.AppendLine("ArticuloInventario.UnidadDeVenta,");
            vSql.AppendLine(vSqlUtil.IIF("ExistenciaPorGrupo.Serial IS NULL", "0", "ExistenciaPorGrupo.Serial", true) + " AS Serial,");
            vSql.AppendLine(vSqlUtil.IIF("ExistenciaPorGrupo.Rollo IS NULL", "0", "ExistenciaPorGrupo.Rollo", true) + " AS Rollo");

            vSql.AppendLine("FROM ArticuloInventario");
            vSql.AppendLine("LEFT OUTER JOIN ExistenciaPorGrupo");
            vSql.AppendLine("ON ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania");
            vSql.AppendLine("AND ArticuloInventario.CodigoGrupo = " + vSqlUtil.IIF("ExistenciaPorGrupo.CodigoGrupo = 0", vSqlUtil.ToSqlValue(""), "ExistenciaPorGrupo.CodigoGrupo", true));
            vSql.AppendLine("AND ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)");

            return vSql.ToString();
        }
        #endregion //Código Programador

    } //End of class clsFacturaSql

} //End of namespace Galac.Adm.Brl.Venta

