using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.GestionCompras;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Brl.GestionCompras.Reportes {
    public class clsOrdenDeCompraSql {
        #region Metodos Generados
        public string SqlCompra(int valConsecutivoCompania, int ConsecutivoCompra) {
            StringBuilder vSql = new StringBuilder();            
            decimal vAlicuotaIva=LibImportData.ToDec(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","AlicuotaIVAdiv100"));            
            QAdvSql vUtilSql = new QAdvSql("");
            string vSQLWhere = "";
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                vSql.AppendLine("SELECT Adm.Gv_OrdenDeCompra_B1.Numero AS Numero");
            }  else {
                vSql.AppendLine("SELECT Adm.Gv_OrdenDeCompra_B1.Serie + CASE LEN(Adm.Gv_OrdenDeCompra_B1.Serie) WHEN 0 THEN '' ELSE '-' END + Adm.Gv_OrdenDeCompra_B1.Numero AS Numero");
            }
            vSql.AppendLine(" , Adm.Gv_OrdenDeCompra_B1.Fecha, Adm.Gv_OrdenDeCompra_B1.CodigoProveedor, Adm.Gv_OrdenDeCompra_B1.NombreProveedor");
            vSql.AppendLine(" , Adm.Proveedor.Direccion , Adm.Proveedor.NumeroRIF, Adm.Proveedor.NumeroNIT, Adm.Proveedor.Telefonos ");
            vSql.AppendLine(" , Adm.Gv_OrdenDeCompra_B1.TotalCompra, Adm.Gv_OrdenDeCompra_B1.Comentarios  ");
            vSql.AppendLine(" , Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.CodigoArticulo, Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.DescripcionArticulo As Descripcion,  Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.Cantidad, Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.CostoUnitario, Gv_ArticuloInventario_B2.UnidadDeVenta,  ( Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.Cantidad * Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.CostoUnitario) AS SubTotal");
            vSql.AppendLine(" , CASE WHEN(Gv_ArticuloInventario_B2.AlicuotaIva = '1') THEN Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.CostoUnitario * "+ vUtilSql.ToSqlValue(vAlicuotaIva) + " * Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.Cantidad END AS IVA ");
            vSql.AppendLine(" , CASE WHEN(Gv_ArticuloInventario_B2.AlicuotaIva = '1') THEN (Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.CostoUnitario * " + vUtilSql.ToSqlValue(vAlicuotaIva) + " * Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.Cantidad)  +  ( Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.CostoUnitario * Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.Cantidad) ELSE   ( Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.CostoUnitario * Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.Cantidad) END AS SubTotalOC");
            vSql.AppendLine(" , Adm.Gv_OrdenDeCompra_B1.DescripcionCondicionesDePago, Adm.Gv_OrdenDeCompra_B1.CondicionesDeImportacionStr AS CondicionesDeImportacion, Adm.Gv_OrdenDeCompra_B1.CondicionesDeEntrega");
            vSql.AppendLine(" , Adm.Gv_OrdenDeCompra_B1.Moneda");
            vSql.AppendLine(" FROM Adm.Gv_OrdenDeCompra_B1 INNER JOIN Adm.ProvEedor ON Adm.Gv_OrdenDeCompra_B1.ConsecutivoCompania = Adm.Proveedor.ConsecutivoCompania AND Adm.Gv_OrdenDeCompra_B1.ConsecutivoProveedor = Adm.Proveedor.Consecutivo ");
            vSql.AppendLine(" INNER JOIN Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1 ON Adm.Gv_OrdenDeCompra_B1.ConsecutivoCompania = Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.ConsecutivoCompania AND Adm.Gv_OrdenDeCompra_B1.Consecutivo = Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.ConsecutivoOrdenDeCompra ");
            vSql.AppendLine(" INNER JOIN Gv_ArticuloInventario_B2 ON Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.ConsecutivoCompania = Gv_ArticuloInventario_B2.ConsecutivoCompania  AND Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1.CodigoArticulo = dbo.Gv_ArticuloInventario_B2.CodigoCompuesto ");
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "Adm.Gv_OrdenDeCompra_B1.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "Adm.Gv_OrdenDeCompra_B1.Consecutivo", ConsecutivoCompra);
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSQLWhere);
            }

            return vSql.ToString();
        }

        public string ContruirSQLOrdenDeCompras(int valConsecutivoCompania, DateTime FechaInicial, DateTime FechaFinal, eStatusDeOrdenDeCompra StatusDeOrdenDeCompra) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vUtilSql = new QAdvSql("");
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("Adm.OrdenDeCompra.Moneda");
            vSql.AppendLine(",Adm.OrdenDeCompra.Numero");
            vSql.AppendLine(",Adm.OrdenDeCompra.Fecha");
            vSql.AppendLine(",Adm.Proveedor.NombreProveedor");
            vSql.AppendLine(",Adm.OrdenDeCompra.TotalCompra");
            vSql.AppendLine(",Adm.OrdenDeCompra.CambioABolivares");
            vSql.AppendLine(",Adm.OrdenDeCompra.TotalCompra*Adm.OrdenDeCompra.CambioABolivares AS 'Total al Cambio' ");
            vSql.AppendLine(",Adm.OrdenDeCompraDetalleArticuloInventario.CodigoArticulo ");
            vSql.AppendLine(",IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion ");
            vSql.AppendLine(",Adm.OrdenDeCompraDetalleArticuloInventario.Cantidad ");
            vSql.AppendLine(",Adm.OrdenDeCompraDetalleArticuloInventario.CantidadRecibida ");
            vSql.AppendLine(",Adm.OrdenDeCompraDetalleArticuloInventario.CostoUnitario ");
            vSql.AppendLine("FROM ");
            vSql.AppendLine("Adm.OrdenDeCompra ");
            vSql.AppendLine("INNER JOIN Adm.Proveedor ");
            vSql.AppendLine("ON Adm.OrdenDeCompra.ConsecutivoCompania = Adm.Proveedor.ConsecutivoCompania ");
            vSql.AppendLine("AND Adm.OrdenDeCompra.ConsecutivoProveedor = Adm.Proveedor.Consecutivo ");
            vSql.AppendLine("INNER JOIN Adm.OrdenDeCompraDetalleArticuloInventario ");
            vSql.AppendLine("ON Adm.OrdenDeCompra.ConsecutivoCompania = Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine("AND Adm.OrdenDeCompra.Consecutivo = Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoOrdenDeCompra ");
            vSql.AppendLine("INNER JOIN IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1 ");
            vSql.AppendLine("ON IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine("AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo  = Adm.OrdenDeCompraDetalleArticuloInventario.CodigoArticulo ");
            vSql.AppendLine(" WHERE ");
            vSql.AppendLine(vUtilSql.SqlDateValueBetween(null,"Adm.OrdenDeCompra.Fecha", FechaInicial, FechaFinal));
            vSql.AppendLine(" AND ");
            if (StatusDeOrdenDeCompra == eStatusDeOrdenDeCompra.CompletamenteProcesada) {
                vSql.AppendLine(" ((select (sum(Adm.OrdenDeCompraDetalleArticuloInventario.Cantidad)- sum(Adm.OrdenDeCompraDetalleArticuloInventario.CantidadRecibida)) ");
               vSql.AppendLine(" FROM Adm.OrdenDeCompraDetalleArticuloInventario ");
               vSql.AppendLine(" WHERE ");
               vSql.AppendLine("Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoOrdenDeCompra= Adm.OrdenDeCompra.Consecutivo ");
               vSql.AppendLine("AND Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoCompania = Adm.OrdenDeCompra.ConsecutivoCompania ");
               vSql.AppendLine(") = 0 ) ");
            } else if (StatusDeOrdenDeCompra == eStatusDeOrdenDeCompra.ParcialmenteProcesada) {
               vSql.AppendLine(" ((select (sum(Adm.OrdenDeCompraDetalleArticuloInventario.Cantidad)- sum(Adm.OrdenDeCompraDetalleArticuloInventario.CantidadRecibida)) ");
               vSql.AppendLine("FROM Adm.OrdenDeCompraDetalleArticuloInventario ");
               vSql.AppendLine(" WHERE ");
               vSql.AppendLine("Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoOrdenDeCompra= Adm.OrdenDeCompra.Consecutivo ");
               vSql.AppendLine("AND Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoCompania = Adm.OrdenDeCompra.ConsecutivoCompania ");
               vSql.AppendLine(") > 0 ) ");
               vSql.AppendLine("AND ((select sum(Adm.OrdenDeCompraDetalleArticuloInventario.CantidadRecibida) ");
               vSql.AppendLine("FROM Adm.OrdenDeCompraDetalleArticuloInventario ");
               vSql.AppendLine("WHERE ");
               vSql.AppendLine("Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoOrdenDeCompra= Adm.OrdenDeCompra.Consecutivo ");
               vSql.AppendLine("AND Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoCompania = Adm.OrdenDeCompra.ConsecutivoCompania ");
               vSql.AppendLine(") <> 0 ) ");
            } else if (StatusDeOrdenDeCompra == eStatusDeOrdenDeCompra.SinProcesar) {
               vSql.AppendLine("((SELECT SUM(Adm.OrdenDeCompraDetalleArticuloInventario.CantidadRecibida) ");
               vSql.AppendLine("FROM Adm.OrdenDeCompraDetalleArticuloInventario ");
               vSql.AppendLine(" WHERE ");
               vSql.AppendLine("Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoOrdenDeCompra= Adm.OrdenDeCompra.Consecutivo ");
               vSql.AppendLine("AND Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoCompania = Adm.OrdenDeCompra.ConsecutivoCompania ");
               vSql.AppendLine(") = 0 )");
            }
            vSql.AppendLine(" AND (Adm.OrdenDeCompra.ConsecutivoCompania = " + valConsecutivoCompania + ") ");
            vSql.AppendLine(" ORDER BY ");
            vSql.AppendLine(" Adm.OrdenDeCompra.Moneda, Adm.OrdenDeCompra.fecha, Adm.OrdenDeCompra.Numero ");
            return vSql.ToString();
        }

        public string ConstruirSQLCotizacionOrdendeCompra(int valConsecutivoCompania, string NumeroCotizacion) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vUtilSql = new QAdvSql("");
            vSql.AppendLine(" SELECT");
            vSql.AppendLine(" 	0 AS Orden");
            vSql.AppendLine(" 	, dbo.cotizacion.Numero AS NumeroCotizacionParaAgrupamiento");
            vSql.AppendLine(" 	, dbo.cotizacion.Numero AS NumeroDocumento ");
            vSql.AppendLine(" 	, dbo.cotizacion.CambioABolivares  AS TasaDeCambio");
            vSql.AppendLine(" 	, dbo.cotizacion.Fecha AS FechaDocumento");
            vSql.AppendLine(" 	, dbo.renglonCotizacion.CodigoArticulo As CodigoArticulo");
            vSql.AppendLine(" 	, dbo.renglonCotizacion.Descripcion AS DescripcionArticulo");
            vSql.AppendLine(" 	, dbo.renglonCotizacion.Cantidad * dbo.renglonCotizacion.PrecioSinIVA * dbo.cotizacion.CambioABolivares AS MontoArticuloEnML");
            vSql.AppendLine(" 	, CASE WHEN dbo.cotizacion.CambioABolivares = 1 THEN 0 ELSE dbo.renglonCotizacion.Cantidad * dbo.renglonCotizacion.PrecioSinIVA END  AS MontoArticuloEnME");
            vSql.AppendLine(" 	, 0 AS PorcentajeVenta");
            vSql.AppendLine(" 	, dbo.Cliente.Nombre AS NombreCliente");
            vSql.AppendLine(" FROM dbo.cotizacion ");
            vSql.AppendLine(" 	INNER JOIN dbo.renglonCotizacion ");
            vSql.AppendLine(" 		ON dbo.cotizacion.ConsecutivoCompania = dbo.renglonCotizacion.ConsecutivoCompania ");
            vSql.AppendLine(" 		AND dbo.cotizacion.Numero = dbo.renglonCotizacion.NumeroCotizacion");
            vSql.AppendLine("	INNER JOIN dbo.Cliente");
            vSql.AppendLine("		ON dbo.cotizacion.ConsecutivoCompania = dbo.Cliente.ConsecutivoCompania");
            vSql.AppendLine("		AND dbo.cotizacion.CodigoCliente  = dbo.Cliente.Codigo");
            vSql.AppendLine(" WHERE ");
            if (NumeroCotizacion != string.Empty) {
                vSql.AppendLine(" 	dbo.cotizacion.Numero = '" + NumeroCotizacion + "' AND");
            }
            vSql.AppendLine(" dbo.cotizacion.ConsecutivoCompania = " + valConsecutivoCompania);
            vSql.AppendLine(" UNION");
            vSql.AppendLine(" SELECT ");
            vSql.AppendLine(" 	1 AS Orden");
            vSql.AppendLine(" 	, dbo.cotizacion.Numero AS NumeroCotizacionParaAgrupamiento");
            vSql.AppendLine(" 	, Adm.OrdenDeCompra.Serie + '-' + Adm.OrdenDeCompra.Numero  AS NumeroDocumento ");
            vSql.AppendLine(" 	, dbo.cotizacion.CambioABolivares  AS TasaDeCambio");
            vSql.AppendLine(" 	, Adm.OrdenDeCompra.Fecha  AS FechaDocumento ");
            vSql.AppendLine(" 	, Adm.OrdenDeCompraDetalleArticuloInventario.CodigoArticulo  As CodigoArticulo");
            vSql.AppendLine(" 	, Adm.OrdenDeCompraDetalleArticuloInventario.DescripcionArticulo  AS DescripcionArticulo");
            vSql.AppendLine("   , CASE WHEN Adm.OrdenDeCompra.TipoDeCompra = '0' THEN ");
            vSql.AppendLine("	    CASE WHEN dbo.cotizacion.CambioABolivares <> 1 AND Adm.OrdenDeCompra.CodigoMoneda = dbo.cotizacion.CodigoMoneda THEN ");
            vSql.AppendLine("		    Adm.OrdenDeCompraDetalleArticuloInventario.Cantidad ");
            vSql.AppendLine("		    * Adm.OrdenDeCompraDetalleArticuloInventario.CostoUnitario ");
            vSql.AppendLine("		    *  dbo.cotizacion.CambioABolivares ");
            vSql.AppendLine("	    ELSE ");
            vSql.AppendLine("		    Adm.OrdenDeCompraDetalleArticuloInventario.Cantidad ");
            vSql.AppendLine("		    * Adm.OrdenDeCompraDetalleArticuloInventario.CostoUnitario ");
            vSql.AppendLine("	    END");
            vSql.AppendLine("	  ELSE");
            vSql.AppendLine("	  CASE WHEN dbo.cotizacion.CambioABolivares <> 1 THEN ");
            vSql.AppendLine("		Adm.OrdenDeCompraDetalleArticuloInventario.Cantidad ");
            vSql.AppendLine("		* Adm.OrdenDeCompraDetalleArticuloInventario.CostoUnitario ");
            vSql.AppendLine("		*  dbo.cotizacion.CambioABolivares ");
            vSql.AppendLine("	  ELSE ");
            vSql.AppendLine("		Adm.OrdenDeCompraDetalleArticuloInventario.Cantidad ");
            vSql.AppendLine("		* Adm.OrdenDeCompraDetalleArticuloInventario.CostoUnitario ");
            vSql.AppendLine("		* Adm.OrdenDeCompra.CambioABolivares END END AS MontoArticuloEnML ");
            vSql.AppendLine(" 	, CASE WHEN Adm.OrdenDeCompra.TipoDeCompra = '0' THEN 0 ELSE CASE WHEN dbo.cotizacion.CambioABolivares = 1 THEN 0 ELSE Adm.OrdenDeCompraDetalleArticuloInventario.Cantidad * Adm.OrdenDeCompraDetalleArticuloInventario.CostoUnitario END   END AS MontoArticuloEnME");
            vSql.AppendLine(" 	, ((Adm.OrdenDeCompraDetalleArticuloInventario.Cantidad * Adm.OrdenDeCompraDetalleArticuloInventario.CostoUnitario) /(cotizacion.TotalBaseImponible +  cotizacion.TotalMontoExento)) * 100");
            vSql.AppendLine(" 	, '' AS NombreCliente");
            vSql.AppendLine(" 	FROM Adm.OrdenDeCompra ");
            vSql.AppendLine(" 	INNER JOIN Adm.OrdenDeCompraDetalleArticuloInventario ");
            vSql.AppendLine(" 		ON Adm.OrdenDeCompra.ConsecutivoCompania = Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoCompania");
            vSql.AppendLine(" 		AND Adm.OrdenDeCompra.Consecutivo = Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoOrdenDeCompra");
            vSql.AppendLine(" 	INNER JOIN dbo.cotizacion");
            vSql.AppendLine(" 		ON Adm.OrdenDeCompra.ConsecutivoCompania = dbo.cotizacion.ConsecutivoCompania");
            vSql.AppendLine(" 		AND Adm.OrdenDeCompra.NumeroCotizacion  = dbo.cotizacion.Numero");  
            vSql.AppendLine(" WHERE");
            if (NumeroCotizacion != string.Empty) {
                vSql.AppendLine(" 	dbo.cotizacion.Numero = '" + NumeroCotizacion + "' AND");
            }
            vSql.AppendLine(" Adm.OrdenDeCompra.ConsecutivoCompania = " + valConsecutivoCompania);
            vSql.AppendLine(" ORDER BY NumeroCotizacionParaAgrupamiento, Orden, FechaDocumento, NumeroDocumento , MontoArticuloEnML");
            return vSql.ToString();
        }
        #endregion //Metodos Generados
    }
}

