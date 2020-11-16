VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} RptImprimirFactura 
   Caption         =   "Imprimir Borrador"
   ClientHeight    =   14850
   ClientLeft      =   2265
   ClientTop       =   345
   ClientWidth     =   19080
   StartUpPosition =   3  'Windows Default
   _ExtentX        =   33655
   _ExtentY        =   26194
   SectionData     =   "RptImprimirFactura.dsx":0000
End
Attribute VB_Name = "RptImprimirFactura"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "RptImprimirFactura"
Private Const CM_MESSAGE_NAME As String = "Reporte Imprimir Factura "
Private Const ERR_NOHAYIMPRESORA = 5007
'Private FechaObtenida As String
'Private TotalRenglones As Currency
Private gRenglon As Object
Private gFactura As Object
Private gAdmAlicuotaIvaActual As Object
Private mConsecutivoCompania As Long
'Private mClienteCiudad As String
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sConfigurarDatosDelReporte(ByVal valValorDelSql As String, _
                  ByVal valVendedorNombre As String, ByVal valClienteCodigo As String, _
                     ByVal valClienteNombre As String, ByVal valClienteNumeroRIF As String, _
                        ByVal valClienteNumeroNIT As String, ByVal valClienteCiudad As String, _
                           ByVal valClienteFax As String, ByVal valClienteTelefono As String, _
                              ByVal valClienteContacto As String, ByVal valClienteDireccion As String, _
                                 ByVal valNombreCompaniaParaInformes As String, ByVal valConsecutivoCompania As Long, ByRef refInsFactura As Object, _
                                    ByRef refInsAdmAlicuotaIvaActual As Object, ByRef refInsRenglon As Object)
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   Set gRenglon = refInsRenglon
   Set gFactura = refInsFactura
   Set gAdmAlicuotaIvaActual = refInsAdmAlicuotaIvaActual
   mConsecutivoCompania = valConsecutivoCompania
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "DataControl1", valValorDelSql) Then
'      mClienteCiudad = valClienteCiudad
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNombreCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtTelefonoDeEnvio", valClienteTelefono
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtFaxDeEnvio", valClienteFax
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtDireccionEnvio", valClienteDireccion
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNombreDelCliente", valClienteNombre
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtCodCliente", valClienteCodigo
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNITDelCliente", valClienteNumeroNIT
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtRIFDelCliente", valClienteNumeroRIF
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNombreDelVendedor", valVendedorNombre
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtContacto", valClienteContacto
      gUtilReports.sDefaultConfigurationForDateFields Me, "txtFecha", "", "FechaFactura", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNumeroDeFactura", "", "NumeroFactura"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNombreOperador", "", "NombreOper"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtCodigoArticulo", "", "CodigoArticulo"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalBs", "", "TotalFact"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtSubTotal", "", "RenglonesAcumulados"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtIVACalculado", "", "totalIva"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtMontoTotalConDescuento", "", "TotalBaseImponible"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtCantidad", "", "Cant", False, ddTXRight, formatoNDecimales
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtPorcentajeDescuento", "", "RenglonPorcentajeDesc"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtPorcentajeDescuentoGeneralizado", "", "FacturaPorcentajeDesc"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtObservaciones", "", "Observacion"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtConcepto", "", "Concepto"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtPrecioUnitario", "", "PrecioSIVA", False, ddTXRight, formatoNDecimales
'      sOutPutFormatCamposNumericos
'      Set insRenglon = New clsRenglonFacturaNavigator
      gRenglon.setClaseDeTrabajo eCTFC_Factura
'      Set insFactura = New clsFacturaNavigator
      gFactura.setClaseDeTrabajo eCTFC_Factura
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter Me, "GroupHeader1", "NumeroFactura", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPBefore
      gUtilReports.sDefaultConfigurationForSummaryFields Me, "txtSubTotal", "txtTotal", eSF_SUM, "GroupHeader1", eSR_GROUP, eST_SUB_TOTAL
      gUtilReports.sDefaultConfigurationForSummaryFields Me, "txtSubTotal", "txtTotal", eSF_SUM, "GroupHeader1", eSR_GROUP, eST_SUB_TOTAL
      gUtilMargins.sAsignarMargenesGenerales Me, ddODefault, "Factura Borrador"
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "ConfigurarDatosDelReporte()", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_Error(ByVal Number As Integer, ByVal Description As DDActiveReports2.IReturnString, ByVal Scode As Long, ByVal Source As String, ByVal HelpFile As String, ByVal HelpContext As Long, ByVal CancelDisplay As DDActiveReports2.IReturnBool)
   On Error GoTo h_ERROR
   If Number <> ERR_NOHAYIMPRESORA Then  'Ignore No printer warning
      gMessage.AlertMessage " " & Description, "ERROR PROCESANDO DATOS"
   End If
   CancelDisplay = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "ActiveReport_Error", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_NoData()
   On Error GoTo h_ERROR
   gMessage.Advertencia "No se Encontró Información para Imprimir"
   Cancel
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "ActiveReport_NoData", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_PageStart()
   On Error GoTo h_ERROR
'   txtNombreCompania.Text = gProyCompaniaActual.GetNombreCompaniaParaInformes(False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "ActiveReport_PageStart()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_ReportStart()
   On Error GoTo h_ERROR
   Me.PageSettings.LeftMargin = 600
   Me.PageSettings.TopMargin = 400
'   txtFecha.OutputFormat = gUtilReports.getDefaultDateFormat
'   GroupHeader1.DataField = "NumeroFactura"
'   GroupHeader1.GrpKeepTogether = ddGrpFirstDetail
'   GroupHeader1.KeepTogether = True
'   GroupHeader1.Repeat = ddRepeatOnPage
   If Not DataControl1.Recordset.EOF Then
      If DataControl1.Recordset!FacturaporcentajeDesc > 0 Then
         lblDescuentoTotal.Visible = True
         txtMontoTotalConDescuento.Visible = True
         lblSubTotalDos.Visible = True
         TxtSubTotalVerdadero.Visible = True
         txtPorcentajeDescuentoGeneralizado.Visible = True
      Else
         lblIVA.Top = 1559
         txtCantidadParaIVA.Top = 1559
         txtIVACalculado.Top = 1559
         lblTotalBs.Top = 1843
         txtTotalBs.Top = 1843
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "ActiveReport_ReportStart()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_Terminate()
   On Error GoTo h_ERROR
'   Set insFactura = Nothing
'   Set insRenglon = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "ActiveReport_Terminate()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Detail_Format()
   On Error GoTo h_ERROR
   sVerificarDescuentoXRenglon
   txtTotal.Text = gRenglon.fCalculaEltotalDelRenglon _
                  (gConvert.fConvierteACurrency(txtPrecioUnitario), 0, gConvert.fConvierteACurrency(txtCantidad), _
                  gConvert.fConvierteACurrency(txtPorcentajeDescuento), True)
'   txtPorcentajeDescuento.Text = gConvert.FormatoNumerico(CStr(txtPorcentajeDescuento.Text), False)
   txtPorcentajeAlicuota.Text = gAdmAlicuotaIvaActual.GetAlicuotaIVA(gConvert.fConvertStringToDate(txtFecha), eTD_ALICUOTAGENERAL)
'   txtPorcentajeAlicuota.Text = gConvert.FormatoNumerico(txtPorcentajeAlicuota.Text, False)
'   txtTotal.Text = gConvert.FormatoNumerico(txtTotal.Text, False)
'   TotalRenglones = TotalRenglones + gConvert.fConvierteACurrency(txtTotal)
   If DataControl1.Recordset!RenglonporcentajeDesc > 0 Then
      lblPorcentajeDescuento.Visible = True
      txtPorcentajeDescuento.Visible = True
   Else
      lblPorcentajeDescuento.Visible = False
      txtPorcentajeDescuento.Text = ""
   End If
'   txtPorcentajeDescuento.Text = gConvert.FormatoNumerico(txtPorcentajeDescuento.Text, False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "Detail_Formart()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupFooter1_BeforePrint()
   On Error GoTo h_ERROR
'   txtSubTotal.Text = gConvert.FormatoNumerico(CStr(txtSubTotal.Text), False)
'   txtIVACalculado = gConvert.FormatoNumerico(CStr(txtIVACalculado.Text), False)
'   txtCantidadParaIVA = gConvert.FormatoNumerico(CStr(txtCantidadParaIVA.Text), False)
'   txtTotalBs = gConvert.FormatoNumerico(CStr(txtTotalBs.Text), False)
'   txtMontoTotalConDescuento.Text = gConvert.FormatoNumerico(CStr(txtMontoTotalConDescuento.Text), False)
'   txtPorcentajeDescuentoGeneralizado.Text = gConvert.FormatoNumerico(CStr(txtPorcentajeDescuentoGeneralizado.Text), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "GroupFooter1_BeforePrint()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupFooter1_Format()
   Dim montoBI As Currency
   On Error GoTo h_ERROR
   montoBI = txtMontoTotalConDescuento.DataValue
'   txtSubTotal.Text = TotalRenglones
   txtMontoTotalConDescuento.Text = (gConvert.fConvierteACurrency(txtSubTotal) * gConvert.fConvierteACurrency(txtPorcentajeDescuentoGeneralizado) / 100)
'   txtCantidadParaIVA.Text = gAdmAlicuotaIvaActual.GetAlicuotaIVA(gConvert.fConvertStringToDate(txtFecha), eTD_ALICUOTAGENERAL)
   txtCantidadParaIVA.Text = gConvert.FormatoNumerico(CStr(montoBI), False)
   TxtSubTotalVerdadero.Text = txtSubTotal.Text - txtMontoTotalConDescuento.Text
'   txtSubTotal.Text = gConvert.FormatoNumerico(txtSubTotal.Text, False)
   TxtSubTotalVerdadero.Text = gConvert.FormatoNumerico(TxtSubTotalVerdadero.Text, False)
   txtMontoTotalConDescuento.Text = gConvert.FormatoNumerico(txtMontoTotalConDescuento.Text, False)
   lblIVA.Caption = "IVA " & gAdmAlicuotaIvaActual.GetAlicuotaIVA(gConvert.fConvertStringToDate(txtFecha), eTD_ALICUOTAGENERAL) & "% Sobre Bs."
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "GroupFooter1_Format()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

'Private Sub sOutPutFormatCamposNumericos()
'   On Error GoTo h_ERROR
'    txtTotal.OutputFormat = gUtilReports.getDefaultNumericFormat
'    txtPrecioUnitario.OutputFormat = gUtilReports.getDefaultNumericFormat
'    txtPorcentajeDescuento.OutputFormat = gUtilReports.getDefaultNumericFormat
'    txtSubTotal.OutputFormat = gUtilReports.getDefaultNumericFormat
'    txtCantidadParaIVA.OutputFormat = gUtilReports.getDefaultNumericFormat
'    txtIVACalculado.OutputFormat = gUtilReports.getDefaultNumericFormat
'    txtTotalBs.OutputFormat = gUtilReports.getDefaultNumericFormat
'    txtPorcentajeAlicuota.OutputFormat = gUtilReports.getDefaultNumericFormat
'    If gProyParametrosCompania.GetUsarDecimalesAlImprimirCantidad Then
'      txtCantidad.OutputFormat = gUtilReports.getDefaultNumericFormat
'    Else
'      txtCantidad.OutputFormat = "#,##0"
'    End If
'h_EXIT: On Error GoTo 0
'   Exit Sub
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "sOutPutFormatCamposNumericos()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Sub

Private Sub sVerificarDescuentoXRenglon()
'   Dim insRenglonFactura As clsRenglonFacturaNavigator
   Dim mrsRenglonFactura As ADODB.Recordset
   Dim SQL As String
'   Set insRenglonFactura = New clsRenglonFacturaNavigator
   gRenglon.setClaseDeTrabajo eCTFC_Factura
   Set mrsRenglonFactura = New ADODB.Recordset
   On Error GoTo h_ERROR
   If Not DataControl1.Recordset.EOF Then
      SQL = "SELECT  MAX (" & gRenglon.getFN_PORCENTAJE_DESCUENTO() & ") AS MaximoPorcentaje "
      SQL = SQL & " FROM " & gRenglon.GetTableName()
      SQL = SQL & " WHERE " & gRenglon.getFN_NUMERO_FACTURA() & " = " _
                     & "'" & DataControl1.Recordset!NumeroFactura & "'"
      SQL = SQL & " AND " & gRenglon.getFN_CONSECUTIVO_COMPANIA() & " = " _
                     & mConsecutivoCompania
      gDbUtil.openRecordSet mrsRenglonFactura, SQL
      If mrsRenglonFactura.RecordCount > 0 Then
         If mrsRenglonFactura!MaximoPorcentaje = 0 Then
            lblPorcentajeDescuento.Visible = False
            txtPorcentajeDescuento.Visible = False
         End If
      End If
   End If
'   Set insRenglonFactura = Nothing
   gDbUtil.sCloseIfOpened mrsRenglonFactura
   Set mrsRenglonFactura = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sVerificarDescuentoXRenglon", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub PageHeader_Format()
   On Error GoTo h_ERROR
   If Not DataControl1.Recordset.EOF Then
      If LenB(DataControl1.Recordset!Observacion) = 0 Then
         lblObservaciones.Visible = False
         txtObservaciones.Visible = False
      Else
         lblObservaciones.Visible = True
         txtObservaciones.Visible = True
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "PageHeader_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
