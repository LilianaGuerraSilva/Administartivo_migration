VERSION 5.00
Begin VB.Form frmInformesDeVendedor 
   BackColor       =   &H00F3F3F3&
   Caption         =   "Informes de Vendedor"
   ClientHeight    =   3225
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   10320
   LinkTopic       =   "Form1"
   ScaleHeight     =   3225
   ScaleWidth      =   10320
   Begin VB.Frame pnlClientes 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Filtrar por Clientes"
      ForeColor       =   &H00808080&
      Height          =   705
      Left            =   4080
      TabIndex        =   13
      Top             =   1680
      Width           =   6090
      Begin VB.ComboBox cmbStatus 
         Height          =   315
         HelpContextID   =   1
         Left            =   780
         TabIndex        =   6
         Top             =   240
         Width           =   1560
      End
      Begin VB.Label lblFrmCliente 
         AutoSize        =   -1  'True
         BackColor       =   &H80000016&
         BackStyle       =   0  'Transparent
         Caption         =   "Status"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   2
         Left            =   120
         TabIndex        =   16
         Top             =   270
         Width           =   450
      End
   End
   Begin VB.Frame frameInformesGenerales 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H00A84439&
      Height          =   2295
      Left            =   105
      TabIndex        =   11
      Top             =   135
      Width           =   3825
      Begin VB.OptionButton optImprimirInforme 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Listado de Clientes Por Vendedor ................."
         ForeColor       =   &H00A84439&
         Height          =   495
         Index           =   2
         Left            =   135
         TabIndex        =   2
         Top             =   1500
         Width           =   3570
      End
      Begin VB.OptionButton optImprimirBorrador 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Libro de Ventas ........................................"
         ForeColor       =   &H00A84439&
         Height          =   255
         Index           =   1
         Left            =   3735
         TabIndex        =   7
         Top             =   5310
         Visible         =   0   'False
         Width           =   2670
      End
      Begin VB.OptionButton optImprimirInforme 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Listado de Vendedores ..................................."
         ForeColor       =   &H00A84439&
         Height          =   495
         Index           =   0
         Left            =   120
         TabIndex        =   0
         Top             =   315
         Width           =   3570
      End
      Begin VB.OptionButton optImprimirInforme 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Listado de Vendores por Comisión por Línea de Producto ...................................................."
         ForeColor       =   &H00A84439&
         Height          =   495
         Index           =   1
         Left            =   120
         TabIndex        =   1
         Top             =   975
         Width           =   3570
      End
   End
   Begin VB.CommandButton cmdSalir 
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   435
      Left            =   2490
      TabIndex        =   10
      Top             =   2610
      Width           =   1095
   End
   Begin VB.CommandButton CmdImprimir 
      Caption         =   "&Impresora"
      Height          =   435
      Left            =   90
      TabIndex        =   8
      Top             =   2610
      Width           =   1095
   End
   Begin VB.CommandButton cmdPantalla 
      Caption         =   "&Pantalla"
      Height          =   435
      Left            =   1290
      TabIndex        =   9
      Top             =   2610
      Width           =   1095
   End
   Begin VB.Frame FrameVendedores 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Vendedor:"
      ForeColor       =   &H00808080&
      Height          =   1185
      Left            =   4050
      TabIndex        =   12
      Top             =   465
      Width           =   6090
      Begin VB.TextBox txtNombreDeVendedor 
         Height          =   285
         Left            =   2040
         TabIndex        =   5
         Top             =   720
         Width           =   3795
      End
      Begin VB.ComboBox cmbCantidadAImprimirVendedor 
         Height          =   315
         Left            =   2040
         TabIndex        =   3
         Top             =   240
         Width           =   1575
      End
      Begin VB.TextBox txtCodigoDeVendedor 
         Height          =   285
         Left            =   1080
         TabIndex        =   4
         Top             =   720
         Width           =   975
      End
      Begin VB.Label lblNombreDeVendedor 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Nombre "
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   15
         Top             =   780
         Width           =   720
      End
      Begin VB.Label lblCantidadAImprimirVendedor 
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Vendedor a Imprimir"
         ForeColor       =   &H00A84439&
         Height          =   255
         Left            =   180
         TabIndex        =   14
         Top             =   300
         Width           =   1815
      End
   End
   Begin VB.Label lblTituloDeReporte 
      AutoSize        =   -1  'True
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Titulo del Informe"
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
      Height          =   345
      Left            =   4050
      TabIndex        =   17
      Top             =   135
      Width           =   2115
   End
End
Attribute VB_Name = "frmInformesDeVendedor"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private mDondeImprimir As enum_DondeImprimir
Private mInformeSeleccionado As Integer
Private insVendedor As Object
Private gProyCompaniaActual As Object
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "frmInformesDeVendedor"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Informes De Vendedor"
End Function

Private Function CM_OPT_LISTADO_DE_VENDEDORES() As Integer
   CM_OPT_LISTADO_DE_VENDEDORES = 0
End Function

Private Function CM_OPT_LISTADO_DE_VENDEDORES_POR_COMISION_POR_LINEA_DE_PRODUCTO() As Integer
   CM_OPT_LISTADO_DE_VENDEDORES_POR_COMISION_POR_LINEA_DE_PRODUCTO = 1
End Function

Private Function CM_OPT_LISTADO_DE_CLIENTES_POR_VENDEDOR() As Integer
   CM_OPT_LISTADO_DE_CLIENTES_POR_VENDEDOR = 2
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

Private Sub cmbCantidadAImprimirVendedor_Click()
On Error GoTo h_ERROR
   If cmbCantidadAImprimirVendedor.Text = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
         txtNombreDeVendedor.Visible = True
         lblNombreDeVendedor.Visible = True
         If gProyParametros.GetUsaCodigoVendedorEnPantalla Then
            txtCodigoDeVendedor.Visible = True
         Else
            txtCodigoDeVendedor.Visible = False
         End If
   Else
         txtNombreDeVendedor.Visible = False
         lblNombreDeVendedor.Visible = False
         txtCodigoDeVendedor.Visible = False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimirVendedor_Click()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbCantidadAImprimirVendedor_KeyDown(KeyCode As Integer, Shift As Integer)
On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimirVendedor_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbCantidadAImprimirVendedor_KeyPress(KeyAscii As Integer)
    On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimirVendedor_KeyPress()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbStatus_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbStatus_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbStatus_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   gAPI.ValidateTextInComboBox cmbStatus
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbStatus_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImprimir_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_IMPRESORA
   sEjecutaElInformeApropiado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CmdImprimir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImprimir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CmdImprimir_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdPantalla_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_PANTALLA
   sEjecutaElInformeApropiado
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
   'Set insVendedor = New clsVendedorNavigator
   lblTituloDeReporte.Visible = True
   gEnumReport.FillComboBoxWithCantidadAImprimir cmbCantidadAImprimirVendedor
   cmbCantidadAImprimirVendedor.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS)
   cmbCantidadAImprimirVendedor.ListIndex = 1
   mInformeSeleccionado = 0
   sValidaLosCampos mInformeSeleccionado
   gEnumProyecto.FillComboBoxWithStatusCliente cmbStatus, Buscar
   cmbStatus.Width = gAPI.maxLengthOfValuesOfComboBox(cmbStatus) * 100 'Numero estimado aproximado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElInformeApropiado()
   Dim sql As String
   Dim insConfigurar As clsCxCRpt
   Dim rpt As DDActiveReports2.ActiveReport
   Dim insVendedorSQL As clsVendedorSQL
   On Error GoTo h_ERROR
   Set insVendedorSQL = New clsVendedorSQL
   Set insConfigurar = New clsCxCRpt
   Set rpt = New DDActiveReports2.ActiveReport
   Select Case mInformeSeleccionado
      Case CM_OPT_LISTADO_DE_VENDEDORES
         sql = insVendedorSQL.fSQLListadoDeVendedores(cmbCantidadAImprimirVendedor.Text, txtCodigoDeVendedor.Text, gProyCompaniaActual.GetConsecutivoCompania)
         If insConfigurar.fConfigurarDatosDelReporteVendedorXLienaDeProducto(rpt, sql, True, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), gGlobalization) Then
            gUtilReports.sMostrarOImprimirReporte rpt, 1, mDondeImprimir, "Listado de Vendedores"
         End If
      Case CM_OPT_LISTADO_DE_VENDEDORES_POR_COMISION_POR_LINEA_DE_PRODUCTO
         sql = insVendedorSQL.fSQLVendedoresXLinea(cmbCantidadAImprimirVendedor.Text, txtCodigoDeVendedor.Text, gProyCompaniaActual.GetConsecutivoCompania)
         If insConfigurar.fConfigurarDatosDelReporteVendedorXLienaDeProducto(rpt, sql, False, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), gGlobalization) Then
            gUtilReports.sMostrarOImprimirReporte rpt, 1, mDondeImprimir, "Listado de Vendedores x Línea"
         End If
      Case CM_OPT_LISTADO_DE_CLIENTES_POR_VENDEDOR
         sql = insVendedorSQL.fSQLVendedoresXCliente(cmbCantidadAImprimirVendedor.Text, txtCodigoDeVendedor.Text, gProyCompaniaActual.GetConsecutivoCompania, gAPI.SelectedElementInComboBoxToString(cmbStatus) <> gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) & "(as)", gEnumProyecto.strStatusClienteToNum(gAPI.SelectedElementInComboBoxToString(cmbStatus)))
         If insConfigurar.fConfigurarDatosDelReporteClientesPorVendedor(rpt, sql, False, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), gGlobalization) Then
            gUtilReports.sMostrarOImprimirReporte rpt, 1, mDondeImprimir, "Listado de Clientes Por Vendedor"
         End If
   End Select
   Set rpt = Nothing
   Set insConfigurar = Nothing
   Set insVendedorSQL = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElInformeApropiado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Load()
   On Error GoTo h_ERROR
   Me.AutoRedraw = True
   Me.ZOrder 0
   If gDefgen.getMainForm.Width > Width Then
      Left = (gDefgen.getMainForm.Width - Width) / 2
      Top = (gDefgen.getMainForm.Height - Height) / 4
   Else
      Left = 0
      Top = 0
   End If
   Me.Width = 10440
   Me.Height = 3650
   mInformeSeleccionado = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sValidaLosCampos(ByVal valIndex As Integer)
   On Error GoTo h_ERROR
   mInformeSeleccionado = valIndex
   optImprimirInforme(valIndex).Value = True
   pnlClientes.Visible = False
    Select Case mInformeSeleccionado
      Case CM_OPT_LISTADO_DE_VENDEDORES
         lblTituloDeReporte.Caption = "Listado de Vendedores"
      Case CM_OPT_LISTADO_DE_VENDEDORES_POR_COMISION_POR_LINEA_DE_PRODUCTO
         lblTituloDeReporte.Caption = "Listado de Vendedores X Línea de Producto"
      Case CM_OPT_LISTADO_DE_CLIENTES_POR_VENDEDOR
         pnlClientes.Visible = True
    End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sValidaLosCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optImprimirInforme_Click(Index As Integer)
   On Error GoTo h_ERROR
   sValidaLosCampos Index
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optImprimirInforme_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optImprimirInforme_DblClick(Index As Integer)
   On Error GoTo h_ERROR
   sValidaLosCampos Index
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optImprimirInforme_DblClick", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optImprimirInforme_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sValidaLosCampos Index
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optImprimirInforme_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeVendedor_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtNombreDeVendedor
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeVendedor_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeVendedor_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeVendedor_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
 
Private Sub txtNombreDeVendedor_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeVendedor_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeVendedor_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If LenB(txtNombreDeVendedor) = 0 Then
      txtNombreDeVendedor = "*"
   End If
   insVendedor.sClrRecord
   insVendedor.SetNombre txtNombreDeVendedor
   If insVendedor.fSearchSelectConnection() Then
      sSelectAndSetValuesOfVendedor
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeVendedor_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sSelectAndSetValuesOfVendedor()
  On Error GoTo h_ERROR
   If insVendedor.fRsRecordCount(False) = 1 Then
      sAssignFieldsFromConnectionVendedor
   ElseIf insVendedor.fRsRecordCount(False) > 1 Then
      If gProyParametros.GetUsaCodigoVendedorEnPantalla Then
         insVendedor.sShowListSelect "Codigo", txtCodigoDeVendedor.Text
      Else
         insVendedor.sShowListSelect "Nombre", txtNombreDeVendedor.Text
      End If
      sAssignFieldsFromConnectionVendedor
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sSelectAndSetValuesOfVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAssignFieldsFromConnectionVendedor()
   On Error GoTo h_ERROR
   txtNombreDeVendedor.Text = insVendedor.GetNombre
   txtCodigoDeVendedor.Text = insVendedor.GetCodigo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLoadObjectValues(ByVal valVendedorNavigator As Object, ByVal valProyCompaniaActual As Object)
On Error GoTo h_ERROR
   Set insVendedor = valVendedorNavigator
   Set gProyCompaniaActual = valProyCompaniaActual
   sInitLookAndFeel
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLoadObjectValues", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


