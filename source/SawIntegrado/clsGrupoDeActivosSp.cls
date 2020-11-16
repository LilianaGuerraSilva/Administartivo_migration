VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsGrupoDeActivosSp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsGrupoDeActivosSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsGrupoDeActivosSp"
End Function

Public Function fCreateDropGrupoDeActivosSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpINS(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpGrupoDeActivoFijoDesdeImportacionCuentaSiAplica(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpGrupoDeActivoFijoDesdeImportacionCuentaSiAplica(valShowMessage)
      vResult = vResult And fDropSpINS(valShowMessage)
   End If
   fCreateDropGrupoDeActivosSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropGrupoDeActivosSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   GetSpNameINS = "GrupoDeActivosINS"
End Function

Private Function fSpParametersINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @Codigo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @Nombre " & gUtilSQL.getVarCharTypeForDB(30) & "," & vbCrLf
   vResult = vResult & "         @EsActivoFijo " & gUtilSQL.getCharTypeForDB(1) & " = 'N'," & vbCrLf
   vResult = vResult & "         @CasillaIAE " & gUtilSQL.getVarCharTypeForDB(20) & " = ''," & vbCrLf
   vResult = vResult & "         @Depreciable " & gUtilSQL.getCharTypeForDB(1) & " = 'N'," & vbCrLf
   vResult = vResult & "         @CuentaDepreciacionAcumulada " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @CuentaGastosDepreciacion " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & " = ''" & vbCrLf
    fSpParametersINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpINS() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   SET @FechaUltimaModificacion = " & gUtilSQL.DfCDateSQL(gUtilSQL.GetSQLFechaActual()) & vbCrLf
   vResult = vResult & "   INSERT INTO GrupoDeActivos(" & vbCrLf
   vResult = vResult & "         ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "         Codigo," & vbCrLf
   vResult = vResult & "         Nombre," & vbCrLf
   vResult = vResult & "         EsActivoFijo," & vbCrLf
   vResult = vResult & "         CasillaIAE," & vbCrLf
   vResult = vResult & "         Depreciable," & vbCrLf
   vResult = vResult & "         CuentaDepreciacionAcumulada," & vbCrLf
   vResult = vResult & "         CuentaGastosDepreciacion," & vbCrLf
   vResult = vResult & "         NombreOperador," & vbCrLf
   vResult = vResult & "         FechaUltimaModificacion" & vbCrLf
   vResult = vResult & "      ) VALUES (" & vbCrLf
   vResult = vResult & "         @ConsecutivoPeriodo," & vbCrLf
   vResult = vResult & "         @Codigo," & vbCrLf
   vResult = vResult & "         @Nombre," & vbCrLf
   vResult = vResult & "         @EsActivoFijo," & vbCrLf
   vResult = vResult & "         @CasillaIAE," & vbCrLf
   vResult = vResult & "         @Depreciable," & vbCrLf
   vResult = vResult & "         @CuentaDepreciacionAcumulada," & vbCrLf
   vResult = vResult & "         @CuentaGastosDepreciacion," & vbCrLf
   vResult = vResult & "         @NombreOperador," & vbCrLf
   vResult = vResult & "         @FechaUltimaModificacion" & vbCrLf
   vResult = vResult & "      )" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpINS = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpINS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpGrupoDeActivoFijoDesdeImportacionCuentaSiAplica(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameGrupoDeActivoFijoDesdeImportacionCuentaSiAplica(), fSpParametersGrupoDeActivoFijoDesdeImportacionCuentaSiAplica(), fStatementsSpGrupoDeActivoFijoDesdeImportacionCuentaSiAplica(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpGrupoDeActivoFijoDesdeImportacionCuentaSiAplica = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpGrupoDeActivoFijoDesdeImportacionCuentaSiAplica", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSpGrupoDeActivoFijoDesdeImportacionCuentaSiAplica(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameGrupoDeActivoFijoDesdeImportacionCuentaSiAplica(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpGrupoDeActivoFijoDesdeImportacionCuentaSiAplica = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpGrupoDeActivoFijoDesdeImportacionCuentaSiAplica", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSpNameGrupoDeActivoFijoDesdeImportacionCuentaSiAplica() As String
   GetSpNameGrupoDeActivoFijoDesdeImportacionCuentaSiAplica = "CrearGrupoDeActivoFijoDesdeImportacionCuentaSiAplica"
End Function

Private Function fSpParametersGrupoDeActivoFijoDesdeImportacionCuentaSiAplica() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "         @ConsecutivoPeriodo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @GrupoActivoFijo " & gUtilSQL.getNumericTypeForDB(9, 0) & "," & vbCrLf
   vResult = vResult & "         @NombreGrupoActivoFijo " & gUtilSQL.getVarCharTypeForDB(30) & " = ''," & vbCrLf
   vResult = vResult & "         @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
    fSpParametersGrupoDeActivoFijoDesdeImportacionCuentaSiAplica = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersGrupoDeActivoFijoDesdeImportacionCuentaSiAplica", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fStatementsSpGrupoDeActivoFijoDesdeImportacionCuentaSiAplica() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN" & vbCrLf
   vResult = vResult & "   DECLARE @NombreGrupo " & gUtilSQL.getVarCharTypeForDB(30) & vbCrLf
   vResult = vResult & "   DECLARE @FechaUltimaModificacion " & gUtilSQL.getDateTypeForDB() & vbCrLf
   vResult = vResult & "   SET @FechaUltimaModificacion = " & gUtilSQL.DfCDateSQL(gUtilSQL.GetSQLFechaActual()) & vbCrLf
   vResult = vResult & "   IF @NombreGrupoActivoFijo = ''" & vbCrLf
   vResult = vResult & "      SET @NombreGrupo = 'IMPORTADO - ' + CAST(@FechaUltimaModificacion AS varchar)" & vbCrLf
   vResult = vResult & "   ELSE" & vbCrLf
   vResult = vResult & "      SET @NombreGrupo = @NombreGrupoActivoFijo" & vbCrLf
   vResult = vResult & "   EXEC Gp_GrupoDeActivosINS @ConsecutivoPeriodo, @GrupoActivoFijo, @NombreGrupo, 'S', '', 'N', '', '', @NombreOperador" & vbCrLf
   vResult = vResult & "END" & vbCrLf
   fStatementsSpGrupoDeActivoFijoDesdeImportacionCuentaSiAplica = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSpGrupoDeActivoFijoDesdeImportacionCuentaSiAplica", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *

