VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCT2.OCX"
Begin VB.Form frmInformesDeCXP 
   BackColor       =   &H00F3F3F3&
   Caption         =   "Informes De CxP"
   ClientHeight    =   5790
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   10680
   LinkTopic       =   "Form1"
   ScaleHeight     =   5790
   ScaleWidth      =   10680
   Begin VB.CheckBox ChkSepararRetenciones 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Mostrar Las Retenciones Por Separado"
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
      Height          =   372
      Left            =   3480
      TabIndex        =   59
      Top             =   3720
      Width           =   4410
   End
   Begin VB.Frame frameStatus 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Status de C x P"
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   3480
      TabIndex        =   56
      Top             =   1560
      Width           =   3855
      Begin VB.ComboBox cmbStatus 
         Height          =   315
         HelpContextID   =   1
         Left            =   840
         TabIndex        =   16
         Top             =   0
         Width           =   1474
      End
      Begin VB.Label lblStatus 
         AutoSize        =   -1  'True
         BackColor       =   &H80000016&
         BackStyle       =   0  'Transparent
         Caption         =   "Status CxP"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   0
         TabIndex        =   57
         Top             =   60
         Width           =   780
      End
   End
   Begin VB.CheckBox chkTotalizarPorMes 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Totalizar por Mes"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   3480
      TabIndex        =   29
      Top             =   4440
      Width           =   2235
   End
   Begin VB.Frame pnlEmitirResumenParaVentas 
      BackColor       =   &H00F3F3F3&
      Height          =   570
      Left            =   3495
      TabIndex        =   52
      Top             =   3645
      Visible         =   0   'False
      Width           =   4815
      Begin VB.ComboBox cmbResumenDeLibros 
         Height          =   315
         Left            =   1575
         TabIndex        =   28
         Top             =   135
         Width           =   1335
      End
      Begin VB.Label lblEmitirResumenParaVentas 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Emitir Resumen"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   53
         Top             =   195
         Width           =   1095
      End
   End
   Begin VB.CheckBox chkMostrarInformeDeProrrateo 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Mostrar Informe De Prorrateo"
      ForeColor       =   &H00A84439&
      Height          =   240
      Left            =   3480
      TabIndex        =   25
      Top             =   3360
      Width           =   3015
   End
   Begin VB.Frame frameFechas 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Fechas"
      ForeColor       =   &H00808080&
      Height          =   915
      Left            =   3495
      TabIndex        =   47
      Top             =   495
      Width           =   2115
      Begin MSComCtl2.DTPicker dtpFechaFinal 
         Height          =   255
         Left            =   660
         TabIndex        =   12
         Top             =   540
         Width           =   1335
         _ExtentX        =   2355
         _ExtentY        =   450
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   88801283
         CurrentDate     =   36978
      End
      Begin MSComCtl2.DTPicker dtpFechaInicial 
         Height          =   255
         Left            =   660
         TabIndex        =   11
         Top             =   180
         Width           =   1335
         _ExtentX        =   2355
         _ExtentY        =   450
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   88801283
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
         TabIndex        =   49
         Top             =   600
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
         TabIndex        =   48
         Top             =   240
         Width           =   405
      End
   End
   Begin VB.Frame frameOrdenadoPor 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   3495
      TabIndex        =   50
      Top             =   555
      Width           =   4455
      Begin VB.ComboBox cmbOrdenadoPor 
         Height          =   315
         Left            =   1050
         TabIndex        =   15
         Top             =   0
         Width           =   1575
      End
      Begin VB.Label lblOrdenadoPor 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Ordenado por"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   0
         TabIndex        =   51
         Top             =   60
         Width           =   975
      End
   End
   Begin VB.Frame frameInforme 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Informe"
      ForeColor       =   &H00808080&
      Height          =   915
      Left            =   7560
      TabIndex        =   35
      Top             =   1380
      Width           =   1395
      Begin VB.OptionButton optDetallado 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Detallado"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   18
         Top             =   240
         Width           =   1155
      End
      Begin VB.OptionButton optResumido 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Resumido"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   19
         Top             =   615
         Width           =   1155
      End
   End
   Begin VB.Frame frameMesAno 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Mes / Ano de Aplicación"
      ForeColor       =   &H00808080&
      Height          =   615
      Left            =   3495
      TabIndex        =   41
      Top             =   495
      Width           =   2235
      Begin VB.TextBox txtMes 
         Height          =   285
         Left            =   120
         MaxLength       =   2
         TabIndex        =   14
         Top             =   240
         Width           =   495
      End
      Begin VB.TextBox txtAno 
         Height          =   285
         Left            =   675
         MaxLength       =   4
         TabIndex        =   13
         Top             =   240
         Width           =   735
      End
   End
   Begin VB.Frame frameCantidadAImprimir 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   3495
      TabIndex        =   44
      Top             =   1965
      Width           =   5655
      Begin VB.ComboBox CmbCantidadAImprimir 
         Height          =   315
         Left            =   1665
         TabIndex        =   20
         Top             =   0
         Width           =   1575
      End
      Begin VB.Label lblCantidadAimprimir 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Proveedores a Imprimir"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   0
         TabIndex        =   45
         Top             =   60
         Width           =   1605
      End
   End
   Begin VB.CheckBox chkCambiandodePagina 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Una página por proveedor"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   3480
      TabIndex        =   23
      Top             =   2640
      Width           =   2235
   End
   Begin VB.Frame frameMoneda 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Moneda"
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   3495
      TabIndex        =   37
      Top             =   2910
      Visible         =   0   'False
      Width           =   3015
      Begin VB.ComboBox cmbMonedaDeLosReportes 
         Height          =   315
         Left            =   645
         TabIndex        =   24
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
         Left            =   0
         TabIndex        =   39
         Top             =   60
         Width           =   585
      End
   End
   Begin VB.Frame frameTasaDeCambio 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Tasa de cambio"
      ForeColor       =   &H00808080&
      Height          =   915
      Left            =   6570
      TabIndex        =   36
      Top             =   2910
      Width           =   1515
      Begin VB.OptionButton optTasaDeCambio 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Del día"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   27
         Top             =   615
         Width           =   1215
      End
      Begin VB.OptionButton optTasaDeCambio 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Original"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   26
         Top             =   240
         Width           =   1215
      End
   End
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   3195
      TabIndex        =   32
      Top             =   5220
      Width           =   1335
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "&Pantalla"
      Height          =   375
      Left            =   1680
      TabIndex        =   31
      Top             =   5220
      Width           =   1335
   End
   Begin VB.CommandButton cmdImpresora 
      Caption         =   "&Impresora"
      Height          =   375
      Left            =   120
      TabIndex        =   30
      Top             =   5220
      Width           =   1335
   End
   Begin VB.Frame frameInformes 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   5055
      Left            =   120
      TabIndex        =   34
      Top             =   120
      Width           =   3255
      Begin VB.OptionButton optInformesDeCxP 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "CxP sin Retenciones I.S.L.R ..........."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   12
         Left            =   120
         TabIndex        =   58
         Top             =   2820
         Width           =   2955
      End
      Begin VB.OptionButton optInformesDeCxP 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Ret IVA en CxP ................................"
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   11
         Left            =   120
         TabIndex        =   6
         Top             =   2505
         Width           =   2955
      End
      Begin VB.OptionButton optInformesDeCxP 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Análisis de Vencimiento entre Fechas"
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   10
         Left            =   120
         TabIndex        =   2
         Top             =   1245
         Width           =   2955
      End
      Begin VB.OptionButton optInformesDeCxP 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Análisis de Vencimiento a una Fecha"
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   9
         Left            =   120
         TabIndex        =   4
         Top             =   1875
         Width           =   2955
      End
      Begin VB.OptionButton optInformesDeCxP 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Proveedores sin Movimientos ............"
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   8
         Left            =   120
         TabIndex        =   10
         Top             =   4455
         Width           =   2955
      End
      Begin VB.OptionButton optInformesDeCxP 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Listado de Proveedores ...................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   7
         Left            =   120
         TabIndex        =   9
         Top             =   4140
         Width           =   2955
      End
      Begin VB.OptionButton optInformesDeCxP 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Histórico de Proveedor ......................"
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   6
         Left            =   120
         TabIndex        =   8
         Top             =   3825
         Width           =   2955
      End
      Begin VB.OptionButton optInformesDeCxP 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "CxP Pendientes entre Fechas ........."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   5
         Left            =   120
         TabIndex        =   0
         Top             =   615
         Width           =   2955
      End
      Begin VB.OptionButton optInformesDeCxP 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Análisis de Vencimiento ....................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   1
         Top             =   930
         Width           =   2955
      End
      Begin VB.OptionButton optInformesDeCxP 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Libro de Compras .............................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   2
         Left            =   120
         TabIndex        =   5
         Top             =   2190
         Width           =   2955
      End
      Begin VB.OptionButton optInformesDeCxP 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Estado de Cuenta ............................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   3
         Left            =   120
         TabIndex        =   7
         Top             =   3510
         Width           =   2955
      End
      Begin VB.OptionButton optInformesDeCxP 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Análisis C x P Histórico ......................"
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   4
         Left            =   120
         TabIndex        =   3
         Top             =   1560
         Width           =   2955
      End
      Begin VB.Label lblTitulosInformes 
         BackColor       =   &H00A86602&
         Caption         =   " Informes de Proveedores"
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
         Index           =   1
         Left            =   120
         TabIndex        =   55
         Top             =   3135
         Width           =   2955
      End
      Begin VB.Label lblTitulosInformes 
         BackColor       =   &H00A86602&
         Caption         =   " Informes de C x P"
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
         Index           =   0
         Left            =   120
         TabIndex        =   54
         Top             =   240
         Width           =   2955
      End
   End
   Begin VB.Frame frameProveedor 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Proveedor"
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   3495
      TabIndex        =   40
      Top             =   2280
      Width           =   5655
      Begin VB.TextBox txtCodigo 
         Height          =   285
         Left            =   615
         TabIndex        =   21
         Top             =   60
         Width           =   1335
      End
      Begin VB.TextBox txtNombreDeProveedor 
         Height          =   285
         Left            =   2010
         TabIndex        =   22
         Top             =   60
         Width           =   3615
      End
      Begin VB.Label lblNombreDeProveedor 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Nombre"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   0
         TabIndex        =   42
         Top             =   60
         Width           =   555
      End
   End
   Begin VB.CommandButton cmdExportarAXLS 
      Caption         =   "&Exportar a XLS"
      Height          =   375
      Left            =   4740
      TabIndex        =   33
      Top             =   5220
      Width           =   1335
   End
   Begin VB.Frame frameAnalisisDeVencimiento 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Análisis de Vencimiento Ordenado por"
      ForeColor       =   &H00808080&
      Height          =   315
      Left            =   3480
      TabIndex        =   43
      Top             =   1440
      Width           =   5655
      Begin VB.ComboBox cmbAnalisisDeVencimientoPor 
         Height          =   315
         Left            =   2040
         TabIndex        =   17
         Top             =   0
         Width           =   1815
      End
      Begin VB.Label lblAnalisisDeVencimientoPor 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Análisis de Vencimiento por "
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   0
         TabIndex        =   46
         Top             =   60
         Width           =   1980
      End
   End
   Begin VB.Label lblDatosDelInforme 
      AutoSize        =   -1  'True
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      Caption         =   "lblDatosDelInforme"
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
      Left            =   3495
      TabIndex        =   38
      Top             =   120
      Width           =   2340
   End
End
Attribute VB_Name = "frmInformesDeCXP"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private gProyCompaniaActual As Object
Private insProveedorNav As Object
Private insRptInformesEspecialesConfigurar As Object
Private insAdmPropAnalisisVenc As Object
Private insConexionesSawAOS As Object
Private insCxPNav As Object
Private insAnticipoNav As Object
Private insFacturaNav As Object
Private insNoComunSawIva As Object
Private insRptFactura As Object
Private insComunSawIvaSqls As Object
Private insCxPSQL As clsCxpSQL

Private mInformeSeleccionado As Integer
Private mDondeImprimir As enum_DondeImprimir

Private Const GetGender As Integer = eg_Male
Private Const CM_FILE_NAME As String = "frmInformesDeCXP"
Private Const CM_MESSAGE_NAME As String = "Informes De CxP"
Private Const OPT_ANALISIS_DE_VENCIMIENTO As Integer = 0
Private Const OPT_LIBRO_DE_COMPRAS As Integer = 2
Private Const OPT_ESTADO_DE_CUENTA As Integer = 3
Private Const OPT_ANALISIS_CxP_HISTORICO As Integer = 4
Private Const OPT_CxP_PENDIENTE_ENTRE_FECHAS As Integer = 5
Private Const OPT_HISTORICO_PROVEEDOR As Integer = 6
Private Const OPT_LISTA_DE_PROVEEDORES As Integer = 7
Private Const OPT_PROVEEDORES_SIN_MOVIMIENTOS As Integer = 8
Private Const OPT_ANALISIS_DE_VENCIMIENTO_A_UNA_FECHA As Integer = 9
Private Const OPT_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHA As Integer = 10

Private Sub sCheckForSpecialKeys(ByVal valKeyCode As Integer, ByVal valShift As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, valKeyCode) Then
   End If
   Const ASC_CR = 13
   If (valKeyCode = vbKeyF6) Or (valKeyCode = ASC_CR) Then
      cmdGrabar_Click
   ElseIf (valKeyCode = vbKeyF8) Then
      cmdImpresora_Click
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCheckForSpecialKeys", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbCantidadAImprimir_Click()
   On Error GoTo h_ERROR
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      frameProveedor.Visible = True
      ChkCambiandodePagina.Visible = False
   Else
      frameProveedor.Visible = False
      ChkCambiandodePagina.Visible = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CmbCantidadAImprimir_Click()", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbCantidadAImprimir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CmbCantidadAImprimir_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbResumenDeLibros_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   gAPI.ValidateTextInComboBox cmbResumenDeLibros
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbResumenDeLibros_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdExportarAXLS_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_XLS
   sEjecutaElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdExportarAXLS_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_Click()
   On Error GoTo h_ERROR
   gAPI.ssSetFocus cmdGrabar
   mDondeImprimir = eDI_PANTALLA
   sEjecutaElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdGrabar_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImpresora_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdImpresora_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdSalir_Click()
   On Error GoTo h_ERROR
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdGrabar_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdSalir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImpresora_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_IMPRESORA
   sEjecutaElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdImpresora_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInitLookAndFeel()
   On Error GoTo h_ERROR
   Me.Caption = CM_MESSAGE_NAME
   gAPI.ssSetFocus optInformesDeCxP(OPT_CxP_PENDIENTE_ENTRE_FECHAS)
   mInformeSeleccionado = OPT_CxP_PENDIENTE_ENTRE_FECHAS
   optTasaDeCambio(0).Value = True
   sInitDefaultValues
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Load()
   On Error GoTo h_ERROR
   Me.ZOrder 0
   Me.Height = 6500
   Me.Width = 10900
   If gDefgen.getMainForm.Width > Width Then
      Left = (gDefgen.getMainForm.Width - Width) / 4
      Top = (gDefgen.getMainForm.Height - Height) / 4
   Else
      Left = 0
      Top = 0
   End If
   mInformeSeleccionado = OPT_CxP_PENDIENTE_ENTRE_FECHAS
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Load", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Unload(Cancel As Integer)
   Dim insFechaDeLosInformes As clsFechasDeLosInformesNav
   On Error GoTo h_ERROR
   Set insFechaDeLosInformes = New clsFechasDeLosInformesNav
   insFechaDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.Value, dtpFechaFinal.Value, gProyUsuarioActual.GetNombreDelUsuario
   Set insFechaDeLosInformes = Nothing

h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Unload", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optInformesDeCxP_Click(Index As Integer)
   On Error GoTo h_ERROR
   mInformeSeleccionado = Index
   sOcultarTodosLosCampos
   Select Case mInformeSeleccionado
      Case OPT_ANALISIS_DE_VENCIMIENTO: sActivarCamposDeAnalisisDeVencimiento
      Case OPT_LIBRO_DE_COMPRAS: sActivarCamposDeLibroDeCompras
      Case OPT_ESTADO_DE_CUENTA: sActivarCamposDeEstadoDeCuenta
      Case OPT_ANALISIS_CxP_HISTORICO: sActivarCamposDeAnalisisHistoricoCXP
      Case OPT_CxP_PENDIENTE_ENTRE_FECHAS: sActivarCamposDeCXPPendientesEntreFechas
      Case OPT_HISTORICO_PROVEEDOR: sActivarCamposDeHistoricoProveedor
      Case OPT_LISTA_DE_PROVEEDORES: sActivarCamposListaProveedores
      Case OPT_PROVEEDORES_SIN_MOVIMIENTOS: sActivarCamposDeProveedoresSinMovimientos
      Case OPT_ANALISIS_DE_VENCIMIENTO_A_UNA_FECHA: sActivarCamposDeAnalisisDeVencimientoAUnaFecha
      Case OPT_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHA: sActivarCamposDeAnalisisDeVencimientoEntreFechas
      Case OPT_RET_IVA_EN_CxP: sActivarCamposDeRetencionesIVAenCxP
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInformesDeCxP_Click", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optInformesDeCxP_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInformesDeCxP_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optTasaDeCambio_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optTasaDeCambio_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtpFechaInicial_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "dtpFechaInicial_KeyDown()", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtpFechaFinal_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "dtpFechaFinal_KeyDown()", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElInformeSeleccionado()
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case OPT_ANALISIS_DE_VENCIMIENTO: sEjecutaAnalisisDeVencimiento
      Case OPT_LIBRO_DE_COMPRAS: sEjecutaElLibroDeCompras
      Case OPT_ESTADO_DE_CUENTA: sEjecutaEstadoDeCuenta
      Case OPT_ANALISIS_CxP_HISTORICO: sEjecutaAnalisisCxPHistorico
      Case OPT_CxP_PENDIENTE_ENTRE_FECHAS: sEjecutaCXPPendientesEntreFechas 'dll sql y rpt
      Case OPT_HISTORICO_PROVEEDOR: sEjecutaHistoricoCXP
      Case OPT_LISTA_DE_PROVEEDORES: sEjecutaListaDeProveedores
      Case OPT_PROVEEDORES_SIN_MOVIMIENTOS: sEjecutaProveedoresSinMovimientos
      Case OPT_ANALISIS_DE_VENCIMIENTO_A_UNA_FECHA: sEjecutaAnalisisDeCxPAUnaFecha
      Case OPT_ANALISIS_DE_VENCIMIENTO_ENTRE_FECHA: sEjecutaAnalisisDeVencimientoEntreFechas
      Case OPT_RET_IVA_EN_CxP: sEjecutaRetencionesIVAenCxP
      Case OPT_CXP_SIN_RET: sEjecutaCxPSinRetencionISLR
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElInformeSeleccionado", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeAnalisisDeVencimiento()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Análisis de Vencimiento"
   frameInforme.Visible = True
   frameMoneda.Visible = True
   frameAnalisisDeVencimiento.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeAnalisisDeVencimiento", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaAnalisisDeVencimiento()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   Dim detallado As Boolean
   Dim Titulo As String
   Dim monedaOrig As Boolean
   Dim valPropAnalisisVencActual As Boolean
   Dim ReporteOrdenadoPorNombre As Boolean
   Dim NombreCompaniaParaInformes As String
   Dim MensajesDeMonedaParaInformes As String
   On Error GoTo h_ERROR
   Set reporte = New DDActiveReports2.ActiveReport
   detallado = optDetallado.Value
   Titulo = "Análisis de Vencimiento de Cuentas por Pagar - "
   If detallado Then
      Titulo = Titulo & "Detallado"
   Else
      Titulo = Titulo & "Resumido"
   End If
   NombreCompaniaParaInformes = gProyCompaniaActual.GetNombreCompaniaParaInformes(False)
   ReporteOrdenadoPorNombre = (cmbAnalisisDeVencimientoPor.Text = gEnumReport.enumReporteOrdenadoPorToString(eRO_Nombre))
   valPropAnalisisVencActual = insAdmPropAnalisisVenc.fBuscaValoresDeLasPropAnalisisVencActual
   monedaOrig = Not (cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal))
   Set insCxPSQL = New clsCxpSQL
   SqlDelReporte = insCxPSQL.fConstruirSQLDelReporteAnalisisDeVencimiento(monedaOrig, valPropAnalisisVencActual, gUltimaTasaDeCambio, gMonedaLocalActual, gProyParametros.GetNombreMonedaLocal, insAdmPropAnalisisVenc.GetPrimerVencimiento, insAdmPropAnalisisVenc.GetSegundoVencimiento, insAdmPropAnalisisVenc.GetTercerVencimiento, gProyCompaniaActual.GetConsecutivoCompania, ReporteOrdenadoPorNombre)
   If optTasaDeCambio(0).Value Then
      MensajesDeMonedaParaInformes = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
   ElseIf optTasaDeCambio(1).Value Then
      MensajesDeMonedaParaInformes = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia)
   End If
   If insRptInformesEspecialesConfigurar.fConfiguraLasSeccionesDelReporteDeAnalisisDeVencimiento(reporte, SqlDelReporte, detallado, True, monedaOrig, "", valPropAnalisisVencActual, NombreCompaniaParaInformes, insAdmPropAnalisisVenc.GetPrimerVencimiento, insAdmPropAnalisisVenc.GetSegundoVencimiento, insAdmPropAnalisisVenc.GetTercerVencimiento, MensajesDeMonedaParaInformes) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, Titulo
   End If
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaAnalisisDeVencimiento", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitDefaultValues()
   Dim insFechaDeLosInformes As clsFechasDeLosInformesNav
   On Error GoTo h_ERROR
   Set insFechaDeLosInformes = New clsFechasDeLosInformesNav
   insFechaDeLosInformes.sLeeLasFechasDeInformes dtpFechaInicial, dtpFechaFinal, gProyUsuarioActual.GetNombreDelUsuario
   Set insFechaDeLosInformes = Nothing
   gEnumReport.FillComboBoxWithReporteOrdenadoPor cmbAnalisisDeVencimientoPor
   cmbAnalisisDeVencimientoPor.ListIndex = 0
   txtCodigo.Visible = False
   gEnumProyecto.FillComboBoxWithStatusDocumento cmbStatus, Buscar
   cmbStatus.Text = gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_PORCANCELAR)
   cmbStatus.ListIndex = 0
   gEnumReport.FillComboBoxWithCantidadAImprimir CmbCantidadAImprimir
   CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(enum_CantidadAImprimir.eCI_uno)
   CmbCantidadAImprimir.ListIndex = 0
   gEnumReport.FillComboBoxWithMonedaDeLosReportes cmbMonedaDeLosReportes, gProyParametros.GetNombreMonedaLocal
   cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal)
   cmbMonedaDeLosReportes.ListIndex = eMR_EnMonedaOriginal
   optDetallado.Value = True
   If Not gProyParametros.GetUsarCodigoProveedorEnPantalla Then
      cmbAnalisisDeVencimientoPor.ListIndex = eRO_Nombre
   End If
   If gProyParametros.GetEsSistemaParaIG Then
      optInformesDeCxP(OPT_PROVEEDORES_SIN_MOVIMIENTOS).Visible = False
      optInformesDeCxP(OPT_PROVEEDORES_SIN_MOVIMIENTOS).Enabled = False
   Else
      optInformesDeCxP(OPT_PROVEEDORES_SIN_MOVIMIENTOS).Visible = True
      optInformesDeCxP(OPT_PROVEEDORES_SIN_MOVIMIENTOS).Enabled = True
   End If
   If gProyParametros.GetUsarCodigoProveedorEnPantalla Then
      txtCodigo.Visible = True
   Else
      txtCodigo.Visible = False
      txtNombreDeProveedor.Left = txtCodigo.Left
   End If
   If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial Then
      optInformesDeCxP(OPT_RET_IVA_EN_CxP).Enabled = True
   Else
      optInformesDeCxP(OPT_RET_IVA_EN_CxP).Enabled = False
   End If
   gEnumProyecto.FillComboBoxWithFormaDeEscogerCompania cmbOrdenadoPor
   cmbOrdenadoPor.Text = gEnumProyecto.enumFormaDeEscogerCompaniaToString(enum_FormaDeEscogerCompania.eFD_PORNOMBRE)
   cmbOrdenadoPor.ListIndex = 0
   gEnumProyecto.FillComboBoxWithResumenDeLibrosParaCompras cmbResumenDeLibros
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitDefaultValues", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optDetallado_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optDetallado_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optDetallado_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optDetallado_KeyPress()", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optResumido_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optResumido_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optResumido_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optResumido_KeyPress()", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbAnalisisDeVencimientoPor_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbAnalisisDeVencimientoPor_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbAnalisisDeVencimientoPor_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbAnalisisDeVencimientoPor_KeyPress()", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeLibroDeCompras()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Libro de Compras"
   frameMesAno.Visible = True
   txtMes.Text = gConvert.fConvierteAString(Month(gDefgen.fGetValorDeFechaParaInicializarCampo))
   txtAno.Text = gConvert.fConvierteAString(Year(gDefgen.fGetValorDeFechaParaInicializarCampo))
   cmdExportarAXLS.Visible = True
   If Not gProyCompaniaActual.fElTipoDeContribuyenteEsFormal Then
      chkMostrarInformeDeProrrateo.Visible = True
      pnlEmitirResumenParaVentas.Visible = True
      ChkSepararRetenciones.Visible = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " sActivarCamposDeLibroDeCompras", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAno_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtAno
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAno_KeyDown(KeyCode As Integer, Shift As Integer)
  On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAno_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.EsUnCaracterAsciiValidoParaCampoTipoIntegerSoloPositivo(KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_KeyPress", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMes_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If LenB(txtMes) = 0 Then
      sShowMessageForRequiredFields "Mes", txtMes
      Cancel = True
   ElseIf txtMes < 1 Or txtMes > 12 Then
      gMessage.Advertencia "Debe introducir un numero de 'Mes' válido"
      Cancel = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAno_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If LenB(txtAno) = 0 Then
      sShowMessageForRequiredFields "Año", txtAno
      Cancel = True
   ElseIf gTexto.DfLen(txtAno.Text) < 4 Then
      gMessage.Advertencia "Debe introducir un 'Año' de cuatro dígitos'"
      Cancel = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMes_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.EsUnCaracterAsciiValidoParaCampoTipoIntegerSoloPositivo(KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_KeyPress", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sShowMessageForRequiredFields(ByVal valCampo As String, ByRef refCampo As TextBox)
   On Error GoTo h_ERROR
   gMessage.ShowRequiredFields valCampo
   gAPI.ssSetFocus refCampo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sShowMessageForRequiredFields", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMes_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtMes
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMes_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElLibroDeCompras()
   On Error GoTo h_ERROR
   If Trim(txtMes.Text) = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
   ElseIf Trim(txtAno.Text) = "" Then
      sShowMessageForRequiredFields "Año", txtAno
   ElseIf mDondeImprimir = eDI_XLS Then
      sEjecutaLibroDeComprasEnXLS
   Else
      sEjecutaLibroDeCompras
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElLibroDeCompras", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeProveedor_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtNombreDeProveedor
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeProveedor_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeProveedor_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeProveedor_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeProveedor_Validate(Cancel As Boolean)
   Dim refCodigoProveedor As String
   Dim refNombreProveedor As String
   On Error GoTo h_ERROR
   If LenB(txtNombreDeProveedor.Text) = 0 Then
      txtNombreDeProveedor.Text = "*"
   End If
   If insConexionesSawAOS.fSelectAndSetValuesOfProveedorFromAOS(insProveedorNav, refCodigoProveedor, refNombreProveedor, txtNombreDeProveedor.Text, "NombreProveedor") Then
      sAssignFieldsFromConnectionProveedor
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " txtNombreDeProveedor_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigo_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtNombreDeProveedor
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtcodigo_GotFocus", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigo_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtcodigo_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigo_Validate(Cancel As Boolean)
   Dim refCodigoProveedor As String
   Dim refNombreProveedor As String
   On Error GoTo h_ERROR
   If LenB(txtCodigo) = 0 Then
      txtCodigo = "*"
   Else
      txtCodigo.Text = insProveedorNav.fFillWithCerosOnLeft(txtCodigo.Text)
   End If
   insProveedorNav.SetCodigoProveedor txtCodigo
   If insConexionesSawAOS.fSelectAndSetValuesOfProveedorFromAOS(insProveedorNav, refCodigoProveedor, refNombreProveedor, txtCodigo.Text, "") Then
      sAssignFieldsFromConnectionProveedor
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Cancel = True
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigo_Validate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAssignFieldsFromConnectionProveedor()
   On Error GoTo h_ERROR
   txtNombreDeProveedor.Text = insProveedorNav.GetNombreProveedor
   txtCodigo.Text = insProveedorNav.GetCodigoProveedor
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionProveedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeEstadoDeCuenta()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Estado de Cuenta Proveedor"
   frameTasaDeCambio.Visible = True
   frameMoneda.Visible = True
   frameCantidadAImprimir.Visible = True
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      frameProveedor.Visible = True
      ChkCambiandodePagina.Visible = False
   Else
      frameProveedor.Visible = False
      ChkCambiandodePagina.Visible = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeEstadoDeCuenta", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
   
Private Sub sEjecutaEstadoDeCuenta()
   Dim SqlDelReporte As String
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCxPRpt
   Dim usarCambioOriginal As Boolean
   Dim usarMonedaOriginal As Boolean
   Dim unaPaginaPorProveedor As Boolean
   Dim sqlTipoDeCxP As String
   Dim ReporteMonedaLocal As Boolean
   Dim ImprimirProveedorUnico As Boolean
   On Error GoTo h_ERROR
   If txtNombreDeProveedor.Text = "" And CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      sShowMessageForRequiredFields "Nombre del Proveedor", txtNombreDeProveedor
   Else
      Set reporte = New DDActiveReports2.ActiveReport
      Set insConfigurar = New clsCxPRpt

      sqlTipoDeCxP = gUtilSQL.DfSQLCaseIfForEnum("CxP.TipoDeCxP", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), "")
      usarMonedaOriginal = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal))
      If usarMonedaOriginal Then
         ReporteMonedaLocal = False
      Else
         ReporteMonedaLocal = True
      End If
      If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
         ImprimirProveedorUnico = True
      Else
         ImprimirProveedorUnico = False
      End If
      SqlDelReporte = insCxPSQL.fSQLEstadoDeCuenta(sqlTipoDeCxP, ReporteMonedaLocal, optTasaDeCambio(0).Value, gProyCompaniaActual.GetConsecutivoCompania, ImprimirProveedorUnico, txtCodigo.Text, gMonedaLocalActual, gUltimaTasaDeCambio)
      usarCambioOriginal = False
      unaPaginaPorProveedor = False
      If gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal) Then
         usarCambioOriginal = optTasaDeCambio(0).Value
      End If
      If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
         unaPaginaPorProveedor = gAPI.fGetCheckBoxValue(ChkCambiandodePagina)
      End If
         usarMonedaOriginal = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal))
         If gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal) Then
            usarCambioOriginal = optTasaDeCambio(0).Value
         End If
      If insConfigurar.fConfigurarReporteEstadoDeCuenta(reporte, SqlDelReporte, usarMonedaOriginal, usarCambioOriginal, unaPaginaPorProveedor, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal), gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia), gGlobalization) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Estado de Cuenta de Cliente"
      End If
      Set reporte = Nothing
      Set insConfigurar = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaEstadoDeCuenta", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeAnalisisHistoricoCXP()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Análisis Histórico CxP"
   frameFechas.Visible = True
   frameMoneda.Visible = True
   frameCantidadAImprimir.Visible = True
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      frameProveedor.Visible = True
      ChkCambiandodePagina.Visible = False
   Else
      frameProveedor.Visible = False
      ChkCambiandodePagina.Visible = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeAnalisisHistoricoCXP", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaAnalisisCxPHistorico()
   Dim SqlDelReporte As String
   Dim usarCambioOriginal As Boolean
   Dim insConfigurar As clsCxPRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim unaPaginaPorProveedor As Boolean
   Dim valImprimeUno As Boolean
   Dim ReporteEnMonedaLocal As Boolean
   Dim valTipoDeAnticipo As String
   On Error GoTo h_ERROR
   Set insConfigurar = New clsCxPRpt
   Set reporte = New DDActiveReports2.ActiveReport
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
      dtpFechaFinal.Value = dtpFechaInicial.Value
   End If
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(enum_CantidadAImprimir.eCI_uno) And txtNombreDeProveedor.Text = "" Then
      sShowMessageForRequiredFields "Nombre Proveedor", txtNombreDeProveedor
      GoTo h_EXIT
   End If
   
   unaPaginaPorProveedor = False
   If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
      unaPaginaPorProveedor = gConvert.ConvertByteToBoolean(ChkCambiandodePagina.Value)
   End If
   usarCambioOriginal = optTasaDeCambio(0).Value
   ReporteEnMonedaLocal = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal))
   valImprimeUno = (gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno))
   valTipoDeAnticipo = gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeAnticipoToString(eTDA_PAGADO))
   SqlDelReporte = insCxPSQL.fSQLAnalisisCxPHistorico(usarCambioOriginal, gUltimaTasaDeCambio, gMonedaLocalActual, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetConsecutivoCompania, ReporteEnMonedaLocal, valImprimeUno, gUtilSQL.fSimpleSqlValue(txtCodigo.Text), valTipoDeAnticipo, insCxPNav.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_SQL(True), insAnticipoNav.getFN_NO_DOC_ORIGEN_CONTABILIZACION_PARA_SQL(True))
   If insConfigurar.fConfigurarDatosDelReporteAnalisisDeCxPHistorico(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, usarCambioOriginal, unaPaginaPorProveedor, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal), gGlobalization) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Análisis de Cuentas por Pagar Histórico"
   End If
   Set reporte = Nothing
   Set insConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaAnalisisCxPHistorico", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaCXPPendientesEntreFechas()
   Dim insConfigurar As clsCxPRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   Dim usarCambioOriginal As Boolean
   Dim monedaOriginal As Boolean
   Dim MensajesDeMonedaParaInformes As String
    Dim valImprimeUno As Boolean
   On Error GoTo h_ERROR
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
      dtpFechaFinal.Value = dtpFechaInicial.Value
   End If
   Set insConfigurar = New clsCxPRpt
   Set reporte = New DDActiveReports2.ActiveReport
   
   usarCambioOriginal = optTasaDeCambio(0).Value
   monedaOriginal = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal))
   valImprimeUno = (gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno))
   SqlDelReporte = insCxPSQL.fSQLCxPEntreFechasCxPPendientesEntreFechas(monedaOriginal, usarCambioOriginal, gAPI.SelectedElementInComboBoxToString(cmbStatus), dtpFechaInicial.Value, dtpFechaFinal.Value, mInformeSeleccionado, gProyCompaniaActual.GetConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio, valImprimeUno, gUtilSQL.fSimpleSqlValue(txtCodigo.Text))
   If optTasaDeCambio(0).Value Then
      MensajesDeMonedaParaInformes = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
   ElseIf optTasaDeCambio(1).Value Then
      MensajesDeMonedaParaInformes = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia)
   End If
   If insConfigurar.fConfigurarDatosDelReporteCxPEntreFechasCxPPendientesEntreFechas(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, monedaOriginal, usarCambioOriginal, True, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), MensajesDeMonedaParaInformes) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Cuentas por Pagar entre Fechas"
   End If
   Set insConfigurar = Nothing
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCXPPendientesEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCXPPendientesEntreFechas()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - CXP Pendientes entre Fechas"
   frameFechas.Visible = True
   frameTasaDeCambio.Visible = True
   frameMoneda.Visible = True
   frameStatus.Visible = True
   frameCantidadAImprimir.Visible = True
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      frameProveedor.Visible = True
      ChkCambiandodePagina.Visible = False
   Else
      frameProveedor.Visible = False
      ChkCambiandodePagina.Visible = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeCXPPendientesEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeHistoricoProveedor()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Histórico por Proveedor"
   frameFechas.Visible = True
   frameMoneda.Visible = True
   frameCantidadAImprimir.Visible = True
   If CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      frameProveedor.Visible = True
      ChkCambiandodePagina.Visible = False
   Else
      frameProveedor.Visible = False
      ChkCambiandodePagina.Visible = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeHistoricoProveedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaHistoricoCXP()
   Dim SqlDelReporte As String
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCxPRpt
   Dim usarCambioBs As Boolean
   Dim codigoProveedor As String
   Dim unaPaginaPorProveedor As Boolean
   On Error GoTo h_ERROR
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
      dtpFechaFinal.Value = dtpFechaInicial.Value
   End If
   If txtNombreDeProveedor.Text = "" And CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      sShowMessageForRequiredFields "Nombre del Proveedor", txtNombreDeProveedor
    Else
      Set reporte = New DDActiveReports2.ActiveReport
      Set insConfigurar = New clsCxPRpt
      If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
         unaPaginaPorProveedor = gAPI.fGetCheckBoxValue(ChkCambiandodePagina)
      End If
      usarCambioBs = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal))
      If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
         codigoProveedor = Trim(txtNombreDeProveedor.Text)
      End If
      SqlDelReporte = insCxPSQL.fSQLHistoricoDeProveedor(usarCambioBs, gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir), txtCodigo.Text, gUltimaTasaDeCambio, gMonedaLocalActual)
      If insConfigurar.fConfigurarDatosDelReporteHistoricoProveedor(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, usarCambioBs, unaPaginaPorProveedor, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia), gGlobalization) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Histórico de Proveedor"
      End If
      Set reporte = Nothing
      Set insConfigurar = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaHistoricoCXP", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbMonedaDeLosReportes_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbMonedaDeLosReportes_KeyDown", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultarTodosLosCampos()
   On Error GoTo h_ERROR
   dtpFechaInicial.Visible = True
   lblFechaInicial.Visible = True
   frameAnalisisDeVencimiento.Visible = False
   frameFechas.Visible = False
   frameMesAno.Visible = False
   frameProveedor.Visible = False
   frameStatus.Visible = False
   frameCantidadAImprimir.Visible = False
   frameTasaDeCambio.Visible = False
   frameMoneda.Visible = False
   frameInforme.Visible = False
   frameOrdenadoPor.Visible = False
   ChkCambiandodePagina.Visible = False
   cmdExportarAXLS.Visible = False
   chkMostrarInformeDeProrrateo.Visible = False
   pnlEmitirResumenParaVentas.Visible = False
   chkTotalizarPorMes.Visible = False
   ChkSepararRetenciones.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOcultarTodosLosCampos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
   
Private Sub sActivarCamposListaProveedores()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Listado de Proveedores"
   frameOrdenadoPor.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposListaProveedores", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
      
Private Sub sEjecutaListaDeProveedores()
   Dim insConfigurarInformeCompraVenta As clsCxPRpt
   Dim rptMostrarReporte As DDActiveReports2.ActiveReport
   Dim mensajeViewer As String
   Dim sql As String
   On Error GoTo h_ERROR
   Set rptMostrarReporte = New DDActiveReports2.ActiveReport
   Set insConfigurarInformeCompraVenta = New clsCxPRpt
   sql = insCxPSQL.fConstruirSqlDeListarProveedores(gProyParametros.GetUsarCodigoProveedorEnPantalla, cmbOrdenadoPor.Text, gEnumProyecto.enumFormaDeEscogerCompaniaToString(enum_FormaDeEscogerCompania.eFD_PORCODIGO), gProyCompaniaActual.GetConsecutivoCompania)
   mensajeViewer = "Lista de Proveedores"
   If insConfigurarInformeCompraVenta.fConfiguraLasSeccionesDeListaDeProveedor(rptMostrarReporte, sql, gProyParametrosCompania.GetUsaRetencion, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), gGlobalization) Then
      gUtilReports.sMostrarOImprimirReporte rptMostrarReporte, 1, mDondeImprimir, mensajeViewer
      Set rptMostrarReporte = Nothing
      Set insConfigurarInformeCompraVenta = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaClientesPorZona", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
  
Private Sub sActivarCamposDeProveedoresSinMovimientos()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Proveedores Sin Movimientos"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeProveedoresSinMovimientos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaProveedoresSinMovimientos()
   Dim varSQLDelReporte As String
   Dim varReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsClienteRpt
   On Error GoTo h_ERROR
   varSQLDelReporte = fSqlListadoDeProveedoresSinMovimientos
   Set varReporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsClienteRpt
   If insConfigurar.fConfigurarDatosDelReporteListadoDeClientesProvSinMov(varSQLDelReporte, eSM_Proveedor, varReporte, "CodigoProveedor", "NombreProveedor", "", "ContactoProveedor", "FaxProveedor", "TelefonoProveedor", gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), gDefDatabaseConexion) Then
      gUtilReports.sMostrarOImprimirReporte varReporte, 1, mDondeImprimir, "Proveedores Sin Movimientos"
   End If
   Set varReporte = Nothing
   Set insConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaProveedoresSinMovimientos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSQLFNCountProveedoresInSystemModules() As String
   Dim varSQL As String
   Dim sqlCountCompra As String
   Dim sqlCountCxP As String
   Dim sqlCountPago  As String
   Dim sqlCountARCV As String
   Dim sqlCountAnticipo As String
   On Error GoTo h_ERROR
   sqlCountCompra = "(SELECT " & gUtilSQL.fCOUNT("compra.CodigoProveedor", "") & " FROM compra WHERE compra.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania & " AND compra.CodigoProveedor =  Adm.Proveedor.CodigoProveedor) > 0"
   sqlCountCxP = "(SELECT " & gUtilSQL.fCOUNT("cxP.CodigoProveedor", "") & " FROM cxP WHERE cxP.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania & " AND cxP.CodigoProveedor =  Adm.Proveedor.CodigoProveedor) > 0"
   sqlCountPago = "(SELECT " & gUtilSQL.fCOUNT("pago.CodigoProveedor", "") & " FROM pago WHERE pago.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania & " AND pago.CodigoProveedor =  Adm.Proveedor.CodigoProveedor) > 0"
   sqlCountARCV = "(SELECT" & gUtilSQL.fCOUNT("ARCV.CodigoProveedor", "") & " FROM ARCV WHERE ARCV.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania & " AND ARCV.CodigoProveedor = Adm.Proveedor.CodigoProveedor) > 0"
   sqlCountAnticipo = "(SELECT" & gUtilSQL.fCOUNT("anticipo.CodigoProveedor", "") & " FROM anticipo WHERE anticipo.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania & " AND anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO) & " AND anticipo.CodigoProveedor = Adm.Proveedor.CodigoProveedor) > 0"
   If Not gDefProg.fEsSistemaAdmInfotax Then
      varSQL = varSQL & gUtilSQL.getIIF(sqlCountCompra, "'1'", "'0'", True) & " AS T1, "
   End If
   varSQL = varSQL & gUtilSQL.getIIF(sqlCountCxP, "'1'", "'0'", True) & " AS T2, "
   varSQL = varSQL & gUtilSQL.getIIF(sqlCountPago, "'1'", "'0'", True) & " AS T3, "
   varSQL = varSQL & gUtilSQL.getIIF(sqlCountARCV, "'1'", "'0'", True) & " AS T4, "
   varSQL = varSQL & gUtilSQL.getIIF(sqlCountAnticipo, "'1'", "'0'", True) & " AS T5, "
   fSQLFNCountProveedoresInSystemModules = varSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFNCountProveedoresInSystemModules", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlListadoDeProveedoresSinMovimientos() As String
   Dim sql As String
   Dim varCamposDeControl As String
   On Error GoTo h_ERROR
   varCamposDeControl = fSQLFNCountProveedoresInSystemModules
   sql = "SELECT "
   If varCamposDeControl <> "" Then
      sql = sql & varCamposDeControl
   End If
   sql = sql & "CodigoProveedor AS CodigoProveedor, "
   sql = sql & "NombreProveedor AS NombreProveedor, "
   sql = sql & "Telefonos AS TelefonoProveedor, "
   sql = sql & "Fax AS FaxProveedor, "
   sql = sql & "Contacto AS ContactoProveedor "
   sql = sql & " FROM Adm.Proveedor"
   sql = sql & " WHERE Adm.Proveedor.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   fSqlListadoDeProveedoresSinMovimientos = sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlListadoDeProveedoresSinMovimientos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sEjecutaLibroDeCompras()
   Dim SqlDelReporte As String
   Dim reporteLibroDeCompras As DDActiveReports2.ActiveReport
   Dim porcentajeDeProrrateo As Currency
   Dim mMostrarInformedeVentas As Boolean
   Dim insCxPDsr As clsCxPDsr
   Dim insLibrosVentasYComprasSQL As clsLibrosVentasYComprasSQL
   On Error GoTo h_ERROR
   Set insLibrosVentasYComprasSQL = New clsLibrosVentasYComprasSQL
   Set insCxPDsr = New clsCxPDsr
   mMostrarInformedeVentas = False
   If cmbResumenDeLibros.Text = gEnumProyecto.enumResumenDeLibrosToString(eRDL_VENTAS_Y_COMPRAS) Then
      mMostrarInformedeVentas = True
   End If
   insFacturaNav.setClaseDeTrabajo eCTFC_Factura
   SqlDelReporte = insLibrosVentasYComprasSQL.fSQLLibroDeComprasNAlicuotas(gProyCompaniaActual.GetConsecutivoCompania, txtMes.Text, txtAno.Text, gMonedaLocalActual, dtpFechaInicial.Value, dtpFechaFinal.Value)
   porcentajeDeProrrateo = insFacturaNav.fPorcentajeProrrateo(dtpFechaInicial.Value, dtpFechaFinal.Value, txtMes.Text, txtAno.Text)
   Set reporteLibroDeCompras = insCxPDsr.fConfigurarDsrLibroDeComprasNAlicuotas(SqlDelReporte, txtMes.Text, txtAno.Text, porcentajeDeProrrateo, gConvert.ConvertByteToBoolean(chkMostrarInformeDeProrrateo.Value), mMostrarInformedeVentas, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), insFacturaNav, gProyCompaniaActual, gEnumProyecto, insNoComunSawIva, insRptFactura, gGlobalization, gAdmAlicuotaIvaActual, dtpFechaInicial.Value, dtpFechaFinal.Value)
   gUtilReports.sMostrarOImprimirReporte reporteLibroDeCompras, 1, mDondeImprimir, ""
   Set reporteLibroDeCompras = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaLibroDeCompras", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaLibroDeComprasEnXLS()
   Dim nombreXLS As String
   Dim SeCreoElArchivo As Boolean
   Dim AlicSeparadas As Boolean
   Dim varRepetirTitulos As Boolean
   Dim varSepararRetenciones As Boolean
   Dim vScale As Integer
   Dim mAplicaContribuyenteEspecial As Boolean
   Dim vFechaDateHasta As Date
   Dim vFechaDateDesde As Date
   On Error GoTo h_ERROR
   nombreXLS = fNombreDelArchivo("Libro de Compras XLS")
   vScale = 40
   If txtMes.Text = "" Or txtAno.Text = "" Then
      sShowMessageForRequiredFields "Mes", txtMes
      GoTo h_EXIT
   Else
      vFechaDateDesde = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, True)
      vFechaDateHasta = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, False)
      AlicSeparadas = True
      varRepetirTitulos = False 'chkRepetirTitulos.value = Checked
      varSepararRetenciones = ChkSepararRetenciones.Value = Checked
      mAplicaContribuyenteEspecial = (gProyCompaniaActual.GetTipoDeContribuyenteIVAStr = gEnumProyecto.enumTipoDeContribuyenteIvaToString(eTD_ESPECIAL))
      SeCreoElArchivo = fCreaElArchivoDeExportacion(nombreXLS)
      insComunSawIvaSqls.sEjecutaLibroDeComprasEnXLS txtMes.Text, txtAno.Text, txtMes.Text, txtAno, nombreXLS, nombreXLS, Me.hWnd, SeCreoElArchivo, varSepararRetenciones, True, vFechaDateDesde, vFechaDateHasta, mAplicaContribuyenteEspecial, False, vScale
  End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR:
   If Err.Number = 5018 Then
      gMessage.Advertencia "El archivo " & nombreXLS & "  se encuentra actualmente abierto, cierrelo y exportelo nuevamente."
      Err.Clear
   Else
      Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaLibroDeComprasEnXLS", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
   End If
End Sub

Private Sub sActivarCamposDeAnalisisDeVencimientoAUnaFecha()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Análisis de Vencimiento a una Fecha"
   frameInforme.Visible = True
   frameAnalisisDeVencimiento.Visible = True
   frameFechas.Visible = True
   frameInforme.Visible = True
   lblFechaInicial.Visible = False
   dtpFechaInicial.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeAnalisisDeVencimientoAUnaFecha", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaAnalisisDeCxPAUnaFecha()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim detallado As Boolean
   Dim Titulo As String
   Dim tasaOrig As Boolean
   Dim monedaOrig As Boolean
   Dim ordenadoPorNombre As Boolean
   On Error GoTo h_ERROR
   Set reporte = New DDActiveReports2.ActiveReport
   detallado = optDetallado.Value
   Titulo = "Análisis de Vencimiento de Cuentas por Pagar a una Fecha - "
   If detallado Then
      Titulo = Titulo & "Detallado"
   Else
      Titulo = Titulo & "Resumido"
   End If
   Titulo = Titulo & " (" & gConvert.dateToStringYY(dtpFechaFinal.Value) & ")"
   tasaOrig = optTasaDeCambio(0).Value
   monedaOrig = Not (cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal))
   ordenadoPorNombre = (cmbAnalisisDeVencimientoPor.Text = gEnumReport.enumReporteOrdenadoPorToString(eRO_Nombre))
   If insRptInformesEspecialesConfigurar.fConfiguraAnalisisDeCxPAUnaFecha(reporte, dtpFechaFinal.Value, detallado, tasaOrig, monedaOrig, ordenadoPorNombre) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, Titulo
   End If
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaAnalisisDeCxPAUnaFecha", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeAnalisisDeVencimientoEntreFechas()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Análisis de Vencimiento entre Fechas"
   frameFechas.Visible = True
   chkTotalizarPorMes.Visible = True
   chkTotalizarPorMes.Value = vbChecked
   frameAnalisisDeVencimiento.Visible = True
   frameMoneda.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeAnalisisDeVencimientoEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaAnalisisDeVencimientoEntreFechas()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsCxPRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim monedaOriginal As Boolean
   Dim totalizarPorMes As Boolean
   On Error GoTo h_ERROR
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
      dtpFechaFinal.Value = dtpFechaInicial.Value
   End If
   Set insConfigurar = New clsCxPRpt
   Set reporte = New DDActiveReports2.ActiveReport
   monedaOriginal = Not (cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal))
   SqlDelReporte = insCxPSQL.fSQLDelReporteAnalisisDeVencimientoEntreFechas(cmbAnalisisDeVencimientoPor.Text, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetConsecutivoCompania, gProyParametros.GetNombreMonedaLocal, monedaOriginal, gMonedaLocalActual, insAdmPropAnalisisVenc, gUltimaTasaDeCambio)
   totalizarPorMes = (chkTotalizarPorMes.Value = vbChecked)
   If insConfigurar.fConfigurarDatosDelReporteAnalisisDeVencimientoEntreFechas(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, monedaOriginal, totalizarPorMes, insAdmPropAnalisisVenc.fBuscaValoresDeLasPropAnalisisVencActual, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal), insAdmPropAnalisisVenc.GetPrimerVencimiento, insAdmPropAnalisisVenc.GetSegundoVencimiento, insAdmPropAnalisisVenc.GetTercerVencimiento) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Análisis de Vencimiento entre Fechas"
   End If
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaAnalisisDeVencimientoEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Function fCreaElArchivoDeExportacion(ByVal valNombreDeArchivo As String) As Boolean
   On Error GoTo h_ERROR
   If gUtilFile.fExisteElFolder(valNombreDeArchivo) Then
      If gUtilFile.fExisteElArchivo(valNombreDeArchivo) Then
         gUtilFile.fBorraElArchivoSinVerificarSuExistencia valNombreDeArchivo
      End If
      fCreaElArchivoDeExportacion = gUtilFile.fCreaArchivoDeTexto(valNombreDeArchivo)
   Else
      If gUtilFile.fDfMkPath(gUtilFile.getPathName(valNombreDeArchivo), False) Then
         fCreaElArchivoDeExportacion = gUtilFile.fCreaArchivoDeTexto(valNombreDeArchivo)
      Else
         fCreaElArchivoDeExportacion = False
      End If
   End If
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSeCreaElArchivoDeExportacion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelArchivo(ByVal valLibro As String) As String
   Dim varNombreArchivoXLS As String
   Dim NombreArchivo As String
   On Error GoTo h_ERROR
   varNombreArchivoXLS = gTexto.QuitaCaracteresInvalidosParaDirectorio(gTexto.DfReplace(valLibro, ".", ""))
   NombreArchivo = gWorkPaths.GetDatabaseDir & "ArchivosXls\" & gUtilFile.fCreaElNombreDeArchivoDeExportacion(gProyCompaniaActual.GetNombre, True, varNombreArchivoXLS, ".xls")
   fNombreDelArchivo = NombreArchivo
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSeCreaElArchivoDeExportacion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function OPT_RET_IVA_EN_CxP() As Integer
   OPT_RET_IVA_EN_CxP = 11
End Function
Private Function OPT_CXP_SIN_RET() As Integer
   OPT_CXP_SIN_RET = 12
End Function

Private Sub sActivarCamposDeRetencionesIVAenCxP()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Retenciones de IVA en CxP"
   frameFechas.Visible = True
   frameTasaDeCambio.Visible = True
   frameMoneda.Visible = True
   frameStatus.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "ActivarCamposDeCXPEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaRetencionesIVAenCxP()
   Dim insConfigurar As clsCxPRpt
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   Dim usarCambioOriginal As Boolean
   Dim monedaOriginal As Boolean
   Dim MensajesDeMonedaParaInformes As String
   On Error GoTo h_ERROR
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
      dtpFechaFinal.Value = dtpFechaInicial.Value
   End If
   Set insConfigurar = New clsCxPRpt
   Set reporte = New DDActiveReports2.ActiveReport
   usarCambioOriginal = optTasaDeCambio(0).Value
   monedaOriginal = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal))
   SqlDelReporte = insCxPSQL.fSQLRetIVAenCxP(monedaOriginal, usarCambioOriginal, gAPI.SelectedElementInComboBoxToString(cmbStatus), dtpFechaInicial.Value, dtpFechaFinal.Value, mInformeSeleccionado, gProyCompaniaActual.GetConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   Debug.Print SqlDelReporte
   If optTasaDeCambio(0).Value Then
      MensajesDeMonedaParaInformes = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
   ElseIf optTasaDeCambio(1).Value Then
      MensajesDeMonedaParaInformes = gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia)
   End If
   If insConfigurar.fConfigurarDatosDelReporteRetencionesDeIvaEnCxP(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, monedaOriginal, usarCambioOriginal, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), MensajesDeMonedaParaInformes) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Retenciones de IVA en CxP"
   End If
   Set reporte = Nothing
   Set insConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCXPEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaCxPSinRetencionISLR()
   Dim varSQLDelReporte As String
   Dim varReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCxPRpt
   On Error GoTo h_ERROR
   Set varReporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsCxPRpt
   varSQLDelReporte = insCxPSQL.fSQLCxPPorCancelar(gProyCompaniaActual.GetConsecutivoCompania, "", False)
   If insConfigurar.fConfigurarDatosDelReporteCxPSinRetenciones(varReporte, varSQLDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
      gUtilReports.sMostrarOImprimirReporte varReporte, 1, mDondeImprimir, "CXP Sin Retenciones del ISLR"
   End If
   Set varReporte = Nothing
   Set insConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaProveedoresSinMovimientos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLoadObjectValues(ByVal valCompaniaActual As Object, _
                              ByVal valProveedor As Object, _
                              ByVal valRptInformesEspecialesConfigurar As Object, _
                              ByVal valAdmPropAnalisisVenc As Object, _
                              ByVal valConexionesSawAOS As Object, _
                              ByVal valCxP As Object, _
                              ByVal valAnticipo As Object, _
                              ByVal valFactura As Object, _
                              ByVal valNoComunSawIva As Object, _
                              ByVal valRptFactura As Object, _
                              ByVal valComunSawIvaSqls As Object)
   On Error GoTo h_ERROR
   Set gProyCompaniaActual = valCompaniaActual
   Set insProveedorNav = valProveedor
   Set insRptInformesEspecialesConfigurar = valRptInformesEspecialesConfigurar
   Set insAdmPropAnalisisVenc = valAdmPropAnalisisVenc
   Set insConexionesSawAOS = valConexionesSawAOS
   Set insCxPNav = valCxP
   Set insAnticipoNav = valAnticipo
   Set insFacturaNav = valFactura
   Set insNoComunSawIva = valNoComunSawIva
   Set insRptFactura = valRptFactura
   Set insComunSawIvaSqls = valComunSawIvaSqls
   Set insCxPSQL = New clsCxpSQL
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLoadObjectValues", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


