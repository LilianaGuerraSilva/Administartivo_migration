VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCT2.OCX"
Begin VB.Form frmRepCompDeContabilizacionAutomac 
   BackColor       =   &H00F3F3F3&
   Caption         =   "Comprobantes De Contabilizacion Automatica"
   ClientHeight    =   4545
   ClientLeft      =   210
   ClientTop       =   2040
   ClientWidth     =   11655
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MDIChild        =   -1  'True
   ScaleHeight     =   4545
   ScaleWidth      =   11655
   Begin VB.ComboBox CmbOpcionTipoDeDocumentoAImprimir 
      Height          =   315
      ItemData        =   "frmRepCompDeContabilizacionAutomac.frx":0000
      Left            =   7440
      List            =   "frmRepCompDeContabilizacionAutomac.frx":0002
      TabIndex        =   3
      Top             =   960
      Width           =   975
   End
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   3360
      TabIndex        =   8
      Top             =   3660
      Width           =   1335
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "&Pantalla"
      Height          =   375
      Left            =   1740
      TabIndex        =   7
      Top             =   3660
      Width           =   1335
   End
   Begin VB.CommandButton cmdImpresora 
      Caption         =   "&Impresora"
      Height          =   375
      Left            =   120
      TabIndex        =   6
      Top             =   3660
      Width           =   1335
   End
   Begin VB.Frame frameInformes 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   3315
      Left            =   120
      TabIndex        =   9
      Top             =   120
      Width           =   3975
      Begin VB.OptionButton optComprobantesDeContabilizacionAutomatica 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Comprobantes con &Nro Doc. Origen Duplicado"
         CausesValidation=   0   'False
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
         Height          =   375
         Index           =   4
         Left            =   120
         TabIndex        =   17
         Top             =   2760
         Width           =   3495
      End
      Begin VB.OptionButton optComprobantesDeContabilizacionAutomatica 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Comprobantes Modificados"
         CausesValidation=   0   'False
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
         Index           =   3
         Left            =   120
         TabIndex        =   16
         Top             =   2160
         Width           =   3495
      End
      Begin VB.OptionButton optComprobantesDeContabilizacionAutomatica 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Documentos sin Comprobantes"
         CausesValidation=   0   'False
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
         Index           =   0
         Left            =   120
         TabIndex        =   0
         Top             =   480
         Width           =   3495
      End
      Begin VB.OptionButton optComprobantesDeContabilizacionAutomatica 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Documentos Contabilizados"
         CausesValidation=   0   'False
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
         Index           =   1
         Left            =   120
         TabIndex        =   1
         Top             =   1020
         Width           =   3495
      End
      Begin VB.OptionButton optComprobantesDeContabilizacionAutomatica 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Comprobantes sin Documento Origen"
         CausesValidation=   0   'False
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
         Index           =   2
         Left            =   120
         TabIndex        =   2
         Top             =   1560
         Width           =   3495
      End
   End
   Begin MSComCtl2.DTPicker dtpFechaFinal 
      Height          =   285
      Left            =   7440
      TabIndex        =   5
      Top             =   2160
      Width           =   1215
      _ExtentX        =   2143
      _ExtentY        =   503
      _Version        =   393216
      CustomFormat    =   "dd/MM/yyyy"
      Format          =   62128131
      CurrentDate     =   36978
   End
   Begin MSComCtl2.DTPicker dtpFechaInicial 
      Height          =   285
      Left            =   7440
      TabIndex        =   4
      Top             =   1680
      Width           =   1215
      _ExtentX        =   2143
      _ExtentY        =   503
      _Version        =   393216
      CustomFormat    =   "dd/MM/yyyy"
      Format          =   62128131
      CurrentDate     =   36978
   End
   Begin VB.Label lblOpcionTipoDeDocumentoAImprimir 
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      Caption         =   "Opción Tipo de Documento a Imprimir :"
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
      Height          =   435
      Left            =   4440
      TabIndex        =   15
      Top             =   960
      Width           =   2610
   End
   Begin VB.Label lblAnchoPapelImpresora 
      AutoSize        =   -1  'True
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      Caption         =   "Ancho Papel Impresora"
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
      Height          =   195
      Left            =   4440
      TabIndex        =   14
      Top             =   2640
      Width           =   1980
   End
   Begin VB.Label lblCantidadDeColumnas 
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      BorderStyle     =   1  'Fixed Single
      Caption         =   "80 Columnas"
      Enabled         =   0   'False
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
      Left            =   7440
      TabIndex        =   13
      Top             =   2640
      Width           =   1215
   End
   Begin VB.Label lblFechaFinal 
      AutoSize        =   -1  'True
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      Caption         =   "Fecha Final"
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
      Height          =   195
      Left            =   4440
      TabIndex        =   12
      Top             =   2160
      Width           =   1005
   End
   Begin VB.Label lblFechaInicial 
      AutoSize        =   -1  'True
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      Caption         =   "Fecha  Inicial"
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
      Height          =   195
      Left            =   4440
      TabIndex        =   11
      Top             =   1680
      Width           =   1170
   End
   Begin VB.Label lblDatosDelInforme 
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00A84439&
      Height          =   255
      Left            =   4440
      TabIndex        =   10
      Top             =   480
      Width           =   6495
   End
End
Attribute VB_Name = "frmRepCompDeContabilizacionAutomac"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private gFechasDeLosInformes As clsFechasDeLosInformesNav
Private mInformeSeleccionado As Integer
Private mDondeImprimir As enum_DondeImprimir
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "frmComprobantesDeContabilizacionAutomatica"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Comprobantes De Contabilizacion Automatica"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function mInformeDocsSinComp() As Long
   mInformeDocsSinComp = 0
End Function

Private Function mInformeDocsContabilizados() As Long
   mInformeDocsContabilizados = 1
End Function

Private Function mInformeCompSinDocOrigen() As Long
   mInformeCompSinDocOrigen = 2
End Function

Private Function mInformeComprobantesModificados() As Long
   mInformeComprobantesModificados = 3
End Function

Private Function mInformeCompConNroDocOrigenDuplicado() As Long
   mInformeCompConNroDocOrigenDuplicado = 4
End Function

Private Sub sCheckForSpecialKeys(ByVal valKeyCode As Integer, ByVal valShift As Integer)
   Dim otherUserUdatedFirst As Boolean
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, valKeyCode) Then
   ElseIf (valKeyCode = vbKeyF6) Then
      cmdGrabar_Click
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCheckForSpecialKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_PANTALLA
   sMuestraElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdGrabar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImpresora_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdImpresora_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdSalir_Click()
   On Error GoTo h_ERROR
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdSalir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdGrabar_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdSalir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
  sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdSalir_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImpresora_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_IMPRESORA
   sMuestraElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdImpresora_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInitLookAndFeel()
   On Error GoTo h_ERROR
   Me.Caption = CM_MESSAGE_NAME
   Set gFechasDeLosInformes = New clsFechasDeLosInformesNav
   sInitDefaultValues
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtpFechaFinal_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If Not gContPeriodoActual.LaFechaPerteneceAlPeriodoActual(dtpFechaFinal.value, True) Then
      Cancel = True
      gAPI.ssSetFocus dtpFechaFinal
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "dtpFechaFinal_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtpFechaInicial_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If Not gContPeriodoActual.LaFechaPerteneceAlPeriodoActual(dtpFechaInicial.value, True) Then
      Cancel = True
      gAPI.ssSetFocus dtpFechaInicial
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "dtpFechaInicial_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   Me.Width = 10635
   Me.Height = 4950
   mInformeSeleccionado = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Unload(Cancel As Integer)
   On Error GoTo h_ERROR
   gFechasDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.value, dtpFechaFinal.value, gProyUsuarioActual.GetNombreDelUsuario
   Set gFechasDeLosInformes = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Form_Unload", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraElInformeSeleccionado()
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case mInformeDocsSinComp: sMuestraInformesDeDocumentosSinComprobantes
      Case mInformeDocsContabilizados: sMuestraInformesDeDocumentosContabilizados
      Case mInformeCompSinDocOrigen: sMuestraInformesDeComprobantesSinDocumentos
      Case mInformeComprobantesModificados: sEjecutaElInformeDeComprobantesModificados
      Case mInformeCompConNroDocOrigenDuplicado: sEjecutaElInformeDeComprobantesConNroDocOrigenDuplicado
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sMuestraElInformeSeleccionado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeInformeDeDocumentosSinComprobante()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Documentos sin Comprobante"
   lblOpcionTipoDeDocumentoAImprimir.Visible = True
   CmbOpcionTipoDeDocumentoAImprimir.Visible = True
   lblFechaFinal.Visible = True
   dtpFechaFinal.Visible = True
   lblFechaInicial.Visible = True
   dtpFechaInicial.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "sActivarCamposDeInformeDeDocumentosSinComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeInformeDeDocumentosContabilizados()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Documentos Contabilizados"
   lblOpcionTipoDeDocumentoAImprimir.Visible = True
   CmbOpcionTipoDeDocumentoAImprimir.Visible = True
   lblFechaFinal.Visible = True
   dtpFechaFinal.Visible = True
   lblFechaInicial.Visible = True
   dtpFechaInicial.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "sActivarCamposDeInformeDeDocumentosContabilizados", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fConstruirSQLDelInformesDeDocumentosContabilizados() As String
   Dim SQL As String
   Dim insCxC As clsCxCNavigator
   Dim insCxP As clsCxPNavigator
   Dim insCobranza As clsCobranzaNavigator
   Dim insFactura As clsFacturaNavigator
   Dim insMovimientoBancario As clsMovimientoBancarioNavigator
   Dim insPago As clsPagoNavigator
   Dim insAnticipo As clsAnticipoNavigator
   Dim tipoDocumento As enum_ComprobanteGeneradoPor
   On Error GoTo h_ERROR
   tipoDocumento = gEnumProyectoWincont.strComprobanteGeneradoPorToNum(CmbOpcionTipoDeDocumentoAImprimir.Text)
   Select Case tipoDocumento
      Case enum_ComprobanteGeneradoPor.eCG_CXC
         Set insCxC = New clsCxCNavigator
         SQL = insCxC.fConstruirSQLDeCxCConComprobante(dtpFechaInicial.value, dtpFechaFinal.value)
         Set insCxC = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_CXP
         Set insCxP = New clsCxPNavigator
         SQL = insCxP.fConstruirSQLDeCxPConComprobante(dtpFechaInicial, dtpFechaFinal)
         Set insCxP = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_COBRANZA
         Set insCobranza = New clsCobranzaNavigator
         SQL = insCobranza.fConstruirSQLDeCobranzaConComprobante(dtpFechaInicial, dtpFechaFinal)
         Set insCobranza = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_FACTURA
         Set insFactura = New clsFacturaNavigator
         SQL = insFactura.fConstruirSQLDeFacturaConComprobante(dtpFechaInicial, dtpFechaFinal, , False)
         Set insFactura = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_MOVIMIENTO_BANCARIO
         Set insMovimientoBancario = New clsMovimientoBancarioNavigator
         SQL = insMovimientoBancario.fConstruirSQLDeMovimientoBancarioConComprobante(dtpFechaInicial.value, dtpFechaFinal.value)
         Set insMovimientoBancario = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_PAGOS
         Set insPago = New clsPagoNavigator
         SQL = insPago.fConstruirSQLDePagoConComprobante(dtpFechaInicial, dtpFechaFinal)
         Set insPago = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_RESUMEN_DIARIO_VENTAS
         Set insFactura = New clsFacturaNavigator
         insFactura.setClaseDeTrabajo eCTFC_Factura
         SQL = insFactura.fConstruirSQLDeFacturaConComprobante(dtpFechaInicial, dtpFechaFinal, , True)
         Set insFactura = Nothing
      Case enum_ComprobanteGeneradoPor.eCG_ANTICIPO
         Set insAnticipo = New clsAnticipoNavigator
         SQL = insAnticipo.fConstruirSQLDeAnticipoConComprobante(dtpFechaInicial, dtpFechaFinal)
         Set insAnticipo = Nothing
   End Select
   fConstruirSQLDelInformesDeDocumentosContabilizados = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelInformesDeDocumentosContabilizados", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConstruirSQLDelInformesDeComprobantesSinDocumento() As String
   Dim SQL As String
   Dim varComprobanteNavigator As clsComprobanteNavigator
   On Error GoTo h_ERROR
   Set varComprobanteNavigator = New clsComprobanteNavigator
   SQL = "SELECT " & varComprobanteNavigator.GetTableName & "." & "GeneradoPor"
   SQL = SQL & " AS TipoDeDocumento, "
   SQL = SQL & varComprobanteNavigator.GetTableName & "." & "FECHA"
   SQL = SQL & " AS FechaCombrobante, "
   SQL = SQL & varComprobanteNavigator.GetTableName & "." & "NUMERO"
   SQL = SQL & " AS NumeroComprobante, "
   SQL = SQL & varComprobanteNavigator.GetTableName & "." & "DESCRIPCION"
   SQL = SQL & " AS DescripcionComprobante, "
   SQL = SQL & varComprobanteNavigator.GetTableName & "." & "TotalHaber"
   SQL = SQL & " AS TotalHaber, "
   SQL = SQL & varComprobanteNavigator.GetTableName & "." & "NoDocumentoOrigen"
   SQL = SQL & " AS NumeroDocumentoOrigen "
   SQL = SQL & " FROM " & varComprobanteNavigator.GetTableName
   SQL = SQL & " WHERE " & "ConsecutivoPeriodo" & " = " & gContPeriodoActual.GetConsecutivoPeriodo
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("FECHA", dtpFechaInicial.value, dtpFechaFinal.value)
   SQL = SQL & " AND " & "GeneradoPor" & " = '" & gConvert.enumerativoAChar(gEnumProyectoWincont.strComprobanteGeneradoPorToNum(gAPI.SelectedElementInComboBoxToString(CmbOpcionTipoDeDocumentoAImprimir), False)) & "'"
   SQL = SQL & " AND (" & "NoDocumentoOrigen" & " = '' " _
         & " OR " & gUtilSQL.DfSQLIsNull("NoDocumentoOrigen") & ")"
   Set varComprobanteNavigator = Nothing
   fConstruirSQLDelInformesDeComprobantesSinDocumento = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelInformesDeComprobantesSinDocumento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sInitDefaultValues()
   On Error GoTo h_ERROR
   gFechasDeLosInformes.sLeeLasFechasDeInformes dtpFechaInicial, dtpFechaFinal, gProyUsuarioActual.GetNombreDelUsuario
   If Not gContPeriodoActual.LaFechaPerteneceAlPeriodoActual(dtpFechaFinal.value) Then
      dtpFechaFinal.value = gContPeriodoActual.GetFechaCierreDelPeriodo
   End If
   If Not gContPeriodoActual.LaFechaPerteneceAlPeriodoActual(dtpFechaInicial.value) Then
      dtpFechaInicial.value = gContPeriodoActual.GetFechaAperturaDelPeriodo
   End If
   gEnumProyectoWincont.FillComboBoxWithComprobanteGeneradoPor CmbOpcionTipoDeDocumentoAImprimir, eCG_CXC
   CmbOpcionTipoDeDocumentoAImprimir.Text = gEnumProyectoWincont.enumComprobanteGeneradoPorToString(eCG_USUARIO)
   CmbOpcionTipoDeDocumentoAImprimir.ListIndex = 0
   optComprobantesDeContabilizacionAutomatica(mInformeDocsSinComp).value = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sInitDefaultValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbOpcionTipoDeDocumentoAImprimir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "cmbOpcionTipoDeDocumentoAImprimir_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbOpcionTipoDeDocumentoAImprimir_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "cmbOpcionTipoDeDocumentoAImprimir_KeyPress()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraInformesDeDocumentosSinComprobantes()
'   Dim MostrarReporte As rptInformeDocumentosSinComp
'   Dim SqlDelReporte As String
'   Dim insNoComun As clsNoComunWincont
'   On Error GoTo h_ERROR
'   sActivarCamposDeInformeDeDocumentosSinComprobante
'   If dtpFechaFinal.value < dtpFechaInicial.value Then
'      dtpFechaFinal.value = dtpFechaInicial.value
'   End If
'   sValidarLaFechaInicial
'   gFechasDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.value, dtpFechaFinal.value, gProyUsuarioActual.GetNombreDelUsuario
'   Set MostrarReporte = New rptInformeDocumentosSinComp
'   Set insNoComun = New clsNoComunWincont
'   SqlDelReporte = insNoComun.fConstruirSQLDelInformesDeDocumentosSinComprobante(gEnumProyectoWincont.strComprobanteGeneradoPorToNum(CmbOpcionTipoDeDocumentoAImprimir.Text), dtpFechaInicial.value, dtpFechaFinal.value)
'   Set insNoComun = Nothing
'   MostrarReporte.sConfigurarDatosDelReporte SqlDelReporte, CmbOpcionTipoDeDocumentoAImprimir.Text, dtpFechaInicial.value, dtpFechaFinal.value
'   gUtilReports.sMostrarOImprimirReporte MostrarReporte, 1, mDondeImprimir, "Documentos sin Comprobantes"
'   Set MostrarReporte = Nothing
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   Dim insNoComun As clsNoComunWincont
   Dim insSawIntegradoDsr As clsSawDsr
   On Error GoTo h_ERROR
   Set insSawIntegradoDsr = New clsSawDsr
   Set insNoComun = New clsNoComunWincont
   sActivarCamposDeInformeDeDocumentosSinComprobante
   If dtpFechaFinal.value < dtpFechaInicial.value Then
      dtpFechaFinal.value = dtpFechaInicial.value
   End If
   sValidarLaFechaInicial
   gFechasDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.value, dtpFechaFinal.value, gProyUsuarioActual.GetNombreDelUsuario
   SqlDelReporte = insNoComun.fConstruirSQLDelInformesDeDocumentosSinComprobante(gEnumProyectoWincont.strComprobanteGeneradoPorToNum(CmbOpcionTipoDeDocumentoAImprimir.Text), dtpFechaInicial.value, dtpFechaFinal.value)
   Set reporte = insSawIntegradoDsr.fConfigurarDsrInformeDocumentosSinComp(SqlDelReporte, CmbOpcionTipoDeDocumentoAImprimir.Text, dtpFechaInicial.value, dtpFechaFinal.value, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gEnumProyectoWincont, gEnumProyecto)
   gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Documentos sin Comprobantes"
   Set reporte = Nothing
   Set insNoComun = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sMuestraInformesDeDocumentosSinComprobantes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraInformesDeDocumentosContabilizados()
'   Dim MostrarReporte As rptInformeDeDocumentosContabilizados
'   Dim SqlDelReporte As String
'   On Error GoTo h_ERROR
'   sActivarCamposDeInformeDeDocumentosContabilizados
'   If dtpFechaFinal.value < dtpFechaInicial.value Then
'      dtpFechaFinal.value = dtpFechaInicial.value
'   End If
'   sValidarLaFechaInicial
'   gFechasDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.value, dtpFechaFinal.value, gProyUsuarioActual.GetNombreDelUsuario
'   Set MostrarReporte = New rptInformeDeDocumentosContabilizados
'   SqlDelReporte = fConstruirSQLDelInformesDeDocumentosContabilizados
'   MostrarReporte.sConfigurarDatosDelReporte SqlDelReporte, CmbOpcionTipoDeDocumentoAImprimir.Text, dtpFechaInicial.value, dtpFechaFinal.value
'   gUtilReports.sMostrarOImprimirReporte MostrarReporte, 1, mDondeImprimir, "Documentos Contabilizados"
'   Set MostrarReporte = Nothing
   Dim reporte As DDActiveReports2.ActiveReport
   Dim SqlDelReporte As String
   Dim insSawIntegrado As clsSawDsr
   On Error GoTo h_ERROR
   Set insSawIntegrado = New clsSawDsr
   Set reporte = New DDActiveReports2.ActiveReport
   sActivarCamposDeInformeDeDocumentosContabilizados
   If dtpFechaFinal.value < dtpFechaInicial.value Then
      dtpFechaFinal.value = dtpFechaInicial.value
   End If
   sValidarLaFechaInicial
   gFechasDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.value, dtpFechaFinal.value, gProyUsuarioActual.GetNombreDelUsuario
   SqlDelReporte = fConstruirSQLDelInformesDeDocumentosContabilizados
   Debug.Print "Llegue"
   Set reporte = insSawIntegrado.fConfigurarDsrInformeDeDocumentosContabilizados(SqlDelReporte, CmbOpcionTipoDeDocumentoAImprimir.Text, dtpFechaInicial.value, dtpFechaFinal.value, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gEnumProyectoWincont.strComprobanteGeneradoPorToNum(CmbOpcionTipoDeDocumentoAImprimir.Text), gEnumProyectoWincont, gEnumProyecto)
   gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Documentos Contabilizados"
   Set reporte = Nothing
   Set insSawIntegrado = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sMuestraInformesDeDocumentosContabilizados", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sMuestraInformesDeComprobantesSinDocumentos()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insSawDsr As clsSawDsr
   Dim SqlDelReporte As String
   On Error GoTo h_ERROR
   Set insSawDsr = New clsSawDsr
   sActivarCamposDeInformeDeDocumentosContabilizados
   If dtpFechaFinal.value < dtpFechaInicial.value Then
      dtpFechaFinal.value = dtpFechaInicial.value
   End If
   sValidarLaFechaInicial
   gFechasDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.value, dtpFechaFinal.value, gProyUsuarioActual.GetNombreDelUsuario
   SqlDelReporte = fConstruirSQLDelInformesDeComprobantesSinDocumento
   Set reporte = insSawDsr.fConfigurarDsrInformeDeCompSinDocOrigen(SqlDelReporte, CmbOpcionTipoDeDocumentoAImprimir.Text, dtpFechaInicial.value, dtpFechaFinal.value, gProyCompaniaActual.GetNombreCompaniaParaInformes(False))
   gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Comprobantes sin Documento de Origen"
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sMuestraInformesDeDocumentosContabilizados", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultaCampos()
   On Error GoTo h_ERROR
   CmbOpcionTipoDeDocumentoAImprimir.Visible = False
   dtpFechaFinal.Visible = False
   dtpFechaInicial.Visible = False
   lblAnchoPapelImpresora.Visible = False
   lblCantidadDeColumnas.Visible = False
   lblFechaFinal.Visible = False
   lblFechaInicial.Visible = False
   lblOpcionTipoDeDocumentoAImprimir.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sOcultaCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeComprobantesContabilizadosSinDocumento()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Comprobantes Sin Documento Origen"
   lblOpcionTipoDeDocumentoAImprimir.Visible = True
   CmbOpcionTipoDeDocumentoAImprimir.Visible = True
   lblFechaFinal.Visible = True
   dtpFechaFinal.Visible = True
   lblFechaInicial.Visible = True
   dtpFechaInicial.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "sActivarCamposDeComprobantesContabilizadosSinDocumento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optComprobantesDeContabilizacionAutomatica_Click(Index As Integer)
   On Error GoTo h_ERROR
   mInformeSeleccionado = Index
   Select Case mInformeSeleccionado
      Case mInformeDocsSinComp: sActivarCamposDeInformeDeDocumentosSinComprobante
      Case mInformeDocsContabilizados: sActivarCamposDeInformeDeDocumentosContabilizados
      Case mInformeCompSinDocOrigen: sActivarCamposDeComprobantesContabilizadosSinDocumento
      Case mInformeComprobantesModificados: sActivarCamposDeComprobantesModificados
      Case mInformeCompConNroDocOrigenDuplicado: sActivarCamposDeComprobantesConNroDocOrigenDuplicado
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "optComprobantesDeContabilizacionAutomatica_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeComprobantesModificados()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Listado de Comprobantes Modificados"
   sOcultaCampos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "sActivarCamposDeComprobantesModificados", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElInformeDeComprobantesModificados()
   Dim varReporte As DDActiveReports2.ActiveReport
   Dim varComprobanteConfigurar As clsRptComprobanteConfigurar
   Dim varSQLDelReporte As String
   On Error GoTo h_ERROR
   Set varReporte = New ActiveReport
   varSQLDelReporte = fSQLInformeDeComprobantesModificados
   Set varComprobanteConfigurar = New clsRptComprobanteConfigurar
   If varComprobanteConfigurar.fConfiguraElInformeDeComprobantesAutomaticosModificados(varReporte, varSQLDelReporte, _
         gContPeriodoActual.GetFechaAperturaDelPeriodo, gContPeriodoActual.GetFechaCierreDelPeriodo) Then
      gUtilReports.sMostrarOImprimirReporte varReporte, 1, mDondeImprimir, _
         varComprobanteConfigurar.fNombreDelReporteDeComprobantes(varComprobanteConfigurar.CM_RPT_COMPROBANTE_AUTOMATICOS)
   End If
   Set varReporte = Nothing
   Set varComprobanteConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sEjecutaElInformeDeComprobantesModificados", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSQLInformeDeComprobantesModificados() As String
   Dim varSQL As String
   Dim varComprobanteNavigator As clsComprobanteNavigator
   On Error GoTo h_ERROR
   Set varComprobanteNavigator = New clsComprobanteNavigator
   varSQL = "SELECT " & "NUMERO" & ", " & "GeneradoPor" & ", " & "NoDocumentoOrigen" & ", " & "NombreOperador" & ", " & "FechaUltimaModificacion" _
         & " FROM " & varComprobanteNavigator.GetTableName _
         & " WHERE " & "FueModificado" & " = '" & gConvert.ConvertBooleanToString(True) & "'" _
         & " AND " & "ConsecutivoPeriodo" & " = " & gContPeriodoActual.GetConsecutivoPeriodo
   fSQLInformeDeComprobantesModificados = varSQL
   Set varComprobanteNavigator = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLInformeDeComprobantesModificados", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sValidarLaFechaInicial()
   On Error GoTo h_ERROR
   If gUtilDate.fF1IsLessOrEqualToF2(dtpFechaInicial.value, gProyParametrosCompania.GetFechaDeInicioContabilizacion) Then
      dtpFechaInicial.value = gProyParametrosCompania.GetFechaDeInicioContabilizacion
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sValidarLaFechaInicial", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeComprobantesConNroDocOrigenDuplicado()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Comprobantes con Nro. Documento Origen Duplicados"
   sOcultaCampos
   lblOpcionTipoDeDocumentoAImprimir.Visible = True
   CmbOpcionTipoDeDocumentoAImprimir.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "sActivarCamposDeComprobantesConNroDocOrigenDuplicado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElInformeDeComprobantesConNroDocOrigenDuplicado()
   Dim varSQLDelReporte As String
   Dim varAsientoNavigator As clsAsientoNavigator
   Dim varNombreReporte As String
   Dim varReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsRptInformesVariosWco
   Dim insCuenta As clsCuentaNavigator
   Dim insAuxiliar As clsAuxiliarNavigator
   Dim insCentroDeCostos As clsCentroDeCostosNavigator
   On Error GoTo h_ERROR
   Set varReporte = New ActiveReport
   Set insConfigurar = New clsRptInformesVariosWco
   Set varAsientoNavigator = New clsAsientoNavigator
   Set insCuenta = New clsCuentaNavigator
   Set insAuxiliar = New clsAuxiliarNavigator
   Set insCentroDeCostos = New clsCentroDeCostosNavigator
   varSQLDelReporte = fSQLInformeDeComprobantesConNroDocOrigenDuplicado
   If varSQLDelReporte <> "" Then
      varNombreReporte = "Comprobantes de " & gAPI.SelectedElementInComboBoxToString(CmbOpcionTipoDeDocumentoAImprimir) & " con Nro. Doc. Origen Duplicados"
   If insConfigurar.fConfigurarDatosDelReporteDiarioDeComprobante(varReporte, varSQLDelReporte, False, True, True, False, insCuenta, insAuxiliar, insCentroDeCostos, gProyParametrosWinCont, _
                           gProyCompaniaActual, gContPeriodoActual, gEnumProyectoWincont, gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision) Then
            gUtilReports.sMostrarOImprimirReporte varReporte, 1, mDondeImprimir, varNombreReporte
         End If
   Else
      gMessage.Advertencia "No se encontró información para imprimir."
   End If
      Set varReporte = Nothing
      Set varAsientoNavigator = Nothing
      Set varReporte = Nothing
      Set insConfigurar = Nothing
      Set insCuenta = Nothing
      Set insAuxiliar = Nothing
      Set insCentroDeCostos = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sEjecutaElInformeDeComprobantesConNroDocOrigenDuplicado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSQLInformeDeComprobantesConNroDocOrigenDuplicado() As String
   Dim varSQL As String
   Dim varComprobanteNavigator As clsComprobanteNavigator
   Dim varConjuntoNrosDocDuplicados As String
   On Error GoTo h_ERROR
   Set varComprobanteNavigator = New clsComprobanteNavigator
   varSQL = "SELECT " & "NoDocumentoOrigen" _
         & " FROM " & varComprobanteNavigator.GetTableName _
         & " WHERE " & "GeneradoPor" & " = '" & gConvert.enumerativoAChar(gEnumProyectoWincont.strComprobanteGeneradoPorToNum(gAPI.SelectedElementInComboBoxToString(CmbOpcionTipoDeDocumentoAImprimir))) & "'" _
         & " AND " & "ConsecutivoPeriodo" & " = " & gContPeriodoActual.GetConsecutivoPeriodo _
         & " GROUP BY " & "NoDocumentoOrigen" _
         & " HAVING COUNT(" & "NoDocumentoOrigen" & ") > 1 "
   varConjuntoNrosDocDuplicados = gDbUtil.fBuildResultSetAsString(varSQL)
   If varConjuntoNrosDocDuplicados <> "''" Then
      varSQL = " SELECT " & "NUMERO" & " AS NroComprobante, " _
            & "STATUS" & " AS StatusComprobante, " _
            & "FECHA" & " AS FechaComprobante, " _
            & "DESCRIPCION" & " AS DescripcionComprobante, " _
            & "TotalDebe" & " AS TotalMontoDebe, " _
            & "TotalHaber" & " AS TotalMontoHaber, " _
            & "NoDocumentoOrigen" & " AS DocumentoOrigen " _
            & " FROM " & varComprobanteNavigator.GetTableName _
            & " WHERE " & "GeneradoPor" & " = '" & gConvert.enumerativoAChar(gEnumProyectoWincont.strComprobanteGeneradoPorToNum(gAPI.SelectedElementInComboBoxToString(CmbOpcionTipoDeDocumentoAImprimir))) & "'" _
            & " AND " & "ConsecutivoPeriodo" & " = " & gContPeriodoActual.GetConsecutivoPeriodo _
            & " AND " & "NoDocumentoOrigen" & " IN (" & varConjuntoNrosDocDuplicados & ")" _
            & " ORDER BY " & "NoDocumentoOrigen" & ", " & "NUMERO"
   End If
   fSQLInformeDeComprobantesConNroDocOrigenDuplicado = varSQL
   Set varComprobanteNavigator = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLInformeDeComprobantesConNroDocOrigenDuplicado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

