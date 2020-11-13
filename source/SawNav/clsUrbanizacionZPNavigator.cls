VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsUrbanizacionZPNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private mUrbanizacion As String
Private mZonaPostal As String
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsUrbanizacionZPNavigator"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Urbanización - Zona Postal"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Public Function GetUrbanizacion() As String
   GetUrbanizacion = mUrbanizacion
End Function
Public Sub SetUrbanizacion(ByVal valNewValue As String)
   mUrbanizacion = gTexto.DfMid(valNewValue, 1, 30)
End Sub
Public Function GetZonaPostal() As String
   GetZonaPostal = mZonaPostal
End Function
Public Sub SetZonaPostal(ByVal valNewValue As String)
   mZonaPostal = gTexto.DfMid(valNewValue, 1, 7)
End Sub
Public Function GetTableName() As String
   GetTableName = "urbanizacionZP"
End Function

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   While Not valRecordSet.EOF
      If gDbUtil.fRecordCountOfSql(gUtilSQL.fSQLSearchByFieldString("Saw.Gv_UrbanizacionZP_B1", "Urbanizacion", valRecordSet("Urbanizacion").value)) <= 0 Then
         Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_UrbanizacionZPINS", gDefDatabase.Conexion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Urbanizacion", 30, valRecordSet("Urbanizacion").value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@ZonaPostal", 7, valRecordSet("ZonaPostal").value)
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

