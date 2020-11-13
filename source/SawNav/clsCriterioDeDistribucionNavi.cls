VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCriterioDeDistribucionNav"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCriterioDeDistribucionNavigator"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Criterios De Distribución"
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
      Set vCmd = insUtAdoCmd.fCreateCmd("Comun.Gp_CriterioDeDistribucionINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", valRecordSet("Consecutivo").Value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Codigo", 20, valRecordSet("Codigo").Value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Descripcion", 60, valRecordSet("Descripcion").Value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeNoDeducible", valRecordSet("PorcentajeNoDeducible").Value)
      If gDbUtil.FieldInRecordSet(valRecordSet, "PorcentajeGasto") Then
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeGasto", valRecordSet("PorcentajeGasto").Value)
      Else
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeGasto", 0)
      End If
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 20, valRecordSet("NombreOperador").Value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", valRecordSet("FechaUltimaModificacion").Value)
      insUtAdoCmd.fExecCmd vCmd
      Set vCmd = Nothing
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   valRecordSet.Filter = adFilterNone
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

