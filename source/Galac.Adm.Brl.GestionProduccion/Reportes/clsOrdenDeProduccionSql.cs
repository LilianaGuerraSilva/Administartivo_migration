using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.GestionProduccion.Reportes {
    public class clsOrdenDeProduccionSql {
        private QAdvSql vSqlUtil = new QAdvSql("");
        #region Metodos Generados
        public string SqlOrdenDeProduccionRpt(int valConsecutivoCompania, string valCodigoOrden, DateTime valFechaInicio, DateTime valFechaFinal, eGeneradoPor valGeneradoPor) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("OrdenDeProduccion.ConsecutivoCompania, ");
            vSql.AppendLine("OrdenDeProduccion.Codigo, ");
            vSql.AppendLine("OrdenDeProduccion.FechaInicio, ");
            vSql.AppendLine("OrdenDeProduccionDetalleArticulo.CodigoArticulo + ' - ' + ArticuloInventario.Descripcion AS InventarioAProducir, ");
            vSql.AppendLine("Almacen.Codigo + ' - ' + Almacen.NombreAlmacen AS AlmacenProductoTerminado, ");
            vSql.AppendLine("OrdenDeProduccionDetalleArticulo.CantidadSolicitada ");
            vSql.AppendLine("FROM ");
            vSql.AppendLine("Adm.OrdenDeProduccion ");
            vSql.AppendLine("INNER JOIN Adm.OrdenDeProduccionDetalleArticulo ON ");
            vSql.AppendLine("OrdenDeProduccion.Consecutivo = OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion ");
            vSql.AppendLine("AND OrdenDeProduccion.ConsecutivoCompania = OrdenDeProduccionDetalleArticulo.ConsecutivoCompania ");
            vSql.AppendLine("INNER JOIN .ArticuloInventario ON ");
            vSql.AppendLine("OrdenDeProduccionDetalleArticulo.CodigoArticulo = ArticuloInventario.Codigo ");
            vSql.AppendLine("AND OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine("INNER JOIN Saw.Almacen ON ");
            vSql.AppendLine("OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = Almacen.ConsecutivoCompania ");
            vSql.AppendLine("AND OrdenDeProduccionDetalleArticulo.ConsecutivoAlmacen = Almacen.Consecutivo ");
            string vSqlWhere = vSqlUtil.SqlIntValueWithAnd(string.Empty, "Adm.OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            if (valGeneradoPor == eGeneradoPor.Orden) {
                vSqlWhere = vSqlUtil.SqlValueWithAnd(vSqlWhere, "Adm.OrdenDeProduccion.Codigo", valCodigoOrden);
            } else if (valGeneradoPor == eGeneradoPor.Fecha) {
                vSqlWhere = vSqlUtil.SqlDateValueBetween(vSqlWhere, "Adm.OrdenDeProduccion.FechaCreacion", valFechaInicio, valFechaFinal);
            }
            vSqlWhere = vSqlUtil.SqlEnumValueWithAnd(vSqlWhere, "Adm.OrdenDeProduccion.StatusOp", (int)eTipoStatusOrdenProduccion.Ingresada);
            if (LibString.Len(vSqlWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSqlWhere);
            }
            vSql.AppendLine("ORDER BY Adm.OrdenDeProduccion.Codigo");
            return vSql.ToString();
        }

        public string SqlOrdenDeProduccionSubRpt(int valConsecutivoCompania, string valCodigoOrden, DateTime valFechaInicio, DateTime valFechaFinal, eGeneradoPor valGeneradoPor) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("OrdenDeProduccion.ConsecutivoCompania, ");
            vSql.AppendLine("OrdenDeProduccion.Codigo, ");
            vSql.AppendLine("OrdenDeProduccion.FechaInicio, ");
            vSql.AppendLine("OrdenDeProduccionDetalleMateriales.CodigoArticulo + ' - ' + ArticuloInventario.Descripcion AS MaterialesServicioUtilizado, ");
            vSql.AppendLine("OrdenDeProduccionDetalleMateriales.CantidadReservadaInventario, ");
            vSql.AppendLine("Almacen.Codigo + ' - ' + Almacen.NombreAlmacen AS AlmacenMaterialesServicioUtilizado ");
            vSql.AppendLine("FROM ");
            vSql.AppendLine("Adm.OrdenDeProduccion ");
            vSql.AppendLine("INNER JOIN Adm.OrdenDeProduccionDetalleMateriales ON ");
            vSql.AppendLine("OrdenDeProduccion.Consecutivo = OrdenDeProduccionDetalleMateriales.ConsecutivoOrdenDeProduccion ");
            vSql.AppendLine("AND OrdenDeProduccion.ConsecutivoCompania = OrdenDeProduccionDetalleMateriales.ConsecutivoCompania ");
            vSql.AppendLine("INNER JOIN dbo.ArticuloInventario  ON ");
            vSql.AppendLine("OrdenDeProduccionDetalleMateriales.CodigoArticulo = ArticuloInventario.Codigo ");
            vSql.AppendLine("AND OrdenDeProduccionDetalleMateriales.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine("INNER JOIN Saw.Almacen AS Almacen ON ");
            vSql.AppendLine("OrdenDeProduccionDetalleMateriales.ConsecutivoAlmacen = Almacen.Consecutivo ");
            vSql.AppendLine("AND OrdenDeProduccionDetalleMateriales.ConsecutivoCompania = Almacen.ConsecutivoCompania ");           
            string vSqlWhere = vSqlUtil.SqlIntValueWithAnd(string.Empty, "Adm.OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            if (valGeneradoPor == eGeneradoPor.Orden) {
                vSqlWhere = vSqlUtil.SqlValueWithAnd(vSqlWhere, "Adm.OrdenDeProduccion.Codigo", valCodigoOrden);
            } else if (valGeneradoPor == eGeneradoPor.Fecha) {
                vSqlWhere = vSqlUtil.SqlDateValueBetween(vSqlWhere, "Adm.OrdenDeProduccion.FechaCreacion", valFechaInicio, valFechaFinal);
            }
            vSqlWhere = vSqlUtil.SqlEnumValueWithAnd(vSqlWhere, "Adm.OrdenDeProduccion.StatusOp", (int)eTipoStatusOrdenProduccion.Ingresada);
            if (LibString.Len(vSqlWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSqlWhere);
            }
            vSql.AppendLine("ORDER BY Adm.OrdenDeProduccion.Codigo");
            return vSql.ToString();
        }

        public string SqlRequisicionDeMateriales(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, bool valMostrarSoloExistenciaInsuficiente, string valCodigoOrden, eGeneradoPor valGeneradoPor) {
            StringBuilder vSql = new StringBuilder();
            string vSqlWhere = vSqlUtil.SqlIntValueWithAnd(string.Empty, "Adm.OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            vSqlWhere = vSqlUtil.SqlEnumValueWithAnd(vSqlWhere, "Adm.OrdenDeProduccion.StatusOp", (int)eTipoStatusOrdenProduccion.Ingresada);
            if (valGeneradoPor == eGeneradoPor.Orden) {
                vSqlWhere = vSqlUtil.SqlValueWithAnd(vSqlWhere, "Adm.OrdenDeProduccion.Codigo", valCodigoOrden);
            } else if (valGeneradoPor == eGeneradoPor.Fecha) {
                vSqlWhere = vSqlUtil.SqlDateValueBetween(vSqlWhere, "Adm.OrdenDeProduccion.FechaCreacion", valFechaInicial, valFechaFinal);
            }

            vSql.AppendLine("SELECT	");
            vSql.AppendLine("Adm.OrdenDeProduccion.ConsecutivoCompania, ");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CodigoArticulo + ' - ' + ArticuloInventario.Descripcion AS MaterialesServicioUtilizado, ");
            vSql.AppendLine("Adm.OrdenDeProduccion.FechaCreacion, ");
            vSql.AppendLine("Adm.OrdenDeProduccion.Codigo, ");
            vSql.AppendLine("Almacen.Codigo + ' - ' + Almacen.NombreAlmacen AS AlmacenMaterialesServicioUtilizado, ");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CantidadReservadaInventario, ");
            vSql.AppendLine("ArticuloInventario.Existencia, ");
            vSql.AppendLine("CASE WHEN ArticuloInventario.TipoDeArticulo = '1'  THEN 'N/A' ELSE CONVERT(varchar, CAST(ExistenciaPorAlmacen.Cantidad AS money), 8) END AS ExistenciaToStr");
            vSql.AppendLine("FROM Adm.OrdenDeProduccion INNER JOIN Adm.OrdenDeProduccionDetalleMateriales ");
            vSql.AppendLine("ON Adm.OrdenDeProduccion.ConsecutivoCompania = Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoCompania AND ");
            vSql.AppendLine("Adm.OrdenDeProduccion.Consecutivo = Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoOrdenDeProduccion INNER JOIN ArticuloInventario AS ArticuloInventario");
            vSql.AppendLine("ON ArticuloInventario.ConsecutivoCompania = Adm.OrdenDeProduccionDetalleMateriales.ConsecutivoCompania AND ");
            vSql.AppendLine("ArticuloInventario.Codigo = Adm.OrdenDeProduccionDetalleMateriales.CodigoArticulo INNER JOIN Saw.Almacen AS Almacen");
            vSql.AppendLine("ON Almacen.ConsecutivoCompania = Adm.OrdenDeProduccion.ConsecutivoCompania AND Almacen.Consecutivo = Adm.OrdenDeProduccion.ConsecutivoAlmacenMateriales INNER JOIN ExistenciaPorAlmacen ");
            vSql.AppendLine("ON Almacen.ConsecutivoCompania = ExistenciaPorAlmacen.ConsecutivoCompania AND Almacen.Consecutivo = ExistenciaPorAlmacen.ConsecutivoAlmacen AND  ");
            vSql.AppendLine("Almacen.ConsecutivoCompania = ExistenciaPorAlmacen.ConsecutivoCompania AND Almacen.Consecutivo = ExistenciaPorAlmacen.ConsecutivoAlmacen AND ");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleMateriales.CodigoArticulo = ExistenciaPorAlmacen.CodigoArticulo AND Adm.OrdenDeProduccion.ConsecutivoAlmacenMateriales = ExistenciaPorAlmacen.ConsecutivoAlmacen ");
            vSql.AppendLine(vSqlUtil.WhereSql(vSqlWhere));
            vSql.AppendLine("ORDER BY Adm.OrdenDeProduccionDetalleMateriales.CodigoArticulo, Adm.OrdenDeProduccion.Codigo");
            return vSql.ToString();
        }

        public string SqlCostoProduccionInventarioEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, eCantidadAImprimir valCantidadAImprimir, string valCodigoInventario, eGeneradoPor valGeneradoPor, string valCodigoOrden, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
            string vSQLWhere;
            StringBuilder vSql = new StringBuilder();
            vSQLWhere = vSqlUtil.SqlIntValueWithAnd(string.Empty, "Adm.OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "Adm.OrdenDeProduccion.StatusOp", (int)eTipoStatusOrdenProduccion.Cerrada);
            if (valCantidadAImprimir == eCantidadAImprimir.One) {
                vSQLWhere = vSqlUtil.SqlValueWithAnd(vSQLWhere, "dbo.ArticuloInventario.Codigo", valCodigoInventario);
            }
            vSQLWhere = vSqlUtil.SqlValueWithAnd(vSQLWhere, "Adm.OrdenDeProduccion.Codigo", valCodigoOrden);
            //if (valGeneradoPor == eGeneradoPor.Orden) {
            //} else if (valGeneradoPor == eGeneradoPor.Fecha) {
            //    vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "Adm.OrdenDeProduccion.FechaFinalizacion", valFechaDesde, valFechaHasta);
            //}

            /* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
            string vSqlCambioDelDia;
            string vSqlCambioMasCercano;
            string vSqlCambioOriginal = "OrdenDeProduccion.CambioCostoProduccion";
            string vSqlMontoSubTotal = "(SALIDAS.MontoSubTotal)";
            string vSqlMontoCostoUnitario = "(SALIDAS.CostoUnitario)";
            string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
            string vCodigoMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            string vSqlCambio = vSqlCambioOriginal;
            if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
                if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
                    vSqlCambio = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = OrdenDeProduccion.CodigoMonedaCostoProduccion AND FechaDeVigencia <= " + vSqlUtil.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
                } else {
                    vSqlCambio = vSqlCambioOriginal;
                }
                vSqlMontoSubTotal = vSqlUtil.RoundToNDecimals(vSqlMontoSubTotal + " * " + vSqlCambio, 8);
                vSqlMontoCostoUnitario = vSqlUtil.RoundToNDecimals(vSqlMontoCostoUnitario + " * " + vSqlCambio, 8);
            } else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
                vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + vSqlUtil.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + vSqlUtil.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
                vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + vSqlUtil.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= OrdenDeProduccion.FechaFinalizacion ORDER BY FechaDeVigencia DESC), 1)";
                if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
                    vSqlCambio = vSqlCambioDelDia;
                } else {
                    vSqlCambio = vSqlCambioMasCercano;
                }
                vSqlCambio = vSqlUtil.IIF("OrdenDeProduccion.CodigoMonedaCostoProduccion = " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
                vSqlMontoSubTotal = vSqlUtil.RoundToNDecimals(vSqlMontoSubTotal + " / " + vSqlCambio, 8);
                vSqlMontoCostoUnitario = vSqlUtil.RoundToNDecimals(vSqlMontoCostoUnitario + " / " + vSqlCambio, 8);
            } else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
            }
            /* FIN */

            vSql.AppendLine("SELECT");
            vSql.AppendLine("Adm.OrdenDeProduccion.ConsecutivoCompania,");
            vSql.AppendLine("Adm.OrdenDeProduccion.Codigo,");
            vSql.AppendLine("dbo.ArticuloInventario.Codigo + ' - ' + dbo.ArticuloInventario.Descripcion  AS ArticuloInventario,");
            vSql.AppendLine("Adm.OrdenDeProduccion.FechaFinalizacion,");
            vSql.AppendLine("SALIDAS.CantidadProducida,");
            vSql.AppendLine(" (CASE WHEN OrdenDeProduccion.CodigoMonedaCostoProduccion = " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal) + " THEN ISNULL((SELECT TOP 1 Nombre FROM Moneda WHERE Codigo = " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal) + "), " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal) + ") ELSE ISNULL((SELECT TOP 1 Nombre FROM Moneda WHERE Codigo = OrdenDeProduccion.CodigoMonedaCostoProduccion), " + vSqlUtil.ToSqlValue(vCodigoMonedaExtranjera) + " )  END) AS Moneda,");
            vSql.AppendLine(" " + vSqlCambio + " AS Cambio, ");
            vSql.AppendLine(" " + vSqlMontoCostoUnitario + " AS CostoUnitario, ");
            vSql.AppendLine(" " + vSqlMontoSubTotal + " AS MontoSubTotal ");
            vSql.AppendLine("FROM");
            vSql.AppendLine("Adm.OrdenDeProduccion INNER JOIN");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo AS SALIDAS ON Adm.OrdenDeProduccion.ConsecutivoCompania = SALIDAS.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccion.Consecutivo = SALIDAS.ConsecutivoOrdenDeProduccion INNER JOIN");
            vSql.AppendLine("ArticuloInventario ON SALIDAS.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND");
            vSql.AppendLine("SALIDAS.CodigoArticulo = ArticuloInventario.Codigo");
            vSql.AppendLine(vSqlUtil.WhereSql(vSQLWhere));
            return vSql.ToString();
        }

        public string SqlCostoMatServUtilizadosEnProduccionInv(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoOrden, eGeneradoPor valGeneradoPor, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = string.Empty;

            vSQLWhere = vSqlUtil.SqlIntValueWithAnd(string.Empty, "Orden.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "Orden.StatusOp", (int)eTipoStatusOrdenProduccion.Cerrada);
            vSQLWhere = vSqlUtil.SqlValueWithAnd(vSQLWhere, "Orden.Codigo", valCodigoOrden);

            string vSQLWhereCTE = string.Empty;
            vSQLWhereCTE = vSqlUtil.SqlIntValueWithAnd(string.Empty, "Orden.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhereCTE = vSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "Orden.StatusOp", (int)eTipoStatusOrdenProduccion.Cerrada);
            vSQLWhereCTE = vSqlUtil.SqlValueWithAnd(vSQLWhere, "Orden.Codigo", valCodigoOrden);

            //if (valGeneradoPor == eGeneradoPor.Orden) {
            //} else if (valGeneradoPor == eGeneradoPor.Fecha) {
            //    vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "Orden.FechaFinalizacion", valFechaDesde, valFechaHasta);
            //}

            /* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
            string vSqlCambioDelDia;
            string vSqlCambioMasCercano;
            string vSqlCambioOriginal = "Orden.CambioCostoProduccion";
            string vSqlMontoSubTotal = "(Insumos.MontoSubtotal)";
            string vSqlMontoCostoUnitarioSalida = "(Salidas.CostoUnitario)";
            string vSumCostoTotalOP = "(SELECT SumCosto FROM CTE_TotalCosto)";
            string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
            string vCodigoMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            string vSqlCambio = vSqlCambioOriginal;
            if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
                if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
                    vSqlCambio = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = Orden.CodigoMonedaCostoProduccion AND FechaDeVigencia <= " + vSqlUtil.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
                } else {
                    vSqlCambio = vSqlCambioOriginal;
                }
                vSqlMontoSubTotal = vSqlUtil.RoundToNDecimals(vSqlMontoSubTotal + " * " + vSqlCambio, 8);
                vSqlMontoCostoUnitarioSalida = vSqlUtil.RoundToNDecimals(vSqlMontoCostoUnitarioSalida + " * " + vSqlCambio, 8);
            } else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
                vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + vSqlUtil.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + vSqlUtil.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
                vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + vSqlUtil.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= Orden.FechaFinalizacion ORDER BY FechaDeVigencia DESC), 1)";
                if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
                    vSqlCambio = vSqlCambioDelDia;
                } else {
                    vSqlCambio = vSqlCambioMasCercano;
                }
                vSqlCambio = vSqlUtil.IIF("Orden.CodigoMonedaCostoProduccion = " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
                vSqlMontoSubTotal = vSqlUtil.RoundToNDecimals(vSqlMontoSubTotal + " / " + vSqlCambio, 8);
                vSqlMontoCostoUnitarioSalida = vSqlUtil.RoundToNDecimals(vSqlMontoCostoUnitarioSalida + " / " + vSqlCambio, 8);
            } else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
            }
            /* FIN */

            vSql.AppendLine(";WITH CTE_TotalCosto AS ( ");
            vSql.AppendLine("SELECT ISNULL(SUM(Insumos.MontoSubtotal), 0) AS SumCosto");
            vSql.AppendLine("FROM Adm.OrdenDeProduccion AS Orden INNER JOIN Adm.OrdenDeProduccionDetalleMateriales AS Insumos");
            vSql.AppendLine("ON Orden.ConsecutivoCompania = Insumos.ConsecutivoCompania AND Orden.Consecutivo = Insumos.ConsecutivoOrdenDeProduccion");
            vSql.AppendLine(vSqlUtil.WhereSql(vSQLWhereCTE) + ")");
            //Orden
            vSql.AppendLine("SELECT Orden.Codigo AS CodigoOrden, ");
            vSql.AppendLine("Orden.Descripcion AS DescripcionOrden, ");
            vSql.AppendLine(" Orden.CodigoMonedaCostoProduccion, ");
            vSql.AppendLine("(CASE WHEN Orden.CodigoMonedaCostoProduccion = " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal) + " THEN ISNULL((SELECT TOP 1 Nombre FROM Moneda WHERE Codigo = " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal) + "), " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal) + ") ELSE ISNULL((SELECT TOP 1 Nombre FROM Moneda WHERE Codigo = Orden.CodigoMonedaCostoProduccion), " + vSqlUtil.ToSqlValue(vCodigoMonedaExtranjera) + " )  END) AS Moneda, ");
            vSql.AppendLine(vSqlCambio + " AS Cambio, ");
            vSql.AppendLine("Orden.FechaInicio, ");
            vSql.AppendLine("Orden.FechaFinalizacion, ");
            vSql.AppendLine(vSumCostoTotalOP + " AS CostoTotalOP, ");
            //Salidas
            vSql.AppendLine("Salidas.Consecutivo AS ConsecutivoSalida, ");
            vSql.AppendLine("Salidas.CodigoArticulo AS CodigoArticuloProducido, ");
            vSql.AppendLine("ArticuloInventario.Descripcion AS DescripcionArticuloProducido, ");
            vSql.AppendLine("Salidas.CantidadProducida, ");
            vSql.AppendLine(vSqlMontoCostoUnitarioSalida + " AS CostoUnitario, ");
            //Insumos
            vSql.AppendLine("Insumos.CodigoArticulo AS ArticuloComsumido, ");
            vSql.AppendLine("Insumos.CantidadConsumida, ");
            vSql.AppendLine(vSqlMontoSubTotal + " AS MontoSubtotal, ");
            vSql.AppendLine("Salidas.PorcentajeCostoCierre, ");
            vSql.AppendLine("(CASE WHEN Salidas.CantidadProducida = 0 THEN 0 ELSE ROUND(" + vSqlMontoSubTotal + " * (Salidas.PorcentajeCostoCierre/100), 2) END) AS SubTotal ");

            vSql.AppendLine("FROM Adm.OrdenDeProduccionDetalleArticulo AS Salidas INNER JOIN Adm.OrdenDeProduccion AS Orden ");
            vSql.AppendLine("ON Salidas.ConsecutivoCompania = Orden.ConsecutivoCompania AND Salidas.ConsecutivoOrdenDeProduccion = Orden.Consecutivo ");
            vSql.AppendLine("INNER JOIN ArticuloInventario ON Salidas.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND Salidas.CodigoArticulo = ArticuloInventario.Codigo ");
            vSql.AppendLine("RIGHT JOIN Adm.OrdenDeProduccionDetalleMateriales AS Insumos ON Salidas.ConsecutivoCompania = Insumos.ConsecutivoCompania AND Salidas.ConsecutivoOrdenDeProduccion = Insumos.ConsecutivoOrdenDeProduccion ");
            vSql.AppendLine(vSqlUtil.WhereSql(vSQLWhere));
            vSql.AppendLine("ORDER BY Orden.Consecutivo, Salidas.Consecutivo, Insumos.Consecutivo");
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
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CantidadSolicitada - Adm.OrdenDeProduccionDetalleArticulo.CantidadProducida AS Diferencia,");
            vSql.AppendLine("Adm.OrdenDeProduccion.MotivoDeAnulacion");
            vSql.AppendLine("FROM");
            vSql.AppendLine("Adm.OrdenDeProduccion INNER JOIN");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo ON Adm.OrdenDeProduccion.ConsecutivoCompania = Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccion.Consecutivo = Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion INNER JOIN");
            vSql.AppendLine("dbo.ArticuloInventario ON Adm.OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania AND");
            vSql.AppendLine("Adm.OrdenDeProduccionDetalleArticulo.CodigoArticulo = dbo.ArticuloInventario.Codigo  INNER JOIN");
            vSql.AppendLine("Adm.Gv_EnumTipoStatusOrdenProduccion ON Adm.OrdenDeProduccion.StatusOp = Adm.Gv_EnumTipoStatusOrdenProduccion.DbValue COLLATE Modern_Spanish_CI_AS");

            vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "Adm.OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "Adm.OrdenDeProduccion.StatusOp", (int)valEstatus);

            if (valGeneradoPor == eGeneradoPor.Orden) {
                vSQLWhere = vSqlUtil.SqlValueWithAnd(vSQLWhere, "Adm.OrdenDeProduccion.Codigo", valCodigoOrden);
            } else {

                if (valEstatus == eTipoStatusOrdenProduccion.Ingresada) {
                    vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "Adm.OrdenDeProduccion.FechaCreacion", valFechaInicial, valFechaFinal);
                } else if (valEstatus == eTipoStatusOrdenProduccion.Iniciada) {
                    vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "Adm.OrdenDeProduccion.FechaInicio", valFechaInicial, valFechaFinal);
                } else if (valEstatus == eTipoStatusOrdenProduccion.Anulada || valEstatus == eTipoStatusOrdenProduccion.Cerrada) {
                    vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "Adm.OrdenDeProduccion.FechaFinalizacion", valFechaInicial, valFechaFinal);
                }
            }
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSQLWhere);
            }
            return vSql.ToString();
        }

        public string SqlDetalleDeCostoDeProduccion(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eSeleccionarOrdenPor valSeleccionarOrdenPor, int valConsecutivoOrden, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere;
            vSQLWhere = vSqlUtil.SqlIntValueWithAnd(string.Empty, "OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "OrdenDeProduccion.StatusOp", (int)eTipoStatusOrdenProduccion.Cerrada);
            if (valSeleccionarOrdenPor == eSeleccionarOrdenPor.NumeroDeOrden) {
                vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "OrdenDeProduccion.Consecutivo", valConsecutivoOrden);
            } else if (valSeleccionarOrdenPor == eSeleccionarOrdenPor.FechaDeInicio) {
                vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "OrdenDeProduccion.FechaInicio", valFechaInicial, valFechaFinal);
            } else if (valSeleccionarOrdenPor == eSeleccionarOrdenPor.FechaDeFinalizacion) {
                vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "OrdenDeProduccion.FechaFinalizacion", valFechaInicial, valFechaFinal);
            }

            /* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
            string vSqlCambioDelDia;
            string vSqlCambioMasCercano;
            string vSqlCambioOriginal = "OrdenDeProduccion.CambioCostoProduccion";
            string vSqlMontoSubTotal = "(Insumos.MontoSubtotal)";
            string vSqlMontoCostoUnitario = "(Insumos.CostoUnitarioArticuloInventario)";
            string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
            string vCodigoMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            string vSqlCambio = vSqlCambioOriginal;
            if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
                if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
                    vSqlCambio = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = OrdenDeProduccion.CodigoMonedaCostoProduccion AND FechaDeVigencia <= " + vSqlUtil.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
                } else {
                    vSqlCambio = vSqlCambioOriginal;
                }
                vSqlMontoSubTotal = vSqlUtil.RoundToNDecimals(vSqlMontoSubTotal + " * " + vSqlCambio, 8);
                vSqlMontoCostoUnitario = vSqlUtil.RoundToNDecimals(vSqlMontoCostoUnitario + " * " + vSqlCambio, 8);
            } else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
                vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + vSqlUtil.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + vSqlUtil.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
                vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + vSqlUtil.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= OrdenDeProduccion.FechaFinalizacion ORDER BY FechaDeVigencia DESC), 1)";
                if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
                    vSqlCambio = vSqlCambioDelDia;
                } else {
                    vSqlCambio = vSqlCambioMasCercano;
                }
                vSqlCambio = vSqlUtil.IIF("OrdenDeProduccion.CodigoMonedaCostoProduccion = " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
                vSqlMontoSubTotal = vSqlUtil.RoundToNDecimals(vSqlMontoSubTotal + " / " + vSqlCambio, 8);
                vSqlMontoCostoUnitario = vSqlUtil.RoundToNDecimals(vSqlMontoCostoUnitario + " / " + vSqlCambio, 8);
            } else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
            }
            /* FIN */

            vSql.AppendLine("SELECT");
            vSql.AppendLine(" OrdenDeProduccion.Codigo AS CodigoOrden,");
            vSql.AppendLine(" Insumos.Consecutivo AS ConsecutivoInsumos,");
            vSql.AppendLine(" OrdenDeProduccion.Codigo + ' - ' + OrdenDeProduccion.Descripcion AS OrdenCodigoDescripcion,");
            vSql.AppendLine(" OrdenDeProduccion.StatusOp AS Estatus,");
            vSql.AppendLine(" (CASE WHEN OrdenDeProduccion.CodigoMonedaCostoProduccion = " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal) + " THEN ISNULL((SELECT TOP 1 Nombre FROM Moneda WHERE Codigo = " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal) + "), " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal) + ") ELSE ISNULL((SELECT TOP 1 Nombre FROM Moneda WHERE Codigo = OrdenDeProduccion.CodigoMonedaCostoProduccion), " + vSqlUtil.ToSqlValue(vCodigoMonedaExtranjera) + " )  END) AS Moneda,");
            vSql.AppendLine(" " + vSqlCambio + " AS Cambio, ");
            vSql.AppendLine(" OrdenDeProduccion.FechaInicio,");
            vSql.AppendLine(" OrdenDeProduccion.FechaFinalizacion,");
            vSql.AppendLine(" (CASE WHEN OrdenDeProduccion.CostoTerminadoCalculadoAPartirDe = '0' THEN 'Costos expresados en ' + ISNULL((SELECT TOP 1 Nombre FROM Moneda WHERE Codigo = " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal) + "), " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal) + ") ELSE 'Costo expresados en ' + ISNULL((SELECT TOP 1 Nombre FROM Moneda WHERE Codigo = OrdenDeProduccion.CodigoMonedaCostoProduccion), " + vSqlUtil.ToSqlValue(vCodigoMonedaExtranjera) + " )  END) AS FormaDeCalculoDelCosto,");
            vSql.AppendLine(" OrdenDeProduccion.Observacion,");
            vSql.AppendLine(" AlmacenInsumos.Codigo +  ' - ' + AlmacenInsumos.NombreAlmacen AS AlmacenInsumos,");
            vSql.AppendLine(" AlmacenSalidas.Codigo +  ' - ' + AlmacenInsumos.NombreAlmacen AS AlmacenSalida,");
            vSql.AppendLine(" Insumos.CodigoArticulo + ' - ' + ArticuloInventario.Descripcion AS ArticuloInventarioInsumo,");
            vSql.AppendLine(" ArticuloInventario.UnidadDeVenta AS Unidad,");
            vSql.AppendLine(" Insumos.CantidadReservadaInventario AS CantidadEstimada,");
            vSql.AppendLine(" Insumos.CantidadConsumida,");
            vSql.AppendLine(" " + vSqlMontoCostoUnitario + " AS CostoUnitarioMatServ, ");
            vSql.AppendLine(" " + vSqlMontoSubTotal + " AS MontoTotalConsumo ");
            vSql.AppendLine("FROM Adm.OrdenDeProduccion INNER JOIN Adm.OrdenDeProduccionDetalleMateriales AS Insumos ON OrdenDeProduccion.ConsecutivoCompania = Insumos.ConsecutivoCompania AND OrdenDeProduccion.Consecutivo = Insumos.ConsecutivoOrdenDeProduccion");
            vSql.AppendLine("INNER JOIN Saw.Almacen AS AlmacenInsumos ON OrdenDeProduccion.ConsecutivoCompania = AlmacenInsumos.ConsecutivoCompania AND OrdenDeProduccion.ConsecutivoAlmacenMateriales = AlmacenInsumos.Consecutivo");
            vSql.AppendLine("INNER JOIN Saw.Almacen AS AlmacenSalidas ON OrdenDeProduccion.ConsecutivoCompania = AlmacenSalidas.ConsecutivoCompania AND OrdenDeProduccion.ConsecutivoAlmacenProductoTerminado = AlmacenSalidas.Consecutivo");
            vSql.AppendLine("INNER JOIN ArticuloInventario ON Insumos.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND Insumos.CodigoArticulo = ArticuloInventario.Codigo");
            vSql.AppendLine(vSqlUtil.WhereSql(vSQLWhere));
            vSql.AppendLine("ORDER BY CodigoOrden, ConsecutivoInsumos");
            return vSql.ToString();
        }

        public string SqlDetalleDeCostoDeProduccionSalida(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eSeleccionarOrdenPor valSeleccionarOrdenPor, int valConsecutivoOrden, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valCodigoMoneda, string valNombreMoneda) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere;
            vSQLWhere = vSqlUtil.SqlIntValueWithAnd(string.Empty, "OrdenDeProduccion.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "OrdenDeProduccion.StatusOp", (int)eTipoStatusOrdenProduccion.Cerrada);
            if (valSeleccionarOrdenPor == eSeleccionarOrdenPor.NumeroDeOrden) {
                vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "OrdenDeProduccion.Consecutivo", valConsecutivoOrden);
            } else if (valSeleccionarOrdenPor == eSeleccionarOrdenPor.FechaDeInicio) {
                vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "OrdenDeProduccion.FechaInicio", valFechaInicial, valFechaFinal);
            } else if (valSeleccionarOrdenPor == eSeleccionarOrdenPor.FechaDeFinalizacion) {
                vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "OrdenDeProduccion.FechaFinalizacion", valFechaInicial, valFechaFinal);
            }

            /* INICIO: Manejo para multimoneda: Moneda Local // Moneda Extranjera Original y Moneda Local en Moneda Extranjera // Moneda Original */
            string vSqlCambioDelDia;
            string vSqlCambioMasCercano;
            string vSqlCambioOriginal = "OrdenDeProduccion.CambioCostoProduccion";
            string vSqlMontoSubTotalSalida = "(Salidas.MontoSubTotal)";
            string vSqlMontoCostoUnitarioSalida = "(Salidas.CostoUnitario)";
            string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
            string vSqlCambio = vSqlCambioOriginal;
            if (valMonedaDelInforme == eMonedaDelInformeMM.EnBolivares) {
                if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
                    vSqlCambio = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = OrdenDeProduccion.CodigoMonedaCostoProduccion AND FechaDeVigencia <= " + vSqlUtil.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
                } else {
                    vSqlCambio = vSqlCambioOriginal;
                }
                vSqlMontoSubTotalSalida = vSqlUtil.RoundToNDecimals(vSqlMontoSubTotalSalida + " * " + vSqlCambio, 8);
                vSqlMontoCostoUnitarioSalida = vSqlUtil.RoundToNDecimals(vSqlMontoCostoUnitarioSalida + " * " + vSqlCambio, 8);
            } else if (valMonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa) {
                vSqlCambioDelDia = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + vSqlUtil.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= " + vSqlUtil.ToSqlValue(LibDate.Today()) + " ORDER BY FechaDeVigencia DESC), 1)";
                vSqlCambioMasCercano = "ISNULL((SELECT TOP 1 CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = " + vSqlUtil.ToSqlValue(valCodigoMoneda) + " AND FechaDeVigencia <= OrdenDeProduccion.FechaFinalizacion ORDER BY FechaDeVigencia DESC), 1)";
                if (valTasaDeCambio == eTasaDeCambioParaImpresion.DelDia) {
                    vSqlCambio = vSqlCambioDelDia;
                } else {
                    vSqlCambio = vSqlCambioMasCercano;
                }
                vSqlCambio = vSqlUtil.IIF("OrdenDeProduccion.CodigoMonedaCostoProduccion = " + vSqlUtil.ToSqlValue(vCodigoMonedaLocal), vSqlCambio, " 1 ", true);
                vSqlMontoSubTotalSalida = vSqlUtil.RoundToNDecimals(vSqlMontoSubTotalSalida + " / " + vSqlCambio, 8);
                vSqlMontoCostoUnitarioSalida = vSqlUtil.RoundToNDecimals(vSqlMontoCostoUnitarioSalida + " / " + vSqlCambio, 8);
            } else if (valMonedaDelInforme == eMonedaDelInformeMM.EnMonedaOriginal) {
            }
            /* FIN */

            vSql.AppendLine("SELECT");
            vSql.AppendLine(" OrdenDeProduccion.Codigo AS CodigoOrden,");
            vSql.AppendLine(" Salidas.Consecutivo AS ConsecutivoSalidas,");
            vSql.AppendLine(" Salidas.CodigoArticulo + ' - ' + ArticuloInventario.Descripcion AS DescripcionArticuloSalida,");
            vSql.AppendLine(" ArticuloInventario.UnidadDeVenta AS Unidad,");
            vSql.AppendLine(" Salidas.CantidadSolicitada AS CantidadAProducir,");
            vSql.AppendLine(" Salidas.CantidadProducida,");
            vSql.AppendLine(" Salidas.PorcentajeCostoCierre AS PorcentajeCosto,");
            vSql.AppendLine(" " + vSqlMontoCostoUnitarioSalida + " AS CostoUnitario, ");
            vSql.AppendLine(" " + vSqlMontoSubTotalSalida + " AS CostoTotal ");
            vSql.AppendLine("FROM Adm.OrdenDeProduccion INNER JOIN Adm.OrdenDeProduccionDetalleArticulo AS Salidas ON OrdenDeProduccion.ConsecutivoCompania = Salidas.ConsecutivoCompania AND OrdenDeProduccion.Consecutivo = Salidas.ConsecutivoOrdenDeProduccion");
            vSql.AppendLine("INNER JOIN ArticuloInventario AS ArticuloInventario ON Salidas.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND Salidas.CodigoArticulo = ArticuloInventario.Codigo ");
            vSql.AppendLine(vSqlUtil.WhereSql(vSQLWhere));
            vSql.AppendLine("ORDER BY CodigoOrden, ConsecutivoSalidas");
            return vSql.ToString();
        }
        #endregion //Metodos Generados

    } //End of class clsOrdenDeProduccionSql

} //End of namespace Galac.Adm.Brl. GestionProduccion

