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
            vSql.AppendLine("LoteDeInventario.Existencia, ");
            vSql.AppendLine("LoteDeInventario.FechaDeVencimiento, ");
            vSql.AppendLine(insSqlUtil.DateDiff("day", insSqlUtil.GetToday(), "LoteDeInventario.FechaDeVencimiento") + " AS DiasPorVencer");
            vSql.AppendLine("FROM Saw.LoteDeInventario INNER JOIN ArticuloInventario ON ");
            vSql.AppendLine("LoteDeInventario.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine("AND LoteDeInventario.CodigoArticulo = ArticuloInventario.Codigo ");
            //
            vSQLWhere = insSqlUtil.SqlIntValueWithAnd(vSQLWhere, "LoteDeInventario.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = insSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "ArticuloInventario.TipoArticuloInv", (int)eTipoArticuloInv.LoteFechadeVencimiento);
            vSQLWhere = insSqlUtil.SqlIntValueWithOperators(vSQLWhere, "LoteDeInventario.Existencia", 0, "AND", ">");
            vSQLWhere = insSqlUtil.SqlIntValueWithOperators(vSQLWhere, insSqlUtil.DateDiff("day", insSqlUtil.GetToday(), "LoteDeInventario.FechaDeVencimiento"), valDiasPorVencer, "AND", "<=");
            if (!LibString.IsNullOrEmpty(valCodigoArticulo)) {
                vSQLWhere = insSqlUtil.SqlValueWithAnd(vSQLWhere, "ArticuloInventario.Codigo",valCodigoArticulo);
            }
            if (!LibString.IsNullOrEmpty(valLineaDeProducto)) {
                vSQLWhere = insSqlUtil.SqlValueWithAnd(vSQLWhere, "ArticuloInventario.LineaDeProducto", valLineaDeProducto);
            }
            vSQLWhere += " AND LoteDeInventario.FechaDeVencimiento > " + insSqlUtil.GetToday();
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSQLWhere);
            }
            vSql.AppendLine("ORDER BY" + (valOrdenarFecha == eOrdenarFecha.Ascendente ? "ASC" : "DESC"));
            return vSql.ToString();
        }
		
		public string SqlLoteDeInventarioVencidos(int valConsecutivoCompania, string valNombreLineaDeProducto, string valCodigoArticulo, eOrdenarFecha valOrdenarFecha) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            string vSQLWhere = "";
            vSql.AppendLine(" SELECT Art.Codigo AS CodigoArticulo, Art.Descripcion AS DescripcionArticulo, Art.LineaDeProducto, LI.Existencia AS Existencia, LI.CodigoLote AS LoteDeInventario, LI.FechaDeVencimiento AS FechaDeVencimiento,");
            vSql.AppendLine(insSqlUtil.DateDiff("day", "LI.FechaDeVencimiento", insSql.ToSqlValue(LibDate.Today())) + " AS DiasVencido");
            vSql.AppendLine(" FROM SAW.LoteDeInventario AS LI INNER JOIN ArticuloInventario AS Art ON");
            vSql.AppendLine(" LI.ConsecutivoCompania = Art.ConsecutivoCompania AND LI.CodigoArticulo = Art.Codigo");
            vSQLWhere = insSqlUtil.SqlIntValueWithAnd(vSQLWhere, "LI.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = insSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "Art.TipoArticuloInv", (int)eTipoArticuloInv.LoteFechadeVencimiento);
            vSQLWhere = insSqlUtil.SqlIntValueWithOperators(vSQLWhere, "LI.Existencia", 0, "AND", ">");
            vSQLWhere = vSQLWhere + " AND LI.FechaDeVencimiento < " + insSql.ToSqlValue(LibDate.Today());
            if (!LibString.IsNullOrEmpty(valNombreLineaDeProducto)) {
                vSQLWhere = vSQLWhere + " AND Art.LineaDeProducto = " + insSql.ToSqlValue(valNombreLineaDeProducto);
            }
            if (!LibString.IsNullOrEmpty(valCodigoArticulo)) {
                vSQLWhere = vSQLWhere + " AND Art.Codigo = " + insSql.ToSqlValue(valCodigoArticulo);
            }
            vSql.AppendLine(" WHERE " + vSQLWhere);
			vSql.AppendLine("ORDER BY LI.FechaDeVencimiento " + (valOrdenarFecha == eOrdenarFecha.Ascendente ? "ASC" : "DESC"));
			return vSql.ToString();
		}
        #endregion //Metodos Generados
    }
} //End of namespace Galac.Saw.Brl.Inventario

