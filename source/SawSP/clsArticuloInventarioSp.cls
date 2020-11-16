VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsArticuloInventarioSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsArticuloInventarioSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsArticuloInventarioSp"
End Function

Public Function fCreateDropGrupoDeArticuloInventarioSp(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateFnExistencia(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnCompraUnaFecha(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnCostosUltimaCompra(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnCostoPromedioPonderado(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnArticuloInventarioMaxOrMinCodigo(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpoInventarioActualizarCostosyMargenes(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpArticuloInventarioActualizarCostos(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnMovimientosDeInventario(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnCostoPromedioMovimientosInv(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpoInventarioActualizarCostosyMargenesFormulaOpcional(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpArticuloInventarioActualizarCostosFormulaOpcional(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpArticuloInventarioActualizarCostosxUltimaCompra(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpInventarioActualizarCostosyMargenesxUltimaCompra(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnCompraUltimaFecha(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpInventarioLiberarReservas(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpInventarioLiberarReservas(valShowMessage)
      vResult = vResult And fDropFnCompraUltimaFecha(valShowMessage)
      vResult = vResult And fDropSpInventarioActualizarCostosyMargenesxUltimaCompra(valShowMessage)
      vResult = vResult And fDropSpArticuloInventarioActualizarCostosxUltimaCompra(valShowMessage)
      vResult = vResult And fDropSpoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra(valShowMessage)
      vResult = vResult And fDropSpArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra(valShowMessage)
      vResult = vResult And fDropSpArticuloInventarioActualizarCostosFormulaOpcional(valShowMessage)
      vResult = vResult And fDropSpoInventarioActualizarCostosyMargenesFormulaOpcional(valShowMessage)
      vResult = vResult And fDropFnCostoPromedioMovimientosInv(valShowMessage)
      vResult = vResult And fDropFnMovimientosDeInventario(valShowMessage)
      vResult = vResult And fDropSpArticuloInventarioActualizarCostos(valShowMessage)
      vResult = vResult And fDropSpoInventarioActualizarCostosyMargenes(valShowMessage)
      vResult = vResult And fDropFnArticuloInventarioMaxOrMinCodigo(valShowMessage)
      vResult = vResult And fDropFnCostoPromedioPonderado(valShowMessage)
      vResult = vResult And fDropFnCostosUltimaCompra(valShowMessage)
      vResult = vResult And fDropFnCompraUnaFecha(valShowMessage)
      vResult = vResult And fDropFnExistencia(valShowMessage)
     
     
   End If
  fCreateDropGrupoDeArticuloInventarioSp = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropGrupoDeActivosSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateFnExistencia(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameExistencia(), fFnParametersExistencia(), fFnScalarReturnDataTypeExistencia(), fStatementsFnExistencia(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnExistencia = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnExistencia", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnExistencia(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameExistencia(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnExistencia = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnExistencia", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameExistencia() As String
   GetFnNameExistencia = "Existencia"
End Function

Private Function fFnParametersExistencia() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & " @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " ," & vbCrLf
   vResult = vResult & " @Fecha " & gUtilSQL.getDateTypeForDB() & " ," & vbCrLf
   vResult = vResult & " @CodigoArticulo VARCHAR(30)," & vbCrLf
   vResult = vResult & " @PrioridadOperacion " & gUtilSQL.getNumericTypeForDB(9, 0) & " ," & vbCrLf
   vResult = vResult & " @NumeroDocumento VARCHAR(40)" & vbCrLf
   fFnParametersExistencia = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersExistencia", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeExistencia() As String
   fFnScalarReturnDataTypeExistencia = " MONEY "
End Function

Private Function fStatementsFnExistencia() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "DECLARE" & vbCrLf
   vResult = vResult & "@Return MONEY" & vbCrLf
   vResult = vResult & "     select @Return =" & vbCrLf
   vResult = vResult & "     sum (entrada) - sum (salidad)" & vbCrLf
   vResult = vResult & "     FROM IGV_ArticuloInvMovimiento" & vbCrLf
   vResult = vResult & "     where" & vbCrLf
   vResult = vResult & "     ((Fecha < @Fecha) or (Fecha = @Fecha and NumeroDocumento < @NumeroDocumento  and PrioridadOperacion = @PrioridadOperacion)or  (Fecha = @Fecha and PrioridadOperacion < @PrioridadOperacion))" & vbCrLf
   vResult = vResult & "      and" & vbCrLf
   vResult = vResult & "     Codigo =  @CodigoArticulo and" & vbCrLf
   vResult = vResult & "     ConsecutivoCompania  =  @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "     group by   ConsecutivoCompania, Codigo" & vbCrLf
   vResult = vResult & "RETURN @Return" & vbCrLf
   vResult = vResult & "END "
   
   fStatementsFnExistencia = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnExistencia", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateFnCompraUnaFecha(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCompraUnaFecha(), fFnParametersCompraUnaFecha(), fFnScalarReturnDataTypeCompraUnaFecha(), fStatementsFnCompraUnaFecha(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCompraUnaFecha = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCompraUnaFecha", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCompraUnaFecha(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCompraUnaFecha(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCompraUnaFecha = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCompraUnaFecha", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCompraUnaFecha() As String
   GetFnNameCompraUnaFecha = "CompraUnaFecha"
End Function

Private Function fFnParametersCompraUnaFecha() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & " @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " ," & vbCrLf
   vResult = vResult & " @Fecha " & gUtilSQL.getDateTypeForDB() & " ," & vbCrLf
   vResult = vResult & " @CodigoArticulo VARCHAR(30)" & vbCrLf
   fFnParametersCompraUnaFecha = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCompraUnaFecha", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeCompraUnaFecha() As String
   fFnScalarReturnDataTypeCompraUnaFecha = " DECIMAL(14,4) "
End Function

Private Function fStatementsFnCompraUnaFecha() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "DECLARE" & vbCrLf
   vResult = vResult & "@Return DECIMAL(14,4)" & vbCrLf
   vResult = vResult & "SELECT  TOP 1     @Return =" & vbCrLf
   vResult = vResult & "        IGV_ComprasTotalesSinOtrosGastos.CostoUnitario +" & vbCrLf
   vResult = vResult & "        IGV_ComprasOtroGastosPorItem.OtrosGastosPorItem" & vbCrLf
   vResult = vResult & "FROM      IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1" & vbCrLf
   vResult = vResult & "          INNER JOIN" & vbCrLf
   vResult = vResult & "          IGV_ComprasTotalesSinOtrosGastos ON" & vbCrLf
   vResult = vResult & "          IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania =  IGV_ComprasTotalesSinOtrosGastos.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "          IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = IGV_ComprasTotalesSinOtrosGastos.CodigoArticulo" & vbCrLf
   vResult = vResult & "          INNER JOIN" & vbCrLf
   vResult = vResult & "          IGV_ComprasOtroGastosPorItem ON" & vbCrLf
   vResult = vResult & "          IGV_ComprasTotalesSinOtrosGastos.ConsecutivoCompania =  IGV_ComprasOtroGastosPorItem.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "          IGV_ComprasTotalesSinOtrosGastos.NumeroSecuencial =  IGV_ComprasOtroGastosPorItem.NumeroSecuencial AND" & vbCrLf
   vResult = vResult & "          IGV_ComprasTotalesSinOtrosGastos.Numero =  IGV_ComprasOtroGastosPorItem.Numero" & vbCrLf
   vResult = vResult & "WHERE     IGV_ComprasTotalesSinOtrosGastos.EsUnaOrdenDeCompra = 'N' AND" & vbCrLf
   vResult = vResult & "          IGV_ComprasTotalesSinOtrosGastos.StatusCompra = '0'  AND" & vbCrLf
   vResult = vResult & "          IGV_ComprasTotalesSinOtrosGastos.Fecha <=  @Fecha AND" & vbCrLf
   vResult = vResult & "          IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo =  @CodigoArticulo    AND" & vbCrLf
   vResult = vResult & "          IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "Order by   IGV_ComprasTotalesSinOtrosGastos.Fecha  desc" & vbCrLf
   vResult = vResult & "RETURN @Return" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnCompraUnaFecha = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCompraUnaFecha", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateFnCostosUltimaCompra(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCostosUltimaCompra(), fFnParametersCostosUltimaCompra(), fFnScalarReturnDataTypeCostosUltimaCompra(), fStatementsFnCostosUltimaCompra(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCostosUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCostosUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCostosUltimaCompra(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCostosUltimaCompra(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCostosUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCostosUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCostosUltimaCompra() As String
   GetFnNameCostosUltimaCompra = "CostosUltimaCompra"
End Function

Private Function fFnParametersCostosUltimaCompra() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "@valCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "@valFechaI " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "@valFechaF " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "@valCodigoI  " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "@valCodigoF  " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   fFnParametersCostosUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCostosUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fFnScalarReturnDataTypeCostosUltimaCompra() As String
   Dim vResult As String
   vResult = vResult & " @datos TABLE " & vbCrLf
   vResult = vResult & "( -- Estructura de la tabla que devuelve la funcion." & vbCrLf
   vResult = vResult & "   consecutivocompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   Codigo " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "   CostoInicial money," & vbCrLf
   vResult = vResult & "   CostoFinal  money," & vbCrLf
   vResult = vResult & "   FechaInicial " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "   FechaFinal " & gUtilSQL.getDateTypeForDB() & " " & vbCrLf
   vResult = vResult & ")"
   fFnScalarReturnDataTypeCostosUltimaCompra = vResult
   End Function

Private Function fStatementsFnCostosUltimaCompra() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & " BEGIN" & vbCrLf
   vResult = vResult & "-- Variables necesarias para la lógica de la funcion." & vbCrLf
   vResult = vResult & " DECLARE @TablaArticulos Table(" & vbCrLf
   vResult = vResult & "  consecutivocompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   Codigo " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "   CostoInicial money," & vbCrLf
   vResult = vResult & "   CostoFinal  money," & vbCrLf
   vResult = vResult & "   FechaInicial " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "   FechaFinal " & gUtilSQL.getDateTypeForDB() & " " & vbCrLf
   vResult = vResult & ")" & vbCrLf
   vResult = vResult & "   ;WITH  FechaMaxDeCompra(" & vbCrLf
   vResult = vResult & "          ConsecutivoCompania," & vbCrLf
   vResult = vResult & "          codigo ," & vbCrLf
   vResult = vResult & "          fecha) AS(" & vbCrLf
   vResult = vResult & "          SELECT" & vbCrLf
   vResult = vResult & "             ConsecutivoCompania," & vbCrLf
   vResult = vResult & "             codigo," & vbCrLf
   vResult = vResult & "             max(fecha)" & vbCrLf
   vResult = vResult & "             FROM" & vbCrLf
   vResult = vResult & "               IGV_ArticuloInvMovimiento" & vbCrLf
   vResult = vResult & "             WHERE" & vbCrLf
   vResult = vResult & "               Fecha <  @valFechaI AND" & vbCrLf
   vResult = vResult & "                MontoOperacion > 0  AND" & vbCrLf
   vResult = vResult & "              (codigo BETWEEN @valCodigoI AND @valCodigoF) AND" & vbCrLf
   vResult = vResult & "               ConsecutivoCompania = @valCompania" & vbCrLf
   vResult = vResult & "             GROUP BY" & vbCrLf
   vResult = vResult & "              ConsecutivoCompania," & vbCrLf
   vResult = vResult & "              codigo" & vbCrLf
   vResult = vResult & "            )" & vbCrLf
   vResult = vResult & "  INSERT INTO @TablaArticulos" & vbCrLf
   vResult = vResult & "      SELECT" & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.ConsecutivoCompania," & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.Codigo," & vbCrLf
   vResult = vResult & "       (MontoOperacion/Entrada) As CostoUnitarioAterior," & vbCrLf
   vResult = vResult & "        0, " & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.FECHA As FechaInicial, " & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.FECHA As FechaFinal " & vbCrLf
   vResult = vResult & "        FROM" & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento INNER JOIN FechaMaxDeCompra  ON" & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.FECHA = FechaMaxDeCompra.FECHA AND" & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.codigo = FechaMaxDeCompra.codigo AND" & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.ConsecutivoCompania= FechaMaxDeCompra.ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         WHERE" & vbCrLf
   vResult = vResult & "            IGV_ArticuloInvMovimiento.Fecha <  @valFechaI AND" & vbCrLf
   vResult = vResult & "                 IGV_ArticuloInvMovimiento.MontoOperacion > 0  AND" & vbCrLf
   vResult = vResult & "              (IGV_ArticuloInvMovimiento.codigo BETWEEN @valCodigoI AND @valCodigoF) AND" & vbCrLf
   vResult = vResult & "                IGV_ArticuloInvMovimiento.ConsecutivoCompania = @valCompania" & vbCrLf
   vResult = vResult & "             GROUP BY" & vbCrLf
   vResult = vResult & "               IGV_ArticuloInvMovimiento.ConsecutivoCompania," & vbCrLf
   vResult = vResult & "               IGV_ArticuloInvMovimiento.codigo," & vbCrLf
   vResult = vResult & "               IGV_ArticuloInvMovimiento.MontoOperacion," & vbCrLf
   vResult = vResult & "               IGV_ArticuloInvMovimiento.Entrada," & vbCrLf
   vResult = vResult & "               IGV_ArticuloInvMovimiento.FECHA  " & vbCrLf
   vResult = vResult & "    ;WITH  FechaMaxDeCompra(" & vbCrLf
   vResult = vResult & "          ConsecutivoCompania," & vbCrLf
   vResult = vResult & "          codigo ," & vbCrLf
   vResult = vResult & "          fecha) AS(" & vbCrLf
   vResult = vResult & "          SELECT" & vbCrLf
   vResult = vResult & "             ConsecutivoCompania," & vbCrLf
   vResult = vResult & "             codigo," & vbCrLf
   vResult = vResult & "             max(fecha)" & vbCrLf
   vResult = vResult & "             FROM" & vbCrLf
   vResult = vResult & "               IGV_ArticuloInvMovimiento" & vbCrLf
   vResult = vResult & "             WHERE" & vbCrLf
   vResult = vResult & "               Fecha <=  @valFechaF AND" & vbCrLf
   vResult = vResult & "               MontoOperacion > 0  AND" & vbCrLf
   vResult = vResult & "              (codigo BETWEEN @valCodigoI AND @valCodigoF) AND" & vbCrLf
   vResult = vResult & "               ConsecutivoCompania = @valCompania" & vbCrLf
   vResult = vResult & "             GROUP BY" & vbCrLf
   vResult = vResult & "              ConsecutivoCompania," & vbCrLf
   vResult = vResult & "              codigo" & vbCrLf
   vResult = vResult & "            )" & vbCrLf
   vResult = vResult & " INSERT INTO @TablaArticulos" & vbCrLf
   vResult = vResult & "      SELECT" & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.ConsecutivoCompania," & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.Codigo," & vbCrLf
   vResult = vResult & "        0," & vbCrLf
   vResult = vResult & "         (MontoOperacion/Entrada)  As CostoUnitarioActual, " & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.FECHA As FechaInicial, " & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.FECHA As FechaFinal " & vbCrLf
   vResult = vResult & "        FROM" & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento INNER JOIN FechaMaxDeCompra  ON" & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.FECHA = FechaMaxDeCompra.FECHA AND" & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.codigo = FechaMaxDeCompra.codigo AND" & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.ConsecutivoCompania= FechaMaxDeCompra.ConsecutivoCompania" & vbCrLf
   vResult = vResult & "       WHERE" & vbCrLf
   vResult = vResult & "        IGV_ArticuloInvMovimiento.Fecha <= @valFechaF  AND" & vbCrLf
   vResult = vResult & "          IGV_ArticuloInvMovimiento.MontoOperacion > 0  AND" & vbCrLf
   vResult = vResult & "              (IGV_ArticuloInvMovimiento.codigo BETWEEN @valCodigoI AND @valCodigoF) AND" & vbCrLf
   vResult = vResult & "               IGV_ArticuloInvMovimiento.ConsecutivoCompania = @valCompania" & vbCrLf
   vResult = vResult & "             GROUP BY" & vbCrLf
   vResult = vResult & "              IGV_ArticuloInvMovimiento.ConsecutivoCompania," & vbCrLf
   vResult = vResult & "              IGV_ArticuloInvMovimiento.codigo," & vbCrLf
   vResult = vResult & "              IGV_ArticuloInvMovimiento.MontoOperacion," & vbCrLf
   vResult = vResult & "              IGV_ArticuloInvMovimiento.entrada," & vbCrLf
   vResult = vResult & "               IGV_ArticuloInvMovimiento.FECHA  " & vbCrLf
   vResult = vResult & "   INSERT INTO @datos" & vbCrLf
   vResult = vResult & "      SELECT" & vbCrLf
   vResult = vResult & "        consecutivocompania," & vbCrLf
   vResult = vResult & "        Codigo," & vbCrLf
   vResult = vResult & "        max(CostoInicial)," & vbCrLf
   vResult = vResult & "        max(CostoFinal), " & vbCrLf
   vResult = vResult & "        max(FechaInicial), " & vbCrLf
   vResult = vResult & "        Max(FechaFinal) " & vbCrLf
   vResult = vResult & "        from" & vbCrLf
   vResult = vResult & "        @TablaArticulos" & vbCrLf
   vResult = vResult & "         GROUP BY" & vbCrLf
   vResult = vResult & "         ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         codigo" & vbCrLf
   vResult = vResult & "RETURN  END;" & vbCrLf

   fStatementsFnCostosUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCostosUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fCreateFnCostoPromedioPonderado(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCostoPromedioPonderado(), fFnParametersCostoPromedioPonderado(), fFnScalarReturnDataTypeCostoPromedioPonderado(), fStatementsFnCostoPromedioPonderado(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCostoPromedioPonderado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCostoPromedioPonderado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCostoPromedioPonderado(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCostoPromedioPonderado(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCostoPromedioPonderado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCostoPromedioPonderado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCostoPromedioPonderado() As String
   GetFnNameCostoPromedioPonderado = "CostoPromedioPonderado"
End Function

Private Function fFnParametersCostoPromedioPonderado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "@valCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "@valFecha " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "@valFechaF " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "@valCodigo  " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "@valCodigo2  " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   
   fFnParametersCostoPromedioPonderado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCostoPromedioPonderado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fFnScalarReturnDataTypeCostoPromedioPonderado() As String
   Dim vResult As String
   vResult = vResult & " @datos TABLE " & vbCrLf
   vResult = vResult & "( -- Estructura de la tabla que devuelve la funcion." & vbCrLf
   vResult = vResult & "   id_num " & gUtilSQL.getNumericTypeForDB(9, 0) & " IDENTITY(1,1)," & vbCrLf
   vResult = vResult & "   Codigo " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "   Fecha " & gUtilSQL.getDateTypeForDB() & " ," & vbCrLf
   vResult = vResult & "   TipoDeDocumento  " & gUtilSQL.getVarCharTypeForDB(50) & "," & vbCrLf
   vResult = vResult & "   ExistenciaInicial " & gUtilSQL.getNumericTypeForDB(12, 4) & "," & vbCrLf
   vResult = vResult & "   CostoInicial money," & vbCrLf
   vResult = vResult & "   TotalInicial  money," & vbCrLf
   vResult = vResult & "   Entradas  " & gUtilSQL.getNumericTypeForDB(12, 4) & "," & vbCrLf
   vResult = vResult & "   TotalOperacion money," & vbCrLf
   vResult = vResult & "   Salidas  " & gUtilSQL.getNumericTypeForDB(12, 4) & "," & vbCrLf
   vResult = vResult & "   ExistenciaFinal " & gUtilSQL.getNumericTypeForDB(12, 4) & "," & vbCrLf
   vResult = vResult & "   CostoFinal  money," & vbCrLf
   vResult = vResult & "   TotalFinal money," & vbCrLf
   vResult = vResult & "   NumeroDocumento " & gUtilSQL.getVarCharTypeForDB(40) & "," & vbCrLf
   vResult = vResult & "   CostoAlaFecha " & gUtilSQL.getVarCharTypeForDB(1) & "" & vbCrLf
   vResult = vResult & ")"
   fFnScalarReturnDataTypeCostoPromedioPonderado = vResult
   End Function

Private Function fStatementsFnCostoPromedioPonderado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & " BEGIN" & vbCrLf
   vResult = vResult & "-- Variables necesarias para la lógica de la funcion." & vbCrLf
   vResult = vResult & "DECLARE @TablaArticulos Table(" & vbCrLf
   vResult = vResult & " ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & " Codigo " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & " Fecha " & gUtilSQL.getDateTypeForDB() & " ," & vbCrLf
   vResult = vResult & " Descripcion " & gUtilSQL.getVarCharTypeForDB(7000) & "," & vbCrLf
   vResult = vResult & " TipoDeDocumento  " & gUtilSQL.getVarCharTypeForDB(80) & "," & vbCrLf
   vResult = vResult & " PrioridadOperacion " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & " entrada " & gUtilSQL.getNumericTypeForDB(12, 4) & " ," & vbCrLf
   vResult = vResult & " salidad " & gUtilSQL.getNumericTypeForDB(12, 4) & " ," & vbCrLf
   vResult = vResult & " MontoOperacion  money," & vbCrLf
   vResult = vResult & " NumeroDocumento " & gUtilSQL.getVarCharTypeForDB(80) & "," & vbCrLf
   vResult = vResult & " NotaDeCredito " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & " NumeroFacturaAfectada " & gUtilSQL.getVarCharTypeForDB(80) & "," & vbCrLf
   vResult = vResult & " FechaParaBusqueda " & gUtilSQL.getDateTypeForDB() & " " & vbCrLf
   vResult = vResult & ")" & vbCrLf
   vResult = vResult & "DECLARE" & vbCrLf
   vResult = vResult & "     @ExistenciaInicial " & gUtilSQL.getNumericTypeForDB(12, 4) & "," & vbCrLf
   vResult = vResult & "     @idcuenta " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "     @NumeroDocumento " & gUtilSQL.getVarCharTypeForDB(40) & "," & vbCrLf
   vResult = vResult & "     @ExistenciaFinal decimal(12, 4) " & "," & vbCrLf
   vResult = vResult & "     @CodigoActual " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "     @Codigo " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "     @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(12, 2) & " ," & vbCrLf
   vResult = vResult & "     @Fecha " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "     @Descripcion  " & gUtilSQL.getVarCharTypeForDB(7000) & "," & vbCrLf
   vResult = vResult & "     @TipoDeDocumento  " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "     @PrioridadOperacion " & gUtilSQL.getNumericTypeForDB(9, 0) & " ," & vbCrLf
   vResult = vResult & "     @entrada decimal(12,4) " & " ," & vbCrLf
   vResult = vResult & "     @salidad decimal(12,4) " & " ," & vbCrLf
   vResult = vResult & "     @CostoInicial decimal(12,4) " & " ," & vbCrLf
   vResult = vResult & "     @TotalInicial money " & " ," & vbCrLf
   vResult = vResult & "     @TotalOperacion money " & " ," & vbCrLf
   vResult = vResult & "     @saldo decimal(12,4) " & " ," & vbCrLf
   vResult = vResult & "     @CostoFinal money " & " ," & vbCrLf
   vResult = vResult & "     @TotalFinal money" & " ," & vbCrLf
   vResult = vResult & "     @MontoOperacion money " & " ," & vbCrLf
   vResult = vResult & "     @NotaDeCredito " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "     @NumeroFacturaAfectada " & gUtilSQL.getVarCharTypeForDB(40) & "," & vbCrLf
   vResult = vResult & "     @id_num " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "     @FechaParaBusqueda " & gUtilSQL.getDateTypeForDB() & ";" & vbCrLf
   
   vResult = vResult & "      INSERT INTO  @TablaArticulos" & vbCrLf
   vResult = vResult & "       SELECT" & vbCrLf
   vResult = vResult & "         ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         Codigo," & vbCrLf
   vResult = vResult & "         Fecha AS FECHA," & vbCrLf
   vResult = vResult & "         '' AS    Descripcion   ," & vbCrLf
   vResult = vResult & "         'SALDO'   AS    TipoDeDocumento," & vbCrLf
   vResult = vResult & "          1 AS  PrioridadOperacion," & vbCrLf
   vResult = vResult & "         Existencia AS Entrada," & vbCrLf
   vResult = vResult & "         0 AS SALIDAD," & vbCrLf
   vResult = vResult & "         Costo AS MontoOpetacion," & vbCrLf
   vResult = vResult & "           'No Aplica' AS NumeroDocumento," & vbCrLf
   vResult = vResult & "            'N' AS NotaDeCredito," & vbCrLf
   vResult = vResult & "            '' AS  NumeroFacturaAfectada," & vbCrLf
   vResult = vResult & "         Fecha AS FechaParaBusqueda " & vbCrLf
   vResult = vResult & "         FROM       CierreCostoDelPeriodo" & vbCrLf
   vResult = vResult & "         WHERE      Fecha = @valFecha  AND" & vbCrLf
   vResult = vResult & "                  Codigo   Between @valCodigo AND  @valCodigo2 AND " & vbCrLf
   vResult = vResult & "                  ConsecutivoCompania = @valCompania" & vbCrLf
   vResult = vResult & "      UNION" & vbCrLf
   vResult = vResult & "         SELECT" & vbCrLf
   vResult = vResult & "          ArticuloInventario.ConsecutivoCompania," & vbCrLf
   vResult = vResult & "          ArticuloInventario.Codigo," & vbCrLf
   vResult = vResult & "          IGV_ArticuloInvMovimiento.Fecha," & vbCrLf
   vResult = vResult & "          ArticuloInventario.Descripcion," & vbCrLf
   vResult = vResult & "          IGV_ArticuloInvMovimiento.TipoDeDocumento," & vbCrLf
   vResult = vResult & "          IGV_ArticuloInvMovimiento.PrioridadOperacion," & vbCrLf
   vResult = vResult & "          IGV_ArticuloInvMovimiento.entrada," & vbCrLf
   vResult = vResult & "          IGV_ArticuloInvMovimiento.salidad," & vbCrLf
   vResult = vResult & "          IGV_ArticuloInvMovimiento.MontoOperacion ,IGV_ArticuloInvMovimiento.NumeroDocumento,IGV_ArticuloInvMovimiento.NotaDeCredito,  IGV_ArticuloInvMovimiento.NumeroFacturaAfectada AS   NumeroFacturaAfectada," & vbCrLf
   vResult = vResult & "          IGV_ArticuloInvMovimiento.FechaParaBusqueda AS   FechaParaBusqueda " & vbCrLf
   vResult = vResult & "         FROM  ArticuloInventario  LEFT JOIN IGV_ArticuloInvMovimiento ON  " & vbCrLf
   vResult = vResult & "         ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo  AND " & vbCrLf
   vResult = vResult & "         ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania " & vbCrLf
 
 
   vResult = vResult & "         WHERE       Fecha >  @valFecha AND Fecha  <= @valFechaF and" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo   Between @valCodigo AND  @valCodigo2 AND " & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = @valCompania AND " & vbCrLf
   vResult = vResult & "                  ArticuloInventario.TipoDeArticulo = '0'" & vbCrLf
   vResult = vResult & "         ORDER BY  Codigo, Fecha, PrioridadOperacion, NumeroDocumento;" & vbCrLf

   vResult = vResult & "DECLARE   Employee_Cursor CURSOR local FOR" & vbCrLf
   vResult = vResult & "   SELECT" & vbCrLf
   vResult = vResult & "     ConsecutivoCompania," & vbCrLf
   vResult = vResult & "     Codigo," & vbCrLf
   vResult = vResult & "     Fecha," & vbCrLf
   vResult = vResult & "     Descripcion," & vbCrLf
   vResult = vResult & "     TipoDeDocumento," & vbCrLf
   vResult = vResult & "     PrioridadOperacion," & vbCrLf
   vResult = vResult & "     entrada," & vbCrLf
   vResult = vResult & "     salidad," & vbCrLf
   vResult = vResult & "     MontoOperacion," & vbCrLf
   vResult = vResult & "     NumeroDocumento," & vbCrLf
   vResult = vResult & "     NotaDeCredito," & vbCrLf
   vResult = vResult & "     NumeroFacturaAfectada," & vbCrLf
   vResult = vResult & "     FechaParaBusqueda " & vbCrLf
   vResult = vResult & "   FROM @TablaArticulos" & vbCrLf
   vResult = vResult & "   ORDER BY  Codigo, Fecha, PrioridadOperacion ;" & vbCrLf
   vResult = vResult & " OPEN Employee_Cursor;" & vbCrLf
   vResult = vResult & " FETCH NEXT FROM  Employee_Cursor" & vbCrLf
   vResult = vResult & "                       INTO  @ConsecutivoCompania," & vbCrLf
   vResult = vResult & "                             @Codigo," & vbCrLf
   vResult = vResult & "                             @Fecha," & vbCrLf
   vResult = vResult & "                             @Descripcion," & vbCrLf
   vResult = vResult & "                             @TipoDeDocumento," & vbCrLf
   vResult = vResult & "                             @PrioridadOperacion," & vbCrLf
   vResult = vResult & "                             @entrada," & vbCrLf
   vResult = vResult & "                            @salidad," & vbCrLf
   vResult = vResult & "                            @MontoOperacion, @NumeroDocumento ,@NotaDeCredito,@NumeroFacturaAfectada,  @FechaParaBusqueda ;" & vbCrLf
   vResult = vResult & "set   @ExistenciaFinal = 0 ;" & vbCrLf
   vResult = vResult & "set   @CostoInicial = 0;" & vbCrLf
   vResult = vResult & "set   @CodigoActual ='';" & vbCrLf
   vResult = vResult & "set   @TotalOperacion = 0;" & vbCrLf
   vResult = vResult & "SET   @id_num = 0;" & vbCrLf
   vResult = vResult & "WHILE (@@FETCH_STATUS = 0)" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "    if  @CodigoActual =  @Codigo" & vbCrLf
   vResult = vResult & "      begin" & vbCrLf
   vResult = vResult & "          set  @ExistenciaInicial =   @ExistenciaFinal;" & vbCrLf
   vResult = vResult & "          set  @CostoInicial = @CostoFinal;" & vbCrLf
   vResult = vResult & "          set  @TotalInicial  =  @ExistenciaInicial * @CostoInicial;" & vbCrLf
   vResult = vResult & "          set  @ExistenciaFinal = @ExistenciaInicial + @Entrada  ;" & vbCrLf
   vResult = vResult & "            IF (@NotaDeCredito = " & gUtilSQL.fBooleanToSqlValue(True) & ")" & vbCrLf
   vResult = vResult & "                BEGIN" & vbCrLf
   vResult = vResult & "                  SELECT @MontoOperacion = @Entrada  * CostoFinal  FROM @datos WHERE NumeroDocumento = @NumeroFacturaAfectada AND Codigo  = @CodigoActual;" & vbCrLf
   vResult = vResult & "               IF (@MontoOperacion =  0 ) " & vbCrLf
   vResult = vResult & "                    BEGIN" & vbCrLf
   vResult = vResult & "                    SELECT TOP (1)  @MontoOperacion = @Entrada  * CostoFinal  FROM @datos WHERE Fecha <= @FechaParaBusqueda AND Codigo  = @CodigoActual" & vbCrLf
   vResult = vResult & "                    order by  Fecha desc" & vbCrLf
   vResult = vResult & "                    END  " & vbCrLf
   vResult = vResult & "                END " & vbCrLf
   vResult = vResult & "         ELSE if  (@Entrada > 0 and   @MontoOperacion = 0)" & vbCrLf
   vResult = vResult & "                  begin" & vbCrLf
   vResult = vResult & "                  set  @MontoOperacion =  @Entrada *  @CostoInicial ;" & vbCrLf
   vResult = vResult & "                  end" & vbCrLf
   vResult = vResult & "           if (@ExistenciaFinal) > 0" & vbCrLf
   vResult = vResult & "                  begin" & vbCrLf
   vResult = vResult & "                  set  @CostoFinal = (@TotalInicial + @MontoOperacion) / @ExistenciaFinal ;" & vbCrLf
   vResult = vResult & "                  end" & vbCrLf
   vResult = vResult & "          set @ExistenciaFinal = @ExistenciaInicial + @Entrada -  @Salidad;" & vbCrLf
   vResult = vResult & "          set @TotalFinal =  @ExistenciaFinal* @CostoFinal;" & vbCrLf
   vResult = vResult & "      end" & vbCrLf
   vResult = vResult & "     IF  @CodigoActual <>  @Codigo" & vbCrLf
   vResult = vResult & "       BEGIN" & vbCrLf
   vResult = vResult & "       UPDATE  @datos SET  CostoAlaFecha = " & gUtilSQL.fBooleanToSqlValue(True) & " FROM @datos WHERE id_num =  @id_num AND  Codigo =   @CodigoActual ;" & vbCrLf
   vResult = vResult & "         set @CodigoActual = @Codigo;" & vbCrLf
   vResult = vResult & "         set @ExistenciaInicial = 0;" & vbCrLf
   vResult = vResult & "         set @CostoInicial = 0;" & vbCrLf
   vResult = vResult & "         set @TotalInicial  =  0;" & vbCrLf
   vResult = vResult & "         set @CostoFinal=0;" & vbCrLf
   vResult = vResult & "         set @ExistenciaFinal = @ExistenciaInicial + @Entrada  ;" & vbCrLf
   vResult = vResult & "         IF (@NotaDeCredito= " & gUtilSQL.fBooleanToSqlValue(True) & ")" & vbCrLf
   vResult = vResult & "              BEGIN" & vbCrLf
   vResult = vResult & "                SELECT  @MontoOperacion = @Entrada * CostoFinal FROM @datos WHERE NumeroDocumento = @NumeroFacturaAfectada AND Codigo  = @CodigoActual;" & vbCrLf
   vResult = vResult & "               IF (@MontoOperacion =  0 ) " & vbCrLf
   vResult = vResult & "                 BEGIN" & vbCrLf
   vResult = vResult & "                 SELECT TOP (1) @MontoOperacion = @Entrada  * CostoFinal  FROM @datos WHERE Fecha <= @FechaParaBusqueda AND Codigo  = @CodigoActual" & vbCrLf
   vResult = vResult & "                  order by  Fecha desc" & vbCrLf
   vResult = vResult & "                  END  " & vbCrLf
   vResult = vResult & "              END " & vbCrLf
   vResult = vResult & "         ELSE if  (@Entrada > 0 and   @MontoOperacion = 0)" & vbCrLf
   vResult = vResult & "                  begin" & vbCrLf
   vResult = vResult & "                    set  @MontoOperacion =  @Entrada *  @CostoInicial ;" & vbCrLf
   vResult = vResult & "                  end" & vbCrLf
   vResult = vResult & "         IF   (@TipoDeDocumento = 'SALDO')" & vbCrLf
   vResult = vResult & "                  begin" & vbCrLf
   vResult = vResult & "                     set  @CostoFinal =  @MontoOperacion    ;" & vbCrLf
   vResult = vResult & "                  end" & vbCrLf
   vResult = vResult & "          ELSE   if (@ExistenciaFinal)> 0" & vbCrLf
   vResult = vResult & "                  begin" & vbCrLf
   vResult = vResult & "                     set  @CostoFinal = (@TotalInicial + @MontoOperacion) /   @ExistenciaFinal   ;" & vbCrLf
   vResult = vResult & "                  end" & vbCrLf
   vResult = vResult & "          set  @ExistenciaFinal = @ExistenciaInicial + @Entrada -  @Salidad;" & vbCrLf
   vResult = vResult & "          set  @TotalFinal =  @ExistenciaFinal* @CostoFinal;" & vbCrLf
   vResult = vResult & "        end" & vbCrLf
   vResult = vResult & "  INSERT INTO @datos" & vbCrLf
   vResult = vResult & "  (Codigo," & vbCrLf
   vResult = vResult & "   TipoDeDocumento," & vbCrLf
   vResult = vResult & "   Fecha," & vbCrLf
   vResult = vResult & "   ExistenciaInicial," & vbCrLf
   vResult = vResult & "   CostoInicial," & vbCrLf
   vResult = vResult & "   TotalInicial," & vbCrLf
   vResult = vResult & "   Entradas," & vbCrLf
   vResult = vResult & "   TotalOperacion," & vbCrLf
   vResult = vResult & "   Salidas," & vbCrLf
   vResult = vResult & "   ExistenciaFinal," & vbCrLf
   vResult = vResult & "   CostoFinal," & vbCrLf
   vResult = vResult & "   TotalFinal, NumeroDocumento,CostoAlaFecha)VALUES" & vbCrLf
   vResult = vResult & "  (@Codigo," & vbCrLf
   vResult = vResult & "   @TipoDeDocumento," & vbCrLf
   vResult = vResult & "   @Fecha," & vbCrLf
   vResult = vResult & "   @ExistenciaInicial  ," & vbCrLf
   vResult = vResult & "   @CostoInicial   ," & vbCrLf
   vResult = vResult & "   @TotalInicial  ," & vbCrLf
   vResult = vResult & "   @Entrada   ," & vbCrLf
   vResult = vResult & "   @MontoOperacion ," & vbCrLf
   vResult = vResult & "   @Salidad ," & vbCrLf
   vResult = vResult & "   @ExistenciaFinal  ," & vbCrLf
   vResult = vResult & "   @CostoFinal  ," & vbCrLf
   vResult = vResult & "   @TotalFinal , @NumeroDocumento,'N' );" & vbCrLf
   vResult = vResult & "   SET @id_num = @id_num + 1;" & vbCrLf
   vResult = vResult & "   FETCH  NEXT FROM  Employee_Cursor" & vbCrLf
   vResult = vResult & "                        INTO @ConsecutivoCompania," & vbCrLf
   vResult = vResult & "                             @Codigo," & vbCrLf
   vResult = vResult & "                             @Fecha," & vbCrLf
   vResult = vResult & "                             @Descripcion," & vbCrLf
   vResult = vResult & "                             @TipoDeDocumento," & vbCrLf
   vResult = vResult & "                             @PrioridadOperacion," & vbCrLf
   vResult = vResult & "                             @entrada," & vbCrLf
   vResult = vResult & "                             @salidad," & vbCrLf
   vResult = vResult & "                             @MontoOperacion, @NumeroDocumento,@NotaDeCredito,@NumeroFacturaAfectada,@FechaParaBusqueda ;" & vbCrLf
   vResult = vResult & "    END;" & vbCrLf
   vResult = vResult & "     UPDATE  @datos SET  CostoAlaFecha = " & gUtilSQL.fBooleanToSqlValue(True) & " FROM @datos WHERE id_num =  @id_num AND  Codigo = @CodigoActual ;" & vbCrLf
   vResult = vResult & " CLOSE Employee_Cursor;" & vbCrLf
   vResult = vResult & " DEALLOCATE Employee_Cursor;" & vbCrLf
   vResult = vResult & "RETURN  END;" & vbCrLf

   fStatementsFnCostoPromedioPonderado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCostoPromedioPonderado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



Private Function fCreateSpActualizacierres(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameActualizacierres(), fSpParametersActualizacierres(), fStatementsSpActualizacierres(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpActualizacierres = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpActualizacierres", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpActualizacierres(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameActualizacierres(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpActualizacierres = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpActualizacierres", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameActualizacierres() As String
   GetSpNameActualizacierres = "Actualizacierres"
End Function

Private Function fSpParametersActualizacierres() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "@valConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "@valCodigo " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   fSpParametersActualizacierres = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersActualizacierres", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpActualizacierres() As String
   Dim vResult As String
   On Error GoTo h_ERROR
  vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "  DECLARE" & vbCrLf
   vResult = vResult & "  @FechaInicio " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "  @FechaFin " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "  @vFechaPeriodo " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "  @ANo " & gUtilSQL.getVarCharTypeForDB(4) & "," & vbCrLf
   vResult = vResult & "  @Mes varchar (2)," & vbCrLf
   vResult = vResult & "  @Fecha " & gUtilSQL.getVarCharTypeForDB(10) & ";" & vbCrLf
   vResult = vResult & "  SELECT  @FechaInicio =  FechaCierreActualizada " & vbCrLf
   vResult = vResult & "  FROM ArticuloInventario" & vbCrLf
   vResult = vResult & "  WHERE" & vbCrLf
   vResult = vResult & "  ArticuloInventario.RecalcularCierre =  'S' AND " & vbCrLf
   vResult = vResult & "  ArticuloInventario.Codigo = @valCodigo   AND " & vbCrLf
   vResult = vResult & "  ArticuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "  SELECT  @FechaFin    =  MAX(Fecha)" & vbCrLf
   vResult = vResult & "  FROM CierreCostoDelPeriodo" & vbCrLf
   vResult = vResult & "  WHERE CierreCostoDelPeriodo.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "  SET @Fecha  =   '31' + '/' + '12' + '/' + cast(year(@FechaInicio)as varchar);" & vbCrLf
   vResult = vResult & "  SET @vFechaPeriodo   =  Convert(" & gUtilSQL.getDateTypeForDB() & ",@Fecha);" & vbCrLf
   vResult = vResult & "  WHILE @vFechaPeriodo <= @FechaFin" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "       UPDATE CierreCostoDelPeriodo" & vbCrLf
   vResult = vResult & "      SET  CierreCostoDelPeriodo.Existencia = TbCosto.ExistenciaFinal ," & vbCrLf
   vResult = vResult & "            CierreCostoDelPeriodo.Costo =  TbCosto.CostoFinal," & vbCrLf
   vResult = vResult & "            CierreCostoDelPeriodo.Fecha = @vFechaPeriodo" & vbCrLf
   vResult = vResult & "       FROM ArticuloInventario" & vbCrLf
   vResult = vResult & "         INNER JOIN CierreCostoDelPeriodo ON" & vbCrLf
   vResult = vResult & "         @vFechaPeriodo = CierreCostoDelPeriodo.Fecha AND" & vbCrLf
   vResult = vResult & "        ArticuloInventario.codigo = CierreCostoDelPeriodo.Codigo AND" & vbCrLf
   vResult = vResult & "         ArticuloInventario.consecutivoCompania = CierreCostoDelPeriodo.consecutivoCompania" & vbCrLf
   vResult = vResult & "         INNER JOIN dbo.Gf_CostoPromedioPonderado(@valConsecutivoCompania,@FechaInicio, @vFechaPeriodo, @valCodigo,@valCodigo) AS TbCosto ON" & vbCrLf
   vResult = vResult & "        ArticuloInventario.codigo = TbCosto.Codigo" & vbCrLf
   vResult = vResult & "       WHERE  TbCosto.CostoAlaFecha = 'S' AND" & vbCrLf
   vResult = vResult & "              ArticuloInventario.RecalcularCierre =  'S' AND" & vbCrLf
   vResult = vResult & "              ArticuloInventario.FechaCierreActualizada = @FechaInicio AND" & vbCrLf
   vResult = vResult & "              ArticuloInventario.consecutivoCompania =  @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "INSERT INTO CierreCostoDelPeriodo" & vbCrLf
   vResult = vResult & "           (ConsecutivoCompania,Codigo,Fecha,Existencia,Costo,EsCargaInicial)" & vbCrLf
   vResult = vResult & "       SELECT" & vbCrLf
   vResult = vResult & "            @valConsecutivoCompania," & vbCrLf
   vResult = vResult & "            ArticuloInventario.codigo," & vbCrLf
   vResult = vResult & "            @vFechaPeriodo," & vbCrLf
   vResult = vResult & "            TbCosto.ExistenciaFinal," & vbCrLf
   vResult = vResult & "            TbCosto.CostoFinal," & vbCrLf
   vResult = vResult & "            'N'" & vbCrLf
   vResult = vResult & "       FROM ArticuloInventario" & vbCrLf
   vResult = vResult & "         LEFT JOIN CierreCostoDelPeriodo ON" & vbCrLf
   vResult = vResult & "         @vFechaPeriodo = CierreCostoDelPeriodo.Fecha AND" & vbCrLf
   vResult = vResult & "        ArticuloInventario.codigo = CierreCostoDelPeriodo.Codigo AND" & vbCrLf
   vResult = vResult & "         ArticuloInventario.consecutivoCompania = CierreCostoDelPeriodo.consecutivoCompania" & vbCrLf
   vResult = vResult & "         INNER JOIN dbo.Gf_CostoPromedioPonderado(@valConsecutivoCompania,@FechaInicio, @vFechaPeriodo, @valCodigo, @valCodigo) AS TbCosto ON" & vbCrLf
   vResult = vResult & "        ArticuloInventario.codigo = TbCosto.Codigo" & vbCrLf
   vResult = vResult & "        WHERE  TbCosto.CostoAlaFecha = 'S' AND" & vbCrLf
   vResult = vResult & "              ArticuloInventario.RecalcularCierre =  'S' AND" & vbCrLf
   vResult = vResult & "              ArticuloInventario.FechaCierreActualizada = @FechaInicio AND" & vbCrLf
   vResult = vResult & "              ArticuloInventario.consecutivoCompania =  @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "       GROUP BY" & vbCrLf
   vResult = vResult & "       ArticuloInventario.codigo," & vbCrLf
   vResult = vResult & "      TbCosto.ExistenciaFinal," & vbCrLf
   vResult = vResult & "      TbCosto.CostoFinal," & vbCrLf
   vResult = vResult & "       CierreCostoDelPeriodo.Costo" & vbCrLf
   vResult = vResult & "       HAVING  ((case when CierreCostoDelPeriodo.Costo is null then 0 else  CierreCostoDelPeriodo.Costo end)   <= 0)" & vbCrLf
   vResult = vResult & "       UPDATE ArticuloInventario" & vbCrLf
   vResult = vResult & "      SET  ArticuloInventario.FechaCierreActualizada =  @vFechaPeriodo" & vbCrLf
   vResult = vResult & "       FROM ArticuloInventario" & vbCrLf
   vResult = vResult & "       WHERE  ArticuloInventario.RecalcularCierre =  'S' AND" & vbCrLf
   vResult = vResult & "              ArticuloInventario.FechaCierreActualizada = @FechaInicio AND" & vbCrLf
   vResult = vResult & "              ArticuloInventario.consecutivoCompania =  @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "      SET @FechaInicio   =  @vFechaPeriodo;" & vbCrLf
   vResult = vResult & "      SET @vFechaPeriodo   =    dateadd(year,1,@vFechaPeriodo);" & vbCrLf
   vResult = vResult & "      END;" & vbCrLf
   vResult = vResult & "       UPDATE ArticuloInventario" & vbCrLf
   vResult = vResult & "      SET  ArticuloInventario.FechaCierreActualizada =  @FechaFin," & vbCrLf
   vResult = vResult & "            ArticuloInventario.RecalcularCierre =  'N'" & vbCrLf
   vResult = vResult & "       FROM ArticuloInventario" & vbCrLf
   vResult = vResult & "       WHERE  ArticuloInventario.RecalcularCierre =  'S' AND" & vbCrLf
   vResult = vResult & "              ArticuloInventario.FechaCierreActualizada = @FechaInicio AND" & vbCrLf
   vResult = vResult & "              ArticuloInventario.consecutivoCompania =  @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "END;" & vbCrLf
   fStatementsSpActualizacierres = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpActualizacierres", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateFnArticuloInventarioMaxOrMinCodigo(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameArticuloInventarioMaxOrMinCodigo(), fFnParametersArticuloInventarioMaxOrMinCodigo(), fFnScalarReturnDataTypeArticuloInventarioMaxOrMinCodigo(), fStatementsFnArticuloInventarioMaxOrMinCodigo(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnArticuloInventarioMaxOrMinCodigo = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnArticuloInventarioMaxOrMinCodigo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnArticuloInventarioMaxOrMinCodigo(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameArticuloInventarioMaxOrMinCodigo(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnArticuloInventarioMaxOrMinCodigo = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnArticuloInventarioMaxOrMinCodigo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameArticuloInventarioMaxOrMinCodigo() As String
   GetFnNameArticuloInventarioMaxOrMinCodigo = "ArticuloInventarioMaxOrMinCodigo"
End Function

Private Function fFnParametersArticuloInventarioMaxOrMinCodigo() As String
    Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & " @valConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & " @TipoDeArticulo char(1) " & "," & vbCrLf
   vResult = vResult & " @StatusArticulo char(1) " & "," & vbCrLf
   vResult = vResult & " @Maximo char(1) " & vbCrLf
   fFnParametersArticuloInventarioMaxOrMinCodigo = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersArticuloInventarioMaxOrMinCodigo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeArticuloInventarioMaxOrMinCodigo() As String
   fFnScalarReturnDataTypeArticuloInventarioMaxOrMinCodigo = gUtilSQL.getVarCharTypeForDB(30)
End Function

Private Function fStatementsFnArticuloInventarioMaxOrMinCodigo() As String
     Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Result " & gUtilSQL.getVarCharTypeForDB(30) & ";" & vbCrLf
   vResult = vResult & "       IF   @Maximo = 'S'" & vbCrLf
   vResult = vResult & "            SELECT @Result =  MAX(Codigo) FROM ArticuloInventario" & vbCrLf
   vResult = vResult & "            WHERE  ArticuloInventario.TipoDeArticulo =  @TipoDeArticulo AND" & vbCrLf
   vResult = vResult & "                   ArticuloInventario.StatusdelArticulo = @StatusArticulo AND" & vbCrLf
   vResult = vResult & "                   ArticuloInventario.ConsecutivoCompania  = @valConsecutivoCompania ;" & vbCrLf
   vResult = vResult & "       ELSE" & vbCrLf
   vResult = vResult & "            SELECT @Result =  MIN(Codigo) FROM ArticuloInventario" & vbCrLf
   vResult = vResult & "            WHERE  ArticuloInventario.TipoDeArticulo =  @TipoDeArticulo AND" & vbCrLf
   vResult = vResult & "                   ArticuloInventario.StatusdelArticulo = @StatusArticulo AND" & vbCrLf
   vResult = vResult & "                   ArticuloInventario.ConsecutivoCompania  = @valConsecutivoCompania ;" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnArticuloInventarioMaxOrMinCodigo = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnArticuloInventarioMaxOrMinCodigo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'--------------------
Private Function fCreateSpoInventarioActualizarCostosyMargenes(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameoInventarioActualizarCostosyMargenes(), fSpParametersoInventarioActualizarCostosyMargenes(), fStatementsSpoInventarioActualizarCostosyMargenes(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpoInventarioActualizarCostosyMargenes = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpoInventarioActualizarCostosyMargenes", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpoInventarioActualizarCostosyMargenes(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameoInventarioActualizarCostosyMargenes(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpoInventarioActualizarCostosyMargenes = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpoInventarioActualizarCostosyMargenes", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameoInventarioActualizarCostosyMargenes() As String
   GetSpNameoInventarioActualizarCostosyMargenes = "InventarioActualizarCostosyMargenes"
End Function

Private Function fSpParametersoInventarioActualizarCostosyMargenes() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "@valConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "@valMargen  " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valMargen2 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valMargen3 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valMargen4 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valPrecio char(1)," & vbCrLf
   vResult = vResult & "@valPrecio2 char(1)," & vbCrLf
   vResult = vResult & "@valPrecio3 char(1)," & vbCrLf
   vResult = vResult & "@valPrecio4 char(1)," & vbCrLf
   vResult = vResult & "@valAlicuotaGeneral " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valAlicuota2 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valAlicuota3 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valFecha " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "@valNumeroDocumento " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "@valTipoDeDocumento " & gUtilSQL.getVarCharTypeForDB(15) & "" & vbCrLf
   fSpParametersoInventarioActualizarCostosyMargenes = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersInventarioActualizarCostosyMargenes", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpoInventarioActualizarCostosyMargenes() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   If @valPrecio ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia = @valMargen ," & vbCrLf
   vResult = vResult & "                PrecioSinIva  = (((CostoPromedio * @valMargen  ) / 100) + CostoPromedio)," & vbCrLf
   vResult = vResult & "                PrecioConIva  = (((CostoPromedio * @valMargen  ) / 100) + CostoPromedio) + ((((CostoPromedio *  @valMargen  ) / 100) + CostoPromedio) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio2 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia2 = @valMargen2 ," & vbCrLf
   vResult = vResult & "                PrecioSinIva2  = (((CostoPromedio * @valMargen2  ) / 100) + CostoPromedio)," & vbCrLf
   vResult = vResult & "                PrecioConIva2  = (((CostoPromedio * @valMargen2  ) / 100) + CostoPromedio) + ((((CostoPromedio *  @valMargen2  ) / 100) + CostoPromedio) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio3 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia3 = @valMargen3 ," & vbCrLf
   vResult = vResult & "                PrecioSinIva3  = (((CostoPromedio * @valMargen3  ) / 100) + CostoPromedio)," & vbCrLf
   vResult = vResult & "                PrecioConIva3  = (((CostoPromedio * @valMargen3  ) / 100) + CostoPromedio) + ((((CostoPromedio *  @valMargen3  ) / 100) + CostoPromedio) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio4 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia4 = @valMargen4 ," & vbCrLf
   vResult = vResult & "                PrecioSinIva4  = (((CostoPromedio * @valMargen4  ) / 100) + CostoPromedio)," & vbCrLf
   vResult = vResult & "                PrecioConIva4 = (((CostoPromedio * @valMargen4  ) / 100) + CostoPromedio) + ((((CostoPromedio *  @valMargen4  ) / 100) + CostoPromedio) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "END "
   fStatementsSpoInventarioActualizarCostosyMargenes = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpoInventarioActualizarCostosyMargenes", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
 
'--------------------
Private Function fCreateSpArticuloInventarioActualizarCostos(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameArticuloInventarioActualizarCostos(), fSpParametersArticuloInventarioActualizarCostos(), fStatementsSpArticuloInventarioActualizarCostos(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpArticuloInventarioActualizarCostos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpArticuloInventarioActualizarCostos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpArticuloInventarioActualizarCostos(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameArticuloInventarioActualizarCostos(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpArticuloInventarioActualizarCostos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpArticuloInventarioActualizarCostos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameArticuloInventarioActualizarCostos() As String
   GetSpNameArticuloInventarioActualizarCostos = "ArticuloInventarioActualizarCostos"
End Function
Private Function fSpParametersArticuloInventarioActualizarCostos() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "    (@valConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "     @valPrecio char(1)," & vbCrLf
   vResult = vResult & "     @valPrecio2 char(1)," & vbCrLf
   vResult = vResult & "     @valPrecio3 char(1)," & vbCrLf
   vResult = vResult & "     @valPrecio4 char(1)," & vbCrLf
   vResult = vResult & "     @valAlicuotaGeneral " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "     @valAlicuota2 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "     @valAlicuota3 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "     @valFecha " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "     @valNumeroDocumento " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "     @valTipoDeDocumento " & gUtilSQL.getVarCharTypeForDB(15) & ")" & vbCrLf
    fSpParametersArticuloInventarioActualizarCostos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersArticuloInventarioActualizarCostos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpArticuloInventarioActualizarCostos() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   If @valPrecio ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  PrecioSinIva  = (((CostoPromedio * margenganancia  ) / 100) + CostoPromedio)," & vbCrLf
   vResult = vResult & "                PrecioConIva  = (((CostoPromedio * margenganancia  ) / 100) + CostoPromedio) + ((((CostoPromedio *  margenganancia  ) / 100) + CostoPromedio) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio2 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  PrecioSinIva2  = (((CostoPromedio *  margenganancia2  ) / 100) + CostoPromedio)," & vbCrLf
   vResult = vResult & "                PrecioConIva2  = (((CostoPromedio *  margenganancia2  ) / 100) + CostoPromedio) + ((((CostoPromedio *   margenganancia2  ) / 100) + CostoPromedio) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio3 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  PrecioSinIva3  = (((CostoPromedio * margenganancia3 ) / 100) + CostoPromedio)," & vbCrLf
   vResult = vResult & "                PrecioConIva3  = (((CostoPromedio * margenganancia3  ) / 100) + CostoPromedio) + ((((CostoPromedio *  margenganancia3  ) / 100) + CostoPromedio) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio4 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  PrecioSinIva4  = (((CostoPromedio * margenganancia4  ) / 100) + CostoPromedio)," & vbCrLf
   vResult = vResult & "                PrecioConIva4 = (((CostoPromedio * margenganancia4  ) / 100) + CostoPromedio) + ((((CostoPromedio *  margenganancia4  ) / 100) + CostoPromedio) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "END "
   fStatementsSpArticuloInventarioActualizarCostos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpArticuloInventarioActualizarCostos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateFnMovimientosDeInventario(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameMovimientosDeInventario(), fFnParametersMovimientosDeInventario(), fFnScalarReturnDataTypeMovimientosDeInventario(), fStatementsFnMovimientosDeInventario(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnMovimientosDeInventario = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnMovimientosDeInventario", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropFnMovimientosDeInventario(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameMovimientosDeInventario(), valShowMessage)
   Set insLibSf = Nothing
  fDropFnMovimientosDeInventario = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnNameMovimientosDeInventario", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameMovimientosDeInventario() As String
   GetFnNameMovimientosDeInventario = "MovimientosDeInventario"
End Function

Private Function fFnParametersMovimientosDeInventario() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "   @valFechaI " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "   @valFechaF " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "   @valFechacierre " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "   @valConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " ," & vbCrLf
   vResult = vResult & "   @valCodigoI " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "   @valCodigoF " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & vbCrLf
    fFnParametersMovimientosDeInventario = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersMovimientosDeInventario", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fFnScalarReturnDataTypeMovimientosDeInventario() As String
   Dim vResult As String
   vResult = vResult & " TABLE" & vbCrLf
   fFnScalarReturnDataTypeMovimientosDeInventario = vResult
   End Function

Private Function fStatementsFnMovimientosDeInventario() As String
   Dim vResult As String
   On Error GoTo h_ERROR
    vResult = vResult & "RETURN" & vbCrLf
   vResult = vResult & "(" & vbCrLf
   vResult = vResult & "SELECT" & vbCrLf
   vResult = vResult & "ArticuloInventario.ConsecutivoCompania," & vbCrLf
   vResult = vResult & "ArticuloInventario.Codigo," & vbCrLf
   vResult = vResult & "ArticuloInventario.Descripcion ," & vbCrLf
   vResult = vResult & "SUM(  CASE WHEN  (ENTRADA IS NULL )  THEN 0 ELSE ENTRADA END   ) AS Entrada," & vbCrLf
   vResult = vResult & "SUM(  CASE WHEN IGV_ArticuloInvMovimiento.TipoDeDocumento = 'Autoconsumo' OR IGV_ArticuloInvMovimiento.TipoDeDocumento= 'Retiro' OR  (IGV_ArticuloInvMovimiento.salidad IS NULL )  THEN 0 ELSE IGV_ArticuloInvMovimiento.salidad END   )AS Salida," & vbCrLf
   vResult = vResult & "SUM(  CASE WHEN IGV_ArticuloInvMovimiento.TipoDeDocumento = 'Retiro' THEN IGV_ArticuloInvMovimiento.salidad ELSE 0 END   )AS Retiro," & vbCrLf
   vResult = vResult & "SUM(  CASE WHEN IGV_ArticuloInvMovimiento.TipoDeDocumento= 'Autoconsumo' THEN IGV_ArticuloInvMovimiento.salidad ELSE 0 END   )AS Autoconsumo" & vbCrLf
   vResult = vResult & "FROM   ArticuloInventario" & vbCrLf
   vResult = vResult & "LEFT  JOIN IGV_ArticuloInvMovimiento ON IGV_ArticuloInvMovimiento.Fecha" & vbCrLf
   vResult = vResult & "BETWEEN @valFechaI AND @valFechaF  AND" & vbCrLf
   vResult = vResult & "ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo  AND" & vbCrLf
   vResult = vResult & "ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania" & vbCrLf
   vResult = vResult & "WHERE ArticuloInventario.Codigo" & vbCrLf
   vResult = vResult & "BETWEEN  @valCodigoI AND @valCodigoF AND" & vbCrLf
   vResult = vResult & "ArticuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "Group By" & vbCrLf
   vResult = vResult & "  ArticuloInventario.ConsecutivoCompania," & vbCrLf
   vResult = vResult & "  ArticuloInventario.codigo," & vbCrLf
   vResult = vResult & "  ArticuloInventario.Descripcion" & vbCrLf
   vResult = vResult & ")" & vbCrLf
   fStatementsFnMovimientosDeInventario = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnMovimientosDeInventario", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateFnCostoPromedioMovimientosInv(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCostoPromedioMovimientosInv(), fFnParametersCostoPromedioMovimientosInv(), fFnScalarReturnDataTypeCostoPromedioMovimientosInv(), fStatementsFnCostoPromedioMovimientosInv(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCostoPromedioMovimientosInv = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCostoPromedioMovimientosInv", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropFnCostoPromedioMovimientosInv(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCostoPromedioMovimientosInv(), valShowMessage)
   Set insLibSf = Nothing
  fDropFnCostoPromedioMovimientosInv = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnNameCostoPromedioMovimientosInv", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCostoPromedioMovimientosInv() As String
   GetFnNameCostoPromedioMovimientosInv = "CostoPromedioMovimientosInv"
End Function

Private Function fFnParametersCostoPromedioMovimientosInv() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "   @valFechaI " & gUtilSQL.getDateTypeForDB() & " ," & vbCrLf
   vResult = vResult & "   @valFechaF " & gUtilSQL.getDateTypeForDB() & " ," & vbCrLf
   vResult = vResult & "   @valFechacierre " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "   @ConsecutivoCompania INT ," & vbCrLf
   vResult = vResult & "   @CodigoInicial " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "   @CodigoFinal " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   fFnParametersCostoPromedioMovimientosInv = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCostoPromedioMovimientosInv", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fFnScalarReturnDataTypeCostoPromedioMovimientosInv() As String
   Dim vResult As String
   vResult = vResult & " TABLE" & vbCrLf
   fFnScalarReturnDataTypeCostoPromedioMovimientosInv = vResult
   End Function

Private Function fStatementsFnCostoPromedioMovimientosInv() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "RETURN" & vbCrLf
   vResult = vResult & "(" & vbCrLf
   vResult = vResult & "SELECT" & vbCrLf
   vResult = vResult & "MovimientosDeInventario.ConsecutivoCompania," & vbCrLf
   vResult = vResult & "MovimientosDeInventario.Codigo," & vbCrLf
   vResult = vResult & "MovimientosDeInventario.Descripcion ," & vbCrLf
   vResult = vResult & "MovimientosDeInventario.Entrada," & vbCrLf
   vResult = vResult & "MovimientosDeInventario.Salida," & vbCrLf
   vResult = vResult & "MovimientosDeInventario.Retiro," & vbCrLf
   vResult = vResult & "MovimientosDeInventario.Autoconsumo," & vbCrLf
   vResult = vResult & "(CASE WHEN  (fn_CostoPromedioPonderado_1.CostoFinal IS NULL )  THEN 0 ELSE fn_CostoPromedioPonderado_1.CostoFinal END )   AS   CostoFinal" & vbCrLf
   vResult = vResult & "FROM  dbo.Gf_MovimientosDeInventario ( @valFechaI,@valFechaF,@valFechacierre, @ConsecutivoCompania, @CodigoInicial, @CodigoFinal)  AS MovimientosDeInventario" & vbCrLf
   vResult = vResult & "INNER   JOIN  dbo.Gf_CostoPromedioPonderado(@ConsecutivoCompania,@valFechacierre,@valFechaF,  @CodigoInicial, @CodigoFinal) AS fn_CostoPromedioPonderado_1  on" & vbCrLf
   vResult = vResult & "MovimientosDeInventario.Codigo = fn_CostoPromedioPonderado_1.Codigo    and fn_CostoPromedioPonderado_1.CostoALaFecha = 'S'" & vbCrLf
   vResult = vResult & ")" & vbCrLf
   fStatementsFnCostoPromedioMovimientosInv = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCostoPromedioMovimientosInv", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function




 



'--------------------
Private Function fCreateSpoInventarioActualizarCostosyMargenesFormulaOpcional(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameoInventarioActualizarCostosyMargenesFormulaOpcional(), fSpParametersoInventarioActualizarCostosyMargenesFormulaOpcional(), fStatementsSpoInventarioActualizarCostosyMargenesFormulaOpcional(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpoInventarioActualizarCostosyMargenesFormulaOpcional = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpoInventarioActualizarCostosyMargenesFormulaOpcional", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpoInventarioActualizarCostosyMargenesFormulaOpcional(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameoInventarioActualizarCostosyMargenes(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpoInventarioActualizarCostosyMargenesFormulaOpcional = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpoInventarioActualizarCostosyMargenes", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameoInventarioActualizarCostosyMargenesFormulaOpcional() As String
  GetSpNameoInventarioActualizarCostosyMargenesFormulaOpcional = "InventarioActualizarCostosyMargenesFormulaOpcional"
End Function

Private Function fSpParametersoInventarioActualizarCostosyMargenesFormulaOpcional() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "@valConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "@valMargen  " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valMargen2 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valMargen3 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valMargen4 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valPrecio char(1)," & vbCrLf
   vResult = vResult & "@valPrecio2 char(1)," & vbCrLf
   vResult = vResult & "@valPrecio3 char(1)," & vbCrLf
   vResult = vResult & "@valPrecio4 char(1)," & vbCrLf
   vResult = vResult & "@valAlicuotaGeneral " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valAlicuota2 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valAlicuota3 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valFecha " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "@valNumeroDocumento " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "@valTipoDeDocumento " & gUtilSQL.getVarCharTypeForDB(15) & "" & vbCrLf
   fSpParametersoInventarioActualizarCostosyMargenesFormulaOpcional = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersInventarioActualizarCostosyMargenes", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpoInventarioActualizarCostosyMargenesFormulaOpcional() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   If @valPrecio ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia = @valMargen ," & vbCrLf
   vResult = vResult & "                PrecioSinIva  = (CostoPromedio * (1 / ((1 - @valMargen /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva  = (CostoPromedio * (1 / ((1 - @valMargen /100)))) +  " & vbCrLf
   vResult = vResult & "               (CostoPromedio *( (1 / ((1 - @valMargen /100)))) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)  / 100)" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio2 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia2 = @valMargen2 ," & vbCrLf
   vResult = vResult & "                PrecioSinIva2  =  (CostoPromedio *(1 / ((1 - @valMargen2 /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva2  =  (CostoPromedio *(1 / ((1 - @valMargen2 /100)))) +  " & vbCrLf
   vResult = vResult & "                (CostoPromedio *(1 / ((1 - @valMargen2 /100))) *  " & vbCrLf
   vResult = vResult & "                (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)  / 100)" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio3 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia3 = @valMargen3 ," & vbCrLf
   vResult = vResult & "                PrecioSinIva3  =  (CostoPromedio * (1 / ((1 - @valMargen3 /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva3  =  (CostoPromedio * (1 / ((1 - @valMargen3 /100)))) +  " & vbCrLf
   vResult = vResult & "               (CostoPromedio *((1 / ((1 - @valMargen3 /100)))) *  " & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)   / 100)" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio4 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia4 = @valMargen4 ," & vbCrLf
   vResult = vResult & "                PrecioSinIva4  =  (CostoPromedio *(1 / ((1 - @valMargen4 /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva4  =  (CostoPromedio *(1 / ((1 - @valMargen4 /100)))) +  " & vbCrLf
   vResult = vResult & "                (CostoPromedio *((1 / ((1 - @valMargen4 /100))))* " & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)   / 100)" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "END "
    fStatementsSpoInventarioActualizarCostosyMargenesFormulaOpcional = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpoInventarioActualizarCostosyMargenes", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


'--------------------
Private Function fCreateSpArticuloInventarioActualizarCostosFormulaOpcional(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameArticuloInventarioActualizarCostosFormulaOpcional(), fSpParametersArticuloInventarioActualizarCostosFormulaOpcional(), fStatementsSpArticuloInventarioActualizarCostosFormulaOpcional(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpArticuloInventarioActualizarCostosFormulaOpcional = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpArticuloInventarioActualizarCostosFormulaOpcional", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpArticuloInventarioActualizarCostosFormulaOpcional(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameArticuloInventarioActualizarCostosFormulaOpcional(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpArticuloInventarioActualizarCostosFormulaOpcional = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpArticuloInventarioActualizarCostosFormulaOpcional", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameArticuloInventarioActualizarCostosFormulaOpcional() As String
   GetSpNameArticuloInventarioActualizarCostosFormulaOpcional = "ArticuloInventarioActualizarCostosFormulaOpcional"
End Function
Private Function fSpParametersArticuloInventarioActualizarCostosFormulaOpcional() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "    (@valConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "     @valPrecio char(1)," & vbCrLf
   vResult = vResult & "     @valPrecio2 char(1)," & vbCrLf
   vResult = vResult & "     @valPrecio3 char(1)," & vbCrLf
   vResult = vResult & "     @valPrecio4 char(1)," & vbCrLf
   vResult = vResult & "     @valAlicuotaGeneral " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "     @valAlicuota2 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "     @valAlicuota3 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "     @valFecha " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "     @valNumeroDocumento " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "     @valTipoDeDocumento " & gUtilSQL.getVarCharTypeForDB(15) & ")" & vbCrLf
    fSpParametersArticuloInventarioActualizarCostosFormulaOpcional = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersArticuloInventarioActualizarCostos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpArticuloInventarioActualizarCostosFormulaOpcional() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   If @valPrecio ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "        SET     PrecioSinIva  =  (CostoPromedio *(1 / ((1 - margenganancia /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva  =  (CostoPromedio *(1 / ((1 - margenganancia /100)))) +  " & vbCrLf
   vResult = vResult & "               (CostoPromedio *( (1 / ((1 - margenganancia /100))) )*" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)  / 100)" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio2 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "        UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "        SET     PrecioSinIva2  =  (CostoPromedio *(1 / ((1 - margenganancia2 /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva2  =  (CostoPromedio *(1 / ((1 - margenganancia2 /100)))) +  " & vbCrLf
   vResult = vResult & "               (CostoPromedio *(1 / ((1 - margenganancia2 /100))) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio3 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "        SET     PrecioSinIva3  =  (CostoPromedio *(1 / ((1 - margenganancia3 /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva3  =  (CostoPromedio *(1 / ((1 - margenganancia3 /100)))) +  " & vbCrLf
   vResult = vResult & "               (CostoPromedio *( (1 / ((1 - margenganancia3 /100)))) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio4 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "        SET     PrecioSinIva4  =  (CostoPromedio *(1 / ((1 - margenganancia4 /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva4  =  (CostoPromedio *(1 / ((1 - margenganancia4 /100)))) +  " & vbCrLf
   vResult = vResult & "               (CostoPromedio *( (1 / ((1 - margenganancia4 /100)))) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "END "
   fStatementsSpArticuloInventarioActualizarCostosFormulaOpcional = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpArticuloInventarioActualizarCostos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'--------------------
Private Function fCreateSpoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra(), fSpParametersoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra(), fStatementsSpoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpoInventarioActualizarCostosyMargenes", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra() As String
  GetSpNameoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra = "InventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra"
End Function

Private Function fSpParametersoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "@valConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "@valMargen  " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valMargen2 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valMargen3 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valMargen4 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valPrecio char(1)," & vbCrLf
   vResult = vResult & "@valPrecio2 char(1)," & vbCrLf
   vResult = vResult & "@valPrecio3 char(1)," & vbCrLf
   vResult = vResult & "@valPrecio4 char(1)," & vbCrLf
   vResult = vResult & "@valAlicuotaGeneral " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valAlicuota2 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valAlicuota3 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valFecha " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "@valNumeroDocumento " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "@valTipoDeDocumento " & gUtilSQL.getVarCharTypeForDB(15) & "" & vbCrLf
   fSpParametersoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersInventarioActualizarCostosyMargenes", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   If @valPrecio ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia = @valMargen ," & vbCrLf
   vResult = vResult & "                PrecioSinIva  =  ( CostoUnitario * (1 / ((1 - @valMargen /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva  =   ( CostoUnitario *(1 / ((1 - @valMargen /100)))) +  " & vbCrLf
   vResult = vResult & "               ( CostoUnitario * ( (1 / ((1 - @valMargen /100))) ) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)  / 100)" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio2 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia2 = @valMargen2 ," & vbCrLf
   vResult = vResult & "                PrecioSinIva2  =  ( CostoUnitario * (1 / ((1 - @valMargen2 /100))) ) ," & vbCrLf
   vResult = vResult & "                PrecioConIva2  =  ( CostoUnitario * (1 / ((1 - @valMargen2 /100)))) +  " & vbCrLf
   vResult = vResult & "                ( CostoUnitario * ((1 / ((1 - @valMargen2 /100)))) *  " & vbCrLf
   vResult = vResult & "                (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)  / 100)" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio3 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia3 = @valMargen3 ," & vbCrLf
   vResult = vResult & "                PrecioSinIva3  = ( CostoUnitario *  (1 / ((1 - @valMargen3 /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva3  = ( CostoUnitario *   (1 / ((1 - @valMargen3 /100)))) +  " & vbCrLf
   vResult = vResult & "              ( CostoUnitario *  ((1 / ((1 - @valMargen3 /100)))) *  " & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)   / 100)" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio4 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia4 = @valMargen4 ," & vbCrLf
   vResult = vResult & "                PrecioSinIva4  =  ( CostoUnitario * (1 / ((1 - @valMargen4 /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva4  =  ( CostoUnitario * (1 / ((1 - @valMargen4 /100)))) +  " & vbCrLf
   vResult = vResult & "               ( CostoUnitario *  ((1 / ((1 - @valMargen4 /100)))) * " & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)   / 100)" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "END "
    fStatementsSpoInventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpoInventarioActualizarCostosyMargenes", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


'--------------------
Private Function fCreateSpArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra(), fSpParametersArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra(), fStatementsSpArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra() As String
   GetSpNameArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra = "ArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra"
End Function
Private Function fSpParametersArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "    (@valConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "     @valPrecio char(1)," & vbCrLf
   vResult = vResult & "     @valPrecio2 char(1)," & vbCrLf
   vResult = vResult & "     @valPrecio3 char(1)," & vbCrLf
   vResult = vResult & "     @valPrecio4 char(1)," & vbCrLf
   vResult = vResult & "     @valAlicuotaGeneral " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "     @valAlicuota2 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "     @valAlicuota3 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "     @valFecha " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "     @valNumeroDocumento " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "     @valTipoDeDocumento " & gUtilSQL.getVarCharTypeForDB(15) & ")" & vbCrLf
    fSpParametersArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersArticuloInventarioActualizarCostos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   If @valPrecio ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "        SET     PrecioSinIva  =  (CostoUnitario *(1 / ((1 - margenganancia /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva  =  (CostoUnitario *(1 / ((1 - margenganancia /100)))) +  " & vbCrLf
   vResult = vResult & "               (CostoUnitario *( (1 / ((1 - margenganancia /100))) )*" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)  / 100)" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio2 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "        UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "        SET     PrecioSinIva2  =  (CostoUnitario *(1 / ((1 - margenganancia2 /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva2  =  (CostoUnitario *(1 / ((1 - margenganancia2 /100)))) +  " & vbCrLf
   vResult = vResult & "               (CostoUnitario *( (1 / ((1 - margenganancia2 /100))) )*" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END ) / 100)" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio3 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "        SET     PrecioSinIva3  =  (CostoUnitario *(1 / ((1 - margenganancia3 /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva3  =  (CostoUnitario *(1 / ((1 - margenganancia3 /100)))) +  " & vbCrLf
   vResult = vResult & "               (CostoUnitario *( (1 / ((1 - margenganancia3 /100)))) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio4 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "        SET     PrecioSinIva4  =  (CostoUnitario *(1 / ((1 - margenganancia4 /100))))  ," & vbCrLf
   vResult = vResult & "                PrecioConIva4  =  (CostoUnitario *(1 / ((1 - margenganancia4 /100)))) +  " & vbCrLf
   vResult = vResult & "               (CostoUnitario *( (1 / ((1 - margenganancia4 /100)))) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "END "
   fStatementsSpArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpArticuloInventarioActualizarCostos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'--------------------
Private Function fCreateSpArticuloInventarioActualizarCostosxUltimaCompra(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameArticuloInventarioActualizarCostosxUltimaCompra(), fSpParametersArticuloInventarioActualizarCostosxUltimaCompra(), fStatementsSpArticuloInventarioActualizarCostosxUltimaCompra(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpArticuloInventarioActualizarCostosxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpArticuloInventarioActualizarCostosxUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpArticuloInventarioActualizarCostosxUltimaCompra(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameArticuloInventarioActualizarCostosxUltimaCompra(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpArticuloInventarioActualizarCostosxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpArticuloInventarioActualizarCostosxUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameArticuloInventarioActualizarCostosxUltimaCompra() As String
   GetSpNameArticuloInventarioActualizarCostosxUltimaCompra = "ArticuloInventarioActualizarCostosxUltimaCompra"
End Function
Private Function fSpParametersArticuloInventarioActualizarCostosxUltimaCompra() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "    (@valConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "     @valPrecio char(1)," & vbCrLf
   vResult = vResult & "     @valPrecio2 char(1)," & vbCrLf
   vResult = vResult & "     @valPrecio3 char(1)," & vbCrLf
   vResult = vResult & "     @valPrecio4 char(1)," & vbCrLf
   vResult = vResult & "     @valAlicuotaGeneral " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "     @valAlicuota2 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "     @valAlicuota3 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "     @valFecha " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "     @valNumeroDocumento " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "     @valTipoDeDocumento " & gUtilSQL.getVarCharTypeForDB(15) & ")" & vbCrLf
    fSpParametersArticuloInventarioActualizarCostosxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersArticuloInventarioActualizarCostosxUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpArticuloInventarioActualizarCostosxUltimaCompra() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   If @valPrecio ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  PrecioSinIva  = (((CostoUnitario * margenganancia  ) / 100) + CostoUnitario)," & vbCrLf
   vResult = vResult & "                PrecioConIva  = (((CostoUnitario * margenganancia  ) / 100) + CostoUnitario) + ((((CostoUnitario *  margenganancia  ) / 100) + CostoUnitario) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio2 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  PrecioSinIva2  = (((CostoUnitario *  margenganancia2  ) / 100) + CostoUnitario)," & vbCrLf
   vResult = vResult & "                PrecioConIva2  = (((CostoUnitario *  margenganancia2  ) / 100) + CostoUnitario) + ((((CostoUnitario *   margenganancia2  ) / 100) + CostoUnitario) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio3 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  PrecioSinIva3  = (((CostoUnitario * margenganancia3 ) / 100) + CostoUnitario)," & vbCrLf
   vResult = vResult & "                PrecioConIva3  = (((CostoUnitario * margenganancia3  ) / 100) + CostoUnitario) + ((((CostoUnitario *  margenganancia3  ) / 100) + CostoUnitario) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio4 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  PrecioSinIva4  = (((CostoUnitario * margenganancia4  ) / 100) + CostoUnitario)," & vbCrLf
   vResult = vResult & "                PrecioConIva4 = (((CostoUnitario * margenganancia4  ) / 100) + CostoUnitario) + ((((CostoUnitario *  margenganancia4  ) / 100) + CostoUnitario) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "END "
   fStatementsSpArticuloInventarioActualizarCostosxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpArticuloInventarioActualizarCostosxUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


'--------------------
Private Function fCreateSpInventarioActualizarCostosyMargenesxUltimaCompra(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameoActualizarCostosyMargenesxUltimaCompra(), fSpParametersoActualizarCostosyMargenesxUltimaCompra(), fStatementsSpoActualizarCostosyMargenesxUltimaCompra(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
 fCreateSpInventarioActualizarCostosyMargenesxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpoActualizarCostosyMargenesxUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpInventarioActualizarCostosyMargenesxUltimaCompra(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameoActualizarCostosyMargenesxUltimaCompra(), valShowMessage)
   Set insLibSp = Nothing
  fDropSpInventarioActualizarCostosyMargenesxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpoActualizarCostosyMargenesxUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameoActualizarCostosyMargenesxUltimaCompra() As String
   GetSpNameoActualizarCostosyMargenesxUltimaCompra = "InventarioActualizarCostosyMargenesxUltimaCompra"
End Function

Private Function fSpParametersoActualizarCostosyMargenesxUltimaCompra() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "@valConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "@valMargen  " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valMargen2 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valMargen3 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valMargen4 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valPrecio char(1)," & vbCrLf
   vResult = vResult & "@valPrecio2 char(1)," & vbCrLf
   vResult = vResult & "@valPrecio3 char(1)," & vbCrLf
   vResult = vResult & "@valPrecio4 char(1)," & vbCrLf
   vResult = vResult & "@valAlicuotaGeneral " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valAlicuota2 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valAlicuota3 " & gUtilSQL.getNumericTypeForDB(12, 2) & "," & vbCrLf
   vResult = vResult & "@valFecha " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "@valNumeroDocumento " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
   vResult = vResult & "@valTipoDeDocumento " & gUtilSQL.getVarCharTypeForDB(15) & "" & vbCrLf
   fSpParametersoActualizarCostosyMargenesxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersActualizarCostosyMargenesxUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpoActualizarCostosyMargenesxUltimaCompra() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   If @valPrecio ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia = @valMargen ," & vbCrLf
   vResult = vResult & "                PrecioSinIva  = (((CostoUnitario * @valMargen  ) / 100) + CostoUnitario)," & vbCrLf
   vResult = vResult & "                PrecioConIva  = (((CostoUnitario * @valMargen  ) / 100) + CostoUnitario) + ((((CostoUnitario *  @valMargen  ) / 100) + CostoUnitario) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio2 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia2 = @valMargen2 ," & vbCrLf
   vResult = vResult & "                PrecioSinIva2  = (((CostoUnitario * @valMargen2  ) / 100) + CostoUnitario)," & vbCrLf
   vResult = vResult & "                PrecioConIva2  = (((CostoUnitario * @valMargen2  ) / 100) + CostoUnitario) + ((((CostoUnitario *  @valMargen2  ) / 100) + CostoUnitario) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio3 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia3 = @valMargen3 ," & vbCrLf
   vResult = vResult & "                PrecioSinIva3  = (((CostoUnitario * @valMargen3  ) / 100) + CostoUnitario)," & vbCrLf
   vResult = vResult & "                PrecioConIva3  = (((CostoUnitario * @valMargen3  ) / 100) + CostoUnitario) + ((((CostoUnitario *  @valMargen3  ) / 100) + CostoUnitario) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "If @valPrecio4 ='S'" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         UPDATE dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "           SET  margenganancia4 = @valMargen4 ," & vbCrLf
   vResult = vResult & "                PrecioSinIva4  = (((CostoUnitario * @valMargen4  ) / 100) + CostoUnitario)," & vbCrLf
   vResult = vResult & "                PrecioConIva4 = (((CostoUnitario * @valMargen4  ) / 100) + CostoUnitario) + ((((CostoUnitario *  @valMargen4  ) / 100) + CostoUnitario) *" & vbCrLf
   vResult = vResult & "               (CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END) ) / 100" & vbCrLf
   vResult = vResult & "           FROM  articuloInventario   INNER JOIN  IGV_ArticuloInvMovimiento ON" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "                  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo" & vbCrLf
   vResult = vResult & "                  WHERE IGV_ArticuloInvMovimiento.Fecha =  @valFecha AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.NumeroDocumento = @valNumeroDocumento AND" & vbCrLf
   vResult = vResult & "                  IGV_ArticuloInvMovimiento.TipoDeDocumento =  @valTipoDeDocumento AND" & vbCrLf
   vResult = vResult & "                  articuloInventario.ConsecutivoCompania = @valConsecutivoCompania" & vbCrLf
   vResult = vResult & "    END " & vbCrLf
   vResult = vResult & "END "
   fStatementsSpoActualizarCostosyMargenesxUltimaCompra = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpoActualizarCostosyMargenesxUltimaCompra", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function fCreateFnCompraUltimaFecha(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCompraUltimaFecha(), fFnParametersCompraUltimaFecha(), fFnScalarReturnDataTypeCompraUltimaFecha(), fStatementsFnCompraUltimaFecha(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCompraUltimaFecha = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCompraUnaFecha", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCompraUltimaFecha(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCompraUltimaFecha(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCompraUltimaFecha = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCompraUltimaFecha", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCompraUltimaFecha() As String
   GetFnNameCompraUltimaFecha = "CompraUltimaFecha"
End Function

Private Function fFnParametersCompraUltimaFecha() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & " @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " ," & vbCrLf
   vResult = vResult & " @Fecha " & gUtilSQL.getDateTypeForDB() & " ," & vbCrLf
   vResult = vResult & " @CodigoArticulo VARCHAR(30)" & vbCrLf
   fFnParametersCompraUltimaFecha = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCompraUltimaFecha", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeCompraUltimaFecha() As String
   fFnScalarReturnDataTypeCompraUltimaFecha = " smalldatetime "
End Function

Private Function fStatementsFnCompraUltimaFecha() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "DECLARE" & vbCrLf
   vResult = vResult & "@Return smalldatetime " & vbCrLf
   vResult = vResult & "SELECT  TOP 1     @Return =" & vbCrLf
   vResult = vResult & "        IGV_ComprasTotalesSinOtrosGastos.Fecha" & vbCrLf
   vResult = vResult & "FROM      IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1" & vbCrLf
   vResult = vResult & "          INNER JOIN" & vbCrLf
   vResult = vResult & "          IGV_ComprasTotalesSinOtrosGastos ON" & vbCrLf
   vResult = vResult & "          IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania =  IGV_ComprasTotalesSinOtrosGastos.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "          IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = IGV_ComprasTotalesSinOtrosGastos.CodigoArticulo" & vbCrLf
   vResult = vResult & "          INNER JOIN" & vbCrLf
   vResult = vResult & "          IGV_ComprasOtroGastosPorItem ON" & vbCrLf
   vResult = vResult & "          IGV_ComprasTotalesSinOtrosGastos.ConsecutivoCompania =  IGV_ComprasOtroGastosPorItem.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "          IGV_ComprasTotalesSinOtrosGastos.NumeroSecuencial =  IGV_ComprasOtroGastosPorItem.NumeroSecuencial AND" & vbCrLf
   vResult = vResult & "          IGV_ComprasTotalesSinOtrosGastos.Numero =  IGV_ComprasOtroGastosPorItem.Numero" & vbCrLf
   vResult = vResult & "WHERE     IGV_ComprasTotalesSinOtrosGastos.EsUnaOrdenDeCompra = 'N' AND" & vbCrLf
   vResult = vResult & "          IGV_ComprasTotalesSinOtrosGastos.StatusCompra = '0'  AND" & vbCrLf
   vResult = vResult & "          IGV_ComprasTotalesSinOtrosGastos.Fecha <=  @Fecha AND" & vbCrLf
   vResult = vResult & "          IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo =  @CodigoArticulo    AND" & vbCrLf
   vResult = vResult & "          IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "Order by   IGV_ComprasTotalesSinOtrosGastos.Fecha  desc" & vbCrLf
   vResult = vResult & "RETURN @Return" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnCompraUltimaFecha = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCompraUltimaFecha", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpInventarioLiberarReservas(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameInventarioLiberarReservas(), fSpParametersSpInventarioLiberarReservas(), fStatementsSpInventarioLiberarReservas(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpInventarioLiberarReservas = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpInventarioLiberarReservas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpInventarioLiberarReservas(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameInventarioLiberarReservas(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpInventarioLiberarReservas = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpInventarioLiberarReservas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameInventarioLiberarReservas() As String
   GetSpNameInventarioLiberarReservas = "InventarioLiberarReservas"
End Function

Private Function fSpParametersSpInventarioLiberarReservas() As String
   Dim vResult As String
   On Error GoTo h_ERROR
    vResult = vResult & " (@CodigoArticulo " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
    vResult = vResult & "  @Serial " & gUtilSQL.getVarCharTypeForDB(50) & "," & vbCrLf
    vResult = vResult & "  @Rollo  " & gUtilSQL.getVarCharTypeForDB(20) & "," & vbCrLf
    vResult = vResult & " @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
    vResult = vResult & " @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
    vResult = vResult & "  @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & ")" & vbCrLf
    fSpParametersSpInventarioLiberarReservas = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersSpInventarioLiberarReservas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpInventarioLiberarReservas() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "    SET NOCOUNT ON;" & vbCrLf
   vResult = vResult & "    DECLARE @Codigo  VARCHAR(30)" & vbCrLf
   vResult = vResult & "    DECLARE @CodigoTalla VARCHAR(3)" & vbCrLf
   vResult = vResult & "    DECLARE @CodigoColor VARCHAR(3)" & vbCrLf
   vResult = vResult & "    DECLARE @CodigoGrupo VARCHAR(10)" & vbCrLf
   vResult = vResult & "    DECLARE @Reservado  money" & vbCrLf
   vResult = vResult & "    DECLARE @ValidationMsg VARCHAR(1500)" & vbCrLf
   vResult = vResult & "    DECLARE @ReturnValue " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "    SELECT   @Codigo =   Codigo ," & vbCrLf
   vResult = vResult & "             @CodigoTalla  = CodigoTalla," & vbCrLf
   vResult = vResult & "             @CodigoColor  = CodigoColor," & vbCrLf
   vResult = vResult & "             @CodigoGrupo  = CodigoGrupo" & vbCrLf
   vResult = vResult & "    FROM dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1" & vbCrLf
   vResult = vResult & "    WHERE   CodigoDelArticulo = @CodigoArticulo AND" & vbCrLf
   vResult = vResult & "            Rollo   = @Rollo AND " & vbCrLf
   vResult = vResult & "            Serial  = @Serial AND " & vbCrLf
   vResult = vResult & "            ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "    GROUP BY Codigo,Serial,Rollo,CodigoTalla,CodigoColor,CodigoGrupo" & vbCrLf
   vResult = vResult & "    SELECT @Reservado = SUM(dbo.IGV_InventarioConOSinReservas_B1.CantidadReservada) - SUM(dbo.IGV_InventarioConOSinReservas_B1.CantidadDespachada)" & vbCrLf
   vResult = vResult & "    FROM  dbo.IGV_InventarioConOSinReservas_B1 " & vbCrLf
   vResult = vResult & "    WHERE   CodigoDelArticulo = @CodigoArticulo   AND" & vbCrLf
   vResult = vResult & "            Serial =  @Serial AND" & vbCrLf
   vResult = vResult & "            Rollo  =  @Rollo  AND" & vbCrLf
   vResult = vResult & "            ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "    GROUP BY CodigoDelArticulo" & vbCrLf
   vResult = vResult & "    BEGIN TRAN" & vbCrLf
   vResult = vResult & "        UPDATE  dbo.ExistenciaPorGrupo" & vbCrLf
   vResult = vResult & "         SET CantReservada = @Reservado" & vbCrLf
   vResult = vResult & "         WHERE   CodigoArticulo =@Codigo AND" & vbCrLf
   vResult = vResult & "                 Serial = @Serial AND" & vbCrLf
   vResult = vResult & "                 Rollo  =  @Rollo AND" & vbCrLf
   vResult = vResult & "                 CodigoTalla = @CodigoTalla AND" & vbCrLf
   vResult = vResult & "                 CodigoColor = @CodigoColor  AND" & vbCrLf
   vResult = vResult & "                 CodigoGrupo =  @CodigoGrupo AND" & vbCrLf
   vResult = vResult & "                 ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "        SELECT @Reservado = SUM(CantReservada)" & vbCrLf
   vResult = vResult & "        FROM  dbo.ExistenciaPorGrupo" & vbCrLf
   vResult = vResult & "        WHERE  CodigoArticulo = @Codigo  AND" & vbCrLf
   vResult = vResult & "               ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "        GROUP BY CodigoArticulo" & vbCrLf
   vResult = vResult & "        UPDATE  dbo.ArticuloInventario" & vbCrLf
   vResult = vResult & "        SET CantArtReservado = @Reservado" & vbCrLf
   vResult = vResult & "        WHERE    Codigo = @Codigo  AND" & vbCrLf
   vResult = vResult & "                 ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "    SET @ReturnValue = @@ROWCOUNT" & vbCrLf
   vResult = vResult & "     IF @@ERROR = 0" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "     COMMIT TRAN ;" & vbCrLf
   vResult = vResult & "       IF @ReturnValue = 0" & vbCrLf
   vResult = vResult & "           RAISERROR('El registro ha sido modificado o eliminado por otro usuario', 14, 1)" & vbCrLf
   vResult = vResult & "       END" & vbCrLf
   vResult = vResult & "     ELSE" & vbCrLf
   vResult = vResult & "       BEGIN" & vbCrLf
   vResult = vResult & "         SET @ReturnValue = -1" & vbCrLf
   vResult = vResult & "         SET @ValidationMsg = 'Se ha producido un error al Modificar: ' + CAST(@@ERROR AS NVARCHAR(8))" & vbCrLf
   vResult = vResult & "         RAISERROR(@ValidationMsg, 14 ,1)" & vbCrLf
   vResult = vResult & "         ROLLBACK" & vbCrLf
   vResult = vResult & "       END" & vbCrLf
   vResult = vResult & "    RETURN @ReturnValue" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpInventarioLiberarReservas = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpInventarioLiberarReservas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

