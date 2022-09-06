VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.1#0"; "MSCOMCTL.OCX"
Begin VB.Form frmImportarWincont 
   BackColor       =   &H00F3F3F3&
   Caption         =   "Importar data de Wincont"
   ClientHeight    =   3390
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6465
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   ScaleHeight     =   3390
   ScaleWidth      =   6465
   StartUpPosition =   2  'CenterScreen
   Begin VB.ListBox lstCompanias 
      Appearance      =   0  'Flat
      Height          =   1200
      Left            =   360
      TabIndex        =   2
      Top             =   1200
      Width           =   5775
   End
   Begin VB.TextBox txtPath 
      Appearance      =   0  'Flat
      Enabled         =   0   'False
      Height          =   285
      Left            =   1920
      TabIndex        =   0
      TabStop         =   0   'False
      Top             =   480
      Width           =   4215
   End
   Begin VB.CommandButton cmdConectar 
      Caption         =   "&Buscar......"
      Height          =   255
      Left            =   360
      TabIndex        =   1
      TabStop         =   0   'False
      Top             =   480
      Width           =   1455
   End
   Begin MSComctlLib.ProgressBar prgBarImportar 
      Height          =   255
      Left            =   120
      TabIndex        =   5
      Top             =   3000
      Visible         =   0   'False
      Width           =   6255
      _ExtentX        =   11033
      _ExtentY        =   450
      _Version        =   393216
      Appearance      =   1
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "&Grabar"
      Height          =   375
      Left            =   3480
      TabIndex        =   3
      Top             =   2520
      Width           =   1335
   End
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   4920
      TabIndex        =   4
      Top             =   2520
      Width           =   1335
   End
   Begin VB.TextBox txtProgreso 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Enabled         =   0   'False
      Height          =   375
      Left            =   120
      TabIndex        =   8
      TabStop         =   0   'False
      Top             =   2520
      Width           =   3255
   End
   Begin VB.Label lblSeleccioneCompaniaInfo2000 
      BackColor       =   &H00A86602&
      Caption         =   "   Seleccione la Empresa de Contabilidad"
      ForeColor       =   &H00FFFFFF&
      Height          =   255
      Left            =   360
      TabIndex        =   7
      Top             =   840
      Width           =   5775
   End
   Begin VB.Label lblPath 
      BackColor       =   &H00A86602&
      Caption         =   "   Seleccione la Base de Datos de Contabilidad"
      ForeColor       =   &H00FFFFFF&
      Height          =   255
      Left            =   360
      TabIndex        =   6
      Top             =   120
      Width           =   5775
   End
End
Attribute VB_Name = "frmImportarWincont"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private mCnWincont As ADODB.Connection
Private mrsCompania As ADODB.Recordset
Private mConsecutivoCompaniaWincont As Long
Private mNombreDelSistemaActual As String
Private mLockingFileReest As Integer
Private mLockingFileSupervisor As Integer
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "frmImportarWincont"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Importar Wincont"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Sub cmdConectar_Click()
   Dim varPathTemp As String
   Dim varContinuar As Boolean
   Dim i As Integer
   On Error GoTo h_ERROR
   varContinuar = True
   i = 0
   sDesbloqueaElWinCont
   lstCompanias.Clear
   While varContinuar
      i = i + 1
      txtPath.Text = ""
      If fConnectWithOtherSystem(GetSiglasWinCont, False, varPathTemp, mCnWincont) Then
         If varPathTemp <> "" Then
            txtPath.Text = varPathTemp
         End If
         If fPuedoLlenarLaListaConLasCompaniasDelWincont Then
            sFillListWithNombresCompanias
            varContinuar = False
         End If
      End If
      If i = 4 Then
         varContinuar = False
         gMessage.InformationMessage "Verifique bien cuál es la base de datos que desea seleccionar para importación y luego intente nuevamente ejecutar este proceso.", "Cancelando Proceso"
         Unload Me
      End If
   Wend
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdConectar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_Click()
   Dim sql As String
   Dim rsTemp As ADODB.Recordset
   Dim varSalir As Boolean
   Dim vVersionBDDWincont As String
   On Error GoTo h_ERROR
   Set rsTemp = New ADODB.Recordset
   varSalir = True
   If Not fLosDatosEscogidosSonValidos Then
      varSalir = False
      GoTo h_EXIT
   End If
   If Not gUtilFile.fExisteElArchivo(txtPath.Text & "\REES_AYR.TXT") Then
      gMessage.Advertencia "No existe el archivo de bloqueo del Sistema Contable (Wincont). " _
         & "Sin este archivo no se puede asegurar la integridad de la data importada." _
         & vbCr & "El proceso será cancelado."
      GoTo h_EXIT
   End If
   sql = "SELECT fldVersionBDD From VersionContabil "
   If gDbUtil.fOpenRecordSetAllParameters(rsTemp, sql, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      If gDbUtil.fRecordCount(rsTemp) > 0 Then
         gDefProg.GetVersionBaseDeDatos
         vVersionBDDWincont = rsTemp.Fields("fldVersionBDD").value
         If (rsTemp.Fields("fldVersionBDD").value < 2.27) Then
            gMessage.Advertencia "Debe actualizar su programa de Wincont a una versión posterior a la 9.37 para poder realizar este proceso" _
               & vbCr & "El proceso será cancelado."
               GoTo h_EXIT
         End If
      End If
   End If
   If gMessage.YesNoMessage(fMensajeInicioConversion, "CONVERSION WINCONT") Then
      prgBarImportar.Visible = True
      gAPI.SetMouseClock Me
      sEliminaLaInformacionContableAsociadaALaCompaniaActual True
      sSetConsecutivoCompaniaAImportar
      If fEjecutaProcesoDeImportarDataDelWincont(vVersionBDDWincont) Then
         If fEscogeLosNuevosParametrosParaLaCompaniaActual Then
            gAPI.SetMouseNormal Me
            gMessage.Success "El proceso ha conluido con éxito." & vbCr & "A continuación deberá modificar las Reglas de Contabilización del " & mNombreDelSistemaActual & "."
            sModificaLasReglasDeContabilizacion
            sSiLaEmpresaUsaAuxiliaresMandaMensaje
            sModificarLaFechaDeInicioDeContabilizacion
            gMessage.Success "El proceso ha concluido."
         Else
            gMessage.AlertMessage "Ocurrió un error escogiendo los parámetros para la Empresa actual." _
                  & vbCr & "Vuelva a Escoger Empresa.", "SELECCIONAR PARAMETROS ACTUALES"
         End If
      Else
         gMessage.Advertencia "Ocurrió un error convirtiendo la data del Wincont." _
               & vbCr & "El proceso se canceló. Toda la información contable importada hasta el momento en el sistema será eliminada."
         sEliminaLaInformacionContableAsociadaALaCompaniaActual False
      End If
      prgBarImportar.Visible = False
      gAPI.SetMouseNormal Me
   Else
      gMessage.Advertencia "El proceso ha sido cancelado por el usuario."
   End If
   gDbUtil.sDestroyRecordSet rsTemp
h_EXIT: On Error GoTo 0
   If varSalir Then
      Unload Me
   End If
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "cmdGrabar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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

Private Sub Form_Load()
   On Error GoTo h_ERROR
   sInitDefaultValues
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Unload(Cancel As Integer)
   On Error GoTo h_ERROR
   sDesbloqueaElWinCont
   gDbUtil.sDestroyRecordSet mrsCompania
   gDbUtil.sDestroyConnection mCnWincont
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Form_Unload", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtPath_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If txtPath.Text = "" Then
      gMessage.Advertencia "Debe proporcionar una ruta válida para la base de datos del WinCont."
'      Cancel = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
          "txtPath_Validate", CM_FILE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCheckForSpecialKeys(ByVal valKeyCode As Integer, ByVal valShift As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, valKeyCode) Then
   ElseIf valKeyCode = vbKeyF6 Then
      gAPI.ssSetFocus cmdGrabar
      cmdGrabar_Click
   ElseIf valKeyCode = vbKeyEscape Then
      gAPI.ssSetFocus CmdSalir
      cmdSalir_Click
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "sCheckForSpecialKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitDefaultValues()
   On Error GoTo h_ERROR
   Set mCnWincont = New ADODB.Connection
   Set mrsCompania = New ADODB.Recordset
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "sInitDefaultValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInitLookAndFeel(ByVal valNombreFileLog As String)
   On Error GoTo h_ERROR
   gUtilRespaldo.setNombreFileLogRestauracion valNombreFileLog
   If gUtilFile.fBorraElArchivo(valNombreFileLog) Then
   End If
   mNombreDelSistemaActual = "Sistema " & GetNombreDelPrograma(gDefProg.GetSiglasDelPrograma)
   Show vbModal
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sFillListWithNombresCompanias()
   Dim varPuedoContinuar As Boolean
   Dim varSQL As String
   On Error GoTo h_ERROR
   varPuedoContinuar = gDbUtil.fConnectionIsOpened(mCnWincont)
   If ((varPuedoContinuar) And (lstCompanias.ListCount < 1)) Then
      lstCompanias.Clear
      varSQL = "SELECT Codigo + ': ' + Nombre, Compania.* " _
            & " FROM Compania " _
            & " WHERE EsCatalogoGeneral  = 'N'" _
            & " ORDER BY Nombre "
      gDbUtil.sCloseIfOpened mrsCompania
      If gDbUtil.fOpenRecordSetAllParameters(mrsCompania, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
         If mrsCompania.RecordCount > 0 Then
            While Not mrsCompania.EOF
               lstCompanias.AddItem mrsCompania(0).value
               mrsCompania.MoveNext
            Wend
            lstCompanias.ListIndex = 0
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
          "sFillListWithNombresCompanias", CM_FILE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fNombreCompaniaAImportar() As String
   Dim varStrTemp As String
   On Error GoTo h_ERROR
   varStrTemp = gAPI.fSelectedElementInListBoxToString(lstCompanias)
   varStrTemp = Trim(gTexto.DfMid(varStrTemp, gTexto.DfInStr(varStrTemp, ":") + 1))
   fNombreCompaniaAImportar = varStrTemp
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
          "fNombreCompaniaAImportar", CM_FILE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCodigoCompaniaAImportar() As String
   Dim varStrTemp As String
   On Error GoTo h_ERROR
   varStrTemp = gAPI.fSelectedElementInListBoxToString(lstCompanias)
   varStrTemp = Trim(gTexto.DfMid(varStrTemp, 1, gTexto.DfInStr(varStrTemp, ":") - 1))
   fCodigoCompaniaAImportar = varStrTemp
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
          "fCodigoCompaniaAImportar", CM_FILE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sSetConsecutivoCompaniaAImportar()
   On Error GoTo h_ERROR
   mrsCompania.Filter = "Codigo = " & fCodigoCompaniaAImportar
   If mrsCompania.RecordCount > 0 Then
      mConsecutivoCompaniaWincont = mrsCompania("ConsecutivoCompania").value
   End If
   mrsCompania.Filter = adFilterNone
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
          "sSetConsecutivoCompaniaAImportar", CM_FILE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fPuedoLlenarLaListaConLasCompaniasDelWincont() As Boolean
   Dim varPuedoContinuar As Boolean
   On Error GoTo h_ERROR
   varPuedoContinuar = (txtPath.Text <> "")
   If varPuedoContinuar Then
      varPuedoContinuar = Not gUtilFile.fExisteElArchivo(UCase(txtPath.Text & "\SUPERVIS.LDB"))
      If Not varPuedoContinuar Then
         gMessage.AlertMessage "La base de datos " & UCase(txtPath.Text) & " está siendo utilizada en este momento." _
               & " Posiblemente hay algún usuario ejecutando el WinCont o algún Supervisor del sistema está efectuando operaciones " _
               & " de Mantenimiento de base de datos desde el Superutilitario." _
               & vbCr & "Dada la naturaleza de este proceso, bajo ningún concepto pueden haber usuarios accesando la base de datos del Wincont, " _
               & "por lo tanto deberá seleccionar otra base de datos o salir del proceso.", "BASE DE DATOS EN USO"
      Else
         On Error Resume Next
         If gUtilFile.fCreateLockingFile(txtPath.Text & "\REES_AYR.TXT", gProyUsuarioActual.GetNombreYApellido, mLockingFileReest) Then
         End If
         If gUtilFile.fCreateLockingFile(txtPath.Text & "\SUPERVIS.LDB", gProyUsuarioActual.GetNombreYApellido, mLockingFileSupervisor) Then
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   fPuedoLlenarLaListaConLasCompaniasDelWincont = varPuedoContinuar
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
          "fPuedoLlenarLaListaConLasCompaniasDelWincont", CM_FILE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fMensajeInicioConversion() As String
   Dim varMensaje As String
   varMensaje = "El siguiente proceso va a "
   If gProyCompaniaActual.GetUsaModuloDeContabilidad Then
      varMensaje = varMensaje & " SUSTITUIR TODA la información contable que actualmente existe en su "
   Else
      varMensaje = varMensaje & " activar el uso del módulo contable del "
   End If
   varMensaje = varMensaje & mNombreDelSistemaActual & ", "
   varMensaje = varMensaje & " tomando como referencia la Empresa " & vbCr & Space(5) & "'" & fNombreCompaniaAImportar & "' " _
               & vbCr & " de la base de datos de contabilidad que se encuentra en el directorio: " & vbCr & Space(5) & UCase(txtPath.Text)
   varMensaje = varMensaje & vbCr & "Este proceso es irreversible." & vbCrLf & "Está seguro que desea continuar con el mismo?"
   fMensajeInicioConversion = varMensaje
End Function

Private Sub sEliminaLaInformacionContableAsociadaALaCompaniaActual(ByVal valRaiseError As Boolean)
   Dim varPeriodoNavigator As clsPeriodoNavigator
   Dim varLoteNavigator As clsLoteNavigator
   Dim varParametrosActivo As clsParametrosActivoFijoNav
   Dim varSQL As String
   Dim varOtherUserUpdatedFirst As Boolean
   On Error GoTo h_ERROR
   If gProyCompaniaActual.GetUsaModuloDeContabilidad Then
      txtProgreso.Text = "Eliminando información contable anterior..."
      txtProgreso.Refresh
   End If
   Set varPeriodoNavigator = New clsPeriodoNavigator
   While varPeriodoNavigator.SearchForEscogerPeriodoParaEliminar(gProyCompaniaActual.GetConsecutivoCompania)
      If varPeriodoNavigator.fDeleteRecord(varOtherUserUpdatedFirst) Then
      End If
   Wend
   Set varPeriodoNavigator = Nothing
   Set varLoteNavigator = New clsLoteNavigator
   varSQL = "DELETE FROM " & varLoteNavigator.GetTableName & " WHERE ConsecutivoCompania  = " & gProyCompaniaActual.GetConsecutivoCompania
   gDbUtil.Execute gDefDatabase.Conexion, varSQL
   Set varLoteNavigator = Nothing
   Set varParametrosActivo = New clsParametrosActivoFijoNav
   varSQL = "DELETE FROM " & varParametrosActivo.GetTableName & " WHERE ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   gDbUtil.Execute gDefDatabase.Conexion, varSQL
   Set varParametrosActivo = Nothing
   varSQL = "DELETE FROM " & gProyReglasDeContabilizacion.GetTableName & " WHERE ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   gDbUtil.Execute gDefDatabase.Conexion, varSQL
   gProyReglasDeContabilizacion.sClrRecord
   gProyReglasDeContabilizacion.SetConsecutivoCompania gProyCompaniaActual.GetConsecutivoCompania
   If gProyParametrosCompania.fResetFechaDeInicioContabilizacion(gDbUtil.getMinValueForDateType) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: If Not valRaiseError Then
      gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
          "sEliminaLaInformacionContableAsociadaALaCompaniaActual", CM_FILE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
   Else
      Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
          "sEliminaLaInformacionContableAsociadaALaCompaniaActual", CM_FILE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
   End If
End Sub

Private Function fEjecutaProcesoDeImportarDataDelWincont(ByVal valVersionBDDWinCont As String) As Boolean
   Dim varConExito As Boolean
   On Error GoTo h_ERROR
   varConExito = False
   gUtilRespaldo.sInicializaLosValoresDeClase txtProgreso, prgBarImportar, Importar, _
         gProyCompaniaActual.GetNombre, gProyCompaniaActual.GetCodigo
   sImportaParametrosWincont valVersionBDDWinCont
   sImportaTipoDeComprobante
   sImportaLote
   sImportaParametrosActivoFijo
   If fImportaLosPeriodos(valVersionBDDWinCont) Then
      varConExito = True
      sImportaElComprobanteDeApertura
      sModificaLosParametrosContablesDeCompania
      sActivaLaOpcionUsarContabilidadALosSupervisoresYElUsuarioActual
   End If
h_EXIT: On Error GoTo 0
   fEjecutaProcesoDeImportarDataDelWincont = varConExito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fEjecutaProcesoDeImportarDataDelWincont", CM_FILE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sImportaAuxiliar(ByVal valConsecutivoPeriodoWincontActual As Long)
   Dim auxiliarNavigator  As clsAuxiliarNavigator
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set auxiliarNavigator = New clsAuxiliarNavigator
   Set varRSTemp = New ADODB.Recordset
   gUtilRespaldo.sModificarTextoDelProgreso auxiliarNavigator.GetTableName
   varSQL = "SELECT * FROM " & auxiliarNavigator.GetTableName & " WHERE ConsecutivoPeriodo = " & valConsecutivoPeriodoWincontActual
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      If varRSTemp.RecordCount > 0 Then
         While Not varRSTemp.EOF
            auxiliarNavigator.sRestaurarDatos gContPeriodoActual.GetConsecutivoPeriodo, valConsecutivoPeriodoWincontActual, varRSTemp
            varRSTemp.MoveNext
            gUtilRespaldo.sAddProgress
         Wend
      End If
   End If
   Set auxiliarNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaAuxiliar", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImportaCentroDeCosto(ByVal valConsecutivoPeriodoWincontActual As Long)
   Dim CentroDeCostoNavigator  As clsCentroDeCostosNavigator
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set CentroDeCostoNavigator = New clsCentroDeCostosNavigator
   Set varRSTemp = New ADODB.Recordset
   varSQL = "SELECT * FROM Contab.CentroDeCostos WHERE ConsecutivoPeriodo = " & valConsecutivoPeriodoWincontActual
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      If varRSTemp.RecordCount > 0 Then
         gUtilRespaldo.sModificarTextoDelProgreso CentroDeCostoNavigator.GetTableName
         CentroDeCostoNavigator.sRestaurarDatos varRSTemp, gProyCompaniaActual.GetConsecutivoCompania, gContPeriodoActual.GetConsecutivoCompania, gContPeriodoActual.GetConsecutivoPeriodo, valConsecutivoPeriodoWincontActual
         gUtilRespaldo.sAddProgress
      End If
   End If
   Set CentroDeCostoNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaCentroDeCosto", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImportaGrupoDeActivo(ByVal valConsecutivoPeriodoWincontActual As Long)
   Dim GrupodeActivoNavigator  As clsGrupoDeActivosNavigator
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set GrupodeActivoNavigator = New clsGrupoDeActivosNavigator
   Set varRSTemp = New ADODB.Recordset
   gUtilRespaldo.sModificarTextoDelProgreso GrupodeActivoNavigator.GetTableName
   varSQL = "SELECT * FROM " & GrupodeActivoNavigator.GetTableName & " WHERE ConsecutivoPeriodo = " & valConsecutivoPeriodoWincontActual
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      If varRSTemp.RecordCount > 0 Then
         While Not varRSTemp.EOF
            GrupodeActivoNavigator.sAddDirectoDesdeRecordsetExterno gContPeriodoActual.GetConsecutivoPeriodo, varRSTemp
            varRSTemp.MoveNext
            gUtilRespaldo.sAddProgress
         Wend
      End If
   End If
   Set GrupodeActivoNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaGrupoDeActivo", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImportaCuenta(ByVal valConsecutivoPeriodoWincontActual As Long, ByVal valVersionBDD As String)
   Dim cuentaNavigator  As clsCuentaNavigator
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set cuentaNavigator = New clsCuentaNavigator
   Set varRSTemp = New ADODB.Recordset
   gUtilRespaldo.sModificarTextoDelProgreso cuentaNavigator.GetTableName
   varSQL = "SELECT * FROM " & cuentaNavigator.GetTableName & " WHERE ConsecutivoPeriodo = " & valConsecutivoPeriodoWincontActual
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      If varRSTemp.RecordCount > 0 Then
         While Not varRSTemp.EOF
            cuentaNavigator.sAddDirectoDesdeRecordsetExterno gContPeriodoActual.GetConsecutivoPeriodo, varRSTemp, valVersionBDD
            varRSTemp.MoveNext
            gUtilRespaldo.sAddProgress
         Wend
      End If
   End If
   Set cuentaNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaCuenta", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImportaGrupoDeInventario(ByVal valConsecutivoPeriodoWincontActual As Long)
   Dim grupoDeInventarioNavigator  As clsGrupoDeInventarioNav
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set grupoDeInventarioNavigator = New clsGrupoDeInventarioNav
   Set varRSTemp = New ADODB.Recordset
   gUtilRespaldo.sModificarTextoDelProgreso grupoDeInventarioNavigator.GetTableName
   varSQL = "SELECT * FROM " & grupoDeInventarioNavigator.GetTableName & " WHERE ConsecutivoPeriodo = " & valConsecutivoPeriodoWincontActual
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      If varRSTemp.RecordCount > 0 Then
         While Not varRSTemp.EOF
            grupoDeInventarioNavigator.sAddDirectoDesdeRecordsetExterno gContPeriodoActual.GetConsecutivoPeriodo, varRSTemp
            varRSTemp.MoveNext
            gUtilRespaldo.sAddProgress
         Wend
      End If
   End If
   Set grupoDeInventarioNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaGrupoDeInventario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImportaActivoFijo(ByVal valConsecutivoPeriodoWincontActual As Long)
   Dim activoFijoNavigator  As clsActivoFijoNavigator
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set activoFijoNavigator = New clsActivoFijoNavigator
   Set varRSTemp = New ADODB.Recordset
   gUtilRespaldo.sModificarTextoDelProgreso activoFijoNavigator.GetTableName
   varSQL = "SELECT * FROM " & activoFijoNavigator.GetTableName & " WHERE ConsecutivoPeriodo = " & valConsecutivoPeriodoWincontActual
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      If varRSTemp.RecordCount > 0 Then
         While Not varRSTemp.EOF
            activoFijoNavigator.sAddDirectoDesdeRecordsetExterno gContPeriodoActual.GetConsecutivoPeriodo, varRSTemp
            varRSTemp.MoveNext
            gUtilRespaldo.sAddProgress
         Wend
      End If
   End If
   Set activoFijoNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaActivoFijo", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImportaComprobante(ByVal valConsecutivoPeriodoWincontActual As Long)
   Dim comprobanteNavigator  As clsComprobanteNavigator
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set comprobanteNavigator = New clsComprobanteNavigator
   Set varRSTemp = New ADODB.Recordset
   gUtilRespaldo.sModificarTextoDelProgreso comprobanteNavigator.GetTableName
   If TrabajandoConSaldoInicialContabilidad Then
      varSQL = "SELECT * FROM " & comprobanteNavigator.GetTableName & " WHERE ConsecutivoPeriodo" _
            & " IN (SELECT ConsecutivoPeriodo FROM " & gContPeriodoActual.GetTableName & " WHERE ConsecutivoCompania = " & mConsecutivoCompaniaWincont & ")"
   Else
      varSQL = "SELECT * FROM " & comprobanteNavigator.GetTableName & " WHERE ConsecutivoPeriodo = " & valConsecutivoPeriodoWincontActual
   End If
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      If varRSTemp.RecordCount > 0 Then
         While Not varRSTemp.EOF
            comprobanteNavigator.sAddDirectoDesdeRecordsetExterno gContPeriodoActual.GetConsecutivoPeriodo, varRSTemp
            varRSTemp.MoveNext
            gUtilRespaldo.sAddProgress
         Wend
      End If
   End If
   Set comprobanteNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaComprobante", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImportaAsiento(ByVal valConsecutivoPeriodoWincontActual As Long)
   Dim asientoNavigator  As clsAsientoNavigator
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set asientoNavigator = New clsAsientoNavigator
   Set varRSTemp = New ADODB.Recordset
   gUtilRespaldo.sModificarTextoDelProgreso asientoNavigator.GetTableName
   If TrabajandoConSaldoInicialContabilidad Then
      varSQL = "SELECT * FROM " & asientoNavigator.GetTableName & " WHERE ConsecutivoPeriodo" _
            & " IN (SELECT ConsecutivoPeriodo FROM " & gContPeriodoActual.GetTableName & " WHERE ConsecutivoCompania = " & mConsecutivoCompaniaWincont & ")"
   Else
      varSQL = "SELECT * FROM " & asientoNavigator.GetTableName & " WHERE ConsecutivoPeriodo = " & valConsecutivoPeriodoWincontActual
   End If
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      If varRSTemp.RecordCount > 0 Then
         While Not varRSTemp.EOF
            asientoNavigator.sAddDirectoDesdeRecordsetExterno gContPeriodoActual.GetConsecutivoPeriodo, varRSTemp
            varRSTemp.MoveNext
            gUtilRespaldo.sAddProgress
         Wend
      End If
   End If
   Set asientoNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaAsiento", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImportaTipoDeComprobante()
   Dim TipoDeComprobanteNavigator As clsTipoDeComprobanteNav
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set TipoDeComprobanteNavigator = New clsTipoDeComprobanteNav
   Set varRSTemp = New ADODB.Recordset
   gUtilRespaldo.sModificarTextoDelProgreso "TipoDeComprobante"
   varSQL = "SELECT * FROM Contab.TipoDeComprobante"
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      If gDbUtil.fRecordCount(varRSTemp) > 0 Then
         TipoDeComprobanteNavigator.sRestaurarDatos varRSTemp
         gUtilRespaldo.sAddProgress
      End If
   End If
   Set TipoDeComprobanteNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaTipoDeComprobante", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImportaParametrosWincont(ByVal valDBVersion As String)
   Dim varParametrosWincontNavigator As clsParametrosNavigator
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set varParametrosWincontNavigator = New clsParametrosNavigator
   Set varRSTemp = New ADODB.Recordset
   gUtilRespaldo.sModificarTextoDelProgreso varParametrosWincontNavigator.GetTableName
   varSQL = "SELECT * FROM " & varParametrosWincontNavigator.GetTableName
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      If varRSTemp.RecordCount = 1 Then
         varSQL = "DELETE FROM " & varParametrosWincontNavigator.GetTableName
         gDbUtil.Execute gDefDatabase.Conexion, varSQL
         If Not varParametrosWincontNavigator.fSearch() Then
            varParametrosWincontNavigator.sRestaurarDatos varRSTemp, valDBVersion
         End If
         gUtilRespaldo.sAddProgress
      End If
   End If
   Set varParametrosWincontNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaParametrosWincont", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImportaLote()
   Dim LoteNavigator As clsLoteNavigator
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set LoteNavigator = New clsLoteNavigator
   Set varRSTemp = New ADODB.Recordset
   gUtilRespaldo.sModificarTextoDelProgreso LoteNavigator.GetTableName
   varSQL = "SELECT * FROM " & LoteNavigator.GetTableName & " WHERE ConsecutivoCompania = " & mConsecutivoCompaniaWincont
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      While Not varRSTemp.EOF
         LoteNavigator.sAddDirectoDesdeRecordsetExterno gProyCompaniaActual.GetConsecutivoCompania, varRSTemp
         gUtilRespaldo.sAddProgress
         varRSTemp.MoveNext
      Wend
   End If
   Set LoteNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaLote", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImportaParametrosActivoFijo()
   Dim ParametrosActivoFijoNavigator As clsParametrosActivoFijoNav
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   On Error GoTo h_ERROR
   Set ParametrosActivoFijoNavigator = New clsParametrosActivoFijoNav
   Set varRSTemp = New ADODB.Recordset
   gUtilRespaldo.sModificarTextoDelProgreso ParametrosActivoFijoNavigator.GetTableName
   varSQL = "SELECT * FROM " & ParametrosActivoFijoNavigator.GetTableName & " WHERE ConsecutivoCompania = " & mConsecutivoCompaniaWincont
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      While Not varRSTemp.EOF
         ParametrosActivoFijoNavigator.sAddDirectoDesdeRecordsetExterno gProyCompaniaActual.GetConsecutivoCompania, varRSTemp
         gUtilRespaldo.sAddProgress
         varRSTemp.MoveNext
      Wend
   End If
   Set ParametrosActivoFijoNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaParametrosActivoFijo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sImportaDatosDeTablasRelacionadasConPeriodo(ByVal valConsecutivoPeriodoWincontActual As Long, ByVal valVersionBDD As String)
   On Error GoTo h_ERROR
   sImportaPeriodoRenglon valConsecutivoPeriodoWincontActual
   sImportaAuxiliar valConsecutivoPeriodoWincontActual
   sImportaCentroDeCosto valConsecutivoPeriodoWincontActual
   sImportaGrupoDeActivo valConsecutivoPeriodoWincontActual
   sImportaCuenta valConsecutivoPeriodoWincontActual, valVersionBDD
   sImportaGrupoDeInventario valConsecutivoPeriodoWincontActual
   sImportaActivoFijo valConsecutivoPeriodoWincontActual
   sImportaComprobante valConsecutivoPeriodoWincontActual
   sImportaAsiento valConsecutivoPeriodoWincontActual
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaDatosDeTablasRelacionadasConPeriodo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fImportaLosPeriodos(ByVal valVersionBDD As String) As Boolean
   Dim periodoNavigator As clsPeriodoNavigator
   Dim varRSTemp As ADODB.Recordset
   Dim varSQL As String
   Dim varSeImportoAlMenosUno As Boolean
   On Error GoTo h_ERROR
   Set periodoNavigator = New clsPeriodoNavigator
   Set varRSTemp = New ADODB.Recordset
   fImportaLosPeriodos = False
   varSeImportoAlMenosUno = False
   gUtilRespaldo.sModificarTextoDelProgreso periodoNavigator.GetTableName
   varSQL = "SELECT * FROM " & periodoNavigator.GetTableName & " WHERE ConsecutivoCompania = " & mConsecutivoCompaniaWincont
   If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
      While Not varRSTemp.EOF
         periodoNavigator.sAddDirectoDesdeRecordsetExterno gProyCompaniaActual.GetConsecutivoCompania, varRSTemp
         If gContPeriodoActual.SearchAndFillByConsecutivo(periodoNavigator.GetConsecutivoPeriodo) Then
            varSeImportoAlMenosUno = True
            sImportaDatosDeTablasRelacionadasConPeriodo varRSTemp("ConsecutivoPeriodo").value, valVersionBDD
         End If
         gUtilRespaldo.sAddProgress
         varRSTemp.MoveNext
      Wend
   End If
   Set periodoNavigator = Nothing
   gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   fImportaLosPeriodos = varSeImportoAlMenosUno
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fImportaLosPeriodos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sImportaElComprobanteDeApertura()
   On Error GoTo h_ERROR
   If gContPeriodoActual.BuscaElPrimerPeriodoDeLaCompania(gProyCompaniaActual.GetConsecutivoCompania) Then
      TrabajandoConSaldoInicialContabilidad = True
      sImportaComprobante 0
      sImportaAsiento 0
      TrabajandoConSaldoInicialContabilidad = False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: TrabajandoConSaldoInicialContabilidad = False
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaElComprobanteDeApertura", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sModificaLosParametrosContablesDeCompania()
   Dim varOtherUserUpdateFirst As Boolean
   On Error GoTo h_ERROR
   mrsCompania.Filter = "Codigo = " & fCodigoCompaniaAImportar
   If mrsCompania.RecordCount > 0 Then
      gProyCompaniaActual.SetUsaAuxiliares gConvert.ConvertStringToBoolean(mrsCompania("UsaAuxiliares").value)
      gProyCompaniaActual.SetUsaCentroDeCostos gConvert.ConvertStringToBoolean(mrsCompania("UsaCentroDeCostos").value)
      gProyCompaniaActual.SetUsaConexionConAXI gConvert.ConvertStringToBoolean(mrsCompania("UsaConexionConAxi").value)
      gProyCompaniaActual.SetUsaConexionConISLR gConvert.ConvertStringToBoolean(mrsCompania("UsaConexionConIslr").value)
      gProyCompaniaActual.SetUsaCostoDeVentas gConvert.ConvertStringToBoolean(mrsCompania("UsaCostoDeVentas").value)
      gProyCompaniaActual.SetUsaModuloDeActivoFijo gConvert.ConvertStringToBoolean(mrsCompania("UsaModuloDeActivoFijo").value)
      gProyCompaniaActual.SetUsaModuloDeContabilidad True
      If gProyCompaniaActual.fUpdateRecord(varOtherUserUpdateFirst, Modificar) Then
      End If
      If gProyParametrosCompania.fResetFechaDeInicioContabilizacion(gDefgen.fGetValorDeFechaParaInicializarCampo) Then
      End If
   End If
   mrsCompania.Filter = adFilterNone
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sModificaLosParametrosContablesDeCompania", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fEscogeLosNuevosParametrosParaLaCompaniaActual() As Boolean
   On Error GoTo h_ERROR
   fEscogeLosNuevosParametrosParaLaCompaniaActual = False
   gProyReglasDeContabilizacion.sEscogeReglasDeContabilizacionDeLaCiaActualSiNoExisteLoCrea False
   If gProyParametrosWinCont.fEscogeElRecordDeParametrosActual Then
   End If
   If gProyParametrosCompania.EscogeParametrosCompaniaActual Then
   End If
   If gProyParametrosActivo.fEscogeElRecordDeParametrosActivo Then
   End If
   gProyUsuarioActual.sVuelveALeerLosDatosDelUsuarioActual
   gContPeriodoActual.EscogeElUltimoPeriodoDeLaCompania gProyCompaniaActual.GetConsecutivoCompania
h_EXIT: On Error GoTo 0
   fEscogeLosNuevosParametrosParaLaCompaniaActual = True
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fEscogeLosNuevosParametrosParaLaCompaniaActual", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sModificaLasReglasDeContabilizacion()
   Dim varReglasDeContabilizacionMenu As clsReglasDeContabilizacionMenu
   On Error GoTo h_ERROR
   Set varReglasDeContabilizacionMenu = New clsReglasDeContabilizacionMenu
   varReglasDeContabilizacionMenu.sBuscaElRecordDeReglasDeContabilizacionYejecutaLaAccion Modificar
   Set varReglasDeContabilizacionMenu = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sEscogeLosNuevosParametrosParaLaCompaniaActual", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivaLaOpcionUsarContabilidadALosSupervisoresYElUsuarioActual()
   Dim varSQL As String
   Dim insUsuarioNavigator As clsUsuarioNavigator
   On Error GoTo h_ERROR
   Set insUsuarioNavigator = New clsUsuarioNavigator
   If (gDefProg.GetSiglasDelPrograma = GetSiglasSAW) Then
      varSQL = gUtilSQL.fSQLUpdateWithNJoin("Lib.GUserSecurity", " SET HasAccess = 'S'", "Lib.GUserSecurity INNER JOIN Lib.GUser ON Lib.GUserSecurity.UserName = Lib.GUser.UserName", _
      " WHERE Lib.GUserSecurity.ProjectModule = " & gUtilSQL.fSimpleSqlValue("Contabilidad") & " AND ProjectAction = " & gUtilSQL.fSimpleSqlValue("Usa Módulo de Contabilidad") _
      & " AND Lib.GUser.Status = '0' AND (Lib.GUser.IsSuperviser = 'S' OR Lib.GUser.UserName = " & gUtilSQL.fSimpleSqlValue(gProyUsuarioActual.GetNombreDelUsuario) & ")")
   Else
   varSQL = "UPDATE " & insUsuarioNavigator.GetTableName _
            & " SET UsaContabilidad = 'S'" _
            & " WHERE EsSupervisor = 'S'" _
            & " OR NombreDelUsuario = '" & gProyUsuarioActual.GetNombreDelUsuario & "'"
   End If
   gDbUtil.Execute Conexion, varSQL
   gProyUsuarioActual.sVuelveALeerLosDatosDelUsuarioActual
   Set insUsuarioNavigator = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sActivaLaOpcionUsarContabilidadALosSupervisoresYElUsuarioActual", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sDesbloqueaElWinCont()
   On Error Resume Next
   If txtPath.Text <> "" Then
      If Not gUtilFile.fDeleteLockingFile(txtPath.Text & "\REES_AYR.TXT", mLockingFileReest) Then
         gMessage.Advertencia "No pudo ser eliminado el archivo: '" & txtPath.Text & "\REES_AYR.TXT'" _
               & vbCr & "Si tiene problemas para ingresar al Wincont elimínelo manualmente."
      End If
      If Not gUtilFile.fDeleteLockingFile(txtPath.Text & "\SUPERVIS.LDB", mLockingFileSupervisor) Then
         gMessage.Advertencia "No pudo ser eliminado el archivo: '" & txtPath.Text & "\SUPERVIS.LDB'" _
               & vbCr & "Si tiene problemas para ingresar al Wincont elimínelo manualmente."
      End If
   End If
   On Error GoTo 0
End Sub

Private Function fLosDatosEscogidosSonValidos() As Boolean
   Dim varSonValidos As Boolean
   On Error GoTo h_ERROR
   varSonValidos = True
   fLosDatosEscogidosSonValidos = False
   If txtPath.Text = "" Then
      varSonValidos = False
   End If
   If Not varSonValidos Then
      gMessage.Advertencia "Debe proporcionar una ruta válida para la base de datos del WinCont."
      GoTo h_EXIT
   End If
   If gAPI.fSelectedElementInListBoxToString(lstCompanias) = "" Then
      gMessage.Advertencia "Debe escoger una base de datos que tenga alguna Empresa."
      varSonValidos = False
   End If
h_EXIT: On Error GoTo 0
   fLosDatosEscogidosSonValidos = varSonValidos
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fLosDatosEscogidosSonValidos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sSiLaEmpresaUsaAuxiliaresMandaMensaje()
   On Error GoTo h_ERROR
   If gProyCompaniaActual.GetUsaAuxiliares Then
      gMessage.InformationMessage "El Módulo Contable tiene activada la opción 'Usar Auxiliares'. " _
            & vbCr & "Debe dirigirse al menú de Auxiliares, por Modo Avanzado y ejecutar los procesos: " _
            & vbCr & Space(10) & " * Generar Auxiliares de Clientes" _
            & vbCr & Space(10) & " * Generar Auxiliares de Proveedores" _
            & vbCr & "para que la contabilización automática se ejecute correctamente.", "Módulo de Auxiliares"
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sSiLaEmpresaUsaAuxiliaresMandaMensaje", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sModificarLaFechaDeInicioDeContabilizacion()
   Dim varInput As String
   Dim varEsValido As Boolean
   Dim varFecha As Date
   On Error GoTo h_ERROR
   varEsValido = False
   While Not varEsValido
      varInput = gMessage.fGalacInputBox("Introduzca la Fecha de Inicio de Contabilización:", "Parámetro de la Compañía", False)
      varEsValido = gUtilDate.fElStringContieneUnaFechaValida(varInput, True)
   Wend
   varFecha = gConvert.fConvertStringToDate(varInput)
   varEsValido = gContPeriodoActual.fLaFechaInicioContabilizacionEsValida(varFecha)
   If Not varEsValido Then
      gMessage.Advertencia "La fecha introducida no se encuentra en ningún período contable. Se le asignará la Fecha de Cierre de este período, puede modificarla luego en los parámetros de la empresa."
      varFecha = gContPeriodoActual.GetFechaCierreDelPeriodo
   End If
   If gProyParametrosCompania.fResetFechaDeInicioContabilizacion(varFecha) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sModificarLaFechaDeInicioDeContabilizacion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

'************* AYRG OJO FUNCION DE LIBRERIA *******************
'Estaba en clsDbConnect pero por problema de memoria de el sistema interno la saqué de allí _
y la dejo en este proceso el cual hasta ahora es el único que lo usa.

Private Function fConnectWithOtherSystem(ByVal valSiglasDelSistemaAConectar As String, ByVal valPuedoConectarSiEstaEnUso As Boolean, ByRef refPathDeTrabajo As String, _
            ByRef refConnection As ADODB.Connection) As Boolean
   '   Dim insFrmReadIni As frmReadExternalConnection
   Dim varIsOpened As Boolean
'   Dim varContinuar As Boolean
   Dim insLibGalacForms As clsLibGalacForms
   On Error GoTo h_ERROR
'   varIsOpened = False
'   refPathDeTrabajo = ""
'   gDbUtil.sCloseConnectionIfOpened refConnection
'   Set insFrmReadIni = New frmReadExternalConnection
'   insFrmReadIni.sInitLookAndFeel valSiglasDelSistemaAConectar, True
'   If insFrmReadIni.getDBNameMDB <> "" Or insFrmReadIni.getDBNameSQL <> "" Then
'      If (Not valPuedoConectarSiEstaEnUso) And (Not insFrmReadIni.getIsSQLDataBase) Then
'         varContinuar = Not (gUtilFile.fExisteElArchivo(gTexto.DfReplace(insFrmReadIni.getDBNameMDB, ".MDB", ".LDB")))
'      Else
'         varContinuar = True
'      End If
'      If varContinuar Then
'         If refConnection Is Nothing Then
'            Set refConnection = New ADODB.Connection
'         End If
'         varIsOpened = gDbUtil.fOpenConnection(gDbUtil.fConnectionString(insFrmReadIni.getDBNameMDB, insFrmReadIni.getIsSQLDataBase, insFrmReadIni.getUseDSN, insFrmReadIni.getSecurityMode, insFrmReadIni.getServerName, insFrmReadIni.getDBNameSQL, insFrmReadIni.getDSN, insFrmReadIni.getUserAndPasswordWithConnectionString, insFrmReadIni.getUserID, insFrmReadIni.getUserPwd), _
'               insFrmReadIni.getIsSQLDataBase, insFrmReadIni.getSecurityMode, refConnection, insFrmReadIni.getUserAndPasswordWithConnectionString, insFrmReadIni.getUserID, insFrmReadIni.getUserPwd)
'         If varIsOpened Then
'            refPathDeTrabajo = insFrmReadIni.getPathBaseDeDatos
'         End If
'      Else
'         gMessage.AlertMessage "La Base de Datos seleccionada está en uso. Escoga otra ó dé instrucciones para salir del sistema que la está utilizando.", "BASE DE DATOS EN USO"
'      End If
'   Else
'      gMessage.AlertMessage "No se proporcionó información válida para conectarse con la base de datos.", "Información No Válida"
'   End If
'   Set insFrmReadIni = Nothing
   Set insLibGalacForms = New clsLibGalacForms
   varIsOpened = insLibGalacForms.fInvokeReadExternalConnectionAndConnect(valSiglasDelSistemaAConectar, valPuedoConectarSiEstaEnUso, refPathDeTrabajo, refConnection)
   Set insLibGalacForms = Nothing
   fConnectWithOtherSystem = varIsOpened
h_EXIT: On Error GoTo 0
   fConnectWithOtherSystem = varIsOpened
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConnectWithOtherSystem", CM_FILE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sImportaPeriodoRenglon(ByVal valConsecutivoPeriodoWincontActual As Long)
    Dim insPeriodoRenglon As clsPeriodoRenglonNavigator
    Dim varRSTemp As ADODB.Recordset
    Dim varSQL As String
    On Error GoTo h_ERROR
    Set insPeriodoRenglon = New clsPeriodoRenglonNavigator
    Set varRSTemp = New ADODB.Recordset
    
    varSQL = "SELECT * FROM " & insPeriodoRenglon.GetTableName & " WHERE ConsecutivoPeriodo = " & valConsecutivoPeriodoWincontActual
    If gDbUtil.fOpenRecordSetAllParameters(varRSTemp, varSQL, mCnWincont, adLockOptimistic, adUseClient, adOpenForwardOnly) Then
       If varRSTemp.RecordCount > 0 Then
          While Not varRSTemp.EOF
             insPeriodoRenglon.sAddDirectoDesdeRecordsetExterno gContPeriodoActual.GetConsecutivoPeriodo, varRSTemp
             varRSTemp.MoveNext
             gUtilRespaldo.sAddProgress
          Wend
       End If
    End If
    Set insPeriodoRenglon = Nothing
    gDbUtil.sDestroyRecordSet varRSTemp
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sImportaPeriodoRenglon", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
