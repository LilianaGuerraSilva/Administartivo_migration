VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptLibroDeVentas 
   Caption         =   "Libro de Ventas"
   ClientHeight    =   10950
   ClientLeft      =   225
   ClientTop       =   555
   ClientWidth     =   17160
   StartUpPosition =   3  'Windows Default
   WindowState     =   2  'Maximized
   _ExtentX        =   30268
   _ExtentY        =   19315
   SectionData     =   "rptLibroDeVentas.dsx":0000
End
Attribute VB_Name = "rptLibroDeVentas"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private fechaDeHoy As String
Private mRepImprimirDetalles As Boolean
Private totalVentas As Currency
Private totalBase As Currency
Private totalNoCausaImp As Currency
Private totalImpuesto As Currency
Private Const CM_FILE_NAME As String = "rptLibroDeVentas"
Private Const CM_MESSAGE_NAME As String = "Libro de Ventas"
Private Const ERR_NOHAYIMPRESORA = 5007
Private mRepFechaInicioMes As Date
Public Sub sConfigurarDatosDelReporte(ByVal valSqlDelReporte As String, ByVal valMes As String, ByVal ValAno As String, ByVal valNombreCompaniaParaInformes As String, ByVal refInsAdmAlicuotaIvaActual As Object, ByRef gGlobalization As Object)
   On Error GoTo h_ERROR
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "dcOrigenData", valSqlDelReporte) Then
      gUtilReports.sDefaultConfigurationForLabels Me, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultConfigurationForLabels Me, "lblNoRIF", "No " & gGlobalization.fPromptRIF
      gUtilReports.sDefaultConfigurationForLabels Me, "lblTotalVentas", "Total Ventas Incluyendo " & gGlobalization.fPromptIVA
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaYHoraDeEmision"
      gUtilReports.sConfiguraEncabezado Me, "lblCompania", "lblFechaYHoraDeEmision", "lblInforme", "LblNumeroDePagina", "lblFechaInicialYFinal", True, True
      mRepFechaInicioMes = gConvert.fConvertStringToDate("01/" & valMes & "/" & ValAno)
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNoFactura", "", "Numero"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNombreCliente", "", "Nombre"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalVentas", "", "TotalFacturaConMoneda"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNoRif", "", "NumeroRIF"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtDiferida", "", "EsDiferida"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtAplicaDecretoIvaEspecial", "", "AplicaDecretoIvaEspecial"
      gUtilReports.sDefaultConfigurationForDateFields Me, "txtDia", "", "Fecha", eFT_DATE
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtNoCausaImp", "", "MontoExentoConMoneda"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtBase", "", "BaseConMoneda", 2
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtImpuesto", "", "TotalIVAConMoneda"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtCantidad", "", "Cantidad"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtCostoUnitario", "", "PreciosSinIvaConMoneda"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtDescripcion", "", "Descripcion"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtMomtoTotalVentas", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalBase", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalImpuesto", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalNoCausaImp", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalVentas", "", ""
      lblMensajeTasaDeCambio.Caption = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
      GroupHeader1.DataField = "Numero"
      lblTasaGeneral.Caption = lblTasaGeneral.Caption & "(" & _
         refInsAdmAlicuotaIvaActual.GetAlicuotaIVA(mRepFechaInicioMes, eTD_ALICUOTAGENERAL) & "%)"
      gUtilMargins.sAsignarMargenesGenerales Me
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
    "ConfigurarDatosDelReporte", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function GetGender() As Enum_Gender
  GetGender = eg_Male
End Function

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

Private Sub Detail_Format()
   On Error GoTo h_ERROR
   If mRepImprimirDetalles = True Then
      Detail.Visible = True
   End If
   If dcOrigenData.Recordset!statusFactura = eSF_ANULADA Then
     Detail.Visible = False
   Else
      Detail.Visible = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
    "Detail_Format()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupHeader1_AfterPrint()
 On Error GoTo h_ERROR
 lblAnulada.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
    "GroupHeader1_AfterPrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupHeader1_BeforePrint()
   On Error GoTo h_ERROR
'   txtDia.Text = gTexto.DfMid((gConvert.dateToStringYY(txtDia.Text)), 1, 5)
   txtTotalVentas.Text = gConvert.FormatoNumerico(CStr(CCur(txtTotalVentas)), False)
   txtNoCausaImp.Text = gConvert.FormatoNumerico(CStr(CCur(txtNoCausaImp)), False)
   txtBase.Text = gConvert.FormatoNumerico(CStr(CCur(txtBase)), False)
   txtImpuesto.Text = gConvert.FormatoNumerico(CStr(CCur(txtImpuesto)), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
    "GroupHeader1_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_PageStart()
   Dim Mes As Integer
   On Error GoTo h_ERROR
   lblNumeroPagina.Caption = "Pág " & pageNumber
   Mes = Month(CDate(txtDia.Text))
   If mRepImprimirDetalles = True Then
      lblInforme.Caption = lblInforme.Caption & " " & gEnumReport.enumMesToString(Mes)
   Else
      lblInforme.Caption = "Libro de Ventas del Mes de " & gEnumReport.enumMesToString(Mes)
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
    "ActiveReport_PageStar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupHeader1_Format()
   On Error GoTo h_ERROR
   If Not dcOrigenData.Recordset.EOF Then
      If dcOrigenData.Recordset!statusFactura = eSF_ANULADA Then
         txtTotalVentas.DataValue = 0
         txtBase.DataValue = 0
         txtNoCausaImp.DataValue = 0
         txtImpuesto.DataValue = 0
         lblAnulada.Visible = True
      Else
         lblAnulada.Visible = False
      End If
   End If
   totalVentas = totalVentas + CCur(txtTotalVentas.DataValue)
   totalBase = totalBase + CCur(txtBase.DataValue)
   totalNoCausaImp = totalNoCausaImp + CCur(txtNoCausaImp.DataValue)
   totalImpuesto = totalImpuesto + CCur(txtImpuesto.DataValue)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "GroupHeader1_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ReportFooter_BeforePrint()
 On Error GoTo h_ERROR
 txtMomtoTotalVentas.Text = gConvert.FormatoNumerico(CStr(CCur(totalVentas)), False)
 txtTotalNoCausaImp.Text = gConvert.FormatoNumerico(CStr(CCur(totalNoCausaImp)), False)
 txtTotalBase.Text = gConvert.FormatoNumerico(CStr(CCur(totalBase)), False)
 txtTotalImpuesto.Text = gConvert.FormatoNumerico(CStr(CCur(totalImpuesto)), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
    "ReportFooter_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

