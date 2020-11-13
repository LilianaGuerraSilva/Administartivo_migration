VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsClasificadorActividadEconNav"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsClasificadorActividadEconomicaNav"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Clasificador Actividad Economica"
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
      If gDbUtil.fRecordCountOfSql(gUtilSQL.fSQLSearchByFieldString("Comun.Gv_ClasificadorActividadEconomica_B1", "Codigo", valRecordSet("Codigo").Value)) <= 0 Then
         Set vCmd = insUtAdoCmd.fCreateCmd("Comun.Gp_ClasificadorActividadEconomicaINS", gDefDatabase.Conexion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@DateFormat", 3, "DMY")
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Codigo", 10, valRecordSet("Codigo").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoMunicipio", 10, valRecordSet("CodigoMunicipio").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoActividad", 10, valRecordSet("CodigoActividad").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Descripcion", 100, valRecordSet("Descripcion").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@BaseImponible", valRecordSet("BaseImponible").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@AlicuotaPatente", valRecordSet("AlicuotaPatente").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@AlicuotaRetencion", valRecordSet("AlicuotaRetencion").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@MinimoTributable", valRecordSet("MinimoTributable").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@ParaPagosMayoresDe", valRecordSet("ParaPagosMayoresDe").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaAplicacion", valRecordSet("FechaAplicacion").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@Sustraendo", valRecordSet("Sustraendo").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EsTitulo", 1, valRecordSet("EsTitulo").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EstaVigente", 1, valRecordSet("EstaVigente").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@fldOrigen", 1, valRecordSet("fldOrigen").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, valRecordSet("NombreOperador").Value)
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

