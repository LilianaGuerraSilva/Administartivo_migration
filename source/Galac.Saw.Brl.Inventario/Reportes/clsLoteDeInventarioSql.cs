using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Brl.Inventario.Reportes {
    public class clsLoteDeInventarioSql {
        #region Metodos Generados

        QAdvSql insUtilSql = new QAdvSql("");
        public string SqlArticulosPorVencer(int valConsecutivoCompania, string valLineaDeProducto, string valCodigoArticulo, int valDiasPorVencer, eOrdenarFecha valOrdenarFecha) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";
            vSql.AppendLine("SELECT ArticuloInventario.Codigo, ");
            vSql.AppendLine("ArticuloInventario.Descripcion, ");
            vSql.AppendLine("ArticuloInventario.LineaDeProducto, ");
            vSql.AppendLine("LoteDeInventario.CodigoLote AS lote, ");
            vSql.AppendLine("LoteDeInventario.Existencia, ");
            vSql.AppendLine("LoteDeInventario.FechaDeVencimiento, ");
            vSql.AppendLine(insUtilSql.DateDiff("day", insUtilSql.GetToday(), "LoteDeInventario.FechaDeVencimiento") + " AS DiasPorVencer");
            vSql.AppendLine("FROM Saw.LoteDeInventario INNER JOIN ArticuloInventario ON ");
            vSql.AppendLine("LoteDeInventario.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine("AND LoteDeInventario.CodigoArticulo = ArticuloInventario.Codigo ");
            //
            vSQLWhere = insUtilSql.SqlIntValueWithAnd(vSQLWhere, "LoteDeInventario.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = insUtilSql.SqlEnumValueWithAnd(vSQLWhere, "ArticuloInventario.TipoArticuloInv", (int)eTipoArticuloInv.LoteFechadeVencimiento);
            vSQLWhere = insUtilSql.SqlIntValueWithOperators(vSQLWhere, insUtilSql.DateDiff("day", insUtilSql.GetToday(), "LoteDeInventario.FechaDeVencimiento"), valDiasPorVencer, "AND", "<=");
            vSQLWhere = insUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "LoteDeInventario.Existencia", "0", "AND", ">=");
            if (!LibString.IsNullOrEmpty(valCodigoArticulo)) {
                vSQLWhere = insUtilSql.SqlValueWithAnd(vSQLWhere, "ArticuloInventario.Codigo", valCodigoArticulo);
            }
            if (!LibString.IsNullOrEmpty(valLineaDeProducto)) {
                vSQLWhere = insUtilSql.SqlValueWithAnd(vSQLWhere, "ArticuloInventario.LineaDeProducto", valLineaDeProducto);
            }
            vSQLWhere = insUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "LoteDeInventario.FechaDeVencimiento", insUtilSql.GetToday(), "AND", ">=");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(insUtilSql.WhereSql(vSQLWhere));
            }
            vSql.AppendLine("ORDER  BY FechaDeVencimiento " + (valOrdenarFecha == eOrdenarFecha.Ascendente ? "ASC" : "DESC"));
            return vSql.ToString();
        }

        public string SqlLoteDeInventarioVencidos(int valConsecutivoCompania, string valNombreLineaDeProducto, string valCodigoArticulo, eOrdenarFecha valOrdenarFecha) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";
            vSql.AppendLine("SELECT ArticuloInventario.Codigo AS CodigoArticulo, ");
            vSql.AppendLine("ArticuloInventario.Descripcion AS DescripcionArticulo, ");
            vSql.AppendLine("ArticuloInventario.LineaDeProducto, ");
            vSql.AppendLine("LoteDeInventario.Existencia AS Existencia, ");
            vSql.AppendLine("LoteDeInventario.CodigoLote AS LoteDeInventario, ");
            vSql.AppendLine("LoteDeInventario.FechaDeVencimiento AS FechaDeVencimiento, ");
            vSql.AppendLine(insUtilSql.DateDiff("day", "LoteDeInventario.FechaDeVencimiento", insUtilSql.GetToday()) + " AS DiasVencidos ");
            vSql.AppendLine("FROM Saw.LoteDeInventario INNER JOIN ArticuloInventario ON ");
            vSql.AppendLine("LoteDeInventario.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine("AND LoteDeInventario.CodigoArticulo = ArticuloInventario.Codigo");
            vSQLWhere = insUtilSql.SqlIntValueWithAnd(vSQLWhere, "LoteDeInventario.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = insUtilSql.SqlEnumValueWithAnd(vSQLWhere, "ArticuloInventario.TipoArticuloInv", (int)eTipoArticuloInv.LoteFechadeVencimiento);
            vSQLWhere = insUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "LoteDeInventario.Existencia", "0", "AND", ">");
            vSQLWhere = insUtilSql.SqlIntValueWithOperators(vSQLWhere, insUtilSql.DateDiff("day", "LoteDeInventario.FechaDeVencimiento", insUtilSql.GetToday()), 1, "AND", ">=");
            vSQLWhere = insUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "LoteDeInventario.FechaDeVencimiento", insUtilSql.GetToday(), "AND", "<");
            if (!LibString.IsNullOrEmpty(valNombreLineaDeProducto)) {
                vSQLWhere = insUtilSql.SqlValueWithAnd(vSQLWhere, "ArticuloInventario.LineaDeProducto", valNombreLineaDeProducto);
            }
            if (!LibString.IsNullOrEmpty(valCodigoArticulo)) {
                vSQLWhere = insUtilSql.SqlValueWithAnd(vSQLWhere, "ArticuloInventario.Codigo", valCodigoArticulo);
            }
            vSql.AppendLine(insUtilSql.WhereSql(vSQLWhere));
            vSql.AppendLine("ORDER BY LoteDeInventario.FechaDeVencimiento " + (valOrdenarFecha == eOrdenarFecha.Ascendente ? "ASC" : "DESC"));
            return vSql.ToString();
        }

        private string SqlWhereMovimientoDeInventario(int valConsecutivoCompania, string valLoteDeInventario, string valCodigoArticulo, DateTime valFechaInicial, DateTime valFechaFinal, eTipodeOperacion valTipodeOperacion, bool valSqlMovFacturas) {
            string vSQLWhere = "";
            vSQLWhere = insUtilSql.SqlIntValueWithAnd(vSQLWhere, "LoteDeInventarioMovimiento.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = insUtilSql.SqlEnumValueWithAnd(vSQLWhere, "LoteDeInventarioMovimiento.TipoOperacion", (int)valTipodeOperacion);
            vSQLWhere = insUtilSql.SqlEnumValueWithAnd(vSQLWhere, "LoteDeInventarioMovimiento.StatusDocumentoOrigen", (int)eStatusLoteDeInventario.Vigente);
            vSQLWhere = insUtilSql.SqlValueWithAnd(vSQLWhere, "LoteDeInventario.CodigoLote", valLoteDeInventario);
            vSQLWhere = insUtilSql.SqlValueWithAnd(vSQLWhere, "LoteDeInventario.CodigoArticulo", valCodigoArticulo);
            vSQLWhere = insUtilSql.SqlDateValueBetween(vSQLWhere, "LoteDeInventarioMovimiento.Fecha", valFechaInicial, valFechaFinal);
            if (valSqlMovFacturas) {
                vSQLWhere = insUtilSql.SqlEnumValueWithAnd(vSQLWhere, "Factura.GeneradaPorNotaEntrega", 0);
                vSQLWhere = insUtilSql.SqlEnumValueWithAnd(vSQLWhere, "LoteDeInventarioMovimiento.Modulo", (int)eOrigenLoteInv.Factura);
            } else {
                vSQLWhere = insUtilSql.SqlEnumValueWithOperators(vSQLWhere, "LoteDeInventarioMovimiento.Modulo", (int)eOrigenLoteInv.Factura, "AND", "<>");
            }
            vSQLWhere = insUtilSql.WhereSql(vSQLWhere);
            return vSQLWhere;
        }

        private string SqlSelectMovimientoDeLoteInventario(string valSqlWhere, bool valSqlMovFacturas, eTipodeOperacion valTipoOperacion) {
            string vTipoMovimiento = $"CASE WHEN Modulo={insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.Factura)} THEN 'Factura' WHEN Modulo={insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeCredito)} THEN 'Nota de Crédito' WHEN Modulo={insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeDebito)} THEN 'Nota de Débito' WHEN Modulo={insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeEntrega)} THEN 'Nota de Entrega' WHEN Modulo={insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.Produccion)} THEN 'Producción' WHEN Modulo={insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.NotaEntradaSalida)} THEN 'Nota de Entrada/Salida' WHEN Modulo={insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.Compra)} THEN 'Compra' WHEN Modulo={insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.ConteoFisico)} THEN 'Conteo Físico' END AS TipoMovimiento, ";
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT LoteDeInventarioMovimiento.Fecha AS FechaMovimiento,");
            if (valTipoOperacion == eTipodeOperacion.EntradadeInventario) {
                vSql.AppendLine("Cantidad AS Entrada,");
                vSql.AppendLine("0 AS Salida,");

            } else if (valTipoOperacion == eTipodeOperacion.SalidadeInventario) {
                vSql.AppendLine("0 AS Entrada,");
                vSql.AppendLine("Cantidad AS Salida,");
            }
            vSql.AppendLine("NumeroDocumentoOrigen AS NroDocumento,");
            vSql.AppendLine(vTipoMovimiento);
            vSql.AppendLine("LoteDeInventario.CodigoLote AS Lote,");
            vSql.AppendLine("LoteDeInventario.CodigoArticulo,");
            vSql.AppendLine("ArticuloInventario.Descripcion AS Articulo,");
            vSql.AppendLine("LoteDeInventario.FechaDeElaboracion,");
            vSql.AppendLine("LoteDeInventario.FechaDeVencimiento");
            vSql.AppendLine("FROM Saw.LoteDeInventarioMovimiento");
            vSql.AppendLine("LEFT JOIN Saw.LoteDeInventario ON");
            vSql.AppendLine("LoteDeInventarioMovimiento.ConsecutivoLote = LoteDeInventario.Consecutivo AND");
            vSql.AppendLine("LoteDeInventarioMovimiento.ConsecutivoCompania = LoteDeInventario.ConsecutivoCompania");
            vSql.AppendLine("LEFT JOIN ArticuloInventario ON");
            vSql.AppendLine("LoteDeInventario.CodigoArticulo = ArticuloInventario.Codigo AND");
            vSql.AppendLine("LoteDeInventario.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania");
            if (valSqlMovFacturas) {
                vSql.AppendLine("LEFT JOIN Factura ON ");
                vSql.AppendLine("LoteDeInventarioMovimiento.NumeroDocumentoOrigen = Factura.Numero AND ");
                vSql.AppendLine("LoteDeInventarioMovimiento.ConsecutivoCompania = Factura.ConsecutivoCompania ");
            }
            vSql.AppendLine(valSqlWhere);
            return vSql.ToString();
        }

        public string SqlMovimientoDeLoteInventario(int valConsecutivoCompania, string valLoteDeInventario, string valCodigoArticulo, DateTime valFechaInicial, DateTime valFechaFinal) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = SqlWhereMovimientoDeInventario(valConsecutivoCompania, valLoteDeInventario, valCodigoArticulo, valFechaInicial, valFechaFinal, eTipodeOperacion.EntradadeInventario, false);
            vSql.AppendLine(SqlSelectMovimientoDeLoteInventario(vSQLWhere, false, eTipodeOperacion.EntradadeInventario));
            vSql.AppendLine("UNION");
            vSQLWhere = SqlWhereMovimientoDeInventario(valConsecutivoCompania, valLoteDeInventario, valCodigoArticulo, valFechaInicial, valFechaFinal, eTipodeOperacion.SalidadeInventario, false);
            vSql.AppendLine(SqlSelectMovimientoDeLoteInventario(vSQLWhere, false, eTipodeOperacion.SalidadeInventario));
            vSql.AppendLine("UNION");
            vSQLWhere = SqlWhereMovimientoDeInventario(valConsecutivoCompania, valLoteDeInventario, valCodigoArticulo, valFechaInicial, valFechaFinal, eTipodeOperacion.SalidadeInventario, true);
            vSql.AppendLine(SqlSelectMovimientoDeLoteInventario(vSQLWhere, true, eTipodeOperacion.SalidadeInventario));
            vSql.AppendLine("ORDER BY LoteDeInventarioMovimiento.Fecha ASC");
            string vPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\InvMov.Sql";
            LibFile.WriteLineInFile(vPath, vSql.ToString(), false);
            return vSql.ToString();
        }
        #endregion //Metodos Generados
    }
} //End of namespace Galac.Saw.Brl.Inventario

