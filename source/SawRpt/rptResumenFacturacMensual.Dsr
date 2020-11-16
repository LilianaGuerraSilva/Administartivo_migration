VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptResumenFacturacMensual 
   Caption         =   "Facturación Acumulada Por Meses"
   ClientHeight    =   12450
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   17160
   WindowState     =   2  'Maximized
   _ExtentX        =   30268
   _ExtentY        =   21960
   SectionData     =   "rptResumenFacturacMensual.dsx":0000
End
Attribute VB_Name = "rptResumenFacturacMensual"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "rptResumenFacturacMensual"
Private Const CM_MESSAGE_NAME As String = "Reporte De Facturacion Por Mes"
Private Const ERR_NOHAYIMPRESORA = 5007
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sConfigurarDatosDelReporte(ByVal valSqlDelReporte As String, ByVal valMesDesde As String, ByVal valMesHasta As String, ByVal valAnoDesde As String, ByVal valAnoHasta As String, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaCambio As Boolean, ByRef gGlobalization As Object)
   On Error GoTo h_ERROR
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "dcOrigenData", valSqlDelReporte) Then
      gUtilReports.sDefaultConfigurationForLabels Me, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultConfigurationForLabels Me, "lblMontoIVA", "Monto " & gGlobalization.fPromptIVA
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForLabels Me, "lblFechaInicialYFinal", "Del " & valMesDesde & "/" & valAnoDesde & " Hasta " & valMesHasta & "/" & valAnoHasta
      gUtilReports.sConfiguraEncabezado Me, "lblCompania", "lblFechaYHoraDeEmision", "lblInforme", "LblNumeroDePagina", "lblFechaInicialYFinal", False, True
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtMontoExento", "", "MontoEx"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtMontoGravable", "", "BaseImp"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtMontoIVA", "", "MontoIVA"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalDelMes", "", "TotalFact"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtNetoDelMes", "", "TotalNetoFact"
      '****Verificar LenB antes estaba con gTexto
      If LenB(valMesDesde) < 2 Then
         valMesDesde = "0" & valMesDesde
      End If
      If LenB(valMesHasta) < 2 Then
         valMesHasta = "0" & valMesHasta
      End If
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtMoneda", "", "MonedaReporte"
      If valUsaCambio Then
         gUtilReports.sDefaultConfigurationForLabels Me, "lblNotaDeCambio", gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
         gUtilReports.sConfigurationForNotvisibleOrVisible Me, "lblNotaDeCambio", True
      Else
         gUtilReports.sDefaultConfigurationForLabels Me, "lblNotaDeCambio", ""
         gUtilReports.sConfigurationForNotvisibleOrVisible Me, "lblNotaDeCambio", False
      End If
'      lblMensajeTasaDeCambio.Visible = True
'      lblMensajeTasaDeCambio.Caption = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalMontoExento", "", "MontoEx"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalMontoGravable", "", "BaseImp"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalMontoIVA", "", "MontoIVA"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalTotalDelMes", "", "TotalFact"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalNetoDelMes", "", "TotalNetoFact"
      gUtilMargins.sAsignarMargenesGenerales Me
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
   Dim Mes As String
   Dim Año As String
   On Error GoTo h_ERROR
   Año = DcOrigenData.Recordset!ano
   Mes = DcOrigenData.Recordset!Mes
   Mes = gEnumReport.enumMesToString(Mes)
   txtMesAno.Text = Mes & "-" & Año
   txtMontoExento.Text = gConvert.FormatoNumerico(CStr(txtMontoExento), False)
   txtMontoGravable.Text = gConvert.FormatoNumerico(CStr(txtMontoGravable), False)
   txtMontoIVA.Text = gConvert.FormatoNumerico(CStr(txtMontoIVA), False)
   txtTotalDelMes.Text = gConvert.FormatoNumerico(CStr(txtTotalDelMes), False)
   txtNetoDelMes.Text = gConvert.FormatoNumerico(CStr(txtNetoDelMes), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
    "Detail_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ReportFooter_BeforePrint()
   On Error GoTo h_ERROR
   txtTotalMontoExento.Text = gConvert.FormatoNumerico(CStr(txtTotalMontoExento), False)
   txtTotalMontoGravable.Text = gConvert.FormatoNumerico(CStr(txtTotalMontoGravable), False)
   txtTotalMontoIVA.Text = gConvert.FormatoNumerico(CStr(txtTotalMontoIVA), False)
   txtTotalTotalDelMes.Text = gConvert.FormatoNumerico(CStr(txtTotalTotalDelMes), False)
   txtTotalNetoDelMes.Text = gConvert.FormatoNumerico(CStr(txtTotalNetoDelMes), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
    "ReportFooter_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

