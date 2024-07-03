SET DATEFORMAT dmy
SET LANGUAGE spanish

DECLARE @Compania AS int

SET @Compania = 1

/* Ventas */

SET LANGUAGE spanish
SELECT 
F.Numero AS numero, 
F.TipoDeDocumento AS tipoDeDocumento,
F.Fecha AS fecha,
MONTH(F.Fecha) AS mesFactura,
DATENAME(M, F.Fecha) AS mesFacturaStr,
YEAR(F.Fecha) AS anoFactura,
RF.Articulo AS codigoArticulo,
RF.Descripcion AS descripcionArticuloOriginal,
CAST(RF.Articulo AS varchar) + ' - ' + LEFT(CAST(RF.Descripcion AS varchar), 50) AS descripcionArticulo,
V.Nombre AS nombreVendedor,
AI.LineaDeProducto AS lineaDeProducto,
C.SectorDeNegocio AS sectorNegocioCliente,
C.ZonaDeCobranza AS zonaCobranzaCliente,
Ruta.Descripcion AS rutaComercializacionVendedor,
RF.Cantidad AS unidades,
(CASE WHEN CodigoMoneda = 'VED' THEN ROUND(ROUND(ROUND((RF.Cantidad * RF.PrecioSinIVA) * (1 - (RF.PorcentajeDescuento/100)), 2) * (1 - (F.PorcentajeDescuento/100)), 2) / F.CambioMonedaCXC,2) ELSE ROUND(ROUND((RF.Cantidad * RF.PrecioSinIVA) * (1 - (RF.PorcentajeDescuento/100)), 2) * (1 - (F.PorcentajeDescuento/100)), 2) END)
AS totalRenglonConDescuentosME,
AI.UnidadDeVenta AS unidadDeVenta

FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.TipoDeDocumento = RF.TipoDeDocumento AND F.Numero = RF.NumeroFactura
INNER JOIN articuloInventario AI ON RF.ConsecutivoCompania = AI.ConsecutivoCompania AND RF.Articulo = AI.Codigo
INNER JOIN Adm.Vendedor V ON F.ConsecutivoCompania = V.ConsecutivoCompania AND F.ConsecutivoVendedor = V.Consecutivo
INNER JOIN Saw.RutaDeComercializacion Ruta ON V.ConsecutivoCompania = Ruta.ConsecutivoCompania AND V.ConsecutivoRutaDeComercializacion = Ruta.Consecutivo
INNER JOIN cliente C ON F.ConsecutivoCompania = C.ConsecutivoCompania AND F.CodigoCliente = C.Codigo

WHERE F.ConsecutivoCompania = @Compania
AND YEAR(F.Fecha) BETWEEN YEAR(GETDATE())-1 AND YEAR(GETDATE())
AND F.StatusFactura = '0'
AND F.GeneradoPor <> '3'
AND F.TipoDeDocumento IN ('0', '1', '2', '5', '7')

ORDER BY F.Fecha, F.Numero



-- COMPARATIVO DE VENTAS TOTALES AÑO ACTUAL VS AÑO ANTERIOR UNIDADES 
-- COMPARATIVO DE VENTAS TOTALES AÑO ACTUAL VS AÑO ANTERIOR EN DÓLARES A TASA ORIGINAL
-- TOP 5 DE PRODUCTOS MÁS VENDIDOS AÑO ACTUAL EN DÓLARES A TASA ORIGINAL
-- TOP 5 DE MEJORES VENDEDORES AÑO ACTUAL EN DÓLARES A TASA ORIGINAL
-- VENTAS POR LÍNEA DE PRODUCTO UNIDADES AÑO ACTUAL
-- VENTAS POR LÍNEA DE PRODUCTO MONTO EN DÓLARES A TASA ORIGINAL AÑO ACTUAL
-- VENTAS POR SECTOR DE NEGOCIO UNIDADES AÑO ACTUAL
-- VENTAS POR SECTOR DE NEGOCIO MONTO EN DÓLARES A TASA ORIGINAL AÑO ACTUAL
-- VENTAS POR ZONA DE COBRANZA UNIDADES AÑO ACTUAL
-- VENTAS POR ZONA DE COBRANZA MONTO EN DÓLARES A TASA ORIGINAL AÑO ACTUAL