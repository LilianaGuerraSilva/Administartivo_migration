using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;

namespace Galac.Saw.Brl.Inventario.Reportes {
    public class clsArticuloInventarioSql {
        #region Metodos Generados
        public string SqlListadoDePrecios(int valConsecutivoCompania, string valNombreLineaDeProducto, int valCantidadDecimales) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            string vFactor = insSql.ToSqlValue(Galac.Saw.Reconv.clsUtilReconv.GetFactorDeConversion());
            string vCantidadDecimales = LibConvert.ToStr(valCantidadDecimales);
            string vSQLWhere = "";
            vSql.AppendLine("SELECT Codigo");
            vSql.AppendLine("			, Descripcion");
            vSql.AppendLine("			, PrecioConIva");
            vSql.AppendLine("			, ROUND(PrecioConIva * " + vFactor + "," + vCantidadDecimales + ") AS EnBsS");
            vSql.AppendLine("			, PrecioConIva2");
            vSql.AppendLine("			, ROUND(PrecioConIva2 *  " + vFactor + "," + vCantidadDecimales + ") AS EnBsS2");
            vSql.AppendLine("			, PrecioConIva3");
            vSql.AppendLine("			, ROUND(PrecioConIva3 * " + vFactor + "," + vCantidadDecimales + ") AS EnBsS3");
            vSql.AppendLine("			, PrecioConIva4");
            vSql.AppendLine("			, ROUND(PrecioConIva4*" + vFactor + "," + vCantidadDecimales + ") AS EnBsS4");
            vSql.AppendLine("			, LineaDeProducto");
            vSql.AppendLine("			 FROM ArticuloInventario");
            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "ConsecutivoCompania", valConsecutivoCompania);
            if (LibString.Len(valNombreLineaDeProducto) > 0) {
                vSQLWhere = vSQLWhere + " AND LineaDeProducto = " + insSql.ToSqlValue(valNombreLineaDeProducto);
            }
            vSQLWhere = vSQLWhere + " AND Codigo NOT LIKE  'RD%@' AND Codigo NOT LIKE  'RD_AliExten%'";
            vSql.AppendLine(" WHERE " + vSQLWhere);
            vSql.AppendLine(" ORDER BY  LineaDeProducto, Codigo ");
            return vSql.ToString();
        }

        public string SqlListdoDeArticulosBalanza(int valConsecutivoCompania, string valLineaDeProducto, bool valFiltrarPorLineaDeProducto) {
            QAdvSql SqlUtil = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";
            vSql.AppendLine("SELECT ");
            vSql.AppendLine(" Codigo ");
            vSql.AppendLine(", Descripcion ");
            vSql.AppendLine(", Existencia ");
            vSql.AppendLine(", LineaDeProducto ");
            vSql.AppendLine(" FROM dbo.ArticuloInventario");
            vSQLWhere += SqlUtil.SqlIntValueWithAnd(vSQLWhere, "ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = SqlUtil.SqlBoolValueWithAnd(vSQLWhere, "UsaBalanza", true);
            if (valFiltrarPorLineaDeProducto) {
                vSQLWhere = SqlUtil.SqlValueWithAnd(vSQLWhere, "LineaDeProducto", valLineaDeProducto);
            }
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSQLWhere);
            }
            vSql.AppendLine(" ORDER BY  dbo.ArticuloInventario.LineaDeProducto, dbo.ArticuloInventario.Codigo ");
            return vSql.ToString();
        }

        public string SqlValoracionDeInventario(int valConsecutivoCompania, string valCodigoDesde, string valCodigoHasta, string valLineaDeProducto, decimal valCambioMoneda, bool valUsaPrecioConIva) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql SqlUtil = new QAdvSql("");
            string vSqlWhere = "";
            string vSqlCambio = "";
            string vSqlFechaUltimaCompra = "";
            string vSqlCostoUnitarioME = "";
            string vSqlPrecioDeVentaME = "";
            string vSqlPrecioDeVenta = "";

            vSqlPrecioDeVenta = valUsaPrecioConIva ? "articuloInventario.PRECIOCONIVA" : "articuloInventario.PRECIOSINIVA";
            vSqlCambio = SqlUtil.IIF("CTE_UltimaCompra.CambioABolivares IS NULL OR CTE_UltimaCompra.CambioABolivares < = 1", SqlUtil.ToSqlValue(valCambioMoneda), "CTE_UltimaCompra.CambioABolivares", true);
            vSqlFechaUltimaCompra = SqlUtil.IIF("CTE_UltimaCompra.FechaUltimaCompra IS NULL ", SqlUtil.GetToday(), "CTE_UltimaCompra.FechaUltimaCompra", true);
            vSqlCostoUnitarioME = "(articuloInventario.COSTOUNITARIO /" + vSqlCambio + ")";
            vSqlPrecioDeVentaME = "(" + vSqlPrecioDeVenta + " / " + vSqlCambio + ")";
            vSqlWhere = SqlUtil.SqlValueBetween(vSqlWhere, "articuloInventario.Codigo", valCodigoDesde, valCodigoHasta, " AND ");
            vSqlWhere = SqlUtil.SqlValueWildCardWithAnd(vSqlWhere, "articuloInventario.Codigo" + " NOT ", "RD_Ali");
            vSqlWhere = SqlUtil.SqlValueWildCardWithAnd(vSqlWhere, "articuloInventario.Codigo" + " NOT ", "RD_Co");
            if (!LibString.IsNullOrEmpty(valLineaDeProducto)) {
                vSqlWhere = SqlUtil.SqlValueWithAnd(vSqlWhere, "articuloInventario.LineaDeProducto", valLineaDeProducto);
            }
            vSqlWhere = SqlUtil.SqlIntValueWithAnd(vSqlWhere, "articuloInventario.ConsecutivoCompania", valConsecutivoCompania);
            vSqlWhere = SqlUtil.WhereSql(vSqlWhere);
            vSql.AppendLine("SET DATEFORMAT dmy");
            vSql.AppendLine(" ;WITH CTE_UltimaCompra(CodigoArticulo,FechaUltimaCompra,PrecioUltimaCompra,CodigoMoneda,CambioABolivares,ConsecutivoCompania)");
            vSql.AppendLine(" AS( SELECT Adm.CompraDetalleArticuloInventario.CodigoArticulo,");
            vSql.AppendLine(" MAX(Fecha) AS FechaUltimaCompra,");
            vSql.AppendLine(" MAX(Adm.CompraDetalleArticuloInventario.PrecioUnitario) AS PrecioUltimaCompra,");
            vSql.AppendLine(" Adm.Compra.CodigoMoneda,");
            vSql.AppendLine(" Adm.Compra.CambioABolivares,");
            vSql.AppendLine(" Adm.Compra.ConsecutivoCompania");
            vSql.AppendLine(" FROM Adm.Compra");
            vSql.AppendLine("  RIGHT JOIN");
            vSql.AppendLine(" Adm.CompraDetalleArticuloInventario");
            vSql.AppendLine(" ON Adm.Compra.Consecutivo = Adm.CompraDetalleArticuloInventario.ConsecutivoCompra AND");
            vSql.AppendLine(" Adm.Compra.ConsecutivoCompania = Adm.CompraDetalleArticuloInventario.ConsecutivoCompania");
            vSql.AppendLine(" WHERE Adm.Compra.ConsecutivoCompania = " + SqlUtil.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine(" GROUP BY");
            vSql.AppendLine(" Adm.CompraDetalleArticuloInventario.CodigoArticulo,");
            vSql.AppendLine(" Adm.Compra.CambioABolivares,");
            vSql.AppendLine(" Adm.Compra.CodigoMoneda,");
            vSql.AppendLine(" Adm.Compra.ConsecutivoCompania ");
            vSql.AppendLine(" HAVING Adm.Compra.CambioABolivares <> 0)");
            vSql.AppendLine(" SELECT articuloInventario.Codigo AS Codigo,");
            vSql.AppendLine(SqlUtil.Left("articuloInventario.Descripcion", 30) + " AS Descripcion,");
            vSql.AppendLine(" articuloInventario.EXISTENCIA,");
            vSql.AppendLine(" articuloInventario.CostoUnitario,");
            vSql.AppendLine(" articuloInventario.EXISTENCIA * " + SqlUtil.ToDbl(" articuloInventario.CostoUnitario ") + " AS Valoracion,");
            vSql.AppendLine(vSqlPrecioDeVenta + " AS PrecioDeVenta,");
            vSql.AppendLine(" articuloInventario.EXISTENCIA  * " + SqlUtil.ToDbl(vSqlPrecioDeVenta) + " AS ValorDeVenta,");
            vSql.AppendLine(" articuloInventario.LineaDeProducto, ");
            vSql.AppendLine(vSqlCambio + " AS Cambio, ");
            vSql.AppendLine(vSqlFechaUltimaCompra + "AS FechaUltimaCompra, ");
            vSql.AppendLine(vSqlCostoUnitarioME + " AS CostoUnitarioME, ");
            vSql.AppendLine(" articuloInventario.EXISTENCIA * " + vSqlCostoUnitarioME + " AS ValoracionME, ");
            vSql.AppendLine(vSqlPrecioDeVentaME + " AS PrecioDeVentaME, ");
            vSql.AppendLine(" articuloInventario.EXISTENCIA * " + vSqlPrecioDeVentaME + " AS ValorDeVentaME ");
            vSql.AppendLine(" FROM articuloInventario ");
            vSql.AppendLine(" LEFT OUTER JOIN CTE_UltimaCompra  ON");
            vSql.AppendLine(" CTE_UltimaCompra.CodigoArticulo = ArticuloInventario.Codigo AND");
            vSql.AppendLine(" CTE_UltimaCompra.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine(vSqlWhere);
            vSql.AppendLine(" ORDER BY ");
            vSql.AppendLine(" articuloInventario.LINEADEPRODUCTO,");
            vSql.AppendLine(" articuloInventario.CODIGO");
            return vSql.ToString();
        }

        public string SqlListadoDePreciosBsD(int valConsecutivoCompania, string valNombreLineaDeProducto, int valCantidadDecimales) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            string vCantidadDecimales = LibConvert.ToStr(valCantidadDecimales);
            string vFactor = insSql.ToSqlValue(Galac.Saw.Reconv.clsUtilReconv.GetFactorDeConversion());
            string vSQLWhere = "";
            vSql.AppendLine("SELECT Codigo");
            vSql.AppendLine("			, Descripcion");
            vSql.AppendLine("			, PrecioConIva");
            vSql.AppendLine("			, ROUND(PrecioConIva / " + vFactor + "," + vCantidadDecimales + ") AS EnBsS");
            vSql.AppendLine("			, PrecioConIva2");
            vSql.AppendLine("			, ROUND(PrecioConIva2 /  " + vFactor + "," + vCantidadDecimales + ") AS EnBsS2");
            vSql.AppendLine("			, PrecioConIva3");
            vSql.AppendLine("			, ROUND(PrecioConIva3 / " + vFactor + "," + vCantidadDecimales + ") AS EnBsS3");
            vSql.AppendLine("			, PrecioConIva4");
            vSql.AppendLine("			, ROUND(PrecioConIva4 / " + vFactor + "," + vCantidadDecimales + ") AS EnBsS4");
            vSql.AppendLine("			, LineaDeProducto");
            vSql.AppendLine("			 FROM ArticuloInventario");
            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "ConsecutivoCompania", valConsecutivoCompania);
            if (LibString.Len(valNombreLineaDeProducto) > 0) {
                vSQLWhere = vSQLWhere + " AND LineaDeProducto = " + insSql.ToSqlValue(valNombreLineaDeProducto);
            }
            vSQLWhere = vSQLWhere + " AND Codigo NOT LIKE  'RD%@' AND Codigo NOT LIKE  'RD_AliExten%'";
            vSql.AppendLine(" WHERE " + vSQLWhere);
            vSql.AppendLine(" ORDER BY  LineaDeProducto, Codigo ");
            return vSql.ToString();
        }

        public string SqlListadoDePreciosME(int valConsecutivoCompania, string valNombreLineaDeProducto, decimal valTasaCambio, int valCantidadDecimales) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            string vFactor = insSql.ToSqlValue(Galac.Saw.Reconv.clsUtilReconv.GetFactorDeConversion());
            string vTasaCambio = insSql.ToSqlValue(valTasaCambio);
            string vCantidadDecimales = LibConvert.ToStr(valCantidadDecimales);
            string vSQLWhere = "";
            vSql.AppendLine("SELECT ArticuloInventario.Codigo");
            vSql.AppendLine("			, ArticuloInventario.Descripcion");
            vSql.AppendLine("			, ROUND(CamposMonedaExtranjera.MePrecioConIva * " + vTasaCambio + "," + vCantidadDecimales + ") AS PrecioConIva");
            vSql.AppendLine("			, ROUND(ROUND(CamposMonedaExtranjera.MePrecioConIva * " + vTasaCambio + "," + vCantidadDecimales + ") * " + vFactor + "," + vCantidadDecimales + ") AS EnBsS");
            vSql.AppendLine("			, ROUND(CamposMonedaExtranjera.MePrecioConIva2 * " + vTasaCambio + "," + vCantidadDecimales + ") AS PrecioConIva2");
            vSql.AppendLine("			, ROUND(ROUND(CamposMonedaExtranjera.MePrecioConIva2 * " + vTasaCambio + "," + vCantidadDecimales + ") *  " + vFactor + "," + vCantidadDecimales + ") AS EnBsS2");
            vSql.AppendLine("			, ROUND(CamposMonedaExtranjera.MePrecioConIva3 * " + vTasaCambio + "," + vCantidadDecimales + ") AS PrecioConIva3");
            vSql.AppendLine("			, ROUND(ROUND(CamposMonedaExtranjera.MePrecioConIva3 * " + vTasaCambio + "," + vCantidadDecimales + ") * " + vFactor + "," + vCantidadDecimales + ") AS EnBsS3");
            vSql.AppendLine("			, ROUND(CamposMonedaExtranjera.MePrecioConIva4 * " + vTasaCambio + "," + vCantidadDecimales + ") AS PrecioConIva4");
            vSql.AppendLine("			, ROUND(ROUND(CamposMonedaExtranjera.MePrecioConIva4 * " + vTasaCambio + "," + vCantidadDecimales + ") * " + vFactor + "," + vCantidadDecimales + ") AS EnBsS4");
            vSql.AppendLine("			, ArticuloInventario.LineaDeProducto");
            vSql.AppendLine("			 FROM ArticuloInventario");
            vSql.AppendLine("  INNER JOIN");
            vSql.AppendLine(" CamposMonedaExtranjera");
            vSql.AppendLine(" ON ArticuloInventario.Codigo = CamposMonedaExtranjera.Codigo AND");
            vSql.AppendLine(" ArticuloInventario.ConsecutivoCompania = CamposMonedaExtranjera.ConsecutivoCompania");
            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania);
            if (LibString.Len(valNombreLineaDeProducto) > 0) {
                vSQLWhere = vSQLWhere + " AND ArticuloInventario.LineaDeProducto = " + insSql.ToSqlValue(valNombreLineaDeProducto);
            }
            vSQLWhere = vSQLWhere + " AND ArticuloInventario.Codigo NOT LIKE  'RD%@' AND ArticuloInventario.Codigo NOT LIKE  'RD_AliExten%'";
            vSql.AppendLine(" WHERE " + vSQLWhere);
            vSql.AppendLine(" ORDER BY  ArticuloInventario.LineaDeProducto, ArticuloInventario.Codigo ");
            return vSql.ToString();
        }
		
        public string SqlListadoDePreciosBsDME(int valConsecutivoCompania, string valNombreLineaDeProducto, decimal valTasaCambio, int valCantidadDecimales) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            string vFactor = insSql.ToSqlValue(Galac.Saw.Reconv.clsUtilReconv.GetFactorDeConversion());
            string vTasaCambio = insSql.ToSqlValue(valTasaCambio);
            string vCantidadDecimales = LibConvert.ToStr(valCantidadDecimales);
            string vSQLWhere = "";
            vSql.AppendLine("SELECT ArticuloInventario.Codigo");
            vSql.AppendLine("			, ArticuloInventario.Descripcion");
            vSql.AppendLine("			, ROUND(CamposMonedaExtranjera.MePrecioConIva * " + vTasaCambio + "," + vCantidadDecimales + ") AS PrecioConIva");
            vSql.AppendLine("			, ROUND(ROUND(CamposMonedaExtranjera.MePrecioConIva * " + vTasaCambio + "," + vCantidadDecimales + ") / " + vFactor + "," + vCantidadDecimales + ") AS EnBsS");
            vSql.AppendLine("			, ROUND(CamposMonedaExtranjera.MePrecioConIva2 * " + vTasaCambio + "," + vCantidadDecimales + ") AS PrecioConIva2");
            vSql.AppendLine("			, ROUND(ROUND(CamposMonedaExtranjera.MePrecioConIva2 * " + vTasaCambio + "," + vCantidadDecimales + ") /  " + vFactor + "," + vCantidadDecimales + ") AS EnBsS2");
            vSql.AppendLine("			, ROUND(CamposMonedaExtranjera.MePrecioConIva3 * " + vTasaCambio + "," + vCantidadDecimales + ") AS PrecioConIva3");
            vSql.AppendLine("			, ROUND(ROUND(CamposMonedaExtranjera.MePrecioConIva3 * " + vTasaCambio + "," + vCantidadDecimales + ") / " + vFactor + "," + vCantidadDecimales + ") AS EnBsS3");
            vSql.AppendLine("			, ROUND(CamposMonedaExtranjera.MePrecioConIva4 * " + vTasaCambio + "," + vCantidadDecimales + ") AS PrecioConIva4");
            vSql.AppendLine("			, ROUND(ROUND(CamposMonedaExtranjera.MePrecioConIva4 * " + vTasaCambio + "," + vCantidadDecimales + ") / " + vFactor + "," + vCantidadDecimales + ") AS EnBsS4");
            vSql.AppendLine("			, ArticuloInventario.LineaDeProducto");
            vSql.AppendLine("			 FROM ArticuloInventario");
            vSql.AppendLine("  INNER JOIN");
            vSql.AppendLine(" CamposMonedaExtranjera");
            vSql.AppendLine(" ON ArticuloInventario.Codigo = CamposMonedaExtranjera.Codigo AND");
            vSql.AppendLine(" ArticuloInventario.ConsecutivoCompania = CamposMonedaExtranjera.ConsecutivoCompania");
            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania);
            if (LibString.Len(valNombreLineaDeProducto) > 0) {
                vSQLWhere = vSQLWhere + " AND ArticuloInventario.LineaDeProducto = " + insSql.ToSqlValue(valNombreLineaDeProducto);
            }
            vSQLWhere = vSQLWhere + " AND ArticuloInventario.Codigo NOT LIKE  'RD%@' AND ArticuloInventario.Codigo NOT LIKE  'RD_AliExten%'";
            vSql.AppendLine(" WHERE " + vSQLWhere);
            vSql.AppendLine(" ORDER BY  ArticuloInventario.LineaDeProducto, ArticuloInventario.Codigo ");
            return vSql.ToString();
        }
        #endregion //Metodos Generados
    } //End of class clsArticuloInventarioSql
} //End of namespace Galac.Saw.Brl.Inventario