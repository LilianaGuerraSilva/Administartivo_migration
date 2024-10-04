using System;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Brl.Inventario.Reportes {
    public class clsLoteDeInventarioSql {
        #region Metodos Generados
        QAdvSql insUtilSql = new QAdvSql("");
        #region Info Articulos Por Vencer
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
        #endregion Info Articulos Por Vencer
        #region Info Articulos Vencidos
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
        #endregion Info Articulos por Vencer
        #region Info Movimiento de Lote de Inventario
        private string SqlCte_SelectMovInvetarioExistenciaInicial(eTipodeOperacion valTipodeOperacion, bool valSqlMovFacturas) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine(insUtilSql.IIF("Cantidad > 0", "Cantidad", insUtilSql.ToSqlValue(0), true) + " AS Entrada, ");
            vSql.AppendLine(insUtilSql.IIF("Cantidad < 0", "Cantidad", insUtilSql.ToSqlValue(0), true) + " AS Salida, ");
            vSql.AppendLine("ConsecutivoLote ");
            vSql.AppendLine("FROM Saw.LoteDeInventarioMovimiento");
            vSql.AppendLine("LEFT JOIN Saw.LoteDeInventario ON");
            vSql.AppendLine("LoteDeInventarioMovimiento.ConsecutivoLote = LoteDeInventario.Consecutivo AND");
            vSql.AppendLine("LoteDeInventarioMovimiento.ConsecutivoCompania = LoteDeInventario.ConsecutivoCompania");
            vSql.AppendLine("LEFT JOIN ArticuloInventario ON");
            vSql.AppendLine("LoteDeInventario.CodigoArticulo = ArticuloInventario.Codigo AND");
            vSql.AppendLine("LoteDeInventario.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania");
            if (valSqlMovFacturas) {
                vSql.AppendLine("LEFT JOIN Factura ON");
                vSql.AppendLine("LoteDeInventarioMovimiento.NumeroDocumentoOrigen = Factura.Numero AND");
                vSql.AppendLine("LoteDeInventarioMovimiento.ConsecutivoCompania = Factura.ConsecutivoCompania");
            }
            return vSql.ToString();
        }

        private string SqlCte_SelectMovInvetarioAnulados(int valConsecutivoCompania) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(", CTE_Anulados AS (");
            vSql.AppendLine("SELECT LIM_Anulados.ConsecutivoLote,LIM_Anulados.NumeroDocumentoOrigen,LIM_Anulados.Modulo");
            vSql.AppendLine("FROM Saw.LoteDeInventarioMovimiento AS LIM_Vigentes");
            vSql.AppendLine("INNER JOIN Saw.LoteDeInventarioMovimiento AS LIM_Anulados ON");
            vSql.AppendLine("LIM_Vigentes.ConsecutivoCompania = LIM_Anulados.ConsecutivoCompania AND");
            vSql.AppendLine("LIM_Vigentes.NumeroDocumentoOrigen = LIM_Anulados.NumeroDocumentoOrigen AND");
            vSql.AppendLine("LIM_Vigentes.ConsecutivoLote = LIM_Anulados.ConsecutivoLote");
            vSql.AppendLine($"WHERE (LIM_Anulados.StatusDocumentoOrigen = {insUtilSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Anulado)}) AND (LIM_Vigentes.StatusDocumentoOrigen = {insUtilSql.EnumToSqlValue((int)eStatusDocOrigenLoteInv.Vigente)})");
            vSql.AppendLine($"AND LIM_Vigentes.ConsecutivoCompania = {insUtilSql.ToSqlValue(valConsecutivoCompania)}");
            vSql.AppendLine("GROUP BY LIM_Anulados.ConsecutivoLote,LIM_Anulados.NumeroDocumentoOrigen,LIM_Anulados.Modulo)");
            return vSql.ToString();
        }

        private string SqlCte_MovInvetarioExistenciaInicial(int valConsecutivoCompania, string valLoteDeInventario, string valCodigoArticulo, DateTime valFechaInicial, DateTime valFechaFinal) {
            string vSqlWhere = "";
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(";WITH CTE_ExistenciaInicial AS(");
            //Sql para Entradas
            vSqlWhere = SqlWhereMovimientoDeInventario(valConsecutivoCompania, valLoteDeInventario, valCodigoArticulo, valFechaInicial, valFechaFinal, eTipodeOperacion.EntradadeInventario, false, true);
            vSql.AppendLine(SqlCte_SelectMovInvetarioExistenciaInicial(eTipodeOperacion.EntradadeInventario, false));
            vSql.AppendLine(insUtilSql.WhereSql(vSqlWhere));
            vSql.AppendLine("UNION ");
            //Sql para Salidas Descartando Facturas
            vSqlWhere = SqlWhereMovimientoDeInventario(valConsecutivoCompania, valLoteDeInventario, valCodigoArticulo, valFechaInicial, valFechaFinal, eTipodeOperacion.SalidadeInventario, false, true);
            vSql.AppendLine(SqlCte_SelectMovInvetarioExistenciaInicial(eTipodeOperacion.SalidadeInventario, false));
            vSql.AppendLine(insUtilSql.WhereSql(vSqlWhere));
            vSql.AppendLine("UNION ");
            //Sql para Salidas Solo Facturas 
            vSqlWhere = SqlWhereMovimientoDeInventario(valConsecutivoCompania, valLoteDeInventario, valCodigoArticulo, valFechaInicial, valFechaFinal, eTipodeOperacion.SalidadeInventario, true, true);
            vSql.AppendLine(SqlCte_SelectMovInvetarioExistenciaInicial(eTipodeOperacion.SalidadeInventario, true));
            vSql.AppendLine(insUtilSql.WhereSql(vSqlWhere));
            vSql.AppendLine(") , CTE_ExistenciaInicialTotal AS( ");
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("SUM(Entrada) - SUM(Salida) AS ExistenciaInicial,");
            vSql.AppendLine("MIN(ConsecutivoLote) AS ConsecutivoLote ");
            vSql.AppendLine("FROM CTE_ExistenciaInicial)");
            return vSql.ToString();
        }

        private string SqlWhereMovimientoDeInventario(int valConsecutivoCompania, string valLoteDeInventario, string valCodigoArticulo, DateTime valFechaInicial, DateTime valFechaFinal, eTipodeOperacion valTipodeOperacion, bool valSqlMovFacturas, bool valParaSaldosIniciales) {
            string vSQLWhere = "";
            vSQLWhere = insUtilSql.SqlIntValueWithAnd(vSQLWhere, "LoteDeInventarioMovimiento.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = insUtilSql.SqlEnumValueWithAnd(vSQLWhere, "LoteDeInventarioMovimiento.TipoOperacion", (int)valTipodeOperacion);
            vSQLWhere = insUtilSql.SqlEnumValueWithAnd(vSQLWhere, "LoteDeInventarioMovimiento.StatusDocumentoOrigen", (int)eStatusLoteDeInventario.Vigente);
            vSQLWhere = insUtilSql.SqlValueWithAnd(vSQLWhere, "LoteDeInventario.CodigoLote", valLoteDeInventario);
            vSQLWhere = insUtilSql.SqlValueWithAnd(vSQLWhere, "LoteDeInventario.CodigoArticulo", valCodigoArticulo);
            if (valParaSaldosIniciales) {
                vSQLWhere = insUtilSql.SqlDateValueWithOperators(vSQLWhere, "LoteDeInventarioMovimiento.Fecha", valFechaInicial, insUtilSql.CurrentDateFormat, "AND", "<");
            } else {
                vSQLWhere = insUtilSql.SqlDateValueBetween(vSQLWhere, "LoteDeInventarioMovimiento.Fecha", valFechaInicial, valFechaFinal);
                vSQLWhere = vSQLWhere + " AND LoteDeInventarioMovimiento.NumeroDocumentoOrigen NOT IN(SELECT CTE_ANULADOS.NumeroDocumentoOrigen FROM CTE_ANULADOS WHERE CTE_Anulados.ConsecutivoLote = Saw.LoteDeInventario.Consecutivo AND CTE_Anulados.Modulo = LoteDeInventarioMovimiento.Modulo)";
            }
            if (valSqlMovFacturas) {
                vSQLWhere = insUtilSql.SqlEnumValueWithAnd(vSQLWhere, "Factura.GeneradaPorNotaEntrega", 0);
                vSQLWhere = insUtilSql.SqlEnumValueWithAnd(vSQLWhere, "LoteDeInventarioMovimiento.Modulo", (int)eOrigenLoteInv.Factura);
                vSQLWhere = insUtilSql.SqlEnumValueWithAnd(vSQLWhere, "Factura.StatusFactura", 0);
            } else {
                vSQLWhere = insUtilSql.SqlEnumValueWithOperators(vSQLWhere, "LoteDeInventarioMovimiento.Modulo", (int)eOrigenLoteInv.Factura, "AND", "<>");
            }
            vSQLWhere = " WHERE " + vSQLWhere;
            return vSQLWhere;
        }

        private string SqlSelectMovimientoDeLoteInventario(int valConsecutivoCompania, string valLoteDeInventario, string valCodigoArticulo, DateTime valFechaInicial, DateTime valFechaFinal, eTipodeOperacion valTipoOperacion, bool valEsParaMovFactura) {
            string vSQLWhere = SqlWhereMovimientoDeInventario(valConsecutivoCompania, valLoteDeInventario, valCodigoArticulo, valFechaInicial, valFechaFinal, valTipoOperacion, valEsParaMovFactura, false);
            string vTipoMovimiento = $"(CASE Modulo WHEN {insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.Factura)} THEN 'Factura' WHEN {insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeCredito)} THEN 'Nota de Crédito' WHEN {insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeDebito)} THEN 'Nota de Débito' WHEN {insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.NotaDeEntrega)} THEN 'Nota de Entrega' WHEN {insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.Produccion)} THEN 'Producción' WHEN {insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.NotaEntradaSalida)} THEN 'Nota de Entrada/Salida' WHEN {insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.Compra)} THEN 'Compra' WHEN {insUtilSql.EnumToSqlValue((int)eOrigenLoteInv.ConteoFisico)} THEN 'Conteo Físico' END) AS TipoMovimiento, ";
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ISNULL(CTE_ExistenciaInicialTotal.ExistenciaInicial,0) AS ExistenciaInicial, ");
            vSql.AppendLine(insUtilSql.IIF("Cantidad > 0", "Cantidad", insUtilSql.ToSqlValue(0), true) + " AS Entrada, ");
            vSql.AppendLine(insUtilSql.IIF("Cantidad < 0", "Cantidad", insUtilSql.ToSqlValue(0), true) + " AS Salida, ");
            vSql.AppendLine("NumeroDocumentoOrigen AS NroDocumento,");
            vSql.AppendLine(vTipoMovimiento);
            vSql.AppendLine("LoteDeInventario.CodigoLote AS Lote,");
            vSql.AppendLine("LoteDeInventario.CodigoArticulo,");
            vSql.AppendLine("ArticuloInventario.Descripcion AS Articulo,");
            vSql.AppendLine("LoteDeInventarioMovimiento.Fecha AS FechaMovimiento,");
            vSql.AppendLine("LoteDeInventario.FechaDeElaboracion,");
            vSql.AppendLine("LoteDeInventario.FechaDeVencimiento");
            vSql.AppendLine("FROM Saw.LoteDeInventarioMovimiento");
            vSql.AppendLine("LEFT JOIN Saw.LoteDeInventario ON");
            vSql.AppendLine("LoteDeInventarioMovimiento.ConsecutivoLote = LoteDeInventario.Consecutivo AND");
            vSql.AppendLine("LoteDeInventarioMovimiento.ConsecutivoCompania = LoteDeInventario.ConsecutivoCompania");
            vSql.AppendLine("LEFT JOIN ArticuloInventario ON");
            vSql.AppendLine("LoteDeInventario.CodigoArticulo = ArticuloInventario.Codigo AND");
            vSql.AppendLine("LoteDeInventario.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania");
            vSql.AppendLine("LEFT JOIN CTE_ExistenciaInicialTotal ON");
            vSql.AppendLine("LoteDeInventario.Consecutivo = CTE_ExistenciaInicialTotal.ConsecutivoLote");
            if (valEsParaMovFactura) {
                vSql.AppendLine("LEFT JOIN Factura ON ");
                vSql.AppendLine("LoteDeInventarioMovimiento.NumeroDocumentoOrigen = Factura.Numero AND ");
                vSql.AppendLine("LoteDeInventarioMovimiento.ConsecutivoCompania = Factura.ConsecutivoCompania ");
            }
            vSql.AppendLine(vSQLWhere);
            return vSql.ToString();
        }

        public string SqlMovimientoDeLoteInventario(int valConsecutivoCompania, string valLoteDeInventario, string valCodigoArticulo, DateTime valFechaInicial, DateTime valFechaFinal) {
            StringBuilder vSql = new StringBuilder();            
            //Sql para Saldo Inicial
            vSql.AppendLine(SqlCte_MovInvetarioExistenciaInicial(valConsecutivoCompania, valLoteDeInventario, valCodigoArticulo, valFechaInicial, valFechaFinal));
            //
            vSql.AppendLine(SqlCte_SelectMovInvetarioAnulados(valConsecutivoCompania));
            //Sql para Entradas Descartando Factura             
            vSql.AppendLine(SqlSelectMovimientoDeLoteInventario(valConsecutivoCompania, valLoteDeInventario, valCodigoArticulo, valFechaInicial, valFechaFinal, eTipodeOperacion.EntradadeInventario, false));
            vSql.AppendLine("UNION ");
            //Sql para Salidas Descartando Factura            
            vSql.AppendLine(SqlSelectMovimientoDeLoteInventario(valConsecutivoCompania, valLoteDeInventario, valCodigoArticulo, valFechaInicial, valFechaFinal, eTipodeOperacion.SalidadeInventario, false));
            vSql.AppendLine("UNION ");
            //Sql para Salidas Solo Factura            
            vSql.AppendLine(SqlSelectMovimientoDeLoteInventario(valConsecutivoCompania, valLoteDeInventario, valCodigoArticulo, valFechaInicial, valFechaFinal, eTipodeOperacion.SalidadeInventario, true));
            vSql.AppendLine("ORDER BY FechaMovimiento ASC");
            return vSql.ToString();
        }
        #endregion Info Movimiento de Lote de Inventario
        #endregion //Metodos Generados
    }
} //End of namespace Galac.Saw.Brl.Inventario

