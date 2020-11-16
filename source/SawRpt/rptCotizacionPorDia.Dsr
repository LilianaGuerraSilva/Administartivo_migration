VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptCotizacionPorDia 
   Caption         =   "Cotización por Día"
   ClientHeight    =   14820
   ClientLeft      =   225
   ClientTop       =   525
   ClientWidth     =   18960
   StartUpPosition =   3  'Windows Default
   WindowState     =   2  'Maximized
   _ExtentX        =   33443
   _ExtentY        =   26141
   SectionData     =   "rptCotizacionPorDia.dsx":0000
End
Attribute VB_Name = "rptCotizacionPorDia"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "rptCotizacionPorDia"
Private Const CM_MESSAGE_NAME As String = "Reporte Cotización por Dia"
Private Const ERR_NOHAYIMPRESORA = 5007

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sConfigurarDatosDelReporte(ByVal valValorDelSql As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valEfecturaCambioABs As Boolean, ByVal valNombreCompaniaParaInformes As String)
   On Error GoTo h_ERROR
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "DataControl1", valValorDelSql) Then
      gUtilReports.sDefaultConfigurationForLabels Me, "lblNombreDeLaCiaActual", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaEmisionYHoraActual"
      gUtilReports.sDefaultConfigurationForLabels Me, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
      gUtilReports.sConfiguraEncabezado Me, "lblNombreDeLaCiaActual", "lblFechaEmisionYHoraActual", "lblTitulo", "LblNumeroDePagina", "lblFechaInicialYFinal", False, True
      gUtilReports.sDefaultConfigurationForDateFields Me, "txtFecha", "", "Fecha", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNumeroDeCotizaciones", "", "NumeroDeCotizaciones"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtCotizacionMayor", "", "CotizacionMayor"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtMontoCotizado", "", "MontoTotal"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtMontoBruto", "", "MontoBruto"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtMoneda", "", "Moneda"
'      txtFecha.OutputFormat = gUtilReports.getDefaultDateFormat
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter Me, "GroupHeader1", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPBeforeAfter
      gUtilReports.sDefaultConfigurationForSummaryFields Me, "txtTotalMontoBruto", "MontoBruto", eSF_SUM, "GroupHeader1", eSR_GROUP, eST_SUB_TOTAL
      gUtilReports.sDefaultConfigurationForSummaryFields Me, "txtTotalDelReporte", "MontoTotal", eSF_SUM, "GroupHeader1", eSR_GROUP, eST_SUB_TOTAL
      gUtilReports.sDefaultConfigurationForSummaryFields Me, "txtTotalNumeroDeCotizaciones", "NumeroDeCotizaciones", eSF_SUM, "GroupHeader1", eSR_GROUP, eST_SUB_TOTAL
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtTotalNumeroDeCotizaciones", "", ""
      gUtilMargins.sAsignarMargenesGenerales Me
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace _
   (Err.Description, CM_FILE_NAME, "ConfigurarDatosDelReporte()", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_Terminate()
   On Error GoTo h_ERROR
   WindowState = vbNormal
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
  "ActiveReport_Terminate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   LblNumeroDePagina.Caption = "Pag. " & pageNumber
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, _
      "ActiveReport_PageStart()", CM_FILE_NAME, CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Detail_Format()
   Dim insUtilSawDsr As clsUtilDsr
   On Error GoTo h_ERROR
   Set insUtilSawDsr = New clsUtilDsr
   txtDiaDeLaSemana.Text = gUtilDate.fDayOfWeek(DataControl1.Recordset!Fecha)
   txtDiaDeLaSemana.Text = insUtilSawDsr.enumDiaDeLaSemanaToString(txtDiaDeLaSemana.Text)
   If txtDiaDeLaSemana.Text = "Lunes" Then
      Line2.Visible = True
   Else
      Line2.Visible = False
   End If
   txtMontoCotizado.Text = gConvert.FormatoNumerico(CStr(txtMontoCotizado.Text), False)
   txtCotizacionMayor.Text = gConvert.FormatoNumerico(CStr(txtCotizacionMayor.Text), False)
'   mAcumuladorTotalCotizacion = mAcumuladorTotalCotizacion + gConvert.fConvierteACurrency(txtMontoCotizado.Text)
'   mAcumuladorNumeroDeCotizaciones = mAcumuladorNumeroDeCotizaciones + txtNumeroDeCotizaciones.Text
   Set insUtilSawDsr = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
    "Detail_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupFooter2_Format()
   On Error GoTo h_ERROR
'   txtTotalDelReporte.Text = gConvert.FormatoNumerico(CStr(mAcumuladorTotalCotizacion), False)
'   txtTotalNumeroDeCotizaciones.Text = gConvert.fConvierteAString(txtTotalNumeroDeCotizaciones.Text)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "GroupFooter1_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

