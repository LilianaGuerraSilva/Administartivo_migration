VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCompraDetSerialRolloNav"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCompraDetSerialRolloNav"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Compra Detalle Serial Rollo"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset, ByVal valConsecutivoCompaniaOld As Long, ByVal valConsecutivoCompaniaNew As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vSQL As String
   On Error GoTo h_ERROR
   valRecordSet.Filter = "ConsecutivoCompania = " & valConsecutivoCompaniaOld
   Set insUtAdoCmd = New clsUtilAdoCmd
   While Not valRecordSet.EOF
      vSQL = "SELECT ConsecutivoCompania FROM Adm.Gv_CompraDetalleSerialRollo_B1 WHERE ConsecutivoCompra = " & valRecordSet("ConsecutivoCompra").Value
      vSQL = vSQL & " AND ConsecutivoCompania = " & valConsecutivoCompaniaNew & " AND Consecutivo = " & valRecordSet("Consecutivo").Value
      If gDbUtil.fRecordCountOfSql(vSQL) <= 0 Then
         Set vCmd = insUtAdoCmd.fCreateCmd("Adm.Gp_CompraDetalleSerialRolloINS", gDefDatabase.Conexion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompra", gConvert.ConvierteAlong(valRecordSet("ConsecutivoCompra").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", gConvert.ConvierteAlong(valRecordSet("Consecutivo").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoArticulo", 30, valRecordSet("CodigoArticulo").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Serial", 50, valRecordSet("Serial").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Rollo", 20, valRecordSet("Rollo").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@Cantidad", valRecordSet("Cantidad").Value)
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
