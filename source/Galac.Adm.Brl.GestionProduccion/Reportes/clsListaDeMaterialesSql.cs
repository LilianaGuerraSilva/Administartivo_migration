using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Lib;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Brl.GestionProduccion.Reportes {
    
    public class clsListaDeMaterialesSql {
          private QAdvSql vSqlUtil = new QAdvSql("");
        #region Metodos Generados
        public string SqlListaDeMaterialesSalida(int valConsecutivoCompania, string valCodigoListaAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir, string valMonedaDelInformeMM, decimal valTasaDeCambio, string[] valListaMoneda) {
            StringBuilder vSql = new StringBuilder();            
            string vSqlCostoUnitario;
             int vPos = LibString.IndexOf(valMonedaDelInformeMM, " ") > 0 ? LibString.IndexOf(valMonedaDelInformeMM, "expresado") - 1 : LibString.Len(valMonedaDelInformeMM);
            string vNombreMoneda = LibString.SubString(valMonedaDelInformeMM, 0, vPos);
            if(LibString.S1IsEqualToS2(valMonedaDelInformeMM, valListaMoneda[1])) { // En ME
                vSqlCostoUnitario = "ArticuloInventario.MeCostoUnitario AS CostoUnitario, ";                
            } else if(LibString.S1IsEqualToS2(valMonedaDelInformeMM, valListaMoneda[2])) { // ML expresados en ME
                vSqlCostoUnitario = vSqlUtil.RoundToNDecimals($"ArticuloInventario.CostoUnitario / {valTasaDeCambio}", 2, "CostoUnitario,");                
            } else if(LibString.S1IsEqualToS2(valMonedaDelInformeMM, valListaMoneda[3])) { // ME expresados en ML
                vSqlCostoUnitario = vSqlUtil.RoundToNDecimals($"ArticuloInventario.MeCostoUnitario * {valTasaDeCambio}", 2, "CostoUnitario,");                
            } else {    // En ML
                vSqlCostoUnitario = "ArticuloInventario.CostoUnitario, ";                
            }
            vSql.AppendLine(";WITH CTE_Insumos AS (");
            vSql.AppendLine(SqlListaDeMaterialesInsumos(valConsecutivoCompania, valCodigoListaAProducir, valCantidadAImprimir, valCantidadAProducir, valMonedaDelInformeMM, valTasaDeCambio, valListaMoneda) + ") ");
            vSql.AppendLine(",CTE_CostoTotalInsumos AS ( ");            
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("CTE_Insumos.Consecutivo, ");
            vSql.AppendLine("SUM(CostoTotal) AS SumCostoTotal ");
            vSql.AppendLine("FROM CTE_Insumos ");
            vSql.AppendLine("GROUP BY CTE_Insumos.Consecutivo) ");
            vSql.AppendLine("SELECT ");
            vSql.AppendLine(vSqlUtil.ToSqlValue(vNombreMoneda) + " AS Moneda,");
            vSql.AppendLine("ListaDeMateriales.Codigo, ");
            vSql.AppendLine("ListaDeMateriales.Codigo + ' - ' + ListaDeMateriales.Nombre AS ListaDeMateriales, ");
            vSql.AppendLine("ListaDeMaterialesDetalleSalidas.CodigoArticuloInventario AS CodigoListaSalida, ");
            vSql.AppendLine("ArticuloInventario.Descripcion AS ArticuloListaSalida, ");
            vSql.AppendLine("SUBSTRING(ArticuloInventario.UnidadDeVenta,1,10) AS Unidades, ");
            vSql.AppendLine("ListaDeMaterialesDetalleSalidas.PorcentajeDeCosto, ");
            vSql.AppendLine(vSqlCostoUnitario);
            vSql.AppendLine("ListaDeMaterialesDetalleSalidas.Cantidad As CantidadArticulos, ");
            vSql.AppendLine(vSqlUtil.ToSqlValue(valCantidadAProducir) + " AS CantidadAProducir, ");
            vSql.AppendLine(vSqlUtil.RoundToNDecimals(vSqlUtil.ToSqlValue(valCantidadAProducir) + " * ListaDeMaterialesDetalleSalidas.Cantidad", 2, "CantidadDetalleAProducir,"));
            vSql.AppendLine(vSqlUtil.RoundToNDecimals("CTE_CostoTotalInsumos.SumCostoTotal * ListaDeMaterialesDetalleSalidas.PorcentajeDeCosto / 100", 2, "CostoTotal"));
            vSql.AppendLine("FROM Adm.ListaDeMateriales ");
            vSql.AppendLine("INNER JOIN Adm.ListaDeMaterialesDetalleSalidas ON ");
            vSql.AppendLine("ListaDeMateriales.ConsecutivoCompania = ListaDeMaterialesDetalleSalidas.ConsecutivoCompania AND ");
            vSql.AppendLine("ListaDeMateriales.Consecutivo = ListaDeMaterialesDetalleSalidas.ConsecutivoListaDeMateriales ");
            vSql.AppendLine("INNER JOIN ArticuloInventario ON ");
            vSql.AppendLine("ListaDeMaterialesDetalleSalidas.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND ");
            vSql.AppendLine("ListaDeMaterialesDetalleSalidas.CodigoArticuloInventario = ArticuloInventario.Codigo ");
            vSql.AppendLine("INNER JOIN CTE_CostoTotalInsumos ON ");
            vSql.AppendLine("ListaDeMateriales.Consecutivo = CTE_CostoTotalInsumos.Consecutivo ");
            vSql.AppendLine("WHERE ListaDeMateriales.ConsecutivoCompania =  " + vSqlUtil.ToSqlValue(valConsecutivoCompania));
            if(valCantidadAImprimir == eCantidadAImprimir.One) {
                vSql.AppendLine(" AND ListaDeMateriales.Codigo = " + vSqlUtil.ToSqlValue(valCodigoListaAProducir));
            }
            vSql.AppendLine(" ORDER BY ListaDeMateriales.Codigo");
            return vSql.ToString();
        }

        public string SqlListaDeMaterialesInsumos(int valConsecutivoCompania, string valCodigoListaAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir, string valMonedaDelInformeMM, decimal valTasaDeCambio, string[] valListaMoneda) {
            StringBuilder vSql = new StringBuilder();
            string vSqlCostoTotal;
            string vSqlCostoUnitario;
            if(LibString.S1IsEqualToS2(valMonedaDelInformeMM, valListaMoneda[1])) { // En ME
                vSqlCostoUnitario = "ArticuloInventario.MeCostoUnitario AS CostoUnitario, ";
                vSqlCostoTotal = vSqlUtil.RoundToNDecimals($"{vSqlUtil.ToSqlValue(valCantidadAProducir)} * Adm.ListaDeMaterialesDetalleArticulo.Cantidad * ArticuloInventario.MeCostoUnitario", 2, "CostoTotal");
            } else if(LibString.S1IsEqualToS2(valMonedaDelInformeMM, valListaMoneda[2])) { // ML expresados en ME
                vSqlCostoUnitario = vSqlUtil.RoundToNDecimals($"ArticuloInventario.CostoUnitario / {valTasaDeCambio}", 2, "CostoUnitario,");
                vSqlCostoTotal = vSqlUtil.RoundToNDecimals($"({vSqlUtil.ToSqlValue(valCantidadAProducir)} * Adm.ListaDeMaterialesDetalleArticulo.Cantidad * ArticuloInventario.CostoUnitario) / {vSqlUtil.ToSqlValue(valTasaDeCambio)}", 2, "CostoTotal");
            } else if(LibString.S1IsEqualToS2(valMonedaDelInformeMM, valListaMoneda[3])) { // ME expresados en ML
                vSqlCostoUnitario = vSqlUtil.RoundToNDecimals($"ArticuloInventario.MeCostoUnitario * {valTasaDeCambio}", 2, "CostoUnitario,");
                vSqlCostoTotal = vSqlUtil.RoundToNDecimals($"({vSqlUtil.ToSqlValue(valCantidadAProducir)} * Adm.ListaDeMaterialesDetalleArticulo.Cantidad * ArticuloInventario.MeCostoUnitario) * {vSqlUtil.ToSqlValue(valTasaDeCambio)}", 2, "CostoTotal");
            } else {    // En ML
                vSqlCostoUnitario = "ArticuloInventario.CostoUnitario, ";
                vSqlCostoTotal = vSqlUtil.RoundToNDecimals($"{vSqlUtil.ToSqlValue(valCantidadAProducir)} * Adm.ListaDeMaterialesDetalleArticulo.Cantidad * ArticuloInventario.CostoUnitario", 2, "CostoTotal");
            }
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("ListaDeMateriales.Consecutivo,");
            vSql.AppendLine("ListaDeMateriales.CodigoArticuloInventario AS Codigo, ");
            vSql.AppendLine("ArticuloInventario.Descripcion AS ListaArticuloInsumos, ");
            vSql.AppendLine("ListaDeMaterialesDetalleArticulo.Cantidad AS CantidadInsumos, ");
            vSql.AppendLine("ArticuloInventario.CostoUnitario, ");          
            vSql.AppendLine("ArticuloInventario.Existencia, ");
            vSql.AppendLine("SUBSTRING(ArticuloInventario.UnidadDeVenta,1,10) AS Unidades, ");
            vSql.AppendLine(vSqlUtil.RoundToNDecimals($"{vSqlUtil.ToSqlValue(valCantidadAProducir)} * Adm.ListaDeMaterialesDetalleArticulo.Cantidad", 2, "CantidadAReservar,"));
            vSql.AppendLine(vSqlCostoTotal);
            vSql.AppendLine("FROM Adm.ListaDeMateriales ");
            vSql.AppendLine("INNER JOIN Adm.ListaDeMaterialesDetalleArticulo ON ");
            vSql.AppendLine("ListaDeMateriales.ConsecutivoCompania = ListaDeMaterialesDetalleArticulo.ConsecutivoCompania AND ");
            vSql.AppendLine("ListaDeMateriales.Consecutivo = ListaDeMaterialesDetalleArticulo.ConsecutivoListaDeMateriales ");
            vSql.AppendLine("INNER JOIN ArticuloInventario AS ArticuloInventario ON ");
            vSql.AppendLine("ListaDeMaterialesDetalleArticulo.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND ");
            vSql.AppendLine("ListaDeMaterialesDetalleArticulo.CodigoArticuloInventario = ArticuloInventario.Codigo ");
            vSql.AppendLine("WHERE ListaDeMateriales.ConsecutivoCompania = " + vSqlUtil.ToSqlValue(valConsecutivoCompania));
            if(valCantidadAImprimir == eCantidadAImprimir.One) {
                vSql.AppendLine(" AND ListaDeMateriales.Codigo = " + vSqlUtil.ToSqlValue(valCodigoListaAProducir));
            }                       
            return vSql.ToString();
        }
        #endregion //Metodos Generados
    } //End of class clsListaDeMaterialesSql
} //End of namespace Galac.Adm.Brl.GestionProduccion

