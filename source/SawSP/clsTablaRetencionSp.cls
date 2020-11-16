VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsTablaRetencionSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsTablaRetencionSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsTablaRetencionSp"
End Function

Public Function fCreateDropTablaRetencionSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpINS(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnCodigoRetencionUsualS10(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropFnCodigoRetencionUsualS10(valShowMessage)
      vResult = vResult And fDropSpINS(valShowMessage)
   End If
   fCreateDropTablaRetencionSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropTablaRetencionSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameINS = "TablaRetencionINS"
End Function

Private Function fSpParametersINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @TipoDePersona " & gUtilSQL.getCharTypeForDB(1) & "," & vbCrLf
   vResult = vResult & "      @Codigo " & gUtilSQL.getVarCharTypeForDB(6) & "," & vbCrLf
   vResult = vResult & "      @TipoDePago " & gUtilSQL.getVarCharTypeForDB(50) & " = ''," & vbCrLf
   vResult = vResult & "      @Comentarios " & gUtilSQL.getVarCharTypeForDB(255) & " = ''," & vbCrLf
   vResult = vResult & "      @BaseImponible " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0," & vbCrLf
   vResult = vResult & "      @Tarifa " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0," & vbCrLf
   vResult = vResult & "      @ParaPagosMayoresDe " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0," & vbCrLf
   vResult = vResult & "      @FechaAplicacion " & gUtilSQL.getDateTypeForDB() & " = ''," & vbCrLf
   vResult = vResult & "      @SustraENDo " & gUtilSQL.getNumericTypeForDB(12, 2) & " = 0," & vbCrLf
   vResult = vResult & "      @AcumulaParaPJND " & gUtilSQL.getCharTypeForDB(1) & " = 'N'" & vbCrLf
    fSpParametersINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   INSERT INTO TablaRetencion(" & vbCrLf
   vResult = vResult & "         TipoDePersona," & vbCrLf
   vResult = vResult & "         Codigo," & vbCrLf
   vResult = vResult & "         TipoDePago," & vbCrLf
   vResult = vResult & "         Comentarios," & vbCrLf
   vResult = vResult & "         BaseImponible," & vbCrLf
   vResult = vResult & "         Tarifa," & vbCrLf
   vResult = vResult & "         ParaPagosMayoresDe," & vbCrLf
   vResult = vResult & "         FechaAplicacion," & vbCrLf
   vResult = vResult & "         SustraENDo," & vbCrLf
   vResult = vResult & "         AcumulaParaPJND)" & vbCrLf
   vResult = vResult & "      VALUES(" & vbCrLf
   vResult = vResult & "         @TipoDePersona," & vbCrLf
   vResult = vResult & "         @Codigo," & vbCrLf
   vResult = vResult & "         @TipoDePago," & vbCrLf
   vResult = vResult & "         @Comentarios," & vbCrLf
   vResult = vResult & "         @BaseImponible," & vbCrLf
   vResult = vResult & "         @Tarifa," & vbCrLf
   vResult = vResult & "         @ParaPagosMayoresDe," & vbCrLf
   vResult = vResult & "         @FechaAplicacion," & vbCrLf
   vResult = vResult & "         @SustraENDo," & vbCrLf
   vResult = vResult & "         @AcumulaParaPJND)" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnCodigoRetencionUsualS10(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameCodigoRetencionUsualS10(), fFnParametersCodigoRetencionUsualS10(), fFnScalarReturnDataTypeCodigoRetencionUsualS10(), fStatementsFnCodigoRetencionUsualS10(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnCodigoRetencionUsualS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnCodigoRetencionUsualS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnCodigoRetencionUsualS10(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameCodigoRetencionUsualS10(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnCodigoRetencionUsualS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnCodigoRetencionUsualS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameCodigoRetencionUsualS10() As String
   GetFnNameCodigoRetencionUsualS10 = "CodRetUsualS10"
End Function

Private Function fFnParametersCodigoRetencionUsualS10() As String
   fFnParametersCodigoRetencionUsualS10 = ""
End Function

Private Function fFnScalarReturnDataTypeCodigoRetencionUsualS10() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = gUtilSQL.getVarCharTypeForDB(6)
   fFnScalarReturnDataTypeCodigoRetencionUsualS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnScalarReturnDataTypeCodigoRetencionUsualS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsFnCodigoRetencionUsualS10() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @CodigoRet " & gUtilSQL.getVarCharTypeForDB(6) & vbCrLf
   vResult = vResult & "   SET @CodigoRet = 'NORET'" & vbCrLf
   vResult = vResult & "   RETURN @CodigoRet" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnCodigoRetencionUsualS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnCodigoRetencionUsualS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *

