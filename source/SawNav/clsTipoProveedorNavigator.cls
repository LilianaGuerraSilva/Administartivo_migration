VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsTipoProveedorNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsTipoProveedorNavigator"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Tipo Proveedor"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset, ByVal valConsecutivoCompaniaOld As Long, ByVal valConsecutivoCompaniaNew As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   valRecordSet.Filter = "ConsecutivoCompania = " & valConsecutivoCompaniaOld
   While Not valRecordSet.EOF
      Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_TipoProveedorINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Nombre", 20, valRecordSet("Nombre").value)
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

Public Sub sInsertaValorPorDefecto(ByVal valConsecutivoCompania As Long, ByVal valNombre As String)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
      Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_TipoProveedorINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompania)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Nombre", 20, valNombre)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, gProyUsuarioActual.GetNombreDelUsuario)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", gUtilDate.getFechaDeHoy)
      insUtAdoCmd.fExecCmd vCmd
      Set vCmd = Nothing
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.lastDllError)
End Sub


Public Function fCopiaLosTiposDeProveedorDeOtraCompania(ByVal valConsecutivoCompania As Long) As Boolean
   Dim valSQLListaDeCamposConIgualValor As String
   Dim SQL As String
   On Error GoTo h_ERROR
   fCopiaLosTiposDeProveedorDeOtraCompania = False
   valSQLListaDeCamposConIgualValor = "Nombre,NombreOperador,FechaUltimaModificacion"
   SQL = " INSERT INTO " & "Saw.TipoProveedor" & " (" & valSQLListaDeCamposConIgualValor & ", " & "ConsecutivoCompania" & ") " & " SELECT " & valSQLListaDeCamposConIgualValor & ", " & gProyCompaniaActual.GetConsecutivoCompania & " FROM  " & "Saw.TipoProveedor" & " WHERE " & "ConsecutivoCompania" & " = " & valConsecutivoCompania
   SQL = SQL & " AND " & "Nombre" & " NOT IN ('NACIONAL', 'EXTRANJERO') "
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   fCopiaLosTiposDeProveedorDeOtraCompania = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCopiaLosTiposDeProveedorDeOtraCompania", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.lastDllError)
End Function


Public Function fSearchByField(ByVal fieldName As String, ByVal fieldValue As String) As Boolean
   Dim mrsRecord As ADODB.Recordset
   On Error GoTo h_ERROR
   fSearchByField = False
      Set mrsRecord = New ADODB.Recordset
   If gDbUtil.fOpenRecordSetAllParameters(mrsRecord, gProyCompaniaActual.getSQLSearchByFieldStringAndConsecutivo("Saw.TipoProveedor", fieldName, fieldValue), _
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

