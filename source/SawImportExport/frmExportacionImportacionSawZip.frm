VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomct2.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{4CE56793-802C-4711-9B30-453D91A33B40}#5.0#0"; "bw6zp16r.ocx"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form frmExportacionImportacionSawZip 
   BackColor       =   &H00F3F3F3&
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Exportar"
   ClientHeight    =   5430
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   9165
   Icon            =   "frmExportacionImportacionSawZip.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   ScaleHeight     =   5430
   ScaleWidth      =   9165
   ShowInTaskbar   =   0   'False
   Begin VB.Frame frameFechas 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Fechas"
      ForeColor       =   &H00808080&
      Height          =   615
      Left            =   5160
      TabIndex        =   22
      Top             =   480
      Visible         =   0   'False
      Width           =   3735
      Begin MSComCtl2.DTPicker dtpFechaFinal 
         Height          =   270
         Left            =   2400
         TabIndex        =   23
         Top             =   225
         Width           =   1215
         _ExtentX        =   2143
         _ExtentY        =   476
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   111345667
         CurrentDate     =   38718
      End
      Begin MSComCtl2.DTPicker dtpFechaInicial 
         Height          =   270
         Left            =   600
         TabIndex        =   24
         Top             =   230
         Width           =   1215
         _ExtentX        =   2143
         _ExtentY        =   476
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   111345667
         CurrentDate     =   38718
      End
      Begin VB.Label lblFechaInicial 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Inicial"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   26
         Top             =   240
         Width           =   405
      End
      Begin VB.Label lblFechaFinal 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Final"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   1920
         TabIndex        =   25
         Top             =   240
         Width           =   330
      End
   End
   Begin VB.TextBox txtProgreso 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   255
      Left            =   120
      TabIndex        =   21
      TabStop         =   0   'False
      Top             =   4190
      Width           =   3015
   End
   Begin VB.CommandButton cmdSelectAllChk 
      Caption         =   "Seleccionar todos"
      Height          =   255
      Left            =   6600
      TabIndex        =   6
      Top             =   4920
      Width           =   2385
   End
   Begin MSComctlLib.ProgressBar prgExportarImportar 
      Height          =   315
      Left            =   75
      TabIndex        =   18
      Top             =   4440
      Width           =   8940
      _ExtentX        =   15769
      _ExtentY        =   556
      _Version        =   393216
      Appearance      =   1
   End
   Begin VB.CommandButton cmdSalir 
      Caption         =   "Sali&r"
      Height          =   375
      Left            =   4530
      TabIndex        =   17
      Top             =   4830
      Width           =   1185
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "&Aceptar"
      Height          =   375
      Left            =   3240
      TabIndex        =   16
      Top             =   4830
      Width           =   1185
   End
   Begin VB.CommandButton cmdBuscar 
      Caption         =   "..."
      Height          =   315
      Left            =   8520
      TabIndex        =   5
      Top             =   1170
      Width           =   420
   End
   Begin VB.TextBox txtNombreDelArchivo 
      Height          =   315
      Left            =   2520
      TabIndex        =   4
      Top             =   1170
      Width           =   5985
   End
   Begin MSComDlg.CommonDialog dlgCommonDialog 
      Left            =   720
      Top             =   4800
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.ComboBox cmbTipoDeSeparacion 
      Height          =   315
      Left            =   2520
      TabIndex        =   2
      Top             =   585
      Width           =   2415
   End
   Begin VB.Frame FrameModulos 
      Height          =   2415
      Left            =   120
      TabIndex        =   19
      Top             =   1680
      Width           =   8895
      Begin VB.CheckBox chkGenerarCxCAlImportar 
         Caption         =   "Generar CxC de las facturas  a importar"
         ForeColor       =   &H00C00000&
         Height          =   375
         Left            =   6120
         TabIndex        =   15
         Top             =   1800
         Width           =   2415
      End
      Begin VB.CheckBox chkModulo 
         Caption         =   "Modulo De Borradores De Facturas"
         Height          =   495
         Index           =   7
         Left            =   6120
         TabIndex        =   14
         Top             =   1200
         Width           =   2055
      End
      Begin VB.CheckBox chkModulo 
         Caption         =   "Modulo De Facturas"
         Height          =   255
         Index           =   6
         Left            =   6120
         TabIndex        =   13
         Top             =   720
         Width           =   2055
      End
      Begin VB.CheckBox chkModulo 
         Caption         =   "Modulo De CxC"
         Height          =   375
         Index           =   5
         Left            =   3360
         TabIndex        =   12
         Top             =   1800
         Width           =   2055
      End
      Begin VB.CheckBox chkModulo 
         Caption         =   "Modulo De CxP"
         Height          =   255
         Index           =   4
         Left            =   3360
         TabIndex        =   11
         Top             =   1320
         Width           =   2055
      End
      Begin VB.CheckBox chkModulo 
         Caption         =   "Modulo De Artículos de Inventario"
         Height          =   495
         Index           =   3
         Left            =   3360
         TabIndex        =   10
         Top             =   600
         Width           =   2055
      End
      Begin VB.CheckBox chkModulo 
         Caption         =   "Modulo De Clientes"
         Height          =   255
         Index           =   2
         Left            =   480
         TabIndex        =   9
         Top             =   1920
         Width           =   2055
      End
      Begin VB.CheckBox chkModulo 
         Caption         =   "Modulo De Proveedores"
         Height          =   255
         Index           =   1
         Left            =   480
         TabIndex        =   8
         Top             =   1320
         Width           =   2055
      End
      Begin VB.CheckBox chkModulo 
         Caption         =   "Modulo De Vendedores"
         Height          =   255
         Index           =   0
         Left            =   480
         TabIndex        =   7
         Top             =   720
         Width           =   2055
      End
      Begin VB.Label lblTitulo 
         BackColor       =   &H00A86602&
         Caption         =   " Modulos que Desea "
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
         Index           =   3
         Left            =   120
         TabIndex        =   20
         Top             =   240
         Width           =   8685
      End
   End
   Begin BWZipCompress316b.MaqZip MaqZip1 
      Left            =   120
      Top             =   4800
      _ExtentX        =   1058
      _ExtentY        =   1058
   End
   Begin VB.Label lblporcentaje 
      BackColor       =   &H00F3F3F3&
      Height          =   255
      Left            =   3240
      TabIndex        =   27
      Top             =   4200
      Width           =   5775
   End
   Begin VB.Label lblTitulo 
      BackStyle       =   0  'Transparent
      Caption         =   "Nombre del Archivo a"
      ForeColor       =   &H00A84439&
      Height          =   240
      Index           =   2
      Left            =   180
      TabIndex        =   3
      Top             =   1200
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
      Top             =   615
      Width           =   1995
   End
   Begin VB.Label lblTitulo 
      BackColor       =   &H00A86602&
      Caption         =   " Datos para la "
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
      Top             =   120
      Width           =   8895
   End
End
Attribute VB_Name = "frmExportacionImportacionSawZip"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private mModuloSeleccionado As Integer
Private mAction As AccionSobreRecord
Private mNombreCarpetaExportacion As String
Private mNombreArchivoImportacion As String
Private mIsEntreFechas As Boolean
Private insUtilImport As clsUtilImport
Private Const CM_OPT_chkModuloDeVendedores    As Integer = 0
Private Const CM_OPT_chkModuloDeProveedores    As Integer = 1
Private Const CM_OPT_chkModuloDeClientes   As Integer = 2
Private Const CM_OPT_chkModuloDeArtículosInventario As Integer = 3
Private Const CM_OPT_chkModuloDeCxP As Integer = 4
Private Const CM_OPT_chkModuloDeCxC As Integer = 5
Private Const CM_OPT_chkModuloDeFacturas As Integer = 6
Private Const CM_OPT_chkModuloDeBorradoresDeFacturas As Integer = 7

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "frmExportacionImportacionSawZip"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Exportación/Importación Administrativo"
End Function

Private Function CM_TITULO_ARCHIVO() As String
   CM_TITULO_ARCHIVO = "Ruta del Archivo a "
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
   Me.Top = 0
   Me.Left = 0
   cmbTipoDeSeparacion.Width = 2415
    chkModulo(CM_OPT_chkModuloDeClientes).Value = 0
    chkModulo(CM_OPT_chkModuloDeVendedores).Value = 0
    chkModulo(CM_OPT_chkModuloDeProveedores).Value = 0
    chkModulo(CM_OPT_chkModuloDeArtículosInventario).Value = 0
    chkModulo(CM_OPT_chkModuloDeCxP).Value = 0
    chkModulo(CM_OPT_chkModuloDeCxC).Value = 0
    chkModulo(CM_OPT_chkModuloDeFacturas).Value = 0
    chkModulo(CM_OPT_chkModuloDeBorradoresDeFacturas).Value = 0
    If mAction = Importar Then
      chkGenerarCxCAlImportar.Visible = True
      chkGenerarCxCAlImportar.Enabled = False
      chkGenerarCxCAlImportar.Value = 0
    Else
      chkGenerarCxCAlImportar.Visible = False
      chkGenerarCxCAlImportar.Value = 0
      txtNombreDelArchivo.Text = gUtilFile.fAddSlashCharToEndOfPathIfRequired(gDefDatabase.getPathActualDeLaAplicacion) & mNombreCarpetaExportacion
      dtpFechaInicial.Value = gUtilDate.getFechaDeHoy
      dtpFechaFinal.Value = gUtilDate.getFechaDeHoy
    End If
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sInitDefaultValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub chkModulo_Click(Index As Integer)
   On Error GoTo h_ERROR
   If chkModulo(CM_OPT_chkModuloDeFacturas).Value = 1 Then
       chkGenerarCxCAlImportar.Enabled = True
   Else
       chkGenerarCxCAlImportar.Enabled = False
       chkGenerarCxCAlImportar.Value = 0
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "chkModulo_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
    If mAction = Importar Then
        txtNombreDelArchivo.Text = gUtilCommonDialog.fBuscarNombreDeArchivo(dlgCommonDialog, "Buscar", getFilterZip)
        gApi.ssSetFocus txtNombreDelArchivo
    Else
        txtNombreDelArchivo.Text = gUtilCommonDialog.fBuscarFolder(dlgCommonDialog, "Buscar")
        gApi.ssSetFocus txtNombreDelArchivo
    End If
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
   If fVerificaQueDeseaExportarImportarElArchivoALaRutaEspecificada() Then
      gApi.SetMouseClock Me
      sPreparaModulosParaEjecucion
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

Public Sub sInitLookAndFeel(ByVal valAction As AccionSobreRecord, ByVal valEntreFechas As Boolean)
   On Error GoTo h_ERROR
   mAction = valAction
   mIsEntreFechas = valEntreFechas
   Me.Caption = gDefGen.AccionSobreRecordStr(mAction)
   cmdGrabar.Caption = gDefGen.AccionSobreRecordStr(mAction)
   If mAction = Importar Then
      lblTitulo(0).Caption = "Datos para la Importación "
      lblTitulo(1).Caption = CM_TITULO_FORMATO & "Importación "
   Else
      lblTitulo(1).Caption = CM_TITULO_FORMATO & "Exportación "
      lblTitulo(0).Caption = "Datos para la Exportación "
      frameFechas.Visible = valEntreFechas
   End If
   lblTitulo(2).Caption = CM_TITULO_ARCHIVO & gDefGen.AccionSobreRecordStr(mAction)
   lblTitulo(3).Caption = lblTitulo(3).Caption & gDefGen.AccionSobreRecordStr(mAction)
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

Private Sub cmdSelectAllChk_Click()
On Error GoTo h_ERROR
  chkModulo(CM_OPT_chkModuloDeClientes).Value = 1
  chkModulo(CM_OPT_chkModuloDeVendedores).Value = 1
  chkModulo(CM_OPT_chkModuloDeProveedores).Value = 1
  chkModulo(CM_OPT_chkModuloDeArtículosInventario).Value = 1
  chkModulo(CM_OPT_chkModuloDeCxP).Value = 1
  chkModulo(CM_OPT_chkModuloDeCxC).Value = 1
  chkModulo(CM_OPT_chkModuloDeFacturas).Value = 1
  chkModulo(CM_OPT_chkModuloDeBorradoresDeFacturas).Value = 1
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdSelectAllChk_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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

Private Function getFilterZip() As String
   On Error GoTo h_ERROR
      getFilterZip = "Importacion (*.zip)|*.zip|Todos Los Archivos (*.*)|*.*"
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "getFilterZip", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fVerificaQueDeseaExportarImportarElArchivoALaRutaEspecificada() As Boolean
    Dim valEsValido As Boolean
    On Error GoTo h_ERROR
    valEsValido = False
    Select Case mAction
    Case Exportar: mNombreCarpetaExportacion = gTexto.DfReplace(gUtilFile.fCreaElNombreDeArchivoDeExportacion(mNombreCompaniaActual, True, "MODULOS_EXPORTADOS", ""), ".txt", "")
         If gTexto.DfLen(txtNombreDelArchivo.Text) > 0 Then
            txtNombreDelArchivo.Text = txtNombreDelArchivo.Text & "\" & mNombreCarpetaExportacion
            If gUtilFile.fExisteElFolder(txtNombreDelArchivo.Text) Then
               If gMessage.Confirm("El nombre de archivo dado para la exportación ya existe en la ruta o directorio específicado." & vbCrLf & " ¿Desea reemplazar este archivo?") Then
                  valEsValido = True
               End If
            ElseIf gUtilFile.fCrearCarpeta(txtNombreDelArchivo.Text) Then
               valEsValido = True
            End If
         Else
            txtNombreDelArchivo.Text = txtNombreDelArchivo.Text & gUtilFile.fAddSlashCharToEndOfPathIfRequired(gDefDatabase.getPathActualDeLaAplicacion) & mNombreCarpetaExportacion
            If gUtilFile.fCrearCarpeta(txtNombreDelArchivo.Text) Then
               gMessage.Advertencia "No ingresó ningun nombre válido de archivo." & vbCrLf & "La exportación se hará al archivo" & gUtilFile.fAddSlashCharToEndOfPathIfRequired(gDefDatabase.getPathActualDeLaAplicacion) & mNombreCarpetaExportacion
               valEsValido = True
            End If
         End If
    Case Importar:
         If gTexto.DfLen(txtNombreDelArchivo.Text) > 0 Then
            If gUtilFile.fExisteElArchivo(txtNombreDelArchivo.Text) Then
               valEsValido = True
               mNombreArchivoImportacion = gTexto.DfReplace(txtNombreDelArchivo.Text, gUtilFile.fParentFolderName(txtNombreDelArchivo.Text), "")
            Else
               gMessage.Advertencia "No ingresó ningun nombre válido de archivo."
            End If
         Else
            gMessage.Advertencia "Ingrese la ruta del archivo que desea importar."
         End If
    End Select
h_EXIT:
   On Error GoTo 0
   fVerificaQueDeseaExportarImportarElArchivoALaRutaEspecificada = valEsValido
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fVerificaQueDeseaExportarImportarElArchivoALaRutaEspecificada", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub MaqZip1_Errorcode(Errorcode As Long, Errordescription As String)
   On Error GoTo h_ERROR
   If Errorcode <> 0 Then
      gMessage.AlertMessage Errordescription, "ERROR " & Errorcode
   End If
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "MaqZip1_Errorcode", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub MaqZip1_Progress(Position As Long)
   On Error Resume Next
   gApi.sAddProgress prgExportarImportar
   On Error GoTo 0
End Sub

Private Sub MaqZip1_SplitZipFileProgress(ProgressValue As Long)
   On Error Resume Next
   gApi.sAddProgress prgExportarImportar
   On Error GoTo 0
End Sub

Private Sub MaqZip1_SystemErrorCode(Errorcode As Long, Description As String)
   On Error GoTo h_ERROR
   If Errorcode <> 0 Then
      gMessage.AlertMessage Errorcode & vbCrLf & Description, "ERROR"
   End If
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "MaqZip1_SystemErrorCode", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub MaqZip1_WarningCode(WarningCode As Long, WarningDescription As String)
   On Error GoTo h_ERROR
   If WarningCode > 0 Then
      gMessage.Advertencia WarningCode & vbCrLf & WarningDescription
   End If
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "MaqZip1_WarningCode", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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

Private Sub sPreparaModulosParaEjecucion()
    Dim Index As Integer
    Dim valEntidad As enum_EntidadAExportarOImportar
    Dim insExportImport As clsExportarImportar
    Dim Extencion As String
    Dim nombreDelArchivo As String
    Dim refBorradorFacturaStr As String
    Dim refEsFacturaBorradores As Boolean
    Dim refExito As Boolean
    Dim Continuar As Boolean
    Dim PathCarpeta As String
    On Error GoTo h_ERROR
    Set insExportImport = New clsExportarImportar
    refExito = False
    Continuar = True
    Extencion = gTexto.DfMid(gApi.SelectedElementInComboBoxToString(cmbTipoDeSeparacion), 1, 3)
    PathCarpeta = txtNombreDelArchivo.Text
    If mAction = Importar Then
        sDescomprimeElZip PathCarpeta, insExportImport, Continuar
    End If
    If Continuar Then
        For Index = CM_OPT_chkModuloDeVendedores To CM_OPT_chkModuloDeBorradoresDeFacturas
            If chkModulo(Index).Value = Checked Then
               sVerificaValoresParaBorradoresFactura Index, refEsFacturaBorradores, refBorradorFacturaStr
               valEntidad = fChekToValEntidadModulo(Index)
               nombreDelArchivo = PathCarpeta & "\" & gEnumProyecto.enumEntidadAExportarOImportarToString(valEntidad) & refBorradorFacturaStr & "." & Extencion
               insExportImport.sEjecutarAccion mAction, valEntidad, nombreDelArchivo, Extencion, prgExportarImportar, gConvert.ConvertByteToBoolean(chkGenerarCxCAlImportar.Value), refExito, refEsFacturaBorradores, mIsEntreFechas, dtpFechaInicial.Value, dtpFechaFinal.Value, lblporcentaje, Me, txtProgreso
            End If
        Next
        If refExito Then
           If mAction = Exportar Then
              sCreaElZip
           Else
              gMessage.exito "¡Se ha completado la importación de los modulos seleccionados!"
              If refExito = gUtilFile.fBorrarCarpeta(gTexto.DfReplace(PathCarpeta, ".Zip", "")) Then
                 gMessage.exito "La Operación ha concluido exitosamente."
              End If
           End If
        Else
           gMessage.Advertencia "Transacción cancelada." & vbCrLf & "No se realizó ninguna acción."
        End If
    End If
    Set insExportImport = Nothing
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sPreparaModulosParaEjecucion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fChekToValEntidadModulo(ByVal valIndex As Integer) As enum_EntidadAExportarOImportar
    Dim mEntidad As enum_EntidadAExportarOImportar
On Error GoTo h_ERROR
    Select Case valIndex
        Case CM_OPT_chkModuloDeClientes: mEntidad = eEAEI_CLIENTE
        Case CM_OPT_chkModuloDeVendedores: mEntidad = eEAEI_VENDEDOR
        Case CM_OPT_chkModuloDeProveedores: mEntidad = eEAEI_PROVEEDOR
        Case CM_OPT_chkModuloDeArtículosInventario: mEntidad = eEAEI_ARTICULO_INVENTARIO
        Case CM_OPT_chkModuloDeCxP: mEntidad = eEAEI_CxP
        Case CM_OPT_chkModuloDeCxC: mEntidad = eEAEI_CxC
        Case CM_OPT_chkModuloDeFacturas: mEntidad = eEAEI_FACTURA
        Case CM_OPT_chkModuloDeBorradoresDeFacturas: mEntidad = eEAEI_FACTURA
    End Select
    fChekToValEntidadModulo = mEntidad
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fChekToValEntidadModulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sDescomprimeElZip(ByRef PathCarpeta As String, insExportImport As clsExportarImportar, ByRef refContinuar As Boolean)
    Dim TamañoLimite
    On Error GoTo h_ERROR
    TamañoLimite = gUtilFile.getEspacioLibreEnDiscoEnBytes(txtNombreDelArchivo.Text)
    PathCarpeta = gTexto.DfReplace(txtNombreDelArchivo.Text, ".zip", "")
    If gUtilFile.fCrearCarpeta(PathCarpeta) Then
      txtProgreso.Text = "Descomprimiendo el Zip...."
      If insExportImport.fDescomprimeLaCarpetaZip(MaqZip1, False, PathCarpeta, TamañoLimite, txtNombreDelArchivo.Text) Then
      txtProgreso.Text = "Verificando la data...."
      Else
         gMessage.Advertencia "Transacción cancelada." & vbCrLf & "No se pudo descomprimir el archivo " & mNombreArchivoImportacion
         txtProgreso.Text = ""
         refContinuar = False
      End If
    End If
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sDescomprimeElZip", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCreaElZip()
    Dim insExportImport As clsExportarImportar
    On Error GoTo h_ERROR
    Set insExportImport = New clsExportarImportar
    txtProgreso.Text = "Creando el ZIP...."
    If insExportImport.fRespaldaTodosLosArchivosEnUnZip(txtNombreDelArchivo.Text, "*.*", gUtilFile.fParentFolderName(txtNombreDelArchivo.Text), mNombreCarpetaExportacion & ".zip", MaqZip1) Then
        txtProgreso.Text = ""
        gMessage.exito "La Operación ha sido Completada."
    Else
        gMessage.Advertencia "Transacción cancelada." & vbCrLf & "No se pudo crear el archivo " & mNombreCarpetaExportacion & ".zip"
    End If
    Set insExportImport = Nothing
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCreaElZip", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


Private Sub sVerificaValoresParaBorradoresFactura(ByVal Index As Integer, ByRef refEsFacturaBorradores As Boolean, ByRef refBorradorFacturaStr As String)
On Error GoTo h_ERROR
    If Index = CM_OPT_chkModuloDeBorradoresDeFacturas Then
        refEsFacturaBorradores = True
        refBorradorFacturaStr = " Documentos en Espera"
    Else
        refEsFacturaBorradores = False
        refBorradorFacturaStr = ""
    End If
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sVerificaValoresParaBorradoresFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

