VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsZonaCobranzaNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private mConsecutivoCompania As Long
Private mNombre As String
Private mNombreOperador As String
Private mFechaUltimaModificacion As Date
Private mFechaUltimaModificacionIsSet As Boolean
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsZonaCobranzaNavigator"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Zona Cobranza"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function GetConsecutivoCompania() As Long
   GetConsecutivoCompania = mConsecutivoCompania
End Function
Public Sub SetConsecutivoCompania(ByVal valNewValue As Long)
   mConsecutivoCompania = valNewValue
End Sub
Public Function GetNombre() As String
   GetNombre = mNombre
End Function
Public Sub SetNombre(ByVal valNewValue As String)
   mNombre = gTexto.DfMid(valNewValue, 1, 100)
End Sub
Public Function GetNombreOperador() As String
   GetNombreOperador = mNombreOperador
End Function
Public Sub SetNombreOperador(ByVal valNewValue As String)
   mNombreOperador = gTexto.DfMid(valNewValue, 1, 10)
End Sub
Public Function GetFechaUltimaModificacion() As Date
   GetFechaUltimaModificacion = mFechaUltimaModificacion
End Function
Public Sub SetFechaUltimaModificacion(ByVal valNewValue As Date)
   mFechaUltimaModificacion = valNewValue
   mFechaUltimaModificacionIsSet = True
End Sub
Public Function GetTableName() As String
   GetTableName = "zonaCobranza"
End Function
Public Function fCopiaLasZonaCobranzaDeOtraCompania(ByVal valConsecutivoCompania As Long) As Boolean
   Dim valSQLListaDeCamposConIgualValor As String
   Dim SQL As String
   On Error GoTo h_ERROR
   fCopiaLasZonaCobranzaDeOtraCompania = False
   valSQLListaDeCamposConIgualValor = "Nombre"
   SQL = " INSERT INTO Saw." & GetTableName & " (" & valSQLListaDeCamposConIgualValor & ", " & "NombreOperador" & ", " & "FechaUltimaModificacion" & ", " & "ConsecutivoCompania" & ") " & " SELECT " & valSQLListaDeCamposConIgualValor & ", " & "'" & gProyUsuarioActual.GetNombreDelUsuario & "', " & gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy) & ", " & gProyCompaniaActual.GetConsecutivoCompania & " FROM  " & GetTableName & " WHERE " & "ConsecutivoCompania" & " = " & valConsecutivoCompania
   SQL = SQL & " AND " & "Nombre" & " <> '" & gProyCompaniaActual.GetCiudad & "'"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   fCopiaLasZonaCobranzaDeOtraCompania = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCopiaLasZonaCobranzaDeOtraCompania", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fEstableceZonaDeCobranza() As String
Dim rsZonaCobranza As ADODB.Recordset
   Dim SQL As String
   Dim PrimeraZona As String
   On Error GoTo h_ERROR
   PrimeraZona = ""
   SQL = " SELECT Nombre"
   SQL = SQL & " FROM Saw.Gv_ZonaCobranza_B1"
   Set rsZonaCobranza = New ADODB.Recordset
   If gDbUtil.fOpenRecordset(rsZonaCobranza, SQL, gDefDatabase.Conexion) Then
      If gDbUtil.fRecordCount(rsZonaCobranza) Then
         rsZonaCobranza.MoveFirst
         PrimeraZona = rsZonaCobranza.Fields("Nombre").value
      End If
   End If
   gDbUtil.sDestroyRecordSet rsZonaCobranza
h_EXIT: On Error GoTo 0
   fEstableceZonaDeCobranza = PrimeraZona
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fEstableceZonaDeCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSearchByField(ByVal fieldName As String, ByVal fieldValue As String) As Boolean
   Dim mrsZonaCobranza As ADODB.Recordset
   On Error GoTo h_ERROR
   fSearchByField = False
      Set mrsZonaCobranza = New ADODB.Recordset
   If gDbUtil.fOpenRecordSetAllParameters(mrsZonaCobranza, gProyCompaniaActual.getSQLSearchByFieldStringAndConsecutivo("Saw.ZonaCobranza", fieldName, fieldValue), _
         gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If mrsZonaCobranza.RecordCount = 0 Then
         fSearchByField = False
      Else
         fSearchByField = True
         mConsecutivoCompania = mrsZonaCobranza("ConsecutivoCompania").value
         mNombre = mrsZonaCobranza("Nombre").value
      End If
   End If
   gDbUtil.sDestroyRecordSet mrsZonaCobranza
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSearchByField = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSearchByField", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset, ByVal valConsecutivoCompaniaOld As Long, ByVal valConsecutivoCompaniaNew As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   valRecordSet.filter = "ConsecutivoCompania = " & valConsecutivoCompaniaOld
   While Not valRecordSet.EOF
      Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_ZonaCobranzaINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Nombre", 100, valRecordSet("Nombre").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, valRecordSet("NombreOperador").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", valRecordSet("FechaUltimaModificacion").value)
      insUtAdoCmd.fExecCmd vCmd
      Set vCmd = Nothing
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   valRecordSet.filter = adFilterNone
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
