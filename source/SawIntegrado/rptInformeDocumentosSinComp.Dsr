VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptInformeDocumentosSinComp 
   Caption         =   "Informe De Documentos Sin Comprobantes"
   ClientHeight    =   8595
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   11880
   StartUpPosition =   3  'Windows Default
   _ExtentX        =   20955
   _ExtentY        =   15161
   SectionData     =   "rptInformeDocumentosSinComp.dsx":0000
End
Attribute VB_Name = "rptInformeDocumentosSinComp"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "Informe De Comprobantes Sin Documentos"
Private Const CM_MESSAGE_NAME As String = "RptInforme de Documento Contabilizados"
Private Const ERR_NOHAYIMPRESORA = 5007
'Private Const LEF_INICIAL_DE_TIPO = 7795
'Private Const mInformeDocsSinComp As Long = 0
'Private Const mInformeDocsContabilizados As Long = 1
'Private Const mInformeCompSinDocOrigen As Long = 2
Private Const mLargoInicialDeTercerCampo As Long = 1400
Private Const mLargoInicialDeCuartoCampo As Long = 2267
Private Const mLargoInicialDeQuintoCampo As Long = 4036
Private Const mPosicionInicialDeTercerCampo As Long = 3312
Private Const mPosicionInicialDeCuartoCampo As Long = 4618
Private Const mPosicionInicialDeQuintoCampo As Long = 6990
Private OpcionTipoDeDocumento As String
Private gEnumProyectoWincont As Object
Private gEnumProyecto As Object

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sConfigurarDatosDelReporte(ByVal valSqlDelReporte As String, ByVal valOpcionTipoDeDocumento As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByRef valEnumProyectoWincont As Object, ByRef valEnumProyecto As Object)
   On Error GoTo h_ERROR
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "dcOrigenData", valSqlDelReporte) Then
      Set gEnumProyectoWincont = valEnumProyectoWincont
      Set gEnumProyecto = valEnumProyecto
      OpcionTipoDeDocumento = valOpcionTipoDeDocumento
      gUtilReports.sDefaultConfigurationForLabels Me, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForLabels Me, "lblNombreDelReporte", "Informe de " & valOpcionTipoDeDocumento & " sin Comprobante"
      gUtilReports.sDefaultConfigurationForLabels Me, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
      gUtilReports.sConfiguraEncabezado Me, "lblCompania", "lblFechaYHoraDeEmision", "lblNombreDelReporte", "LblNumeroDePagina", "lblFechaInicialYFinal", True, True
      gUtilReports.sDefaultConfigurationForDateFields Me, "txtFechaDelDocumento", "", "FechaDocumento", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNoDocumento", "", "NumeroDocumento"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtMontoDeDocumento", "", "TotalDocumento"
      sIniciarPropiedadesDeEtiquetas
      Select Case valOpcionTipoDeDocumento
         Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_CXP)
            sActivarCamposDeCxp
         Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_PAGOS)
            sActivarCamposDePagos
         Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_CXC)
            sActivarCamposDeCxC
         Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_MOVIMIENTO_BANCARIO)
             sActivarCamposDeMovimientoBancario
         Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_FACTURA)
             sActivarCamposDeFactura
         Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_COBRANZA)
            sActivarCamposDeCobranza
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
   lblTercerCampoAMostrarEnReporte.Visible = True
   txtTercerCampoAMostrarEnReporte.Visible = True
   lblCuartoCampoAMostrarEnReporte.Visible = True
   txtCuartoCampoAMostrarEnReporte.Visible = True
   lblQuintoCampoAMostrarEnReporte.Visible = True
   txtQuintoCampoAMostrarEnReporte.Visible = True
   lblTercerCampoAMostrarEnReporte.Caption = "Tipo"
   lblCuartoCampoAMostrarEnReporte.Caption = "Descripción"
   lblQuintoCampoAMostrarEnReporte.Caption = "Status"
   lblCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo + 600
   txtCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo + 600
   lblQuintoCampoAMostrarEnReporte.Left = mPosicionInicialDeQuintoCampo + 600
   txtQuintoCampoAMostrarEnReporte.Left = mPosicionInicialDeQuintoCampo + 600
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtTercerCampoAMostrarEnReporte", "", "Tipo"
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtCuartoCampoAMostrarEnReporte", "", "Descripcion"
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtQuintoCampoAMostrarEnReporte", "", "Status"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivarCamposDeCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCxp()
   On Error GoTo h_ERROR
   lblTercerCampoAMostrarEnReporte.Visible = True
   txtTercerCampoAMostrarEnReporte.Visible = True
   lblCuartoCampoAMostrarEnReporte.Visible = True
   txtCuartoCampoAMostrarEnReporte.Visible = True
   lblTercerCampoAMostrarEnReporte.Caption = "Tipo"
   lblCuartoCampoAMostrarEnReporte.Caption = "Proveedor"
   lblQuintoCampoAMostrarEnReporte.Caption = "Nombre Proveedor"
   lblCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo + mLargoInicialDeQuintoCampo
   txtCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo + mLargoInicialDeQuintoCampo
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtTercerCampoAMostrarEnReporte", "", "Tipo"
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtCuartoCampoAMostrarEnReporte", "", "CodigoProveedor"
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtQuintoCampoAMostrarEnReporte", "", "NombreProveedor"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivarCamposDeCxp", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDePagos()
   On Error GoTo h_ERROR
   lblTercerCampoAMostrarEnReporte.Visible = True
   txtTercerCampoAMostrarEnReporte.Visible = True
   lblQuintoCampoAMostrarEnReporte.Visible = True
   txtQuintoCampoAMostrarEnReporte.Visible = True
   lblTercerCampoAMostrarEnReporte.Caption = "Proveedor"
   lblQuintoCampoAMostrarEnReporte.Caption = "Descripción"
   lblTercerCampoAMostrarEnReporte.Width = mLargoInicialDeTercerCampo + mLargoInicialDeCuartoCampo
   txtTercerCampoAMostrarEnReporte.Width = mLargoInicialDeTercerCampo + mLargoInicialDeCuartoCampo
   lblTercerCampoAMostrarEnReporte.Width = mLargoInicialDeTercerCampo + mLargoInicialDeCuartoCampo
   txtTercerCampoAMostrarEnReporte.Width = mLargoInicialDeTercerCampo + mLargoInicialDeCuartoCampo
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtQuintoCampoAMostrarEnReporte", "", "Descripcion"
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtTercerCampoAMostrarEnReporte", "", "CodigoProveedor"
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtCuartoCampoAMostrarEnReporte", "", "NombreProveedor"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivarCamposDePagos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeMovimientoBancario()
   On Error GoTo h_ERROR
   lblTercerCampoAMostrarEnReporte.Visible = True
   txtTercerCampoAMostrarEnReporte.Visible = True
   lblQuintoCampoAMostrarEnReporte.Visible = True
   txtQuintoCampoAMostrarEnReporte.Visible = True
   lblTercerCampoAMostrarEnReporte.Caption = "Descripción"
   lblQuintoCampoAMostrarEnReporte.Caption = "Concepto"
   lblTercerCampoAMostrarEnReporte.Width = mLargoInicialDeTercerCampo + mLargoInicialDeCuartoCampo
   txtTercerCampoAMostrarEnReporte.Width = mLargoInicialDeTercerCampo + mLargoInicialDeCuartoCampo
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtQuintoCampoAMostrarEnReporte", "", "Concepto"
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtTercerCampoAMostrarEnReporte", "", "Descripcion"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivarCamposDeMovimientoBancario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCobranza()
   On Error GoTo h_ERROR
   lblTercerCampoAMostrarEnReporte.Visible = True
   txtTercerCampoAMostrarEnReporte.Visible = True
   lblCuartoCampoAMostrarEnReporte.Visible = True
   txtCuartoCampoAMostrarEnReporte.Visible = True
   lblQuintoCampoAMostrarEnReporte.Visible = True
   txtQuintoCampoAMostrarEnReporte.Visible = True
   lblTercerCampoAMostrarEnReporte.Caption = "Status"
   lblCuartoCampoAMostrarEnReporte.Caption = "Concepto"
   lblQuintoCampoAMostrarEnReporte.Caption = "Cliente"
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtTercerCampoAMostrarEnReporte", "", "Status"
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtCuartoCampoAMostrarEnReporte", "", "Concepto"
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtQuintoCampoAMostrarEnReporte", "", "NombreCliente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActivarCamposDeCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeFactura()
   On Error GoTo h_ERROR
   lblTercerCampoAMostrarEnReporte.Visible = True
   txtTercerCampoAMostrarEnReporte.Visible = True
   lblCuartoCampoAMostrarEnReporte.Visible = True
   txtCuartoCampoAMostrarEnReporte.Visible = True
   lblCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo + mLargoInicialDeQuintoCampo
   txtCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo + mLargoInicialDeQuintoCampo
   lblTercerCampoAMostrarEnReporte.Caption = "Fecha Vencimiento"
   lblCuartoCampoAMostrarEnReporte.Caption = "Nombre Cliente"
   lblQuintoCampoAMostrarEnReporte.Caption = "Nombre Vendedor"
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtTercerCampoAMostrarEnReporte", "", "FechaVencimiento"
   gUtilReports.sDefaultConfigurationForStrFields Me, "txtCuartoCampoAMostrarEnReporte", "", "NombreCliente"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivarCamposDeFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sIniciarPropiedadesDeEtiquetas()
   On Error GoTo h_ERROR
   lblTercerCampoAMostrarEnReporte.Width = mLargoInicialDeTercerCampo
   txtTercerCampoAMostrarEnReporte.Width = mLargoInicialDeTercerCampo
   lblCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo
   txtCuartoCampoAMostrarEnReporte.Width = mLargoInicialDeCuartoCampo
   lblQuintoCampoAMostrarEnReporte.Width = mLargoInicialDeQuintoCampo
   txtQuintoCampoAMostrarEnReporte.Width = mLargoInicialDeQuintoCampo
   lblTercerCampoAMostrarEnReporte.Left = mPosicionInicialDeTercerCampo
   txtTercerCampoAMostrarEnReporte.Left = mPosicionInicialDeTercerCampo
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
   txtMontoDeDocumento = gConvert.fNumeroStringToStringConSeparadorDeMiles(txtMontoDeDocumento)
   Select Case OpcionTipoDeDocumento
      Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_CXP)
           txtTercerCampoAMostrarEnReporte.Text = gEnumProyecto.enumTipoDeCxCToString _
           (gConvert.charAEnumerativoInt(txtTercerCampoAMostrarEnReporte.Text))
           txtCuartoCampoAMostrarEnReporte.Text = txtCuartoCampoAMostrarEnReporte.Text _
           & "-" & txtQuintoCampoAMostrarEnReporte.Text
      Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_PAGOS)
           txtTercerCampoAMostrarEnReporte.Text = txtTercerCampoAMostrarEnReporte.Text _
           & "-" & txtCuartoCampoAMostrarEnReporte.Text
      Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_CXC)
           txtTercerCampoAMostrarEnReporte.Text = gEnumProyecto.enumTipoDeCxCToString _
           (gConvert.charAEnumerativoInt(txtTercerCampoAMostrarEnReporte.Text))
           txtQuintoCampoAMostrarEnReporte.Text = gEnumProyecto.enumStatusDocumentoToString _
           (gConvert.charAEnumerativoInt(txtQuintoCampoAMostrarEnReporte.Text))
      Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_MOVIMIENTO_BANCARIO)
      Case gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_COBRANZA)
           txtTercerCampoAMostrarEnReporte.Text = gEnumProyecto.enumStatusCobranzaToString _
           (gConvert.charAEnumerativoInt(txtTercerCampoAMostrarEnReporte.Text))
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Detail_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

