VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsParametrosSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsParametrosSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsParametrosSp"
End Function
Public Function fCreateDropParametrosSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateFnEnumToDBValue(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnDBValueToEnum(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropFnDBValueToEnum(valShowMessage)
      vResult = vResult And fDropFnEnumToDBValue(valShowMessage)
   End If
   fCreateDropParametrosSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropParametrosSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnEnumToDBValue(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameEnumToDBValue(), fFnParametersEnumToDBValue(), fFnScalarReturnDataTypeEnumToDBValue(), fStatementsFnEnumToDBValue(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnEnumToDBValue = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnEnumToDBValue", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnEnumToDBValue(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameEnumToDBValue(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnEnumToDBValue = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnEnumToDBValue", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameEnumToDBValue() As String
   GetFnNameEnumToDBValue = "EnumToDBValue"
End Function

Private Function fFnParametersEnumToDBValue() As String
   fFnParametersEnumToDBValue = "@EnumValue int"
End Function

Private Function fFnScalarReturnDataTypeEnumToDBValue() As String
   fFnScalarReturnDataTypeEnumToDBValue = "char(1)"
End Function

Private Function fStatementsFnEnumToDBValue() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   IF @EnumValue > 200" & vbCrLf
   vResult = vResult & "      SET @Result = ''" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Result = CHAR(@EnumValue + 48)" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsFnEnumToDBValue = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnEnumToDBValue", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateFnDBValueToEnum(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fCreateScalarFunction(GetFnNameDBValueToEnum(), fFnParametersDBValueToEnum(), fFnScalarReturnDataTypeDBValueToEnum(), fStatementsFnDBValueToEnum(), valShowMessage, valWithEncryption)
   Set insLibSf = Nothing
   fCreateFnDBValueToEnum = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnDBValueToEnum", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropFnDBValueToEnum(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSf As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibSf = New clsUtilStoredFunctions
   vResult = insLibSf.fDropFunction(GetFnNameDBValueToEnum(), valShowMessage)
   Set insLibSf = Nothing
   fDropFnDBValueToEnum = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnDBValueToEnum", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetFnNameDBValueToEnum() As String
   GetFnNameDBValueToEnum = "DBValueToEnum"
End Function

Private Function fFnParametersDBValueToEnum() As String
   fFnParametersDBValueToEnum = "@Char char(1)"
End Function

Private Function fFnScalarReturnDataTypeDBValueToEnum() As String
   fFnScalarReturnDataTypeDBValueToEnum = "int"
End Function

Private Function fStatementsFnDBValueToEnum() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Result int" & vbCrLf
   vResult = vResult & "   DECLARE @AsciiValue int" & vbCrLf
   vResult = vResult & "   SET @AsciiValue = ASCII('0')" & vbCrLf
   vResult = vResult & "   IF (NOT (@Char IS NULL))" & vbCrLf
   vResult = vResult & "      IF (LEN(@Char) > 0)" & vbCrLf
   vResult = vResult & "         SET @AsciiValue = ASCII(@Char)" & vbCrLf
   vResult = vResult & "   IF @AsciiValue >= 48" & vbCrLf
   vResult = vResult & "      SET @Result = @AsciiValue - 48" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @Result = @AsciiValue" & vbCrLf
   vResult = vResult & "   RETURN @Result" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsFnDBValueToEnum = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnDBValueToEnum", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *

