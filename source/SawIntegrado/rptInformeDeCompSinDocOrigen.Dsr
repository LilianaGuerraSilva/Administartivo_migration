VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptInformeDeCompSinDocOrigen 
   Caption         =   "Informe De Comprobante Sin Documento Origen"
   ClientHeight    =   8595
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   11880
   StartUpPosition =   3  'Windows Default
   _ExtentX        =   20955
   _ExtentY        =   15161
   SectionData     =   "rptInformeDeCompSinDocOrigen.dsx":0000
End
Attribute VB_Name = "rptInformeDeCompSinDocOrigen"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "rptInformeDeCompSinDocOrigen"
Private Const CM_MESSAGE_NAME As String = "Comprobantes sin Numero Documento Origen"
Private Const ERR_NOHAYIMPRESORA = 5007
Private Const LEF_INICIAL_DE_TIPO = 7795
Private OpcionTipoDeDocumento As String
'Private insComprobanteNavigator As clsComprobanteNavigator
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sConfigurarDatosDelReporte(ByVal valSqlDelReporte As String, _
                        ByVal valOpcionTipoDeDocumento As String, _
                        ByVal valFechaInicial As Date, _
                        ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String)
   On Error GoTo h_ERROR
'   Set insComprobanteNavigator = New clsComprobanteNavigator
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "dcOrigenData", valSqlDelReporte) Then
      gUtilReports.sDefaultConfigurationForLabels Me, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForLabels Me, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
      gUtilReports.sConfiguraEncabezado Me, "lblCompania", "lblFechaYHoraDeEmision", "lblNombreDelReporte", "LblNumeroDePagina", "lblFechaInicialYFinal", False, True
      OpcionTipoDeDocumento = valOpcionTipoDeDocumento
'      Me.PageSettings.LeftMargin = 600
'      Me.PageSettings.TopMargin = 400
'      Me.PrintWidth = 10800
'      Me.PageSettings.RightMargin = 400
      gUtilReports.sDefaultConfigurationForDateFields Me, "txtFechaDelComprobante", "", "FechaCombrobante", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNoComprobante", "", "NumeroComprobante"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtDescripcion", "", "DescripcionComprobante"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtTotalHaber", "", "TotalHaber"
      gUtilMargins.sAsignarMargenesGenerales Me
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sConfigurarDatosDelReporte", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
'   gMessage.Advertencia "No se Encontró Información para Imprimir"
   Cancel
'   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "ActiveReport_NoData", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_PageStart()
    On Error GoTo h_ERROR
    lblNumeroDePagina.Caption = "Pág " & pageNumber
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActiveReport_PageStart()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_Terminate()
   On Error GoTo h_ERROR
'   Set insComprobanteNavigator = Nothing
   WindowState = vbNormal
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActiveReport_Terminate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Detail_Format()
   On Error GoTo h_ERROR
   txtTotalHaber.Text = gConvert.fNumeroStringToStringConSeparadorDeMiles(txtTotalHaber.Text)
'   txtNoComprobante.Text = insComprobanteNavigator.fNumeroComprobanteFormateado(txtNoComprobante.Text)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Detail_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

