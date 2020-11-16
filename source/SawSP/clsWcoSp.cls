VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsWcoSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsWcoSp"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "clsWcoSp"
End Function
Public Function fCreateProjectStoredProcedures(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   vResult = vResult And fCreateDropBalancesSP(True, valShowMessage, valWithEncryption)
   fCreateProjectStoredProcedures = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateProjectStoredProcedures", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fDropProjectStoredProcedures(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   vResult = vResult And fCreateDropBalancesSP(False, valShowMessage)
   fDropProjectStoredProcedures = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropProjectStoredProcedures", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateDropBalancesSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      'SP para Informe de Balance General
      vResult = vResult And fCreateFnCuentaResultadoEjercicio(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnDescripcionCuenta(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnMontoResultadoDelEjercicio(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpBGActivos(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpBGPasivosYPatrimonio(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpBGCuentasDeOrden(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpGananciasyPerdida(valShowMessage, valWithEncryption)
   Else
      'SP para Informe de Balance General
      vResult = vResult And fDropSpGananciasyPerdida(valShowMessage)
      vResult = vResult And fDropSpBGCuentasDeOrden(valShowMessage)
      vResult = vResult And fDropSpBGPasivosYPatrimonio(valShowMessage)
      vResult = vResult And fDropSpBGActivos(valShowMessage)
      vResult = vResult And fCreateFnMontoResultadoDelEjercicio(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnDescripcionCuenta(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnCuentaResultadoEjercicio(valShowMessage, valWithEncryption)
   End If
   fCreateDropBalancesSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropBalancesSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpBGActivos(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameEsquemaBalanceCuentas(), fSpParametersEsquemaBalanceCuentas(), fStatementsSpEsquemaBalanceCuentas(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibSp.fCreateStoredProcedure(GetSpNameBalanceBGActivoFijo(), fSpParametersBalanceBGActivoFijo(), fStatementsSpBalanceBGActivoFijo(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibSp.fCreateStoredProcedure(GetSpNameBalanceBGDepreciacionAF(), fSpParametersBalanceBGDepreciacionAF(), fStatementsSpBalanceBGDepreciacionAF(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibSp.fCreateStoredProcedure(GetSpNameBalanceBGInventarios(), fSpParametersBalanceBGInventarios(), fStatementsSpBalanceBGInventarios(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibSp.fCreateStoredProcedure(GetSpNameBalanceBGOtrosActivos(), fSpParametersBalanceBGOtrosActivos(), fStatementsSpBalanceBGOtrosActivos(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpBGActivos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpBGActivos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropSpBGActivos(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameEsquemaBalanceCuentas(), valShowMessage)
   vResult = vResult And insLibSp.fDropStoredProc(GetSpNameBalanceBGActivoFijo(), valShowMessage)
   vResult = vResult And insLibSp.fDropStoredProc(GetSpNameBalanceBGDepreciacionAF(), valShowMessage)
   vResult = vResult And insLibSp.fDropStoredProc(GetSpNameBalanceBGInventarios(), valShowMessage)
   vResult = vResult And insLibSp.fDropStoredProc(GetSpNameBalanceBGOtrosActivos(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpBGActivos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpBGActivos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameBalanceBGActivoFijo() As String
   GetSpNameBalanceBGActivoFijo = "BalanceBGActivoFijo"
End Function

Private Function fSpParametersBalanceBGActivoFijo() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @DateFormat " & gUtilSQL.getVarCharTypeForDB(3) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @CuentaUsadaPara " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @FechaDesde  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaHasta  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaCorte  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @MostrarInforme  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   fSpParametersBalanceBGActivoFijo = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersBalanceBGActivoFijo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpBalanceBGActivoFijo() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   SET DATEFORMAT @DateFormat " & vbCrLf
   vResult = vResult & "   SELECT " & vbCrLf
   vResult = vResult & "   CodigoCuenta, " & vbCrLf
   vResult = vResult & "   DescripcionCuenta, " & vbCrLf
   vResult = vResult & "   SUM(Saldo) As Monto1 " & vbCrLf
   vResult = vResult & "   FROM IGV_CuentasInformeDeBalanceYResultado " & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo " & vbCrLf
   vResult = vResult & "   AND TipoCuenta = " & gUtilSQL.fSimpleSqlValue("OTROS") & vbCrLf
   vResult = vResult & "   AND Fecha <= @FechaCorte " & vbCrLf
   vResult = vResult & "   GROUP BY CodigoCuenta, DescripcionCuenta " & vbCrLf
   vResult = vResult & "   ORDER BY CodigoCuenta " & vbCrLf
   vResult = vResult & "END"
   fStatementsSpBalanceBGActivoFijo = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpBalanceBGActivoFijo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameBalanceBGDepreciacionAF() As String
   GetSpNameBalanceBGDepreciacionAF = "BalanceBGDepreciacionAF"
End Function

Private Function fSpParametersBalanceBGDepreciacionAF() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @DateFormat " & gUtilSQL.getVarCharTypeForDB(3) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @CuentaUsadaPara " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @FechaDesde  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaHasta  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaCorte  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @MostrarInforme  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   fSpParametersBalanceBGDepreciacionAF = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersBalanceBGDepreciacionAF", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpBalanceBGDepreciacionAF() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   SET DATEFORMAT @DateFormat " & vbCrLf
   vResult = vResult & "   SELECT " & vbCrLf
   vResult = vResult & "   CodigoCuenta, " & vbCrLf
   vResult = vResult & "   DescripcionCuenta, " & vbCrLf
   vResult = vResult & "   SUM(Saldo) As Monto1 " & vbCrLf
   vResult = vResult & "   FROM IGV_CuentasInformeDeBalanceYResultado " & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo " & vbCrLf
   vResult = vResult & "   AND TipoCuenta = " & gUtilSQL.fSimpleSqlValue("OTROS") & vbCrLf
   vResult = vResult & "   AND Fecha <= @FechaCorte " & vbCrLf
   vResult = vResult & "   GROUP BY CodigoCuenta, DescripcionCuenta " & vbCrLf
   vResult = vResult & "   ORDER BY CodigoCuenta " & vbCrLf
   vResult = vResult & "END"
   fStatementsSpBalanceBGDepreciacionAF = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpBalanceBGDepreciacionAF", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameBalanceBGInventarios() As String
   GetSpNameBalanceBGInventarios = "BalanceBGInventarios"
End Function

Private Function fSpParametersBalanceBGInventarios() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @DateFormat " & gUtilSQL.getVarCharTypeForDB(3) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @CuentaUsadaPara " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @FechaDesde  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaHasta  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaCorte  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @MostrarInforme  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   fSpParametersBalanceBGInventarios = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersBalanceBGInventarios", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpBalanceBGInventarios() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   SET DATEFORMAT @DateFormat " & vbCrLf
   vResult = vResult & "   SELECT " & vbCrLf
   vResult = vResult & "   CodigoCuenta, " & vbCrLf
   vResult = vResult & "   DescripcionCuenta, " & vbCrLf
   vResult = vResult & "   SUM(Saldo) As Monto1 " & vbCrLf
   vResult = vResult & "   FROM IGV_CuentasInformeDeBalanceYResultado " & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo " & vbCrLf
   vResult = vResult & "   AND TipoCuenta = " & gUtilSQL.fSimpleSqlValue("OTROS") & vbCrLf
   vResult = vResult & "   AND Fecha <= @FechaCorte " & vbCrLf
   vResult = vResult & "   GROUP BY CodigoCuenta, DescripcionCuenta " & vbCrLf
   vResult = vResult & "   ORDER BY CodigoCuenta " & vbCrLf
   vResult = vResult & "END"
   fStatementsSpBalanceBGInventarios = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpBalanceBGInventarios", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameBalanceBGOtrosActivos() As String
   GetSpNameBalanceBGOtrosActivos = "BalanceBGOtrosActivos"
End Function

Private Function fSpParametersBalanceBGOtrosActivos() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @DateFormat " & gUtilSQL.getVarCharTypeForDB(3) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @CuentaUsadaPara " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @FechaDesde  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaHasta  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaCorte  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @MostrarInforme  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   fSpParametersBalanceBGOtrosActivos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersBalanceBGOtrosActivos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpBalanceBGOtrosActivos() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   SET DATEFORMAT @DateFormat " & vbCrLf
   vResult = vResult & "   SELECT " & vbCrLf
   vResult = vResult & "   CodigoCuenta, " & vbCrLf
   vResult = vResult & "   DescripcionCuenta, " & vbCrLf
   vResult = vResult & "   SUM(Saldo) As Monto1 " & vbCrLf
   vResult = vResult & "   FROM IGV_CuentasInformeDeBalanceYResultado " & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo " & vbCrLf
   vResult = vResult & "   AND TipoCuenta = '0'" '& gUtilSQL.fSimpleSqlValue(enum_TipoCuenta.eTC_Activo) & vbCrLf
   vResult = vResult & "   AND Fecha <= @FechaCorte " & vbCrLf
   vResult = vResult & "   GROUP BY CodigoCuenta, DescripcionCuenta " & vbCrLf
   vResult = vResult & "   ORDER BY CodigoCuenta " & vbCrLf
   vResult = vResult & "END"
   fStatementsSpBalanceBGOtrosActivos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpBalanceBGOtrosActivos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpBGPasivosYPatrimonio(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameBalanceBGPasivos(), fSpParametersBalanceBGPasivos(), fStatementsSpBalanceBGPasivos(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibSp.fCreateStoredProcedure(GetSpNameBalanceBGPatrimonio(), fSpParametersBalanceBGPatrimonio(), fStatementsSpBalanceBGPatrimonio(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpBGPasivosYPatrimonio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpBGPasivosYPatrimonio", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropSpBGPasivosYPatrimonio(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameBalanceBGPasivos(), valShowMessage)
   vResult = vResult And insLibSp.fDropStoredProc(GetSpNameBalanceBGPatrimonio(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpBGPasivosYPatrimonio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpBGPasivosYPatrimonio", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameBalanceBGPasivos() As String
   GetSpNameBalanceBGPasivos = "BalanceBGPasivos"
End Function

Private Function fSpParametersBalanceBGPasivos() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @DateFormat " & gUtilSQL.getVarCharTypeForDB(3) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @CuentaUsadaPara " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @FechaDesde  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaHasta  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaCorte  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @MostrarInforme  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   fSpParametersBalanceBGPasivos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersBalanceBGPasivos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpBalanceBGPasivos() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   SET DATEFORMAT @DateFormat " & vbCrLf
   vResult = vResult & "   SELECT " & vbCrLf
   vResult = vResult & "   CodigoCuenta, " & vbCrLf
   vResult = vResult & "   DescripcionCuenta, " & vbCrLf
   vResult = vResult & "   SUM(Saldo) As Monto1 " & vbCrLf
   vResult = vResult & "   FROM IGV_CuentasInformeDeBalanceYResultado " & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo " & vbCrLf
   vResult = vResult & "   AND TipoCuenta = '1'" '& gUtilSQL.fSimpleSqlValue(enum_TipoCuenta.eTC_Pasivo) & vbCrLf
   vResult = vResult & "   AND Fecha <= @FechaCorte " & vbCrLf
   vResult = vResult & "   GROUP BY CodigoCuenta, DescripcionCuenta " & vbCrLf
   vResult = vResult & "   ORDER BY CodigoCuenta " & vbCrLf
   vResult = vResult & "END"
   fStatementsSpBalanceBGPasivos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpBalanceBGPasivos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameBalanceBGPatrimonio() As String
   GetSpNameBalanceBGPatrimonio = "BalanceBGPatrimonio"
End Function

Private Function fSpParametersBalanceBGPatrimonio() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @DateFormat " & gUtilSQL.getVarCharTypeForDB(3) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @CuentaUsadaPara " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @FechaDesde  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaHasta  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaCorte  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @MostrarInforme  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   fSpParametersBalanceBGPatrimonio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersBalanceBGPatrimonio", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpBalanceBGPatrimonio() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "    SET DATEFORMAT @DateFormat" & vbCrLf
   vResult = vResult & "    DECLARE @FechaCierrePeriodo  " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "    DECLARE @FechaInicioPeriodo  " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "    DECLARE @MontoResultadoEjercicio Money" & vbCrLf
   vResult = vResult & "    DECLARE @CuentaResultadoEjercicio  " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "    DECLARE @DescipcionCuenta  " & gUtilSQL.getVarCharTypeForDB(40) & "" & vbCrLf
   vResult = vResult & "    DECLARE @sql  " & gUtilSQL.getVarCharTypeForDB(3000) & "" & vbCrLf
   vResult = vResult & "    DECLARE @sqlUnion  " & gUtilSQL.getVarCharTypeForDB(1000) & "" & vbCrLf
   vResult = vResult & "    DECLARE @Query  " & gUtilSQL.getVarCharTypeForDB(4000) & "" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    SELECT @FechaInicioPeriodo = FechaAperturaDelPeriodo FROM PERIODO WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "    SELECT @FechaCierrePeriodo = FechaCierreDelPeriodo FROM PERIODO WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    SET @sqlUnion = ''" & vbCrLf
   vResult = vResult & "    SET @Sql = 'SELECT '" & vbCrLf
   vResult = vResult & "    SET @Sql = @Sql + 'CodigoCuenta, '" & vbCrLf
   vResult = vResult & "    SET @Sql = @Sql + 'DescripcionCuenta, '" & vbCrLf
   vResult = vResult & "    SET @Sql = @Sql + 'SUM(Saldo) AS Monto1 '" & vbCrLf
   vResult = vResult & "    SET @Sql = @Sql + 'FROM IGV_CuentasInformeDeBalanceYResultado '" & vbCrLf
   vResult = vResult & "    SET @Sql = @Sql + 'WHERE  ConsecutivoPeriodo = ' + CAST(@ConsecutivoPeriodo AS varchar)" & vbCrLf
   vResult = vResult & "    SET @Sql = @Sql + ' AND TipoCuenta = ''2'''" & vbCrLf
   vResult = vResult & "    SET @Sql = @Sql + ' AND Fecha <= CAST(''' + CONVERT(varchar, @FechaCorte, 103) + ''' AS smalldatetime)'" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    IF NOT EXISTS(SELECT Numero FROM dbo.COMPROBANTE WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND GeneradoPor = '3')" & vbCrLf
   vResult = vResult & "        OR NOT @FechaCorte = @FechaCierrePeriodo" & vbCrLf
   vResult = vResult & "    BEGIN" & vbCrLf
   vResult = vResult & "       EXEC @CuentaResultadoEjercicio = Gf_CuentaResultadoEjercicio @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "       EXEC @DescipcionCuenta = Gf_DescripcionCuenta @ConsecutivoPeriodo, @CuentaResultadoEjercicio" & vbCrLf
   vResult = vResult & "       EXEC @MontoResultadoEjercicio = Gf_MontoResultadoDelEjercicio @ConsecutivoPeriodo, @FechaInicioPeriodo, @FechaCorte, @FechaCierrePeriodo" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "       SET @Sql = @Sql + ' AND CodigoCuenta <> ''' + CAST(@CuentaResultadoEjercicio AS varchar) + ''''" & vbCrLf
   vResult = vResult & "       SET @Sql = @Sql + ' GROUP BY CodigoCuenta, DescripcionCuenta '" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "       SET @sqlUnion = ' UNION SELECT '''" & vbCrLf
   vResult = vResult & "       SET @sqlUnion = @sqlUnion + @CuentaResultadoEjercicio + ''' AS CodigoCuenta, '''" & vbCrLf
   vResult = vResult & "       SET @sqlUnion = @sqlUnion + @DescipcionCuenta + ''' AS DescripcionCuenta, '" & vbCrLf
   vResult = vResult & "       SET @sqlUnion = @sqlUnion + 'CAST(''' + CONVERT(varchar, @MontoResultadoEjercicio) + ''' AS money) As Monto1 '" & vbCrLf
   vResult = vResult & "    END" & vbCrLf
   vResult = vResult & "    ELSE" & vbCrLf
   vResult = vResult & "    BEGIN" & vbCrLf
   vResult = vResult & "        SET @Sql = @Sql + ' GROUP BY CodigoCuenta, DescripcionCuenta '" & vbCrLf
   vResult = vResult & "    END" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    SET @Query = @sql + @sqlUnion" & vbCrLf
   vResult = vResult & "    SET @Query = @Query + ' ORDER BY CodigoCuenta'" & vbCrLf
   vResult = vResult & "    EXECUTE(@Query)" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpBalanceBGPatrimonio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpBalanceBGPatrimonio", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpBGCuentasDeOrden(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameBalanceBGDeOrden(), fSpParametersBalanceBGDeOrden(), fStatementsSpBalanceBGDeOrden(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpBGCuentasDeOrden = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpBGCuentasDeOrden", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropSpBGCuentasDeOrden(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameBalanceBGDeOrden(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpBGCuentasDeOrden = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpBGCuentasDeOrden", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameBalanceBGDeOrden() As String
   GetSpNameBalanceBGDeOrden = "BalanceBGDeOrden"
End Function

Private Function fSpParametersBalanceBGDeOrden() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @DateFormat " & gUtilSQL.getVarCharTypeForDB(3) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @CuentaUsadaPara " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @FechaDesde  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaHasta  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaCorte  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @MostrarInforme  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   fSpParametersBalanceBGDeOrden = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersBalanceBGDeOrden", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpBalanceBGDeOrden() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   SET DATEFORMAT @DateFormat " & vbCrLf
   vResult = vResult & "   SELECT " & vbCrLf
   vResult = vResult & "   CodigoCuenta, " & vbCrLf
   vResult = vResult & "   DescripcionCuenta, " & vbCrLf
   vResult = vResult & "   SUM(Saldo) As Monto1 " & vbCrLf
   vResult = vResult & "   FROM IGV_CuentasInformeDeBalanceYResultado " & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo " & vbCrLf
   vResult = vResult & "   AND (TipoCuenta = '7'" & vbCrLf
   vResult = vResult & "   OR TipoCuenta = '8'" & ")" & vbCrLf
   vResult = vResult & "   AND Fecha <= @FechaCorte " & vbCrLf
   vResult = vResult & "   GROUP BY CodigoCuenta, DescripcionCuenta " & vbCrLf
   vResult = vResult & "   ORDER BY CodigoCuenta " & vbCrLf
   vResult = vResult & "END"
   fStatementsSpBalanceBGDeOrden = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpBalanceBGDeOrden", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpGananciasyPerdida(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameBalanceGyPIngresos(), fSpParametersBalanceGyPIngresos(), fStatementsSpBalanceGyPIngresos(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibSp.fCreateStoredProcedure(GetSpNameBalanceGyPEgresos(), fSpParametersBalanceGyPEgresos(), fStatementsSpBalanceGyPEgresos(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibSp.fCreateStoredProcedure(GetSpNameBalanceGyPCostoVenta(), fSpParametersBalanceGyPCostoVenta(), fStatementsSpBalanceGyPCostoVenta(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibSp.fCreateStoredProcedure(GetSpNameBalanceGyPAsientosDeReserva(), fSpParametersBalanceGyPAsientosDeReserva(), fStatementsSpBalanceGyPAsientosDeReserva(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibSp.fCreateStoredProcedure(GetSpNameBalanceGyPAsientosDeCierre(), fSpParametersBalanceGyPAsientosDeCierre(), fStatementsSpBalanceGyPAsientosDeCierre(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpGananciasyPerdida = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpGananciasyPerdida", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropSpGananciasyPerdida(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameBalanceGyPIngresos(), valShowMessage)
   vResult = vResult And insLibSp.fDropStoredProc(GetSpNameBalanceGyPEgresos(), valShowMessage)
   vResult = vResult And insLibSp.fDropStoredProc(GetSpNameBalanceGyPCostoVenta(), valShowMessage)
   vResult = vResult And insLibSp.fDropStoredProc(GetSpNameBalanceGyPAsientosDeReserva(), valShowMessage)
   vResult = vResult And insLibSp.fDropStoredProc(GetSpNameBalanceGyPAsientosDeCierre(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpGananciasyPerdida = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpGananciasyPerdida", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameBalanceGyPIngresos() As String
   GetSpNameBalanceGyPIngresos = "BalanceGyPIngresos"
End Function

Private Function fSpParametersBalanceGyPIngresos() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @DateFormat " & gUtilSQL.getVarCharTypeForDB(3) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @CuentaUsadaPara " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @FechaDesde  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaHasta  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaCorte  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @MostrarInforme  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   fSpParametersBalanceGyPIngresos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersBalanceGyPIngresos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpBalanceGyPIngresos() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   SET DATEFORMAT @DateFormat " & vbCrLf
   vResult = vResult & "   SELECT " & vbCrLf
   vResult = vResult & "   CodigoCuenta, " & vbCrLf
   vResult = vResult & "   DescripcionCuenta, " & vbCrLf
   vResult = vResult & "   SUM(Saldo) As Monto1 " & vbCrLf
   vResult = vResult & "   FROM IGV_CuentasInformeDeBalanceYResultado " & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo " & vbCrLf
   vResult = vResult & "   AND TipoCuenta = '3'" & vbCrLf
   vResult = vResult & "   AND Fecha BETWEEN @FechaDesde AND @FechaHasta" & vbCrLf
   vResult = vResult & "   AND GeneradoPor <> '3'" & vbCrLf
   vResult = vResult & "   GROUP BY CodigoCuenta, DescripcionCuenta " & vbCrLf
   vResult = vResult & "   ORDER BY CodigoCuenta " & vbCrLf
   vResult = vResult & "END"
   fStatementsSpBalanceGyPIngresos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpBalanceGyPIngresos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameBalanceGyPEgresos() As String
   GetSpNameBalanceGyPEgresos = "BalanceGyPEgresos"
End Function

Private Function fSpParametersBalanceGyPEgresos() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @DateFormat " & gUtilSQL.getVarCharTypeForDB(3) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @CuentaUsadaPara " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @FechaDesde  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaHasta  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaCorte  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @MostrarInforme  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   fSpParametersBalanceGyPEgresos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersBalanceGyPEgresos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpBalanceGyPEgresos() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   SET DATEFORMAT @DateFormat " & vbCrLf
   vResult = vResult & "   SELECT " & vbCrLf
   vResult = vResult & "   CodigoCuenta, " & vbCrLf
   vResult = vResult & "   DescripcionCuenta, " & vbCrLf
   vResult = vResult & "   SUM(Saldo) As Monto1 " & vbCrLf
   vResult = vResult & "   FROM IGV_CuentasInformeDeBalanceYResultado " & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo " & vbCrLf
   vResult = vResult & "   AND TipoCuenta = '5'" & vbCrLf
   vResult = vResult & "   AND Fecha BETWEEN @FechaDesde AND @FechaHasta" & vbCrLf
   vResult = vResult & "   AND GeneradoPor <> '3'" & vbCrLf
   vResult = vResult & "   GROUP BY CodigoCuenta, DescripcionCuenta " & vbCrLf
   vResult = vResult & "   ORDER BY CodigoCuenta " & vbCrLf
   vResult = vResult & "END"
   fStatementsSpBalanceGyPEgresos = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpBalanceGyPEgresos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameBalanceGyPCostoVenta() As String
   GetSpNameBalanceGyPCostoVenta = "BalanceGyPCostoVenta"
End Function

Private Function fSpParametersBalanceGyPCostoVenta() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @DateFormat " & gUtilSQL.getVarCharTypeForDB(3) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @CuentaUsadaPara " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @FechaDesde  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaHasta  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaCorte  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @MostrarInforme  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   fSpParametersBalanceGyPCostoVenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersBalanceGyPCostoVenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpBalanceGyPCostoVenta() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   SET DATEFORMAT @DateFormat " & vbCrLf
   vResult = vResult & "   SELECT " & vbCrLf
   vResult = vResult & "   CodigoCuenta, " & vbCrLf
   vResult = vResult & "   DescripcionCuenta, " & vbCrLf
   vResult = vResult & "   SUM(Saldo) As Monto1 " & vbCrLf
   vResult = vResult & "   FROM IGV_CuentasInformeDeBalanceYResultado " & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo " & vbCrLf
   vResult = vResult & "   AND TipoCuenta = '4'" & vbCrLf
   vResult = vResult & "   AND Fecha BETWEEN @FechaDesde AND @FechaHasta" & vbCrLf
   vResult = vResult & "   AND GeneradoPor <> '3'" & vbCrLf
   vResult = vResult & "   GROUP BY CodigoCuenta, DescripcionCuenta " & vbCrLf
   vResult = vResult & "   ORDER BY CodigoCuenta " & vbCrLf
   vResult = vResult & "END"
   fStatementsSpBalanceGyPCostoVenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpBalanceGyPCostoVenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameEsquemaBalanceCuentas() As String
   GetSpNameEsquemaBalanceCuentas = "EsquemaBalanceCuentas"
End Function

Private Function fSpParametersEsquemaBalanceCuentas() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoMultiArchivo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @SqlWhere " & gUtilSQL.getVarCharTypeForDB(6000) & vbCrLf
   fSpParametersEsquemaBalanceCuentas = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersEsquemaBalanceCuentas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpEsquemaBalanceCuentas() As String
   Dim vResult As String
   
   On Error GoTo h_ERROR
   
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "     DECLARE @Query varchar(max)"
   vResult = vResult & "" & vbCrLf
   vResult = vResult & "     SET @Query = 'SELECT " & vbCrLf
   vResult = vResult & "         Consecutivo," & vbCrLf
   vResult = vResult & "         CodigoCuenta," & vbCrLf
   vResult = vResult & "         DescripcionCuenta," & vbCrLf
   vResult = vResult & "         NivelDetalle," & vbCrLf
   vResult = vResult & "         Naturaleza," & vbCrLf
   vResult = vResult & "         DescripcionDefCuenta" & vbCrLf
   vResult = vResult & "     FROM IGV_CuentaDescripcionBalance_B1' " & vbCrLf
   vResult = vResult & "" & vbCrLf
   vResult = vResult & "     IF(NOT @SqlWhere IS NULL ) " & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "         SET @Query = @Query + ' WHERE ' + @SqlWhere + ' ORDER BY CodigoCuenta, ColumnaDeOrden'" & vbCrLf
   vResult = vResult & "     END" & vbCrLf
   vResult = vResult & "" & vbCrLf
   vResult = vResult & "     EXECUTE(@Query)" & vbCrLf
   vResult = vResult & "END"
   fStatementsSpEsquemaBalanceCuentas = vResult
   
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpEsquemaBalanceCuentas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnMontoResultadoDelEjercicio(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameMontoResultadoDelEjercicio(), fFnParametersMontoResultadoDelEjercicio(), fFnScalarReturnDataTypeMontoResultadoDelEjercicio(), fStatementsFnMontoResultadoDelEjercicio(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnMontoResultadoDelEjercicio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnAdjustCode", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnMontoResultadoDelEjercicio(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameMontoResultadoDelEjercicio(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnMontoResultadoDelEjercicio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnAdjustCode", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameMontoResultadoDelEjercicio() As String
   GetFnNameMontoResultadoDelEjercicio = "MontoResultadoDelEjercicio"
End Function

Private Function fFnParametersMontoResultadoDelEjercicio() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "   @ConsecutivoPeriodo  " & gUtilSQL.getNumericTypeForDB(9, 0) & " ," & vbCrLf
   vResult = vResult & "   @FechaInicioPeriodo   " & gUtilSQL.getDateTypeForDB() & " ," & vbCrLf
   vResult = vResult & "   @FechaCorte   " & gUtilSQL.getDateTypeForDB() & " ," & vbCrLf
   vResult = vResult & "   @FechaCierrePeriodo   " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   fFnParametersMontoResultadoDelEjercicio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersMontoResultadoDelEjercicio", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeMontoResultadoDelEjercicio() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = gUtilSQL.getNumericTypeForDB(9, 2)
   fFnScalarReturnDataTypeMontoResultadoDelEjercicio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnScalarReturnDataTypeMontoResultadoDelEjercicio", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsFnMontoResultadoDelEjercicio() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "    DECLARE @Result Money" & vbCrLf
   vResult = vResult & "    DECLARE @ResultIngresos Money" & vbCrLf
   vResult = vResult & "    DECLARE @ResultEgresos Money" & vbCrLf
   vResult = vResult & "    DECLARE @ResultCostos Money" & vbCrLf
   vResult = vResult & "    DECLARE @ResultReservas Money" & vbCrLf
   vResult = vResult & "    DECLARE @CuentaResultadoEjercicio  " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    EXEC @CuentaResultadoEjercicio = Gf_CuentaResultadoEjercicio @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    SELECT @ResultIngresos = SUM(Saldo) FROM dbo.IGV_CuentasInformeDeBalanceYResultado WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND TipoCuenta = '3' AND Fecha BETWEEN @FechaInicioPeriodo AND @FechaCorte" & vbCrLf
   vResult = vResult & "    SELECT @ResultEgresos = SUM(Saldo) FROM dbo.IGV_CuentasInformeDeBalanceYResultado WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND TipoCuenta = '5' AND Fecha BETWEEN @FechaInicioPeriodo AND @FechaCorte" & vbCrLf
   vResult = vResult & "    SELECT @ResultCostos = SUM(Saldo) FROM dbo.IGV_CuentasInformeDeBalanceYResultado WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND TipoCuenta = '4' AND Fecha BETWEEN @FechaInicioPeriodo AND @FechaCorte" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    IF (@ResultIngresos)IS NULL" & vbCrLf
   vResult = vResult & "    BEGIN" & vbCrLf
   vResult = vResult & "        SET @ResultIngresos = 0" & vbCrLf
   vResult = vResult & "    END" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    IF (@ResultEgresos)IS NULL" & vbCrLf
   vResult = vResult & "    BEGIN" & vbCrLf
   vResult = vResult & "        SET @ResultEgresos = 0" & vbCrLf
   vResult = vResult & "    END" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    IF (@ResultCostos)IS NULL" & vbCrLf
   vResult = vResult & "    BEGIN" & vbCrLf
   vResult = vResult & "        SET @ResultCostos = 0" & vbCrLf
   vResult = vResult & "    END" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    SET @Result = (@ResultIngresos + @ResultCostos)" & vbCrLf
   vResult = vResult & "    SET @Result = (@Result + @ResultEgresos)" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    IF (@FechaCorte = @FechaCierrePeriodo)" & vbCrLf
   vResult = vResult & "    BEGIN" & vbCrLf
   vResult = vResult & "       SELECT @ResultReservas = SUM(MontoDebe - MontoHaber)" & vbCrLf
   vResult = vResult & "       FROM ASIENTO INNER JOIN COMPROBANTE ON ASIENTO.ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "       AND ASIENTO.NumeroComprobante = COMPROBANTE.Numero" & vbCrLf
   vResult = vResult & "       INNER JOIN CUENTA ON ASIENTO.ConsecutivoPeriodo = CUENTA.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "       AND ASIENTO.CodigoCuenta = CUENTA.Codigo" & vbCrLf
   vResult = vResult & "       WHERE CUENTA.Codigo <> @CuentaResultadoEjercicio" & vbCrLf
   vResult = vResult & "       AND COMPROBANTE.GeneradoPor = '8' AND COMPROBANTE.ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "    END" & vbCrLf
   vResult = vResult & "    ELSE" & vbCrLf
   vResult = vResult & "    BEGIN" & vbCrLf
   vResult = vResult & "       SET @ResultReservas = 0" & vbCrLf
   vResult = vResult & "    END" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    IF (@ResultReservas)IS NULL" & vbCrLf
   vResult = vResult & "    BEGIN" & vbCrLf
   vResult = vResult & "        SET @ResultReservas = 0" & vbCrLf
   vResult = vResult & "    END" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "    SET @Result = (@Result - @ResultReservas)" & vbCrLf
   vResult = vResult & "    RETURN @Result" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsFnMontoResultadoDelEjercicio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnMontoResultadoDelEjercicio", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function fCreateFnCuentaResultadoEjercicio(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCuentaResultadoEjercicio(), fFnParametersCuentaResultadoEjercicio(), fFnScalarReturnDataTypeCuentaResultadoEjercicio(), fStatementsFnCuentaResultadoEjercicio(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCuentaResultadoEjercicio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCuentaResultadoEjercicio", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCuentaResultadoEjercicio(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCuentaResultadoEjercicio(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCuentaResultadoEjercicio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCuentaResultadoEjercicio", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCuentaResultadoEjercicio() As String
   GetFnNameCuentaResultadoEjercicio = "CuentaResultadoEjercicio"
End Function

Private Function fFnParametersCuentaResultadoEjercicio() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "   @ConsecutivoPeriodo  " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   fFnParametersCuentaResultadoEjercicio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersCuentaResultadoEjercicio", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeCuentaResultadoEjercicio() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = gUtilSQL.getVarCharTypeForDB(30)
   fFnScalarReturnDataTypeCuentaResultadoEjercicio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnScalarReturnDataTypeCuentaResultadoEjercicio", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsFnCuentaResultadoEjercicio() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "    DECLARE @Result varchar (30)" & vbCrLf
   vResult = vResult & "    SELECT @Result = CierreDelEjercicio FROM PERIODO WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "    RETURN @Result" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsFnCuentaResultadoEjercicio = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCuentaResultadoEjercicio", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function fCreateFnDescripcionCuenta(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameDescripcionCuenta(), fFnParametersDescripcionCuenta(), fFnScalarReturnDataTypeDescripcionCuenta(), fStatementsFnDescripcionCuenta(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnDescripcionCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnDescripcionCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnDescripcionCuenta(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameDescripcionCuenta(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnDescripcionCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnDescripcionCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameDescripcionCuenta() As String
   GetFnNameDescripcionCuenta = "DescripcionCuenta"
End Function

Private Function fFnParametersDescripcionCuenta() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "   @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "   @CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   fFnParametersDescripcionCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersDescripcionCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeDescripcionCuenta() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = gUtilSQL.getVarCharTypeForDB(40)
   fFnScalarReturnDataTypeDescripcionCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnScalarReturnDataTypeDescripcionCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsFnDescripcionCuenta() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "    DECLARE @Result varchar (40)" & vbCrLf
   vResult = vResult & "    SELECT @Result = Descripcion FROM CUENTA WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @CodigoCuenta" & vbCrLf
   vResult = vResult & "    RETURN @Result" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsFnDescripcionCuenta = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnDescripcionCuenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameBalanceGyPAsientosDeReserva() As String
   GetSpNameBalanceGyPAsientosDeReserva = "BalanceGyPAsientosDeReserva"
End Function

Private Function fSpParametersBalanceGyPAsientosDeReserva() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @DateFormat " & gUtilSQL.getVarCharTypeForDB(3) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @CuentaUsadaPara " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @FechaDesde  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaHasta  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaCorte  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @MostrarInforme  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   fSpParametersBalanceGyPAsientosDeReserva = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersBalanceGyPAsientosDeReserva", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpBalanceGyPAsientosDeReserva() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @CuentaResultadoEjercicio " & gUtilSQL.getVarCharTypeForDB(30) & "" & vbCrLf
   vResult = vResult & "   DECLARE @FechaCierrePeriodo " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "   EXEC @CuentaResultadoEjercicio = Gf_CuentaResultadoEjercicio @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "   SELECT @FechaCierrePeriodo = FechaCierreDelPeriodo FROM PERIODO WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & vbCrLf
   vResult = vResult & "   IF (EXISTS(SELECT Numero FROM COMPROBANTE WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND GeneradoPor = " & "'8'" & ")) AND (@FechaCorte = @FechaCierrePeriodo)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT CUENTA.Codigo AS CodigoCuenta," & vbCrLf
   vResult = vResult & "      CUENTA.Descripcion AS DescripcionCuenta," & vbCrLf
   vResult = vResult & "      ((MontoDebe - MontoHaber) * -1) AS Monto1" & vbCrLf
   vResult = vResult & "      FROM ASIENTO" & vbCrLf
   vResult = vResult & "      INNER JOIN COMPROBANTE" & vbCrLf
   vResult = vResult & "      ON ASIENTO.ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "      AND ASIENTO.NumeroComprobante = COMPROBANTE.Numero" & vbCrLf
   vResult = vResult & "      INNER JOIN CUENTA" & vbCrLf
   vResult = vResult & "      ON ASIENTO.ConsecutivoPeriodo = CUENTA.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "      AND ASIENTO.CodigoCuenta = CUENTA.Codigo" & vbCrLf
   vResult = vResult & "      WHERE CUENTA.Codigo <> @CuentaResultadoEjercicio" & vbCrLf
   vResult = vResult & "      AND COMPROBANTE.GeneradoPor = " & "'8'" & vbCrLf
   vResult = vResult & "      AND COMPROBANTE.ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpBalanceGyPAsientosDeReserva = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpBalanceGyPAsientosDeReserva", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameBalanceGyPAsientosDeCierre() As String
   GetSpNameBalanceGyPAsientosDeCierre = "BalanceGyPAsientosDeCierre"
End Function

Private Function fSpParametersBalanceGyPAsientosDeCierre() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @DateFormat " & gUtilSQL.getVarCharTypeForDB(3) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(10, 0) & "," & vbCrLf
   vResult = vResult & "      @CuentaUsadaPara " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @FechaDesde  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaHasta  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @FechaCorte  " & gUtilSQL.getDateTypeForDB() & "," & vbCrLf
   vResult = vResult & "      @MostrarInforme  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   fSpParametersBalanceGyPAsientosDeCierre = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersBalanceGyPAsientosDeCierre", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpBalanceGyPAsientosDeCierre() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   SET DATEFORMAT @DateFormat " & vbCrLf
   vResult = vResult & "   SELECT " & vbCrLf
   vResult = vResult & "   CodigoCuenta, " & vbCrLf
   vResult = vResult & "   DescripcionCuenta, " & vbCrLf
   vResult = vResult & "   SUM(Saldo) As Monto1 " & vbCrLf
   vResult = vResult & "   FROM IGV_CuentasInformeDeBalanceYResultado " & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo " & vbCrLf
   vResult = vResult & "   AND TipoCuenta = " & gUtilSQL.fSimpleSqlValue("OTROS") & vbCrLf
   vResult = vResult & "   AND Fecha <= @FechaCorte " & vbCrLf
   vResult = vResult & "   GROUP BY CodigoCuenta, DescripcionCuenta " & vbCrLf
   vResult = vResult & "   ORDER BY CodigoCuenta " & vbCrLf
   vResult = vResult & "END"
   fStatementsSpBalanceGyPAsientosDeCierre = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpBalanceGyPAsientosDeCierre", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


