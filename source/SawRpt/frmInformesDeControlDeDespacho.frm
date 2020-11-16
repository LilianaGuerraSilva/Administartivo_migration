VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomct2.ocx"
Begin VB.Form frmInformesDeControlDeDespacho 
   BackColor       =   &H00F3F3F3&
   Caption         =   "Informes de Control de despacho"
   ClientHeight    =   4650
   ClientLeft      =   120
   ClientTop       =   450
   ClientWidth     =   9180
   LinkTopic       =   "Form1"
   ScaleHeight     =   4650
   ScaleWidth      =   9180
   Begin VB.ComboBox cmbStatus 
      Height          =   315
      Left            =   6600
      TabIndex        =   19
      Text            =   "cmbStatus"
      Top             =   2400
      Width           =   2175
   End
   Begin VB.Frame frameFechas 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Fechas"
      ForeColor       =   &H00808080&
      Height          =   1035
      Left            =   3240
      TabIndex        =   13
      Top             =   2220
      Width           =   2115
      Begin MSComCtl2.DTPicker dtpFechaFinal 
         Height          =   285
         Left            =   720
         TabIndex        =   14
         Top             =   600
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   503
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   41025539
         CurrentDate     =   37187
      End
      Begin MSComCtl2.DTPicker dtpFechaInicial 
         Height          =   285
         Left            =   705
         TabIndex        =   15
         Top             =   255
         Width           =   1275
         _ExtentX        =   2249
         _ExtentY        =   503
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   41025539
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
         TabIndex        =   17
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
         TabIndex        =   16
         Top             =   645
         Width           =   330
      End
   End
   Begin VB.TextBox txtNumeroDespacho 
      Height          =   285
      Left            =   4155
      TabIndex        =   1
      Top             =   255
      Width           =   2145
   End
   Begin VB.Frame frameInformes 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   3150
      Left            =   120
      TabIndex        =   10
      Top             =   120
      Width           =   2895
      Begin VB.OptionButton optControlDespacho 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Listado de Despachos ..............."
         ForeColor       =   &H00A84439&
         Height          =   495
         Index           =   1
         Left            =   45
         TabIndex        =   18
         Top             =   840
         Value           =   -1  'True
         Width           =   2685
      End
      Begin VB.OptionButton optControlDespacho 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Impresión de comprobante de despacho............................"
         ForeColor       =   &H00A84439&
         Height          =   495
         Index           =   0
         Left            =   60
         TabIndex        =   0
         Top             =   240
         Width           =   2685
      End
   End
   Begin VB.CommandButton cmdPantalla 
      Caption         =   "&Pantalla"
      Height          =   375
      Left            =   5910
      TabIndex        =   5
      Top             =   3630
      Width           =   1335
   End
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   7350
      TabIndex        =   6
      Top             =   3630
      Width           =   1335
   End
   Begin VB.Frame frameCliente 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Cliente"
      ForeColor       =   &H00808080&
      Height          =   1335
      Left            =   3210
      TabIndex        =   7
      Top             =   795
      Width           =   5565
      Begin VB.ComboBox CmbCantidadImprimir 
         Height          =   315
         Left            =   1575
         TabIndex        =   2
         Text            =   "cmbCantidadAImprimirFacCliente"
         Top             =   180
         Width           =   1635
      End
      Begin VB.TextBox txtCodigoDeCliente 
         Height          =   285
         Left            =   1560
         TabIndex        =   3
         Top             =   600
         Width           =   1095
      End
      Begin VB.TextBox txtNombreDeCliente 
         Height          =   285
         Left            =   1560
         TabIndex        =   4
         Top             =   960
         Width           =   3810
      End
      Begin VB.Label lblNombreDeCliente 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Código Cliente"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   11
         Top             =   645
         Width           =   1020
      End
      Begin VB.Label lblNombreDeCliente 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Nombre Cliente"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   9
         Top             =   1005
         Width           =   1080
      End
      Begin VB.Label lblCantidadimprimir 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Cantidad a Imprimir"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   8
         Top             =   240
         Width           =   1335
      End
   End
   Begin VB.Label lblStatus 
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      Caption         =   "Estatus"
      ForeColor       =   &H00A84439&
      Height          =   240
      Left            =   5655
      TabIndex        =   20
      Top             =   2445
      Width           =   765
   End
   Begin VB.Label lblNumeroDespacho 
      AutoSize        =   -1  'True
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      Caption         =   "Número"
      ForeColor       =   &H00A84439&
      Height          =   195
      Index           =   0
      Left            =   3255
      TabIndex        =   12
      Top             =   270
      Width           =   555
   End
End
Attribute VB_Name = "frmInformesDeControlDeDespacho"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "frmInformesDeControlDeDespacho"
Private Const CM_MESSAGE_NAME = "Informes de Control de Despacho"
Private gFechasDeLosInformes As clsFechasDeLosInformesNav
Private mInformeSeleccionado As Integer
Private mDondeImprimir As enum_DondeImprimir
Private insCliente As Object
Private insControlDespacho As Object
Private gProyCompaniaActual As Object

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function CM_OPT_IMPRESION_DE_COMPROBANTE() As Integer
   CM_OPT_IMPRESION_DE_COMPROBANTE = 0
End Function

Private Function CM_OPT_LISTADO_DE_DESPACHO() As Integer
   CM_OPT_LISTADO_DE_DESPACHO = 1
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
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCheckForSpecialKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInitDefaultValues()
   Dim i As Integer
   On Error GoTo h_ERROR
   gFechasDeLosInformes.sLeeLasFechasDeInformes dtpFechaInicial, dtpFechaFinal, gProyUsuarioActual.GetNombreDelUsuario
   gEnumProyecto.FillComboBoxWithStatusControlDespacho cmbStatus, Buscar
   cmbStatus.Text = gEnumProyecto.enumStatusControlDespachoToString(eSCD_PorProcesar)
   cmbStatus.ListIndex = 0
   gEnumReport.FillComboBoxWithCantidadAImprimir CmbCantidadImprimir
   CmbCantidadImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS)
   CmbCantidadImprimir.ListIndex = 1
h_EXIT:  On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitDefaultValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub CmbCantidadImprimir_Click()
   On Error GoTo h_ERROR
   If CmbCantidadImprimir.ListIndex = 1 Then
      lblNombreDeCliente(0).Visible = False
      lblNombreDeCliente(1).Visible = False
      txtNombreDeCliente.Visible = False
      txtCodigoDeCliente.Visible = False
      txtNombreDeCliente.Text = ""
   Else
      lblNombreDeCliente(0).Visible = True
      txtNombreDeCliente.Visible = True
      If gProyParametros.GetUsaCodigoClienteEnPantalla Then
         txtCodigoDeCliente.Visible = True
         lblNombreDeCliente(1).Visible = True
         txtNombreDeCliente.Enabled = False
      Else
         lblNombreDeCliente(1).Visible = False
         txtCodigoDeCliente.Visible = False
         txtNombreDeCliente.Enabled = True
         txtNombreDeCliente.Left = txtCodigoDeCliente.Left
      End If
   End If
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CmbCantidadImprimir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_PANTALLA
   sEjecutaElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdGrabar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdPantalla_Click()
  mDondeImprimir = eDI_PANTALLA
  sEjecutaElInformeSeleccionado
End Sub

Private Sub cmdSalir_Click()
  On Error GoTo h_ERROR
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Sub

Private Sub Form_Unload(Cancel As Integer)
   On Error GoTo h_ERROR
   gFechasDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.Value, dtpFechaFinal.Value, gProyUsuarioActual.GetNombreDelUsuario
'   Set gFechasDeLosInformes = Nothing
'   Set insControlDespacho = Nothing
'   Set insCliente = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Unload", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optControlDespacho_Click(Index As Integer)
   On Error GoTo h_ERROR
   mInformeSeleccionado = Index
   sOcultaTodosLosCampos
   Select Case mInformeSeleccionado
      Case CM_OPT_IMPRESION_DE_COMPROBANTE: sActivaCamposDelInformeComprobanteControlDespacho
      Case CM_OPT_LISTADO_DE_DESPACHO: sActivaCamposDelListadoDeDespacho
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInformesDeCompra_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Sub

Private Sub txtCodigoDeCliente_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtCodigoDeCliente
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeCliente_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeCliente_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeCliente_KeyDown()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeCliente_Validate(Cancel As Boolean)
On Error GoTo h_ERROR
   
   If LenB(txtCodigoDeCliente.Text) = 0 Then
     txtCodigoDeCliente.Text = "*"
   End If
   insCliente.sClrRecord
   insCliente.SetNombre txtCodigoDeCliente.Text
   If insCliente.fSearchSelectConnection(True, False, False, 0, False, True) Then
      sSelectAndSetValuesOfCliente insCliente
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " txtNombreDeCliente_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeCliente_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtNombreDeCliente
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeCliente_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeCliente_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeCliente_KeyDown()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeCliente_KeyPress(KeyAscii As Integer)
On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeCliente_KeyPress()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeCliente_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If LenB(txtNombreDeCliente.Text) = 0 Then
      txtNombreDeCliente.Text = "*"
   End If
   insCliente.sClrRecord
   insCliente.SetNombre txtNombreDeCliente.Text
   If insCliente.fSearchSelectConnection(True, False, False, 0, False, True) Then
      sSelectAndSetValuesOfCliente insCliente
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " txtNombreDeCliente_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultaTodosLosCampos()
   On Error GoTo h_ERROR
   lblNumeroDespacho(0).Visible = False
   txtNumeroDespacho.Visible = False
   txtNumeroDespacho.Text = ""
   frameCliente.Visible = False
   txtCodigoDeCliente.Text = ""
   txtNombreDeCliente.Text = ""
   frameFechas.Visible = False
   lblStatus.Visible = False
   cmbStatus.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOcultaTodosLosCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sSelectAndSetValuesOfCliente(ByRef refCliente As Object)
   On Error GoTo h_ERROR
   If refCliente.fRsRecordCount(False) = 1 Then
      sAssignFieldsFromConnectionCliente refCliente
   ElseIf refCliente.fRsRecordCount(False) > 1 Then
      If gProyParametros.GetUsaCodigoClienteEnPantalla Then
         refCliente.sShowListSelect "Codigo", txtCodigoDeCliente.Text
      Else
         refCliente.sShowListSelect "Nombre", txtNombreDeCliente.Text
      End If
      sAssignFieldsFromConnectionCliente refCliente
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sSelectAndSetValuesOfCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAssignFieldsFromConnectionCliente(ByVal valCliente As Object)
   On Error GoTo h_ERROR
   txtNombreDeCliente.Text = valCliente.GetNombre
   txtCodigoDeCliente.Text = valCliente.GetCodigo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElInformeSeleccionado()
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case CM_OPT_IMPRESION_DE_COMPROBANTE(): sEjecutaImpresionDeComprobanteDespacho
      Case CM_OPT_LISTADO_DE_DESPACHO(): sEjecutaListadoDeDespacho
      
     End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElInformeSeleccionado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInitLookAndFeel()
   On Error GoTo h_ERROR
   Set gFechasDeLosInformes = New clsFechasDeLosInformesNav
   Me.Caption = CM_MESSAGE_NAME
   mInformeSeleccionado = CM_OPT_IMPRESION_DE_COMPROBANTE
   gAPI.ssSetFocus optControlDespacho(0)
   sInitDefaultValues
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sShowMessageForRequiredFields()
   gMessage.ShowRequiredFields "Código, Nombre"
End Sub
Private Sub Form_Load()
   On Error GoTo h_ERROR
   Me.ZOrder 0
   If gDefgen.getMainForm.Width > Width Then
      Left = (gDefgen.getMainForm.Width - Width) / 4
      Top = (gDefgen.getMainForm.Height - Height) / 4
   Else
      Left = 0
      Top = 0
   End If
   Me.Width = 9420
   Me.Height = 5220
   mInformeSeleccionado = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNumeroDespacho_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtNumeroDespacho
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNumeroDespacho_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNumeroDespacho_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNumeroDespacho_KeyDown()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sSelectAndSetValuesOfControlDespacho(ByRef refControlDespacho As Object)
   On Error GoTo h_ERROR
   If refControlDespacho.fRsRecordCount(False) = 1 Then
      sAssignFieldsFromConnectionControlDespacho refControlDespacho
   ElseIf refControlDespacho.fRsRecordCount(False) > 1 Then
      refControlDespacho.sShowListSelect "Numero", txtNumeroDespacho.Text
      sAssignFieldsFromConnectionControlDespacho refControlDespacho
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sSelectAndSetValuesOfControlDespacho", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAssignFieldsFromConnectionControlDespacho(ByVal valControlDespacho As Object)
   On Error GoTo h_ERROR
   txtNumeroDespacho.Text = valControlDespacho.GetNumero
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionControlDespacho", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNumeroDespacho_KeyPress(KeyAscii As Integer)
   If KeyAscii = 13 Then
      If LenB(txtNumeroDespacho) = 0 Then
         txtNumeroDespacho = "*"
      End If
      insControlDespacho.sClrRecord
      insControlDespacho.SetNumero txtNumeroDespacho.Text
      If insControlDespacho.fSearchSelectConnection(igMostrarMensaje) Then
         sSelectAndSetValuesOfControlDespacho insControlDespacho
      Else
         GoTo h_EXIT
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " txtNombreDeCliente_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivaCamposDelInformeComprobanteControlDespacho()
   On Error GoTo h_ERROR
   lblNumeroDespacho(0).Visible = True
   txtNumeroDespacho.Visible = True
   frameCliente.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivaCamposDelInformeComprasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivaCamposDelListadoDeDespacho()
   On Error GoTo h_ERROR
   frameCliente.Visible = True
   frameFechas.Visible = True
   lblStatus.Visible = True
   cmbStatus.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivaCamposDelListadoDeDespacho", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaImpresionDeComprobanteDespacho()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsControlDespachoRpt
   Dim insControlDespachoSQL As clsControlDespachoSQL
   Dim vSqlDelReporte As String
   On Error GoTo h_ERROR
   Set insControlDespachoSQL = New clsControlDespachoSQL
   Set insConfigurar = New clsControlDespachoRpt
   
   If CmbCantidadImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And LenB(txtCodigoDeCliente.Text) = 0 Then
      sShowMessageForRequiredFields
   Else
      Set reporte = New DDActiveReports2.ActiveReport
      
      vSqlDelReporte = insControlDespachoSQL.fSQLImpresionComprobanteControlDespacho(gProyCompaniaActual.GetConsecutivoCompania, txtNumeroDespacho.Text, CmbCantidadImprimir.Text, txtCodigoDeCliente.Text)
           
      If insConfigurar.fConfigurarDatosDelReporteImpresionComprobanteDeDespacho(reporte, vSqlDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Impresión Comprobante de Despacho"
      End If
      Set insConfigurar = Nothing
      Set insControlDespachoSQL = Nothing
      Set reporte = Nothing
  End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaImpresionDeComprobanteDespacho", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaListadoDeDespacho()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsControlDespachoRpt
   Dim insControlDeDespachoSQL As clsControlDespachoSQL
   Dim vSqlDelReporte As String
   Dim ValcmbStatus As String
   On Error GoTo h_ERROR
   Set insControlDeDespachoSQL = New clsControlDespachoSQL
   Set insConfigurar = New clsControlDespachoRpt
   ValcmbStatus = gAPI.SelectedElementInComboBoxToString(cmbStatus)
   If dtpFechaFinal.value < dtpFechaInicial.value Then
      dtpFechaInicial.value = dtpFechaFinal.value
   End If
   If CmbCantidadImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And LenB(txtCodigoDeCliente.Text) = 0 Then
      sShowMessageForRequiredFields
   Else
      Set reporte = New DDActiveReports2.ActiveReport
      ValcmbStatus = gAPI.SelectedElementInComboBoxToString(cmbStatus)
      vSqlDelReporte = insControlDeDespachoSQL.fSQLListadoDeDespacho(gProyCompaniaActual.GetConsecutivoCompania, txtNumeroDespacho.Text, CmbCantidadImprimir.Text, txtCodigoDeCliente.Text, dtpFechaInicial.value, dtpFechaFinal.value, ValcmbStatus)
      If insConfigurar.fConfigurarDatosDelReporteListadoDeDespachos(reporte, vSqlDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False)) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Listado de Despachos"
      End If
      Set insConfigurar = Nothing
      Set insControlDeDespachoSQL = Nothing
      Set reporte = Nothing
  End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaListadoDeDespacho", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLoadObjectValues(ByVal valInsCompaniaActual As Object, ByVal valInsControlDespacho As Object, _
                                 ByVal valInsCliente As Object)
   On Error GoTo h_ERROR
   Set gProyCompaniaActual = valInsCompaniaActual
   Set insControlDespacho = valInsControlDespacho
   Set insCliente = valInsCliente
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLoadObjectValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
