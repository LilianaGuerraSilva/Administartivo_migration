VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptFacturacionPorVendedor 
   Caption         =   "Facturación Por Vendedor"
   ClientHeight    =   12450
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   17160
   WindowState     =   2  'Maximized
   _ExtentX        =   30268
   _ExtentY        =   21960
   SectionData     =   "rptFacturacionPorVendedor.dsx":0000
End
Attribute VB_Name = "rptFacturacionPorVendedor"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "rptFacturacionPorVendedor"
Private Const CM_MESSAGE_NAME As String = "Reporte Facturacion Por Vendedor"
Private Const ERR_NOHAYIMPRESORA = 5007

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sConfigurarDatosDelReporte(ByVal valSqlDelReporte As String, _
                                        ByVal valFechaInicial As Date, _
                                          ByVal valFechaFinal As Date, _
                                            ByVal valNombreCompaniaParaInformes As String, _
                                              ByVal valUsaCodigoVendedorEnPantalla As Boolean, _
                                                ByRef gGlobalization As Object)
   Dim varLeft As Integer
   On Error GoTo h_ERROR
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "dcOrigenData", valSqlDelReporte) Then
'      txtCodigoVendedor.Visible = gProyParametros.GetUsaCodigoVendedorEnPantalla
      txtCodigoVendedor.Visible = valUsaCodigoVendedorEnPantalla
      gUtilReports.sConfiguraEncabezado Me, "lblNombreDeLaCiaActual", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "LblNumeroDePagina", "lblFechaInicialYFinal", False, True
      gUtilReports.sDefaultConfigurationForLabels Me, "lblNombreDeLaCiaActual", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForLabels Me, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
      gUtilReports.sDefaultConfigurationForLabels Me, "lblMontoIVA", "Monto " & gGlobalization.fPromptIVA
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtCodigoVendedor", "", "CodigoVendedor"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNombreVendedor", "", "NombreVendedor"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtCod", "", "CodigoCliente"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNombreCliente", "", "NombreCliente"
      gUtilReports.sDefaultConfigurationForDateFields Me, "txtFecha", "", "Fecha", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNoDocumento", "", "Numero"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtMontoExento", "", "TotalMontoExent"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtMontoGravado", "", "TotalBaseImp"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtMontoIVA", "", "TotalesIVA", 2
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalBs", "", "TotalFact", 2

      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalMontoExento", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalMontoGravado", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalMontoIVA", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalMontoExento", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalTotalBs", "", ""
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtMonedaDoc", "", "Moneda"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtCambio", "", "CambioABolivares"
      lblNumeroDePagina.Visible = True
      gUtilReports.sDefaultConfigurationForSummaryFields Me, "txtMontoExentoVend", "TotalMontoExent", eSF_SUM, "GroupHeader3", eSR_GROUP, eST_SUB_TOTAL
      gUtilReports.sDefaultConfigurationForSummaryFields Me, "txtGravadoVend", "TotalBaseImp", eSF_SUM, "GroupHeader3", eSR_GROUP, eST_SUB_TOTAL
      gUtilReports.sDefaultConfigurationForSummaryFields Me, "txtIvaVend", "TotalesIVA", eSF_SUM, "GroupHeader3", eSR_GROUP, eST_SUB_TOTAL
      gUtilReports.sDefaultConfigurationForSummaryFields Me, "txtTotalVend", "TotalFact", eSF_SUM, "GroupHeader3", eSR_GROUP, eST_SUB_TOTAL

      
      
'      GroupHeader2.DataField = "NombreVendedor"
'      GroupHeader2.GrpKeepTogether = ddGrpFirstDetail
'      GroupHeader2.KeepTogether = True
'      GroupHeader2.NewPage = ddNPNone
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter Me, "GroupHeader1", "Moneda", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter Me, "GroupHeader3", "NombreVendedor", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone

'      GroupHeader1.Repeat = ddRepeatOnPage
'      GroupHeader1.DataField = "Moneda"
'      GroupHeader1.Repeat = ddRepeatOnPage
'      GroupHeader1.KeepTogether = True
'      GroupHeader1.NewPage = ddNPNone
      txtTotalMontoExento.DataField = ("TotalMontoExent")
      txtTotalMontoGravado.DataField = ("TotalBaseImp")
      txtTotalMontoIVA.DataField = ("TotalesIVA")
      txtTotalTotalBs.DataField = ("TotalFact")
      'txtTotalMontoOriginal.DataField = ("MontoOriginal")
      txtMensajeDelCambioDeLaMoneda.Text = Trim(gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal))
      'fOcultarMontoExento
      fOcultarMontoOriginal
'      Me.PageSettings.Orientation = ddOLandscape
      txtMensajeDelCambioDeLaMoneda.Visible = True
      sReordenarCamposSiUsaMoneExt
      'Me.PrintWidth = 12000
'      lblTotalVendedor.Left = 3400
      gUtilMargins.sAsignarMargenesGenerales Me, ddODefault
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ConfigurarDatosDelReporte", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_Terminate()
   On Error GoTo h_ERROR
   WindowState = vbNormal
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActiveReport_Te rminate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_NoData()
   On Error GoTo h_ERROR
   gMessage.InformationMessage "No se encontró información para imprimir", "RESULTADO"
   Cancel
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActiveReport_NoData", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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

Private Sub ActiveReport_PageStart()
   On Error GoTo h_ERROR
   lblNumeroDePagina.Caption = "Pág " & pageNumber
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "ActiveReport_PageStar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Detail_Format()
   On Error GoTo h_ERROR
   txtMontoExento.Text = gConvert.FormatoNumerico(CStr(txtMontoExento), False)
   txtMontoGravado.Text = gConvert.FormatoNumerico(CStr(txtMontoGravado), False)
   txtMontoIVA.Text = gConvert.FormatoNumerico(CStr(txtMontoIVA), False)
   txtTotalBs.Text = gConvert.FormatoNumerico(CStr(txtTotalBs), False)
   txtFecha.Text = gConvert.dateToStringYY(txtFecha)
   txtCambio.Text = gConvert.FormatoNumerico(CStr(txtCambio), False)
   'txtMontoOriginal = gConvert.FormatoNumerico(CStr(txtMontoOriginal), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Detail_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupFooter1_BeforePrint()
   On Error GoTo h_ERROR
   lblTotalMoneda.Caption = "Totales  " & txtMonedaDoc.DataValue
   txtTotalMontoExento.Text = gConvert.FormatoNumerico(CStr(txtTotalMontoExento), False)
   txtTotalMontoGravado.Text = gConvert.FormatoNumerico(CStr(txtTotalMontoGravado), False)
   txtTotalMontoIVA.Text = gConvert.FormatoNumerico(CStr(txtTotalMontoIVA), False)
   txtTotalTotalBs.Text = gConvert.FormatoNumerico(CStr(txtTotalTotalBs), False)
   'txtTotalMontoOriginal.Text = gConvert.FormatoNumerico(CStr(txtTotalMontoOriginal), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "GroupFooter1_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sReordenarCamposSiUsaMoneExt()
   On Error GoTo h_ERROR
   'lblMoneda.Visible = True
   lblCambio.Visible = True
   txtCambio.Visible = True
   'txtMoneda.Visible = True
'   lblMontoOriginal.Visible = True
   'txtTotalMontoOriginal.Visible = True
   'txtTotalMontoOriginal.Left = txtMontoOriginal.Left
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ReordenarCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fOcultarMontoExento() As Boolean
   On Error GoTo h_ERROR
   fOcultarMontoExento = False
   If Not dcOrigenData.Recordset.EOF Then
      If dcOrigenData.Recordset!TotalMontoExent <= 0 Then
         lblMontoExento.Visible = False
         txtMontoExento.Visible = False
         txtTotalMontoExento.Visible = False
         fOcultarMontoExento = True
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fOcultarMontoExento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fOcultarMontoOriginal() As Boolean
   On Error GoTo h_ERROR
   fOcultarMontoOriginal = False
   If Not dcOrigenData.Recordset.EOF Then
      If dcOrigenData.Recordset!TotalMontoExent <= 0 Then
         'lblMontoOriginal.Visible = False
         'txtMontoOriginal.Visible = False
         fOcultarMontoOriginal = True
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fOcultarMontoExento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sOrganizarCamposSiNoUsaMonedaExt(ByVal valLeft As Integer)
   On Error GoTo h_ERROR
   lblMontoExento.Left = lblMontoExento.Left - valLeft
   txtMontoExento.Left = lblMontoExento.Left
   lblMontoGravado.Left = lblMontoGravado.Left - valLeft
   txtMontoGravado.Left = lblMontoGravado.Left
   lblMontoIVA.Left = lblMontoIVA.Left - valLeft
   txtMontoIVA.Left = lblMontoIVA.Left
   lblTotalBS.Left = lblTotalBS.Left - valLeft
   txtTotalBs.Left = lblTotalBS.Left
   txtTotalMontoExento.Left = txtMontoExento.Left
   txtTotalMontoGravado.Left = txtMontoGravado.Left
   txtTotalMontoIVA.Left = txtMontoIVA.Left
   txtTotalTotalBs.Left = txtTotalBs.Left
   If fOcultarMontoExento Then
'      Line1.X2 = 9500
'   Else
'      Line1.X2 = 10200
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
        "sOrganizarCamposSiNoUsaMonedaExt", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


Private Sub GroupFooter3_BeforePrint()
   On Error GoTo h_ERROR
   lblTotalVendedor.Caption = "Totales  " & txtNombreVendedor.DataValue
   txtMontoExentoVend.Text = gConvert.FormatoNumerico(CStr(txtMontoExentoVend), False)
   txtGravadoVend.Text = gConvert.FormatoNumerico(CStr(txtGravadoVend), False)
   txtIvaVend.Text = gConvert.FormatoNumerico(CStr(txtIvaVend), False)
   txtTotalVend.Text = gConvert.FormatoNumerico(CStr(txtTotalVend), False)
   'txtTotalMontoOriginal.Text = gConvert.FormatoNumerico(CStr(txtTotalMontoOriginal), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "GroupFooter3_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

