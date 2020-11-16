VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsAsientoSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsAsientoSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsAsientoSp"
End Function

Public Function fCreateDropAsientoSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpINS(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpS10ComprobanteAsientoSEL(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnGenConsecutivo(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpCuadrarUPD(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpS10ComprobanteAsientoSEL(valShowMessage)
      vResult = vResult And fDropSpINS(valShowMessage)
      vResult = vResult And fDropFnGenConsecutivo(valShowMessage)
      vResult = vResult And fDropSpCuadrarUPD(valShowMessage)
   End If
   fCreateDropAsientoSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropAsientoSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpINS(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameINS(), fSpParametersINS(), fStatementsSpINS(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpINS(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameINS(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameINS() As String
   GetSpNameINS = "AsientoINS"
End Function

Private Function fSpParametersINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "         @ConsecutivoAsiento " & gUtilSQL.getNumericTypeForDB(9, 0) & ", " & vbCrLf
   vResult = vResult & "         @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & ", " & vbCrLf
   vResult = vResult & "         @FechaComprobante " & gUtilSQL.getDateTypeForDB() & ", " & vbCrLf
   vResult = vResult & "         @CodigoCuenta " & gUtilSQL.getVarCharTypeForDB(30) & ", " & vbCrLf
   vResult = vResult & "         @MontoDebe " & gUtilSQL.getNumericTypeForDB(12, 2) & ", " & vbCrLf
   vResult = vResult & "         @MontoHaber " & gUtilSQL.getNumericTypeForDB(12, 2) & ", " & vbCrLf
   vResult = vResult & "         @Descripcion " & gUtilSQL.getVarCharTypeForDB(160) & ", " & vbCrLf
   vResult = vResult & "         @FechaDeReferencia " & gUtilSQL.getDateTypeForDB() & ", " & vbCrLf
   vResult = vResult & "         @NumeroReferencia " & gUtilSQL.getVarCharTypeForDB(16) & ", " & vbCrLf
   vResult = vResult & "         @CentroDeCostos " & gUtilSQL.getVarCharTypeForDB(5) & ", " & vbCrLf
   vResult = vResult & "         @Auxiliar " & gUtilSQL.getVarCharTypeForDB(10) & ", " & vbCrLf
   vResult = vResult & "         @ConciliadoSn " & gUtilSQL.getCharTypeForDB(1) & ", " & vbCrLf
   vResult = vResult & "         @FechaEnQueSeConcilio " & gUtilSQL.getDateTypeForDB() & ", " & vbCrLf
   vResult = vResult & "         @NroConciliacion " & gUtilSQL.getVarCharTypeForDB(9) & ", " & vbCrLf
   vResult = vResult & "         @Contabilizado " & gUtilSQL.getCharTypeForDB(1) & ", " & vbCrLf
   vResult = vResult & "         @AfectaEfectivo " & gUtilSQL.getCharTypeForDB(1) & ", " & vbCrLf
   vResult = vResult & "         @EsAsientoDeActivoFijo " & gUtilSQL.getCharTypeForDB(1) & ", " & vbCrLf
   vResult = vResult & "         @TipoDeAsiento " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
    fSpParametersINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "      INSERT INTO ASIENTO" & vbCrLf
   vResult = vResult & "               (ConsecutivoPeriodo, " & vbCrLf
   vResult = vResult & "               ConsecutivoAsiento, " & vbCrLf
   vResult = vResult & "               NumeroComprobante, " & vbCrLf
   vResult = vResult & "               FechaComprobante, " & vbCrLf
   vResult = vResult & "               CodigoCuenta, " & vbCrLf
   vResult = vResult & "               MontoDebe, " & vbCrLf
   vResult = vResult & "               MontoHaber, " & vbCrLf
   vResult = vResult & "               Descripcion, " & vbCrLf
   vResult = vResult & "               FechaDeReferencia, " & vbCrLf
   vResult = vResult & "               NumeroReferencia, " & vbCrLf
   vResult = vResult & "               CentroDeCostos, " & vbCrLf
   vResult = vResult & "               Auxiliar, " & vbCrLf
   vResult = vResult & "               ConciliadoSn, " & vbCrLf
   vResult = vResult & "               FechaEnQueSeConcilio, " & vbCrLf
   vResult = vResult & "               NroConciliacion, " & vbCrLf
   vResult = vResult & "               Contabilizado, " & vbCrLf
   vResult = vResult & "               AfectaEfectivo, " & vbCrLf
   vResult = vResult & "               EsAsientoDeActivoFijo, " & vbCrLf
   vResult = vResult & "               TipoDeAsiento)" & vbCrLf
   vResult = vResult & "          VALUES" & vbCrLf
   vResult = vResult & "               (@ConsecutivoPeriodo, " & vbCrLf
   vResult = vResult & "               @ConsecutivoAsiento, " & vbCrLf
   vResult = vResult & "               @NumeroComprobante, " & vbCrLf
   vResult = vResult & "               @FechaComprobante, " & vbCrLf
   vResult = vResult & "               @CodigoCuenta, " & vbCrLf
   vResult = vResult & "               @MontoDebe, " & vbCrLf
   vResult = vResult & "               @MontoHaber, " & vbCrLf
   vResult = vResult & "               @Descripcion, " & vbCrLf
   vResult = vResult & "               @FechaDeReferencia, " & vbCrLf
   vResult = vResult & "               @NumeroReferencia, " & vbCrLf
   vResult = vResult & "               @CentroDeCostos, " & vbCrLf
   vResult = vResult & "               @Auxiliar, " & vbCrLf
   vResult = vResult & "               @ConciliadoSn, " & vbCrLf
   vResult = vResult & "               @FechaEnQueSeConcilio, " & vbCrLf
   vResult = vResult & "               @NroConciliacion, " & vbCrLf
   vResult = vResult & "               @Contabilizado, " & vbCrLf
   vResult = vResult & "               @AfectaEfectivo, " & vbCrLf
   vResult = vResult & "               @EsAsientoDeActivoFijo, " & vbCrLf
   vResult = vResult & "               @TipoDeAsiento)" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpS10ComprobanteAsientoSEL(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameS10ComprobanteAsientoSEL(), fSpParametersS10ComprobanteAsientoSEL(), fStatementsSpS10ComprobanteAsientoSEL(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpS10ComprobanteAsientoSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpS10ComprobanteAsientoSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpS10ComprobanteAsientoSEL(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameS10ComprobanteAsientoSEL(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpS10ComprobanteAsientoSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpS10ComprobanteAsientoSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameS10ComprobanteAsientoSEL() As String
   GetSpNameS10ComprobanteAsientoSEL = "S10_ComprobanteAsientoSEL"
End Function

Private Function fSpParametersS10ComprobanteAsientoSEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
    fSpParametersS10ComprobanteAsientoSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersS10ComprobanteAsientoSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpS10ComprobanteAsientoSEL() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   IF NOT EXISTS(SELECT ConsecutivoCompania FROM PERIODO WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      RAISERROR('No existe Período con Consecutivo Período:%d', 16, 1, @ConsecutivoPeriodo)" & vbCrLf
   vResult = vResult & "      RETURN 0" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT" & vbCrLf
   vResult = vResult & "            COMPROBANTE.ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "            COMPROBANTE.Numero," & vbCrLf
   vResult = vResult & "            COMPROBANTE.Status," & vbCrLf
   vResult = vResult & "            COMPROBANTE.Fecha," & vbCrLf
   vResult = vResult & "            COMPROBANTE.Descripcion AS DescripcionComprobante," & vbCrLf
   vResult = vResult & "            COMPROBANTE.TotalDebe," & vbCrLf
   vResult = vResult & "            COMPROBANTE.TotalHaber," & vbCrLf
   vResult = vResult & "            ASIENTO.ConsecutivoAsiento," & vbCrLf
   vResult = vResult & "            ASIENTO.CodigoCuenta," & vbCrLf
   vResult = vResult & "            ASIENTO.MontoDebe," & vbCrLf
   vResult = vResult & "            ASIENTO.MontoHaber," & vbCrLf
   vResult = vResult & "            ASIENTO.Descripcion AS DescripcionAsiento" & vbCrLf
   vResult = vResult & "         FROM COMPROBANTE INNER JOIN ASIENTO" & vbCrLf
   vResult = vResult & "            ON COMPROBANTE.ConsecutivoPeriodo = ASIENTO.ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "            AND COMPROBANTE.Numero = ASIENTO.NumeroComprobante" & vbCrLf
   vResult = vResult & "         WHERE COMPROBANTE.ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "            AND COMPROBANTE.Numero = @NumeroComprobante" & vbCrLf
   vResult = vResult & "      RETURN @@ROWCOUNT" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpS10ComprobanteAsientoSEL = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpS10ComprobanteAsientoSEL", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnGenConsecutivo(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameGenConsecutivo, fFnParametersGenConsecutivo, fFnScalarReturnDataTypeGenConsecutivo, fStatementsFnGenConsecutivo, valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnGenConsecutivo = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnGenConsecutivo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnGenConsecutivo(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameGenConsecutivo, valShowMessage)
   Set insLibSf = Nothing
   fDropFnGenConsecutivo = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpGenConsecutivo(@ConsecutivoPeriodo int, @NumeroComprobante varchar(8))", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeGenConsecutivo() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = gUtilSQL.getNumericTypeForDB(9, 0)
   fFnScalarReturnDataTypeGenConsecutivo = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnScalarReturnDataTypeGeneraNumero", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameGenConsecutivo() As String
   GetFnNameGenConsecutivo = "AsientoGenConsecutivo"
End Function

Private Function fFnParametersGenConsecutivo() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "   @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & ",   @NumeroComprobante " & gUtilSQL.getVarCharTypeForDB(8) & vbCrLf
h_EXIT:  On Error GoTo 0
   fFnParametersGenConsecutivo = vResult
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersGenConsecutivo(@ConsecutivoPeriodo int, @NumeroComprobante varchar(8))", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsFnGenConsecutivo() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @Result " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @MaxConsecutivoAsiento " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   SET @Result = 1" & vbCrLf
   vResult = vResult & "   SELECT @MaxConsecutivoAsiento = ConsecutivoAsiento FROM Asiento WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND NumeroComprobante = @NumeroComprobante" & vbCrLf
   vResult = vResult & "   IF (NOT @MaxConsecutivoAsiento IS NULL)" & vbCrLf
   vResult = vResult & "      IF (@MaxConsecutivoAsiento > 0)" & vbCrLf
   vResult = vResult & "        BEGIN" & vbCrLf
   vResult = vResult & "            SET @MaxConsecutivoAsiento = @MaxConsecutivoAsiento + 1" & vbCrLf
   vResult = vResult & "            SET @Result = @MaxConsecutivoAsiento" & vbCrLf
   vResult = vResult & "END "
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnGenConsecutivo = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpGenConsecutivo(@ConsecutivoPeriodo int, @NumeroComprobante varchar(8))", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpCuadrarUPD(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameCuadrarUPD(), fSpParametersCuadrarUPD(), fStatementsSpCuadrarUPD(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpCuadrarUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpCuadrarUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpCuadrarUPD(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameCuadrarUPD(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpCuadrarUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpCuadrarUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameCuadrarUPD() As String
   GetSpNameCuadrarUPD = "AsientoCuadrarUPD"
End Function

Private Function fSpParametersCuadrarUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "          @ConsecutivoPeriodo  " & gUtilSQL.getNumericTypeForDB(9, 0) & " ," & vbCrLf
   vResult = vResult & "         @NumeroComprobante  " & gUtilSQL.getVarCharTypeForDB(8) & ", " & vbCrLf
   vResult = vResult & "         @Contabilizado  " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
    fSpParametersCuadrarUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersCuadrarUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpCuadrarUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   UPDATE ASIENTO" & vbCrLf
   vResult = vResult & "   SET Contabilizado = @Contabilizado" & vbCrLf
   vResult = vResult & "   WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo" & vbCrLf
   vResult = vResult & "   AND NumeroComprobante = @NumeroComprobante" & vbCrLf
   vResult = vResult & "END "
   fStatementsSpCuadrarUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpCuadrarUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
