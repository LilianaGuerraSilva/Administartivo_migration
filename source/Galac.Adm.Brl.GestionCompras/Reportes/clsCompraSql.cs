using System;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Comun.Ccl.SttDef;

namespace Galac.Adm.Brl.GestionCompras.Reportes {
    public class clsCompraSql
    {
        #region Metodos Generados
        public string SqlCompra(int valConsecutivoCompania, int ConsecutivoCompra)
        {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vUtilSql = new QAdvSql("");
            string vSQLWhere = "";

            vSql.AppendLine("SELECT Adm.Gv_Compra_B1.Serie + CASE LEN(Adm.Gv_Compra_B1.Serie) WHEN 0 THEN '' ELSE '-' END + Adm.Gv_Compra_B1.Numero AS Numero");
            vSql.AppendLine(" ,Adm.Gv_Compra_B1.StatusCompraStr, Adm.Gv_Compra_B1.Moneda, Adm.Gv_Compra_B1.Fecha, Adm.Gv_Compra_B1.CodigoProveedor, Adm.Gv_Compra_B1.NombreProveedor");
            vSql.AppendLine(" , Adm.Proveedor.Direccion , Adm.Proveedor.NumeroRIF, Adm.Proveedor.NumeroNIT, Adm.Proveedor.Telefonos ");
            vSql.AppendLine(" , Adm.Gv_Compra_B1.TotalOtrosGastos, Adm.Gv_Compra_B1.TotalCompra, Adm.Gv_Compra_B1.Comentarios  ");
            vSql.AppendLine(" , Adm.Gv_CompraDetalleArticuloInventario_B1.CodigoArticulo, dbo.Gv_ArticuloInventario_B2.Descripcion,  Adm.Gv_CompraDetalleArticuloInventario_B1.Cantidad, Adm.Gv_CompraDetalleArticuloInventario_B1.PrecioUnitario, dbo.Gv_ArticuloInventario_B2.UnidadDeVenta,  ( Adm.Gv_CompraDetalleArticuloInventario_B1.Cantidad * Adm.Gv_CompraDetalleArticuloInventario_B1.PrecioUnitario) AS SubTotal");
            vSql.AppendLine(" FROM Adm.Gv_Compra_B1 INNER JOIN Adm.ProvEedor ON Adm.Gv_Compra_B1.ConsecutivoCompania = Adm.Proveedor.ConsecutivoCompania AND Adm.Gv_Compra_B1.ConsecutivoProveedor = Adm.Proveedor.Consecutivo ");
            vSql.AppendLine(" INNER JOIN Adm.Gv_CompraDetalleArticuloInventario_B1 ON Adm.Gv_Compra_B1.ConsecutivoCompania = Adm.Gv_CompraDetalleArticuloInventario_B1.ConsecutivoCompania AND Adm.Gv_Compra_B1.Consecutivo = Adm.Gv_CompraDetalleArticuloInventario_B1.ConsecutivoCompra ");
            vSql.AppendLine(" INNER JOIN dbo.Gv_ArticuloInventario_B2 ON Adm.Gv_CompraDetalleArticuloInventario_B1.ConsecutivoCompania = dbo.Gv_ArticuloInventario_B2.ConsecutivoCompania  AND Adm.Gv_CompraDetalleArticuloInventario_B1.CodigoArticulo = dbo.Gv_ArticuloInventario_B2.CodigoCompuesto  ");
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "Adm.Gv_Compra_B1.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "Adm.Gv_Compra_B1.Consecutivo", ConsecutivoCompra);
            if (LibString.Len(vSQLWhere) > 0)
            {
                vSql.AppendLine(" WHERE " + vSQLWhere);
            }
          
            return vSql.ToString();
        }
        #region Entre Fechas
        public string SqlImprimirCompra(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, bool valCambioOriginal, bool valMostrarComprasAnuladas, bool valMuestraDetalle, eMonedaParaImpresion MonedaParaImpresion) {
            if (MonedaParaImpresion == eMonedaParaImpresion.EnMonedaOriginal) {
                return ConstruirSQLComprasEntreFechasSoloMonedaOriginal(valConsecutivoCompania, valFechaInicial, valFechaFinal, valMostrarComprasAnuladas, valMuestraDetalle);
            } else {
                return ConfigurarDatosYSeccionesDelReporteComprasEntreFechasBolivaresMonedaOriginal(valConsecutivoCompania, valFechaInicial, valFechaFinal, valCambioOriginal, valMostrarComprasAnuladas, valMuestraDetalle);
            }
            
		}

        private string ConstruirSQLComprasEntreFechasSoloMonedaOriginal(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, bool valMostrarComprasAnuladas, bool valMuestraDetalle) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";
            
            vSQLWhere = "IGV_ComprasConCambioOriginal.EsUnaOrdenDeCompra = " + new QAdvSql("").ToSqlValue("N");
            vSQLWhere = new QAdvSql("").SqlDateValueBetween(vSQLWhere, " IGV_ComprasConCambioOriginal.Fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(vSQLWhere, " IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania", valConsecutivoCompania);

            if( !valMostrarComprasAnuladas) {
               vSQLWhere = vSQLWhere + " AND  IGV_ComprasConCambioOriginal.StatusCompra = " + new QAdvSql("").ToSqlValue((int)eStatusCompra.Vigente);
            }
            
            if( valMuestraDetalle ) {
               vSql.AppendLine(" SELECT " );
               vSql.AppendLine( "IGV_ComprasConCambioOriginal.Moneda," );
               vSql.AppendLine( "IGV_ComprasConCambioOriginal.Fecha," );
               vSql.AppendLine( "IGV_ComprasConCambioOriginal.Numero," );
               vSql.AppendLine( "IGV_ComprasConCambioOriginal.NombreProveedor,");
               vSql.AppendLine( "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo,");
               vSql.AppendLine( new QAdvSql("").IIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = '0' ", new QAdvSql("").ToSqlValue(""), "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial", true) + " AS Serial, ");
               vSql.AppendLine(new QAdvSql("").IIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo =  '0' ", new QAdvSql("").ToSqlValue(""), "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo", true) + " AS Rollo, ");
               vSql.AppendLine( "IGV_ComprasConCambioOriginal.cantidadrecibida,");
               vSql.AppendLine( "IGV_ComprasConCambioOriginal.TotalRenglones AS totalCompra,");
               vSql.AppendLine( "IGV_ComprasConCambioOriginal.CostoUnitario,");
               
               vSql.AppendLine( "(IGV_ComprasConCambioOriginal.cantidadrecibida * IGV_ComprasConCambioOriginal.CostoUnitario)  AS CompraTotalAlcambio," );
               
               vSql.AppendLine( new QAdvSql("").IIF("IGV_ComprasConCambioOriginal.StatusCompra  = " + new QAdvSql("").ToSqlValue((int)eStatusCompra.Vigente), new QAdvSql("").ToSqlValue("VIGENTES"), new QAdvSql("").ToSqlValue("ANULADAS"), true) + " AS StatusCompra ");
               vSql.AppendLine( " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1");
               
               vSql.AppendLine( "  INNER JOIN IGV_ComprasConCambioOriginal ON ");
               
                vSql.AppendLine( "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = IGV_ComprasConCambioOriginal.ConsecutivoCompania AND ");
               vSql.AppendLine( "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = IGV_ComprasConCambioOriginal.CodigoArticulo AND ");
               vSql.AppendLine( "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = IGV_ComprasConCambioOriginal.Serial AND ");
               vSql.AppendLine( "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = IGV_ComprasConCambioOriginal.Rollo  ");
               
               vSql.AppendLine( " WHERE " + vSQLWhere );
               vSql.AppendLine( " Order By ");
               vSql.AppendLine( "IGV_ComprasConCambioOriginal.StatusCompra,");
               vSql.AppendLine( "IGV_ComprasConCambioOriginal.Moneda," );
               vSql.AppendLine( " fecha," );
               vSql.AppendLine( " numero");
            } else {
               vSql.AppendLine(" SELECT " );
               vSql.AppendLine( " IGV_ComprasConCambioOriginal.Moneda,");
               vSql.AppendLine( " IGV_ComprasConCambioOriginal.Fecha,");
               vSql.AppendLine( " IGV_ComprasConCambioOriginal.Numero,");
               vSql.AppendLine( " IGV_ComprasConCambioOriginal.NombreProveedor,");
               vSql.AppendLine( new QAdvSql("").ToSqlValue("")+ " AS CodigoDelArticulo,");
               vSql.AppendLine( new QAdvSql("").ToSqlValue("")+ " AS Serial,");
               vSql.AppendLine( new QAdvSql("").ToSqlValue("") + " AS Rollo,");
               vSql.AppendLine( "0 AS cantidadrecibida," );
               vSql.AppendLine( "SUM( IGV_ComprasConCambioOriginal.TotalRenglones) AS TotalCompra,");
               vSql.AppendLine( "0 AS CostoUnitario,");
               
               vSql.AppendLine( "0 AS costoAlcambio, ");
               vSql.AppendLine(new QAdvSql("").IIF("IGV_ComprasConCambioOriginal.StatusCompra  = " + new QAdvSql("").ToSqlValue((int)eStatusCompra.Vigente), new QAdvSql("").ToSqlValue("VIGENTES"), new QAdvSql("").ToSqlValue("ANULADAS"), true) + " AS StatusCompra ");
               vSql.AppendLine( " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1");
               vSql.AppendLine( "  INNER JOIN IGV_ComprasConCambioOriginal ON ");
               vSql.AppendLine( "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = IGV_ComprasConCambioOriginal.ConsecutivoCompania AND ");
               vSql.AppendLine( "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = IGV_ComprasConCambioOriginal.CodigoArticulo AND ");
               vSql.AppendLine( "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = IGV_ComprasConCambioOriginal.Serial AND ");
               vSql.AppendLine( "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = IGV_ComprasConCambioOriginal.Rollo  ");
               vSql.AppendLine( " WHERE " + vSQLWhere );
               vSql.AppendLine( " GROUP BY  " );
               vSql.AppendLine( " IGV_ComprasConCambioOriginal.Moneda,IGV_ComprasConCambioOriginal.Fecha, " );
               vSql.AppendLine( " IGV_ComprasConCambioOriginal.Numero, IGV_ComprasConCambioOriginal.NombreProveedor, " );
               vSql.AppendLine( " StatusCompra" );
               vSql.AppendLine( " Order By " );
               vSql.AppendLine( " StatusCompra," );
               vSql.AppendLine( "IGV_ComprasConCambioOriginal.Moneda," );
               vSql.AppendLine( " fecha," );
               vSql.AppendLine( " numero" );
            }
            return vSql.ToString();

        }

        private string ConfigurarDatosYSeccionesDelReporteComprasEntreFechasBolivaresMonedaOriginal(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, bool valCambioOriginal, bool valMostrarComprasAnuladas, bool valMuestraDetalle) {
            
              StringBuilder vSql = new StringBuilder();
              string vSQLWhere = "";
              
           if ( valCambioOriginal) {
               vSQLWhere = "IGV_ComprasConCambioOriginal.EsUnaOrdenDeCompra = " + new QAdvSql("").ToSqlValue("N");
               vSQLWhere = new QAdvSql("").SqlDateValueBetween(vSQLWhere, "IGV_ComprasConCambioOriginal.Fecha", valFechaInicial, valFechaFinal);
               vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(vSQLWhere, "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania", valConsecutivoCompania);
               
               if ( ! valMostrarComprasAnuladas ) {
                vSQLWhere = vSQLWhere + " AND IGV_ComprasConCambioOriginal.StatusCompra = " + new QAdvSql("").ToSqlValue((int) eStatusCompra.Vigente);
               } 
               
               if ( valMuestraDetalle ) {
                vSql.AppendLine(" SELECT ");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.Moneda,");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.Fecha,");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.Numero,");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.NombreProveedor,");
                vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo,");
                vSql.AppendLine(new QAdvSql("").IIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = '0' ", new QAdvSql("").ToSqlValue(""), "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial", true) + " AS Serial, ");
                vSql.AppendLine(new QAdvSql("").IIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo =  '0' ", new QAdvSql("").ToSqlValue("N"), "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo", true) + " AS Rollo, ");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.cantidadrecibida,");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.TotalRenglones AS totalCompra,");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.CostoUnitario,");
               
                if ( valCambioOriginal ) {
                 vSql.AppendLine("IGV_ComprasConCambioOriginal.TotalRenglonesCambio  AS CompraTotalAlcambio,");
                 vSql.AppendLine("IGV_ComprasConCambioOriginal.CambioAbolivares AS cambio,");
                } else {
                 vSql.AppendLine("IGV_ComprasConCambioOriginal.TotalRenglonesCambio  AS CompraTotalAlcambio,");
                 vSql.AppendLine("IGV_ComprasConCambioOriginal.cambioalafecha AS cambio,");
                } 
               
                if ( valCambioOriginal ) {
                 vSql.AppendLine("( IGV_ComprasConCambioOriginal.CostoUnitario) AS costoAlcambio, ");
                } else {
                 vSql.AppendLine("( IGV_ComprasConCambioOriginal.cambioalafecha * IGV_ComprasConCambioOriginal.CostoUnitario) AS costoAlcambio, ");
                } 
                vSql.AppendLine(new QAdvSql("").IIF("IGV_ComprasConCambioOriginal.StatusCompra  = " + new QAdvSql("").ToSqlValue((int)eStatusCompra.Vigente), new QAdvSql("").ToSqlValue("VIGENTES"), new QAdvSql("").ToSqlValue("ANULADAS"), true) + " AS StatusCompra ");
                vSql.AppendLine(" FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1");
               
                vSql.AppendLine(" INNER JOIN IGV_ComprasConCambioOriginal ON ");
               
                vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = IGV_ComprasConCambioOriginal.ConsecutivoCompania AND ");
                vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = IGV_ComprasConCambioOriginal.CodigoArticulo AND ");
                vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = IGV_ComprasConCambioOriginal.Serial AND ");
                vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = IGV_ComprasConCambioOriginal.Rollo ");
               
                vSql.AppendLine(" WHERE " + vSQLWhere);
                vSql.AppendLine(" Order By ");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.StatusCompra,");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.Moneda,");
                vSql.AppendLine(" fecha,");
                vSql.AppendLine(" numero");
              } else {
                vSql.AppendLine(" SELECT ");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.Moneda,");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.Fecha,");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.Numero,");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.NombreProveedor,");
                vSql.AppendLine(new QAdvSql("").ToSqlValue("") + " AS CodigoDelArticulo,");
                vSql.AppendLine(new QAdvSql("").ToSqlValue("") + " AS Serial,");
                vSql.AppendLine(new QAdvSql("").ToSqlValue("") + " AS Rollo,");
                vSql.AppendLine("0 AS cantidadrecibida,");
                vSql.AppendLine("SUM(IGV_ComprasConCambioOriginal.TotalRenglones) AS totalCompra,");
                vSql.AppendLine("0 AS CostoUnitario,");
                if ( valCambioOriginal ) {
                 vSql.AppendLine("SUM(IGV_ComprasConCambioOriginal.TotalRenglonesCambio)  AS CompraTotalAlcambio,");
                    vSql.AppendLine("MAX(IGV_ComprasConCambioOriginal.CambioAbolivares) AS cambio,");
                } else {
                 vSql.AppendLine("SUM(IGV_ComprasConCambioOriginal.TotalRenglonesCambio)  AS CompraTotalAlcambio,");
                 vSql.AppendLine("MAX(IGV_ComprasConCambioOriginal.cambioalafecha) AS cambio,");
                } 
                vSql.AppendLine("0 AS costoAlcambio, ");
                vSql.AppendLine(new QAdvSql("").IIF("IGV_ComprasConCambioOriginal.StatusCompra  = " + new QAdvSql("").ToSqlValue((int)eStatusCompra.Vigente), new QAdvSql("").ToSqlValue("VIGENTES"), new QAdvSql("").ToSqlValue("ANULADAS"), true) + " AS StatusCompra ");
                vSql.AppendLine(" FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1");
                vSql.AppendLine("  INNER JOIN IGV_ComprasConCambioOriginal ON ");
                vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = IGV_ComprasConCambioOriginal.ConsecutivoCompania AND ");
                vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = IGV_ComprasConCambioOriginal.CodigoArticulo AND ");
                vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = IGV_ComprasConCambioOriginal.Serial AND ");
                vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = IGV_ComprasConCambioOriginal.Rollo  ");
                vSql.AppendLine(" WHERE " + vSQLWhere);
                vSql.AppendLine(" GROUP BY  ");
                vSql.AppendLine(" IGV_ComprasConCambioOriginal.Moneda,IGV_ComprasConCambioOriginal.Fecha, ");
                vSql.AppendLine(" IGV_ComprasConCambioOriginal.Numero, IGV_ComprasConCambioOriginal.NombreProveedor, ");
                vSql.AppendLine(" StatusCompra");
                vSql.AppendLine(" Order By ");
                vSql.AppendLine(" StatusCompra,");
                vSql.AppendLine("IGV_ComprasConCambioOriginal.Moneda,");
                vSql.AppendLine(" fecha,");
                vSql.AppendLine(" numero");
               } 
               } else {
               vSQLWhere = "IGV_ComprasConCambioAlDia.EsUnaOrdenDeCompra = " + new QAdvSql("").ToSqlValue("N");
               vSQLWhere = new QAdvSql("").SqlDateValueBetween(vSQLWhere, "IGV_ComprasConCambioAlDia.Fecha", valFechaInicial, valFechaFinal);
               vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(vSQLWhere, "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania", valConsecutivoCompania);
               if ( ! valMostrarComprasAnuladas ) {
                vSQLWhere = vSQLWhere + " AND IGV_ComprasConCambioAlDia.StatusCompra = " +  new QAdvSql("").ToSqlValue((int)eStatusCompra.Vigente);
               }
              
               if ( valMuestraDetalle ) {
                   vSql.AppendLine(" SELECT ");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.Moneda,");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.Fecha,");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.Numero,");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.NombreProveedor,");
                   
                   vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo,");
                   vSql.AppendLine(new QAdvSql("").IIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = '0' ", new QAdvSql("").ToSqlValue(""), "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial", true) + " AS Serial, ");
                   vSql.AppendLine(new QAdvSql("").IIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo =  '0' ", new QAdvSql("").ToSqlValue(""), "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo", true) + " AS Rollo, ");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.cantidadrecibida,");
                   
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.TotalRenglones AS totalCompra,");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.CostoUnitario,");
                   if ( valCambioOriginal ) {
                      vSql.AppendLine("IGV_ComprasConCambioAlDia.TotalRenglonesCambio  AS CompraTotalAlcambio,");
                      vSql.AppendLine("IGV_ComprasConCambioAlDia.CambioAbolivares AS cambio,");
                   } else {
                      vSql.AppendLine("IGV_ComprasConCambioAlDia.TotalRenglonesCambio  AS CompraTotalAlcambio,");
                      vSql.AppendLine("IGV_ComprasConCambioAlDia.cambioalafecha AS cambio,");
                   }
                   if ( valCambioOriginal ) {
                      vSql.AppendLine("( IGV_ComprasConCambioAlDia.CostoUnitario) AS costoAlcambio, ");
                   } else {
                      vSql.AppendLine("(IGV_ComprasConCambioAlDia.cambioalafecha * IGV_ComprasConCambioAlDia.CostoUnitario) AS costoAlcambio, ");
                   }
                   vSql.AppendLine(new QAdvSql("").IIF("IGV_ComprasConCambioAlDia.StatusCompra  = " + new QAdvSql("").ToSqlValue((int)eStatusCompra.Vigente), new QAdvSql("").ToSqlValue("VIGENTES"), new QAdvSql("").ToSqlValue("ANULADAS"), true) + " AS StatusCompra ");
                   vSql.AppendLine(" FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1");
                   
                   vSql.AppendLine("  INNER JOIN IGV_ComprasConCambioAlDia ON ");
                   
                   vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = IGV_ComprasConCambioAlDia.ConsecutivoCompania AND ");
                   vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = IGV_ComprasConCambioAlDia.CodigoArticulo AND ");
                   vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = IGV_ComprasConCambioAlDia.Serial AND ");
                   vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = IGV_ComprasConCambioAlDia.Rollo  ");
                   
                   vSql.AppendLine(" WHERE " + vSQLWhere);
                   vSql.AppendLine(" Order By ");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.StatusCompra,");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.Moneda,");
                   vSql.AppendLine(" fecha,");
                   vSql.AppendLine(" numero");
               } else {
                   vSql.AppendLine(" SELECT ");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.Moneda,");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.Fecha,");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.Numero,");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.NombreProveedor,");
                   vSql.AppendLine(new QAdvSql("").ToSqlValue("") + " AS CodigoDelArticulo,");
                   vSql.AppendLine(new QAdvSql("").ToSqlValue("") + " AS Serial,");
                   vSql.AppendLine(new QAdvSql("").ToSqlValue("") + " AS Rollo,");
                   vSql.AppendLine("0 AS cantidadrecibida,");
                   vSql.AppendLine("SUM(IGV_ComprasConCambioAlDia.TotalRenglones) AS totalCompra,");
                   vSql.AppendLine("0 AS CostoUnitario,");
                   if ( valCambioOriginal ) {
                      vSql.AppendLine("SUM(IGV_ComprasConCambioAlDia.TotalRenglonesCambio)  AS CompraTotalAlcambio,");
                      vSql.AppendLine("MAX(IGV_ComprasConCambioAlDia.CambioAbolivares) AS cambio,");
                   } else {
                      vSql.AppendLine("SUM(IGV_ComprasConCambioAlDia.TotalRenglonesCambio)  AS CompraTotalAlcambio,");
                      vSql.AppendLine("MAX(IGV_ComprasConCambioAlDia.cambioalafecha) AS cambio,");
                   }
                   vSql.AppendLine("0 AS costoAlcambio, ");
                   vSql.AppendLine(new QAdvSql("").IIF("IGV_ComprasConCambioAlDia.StatusCompra  = " + new QAdvSql("").ToSqlValue((int)eStatusCompra.Vigente), new QAdvSql("").ToSqlValue("VIGENTES"), new QAdvSql("").ToSqlValue("ANULADAS"), true) + " AS StatusCompra ");
                   vSql.AppendLine(" FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1");
                   vSql.AppendLine("  INNER JOIN IGV_ComprasConCambioAlDia ON ");
                   vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = IGV_ComprasConCambioAlDia.ConsecutivoCompania AND ");
                   vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = IGV_ComprasConCambioAlDia.CodigoArticulo AND ");
                   vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = IGV_ComprasConCambioAlDia.Serial AND ");
                   vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = IGV_ComprasConCambioAlDia.Rollo  ");
                   vSql.AppendLine(" WHERE " + vSQLWhere);
                   vSql.AppendLine(" GROUP BY  ");
                   vSql.AppendLine(" IGV_ComprasConCambioAlDia.Moneda,IGV_ComprasConCambioAlDia.Fecha, ");
                   vSql.AppendLine(" IGV_ComprasConCambioAlDia.Numero, IGV_ComprasConCambioAlDia.NombreProveedor, ");
                   vSql.AppendLine(" StatusCompra");
                   vSql.AppendLine(" Order By ");
                   vSql.AppendLine(" StatusCompra,");
                   vSql.AppendLine("IGV_ComprasConCambioAlDia.Moneda,");
                   vSql.AppendLine(" fecha,");
                   vSql.AppendLine(" numero");
               }
           }

           return vSql.ToString();
        }
#endregion

        public string ConstruirSQLCostoDeComprasEntreFechas(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eReporteCostoDeCompras valLineasDeProductoCantidadAImprimir, string valCodigoProducto) {
           StringBuilder vSql = new StringBuilder();
           string vSQLWhere = "";
           QAdvSql vUtilSql = new QAdvSql("");
           vSQLWhere = "IGV_ComprasConCambioOriginal.EsUnaOrdenDeCompra = " + vUtilSql.ToSqlValue("N");

           if (valLineasDeProductoCantidadAImprimir == eReporteCostoDeCompras.UnaLineaDeProducto) {
               vSQLWhere = vUtilSql.SqlValueWithAnd(vSQLWhere, "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto", valCodigoProducto, false);
           }
           if (valLineasDeProductoCantidadAImprimir == eReporteCostoDeCompras.UnArticulo) {
              vSQLWhere = vUtilSql.SqlValueWithAnd(vSQLWhere, "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo",valCodigoProducto);
           }
           vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere,"IGV_ComprasConCambioOriginal.Fecha", valFechaInicial, valFechaFinal);
           vSQLWhere = vSQLWhere + " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = " + vUtilSql.ToSqlValue(valConsecutivoCompania);
           
           vSql.AppendLine( " SELECT " );
           vSql.AppendLine( "IGV_ComprasConCambioOriginal.Moneda,");
           vSql.AppendLine( "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto AS LineaDeProducto,");
           vSql.AppendLine( " MAX( IGV_ComprasConCambioOriginal.Fecha) AS FechaUltCompra ,");
           vSql.AppendLine( "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo AS CodigoArticulo, ");
           vSql.AppendLine( "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion AS Descripcion, ");
           vSql.AppendLine( " SUM( IGV_ComprasConCambioOriginal.cantidadrecibida) AS CantArtComp, ");
           vSql.AppendLine( " COUNT(NUMERO) AS VecesComprados, ");
           vSql.AppendLine( " MAX( IGV_ComprasConCambioOriginal.CostoUnitario ) AS CostoMaximo,");
           vSql.AppendLine( " MIN( IGV_ComprasConCambioOriginal.CostoUnitario ) AS CostoMinimo,");
           vSql.AppendLine( " SUM( IGV_ComprasConCambioOriginal.TotalRenglones ) AS TotalCompXArt,");
           vSql.AppendLine( vUtilSql.IIF( " SUM( IGV_ComprasConCambioOriginal.cantidadrecibida) = 0","0"," SUM( IGV_ComprasConCambioOriginal.CostoUnitario ) / SUM( IGV_ComprasConCambioOriginal.cantidadrecibida)", true) + "AS PromPonDeComp");
           vSql.AppendLine( " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1");
           vSql.AppendLine( "  INNER JOIN  IGV_ComprasConCambioOriginal ON ");
           vSql.AppendLine( " IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = IGV_ComprasConCambioOriginal.ConsecutivoCompania AND ");
           vSql.AppendLine( " IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = IGV_ComprasConCambioOriginal.CodigoArticulo AND ");
           vSql.AppendLine( " IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = IGV_ComprasConCambioOriginal.Serial AND ");
           vSql.AppendLine( " IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = IGV_ComprasConCambioOriginal.Rollo  ");
           vSql.AppendLine( " WHERE " + vSQLWhere);
           vSql.AppendLine( " Group By ");
           vSql.AppendLine( " IGV_ComprasConCambioOriginal.Moneda,");
           vSql.AppendLine( " IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto,");
           vSql.AppendLine( " IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo,");
           vSql.AppendLine( " IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion");
           vSql.AppendLine( " Order By ");
           vSql.AppendLine( " IGV_ComprasConCambioOriginal.Moneda");
           return vSql.ToString();
        }
        
        public string ContruirSQLHistoricoCompras(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, eReporteCostoDeCompras valLineasDeProductoCantidadAImprimir, string valCodigoProducto) {

           StringBuilder vSql = new StringBuilder();
           QAdvSql vUtilSql = new QAdvSql("");
           string vDescripcionTallaColor;
           string vSQLWhere;
           bool valCambioOriginal = true;
           bool valMostrarComprasAnuladas = false;
           vSQLWhere = "IGV_ComprasConCambioOriginal.EsUnaOrdenDeCompra = " + vUtilSql.ToSqlValue("N");
           if ( !valMostrarComprasAnuladas ) {
              vSQLWhere = vSQLWhere + " AND IGV_ComprasConCambioOriginal.StatusCompra = " + vUtilSql.ToSqlValue((int)eStatusCompra.Vigente);
           }
           vDescripcionTallaColor = vUtilSql.IIF(vUtilSql.IsNull("IGV_ExistenciaPorGrupoConTallasyColores.DescripcionColor"), vUtilSql.ToSqlValue(""), "IGV_ExistenciaPorGrupoConTallasyColores.DescripcionColor", true);
           vDescripcionTallaColor = vDescripcionTallaColor + " + " + vUtilSql.IIF(vUtilSql.IsNull("IGV_ExistenciaPorGrupoConTallasyColores.DescripcionTalla"), vUtilSql.ToSqlValue(""), "IGV_ExistenciaPorGrupoConTallasyColores.DescripcionTalla", true);
            
           if ( valLineasDeProductoCantidadAImprimir == eReporteCostoDeCompras.UnaLineaDeProducto) {
              vSQLWhere = vUtilSql.SqlValueWithAnd(vSQLWhere, "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto", valCodigoProducto, false);
           }
           if (valLineasDeProductoCantidadAImprimir == eReporteCostoDeCompras.UnArticulo) {
              vSQLWhere = vUtilSql.SqlValueWithAnd(vSQLWhere, "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo", valCodigoProducto, false);
           }
          
           vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "IGV_ComprasConCambioOriginal.Fecha", valFechaInicial, valFechaFinal);
           vSQLWhere =vUtilSql.SqlIntValueWithAnd(vSQLWhere, "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania", valConsecutivoCompania);
            
           vSql.AppendLine( " SELECT ");
           vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto," );
           vSql.AppendLine("IGV_ComprasConCambioOriginal.Fecha," );
           vSql.AppendLine("IGV_ComprasConCambioOriginal.Numero,");
           vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo,");
           vSql.AppendLine(vUtilSql.IIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = '0' ", vUtilSql.ToSqlValue(""), "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial", true) + " AS Serial, ");
           vSql.AppendLine(vUtilSql.IIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo =  '0' ", vUtilSql.ToSqlValue(""), "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo", true) + " AS Rollo, ");
           vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion + " + vDescripcionTallaColor + " AS Descripcion  , ");
           
           vSql.AppendLine("IGV_ComprasConCambioOriginal.CodigoAlmacen,");
           vSql.AppendLine("IGV_ComprasConCambioOriginal.NombreProveedor,");
           vSql.AppendLine("IGV_ComprasConCambioOriginal.cantidadrecibida,");
          
           vSql.AppendLine("IGV_ComprasConCambioOriginal.Moneda,");
           vSql.AppendLine("IGV_ComprasConCambioOriginal.CostoUnitario,");
           if ( valCambioOriginal ) {
              vSql.AppendLine("IGV_ComprasConCambioOriginal.CambioAbolivares AS cambio,");
           } else {
              vSql.AppendLine("IGV_ComprasConCambioOriginal.cambioalafecha AS cambio,"); 
           }
           if ( valCambioOriginal ) {
               vSql.AppendLine("( IGV_ComprasConCambioOriginal.CambioAbolivares * IGV_ComprasConCambioOriginal.CostoUnitario) AS costoAlcambio,");
           } else {
              vSql.AppendLine("( IGV_ComprasConCambioOriginal.cambioalafecha * IGV_ComprasConCambioOriginal.CostoUnitario) AS costoAlcambio,");
           }
           vSql.AppendLine(vUtilSql.IIF("IGV_ComprasConCambioOriginal.StatusCompra  = " + vUtilSql.ToSqlValue((int)eStatusCompra.Vigente), vUtilSql.ToSqlValue("VIGENTES"), vUtilSql.ToSqlValue("ANULADAS"), true) + " AS StatusCompra ");
           vSql.AppendLine(" FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1");
           vSql.AppendLine("  INNER JOIN  IGV_ComprasConCambioOriginal ON ");
           vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = IGV_ComprasConCambioOriginal.ConsecutivoCompania AND ");
           vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = IGV_ComprasConCambioOriginal.CodigoArticulo AND ");
           vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = IGV_ComprasConCambioOriginal.Serial AND ");
           vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = IGV_ComprasConCambioOriginal.Rollo  ");
           vSql.AppendLine(" LEFT  JOIN IGV_ExistenciaPorGrupoConTallasyColores ON ");
           vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = IGV_ExistenciaPorGrupoConTallasyColores.CodigoArticulo AND ");
           vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = IGV_ExistenciaPorGrupoConTallasyColores.Serial AND ");
           vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = IGV_ExistenciaPorGrupoConTallasyColores.Rollo AND ");
           vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = IGV_ExistenciaPorGrupoConTallasyColores.ConsecutivoCompania ");
           vSql.AppendLine(" WHERE " + vSQLWhere);
           vSql.AppendLine(" Order By ");
           vSql.AppendLine("IGV_ComprasConCambioOriginal.StatusCompra,");
           vSql.AppendLine("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto,");
           vSql.AppendLine(" fecha,");
           vSql.AppendLine(" numero");
           return vSql.ToString();
        }

        public string ConstruirSQLMargenSobreCostoPromedioDeComp(int valConsecutivoCompania, eNivelDePrecio valNivelDePrecio, eReporteCostoDeCompras valLineasDeProductoCantidadAImprimir, string valCodigoProducto) {

            string GetViewNameComprasConCambioOriginal = "IGV_ComprasConCambioOriginal";
            string GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 = "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1";
            StringBuilder vSql = new StringBuilder();
            QAdvSql vUtilSql = new QAdvSql("");
            string vSQLWhere;
            string vPrecioVenta;
            string vCondicionPrecioVenta;
            string vMargen;
            string vPromComp;
            vPrecioVenta = SqlNivelDePrecio(valNivelDePrecio, true);
            vPromComp = vUtilSql.IIF(" SUM( " + GetViewNameComprasConCambioOriginal + ".cantidadrecibida) = 0","0","SUM( " + GetViewNameComprasConCambioOriginal + ".CambioAbolivares * " + GetViewNameComprasConCambioOriginal + ".CostoUnitario ) / " + " SUM( " + GetViewNameComprasConCambioOriginal + ".cantidadrecibida)", true);
            vMargen = GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + "." + vPrecioVenta + "-" + vPromComp;
            vCondicionPrecioVenta = vUtilSql.IIF(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + "." + vPrecioVenta + " = " + vUtilSql.ToSqlValue(0), vUtilSql.ToSqlValue(-1), GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + "." + vPrecioVenta, true);
            
            vSQLWhere = GetViewNameComprasConCambioOriginal + ".EsUnaOrdenDeCompra = " + vUtilSql.ToSqlValue("N");
            if ( valLineasDeProductoCantidadAImprimir == eReporteCostoDeCompras.UnaLineaDeProducto ) {
                vSQLWhere = vUtilSql.SqlValueWithAnd(vSQLWhere, GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".LineaDeProducto", valCodigoProducto, false);
            }
            if (valLineasDeProductoCantidadAImprimir == eReporteCostoDeCompras.UnArticulo) {
               vSQLWhere = vUtilSql.SqlValueWithAnd(vSQLWhere, GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".CodigoDelArticulo", valCodigoProducto, false);
            }

            vSQLWhere = vSQLWhere + " AND " + GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".ConsecutivoCompania = " + vUtilSql.ToSqlValue(valConsecutivoCompania);
           
            vSql.AppendLine(" SELECT ");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".LineaDeProducto AS LineaDeProducto,");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".Codigo AS Codigo, ");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".Descripcion AS Descripcion, ");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + "." + vPrecioVenta + " AS PV, ");
            vSql.AppendLine(vPromComp + " AS PromDeComp, ");
            vSql.AppendLine(vMargen + " AS Margen, ");
            vSql.AppendLine(" ( " + vMargen + "/" + vCondicionPrecioVenta + ") * " + vUtilSql.ToSqlValue(100) + " AS PorcentajeMargen ");
            
            vSql.AppendLine(" FROM " + GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1);
            vSql.AppendLine("  INNER JOIN  " + GetViewNameComprasConCambioOriginal + " ON ");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".ConsecutivoCompania = " + GetViewNameComprasConCambioOriginal + ".ConsecutivoCompania AND ");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".CodigoDelArticulo = " + GetViewNameComprasConCambioOriginal + ".CodigoArticulo AND ");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".Serial = " + GetViewNameComprasConCambioOriginal + ".Serial AND ");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".Rollo = " + GetViewNameComprasConCambioOriginal + ".Rollo  ");
            vSql.AppendLine(" WHERE " + vSQLWhere);
            vSql.AppendLine(" Group By ");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".LineaDeProducto,");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".Codigo,");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".Descripcion,");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + "." + vPrecioVenta);
           
            vSql.AppendLine(" Order By ");
            vSql.AppendLine(GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 + ".LineaDeProducto");

            return vSql.ToString();
        }

        public string ConstruirSQLImpresionDeComprasEtiquetas(int valConsecutivoCompania, eNivelDePrecio valNivelDePrecio, string valNumero) {
            
            string GetViewNameComprasImprimirEtiquetasPorCompras = "IGV_ComprasImprimirEtiquetasPorCompras";
            StringBuilder vSql = new StringBuilder();
            StringBuilder vGroupby = new StringBuilder();
            QAdvSql vUtilSql = new QAdvSql("");
            string vWhere;
            string vPrecioSinIva;
            string vPrecioConIva;

            
            vPrecioSinIva = GetViewNameComprasImprimirEtiquetasPorCompras + "." + SqlNivelDePrecio(valNivelDePrecio, true);
            vPrecioConIva = GetViewNameComprasImprimirEtiquetasPorCompras + "." + SqlNivelDePrecio(valNivelDePrecio, false);
            
            vWhere = "";
            vWhere = GetViewNameComprasImprimirEtiquetasPorCompras + ".Numero ='" + valNumero + "'";
            vWhere = vWhere + " AND ";
            vWhere = vWhere + GetViewNameComprasImprimirEtiquetasPorCompras + ".Compania ='" + valConsecutivoCompania + "'";

            vGroupby.AppendLine(" GROUP BY ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Numero, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".PrecioSinIva, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".PrecioConIva, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".PrecioSinIva2, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".PrecioConIva2, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".PrecioSinIva3, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".PrecioConIva3, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".PrecioSinIva4, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".PrecioConIva4, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".CodigoDelArticulo, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Descripcion, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".CantidadRecibida, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Serial, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Rollo, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".DescripcionColor, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".DescripcionTalla, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Fecha, ");
            vGroupby.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".NombreProveedor");
            
            vSql.AppendLine("SELECT ");
            vSql.AppendLine(" (" + GetViewNameComprasImprimirEtiquetasPorCompras + ".CodigoDelArticulo");
            vSql.AppendLine(" +ISNULL (" + GetViewNameComprasImprimirEtiquetasPorCompras + ".Serial ,'') + ISNULL (" + GetViewNameComprasImprimirEtiquetasPorCompras + ".Rollo ,'') ) AS Concatenado, ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Numero, ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".NombreProveedor, ");
            vSql.AppendLine(vPrecioSinIva + " AS PrecioSinIva, ");
            vSql.AppendLine(vPrecioConIva + " AS PrecioConIva, ");
            vSql.AppendLine("CASE WHEN " + vPrecioSinIva + " = '0'");
            vSql.AppendLine(" OR ");
            vSql.AppendLine(vPrecioConIva + " = '0' THEN ");
            vSql.AppendLine("'0' ");
            vSql.AppendLine("ELSE ");
            vSql.AppendLine("(" + vPrecioConIva + " - " + vPrecioSinIva + ") ");
            vSql.AppendLine("END AS MontoIVA, ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".CodigoDelArticulo, ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Descripcion, ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".CantidadRecibida, ");
            vSql.AppendLine("CASE WHEN ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Serial IS NULL OR ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Serial = '0' THEN ");
            vSql.AppendLine("'Sin Serial' ELSE ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Serial ");
            vSql.AppendLine("END AS Serial, ");
            vSql.AppendLine("CASE WHEN ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Rollo IS NULL OR ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Rollo = '0' THEN ");
            vSql.AppendLine("'Sin Rollo' ELSE ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Rollo ");
            vSql.AppendLine("END AS Rollo, ");
            vSql.AppendLine("CASE WHEN ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".DescripcionTalla IS NULL THEN ");
            vSql.AppendLine("'Sin Talla' ELSE ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".DescripcionTalla ");
            vSql.AppendLine("END AS Talla, ");
            vSql.AppendLine("CASE WHEN ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".DescripcionColor IS NULL THEN ");
            vSql.AppendLine("'Sin Color' ELSE ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".DescripcionColor ");
            vSql.AppendLine("END AS Color, ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras + ".Fecha ");
            vSql.AppendLine("FROM ");
            vSql.AppendLine(GetViewNameComprasImprimirEtiquetasPorCompras);
            vSql.AppendLine(" WHERE " + vWhere);
            vSql.AppendLine(vGroupby.ToString());

            return vSql.ToString();
        }

        private string SqlNivelDePrecio(eNivelDePrecio valNivelDePrecio,bool valPrecioSinIva) { 
            string vPrecioVenta = "";        
            switch (valNivelDePrecio) {
                case eNivelDePrecio.Nivel1:
                    vPrecioVenta = "Iva";
                    break;
                case eNivelDePrecio.Nivel2:
                    vPrecioVenta = "Iva2";
                    break;
                case eNivelDePrecio.Nivel3:
                    vPrecioVenta = "Iva3";
                    break;
                case eNivelDePrecio.Nivel4:
                    vPrecioVenta = "Iva4";
                    break;
                default:
                    vPrecioVenta = "Iva";
                    break;
            }
            if (valPrecioSinIva) {
                vPrecioVenta = "PrecioSin" + vPrecioVenta;
            } else {
                vPrecioVenta = "PrecioCon" + vPrecioVenta;
            }
            return vPrecioVenta;
        }
        #endregion //Metodos Generados
    }
}

