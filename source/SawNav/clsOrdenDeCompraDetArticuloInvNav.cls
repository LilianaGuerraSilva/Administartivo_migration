VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsOrdenDeCompraDetArtInvNav"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsOrdenDeCompraDetArticuloInvNav"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Orden De Compra Detalle Articulo Inventario"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset, ByVal valConsecutivoCompaniaOld As Long, ByVal valConsecutivoCompaniaNew As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vSQL As String
   On Error GoTo h_ERROR
   valRecordSet.Filter = "ConsecutivoCompania = " & valConsecutivoCompaniaOld
   Set insUtAdoCmd = New clsUtilAdoCmd
   While Not valRecordSet.EOF
      vSQL = "SELECT ConsecutivoCompania FROM Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1 WHERE ConsecutivoOrdenDeCompra = " & valRecordSet("ConsecutivoOrdenDeCompra").Value
      vSQL = vSQL & " AND ConsecutivoCompania = " & valConsecutivoCompaniaNew & " AND Consecutivo = " & valRecordSet("Consecutivo").Value
      If gDbUtil.fRecordCountOfSql(vSQL) <= 0 Then
         Set vCmd = insUtAdoCmd.fCreateCmd("Adm.Gp_OrdenDeCompraDetalleArticuloInventarioINS", gDefDatabase.Conexion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoOrdenDeCompra", gConvert.ConvierteAlong(valRecordSet("ConsecutivoOrdenDeCompra").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", gConvert.ConvierteAlong(valRecordSet("Consecutivo").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoArticulo", 30, valRecordSet("CodigoArticulo").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@DescripcionArticulo", 7000, valRecordSet("DescripcionArticulo").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@Cantidad", gConvert.fConvertStringToCurrency(valRecordSet("Cantidad").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@CostoUnitario", gConvert.fConvertStringToCurrency(valRecordSet("CostoUnitario").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@CantidadRecibida", gConvert.fConvertStringToCurrency(valRecordSet("CantidadRecibida").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeDeDistribucion", gConvert.fConvertStringToCurrency(valRecordSet("PorcentajeDeDistribucion").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@MontoDistribucion", gConvert.fConvertStringToCurrency(valRecordSet("MontoDistribucion").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeSeguro", gConvert.fConvertStringToCurrency(valRecordSet("PorcentajeSeguro").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoLoteDeInventario", gConvert.ConvierteAlong(valRecordSet("ConsecutivoLoteDeInventario").Value))
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
