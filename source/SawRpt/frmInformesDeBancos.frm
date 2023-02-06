VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCT2.OCX"
Begin VB.Form frmInformesDeBancos 
   BackColor       =   &H00F3F3F3&
   Caption         =   "Informes de Bancos"
   ClientHeight    =   5535
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   10080
   LinkTopic       =   "Form1"
   ScaleHeight     =   5535
   ScaleWidth      =   10080
   Begin VB.Frame framePeriodo 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Periodo a Declarar"
      ForeColor       =   &H80000010&
      Height          =   675
      Left            =   3360
      TabIndex        =   36
      Top             =   1200
      Width           =   6015
      Begin VB.TextBox txtAno 
         Height          =   285
         Left            =   5220
         MaxLength       =   4
         TabIndex        =   13
         Top             =   240
         Width           =   735
      End
      Begin VB.TextBox txtMes 
         Height          =   285
         Left            =   4620
         MaxLength       =   2
         TabIndex        =   12
         Top             =   240
         Width           =   495
      End
      Begin VB.ComboBox cmbQuincenaAGenerar 
         Height          =   315
         Left            =   1620
         TabIndex        =   11
         Top             =   240
         Width           =   1635
      End
      Begin VB.Label lblMesAno 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Mes/Año "
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   3840
         TabIndex        =   38
         Top             =   300
         Width           =   705
      End
      Begin VB.Label lblQuincenaAGenerar 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "Quincena a Generar"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   37
         Top             =   300
         Width           =   1440
      End
   End
   Begin VB.CheckBox chkAgruparPorBeneficiario 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Agrupar por Beneficiario"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   7080
      TabIndex        =   10
      Top             =   1200
      Visible         =   0   'False
      Width           =   2295
   End
   Begin VB.Frame frameFechas 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Fechas"
      ForeColor       =   &H00808080&
      Height          =   1095
      Left            =   3360
      TabIndex        =   27
      Top             =   720
      Width           =   2115
      Begin MSComCtl2.DTPicker dtpFechaFinal 
         Height          =   285
         Left            =   720
         TabIndex        =   8
         Top             =   645
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   503
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   93585411
         CurrentDate     =   37187
      End
      Begin MSComCtl2.DTPicker dtpFechaInicial 
         Height          =   285
         Left            =   705
         TabIndex        =   7
         Top             =   240
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   503
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   93585411
         CurrentDate     =   37187
      End
      Begin VB.Label lblFechaInicial 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Inicial"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   29
         Top             =   300
         Width           =   405
      End
      Begin VB.Label lblFechaFinal 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Final"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   28
         Top             =   690
         Width           =   330
      End
   End
   Begin VB.Frame frameCuentaBancariaYConcepto 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Cuenta Bancaria / Concepto"
      ForeColor       =   &H00808080&
      Height          =   2295
      Left            =   3360
      TabIndex        =   26
      Top             =   2160
      Width           =   6015
      Begin VB.Frame frameConcepto 
         BackColor       =   &H00F3F3F3&
         BorderStyle     =   0  'None
         ForeColor       =   &H00808080&
         Height          =   975
         Left            =   120
         TabIndex        =   30
         Top             =   1275
         Width           =   5775
         Begin VB.ComboBox CmbCantidadAImprimir 
            Height          =   315
            Index           =   1
            Left            =   3000
            TabIndex        =   18
            Top             =   0
            Width           =   1575
         End
         Begin VB.TextBox txtDescripcionDeConcepto 
            Height          =   285
            Left            =   1440
            Locked          =   -1  'True
            TabIndex        =   20
            Top             =   600
            Width           =   4200
         End
         Begin VB.TextBox txtConcepto 
            Height          =   285
            Left            =   0
            TabIndex        =   19
            Top             =   600
            Width           =   1335
         End
         Begin VB.Label lblCantidadAImprimir 
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Cantidad Conceptos a Imprimir.................."
            ForeColor       =   &H00A84439&
            Height          =   195
            Index           =   1
            Left            =   0
            TabIndex        =   41
            Top             =   120
            Width           =   2955
         End
         Begin VB.Label lblDescripcionConcepto 
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Descripción"
            ForeColor       =   &H00A84439&
            Height          =   195
            Left            =   1440
            TabIndex        =   39
            Top             =   360
            Width           =   840
         End
         Begin VB.Label lblConcepto 
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Código"
            ForeColor       =   &H00A84439&
            Height          =   195
            Left            =   0
            TabIndex        =   31
            Top             =   360
            Width           =   495
         End
      End
      Begin VB.Frame frameCuentaBancaria 
         BackColor       =   &H00F3F3F3&
         BorderStyle     =   0  'None
         ForeColor       =   &H00808080&
         Height          =   900
         Left            =   120
         TabIndex        =   32
         Top             =   240
         Width           =   5775
         Begin VB.ComboBox CmbCantidadAImprimir 
            Height          =   315
            Index           =   0
            Left            =   3000
            TabIndex        =   15
            Top             =   0
            Width           =   1575
         End
         Begin VB.TextBox txtNumeroDeCuenta 
            Height          =   285
            Left            =   0
            TabIndex        =   16
            Top             =   600
            Width           =   1335
         End
         Begin VB.Label lblCantidadAImprimir 
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Cantidad Cuentas Bancarias a Imprimir....."
            ForeColor       =   &H00A84439&
            Height          =   195
            Index           =   0
            Left            =   0
            TabIndex        =   40
            Top             =   90
            Width           =   2940
         End
         Begin VB.Label lblNombreCuentaBancaria 
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Nombre"
            ForeColor       =   &H00A84439&
            Height          =   195
            Left            =   1440
            TabIndex        =   34
            Top             =   360
            Width           =   555
         End
         Begin VB.Label lblNombreCtaBancaria 
            BackColor       =   &H00F3F3F3&
            BorderStyle     =   1  'Fixed Single
            Caption         =   "lblNombreCtaBancaria"
            Height          =   285
            Left            =   1440
            TabIndex        =   17
            Top             =   600
            Width           =   4215
         End
         Begin VB.Label lblNumeroDeCuenta 
            AutoSize        =   -1  'True
            BackColor       =   &H80000005&
            BackStyle       =   0  'Transparent
            Caption         =   "Código"
            ForeColor       =   &H00A84439&
            Height          =   195
            Left            =   0
            TabIndex        =   33
            Top             =   360
            Width           =   495
         End
      End
   End
   Begin VB.CheckBox chkImprimirSoloCuentasActivas 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Sólo cuentas activas"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   7440
      TabIndex        =   9
      Top             =   840
      Width           =   1935
   End
   Begin VB.CommandButton CmdImprimir 
      Caption         =   "&Impresora"
      Height          =   375
      Left            =   120
      TabIndex        =   21
      Top             =   4560
      Width           =   1335
   End
   Begin VB.CommandButton cmdPantalla 
      Caption         =   "&Pantalla"
      Height          =   375
      Left            =   1755
      TabIndex        =   22
      Top             =   4560
      Width           =   1335
   End
   Begin VB.CommandButton CmdSalir 
      Cancel          =   -1  'True
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   3390
      TabIndex        =   23
      Top             =   4560
      Width           =   1335
   End
   Begin VB.Frame frameInformes 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   4215
      Left            =   120
      TabIndex        =   24
      Top             =   240
      Width           =   3015
      Begin VB.OptionButton optInformeDeBanco 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Declaración del  IGTF GO6687...."
         ForeColor       =   &H00A84439&
         Height          =   315
         Index           =   0
         Left            =   120
         TabIndex        =   42
         Top             =   3720
         Width           =   2715
      End
      Begin VB.OptionButton optInformeDeBanco 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Trans. por Cuenta &y Concepto......"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   8
         Left            =   120
         TabIndex        =   2
         Top             =   1280
         Width           =   2715
      End
      Begin VB.OptionButton optInformeDeBanco 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Transacciones por Beneficiario....."
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   6
         Left            =   120
         TabIndex        =   5
         Top             =   2660
         Width           =   2715
      End
      Begin VB.OptionButton optInformeDeBanco 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Declaración del Impuesto a las Transacciones Financieras..........."
         ForeColor       =   &H00A84439&
         Height          =   435
         Index           =   7
         Left            =   120
         TabIndex        =   6
         Top             =   3120
         Width           =   2715
      End
      Begin VB.OptionButton optInformeDeBanco 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Transacciones por &Cuenta..........."
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   0
         Top             =   360
         Width           =   2715
      End
      Begin VB.OptionButton optInformeDeBanco 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Transacciones por C&oncepto........"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   2
         Left            =   120
         TabIndex        =   1
         Top             =   820
         Width           =   2715
      End
      Begin VB.OptionButton optInformeDeBanco 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Transacciones por &Día................"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   3
         Left            =   120
         TabIndex        =   3
         Top             =   1740
         Width           =   2715
      End
      Begin VB.OptionButton optInformeDeBanco 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Flujo de Caja Histórico................"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   4
         Left            =   120
         TabIndex        =   4
         Top             =   2200
         Width           =   2715
      End
   End
   Begin VB.Frame frameSeleccion 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Selección"
      ForeColor       =   &H00808080&
      Height          =   855
      Left            =   3360
      TabIndex        =   35
      Top             =   2160
      Width           =   1695
      Begin VB.ComboBox cmbSeleccion 
         Height          =   315
         Left            =   120
         TabIndex        =   14
         Top             =   360
         Width           =   1815
      End
   End
   Begin VB.Label lblDatosDelReporte 
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
      Height          =   780
      Left            =   3300
      TabIndex        =   25
      Top             =   60
      Width           =   6465
   End
End
Attribute VB_Name = "frmInformesDeBancos"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "frmInformesDeBancos"
Private Const CM_MESSAGE_NAME As String = "Informes De Bancos"
Private gFechasDeLosInformes As clsFechasDeLosInformesNav
Private Const CM_OPT_TRANSACCIONES_POR_CUENTA As Integer = 1
Private Const CM_OPT_TRANSACCIONES_POR_CONCEPTO As Integer = 2
Private Const CM_OPT_TRANSACCIONES_POR_DIA As Integer = 3
Private Const CM_OPT_FLUJO_DE_CAJA_HISTORICO As Integer = 4
Private Const CM_OPT_TRANSACCIONES_POR_BENEFICIARIO As Integer = 6
Private Const CM_OPT_DECLARACION_IMPUESTO_TRANSACCIONES_FINANCIERAS As Integer = 7
Private Const CM_OPT_TRANSACCIONES_POR_CUENTA_Y_CONCEPTO As Integer = 8
Private Const CM_OPT_DECLARACION_DEL_IGTF_GO6687 As Integer = 0
Private Const CM_QDI_PrimeraQuincena As Integer = 0
Private Const CM_QDI_SegundaQuincena As Integer = 1
Private Const CM_QDI_TodoElMes As Integer = 2
Private mDondeImprimir As enum_DondeImprimir
Private mInformeSeleccionado As Integer
Private insConceptoBancario As Object
Private insCnxAos As Object
Private gProyCompaniaActual As Object
Private insCuenta As Object
Private gMonedaLocalActual As Object

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
Private Sub cmdImprimir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CmdImprimir_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub dtpFechaFinal_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
      If Not fSePuedeGenerarInforme Then
         gAPI.ssSetFocus dtpFechaFinal
         Cancel = False
         GoTo h_EXIT
      End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "dtpFechaFinal_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optInformeDeBanco_Click(Index As Integer)
   On Error GoTo h_ERROR
   mInformeSeleccionado = Index
   sOcultaCampos
   Select Case mInformeSeleccionado
      Case CM_OPT_TRANSACCIONES_POR_CUENTA: sImprimirTransaccionesXCuenta
      Case CM_OPT_TRANSACCIONES_POR_CONCEPTO: sImprimirTransaccionesXConcepto
      Case CM_OPT_TRANSACCIONES_POR_DIA: sImprimirTransaccionesXDia
      Case CM_OPT_FLUJO_DE_CAJA_HISTORICO: sImprimirFlujoDeCajaHistorico
      Case CM_OPT_TRANSACCIONES_POR_BENEFICIARIO: sImprimirTransaccionesXBeneficiario
      Case CM_OPT_DECLARACION_IMPUESTO_TRANSACCIONES_FINANCIERAS: sImprimirDeclaracionITF
      Case CM_OPT_TRANSACCIONES_POR_CUENTA_Y_CONCEPTO: sImprimirTransaccionesXCuentaYConcepto
      Case CM_OPT_DECLARACION_DEL_IGTF_GO6687: sImprimirDeclaracionIGTFGO6687
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInformeDeBanco_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub dtpFechaInicial_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "dtpFechaInicial_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub dtpFechaFinal_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "dtpFechaFinal_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub cmbCantidadAImprimir_Click(Index As Integer)
   On Error GoTo h_ERROR
   Select Case Index
      Case 0 'CuentaBancaria
         If CmbCantidadAImprimir(Index).Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
            If mInformeSeleccionado = CM_OPT_TRANSACCIONES_POR_CUENTA Or mInformeSeleccionado = CM_OPT_TRANSACCIONES_POR_BENEFICIARIO Or mInformeSeleccionado = CM_OPT_DECLARACION_IMPUESTO_TRANSACCIONES_FINANCIERAS Or mInformeSeleccionado = CM_OPT_DECLARACION_DEL_IGTF_GO6687 Or mInformeSeleccionado = CM_OPT_TRANSACCIONES_POR_CUENTA_Y_CONCEPTO Then
'               frameCuentaBancaria.Visible = False
               sOcultarOMostrarDatosCuentaBancaria False
               chkImprimirSoloCuentasActivas.Visible = True
            End If
         Else
            If mInformeSeleccionado = CM_OPT_TRANSACCIONES_POR_CUENTA Or mInformeSeleccionado = CM_OPT_TRANSACCIONES_POR_BENEFICIARIO Or mInformeSeleccionado = CM_OPT_DECLARACION_IMPUESTO_TRANSACCIONES_FINANCIERAS Or mInformeSeleccionado = CM_OPT_DECLARACION_DEL_IGTF_GO6687 Or mInformeSeleccionado = CM_OPT_TRANSACCIONES_POR_CUENTA_Y_CONCEPTO Then
'               frameCuentaBancaria.Visible = True
               sOcultarOMostrarDatosCuentaBancaria True
               chkImprimirSoloCuentasActivas.Visible = False
            End If
         End If
      Case 1 'ConceptoBancario
         If CmbCantidadAImprimir(Index).Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
            If mInformeSeleccionado = CM_OPT_TRANSACCIONES_POR_CONCEPTO Or mInformeSeleccionado = CM_OPT_TRANSACCIONES_POR_CUENTA_Y_CONCEPTO Then
'               frameConcepto.Visible = False
               sOcultarOMostrarDatosConceptoBancaria False
            End If
         Else
            If mInformeSeleccionado = CM_OPT_TRANSACCIONES_POR_CONCEPTO Or mInformeSeleccionado = CM_OPT_TRANSACCIONES_POR_CUENTA_Y_CONCEPTO Then
'               frameConcepto.Visible = True
               sOcultarOMostrarDatosConceptoBancaria True
            End If
         End If
      End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub cmbCantidadAImprimir_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimir_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub CmbCantidadAImprimir_KeyPress(Index As Integer, KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimir_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub cmdImprimir_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_IMPRESORA
      If Not fSePuedeGenerarInforme Then
         gAPI.ssSetFocus dtpFechaFinal
         GoTo h_EXIT
      End If
   sEjecutaElReporteApropiado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CmdImprimir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub cmdPantalla_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_PANTALLA
      If Not fSePuedeGenerarInforme Then
         gAPI.ssSetFocus dtpFechaFinal
         GoTo h_EXIT
      End If
   sEjecutaElReporteApropiado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdPantalla_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub cmdPantalla_KeyDown(KeyCode As Integer, Shift As Integer)
On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdPantalla_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub cmdSalir_Click()
   On Error GoTo h_ERROR
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub cmdSalir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Sub sInitLookAndFeel()
   On Error GoTo h_ERROR
   Me.Caption = CM_MESSAGE_NAME
   Set gFechasDeLosInformes = New clsFechasDeLosInformesNav
   gEnumReport.FillComboBoxWithCantidadAImprimir CmbCantidadAImprimir(0)
   gEnumReport.FillComboBoxWithCantidadAImprimir CmbCantidadAImprimir(1)
   CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno)
   CmbCantidadAImprimir(1).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno)
   CmbCantidadAImprimir(0).ListIndex = 0
   CmbCantidadAImprimir(1).ListIndex = 0
   gFechasDeLosInformes.sLeeLasFechasDeInformes dtpFechaInicial, dtpFechaFinal, gProyUsuarioActual.GetNombreDelUsuario
   chkImprimirSoloCuentasActivas = vbChecked
   gEnumReport.FillComboBoxWithReporteDetalladoResumido cmbSeleccion
   cmbSeleccion.Text = gEnumReport.enum_ReporteDetalladoResumidoToString(eRP_DETALLADO)
   cmbSeleccion.ListIndex = 1
   FillComboBoxWithQuincenaMes (False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   Me.Width = 10200
   Me.Height = 5565
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub Form_Unload(Cancel As Integer)
   On Error GoTo h_ERROR
   gFechasDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.Value, dtpFechaFinal.Value, gProyUsuarioActual.GetNombreDelUsuario
   Set gFechasDeLosInformes = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Unload", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtAno_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtAno
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtAno_KeyDown(KeyCode As Integer, Shift As Integer)
  On Error GoTo h_ERROR
  sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtAno_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.EsUnCaracterAsciiValidoParaCampoTipoIntegerSoloPositivo(KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtAno_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If Not txtAno.Visible Then
      GoTo h_EXIT
   End If
   If LenB(txtAno.Text) = 0 Then
      sShowMessageForRequiredFields "Año", txtAno
      Cancel = True
   ElseIf gTexto.DfLen(txtAno.Text) < 4 Then
      gMessage.Advertencia "Debe introducir un 'Año' de cuatro dígitos'"
      Cancel = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtMes_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.EsUnCaracterAsciiValidoParaCampoTipoIntegerSoloPositivo(KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtMes_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtMes
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtMes_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtMes_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If Not txtMes.Visible Then
      GoTo h_EXIT
   End If
   If LenB(txtMes.Text) = 0 Then
      sShowMessageForRequiredFields "Mes", txtMes
      Cancel = True
   ElseIf gConvert.ConvierteAInteger(txtMes.Text) < 1 Or gConvert.ConvierteAInteger(txtMes.Text) > 12 Then
      gMessage.Advertencia "Debe introducir un numero de 'Mes' válido"
      Cancel = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtNumeroDeCuenta_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtNumeroDeCuenta
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNumeroDeCuenta_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtNumeroDeCuenta_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNumeroDeCuenta_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtNumeroDeCuenta_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNumeroDeCuenta_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtNumeroDeCuenta_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If gTexto.DfLen(txtNumeroDeCuenta.Text) = 0 Then
      txtNumeroDeCuenta.Text = "*"
   End If
   If insCnxAos.fSelectAndSetValuesOfCuentaBancariaFromAOS(insCuenta, txtNumeroDeCuenta.Text, "", enum_StatusCtaBancaria.eSC_ACTIVA, "Gv_CuentaBancaria_B1.Status", "", False, False, False, True) Then
      txtNumeroDeCuenta.Text = insCuenta.GetCodigo
      lblNombreCtaBancaria.Caption = insCuenta.GetNombreCuenta
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNumeroDeCuenta_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtConcepto_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtConcepto
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtConcepto_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtConcepto_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtConcepto_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtConcepto_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtConcepto_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAssignFieldsFromConnectionConcepto(ByVal valConceptoBancario As Object)
   On Error GoTo h_ERROR
   txtConcepto.Text = valConceptoBancario.GetCodigo
   txtDescripcionDeConcepto.Text = valConceptoBancario.GetDescripcion
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionConcepto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtConcepto_Validate(Cancel As Boolean)
   Dim campo As String
   Dim valor As String
   On Error GoTo h_ERROR
   If LenB(txtConcepto.Text) = 0 Then
      txtConcepto.Text = "*"
      txtDescripcionDeConcepto.Text = ""
   Else
      txtDescripcionDeConcepto.Text = ""
   End If
   insConceptoBancario.sClrRecord
   insConceptoBancario.SetCodigo txtConcepto.Text
   If gProyParametros.GetUsaCodigoConceptoBancarioEnPantalla Then
      campo = "Codigo"
      valor = txtConcepto.Text
   Else
      campo = "Descripcion"
      valor = txtDescripcionDeConcepto.Text
   End If
   If insCnxAos.fSelectAndSetValuesOfConceptoBancarioFromAOS(insConceptoBancario, valor, campo) Then
      sAssignFieldsFromConnectionConcepto insConceptoBancario
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " txtConcepto_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtDescripcionDeConcepto_Validate(Cancel As Boolean)
   Dim campo As String
   Dim valor As String
   On Error GoTo h_ERROR
   If LenB(txtDescripcionDeConcepto.Text) = 0 Then
      txtDescripcionDeConcepto.Text = "*"
   End If
   insConceptoBancario.sClrRecord
   insConceptoBancario.SetDescripcion txtDescripcionDeConcepto.Text
   If gProyParametros.GetUsaCodigoConceptoBancarioEnPantalla Then
      campo = "Codigo"
      valor = txtConcepto.Text
   Else
      campo = "Descripcion"
      valor = txtDescripcionDeConcepto.Text
   End If
   If insCnxAos.fSelectAndSetValuesOfConceptoBancarioFromAOS(insConceptoBancario, valor, campo) Then
      sAssignFieldsFromConnectionConcepto insConceptoBancario
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " txtDescripcionDeConcepto_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sEjecutaElReporteApropiado()
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case CM_OPT_TRANSACCIONES_POR_CUENTA: sEjecutaElReporteTransaccionesXCtaBancaria
      Case CM_OPT_TRANSACCIONES_POR_CONCEPTO: sEjecutaElReporteTransaccionesXConcepto
      Case CM_OPT_TRANSACCIONES_POR_DIA: sEjecutaElReporteTransaccionesXDia
      Case CM_OPT_FLUJO_DE_CAJA_HISTORICO: sEjecutaElReporteFlujoDeCajaHistorico
      Case CM_OPT_TRANSACCIONES_POR_BENEFICIARIO: sEjecutaElReporteTransaccionesXBeneficirio
      Case CM_OPT_DECLARACION_IMPUESTO_TRANSACCIONES_FINANCIERAS: sEjecutaElReporteDeclaracionITF
      Case CM_OPT_TRANSACCIONES_POR_CUENTA_Y_CONCEPTO: sEjecutaElReporteTransaccionesXConceptoXCuentaBancaria
      Case CM_OPT_DECLARACION_DEL_IGTF_GO6687: sEjecutaElReporteDeclaracionITFGO6687
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteApropiado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImprimirTransaccionesXCuenta()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Reporte - Transacciones por Cuenta"
   frameCuentaBancariaYConcepto.Visible = True
   frameCuentaBancariaYConcepto.Caption = "Cuenta Bancaria"
   frameCuentaBancaria.Top = 270
   frameCuentaBancaria.Visible = True
   frameConcepto.Top = 1275
   frameConcepto.Visible = False
   If CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
'      frameCuentaBancaria.Visible = True
      sOcultarOMostrarDatosCuentaBancaria True
   Else
'      frameCuentaBancaria.Visible = False
      sOcultarOMostrarDatosCuentaBancaria False
      chkImprimirSoloCuentasActivas.Visible = True
   End If
   frameFechas.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimirTransaccionesXCuenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sImprimirTransaccionesXConcepto()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Reporte - Transacciones por Concepto"
   frameCuentaBancariaYConcepto.Visible = True
   frameCuentaBancariaYConcepto.Caption = "Concepto"
   frameConcepto.Top = 270
   frameConcepto.Visible = True
   frameCuentaBancaria.Top = 1275
   frameCuentaBancaria.Visible = False
   If CmbCantidadAImprimir(1).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
'      frameConcepto.Visible = True
      sOcultarOMostrarDatosConceptoBancaria True
   Else
'      frameConcepto.Visible = False
      sOcultarOMostrarDatosConceptoBancaria False
   End If
   frameFechas.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimirTransaccionesXConcepto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sImprimirTransaccionesXDia()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Reporte - Transacciones por Día"
   frameFechas.Visible = True
   lblFechaFinal.Visible = False
   dtpFechaFinal.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimirTransaccionesXDia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sImprimirTransaccionesXBeneficiario()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Reporte - Transacciones por Cuenta Con Beneficiario"
   frameCuentaBancariaYConcepto.Visible = True
   frameCuentaBancariaYConcepto.Caption = "Cuenta Bancaria"
   frameCuentaBancaria.Top = 270
   frameCuentaBancaria.Visible = True
   frameConcepto.Top = 1275
   frameConcepto.Visible = False
   If CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
'      frameCuentaBancaria.Visible = True
      sOcultarOMostrarDatosCuentaBancaria True
   Else
'      frameCuentaBancaria.Visible = False
      sOcultarOMostrarDatosCuentaBancaria False
      chkImprimirSoloCuentasActivas.Visible = True
   End If
   chkAgruparPorBeneficiario.Visible = True
   frameFechas.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimirTransaccionesXBeneficiario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sImprimirFlujoDeCajaHistorico()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Reporte - Flujo De Caja Historico"
   frameFechas.Visible = True
   frameSeleccion.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimirFlujoDeCajaHistorico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElReporteTransaccionesXCtaBancaria()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsBancoRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insBancosSQL As clsBancosSQL
   On Error GoTo h_ERROR
   If CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And txtNumeroDeCuenta.Text = "" Then
      sShowMessageForRequiredFields "Numero de Cuenta", txtNumeroDeCuenta
   Else
      If dtpFechaFinal.Value < dtpFechaInicial.Value Then
         dtpFechaInicial.Value = dtpFechaFinal.Value
      End If
      Set reporte = New DDActiveReports2.ActiveReport
      Set insConfigurar = New clsBancoRpt
      Set insBancosSQL = New clsBancosSQL
      SqlDelReporte = insBancosSQL.fSQLTransaccionesPorCuenta(gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, mInformeSeleccionado = CM_OPT_TRANSACCIONES_POR_BENEFICIARIO, gDefProg.fEsSistemaAdmInfotax, gAPI.fGetCheckBoxValue(chkImprimirSoloCuentasActivas), gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir(0)), txtNumeroDeCuenta.Text, gConvert.ConvertByteToBoolean(chkAgruparPorBeneficiario.Value), True, gGlobalization)
      If insConfigurar.fConfiguraDatosDelReporteDeTransaccionesPorCuentaBancaria(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), gGlobalization) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Transacciones por Cuenta Bancaria"
      End If
      
      
      
      Set reporte = Nothing
      Set insConfigurar = Nothing
      Set insBancosSQL = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteTransaccionesXCtaBancaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sEjecutaElReporteTransaccionesXConcepto()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsBancoRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insClsBancosSQL As clsBancosSQL
   Dim tituloDelReporte As String
   On Error GoTo h_ERROR
   If CmbCantidadAImprimir(1).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And txtConcepto.Text = "" Then
      sShowMessageForRequiredFields "Codigo Concepto Bancario", txtConcepto
   Else
      If dtpFechaFinal.Value < dtpFechaInicial.Value Then
         dtpFechaInicial.Value = dtpFechaFinal.Value
      End If
      Set insConfigurar = New clsBancoRpt
      Set reporte = New DDActiveReports2.ActiveReport
      Set insClsBancosSQL = New clsBancosSQL
      tituloDelReporte = "Transacciones Bancarias por Concepto"
      SqlDelReporte = insClsBancosSQL.fSQLTransaccionesPorConcepto(gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, CmbCantidadAImprimir(1).Text, txtConcepto.Text)
      If insConfigurar.fConfiguraDatosDelReporteDeTransaccionesPorConcepto(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Transacciones por Concepto"
      End If
      Set reporte = Nothing
      Set insConfigurar = Nothing
      Set insClsBancosSQL = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteTransaccionesXConcepto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sEjecutaElReporteTransaccionesXDia()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsBancoRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insClsBancosSQL As clsBancosSQL
   On Error GoTo h_ERROR
   Set reporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsBancoRpt
   Set insClsBancosSQL = New clsBancosSQL
   SqlDelReporte = insClsBancosSQL.fSQLTransaccionesPorDia(gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "movimientoBancario.Monto", "movimientoBancario.Fecha"))
   If insConfigurar.fConfiguraDatosDelReporteDeTransaccionesPorDia(reporte, SqlDelReporte, dtpFechaInicial.Value, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Transacciones por Día"
   End If
   Set insConfigurar = Nothing
   Set reporte = Nothing
   Set insClsBancosSQL = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteTransaccionesXDia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sEjecutaElReporteFlujoDeCajaHistorico()
   Dim SqlDelReporte As String
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insClsBancosSQL As clsBancosSQL
   Dim InsBancoDsr As clsBancoDsr
   On Error GoTo h_ERROR
   Set insClsBancosSQL = New clsBancosSQL
   Set InsBancoDsr = New clsBancoDsr
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
       dtpFechaInicial.Value = dtpFechaFinal.Value
   End If
   SqlDelReporte = insClsBancosSQL.fSQLFlujoDeCajaHistorico(gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "MovimientoBancario.Monto", "MovimientoBancario.Fecha"))
   Set reporte = InsBancoDsr.fConfigurarDsrFlujoDeCajaHistorico(SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, cmbSeleccion.ListIndex, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gUtilSQL, gProyCompaniaActual, gEnumProyecto)
   gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Flujo de Caja Histórico"
   Set reporte = Nothing
   Set insClsBancosSQL = Nothing
   Set InsBancoDsr = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteFlujoDeCajaHistorico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElReporteTransaccionesXBeneficirio()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsBancoRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insBancosSQL As clsBancosSQL
   On Error GoTo h_ERROR
   If CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And txtNumeroDeCuenta.Text = "" Then
      sShowMessageForRequiredFields "Numero de Cuenta", txtNumeroDeCuenta
   Else
      If dtpFechaFinal.Value < dtpFechaInicial.Value Then
         dtpFechaInicial.Value = dtpFechaFinal.Value
      End If
      Set reporte = New DDActiveReports2.ActiveReport
      Set insConfigurar = New clsBancoRpt
      Set insBancosSQL = New clsBancosSQL
      SqlDelReporte = insBancosSQL.fSQLTransaccionesPorCuenta(gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, mInformeSeleccionado = CM_OPT_TRANSACCIONES_POR_BENEFICIARIO, gDefProg.fEsSistemaAdmInfotax, gAPI.fGetCheckBoxValue(chkImprimirSoloCuentasActivas), gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir(0)), txtNumeroDeCuenta.Text, gConvert.ConvertByteToBoolean(chkAgruparPorBeneficiario.Value), True, gGlobalization)
      If insConfigurar.fConfiguraDatosDelReporteDeTransaccionesPorBeneficiario(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, gConvert.ConvertByteToBoolean(chkAgruparPorBeneficiario.Value), gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Transacciones por Cuenta Bancaria con Beneficiario"
      End If
      Set reporte = Nothing
      Set insConfigurar = Nothing
      Set insBancosSQL = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteTransaccionesXBeneficirio", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sShowMessageForRequiredFields(ByVal valCampo As String, ByRef refCampo As TextBox)
   On Error GoTo h_ERROR
   gMessage.ShowRequiredFields valCampo
   gAPI.ssSetFocus refCampo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sShowMessageForRequiredFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sOcultaCampos()
   On Error GoTo h_ERROR
   lblFechaFinal.Visible = True
   dtpFechaFinal.Visible = True
   lblNombreCtaBancaria.Caption = ""
   frameCuentaBancariaYConcepto.Visible = False
'   frameCuentaBancaria.Visible = False
   sOcultarOMostrarDatosCuentaBancaria False
'   frameConcepto.Visible = False
   sOcultarOMostrarDatosConceptoBancaria False
   frameFechas.Visible = False
   chkImprimirSoloCuentasActivas.Visible = False
   frameCuentaBancaria.Top = 270
   frameConcepto.Top = 1275
   frameSeleccion.Visible = False
   chkAgruparPorBeneficiario.Visible = False
   framePeriodo.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOcultaCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Function fSePuedeGenerarInforme() As Boolean
   On Error GoTo h_ERROR
    fSePuedeGenerarInforme = True
   If Not optInformeDeBanco(3).Value And Not optInformeDeBanco(7).Value Then
      If dtpFechaFinal.Value > gUtilDate.SumaNdias(gMonedaLocalActual.GetHoyVigenteDesde, -1) And dtpFechaInicial.Value < gUtilDate.SumaNdias(gMonedaLocalActual.GetHoyVigenteDesde, -1) Then
         gMessage.Advertencia "Los Informes de Banco sólo pueden tener documentos en una moneda, por lo que la fecha " & gUtilDate.SumaNdias(gMonedaLocalActual.GetHoyVigenteDesde, -1) & " no puede estar entre las fechas de inicio y fin." & vbCrLf & "Coloque " & gUtilDate.SumaNdias(gMonedaLocalActual.GetHoyVigenteDesde, -1) & " como Fecha Fin, si desea imprimir la porción del informe en Bs." & vbCrLf & "Coloque " & gMonedaLocalActual.GetHoyVigenteDesde & " como Fecha Inicio si desea imprimir la porción del Informe en Bs.F"
          fSePuedeGenerarInforme = False
      ElseIf dtpFechaFinal.Value < dtpFechaInicial.Value Then
         gMessage.Advertencia "La fecha inicial es mayor a la fecha final"
         fSePuedeGenerarInforme = False
      End If
   End If
h_EXIT:    On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sVerificarPeriodoDeVigencia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Sub sImprimirDeclaracionITF()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - Declaración a las Transacciones Financieras"
   framePeriodo.Visible = True
   frameCuentaBancariaYConcepto.Visible = True
   frameCuentaBancariaYConcepto.Caption = "Cuenta Bancaria"
   frameCuentaBancaria.Top = 270
   frameCuentaBancaria.Visible = True
   frameConcepto.Top = 1275
   frameConcepto.Visible = False
   If CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
'      frameCuentaBancaria.Visible = True
      sOcultarOMostrarDatosCuentaBancaria True
   Else
'      frameCuentaBancaria.Visible = False
      sOcultarOMostrarDatosCuentaBancaria False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimirDeclaracionITF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sEjecutaElReporteDeclaracionITF()
   Dim FechaInicial As Date
   Dim FechaFinal As Date
   Dim SqlDelReporte As String
   Dim insConfigurar As clsBancoRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insBancosSQL As clsBancosSQL
   On Error GoTo h_ERROR
   Set insBancosSQL = New clsBancosSQL

   If txtMes.Text = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
      GoTo h_EXIT
   End If
   If txtAno.Text = "" Then
      sShowMessageForRequiredFields "Año", txtAno
      GoTo h_EXIT
   End If
   FechaInicial = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, True)
   FechaFinal = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, False)
   If gAPI.SelectedElementInComboBoxToString(cmbQuincenaAGenerar) = CM_QuincenaMes(CM_QDI_PrimeraQuincena) Then
      FechaFinal = gUtilDate.fColocaDiaEnFecha(15, FechaFinal)
   ElseIf gAPI.SelectedElementInComboBoxToString(cmbQuincenaAGenerar) = CM_QuincenaMes(CM_QDI_SegundaQuincena) Then
      FechaInicial = gUtilDate.fColocaDiaEnFecha(16, FechaInicial)
   End If
   Set reporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsBancoRpt
'   SqlDelReporte = fSQLDeclaracionITF(FechaInicial, FechaFinal, txtNumeroDeCuenta.Text)
   SqlDelReporte = insBancosSQL.fSQLDeclaracionITF(FechaInicial, FechaFinal, txtNumeroDeCuenta.Text, gProyParametrosCompania.GetConsecutivoCompania, gAPI.fGetCheckBoxValue(chkImprimirSoloCuentasActivas), CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno))

   If insConfigurar.fConfiguraDatosDelReporteDeDeclaracionImpuestoTransaccionesFinancieras(reporte, SqlDelReporte, FechaInicial, FechaFinal, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Declaración del Impuesto a las Transacciones Financieras"
   End If
   Set insBancosSQL = Nothing
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteDeclaracionITF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub FillComboBoxWithQuincenaMes(ByVal valIncluirTodoElMes As Boolean)
   Dim nCount As Integer
   On Error GoTo h_ERROR
   cmbQuincenaAGenerar.Clear
   nCount = 0
   cmbQuincenaAGenerar.Text = CM_QuincenaMes(CM_QDI_PrimeraQuincena)
   cmbQuincenaAGenerar.List(nCount) = CM_QuincenaMes(CM_QDI_PrimeraQuincena)
   nCount = nCount + 1
   cmbQuincenaAGenerar.List(nCount) = CM_QuincenaMes(CM_QDI_SegundaQuincena)
   If valIncluirTodoElMes Then
      nCount = nCount + 1
      cmbQuincenaAGenerar.List(nCount) = CM_QuincenaMes(CM_QDI_TodoElMes)
   End If
   cmbQuincenaAGenerar.Width = gAPI.sugestedWidthForComboBox(cmbQuincenaAGenerar)
   cmbQuincenaAGenerar.ListIndex = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "FillComboBoxWithQuincenaDeclaracionInformativa", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Function CM_QuincenaMes(ByVal valQuincenaADeclarar As Integer) As String
   On Error GoTo h_ERROR
   Select Case valQuincenaADeclarar
      Case CM_QDI_PrimeraQuincena: CM_QuincenaMes = "Primera Quincena"
      Case CM_QDI_SegundaQuincena: CM_QuincenaMes = "Segunda Quincena"
      Case CM_QDI_TodoElMes: CM_QuincenaMes = "Todo el Mes"
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "enumQuincenaDeclaracionInformativaToString", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sImprimirTransaccionesXCuentaYConcepto()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Reporte - Transacciones por Cuenta Y Concepto"
   frameCuentaBancariaYConcepto.Visible = True
   frameCuentaBancariaYConcepto.Caption = "Cuenta Bancaria / Concepto"
   frameCuentaBancaria.Top = 270
   frameCuentaBancaria.Visible = True
   frameConcepto.Top = 1275
   frameConcepto.Visible = True
   If CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      sOcultarOMostrarDatosCuentaBancaria True
   Else
      sOcultarOMostrarDatosCuentaBancaria False
      chkImprimirSoloCuentasActivas.Visible = True
   End If
   frameFechas.Visible = True
   If CmbCantidadAImprimir(1).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      sOcultarOMostrarDatosConceptoBancaria True
   Else
      sOcultarOMostrarDatosConceptoBancaria False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimirTransaccionesXCuentaYConcepto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultarOMostrarDatosCuentaBancaria(ByVal Visibilidad As Boolean)
   On Error GoTo h_ERROR
   lblNumeroDeCuenta.Visible = Visibilidad
   lblNombreCuentaBancaria.Visible = Visibilidad
   txtNumeroDeCuenta.Visible = Visibilidad
   lblNombreCtaBancaria.Visible = Visibilidad
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOcultarOMostrarDatosCuentaBancaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultarOMostrarDatosConceptoBancaria(ByVal Visibilidad As Boolean)
   On Error GoTo h_ERROR
   lblConcepto.Visible = Visibilidad
   lblDescripcionConcepto.Visible = Visibilidad
   txtConcepto.Visible = Visibilidad
   txtDescripcionDeConcepto.Visible = Visibilidad
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOcultarOMostrarDatosCuentaBancaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElReporteTransaccionesXConceptoXCuentaBancaria()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsBancoRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insClsBancosSQL As clsBancosSQL
   Dim tituloDelReporte As String
   On Error GoTo h_ERROR
   If CmbCantidadAImprimir(1).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And txtConcepto.Text = "" Then
      sShowMessageForRequiredFields "Codigo Concepto Bancario", txtConcepto
      GoTo h_EXIT
   End If
   If CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And txtNumeroDeCuenta.Text = "" Then
      sShowMessageForRequiredFields "Codigo Cuenta Bancaria", txtNumeroDeCuenta
      GoTo h_EXIT
   End If
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
      dtpFechaInicial.Value = dtpFechaFinal.Value
   End If
   Set insConfigurar = New clsBancoRpt
   Set reporte = New DDActiveReports2.ActiveReport
   Set insClsBancosSQL = New clsBancosSQL
   tituloDelReporte = "Transacciones Bancarias por Concepto y Cuenta Bancaria"
   
   SqlDelReporte = insClsBancosSQL.fSQLTransaccionesPorConceptoYCuentaBancaria(gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, CmbCantidadAImprimir(1).Text, txtConcepto.Text, gAPI.fGetCheckBoxValue(chkImprimirSoloCuentasActivas), gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir(0)), txtNumeroDeCuenta.Text)
   If insConfigurar.fConfiguraDatosDelReporteDeTransaccionesPorConcepto(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Transacciones por Concepto Y Cuenta Bancaria"
   End If
   Set reporte = Nothing
   Set insConfigurar = Nothing
   Set insClsBancosSQL = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteTransaccionesXConceptoXCuentaBancaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLoadObjectValues(ByVal valCompaniaActual As Object, ByVal valConexionAOS As Object, ByVal valInsCuenta As Object, ByVal valGMonedaLocalActual As Object, ByVal valInsConceptoBancario As Object, ByVal valProyParametrosCompania As Object)
On Error GoTo h_ERROR
   Set gProyCompaniaActual = valCompaniaActual
   Set insCnxAos = valConexionAOS
   Set insCuenta = valInsCuenta
   Set gMonedaLocalActual = valGMonedaLocalActual
   Set insConceptoBancario = valInsConceptoBancario
   Set gProyParametrosCompania = valProyParametrosCompania
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLoadObjectValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sEjecutaElReporteDeclaracionITFGO6687()
   Dim FechaInicial As Date
   Dim FechaFinal As Date
   Dim SqlDelReporte As String
   Dim insConfigurar As clsBancoRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insBancosSQL As clsBancosSQL
   On Error GoTo h_ERROR
   Set insBancosSQL = New clsBancosSQL
   FechaInicial = dtpFechaInicial.Value
   FechaFinal = dtpFechaFinal.Value
   Set reporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsBancoRpt
   
   SqlDelReporte = insBancosSQL.fSQLDeclaracionITFGO6687(FechaInicial, FechaFinal, txtNumeroDeCuenta.Text, gProyParametrosCompania.GetConsecutivoCompania, gAPI.fGetCheckBoxValue(chkImprimirSoloCuentasActivas), CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno), gProyParametrosCompania.GetRedondeaMontoDebitoBancario, gProyParametrosCompania.GetConceptoDebitoBancario, gProyParametrosCompania.GetConceptoBancarioReversoDePago, gProyParametrosCompania.GetConceptoBancarioReversoTransfEgreso)
   If insConfigurar.fConfiguraDatosDelReporteDeDeclaracionImpuestoTransaccionesFinancierasGO6687(reporte, SqlDelReporte, FechaInicial, FechaFinal, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Declaración del Impuesto a las Transacciones Financieras"
   End If
   Set insBancosSQL = Nothing
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteDeclaracionITFGO6687", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sImprimirDeclaracionIGTFGO6687()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Informe - Declaración a las Grandes Transacciones Financieras"
   frameCuentaBancariaYConcepto.Visible = True
   frameCuentaBancariaYConcepto.Caption = "Cuenta Bancaria"
   frameCuentaBancaria.Top = 270
   frameCuentaBancaria.Visible = True
   frameConcepto.Top = 1275
   frameConcepto.Visible = False
   frameFechas.Visible = True
   If CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      sOcultarOMostrarDatosCuentaBancaria True
   Else
      sOcultarOMostrarDatosCuentaBancaria False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimirDeclaracionITF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
