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

        QAdvSql insSqlUtil = new QAdvSql("");
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
            vSql.AppendLine(insSqlUtil.DateDiff("day", insSqlUtil.GetToday(), "LoteDeInventario.FechaDeVencimiento") + " AS DiasPorVencer");
            vSql.AppendLine("FROM Saw.LoteDeInventario INNER JOIN ArticuloInventario ON ");
            vSql.AppendLine("LoteDeInventario.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine("AND LoteDeInventario.CodigoArticulo = ArticuloInventario.Codigo ");
            //
            vSQLWhere = insSqlUtil.SqlIntValueWithAnd(vSQLWhere, "LoteDeInventario.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = insSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "ArticuloInventario.TipoArticuloInv", (int)eTipoArticuloInv.LoteFechadeVencimiento);
            vSQLWhere = insSqlUtil.SqlIntValueWithOperators(vSQLWhere, insSqlUtil.DateDiff("day", insSqlUtil.GetToday(), "LoteDeInventario.FechaDeVencimiento"), valDiasPorVencer, "AND", "<=");
            vSQLWhere = insSqlUtil.SqlExpressionValueWithAnd(vSQLWhere, "LoteDeInventario.Existencia", "0", "AND", ">");
            if (!LibString.IsNullOrEmpty(valCodigoArticulo)) {
                vSQLWhere = insSqlUtil.SqlValueWithAnd(vSQLWhere, "ArticuloInventario.Codigo",valCodigoArticulo);
            }
            if (!LibString.IsNullOrEmpty(valLineaDeProducto)) {
                vSQLWhere = insSqlUtil.SqlValueWithAnd(vSQLWhere, "ArticuloInventario.LineaDeProducto", valLineaDeProducto);
            }
            vSQLWhere = insSqlUtil.SqlExpressionValueWithAnd(vSQLWhere, "LoteDeInventario.FechaDeVencimiento", insSqlUtil.GetToday(), "AND", ">");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(insSqlUtil.WhereSql(vSQLWhere));
            }
            vSql.AppendLine("ORDER  BY FechaDeVencimiento " + (valOrdenarFecha == eOrdenarFecha.Ascendente ? "ASC" : "DESC"));            
            return vSql.ToString();
        }
        #endregion //Metodos Generados
    }
} //End of namespace Galac.Saw.Brl.Inventario

