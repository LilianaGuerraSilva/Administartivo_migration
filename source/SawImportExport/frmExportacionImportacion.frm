VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form frmExportacionImportacion 
   BackColor       =   &H00F3F3F3&
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Exportar"
   ClientHeight    =   2715
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   9510
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   ScaleHeight     =   2715
   ScaleWidth      =   9510
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox txtProgreso 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   255
      Left            =   240
      TabIndex        =   11
      TabStop         =   0   'False
      Top             =   1550
      Width           =   3015
   End
   Begin VB.CheckBox chkGenerarCxCAlImportar 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Generar CxC de las facturas  a importar"
      ForeColor       =   &H00A84439&
      Height          =   375
      Left            =   6120
      TabIndex        =   9
      Top             =   480
      Visible         =   0   'False
      Width           =   2895
   End
   Begin MSComctlLib.ProgressBar prgExportarImportar 
      Height          =   315
      Left            =   195
      TabIndex        =   8
      Top             =   1800
      Width           =   8835
      _ExtentX        =   15584
      _ExtentY        =   556
      _Version        =   393216
      Appearance      =   1
   End
   Begin VB.CommandButton cmdSalir 
      Caption         =   "Sali&r"
      Height          =   375
      Left            =   4530
      TabIndex        =   7
      Top             =   2190
      Width           =   1185
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "&Aceptar"
      Height          =   375
      Left            =   3240
      TabIndex        =   6
      Top             =   2190
      Width           =   1185
   End
   Begin VB.CommandButton cmdBuscar 
      Caption         =   "..."
      Height          =   315
      Left            =   8505
      TabIndex        =   5
      Top             =   1043
      Width           =   420
   End
   Begin VB.TextBox txtNombreDelArchivo 
      Height          =   315
      Left            =   2483
      TabIndex        =   3
      Top             =   1043
      Width           =   5865
   End
   Begin MSComDlg.CommonDialog dlgCommonDialog 
      Left            =   180
      Top             =   675
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.ComboBox cmbTipoDeSeparacion 
      Height          =   315
      Left            =   2483
      TabIndex        =   2
      Top             =   465
      Width           =   2895
   End
   Begin VB.Label lblporcentaje 
      Alignment       =   2  'Center
      BackColor       =   &H00F3F3F3&
      Height          =   255
      Left            =   240
      TabIndex        =   10
      Top             =   1560
      Width           =   8775
   End
   Begin VB.Label lblTitulo 
      BackStyle       =   0  'Transparent
      Caption         =   "Nombre del Archivo a"
      ForeColor       =   &H00A84439&
      Height          =   240
      Index           =   2
      Left            =   180
      TabIndex        =   4
      Top             =   1080
      Width           =   2310
   End
   Begin VB.Label lblTitulo 
      BackStyle       =   0  'Transparent
      Caption         =   "Formato de Exportación"
      ForeColor       =   &H00A84439&
      Height          =   240
      Index           =   1
      Left            =   180
      TabIndex        =   1
      Top             =   495
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
      Top             =   90
      Width           =   8925
   End
End
Attribute VB_Name = "frmExportacionImportacion"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private mAction As AccionSobreRecord
Private mEntidad As enum_EntidadAExportarOImportar
Private mEsFacturaBorradores As Boolean
Private insUtilImport As clsUtilImport

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "frmExportacionImportacion"
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
   If mAction = Importar Then
      lblTitulo(1).Caption = CM_TITULO_FORMATO & "Importación "
   Else
      lblTitulo(1).Caption = CM_TITULO_FORMATO & "Exportación "
   End If
   lblTitulo(2).Caption = CM_TITULO_ARCHIVO & gDefGen.AccionSobreRecordStr(mAction)
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
   If mAction = Exportar Then
      txtNombreDelArchivo.Text = gUtilCommonDialog.fGuardarArchivoComo(dlgCommonDialog, "Guardar", getFilter)
   Else
      txtNombreDelArchivo.Text = gUtilCommonDialog.fBuscarNombreDeArchivo(dlgCommonDialog, "Buscar", getFilter)
   End If
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
   If fVerificaQueDeseaExportarElArchivoALaRutaEspecificada() Then
      gApi.SetMouseClock Me
      insExportImport.sEjecutarAccion mAction, mEntidad, txtNombreDelArchivo.Text, Separador, prgExportarImportar, gConvert.ConvertByteToBoolean(chkGenerarCxCAlImportar.Value), , mEsFacturaBorradores, , , , lblporcentaje, Me, txtProgreso
      gApi.SetMouseNormal Me
      Unload Me
   End If
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

Public Sub sInitLookAndFeel(ByVal valAction As AccionSobreRecord, ByVal valEntidad As enum_EntidadAExportarOImportar, Optional ByVal valEsFacturaBorradores As Boolean = False)
   Dim vLabelBorrador As String
   On Error GoTo h_ERROR
   mAction = valAction
   mEntidad = valEntidad
   mEsFacturaBorradores = valEsFacturaBorradores
   If mEsFacturaBorradores Then
      vLabelBorrador = " Borradores"
   Else
      vLabelBorrador = ""
   End If
   Me.Caption = gDefGen.AccionSobreRecordStr(mAction) & " - " & gEnumProyecto.enumEntidadAExportarOImportarToString(mEntidad) & vLabelBorrador
   lblTitulo(0).Caption = "Datos de para  " & gDefGen.AccionSobreRecordStr(mAction) & vLabelBorrador
   cmdGrabar.Caption = gDefGen.AccionSobreRecordStr(mAction)
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
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sClrRecord()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
         gMessage.Advertencia "No ingresó ningun nombre válido de archivo." & vbCrLf & "La exportación se hará al archivo" & gUtilFile.fAddSlashCharToEndOfPathIfRequired(gDefDatabase.getPathActualDeLaAplicacion) & gEnumProyecto.enumEntidadAExportarOImportarToString(mEntidad) & "." & gTexto.DfMid(gApi.SelectedElementInComboBoxToString(cmbTipoDeSeparacion), 1, 3)
         valEsValido = True
      End If
   Else
      If mAction = Importar Then
         If LenB(txtNombreDelArchivo.Text) > 0 Then
            If gUtilFile.fExisteElArchivo(txtNombreDelArchivo.Text) Then
               valEsValido = True
            Else
               gMessage.Advertencia "No ingresó ningun nombre válido de archivo."
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
