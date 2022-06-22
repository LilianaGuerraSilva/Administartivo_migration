VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCompraNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCompraNavigator"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Compra"
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
      vSQL = "SELECT ConsecutivoCompania FROM Adm.Gv_Compra_B1 WHERE Consecutivo = " & valRecordSet("Consecutivo").Value & " AND ConsecutivoCompania = " & valConsecutivoCompaniaNew & " ORDER BY Consecutivo"
      If gDbUtil.fRecordCountOfSql(vSQL) <= 0 Then
         Set vCmd = insUtAdoCmd.fCreateCmd("Adm.Gp_CompraINS", gDefDatabase.Conexion)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@DateFormat", 3, "dmy")
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@Consecutivo", gConvert.ConvierteAlong(valRecordSet("Consecutivo").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Serie", 20, valRecordSet("Serie").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Numero", 10, valRecordSet("Numero").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@Fecha", valRecordSet("Fecha").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoProveedor", gConvert.ConvierteAlong(valRecordSet("ConsecutivoProveedor").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoAlmacen", gConvert.ConvierteAlong(valRecordSet("ConsecutivoAlmacen").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Moneda", 80, valRecordSet("Moneda").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CodigoMoneda", 4, valRecordSet("CodigoMoneda").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@CambioABolivares", valRecordSet("CambioABolivares").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@GenerarCxp", 1, valRecordSet("GenerarCxp").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@UsaSeguro", 1, valRecordSet("UsaSeguro").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDeDistribucion", 1, valRecordSet("TipoDeDistribucion").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TasaAduanera", valRecordSet("TasaAduanera").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TasaDolar", valRecordSet("TasaDolar").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@ValorUT", valRecordSet("ValorUT").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TotalRenglones", valRecordSet("TotalRenglones").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TotalOtrosGastos", valRecordSet("TotalOtrosGastos").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterCurrency("@TotalCompra", valRecordSet("TotalCompra").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Comentarios", 255, valRecordSet("Comentarios").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@StatusCompra", 1, valRecordSet("StatusCompra").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDeCompra", 1, valRecordSet("TipoDeCompra").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaDeAnulacion", valRecordSet("FechaDeAnulacion").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoOrdenDeCompra", gConvert.ConvierteAlong(valRecordSet("ConsecutivoOrdenDeCompra").Value))
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NoFacturaNotaEntrega", 20, valRecordSet("NoFacturaNotaEntrega").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoDeCompraParaCxp", 1, valRecordSet("TipoDeCompra").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("CodigoMonedaCostoUltimaCompra", 4, valRecordSet("CodigoMonedaCostoUltimaCompra").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("CambioCostoUltimaCompra", 4, valRecordSet("CambioCostoUltimaCompra").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 10, valRecordSet("NombreOperador").Value)
         vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", valRecordSet("FechaUltimaModificacion").Value)
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
