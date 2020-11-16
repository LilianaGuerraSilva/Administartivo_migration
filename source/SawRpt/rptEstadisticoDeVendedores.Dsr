VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptEstadisticoDeVendedores 
   Caption         =   "Estadistico De Vendedores"
   ClientHeight    =   8595
   ClientLeft      =   165
   ClientTop       =   450
   ClientWidth     =   11880
   StartUpPosition =   3  'Windows Default
   WindowState     =   2  'Maximized
   _ExtentX        =   20955
   _ExtentY        =   15161
   SectionData     =   "rptEstadisticoDeVendedores.dsx":0000
End
Attribute VB_Name = "rptEstadisticoDeVendedores"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "rptEstadisticoDeVendedores"
Private Const CM_MESSAGE_NAME As String = "Reporte Estadistico De Vendedores"
Private Const ERR_NOHAYIMPRESORA = 5007
Private mAcumTotalBaseImponible As Currency
Private mAcumEnero As Currency
Private mAcumFebrero As Currency
Private mAcumMarzo As Currency
Private mAcumAbril As Currency
Private mAcumMayo As Currency
Private mAcumJunio As Currency
Private mAcumJulio As Currency
Private mAcumAgosto As Currency
Private mAcumSeptiembre As Currency
Private mAcumOctubre As Currency
Private mAcumNoviembre As Currency
Private mAcumDiciembre As Currency
'Public mHayDatos As Boolean
Public Sub sConfigurarDatosDelReporte(ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, _
                                        ByVal valFechaFinal As Date, ByVal valMonedaDeLosReportes As String, ByVal valNombreCompaniaParaInformes As String)
   On Error GoTo h_ERROR
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "dcOrigenData", valSqlDelReporte) Then
      gUtilReports.sDefaultConfigurationForLabels Me, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForLabels Me, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
      gUtilReports.sConfiguraEncabezado Me, "lblCompania", "lblFechaHoraDeEmision", "lblCXPEntreFechas", "LblNumeroDePagina", "lblFechaInicialYFinal", True, True
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtAño", "", "Anno"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNombreVendedor", "", "NombreDelVendedor"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtCodigoVendedor", "", "CodigoDelVendedor"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalBaseImponible", "", "AcumBaseImponible"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalDelReporteBaseImp", "", ""
      txtEnero.DataField = "Ene"
      txtFebrero.DataField = "Feb"
      txtMarzo.DataField = "Mar"
      txtAbril.DataField = "Abr"
      txtMayo.DataField = "May"
      txtJunio.DataField = "Jun"
      txtJulio.DataField = "Jul"
      txtAgosto.DataField = "Ago"
      txtSeptiembre.DataField = "Sep"
      txtOctubre.DataField = "Oct"
      txtNoviembre.DataField = "Nov"
      txtDiciembre.DataField = "Dic"
'      mHayDatos = True
      mAcumTotalBaseImponible = 0
      mAcumEnero = 0
      mAcumFebrero = 0
      mAcumMarzo = 0
      mAcumAbril = 0
      mAcumMayo = 0
      mAcumJunio = 0
      mAcumJulio = 0
      mAcumAgosto = 0
      mAcumSeptiembre = 0
      mAcumOctubre = 0
      mAcumNoviembre = 0
      mAcumDiciembre = 0
'      lblNumeroDePagina.Visible = gProyParametros.GetImprimirNoPagina
'      lblFechaYHoraDeEmision.Visible = gProyParametros.GetImprimirFechaDeEmision
      lblMensajeDeLaTasaDeCambio.Caption = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
      If (valMonedaDeLosReportes = gEnumReport.enumMonedaDeLosReportesToString(enum_MonedaDeLosReportes.eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal)) Then
         gUtilReports.sDefaultConfigurationForStrFields Me, "txtMonedaPorGrupo", "", "Moneda"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter Me, "GHMoneda", "Moneda", ddGrpFirstDetail, ddRepeatOnPageIncludeNoDetail, True, ddNPNone
         GHMoneda.GrpKeepTogether = ddGrpAll
      Else
         lblMonedaPorGrupo.Visible = False
         txtMonedaPorGrupo.Visible = False
         lblMensajeDeLaTasaDeCambio.Visible = False
      End If
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter Me, "GHAnno", "Anno", ddGrpFirstDetail, ddRepeatOnPageIncludeNoDetail, True, ddNPAfter
      GHAnno.GrpKeepTogether = ddGrpAll
      gUtilMargins.sAsignarMargenesGenerales Me
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sConfigurarDatosDelReporte", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
'   mHayDatos = False
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

Private Function GetGender() As Enum_Gender
   On Error GoTo h_ERROR
   GetGender = eg_Male
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GetGender", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

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
   sAcumulaYFormateaLosCampos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Detail_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GFMoneda_AfterPrint()
   On Error GoTo h_ERROR
   mAcumTotalBaseImponible = 0
   mAcumEnero = 0
   mAcumFebrero = 0
   mAcumMarzo = 0
   mAcumAbril = 0
   mAcumMayo = 0
   mAcumJunio = 0
   mAcumJulio = 0
   mAcumAgosto = 0
   mAcumSeptiembre = 0
   mAcumOctubre = 0
   mAcumNoviembre = 0
   mAcumDiciembre = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "GFMoneda_AfterPrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GFMoneda_Format()
   On Error GoTo h_ERROR
   txtTotalDelReporteBaseImp.Text = gConvert.FormatoNumerico(CStr(mAcumTotalBaseImponible), False)
   txtTotalEnero.Text = gConvert.FormatoNumerico(CStr(mAcumEnero), False)
   txtTotalFebrero.Text = gConvert.FormatoNumerico(CStr(mAcumFebrero), False)
   txtTotalMarzo.Text = gConvert.FormatoNumerico(CStr(mAcumMarzo), False)
   txtTotalAbril.Text = gConvert.FormatoNumerico(CStr(mAcumAbril), False)
   txtTotalMayo.Text = gConvert.FormatoNumerico(CStr(mAcumMayo), False)
   txtTotalJunio.Text = gConvert.FormatoNumerico(CStr(mAcumJunio), False)
   txtTotalJulio.Text = gConvert.FormatoNumerico(CStr(mAcumJulio), False)
   txtTotalAgosto.Text = gConvert.FormatoNumerico(CStr(mAcumAgosto), False)
   txtTotalSeptiembre.Text = gConvert.FormatoNumerico(CStr(mAcumSeptiembre), False)
   txtTotalOctubre.Text = gConvert.FormatoNumerico(CStr(mAcumOctubre), False)
   txtTotalNoviembre.Text = gConvert.FormatoNumerico(CStr(mAcumNoviembre), False)
   txtTotalDiciembre.Text = gConvert.FormatoNumerico(CStr(mAcumDiciembre), False)
   lblTotal.Caption = "Totales del Año : " & txtAño.Text
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "GFMoneda_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sAcumulaYFormateaLosCampos()
   On Error GoTo h_ERROR
   mAcumTotalBaseImponible = mAcumTotalBaseImponible + gConvert.fConvierteACurrency(txtTotalBaseImponible.Text)
   mAcumEnero = mAcumEnero + gConvert.fConvierteACurrency(txtEnero.Text)
   mAcumFebrero = mAcumFebrero + gConvert.fConvierteACurrency(txtFebrero.Text)
   mAcumMarzo = mAcumMarzo + gConvert.fConvierteACurrency(txtMarzo.Text)
   mAcumAbril = mAcumAbril + gConvert.fConvierteACurrency(txtAbril.Text)
   mAcumMayo = mAcumMayo + gConvert.fConvierteACurrency(txtMayo.Text)
   mAcumJunio = mAcumJunio + gConvert.fConvierteACurrency(txtJunio.Text)
   mAcumJulio = mAcumJulio + gConvert.fConvierteACurrency(txtJulio.Text)
   mAcumAgosto = mAcumAgosto + gConvert.fConvierteACurrency(txtAgosto.Text)
   mAcumSeptiembre = mAcumSeptiembre + gConvert.fConvierteACurrency(txtSeptiembre.Text)
   mAcumOctubre = mAcumOctubre + gConvert.fConvierteACurrency(txtOctubre.Text)
   mAcumNoviembre = mAcumNoviembre + gConvert.fConvierteACurrency(txtNoviembre.Text)
   mAcumDiciembre = mAcumDiciembre + gConvert.fConvierteACurrency(txtDiciembre.Text)
   txtTotalBaseImponible.Text = gConvert.FormatoNumerico(CStr(txtTotalBaseImponible.Text), False)
   txtEnero.Text = gConvert.FormatoNumerico(CStr(txtEnero.Text), False)
   txtFebrero.Text = gConvert.FormatoNumerico(CStr(txtFebrero.Text), False)
   txtMarzo.Text = gConvert.FormatoNumerico(CStr(txtMarzo.Text), False)
   txtAbril.Text = gConvert.FormatoNumerico(CStr(txtAbril.Text), False)
   txtMayo.Text = gConvert.FormatoNumerico(CStr(txtMayo.Text), False)
   txtJunio.Text = gConvert.FormatoNumerico(CStr(txtJunio.Text), False)
   txtJulio.Text = gConvert.FormatoNumerico(CStr(txtJulio.Text), False)
   txtAgosto.Text = gConvert.FormatoNumerico(CStr(txtAgosto.Text), False)
   txtSeptiembre.Text = gConvert.FormatoNumerico(CStr(txtSeptiembre.Text), False)
   txtOctubre.Text = gConvert.FormatoNumerico(CStr(txtOctubre.Text), False)
   txtNoviembre.Text = gConvert.FormatoNumerico(CStr(txtNoviembre.Text), False)
   txtDiciembre.Text = gConvert.FormatoNumerico(CStr(txtDiciembre.Text), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "sAcumulaYFormateaLosCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

