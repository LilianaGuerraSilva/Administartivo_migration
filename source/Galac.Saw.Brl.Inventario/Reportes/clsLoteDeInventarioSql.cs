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
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("ArticuloInventario.Codigo, ");
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
            vSQLWhere = insUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "LoteDeInventario.Existencia", "0", "AND", ">");
            if (!LibString.IsNullOrEmpty(valCodigoArticulo)) {
                vSQLWhere = insUtilSql.SqlValueWithAnd(vSQLWhere, "ArticuloInventario.Codigo", valCodigoArticulo);
            }
            if (!LibString.IsNullOrEmpty(valLineaDeProducto)) {
                vSQLWhere = insUtilSql.SqlValueWithAnd(vSQLWhere, "ArticuloInventario.LineaDeProducto", valLineaDeProducto);
            }
            vSQLWhere = insUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "LoteDeInventario.FechaDeVencimiento", insUtilSql.GetToday(), "AND", ">");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(insUtilSql.WhereSql(vSQLWhere));
            }
            vSql.AppendLine("ORDER  BY FechaDeVencimiento " + (valOrdenarFecha == eOrdenarFecha.Ascendente ? "ASC" : "DESC"));
            return vSql.ToString();
        }

        public string SqlLoteDeInventarioVencidos(int valConsecutivoCompania, string valNombreLineaDeProducto, string valCodigoArticulo, eOrdenarFecha valOrdenarFecha) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";
            vSql.AppendLine(" SELECT ArticuloInventario.Codigo AS CodigoArticulo,");
            vSql.AppendLine("ArticuloInventario.Descripcion AS DescripcionArticulo, ");
            vSql.AppendLine("ArticuloInventario.LineaDeProducto, ");
            vSql.AppendLine("LoteDeInventario.Existencia AS Existencia, ");
            vSql.AppendLine("LoteDeInventario.CodigoLote AS LoteDeInventario, ");
            vSql.AppendLine("LoteDeInventario.FechaDeVencimiento AS FechaDeVencimiento, ");
            vSql.AppendLine(insUtilSql.DateDiff("day", insUtilSql.GetToday(), "LoteDeInventario.FechaDeVencimiento") + " AS DiasVencido ");
            vSql.AppendLine("FROM Saw.LoteDeInventario INNER JOIN ArticuloInventario ON ");
            vSql.AppendLine("LoteDeInventario.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine("AND LoteDeInventario.CodigoArticulo = ArticuloInventario.Codigo");
            vSQLWhere = insUtilSql.SqlIntValueWithAnd(vSQLWhere, "LoteDeInventario.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = insUtilSql.SqlEnumValueWithAnd(vSQLWhere, "ArticuloInventario.TipoArticuloInv", (int)eTipoArticuloInv.LoteFechadeVencimiento);
            vSQLWhere = insUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "LoteDeInventario.Existencia", "0", "AND", ">");
            vSQLWhere = insUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "LoteDeInventario.FechaDeVencimiento", insUtilSql.GetToday(), "AND", "<");
            if (!LibString.IsNullOrEmpty(valNombreLineaDeProducto)) {
                vSQLWhere = insUtilSql.SqlValueWithAnd(vSQLWhere, "LoteDeInventario.LineaDeProducto", valNombreLineaDeProducto);
            }
            if (!LibString.IsNullOrEmpty(valCodigoArticulo)) {
                vSQLWhere = insUtilSql.SqlValueWithAnd(vSQLWhere, "ArticuloInventario.Codigo", valCodigoArticulo);
            }
            vSql.AppendLine(insUtilSql.WhereSql(vSQLWhere));
            vSql.AppendLine("ORDER BY LoteDeInventario.FechaDeVencimiento " + (valOrdenarFecha == eOrdenarFecha.Ascendente ? "ASC" : "DESC"));
            return vSql.ToString();
        }
        #endregion //Metodos Generados
    }
} //End of namespace Galac.Saw.Brl.Inventario

