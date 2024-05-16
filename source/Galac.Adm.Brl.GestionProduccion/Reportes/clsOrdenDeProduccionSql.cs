using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Brl. GestionProduccion.Reportes {
    public class clsOrdenDeProduccionSql {
        #region Metodos Generados
		public string SqlOrdenDeProduccionRpt(int valConsecutivoCompania, string valCodigoOrden, DateTime valFechaInicio, DateTime valFechaFinal, eGeneradoPor valGeneradoPor){
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT");
            vSql.AppendLine("Adm.OrdenDeProduccion.ConsecutivoCompania,");
            vSql.AppendLine("Adm.OrdenDeProduccion.Codigo,");
            vSql.AppendLine("Adm.OrdenDeProduccion.FechaInicio,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CodigoArticulo + ' - ' + dbo.ArticuloInventario.Descripcion AS InventarioAProducir,");
            vSql.AppendLine("Saw.Almacen.Codigo + ' - ' + Saw.Almacen.NombreAlmacen AS AlmacenProductoTerminado,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CantidadSolicitada,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CodigoArticulo + ' - ' + ArticuloInventario_1.Descripcion AS MaterialesServicioUtilizado,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CantidadReservadaInventario,");
            vSql.AppendLine("Almacen_1.Codigo + ' - ' + Almacen_1.NombreAlmacen AS AlmacenMaterialesServicioUtilizado");
            vSql.AppendLine("FROM");
            vSql.AppendLine("Adm.OrdenDeProduccion INNER JOIN");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo ON Adm.OrdenDeProduccion.ConsecutivoCompania = Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccion.Consecutivo = Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion INNER JOIN");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales ON Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion = Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoOrdenDeProduccion AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.Consecutivo = Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoOrdenDeProduccionDetalleArticulo INNER JOIN");
            vSql.AppendLine("dbo.ArticuloInventario ON Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CodigoArticulo = dbo.ArticuloInventario.Codigo INNER JOIN");
            vSql.AppendLine("dbo.ArticuloInventario AS ArticuloInventario_1 ON Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoCompania = ArticuloInventario_1.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CodigoArticulo = ArticuloInventario_1.Codigo INNER JOIN");
            vSql.AppendLine("Saw.Almacen ON Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoAlmacen = Saw.Almacen.Consecutivo AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = Saw.Almacen.ConsecutivoCompania INNER JOIN");
            vSql.AppendLine("Saw.Almacen AS Almacen_1 ON Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoCompania = Almacen_1.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoAlmacen = Almacen_1.Consecutivo");

            string vSqlWhere = new QAdvSql("").SqlIntValueWithAnd(string.Empty, "Adm.OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            if (valGeneradoPor == eGeneradoPor.Orden) {
                vSqlWhere = new QAdvSql("").SqlValueWithAnd(vSqlWhere, "Adm.OrdenDeProduccion.Codigo", valCodigoOrden);
            } else if (valGeneradoPor == eGeneradoPor.Fecha) {
                vSqlWhere = new QAdvSql("").SqlDateValueBetween(vSqlWhere, "Adm.OrdenDeProduccion.FechaCreacion", valFechaInicio, valFechaFinal);
            }
            vSqlWhere = new QAdvSql("").SqlEnumValueWithAnd(vSqlWhere, "Adm.OrdenDeProduccion.StatusOp", (int)eTipoStatusOrdenProduccion.Ingresada);
            if (LibString.Len(vSqlWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSqlWhere);
            }
            vSql.AppendLine("ORDER BY Adm.OrdenDeProduccion.Codigo");

			return vSql.ToString();
		}

        public string SqlRequisicionDeMateriales(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, bool valMostrarSoloExistenciaInsuficiente, string valCodigoOrden, eGeneradoPor valGeneradoPor) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT");
            vSql.AppendLine("Adm.OrdenDeProduccion.ConsecutivoCompania,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CodigoArticulo + ' - ' + ArticuloInventario_1.Descripcion AS MaterialesServicioUtilizado,");
            vSql.AppendLine("Adm.OrdenDeProduccion.FechaInicio,");
            vSql.AppendLine("Adm.OrdenDeProduccion.Codigo,");
            vSql.AppendLine("Almacen_1.Codigo + ' - ' + Almacen_1.NombreAlmacen AS AlmacenMaterialesServicioUtilizado,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CantidadReservadaInventario, ");
            vSql.AppendLine("ArticuloInventario_1.Existencia, ");
            vSql.AppendLine("ArticuloInventario_1.TipoDeArticulo, ");
            vSql.AppendLine("CASE WHEN ArticuloInventario_1.TipoDeArticulo ='1' THEN 'N/A' ELSE Convert(varchar, cast(ArticuloInventario_1.Existencia as money), 2) END as ExistenciaToStr ");
            vSql.AppendLine("FROM");
            vSql.AppendLine("Adm.OrdenDeProduccion INNER JOIN");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo ON Adm.OrdenDeProduccion.ConsecutivoCompania = Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccion.Consecutivo = Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion INNER JOIN");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales ON Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion = Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoOrdenDeProduccion AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.Consecutivo = Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoOrdenDeProduccionDetalleArticulo INNER JOIN");
            vSql.AppendLine("dbo.ArticuloInventario ON Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CodigoArticulo = dbo.ArticuloInventario.Codigo INNER JOIN");
            vSql.AppendLine("dbo.ArticuloInventario AS ArticuloInventario_1 ON Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoCompania = ArticuloInventario_1.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CodigoArticulo = ArticuloInventario_1.Codigo INNER JOIN");
            vSql.AppendLine("Saw.Almacen ON Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoAlmacen = Saw.Almacen.Consecutivo AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = Saw.Almacen.ConsecutivoCompania INNER JOIN");
            vSql.AppendLine("Saw.Almacen AS Almacen_1 ON Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoCompania = Almacen_1.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoAlmacen = Almacen_1.Consecutivo");

            string vSqlWhere = new QAdvSql("").SqlIntValueWithAnd(string.Empty, "Adm.OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            if (valGeneradoPor == eGeneradoPor.Orden) {
                vSqlWhere = new QAdvSql("").SqlValueWithAnd(vSqlWhere, "Adm.OrdenDeProduccion.Codigo", valCodigoOrden);
            } else if (valGeneradoPor == eGeneradoPor.Fecha) {
                vSqlWhere = new QAdvSql("").SqlDateValueBetween(vSqlWhere, "Adm.OrdenDeProduccion.FechaCreacion", valFechaInicial, valFechaFinal);
            }
            vSqlWhere = new QAdvSql("").SqlEnumValueWithAnd(vSqlWhere, "Adm.OrdenDeProduccion.StatusOp", (int)eTipoStatusOrdenProduccion.Ingresada);
            if (LibString.Len(vSqlWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSqlWhere);
            }
            vSql.AppendLine("ORDER BY Adm.OrdenDeProduccionDetalleMateriales.CodigoArticulo, Adm.OrdenDeProduccion.Codigo");

            return vSql.ToString();
		}

        public string SqlCostoProduccionInventarioEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eCantidadAImprimir valCantidadAImprimir, string valCodigoInventario, eGeneradoPor valGeneradoPor, string valCodigoOrden) {
            string vSQLWhere = string.Empty;
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT");
            vSql.AppendLine("Adm.OrdenDeProduccion.ConsecutivoCompania,");
            vSql.AppendLine("Adm.OrdenDeProduccion.Codigo,");
            vSql.AppendLine("dbo.ArticuloInventario.Codigo + ' - ' + dbo.ArticuloInventario.Descripcion  AS ArticuloInventario,");
            vSql.AppendLine("Adm.OrdenDeProduccion.FechaFinalizacion,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CantidadProducida,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CostoUnitario,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.MontoSubTotal");
            vSql.AppendLine("FROM");
            vSql.AppendLine("Adm.OrdenDeProduccion INNER JOIN");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo ON Adm.OrdenDeProduccion.ConsecutivoCompania = Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccion.Consecutivo = Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion INNER JOIN");
            vSql.AppendLine("dbo.ArticuloInventario ON Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CodigoArticulo = dbo.ArticuloInventario.Codigo");

            vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(string.Empty, "Adm.OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = new QAdvSql("").SqlEnumValueWithAnd(vSQLWhere, "Adm.OrdenDeProduccion.StatusOp", (int)eTipoStatusOrdenProduccion.Cerrada);
            if (valCantidadAImprimir == eCantidadAImprimir.One) {
                vSQLWhere = new QAdvSql("").SqlValueWithAnd(vSQLWhere, "dbo.ArticuloInventario.Codigo", valCodigoInventario);
            }
            if (valGeneradoPor == eGeneradoPor.Orden) {
                vSQLWhere = new QAdvSql("").SqlValueWithAnd(vSQLWhere, "Adm.OrdenDeProduccion.Codigo", valCodigoOrden);
            } else if (valGeneradoPor == eGeneradoPor.Fecha) {
                vSQLWhere = new QAdvSql("").SqlDateValueBetween(vSQLWhere, "Adm.OrdenDeProduccion.FechaFinalizacion", valFechaDesde, valFechaHasta);
            }
			if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSQLWhere);
			}
            return vSql.ToString();
		}

        public string SqlCostoMatServUtilizadosEnProduccionInv(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoOrden, eGeneradoPor valGeneradoPor) {
            string vSQLWhere = string.Empty;
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CodigoArticulo + ' - ' + ArticuloInventario.Descripcion AS InventarioProducido,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CodigoArticulo + ' - ' + ArticuloInventario_1.Descripcion AS ArticuloServicioUtilizado,");
            vSql.AppendLine("Adm.OrdenDeProduccion.Codigo AS Orden,");
            vSql.AppendLine("Adm.OrdenDeProduccion.FechaFinalizacion,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CantidadConsumida,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CostoUnitarioArticuloInventario,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.MontoSubtotal");
            vSql.AppendLine("FROM");
            vSql.AppendLine("Adm.OrdenDeProduccion INNER JOIN");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo ON Adm.OrdenDeProduccion.ConsecutivoCompania = Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccion.Consecutivo = Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion INNER JOIN");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales ON Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion = Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoOrdenDeProduccion AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.Consecutivo = Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoOrdenDeProduccionDetalleArticulo INNER JOIN");
            vSql.AppendLine("dbo.ArticuloInventario ON Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CodigoArticulo = dbo.ArticuloInventario.Codigo INNER JOIN");
            vSql.AppendLine("dbo.ArticuloInventario AS ArticuloInventario_1 ON Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoCompania = ArticuloInventario_1.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CodigoArticulo = ArticuloInventario_1.Codigo");

            vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(string.Empty, "Adm.OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = new QAdvSql("").SqlEnumValueWithAnd(vSQLWhere, "Adm.OrdenDeProduccion.StatusOp", (int)eTipoStatusOrdenProduccion.Cerrada);
            if (valGeneradoPor == eGeneradoPor.Orden) {
                vSQLWhere = new QAdvSql("").SqlValueWithAnd(vSQLWhere, "Adm.OrdenDeProduccion.Codigo", valCodigoOrden);
            } else if (valGeneradoPor == eGeneradoPor.Fecha) {
                vSQLWhere = new QAdvSql("").SqlDateValueBetween(vSQLWhere, "Adm.OrdenDeProduccion.FechaFinalizacion", valFechaDesde, valFechaHasta);
            }
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSQLWhere);
            }
            vSql.AppendLine("ORDER BY Adm.OrdenDeProduccionDetalleArticulo.CodigoArticulo,Adm.OrdenDeProduccionDetalleMateriales.CodigoArticulo, Adm.OrdenDeProduccion.Codigo");

            return vSql.ToString();
        }

        public string SqlProduccionPorEstatusEntreFecha(int valConsecutivoCompania, eTipoStatusOrdenProduccion valEstatus, DateTime valFechaInicial, DateTime valFechaFinal, eGeneradoPor valGeneradoPor, string valCodigoOrden) {
            string vSQLWhere = string.Empty;
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT");
            vSql.AppendLine("Adm.OrdenDeProduccion.StatusOp,");
            vSql.AppendLine("Adm.Gv_EnumTipoStatusOrdenProduccion.StrValue AS EstatusStr,");
            vSql.AppendLine("dbo.ArticuloInventario.Codigo + '-' +  dbo.ArticuloInventario.Descripcion AS InventarioAProducir,");
            vSql.AppendLine("Adm.OrdenDeProduccion.FechaCreacion,");
            vSql.AppendLine("Adm.OrdenDeProduccion.FechaInicio,");
            vSql.AppendLine("Adm.OrdenDeProduccion.FechaFinalizacion,");
            vSql.AppendLine("Adm.OrdenDeProduccion.Codigo AS Orden,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CantidadSolicitada,");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CantidadProducida,");
            vSql.AppendLine("Adm.OrdenDeProduccion.MotivoDeAnulacion");
            vSql.AppendLine("FROM");
            vSql.AppendLine("Adm.OrdenDeProduccion INNER JOIN");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo ON Adm.OrdenDeProduccion.ConsecutivoCompania = Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccion.Consecutivo = Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion INNER JOIN");
            vSql.AppendLine("dbo.ArticuloInventario ON Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CodigoArticulo = dbo.ArticuloInventario.Codigo  INNER JOIN");
            vSql.AppendLine("Adm.Gv_EnumTipoStatusOrdenProduccion ON Adm.OrdenDeProduccion.StatusOp = Adm.Gv_EnumTipoStatusOrdenProduccion.DbValue COLLATE Modern_Spanish_CI_AS");

            vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(vSQLWhere, "Adm.OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = new QAdvSql("").SqlEnumValueWithAnd(vSQLWhere, "Adm.OrdenDeProduccion.StatusOp", (int)valEstatus);

            if (valGeneradoPor == eGeneradoPor.Orden) {
                vSQLWhere = new QAdvSql("").SqlValueWithAnd(vSQLWhere, "Adm.OrdenDeProduccion.Codigo", valCodigoOrden);
            } else {

                if (valEstatus == eTipoStatusOrdenProduccion.Ingresada) {
                    vSQLWhere = new QAdvSql("").SqlDateValueBetween(vSQLWhere, "Adm.OrdenDeProduccion.FechaCreacion", valFechaInicial, valFechaFinal);
                } else if (valEstatus == eTipoStatusOrdenProduccion.Iniciada) {
                    vSQLWhere = new QAdvSql("").SqlDateValueBetween(vSQLWhere, "Adm.OrdenDeProduccion.FechaInicio", valFechaInicial, valFechaFinal);
                } else if (valEstatus == eTipoStatusOrdenProduccion.Anulada || valEstatus == eTipoStatusOrdenProduccion.Cerrada) {
                    vSQLWhere = new QAdvSql("").SqlDateValueBetween(vSQLWhere, "Adm.OrdenDeProduccion.FechaFinalizacion", valFechaInicial, valFechaFinal);
                }
            }
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSQLWhere);
            }
            return vSql.ToString();
        }

        public string SqlDetalleDeCostoDeProduccion(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eSeleccionarOrdenPor valSeleccionarOrdenPor, int valConsecutivoOrden) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vUtilSql = new QAdvSql("");
            string vSQLWhere;

            vSQLWhere = vUtilSql.SqlIntValueWithAnd(string.Empty, "OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vUtilSql.SqlEnumValueWithAnd(vSQLWhere, "OrdenDeProduccion.StatusOp", (int)eTipoStatusOrdenProduccion.Cerrada);
            if (valSeleccionarOrdenPor == eSeleccionarOrdenPor.NumeroDeOrden) {
                vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "OrdenDeProduccion.Consecutivo", valConsecutivoOrden);
            } else if (valSeleccionarOrdenPor == eSeleccionarOrdenPor.FechaDeInicio) {
                vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "OrdenDeProduccion.FechaInicio", valFechaInicial, valFechaFinal);
            } else if (valSeleccionarOrdenPor == eSeleccionarOrdenPor.FechaDeFinalizacion) {
                vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "OrdenDeProduccion.FechaFinalizacion", valFechaInicial, valFechaFinal);
            }

            string vCodigoMonedaLocal = "VED";//parámetro
            string vCodigoMonedaExtranjera = "USD";//parámetro

            vSql.AppendLine("SELECT");
            vSql.AppendLine("OrdenDeProduccion.Codigo AS CodigoOrden,");
            vSql.AppendLine("Insumos.Consecutivo AS ConsecutivoInsumos,");
            vSql.AppendLine("OrdenDeProduccion.Codigo + ' - ' + OrdenDeProduccion.Descripcion AS OrdenCodigoDescripcion,");
            vSql.AppendLine("");
            vSql.AppendLine("OrdenDeProduccion.StatusOp AS Estatus,");
            vSql.AppendLine("OrdenDeProduccion.CodigoMonedaCostoProduccion AS Moneda,");
            vSql.AppendLine("OrdenDeProduccion.CambioCostoProduccion AS Cambio,");
            vSql.AppendLine("OrdenDeProduccion.FechaInicio,");
            vSql.AppendLine("OrdenDeProduccion.FechaFinalizacion,");
            vSql.AppendLine("(CASE WHEN OrdenDeProduccion.CostoTerminadoCalculadoAPartirDe = '0' THEN 'Costos expresados en ' + ISNULL((SELECT TOP 1 Nombre FROM Moneda WHERE Codigo = " + vUtilSql.ToSqlValue(vCodigoMonedaLocal) + "), " + vUtilSql.ToSqlValue(vCodigoMonedaLocal) + ") ELSE 'Costo expresados en ' + ISNULL((SELECT TOP 1 Nombre FROM Moneda WHERE Codigo = OrdenDeProduccion.CodigoMonedaCostoProduccion), " + vUtilSql.ToSqlValue(vCodigoMonedaExtranjera) + " )  END) AS FormaDeCalculoDelCosto,");
            vSql.AppendLine("OrdenDeProduccion.Observacion,");
            vSql.AppendLine("AlmacenInsumos.Codigo +  ' - ' + AlmacenInsumos.NombreAlmacen AS AlmacenInsumos,");
            vSql.AppendLine("AlmacenSalidas.Codigo +  ' - ' + AlmacenInsumos.NombreAlmacen AS AlmacenSalida,");
            vSql.AppendLine("Insumos.CodigoArticulo + ' - ' + ArticuloInventario.Descripcion AS ArticuloInventarioInsumo,");
            vSql.AppendLine("ArticuloInventario.UnidadDeVenta AS Unidad,");
            vSql.AppendLine("Insumos.CantidadReservadaInventario AS CantidadEstimada,");
            vSql.AppendLine("Insumos.CantidadConsumida,");
            vSql.AppendLine("Insumos.CostoUnitarioArticuloInventario AS CostoUnitarioMatServ,");
            vSql.AppendLine("Insumos.MontoSubtotal AS MontoTotalConsumo");

            vSql.AppendLine("FROM Adm.OrdenDeProduccion INNER JOIN Adm.OrdenDeProduccionDetalleMateriales AS Insumos ON OrdenDeProduccion.ConsecutivoCompania = Insumos.ConsecutivoCompania AND OrdenDeProduccion.Consecutivo = Insumos.ConsecutivoOrdenDeProduccion");
            vSql.AppendLine("INNER JOIN Saw.Almacen AS AlmacenInsumos ON OrdenDeProduccion.ConsecutivoCompania = AlmacenInsumos.ConsecutivoCompania AND OrdenDeProduccion.ConsecutivoAlmacenMateriales = AlmacenInsumos.Consecutivo");
            vSql.AppendLine("INNER JOIN Saw.Almacen AS AlmacenSalidas ON OrdenDeProduccion.ConsecutivoCompania = AlmacenSalidas.ConsecutivoCompania AND OrdenDeProduccion.ConsecutivoAlmacenProductoTerminado = AlmacenSalidas.Consecutivo");
            vSql.AppendLine("INNER JOIN ArticuloInventario ON Insumos.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND Insumos.CodigoArticulo = ArticuloInventario.Codigo");

            vSql.AppendLine(vUtilSql.WhereSql(vSQLWhere));
            vSql.AppendLine("ORDER BY CodigoOrden, ConsecutivoInsumos");
            return vSql.ToString();
        }

        public string SqlDetalleDeCostoDeProduccionSalida(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eSeleccionarOrdenPor valSeleccionarOrdenPor, int valConsecutivoOrden) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vUtilSql = new QAdvSql("");
            string vSQLWhere;
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(string.Empty, "OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vUtilSql.SqlEnumValueWithAnd(vSQLWhere, "OrdenDeProduccion.StatusOp", (int)eTipoStatusOrdenProduccion.Cerrada);
            if (valSeleccionarOrdenPor == eSeleccionarOrdenPor.NumeroDeOrden) {
                vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "OrdenDeProduccion.Consecutivo", valConsecutivoOrden);
            } else if (valSeleccionarOrdenPor == eSeleccionarOrdenPor.FechaDeInicio) {
                vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "OrdenDeProduccion.FechaInicio", valFechaInicial, valFechaFinal);
            } else if (valSeleccionarOrdenPor == eSeleccionarOrdenPor.FechaDeFinalizacion) {
                vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "OrdenDeProduccion.FechaFinalizacion", valFechaInicial, valFechaFinal);
            }

            vSql.AppendLine("SELECT");
            vSql.AppendLine("OrdenDeProduccion.Codigo AS CodigoOrden,");
            vSql.AppendLine("Salidas.Consecutivo AS ConsecutivoSalidas,");            
            vSql.AppendLine("Salidas.CodigoArticulo + ' - ' + ArticuloInventario.Descripcion AS DescripcionArticuloSalida,");
            vSql.AppendLine("ArticuloInventario.UnidadDeVenta AS Unidad,");
            vSql.AppendLine("Salidas.CantidadSolicitada AS CantidadAProducir,");
            vSql.AppendLine("Salidas.CantidadProducida,");
            vSql.AppendLine("Salidas.PorcentajeCostoCierre AS PorcentajeCosto,");
            vSql.AppendLine("Salidas.CostoUnitario AS CostoUnitario,");
            vSql.AppendLine("Salidas.MontoSubTotal AS CostoTotal");

            vSql.AppendLine("FROM Adm.OrdenDeProduccion INNER JOIN Adm.OrdenDeProduccionDetalleArticulo AS Salidas ON OrdenDeProduccion.ConsecutivoCompania = Salidas.ConsecutivoCompania AND OrdenDeProduccion.Consecutivo = Salidas.ConsecutivoOrdenDeProduccion");
            vSql.AppendLine("INNER JOIN ArticuloInventario AS ArticuloInventario ON Salidas.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND Salidas.CodigoArticulo = ArticuloInventario.Codigo ");
            vSql.AppendLine(vUtilSql.WhereSql(vSQLWhere));
            vSql.AppendLine("ORDER BY CodigoOrden, ConsecutivoSalidas");
            return vSql.ToString();
        }
        #endregion //Metodos Generados

    } //End of class clsOrdenDeProduccionSql

} //End of namespace Galac.Adm.Brl. GestionProduccion

