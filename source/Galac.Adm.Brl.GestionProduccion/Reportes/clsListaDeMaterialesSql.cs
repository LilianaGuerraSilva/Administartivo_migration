using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.GestionProduccion.Reportes {
    
    public class clsListaDeMaterialesSql {
          private QAdvSql vSqlUtil = new QAdvSql("");
        #region Metodos Generados
        public string SqlListaDeMaterialesSalida(int valConsecutivoCompania, string valCodigoListaAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir, string valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valNombreMoneda, string valCodigoMoneda) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("ListaDeMateriales.Codigo, ");
            vSql.AppendLine("ListaDeMateriales.Codigo + ' - ' + ListaDeMateriales.Nombre AS ListaDeMateriales, ");
            vSql.AppendLine("ListaDeMaterialesDetalleSalidas.CodigoArticuloInventario AS CodigoListaSalida, ");
            vSql.AppendLine("ArticuloInventario.Descripcion AS ArticuloListaSalida, ");
            vSql.AppendLine(vSqlUtil.ToSqlValue(valCantidadAProducir) +" * ListaDeMaterialesDetalleSalidas.Cantidad As CantidadAProducirDetalle, ");
            vSql.AppendLine("ListaDeMaterialesDetalleSalidas.PorcentajeDeCosto, ");
            vSql.AppendLine("ArticuloInventario.CostoUnitario, ");
            vSql.AppendLine(vSqlUtil.ToSqlValue(valNombreMoneda) + " AS Moneda, ");
            vSql.AppendLine(vSqlUtil.ToSqlValue(valCantidadAProducir) + " AS CantidadAProducir, ");
            vSql.AppendLine("((" + vSqlUtil.ToSqlValue(valCantidadAProducir) + " * Adm.ListaDeMaterialesDetalleSalidas.Cantidad) * ArticuloInventario.CostoUnitario) AS CostoCalculado");
            vSql.AppendLine("FROM Adm.ListaDeMateriales ");
            vSql.AppendLine("INNER JOIN Adm.ListaDeMaterialesDetalleSalidas ON ");
            vSql.AppendLine("ListaDeMateriales.ConsecutivoCompania = ListaDeMaterialesDetalleSalidas.ConsecutivoCompania AND ");
            vSql.AppendLine("ListaDeMateriales.Consecutivo = ListaDeMaterialesDetalleSalidas.ConsecutivoListaDeMateriales ");
            vSql.AppendLine("INNER JOIN ArticuloInventario ON ");
            vSql.AppendLine("ListaDeMaterialesDetalleSalidas.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND ");
            vSql.AppendLine("ListaDeMaterialesDetalleSalidas.CodigoArticuloInventario = ArticuloInventario.Codigo ");
            vSql.AppendLine("WHERE ListaDeMateriales.ConsecutivoCompania =  " + vSqlUtil.ToSqlValue(valConsecutivoCompania));
            if (valCantidadAImprimir == eCantidadAImprimir.One) {
                vSql.AppendLine(" AND ListaDeMateriales.Codigo = " + vSqlUtil.ToSqlValue(valCodigoListaAProducir));
            }
            vSql.AppendLine(" ORDER BY ListaDeMateriales.Codigo,ListaDeMateriales.FechaCreacion");
            return vSql.ToString();
        }

        public string SqlListaDeMaterialesInsumos(int valConsecutivoCompania, string valCodigoListaAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir, string valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valNombreMoneda, string valCodigoMoneda) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("ListaDeMaterialesDetalleArticulo.CodigoArticuloInventario AS CodigoArticuloListainsumos, ");
            vSql.AppendLine("ListaDeMaterialesDetalleArticulo.CodigoArticuloInventario + ' - ' + ArticuloInventario.Descripcion AS ListaArticuloInsumos, ");
            vSql.AppendLine("ListaDeMaterialesDetalleArticulo.Cantidad AS CantidadInsumos, ");
            vSql.AppendLine("ArticuloInventario.CostoUnitario, ");
            vSql.AppendLine("(" + vSqlUtil.ToSqlValue(valCantidadAProducir) + " * Adm.ListaDeMaterialesDetalleArticulo.Cantidad)  AS CantidadAReservar,");
            vSql.AppendLine("((" + vSqlUtil.ToSqlValue(valCantidadAProducir) + " * Adm.ListaDeMaterialesDetalleArticulo.Cantidad) * ArticuloInventario.CostoUnitario) AS CostoTotal,");
            vSql.AppendLine("ArticuloInventario.Existencia ");
            vSql.AppendLine("FROM Adm.ListaDeMateriales ");
            vSql.AppendLine("INNER JOIN Adm.ListaDeMaterialesDetalleArticulo ON ");
            vSql.AppendLine("ListaDeMateriales.ConsecutivoCompania = ListaDeMaterialesDetalleArticulo.ConsecutivoCompania AND ");
            vSql.AppendLine("ListaDeMateriales.Consecutivo = ListaDeMaterialesDetalleArticulo.ConsecutivoListaDeMateriales ");
            vSql.AppendLine("INNER JOIN ArticuloInventario AS ArticuloInventario ON ");
            vSql.AppendLine("ListaDeMaterialesDetalleArticulo.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND ");
            vSql.AppendLine("ListaDeMaterialesDetalleArticulo.CodigoArticuloInventario = ArticuloInventario.Codigo ");
            vSql.AppendLine("WHERE ListaDeMateriales.ConsecutivoCompania = " + vSqlUtil.ToSqlValue(valConsecutivoCompania));
            if (valCantidadAImprimir == eCantidadAImprimir.One) {
                vSql.AppendLine(" AND ListaDeMateriales.Codigo = " + vSqlUtil.ToSqlValue(valCodigoListaAProducir));
            }
            vSql.AppendLine(" ORDER BY ListaDeMateriales.Codigo,ListaDeMateriales.FechaCreacion");
            return vSql.ToString();
        }

        /*
        string SqlListaDeMaterialesDeInventarioAProducir(int valConsecutivoCompania, string valCodigoListaAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir,string valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valNombreMoneda, string valCodigoMoneda) {
            string vSQLWhere = string.Empty;
            StringBuilder vSql = new StringBuilder();
            if (valCantidadAProducir == 0) { valCantidadAProducir = 1; }
            vSql.AppendLine("SELECT");
            vSql.AppendLine("Adm.ListaDeMateriales.Codigo AS CodigoListaDeMateriales,");
            vSql.AppendLine("Adm.ListaDeMateriales.Codigo + ' - ' +  Adm.ListaDeMateriales.Nombre AS ListaDeMateriales,");
            vSql.AppendLine("Adm.ListaDeMateriales.CodigoArticuloInventario AS CodigoInventarioAProducir, ");
            vSql.AppendLine("Adm.ListaDeMateriales.CodigoArticuloInventario + ' - ' + dbo.ArticuloInventario.Descripcion AS InventarioAProducir,");
            vSql.AppendLine("Adm.ListaDeMaterialesDetalleArticulo.Consecutivo AS ItemDetalleListaMateriales,");
            vSql.AppendLine("Adm.ListaDeMaterialesDetalleArticulo.CodigoArticuloInventario  +  ' - ' + ArticuloInventario_1.Descripcion AS Articulo,");
            vSql.AppendLine("Adm.ListaDeMaterialesDetalleArticulo.Cantidad,");
            vSql.AppendLine("ArticuloInventario_1.CostoUnitario,");
            vSql.AppendLine(new QAdvSql("").ToSqlValue(valCantidadAProducir) + " AS CantidadAProducir,");
            vSql.AppendLine("(" + new QAdvSql("").ToSqlValue(valCantidadAProducir) + "*Adm.ListaDeMaterialesDetalleArticulo.Cantidad)  AS CantidadAReservarEnInventario,");
            vSql.AppendLine("((" + new QAdvSql("").ToSqlValue(valCantidadAProducir) + "*Adm.ListaDeMaterialesDetalleArticulo.Cantidad)*ArticuloInventario_1.CostoUnitario) AS CostoTotal,");
            vSql.AppendLine("ArticuloInventario_1.Existencia, ");
            vSql.AppendLine("ArticuloInventario_1.TipoDeArticulo, ");
            vSql.AppendLine("CASE WHEN ArticuloInventario_1.TipoDeArticulo ='1' THEN 'N/A' ELSE Convert(varchar, cast(ArticuloInventario_1.Existencia as money), 2) END as ExistenciaToStr ");
            vSql.AppendLine("FROM");
            vSql.AppendLine("Adm.ListaDeMateriales INNER JOIN");
            vSql.AppendLine("Adm.ListaDeMaterialesDetalleArticulo ON Adm.ListaDeMateriales.ConsecutivoCompania = Adm.ListaDeMaterialesDetalleArticulo.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.ListaDeMateriales.Consecutivo = Adm.ListaDeMaterialesDetalleArticulo.ConsecutivoListaDeMateriales INNER JOIN");
            vSql.AppendLine("dbo.ArticuloInventario ON Adm.ListaDeMateriales.ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.ListaDeMateriales.CodigoArticuloInventario = dbo.ArticuloInventario.Codigo INNER JOIN");
            vSql.AppendLine("dbo.ArticuloInventario AS ArticuloInventario_1 ON Adm.ListaDeMaterialesDetalleArticulo.ConsecutivoCompania = ArticuloInventario_1.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.ListaDeMaterialesDetalleArticulo.CodigoArticuloInventario = ArticuloInventario_1.Codigo");
            vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(vSQLWhere, "Adm.ListaDeMateriales.ConsecutivoCompania", valConsecutivoCompania);
            if (valCantidadAImprimir == eCantidadAImprimir.One) {
                vSQLWhere = new QAdvSql("").SqlValueWithAnd(vSQLWhere, "Adm.ListaDeMateriales.CodigoArticuloInventario", valCodigoListaAProducir);
            }
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSQLWhere);
            }
            vSql.AppendLine("Order By Adm.ListaDeMateriales.CodigoArticuloInventario ");
            return vSql.ToString();
        }
        */
        #endregion //Metodos Generados

    } //End of class clsListaDeMaterialesSql
} //End of namespace Galac.Adm.Brl.GestionProduccion

