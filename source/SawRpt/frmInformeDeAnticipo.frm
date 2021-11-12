VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCT2.OCX"
Object = "{9F849E84-0608-4BCD-A2B4-8EC557266A4F}#11.0#0"; "GSTextBox.ocx"
Begin VB.Form frmInformeDeAnticipo 
   BackColor       =   &H00F3F3F3&
   Caption         =   "frmInformeDeAnticipo"
   ClientHeight    =   3570
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   10995
   LinkTopic       =   "Form1"
   ScaleHeight     =   3570
   ScaleWidth      =   10995
   Begin VB.Frame frameHasta 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Hasta la fecha"
      ForeColor       =   &H00808080&
      Height          =   615
      Left            =   8520
      TabIndex        =   34
      Top             =   2340
      Width           =   1455
      Begin MSComCtl2.DTPicker dtpFechaHasta 
         Height          =   315
         Left            =   120
         TabIndex        =   35
         Top             =   240
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   556
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   96600067
         CurrentDate     =   36978
      End
   End
   Begin VB.Frame frameCotizacion 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Height          =   315
      Left            =   6720
      TabIndex        =   32
      Top             =   840
      Visible         =   0   'False
      Width           =   3015
      Begin GSTextBox.GSText txtNumeroCotizacion 
         Height          =   285
         Left            =   1080
         TabIndex        =   6
         Top             =   0
         Width           =   1230
         _ExtentX        =   2170
         _ExtentY        =   503
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   10
      End
      Begin VB.Label lblNumeroCotizacion 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Cotización"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   240
         TabIndex        =   33
         Top             =   45
         Width           =   735
      End
   End
   Begin VB.Frame frameClienteProveedor 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Cliente / Proveedor"
      ForeColor       =   &H00808080&
      Height          =   555
      Left            =   3360
      TabIndex        =   26
      Top             =   1200
      Width           =   7515
      Begin GSTextBox.GSText txtCodigo 
         Height          =   285
         Left            =   780
         TabIndex        =   7
         Top             =   180
         Width           =   1110
         _ExtentX        =   1958
         _ExtentY        =   503
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   10
         Text            =   "Código"
         GMsgName        =   "Código"
      End
      Begin GSTextBox.GSText txtNombre 
         Height          =   285
         Left            =   1950
         TabIndex        =   8
         Top             =   195
         Width           =   5430
         _ExtentX        =   9578
         _ExtentY        =   503
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         MaxLength       =   80
         Text            =   "Nombre"
         GMsgName        =   "Nombre"
      End
      Begin VB.Label lblCodigo 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Nombre"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   27
         Top             =   240
         Width           =   555
      End
   End
   Begin VB.CommandButton cmdImpresora 
      Caption         =   "&Impresora"
      Height          =   375
      Left            =   60
      TabIndex        =   14
      Top             =   3090
      Width           =   1215
   End
   Begin VB.Frame frameMoneda 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Moneda"
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   7320
      TabIndex        =   24
      Top             =   1950
      Visible         =   0   'False
      Width           =   3495
      Begin VB.ComboBox cmbMonedaDeLosReportes 
         Height          =   315
         Left            =   720
         TabIndex        =   13
         Top             =   0
         Width           =   1755
      End
      Begin VB.Label lblMonedaDeLosReportes 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Moneda"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   80
         TabIndex        =   25
         Top             =   50
         Width           =   585
      End
   End
   Begin VB.Frame frameTasaDeCambio 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Tasa de cambio"
      ForeColor       =   &H00808080&
      Height          =   1095
      Left            =   5520
      TabIndex        =   23
      Top             =   1860
      Width           =   1695
      Begin VB.OptionButton optTasaDeCambio 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Original"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   11
         Top             =   240
         Width           =   1335
      End
      Begin VB.OptionButton optTasaDeCambio 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Del día"
         ForeColor       =   &H00A84439&
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   12
         Top             =   555
         Width           =   1335
      End
   End
   Begin VB.Frame frameFechas 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Fechas"
      ForeColor       =   &H00808080&
      Height          =   1095
      Left            =   3345
      TabIndex        =   19
      Top             =   1860
      Width           =   2055
      Begin MSComCtl2.DTPicker dtpFechaFinal 
         Height          =   315
         Left            =   720
         TabIndex        =   10
         Top             =   615
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   556
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   96600067
         CurrentDate     =   36978
      End
      Begin MSComCtl2.DTPicker dtpFechaInicial 
         Height          =   315
         Left            =   720
         TabIndex        =   9
         Top             =   240
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   556
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   96600067
         CurrentDate     =   36978
      End
      Begin VB.Label lblFechaFinal 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Final"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   21
         Top             =   675
         Width           =   330
      End
      Begin VB.Label lblFechaInicial 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Inicial"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   20
         Top             =   300
         Width           =   405
      End
   End
   Begin VB.Frame frameInformes 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   2535
      Left            =   120
      TabIndex        =   17
      Top             =   120
      Width           =   3135
      Begin VB.OptionButton optInformeDeAnticipo 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Anticipos a una Fecha......................"
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   4
         Left            =   120
         TabIndex        =   3
         Top             =   1920
         Width           =   2895
      End
      Begin VB.OptionButton optInformeDeAnticipo 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Anticipo Asociados a Cotizacion . . . "
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   3
         Left            =   120
         TabIndex        =   2
         Top             =   1440
         Visible         =   0   'False
         Width           =   2895
      End
      Begin VB.OptionButton optInformeDeAnticipo 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Anticipo entre Fechas ......................"
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   2
         Left            =   120
         TabIndex        =   1
         Top             =   960
         Width           =   2895
      End
      Begin VB.OptionButton optInformeDeAnticipo 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Anticipo por Cliente / Proveedor ......"
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   0
         Top             =   600
         Width           =   2895
      End
      Begin VB.Label lblInformesDeAnticipo 
         BackColor       =   &H00A86602&
         Caption         =   "Informe de Anticipo Cob/Pag"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FFFFFF&
         Height          =   255
         Left            =   120
         TabIndex        =   18
         Top             =   240
         Width           =   2895
      End
   End
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   2940
      TabIndex        =   16
      Top             =   3090
      Width           =   1215
   End
   Begin VB.CommandButton cmdPantalla 
      Caption         =   "&Pantalla"
      Height          =   375
      Left            =   1560
      TabIndex        =   15
      Top             =   3090
      Width           =   1215
   End
   Begin VB.Frame frameCantidad 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Status de Anticipos Cobrados"
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   3360
      TabIndex        =   30
      Top             =   840
      Width           =   3135
      Begin VB.ComboBox CmbCantidadAImprimir 
         Height          =   315
         Left            =   1590
         TabIndex        =   4
         Top             =   0
         Width           =   975
      End
      Begin VB.Label lblCantdadDeClienteImprimir 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Cantidad  a Imprimir"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   60
         TabIndex        =   31
         Top             =   60
         Width           =   1380
      End
   End
   Begin VB.Frame frameStatus 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Status de Anticipos Cobrados"
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   3435
      TabIndex        =   28
      Top             =   435
      Width           =   3975
      Begin VB.ComboBox cmbStatus 
         Height          =   315
         Left            =   1305
         Sorted          =   -1  'True
         TabIndex        =   5
         Top             =   0
         Width           =   1935
      End
      Begin VB.Label lblStatus 
         AutoSize        =   -1  'True
         BackColor       =   &H80000016&
         BackStyle       =   0  'Transparent
         Caption         =   "Status Anticipos "
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   0
         TabIndex        =   29
         Top             =   60
         Width           =   1185
      End
   End
   Begin VB.Frame frmOrdenar 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Ordenar por"
      ForeColor       =   &H00808080&
      Height          =   825
      Left            =   7785
      TabIndex        =   36
      Top             =   345
      Width           =   1695
      Begin VB.OptionButton optOrdenar 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Estatus"
         ForeColor       =   &H00A84439&
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   38
         Top             =   465
         Width           =   1335
      End
      Begin VB.OptionButton optOrdenar 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Cliente"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   37
         Top             =   210
         Width           =   1335
      End
   End
   Begin VB.Label lblDatosDelReporte 
      AutoSize        =   -1  'True
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "lblDatosDelReporte"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000040C0&
      Height          =   300
      Left            =   3375
      TabIndex        =   22
      Top             =   60
      Width           =   2385
   End
End
Attribute VB_Name = "frmInformeDeAnticipo"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private mInformeSeleccionado As Integer
Private mTipoDeAnticipo As enum_TipoDeAnticipo
Private mDondeImprimir As enum_DondeImprimir
Private insCotizacion As Object
Private mNumeroCotizacionOriginal As String
Private insAnticipoSQL As clsAnticipoSQL
Private insClienteNavigator As Object
Private insProveedorNavigator As Object
Private insFacturaNavigator As Object
Private insConexionesSawAOS As Object
Private gProyCompaniaActual As Object

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "frmInformeDeAnticipo"
End Function

Private Function CM_MESSAGE_NAME() As String
   If mTipoDeAnticipo = eTDA_COBRADO Then
      CM_MESSAGE_NAME = "Informe de Anticipo de Cobrados"
   ElseIf mTipoDeAnticipo = eTDA_PAGADO Then
      CM_MESSAGE_NAME = "Informe de Anticipo de Pagos "
   End If
End Function

Private Function OPT_INFORME_ANTICIPO_ENTRE_FECHAS() As Integer
   OPT_INFORME_ANTICIPO_ENTRE_FECHAS = 2
End Function

Private Function OPT_INFORME_ANTICIPO_POR_CLIENTE() As Integer
  OPT_INFORME_ANTICIPO_POR_CLIENTE = 1
End Function

Private Function OPT_INFORME_ANTICIPO_ASOCIADO_CON_COTIZACION() As Integer
  OPT_INFORME_ANTICIPO_ASOCIADO_CON_COTIZACION = 3
End Function

Public Sub sInitLookAndFeel(ByVal valTipoDeAnticipo As enum_TipoDeAnticipo)
   On Error GoTo h_ERROR
   sInitDefaultValues
   mTipoDeAnticipo = valTipoDeAnticipo
   gEnumProyecto.FillComboBoxWithStatusAnticipo cmbStatus, Buscar
   gAPI.SelectTheElementInComboBox cmbStatus, gEnumProyecto.enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_VIGENTE)
   Me.Caption = CM_MESSAGE_NAME
   sInitFormValues
   Set insCotizacion = insFacturaNavigator
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbMonedaDeLosReportes_Click()
   On Error GoTo h_ERROR
   If (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal)) Then
      frameTasaDeCambio.Visible = True
   Else
      frameTasaDeCambio.Visible = False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbMonedaDeLosReporte_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdPantalla_Click()
  On Error GoTo h_ERROR
   mDondeImprimir = eDI_PANTALLA
   sEjecutaElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdPantalla_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImpresora_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_IMPRESORA
   sEjecutaElInformeSeleccionado
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
   Me.Width = 11115
   Me.Height = 4110
   mInformeSeleccionado = OPT_INFORME_ANTICIPO_POR_CLIENTE
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Unload(Cancel As Integer)
   Dim gFechasDeLosInformes As clsFechasDeLosInformesNav
   On Error GoTo h_ERROR
   Set gFechasDeLosInformes = New clsFechasDeLosInformesNav
   gFechasDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.Value, dtpFechaFinal.Value, gProyUsuarioActual.GetNombreDelUsuario
   Set gFechasDeLosInformes = Nothing
   Set insCotizacion = Nothing
   Set insAnticipoSQL = Nothing
   Set insClienteNavigator = Nothing
   Set insProveedorNavigator = Nothing
   Set insConexionesSawAOS = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Unload", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optInformeDeAnticipo_Click(Index As Integer)
   On Error GoTo h_ERROR
   mInformeSeleccionado = Index
   sOcultaTodosLosCampos
   Select Case mInformeSeleccionado
      Case OPT_INFORME_ANTICIPO_POR_CLIENTE: sActivarCamposDeAnticipoPorCliente
      Case OPT_INFORME_ANTICIPO_ENTRE_FECHAS: sActivarCamposDeAnticipoEntreFechas
      Case OPT_INFORME_ANTICIPO_ASOCIADO_CON_COTIZACION: sActivarCamposDeAnticipoAsociadosConCotizacion
      Case OPT_INFORME_ANTICIPO_A_UNA_FECHA: sActivarCamposDeAnticipoAUnaFecha
  End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInformeDeCxC_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElInformeSeleccionado()
   On Error GoTo h_ERROR
      Select Case mInformeSeleccionado
         Case OPT_INFORME_ANTICIPO_POR_CLIENTE: sEjecutaInformeAnticipoXClienteProveedor
         Case OPT_INFORME_ANTICIPO_ENTRE_FECHAS: sEjecutaInformeAnticipoEntreFechas
         Case OPT_INFORME_ANTICIPO_ASOCIADO_CON_COTIZACION: sEjecutaInformeAnticipoCobradosAsociadosConCotizacion
         Case OPT_INFORME_ANTICIPO_A_UNA_FECHA: sEjecutaInformeAnticipoAUnaFecha
      End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElInformeSeleccionado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeAnticipoEntreFechas()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - Anticipo entre Fechas"
   frameFechas.Visible = True
   lblFechaInicial.Visible = True
   lblFechaFinal.Visible = True
   frameStatus.Visible = True
   frameMoneda.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOCultaTodosLosCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeAnticipoPorCliente() '18/7
   On Error GoTo h_ERROR
   If mTipoDeAnticipo = eTDA_COBRADO Then '18/7
      lblDatosDelReporte.Caption = "Datos del Informe - Anticipo por Cliente"
      optOrdenar(0).Caption = "Cliente"
   Else '18/7
      lblDatosDelReporte.Caption = "Datos del Informe - Anticipo por Proveedor" '18/7
      optOrdenar(0).Caption = "Proveedor"
   End If
   frameCantidad.Visible = True
   frameClienteProveedor.Visible = True
   lblCodigo.Visible = True
   txtNombre.Visible = True
   frameMoneda.Visible = True
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      frameClienteProveedor.Visible = True
   Else
      frameClienteProveedor.Visible = False
   End If
   frameStatus.Visible = True
   frmOrdenar.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeCXCPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeAnticipoAsociadosConCotizacion() '18/7
   On Error GoTo h_ERROR
   If mTipoDeAnticipo = eTDA_COBRADO Then '18/7
      lblDatosDelReporte.Caption = "Datos del Informe - Anticipo Asociados con Cotizacion"
   End If
   frameCantidad.Visible = True
   frameClienteProveedor.Visible = False
   frameCotizacion.Visible = True
   frameMoneda.Visible = True
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      frameClienteProveedor.Visible = False
      frameCotizacion.Visible = True
   Else
      frameClienteProveedor.Visible = False
      frameCotizacion.Visible = False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeAnticipoAsociadosConCotizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sInitDefaultValues()
   Dim gFechasDeLosInformes As clsFechasDeLosInformesNav
   On Error GoTo h_ERROR
   Set gFechasDeLosInformes = New clsFechasDeLosInformesNav
   gFechasDeLosInformes.sLeeLasFechasDeInformes dtpFechaInicial, dtpFechaFinal, gProyUsuarioActual.GetNombreDelUsuario
   Set gFechasDeLosInformes = Nothing
   Set insAnticipoSQL = New clsAnticipoSQL
   gEnumReport.FillComboBoxWithMonedaDeLosReportes cmbMonedaDeLosReportes, gProyParametros.GetNombreMonedaLocal
   gAPI.SelectTheElementInComboBox cmbMonedaDeLosReportes, gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal)
   optTasaDeCambio(0).Value = True
   optOrdenar(0).Value = True
   If gProyParametros.GetUsaCodigoClienteEnPantalla Then
      lblCodigo.Caption = "Código"
      txtNombre.Enabled = False
   Else
      lblCodigo.Caption = "Nombre"
      txtNombre.Left = txtCodigo.Left
      txtCodigo.Visible = False
      txtCodigo.Text = ""
      txtNombre.Text = ""
   End If
   gEnumReport.FillComboBoxWithCantidadAImprimir CmbCantidadAImprimir
   gAPI.SelectTheElementInComboBox CmbCantidadAImprimir, gEnumReport.enumCantidadAImprimirToString(eCI_uno)
   gAPI.ssSetFocus optInformeDeAnticipo(OPT_INFORME_ANTICIPO_POR_CLIENTE)
   optInformeDeAnticipo(OPT_INFORME_ANTICIPO_POR_CLIENTE).Value = True
   dtpFechaInicial.Value = gUtilDate.getFechaDeHoy
   dtpFechaFinal.Value = gUtilDate.getFechaDeHoy
   dtpFechaHasta.Value = gUtilDate.getFechaDeHoy
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitDefaltValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigo_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigo_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigo_LostFocus()
   On Error GoTo h_ERROR
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigo_LostFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigo_Validate(Cancel As Boolean)
   Dim refCodigoProveedor As String
   Dim refNombreProveedor As String
   On Error GoTo h_ERROR
   Cancel = False
   If LenB(txtCodigo.Text) = 0 Then
      txtCodigo.Text = "*"
   End If
   If mTipoDeAnticipo = eTDA_COBRADO Then
      insClienteNavigator.sClrRecord
      insClienteNavigator.SetCodigo txtCodigo.Text
      If insClienteNavigator.fSearchSelectConnection(False, False, False, 0, False, True) Then
         sSelectAndSetValuesOfClienteOProveedor insClienteNavigator, Nothing
      Else
         Cancel = True
         gAPI.ssSetFocus txtCodigo
      End If
   Else
      If insConexionesSawAOS.fSelectAndSetValuesOfProveedorFromAOS(insProveedorNavigator, refCodigoProveedor, refNombreProveedor, txtCodigo.Text, "") Then
         sSelectAndSetValuesOfClienteOProveedor Nothing, insProveedorNavigator
      Else
         Cancel = True
         gAPI.ssSetFocus txtCodigo
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Cancel = True
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigo_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNumeroCotizacion_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigo_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNumeroCotizacion_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
      If LenB(txtNumeroCotizacion.Text) = 0 Then
             txtNumeroCotizacion.Text = "*"
      End If
      If UCase(txtNumeroCotizacion.Text) <> mNumeroCotizacionOriginal Then
         insCotizacion.sClrRecord
         insCotizacion.setClaseDeTrabajo eCTFC_Cotizacion
         insCotizacion.SetNumero txtNumeroCotizacion.Text
         If insCotizacion.fSearchSelectConnection(igMostrarMensaje) Then
            sSelectAndSetValuesOfCotizacion
            mNumeroCotizacionOriginal = UCase(txtNumeroCotizacion.Text)
         Else
            Cancel = True
            gAPI.ssSetFocus txtNumeroCotizacion
            GoTo h_EXIT
         End If
       End If
      Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Cancel = True
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigo_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtNombre_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombre_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombre_Validate(Cancel As Boolean)
   Dim refCodigoProveedor As String
   Dim refNombreProveedor As String
   On Error GoTo h_ERROR
   Cancel = False
   If LenB(txtNombre.Text) = 0 Then
      txtNombre.Text = "*"
   End If
   If mTipoDeAnticipo = eTDA_COBRADO Then
      insClienteNavigator.sClrRecord
      insClienteNavigator.SetNombre txtNombre.Text
      If insClienteNavigator.fSearchSelectConnection(False, False, False, 0, False, True) Then
         sSelectAndSetValuesOfClienteOProveedor insClienteNavigator, Nothing
      Else
         Cancel = True
         gAPI.ssSetFocus txtNombre
      End If
   Else
      If insConexionesSawAOS.fSelectAndSetValuesOfProveedorFromAOS(insProveedorNavigator, refCodigoProveedor, refNombreProveedor, txtNombre.Text, "NombreProveedor") Then
         sSelectAndSetValuesOfClienteOProveedor Nothing, insProveedorNavigator
      Else
         Cancel = True
         gAPI.ssSetFocus txtNombre
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Cancel = True
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombre_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCheckForSpecialKeys(ByVal valKeyCode As Integer, ByVal valShift As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, valKeyCode) Then
   End If
   Select Case valKeyCode
      Case vbKeyEscape: Unload Me
      Case vbKeyF6: gAPI.ssSetFocus cmdPantalla
         cmdPantalla_Click
      Case vbKeyF8: gAPI.ssSetFocus cmdImpresora
         cmdImpresora_Click
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCheckForSpecialKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaInformeAnticipoEntreFechas()
   Dim SqlDelReporte As String
   Dim insConfAnticipoCobradoEntreFecha As clsAnticipoRpt
   Dim rptAnticipoCobradoEntreFecha As DDActiveReports2.ActiveReport
   Dim monedaOriginal As Boolean
   Dim ValcmbStatus As String
   On Error GoTo h_ERROR
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
      dtpFechaFinal.Value = dtpFechaInicial.Value
   End If
   Set insConfAnticipoCobradoEntreFecha = New clsAnticipoRpt
   Set rptAnticipoCobradoEntreFecha = New DDActiveReports2.ActiveReport
   monedaOriginal = (cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gMonedaLocalActual.GetHoyNombreMoneda))
   ValcmbStatus = gAPI.SelectedElementInComboBoxToString(cmbStatus)
   SqlDelReporte = insAnticipoSQL.fSQLAnticipoCobradosPagadosEntreFechas(monedaOriginal, optTasaDeCambio(0).Value, gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, mTipoDeAnticipo, ValcmbStatus, gMonedaLocalActual, gUltimaTasaDeCambio)
   If insConfAnticipoCobradoEntreFecha.fConfigurarDatosDeAntiposCobradosPagadosEntreFechas(rptAnticipoCobradoEntreFecha, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, mTipoDeAnticipo, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
      gUtilReports.sMostrarOImprimirReporte rptAnticipoCobradoEntreFecha, 1, mDondeImprimir, "Anticipos entre Fechas"
   End If
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaInformeAnticipoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaInformeAnticipoXClienteProveedor()
   Dim SqlDelReporte As String
   Dim rptReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsAnticipoRpt
   Dim ClienteProveedorUnico As Boolean
   Dim ReporteEnMonedaLocal As Boolean
   Dim TituloReporte As String
   Dim ValcmbStatus As String
   On Error GoTo h_ERROR
   If LenB(txtNombre.Text) = 0 And LenB(txtCodigo.Text) = 0 And CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      If mTipoDeAnticipo = eTDA_COBRADO Then
         sShowMessageForRequiredFields "Nombre del Cliente", txtNombre
      Else
         sShowMessageForRequiredFields "Nombre del Proveedor", txtNombre
      End If
   Else
      Set rptReporte = New DDActiveReports2.ActiveReport
      Set insConfigurar = New clsAnticipoRpt
      ReporteEnMonedaLocal = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) <> gEnumReport.enumMonedaDeLosReportesToString(enum_MonedaDeLosReportes.eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal))
      If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
         ClienteProveedorUnico = True
      Else
         ClienteProveedorUnico = False
      End If
      If mTipoDeAnticipo = eTDA_COBRADO Then
         TituloReporte = "Anticipos Cobrados por Clientes"
      Else
         TituloReporte = "Anticipos Pagados por Proveedor"
      End If
      ValcmbStatus = gAPI.SelectedElementInComboBoxToString(cmbStatus)
      SqlDelReporte = insAnticipoSQL.fSQLAnticipoCobradosPagadosXClienteProveedor(ReporteEnMonedaLocal, optTasaDeCambio(0).Value, gProyCompaniaActual.GetConsecutivoCompania, mTipoDeAnticipo, txtCodigo.Text, ClienteProveedorUnico, gMonedaLocalActual, gUltimaTasaDeCambio, ValcmbStatus, optOrdenar(0).Value)
      If insConfigurar.fConfigurarDatosDeAntiposCobradosPagadosXClienteProveedor(rptReporte, SqlDelReporte, mTipoDeAnticipo, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), optOrdenar(0).Value) Then
         gUtilReports.sMostrarOImprimirReporte rptReporte, 1, eDI_PANTALLA, TituloReporte
      End If
      Set rptReporte = Nothing
      Set insConfigurar = Nothing
      
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaInformeAnticipoXClienteProveedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sShowMessageForRequiredFields(ByVal valCampo As String, ByRef refCampo As GSText)
   On Error GoTo h_ERROR
   gMessage.ShowRequiredFields valCampo
   gAPI.ssSetFocus refCampo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sShowMessageForRequiredFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbCantidadAImprimir_Click()
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case OPT_INFORME_ANTICIPO_POR_CLIENTE ', OPT_INFORME_ANTICIPO_POR_PROVEEDOR
         If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
            frameClienteProveedor.Visible = False
         Else
            frameClienteProveedor.Visible = True
            txtNombre.Text = ""
            txtCodigo.Text = ""
         End If
      Case OPT_INFORME_ANTICIPO_ASOCIADO_CON_COTIZACION
         If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
            frameCotizacion.Visible = False
         Else
            frameCotizacion.Visible = True
            txtNumeroCotizacion.Text = ""
         End If
       Case OPT_INFORME_ANTICIPO_A_UNA_FECHA
         If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
             frameClienteProveedor.Visible = False
         Else
           frameClienteProveedor.Visible = True
            txtNombre.Text = ""
            txtCodigo.Text = ""
         End If
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optTasaDeCambio_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optTasaDeCambio_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultaTodosLosCampos()
   On Error GoTo h_ERROR
   frameCantidad.Visible = False
   frameStatus.Visible = False
   frameFechas.Visible = False
   frameClienteProveedor.Visible = False
   frameMoneda.Visible = False
   frameTasaDeCambio.Visible = False
   frameCotizacion.Visible = False
   frameHasta.Visible = False
   frmOrdenar.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOcultaTodosLosCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitFormValues()
   On Error GoTo h_ERROR
   If mTipoDeAnticipo = eTDA_COBRADO Then
      lblInformesDeAnticipo.Caption = "Informe de Anticipo Cobrados"
      optInformeDeAnticipo(OPT_INFORME_ANTICIPO_POR_CLIENTE).Caption = "Anticipo por Cliente .........................."
      frameClienteProveedor.Caption = "Cliente "
      optInformeDeAnticipo(3).Visible = True
   Else
      lblInformesDeAnticipo.Caption = "Informe de Anticipo Pagados"
      optInformeDeAnticipo(OPT_INFORME_ANTICIPO_POR_CLIENTE).Caption = "Anticipo por Proveedor ...................."
      frameClienteProveedor.Caption = "Proveedor "
      optInformeDeAnticipo(3).Visible = False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitFormValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sSelectAndSetValuesOfClienteOProveedor(ByRef refCliente As Object, ByRef refProveedor As Object)
   On Error GoTo h_ERROR
   If mTipoDeAnticipo = eTDA_COBRADO Then
      If refCliente.fRsRecordCount(False) = 1 Then
         sAssignFieldsFromConnectionClienteOProveedor refCliente, Nothing
      ElseIf refCliente.fRsRecordCount(False) > 1 Then
         refCliente.sShowListSelect "Codigo", txtCodigo.Text
         sAssignFieldsFromConnectionClienteOProveedor refCliente, Nothing
      End If
   Else
      sAssignFieldsFromConnectionClienteOProveedor Nothing, refProveedor
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sSelectAndSetValuesOfClienteOProveedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAssignFieldsFromConnectionClienteOProveedor(ByVal valCliente As Object, ByVal valProveedor As Object)
   On Error GoTo h_ERROR
   If mTipoDeAnticipo = eTDA_COBRADO Then
      txtCodigo.Text = valCliente.GetCodigo
      txtNombre.Text = valCliente.GetNombre
   Else
      txtCodigo.Text = valProveedor.GetCodigoProveedor
      txtNombre.Text = valProveedor.GetNombreProveedor
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionClienteOProveedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sSelectAndSetValuesOfCotizacion()
   On Error GoTo h_ERROR
   If insCotizacion.fRsRecordCount(False) = 1 Then
      sAssignFieldsFromConnectionCotizacion
   ElseIf insCotizacion.fRsRecordCount(False) > 1 Then
      insCotizacion.sShowListSelect "Numero", txtNumeroCotizacion.Text
      sAssignFieldsFromConnectionCotizacion
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sSelectAndSetValuesOfCotizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAssignFieldsFromConnectionCotizacion()
   On Error GoTo h_ERROR
   txtNumeroCotizacion.Text = insCotizacion.GetNumero
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionCotizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaInformeAnticipoCobradosAsociadosConCotizacion()
   Dim SqlDelReporte As String
   Dim rptReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsAnticipoRpt
   Dim ReporteEnMonedaLocal As Boolean
   Dim valImprimirUno As Boolean
   On Error GoTo h_ERROR
   If LenB(txtNumeroCotizacion.Text) = 0 And LenB(txtNumeroCotizacion.Text) = 0 And CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
       sShowMessageForRequiredFields "Numero de Cotizacion", txtNumeroCotizacion
   Else
      Set rptReporte = New DDActiveReports2.ActiveReport
      Set insConfigurar = New clsAnticipoRpt
      ReporteEnMonedaLocal = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) <> gEnumReport.enumMonedaDeLosReportesToString(enum_MonedaDeLosReportes.eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal))
      If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
         valImprimirUno = True
      Else
         valImprimirUno = False
      End If

      SqlDelReporte = insAnticipoSQL.fSQLAnticipoAsociadosconCotizacion(ReporteEnMonedaLocal, optTasaDeCambio(0).Value, gProyCompaniaActual.GetConsecutivoCompania, mTipoDeAnticipo, valImprimirUno, txtNumeroCotizacion.Text, gMonedaLocalActual, gUltimaTasaDeCambio)
      
      If insConfigurar.fConfigurarDatosDeAntiposCobradosAsociadosConCotizacion(rptReporte, SqlDelReporte, mTipoDeAnticipo, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
         gUtilReports.sMostrarOImprimirReporte rptReporte, 1, eDI_PANTALLA, "Anticipos Cobrados por Cotización"
      End If
      Set rptReporte = Nothing
      Set insConfigurar = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaInformeAnticipoCobradosAsociadosConCotizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function OPT_INFORME_ANTICIPO_A_UNA_FECHA() As Integer
  OPT_INFORME_ANTICIPO_A_UNA_FECHA = 4
End Function

Private Sub sActivarCamposDeAnticipoAUnaFecha()
   On Error GoTo h_ERROR
   If mTipoDeAnticipo = eTDA_COBRADO Then
      lblDatosDelReporte.Caption = "Datos del Informe - Anticipo A una Fecha"
   End If
   frameFechas.Visible = False
   frameCantidad.Visible = True
   frameClienteProveedor.Visible = False
   frameCotizacion.Visible = False
   frameMoneda.Visible = True
   frameStatus.Visible = False
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      frameClienteProveedor.Visible = True
      frameCotizacion.Visible = False
   Else
      frameClienteProveedor.Visible = False
      frameCotizacion.Visible = False
   End If
   frameHasta.Visible = True
   frameHasta.Left = 3345
   frameHasta.Top = 1785
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeAnticipoAUnaFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaInformeAnticipoAUnaFecha()
   Dim SqlDelReporte As String
   Dim rptReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsAnticipoRpt
   Dim ReporteEnMonedaLocal As Boolean
   Dim valImprimirUno As Boolean
   Dim valoptTasaDeCambio As Boolean
   Dim Titulo As String

   On Error GoTo h_ERROR
      Set rptReporte = New DDActiveReports2.ActiveReport
      Set insConfigurar = New clsAnticipoRpt
      Titulo = "Anticipos A Una Fecha"
      If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And txtCodigo.Text = "" Then
         sShowMessageForRequiredFields "Nombre del Cliente", txtCodigo
      Else
         ReporteEnMonedaLocal = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) <> gEnumReport.enumMonedaDeLosReportesToString(enum_MonedaDeLosReportes.eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal))
         valoptTasaDeCambio = optTasaDeCambio(0).Value
         If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
            valImprimirUno = True
         Else
            valImprimirUno = False
         End If
         If mTipoDeAnticipo = 1 Then
            SqlDelReporte = insAnticipoSQL.fSQLAnticiposPagadosAUnaFecha(ReporteEnMonedaLocal, valoptTasaDeCambio, gProyCompaniaActual.GetConsecutivoCompania, mTipoDeAnticipo, txtCodigo.Text, valImprimirUno, gUltimaTasaDeCambio, dtpFechaHasta.Value, gMonedaLocalActual)
         Else
            SqlDelReporte = insAnticipoSQL.fSQLAnticiposCobradosAUnaFecha(ReporteEnMonedaLocal, valoptTasaDeCambio, gProyCompaniaActual.GetConsecutivoCompania, mTipoDeAnticipo, txtCodigo.Text, valImprimirUno, gMonedaLocalActual, gUltimaTasaDeCambio, dtpFechaHasta.Value)
         End If
         If insConfigurar.fConfigurarDatosDeAnticipoAUnaFecha(rptReporte, SqlDelReporte, mTipoDeAnticipo, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), dtpFechaHasta.Value) Then
            gUtilReports.sMostrarOImprimirReporte rptReporte, 1, mDondeImprimir, Titulo
         End If
      End If
      Set rptReporte = Nothing
      Set insConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaInformeAnticipoAUnaFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLoadObjectValues(ByVal valVendedorNavigator As Object, ByVal valClienteNavigator As Object, ByVal valProveedorNavigator As Object, ByVal valProyCompaniaActual As Object, ByVal valFacturaNavigator As Object, ByVal valConexionesSawAOS As Object, ByVal valTipoDeAnticipo As enum_TipoDeAnticipo)
On Error GoTo h_ERROR
   Set insClienteNavigator = valClienteNavigator
   Set gProyCompaniaActual = valProyCompaniaActual
   Set insProveedorNavigator = valProveedorNavigator
   Set insFacturaNavigator = valFacturaNavigator
   Set insConexionesSawAOS = valConexionesSawAOS
   Set gProyCompaniaActual = valProyCompaniaActual
   sInitLookAndFeel valTipoDeAnticipo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLoadObjectValues", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


