VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.1#0"; "MSCOMCTL.OCX"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form frmInformesImpuestosMunicipales 
   BackColor       =   &H00F3F3F3&
   Caption         =   "Form1"
   ClientHeight    =   4290
   ClientLeft      =   120
   ClientTop       =   450
   ClientWidth     =   10035
   LinkTopic       =   "Form1"
   ScaleHeight     =   4290
   ScaleWidth      =   10035
   Begin VB.Frame frameDispositivo 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Dispositivo"
      ForeColor       =   &H8000000C&
      Height          =   1455
      Left            =   4080
      TabIndex        =   12
      Top             =   1560
      Width           =   5775
      Begin VB.CommandButton cmdBuscar 
         Caption         =   "..."
         Height          =   315
         Left            =   5160
         TabIndex        =   15
         Top             =   720
         Width           =   420
      End
      Begin VB.TextBox txtNombreDelArchivo 
         Enabled         =   0   'False
         ForeColor       =   &H00808080&
         Height          =   315
         Left            =   120
         TabIndex        =   14
         Top             =   720
         Width           =   4905
      End
      Begin MSComctlLib.ProgressBar pbBarraDePreogreso 
         Height          =   210
         Left            =   120
         TabIndex        =   16
         Top             =   1200
         Visible         =   0   'False
         Width           =   5415
         _ExtentX        =   9551
         _ExtentY        =   370
         _Version        =   393216
         Appearance      =   1
      End
      Begin VB.Label lblNombreArchivoEtiqueta 
         BackColor       =   &H00F3F3F3&
         Caption         =   "Nombre del Archivo:"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00800000&
         Height          =   255
         Left            =   120
         TabIndex        =   13
         Top             =   360
         Width           =   2295
      End
   End
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   3240
      TabIndex        =   11
      Top             =   3720
      Width           =   1335
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "&Pantalla"
      Height          =   375
      Left            =   1680
      TabIndex        =   10
      Top             =   3720
      Width           =   1335
   End
   Begin VB.CommandButton cmdGenerar 
      Caption         =   "&Generar"
      Height          =   375
      Left            =   120
      TabIndex        =   9
      Top             =   3720
      Width           =   1335
   End
   Begin VB.Frame frameInformes 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   2955
      Left            =   240
      TabIndex        =   5
      Top             =   120
      Width           =   3705
      Begin VB.OptionButton optInformeDeImpuestos 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Resumen retenciones actividades económicas e industria comercio y servicios ....."
         ForeColor       =   &H00A84439&
         Height          =   675
         Index           =   0
         Left            =   120
         TabIndex        =   0
         Top             =   480
         Width           =   3225
      End
      Begin VB.OptionButton optInformeDeImpuestos 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Generacion Archivo Plano....."
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   6
         Top             =   2520
         Width           =   3225
      End
      Begin VB.Label lblVarios 
         BackColor       =   &H00A86602&
         Caption         =   " Declaraciones electrónicas  "
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
         Left            =   0
         TabIndex        =   8
         Top             =   2040
         Width           =   3690
      End
      Begin VB.Label lblInformesDeImpuestosMunicipales 
         BackColor       =   &H00A86602&
         Caption         =   "  Informes de retenciones"
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
         Left            =   -45
         TabIndex        =   7
         Top             =   120
         Width           =   3690
      End
   End
   Begin VB.Frame framePeriodoDeAplicacion 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Período de Aplicación"
      ForeColor       =   &H80000010&
      Height          =   675
      Left            =   4080
      TabIndex        =   1
      Top             =   720
      Width           =   2415
      Begin VB.TextBox txtAno 
         Height          =   285
         Left            =   1560
         MaxLength       =   4
         TabIndex        =   3
         Top             =   240
         Width           =   735
      End
      Begin VB.TextBox txtMes 
         Height          =   285
         Left            =   960
         MaxLength       =   2
         TabIndex        =   2
         Top             =   240
         Width           =   495
      End
      Begin VB.Label lblMesAno 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Mes/Año "
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   180
         TabIndex        =   4
         Top             =   300
         Width           =   705
      End
   End
   Begin MSComDlg.CommonDialog cdControlDeArchivo 
      Left            =   9360
      Top             =   3240
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
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
      Left            =   4080
      TabIndex        =   17
      Top             =   240
      Width           =   6660
   End
End
Attribute VB_Name = "frmInformesImpuestosMunicipales"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private gProyCompaniaActual As Object
Private gDefDatabase As Object
Private gUtilWrp As Object
Private mInformeSeleccionado As Integer
Private mDondeImprimir As enum_DondeImprimir
Private Const CM_FILE_NAME As String = "frmInformesImpuestosMunicipales"
Private Const CM_MESSAGE_NAME As String = "Informes Impuestos Municipales."
Private Const OPT_RESUMEN_RETENCIONES_ACTIVIDADES_ECONOMICAS As Integer = 0
Private Const OPT_ARCHIVO_PLANO_RETENCIONES_ACTIVIDADES_ECONOMICAS As Integer = 1

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Sub cmdBuscar_Click()
   On Error GoTo h_ERROR
   cdControlDeArchivo.InitDir = gDefDatabase.getLogicUnitName
   cdControlDeArchivo.FileName = "*.*"
   cdControlDeArchivo.DefaultExt = "*.*"
   cdControlDeArchivo.DialogTitle = "Buscar Archivo a " & gDefgen.AccionSobreRecordStr(Exportar)
   cdControlDeArchivo.ShowSave
   If (cdControlDeArchivo.FileName <> "") And (UCase(cdControlDeArchivo.FileName) <> UCase("*.*")) Then
       txtNombreDelArchivo.Text = UCase(cdControlDeArchivo.FileName)
       cmdGrabar.Enabled = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdBuscarArchivo_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGenerar_Click()
Dim a As String
  On Error GoTo h_ERROR
  If (txtNombreDelArchivo.Text <> "") Then
     fRealizaLaExportacionDeDatos
  End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdGenerar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_Click()
  On Error GoTo h_ERROR
   gAPI.ssSetFocus cmdGrabar
   mDondeImprimir = eDI_PANTALLA
   sEjecutaElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdGrabar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdSalir_Click()
   On Error GoTo h_ERROR
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Sub sInitLookAndFeel()
   On Error GoTo h_ERROR
   Me.Caption = CM_MESSAGE_NAME
   gAPI.ssSetFocus optInformeDeImpuestos(OPT_RESUMEN_RETENCIONES_ACTIVIDADES_ECONOMICAS)
   mInformeSeleccionado = OPT_RESUMEN_RETENCIONES_ACTIVIDADES_ECONOMICAS
   sInitDefaultValues
   gAPI.ssSetFocus optInformeDeImpuestos(OPT_RESUMEN_RETENCIONES_ACTIVIDADES_ECONOMICAS)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitDefaultValues()
   On Error GoTo h_ERROR
   txtMes.Text = gConvert.MesAString2Digitos(gUtilDate.getFechaDeHoy)
   txtAno.Text = gConvert.fConvierteAString(gUtilDate.fYear(gUtilDate.getFechaDeHoy))
   txtNombreDelArchivo.Text = fNombreDelArchivo
   pbBarraDePreogreso.Visible = False
   If fBuscarCodigoMunicipio = "VENBOL0005" Then
      cmdGenerar.Visible = False
      cmdGrabar.Visible = True
   Else
      cmdGenerar.Visible = True
      cmdGrabar.Visible = False
   End If
h_EXIT:   On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitDefaultValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Load()
On Error GoTo h_ERROR
   On Error GoTo h_ERROR
   Me.AutoRedraw = True
   Me.ZOrder 0
   Height = 5100
   Width = 10650
   If gDefgen.getMainForm.Width > Width Then
      Left = (gDefgen.getMainForm.Width - Width) / 20
      Top = (gDefgen.getMainForm.Height - Height) / 6
   Else
      Left = 0
      Top = 0
   End If
    mInformeSeleccionado = OPT_RESUMEN_RETENCIONES_ACTIVIDADES_ECONOMICAS
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtAno_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtAno
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
 
Private Sub txtAno_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.EsUnCaracterAsciiValidoParaCampoTipoIntegerSoloPositivo(KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAno_Validate(Cancel As Boolean)
   Dim valFechaCompleta As Date
   On Error GoTo h_ERROR
   If Not txtAno.Visible Then
      GoTo h_EXIT
   End If
   If LenB(txtAno.Text) = 0 Then
      sShowMessageForRequiredFields "Año", txtAno
      Cancel = True
   ElseIf gTexto.DfLen(txtAno.Text) < 4 Then
      gMessage.Advertencia "Debe introducir un 'Año' de cuatro dígitos'"
      Cancel = True
   End If
   If (txtMes.Text <> "") Then
    valFechaCompleta = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(txtMes.Text, txtAno.Text, False)
    If gDefgen.LaFechaEsMayorQueElLimiteDeIngresoDeDatos(valFechaCompleta, True, Generar) Then
       txtMes.Text = gTexto.llenaConCaracterALaIzquierda(Month(gDefgen.fFechaLimiteParaIngresoDeDatos), "0", 2)
       txtAno.Text = Year(gDefgen.fFechaLimiteParaIngresoDeDatos)
    End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAno_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtMes_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.EsUnCaracterAsciiValidoParaCampoTipoIntegerSoloPositivo(KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtMes_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fRealizaLaExportacionDeDatos()
 Dim vRutaArchivo As String
 On Error GoTo h_ERROR
 vRutaArchivo = ""
 vRutaArchivo = txtNombreDelArchivo.Text
  gUtilFile.sBorraElArchivo vRutaArchivo
 If gUtilFile.fCreaArchivoDeTexto(vRutaArchivo) Then
    If gUtilFile.fExisteElArchivo(vRutaArchivo) Then
     pbBarraDePreogreso.Visible = True
     sGenerarTxtRetencionImpuestoMunicipal vRutaArchivo
     pbBarraDePreogreso.Visible = False
    End If
 End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fRealizaLaExportacionDeDatos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sGenerarTxtRetencionImpuestoMunicipal(ByVal valRutaArchivo As String)
   Dim vResult As Boolean
   Dim objWrapper As Object
   Dim vSQLWhere As String
   Dim insCxP As clsCxpSQL
   On Error GoTo h_ERROR
   
   Set insCxP = New clsCxpSQL
   Set objWrapper = gUtilWrp.fCreateWrpForModule(gUtilWrp.fWrpClassName("Impuesto", "wrpFormatosImpMunicipales"), gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword)
    
   vSQLWhere = insCxP.fSqlWhereArchivoDeTextoRetencionImpuestos(txtAno.Text, txtMes.Text, gProyCompaniaActual.GetConsecutivoCompania)
   gMessage.AlertMessage "Este proceso puede tardar unos minutos, presione aceptar para continuar.", "Información"
   vResult = objWrapper.EscribirTxtImpuestoMunicipal(valRutaArchivo, vSQLWhere, gProyCompaniaActual.GetConsecutivoCompania)

   If vResult Then
      gMessage.AlertMessage "Proceso finalizado correctamente, presione aceptar para continuar.", "Información"
   Else
      gMessage.Warning "No se encontro información para la fecha indicada. El proceso no pudo ser completado, presione aceptar para continuar."
   End If
h_EXIT:  On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddInteropMethodToStackTrace(Err.Description, CM_FILE_NAME, "sGenerarTxtRetencionImpuestoMunicipal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError, Err.Source)
End Sub
 
Private Sub optInformeDeImpuestos_Click(Index As Integer)
 On Error GoTo h_ERROR
   mInformeSeleccionado = Index
   sOcultarTodosLosCampos
   Select Case mInformeSeleccionado
      Case OPT_RESUMEN_RETENCIONES_ACTIVIDADES_ECONOMICAS: sResumenRetencionesActvidadesEconomicas
      Case OPT_ARCHIVO_PLANO_RETENCIONES_ACTIVIDADES_ECONOMICAS: sActivarCamposGeneracionArchivoPlano
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInformeDeImpuestos_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposGeneracionArchivoPlano()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Retenciones Actividades Económicas"
   If fBuscarCodigoMunicipio = "VENBOL0005" Then
      cmdGenerar.Visible = False
      cmdGrabar.Visible = True
      frameDispositivo.Visible = False
   Else
      cmdGenerar.Visible = True
      cmdGrabar.Visible = False
      frameDispositivo.Visible = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeAnalisisDeVencimiento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sResumenRetencionesActvidadesEconomicas()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Resumen Retenciones Actividades Económicas"
   cmdGrabar.Visible = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sResumenRetencionesActvidadesEconomicas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultarTodosLosCampos()
   On Error GoTo h_ERROR
  frameDispositivo.Visible = False
  cmdGrabar.Visible = False
  cmdGenerar.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOcultarTodosLosCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElInformeSeleccionado()
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case OPT_RESUMEN_RETENCIONES_ACTIVIDADES_ECONOMICAS: sEjecutaResumenRetencionesActividadesEconomicas
      Case OPT_ARCHIVO_PLANO_RETENCIONES_ACTIVIDADES_ECONOMICAS: sEjecutaResumenRetencionesPorMunicipio
      End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElInformeSeleccionado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
 
Private Sub sShowMessageForRequiredFields(ByVal valCampo As String, ByRef refCampo As TextBox)
   On Error GoTo h_ERROR
   gMessage.ShowRequiredFields valCampo
   gAPI.ssSetFocus refCampo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sShowMessageForRequiredFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
 
Private Sub sEjecutaResumenRetencionesActividadesEconomicas()
   Dim vSqlDelReporte As String
   Dim vReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCxPRpt
   Dim vinsCXPSQL As clsCxpSQL
   Dim vEsPorMunicipio As Boolean
   Dim vEsMunicipioCristobalRojas As Boolean
   On Error GoTo h_ERROR
   Set vReporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsCxPRpt
   Set vinsCXPSQL = New clsCxpSQL
   vEsPorMunicipio = False
   vEsMunicipioCristobalRojas = False
   If fBuscarCodigoMunicipio = "VENBOL0005" And gConvert.ConvertByteToBoolean(optInformeDeImpuestos(1).Value) Then
      vEsPorMunicipio = True
   End If
   If fBuscarCodigoMunicipio = "VENMIR0008" And gConvert.ConvertByteToBoolean(optInformeDeImpuestos(0).Value) Then
      vEsMunicipioCristobalRojas = True
   End If
   vSqlDelReporte = vinsCXPSQL.fSQLResumenRetencionesActividadesEconomicas(gConvert.ConvierteAlong(txtAno.Text), gConvert.ConvierteAlong(txtMes.Text), gProyCompaniaActual.GetConsecutivoCompania)
   If insConfigurar.fConfigurarDatosDelReporteResumenRetencionDeActividadesEconomicas(vReporte, vSqlDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), vEsPorMunicipio, vEsMunicipioCristobalRojas) Then
      gUtilReports.sMostrarOImprimirReporte vReporte, 1, mDondeImprimir, "Resumen Retenciones Actividades Económicas"
   End If
   Set insConfigurar = Nothing
   Set vinsCXPSQL = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaProveedoresSinMovimientos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fNombreDelArchivo() As String
   Dim RutaParaArchivo As String
   Dim NombreDeArchivo As String
   Dim VarCompania As String
   On Error GoTo h_ERROR
   fNombreDelArchivo = ""
   If gDefDatabase.getIsSQLDataBase Then
      RutaParaArchivo = gDefDatabase.getLogicUnitName
   Else
      RutaParaArchivo = gDefDatabase.getPathActualDeLaAplicacion
   End If
   VarCompania = gProyCompaniaActual.GetNombre
   gTexto.ReemplazaCaracteresEnElString VarCompania, " ", ""
   NombreDeArchivo = gDefgen.fDataPathUser(gDefProg.GetSiglasDelPrograma, False) & "Impues_" & VarCompania & "_" & gUtilDate.fDay(gUtilDate.getFechaDeHoy) & gUtilDate.fMonth(gUtilDate.getFechaDeHoy) & gUtilDate.fYear(gUtilDate.getFechaDeHoy) & ".txt"
   fNombreDelArchivo = NombreDeArchivo
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreDelArchivo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fBuscarCodigoMunicipio() As String
   Dim SQL As String
   Dim rsMunicipio As ADODB.Recordset
   Dim vCodigoMunicipio As String
   On Error GoTo h_ERROR
   SQL = "SELECT Comun.Municipio.Codigo FROM Comun.MunicipioCiudad "
   SQL = SQL & " INNER JOIN Comun.Municipio ON (Comun.Municipio.Codigo = Comun.MunicipioCiudad.CodigoMunicipio)"
   SQL = SQL & " WHERE Consecutivo =  " & gProyCompaniaActual.GetConsecutivoMunicipio
   
   Set rsMunicipio = New ADODB.Recordset
   If gDbUtil.fOpenRecordset(rsMunicipio, SQL, gDefDatabase.Conexion) Then
      If gDbUtil.fRecordCount(rsMunicipio) > 0 Then
          vCodigoMunicipio = rsMunicipio("Codigo").Value
      End If
   End If
   gDbUtil.sDestroyRecordSet rsMunicipio
   fBuscarCodigoMunicipio = vCodigoMunicipio
h_EXIT:    On Error GoTo 0
   Exit Function
h_ERROR:    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fBuscarCodigoMunicipio", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sEjecutaResumenRetencionesPorMunicipio()
   Dim vSqlDelReporte As String
   Dim vReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCxPRpt
   Dim vinsCXPSQL As clsCxpSQL
   Dim vEsPorMunicipio As Boolean
   On Error GoTo h_ERROR
   Set vReporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsCxPRpt
   Set vinsCXPSQL = New clsCxpSQL
   vEsPorMunicipio = False
   If fBuscarCodigoMunicipio = "VENBOL0005" And optInformeDeImpuestos(1).Value Then
      vEsPorMunicipio = True
   End If
   vSqlDelReporte = vinsCXPSQL.fSQLResumenRetencionesActividadesEconomicas(gConvert.ConvierteAlong(txtAno.Text), gConvert.ConvierteAlong(txtMes.Text), gProyCompaniaActual.GetConsecutivoCompania)
   If insConfigurar.fConfigurarDatosDelReporteResumenRetencionDeActividadesEconomicas(vReporte, vSqlDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False, False), vEsPorMunicipio, False) Then
      gUtilReports.sMostrarOImprimirReporte vReporte, 1, mDondeImprimir, "Resumen Retenciones Actividades Económicas"
   End If
   Set insConfigurar = Nothing
   Set vinsCXPSQL = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaProveedoresSinMovimientos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLoadObjectValues(ByVal valCompaniaActual As Object, ByVal valDefDatabase As Object, _
                             ByVal valUtilWrp As Object)
   On Error GoTo h_ERROR
   Set gProyCompaniaActual = valCompaniaActual
   Set gDefDatabase = valDefDatabase
   Set gUtilWrp = valUtilWrp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLoadObjectValues", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
