VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsInitImportExport"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsInitImportExport"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Exportar/Importar "
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Sub sInitializeInternalClassesImportExport(ByRef refFactura As Object, ByRef refRenglonFactura As Object, ByRef refCxC As Object, ByRef refCxP As Object, ByRef refVendedor As Object, ByRef refCliente As Object, ByRef refMoneda As Object, ByRef refRenglonComisionesDeVendedor As Object, _
      ByRef refProveedor As Object, ByRef refLoteDeAdm As Object, ByRef refColor As Object, ByRef refTalla As Object, ByRef refGrupoTallaColor As Object, ByRef refRenglonGrupoColor As Object, ByRef refRenglonGrupoTalla As Object, ByRef refArticuloInventario As Object, _
      ByRef refCamposMonedaExtranjera As Object, ByRef refExistenciaPorGrupo As Object, ByRef refParametrosCompaniaNavigator As Object, ByRef refNoComunWincont As Object, ByRef refTablaRetencionNavigator As Object, ByRef refTipoProveedorNavigator As Object, ByRef refInputCamposEspeciales As Object, _
      ByRef refCiudadNavigator As Object, ByRef refZonaCobranzaNavigator As Object, ByRef refSectorDeNegocioNavigator As Object, ByRef refCajaNavigator As Object, ByRef refAlmacenNavigator As Object, ByRef refLineaDeProductoNavigator As Object, ByRef refCategoriaNavigator As Object, _
      ByRef refUnidadDeVentaNavigator As Object, ByRef refNotaDeEntradaSalidaNavigator As Object, ByRef refExistenciaPorAlmacenNavigator As Object, ByRef refUtilMenus As Object, ByRef refCuenta As Object, ByRef refCompaniaNavigator As Object, ByRef refRenglonCobroDeFactura As Object, ByRef refAdmAlicuotaIvaActual As Object, _
      ByRef refTipoComprobanteNavigator As Object)
Set insFactura = refFactura
Set insRenglonFactura = refRenglonFactura
Set insCtaContable = refCuenta
Set insCxC = refCxC
Set insCxP = refCxP
Set insVendedor = refVendedor
Set insCliente = refCliente
Set insMoneda = refMoneda
Set insRenglonComisionesDeVendedor = refRenglonComisionesDeVendedor
Set insProveedor = refProveedor
Set insLoteDeAdm = refLoteDeAdm
Set insColor = refColor
Set insTalla = refTalla
Set insGrupoTallaColor = refGrupoTallaColor
Set insRenglonGrupoColor = refRenglonGrupoColor
Set insRenglonGrupoTalla = refRenglonGrupoTalla
Set insArticuloInventario = refArticuloInventario
Set insCamposMonedaExtranjera = refCamposMonedaExtranjera
Set insExistenciaPorGrupo = refExistenciaPorGrupo
Set gProyParametrosCompania = refParametrosCompaniaNavigator
Set gAdmAlicuotaIvaActual = refAdmAlicuotaIvaActual
Set gProyCompaniaActual = refCompaniaNavigator
Set varNoComunWincont = refNoComunWincont
Set insTablaRetencionNavigator = refTablaRetencionNavigator
Set insTipoProveedor = refTipoProveedorNavigator
Set insInputCamposEspeciales = refInputCamposEspeciales 'quitar cuando ayrg avise, instanciar solo al necesitarla
Set insCiudad = refCiudadNavigator
Set insZonaCobranza = refZonaCobranzaNavigator
Set insSectorNegocio = refSectorDeNegocioNavigator
Set insCaja = refCajaNavigator
Set insAlmacen = refAlmacenNavigator
Set insLineaDeProducto = refLineaDeProductoNavigator
Set insCategoriaNav = refCategoriaNavigator
Set insUnidadDeVenta = refUnidadDeVentaNavigator
Set insNotaDeEntrada = refNotaDeEntradaSalidaNavigator
Set insExistenciaNavigator = refExistenciaPorAlmacenNavigator
Set insRenglonCobroDeFactura = refRenglonCobroDeFactura
Set insTipoDeComprobante = refTipoComprobanteNavigator
End Sub

Public Sub sInitializeInternalGlobalClasses(ByRef refInsError As clsError, _
            ByRef refInsMessage As clsMessage, _
            ByRef refInsTexto As clsTexto, _
            ByRef refInsDefGen As clsDefgen, _
            ByRef refInsConvert As clsConvert, _
            ByRef refInsApi As clsDefensiveAPI, _
            ByRef refInsUtilFile As clsUtilFiles, _
            ByRef refInsUtilDate As clsUtilDate, _
            ByRef refInsErrorCodesIG As clsErrorCodesIG, _
            ByRef refInsUtilMathOperations As clsUtilMathOperations, _
            ByRef refEnumAdministrativo As clsEnumAdministrativo, _
            ByRef refEnumContabilidad As clsEnumContabilidad, _
            ByRef refUtilSQLStatement As clsUtilSQLStatement, _
            ByRef refEnumTablaRetencion As clsEnumTablaRetencion, _
            ByRef refDefDatabase As clsDefDatabase, _
            ByRef refDbUtil As clsDBUtil, _
            ByRef refGlobalization As clsUtilGlobalization)
            
      Set gError = refInsError
      Set gMessage = refInsMessage
      Set gTexto = refInsTexto
      Set gDefGen = refInsDefGen
      Set gConvert = refInsConvert
      Set gApi = refInsApi
      Set gUtilFile = refInsUtilFile
      Set gUtilDate = refInsUtilDate
      Set gErrorCodesIG = refInsErrorCodesIG
      Set gUtilMathOperations = refInsUtilMathOperations
      Set gEnumProyecto = refEnumAdministrativo
      Set gEnumProyectoWincont = refEnumContabilidad
      Set gUtilSQL = refUtilSQLStatement
      Set gEnumTablaRetencion = refEnumTablaRetencion
      Set gDefDatabase = refDefDatabase
      Set gDbUtil = refDbUtil
      Set gGlobalization = refGlobalization
     
End Sub

Public Sub sSendDllVersionToLibGalacCI()
   Dim insInicializaClases As clsInitLibGalac
   Set insInicializaClases = New clsInitLibGalac
   insInicializaClases.sAddDllInfoToListOfDllsInUse getDllVersion
   Set insInicializaClases = New clsInitLibGalac
End Sub

Public Sub sLlamaAlFormExportacionImportacion(ByVal valAction As AccionSobreRecord, ByVal valEnumEntidad As enum_EntidadAExportarOImportar, Optional ByVal valSonBorradores As Boolean = False, Optional ByRef refWrpAlmacen As Object, Optional ByRef refgLibGalacDataParse As Object)
   Dim insfrmExportacionImportacion As frmExportacionImportacion
   On Error GoTo h_ERROR
   Set insWrpAlmacen = refWrpAlmacen
   Set gLibGalacDataParse = refgLibGalacDataParse
   Set insfrmExportacionImportacion = New frmExportacionImportacion
   insfrmExportacionImportacion.Show
   insfrmExportacionImportacion.sInitLookAndFeel valAction, valEnumEntidad, valSonBorradores
   If valAction = Importar And valEnumEntidad = eEAEI_FACTURA And Not valSonBorradores Then
      insfrmExportacionImportacion.chkGenerarCxCAlImportar.Visible = True
   End If
h_EXIT: On Error GoTo 0
        Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sLlamaAlFormExportacionImportacion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLlamaAlFormExportacionImportacionEnZip(ByVal valAction As AccionSobreRecord, Optional valEntreFechas As Boolean = False, Optional ByRef refWrpAlmacen As Object, Optional ByRef refgLibGalacDataParse As Object)
   Dim insfrmExportacionImportacionSawZip As frmExportacionImportacionSawZip
   On Error GoTo h_ERROR
   Set insWrpAlmacen = refWrpAlmacen
   Set gLibGalacDataParse = refgLibGalacDataParse
   Set insfrmExportacionImportacionSawZip = New frmExportacionImportacionSawZip
         If valAction = Exportar Or valAction = Importar Then
            insfrmExportacionImportacionSawZip.Show
            insfrmExportacionImportacionSawZip.sInitLookAndFeel valAction, valEntreFechas
         End If
h_EXIT: On Error GoTo 0
        Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sLlamaAlFormExportacionImportacionEnZip", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInitializeInternalGlobalParameters(ByRef refConsecutivoPeriodo As Long, _
      ByRef refConsecutivoCompaniaActual As Long, _
      ByRef refNombreCompaniaActual As String, _
      ByRef refCiudadCompaniaActual As String, _
      ByRef refNombreDelUsuario As String, _
      ByRef refCodigoMonedaLocal As String, ByRef refCodigoMonedaExtranjera As String, _
      ByRef refNombreMonedaLocal As String, ByRef refNombreMonedaExtranjera As String)
            
      mConsecutivoPeriodo = refConsecutivoPeriodo
      mConsecutivoCompaniaActual = refConsecutivoCompaniaActual
      mNombreCompaniaActual = refNombreCompaniaActual
      mCiudadCompaniaActual = refCiudadCompaniaActual
      mNombreDelUsuario = refNombreDelUsuario
      mCodigoMonedaLocal = refCodigoMonedaLocal
      mCodigoMonedaExtranjera = refCodigoMonedaExtranjera
      mNombreMonedaLocal = refNombreMonedaLocal
      mNombreMonedaExtranjera = refNombreMonedaExtranjera
End Sub

Public Sub sLlamaAlFormExportacionMercantil(ByVal valAction As AccionSobreRecord, ByVal valEnumEntidad As enum_EntidadAExportarOImportar, ByRef refCuentaBancariaNavegator As Object, ByVal valUsaCodigoBancoEnPantalla As Boolean, ByRef refConexionAos As Object)
   Dim insfrmExportacionMercantil As frmExportacionMercantil
   On Error GoTo h_ERROR
   Set insfrmExportacionMercantil = New frmExportacionMercantil
   insfrmExportacionMercantil.sInitLookAndFeel valAction, valEnumEntidad, refCuentaBancariaNavegator, valUsaCodigoBancoEnPantalla, refConexionAos
   insfrmExportacionMercantil.Show
h_EXIT: On Error GoTo 0
        Exit Sub
h_ERROR: Err.Raise Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sLlamaAlFormExportacionMercantil", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLlamaAlFormExportacionBOD(ByVal valAction As AccionSobreRecord, ByVal valEnumEntidad As enum_EntidadAExportarOImportar, ByRef refCuentaBancariaNavegator As Object, ByVal valUsaCodigoBancoEnPantalla As Boolean, ByRef refConexionAos As Object)
   Dim insfrmExportacionBOD As frmExportacionBOD
   On Error GoTo h_ERROR
   Set insfrmExportacionBOD = New frmExportacionBOD
   insfrmExportacionBOD.sInitLookAndFeel valAction, valEnumEntidad, refCuentaBancariaNavegator, valUsaCodigoBancoEnPantalla, refConexionAos
   insfrmExportacionBOD.Show
h_EXIT: On Error GoTo 0
        Exit Sub
h_ERROR: Err.Raise Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sLlamaAlFormExportacionBOD", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLlamaAlFormExportacionBanesco(ByVal valAction As AccionSobreRecord, ByVal valEnumEntidad As enum_EntidadAExportarOImportar, ByRef refCuentaBancariaNavegator As Object, ByVal valUsaCodigoBancoEnPantalla As Boolean, ByRef refConexionAos As Object)
   Dim insfrmExportacionBanesco As frmExportacionBanesco
   On Error GoTo h_ERROR
   Set insfrmExportacionBanesco = New frmExportacionBanesco
   insfrmExportacionBanesco.sInitLookAndFeel valAction, valEnumEntidad, refCuentaBancariaNavegator, valUsaCodigoBancoEnPantalla, refConexionAos
   insfrmExportacionBanesco.Show
h_EXIT: On Error GoTo 0
        Exit Sub
h_ERROR: Err.Raise Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sLlamaAlFormExportacionBanesco", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

