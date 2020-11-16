VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsAnticipoSP"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsAnticipoSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsAnticipoSp"
End Function

Public Function fCreateDropAnticipoSp(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpGenerarAnticipoCobrado(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpAnticipoCobradoCorregirStatusYMontoAbonado(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpAnticipoPagadoCorregirStatusYMontoAbonado(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpGenerarAnticipoCobrado(valShowMessage)
      vResult = vResult And fDropSpAnticipoCobradoCorregirStatusYMontoAbonado(valShowMessage)
      vResult = vResult And fDropSpAnticipoPagadoCorregirStatusYMontoAbonado(valShowMessage)
   End If
  fCreateDropAnticipoSp = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropAnticipoSp", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpGenerarAnticipoCobrado(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameGenerarAnticipoCobrado(), fSpParametersSpGenerarAnticipoCobrado(), fStatementsSpGenerarAnticipoCobrado(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpGenerarAnticipoCobrado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpGenerarAnticipoCobrado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpGenerarAnticipoCobrado(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameGenerarAnticipoCobrado(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpGenerarAnticipoCobrado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpGenerarAnticipoCobrado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameGenerarAnticipoCobrado() As String
   GetSpNameGenerarAnticipoCobrado = "GenerarAnticipoCobrado"
End Function

Private Function fSpParametersSpGenerarAnticipoCobrado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "   @ConsecutivoCompania  " & gUtilSQL.getNumericTypeForDB(9, 0) & " ," & vbCrLf
   vResult = vResult & "   @NumeroCobranza  " & gUtilSQL.getVarCharTypeForDB(20) & "  = ''," & vbCrLf
   vResult = vResult & "   @CodigoCliente  " & gUtilSQL.getVarCharTypeForDB(10) & "  = ''," & vbCrLf
   vResult = vResult & "   @MontoCobranza AS Money," & vbCrLf
   vResult = vResult & "   @NumeroAnticipo " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "   @SimboloMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " = ''," & vbCrLf
   vResult = vResult & "   @Secuencial " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
    fSpParametersSpGenerarAnticipoCobrado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersSpGenerarAnticipoCobrado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpGenerarAnticipoCobrado() As String
   Dim vResult As String
   On Error GoTo h_ERROR

   vResult = vResult & "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @StatusCxC AS CHAR(1)" & vbCrLf
   vResult = vResult & "   DECLARE @MontoAbonadoTotal AS MONEY" & vbCrLf
   vResult = vResult & "   DECLARE @MontoTotalCxC AS MONEY" & vbCrLf
   vResult = vResult & "   DECLARE @Aplicar AS VARCHAR(2)" & vbCrLf
   vResult = vResult & "   DECLARE @MontoOriginalAnticipo AS MONEY" & vbCrLf
   vResult = vResult & "   DECLARE @MontoUsadoAnticipo AS MONEY" & vbCrLf
   vResult = vResult & "   DECLARE @ConsecutivoAnticipo " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CodigoMoneda AS " & gUtilSQL.getVarCharTypeForDB(4) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Cambio AS MONEY" & vbCrLf
   vResult = vResult & "   DECLARE @MontoUsadoActualAnticipo AS MONEY" & vbCrLf
   vResult = vResult & "   DECLARE @MontoCobranzaActualAnticipo AS MONEY" & vbCrLf
   vResult = vResult & "   DECLARE @MontoCobradoAnticipo AS MONEY" & vbCrLf
   vResult = vResult & "   SET @Aplicar = 'SI'" & vbCrLf
   vResult = vResult & "   SET @StatusCxC = '0'" & vbCrLf
   vResult = vResult & "   SET NOCOUNT OFF;" & vbCrLf
   vResult = vResult & "   IF" & vbCrLf
   vResult = vResult & "   EXISTS(" & vbCrLf
   vResult = vResult & "   SELECT Numero" & vbCrLf
   vResult = vResult & "        FROM anticipo WHERE Numero = @NumeroAnticipo AND ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "     SELECT @MontoOriginalAnticipo =  montototal," & vbCrLf
   vResult = vResult & "          @MontoUsadoAnticipo =  montousado," & vbCrLf
   vResult = vResult & "          @ConsecutivoAnticipo = consecutivoanticipo," & vbCrLf
   vResult = vResult & "        @CodigoMoneda =  CodigoMoneda," & vbCrLf
   vResult = vResult & "        @Cambio =  cambio" & vbCrLf
   vResult = vResult & "        FROM anticipo WHERE Numero = @NumeroAnticipo AND ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente" & vbCrLf
   vResult = vResult & "     INSERT INTO [dbo].[anticipoCobrado]" & vbCrLf
   vResult = vResult & "           (ConsecutivoCompania" & vbCrLf
   vResult = vResult & "           ,NumeroCobranza" & vbCrLf
   vResult = vResult & "           ,Secuencial" & vbCrLf
   vResult = vResult & "           ,ConsecutivoAnticipoUsado" & vbCrLf
   vResult = vResult & "           ,NumeroAnticipo" & vbCrLf
   vResult = vResult & "           ,MontoOriginal" & vbCrLf
   vResult = vResult & "           ,MontoRestanteAlDia" & vbCrLf
   vResult = vResult & "           ,SimboloMoneda" & vbCrLf
   vResult = vResult & "           ,CodigoMoneda" & vbCrLf
   vResult = vResult & "           ,Cambio" & vbCrLf
   vResult = vResult & "           ,MontoTotalDelAnticipo" & vbCrLf
   vResult = vResult & "           ,MontoAplicado)" & vbCrLf
   vResult = vResult & "     VALUES" & vbCrLf
   vResult = vResult & "           (@ConsecutivoCompania," & vbCrLf
   vResult = vResult & "            @NumeroCobranza," & vbCrLf
   vResult = vResult & "           @Secuencial , -- verificar en pruebas" & vbCrLf
   vResult = vResult & "           @ConsecutivoAnticipo," & vbCrLf
   vResult = vResult & "           @NumeroAnticipo," & vbCrLf
   vResult = vResult & "           @MontoOriginalAnticipo," & vbCrLf
   vResult = vResult & "           @MontoOriginalAnticipo - @MontoUsadoAnticipo," & vbCrLf
   vResult = vResult & "           @SimboloMoneda," & vbCrLf
   vResult = vResult & "           @CodigoMoneda," & vbCrLf
   vResult = vResult & "           @Cambio," & vbCrLf
   vResult = vResult & "           @MontoOriginalAnticipo - @MontoUsadoAnticipo," & vbCrLf
   vResult = vResult & "           @MontoCobranza)" & vbCrLf
   vResult = vResult & "       SET  @MontoUsadoActualAnticipo = @MontoCobranza + @MontoUsadoAnticipo" & vbCrLf
   vResult = vResult & "       IF (@MontoUsadoActualAnticipo =  0 )" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "          UPDATE ANTICIPO SET STATUS = '0' , MontoUsado = @MontoUsadoActualAnticipo" & vbCrLf
   vResult = vResult & "          WHERE Numero = @NumeroAnticipo AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         END" & vbCrLf
   vResult = vResult & "       IF (@MontoUsadoActualAnticipo >  0  AND @MontoUsadoActualAnticipo <  @MontoOriginalAnticipo)" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "          UPDATE ANTICIPO SET STATUS = '2' , MontoUsado = @MontoUsadoActualAnticipo" & vbCrLf
   vResult = vResult & "          WHERE Numero = @NumeroAnticipo AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         END" & vbCrLf
   vResult = vResult & "       IF (@MontoUsadoActualAnticipo =  @MontoOriginalAnticipo)" & vbCrLf
   vResult = vResult & "         BEGIN" & vbCrLf
   vResult = vResult & "          UPDATE ANTICIPO SET STATUS = '3' , MontoUsado = @MontoUsadoActualAnticipo" & vbCrLf
   vResult = vResult & "          WHERE Numero = @NumeroAnticipo AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "         END" & vbCrLf   
   vResult = vResult & "       SELECT @MontoCobranzaActualAnticipo =  CobradoAnticipo"
   vResult = vResult & "          FROM COBRANZA WHERE Numero = @NumeroCobranza  AND ConsecutivoCompania = @ConsecutivoCompania " & vbCrLf
   vResult = vResult & "       SET  @MontoCobradoAnticipo = @MontoCobranza + @MontoCobranzaActualAnticipo" & vbCrLf
   vResult = vResult & "       UPDATE COBRANZA SET CobradoAnticipo =@MontoCobradoAnticipo WHERE" & vbCrLf
   vResult = vResult & "       Numero =@NumeroCobranza AND ConsecutivoCompania = @ConsecutivoCompania " & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "  END" & vbCrLf
   fStatementsSpGenerarAnticipoCobrado = vResult
   
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpGenerarAnticipoCobrado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpAnticipoCobradoCorregirStatusYMontoAbonado(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameAnticipoCobradoCorregirStatusYMontoAbonado(), fSpParametersAnticipoCobradoCorregirStatusYMontoAbonado(), fStatementsSpAnticipoCobradoCorregirStatusYMontoAbonado(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpAnticipoCobradoCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpAnticipoCobradoCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpAnticipoCobradoCorregirStatusYMontoAbonado(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameAnticipoCobradoCorregirStatusYMontoAbonado(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpAnticipoCobradoCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpAnticipoCobradoCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameAnticipoCobradoCorregirStatusYMontoAbonado() As String
   GetSpNameAnticipoCobradoCorregirStatusYMontoAbonado = "AnticipoCobradoCorregirStatusYMontoAbonado"
End Function

Private Function fSpParametersAnticipoCobradoCorregirStatusYMontoAbonado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "@CodigoCliente " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "@ConsecutivoAnticipo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   fSpParametersAnticipoCobradoCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersAnticipoCobradoCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpAnticipoCobradoCorregirStatusYMontoAbonado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   
   vResult = vResult & "   DECLARE @TotalAnticipos AS Money" & vbCrLf
   vResult = vResult & "   DECLARE @TotalAnticipoAbonado AS Money" & vbCrLf
   vResult = vResult & "   SET @TotalAnticipos =0" & vbCrLf
   vResult = vResult & "   SET NOCOUNT OFF" & vbCrLf
   vResult = vResult & "   If Exists(Select @ConsecutivoAnticipo FROM anticipo WHERE ConsecutivoAnticipo=@ConsecutivoAnticipo AND ConsecutivoCompania=ConsecutivoCompania AND CodigoCliente=@CodigoCliente)" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SELECT @TotalAnticipos= CASE WHEN SUM(dbo.AnticipoCobrado.MontoAplicado) Is Null then 0 else SUM(dbo.AnticipoCobrado.MontoAplicado) END" & vbCrLf
   vResult = vResult & "      FROM dbo.anticipoCobrado INNER JOIN dbo.anticipo ON anticipo.ConsecutivoCompania=anticipoCobrado.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "      anticipo.Numero=dbo.anticipoCobrado.NumeroAnticipo" & vbCrLf
   vResult = vResult & "      INNER JOIN dbo.Cobranza ON dbo.Cobranza.ConsecutivoCompania=dbo.anticipoCobrado.ConsecutivoCompania AND " & vbCrLf
   vResult = vResult & "      dbo.anticipoCobrado.NumeroCobranza=dbo.Cobranza.Numero " & vbCrLf
   vResult = vResult & "      WHERE dbo.anticipo.ConsecutivoCompania=@ConsecutivoCompania AND dbo.anticipo.ConsecutivoAnticipo=@ConsecutivoAnticipo" & vbCrLf
   vResult = vResult & "      AND dbo.anticipo.CodigoCliente=@CodigoCliente" & vbCrLf
   vResult = vResult & "      AND dbo.Cobranza.StatusCobranza<>'1' "
   vResult = vResult & "      SELECT @TotalAnticipoAbonado= dbo.anticipo.MontoTotal" & vbCrLf
   vResult = vResult & "      FROM anticipo WHERE dbo.anticipo.ConsecutivoCompania=@ConsecutivoCompania AND dbo.anticipo.ConsecutivoAnticipo=@ConsecutivoAnticipo" & vbCrLf
   vResult = vResult & "      AND dbo.anticipo.CodigoCliente=@CodigoCliente" & vbCrLf
   vResult = vResult & "      BEGIN" & vbCrLf
   vResult = vResult & "         IF (ABS(ABS( @TotalAnticipos) - ABS(@TotalAnticipoAbonado)) <= 0.01 AND ABS(ABS(@TotalAnticipos) - ABS(@TotalAnticipoAbonado))> 0 )" & vbCrLf
   vResult = vResult & "            SET @TotalAnticipos = @TotalAnticipoAbonado" & vbCrLf
   vResult = vResult & "            UPDATE anticipo SET MontoUsado =@TotalAnticipos WHERE" & vbCrLf
   vResult = vResult & "             ConsecutivoAnticipo =@ConsecutivoAnticipo AND ConsecutivoCompania = @ConsecutivoCompania  And" & vbCrLf
   vResult = vResult & "             CodigoCliente=@CodigoCliente" & vbCrLf
   vResult = vResult & "            IF (@TotalAnticipos=@TotalAnticipoAbonado)" & vbCrLf
   vResult = vResult & "             BEGIN" & vbCrLf
   vResult = vResult & "               UPDATE anticipo SET Status = '3' WHERE" & vbCrLf
   vResult = vResult & "               ConsecutivoAnticipo =@ConsecutivoAnticipo AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND CodigoCliente = @CodigoCliente" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "           ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "            IF (@TotalAnticipos  = 0 )" & vbCrLf
   vResult = vResult & "               UPDATE anticipo SET Status = '0' WHERE" & vbCrLf
   vResult = vResult & "               ConsecutivoAnticipo =@ConsecutivoAnticipo AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND CodigoCliente = @CodigoCliente" & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "               UPDATE anticipo SET Status = '2' WHERE" & vbCrLf
   vResult = vResult & "               ConsecutivoAnticipo =@ConsecutivoAnticipo AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND CodigoCliente = @CodigoCliente" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "      END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpAnticipoCobradoCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpAnticipoCobradoCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSpAnticipoPagadoCorregirStatusYMontoAbonado(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameAnticipoPagadoCorregirStatusYMontoAbonado(), fSpParametersAnticipoPagadoCorregirStatusYMontoAbonado(), fStatementsSpAnticipoPagadoCorregirStatusYMontoAbonado(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpAnticipoPagadoCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpAnticipoPagadoCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpAnticipoPagadoCorregirStatusYMontoAbonado(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameAnticipoPagadoCorregirStatusYMontoAbonado(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpAnticipoPagadoCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpAnticipoPagadoCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameAnticipoPagadoCorregirStatusYMontoAbonado() As String
   GetSpNameAnticipoPagadoCorregirStatusYMontoAbonado = "AnticipoPagadoCorregirStatusYMontoAbonado"
End Function

Private Function fSpParametersAnticipoPagadoCorregirStatusYMontoAbonado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = vResult & "@CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & "," & vbCrLf
   vResult = vResult & "@ConsecutivoAnticipo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   fSpParametersAnticipoPagadoCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersAnticipoPagadoCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpAnticipoPagadoCorregirStatusYMontoAbonado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   
   vResult = vResult & "DECLARE @TotalAnticipos AS Money" & vbCrLf
   vResult = vResult & "DECLARE @TotalAnticipoAbonado AS Money" & vbCrLf
   vResult = vResult & "SET @TotalANticipos =0" & vbCrLf
   vResult = vResult & "SET NOCOUNT OFF" & vbCrLf
   vResult = vResult & " IF Exists(Select @ConsecutivoAnticipo FROM anticipo WHERE ConsecutivoAnticipo=@ConsecutivoAnticipo AND ConsecutivoCompania=ConsecutivoCompania AND CodigoProveedor=@CodigoProveedor)" & vbCrLf
   vResult = vResult & "     BEGIN" & vbCrLf
   vResult = vResult & "     SELECT @TotalAnticipos= CASE WHEN SUM(dbo.anticipoPagado.MontoAplicado) Is Null then 0 else SUM(dbo.anticipoPagado.MontoAplicado) END" & vbCrLf
   vResult = vResult & "     FROM  dbo.anticipoPagado INNER JOIN dbo.anticipo ON anticipo.ConsecutivoCompania=anticipoPagado.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "     anticipo.Numero=dbo.anticipoPagado.NumeroAnticipo" & vbCrLf
   vResult = vResult & "     INNER JOIN dbo.Pago ON dbo.anticipoPagado.ConsecutivoCompania= dbo.Pago.ConsecutivoCompania AND " & vbCrLf
   vResult = vResult & "     dbo.anticipoPagado.NumeroComprobante= dbo.Pago.NumeroComprobante " & vbCrLf
   vResult = vResult & "     WHERE dbo.anticipo.ConsecutivoCompania=@ConsecutivoCompania AND dbo.anticipo.ConsecutivoAnticipo=@ConsecutivoAnticipo" & vbCrLf
   vResult = vResult & "     AND dbo.anticipo.CodigoProveedor=@CodigoProveedor" & vbCrLf
   vResult = vResult & "     AND dbo.Pago.StatusOrdenDePago<>'1' "
   vResult = vResult & "     SELECT @TotalAnticipoAbonado= dbo.anticipo.MontoTotal" & vbCrLf
   vResult = vResult & "     FROM anticipo WHERE dbo.anticipo.ConsecutivoCompania=@ConsecutivoCompania AND dbo.anticipo.ConsecutivoAnticipo=@ConsecutivoAnticipo" & vbCrLf
   vResult = vResult & "     AND dbo.anticipo.CodigoProveedor=@CodigoProveedor" & vbCrLf
   vResult = vResult & "       BEGIN" & vbCrLf
   vResult = vResult & "        IF (ABS(ABS( @TotalAnticipos) - ABS(@TotalAnticipoAbonado)) <= 0.01 AND ABS(ABS(@TotalAnticipos) - ABS(@TotalAnticipoAbonado))> 0 )" & vbCrLf
   vResult = vResult & "            SET @TotalAnticipos = @TotalAnticipoAbonado" & vbCrLf
   vResult = vResult & "            UPDATE anticipo SET MontoUsado =@TotalAnticipos WHERE" & vbCrLf
   vResult = vResult & "               ConsecutivoAnticipo=@ConsecutivoAnticipo AND ConsecutivoCompania = @ConsecutivoCompania  And" & vbCrLf
   vResult = vResult & "               CodigoProveedor=@CodigoProveedor" & vbCrLf
   vResult = vResult & "            IF (@TotalAnticipos=@TotalAnticipoAbonado)" & vbCrLf
   vResult = vResult & "               BEGIN" & vbCrLf
   vResult = vResult & "                 UPDATE anticipo SET Status = '3' WHERE" & vbCrLf
   vResult = vResult & "                 ConsecutivoAnticipo=@ConsecutivoAnticipo AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "                 AND CodigoProveedor = @CodigoProveedor" & vbCrLf
   vResult = vResult & "               END" & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "            BEGIN" & vbCrLf
   vResult = vResult & "            IF (@TotalAnticipos  = 0 )" & vbCrLf
   vResult = vResult & "               UPDATE anticipo SET Status = '0' WHERE" & vbCrLf
   vResult = vResult & "               ConsecutivoAnticipo=@ConsecutivoAnticipo AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND CodigoProveedor = @CodigoProveedor" & vbCrLf
   vResult = vResult & "            ELSE" & vbCrLf
   vResult = vResult & "               UPDATE anticipo SET Status = '2' WHERE" & vbCrLf
   vResult = vResult & "               ConsecutivoAnticipo=@ConsecutivoAnticipo AND ConsecutivoCompania = @ConsecutivoCompania" & vbCrLf
   vResult = vResult & "               AND CodigoProveedor = @CodigoProveedor" & vbCrLf
   vResult = vResult & "            END" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpAnticipoPagadoCorregirStatusYMontoAbonado = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpAnticipoPagadoCorregirStatusYMontoAbonado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

