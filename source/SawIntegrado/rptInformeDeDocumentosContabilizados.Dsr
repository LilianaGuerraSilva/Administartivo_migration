VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptInformeDeDocumentosContabilizados 
   Caption         =   "ActiveReport1"
   ClientHeight    =   11010
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   15240
   StartUpPosition =   3  'Windows Default
   WindowState     =   2  'Maximized
   _ExtentX        =   26882
   _ExtentY        =   19420
   SectionData     =   "rptInformeDeDocumentosContabilizados.dsx":0000
End
Attribute VB_Name = "rptInformeDeDocumentosContabilizados"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const ERR_NOHAYIMPRESORA = 5007
Private Const LEF_INICIAL_DE_TIPO = 7795
'Private Const mInformeDocsSinComp As Long = 0
'Private Const mInformeDocsContabilizados As Long = 1
'Private Const mInformeCompSinDocOrigen As Long = 2
'Private Const mLargoInicialDeTercerCampo As Long = 1400
Private Const mLargoInicialDeCuartoCampo As Long = 2700
Private Const mLargoInicialDeQuintoCampo As Long = 2700
'Private Const mPosicionInicialDeTercerCampo As Long = 3118
Private Const mPosicionInicialDeCuartoCampo As Long = 5805
Private Const mPosicionInicialDeQuintoCampo As Long = 8505
Private OpcionTipoDeDocumento As String
Private gEnumProyectoWincont As Object
Private gEnumProyecto As Object
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "rptInformeDeDocumentosContabilizados"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "RptInforme de Documento Contabilizados"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sConfigurarDatosDelReporte(ByVal valSqlDelReporte As String, ByVal valOpcionTipoDeDocumento As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valopcionTipoDocumentoEnum As String, ByRef valEnumContabilidad As Object, ByRef valEnumProyecto As Object)
   Dim opcionTipoDocumentoEnum As enum_ComprobanteGeneradoPor
   On Error GoTo h_ERROR
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "dcOrigenData", valSqlDelReporte) Then
      OpcionTipoDeDocumento = valOpcionTipoDeDocumento
      Set gEnumProyectoWincont = valEnumContabilidad
      Set gEnumProyecto = valEnumProyecto
      gUtilReports.sDefaultConfigurationForLabels Me, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForLabels Me, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
      gUtilReports.sConfiguraEncabezado Me, "lblCompania", "lblFechaYHoraDeEmision", "lblNombreDelReporte", "LblNumeroDePagina", "lblFechaInicialYFinal", True, True
      gUtilReports.sDefaultConfigurationForDateFields Me, "txtFechaDelDocumento", "", "FechaDocumento", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNoDocumento", "", "NumeroDocumentoOrigen"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtMontoDeDocumento", "", "TotalDocumento"
      gUtilReports.sDefaultConfigurationForDateFields Me, "txtFechaComprobante", "", "FechaCombrobante", eFT_DATE
      sIniciarPropiedadesDeEtiquetas
      Select Case valopcionTipoDocumentoEnum
         Case enum_ComprobanteGeneradoPor.eCG_CXP: sActivarCamposDeCxp
         Case enum_ComprobanteGeneradoPor.eCG_PAGOS: sActivarCamposDePagos
         Case enum_ComprobanteGeneradoPor.eCG_CXC: sActivarCamposDeCxC
         Case enum_ComprobanteGeneradoPor.eCG_MOVIMIENTO_BANCARIO: sActivarCamposDeMovimientoBancario
         Case enum_ComprobanteGeneradoPor.eCG_FACTURA: sActivarCamposDeFactura
         Case enum_ComprobanteGeneradoPor.eCG_COBRANZA: sActivarCamposDeCobranza
         Case enum_ComprobanteGeneradoPor.eCG_ANTICIPO: sActivarCamposDeAnticipo
      End Select
'      Me.PageSettings.LeftMargin = 600
'      Me.PageSettings.TopMargin = 400
'      Me.PrintWidth = 10800
'      Me.PageSettings.RightMargin = 400
      gUtilMargins.sAsignarMargenesGenerales Me
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sConfigurarDatosDelReporte", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCxC()
   On Error GoTo h_ERROR
   lblCuartoCampoAMostrarEnReporte.Visible = True
   txtCuartoCampoAMostrarEnReporte.Visible = True
   lblQuintoCampoAMostrarEnReporte.Visible = True
   txtQuintoCampoAMostrarEnReporte.Visible = True
   lblCuartoCampoAMostrarEnReporte.Caption = "Tipo De CxC"
   lblQuintoCampoAMostrarEnReporte.Caption = "Descripción"
   gUtilReports.sAssignDataFieldAndOuputFormat txtCuartoCampoAMostrarEnReporte, eFT_STRING, "Tipo"
   gUtilReports.sAssignDataFieldAndOuputFormat txtQuintoCampoAMostrarEnReporte, eFT_STRING, "Descripcion"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivarCamposDeCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCxp()
   On Error GoTo h_ERROR
   lblCuartoCampoAMostrarEnReporte.Visible = True
   txtCuartoCampoAMostrarEnReporte.Visible = True
   lblCuartoCampoAMostrarEnReporte.Caption = "Proveedor"
   lblQuintoCampoAMostrarEnReporte.Caption = "Nombre Proveedor"
   lblCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo + mLargoInicialDeQuintoCampo
   txtCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo + mLargoInicialDeQuintoCampo
   gUtilReports.sAssignDataFieldAndOuputFormat txtCuartoCampoAMostrarEnReporte, eFT_STRING, "CodigoProveedor"
   gUtilReports.sAssignDataFieldAndOuputFormat txtQuintoCampoAMostrarEnReporte, eFT_STRING, "NombreProveedor"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivarCamposDeCxp", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDePagos()
   On Error GoTo h_ERROR
   lblCuartoCampoAMostrarEnReporte.Visible = True
   txtCuartoCampoAMostrarEnReporte.Visible = True
   lblCuartoCampoAMostrarEnReporte.Caption = "Proveedor"
   lblQuintoCampoAMostrarEnReporte.Caption = "Nombre Proveedor"
   lblCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo + mLargoInicialDeQuintoCampo
   txtCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo + mLargoInicialDeQuintoCampo
   gUtilReports.sAssignDataFieldAndOuputFormat txtQuintoCampoAMostrarEnReporte, eFT_STRING, "NombreProveedor"
   gUtilReports.sAssignDataFieldAndOuputFormat txtCuartoCampoAMostrarEnReporte, eFT_STRING, "CodigoProveedor"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivarCamposDePagos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeMovimientoBancario()
   On Error GoTo h_ERROR
   lblCuartoCampoAMostrarEnReporte.Visible = True
   txtCuartoCampoAMostrarEnReporte.Visible = True
   lblCuartoCampoAMostrarEnReporte.Caption = "Descripción"
   lblCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo + mLargoInicialDeQuintoCampo
   txtCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo + mLargoInicialDeQuintoCampo
   gUtilReports.sAssignDataFieldAndOuputFormat txtCuartoCampoAMostrarEnReporte, eFT_STRING, "Descripcion"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivarCamposDeMovimientoBancario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCobranza()
   On Error GoTo h_ERROR
   lblCuartoCampoAMostrarEnReporte.Visible = True
   txtCuartoCampoAMostrarEnReporte.Visible = True
   lblQuintoCampoAMostrarEnReporte.Visible = True
   txtQuintoCampoAMostrarEnReporte.Visible = True
   lblCuartoCampoAMostrarEnReporte.Caption = "Concepto"
   lblQuintoCampoAMostrarEnReporte.Caption = "Cliente"
   gUtilReports.sAssignDataFieldAndOuputFormat txtCuartoCampoAMostrarEnReporte, eFT_STRING, "Concepto"
   gUtilReports.sAssignDataFieldAndOuputFormat txtQuintoCampoAMostrarEnReporte, eFT_STRING, "NombreCliente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivarCamposDeCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeFactura()
   On Error GoTo h_ERROR
   lblCuartoCampoAMostrarEnReporte.Visible = True
   txtCuartoCampoAMostrarEnReporte.Visible = True
   lblQuintoCampoAMostrarEnReporte.Visible = True
   txtQuintoCampoAMostrarEnReporte.Visible = True
   lblCuartoCampoAMostrarEnReporte.Caption = "Nombre Cliente"
   lblQuintoCampoAMostrarEnReporte.Caption = "FechaVencimiento"
   gUtilReports.sAssignDataFieldAndOuputFormat txtCuartoCampoAMostrarEnReporte, eFT_STRING, "NombreCliente"
   gUtilReports.sAssignDataFieldAndOuputFormat txtQuintoCampoAMostrarEnReporte, eFT_DATE, "FechaVencimiento"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivarCamposDeFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeAnticipo()
   On Error GoTo h_ERROR
   lblCuartoCampoAMostrarEnReporte.Visible = True
   txtCuartoCampoAMostrarEnReporte.Visible = True
   lblQuintoCampoAMostrarEnReporte.Visible = True
   txtQuintoCampoAMostrarEnReporte.Visible = True
   lblCuartoCampoAMostrarEnReporte.Caption = "Tipo de Anticipo"
   lblQuintoCampoAMostrarEnReporte.Caption = "Descripción"
   gUtilReports.sAssignDataFieldAndOuputFormat txtCuartoCampoAMostrarEnReporte, eFT_STRING, "Tipo"
   gUtilReports.sAssignDataFieldAndOuputFormat txtQuintoCampoAMostrarEnReporte, eFT_STRING, "Descripcion"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivarCamposDeAnticipo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sIniciarPropiedadesDeEtiquetas()
   On Error GoTo h_ERROR
   lblCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo
   txtCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo
   lblQuintoCampoAMostrarEnReporte.Width = mLargoInicialDeQuintoCampo
   txtQuintoCampoAMostrarEnReporte.Width = mLargoInicialDeQuintoCampo
   lblCuartoCampoAMostrarEnReporte.Left = mPosicionInicialDeCuartoCampo
   txtCuartoCampoAMostrarEnReporte.Left = mPosicionInicialDeCuartoCampo
   lblQuintoCampoAMostrarEnReporte.Left = mPosicionInicialDeQuintoCampo
   txtQuintoCampoAMostrarEnReporte.Left = mPosicionInicialDeQuintoCampo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sIniciarPropiedadesDeEtiquetas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   Cancel
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

'Reemplazado: Private Sub ActiveReport_Terminate
Private Sub ActiveReport_Terminate()
   If Err.Number <> 0 Then
      WindowState = vbNormal
   Else
      On Error GoTo h_ERROR
      WindowState = vbNormal
h_EXIT: On Error GoTo 0
      Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActiveReport_Terminate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
      End If
End Sub

Private Sub Detail_Format()
   On Error GoTo h_ERROR
'   txtMontoDeDocumento = gConvert.fNumeroStringToStringConSeparadorDeMiles(txtMontoDeDocumento)
   Select Case OpcionTipoDeDocumento
      Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_CXP)
            txtNoDocumento.Text = gTexto.DfReplace(txtNoDocumento.Text, gTexto.fSeparadorStandardDeElementosString, "<#>")
            txtCuartoCampoAMostrarEnReporte.Text = txtCuartoCampoAMostrarEnReporte.Text _
            & "-" & txtQuintoCampoAMostrarEnReporte.Text
      Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_PAGOS)
            txtCuartoCampoAMostrarEnReporte.Text = txtCuartoCampoAMostrarEnReporte.Text _
            & "-" & txtQuintoCampoAMostrarEnReporte.Text
      Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_CXC)
            txtCuartoCampoAMostrarEnReporte.Text = gEnumProyecto.enumTipoDeCxCToString _
            (gConvert.charAEnumerativoInt(txtCuartoCampoAMostrarEnReporte.Text))
      Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_MOVIMIENTO_BANCARIO)
      Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_COBRANZA)
      Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_ANTICIPO)
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Detail_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

