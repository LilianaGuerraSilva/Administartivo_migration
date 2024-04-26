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

        #region Metodos Generados

        public string SqlListaDeMaterialesDeInventarioAProducir(int valConsecutivoCompania, string valCodigoListaAProducir, eCantidadAImprimir valCantidadAImprimir, decimal valCantidadAProducir,eMonedaDelInformeMM valMonedaDelInformeMM, eTasaDeCambioParaImpresion valTipoTasaDeCambio, string valNombreMoneda, string valCodigoMoneda) {
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

        #endregion //Metodos Generados

    } //End of class clsListaDeMaterialesSql
} //End of namespace Galac.Adm.Brl.GestionProduccion

