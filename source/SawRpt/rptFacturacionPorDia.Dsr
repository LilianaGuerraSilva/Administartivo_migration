VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptFacturacionPorDia 
   Caption         =   "Facturación por Día"
   ClientHeight    =   14850
   ClientLeft      =   60
   ClientTop       =   90
   ClientWidth     =   19080
   WindowState     =   2  'Maximized
   _ExtentX        =   33655
   _ExtentY        =   26194
   SectionData     =   "rptFacturacionPorDia.dsx":0000
End
Attribute VB_Name = "rptFacturacionPorDia"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "rptFacturacionPorDia"
Private Const CM_MESSAGE_NAME As String = "Reporte Facturacion por Dia"
Private Const ERR_NOHAYIMPRESORA = 5007
Private mAcumuladorTotalFacturado As Currency
Private mAcumParaNumeroDeFacturas As Integer
Private mAcumuladorTotalFacturadoPorGrupo As Currency
Private mAcumParaNumeroDeFacturasPorGrupo As Integer
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Sub ActiveReport_Terminate()
   On Error GoTo h_ERROR
   WindowState = vbNormal
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActiveReport_Terminate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sConfigurarDatosDelReporte(ByVal valValorDelSql As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMonedaDeLosReportes As String, ByVal valNombreCompaniaParaInformes As String, ByVal valNombreMonedaLocal As String)
   On Error GoTo h_ERROR
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "DataControl1", valValorDelSql) Then
      gUtilReports.sDefaultConfigurationForLabels Me, "lblNombreDeLaCiaActual", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaEmisionYHoraActual"
      gUtilReports.sDefaultConfigurationForLabels Me, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
      gUtilReports.sConfiguraEncabezado Me, "lblNombreDeLaCiaActual", "lblFechaEmisionYHoraActual", "lblTitulo", "LblNumeroDePagina", "lblFechaInicialYFinal", False, True
      gUtilReports.sDefaultConfigurationForDateFields Me, "txtFecha", "", "Fecha", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNumeroDeFacturas", "", "Facturas"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtFacturaMayor", "", "FacturaMayor"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtMonto", "", "MontoTotal"
      If valMonedaDeLosReportes = gEnumReport.enumMonedaDeLosReportesToString(enum_MonedaDeLosReportes.eMR_EnMonedaOriginal, valNombreMonedaLocal) Then
         txtMensajeDelCambioDeLaMoneda.Visible = True
         txtMensajeDelCambioDeLaMoneda.Text = Trim(gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal))
         gUtilReports.sDefaultConfigurationForStrFields Me, "txtMonedaPorGrupo", "", "Moneda"
      ElseIf valMonedaDeLosReportes = gEnumReport.enumMonedaDeLosReportesToString(enum_MonedaDeLosReportes.eMR_EnBs, valNombreMonedaLocal) Then
'         gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalPorGrupoNumeroDeFacturas", "", ""
'         lblMonedaPorGrupo.Visible = False
'         txtMonedaPorGrupo.Visible = False
         GFMoneda.Height = 0
         GFMoneda.Visible = False
         gUtilReports.sDefaultConfigurationForStrFields Me, "txtMonedaPorGrupo", valNombreMonedaLocal, "Moneda"
      End If
'      mAcumuladorTotalFacturado = 0
'      mAcumParaNumeroDeFacturas = 0
'      mAcumuladorTotalFacturadoPorGrupo = 0
'      mAcumParaNumeroDeFacturasPorGrupo = 0
'      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalPorGrupo", "", ""
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter Me, "GHMoneda", "Moneda", ddGrpAll, ddRepeatOnPageIncludeNoDetail, True, ddNPAfter
      ReportFooter.Height = 510
      ReportFooter.Visible = True
      gUtilReports.sDefaultConfigurationForSummaryFields Me, "txtTotalDelReporte", "MontoTotal", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
      gUtilReports.sDefaultConfigurationForSummaryFields Me, "txtTotalDelReporte", " txtTotalNumeroDeFacturas", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtTotalNumeroDeFacturas", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalPorGrupoNumeroDeFacturas", "", ""
      gUtilMargins.sAsignarMargenesGenerales Me
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace _
   (Err.Description, CM_FILE_NAME, "sConfigurarDatosDelReporte", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   lblNumeroDePagina.Caption = "Pag. " & pageNumber
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace _
   (Err.Description, "ActiveReport_PageStart()", CM_FILE_NAME, CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub Detail_Format()
   Dim InsUtilDsr As clsUtilDsr
   On Error GoTo h_ERROR
   Set InsUtilDsr = New clsUtilDsr
   txtDiaDeLaSemana.Text = gUtilDate.fDayOfWeek(DataControl1.Recordset!Fecha)
   txtDiaDeLaSemana.Text = InsUtilDsr.enumDiaDeLaSemanaToString(txtDiaDeLaSemana.Text)
   If txtDiaDeLaSemana.Text = "Lunes" Then
      Line22.Visible = True
     Else
      Line22.Visible = False
   End If
   txtMonto.Text = gConvert.FormatoNumerico(CStr(txtMonto), False)
   txtFacturaMayor.Text = gConvert.FormatoNumerico(CStr(txtFacturaMayor), False)
   mAcumuladorTotalFacturadoPorGrupo = mAcumuladorTotalFacturadoPorGrupo + gConvert.fConvierteACurrency(txtMonto.Text)
   mAcumParaNumeroDeFacturasPorGrupo = mAcumParaNumeroDeFacturasPorGrupo + CInt(txtNumeroDeFacturas.Text)
   Set InsUtilDsr = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Detail_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GFMoneda_AfterPrint()
   On Error GoTo h_ERROR
'   If Not mMonedaDelReporte = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal) Then
'      mAcumuladorTotalFacturadoPorGrupo = 0
'      mAcumParaNumeroDeFacturasPorGrupo = 0
'   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "GFMoneda_afterPrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GFMoneda_BeforePrint()
   On Error GoTo h_ERROR
   txtTotalPorGrupo.Text = gConvert.FormatoNumerico(CStr(mAcumuladorTotalFacturadoPorGrupo), False)
   txtTotalPorGrupoNumeroDeFacturas.Text = CStr(mAcumParaNumeroDeFacturasPorGrupo)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
    "GFMoneda_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ReportFooter_Format()
   On Error GoTo h_ERROR
   mAcumParaNumeroDeFacturas = mAcumParaNumeroDeFacturas + mAcumParaNumeroDeFacturasPorGrupo
   mAcumuladorTotalFacturado = mAcumuladorTotalFacturado + mAcumuladorTotalFacturadoPorGrupo
   txtTotalDelReporte.Text = gConvert.FormatoNumerico(CStr(mAcumuladorTotalFacturado), False)
   txtTotalNumeroDeFacturas.Text = CStr(mAcumParaNumeroDeFacturas)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ReportFooter_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

