VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomct2.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.2#0"; "MSCOMCTL.OCX"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Object = "{9F849E84-0608-4BCD-A2B4-8EC557266A4F}#11.0#0"; "GSTextBox.ocx"
Begin VB.Form frmExportacionBanesco 
   BackColor       =   &H00F3F3F3&
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Exportar"
   ClientHeight    =   4350
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   9225
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   ScaleHeight     =   4350
   ScaleWidth      =   9225
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox txtNumeroDeReferencia 
      Height          =   285
      Left            =   1920
      MaxLength       =   8
      TabIndex        =   6
      Top             =   2130
      Width           =   1830
   End
   Begin VB.TextBox txtCodigoCuentaBancaria 
      Height          =   285
      Left            =   1680
      TabIndex        =   1
      Top             =   435
      Width           =   840
   End
   Begin VB.TextBox txtProgreso 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   255
      Left            =   240
      TabIndex        =   14
      TabStop         =   0   'False
      Top             =   3135
      Width           =   3015
   End
   Begin MSComctlLib.ProgressBar prgBarExportarDatos 
      Height          =   315
      Left            =   240
      TabIndex        =   12
      Top             =   3435
      Width           =   8835
      _ExtentX        =   15584
      _ExtentY        =   556
      _Version        =   393216
      Appearance      =   1
   End
   Begin VB.CommandButton cmdSalir 
      Caption         =   "Sali&r"
      Height          =   375
      Left            =   4680
      TabIndex        =   10
      Top             =   3825
      Width           =   1185
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "&Aceptar"
      Height          =   375
      Left            =   3300
      TabIndex        =   9
      Top             =   3825
      Width           =   1185
   End
   Begin VB.CommandButton cmdBuscar 
      Caption         =   "..."
      Height          =   315
      Left            =   8490
      TabIndex        =   7
      Top             =   2700
      Width           =   420
   End
   Begin VB.TextBox txtNombreDelArchivo 
      Height          =   315
      Left            =   2483
      TabIndex        =   8
      Top             =   2715
      Width           =   5865
   End
   Begin MSComDlg.CommonDialog dlgCommonDialog 
      Left            =   330
      Top             =   3720
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin GSTextBox.GSText txtNombreCuentaBancaria 
      Height          =   285
      Left            =   2550
      TabIndex        =   2
      Top             =   420
      Width           =   5145
      _ExtentX        =   9075
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
      MaxLength       =   40
      GMsgName        =   "Nombre Cuenta Bancaria"
   End
   Begin VB.Frame frameFechas 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Fechas"
      ForeColor       =   &H00808080&
      Height          =   615
      Left            =   225
      TabIndex        =   15
      Top             =   1170
      Width           =   3750
      Begin MSComCtl2.DTPicker dtpFechaFinal 
         Height          =   270
         Left            =   2385
         TabIndex        =   5
         Top             =   240
         Width           =   1260
         _ExtentX        =   2223
         _ExtentY        =   476
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   90832899
         CurrentDate     =   38718
      End
      Begin MSComCtl2.DTPicker dtpFechaInicial 
         Height          =   270
         Left            =   600
         TabIndex        =   4
         Top             =   225
         Width           =   1260
         _ExtentX        =   2223
         _ExtentY        =   476
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   90832899
         CurrentDate     =   38718
      End
      Begin VB.Label lblFechaFinal 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Final"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   1905
         TabIndex        =   16
         Top             =   270
         Width           =   330
      End
      Begin VB.Label lblFechaInicial 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Inicial"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   17
         Top             =   240
         Width           =   405
      End
   End
   Begin MSComCtl2.DTPicker dtpFechaGestion 
      Height          =   270
      Left            =   1680
      TabIndex        =   3
      Top             =   795
      Width           =   1260
      _ExtentX        =   2223
      _ExtentY        =   476
      _Version        =   393216
      CustomFormat    =   "dd/MM/yyyy"
      Format          =   90832899
      CurrentDate     =   38718
   End
   Begin VB.Label lblTitulo 
      BackStyle       =   0  'Transparent
      ForeColor       =   &H00A84439&
      Height          =   240
      Index           =   1
      Left            =   180
      TabIndex        =   21
      Top             =   2310
      Visible         =   0   'False
      Width           =   1995
   End
   Begin VB.Label lblNumeroDeContrato 
      BackStyle       =   0  'Transparent
      Caption         =   "Número de Referencia"
      ForeColor       =   &H00A84439&
      Height          =   240
      Left            =   180
      TabIndex        =   20
      Top             =   2130
      Width           =   1680
   End
   Begin VB.Label lblFechaGestion 
      AutoSize        =   -1  'True
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      Caption         =   "Fecha de Gestión"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   195
      TabIndex        =   19
      Top             =   765
      Width           =   1260
   End
   Begin VB.Label lblTitulo 
      BackStyle       =   0  'Transparent
      Caption         =   "Cuenta a Procesar:"
      ForeColor       =   &H00A84439&
      Height          =   240
      Index           =   3
      Left            =   195
      TabIndex        =   18
      Top             =   435
      Width           =   1440
   End
   Begin VB.Label lblporcentaje 
      Alignment       =   2  'Center
      BackColor       =   &H00F3F3F3&
      Height          =   255
      Left            =   345
      TabIndex        =   13
      Top             =   3195
      Width           =   8775
   End
   Begin VB.Label lblTitulo 
      BackStyle       =   0  'Transparent
      Caption         =   "Nombre del Archivo a Exportar"
      ForeColor       =   &H00A84439&
      Height          =   240
      Index           =   2
      Left            =   180
      TabIndex        =   11
      Top             =   2745
      Width           =   2310
   End
   Begin VB.Label lblTitulo 
      BackColor       =   &H00A86602&
      Caption         =   " Datos para la Exportación"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FFFFFF&
      Height          =   240
      Index           =   0
      Left            =   120
      TabIndex        =   0
      Top             =   105
      Width           =   8925
   End
End
Attribute VB_Name = "frmExportacionBanesco"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mAction As AccionSobreRecord
Private mEntidad As enum_EntidadAExportarOImportar
Private mEsFacturaBorradores As Boolean
Private insUtilImport As clsUtilImport
Private mCodigoCuentaBancariaOriginal As String
Private mNombreCuentaBancariaOriginal As String
Private insCuentaBancaria As Object
Private insConexionAos As Object
Private mUsaCodigoBancoEnPantalla As Boolean

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "frmExportacionBanesco"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Exportación Banesco"
End Function

Private Function CM_TITULO_ARCHIVO() As String
   CM_TITULO_ARCHIVO = "Nombre del Archivo a "
End Function

Private Function CM_TITULO_ARCHIVO_DE_DETALLES() As String
   CM_TITULO_ARCHIVO_DE_DETALLES = "Nombre del Archivo de Detalles "
End Function
 
Private Function CM_TITULO_FORMATO() As String
   CM_TITULO_FORMATO = "Formato de "
End Function
 
Private Function GetGender() As Enum_Gender
   GetGender = Enum_Gender.eg_Male
End Function

Private Sub sInitDefaultValues()
   On Error GoTo h_ERROR
   sClrRecord
   Set insUtilImport = New clsUtilImport
   txtNombreDelArchivo.Text = ""
   lblTitulo(1).Caption = CM_TITULO_FORMATO & "Exportación "
   lblTitulo(2).Caption = CM_TITULO_ARCHIVO & gDefGen.AccionSobreRecordStr(mAction)
   dtpFechaInicial.Value = gUtilDate.getFechaDeHoy
   dtpFechaFinal.Value = gUtilDate.getFechaDeHoy
   dtpFechaGestion.Value = gUtilDate.getFechaDeHoy
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sInitDefaultValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdBuscar_Click()
   Dim gUtilCommonDialog As clsUtilCommonDialog
   Dim vFilter As String
   On Error GoTo h_ERROR
   vFilter = "Archivos de Texto (*.txt)|*.txt|Todos Los Archivos (*.*)|*.*"
   Set gUtilCommonDialog = New clsUtilCommonDialog
   txtNombreDelArchivo.Text = gUtilCommonDialog.fGuardarArchivoComo(dlgCommonDialog, "Guardar", vFilter)
   gApi.ssSetFocus txtNombreDelArchivo
   Set gUtilCommonDialog = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "cmdBuscar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_Click()
   Dim insExportImport As clsExportarImportar
   On Error GoTo h_ERROR
   Set insExportImport = New clsExportarImportar
   If gTexto.DfLen(txtCodigoCuentaBancaria) = 0 Or gTexto.DfLen(txtNumeroDeReferencia) = 0 Or dtpFechaFinal.Value < dtpFechaInicial.Value _
         Or dtpFechaGestion.Value < gUtilDate.getFechaDeHoy Then
      If gTexto.DfLen(txtCodigoCuentaBancaria) = 0 Then
         gMessage.Advertencia "Debe seleccionar una cuenta. Para la exportación del archivo."
      End If
      If gTexto.DfLen(txtNumeroDeReferencia.Text) = 0 Then
         gMessage.Advertencia "Debe indicar un Número de Referencia. Para la exportación del archivo."
      End If
      If dtpFechaGestion.Value < gUtilDate.getFechaDeHoy Then
         gMessage.Advertencia "La Fecha de Gestión no debe ser Menor que la Fecha Actual."
      End If
      If dtpFechaFinal.Value < dtpFechaInicial.Value Then
         gMessage.Advertencia "Debe verificar el rango de fechas. La Fecha Inicial no debe ser Mayor que la Fecha Final."
      End If
   Else
      If fVerificaQueDeseaExportarElArchivoALaRutaEspecificada() Then
         gApi.SetMouseClock Me
         If Not fVerificarExistenciaDePagosEntreRangoDeFecha(dtpFechaInicial.Value, dtpFechaFinal.Value, txtCodigoCuentaBancaria) Then
            sGenerarArchivoDeExportacionParaBanesco
            gApi.SetMouseNormal Me
            Unload Me
         End If
      End If
   End If
   gApi.SetMouseNormal Me
   Set insExportImport = Nothing
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdGrabar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
   gApi.SetMouseNormal Me
End Sub

Private Sub cmdSalir_Click()
   On Error GoTo h_ERROR
   Unload Me
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdSalir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInitLookAndFeel(ByVal valAction As AccionSobreRecord, ByVal valEntidad As enum_EntidadAExportarOImportar, ByRef refCuentaBancaria As Object, ByVal valUsaCodigoBancoEnPantalla As Boolean, ByRef refConexionAos As Object)
   On Error GoTo h_ERROR
   Set insCuentaBancaria = refCuentaBancaria
   Set insConexionAos = refConexionAos
   mAction = valAction
   mEntidad = valEntidad
   mUsaCodigoBancoEnPantalla = valUsaCodigoBancoEnPantalla
   Me.Caption = gDefGen.AccionSobreRecordStr(mAction) & " - " & gEnumProyecto.enumEntidadAExportarOImportarToString(mEntidad) '& vLabelBorrador
   lblTitulo(0).Caption = "Datos de Pagos para  " & gDefGen.AccionSobreRecordStr(mAction)
   cmdGrabar.Caption = gDefGen.AccionSobreRecordStr(mAction)
   sConfiguraCodigoYNombreCuenta
   sInitDefaultValues
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sClrRecord()
   On Error GoTo h_ERROR
   gApi.ClearAllTextBoxes Me
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sClrRecord", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Load()
   On Error GoTo h_ERROR
   sInitDefaultValues
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fVerificaQueDeseaExportarElArchivoALaRutaEspecificada() As Boolean
   Dim valEsValido As Boolean
   On Error GoTo h_ERROR
   valEsValido = False
   If mAction = Exportar Then
      If LenB(txtNombreDelArchivo.Text) > 0 Then
         If gUtilFile.fExisteElArchivo(txtNombreDelArchivo.Text) Then
            If gMessage.Confirm("El nombre de archivo dado para la exportación ya existe en la ruta o directorio específicado." & vbCrLf & " ¿Desea reemplazar este archivo?") Then
               valEsValido = True
            End If
         Else
            valEsValido = True
         End If
      Else
         txtNombreDelArchivo.Text = txtNombreDelArchivo.Text & gUtilFile.fAddSlashCharToEndOfPathIfRequired(gDefDatabase.getPathActualDeLaAplicacion) & gEnumProyecto.enumEntidadAExportarOImportarToString(mEntidad) & ".txt"
         gMessage.Advertencia "No ingresó ningún nombre válido de archivo." & vbCrLf & "La exportación se hará al archivo" & gUtilFile.fAddSlashCharToEndOfPathIfRequired(gDefDatabase.getPathActualDeLaAplicacion) & gEnumProyecto.enumEntidadAExportarOImportarToString(mEntidad) & ".txt"
         valEsValido = True
      End If
   Else
      If mAction = Importar Then
         If LenB(txtNombreDelArchivo.Text) > 0 Then
            If gUtilFile.fExisteElArchivo(txtNombreDelArchivo.Text) Then
               valEsValido = True
            Else
               gMessage.Advertencia "No ingresó ningún nombre válido de archivo."
            End If
         Else
            gMessage.Advertencia "Ingrese la ruta del archivo que desea importar."
         End If
      End If
   End If
h_EXIT:
   On Error GoTo 0
   fVerificaQueDeseaExportarElArchivoALaRutaEspecificada = valEsValido
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fVerificaQueDeseaExportarElArchivoALaRutaEspecificada", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub txtCodigoCuentaBancaria_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoCuentaBancaria_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoCuentaBancaria_Validate(Cancel As Boolean)
   Dim mWhere As String
   On Error GoTo h_ERROR
   Cancel = False
   mWhere = "Saw.Gv_CuentaBancaria_B1.Status=0"
   If LenB(txtCodigoCuentaBancaria.Text) = 0 Then
      txtCodigoCuentaBancaria.Text = "*"
   End If
   If UCase(txtCodigoCuentaBancaria.Text) <> mCodigoCuentaBancariaOriginal Then
     If insConexionAos.fSelectAndSetValuesOfCuentaBancariaFromAOS(insCuentaBancaria, txtCodigoCuentaBancaria.Text, "", "", "", mWhere) Then
         sAssignFieldsFromConnectionCuentaBancaria
         mCodigoCuentaBancariaOriginal = UCase(txtCodigoCuentaBancaria.Text)
      Else
         Cancel = True
         gApi.ssSetFocus txtCodigoCuentaBancaria
      End If
   End If
            
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Cancel = True
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoCuentaBancaria_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreCuentaBancaria_Validate(Cancel As Boolean)
   Dim mWhere  As String
   On Error GoTo h_ERROR
   If gTexto.DfLen(txtNombreCuentaBancaria.Text) = 0 Or txtNombreCuentaBancaria.Text = "*" Then
      txtCodigoCuentaBancaria.Text = ""
   End If
   mWhere = "Saw.Gv_CuentaBancaria_B1.Status=0"
   If LenB(txtNombreCuentaBancaria.Text) = 0 Then
      txtNombreCuentaBancaria.Text = "*"
   End If
   If UCase(txtNombreCuentaBancaria.Text) <> mNombreCuentaBancariaOriginal Then
      If insConexionAos.fSelectAndSetValuesOfCuentaBancariaFromAOS(insCuentaBancaria, txtNombreCuentaBancaria.Text, "", "", "", mWhere) Then
         sAssignFieldsFromConnectionCuentaBancaria
         mNombreCuentaBancariaOriginal = UCase(txtNombreCuentaBancaria.Text)
      Else
         Cancel = True
         gApi.ssSetFocus txtCodigoCuentaBancaria
      End If
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Cancel = True
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreCuentaBancaria_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtNombreDelArchivo_Validate(Cancel As Boolean)
   Dim varExtensionDelFile As String
   On Error GoTo h_ERROR
   If txtNombreDelArchivo.Text = "" Then
   Else
      If Not gUtilFile.fIsValidStringForNewFileName(txtNombreDelArchivo.Text, True) Then
         txtNombreDelArchivo.Text = ""
         Cancel = True
      End If
   End If
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "txtNombreDelArchivo_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sSelectAndSetValuesOfCuentaBancaria()
   On Error GoTo h_ERROR
   If insCuentaBancaria.fRsRecordCount = 1 Then
      sAssignFieldsFromConnectionCuentaBancaria
   ElseIf insCuentaBancaria.fRsRecordCount > 1 Then
      If mUsaCodigoBancoEnPantalla Then
         insCuentaBancaria.sShowListSelect "Codigo", txtCodigoCuentaBancaria.Text
      Else
         insCuentaBancaria.sShowListSelect "Codigo", txtNombreCuentaBancaria.Text
      End If
      sAssignFieldsFromConnectionCuentaBancaria
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sSelectAndSetValuesOfCuentaBancaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAssignFieldsFromConnectionCuentaBancaria()
   On Error GoTo h_ERROR
   txtCodigoCuentaBancaria.Text = insCuentaBancaria.GetCodigo
   txtNombreCuentaBancaria.Text = insCuentaBancaria.GetNombreCuenta
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionCuentaBancaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sConfiguraCodigoYNombreCuenta()
   On Error GoTo h_ERROR
   txtCodigoCuentaBancaria.Visible = mUsaCodigoBancoEnPantalla
   txtCodigoCuentaBancaria.Enabled = mUsaCodigoBancoEnPantalla
   txtNombreCuentaBancaria.Enabled = Not mUsaCodigoBancoEnPantalla
   If Not mUsaCodigoBancoEnPantalla Then
      txtNombreCuentaBancaria.Left = txtCodigoCuentaBancaria.Left
   End If
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sConfiguraCodigoYNombreCuenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCheckForSpecialKeys(ByVal valKeyCode As Integer, ByVal valShift As Integer)
   On Error GoTo h_ERROR
   If gApi.sfEnterLikeTab(Me, valKeyCode) Then
   End If
   Select Case valKeyCode
      Case vbKeyEscape: Unload Me
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCheckForSpecialKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sGenerarArchivoDeExportacionParaBanesco()
   Dim vSqlEncabezado As String
   Dim insExportar As clsExportarImportar
   On Error GoTo h_ERROR
   Set insExportar = New clsExportarImportar
   'prgBarExportarDatos.Visible = True
   vSqlEncabezado = insExportar.fSQLExportarCabeceraBanesco(txtCodigoCuentaBancaria, dtpFechaInicial.Value, dtpFechaFinal.Value, dtpFechaGestion.Value, txtNumeroDeReferencia)
   If fCreaElArchivoDeExportacion(txtNombreDelArchivo.Text) Then
      If fExportarArchBanesco(vSqlEncabezado, txtNombreDelArchivo.Text, txtNumeroDeReferencia) Then
         'prgBarExportarDatos.Visible = False
         gMessage.exito "El archivo fue generado satisfactoriamente..."
         Unload Me
      End If
   Else
      gMessage.Advertencia "Proceso Fallido. Nombre o Ruta del Archivo inválido"
      Unload Me
   End If
   Set insExportar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGenerarArchivoDeExportacionParaBanesco", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fCreaElArchivoDeExportacion(ByVal valNombreDeArchivo As String) As Boolean
   On Error GoTo h_ERROR
   If gUtilFile.fExisteElArchivo(valNombreDeArchivo) Then
      gUtilFile.sBorraElArchivo valNombreDeArchivo
      fCreaElArchivoDeExportacion = gUtilFile.fCreaArchivoDeTexto(valNombreDeArchivo)
   Else
      If gUtilFile.fDfMkPath(gUtilFile.getPathName(valNombreDeArchivo), False) Then
         fCreaElArchivoDeExportacion = gUtilFile.fCreaArchivoDeTexto(valNombreDeArchivo)
      Else
         fCreaElArchivoDeExportacion = False
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreaElArchivoDeExportacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExportarArchBanesco(ByVal valSqlEncabezado As String, ByVal valNombreDeArchivo As String, ByVal valNumeroReferencia) As Boolean
   Dim rsExportacion As ADODB.Recordset
   Dim vLineaAExportar As String
   Dim vMontoTotal As String
   Dim vControl As Boolean
   Dim vHora As String
   On Error GoTo h_ERROR
   Set rsExportacion = New ADODB.Recordset
   'Cabecera de Arch Banesco
   If gDbUtil.fOpenRecordSetAllParameters(rsExportacion, valSqlEncabezado, Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If gDbUtil.fRecordCount(rsExportacion) > 0 Then
         vHora = gTexto.fCleanTextOfInvalidChars(gConvert.fTimeToString(gUtilDate.getActualTime, True), ":")
         vLineaAExportar = "HDRBANESCO" & gTexto.fNBlancos(8) & _
                  "ED" & gTexto.fNBlancos(2) & "95BPAYMULP"
         gUtilFile.fGrabarDatosEnElArchivo valNombreDeArchivo, vLineaAExportar
         vLineaAExportar = "01SCV" & gTexto.fNBlancos(32) & _
                  "9" & gTexto.fNBlancos(2) & valNumeroReferencia & gTexto.fNBlancos(35 - gTexto.DfLen(valNumeroReferencia)) & _
                  gUtilDate.fDateFormat(gUtilDate.getFechaDeHoy, "yyyymmdd") & vHora
         gUtilFile.fGrabarDatosEnElArchivo valNombreDeArchivo, vLineaAExportar
         vControl = fExportarArchPagoBanesco(valNombreDeArchivo, rsExportacion)
         'gApi.sAddProgress prgBarExportarDatos
      End If
   End If
   fExportarArchBanesco = True
   gDbUtil.sDestroyRecordSet rsExportacion
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExportarArchBanesco", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExportarArchPagoBanesco(ByVal valNombreDeArchivo As String, ByVal valRsEncabezado As ADODB.Recordset) As Boolean
   Dim rsExportacion As ADODB.Recordset
   Dim vLineaAExportar As String
   Dim vExportacionPago As String
   Dim insExportar As clsExportarImportar
   Dim contRegistro As Integer
   Dim vNumeroComprobante As String
   Dim vMontoTotal As Currency
   Dim vBeneficiario As String
   Dim vControl As Boolean
   On Error GoTo h_ERROR
   Set rsExportacion = New ADODB.Recordset
   Set insExportar = New clsExportarImportar
   'Pago de Arch Banesco
   vExportacionPago = insExportar.fSQLExportarPagoBanesco(txtCodigoCuentaBancaria, dtpFechaInicial.Value, dtpFechaFinal.Value, dtpFechaGestion.Value)
   If gDbUtil.fOpenRecordSetAllParameters(rsExportacion, vExportacionPago, Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If gDbUtil.fRecordCount(rsExportacion) > 0 Then
         vMontoTotal = fCalculaMontoTotal(rsExportacion)
         vControl = fEscribeCabecera(valNombreDeArchivo, valRsEncabezado, vMontoTotal)
         rsExportacion.MoveFirst
         For contRegistro = 0 To gDbUtil.fRecordCount(rsExportacion) - 1
            vBeneficiario = fPrepararNombreBeneficiario(rsExportacion.Fields("Beneficiario"))
            vLineaAExportar = rsExportacion.Fields("Seccion1")
            If gTexto.DfLen(rsExportacion.Fields("NumeroComprobante")) <= 8 Then
               vLineaAExportar = vLineaAExportar & gTexto.llenaConCaracterALaIzquierda(rsExportacion.Fields("NumeroComprobante"), _
                        "0", 8) & gTexto.fNBlancos(22)
            Else
               vLineaAExportar = vLineaAExportar & gTexto.LlenaConCaracterALaDerecha(rsExportacion.Fields("NumeroComprobante"), _
                        " ", 30)
            End If
            vLineaAExportar = vLineaAExportar & rsExportacion.Fields("MontoAcreditar") & _
                    rsExportacion.Fields("CuentaProveedor") & gTexto.fNBlancos(10) & _
                    rsExportacion.Fields("PrefijoBanco") & gTexto.fNBlancos(10) & _
                    UCase(rsExportacion.Fields("RIF")) & gTexto.fNBlancos(17 - gTexto.DfLen(rsExportacion.Fields("RIF"))) & _
                    gTexto.DfLeft(vBeneficiario, 35) & gTexto.fNBlancos(271 - gTexto.DfLen(gTexto.DfLeft(vBeneficiario, 35))) & _
                    rsExportacion.Fields("CaracterControl")
            gUtilFile.fGrabarDatosEnElArchivo valNombreDeArchivo, vLineaAExportar
            rsExportacion.MoveNext
         Next
         vLineaAExportar = ""
         vLineaAExportar = "06" & gTexto.nCar("0", 14) & "1" & gTexto.nCar("0", 15 - gTexto.DfLen(gConvert.fConvierteAString(contRegistro))) & contRegistro & _
               gTexto.nCar("0", 15 - gTexto.DfLen(gConvert.fConvierteAString(vMontoTotal))) & vMontoTotal
         gUtilFile.fGrabarDatosEnElArchivo valNombreDeArchivo, vLineaAExportar
      End If
   End If
   Set insExportar = Nothing
   fExportarArchPagoBanesco = True
   gDbUtil.sDestroyRecordSet rsExportacion
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExportarArchPagoBanesco", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fVerificarExistenciaDePagosEntreRangoDeFecha(ByVal valFechaInicial As String, _
                                                             ByVal valFechaFinal As String, _
                                                             ByVal valNumeroCuenta As String) As Boolean
   Dim rsValidarPagosExistentes As ADODB.Recordset
   Dim SQL As String
   Dim vValidarPagosExistentes As Boolean
   
   On Error GoTo h_ERROR
   
   Set rsValidarPagosExistentes = New ADODB.Recordset
   vValidarPagosExistentes = False

   
   SQL = ""
   SQL = "SELECT *"

   SQL = SQL & " FROM IGV_BODCabecera"

   SQL = SQL & " WHERE ConsecutivoCompania = " & mConsecutivoCompaniaActual
   SQL = SQL & " AND CodigoCuentaBancaria = " & gUtilSQL.fSimpleSqlValue(valNumeroCuenta)
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("Fecha", valFechaInicial, valFechaFinal) & ")"
   
   If gDbUtil.fOpenRecordset(rsValidarPagosExistentes, SQL, gDefDatabase.Conexion) Then
      If rsValidarPagosExistentes.RecordCount = 0 Then
         vValidarPagosExistentes = True
         gMessage.Advertencia "No se encontraron Pagos realizados en el rango de fechas seleccionado." _
                              & vbCrLf & "Verifique el rango de fechas."
      End If
   End If
   gDbUtil.sDestroyRecordSet rsValidarPagosExistentes
   fVerificarExistenciaDePagosEntreRangoDeFecha = vValidarPagosExistentes
h_EXIT:
   fVerificarExistenciaDePagosEntreRangoDeFecha = vValidarPagosExistentes
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fVerificarExistenciaDePagosEntreRangoDeFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub txtNumeroDeReferencia_KeyPress(KeyAscii As Integer)
 On Error GoTo h_ERROR
   If (Not gApi.esUnAsciiCodeDeInputNumerico(KeyAscii, False, False)) Or (KeyAscii = vbKeySpace) Then
      If Not gApi.fIsAsciiCodeInputInSet(KeyAscii, "") Then
         KeyAscii = 0
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNumero_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fPrepararNombreBeneficiario(ByVal valNombreBeneficiario As String) As String
On Error GoTo h_ERROR
   Dim vBeneficiario As String
   
   vBeneficiario = gTexto.fCleanTextOfInvalidChars(valNombreBeneficiario, "-()+/*;:_#$&'")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, ".", " ")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, ",", " ")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, "Ñ", "N")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, "Á", "A")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, "É", "E")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, "Í", "I")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, "Ó", "O")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, "Ú", "U")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, "Ü", "U")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, "À", "A")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, "È", "E")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, "Ì", "I")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, "Ò", "O")
   vBeneficiario = gTexto.DfReplace(vBeneficiario, "Ù", "U")
   
   fPrepararNombreBeneficiario = vBeneficiario
   
h_EXIT: On Error GoTo 0
   fPrepararNombreBeneficiario = vBeneficiario
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fPrepararNombreBeneficiario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCalculaMontoTotal(ByVal valRsPagos As ADODB.Recordset) As String
On Error GoTo h_ERROR
   Dim vMonto As Currency
   Dim contRegistro As Integer
   valRsPagos.MoveFirst
      For contRegistro = 0 To gDbUtil.fRecordCount(valRsPagos) - 1
         vMonto = vMonto + gConvert.fConvertStringToCurrency(valRsPagos.Fields("Monto"))
         valRsPagos.MoveNext
      Next
   fCalculaMontoTotal = gConvert.fConvierteAString(vMonto)
h_EXIT: On Error GoTo 0
   fCalculaMontoTotal = gConvert.fConvierteAString(vMonto)
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCalculaMontoTotal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fEscribeCabecera(ByVal valNombreDeArchivo As String, ByVal valRsCabecera As ADODB.Recordset, ByVal valMontoTotal As Currency) As Boolean
On Error GoTo h_ERROR
   Dim vLineaAExportar As String
   valRsCabecera.MoveFirst
   vLineaAExportar = ""
   vLineaAExportar = valRsCabecera.Fields("Seccion") & _
         valRsCabecera.Fields("Blanco1") & _
         gTexto.LlenaConCaracterALaDerecha(valRsCabecera.Fields("NumeroRif"), " ", 17) & _
         gTexto.LlenaConCaracterALaDerecha(valRsCabecera.Fields("Compania"), " ", 35) & _
         gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMontoTotal), "0", 15) & "VEF" & gTexto.fNBlancos(1) & _
         gTexto.LlenaConCaracterALaDerecha(valRsCabecera.Fields("CuentaDebitar"), " ", 34) & _
         gTexto.LlenaConCaracterALaDerecha("BANESCO", " ", 11) & _
         gTexto.fCleanTextOfInvalidChars(valRsCabecera.Fields("FechaDeposito"), "/")
   gUtilFile.fGrabarDatosEnElArchivo valNombreDeArchivo, vLineaAExportar
h_EXIT: On Error GoTo 0
   fEscribeCabecera = True
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEscribeCabecera", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
