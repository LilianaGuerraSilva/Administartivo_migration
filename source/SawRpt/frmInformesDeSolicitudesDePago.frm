VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomct2.ocx"
Begin VB.Form frmInformesDeSolicitudesDePago 
   Caption         =   "Form1"
   ClientHeight    =   6240
   ClientLeft      =   120
   ClientTop       =   420
   ClientWidth     =   12615
   LinkTopic       =   "Form1"
   ScaleHeight     =   6240
   ScaleWidth      =   12615
   Begin VB.Frame FrmSolicitudDatos 
      Caption         =   "Datos Solicitud"
      Height          =   975
      Left            =   3480
      TabIndex        =   17
      Top             =   600
      Width           =   3975
      Begin VB.TextBox txtNumeroSolicitud 
         Height          =   375
         Left            =   1680
         TabIndex        =   18
         Top             =   360
         Width           =   1695
      End
      Begin VB.Label Label1 
         BackColor       =   &H80000016&
         BackStyle       =   0  'Transparent
         Caption         =   "Nro Solicitud"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00A84439&
         Height          =   255
         Left            =   240
         TabIndex        =   19
         Top             =   480
         Width           =   1695
      End
   End
   Begin VB.ComboBox cmbEstatusSolicitud 
      Height          =   315
      HelpContextID   =   1
      Left            =   7320
      TabIndex        =   14
      Top             =   840
      Width           =   2445
   End
   Begin VB.ComboBox cmbFormaDePago 
      Height          =   315
      HelpContextID   =   1
      Left            =   7320
      TabIndex        =   11
      Top             =   1320
      Width           =   2445
   End
   Begin VB.CommandButton CmdSalir 
      Cancel          =   -1  'True
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   3390
      TabIndex        =   10
      Top             =   5640
      Width           =   1335
   End
   Begin VB.CommandButton cmdPantalla 
      Caption         =   "&Pantalla"
      Height          =   375
      Left            =   1755
      TabIndex        =   9
      Top             =   5640
      Width           =   1335
   End
   Begin VB.CommandButton CmdImprimir 
      Caption         =   "&Impresora"
      Height          =   375
      Left            =   120
      TabIndex        =   8
      Top             =   5640
      Width           =   1335
   End
   Begin VB.Frame frameFechas 
      Caption         =   "Fechas"
      ForeColor       =   &H00808080&
      Height          =   1095
      Left            =   3480
      TabIndex        =   3
      Top             =   600
      Width           =   2115
      Begin MSComCtl2.DTPicker dtpFechaFinal 
         Height          =   285
         Left            =   720
         TabIndex        =   4
         Top             =   645
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   503
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   105775107
         CurrentDate     =   37187
      End
      Begin MSComCtl2.DTPicker dtpFechaInicial 
         Height          =   285
         Left            =   705
         TabIndex        =   5
         Top             =   240
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   503
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   105775107
         CurrentDate     =   37187
      End
      Begin VB.Label lblFechaFinal 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Final"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   7
         Top             =   690
         Width           =   330
      End
      Begin VB.Label lblFechaInicial 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Inicial"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   6
         Top             =   300
         Width           =   405
      End
   End
   Begin VB.Frame frameInformes 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   5055
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   3255
      Begin VB.OptionButton optInformeDeSolicitudDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Solicitu&des de Pago  "
         ForeColor       =   &H00A84439&
         Height          =   495
         Index           =   2
         Left            =   120
         TabIndex        =   16
         Top             =   1320
         Width           =   2955
      End
      Begin VB.OptionButton optInformeDeSolicitudDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "S&olicitudes de Pago  Forma de pago"
         ForeColor       =   &H00A84439&
         Height          =   495
         Index           =   1
         Left            =   120
         TabIndex        =   15
         Top             =   840
         Width           =   2955
      End
      Begin VB.OptionButton optInformeDeSolicitudDePago 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Solicitudes de Pago  entre fechas "
         ForeColor       =   &H00A84439&
         Height          =   495
         Index           =   0
         Left            =   120
         TabIndex        =   1
         Top             =   240
         Width           =   2955
      End
   End
   Begin VB.Label lblStatusSolicitud 
      BackColor       =   &H80000016&
      BackStyle       =   0  'Transparent
      Caption         =   "Estatus Solicitud"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00A84439&
      Height          =   255
      Left            =   5640
      TabIndex        =   13
      Top             =   840
      Width           =   1575
   End
   Begin VB.Label lblFormaDePago 
      BackColor       =   &H80000016&
      BackStyle       =   0  'Transparent
      Caption         =   "Forma De Pago"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00A84439&
      Height          =   255
      Left            =   5640
      TabIndex        =   12
      Top             =   1320
      Width           =   1335
   End
   Begin VB.Label lblDatosDelReporte 
      BackStyle       =   0  'Transparent
      Caption         =   "lblDatosDelReporte"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000040C0&
      Height          =   540
      Left            =   3600
      TabIndex        =   2
      Top             =   120
      Width           =   8145
   End
End
Attribute VB_Name = "frmInformesDeSolicitudesDePago"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "frmInformesDeSolicitudesDePago"
Private Const CM_MESSAGE_NAME As String = "Informes De Solicitudes De Pago"
Private gFechasDeLosInformes As clsFechasDeLosInformesNav
Private gProyCompaniaActual As Object
Private mConexion As Object
Private Const CM_OPT_SolicitudesDePago_ENTRE_FECHAS As Integer = 0
Private Const CM_OPT_SolicitudesDePago_PorFormaDePago As Integer = 1
Private Const CM_OPT_SolicitudDePago As Integer = 2
Private mDondeImprimir As enum_DondeImprimir
Private mInformeSeleccionado As Integer
Private mConsecutivoSolicitud As Long

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Sub sCheckForSpecialKeys(ByVal valKeyCode As Integer, ByVal valShift As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, valKeyCode) Then
   End If
   Select Case valKeyCode
      Case vbKeyEscape
         Unload Me
         Case vbKeyF6
         gAPI.ssSetFocus cmdPantalla
         cmdPantalla_Click
      Case vbKeyF8
         gAPI.ssSetFocus CmdImprimir
         cmdImprimir_Click
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCheckForSpecialKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultaCampos()
   On Error GoTo h_ERROR
   frameFechas.Visible = False
   lblFormaDePago.Visible = False
   cmbFormaDePago.Visible = False
   lblStatusSolicitud.Visible = False
   cmbEstatusSolicitud.Visible = False
   FrmSolicitudDatos.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOcultaCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImprimir_Click()
  On Error GoTo h_ERROR
   mDondeImprimir = eDI_IMPRESORA
''      If Not fSePuedeGenerarInforme Then
''         gAPI.ssSetFocus dtpFechaFinal
''         GoTo h_EXIT
''      End If
   sEjecutaElReporteApropiado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CmdImprimir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Sub

Private Sub cmdPantalla_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_PANTALLA
   sEjecutaElReporteApropiado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdPantalla_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Sub

Private Sub cmdSalir_Click()
  On Error GoTo h_ERROR
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Sub

Private Sub optInformeDeSolicitudDePago_Click(Index As Integer)
  On Error GoTo h_ERROR
   mInformeSeleccionado = Index
   sOcultaCampos
   Select Case mInformeSeleccionado
      Case CM_OPT_SolicitudesDePago_ENTRE_FECHAS: sActivarCampoSolicitudDePagoEntreFechas
      Case CM_OPT_SolicitudesDePago_PorFormaDePago: sActivarCampoSolicitudDePagoFormaDePago
      Case CM_OPT_SolicitudDePago:   sActivarCampoSolicitudDePago
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInformeDeSolicitudDePago_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Sub

Public Sub sInitLookAndFeel()
   On Error GoTo h_ERROR
   Me.Caption = CM_MESSAGE_NAME
   Set gFechasDeLosInformes = New clsFechasDeLosInformesNav
   sLLenaCombo
   gFechasDeLosInformes.sLeeLasFechasDeInformes dtpFechaInicial, dtpFechaFinal, gProyUsuarioActual.GetNombreDelUsuario
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sLLenaCombo()
   On Error GoTo h_ERROR
  gEnumProyecto.FillComboBoxWithStatusSolicitud cmbEstatusSolicitud
  cmbEstatusSolicitud.List(cmbEstatusSolicitud.ListCount) = "Todos"
  cmbEstatusSolicitud.ListIndex = cmbEstatusSolicitud.ListCount - 1
  cmbEstatusSolicitud.Width = 1575
  gEnumProyecto.FillComboBoxWithTipoDeFormaDePagoSolicitud cmbFormaDePago
  cmbFormaDePago.List(cmbFormaDePago.ListCount) = "Todos"
  cmbFormaDePago.ListIndex = cmbFormaDePago.ListCount - 1
  cmbFormaDePago.Width = 1575
h_EXIT:   On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sLLenaCombo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sActivarCampoSolicitudDePagoEntreFechas()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Reporte - Solicitudes de pago entre fechas"
   frameFechas.Visible = True
   lblStatusSolicitud.Visible = True
   cmbEstatusSolicitud.Visible = True
   lblFormaDePago.Visible = True
   cmbFormaDePago.Visible = True
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCampoSolicitudDePagoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sActivarCampoSolicitudDePagoFormaDePago()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Reporte - Solicitudes de pago por forma de pago"
   frameFechas.Visible = True
   lblStatusSolicitud.Visible = False
   cmbEstatusSolicitud.Visible = False
   lblFormaDePago.Visible = False
   cmbFormaDePago.Visible = False
 
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCampoSolicitudDePagoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sActivarCampoSolicitudDePago()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Reporte - Solicitud de pago"
   FrmSolicitudDatos.Visible = True
   
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCampoSolicitudDePagoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
 
Private Sub sEjecutaElReporteApropiado()
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case CM_OPT_SolicitudesDePago_ENTRE_FECHAS: sEjecutaElReporteSolicitudDePagoEntreFechas
      Case CM_OPT_SolicitudesDePago_PorFormaDePago: sEjecutaElReporteSolicitudDePagoFormaDePago
      Case CM_OPT_SolicitudDePago: sEjecutaElReporteSolicitudDePago
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteApropiado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


Private Sub sEjecutaElReporteSolicitudDePagoEntreFechas()
   Dim vSqlDelReporte As String
   Dim vSqlFormaDePago As String
   Dim vSqlFiltro As String
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsSolicitudesDePagoRpt
   Dim insSolicitudesDePagoVista As clsSolicitudesDePagoVista
   Dim insSolicitudDePagoSQL As clsSolicitudesDePagoSQL
   Dim valUsaCuentaBancaria As Boolean
   Dim valEfecturaCambioABs As Boolean
   Dim valAgruparBeneficiario As Boolean
   On Error GoTo h_ERROR
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
       dtpFechaInicial.Value = dtpFechaFinal.Value
   End If
   Set reporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsSolicitudesDePagoRpt
   Set insSolicitudDePagoSQL = New clsSolicitudesDePagoSQL
   Set insSolicitudesDePagoVista = New clsSolicitudesDePagoVista
   If cmbFormaDePago.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
      vSqlFormaDePago = ""
   Else
      vSqlFormaDePago = gUtilSQL.fSQLEnumValueWithAnd("", insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".FormaDePago", gEnumProyecto.strTipoDeFormaDePagoSolicitudToNum(cmbFormaDePago.Text))
   End If
   
   If cmbEstatusSolicitud.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
      vSqlFiltro = vSqlFormaDePago
   Else
    vSqlFiltro = gUtilSQL.fSQLEnumValueWithAnd(vSqlFormaDePago, insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".StatusSolicitud", gEnumProyecto.strStatusSolicitudToNum(cmbEstatusSolicitud.Text))
   End If
   
      vSqlDelReporte = insSolicitudDePagoSQL.fSqlSolicitudesDePagoEntreFechaReporte(gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, vSqlFiltro)
      If insConfigurar.fConfigurarDatosDelReporteSolicitudDePagoEntreFecha(reporte, vSqlDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), dtpFechaInicial.Value, dtpFechaFinal.Value) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Solicitud De Pago Entre Fechas"
      End If
  
   Set insSolicitudesDePagoVista = Nothing
   Set insConfigurar = Nothing
   Set reporte = Nothing
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteSolicitudDePagoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElReporteSolicitudDePagoFormaDePago()
   Dim vSqlDelReporte As String
   Dim vSqlFormaDePago As String
   Dim vSqlFiltro As String
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsSolicitudesDePagoRpt
   Dim insSolicitudesDePagoVista As clsSolicitudesDePagoVista
   Dim insSolicitudDePagoSQL As clsSolicitudesDePagoSQL
   Dim valUsaCuentaBancaria As Boolean
   Dim valEfecturaCambioABs As Boolean
   Dim valAgruparBeneficiario As Boolean
   On Error GoTo h_ERROR
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
       dtpFechaInicial.Value = dtpFechaFinal.Value
   End If
   Set reporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsSolicitudesDePagoRpt
   Set insSolicitudDePagoSQL = New clsSolicitudesDePagoSQL
   Set insSolicitudesDePagoVista = New clsSolicitudesDePagoVista
    
   If cmbEstatusSolicitud.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
      vSqlFiltro = ""
   Else
    vSqlFiltro = gUtilSQL.fSQLEnumValueWithAnd(vSqlFormaDePago, "StatusSolicitud", gEnumProyecto.strStatusSolicitudToNum(cmbEstatusSolicitud.Text))
   End If
   
      vSqlDelReporte = insSolicitudDePagoSQL.fSqlSolicitudesDePagoEntreFechaReportePivote(gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, vSqlFiltro)
      If insConfigurar.fConfigurarDatosDelReporteSolicitudDePagoPorFormaDePago(reporte, vSqlDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), dtpFechaInicial.Value, dtpFechaFinal.Value) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Solicitud De Pago  Por Forma De Pago"
      End If
  
   Set insSolicitudesDePagoVista = Nothing
   Set insConfigurar = Nothing
   Set reporte = Nothing
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteSolicitudDePagoFormaDePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub




Private Sub sEjecutaElReporteSolicitudDePago()
   Dim vSqlDelReporte As String
   Dim vSqlFormaDePago As String
   Dim vSqlFiltro As String
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsSolicitudesDePagoRpt
   Dim insSolicitudesDePagoVista As clsSolicitudesDePagoVista
   Dim insSolicitudDePagoSQL As clsSolicitudesDePagoSQL
   Dim valUsaCuentaBancaria As Boolean
   Dim valEfecturaCambioABs As Boolean
   Dim valAgruparBeneficiario As Boolean
   Dim a As Long
   On Error GoTo h_ERROR
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
       dtpFechaInicial.Value = dtpFechaFinal.Value
   End If
   Set reporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsSolicitudesDePagoRpt
   Set insSolicitudDePagoSQL = New clsSolicitudesDePagoSQL
   Set insSolicitudesDePagoVista = New clsSolicitudesDePagoVista
   If (mConsecutivoSolicitud = 0) Then
       If (Not fSelectAndSetValuesOfSolicitudAOS) Then
         sShowMessageForRequiredFields
         GoTo h_EXIT
       End If
   End If
   
   If cmbFormaDePago.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
      vSqlFormaDePago = ""
   Else
      vSqlFormaDePago = gUtilSQL.fSQLEnumValueWithAnd("", insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".FormaDePago", gEnumProyecto.strTipoDeFormaDePagoSolicitudToNum(cmbFormaDePago.Text))
   End If
      vSqlFiltro = gUtilSQL.fSQLNumberValueWithAnd(vSqlFormaDePago, insSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1() & ".ConsecutivoSolicitud", mConsecutivoSolicitud)
   
      vSqlDelReporte = insSolicitudDePagoSQL.fSqlSolicitudDePago(gProyCompaniaActual.GetConsecutivoCompania, vSqlFiltro)
      If insConfigurar.fConfigurarDatosDelReporteSolicitudDePago(reporte, vSqlDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Solicitudes De Pago"
      End If
  
   Set insSolicitudesDePagoVista = Nothing
   Set insConfigurar = Nothing
   Set reporte = Nothing
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteSolicitudDePagoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


 
Private Sub txtNumeroSolicitud_GotFocus()
On Error GoTo h_ERROR
   gAPI.SelectAllText txtNumeroSolicitud
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "NumeroSolicitud_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNumeroSolicitud_KeyDown(KeyCode As Integer, Shift As Integer)
 On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNumeroSolicitud_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
 
Private Function fSelectAndSetValuesOfSolicitudAOS() As Boolean
   Dim xmlResultado As String
   Dim vValor As String
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
    mConsecutivoSolicitud = 0
   If gTexto.DfLen(txtNumeroSolicitud.Text) = 0 Or txtNumeroSolicitud.Text = "*" Then
     vValor = ""
   Else
     vValor = "NumeroDocumentoOrigen = " & txtNumeroSolicitud.Text
   End If
   If mConexion.fSelectAndSetValuesOfSolicitudesDePagoFromAOS(vValor, xmlResultado) Then
       gLibGalacDataParse.Initialize xmlResultado
       txtNumeroSolicitud.Text = gLibGalacDataParse.GetString(0, "NumeroDocumentoOrigen", 0)
       mConsecutivoSolicitud = gLibGalacDataParse.GetLong(0, "ConsecutivoSolicitud", 0)
   Else
      vResult = False
   End If
  fSelectAndSetValuesOfSolicitudAOS = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSelectAndSetValuesOfSolicitudAOS = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSelectAndSetValuesOfCategoriaAOS", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Sub Form_Load()
   On Error GoTo h_ERROR
   Me.AutoRedraw = True
   Me.ZOrder 0
   If gDefgen.getMainForm.Width > Width Then
      Left = (gDefgen.getMainForm.Width - Width) / 4
      Top = (gDefgen.getMainForm.Height - Height) / 4
   Else
      Left = 0
      Top = 0
   End If
   Me.Width = 10725
   Me.Height = 6975
   mInformeSeleccionado = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNumeroSolicitud_Validate(Cancel As Boolean)
On Error GoTo h_ERROR
fSelectAndSetValuesOfSolicitudAOS
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNumeroSolicitud_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sShowMessageForRequiredFields()
   gMessage.ShowRequiredFields "Nro Solicitud"
End Sub

Public Sub sLoadObjectValues(ByVal valCompaniaActual As Object, ByVal valConexionAOS As Object)
On Error GoTo h_ERROR
   Set gProyCompaniaActual = valCompaniaActual
   Set mConexion = valConexionAOS
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLoadObjectValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

