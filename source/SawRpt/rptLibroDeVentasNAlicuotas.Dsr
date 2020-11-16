VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptLibroDeVentasNAlicuotas 
   Caption         =   "Libro de Ventas"
   ClientHeight    =   11010
   ClientLeft      =   510
   ClientTop       =   0
   ClientWidth     =   15240
   WindowState     =   2  'Maximized
   _ExtentX        =   26882
   _ExtentY        =   19420
   SectionData     =   "rptLibroDeVentasNAlicuotas.dsx":0000
End
Attribute VB_Name = "rptLibroDeVentasNAlicuotas"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "rptLibroDeVentasNAlicuotas"
Private Const CM_MESSAGE_NAME As String = "Informe de Ventas"
Private Const ERR_NOHAYIMPRESORA = 5007
Private mFechaDeHoy As String
Private mRepImprimirDetalles As Boolean
Private mRepFechaInicioMes As Date
Private mTotalVentas As Currency
Private mTotalBase As Currency
Private mTotalNoCausaImp As Currency
Private mTotalImpuesto As Currency
Private mTotalVentasAlicuotaGeneral As Currency
Private mTotalBaseAlicuotaGeneral As Currency
Private mTotalNoCausaImpAlicuotaGeneral As Currency
Private mTotalImpuestoAlicuotaGeneral As Currency
Private mTotalVentasAlicuota2 As Currency
Private mTotalBaseAlicuota2 As Currency
Private mTotalImpuestoAlicuota2 As Currency
Private mTotalVentasAlicuota3 As Currency
Private mTotalBaseAlicuota3 As Currency
Private mTotalImpuestoAlicuota3 As Currency
Private mBaseImponibleAlicuotaGeneral As Currency
Private mBaseImponibleAlicuota2 As Currency
Private mBaseImponibleAlicuota3 As Currency
Private mMontoIVAAlicuotaGeneral As Currency
Private mMontoIVAAlicuota2 As Currency
Private mMontoIVAAlicuota3 As Currency
Private mPorcentajeIVAAlicuotaGeneral As Currency
Private mPorcentajeIVAAlicuota2 As Currency
Private mPorcentajeIVAAlicuota3 As Currency
Private gFacturaNav As Object
Private gAdmAlicuotaIvaActual As Object
'Private insFacturaNavigator As clsFacturaNavigator
Public Sub sConfigurarDatosDelReporte(ByVal valSqlDelReporte As String, ByVal valImprimirDetalles As Boolean, ByVal valMes As String, ByVal ValAno As String, ByVal valNombreCompaniaParaInformes As String, ByRef refInsFactura As Object, ByRef refInsAdmAlicuotaIvaActual As Object, ByRef gGlobalization As Object)
   On Error GoTo h_ERROR
   Set gFacturaNav = refInsFactura
   Set gAdmAlicuotaIvaActual = refInsAdmAlicuotaIvaActual
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "dcOrigenData", valSqlDelReporte) Then
      mRepImprimirDetalles = valImprimirDetalles
      mRepFechaInicioMes = gConvert.fConvertStringToDate("01/" & valMes & "/" & ValAno)
      gUtilReports.sDefaultConfigurationForLabels Me, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultConfigurationForLabels Me, "lblNombreCliente", "Nombre Cliente /    Nº. " & gGlobalization.fPromptIVA & ":"
      gUtilReports.sDefaultConfigurationForLabels Me, "lblTotalVentas", "Total Ventas Incluyendo " & gGlobalization.fPromptIVA
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaYHoraDeEmision"
      gUtilReports.sConfiguraEncabezado Me, "lblCompania", "lblFechaYHoraDeEmision", "lblTitulo", "LblNumeroDePagina", "", False, True
      If Not mRepImprimirDetalles Then
         PageHeader.Height = 1800
         lblNombreDelReporte.Caption = "Libro de Ventas del Mes de " & gEnumReport.enumMesToString(Month(mRepFechaInicioMes))
      Else
         lblNombreDelReporte.Caption = lblNombreDelReporte.Caption & " " & gEnumReport.enumMesToString(Month(mRepFechaInicioMes))
      End If
      lblMensajeTasaDeCambio.Caption = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNoFactura", "", "Numero"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNombreCliente", "", "Nombre"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalVentas", "", "TotalFacturaConMoneda"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNoRIF", "", "NumeroRIF"
      gUtilReports.sDefaultConfigurationForDateFields Me, "txtDia", "", "Fecha", eFT_DATE
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtNoCausaImp", "", "MontoExentoConMoneda"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtCantidad", "", "Cantidad"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtCostoUnitario", "", "PreciosSinIvaConMoneda"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtDescripcion", "", "Descripcion"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtMontoTotalVentas", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalBaseContribuyentes", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalImpuestoContribuyentes", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalNoCausaImp", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalVentas", "", ""
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter Me, "GHNumeroFactura", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
'      GHNumeroFactura.DataField = "Numero"
'      Set insFacturaNavigator = New clsFacturaNavigator
'      insFacturaNavigator.setClaseDeTrabajo eCTFC_Factura
      sConfiguraElDetalle
      sConfiguraLosPorcentajesDeLasAlicuotasDelIVA
      sInicializaLosValoresDeLosCamposNoContribuyentes
      gUtilMargins.sAsignarMargenesGenerales Me
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sConfigurarDatosDelReporte", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function GetGender() As Enum_Gender
   On Error GoTo h_ERROR
   GetGender = eg_Male
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GetGender", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub ActiveReport_Terminate()
   On Error GoTo h_ERROR
'   Set insFacturaNavigator = Nothing
   WindowState = vbNormal
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActiveReport_Terminate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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

Private Sub Detail_Format()
   On Error GoTo h_ERROR
   If mRepImprimirDetalles = True Then
      Detail.Visible = True
   End If
   If mRepImprimirDetalles = True Then
      If DcOrigenData.Recordset!statusFactura = eSF_ANULADA Then
        Detail.Visible = False
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Detail_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GHNumeroFactura_AfterPrint()
   On Error GoTo h_ERROR
   lblAnulada.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GHNumeroFactura_AfterPrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GHNumeroFactura_BeforePrint()
   On Error GoTo h_ERROR
'   txtDia.Text = gTexto.DfMid((gConvert.dateToStringYY(txtDia.Text)), 1, 5)
   txtTotalVentas.Text = gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(txtTotalVentas.DataValue), False)
   txtNoCausaImp.Text = gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(txtNoCausaImp.DataValue), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GHNumeroFactura_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_PageStart()
   On Error GoTo h_ERROR
   lblNumeroPagina.Caption = "Pág. " & pageNumber
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActiveReport_PageStar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GHNumeroFactura_Format()
   Dim cambioABolivares As Currency
   On Error GoTo h_ERROR
   cambioABolivares = 1
   If gFacturaNav.fSearchByNumeroTipoDocumento(txtNoFactura.DataValue, eTF_NOASIGNADO, False) Then
      gFacturaNav.sCalculaTotalesFactura False
   End If
   If Not DcOrigenData.Recordset.EOF Then
      If DcOrigenData.Recordset!statusFactura = eSF_ANULADA Then
         txtTotalVentas.DataValue = 0
         txtNoCausaImp.DataValue = 0
         lblAnulada.Visible = True
         mMontoIVAAlicuotaGeneral = 0
         mMontoIVAAlicuota2 = 0
         mMontoIVAAlicuota3 = 0
         mBaseImponibleAlicuotaGeneral = 0
         mBaseImponibleAlicuota2 = 0
         mBaseImponibleAlicuota3 = 0
      Else
         lblAnulada.Visible = False
         cambioABolivares = gConvert.fConvierteACurrency(DcOrigenData.Recordset(gFacturaNav.getFN_CAMBIO_ABOLIVARES).Value)
         mMontoIVAAlicuotaGeneral = gFacturaNav.GetTotalIVAAlicuotaX(eTD_ALICUOTAGENERAL) * cambioABolivares
         mMontoIVAAlicuota2 = gFacturaNav.GetTotalIVAAlicuotaX(eTD_ALICUOTA2) * cambioABolivares
         mMontoIVAAlicuota3 = gFacturaNav.GetTotalIVAAlicuotaX(eTD_ALICUOTA3) * cambioABolivares
         mBaseImponibleAlicuotaGeneral = gFacturaNav.GetBaseImponibleAlicuotaXDespuesDescuento(eTD_ALICUOTAGENERAL) * cambioABolivares
         mBaseImponibleAlicuota2 = gFacturaNav.GetBaseImponibleAlicuotaXDespuesDescuento(eTD_ALICUOTA2) * cambioABolivares
         mBaseImponibleAlicuota3 = gFacturaNav.GetBaseImponibleAlicuotaXDespuesDescuento(eTD_ALICUOTA3) * cambioABolivares
      End If
   End If
   txtBasesImponiblesConcatenadasAContribuyentes.Text = ""
   txtImpuestosConcatenadosAContribuyentes.Text = ""
   txtPorcentajesAlicuotasConcatenadosAContribuyentes.Text = ""
   If (mBaseImponibleAlicuotaGeneral = 0 And mBaseImponibleAlicuota2 = 0 And mBaseImponibleAlicuota3 = 0) Then
      txtBasesImponiblesConcatenadasAContribuyentes.Text = "0.00"
      txtImpuestosConcatenadosAContribuyentes.Text = "0.00"
      txtPorcentajesAlicuotasConcatenadosAContribuyentes.Text = "0.00"
   Else
      If mBaseImponibleAlicuotaGeneral <> 0 Then
         txtBasesImponiblesConcatenadasAContribuyentes.Text = txtBasesImponiblesConcatenadasAContribuyentes.Text & gConvert.fNumToStringConSeparadorDeMiles(mBaseImponibleAlicuotaGeneral) & vbCrLf
         txtImpuestosConcatenadosAContribuyentes.Text = txtImpuestosConcatenadosAContribuyentes.Text & gConvert.fNumToStringConSeparadorDeMiles(mMontoIVAAlicuotaGeneral) & vbCrLf
         txtPorcentajesAlicuotasConcatenadosAContribuyentes.Text = txtPorcentajesAlicuotasConcatenadosAContribuyentes.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuotaGeneral) & vbCrLf
      End If
      If mBaseImponibleAlicuota2 <> 0 Then
         txtBasesImponiblesConcatenadasAContribuyentes.Text = txtBasesImponiblesConcatenadasAContribuyentes.Text & gConvert.fNumToStringConSeparadorDeMiles(mBaseImponibleAlicuota2) & vbCrLf
         txtImpuestosConcatenadosAContribuyentes.Text = txtImpuestosConcatenadosAContribuyentes.Text & gConvert.fNumToStringConSeparadorDeMiles(mMontoIVAAlicuota2) & vbCrLf
         txtPorcentajesAlicuotasConcatenadosAContribuyentes.Text = txtPorcentajesAlicuotasConcatenadosAContribuyentes.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuota2) & vbCrLf
      End If
      If mBaseImponibleAlicuota3 <> 0 Then
         txtBasesImponiblesConcatenadasAContribuyentes.Text = txtBasesImponiblesConcatenadasAContribuyentes.Text & gConvert.fNumToStringConSeparadorDeMiles(mBaseImponibleAlicuota3) & vbCrLf
         txtImpuestosConcatenadosAContribuyentes.Text = txtImpuestosConcatenadosAContribuyentes.Text & gConvert.fNumToStringConSeparadorDeMiles(mMontoIVAAlicuota3) & vbCrLf
         txtPorcentajesAlicuotasConcatenadosAContribuyentes.Text = txtPorcentajesAlicuotasConcatenadosAContribuyentes.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuota3) & vbCrLf
      End If
   End If
   mTotalVentas = mTotalVentas + gConvert.fConvierteACurrency(txtTotalVentas.DataValue)
   mTotalNoCausaImp = mTotalNoCausaImp + gConvert.fConvierteACurrency(txtNoCausaImp.DataValue)
   mTotalBaseAlicuotaGeneral = mTotalBaseAlicuotaGeneral + mBaseImponibleAlicuotaGeneral
   mTotalImpuestoAlicuotaGeneral = mTotalImpuestoAlicuotaGeneral + mMontoIVAAlicuotaGeneral
   mTotalBaseAlicuota2 = mTotalBaseAlicuota2 + mBaseImponibleAlicuota2
   mTotalImpuestoAlicuota2 = mTotalImpuestoAlicuota2 + mMontoIVAAlicuota2
   mTotalBaseAlicuota3 = mTotalBaseAlicuota3 + mBaseImponibleAlicuota3
   mTotalImpuestoAlicuota3 = mTotalImpuestoAlicuota3 + mMontoIVAAlicuota3
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GHNumeroFactura_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ReportFooter_BeforePrint()
   On Error GoTo h_ERROR
   txtTotalNoCausaImpuestoAlicuotaGeneral.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalNoCausaImp)
   txtTotalBaseImponibleAlicuotaGeneralContribuyentes.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalBaseAlicuotaGeneral)
   txtTotalImpuestoAlicuotaGeneralContribuyentes.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalImpuestoAlicuotaGeneral)
   mTotalVentasAlicuotaGeneral = mTotalBaseAlicuotaGeneral + mTotalImpuestoAlicuotaGeneral + mTotalNoCausaImp
   txtMontoTotalAlicuotaGeneral.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalVentasAlicuotaGeneral)
   txtTotalBaseImponibleAlicuota2Contribuyentes.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalBaseAlicuota2)
   txtTotalImpuestoAlicuota2Contribuyentes.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalImpuestoAlicuota2)
   mTotalVentasAlicuota2 = mTotalBaseAlicuota2 + mTotalImpuestoAlicuota2
   txtMontoTotalAlicuota2.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalVentasAlicuota2)
   txtTotalBaseImponibleAlicuota3Contribuyentes.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalBaseAlicuota3)
   txtTotalImpuestoAlicuota3Contribuyentes.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalImpuestoAlicuota3)
   mTotalVentasAlicuota3 = mTotalBaseAlicuota3 + mTotalImpuestoAlicuota3
   txtMontoTotalAlicuota3.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalVentasAlicuota3)
   mTotalBase = mTotalBaseAlicuotaGeneral + mTotalBaseAlicuota2 + mTotalBaseAlicuota3
   mTotalImpuesto = mTotalImpuestoAlicuotaGeneral + mTotalImpuestoAlicuota2 + mTotalImpuestoAlicuota3
   mTotalVentas = mTotalVentasAlicuotaGeneral + mTotalVentasAlicuota2 + mTotalVentasAlicuota3
   txtTotalNoCausaImp.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalNoCausaImp)
   txtTotalBaseContribuyentes.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalBase)
   txtTotalImpuestoContribuyentes.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalImpuesto)
   txtMontoTotalVentas.Text = gConvert.fNumToStringConSeparadorDeMiles(mTotalVentas)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ReportFooter_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sConfiguraLosMargenes()
   On Error GoTo h_ERROR
   Me.PageSettings.LeftMargin = 50
   Me.PageSettings.RightMargin = 100
   Me.PrintWidth = 12000
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sConfiguraLosMargenes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
'
'Private Sub sConfiguraElEncabezado()
'   On Error GoTo h_ERROR
'h_EXIT: On Error GoTo 0
'   Exit Sub
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "sConfiguraElEncabezado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Sub

Private Sub sConfiguraElDetalle()
   On Error GoTo h_ERROR
   If Not mRepImprimirDetalles Then
      Detail.Height = 0
      Detail.Visible = False
      lblCantidad.Height = 0
      lblCostoUnitario.Height = 0
      lblDescripcion.Height = 0
      lblRayas.Height = 0
      GFNumeroFactura.Visible = False
      lineDetail.Visible = False
   Else
      linePuntos2.Visible = False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sConfiguraElDetalle", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sConfiguraLosPorcentajesDeLasAlicuotasDelIVA()
   On Error GoTo h_ERROR
   mPorcentajeIVAAlicuotaGeneral = gConvert.fNumToStringConSeparadorDeMiles(gAdmAlicuotaIvaActual.GetAlicuotaIVA(mRepFechaInicioMes, eTD_ALICUOTAGENERAL))
   mPorcentajeIVAAlicuota2 = gConvert.fNumToStringConSeparadorDeMiles(gAdmAlicuotaIvaActual.GetAlicuotaIVA(mRepFechaInicioMes, eTD_ALICUOTA2))
   mPorcentajeIVAAlicuota3 = gConvert.fNumToStringConSeparadorDeMiles(gAdmAlicuotaIvaActual.GetAlicuotaIVA(mRepFechaInicioMes, eTD_ALICUOTA3))
   lblTotalesAlicuota3.Caption = lblTotalesAlicuotaGeneral.Caption
   lblTotalesAlicuotaGeneral.Caption = lblTotalesAlicuotaGeneral.Caption & " " & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuotaGeneral) & " %"
   lblTotalesAlicuota2.Caption = lblTotalesAlicuota2.Caption & " " & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuota2) & "% "
   lblTotalesAlicuota3.Caption = lblTotalesAlicuota3.Caption & " + " & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuota3 - mPorcentajeIVAAlicuotaGeneral) & "% "
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sConfiguraLosPorcentajesDeLasAlicuotasDelIVA", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInicializaLosValoresDeLosCamposNoContribuyentes()
   On Error GoTo h_ERROR
   txtBasesImponiblesConcatenadaANoContribuyentes.Text = ""
   txtImpuestosConcatenadosANoContribuyentes.Text = ""
   txtPorcentajesAlicuotasConcatenadosANoContribuyentes.Text = ""
   txtTotalBaseImponibleAlicuotaGeneralNoContribuyentes.Text = "0.00"
   txtTotalBaseImponibleAlicuota2NoContribuyentes.Text = "0.00"
   txtTotalBaseImponibleAlicuota3NoContribuyentes.Text = "0.00"
   txtTotalImpuestoAlicuotaGeneralNoContribuyentes.Text = "0.00"
   txtTotalImpuestoAlicuota2NoContribuyentes.Text = "0.00"
   txtTotalImpuestoAlicuota3NoContribuyentes.Text = "0.00"
   txtTotalBaseNoContribuyentes.Text = "0.00"
   txtTotalImpuestoNoContribuyentes.Text = "0.00"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sInicializaLosValoresDeLosCamposNoContribuyentes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
