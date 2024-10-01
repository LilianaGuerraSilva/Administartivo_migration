VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCompraDetArtInventarioNav"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCompraDetArtInventarioNav"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Compra Detalle Articulo Inventario"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset, ByVal valConsecutivoCompaniaOld As Long, ByVal valConsecutivoCompaniaNew As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   Dim vSQL As String
   On Error GoTo h_ERROR
   valRecordSet.filter = "ConsecutivoCompania = " & valConsecutivoCompaniaOld
   Set insUtAdoCmd = New clsUtilAdoCmd
   While Not valRecordSet.EOF
      vSQL = "SELECT ConsecutivoCompania FROM Adm.Gv_CompraDetalleArticuloInventario_B1 WHERE ConsecutivoCompra = " & valRecordSet("ConsecutivoCompra").value 
      vSQL = vSQL & " AND ConsecutivoCompania = " & valConsecutivoCompaniaNew  & " AND Consecutivo = " & valRecordSet("Consecutivo").value
      If gDbUtil.fRecordCountOfSql(vSQL) <= 0 Then
         Set vCmd = insUtAdoCmd.fCreateCmd("Adm.Gp_CompraDetalleArticuloInventarioINS", gDefDatabase.Conexion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompra", gConvert.ConvierteAlong(valRecordSet("ConsecutivoCompra").value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", gConvert.ConvierteAlong(valRecordSet("Consecutivo").value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoArticulo", 30, valRecordSet("CodigoArticulo").value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@Cantidad", valRecordSet("Cantidad").value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PrecioUnitario", valRecordSet("PrecioUnitario").value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@CantidadRecibida", valRecordSet("CantidadRecibida").value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeDeDistribucion", valRecordSet("PorcentajeDeDistribucion").value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@MontoDistribucion", valRecordSet("MontoDistribucion").value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@PorcentajeSeguro", valRecordSet("PorcentajeSeguro").value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@ConsecutivoLoteDeInventario", valRecordSet("ConsecutivoLoteDeInventario").Value)
         insUtAdoCmd.fExecCmd vCmd
         Set vCmd = Nothing
      End If
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   valRecordSet.filter = adFilterNone
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
