VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsNotaESSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsNotaESSQL"
Private Const CM_MESSAGE_NAME As String = "Nota ES SQL"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSQLDelReporteNotasDeESEntreFechas(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valFiltrarPorCliente As Boolean, ByVal valNombreCliente As String) As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim Sql As String
   Dim vSQLWhere As String
   Dim insArticuloVista As clsArticuloInvVista
   
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set insArticuloVista = New clsArticuloInvVista
   
   vSQLWhere = gUtilSQL.fSQLValueDatesBetween("", "notaDeEntradaSalida.Fecha", valFechaInicial, valFechaFinal)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "StatusNotaEntradaSalida", enum_StatusNotaEntradaSalida.eSNES_Vigente)
   vSQLWhere = vSQLWhere & " AND " & "notaDeEntradaSalida.ConsecutivoCompania" & " = " & valConsecutivoCompania
   If (valFiltrarPorCliente) Then
   vSQLWhere = vSQLWhere & " AND " & "notaDeEntradaSalida.CodigoCliente = (SELECT Codigo FROM cliente WHERE Nombre='" & valNombreCliente & "' AND consecutivoCompania=" & valConsecutivoCompania & ")"
   End If
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   Sql = "SELECT "
   Sql = Sql & "notaDeEntradaSalida.Fecha, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("notaDeEntradaSalida.TipoDeOperacion", enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO, gEnumProyecto.fEnumTipodeOperacionToStringInArray(True), "") & " AS TipodeOperacion, "
   Sql = Sql & "notaDeEntradaSalida.NumeroDocumento, "
   Sql = Sql & "notaDeEntradaSalida.CodigoAlmacen, "
   Sql = Sql & "almacen.NombreAlmacen, "
   Sql = Sql & "renglonNotaES.CodigoArticulo, "
   Sql = Sql & "renglonNotaES.Cantidad, "
   Sql = Sql & insArticuloVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion"
   Sql = Sql & " FROM almacen INNER JOIN ("
   Sql = Sql & "notaDeEntradaSalida INNER JOIN ("
   Sql = Sql & insArticuloVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " INNER JOIN "
   Sql = Sql & "renglonNotaES ON ("
   Sql = Sql & insArticuloVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo" & _
               " = renglonNotaES.CodigoArticulo"
   Sql = Sql & ") AND ("
   Sql = Sql & insArticuloVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial" & _
               " = renglonNotaES.Serial"
   Sql = Sql & ") AND ("
   Sql = Sql & insArticuloVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo" & _
               " = renglonNotaES.Rollo"
   Sql = Sql & ") AND ("
   Sql = Sql & insArticuloVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania" & _
               " = renglonNotaES.ConsecutivoCompania"
   Sql = Sql & ")) ON ("
   Sql = Sql & "notaDeEntradaSalida.NumeroDocumento" & _
               " = renglonNotaES.NumeroDocumento"
   Sql = Sql & ") AND ("
   Sql = Sql & "notaDeEntradaSalida.ConsecutivoCompania" & _
               " = renglonNotaES.ConsecutivoCompania"
   Sql = Sql & ")) ON ("
   Sql = Sql & "almacen.Codigo" & _
               " = notaDeEntradaSalida.CodigoAlmacen"
   Sql = Sql & ") AND ("
   Sql = Sql & "almacen.ConsecutivoCompania" & _
               " = notaDeEntradaSalida.ConsecutivoCompania)"
   Sql = Sql & vSQLWhere
   Sql = Sql & " Order by notaDeEntradaSalida.NumeroDocumento"
   Set gEnumProyecto = Nothing
   Set insArticuloVista = Nothing
h_EXIT: On Error GoTo 0
   fSQLDelReporteNotasDeESEntreFechas = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDelReporteNotasDeESEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteConteosFisicoEntreFechas(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "conteoFisico.ConsecutivoConteo, "
   Sql = Sql & "conteoFisico.Fecha, "
   Sql = Sql & "conteoFisico.CodigoAlmacen, "
   Sql = Sql & "almacen.NombreAlmacen, "
   Sql = Sql & gUtilSQL.getIIF("conteoFisico.SoloUnaLinea" & _
               " = " & gUtilSQL.fSimpleSqlValue(gConvert.ConvertBooleanToString(True)), _
               "conteoFisico.LineaDeProducto", _
               "'Todas las Línea'", True) & " AS LineaDeProducto, "
   Sql = Sql & "conteoFisico.NombreOperador"
   Sql = Sql & " FROM conteoFisico INNER JOIN "
   Sql = Sql & "almacen ON "
   Sql = Sql & "conteoFisico.ConsecutivoCompania" & _
               " = almacen.ConsecutivoCompania"
   Sql = Sql & " AND conteoFisico.CodigoAlmacen" & _
               " = almacen.Codigo"
   Sql = Sql & " WHERE "
   Sql = Sql & "conteoFisico.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("conteoFisico.Fecha", _
               gConvert.dateToString(valFechaInicial), gConvert.dateToString(valFechaFinal))
   Sql = Sql & " ORDER BY "
   Sql = Sql & "conteoFisico.ConsecutivoConteo, "
   Sql = Sql & "conteoFisico.Fecha"
h_EXIT: On Error GoTo 0
   fSQLDelReporteConteosFisicoEntreFechas = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelReporteConteosFisicoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlListaDePreciosME(ByVal valCambioABs As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   sql = sql & gUtilsql.getIIF("CamposMonedaExtranjera.MePrecioConIva > 0 ", " CamposMonedaExtranjera.MePrecioConIva * " & valCambioABs, " articuloInventario.PrecioConIva ", True) & " AS PrecioConIva, "
   sql = sql & gUtilsql.getIIF("CamposMonedaExtranjera.MePrecioSinIva > 0 ", " CamposMonedaExtranjera.MePrecioSinIva * " & valCambioABs, " articuloInventario.PrecioSinIva ", True) & " AS PrecioSinIva, "
   sql = sql & gUtilsql.getIIF("CamposMonedaExtranjera.MePrecioConIva > 0 ", " CamposMonedaExtranjera.MePrecioConIva  ", " articuloInventario.PrecioConIva / " & valCambioABs, True) & " AS PrecioConIvaME, "
   sql = sql & gUtilsql.getIIF("CamposMonedaExtranjera.MePrecioSinIva > 0 ", " CamposMonedaExtranjera.MePrecioSinIva  ", " articuloInventario.PrecioSinIva / " & valCambioABs, True) & " AS PrecioSinIvaME, "
   sql = sql & " " & valCambioABs & " As CambioABolivares "
h_EXIT: On Error GoTo 0
   fsqlListaDePreciosME = sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fsqlListaDePreciosME", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fsqlReporteNotaEntradaSalida(ByVal valConsecutivoCompania As Long, ByVal valNumeroDocumento As String, ByVal valUsaListaDePreciosME As Boolean, ByVal valCambioABolivares As Currency) As String

   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT notaDeEntradaSalida.NumeroDocumento, "
   Sql = Sql & "notaDeEntradaSalida.Fecha, "
   Sql = Sql & "notaDeEntradaSalida.CodigoAlmacen, "
   Sql = Sql & "notaDeEntradaSalida.CodigoCliente, "
   Sql = Sql & "notaDeEntradaSalida.TipodeOperacion, "
   Sql = Sql & "notaDeEntradaSalida.Comentarios, "
   Sql = Sql & "renglonNotaES.CodigoArticulo, "
   Sql = Sql & "renglonNotaES.Cantidad, "
   Sql = Sql & "articuloInventario.Descripcion, "
   Sql = Sql & "renglonNotaES.TipoArticuloInv, "
   Sql = Sql & "renglonNotaES.Serial, "
   Sql = Sql & "RenglonNotaES.Rollo, "
   Sql = Sql & "renglonNotaES.consecutivorenglon, "
   If (valUsaListaDePreciosME) Then
      sql = sql & fsqlListaDePreciosME(gUtilsql.fNumToStrsql(valCambioABolivares))
   Else
   Sql = Sql & "articuloInventario.PrecioSinIva, "
   Sql = Sql & "articuloInventario.PrecioConIva "
   End If
   Sql = Sql & " FROM (notaDeEntradaSalida INNER JOIN renglonNotaES ON "
   Sql = Sql & " (notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania) "
   Sql = Sql & " AND (notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento)) "
   Sql = Sql & " INNER JOIN articuloInventario ON (renglonNotaES.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) "
   Sql = Sql & " AND (renglonNotaES.CodigoArticulo = articuloInventario.Codigo)"
   If (valUsaListaDePreciosME) Then
      sql = sql & " RIGHT JOIN CamposMonedaExtranjera ON "
      sql = sql & " CamposMonedaExtranjera.Codigo = renglonNotaES.CodigoArticulo "
      sql = sql & " AND CamposMonedaExtranjera.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania "
   End If
   Sql = Sql & " WHERE NotaDeEntradaSalida.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND NotaDeEntradaSalida.NumeroDocumento = " & gUtilSQL.fSimpleSqlValue(valNumeroDocumento)
   Sql = Sql & " AND notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento"
   Sql = Sql & " GROUP BY notaDeEntradaSalida.NumeroDocumento, notaDeEntradaSalida.Fecha, notaDeEntradaSalida.CodigoAlmacen, notaDeEntradaSalida.CodigoCliente, notaDeEntradaSalida.TipodeOperacion, "
   Sql = Sql & " notaDeEntradaSalida.Comentarios, renglonNotaES.CodigoArticulo, renglonNotaES.Cantidad, articuloInventario.Descripcion, renglonNotaES.TipoArticuloInv, RenglonNotaES.Serial, RenglonNotaES.Rollo, renglonNotaES.consecutivorenglon, "
   Sql = Sql & " articuloInventario.PrecioSinIva, articuloInventario.PrecioConIva"
   If (valUsaListaDePreciosME) Then
      sql = sql & ", CamposMonedaExtranjera.MePrecioSinIva "
      sql = sql & ", CamposMonedaExtranjera.MEPrecioConIva "
   End If
   Sql = Sql & " UNION"
   Sql = Sql & " SELECT notaDeEntradaSalida.NumeroDocumento, "
   Sql = Sql & "notaDeEntradaSalida.Fecha, "
   Sql = Sql & "notaDeEntradaSalida.CodigoAlmacen, "
   Sql = Sql & "notaDeEntradaSalida.CodigoCliente, "
   Sql = Sql & "notaDeEntradaSalida.TipodeOperacion, "
   Sql = Sql & "notaDeEntradaSalida.Comentarios, "
   Sql = Sql & "renglonNotaES.CodigoArticulo, "
   Sql = Sql & "renglonNotaES.Cantidad, "
   Sql = Sql & "(ArticuloInventario.Descripcion +" & gUtilSQL.fSimpleSqlValue(" ") & "+ ISNULL(Color.DescripcionColor,'')+" & gUtilSQL.fSimpleSqlValue(" ") & "+ ISNULL(Talla.DescripcionTalla,'')" & ") AS Descripcion, "
   Sql = Sql & "renglonNotaES.TipoArticuloInv, "
   Sql = Sql & "renglonNotaES.Serial, "
   Sql = Sql & "RenglonNotaES.Rollo, "
   Sql = Sql & "renglonNotaES.consecutivorenglon, "
   If (valUsaListaDePreciosME) Then
      sql = sql & fSqlListaDePreciosME(gUtilSQL.fNumToStrSql(valCambioABolivares))
   Else
   	sql = sql & "articuloInventario.PrecioSinIva, "
   	sql = sql & "articuloInventario.PrecioConIva "
   End If
   	sql = sql & " FROM (notaDeEntradaSalida INNER JOIN (renglonNotaES INNER JOIN ((ExistenciaPorGrupo LEFT JOIN Talla ON (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania) "
	sql = sql & " AND (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla)) LEFT JOIN Color "
	sql = sql & " ON (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor) "
	sql = sql & " AND (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania)) "
	sql = sql & " ON (renglonNotaES.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania) "
	sql = sql & " AND (renglonNotaES.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.codigoTalla))) "
	sql = sql & " ON (notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento) "
	sql = sql & " AND (notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania)) "
	sql = sql & " LEFT JOIN articuloInventario ON (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo) "
	sql = sql & " AND (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)"
   If (valUsaListaDePreciosME) Then
      sql = sql & " RIGHT JOIN CamposMonedaExtranjera ON "
      sql = sql & " CamposMonedaExtranjera.Codigo = renglonNotaES.CodigoArticulo "
      sql = sql & " AND CamposMonedaExtranjera.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania "
   End If
   Sql = Sql & " WHERE NotaDeEntradaSalida.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND NotaDeEntradaSalida.NumeroDocumento = " & gUtilSQL.fSimpleSqlValue(valNumeroDocumento)
   Sql = Sql & " AND notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento"
   Sql = Sql & " GROUP BY notaDeEntradaSalida.NumeroDocumento, notaDeEntradaSalida.Fecha, notaDeEntradaSalida.CodigoAlmacen, notaDeEntradaSalida.CodigoCliente, notaDeEntradaSalida.TipodeOperacion, notaDeEntradaSalida.Comentarios, renglonNotaES.CodigoArticulo, renglonNotaES.Cantidad, articuloInventario.Descripcion, Color.DescripcionColor, Talla.DescripcionTalla, renglonNotaES.TipoArticuloInv, RenglonNotaES.Serial, RenglonNotaES.Rollo, renglonNotaES.consecutivorenglon, "
   Sql = Sql & " articuloInventario.PrecioSinIva, articuloInventario.PrecioConIva"
   If (valUsaListaDePreciosME) Then
      sql = sql & ", CamposMonedaExtranjera.MePrecioSinIva "
      sql = sql & ", CamposMonedaExtranjera.MEPrecioConIva "
   End If
   Sql = Sql & " ORDER BY  NotaDeEntradaSalida.NumeroDocumento,"
   Sql = Sql & " renglonNotaES.CodigoArticulo"
h_EXIT: On Error GoTo 0
   fSQLReporteNotaEntradaSalida = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLReporteNotaEntradaSalida", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fQueryNotaES(ByVal valTipoOperacion As enum_TipodeOperacion, ByVal valCodigoArticulo As String, ByVal valCodigoAlmacen As String, ByVal valConsecutivoCompania As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT SUM(" & "renglonNotaES" & ".Cantidad) AS SumCantidad"
   Sql = Sql & " FROM " & "notaDeEntradaSalida" & " INNER JOIN " & "renglonNotaES" & ""
   Sql = Sql & " ON " & "notaDeEntradaSalida" & ".NumeroDocumento=" & "renglonNotaES" & ".NumeroDocumento"
   Sql = Sql & " AND " & "notaDeEntradaSalida" & ".ConsecutivoCompania = " & "renglonNotaES" & ".ConsecutivoCompania"
   Sql = Sql & " WHERE " & "notaDeEntradaSalida" & ".ConsecutivoCompania=" & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & ""
   Sql = Sql & " AND " & "notaDeEntradaSalida" & ".CodigoAlmacen='" & valCodigoAlmacen & "'"
   Sql = Sql & " AND " & "notaDeEntradaSalida" & ".TipodeOperacion='" & valTipoOperacion & "'"
   Sql = Sql & " AND " & "renglonNotaES" & ".CodigoArticulo='" & valCodigoArticulo & "'"
   fQueryNotaES = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fQueryNotaES", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

