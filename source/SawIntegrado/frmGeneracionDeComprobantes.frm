VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCT2.OCX"
Begin VB.Form frmGeneracionDeComprobantes 
   BackColor       =   &H00F3F3F3&
   BorderStyle     =   3  'Fixed Dialog
   ClientHeight    =   2205
   ClientLeft      =   45
   ClientTop       =   60
   ClientWidth     =   7875
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   LinkTopic       =   "Generacion De Contratos"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2205
   ScaleWidth      =   7875
   ShowInTaskbar   =   0   'False
   Visible         =   0   'False
   Begin VB.Frame FrmRangoDeMeses 
      Caption         =   "Rango de fechas de los comprobantes a borrar"
      Height          =   1095
      Left            =   1800
      TabIndex        =   7
      Top             =   480
      Visible         =   0   'False
      Width           =   3615
      Begin MSComCtl2.DTPicker dtMesAnoABorrarHasta 
         Height          =   285
         Left            =   1800
         TabIndex        =   8
         Top             =   600
         Width           =   1335
         _ExtentX        =   2355
         _ExtentY        =   503
         _Version        =   393216
         CustomFormat    =   "MM/yyyy"
         Format          =   89128963
         UpDown          =   -1  'True
         CurrentDate     =   36945
      End
      Begin MSComCtl2.DTPicker dtMesAnoABorrarDesde 
         Height          =   285
         Left            =   1800
         TabIndex        =   11
         Top             =   240
         Width           =   1335
         _ExtentX        =   2355
         _ExtentY        =   503
         _Version        =   393216
         CustomFormat    =   "MM/yyyy"
         Format          =   89128963
         UpDown          =   -1  'True
         CurrentDate     =   36945
      End
      Begin VB.Label Label2 
         BackColor       =   &H80000016&
         BackStyle       =   0  'Transparent
         Caption         =   "Mes Desde: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   255
         Left            =   240
         TabIndex        =   10
         Top             =   240
         Width           =   1380
      End
      Begin VB.Label Label1 
         BackColor       =   &H80000016&
         BackStyle       =   0  'Transparent
         Caption         =   "Mes Hasta: "
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00000000&
         Height          =   255
         Left            =   240
         TabIndex        =   9
         Top             =   600
         Width           =   1425
      End
   End
   Begin VB.CommandButton cmdSalir 
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   6120
      TabIndex        =   1
      Top             =   1680
      Width           =   1335
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "&Generar"
      Height          =   375
      Left            =   4200
      TabIndex        =   0
      Top             =   1680
      Width           =   1335
   End
   Begin MSComCtl2.DTPicker dtMesAnoAcontabilizar 
      Height          =   285
      Left            =   3840
      TabIndex        =   2
      Top             =   840
      Width           =   1335
      _ExtentX        =   2355
      _ExtentY        =   503
      _Version        =   393216
      CustomFormat    =   "MM/yyyy"
      Format          =   89128963
      UpDown          =   -1  'True
      CurrentDate     =   36945
   End
   Begin MSComCtl2.DTPicker dtDiaAcontabilizar 
      Height          =   285
      Left            =   3840
      TabIndex        =   4
      Top             =   1200
      Visible         =   0   'False
      Width           =   1335
      _ExtentX        =   2355
      _ExtentY        =   503
      _Version        =   393216
      CustomFormat    =   "dd/MM/yyyy"
      Format          =   89128963
      CurrentDate     =   36945
   End
   Begin VB.Label lblTitulo 
      Alignment       =   2  'Center
      BackColor       =   &H00A86602&
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FFFFFF&
      Height          =   345
      Left            =   0
      TabIndex        =   6
      Top             =   5
      Width           =   7815
   End
   Begin VB.Label lblDiaAcontabilizar 
      BackColor       =   &H80000016&
      BackStyle       =   0  'Transparent
      Caption         =   "Dia a Contabilizar"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   195
      Left            =   2040
      TabIndex        =   5
      Top             =   1245
      Visible         =   0   'False
      Width           =   1740
   End
   Begin VB.Label lblMesAnoAcontabilizar 
      BackColor       =   &H80000016&
      BackStyle       =   0  'Transparent
      Caption         =   "Mes a Contabilizar: "
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   255
      Left            =   2040
      TabIndex        =   3
      Top             =   885
      Width           =   1740
   End
End
Attribute VB_Name = "frmGeneracionDeComprobantes"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "frmGeneracionDeComprobantes"
Private Const CM_MESSAGE_NAME As String = "Generación De Comprobantes"
Private Declare Function GetKeyState Lib "user32" (ByVal nVirtKey As Long) As Integer
Private mAction As AccionSobreRecord
Private mContabilizarPorDia As Boolean
Private mTipoDeContabilizacion As enum_ComprobanteGeneradoPor
Private mTipoDocumento As Integer
Private mFechaDesde As Date
Private mFechaHasta As Date
Private mConsecutivo As Long

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Sub SetAction(ByVal valActionValue As AccionSobreRecord)
   On Error GoTo h_ERROR
   mAction = valActionValue
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "SetAction", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub SetTipoDeContabilizacion(ByVal valTipoDeContabilizacionValue As enum_ComprobanteGeneradoPor)
   On Error GoTo h_ERROR
   mTipoDeContabilizacion = valTipoDeContabilizacionValue
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "SetTipoDeContabilizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCheckForSpecialKeys(ByVal valKeyCode As Integer, ByVal valShift As Integer)
   Dim otherUserUdatedFirst As Boolean
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, valKeyCode) Then
   ElseIf valKeyCode = vbKeyF6 Then
      gAPI.ssSetFocus cmdGrabar
   ElseIf valKeyCode = vbKeyEscape Then
      gAPI.ssSetFocus cmdSalir
      Unload Me
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCheckForSpecialKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sRunProcessInLostFocus()
   On Error GoTo h_ERROR
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "RunProcessLostFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_Click()
   Dim Continuar As Boolean
   On Error GoTo h_ERROR
  Continuar = True
   If (mAction = Eliminar) And (mTipoDeContabilizacion = eCG_INVENTARIO) Then
       sEjecutaBorradoDeLosComprobantesCostoVenta
       Continuar = False
  ElseIf Not gProyReglasDeContabilizacion.fLasCuentasDeReglasDeContabilizacionEstanCompletas(mTipoDeContabilizacion, True, getTipoDocumento) Then
      Continuar = False
   End If
   If Not Continuar Then
      cmdSalir_Click
   ElseIf Not fElDiaOMesYaEstaContabilizado(True) Then
      If (mTipoDeContabilizacion = eCG_INVENTARIO) Then
          If (fContinuarApesarQueElMesEstaEnCurso()) Then
             If fCalculaFechaAContabilizar(True) Then
               sEjecutaProcesosDeContabilizacion
             End If
          End If
      Else
        sEjecutaProcesosDeContabilizacion
     End If
      
      Unload Me
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdGrabar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
   Unload Me
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

Private Sub sRellenaElLabelDeTitulo()
   Dim Titulo As String
   On Error GoTo h_ERROR
   Titulo = "Contabilización de "
   Select Case mTipoDeContabilizacion
      Case eCG_CXC: Titulo = Titulo & " CxC "
      Case eCG_FACTURA: Titulo = Titulo & " Facturas "
      Case eCG_CXP: Titulo = Titulo & " CxP "
      Case eCG_COBRANZA: Titulo = Titulo & " Cobranzas "
      Case eCG_PAGOS: Titulo = Titulo & " Pagos "
      Case eCG_MOVIMIENTO_BANCARIO: Titulo = Titulo & " Movimientos Bancarios "
      Case eCG_RESUMEN_DIARIO_VENTAS: Titulo = Titulo & " Resumen de Ventas "
      Case eCG_ANTICIPO
         If getTipoDocumento = enum_TipoDeAnticipo.eTDA_COBRADO Then
            Titulo = Titulo & " Anticipo - Cobrado "
         ElseIf getTipoDocumento = enum_TipoDeAnticipo.eTDA_PAGADO Then
            Titulo = Titulo & " Anticipo - Pagado "
         End If
   End Select
   Titulo = Titulo & "por lote"
   If mContabilizarPorDia Then
      Titulo = Titulo & " resumidos por día"
   Else
      Titulo = Titulo & " resumidos por mes"
   End If
   If (mTipoDeContabilizacion = eCG_INVENTARIO) Then
    Titulo = " Contabilización de Costo de Inventario "
   End If
   lblTitulo.Caption = Titulo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sRellenaElLabelDeTitulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInitLookAndFeelAndSetValues(ByVal valAction As AccionSobreRecord, ByVal valTipoDeContabilizacion As enum_ComprobanteGeneradoPor, ByVal valTipoDocumento As Integer)
   On Error GoTo h_ERROR
   SetAction valAction
   SetTipoDeContabilizacion valTipoDeContabilizacion
   SetTipoDeDocumento valTipoDocumento
   sInitDefaultValues
   lblDiaAcontabilizar.Top = lblMesAnoAcontabilizar.Top
   dtDiaAcontabilizar.Top = dtMesAnoAcontabilizar.Top
   If (mTipoDeContabilizacion = eCG_INVENTARIO) Then
      FrmRangoDeMeses.Visible = False
      If (valAction = Eliminar) Then
         cmdGrabar.Caption = "Borrar"
         sCalculaFechaMaxContabilizado
         dtMesAnoABorrarHasta.Enabled = False
         FrmRangoDeMeses.Visible = True
      Else
        If fCalculaFechaAContabilizar(False) Then
        End If
        dtMesAnoAcontabilizar.Enabled = False
      End If
   End If
   If mContabilizarPorDia Then
      lblMesAnoAcontabilizar.Visible = False
      dtMesAnoAcontabilizar.Visible = False
      lblDiaAcontabilizar.Visible = True
      dtDiaAcontabilizar.Visible = True
   End If
   sRellenaElLabelDeTitulo
   
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sInitLookAndFeelAndSetValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitLookAndFeel()
   On Error GoTo h_ERROR
   Me.Caption = CM_MESSAGE_NAME & " - " & gDefgen.AccionSobreRecordStr(mAction)
   cmdGrabar.ToolTipText = gMessage.getToolTipGrabarF6(mAction)
   cmdSalir.ToolTipText = gMessage.getToolTipSalirEscape(mAction)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtMesAnoABorrarDesde_Validate(Cancel As Boolean)
  On Error GoTo h_ERROR
   If gProyCompaniaActual.GetTieneAccesoACaracteristicaContabilidadActiva And gProyUsuarioActual.GetUsaContabilidad Then
      If gContPeriodoActual.fLaFechaPerteneceAunMesCerrado(dtMesAnoABorrarDesde, False) Then
         gMessage.Advertencia "Fecha no Válida, pertenece a un Mes Cerrado "
         gAPI.ssSetFocus dtMesAnoABorrarDesde
         GoTo h_EXIT
      ElseIf Not (gUtilDate.fF1IsBetweenF2AndF3(dtMesAnoABorrarDesde.value, gProyParametrosCompania.GetFechaDesdeUsoMetodoDeCosteo, dtMesAnoABorrarHasta)) Then
         gAPI.ssSetFocus dtMesAnoABorrarDesde
         Cancel = True
         GoTo h_EXIT
      End If
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Cancel = True
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "dtMesAnoABorrarDesde", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   sInitDefaultValues
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Terminate()
   On Error GoTo h_ERROR
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Form_Terminate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtMesAnoAcontabilizar_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "dtMesAnoAcontabilizar_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtMesAnoAcontabilizar_LostFocus()
   On Error GoTo h_ERROR
   sRunProcessInLostFocus
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "dtMesAnoAcontabilizar_LostFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitDefaultValues()
   On Error GoTo h_ERROR
   dtMesAnoAcontabilizar.value = gUtilDate.getFechaDeHoy
   dtDiaAcontabilizar.value = gUtilDate.getFechaDeHoy
   mContabilizarPorDia = False
   Select Case mTipoDeContabilizacion
      Case eCG_CXC:
         If gProyReglasDeContabilizacion.GetTipoContabilizacionCxCAsEnum = eTD_POR_LOTE And _
            gProyReglasDeContabilizacion.GetContabPorLoteCxCAsEnum = eCP_DIARIA Then
            mContabilizarPorDia = True
         End If
      Case eCG_FACTURA:
         If gProyReglasDeContabilizacion.GetTipoContabilizacionFacturacionAsEnum = eTD_POR_LOTE And _
            gProyReglasDeContabilizacion.GetContabPorLoteFacturacionAsEnum = eCP_DIARIA Then
            mContabilizarPorDia = True
         End If
      Case eCG_CXP:
         If gProyReglasDeContabilizacion.GetTipoContabilizacionCxPAsEnum = eTD_POR_LOTE And _
            gProyReglasDeContabilizacion.GetContabPorLoteCxPAsEnum = eCP_DIARIA Then
            mContabilizarPorDia = True
         End If
      Case eCG_COBRANZA:
         If gProyReglasDeContabilizacion.GetTipoContabilizacionCobranzaAsEnum = eTD_POR_LOTE And _
            gProyReglasDeContabilizacion.GetContabPorLoteCobranzaAsEnum = eCP_DIARIA Then
            mContabilizarPorDia = True
         End If
      Case eCG_PAGOS:
         If gProyReglasDeContabilizacion.GetTipoContabilizacionPagosAsEnum = eTD_POR_LOTE And _
            gProyReglasDeContabilizacion.GetContabPorLotePagosAsEnum = eCP_DIARIA Then
            mContabilizarPorDia = True
         End If
      Case eCG_MOVIMIENTO_BANCARIO:
         If gProyReglasDeContabilizacion.GetTipoContabilizacionMovBancarioAsEnum = eTD_POR_LOTE And _
            gProyReglasDeContabilizacion.GetContabPorLoteMovBancarioAsEnum = eCP_DIARIA Then
            mContabilizarPorDia = True
         End If
      Case eCG_RESUMEN_DIARIO_VENTAS:
         If gProyReglasDeContabilizacion.GetTipoContabilizacionRDVtasAsEnum = eTD_POR_LOTE And _
            gProyReglasDeContabilizacion.GetContabPorLoteRDVtasAsEnum = eCP_DIARIA Then
            mContabilizarPorDia = True
         End If
      Case eCG_ANTICIPO:
         If gProyReglasDeContabilizacion.GetTipoContabilizacionAnticipoAsEnum = eTD_POR_LOTE And _
            gProyReglasDeContabilizacion.GetContabPorLoteAnticipoAsEnum = eCP_DIARIA Then
            mContabilizarPorDia = True
         End If
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "InitDefaltValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtDiaAcontabilizar_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "dtDiaAcontabilizar_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtDiaAcontabilizar_LostFocus()
   On Error GoTo h_ERROR
   sRunProcessInLostFocus
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "dtDiaAcontabilizar_LostFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtDiaAcontabilizar_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If gProyCompaniaActual.GetTieneAccesoACaracteristicaContabilidadActiva _
         And gProyUsuarioActual.GetUsaContabilidad Then
      If gContPeriodoActual.fLaFechaPerteneceAunMesCerrado(dtDiaAcontabilizar, False) Then
         gMessage.Advertencia "Fecha no Válida, pertenece a un Mes Cerrado "
         dtDiaAcontabilizar.SetFocus
         GoTo h_EXIT
      End If
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Cancel = True
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "dtDiaAcontabilizar_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fElDiaOMesYaEstaContabilizado(ByVal ShowMessage As Boolean) As Boolean
   Dim FechaBuscada As Date
   Dim NumeroBuscado As String
   Dim EstaContabilizado As Boolean
   Dim insComprobanteNavigator As clsComprobanteNavigator
   On Error GoTo h_ERROR
   Set insComprobanteNavigator = New clsComprobanteNavigator
   EstaContabilizado = False
   If mContabilizarPorDia Then
      FechaBuscada = dtDiaAcontabilizar
   Else
      FechaBuscada = gUtilDate.LastDayOfTheMonthAsDate(dtMesAnoAcontabilizar)
   End If
   NumeroBuscado = fArmaElNumeroDelComprobanteBuscado
   If insComprobanteNavigator.fElComprobanteDeContabilizacionAutomaticaYaExiste(mTipoDeContabilizacion, FechaBuscada, NumeroBuscado, True, 0, True) Then 'para revision(ConsecutivoDocumento para Comprobante -> 0,True)
      EstaContabilizado = True
   End If
h_EXIT:
   Set insComprobanteNavigator = Nothing
   fElDiaOMesYaEstaContabilizado = EstaContabilizado
   On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fElDiaOMesYaEstaContabilizado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
      
Private Function fArmaElNumeroDelComprobanteBuscado() As String
   Dim varNumeroBuscado As String
   On Error GoTo h_ERROR
   varNumeroBuscado = ""
   If mContabilizarPorDia Then
      varNumeroBuscado = gConvert.fDateToKeyOrder(dtDiaAcontabilizar)
   Else
      varNumeroBuscado = gTexto.DfMid(gConvert.fDateToKeyOrder(dtMesAnoAcontabilizar), 1, 6)
   End If
h_EXIT:
   fArmaElNumeroDelComprobanteBuscado = varNumeroBuscado
   On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fArmaElNumeroDelComprobanteBuscado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
      
Private Sub sEjecutaProcesosDeContabilizacion()
   Dim insGenerarComprobanteNavigator As clsGenerarComprobanteNavigator
   Dim tipoDocumento As Integer
   On Error GoTo h_ERROR
   Set insGenerarComprobanteNavigator = New clsGenerarComprobanteNavigator
   sCalculaFechaDesdeYFechaHasta
   If mTipoDeContabilizacion = eCG_RESUMEN_DIARIO_VENTAS Then
      tipoDocumento = enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS
   ElseIf mTipoDeContabilizacion = eCG_ANTICIPO Then
      tipoDocumento = getTipoDocumento
   Else
      tipoDocumento = -1
   End If
   If insGenerarComprobanteNavigator.fGeneraElComprobanteParaUnoOVariosDocumentos(mFechaDesde, mFechaHasta, mTipoDeContabilizacion, False, "", False, False, False, tipoDocumento, 0, eTAPCIGTF_NoAsignado) Then
   End If
   Set insGenerarComprobanteNavigator = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sEjecutaProcesosDeContabilizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCalculaFechaDesdeYFechaHasta()
   On Error GoTo h_ERROR
   If mContabilizarPorDia Then
      mFechaDesde = dtDiaAcontabilizar
      mFechaHasta = dtDiaAcontabilizar
   Else
      mFechaDesde = gUtilDate.fColocaDiaEnFecha(1, dtMesAnoAcontabilizar)
      mFechaHasta = gUtilDate.fColocaUltimoDiaDelMes(dtMesAnoAcontabilizar)
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCalculaFechaDesdeYFechaHasta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub SetTipoDeDocumento(ByVal valTipoDocumentoValue As Integer)
   On Error GoTo h_ERROR
   mTipoDocumento = valTipoDocumentoValue
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "SetTipoDeContabilizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function getTipoDocumento() As Integer
   getTipoDocumento = mTipoDocumento
End Function
Private Function fCalculaFechaAContabilizar(ByVal valVieneDeGrabar As Boolean) As Boolean
 Dim insComprobanteSQL As clsComprobanteSQL
 Dim insperiodo As clsPeriodoNavigator
 Dim vTemp As String
 Dim vResult As Boolean
 Dim vRegistros As Long
 On Error GoTo h_ERROR
    Set insComprobanteSQL = New clsComprobanteSQL
    Set insperiodo = New clsPeriodoNavigator
    fCalculaFechaAContabilizar = True
   vTemp = gDbUtil.fBuildResultSetAsString(insComprobanteSQL.fSQLFechaAContabilizar(gProyCompaniaActual.GetConsecutivoCompania, gContPeriodoActual.GetFechaCierreDelPeriodo, gProyParametrosCompania.GetFechaContabilizacionDeCosteo))
   vTemp = gTexto.fCleanTextOfInvalidChars(vTemp, "'")
   If insperiodo.fBuscaElPeriodoDeFecha(vTemp, False) Then
      If valVieneDeGrabar Then
            If insperiodo.GetConsecutivoPeriodo = gContPeriodoActual.GetConsecutivoPeriodo Then
                If Not insperiodo.fLaFechaPerteneceAunMesCerrado(vTemp, False) Then
                    If Not insperiodo.fLaFechaPerteneceAUnPeriodoCerrado(vTemp, False) Then
                        dtMesAnoAcontabilizar.value = gConvert.fConvertStringToDate(gTexto.fCleanTextOfInvalidChars(vTemp, "'"), True)
                    Else
                    gMessage.Advertencia "La fecha siguiente de contabilización de costos pertenece a " & _
                                         "un perido cerrado. Aperture el mes antes de realizar el proceso"
                    fCalculaFechaAContabilizar = False
                    End If
                Else
                    gMessage.Advertencia "La fecha siguiente de contabilización de costos pertenece a " & _
                                         "un mes cerrado. Aperture el mes antes de realizar el proceso"
                    fCalculaFechaAContabilizar = False
                End If
            Else
                gMessage.Advertencia "No es posible generar comprobantes costos en este periodo. Verifique los parametros " & _
                                       " de contabilización de costos"
                fCalculaFechaAContabilizar = False
            End If
         Else
            dtMesAnoAcontabilizar.value = gConvert.fConvertStringToDate(gTexto.fCleanTextOfInvalidChars(vTemp, "'"), True)
      End If
    ElseIf valVieneDeGrabar Then
         fCalculaFechaAContabilizar = False
                gMessage.Advertencia "No es posible generar comprobantes costos en este periodo, ya que no existe un periodo valido. Verifique los parametros " & _
                                  " de contabilización de costos"

    End If
   Set insComprobanteSQL = Nothing
   Set insperiodo = Nothing
  Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCalculaFechaAContabilizar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Sub sCalculaFechaMaxContabilizado()
 Dim insComprobanteSQL As clsComprobanteSQL
 Dim vTemp As String
 Dim vResult As Boolean
 Dim vRegistros As Long
 On Error GoTo h_ERROR
   Set insComprobanteSQL = New clsComprobanteSQL
   vTemp = gDbUtil.fBuildResultSetAsString(insComprobanteSQL.fSQLFechaMaxComprobanteDelPeriodoInventario(gContPeriodoActual.GetConsecutivoPeriodo))
   dtMesAnoABorrarHasta.value = gConvert.fConvertStringToDate(gTexto.fCleanTextOfInvalidChars(vTemp, "'"), True)
   dtMesAnoABorrarDesde.value = dtMesAnoABorrarHasta.value
   Set insComprobanteSQL = Nothing
  Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCalculaFechaAContabilizar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sEjecutaBorradoDeLosComprobantesCostoVenta()
 Dim insComprobanteSQL As clsComprobanteSQL
 Dim vTemp As String
 Dim vResult As Boolean
 Dim vRegistros As Long
 On Error GoTo h_ERROR
   If (gContPeriodoActual.fLaFechaPerteneceAunMesCerrado(dtMesAnoABorrarDesde, False)) Then
   gMessage.Advertencia "Fecha no Válida, pertenece a un Mes Cerrado "
Else
    Set insComprobanteSQL = New clsComprobanteSQL
    gDbUtil.Execute gDefDatabase.Conexion, insComprobanteSQL.fSQLDeleteComprobantesDeCostoDeInventarioEntreFechas(gContPeriodoActual.GetConsecutivoPeriodo, dtMesAnoABorrarDesde.value, dtMesAnoABorrarHasta.value)
   Set insComprobanteSQL = Nothing
End If
  Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCalculaFechaAContabilizar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Function fContinuarApesarQueElMesEstaEnCurso() As Boolean
Dim vResult As Boolean
Dim vFechaHoy As Date
 On Error GoTo h_ERROR
 vResult = False
 vFechaHoy = gUtilDate.getFechaDeHoy
   If ((dtMesAnoAcontabilizar.Year = gUtilDate.fYear(vFechaHoy)) And (dtMesAnoAcontabilizar.Month = gUtilDate.fMonth(vFechaHoy))) Then
     If (gUtilDate.fF1IsLessThanF2(dtMesAnoAcontabilizar.value, gUtilDate.fColocaUltimoDiaDelMes(vFechaHoy))) Then
       If (gMessage.Confirm("El  comprobante que esta por generar pertenece  al mes en curso, por lo que una vez generado solo podrá ejecutar operaciones con fecha de los próximos meses. ¿Desea continuar?")) Then
          vResult = True
       End If
     End If
   Else
     vResult = True
   End If
   fContinuarApesarQueElMesEstaEnCurso = vResult
 Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fContinuarApesarQueElMesEstaEnCurso", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

