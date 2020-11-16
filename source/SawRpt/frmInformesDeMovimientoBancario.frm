VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCT2.OCX"
Object = "{9F849E84-0608-4BCD-A2B4-8EC557266A4F}#11.0#0"; "GSTextBox.ocx"
Begin VB.Form frmInformesDeMovimientoBancario 
   Caption         =   "Informes de Movimientos Bancarios"
   ClientHeight    =   5730
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   9960
   LinkTopic       =   "Form1"
   ScaleHeight     =   5730
   ScaleWidth      =   9960
   Begin VB.Frame frmBeneficiario 
      Caption         =   "Filtrado por"
      Height          =   855
      Left            =   3360
      TabIndex        =   27
      Top             =   1800
      Width           =   6015
      Begin VB.CheckBox chkMovimientoBancario 
         Alignment       =   1  'Right Justify
         Caption         =   "Beneficiario"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   29
         Top             =   240
         Width           =   1935
      End
      Begin GSTextBox.GSText txtBeneficiario 
         Height          =   285
         Left            =   120
         TabIndex        =   28
         Top             =   480
         Width           =   5775
         _ExtentX        =   10186
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
      End
   End
   Begin VB.Frame frameMonto 
      Height          =   615
      Left            =   3360
      TabIndex        =   24
      Top             =   4320
      Width           =   4215
      Begin VB.CheckBox chkMovimientoBancario 
         Alignment       =   1  'Right Justify
         Caption         =   "Para Montos superiores a"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   26
         Top             =   240
         Visible         =   0   'False
         Width           =   2175
      End
      Begin GSTextBox.GSText txtMonto 
         Height          =   285
         Left            =   2400
         TabIndex        =   25
         Top             =   240
         Width           =   1695
         _ExtentX        =   2990
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
      End
   End
   Begin VB.ComboBox cmbMonedaDeLosReportes 
      Height          =   315
      Left            =   5160
      TabIndex        =   22
      Text            =   "En Moneda Original"
      Top             =   1320
      Width           =   1935
   End
   Begin VB.Frame frameTasaDeCambio 
      Caption         =   "Tasa de cambio"
      ForeColor       =   &H00808080&
      Height          =   915
      Left            =   7800
      TabIndex        =   18
      Top             =   4440
      Width           =   1515
      Begin VB.OptionButton optTasaDeCambio 
         Alignment       =   1  'Right Justify
         Caption         =   "Original"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   20
         Top             =   240
         Width           =   1215
      End
      Begin VB.OptionButton optTasaDeCambio 
         Alignment       =   1  'Right Justify
         Caption         =   "Del día"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   19
         Top             =   615
         Width           =   1215
      End
   End
   Begin VB.Frame frameCuentaBancaria 
      Caption         =   "Cuenta Bancaria"
      ForeColor       =   &H00808080&
      Height          =   1380
      Left            =   3360
      TabIndex        =   12
      Top             =   2880
      Visible         =   0   'False
      Width           =   6015
      Begin VB.TextBox txtNumeroDeCuenta 
         Height          =   285
         Left            =   120
         TabIndex        =   23
         Top             =   840
         Width           =   1455
      End
      Begin VB.ComboBox CmbCantidadAImprimir 
         Height          =   315
         Index           =   0
         Left            =   3120
         TabIndex        =   13
         Top             =   360
         Width           =   1575
      End
      Begin VB.Label lblNumeroDeCuenta 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Código"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   17
         Top             =   600
         Width           =   495
      End
      Begin VB.Label lblNombreCtaBancaria 
         BackColor       =   &H00F3F3F3&
         BorderStyle     =   1  'Fixed Single
         Height          =   285
         Left            =   1560
         TabIndex        =   16
         Top             =   840
         Width           =   4215
      End
      Begin VB.Label lblNombreCuentaBancaria 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Nombre"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   1560
         TabIndex        =   15
         Top             =   600
         Width           =   555
      End
      Begin VB.Label lblCantidadAImprimir 
         AutoSize        =   -1  'True
         BackColor       =   &H80000005&
         BackStyle       =   0  'Transparent
         Caption         =   "Cantidad Cuentas Bancarias a Imprimir....."
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   14
         Top             =   360
         Width           =   2940
      End
   End
   Begin VB.CheckBox chkAgruparPorBeneficiario 
      Alignment       =   1  'Right Justify
      Caption         =   "Agrupar por Beneficiario. Muestra solo Egresos"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   3360
      TabIndex        =   11
      Top             =   960
      Width           =   3615
   End
   Begin VB.Frame frameFechas 
      Caption         =   "Fechas"
      ForeColor       =   &H00808080&
      Height          =   1095
      Left            =   7200
      TabIndex        =   8
      Top             =   600
      Width           =   2115
      Begin MSComCtl2.DTPicker dtpFechaFinal 
         Height          =   285
         Left            =   720
         TabIndex        =   2
         Top             =   645
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   503
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   92864515
         CurrentDate     =   37187
      End
      Begin MSComCtl2.DTPicker dtpFechaInicial 
         Height          =   285
         Left            =   705
         TabIndex        =   1
         Top             =   240
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   503
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   92864515
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
         TabIndex        =   10
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
         TabIndex        =   9
         Top             =   690
         Width           =   330
      End
   End
   Begin VB.CommandButton CmdImprimir 
      Caption         =   "&Impresora"
      Height          =   375
      Left            =   120
      TabIndex        =   4
      Top             =   5160
      Width           =   1335
   End
   Begin VB.CommandButton cmdPantalla 
      Caption         =   "&Pantalla"
      Height          =   375
      Left            =   1755
      TabIndex        =   3
      Top             =   5160
      Width           =   1335
   End
   Begin VB.CommandButton CmdSalir 
      Cancel          =   -1  'True
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   3390
      TabIndex        =   5
      Top             =   5160
      Width           =   1335
   End
   Begin VB.Frame frameInformes 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   4815
      Left            =   120
      TabIndex        =   6
      Top             =   120
      Width           =   3015
      Begin VB.OptionButton optInformeDeMovimientoBancario 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Movimientos Bancarios entre fechas .........................................."
         ForeColor       =   &H00A84439&
         Height          =   495
         Index           =   0
         Left            =   120
         TabIndex        =   0
         Top             =   240
         Width           =   2715
      End
   End
   Begin VB.Label lblMonedaDeLosReportes 
      AutoSize        =   -1  'True
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Moneda en el Informe"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   3360
      TabIndex        =   21
      Top             =   1320
      Width           =   1545
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
      Height          =   540
      Left            =   3300
      TabIndex        =   7
      Top             =   60
      Width           =   6465
   End
End
Attribute VB_Name = "frmInformesDeMovimientoBancario"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "frmInformesDeMovimientoBancario"
Private Const CM_MESSAGE_NAME As String = "Informes De Movimientos Bancarios"
Private gFechasDeLosInformes As clsFechasDeLosInformesNav
Private Const CM_OPT_MOV_BANCARIO_ENTRE_FECHAS As Integer = 0
Private mDondeImprimir As enum_DondeImprimir
Private mInformeSeleccionado As Integer
Private insCuenta As Object
Private mConexion As Object
Private gProyCompaniaActual As Object
Private gUltimaTasaDeCambio As Object
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

Private Sub chkMovimientoBancario_Click(Index As Integer)
   On Error GoTo h_ERROR
   sMuestraOcultaCamposSegunSeleccion (Index)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "chkMovimientoBancario_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
''      If Not fSePuedeGenerarInforme Then
''         gAPI.ssSetFocus dtpFechaFinal
''         Cancel = False
''         GoTo h_EXIT
''      End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "dtpFechaFinal_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optInformeDeMovimientoBancario_Click(Index As Integer)
   On Error GoTo h_ERROR
   mInformeSeleccionado = Index
   sOcultaCampos
   Select Case mInformeSeleccionado
      Case CM_OPT_MOV_BANCARIO_ENTRE_FECHAS: sActivarCamposMovBancarioEntreFechas

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
            If mInformeSeleccionado = CM_OPT_MOV_BANCARIO_ENTRE_FECHAS Then
               'frameCuentaBancaria.Visible = False
               sOcultarOMostrarDatosCuentaBancaria False
            End If
         Else
            If mInformeSeleccionado = CM_OPT_MOV_BANCARIO_ENTRE_FECHAS Then
               'frameCuentaBancaria.Visible = True
               sOcultarOMostrarDatosCuentaBancaria True
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
''      If Not fSePuedeGenerarInforme Then
''         gAPI.ssSetFocus dtpFechaFinal
''         GoTo h_EXIT
''      End If
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
   CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno)
   CmbCantidadAImprimir(0).ListIndex = 0
   gEnumReport.FillComboBoxWithMonedaDeLosReportes cmbMonedaDeLosReportes, gProyParametros.GetNombreMonedaLocal
   'cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal)
   cmbMonedaDeLosReportes.ListIndex = eMR_EnMonedaOriginal
   optTasaDeCambio(0).Value = True
   cmbMonedaDeLosReportes.Width = 1935
   gFechasDeLosInformes.sLeeLasFechasDeInformes dtpFechaInicial, dtpFechaFinal, gProyUsuarioActual.GetNombreDelUsuario
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
   Me.Height = 6270
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


Private Sub sAssignFieldsFromConnectionCuenta(ByVal ValCuenta As Object)
   On Error GoTo h_ERROR
   txtNumeroDeCuenta.Text = ValCuenta.GetCodigo
   lblNombreCtaBancaria.Caption = ValCuenta.GetNombreCuenta
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionCuenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNumeroDeCuenta_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If gTexto.DfLen(txtNumeroDeCuenta.Text) = 0 Then
      txtNumeroDeCuenta.Text = "*"
   End If

   If mConexion.fSelectAndSetValuesOfCuentaBancariaFromAOS(insCuenta, txtNumeroDeCuenta.Text, "NumeroCuenta", enum_StatusCtaBancaria.eSC_ACTIVA, "Gv_CuentaBancaria_B1.Status", "", True) Then
      sAssignFieldsFromConnectionCuenta insCuenta
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNumeroDeCuenta_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElReporteApropiado()
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case CM_OPT_MOV_BANCARIO_ENTRE_FECHAS: sEjecutaElReporteMovBancarioEntreFechas

   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteApropiado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposMovBancarioEntreFechas()
   On Error GoTo h_ERROR
   lblDatosDelReporte.Caption = "Datos del Reporte - Movimientos Bancarios entre fechas"
   frameCuentaBancaria.Visible = True
   frameCuentaBancaria.Caption = "Cuenta Bancaria"
   If CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      frameCuentaBancaria.Visible = True
      sOcultarOMostrarDatosCuentaBancaria True
   Else
      frameCuentaBancaria.Visible = False
      sOcultarOMostrarDatosCuentaBancaria False
   End If
   frameFechas.Visible = True
   chkMovimientoBancario(0).Visible = True
   chkAgruparPorBeneficiario.Visible = True
   chkMovimientoBancario(1).Visible = True
   cmbMonedaDeLosReportes.Visible = True
   lblMonedaDeLosReportes.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sImprimirMovBancarioEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElReporteMovBancarioEntreFechas()
   Dim SqlDelReporte As String
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsMovimientoBancarioRpt
   Dim insBancosSQL As clsMovimientoBancarioSQL
   Dim valUsaCuentaBancaria As Boolean
   Dim valEfecturaCambioABs As Boolean
   Dim valAgruparBeneficiario As Boolean
   On Error GoTo h_ERROR
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
       dtpFechaInicial.Value = dtpFechaFinal.Value
   End If
   Set reporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsMovimientoBancarioRpt
   Set insBancosSQL = New clsMovimientoBancarioSQL
   If CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And txtNumeroDeCuenta.Text = "" Then
      sShowMessageForRequiredFields "Numero de Cuenta", txtNumeroDeCuenta
   Else
      valUsaCuentaBancaria = CmbCantidadAImprimir(0).Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno)
      valEfecturaCambioABs = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal))
      valAgruparBeneficiario = gConvert.ConvertByteToBoolean(chkAgruparPorBeneficiario.Value)

      SqlDelReporte = insBancosSQL.fSQLReporteMovimientoBancarioEntreFechas(gProyCompaniaActual.GetConsecutivoCompania, txtNumeroDeCuenta.Text, dtpFechaInicial.Value, dtpFechaFinal.Value, valUsaCuentaBancaria, valAgruparBeneficiario, gConvert.ConvertByteToBoolean(chkMovimientoBancario(0).Value), gConvert.fConvertStringToCurrency(txtMonto.Text), valEfecturaCambioABs, gUltimaTasaDeCambio, gMonedaLocalActual, optTasaDeCambio(0).Value, gConvert.ConvertByteToBoolean(chkMovimientoBancario(1).Value), txtBeneficiario.Text)
      If insConfigurar.fConfiguraDatosDelReporteDeSaldosBancarios(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), valAgruparBeneficiario) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Movimientos Bancarios Entre Fechas"
      End If
   End If

   Set insConfigurar = Nothing
   Set reporte = Nothing
   Set insBancosSQL = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElReporteSaldosBancarios", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sShowMessageForRequiredFields(ByVal valCampo As String, ByRef refCampo As TextBox)
   On Error GoTo h_ERROR
   gMessage.ShowRequiredFields valCampo
   gAPI.ssSetFocus refCampo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sShowMessageForRequiredFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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

Private Sub sOcultaCampos()
   On Error GoTo h_ERROR
   frameFechas.Visible = False
   frameTasaDeCambio.Visible = False
   chkAgruparPorBeneficiario.Visible = False
   chkMovimientoBancario(0).Visible = False
   chkMovimientoBancario(1).Visible = False
   frameCuentaBancaria.Visible = False
   cmbMonedaDeLosReportes.Visible = False
   lblMonedaDeLosReportes.Visible = False
   txtBeneficiario.Visible = False
   txtMonto.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOcultaCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraOcultaCamposSegunSeleccion(ByVal Index As Integer)
   Dim vEsVisible As Boolean
   On Error GoTo h_ERROR
   If Index = 0 Then
      vEsVisible = gConvert.ConvertByteToBoolean(chkMovimientoBancario(0).Value)
      txtMonto.Visible = vEsVisible
      txtMonto.Text = ""
   ElseIf Index = 1 Then
      vEsVisible = gConvert.ConvertByteToBoolean(chkMovimientoBancario(1).Value)
      txtBeneficiario.Visible = vEsVisible
      txtBeneficiario.Text = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "chkFiltroExistencia_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLoadObjectValues(ByVal valCompaniaActual As Object, ByVal valConexionAOS As Object, ByVal valInsCuenta As Object, ByVal valGUltimaTasaDeCambio As Object, ByVal valGMonedaLocalActual As Object)
On Error GoTo h_ERROR
   Set gProyCompaniaActual = valCompaniaActual
   Set mConexion = valConexionAOS
   Set insCuenta = valInsCuenta
   Set gUltimaTasaDeCambio = valGUltimaTasaDeCambio
   Set gMonedaLocalActual = valGMonedaLocalActual
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLoadObjectValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
