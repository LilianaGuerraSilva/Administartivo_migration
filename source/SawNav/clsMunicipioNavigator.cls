VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsMunicipioNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsMunicipioNavigator"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Municipio"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   While Not valRecordSet.EOF
      If gDbUtil.fRecordCountOfSql(gUtilSQL.fSQLSearchByFieldString("Comun.Gv_Municipio_B1", "Codigo", valRecordSet("Codigo").Value)) <= 0 Then
         Set vCmd = insUtAdoCmd.fCreateCmd("Comun.Gp_MunicipioINS", gDefDatabase.Conexion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Codigo", 10, valRecordSet("Codigo").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreMunicipio", 100, valRecordSet("NombreMunicipio").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreRpx", 100, valRecordSet("NombreRpx").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreTxt", 100, valRecordSet("NombreTxt").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@fldOrigen", 1, valRecordSet("fldOrigen").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 10, valRecordSet("NombreOperador").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", valRecordSet("FechaUltimaModificacion").Value)
         insUtAdoCmd.fExecCmd vCmd
         Set vCmd = Nothing
      End If
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

