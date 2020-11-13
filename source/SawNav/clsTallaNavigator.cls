VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsTallaNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private mConsecutivoCompania As Long
Private mCodigoTalla As String
Private mDescripcionTalla As String
Private mCodigoLote As String
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsTallaNavigator"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Talla"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Public Function GetConsecutivoCompania() As Long
   GetConsecutivoCompania = mConsecutivoCompania
End Function
Public Function GetCodigoTalla() As String
   GetCodigoTalla = mCodigoTalla
End Function

Public Function GetDescripcionTalla() As String
   GetDescripcionTalla = mDescripcionTalla
End Function

Public Function GetCodigoLote() As String
   GetCodigoLote = mCodigoLote
End Function
 

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset, ByVal valConsecutivoCompaniaOld As Long, ByVal valConsecutivoCompaniaNew As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   valRecordSet.Filter = "ConsecutivoCompania = " & valConsecutivoCompaniaOld
   While Not valRecordSet.EOF
      Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_TallaINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoTalla", 3, valRecordSet("CodigoTalla").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@DescripcionTalla", 20, valRecordSet("DescripcionTalla").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoLote", 10, valRecordSet("CodigoLote").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, valRecordSet("NombreOperador").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", valRecordSet("FechaUltimaModificacion").value)
      insUtAdoCmd.fExecCmd vCmd
      Set vCmd = Nothing
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   valRecordSet.Filter = adFilterNone
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.lastDllError)
End Sub

Public Sub sInicializarValores(ByVal valConsecutivoCompania As Long, ByVal valCodigoTalla As String, ByVal valDescripcionTalla As String, ByVal valCodigoLote As String)
   On Error GoTo h_ERROR
h_EXIT:    On Error GoTo 0
    mConsecutivoCompania = valConsecutivoCompania
    mCodigoTalla = valCodigoTalla
    mDescripcionTalla = valDescripcionTalla
    mCodigoLote = valCodigoLote
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInicializarValores", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.lastDllError)
End Sub

Public Function fSearchByField(ByVal fieldName As String, ByVal fieldValue As String) As Boolean
   Dim mrsRecord As ADODB.Recordset
   On Error GoTo h_ERROR
   fSearchByField = False
      Set mrsRecord = New ADODB.Recordset
   If gDbUtil.fOpenRecordSetAllParameters(mrsRecord, gProyCompaniaActual.getSQLSearchByFieldStringAndConsecutivo("Saw.Talla", fieldName, fieldValue), _
         gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If mrsRecord.RecordCount > 0 Then
         fSearchByField = True
      End If
   End If
   gDbUtil.sDestroyRecordSet mrsRecord
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSearchByField = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSearchByField", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.lastDllError)
End Function


Public Sub sInsertarValoresDesdeImportacion(ByVal valCodigoLote As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
      Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_TallaINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", mConsecutivoCompania)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoTalla", 3, mCodigoTalla)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@DescripcionTalla", 20, mDescripcionTalla)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoLote", 10, valCodigoLote)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, gProyUsuarioActual.GetNombreDelUsuario)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", gUtilDate.getFechaDeHoy)
      insUtAdoCmd.fExecCmd vCmd
      Set vCmd = Nothing
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInsertarValoresDesdeImportacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.lastDllError)
End Sub
