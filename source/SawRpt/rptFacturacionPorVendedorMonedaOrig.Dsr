VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptFacturacionPorVendedorMonedaOrig 
   Caption         =   "Facturación Por Vendedor"
   ClientHeight    =   8595
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   11880
   StartUpPosition =   3  'Windows Default
   WindowState     =   2  'Maximized
   _ExtentX        =   20955
   _ExtentY        =   15161
   SectionData     =   "rptFacturacionPorVendedorMonedaOrig.dsx":0000
End
Attribute VB_Name = "rptFacturacionPorVendedorMonedaOrig"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "rptFacturacionPorVendedorMonedaOrig"
Private Const CM_MESSAGE_NAME As String = "Reporte Facturacion Por Vendedor en Moneda Original"
Private Const ERR_NOHAYIMPRESORA = 5007

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sConfigurarDatosDelReporte(ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaCodigoVendedorEnPantalla As Boolean)
   Dim varLeft As Integer
   On Error GoTo h_ERROR
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "dcOrigenData", valSqlDelReporte) Then
      txtCodigoVendedor.Visible = valUsaCodigoVendedorEnPantalla
      gUtilReports.sDefaultConfigurationForLabels Me, "lblNombreDeLaCia", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForLabels Me, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
      gUtilReports.sConfiguraEncabezado Me, "lblNombreDeLaCia", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "LblNumeroDePagina", "lblFechaInicialYFinal", False, True
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtCodigoVendedor", "", "CodigoVendedor"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNombreVendedor", "", "NombreVendedor"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtMoneda", "", "Moneda"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtCod", "", "CodigoCliente"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNombreCliente", "", "NombreCliente"
      gUtilReports.sDefaultConfigurationForDateFields Me, "txtFecha", "", "Fecha", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNoDocumento", "", "Numero"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalBs", "", "TotalFact"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalMoneda", "", ""
'      lblNumeroDePagina.Visible = gProyParametros.GetImprimirNoPagina
'      lblFechaYHoraDeEmision.Visible = gProyParametros.GetImprimirFechaDeEmision
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter Me, "GHVendedor", "NombreVendedor", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPBefore
'      GHVendedor.DataField = "NombreVendedor"
'      GHVendedor.GrpKeepTogether = ddGrpFirstDetail
'      GHVendedor.KeepTogether = True
'      GHVendedor.NewPage = ddNPBefore
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter Me, "GHVendedor", "Moneda", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPBefore
'      GHMoneda.DataField = "Moneda"
'      GHMoneda.GrpKeepTogether = ddGrpFirstDetail
      GHMoneda.KeepTogether = True
   '   txtTotalTotalBs.DataField = ("TotalFact")
      txtTotalMoneda.DataField = ("TotalFact")
      Me.PageSettings.Orientation = ddOPortrait
      Me.PrintWidth = 10000
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
   On Error GoTo h_ERROR
   txtTotalBs.Text = gConvert.FormatoNumerico(CStr(txtTotalBs), False)
   txtFecha.Text = gConvert.dateToStringYY(txtFecha)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Detail_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupFooter1_BeforePrint()
   On Error GoTo h_ERROR
'   lblTotalVendedor.Caption = "Totales  " & txtNombreVendedor.DataValue
   'txtTotalTotalBs.Text = gConvert.FormatoNumerico(CStr(txtTotalTotalBs), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "GroupFooter1_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GFMoneda_BeforePrint()
   On Error GoTo h_ERROR
   lblTotalMoneda.Caption = "Totales  " & txtMoneda.DataValue
   txtTotalMoneda.Text = gConvert.FormatoNumerico(CStr(txtTotalMoneda), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "GroupFooter1_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

