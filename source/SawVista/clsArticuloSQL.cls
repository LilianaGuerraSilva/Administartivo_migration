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
   Dim Sql As String
   Dim sqlTipoDeArticulo As String
   Dim preciosSinIva As Boolean
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   preciosSinIva = (valPreciosConSinIva = valenumPreciosDeLosProductosToString)
   sqlTipoDeArticulo = gUtilSQL.DfSQLCaseIfForEnum("articuloInventario.TIPODEARTICULO", enum_TipoDeArticulo.eTD_MERCANCIA, gEnumProyecto.fEnumTipoDeArticuloToStringInArray(True), "TipoDeArticuloStr")
   Sql = "SELECT "
   Sql = Sql & "articuloInventario.CODIGO, "
   Sql = Sql & "articuloInventario.DESCRIPCION , "
   Sql = Sql & "articuloInventario.PORCENTAJEBASEIMPONIBLE , "
   Sql = Sql & "articuloInventario.CAMPODEFINIBLE1 , "
   Sql = Sql & "articuloInventario.CAMPODEFINIBLE2 , "
   Sql = Sql & "articuloInventario.CAMPODEFINIBLE3 , "
   Sql = Sql & "articuloInventario.CAMPODEFINIBLE4 , "
   Sql = Sql & "articuloInventario.CAMPODEFINIBLE5 , "
   Sql = Sql & "articuloInventario.LINEADEPRODUCTO , "
   Sql = Sql & "articuloInventario.TIPODEARTICULO , "
   Sql = Sql & sqlTipoDeArticulo & ", "
   Sql = Sql & "articuloInventario.COSTOUNITARIO , "
   If preciosSinIva Then
      Sql = Sql & "articuloInventario.PRECIOSINIVA AS Precio, "
      Sql = Sql & "articuloInventario.PRECIOSINIVA2 AS Precio2, "
      Sql = Sql & "articuloInventario.PRECIOSINIVA3 AS Precio3, "
      Sql = Sql & "articuloInventario.PRECIOSINIVA4 AS Precio4 , "
   Else
      Sql = Sql & "articuloInventario.PRECIOCONIVA AS Precio, "
      Sql = Sql & "articuloInventario.PRECIOCONIVA2 AS Precio2, "
      Sql = Sql & "articuloInventario.PRECIOCONIVA3 AS Precio3, "
      Sql = Sql & "articuloInventario.PRECIOCONIVA4 AS Precio4, "
   End If
   If valImprimirDetalles Then
      Sql = Sql & "productoCompuesto.CodigoArticulo, "
      Sql = Sql & "ArtInventario.DESCRIPCION AS DescripcionProdCompuesto, "
      Sql = Sql & "productoCompuesto.Cantidad, "
   End If
   If valUsaMonedaExtranjera Then
      Sql = Sql & "articuloInventario.MECOSTOUNITARIO , "
      If preciosSinIva Then
         Sql = Sql & "camposMonedaExtranjera.MEPRECIOSINIVA  AS MePrecio, "
         Sql = Sql & "camposMonedaExtranjera.MEPRECIOSINIVA2 AS MePrecio2, "
         Sql = Sql & "camposMonedaExtranjera.MEPRECIOSINIVA3 AS MePrecio3, "
         Sql = Sql & "camposMonedaExtranjera.MEPRECIOSINIVA4 AS MePrecio4, "
      Else
         Sql = Sql & "camposMonedaExtranjera.MEPRECIOCONIVA AS MePrecio, "
         Sql = Sql & "camposMonedaExtranjera.MEPRECIOCONIVA2 AS MePrecio2, "
         Sql = Sql & "camposMonedaExtranjera.MEPRECIOCONIVA3 AS MePrecio3, "
         Sql = Sql & "camposMonedaExtranjera.MEPRECIOCONIVA4 AS MePrecio4, "
      End If
   End If
   Sql = Sql & "articuloInventario.UNIDADDEVENTA , "
   Sql = Sql & "articuloInventario.EXISTENCIA , "
   Sql = Sql & "articuloInventario.CANTIDADMINIMA , "
   Sql = Sql & "articuloInventario.CANTIDADMAXIMA"
   Sql = Sql & " FROM "
   If valImprimirDetalles Then
      If valUsaMonedaExtranjera Then
         Sql = Sql & "(articuloInventario LEFT JOIN "
         Sql = Sql & "camposMonedaExtranjera ON ("
         Sql = Sql & "articuloInventario.CODIGO  = camposMonedaExtranjera.CODIGO"
         Sql = Sql & ") AND ("
         Sql = Sql & "articuloInventario.CONSECUTIVOCOMPANIA  = camposMonedaExtranjera.CONSECUTIVOCOMPANIA"
         Sql = Sql & ")) LEFT JOIN ("
         Sql = Sql & "articuloInventario AS ArtInventario RIGHT JOIN "
         Sql = Sql & "productoCompuesto ON ("
         Sql = Sql & "ArtInventario.CONSECUTIVOCOMPANIA = productoCompuesto.ConsecutivoCompania"
         Sql = Sql & ") AND (ArtInventario.CODIGO  = productoCompuesto.CodigoArticulo"
         Sql = Sql & ")) ON ("
         Sql = Sql & "articuloInventario.CODIGO  = productoCompuesto.CodigoConexionConElMaster"
         Sql = Sql & ") AND (articuloInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania)"
      Else
         Sql = Sql & "articuloInventario LEFT JOIN ("
         Sql = Sql & "articuloInventario AS ArtInventario RIGHT JOIN productoCompuesto "
         Sql = Sql & " ON (ArtInventario.CONSECUTIVOCOMPANIA = productoCompuesto.ConsecutivoCompania"
         Sql = Sql & ") AND ("
         Sql = Sql & "ArtInventario.CODIGO  = productoCompuesto.CodigoArticulo"
         Sql = Sql & ")) ON ("
         Sql = Sql & "articuloInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania"
         Sql = Sql & ") AND (articuloInventario.CODIGO  = productoCompuesto.CodigoConexionConElMaster)"
      End If
   Else
      If valUsaMonedaExtranjera Then
         Sql = Sql & "articuloInventario "
         Sql = Sql & " LEFT JOIN camposMonedaExtranjera"
         Sql = Sql & " ON articuloInventario.CODIGO  = camposMonedaExtranjera.CODIGO"
         Sql = Sql & " AND articuloInventario.CONSECUTIVOCOMPANIA  = camposMonedaExtranjera.CONSECUTIVOCOMPANIA"
      Else
         Sql = Sql & "articuloInventario"
      End If
   End If
   Sql = Sql & " WHERE "
   Sql = Sql & "articuloInventario.CONSECUTIVOCOMPANIA  = " & valConsecutivoCompania
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
      Sql = Sql & " AND articuloInventario.LINEADEPRODUCTO  = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   If valCodigoDesde <> "" And valCodigoHasta <> "" Then
      Sql = Sql & " AND " & gUtilSQL.fSQLStringValueBetween("articuloInventario.CODIGO", valCodigoDesde, valCodigoHasta)
   End If
   Sql = Sql & " ORDER BY "
   Sql = Sql & "articuloInventario.LINEADEPRODUCTO , "
   Sql = Sql & "articuloInventario.CODIGO"
   Set gEnumProyecto = Nothing
   fConstruirSQLDelReporteArticuloDeInventario = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteArticuloDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteListadoDeArticulos(ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, ByVal valenumCantidadAImprimirToString As String, _
                     ByVal valLineaDeProductos As String, ByVal valTipoDeArticulo As String, ByVal valstrTipoDearticuloInventarioToNum As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "articuloInventario.CODIGO , "
   Sql = Sql & "articuloInventario.DESCRIPCION , "
   Sql = Sql & "articuloInventario.LINEADEPRODUCTO"
   Sql = Sql & " FROM articuloInventario"
   Sql = Sql & " WHERE "
   Sql = Sql & "articuloInventario.CONSECUTIVOCOMPANIA  = " & valConsecutivoCompania
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
      Sql = Sql & " AND articuloInventario.LINEADEPRODUCTO  = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   If valTipoDeArticulo <> gDefgen.getSearchAllComboBox Then
      Sql = Sql & " AND articuloInventario.TIPODEARTICULO  = " & gUtilSQL.fSQLSimpleValueForEnum(valstrTipoDearticuloInventarioToNum)
   End If
   Sql = Sql & " AND " & gUtilSQL.DfLeftSQL("articuloInventario.CODIGO", 3) & "<>" & gUtilSQL.fSimpleSqlValue("RD_")
   Sql = Sql & " AND " & gUtilSQL.DfRightSQL("articuloInventario.CODIGO", 1) & "<>" & gUtilSQL.fSimpleSqlValue("@")
   Sql = Sql & " ORDER BY "
   Sql = Sql & "articuloInventario.LINEADEPRODUCTO , "
   Sql = Sql & "articuloInventario.CODIGO"
   fConstruirSQLDelReporteListadoDeArticulos = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteListadoDeArticulos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConstruirSQLDelReporteListadoArticulosPorRetirarEntreFecha(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long, ByVal valIsSQLDataBase As Boolean) As String
   Dim Sql As String
   Dim SQLCodigoArticulo As String
   On Error GoTo h_ERROR
   SQLCodigoArticulo = gUtilSQL.getIIF("renglonfactura.Serial <> ''", " renglonFactura.Articulo + ' Serial ' + renglonfactura.Serial + ' Rollo '+ renglonfactura.rollo ", " renglonFactura.Articulo", True) & "  as CodArt "
   Sql = " SELECT DISTINCT  " & SQLCodigoArticulo & ", "
   Sql = Sql & " articuloInventario.Descripcion as Descripcion, "
   Sql = Sql & " factura.Numero as NumeroDocumento, "
   Sql = Sql & " renglonFactura.Cantidad as CantReservada, "
   Sql = Sql & " cliente.Nombre as NombreCliente, "
   Sql = Sql & " factura.fechaDeRetiro as fechaRetiro "
   Sql = Sql & " FROM (ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)"
   Sql = Sql & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) INNER JOIN ((cliente INNER JOIN factura ON (cliente.Codigo = factura.CodigoCliente) AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)) "
   Sql = Sql & " INNER JOIN renglonFactura ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) AND (factura.Numero = renglonFactura.NumeroFactura) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) ON (renglonFactura.Articulo = ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   Sql = Sql & " WHERE   (factura.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonFactura.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   Sql = Sql & " AND (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & ")"
   Sql = Sql & " AND (" & gUtilSQL.DfSQLDateValueBetween("factura.fechaDeRetiro", valFechaInicial, valFechaFinal) & ")"
   Sql = Sql & " UNION "
   Sql = Sql & " SELECT DISTINCT  renglonFactura.Articulo as CodArt, "
   Sql = Sql & " articuloInventario.Descripcion as Descripcion, "
   Sql = Sql & " factura.Numero as NumeroDocumento, "
   Sql = Sql & " renglonFactura.Cantidad as CantReservada, "
   Sql = Sql & " cliente.Nombre as NombreCliente, "
   Sql = Sql & " factura.fechaDeRetiro as fechaRetiro "
   Sql = Sql & " FROM articuloInventario INNER JOIN (cliente INNER JOIN (factura INNER JOIN renglonFactura "
   Sql = Sql & " ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) AND (factura.Numero = renglonFactura.NumeroFactura) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) ON (cliente.Codigo = factura.CodigoCliente) AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)) ON (articuloInventario.ConsecutivoCompania = factura.ConsecutivoCompania) AND (articuloInventario.Codigo = renglonFactura.Articulo)"
   Sql = Sql & " WHERE   (factura.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonFactura.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   Sql = Sql & " AND (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & ")"
   Sql = Sql & " AND (" & gUtilSQL.DfSQLDateValueBetween("factura.fechaDeRetiro", valFechaInicial, valFechaFinal) & ")"
   Sql = Sql & " UNION "
   SQLCodigoArticulo = gUtilSQL.getIIF("renglonCotizacion.Serial <> ''", " renglonCotizacion.CodigoArticulo  + ' Serial ' + renglonCotizacion.Serial + ' Rollo '+ renglonCotizacion.rollo ", " renglonCotizacion.CodigoArticulo", True) & "  as CodArt "
   Sql = Sql & " SELECT DISTINCT " & SQLCodigoArticulo & ", "
   Sql = Sql & " articuloInventario.Descripcion as Descripcion, "
   Sql = Sql & " cotizacion.Numero as NumeroDocumento, "
   Sql = Sql & " renglonCotizacion.Cantidad as CantReservada, "
   Sql = Sql & " cliente.Nombre as NombreCliente, "
   Sql = Sql & " cotizacion.fechaDeRetiro as fechaRetiro "
   Sql = Sql & " FROM (ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)"
   Sql = Sql & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) INNER JOIN ((cliente INNER JOIN Cotizacion ON (cliente.Codigo = Cotizacion.CodigoCliente) AND (cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)) "
   Sql = Sql & " INNER JOIN renglonCotizacion ON (Cotizacion.Numero = renglonCotizacion.NumeroCotizacion) AND (Cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)) ON (renglonCotizacion.CodigoArticulo = ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)"
   Sql = Sql & " WHERE   (Cotizacion.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonCotizacion.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   Sql = Sql & " AND (" & gUtilSQL.DfSQLDateValueBetween("cotizacion.fechaDeRetiro", valFechaInicial, valFechaFinal) & ")"
   Sql = Sql & " UNION "
   Sql = Sql & " SELECT DISTINCT  renglonCotizacion.CodigoArticulo as CodArt, "
   Sql = Sql & " articuloInventario.Descripcion as Descripcion, "
   Sql = Sql & " cotizacion.Numero as NumeroDocumento, "
   Sql = Sql & " renglonCotizacion.Cantidad as CantReservada, "
   Sql = Sql & " cliente.Nombre as NombreCliente, "
   Sql = Sql & " cotizacion.fechaDeRetiro as fechaRetiro "
   Sql = Sql & " FROM articuloInventario INNER JOIN (cliente INNER JOIN (Cotizacion INNER JOIN renglonCotizacion "
   Sql = Sql & " ON (Cotizacion.Numero = renglonCotizacion.NumeroCotizacion) AND (Cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)) ON (cliente.Codigo = Cotizacion.CodigoCliente) AND (cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)) ON (articuloInventario.ConsecutivoCompania = Cotizacion.ConsecutivoCompania) AND (articuloInventario.Codigo = renglonCotizacion.CodigoArticulo)"
   Sql = Sql & " WHERE   (cotizacion.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonCotizacion.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   Sql = Sql & " AND (" & gUtilSQL.DfSQLDateValueBetween("cotizacion.fechaDeRetiro", valFechaInicial, valFechaFinal) & ")"
   If valIsSQLDataBase Then
      Sql = Sql & " order by factura.Numero, factura.fechaDeRetiro "
   Else
      Sql = Sql & " order by NumeroDocumento, fechaRetiro"
    End If
  fConstruirSQLDelReporteListadoArticulosPorRetirarEntreFecha = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteListadoArticulosPorRetirarEntreFecha", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteArticulosReservados(ByVal valConsecutivoCompania As Long) As String
   Dim Sql As String
   Dim SQLCodigoArticulo As String
   On Error GoTo h_ERROR
   SQLCodigoArticulo = gUtilSQL.getIIF("renglonfactura.Serial <> '0'", " renglonFactura.Articulo + ' Serial ' + renglonfactura.Serial + ' Rollo '+ renglonfactura.rollo ", " renglonFactura.Articulo", True) & "  as CodArt "
   Sql = " SELECT " & SQLCodigoArticulo & ", "
   Sql = Sql & " articuloInventario.Descripcion as Descripcion, "
   Sql = Sql & " factura.Numero as NumeroDocumento, "
   Sql = Sql & " renglonFactura.Cantidad as CantReservada, "
   Sql = Sql & " cliente.Nombre as NombreCliente, "
   Sql = Sql & " factura.fecha as fecha, "
   Sql = Sql & " renglonFactura.Cantidad * renglonFactura.PrecioSinIVA as MontoTotal "
   Sql = Sql & " FROM (ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)"
   Sql = Sql & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) INNER JOIN ((cliente INNER JOIN factura ON (cliente.Codigo = factura.CodigoCliente) AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)) "
   Sql = Sql & " INNER JOIN renglonFactura ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) AND (factura.Numero = renglonFactura.NumeroFactura) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) ON (renglonFactura.Articulo = ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   Sql = Sql & " WHERE   (factura.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonFactura.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   Sql = Sql & " AND (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & ")"
   Sql = Sql & " UNION "
   Sql = Sql & " SELECT renglonFactura.Articulo  as CodArt, "
   Sql = Sql & " articuloInventario.Descripcion as Descripcion, "
   Sql = Sql & " factura.Numero as NumeroDocumento, "
   Sql = Sql & " renglonFactura.Cantidad as CantReservada, "
   Sql = Sql & " cliente.Nombre as NombreCliente, "
   Sql = Sql & " factura.fecha as fecha, "
   Sql = Sql & " renglonFactura.Cantidad * renglonFactura.PrecioSinIVA as MontoTotal "
   Sql = Sql & " FROM articuloInventario INNER JOIN (cliente INNER JOIN (factura INNER JOIN renglonFactura "
   Sql = Sql & " ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) AND (factura.Numero = renglonFactura.NumeroFactura) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) ON (cliente.Codigo = factura.CodigoCliente) AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)) ON (articuloInventario.ConsecutivoCompania = factura.ConsecutivoCompania) AND (articuloInventario.Codigo = renglonFactura.Articulo)"
   Sql = Sql & " WHERE   (factura.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonFactura.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   Sql = Sql & " AND (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & ")"
   Sql = Sql & " AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Simple) & ")"
   Sql = Sql & " UNION "
   SQLCodigoArticulo = gUtilSQL.getIIF("renglonCotizacion.Serial <> '0'", " renglonCotizacion.CodigoArticulo  + ' Serial ' + renglonCotizacion.Serial + ' Rollo '+ renglonCotizacion.rollo ", " renglonCotizacion.CodigoArticulo", True) & "  as CodArt "
   Sql = Sql & " SELECT " & SQLCodigoArticulo & ", "
   Sql = Sql & " articuloInventario.Descripcion as Descripcion, "
   Sql = Sql & " Cotizacion.Numero as NumeroDocumento, "
   Sql = Sql & " renglonCotizacion.Cantidad as CantReservada, "
   Sql = Sql & " cliente.Nombre as NombreCliente, "
   Sql = Sql & " Cotizacion.fecha as fecha, "
   Sql = Sql & " RenglonCotizacion.Cantidad * renglonCotizacion.PrecioSinIVA as MontoTotal "
   Sql = Sql & " FROM (ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)"
   Sql = Sql & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) INNER JOIN ((cliente INNER JOIN Cotizacion ON (cliente.Codigo = Cotizacion.CodigoCliente) AND (cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)) "
   Sql = Sql & " INNER JOIN renglonCotizacion ON (Cotizacion.Numero = renglonCotizacion.NumeroCotizacion) AND (Cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)) ON (renglonCotizacion.CodigoArticulo = ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)"
   Sql = Sql & " WHERE   (Cotizacion.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonCotizacion.ConsecutivoCompania = " & valConsecutivoCompania & " )"
   Sql = Sql & " UNION "
   Sql = Sql & " SELECT renglonCotizacion.CodigoArticulo As CodArt, "
   Sql = Sql & " articuloInventario.Descripcion As Descripcion, "
   Sql = Sql & " Cotizacion.Numero as NumeroDocumento, "
   Sql = Sql & " renglonCotizacion.Cantidad as CantReservada, "
   Sql = Sql & " cliente.Nombre as NombreCliente, "
   Sql = Sql & " Cotizacion.fecha as fecha, "
   Sql = Sql & " RenglonCotizacion.Cantidad * renglonCotizacion.PrecioSinIVA as MontoTotal "
   Sql = Sql & " FROM articuloInventario INNER JOIN (cliente INNER JOIN (Cotizacion INNER JOIN renglonCotizacion "
   Sql = Sql & " ON (Cotizacion.Numero = renglonCotizacion.NumeroCotizacion) AND (Cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)) ON (cliente.Codigo = Cotizacion.CodigoCliente) AND (cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)) ON (articuloInventario.ConsecutivoCompania = Cotizacion.ConsecutivoCompania) AND (articuloInventario.Codigo = renglonCotizacion.CodigoArticulo)"
   Sql = Sql & " WHERE   (Cotizacion.ReservarMercancia  = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (renglonCotizacion.ConsecutivoCompania = " & valConsecutivoCompania & " )"
    Sql = Sql & "AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Simple) & ")"
  fConstruirSQLDelReporteArticulosReservados = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteArticulosReservados", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLParaLaImpresionDeCodDeBarras(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, _
                                                   ByVal valCodigoHasta As String, ByVal valNivelDePrecio As enum_NivelDePrecio, _
                                                   ByVal valPorcentajeAlicuotaGeneral As Currency, ByVal valPorcentajeAlicuotaReducida As Currency, ByVal valPorcentajeAlicuotaAdicional As Currency)
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vSql As String
   Dim vSqlWhere As String
   Dim vPrecio As String
   Dim vSqlAlicuota As String
   Dim vSqlColorDescrip As String
   Dim vSqlTallaDescrip As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSqlWhere = ""
   vSqlColorDescrip = gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & "ISNULL(Color.DescripcionColor, " & gUtilSQL.fSimpleSqlValue("") & ")"
   vSqlTallaDescrip = gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & "ISNULL(Talla.DescripcionTalla, " & gUtilSQL.fSimpleSqlValue("") & ")"
   If gTexto.DfLen(valCodigoDesde) > 0 And gTexto.DfLen(valCodigoHasta) > 0 Then
      vSqlWhere = gUtilSQL.fSQLStringValueBetween("AI.Codigo", valCodigoDesde, valCodigoHasta) & "  AND  "
   End If
   vSqlWhere = vSqlWhere & "AI.ConsecutivoCompania = " & valConsecutivoCompania
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   vSqlAlicuota = " (CASE AI.AlicuotaIva WHEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_EXENTO) & " THEN 0 WHEN " _
   & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTAGENERAL) & " THEN " & gConvert.fConvierteAString(valPorcentajeAlicuotaGeneral) _
   & " WHEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTA2) & " THEN " & gConvert.fConvierteAString(valPorcentajeAlicuotaReducida) _
   & " WHEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTA3) & " THEN " & gConvert.fConvierteAString(valPorcentajeAlicuotaAdicional) & " ELSE 0 END)"
   vSql = " SELECT AI.Codigo + ISNULL(EPG.CodigoColor, '') + ISNULL(EPG.CodigoTalla, '') AS CodigoArticulo, "
   vSql = vSql & "AI.Descripcion " & gUtilSQL.CharConcat & vSqlColorDescrip & gUtilSQL.CharConcat & vSqlTallaDescrip & " AS Descripcion, "
   vSql = vSql & "AI.AlicuotaIva AS AlicuotaIvaChr, "
   vSql = vSql & vSqlAlicuota & " AS AlicuotaIvaValor, "
   If (valNivelDePrecio = eND_PRECIO_4) Then
      vSql = vSql & "AI.PrecioSinIva4 AS PrecioSinIvaML, "
      vSql = vSql & "AI.PrecioConIva4 AS PrecioConIVAML, "
      vSql = vSql & "(AI.PrecioConIva4 - AI.PrecioSinIva4) AS IvaML, "
      vSql = vSql & "ISNULL(ME.MePrecioSinIva4, 0) AS PrecioSinIvaME, "
      vSql = vSql & "ISNULL(ME.MePrecioConIva4, 0) AS PrecioConIvaME, "
      vSql = vSql & "(ISNULL(ME.MePrecioConIva4, 0) - ISNULL(ME.MePrecioSinIva4, 0)) AS IvaME, "
   ElseIf (valNivelDePrecio = eND_PRECIO_3) Then
      vSql = vSql & "AI.PrecioSinIva3 AS PrecioSinIvaML, "
      vSql = vSql & "AI.PrecioConIva3 AS PrecioConIVAML, "
      vSql = vSql & "(AI.PrecioConIva3 - AI.PrecioSinIva3) AS IvaML, "
      vSql = vSql & "ISNULL(ME.MePrecioSinIva3, 0) AS PrecioSinIvaME, "
      vSql = vSql & "ISNULL(ME.MePrecioConIva3, 0) AS PrecioConIvaME, "
      vSql = vSql & "(ISNULL(ME.MePrecioConIva3, 0) - ISNULL(ME.MePrecioSinIva3, 0)) AS IvaME, "
   ElseIf (valNivelDePrecio = eND_PRECIO_2) Then
      vSql = vSql & "AI.PrecioSinIva2 AS PrecioSinIvaML, "
      vSql = vSql & "AI.PrecioConIva2 AS PrecioConIVAML, "
      vSql = vSql & "(AI.PrecioConIva2 - AI.PrecioSinIva2) AS IvaML, "
      vSql = vSql & "ISNULL(ME.MePrecioSinIva2, 0) AS PrecioSinIvaME, "
      vSql = vSql & "ISNULL(ME.MePrecioConIva2, 0) AS PrecioConIvaME, "
      vSql = vSql & "(ISNULL(ME.MePrecioConIva2, 0) - ISNULL(ME.MePrecioSinIva2, 0)) AS IvaME, "
   Else
      vSql = vSql & "AI.PrecioSinIva AS PrecioSinIvaML, "
      vSql = vSql & "AI.PrecioConIva AS PrecioConIVAML, "
      vSql = vSql & "(AI.PrecioConIva - AI.PrecioSinIva) AS IvaML, "
      vSql = vSql & "ISNULL(ME.MePrecioSinIva, 0) AS PrecioSinIvaME, "
      vSql = vSql & "ISNULL(ME.MePrecioConIva, 0) AS PrecioConIvaME, "
      vSql = vSql & "(ISNULL(ME.MePrecioConIva, 0) - ISNULL(ME.MePrecioSinIva, 0)) AS IvaME, "
   End If
   vSql = vSql & "AI.PorcentajeBaseImponible, AI.UnidadDeVenta, AI.CampoDefinible1, AI.CampoDefinible2, AI.CampoDefinible3, AI.CampoDefinible4, AI.CampoDefinible5, AI.UnidadDeVentaSecundaria "
   vSql = vSql & "FROM ArticuloInventario AS AI LEFT OUTER JOIN "
   vSql = vSql & "ExistenciaPorGrupo AS EPG INNER JOIN "
   vSql = vSql & "Saw.Talla ON EPG.CodigoTalla = Saw.Talla.CodigoTalla INNER JOIN "
   vSql = vSql & "Saw.Color ON EPG.CodigoColor = Saw.Color.CodigoColor ON AI.ConsecutivoCompania = Saw.Color.ConsecutivoCompania AND "
   vSql = vSql & "AI.ConsecutivoCompania = Saw.Talla.ConsecutivoCompania AND "
   vSql = vSql & "AI.Codigo = EPG.CodigoArticulo AND "
   vSql = vSql & "AI.CodigoGrupo = (CASE WHEN EPG.CodigoGrupo = '0' THEN '' ELSE EPG.CodigoGrupo END) AND AI.ConsecutivoCompania = EPG.ConsecutivoCompania LEFT OUTER JOIN "
   vSql = vSql & "CamposMonedaExtranjera AS ME ON AI.ConsecutivoCompania = ME.ConsecutivoCompania AND AI.Codigo = ME.Codigo "
   vSql = vSql & vSqlWhere
h_EXIT:   On Error GoTo 0
   fSQLParaLaImpresionDeCodDeBarras = vSql
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlParaLaImpresionDeCodDeBarras", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteGananciaXProducto(ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, ByVal valenumCantidadAImprimirToString As String, ByVal valLineaDeProductos As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & " CODIGO , "
   Sql = Sql & " DESCRIPCION , "
   Sql = Sql & " PRECIOSINIVA , "
   Sql = Sql & " COSTOUNITARIO , "
   Sql = Sql & " LINEADEPRODUCTO , "
   Sql = Sql & " (PRECIOSINIVA  - COSTOUNITARIO ) AS Ganancia, "
   Sql = Sql & gUtilSQL.getIIF("COSTOUNITARIO  <> 0 ", "(((PRECIOSINIVA  - COSTOUNITARIO ) / COSTOUNITARIO ) * 100)", "0", True) & " AS Porcentaje"
   Sql = Sql & " FROM articuloInventario"
   Sql = Sql & " WHERE "
   Sql = Sql & " CONSECUTIVOCOMPANIA = " & valConsecutivoCompania
   Sql = Sql & " AND TIPODEARTICULO  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
       Sql = Sql & " AND LINEADEPRODUCTO  = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   Sql = Sql & " ORDER BY "
   Sql = Sql & " LINEADEPRODUCTO , "
   Sql = Sql & " CODIGO"
   fConstruirSQLDelReporteGananciaXProducto = Sql
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
   Dim Sql As String
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
   Sql = "SELECT "
   Sql = Sql & "articuloInventario.CODIGO , "
   Sql = Sql & "articuloInventario.DESCRIPCION , "
   Sql = Sql & "articuloInventario.LINEADEPRODUCTO , "
   Sql = Sql & "articuloInventario.TIPODEARTICULO , "
   Sql = Sql & sqlTipoDeArticulo & ", "
   Sql = Sql & "articuloInventario.COSTOUNITARIO , "
   Sql = Sql & "articuloInventario.EXISTENCIA , "
   If preciosSinIva Then
      Sql = Sql & "articuloInventario.PRECIOSINIVA AS Precio, "
   Else
      Sql = Sql & "articuloInventario.PRECIOCONIVA AS Precio, "
   End If
   If valUsaMonedaExtranjera Then
      Sql = Sql & "articuloInventario.MECOSTOUNITARIO , "
      If preciosSinIva Then
         SQLMePrecio = "camposMonedaExtranjera.MEPRECIOSINIVA"
      Else
         SQLMePrecio = "camposMonedaExtranjera.MEPRECIOCONIVA"
      End If
      SQLMePrecio = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(SQLMePrecio), 0, SQLMePrecio)
      Sql = Sql & SQLMePrecio & " AS MePrecio, "
      SQLMeValoracion = "articuloInventario.EXISTENCIA  * articuloInventario.MECOSTOUNITARIO"
      SQLMeValoracion = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(SQLMeValoracion), 0, SQLMeValoracion)
      Sql = Sql & SQLMeValoracion & " AS MeValoracion, "
      SQLValorDeVenta = "articuloInventario.EXISTENCIA  * camposMonedaExtranjera.MEPRECIOSINIVA"
      SQLValorDeVenta = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(SQLValorDeVenta), 0, SQLValorDeVenta)
      Sql = Sql & SQLValorDeVenta & " AS MeValorDeVenta, "
   End If
   Sql = Sql & "(articuloInventario.EXISTENCIA  * articuloInventario.COSTOUNITARIO ) AS Valoracion, "
   Sql = Sql & "(articuloInventario.EXISTENCIA  * articuloInventario.PRECIOSINIVA ) AS ValorDeVenta, "
   Sql = Sql & SQLValorizadoConIva & " as ValorizadoConIva, "
   Sql = Sql & SQLIvaValorizado & " as IvaValorizado "
   Sql = Sql & " FROM "
      If valUsaMonedaExtranjera Then
         Sql = Sql & "( articuloInventario LEFT JOIN "
         Sql = Sql & "camposMonedaExtranjera ON ("
         Sql = Sql & "articuloInventario.CODIGO  = camposMonedaExtranjera.CODIGO"
         Sql = Sql & ") AND ("
         Sql = Sql & "articuloInventario.ConsecutivoCompania  = camposMonedaExtranjera.CONSECUTIVOCOMPANIA"
         Sql = Sql & ")) LEFT JOIN ("
         Sql = Sql & "articuloInventario AS ArtInventario RIGHT JOIN "
         Sql = Sql & "productoCompuesto  ON ("
         Sql = Sql & "ArtInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania"
         Sql = Sql & ") AND (ArtInventario.CODIGO  = productoCompuesto.CodigoArticulo"
         Sql = Sql & ")) ON ("
         Sql = Sql & "articuloInventario.CODIGO  = productoCompuesto.CodigoConexionConElMaster"
         Sql = Sql & ") AND (articuloInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania)"
      Else
         Sql = Sql & "articuloInventario LEFT JOIN ("
         Sql = Sql & "articuloInventario AS ArtInventario RIGHT JOIN productoCompuesto"
         Sql = Sql & " ON (ArtInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania"
         Sql = Sql & ") AND ("
         Sql = Sql & "ArtInventario.CODIGO  = productoCompuesto.CodigoArticulo"
         Sql = Sql & ")) ON ("
         Sql = Sql & "articuloInventario.CONSECUTIVOCOMPANIA  = productoCompuesto.ConsecutivoCompania"
         Sql = Sql & ") AND (articuloInventario.CODIGO  = productoCompuesto.CodigoConexionConElMaster)"
      End If
   Sql = Sql & " WHERE "
   Sql = Sql & "articuloInventario.CONSECUTIVOCOMPANIA  = " & valConsecutivoCompania
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
      Sql = Sql & " AND articuloInventario.LINEADEPRODUCTO  = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   If valCodigoDesde <> "" And valCodigoHasta <> "" Then
      Sql = Sql & " AND " & gUtilSQL.fSQLStringValueBetween("articuloInventario.CODIGO", valCodigoDesde, valCodigoHasta)
   End If
   Sql = Sql & " ORDER BY "
   Sql = Sql & "articuloInventario.LINEADEPRODUCTO , "
   Sql = Sql & "articuloInventario.CODIGO"
   fConstruirSQLDelReporteValoracionDeInventario = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteArticuloDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteHojaDeTrabajoDePedido(ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, _
                                                               ByVal valenumCantidadAImprimirToString As String, ByVal valLineaDeProductos As String, _
                                                                  ByVal valUsaAlmacen As Boolean, ByVal valCantidadAImprimirAlmacen As String, _
                                                                     ByVal valNombreAlmacen As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo , "
   Sql = Sql & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion + " & gUtilSQL.getIIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoTalla <> ''", "  ' ' + saw.talla.DescripcionTalla ", "''") & "+" & gUtilSQL.getIIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoColor <> ''", "  ' ' + saw.color.DescripcionColor", "''") & " AS Descripcion, "
   Sql = Sql & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.UnidadDeVenta, "
   Sql = Sql & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto, "
   Sql = Sql & "existenciaPorAlmacen.Cantidad, "
   Sql = Sql & "almacen.NombreAlmacen"
   Sql = Sql & " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
   Sql = Sql & " INNER JOIN ( almacen"
   Sql = Sql & " INNER JOIN  existenciaPorAlmacen"
   Sql = Sql & " ON ( almacen.Codigo = existenciaPorAlmacen.CodigoAlmacen"
   Sql = Sql & ") AND (almacen.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania))"
   Sql = Sql & " ON (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = existenciaPorAlmacen.CodigoArticulo)"
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania)"
   Sql = Sql & " LEFT JOIN Saw.Color ON IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoColor = Saw.Color.CodigoColor"
   Sql = Sql & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = Saw.Color.ConsecutivoCompania"
   Sql = Sql & " LEFT JOIN Saw.Talla ON  IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoTalla = Saw.Talla.CodigoTalla"
   Sql = Sql & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = Saw.Talla.ConsecutivoCompania"
   Sql = Sql & " WHERE "
   Sql = Sql & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.TipoDeArticulo  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
      Sql = Sql & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   If valUsaAlmacen And valCantidadAImprimirAlmacen = valenumCantidadAImprimirToString Then
      Sql = Sql & " AND almacen.NombreAlmacen = " & gUtilSQL.fSimpleSqlValue(Trim(valNombreAlmacen))
   End If
   Sql = Sql & " GROUP BY IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo , IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion , "
   Sql = Sql & " IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.UnidadDeVenta , IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto, "
   Sql = Sql & " almacen.NombreAlmacen, existenciaPorAlmacen.Cantidad, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo,"
   Sql = Sql & " saw.talla.DescripcionTalla, saw.Color.DescripcionColor, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoTalla, "
   Sql = Sql & " IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoColor"
   Sql = Sql & " ORDER BY "
   Sql = Sql & "almacen.NombreAlmacen, "
   Sql = Sql & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto, "
   Sql = Sql & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo"
   fConstruirSQLDelReporteHojaDeTrabajoDePedido = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteHojaDeTrabajoDePedido", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteDeProductoSinVenta(ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, _
                                                            ByVal valenumCantidadAImprimirToString As String, ByVal valLineaDeProductos As String, _
                                                               ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim Sql As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   Sql = "SELECT (ArticuloInventario.Codigo+"
   Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ExistenciaPorGrupo.CodigoColor"), gUtilSQL.fSimpleSqlValue(" "), "ExistenciaPorGrupo.CodigoColor") & "+"
   Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ExistenciaPorGrupo.CodigoTalla"), gUtilSQL.fSimpleSqlValue(" "), "ExistenciaPorGrupo.CodigoTalla") & ") as Codigo,"
   Sql = Sql & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   Sql = Sql & "ArticuloInventario.CostoUnitario,"
   Sql = Sql & "(" & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("SUM(ExistenciaPorGrupo.Existencia)"), "ArticuloInventario.Existencia", "SUM(ExistenciaPorGrupo.Existencia)") & ") as Existencia,"
   Sql = Sql & " ArticuloInventario.LineaDeProducto,"
   Sql = Sql & gUtilSQL.fMax(gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Factura.Fecha"), "Factura_1.Fecha", "Factura.Fecha"), "Fecha") & ","
   Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("factura.StatusFactura"), "Factura_1.StatusFactura", "Factura.StatusFactura", True) & " As StatusFactura"
   Sql = Sql & " FROM (((articuloInventario LEFT JOIN (ExistenciaPorGrupo LEFT JOIN (renglonFactura LEFT JOIN factura ON (renglonFactura.TipoDeDocumento = factura.TipoDeDocumento) AND (renglonFactura.NumeroFactura = factura.Numero) AND"
   Sql = Sql & " (renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania)) ON ((ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.codigoTalla) = renglonFactura.Articulo) AND"
   Sql = Sql & " (ExistenciaPorGrupo.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) ON (articuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo) AND (articuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania))"
   Sql = Sql & " LEFT JOIN Talla ON (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania)) LEFT JOIN Color ON (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor) AND"
   Sql = Sql & " (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania)) LEFT JOIN (renglonFactura AS renglonFactura_1 LEFT JOIN factura AS factura_1 ON"
   Sql = Sql & " (renglonFactura_1.NumeroFactura = factura_1.Numero) AND (renglonFactura_1.ConsecutivoCompania = factura_1.ConsecutivoCompania)) ON (articuloInventario.Codigo = renglonFactura_1.Articulo)"
   Sql = Sql & " AND (articuloInventario.ConsecutivoCompania = renglonFactura_1.ConsecutivoCompania)"
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
   Sql = Sql & sqlWhere
   Sql = Sql & " GROUP BY ArticuloInventario.Codigo, articuloInventario.CostoUnitario, articuloInventario.Existencia, articuloInventario.LineaDeProducto, factura.StatusFactura, articuloInventario.Descripcion, Color.DescripcionColor, Talla.DescripcionTalla,ExistenciaPorGrupo.CodigoColor,ExistenciaPorGrupo.CodigoTalla,Factura_1.StatusFactura"
   Sql = Sql & " ORDER BY Codigo"
   fConstruirSQLDelReporteDeProductoSinVenta = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteDeProductoSinVenta", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExistenciaInicialMovInvFactura(ByVal valCodigoProducto As String, ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(IGV_ArticuloVenta_B2.Salidas)", 0) & " AS ExistenciaInicial " & vbCrLf
   Sql = Sql & " FROM IGV_ArticuloVenta_B2 "
   Sql = Sql & " WHERE (IGV_ArticuloVenta_B2.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto)) & ")"
   Sql = Sql & " AND (IGV_ArticuloVenta_B2.ConsecutivoCompania  = " & valConsecutivoCompania & ")"
   Sql = Sql & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "IGV_ArticuloVenta_B2.Fecha", valFechaInicial, "<") & ")" & vbCrLf
   fSQLExistenciaInicialMovInvFactura = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExistenciaInicialMovInvFactura", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteProductosPorDebajoDeExistenciaMinima(ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, ByVal valenumCantidadAImprimirToString As String, ByVal valLineaDeProductos As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "CODIGO , "
   Sql = Sql & "DESCRIPCION , "
   Sql = Sql & "CANTIDADMINIMA , "
   Sql = Sql & " EXISTENCIA, "
   Sql = Sql & " LINEADEPRODUCTO"
   Sql = Sql & " FROM articuloInventario"
   Sql = Sql & " WHERE CONSECUTIVOCOMPANIA = " & valConsecutivoCompania
   Sql = Sql & " AND EXISTENCIA  < CANTIDADMINIMA"
   Sql = Sql & " AND TIPODEARTICULO = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
      Sql = Sql & " AND LINEADEPRODUCTO  = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   Sql = Sql & " ORDER BY LINEADEPRODUCTO, CODIGO"
   fConstruirSQLDelReporteProductosPorDebajoDeExistenciaMinima = Sql
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
   Dim Sql As String
   Dim SQLSerial As String
   Dim SQLRollo As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & " ArticuloInventario.codigo  AS  CodArt, "
   Sql = Sql & " ArticuloInventario.descripcion  AS Descripcion,"
   Sql = Sql & " ArticuloInventario.precioSinIva  AS precioSinIva, "
   Sql = Sql & " ArticuloInventario.PrecioSinIVA2 AS precioSinIva2, "
   Sql = Sql & " ArticuloInventario.precioSinIva3  AS precioSinIva3, "
   Sql = Sql & " ArticuloInventario.precioSinIva4  AS precioSinIva4, "
   Sql = Sql & " ExistenciaPorGrupo.existencia AS  Existencia,  "
   Sql = Sql & " Talla.DescripcionTalla AS  DescripcionTalla,  "
   Sql = Sql & " ArticuloInventario.codigo + Talla.CodigoTalla + Color.CodigoColor as CodArtTallaYColor,"
   Sql = Sql & " Color.DescripcionColor AS  DescripcionColor, "
   SQLSerial = gUtilSQL.getIIF("ExistenciaPorGRupo.Serial = '0' ", " ''", " ExistenciaPorGrupo.Serial", True) & "  as Serial "
   Sql = Sql & SQLSerial & ", "
   SQLRollo = gUtilSQL.getIIF("ExistenciaPorGRupo.Rollo = '0' ", " ''", " ExistenciaPorGrupo.Rollo", True) & "  as Rollo "
   Sql = Sql & SQLRollo & " "
   Sql = Sql & " FROM  Talla  INNER JOIN (Color  INNER JOIN (ArticuloInventario  INNER JOIN ExistenciaPorGrupo "
   Sql = Sql & " ON (ArticuloInventario.codigo = ExistenciaPorGrupo.CodigoArticulo ) AND ( ArticuloInventario.consecutivoCompania = ExistenciaPorGrupo.consecutivoCompania ))"
   Sql = Sql & " ON ( Color.consecutivoCompania = ExistenciaPorGrupo.consecutivoCompania ) AND ( Color.CodigoColor = ExistenciaPorGrupo.CodigoColor )) ON (Talla.consecutivoCompania = ExistenciaPorGrupo.consecutivoCompania) AND "
   Sql = Sql & "( Talla.CodigoTalla = ExistenciaPorGrupo.CodigoTalla )"
   Sql = Sql & " WHERE "
   Sql = Sql & " ArticuloInventario.ConsecutivoCompania  = " & valConsecutivoCompaniaActual
   If valCantidadAImprimir = enumCantidadAImprimirToString(eCI_uno) Then
      Sql = Sql & " AND articuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProducto))
   End If
   If valTipoDeArticulo <> gDefgen.getSearchAllComboBox Then
      Sql = Sql & " AND TIPODEARTICULO  = " & gUtilSQL.fSQLSimpleValueForEnum(strTipoDeArticuloToNum(valTipoDeArticulo))
   End If
   Sql = Sql & " ORDER BY "
   Sql = Sql & " ArticuloInventario.codigo, "
   Sql = Sql & " Color.CodigoColor "
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteExistenciaPorTallaYColor = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteExistenciaPorTallaYColor", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteVencimientoDeProducto(ByVal valConsecutivoCompaniaActual As Long, ByVal valCantidadAImprimir As String, ByVal valLineaDeProducto As String)
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "ArticuloInventario.Codigo, "
   Sql = Sql & "ArticuloInventario.Descripcion, "
   Sql = Sql & "ArticuloInventario.FechaDeVencimiento, "
   Sql = Sql & "ArticuloInventario.LineaDeProducto"
   Sql = Sql & " FROM ArticuloInventario"
   Sql = Sql & " WHERE ArticuloInventario.ConsecutivoCompania  = " & valConsecutivoCompaniaActual
   Sql = Sql & " AND ArticuloInventario.TIPODEARTICULO  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   Sql = Sql & " AND (NOT " & gUtilSQL.DfSQLIsNull("ArticuloInventario.FechaDeVencimiento") & ")"
   If valCantidadAImprimir = enumCantidadAImprimirToString(eCI_uno) Then
      Sql = Sql & " AND ArticuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProducto))
   End If
   Sql = Sql & " ORDER BY "
   Sql = Sql & "ArticuloInventario.lineaDeProducto, "
   Sql = Sql & "ArticuloInventario.FechaDeVencimiento, "
   Sql = Sql & "ArticuloInventario.Codigo"
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteVencimientoDeProducto = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteVencimientoDeProducto", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExistenciaPorGrupo(ByVal valCodigoGrupo As String, ByVal valCodigoArticulo As String, ByVal valCodigoCodigoTalla As String, ByVal valCodigoColor As String, ByVal valCodigoCompaniaActual As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT SUM(ExistenciaPorGrupo.CantReservada) AS CantReservada,"
   Sql = Sql & " SUM(ExistenciaPorGrupo.Existencia) AS Existencia"
   Sql = Sql & " FROM ExistenciaPorGrupo WHERE "
   Sql = Sql & " ExistenciaPorGrupo.ConsecutivoCompania = " & valCodigoCompaniaActual
   Sql = Sql & " AND ExistenciaPorGrupo.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigoArticulo)
   Sql = Sql & " AND ExistenciaPorGrupo.CodigoGrupo = " & gUtilSQL.fSimpleSqlValue(valCodigoGrupo)
   Sql = Sql & " AND ExistenciaPorGrupo.CodigoTalla = " & gUtilSQL.fSimpleSqlValue(valCodigoCodigoTalla)
   Sql = Sql & " AND ExistenciaPorGrupo.CodigoColor = " & gUtilSQL.fSimpleSqlValue(valCodigoColor)
   Sql = Sql & " GROUP BY CodigoArticulo "
h_EXIT: On Error GoTo 0
   fSQLExistenciaPorGrupo = Sql
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
   Dim Sql As String
   Dim ExistenciaInicial As String
   Dim insInvPermanenteVista As clsArticuloInvVista
   On Error GoTo h_ERROR
   Set insInvPermanenteVista = New clsArticuloInvVista
   
   Sql = "(SELECT "
   Sql = Sql & gUtilSQL.fIfResultIsNullReplace("SUM(" & gUtilSQL.fIfResultIsNullReplace("Entradas", "0") & " - " & gUtilSQL.fIfResultIsNullReplace("Salidas", "0") & ")", "0")
   Sql = Sql & " FROM  " & insInvPermanenteVista.GetViewNameRegistroInventarioPermanente & " AS InventarioInicial"
   Sql = Sql & " WHERE (InventarioInicial.ConsecutivoCompania = MovimientoDeInventario.ConsecutivoCompania) "
   Sql = Sql & " AND (InventarioInicial.CodigoArticulo = MovimientoDeInventario.CodigoArticulo) "
   Sql = Sql & " AND (InventarioInicial.Fecha < " & gUtilSQL.fSimpleSqlValue(valFechaInicial) & "))"
   ExistenciaInicial = Sql
   
   Sql = "SELECT "
   Sql = Sql & " MovimientoDeInventario.Fecha, "
   Sql = Sql & gUtilSQL.getIIF(" dbo.NotaDeEntradaSalida.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoGeneradoPorNotaDeEntradaSalida.eTGPNDES_OrdenDeProduccion), " MovimientoDeInventario.TipoDeOperacion + ' Nro ' + ISNULL(Adm.OrdenDeProduccion.Codigo, '') ", " MovimientoDeInventario.TipoDeOperacion ") + " AS TipoDeOperacion, "
   Sql = Sql & " MovimientoDeInventario.NumeroDeDocumento, "
   Sql = Sql & " MovimientoDeInventario.CodigoArticulo, "
   Sql = Sql & " MovimientoDeInventario.Descripcion, "
   Sql = Sql & " MovimientoDeInventario.UnidadDeVenta, "
   Sql = Sql & ExistenciaInicial & " AS ExistenciaInicial, "
   Sql = Sql & " MovimientoDeInventario.Entradas, "
   Sql = Sql & " MovimientoDeInventario.Salidas "
   Sql = Sql & " FROM  " & insInvPermanenteVista.GetViewNameRegistroInventarioPermanente & " MovimientoDeInventario "
   Sql = Sql & " LEFT JOIN dbo.NotaDeEntradaSalida ON MovimientoDeInventario.ConsecutivoCompania = dbo.NotaDeEntradaSalida.ConsecutivoCompania "
   Sql = Sql & " AND MovimientoDeInventario.NumeroDeDocumento = dbo.NotaDeEntradaSalida.NumeroDocumento "
   Sql = Sql & " LEFT JOIN adm.OrdenDeProduccion ON MovimientoDeInventario.ConsecutivoCompania = adm.OrdenDeProduccion.ConsecutivoCompania "
   Sql = Sql & " AND dbo.NotaDeEntradaSalida.ConsecutivoDocumentoOrigen = adm.OrdenDeProduccion.Consecutivo "
   Sql = Sql & " WHERE MovimientoDeInventario.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valCodigoCompaniaActual)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("MovimientoDeInventario.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " ORDER BY MovimientoDeInventario.CodigoArticulo, MovimientoDeInventario.Fecha"

   Set insInvPermanenteVista = Nothing
h_EXIT: On Error GoTo 0
   fSQLDetalleDelInventarioPermanenteEnUnidadesFisicas = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDetalleDelInventarioPermanenteEnUnidadesFisicas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDetalleDelInventarioPermanenteValorizado(ByVal valCodigoCompaniaActual As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef gGlobalization As Object, ByVal valCostoUltimaCompra As Boolean) As String
   Dim Sql As String
   Dim ExistenciaInicial As String
   Dim insInvPermanenteVista As clsArticuloInvVista
   On Error GoTo h_ERROR
   Set insInvPermanenteVista = New clsArticuloInvVista
   If valCostoUltimaCompra Then
      Sql = fSQLDetalleDelInventarioPermanenteValorizadoUltimaCompra(valCodigoCompaniaActual, valFechaInicial, valFechaFinal)
   Else
      Sql = "SELECT "
      Sql = Sql & " Fecha, "
      Sql = Sql & " TipoDeDocumento, "
      Sql = Sql & " TipoDeOperacion, "
      If gGlobalization.fEsCodigoPeru Then
         Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfInStrSQL("NumeroDeDocumento", "-") & " - 1 > 0", gUtilSQL.fMid("NumeroDeDocumento", "1", gUtilSQL.DfInStrSQL("NumeroDeDocumento", "-") & " - 1"), "NumeroDeDocumento", True) & " AS SerieDeDocumento, "
         Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfInStrSQL("NumeroDeDocumento", "-") & " - 1 > 0", gUtilSQL.fMid("NumeroDeDocumento", gUtilSQL.DfInStrSQL("NumeroDeDocumento", "-") & "+1", gUtilSQL.fLen(insInvPermanenteVista.GetViewNameRegistroInventarioPermanenteValorizado, "NumeroDeDocumento")), "NumeroDeDocumento", True) & " AS NumeroDeDocumento, "
      Else
         Sql = Sql & " NumeroDeDocumento, "
      End If
      Sql = Sql & " CodigoArticulo, "
      Sql = Sql & " Descripcion, "
      Sql = Sql & " UnidadDeVenta, "
      Sql = Sql & " CostoUnitario, "
      Sql = Sql & " Entradas, "
      Sql = Sql & " CostoUnitarioEntrada, "
      Sql = Sql & " CostoTotalEntrante, "
      Sql = Sql & " Salidas, "
      Sql = Sql & " CostoUnitarioSalida, "
      Sql = Sql & " CostoTotalSaliente "
      Sql = Sql & " FROM  " & insInvPermanenteVista.GetViewNameRegistroInventarioPermanenteValorizado
      Sql = Sql & " WHERE ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valCodigoCompaniaActual)
      Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Fecha", valFechaInicial, valFechaFinal)
      Sql = Sql & " ORDER BY CodigoArticulo, Fecha"
   End If
   Set insInvPermanenteVista = Nothing
h_EXIT: On Error GoTo 0
   fSQLDetalleDelInventarioPermanenteValorizado = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDetalleDelInventarioPermanenteValorizado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExistenciaDetalladaDeArticulo(ByVal valSqlCamposFiltro As String, ByVal valSqlFiltro As String, ByVal valSqlOrder As String, ByVal valConsecutivoCompania As String) As String
   Dim Sql As String
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
     
   Sql = "SELECT "
   Sql = Sql & valSqlCamposFiltro
   Sql = Sql & "articuloInventario.Codigo, "
   Sql = Sql & "articuloInventario.TipoArticuloInv, "
   Sql = Sql & "articuloInventario.Descripcion, "
   Sql = Sql & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".Serial,"
   Sql = Sql & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".Rollo,"
   Sql = Sql & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionColor,"
   Sql = Sql & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionTalla,"
   Sql = Sql & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".Existencia,"
   Sql = Sql & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".CantReservada,"
   Sql = Sql & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DisponibilidadDetalle,"
   Sql = Sql & " articuloInventario.Existencia AS ExistenciaMaster,"
   Sql = Sql & " articuloInventario.CantArtReservado AS CantReservadaMaster,"
   Sql = Sql & " articuloInventario.Existencia - articuloInventario.CantArtReservado AS DisponibilidadMaster,"
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " AS StatusdelArticulo "
   Sql = Sql & " From articuloInventario Left Join "
   Sql = Sql & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & " ON "
   Sql = Sql & "  articuloInventario.ConsecutivoCompania = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".ConsecutivoCompania"
   Sql = Sql & "  AND ArticuloInventario.Codigo = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".CodigoArticulo"
   Sql = Sql & sqlWhere
   Sql = Sql & " ORDER BY " & valSqlOrder
   
   Set gEnumProyecto = Nothing
   Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
   fSQLExistenciaDetalladaDeArticulo = Sql
   
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExistenciaDetalladaDeArticulo", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLUbicacionXAlmacenDeArticulo(ByVal valConsecutivoCompania As Long, ByVal valCodigoAlmacen As String, ByVal valLineaDeProducto As String, ByVal valMarca As String, ByVal valCategoria As String, ByVal valOrdenadoPorCodigo As Boolean, ByVal valSerial As String, ByVal valRollo As String, ByVal valCodigoIncio As String, ByVal valCodigoFin As String) As String
   Dim insArticuloView As clsArticuloInvVista
   Dim Sql As String
   On Error GoTo h_ERROR
   Set insArticuloView = New clsArticuloInvVista
   
   Sql = "SELECT Codigo, Descripcion, Serial, Rollo ,Ubicacion, ConsecutivoCompania, CodigoAlmacen, CodigoArticulo "
   Sql = Sql & " From  " & insArticuloView.GetViewNameUbicacionXAlmacenDeArticulo
   Sql = Sql & " WHERE (ConsecutivoCompania = " & valConsecutivoCompania & ") "
   Sql = Sql & "AND (CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen) & ") "
   
   If gTexto.DfLen(valLineaDeProducto) > 0 Then
      Sql = Sql & "AND (LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(valLineaDeProducto) & ") "
   End If
   If gTexto.DfLen(valMarca) > 0 Then
      Sql = gUtilSQL.fSQLValueWithWildcardWithAnd(Sql, "Marca", valMarca)
   End If
   If gTexto.DfLen(valCategoria) > 0 Then
      Sql = Sql & "AND (Categoria = " & gUtilSQL.fSimpleSqlValue(valCategoria) & ") "
   End If
   If gTexto.DfLen(valSerial) > 0 Then
      Sql = gUtilSQL.fSQLValueWithWildcardWithAnd(Sql, "Serial ", valSerial)
   End If
   If gTexto.DfLen(valRollo) > 0 Then
      Sql = gUtilSQL.fSQLValueWithWildcardWithAnd(Sql, "Rollo ", valRollo)
   End If
   If gTexto.DfLen(valCodigoIncio) > 0 And gTexto.DfLen(valCodigoFin) > 0 Then
      Sql = Sql & "AND " & gUtilSQL.fSQLValueBetween("Codigo", valCodigoIncio, valCodigoFin)
   End If
   Sql = Sql & " ORDER BY "
   If valOrdenadoPorCodigo Then
      Sql = Sql & " Codigo, Descripcion"
   Else
      Sql = Sql & " Descripcion "
   End If
   Set insArticuloView = Nothing
h_EXIT: On Error GoTo 0
   fSQLUbicacionXAlmacenDeArticulo = Sql
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
   Dim Sql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   'Artículo Sencillo
   Sql = "SELECT "
   Sql = Sql & " articuloInventario.Codigo AS codigo, articuloInventario.Descripcion as Descripcion, "
   Sql = Sql & " articuloInventario.Existencia as Existencia,"
   Sql = Sql & " (articuloInventario.CantArtReservado) AS CantidadReservada,"
   Sql = Sql & " IGV_InventarioMercanciaEnTransito.CantidadEnTransito as CantidadEnTransito, "
   Sql = Sql & "(articuloInventario.Existencia - articuloInventario.CantArtReservado + IGV_InventarioMercanciaEnTransito.CantidadEnTransito) AS CantidadDisponible, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " as StatusdelArticulo "
   Sql = Sql & " , articuloinventario.LineaDeProducto as LineaDeProducto, "
   Sql = Sql & " articuloInventario.categoria, articuloInventario.marca"
   Sql = Sql & " FROM IGV_InventarioMercanciaEnTransito"
   Sql = Sql & " INNER JOIN ArticuloInventario"
   Sql = Sql & " ON (IGV_InventarioMercanciaEnTransito.CodigoArticulo = ArticuloInventario.Codigo"
   Sql = Sql & " AND IGV_InventarioMercanciaEnTransito.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania)"
   Sql = Sql & " WHERE IGV_InventarioMercanciaEnTransito.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND TipoArticuloInv IN ( " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Simple) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaSerial) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaSerial_Rollo) & ")  "
   Sql = Sql & " AND ArticuloInventario.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_MERCANCIA
   Sql = Sql & " AND IGV_InventarioMercanciaEnTransito.EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(True)
   If valSqlStatus <> "" Then
      Sql = Sql & " AND ArticuloInventario.StatusdelArticulo = " & valSqlStatus
   End If
   If valRango Then
      Sql = Sql & " AND " & gUtilSQL.fSQLStringValueBetween("ArticuloInventario.Codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   End If
   If valLineaProducto Then
      Sql = Sql & " AND ArticuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrLineaProducto))
   End If
   If valCategoria Then
      Sql = Sql & " AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrCategoria))
   End If
   If valMarca Then
      Sql = Sql & " AND ArticuloInventario.Marca = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrMarca))
   End If
   Sql = Sql & " UNION"
   'OtroArticulos
   Sql = Sql & " SELECT IGV_InventarioExistenciaXGrupo.Codigo AS codigo, IGV_InventarioExistenciaXGrupo.Descripcion as Descripcion,"
   Sql = Sql & " IGV_InventarioExistenciaXGrupo.Existencia as Existencia,"
   Sql = Sql & " (IGV_InventarioExistenciaXGrupo.CantidadReservada) AS CantidadReservada,"
   Sql = Sql & " IGV_InventarioMercanciaEnTransito.CantidadEnTransito as CantidadEnTransito,"
   Sql = Sql & " (IGV_InventarioExistenciaXGrupo.Existencia - IGV_InventarioExistenciaXGrupo.CantidadReservada +"
   Sql = Sql & " IGV_InventarioMercanciaEnTransito.CantidadEnTransito) AS CantidadDisponible, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("IGV_InventarioExistenciaXGrupo.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, _
                        gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " as StatusdelArticulo "
   Sql = Sql & " , IGV_InventarioExistenciaXGrupo.LineaDeProducto as LineaDeProducto, "
   Sql = Sql & " IGV_InventarioExistenciaXGrupo.categoria, IGV_InventarioExistenciaXGrupo.marca "
   Sql = Sql & " FROM IGV_InventarioMercanciaEnTransito"
   Sql = Sql & " INNER JOIN IGV_InventarioExistenciaXGrupo"
   Sql = Sql & " ON (IGV_InventarioMercanciaEnTransito.CodigoArticulo = IGV_InventarioExistenciaXGrupo.CodigoDelArticulo"
   Sql = Sql & " AND IGV_InventarioMercanciaEnTransito.ConsecutivoCompania = IGV_InventarioExistenciaXGrupo.ConsecutivoCompania)"
   Sql = Sql & " WHERE IGV_InventarioMercanciaEnTransito.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND TipoArticuloInv IN ( " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_Color) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial) & ")  "
   Sql = Sql & " AND IGV_InventarioExistenciaXGrupo.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_MERCANCIA
   Sql = Sql & " AND IGV_InventarioMercanciaEnTransito.EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(True)
   If valSqlStatus <> "" Then
      Sql = Sql & " AND IGV_InventarioExistenciaXGrupo.StatusdelArticulo = " & valSqlStatus
   End If
   If valRango Then
      Sql = Sql & " AND " & gUtilSQL.fSQLStringValueBetween("IGV_InventarioExistenciaXGrupo.Codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   End If
   If valLineaProducto Then
      Sql = Sql & " AND IGV_InventarioExistenciaXGrupo.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrLineaProducto))
   End If
   If valCategoria Then
      Sql = Sql & " AND IGV_InventarioExistenciaXGrupo.Categoria = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrCategoria))
   End If
   If valMarca Then
      Sql = Sql & " AND IGV_InventarioExistenciaXGrupo.Marca = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrMarca))
   End If
   Sql = Sql & " ORDER BY " & valSqlOrder
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLExistenciaMercanciaEnTransito = Sql
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
   Dim Sql As String
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

   Sql = Sql & " ;WITH  ArticulosyCostos(ConsecutivoCompania,"
   Sql = Sql & " Codigo,"
   Sql = Sql & " Descripcion,"
   Sql = Sql & " entrada,"
   Sql = Sql & " Salida,"
   Sql = Sql & " Autoconsumo,"
   Sql = Sql & " Retiro,ExistenciaAnterior)"
   Sql = Sql & " AS( "
   Sql = Sql & " SELECT "
   Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania ,"
   Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimiento() & ".codigo,"
   Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimiento() & ".Descripcion,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS ENTRADA ,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS Salida ,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS Autoconsumo,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS Retiro,"
   Sql = Sql & " Sum (ENTRADA) - Sum(SALIDAD)   AS ExistenciaAnterior"
   Sql = Sql & " FROM ArticuloInventario "
   Sql = Sql & " LEFT JOIN " & insArticuloView.GetViewNameArticuloInvMovimiento() & " ON "
   Sql = Sql & " ArticuloInventario.Codigo = " & insArticuloView.GetViewNameArticuloInvMovimiento() & ".Codigo  AND "
   Sql = Sql & " ArticuloInventario.ConsecutivoCompania = " & insArticuloView.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania "
   Sql = Sql & sqlWhere
   Sql = Sql & " GROUP BY "
   Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania,"
   Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimiento() & ".codigo, "
   Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimiento() & ".Descripcion "
   Sql = Sql & " UNION "
   Sql = Sql & " SELECT "
   Sql = Sql & " ArticuloInventario.ConsecutivoCompania,"
   Sql = Sql & " ArticuloInventario.Codigo,"
   Sql = Sql & " ArticuloInventario.Descripcion,"
   Sql = Sql & " SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ENTRADA"), gUtilSQL.fNumToStrSQL(0), "ENTRADA", False) & " )  AS ENTRADA, "
   Sql = Sql & " SUM( " & gUtilSQL.getIIF(vSQLComparacion, gUtilSQL.fNumToStrSQL(0), insArticuloView.GetViewNameArticuloInvMovimiento() & ".salidad", False) & " )  AS Salida,"
   Sql = Sql & " SUM( " & gUtilSQL.getIIF(insArticuloView.GetViewNameArticuloInvMovimiento() & ".TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeOperacionToString(eTO_Autoconsumo)), insArticuloView.GetViewNameArticuloInvMovimiento() & ".salidad", gUtilSQL.fNumToStrSQL(0), False) & " ) AS Autoconsumo, "
   Sql = Sql & " SUM( " & gUtilSQL.getIIF(insArticuloView.GetViewNameArticuloInvMovimiento() & ".TipoDeDocumento= " & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeOperacionToString(eTO_Retiro)), insArticuloView.GetViewNameArticuloInvMovimiento() & ".salidad", gUtilSQL.fNumToStrSQL(0), False) & " ) AS Retiro, "
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS ExistenciaAnterior "
   Sql = Sql & " FROM ArticuloInventario "
   Sql = Sql & " LEFT JOIN " & insArticuloView.GetViewNameArticuloInvMovimiento() & " ON "
   Sql = Sql & gUtilSQL.fSQLValueDatesBetween("", insArticuloView.GetViewNameArticuloInvMovimiento() & ".Fecha ", valFechaInicial, valFechaFinal) & " AND "
   Sql = Sql & " ArticuloInventario.Codigo = " & insArticuloView.GetViewNameArticuloInvMovimiento() & ".Codigo  AND "
   Sql = Sql & " ArticuloInventario.ConsecutivoCompania = " & insArticuloView.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania "
   Sql = Sql & sqlWhere2
   Sql = Sql & " Group By "
   Sql = Sql & " ArticuloInventario.ConsecutivoCompania,"
   Sql = Sql & " ArticuloInventario.codigo,"
   Sql = Sql & " ArticuloInventario.Descripcion"
   Sql = Sql & " )"

   Sql = Sql & "  SELECT "
   Sql = Sql & " ArticulosyCostos.codigo,"
   Sql = Sql & " ArticulosyCostos.Descripcion,"
   Sql = Sql & " sum(ArticulosyCostos.ExistenciaAnterior)  AS ExistenciaAnterior,"
   Sql = Sql & " sum(ArticulosyCostos.Entrada) AS Entrada,"
   Sql = Sql & " sum(ArticulosyCostos.Salida)  AS Salida,"
   Sql = Sql & " sum(ArticulosyCostos.Retiro)  AS Retiro,"
   Sql = Sql & " sum(ArticulosyCostos.Autoconsumo) AS Autoconsumo,"
   Sql = Sql & " (sum(ArticulosyCostos.ExistenciaAnterior) + sum(ArticulosyCostos.Entrada) - sum(ArticulosyCostos.Salida) - sum(ArticulosyCostos.Retiro)- sum(ArticulosyCostos.Autoconsumo))AS ExistenciaActual, "
   Sql = Sql & vCostoFinal & "  *  sum(ArticulosyCostos.Entrada) AS EntradaMoneda,"
   Sql = Sql & vCostoFinal & "  *  sum(ArticulosyCostos.Salida)  AS SalidaMoneda,"
   Sql = Sql & vCostoFinal & "  *  sum(Retiro)  AS RetiroMoneda,"
   Sql = Sql & vCostoFinal & "  *  sum(Autoconsumo) AS AutoconsumoMoneda,"
   Sql = Sql & " (sum(ArticulosyCostos.ExistenciaAnterior) + sum(ArticulosyCostos.Entrada) - sum(ArticulosyCostos.Salida) - sum(ArticulosyCostos.Retiro)- sum(ArticulosyCostos.Autoconsumo)) *  " & vCostoFinal & " AS ExistenciaMoneda,"
   Sql = Sql & vCostoInicial & " As COSTO_UNITARIO"
   Sql = Sql & " FROM "
   Sql = Sql & " ArticulosyCostos LEFT JOIN "
   Sql = Sql & "  dbo.Gf_CostosUltimaCompra(" & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaInicial) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde)) & "," & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta)) & ") AS TBLCostoUltimaCompra ON "
   Sql = Sql & " ArticulosyCostos.codigo = TBLCostoUltimaCompra.codigo AND "
   Sql = Sql & " ArticulosyCostos.ConsecutivoCompania = TBLCostoUltimaCompra.ConsecutivoCompania "
   Sql = Sql & "  Group By "
   Sql = Sql & "  ArticulosyCostos.ConsecutivoCompania,"
   Sql = Sql & "  ArticulosyCostos.codigo,"
   Sql = Sql & "  ArticulosyCostos.Descripcion,"
   Sql = Sql & "  costoinicial,"
   Sql = Sql & "  costofinal,"
   Sql = Sql & " FechaInicial, FechaFinal"
   Sql = Sql & "  order by ArticulosyCostos.codigo"
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
  fSqlCostoUltimaCompraReporte = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCostoUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLRegistroDetalladadoInventario(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valEsSistemaInterno As Boolean, ByVal valConsecutivoCompania As Long) As String
   Dim Sql As String
   Dim sqlFacturaOut As String
   Dim sqlFacturaProductoCompuestoOut As String
   On Error GoTo h_ERROR
   sqlFacturaOut = fSQLFacturaParaRegistroDetallado(valFechaInicial, valFechaFinal, valConsecutivoCompania)
   sqlFacturaProductoCompuestoOut = fSQLFacturaPCParaRegistroDetallado(valFechaInicial, valFechaFinal, valConsecutivoCompania)
   Sql = sqlFacturaOut
   Sql = Sql & " UNION " & sqlFacturaProductoCompuestoOut
   Sql = Sql & " UNION " & fSQLExistenciaParaRegistroDetallado(valConsecutivoCompania)
   If Not valEsSistemaInterno Then
      Sql = Sql & " UNION " & fSQLNotaESParaRegistroDetallado(valFechaInicial, valFechaFinal, valConsecutivoCompania)
      Sql = Sql & " UNION " & fSQLComprasParaRegistroDetallado(valFechaInicial, valFechaFinal, valConsecutivoCompania)
      Sql = Sql & " UNION " & fSQLRegistroDetalladoConteoFisico(valFechaInicial, valFechaFinal, valConsecutivoCompania)
   End If
   Sql = Sql & " UNION " & fSQLFacturaParaExistenciaAnterior(valFechaInicial, valConsecutivoCompania)
   Sql = Sql & " UNION " & fSQLFacturaPCParaExistenciaAnterior(valFechaInicial, valConsecutivoCompania)
   If Not valEsSistemaInterno Then
      Sql = Sql & " UNION " & fSQLNotaESParaExistenciaAnterior(valFechaInicial, valConsecutivoCompania)
      Sql = Sql & " UNION " & fSQLConteoFisicoParaExistenciaAnterior(valFechaInicial, valConsecutivoCompania)
      Sql = Sql & " UNION " & fSQLComprasParaExistenciaAnterior(valFechaInicial, valConsecutivoCompania)
   End If
   fSQLRegistroDetalladadoInventario = Sql
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
   Dim vSqlWhere As String
   Dim sqlOperacion As String
   On Error GoTo h_ERROR
   sqlOperacion = "RenglonNotaES.Cantidad * ArticuloInventario.CostoUnitario"
  
   vSqlWhere = gUtilSQL.fSQLValueDatesBetween("", "NotaDeEntradaSalida.Fecha", valFechaInicial, valFechaFinal)
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "StatusNotaEntradaSalida", enum_StatusNotaEntradaSalida.eSNES_Vigente)
   vSqlWhere = vSqlWhere & " AND ArticuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO)
   vSqlWhere = vSqlWhere & " AND ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   
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
   
   sqlNotaES = sqlNotaES & vSqlWhere
   
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
   sqlNotaES = sqlNotaES & vSqlWhere
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
   Dim Sql As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "ArticuloInventario.Codigo, "
   Sql = Sql & "ArticuloInventario.Descripcion, "
   Sql = Sql & "0 AS ExistenciaAnterior, "
   Sql = Sql & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia >= 0 ", "RenglonConteoFisico.Diferencia", " 0 ") & " as Entrada, "
   Sql = Sql & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia < 0 ", "RenglonConteoFisico.Diferencia * (-1) ", " 0 ") & " As Salida, "
   Sql = Sql & "0 As  Retiro, 0 AS Autoconsumo, "
   Sql = Sql & "0 as ExistenciaActual, "
   Sql = Sql & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia >= 0 ", "RenglonConteoFisico.Diferencia * ArticuloInventario.CostoUnitario", " 0 ") & " as EntradaMoneda, "
   Sql = Sql & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia < 0 ", "RenglonConteoFisico.Diferencia * ArticuloInventario.CostoUnitario* (-1)", " 0 ") & " As SalidaMoneda, "
   Sql = Sql & "0 As  RetiroMoneda, 0 AS AutoconsumoMoneda, "
   Sql = Sql & "0 as ExistenciaMoneda, "
   Sql = Sql & gUtilSQL.DfCStrSQL("ConteoFisico.ConsecutivoConteo") & " As Numero, "
   Sql = Sql & "RenglonConteoFisico.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   Sql = Sql & "0 As COSTO_UNITARIO"
   Sql = Sql & " FROM ConteoFisico  INNER JOIN (ArticuloInventario  INNER JOIN RenglonConteoFisico "
   Sql = Sql & " ON (ArticuloInventario.Codigo = RenglonConteoFisico.CodigoArticulo"
   Sql = Sql & ") AND (ArticuloInventario.ConsecutivoCompania = RenglonConteoFisico.ConsecutivoCompania"
   Sql = Sql & ")) ON (ConteoFisico.ConsecutivoConteo = RenglonConteoFisico.ConsecutivoConteo"
   Sql = Sql & ") AND (ConteoFisico.ConsecutivoCompania = RenglonConteoFisico.ConsecutivoCompania)"
   sqlWhere = " WHERE  (ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   sqlWhere = sqlWhere & " AND (" & gUtilSQL.DfSQLDateValueBetween("ConteoFisico.Fecha", valFechaInicial, valFechaFinal) & ")"
   sqlWhere = sqlWhere & " AND (ConteoFisico.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusConteoFisico.eSC_PROCESADO) & ")"
   sqlWhere = sqlWhere & " AND (ArticuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO) & ")"
   Sql = Sql & sqlWhere
   Sql = Sql & " UNION SELECT RenglonConteoFisico.CodigoArticulo as Codigo, "
   Sql = Sql & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   Sql = Sql & " 0 AS ExistenciaAnterior, "
   Sql = Sql & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia >= 0 ", "RenglonConteoFisico.Diferencia", " 0 ") & " as Entrada, "
   Sql = Sql & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia < 0 ", "RenglonConteoFisico.Diferencia * (-1) ", " 0 ") & " As Salida, "
   Sql = Sql & "0 As  Retiro, 0 AS Autoconsumo, "
   Sql = Sql & "0 as ExistenciaActual, "
   Sql = Sql & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia >= 0 ", "RenglonConteoFisico.Diferencia * ArticuloInventario.CostoUnitario", " 0 ") & " as EntradaMoneda, "
   Sql = Sql & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia < 0 ", "RenglonConteoFisico.Diferencia * ArticuloInventario.CostoUnitario* (-1)", " 0 ") & " As SalidaMoneda, "
   Sql = Sql & "0 As  RetiroMoneda, 0 AS AutoconsumoMoneda, "
   Sql = Sql & "0 as ExistenciaMoneda, "
   Sql = Sql & gUtilSQL.DfCStrSQL("ConteoFisico.ConsecutivoConteo") & " As Numero, "
   Sql = Sql & "RenglonConteoFisico.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   Sql = Sql & "0 As COSTO_UNITARIO"
   Sql = Sql & " FROM conteoFisico INNER JOIN ((((renglonConteoFisico INNER JOIN ExistenciaPorGrupo ON "
   Sql = Sql & " (renglonConteoFisico.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla)) "
   Sql = Sql & " AND (renglonConteoFisico.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) LEFT JOIN articuloInventario ON "
   Sql = Sql & " (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo))"
   Sql = Sql & " LEFT JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)) "
   Sql = Sql & " LEFT JOIN Talla ON (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla)) "
   Sql = Sql & " ON (conteoFisico.ConsecutivoConteo = renglonConteoFisico.ConsecutivoConteo) AND (conteoFisico.ConsecutivoCompania = renglonConteoFisico.ConsecutivoCompania)"
   Sql = Sql & sqlWhere
   fSQLConteoFisicoParaRegistroDetallado = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLConteoFisicoParaRegistroDetallado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLConteoFisico_Para_ExistenciaAnterior(ByVal valFechaInicial As Date, ByVal valConsecutivoCompania As Long) As String
   Dim Sql As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "ArticuloInventario.Codigo, "
   Sql = Sql & "ArticuloInventario.Descripcion, "
   Sql = Sql & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia >= 0 ", "RenglonConteoFisico.Diferencia", "RenglonConteoFisico.Diferencia") & "as ExsitenciaAnterior, "
   Sql = Sql & "0 as Entrada, "
   Sql = Sql & "0 As Salida, "
   Sql = Sql & " 0 As  Retiro, "
   Sql = Sql & " 0 As  Autoconsumo, "
   Sql = Sql & "0 as ExistenciaActual, "
   Sql = Sql & "0 as EntradaMoneda, "
   Sql = Sql & "0 As SalidaMoneda, "
   Sql = Sql & " 0 As  RetiroMoneda, "
   Sql = Sql & " 0 As  AutoconsumoMoneda, "
   Sql = Sql & "0 as ExistenciaMoneda, "
   Sql = Sql & gUtilSQL.DfCStrSQL("ConteoFisico.ConsecutivoConteo") & " As Numero, "
   Sql = Sql & "RenglonConteoFisico.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   Sql = Sql & "0 As COSTO_UNITARIO"
   Sql = Sql & " FROM ConteoFisico  INNER JOIN (ArticuloInventario  INNER JOIN RenglonConteoFisico "
   Sql = Sql & " ON (ArticuloInventario.Codigo = RenglonConteoFisico.CodigoArticulo"
   Sql = Sql & ") AND (ArticuloInventario.ConsecutivoCompania = RenglonConteoFisico.ConsecutivoCompania"
   Sql = Sql & ")) ON (ConteoFisico.ConsecutivoConteo = RenglonConteoFisico.ConsecutivoConteo"
   Sql = Sql & ") AND (ConteoFisico.ConsecutivoCompania = RenglonConteoFisico.ConsecutivoCompania)"
   sqlWhere = " WHERE  (ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   sqlWhere = sqlWhere & " AND (ConteoFisico.Fecha < " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ")"
   sqlWhere = sqlWhere & " AND (ConteoFisico.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusConteoFisico.eSC_PROCESADO) & ")"
   sqlWhere = sqlWhere & " AND (ArticuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO) & ")"
   Sql = Sql & sqlWhere
   Sql = Sql & " UNION SELECT RenglonConteoFisico.CodigoArticulo as Codigo, "
   Sql = Sql & "(ArticuloInventario.Descripcion+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Color.DescripcionColor"), gUtilSQL.fSimpleSqlValue(" "), "Color.DescripcionColor") & "+" & gUtilSQL.fSimpleSqlValue(" ") & "+"
   Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("Talla.DescripcionTalla"), gUtilSQL.fSimpleSqlValue(" "), "Talla.DescripcionTalla") & ") AS Descripcion,"
   Sql = Sql & gUtilSQL.getIIF("RenglonConteoFisico.Diferencia >= 0 ", "RenglonConteoFisico.Diferencia", "RenglonConteoFisico.Diferencia") & "as ExsitenciaAnterior, "
   Sql = Sql & "0 as Entrada, "
   Sql = Sql & "0 As Salida, "
   Sql = Sql & " 0 As  Retiro, "
   Sql = Sql & " 0 As  Autoconsumo, "
   Sql = Sql & "0 as ExistenciaActual, "
   Sql = Sql & "0 as EntradaMoneda, "
   Sql = Sql & "0 As SalidaMoneda, "
   Sql = Sql & " 0 As  RetiroMoneda, "
   Sql = Sql & " 0 As  AutoconsumoMoneda, "
   Sql = Sql & "0 as ExistenciaMoneda, "
   Sql = Sql & gUtilSQL.DfCStrSQL("ConteoFisico.ConsecutivoConteo") & " As Numero, "
   Sql = Sql & "RenglonConteoFisico.ConsecutivoRenglon AS CONSECUTIVO_RENGLON,"
   Sql = Sql & "0 As COSTO_UNITARIO"
   Sql = Sql & " FROM conteoFisico INNER JOIN ((((renglonConteoFisico INNER JOIN ExistenciaPorGrupo ON "
   Sql = Sql & " (renglonConteoFisico.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla)) "
   Sql = Sql & " AND (renglonConteoFisico.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) LEFT JOIN articuloInventario ON "
   Sql = Sql & " (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo))"
   Sql = Sql & " LEFT JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)) "
   Sql = Sql & " LEFT JOIN Talla ON (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla)) "
   Sql = Sql & " ON (conteoFisico.ConsecutivoConteo = renglonConteoFisico.ConsecutivoConteo) AND (conteoFisico.ConsecutivoCompania = renglonConteoFisico.ConsecutivoCompania)"
   Sql = Sql & sqlWhere
   fSQLConteoFisico_Para_ExistenciaAnterior = Sql
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
   Dim Sql As String
   On Error GoTo h_ERROR
   If valEntrada Then
      Sql = " SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(IGV_ArticuloNotaEntradaSalida_B3.Entradas)", 0) & " AS ExistenciaInicial "
   Else
      Sql = " SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(IGV_ArticuloNotaEntradaSalida_B3.Salidas)", 0) & " AS ExistenciaInicial "
   End If
   Sql = Sql & " FROM notaDeEntradaSalida INNER JOIN IGV_ArticuloNotaEntradaSalida_B3"
   Sql = Sql & " ON (IGV_ArticuloNotaEntradaSalida_B3.NumeroDocumento = notaDeEntradaSalida.NumeroDocumento"
   Sql = Sql & " AND IGV_ArticuloNotaEntradaSalida_B3.ConsecutivoCompania = notaDeEntradaSalida.ConsecutivoCompania)"
   Sql = Sql & " INNER JOIN articuloInventario  ON (articuloInventario.Codigo = IGV_ArticuloNotaEntradaSalida_B3.Codigo"
   Sql = Sql & " AND articuloInventario.ConsecutivoCompania = IGV_ArticuloNotaEntradaSalida_B3.ConsecutivoCompania)"
   Sql = Sql & " WHERE"
   Sql = Sql & " articuloInventario.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto))
   Sql = Sql & " AND (ArticuloInventario.ConsecutivoCompania = " & valCompaniaActual & ")"
   Sql = Sql & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "notaDeEntradaSalida.Fecha", valFechaInicial, "<") & ")"
   Sql = Sql & " AND (articuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO) & ")" & vbCrLf
   If valEntrada Then
      Sql = Sql & " AND (notaDeEntradaSalida.TipodeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO) & ")"
   Else
      Sql = Sql & " AND (notaDeEntradaSalida.TipodeOperacion <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO) & ")"
   End If
   Sql = Sql & " AND (notaDeEntradaSalida.StatusNotaEntradaSalida = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusNotaEntradaSalida.eSNES_Vigente) & ")" & vbCrLf
   
   fSQLExistenciaInicialMovInvNotaES = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLExistenciaInicialMovInvNotaES", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLExistenciaInicialMovInvCompras(ByVal valCodigoProducto As String, ByVal valFechaInicial As Date, ByVal valCompaniaActual As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR

   Sql = " SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(renglonCompra.CantidadRecibida)", 0) & " AS ExistenciaInicial " & vbCrLf
   Sql = Sql & " FROM compra INNER JOIN (articuloInventario  INNER JOIN renglonCompra" & vbCrLf
   Sql = Sql & " ON (articuloInventario.Codigo = renglonCompra.CodigoArticulo)" & vbCrLf
   Sql = Sql & " AND (articuloInventario.ConsecutivoCompania = renglonCompra.ConsecutivoCompania))" & vbCrLf
   Sql = Sql & " ON (compra.NumeroSecuencial = renglonCompra.NumeroSecuencialCompra)" & vbCrLf
   Sql = Sql & " AND (compra.ConsecutivoCompania = renglonCompra.ConsecutivoCompania)" & vbCrLf
   Sql = Sql & " WHERE" & vbCrLf
   Sql = Sql & " articuloInventario.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto))
   Sql = Sql & " AND (ArticuloInventario.ConsecutivoCompania = " & valCompaniaActual & ")"
   Sql = Sql & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "compra.Fecha", valFechaInicial, "<") & ")"
   Sql = Sql & " AND (articuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO) & ")" & vbCrLf
   Sql = Sql & " AND (compra.EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(False) & ")" & vbCrLf
   Sql = Sql & " AND (compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE) & ")" & vbCrLf
   Sql = Sql & " AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Simple) & ")" & vbCrLf
   '--*** COMPRA TALLA COLOR
   Sql = Sql & " UNION" & vbCrLf
   Sql = Sql & " SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(renglonCompra.CantidadRecibida)", 0) & " AS ExistenciaInicial " & vbCrLf
   Sql = Sql & " FROM compra INNER JOIN renglonCompra" & vbCrLf
   Sql = Sql & " ON (compra.NumeroSecuencial = renglonCompra.NumeroSecuencialCompra AND compra.ConsecutivoCompania = renglonCompra.ConsecutivoCompania)" & vbCrLf
   Sql = Sql & " INNER JOIN ExistenciaPorGrupo ON (ExistenciaPorGrupo.ConsecutivoCompania = RenglonCompra.ConsecutivoCompania" & vbCrLf
   Sql = Sql & " AND ExistenciaPorGrupo.CodigoArticulo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla = RenglonCompra.CodigoArticulo)" & vbCrLf
   Sql = Sql & " INNER JOIN ArticuloInventario ON (ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania" & vbCrLf
   Sql = Sql & " AND ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)" & vbCrLf
   Sql = Sql & " WHERE" & vbCrLf
   Sql = Sql & " articuloInventario.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto))
   Sql = Sql & " AND (ArticuloInventario.ConsecutivoCompania = " & valCompaniaActual & ")"
   Sql = Sql & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "compra.Fecha", valFechaInicial, "<") & ")"
   Sql = Sql & " AND (articuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO) & ")" & vbCrLf
   Sql = Sql & " AND (compra.EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(False) & ")" & vbCrLf
   Sql = Sql & " AND (compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE) & ")" & vbCrLf
   Sql = Sql & " AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_Color) & ")"
   '--*** COMPRA SERIAL
   Sql = Sql & " UNION" & vbCrLf
   Sql = Sql & " SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(RenglonCompraXSerial.Cantidad)", 0) & " AS ExistenciaInicial " & vbCrLf
   Sql = Sql & " FROM compra INNER JOIN RenglonCompraXSerial" & vbCrLf
   Sql = Sql & " ON (compra.NumeroSecuencial = RenglonCompraXSerial.NumeroSecuencialCompra AND compra.ConsecutivoCompania = RenglonCompraXSerial.ConsecutivoCompania)" & vbCrLf
   Sql = Sql & " INNER JOIN ExistenciaPorGrupo ON (ExistenciaPorGrupo.ConsecutivoCompania = RenglonCompraXSerial.ConsecutivoCompania" & vbCrLf
   Sql = Sql & " AND ExistenciaPorGrupo.CodigoArticulo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla = RenglonCompraXSerial.CodigoArticulo" & vbCrLf
   Sql = Sql & " AND ExistenciaPorGrupo.Serial = RenglonCompraXSerial.Serial AND ExistenciaPorGrupo.Rollo = RenglonCompraXSerial.Rollo)" & vbCrLf
   Sql = Sql & " INNER JOIN ArticuloInventario ON (ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania" & vbCrLf
   Sql = Sql & " AND ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)" & vbCrLf
   Sql = Sql & " WHERE articuloInventario.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto))
   Sql = Sql & " AND (compra.ConsecutivoCompania = " & valCompaniaActual & ")" & vbCrLf
   Sql = Sql & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "compra.Fecha", valFechaInicial, "<") & ")"
   Sql = Sql & " AND (articuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO) & ")" & vbCrLf
   Sql = Sql & " AND (compra.EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(False) & ")" & vbCrLf
   Sql = Sql & " AND (compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE) & ")" & vbCrLf
   Sql = Sql & " AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaSerial) & " OR ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaSerial_Rollo) & ")" & vbCrLf
   '--*** COMPRA TALLA COLOR SERIAL
   Sql = Sql & " UNION" & vbCrLf
   Sql = Sql & " SELECT " & gUtilSQL.fIfResultIsNullReplace("SUM(RenglonCompraXSerial.Cantidad)", 0) & " AS ExistenciaInicial " & vbCrLf
   Sql = Sql & " FROM compra INNER JOIN RenglonCompraXSerial" & vbCrLf
   Sql = Sql & " ON (compra.NumeroSecuencial = RenglonCompraXSerial.NumeroSecuencialCompra AND compra.ConsecutivoCompania = RenglonCompraXSerial.ConsecutivoCompania)" & vbCrLf
   Sql = Sql & " INNER JOIN ExistenciaPorGrupo ON (ExistenciaPorGrupo.ConsecutivoCompania = RenglonCompraXSerial.ConsecutivoCompania" & vbCrLf
   Sql = Sql & " AND ExistenciaPorGrupo.CodigoArticulo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla = RenglonCompraXSerial.CodigoArticulo" & vbCrLf
   Sql = Sql & " AND ExistenciaPorGrupo.Serial = RenglonCompraXSerial.Serial AND ExistenciaPorGrupo.Rollo = RenglonCompraXSerial.Rollo)" & vbCrLf
   Sql = Sql & " INNER JOIN ArticuloInventario ON (ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania" & vbCrLf
   Sql = Sql & " AND ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)" & vbCrLf
   Sql = Sql & " WHERE articuloInventario.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoProducto))
   Sql = Sql & " AND (compra.ConsecutivoCompania = " & valCompaniaActual & ")" & vbCrLf
   Sql = Sql & " AND (" & gUtilSQL.fSQLValueDateWithComparisonRule("", "compra.Fecha", valFechaInicial, "<") & ")"
   Sql = Sql & " AND (articuloInventario.TipoDeArticulo <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO) & ")" & vbCrLf
   Sql = Sql & " AND (compra.EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(False) & ")" & vbCrLf
   Sql = Sql & " AND (compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE) & ")" & vbCrLf
   Sql = Sql & " AND (ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial) & ")" & vbCrLf


   fSQLExistenciaInicialMovInvCompras = Sql
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
   Sql = "SELECT "
   Sql = Sql & SQLCodigoArticulo & " AS CodArt, "
   Sql = Sql & "articuloInventario.Descripcion, "
   Sql = Sql & "articuloInventario.LineaDeProducto,  "
   Sql = Sql & "articuloInventario.Marca,  "
   Sql = Sql & "articuloInventario.PrecioSinIva, "
   Sql = Sql & "articuloInventario.PrecioSinIva2, "
   Sql = Sql & "articuloInventario.PrecioSinIva3, "
   Sql = Sql & "articuloInventario.PrecioSinIva4, "
   Sql = Sql & "articuloInventario.PrecioConIva, "
   Sql = Sql & "articuloInventario.PrecioConIva2, "
   Sql = Sql & "articuloInventario.PrecioConIva3, "
   Sql = Sql & "articuloInventario.PrecioConIva4, "
   Sql = Sql & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioSinIva", "articuloInventario.FechaUltimaModificacion") & " AS PrecioSinIvaEnBsf, "
   Sql = Sql & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioSinIva2", "articuloInventario.FechaUltimaModificacion") & " AS PrecioSinIVA2EnBsf, "
   Sql = Sql & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioSinIva3", "articuloInventario.FechaUltimaModificacion") & " AS PrecioSinIVA3EnBsf, "
   Sql = Sql & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioSinIva4", "articuloInventario.FechaUltimaModificacion") & " AS PrecioSinIVA4EnBsf, "
   Sql = Sql & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioConIva", "articuloInventario.FechaUltimaModificacion") & " AS PrecioConIVAEnBsf, "
   Sql = Sql & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioConIva2", "articuloInventario.FechaUltimaModificacion") & " AS PrecioConIVA2EnBsf, "
   Sql = Sql & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioConIva3", "articuloInventario.FechaUltimaModificacion") & " AS PrecioConIVA3EnBsf, "
   Sql = Sql & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(DateInitBsF), "articuloInventario.PrecioConIva4", "articuloInventario.FechaUltimaModificacion") & " AS PrecioConIVA4EnBsf, "
   Sql = Sql & "articuloInventario.Existencia, "
   Sql = Sql & "articuloInventario.CantArtReservado, "
   Sql = Sql & "articuloInventario.Existencia - articuloInventario.CantArtReservado as Disponibiliad, "
   If valUsaMultiMoneda And valTasaMoneda > 0 And Not valEsMonedaLocalATasaDelDia Then
      Sql = Sql & "articuloInventario.PrecioSinIva/" & SQLTasaMoneda & " AS MePrecioSinIVA, "
      Sql = Sql & "articuloInventario.PrecioSinIva2/" & SQLTasaMoneda & " AS MePrecioSinIVA2, "
      Sql = Sql & "articuloInventario.PrecioSinIva3/" & SQLTasaMoneda & " MePrecioSinIVA3, "
      Sql = Sql & "articuloInventario.PrecioSinIva4/" & SQLTasaMoneda & " AS MePrecioSinIVA4, "
      Sql = Sql & "articuloInventario.PrecioConIva/" & SQLTasaMoneda & " AS MePrecioConIVA, "
      Sql = Sql & "articuloInventario.PrecioConIva2/" & SQLTasaMoneda & " AS MePrecioConIVA2, "
      Sql = Sql & "articuloInventario.PrecioConIva3/" & SQLTasaMoneda & " AS MePrecioConIVA3, "
      Sql = Sql & "articuloInventario.PrecioConIva4/" & SQLTasaMoneda & " AS MePrecioConIVA4, "
   ElseIf valUsaMultiMoneda And valTasaMoneda >= 0 And valEsMonedaLocalATasaDelDia Then
      Sql = Sql & "ISNULL(camposMonedaExtranjera.MePrecioSinIVA, 0) * " & SQLTasaMoneda & " AS MePrecioSinIVA, "
      Sql = Sql & "ISNULL(camposMonedaExtranjera.MePrecioSinIVA2, 0) * " & SQLTasaMoneda & " AS MePrecioSinIVA2, "
      Sql = Sql & "ISNULL(camposMonedaExtranjera.MePrecioSinIVA3, 0) * " & SQLTasaMoneda & " AS MePrecioSinIVA3, "
      Sql = Sql & "ISNULL(camposMonedaExtranjera.MePrecioSinIVA4, 0) * " & SQLTasaMoneda & " AS MePrecioSinIVA4, "
      Sql = Sql & "ISNULL(camposMonedaExtranjera.MePrecioConIVA, 0) * " & SQLTasaMoneda & " AS MePrecioConIVA, "
      Sql = Sql & "ISNULL(camposMonedaExtranjera.MePrecioConIVA2, 0) * " & SQLTasaMoneda & " AS MePrecioConIVA2, "
      Sql = Sql & "ISNULL(camposMonedaExtranjera.MePrecioConIVA3, 0) * " & SQLTasaMoneda & " AS MePrecioConIVA3, "
      Sql = Sql & "ISNULL(camposMonedaExtranjera.MePrecioConIVA4, 0) * " & SQLTasaMoneda & " AS MePrecioConIVA4, "
   Else
      Sql = Sql & "(CASE WHEN (camposMonedaExtranjera.MePrecioSinIVA Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioSinIVA  end) AS MePrecioSinIVA , "
      Sql = Sql & "(CASE WHEN (camposMonedaExtranjera.MePrecioSinIVA2 Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioSinIVA2  end) AS MePrecioSinIVA2 , "
      Sql = Sql & "(CASE WHEN (camposMonedaExtranjera.MePrecioSinIVA3 Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioSinIVA3  end) AS MePrecioSinIVA3 , "
      Sql = Sql & "(CASE WHEN (camposMonedaExtranjera.MePrecioSinIVA4 Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioSinIVA4  end) AS MePrecioSinIVA4 , "
      Sql = Sql & "(CASE WHEN (camposMonedaExtranjera.MePrecioConIVA Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioConIVA  end) AS MePrecioConIVA , "
      Sql = Sql & "(CASE WHEN (camposMonedaExtranjera.MePrecioConIVA2 Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioConIVA2  end) AS MePrecioConIVA2 , "
      Sql = Sql & "(CASE WHEN (camposMonedaExtranjera.MePrecioConIVA3 Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioConIVA3  end) AS MePrecioConIVA3 , "
      Sql = Sql & "(CASE WHEN (camposMonedaExtranjera.MePrecioConIVA4 Is NULL) THEN 0 ELSE camposMonedaExtranjera.MePrecioConIVA4  end) AS MePrecioConIVA4 , "
   End If
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("articuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " As Status, "
   Sql = Sql & " articuloInventario.CampoDefinible1, "
   Sql = Sql & " articuloInventario.CampoDefinible2, "
   Sql = Sql & " articuloInventario.CampoDefinible3, "
   Sql = Sql & " articuloInventario.CampoDefinible4, "
   Sql = Sql & " articuloInventario.CampoDefinible5"
   If valUsaMultiMoneda And Not valEsMonedaLocalATasaDelDia Then
      Sql = Sql & " , '" & valMonedaReporte & "' AS MonedaReporte"
   End If
   Sql = Sql & " FROM articuloInventario LEFT JOIN camposMonedaExtranjera"
   Sql = Sql & " ON articuloInventario.Codigo = camposMonedaExtranjera.Codigo"
   Sql = Sql & " AND articuloInventario.ConsecutivoCompania  = camposMonedaExtranjera.ConsecutivoCompania"
   Sql = Sql & " WHERE articuloInventario.ConsecutivoCompania  = " & valCompaniaActual
   If valCantidadAImprimirLineaDeProducto Then
      Sql = Sql & " AND articuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductoAImprimir))
   End If
   If valStatus = enum_StatusArticulo.eSA_VIGENTE Or valStatus = enum_StatusArticulo.eSA_DESINCORPORADO Then
      Sql = Sql & " AND StatusdelArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(valStatus)
   End If
   Sql = Sql & " AND articuloInventario.Codigo NOT IN (" & _
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
   Sql = Sql & " ORDER BY "
   Sql = Sql & " articuloInventario.LineaDeProducto, "
      If valOrdenarPorCodigo Then
         Sql = Sql & SQLCodigoArticulo & ", "
         Sql = Sql & "articuloInventario.Descripcion"
      Else
         Sql = Sql & "articuloInventario.Descripcion, "
      Sql = Sql & SQLCodigoArticulo
      End If
   Else
      Sql = Sql & " ORDER BY "
      If valOrdenarPorCodigo Then
         Sql = Sql & SQLCodigoArticulo & ", "
         Sql = Sql & "articuloInventario.Descripcion, "
      Else
         Sql = Sql & "articuloInventario.Descripcion, "
         Sql = Sql & SQLCodigoArticulo & ", "
      End If
      Sql = Sql & "articuloInventario.LineaDeProducto"
   End If
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteListaDePrecios = Sql
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
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "INSERT INTO articuloInventario(ConsecutivoCompania, "
   Sql = Sql & "Codigo, Descripcion, "
   Sql = Sql & "LineaDeProducto, StatusdelArticulo, "
   Sql = Sql & "TipoDeArticulo, AlicuotaIva, "
   Sql = Sql & "PrecioSinIva, PrecioConIva, "
   Sql = Sql & "PrecioSinIva2, PrecioConIva2, "
   Sql = Sql & "PrecioSinIva3, PrecioConIva3, "
   Sql = Sql & "PrecioSinIva4, PrecioConIva4, "
   Sql = Sql & "PorcentajeBaseImponible, CostoUnitario, "
   Sql = Sql & "Existencia, CantidadMinima, CantidadMaxima, "
   Sql = Sql & "Categoria, TipoDeProducto, "
   Sql = Sql & "NombrePrograma, Marca, "
   Sql = Sql & "FechaDeVencimiento, UnidadDeVenta, "
   Sql = Sql & "CampoDefinible1, CampoDefinible2, "
   Sql = Sql & "CampoDefinible3, CampoDefinible4, "
   Sql = Sql & "CampoDefinible5, MeCostoUnitario, "
   Sql = Sql & "UnidadDeVentaSecundaria, CuentaContableIngreso, "
   Sql = Sql & "ExcluirDeComision, "
   Sql = Sql & "CodigoLote, NombreOperador, FechaUltimaModificacion, "
   Sql = Sql & "TipoArticuloInv "
   Sql = Sql & ", CostoPromedio"
   Sql = Sql & ", RecalcularCierre "
   Sql = Sql & ", RecalcularCosto "
   Sql = Sql & ", FechaCierreActualizada "
   Sql = Sql & ", CuentaCostoDeVenta"
   Sql = Sql & ", CuentaInventario"
   Sql = Sql & ", ComisionaPorcentaje"
   Sql = Sql & ") SELECT "
   Sql = Sql & valConsecutivoCompania & ", "
   Sql = Sql & gUtilSQL.fSimpleSqlValue(valCodigo) & ", "
   Sql = Sql & gUtilSQL.fSimpleSqlValue(valDescripción) & ", "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("LINEA DE PRODUCTO") & ", "
   Sql = Sql & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusArticulo.eSA_VIGENTE) & ", "
   Sql = Sql & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO) & ", "
   Sql = Sql & gUtilSQL.fSQLSimpleValueForEnum(valAlicuota) & ", "
   Sql = Sql & "0, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("CATEGORIA") & ", "
   Sql = Sql & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeProducto.eTD_TP_NUEVO) & ", "
   Sql = Sql & "'', '', "
   Sql = Sql & gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy) & ", "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("UNIDADES") & ", "
   Sql = Sql & "'', '', '', '', '', 0, 0, '','N', '0', "
   Sql = Sql & gUtilSQL.fSimpleSqlValue(valNombreUsuario) & ", "
   Sql = Sql & gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy) & ",'0'"
   Sql = Sql & ", 0 "
   Sql = Sql & ", " & gUtilSQL.fBooleanToSqlValue(False)
   Sql = Sql & ", " & gUtilSQL.fBooleanToSqlValue(False)
   Sql = Sql & ", " & gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy)
   Sql = Sql & ", " & gUtilSQL.fSimpleSqlValue("")
   Sql = Sql & ", " & gUtilSQL.fSimpleSqlValue("")
   Sql = Sql & ", " & gUtilSQL.fBooleanToSqlValue(True)
   Sql = Sql & " WHERE NOT EXISTS(SELECT ConsecutivoCompania, Codigo FROM articuloInventario WHERE ConsecutivoCompania = " & valConsecutivoCompania & " AND Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigo) & ")"
   
h_EXIT: On Error GoTo 0
   fSQLInsertarArticuloEspecial = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLInsertarArticuloEspecial", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCamposRequeridos(ByVal valNumero As String, ByVal valConsecutivoCompania As Long) As String
   Dim strCampos As String
   Dim Sql As String
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
   Sql = "SELECT " & strCampos & " FROM articuloInventario " & _
         " WHERE Codigo = " & gUtilSQL.fSimpleSqlValue(valNumero) & _
         " AND ConsecutivoCompania = " & valConsecutivoCompania
   fSQLCamposRequeridos = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLCamposRequeridos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryNotaES(ByVal valTipoOperacion As enum_TipodeOperacion, ByVal valCodigoAlmacen As String, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, _
               ByVal valCodigoCompuesto As String, ByVal valCodigo As String, ByVal valSerial As String, ByVal valRollo As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT ISNULL(SUM(RenglonNotaES.Cantidad), 0) AS SumCantidad"
   Sql = Sql & " FROM notaDeEntradaSalida INNER JOIN renglonNotaES "
   Sql = Sql & " ON notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento"
   Sql = Sql & " AND notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania"
   Sql = Sql & " WHERE notaDeEntradaSalida.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND notaDeEntradaSalida.StatusNotaEntradaSalida = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusNotaEntradaSalida.eSNES_Vigente)
   Sql = Sql & " AND notaDeEntradaSalida.CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
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
      Sql = Sql & " AND renglonNotaES.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
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
   fQueryNotaES = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryNotaES", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryTransferencia(ByVal valTipoOperacion As enum_TipodeOperacion, ByVal valCodigoAlmacen As String, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, _
                  ByVal valCodigoCompuesto As String, ByVal valCodigo As String, ByVal valSerial As String, ByVal valRollo As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT ISNULL(SUM(RenglonTransferencia.Cantidad), 0) AS SumCantidad "
   Sql = Sql & " FROM Transferencia INNER JOIN RenglonTransferencia "
   Sql = Sql & " ON Transferencia.NumeroDocumento = RenglonTransferencia.NumeroDocumento"
   Sql = Sql & " AND Transferencia.ConsecutivoCompania = RenglonTransferencia.ConsecutivoCompania"
   Sql = Sql & " WHERE Transferencia.ConsecutivoCompania = " & valConsecutivoCompania
   If valTipoOperacion = eTO_ENTRADA_DE_INVENTARIO Then
      Sql = Sql & " AND Transferencia.CodigoAlmacenEntrada = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
   ElseIf valTipoOperacion = eTO_SALIDA_DE_INVENTARIO Then
      Sql = Sql & " AND Transferencia.CodigoAlmacenSalida = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
   End If
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
      Sql = Sql & " AND RenglonTransferencia.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
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
   fQueryTransferencia = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryTransferencia", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryFactura(ByVal valCodigoAlmacen As String, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, ByVal valCodigoCompuesto As String, _
                  ByVal valCodigo As String, ByVal valSerial As String, ByVal valRollo As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT ISNULL(SUM(Renglonfactura.Cantidad), 0) AS SumCantidad"
   Sql = Sql & " FROM factura INNER JOIN renglonFactura "
   Sql = Sql & " ON factura.Numero = renglonFactura.NumeroFactura "
   Sql = Sql & " AND factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania "
   Sql = Sql & " AND factura.TipoDeDocumento = renglonFactura.TipoDeDocumento "
   Sql = Sql & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND factura.CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
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
      Sql = Sql & " AND renglonFactura.Articulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
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
   fQueryFactura = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryFactura", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryFacturaPC(ByVal valCodigoAlmacen As String, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, ByVal valCodigoCompuesto As String, _
                  ByVal valCodigo As String, ByVal valSerial As String, ByVal valRollo As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT ISNULL(SUM(Renglonfactura.Cantidad*productoCompuesto.cantidad), 0) AS SumCantidad "
   Sql = Sql & " FROM ProductoCompuesto INNER JOIN ArticuloInventario"
   Sql = Sql & "   ON ProductoCompuesto.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania"
   Sql = Sql & "   AND ProductoCompuesto.CodigoConexionConElMaster = ArticuloInventario.Codigo"
   Sql = Sql & "   INNER JOIN factura INNER JOIN renglonFactura"
   Sql = Sql & "   ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania"
   Sql = Sql & "   AND factura.Numero = renglonFactura.NumeroFactura"
   Sql = Sql & "   AND factura.TipoDeDocumento = renglonFactura.TipoDeDocumento"
   Sql = Sql & "   ON ArticuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania"
   Sql = Sql & "   AND ArticuloInventario.Codigo = renglonFactura.Articulo"
   
   Sql = Sql & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND factura.CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
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
      Sql = Sql & " AND productoCompuesto.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
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
   fQueryFacturaPC = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fQueryFacturaPC", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryConteoFisico(ByVal valCodigoAlmacen As String, ByVal valConsecutivoCompania As Long, ByVal valTipoArticuloInvAsEnum As Integer, ByVal valCodigoCompuesto As String, ByVal valCodigo As String, ByVal valSerial As String, ByVal valRollo As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT ISNULL(SUM(Diferencia) ,0) AS SumCantidad"
   Sql = Sql & " FROM ConteoFisico INNER JOIN RenglonConteoFisico "
   Sql = Sql & " ON ConteoFisico.ConsecutivoConteo = RenglonConteoFisico.ConsecutivoConteo"
   Sql = Sql & " AND ConteoFisico.ConsecutivoCompania = RenglonConteoFisico.ConsecutivoCompania"
   Sql = Sql & " WHERE ConteoFisico.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND ConteoFisico.CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
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
      Sql = Sql & " AND RenglonConteoFisico.CodigoArticulo = " & gUtilSQL.fSimpleSqlValue(valCodigo)
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
h_EXIT: On Error GoTo 0
   fQueryConteoFisico = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryConteoFisico", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlCostoUltimaCompraReporteDetallado(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim Sql As String
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
    
 Sql = "; WITH  ArticulosyCostos(ConsecutivoCompania,PrioridadOperacion, NumeroDocumento, "
 Sql = Sql & " Codigo, "
 Sql = Sql & " TipoDeDocumento,"
 Sql = Sql & " Fecha,"
 Sql = Sql & " Descripcion,"
 Sql = Sql & " entrada,"
 Sql = Sql & " salidad,"
 Sql = Sql & " MontoOperacion,"
 Sql = Sql & " existencia,"
 Sql = Sql & " costoAnterior,"
 Sql = Sql & " costoActual) AS "
 Sql = Sql & " ( SELECT "
 Sql = Sql & " ConsecutivoCompania,"
 Sql = Sql & " PrioridadOperacion,"
 Sql = Sql & " NumeroDocumento,"
 Sql = Sql & " Codigo,"
 Sql = Sql & " TipoDeDocumento,"
 Sql = Sql & " Fecha,"
 Sql = Sql & " Descripcion,"
 Sql = Sql & " entrada,"
 Sql = Sql & " salidad,"
 Sql = Sql & " MontoOperacion,"
 Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.Gf_Existencia(ConsecutivoCompania,Fecha,Codigo, PrioridadOperacion, NumeroDocumento)"), gUtilSQL.fNumToStrSQL(0), "dbo.Gf_Existencia(ConsecutivoCompania,Fecha,Codigo, PrioridadOperacion, NumeroDocumento)", False) & " As existencia, "
 Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha-1,Codigo )"), gUtilSQL.fNumToStrSQL(0), "dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha-1,Codigo )", False) & " As costoAnterior,"
 Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha,Codigo )"), gUtilSQL.fNumToStrSQL(0), "dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha,Codigo )", False) & " As costoActual "
 
Sql = Sql & "  FROM   IGV_ArticuloInvMovimiento  "

Sql = Sql & sqlWhere
Sql = Sql & " ) "

Sql = Sql & "  select"
Sql = Sql & "  ArticulosyCostos.codigo,"
Sql = Sql & "  ArticulosyCostos.descripcion,"
Sql = Sql & "  fecha,"
Sql = Sql & "  tipodedocumento,"
Sql = Sql & "  ArticulosyCostos.existencia AS ExistenciaInicial,"
Sql = Sql & "  costoAnterior as CostoInicial,"
Sql = Sql & "  ArticulosyCostos.existencia * costoAnterior  as MontoInicial,"
Sql = Sql & "  entrada ,"
Sql = Sql & gUtilSQL.getIIF("MontoOperacion = 0", "costoAnterior *  entrada", "MontoOperacion") & " As MontoEntrada, "

Sql = Sql & "  salidad,"
Sql = Sql & "  costoAnterior *  salidad as MontoSalidad,"
Sql = Sql & "  ArticulosyCostos.existencia + entrada - salidad as existenciaFinal,"

Sql = Sql & gUtilSQL.getIIF("MontoOperacion = 0", "costoActual", "MontoOperacion / entrada") & " As CostoFinal, "

Sql = Sql & gUtilSQL.getIIF("MontoOperacion = 0", "costoActual", "MontoOperacion / entrada") & " * (ArticulosyCostos.Existencia + entrada - salidad ) As MontoFinal "
Sql = Sql & "   From ArticulosyCostos "
Sql = Sql & "   INNER JOIN  dbo.articuloInventario   ON  "
Sql = Sql & "   ArticulosyCostos.ConsecutivoCompania = articuloInventario.ConsecutivoCompania AND   "
Sql = Sql & "   ArticulosyCostos.Codigo = articuloInventario.Codigo"
Sql = Sql & sqlWhere2

Sql = Sql & "  order by codigo, fecha, PrioridadOperacion, numerodocumento"
    
Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
  fSqlCostoUltimaCompraReporteDetallado = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlExistenciaEntreFechaExitenciaAnterior(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFecha As Date, ByVal valWhere As String) As String
Dim insArticuloView As clsArticuloInvVista
Dim Sql As String
Dim sqlWhere As String
On Error GoTo h_ERROR
Set insArticuloView = New clsArticuloInvVista

sqlWhere = gUtilSQL.fSQLValueDateWithComparisonRule(valWhere, insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Fecha", valFecha, "<")
sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania", valConsecutivoCompania)
sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)

Sql = Sql & " SELECT "

Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania AS ConsecutivoCompania ,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo AS Codigo ,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion    AS Descripcion ,"
Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS CantidadEntrante ,"
Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS CantidadSaliente ,"
Sql = Sql & " SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ENTRADA"), gUtilSQL.fNumToStrSQL(0), "ENTRADA", False) & " ) -  SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("SALIDAD"), gUtilSQL.fNumToStrSQL(0), "SALIDAD", False) & " )   AS ExistenciaAnterior, "
Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS Existencia "

Sql = Sql & " FROM ArticuloInventario "
Sql = Sql & " INNER JOIN " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & " ON "
Sql = Sql & " ArticuloInventario.Codigo = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Codigo  AND "
Sql = Sql & " ArticuloInventario.ConsecutivoCompania = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania "
Sql = Sql & sqlWhere
Sql = Sql & " GROUP BY "
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania ,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion"
Set insArticuloView = Nothing
h_EXIT: On Error GoTo 0
  fSqlExistenciaEntreFechaExitenciaAnterior = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlExistenciaEntreFechaExitenciaAnte", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlExistenciaEntreFechaExitenciaActual(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFecha As Date, ByVal valWhere As String) As String
Dim insArticuloView As clsArticuloInvVista
Dim Sql As String
Dim sqlWhere As String
On Error GoTo h_ERROR
Set insArticuloView = New clsArticuloInvVista

sqlWhere = gUtilSQL.fSQLValueDateWithComparisonRule(valWhere, insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Fecha", valFecha, "<=")
sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania", valConsecutivoCompania)
sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)

Sql = Sql & " SELECT "
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania AS ConsecutivoCompania ,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo AS Codigo ,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion    AS Descripcion ,"
Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS CantidadEntrante ,"
Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS CantidadSaliente ,"
Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS ExistenciaAnterior, "
Sql = Sql & " SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ENTRADA"), gUtilSQL.fNumToStrSQL(0), "ENTRADA", False) & " ) -  SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("SALIDAD"), gUtilSQL.fNumToStrSQL(0), "SALIDAD", False) & " )   AS   Existencia "

Sql = Sql & " FROM ArticuloInventario "
Sql = Sql & " INNER JOIN " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & " ON "
Sql = Sql & " ArticuloInventario.Codigo = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Codigo  AND "
Sql = Sql & " ArticuloInventario.ConsecutivoCompania = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania "
Sql = Sql & sqlWhere

Sql = Sql & " GROUP BY "
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania ,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion"
Set insArticuloView = Nothing
h_EXIT: On Error GoTo 0
  fSqlExistenciaEntreFechaExitenciaActual = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlExistenciaEntreFechaExitenciaActual", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSqlExistenciaEntreFechaEntradasSalidas(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valWhere As String) As String
Dim insArticuloView As clsArticuloInvVista
Dim Sql As String
Dim sqlWhere As String
On Error GoTo h_ERROR
Set insArticuloView = New clsArticuloInvVista

sqlWhere = gUtilSQL.fSQLValueDatesBetween(valWhere, "Fecha", valFechaInicial, valFechaFinal)
sqlWhere = gUtilSQL.fSQLNumberValueWithAnd(sqlWhere, insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania", valConsecutivoCompania)
sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)

Sql = Sql & " SELECT "
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania AS ConsecutivoCompania ,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo AS Codigo ,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion    AS Descripcion ,"

Sql = Sql & " SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ENTRADA"), gUtilSQL.fNumToStrSQL(0), "ENTRADA", False) & " ) AS CantidadEntrante, "
Sql = Sql & " SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("SALIDAD"), gUtilSQL.fNumToStrSQL(0), "SALIDAD", False) & " ) AS CantidadSaliente, "

Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS ExistenciaAnterior, "
Sql = Sql & gUtilSQL.fNumToStrSQL(0) & "  AS Existencia "
Sql = Sql & " FROM ArticuloInventario "
Sql = Sql & " INNER JOIN " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & " ON "
Sql = Sql & " ArticuloInventario.Codigo = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Codigo  AND "
Sql = Sql & " ArticuloInventario.ConsecutivoCompania = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania "
Sql = Sql & sqlWhere

Sql = Sql & " GROUP BY "
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania ,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion"
Set insArticuloView = Nothing
h_EXIT: On Error GoTo 0
 fSqlExistenciaEntreFechaEntradasSalidas = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlExistenciaEntreFechaEntradasSalidas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlInformeExistenciaEntreFechas(ByVal valConsecutivoCompania As Long, ByVal valLineaDeProducto As String, ByVal valMarca As String, ByVal valCategoria As String, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim Sql As String
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
Sql = Sql & " SELECT "
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania AS ConsecutivoCompania ,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo AS Codigo ,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion    AS Descripcion ,"
Sql = Sql & "SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLDateValueBetween(insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Fecha", valFechaInicial, valFechaFinal) & " AND " & gUtilSQL.DfSQLIsNull(" NOT ENTRADA"), "ENTRADA", gUtilSQL.fNumToStrSQL(0), False) & " ) AS CantidadEntrante,"
Sql = Sql & "SUM( " & gUtilSQL.getIIF(gUtilSQL.DfSQLDateValueBetween(insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Fecha", valFechaInicial, valFechaFinal) & " AND " & gUtilSQL.DfSQLIsNull(" NOT SALIDAD"), "SALIDAD", gUtilSQL.fNumToStrSQL(0), False) & " ) AS CantidadSaliente,"
Sql = Sql & " SUM( " & gUtilSQL.getIIF(insArticuloView.GetViewNameArticuloInvMovimientoDetallado & ".Fecha < " & gUtilSQL.fSimpleSqlValue(valFechaInicial) & " AND " & gUtilSQL.DfSQLIsNull("NOT ENTRADA"), "ENTRADA", gUtilSQL.fNumToStrSQL(0), False) & " ) -  SUM( " & gUtilSQL.getIIF(insArticuloView.GetViewNameArticuloInvMovimientoDetallado & ".Fecha < " & gUtilSQL.fSimpleSqlValue(valFechaInicial) & " AND " & gUtilSQL.DfSQLIsNull("NOT SALIDAD"), "SALIDAD", gUtilSQL.fNumToStrSQL(0), False) & " )   AS ExistenciaAnterior, "
Sql = Sql & " SUM( " & gUtilSQL.getIIF(insArticuloView.GetViewNameArticuloInvMovimientoDetallado & ".Fecha <= " & gUtilSQL.fSimpleSqlValue(valFechaFinal) & " AND " & gUtilSQL.DfSQLIsNull("NOT ENTRADA"), "ENTRADA", gUtilSQL.fNumToStrSQL(0), False) & " ) -  SUM( " & gUtilSQL.getIIF(insArticuloView.GetViewNameArticuloInvMovimientoDetallado & ".Fecha <= " & gUtilSQL.fSimpleSqlValue(valFechaFinal) & " AND " & gUtilSQL.DfSQLIsNull("NOT SALIDAD"), "SALIDAD", gUtilSQL.fNumToStrSQL(0), False) & " )   AS Existencia "
Sql = Sql & " FROM ArticuloInventario "
Sql = Sql & " INNER JOIN " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & " ON "
Sql = Sql & " ArticuloInventario.Codigo = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Codigo  AND "
Sql = Sql & " ArticuloInventario.ConsecutivoCompania = " & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania "
Sql = Sql & sqlWhere
Sql = Sql & " GROUP BY "
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".ConsecutivoCompania ,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".CodigoDelArticulo,"
Sql = Sql & insArticuloView.GetViewNameArticuloInvMovimientoDetallado() & ".Descripcion "
Sql = Sql & "ORDER BY CODIGO"
'SQL = fSqlExistenciaEntreFechaExitenciaAnterior(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaInicial, sqlWhere)
'SQL = SQL & " UNION "
'SQL = SQL & fSqlExistenciaEntreFechaEntradasSalidas(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaInicial, valFechaFinal, sqlWhere)
'SQL = SQL & " UNION "
'SQL = SQL & fSqlExistenciaEntreFechaExitenciaActual(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaFinal, sqlWhere)
h_EXIT: On Error GoTo 0
 fSqlInformeExistenciaEntreFechas = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlInformeExistenciaEntreFechas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchArticuloInventarioFactura(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String, ByVal valCodigoAlmacen As String, ByVal valAlicuotas As String, ByVal valPrecio1 As String, ByVal valPrecio2 As String, ByVal strArtResumen As String, ByVal valMonedaExtranjera As Boolean, ByVal vGroupby As String, ByVal valUsaArticulosReservadosPorAlmacen As Boolean, ByVal vaUsaArticulosReservadosPorCotizacion As Boolean, ByVal valBuscarPorSerial As Boolean, ByVal valSerial As String, ByVal valDescripcion As String, ByVal valSqlArtIGTF As String) As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim Sql As String
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
    Sql = fSQLSearchArticuloInventarioFacturaSelect(valConsecutivoCompania, valCodigoArticulo, valCodigoAlmacen, valPrecio1, valPrecio2, sqlWhere, valMonedaExtranjera, vGroupby, valBuscarPorSerial, valSerial, valUsaArticulosReservadosPorAlmacen, vaUsaArticulosReservadosPorCotizacion, valDescripcion)
    Sql = Sql & " UNION "
    Sql = Sql & fSQLSearchArticuloInventarioFacturaSelectCom(valConsecutivoCompania, valCodigoArticulo, valPrecio1, valPrecio2, sqlWhere, valMonedaExtranjera, vGroupby, valCodigoAlmacen, valBuscarPorSerial, valSerial, valUsaArticulosReservadosPorAlmacen, vaUsaArticulosReservadosPorCotizacion, valDescripcion)
    articulosCom = fSQLSearchArticuloInventarioFacturaSelectSoloCompuestos(valConsecutivoCompania, valCodigoArticulo, valPrecio1, valPrecio2, sqlWhere, valMonedaExtranjera, vGroupby, valBuscarPorSerial, valSerial, valDescripcion)
  If (articulosCom <> "") Then
     Sql = Sql & " UNION "
     Sql = Sql & articulosCom
  End If
  
   articulosServicio = fSQLSearchArticuloInventarioFacturaSelectSoloServicios(valConsecutivoCompania, valCodigoArticulo, valPrecio1, valPrecio2, sqlWhere, valMonedaExtranjera, vGroupby, valBuscarPorSerial, valSerial, valDescripcion, strArtResumen, valSqlArtIGTF)
  If (articulosServicio <> "") Then
     Sql = Sql & " UNION "
      Sql = Sql & fSQLSearchArticuloInventarioFacturaSelectSoloServicios(valConsecutivoCompania, valCodigoArticulo, valPrecio1, valPrecio2, sqlWhere, valMonedaExtranjera, vGroupby, valBuscarPorSerial, valSerial, valDescripcion, strArtResumen, valSqlArtIGTF)
  End If
Set insArticuloInvVista = Nothing
h_EXIT:    On Error GoTo 0
   fSQLSearchArticuloInventarioFactura = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchArticuloInventarioFactura", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchArticuloInventarioFacturaSelect(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String, ByVal valCodigoAlmacen As String, ByVal valPrecio1 As String, ByVal valPrecio2 As String, ByVal valsqlWhere As String, ByVal valMonedaExtranjera As Boolean, ByVal vGroupby As String, ByVal valBuscarPorSerial As Boolean, ByVal valSerial As String, ByVal valUsaArticulosReservadosPorAlmacen As Boolean, ByVal vaUsaArticulosReservadosPorCotizacion As Boolean, Optional ByVal valDescripcion As String) As String
Dim insArticuloInvVista As clsArticuloInvVista
Dim vSql As String
Dim vSqlWhere As String
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
     vSqlWhere = gUtilSQL.fSQLValueWithAnd(vSqlWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoDelArticulo", valCodigoArticulo, False)
 End If
 vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
 If valBuscarPorSerial Then
   If valSerial <> "" Then
      If valSerial = "%%" Then
         vSqlWhere = vSqlWhere & " And " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".Serial != " & gUtilSQL.fSimpleSqlValue("0")
      Else
         vSqlWhere = vSqlWhere & "And " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".Serial Like " & gUtilSQL.fSimpleSqlValue(valSerial)
      End If
   End If
 End If
 vSQLCondicionTipoArticulo = " AND ( " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".TipoDeArticulo <> " & enum_TipoDeArticulo.eTD_SERVICIO & ")"
 vSqlWhere = vSqlWhere & vSQLCondicionTipoArticulo
 If valDescripcion <> "" Then
    vSqlWhere = vSqlWhere & "And " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion LIKE " & gUtilSQL.fSimpleSqlValue(valDescripcion)
 End If
 vSqlWhere = gUtilSQL.fSQLValueWithAnd(vSqlWhere, insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoAlmacen", valCodigoAlmacen, False)
 vSqlWhere = vSqlWhere & "AND " & gUtilSQL.fSQLNumberValueWithAnd(valsqlWhere, insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ConsecutivoCompania", valConsecutivoCompania)
 vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
 vSql = ""
 vSql = vSql & " SELECT "
 vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo AS CodigoCompuesto  ,"
 vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion + " & vDescripcionTallaColor & " AS Descripcion , "
 vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto,"
 If vaUsaArticulosReservadosPorCotizacion And valUsaArticulosReservadosPorAlmacen Then
   vSql = vSql & " ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0)" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados,0)" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados,0) AS Disponibilidad,"
 ElseIf valUsaArticulosReservadosPorAlmacen Then
   vSql = vSql & " ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen ,0)" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados,0) AS Disponibilidad,"
 ElseIf vaUsaArticulosReservadosPorCotizacion Then
   vSql = vSql & " ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0)" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados,0) AS Disponibilidad,"
 Else
   vSql = vSql & " ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0) AS Disponibilidad,"
 End If
 vSql = vSql & valPrecio1 & " ,"
 vSql = vSql & valPrecio2 & " ,"
 vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Serial,"
 vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Rollo,"
 vSql = vSql & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CantidadRecibida AS CantidadUltimaCompra,"
 vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".AlicuotaIva,"
 vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoGrupo,"
 vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".TipoArticuloInv,"
 vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
 vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ConsecutivoCompania,"
 vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".PorcentajeBaseImponible,"
 vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoAlmacen,"
 vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen"
 vSql = vSql & " FROM " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1()
vSql = vSql & " INNER JOIN  "
 vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " ON "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania AND "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Serial = " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial AND "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Rollo = " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo AND "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo"
If valUsaArticulosReservadosPorAlmacen Then
   vSql = vSql & " LEFT JOIN  "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen & " ON "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".ConsecutivoCompania AND "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoArticulo AND "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CodigoAlmacen = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoAlmacen"
End If
If vaUsaArticulosReservadosPorCotizacion Then
   vSql = vSql & " LEFT JOIN  "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & " ON "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".ConsecutivoCompania AND "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoArticulo "
End If
vSql = vSql & " LEFT JOIN  "
vSql = vSql & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & " ON "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".ConsecutivoCompania AND "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoAlmacen = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CodigoAlmacen AND  "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CodigoArticulo AND "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Serial = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".Serial AND "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Rollo = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".Rollo "

vSql = vSql & " LEFT JOIN  "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & " ON "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".ConsecutivoCompania AND "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".CodigoArticulo AND "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".Serial AND "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".Rollo AND "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoTalla = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".CodigoTalla AND "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoColor = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".CodigoColor "

vSql = vSql & " LEFT JOIN  "
vSql = vSql & " CamposMonedaExtranjera ON "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = CamposMonedaExtranjera.ConsecutivoCompania AND "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo = CamposMonedaExtranjera.Codigo "

vSql = vSql & vSqlWhere
vSql = vSql & " Group By "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo  ,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Existencia, "
If (valMonedaExtranjera) Then
vSql = vSql & vGroupby & ","
Else
vSql = vSql & valPrecio1 & ","
vSql = vSql & valPrecio2 & ","
End If
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CantArtReservado ,"
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Serial,"
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Rollo,"
vSql = vSql & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CantidadRecibida ,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".AlicuotaIva,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoGrupo,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".TipoArticuloInv,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ConsecutivoCompania,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".PorcentajeBaseImponible,"
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoAlmacen,"
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,"
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionTalla, "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionColor "
If valUsaArticulosReservadosPorAlmacen And vaUsaArticulosReservadosPorCotizacion Then
   vSql = vSql & "," & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados,"
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados"
ElseIf valUsaArticulosReservadosPorAlmacen Then
   vSql = vSql & "," & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados "
ElseIf vaUsaArticulosReservadosPorCotizacion Then
   vSql = vSql & "," & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados"
End If
   Set insArticuloInvVista = Nothing
h_EXIT:    On Error GoTo 0
   fSQLSearchArticuloInventarioFacturaSelect = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchArticuloInventarioFacturaSelect", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchArticuloInventarioFacturaSelectCom(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String, ByVal valPrecio1 As String, ByVal valPrecio2 As String, ByVal valsqlWhere As String, ByVal valMonedaExtranjera As Boolean, ByVal vGroupby As String, ByVal valCodigoAlmacen As String, ByVal valBuscarPorSerial As Boolean, ByVal valSerial As String, ByVal valUsaArticulosReservadosPorAlmacen As Boolean, ByVal vaUsaArticulosReservadosPorCotizacion As Boolean, Optional ByVal valDescripcion As String) As String
Dim insArticuloInvVista As clsArticuloInvVista
Dim vSql As String
Dim vSqlWhere As String
Dim vSQLCondicionTipoArticulo As String
On Error GoTo h_ERROR
Set insArticuloInvVista = New clsArticuloInvVista
 vSQLCondicionTipoArticulo = gUtilSQL.fSQLEnumValueWithAnd("", insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".TipoDeArticulo", enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO)
 vSQLCondicionTipoArticulo = "(" & vSQLCondicionTipoArticulo & ")"
  If gTexto.DfLen(valsqlWhere) > 0 Then
      vSQLCondicionTipoArticulo = valsqlWhere & " AND " & vSQLCondicionTipoArticulo
 End If
 If gTexto.DfLen(valCodigoArticulo) > 0 And valCodigoArticulo <> "*" Then
        vSqlWhere = gUtilSQL.fSQLValueWithAnd(vSqlWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoDelArticulo", valCodigoArticulo, False)
 End If
 vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
 vSqlWhere = vSqlWhere & " And " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoAlmacen = " & gUtilSQL.fSimpleSqlValue(valCodigoAlmacen)
If valBuscarPorSerial Then
   If valSerial <> "" Then
      If valSerial = "%%" Then
         vSqlWhere = vSqlWhere & " And " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".Serial != " & gUtilSQL.fSimpleSqlValue("0")
      Else
         vSqlWhere = vSqlWhere & "And " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".Serial Like " & gUtilSQL.fSimpleSqlValue(valSerial)
      End If
   End If
 End If
 If valDescripcion <> "" Then
    vSqlWhere = vSqlWhere & "And " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion LIKE " & gUtilSQL.fSimpleSqlValue(valDescripcion)
 End If
vSqlWhere = vSqlWhere & " AND " & gUtilSQL.fSQLNumberValueWithAnd(vSQLCondicionTipoArticulo, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
vSql = " SELECT "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoDelArticulo AS CodigoCompuesto, "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".Descripcion,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".LineaDeProducto, "
If vaUsaArticulosReservadosPorCotizacion And valUsaArticulosReservadosPorAlmacen Then
   vSql = vSql & "ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0 )" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados,0)" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados,0) AS Disponibilidad,"
 ElseIf valUsaArticulosReservadosPorAlmacen Then
   vSql = vSql & "ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0 )" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados,0) AS Disponibilidad,"
 ElseIf vaUsaArticulosReservadosPorCotizacion Then
   vSql = vSql & "ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0 )" & _
   " - ISNULL(" & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados,0) AS Disponibilidad,"
 Else
   vSql = vSql & "ISNULL(" & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen,0) AS Disponibilidad,"
 End If
vSql = vSql & valPrecio1 & " ,"
vSql = vSql & valPrecio2 & " ,"
vSql = vSql & gUtilSQL.fSimpleSqlValue("0") & " AS Serial,"
vSql = vSql & gUtilSQL.fSimpleSqlValue("0") & " AS Rollo,"
vSql = vSql & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CantidadRecibida AS CantidadUltimaCompra,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".AlicuotaIva,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoGrupo,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".TipoArticuloInv,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".Codigo,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".ConsecutivoCompania,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".PorcentajeBaseImponible,"
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoAlmacen,"
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ExistenciaAlmacen"
vSql = vSql & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1
vSql = vSql & " LEFT JOIN   CamposMonedaExtranjera ON "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".ConsecutivoCompania = CamposMonedaExtranjera.ConsecutivoCompania AND "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".Codigo = CamposMonedaExtranjera.Codigo  "
vSql = vSql & " LEFT JOIN  " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & " ON "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoArticulo = "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoDelArticulo AND "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".ConsecutivoCompania = "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".ConsecutivoCompania "
vSql = vSql & " LEFT JOIN  "
vSql = vSql & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & " ON "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".ConsecutivoCompania AND "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoAlmacen = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CodigoAlmacen AND  "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".CodigoArticulo AND "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Serial = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".Serial AND "
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() & ".Rollo = " & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales() & ".Rollo "
If valUsaArticulosReservadosPorAlmacen Then
   vSql = vSql & " LEFT JOIN  "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen & " ON "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".ConsecutivoCompania AND "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoArticulo AND "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CodigoAlmacen = " & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoAlmacen"
End If
If vaUsaArticulosReservadosPorCotizacion Then
   vSql = vSql & " LEFT JOIN  "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & " ON "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".ConsecutivoCompania AND "
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CodigoArticulo = " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoDelArticulo "
End If
vSql = vSql & vSqlWhere
vSql = vSql & " Group By "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoDelArticulo , "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".Descripcion,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".LineaDeProducto,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".Existencia, "
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CantArtReservado,"
If (valMonedaExtranjera) Then
vSql = vSql & vGroupby & ","
Else
vSql = vSql & valPrecio1 & ","
vSql = vSql & valPrecio2 & ","
End If
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".AlicuotaIva,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".CodigoGrupo,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".TipoArticuloInv,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".Codigo,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".ConsecutivoCompania,"
vSql = vSql & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 & ".PorcentajeBaseImponible,"
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".CodigoAlmacen,"
vSql = vSql & insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales & ".CantidadRecibida,"
vSql = vSql & insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 & ".ExistenciaAlmacen "
If valUsaArticulosReservadosPorAlmacen And vaUsaArticulosReservadosPorCotizacion Then
   vSql = vSql & "," & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados,"
   vSql = vSql & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados "
ElseIf valUsaArticulosReservadosPorAlmacen Then
   vSql = vSql & "," & insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen() & ".CantArtReservados "
ElseIf vaUsaArticulosReservadosPorCotizacion Then
   vSql = vSql & "," & insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion() & ".CantArtReservados "
End If
   Set insArticuloInvVista = Nothing
h_EXIT:    On Error GoTo 0
 fSQLSearchArticuloInventarioFacturaSelectCom = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchArticuloInventarioFacturaSelectCom", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLSearchArticuloInventarioFacturaSelectSoloCompuestos(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String, ByVal valPrecio1 As String, ByVal valPrecio2 As String, ByVal valsqlWhere As String, ByVal valMonedaExtranjera As Boolean, ByVal vGroupby As String, ByVal valBuscarPorSerial As Boolean, ByVal valSerial As String, Optional ByVal valDescripcion As String) As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vSql As String
   Dim vSqlWhere As String
   Dim vSQLCondicionTipoArticulo As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
 vSQLCondicionTipoArticulo = "ArticuloInventario.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLCondicionTipoArticulo, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   If gTexto.DfLen(valCodigoArticulo) > 0 And valCodigoArticulo <> "*" Then
      vSqlWhere = gUtilSQL.fSQLValueWithAnd(vSqlWhere, "ArticuloInventario.Codigo", valCodigoArticulo, False)
   End If
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   If valBuscarPorSerial Then
      If valSerial <> "" Then
         fSQLSearchArticuloInventarioFacturaSelectSoloCompuestos = ""
         Exit Function
      End If
   End If
   If valDescripcion <> "" Then
      vSqlWhere = vSqlWhere & "And ArticuloInventario.Descripcion LIKE " & gUtilSQL.fSimpleSqlValue(valDescripcion)
   End If
   vSqlWhere = vSqlWhere & " AND articuloInventario.Codigo NOT IN (" & _
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
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   vSql = " SELECT "
   vSql = vSql & " ArticuloInventario.codigo AS CodigoCompuesto, "
   vSql = vSql & " ArticuloInventario.descripcion,"
   vSql = vSql & " ArticuloInventario.LineaDeProducto, "
   vSql = vSql & " ArticuloInventario.Existencia AS Disponibilidad,"
   vSql = vSql & valPrecio1 & " ,"
   vSql = vSql & valPrecio2 & " ,"
   vSql = vSql & " '0' AS Serial,"
   vSql = vSql & " '0' AS Rollo,"
   vSql = vSql & " '0' AS CantidadUltimaCompra,"
   vSql = vSql & " ArticuloInventario.AlicuotaIva,"
   vSql = vSql & " '0' AS CodigoGrupo,"
   vSql = vSql & " ArticuloInventario.TipoArticuloInv,"
   vSql = vSql & " ISNULL(ArticuloInventario.CodigoGrupo,''),"
   vSql = vSql & " ArticuloInventario.ConsecutivoCompania,"
   vSql = vSql & " ArticuloInventario.PorcentajeBaseImponible,"
   vSql = vSql & " '0' AS CodigoAlmacen,"
   vSql = vSql & " ArticuloInventario.Existencia"
   vSql = vSql & " FROM  ArticuloInventario"
   vSql = vSql & " LEFT JOIN   CamposMonedaExtranjera ON "
   vSql = vSql & "ArticuloInventario.ConsecutivoCompania = CamposMonedaExtranjera.ConsecutivoCompania AND "
   vSql = vSql & "ArticuloInventario.Codigo = CamposMonedaExtranjera.Codigo  "
   vSql = vSql & vSqlWhere
   Set insArticuloInvVista = Nothing
h_EXIT:    On Error GoTo 0
 fSQLSearchArticuloInventarioFacturaSelectSoloCompuestos = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchArticuloInventarioFacturaSelectSoloCompuestos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelInformeArticulosXAlmacen(ByVal valCodigoAlmacen As String, ByVal valCantidadAImprimirAlmacen As String, ByVal valConsecutivoCompania As Long)
   Dim Sql As String
   Dim insArticuloView As clsArticuloInvVista
   Dim gEnumReportLocal As clsEnumReport
   On Error GoTo h_ERROR
   Set insArticuloView = New clsArticuloInvVista
   Set gEnumReportLocal = New clsEnumReport
   Sql = "SELECT "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".CodigoAlmacen, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".NombreAlmacen, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".CodigoArticulo, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".Descripcion, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".precioConIva, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".precioSinIva, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".Existencia, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".TipoArticuloInv, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".DescTipoArticuloInv, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".ConsecutivoCompania, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".NombreCompania "
   Sql = Sql & " FROM "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1
   Sql = Sql & " WHERE "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".ConsecutivoCompania = " & valConsecutivoCompania
   If valCantidadAImprimirAlmacen = gEnumReportLocal.enumCantidadAImprimirToString(eCI_uno) Then
      Sql = Sql & " AND " & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".CodigoAlmacen " & _
                  " = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoAlmacen))
   End If
   Sql = Sql & " ORDER BY "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".ConsecutivoCompania, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".CodigoAlmacen, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".TipoArticuloInv, "
   Sql = Sql & insArticuloView.GetViewNameArticuloXAlmacen_pIEB1 & ".CodigoArticulo"
   Set insArticuloView = Nothing
   Set gEnumReportLocal = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLDelInformeArticulosXAlmacen = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSQLDelInformeArticulosXAlmacen", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlUpdateCostoPromedio(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String, ByVal valCostoPromedio As Currency) As String
Dim vWhere As String
 Dim vSql As String
On Error GoTo h_ERROR
h_EXIT: On Error GoTo 0
   vSql = ""
   vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, "Codigo", valCodigoArticulo, False)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSql = " UPDATE    ArticuloInventario SET  "
   vSql = vSql & " CostoPromedio =  " & gUtilSQL.fNumToStrSQL(valCostoPromedio) & " "
   vSql = vSql & vWhere
   fSqlUpdateCostoPromedio = vSql
  Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlUpdateCostoPromedio", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLCargaInicialCierreExistencia(ByVal valConsecutivoCompania As Long) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = "EscargaInicial = " & gUtilSQL.fBooleanToSqlValue(True)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSql = " SELECT Max(Fecha) AS FECHA "
   vSql = vSql & " FROM  CierreCostoDelPeriodo "
   vSql = vSql & vWhere
   vSql = vSql & " Group by FECHA "
  fSQLCargaInicialCierreExistencia = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLCargaInicialCierreExistencia", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCargaCierreAutomatico(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date) As String
   Dim vSql As String
   Dim sqlUltimaCompra As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSql = ""
   valFechaInicial = valFechaInicial - 1
   sqlUltimaCompra = "dbo.Gf_CompraUnaFecha( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania, " & gUtilSQL.fDateToSQLValue(valFechaInicial) & "," & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo)"
   vWhere = gUtilSQL.fSQLEnumValueWithAnd("", "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLValueDateWithComparisonRule(vWhere, insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Fecha", valFechaInicial, "<=")
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSql = " INSERT INTO CierreCostoDelPeriodo (ConsecutivoCompania,Codigo,Fecha,Existencia,Costo,EsCargaInicial)"
   vSql = vSql & " SELECT "
   vSql = vSql & insArticuloInvVista.GetViewNameArticuloInvMovimiento & ".ConsecutivoCompania, "
   vSql = vSql & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo, "
   vSql = vSql & gUtilSQL.fDateToSQLValue(valFechaInicial) & ", "
   vSql = vSql & " SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Entrada ) -   SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Salidad )  AS  Existencia, "
   vSql = vSql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(sqlUltimaCompra), gUtilSQL.fNumToStrSQL(0), sqlUltimaCompra, True) & ","
   vSql = vSql & gUtilSQL.fBooleanToSqlValue(True)
   vSql = vSql & " FROM ArticuloInventario INNER JOIN "
   vSql = vSql & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & " ON "
   vSql = vSql & " ArticuloInventario.ConsecutivoCompania  =  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania AND "
   vSql = vSql & " ArticuloInventario.Codigo =  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo"
   vSql = vSql & vWhere
   vSql = vSql & " Group By "
   vSql = vSql & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania, "
   vSql = vSql & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo, "
   vSql = vSql & " HAVING   (  SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Entrada ) -   SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Salidad ) > " & gUtilSQL.fNumToStrSQL(0) & " ) "
  fSQLCargaCierreAutomatico = vSql
Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLCargaInicialCierreExistencia", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLUpdateArticuloInventarioFechaCierre(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valMarcaRecalcular As Boolean) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = gUtilSQL.fSQLEnumValueWithAnd("", "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   valFechaInicial = valFechaInicial - 1
   vSql = " Update ArticuloInventario "
   vSql = vSql & " SET FechaCierreActualizada  = " & gUtilSQL.fDateToSQLValue(valFechaInicial) & ","
   vSql = vSql & " RecalcularCierre = " & gUtilSQL.fBooleanToSqlValue(valMarcaRecalcular) & ", "
   vSql = vSql & " RecalcularCosto  = " & gUtilSQL.fBooleanToSqlValue(valMarcaRecalcular)
   vSql = vSql & vWhere
  fSQLUpdateArticuloInventarioFechaCierre = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUpdateArticuloInventarioFechaCierre", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLUpdateCargaInicialCierreExistencia(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   valFechaInicial = valFechaInicial - 1
   vWhere = "EscargaInicial = " & gUtilSQL.fBooleanToSqlValue(True)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSql = " Update CierreCostoDelPeriodo "
   vSql = vSql & " SET Fecha  = " & gUtilSQL.fDateToSQLValue(valFechaInicial)
   vSql = vSql & vWhere
   fSQLUpdateCargaInicialCierreExistencia = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUpdateCargaInicialCierreExistencia", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLDELETECargaInicialCierreExistencia(ByVal valConsecutivoCompania As Long) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = " EscargaInicial = " & gUtilSQL.fBooleanToSqlValue(True)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSql = " DELETE CierreCostoDelPeriodo "
   vSql = vSql & vWhere
   fSQLDELETECargaInicialCierreExistencia = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDELETECargaInicialCierreExistencia", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLFechaCierresCostoPromedio(ByVal valConsecutivoCompania As Long, ByVal valFecha As Date) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = " EscargaInicial = " & gUtilSQL.fBooleanToSqlValue(False)
   vWhere = gUtilSQL.fSQLValueDateWithComparisonRule(vWhere, "CierreCostoDelPeriodo.Fecha", valFecha, ">=")
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSql = " SELECT Max(Fecha ) As Fecha  FROM CierreCostoDelPeriodo "
   vSql = vSql & vWhere
   vSql = vSql & "  GROUP BY   ConsecutivoCompania "
   fSQLFechaCierresCostoPromedio = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLFechaCierresCostoPromedio", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLUpdateArticuloInventarioFechaCierreOperacion(ByVal valConsecutivoCompania As Long, ByVal valFechaCierre As Date, ByVal valFechaOperacion As Date, ByVal valDocumento As String, ByVal valOperacion As String) As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vSql As String
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
   vSql = " UPDATE articuloInventario "
   vSql = vSql & " SET   RecalcularCierre =  " & gUtilSQL.fBooleanToSqlValue(True) & " , "
   vSql = vSql & "   RecalcularCosto =   " & gUtilSQL.fBooleanToSqlValue(True) & " , "
   vSql = vSql & "   FechaCierreActualizada = " & gUtilSQL.fDateToSQLValue(valFechaCierre)
   vSql = vSql & "   FROM   articuloInventario  "
   vSql = vSql & "   INNER JOIN  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & " ON "
   vSql = vSql & "   ArticuloInventario.ConsecutivoCompania = " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania AND "
   vSql = vSql & "   ArticuloInventario.Codigo = " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo "
   vSql = vSql & vWhere
   Set insArticuloInvVista = Nothing
   fSQLUpdateArticuloInventarioFechaCierreOperacion = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUpdateCostoPromedioPonderado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLUpdateArticuloInventarioRecalulaCostoOperacion(ByVal valConsecutivoCompania As Long, ByVal valFechaOperacion As Date, ByVal valDocumento As String, ByVal valOperacion As String) As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vSql As String
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
   vSql = " UPDATE articuloInventario "
   vSql = vSql & " SET   RecalcularCierre =  " & gUtilSQL.fBooleanToSqlValue(True) & " , "
   vSql = vSql & "   RecalcularCosto =   " & gUtilSQL.fBooleanToSqlValue(True) & "  "
   vSql = vSql & "   FROM   articuloInventario  "
   vSql = vSql & "   INNER JOIN  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & " ON "
   vSql = vSql & "   ArticuloInventario.ConsecutivoCompania = " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania AND "
   vSql = vSql & "   ArticuloInventario.Codigo = " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo "
   vSql = vSql & vWhere
   Set insArticuloInvVista = Nothing
   fSQLUpdateArticuloInventarioRecalulaCostoOperacion = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUpdateCostoPromedioPonderado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLUpdateCostoPromedioPonderado(ByVal valConsecutivoCompania As Long, ByVal valCodigo As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = " A.CostoALaFecha = " & gUtilSQL.fBooleanToSqlValue(True)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "B.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "B.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "B.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSql = " UPDATE articuloInventario "
   vSql = vSql & " SET   RecalcularCosto =  " & gUtilSQL.fBooleanToSqlValue(False) & " , "
   vSql = vSql & "   CostoPromedio = A.CostoFinal  "
   vSql = vSql & "   FROM   articuloInventario AS B "
   vSql = vSql & "   INNER JOIN  dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaInicial) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigo) & "," & gUtilSQL.fSimpleSqlValue(valCodigo) & ") AS A ON "
   vSql = vSql & "   B.codigo = A.Codigo AND "
   vSql = vSql & "   B.consecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   vSql = vSql & vWhere
 fSQLUpdateCostoPromedioPonderado = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUpdateCostoPromedioPonderado", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLCodigoArticuloDeOperaciones(ByVal valConsecutivoCompania As Long, ByVal valFechaOperacion As Date, ByVal valDocumento As String, ByVal valOperacion As String) As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vSql As String
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
   vSql = " Select articuloInventario.codigo "
   vSql = vSql & "   FROM   articuloInventario  "
   vSql = vSql & "   INNER JOIN  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & " ON "
   vSql = vSql & "   ArticuloInventario.ConsecutivoCompania = " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania AND "
   vSql = vSql & "   ArticuloInventario.Codigo = " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo "
   vSql = vSql & vWhere
    Set insArticuloInvVista = Nothing
 fSQLCodigoArticuloDeOperaciones = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUpdateCostoPromedioPonderadoOperaciones", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLUltimaFechaCierresCostoPromedio(ByVal valConsecutivoCompania As Long) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSql = " SELECT Max(Fecha) As Fecha  FROM CierreCostoDelPeriodo "
   vSql = vSql & vWhere
   vSql = vSql & "  GROUP BY   ConsecutivoCompania "
   fSQLUltimaFechaCierresCostoPromedio = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLUltimaFechaCierresCostoPromedio", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLExistenArticulosConCierresPorRecalculo(ByVal valConsecutivoCompania As Long) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   vWhere = " RecalcularCierre = " & gUtilSQL.fBooleanToSqlValue(True)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSql = " SELECT CODIGO FROM ArticuloInventario "
   vSql = vSql & vWhere
   fSQLExistenArticulosConCierresPorRecalculo = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLExistenArticulosConCierresPorRecalculo", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLFechaCierresCostoPromedioReporte(ByVal valConsecutivoCompania As Long, ByVal valFecha As Date, ByVal valReporte As Boolean) As String
   Dim vSql As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   If (valReporte) Then
    vWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", "CierreCostoDelPeriodo.Fecha", valFecha, "<=")
   Else
    vWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", "CierreCostoDelPeriodo.Fecha", valFecha, "<")
   End If
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSql = " SELECT Max(Fecha ) As Fecha  FROM CierreCostoDelPeriodo "
   vSql = vSql & vWhere
   vSql = vSql & "  GROUP BY   ConsecutivoCompania "
   fSQLFechaCierresCostoPromedioReporte = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLFechaCierresCostoPromedio", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPonderadoReporteDetallado(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim Sql As String
Dim sqlWhere As String
Dim SQLCodigo   As String
On Error GoTo h_ERROR
If (gUtilDate.fF1IsLessThanF2(valFechaInicial, valFechaCierre)) Then
valFechaInicial = valFechaCierre + 1
End If
  Sql = Sql & fSqlCostoPromedioPonderadoReporteSelect1(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaCierre, valFechaInicial, valFechaFinal)
  Sql = Sql & " UNION "
  Sql = Sql & fSqlCostoPromedioPonderadoReporteSelect2(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaCierre, valFechaInicial, valFechaFinal)
h_EXIT: On Error GoTo 0
  fSqlCostoPromedioPonderadoReporteDetallado = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPonderadoReporteSelect1(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim Sql As String
Dim sqlWhere As String
Dim SQLCodigo   As String
On Error GoTo h_ERROR

   sqlWhere = gUtilSQL.fSQLValueBetween("fn_CostoPromedioPonderado_1.codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   sqlWhere = sqlWhere & " AND  fn_CostoPromedioPonderado_1.CostoAlaFecha = " & gUtilSQL.fBooleanToSqlValue(True)
   
   sqlWhere = gUtilSQL.fSQLValueDateWithComparisonRule(sqlWhere, "fn_CostoPromedioPonderado_1.Fecha", valFechaInicial, "<")
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   Sql = Sql & "  SELECT "
   Sql = Sql & " fn_CostoPromedioPonderado_1.id_num, "
   Sql = Sql & " fn_CostoPromedioPonderado_1.codigo AS codigo, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Saldo") & " AS descripcion ,"
   Sql = Sql & gUtilSQL.fDateToSQLValue(valFechaInicial) & "  AS fecha,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.tipodedocumento AS tipodedocumento, "
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS ExistenciaInicial,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS CostoInicial,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS  MontoInicial,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS entrada ,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " As MontoEntrada,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS salidad,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS MontoSalidad,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.existenciaFinal AS existenciaFinal ,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.CostoFinal AS CostoFinal,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.TotalFinal AS MontoFinal "
   Sql = Sql & " FROM  ArticuloInventario INNER JOIN "
   Sql = Sql & "   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS fn_CostoPromedioPonderado_1 ON "
   Sql = Sql & "  ArticuloInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo AND "
   Sql = Sql & " ArticuloInventario.ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   Sql = Sql & sqlWhere
h_EXIT: On Error GoTo 0
  fSqlCostoPromedioPonderadoReporteSelect1 = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPonderadoReporteSelect2(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim Sql As String
Dim sqlWhere As String
Dim SQLCodigo   As String
On Error GoTo h_ERROR
   sqlWhere = gUtilSQL.fSQLValueBetween("fn_CostoPromedioPonderado_1.codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(sqlWhere, "fn_CostoPromedioPonderado_1.Fecha", valFechaInicial, valFechaFinal)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   Sql = Sql & "  SELECT "
   Sql = Sql & " fn_CostoPromedioPonderado_1.id_num, "
   Sql = Sql & " fn_CostoPromedioPonderado_1.codigo AS codigo,"
   Sql = Sql & " ArticuloInventario.descripcion AS descripcion ,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.fecha AS fecha,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.tipodedocumento AS tipodedocumento,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.ExistenciaInicial AS ExistenciaInicial,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.CostoInicial AS CostoInicial,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.TotalInicial AS  MontoInicial,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.Entradas AS entrada ,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.TotalOperacion As MontoEntrada,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.Salidas AS salidad,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.CostoInicial *   fn_CostoPromedioPonderado_1.salidas as MontoSalidad,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.existenciaFinal AS existenciaFinal,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.CostoFinal AS CostoFinal,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.TotalFinal AS MontoFinal"
   Sql = Sql & " FROM  ArticuloInventario INNER JOIN "
   Sql = Sql & "   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS fn_CostoPromedioPonderado_1 ON "
   Sql = Sql & "  ArticuloInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo AND "
   Sql = Sql & " ArticuloInventario.ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
  Sql = Sql & sqlWhere
h_EXIT: On Error GoTo 0
 fSqlCostoPromedioPonderadoReporteSelect2 = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLExistenciayCostoAUnaFecha(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valCodigo As String) As String
   Dim vSql As String
   Dim sqlUltimaCompra As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSql = ""
   sqlUltimaCompra = "dbo.Gf_CompraUnaFecha( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania, " & gUtilSQL.fDateToSQLValue(valFechaInicial) & "," & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo)"
   vWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Fecha", valFechaInicial, "<=")
   vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo", valCodigo, False)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
   vSql = vSql & " SELECT "
   vSql = vSql & " SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Entrada ) -   SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Salidad )  AS  Existencia, "
   vSql = vSql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(sqlUltimaCompra), gUtilSQL.fNumToStrSQL(0), sqlUltimaCompra, True) & " AS Costo "
   vSql = vSql & " FROM ArticuloInventario INNER JOIN "
   vSql = vSql & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & " ON "
   vSql = vSql & " ArticuloInventario.ConsecutivoCompania  =  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania AND "
   vSql = vSql & " ArticuloInventario.Codigo =  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo"
   vSql = vSql & vWhere
   vSql = vSql & " Group By "
   vSql = vSql & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania, "
   vSql = vSql & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo "
   fSQLExistenciayCostoAUnaFecha = vSql
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
   Dim vSql As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSql = ""
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
   vSql = " SELECT  ArticuloInventario.Codigo AS CodigoArticulo,"
   vSql = vSql & " CierreCostoDelPeriodo.Existencia AS Cantidad,"
   vSql = vSql & " CierreCostoDelPeriodo.Costo  AS CostoUnitario,"
   vSql = vSql & " CierreCostoDelPeriodo.Existencia  AS CantidadRecibida,"
   vSql = vSql & " CierreCostoDelPeriodo.ConsecutivoCompania AS ConsecutivoCompania,"
   vSql = vSql & " CierreCostoDelPeriodo.ConsecutivoCompania AS NumeroSecuencialCompra,"
   vSql = vSql & " CierreCostoDelPeriodo.ConsecutivoCompania  As ConsecutivoRenglon"
   vSql = vSql & " FROM ArticuloInventario INNER JOIN "
   vSql = vSql & " CierreCostoDelPeriodo ON  "
   vSql = vSql & " articuloInventario.ConsecutivoCompania = CierreCostoDelPeriodo.ConsecutivoCompania AND "
   vSql = vSql & " articuloInventario.Codigo = CierreCostoDelPeriodo.Codigo "
   vSql = vSql & vWhere
  fSQLDataSourceCargaInicialModificar = vSql
Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDataSourceCargaInicial", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLDataSourceCargaInicialInsertar(ByVal valConsecutivoCompania As Long, ByVal valLineaDeProducto As String, ByVal valMarca As String, ByVal valCategoria As String, ByVal valCodigoIncio As String, ByVal valCodigoFin As String, ByVal valFechaInicial As Date) As String
   Dim vSql As String
   Dim vCondicion As String
   Dim sqlUltimaCompra As String
   Dim vsqlFechaUltimaCompra As String
   
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSql = ""
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
   vSql = vSql & " SELECT "
   vSql = vSql & " ArticuloInventario.Codigo AS CodigoArticulo, "
   vSql = vSql & " SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Entrada ) -   SUM( " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Salidad )  AS Cantidad, "
   vSql = vSql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(sqlUltimaCompra), gUtilSQL.fNumToStrSQL(0), sqlUltimaCompra, True) & " AS CostoUnitario,"
   vSql = vSql & gUtilSQL.fNumToStrSQL(0) & "  AS CantidadRecibida,"
   vSql = vSql & "ArticuloInventario.ConsecutivoCompania AS ConsecutivoCompania, "
   vSql = vSql & gUtilSQL.fNumToStrSQL(0) & " AS NumeroSecuencialCompra,"
   vSql = vSql & gUtilSQL.fNumToStrSQL(0) & "  As ConsecutivoRenglon, "
   vSql = vSql & gUtilSQL.fIfResultIsNullReplace(vsqlFechaUltimaCompra, gUtilSQL.fDateToSQLValue(valFechaInicial)) & "  As Fecha "
   vSql = vSql & " FROM ArticuloInventario "
   vSql = vSql & " INNER JOIN "
   vSql = vSql & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & " ON "
   vSql = vSql & " ArticuloInventario.ConsecutivoCompania  =  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".ConsecutivoCompania AND "
   vSql = vSql & " ArticuloInventario.Codigo =  " & insArticuloInvVista.GetViewNameArticuloInvMovimiento() & ".Codigo"
   vSql = vSql & " LEFT JOIN "
   vSql = vSql & " CierreCostoDelPeriodo ON  "
   vSql = vSql & " articuloInventario.ConsecutivoCompania = CierreCostoDelPeriodo.ConsecutivoCompania AND "
   vSql = vSql & " articuloInventario.Codigo = CierreCostoDelPeriodo.Codigo "
   vSql = vSql & vWhere
   vSql = vSql & " Group By "
   vSql = vSql & " ArticuloInventario.ConsecutivoCompania, "
   vSql = vSql & " ArticuloInventario.Codigo, "
   vSql = vSql & "CierreCostoDelPeriodo.existencia "
   fSQLDataSourceCargaInicialInsertar = vSql
  Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDataSourceCargaInicialInsertar", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLCodigoDeArticulos(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String) As String
   Dim vSql As String
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
   vSql = " SELECT articuloInventario.Codigo "
   vSql = vSql & "   FROM   articuloInventario  "
   vSql = vSql & vWhere
  fSQLCodigoDeArticulos = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLCodigoDeArticulos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPonderadoReporte(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valSoloVigente As Boolean) As String
Dim Sql As String
Dim vFechaIni As Date
Dim vFechaFin As Date
On Error GoTo h_ERROR
vFechaIni = valFechaInicial - 1
Sql = Sql & " SELECT "
Sql = Sql & " CostoPromedioMovimientos.ConsecutivoCompania,"
Sql = Sql & " CostoPromedioMovimientos.Codigo,"
Sql = Sql & " CostoPromedioMovimientos.Descripcion ,"
Sql = Sql & " CostoPromedioMovimientos.Entrada,"
Sql = Sql & " CostoPromedioMovimientos.Salida,"
Sql = Sql & " CostoPromedioMovimientos.Retiro,"
Sql = Sql & " CostoPromedioMovimientos.Autoconsumo,"
Sql = Sql & " isnull(fn_CostoPromedioPonderado_1.existenciaFinal,0) AS ExistenciaAnterior ,"
Sql = Sql & " isnull(fn_CostoPromedioPonderado_1.existenciaFinal,0) + CostoPromedioMovimientos.Entrada - CostoPromedioMovimientos.Salida - CostoPromedioMovimientos.Retiro -  CostoPromedioMovimientos.Autoconsumo AS ExistenciaActual,"
Sql = Sql & " CostoPromedioMovimientos.costofinal *  CostoPromedioMovimientos.Entrada AS EntradaMoneda,"
Sql = Sql & " CostoPromedioMovimientos.costofinal *  CostoPromedioMovimientos.Salida  AS SalidaMoneda,"
Sql = Sql & " CostoPromedioMovimientos.costofinal *  CostoPromedioMovimientos.Retiro  AS RetiroMoneda,"
Sql = Sql & " CostoPromedioMovimientos.costofinal *  CostoPromedioMovimientos.Autoconsumo AS AutoconsumoMoneda,"
Sql = Sql & " (isnull(fn_CostoPromedioPonderado_1.existenciaFinal,0) +  CostoPromedioMovimientos.Entrada - CostoPromedioMovimientos.Salida - CostoPromedioMovimientos.Retiro -  CostoPromedioMovimientos.Autoconsumo) * CostoPromedioMovimientos.CostoFinal AS ExistenciaMoneda,"
Sql = Sql & " CostoPromedioMovimientos.costofinal ,"
Sql = Sql & " CostoPromedioMovimientos.costofinal * isnull(fn_CostoPromedioPonderado_1.existenciaFinal,0)  As COSTO_UNITARIO"
Sql = Sql & " FROM  dbo.Gf_CostoPromedioMovimientosInv( " & gUtilSQL.fDateToSQLValue(valFechaInicial) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS CostoPromedioMovimientos "
Sql = Sql & " LEFT    JOIN  dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(vFechaIni) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS fn_CostoPromedioPonderado_1 ON "
Sql = Sql & " CostoPromedioMovimientos.Codigo = fn_CostoPromedioPonderado_1.Codigo    and fn_CostoPromedioPonderado_1.CostoALaFecha = 'S'"

If (valSoloVigente) Then
    Sql = Sql & "  INNER JOIN ArticuloInventario ON "
    Sql = Sql & "  CostoPromedioMovimientos.Codigo = ArticuloInventario.Codigo  AND  "
    Sql = Sql & gUtilSQL.fSQLEnumValueWithAnd("", "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE) & "  AND "
    Sql = Sql & " ArticuloInventario.ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
End If

h_EXIT: On Error GoTo 0
fSqlCostoPromedioPonderadoReporte = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoReporte", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlBuscarArticulosInventario(ByVal valConsecutivoCompania As Long) As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim vSql As String
   Dim vSqlWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSql = ""
   vSqlWhere = ""
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   vSql = " SELECT "
   vSql = vSql & " Codigo ,"
   vSql = vSql & " CodigoDelArticulo,"
   vSql = vSql & "TipoArticuloInv, TipoDeArticulo,"
   vSql = vSql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("CodigoColor"), gUtilSQL.fSimpleSqlValue(" "), "CodigoColor") & " As CodigoColor,"
   vSql = vSql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("CodigoTalla"), gUtilSQL.fSimpleSqlValue(" "), "CodigoTalla") & " As CodigoTalla,"
   vSql = vSql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("CodigoGrupo"), gUtilSQL.fSimpleSqlValue(" "), "CodigoGrupo") & " As CodigoGrupo,"
   vSql = vSql & "Serial,"
   vSql = vSql & "Rollo "
   vSql = vSql & " From " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
   vSql = vSql & vSqlWhere
h_EXIT: On Error GoTo 0
  Set insArticuloInvVista = Nothing
  fSqlBuscarArticulosInventario = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlBuscarArticulosConCotizacion", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLDelReporteConteoFisico(ByVal valConsecutivoCompania As Long, ByVal valNumeroDeConteoFisico As Long) As String
   Dim Sql As String
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

   Sql = "SELECT conteoFisico.ConsecutivoConteo, articuloInventario.TipoArticuloInv, " & SqlStatus & ", "
   Sql = Sql & " ConteoFisico.CodigoAlmacen, almacen.NombreAlmacen, conteoFisico.SoloUnaLinea, articuloInventario.LineaDeProducto, renglonConteoFisico.CodigoArticulo,"
   Sql = Sql & " ArticuloInventario.Descripcion, renglonConteoFisico.ExistenciaEnElComputador, renglonConteoFisico.ExistenciaFisica, renglonConteoFisico.Diferencia, RenglonConteoFisico.Serial, RenglonConteoFisico.Rollo"
   Sql = Sql & " FROM (almacen INNER JOIN conteoFisico ON (almacen.Codigo = conteoFisico.CodigoAlmacen) AND (almacen.ConsecutivoCompania = conteoFisico.ConsecutivoCompania)) "
   Sql = Sql & " INNER JOIN (articuloInventario INNER JOIN renglonConteoFisico ON (articuloInventario.ConsecutivoCompania = renglonConteoFisico.ConsecutivoCompania) "
   Sql = Sql & " AND (articuloInventario.Codigo = renglonConteoFisico.CodigoArticulo)) ON (conteoFisico.ConsecutivoConteo = renglonConteoFisico.ConsecutivoConteo) "
   Sql = Sql & " AND (conteoFisico.ConsecutivoCompania = renglonConteoFisico.ConsecutivoCompania)"
   Sql = Sql & sqlWhere
   Sql = Sql & " UNION "
   Sql = Sql & " SELECT conteoFisico.ConsecutivoConteo, articuloInventario.TipoArticuloInv, " & SqlStatus & ", "
   Sql = Sql & " ConteoFisico.CodigoAlmacen, almacen.NombreAlmacen, conteoFisico.SoloUnaLinea, articuloInventario.LineaDeProducto, renglonConteoFisico.CodigoArticulo, "
   Sql = Sql & " [ArticuloInventario].[descripcion]+' '+[Color].[DescripcionColor]+' '+[Talla].[DescripcionTalla] AS Descripcion, renglonConteoFisico.ExistenciaEnElComputador,"
   Sql = Sql & " RenglonConteoFisico.ExistenciaFisica, renglonConteoFisico.Diferencia, RenglonConteoFisico.Serial, RenglonConteoFisico.Rollo "
   Sql = Sql & " FROM ((almacen INNER JOIN conteoFisico ON (almacen.ConsecutivoCompania = conteoFisico.ConsecutivoCompania) AND (almacen.Codigo = conteoFisico.CodigoAlmacen)) "
   Sql = Sql & " INNER JOIN renglonConteoFisico ON (conteoFisico.ConsecutivoCompania = renglonConteoFisico.ConsecutivoCompania) AND (conteoFisico.ConsecutivoConteo = renglonConteoFisico.ConsecutivoConteo)) "
   Sql = Sql & " INNER JOIN (((ExistenciaPorGrupo INNER JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo) AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)) "
   Sql = Sql & " INNER JOIN Talla ON (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania)) INNER JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) "
   Sql = Sql & " AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)) ON (renglonConteoFisico.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania) AND (renglonConteoFisico.Serial = ExistenciaPorGrupo.Serial)"
   Sql = Sql & " AND (renglonConteoFisico.Rollo = ExistenciaPorGrupo.Rollo) AND (renglonConteoFisico.CodigoArticulo =(ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla))"
   Sql = Sql & sqlWhere
   Sql = Sql & " ORDER BY conteoFisico.ConsecutivoConteo, articuloInventario.LineaDeProducto, renglonConteoFisico.CodigoArticulo, RenglonConteoFisico.Serial, RenglonConteoFisico.Rollo"
   fSQLDelReporteConteoFisico = Sql
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
   Dim vSqlWhere As String
   Dim Sql As String
   On Error GoTo h_ERROR
  vSqlWhere = ""
 
  If valLineaDeProducto <> "" Then
       vSqlWhere = gUtilSQL.fSQLValueWithAnd(vSqlWhere, "ArticuloInventario.LineaDeProducto", valLineaDeProducto, False)
  End If
 
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "ArticuloInventario.StatusDelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSqlWhere = gUtilSQL.fSQLValueWithAnd(vSqlWhere, "existenciaPorAlmacen.CodigoAlmacen", valCodigoDelAlmacen, False)
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   
   Sql = "SELECT existenciaPorAlmacen.CodigoArticulo AS CodigoArt, existenciaPorAlmacen.Cantidad, '0' AS Serial,'0' AS Rollo,ArticuloInventario.TipoArticuloInv "
   Sql = Sql & " FROM articuloInventario INNER JOIN existenciaPorAlmacen ON (articuloInventario.Codigo = existenciaPorAlmacen.CodigoArticulo) AND (articuloInventario.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania)"
   Sql = Sql & vSqlWhere & " And ArticuloInventario.TipoArticuloInv = '" & eTAI_Simple & "'"
   Sql = Sql & " UNION ALL"
   Sql = Sql & " SELECT existenciaPorAlmacen.CodigoArticulo AS CodigoArt, RenglonExistenciaAlmacen.Cantidad, RenglonExistenciaAlmacen.CodigoSerial AS SERIAL, RenglonExistenciaAlmacen.CodigoRollo AS ROLLO,ArticuloInventario.TipoArticuloInv "
   Sql = Sql & " FROM existenciaPorAlmacen INNER JOIN (articuloInventario INNER JOIN (RenglonExistenciaAlmacen INNER JOIN ExistenciaPorGrupo ON (RenglonExistenciaAlmacen.CodigoRollo = ExistenciaPorGrupo.Rollo)"
   Sql = Sql & " AND (RenglonExistenciaAlmacen.CodigoSerial = ExistenciaPorGrupo.Serial) AND (RenglonExistenciaAlmacen.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla)) AND (RenglonExistenciaAlmacen.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) ON (articuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo) AND (articuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) ON (existenciaPorAlmacen.CodigoArticulo = RenglonExistenciaAlmacen.CodigoArticulo) AND (existenciaPorAlmacen.CodigoAlmacen = RenglonExistenciaAlmacen.CodigoAlmacen) AND (existenciaPorAlmacen.ConsecutivoCompania = RenglonExistenciaAlmacen.ConsecutivoCompania)"
   Sql = Sql & vSqlWhere
   Sql = Sql & " UNION ALL "
   Sql = Sql & " SELECT existenciaPorAlmacen.CodigoArticulo AS CodigoArt,ExistenciaPorAlmacen.Cantidad,'0' AS Serial,'0' AS Rollo, articuloInventario.TipoArticuloInv"
   Sql = Sql & " FROM existenciaPorAlmacen INNER JOIN (articuloInventario INNER JOIN ExistenciaPorGrupo ON (articuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania) AND (articuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)) ON (existenciaPorAlmacen.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla)) AND (existenciaPorAlmacen.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)"
   Sql = Sql & vSqlWhere & " And ArticuloInventario.TipoArticuloInv = '" & eTAI_UsaTalla_Color & "'"
   fSQLSearchArticulosParaElGridDeConteoFisico = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearchArticulosParaElGridDeConteoFisico = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchArticulosParaElGridDeConteoFisico", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlArticulosAjustarCosto(ByVal valConsecutivoCompania As Long, ByVal valConjunto) As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   Dim vSql As String
   vWhere = ""
   vWhere = "CODIGO IN (" & valConjunto & ")"
   vSql = gUtilSQL.fSQLNumberValueWithAnd(vWhere, "ConsecutivoCompania", valConsecutivoCompania)
  fSqlArticulosAjustarCosto = "SELECT * FROM articuloInventario " & gUtilSQL.getWhereSQL(vSql)
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
       Sql = " UPDATE articuloInventario  SET "
      If valPrecio Then
        vAumento = fFormulaAumento(SQLNameColum, valMargen, valUsarNuevaFormula)
         Sql = Sql & " margenganancia = " & valMargen & ", "
         Sql = Sql & " PrecioSinIva  = " & vAumento & ","
         Sql = Sql & " PrecioConIva  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
      If valPrecio2 Or valPrecio3 Or valPrecio4 Then
            Sql = Sql & ", "
         End If
      End If
      If valPrecio2 Then
         vAumento = fFormulaAumento(SQLNameColum, valMargen2, valUsarNuevaFormula)
         Sql = Sql & " margenganancia2 = " & valMargen2 & ", "
         Sql = Sql & " PrecioSinIva2  = " & vAumento & ","
         Sql = Sql & " PrecioConIva2  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
         If valPrecio3 Or valPrecio4 Then
            Sql = Sql & ", "
         End If
      End If
      If valPrecio3 Then
         vAumento = fFormulaAumento(SQLNameColum, valMargen3, valUsarNuevaFormula)
         Sql = Sql & " margenganancia3 = " & valMargen3 & ", "
         Sql = Sql & " PrecioSinIva3  = " & vAumento & ","
         Sql = Sql & " PrecioConIva3  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
         If valPrecio4 Then
            Sql = Sql & ", "
         End If
      End If
      If valPrecio4 Then
         vAumento = fFormulaAumento(SQLNameColum, valMargen4, valUsarNuevaFormula)
         Sql = Sql & " margenganancia4 = " & valMargen4 & ", "
         Sql = Sql & " PrecioSinIva4  = " & vAumento & ","
         Sql = Sql & " PrecioConIva4  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
       End If
      Sql = Sql & " WHERE ConsecutivoCompania = " & valConsecutivoCia
      Sql = Sql & " AND articuloInventario.codigo between '" & valDesde & "' and '" & valHasta & "'"
      Sql = Sql & " AND articuloInventario.tipodearticulo = '0' or articuloInventario.tipodearticulo = '2'"
      If gTexto.DfLen(valLineaProducto) > 0 Then
         Sql = Sql & " AND LineaDeProducto = '" & valLineaProducto & "'"
      End If
      If gTexto.DfLen(valCategoria) > 0 Then
         Sql = Sql & " AND Categoria = '" & valCategoria & "'"
      End If
      If gTexto.DfLen(valMarca) > 0 Then
         Sql = Sql & " AND marca = '" & valMarca & "'"
      End If
    sActualizarPreciosConMargenesNuevos = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActualizarPreciosConMargenes", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function sActualizarPreciosSinMargenesNuevos(ByVal valUsarNuevaFormula As Boolean, ByVal valUsaCostoPromedio As Boolean, ByVal valDesde As String, ByVal valHasta As String, ByVal vSQLAlicuotasVigentes As String, ByVal valConsecutivoCia As Long, ByVal valMarca As String, ByVal valPrecio As Boolean, ByVal valPrecio2 As Boolean, ByVal valPrecio3 As Boolean, ByVal valPrecio4 As Boolean, Optional ByVal valLineaProducto As String, Optional ByVal valCategoria As String) As String
   Dim SQLNameColum As String
   Dim Sql As String
   Dim vAumento As String
   On Error GoTo h_ERROR
   
    If (valUsaCostoPromedio) Then
   SQLNameColum = "CostoPromedio"
   Else
   SQLNameColum = "CostoUnitario"
   End If
   
   Sql = ""
   Sql = " UPDATE  articuloInventario  SET "
   If valPrecio Then
      
      vAumento = fFormulaAumentoSinMargenesNuevos(SQLNameColum, "MargenGanancia", valUsarNuevaFormula)
      Sql = Sql & " PrecioSinIva  = " & vAumento & ","
      Sql = Sql & " PrecioConIva  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
    
      If valPrecio2 Or valPrecio3 Or valPrecio4 Then
         Sql = Sql & ", "
      End If
   End If
   If valPrecio2 Then
      vAumento = fFormulaAumentoSinMargenesNuevos(SQLNameColum, "MargenGanancia2", valUsarNuevaFormula)
      Sql = Sql & " PrecioSinIva2  = " & vAumento & ","
  
      Sql = Sql & " PrecioConIva2  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
      If valPrecio3 Or valPrecio4 Then
         Sql = Sql & ", "
      End If
   End If
   If valPrecio3 Then
      vAumento = fFormulaAumentoSinMargenesNuevos(SQLNameColum, "MargenGanancia3", valUsarNuevaFormula)
      Sql = Sql & " PrecioSinIva3  = " & vAumento & ","
      Sql = Sql & " PrecioConIva3  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
      If valPrecio4 Then
         Sql = Sql & ", "
      End If
   End If
   If valPrecio4 Then
      vAumento = fFormulaAumentoSinMargenesNuevos(SQLNameColum, "MargenGanancia4", valUsarNuevaFormula)
      Sql = Sql & " PrecioSinIva4  = " & vAumento & ","
      Sql = Sql & " PrecioConIva4  = " & vAumento & " + ( " & vAumento & "  * " & vSQLAlicuotasVigentes & ") / 100"
   End If
      Sql = Sql & " WHERE ConsecutivoCompania = " & valConsecutivoCia
      Sql = Sql & " AND articuloInventario.codigo between '" & valDesde & "' and '" & valHasta & "'"
      Sql = Sql & " AND articuloInventario.tipodearticulo = 0 or articuloInventario.tipodearticulo = 2"
   If gTexto.DfLen(valLineaProducto) > 0 Then
      Sql = Sql & " AND LineaDeProducto = '" & valLineaProducto & "'"
   End If
   If gTexto.DfLen(valCategoria) > 0 Then
      Sql = Sql & " AND Categoria = '" & valCategoria & "'"
   End If
   If gTexto.DfLen(valMarca) > 0 Then
      Sql = Sql & " AND marca = '" & valMarca & "'"
   End If
   sActualizarPreciosSinMargenesNuevos = Sql
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
     
    vSqlWhere = gUtilSQL.fSQLValueDatesBetween("", "Fecha", valFechaInicial, valFechaFinal)
    vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "consecutivocompania", valCodigoCompaniaActual)
    vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
 
    Sql = " ;WITH ArticulosyCostos(ConsecutivoCompania,Fecha,TipoDeDocumento, TipoDeOperacion,NumeroDeDocumento,CodigoArticulo,Descripcion,UnidadDeVenta,Entradas,Salidas,CostoUnitarioCompra,CostoUnitarioOperacion)"
   
    Sql = Sql & "AS("
    Sql = Sql & "SELECT"
    Sql = Sql & " Vista.ConsecutivoCompania,"
    Sql = Sql & " Vista.Fecha,"
    Sql = Sql & " Vista.TipoDeDocumento,"
    Sql = Sql & " Vista.TipoDeOperacion,"
    Sql = Sql & " Vista.NumeroDeDocumento,"
    Sql = Sql & " Vista.CodigoArticulo,"
    Sql = Sql & " Vista.Descripcion,"
    Sql = Sql & " Vista.UnidadDeVenta,"
    Sql = Sql & " Vista.Entradas,"
    Sql = Sql & " Vista.Salidas,"
    Sql = Sql & " Round(Vista.CostoUnitario,2) AS CostoUnitarioCompra ,"
    Sql = Sql & " Round(dbo.Gf_CompraUnaFecha(ConsecutivoCompania, Vista.Fecha, Vista.CodigoArticulo), 2) As CostoUnitarioOperacion"
    Sql = Sql & " FROM  " & insInvPermanenteVista.GetViewNameRegistroInventarioPermanenteValorizado & " AS Vista "
    Sql = Sql & vSqlWhere
    Sql = Sql & ")"
    Sql = Sql & "SELECT"
    Sql = Sql & " Fecha,"
    Sql = Sql & " TipoDeDocumento,"
    Sql = Sql & " TipoDeOperacion,"
    Sql = Sql & " NumeroDeDocumento,"
    Sql = Sql & " CodigoArticulo,"
    Sql = Sql & " Descripcion,"
    Sql = Sql & " UnidadDeVenta,"
    Sql = Sql & "isnull( cast ((CASE WHEN TipoDeDocumento = 'Compra' THEN CostoUnitarioCompra ELSE  CostoUnitarioOperacion  END )as decimal(10,2)) ,0 ) AS CostoUnitario ,"
    Sql = Sql & " Entradas,"
    Sql = Sql & " isnull(cast ((CASE WHEN  TipoDeDocumento = 'Compra' AND  Entradas > 0 THEN CostoUnitarioCompra WHEN  TipoDeDocumento <> 'Compra' AND  Entradas > 0 THEN  CostoUnitarioOperacion END )as decimal(10,2)),0) AS CostoUnitarioEntrada ,"
    Sql = Sql & " isnull(cast ((CASE WHEN  TipoDeDocumento = 'Compra' AND  Entradas > 0 THEN CostoUnitarioCompra * Entradas  WHEN  TipoDeDocumento <> 'Compra' AND  Entradas > 0 THEN  CostoUnitarioOperacion END )as decimal(10,2)),0) AS CostoTotalEntrante,"
    Sql = Sql & " Salidas ,"
    Sql = Sql & " isnull(CASE WHEN  Salidas > 0  THEN CostoUnitarioOperacion ELSE 0 END,0 )  AS CostoUnitarioSalida,"
    Sql = Sql & " isnull(cast ((CostoUnitarioOperacion *  Salidas) as decimal(10,2)),0)  AS CostoTotalSaliente, "
    Sql = Sql & " isnull(cast (cast ((CASE WHEN  TipoDeDocumento = 'Compra' AND  Entradas > 0 THEN CostoUnitarioCompra * Entradas  WHEN  TipoDeDocumento <> 'Compra' AND  Entradas > 0 THEN  CostoUnitarioOperacion END )as decimal(10,2)) - (CostoUnitarioOperacion *  Salidas) as decimal(10,2)),0)  AS CostoTotal "
   
   
    Sql = Sql & " From ArticulosyCostos"
    Sql = Sql & " ORDER BY  CodigoArticulo,Fecha"
   Set insInvPermanenteVista = Nothing
h_EXIT: On Error GoTo 0
   fSQLDetalleDelInventarioPermanenteValorizadoUltimaCompra = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDetalleDelInventarioPermanenteValorizadoUltimaCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPorAlmacenReporte(ByVal valConsecutivoCompania As Long, ByVal valCodigoAlmacen As String, ByVal valFechaCierre As Date, ByVal valFecha As Date, ByVal valCodigoMin As String, ByVal valCodigoMax As String) As String

   Dim vSql As String
   Dim vSqlWhere As String
   Dim insArticuloInvVista As clsArticuloInvVista
    On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
    vSqlWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", "ArticuloInvMovimientosAlmacen.Fecha", valFecha, "<=")
   
    If (valCodigoAlmacen <> "") Then
      vSqlWhere = gUtilSQL.fSQLValueWithAnd(vSqlWhere, "ArticuloInvMovimientosAlmacen.CodigoAlmacen", valCodigoAlmacen, False)
    End If
    vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "articuloInventario.ConsecutivoCompania", valConsecutivoCompania)
    vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
 
  
   vSql = vSql & " SELECT"
   vSql = vSql & " ArticuloInvMovimientosAlmacen.CodigoAlmacen,"
   vSql = vSql & " NombreAlmacen,"
   vSql = vSql & " articuloInventario.Codigo,"
   vSql = vSql & " articuloInventario.Descripcion,"
   vSql = vSql & " SUM(ArticuloInvMovimientosAlmacen.Entrada) - SUM (ArticuloInvMovimientosAlmacen.Salida) AS Existencia,"
   vSql = vSql & " fn_CostoPromedioPonderado_1.costofinal AS CostoUnitario,"
   vSql = vSql & " (SUM(ArticuloInvMovimientosAlmacen.Entrada) - SUM (ArticuloInvMovimientosAlmacen.Salida)) * fn_CostoPromedioPonderado_1.costofinal AS CostoTotal"
 
   vSql = vSql & " FROM  articuloInventario"
   vSql = vSql & " INNER JOIN  " & insArticuloInvVista.GetViewNameArticulosMovimientoTransferencia() & " AS ArticuloInvMovimientosAlmacen ON"
   vSql = vSql & " articuloInventario.codigo = ArticuloInvMovimientosAlmacen.codigo AND "
   vSql = vSql & " articuloInventario.ConsecutivoCompania = ArticuloInvMovimientosAlmacen.ConsecutivoCompania"
   vSql = vSql & "  INNER   JOIN  dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFecha) & "," & gUtilSQL.fSimpleSqlValue(valCodigoMin) & "," & gUtilSQL.fSimpleSqlValue(valCodigoMax) & ") AS fn_CostoPromedioPonderado_1 ON "
   vSql = vSql & " articuloInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo   AND"
   vSql = vSql & " fn_CostoPromedioPonderado_1.CostoALaFecha = 'S'"
   vSql = vSql & " INNER   JOIN   almacen ON "
   
   vSql = vSql & " almacen.Codigo = ArticuloInvMovimientosAlmacen.CodigoAlmacen AND"
   vSql = vSql & " almacen.ConsecutivoCompania = ArticuloInvMovimientosAlmacen.ConsecutivoCompania"
   
   
   vSql = vSql & vSqlWhere
   vSql = vSql & " Group By"
   vSql = vSql & "  ArticuloInvMovimientosAlmacen.CodigoAlmacen,"
   vSql = vSql & "  NombreAlmacen,"
   vSql = vSql & " articuloInventario.Codigo,"
   vSql = vSql & " articuloInventario.Descripcion,"
   vSql = vSql & "  fn_CostoPromedioPonderado_1.costofinal"
   
   vSql = vSql & " ORDER BY "
   vSql = vSql & " ArticuloInvMovimientosAlmacen.CodigoAlmacen, articuloInventario.Codigo"
   
   fSqlCostoPromedioPorAlmacenReporte = vSql
  Set insArticuloInvVista = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         " fSqlCostoPromedioPorAlmacenReporte", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function
Public Function fSqlCostoPromedioGeneralReporte(ByVal valConsecutivoCompania As Long, ByVal valFechaCierre As Date, ByVal valFecha As Date, ByVal valCodigoMin As String, ByVal valCodigoMax As String) As String

   Dim vSql As String
   Dim vSqlWhere As String
   Dim insArticuloInvVista As clsArticuloInvVista
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   vSqlWhere = gUtilSQL.fSQLValueDateWithComparisonRule("", "ArticuloInvMovimientosAlmacen.Fecha", valFecha, "<=")
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "articuloInventario.ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
  
   vSql = vSql & " SELECT"
   vSql = vSql & " articuloInventario.Codigo,"
   vSql = vSql & " articuloInventario.Descripcion,"
   vSql = vSql & " SUM(ArticuloInvMovimientosAlmacen.Entrada) - SUM (ArticuloInvMovimientosAlmacen.Salidad) AS Existencia,"
   vSql = vSql & " fn_CostoPromedioPonderado_1.costofinal AS CostoUnitario,"
   vSql = vSql & " (SUM(ArticuloInvMovimientosAlmacen.Entrada) - SUM (ArticuloInvMovimientosAlmacen.Salidad)) * fn_CostoPromedioPonderado_1.costofinal AS CostoTotal"
 
   vSql = vSql & " FROM  articuloInventario"
   vSql = vSql & " INNER JOIN  " & insArticuloInvVista.GetViewNameArticuloInvMovimientoAlmacen() & " AS ArticuloInvMovimientosAlmacen ON"
   vSql = vSql & " articuloInventario.codigo = ArticuloInvMovimientosAlmacen.codigo AND "
   vSql = vSql & " articuloInventario.ConsecutivoCompania = ArticuloInvMovimientosAlmacen.ConsecutivoCompania"
   vSql = vSql & "  INNER   JOIN  dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFecha) & "," & gUtilSQL.fSimpleSqlValue(valCodigoMin) & "," & gUtilSQL.fSimpleSqlValue(valCodigoMax) & ") AS fn_CostoPromedioPonderado_1 ON "
   vSql = vSql & " articuloInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo   AND"
   vSql = vSql & " fn_CostoPromedioPonderado_1.CostoALaFecha = 'S'"
   
   vSql = vSql & vSqlWhere
   vSql = vSql & " Group By"
   vSql = vSql & " articuloInventario.Codigo,"
   vSql = vSql & " articuloInventario.Descripcion,"
   vSql = vSql & " fn_CostoPromedioPonderado_1.costofinal"
    vSql = vSql & " ORDER BY "
   vSql = vSql & " articuloInventario.Codigo"
    fSqlCostoPromedioGeneralReporte = vSql
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
Dim Sql As String
Dim vPrecio As String
Dim vSqlWhere As String
On Error GoTo h_ERROR
Set insArticuloInvVista = New clsArticuloInvVista
   vPrecio = fSqlNivelDePrecio(valNivelDePrecio, valPrecioSinIva)
   vSqlWhere = gUtilSQL.fSQLValueBetween("VistaArticulosCompuestos.Compuesto", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesdeCompuestos), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHastaCompuestos))
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "VistaArticulosCompuestos.ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)

   Sql = Sql & " SELECT "
   Sql = Sql & "VistaArticulosCompuestos.Compuesto,"
   Sql = Sql & "VistaArticulosCompuestos.DescripcionCompuesto,"
   Sql = Sql & gUtilSQL.getIIF("VistaArticulosCompuestos.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO), gUtilSQL.fSimpleSqlValue(" * "), gUtilSQL.fSimpleSqlValue("")) & "+VistaArticulosCompuestos.Codigo AS Codigo,"
   Sql = Sql & "VistaArticulosCompuestos.Descripcion,"
   Sql = Sql & "VistaArticulosCompuestos.Cantidad AS Aporte,"
   Sql = Sql & gUtilSQL.fIfResultIsNullReplace("Gf_CostoPromedioPonderado_1.CostoFinal", "VistaArticulosCompuestos.CostoUnitario") & " AS  CostoIndividual,"
   Sql = Sql & gUtilSQL.fIfResultIsNullReplace("VistaArticulosCompuestos.Cantidad", gUtilSQL.fNumToStrSQL(0)) & " * " & gUtilSQL.fIfResultIsNullReplace("Gf_CostoPromedioPonderado_1.CostoFinal", "VistaArticulosCompuestos.CostoUnitario") & " AS  AporteDelIten,"
   Sql = Sql & "VistaArticulosCompuestos." & vPrecio & " AS PVP "
   Sql = Sql & " FROM  " & insArticuloInvVista.GetViewNameArticulosProductosCompuestos() & " AS VistaArticulosCompuestos LEFT  JOIN  "
   Sql = Sql & " dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaReporte) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS Gf_CostoPromedioPonderado_1 ON "
   Sql = Sql & " VistaArticulosCompuestos.Codigo = Gf_CostoPromedioPonderado_1.Codigo AND "
   Sql = Sql & " Gf_CostoPromedioPonderado_1.CostoAlaFecha = " & gUtilSQL.fBooleanToSqlValue(True)
   Sql = Sql & vSqlWhere
  Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
  fSqlCostoPromedioPonderadoArticulosCompuestos = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoArticulosCompuestos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSqlCostoUltimaCompraArticulosCompuestos(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesdeCompuestos As String, ByVal valCodigoHastaCompuestos As String, ByVal valFechaReporte As Date, ByVal valNivelDePrecio As enum_NivelDePrecio, ByVal valPrecioSinIva As Boolean, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String) As String
Dim insArticuloInvVista As clsArticuloInvVista
Dim Sql As String
Dim vPrecio As String
Dim vSqlWhere As String
On Error GoTo h_ERROR
Set insArticuloInvVista = New clsArticuloInvVista
   vPrecio = fSqlNivelDePrecio(valNivelDePrecio, valPrecioSinIva)
   vSqlWhere = gUtilSQL.fSQLValueBetween("VistaArticulosCompuestos.Compuesto", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesdeCompuestos), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHastaCompuestos))
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "VistaArticulosCompuestos.ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)

   Sql = Sql & " SELECT "
   Sql = Sql & "VistaArticulosCompuestos.Compuesto,"
   Sql = Sql & "VistaArticulosCompuestos.DescripcionCompuesto,"
   Sql = Sql & gUtilSQL.getIIF("VistaArticulosCompuestos.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_SERVICIO), gUtilSQL.fSimpleSqlValue(" * "), gUtilSQL.fSimpleSqlValue("")) & "+VistaArticulosCompuestos.Codigo AS Codigo,"
   Sql = Sql & "VistaArticulosCompuestos.Descripcion,"
   Sql = Sql & "VistaArticulosCompuestos.Cantidad AS Aporte,"
   Sql = Sql & gUtilSQL.fIfResultIsNullReplace("Gf_CostosUltimaCompra.CostoFinal", "VistaArticulosCompuestos.CostoUnitario") & " AS  CostoIndividual,"
   Sql = Sql & gUtilSQL.fIfResultIsNullReplace("VistaArticulosCompuestos.Cantidad", gUtilSQL.fNumToStrSQL(0)) & " * " & gUtilSQL.fIfResultIsNullReplace("Gf_CostosUltimaCompra.CostoFinal", "VistaArticulosCompuestos.CostoUnitario") & " AS  AporteDelIten,"
   Sql = Sql & "VistaArticulosCompuestos." & vPrecio & " AS PVP "
   Sql = Sql & " FROM  " & insArticuloInvVista.GetViewNameArticulosProductosCompuestos() & " AS VistaArticulosCompuestos LEFT  JOIN  "
   Sql = Sql & "  dbo.Gf_CostosUltimaCompra(" & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaReporte) & "," & gUtilSQL.fDateToSQLValue(valFechaReporte) & "," & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde)) & "," & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta)) & ") AS Gf_CostosUltimaCompra ON "
   Sql = Sql & " VistaArticulosCompuestos.Codigo = Gf_CostosUltimaCompra.Codigo "
   
   
   Sql = Sql & vSqlWhere
  Set insArticuloInvVista = Nothing
h_EXIT: On Error GoTo 0
  fSqlCostoUltimaCompraArticulosCompuestos = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoUltimaCompraArticulosCompuestos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLCamposNoRequeridos(ByVal valCodigoArt As String, ByVal gProyCompaniaActual As Object) As String
   Dim strCampos As String
   Dim Sql As String
   On Error GoTo h_ERROR
   strCampos = "ConsecutivoCompania, Codigo, CantArtReservado, NombreOperador, FechaUltimaModificacion, fldTimeStamp, CodigoGrupo, TipoArticuloInv, UsaBalanza"
   Sql = "SELECT " & strCampos & " FROM articuloInventario WHERE Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoArt) & " AND ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   fSQLCamposNoRequeridos = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLCamposNoRequeridos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLAlicuotasVigentes(ByVal gAdmAlicuotaIvaActual As Object) As String
   Dim valAlicuotaGeneral As Currency
   Dim valAlicuota2 As Currency
   Dim valAlicuota3 As Currency
   Dim Sql As String
   On Error GoTo h_ERROR
   valAlicuotaGeneral = gAdmAlicuotaIvaActual.GetAlicuotaIVA(gUtilDate.getFechaDeHoy, eTD_ALICUOTAGENERAL)
   valAlicuota2 = gAdmAlicuotaIvaActual.GetAlicuotaIVA(gUtilDate.getFechaDeHoy, eTD_ALICUOTA2)
   valAlicuota3 = gAdmAlicuotaIvaActual.GetAlicuotaIVA(gUtilDate.getFechaDeHoy, eTD_ALICUOTA3)
   Sql = " (case when alicuotaiva = 1 then " & valAlicuotaGeneral & " when alicuotaiva = 2 then " & valAlicuota2
   Sql = Sql & " when alicuotaiva = 3 then " & valAlicuota3 & " else " & valAlicuotaGeneral & " end)"
   fSQLAlicuotasVigentes = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLCaseAlicuotas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSqlCostoUltimaCompraValoracionDeInventario(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim Sql As String
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
 
 Sql = "; WITH  ArticulosyCostos(ConsecutivoCompania,PrioridadOperacion, NumeroDocumento, "
 Sql = Sql & " Codigo, "
 Sql = Sql & " TipoDeDocumento,"
 Sql = Sql & " Fecha,"
 Sql = Sql & " Descripcion,"
 Sql = Sql & " entrada,"
 Sql = Sql & " salidad,"
 Sql = Sql & " MontoOperacion,"
 Sql = Sql & " existencia,"
 Sql = Sql & " costoAnterior,"
 Sql = Sql & " costoActual) AS "
 Sql = Sql & " ( SELECT "
 Sql = Sql & " ConsecutivoCompania,"
 Sql = Sql & " PrioridadOperacion,"
 Sql = Sql & " NumeroDocumento,"
 Sql = Sql & " Codigo,"
 Sql = Sql & " TipoDeDocumento,"
 Sql = Sql & " Fecha,"
 Sql = Sql & " Descripcion,"
 Sql = Sql & " entrada,"
 Sql = Sql & " salidad,"
 Sql = Sql & " MontoOperacion,"
 Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.Gf_Existencia(ConsecutivoCompania,Fecha,Codigo, PrioridadOperacion, NumeroDocumento)"), gUtilSQL.fNumToStrSQL(0), "dbo.Gf_Existencia(ConsecutivoCompania,Fecha,Codigo, PrioridadOperacion, NumeroDocumento)", False) & " As existencia, "
 Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha-1,Codigo )"), gUtilSQL.fNumToStrSQL(0), "dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha-1,Codigo )", False) & " As costoAnterior,"
 Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha,Codigo )"), gUtilSQL.fNumToStrSQL(0), "dbo.Gf_CompraUnaFecha(ConsecutivoCompania,Fecha,Codigo )", False) & " As costoActual "
 
Sql = Sql & "  FROM   IGV_ArticuloInvMovimiento  "
Sql = Sql & sqlWhere
Sql = Sql & " ) "
Sql = Sql & "  select"
Sql = Sql & "  ArticulosyCostos.codigo,"
Sql = Sql & "  ArticulosyCostos.descripcion,"
Sql = Sql & "  dbo.articuloInventario.UnidadDeVenta,"
Sql = Sql & "  fecha,"
Sql = Sql & "  tipodedocumento,"
Sql = Sql & "  NumeroDocumento, "
Sql = Sql & "  ArticulosyCostos.existencia AS ExistenciaInicial,"
Sql = Sql & "  costoAnterior as CostoInicial,"
Sql = Sql & "  ArticulosyCostos.existencia * costoAnterior  as MontoInicial,"
Sql = Sql & "  entrada ,"
Sql = Sql & gUtilSQL.getIIF("MontoOperacion = 0", "costoAnterior *  entrada", "MontoOperacion") & " As MontoEntrada, "
Sql = Sql & gUtilSQL.getIIF("MontoOperacion = 0", "0", "MontoOperacion / entrada") & " As ConstoUnitarioEntrada, "
Sql = Sql & "  salidad,"
Sql = Sql & "  costoAnterior *  salidad AS MontoSalidad,"
Sql = Sql & "  costoAnterior   AS CostoUnitarioSalida,"
Sql = Sql & "  ArticulosyCostos.existencia + entrada - salidad as existenciaFinal,"
Sql = Sql & gUtilSQL.getIIF("MontoOperacion = 0", "costoActual", "MontoOperacion / entrada") & " As CostoFinal, "
Sql = Sql & gUtilSQL.getIIF("MontoOperacion = 0", "costoActual", "MontoOperacion / entrada") & " * (ArticulosyCostos.Existencia + entrada - salidad ) As MontoFinal "
Sql = Sql & "   From ArticulosyCostos "
Sql = Sql & "   INNER JOIN  dbo.articuloInventario   ON  "
Sql = Sql & "   ArticulosyCostos.ConsecutivoCompania = articuloInventario.ConsecutivoCompania AND   "
Sql = Sql & "   ArticulosyCostos.Codigo = articuloInventario.Codigo"
Sql = Sql & sqlWhere2
Sql = Sql & "  order by codigo, fecha, PrioridadOperacion, numerodocumento"
Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
  fSqlCostoUltimaCompraValoracionDeInventario = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoUltimaCompraValoracionDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlCostoPromedioPonderadoReporteDetalladoValoracionDeInventario(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim Sql As String
Dim sqlWhere As String
Dim SQLCodigo   As String
On Error GoTo h_ERROR
If (gUtilDate.fF1IsLessThanF2(valFechaInicial, valFechaCierre)) Then
valFechaInicial = valFechaCierre + 1
End If
  Sql = Sql & fSqlCostoPromedioPonderadoReporteSelect1ValoracionDeInventario(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaCierre, valFechaInicial, valFechaFinal)
  Sql = Sql & " UNION "
  Sql = Sql & fSqlCostoPromedioPonderadoReporteSelect2ValoracionDeInventario(valConsecutivoCompania, valCodigoDesde, valCodigoHasta, valFechaCierre, valFechaInicial, valFechaFinal)
h_EXIT: On Error GoTo 0
 fSqlCostoPromedioPonderadoReporteDetalladoValoracionDeInventario = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoReporteDetalladoValoracionDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPonderadoReporteSelect1ValoracionDeInventario(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim Sql As String
Dim sqlWhere As String
Dim SQLCodigo   As String
On Error GoTo h_ERROR

   sqlWhere = gUtilSQL.fSQLValueBetween("fn_CostoPromedioPonderado_1.codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   sqlWhere = sqlWhere & " AND  fn_CostoPromedioPonderado_1.CostoAlaFecha = " & gUtilSQL.fBooleanToSqlValue(True)
   sqlWhere = gUtilSQL.fSQLValueDateWithComparisonRule(sqlWhere, "fn_CostoPromedioPonderado_1.Fecha", valFechaInicial, "<")
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   Sql = Sql & "  SELECT "
   Sql = Sql & " fn_CostoPromedioPonderado_1.id_num, "
   Sql = Sql & " fn_CostoPromedioPonderado_1.codigo AS codigo, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Saldo") & " AS descripcion ,"
   Sql = Sql & " ArticuloInventario.UnidadDeVenta ,  "
   Sql = Sql & gUtilSQL.fDateToSQLValue(valFechaInicial) & "  AS fecha,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.tipodedocumento AS tipodedocumento, "
   Sql = Sql & " fn_CostoPromedioPonderado_1.NumeroDocumento, "
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS ExistenciaInicial,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS CostoInicial,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS  MontoInicial,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS entrada ,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " As MontoEntrada,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " As ConstoUnitarioEntrada, "
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS salidad,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS MontoSalidad,"
   Sql = Sql & gUtilSQL.fNumToStrSQL(0) & " AS CostoUnitarioSalida,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.existenciaFinal AS existenciaFinal ,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.CostoFinal AS CostoFinal,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.TotalFinal AS MontoFinal "
   Sql = Sql & " FROM  ArticuloInventario INNER JOIN "
   Sql = Sql & "   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS fn_CostoPromedioPonderado_1 ON "
   Sql = Sql & "  ArticuloInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo AND "
   Sql = Sql & " ArticuloInventario.ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   Sql = Sql & sqlWhere
h_EXIT: On Error GoTo 0
  fSqlCostoPromedioPonderadoReporteSelect1ValoracionDeInventario = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoReporteSelect1ValoracionDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlCostoPromedioPonderadoReporteSelect2ValoracionDeInventario(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
Dim Sql As String
Dim sqlWhere As String
Dim SQLCodigo   As String
On Error GoTo h_ERROR
   sqlWhere = gUtilSQL.fSQLValueBetween("fn_CostoPromedioPonderado_1.codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   sqlWhere = gUtilSQL.fSQLValueDatesBetween(sqlWhere, "fn_CostoPromedioPonderado_1.Fecha", valFechaInicial, valFechaFinal)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   Sql = Sql & "  SELECT "
   Sql = Sql & " fn_CostoPromedioPonderado_1.id_num, "
   Sql = Sql & " fn_CostoPromedioPonderado_1.codigo AS codigo,"
   Sql = Sql & " ArticuloInventario.descripcion AS descripcion ,"
   Sql = Sql & " ArticuloInventario.UnidadDeVenta ,  "
   Sql = Sql & " fn_CostoPromedioPonderado_1.fecha AS fecha,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.tipodedocumento AS tipodedocumento,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.NumeroDocumento, "
   Sql = Sql & " fn_CostoPromedioPonderado_1.ExistenciaInicial AS ExistenciaInicial,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.CostoInicial AS CostoInicial,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.TotalInicial AS  MontoInicial,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.Entradas AS entrada ,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.TotalOperacion As MontoEntrada,"
   Sql = Sql & gUtilSQL.getIIF("fn_CostoPromedioPonderado_1.TotalOperacion = 0", "0", "fn_CostoPromedioPonderado_1.TotalOperacion / fn_CostoPromedioPonderado_1.Entradas ") & " As ConstoUnitarioEntrada, "
   Sql = Sql & " fn_CostoPromedioPonderado_1.Salidas AS salidad,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.CostoInicial *   fn_CostoPromedioPonderado_1.salidas AS MontoSalidad,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.CostoInicial AS CostoUnitarioSalida, "
   Sql = Sql & " fn_CostoPromedioPonderado_1.existenciaFinal AS existenciaFinal,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.CostoFinal AS CostoFinal,"
   Sql = Sql & " fn_CostoPromedioPonderado_1.TotalFinal AS MontoFinal"
   Sql = Sql & " FROM  ArticuloInventario INNER JOIN "
   Sql = Sql & "   dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS fn_CostoPromedioPonderado_1 ON "
   Sql = Sql & "  ArticuloInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo AND "
   Sql = Sql & " ArticuloInventario.ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
  Sql = Sql & sqlWhere
h_EXIT: On Error GoTo 0
fSqlCostoPromedioPonderadoReporteSelect2ValoracionDeInventario = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoReporteSelect2ValoracionDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlInventarioPorCategoria(ByVal valConsecutivoCompania As Long, ByVal valOrdenarPor As String, ByVal valUsaFiltro As Boolean, ByVal valFiltrarPor As String, ByVal valParaFiltrar As String) As String
Dim Sql As String
On Error GoTo h_ERROR
   Sql = Sql & " SELECT "
   Sql = Sql & " Categoria,Codigo,Descripcion,Existencia,"
   Sql = Sql & " CantArtReservado As Reserva,Existencia - CantArtReservado as Disponible,LineaDeProducto As " & gUtilSQL.fSimpleSqlValue("Linea De Producto")
   Sql = Sql & " FROM  ArticuloInventario  "
   Sql = Sql & " WHERE  "
   Sql = Sql & gUtilSQL.DfMidSQL("Descripcion", 1, 7) & " <> " & gUtilSQL.fSimpleSqlValue("Resumen") & " AND " & gUtilSQL.DfMidSQL("Descripcion", 1, 8) & " <> " & gUtilSQL.fSimpleSqlValue("comisión") & " AND "
   Sql = Sql & "ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania)
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
   Sql = Sql & " AND " & valFiltrarPor & " = " & gUtilSQL.fSimpleSqlValue(valParaFiltrar)
   End If
   Sql = Sql & " Order By "
   If (valUsaFiltro) Then
   Sql = Sql & " " & valFiltrarPor
   If Not (valOrdenarPor = valFiltrarPor) Then
   Sql = Sql & " , " & valOrdenarPor
   End If
   Else
   Sql = Sql & valOrdenarPor
   End If
   fSqlInventarioPorCategoria = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlInventarioPorCategoria", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryCompra(ByVal valConsecutivoCompania As Long, ByVal valConsecutivoAlmacen As Long, ByVal valCodigoArticulo As String, ByVal valTipoArticuloInv As enum_TipoArticuloInv, ByVal valSerial As String, ByVal valRollo As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   If valTipoArticuloInv = eTAI_Simple Or valTipoArticuloInv = eTAI_UsaTalla_Color Then
      Sql = "SELECT ISNULL(SUM(CompraDetalleArticuloInventario.Cantidad) , 0) AS SumCantidad"
      Sql = Sql & " FROM Adm.Compra INNER JOIN Adm.CompraDetalleArticuloInventario"
      Sql = Sql & " ON Compra.Consecutivo = CompraDetalleArticuloInventario.ConsecutivoCompra"
      Sql = Sql & " AND Compra.ConsecutivoCompania = CompraDetalleArticuloInventario.ConsecutivoCompania"
      Sql = Sql & " WHERE Compra.ConsecutivoCompania = " & valConsecutivoCompania
      Sql = Sql & " AND Compra.ConsecutivoAlmacen = " & gUtilSQL.fSimpleSqlValue(valConsecutivoAlmacen)
      Sql = Sql & " AND Compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE)
      Sql = Sql & " AND CompraDetalleArticuloInventario.CodigoArticulo =" & gUtilSQL.fSimpleSqlValue(valCodigoArticulo)
   Else
      Sql = "SELECT ISNULL(SUM(CompraDetalleSerialRollo.Cantidad) , 0) AS SumCantidad"
      Sql = Sql & " FROM Adm.compra INNER JOIN Adm.CompraDetalleSerialRollo "
      Sql = Sql & " ON compra.Consecutivo = CompraDetalleSerialRollo.ConsecutivoCompra "
      Sql = Sql & " AND compra.ConsecutivoCompania = CompraDetalleSerialRollo.ConsecutivoCompania"
      Sql = Sql & " WHERE CompraDetalleSerialRollo.CodigoArticulo =" & gUtilSQL.fSimpleSqlValue(valCodigoArticulo)
      If (valSerial <> "" And valSerial <> "0") Then
         Sql = Sql & " AND CompraDetalleSerialRollo.Serial = " & gUtilSQL.fSimpleSqlValue(valSerial)
      End If
      If (valRollo <> "" And valRollo <> "0") Then
         Sql = Sql & " AND CompraDetalleSerialRollo.Rollo = " & gUtilSQL.fSimpleSqlValue(valRollo)
      End If
      Sql = Sql & " AND compra.ConsecutivoCompania = " & valConsecutivoCompania
      Sql = Sql & " AND compra.ConsecutivoAlmacen = " & gUtilSQL.fSimpleSqlValue(valConsecutivoAlmacen)
      Sql = Sql & " AND compra.StatusCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE)
   End If
   fQueryCompra = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryCompra", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLSearchArticuloInventarioFacturaSelectSoloServicios(ByVal valConsecutivoCompania As Long, ByVal valCodigoArticulo As String, ByVal valPrecio1 As String, ByVal valPrecio2 As String, ByVal valsqlWhere As String, ByVal valMonedaExtranjera As Boolean, ByVal vGroupby As String, ByVal valBuscarPorSerial As Boolean, ByVal valSerial As String, ByVal valDescripcion As String, ByVal valArtResumen As String, ByVal valSqlArtIGTF As String) As String
Dim insArticuloInvVista As clsArticuloInvVista
Dim vSql As String
Dim vSqlWhere As String
Dim vSQLCondicionTipoArticulo As String
On Error GoTo h_ERROR
Set insArticuloInvVista = New clsArticuloInvVista
 vSQLCondicionTipoArticulo = "ArticuloInventario.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_SERVICIO
 vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLCondicionTipoArticulo, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompania)
 If gTexto.DfLen(valCodigoArticulo) > 0 And valCodigoArticulo <> "*" Then
    vSqlWhere = gUtilSQL.fSQLValueWithAnd(vSqlWhere, "ArticuloInventario.Codigo", valCodigoArticulo, False)
 End If
 vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
 If valBuscarPorSerial Then
    If valSerial <> "" Then
      fSQLSearchArticuloInventarioFacturaSelectSoloServicios = ""
      Exit Function
   End If
End If
 If valDescripcion <> "" Then
   vSqlWhere = vSqlWhere & "And ArticuloInventario.Descripcion LIKE " & gUtilSQL.fSimpleSqlValue(valDescripcion)
 End If
vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   If gTexto.DfLen(valArtResumen) > 0 Then
vSqlWhere = vSqlWhere & "And ArticuloInventario.Codigo NOT IN (" & valArtResumen & ")  "
   End If
   If gTexto.DfLen(valSqlArtIGTF) > 0 Then
      vSqlWhere = vSqlWhere & "And ArticuloInventario.Codigo NOT IN (" & valSqlArtIGTF & ")  "
   End If
vSql = " SELECT "
vSql = vSql & " ArticuloInventario.codigo AS CodigoCompuesto, "
vSql = vSql & " ArticuloInventario.descripcion,"
vSql = vSql & " ArticuloInventario.LineaDeProducto, "
vSql = vSql & " '0' AS Disponibilidad,"
vSql = vSql & valPrecio1 & " ,"
vSql = vSql & valPrecio2 & " ,"
vSql = vSql & " '0' AS Serial,"
vSql = vSql & " '0' AS Rollo,"
vSql = vSql & " '0' AS CantidadUltimaCompra,"
vSql = vSql & " ArticuloInventario.AlicuotaIva,"
vSql = vSql & " '0' AS CodigoGrupo,"
vSql = vSql & " ArticuloInventario.TipoArticuloInv,"
vSql = vSql & " ISNULL(ArticuloInventario.CodigoGrupo,''),"
vSql = vSql & " ArticuloInventario.ConsecutivoCompania,"
vSql = vSql & " ArticuloInventario.PorcentajeBaseImponible,"
vSql = vSql & " '0' AS CodigoAlmacen,"
vSql = vSql & " '0' AS Existencia"
vSql = vSql & " FROM  ArticuloInventario"
vSql = vSql & " LEFT JOIN   CamposMonedaExtranjera ON "
vSql = vSql & "ArticuloInventario.ConsecutivoCompania = CamposMonedaExtranjera.ConsecutivoCompania AND "
vSql = vSql & "ArticuloInventario.Codigo = CamposMonedaExtranjera.Codigo  "
vSql = vSql & vSqlWhere
   Set insArticuloInvVista = Nothing
h_EXIT:    On Error GoTo 0
 fSQLSearchArticuloInventarioFacturaSelectSoloServicios = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchArticuloInventarioFacturaSelectSoloServicios", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDisponibilidadArticuloInventario(ByVal valConsecutivoCompania As String, ByVal valSqlStatus As String, ByVal valRango As Boolean, ByVal valLineaProducto As Boolean, ByVal valCategoria As Boolean, ByVal valMarca As Boolean, valSqlOrder As String, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valStrLineaProducto As String, ByVal valStrCategoria As String, ByVal valStrMarca As String, ByVal valMuestraExistenciaCero As Boolean) As String
   Dim Sql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   Sql = "SELECT "
   Sql = Sql & " articuloInventario.Descripcion as Descripcion, "
   Sql = Sql & " IGV_InventarioConOSinReservas_B1.Existencia as Existencia,"
   Sql = Sql & " (IGV_InventarioConOSinReservas_B1.CantidadReservada) AS CantidadReservada,"
   Sql = Sql & "(IGV_InventarioConOSinReservas_B1.Existencia - IGV_InventarioConOSinReservas_B1.CantidadReservada) AS CantidadDisponible, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, _
                        gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " as StatusdelArticulo "
   Sql = Sql & " , articuloinventario.LineaDeProducto as LineaDeProducto, "
   Sql = Sql & " articuloInventario.categoria, articuloInventario.marca, "
   Sql = Sql & " articuloInventario.TipoArticuloInv, "
   Sql = Sql & " articuloInventario.Codigo as CodigoOriginal, "
   Sql = Sql & " IGV_InventarioConOSinReservas_B1.Serial, "
   Sql = Sql & " IGV_InventarioConOSinReservas_B1.Rollo, "
   Sql = Sql & " IGV_InventarioConOSinReservas_B1.CodigoDelArticulo,"
   Sql = Sql & " IGV_ExistenciaPorGrupoConTallasyColoresMod.DescripcionColor,"
   Sql = Sql & " IGV_ExistenciaPorGrupoConTallasyColoresMod.DescripcionTalla, "
   Sql = Sql & " articuloInventario.Existencia AS ExistenciaMaster ,"
   Sql = Sql & " articuloInventario.CantArtReservado AS CantReservadaMaster,"
   Sql = Sql & " articuloInventario.Existencia - articuloInventario.CantArtReservado AS DisponibilidadMaster "
   
   Sql = Sql & " FROM articuloInventario"
   Sql = Sql & " INNER JOIN IGV_InventarioConOSinReservas_B1"
   Sql = Sql & " ON (articuloInventario.ConsecutivoCompania = IGV_InventarioConOSinReservas_B1.ConsecutivoCompania "
   Sql = Sql & " AND articuloInventario.Codigo = IGV_InventarioConOSinReservas_B1.Codigo)"
   
   Sql = Sql & " LEFT OUTER JOIN "
   Sql = Sql & " (SELECT ConsecutivoCompania,CodigoArticulo,CodigoColor,CodigoTalla,DescripcionColor,DescripcionTalla,Serial,Rollo FROM IGV_ExistenciaPorGrupoConTallasyColores where IGV_ExistenciaPorGrupoConTallasyColores.CodigoTalla <> '') IGV_ExistenciaPorGrupoConTallasyColoresMod"
   Sql = Sql & " ON (IGV_ExistenciaPorGrupoConTallasyColoresMod.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania "
   Sql = Sql & " AND IGV_InventarioConOSinReservas_B1.CodigoDelArticulo = IGV_ExistenciaPorGrupoConTallasyColoresMod.CodigoArticulo " & gUtilSQL.CharConcat & " IGV_ExistenciaPorGrupoConTallasyColoresMod.codigocolor " & gUtilSQL.CharConcat & " IGV_ExistenciaPorGrupoConTallasyColoresMod.CodigoTalla)"
   
   Sql = Sql & " AND " & gUtilSQL.getIIF("articuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial), "IGV_InventarioConOSinReservas_B1.Serial", gUtilSQL.fSimpleSqlValue(""), True)
   Sql = Sql & " = " & gUtilSQL.getIIF("articuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial), "IGV_ExistenciaPorGrupoConTallasyColoresMod.Serial", gUtilSQL.fSimpleSqlValue(""), True)
   Sql = Sql & " AND " & gUtilSQL.getIIF("articuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial), "IGV_InventarioConOSinReservas_B1.Rollo", gUtilSQL.fSimpleSqlValue(""), True)
   Sql = Sql & " = " & gUtilSQL.getIIF("articuloInventario.TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial), "IGV_ExistenciaPorGrupoConTallasyColoresMod.Rollo", gUtilSQL.fSimpleSqlValue(""), True)
   
   Sql = Sql & " WHERE ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND ArticuloInventario.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_MERCANCIA
   
   If valSqlStatus <> "" Then
      Sql = Sql & " AND ArticuloInventario.StatusdelArticulo = " & valSqlStatus
   End If
   If valRango Then
      Sql = Sql & " AND " & gUtilSQL.fSQLStringValueBetween("ArticuloInventario.Codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   End If
   If valLineaProducto Then
      Sql = Sql & " AND ArticuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrLineaProducto))
   End If
   If valCategoria Then
      Sql = Sql & " AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrCategoria))
   End If
   If valMarca Then
      Sql = Sql & " AND ArticuloInventario.Marca = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrMarca))
   End If
   If Not valMuestraExistenciaCero Then
      Sql = Sql & " AND ArticuloInventario.Existencia <> 0 "
   End If
   Sql = Sql & " ORDER BY " & valSqlOrder
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLDisponibilidadArticuloInventario = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDisponibilidadArticuloInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDisponibilidadArticuloInventarioPorAlmacen(ByVal valConsecutivoCompania As String, ByVal valSqlStatus As String, ByVal valRango As Boolean, ByVal valLineaProducto As Boolean, ByVal valCategoria As Boolean, ByVal valMarca As Boolean, valSqlOrder As String, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valStrLineaProducto As String, ByVal valStrCategoria As String, ByVal valStrMarca As String, ByVal valMuestraExistenciaCero As Boolean, ByVal valAlmacen As String) As String
   Dim Sql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
  
   Sql = "SELECT "
   Sql = Sql & " ExistenciaPorAlmacen.CodigoAlmacen,"
   Sql = Sql & " Almacen.NombreAlmacen as DescripcionAlmacen, "
   Sql = Sql & " articuloInventario.Descripcion as Descripcion, "
   Sql = Sql & " ExistenciaPorAlmacen.Cantidad as Existencia, "
   Sql = Sql & " (IGV_InventarioConOSinReservas_B1.CantidadReservada) AS CantidadReservada,"
   Sql = Sql & "(IGV_InventarioConOSinReservas_B1.Existencia - IGV_InventarioConOSinReservas_B1.CantidadReservada) AS CantidadDisponible, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, _
                        gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " as StatusdelArticulo "
   Sql = Sql & " , articuloinventario.LineaDeProducto as LineaDeProducto, "
   Sql = Sql & " articuloInventario.categoria, articuloInventario.marca, "
   Sql = Sql & " articuloInventario.TipoArticuloInv, "
   Sql = Sql & " articuloInventario.Codigo as CodigoOriginal, "
   Sql = Sql & " IGV_InventarioConOSinReservas_B1.Serial, "
   Sql = Sql & " IGV_InventarioConOSinReservas_B1.Rollo, "
   Sql = Sql & " IGV_InventarioConOSinReservas_B1.CodigoDelArticulo,"
   Sql = Sql & " IGV_ExistenciaPorGrupoConTallasyColoresMod.DescripcionColor,"
   Sql = Sql & " IGV_ExistenciaPorGrupoConTallasyColoresMod.DescripcionTalla, "
   Sql = Sql & " articuloInventario.Existencia AS ExistenciaMaster ,"
   Sql = Sql & " articuloInventario.CantArtReservado AS CantReservadaMaster,"
   Sql = Sql & " articuloInventario.Existencia - articuloInventario.CantArtReservado AS DisponibilidadMaster "
   
   Sql = Sql & " FROM articuloInventario"
   Sql = Sql & " INNER JOIN IGV_InventarioConOSinReservas_B1"
   Sql = Sql & " ON (articuloInventario.ConsecutivoCompania = IGV_InventarioConOSinReservas_B1.ConsecutivoCompania "
   Sql = Sql & " AND articuloInventario.Codigo = IGV_InventarioConOSinReservas_B1.Codigo)"
   
   Sql = Sql & " LEFT OUTER JOIN "
   Sql = Sql & " (SELECT ConsecutivoCompania,CodigoArticulo,CodigoColor,CodigoTalla,DescripcionColor,DescripcionTalla FROM IGV_ExistenciaPorGrupoConTallasyColores where IGV_ExistenciaPorGrupoConTallasyColores.CodigoTalla <> '') IGV_ExistenciaPorGrupoConTallasyColoresMod"
   Sql = Sql & " ON (IGV_ExistenciaPorGrupoConTallasyColoresMod.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania "
   Sql = Sql & " AND IGV_InventarioConOSinReservas_B1.CodigoDelArticulo = IGV_ExistenciaPorGrupoConTallasyColoresMod.CodigoArticulo " & gUtilSQL.CharConcat & " IGV_ExistenciaPorGrupoConTallasyColoresMod.codigocolor " & gUtilSQL.CharConcat & " IGV_ExistenciaPorGrupoConTallasyColoresMod.CodigoTalla)"
  
   Sql = Sql & " INNER JOIN ExistenciaPorAlmacen ON"
   Sql = Sql & " (IGV_InventarioConOSinReservas_B1.ConsecutivoCompania = ExistenciaPorAlmacen.ConsecutivoCompania  "
   Sql = Sql & " AND IGV_InventarioConOSinReservas_B1.CodigoDelArticulo = ExistenciaPorAlmacen.CodigoArticulo)"
   
   Sql = Sql & " INNER JOIN Almacen ON"
   Sql = Sql & " (ExistenciaPorAlmacen.ConsecutivoCompania = Almacen.ConsecutivoCompania"
   Sql = Sql & " AND ExistenciaPorAlmacen.CodigoAlmacen = Almacen.Codigo)"
   
   Sql = Sql & " WHERE ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND ArticuloInventario.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_MERCANCIA
   Sql = Sql & " AND TipoArticuloInv IN ( " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Simple) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_Color) & ")  "
   
   If valSqlStatus <> "" Then
      Sql = Sql & " AND ArticuloInventario.StatusdelArticulo = " & valSqlStatus
   End If
   If valRango Then
      Sql = Sql & " AND " & gUtilSQL.fSQLStringValueBetween("ArticuloInventario.Codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   End If
   If valLineaProducto Then
      Sql = Sql & " AND ArticuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrLineaProducto))
   End If
   If valCategoria Then
      Sql = Sql & " AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrCategoria))
   End If
   If valMarca Then
      Sql = Sql & " AND ArticuloInventario.Marca = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrMarca))
   End If
   If valAlmacen <> "" Then
      Sql = Sql & " AND Almacen.Codigo = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valAlmacen))
   End If
   If Not valMuestraExistenciaCero Then
      Sql = Sql & " AND ArticuloInventario.Existencia <> 0 "
   End If
   
   '---------------------------------------
   Sql = Sql & " UNION "
   '---------------------------------------
   Sql = Sql & " SELECT "
   Sql = Sql & " RenglonExistenciaAlmacen.CodigoAlmacen,"
   Sql = Sql & " Almacen.NombreAlmacen as DescripcionAlmacen, "
   Sql = Sql & " articuloInventario.Descripcion as Descripcion, "
   Sql = Sql & " RenglonExistenciaAlmacen.Cantidad as Existencia,"
   Sql = Sql & " (IGV_InventarioConOSinReservas_B1.CantidadReservada) AS CantidadReservada,"
   Sql = Sql & "(IGV_InventarioConOSinReservas_B1.Existencia - IGV_InventarioConOSinReservas_B1.CantidadReservada) AS CantidadDisponible, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE, _
                        gEnumProyecto.fEnumStatusArticuloToStringInArray(True), "") & " as StatusdelArticulo "
   Sql = Sql & " , articuloinventario.LineaDeProducto as LineaDeProducto, "
   Sql = Sql & " articuloInventario.categoria, articuloInventario.marca, "
   Sql = Sql & " articuloInventario.TipoArticuloInv, "
   Sql = Sql & " articuloInventario.Codigo as CodigoOriginal, "
   Sql = Sql & " IGV_InventarioConOSinReservas_B1.Serial, "
   Sql = Sql & " IGV_InventarioConOSinReservas_B1.Rollo, "
   Sql = Sql & " IGV_InventarioConOSinReservas_B1.CodigoDelArticulo,"
   Sql = Sql & " IGV_ExistenciaPorGrupoConTallasyColoresMod.DescripcionColor,"
   Sql = Sql & " IGV_ExistenciaPorGrupoConTallasyColoresMod.DescripcionTalla, "
   Sql = Sql & " articuloInventario.Existencia AS ExistenciaMaster ,"
   Sql = Sql & " articuloInventario.CantArtReservado AS CantReservadaMaster,"
   Sql = Sql & " articuloInventario.Existencia - articuloInventario.CantArtReservado AS DisponibilidadMaster "
   
   Sql = Sql & " FROM articuloInventario"
   Sql = Sql & " INNER JOIN IGV_InventarioConOSinReservas_B1"
   Sql = Sql & " ON (articuloInventario.ConsecutivoCompania = IGV_InventarioConOSinReservas_B1.ConsecutivoCompania "
   Sql = Sql & " AND articuloInventario.Codigo = IGV_InventarioConOSinReservas_B1.Codigo)"
   
   Sql = Sql & " LEFT OUTER JOIN "
   Sql = Sql & " (SELECT ConsecutivoCompania,CodigoArticulo,CodigoColor,CodigoTalla,DescripcionColor,DescripcionTalla,Serial,Rollo FROM IGV_ExistenciaPorGrupoConTallasyColores where IGV_ExistenciaPorGrupoConTallasyColores.CodigoTalla <> '') IGV_ExistenciaPorGrupoConTallasyColoresMod"
   Sql = Sql & " ON (IGV_ExistenciaPorGrupoConTallasyColoresMod.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania "
   Sql = Sql & " AND IGV_InventarioConOSinReservas_B1.CodigoDelArticulo = IGV_ExistenciaPorGrupoConTallasyColoresMod.CodigoArticulo " & gUtilSQL.CharConcat & " IGV_ExistenciaPorGrupoConTallasyColoresMod.codigocolor " & gUtilSQL.CharConcat & " IGV_ExistenciaPorGrupoConTallasyColoresMod.CodigoTalla"
   Sql = Sql & " AND IGV_InventarioConOSinReservas_B1.Serial = IGV_ExistenciaPorGrupoConTallasyColoresMod.Serial"
   Sql = Sql & " AND IGV_InventarioConOSinReservas_B1.Rollo = IGV_ExistenciaPorGrupoConTallasyColoresMod.Rollo)"
   Sql = Sql & " INNER JOIN RenglonExistenciaAlmacen ON"
   Sql = Sql & " (IGV_InventarioConOSinReservas_B1.ConsecutivoCompania = RenglonExistenciaAlmacen.ConsecutivoCompania  "
   Sql = Sql & " AND IGV_InventarioConOSinReservas_B1.CodigoDelArticulo = RenglonExistenciaAlmacen.CodigoArticulo"
   Sql = Sql & " AND IGV_InventarioConOSinReservas_B1.Serial = RenglonExistenciaAlmacen.CodigoSerial"
   Sql = Sql & " AND IGV_InventarioConOSinReservas_B1.Rollo = RenglonExistenciaAlmacen.CodigoRollo)"
      
   Sql = Sql & " INNER JOIN Almacen ON"
   Sql = Sql & " (RenglonExistenciaAlmacen.ConsecutivoCompania = Almacen.ConsecutivoCompania"
   Sql = Sql & " AND RenglonExistenciaAlmacen.CodigoAlmacen = Almacen.Codigo)"
      
   Sql = Sql & " WHERE ArticuloInventario.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND ArticuloInventario.TipoDeArticulo = " & enum_TipoDeArticulo.eTD_MERCANCIA
   Sql = Sql & " AND TipoArticuloInv IN ( " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaSerial) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaSerial_Rollo) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial) & ")  "
   
   If valSqlStatus <> "" Then
      Sql = Sql & " AND ArticuloInventario.StatusdelArticulo = " & valSqlStatus
   End If
   If valRango Then
      Sql = Sql & " AND " & gUtilSQL.fSQLStringValueBetween("ArticuloInventario.Codigo", gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoDesde), gTexto.fLimpiaStringdeBlancosAAmbosLados(valCodigoHasta))
   End If
   If valLineaProducto Then
      Sql = Sql & " AND ArticuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrLineaProducto))
   End If
   If valCategoria Then
      Sql = Sql & " AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrCategoria))
   End If
   If valMarca Then
      Sql = Sql & " AND ArticuloInventario.Marca = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valStrMarca))
   End If
   If valAlmacen <> "" Then
      Sql = Sql & " AND Almacen.Codigo = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valAlmacen))
   End If
   If Not valMuestraExistenciaCero Then
      Sql = Sql & " AND ArticuloInventario.Existencia <> 0 "
   End If
   Sql = Sql & " ORDER BY " & valSqlOrder
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLDisponibilidadArticuloInventarioPorAlmacen = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDisponibilidadArticuloInventarioPorAlmacen", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlAlertaMaximoMinimo(ByVal valVerificaMaximo As Boolean, ByVal valCodigoArticulo As String, ByVal valCantidad As Currency, ByVal valConsecutivoCompaniaActual As Long) As String
   Dim vSqlWhere As String
   Dim vSql As String
   Dim vCondicion As String
   On Error GoTo h_ERROR
   If (valVerificaMaximo) Then
      vCondicion = "( ( ArticuloInventario.Existencia   + " & gUtilSQL.fSimpleSqlValueForNumeric(valCantidad) & " ) >  ArticuloInventario.CantidadMaxima) "
   Else
      vCondicion = "( ( ArticuloInventario.Existencia   - " & gUtilSQL.fSimpleSqlValueForNumeric(valCantidad) & " ) <  ArticuloInventario.CantidadMinima) "
   End If
   vCondicion = gUtilSQL.getIIF(vCondicion, gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.fSimpleSqlValue("N"), False)
   vSqlWhere = gUtilSQL.fSQLValueWithAnd("", "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo", valCodigoArticulo, False)
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "ArticuloInventario.ConsecutivoCompania", valConsecutivoCompaniaActual)
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   vSql = " SELECT "
   vSql = vSql & vCondicion & "AS Alertar "
   vSql = vSql & " FROM dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1 "
   vSql = vSql & " INNER JOIN   dbo.ArticuloInventario ON"
   vSql = vSql & " dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo = dbo.ArticuloInventario.Codigo AND"
   vSql = vSql & " dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.consecutivoCompania = dbo.ArticuloInventario.consecutivoCompania"
   vSql = vSql & vSqlWhere
   vSql = vSql & " GROUP BY  dbo.ArticuloInventario.Codigo,dbo.ArticuloInventario.Existencia,CantidadMinima,CantidadMaxima "
   fSqlAlertaMaximoMinimo = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSqlAlertaMaximoMinimo = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSearchForListRecords", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function

Public Function fConstruirSQLDelReporteGananciaXProductoSegunLeyCosto(ByVal valConsecutivoCompania As Long, ByVal valCantidadAImprimirLineaDeProducto As String, _
                                                            ByVal valenumCantidadAImprimirToString As String, ByVal valLineaDeProductos As String, ByVal valPrecioVenta As String, ByVal valCostoPromedio As Boolean) As String
   Dim Sql As String
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
   Sql = "SELECT "
   Sql = Sql & "CODIGO , "
   Sql = Sql & "DESCRIPCION , "
   Sql = Sql & "PRECIOSINIVA" + vNivel + " as PRECIOSINIVA , "
   Sql = Sql & " " + vCosto + " as COSTOUNITARIO, "
   Sql = Sql & "LINEADEPRODUCTO ,"
   Sql = Sql & "GastosAdmisibles as porcGastosAdmisibles,"
   Sql = Sql & "MargenGananciaXLey" + vNivel + ","
   Sql = Sql & gUtilSQL.getIIF("" + vCosto + "  <> 0 ", _
                  "(((PRECIOSINIVA" + vNivel + " " & _
                  " - " + vCosto + " " & _
                  ") / " + vCosto + " ) * 100)", _
                  "0", True) & _
               " AS Porcentaje,"
               
   Sql = Sql & gUtilSQL.getIIF("" + vCosto + "  <> 0 ", _
                  "( " + vCosto + " * GastosAdmisibles / 100 )", _
                  "0", True) & _
               " AS GastosAdmisibles,"
               
   Sql = Sql & gUtilSQL.getIIF("" + vCosto + "  <> 0 ", _
                  "( " + vCosto + " + (" + vCosto + " * GastosAdmisibles / 100 ) )", _
                  "0", True) & _
               " AS CostoSegunLey ,"
               
   Sql = Sql & "(PRECIOSINIVA -  " & _
               gUtilSQL.getIIF("" + vCosto + "  <> 0 ", _
                  "( " + vCosto + " + (" + vCosto + " * GastosAdmisibles / 100 ) )", _
                  "0", True) & _
               ") AS Ganancia "
               
   Sql = Sql & " FROM articuloInventario"
   Sql = Sql & " WHERE "
   Sql = Sql & "CONSECUTIVOCOMPANIA   = " & valConsecutivoCompania
   Sql = Sql & " AND TIPODEARTICULO " _
            & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   If valCantidadAImprimirLineaDeProducto = valenumCantidadAImprimirToString Then
       Sql = Sql & " AND LINEADEPRODUCTO " _
            & " = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
   Sql = Sql & " ORDER BY "
   Sql = Sql & "LINEADEPRODUCTO , "
   Sql = Sql & "CODIGO"
 
   fConstruirSQLDelReporteGananciaXProductoSegunLeyCosto = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteGananciaXProductoSegunLeyCosto", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLInsertarArticuloEspecialME(ByVal valConsecutivoCompania As Long, ByVal valCodigo As String, ByVal valDescripcin As String, ByVal valAlicuota As enum_TipoDeAlicuota, ByVal valSqlValueNombreOperador As String, ByVal valSqlValueFechaUltModificacion As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "INSERT INTO camposMonedaExtranjera(ConsecutivoCompania, Codigo, "
   Sql = Sql & "MePrecioSinIva, MePrecioConIva, "
   Sql = Sql & "MePrecioSinIva2, MePrecioConIva2, "
   Sql = Sql & "MePrecioSinIva3, MePrecioConIva3, "
   Sql = Sql & "MePrecioSinIva4, MePrecioConIva4, "
   Sql = Sql & "NombreOperador, FechaUltimaModificacion"
   Sql = Sql & ") SELECT " & valConsecutivoCompania & ", " & gUtilSQL.fSimpleSqlValue(valCodigo)
   Sql = Sql & ",0,0,0,0,0,0,0,0,"
   Sql = Sql & valSqlValueNombreOperador & ", " & valSqlValueFechaUltModificacion
   Sql = Sql & " WHERE NOT EXISTS (SELECT ConsecutivoCompania, Codigo FROM camposMonedaExtranjera WHERE ConsecutivoCompania = " & valConsecutivoCompania & " AND Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigo) & ")"
h_EXIT: On Error GoTo 0
   fSQLInsertarArticuloEspecialME = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLInsertarArticuloEspecialME", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLInsertarSinoExisteLineaDeProducto(ByVal valSqlValueNombreOperador As String, ByVal valSqlValueFechaUltModificacion As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "INSERT INTO Adm.LineaDeProducto (ConsecutivoCompania, Consecutivo, Nombre, PorcentajeComision,CentroDeCosto, NombreOperador, FechaUltimaModificacion) "
   Sql = Sql & "SELECT ConsecutivoCompania, ISNULL((SELECT MAX(Consecutivo) From Adm.LineaDeProducto), 0) + 1, 'LINEA DE PRODUCTO', 0, '', " & valSqlValueNombreOperador & ", "
   Sql = Sql & valSqlValueFechaUltModificacion & " FROM COMPANIA "
   Sql = Sql & " WHERE NOT EXISTS(SELECT Nombre FROM Adm.LineaDeProducto WHERE ConsecutivoCompania = COMPANIA.ConsecutivoCompania AND Nombre = 'LINEA DE PRODUCTO')"
h_EXIT: On Error GoTo 0
   fSQLInsertarSinoExisteLineaDeProducto = Sql
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
