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
        public string SqlListadoDePrecios(int valConsecutivoCompania,string valFiltro) {
            StringBuilder vSql = new StringBuilder();
            string DivideBy = new QAdvSql("").ToSqlValue(Galac.Saw.Reconv.clsUtilReconv.GetFactorDeConversion());
            string vSQLWhere = "";
            vSql.AppendLine("SELECT dbo.ArticuloInventario.Codigo");
            vSql.AppendLine("			, Descripcion");
            vSql.AppendLine("			, PrecioConIva");
            vSql.AppendLine("			, ROUND(PrecioConIva * " + DivideBy + ",2) AS EnBsS");
            vSql.AppendLine("			, PrecioConIva2");
            vSql.AppendLine("			, ROUND(PrecioConIva2 *  " + DivideBy + ",2) AS EnBsS2");
            vSql.AppendLine("			, PrecioConIva3");
            vSql.AppendLine("			, ROUND(PrecioConIva3 * " + DivideBy + ",2) AS EnBsS3");
            vSql.AppendLine("			, PrecioConIva4");
            vSql.AppendLine("			, ROUND(PrecioConIva4*" + DivideBy + ",2) AS EnBsS4");
            vSql.AppendLine("			, LineaDeProducto");
            vSql.AppendLine("			 FROM dbo.ArticuloInventario");
            vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(vSQLWhere,"dbo.ArticuloInventario.ConsecutivoCompania",valConsecutivoCompania);
            if(LibString.Len(valFiltro) > 0) {
                vSQLWhere = vSQLWhere + " AND dbo.ArticuloInventario.LineaDeProducto = '" + valFiltro + "'";
            }
            vSQLWhere = vSQLWhere + " AND Codigo NOT LIKE  'RD%@' AND Codigo NOT LIKE  'RD_AliExten%'";
            vSql.AppendLine(" WHERE " + vSQLWhere);
            vSql.AppendLine(" ORDER BY  LineaDeProducto, dbo.ArticuloInventario.Codigo ");
            return vSql.ToString();
        }

        public string SqlListdoDeArticulosBalanza(int valConsecutivoCompania,string valLineaDeProducto,bool valFiltrarPorLineaDeProducto) {
            QAdvSql SqlUtil = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";
            vSql.AppendLine("SELECT ");
            vSql.AppendLine(" Codigo ");
            vSql.AppendLine(", Descripcion ");
            vSql.AppendLine(", Existencia ");
            vSql.AppendLine(", LineaDeProducto ");
            vSql.AppendLine(" FROM dbo.ArticuloInventario");
            vSQLWhere += SqlUtil.SqlIntValueWithAnd(vSQLWhere,"ConsecutivoCompania",valConsecutivoCompania);
            vSQLWhere = SqlUtil.SqlBoolValueWithAnd(vSQLWhere,"UsaBalanza",true);
            if(valFiltrarPorLineaDeProducto) {
                vSQLWhere = SqlUtil.SqlValueWithAnd(vSQLWhere,"LineaDeProducto",valLineaDeProducto);
            }
            if(LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSQLWhere);
            }
            vSql.AppendLine(" ORDER BY  dbo.ArticuloInventario.LineaDeProducto, dbo.ArticuloInventario.Codigo ");
            return vSql.ToString();
        }

        public string SqlValoracionDeInventario(int valConsecutivoCompania,string valCodigoDesde,string valCodigoHasta,string valLineaDeProducto,decimal valCambioMoneda,bool valUsaPrecioConIva) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql SqlUtil = new QAdvSql("");
            string vSqlWhere = "";
            string vSqlCambio = "";
            string vSqlFechaUltimaCompra = "";
            string vSqlCostoUnitarioME = "";
            string vSqlPrecioDeVentaME = "";            
            string vSqlPrecioDeVenta = "";

            vSqlPrecioDeVenta = valUsaPrecioConIva ? "articuloInventario.PRECIOCONIVA" : "articuloInventario.PRECIOSINIVA";
            vSqlCambio = SqlUtil.IIF("CTE_UltimaCompra.CambioABolivares IS NULL OR CTE_UltimaCompra.CambioABolivares < = 1",SqlUtil.ToSqlValue(valCambioMoneda),"CTE_UltimaCompra.CambioABolivares",true);
            vSqlFechaUltimaCompra = SqlUtil.IIF("CTE_UltimaCompra.FechaUltimaCompra IS NULL ",SqlUtil.GetToday(),"CTE_UltimaCompra.FechaUltimaCompra",true);
            vSqlCostoUnitarioME = "(articuloInventario.COSTOUNITARIO /" + vSqlCambio + ")";
            vSqlPrecioDeVentaME = "(" + vSqlPrecioDeVenta + " / " + vSqlCambio + ")";            
            vSqlWhere = SqlUtil.SqlValueBetween(vSqlWhere,"articuloInventario.Codigo",valCodigoDesde,valCodigoHasta," AND ");
            vSqlWhere = SqlUtil.SqlValueWildCardWithAnd(vSqlWhere,"articuloInventario.Codigo" + " NOT ","RD_Ali");
            vSqlWhere = SqlUtil.SqlValueWildCardWithAnd(vSqlWhere,"articuloInventario.Codigo" + " NOT ","RD_Co");
            if(!LibString.IsNullOrEmpty(valLineaDeProducto)) {
                vSqlWhere = SqlUtil.SqlValueWithAnd(vSqlWhere,"articuloInventario.LineaDeProducto",valLineaDeProducto);
            }
            vSqlWhere = SqlUtil.SqlIntValueWithAnd(vSqlWhere,"articuloInventario.ConsecutivoCompania",valConsecutivoCompania);
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
            vSql.AppendLine(SqlUtil.Left("articuloInventario.Descripcion",30) + " AS Descripcion,");
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
        #endregion //Metodos Generados
    } //End of class clsArticuloInventarioSql
} //End of namespace Galac.Saw.Brl.Inventario

