VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomct2.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Object = "{9F849E84-0608-4BCD-A2B4-8EC557266A4F}#11.0#0"; "GSTextBox.ocx"
Begin VB.Form frmExportacionBOD 
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
   Begin VB.TextBox txtNumeroDeContrato 
      Height          =   285
      Left            =   1725
      MaxLength       =   17
      TabIndex        =   7
      Top             =   1890
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
      TabIndex        =   17
      TabStop         =   0   'False
      Top             =   3135
      Width           =   3015
   End
   Begin MSComctlLib.ProgressBar prgBarExportarDatos 
      Height          =   315
      Left            =   240
      TabIndex        =   15
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
      Left            =   4575
      TabIndex        =   11
      Top             =   3825
      Width           =   1185
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "&Aceptar"
      Height          =   375
      Left            =   3300
      TabIndex        =   10
      Top             =   3825
      Width           =   1185
   End
   Begin VB.CommandButton cmdBuscar 
      Caption         =   "..."
      Height          =   315
      Left            =   8490
      TabIndex        =   9
      Top             =   2700
      Width           =   420
   End
   Begin VB.TextBox txtNombreDelArchivo 
      Height          =   315
      Left            =   2483
      TabIndex        =   13
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
   Begin VB.ComboBox cmbTipoDeSeparacion 
      Height          =   315
      Left            =   2483
      TabIndex        =   8
      Top             =   2280
      Visible         =   0   'False
      Width           =   2895
   End
   Begin GSTextBox.GSText txtNombreCuentaBancaria 
      Height          =   285
      Left            =   2550
      TabIndex        =   18
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
   Begin VB.Frame frmFormaPago 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Descripción de la Operación"
      ForeColor       =   &H00808080&
      Height          =   615
      Left            =   4050
      TabIndex        =   19
      Top             =   1170
      Width           =   3150
      Begin VB.OptionButton OptTipoPago 
         BackColor       =   &H00F3F3F3&
         Caption         =   "Proveedores"
         Height          =   255
         Index           =   0
         Left            =   90
         TabIndex        =   5
         Top             =   210
         Width           =   1590
      End
      Begin VB.OptionButton OptTipoPago 
         BackColor       =   &H00F3F3F3&
         Caption         =   "Nómina"
         Height          =   255
         Index           =   1
         Left            =   1740
         TabIndex        =   6
         Top             =   240
         Width           =   1335
      End
   End
   Begin VB.Frame frameFechas 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Fechas"
      ForeColor       =   &H00808080&
      Height          =   615
      Left            =   225
      TabIndex        =   20
      Top             =   1170
      Width           =   3750
      Begin MSComCtl2.DTPicker dtpFechaFinal 
         Height          =   270
         Left            =   2385
         TabIndex        =   4
         Top             =   255
         Width           =   1260
         _ExtentX        =   2223
         _ExtentY        =   476
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   81854467
         CurrentDate     =   38718
      End
      Begin MSComCtl2.DTPicker dtpFechaInicial 
         Height          =   270
         Left            =   600
         TabIndex        =   3
         Top             =   225
         Width           =   1260
         _ExtentX        =   2223
         _ExtentY        =   476
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   81854467
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
         TabIndex        =   21
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
         TabIndex        =   22
         Top             =   240
         Width           =   405
      End
   End
   Begin MSComCtl2.DTPicker dtpFechaGestion 
      Height          =   270
      Left            =   1680
      TabIndex        =   2
      Top             =   795
      Width           =   1260
      _ExtentX        =   2223
      _ExtentY        =   476
      _Version        =   393216
      CustomFormat    =   "dd/MM/yyyy"
      Format          =   81854467
      CurrentDate     =   38718
   End
   Begin VB.Label lblNumeroDeContrato 
      BackStyle       =   0  'Transparent
      Caption         =   "Número de Contrato"
      ForeColor       =   &H00A84439&
      Height          =   240
      Left            =   180
      TabIndex        =   25
      Top             =   1890
      Width           =   1440
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      Caption         =   "Fecha de Gestión"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   195
      TabIndex        =   24
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
      TabIndex        =   23
      Top             =   435
      Width           =   1440
   End
   Begin VB.Label lblporcentaje 
      Alignment       =   2  'Center
      BackColor       =   &H00F3F3F3&
      Height          =   255
      Left            =   345
      TabIndex        =   16
      Top             =   3195
      Width           =   8775
   End
   Begin VB.Label lblTitulo 
      BackStyle       =   0  'Transparent
      Caption         =   "Nombre del Archivo a"
      ForeColor       =   &H00A84439&
      Height          =   240
      Index           =   2
      Left            =   180
      TabIndex        =   14
      Top             =   2745
      Width           =   2310
   End
   Begin VB.Label lblTitulo 
      BackStyle       =   0  'Transparent
      Caption         =   "Formato de Exportación"
      ForeColor       =   &H00A84439&
      Height          =   240
      Index           =   1
      Left            =   180
      TabIndex        =   12
      Top             =   2310
      Visible         =   0   'False
      Width           =   1995
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
Attribute VB_Name = "frmExportacionBOD"
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
   CM_FILE_NAME = "frmExportacionBOD"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Exportación/Importación"
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
   insUtilImport.sFillComboBoxWithTipoDeSeparacion cmbTipoDeSeparacion, False
   txtNombreDelArchivo.Text = ""
   lblTitulo(1).Caption = CM_TITULO_FORMATO & "Exportación "
   lblTitulo(2).Caption = CM_TITULO_ARCHIVO & gDefGen.AccionSobreRecordStr(mAction)
   OptTipoPago(0).Value = True
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

Private Sub cmbTipoDeSeparacion_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   gApi.ValidateTextInComboBox cmbTipoDeSeparacion
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmbTipoDeSeparacion_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdBuscar_Click()
   Dim gUtilCommonDialog As clsUtilCommonDialog
   On Error GoTo h_ERROR
   Set gUtilCommonDialog = New clsUtilCommonDialog
   txtNombreDelArchivo.Text = gUtilCommonDialog.fGuardarArchivoComo(dlgCommonDialog, "Guardar", getFilter)
   gApi.ssSetFocus txtNombreDelArchivo
   Set gUtilCommonDialog = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "cmdBuscar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_Click()
   Dim insExportImport As clsExportarImportar
   Dim Separador As String
   On Error GoTo h_ERROR
   Set insExportImport = New clsExportarImportar
   Separador = gTexto.DfMid(gApi.SelectedElementInComboBoxToString(cmbTipoDeSeparacion), 1, 3)
   If gTexto.DfLen(txtCodigoCuentaBancaria) = 0 Or gTexto.DfLen(txtNumeroDeContrato) = 0 Or dtpFechaFinal.Value < dtpFechaInicial.Value Then
      If gTexto.DfLen(txtCodigoCuentaBancaria) = 0 Then
         gMessage.Advertencia "Debe seleccionar una cuenta. Para la exportación del archivo."
      End If
      If gTexto.DfLen(txtNumeroDeContrato.Text) = 0 Then
         gMessage.Advertencia "Debe indicar un número de contrato. Para la exportación del archivo."
      End If
      If dtpFechaFinal.Value < dtpFechaInicial.Value Then
         gMessage.Advertencia "Debe verificar el rango de fechas. La Fecha Inicial no debe ser Mayor que la Fecha Final."
      End If
   Else
      If fVerificaQueDeseaExportarElArchivoALaRutaEspecificada() Then
         gApi.SetMouseClock Me
         If Not fVerificarExistenciaDePagosEntreRangoDeFecha(dtpFechaInicial.Value, dtpFechaFinal.Value, txtCodigoCuentaBancaria) Then
            sGenerarArchivoDeExportacionParaBOD
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
   'Dim vLabelBorrador As String
   On Error GoTo h_ERROR
   Set insCuentaBancaria = refCuentaBancaria
   Set insConexionAos = refConexionAos
   mAction = valAction
   mEntidad = valEntidad
   mUsaCodigoBancoEnPantalla = valUsaCodigoBancoEnPantalla
   'vLabelBorrador = ""
   Me.Caption = gDefGen.AccionSobreRecordStr(mAction) & " - " & gEnumProyecto.enumEntidadAExportarOImportarToString(mEntidad) '& vLabelBorrador
   lblTitulo(0).Caption = "Datos de Pagos para  " & gDefGen.AccionSobreRecordStr(mAction) '& vLabelBorrador
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

Private Function getSeparador() As String
   On Error GoTo h_ERROR
   Select Case gTexto.DfMid(gApi.SelectedElementInComboBoxToString(cmbTipoDeSeparacion), 1, 3)
      Case "CVS": getSeparador = ";"
      Case "TXT": getSeparador = vbTab
      Case Else: getSeparador = ";"
   End Select
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "getSeparador", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function getFilter() As String
   On Error GoTo h_ERROR
   If getSeparador = ";" Then
      getFilter = "Exportacion (*.exp)|*.exp|Todos Los Archivos (*.*)|*.*"
   Else
      getFilter = "Archivos de Texto (*.txt)|*.txt|Todos Los Archivos (*.*)|*.*"
   End If
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "getFilter", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

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
         txtNombreDelArchivo.Text = txtNombreDelArchivo.Text & gUtilFile.fAddSlashCharToEndOfPathIfRequired(gDefDatabase.getPathActualDeLaAplicacion) & gEnumProyecto.enumEntidadAExportarOImportarToString(mEntidad) & "." & gTexto.DfMid(gApi.SelectedElementInComboBoxToString(cmbTipoDeSeparacion), 1, 3)
         gMessage.Advertencia "No ingresó ningún nombre válido de archivo." & vbCrLf & "La exportación se hará al archivo" & gUtilFile.fAddSlashCharToEndOfPathIfRequired(gDefDatabase.getPathActualDeLaAplicacion) & gEnumProyecto.enumEntidadAExportarOImportarToString(mEntidad) & "." & gTexto.DfMid(gApi.SelectedElementInComboBoxToString(cmbTipoDeSeparacion), 1, 3)
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
     If insConexionAos.fSelectAndSetValuesOfCuentaBancariaFromAOS(insCuentaBancaria, txtCodigoCuentaBancaria.Text, "", "", "", mWhere, False, False, False, True) Then
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
      If insConexionAos.fSelectAndSetValuesOfCuentaBancariaFromAOS(insCuentaBancaria, txtNombreCuentaBancaria.Text, "", "", "", mWhere, False, False, False, True) Then
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

Private Sub sGenerarArchivoDeExportacionParaBOD()
   Dim vSqlEncabezado As String
   Dim insExportar As clsExportarImportar
   On Error GoTo h_ERROR
   Set insExportar = New clsExportarImportar
   
   'prgBarExportarDatos.Visible = True
   vSqlEncabezado = insExportar.fSQLExportarCabeceraBOD(txtCodigoCuentaBancaria, dtpFechaInicial.Value, dtpFechaFinal.Value, dtpFechaGestion.Value, OptTipoPago(0).Value, txtNumeroDeContrato)
   If fCreaElArchivoDeExportacion(txtNombreDelArchivo.Text) Then
      If fExportarArchBOD(vSqlEncabezado, txtNombreDelArchivo.Text) Then
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGenerarArchivoDeExportacionParaBOD", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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

Public Function fExportarArchBOD(ByVal valSqlEncabezado As String, ByVal valNombreDeArchivo As String) As Boolean
   Dim rsExportacion As ADODB.Recordset
   Dim vLineaAExportar As String
   On Error GoTo h_ERROR
   Set rsExportacion = New ADODB.Recordset
   'Cabecera de Arch BOD
   If gDbUtil.fOpenRecordSetAllParameters(rsExportacion, valSqlEncabezado, Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If gDbUtil.fRecordCount(rsExportacion) > 0 Then
     
         vLineaAExportar = rsExportacion.Fields("Cabecera") & _
                 rsExportacion.Fields("Descripcion") & _
                 rsExportacion.Fields("TipoId") & _
                 rsExportacion.Fields("Rif") & _
                 rsExportacion.Fields("Contrato") & _
                 rsExportacion.Fields("NumeroLote") & _
                 rsExportacion.Fields("FechaGestion") & _
                 rsExportacion.Fields("CantidadRegistros") & _
                 rsExportacion.Fields("TotalDocumentos") & _
                 rsExportacion.Fields("Moneda")

         gUtilFile.fGrabarDatosEnElArchivo valNombreDeArchivo, vLineaAExportar
         
         fExportarArchPagoBOD (valNombreDeArchivo)
         'gApi.sAddProgress prgBarExportarDatos
      End If
   End If
   fExportarArchBOD = True
   gDbUtil.sDestroyRecordSet rsExportacion
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExportarArchBOD", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExportarArchPagoBOD(ByVal valNombreDeArchivo As String) As Boolean
   Dim rsExportacion As ADODB.Recordset
   Dim vLineaAExportar As String
   Dim vExportacionPago As String
   Dim insExportar As clsExportarImportar
   Dim contRegistro As Integer
   Dim vNumeroComprobante As String
   
   Dim vNumeroDelDocumentoPagado As String
   
   On Error GoTo h_ERROR
   Set rsExportacion = New ADODB.Recordset
   Set insExportar = New clsExportarImportar
   'Pago de Arch BOD
   vExportacionPago = insExportar.fSQLExportarPagoBOD(txtCodigoCuentaBancaria, dtpFechaInicial.Value, dtpFechaFinal.Value, OptTipoPago(0).Value, dtpFechaGestion.Value)
   If gDbUtil.fOpenRecordSetAllParameters(rsExportacion, vExportacionPago, Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If gDbUtil.fRecordCount(rsExportacion) > 0 Then
         rsExportacion.MoveFirst
         For contRegistro = 0 To gDbUtil.fRecordCount(rsExportacion) - 1
            vLineaAExportar = rsExportacion.Fields("Detalle") & _
                    rsExportacion.Fields("TipoId") & _
                    rsExportacion.Fields("Rif") & _
                    rsExportacion.Fields("Proveedor") & _
                    rsExportacion.Fields("NumeroComprobante") & _
                    rsExportacion.Fields("DescripcionPago") & _
                    rsExportacion.Fields("ModalidadPago") & _
                    rsExportacion.Fields("CuentaAcreditar") & _
                    rsExportacion.Fields("CodigoBanco") & _
                    rsExportacion.Fields("FechaValor") & _
                    rsExportacion.Fields("TotalDocumentos") & _
                    rsExportacion.Fields("ImpuestoRetenido") & _
                    rsExportacion.Fields("CorreoElectronico") & _
                    rsExportacion.Fields("Telefonos")
                    
             
            gUtilFile.fGrabarDatosEnElArchivo valNombreDeArchivo, vLineaAExportar
            
            vNumeroComprobante = rsExportacion.Fields("NumeroComprobante")
            fExportarArchOperacionesBOD valNombreDeArchivo, vNumeroComprobante
            
            rsExportacion.MoveNext
            'gApi.sAddProgress prgBarExportarDatos
         Next
      End If
   End If
   Set insExportar = Nothing
   fExportarArchPagoBOD = True
   gDbUtil.sDestroyRecordSet rsExportacion
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExportarArchPagoBOD", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExportarArchOperacionesBOD(ByVal valNombreDeArchivo As String, ByVal valNumeroComprobante As String) As Boolean
   Dim rsExportacion As ADODB.Recordset
   Dim vLineaAExportar As String
   Dim vExportacionOperaciones As String
   Dim insExportar As clsExportarImportar
   Dim contRegistro As Integer
   On Error GoTo h_ERROR
   Set rsExportacion = New ADODB.Recordset
   Set insExportar = New clsExportarImportar
   'Pago de Arch BOD
   vExportacionOperaciones = insExportar.fSQLExportarOperacionBOD(txtCodigoCuentaBancaria, dtpFechaInicial.Value, dtpFechaFinal.Value, OptTipoPago(0).Value, valNumeroComprobante)
   
   If gDbUtil.fOpenRecordSetAllParameters(rsExportacion, vExportacionOperaciones, Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If rsExportacion.RecordCount > 0 Then
         rsExportacion.MoveFirst
         For contRegistro = 0 To rsExportacion.RecordCount - 1
            vLineaAExportar = rsExportacion.Fields("Operacion") & _
            rsExportacion.Fields("FacturaAsociado") & _
            rsExportacion.Fields("TotalDocumentos") & _
            rsExportacion.Fields("FechaValor") & _
            rsExportacion.Fields("ImpuestoRetenido")

            gUtilFile.fGrabarDatosEnElArchivo valNombreDeArchivo, vLineaAExportar
            rsExportacion.MoveNext
            'gApi.sAddProgress prgBarExportarDatos
         Next
      End If
   End If
   Set insExportar = Nothing
   fExportarArchOperacionesBOD = True
   gDbUtil.sDestroyRecordSet rsExportacion
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExportarArchOperacionesBOD", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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

Private Sub txtNumeroDeContrato_KeyPress(KeyAscii As Integer)
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
