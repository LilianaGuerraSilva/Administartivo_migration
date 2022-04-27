VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsInitSawRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Public Sub sInitializeInternalGlobalClasses(ByRef refInsError As clsError, ByRef refInsUtilReports As clsUtilDDReports, _
                                             ByRef refInsUtilMargins As clsUtilMargins, ByRef refInsMessage As clsMessage, _
                                             ByRef refInsUtilDate As clsUtilDate, ByRef refInsDbUtil As clsDBUtil, _
                                             ByRef refInsTexto As clsTexto, ByRef refProyUsuarioActual As Object, _
                                             ByRef refLibGalacDataParse As Object, ByVal valGDefProg As Object, _
                                             ByVal valGGlobalization As clsUtilGlobalization, ByVal valGDefgen As Object, _
                                             ByVal valUtilFile As clsUtilFiles, ByVal valLibControlesGalac As clsLibOtrosControles, _
                                             ByVal valMonedaLocalActual As Object, ByVal valUltimaTasaDeCambio As Object, _
                                             ByVal valUtilWorkPaths As clsUtilWorkPaths, ByVal valListLibrary As Object, _
                                             ByVal valDefDatabaseConexion As ADODB.Connection, ByVal valEnumTablaRetencion As clsEnumTablaRetencion, _
                                             ByVal valAdmAlicuotaIvaActual As Object, ByRef refInsUtilMathOperations As clsUtilMathOperations)
      Set gError = refInsError
      Set gUtilReports = refInsUtilReports
      Set gUtilMargins = refInsUtilMargins
      Set gConvert = New clsConvert
      Set gUtilScript = New clsUtilDDScript
      Set gMessage = refInsMessage
      Set gUtilDate = refInsUtilDate
      Set gEnumReport = New clsEnumReport
      Set gDbUtil = New clsDBUtil
      Set gTexto = New clsTexto
      Set gAPI = New clsDefensiveAPI
      Set gUtilSQL = New clsUtilSQLStatement
      Set gProyUsuarioActual = refProyUsuarioActual
      Set gLibGalacDataParse = refLibGalacDataParse
      Set gEnumProyecto = New clsEnumAdministrativo
      Set gDefgen = valGDefgen
      Set gDefProg = valGDefProg
      Set gGlobalization = valGGlobalization
      Set gUtilFile = valUtilFile
      Set gLibControlesGalac = valLibControlesGalac
      Set gMonedaLocalActual = valMonedaLocalActual
      Set gUltimaTasaDeCambio = valUltimaTasaDeCambio
      Set gListLibrary = valListLibrary
      Set gWorkPaths = valUtilWorkPaths
      Set gDefDatabaseConexion = valDefDatabaseConexion
      Set gEnumTablaRetencion = valEnumTablaRetencion
      Set gAdmAlicuotaIvaActual = valAdmAlicuotaIvaActual
      Set gUtilMathOperations = refInsUtilMathOperations
End Sub

Private Sub sInstances()
   Dim insAnticipo As clsAnticipoRpt
   Dim insARCV As clsARCVRpt
   Dim insArticuloInv As clsArticuloInvRpt
   Dim insBanco As clsBancoRpt
   Dim insCliente As clsClienteRpt
   Dim insCotizacion As clsCotizacionRpt
   Dim insCxC As clsCxCRpt
   Dim insCuadreCaja As clsCuadreCajaRpt
End Sub

Public Sub sLlamarInformesSolicitudesDePago(ByVal valCompaniaActual As Object, ByVal valConexionAOS As Object)
   Dim insFrmInformesDeSolicitudesDePago As frmInformesDeSolicitudesDePago
   Set insFrmInformesDeSolicitudesDePago = New frmInformesDeSolicitudesDePago
   insFrmInformesDeSolicitudesDePago.sInitLookAndFeel
   insFrmInformesDeSolicitudesDePago.sLoadObjectValues valCompaniaActual, valConexionAOS
   insFrmInformesDeSolicitudesDePago.Show
End Sub

Public Sub sLlamarInformesMovimientoBancario(ByVal valCompaniaActual As Object, ByVal valConexionAOS As Object, ByVal valInsCuentaBancaria As Object, ByVal valGUltimaTasaDeCambio As Object, ByVal valGMonedaLocalActual As Object)
   Dim insFrmInformesDeMovimientoBancario As frmInformesDeMovimientoBancario
   Set insFrmInformesDeMovimientoBancario = New frmInformesDeMovimientoBancario
   insFrmInformesDeMovimientoBancario.sInitLookAndFeel
   insFrmInformesDeMovimientoBancario.sLoadObjectValues valCompaniaActual, valConexionAOS, valInsCuentaBancaria, valGUltimaTasaDeCambio, valGMonedaLocalActual
   insFrmInformesDeMovimientoBancario.Show
End Sub

Public Sub sLlamarInformesDeBanco(ByVal valCompaniaActual As Object, ByVal valConexionAOS As Object, ByVal valInsCuentaBancaria As Object, ByVal valGMonedaLocalActual As Object, ByVal valInsConceptoBancario As Object, ByVal valProyParametrosCompania As Object)
   Dim insFrmInformesDeBanco As frmInformesDeBancos
   Set insFrmInformesDeBanco = New frmInformesDeBancos
   insFrmInformesDeBanco.sInitLookAndFeel
   insFrmInformesDeBanco.sLoadObjectValues valCompaniaActual, valConexionAOS, valInsCuentaBancaria, valGMonedaLocalActual, valInsConceptoBancario, valProyParametrosCompania
   insFrmInformesDeBanco.Show
End Sub
   
Public Sub sLlamarfrmInformesDePago(ByVal valMostrarPantallaXML As Boolean, ByVal valCompaniaActual As Object, _
                                       ByVal valProyParametrosCompania As Object, _
                                       ByVal valInsTablaRetencion As Object, _
                                       ByVal valInsProveedor As Object, _
                                       ByVal valInsPagoSQL As Object, _
                                       ByVal valInsPagoRpt As Object, _
                                       ByVal valInsLibWincont As Object, _
                                       ByVal valInsComprobante As Object, _
                                       ByVal valInsRptPagoConfigurar As Object, _
                                       ByVal valInsSearchConnectionComunAOS As Object, _
                                       ByVal valInsConexionesSawAOS As Object, _
                                       ByVal valInsFactura As Object, _
                                       ByVal valInsRenglonFact As Object, _
                                       ByVal valInsPago As Object, _
                                       ByVal valInsCxP As Object, _
                                       ByVal valInsUtilXml As Object)
   Dim insInformesDePago As frmInformesDePago
   On Error GoTo h_ERROR
   Set insInformesDePago = New frmInformesDePago
   insInformesDePago.sLoadObjectValues valCompaniaActual, valProyParametrosCompania, valInsTablaRetencion, valInsProveedor, _
                                       valInsPagoSQL, valInsPagoRpt, valInsLibWincont, valInsComprobante, valInsRptPagoConfigurar, _
                                       valInsSearchConnectionComunAOS, valInsConexionesSawAOS, _
                                       valInsFactura, valInsRenglonFact, valInsPago, valInsCxP, valInsUtilXml
   insInformesDePago.sInitLookAndFeel valMostrarPantallaXML
   insInformesDePago.Show
   Set insInformesDePago = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsInitSawRpt", "sLlamarfrmInformesDePago", "clsInitSawRpt", 0, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLlamarfrmInformesDeCxP(ByVal valCompaniaActual As Object, _
                              ByVal valProveedor As Object, _
                              ByVal valRptInformesEspecialesConfigurar As Object, _
                              ByVal valAdmPropAnalisisVenc As Object, _
                              ByVal valConexionesSawAOS As Object, _
                              ByVal valCxP As Object, _
                              ByVal valAnticipo As Object, _
                              ByVal valFactura As Object, _
                              ByVal valNoComunSawIva As Object, _
                              ByVal valRptFactura As Object, _
                              ByVal valComunSawIvaSqls As Object)
   Dim insInformesDeCxP As frmInformesDeCXP
   On Error GoTo h_ERROR
   Set insInformesDeCxP = New frmInformesDeCXP
   insInformesDeCxP.sLoadObjectValues valCompaniaActual, valProveedor, valRptInformesEspecialesConfigurar, _
                                       valAdmPropAnalisisVenc, valConexionesSawAOS, _
                                       valCxP, valAnticipo, valFactura, valNoComunSawIva, _
                                       valRptFactura, valComunSawIvaSqls
   insInformesDeCxP.sInitLookAndFeel
   insInformesDeCxP.Show
   Set insInformesDeCxP = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsInitSawRpt", "sLlamarfrmInformesDeCxP", "clsInitSawRpt", 0, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLlamarInformesDeControlDespacho(ByVal valInsCompaniaActual As Object, _
                                  ByVal valInsControlDespacho As Object, _
                                  ByVal valInsCliente As Object)
   Dim insFrmInformesDeControlDespacho As frmInformesDeControlDeDespacho
   On Error GoTo h_ERROR
   Set insFrmInformesDeControlDespacho = New frmInformesDeControlDeDespacho
   insFrmInformesDeControlDespacho.sLoadObjectValues valInsCompaniaActual, valInsControlDespacho, valInsCliente
   insFrmInformesDeControlDespacho.sInitLookAndFeel
   insFrmInformesDeControlDespacho.Show
   Set insFrmInformesDeControlDespacho = Nothing
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsInitSawRpt", "sLlamarInformesDeControlDespacho", "clsInitSawRpt", 0, Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Sub

Public Sub sLlamarInformesDeImpuestosMunicipales(ByVal valInsCompaniaActual As Object, ByVal valInsDefDatabase As Object, _
                                                 ByVal valUtilWrp As Object)
   Dim insFrmInformesDeImpuestosMunicipales As frmInformesImpuestosMunicipales
   On Error GoTo h_ERROR
   Set insFrmInformesDeImpuestosMunicipales = New frmInformesImpuestosMunicipales
   insFrmInformesDeImpuestosMunicipales.sLoadObjectValues valInsCompaniaActual, valInsDefDatabase, valUtilWrp
   insFrmInformesDeImpuestosMunicipales.sInitLookAndFeel
   insFrmInformesDeImpuestosMunicipales.Show
   Set insFrmInformesDeImpuestosMunicipales = Nothing
h_EXIT:    On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsInitSawRpt", "sLlamarInformesDeImpuestosMunicipales", "clsInitSawRpt", 0, Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Sub

Public Sub sLlamarfrmInformesDeCxC(ByVal valCompaniaActual As Object, _
                              ByVal valVendedor As Object, _
                                 ByVal valRptInformesEspecialesConfigurar As Object, _
                                    ByVal valPropAnalisisVencNav As Object, _
                                    ByVal valCliente As Object, _
                                       ByVal valCamposDefFactura As Object, _
                                          ByVal valARExportExcel As Object, _
                                             ByVal valFacturaNav As Object)
                              
   Dim insInformesDeCxC As frmInformesDeCXC
   On Error GoTo h_ERROR
   Set insInformesDeCxC = New frmInformesDeCXC
   insInformesDeCxC.sLoadObjectValues valCompaniaActual, valVendedor, valCliente, _
                                          valPropAnalisisVencNav, valCamposDefFactura, _
                                             valARExportExcel, valFacturaNav, _
                                                valRptInformesEspecialesConfigurar
   insInformesDeCxC.sInitLookAndFeel
   insInformesDeCxC.Show
   Set insInformesDeCxC = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsInitSawRpt", "sLlamarfrmInformesDeCxC", "clsInitSawRpt", 0, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLlamarfrmInformesDeCobranza(ByVal valVendedorNavegator As Object, _
                                                   ByVal valClienteNavegator As Object, _
                                                      ByVal valCuentaBancariaNavegator As Object, _
                                                         ByVal valComprobanteNavegator As Object, _
                                                            ByVal valConexionSAWAos As Object, _
                                                               ByVal valCobranzasNavegator As Object, _
                                                                  ByVal valRenglonFacturaNavegator As Object, _
                                                                     ByVal valFacturaNavegator As Object, _
                                                                        ByVal valArtInventarioNavegator As Object, _
                                                                           ByVal valOtrosCargosFacturaNavegator As Object, _
                                                                              ByVal valRenglonOtrosCargosFactNavegator As Object, _
                                                                                 ByVal valZonaCobranzaNavegator As Object, _
                                                                                    ByVal valProyCompaniaActual As Object)

   Dim insfrmInformesDeCobranza As frmInformesDeCobranza
   On Error GoTo h_ERROR
   Set insfrmInformesDeCobranza = New frmInformesDeCobranza
   insfrmInformesDeCobranza.sLoadObjectValues valVendedorNavegator, valClienteNavegator, valCuentaBancariaNavegator, _
                           valComprobanteNavegator, valConexionSAWAos, valCobranzasNavegator, _
                           valRenglonFacturaNavegator, valFacturaNavegator, valArtInventarioNavegator, _
                           valOtrosCargosFacturaNavegator, valRenglonOtrosCargosFactNavegator, _
                           valZonaCobranzaNavegator, valProyCompaniaActual

   insfrmInformesDeCobranza.sInitLookAndFeel
   insfrmInformesDeCobranza.Show
   Set insfrmInformesDeCobranza = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsInitSawRpt", "sLlamarfrmInformesDeCobranza", "clsInitSawRpt", 0, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLlamarfrmInformesDeVendedor(ByVal valVendedorNavegator As Object, ByVal valProyCompaniaActual As Object)
   Dim insInformesDeVendedor As frmInformesDeVendedor
   On Error GoTo h_ERROR
   Set insInformesDeVendedor = New frmInformesDeVendedor
   insInformesDeVendedor.sLoadObjectValues valVendedorNavegator, valProyCompaniaActual
   insInformesDeVendedor.Show
   Set insInformesDeVendedor = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsInitSawRpt", "sLlamarfrmInformesDeVendedor", "clsInitSawRpt", 0, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLlamarfrmInformesDeAnticipo(ByVal valVendedorNavigator As Object, ByVal valClienteNavigator As Object, ByVal valProveedorNavigator As Object, ByVal valProyCompaniaActual As Object, ByVal valFacturaNavigator As Object, ByVal valConexionesSawAOS As Object, ByVal valTipoDeAnticipo As enum_TipoDeAnticipo)
   Dim insfrmInformeDeAnticipo  As frmInformeDeAnticipo
   On Error GoTo h_ERROR
   Set insfrmInformeDeAnticipo = New frmInformeDeAnticipo
   insfrmInformeDeAnticipo.sLoadObjectValues valVendedorNavigator, valClienteNavigator, valProveedorNavigator, valProyCompaniaActual, valFacturaNavigator, valConexionesSawAOS, valTipoDeAnticipo
   insfrmInformeDeAnticipo.Show
   Set insfrmInformeDeAnticipo = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, "clsInitSawRpt", "sLlamarfrmInformesDeAnticipo", "clsInitSawRpt", 0, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

