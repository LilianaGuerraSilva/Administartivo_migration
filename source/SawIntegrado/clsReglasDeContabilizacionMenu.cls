VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsReglasDeContabilizacionMenu"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsReglasDeContabilizacionMenu"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Reglas De Contabilización"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sCreaElMenuDeReglasDeContabilizacion(ByRef refForma As Form)
   Dim numSubMenu As Long
   Dim ID As Long
   On Error GoTo h_ERROR
   numSubMenu = gUtilMenus.fAddSubMenu(refForma, gEnumProyecto.enum_SystemModulesToStr(eSM_ReglasDeContabilizacion), ID, 1)
   Call gUtilMenus.fAddMenuItem(refForma, gUtilMenus.fCaptionGestionarModulo(gEnumProyecto.enum_SystemModulesToStr(eSM_ReglasDeContabilizacion)), numSubMenu, True, False)
   Call gUtilMenus.fAddMenuItem(refForma, gDefgen.AccionSobreRecordStr(imprimir), numSubMenu, gProyUsuarioActual.GetModificarReglasDeContabilizacion, False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sCreaElMenuDeReglasDeContabilizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sEjecutaAccionEscogidaMenuReglasDeContabilizacion(ByVal valCaptionMenuEscogido As String)
   On Error GoTo h_ERROR
   Select Case valCaptionMenuEscogido
      Case gDefgen.AccionSobreRecordStr(imprimir):   fImprimeReglasDeContabilizacion
      Case Else: sEjecutaAccion insertar
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sEjecutaAccionEscogidaMenuReglasDeContabilizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


Private Sub fImprimeReglasDeContabilizacion()
   Dim insReglasDeContabilizacionNavigator As clsReglasDeContabilizacionNavigator
   On Error GoTo h_ERROR
   Set insReglasDeContabilizacionNavigator = New clsReglasDeContabilizacionNavigator
   insReglasDeContabilizacionNavigator.fImprimeReglasDeContabilizacion
   
   Set insReglasDeContabilizacionNavigator = Nothing
h_EXIT:  On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fImprimeReglasDeContabilizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
 


Private Sub sEjecutaAccion(ByVal valAccion As AccionSobreRecord)
   On Error GoTo h_ERROR
   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
      If Not gContPeriodoActual.ElPeriodoEstaCerrado(True) Then
            sBuscaElRecordDeReglasDeContabilizacionYejecutaLaAccion valAccion
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sEjecutaAccion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
 
 
Public Sub sBuscaElRecordDeReglasDeContabilizacionYejecutaLaAccion(ByVal valAccion As AccionSobreRecord)
   On Error GoTo h_ERROR
      If (gProyReglasDeContabilizacion.fRsRecordCount = 0) Or _
             (gProyReglasDeContabilizacion.GetConsecutivoCompania <> gProyCompaniaActual.GetConsecutivoCompania) Then
         gProyReglasDeContabilizacion.sEscogeReglasDeContabilizacionDeLaCiaActualSiNoExisteLoCrea False
      End If
      sEjecutaAccionInterop valAccion
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sBuscaElRecordDeReglasDeContabilizacionYejecutaLaAccion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaAccionInterop(ByVal valAccion As AccionSobreRecord)
  Dim objWrapper As Object
  Dim vParametros As String
  Dim insLibSaW As clsLibSAW
  Dim insLibServerProperty As clsLibServerProperty
  Dim insReglasReglasDeContabilizacion As clsReglasDeContabilizacionNavigator
   On Error GoTo h_ERROR
   Set insLibSaW = New clsLibSAW
   Set insLibServerProperty = New clsLibServerProperty
   Set objWrapper = CreateObject(gUtilWrp.fWrpClassName("Contabilizacion", "wrpReglasDeContabilizacion"))
   Set insReglasReglasDeContabilizacion = New clsReglasDeContabilizacionNavigator

   If gProyCompaniaActual.fLaCompaniaYElPeriodoFueronEscogidos Then
     If Not gContPeriodoActual.ElPeriodoEstaCerrado(True) Then
        vParametros = "<GpData><ReglasDeContabilizacion><UsaAuxiliares>" & gConvert.ConvertBooleanToString(gProyCompaniaActual.GetUsaAuxiliares) & "</UsaAuxiliares>"
        vParametros = vParametros & "<UsaModuloDeActivoFijo>" & gConvert.ConvertBooleanToString(gProyCompaniaActual.GetUsaModuloDeActivoFijo) & "</UsaModuloDeActivoFijo>"
        vParametros = vParametros & "<ConsecutivoCompania>" & gConvert.fConvierteAString(gProyParametrosCompania.GetConsecutivoCompania) & "</ConsecutivoCompania>"
        vParametros = vParametros & "<ConsecutivoPeriodo>" & gConvert.fConvierteAString(gContPeriodoActual.GetConsecutivoPeriodo) & "</ConsecutivoPeriodo>"
        vParametros = vParametros & "<UsarResumenDiarioDeVentas>" & gConvert.ConvertBooleanToString(gProyParametrosCompania.GetUsarResumenDiarioDeVentas) & "</UsarResumenDiarioDeVentas>"
        vParametros = vParametros & "<UsarRenglonesEnResumenVtas>" & gConvert.ConvertBooleanToString(gProyParametrosCompania.GetUsarRenglonesEnResumenVtas) & "</UsarRenglonesEnResumenVtas>"
        vParametros = vParametros & "<MuestraTipoComprobante>" & gConvert.ConvertBooleanToString(gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO) & "</MuestraTipoComprobante>"
        vParametros = vParametros & "<PuedoUsarOpcionesDeContribuyenteEspecial>" & gConvert.ConvertBooleanToString(gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial) & "</PuedoUsarOpcionesDeContribuyenteEspecial>"
        vParametros = vParametros & "<DesactivaFacturaTipoComprobante>" & gConvert.ConvertBooleanToString(sDesactivaLosTipoComprobanteQueTenganRecordsAsociados(gProyReglasDeContabilizacion.GetFacturaTipoComprobante)) & "</DesactivaFacturaTipoComprobante>"
        vParametros = vParametros & "<DesactivaCxCTipoComprobante>" & gConvert.ConvertBooleanToString(sDesactivaLosTipoComprobanteQueTenganRecordsAsociados(gProyReglasDeContabilizacion.GetCxCTipoComprobante)) & "</DesactivaCxCTipoComprobante>"
        vParametros = vParametros & "<DesactivaCxPTipoComprobante>" & gConvert.ConvertBooleanToString(sDesactivaLosTipoComprobanteQueTenganRecordsAsociados(gProyReglasDeContabilizacion.GetCxPTipoComprobante)) & "</DesactivaCxPTipoComprobante>"
        vParametros = vParametros & "<DesactivaCobranzaTipoComprobante>" & gConvert.ConvertBooleanToString(sDesactivaLosTipoComprobanteQueTenganRecordsAsociados(gProyReglasDeContabilizacion.GetCobranzaTipoComprobante)) & "</DesactivaCobranzaTipoComprobante>"
        vParametros = vParametros & "<DesactivaPagoTipoComprobante>" & gConvert.ConvertBooleanToString(sDesactivaLosTipoComprobanteQueTenganRecordsAsociados(gProyReglasDeContabilizacion.GetPagoTipoComprobante)) & "</DesactivaPagoTipoComprobante>"
        vParametros = vParametros & "<DesactivaBancarioTipoComprobante>" & gConvert.ConvertBooleanToString(sDesactivaLosTipoComprobanteQueTenganRecordsAsociados(gProyReglasDeContabilizacion.GetMovimientoBancarioTipoComprobante)) & "</DesactivaBancarioTipoComprobante>"
        vParametros = vParametros & "<DesactivaAnticipoTipoComprobante>" & gConvert.ConvertBooleanToString(sDesactivaLosTipoComprobanteQueTenganRecordsAsociados(gProyReglasDeContabilizacion.GetAnticipoTipoComprobante)) & "</DesactivaAnticipoTipoComprobante>"
        vParametros = vParametros & "<DesactivaCajaChicaTipoComprobante>" & gConvert.ConvertBooleanToString(sDesactivaLosTipoComprobanteQueTenganRecordsAsociados(gProyReglasDeContabilizacion.GetCajaChicaTipoComprobante)) & "</DesactivaCajaChicaTipoComprobante>"
        vParametros = vParametros & "<Numero>" & gProyReglasDeContabilizacion.GetNumero & "</Numero>"
        vParametros = vParametros & "<RetenerIVAEnCxp>" & gConvert.ConvertBooleanToString(gProyParametrosCompania.GetEnDondeRetenerIVAAsEnum = eDS_CXP) & "</RetenerIVAEnCxp>"
        vParametros = vParametros & "<GetCierreDelEjercicio>" & gContPeriodoActual.GetCierreDelEjercicio & "</GetCierreDelEjercicio>"
        vParametros = vParametros & "<DondeRetenerIVA>" & gProyParametrosCompania.GetEnDondeRetenerIVAAsEnum & "</DondeRetenerIVA>"
        vParametros = vParametros & "<EsContribuyenteEspecial>" & gConvert.ConvertBooleanToString(gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial) & "</EsContribuyenteEspecial>"
        vParametros = vParametros & "<UsarImpuestoTransaccionesFinancieras>" & gConvert.ConvertBooleanToString(gProyParametrosCompania.GetManejaDebitoBancario) & "</UsarImpuestoTransaccionesFinancieras>"
        vParametros = vParametros & "<MaxNumLevels>" & gConvert.fConvierteAString(gContNiveles.GetCantidadMaximaDeNiveles) & "</MaxNumLevels>"
        vParametros = vParametros & "<MaxNumLevelsAtMatrix>" & gConvert.fConvierteAString(0) & "</MaxNumLevelsAtMatrix>"
        vParametros = vParametros & "<MinNumLevels>" & gConvert.fConvierteAString(1) & "</MinNumLevels>"
        vParametros = vParametros & "<MaxLength>" & gConvert.fConvierteAString(gContNiveles.GetLongitudMaximaDelCodigo) & "</MaxLength>"
        vParametros = vParametros & "<UseZeroAtRigth>" & gConvert.fConvierteAString(gContPeriodoActual.GetCuentaConCerosADerecha) & "</UseZeroAtRigth>"
        vParametros = vParametros & "<Niveles>" & gContPeriodoActual.GetNivel1 & ","
        vParametros = vParametros & gContPeriodoActual.GetNivel2 & ","
        vParametros = vParametros & gContPeriodoActual.GetNivel3 & ","
        vParametros = vParametros & gContPeriodoActual.GetNivel4 & ","
        vParametros = vParametros & gContPeriodoActual.GetNivel5 & ","
        vParametros = vParametros & gContPeriodoActual.GetNivel6 & ","
        vParametros = vParametros & gContPeriodoActual.GetNivel7 & ","
        vParametros = vParametros & gContPeriodoActual.GetNivel8 & ","
        vParametros = vParametros & gContPeriodoActual.GetNivel9 & ","
        vParametros = vParametros & gContPeriodoActual.GetNivel10 & "</Niveles>"
        vParametros = vParametros & "<ServerVersionIs2008OrHigher>" & gConvert.ConvertBooleanToString(insLibServerProperty.fSqlServerVersionIs2008OrHigher) & "</ServerVersionIs2008OrHigher>"
        vParametros = vParametros & "<UsarVentasConIvaDiferido>" & gConvert.ConvertBooleanToString(gProyParametrosCompania.GetUsarVentasConIvaDiferido) & "</UsarVentasConIvaDiferido>"
        vParametros = vParametros & "</ReglasDeContabilizacion></GpData>"
        objWrapper.InitializeComponent gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword, gWorkPaths.fAppPath & gDefProg.GetConfigFileName
        objWrapper.InitializeDefProg gDefProg.GetSiglasDelPrograma, gDefProg.GetVersionDelPrograma, gDefProg.GetVersionBaseDeDatos, gConvert.dateToString(gDefProg.GetFechaDeLaVersion), gConvert.dateToString(gDefProg.GetFechaDeLaVersion), gDefgen.GetValorDeLaCaracteristicaEspecial, gDefProg.GetPais, gConvert.fConvierteAString(1), gDefProg.GetAllowUseOfPASOnLine
        objWrapper.Execute gDefgen.AccionSobreRecordStr(valAccion), gConvert.fConvierteAString(gProyCompaniaActual.GetConsecutivoCompania), vParametros
 
        gProyReglasDeContabilizacion.sEscogeReglasDeContabilizacionDeLaCiaActualSiNoExisteLoCrea False
      End If
   End If
    Set insLibServerProperty = Nothing
    Set objWrapper = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sEjecutaAccion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


Private Function sDesactivaLosTipoComprobanteQueTenganRecordsAsociados(ByVal valTipoDeComprobante As String) As Boolean

   Dim vResult As Boolean
   Dim varComprobanteNavigator As clsComprobanteNavigator
   On Error GoTo h_ERROR
   vResult = False
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      Set varComprobanteNavigator = New clsComprobanteNavigator
      If valTipoDeComprobante <> "" Then
         If varComprobanteNavigator.fSearchPorTipoDeComprobante(valTipoDeComprobante, True) Then
           vResult = True
         End If
      End If
   End If
   
   Set varComprobanteNavigator = Nothing
  sDesactivaLosTipoComprobanteQueTenganRecordsAsociados = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sDesactivaLosTipoComprobanteQueTenganRecordsAsociados", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
