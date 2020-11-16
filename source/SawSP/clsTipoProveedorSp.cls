VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsTipoProveedorSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsTipoProveedorSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsTipoProveedorSp"
End Function

Public Function fCreateDropTipoProveedorSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      'vResult = vResult And fCreateSpINS(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnTipoProveedorS10(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropFnTipoProveedorS10(valShowMessage)
      'vResult = vResult And fDropSpINS(valShowMessage)
   End If
   fCreateDropTipoProveedorSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropTipoProveedorSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameINS = "TipoProveedorINS"
End Function

Private Function fSpParametersINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "      @ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "      @Nombre " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
    fSpParametersINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   INSERT INTO TipoProveedor(" & vbCrLf
   vResult = vResult & "         ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         Nombre)" & vbCrLf
   vResult = vResult & "      VALUES(" & vbCrLf
   vResult = vResult & "         @ConsecutivoCompania," & vbCrLf
   vResult = vResult & "         @Nombre)" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnTipoProveedorS10(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameTipoProveedorS10(), fFnParametersTipoProveedorS10(), fFnScalarReturnDataTypeTipoProveedorS10(), fStatementsFnTipoProveedorS10(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnTipoProveedorS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnTipoProveedorS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnTipoProveedorS10(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameTipoProveedorS10(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnTipoProveedorS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnTipoProveedorS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameTipoProveedorS10() As String
   GetFnNameTipoProveedorS10 = "TipoProvS10"
End Function

Private Function fFnParametersTipoProveedorS10() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "@ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0)
   fFnParametersTipoProveedorS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParametersTipoProveedorS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFnScalarReturnDataTypeTipoProveedorS10() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = gUtilSQL.getVarCharTypeForDB(20)
   fFnScalarReturnDataTypeTipoProveedorS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnScalarReturnDataTypeTipoProveedorS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsFnTipoProveedorS10() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result " & gUtilSQL.getVarCharTypeForDB(20) & vbCrLf
   vResult = vResult & "   SET @Result = 'NACIONAL'" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END "
   fStatementsFnTipoProveedorS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnTipoProveedorS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *

