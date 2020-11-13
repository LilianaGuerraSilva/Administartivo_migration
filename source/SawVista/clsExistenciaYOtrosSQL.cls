VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsExistenciaYOtrosSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsExistenciaYOtrosSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Existencia Y Otros"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSQLDelReporteExistenciaPorArticulo(ByVal valCodigoArticulo As String, ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "articuloInventario.Codigo AS CodigoArticulo, "
   SQL = SQL & "articuloInventario.Descripcion, "
   SQL = SQL & "almacen.Codigo AS CodigoAlmacen, "
   SQL = SQL & "almacen.NombreAlmacen, "
   SQL = SQL & "existenciaPorAlmacen.Cantidad"
   SQL = SQL & " FROM " & "articuloInventario INNER JOIN ("
   SQL = SQL & "almacen INNER JOIN existenciaPorAlmacen ON ("
   SQL = SQL & "almacen.Codigo" & _
               " = " & "existenciaPorAlmacen.CodigoAlmacen"
   SQL = SQL & ") AND ("
   SQL = SQL & "almacen." & "ConsecutivoCompania" & _
               " = " & "existenciaPorAlmacen.ConsecutivoCompania"
   SQL = SQL & ")) ON ("
   SQL = SQL & "articuloInventario.Codigo" & _
               " = " & "existenciaPorAlmacen.CodigoArticulo"
   SQL = SQL & ") AND ("
   SQL = SQL & "articuloInventario.ConsecutivoCompania" & _
               " = " & "existenciaPorAlmacen.ConsecutivoCompania" & ")"
   SQL = SQL & " WHERE " & "articuloInventario.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND  ExistenciaPorAlmacen.CodigoArticulo" & _
               " = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoArticulo))
   SQL = SQL & " AND " & "articuloInventario.TipoDeArticulo" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA) 'SQL = SQL & " ORDER BY " & "almacen.Codigo"
   SQL = SQL & " UNION SELECT existenciaPorAlmacen.CodigoArticulo, (ArticuloInventario.Descripcion+' '+Color.DescripcionColor+' '+Talla.DescripcionTalla) AS Descripcion,"
   SQL = SQL & " ExistenciaPorAlmacen.CodigoAlmacen, almacen.NombreAlmacen, existenciaPorAlmacen.Cantidad"
   SQL = SQL & " FROM (((almacen INNER JOIN (ExistenciaPorGrupo INNER JOIN existenciaPorAlmacen ON "
   SQL = SQL & " ((ExistenciaPorGrupo.CodigoArticulo+existenciaporGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTAlla) = existenciaPorAlmacen.CodigoArticulo)"
   SQL = SQL & " AND (ExistenciaPorGrupo.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania)) ON (almacen.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania) "
   SQL = SQL & " AND (almacen.Codigo = existenciaPorAlmacen.CodigoAlmacen)) INNER JOIN articuloInventario ON (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania)"
   SQL = SQL & " AND (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)) INNER JOIN Talla ON (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania) "
   SQL = SQL & " AND (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla)) INNER JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) "
   SQL = SQL & " AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)"
   SQL = SQL & " WHERE " & "articuloInventario.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND  ExistenciaPorAlmacen.CodigoArticulo" & _
               " = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoArticulo))
   SQL = SQL & " AND " & "articuloInventario.TipoDeArticulo" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA) 'SQL = SQL & " ORDER BY " & "almacen.Codigo"
   fSQLDelReporteExistenciaPorArticulo = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelReporteExistenciaPorArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteExistenciaPorAlmacen(ByVal valCodigoAlmacen As String, ByVal valOrdenadoPor As String, ByVal valCantidadAImprimirAlmacen As String, ByVal valConsecutivoCompania As Long, ByVal valFiltrarPorStatusArticulo As Boolean, ByVal valStastusArticulo As Integer, ByRef gEnumReport As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "almacen.Codigo AS CodigoAlmacen, "
   SQL = SQL & "almacen.NombreAlmacen, "
   SQL = SQL & "ExistenciaPorAlmacen.CodigoArticulo,"
   SQL = SQL & "articuloInventario.Descripcion, "
   SQL = SQL & "existenciaPorAlmacen.Cantidad"
   SQL = SQL & ", almacen.TipoDeAlmacen,almacen.Codigo"
   SQL = SQL & " FROM " & "articuloInventario INNER JOIN ("
   SQL = SQL & "almacen INNER JOIN existenciaPorAlmacen ON ("
   SQL = SQL & "almacen.Codigo" & _
               " = " & "existenciaPorAlmacen.CodigoAlmacen"
   SQL = SQL & ") AND ("
   SQL = SQL & "almacen.ConsecutivoCompania" & _
               " = " & "existenciaPorAlmacen.ConsecutivoCompania"
   SQL = SQL & ")) ON ("
   SQL = SQL & "articuloInventario.Codigo" & _
               " = " & "existenciaPorAlmacen.CodigoArticulo"
   SQL = SQL & ") AND ("
   SQL = SQL & "articuloInventario.ConsecutivoCompania" & _
               " = " & "existenciaPorAlmacen.ConsecutivoCompania" & ")"
   SQL = SQL & " WHERE " & "articuloInventario.ConsecutivoCompania" & _
             " = " & valConsecutivoCompania
   SQL = SQL & " AND " & "articuloInventario.TipoDeArticulo" & _
             " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   If valCantidadAImprimirAlmacen = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND " & "almacen.Codigo" & _
                  " = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoAlmacen))
   End If
   If valFiltrarPorStatusArticulo Then
      SQL = SQL & " AND " & "articuloInventario.StatusDelArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(valStastusArticulo)
   End If
   SQL = SQL & " UNION SELECT almacen.Codigo AS CodigoAlmacen, almacen.NombreAlmacen,ExistenciaporAlmacen.CodigoArticulo,(ArticuloInventario.Descripcion+' '+Color.DescripcionColor+' '+Talla.DescripcionTalla) as Descripcion,existenciaPorAlmacen.Cantidad, almacen.TipoDeAlmacen,almacen.Codigo"
   SQL = SQL & " FROM articuloInventario INNER JOIN ((almacen INNER JOIN existenciaPorAlmacen ON (almacen.Codigo = existenciaPorAlmacen.CodigoAlmacen) AND (almacen.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania)) INNER JOIN ((ExistenciaPorGrupo INNER JOIN Talla ON (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla) AND (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania)) INNER JOIN Color ON (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor) AND (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania)) ON (existenciaPorAlmacen.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania) AND (existenciaPorAlmacen.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.codigoColor+ExistenciaPorGrupo.CodigoTalla))) ON (articuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo) AND (articuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)"
   SQL = SQL & " WHERE " & "articuloInventario.ConsecutivoCompania" & _
             " = " & valConsecutivoCompania
   SQL = SQL & " AND " & "articuloInventario.TipoDeArticulo" & _
             " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   If valCantidadAImprimirAlmacen = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND " & "almacen.Codigo" & _
                  " = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoAlmacen))
   End If
   If valFiltrarPorStatusArticulo Then
      SQL = SQL & " AND " & "articuloInventario.StatusDelArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(valStastusArticulo)
   End If
   SQL = SQL & " ORDER BY "
   If valOrdenadoPor = gEnumReport.enumReporteOrdenadoPorToString(eRO_Nombre) Then
      SQL = SQL & "almacen.NombreAlmacen, "
      SQL = SQL & "articuloInventario.Descripcion"
   Else
      SQL = SQL & "CodigoAlmacen,  "
      SQL = SQL & "Existenciaporalmacen.codigoArticulo"
   End If
   fSQLDelReporteExistenciaPorAlmacen = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDelReporteExistenciaPorAlmacen", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteCostoPorAlmacen(ByVal valCodigoAlmacen As String, ByVal valOrdenadoPor As String, ByVal valCantidadAImprimirAlmacen As String, ByVal valOpcionesDeMoneda As String, ByVal valConsecutivoCompania As Long, ByRef gEnumProyecto As Object, ByRef gEnumReport As Object) As String
   Dim SQL As String
   Dim sqlWhere As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "almacen.Codigo AS CodigoAlmacen, "
   SQL = SQL & "almacen.NombreAlmacen, "
   SQL = SQL & "ExistenciaPorAlmacen.CodigoArticulo, "
   SQL = SQL & "articuloInventario.Descripcion, "
   SQL = SQL & "(" & "existenciaPorAlmacen.Cantidad" & _
               " * " & "articuloInventario.CostoUnitario) AS CostoTotal,  "
   If valOpcionesDeMoneda = gEnumProyecto.enumOpcionesParaElReporteListadoDePreciosToString(eORLP_PRECIODEVENTAMONEDAEXTRANJERA) _
      Then
      SQL = SQL & "articuloInventario.MeCostoUnitario,  "
      SQL = SQL & "(" & "existenciaPorAlmacen.Cantidad" & _
                  " * " & "articuloInventario.MeCostoUnitario) AS CostoTotalME, "
   End If
   SQL = SQL & "existenciaPorAlmacen.Cantidad" & ", "
   SQL = SQL & "articuloInventario.CostoUnitario"
   SQL = SQL & " FROM " & "articuloInventario INNER JOIN ("
   SQL = SQL & "almacen INNER JOIN existenciaPorAlmacen ON ("
   SQL = SQL & "almacen.Codigo" & _
               " = " & "existenciaPorAlmacen.CodigoAlmacen"
   SQL = SQL & ") AND ("
   SQL = SQL & "almacen.ConsecutivoCompania" & _
               " = " & "existenciaPorAlmacen.ConsecutivoCompania"
   SQL = SQL & ")) ON ("
   SQL = SQL & "articuloInventario.Codigo" & _
               " = " & "existenciaPorAlmacen.CodigoArticulo"
   SQL = SQL & ") AND ("
   SQL = SQL & "articuloInventario.ConsecutivoCompania" & _
               " = " & "existenciaPorAlmacen.ConsecutivoCompania" & ")"
   sqlWhere = " WHERE " & "articuloInventario.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   sqlWhere = sqlWhere & " AND " & "articuloInventario.TipoDeArticulo" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   If valCantidadAImprimirAlmacen = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      sqlWhere = sqlWhere & " AND " & "almacen.Codigo" & _
                  " = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoAlmacen))
   End If
   SQL = SQL & sqlWhere
'   SQL = SQL & " ORDER BY " 'SQL = SQL & "almacen.TipoDeAlmacen, "
'   If valOrdenadoPor = gEnumReport.enumReporteOrdenadoPorToString(eRO_Nombre) Then
'      SQL = SQL & "almacen.NombreAlmacen, "
'      SQL = SQL & "articuloInventario.Descripcion"
'   Else
'      SQL = SQL & "almacen.Codigo, "
'      SQL = SQL & "ExistenciaPorALmacen.CodigoArticulo"
'   End If
   SQL = SQL & " UNION SELECT almacen.Codigo AS CodigoAlmacen, almacen.NombreAlmacen, "
   SQL = SQL & " RenglonExistenciaAlmacen.CodigoArticulo, "
   SQL = SQL & " (articuloInventario.Descripcion+' '+Talla.DescripcionTalla+' '+Color.DescripcionColor) as Descripcion, "
    If valOpcionesDeMoneda = gEnumProyecto.enumOpcionesParaElReporteListadoDePreciosToString(eORLP_PRECIODEVENTAMONEDAEXTRANJERA) Then
    SQL = SQL & "0, 0, "
   End If

   SQL = SQL & "(existenciaPorAlmacen.Cantidad*articuloInventario.CostoUnitario) AS CostoTotal, "
   SQL = SQL & " existenciaPorAlmacen.Cantidad, articuloInventario.CostoUnitario"
   SQL = SQL & " FROM (((almacen INNER JOIN existenciaPorAlmacen ON (almacen.Codigo = existenciaPorAlmacen.CodigoAlmacen) "
   SQL = SQL & " AND (almacen.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania)) "
   SQL = SQL & " INNER JOIN ((RenglonExistenciaAlmacen INNER JOIN ExistenciaPorGrupo ON "
   SQL = SQL & " (RenglonExistenciaAlmacen.CodigoArticulo =(ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla))"
   SQL = SQL & " AND (RenglonExistenciaAlmacen.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)) "
   SQL = SQL & " INNER JOIN articuloInventario ON (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) "
   SQL = SQL & " AND (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)) ON "
   SQL = SQL & " (existenciaPorAlmacen.CodigoArticulo = RenglonExistenciaAlmacen.CodigoArticulo) "
   SQL = SQL & " AND (existenciaPorAlmacen.CodigoAlmacen = RenglonExistenciaAlmacen.CodigoAlmacen) "
   SQL = SQL & " AND (existenciaPorAlmacen.ConsecutivoCompania = RenglonExistenciaAlmacen.ConsecutivoCompania)) "
   SQL = SQL & " INNER JOIN Talla ON (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla) AND"
   SQL = SQL & " (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania)) INNER JOIN Color ON "
   SQL = SQL & " (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor) AND (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania)"
   SQL = SQL & sqlWhere
   SQL = SQL & " ORDER BY " 'SQL = SQL & "almacen.TipoDeAlmacen, "
     If valOrdenadoPor = gEnumReport.enumReporteOrdenadoPorToString(eRO_Nombre) Then
        SQL = SQL & "almacen.NombreAlmacen, "
        SQL = SQL & "articuloInventario.Descripcion"
     Else
        SQL = SQL & "almacen.Codigo, "
        SQL = SQL & "ExistenciaPorALmacen.CodigoArticulo"
     End If
   fSQLDelReporteCostoPorAlmacen = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelReporteCostoPorAlmacen", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteFormatoDeConteoFisico(ByVal valCodigoAlmacen As String, ByVal valLineaDeProductos As String, ByVal valCantidadAImprimirLineaDeProducto As String, ByVal valCantidadAImprimirAlmacen As String, ByVal valConsecutivoCompania As Long, ByRef gEnumReport As Object) As String
   Dim sqlWhere As String
   Dim SQL As String
   On Error GoTo h_ERROR
   sqlWhere = sqlWhere & " WHERE " & "Almacen.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   sqlWhere = sqlWhere & " AND " & "articuloInventario.TipoDeArticulo" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeArticulo.eTD_MERCANCIA)
   sqlWhere = sqlWhere & " AND " & "articuloInventario.StatusdelArticulo" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusArticulo.eSA_VIGENTE)
   If valCantidadAImprimirAlmacen = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      sqlWhere = sqlWhere & " AND " & "Almacen.Codigo" & _
                  " = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoAlmacen))
   End If
   If valCantidadAImprimirLineaDeProducto = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      sqlWhere = sqlWhere & " AND " & "articuloInventario.LineaDeProducto" & _
                  " = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductos))
   End If
      SQL = "SELECT ExistenciaPorAlmacen.CodigoArticulo, articuloInventario.Descripcion, articuloInventario.LineaDeProducto, almacen.Codigo AS CodigoAlmacen, "
      SQL = SQL & " almacen.NombreAlmacen, existenciaPorAlmacen.Cantidad "
      SQL = SQL & " FROM almacen INNER JOIN (articuloInventario INNER JOIN existenciaPorAlmacen ON (articuloInventario.Codigo = existenciaPorAlmacen.CodigoArticulo) "
      SQL = SQL & " AND (articuloInventario.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania)) ON (almacen.Codigo = existenciaPorAlmacen.CodigoAlmacen) "
      SQL = SQL & " AND (almacen.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania) "
      SQL = SQL & sqlWhere
      SQL = SQL & " UNION "
      SQL = SQL & " SELECT ExistenciaPorAlmacen.CodigoArticulo, (articuloInventario.Descripcion+' '+Color.DescripcionColor+' '+Talla.DescripcionTalla)as Descripcion, articuloInventario.LineaDeProducto, almacen.Codigo AS CodigoAlmacen,"
      SQL = SQL & " almacen.NombreAlmacen, existenciaPorAlmacen.Cantidad"
      SQL = SQL & " FROM (almacen INNER JOIN existenciaPorAlmacen ON (almacen.Codigo = existenciaPorAlmacen.CodigoAlmacen) AND (almacen.ConsecutivoCompania = existenciaPorAlmacen.ConsecutivoCompania))"
      SQL = SQL & " INNER JOIN (((ExistenciaPorGrupo INNER JOIN Talla ON (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla)) "
      SQL = SQL & " INNER JOIN Color ON (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)) "
      SQL = SQL & " INNER JOIN articuloInventario ON (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) AND (ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo)) "
      SQL = SQL & " ON (existenciaPorAlmacen.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGRupo.codigoTalla)) "
      SQL = SQL & " AND (existenciaPorAlmacen.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)"
      SQL = SQL & sqlWhere
      SQL = SQL & " ORDER BY "
      SQL = SQL & "CodigoAlmacen,"
      SQL = SQL & "articuloInventario.LineaDeProducto,"
      SQL = SQL & "ExistenciaPorAlmacen.CodigoArticulo"
   fSQLDelReporteFormatoDeConteoFisico = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fSQLDelReporteFormatoDeConteoFisico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

