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
Private Const GetGender = Enum_Gender.eg_Male

Public Function fSQLDelReporteNotasDeESEntreFechas(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim Sql As String
   Dim vSqlWhere As String
   Dim insArticuloVista As clsArticuloInvVista
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set insArticuloVista = New clsArticuloInvVista
   
   vSqlWhere = gUtilSQL.fSQLValueDatesBetween("", "notaDeEntradaSalida.Fecha", valFechaInicial, valFechaFinal)
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "StatusNotaEntradaSalida", enum_StatusNotaEntradaSalida.eSNES_Vigente)
   vSqlWhere = vSqlWhere & " AND notaDeEntradaSalida.ConsecutivoCompania = " & valConsecutivoCompania
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   
   Sql = "SELECT "
   Sql = Sql & "notaDeEntradaSalida.Fecha, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("notaDeEntradaSalida.TipoDeOperacion", enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO, gEnumProyecto.fEnumTipodeOperacionToStringInArray(True), "") & " AS TipodeOperacion, "
   Sql = Sql & "notaDeEntradaSalida.NumeroDocumento, notaDeEntradaSalida.CodigoAlmacen, "
   Sql = Sql & "almacen.NombreAlmacen, renglonNotaES.CodigoArticulo, renglonNotaES.Cantidad, "
   Sql = Sql & insArticuloVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion"
   Sql = Sql & " FROM almacen INNER JOIN ("
   Sql = Sql & "notaDeEntradaSalida INNER JOIN ("
   Sql = Sql & insArticuloVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " INNER JOIN "
   Sql = Sql & "renglonNotaES ON ("
   Sql = Sql & insArticuloVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = renglonNotaES.CodigoArticulo"
   Sql = Sql & ") AND ("
   Sql = Sql & insArticuloVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = renglonNotaES.Serial"
   Sql = Sql & ") AND ("
   Sql = Sql & insArticuloVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = renglonNotaES.Rollo"
   Sql = Sql & ") AND ("
   Sql = Sql & insArticuloVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = renglonNotaES.ConsecutivoCompania"
   Sql = Sql & ")) ON ("
   Sql = Sql & "notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento"
   Sql = Sql & ") AND ("
   Sql = Sql & "notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania"
   Sql = Sql & ")) ON ("
   Sql = Sql & "almacen.Codigo" & " = notaDeEntradaSalida.CodigoAlmacen"
   Sql = Sql & ") AND ("
   Sql = Sql & "almacen.ConsecutivoCompania = notaDeEntradaSalida.ConsecutivoCompania)"
   Sql = Sql & vSqlWhere
   Sql = Sql & " Order by notaDeEntradaSalida.NumeroDocumento"
   Set gEnumProyecto = Nothing
   Set insArticuloVista = Nothing
h_EXIT: On Error GoTo 0
   fSQLDelReporteNotasDeESEntreFechas = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDelReporteNotasDeESEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteConteosFisicoEntreFechas(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "conteoFisico.ConsecutivoConteo, "
   Sql = Sql & "conteoFisico.Fecha, "
   Sql = Sql & "conteoFisico.CodigoAlmacen, "
   Sql = Sql & "almacen.NombreAlmacen, "
   Sql = Sql & gUtilSQL.getIIF("conteoFisico.SoloUnaLinea = " & gUtilSQL.fSimpleSqlValue(gConvert.ConvertBooleanToString(True)), "conteoFisico.LineaDeProducto", "'Todas las Línea'", True) & " AS LineaDeProducto, "
   Sql = Sql & "conteoFisico.NombreOperador"
   Sql = Sql & " FROM conteoFisico INNER JOIN "
   Sql = Sql & "almacen ON "
   Sql = Sql & "conteoFisico.ConsecutivoCompania = almacen.ConsecutivoCompania"
   Sql = Sql & " AND conteoFisico.CodigoAlmacen = almacen.Codigo"
   Sql = Sql & " WHERE "
   Sql = Sql & "conteoFisico.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("conteoFisico.Fecha", gConvert.dateToString(valFechaInicial), gConvert.dateToString(valFechaFinal))
   Sql = Sql & " ORDER BY "
   Sql = Sql & "conteoFisico.ConsecutivoConteo, "
   Sql = Sql & "conteoFisico.Fecha"
h_EXIT: On Error GoTo 0
   fSQLDelReporteConteosFisicoEntreFechas = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDelReporteConteosFisicoEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlListaDePreciosME(ByVal valCambioABs As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = Sql & gUtilSQL.getIIF("ISNULL(CamposMonedaExtranjera.MePrecioConIva, 0) > 0 ", "ISNULL(CamposMonedaExtranjera.MePrecioConIva, 0) * " & valCambioABs, " articuloInventario.PrecioConIva ", True) & " AS PrecioConIva, "
   Sql = Sql & gUtilSQL.getIIF("ISNULL(CamposMonedaExtranjera.MePrecioSinIva, 0) > 0 ", "ISNULL(CamposMonedaExtranjera.MePrecioSinIva, 0) * " & valCambioABs, " articuloInventario.PrecioSinIva ", True) & " AS PrecioSinIva, "
   Sql = Sql & gUtilSQL.getIIF("ISNULL(CamposMonedaExtranjera.MePrecioConIva, 0) > 0 ", "ISNULL(CamposMonedaExtranjera.MePrecioConIva, 0)  ", " articuloInventario.PrecioConIva / " & valCambioABs, True) & " AS PrecioConIvaME, "
   Sql = Sql & gUtilSQL.getIIF("ISNULL(CamposMonedaExtranjera.MePrecioSinIva, 0) > 0 ", "ISNULL(CamposMonedaExtranjera.MePrecioSinIva, 0) ", " articuloInventario.PrecioSinIva / " & valCambioABs, True) & " AS PrecioSinIvaME, "
   Sql = Sql & valCambioABs & " AS CambioABolivares "
h_EXIT: On Error GoTo 0
   fSqlListaDePreciosME = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fsqlListaDePreciosME", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



Public Function fQueryNotaES(ByVal valTipoOperacion As enum_TipodeOperacion, ByVal valCodigoArticulo As String, ByVal valCodigoAlmacen As String, ByVal valConsecutivoCompania As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT SUM(renglonNotaES.Cantidad) AS SumCantidad"
   Sql = Sql & " FROM notaDeEntradaSalida INNER JOIN renglonNotaES"
   Sql = Sql & " ON notaDeEntradaSalida.NumeroDocumento=renglonNotaES.NumeroDocumento"
   Sql = Sql & " AND notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania"
   Sql = Sql & " WHERE notaDeEntradaSalida.ConsecutivoCompania=" & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & ""
   Sql = Sql & " AND notaDeEntradaSalida.CodigoAlmacen='" & valCodigoAlmacen & "'"
   Sql = Sql & " AND notaDeEntradaSalida.TipodeOperacion='" & valTipoOperacion & "'"
   Sql = Sql & " AND renglonNotaES.CodigoArticulo='" & valCodigoArticulo & "'"
   fQueryNotaES = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fQueryNotaES", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
