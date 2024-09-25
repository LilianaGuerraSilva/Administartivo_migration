VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsArticuloSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsArticuloSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Articulo"

Public Function fConstruirSQLDelReporteArticuloDeInventario(ByVal valPreciosConSinIva As String, ByVal valenumPreciosDeLosProductosToString As String, ByVal valImprimirDetalles As Boolean, ByVal valUsaMonedaExtranjera As Boolean, ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, ByVal valenumCantidadAImprimirToString As String, ByVal valLineaDeProductos As String, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String) As String
   Dim TraerCamposSinOConString As String
   Dim TraerCamposUsaMonedaExtranjera As String
   Dim SQL As String
   Dim sqlTipoDeArticulo As String
   Dim preciosSinIva As Boolean
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   preciosSinIva = (valPreciosConSinIva = valenumPreciosDeLosProductosToString)
   sqlTipoDeArticulo = gUtilSQL.DfSQLCaseIfForEnum("articuloInventario.TIPODEARTICULO", enum_TipoDeArticulo.eTD_MERCANCIA, gEnumProyecto.fEnumTipoDeArticuloToStringInArray(True), "TipoDeArticuloStr")
   SQL = "SELECT "
   SQL = SQL & "articuloInventario.CODIGO, "
   SQL = SQL & "articuloInventario.DESCRIPCION , "
   SQL = SQL & "articuloInventario.PORCENTAJEBASEIMPONIBLE , "
   SQL = SQL & "articuloInventario.CAMPODEFINIBLE1 , "
   SQL = SQL & "articuloInventario.CAMPODEFINIBLE2 , "
   SQL = SQL & "articuloInventario.CAMPODEFINIBLE3 , "
   SQL = SQL & "articuloInventario.CAMPODEFINIBLE4 , "
   SQL = SQL & "articuloInventario.CAMPODEFINIBLE5 , "
   SQL = SQL & "articuloInventario.LINEADEPRODUCTO , "
   SQL = SQL & "articuloInventario.TIPODEARTICULO , "
   SQL = SQL & sqlTipoDeArticulo & ", "
   SQL = SQL & "articuloInventario.COSTOUNITARIO , "
   If preciosSinIva Then
      SQL = SQL & "articuloInventario.PRECIOSINIVA AS Precio, "
      SQL = SQL & "articuloInventario.PRECIOSINIVA2 AS Precio2, "
      SQL = SQL & "articuloInventario.PRECIOSINIVA3 AS Precio3, "
      SQL = SQL & "articuloInventario.PRECIOSINIVA4 AS Precio4 , "
   Else
      SQL = SQL & "articuloInventario.PRECIOCONIVA AS Precio, "
      SQL = SQL & "articuloInventario.PRECIOCONIVA2 AS Precio2, "
      SQL = SQL & "articuloInventario.PRECIOCONIVA3 AS Precio3, "
      SQL = SQL & "articuloInventario.PRECIOCONIVA4 AS Precio4, "
   End If
   If valImprimirDetalles Then
      SQL = SQL & "productoCompuesto.CodigoArticulo, "
      SQL = SQL & "ArtInventario.DESCRIPCION AS DescripcionProdCompuesto, "
      SQL = SQL & "productoCompuesto.Cantidad, "
   End If
   If valUsaMonedaExtranjera Then
      SQL = SQL & "articuloInventario.MECOSTOUNITARIO , "
      If preciosSinIva Then
         SQL = SQL & "camposMonedaExtranjera.MEPRECIOSINIVA  AS MePrecio, "
         SQL = SQL & "camposMonedaExtranjera.MEPRECIOSINIVA2 AS MePrecio2, "
         SQL = SQL & "camposMonedaExtranjera.MEPRECIOSINIVA3 AS MePrecio3, "
         SQL = SQL & "camposMonedaExtranjera.MEPRECIOSINIVA4 AS MePrecio4, "
      Else
         SQL = SQL & "camposMonedaExtranjera.MEPRECIOCONIVA AS MePrecio, "
         SQL = SQL & "camposMonedaExtranjera.MEPRECIOCONIVA2 AS MePrecio2, "
         SQL = SQL & "camposMonedaExtranjera.MEPRECIOCONIVA3 AS MePrecio3, "
         SQL = SQL & "camposMonedaExtranjera.MEPRECIOCONIVA4 AS MePrecio4, "
      End If
   End If
   SQL = SQL & "articuloInventario.UNIDADDEVENTA , "
   SQL = SQL & "articuloInventario.EXISTENCIA , "
   SQL = SQL & "articuloInventario.CANTIDADMINIMA , "
   SQL = SQL & "articuloInventario.CANTIDADMAXIMA"
   SQL = SQL & " FROM "
   If valImprimirDetalles Then
      If valUsaMonedaExtranjera Then
         SQL = SQL & "(articuloInventario LEFT JOIN "
         SQL = SQL & "camposMonedaExtranjera ON ("
         SQL = SQL & "articuloInventario.CODIGO  = camposMonedaExtranjera.CODIGO"
         SQL = SQL & ") AND ("
         SQL = SQL & "articuloInventario.CONSECUTIVOCOMPANIA  = camposMonedaExtranjera.CONSECUTIVOCOMPANIA"
         SQL = SQL & ")) LEFT JOIN ("
         SQL = SQL & "articuloInventario AS ArtInventario RIGHT JOIN "
         SQL = SQL & "productoCompuesto ON ("
         SQL = SQL & "ArtInventario.CONSECUTIVOCOMPANIA = productoCompuesto.ConsecutivoCompania"
         SQL = SQL & ") AND (ArtInventario.CODIGO  = productoCompuesto.CodigoArticulo"
         SQL = SQL & ")) ON ("
         SQL = SQL & "articuloInventario.CODIGO  = productoCompuesto.CodigoConexionConElMaster"
         SQL = SQL & ") AND (articuloInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania)"
      Else
         SQL = SQL & "articuloInventario LEFT JOIN ("
         SQL = SQL & "articuloInventario AS ArtInventario RIGHT JOIN productoCompuesto "
         SQL = SQL & " ON (ArtInventario.CONSECUTIVOCOMPANIA = productoCompuesto.ConsecutivoCompania"
         SQL = SQL & ") AND ("
         SQL = SQL & "ArtInventario.CODIGO  = productoCompuesto.CodigoArticulo"
         SQL = SQL & ")) ON ("
         SQL = SQL & "articuloInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania"
         SQL = SQL & ") AND (articuloInventario.CODIGO  = productoCompuesto.CodigoConexionConElMaster)"
      End If
   Else
      If valUsaMonedaExtranjera Then
         SQL = SQL & "articuloInventario "
         SQL = SQL & " LEFT JOIN camposMonedaExtranjera"
         SQL = SQL & " ON articuloInventario.CODIGO  = camposMonedaExtranjera.CODIGO"
         SQL = SQL & " AND articuloInventario.CONSECUTIVOCOMPANIA  = camposMonedaExtranjera.CONSECUTIVOCOMPANIA"
      Else
         SQL = SQL & "articuloInventario"
      End If
   End If
   SQL = SQL & " WHERE "
   SQL = SQL & "articuloInventario.CONSECUTIVOCOMPANIA  = " & valConsecutivoCompania
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
      SQL = SQL & " AND articuloInventario.LINEADEPRODUCTO  = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   If valCodigoDesde <> "" And valCodigoHasta <> "" Then
      SQL = SQL & " AND " & gUtilSQL.fSQLStringValueBetween("articuloInventario.CODIGO", valCodigoDesde, valCodigoHasta)
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & "articuloInventario.LINEADEPRODUCTO , "
   SQL = SQL & "articuloInventario.CODIGO"
   Set gEnumProyecto = Nothing
   fConstruirSQLDelReporteArticuloDeInventario = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteArticuloDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteListadoDeArticulos(ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, ByVal valenumCantidadAImprimirToString As String, _
                     ByVal valLineaDeProductos As String, ByVal valTipoDeArticulo As String, ByVal valstrTipoDearticuloInventarioToNum As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "articuloInventario.CODIGO , "
   SQL = SQL & "articuloInventario.DESCRIPCION , "
   SQL = SQL & "articuloInventario.LINEADEPRODUCTO"
   SQL = SQL & " FROM articuloInventario"
   SQL = SQL & " WHERE "
   SQL = SQL & "articuloInventario.CONSECUTIVOCOMPANIA  = " & valConsecutivoCompania
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
      SQL = SQL & " AND articuloInventario.LINEADEPRODUCTO  = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   If valTipoDeArticulo <> gDefgen.getSearchAllComboBox Then
      SQL = SQL & " AND articuloInventario.TIPODEARTICULO  = " & gUtilSQL.fSQLSimpleValueForEnum(valstrTipoDearticuloInventarioToNum)
   End If
   SQL = SQL & " AND " & gUtilSQL.DfLeftSQL("articuloInventario.CODIGO", 3) & "<>" & gUtilSQL.fSimpleSqlValue("RD_")
   SQL = SQL & " AND " & gUtilSQL.DfRightSQL("articuloInventario.CODIGO", 1) & "<>" & gUtilSQL.fSimpleSqlValue("@")
   SQL = SQL & " ORDER BY "
   SQL = SQL & "articuloInventario.LINEADEPRODUCTO , "
   SQL = SQL & "articuloInventario.CODIGO"
   fConstruirSQLDelReporteListadoDeArticulos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteListadoDeArticulos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConstruirSQLDelReporteListadoArticulosPorRetirarEntreFecha(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long, ByVal valIsSQLDataBase As Boolean) As String
   Dim SQL As String
   Dim SQLCodigoArticulo As String
   On Error GoTo h_ERROR
   SQLCodigoArticulo = gUtilSQL.getIIF("renglonfactura.Serial <> ''", " renglonFactura.Articulo + ' Serial ' + renglonfactura.Serial + ' Rollo '+ renglonfactura.rollo ", " renglonFactura.Articulo", True) & "  as CodArt "
   SQL = " SELECT DISTINCT  " & SQLCodigoArticulo & ", "
   SQL = SQL & " articuloInventario.Descripcion as Descripcion, "
   SQL = SQL & " factura.Numero as NumeroDocumento, "
   SQL = SQL & " renglonFactura.Cantidad as CantReservada, "
   SQL = SQL & " cliente.Nombre as NombreCliente, "
   SQL = SQL & " factura.fechaDeRetiro as fechaRetiro "
   SQL = SQL & " FROM (ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)"
   SQL = SQL & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) INNER JOIN ((cliente INNER JOIN factura ON (cliente.Codigo = factura.CodigoCliente) AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)) "
   SQL = SQL & " INNER JOIN renglonFactura ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) AND (factura.Numero = renglonFactura.NumeroFactura) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) ON (renglonFactura.Articulo = ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   SQL = SQL & " WHERE   (factura.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonFactura.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   SQL = SQL & " AND (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & ")"
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("factura.fechaDeRetiro", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT DISTINCT  renglonFactura.Articulo as CodArt, "
   SQL = SQL & " articuloInventario.Descripcion as Descripcion, "
   SQL = SQL & " factura.Numero as NumeroDocumento, "
   SQL = SQL & " renglonFactura.Cantidad as CantReservada, "
   SQL = SQL & " cliente.Nombre as NombreCliente, "
   SQL = SQL & " factura.fechaDeRetiro as fechaRetiro "
   SQL = SQL & " FROM articuloInventario INNER JOIN (cliente INNER JOIN (factura INNER JOIN renglonFactura "
   SQL = SQL & " ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) AND (factura.Numero = renglonFactura.NumeroFactura) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) ON (cliente.Codigo = factura.CodigoCliente) AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)) ON (articuloInventario.ConsecutivoCompania = factura.ConsecutivoCompania) AND (articuloInventario.Codigo = renglonFactura.Articulo)"
   SQL = SQL & " WHERE   (factura.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonFactura.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   SQL = SQL & " AND (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & ")"
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("factura.fechaDeRetiro", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " UNION "
   SQLCodigoArticulo = gUtilSQL.getIIF("renglonCotizacion.Serial <> ''", " renglonCotizacion.CodigoArticulo  + ' Serial ' + renglonCotizacion.Serial + ' Rollo '+ renglonCotizacion.rollo ", " renglonCotizacion.CodigoArticulo", True) & "  as CodArt "
   SQL = SQL & " SELECT DISTINCT " & SQLCodigoArticulo & ", "
   SQL = SQL & " articuloInventario.Descripcion as Descripcion, "
   SQL = SQL & " cotizacion.Numero as NumeroDocumento, "
   SQL = SQL & " renglonCotizacion.Cantidad as CantReservada, "
   SQL = SQL & " cliente.Nombre as NombreCliente, "
   SQL = SQL & " cotizacion.fechaDeRetiro as fechaRetiro "
   SQL = SQL & " FROM (ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)"
   SQL = SQL & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) INNER JOIN ((cliente INNER JOIN Cotizacion ON (cliente.Codigo = Cotizacion.CodigoCliente) AND (cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)) "
   SQL = SQL & " INNER JOIN renglonCotizacion ON (Cotizacion.Numero = renglonCotizacion.NumeroCotizacion) AND (Cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)) ON (renglonCotizacion.CodigoArticulo = ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)"
   SQL = SQL & " WHERE   (Cotizacion.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonCotizacion.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("cotizacion.fechaDeRetiro", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT DISTINCT  renglonCotizacion.CodigoArticulo as CodArt, "
   SQL = SQL & " articuloInventario.Descripcion as Descripcion, "
   SQL = SQL & " cotizacion.Numero as NumeroDocumento, "
   SQL = SQL & " renglonCotizacion.Cantidad as CantReservada, "
   SQL = SQL & " cliente.Nombre as NombreCliente, "
   SQL = SQL & " cotizacion.fechaDeRetiro as fechaRetiro "
   SQL = SQL & " FROM articuloInventario INNER JOIN (cliente INNER JOIN (Cotizacion INNER JOIN renglonCotizacion "
   SQL = SQL & " ON (Cotizacion.Numero = renglonCotizacion.NumeroCotizacion) AND (Cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)) ON (cliente.Codigo = Cotizacion.CodigoCliente) AND (cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)) ON (articuloInventario.ConsecutivoCompania = Cotizacion.ConsecutivoCompania) AND (articuloInventario.Codigo = renglonCotizacion.CodigoArticulo)"
   SQL = SQL & " WHERE   (cotizacion.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonCotizacion.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("cotizacion.fechaDeRetiro", valFechaInicial, valFechaFinal) & ")"
   If valIsSQLDataBase Then
      SQL = SQL & " order by factura.Numero, factura.fechaDeRetiro "
   Else
      SQL = SQL & " order by NumeroDocumento, fechaRetiro"
    End If
  fConstruirSQLDelReporteListadoArticulosPorRetirarEntreFecha = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteListadoArticulosPorRetirarEntreFecha", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteArticulosReservados(ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim SQLCodigoArticulo As String
   On Error GoTo h_ERROR
   SQLCodigoArticulo = gUtilSQL.getIIF("renglonfactura.Serial <> '0'", " renglonFactura.Articulo + ' Serial ' + renglonfactura.Serial + ' Rollo '+ renglonfactura.rollo ", " renglonFactura.Articulo", True) & "  as CodArt "
   SQL = " SELECT " & SQLCodigoArticulo & ", "
   SQL = SQL & " articuloInventario.Descripcion as Descripcion, "
   SQL = SQL & " factura.Numero as NumeroDocumento, "
   SQL = SQL & " renglonFactura.Cantidad as CantReservada, "
   SQL = SQL & " cliente.Nombre as NombreCliente, "
   SQL = SQL & " factura.fecha as fecha, "
   SQL = SQL & " renglonFactura.Cantidad * renglonFactura.PrecioSinIVA as MontoTotal "
   SQL = SQL & " FROM (ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)"
   SQL = SQL & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) INNER JOIN ((cliente INNER JOIN factura ON (cliente.Codigo = factura.CodigoCliente) AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)) "
   SQL = SQL & " INNER JOIN renglonFactura ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) AND (factura.Numero = renglonFactura.NumeroFactura) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) ON (renglonFactura.Articulo = ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   SQL = SQL & " WHERE   (factura.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonFactura.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   SQL = SQL & " AND (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & ")"
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT renglonFactura.Articulo  as CodArt, "
   SQL = SQL & " articuloInventario.Descripcion as Descripcion, "
   SQL = SQL & " factura.Numero as NumeroDocumento, "
   SQL = SQL & " renglonFactura.Cantidad as CantReservada, "
   SQL = SQL & " cliente.Nombre as NombreCliente, "
   SQL = SQL & " factura.fecha as fecha, "
   SQL = SQL & " renglonFactura.Cantidad * renglonFactura.PrecioSinIVA as MontoTotal "
   SQL = SQL & " FROM articuloInventario INNER JOIN (cliente INNER JOIN (factura INNER JOIN renglonFactura "
   SQL = SQL & " ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) AND (factura.Numero = renglonFactura.NumeroFactura) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) ON (cliente.Codigo = factura.CodigoCliente) AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)) ON (articuloInventario.ConsecutivoCompania = factura.ConsecutivoCompania) AND (articuloInventario.Codigo = renglonFactura.Articulo)"
   SQL = SQL & " WHERE   (factura.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonFactura.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   SQL = SQL & " AND (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & ")"
   SQL = SQL & " AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Simple) & ")"
   SQL = SQL & " UNION "
   SQLCodigoArticulo = gUtilSQL.getIIF("renglonCotizacion.Serial <> '0'", " renglonCotizacion.CodigoArticulo  + ' Serial ' + renglonCotizacion.Serial + ' Rollo '+ renglonCotizacion.rollo ", " renglonCotizacion.CodigoArticulo", True) & "  as CodArt "
   SQL = SQL & " SELECT " & SQLCodigoArticulo & ", "
   SQL = SQL & " articuloInventario.Descripcion as Descripcion, "
   SQL = SQL & " Cotizacion.Numero as NumeroDocumento, "
   SQL = SQL & " renglonCotizacion.Cantidad as CantReservada, "
   SQL = SQL & " cliente.Nombre as NombreCliente, "
   SQL = SQL & " Cotizacion.fecha as fecha, "
   SQL = SQL & " RenglonCotizacion.Cantidad * renglonCotizacion.PrecioSinIVA as MontoTotal "
   SQL = SQL & " FROM (ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)"
   SQL = SQL & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) INNER JOIN ((cliente INNER JOIN Cotizacion ON (cliente.Codigo = Cotizacion.CodigoCliente) AND (cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)) "
   SQL = SQL & " INNER JOIN renglonCotizacion ON (Cotizacion.Numero = renglonCotizacion.NumeroCotizacion) AND (Cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)) ON (renglonCotizacion.CodigoArticulo = ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)"
   SQL = SQL & " WHERE   (Cotizacion.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonCotizacion.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT renglonCotizacion.CodigoArticulo As CodArt, "
   SQL = SQL & " articuloInventario.Descripcion As Descripcion, "
   SQL = SQL & " Cotizacion.Numero as NumeroDocumento, "
   SQL = SQL & " renglonCotizacion.Cantidad as CantReservada, "
   SQL = SQL & " cliente.Nombre as NombreCliente, "
   SQL = SQL & " Cotizacion.fecha as fecha, "
   SQL = SQL & " RenglonCotizacion.Cantidad * renglonCotizacion.PrecioSinIVA as MontoTotal "
   SQL = SQL & " FROM articuloInventario INNER JOIN (cliente INNER JOIN (Cotizacion INNER JOIN renglonCotizacion "
   SQL = SQL & " ON (Cotizacion.Numero = renglonCotizacion.NumeroCotizacion) AND (Cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)) ON (cliente.Codigo = Cotizacion.CodigoCliente) AND (cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)) ON (articuloInventario.ConsecutivoCompania = Cotizacion.ConsecutivoCompania) AND (articuloInventario.Codigo = renglonCotizacion.CodigoArticulo)"
   SQL = SQL & " WHERE   (Cotizacion.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonCotizacion.ConsecutivoCompania = " & valConsecutivoCompania & " )"
    SQL = SQL & "AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Simple) & ")"
  fConstruirSQLDelReporteArticulosReservados = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteArticulosReservados", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLParaLaImpresionDeCodDeBarras(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, _
                                                   ByVal valCodigoHasta As String, ByVal valNivelDePrecio As enum_NivelDePrecio, _
                                                   ByVal valPorcentajeAlicuotaGeneral As Currency, ByVal valPorcentajeAlicuotaReducida As Currency, ByVal valPorcentajeAlicuotaAdicional As Currency)
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vSQL As String
   Dim vSQLWhere As String
   Dim vPrecio As String
   Dim vSqlAlicuota As String
   Dim vSqlColorDescrip As String
   Dim vSqlTallaDescrip As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSQLWhere = ""
   vSqlColorDescrip = gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & "ISNULL(Color.DescripcionColor, " & gUtilSQL.fSimpleSqlValue("") & ")"
   vSqlTallaDescrip = gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & "ISNULL(Talla.DescripcionTalla, " & gUtilSQL.fSimpleSqlValue("") & ")"
   If gTexto.DfLen(valCodigoDesde) > 0 And gTexto.DfLen(valCodigoHasta) > 0 Then
      vSQLWhere = gUtilSQL.fSQLStringValueBetween("AI.Codigo", valCodigoDesde, valCodigoHasta) & "  AND  "
   End If
   vSQLWhere = vSQLWhere & "AI.ConsecutivoCompania = " & valConsecutivoCompania
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   vSqlAlicuota = " (CASE AI.AlicuotaIva WHEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_EXENTO) & " THEN 0 WHEN " _
   & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTAGENERAL) & " THEN " & gConvert.fConvierteAString(valPorcentajeAlicuotaGeneral) _
   & " WHEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTA2) & " THEN " & gConvert.fConvierteAString(valPorcentajeAlicuotaReducida) _
   & " WHEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTA3) & " THEN " & gConvert.fConvierteAString(valPorcentajeAlicuotaAdicional) & " ELSE 0 END)"
   vSQL = " SELECT AI.Codigo + ISNULL(EPG.CodigoColor, '') + ISNULL(EPG.CodigoTalla, '') AS CodigoArticulo, "
   vSQL = vSQL & "AI.Descripcion " & gUtilSQL.CharConcat & vSqlColorDescrip & gUtilSQL.CharConcat & vSqlTallaDescrip & " AS Descripcion, "
   vSQL = vSQL & "AI.AlicuotaIva AS AlicuotaIvaChr, "
   vSQL = vSQL & vSqlAlicuota & " AS AlicuotaIvaValor, "
   If (valNivelDePrecio = eND_PRECIO_4) Then
      vSQL = vSQL & "AI.PrecioSinIva4 AS PrecioSinIvaML, "
      vSQL = vSQL & "AI.PrecioConIva4 AS PrecioConIVAML, "
      vSQL = vSQL & "(AI.PrecioConIva4 - AI.PrecioSinIva4) AS IvaML, "
      vSQL = vSQL & "ISNULL(ME.MePrecioSinIva4, 0) AS PrecioSinIvaME, "
      vSQL = vSQL & "ISNULL(ME.MePrecioConIva4, 0) AS PrecioConIvaME, "
      vSQL = vSQL & "(ISNULL(ME.MePrecioConIva4, 0) - ISNULL(ME.MePrecioSinIva4, 0)) AS IvaME, "
   ElseIf (valNivelDePrecio = eND_PRECIO_3) Then
      vSQL = vSQL & "AI.PrecioSinIva3 AS PrecioSinIvaML, "
      vSQL = vSQL & "AI.PrecioConIva3 AS PrecioConIVAML, "
      vSQL = vSQL & "(AI.PrecioConIva3 - AI.PrecioSinIva3) AS IvaML, "
      vSQL = vSQL & "ISNULL(ME.MePrecioSinIva3, 0) AS PrecioSinIvaME, "
      vSQL = vSQL & "ISNULL(ME.MePrecioConIva3, 0) AS PrecioConIvaME, "
      vSQL = vSQL & "(ISNULL(ME.MePrecioConIva3, 0) - ISNULL(ME.MePrecioSinIva3, 0)) AS IvaME, "
   ElseIf (valNivelDePrecio = eND_PRECIO_2) Then
      vSQL = vSQL & "AI.PrecioSinIva2 AS PrecioSinIvaML, "
      vSQL = vSQL & "AI.PrecioConIva2 AS PrecioConIVAML, "
      vSQL = vSQL & "(AI.PrecioConIva2 - AI.PrecioSinIva2) AS IvaML, "
      vSQL = vSQL & "ISNULL(ME.MePrecioSinIva2, 0) AS PrecioSinIvaME, "
      vSQL = vSQL & "ISNULL(ME.MePrecioConIva2, 0) AS PrecioConIvaME, "
      vSQL = vSQL & "(ISNULL(ME.MePrecioConIva2, 0) - ISNULL(ME.MePrecioSinIva2, 0)) AS IvaME, "
   Else
      vSQL = vSQL & "AI.PrecioSinIva AS PrecioSinIvaML, "
      vSQL = vSQL & "AI.PrecioConIva AS PrecioConIVAML, "
      vSQL = vSQL & "(AI.PrecioConIva - AI.PrecioSinIva) AS IvaML, "
      vSQL = vSQL & "ISNULL(ME.MePrecioSinIva, 0) AS PrecioSinIvaME, "
      vSQL = vSQL & "ISNULL(ME.MePrecioConIva, 0) AS PrecioConIvaME, "
      vSQL = vSQL & "(ISNULL(ME.MePrecioConIva, 0) - ISNULL(ME.MePrecioSinIva, 0)) AS IvaME, "
   End If
   vSQL = vSQL & "AI.PorcentajeBaseImponible, AI.UnidadDeVenta, AI.CampoDefinible1, AI.CampoDefinible2, AI.CampoDefinible3, AI.CampoDefinible4, AI.CampoDefinible5, AI.UnidadDeVentaSecundaria "
   vSQL = vSQL & "FROM ArticuloInventario AS AI LEFT OUTER JOIN "
   vSQL = vSQL & "ExistenciaPorGrupo AS EPG INNER JOIN "
   vSQL = vSQL & "Saw.Talla ON EPG.CodigoTalla = Saw.Talla.CodigoTalla INNER JOIN "
   vSQL = vSQL & "Saw.Color ON EPG.CodigoColor = Saw.Color.CodigoColor ON AI.ConsecutivoCompania = Saw.Color.ConsecutivoCompania AND "
   vSQL = vSQL & "AI.ConsecutivoCompania = Saw.Talla.ConsecutivoCompania AND "
   vSQL = vSQL & "AI.Codigo = EPG.CodigoArticulo AND "
   vSQL = vSQL & "AI.CodigoGrupo = (CASE WHEN EPG.CodigoGrupo = '0' THEN '' ELSE EPG.CodigoGrupo END) AND AI.ConsecutivoCompania = EPG.ConsecutivoCompania LEFT OUTER JOIN "
   vSQL = vSQL & "CamposMonedaExtranjera AS ME ON AI.ConsecutivoCompania = ME.ConsecutivoCompania AND AI.Codigo = ME.Codigo "
   vSQL = vSQL & vSQLWhere
h_EXIT:   On Error GoTo 0
   fSQLParaLaImpresionDeCodDeBarras = vSQL
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlParaLaImpresionDeCodDeBarras", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteGananciaXProducto(ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, ByVal valenumCantidadAImprimirToString As String, ByVal valLineaDeProductos As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & " CODIGO , "
   SQL = SQL & " DESCRIPCION , "
   SQL = SQL & " PRECIOSINIVA , "
   SQL = SQL & " COSTOUNITARIO , "
   SQL = SQL & " LINEADEPRODUCTO , "
   SQL = SQL & " (PRECIOSINIVA  - COSTOUNITARIO ) AS Ganancia, "
   SQL = SQL & gUtilSQL.getIIF("COSTOUNITARIO  <> 0 ", "(((PRECIOSINIVA  - COSTOUNITARIO ) / COSTOUNITARIO ) * 100)", "0", True) & " AS Porcentaje"
   SQL = SQL & " FROM articuloInventario"
   SQL = SQL & " WHERE "
   SQL = SQL & " CONSECUTIVOCOMPANIA = " & valConsecutivoCompania
   SQL = SQL & " AND TIPODEARTICULO  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
       SQL = SQL & " AND LINEADEPRODUCTO  = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & " LINEADEPRODUCTO , "
   SQL = SQL & " CODIGO"
   fConstruirSQLDelReporteGananciaXProducto = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteGananciaXProducto", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteValoracionDeInventario(ByVal valPreciosConSinIva As String, _
                                                                  ByVal valUsaMonedaExtranjera As Boolean, ByVal valConsecutivoCompania As Long, _
                                                                     ByVal valCantidadAImprimirLineaDeProducto As String, ByVal valenumCantidadAImprimirToString As String, _
                                                                        ByVal valLineaDeProductos As String, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByRef gGlobalization As Object) As String
   Dim TraerCamposSinOConString As String
   Dim TraerCamposUsaMonedaExtranjera As String
   Dim SQL As String
   Dim SQLMePrecio As String
   Dim SQLMeValoracion As String
   Dim SQLValorDeVenta As String
   Dim SQLValorizadoConIva As String
   Dim SQLIvaValorizado As String
   Dim sqlTipoDeArticulo As String
   Dim preciosSinIva As Boolean
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   preciosSinIva = (valPreciosConSinIva = gEnumProyecto.enumPreciosDeLosProductosToString(ePD_PRECIOSINIVA, gGlobalization))
   sqlTipoDeArticulo = gUtilSQL.DfSQLCaseIfForEnum("articuloInventario.TIPODEARTICULO", enum_TipoDeArticulo.eTD_MERCANCIA, gEnumProyecto.fEnumTipoDeArticuloToStringInArray(True), "TipoDeArticuloStr")
   SQLValorizadoConIva = "articuloInventario.EXISTENCIA * articuloInventario.PRECIOCONIVA"
   SQLIvaValorizado = "(articuloInventario.EXISTENCIA * articuloInventario.PRECIOCONIVA) - (articuloInventario.EXISTENCIA * articuloInventario.PRECIOSINIVA)"
   SQL = "SELECT "
   SQL = SQL & "articuloInventario.CODIGO , "
   SQL = SQL & "articuloInventario.DESCRIPCION , "
   SQL = SQL & "articuloInventario.LINEADEPRODUCTO , "
   SQL = SQL & "articuloInventario.TIPODEARTICULO , "
   SQL = SQL & sqlTipoDeArticulo & ", "
   SQL = SQL & "articuloInventario.COSTOUNITARIO , "
   SQL = SQL & "articuloInventario.EXISTENCIA , "
   If preciosSinIva Then
      SQL = SQL & "articuloInventario.PRECIOSINIVA AS Precio, "
   Else
      SQL = SQL & "articuloInventario.PRECIOCONIVA AS Precio, "
   End If
   If valUsaMonedaExtranjera Then
      SQL = SQL & "articuloInventario.MECOSTOUNITARIO , "
      If preciosSinIva Then
         SQLMePrecio = "camposMonedaExtranjera.MEPRECIOSINIVA"
      Else
         SQLMePrecio = "camposMonedaExtranjera.MEPRECIOCONIVA"
      End If
      SQLMePrecio = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(SQLMePrecio), 0, SQLMePrecio)
      SQL = SQL & SQLMePrecio & " AS MePrecio, "
      SQLMeValoracion = "articuloInventario.EXISTENCIA  * articuloInventario.MECOSTOUNITARIO"
      SQLMeValoracion = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(SQLMeValoracion), 0, SQLMeValoracion)
      SQL = SQL & SQLMeValoracion & " AS MeValoracion, "
      SQLValorDeVenta = "articuloInventario.EXISTENCIA  * camposMonedaExtranjera.MEPRECIOSINIVA"
      SQLValorDeVenta = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(SQLValorDeVenta), 0, SQLValorDeVenta)
      SQL = SQL & SQLValorDeVenta & " AS MeValorDeVenta, "
   End If
   SQL = SQL & "(articuloInventario.EXISTENCIA  * articuloInventario.COSTOUNITARIO ) AS Valoracion, "
   SQL = SQL & "(articuloInventario.EXISTENCIA  * articuloInventario.PRECIOSINIVA ) AS ValorDeVenta, "
   SQL = SQL & SQLValorizadoConIva & " as ValorizadoConIva, "
   SQL = SQL & SQLIvaValorizado & " as IvaValorizado "
   SQL = SQL & " FROM "
      If valUsaMonedaExtranjera Then
         SQL = SQL & "( articuloInventario LEFT JOIN "
         SQL = SQL & "camposMonedaExtranjera ON ("
         SQL = SQL & "articuloInventario.CODIGO  = camposMonedaExtranjera.CODIGO"
         SQL = SQL & ") AND ("
         SQL = SQL & "articuloInventario.ConsecutivoCompania  = camposMonedaExtranjera.CONSECUTIVOCOMPANIA"
         SQL = SQL & ")) LEFT JOIN ("
         SQL = SQL & "articuloInventario AS ArtInventario RIGHT JOIN "
         SQL = SQL & "productoCompuesto  ON ("
         SQL = SQL & "ArtInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania"
         SQL = SQL & ") AND (ArtInventario.CODIGO  = productoCompuesto.CodigoArticulo"
         SQL = SQL & ")) ON ("
         SQL = SQL & "articuloInventario.CODIGO  = productoCompuesto.CodigoConexionConElMaster"
         SQL = SQL & ") AND (articuloInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania)"
      Else
         SQL = SQL & "articuloInventario LEFT JOIN ("
         SQL = SQL & "articuloInventario AS ArtInventario RIGHT JOIN productoCompuesto"
         SQL = SQL & " ON (ArtInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania"
         SQL = SQL & ") AND ("
         SQL = SQL & "ArtInventario.CODIGO  = productoCompuesto.CodigoArticulo"
         SQL = SQL & ")) ON ("
         SQL = SQL & "articuloInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania"
         SQL = SQL & ") AND (articuloInventario.CODIGO  = productoCompuesto.CodigoConexionConElMaster)"
      End If
   SQL = SQL & " WHERE "
   SQL = SQL & "articuloInventario.CONSECUTIVOCOMPANIA  = " & valConsecutivoCompania
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
      SQL = SQL & " AND articuloInventario.LINEADEPRODUCTO  = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   If valCodigoDesde <> "" And valCodigoHasta <> "" Then
      SQL = SQL & " AND " & gUtilSQL.fSQLStringValueBetween("articuloInventario.CODIGO", valCodigoDesde, valCodigoHasta)
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & "articuloInventario.LINEADEPRODUCTO , "
   SQL = SQL & "articuloInventario.CODIGO"
   fConstruirSQLDelReporteValoracionDeInventario = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteArticuloDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteHojaDeTrabajoDePedido(ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, _
                                                               ByVal valenumCantidadAImprimirToString As String, ByVal valLineaDeProductos As String, _
                                                                  ByVal valUsaAlmacen As Boolean, ByVal valCantidadAImprimirAlmacen As String, _
                                                                     ByVal valNombreAlmacen As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo , "
   SQL = SQL & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion + " & gUtilSQL.getIIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoTalla <> ''", "  ' ' + saw.talla.DescripcionTalla ", "''") & "+" & gUtilSQL.getIIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoColor <> ''", "  ' ' + saw.color.DescripcionColor", "''") & " AS Descripcion, "
   SQL = SQL & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.UnidadDeVenta, "
   SQL = SQL & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto, "
   SQL = SQL & "existenciaPorAlmacen.Cantidad, "
   SQL = SQL & "almacen.NombreAlmacen"
   SQL = SQL & " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
   SQL = SQL & " INNER JOIN ( almacen"
   SQL = SQL & " INNER JOIN  existenciaPorAlmacen"
   SQL = SQL & " ON ( almacen.Codigo = existenciaPorAlmacen.CodigoAlmacen"
   SQL = SQL & ") AND (almacen.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania))"
   SQL = SQL & " ON (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = existenciaPorAlmacen.CodigoArticulo)"
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania)"
   SQL = SQL & " LEFT JOIN Saw.Color ON IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoColor = Saw.Color.CodigoColor"
   SQL = SQL & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = Saw.Color.ConsecutivoCompania"
   SQL = SQL & " LEFT JOIN Saw.Talla ON  IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoTalla = Saw.Talla.CodigoTalla"
   SQL = SQL & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = Saw.Talla.ConsecutivoCompania"
   SQL = SQL & " WHERE "
   SQL = SQL & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.TipoDeArticulo  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
      SQL = SQL & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   If valUsaAlmacen And valCantidadAImprimirAlmacen = valenumCantidadAImprimirToString Then
      SQL = SQL & " AND almacen.NombreAlmacen = " & gUtilSQL.fSimpleSqlValue(Trim(valNombreAlmacen))
   End If
   SQL = SQL & " GROUP BY IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo , IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion , "
   SQL = SQL & " IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.UnidadDeVenta , IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto, "
   SQL = SQL & " almacen.NombreAlmacen, existenciaPorAlmacen.Cantidad, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo,"
   SQL = SQL & " saw.talla.DescripcionTalla, saw.Color.DescripcionColor, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoTalla, "
   SQL = SQL & " IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoColor"
   SQL = SQL & " ORDER BY "
   SQL = SQL & "almacen.NombreAlmacen, "
   SQL = SQL & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto, "
   SQL = SQL & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo"
   fConstruirSQLDelReporteHojaDeTrabajoDePedido = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteHojaDeTrabajoDePedido", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteDeProductoSinVenta(ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, _
                                                            ByVal valenumCantidadAImprimirToString As String, ByVal valLineaDeProductos As String, _
                                                               ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim SQL As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   SQL = "SELECT (ArticuloInventario.Codigo+"
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ExistenciaPorGrupo.CodigoColor"), gUtilSQL.fSimpleSqlValue(" "), "ExistenciaPorGrupo.CodigoColor") & "+"
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ExistenciaPorGrupo.CodigoTalla"), gUtilSQL.fSimpleSqlValue(" "), "ExistenciaPorGrupo.CodigoTalla") & ") as Codigo,"
   SQL = SQL & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   SQL = SQL & "ArticuloInventario.CostoUnitario,"
   SQL = SQL & "(" & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("SUM(ExistenciaPorGrupo.Existencia)"), "ArticuloInventario.Existencia", "SUM(ExistenciaPorGrupo.Existencia)") & ") as Existencia,"
   SQL = SQL & " ArticuloInventario.LineaDeProducto,"
   SQL = SQL & gUtilSQL.fMax(gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Factura.Fecha"), "Factura_1.Fecha", "Factura.Fecha"), "Fecha") & ","
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("factura.StatusFactura"), "Factura_1.StatusFactura", "Factura.StatusFactura", True) & " As StatusFactura"
   SQL = SQL & " FROM (((articuloInventario LEFT JOIN (ExistenciaPorGrupo LEFT JOIN (renglonFactura LEFT JOIN factura ON (renglonFactura.TipoDeDocumento = factura.TipoDeDocumento) AND (renglonFactura.NumeroFactura = factura.Numero) AND"
   SQL = SQL & " (renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania)) ON ((ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.codigoTalla) = renglonFactura.Articulo) AND"
   SQL = SQL & " (ExistenciaPorGrupo.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) ON (articuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo) AND (articuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania))"
   SQL = SQL & " LEFT JOIN Talla ON (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania)) LEFT JOIN Color ON (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor) AND"
   SQL = SQL & " (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania)) LEFT JOIN (renglonFactura AS renglonFactura_1 LEFT JOIN factura AS factura_1 ON"
   SQL = SQL & " (renglonFactura_1.NumeroFactura = factura_1.Numero) AND (renglonFactura_1.ConsecutivoCompania = factura_1.ConsecutivoCompania)) ON (articuloInventario.Codigo = renglonFactura_1.Articulo)"
   SQL = SQL & " AND (articuloInventario.ConsecutivoCompania = renglonFactura_1.ConsecutivoCompania)"
   sqlWhere = " WHERE  "
   sqlWhere = sqlWhere & "articuloInventario.CONSECUTIVOCOMPANIA = " & valConsecutivoCompania
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
      sqlWhere = sqlWhere & " AND articuloInventario.LINEADEPRODUCTO  = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   sqlWhere = sqlWhere & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   sqlWhere = sqlWhere & " OR " & gUtilSQL.DfSQLIsNull("Factura.StatusFactura") & ")"
   sqlWhere = sqlWhere & " AND (NOT " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   sqlWhere = sqlWhere & " OR " & gUtilSQL.DfSQLIsNull("factura.fecha") & ") AND "
   sqlWhere = sqlWhere & " (factura_1.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & " OR " & gUtilSQL.DfSQLIsNull("factura_1.StatusFactura") & ")"
   SQL = SQL & sqlWhere
   SQL = SQL & " GROUP BY ArticuloInventario.Codigo, articuloInventario.CostoUnitario, articuloInventario.Existencia, articuloInventario.LineaDeProducto, factura.StatusFactura, articuloInventario.Descripcion, Color.DescripcionColor, Talla.DescripcionTalla,ExistenciaPorGrupo.CodigoColor,ExistenciaPorGrupo.CodigoTalla,Factura_1.StatusFactura"
   SQL = SQL & " ORDER BY Codigo"
   fConstruirSQLDelReporteDeProductoSinVenta = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteDeProductoSinVenta", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExistenciaInicialMovInvFactura(ByVal valCodigoProducto As String, ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(IGV_ArticuloVenta_B2.Salidas)", 0) & " AS ExistenciaInicial " & vbCrLf
   SQL = SQL & " FROM IGV_ArticuloVenta_B2 "
   SQL = SQL & " WHERE (IGV_ArticuloVenta_B2.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto)) & ")"
   SQL = SQL & " AND (IGV_ArticuloVenta_B2.ConsecutivoCompania  = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "IGV_ArticuloVenta_B2.Fecha", valFechaInicial, "<") & ")" & vbCrLf
   fSQLExistenciaInicialMovInvFactura = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExistenciaInicialMovInvFactura", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteProductosPorDebajoDeExistenciaMinima(ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, ByVal valenumCantidadAImprimirToString As String, ByVal valLineaDeProductos As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "CODIGO , "
   SQL = SQL & "DESCRIPCION , "
   SQL = SQL & "CANTIDADMINIMA , "
   SQL = SQL & " EXISTENCIA, "
   SQL = SQL & " LINEADEPRODUCTO"
   SQL = SQL & " FROM articuloInventario"
   SQL = SQL & " WHERE CONSECUTIVOCOMPANIA = " & valConsecutivoCompania
   SQL = SQL & " AND EXISTENCIA  < CANTIDADMINIMA"
   SQL = SQL & " AND TIPODEARTICULO = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
      SQL = SQL & " AND LINEADEPRODUCTO  = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   SQL = SQL & " ORDER BY LINEADEPRODUCTO, CODIGO"
   fConstruirSQLDelReporteProductosPorDebajoDeExistenciaMinima = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteProductosPorDebajoDeExistenciaMinima", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLMovInvFactura(ByVal valCodigoProducto As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long) As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim sqlFact As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlFact = "SELECT "
   sqlFact = sqlFact & "Factura.Fecha AS Fecha, "
   sqlFact = sqlFact & "Factura.Numero As NoDocumento, "
   sqlFact = sqlFact & "RenglonFactura.Articulo As Codigo, "
   sqlFact = sqlFact & "0 AS CantidadEntrante, "
   sqlFact = sqlFact & "RenglonFactura.Cantidad  AS CantidadSaliente, "
   sqlFact = sqlFact & "articuloInventario.Descripcion AS Descripcion, "
   sqlFact = sqlFact & "articuloInventario.TipoDeArticulo AS TipoDeArticulo, "
   sqlFact = sqlFact & gUtilSQL.DfSQLCaseIfForEnum("Factura.TIPODEDOCUMENTO", enum_TipoDocumentoFactura.eTF_FACTURA, gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True), "TipoOperacion")
   sqlFact = sqlFact & ",Factura.NUMERO, "
   sqlFact = sqlFact & "RenglonFactura.ConsecutivoRenglon AS ConsecutivoRenglon"
   sqlFact = sqlFact & " FROM Factura  INNER JOIN (articuloInventario INNER JOIN RenglonFactura "
   sqlFact = sqlFact & " ON (articuloInventario.Codigo  = RenglonFactura.Articulo "
   sqlFact = sqlFact & ") AND (articuloInventario.ConsecutivoCompania  = RenglonFactura.ConsecutivoCompania"
   sqlFact = sqlFact & ")) ON (Factura.Numero  = RenglonFactura.NumeroFactura "
   sqlFact = sqlFact & ") AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania"
   sqlFact = sqlFact & ") AND (Factura.TipoDeDocumento  = RenglonFactura.TipoDeDocumento )"
   
   sqlWhere = " WHERE (  articuloInventario.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto)) & ")"
   sqlWhere = sqlWhere & " AND (articuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   sqlWhere = sqlWhere & " AND (Factura.StatusFactura  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   sqlWhere = sqlWhere & " AND (" & gUtilSQL.DfSQLDateValueBetween("Factura.FECHA", valFechaInicial, valFechaFinal) & ")"
   sqlWhere = sqlWhere & " AND (articuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO) & ")" & vbCrLf
   sqlWhere = sqlWhere & " AND (factura.GeneradaPorNotaEntrega <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_FacturaGeneraNotaEntrega.eFGNE_Generada) & ")" & vbCrLf
  
   sqlFact = sqlFact & sqlWhere
   sqlFact = sqlFact & " UNION SELECT "
   sqlFact = sqlFact & "Factura.Fecha  Fecha, "
   sqlFact = sqlFact & "Factura.Numero NoDocumento, "
   sqlFact = sqlFact & "RenglonFactura.Articulo As Codigo, "
   sqlFact = sqlFact & "0 AS CantidadEntrante, "
   sqlFact = sqlFact & "RenglonFactura.Cantidad AS CantidadSaliente, "
   sqlFact = sqlFact & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlFact = sqlFact & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlFact = sqlFact & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   sqlFact = sqlFact & "articuloInventario.TipoDeArticulo  AS TipoDeArticulo, "
   sqlFact = sqlFact & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True), "TipoOperacion")
   sqlFact = sqlFact & ", Factura.Numero, "
   sqlFact = sqlFact & "RenglonFactura.ConsecutivoRenglon AS ConsecutivoRenglon"
   sqlFact = sqlFact & " FROM ((factura INNER JOIN (renglonFactura INNER JOIN (ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)) ON (renglonFactura.Articulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.codigoTalla)) AND (renglonFactura.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) AND (factura.Numero = renglonFactura.NumeroFactura) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) LEFT JOIN Color ON (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor) AND (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania)) LEFT JOIN Talla ON (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania)"
   sqlFact = sqlFact & sqlWhere
   Set gEnumProyecto = New clsEnumAdministrativo
   fSQLMovInvFactura = sqlFact
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLMovInvFactura", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteExistenciaPorTallaYColor(ByVal valConsecutivoCompaniaActual As Long, ByVal valCantidadAImprimir As String, ByVal valTipoDeArticulo As String, ByVal valLineaDeProducto As String) As String
   Dim SQL As String
   Dim SQLSerial As String
   Dim SQLRollo As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & " ArticuloInventario.codigo  AS  CodArt, "
   SQL = SQL & " ArticuloInventario.descripcion  AS Descripcion,"
   SQL = SQL & " ArticuloInventario.precioSinIva  AS precioSinIva, "
   SQL = SQL & " ArticuloInventario.PrecioSinIVA2 AS precioSinIva2, "
   SQL = SQL & " ArticuloInventario.precioSinIva3  AS precioSinIva3, "
   SQL = SQL & " ArticuloInventario.precioSinIva4  AS precioSinIva4, "
   SQL = SQL & " ExistenciaPorGrupo.existencia AS  Existencia,  "
   SQL = SQL & " Talla.DescripcionTalla AS  DescripcionTalla,  "
   SQL = SQL & " ArticuloInventario.codigo + Talla.CodigoTalla + Color.CodigoColor as CodArtTallaYColor,"
   SQL = SQL & " Color.DescripcionColor AS  DescripcionColor, "
   SQLSerial = gUtilSQL.getIIF("ExistenciaPorGRupo.Serial = '0' ", " ''", " ExistenciaPorGrupo.Serial", True) & "  as Serial "
   SQL = SQL & SQLSerial & ", "
   SQLRollo = gUtilSQL.getIIF("ExistenciaPorGRupo.Rollo = '0' ", " ''", " ExistenciaPorGrupo.Rollo", True) & "  as Rollo "
   SQL = SQL & SQLRollo & " "
   SQL = SQL & " FROM  Talla  INNER JOIN (Color  INNER JOIN (ArticuloInventario  INNER JOIN ExistenciaPorGrupo "
   SQL = SQL & " ON (ArticuloInventario.codigo = ExistenciaPorGrupo.CodigoArticulo ) AND ( ArticuloInventario.consecutivoCompania = ExistenciaPorGrupo.consecutivoCompania ))"
   SQL = SQL & " ON ( Color.consecutivoCompania = ExistenciaPorGrupo.consecutivoCompania ) AND ( Color.CodigoColor = ExistenciaPorGrupo.CodigoColor )) ON (Talla.consecutivoCompania = ExistenciaPorGrupo.consecutivoCompania) AND "
   SQL = SQL & "( Talla.CodigoTalla = ExistenciaPorGrupo.CodigoTalla )"
   SQL = SQL & " WHERE "
   SQL = SQL & " ArticuloInventario.ConsecutivoCompania  = " & valConsecutivoCompaniaActual
   If valCantidadAImprimir = enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND articuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProducto))
   End If
   If valTipoDeArticulo <> gDefgen.getSearchAllComboBox Then
      SQL = SQL & " AND TIPODEARTICULO  = " & gUtilSQL.fSQLSimpleValueForEnum(strTipoDeArticuloToNum(valTipoDeArticulo))
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & " ArticuloInventario.codigo, "
   SQL = SQL & " Color.CodigoColor "
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteExistenciaPorTallaYColor = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteExistenciaPorTallaYColor", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteVencimientoDeProducto(ByVal valConsecutivoCompaniaActual As Long, ByVal valCantidadAImprimir As String, ByVal valLineaDeProducto As String)
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "ArticuloInventario.Codigo, "
   SQL = SQL & "ArticuloInventario.Descripcion, "
   SQL = SQL & "ArticuloInventario.FechaDeVencimiento, "
   SQL = SQL & "ArticuloInventario.LineaDeProducto"
   SQL = SQL & " FROM ArticuloInventario"
   SQL = SQL & " WHERE ArticuloInventario.ConsecutivoCompania  = " & valConsecutivoCompaniaActual
   SQL = SQL & " AND ArticuloInventario.TIPODEARTICULO  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   SQL = SQL & " AND (NOT " & gUtilSQL.DfSQLIsNull("ArticuloInventario.FechaDeVencimiento") & ")"
   If valCantidadAImprimir = enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND ArticuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProducto))
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & "ArticuloInventario.lineaDeProducto, "
   SQL = SQL & "ArticuloInventario.FechaDeVencimiento, "
   SQL = SQL & "ArticuloInventario.Codigo"
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteVencimientoDeProducto = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteVencimientoDeProducto", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExistenciaPorGrupo(ByVal valCodigoGrupo As String, ByVal valCodigoArticulo As String, ByVal valCodigoCodigoTalla As String, ByVal valCodigoColor As String, ByVal valCodigoCompaniaActual As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT SUM(ExistenciaPorGrupo.CantReservada) AS CantReservada,"
   SQL = SQL & " SUM(ExistenciaPorGrupo.Existencia) AS Existencia"
   SQL = SQL & " FROM ExistenciaPorGrupo WHERE "
   SQL = SQL & " ExistenciaPorGrupo.ConsecutivoCompania = " & valCodigoCompaniaActual
   SQL = SQL & " AND ExistenciaPorGrupo.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigoArticulo)
   SQL = SQL & " AND ExistenciaPorGrupo.CodigoGrupo = " & gUtilSQL.fSimpleSqlValue(valCodigoGrupo)
   SQL = SQL & " AND ExistenciaPorGrupo.CodigoTalla = " & gUtilSQL.fSimpleSqlValue(valCodigoCodigoTalla)
   SQL = SQL & " AND ExistenciaPorGrupo.CodigoColor = " & gUtilSQL.fSimpleSqlValue(valCodigoColor)
   SQL = SQL & " GROUP BY CodigoArticulo "
h_EXIT: On Error GoTo 0
   fSQLExistenciaPorGrupo = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExistenciaPorGrupo", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLMovInvFacturaPC(ByVal valCodigoProducto As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long) As String
   Dim sqlFact As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlFact = "SELECT "
   sqlFact = sqlFact & "Factura.FECHA , "
   sqlFact = sqlFact & "Factura.NUMERO" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" (*) ") & ", "
   sqlFact = sqlFact & "productoCompuesto.CodigoArticulo, "
   sqlFact = sqlFact & "0 AS CantidadEntrante, "
   sqlFact = sqlFact & "(RenglonFactura.CANTIDAD  * productoCompuesto.Cantidad), "
   sqlFact = sqlFact & "articuloInventario.DESCRIPCION , "
   sqlFact = sqlFact & "articuloInventario.TIPODEARTICULO , "
   sqlFact = sqlFact & gUtilSQL.DfSQLCaseIfForEnum("Factura.TIPODEDOCUMENTO", enum_TipoDocumentoFactura.eTF_FACTURA, gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True), "") & ", "
   sqlFact = sqlFact & "Factura.NUMERO, "
   sqlFact = sqlFact & "RenglonFactura.CONSECUTIVORENGLON AS CONSECUTIVO_RENGLON"
   sqlFact = sqlFact & " FROM Factura INNER JOIN (("
   sqlFact = sqlFact & " articuloInventario INNER JOIN productoCompuesto ON ("
   sqlFact = sqlFact & "articuloInventario.CODIGO  = productoCompuesto.CodigoConexionConElMaster"
   sqlFact = sqlFact & ") AND (articuloInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania"
   sqlFact = sqlFact & ")) INNER JOIN RenglonFactura"
   sqlFact = sqlFact & " ON (articuloInventario.CODIGO  = RenglonFactura.ARTICULO"
   sqlFact = sqlFact & ") AND (articuloInventario.CONSECUTIVOCOMPANIA  = RenglonFactura.CONSECUTIVOCOMPANIA"
   sqlFact = sqlFact & ")) ON (Factura.NUMERO  = RenglonFactura.NUMEROFACTURA"
   sqlFact = sqlFact & ") AND (Factura.CONSECUTIVOCOMPANIA = RenglonFactura.CONSECUTIVOCOMPANIA"
   sqlFact = sqlFact & ") AND (Factura.TIPODEDOCUMENTO = RenglonFactura.TIPODEDOCUMENTO)"
   sqlFact = sqlFact & " WHERE (productoCompuesto.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto)) & ")"
   sqlFact = sqlFact & " AND (articuloInventario.CONSECUTIVOCOMPANIA = " & valConsecutivoCompania & ")"
   sqlFact = sqlFact & " AND (Factura.STATUSFACTURA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   sqlFact = sqlFact & " AND (" & gUtilSQL.DfSQLDateValueBetween("Factura.FECHA", valFechaInicial, valFechaFinal) & ")"
   Set gEnumProyecto = Nothing
   fSQLMovInvFacturaPC = sqlFact
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLMovInvFacturaPC", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLMovInvNotaES(ByVal valCodigoProducto As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long) As String
   Dim sqlNotaES As String
   Dim sqlWhere As String
   Dim SQLCodigo As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQLCodigo = gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoProducto)
   sqlWhere = gUtilSQL.fSQLValueWithAnd("", "articuloInventario.CODIGO", SQLCodigo, False)
   sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, "articuloInventario.CONSECUTIVOCOMPANIA", valConsecutivoCompania)
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(sqlWhere, "notaDeEntradaSalida.Fecha", valFechaInicial, valFechaFinal)
   sqlWhere = gUtilSQL.fSQLEnumValueWithAnd(sqlWhere, "notaDeEntradaSalida.StatusNotaEntradaSalida", enum_StatusNotaEntradaSalida.eSNES_Vigente)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   
   sqlNotaES = "SELECT "
   sqlNotaES = sqlNotaES & "notaDeEntradaSalida.Fecha, "
   sqlNotaES = sqlNotaES & "notaDeEntradaSalida.NumeroDocumento, "
   sqlNotaES = sqlNotaES & "articuloInventario.CODIGO , "
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF("notaDeEntradaSalida.TipodeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO), "renglonNotaES.Cantidad", "0") & ", "
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF(fSqlCondicionTipoOperacion(), "renglonNotaES.Cantidad", "0", True) & ", "
   sqlNotaES = sqlNotaES & "articuloInventario.DESCRIPCION , "
   sqlNotaES = sqlNotaES & "articuloInventario.TIPODEARTICULO , "
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO) & " AND  NotaDeEntradaSalida.GeneradoPor = '0' " _
   , "'Entrada de Inventario'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO) & " AND  NotaDeEntradaSalida.GeneradoPor = '1' " _
   , "'Entrada producto terminado'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_SALIDA_DE_INVENTARIO) & " AND  NotaDeEntradaSalida.GeneradoPor = '0' " _
   , "'Salida de Inventario'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_SALIDA_DE_INVENTARIO) & " AND  NotaDeEntradaSalida.GeneradoPor = '1' " _
   , "'Salida Inventario - Orden Prod.'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_Autoconsumo) _
   , "'Autoconsumo'", "'Retiro'"))))) & " AS TipoOperacion, "

   sqlNotaES = sqlNotaES & " notaDeEntradaSalida.NumeroDocumento AS Numero, "
   sqlNotaES = sqlNotaES & "renglonNotaES.consecutivoRenglon AS CONSECUTIVO_RENGLON"
   sqlNotaES = sqlNotaES & " FROM notaDeEntradaSalida " & _
                  " INNER JOIN (articuloInventario INNER JOIN renglonNotaES" & _
                  " ON (articuloInventario.CODIGO = renglonNotaES.CodigoArticulo" & _
                  ") AND (articuloInventario.CONSECUTIVOCOMPANIA" & _
                  " = renglonNotaES.ConsecutivoCompania)) ON (notaDeEntradaSalida.NumeroDocumento" & _
                  " = renglonNotaES.NumeroDocumento" & _
                  ") AND (notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania)"
   sqlNotaES = sqlNotaES & sqlWhere
   sqlNotaES = sqlNotaES & " UNION SELECT "
   sqlNotaES = sqlNotaES & "notaDeEntradaSalida.Fecha, "
   sqlNotaES = sqlNotaES & "notaDeEntradaSalida.NumeroDocumento, "
   sqlNotaES = sqlNotaES & "RenglonNotaEs.CodigoArticulo as Codigo, "
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO), "renglonNotaES.Cantidad", "0") & ", "
   
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF(fSqlCondicionTipoOperacion(), "renglonNotaES.Cantidad", "0", True) & ", "
   sqlNotaES = sqlNotaES & "(ArticuloInventario.Descripcion +" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   sqlNotaES = sqlNotaES & "articuloInventario.TipoDeArticulo, "
    sqlNotaES = sqlNotaES & gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO) & " AND  NotaDeEntradaSalida.GeneradoPor = '0' " _
   , "'Entrada de Inventario'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO) & " AND  NotaDeEntradaSalida.GeneradoPor = '1' " _
   , "'Entrada de Inventario Por Orden de Producción'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_SALIDA_DE_INVENTARIO) & " AND  NotaDeEntradaSalida.GeneradoPor = '0' " _
   , "'Salida de Inventario'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_SALIDA_DE_INVENTARIO) & " AND  NotaDeEntradaSalida.GeneradoPor = '1' " _
   , "'Salida de Inventario Por Orden de Producción'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_Autoconsumo) _
   , "'Autoconsumo'", "'Retiro'"))))) & " AS TipoOperacion, "
   sqlNotaES = sqlNotaES & " notaDeEntradaSalida.NumeroDocumento AS Numero, "
   sqlNotaES = sqlNotaES & "renglonNotaES.ConsecutivoRenglon AS CONSECUTIVO_RENGLON"
   sqlNotaES = sqlNotaES & " FROM notaDeEntradaSalida INNER JOIN ((((renglonNotaES INNER JOIN ExistenciaPorGrupo ON "
   sqlNotaES = sqlNotaES & " (renglonNotaES.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoCOLOR+ExistenciaPorGrupo.CodigoTALLA))"
   sqlNotaES = sqlNotaES & " AND (renglonNotaES.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) LEFT JOIN articuloInventario ON "
   sqlNotaES = sqlNotaES & " (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) AND "
   sqlNotaES = sqlNotaES & " (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)) LEFT JOIN Talla ON "
   sqlNotaES = sqlNotaES & " (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla)) LEFT JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)) ON (notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento) "
   sqlNotaES = sqlNotaES & " AND (notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania)"
   sqlNotaES = sqlNotaES & sqlWhere
   Set gEnumProyecto = Nothing
   fSQLMovInvNotaES = sqlNotaES
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLMovInvNotaES", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDetalleDelInventarioPermanenteEnUnidadesFisicas(ByVal valCodigoCompaniaActual As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim SQL As String
   Dim ExistenciaInicial As String
   Dim insInvPermanenteVista As clsArticuloInvVista
   On Error GoTo h_ERROR
   Set insInvPermanenteVista = New clsArticuloInvVista
   
   SQL = "(SELECT "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("SUM(" & gUtilSQL.fIfResultIsNullReplace("Entradas", "0") & " - " & gUtilSQL.fIfResultIsNullReplace("Salidas", "0") & ")", "0")
   SQL = SQL & " FROM  " & insInvPermanenteVista.GetViewNameRegistroInventarioPermanente & " AS InventarioInicial"
   SQL = SQL & " WHERE (InventarioInicial.ConsecutivoCompania = MovimientoDeInventario.ConsecutivoCompania) "
   SQL = SQL & " AND (InventarioInicial.CodigoArticulo = MovimientoDeInventario.CodigoArticulo) "
   SQL = SQL & " AND (InventarioInicial.Fecha < " & gUtilSQL.fSimpleSqlValue(valFechaInicial) & "))"
   ExistenciaInicial = SQL
   
   SQL = "SELECT "
   SQL = SQL & " MovimientoDeInventario.Fecha, "
   SQL = SQL & gUtilSQL.getIIF(" dbo.NotaDeEntradaSalida.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoGeneradoPorNotaDeEntradaSalida.eTGPNDES_OrdenDeProduccion), " MovimientoDeInventario.TipoDeOperacion + ' Nro ' + ISNULL(Adm.OrdenDeProduccion.Codigo, '') ", " MovimientoDeInventario.TipoDeOperacion ") + " AS TipoDeOperacion, "
   SQL = SQL & " MovimientoDeInventario.NumeroDeDocumento, "
   SQL = SQL & " MovimientoDeInventario.CodigoArticulo, "
   SQL = SQL & " MovimientoDeInventario.Descripcion, "
   SQL = SQL & " MovimientoDeInventario.UnidadDeVenta, "
   SQL = SQL & ExistenciaInicial & " AS ExistenciaInicial, "
   SQL = SQL & " MovimientoDeInventario.Entradas, "
   SQL = SQL & " MovimientoDeInventario.Salidas "
   SQL = SQL & " FROM  " & insInvPermanenteVista.GetViewNameRegistroInventarioPermanente & " MovimientoDeInventario "
   SQL = SQL & " LEFT JOIN dbo.NotaDeEntradaSalida ON MovimientoDeInventario.ConsecutivoCompania = dbo.NotaDeEntradaSalida.ConsecutivoCompania "
   SQL = SQL & " AND MovimientoDeInventario.NumeroDeDocumento = dbo.NotaDeEntradaSalida.NumeroDocumento "
   SQL = SQL & " LEFT JOIN adm.OrdenDeProduccion ON MovimientoDeInventario.ConsecutivoCompania = adm.OrdenDeProduccion.ConsecutivoCompania "
   SQL = SQL & " AND dbo.NotaDeEntradaSalida.ConsecutivoDocumentoOrigen = adm.OrdenDeProduccion.Consecutivo "
   SQL = SQL & " WHERE MovimientoDeInventario.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valCodigoCompaniaActual)
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("MovimientoDeInventario.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " ORDER BY MovimientoDeInventario.CodigoArticulo, MovimientoDeInventario.Fecha"

   Set insInvPermanenteVista = Nothing
h_EXIT: On Error GoTo 0
   fSQLDetalleDelInventarioPermanenteEnUnidadesFisicas = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDetalleDelInventarioPermanenteEnUnidadesFisicas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDetalleDelInventarioPermanenteValorizado(ByVal valCodigoCompaniaActual As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef gGlobalization As Object, ByVal valCostoUltimaCompra As Boolean) As String
   Dim SQL As String
   Dim ExistenciaInicial As String
   Dim insInvPermanenteVista As clsArticuloInvVista
   On Error GoTo h_ERROR
   Set insInvPermanenteVista = New clsArticuloInvVista
   If valCostoUltimaCompra Then
      SQL = fSQLDetalleDelInventarioPermanenteValorizadoUltimaCompra(valCodigoCompaniaActual, valFechaInicial, valFechaFinal)
   Else
      SQL = "SELECT "
      SQL = SQL & " Fecha, "
      SQL = SQL & " TipoDeDocumento, "
      SQL = SQL & " TipoDeOperacion, "
      If gGlobalization.fEsCodigoPeru Then
         SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfInStrSQL("NumeroDeDocumento", "-") & " - 1 > 0", gUtilSQL.fMid("NumeroDeDocumento", "1", gUtilSQL.DfInStrSQL("NumeroDeDocumento", "-") & " - 1"), "NumeroDeDocumento", True) & " AS SerieDeDocumento, "
         SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfInStrSQL("NumeroDeDocumento", "-") & " - 1 > 0", gUtilSQL.fMid("NumeroDeDocumento", gUtilSQL.DfInStrSQL("NumeroDeDocumento", "-") & "+1", gUtilSQL.fLen(insInvPermanenteVista.GetViewNameRegistroInventarioPermanenteValorizado, "NumeroDeDocumento")), "NumeroDeDocumento", True) & " AS NumeroDeDocumento, "
      Else
         SQL = SQL & " NumeroDeDocumento, "
      End If
      SQL = SQL & " CodigoArticulo, "
      SQL = SQL & " Descripcion, "
      SQL = SQL & " UnidadDeVenta, "
      SQL = SQL & " CostoUnitario, "
      SQL = SQL & " Entradas, "
      SQL = SQL & " CostoUnitarioEntrada, "
      SQL = SQL & " CostoTotalEntrante, "
      SQL = SQL & " Salidas, "
      SQL = SQL & " CostoUnitarioSalida, "
      SQL = SQL & " CostoTotalSaliente "
      SQL = SQL & " FROM  " & insInvPermanenteVista.GetViewNameRegistroInventarioPermanenteValorizado
      SQL = SQL & " WHERE ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valCodigoCompaniaActual)
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Fecha", valFechaInicial, valFechaFinal)
      SQL = SQL & " ORDER BY CodigoArticulo, Fecha"
   End If
   Set insInvPermanenteVista = Nothing
h_EXIT: On Error GoTo 0
   fSQLDetalleDelInventarioPermanenteValorizado = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDetalleDelInventarioPermanenteValorizado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExistenciaDetalladaDeArticulo(ByVal valSqlCamposFiltro As String, ByVal valSqlFiltro As String, ByVal valSqlOrder As String, ByVal valConsecutivoCompania As String) As String
   Dim SQL As String
   Dim sqlWhere As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim insArticuloInvVista As clsArticuloInvVista
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set insArticuloInvVista = New clsArticuloInvVista
   
   sqlWhere = gUtilSQL.fSQLEnumValueWithAnd(valSqlFiltro, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   sqlWhere = sqlWhere & " AND ArticuloInventario.Codigo NOT LIKE 'RD_%'"
   sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
     
   SQL = "SELECT "
   SQL = SQL & valSqlCamposFiltro
   SQL = SQL & "articuloInventario.Codigo, "
   SQL = SQL & "articuloInventario.TipoArticuloInv, "
   SQL = SQL & "articuloInventario.Descripcion, "
   SQL = SQL & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".Serial,"
   SQL = SQL & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".Rollo,"
   SQL = SQL & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionColor,"
   SQL = SQL & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionTalla,"
   SQL = SQL & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".Existencia,"
   SQL = SQL & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".CantReservada,"
   SQL = SQL & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DisponibilidadDetalle,"
   SQL = SQL & " articuloInventario.Existencia AS ExistenciaMaster,"
   SQL = SQL & " articuloInventario.CantArtReservado AS CantReservadaMaster,"
   SQL = SQL & " articuloInventario.Existencia - articuloInventario.CantArtReservado AS DisponibilidadMaster,"
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " AS StatusdelArticulo "
   SQL = SQL & " From articuloInventario Left Join "
   SQL = SQL & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & " ON "
   SQL = SQL & "  articuloInventario.ConsecutivoCompania = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".ConsecutivoCompania"
   SQL = SQL & "  AND ArticuloInventario.Codigo = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".CodigoArticulo"
   SQL = SQL & sqlWhere
   SQL = SQL & " ORDER BY " & valSqlOrder
   
   Set gEnumProyecto = Nothing
   Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
   fSQLExistenciaDetalladaDeArticulo = SQL
   
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExistenciaDetalladaDeArticulo", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLUbicacionXAlmacenDeArticulo(ByVal valConsecutivoCompania As Long, ByVal valCodigoAlmacen As String, ByVal valLineaDeProducto As String, ByVal valMarca As String, ByVal valCategoria As String, ByVal valOrdenadoPorCodigo As Boolean, ByVal valSerial As String, ByVal valRollo As String, ByVal valCodigoIncio As String, ByVal valCodigoFin As String) As String
   Dim insArticuloView As clsArticuloInvVista
   Dim SQL As String
   On Error GoTo h_ERROR
   Set insArticuloView = New clsArticuloInvVista
   
   SQL = "SELECT Codigo, Descripcion, Serial, Rollo ,Ubicacion, ConsecutivoCompania, CodigoAlmacen, CodigoArticulo "
   SQL = SQL & " From  " & insArticuloView.GetViewNameUbicacionXAlmacenDeArticulo
   SQL = SQL & " WHERE (ConsecutivoCompania = " & valConsecutivoCompania & ") "
   SQL = SQL & "AND (CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen) & ") "
   
   If gTexto.DfLen(valLineaDeProducto) > 0 Then
      SQL = SQL & "AND (LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(valLineaDeProducto) & ") "
   End If
   If gTexto.DfLen(valMarca) > 0 Then
      SQL = gUtilSQL.fSQLValueWithWildcardWithAnd(SQL, "Marca", valMarca)
   End If
   If gTexto.DfLen(valCategoria) > 0 Then
      SQL = SQL & "AND (Categoria = " & gUtilSQL.fSimpleSqlValue(valCategoria) & ") "
   End If
   If gTexto.DfLen(valSerial) > 0 Then
      SQL = gUtilSQL.fSQLValueWithWildcardWithAnd(SQL, "Serial ", valSerial)
   End If
   If gTexto.DfLen(valRollo) > 0 Then
      SQL = gUtilSQL.fSQLValueWithWildcardWithAnd(SQL, "Rollo ", valRollo)
   End If
   If gTexto.DfLen(valCodigoIncio) > 0 And gTexto.DfLen(valCodigoFin) > 0 Then
      SQL = SQL & "AND " & gUtilSQL.fSQLValueBetween("Codigo", valCodigoIncio, valCodigoFin)
   End If
   SQL = SQL & " ORDER BY "
   If valOrdenadoPorCodigo Then
      SQL = SQL & " Codigo, Descripcion"
   Else
      SQL = SQL & " Descripcion "
   End If
   Set insArticuloView = Nothing
h_EXIT: On Error GoTo 0
   fSQLUbicacionXAlmacenDeArticulo = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLUbicacionXAlmacenDeArticulo", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCondicionTipoOperacion() As String
   Dim vResult As String
   On Error GoTo h_ERROR
    vResult = ""
    vResult = gUtilSQL.fSQLEnumValueWithAnd("", "notaDeEntradaSalida.TipodeOperacion", enum_TipodeOperacion.eTO_SALIDA_DE_INVENTARIO)
    vResult = vResult & " OR  " & gUtilSQL.fSQLEnumValueWithAnd("", "notaDeEntradaSalida.TipodeOperacion", enum_TipodeOperacion.eTO_Autoconsumo)
    vResult = vResult & " OR  " & gUtilSQL.fSQLEnumValueWithAnd("", "notaDeEntradaSalida.TipodeOperacion", enum_TipodeOperacion.eTO_Retiro)
    vResult = "(" & vResult & ")"
   fSqlCondicionTipoOperacion = vResult
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewDisponibilidadXArticulo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExistenciaMercanciaEnTransito(ByVal valConsecutivoCompania As String, _
                                                   ByVal valSqlStatus As String, ByVal valRango As Boolean, _
                                                    ByVal valLineaProducto As Boolean, ByVal valCategoria As Boolean, _
                                                     ByVal valMarca As Boolean, valSqlOrder As String, _
                                                      ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, _
                                                       ByVal valStrLineaProducto As String, ByVal valStrCategoria As String, _
                                                        ByVal valStrMarca As String) As String
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   'Artículo Sencillo
   SQL = "SELECT "
   SQL = SQL & " articuloInventario.Codigo AS codigo, articuloInventario.Descripcion as Descripcion, "
   SQL = SQL & " articuloInventario.Existencia as Existencia,"
   SQL = SQL & " (articuloInventario.CantArtReservado) AS CantidadReservada,"
   SQL = SQL & " IGV_InventarioMercanciaEnTransito.CantidadEnTransito as CantidadEnTransito, "
   SQL = SQL & "(articuloInventario.Existencia - articuloInventario.CantArtReservado + IGV_InventarioMercanciaEnTransito.CantidadEnTransito) AS CantidadDisponible, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " as StatusdelArticulo "
   SQL = SQL & " , articuloinventario.LineaDeProducto as LineaDeProducto, "
   SQL = SQL & " articuloInventario.categoria, articuloInventario.marca"
   SQL = SQL & " FROM IGV_InventarioMercanciaEnTransito"
   SQL = SQL & " INNER JOIN ArticuloInventario"
   SQL = SQL & " ON (IGV_InventarioMercanciaEnTransito.CodigoArticulo = ArticuloInventario.Codigo"
   SQL = SQL & " AND IGV_InventarioMercanciaEnTransito.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania)"
   SQL = SQL & " WHERE IGV_InventarioMercanciaEnTransito.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND TipoArticuloInv IN ( " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Simple) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaSerial) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaSerial_Rollo) & ")  "
   SQL = SQL & " AND ArticuloInventario.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_MERCANCIA
   SQL = SQL & " AND IGV_InventarioMercanciaEnTransito.EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(True)
   If valSqlStatus <> "" Then
      SQL = SQL & " AND ArticuloInventario.StatusdelArticulo = " & valSqlStatus
   End If
   If valRango Then
      SQL = SQL & " AND " & gUtilSQL.fSQLStringValueBetween("ArticuloInventario.Codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   End If
   If valLineaProducto Then
      SQL = SQL & " AND ArticuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrLineaProducto))
   End If
   If valCategoria Then
      SQL = SQL & " AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrCategoria))
   End If
   If valMarca Then
      SQL = SQL & " AND ArticuloInventario.Marca = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrMarca))
   End If
   SQL = SQL & " UNION"
   'OtroArticulos
   SQL = SQL & " SELECT IGV_InventarioExistenciaXGrupo.Codigo AS codigo, IGV_InventarioExistenciaXGrupo.Descripcion as Descripcion,"
   SQL = SQL & " IGV_InventarioExistenciaXGrupo.Existencia as Existencia,"
   SQL = SQL & " (IGV_InventarioExistenciaXGrupo.CantidadReservada) AS CantidadReservada,"
   SQL = SQL & " IGV_InventarioMercanciaEnTransito.CantidadEnTransito as CantidadEnTransito,"
   SQL = SQL & " (IGV_InventarioExistenciaXGrupo.Existencia - IGV_InventarioExistenciaXGrupo.CantidadReservada +"
   SQL = SQL & " IGV_InventarioMercanciaEnTransito.CantidadEnTransito) AS CantidadDisponible, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("IGV_InventarioExistenciaXGrupo.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, _
                        gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " as StatusdelArticulo "
   SQL = SQL & " , IGV_InventarioExistenciaXGrupo.LineaDeProducto as LineaDeProducto, "
   SQL = SQL & " IGV_InventarioExistenciaXGrupo.categoria, IGV_InventarioExistenciaXGrupo.marca "
   SQL = SQL & " FROM IGV_InventarioMercanciaEnTransito"
   SQL = SQL & " INNER JOIN IGV_InventarioExistenciaXGrupo"
   SQL = SQL & " ON (IGV_InventarioMercanciaEnTransito.CodigoArticulo = IGV_InventarioExistenciaXGrupo.CodigoDelArticulo"
   SQL = SQL & " AND IGV_InventarioMercanciaEnTransito.ConsecutivoCompania = IGV_InventarioExistenciaXGrupo.ConsecutivoCompania)"
   SQL = SQL & " WHERE IGV_InventarioMercanciaEnTransito.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND TipoArticuloInv IN ( " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_Color) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial) & ")  "
   SQL = SQL & " AND IGV_InventarioExistenciaXGrupo.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_MERCANCIA
   SQL = SQL & " AND IGV_InventarioMercanciaEnTransito.EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(True)
   If valSqlStatus <> "" Then
      SQL = SQL & " AND IGV_InventarioExistenciaXGrupo.StatusdelArticulo = " & valSqlStatus
   End If
   If valRango Then
      SQL = SQL & " AND " & gUtilSQL.fSQLStringValueBetween("IGV_InventarioExistenciaXGrupo.Codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   End If
   If valLineaProducto Then
      SQL = SQL & " AND IGV_InventarioExistenciaXGrupo.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrLineaProducto))
   End If
   If valCategoria Then
      SQL = SQL & " AND IGV_InventarioExistenciaXGrupo.Categoria = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrCategoria))
   End If
   If valMarca Then
      SQL = SQL & " AND IGV_InventarioExistenciaXGrupo.Marca = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrMarca))
   End If
   SQL = SQL & " ORDER BY " & valSqlOrder
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLExistenciaMercanciaEnTransito = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExistenciaMercanciaEnTransito", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlNivelDePrecio(ByVal valNivelDePrecio As enum_NivelDePrecio, ByVal valPreciosSinIva As Boolean) As String
   If (valPreciosSinIva) Then
      fSqlNivelDePrecio = fSqlNivelDePrecioSinIva(valNivelDePrecio)
   Else
      fSqlNivelDePrecio = fSqlNivelDePrecioConIva(valNivelDePrecio)
   End If
End Function

Private Function fSqlNivelDePrecioSinIva(ByVal valNivelDePrecio As enum_NivelDePrecio) As String
   Select Case valNivelDePrecio
      Case eND_PRECIO_1: fSqlNivelDePrecioSinIva = "PrecioSinIva"
      Case eND_PRECIO_2: fSqlNivelDePrecioSinIva = "PrecioSinIva2"
      Case eND_PRECIO_3: fSqlNivelDePrecioSinIva = "PrecioSinIva3"
      Case eND_PRECIO_4: fSqlNivelDePrecioSinIva = "PrecioSinIva4"
      Case Else: fSqlNivelDePrecioSinIva = "PrecioSinIva"
   End Select
End Function

Private Function fSqlNivelDePrecioConIva(ByVal valNivelDePrecio As enum_NivelDePrecio) As String
   Select Case valNivelDePrecio
      Case eND_PRECIO_1: fSqlNivelDePrecioConIva = "PrecioConIva"
      Case eND_PRECIO_2: fSqlNivelDePrecioConIva = "PrecioConIva2"
      Case eND_PRECIO_3: fSqlNivelDePrecioConIva = "PrecioConIva3"
      Case eND_PRECIO_4: fSqlNivelDePrecioConIva = "PrecioConIva4"
      Case Else: fSqlNivelDePrecioConIva = "PrecioSinIva"
   End Select
End Function

Public Function fSqlNivelDePrecioME(ByVal valNivelDePrecio As enum_NivelDePrecio, ByVal valPreciosSinIva As Boolean) As String
   If (valPreciosSinIva) Then
      fSqlNivelDePrecioME = fSqlNivelDePrecioSinIvaME(valNivelDePrecio)
   Else
      fSqlNivelDePrecioME = fSqlNivelDePrecioConIvaME(valNivelDePrecio)
   End If
End Function

Private Function fSqlNivelDePrecioSinIvaME(ByVal valNivelDePrecio As enum_NivelDePrecio) As String
   Select Case valNivelDePrecio
      Case eND_PRECIO_1: fSqlNivelDePrecioSinIvaME = "MEPrecioSinIva"
      Case eND_PRECIO_2: fSqlNivelDePrecioSinIvaME = "MEPrecioSinIva2"
      Case eND_PRECIO_3: fSqlNivelDePrecioSinIvaME = "MEPrecioSinIva3"
      Case eND_PRECIO_4: fSqlNivelDePrecioSinIvaME = "MEPrecioSinIva4"
      Case Else: fSqlNivelDePrecioSinIvaME = "MEPrecioSinIva"
   End Select
End Function

Private Function fSqlNivelDePrecioConIvaME(ByVal valNivelDePrecio As enum_NivelDePrecio) As String
   Select Case valNivelDePrecio
      Case eND_PRECIO_1: fSqlNivelDePrecioConIvaME = "MEPrecioConIva"
      Case eND_PRECIO_2: fSqlNivelDePrecioConIvaME = "MEPrecioConIva2"
      Case eND_PRECIO_3: fSqlNivelDePrecioConIvaME = "MEPrecioConIva3"
      Case eND_PRECIO_4: fSqlNivelDePrecioConIvaME = "MEPrecioConIva4"
      Case Else: fSqlNivelDePrecioConIvaME = "MEPrecioSinIva"
   End Select
End Function

Public Function fSqlCostosReporte(ByVal valManejaCostoPromedio As Boolean, ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   On Error GoTo h_ERROR
   If (valManejaCostoPromedio) Then
      fSqlCostosReporte = fSqlCostoPromedioPonderadoReporteDetallado(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaCierre, valFechaInicial, valFechaFinal)
   Else
      fSqlCostosReporte = fSqlCostoUltimaCompraReporteDetallado(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaInicial, valFechaFinal)
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCostosReporte", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlCostoUltimaCompraReporte(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal gMonedaLocalActual As Object, ByVal valSoloVigente As Boolean) As String
   Dim insArticuloView As clsArticuloInvVista
   Dim SQL As String
   Dim vSQLComparacion As String
   Dim vFecha As Date
   Dim sqlWhere As String
   Dim sqlWhere2 As String
   Dim SQLCodigo   As String
   Dim vCostoInicial As String
   Dim vCostoFinal As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set insArticuloView = New clsArticuloInvVista
   vFecha = valFechaInicial - 1
   vCostoInicial = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "costoinicial", "FechaInicial")
   vCostoFinal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "costofinal", "FechaFinal")
   vSQLComparacion = insArticuloView.GetViewNameArticuloInvMovimiento() & ".TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeOperacionToString(eTO_Autoconsumo))
   vSQLComparacion = vSQLComparacion & " OR " & insArticuloView.GetViewNameArticuloInvMovimiento() & ".TipoDeDocumento= " & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeOperacionToString(eTO_Retiro))
   sqlWhere = gUtilSQL.fSQLValueBetween(insArticuloView.GetViewNameArticuloInvMovimiento() & ".codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   If (valSoloVigente) Then
      sqlWhere = gUtilSQL.fSQLEnumValueWithAnd(sqlWhere, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   End If
   sqlWhere = gUtilSQL.fSQLValueDateWithComparisonRule(sqlWhere, insArticuloView.GetViewNameArticuloInvMovimiento() & ".Fecha", valFechaInicial, "<")
   sqlWhere = gUtilSQL.fSQLEnumValueWithAnd(sqlWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, insArticuloView.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania", valConsecutivoCompania)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   
   sqlWhere2 = gUtilSQL.fSQLValueBetween("ArticuloInventario.codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   If valSoloVigente Then
      sqlWhere2 = gUtilSQL.fSQLEnumValueWithAnd(sqlWhere2, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   End If
   sqlWhere2 = gUtilSQL.fSQLEnumValueWithAnd(sqlWhere2, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   sqlWhere2 = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere2, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   sqlWhere2 = gUtilSQL.getWhereSQL(sqlWhere2)

   SQL = SQL & " ;WITH  ArticulosyCostos(ConsecutivoCompania,"
   SQL = SQL & " Codigo,"
   SQL = SQL & " Descripcion,"
   SQL = SQL & " entrada,"
   SQL = SQL & " Salida,"
   SQL = SQL & " Autoconsumo,"
   SQL = SQL & " Retiro,ExistenciaAnterior)"
   SQL = SQL & " AS( "
   SQL = SQL & " SELECT "
   SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania ,"
   SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimiento() & ".codigo,"
   SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimiento() & ".Descripcion,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS ENTRADA ,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS Salida ,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS Autoconsumo,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS Retiro,"
   SQL = SQL & " Sum (ENTRADA) - Sum(SALIDAD)   AS ExistenciaAnterior"
   SQL = SQL & " FROM ArticuloInventario "
   SQL = SQL & " LEFT JOIN " & insArticuloView.GetViewNameArticuloInvMovimiento() & " ON "
   SQL = SQL & " ArticuloInventario.Codigo = " & insArticuloView.GetViewNameArticuloInvMovimiento() & ".Codigo  AND "
   SQL = SQL & " ArticuloInventario.ConsecutivoCompania = " & insArticuloView.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania "
   SQL = SQL & sqlWhere
   SQL = SQL & " GROUP BY "
   SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania,"
   SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimiento() & ".codigo, "
   SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimiento() & ".Descripcion "
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT "
   SQL = SQL & " ArticuloInventario.ConsecutivoCompania,"
   SQL = SQL & " ArticuloInventario.Codigo,"
   SQL = SQL & " ArticuloInventario.Descripcion,"
   SQL = SQL & " SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ENTRADA"), gUtilSQL.fNumToStrSQL(0), "ENTRADA", False) & " )  AS ENTRADA, "
   SQL = SQL & " SUM( " & gUtilSQL.getIIF(vSQLComparacion, gUtilSQL.fNumToStrSQL(0), insArticuloView.GetViewNameArticuloInvMovimiento() & ".salidad", False) & " )  AS Salida,"
   SQL = SQL & " SUM( " & gUtilSQL.getIIF(insArticuloView.GetViewNameArticuloInvMovimiento() & ".TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeOperacionToString(eTO_Autoconsumo)), insArticuloView.GetViewNameArticuloInvMovimiento() & ".salidad", gUtilSQL.fNumToStrSQL(0), False) & " ) AS Autoconsumo, "
   SQL = SQL & " SUM( " & gUtilSQL.getIIF(insArticuloView.GetViewNameArticuloInvMovimiento() & ".TipoDeDocumento= " & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeOperacionToString(eTO_Retiro)), insArticuloView.GetViewNameArticuloInvMovimiento() & ".salidad", gUtilSQL.fNumToStrSQL(0), False) & " ) AS Retiro, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS ExistenciaAnterior "
   SQL = SQL & " FROM ArticuloInventario "
   SQL = SQL & " LEFT JOIN " & insArticuloView.GetViewNameArticuloInvMovimiento() & " ON "
   SQL = SQL & gUtilSQL.fSQLValueDatesBetween("", insArticuloView.GetViewNameArticuloInvMovimiento() & ".Fecha ", valFechaInicial, valFechaFinal) & " AND "
   SQL = SQL & " ArticuloInventario.Codigo = " & insArticuloView.GetViewNameArticuloInvMovimiento() & ".Codigo  AND "
   SQL = SQL & " ArticuloInventario.ConsecutivoCompania = " & insArticuloView.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania "
   SQL = SQL & sqlWhere2
   SQL = SQL & " Group By "
   SQL = SQL & " ArticuloInventario.ConsecutivoCompania,"
   SQL = SQL & " ArticuloInventario.codigo,"
   SQL = SQL & " ArticuloInventario.Descripcion"
   SQL = SQL & " )"

   SQL = SQL & "  SELECT "
   SQL = SQL & " ArticulosyCostos.codigo,"
   SQL = SQL & " ArticulosyCostos.Descripcion,"
   SQL = SQL & " sum(ArticulosyCostos.ExistenciaAnterior)  AS ExistenciaAnterior,"
   SQL = SQL & " sum(ArticulosyCostos.Entrada) AS Entrada,"
   SQL = SQL & " sum(ArticulosyCostos.Salida)  AS Salida,"
   SQL = SQL & " sum(ArticulosyCostos.Retiro)  AS Retiro,"
   SQL = SQL & " sum(ArticulosyCostos.Autoconsumo) AS Autoconsumo,"
   SQL = SQL & " (sum(ArticulosyCostos.ExistenciaAnterior) + sum(ArticulosyCostos.Entrada) - sum(ArticulosyCostos.Salida) - sum(ArticulosyCostos.Retiro)- sum(ArticulosyCostos.Autoconsumo))AS ExistenciaActual, "
   SQL = SQL & vCostoFinal & "  *  sum(ArticulosyCostos.Entrada) AS EntradaMoneda,"
   SQL = SQL & vCostoFinal & "  *  sum(ArticulosyCostos.Salida)  AS SalidaMoneda,"
   SQL = SQL & vCostoFinal & "  *  sum(Retiro)  AS RetiroMoneda,"
   SQL = SQL & vCostoFinal & "  *  sum(Autoconsumo) AS AutoconsumoMoneda,"
   SQL = SQL & " (sum(ArticulosyCostos.ExistenciaAnterior) + sum(ArticulosyCostos.Entrada) - sum(ArticulosyCostos.Salida) - sum(ArticulosyCostos.Retiro)- sum(ArticulosyCostos.Autoconsumo)) *  " & vCostoFinal & " AS ExistenciaMoneda,"
   SQL = SQL & vCostoInicial & " As COSTO_UNITARIO"
   SQL = SQL & " FROM "
   SQL = SQL & " ArticulosyCostos LEFT JOIN "
   SQL = SQL & "  dbo.Gf_CostosUltimaCompra(" & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaInicial) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde)) & "," & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta)) & ") AS TBLCostoUltimaCompra ON "
   SQL = SQL & " ArticulosyCostos.codigo = TBLCostoUltimaCompra.codigo AND "
   SQL = SQL & " ArticulosyCostos.ConsecutivoCompania = TBLCostoUltimaCompra.ConsecutivoCompania "
   SQL = SQL & "  Group By "
   SQL = SQL & "  ArticulosyCostos.ConsecutivoCompania,"
   SQL = SQL & "  ArticulosyCostos.codigo,"
   SQL = SQL & "  ArticulosyCostos.Descripcion,"
   SQL = SQL & "  costoinicial,"
   SQL = SQL & "  costofinal,"
   SQL = SQL & " FechaInicial, FechaFinal"
   SQL = SQL & "  order by ArticulosyCostos.codigo"
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
  fSqlCostoUltimaCompraReporte = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCostoUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLRegistroDetalladadoInventario(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valEsSistemaInterno As Boolean, ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim sqlFacturaOut As String
   Dim sqlFacturaProductoCompuestoOut As String
   On Error GoTo h_ERROR
   sqlFacturaOut = fSQLFacturaParaRegistroDetallado(valFechaInicial, valFechaFinal, valConsecutivoCompania)
   sqlFacturaProductoCompuestoOut = fSQLFacturaPCParaRegistroDetallado(valFechaInicial, valFechaFinal, valConsecutivoCompania)
   SQL = sqlFacturaOut
   SQL = SQL & " UNION " & sqlFacturaProductoCompuestoOut
   SQL = SQL & " UNION " & fSQLExistenciaParaRegistroDetallado(valConsecutivoCompania)
   If Not valEsSistemaInterno Then
      SQL = SQL & " UNION " & fSQLNotaESParaRegistroDetallado(valFechaInicial, valFechaFinal, valConsecutivoCompania)
      SQL = SQL & " UNION " & fSQLComprasParaRegistroDetallado(valFechaInicial, valFechaFinal, valConsecutivoCompania)
      SQL = SQL & " UNION " & fSQLRegistroDetalladoConteoFisico(valFechaInicial, valFechaFinal, valConsecutivoCompania)
   End If
   SQL = SQL & " UNION " & fSQLFacturaParaExistenciaAnterior(valFechaInicial, valConsecutivoCompania)
   SQL = SQL & " UNION " & fSQLFacturaPCParaExistenciaAnterior(valFechaInicial, valConsecutivoCompania)
   If Not valEsSistemaInterno Then
      SQL = SQL & " UNION " & fSQLNotaESParaExistenciaAnterior(valFechaInicial, valConsecutivoCompania)
      SQL = SQL & " UNION " & fSQLConteoFisicoParaExistenciaAnterior(valFechaInicial, valConsecutivoCompania)
      SQL = SQL & " UNION " & fSQLComprasParaExistenciaAnterior(valFechaInicial, valConsecutivoCompania)
   End If
   fSQLRegistroDetalladadoInventario = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLRegistroDetalladadoInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLFacturaParaRegistroDetallado(ByVal valFechaInicial As String, ByVal valFechaFinal As String, ByVal valConsecutivoCompania As Long)
   Dim sqlFact As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   sqlFact = "SELECT "
   sqlFact = sqlFact & "ArticuloInventario.Codigo, "
   sqlFact = sqlFact & "ArticuloInventario.Descripcion, "
   sqlFact = sqlFact & "0 AS ExistenciaAnterior, 0 AS Entrada, "
   sqlFact = sqlFact & "renglonFactura.Cantidad AS Salida, "
   sqlFact = sqlFact & "0 as  Retiro, 0 AS Autoconsumo, "
   sqlFact = sqlFact & "0 as ExistenciaActual, 0 AS EntradaMoneda, "
   sqlFact = sqlFact & "renglonFactura.Cantidad * ArticuloInventario.CostoUnitario AS SalidaMoneda, "
   sqlFact = sqlFact & "0 as  RetiroMoneda, 0 AS AutoconsumoMoneda, "
   sqlFact = sqlFact & "0 as ExistenciaMoneda, "
   sqlFact = sqlFact & "factura.Numero, "
   sqlFact = sqlFact & "renglonFactura.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   sqlFact = sqlFact & " 0 AS COSTO_UNITARIO"
   sqlFact = sqlFact & " FROM factura  INNER JOIN (ArticuloInventario INNER JOIN renglonFactura "
   sqlFact = sqlFact & " ON (ArticuloInventario.Codigo = renglonFactura.articulo "
   sqlFact = sqlFact & ") AND (ArticuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania "
   sqlFact = sqlFact & ")) ON (factura.Numero  = renglonFactura.NumeroFactura "
   sqlFact = sqlFact & ") AND (factura.ConsecutivoCompania  = renglonFactura.ConsecutivoCompania "
   sqlFact = sqlFact & ") AND (factura.TipoDeDocumento  = renglonFactura.TipoDeDocumento)"
   sqlWhere = " WHERE (ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   sqlWhere = sqlWhere & " AND (factura.StatusFactura  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   sqlWhere = sqlWhere & " AND (" & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal) & ")"
   sqlWhere = sqlWhere & " AND (ArticuloInventario.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA) & ")"
   sqlFact = sqlFact & sqlWhere
   sqlFact = sqlFact & " UNION SELECT RenglonFactura.Articulo as codigo, articuloInventario.Descripcion, 0 AS ExistenciaAnterior, 0 AS Entrada,"
   sqlFact = sqlFact & " renglonFactura.Cantidad AS Salida, 0 As  Retiro, 0 AS Autoconsumo, 0 AS ExistenciaActual, 0 AS EntradaMoneda, renglonFactura.Cantidad*articuloInventario.CostoUnitario AS SalidaMoneda, "
   sqlFact = sqlFact & " 0 as  RetiroMoneda, 0 AS AutoconsumoMoneda, "
   sqlFact = sqlFact & " 0 AS ExistenciaMoneda, factura.Numero, renglonFactura.ConsecutivoRenglon AS CONSECUTIVO_RENGLON, 0 AS COSTO_UNITARIO"
   sqlFact = sqlFact & " FROM (factura INNER JOIN renglonFactura ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) "
   sqlFact = sqlFact & " AND (factura.Numero = renglonFactura.NumeroFactura) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) "
   sqlFact = sqlFact & " INNER JOIN (ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo) "
   sqlFact = sqlFact & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) ON (renglonFactura.Articulo ="
   sqlFact = sqlFact & " ( ExistenciaPorGrupo.CodigoArticulo+ExistenciaporGrupo.codigoColor+ExistenciaPorGrupo.CodigoTalla)) "
   sqlFact = sqlFact & " AND (renglonFactura.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)"
   sqlFact = sqlFact & sqlWhere
   fSQLFacturaParaRegistroDetallado = sqlFact
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFacturaParaRegistroDetallado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLExistenciaParaRegistroDetallado(ByVal valConsecutivoCompania As Long)
   Dim SQLExistencia As String
   On Error GoTo h_ERROR
   SQLExistencia = "SELECT "
   SQLExistencia = SQLExistencia & " ArticuloInventario.Codigo, "
   SQLExistencia = SQLExistencia & " ArticuloInventario.Descripcion, "
   SQLExistencia = SQLExistencia & " 0 AS ExistenciaAnterior, 0 AS Entrada, 0 AS Salida, 0 As  Retiro, 0 AS Autoconsumo,  "
   SQLExistencia = SQLExistencia & " ArticuloInventario.Existencia as ExistenciaActual, "
   SQLExistencia = SQLExistencia & " 0 AS EntradaMoneda, 0 AS SalidaMoneda, 0 As  RetiroMoneda, 0 AS AutoconsumoMoneda, "
   SQLExistencia = SQLExistencia & " ArticuloInventario.Existencia * ArticuloInventario.CostoUnitario as ExistenciaMoneda, "
   SQLExistencia = SQLExistencia & " '' as Numero, 0 AS CONSECUTIVO_RENGLON,"
   SQLExistencia = SQLExistencia & " CostoUnitario AS COSTO_UNITARIO"
   SQLExistencia = SQLExistencia & " FROM ArticuloInventario "
   SQLExistencia = SQLExistencia & " WHERE (ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   SQLExistencia = SQLExistencia & " AND (ArticuloInventario.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA) & ")"
   SQLExistencia = SQLExistencia & " AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSimpleSqlValue(eTAI_Simple) & " Or ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSimpleSqlValue(eTAI_UsaSerial) & ")"
   SQLExistencia = SQLExistencia & " UNION SELECT (ExistenciaporGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla) AS Codigo, "
   SQLExistencia = SQLExistencia & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   SQLExistencia = SQLExistencia & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   SQLExistencia = SQLExistencia & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   SQLExistencia = SQLExistencia & " 0 AS ExistenciaAnterior, 0 AS Entrada, 0 AS Salida, 0 As  Retiro, 0 AS Autoconsumo, "
   SQLExistencia = SQLExistencia & " SUM(ExistenciaPorGrupo.Existencia) AS ExistenciaActual, 0 AS EntradaMoneda, 0 AS SalidaMoneda,  0 As  RetiroMoneda, 0 AS AutoconsumoMoneda,"
   SQLExistencia = SQLExistencia & " (SUM(ExistenciaPorGrupo.Existencia)*articuloInventario.CostoUnitario) AS ExistenciaMoneda, '' AS Numero, 0 AS CONSECUTIVO_RENGLON, articuloInventario.CostoUnitario AS COSTO_UNITARIO"
   SQLExistencia = SQLExistencia & " FROM ((ExistenciaPorGrupo LEFT JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)) LEFT JOIN Talla ON "
   SQLExistencia = SQLExistencia & " (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla)) LEFT JOIN articuloInventario ON "
   SQLExistencia = SQLExistencia & " (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo) AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)"
   SQLExistencia = SQLExistencia & " WHERE (ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   SQLExistencia = SQLExistencia & " AND (ArticuloInventario.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   SQLExistencia = SQLExistencia & " And (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSimpleSqlValue(eTAI_UsaTalla_Color) & " OR ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSimpleSqlValue(eTAI_UsaTalla_ColorySerial) & "))"
   SQLExistencia = SQLExistencia & " GROUP BY existenciaporGrupo.CodigoArticulo,"
   SQLExistencia = SQLExistencia & " ExistenciaPorGrupo.CodigoColor,ExistenciaPorGrupo.CodigoTalla,articuloInventario.Descripcion,"
   SQLExistencia = SQLExistencia & " Color.descripcionColor,Talla.descripcionTalla,ArticuloInventario.CostoUnitario"
   fSQLExistenciaParaRegistroDetallado = SQLExistencia
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExistenciaParaRegistroDetallado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLNotaESParaRegistroDetallado(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long) As String
   Dim sqlNotaES As String
   Dim vSQLWhere As String
   Dim sqlOperacion As String
   On Error GoTo h_ERROR
   sqlOperacion = "RenglonNotaES.Cantidad * ArticuloInventario.CostoUnitario"
  
   vSQLWhere = gUtilSQL.fSQLValueDatesBetween("", "NotaDeEntradaSalida.Fecha", valFechaInicial, valFechaFinal)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "StatusNotaEntradaSalida", enum_StatusNotaEntradaSalida.eSNES_Vigente)
   vSQLWhere = vSQLWhere & " AND ArticuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO)
   vSQLWhere = vSQLWhere & " AND ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   sqlNotaES = "SELECT "
   sqlNotaES = sqlNotaES & "ArticuloInventario.Codigo, "
   sqlNotaES = sqlNotaES & "ArticuloInventario.Descripcion, "
   sqlNotaES = sqlNotaES & "0 AS ExistenciaAnterior, "
   
   sqlNotaES = sqlNotaES & fSqlNotaESCantidadPorOperacion()
   sqlNotaES = sqlNotaES & "0 AS ExistenciaActual, "
   sqlNotaES = sqlNotaES & fSqlNotaESCostoPorOperacion()
   sqlNotaES = sqlNotaES & "0 as ExistenciaMoneda, "
   sqlNotaES = sqlNotaES & "NotaDeEntradaSalida.NumeroDocumento As Numero, "
   sqlNotaES = sqlNotaES & "RenglonNotaES.consecutivoRenglon AS CONSECUTIVO_RENGLON,"
   sqlNotaES = sqlNotaES & " 0 As COSTO_UNITARIO"
   sqlNotaES = sqlNotaES & " FROM NotaDeEntradaSalida " & _
                  " INNER JOIN (ArticuloInventario INNER JOIN RenglonNotaES " & _
                  " ON (ArticuloInventario.Codigo" & _
                  " = RenglonNotaES.CodigoArticulo" & _
                  ") AND (ArticuloInventario.ConsecutivoCompania" & _
                  " = RenglonNotaES.ConsecutivoCompania" & _
                  ")) ON (NotaDeEntradaSalida.NumeroDocumento" & _
                  " = RenglonNotaES.NumeroDocumento" & _
                  ") AND (NotaDeEntradaSalida.ConsecutivoCompania" & _
                  " = RenglonNotaES.ConsecutivoCompania)"
   
   sqlNotaES = sqlNotaES & vSQLWhere
   
   sqlNotaES = sqlNotaES & " UNION SELECT RenglonNotaEs.CodigoArticulo as Codigo,"
   sqlNotaES = sqlNotaES & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   sqlNotaES = sqlNotaES & "0 AS ExistenciaAnterior, "
   sqlNotaES = sqlNotaES & fSqlNotaESCantidadPorOperacion()
   sqlNotaES = sqlNotaES & "0 AS ExistenciaActual, "
   sqlNotaES = sqlNotaES & fSqlNotaESCostoPorOperacion()
   sqlNotaES = sqlNotaES & "0 as ExistenciaMoneda, "
   sqlNotaES = sqlNotaES & "NotaDeEntradaSalida.NumeroDocumento As Numero, "
   sqlNotaES = sqlNotaES & "RenglonNotaES.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   sqlNotaES = sqlNotaES & " 0 As COSTO_UNITARIO"
   sqlNotaES = sqlNotaES & " FROM ((((notaDeEntradaSalida INNER JOIN renglonNotaES ON (notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania) "
   sqlNotaES = sqlNotaES & " AND (notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento)) INNER JOIN ExistenciaPorGrupo ON "
   sqlNotaES = sqlNotaES & " (renglonNotaES.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania) "
   sqlNotaES = sqlNotaES & " AND (renglonNotaES.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.codigoColor+ExistenciaPorGrupo.CodigoTalla)))"
   sqlNotaES = sqlNotaES & " LEFT JOIN articuloInventario ON (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) "
   sqlNotaES = sqlNotaES & " AND (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)) LEFT JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) "
   sqlNotaES = sqlNotaES & " AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)) LEFT JOIN Talla ON (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania) "
   sqlNotaES = sqlNotaES & " AND (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla)"
   sqlNotaES = sqlNotaES & vSQLWhere
   fSQLNotaESParaRegistroDetallado = sqlNotaES
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLNotaESParaRegistroDetallado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLComprasParaRegistroDetallado(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long) As String
   Dim sqlCompras As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   sqlCompras = "SELECT "
   sqlCompras = sqlCompras & "ArticuloInventario.Codigo, "
   sqlCompras = sqlCompras & "ArticuloInventario.Descripcion, "
   sqlCompras = sqlCompras & "0 AS ExistenciaAnterior, "
   sqlCompras = sqlCompras & "RenglonCompra.Cantidad As Entrada, "
   sqlCompras = sqlCompras & "0 AS Salida, 0 As  Retiro, 0 AS Autoconsumo, 0 as ExistenciaActual, "
   sqlCompras = sqlCompras & "RenglonCompra.Cantidad * ArticuloInventario.CostoUnitario AS EntradaMoneda, "
   sqlCompras = sqlCompras & "0 AS SalidaMoneda, 0 As  RetiroMoneda, 0 AS AutoconsumoMoneda, 0 as ExistenciaMoneda, "
   sqlCompras = sqlCompras & "Compra.NumeroSecuencial As Numero, "
   sqlCompras = sqlCompras & "RenglonCompra.consecutivoRenglon AS CONSECUTIVO_RENGLON,"
   sqlCompras = sqlCompras & " 0 AS COSTO_UNITARIO"
   sqlCompras = sqlCompras & " FROM Compra INNER JOIN (ArticuloInventario " & _
                  " INNER JOIN RenglonCompra " & _
                  " ON (ArticuloInventario.Codigo" & _
                  " = RenglonCompra.CodigoArticulo" & _
                  ") AND (ArticuloInventario.ConsecutivoCompania" & _
                  " = RenglonCompra.ConsecutivoCompania" & _
                  ")) ON (Compra.NumeroSecuencial" & _
                  " = RenglonCompra.NumeroSecuencialCompra" & _
                  ") AND (Compra.ConsecutivoCompania" & _
                  " = RenglonCompra.ConsecutivoCompania)"
   sqlWhere = " WHERE (ArticuloInventario.ConsecutivoCompania" & _
                  " = " & valConsecutivoCompania & ")"
   sqlWhere = sqlWhere & " AND (" & gUtilSQL.DfSQLDateValueBetween("Compra.Fecha", _
                  valFechaInicial, valFechaFinal) & ")"
   sqlWhere = sqlWhere & " AND (Compra.EsUnaOrdenDeCompra" & _
                  " ='" & gConvert.ConvertBooleanToString(False) & "')"
   sqlWhere = sqlWhere & " AND (Compra.StatusCompra" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE) & ") "
   sqlWhere = sqlWhere & " AND (ArticuloInventario.TipoDeArticulo" & _
                        " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO) & ")"
   sqlCompras = sqlCompras & sqlWhere
   sqlCompras = sqlCompras & " UNION SELECT renglonCompra.CodigoArticulo AS Codigo, "
   sqlCompras = sqlCompras & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlCompras = sqlCompras & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlCompras = sqlCompras & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   sqlCompras = sqlCompras & " 0 AS ExistenciaAnterior, renglonCompra.Cantidad AS Entrada, 0 AS Salida, 0 As  Retiro, 0 AS Autoconsumo, 0 AS ExistenciaActual, "
   sqlCompras = sqlCompras & " RenglonCompra.Cantidad*articuloInventario.CostoUnitario AS EntradaMoneda, 0 AS SalidaMoneda, 0 As  RetiroMoneda, 0 AS AutoconsumoMoneda, "
   sqlCompras = sqlCompras & " 0 AS ExistenciaMoneda, compra.NumeroSecuencial AS Numero, renglonCompra.consecutivoRenglon AS CONSECUTIVO_RENGLON,"
   sqlCompras = sqlCompras & " 0 AS COSTO_UNITARIO"
   sqlCompras = sqlCompras & " FROM (compra INNER JOIN (((renglonCompra INNER JOIN ExistenciaPorGrupo ON (renglonCompra.CodigoArticulo = "
   sqlCompras = sqlCompras & " (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla)) "
   sqlCompras = sqlCompras & " AND (renglonCompra.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) "
   sqlCompras = sqlCompras & " LEFT JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo) "
   sqlCompras = sqlCompras & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) "
   sqlCompras = sqlCompras & " LEFT JOIN Talla ON (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla) "
   sqlCompras = sqlCompras & " AND (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania)) ON (compra.NumeroSecuencial = renglonCompra.NumeroSecuencialCompra) "
   sqlCompras = sqlCompras & " AND (compra.ConsecutivoCompania = renglonCompra.ConsecutivoCompania)) LEFT JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) "
   sqlCompras = sqlCompras & " AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)"
   sqlCompras = sqlCompras & sqlWhere
   fSQLComprasParaRegistroDetallado = sqlCompras
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLComprasParaRegistroDetallado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLRegistroDetalladoConteoFisico(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long) As String
   Dim SQLConteoFisico As String
   On Error GoTo h_ERROR
   SQLConteoFisico = fSQLConteoFisicoParaRegistroDetallado(valFechaInicial, valFechaFinal, valConsecutivoCompania)
   fSQLRegistroDetalladoConteoFisico = SQLConteoFisico
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLRegistroDetalladoConteoFisico", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLFacturaPCParaRegistroDetallado(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long) As String
   Dim sqlFact As String
   Const tablaAuxiliar As String * 4 = "AIPC"
   On Error GoTo h_ERROR
   sqlFact = "SELECT "
   sqlFact = sqlFact & "ProductoCompuesto.CodigoArticulo AS Codigo, "
   sqlFact = sqlFact & tablaAuxiliar & ".Descripcion, "
   sqlFact = sqlFact & "0 AS ExistenciaAnterior, 0 AS Entrada, "
   sqlFact = sqlFact & "renglonFactura.Cantidad * ProductoCompuesto.Cantidad AS Salida, "
   sqlFact = sqlFact & " 0 as  Retiro, 0 AS Autoconsumo, "
   sqlFact = sqlFact & "0 as ExistenciaActual, 0 AS EntradaMoneda, "
   sqlFact = sqlFact & "renglonFactura.Cantidad * " & tablaAuxiliar & ".CostoUnitario * ProductoCompuesto.Cantidad AS SalidaMoneda, "
   sqlFact = sqlFact & " 0 as  RetiroMoneda, 0 AS AutoconsumoMoneda, "
   sqlFact = sqlFact & "0 as ExistenciaMoneda, "
   sqlFact = sqlFact & "factura.Numero, "
   sqlFact = sqlFact & "renglonFactura.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   sqlFact = sqlFact & " 0 AS COSTO_UNITARIO"
   sqlFact = sqlFact & " FROM factura INNER JOIN (( ( "
   sqlFact = sqlFact & "ArticuloInventario INNER JOIN ProductoCompuesto ON ("
   sqlFact = sqlFact & "ArticuloInventario.Codigo = ProductoCompuesto.CodigoConexionConElMaster"
   sqlFact = sqlFact & ") AND (ArticuloInventario.ConsecutivoCompania = ProductoCompuesto.ConsecutivoCompania"
   sqlFact = sqlFact & ")) INNER JOIN "
   sqlFact = sqlFact & "ArticuloInventario " & tablaAuxiliar & " ON "
   sqlFact = sqlFact & "(ProductoCompuesto.CodigoArticulo"
   sqlFact = sqlFact & " = " & tablaAuxiliar & ".Codigo)"
   sqlFact = sqlFact & " AND (ProductoCompuesto.ConsecutivoCompania"
   sqlFact = sqlFact & " = " & tablaAuxiliar & ".ConsecutivoCompania))"
   sqlFact = sqlFact & " INNER JOIN renglonFactura "
   sqlFact = sqlFact & " ON (ArticuloInventario.Codigo = renglonFactura.articulo "
   sqlFact = sqlFact & ") AND (ArticuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania "
   sqlFact = sqlFact & ")) ON (factura.Numero  = renglonFactura.NumeroFactura "
   sqlFact = sqlFact & ") AND (factura.ConsecutivoCompania  = renglonFactura.ConsecutivoCompania "
   sqlFact = sqlFact & ") AND (factura.TipoDeDocumento  = renglonFactura.TipoDeDocumento)"
   sqlFact = sqlFact & " WHERE (ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   sqlFact = sqlFact & " AND (factura.StatusFactura  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ") "
   sqlFact = sqlFact & " AND (" & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal) & ")"
   sqlFact = sqlFact & " AND (ArticuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO) & ")"
   fSQLFacturaPCParaRegistroDetallado = sqlFact
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFacturaPCParaRegistroDetallado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
 
Private Function fSQLFacturaPCParaExistenciaAnterior(ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long) As String
   Dim sqlFact As String
   On Error GoTo h_ERROR
   sqlFact = "SELECT "
   sqlFact = sqlFact & "ProductoCompuesto.CodigoArticulo AS Codigo, "
   sqlFact = sqlFact & "ArticuloInventario.Descripcion, "
   sqlFact = sqlFact & "(renglonFactura.Cantidad * ProductoCompuesto.Cantidad *  (-1)) AS ExistenciaAnterior, "
   sqlFact = sqlFact & "0 AS Entrada, 0 AS Salida,  0 As  Retiro, 0 AS Autoconsumo, 0 as ExistenciaActual, "
   sqlFact = sqlFact & "0 AS EntradaMoneda, 0 AS SalidaMoneda, 0 As  RetiroMoneda, 0 AS AutoconsumoMoneda,  0 as ExistenciaMoneda, "
   sqlFact = sqlFact & "factura.Numero, "
   sqlFact = sqlFact & "renglonFactura.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   sqlFact = sqlFact & " 0 AS COSTO_UNITARIO"
   sqlFact = sqlFact & " FROM factura INNER JOIN (("
   sqlFact = sqlFact & "ArticuloInventario INNER JOIN ProductoCompuesto ON ("
   sqlFact = sqlFact & "ArticuloInventario.Codigo = ProductoCompuesto.CodigoConexionConElMaster"
   sqlFact = sqlFact & ") AND (ArticuloInventario.ConsecutivoCompania = ProductoCompuesto.ConsecutivoCompania"
   sqlFact = sqlFact & ")) INNER JOIN renglonFactura "
   sqlFact = sqlFact & " ON (ArticuloInventario.Codigo = renglonFactura.articulo "
   sqlFact = sqlFact & ") AND (ArticuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania "
   sqlFact = sqlFact & ")) ON (factura.Numero  = renglonFactura.NumeroFactura "
   sqlFact = sqlFact & ") AND (factura.ConsecutivoCompania  = renglonFactura.ConsecutivoCompania "
   sqlFact = sqlFact & ") AND (factura.TipoDeDocumento  = renglonFactura.TipoDeDocumento)"
   sqlFact = sqlFact & " WHERE (ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   sqlFact = sqlFact & " AND (factura.StatusFactura  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   sqlFact = sqlFact & " AND (factura.Fecha < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ") "
   sqlFact = sqlFact & " AND (ArticuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO) & ") "
   fSQLFacturaPCParaExistenciaAnterior = sqlFact
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFacturaPCParaExistenciaAnterior", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLFacturaParaExistenciaAnterior(ByVal valFechaInicial As String, ByVal valConsecutivoCompania As Long)
   Dim sqlFact As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   sqlFact = "SELECT "
   sqlFact = sqlFact & "ArticuloInventario.Codigo, "
   sqlFact = sqlFact & "ArticuloInventario.Descripcion, "
   sqlFact = sqlFact & "(renglonFactura.Cantidad * (-1)) AS ExistenciaAnterior, "
   sqlFact = sqlFact & "0 AS Entrada, 0 AS Salida, 0 As  Retiro, 0 AS Autoconsumo, 0 As ExistenciaActual, "
   sqlFact = sqlFact & "0 AS EntradaMoneda, 0 AS SalidaMoneda, 0 As  RetiroMoneda, 0 AS AutoconsumoMoneda, 0 As ExistenciaMoneda, "
   sqlFact = sqlFact & "factura.Numero, "
   sqlFact = sqlFact & "renglonFactura.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   sqlFact = sqlFact & " 0 AS COSTO_UNITARIO"
   sqlFact = sqlFact & " FROM factura INNER JOIN (ArticuloInventario  INNER JOIN renglonFactura "
   sqlFact = sqlFact & " ON (ArticuloInventario.Codigo = renglonFactura.articulo "
   sqlFact = sqlFact & ") AND (ArticuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania "
   sqlFact = sqlFact & ")) ON (factura.Numero  = renglonFactura.NumeroFactura "
   sqlFact = sqlFact & ") AND (factura.ConsecutivoCompania  = renglonFactura.ConsecutivoCompania "
   sqlFact = sqlFact & ") AND (factura.TipoDeDocumento  = renglonFactura.TipoDeDocumento)"
   sqlWhere = " WHERE (ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   sqlWhere = sqlWhere & " AND (factura.StatusFactura  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   sqlWhere = sqlWhere & " AND (factura.Fecha < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ") "
   sqlWhere = sqlWhere & " AND (ArticuloInventario.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA) & ")"
   sqlFact = sqlFact & sqlWhere
   sqlFact = sqlFact & " UNION SELECT RenglonFactura.Articulo as codigo, "
   sqlFact = sqlFact & " ArticuloInventario.Descripcion, (renglonFactura.Cantidad * (-1)) AS ExistenciaAnterior,"
   sqlFact = sqlFact & " 0 AS Entrada, 0 AS Salida,  0 As  Retiro, 0 AS Autoconsumo, 0 as ExistenciaActual, 0 AS EntradaMoneda, 0 AS SalidaMoneda, 0 As  RetiroMoneda, 0 AS AutoconsumoMoneda, "
   sqlFact = sqlFact & " 0 as ExistenciaMoneda, factura.Numero, renglonFactura.ConsecutivoRenglon AS CONSECUTIVO_RENGLON, "
   sqlFact = sqlFact & " 0 AS COSTO_UNITARIO"
   sqlFact = sqlFact & " FROM (factura INNER JOIN renglonFactura ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) "
   sqlFact = sqlFact & " AND (factura.Numero = renglonFactura.NumeroFactura) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) "
   sqlFact = sqlFact & " INNER JOIN (ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo) "
   sqlFact = sqlFact & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) ON (renglonFactura.Articulo =( ExistenciaPorGrupo.CodigoArticulo+ExistenciaporGrupo.codigoColor+ExistenciaPorGrupo.CodigoTalla)) "
   sqlFact = sqlFact & " AND (renglonFactura.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)"
   sqlFact = sqlFact & sqlWhere
   fSQLFacturaParaExistenciaAnterior = sqlFact
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFacturaParaExistenciaAnterior", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLNotaESParaExistenciaAnterior(ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long) As String
   Dim sqlNotaES As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   sqlNotaES = "SELECT "
   sqlNotaES = sqlNotaES & "ArticuloInventario.Codigo, "
   sqlNotaES = sqlNotaES & "ArticuloInventario.Descripcion, "
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF("NotaDeEntradaSalida.TipodeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO), "RenglonNotaES.Cantidad", "RenglonNotaES.Cantidad * (-1) ") & " AS ExistenciaAnterior, "
   sqlNotaES = sqlNotaES & "0 AS Entradas, 0 AS Salidas, 0 As  Retiro, 0 As Autoconsumo, 0 AS ExistenciaActual, "
   sqlNotaES = sqlNotaES & "0 as EntradaMoneda, 0 as SalidaMoneda,  0 As  RetiroMoneda,0 As AutoconsumoMoneda, 0 as ExistenciaMoneda, "
   sqlNotaES = sqlNotaES & "NotaDeEntradaSalida.NumeroDocumento As Numero, "
   sqlNotaES = sqlNotaES & "RenglonNotaES.consecutivoRenglon AS CONSECUTIVO_RENGLON,"
   sqlNotaES = sqlNotaES & "0 As COSTO_UNITARIO"
   sqlNotaES = sqlNotaES & " FROM NotaDeEntradaSalida  INNER JOIN (ArticuloInventario INNER JOIN RenglonNotaES "
   sqlNotaES = sqlNotaES & " ON (ArticuloInventario.Codigo = RenglonNotaES.CodigoArticulo"
   sqlNotaES = sqlNotaES & ") AND (ArticuloInventario.ConsecutivoCompania = RenglonNotaES.ConsecutivoCompania"
   sqlNotaES = sqlNotaES & ")) ON (NotaDeEntradaSalida.NumeroDocumento = RenglonNotaES.NumeroDocumento"
   sqlNotaES = sqlNotaES & ") AND (NotaDeEntradaSalida.ConsecutivoCompania = RenglonNotaES.ConsecutivoCompania)"
   sqlWhere = " WHERE (ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   sqlWhere = sqlWhere & " AND (NotaDeEntradaSalida.Fecha < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ") "
   sqlWhere = sqlWhere & " AND (ArticuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO) & ")"
   sqlNotaES = sqlNotaES & sqlWhere
   sqlNotaES = sqlNotaES & " UNION  SELECT  RenglonNotaEs.CodigoArticulo as Codigo,"
   sqlNotaES = sqlNotaES & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF("NotaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO), "RenglonNotaES.Cantidad", "RenglonNotaES.Cantidad * (-1) ") & " AS ExistenciaAnterior, "
   sqlNotaES = sqlNotaES & "0 AS Entradas, 0 as Salidas, 0 As  Retiro, 0 As Autoconsumo, 0 AS ExistenciaActual, "
   sqlNotaES = sqlNotaES & "0 as EntradaMoneda, 0 as SalidaMoneda, 0 As  RetiroMoneda, 0 As AutoconsumoMoneda, 0 as ExistenciaMoneda, "
   sqlNotaES = sqlNotaES & "NotaDeEntradaSalida.NumeroDocumento As Numero, "
   sqlNotaES = sqlNotaES & "RenglonNotaES.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   sqlNotaES = sqlNotaES & "0 As COSTO_UNITARIO"
   sqlNotaES = sqlNotaES & " FROM ((((notaDeEntradaSalida INNER JOIN renglonNotaES ON (notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania) "
   sqlNotaES = sqlNotaES & " AND (notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento)) INNER JOIN ExistenciaPorGrupo ON "
   sqlNotaES = sqlNotaES & " (renglonNotaES.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania) "
   sqlNotaES = sqlNotaES & " AND (renglonNotaES.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.codigoColor+ExistenciaPorGrupo.CodigoTalla)))"
   sqlNotaES = sqlNotaES & " LEFT JOIN articuloInventario ON (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) "
   sqlNotaES = sqlNotaES & " AND (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)) LEFT JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) "
   sqlNotaES = sqlNotaES & " AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)) LEFT JOIN Talla ON (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania) "
   sqlNotaES = sqlNotaES & " AND (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla)"
   sqlNotaES = sqlNotaES & sqlWhere
   fSQLNotaESParaExistenciaAnterior = sqlNotaES
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLNotaESParaExistenciaAnterior", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLConteoFisicoParaExistenciaAnterior(ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long) As String
   Dim SQLConteoFisico As String
   On Error GoTo h_ERROR
   SQLConteoFisico = fSQLConteoFisico_Para_ExistenciaAnterior(valFechaInicial, valConsecutivoCompania)
   fSQLConteoFisicoParaExistenciaAnterior = SQLConteoFisico
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLConteoFisicoParaExistenciaAnterior", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLComprasParaExistenciaAnterior(ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long) As String
   Dim sqlCompras As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   sqlCompras = "SELECT "
   sqlCompras = sqlCompras & "ArticuloInventario.Codigo, "
   sqlCompras = sqlCompras & "ArticuloInventario.Descripcion, "
   sqlCompras = sqlCompras & "RenglonCompra.Cantidad As  ExistenciaAnterior, "
   sqlCompras = sqlCompras & "0 As Entrada, 0 AS Salida, 0 As  Retiro,  0 As  Autoconsumo, 0 As ExistenciaActual, "
   sqlCompras = sqlCompras & "0 AS EntradaMoneda, 0 AS SalidaMoneda, 0 As  RetiroMoneda,  0 As  AutoconsumoMoneda, 0 As ExistenciaMoneda, "
   sqlCompras = sqlCompras & "Compra.NumeroSecuencial As Numero, "
   sqlCompras = sqlCompras & "RenglonCompra.consecutivoRenglon AS CONSECUTIVO_RENGLON,"
   sqlCompras = sqlCompras & " 0 AS COSTO_UNITARIO"
   sqlCompras = sqlCompras & " FROM Compra  INNER JOIN (ArticuloInventario INNER JOIN RenglonCompra "
   sqlCompras = sqlCompras & " ON (ArticuloInventario.Codigo = RenglonCompra.CodigoArticulo"
   sqlCompras = sqlCompras & ") AND (ArticuloInventario.ConsecutivoCompania = RenglonCompra.ConsecutivoCompania"
   sqlCompras = sqlCompras & ")) ON (Compra.NumeroSecuencial = RenglonCompra.NumeroSecuencialCompra"
   sqlCompras = sqlCompras & ") AND (Compra.ConsecutivoCompania = RenglonCompra.ConsecutivoCompania)"
   sqlWhere = " WHERE (ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   sqlWhere = sqlWhere & " AND (Compra.Fecha < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ")"
   sqlWhere = sqlWhere & " AND (Compra.EsUnaOrdenDeCompra ='" & gConvert.ConvertBooleanToString(False) & "')"
   sqlWhere = sqlWhere & " AND (Compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE) & ")"
   sqlWhere = sqlWhere & " AND (ArticuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO) & ")"
   sqlCompras = sqlCompras & sqlWhere
   sqlCompras = sqlCompras & " UNION SELECT renglonCompra.CodigoArticulo AS Codigo, "
   sqlCompras = sqlCompras & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlCompras = sqlCompras & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlCompras = sqlCompras & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   sqlCompras = sqlCompras & "RenglonCompra.Cantidad As  ExistenciaAnterior, "
   sqlCompras = sqlCompras & "0 As Entrada, 0 AS Salida, 0 As  Retiro,  0 As  Autoconsumo, 0 As ExistenciaActual, "
   sqlCompras = sqlCompras & "0 AS EntradaMoneda, 0 AS SalidaMoneda, 0 As  RetiroMoneda,  0 As  AutoconsumoMoneda, 0 As ExistenciaMoneda, "
   sqlCompras = sqlCompras & "Compra.NumeroSecuencial As Numero, "
   sqlCompras = sqlCompras & "RenglonCompra.consecutivoRenglon AS CONSECUTIVO_RENGLON,"
   sqlCompras = sqlCompras & " 0 AS COSTO_UNITARIO"
   sqlCompras = sqlCompras & " FROM (compra INNER JOIN (((renglonCompra INNER JOIN ExistenciaPorGrupo ON (renglonCompra.CodigoArticulo = "
   sqlCompras = sqlCompras & " (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla)) "
   sqlCompras = sqlCompras & " AND (renglonCompra.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) "
   sqlCompras = sqlCompras & " LEFT JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo) "
   sqlCompras = sqlCompras & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) "
   sqlCompras = sqlCompras & " LEFT JOIN Talla ON (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla) "
   sqlCompras = sqlCompras & " AND (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania)) ON (compra.NumeroSecuencial = renglonCompra.NumeroSecuencialCompra) "
   sqlCompras = sqlCompras & " AND (compra.ConsecutivoCompania = renglonCompra.ConsecutivoCompania)) LEFT JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) "
   sqlCompras = sqlCompras & " AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)"
   sqlCompras = sqlCompras & sqlWhere
   fSQLComprasParaExistenciaAnterior = sqlCompras
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLComprasParaExistenciaAnterior", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlNotaESCantidadPorOperacion() As String
   Dim sqlNotaES As String
   On Error GoTo h_ERROR
    sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.fSQLEnumValueWithAnd("", "NotaDeEntradaSalida.TipodeOperacion", enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO), "RenglonNotaES.Cantidad", "0") & " AS Entradas, "
    sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.fSQLEnumValueWithAnd("", "NotaDeEntradaSalida.TipodeOperacion", enum_TipodeOperacion.eTO_SALIDA_DE_INVENTARIO), "RenglonNotaES.Cantidad", "0") & " AS Salidas, "
    sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.fSQLEnumValueWithAnd("", "NotaDeEntradaSalida.TipodeOperacion", enum_TipodeOperacion.eTO_Retiro), "RenglonNotaES.Cantidad", "0") & " AS Retiro, "
    sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.fSQLEnumValueWithAnd("", "NotaDeEntradaSalida.TipodeOperacion", enum_TipodeOperacion.eTO_Autoconsumo), "RenglonNotaES.Cantidad", "0") & " AS Autoconsumo, "
    fSqlNotaESCantidadPorOperacion = sqlNotaES
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlNotaESCantidadPorOperacion", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlNotaESCostoPorOperacion() As String
   Dim sqlNotaES As String
   Dim sqlOperacion As String
   On Error GoTo h_ERROR
   sqlOperacion = "RenglonNotaES.Cantidad * ArticuloInventario.CostoUnitario"
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.fSQLEnumValueWithAnd("", "NotaDeEntradaSalida.TipodeOperacion", enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO), sqlOperacion, "0") & " AS EntradaMoneda, "
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.fSQLEnumValueWithAnd("", "NotaDeEntradaSalida.TipodeOperacion", enum_TipodeOperacion.eTO_SALIDA_DE_INVENTARIO), sqlOperacion, "0") & " AS SalidaMoneda, "
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.fSQLEnumValueWithAnd("", "NotaDeEntradaSalida.TipodeOperacion", enum_TipodeOperacion.eTO_Retiro), sqlOperacion, "0") & " AS RetiroMoneda, "
   sqlNotaES = sqlNotaES & gUtilSQL.getIIF(gUtilSQL.fSQLEnumValueWithAnd("", "NotaDeEntradaSalida.TipodeOperacion", enum_TipodeOperacion.eTO_Autoconsumo), sqlOperacion, "0") & " AS AutoconsumoMoneda, "
   fSqlNotaESCostoPorOperacion = sqlNotaES
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCalcularPreciosReconvercion", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLConteoFisicoParaRegistroDetallado(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "ArticuloInventario.Codigo, "
   SQL = SQL & "ArticuloInventario.Descripcion, "
   SQL = SQL & "0 AS ExistenciaAnterior, "
   SQL = SQL & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia >= 0 ", "RenglonConteoFisico.Diferencia", " 0 ") & " as Entrada, "
   SQL = SQL & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia < 0 ", "RenglonConteoFisico.Diferencia * (-1) ", " 0 ") & " As Salida, "
   SQL = SQL & "0 As  Retiro, 0 AS Autoconsumo, "
   SQL = SQL & "0 as ExistenciaActual, "
   SQL = SQL & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia >= 0 ", "RenglonConteoFisico.Diferencia * ArticuloInventario.CostoUnitario", " 0 ") & " as EntradaMoneda, "
   SQL = SQL & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia < 0 ", "RenglonConteoFisico.Diferencia * ArticuloInventario.CostoUnitario* (-1)", " 0 ") & " As SalidaMoneda, "
   SQL = SQL & "0 As  RetiroMoneda, 0 AS AutoconsumoMoneda, "
   SQL = SQL & "0 as ExistenciaMoneda, "
   SQL = SQL & gUtilSQL.DfCStrSQL("ConteoFisico.ConsecutivoConteo") & " As Numero, "
   SQL = SQL & "RenglonConteoFisico.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   SQL = SQL & "0 As COSTO_UNITARIO"
   SQL = SQL & " FROM ConteoFisico  INNER JOIN (ArticuloInventario  INNER JOIN RenglonConteoFisico "
   SQL = SQL & " ON (ArticuloInventario.Codigo = RenglonConteoFisico.CodigoArticulo"
   SQL = SQL & ") AND (ArticuloInventario.ConsecutivoCompania = RenglonConteoFisico.ConsecutivoCompania"
   SQL = SQL & ")) ON (ConteoFisico.ConsecutivoConteo = RenglonConteoFisico.ConsecutivoConteo"
   SQL = SQL & ") AND (ConteoFisico.ConsecutivoCompania = RenglonConteoFisico.ConsecutivoCompania)"
   sqlWhere = " WHERE  (ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   sqlWhere = sqlWhere & " AND (" & gUtilSQL.DfSQLDateValueBetween("ConteoFisico.Fecha", valFechaInicial, valFechaFinal) & ")"
   sqlWhere = sqlWhere & " AND (ConteoFisico.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusConteoFisico.eSC_PROCESADO) & ")"
   sqlWhere = sqlWhere & " AND (ArticuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO) & ")"
   SQL = SQL & sqlWhere
   SQL = SQL & " UNION SELECT RenglonConteoFisico.CodigoArticulo as Codigo, "
   SQL = SQL & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   SQL = SQL & " 0 AS ExistenciaAnterior, "
   SQL = SQL & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia >= 0 ", "RenglonConteoFisico.Diferencia", " 0 ") & " as Entrada, "
   SQL = SQL & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia < 0 ", "RenglonConteoFisico.Diferencia * (-1) ", " 0 ") & " As Salida, "
   SQL = SQL & "0 As  Retiro, 0 AS Autoconsumo, "
   SQL = SQL & "0 as ExistenciaActual, "
   SQL = SQL & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia >= 0 ", "RenglonConteoFisico.Diferencia * ArticuloInventario.CostoUnitario", " 0 ") & " as EntradaMoneda, "
   SQL = SQL & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia < 0 ", "RenglonConteoFisico.Diferencia * ArticuloInventario.CostoUnitario* (-1)", " 0 ") & " As SalidaMoneda, "
   SQL = SQL & "0 As  RetiroMoneda, 0 AS AutoconsumoMoneda, "
   SQL = SQL & "0 as ExistenciaMoneda, "
   SQL = SQL & gUtilSQL.DfCStrSQL("ConteoFisico.ConsecutivoConteo") & " As Numero, "
   SQL = SQL & "RenglonConteoFisico.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   SQL = SQL & "0 As COSTO_UNITARIO"
   SQL = SQL & " FROM conteoFisico INNER JOIN ((((renglonConteoFisico INNER JOIN ExistenciaPorGrupo ON "
   SQL = SQL & " (renglonConteoFisico.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla)) "
   SQL = SQL & " AND (renglonConteoFisico.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) LEFT JOIN articuloInventario ON "
   SQL = SQL & " (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo))"
   SQL = SQL & " LEFT JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)) "
   SQL = SQL & " LEFT JOIN Talla ON (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla)) "
   SQL = SQL & " ON (conteoFisico.ConsecutivoConteo = renglonConteoFisico.ConsecutivoConteo) AND (conteoFisico.ConsecutivoCompania = renglonConteoFisico.ConsecutivoCompania)"
   SQL = SQL & sqlWhere
   fSQLConteoFisicoParaRegistroDetallado = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLConteoFisicoParaRegistroDetallado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLConteoFisico_Para_ExistenciaAnterior(ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "ArticuloInventario.Codigo, "
   SQL = SQL & "ArticuloInventario.Descripcion, "
   SQL = SQL & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia >= 0 ", "RenglonConteoFisico.Diferencia", "RenglonConteoFisico.Diferencia") & "as ExsitenciaAnterior, "
   SQL = SQL & "0 as Entrada, "
   SQL = SQL & "0 As Salida, "
   SQL = SQL & " 0 As  Retiro, "
   SQL = SQL & " 0 As  Autoconsumo, "
   SQL = SQL & "0 as ExistenciaActual, "
   SQL = SQL & "0 as EntradaMoneda, "
   SQL = SQL & "0 As SalidaMoneda, "
   SQL = SQL & " 0 As  RetiroMoneda, "
   SQL = SQL & " 0 As  AutoconsumoMoneda, "
   SQL = SQL & "0 as ExistenciaMoneda, "
   SQL = SQL & gUtilSQL.DfCStrSQL("ConteoFisico.ConsecutivoConteo") & " As Numero, "
   SQL = SQL & "RenglonConteoFisico.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   SQL = SQL & "0 As COSTO_UNITARIO"
   SQL = SQL & " FROM ConteoFisico  INNER JOIN (ArticuloInventario  INNER JOIN RenglonConteoFisico "
   SQL = SQL & " ON (ArticuloInventario.Codigo = RenglonConteoFisico.CodigoArticulo"
   SQL = SQL & ") AND (ArticuloInventario.ConsecutivoCompania = RenglonConteoFisico.ConsecutivoCompania"
   SQL = SQL & ")) ON (ConteoFisico.ConsecutivoConteo = RenglonConteoFisico.ConsecutivoConteo"
   SQL = SQL & ") AND (ConteoFisico.ConsecutivoCompania = RenglonConteoFisico.ConsecutivoCompania)"
   sqlWhere = " WHERE  (ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   sqlWhere = sqlWhere & " AND (ConteoFisico.Fecha < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ")"
   sqlWhere = sqlWhere & " AND (ConteoFisico.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusConteoFisico.eSC_PROCESADO) & ")"
   sqlWhere = sqlWhere & " AND (ArticuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO) & ")"
   SQL = SQL & sqlWhere
   SQL = SQL & " UNION SELECT RenglonConteoFisico.CodigoArticulo as Codigo, "
   SQL = SQL & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   SQL = SQL & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia >= 0 ", "RenglonConteoFisico.Diferencia", "RenglonConteoFisico.Diferencia") & "as ExsitenciaAnterior, "
   SQL = SQL & "0 as Entrada, "
   SQL = SQL & "0 As Salida, "
   SQL = SQL & " 0 As  Retiro, "
   SQL = SQL & " 0 As  Autoconsumo, "
   SQL = SQL & "0 as ExistenciaActual, "
   SQL = SQL & "0 as EntradaMoneda, "
   SQL = SQL & "0 As SalidaMoneda, "
   SQL = SQL & " 0 As  RetiroMoneda, "
   SQL = SQL & " 0 As  AutoconsumoMoneda, "
   SQL = SQL & "0 as ExistenciaMoneda, "
   SQL = SQL & gUtilSQL.DfCStrSQL("ConteoFisico.ConsecutivoConteo") & " As Numero, "
   SQL = SQL & "RenglonConteoFisico.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   SQL = SQL & "0 As COSTO_UNITARIO"
   SQL = SQL & " FROM conteoFisico INNER JOIN ((((renglonConteoFisico INNER JOIN ExistenciaPorGrupo ON "
   SQL = SQL & " (renglonConteoFisico.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla)) "
   SQL = SQL & " AND (renglonConteoFisico.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) LEFT JOIN articuloInventario ON "
   SQL = SQL & " (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo))"
   SQL = SQL & " LEFT JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)) "
   SQL = SQL & " LEFT JOIN Talla ON (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla)) "
   SQL = SQL & " ON (conteoFisico.ConsecutivoConteo = renglonConteoFisico.ConsecutivoConteo) AND (conteoFisico.ConsecutivoCompania = renglonConteoFisico.ConsecutivoCompania)"
   SQL = SQL & sqlWhere
   fSQLConteoFisico_Para_ExistenciaAnterior = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLConteoFisico_Para_ExistenciaAnterior", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLMovInvCompras(ByVal valCodigoProducto As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCompaniaActual As Long) As String
   Dim sqlCompras As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   sqlCompras = "SELECT "
   sqlCompras = sqlCompras & "Compra.Fecha, "
   sqlCompras = sqlCompras & "Compra.numero, "
   sqlCompras = sqlCompras & "ArticuloInventario.Codigo, "
   sqlCompras = sqlCompras & "CompraDetalleArticuloInventario.Cantidad AS CantidadEntrante, "
   sqlCompras = sqlCompras & "0, "
   sqlCompras = sqlCompras & "ArticuloInventario.Descripcion, "
   sqlCompras = sqlCompras & "ArticuloInventario.TipoDeArticulo, "
   sqlCompras = sqlCompras & "'Compra', "
   sqlCompras = sqlCompras & gUtilSQL.fCast("Compra.Consecutivo", eTDSS_VARCHAR, "Numero ") & ", "
   sqlCompras = sqlCompras & "CompraDetalleArticuloInventario.Consecutivo AS CONSECUTIVO_RENGLON"
   sqlCompras = sqlCompras & " FROM Adm.Compra INNER JOIN (ArticuloInventario INNER JOIN Adm.CompraDetalleArticuloInventario ON (ArticuloInventario.Codigo = CompraDetalleArticuloInventario.CodigoArticulo) AND (ArticuloInventario.ConsecutivoCompania = CompraDetalleArticuloInventario.ConsecutivoCompania)) ON (Compra.Consecutivo = CompraDetalleArticuloInventario.ConsecutivoCompra) AND (Compra.ConsecutivoCompania = CompraDetalleArticuloInventario.ConsecutivoCompania)"
   sqlWhere = " WHERE (ArticuloInventario.Codigo  = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto)) & ")"
   sqlWhere = sqlWhere & " AND (ArticuloInventario.ConsecutivoCompania = " & valCompaniaActual & ")"
   sqlWhere = sqlWhere & " AND (" & gUtilSQL.DfSQLDateValueBetween("Compra.Fecha", valFechaInicial, valFechaFinal) & ")"
   sqlWhere = sqlWhere & " AND (Compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE) & ")"
   sqlCompras = sqlCompras & sqlWhere
   sqlCompras = sqlCompras & " UNION SELECT "
   sqlCompras = sqlCompras & "Compra.Fecha, "
   sqlCompras = sqlCompras & "Compra.Numero, "
   sqlCompras = sqlCompras & "CompraDetalleArticuloInventario.CodigoArticulo as Codigo, "
   sqlCompras = sqlCompras & "CompraDetalleArticuloInventario.Cantidad AS CantidadEntrante, "
   sqlCompras = sqlCompras & "0, "
   sqlCompras = sqlCompras & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlCompras = sqlCompras & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   sqlCompras = sqlCompras & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   sqlCompras = sqlCompras & "articuloInventario.TipoDeArticulo, "
   sqlCompras = sqlCompras & "'Compra', "
   sqlCompras = sqlCompras & gUtilSQL.fCast("Compra.Consecutivo", eTDSS_VARCHAR, "Numero ") & ", "
   sqlCompras = sqlCompras & "CompraDetalleArticuloInventario.Consecutivo AS CONSECUTIVO_RENGLON"
   sqlCompras = sqlCompras & " FROM (Adm.Compra INNER JOIN Adm.CompraDetalleArticuloInventario ON (Compra.Consecutivo = CompraDetalleArticuloInventario.ConsecutivoCompra) AND (Compra.ConsecutivoCompania = CompraDetalleArticuloInventario.ConsecutivoCompania)) INNER JOIN (((ExistenciaPorGrupo INNER JOIN ArticuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = ArticuloInventario.Codigo) AND (ExistenciaPorGrupo.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania)) LEFT JOIN Talla ON (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania)) LEFT JOIN Color ON (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor) AND (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania)) ON (CompraDetalleArticuloInventario.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.codigoTalla)) AND (CompraDetalleArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)"
   sqlCompras = sqlCompras & sqlWhere
   fSQLMovInvCompras = sqlCompras
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLMovInvCompras", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExistenciaInicialMovInvNotaES(ByVal valCodigoProducto As String, ByVal valFechaInicial As Date, _
                              ByVal valEntrada As Boolean, ByVal valCompaniaActual As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   If valEntrada Then
      SQL = " SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(IGV_ArticuloNotaEntradaSalida_B3.Entradas)", 0) & " AS ExistenciaInicial "
   Else
      SQL = " SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(IGV_ArticuloNotaEntradaSalida_B3.Salidas)", 0) & " AS ExistenciaInicial "
   End If
   SQL = SQL & " FROM notaDeEntradaSalida INNER JOIN IGV_ArticuloNotaEntradaSalida_B3"
   SQL = SQL & " ON (IGV_ArticuloNotaEntradaSalida_B3.NumeroDocumento = notaDeEntradaSalida.NumeroDocumento"
   SQL = SQL & " AND IGV_ArticuloNotaEntradaSalida_B3.ConsecutivoCompania = notaDeEntradaSalida.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN articuloInventario  ON (articuloInventario.Codigo = IGV_ArticuloNotaEntradaSalida_B3.Codigo"
   SQL = SQL & " AND articuloInventario.ConsecutivoCompania = IGV_ArticuloNotaEntradaSalida_B3.ConsecutivoCompania)"
   SQL = SQL & " WHERE"
   SQL = SQL & " articuloInventario.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto))
   SQL = SQL & " AND (ArticuloInventario.ConsecutivoCompania = " & valCompaniaActual & ")"
   SQL = SQL & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "notaDeEntradaSalida.Fecha", valFechaInicial, "<") & ")"
   SQL = SQL & " AND (articuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO) & ")" & vbCrLf
   If valEntrada Then
      SQL = SQL & " AND (notaDeEntradaSalida.TipodeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO) & ")"
   Else
      SQL = SQL & " AND (notaDeEntradaSalida.TipodeOperacion <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO) & ")"
   End If
   SQL = SQL & " AND (notaDeEntradaSalida.StatusNotaEntradaSalida = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusNotaEntradaSalida.eSNES_Vigente) & ")" & vbCrLf
   
   fSQLExistenciaInicialMovInvNotaES = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExistenciaInicialMovInvNotaES", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExistenciaInicialMovInvCompras(ByVal valCodigoProducto As String, ByVal valFechaInicial As Date, ByVal valCompaniaActual As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR

   SQL = " SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(renglonCompra.CantidadRecibida)", 0) & " AS ExistenciaInicial " & vbCrLf
   SQL = SQL & " FROM compra INNER JOIN (articuloInventario  INNER JOIN renglonCompra" & vbCrLf
   SQL = SQL & " ON (articuloInventario.Codigo = renglonCompra.CodigoArticulo)" & vbCrLf
   SQL = SQL & " AND (articuloInventario.ConsecutivoCompania = renglonCompra.ConsecutivoCompania))" & vbCrLf
   SQL = SQL & " ON (compra.NumeroSecuencial = renglonCompra.NumeroSecuencialCompra)" & vbCrLf
   SQL = SQL & " AND (compra.ConsecutivoCompania = renglonCompra.ConsecutivoCompania)" & vbCrLf
   SQL = SQL & " WHERE" & vbCrLf
   SQL = SQL & " articuloInventario.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto))
   SQL = SQL & " AND (ArticuloInventario.ConsecutivoCompania = " & valCompaniaActual & ")"
   SQL = SQL & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "compra.Fecha", valFechaInicial, "<") & ")"
   SQL = SQL & " AND (articuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO) & ")" & vbCrLf
   SQL = SQL & " AND (compra.EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(False) & ")" & vbCrLf
   SQL = SQL & " AND (compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE) & ")" & vbCrLf
   SQL = SQL & " AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Simple) & ")" & vbCrLf
   '--*** COMPRA TALLA COLOR
   SQL = SQL & " UNION" & vbCrLf
   SQL = SQL & " SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(renglonCompra.CantidadRecibida)", 0) & " AS ExistenciaInicial " & vbCrLf
   SQL = SQL & " FROM compra INNER JOIN renglonCompra" & vbCrLf
   SQL = SQL & " ON (compra.NumeroSecuencial = renglonCompra.NumeroSecuencialCompra AND compra.ConsecutivoCompania = renglonCompra.ConsecutivoCompania)" & vbCrLf
   SQL = SQL & " INNER JOIN ExistenciaPorGrupo ON (ExistenciaPorGrupo.ConsecutivoCompania = RenglonCompra.ConsecutivoCompania" & vbCrLf
   SQL = SQL & " AND ExistenciaPorGrupo.CodigoArticulo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla = RenglonCompra.CodigoArticulo)" & vbCrLf
   SQL = SQL & " INNER JOIN ArticuloInventario ON (ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania" & vbCrLf
   SQL = SQL & " AND ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)" & vbCrLf
   SQL = SQL & " WHERE" & vbCrLf
   SQL = SQL & " articuloInventario.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto))
   SQL = SQL & " AND (ArticuloInventario.ConsecutivoCompania = " & valCompaniaActual & ")"
   SQL = SQL & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "compra.Fecha", valFechaInicial, "<") & ")"
   SQL = SQL & " AND (articuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO) & ")" & vbCrLf
   SQL = SQL & " AND (compra.EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(False) & ")" & vbCrLf
   SQL = SQL & " AND (compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE) & ")" & vbCrLf
   SQL = SQL & " AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_Color) & ")"
   '--*** COMPRA SERIAL
   SQL = SQL & " UNION" & vbCrLf
   SQL = SQL & " SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(RenglonCompraXSerial.Cantidad)", 0) & " AS ExistenciaInicial " & vbCrLf
   SQL = SQL & " FROM compra INNER JOIN RenglonCompraXSerial" & vbCrLf
   SQL = SQL & " ON (compra.NumeroSecuencial = RenglonCompraXSerial.NumeroSecuencialCompra AND compra.ConsecutivoCompania = RenglonCompraXSerial.ConsecutivoCompania)" & vbCrLf
   SQL = SQL & " INNER JOIN ExistenciaPorGrupo ON (ExistenciaPorGrupo.ConsecutivoCompania = RenglonCompraXSerial.ConsecutivoCompania" & vbCrLf
   SQL = SQL & " AND ExistenciaPorGrupo.CodigoArticulo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla = RenglonCompraXSerial.CodigoArticulo" & vbCrLf
   SQL = SQL & " AND ExistenciaPorGrupo.Serial = RenglonCompraXSerial.Serial AND ExistenciaPorGrupo.Rollo = RenglonCompraXSerial.Rollo)" & vbCrLf
   SQL = SQL & " INNER JOIN ArticuloInventario ON (ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania" & vbCrLf
   SQL = SQL & " AND ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)" & vbCrLf
   SQL = SQL & " WHERE articuloInventario.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto))
   SQL = SQL & " AND (compra.ConsecutivoCompania = " & valCompaniaActual & ")" & vbCrLf
   SQL = SQL & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "compra.Fecha", valFechaInicial, "<") & ")"
   SQL = SQL & " AND (articuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO) & ")" & vbCrLf
   SQL = SQL & " AND (compra.EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(False) & ")" & vbCrLf
   SQL = SQL & " AND (compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE) & ")" & vbCrLf
   SQL = SQL & " AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaSerial) & " OR ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaSerial_Rollo) & ")" & vbCrLf
   '--*** COMPRA TALLA COLOR SERIAL
   SQL = SQL & " UNION" & vbCrLf
   SQL = SQL & " SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(RenglonCompraXSerial.Cantidad)", 0) & " AS ExistenciaInicial " & vbCrLf
   SQL = SQL & " FROM compra INNER JOIN RenglonCompraXSerial" & vbCrLf
   SQL = SQL & " ON (compra.NumeroSecuencial = RenglonCompraXSerial.NumeroSecuencialCompra AND compra.ConsecutivoCompania = RenglonCompraXSerial.ConsecutivoCompania)" & vbCrLf
   SQL = SQL & " INNER JOIN ExistenciaPorGrupo ON (ExistenciaPorGrupo.ConsecutivoCompania = RenglonCompraXSerial.ConsecutivoCompania" & vbCrLf
   SQL = SQL & " AND ExistenciaPorGrupo.CodigoArticulo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla = RenglonCompraXSerial.CodigoArticulo" & vbCrLf
   SQL = SQL & " AND ExistenciaPorGrupo.Serial = RenglonCompraXSerial.Serial AND ExistenciaPorGrupo.Rollo = RenglonCompraXSerial.Rollo)" & vbCrLf
   SQL = SQL & " INNER JOIN ArticuloInventario ON (ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania" & vbCrLf
   SQL = SQL & " AND ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)" & vbCrLf
   SQL = SQL & " WHERE articuloInventario.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto))
   SQL = SQL & " AND (compra.ConsecutivoCompania = " & valCompaniaActual & ")" & vbCrLf
   SQL = SQL & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "compra.Fecha", valFechaInicial, "<") & ")"
   SQL = SQL & " AND (articuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO) & ")" & vbCrLf
   SQL = SQL & " AND (compra.EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(False) & ")" & vbCrLf
   SQL = SQL & " AND (compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE) & ")" & vbCrLf
   SQL = SQL & " AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial) & ")" & vbCrLf


   fSQLExistenciaInicialMovInvCompras = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLExistenciaInicialMovInvCompras", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteListaDePrecios(ByVal valLineaDeProductoAImprimir As String, ByVal valStatus As Integer, ByVal valOrdenarPorCodigo As Boolean, ByVal valCantidadAImprimirLineaDeProducto As String, ByVal valAgruparPorLineaDeProducto As Boolean, ByVal valCodigoPais As String, ByVal valCompaniaActual As Long, ByVal valOrdenamientoCodigo As String, ByVal valBuscarPais As Boolean, ByVal gMonedaLocalActual As Object, ByVal valTasaMoneda As Currency, ByVal valMonedaReporte As String, ByVal valUsaMultiMoneda As Boolean, ByVal valEsMonedaLocalATasaDelDia As Boolean) As String
   Dim Sql As String
   Dim SQLTasaMoneda As String
   Dim SQLCodigoArticulo As String
   Dim vCountryCode As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Const DateInitBsF = #1/1/2008#
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   If valBuscarPais Then
      vCountryCode = valCodigoPais
   End If
   If valOrdenamientoCodigo Then
      SQLCodigoArticulo = gUtilSQL.DfRightSQL("articuloInventario.Codigo", 30, "               ")
   Else 'If gProyParametros.GetOrdenamientoDeCodigoStringAsEnum = eFD_NORMAL Then
      SQLCodigoArticulo = "articuloInventario.Codigo"
   End If
   SQLTasaMoneda = gUtilSQL.fNumToStrSQL(valTasaMoneda)
   SQL = "SELECT "
   SQL = SQL & SQLCodigoArticulo & " AS CodArt, "
   SQL = SQL & "articuloInventario.Descripcion, "
   SQL = SQL & "articuloInventario.LineaDeProducto,  "
   SQL = SQL & "articuloInventario.Marca,  "
   SQL = SQL & "articuloInventario.PrecioSinIva, "
   SQL = SQL & "articuloInventario.PrecioSinIva2, "
   SQL = SQL & "articuloInventario.PrecioSinIva3, "
   SQL = SQL & "articuloInventario.PrecioSinIva4, "
   SQL = SQL & "articuloInventario.PrecioConIva, "
   SQL = SQL & "articuloInventario.PrecioConIva2, "
   SQL = SQL & "articuloInventario.PrecioConIva3, "
   SQL = SQL & "articuloInventario.PrecioConIva4, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioSinIva", "articuloInventario.FechaUltimaModificacion") & " AS PrecioSinIvaEnBsf, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioSinIva2", "articuloInventario.FechaUltimaModificacion") & " AS PrecioSinIVA2EnBsf, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioSinIva3", "articuloInventario.FechaUltimaModificacion") & " AS PrecioSinIVA3EnBsf, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioSinIva4", "articuloInventario.FechaUltimaModificacion") & " AS PrecioSinIVA4EnBsf, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioConIva", "articuloInventario.FechaUltimaModificacion") & " AS PrecioConIVAEnBsf, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioConIva2", "articuloInventario.FechaUltimaModificacion") & " AS PrecioConIVA2EnBsf, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioConIva3", "articuloInventario.FechaUltimaModificacion") & " AS PrecioConIVA3EnBsf, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioConIva4", "articuloInventario.FechaUltimaModificacion") & " AS PrecioConIVA4EnBsf, "
   SQL = SQL & "articuloInventario.Existencia, "
   SQL = SQL & "articuloInventario.CantArtReservado, "
   SQL = SQL & "articuloInventario.Existencia - articuloInventario.CantArtReservado as Disponibiliad, "
   If valUsaMultiMoneda And valTasaMoneda > 0 And Not valEsMonedaLocalATasaDelDia Then
      SQL = SQL & "articuloInventario.PrecioSinIva/" & SQLTasaMoneda & " AS MePrecioSinIVA, "
      SQL = SQL & "articuloInventario.PrecioSinIva2/" & SQLTasaMoneda & " AS MePrecioSinIVA2, "
      SQL = SQL & "articuloInventario.PrecioSinIva3/" & SQLTasaMoneda & " MePrecioSinIVA3, "
      SQL = SQL & "articuloInventario.PrecioSinIva4/" & SQLTasaMoneda & " AS MePrecioSinIVA4, "
      SQL = SQL & "articuloInventario.PrecioConIva/" & SQLTasaMoneda & " AS MePrecioConIVA, "
      SQL = SQL & "articuloInventario.PrecioConIva2/" & SQLTasaMoneda & " AS MePrecioConIVA2, "
      SQL = SQL & "articuloInventario.PrecioConIva3/" & SQLTasaMoneda & " AS MePrecioConIVA3, "
      SQL = SQL & "articuloInventario.PrecioConIva4/" & SQLTasaMoneda & " AS MePrecioConIVA4, "
   ElseIf valUsaMultiMoneda And valTasaMoneda >= 0 And valEsMonedaLocalATasaDelDia Then
      SQL = SQL & "ISNULL(camposMonedaExtranjera.MePrecioSinIVA, 0) * " & SQLTasaMoneda & " AS MePrecioSinIVA, "
      SQL = SQL & "ISNULL(camposMonedaExtranjera.MePrecioSinIVA2, 0) * " & SQLTasaMoneda & " AS MePrecioSinIVA2, "
      SQL = SQL & "ISNULL(camposMonedaExtranjera.MePrecioSinIVA3, 0) * " & SQLTasaMoneda & " AS MePrecioSinIVA3, "
      SQL = SQL & "ISNULL(camposMonedaExtranjera.MePrecioSinIVA4, 0) * " & SQLTasaMoneda & " AS MePrecioSinIVA4, "
      SQL = SQL & "ISNULL(camposMonedaExtranjera.MePrecioConIVA, 0) * " & SQLTasaMoneda & " AS MePrecioConIVA, "
      SQL = SQL & "ISNULL(camposMonedaExtranjera.MePrecioConIVA2, 0) * " & SQLTasaMoneda & " AS MePrecioConIVA2, "
      SQL = SQL & "ISNULL(camposMonedaExtranjera.MePrecioConIVA3, 0) * " & SQLTasaMoneda & " AS MePrecioConIVA3, "
      SQL = SQL & "ISNULL(camposMonedaExtranjera.MePrecioConIVA4, 0) * " & SQLTasaMoneda & " AS MePrecioConIVA4, "
   Else
      SQL = SQL & "(CASE WHEN (camposMonedaExtranjera.MePrecioSinIVA Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioSinIVA  end) AS MePrecioSinIVA , "
      SQL = SQL & "(CASE WHEN (camposMonedaExtranjera.MePrecioSinIVA2 Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioSinIVA2  end) AS MePrecioSinIVA2 , "
      SQL = SQL & "(CASE WHEN (camposMonedaExtranjera.MePrecioSinIVA3 Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioSinIVA3  end) AS MePrecioSinIVA3 , "
      SQL = SQL & "(CASE WHEN (camposMonedaExtranjera.MePrecioSinIVA4 Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioSinIVA4  end) AS MePrecioSinIVA4 , "
      SQL = SQL & "(CASE WHEN (camposMonedaExtranjera.MePrecioConIVA Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioConIVA  end) AS MePrecioConIVA , "
      SQL = SQL & "(CASE WHEN (camposMonedaExtranjera.MePrecioConIVA2 Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioConIVA2  end) AS MePrecioConIVA2 , "
      SQL = SQL & "(CASE WHEN (camposMonedaExtranjera.MePrecioConIVA3 Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioConIVA3  end) AS MePrecioConIVA3 , "
      SQL = SQL & "(CASE WHEN (camposMonedaExtranjera.MePrecioConIVA4 Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioConIVA4  end) AS MePrecioConIVA4 , "
   End If
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("articuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " As Status, "
   SQL = SQL & " articuloInventario.CampoDefinible1, "
   SQL = SQL & " articuloInventario.CampoDefinible2, "
   SQL = SQL & " articuloInventario.CampoDefinible3, "
   SQL = SQL & " articuloInventario.CampoDefinible4, "
   SQL = SQL & " articuloInventario.CampoDefinible5"
   If valUsaMultiMoneda And Not valEsMonedaLocalATasaDelDia Then
      SQL = SQL & " , '" & valMonedaReporte & "' AS MonedaReporte"
   End If
   SQL = SQL & " FROM articuloInventario LEFT JOIN camposMonedaExtranjera"
   SQL = SQL & " ON articuloInventario.Codigo = camposMonedaExtranjera.Codigo"
   SQL = SQL & " AND articuloInventario.ConsecutivoCompania  = camposMonedaExtranjera.ConsecutivoCompania"
   SQL = SQL & " WHERE articuloInventario.ConsecutivoCompania  = " & valCompaniaActual
   If valCantidadAImprimirLineaDeProducto Then
      SQL = SQL & " AND articuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductoAImprimir))
   End If
   If valStatus = enum_StatusArticulo.eSA_VIGENTE Or valStatus = enum_StatusArticulo.eSA_DESINCORPORADO Then
      SQL = SQL & " AND StatusdelArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(valStatus)
   End If
   SQL = SQL & " AND articuloInventario.Codigo NOT IN (" & _
               gUtilSQL.fSimpleSqlValue("RD_AliGeneral @") & ", " & _
               gUtilSQL.fSimpleSqlValue("RD_AliGeneralNC @") & ", " & _
               gUtilSQL.fSimpleSqlValue("RD_AliReducida@") & ", " & _
               gUtilSQL.fSimpleSqlValue("RD_AliReducidaNC @") & ", " & _
               gUtilSQL.fSimpleSqlValue("RD_AliExentaNC @") & ", " & _
               gUtilSQL.fSimpleSqlValue("RD_AliExtendidaNC @") & ", " & _
               gUtilSQL.fSimpleSqlValue(getCodigoArticuloResumenDiario(eTD_EXENTO)) & ", " & _
               gUtilSQL.fSimpleSqlValue(getCodigoArticuloResumenDiario(eTD_ALICUOTAGENERAL)) & ", " & _
               gUtilSQL.fSimpleSqlValue(getCodigoArticuloResumenDiario(eTD_ALICUOTA2)) & ", " & _
               gUtilSQL.fSimpleSqlValue(getCodigoArticuloResumenDiario(eTD_ALICUOTA3)) & ", " & _
               gUtilSQL.fSimpleSqlValue(getCodigoComisionXPorcentajeDeAlmacenaje) & ")"
   If valAgruparPorLineaDeProducto Then
   SQL = SQL & " ORDER BY "
   SQL = SQL & " articuloInventario.LineaDeProducto, "
      If valOrdenarPorCodigo Then
         SQL = SQL & SQLCodigoArticulo & ", "
         SQL = SQL & "articuloInventario.Descripcion"
      Else
         SQL = SQL & "articuloInventario.Descripcion, "
      SQL = SQL & SQLCodigoArticulo
      End If
   Else
      SQL = SQL & " ORDER BY "
      If valOrdenarPorCodigo Then
         SQL = SQL & SQLCodigoArticulo & ", "
         SQL = SQL & "articuloInventario.Descripcion, "
      Else
         SQL = SQL & "articuloInventario.Descripcion, "
         SQL = SQL & SQLCodigoArticulo & ", "
      End If
      SQL = SQL & "articuloInventario.LineaDeProducto"
   End If
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteListaDePrecios = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSQLDelReporteListaDePrecios", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function getCodigoArticuloResumenDiario(ByVal valAlicuota As enum_TipoDeAlicuota) As String
   On Error GoTo h_ERROR
   Select Case valAlicuota
      Case eTD_EXENTO: getCodigoArticuloResumenDiario = "RD_AliExenta  @"
      Case eTD_ALICUOTAGENERAL: getCodigoArticuloResumenDiario = "RD_AliGeneral @"
      Case eTD_ALICUOTA2: getCodigoArticuloResumenDiario = "RD_AliReducida@"
      Case eTD_ALICUOTA3: getCodigoArticuloResumenDiario = "RD_AliExtendida"
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "getCodigoArticuloResumenDiario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function getCodigoComisionXPorcentajeDeAlmacenaje() As String
   On Error GoTo h_ERROR
      getCodigoComisionXPorcentajeDeAlmacenaje = "RD_ComXPorcDeAlmacen @"
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "getCodigoComisionXPorcentajeDeAlmacenaje", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLInsertarArticuloEspecial(ByVal valConsecutivoCompania As Long, ByVal valCodigo As String, ByVal valDescripción As String, ByVal valAlicuota As enum_TipoDeAlicuota, ByVal valNombreUsuario As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "INSERT INTO articuloInventario(ConsecutivoCompania, "
   SQL = SQL & "Codigo, Descripcion, "
   SQL = SQL & "LineaDeProducto, StatusdelArticulo, "
   SQL = SQL & "TipoDeArticulo, AlicuotaIva, "
   SQL = SQL & "PrecioSinIva, PrecioConIva, "
   SQL = SQL & "PrecioSinIva2, PrecioConIva2, "
   SQL = SQL & "PrecioSinIva3, PrecioConIva3, "
   SQL = SQL & "PrecioSinIva4, PrecioConIva4, "
   SQL = SQL & "PorcentajeBaseImponible, CostoUnitario, "
   SQL = SQL & "Existencia, CantidadMinima, CantidadMaxima, "
   SQL = SQL & "Categoria, TipoDeProducto, "
   SQL = SQL & "NombrePrograma, Marca, "
   SQL = SQL & "FechaDeVencimiento, UnidadDeVenta, "
   SQL = SQL & "CampoDefinible1, CampoDefinible2, "
   SQL = SQL & "CampoDefinible3, CampoDefinible4, "
   SQL = SQL & "CampoDefinible5, MeCostoUnitario, "
   SQL = SQL & "UnidadDeVentaSecundaria, CuentaContableIngreso, "
   SQL = SQL & "ExcluirDeComision, "
   SQL = SQL & "CodigoLote, NombreOperador, FechaUltimaModificacion, "
   SQL = SQL & "TipoArticuloInv "
   SQL = SQL & ", CostoPromedio"
   SQL = SQL & ", RecalcularCierre "
   SQL = SQL & ", RecalcularCosto "
   SQL = SQL & ", FechaCierreActualizada "
   SQL = SQL & ", CuentaCostoDeVenta"
   SQL = SQL & ", CuentaInventario"
   SQL = SQL & ", ComisionaPorcentaje"
   SQL = SQL & ") SELECT "
   SQL = SQL & valConsecutivoCompania & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(valCodigo) & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(valDescripción) & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("LINEA DE PRODUCTO") & ", "
   SQL = SQL & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusArticulo.eSA_VIGENTE) & ", "
   SQL = SQL & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO) & ", "
   SQL = SQL & gUtilSQL.fSQLSimpleValueForEnum(valAlicuota) & ", "
   SQL = SQL & "0, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("CATEGORIA") & ", "
   SQL = SQL & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeProducto.eTD_TP_NUEVO) & ", "
   SQL = SQL & "'', '', "
   SQL = SQL & gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy) & ", "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("UNIDADES") & ", "
   SQL = SQL & "'', '', '', '', '', 0, 0, '','N', '0', "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(valNombreUsuario) & ", "
   SQL = SQL & gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy) & ",'0'"
   SQL = SQL & ", 0 "
   SQL = SQL & ", " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & ", " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & ", " & gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy)
   SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("")
   SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("")
   SQL = SQL & ", " & gUtilSQL.fBooleanToSqlValue(True)
   SQL = SQL & " WHERE NOT EXISTS(SELECT ConsecutivoCompania, Codigo FROM articuloInventario WHERE ConsecutivoCompania = " & valConsecutivoCompania & " AND Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigo) & ")"
   
h_EXIT: On Error GoTo 0
   fSQLInsertarArticuloEspecial = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLInsertarArticuloEspecial", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCamposRequeridos(ByVal valNumero As String, ByVal valConsecutivoCompania As Long) As String
   Dim strCampos As String
   Dim SQL As String
   On Error GoTo h_ERROR
   strCampos = "ConsecutivoCompania,"
   strCampos = strCampos & "Codigo,"
   strCampos = strCampos & "Descripcion,"
   strCampos = strCampos & "LineaDeProducto,"
   strCampos = strCampos & "StatusdelArticulo, "
   strCampos = strCampos & "TipoDeArticulo,"
   strCampos = strCampos & "AlicuotaIVA,"
   strCampos = strCampos & "PrecioSinIVA,"
   strCampos = strCampos & "PrecioConIVA,"
   strCampos = strCampos & "PrecioSinIVA2,"
   strCampos = strCampos & "PrecioConIVA2,"
   strCampos = strCampos & "PrecioSinIVA3,"
   strCampos = strCampos & "PrecioConIVA3,"
   strCampos = strCampos & "PrecioSinIVA4,"
   strCampos = strCampos & "PrecioConIVA4,"
   strCampos = strCampos & "PorcentajeBaseImponible,"
   strCampos = strCampos & "CostoUnitario,"
   strCampos = strCampos & "Existencia,"
   strCampos = strCampos & "CantidadMinima,"
   strCampos = strCampos & "CantidadMaxima,"
   strCampos = strCampos & "Categoria,"
   strCampos = strCampos & "TipoDeProducto,"
   strCampos = strCampos & "NombrePrograma,"
   strCampos = strCampos & "Marca,"
   strCampos = strCampos & "FechaDeVencimiento,"
   strCampos = strCampos & "UnidadDeVenta,"
   strCampos = strCampos & "CampoDefinible1,"
   strCampos = strCampos & "CampoDefinible2,"
   strCampos = strCampos & "CampoDefinible3,"
   strCampos = strCampos & "CampoDefinible4,"
   strCampos = strCampos & "CampoDefinible5,"
   strCampos = strCampos & "MeCostoUnitario,"
   strCampos = strCampos & "UnidadDeVentaSecundaria,"
   strCampos = strCampos & "CuentaContableIngreso,"
   strCampos = strCampos & "CodigoLote,"
   strCampos = strCampos & "PorcentajeComision,"
   strCampos = strCampos & "ExcluirDeComision,"
   strCampos = strCampos & "MargenGanancia,"
   strCampos = strCampos & "MargenGanancia2,"
   strCampos = strCampos & "MargenGanancia3,"
   strCampos = strCampos & "MargenGanancia4,"
   strCampos = strCampos & "CuentaCostoDeVenta,"
   strCampos = strCampos & "CuentaInventario,"
   strCampos = strCampos & "ComisionaPorcentaje,"
   strCampos = strCampos & "GastosAdmisibles,"
   strCampos = strCampos & "MargenGananciaXLey,"
   strCampos = strCampos & "MargenGananciaXLey2,"
   strCampos = strCampos & "MargenGananciaXLey3,"
   strCampos = strCampos & "MargenGananciaXLey4, "
   strCampos = strCampos & "UsaBalanza "
   SQL = "SELECT " & strCampos & " FROM articuloInventario " & _
         " WHERE Codigo = " & gUtilSQL.fSimpleSqlValue(valNumero) & _
         " AND ConsecutivoCompania = " & valConsecutivoCompania
   fSQLCamposRequeridos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLCamposRequeridos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryNotaES(ByVal valTipoOperacion As enum_TipodeOperacion, ByVal valCodigoAlmacen As String, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, _
               ByVal valCodigoCompuesto As String, ByVal valCodigo As String, ByVal valSerial As String, ByVal valRollo As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT ISNULL(SUM(RenglonNotaES.Cantidad), 0) AS SumCantidad"
   SQL = SQL & " FROM notaDeEntradaSalida INNER JOIN renglonNotaES "
   SQL = SQL & " ON notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento"
   SQL = SQL & " AND notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania"
   SQL = SQL & " WHERE notaDeEntradaSalida.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND notaDeEntradaSalida.StatusNotaEntradaSalida = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusNotaEntradaSalida.eSNES_Vigente)
   SQL = SQL & " AND notaDeEntradaSalida.CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
   SQL = SQL & " AND notaDeEntradaSalida.TipodeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(valTipoOperacion)
   If valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
      SQL = SQL & " AND renglonNotaES.CodigoArticulo =" & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
      If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND RenglonNotaES.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            SQL = SQL & " AND renglonNotaES.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   Else
      SQL = SQL & " AND renglonNotaES.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
      If valTipoArticuloInvAsEnum = eTAI_UsaSerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND renglonNotaES.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
      ElseIf valTipoArticuloInvAsEnum = eTAI_UsaSerial_Rollo Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND RenglonNotaES.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            SQL = SQL & " AND renglonNotaES.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   End If
   fQueryNotaES = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryNotaES", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryTransferencia(ByVal valTipoOperacion As enum_TipodeOperacion, ByVal valCodigoAlmacen As String, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, _
                  ByVal valCodigoCompuesto As String, ByVal valCodigo As String, ByVal valSerial As String, ByVal valRollo As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT ISNULL(SUM(RenglonTransferencia.Cantidad), 0) AS SumCantidad "
   SQL = SQL & " FROM Transferencia INNER JOIN RenglonTransferencia "
   SQL = SQL & " ON Transferencia.NumeroDocumento = RenglonTransferencia.NumeroDocumento"
   SQL = SQL & " AND Transferencia.ConsecutivoCompania = RenglonTransferencia.ConsecutivoCompania"
   SQL = SQL & " WHERE Transferencia.ConsecutivoCompania = " & valConsecutivoCompania
   If valTipoOperacion = eTO_ENTRADA_DE_INVENTARIO Then
      SQL = SQL & " AND Transferencia.CodigoAlmacenEntrada = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
   ElseIf valTipoOperacion = eTO_SALIDA_DE_INVENTARIO Then
      SQL = SQL & " AND Transferencia.CodigoAlmacenSalida = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
   End If
   If valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
      SQL = SQL & " AND RenglonTransferencia.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
      If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND RenglonTransferencia.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            SQL = SQL & " AND RenglonTransferencia.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   Else
      SQL = SQL & " AND RenglonTransferencia.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
      If valTipoArticuloInvAsEnum = eTAI_UsaSerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND RenglonTransferencia.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
      ElseIf valTipoArticuloInvAsEnum = eTAI_UsaSerial_Rollo Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND RenglonTransferencia.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            SQL = SQL & " AND RenglonTransferencia.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   End If
   fQueryTransferencia = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryTransferencia", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryFactura(ByVal valCodigoAlmacen As String, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, ByVal valCodigoCompuesto As String, _
                  ByVal valCodigo As String, ByVal valSerial As String, ByVal valRollo As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT ISNULL(SUM(Renglonfactura.Cantidad), 0) AS SumCantidad"
   SQL = SQL & " FROM factura INNER JOIN renglonFactura "
   SQL = SQL & " ON factura.Numero = renglonFactura.NumeroFactura "
   SQL = SQL & " AND factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania "
   SQL = SQL & " AND factura.TipoDeDocumento = renglonFactura.TipoDeDocumento "
   SQL = SQL & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND factura.CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
   SQL = SQL & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(eSF_EMITIDA)
   SQL = SQL & " AND factura.GeneradaPorNotaEntrega = " & gUtilSQL.fSQLSimpleValueForEnum(enum_FacturaGeneraNotaEntrega.eFGNE_NoGenerada)
   If valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
      SQL = SQL & " AND renglonFactura.Articulo = " & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
      If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND Renglonfactura.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            SQL = SQL & " AND renglonfactura.rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   Else
      SQL = SQL & " AND renglonFactura.Articulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
      If valTipoArticuloInvAsEnum = eTAI_UsaSerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND Renglonfactura.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
      ElseIf valTipoArticuloInvAsEnum = eTAI_UsaSerial_Rollo Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND Renglonfactura.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            SQL = SQL & " AND Renglonfactura.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   End If
   fQueryFactura = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryFactura", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryFacturaPC(ByVal valCodigoAlmacen As String, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, ByVal valCodigoCompuesto As String, _
                  ByVal valCodigo As String, ByVal valSerial As String, ByVal valRollo As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT ISNULL(SUM(Renglonfactura.Cantidad*productoCompuesto.cantidad), 0) AS SumCantidad "
   SQL = SQL & " FROM ProductoCompuesto INNER JOIN ArticuloInventario"
   SQL = SQL & "   ON ProductoCompuesto.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania"
   SQL = SQL & "   AND ProductoCompuesto.CodigoConexionConElMaster = ArticuloInventario.Codigo"
   SQL = SQL & "   INNER JOIN factura INNER JOIN renglonFactura"
   SQL = SQL & "   ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania"
   SQL = SQL & "   AND factura.Numero = renglonFactura.NumeroFactura"
   SQL = SQL & "   AND factura.TipoDeDocumento = renglonFactura.TipoDeDocumento"
   SQL = SQL & "   ON ArticuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania"
   SQL = SQL & "   AND ArticuloInventario.Codigo = renglonFactura.Articulo"
   
   SQL = SQL & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND factura.CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
   SQL = SQL & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(eSF_EMITIDA)
   If valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
      SQL = SQL & " AND productoCompuesto.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
      If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND Renglonfactura.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            SQL = SQL & " AND renglonfactura.rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   Else
      SQL = SQL & " AND productoCompuesto.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
      If valTipoArticuloInvAsEnum = eTAI_UsaSerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND Renglonfactura.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
      ElseIf valTipoArticuloInvAsEnum = eTAI_UsaSerial_Rollo Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND Renglonfactura.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            SQL = SQL & " AND Renglonfactura.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   End If
   fQueryFacturaPC = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fQueryFacturaPC", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryConteoFisico(ByVal valCodigoAlmacen As String, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, ByVal valCodigoCompuesto As String, ByVal valCodigo As String, ByVal valSerial As String, ByVal valRollo As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   SQL = "SELECT ISNULL(SUM(Diferencia) ,0) AS SumCantidad"
   SQL = SQL & " FROM ConteoFisico INNER JOIN RenglonConteoFisico "
   SQL = SQL & " ON ConteoFisico.ConsecutivoConteo = RenglonConteoFisico.ConsecutivoConteo"
   SQL = SQL & " AND ConteoFisico.ConsecutivoCompania = RenglonConteoFisico.ConsecutivoCompania"
   SQL = SQL & " WHERE ConteoFisico.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND ConteoFisico.CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
   SQL = SQL & " AND ConteoFisico.Status = " & gUtilSQL.fSQLSimpleValueForEnum(eSC_PROCESADO)
   If valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
      SQL = SQL & " AND RenglonConteoFisico.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
      If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND RenglonConteoFisico.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            SQL = SQL & " AND RenglonConteoFisico.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   Else
      SQL = SQL & " AND RenglonConteoFisico.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
      If valTipoArticuloInvAsEnum = eTAI_UsaSerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND RenglonConteoFisico.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
      ElseIf valTipoArticuloInvAsEnum = eTAI_UsaSerial_Rollo Then
         If (valSerial <> "" And valSerial <> "0") Then
            SQL = SQL & " AND RenglonConteoFisico.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            SQL = SQL & " AND RenglonConteoFisico.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   fQueryConteoFisico = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryConteoFisico", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlCostoUltimaCompraReporteDetallado(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim SQL As String
Dim sqlWhere As String
Dim SQLCodigo   As String
Dim sqlWhere2 As String
Dim gEnumProyecto As clsEnumAdministrativo
On Error GoTo h_ERROR
Set gEnumProyecto = New clsEnumAdministrativo
       
   sqlWhere = gUtilSQL.fSQLValueBetween("CODIGO", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(sqlWhere, "Fecha", valFechaInicial, valFechaFinal)
   sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, "ConsecutivoCompania", gUtilSQL.fNumToStrSQL(valConsecutivoCompania), True)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   sqlWhere2 = gUtilSQL.fSQLEnumValueWithAnd("", "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   sqlWhere2 = gUtilSQL.getWhereSQL(sqlWhere2)
    
 SQL = "; WITH  ArticulosyCostos(ConsecutivoCompania,PrioridadOperacion, NumeroDocumento, "
 SQL = SQL & " Codigo, "
 SQL = SQL & " TipoDeDocumento,"
 SQL = SQL & " Fecha,"
 SQL = SQL & " Descripcion,"
 SQL = SQL & " entrada,"
 SQL = SQL & " salidad,"
 SQL = SQL & " MontoOperacion,"
 SQL = SQL & " existencia,"
 SQL = SQL & " costoAnterior,"
 SQL = SQL & " costoActual) AS "
 SQL = SQL & " ( SELECT "
 SQL = SQL & " ConsecutivoCompania,"
 SQL = SQL & " PrioridadOperacion,"
 SQL = SQL & " NumeroDocumento,"
 SQL = SQL & " Codigo,"
 SQL = SQL & " TipoDeDocumento,"
 SQL = SQL & " Fecha,"
 SQL = SQL & " Descripcion,"
 SQL = SQL & " entrada,"
 SQL = SQL & " salidad,"
 SQL = SQL & " MontoOperacion,"
 SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.Gf_Existencia(ConsecutivoCompania,Fecha,Codigo, PrioridadOperacion, NumeroDocumento)"), gUtilSQL.fNumToStrSQL(0), "dbo.Gf_Existencia(ConsecutivoCompania,Fecha,Codigo, PrioridadOperacion, NumeroDocumento)", False) & " As existencia, "
 SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha-1,Codigo )"), gUtilSQL.fNumToStrSQL(0), "dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha-1,Codigo )", False) & " As costoAnterior,"
 SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha,Codigo )"), gUtilSQL.fNumToStrSQL(0), "dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha,Codigo )", False) & " As costoActual "
 
SQL = SQL & "  FROM   IGV_ArticuloInvMovimiento  "

SQL = SQL & sqlWhere
SQL = SQL & " ) "

SQL = SQL & "  select"
SQL = SQL & "  ArticulosyCostos.codigo,"
SQL = SQL & "  ArticulosyCostos.descripcion,"
SQL = SQL & "  fecha,"
SQL = SQL & "  tipodedocumento,"
SQL = SQL & "  ArticulosyCostos.existencia AS ExistenciaInicial,"
SQL = SQL & "  costoAnterior as CostoInicial,"
SQL = SQL & "  ArticulosyCostos.existencia * costoAnterior  as MontoInicial,"
SQL = SQL & "  entrada ,"
SQL = SQL & gUtilSQL.getIIF("MontoOperacion = 0", "costoAnterior *  entrada", "MontoOperacion") & " As MontoEntrada, "

SQL = SQL & "  salidad,"
SQL = SQL & "  costoAnterior *  salidad as MontoSalidad,"
SQL = SQL & "  ArticulosyCostos.existencia + entrada - salidad as existenciaFinal,"

SQL = SQL & gUtilSQL.getIIF("MontoOperacion = 0", "costoActual", "MontoOperacion / entrada") & " As CostoFinal, "

SQL = SQL & gUtilSQL.getIIF("MontoOperacion = 0", "costoActual", "MontoOperacion / entrada") & " * (ArticulosyCostos.Existencia + entrada - salidad ) As MontoFinal "
SQL = SQL & "   From ArticulosyCostos "
SQL = SQL & "   INNER JOIN  dbo.articuloInventario   ON  "
SQL = SQL & "   ArticulosyCostos.ConsecutivoCompania = articuloInventario.ConsecutivoCompania AND   "
SQL = SQL & "   ArticulosyCostos.Codigo = articuloInventario.Codigo"
SQL = SQL & sqlWhere2

SQL = SQL & "  order by codigo, fecha, PrioridadOperacion, numerodocumento"
    
Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
  fSqlCostoUltimaCompraReporteDetallado = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlExistenciaEntreFechaExitenciaAnterior(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFecha As Date, ByVal valWhere As String) As String
Dim insArticuloView As clsArticuloInvVista
Dim SQL As String
Dim sqlWhere As String
On Error GoTo h_ERROR
Set insArticuloView = New clsArticuloInvVista

sqlWhere = gUtilSQL.fSQLValueDateWithComparisonRule(valWhere, insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Fecha", valFecha, "<")
sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania", valConsecutivoCompania)
sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)

SQL = SQL & " SELECT "

SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania AS ConsecutivoCompania ,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo AS Codigo ,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion    AS Descripcion ,"
SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS CantidadEntrante ,"
SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS CantidadSaliente ,"
SQL = SQL & " SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ENTRADA"), gUtilSQL.fNumToStrSQL(0), "ENTRADA", False) & " ) -  SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("SALIDAD"), gUtilSQL.fNumToStrSQL(0), "SALIDAD", False) & " )   AS ExistenciaAnterior, "
SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS Existencia "

SQL = SQL & " FROM ArticuloInventario "
SQL = SQL & " INNER JOIN " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & " ON "
SQL = SQL & " ArticuloInventario.Codigo = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Codigo  AND "
SQL = SQL & " ArticuloInventario.ConsecutivoCompania = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania "
SQL = SQL & sqlWhere
SQL = SQL & " GROUP BY "
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania ,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion"
Set insArticuloView = Nothing
h_EXIT: On Error GoTo 0
  fSqlExistenciaEntreFechaExitenciaAnterior = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlExistenciaEntreFechaExitenciaAnte", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlExistenciaEntreFechaExitenciaActual(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFecha As Date, ByVal valWhere As String) As String
Dim insArticuloView As clsArticuloInvVista
Dim SQL As String
Dim sqlWhere As String
On Error GoTo h_ERROR
Set insArticuloView = New clsArticuloInvVista

sqlWhere = gUtilSQL.fSQLValueDateWithComparisonRule(valWhere, insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Fecha", valFecha, "<=")
sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania", valConsecutivoCompania)
sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)

SQL = SQL & " SELECT "
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania AS ConsecutivoCompania ,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo AS Codigo ,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion    AS Descripcion ,"
SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS CantidadEntrante ,"
SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS CantidadSaliente ,"
SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS ExistenciaAnterior, "
SQL = SQL & " SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ENTRADA"), gUtilSQL.fNumToStrSQL(0), "ENTRADA", False) & " ) -  SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("SALIDAD"), gUtilSQL.fNumToStrSQL(0), "SALIDAD", False) & " )   AS   Existencia "

SQL = SQL & " FROM ArticuloInventario "
SQL = SQL & " INNER JOIN " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & " ON "
SQL = SQL & " ArticuloInventario.Codigo = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Codigo  AND "
SQL = SQL & " ArticuloInventario.ConsecutivoCompania = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania "
SQL = SQL & sqlWhere

SQL = SQL & " GROUP BY "
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania ,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion"
Set insArticuloView = Nothing
h_EXIT: On Error GoTo 0
  fSqlExistenciaEntreFechaExitenciaActual = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlExistenciaEntreFechaExitenciaActual", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSqlExistenciaEntreFechaEntradasSalidas(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valWhere As String) As String
Dim insArticuloView As clsArticuloInvVista
Dim SQL As String
Dim sqlWhere As String
On Error GoTo h_ERROR
Set insArticuloView = New clsArticuloInvVista

sqlWhere = gUtilSQL.fSQLValueDatesBetween(valWhere, "Fecha", valFechaInicial, valFechaFinal)
sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania", valConsecutivoCompania)
sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)

SQL = SQL & " SELECT "
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania AS ConsecutivoCompania ,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo AS Codigo ,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion    AS Descripcion ,"

SQL = SQL & " SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ENTRADA"), gUtilSQL.fNumToStrSQL(0), "ENTRADA", False) & " ) AS CantidadEntrante, "
SQL = SQL & " SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("SALIDAD"), gUtilSQL.fNumToStrSQL(0), "SALIDAD", False) & " ) AS CantidadSaliente, "

SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS ExistenciaAnterior, "
SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "  AS Existencia "
SQL = SQL & " FROM ArticuloInventario "
SQL = SQL & " INNER JOIN " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & " ON "
SQL = SQL & " ArticuloInventario.Codigo = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Codigo  AND "
SQL = SQL & " ArticuloInventario.ConsecutivoCompania = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania "
SQL = SQL & sqlWhere

SQL = SQL & " GROUP BY "
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania ,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion"
Set insArticuloView = Nothing
h_EXIT: On Error GoTo 0
 fSqlExistenciaEntreFechaEntradasSalidas = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlExistenciaEntreFechaEntradasSalidas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlInformeExistenciaEntreFechas(ByVal valConsecutivoCompania As Long, ByVal valLineaDeProducto As String, ByVal valMarca As String, ByVal valCategoria As String, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim SQL As String
Dim insArticuloView As clsArticuloInvVista
Dim sqlWhere  As String
Set insArticuloView = New clsArticuloInvVista
On Error GoTo h_ERROR
     sqlWhere = " WHERE " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania =" & valConsecutivoCompania
     sqlWhere = sqlWhere & " AND " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Fecha <=" & gUtilSQL.fSimpleSqlValue(valFechaFinal)
    
     If gTexto.DfLen(valLineaDeProducto) > 0 Then
       sqlWhere = gUtilSQL.fSQLValueWithAnd(sqlWhere, "articuloInventario.LineaDeProducto", valLineaDeProducto, False)
     End If
     If gTexto.DfLen(valMarca) > 0 Then
       sqlWhere = gUtilSQL.fSQLValueWithAnd(sqlWhere, "articuloInventario.Marca", valMarca, False)
     End If
      If gTexto.DfLen(valCategoria) > 0 Then
        sqlWhere = gUtilSQL.fSQLValueWithAnd(sqlWhere, "articuloInventario.Categoria", valCategoria, False)
     End If
     If gTexto.DfLen(valCodigoDesde) > 0 And gTexto.DfLen(valCodigoHasta) > 0 Then
        sqlWhere = sqlWhere & " AND " & gUtilSQL.fSQLValueBetween("articuloInventario.Codigo", valCodigoDesde, valCodigoHasta)
     End If
SQL = SQL & " SELECT "
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania AS ConsecutivoCompania ,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo AS Codigo ,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion    AS Descripcion ,"
SQL = SQL & "SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLDateValueBetween(insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Fecha", valFechaInicial, valFechaFinal) & " AND " & gUtilSQL.DfSQLIsNull(" NOT ENTRADA"), "ENTRADA", gUtilSQL.fNumToStrSQL(0), False) & " ) AS CantidadEntrante,"
SQL = SQL & "SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLDateValueBetween(insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Fecha", valFechaInicial, valFechaFinal) & " AND " & gUtilSQL.DfSQLIsNull(" NOT SALIDAD"), "SALIDAD", gUtilSQL.fNumToStrSQL(0), False) & " ) AS CantidadSaliente,"
SQL = SQL & " SUM( " & gUtilSQL.getIIF(insArticuloView.GetViewNameArticuloInvMovimientoDetallado & ".Fecha < " & gUtilSQL.fSimpleSqlValue(valFechaInicial) & " AND " & gUtilSQL.DfSQLIsNull("NOT ENTRADA"), "ENTRADA", gUtilSQL.fNumToStrSQL(0), False) & " ) -  SUM( " & gUtilSQL.getIIF(insArticuloView.GetViewNameArticuloInvMovimientoDetallado & ".Fecha < " & gUtilSQL.fSimpleSqlValue(valFechaInicial) & " AND " & gUtilSQL.DfSQLIsNull("NOT SALIDAD"), "SALIDAD", gUtilSQL.fNumToStrSQL(0), False) & " )   AS ExistenciaAnterior, "
SQL = SQL & " SUM( " & gUtilSQL.getIIF(insArticuloView.GetViewNameArticuloInvMovimientoDetallado & ".Fecha <= " & gUtilSQL.fSimpleSqlValue(valFechaFinal) & " AND " & gUtilSQL.DfSQLIsNull("NOT ENTRADA"), "ENTRADA", gUtilSQL.fNumToStrSQL(0), False) & " ) -  SUM( " & gUtilSQL.getIIF(insArticuloView.GetViewNameArticuloInvMovimientoDetallado & ".Fecha <= " & gUtilSQL.fSimpleSqlValue(valFechaFinal) & " AND " & gUtilSQL.DfSQLIsNull("NOT SALIDAD"), "SALIDAD", gUtilSQL.fNumToStrSQL(0), False) & " )   AS Existencia "
SQL = SQL & " FROM ArticuloInventario "
SQL = SQL & " INNER JOIN " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & " ON "
SQL = SQL & " ArticuloInventario.Codigo = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Codigo  AND "
SQL = SQL & " ArticuloInventario.ConsecutivoCompania = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania "
SQL = SQL & sqlWhere
SQL = SQL & " GROUP BY "
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania ,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo,"
SQL = SQL & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion "
SQL = SQL & "ORDER BY CODIGO"
'SQL = fSqlExistenciaEntreFechaExitenciaAnterior(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaInicial, sqlWhere)
'SQL = SQL & " UNION "
'SQL = SQL & fSqlExistenciaEntreFechaEntradasSalidas(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaInicial, valFechaFinal, sqlWhere)
'SQL = SQL & " UNION "
'SQL = SQL & fSqlExistenciaEntreFechaExitenciaActual(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaFinal, sqlWhere)
h_EXIT: On Error GoTo 0
 fSqlInformeExistenciaEntreFechas = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlInformeExistenciaEntreFechas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchArticuloInventarioFactura(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String, ByVal valCodigoAlmacen As String, ByVal valAlicuotas As String, ByVal valPrecio1 As String, ByVal valPrecio2 As String, ByVal strArtResumen As String, ByVal valMonedaExtranjera As Boolean, ByVal vGroupby As String, ByVal valUsaArticulosReservadosPorAlmacen As Boolean, ByVal vaUsaArticulosReservadosPorCotizacion As Boolean, ByVal valBuscarPorSerial As Boolean, ByVal valSerial As String, ByVal valDescripcion As String, ByVal valSqlArtIGTF As String) As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim SQL As String
   Dim sqlWhere  As String
   Dim articulosCom As String
   Dim articulosServicio As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   sqlWhere = ""
   If gTexto.DfLen(valAlicuotas) > 0 Then
     sqlWhere = " AlicuotaIva IN( " & valAlicuotas & ") "
   End If
   If gTexto.DfLen(strArtResumen) > 0 Then
      If gTexto.DfLen(sqlWhere) > 0 Then
         sqlWhere = sqlWhere & " AND "
      End If
      sqlWhere = sqlWhere & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo NOT  IN( " & strArtResumen & ")"
   End If
   If gTexto.DfLen(valSqlArtIGTF) > 0 Then
      If gTexto.DfLen(sqlWhere) > 0 Then
         sqlWhere = sqlWhere & " AND "
      End If
      sqlWhere = sqlWhere & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo NOT  IN( " & valSqlArtIGTF & ")"
   End If
    SQL = fSQLSearchArticuloInventarioFacturaSelect(valConsecutivoCompania, valCodigoArticulo, valCodigoAlmacen, valPrecio1, valPrecio2, sqlWhere, valMonedaExtranjera, vGroupby, valBuscarPorSerial, valSerial, valUsaArticulosReservadosPorAlmacen, vaUsaArticulosReservadosPorCotizacion, valDescripcion)
    SQL = SQL & " UNION "
    SQL = SQL & fSQLSearchArticuloInventarioFacturaSelectCom(valConsecutivoCompania, valCodigoArticulo, valPrecio1, valPrecio2, sqlWhere, valMonedaExtranjera, vGroupby, valCodigoAlmacen, valBuscarPorSerial, valSerial, valUsaArticulosReservadosPorAlmacen, vaUsaArticulosReservadosPorCotizacion, valDescripcion)
    articulosCom = fSQLSearchArticuloInventarioFacturaSelectSoloCompuestos(valConsecutivoCompania, valCodigoArticulo, valPrecio1, valPrecio2, sqlWhere, valMonedaExtranjera, vGroupby, valBuscarPorSerial, valSerial, valDescripcion)
  If (articulosCom <> "") Then
     SQL = SQL & " UNION "
     SQL = SQL & articulosCom
  End If
  
   articulosServicio = fSQLSearchArticuloInventarioFacturaSelectSoloServicios(valConsecutivoCompania, valCodigoArticulo, valPrecio1, valPrecio2, sqlWhere, valMonedaExtranjera, vGroupby, valBuscarPorSerial, valSerial, valDescripcion, strArtResumen, valSqlArtIGTF)
  If (articulosServicio <> "") Then
     SQL = SQL & " UNION "
      SQL = SQL & fSQLSearchArticuloInventarioFacturaSelectSoloServicios(valConsecutivoCompania, valCodigoArticulo, valPrecio1, valPrecio2, sqlWhere, valMonedaExtranjera, vGroupby, valBuscarPorSerial, valSerial, valDescripcion, strArtResumen, valSqlArtIGTF)
  End If
Set insArticuloInvVista = Nothing
h_EXIT:    On Error GoTo 0
   fSQLSearchArticuloInventarioFactura = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchArticuloInventarioFactura", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchArticuloInventarioFacturaSelect(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String, ByVal valCodigoAlmacen As String, ByVal valPrecio1 As String, ByVal valPrecio2 As String, ByVal valsqlWhere As String, ByVal valMonedaExtranjera As Boolean, ByVal vGroupby As String, ByVal valBuscarPorSerial As Boolean, ByVal valSerial As String, ByVal valUsaArticulosReservadosPorAlmacen As Boolean, ByVal vaUsaArticulosReservadosPorCotizacion As Boolean, Optional ByVal valDescripcion As String) As String
Dim insArticuloInvVista As clsArticuloInvVista
Dim vSQL As String
Dim vSQLWhere As String
Dim vDescripcionTallaColor As String
Dim vSQLCondicionExistencia As String
Dim vSQLCondicionTipoArticulo As String
On Error GoTo h_ERROR
Set insArticuloInvVista = New clsArticuloInvVista
 If gTexto.DfLen(valsqlWhere) > 0 Then
   vSQLCondicionExistencia = valsqlWhere & " AND "
 End If
 vDescripcionTallaColor = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionColor"), gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionColor", True)
 vDescripcionTallaColor = vDescripcionTallaColor & " + " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionTalla"), gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionTalla", True)
 vSQLCondicionExistencia = vSQLCondicionExistencia & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen > " & gUtilSQL.fNumToStrSQL(0)
 If gTexto.DfLen(valCodigoArticulo) > 0 And valCodigoArticulo <> "*" Then
     vSQLWhere = gUtilSQL.fSQLValueWithAnd(vSQLWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoDelArticulo", valCodigoArticulo, False)
 End If
 vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
 If valBuscarPorSerial Then
   If valSerial <> "" Then
      If valSerial = "%%" Then
         vSQLWhere = vSQLWhere & " And " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".Serial != " & gUtilSQL.fSimpleSqlValue("0")
      Else
         vSQLWhere = vSQLWhere & "And " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".Serial Like " & gUtilSQL.fSimpleSqlValue(valSerial)
      End If
   End If
 End If
 vSQLCondicionTipoArticulo = " AND ( " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".TipoDeArticulo <> " & enum_TipoDeArticulo.eTD_SERVICIO & ")"
 vSQLWhere = vSQLWhere & vSQLCondicionTipoArticulo
 If valDescripcion <> "" Then
    vSQLWhere = vSQLWhere & "And " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion LIKE " & gUtilSQL.fSimpleSqlValue(valDescripcion)
 End If
 vSQLWhere = gUtilSQL.fSQLValueWithAnd(vSQLWhere, insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoAlmacen", valCodigoAlmacen, False)
 vSQLWhere = vSQLWhere & "AND " & gUtilSQL.fSQLNumberValueWithAnd(valsqlWhere, insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ConsecutivoCompania", valConsecutivoCompania)
 vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
 vSQL = ""
 vSQL = vSQL & " SELECT "
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo AS CodigoCompuesto  ,"
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion + " & vDescripcionTallaColor & " AS Descripcion , "
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto,"
 If vaUsaArticulosReservadosPorCotizacion And valUsaArticulosReservadosPorAlmacen Then
   vSQL = vSQL & " ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0)" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados,0)" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados,0) AS Disponibilidad,"
 ElseIf valUsaArticulosReservadosPorAlmacen Then
   vSQL = vSQL & " ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen ,0)" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados,0) AS Disponibilidad,"
 ElseIf vaUsaArticulosReservadosPorCotizacion Then
   vSQL = vSQL & " ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0)" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados,0) AS Disponibilidad,"
 Else
   vSQL = vSQL & " ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0) AS Disponibilidad,"
 End If
 vSQL = vSQL & valPrecio1 & " ,"
 vSQL = vSQL & valPrecio2 & " ,"
 vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Serial,"
 vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Rollo,"
 vSQL = vSQL & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CantidadRecibida AS CantidadUltimaCompra,"
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".AlicuotaIva,"
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoGrupo,"
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".TipoArticuloInv,"
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
 vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ConsecutivoCompania,"
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".PorcentajeBaseImponible,"
 vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoAlmacen,"
 vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen"
 vSQL = vSQL & " FROM " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1()
vSQL = vSQL & " INNER JOIN  "
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " ON "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Serial = " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Rollo = " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo"
If valUsaArticulosReservadosPorAlmacen Then
   vSQL = vSQL & " LEFT JOIN  "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen & " ON "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".ConsecutivoCompania AND "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoArticulo AND "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CodigoAlmacen = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoAlmacen"
End If
If vaUsaArticulosReservadosPorCotizacion Then
   vSQL = vSQL & " LEFT JOIN  "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & " ON "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".ConsecutivoCompania AND "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoArticulo "
End If
vSQL = vSQL & " LEFT JOIN  "
vSQL = vSQL & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & " ON "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".ConsecutivoCompania AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoAlmacen = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CodigoAlmacen AND  "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CodigoArticulo AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Serial = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".Serial AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Rollo = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".Rollo "

vSQL = vSQL & " LEFT JOIN  "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & " ON "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".ConsecutivoCompania AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".CodigoArticulo AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".Serial AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".Rollo AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoTalla = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".CodigoTalla AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoColor = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".CodigoColor "

vSQL = vSQL & " LEFT JOIN  "
vSQL = vSQL & " CamposMonedaExtranjera ON "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = CamposMonedaExtranjera.ConsecutivoCompania AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo = CamposMonedaExtranjera.Codigo "

vSQL = vSQL & vSQLWhere
vSQL = vSQL & " Group By "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo  ,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Existencia, "
If (valMonedaExtranjera) Then
vSQL = vSQL & vGroupby & ","
Else
vSQL = vSQL & valPrecio1 & ","
vSQL = vSQL & valPrecio2 & ","
End If
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CantArtReservado ,"
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Serial,"
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Rollo,"
vSQL = vSQL & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CantidadRecibida ,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".AlicuotaIva,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoGrupo,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".TipoArticuloInv,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ConsecutivoCompania,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".PorcentajeBaseImponible,"
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoAlmacen,"
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,"
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionTalla, "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionColor "
If valUsaArticulosReservadosPorAlmacen And vaUsaArticulosReservadosPorCotizacion Then
   vSQL = vSQL & "," & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados,"
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados"
ElseIf valUsaArticulosReservadosPorAlmacen Then
   vSQL = vSQL & "," & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados "
ElseIf vaUsaArticulosReservadosPorCotizacion Then
   vSQL = vSQL & "," & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados"
End If
   Set insArticuloInvVista = Nothing
h_EXIT:    On Error GoTo 0
   fSQLSearchArticuloInventarioFacturaSelect = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchArticuloInventarioFacturaSelect", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchArticuloInventarioFacturaSelectCom(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String, ByVal valPrecio1 As String, ByVal valPrecio2 As String, ByVal valsqlWhere As String, ByVal valMonedaExtranjera As Boolean, ByVal vGroupby As String, ByVal valCodigoAlmacen As String, ByVal valBuscarPorSerial As Boolean, ByVal valSerial As String, ByVal valUsaArticulosReservadosPorAlmacen As Boolean, ByVal vaUsaArticulosReservadosPorCotizacion As Boolean, Optional ByVal valDescripcion As String) As String
Dim insArticuloInvVista As clsArticuloInvVista
Dim vSQL As String
Dim vSQLWhere As String
Dim vSQLCondicionTipoArticulo As String
On Error GoTo h_ERROR
Set insArticuloInvVista = New clsArticuloInvVista
 vSQLCondicionTipoArticulo = gUtilSQL.fSQLEnumValueWithAnd("", insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".TipoDeArticulo", enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO)
 vSQLCondicionTipoArticulo = "(" & vSQLCondicionTipoArticulo & ")"
  If gTexto.DfLen(valsqlWhere) > 0 Then
      vSQLCondicionTipoArticulo = valsqlWhere & " AND " & vSQLCondicionTipoArticulo
 End If
 If gTexto.DfLen(valCodigoArticulo) > 0 And valCodigoArticulo <> "*" Then
        vSQLWhere = gUtilSQL.fSQLValueWithAnd(vSQLWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoDelArticulo", valCodigoArticulo, False)
 End If
 vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
 vSQLWhere = vSQLWhere & " And " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
If valBuscarPorSerial Then
   If valSerial <> "" Then
      If valSerial = "%%" Then
         vSQLWhere = vSQLWhere & " And " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".Serial != " & gUtilSQL.fSimpleSqlValue("0")
      Else
         vSQLWhere = vSQLWhere & "And " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".Serial Like " & gUtilSQL.fSimpleSqlValue(valSerial)
      End If
   End If
 End If
 If valDescripcion <> "" Then
    vSQLWhere = vSQLWhere & "And " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion LIKE " & gUtilSQL.fSimpleSqlValue(valDescripcion)
 End If
vSQLWhere = vSQLWhere & " AND " & gUtilSQL.fSQLNumberValueWithAnd(vSQLCondicionTipoArticulo, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
vSQL = " SELECT "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoDelArticulo AS CodigoCompuesto, "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".Descripcion,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".LineaDeProducto, "
If vaUsaArticulosReservadosPorCotizacion And valUsaArticulosReservadosPorAlmacen Then
   vSQL = vSQL & "ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0 )" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados,0)" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados,0) AS Disponibilidad,"
 ElseIf valUsaArticulosReservadosPorAlmacen Then
   vSQL = vSQL & "ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0 )" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados,0) AS Disponibilidad,"
 ElseIf vaUsaArticulosReservadosPorCotizacion Then
   vSQL = vSQL & "ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0 )" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados,0) AS Disponibilidad,"
 Else
   vSQL = vSQL & "ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0) AS Disponibilidad,"
 End If
vSQL = vSQL & valPrecio1 & " ,"
vSQL = vSQL & valPrecio2 & " ,"
vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0") & " AS Serial,"
vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0") & " AS Rollo,"
vSQL = vSQL & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CantidadRecibida AS CantidadUltimaCompra,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".AlicuotaIva,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoGrupo,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".TipoArticuloInv,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".Codigo,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".ConsecutivoCompania,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".PorcentajeBaseImponible,"
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoAlmacen,"
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen"
vSQL = vSQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1
vSQL = vSQL & " LEFT JOIN   CamposMonedaExtranjera ON "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".ConsecutivoCompania = CamposMonedaExtranjera.ConsecutivoCompania AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".Codigo = CamposMonedaExtranjera.Codigo  "
vSQL = vSQL & " LEFT JOIN  " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & " ON "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoArticulo = "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoDelArticulo AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".ConsecutivoCompania = "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".ConsecutivoCompania "
vSQL = vSQL & " LEFT JOIN  "
vSQL = vSQL & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & " ON "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".ConsecutivoCompania AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoAlmacen = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CodigoAlmacen AND  "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CodigoArticulo AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Serial = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".Serial AND "
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Rollo = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".Rollo "
If valUsaArticulosReservadosPorAlmacen Then
   vSQL = vSQL & " LEFT JOIN  "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen & " ON "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".ConsecutivoCompania AND "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoArticulo AND "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CodigoAlmacen = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoAlmacen"
End If
If vaUsaArticulosReservadosPorCotizacion Then
   vSQL = vSQL & " LEFT JOIN  "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & " ON "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".ConsecutivoCompania AND "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoDelArticulo "
End If
vSQL = vSQL & vSQLWhere
vSQL = vSQL & " Group By "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoDelArticulo , "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".Descripcion,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".LineaDeProducto,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".Existencia, "
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CantArtReservado,"
If (valMonedaExtranjera) Then
vSQL = vSQL & vGroupby & ","
Else
vSQL = vSQL & valPrecio1 & ","
vSQL = vSQL & valPrecio2 & ","
End If
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".AlicuotaIva,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoGrupo,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".TipoArticuloInv,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".Codigo,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".ConsecutivoCompania,"
vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".PorcentajeBaseImponible,"
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoAlmacen,"
vSQL = vSQL & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales & ".CantidadRecibida,"
vSQL = vSQL & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".ExistenciaAlmacen "
If valUsaArticulosReservadosPorAlmacen And vaUsaArticulosReservadosPorCotizacion Then
   vSQL = vSQL & "," & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados,"
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados "
ElseIf valUsaArticulosReservadosPorAlmacen Then
   vSQL = vSQL & "," & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados "
ElseIf vaUsaArticulosReservadosPorCotizacion Then
   vSQL = vSQL & "," & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados "
End If
   Set insArticuloInvVista = Nothing
h_EXIT:    On Error GoTo 0
 fSQLSearchArticuloInventarioFacturaSelectCom = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchArticuloInventarioFacturaSelectCom", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLSearchArticuloInventarioFacturaSelectSoloCompuestos(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String, ByVal valPrecio1 As String, ByVal valPrecio2 As String, ByVal valsqlWhere As String, ByVal valMonedaExtranjera As Boolean, ByVal vGroupby As String, ByVal valBuscarPorSerial As Boolean, ByVal valSerial As String, Optional ByVal valDescripcion As String) As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vSQL As String
   Dim vSQLWhere As String
   Dim vSQLCondicionTipoArticulo As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
 vSQLCondicionTipoArticulo = "ArticuloInventario.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLCondicionTipoArticulo, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   If gTexto.DfLen(valCodigoArticulo) > 0 And valCodigoArticulo <> "*" Then
      vSQLWhere = gUtilSQL.fSQLValueWithAnd(vSQLWhere, "ArticuloInventario.Codigo", valCodigoArticulo, False)
   End If
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   If valBuscarPorSerial Then
      If valSerial <> "" Then
         fSQLSearchArticuloInventarioFacturaSelectSoloCompuestos = ""
         Exit Function
      End If
   End If
   If valDescripcion <> "" Then
      vSQLWhere = vSQLWhere & "And ArticuloInventario.Descripcion LIKE " & gUtilSQL.fSimpleSqlValue(valDescripcion)
   End If
   vSQLWhere = vSQLWhere & " AND articuloInventario.Codigo NOT IN (" & _
               gUtilSQL.fSimpleSqlValue("RD_AliGeneral @") & ", " & _
               gUtilSQL.fSimpleSqlValue("RD_AliGeneralNC @") & ", " & _
               gUtilSQL.fSimpleSqlValue("RD_AliReducida@") & ", " & _
               gUtilSQL.fSimpleSqlValue("RD_AliReducidaNC @") & ", " & _
               gUtilSQL.fSimpleSqlValue("RD_AliExentaNC @") & ", " & _
               gUtilSQL.fSimpleSqlValue("RD_AliExtendidaNC @") & ", " & _
               gUtilSQL.fSimpleSqlValue(getCodigoArticuloResumenDiario(eTD_EXENTO)) & ", " & _
               gUtilSQL.fSimpleSqlValue(getCodigoArticuloResumenDiario(eTD_ALICUOTAGENERAL)) & ", " & _
               gUtilSQL.fSimpleSqlValue(getCodigoArticuloResumenDiario(eTD_ALICUOTA2)) & ", " & _
               gUtilSQL.fSimpleSqlValue(getCodigoArticuloResumenDiario(eTD_ALICUOTA3)) & ", " & _
               gUtilSQL.fSimpleSqlValue(getCodigoComisionXPorcentajeDeAlmacenaje) & ")"
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   vSQL = " SELECT "
   vSQL = vSQL & " ArticuloInventario.codigo AS CodigoCompuesto, "
   vSQL = vSQL & " ArticuloInventario.descripcion,"
   vSQL = vSQL & " ArticuloInventario.LineaDeProducto, "
   vSQL = vSQL & " ArticuloInventario.Existencia AS Disponibilidad,"
   vSQL = vSQL & valPrecio1 & " ,"
   vSQL = vSQL & valPrecio2 & " ,"
   vSQL = vSQL & " '0' AS Serial,"
   vSQL = vSQL & " '0' AS Rollo,"
   vSQL = vSQL & " '0' AS CantidadUltimaCompra,"
   vSQL = vSQL & " ArticuloInventario.AlicuotaIva,"
   vSQL = vSQL & " '0' AS CodigoGrupo,"
   vSQL = vSQL & " ArticuloInventario.TipoArticuloInv,"
   vSQL = vSQL & " ISNULL(ArticuloInventario.CodigoGrupo,''),"
   vSQL = vSQL & " ArticuloInventario.ConsecutivoCompania,"
   vSQL = vSQL & " ArticuloInventario.PorcentajeBaseImponible,"
   vSQL = vSQL & " '0' AS CodigoAlmacen,"
   vSQL = vSQL & " ArticuloInventario.Existencia"
   vSQL = vSQL & " FROM  ArticuloInventario"
   vSQL = vSQL & " LEFT JOIN   CamposMonedaExtranjera ON "
   vSQL = vSQL & "ArticuloInventario.ConsecutivoCompania = CamposMonedaExtranjera.ConsecutivoCompania AND "
   vSQL = vSQL & "ArticuloInventario.Codigo = CamposMonedaExtranjera.Codigo  "
   vSQL = vSQL & vSQLWhere
   Set insArticuloInvVista = Nothing
h_EXIT:    On Error GoTo 0
 fSQLSearchArticuloInventarioFacturaSelectSoloCompuestos = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchArticuloInventarioFacturaSelectSoloCompuestos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelInformeArticulosXAlmacen(ByVal valCodigoAlmacen As String, ByVal valCantidadAImprimirAlmacen As String, ByVal valConsecutivoCompania As Long)
   Dim SQL As String
   Dim insArticuloView As clsArticuloInvVista
   Dim gEnumReportLocal As clsEnumReport
   On Error GoTo h_ERROR
   Set insArticuloView = New clsArticuloInvVista
   Set gEnumReportLocal = New clsEnumReport
   SQL = "SELECT "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".CodigoAlmacen, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".NombreAlmacen, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".CodigoArticulo, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".Descripcion, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".precioConIva, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".precioSinIva, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".Existencia, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".TipoArticuloInv, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".DescTipoArticuloInv, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".ConsecutivoCompania, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".NombreCompania "
   SQL = SQL & " FROM "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1
   SQL = SQL & " WHERE "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".ConsecutivoCompania = " & valConsecutivoCompania
   If valCantidadAImprimirAlmacen = gEnumReportLocal.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND " & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".CodigoAlmacen " & _
                  " = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoAlmacen))
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".ConsecutivoCompania, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".CodigoAlmacen, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".TipoArticuloInv, "
   SQL = SQL & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".CodigoArticulo"
   Set insArticuloView = Nothing
   Set gEnumReportLocal = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLDelInformeArticulosXAlmacen = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSQLDelInformeArticulosXAlmacen", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlUpdateCostoPromedio(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String, ByVal valCostoPromedio As Currency) As String
Dim vWhere As String
 Dim vSQL As String
On Error GoTo h_ERROR
h_EXIT: On Error GoTo 0
   vSQL = ""
   vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, "Codigo", valCodigoArticulo, False)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " UPDATE    ArticuloInventario SET  "
   vSQL = vSQL & " CostoPromedio =  " & gUtilSQL.fNumToStrSQL(valCostoPromedio) & " "
   vSQL = vSQL & vWhere
   fSqlUpdateCostoPromedio = vSQL
  Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlUpdateCostoPromedio", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLCargaInicialCierreExistencia(ByVal valConsecutivoCompania As Long) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = "EscargaInicial = " & gUtilSQL.fBooleanToSqlValue(True)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " SELECT Max(Fecha) AS FECHA "
   vSQL = vSQL & " FROM  CierreCostoDelPeriodo "
   vSQL = vSQL & vWhere
   vSQL = vSQL & " Group by FECHA "
  fSQLCargaInicialCierreExistencia = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLCargaInicialCierreExistencia", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCargaCierreAutomatico(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date) As String
   Dim vSQL As String
   Dim sqlUltimaCompra As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSQL = ""
   valFechaInicial = valFechaInicial - 1
   sqlUltimaCompra = "dbo.Gf_CompraUnaFecha( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania, " & gUtilSQL.fDateToSQLValue(valFechaInicial) & "," & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo)"
   vWhere = gUtilSQL.fSQLEnumValueWithAnd("", "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLValueDateWithComparisonRule(vWhere, insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Fecha", valFechaInicial, "<=")
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " INSERT INTO CierreCostoDelPeriodo (ConsecutivoCompania,Codigo,Fecha,Existencia,Costo,EsCargaInicial)"
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticuloInvMovimiento & ".ConsecutivoCompania, "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo, "
   vSQL = vSQL & gUtilSQL.fDateToSQLValue(valFechaInicial) & ", "
   vSQL = vSQL & " SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Entrada ) -   SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Salidad )  AS  Existencia, "
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(sqlUltimaCompra), gUtilSQL.fNumToStrSQL(0), sqlUltimaCompra, True) & ","
   vSQL = vSQL & gUtilSQL.fBooleanToSqlValue(True)
   vSQL = vSQL & " FROM ArticuloInventario INNER JOIN "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & " ON "
   vSQL = vSQL & " ArticuloInventario.ConsecutivoCompania  =  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania AND "
   vSQL = vSQL & " ArticuloInventario.Codigo =  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo"
   vSQL = vSQL & vWhere
   vSQL = vSQL & " Group By "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania, "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo, "
   vSQL = vSQL & " HAVING   (  SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Entrada ) -   SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Salidad ) > " & gUtilSQL.fNumToStrSQL(0) & " ) "
  fSQLCargaCierreAutomatico = vSQL
Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLCargaInicialCierreExistencia", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLUpdateArticuloInventarioFechaCierre(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valMarcaRecalcular As Boolean) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = gUtilSQL.fSQLEnumValueWithAnd("", "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   valFechaInicial = valFechaInicial - 1
   vSQL = " Update ArticuloInventario "
   vSQL = vSQL & " SET FechaCierreActualizada  = " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ","
   vSQL = vSQL & " RecalcularCierre = " & gUtilSQL.fBooleanToSqlValue(valMarcaRecalcular) & ", "
   vSQL = vSQL & " RecalcularCosto  = " & gUtilSQL.fBooleanToSqlValue(valMarcaRecalcular)
   vSQL = vSQL & vWhere
  fSQLUpdateArticuloInventarioFechaCierre = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUpdateArticuloInventarioFechaCierre", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLUpdateCargaInicialCierreExistencia(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   valFechaInicial = valFechaInicial - 1
   vWhere = "EscargaInicial = " & gUtilSQL.fBooleanToSqlValue(True)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " Update CierreCostoDelPeriodo "
   vSQL = vSQL & " SET Fecha  = " & gUtilSQL.fDateToSQLValue(valFechaInicial)
   vSQL = vSQL & vWhere
   fSQLUpdateCargaInicialCierreExistencia = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUpdateCargaInicialCierreExistencia", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLDELETECargaInicialCierreExistencia(ByVal valConsecutivoCompania As Long) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = " EscargaInicial = " & gUtilSQL.fBooleanToSqlValue(True)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " DELETE CierreCostoDelPeriodo "
   vSQL = vSQL & vWhere
   fSQLDELETECargaInicialCierreExistencia = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDELETECargaInicialCierreExistencia", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLFechaCierresCostoPromedio(ByVal valConsecutivoCompania As Long, ByVal valFecha As Date) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = " EscargaInicial = " & gUtilSQL.fBooleanToSqlValue(False)
   vWhere = gUtilSQL.fSQLValueDateWithComparisonRule(vWhere, "CierreCostoDelPeriodo.Fecha", valFecha, ">=")
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " SELECT Max(Fecha ) As Fecha  FROM CierreCostoDelPeriodo "
   vSQL = vSQL & vWhere
   vSQL = vSQL & "  GROUP BY   ConsecutivoCompania "
   fSQLFechaCierresCostoPromedio = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLFechaCierresCostoPromedio", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLUpdateArticuloInventarioFechaCierreOperacion(ByVal valConsecutivoCompania As Long, ByVal valFechaCierre As Date, ByVal valFechaOperacion As Date, ByVal valDocumento As String, ByVal valOperacion As String) As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vWhere = gUtilSQL.fSQLDateValue(insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Fecha", valFechaOperacion)
   vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".NumeroDocumento", valDocumento, False)
   vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".TipoDeDocumento", valOperacion, False)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "articuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "articuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "articuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " UPDATE articuloInventario "
   vSQL = vSQL & " SET   RecalcularCierre =  " & gUtilSQL.fBooleanToSqlValue(True) & " , "
   vSQL = vSQL & "   RecalcularCosto =   " & gUtilSQL.fBooleanToSqlValue(True) & " , "
   vSQL = vSQL & "   FechaCierreActualizada = " & gUtilSQL.fDateToSQLValue(valFechaCierre)
   vSQL = vSQL & "   FROM   articuloInventario  "
   vSQL = vSQL & "   INNER JOIN  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & " ON "
   vSQL = vSQL & "   ArticuloInventario.ConsecutivoCompania = " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania AND "
   vSQL = vSQL & "   ArticuloInventario.Codigo = " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo "
   vSQL = vSQL & vWhere
   Set insArticuloInvVista = Nothing
   fSQLUpdateArticuloInventarioFechaCierreOperacion = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUpdateCostoPromedioPonderado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLUpdateArticuloInventarioRecalulaCostoOperacion(ByVal valConsecutivoCompania As Long, ByVal valFechaOperacion As Date, ByVal valDocumento As String, ByVal valOperacion As String) As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vWhere = gUtilSQL.fSQLDateValue(insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Fecha", valFechaOperacion)
   vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".NumeroDocumento", valDocumento, False)
   vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".TipoDeDocumento", valOperacion, False)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "articuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "articuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "articuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " UPDATE articuloInventario "
   vSQL = vSQL & " SET   RecalcularCierre =  " & gUtilSQL.fBooleanToSqlValue(True) & " , "
   vSQL = vSQL & "   RecalcularCosto =   " & gUtilSQL.fBooleanToSqlValue(True) & "  "
   vSQL = vSQL & "   FROM   articuloInventario  "
   vSQL = vSQL & "   INNER JOIN  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & " ON "
   vSQL = vSQL & "   ArticuloInventario.ConsecutivoCompania = " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania AND "
   vSQL = vSQL & "   ArticuloInventario.Codigo = " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo "
   vSQL = vSQL & vWhere
   Set insArticuloInvVista = Nothing
   fSQLUpdateArticuloInventarioRecalulaCostoOperacion = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUpdateCostoPromedioPonderado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLUpdateCostoPromedioPonderado(ByVal valConsecutivoCompania As Long, ByVal valCodigo As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = " A.CostoALaFecha = " & gUtilSQL.fBooleanToSqlValue(True)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "B.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "B.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "B.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " UPDATE articuloInventario "
   vSQL = vSQL & " SET   RecalcularCosto =  " & gUtilSQL.fBooleanToSqlValue(False) & " , "
   vSQL = vSQL & "   CostoPromedio = A.CostoFinal  "
   vSQL = vSQL & "   FROM   articuloInventario AS B "
   vSQL = vSQL & "   INNER JOIN  dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaInicial) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigo) & "," & gUtilSQL.fSimpleSqlValue(valCodigo) & ") AS A ON "
   vSQL = vSQL & "   B.codigo = A.Codigo AND "
   vSQL = vSQL & "   B.consecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   vSQL = vSQL & vWhere
 fSQLUpdateCostoPromedioPonderado = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUpdateCostoPromedioPonderado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLCodigoArticuloDeOperaciones(ByVal valConsecutivoCompania As Long, ByVal valFechaOperacion As Date, ByVal valDocumento As String, ByVal valOperacion As String) As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vWhere = gUtilSQL.fSQLDateValue(insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Fecha", valFechaOperacion)
   vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".NumeroDocumento", valDocumento, False)
   vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".TipoDeDocumento", valOperacion, False)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "articuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "articuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "articuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " Select articuloInventario.codigo "
   vSQL = vSQL & "   FROM   articuloInventario  "
   vSQL = vSQL & "   INNER JOIN  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & " ON "
   vSQL = vSQL & "   ArticuloInventario.ConsecutivoCompania = " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania AND "
   vSQL = vSQL & "   ArticuloInventario.Codigo = " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo "
   vSQL = vSQL & vWhere
    Set insArticuloInvVista = Nothing
 fSQLCodigoArticuloDeOperaciones = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUpdateCostoPromedioPonderadoOperaciones", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLUltimaFechaCierresCostoPromedio(ByVal valConsecutivoCompania As Long) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " SELECT Max(Fecha) As Fecha  FROM CierreCostoDelPeriodo "
   vSQL = vSQL & vWhere
   vSQL = vSQL & "  GROUP BY   ConsecutivoCompania "
   fSQLUltimaFechaCierresCostoPromedio = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUltimaFechaCierresCostoPromedio", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLExistenArticulosConCierresPorRecalculo(ByVal valConsecutivoCompania As Long) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = " RecalcularCierre = " & gUtilSQL.fBooleanToSqlValue(True)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " SELECT CODIGO FROM ArticuloInventario "
   vSQL = vSQL & vWhere
   fSQLExistenArticulosConCierresPorRecalculo = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLExistenArticulosConCierresPorRecalculo", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLFechaCierresCostoPromedioReporte(ByVal valConsecutivoCompania As Long, ByVal valFecha As Date, ByVal valReporte As Boolean) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   If (valReporte) Then
    vWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", "CierreCostoDelPeriodo.Fecha", valFecha, "<=")
   Else
    vWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", "CierreCostoDelPeriodo.Fecha", valFecha, "<")
   End If
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " SELECT Max(Fecha ) As Fecha  FROM CierreCostoDelPeriodo "
   vSQL = vSQL & vWhere
   vSQL = vSQL & "  GROUP BY   ConsecutivoCompania "
   fSQLFechaCierresCostoPromedioReporte = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLFechaCierresCostoPromedio", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPonderadoReporteDetallado(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim SQL As String
Dim sqlWhere As String
Dim SQLCodigo   As String
On Error GoTo h_ERROR
If (gUtilDate.fF1IsLessThanF2(valFechaInicial, valFechaCierre)) Then
valFechaInicial = valFechaCierre + 1
End If
  SQL = SQL & fSqlCostoPromedioPonderadoReporteSelect1(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaCierre, valFechaInicial, valFechaFinal)
  SQL = SQL & " UNION "
  SQL = SQL & fSqlCostoPromedioPonderadoReporteSelect2(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaCierre, valFechaInicial, valFechaFinal)
h_EXIT: On Error GoTo 0
  fSqlCostoPromedioPonderadoReporteDetallado = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPonderadoReporteSelect1(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim SQL As String
Dim sqlWhere As String
Dim SQLCodigo   As String
On Error GoTo h_ERROR

   sqlWhere = gUtilSQL.fSQLValueBetween("fn_CostoPromedioPonderado_1.codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   sqlWhere = sqlWhere & " AND  fn_CostoPromedioPonderado_1.CostoAlaFecha = " & gUtilSQL.fBooleanToSqlValue(True)
   
   sqlWhere = gUtilSQL.fSQLValueDateWithComparisonRule(sqlWhere, "fn_CostoPromedioPonderado_1.Fecha", valFechaInicial, "<")
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   SQL = SQL & "  SELECT "
   SQL = SQL & " fn_CostoPromedioPonderado_1.id_num, "
   SQL = SQL & " fn_CostoPromedioPonderado_1.codigo AS codigo, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Saldo") & " AS descripcion ,"
   SQL = SQL & gUtilSQL.fDateToSQLValue(valFechaInicial) & "  AS fecha,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.tipodedocumento AS tipodedocumento, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS ExistenciaInicial,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS CostoInicial,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS  MontoInicial,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS entrada ,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " As MontoEntrada,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS salidad,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoSalidad,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.existenciaFinal AS existenciaFinal ,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.CostoFinal AS CostoFinal,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.TotalFinal AS MontoFinal "
   SQL = SQL & " FROM  ArticuloInventario INNER JOIN "
   SQL = SQL & "   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS fn_CostoPromedioPonderado_1 ON "
   SQL = SQL & "  ArticuloInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo AND "
   SQL = SQL & " ArticuloInventario.ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   SQL = SQL & sqlWhere
h_EXIT: On Error GoTo 0
  fSqlCostoPromedioPonderadoReporteSelect1 = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPonderadoReporteSelect2(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim SQL As String
Dim sqlWhere As String
Dim SQLCodigo   As String
On Error GoTo h_ERROR
   sqlWhere = gUtilSQL.fSQLValueBetween("fn_CostoPromedioPonderado_1.codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(sqlWhere, "fn_CostoPromedioPonderado_1.Fecha", valFechaInicial, valFechaFinal)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   SQL = SQL & "  SELECT "
   SQL = SQL & " fn_CostoPromedioPonderado_1.id_num, "
   SQL = SQL & " fn_CostoPromedioPonderado_1.codigo AS codigo,"
   SQL = SQL & " ArticuloInventario.descripcion AS descripcion ,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.fecha AS fecha,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.tipodedocumento AS tipodedocumento,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.ExistenciaInicial AS ExistenciaInicial,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.CostoInicial AS CostoInicial,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.TotalInicial AS  MontoInicial,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.Entradas AS entrada ,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.TotalOperacion As MontoEntrada,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.Salidas AS salidad,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.CostoInicial *   fn_CostoPromedioPonderado_1.salidas as MontoSalidad,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.existenciaFinal AS existenciaFinal,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.CostoFinal AS CostoFinal,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.TotalFinal AS MontoFinal"
   SQL = SQL & " FROM  ArticuloInventario INNER JOIN "
   SQL = SQL & "   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS fn_CostoPromedioPonderado_1 ON "
   SQL = SQL & "  ArticuloInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo AND "
   SQL = SQL & " ArticuloInventario.ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
  SQL = SQL & sqlWhere
h_EXIT: On Error GoTo 0
 fSqlCostoPromedioPonderadoReporteSelect2 = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLExistenciayCostoAUnaFecha(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valCodigo As String) As String
   Dim vSQL As String
   Dim sqlUltimaCompra As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSQL = ""
   sqlUltimaCompra = "dbo.Gf_CompraUnaFecha( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania, " & gUtilSQL.fDateToSQLValue(valFechaInicial) & "," & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo)"
   vWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Fecha", valFechaInicial, "<=")
   vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo", valCodigo, False)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & " SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Entrada ) -   SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Salidad )  AS  Existencia, "
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(sqlUltimaCompra), gUtilSQL.fNumToStrSQL(0), sqlUltimaCompra, True) & " AS Costo "
   vSQL = vSQL & " FROM ArticuloInventario INNER JOIN "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & " ON "
   vSQL = vSQL & " ArticuloInventario.ConsecutivoCompania  =  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania AND "
   vSQL = vSQL & " ArticuloInventario.Codigo =  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo"
   vSQL = vSQL & vWhere
   vSQL = vSQL & " Group By "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania, "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo "
   fSQLExistenciayCostoAUnaFecha = vSQL
Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLExistenciayCostoAUnaFecha", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLDataSourceCargaInicial(ByVal valConsecutivoCompania As Long, ByVal valLineaDeProducto As String, ByVal valMarca As String, ByVal valCategoria As String, ByVal valCodigoIncio As String, ByVal valCodigoFin As String, ByVal valEstaModificando As Boolean, ByVal valFechaInicial As Date) As String
   On Error GoTo h_ERROR
   If (valEstaModificando) Then
      fSQLDataSourceCargaInicial = fSQLDataSourceCargaInicialModificar(valConsecutivoCompania, valLineaDeProducto, valMarca, valCategoria, valCodigoIncio, valCodigoFin)
   Else
      fSQLDataSourceCargaInicial = fSQLDataSourceCargaInicialInsertar(valConsecutivoCompania, valLineaDeProducto, valMarca, valCategoria, valCodigoIncio, valCodigoFin, valFechaInicial)
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDataSourceCargaInicial", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLDataSourceCargaInicialModificar(ByVal valConsecutivoCompania As Long, ByVal valLineaDeProducto As String, ByVal valMarca As String, ByVal valCategoria As String, ByVal valCodigoIncio As String, ByVal valCodigoFin As String) As String
   Dim vSQL As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSQL = ""
   vWhere = " EsCargaInicial = " & gUtilSQL.fBooleanToSqlValue(True)
     If gTexto.DfLen(valLineaDeProducto) > 0 Then
       vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, "articuloInventario.LineaDeProducto", valLineaDeProducto, False)
     End If
     If gTexto.DfLen(valMarca) > 0 Then
       vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, "articuloInventario.Marca", valMarca, False)
     End If
      If gTexto.DfLen(valCategoria) > 0 Then
        vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, "articuloInventario.Categoria", valCategoria, False)
     End If
     If gTexto.DfLen(valCodigoIncio) > 0 And gTexto.DfLen(valCodigoFin) > 0 Then
        vWhere = vWhere & " AND " & gUtilSQL.fSQLValueBetween("articuloInventario.Codigo", valCodigoIncio, valCodigoFin)
     End If
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " SELECT  ArticuloInventario.Codigo AS CodigoArticulo,"
   vSQL = vSQL & " CierreCostoDelPeriodo.Existencia AS Cantidad,"
   vSQL = vSQL & " CierreCostoDelPeriodo.Costo  AS CostoUnitario,"
   vSQL = vSQL & " CierreCostoDelPeriodo.Existencia  AS CantidadRecibida,"
   vSQL = vSQL & " CierreCostoDelPeriodo.ConsecutivoCompania AS ConsecutivoCompania,"
   vSQL = vSQL & " CierreCostoDelPeriodo.ConsecutivoCompania AS NumeroSecuencialCompra,"
   vSQL = vSQL & " CierreCostoDelPeriodo.ConsecutivoCompania  As ConsecutivoRenglon"
   vSQL = vSQL & " FROM ArticuloInventario INNER JOIN "
   vSQL = vSQL & " CierreCostoDelPeriodo ON  "
   vSQL = vSQL & " articuloInventario.ConsecutivoCompania = CierreCostoDelPeriodo.ConsecutivoCompania AND "
   vSQL = vSQL & " articuloInventario.Codigo = CierreCostoDelPeriodo.Codigo "
   vSQL = vSQL & vWhere
  fSQLDataSourceCargaInicialModificar = vSQL
Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDataSourceCargaInicial", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLDataSourceCargaInicialInsertar(ByVal valConsecutivoCompania As Long, ByVal valLineaDeProducto As String, ByVal valMarca As String, ByVal valCategoria As String, ByVal valCodigoIncio As String, ByVal valCodigoFin As String, ByVal valFechaInicial As Date) As String
   Dim vSQL As String
   Dim vCondicion As String
   Dim sqlUltimaCompra As String
   Dim vsqlFechaUltimaCompra As String
   
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSQL = ""
   valFechaInicial = valFechaInicial - 1
   sqlUltimaCompra = "dbo.Gf_CompraUnaFecha(  ArticuloInventario.ConsecutivoCompania, " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ",ArticuloInventario.Codigo)"
   vsqlFechaUltimaCompra = "dbo.Gf_CompraUltimaFecha(  ArticuloInventario.ConsecutivoCompania, " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ",ArticuloInventario.Codigo)"
   
   vWhere = gUtilSQL.DfSQLIsNull("CierreCostoDelPeriodo.existencia")
     If gTexto.DfLen(valLineaDeProducto) > 0 Then
       vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, "articuloInventario.LineaDeProducto", valLineaDeProducto, False)
     End If
     If gTexto.DfLen(valMarca) > 0 Then
       vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, "articuloInventario.Marca", valMarca, False)
     End If
      If gTexto.DfLen(valCategoria) > 0 Then
        vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, "articuloInventario.Categoria", valCategoria, False)
     End If
     If gTexto.DfLen(valCodigoIncio) > 0 And gTexto.DfLen(valCodigoFin) > 0 Then
        vWhere = vWhere & " AND " & gUtilSQL.fSQLValueBetween("articuloInventario.Codigo", valCodigoIncio, valCodigoFin)
     End If
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLValueDateWithComparisonRule(vWhere, insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Fecha", valFechaInicial, "<=")
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & " ArticuloInventario.Codigo AS CodigoArticulo, "
   vSQL = vSQL & " SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Entrada ) -   SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Salidad )  AS Cantidad, "
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(sqlUltimaCompra), gUtilSQL.fNumToStrSQL(0), sqlUltimaCompra, True) & " AS CostoUnitario,"
   vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & "  AS CantidadRecibida,"
   vSQL = vSQL & "ArticuloInventario.ConsecutivoCompania AS ConsecutivoCompania, "
   vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS NumeroSecuencialCompra,"
   vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & "  As ConsecutivoRenglon, "
   vSQL = vSQL & gUtilSQL.fIfResultIsNullReplace(vsqlFechaUltimaCompra, gUtilSQL.fDateToSQLValue(valFechaInicial)) & "  As Fecha "
   vSQL = vSQL & " FROM ArticuloInventario "
   vSQL = vSQL & " INNER JOIN "
   vSQL = vSQL & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & " ON "
   vSQL = vSQL & " ArticuloInventario.ConsecutivoCompania  =  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania AND "
   vSQL = vSQL & " ArticuloInventario.Codigo =  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo"
   vSQL = vSQL & " LEFT JOIN "
   vSQL = vSQL & " CierreCostoDelPeriodo ON  "
   vSQL = vSQL & " articuloInventario.ConsecutivoCompania = CierreCostoDelPeriodo.ConsecutivoCompania AND "
   vSQL = vSQL & " articuloInventario.Codigo = CierreCostoDelPeriodo.Codigo "
   vSQL = vSQL & vWhere
   vSQL = vSQL & " Group By "
   vSQL = vSQL & " ArticuloInventario.ConsecutivoCompania, "
   vSQL = vSQL & " ArticuloInventario.Codigo, "
   vSQL = vSQL & "CierreCostoDelPeriodo.existencia "
   fSQLDataSourceCargaInicialInsertar = vSQL
  Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDataSourceCargaInicialInsertar", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLCodigoDeArticulos(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String) As String
   Dim vSQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
    vWhere = ""
   If (valCodigoArticulo <> "") Then
     vWhere = gUtilSQL.fSQLValueWithAnd("", "articuloInventario.Codigo", valCodigoArticulo, False)
   End If
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "articuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "articuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "articuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSQL = " SELECT articuloInventario.Codigo "
   vSQL = vSQL & "   FROM   articuloInventario  "
   vSQL = vSQL & vWhere
  fSQLCodigoDeArticulos = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLCodigoDeArticulos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPonderadoReporte(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valSoloVigente As Boolean) As String
Dim SQL As String
Dim vFechaIni As Date
Dim vFechaFin As Date
On Error GoTo h_ERROR
vFechaIni = valFechaInicial - 1
SQL = SQL & " SELECT "
SQL = SQL & " CostoPromedioMovimientos.ConsecutivoCompania,"
SQL = SQL & " CostoPromedioMovimientos.Codigo,"
SQL = SQL & " CostoPromedioMovimientos.Descripcion ,"
SQL = SQL & " CostoPromedioMovimientos.Entrada,"
SQL = SQL & " CostoPromedioMovimientos.Salida,"
SQL = SQL & " CostoPromedioMovimientos.Retiro,"
SQL = SQL & " CostoPromedioMovimientos.Autoconsumo,"
SQL = SQL & " isnull(fn_CostoPromedioPonderado_1.existenciaFinal,0) AS ExistenciaAnterior ,"
SQL = SQL & " isnull(fn_CostoPromedioPonderado_1.existenciaFinal,0) + CostoPromedioMovimientos.Entrada - CostoPromedioMovimientos.Salida - CostoPromedioMovimientos.Retiro -  CostoPromedioMovimientos.Autoconsumo AS ExistenciaActual,"
SQL = SQL & " CostoPromedioMovimientos.costofinal *  CostoPromedioMovimientos.Entrada AS EntradaMoneda,"
SQL = SQL & " CostoPromedioMovimientos.costofinal *  CostoPromedioMovimientos.Salida  AS SalidaMoneda,"
SQL = SQL & " CostoPromedioMovimientos.costofinal *  CostoPromedioMovimientos.Retiro  AS RetiroMoneda,"
SQL = SQL & " CostoPromedioMovimientos.costofinal *  CostoPromedioMovimientos.Autoconsumo AS AutoconsumoMoneda,"
SQL = SQL & " (isnull(fn_CostoPromedioPonderado_1.existenciaFinal,0) +  CostoPromedioMovimientos.Entrada - CostoPromedioMovimientos.Salida - CostoPromedioMovimientos.Retiro -  CostoPromedioMovimientos.Autoconsumo) * CostoPromedioMovimientos.CostoFinal AS ExistenciaMoneda,"
SQL = SQL & " CostoPromedioMovimientos.costofinal ,"
SQL = SQL & " CostoPromedioMovimientos.costofinal * isnull(fn_CostoPromedioPonderado_1.existenciaFinal,0)  As COSTO_UNITARIO"
SQL = SQL & " FROM  dbo.Gf_CostoPromedioMovimientosInv( " & gUtilSQL.fDateToSQLValue(valFechaInicial) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS CostoPromedioMovimientos "
SQL = SQL & " LEFT    JOIN  dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(vFechaIni) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS fn_CostoPromedioPonderado_1 ON "
SQL = SQL & " CostoPromedioMovimientos.Codigo = fn_CostoPromedioPonderado_1.Codigo    and fn_CostoPromedioPonderado_1.CostoALaFecha = 'S'"

If (valSoloVigente) Then
    SQL = SQL & "  INNER JOIN ArticuloInventario ON "
    SQL = SQL & "  CostoPromedioMovimientos.Codigo = ArticuloInventario.Codigo  AND  "
    SQL = SQL & gUtilSQL.fSQLEnumValueWithAnd("", "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE) & "  AND "
    SQL = SQL & " ArticuloInventario.ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
End If

h_EXIT: On Error GoTo 0
fSqlCostoPromedioPonderadoReporte = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoReporte", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlBuscarArticulosInventario(ByVal valConsecutivoCompania As Long) As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vSQL As String
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSQL = ""
   vSQLWhere = ""
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   vSQL = " SELECT "
   vSQL = vSQL & " Codigo ,"
   vSQL = vSQL & " CodigoDelArticulo,"
   vSQL = vSQL & "TipoArticuloInv, TipoDeArticulo,"
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("CodigoColor"), gUtilSQL.fSimpleSqlValue(" "), "CodigoColor") & " As CodigoColor,"
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("CodigoTalla"), gUtilSQL.fSimpleSqlValue(" "), "CodigoTalla") & " As CodigoTalla,"
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("CodigoGrupo"), gUtilSQL.fSimpleSqlValue(" "), "CodigoGrupo") & " As CodigoGrupo,"
   vSQL = vSQL & "Serial,"
   vSQL = vSQL & "Rollo "
   vSQL = vSQL & " From " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
   vSQL = vSQL & vSQLWhere
h_EXIT: On Error GoTo 0
  Set insArticuloInvVista = Nothing
  fSqlBuscarArticulosInventario = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlBuscarArticulosConCotizacion", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLDelReporteConteoFisico(ByVal valConsecutivoCompania As Long, ByVal valNumeroDeConteoFisico As Long) As String
   Dim SQL As String
   Dim sqlWhere As String
   Dim SqlStatus As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
    
   Set gEnumProyecto = New clsEnumAdministrativo
  
   SqlStatus = gUtilSQL.DfSQLCaseIfForEnum("ConteoFisico.Status", enum_StatusConteoFisico.eSC_PROCESADO, gEnumProyecto.fEnumStatusConteoFisicoToStringInArray(True), "Status")
               
   sqlWhere = " WHERE ArticuloInventario.ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   If LenB(Trim(valNumeroDeConteoFisico)) > 0 And gConvert.ConvierteAdouble(valNumeroDeConteoFisico) > 0 Then
      sqlWhere = sqlWhere & " AND ConteoFisico.ConsecutivoConteo = " & valNumeroDeConteoFisico
   End If

   SQL = "SELECT conteoFisico.ConsecutivoConteo, articuloInventario.TipoArticuloInv, " & SqlStatus & ", "
   SQL = SQL & " ConteoFisico.CodigoAlmacen, almacen.NombreAlmacen, conteoFisico.SoloUnaLinea, articuloInventario.LineaDeProducto, renglonConteoFisico.CodigoArticulo,"
   SQL = SQL & " ArticuloInventario.Descripcion, renglonConteoFisico.ExistenciaEnElComputador, renglonConteoFisico.ExistenciaFisica, renglonConteoFisico.Diferencia, RenglonConteoFisico.Serial, RenglonConteoFisico.Rollo"
   SQL = SQL & " FROM (almacen INNER JOIN conteoFisico ON (almacen.Codigo = conteoFisico.CodigoAlmacen) AND (almacen.ConsecutivoCompania = conteoFisico.ConsecutivoCompania)) "
   SQL = SQL & " INNER JOIN (articuloInventario INNER JOIN renglonConteoFisico ON (articuloInventario.ConsecutivoCompania = renglonConteoFisico.ConsecutivoCompania) "
   SQL = SQL & " AND (articuloInventario.Codigo = renglonConteoFisico.CodigoArticulo)) ON (conteoFisico.ConsecutivoConteo = renglonConteoFisico.ConsecutivoConteo) "
   SQL = SQL & " AND (conteoFisico.ConsecutivoCompania = renglonConteoFisico.ConsecutivoCompania)"
   SQL = SQL & sqlWhere
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT conteoFisico.ConsecutivoConteo, articuloInventario.TipoArticuloInv, " & SqlStatus & ", "
   SQL = SQL & " ConteoFisico.CodigoAlmacen, almacen.NombreAlmacen, conteoFisico.SoloUnaLinea, articuloInventario.LineaDeProducto, renglonConteoFisico.CodigoArticulo, "
   SQL = SQL & " [ArticuloInventario].[descripcion]+' '+[Color].[DescripcionColor]+' '+[Talla].[DescripcionTalla] AS Descripcion, renglonConteoFisico.ExistenciaEnElComputador,"
   SQL = SQL & " RenglonConteoFisico.ExistenciaFisica, renglonConteoFisico.Diferencia, RenglonConteoFisico.Serial, RenglonConteoFisico.Rollo "
   SQL = SQL & " FROM ((almacen INNER JOIN conteoFisico ON (almacen.ConsecutivoCompania = conteoFisico.ConsecutivoCompania) AND (almacen.Codigo = conteoFisico.CodigoAlmacen)) "
   SQL = SQL & " INNER JOIN renglonConteoFisico ON (conteoFisico.ConsecutivoCompania = renglonConteoFisico.ConsecutivoCompania) AND (conteoFisico.ConsecutivoConteo = renglonConteoFisico.ConsecutivoConteo)) "
   SQL = SQL & " INNER JOIN (((ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo) AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) "
   SQL = SQL & " INNER JOIN Talla ON (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania)) INNER JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) "
   SQL = SQL & " AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)) ON (renglonConteoFisico.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania) AND (renglonConteoFisico.Serial = ExistenciaPorGrupo.Serial)"
   SQL = SQL & " AND (renglonConteoFisico.Rollo = ExistenciaPorGrupo.Rollo) AND (renglonConteoFisico.CodigoArticulo =(ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla))"
   SQL = SQL & sqlWhere
   SQL = SQL & " ORDER BY conteoFisico.ConsecutivoConteo, articuloInventario.LineaDeProducto, renglonConteoFisico.CodigoArticulo, RenglonConteoFisico.Serial, RenglonConteoFisico.Rollo"
   fSQLDelReporteConteoFisico = SQL
  Set gEnumProyecto = Nothing

h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "fSQLDelReporteConteoFisico", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fArticuloInventarioMaxOrMinCodigo(ByVal valConsecutivoCompania As Long, ByVal valMax As Boolean) As String
   Dim sqlWhere As String
   Dim vResult As String
   On Error GoTo h_ERROR
   If (valMax) Then
      vResult = " dbo.Gf_ArticuloInventarioMaxOrMinCodigo(" & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusArticulo.eSA_VIGENTE) & "," & gUtilSQL.fSimpleSqlValue("S") & ")"
     Else
      vResult = " dbo.Gf_ArticuloInventarioMaxOrMinCodigo(" & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusArticulo.eSA_VIGENTE) & "," & gUtilSQL.fSimpleSqlValue("N") & ")"
   End If
   fArticuloInventarioMaxOrMinCodigo = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fArticuloInventarioMaxOrMinCodigo", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLSearchArticulosParaElGridDeConteoFisico(ByVal valConsecutivoCompania As Long, ByVal valCodigoDelAlmacen As String, Optional ByVal valLineaDeProducto As String) As String
   Dim vSQLWhere As String
   Dim SQL As String
   On Error GoTo h_ERROR
  vSQLWhere = ""
 
  If valLineaDeProducto <> "" Then
       vSQLWhere = gUtilSQL.fSQLValueWithAnd(vSQLWhere, "ArticuloInventario.LineaDeProducto", valLineaDeProducto, False)
  End If
 
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "ArticuloInventario.StatusDelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSQLWhere = gUtilSQL.fSQLValueWithAnd(vSQLWhere, "existenciaPorAlmacen.CodigoAlmacen", valCodigoDelAlmacen, False)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   SQL = "SELECT existenciaPorAlmacen.CodigoArticulo AS CodigoArt, '' AS CodigoLote, existenciaPorAlmacen.Cantidad, '0' AS Serial,'0' AS Rollo,ArticuloInventario.TipoArticuloInv "
   SQL = SQL & " FROM articuloInventario INNER JOIN existenciaPorAlmacen ON (articuloInventario.Codigo = existenciaPorAlmacen.CodigoArticulo) AND (articuloInventario.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania)"
   SQL = SQL & vSQLWhere & " And ArticuloInventario.TipoArticuloInv = '" & eTAI_Simple & "'"
   SQL = SQL & " UNION ALL"
   SQL = SQL & " SELECT existenciaPorAlmacen.CodigoArticulo AS CodigoArt, '' AS CodigoLote, RenglonExistenciaAlmacen.Cantidad, RenglonExistenciaAlmacen.CodigoSerial AS SERIAL, RenglonExistenciaAlmacen.CodigoRollo AS ROLLO,ArticuloInventario.TipoArticuloInv "
   SQL = SQL & " FROM existenciaPorAlmacen INNER JOIN (articuloInventario INNER JOIN (RenglonExistenciaAlmacen INNER JOIN ExistenciaPorGrupo ON (RenglonExistenciaAlmacen.CodigoRollo = ExistenciaPorGrupo.Rollo)"
   SQL = SQL & " AND (RenglonExistenciaAlmacen.CodigoSerial = ExistenciaPorGrupo.Serial) AND (RenglonExistenciaAlmacen.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla)) AND (RenglonExistenciaAlmacen.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) ON (articuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo) AND (articuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) ON (existenciaPorAlmacen.CodigoArticulo = RenglonExistenciaAlmacen.CodigoArticulo) AND (existenciaPorAlmacen.CodigoAlmacen = RenglonExistenciaAlmacen.CodigoAlmacen) AND (existenciaPorAlmacen.ConsecutivoCompania = RenglonExistenciaAlmacen.ConsecutivoCompania)"
   SQL = SQL & vSQLWhere
   SQL = SQL & " UNION ALL "
   SQL = SQL & " SELECT existenciaPorAlmacen.CodigoArticulo AS CodigoArt, '' AS CodigoLote, ExistenciaPorAlmacen.Cantidad,'0' AS Serial,'0' AS Rollo, articuloInventario.TipoArticuloInv"
   SQL = SQL & " FROM existenciaPorAlmacen INNER JOIN (articuloInventario INNER JOIN ExistenciaPorGrupo ON (articuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania) AND (articuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)) ON (existenciaPorAlmacen.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla)) AND (existenciaPorAlmacen.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)"
   SQL = SQL & vSQLWhere & " And ArticuloInventario.TipoArticuloInv = '" & eTAI_UsaTalla_Color & "'"
   SQL = SQL & " UNION ALL" & vbCrLf
   SQL = SQL & "SELECT" & vbCrLf
   SQL = SQL & "   articuloInventario.Codigo AS CodigoArt," & vbCrLf
   SQL = SQL & "   Saw.LoteDeInventario.CodigoLote As CodigoLote," & vbCrLf
   SQL = SQL & "   Saw.LoteDeInventario.Existencia AS Cantidad," & vbCrLf
   SQL = SQL & "   '0' AS SERIAL," & vbCrLf
   SQL = SQL & "   '0' AS ROLLO," & vbCrLf
   SQL = SQL & "   articuloInventario.TipoArticuloInv" & vbCrLf
   SQL = SQL & "FROM" & vbCrLf
   SQL = SQL & "articuloInventario INNER JOIN" & vbCrLf
   SQL = SQL & "Saw.LoteDeInventario ON articuloInventario.Codigo = Saw.LoteDeInventario.CodigoArticulo" & vbCrLf
   SQL = SQL & "AND articuloInventario.ConsecutivoCompania = Saw.LoteDeInventario.ConsecutivoCompania" & vbCrLf
   SQL = SQL & "Where " & vbCrLf
   SQL = SQL & " (articuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_LoteFechadeVencimiento) & vbCrLf
   SQL = SQL & "    OR articuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Lote) & ")" & vbCrLf
   SQL = SQL & "AND " & vbCrLf
   SQL = SQL & " ArticuloInventario.StatusDelArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusArticulo.eSA_VIGENTE) & vbCrLf
   SQL = SQL & " AND  ArticuloInventario.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA) & vbCrLf
   SQL = SQL & " AND ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania
   fSQLSearchArticulosParaElGridDeConteoFisico = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearchArticulosParaElGridDeConteoFisico = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchArticulosParaElGridDeConteoFisico", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlArticulosAjustarCosto(ByVal valConsecutivoCompania As Long, ByVal valConjunto) As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   Dim vSQL As String
   vWhere = ""
   vWhere = "CODIGO IN (" & valConjunto & ")"
   vSQL = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
  fSqlArticulosAjustarCosto = "SELECT * FROM articuloInventario " & gUtilSQL.getWhereSQL(vSQL)
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSqlArticulosAjustarCosto = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "fSqlArticulosAjustarCosto", CM_FILE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function sActualizarPreciosConMargenesNuevos(ByVal valUsarNuevaFormula As Boolean, ByVal valUsaCostoPromedio As Boolean, ByVal valDesde As String, ByVal valHasta As String, ByVal vSQLAlicuotasVigentes As String, ByVal valConsecutivoCia As Long, ByVal valMarca As String, ByVal valMargen As Currency, ByVal valMargen2 As Currency, ByVal valMargen3 As Currency, ByVal valMargen4 As Currency, ByVal valPrecio As Boolean, ByVal valPrecio2 As Boolean, ByVal valPrecio3 As Boolean, ByVal valPrecio4 As Boolean, Optional ByVal valLineaProducto As String, Optional ByVal valCategoria As String) As String
   Dim Sql As String
   Dim SQLNameColum As String
   Dim vAumento As String
   On Error GoTo h_ERROR
   If (valUsaCostoPromedio) Then
   SQLNameColum = "CostoPromedio"
   Else
   SQLNameColum = "CostoUnitario"
   End If
       SQL = " UPDATE articuloInventario  SET "
      If valPrecio Then
        vAumento = fFormulaAumento(SQLNameColum, valMargen, valUsarNuevaFormula)
         SQL = SQL & " margenganancia = " & valMargen & ", "
         SQL = SQL & " PrecioSinIva  = " & vAumento & ","
         SQL = SQL & " PrecioConIva  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
      If valPrecio2 Or valPrecio3 Or valPrecio4 Then
            SQL = SQL & ", "
         End If
      End If
      If valPrecio2 Then
         vAumento = fFormulaAumento(SQLNameColum, valMargen2, valUsarNuevaFormula)
         SQL = SQL & " margenganancia2 = " & valMargen2 & ", "
         SQL = SQL & " PrecioSinIva2  = " & vAumento & ","
         SQL = SQL & " PrecioConIva2  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
         If valPrecio3 Or valPrecio4 Then
            SQL = SQL & ", "
         End If
      End If
      If valPrecio3 Then
         vAumento = fFormulaAumento(SQLNameColum, valMargen3, valUsarNuevaFormula)
         SQL = SQL & " margenganancia3 = " & valMargen3 & ", "
         SQL = SQL & " PrecioSinIva3  = " & vAumento & ","
         SQL = SQL & " PrecioConIva3  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
         If valPrecio4 Then
            SQL = SQL & ", "
         End If
      End If
      If valPrecio4 Then
         vAumento = fFormulaAumento(SQLNameColum, valMargen4, valUsarNuevaFormula)
         SQL = SQL & " margenganancia4 = " & valMargen4 & ", "
         SQL = SQL & " PrecioSinIva4  = " & vAumento & ","
         SQL = SQL & " PrecioConIva4  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
       End If
      SQL = SQL & " WHERE ConsecutivoCompania = " & valConsecutivoCia
      SQL = SQL & " AND articuloInventario.codigo between '" & valDesde & "' and '" & valHasta & "'"
      SQL = SQL & " AND articuloInventario.tipodearticulo = '0' or articuloInventario.tipodearticulo = '2'"
      If gTexto.DfLen(valLineaProducto) > 0 Then
         SQL = SQL & " AND LineaDeProducto = '" & valLineaProducto & "'"
      End If
      If gTexto.DfLen(valCategoria) > 0 Then
         SQL = SQL & " AND Categoria = '" & valCategoria & "'"
      End If
      If gTexto.DfLen(valMarca) > 0 Then
         SQL = SQL & " AND marca = '" & valMarca & "'"
      End If
    sActualizarPreciosConMargenesNuevos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActualizarPreciosConMargenes", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function sActualizarPreciosSinMargenesNuevos(ByVal valUsarNuevaFormula As Boolean, ByVal valUsaCostoPromedio As Boolean, ByVal valDesde As String, ByVal valHasta As String, ByVal vSQLAlicuotasVigentes As String, ByVal valConsecutivoCia As Long, ByVal valMarca As String, ByVal valPrecio As Boolean, ByVal valPrecio2 As Boolean, ByVal valPrecio3 As Boolean, ByVal valPrecio4 As Boolean, Optional ByVal valLineaProducto As String, Optional ByVal valCategoria As String) As String
   Dim SQLNameColum As String
   Dim SQL As String
   Dim vAumento As String
   On Error GoTo h_ERROR
   
    If (valUsaCostoPromedio) Then
   SQLNameColum = "CostoPromedio"
   Else
   SQLNameColum = "CostoUnitario"
   End If
   
   SQL = ""
   SQL = " UPDATE  articuloInventario  SET "
   If valPrecio Then
      
      vAumento = fFormulaAumentoSinMargenesNuevos(SQLNameColum, "MargenGanancia", valUsarNuevaFormula)
      SQL = SQL & " PrecioSinIva  = " & vAumento & ","
      SQL = SQL & " PrecioConIva  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
    
      If valPrecio2 Or valPrecio3 Or valPrecio4 Then
         SQL = SQL & ", "
      End If
   End If
   If valPrecio2 Then
      vAumento = fFormulaAumentoSinMargenesNuevos(SQLNameColum, "MargenGanancia2", valUsarNuevaFormula)
      SQL = SQL & " PrecioSinIva2  = " & vAumento & ","
  
      SQL = SQL & " PrecioConIva2  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
      If valPrecio3 Or valPrecio4 Then
         SQL = SQL & ", "
      End If
   End If
   If valPrecio3 Then
      vAumento = fFormulaAumentoSinMargenesNuevos(SQLNameColum, "MargenGanancia3", valUsarNuevaFormula)
      SQL = SQL & " PrecioSinIva3  = " & vAumento & ","
      SQL = SQL & " PrecioConIva3  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
      If valPrecio4 Then
         SQL = SQL & ", "
      End If
   End If
   If valPrecio4 Then
      vAumento = fFormulaAumentoSinMargenesNuevos(SQLNameColum, "MargenGanancia4", valUsarNuevaFormula)
      SQL = SQL & " PrecioSinIva4  = " & vAumento & ","
      SQL = SQL & " PrecioConIva4  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
   End If
      SQL = SQL & " WHERE ConsecutivoCompania = " & valConsecutivoCia
      SQL = SQL & " AND articuloInventario.codigo between '" & valDesde & "' and '" & valHasta & "'"
      SQL = SQL & " AND articuloInventario.tipodearticulo = 0 or articuloInventario.tipodearticulo = 2"
   If gTexto.DfLen(valLineaProducto) > 0 Then
      SQL = SQL & " AND LineaDeProducto = '" & valLineaProducto & "'"
   End If
   If gTexto.DfLen(valCategoria) > 0 Then
      SQL = SQL & " AND Categoria = '" & valCategoria & "'"
   End If
   If gTexto.DfLen(valMarca) > 0 Then
      SQL = SQL & " AND marca = '" & valMarca & "'"
   End If
   sActualizarPreciosSinMargenesNuevos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActualizarPreciosSinMargenes", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlActualizarCantidad(ByVal valConsecutivoCompania As Long, ByVal valCodigoAlmacen As String, ByVal valCantidad As Currency, ByVal valCodigo As String, ByVal valSerial As String, ByVal valRollo As String, ByVal valCodigoCompuesto As String, ByVal valTipoArticuloInvAsEnum As Integer, ByVal valNsql As Integer) As String
   Dim vResult As String
   On Error GoTo h_ERROR
   If (valNsql = 1) Then 'existenciaPorAlmacen
      vResult = "UPDATE existenciaPorAlmacen SET Cantidad = Cantidad + " & gUtilSQL.fNumToStrSQL(valCantidad)
      vResult = vResult & " WHERE CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
      If valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
        vResult = vResult & " AND CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
      Else
        vResult = vResult & " AND CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
      End If
      vResult = vResult & " AND ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   End If
   If (valNsql = 2) Then 'articuloInventario
       vResult = "UPDATE articuloInventario SET Existencia = Existencia + " & gUtilSQL.fNumToStrSQL(valCantidad)
       vResult = vResult & " WHERE Codigo=" & gUtilSQL.fSimpleSqlValue(valCodigo)
       vResult = vResult & " AND ConsecutivoCompania=" & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   End If
   If (valNsql = 3) Then 'ExistenciaPorGrupo
      If Not valTipoArticuloInvAsEnum = eTAI_Simple Then
         vResult = "UPDATE ExistenciaPorGrupo SET Existencia = Existencia + " & gUtilSQL.fNumToStrSQL(valCantidad)
         vResult = vResult & " WHERE ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
         If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Then
            vResult = vResult & " AND (CodigoArticulo+CodigoColor+CodigoTalla) = " & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
            If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
               vResult = vResult & " AND Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
               vResult = vResult & " AND Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
            End If
         Else
            vResult = vResult & " AND CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
            If valTipoArticuloInvAsEnum = eTAI_UsaSerial Then
               vResult = vResult & " AND Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
            ElseIf valTipoArticuloInvAsEnum = eTAI_UsaSerial_Rollo Then
               vResult = vResult & " AND Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
               vResult = vResult & " AND Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
            End If
         End If
      End If
   End If
   If (valNsql = 4) Then 'RenglonExistenciaAlmacen
      If Not valTipoArticuloInvAsEnum = eTAI_Simple Then
         vResult = "UPDATE RenglonExistenciaAlmacen SET Cantidad = " & gUtilSQL.fNumToStrSQL(valCantidad)
         vResult = vResult & " WHERE ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
         vResult = vResult & " AND CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
         If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Then
            vResult = vResult & "AND CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
            If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
               vResult = vResult & "AND CodigoSerial = " & gUtilSQL.fSimpleSqlValue(valSerial)
               vResult = vResult & "AND CodigoRollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
            End If
         Else
            vResult = vResult & "AND CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
            If valTipoArticuloInvAsEnum = eTAI_UsaSerial Then
               vResult = vResult & "AND CodigoSerial = " & gUtilSQL.fSimpleSqlValue(valSerial)
            ElseIf valTipoArticuloInvAsEnum = eTAI_UsaSerial_Rollo Then
               vResult = vResult & "AND CodigoSerial = " & gUtilSQL.fSimpleSqlValue(valSerial)
               vResult = vResult & "AND CodigoRollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
            End If
         End If
      End If
   End If
   fSqlActualizarCantidad = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlActualizarCantidad", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fActualizaTodosLosPreciosDeArticulos(ByVal valConsecutivoCia As Long, ByVal valLineaProducto As String, ByVal valAjuste As String, ByVal valporcentaje As Currency, Optional ByVal valPrecio As Boolean = True, Optional ByVal valPrecio2 As Boolean = True, Optional ByVal valPrecio3 As Boolean = True, Optional ByVal valPrecio4 As Boolean = True) As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = " UPDATE articuloInventario  SET "
   If valPrecio Then
      vResult = vResult & "PrecioSinIva  = (PrecioSinIva)  " & valAjuste & "  (PrecioSinIva * " & valporcentaje & ")/ 100, "
      vResult = vResult & "PrecioConIva  = (PrecioConIva)  " & valAjuste & " (" & valporcentaje & " * PrecioConIva)/ 100"
      If valPrecio2 Or valPrecio3 Or valPrecio4 Then
         vResult = vResult & ", "
      End If
   End If
   If valPrecio2 Then
      vResult = vResult & "PrecioSinIva2 = (PrecioSinIva2) " & valAjuste & " (" & valporcentaje & " * PrecioSinIva2)/ 100, "
      vResult = vResult & "PrecioConIva2 = (PrecioConIva2) " & valAjuste & " (" & valporcentaje & " * PrecioConIva2)/ 100"
      If valPrecio3 Or valPrecio4 Then
         vResult = vResult & ", "
      End If
   End If
   If valPrecio3 Then
      vResult = vResult & "PrecioSinIva3 = (PrecioSinIva3) " & valAjuste & " (" & valporcentaje & " * PrecioSinIva3)/ 100, "
      vResult = vResult & "PrecioConIva3 = (PrecioConIva3) " & valAjuste & " (" & valporcentaje & " * PrecioConIva3)/ 100"
      If valPrecio4 Then
         vResult = vResult & ", "
      End If
   End If
   If valPrecio4 Then
      vResult = vResult & "PrecioSinIva4 = (PrecioSinIva4) " & valAjuste & " (" & valporcentaje & " * PrecioSinIva4)/ 100,"
      vResult = vResult & "PrecioConIva4 = (PrecioConIva4) " & valAjuste & " (" & valporcentaje & " * PrecioConIva4)/ 100"
   End If
   vResult = vResult & " WHERE ConsecutivoCompania = " & valConsecutivoCia
   vResult = vResult & " AND articuloInventario.StatusdelArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusArticulo.eSA_DESINCORPORADO)
   If LenB(valLineaProducto) > 0 Then
      vResult = vResult & " AND LineaDeProducto = '" & valLineaProducto & "'"
   End If
   fActualizaTodosLosPreciosDeArticulos = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fActualizaTodosLosPreciosDeArticulos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDetalleDelInventarioPermanenteValorizadoUltimaCompra(ByVal valCodigoCompaniaActual As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim Sql As String
   Dim vSqlWhere As String
   Dim ExistenciaInicial As String
   Dim insInvPermanenteVista As clsArticuloInvVista
   On Error GoTo h_ERROR
   Set insInvPermanenteVista = New clsArticuloInvVista
     
    vSQLWhere = gUtilSQL.fSQLValueDatesBetween("", "Fecha", valFechaInicial, valFechaFinal)
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "consecutivocompania", valCodigoCompaniaActual)
    vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
 
    SQL = " ;WITH ArticulosyCostos(ConsecutivoCompania,Fecha,TipoDeDocumento, TipoDeOperacion,NumeroDeDocumento,CodigoArticulo,Descripcion,UnidadDeVenta,Entradas,Salidas,CostoUnitarioCompra,CostoUnitarioOperacion)"
   
    SQL = SQL & "AS("
    SQL = SQL & "SELECT"
    SQL = SQL & " Vista.ConsecutivoCompania,"
    SQL = SQL & " Vista.Fecha,"
    SQL = SQL & " Vista.TipoDeDocumento,"
    SQL = SQL & " Vista.TipoDeOperacion,"
    SQL = SQL & " Vista.NumeroDeDocumento,"
    SQL = SQL & " Vista.CodigoArticulo,"
    SQL = SQL & " Vista.Descripcion,"
    SQL = SQL & " Vista.UnidadDeVenta,"
    SQL = SQL & " Vista.Entradas,"
    SQL = SQL & " Vista.Salidas,"
    SQL = SQL & " Round(Vista.CostoUnitario,2) AS CostoUnitarioCompra ,"
    SQL = SQL & " Round(dbo.Gf_CompraUnaFecha(ConsecutivoCompania, Vista.Fecha, Vista.CodigoArticulo), 2) As CostoUnitarioOperacion"
    SQL = SQL & " FROM  " & insInvPermanenteVista.GetViewNameRegistroInventarioPermanenteValorizado & " AS Vista "
    SQL = SQL & vSQLWhere
    SQL = SQL & ")"
    SQL = SQL & "SELECT"
    SQL = SQL & " Fecha,"
    SQL = SQL & " TipoDeDocumento,"
    SQL = SQL & " TipoDeOperacion,"
    SQL = SQL & " NumeroDeDocumento,"
    SQL = SQL & " CodigoArticulo,"
    SQL = SQL & " Descripcion,"
    SQL = SQL & " UnidadDeVenta,"
    SQL = SQL & "isnull( cast ((CASE WHEN TipoDeDocumento = 'Compra' THEN CostoUnitarioCompra ELSE  CostoUnitarioOperacion  END )as decimal(10,2)) ,0 ) AS CostoUnitario ,"
    SQL = SQL & " Entradas,"
    SQL = SQL & " isnull(cast ((CASE WHEN  TipoDeDocumento = 'Compra' AND  Entradas > 0 THEN CostoUnitarioCompra WHEN  TipoDeDocumento <> 'Compra' AND  Entradas > 0 THEN  CostoUnitarioOperacion END )as decimal(10,2)),0) AS CostoUnitarioEntrada ,"
    SQL = SQL & " isnull(cast ((CASE WHEN  TipoDeDocumento = 'Compra' AND  Entradas > 0 THEN CostoUnitarioCompra * Entradas  WHEN  TipoDeDocumento <> 'Compra' AND  Entradas > 0 THEN  CostoUnitarioOperacion END )as decimal(10,2)),0) AS CostoTotalEntrante,"
    SQL = SQL & " Salidas ,"
    SQL = SQL & " isnull(CASE WHEN  Salidas > 0  THEN CostoUnitarioOperacion ELSE 0 END,0 )  AS CostoUnitarioSalida,"
    SQL = SQL & " isnull(cast ((CostoUnitarioOperacion *  Salidas) as decimal(10,2)),0)  AS CostoTotalSaliente, "
    SQL = SQL & " isnull(cast (cast ((CASE WHEN  TipoDeDocumento = 'Compra' AND  Entradas > 0 THEN CostoUnitarioCompra * Entradas  WHEN  TipoDeDocumento <> 'Compra' AND  Entradas > 0 THEN  CostoUnitarioOperacion END )as decimal(10,2)) - (CostoUnitarioOperacion *  Salidas) as decimal(10,2)),0)  AS CostoTotal "
   
   
    SQL = SQL & " From ArticulosyCostos"
    SQL = SQL & " ORDER BY  CodigoArticulo,Fecha"
   Set insInvPermanenteVista = Nothing
h_EXIT: On Error GoTo 0
   fSQLDetalleDelInventarioPermanenteValorizadoUltimaCompra = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDetalleDelInventarioPermanenteValorizadoUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPorAlmacenReporte(ByVal valConsecutivoCompania As Long, ByVal valCodigoAlmacen As String, ByVal valFechaCierre As Date, ByVal valFecha As Date, ByVal valCodigoMin As String, ByVal valCodigoMax As String) As String

   Dim vSQL As String
   Dim vSQLWhere As String
   Dim insArticuloInvVista As clsArticuloInvVista
    On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
    vSQLWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", "ArticuloInvMovimientosAlmacen.Fecha", valFecha, "<=")
   
    If (valCodigoAlmacen <> "") Then
      vSQLWhere = gUtilSQL.fSQLValueWithAnd(vSQLWhere, "ArticuloInvMovimientosAlmacen.CodigoAlmacen", valCodigoAlmacen, False)
    End If
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "articuloInventario.ConsecutivoCompania", valConsecutivoCompania)
    vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
 
  
   vSQL = vSQL & " SELECT"
   vSQL = vSQL & " ArticuloInvMovimientosAlmacen.CodigoAlmacen,"
   vSQL = vSQL & " NombreAlmacen,"
   vSQL = vSQL & " articuloInventario.Codigo,"
   vSQL = vSQL & " articuloInventario.Descripcion,"
   vSQL = vSQL & " SUM(ArticuloInvMovimientosAlmacen.Entrada) - SUM (ArticuloInvMovimientosAlmacen.Salida) AS Existencia,"
   vSQL = vSQL & " fn_CostoPromedioPonderado_1.costofinal AS CostoUnitario,"
   vSQL = vSQL & " (SUM(ArticuloInvMovimientosAlmacen.Entrada) - SUM (ArticuloInvMovimientosAlmacen.Salida)) * fn_CostoPromedioPonderado_1.costofinal AS CostoTotal"
 
   vSQL = vSQL & " FROM  articuloInventario"
   vSQL = vSQL & " INNER JOIN  " & insArticuloInvVista.GetViewNameArticulosMovimientoTransferencia() & " AS ArticuloInvMovimientosAlmacen ON"
   vSQL = vSQL & " articuloInventario.codigo = ArticuloInvMovimientosAlmacen.codigo AND "
   vSQL = vSQL & " articuloInventario.ConsecutivoCompania = ArticuloInvMovimientosAlmacen.ConsecutivoCompania"
   vSQL = vSQL & "  INNER   JOIN  dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFecha) & "," & gUtilSQL.fSimpleSqlValue(valCodigoMin) & "," & gUtilSQL.fSimpleSqlValue(valCodigoMax) & ") AS fn_CostoPromedioPonderado_1 ON "
   vSQL = vSQL & " articuloInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo   AND"
   vSQL = vSQL & " fn_CostoPromedioPonderado_1.CostoALaFecha = 'S'"
   vSQL = vSQL & " INNER   JOIN   almacen ON "
   
   vSQL = vSQL & " almacen.Codigo = ArticuloInvMovimientosAlmacen.CodigoAlmacen AND"
   vSQL = vSQL & " almacen.ConsecutivoCompania = ArticuloInvMovimientosAlmacen.ConsecutivoCompania"
   
   
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & " Group By"
   vSQL = vSQL & "  ArticuloInvMovimientosAlmacen.CodigoAlmacen,"
   vSQL = vSQL & "  NombreAlmacen,"
   vSQL = vSQL & " articuloInventario.Codigo,"
   vSQL = vSQL & " articuloInventario.Descripcion,"
   vSQL = vSQL & "  fn_CostoPromedioPonderado_1.costofinal"
   
   vSQL = vSQL & " ORDER BY "
   vSQL = vSQL & " ArticuloInvMovimientosAlmacen.CodigoAlmacen, articuloInventario.Codigo"
   
   fSqlCostoPromedioPorAlmacenReporte = vSQL
  Set insArticuloInvVista = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         " fSqlCostoPromedioPorAlmacenReporte", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function
Public Function fSqlCostoPromedioGeneralReporte(ByVal valConsecutivoCompania As Long, ByVal valFechaCierre As Date, ByVal valFecha As Date, ByVal valCodigoMin As String, ByVal valCodigoMax As String) As String

   Dim vSQL As String
   Dim vSQLWhere As String
   Dim insArticuloInvVista As clsArticuloInvVista
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSQLWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", "ArticuloInvMovimientosAlmacen.Fecha", valFecha, "<=")
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "articuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
  
   vSQL = vSQL & " SELECT"
   vSQL = vSQL & " articuloInventario.Codigo,"
   vSQL = vSQL & " articuloInventario.Descripcion,"
   vSQL = vSQL & " SUM(ArticuloInvMovimientosAlmacen.Entrada) - SUM (ArticuloInvMovimientosAlmacen.Salidad) AS Existencia,"
   vSQL = vSQL & " fn_CostoPromedioPonderado_1.costofinal AS CostoUnitario,"
   vSQL = vSQL & " (SUM(ArticuloInvMovimientosAlmacen.Entrada) - SUM (ArticuloInvMovimientosAlmacen.Salidad)) * fn_CostoPromedioPonderado_1.costofinal AS CostoTotal"
 
   vSQL = vSQL & " FROM  articuloInventario"
   vSQL = vSQL & " INNER JOIN  " & insArticuloInvVista.GetViewNameArticuloInvMovimientoAlmacen() & " AS ArticuloInvMovimientosAlmacen ON"
   vSQL = vSQL & " articuloInventario.codigo = ArticuloInvMovimientosAlmacen.codigo AND "
   vSQL = vSQL & " articuloInventario.ConsecutivoCompania = ArticuloInvMovimientosAlmacen.ConsecutivoCompania"
   vSQL = vSQL & "  INNER   JOIN  dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFecha) & "," & gUtilSQL.fSimpleSqlValue(valCodigoMin) & "," & gUtilSQL.fSimpleSqlValue(valCodigoMax) & ") AS fn_CostoPromedioPonderado_1 ON "
   vSQL = vSQL & " articuloInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo   AND"
   vSQL = vSQL & " fn_CostoPromedioPonderado_1.CostoALaFecha = 'S'"
   
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & " Group By"
   vSQL = vSQL & " articuloInventario.Codigo,"
   vSQL = vSQL & " articuloInventario.Descripcion,"
   vSQL = vSQL & " fn_CostoPromedioPonderado_1.costofinal"
    vSQL = vSQL & " ORDER BY "
   vSQL = vSQL & " articuloInventario.Codigo"
    fSqlCostoPromedioGeneralReporte = vSQL
  Set insArticuloInvVista = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         " fSqlCostoPromedioGeneralReporte", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function
Public Function fFormulaAumento(ByVal valNameColumna As String, ByVal valMargen As Currency, ByVal valUsarNuevaFormula As Boolean) As String
   Dim vAumento As String
   Dim vResult As String
   Dim SQLNameColum As String
   On Error GoTo h_ERROR
   valMargen = valMargen / 100
   If (valUsarNuevaFormula) Then
      vAumento = " (1 / ((1 - " & gUtilSQL.fNumToStrSQL(valMargen) & " )))"
      vAumento = "(" & valNameColumna & "*" & vAumento & ")"
   Else
      vAumento = "(((" & valNameColumna & "  * " & gUtilSQL.fNumToStrSQL(valMargen) & " )) + " & valNameColumna & " )"
   End If
      vResult = vAumento
   fFormulaAumento = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " fFormulaAumento", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fFormulaAumentoSinMargenesNuevos(ByVal valNameColumna As String, ByVal valMargen As String, ByVal valUsarNuevaFormula As Boolean) As String
   Dim vAumento As String
   Dim vResult As String
   Dim SQLNameColum As String
   On Error GoTo h_ERROR

   
   If (valUsarNuevaFormula) Then
      vAumento = " (1 / (1 - (" & valMargen & " / 100  )))"
      vAumento = "(" & valNameColumna & "*" & vAumento & ")"
   Else
      vAumento = "(((" & valNameColumna & "  * " & valMargen & "  ) / 100) + " & valNameColumna & " )"
   End If
      vResult = vAumento
   fFormulaAumentoSinMargenesNuevos = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " fFormulaAumento", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlCostoPromedioPonderadoArticulosCompuestos(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesdeCompuestos As String, ByVal valCodigoHastaCompuestos As String, ByVal valFechaCierre As Date, ByVal valFechaReporte As Date, ByVal valNivelDePrecio As enum_NivelDePrecio, ByVal valPrecioSinIva As Boolean, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String) As String
Dim insArticuloInvVista As clsArticuloInvVista
Dim SQL As String
Dim vPrecio As String
Dim vSQLWhere As String
On Error GoTo h_ERROR
Set insArticuloInvVista = New clsArticuloInvVista
   vPrecio = fSqlNivelDePrecio(valNivelDePrecio, valPrecioSinIva)
   vSQLWhere = gUtilSQL.fSQLValueBetween("VistaArticulosCompuestos.Compuesto", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesdeCompuestos), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHastaCompuestos))
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "VistaArticulosCompuestos.ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)

   SQL = SQL & " SELECT "
   SQL = SQL & "VistaArticulosCompuestos.Compuesto,"
   SQL = SQL & "VistaArticulosCompuestos.DescripcionCompuesto,"
   SQL = SQL & gUtilSQL.getIIF("VistaArticulosCompuestos.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO), gUtilSQL.fSimpleSqlValue(" * "), gUtilSQL.fSimpleSqlValue("")) & "+VistaArticulosCompuestos.Codigo AS Codigo,"
   SQL = SQL & "VistaArticulosCompuestos.Descripcion,"
   SQL = SQL & "VistaArticulosCompuestos.Cantidad AS Aporte,"
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("Gf_CostoPromedioPonderado_1.CostoFinal", "VistaArticulosCompuestos.CostoUnitario") & " AS  CostoIndividual,"
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("VistaArticulosCompuestos.Cantidad", gUtilSQL.fNumToStrSQL(0)) & " * " & gUtilSQL.fIfResultIsNullReplace("Gf_CostoPromedioPonderado_1.CostoFinal", "VistaArticulosCompuestos.CostoUnitario") & " AS  AporteDelIten,"
   SQL = SQL & "VistaArticulosCompuestos." & vPrecio & " AS PVP "
   SQL = SQL & " FROM  " & insArticuloInvVista.GetViewNameArticulosProductosCompuestos() & " AS VistaArticulosCompuestos LEFT  JOIN  "
   SQL = SQL & " dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaReporte) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS Gf_CostoPromedioPonderado_1 ON "
   SQL = SQL & " VistaArticulosCompuestos.Codigo = Gf_CostoPromedioPonderado_1.Codigo AND "
   SQL = SQL & " Gf_CostoPromedioPonderado_1.CostoAlaFecha = " & gUtilSQL.fBooleanToSqlValue(True)
   SQL = SQL & vSQLWhere
  Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
  fSqlCostoPromedioPonderadoArticulosCompuestos = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoArticulosCompuestos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSqlCostoUltimaCompraArticulosCompuestos(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesdeCompuestos As String, ByVal valCodigoHastaCompuestos As String, ByVal valFechaReporte As Date, ByVal valNivelDePrecio As enum_NivelDePrecio, ByVal valPrecioSinIva As Boolean, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String) As String
Dim insArticuloInvVista As clsArticuloInvVista
Dim SQL As String
Dim vPrecio As String
Dim vSQLWhere As String
On Error GoTo h_ERROR
Set insArticuloInvVista = New clsArticuloInvVista
   vPrecio = fSqlNivelDePrecio(valNivelDePrecio, valPrecioSinIva)
   vSQLWhere = gUtilSQL.fSQLValueBetween("VistaArticulosCompuestos.Compuesto", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesdeCompuestos), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHastaCompuestos))
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "VistaArticulosCompuestos.ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)

   SQL = SQL & " SELECT "
   SQL = SQL & "VistaArticulosCompuestos.Compuesto,"
   SQL = SQL & "VistaArticulosCompuestos.DescripcionCompuesto,"
   SQL = SQL & gUtilSQL.getIIF("VistaArticulosCompuestos.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO), gUtilSQL.fSimpleSqlValue(" * "), gUtilSQL.fSimpleSqlValue("")) & "+VistaArticulosCompuestos.Codigo AS Codigo,"
   SQL = SQL & "VistaArticulosCompuestos.Descripcion,"
   SQL = SQL & "VistaArticulosCompuestos.Cantidad AS Aporte,"
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("Gf_CostosUltimaCompra.CostoFinal", "VistaArticulosCompuestos.CostoUnitario") & " AS  CostoIndividual,"
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace("VistaArticulosCompuestos.Cantidad", gUtilSQL.fNumToStrSQL(0)) & " * " & gUtilSQL.fIfResultIsNullReplace("Gf_CostosUltimaCompra.CostoFinal", "VistaArticulosCompuestos.CostoUnitario") & " AS  AporteDelIten,"
   SQL = SQL & "VistaArticulosCompuestos." & vPrecio & " AS PVP "
   SQL = SQL & " FROM  " & insArticuloInvVista.GetViewNameArticulosProductosCompuestos() & " AS VistaArticulosCompuestos LEFT  JOIN  "
   SQL = SQL & "  dbo.Gf_CostosUltimaCompra(" & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaReporte) & "," & gUtilSQL.fDateToSQLValue(valFechaReporte) & "," & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde)) & "," & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta)) & ") AS Gf_CostosUltimaCompra ON "
   SQL = SQL & " VistaArticulosCompuestos.Codigo = Gf_CostosUltimaCompra.Codigo "
   
   
   SQL = SQL & vSQLWhere
  Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
  fSqlCostoUltimaCompraArticulosCompuestos = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoUltimaCompraArticulosCompuestos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCamposNoRequeridos(ByVal valCodigoArt As String, ByVal gProyCompaniaActual As Object) As String
   Dim strCampos As String
   Dim SQL As String
   On Error GoTo h_ERROR
   strCampos = "ConsecutivoCompania, Codigo, CantArtReservado, NombreOperador, FechaUltimaModificacion, fldTimeStamp, CodigoGrupo, TipoArticuloInv, UsaBalanza"
   SQL = "SELECT " & strCampos & " FROM articuloInventario WHERE Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoArt) & " AND ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   fSQLCamposNoRequeridos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLCamposNoRequeridos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLAlicuotasVigentes(ByVal gAdmAlicuotaIvaActual As Object) As String
   Dim valAlicuotaGeneral As Currency
   Dim valAlicuota2 As Currency
   Dim valAlicuota3 As Currency
   Dim SQL As String
   On Error GoTo h_ERROR
   valAlicuotaGeneral = gAdmAlicuotaIvaActual.GetAlicuotaIVA(gUtilDate.getFechaDeHoy, eTD_ALICUOTAGENERAL)
   valAlicuota2 = gAdmAlicuotaIvaActual.GetAlicuotaIVA(gUtilDate.getFechaDeHoy, eTD_ALICUOTA2)
   valAlicuota3 = gAdmAlicuotaIvaActual.GetAlicuotaIVA(gUtilDate.getFechaDeHoy, eTD_ALICUOTA3)
   SQL = " (case when alicuotaiva = 1 then " & valAlicuotaGeneral & " when alicuotaiva = 2 then " & valAlicuota2
   SQL = SQL & " when alicuotaiva = 3 then " & valAlicuota3 & " else " & valAlicuotaGeneral & " end)"
   fSQLAlicuotasVigentes = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLCaseAlicuotas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSqlCostoUltimaCompraValoracionDeInventario(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim SQL As String
Dim sqlWhere As String
Dim SQLCodigo   As String
Dim sqlWhere2 As String
Dim gEnumProyecto As clsEnumAdministrativo
On Error GoTo h_ERROR
Set gEnumProyecto = New clsEnumAdministrativo

                 
       

   sqlWhere = gUtilSQL.fSQLValueBetween("CODIGO", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(sqlWhere, "Fecha", valFechaInicial, valFechaFinal)
   sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, "ConsecutivoCompania", valConsecutivoCompania)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
 
   sqlWhere2 = gUtilSQL.fSQLEnumValueWithAnd("", "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   sqlWhere2 = gUtilSQL.getWhereSQL(sqlWhere2)
 
 SQL = "; WITH  ArticulosyCostos(ConsecutivoCompania,PrioridadOperacion, NumeroDocumento, "
 SQL = SQL & " Codigo, "
 SQL = SQL & " TipoDeDocumento,"
 SQL = SQL & " Fecha,"
 SQL = SQL & " Descripcion,"
 SQL = SQL & " entrada,"
 SQL = SQL & " salidad,"
 SQL = SQL & " MontoOperacion,"
 SQL = SQL & " existencia,"
 SQL = SQL & " costoAnterior,"
 SQL = SQL & " costoActual) AS "
 SQL = SQL & " ( SELECT "
 SQL = SQL & " ConsecutivoCompania,"
 SQL = SQL & " PrioridadOperacion,"
 SQL = SQL & " NumeroDocumento,"
 SQL = SQL & " Codigo,"
 SQL = SQL & " TipoDeDocumento,"
 SQL = SQL & " Fecha,"
 SQL = SQL & " Descripcion,"
 SQL = SQL & " entrada,"
 SQL = SQL & " salidad,"
 SQL = SQL & " MontoOperacion,"
 SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.Gf_Existencia(ConsecutivoCompania,Fecha,Codigo, PrioridadOperacion, NumeroDocumento)"), gUtilSQL.fNumToStrSQL(0), "dbo.Gf_Existencia(ConsecutivoCompania,Fecha,Codigo, PrioridadOperacion, NumeroDocumento)", False) & " As existencia, "
 SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha-1,Codigo )"), gUtilSQL.fNumToStrSQL(0), "dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha-1,Codigo )", False) & " As costoAnterior,"
 SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha,Codigo )"), gUtilSQL.fNumToStrSQL(0), "dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha,Codigo )", False) & " As costoActual "
 
SQL = SQL & "  FROM   IGV_ArticuloInvMovimiento  "
SQL = SQL & sqlWhere
SQL = SQL & " ) "
SQL = SQL & "  select"
SQL = SQL & "  ArticulosyCostos.codigo,"
SQL = SQL & "  ArticulosyCostos.descripcion,"
SQL = SQL & "  dbo.articuloInventario.UnidadDeVenta,"
SQL = SQL & "  fecha,"
SQL = SQL & "  tipodedocumento,"
SQL = SQL & "  NumeroDocumento, "
SQL = SQL & "  ArticulosyCostos.existencia AS ExistenciaInicial,"
SQL = SQL & "  costoAnterior as CostoInicial,"
SQL = SQL & "  ArticulosyCostos.existencia * costoAnterior  as MontoInicial,"
SQL = SQL & "  entrada ,"
SQL = SQL & gUtilSQL.getIIF("MontoOperacion = 0", "costoAnterior *  entrada", "MontoOperacion") & " As MontoEntrada, "
SQL = SQL & gUtilSQL.getIIF("MontoOperacion = 0", "0", "MontoOperacion / entrada") & " As ConstoUnitarioEntrada, "
SQL = SQL & "  salidad,"
SQL = SQL & "  costoAnterior *  salidad AS MontoSalidad,"
SQL = SQL & "  costoAnterior   AS CostoUnitarioSalida,"
SQL = SQL & "  ArticulosyCostos.existencia + entrada - salidad as existenciaFinal,"
SQL = SQL & gUtilSQL.getIIF("MontoOperacion = 0", "costoActual", "MontoOperacion / entrada") & " As CostoFinal, "
SQL = SQL & gUtilSQL.getIIF("MontoOperacion = 0", "costoActual", "MontoOperacion / entrada") & " * (ArticulosyCostos.Existencia + entrada - salidad ) As MontoFinal "
SQL = SQL & "   From ArticulosyCostos "
SQL = SQL & "   INNER JOIN  dbo.articuloInventario   ON  "
SQL = SQL & "   ArticulosyCostos.ConsecutivoCompania = articuloInventario.ConsecutivoCompania AND   "
SQL = SQL & "   ArticulosyCostos.Codigo = articuloInventario.Codigo"
SQL = SQL & sqlWhere2
SQL = SQL & "  order by codigo, fecha, PrioridadOperacion, numerodocumento"
Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
  fSqlCostoUltimaCompraValoracionDeInventario = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoUltimaCompraValoracionDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlCostoPromedioPonderadoReporteDetalladoValoracionDeInventario(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim SQL As String
Dim sqlWhere As String
Dim SQLCodigo   As String
On Error GoTo h_ERROR
If (gUtilDate.fF1IsLessThanF2(valFechaInicial, valFechaCierre)) Then
valFechaInicial = valFechaCierre + 1
End If
  SQL = SQL & fSqlCostoPromedioPonderadoReporteSelect1ValoracionDeInventario(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaCierre, valFechaInicial, valFechaFinal)
  SQL = SQL & " UNION "
  SQL = SQL & fSqlCostoPromedioPonderadoReporteSelect2ValoracionDeInventario(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaCierre, valFechaInicial, valFechaFinal)
h_EXIT: On Error GoTo 0
 fSqlCostoPromedioPonderadoReporteDetalladoValoracionDeInventario = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoReporteDetalladoValoracionDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPonderadoReporteSelect1ValoracionDeInventario(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim SQL As String
Dim sqlWhere As String
Dim SQLCodigo   As String
On Error GoTo h_ERROR

   sqlWhere = gUtilSQL.fSQLValueBetween("fn_CostoPromedioPonderado_1.codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   sqlWhere = sqlWhere & " AND  fn_CostoPromedioPonderado_1.CostoAlaFecha = " & gUtilSQL.fBooleanToSqlValue(True)
   sqlWhere = gUtilSQL.fSQLValueDateWithComparisonRule(sqlWhere, "fn_CostoPromedioPonderado_1.Fecha", valFechaInicial, "<")
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   SQL = SQL & "  SELECT "
   SQL = SQL & " fn_CostoPromedioPonderado_1.id_num, "
   SQL = SQL & " fn_CostoPromedioPonderado_1.codigo AS codigo, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Saldo") & " AS descripcion ,"
   SQL = SQL & " ArticuloInventario.UnidadDeVenta ,  "
   SQL = SQL & gUtilSQL.fDateToSQLValue(valFechaInicial) & "  AS fecha,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.tipodedocumento AS tipodedocumento, "
   SQL = SQL & " fn_CostoPromedioPonderado_1.NumeroDocumento, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS ExistenciaInicial,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS CostoInicial,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS  MontoInicial,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS entrada ,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " As MontoEntrada,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " As ConstoUnitarioEntrada, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS salidad,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoSalidad,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS CostoUnitarioSalida,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.existenciaFinal AS existenciaFinal ,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.CostoFinal AS CostoFinal,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.TotalFinal AS MontoFinal "
   SQL = SQL & " FROM  ArticuloInventario INNER JOIN "
   SQL = SQL & "   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS fn_CostoPromedioPonderado_1 ON "
   SQL = SQL & "  ArticuloInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo AND "
   SQL = SQL & " ArticuloInventario.ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   SQL = SQL & sqlWhere
h_EXIT: On Error GoTo 0
  fSqlCostoPromedioPonderadoReporteSelect1ValoracionDeInventario = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoReporteSelect1ValoracionDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPonderadoReporteSelect2ValoracionDeInventario(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim SQL As String
Dim sqlWhere As String
Dim SQLCodigo   As String
On Error GoTo h_ERROR
   sqlWhere = gUtilSQL.fSQLValueBetween("fn_CostoPromedioPonderado_1.codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(sqlWhere, "fn_CostoPromedioPonderado_1.Fecha", valFechaInicial, valFechaFinal)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   SQL = SQL & "  SELECT "
   SQL = SQL & " fn_CostoPromedioPonderado_1.id_num, "
   SQL = SQL & " fn_CostoPromedioPonderado_1.codigo AS codigo,"
   SQL = SQL & " ArticuloInventario.descripcion AS descripcion ,"
   SQL = SQL & " ArticuloInventario.UnidadDeVenta ,  "
   SQL = SQL & " fn_CostoPromedioPonderado_1.fecha AS fecha,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.tipodedocumento AS tipodedocumento,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.NumeroDocumento, "
   SQL = SQL & " fn_CostoPromedioPonderado_1.ExistenciaInicial AS ExistenciaInicial,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.CostoInicial AS CostoInicial,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.TotalInicial AS  MontoInicial,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.Entradas AS entrada ,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.TotalOperacion As MontoEntrada,"
   SQL = SQL & gUtilSQL.getIIF("fn_CostoPromedioPonderado_1.TotalOperacion = 0", "0", "fn_CostoPromedioPonderado_1.TotalOperacion / fn_CostoPromedioPonderado_1.Entradas ") & " As ConstoUnitarioEntrada, "
   SQL = SQL & " fn_CostoPromedioPonderado_1.Salidas AS salidad,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.CostoInicial *   fn_CostoPromedioPonderado_1.salidas AS MontoSalidad,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.CostoInicial AS CostoUnitarioSalida, "
   SQL = SQL & " fn_CostoPromedioPonderado_1.existenciaFinal AS existenciaFinal,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.CostoFinal AS CostoFinal,"
   SQL = SQL & " fn_CostoPromedioPonderado_1.TotalFinal AS MontoFinal"
   SQL = SQL & " FROM  ArticuloInventario INNER JOIN "
   SQL = SQL & "   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS fn_CostoPromedioPonderado_1 ON "
   SQL = SQL & "  ArticuloInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo AND "
   SQL = SQL & " ArticuloInventario.ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
  SQL = SQL & sqlWhere
h_EXIT: On Error GoTo 0
fSqlCostoPromedioPonderadoReporteSelect2ValoracionDeInventario = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoReporteSelect2ValoracionDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlInventarioPorCategoria(ByVal valConsecutivoCompania As Long, ByVal valOrdenarPor As String, ByVal valUsaFiltro As Boolean, ByVal valFiltrarPor As String, ByVal valParaFiltrar As String) As String
Dim SQL As String
On Error GoTo h_ERROR
   SQL = SQL & " SELECT "
   SQL = SQL & " Categoria,Codigo,Descripcion,Existencia,"
   SQL = SQL & " CantArtReservado As Reserva,Existencia - CantArtReservado as Disponible,LineaDeProducto As " & gUtilSQL.fSimpleSqlValue("Linea De Producto")
   SQL = SQL & " FROM  ArticuloInventario  "
   SQL = SQL & " WHERE  "
   SQL = SQL & gUtilSQL.DfMidSQL("Descripcion", 1, 7) & " <> " & gUtilSQL.fSimpleSqlValue("Resumen") & " AND " & gUtilSQL.DfMidSQL("Descripcion", 1, 8) & " <> " & gUtilSQL.fSimpleSqlValue("comisión") & " AND "
   SQL = SQL & "ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania)
   If (valOrdenarPor = "Código") Then
   valOrdenarPor = "Codigo"
   End If
   If (valFiltrarPor = "Linea De Producto") Then
   valFiltrarPor = "LineaDeProducto"
   End If
   If (valOrdenarPor = "Linea De Producto") Then
   valOrdenarPor = "LineaDeProducto"
   End If
   If (valUsaFiltro And Not (valParaFiltrar = "TODAS")) Then
   SQL = SQL & " AND " & valFiltrarPor & " = " & gUtilSQL.fSimpleSqlValue(valParaFiltrar)
   End If
   SQL = SQL & " Order By "
   If (valUsaFiltro) Then
   SQL = SQL & " " & valFiltrarPor
   If Not (valOrdenarPor = valFiltrarPor) Then
   SQL = SQL & " , " & valOrdenarPor
   End If
   Else
   SQL = SQL & valOrdenarPor
   End If
   fSqlInventarioPorCategoria = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlInventarioPorCategoria", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryCompra(ByVal valConsecutivoCompania As Long, ByVal valConsecutivoAlmacen As Long, ByVal valCodigoArticulo As String, ByVal valTipoArticuloInv As enum_TipoArticuloInv, ByVal valSerial As String, ByVal valRollo As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   If valTipoArticuloInv = eTAI_Simple Or valTipoArticuloInv = eTAI_UsaTalla_Color Then
      SQL = "SELECT ISNULL(SUM(CompraDetalleArticuloInventario.Cantidad) , 0) AS SumCantidad"
      SQL = SQL & " FROM Adm.Compra INNER JOIN Adm.CompraDetalleArticuloInventario"
      SQL = SQL & " ON Compra.Consecutivo = CompraDetalleArticuloInventario.ConsecutivoCompra"
      SQL = SQL & " AND Compra.ConsecutivoCompania = CompraDetalleArticuloInventario.ConsecutivoCompania"
      SQL = SQL & " WHERE Compra.ConsecutivoCompania = " & valConsecutivoCompania
      SQL = SQL & " AND Compra.ConsecutivoAlmacen = " & gUtilSQL.fSimpleSqlValue(valConsecutivoAlmacen)
      SQL = SQL & " AND Compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE)
      SQL = SQL & " AND CompraDetalleArticuloInventario.CodigoArticulo =" & gUtilSQL.fSimpleSqlValue(valCodigoArticulo)
   Else
      SQL = "SELECT ISNULL(SUM(CompraDetalleSerialRollo.Cantidad) , 0) AS SumCantidad"
      SQL = SQL & " FROM Adm.compra INNER JOIN Adm.CompraDetalleSerialRollo "
      SQL = SQL & " ON compra.Consecutivo = CompraDetalleSerialRollo.ConsecutivoCompra "
      SQL = SQL & " AND compra.ConsecutivoCompania = CompraDetalleSerialRollo.ConsecutivoCompania"
      SQL = SQL & " WHERE CompraDetalleSerialRollo.CodigoArticulo =" & gUtilSQL.fSimpleSqlValue(valCodigoArticulo)
      If (valSerial <> "" And valSerial <> "0") Then
         SQL = SQL & " AND CompraDetalleSerialRollo.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
      End If
      If (valRollo <> "" And valRollo <> "0") Then
         SQL = SQL & " AND CompraDetalleSerialRollo.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
      End If
      SQL = SQL & " AND compra.ConsecutivoCompania = " & valConsecutivoCompania
      SQL = SQL & " AND compra.ConsecutivoAlmacen = " & gUtilSQL.fSimpleSqlValue(valConsecutivoAlmacen)
      SQL = SQL & " AND compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE)
   End If
   fQueryCompra = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLSearchArticuloInventarioFacturaSelectSoloServicios(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String, ByVal valPrecio1 As String, ByVal valPrecio2 As String, ByVal valsqlWhere As String, ByVal valMonedaExtranjera As Boolean, ByVal vGroupby As String, ByVal valBuscarPorSerial As Boolean, ByVal valSerial As String, ByVal valDescripcion As String, ByVal valArtResumen As String, ByVal valSqlArtIGTF As String) As String
Dim insArticuloInvVista As clsArticuloInvVista
Dim vSQL As String
Dim vSQLWhere As String
Dim vSQLCondicionTipoArticulo As String
On Error GoTo h_ERROR
Set insArticuloInvVista = New clsArticuloInvVista
 vSQLCondicionTipoArticulo = "ArticuloInventario.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_SERVICIO
 vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLCondicionTipoArticulo, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
 If gTexto.DfLen(valCodigoArticulo) > 0 And valCodigoArticulo <> "*" Then
    vSQLWhere = gUtilSQL.fSQLValueWithAnd(vSQLWhere, "ArticuloInventario.Codigo", valCodigoArticulo, False)
 End If
 vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
 If valBuscarPorSerial Then
    If valSerial <> "" Then
      fSQLSearchArticuloInventarioFacturaSelectSoloServicios = ""
      Exit Function
   End If
End If
 If valDescripcion <> "" Then
   vSQLWhere = vSQLWhere & "And ArticuloInventario.Descripcion LIKE " & gUtilSQL.fSimpleSqlValue(valDescripcion)
 End If
vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   If gTexto.DfLen(valArtResumen) > 0 Then
vSQLWhere = vSQLWhere & "And ArticuloInventario.Codigo NOT IN (" & valArtResumen & ")  "
   End If
   If gTexto.DfLen(valSqlArtIGTF) > 0 Then
      vSQLWhere = vSQLWhere & "And ArticuloInventario.Codigo NOT IN (" & valSqlArtIGTF & ")  "
   End If
vSQL = " SELECT "
vSQL = vSQL & " ArticuloInventario.codigo AS CodigoCompuesto, "
vSQL = vSQL & " ArticuloInventario.descripcion,"
vSQL = vSQL & " ArticuloInventario.LineaDeProducto, "
vSQL = vSQL & " '0' AS Disponibilidad,"
vSQL = vSQL & valPrecio1 & " ,"
vSQL = vSQL & valPrecio2 & " ,"
vSQL = vSQL & " '0' AS Serial,"
vSQL = vSQL & " '0' AS Rollo,"
vSQL = vSQL & " '0' AS CantidadUltimaCompra,"
vSQL = vSQL & " ArticuloInventario.AlicuotaIva,"
vSQL = vSQL & " '0' AS CodigoGrupo,"
vSQL = vSQL & " ArticuloInventario.TipoArticuloInv,"
vSQL = vSQL & " ISNULL(ArticuloInventario.CodigoGrupo,''),"
vSQL = vSQL & " ArticuloInventario.ConsecutivoCompania,"
vSQL = vSQL & " ArticuloInventario.PorcentajeBaseImponible,"
vSQL = vSQL & " '0' AS CodigoAlmacen,"
vSQL = vSQL & " '0' AS Existencia"
vSQL = vSQL & " FROM  ArticuloInventario"
vSQL = vSQL & " LEFT JOIN   CamposMonedaExtranjera ON "
vSQL = vSQL & "ArticuloInventario.ConsecutivoCompania = CamposMonedaExtranjera.ConsecutivoCompania AND "
vSQL = vSQL & "ArticuloInventario.Codigo = CamposMonedaExtranjera.Codigo  "
vSQL = vSQL & vSQLWhere
   Set insArticuloInvVista = Nothing
h_EXIT:    On Error GoTo 0
 fSQLSearchArticuloInventarioFacturaSelectSoloServicios = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchArticuloInventarioFacturaSelectSoloServicios", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDisponibilidadArticuloInventario(ByVal valConsecutivoCompania As String, ByVal valSqlStatus As String, ByVal valRango As Boolean, ByVal valLineaProducto As Boolean, ByVal valCategoria As Boolean, ByVal valMarca As Boolean, valSqlOrder As String, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valStrLineaProducto As String, ByVal valStrCategoria As String, ByVal valStrMarca As String, ByVal valMuestraExistenciaCero As Boolean) As String
   Dim Sql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   SQL = "SELECT "
   SQL = SQL & " articuloInventario.Descripcion as Descripcion, "
   SQL = SQL & " IGV_InventarioConOSinReservas_B1.Existencia as Existencia,"
   SQL = SQL & " (IGV_InventarioConOSinReservas_B1.CantidadReservada) AS CantidadReservada,"
   SQL = SQL & "(IGV_InventarioConOSinReservas_B1.Existencia - IGV_InventarioConOSinReservas_B1.CantidadReservada) AS CantidadDisponible, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, _
                        gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " as StatusdelArticulo "
   SQL = SQL & " , articuloinventario.LineaDeProducto as LineaDeProducto, "
   SQL = SQL & " articuloInventario.categoria, articuloInventario.marca, "
   SQL = SQL & " articuloInventario.TipoArticuloInv, "
   SQL = SQL & " articuloInventario.Codigo as CodigoOriginal, "
   SQL = SQL & " IGV_InventarioConOSinReservas_B1.Serial, "
   SQL = SQL & " IGV_InventarioConOSinReservas_B1.Rollo, "
   SQL = SQL & " IGV_InventarioConOSinReservas_B1.CodigoDelArticulo,"
   SQL = SQL & " IGV_ExistenciaPorGrupoConTallasyColoresMod.DescripcionColor,"
   SQL = SQL & " IGV_ExistenciaPorGrupoConTallasyColoresMod.DescripcionTalla, "
   SQL = SQL & " articuloInventario.Existencia AS ExistenciaMaster ,"
   SQL = SQL & " articuloInventario.CantArtReservado AS CantReservadaMaster,"
   SQL = SQL & " articuloInventario.Existencia - articuloInventario.CantArtReservado AS DisponibilidadMaster "
   
   SQL = SQL & " FROM articuloInventario"
   SQL = SQL & " INNER JOIN IGV_InventarioConOSinReservas_B1"
   SQL = SQL & " ON (articuloInventario.ConsecutivoCompania = IGV_InventarioConOSinReservas_B1.ConsecutivoCompania "
   SQL = SQL & " AND articuloInventario.Codigo = IGV_InventarioConOSinReservas_B1.Codigo)"
   
   SQL = SQL & " LEFT OUTER JOIN "
   SQL = SQL & " (SELECT ConsecutivoCompania,CodigoArticulo,CodigoColor,CodigoTalla,DescripcionColor,DescripcionTalla,Serial,Rollo FROM IGV_ExistenciaPorGrupoConTallasyColores where IGV_ExistenciaPorGrupoConTallasyColores.CodigoTalla <> '') IGV_ExistenciaPorGrupoConTallasyColoresMod"
   SQL = SQL & " ON (IGV_ExistenciaPorGrupoConTallasyColoresMod.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania "
   SQL = SQL & " AND IGV_InventarioConOSinReservas_B1.CodigoDelArticulo = IGV_ExistenciaPorGrupoConTallasyColoresMod.CodigoArticulo " & gUtilSQL.CharConcat & " IGV_ExistenciaPorGrupoConTallasyColoresMod.codigocolor " & gUtilSQL.CharConcat & " IGV_ExistenciaPorGrupoConTallasyColoresMod.CodigoTalla)"
   
   SQL = SQL & " AND " & gUtilSQL.getIIF("articuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial), "IGV_InventarioConOSinReservas_B1.Serial", gUtilSQL.fSimpleSqlValue(""), True)
   SQL = SQL & " = " & gUtilSQL.getIIF("articuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial), "IGV_ExistenciaPorGrupoConTallasyColoresMod.Serial", gUtilSQL.fSimpleSqlValue(""), True)
   SQL = SQL & " AND " & gUtilSQL.getIIF("articuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial), "IGV_InventarioConOSinReservas_B1.Rollo", gUtilSQL.fSimpleSqlValue(""), True)
   SQL = SQL & " = " & gUtilSQL.getIIF("articuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial), "IGV_ExistenciaPorGrupoConTallasyColoresMod.Rollo", gUtilSQL.fSimpleSqlValue(""), True)
   
   SQL = SQL & " WHERE ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND ArticuloInventario.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_MERCANCIA
   
   If valSqlStatus <> "" Then
      SQL = SQL & " AND ArticuloInventario.StatusdelArticulo = " & valSqlStatus
   End If
   If valRango Then
      SQL = SQL & " AND " & gUtilSQL.fSQLStringValueBetween("ArticuloInventario.Codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   End If
   If valLineaProducto Then
      SQL = SQL & " AND ArticuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrLineaProducto))
   End If
   If valCategoria Then
      SQL = SQL & " AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrCategoria))
   End If
   If valMarca Then
      SQL = SQL & " AND ArticuloInventario.Marca = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrMarca))
   End If
   If Not valMuestraExistenciaCero Then
      SQL = SQL & " AND ArticuloInventario.Existencia <> 0 "
   End If
   SQL = SQL & " ORDER BY " & valSqlOrder
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLDisponibilidadArticuloInventario = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDisponibilidadArticuloInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDisponibilidadArticuloInventarioPorAlmacen(ByVal valConsecutivoCompania As String, ByVal valSqlStatus As String, ByVal valRango As Boolean, ByVal valLineaProducto As Boolean, ByVal valCategoria As Boolean, ByVal valMarca As Boolean, valSqlOrder As String, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valStrLineaProducto As String, ByVal valStrCategoria As String, ByVal valStrMarca As String, ByVal valMuestraExistenciaCero As Boolean, ByVal valAlmacen As String) As String
   Dim Sql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
  
   SQL = "SELECT "
   SQL = SQL & " ExistenciaPorAlmacen.CodigoAlmacen,"
   SQL = SQL & " Almacen.NombreAlmacen as DescripcionAlmacen, "
   SQL = SQL & " articuloInventario.Descripcion as Descripcion, "
   SQL = SQL & " ExistenciaPorAlmacen.Cantidad as Existencia, "
   SQL = SQL & " (IGV_InventarioConOSinReservas_B1.CantidadReservada) AS CantidadReservada,"
   SQL = SQL & "(IGV_InventarioConOSinReservas_B1.Existencia - IGV_InventarioConOSinReservas_B1.CantidadReservada) AS CantidadDisponible, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, _
                        gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " as StatusdelArticulo "
   SQL = SQL & " , articuloinventario.LineaDeProducto as LineaDeProducto, "
   SQL = SQL & " articuloInventario.categoria, articuloInventario.marca, "
   SQL = SQL & " articuloInventario.TipoArticuloInv, "
   SQL = SQL & " articuloInventario.Codigo as CodigoOriginal, "
   SQL = SQL & " IGV_InventarioConOSinReservas_B1.Serial, "
   SQL = SQL & " IGV_InventarioConOSinReservas_B1.Rollo, "
   SQL = SQL & " IGV_InventarioConOSinReservas_B1.CodigoDelArticulo,"
   SQL = SQL & " IGV_ExistenciaPorGrupoConTallasyColoresMod.DescripcionColor,"
   SQL = SQL & " IGV_ExistenciaPorGrupoConTallasyColoresMod.DescripcionTalla, "
   SQL = SQL & " articuloInventario.Existencia AS ExistenciaMaster ,"
   SQL = SQL & " articuloInventario.CantArtReservado AS CantReservadaMaster,"
   SQL = SQL & " articuloInventario.Existencia - articuloInventario.CantArtReservado AS DisponibilidadMaster "
   
   SQL = SQL & " FROM articuloInventario"
   SQL = SQL & " INNER JOIN IGV_InventarioConOSinReservas_B1"
   SQL = SQL & " ON (articuloInventario.ConsecutivoCompania = IGV_InventarioConOSinReservas_B1.ConsecutivoCompania "
   SQL = SQL & " AND articuloInventario.Codigo = IGV_InventarioConOSinReservas_B1.Codigo)"
   
   SQL = SQL & " LEFT OUTER JOIN "
   SQL = SQL & " (SELECT ConsecutivoCompania,CodigoArticulo,CodigoColor,CodigoTalla,DescripcionColor,DescripcionTalla FROM IGV_ExistenciaPorGrupoConTallasyColores where IGV_ExistenciaPorGrupoConTallasyColores.CodigoTalla <> '') IGV_ExistenciaPorGrupoConTallasyColoresMod"
   SQL = SQL & " ON (IGV_ExistenciaPorGrupoConTallasyColoresMod.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania "
   SQL = SQL & " AND IGV_InventarioConOSinReservas_B1.CodigoDelArticulo = IGV_ExistenciaPorGrupoConTallasyColoresMod.CodigoArticulo " & gUtilSQL.CharConcat & " IGV_ExistenciaPorGrupoConTallasyColoresMod.codigocolor " & gUtilSQL.CharConcat & " IGV_ExistenciaPorGrupoConTallasyColoresMod.CodigoTalla)"
  
   SQL = SQL & " INNER JOIN ExistenciaPorAlmacen ON"
   SQL = SQL & " (IGV_InventarioConOSinReservas_B1.ConsecutivoCompania = ExistenciaPorAlmacen.ConsecutivoCompania  "
   SQL = SQL & " AND IGV_InventarioConOSinReservas_B1.CodigoDelArticulo = ExistenciaPorAlmacen.CodigoArticulo)"
   
   SQL = SQL & " INNER JOIN Almacen ON"
   SQL = SQL & " (ExistenciaPorAlmacen.ConsecutivoCompania = Almacen.ConsecutivoCompania"
   SQL = SQL & " AND ExistenciaPorAlmacen.CodigoAlmacen = Almacen.Codigo)"
   
   SQL = SQL & " WHERE ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND ArticuloInventario.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_MERCANCIA
   SQL = SQL & " AND TipoArticuloInv IN ( " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Simple) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_Color) & ")  "
   
   If valSqlStatus <> "" Then
      SQL = SQL & " AND ArticuloInventario.StatusdelArticulo = " & valSqlStatus
   End If
   If valRango Then
      SQL = SQL & " AND " & gUtilSQL.fSQLStringValueBetween("ArticuloInventario.Codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   End If
   If valLineaProducto Then
      SQL = SQL & " AND ArticuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrLineaProducto))
   End If
   If valCategoria Then
      SQL = SQL & " AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrCategoria))
   End If
   If valMarca Then
      SQL = SQL & " AND ArticuloInventario.Marca = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrMarca))
   End If
   If valAlmacen <> "" Then
      SQL = SQL & " AND Almacen.Codigo = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valAlmacen))
   End If
   If Not valMuestraExistenciaCero Then
      SQL = SQL & " AND ArticuloInventario.Existencia <> 0 "
   End If
   
   '---------------------------------------
   SQL = SQL & " UNION "
   '---------------------------------------
   SQL = SQL & " SELECT "
   SQL = SQL & " RenglonExistenciaAlmacen.CodigoAlmacen,"
   SQL = SQL & " Almacen.NombreAlmacen as DescripcionAlmacen, "
   SQL = SQL & " articuloInventario.Descripcion as Descripcion, "
   SQL = SQL & " RenglonExistenciaAlmacen.Cantidad as Existencia,"
   SQL = SQL & " (IGV_InventarioConOSinReservas_B1.CantidadReservada) AS CantidadReservada,"
   SQL = SQL & "(IGV_InventarioConOSinReservas_B1.Existencia - IGV_InventarioConOSinReservas_B1.CantidadReservada) AS CantidadDisponible, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, _
                        gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " as StatusdelArticulo "
   SQL = SQL & " , articuloinventario.LineaDeProducto as LineaDeProducto, "
   SQL = SQL & " articuloInventario.categoria, articuloInventario.marca, "
   SQL = SQL & " articuloInventario.TipoArticuloInv, "
   SQL = SQL & " articuloInventario.Codigo as CodigoOriginal, "
   SQL = SQL & " IGV_InventarioConOSinReservas_B1.Serial, "
   SQL = SQL & " IGV_InventarioConOSinReservas_B1.Rollo, "
   SQL = SQL & " IGV_InventarioConOSinReservas_B1.CodigoDelArticulo,"
   SQL = SQL & " IGV_ExistenciaPorGrupoConTallasyColoresMod.DescripcionColor,"
   SQL = SQL & " IGV_ExistenciaPorGrupoConTallasyColoresMod.DescripcionTalla, "
   SQL = SQL & " articuloInventario.Existencia AS ExistenciaMaster ,"
   SQL = SQL & " articuloInventario.CantArtReservado AS CantReservadaMaster,"
   SQL = SQL & " articuloInventario.Existencia - articuloInventario.CantArtReservado AS DisponibilidadMaster "
   
   SQL = SQL & " FROM articuloInventario"
   SQL = SQL & " INNER JOIN IGV_InventarioConOSinReservas_B1"
   SQL = SQL & " ON (articuloInventario.ConsecutivoCompania = IGV_InventarioConOSinReservas_B1.ConsecutivoCompania "
   SQL = SQL & " AND articuloInventario.Codigo = IGV_InventarioConOSinReservas_B1.Codigo)"
   
   SQL = SQL & " LEFT OUTER JOIN "
   SQL = SQL & " (SELECT ConsecutivoCompania,CodigoArticulo,CodigoColor,CodigoTalla,DescripcionColor,DescripcionTalla,Serial,Rollo FROM IGV_ExistenciaPorGrupoConTallasyColores where IGV_ExistenciaPorGrupoConTallasyColores.CodigoTalla <> '') IGV_ExistenciaPorGrupoConTallasyColoresMod"
   SQL = SQL & " ON (IGV_ExistenciaPorGrupoConTallasyColoresMod.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania "
   SQL = SQL & " AND IGV_InventarioConOSinReservas_B1.CodigoDelArticulo = IGV_ExistenciaPorGrupoConTallasyColoresMod.CodigoArticulo " & gUtilSQL.CharConcat & " IGV_ExistenciaPorGrupoConTallasyColoresMod.codigocolor " & gUtilSQL.CharConcat & " IGV_ExistenciaPorGrupoConTallasyColoresMod.CodigoTalla"
   SQL = SQL & " AND IGV_InventarioConOSinReservas_B1.Serial = IGV_ExistenciaPorGrupoConTallasyColoresMod.Serial"
   SQL = SQL & " AND IGV_InventarioConOSinReservas_B1.Rollo = IGV_ExistenciaPorGrupoConTallasyColoresMod.Rollo)"
   SQL = SQL & " INNER JOIN RenglonExistenciaAlmacen ON"
   SQL = SQL & " (IGV_InventarioConOSinReservas_B1.ConsecutivoCompania = RenglonExistenciaAlmacen.ConsecutivoCompania  "
   SQL = SQL & " AND IGV_InventarioConOSinReservas_B1.CodigoDelArticulo = RenglonExistenciaAlmacen.CodigoArticulo"
   SQL = SQL & " AND IGV_InventarioConOSinReservas_B1.Serial = RenglonExistenciaAlmacen.CodigoSerial"
   SQL = SQL & " AND IGV_InventarioConOSinReservas_B1.Rollo = RenglonExistenciaAlmacen.CodigoRollo)"
      
   SQL = SQL & " INNER JOIN Almacen ON"
   SQL = SQL & " (RenglonExistenciaAlmacen.ConsecutivoCompania = Almacen.ConsecutivoCompania"
   SQL = SQL & " AND RenglonExistenciaAlmacen.CodigoAlmacen = Almacen.Codigo)"
      
   SQL = SQL & " WHERE ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND ArticuloInventario.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_MERCANCIA
   SQL = SQL & " AND TipoArticuloInv IN ( " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaSerial) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaSerial_Rollo) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial) & ")  "
   
   If valSqlStatus <> "" Then
      SQL = SQL & " AND ArticuloInventario.StatusdelArticulo = " & valSqlStatus
   End If
   If valRango Then
      SQL = SQL & " AND " & gUtilSQL.fSQLStringValueBetween("ArticuloInventario.Codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   End If
   If valLineaProducto Then
      SQL = SQL & " AND ArticuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrLineaProducto))
   End If
   If valCategoria Then
      SQL = SQL & " AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrCategoria))
   End If
   If valMarca Then
      SQL = SQL & " AND ArticuloInventario.Marca = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrMarca))
   End If
   If valAlmacen <> "" Then
      SQL = SQL & " AND Almacen.Codigo = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valAlmacen))
   End If
   If Not valMuestraExistenciaCero Then
      SQL = SQL & " AND ArticuloInventario.Existencia <> 0 "
   End If
   
   SQL = SQL & " ORDER BY " & valSqlOrder
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLDisponibilidadArticuloInventarioPorAlmacen = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDisponibilidadArticuloInventarioPorAlmacen", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlAlertaMaximoMinimo(ByVal valVerificaMaximo As Boolean, ByVal valCodigoArticulo As String, ByVal valCantidad As Currency, ByVal valConsecutivoCompaniaActual As Long) As String
   Dim vSQLWhere As String
   Dim vSQL As String
   Dim vCondicion As String
   On Error GoTo h_ERROR
   If (valVerificaMaximo) Then
      vCondicion = "( ( ArticuloInventario.Existencia   + " & gUtilSQL.fSimpleSqlValueForNumeric(valCantidad) & " ) >  ArticuloInventario.CantidadMaxima) "
   Else
      vCondicion = "( ( ArticuloInventario.Existencia   - " & gUtilSQL.fSimpleSqlValueForNumeric(valCantidad) & " ) <  ArticuloInventario.CantidadMinima) "
   End If
   vCondicion = gUtilSQL.getIIF(vCondicion, gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.fSimpleSqlValue("N"), False)
   vSQLWhere = gUtilSQL.fSQLValueWithAnd("", "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo", valCodigoArticulo, False)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompaniaActual)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   vSQL = " SELECT "
   vSQL = vSQL & vCondicion & "AS Alertar "
   vSQL = vSQL & " FROM dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1 "
   vSQL = vSQL & " INNER JOIN   dbo.ArticuloInventario ON"
   vSQL = vSQL & " dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo = dbo.ArticuloInventario.Codigo AND"
   vSQL = vSQL & " dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.consecutivoCompania = dbo.ArticuloInventario.consecutivoCompania"
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & " GROUP BY  dbo.ArticuloInventario.Codigo,dbo.ArticuloInventario.Existencia,CantidadMinima,CantidadMaxima "
   fSqlAlertaMaximoMinimo = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSqlAlertaMaximoMinimo = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSearchForListRecords", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function

Public Function fConstruirSQLDelReporteGananciaXProductoSegunLeyCosto(ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, _
                                                            ByVal valenumCantidadAImprimirToString As String, ByVal valLineaDeProductos As String, ByVal valPrecioVenta As String, ByVal valCostoPromedio As Boolean) As String
   Dim SQL As String
   Dim vPrecioSinIva As String
   Dim vPrecioSinIva2 As String
   Dim vNivel As String
   Dim vCosto As String
   On Error GoTo h_ERROR
   vNivel = Mid(valPrecioVenta, InStr(1, valPrecioVenta, " ") + 1, 1)
   If vNivel = "1" Then
      vNivel = ""
   End If
   If valCostoPromedio Then
      vCosto = "COSTOPROMEDIO"
   Else
      vCosto = "COSTOUNITARIO"
   End If
   SQL = "SELECT "
   SQL = SQL & "CODIGO , "
   SQL = SQL & "DESCRIPCION , "
   SQL = SQL & "PRECIOSINIVA" + vNivel + " as PRECIOSINIVA , "
   SQL = SQL & " " + vCosto + " as COSTOUNITARIO, "
   SQL = SQL & "LINEADEPRODUCTO ,"
   SQL = SQL & "GastosAdmisibles as porcGastosAdmisibles,"
   SQL = SQL & "MargenGananciaXLey" + vNivel + ","
   SQL = SQL & gUtilSQL.getIIF("" + vCosto + "  <> 0 ", _
                  "(((PRECIOSINIVA" + vNivel + " " & _
                  " - " + vCosto + " " & _
                  ") / " + vCosto + " ) * 100)", _
                  "0", True) & _
               " AS Porcentaje,"
               
   SQL = SQL & gUtilSQL.getIIF("" + vCosto + "  <> 0 ", _
                  "( " + vCosto + " * GastosAdmisibles / 100 )", _
                  "0", True) & _
               " AS GastosAdmisibles,"
               
   SQL = SQL & gUtilSQL.getIIF("" + vCosto + "  <> 0 ", _
                  "( " + vCosto + " + (" + vCosto + " * GastosAdmisibles / 100 ) )", _
                  "0", True) & _
               " AS CostoSegunLey ,"
               
   SQL = SQL & "(PRECIOSINIVA -  " & _
               gUtilSQL.getIIF("" + vCosto + "  <> 0 ", _
                  "( " + vCosto + " + (" + vCosto + " * GastosAdmisibles / 100 ) )", _
                  "0", True) & _
               ") AS Ganancia "
               
   SQL = SQL & " FROM articuloInventario"
   SQL = SQL & " WHERE "
   SQL = SQL & "CONSECUTIVOCOMPANIA   = " & valConsecutivoCompania
   SQL = SQL & " AND TIPODEARTICULO " _
            & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
       SQL = SQL & " AND LINEADEPRODUCTO " _
            & " = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & "LINEADEPRODUCTO , "
   SQL = SQL & "CODIGO"
 
   fConstruirSQLDelReporteGananciaXProductoSegunLeyCosto = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteGananciaXProductoSegunLeyCosto", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLInsertarArticuloEspecialME(ByVal valConsecutivoCompania As Long, ByVal valCodigo As String, ByVal valDescripcin As String, ByVal valAlicuota As enum_TipoDeAlicuota, ByVal valSqlValueNombreOperador As String, ByVal valSqlValueFechaUltModificacion As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "INSERT INTO camposMonedaExtranjera(ConsecutivoCompania, Codigo, "
   SQL = SQL & "MePrecioSinIva, MePrecioConIva, "
   SQL = SQL & "MePrecioSinIva2, MePrecioConIva2, "
   SQL = SQL & "MePrecioSinIva3, MePrecioConIva3, "
   SQL = SQL & "MePrecioSinIva4, MePrecioConIva4, "
   SQL = SQL & "NombreOperador, FechaUltimaModificacion"
   SQL = SQL & ") SELECT " & valConsecutivoCompania & ", " & gUtilSQL.fSimpleSqlValue(valCodigo)
   SQL = SQL & ",0,0,0,0,0,0,0,0,"
   SQL = SQL & valSqlValueNombreOperador & ", " & valSqlValueFechaUltModificacion
   SQL = SQL & " WHERE NOT EXISTS (SELECT ConsecutivoCompania, Codigo FROM camposMonedaExtranjera WHERE ConsecutivoCompania = " & valConsecutivoCompania & " AND Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigo) & ")"
h_EXIT: On Error GoTo 0
   fSQLInsertarArticuloEspecialME = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLInsertarArticuloEspecialME", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLInsertarSinoExisteLineaDeProducto(ByVal valSqlValueNombreOperador As String, ByVal valSqlValueFechaUltModificacion As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "INSERT INTO Adm.LineaDeProducto (ConsecutivoCompania, Consecutivo, Nombre, PorcentajeComision,CentroDeCosto, NombreOperador, FechaUltimaModificacion) "
   SQL = SQL & "SELECT ConsecutivoCompania, ISNULL((SELECT MAX(Consecutivo) From Adm.LineaDeProducto), 0) + 1, 'LINEA DE PRODUCTO', 0, '', " & valSqlValueNombreOperador & ", "
   SQL = SQL & valSqlValueFechaUltModificacion & " FROM COMPANIA "
   SQL = SQL & " WHERE NOT EXISTS(SELECT Nombre FROM Adm.LineaDeProducto WHERE ConsecutivoCompania = COMPANIA.ConsecutivoCompania AND Nombre = 'LINEA DE PRODUCTO')"
h_EXIT: On Error GoTo 0
   fSQLInsertarSinoExisteLineaDeProducto = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLInsertarSinoExisteLineaDeProducto", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryNotaESSaldoInicial(ByVal valTipoOperacion As enum_TipodeOperacion, ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, _
               ByVal valCodigoCompuesto As String, ByVal valCodigoDesde As String, ByVal valSerial As String, ByVal valRollo As String, ByVal valCodigoHasta As String, ByVal valCodigoAlmacen As String, ByVal valEsSalida As Boolean) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT RenglonNotaES.CodigoArticulo AS CodigoArticulo,"
   If valEsSalida Then
      Sql = Sql & " -1 * (ISNULL(SUM(RenglonNotaES.Cantidad), 0)) AS SumCantidad,"
   Else
      Sql = Sql & " ISNULL(SUM(RenglonNotaES.Cantidad), 0) AS SumCantidad,"
   End If
   Sql = Sql & " notaDeEntradaSalida.CodigoAlmacen AS CodigoAlmacen"
   Sql = Sql & " FROM notaDeEntradaSalida INNER JOIN renglonNotaES "
   Sql = Sql & " ON notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento"
   Sql = Sql & " AND notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania"
   Sql = Sql & " INNER JOIN articuloInventario  ON articuloInventario.Codigo = renglonNotaES.CodigoArticulo "
   Sql = Sql & " AND articuloInventario.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania"
   Sql = Sql & " INNER JOIN almacen ON notaDeEntradaSalida.CodigoAlmacen = almacen.Codigo AND notaDeEntradaSalida.ConsecutivoCompania = almacen.ConsecutivoCompania"
   Sql = Sql & " WHERE notaDeEntradaSalida.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND notaDeEntradaSalida.StatusNotaEntradaSalida = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusNotaEntradaSalida.eSNES_Vigente)
   Sql = Sql & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "NotaDeEntradaSalida.Fecha", valFechaInicial, "<") & ")"
   Sql = Sql & " AND notaDeEntradaSalida.TipodeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(valTipoOperacion)
   If valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
      Sql = Sql & " AND renglonNotaES.CodigoArticulo =" & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
      If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND RenglonNotaES.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            Sql = Sql & " AND renglonNotaES.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   Else
      Sql = Sql & " AND " & gUtilSQL.fSQLStringValueBetween("renglonNotaES.CodigoArticulo", Trim(valCodigoDesde), Trim(valCodigoHasta)) & ""
      If valTipoArticuloInvAsEnum = eTAI_UsaSerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND renglonNotaES.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
      ElseIf valTipoArticuloInvAsEnum = eTAI_UsaSerial_Rollo Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND RenglonNotaES.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            Sql = Sql & " AND renglonNotaES.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   End If
   Sql = Sql & " AND almacen.Codigo LIKE " & gUtilSQL.getIIF(gUtilSQL.fSimpleSqlValue(valCodigoAlmacen) & " <> " & gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(valCodigoAlmacen), gUtilSQL.fSimpleSqlValue("%"))
   Sql = Sql & " GROUP BY CodigoArticulo, CodigoAlmacen "
   fQueryNotaESSaldoInicial = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryNotaESSaldoInicial", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryTransferenciaSaldoInicial(ByVal valTipoOperacion As enum_TipodeOperacion, ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, _
                  ByVal valCodigoCompuesto As String, ByVal valCodigoDesde As String, ByVal valSerial As String, ByVal valRollo As String, ByVal valCodigoHasta As String, ByVal valCodigoAlmacen As String, ByVal valEsSalida As Boolean) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT RenglonTransferencia.CodigoArticulo AS CodigoArticulo,"
   If valEsSalida Then
      Sql = Sql & " -1 * (ISNULL(SUM(RenglonTransferencia.Cantidad), 0)) AS SumCantidad,"
   Else
      Sql = Sql & " ISNULL(SUM(RenglonTransferencia.Cantidad), 0) AS SumCantidad,"
   End If
   If valTipoOperacion = eTO_ENTRADA_DE_INVENTARIO Then
      Sql = Sql & " Transferencia.CodigoAlmacenEntrada AS CodigoAlmacen"
   ElseIf valTipoOperacion = eTO_SALIDA_DE_INVENTARIO Then
      Sql = Sql & " Transferencia.CodigoAlmacenSalida AS CodigoAlmacen"
   End If
   Sql = Sql & " FROM Transferencia INNER JOIN RenglonTransferencia "
   Sql = Sql & " ON Transferencia.NumeroDocumento = RenglonTransferencia.NumeroDocumento"
   Sql = Sql & " AND Transferencia.ConsecutivoCompania = RenglonTransferencia.ConsecutivoCompania"
   If valTipoOperacion = eTO_ENTRADA_DE_INVENTARIO Then
      Sql = Sql & " INNER JOIN almacen ON Transferencia.CodigoAlmacenEntrada = almacen.Codigo AND Transferencia.ConsecutivoCompania = almacen.ConsecutivoCompania"
   ElseIf valTipoOperacion = eTO_SALIDA_DE_INVENTARIO Then
      Sql = Sql & " INNER JOIN almacen ON Transferencia.CodigoAlmacenSalida = almacen.Codigo AND Transferencia.ConsecutivoCompania = almacen.ConsecutivoCompania"
   End If
   Sql = Sql & " WHERE Transferencia.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "Transferencia.Fecha", valFechaInicial, "<") & ")"
   If valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
      Sql = Sql & " AND RenglonTransferencia.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
      If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND RenglonTransferencia.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            Sql = Sql & " AND RenglonTransferencia.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   Else
      Sql = Sql & " AND " & gUtilSQL.fSQLStringValueBetween("RenglonTransferencia.CodigoArticulo", Trim(valCodigoDesde), Trim(valCodigoHasta)) & ""
      If valTipoArticuloInvAsEnum = eTAI_UsaSerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND RenglonTransferencia.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
      ElseIf valTipoArticuloInvAsEnum = eTAI_UsaSerial_Rollo Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND RenglonTransferencia.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            Sql = Sql & " AND RenglonTransferencia.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   End If
   Sql = Sql & " AND almacen.Codigo LIKE " & gUtilSQL.getIIF(gUtilSQL.fSimpleSqlValue(valCodigoAlmacen) & " <> " & gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(valCodigoAlmacen), gUtilSQL.fSimpleSqlValue("%"))
   If valTipoOperacion = eTO_ENTRADA_DE_INVENTARIO Then
      Sql = Sql & " GROUP BY CodigoArticulo, CodigoAlmacenEntrada"
   ElseIf valTipoOperacion = eTO_SALIDA_DE_INVENTARIO Then
      Sql = Sql & " GROUP BY CodigoArticulo, CodigoAlmacenSalida"
   End If
   fQueryTransferenciaSaldoInicial = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryTransferenciaSaldoInicial", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryFacturaSaldoInicial(ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, ByVal valCodigoCompuesto As String, _
                  ByVal valCodigoDesde As String, ByVal valSerial As String, ByVal valRollo As String, ByVal valCodigoHasta As String, ByVal valCodigoAlmacen As String, ByVal valEsSalida As Boolean) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT renglonFactura.Articulo AS CodigoArticulo, "
   If valEsSalida Then
      Sql = Sql & " -1 * (ISNULL(SUM(Renglonfactura.Cantidad), 0)) AS SumCantidad,"
   Else
      Sql = Sql & " ISNULL(SUM(Renglonfactura.Cantidad), 0) AS SumCantidad,"
   End If
   Sql = Sql & " factura.CodigoAlmacen AS CodigoAlmacen "
   Sql = Sql & " FROM factura INNER JOIN renglonFactura "
   Sql = Sql & " ON factura.Numero = renglonFactura.NumeroFactura "
   Sql = Sql & " AND factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND factura.TipoDeDocumento = renglonFactura.TipoDeDocumento "
   Sql = Sql & " INNER JOIN almacen ON factura.ConsecutivoCompania = almacen.ConsecutivoCompania AND factura.CodigoAlmacen = almacen.Codigo"
   Sql = Sql & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "factura.Fecha", valFechaInicial, "<") & ")"
   Sql = Sql & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(eSF_EMITIDA)
   Sql = Sql & " AND factura.GeneradaPorNotaEntrega = " & gUtilSQL.fSQLSimpleValueForEnum(enum_FacturaGeneraNotaEntrega.eFGNE_NoGenerada)
   If valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
      Sql = Sql & " AND renglonFactura.Articulo = " & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
      If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND Renglonfactura.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            Sql = Sql & " AND renglonfactura.rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   Else
      Sql = Sql & " AND " & gUtilSQL.fSQLStringValueBetween("renglonFactura.Articulo", Trim(valCodigoDesde), Trim(valCodigoHasta))
      If valTipoArticuloInvAsEnum = eTAI_UsaSerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND Renglonfactura.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
      ElseIf valTipoArticuloInvAsEnum = eTAI_UsaSerial_Rollo Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND Renglonfactura.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            Sql = Sql & " AND Renglonfactura.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   End If
   Sql = Sql & " AND almacen.Codigo LIKE " & gUtilSQL.getIIF(gUtilSQL.fSimpleSqlValue(valCodigoAlmacen) & " <> " & gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(valCodigoAlmacen), gUtilSQL.fSimpleSqlValue("%"))
   Sql = Sql & " GROUP BY Articulo, CodigoAlmacen"
   fQueryFacturaSaldoInicial = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryFacturaSaldoInicial", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryFacturaPCSaldoInicial(ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, ByVal valCodigoCompuesto As String, _
                  ByVal valCodigoDesde As String, ByVal valSerial As String, ByVal valRollo As String, ByVal valCodigoHasta As String, ByVal valCodigoAlmacen As String, ByVal valEsSalida As Boolean) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT renglonFactura.Articulo AS CodigoArticulo, "
   If valEsSalida Then
      Sql = Sql & " -1 * (ISNULL(SUM(Renglonfactura.Cantidad*productoCompuesto.cantidad), 0)) AS SumCantidad,"
   Else
      Sql = Sql & " ISNULL(SUM(Renglonfactura.Cantidad*productoCompuesto.cantidad), 0) AS SumCantidad,"
   End If
   Sql = Sql & " factura.CodigoAlmacen AS CodigoAlmacen"
   Sql = Sql & " FROM ProductoCompuesto INNER JOIN ArticuloInventario"
   Sql = Sql & " ON ProductoCompuesto.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND ProductoCompuesto.CodigoConexionConElMaster = ArticuloInventario.Codigo"
   Sql = Sql & " INNER JOIN renglonFactura ON ArticuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania"
   Sql = Sql & " AND ArticuloInventario.Codigo = renglonFactura.Articulo"
   Sql = Sql & " INNER JOIN factura ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND factura.Numero = renglonFactura.NumeroFactura"
   Sql = Sql & " AND factura.TipoDeDocumento = renglonFactura.TipoDeDocumento"
   Sql = Sql & " INNER JOIN almacen ON ArticuloInventario.ConsecutivoCompania = almacen.ConsecutivoCompania"
   Sql = Sql & " AND ArticuloInventario.Codigo = almacen.Codigo"
   Sql = Sql & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "factura.Fecha", valFechaInicial, "<") & ")"
   Sql = Sql & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(eSF_EMITIDA)
   If valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
      Sql = Sql & " AND productoCompuesto.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
      If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND Renglonfactura.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            Sql = Sql & " AND renglonfactura.rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   Else
      Sql = Sql & " AND " & gUtilSQL.fSQLStringValueBetween("productoCompuesto.CodigoArticulo", Trim(valCodigoDesde), Trim(valCodigoHasta))
      If valTipoArticuloInvAsEnum = eTAI_UsaSerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND Renglonfactura.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
      ElseIf valTipoArticuloInvAsEnum = eTAI_UsaSerial_Rollo Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND Renglonfactura.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            Sql = Sql & " AND Renglonfactura.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   End If
   Sql = Sql & " AND almacen.Codigo LIKE " & gUtilSQL.getIIF(gUtilSQL.fSimpleSqlValue(valCodigoAlmacen) & " <> " & gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(valCodigoAlmacen), gUtilSQL.fSimpleSqlValue("%"))
   Sql = Sql & " GROUP BY Articulo, CodigoAlmacen"
   fQueryFacturaPCSaldoInicial = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryFacturaPCSaldoInicial", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryCompraSaldoInicial(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valCodigoDesde As String, ByVal valTipoArticuloInv As enum_TipoArticuloInv, ByVal valSerial As String, ByVal valRollo As String, ByVal valCodigoHasta As String, ByVal valCodigoAlmacen As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   If valTipoArticuloInv = eTAI_Simple Or valTipoArticuloInv = eTAI_UsaTalla_Color Then
      Sql = "SELECT CompraDetalleArticuloInventario.CodigoArticulo AS CodigoArticulo, ISNULL(SUM(CompraDetalleArticuloInventario.Cantidad) , 0) AS SumCantidad, Compra.CodigoAlmacen AS CodigoAlmacen"
      Sql = Sql & " FROM compra INNER JOIN Adm.CompraDetalleArticuloInventario"
      Sql = Sql & " ON compra.Consecutivo = CompraDetalleArticuloInventario.ConsecutivoCompra AND compra.ConsecutivoCompania = CompraDetalleArticuloInventario.ConsecutivoCompania"
      Sql = Sql & " INNER JOIN Almacen ON Almacen.Codigo = compra.CodigoAlmacen AND Almacen.ConsecutivoCompania = compra.ConsecutivoCompania"
      Sql = Sql & " WHERE " & gUtilSQL.fSQLStringValueBetween("CompraDetalleArticuloInventario.CodigoArticulo", Trim(valCodigoDesde), Trim(valCodigoHasta))
   Else
      Sql = "SELECT CompraDetalleSerialRollo.CodigoArticulo AS CodigoArticulo, ISNULL(SUM(CompraDetalleSerialRollo.Cantidad) , 0) AS SumCantidad, Compra.CodigoAlmacen AS CodigoAlmacen"
      Sql = Sql & " FROM compra INNER JOIN Adm.CompraDetalleSerialRollo "
      Sql = Sql & " ON compra.Consecutivo = CompraDetalleSerialRollo.ConsecutivoCompra AND compra.ConsecutivoCompania = CompraDetalleSerialRollo.ConsecutivoCompania"
      Sql = Sql & " INNER JOIN Almacen ON Almacen.Codigo = compra.CodigoAlmacen AND Almacen.ConsecutivoCompania = compra.ConsecutivoCompania"
      Sql = Sql & " WHERE " & gUtilSQL.fSQLStringValueBetween("CompraDetalleSerialRollo.CodigoArticulo", Trim(valCodigoDesde), Trim(valCodigoHasta))
      If (valSerial <> "" And valSerial <> "0") Then
         Sql = Sql & " AND CompraDetalleSerialRollo.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
      End If
      If (valRollo <> "" And valRollo <> "0") Then
         Sql = Sql & " AND CompraDetalleSerialRollo.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
      End If
   End If
   Sql = Sql & " AND Compra.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValueDateWithComparisonRule("", "Compra.Fecha", valFechaInicial, "<") & ""
   Sql = Sql & " AND Compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE)
   Sql = Sql & " AND almacen.Codigo LIKE " & gUtilSQL.getIIF(gUtilSQL.fSimpleSqlValue(valCodigoAlmacen) & " <> " & gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(valCodigoAlmacen), gUtilSQL.fSimpleSqlValue("%"))
   Sql = Sql & " GROUP BY CodigoArticulo, CodigoAlmacen"
   fQueryCompraSaldoInicial = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryCompraSaldoInicial", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryConteoFisicoSaldoInicial(ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, ByVal valCodigoCompuesto As String, ByVal valCodigoDesde As String, ByVal valSerial As String, ByVal valRollo As String, ByVal valCodigoHasta As String, ByVal valCodigoAlmacen As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT RenglonConteoFisico.CodigoArticulo AS CodigoArticulo, ISNULL(SUM(Diferencia) ,0) AS SumCantidad, ConteoFisico.CodigoAlmacen AS CodigoAlmacen"
   Sql = Sql & " FROM ConteoFisico INNER JOIN RenglonConteoFisico "
   Sql = Sql & " ON ConteoFisico.ConsecutivoConteo = RenglonConteoFisico.ConsecutivoConteo"
   Sql = Sql & " AND ConteoFisico.ConsecutivoCompania = RenglonConteoFisico.ConsecutivoCompania"
   Sql = Sql & " INNER JOIN Almacen ON Almacen.Codigo = ConteoFisico.CodigoAlmacen AND Almacen.ConsecutivoCompania = ConteoFisico.ConsecutivoCompania"
   Sql = Sql & " WHERE ConteoFisico.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "ConteoFisico.Fecha", valFechaInicial, "<") & ")"
   Sql = Sql & " AND ConteoFisico.Status = " & gUtilSQL.fSQLSimpleValueForEnum(eSC_PROCESADO)
   If valTipoArticuloInvAsEnum = eTAI_UsaTalla_Color Or valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
      Sql = Sql & " AND RenglonConteoFisico.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigoCompuesto)
      If valTipoArticuloInvAsEnum = eTAI_UsaTalla_ColorySerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND RenglonConteoFisico.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            Sql = Sql & " AND RenglonConteoFisico.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   Else
      Sql = Sql & " AND " & gUtilSQL.fSQLStringValueBetween("RenglonConteoFisico.CodigoArticulo", Trim(valCodigoDesde), Trim(valCodigoHasta))
      If valTipoArticuloInvAsEnum = eTAI_UsaSerial Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND RenglonConteoFisico.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
      ElseIf valTipoArticuloInvAsEnum = eTAI_UsaSerial_Rollo Then
         If (valSerial <> "" And valSerial <> "0") Then
            Sql = Sql & " AND RenglonConteoFisico.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
         End If
         If (valRollo <> "" And valRollo <> "0") Then
            Sql = Sql & " AND RenglonConteoFisico.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
         End If
      End If
   End If
   Sql = Sql & " AND almacen.Codigo LIKE " & gUtilSQL.getIIF(gUtilSQL.fSimpleSqlValue(valCodigoAlmacen) & " <> " & gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue(valCodigoAlmacen), gUtilSQL.fSimpleSqlValue("%"))
   Sql = Sql & " GROUP BY CodigoArticulo, CodigoAlmacen"
h_EXIT: On Error GoTo 0
   fQueryConteoFisicoSaldoInicial = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryConteoFisicoSaldoInicial", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
