VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCompraDetalleGastoNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCompraDetalleGastoNavigator"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Compra Detalle Gasto"
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
      vSQL = "SELECT ConsecutivoCompania FROM Adm.Gv_CompraDetalleGasto_B1 WHERE ConsecutivoCompra = " & valRecordSet("ConsecutivoCompra").Value
      vSQL = vSQL & " AND ConsecutivoCompania = " & valConsecutivoCompaniaNew & " AND ConsecutivoRenglon = " & valRecordSet("ConsecutivoRenglon").Value
      If gDbUtil.fRecordCountOfSql(vSQL) <= 0 Then
         Set vCmd = insUtAdoCmd.fCreateCmd("Adm.Gp_CompraDetalleGastoINS", gDefDatabase.Conexion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompra", gConvert.ConvierteAlong(valRecordSet("ConsecutivoCompra").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCxP", gConvert.ConvierteAlong(valRecordSet("ConsecutivoCxP").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoRenglon", gConvert.ConvierteAlong(valRecordSet("ConsecutivoRenglon").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDeCosto", 1, valRecordSet("TipoDeCosto").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@Monto", valRecordSet("Monto").Value)
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
