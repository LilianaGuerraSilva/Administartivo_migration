VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsComprasSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsComprasSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Compras"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function eRC_UNARTICULO() As Integer
   eRC_UNARTICULO = 0
End Function

Public Function eRC_UNALINEADEPRODUCTO() As Integer
   eRC_UNALINEADEPRODUCTO = 1
End Function

Public Function eRC_TODOSLOSARTICULOS() As Integer
   eRC_TODOSLOSARTICULOS = 2
End Function

Public Function enumReporteCostoDeComprasToString(ByVal ItemAImprimir As Integer) As String
   On Error GoTo h_ERROR
   Select Case ItemAImprimir
      Case eRC_UNARTICULO: enumReporteCostoDeComprasToString = "Un Articulo"
      Case eRC_UNALINEADEPRODUCTO: enumReporteCostoDeComprasToString = "Una Linea"
      Case eRC_TODOSLOSARTICULOS: enumReporteCostoDeComprasToString = "Todos Los Articulos"
      Case Else: enumReporteCostoDeComprasToString = enumReporteCostoDeComprasToString(0)
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "enumReporteCostoDeComprasToString", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLComprasEntreFechas(ByVal valConsecutivoCompania As Long, _
                        ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCambioOriginal As Boolean, ByVal valMostrarComprasAnuladas As Boolean, ByVal valMuestraDetalle As Boolean) As String
   Dim SQL As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim insComprasVista As clsComprasVista
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   Set insComprasVista = New clsComprasVista
   vWhere = ""
   
If (valCambioOriginal) Then
    vWhere = insComprasVista.GetViewNameComprasConCambioOriginal() & ".EsUnaOrdenDeCompra = " & gUtilSQL.fSimpleSqlValue("N")
    vWhere = gUtilSQL.fSQLValueDatesBetween(vWhere, insComprasVista.GetViewNameComprasConCambioOriginal() & " .Fecha", valFechaInicial, valFechaFinal)
    vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
    vWhere = gUtilSQL.getWhereSQL(vWhere)
    
    If Not valMostrarComprasAnuladas Then
     vWhere = vWhere & " AND " & insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra = " & gUtilSQL.fSimpleSqlValue(enum_StatusCompra.eSCp_VIGENTE)
    End If
    
    If valMuestraDetalle Then
     SQL = " SELECT "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Fecha,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Numero,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".NombreProveedor,"
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo,"
     SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial", True) & " AS Serial, "
     SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo =  '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo", True) & " AS Rollo, "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".cantidadrecibida,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglones AS totalCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".CostoUnitario,"
    
     If (valCambioOriginal) Then
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglonesCambio  AS CompraTotalAlcambio,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".CambioAbolivares AS cambio,"
     Else
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglonesCambio  AS CompraTotalAlcambio,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".cambioalafecha AS cambio,"
     End If
    
     If (valCambioOriginal) Then
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioOriginal() & ".CostoUnitario) AS costoAlcambio, "
     Else
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioOriginal() & ".cambioalafecha" & " * " & insComprasVista.GetViewNameComprasConCambioOriginal() & ".CostoUnitario) AS costoAlcambio, "
     End If
     SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
     SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
    
     SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioOriginal & " ON "
    
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".ConsecutivoCompania AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CodigoArticulo AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Serial AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Rollo  "
    
     SQL = SQL & vWhere
     SQL = SQL & " Order By "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
     SQL = SQL & " fecha,"
     SQL = SQL & " numero"
    Else
     SQL = " SELECT "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Fecha,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Numero,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".NombreProveedor,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS CodigoDelArticulo,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Serial,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Rollo,"
     SQL = SQL & "0 AS cantidadrecibida,"
     SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglones) AS totalCompra,"
     SQL = SQL & "0 AS CostoUnitario,"
     If (valCambioOriginal) Then
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglonesCambio)  AS CompraTotalAlcambio,"
      SQL = SQL & "MAX(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".CambioAbolivares) AS cambio,"
     Else
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglonesCambio)  AS CompraTotalAlcambio,"
      SQL = SQL & "MAX(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".cambioalafecha) AS cambio,"
     End If
     SQL = SQL & "0 AS costoAlcambio, "
     SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
     SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
     SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioOriginal & " ON "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".ConsecutivoCompania AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CodigoArticulo AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Serial AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Rollo  "
     SQL = SQL & vWhere
     SQL = SQL & " GROUP BY  "
     SQL = SQL & " IGV_ComprasConCambioOriginal.Moneda,IGV_ComprasConCambioOriginal.Fecha, "
     SQL = SQL & " IGV_ComprasConCambioOriginal.Numero, IGV_ComprasConCambioOriginal.NombreProveedor, "
     SQL = SQL & " StatusCompra"
     SQL = SQL & " Order By "
     SQL = SQL & " StatusCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
     SQL = SQL & " fecha,"
     SQL = SQL & " numero"
    End If
   
   Set insArticuloInvVista = Nothing
   Set insComprasVista = Nothing
Else
    vWhere = insComprasVista.GetViewNameComprasConCambioAlDia() & ".EsUnaOrdenDeCompra = " & gUtilSQL.fSimpleSqlValue("N")
    vWhere = gUtilSQL.fSQLValueDatesBetween(vWhere, insComprasVista.GetViewNameComprasConCambioAlDia() & " .Fecha", valFechaInicial, valFechaFinal)
    vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
    vWhere = gUtilSQL.getWhereSQL(vWhere)
    If Not valMostrarComprasAnuladas Then
     vWhere = vWhere & " AND " & insComprasVista.GetViewNameComprasConCambioAlDia & ".StatusCompra = " & gUtilSQL.fSimpleSqlValue(enum_StatusCompra.eSCp_VIGENTE)
    End If
   
    If valMuestraDetalle Then
     SQL = " SELECT "
    
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Moneda,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Fecha,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Numero,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".NombreProveedor,"
   
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo,"
     SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial", True) & " AS Serial, "
     SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo =  '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo", True) & " AS Rollo, "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".cantidadrecibida,"
   
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglones AS totalCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".CostoUnitario,"
     If (valCambioOriginal) Then
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglonesCambio  AS CompraTotalAlcambio,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".CambioAbolivares AS cambio,"
     Else
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglonesCambio  AS CompraTotalAlcambio,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".cambioalafecha AS cambio,"
     End If
     If (valCambioOriginal) Then
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioAlDia() & ".CostoUnitario) AS costoAlcambio, "
     Else
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioAlDia() & ".cambioalafecha" & " * " & insComprasVista.GetViewNameComprasConCambioAlDia() & ".CostoUnitario) AS costoAlcambio, "
     End If
     SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioAlDia & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
     SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
    
     SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioAlDia & " ON "
    
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".ConsecutivoCompania AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".CodigoArticulo AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".Serial AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".Rollo  "
    
     SQL = SQL & vWhere
     SQL = SQL & " Order By "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".StatusCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Moneda,"
     SQL = SQL & " fecha,"
     SQL = SQL & " numero"
    Else
     SQL = " SELECT "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Moneda,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Fecha,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Numero,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".NombreProveedor,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS CodigoDelArticulo,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Serial,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Rollo,"
     SQL = SQL & "0 AS cantidadrecibida,"
     SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglones) AS totalCompra,"
     SQL = SQL & "0 AS CostoUnitario,"
     If (valCambioOriginal) Then
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglonesCambio)  AS CompraTotalAlcambio,"
      SQL = SQL & "MAX(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".CambioAbolivares) AS cambio,"
     Else
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglonesCambio)  AS CompraTotalAlcambio,"
      SQL = SQL & "MAX(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".cambioalafecha) AS cambio,"
     End If
     SQL = SQL & "0 AS costoAlcambio, "
     SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioAlDia & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
     SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
     SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioAlDia & " ON "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".ConsecutivoCompania AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".CodigoArticulo AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".Serial AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".Rollo  "
     SQL = SQL & vWhere
     SQL = SQL & " GROUP BY  "
     SQL = SQL & " IGV_ComprasConCambioAlDia.Moneda,IGV_ComprasConCambioAlDia.Fecha, "
     SQL = SQL & " IGV_ComprasConCambioAlDia.Numero, IGV_ComprasConCambioAlDia.NombreProveedor, "
     SQL = SQL & " StatusCompra"
     SQL = SQL & " Order By "
     SQL = SQL & " StatusCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Moneda,"
     SQL = SQL & " fecha,"
     SQL = SQL & " numero"
    End If
   Set insArticuloInvVista = Nothing
   Set insComprasVista = Nothing
End If
  
  fConstruirSQLComprasEntreFechas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLComprasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLComprasEntreFechasTotales(ByVal valConsecutivoCompania As Long, _
                        ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCambioOriginal As Boolean, ByVal valMostrarComprasAnuladas As Boolean, ByVal valMuestraDetalle As Boolean, ByVal valTotales As Boolean) As String
   Dim SQL As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim insComprasVista As clsComprasVista
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   Set insComprasVista = New clsComprasVista
   vWhere = ""
   
If (valCambioOriginal) Then
    vWhere = insComprasVista.GetViewNameComprasConCambioOriginal() & ".EsUnaOrdenDeCompra = " & gUtilSQL.fSimpleSqlValue("N")
    vWhere = gUtilSQL.fSQLValueDatesBetween(vWhere, insComprasVista.GetViewNameComprasConCambioOriginal() & " .Fecha", valFechaInicial, valFechaFinal)
    vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
    vWhere = gUtilSQL.getWhereSQL(vWhere)
    
    If Not valMostrarComprasAnuladas Then
     vWhere = vWhere & " AND " & insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra = " & gUtilSQL.fSimpleSqlValue(enum_StatusCompra.eSCp_VIGENTE)
    End If
    
    If valMuestraDetalle Then
     SQL = " SELECT "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Fecha,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Numero,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".NombreProveedor,"
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo,"
     SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial", True) & " AS Serial, "
     SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo =  '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo", True) & " AS Rollo, "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".cantidadrecibida,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglones AS totalCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".CostoUnitario,"
    
     If (valCambioOriginal) Then
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglonesCambio  AS CompraTotalAlcambio,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".CambioAbolivares AS cambio,"
     Else
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglonesCambio  AS CompraTotalAlcambio,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".cambioalafecha AS cambio,"
     End If
    
     If (valCambioOriginal) Then
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioOriginal() & ".CostoUnitario) AS costoAlcambio, "
     Else
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioOriginal() & ".cambioalafecha" & " * " & insComprasVista.GetViewNameComprasConCambioOriginal() & ".CostoUnitario) AS costoAlcambio, "
     End If
     SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
     SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
    
     SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioOriginal & " ON "
    
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".ConsecutivoCompania AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CodigoArticulo AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Serial AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Rollo  "
    
     SQL = SQL & vWhere
     SQL = SQL & " Order By "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
     SQL = SQL & " fecha,"
     SQL = SQL & " numero"
    Else
     SQL = " SELECT "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Fecha,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Numero,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".NombreProveedor,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS CodigoDelArticulo,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Serial,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Rollo,"
     SQL = SQL & "0 AS cantidadrecibida,"
     SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglones) AS totalCompra,"
     SQL = SQL & "0 AS CostoUnitario,"
     If (valCambioOriginal) Then
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglonesCambio)  AS CompraTotalAlcambio,"
      SQL = SQL & "MAX(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".CambioAbolivares) AS cambio,"
     Else
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglonesCambio)  AS CompraTotalAlcambio,"
      SQL = SQL & "MAX(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".cambioalafecha) AS cambio,"
     End If
     SQL = SQL & "0 AS costoAlcambio, "
     SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
     SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
     SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioOriginal & " ON "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".ConsecutivoCompania AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CodigoArticulo AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Serial AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Rollo  "
     SQL = SQL & vWhere
     SQL = SQL & " GROUP BY  "
     SQL = SQL & " IGV_ComprasConCambioOriginal.Moneda,IGV_ComprasConCambioOriginal.Fecha, "
     SQL = SQL & " IGV_ComprasConCambioOriginal.Numero, IGV_ComprasConCambioOriginal.NombreProveedor, "
     SQL = SQL & " StatusCompra"
     SQL = SQL & " Order By "
     SQL = SQL & " StatusCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
     SQL = SQL & " fecha,"
     SQL = SQL & " numero"
    End If
   
   Set insArticuloInvVista = Nothing
   Set insComprasVista = Nothing
Else
    vWhere = insComprasVista.GetViewNameComprasConCambioAlDia() & ".EsUnaOrdenDeCompra = " & gUtilSQL.fSimpleSqlValue("N")
    vWhere = gUtilSQL.fSQLValueDatesBetween(vWhere, insComprasVista.GetViewNameComprasConCambioAlDia() & " .Fecha", valFechaInicial, valFechaFinal)
    vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
    vWhere = gUtilSQL.getWhereSQL(vWhere)
    If Not valMostrarComprasAnuladas Then
     vWhere = vWhere & " AND " & insComprasVista.GetViewNameComprasConCambioAlDia & ".StatusCompra = " & gUtilSQL.fSimpleSqlValue(enum_StatusCompra.eSCp_VIGENTE)
    End If
   
    If valMuestraDetalle Then
     SQL = " SELECT "
    
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Moneda,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Fecha,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Numero,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".NombreProveedor,"
   
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo,"
     SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial", True) & " AS Serial, "
     SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo =  '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo", True) & " AS Rollo, "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".cantidadrecibida,"
   
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglones AS totalCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".CostoUnitario,"
     If (valCambioOriginal) Then
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglonesCambio  AS CompraTotalAlcambio,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".CambioAbolivares AS cambio,"
     Else
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglonesCambio  AS CompraTotalAlcambio,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".cambioalafecha AS cambio,"
     End If
     If (valCambioOriginal) Then
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioAlDia() & ".CostoUnitario) AS costoAlcambio, "
     Else
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioAlDia() & ".cambioalafecha" & " * " & insComprasVista.GetViewNameComprasConCambioAlDia() & ".CostoUnitario) AS costoAlcambio, "
     End If
     SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioAlDia & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
     SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
    
     SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioAlDia & " ON "
    
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".ConsecutivoCompania AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".CodigoArticulo AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".Serial AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".Rollo  "
    
     SQL = SQL & vWhere
     SQL = SQL & " Order By "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".StatusCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Moneda,"
     SQL = SQL & " fecha,"
     SQL = SQL & " numero"
    Else
     SQL = " SELECT "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Moneda,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Fecha,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Numero,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".NombreProveedor,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS CodigoDelArticulo,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Serial,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Rollo,"
     SQL = SQL & "0 AS cantidadrecibida,"
     SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglones) AS totalCompra,"
     SQL = SQL & "0 AS CostoUnitario,"
     If (valCambioOriginal) Then
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglonesCambio)  AS CompraTotalAlcambio,"
      SQL = SQL & "MAX(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".CambioAbolivares) AS cambio,"
     Else
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglonesCambio)  AS CompraTotalAlcambio,"
      SQL = SQL & "MAX(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".cambioalafecha) AS cambio,"
     End If
     SQL = SQL & "0 AS costoAlcambio, "
     SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioAlDia & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
     SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
     SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioAlDia & " ON "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".ConsecutivoCompania AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".CodigoArticulo AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".Serial AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".Rollo  "
     SQL = SQL & vWhere
     SQL = SQL & " GROUP BY  "
     SQL = SQL & " IGV_ComprasConCambioAlDia.Moneda,IGV_ComprasConCambioAlDia.Fecha, "
     SQL = SQL & " IGV_ComprasConCambioAlDia.Numero, IGV_ComprasConCambioAlDia.NombreProveedor, "
     SQL = SQL & " StatusCompra"
     SQL = SQL & " Order By "
     SQL = SQL & " StatusCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Moneda,"
     SQL = SQL & " fecha,"
     SQL = SQL & " numero"
    End If
   Set insArticuloInvVista = Nothing
   Set insComprasVista = Nothing
End If
  
  fConstruirSQLComprasEntreFechasTotales = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLComprasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLComprasEntreFechasSoloMonedaOriginal(ByVal valConsecutivoCompania As Long, _
                                                                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMostrarComprasAnuladas As Boolean, ByVal valMuestraDetalle As Boolean, ByVal valTotales As Boolean) As String
Dim SQL As String
Dim insArticuloInvVista As clsArticuloInvVista
Dim insComprasVista As clsComprasVista
Dim vWhere As String

   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   Set insComprasVista = New clsComprasVista
   vWhere = ""

   vWhere = insComprasVista.GetViewNameComprasConCambioOriginal() & ".EsUnaOrdenDeCompra = " & gUtilSQL.fSimpleSqlValue("N")
   vWhere = gUtilSQL.fSQLValueDatesBetween(vWhere, insComprasVista.GetViewNameComprasConCambioOriginal() & " .Fecha", valFechaInicial, valFechaFinal)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)

   If Not valMostrarComprasAnuladas Then
      vWhere = vWhere & " AND " & insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra = " & gUtilSQL.fSimpleSqlValue(enum_StatusCompra.eSCp_VIGENTE)
   End If

   If valMuestraDetalle Then
      SQL = " SELECT "
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Fecha,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Numero,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".NombreProveedor,"
      SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo,"
      SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial", True) & " AS Serial, "
      SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo =  '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo", True) & " AS Rollo, "
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".cantidadrecibida,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglones AS totalCompra,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".CostoUnitario,"
      
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioOriginal() & ".cantidadrecibida" & " * " & insComprasVista.GetViewNameComprasConCambioOriginal() & ".CostoUnitario)  AS CompraTotalAlcambio,"
      
      SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
      SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
      
      SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioOriginal & " ON "
      
      SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".ConsecutivoCompania AND "
      SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CodigoArticulo AND "
      SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Serial AND "
      SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Rollo  "
      
      SQL = SQL & vWhere
      SQL = SQL & " Order By "
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
      SQL = SQL & " fecha,"
      SQL = SQL & " numero"
   Else
      SQL = " SELECT "
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Fecha,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Numero,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".NombreProveedor,"
      SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS CodigoDelArticulo,"
      SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Serial,"
      SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Rollo,"
      SQL = SQL & "0 AS cantidadrecibida,"
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglones) AS totalCompra,"
      SQL = SQL & "0 AS CostoUnitario,"
      
      SQL = SQL & "0 AS costoAlcambio, "
      SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
      SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
      SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioOriginal & " ON "
      SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".ConsecutivoCompania AND "
      SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CodigoArticulo AND "
      SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Serial AND "
      SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Rollo  "
      SQL = SQL & vWhere
      SQL = SQL & " GROUP BY  "
      SQL = SQL & " IGV_ComprasConCambioOriginal.Moneda,IGV_ComprasConCambioOriginal.Fecha, "
      SQL = SQL & " IGV_ComprasConCambioOriginal.Numero, IGV_ComprasConCambioOriginal.NombreProveedor, "
      SQL = SQL & " StatusCompra"
      SQL = SQL & " Order By "
      SQL = SQL & " StatusCompra,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
      SQL = SQL & " fecha,"
      SQL = SQL & " numero"
   End If

   Set insArticuloInvVista = Nothing
   Set insComprasVista = Nothing

   fConstruirSQLComprasEntreFechasSoloMonedaOriginal = SQL

h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
"fConstruirSQLComprasEntreFechasSoloMonedaOriginal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Function

Public Function fConstruirSQLComprasEntreFechasBolivaresMonedaOriginal(ByVal valConsecutivoCompania As Long, _
                        ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCambioOriginal As Boolean, ByVal valMostrarComprasAnuladas As Boolean, ByVal valMuestraDetalle As Boolean, ByVal valTotales As Boolean) As String
   Dim SQL As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim insComprasVista As clsComprasVista
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   Set insComprasVista = New clsComprasVista
   vWhere = ""
   
If (valCambioOriginal) Then
    vWhere = insComprasVista.GetViewNameComprasConCambioOriginal() & ".EsUnaOrdenDeCompra = " & gUtilSQL.fSimpleSqlValue("N")
    vWhere = gUtilSQL.fSQLValueDatesBetween(vWhere, insComprasVista.GetViewNameComprasConCambioOriginal() & " .Fecha", valFechaInicial, valFechaFinal)
    vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
    vWhere = gUtilSQL.getWhereSQL(vWhere)
    
    If Not valMostrarComprasAnuladas Then
     vWhere = vWhere & " AND " & insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra = " & gUtilSQL.fSimpleSqlValue(enum_StatusCompra.eSCp_VIGENTE)
    End If
    
    If valMuestraDetalle Then
     SQL = " SELECT "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Fecha,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Numero,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".NombreProveedor,"
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo,"
     SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial", True) & " AS Serial, "
     SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo =  '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo", True) & " AS Rollo, "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".cantidadrecibida,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglones AS totalCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".CostoUnitario,"
    
     If (valCambioOriginal) Then
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglonesCambio  AS CompraTotalAlcambio,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".CambioAbolivares AS cambio,"
     Else
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglonesCambio  AS CompraTotalAlcambio,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".cambioalafecha AS cambio,"
     End If
    
     If (valCambioOriginal) Then
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioOriginal() & ".CostoUnitario) AS costoAlcambio, "
     Else
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioOriginal() & ".cambioalafecha" & " * " & insComprasVista.GetViewNameComprasConCambioOriginal() & ".CostoUnitario) AS costoAlcambio, "
     End If
     SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
     SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
    
     SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioOriginal & " ON "
    
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".ConsecutivoCompania AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CodigoArticulo AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Serial AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Rollo  "
    
     SQL = SQL & vWhere
     SQL = SQL & " Order By "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
     SQL = SQL & " fecha,"
     SQL = SQL & " numero"
    Else
     SQL = " SELECT "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Fecha,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Numero,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".NombreProveedor,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS CodigoDelArticulo,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Serial,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Rollo,"
     SQL = SQL & "0 AS cantidadrecibida,"
     SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglones) AS totalCompra,"
     SQL = SQL & "0 AS CostoUnitario,"
     If (valCambioOriginal) Then
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglonesCambio)  AS CompraTotalAlcambio,"
      SQL = SQL & "MAX(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".CambioAbolivares) AS cambio,"
     Else
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglonesCambio)  AS CompraTotalAlcambio,"
      SQL = SQL & "MAX(" & insComprasVista.GetViewNameComprasConCambioOriginal & ".cambioalafecha) AS cambio,"
     End If
     SQL = SQL & "0 AS costoAlcambio, "
     SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
     SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
     SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioOriginal & " ON "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".ConsecutivoCompania AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CodigoArticulo AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Serial AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Rollo  "
     SQL = SQL & vWhere
     SQL = SQL & " GROUP BY  "
     SQL = SQL & " IGV_ComprasConCambioOriginal.Moneda,IGV_ComprasConCambioOriginal.Fecha, "
     SQL = SQL & " IGV_ComprasConCambioOriginal.Numero, IGV_ComprasConCambioOriginal.NombreProveedor, "
     SQL = SQL & " StatusCompra"
     SQL = SQL & " Order By "
     SQL = SQL & " StatusCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
     SQL = SQL & " fecha,"
     SQL = SQL & " numero"
    End If
   
   Set insArticuloInvVista = Nothing
   Set insComprasVista = Nothing
Else
    vWhere = insComprasVista.GetViewNameComprasConCambioAlDia() & ".EsUnaOrdenDeCompra = " & gUtilSQL.fSimpleSqlValue("N")
    vWhere = gUtilSQL.fSQLValueDatesBetween(vWhere, insComprasVista.GetViewNameComprasConCambioAlDia() & " .Fecha", valFechaInicial, valFechaFinal)
    vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
    vWhere = gUtilSQL.getWhereSQL(vWhere)
    If Not valMostrarComprasAnuladas Then
     vWhere = vWhere & " AND " & insComprasVista.GetViewNameComprasConCambioAlDia & ".StatusCompra = " & gUtilSQL.fSimpleSqlValue(enum_StatusCompra.eSCp_VIGENTE)
    End If
   
    If valMuestraDetalle Then
     SQL = " SELECT "
    
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Moneda,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Fecha,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Numero,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".NombreProveedor,"
   
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo,"
     SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial", True) & " AS Serial, "
     SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo =  '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo", True) & " AS Rollo, "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".cantidadrecibida,"
   
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglones AS totalCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".CostoUnitario,"
     If (valCambioOriginal) Then
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglonesCambio  AS CompraTotalAlcambio,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".CambioAbolivares AS cambio,"
     Else
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglonesCambio  AS CompraTotalAlcambio,"
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".cambioalafecha AS cambio,"
     End If
     If (valCambioOriginal) Then
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioAlDia() & ".CostoUnitario) AS costoAlcambio, "
     Else
      SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioAlDia() & ".cambioalafecha" & " * " & insComprasVista.GetViewNameComprasConCambioAlDia() & ".CostoUnitario) AS costoAlcambio, "
     End If
     SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioAlDia & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
     SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
    
     SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioAlDia & " ON "
    
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".ConsecutivoCompania AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".CodigoArticulo AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".Serial AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".Rollo  "
    
     SQL = SQL & vWhere
     SQL = SQL & " Order By "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".StatusCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Moneda,"
     SQL = SQL & " fecha,"
     SQL = SQL & " numero"
    Else
     SQL = " SELECT "
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Moneda,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Fecha,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Numero,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".NombreProveedor,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS CodigoDelArticulo,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Serial,"
     SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS Rollo,"
     SQL = SQL & "0 AS cantidadrecibida,"
     SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglones) AS totalCompra,"
     SQL = SQL & "0 AS CostoUnitario,"
     If (valCambioOriginal) Then
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglonesCambio)  AS CompraTotalAlcambio,"
      SQL = SQL & "MAX(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".CambioAbolivares) AS cambio,"
     Else
      SQL = SQL & "SUM(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".TotalRenglonesCambio)  AS CompraTotalAlcambio,"
      SQL = SQL & "MAX(" & insComprasVista.GetViewNameComprasConCambioAlDia & ".cambioalafecha) AS cambio,"
     End If
     SQL = SQL & "0 AS costoAlcambio, "
     SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioAlDia & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
     SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
     SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioAlDia & " ON "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".ConsecutivoCompania AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".CodigoArticulo AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".Serial AND "
     SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioAlDia & ".Rollo  "
     SQL = SQL & vWhere
     SQL = SQL & " GROUP BY  "
     SQL = SQL & " IGV_ComprasConCambioAlDia.Moneda,IGV_ComprasConCambioAlDia.Fecha, "
     SQL = SQL & " IGV_ComprasConCambioAlDia.Numero, IGV_ComprasConCambioAlDia.NombreProveedor, "
     SQL = SQL & " StatusCompra"
     SQL = SQL & " Order By "
     SQL = SQL & " StatusCompra,"
     SQL = SQL & insComprasVista.GetViewNameComprasConCambioAlDia & ".Moneda,"
     SQL = SQL & " fecha,"
     SQL = SQL & " numero"
    End If
   Set insArticuloInvVista = Nothing
   Set insComprasVista = Nothing
End If
  
  fConstruirSQLComprasEntreFechasBolivaresMonedaOriginal = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLComprasEntreFechasBolivaresMonedaOriginal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Function

Public Function fConstruirSQLCostoDeComprasEntreFechas(ByVal valConsecutivoCompania As Long, _
                        ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valLineasDeProductoCantidadAImprimir As String, _
                        ByVal valLineaDeProducto As String, ByVal valCodigoProducto As String) As String
   Dim SQL As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim insComprasVista As clsComprasVista
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   Set insComprasVista = New clsComprasVista
     
    vWhere = insComprasVista.GetViewNameComprasConCambioOriginal() & ".EsUnaOrdenDeCompra = " & gUtilSQL.fSimpleSqlValue("N")
   
    If (valLineasDeProductoCantidadAImprimir = enumReporteCostoDeComprasToString(eRC_UNALINEADEPRODUCTO)) Then
       vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto", valLineaDeProducto, False)
    End If
    If (valLineasDeProductoCantidadAImprimir = enumReporteCostoDeComprasToString(eRC_UNARTICULO)) Then
       vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo", valCodigoProducto, False)
    End If
   
 
   vWhere = gUtilSQL.fSQLValueDatesBetween(vWhere, insComprasVista.GetViewNameComprasConCambioOriginal() & " .Fecha", valFechaInicial, valFechaFinal)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
 
    SQL = " SELECT "
    SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto AS LineaDeProducto,"
    SQL = SQL & " MAX( " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Fecha) AS FechaUltCompra ,"
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo AS CodigoArticulo, "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion AS Descripcion, "
    SQL = SQL & " SUM( " & insComprasVista.GetViewNameComprasConCambioOriginal & ".cantidadrecibida) AS CantArtComp, "
    SQL = SQL & " COUNT(NUMERO) AS VecesComprados, "
    SQL = SQL & " MAX( " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CostoUnitario ) AS CostoMaximo,"
    SQL = SQL & " MIN( " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CostoUnitario ) AS CostoMinimo,"
    SQL = SQL & " SUM( " & insComprasVista.GetViewNameComprasConCambioOriginal & ".TotalRenglones ) AS TotalCompXArt,"
    SQL = SQL & " SUM( " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CostoUnitario ) / " & " SUM( " & insComprasVista.GetViewNameComprasConCambioOriginal & ".cantidadrecibida) AS PromPonDeComp"
 
    SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
    SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioOriginal & " ON "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".ConsecutivoCompania AND "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CodigoArticulo AND "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Serial AND "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Rollo  "
    SQL = SQL & vWhere
    SQL = SQL & " Group By "
    SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto,"
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion"

  
    SQL = SQL & " Order By "
    SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda"


   Set insArticuloInvVista = Nothing
   Set insComprasVista = Nothing
     fConstruirSQLCostoDeComprasEntreFechas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
    "fConstruirSQLCostoDeComprasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLMargenSobreCostoPromedioDeComp(ByVal valConsecutivoCompania As Long, _
                        ByVal valNivelDePrecio As enum_NivelDePrecio, _
                        ByVal valLineasDeProductoCantidadAImprimir As String, _
                        ByVal valLineaDeProducto As String, ByVal valCodigoProducto As String) As String
  
 Dim insArticulo As clsArticuloSQL
 Dim SQL As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim insComprasVista As clsComprasVista
   Dim vWhere As String
   Dim vPrecioVenta As String
   Dim vCondicionPrecioVenta As String
   Dim vMargen As String
   Dim vPromComp As String
    On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   Set insComprasVista = New clsComprasVista
   Set insArticulo = New clsArticuloSQL
    
    vPrecioVenta = insArticulo.fSqlNivelDePrecio(valNivelDePrecio, True)
    vPromComp = "( SUM( " & insComprasVista.GetViewNameComprasConCambioOriginal() & ".CambioAbolivares * " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CostoUnitario ) / " & " SUM( " & insComprasVista.GetViewNameComprasConCambioOriginal & ".cantidadrecibida))"
    vMargen = insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & "." & vPrecioVenta & "-" & vPromComp
    vCondicionPrecioVenta = gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & "." & vPrecioVenta & " = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(-1), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & "." & vPrecioVenta, True)
    
    vWhere = insComprasVista.GetViewNameComprasConCambioOriginal() & ".EsUnaOrdenDeCompra = " & gUtilSQL.fSimpleSqlValue("N")
    If (valLineasDeProductoCantidadAImprimir = enumReporteCostoDeComprasToString(eRC_UNALINEADEPRODUCTO)) Then
       vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto", valLineaDeProducto, False)
    End If
    If (valLineasDeProductoCantidadAImprimir = enumReporteCostoDeComprasToString(eRC_UNARTICULO)) Then
       vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo", valCodigoProducto, False)
    End If
   
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
 
    SQL = " SELECT "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto AS LineaDeProducto,"
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo AS Codigo, "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion AS Descripcion, "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & "." & vPrecioVenta & " AS PV, "
    SQL = SQL & vPromComp & " AS PromDeComp, "
    SQL = SQL & vMargen & " AS Margen, "
    SQL = SQL & " ( " & vMargen & "/" & vCondicionPrecioVenta & ") * " & gUtilSQL.fNumToStrSQL(100) & " AS PorcentajeMargen "
    
    SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
    SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioOriginal & " ON "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".ConsecutivoCompania AND "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CodigoArticulo AND "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Serial AND "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Rollo  "
    SQL = SQL & vWhere
    SQL = SQL & " Group By "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto,"
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion,"
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & "." & vPrecioVenta
  
    SQL = SQL & " Order By "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto"
   Set insArticulo = Nothing
   Set insArticuloInvVista = Nothing
   Set insComprasVista = Nothing
h_EXIT:   On Error GoTo 0
   fConstruirSQLMargenSobreCostoPromedioDeComp = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLMargenSobreCostoPromedioDeComp", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fContruirSQLHistoricoCompras(ByVal valConsecutivoCompania As Long, _
                  ByVal valLineasDeProductoCantidadAImprimir As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                  ByVal valLineaDeProducto As String, ByVal valCodigoProducto As String, ByVal valCambioOriginal As Boolean, ByVal valMostrarComprasAnuladas As Boolean) As String
   Dim SQL As String
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim insComprasVista As clsComprasVista
   Dim vDescripcionTallaColor As String
   Dim vWhere As String
   On Error GoTo h_ERROR
   Set insArticuloInvVista = New clsArticuloInvVista
   Set insComprasVista = New clsComprasVista
   vWhere = insComprasVista.GetViewNameComprasConCambioOriginal() & ".EsUnaOrdenDeCompra = " & gUtilSQL.fSimpleSqlValue("N")
   If Not valMostrarComprasAnuladas Then
      vWhere = vWhere & " AND " & insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra = " & gUtilSQL.fSimpleSqlValue(enum_StatusCompra.eSCp_VIGENTE)
   End If
   vDescripcionTallaColor = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionColor"), gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionColor", True)
   vDescripcionTallaColor = vDescripcionTallaColor & " + " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionTalla"), gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".DescripcionTalla", True)
    
    If (valLineasDeProductoCantidadAImprimir = enumReporteCostoDeComprasToString(eRC_UNALINEADEPRODUCTO)) Then
       vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto", valLineaDeProducto, False)
    End If
    If (valLineasDeProductoCantidadAImprimir = enumReporteCostoDeComprasToString(eRC_UNARTICULO)) Then
       vWhere = gUtilSQL.fSQLValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo", valCodigoProducto, False)
    End If
   
   vWhere = gUtilSQL.fSQLValueDatesBetween(vWhere, insComprasVista.GetViewNameComprasConCambioOriginal() & " .Fecha", valFechaInicial, valFechaFinal)
   vWhere = gUtilSQL.fSQLNumberValueWithAnd(vWhere, insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania", valConsecutivoCompania)
   vWhere = gUtilSQL.getWhereSQL(vWhere)
    
    SQL = " SELECT "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto,"
    SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Fecha,"
    SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Numero,"
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo,"
    SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial", True) & " AS Serial, "
    SQL = SQL & gUtilSQL.getIIF(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo =  '0' ", gUtilSQL.fSimpleSqlValue(""), insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo", True) & " AS Rollo, "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion + " & vDescripcionTallaColor & " AS Descripcion  , "
    
    SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".CodigoAlmacen,"
    SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".NombreProveedor,"
    SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".cantidadrecibida,"
   
    SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".Moneda,"
    SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".CostoUnitario,"
     If (valCambioOriginal) Then
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".CambioAbolivares AS cambio,"
      Else
      SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".cambioalafecha AS cambio,"
    
    End If
    If (valCambioOriginal) Then
          SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioOriginal() & ".CambioAbolivares * " & insComprasVista.GetViewNameComprasConCambioOriginal() & ".CostoUnitario) AS costoAlcambio,"
     Else
          SQL = SQL & "(" & insComprasVista.GetViewNameComprasConCambioOriginal() & ".cambioalafecha" & " * " & insComprasVista.GetViewNameComprasConCambioOriginal() & ".CostoUnitario) AS costoAlcambio,"
     End If
    SQL = SQL & gUtilSQL.getIIF(insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCompra.eSCp_VIGENTE), gUtilSQL.fSimpleSqlValue("VIGENTES"), gUtilSQL.fSimpleSqlValue("ANULADAS")) & " AS StatusCompra "
    SQL = SQL & " FROM " & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
    SQL = SQL & "  INNER JOIN  " & insComprasVista.GetViewNameComprasConCambioOriginal & " ON "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".ConsecutivoCompania AND "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".CodigoArticulo AND "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Serial AND "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insComprasVista.GetViewNameComprasConCambioOriginal & ".Rollo  "
    SQL = SQL & " LEFT  JOIN    " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & " ON "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".CodigoArticulo AND "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".Serial AND "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".Rollo AND "
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores & ".ConsecutivoCompania "
    SQL = SQL & vWhere
    SQL = SQL & " Order By "
    SQL = SQL & insComprasVista.GetViewNameComprasConCambioOriginal & ".StatusCompra,"
    SQL = SQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".LineaDeProducto,"
    SQL = SQL & " fecha,"
    SQL = SQL & " numero"
   
   Set insArticuloInvVista = Nothing
   Set insComprasVista = Nothing
   
   fContruirSQLHistoricoCompras = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fContruirSQLHistoricoCompras", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLReporteCompra(ByVal valOrdenCompra As Boolean, ByVal valConsecutivoCompania As Long, ByVal valNumero As String) As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim SQL As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQL = "SELECT "
   SQL = SQL & "renglonCompra.ConsecutivoRenglon, compra.numero, compra.fecha, compra.CodigoAlmacen, compra.CodigoProveedor, compra.TotalCompra, compra.Comentarios, "
   SQL = SQL & "renglonCompra.CodigoArticulo, "
   SQL = SQL & gUtilSQL.getIIF("EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(True), "renglonCompra.Cantidad", "renglonCompra.CantidadRecibida") & " as Cantidad"
   SQL = SQL & " , RenglonCompra.CostoUnitario, "
   SQL = SQL & "ArticuloInventario.Descripcion, ( " & gUtilSQL.getIIF("EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(True), "renglonCompra.Cantidad", "renglonCompra.CantidadRecibida") & " * RenglonCompra.CostoUnitario) AS TotalRenglon, "
   SQL = SQL & "articuloinventario.UnidadDeVenta AS unidad, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("compra.StatusCompra", enum_StatusCompra.eSCp_VIGENTE, gEnumProyecto.fEnumStatusComprasToStringInArray(True), "") & " AS Status, "
   SQL = SQL & "RenglonCompraXSerial.Serial, RenglonCompraXSerial.Rollo, "
   SQL = SQL & gUtilSQL.getIIF("articuloInventario.TipoArticuloInv = " & gUtilSQL.fSimpleSqlValue(enum_TipoArticuloInv.eTAI_Simple) & " OR " & "articuloInventario.TipoArticuloInv = " & gUtilSQL.fSimpleSqlValue(enum_TipoArticuloInv.eTAI_UsaTalla_Color), gUtilSQL.fSimpleSqlValue(0), gUtilSQL.getIIF("articuloInventario.TipoArticuloInv = " & gUtilSQL.fSimpleSqlValue(enum_TipoArticuloInv.eTAI_UsaSerial), gUtilSQL.fSimpleSqlValue(1), gUtilSQL.fSimpleSqlValue(2), True), True) & " AS OpcionParaMostrar, "
   SQL = SQL & "compra.OtrosGastos, "
   SQL = SQL & "compra.Moneda"
   SQL = SQL & " FROM Compra INNER JOIN "
   SQL = SQL & "(RenglonCompra INNER JOIN articuloInventario "
   SQL = SQL & " ON RenglonCompra.CodigoArticulo = articuloInventario.Codigo "
   SQL = SQL & " AND RenglonCompra.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) "
   SQL = SQL & " ON Compra.NumeroSecuencial = RenglonCompra.NumeroSecuencialCompra "
   SQL = SQL & " AND Compra.ConsecutivoCompania = RenglonCompra.ConsecutivoCompania"
   SQL = SQL & " AND Compra.NumeroSecuencial = RenglonCompra.NumeroSecuencialCompra LEFT JOIN"
   SQL = SQL & " RenglonCompraXSerial ON RenglonCompra.ConsecutivoCompania = RenglonCompraXSerial.ConsecutivoCompania "
   SQL = SQL & " AND RenglonCompra.NumeroSecuencialCompra = RenglonCompraXSerial.NumeroSecuencialCompra "
   SQL = SQL & " AND RenglonCompra.CodigoArticulo = RenglonCompraXSerial.CodigoArticulo "
   SQL = SQL & " WHERE compra.numero = " & valNumero
   SQL = SQL & " AND Compra.NumeroSecuencial = RenglonCompra.NumeroSecuencialCompra"
   SQL = SQL & " AND Compra.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT    renglonCompra.consecutivoRenglon, compra.numero, compra.Fecha, compra.CodigoAlmacen, compra.CodigoProveedor, compra.TotalCompra, "
   If valOrdenCompra Then
      SQL = SQL & " compra.Comentarios, renglonCompra.CodigoArticulo, renglonCompra.Cantidad, renglonCompra.CostoUnitario,"
      SQL = SQL & " (articuloInventario.Descripcion" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("DescripcionColor"), gUtilSQL.fSimpleSqlValue(""), "DescripcionColor") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("DescripcionTalla"), gUtilSQL.fSimpleSqlValue(""), "DescripcionTalla") & ") AS Descripcion,"
      SQL = SQL & " renglonCompra.Cantidad * renglonCompra.CostoUnitario AS TotalRenglon, "
   Else
      SQL = SQL & " compra.Comentarios, renglonCompra.CodigoArticulo, renglonCompra.CantidadRecibida, renglonCompra.CostoUnitario,"
      SQL = SQL & " (articuloInventario.Descripcion" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("DescripcionColor"), gUtilSQL.fSimpleSqlValue(""), "DescripcionColor") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("DescripcionTalla"), gUtilSQL.fSimpleSqlValue(""), "DescripcionTalla") & ") AS Descripcion,"
      SQL = SQL & " renglonCompra.CantidadRecibida * renglonCompra.CostoUnitario AS TotalRenglon, "
   End If
   SQL = SQL & "articuloinventario.UnidadDeVenta AS unidad, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("StatusCompra", enum_StatusCompra.eSCp_VIGENTE, gEnumProyecto.fEnumStatusComprasToStringInArray(True), "") & " AS Status, "
   SQL = SQL & " RenglonCompraXSerial.Serial, RenglonCompraXSerial.Rollo, "
   SQL = SQL & gUtilSQL.getIIF("articuloInventario.TipoArticuloInv = " & gUtilSQL.fSimpleSqlValue(enum_TipoArticuloInv.eTAI_Simple) & " OR " & "articuloInventario.TipoArticuloInv = " & gUtilSQL.fSimpleSqlValue(enum_TipoArticuloInv.eTAI_UsaTalla_Color), gUtilSQL.fSimpleSqlValue(0), gUtilSQL.getIIF("articuloInventario.TipoArticuloInv = " & gUtilSQL.fSimpleSqlValue(enum_TipoArticuloInv.eTAI_UsaSerial), gUtilSQL.fSimpleSqlValue(1), gUtilSQL.fSimpleSqlValue(2), True), True) & " AS OpcionParaMostrar, "
   SQL = SQL & "compra.OtrosGastos, "
   SQL = SQL & "compra.Moneda"
   SQL = SQL & " FROM renglonCompra INNER JOIN"
   SQL = SQL & " compra ON renglonCompra.NumeroSecuencialCompra = compra.NumeroSecuencial "
   SQL = SQL & " AND renglonCompra.ConsecutivoCompania = compra.ConsecutivoCompania "
   SQL = SQL & " AND renglonCompra.NumeroSecuencialCompra = compra.NumeroSecuencial "
   SQL = SQL & " AND renglonCompra.NumeroSecuencialCompra = compra.NumeroSecuencial INNER JOIN"
   SQL = SQL & " ExistenciaPorGrupo ON renglonCompra.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania "
   SQL = SQL & " AND (renglonCompra.CodigoArticulo = (ExistenciaPorGrupo.CodigoArticulo + codigoColor + CodigoTalla)) INNER JOIN"
   SQL = SQL & " articuloInventario ON ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania "
   SQL = SQL & " AND ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo LEFT OUTER JOIN"
   SQL = SQL & " Talla ON ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla "
   SQL = SQL & " AND ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania LEFT OUTER JOIN"
   SQL = SQL & " Color ON ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania "
   SQL = SQL & " AND ExistenciaPorGrupo.CodigoColor = Color.CodigoColor LEFT OUTER JOIN "
   SQL = SQL & " RenglonCompraXSerial ON renglonCompra.ConsecutivoCompania = RenglonCompraXSerial.ConsecutivoCompania  "
   SQL = SQL & " AND renglonCompra.NumeroSecuencialCompra = RenglonCompraXSerial.NumeroSecuencialCompra  "
   SQL = SQL & " AND renglonCompra.CodigoArticulo = RenglonCompraXSerial.CodigoArticulo "
   SQL = SQL & " WHERE Compra.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND compra.numero = " & valNumero
   SQL = SQL & " AND Compra.NumeroSecuencial = RenglonCompra.NumeroSecuencialCompra"
   SQL = SQL & " AND Compra.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " ORDER BY renglonCompra.ConsecutivoRenglon"
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLReporteCompra = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLReporteCompra", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDetailRenglonCompraDesglosado(ByVal valNumeroSecuencial As String, ByVal gProyCompaniaActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "CodigoArticulo, "
   SQL = SQL & "SUM(Cantidad) AS CantidadFinal "
   SQL = SQL & " FROM renglonCompra"
   SQL = SQL & " WHERE ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   SQL = SQL & " AND NumeroSecuencialCompra ='" & valNumeroSecuencial & "'"
   SQL = SQL & " GROUP BY CodigoArticulo"
   SQL = SQL & " ORDER BY CodigoArticulo"
   fSQLDetailRenglonCompraDesglosado = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDetailRenglonCompraDesglosado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fContruirSQLOrdenDeCompras(ByVal valConsecutivoCompania As Long, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                  ByVal valStatusOrdenDeCompra As Integer) As String
   Dim SQL As String
   Dim vWhere As String
   On Error GoTo h_ERROR
    SQL = SQL & "SELECT DISTINCT Compra.Moneda,  Compra.Numero,Compra.Fecha,Proveedor.NombreProveedor,Compra.TotalCompra,Compra.CambioABolivares,"
    SQL = SQL & " Compra.TotalCompra*Compra.CambioABolivares as 'Total"
    SQL = SQL & " al Cambio',RenglonCompra.CodigoArticulo,IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion,RenglonCompra.Cantidad,RenglonCompra.CantidadRecibida,RenglonCompra.CostoUnitario"
    SQL = SQL & " FROM RenglonCompra INNER JOIN Compra ON Compra.ConsecutivoCompania"
    SQL = SQL & " = RenglonCompra.ConsecutivoCompania AND  Compra.NumeroSecuencial"
    SQL = SQL & " =  RenglonCompra.NumeroSecuencialCompra INNER JOIN"
    SQL = SQL & "  Proveedor ON Proveedor.ConsecutivoCompania = Compra.ConsecutivoCompania"
    SQL = SQL & " AND Proveedor.CodigoProveedor = Compra.CodigoProveedor"
    SQL = SQL & " INNER JOIN IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
    SQL = SQL & " ON IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania="
    SQL = SQL & " RenglonCompra. ConsecutivoCompania AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonCompra.CodigoArticulo "
    SQL = SQL & " WHERE   (Compra.EsUnaOrdenDeCompra"
    SQL = SQL & " = 'S')  AND (Compra.Fecha BETWEEN '" & valFechaInicial & "' AND"
    SQL = SQL & " '" & valFechaFinal & "')  "
    If valStatusOrdenDeCompra = 0 Then
    SQL = SQL & " AND ((select (sum(RenglonCompra.Cantidad)- sum(RenglonCompra.CantidadRecibida))from"
    SQL = SQL & " RenglonCompra where RenglonCompra.NumeroSecuencialCompra= Compra.NumeroSecuencial) = 0 )"
    ElseIf valStatusOrdenDeCompra = 1 Then
    SQL = SQL & " AND ((select (sum(RenglonCompra.Cantidad)- sum(RenglonCompra.CantidadRecibida))from"
    SQL = SQL & " RenglonCompra where RenglonCompra.NumeroSecuencialCompra= Compra.NumeroSecuencial) > 0 )"
    SQL = SQL & " AND ((select sum(RenglonCompra.CantidadRecibida)from"
    SQL = SQL & " RenglonCompra where RenglonCompra.NumeroSecuencialCompra= Compra.NumeroSecuencial) <> 0 )"
    ElseIf valStatusOrdenDeCompra = 2 Then
    SQL = SQL & " AND ((select sum(RenglonCompra.CantidadRecibida)from"
    SQL = SQL & " RenglonCompra where RenglonCompra.NumeroSecuencialCompra= Compra.NumeroSecuencial) = 0 )"
    End If
    SQL = SQL & " AND  (Compra.ConsecutivoCompania='" & valConsecutivoCompania & "') "
    SQL = SQL & " ORDER BY Compra.Moneda, Compra.fecha, Compra.Numero"
   fContruirSQLOrdenDeCompras = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fContruirSQLOrdenDeCompras", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlNivelDePrecioSinIvaAux(ByVal valNivelDePrecio As enum_NivelDePrecio) As String  ', ByVal valPreciosSinIva As Boolean) As String
   fSqlNivelDePrecioSinIvaAux = fSqlNivelDePrecioSinIva(valNivelDePrecio)
   'If (valPreciosSinIva) Then
      'fSqlNivelDePrecio = fSqlNivelDePrecioSinIva(valNivelDePrecio)
       'Else
      'fSqlNivelDePrecio = fSqlNivelDePrecioConIva(valNivelDePrecio)
      'End If
End Function

Public Function fSqlNivelDePrecioConIvaAux(ByVal valNivelDePrecio As enum_NivelDePrecio) As String  ', ByVal valPreciosSinIva As Boolean) As String
   fSqlNivelDePrecioConIvaAux = fSqlNivelDePrecioConIva(valNivelDePrecio)
   'If (valPreciosSinIva) Then
      'fSqlNivelDePrecio = fSqlNivelDePrecioSinIva(valNivelDePrecio)
       'Else
      'fSqlNivelDePrecio = fSqlNivelDePrecioConIva(valNivelDePrecio)
      'End If
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

'Public Function fSQLParaLaImpresionDeComprasEtiquetas(ByVal valConsecutivoCompania As Long, ByVal valNumeroCompra As String, _
'                                                      ByVal valNivelDePrecio As enum_NivelDePrecio, ByVal valPrecioSinIva As Boolean)
'   Dim insCompraVista As clsComprasVista
'   Dim SQL As String
'   Dim vWhere As String
'   Dim vPrecio As String
'   Dim vCreate As String
'   Set insCompraVista = New clsComprasVista
'
'   On Error GoTo h_ERROR

'   vCreate = "CREATE TABLE #TablaTemporal (Campo1 int) "
'   vCreate = vCreate & "DECLARE @max AS INT, @rc AS INT; "
'   vCreate = vCreate & "SET @max = 1000; "
'   vCreate = vCreate & "SET @rc = 1; "
'   vCreate = vCreate & "INSERT INTO #TablaTemporal(Campo1) VALUES(1); "
'   vCreate = vCreate & "WHILE @rc * 2 <= @max "
'   vCreate = vCreate & "BEGIN "
'   vCreate = vCreate & "INSERT INTO #TablaTemporal(Campo1) SELECT (Campo1) + @rc FROM #TablaTemporal; "
'   vCreate = vCreate & "SET @rc = @rc * 2; "
'   vCreate = vCreate & "END "
'   vCreate = vCreate & "INSERT INTO #TablaTemporal(Campo1) "
'   vCreate = vCreate & "SELECT Campo1 + @rc "
'   vCreate = vCreate & "FROM #TablaTemporal "
'   vCreate = vCreate & "WHERE Campo1 + @rc <= @max; "

'   SQL = ""
'   SQL = vCreate
'   SQL = SQL & "SELECT "
'   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Numero, "
'
'   vPrecio = insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & "." & fSqlNivelDePrecio(valNivelDePrecio, valPrecioSinIva)
'   SQL = SQL & vPrecio & " AS Precio, "
'
'   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".CodigoDelArticulo, "
'   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Descripcion, "
'   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".CantidadRecibida, "
'   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Serial, "
'   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Rollo,"
'   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".DescripcionTalla, "
'   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".DescripcionColor, "
'   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Fecha "
'
'   SQL = SQL & "FROM "
'   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ", "
'   SQL = SQL & "EnumBase "
'   'SQL = SQL & "#TablaTemporal "
'
'   vWhere = ""
'   vWhere = vWhere & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".CodigoDelArticulo "
'   vWhere = vWhere & "IN (SELECT "
'   vWhere = vWhere & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".CodigoDelArticulo "
'   vWhere = vWhere & "FROM "
'   vWhere = vWhere & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras()
'   vWhere = vWhere & ") AND EnumBase.IntValue+1 <= " '#TablaTemporal.Campo1  <= "
'   vWhere = vWhere & "(CASE WHEN "
'   vWhere = vWhere & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".CantidadRecibida = 1 "
'   vWhere = vWhere & "THEN "
'   vWhere = vWhere & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".CantidadRecibida - 1 "
'   vWhere = vWhere & "ELSE ("
'   vWhere = vWhere & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".CantidadRecibida"
'   vWhere = vWhere & ") "
'   vWhere = vWhere & "END) AND "
'   vWhere = vWhere & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Numero ='" & valNumeroCompra & "'"
'   vWhere = gUtilSQL.getWhereSQL(vWhere)
'
'   SQL = SQL & vWhere
'
'h_EXIT:   On Error GoTo 0
'   fSQLParaLaImpresionDeComprasEtiquetas = SQL
'   Exit Function
'h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'      "fSQLParaLaImpresionDeComprasEtiquetas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function

Public Function fSQLParaLaImpresionDeComprasEtiquetas(ByVal valConsecutivoCompania As Long, ByVal valNumeroCompra As String, _
                                                      ByVal valNivelDePrecio As enum_NivelDePrecio) ', ByVal valPrecioSinIva As Boolean)
   Dim insCompraVista As clsComprasVista
   Dim SQL As String
   Dim vWhere As String
   Dim vPrecioSinIva As String
   Dim vPrecioConIva As String
   Dim vGroupby As String
   Set insCompraVista = New clsComprasVista

   On Error GoTo h_ERROR

   vPrecioSinIva = insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & "." & fSqlNivelDePrecioSinIvaAux(valNivelDePrecio)
   vPrecioConIva = insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & "." & fSqlNivelDePrecioConIvaAux(valNivelDePrecio)

   vWhere = ""
   vWhere = insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Numero ='" & valNumeroCompra & "'"
   vWhere = vWhere & " AND "
   vWhere = vWhere & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Compania ='" & valConsecutivoCompania & "'"
   vWhere = gUtilSQL.getWhereSQL(vWhere)

   vGroupby = ""
   vGroupby = " GROUP BY "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Numero, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".PrecioSinIva, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".PrecioConIva, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".PrecioSinIva2, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".PrecioConIva2, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".PrecioSinIva3, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".PrecioConIva3, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".PrecioSinIva4, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".PrecioConIva4, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".CodigoDelArticulo, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Descripcion, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".CantidadRecibida, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Serial, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Rollo, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".DescripcionColor, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".DescripcionTalla, "
   vGroupby = vGroupby & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Fecha"

   SQL = "SELECT "
   SQL = SQL & " (" & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".CodigoDelArticulo"
   SQL = SQL & " +ISNULL (" & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Serial ,'') + ISNULL (" & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Rollo ,'') ) AS Concatenado, "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Numero, "
   SQL = SQL & vPrecioSinIva & " AS PrecioSinIva, "
   SQL = SQL & vPrecioConIva & " AS PrecioConIva, "
   SQL = SQL & "CASE WHEN " & vPrecioSinIva & " = '0'"
   SQL = SQL & " OR "
   SQL = SQL & vPrecioConIva & " = '0' THEN "
   SQL = SQL & "'0' "
   SQL = SQL & "ELSE "
   SQL = SQL & "(" & vPrecioConIva & " - " & vPrecioSinIva & ") "
   SQL = SQL & "END AS MontoIVA, "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".CodigoDelArticulo, "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Descripcion, "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".CantidadRecibida, "
   SQL = SQL & "CASE WHEN "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Serial IS NULL OR "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Serial = '0' THEN "
   SQL = SQL & "'Sin Serial' ELSE "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Serial "
   SQL = SQL & "END AS Serial, "
   SQL = SQL & "CASE WHEN "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Rollo IS NULL OR "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Rollo = '0' THEN "
   SQL = SQL & "'Sin Rollo' ELSE "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Rollo "
   SQL = SQL & "END AS Rollo, "
   SQL = SQL & "CASE WHEN "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".DescripcionTalla IS NULL THEN "
   SQL = SQL & "'Sin Talla' ELSE "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".DescripcionTalla "
   SQL = SQL & "END AS Talla, "
   SQL = SQL & "CASE WHEN "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".DescripcionColor IS NULL THEN "
   SQL = SQL & "'Sin Color' ELSE "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".DescripcionColor "
   SQL = SQL & "END AS Color, "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras() & ".Fecha "
   SQL = SQL & "FROM "
   SQL = SQL & insCompraVista.GetViewNameComprasImprimirEtiquetasPorCompras()
   SQL = SQL & vWhere
   SQL = SQL & vGroupby

h_EXIT:   On Error GoTo 0
   fSQLParaLaImpresionDeComprasEtiquetas = SQL
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLParaLaImpresionDeComprasEtiquetas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

