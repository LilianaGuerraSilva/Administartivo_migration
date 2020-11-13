VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsUnidadDeVentaNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private mrsUnidadDeVenta As ADODB.Recordset
Private mrsUnidadDeVentaQuery As ADODB.Recordset
Private mNombre As String
Private mNombreOperador As String
Private mFechaUltimaModificacion As Date
Private mFechaUltimaModificacionIsSet As Boolean

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsUnidadDeVentaNavigator"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Unidad De Venta"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function



Public Function GetTableName() As String
   GetTableName = "unidadDeVenta"
End Function

Public Sub sInsertaUnidadDeVentaPorDefault()
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "INSERT INTO Saw." & GetTableName & " (Nombre, " & "NombreOperador, FechaUltimaModificacion" & " ) VALUES " & "('UNIDADES', '" & gProyUsuarioActual.GetNombreDelUsuario & "', " & gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy) & ")"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInsertaUnidadDeVentaPorDefault", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.lastDllError)
End Sub

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   While Not valRecordSet.EOF
      If gDbUtil.fRecordCountOfSql(gUtilSQL.fSQLSearchByFieldString("Saw.Gv_UnidadDeVenta_B1", "Nombre", valRecordSet("Nombre").value)) <= 0 Then
         Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_UnidadDeVentaINS", gDefDatabase.Conexion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Nombre", 20, valRecordSet("Nombre").value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, valRecordSet("NombreOperador").value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", valRecordSet("FechaUltimaModificacion").value)
         insUtAdoCmd.fExecCmd vCmd
         Set vCmd = Nothing
      End If
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   valRecordSet.Filter = adFilterNone
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.lastDllError)
End Sub
