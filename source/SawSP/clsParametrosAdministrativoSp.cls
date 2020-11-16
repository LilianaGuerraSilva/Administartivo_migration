VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsParametrosAdministrativoSP"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsParametrosAdministrativoSp"
End Function
Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "clsParametrosAdministrativoSp"
End Function
Public Function fCreateDropParametrosAdministrativosSP(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, Optional ByVal valWithEncryption As Boolean = True) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If valCreate Then
      vResult = vResult And fCreateSpUPD(valShowMessage, valWithEncryption)
'      vResult = vResult And fCreateSpActivarS10(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateSpDesactivarS10(valShowMessage, valWithEncryption)
      vResult = vResult And fCreateFnSpaceBeforeCap(valShowMessage, valWithEncryption)
   Else
      vResult = vResult And fDropSpDesactivarS10(valShowMessage)
      vResult = vResult And fDropSpActivarS10(valShowMessage)
      vResult = vResult And fDropSpUPD(valShowMessage)
      vResult = vResult And fDropFnSpaceBeforeCap(valShowMessage)
   End If
   fCreateDropParametrosAdministrativosSP = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropParametrosAdministrativosSP", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpUPD(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameUPD(), fSpParametersUPD(), fStatementsSpUPD(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropSpUPD(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameUPD(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameUPD() As String
   GetSpNameUPD = "ParametrosAdministrativoUPD"
End Function
Private Function fSpParametersUPD() As String
   Dim vResult As String
   vResult = " @Secuencial " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   vResult = vResult & " @FormaDeEscogerCompania " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @EscogerCompaniaAlEntrar " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @EscogerUltimaCompaniaUsada " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @usarZonaCobranza " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @UsaCodigoClienteEnPantalla " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @UsaCodigoVendedorEnPantalla " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @UsarCodigoProveedorEnPantalla " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @UsaCodigoBancoEnPantalla " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @VerificarFacturasManualesFaltantes " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @UsaCodigoConceptoBancarioEnPantalla " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @GenerarMovBancarioDesdePago " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @NombreMonedaLocal " & gUtilSQL.getVarCharTypeForDB(80) & "= NULL, "
   vResult = vResult & " @NombreMonedaExtranjera " & gUtilSQL.getVarCharTypeForDB(80) & " = NULL, "
   vResult = vResult & " @GenerarMovBancarioDesdeCobro " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @MandarMensajeNumeroDeMovimientoBancario " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @SugerirConsecutivoEnCobranza " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @NumFacturasManualesFaltantes " & gUtilSQL.getNumericTypeForDB(9, 0) & " = NULL, "
   vResult = vResult & " @LongitudCodigoVendedor " & gUtilSQL.getNumericTypeForDB(9, 0) & " = NULL, "
   vResult = vResult & " @LongitudCodigoCliente " & gUtilSQL.getNumericTypeForDB(9, 0) & " = NULL, "
   vResult = vResult & " @PermitirEditarIVAenCxC_CxP " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @LongitudCodigoProveedor " & gUtilSQL.getNumericTypeForDB(9, 0) & " = NULL, "
   vResult = vResult & " @ConfirmarImpresionPorSecciones " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @GenerarCxPDesdeCompra " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @PermitirSobregiro " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @SeResincronizaronLosSupervisores " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @DevolucionReversoSeGeneraComo " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @AvisoDeClienteConDeuda " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @MontoAPartirDelCualEnviarAvisoDeuda " & gUtilSQL.getNumericTypeForDB(12, 2) & " = NULL, "
   vResult = vResult & " @OrdenamientoDeCodigoString " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @ImprimirComprobanteDeCxC " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @ImprimirComprobanteDeCxP " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @CodigoMonedaLocal " & gUtilSQL.getVarCharTypeForDB(4) & " = NULL, "
   vResult = vResult & " @CodigoMonedaExtranjera " & gUtilSQL.getVarCharTypeForDB(4) & " = NULL, "
   vResult = vResult & " @AvisarSiProveedorTieneAnticipos " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @PuedeUsarS10 " & gUtilSQL.getCharTypeForDB(1) & " = NULL, "
   vResult = vResult & " @S10DBName " & gUtilSQL.getVarCharTypeForDB(128) & " = NULL, "
   vResult = vResult & " @NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & " = NULL "
   fSpParametersUPD = vResult
End Function
Private Function fStatementsSpUPD() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN "
   vResult = vResult & "   DECLARE @Ori_FormaDeEscogerCompania " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_EscogerCompaniaAlEntrar " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_EscogerUltimaCompaniaUsada " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_usarZonaCobranza " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaCodigoClienteEnPantalla " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaCodigoVendedorEnPantalla " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsarCodigoProveedorEnPantalla " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaCodigoBancoEnPantalla " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_VerificarFacturasManualesFaltantes " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_UsaCodigoConceptoBancarioEnPantalla " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_GenerarMovBancarioDesdePago " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NombreMonedaLocal " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NombreMonedaExtranjera " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_GenerarMovBancarioDesdeCobro " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MandarMensajeNumeroDeMovimientoBancario " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_SugerirConsecutivoEnCobranza " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NumFacturasManualesFaltantes " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_LongitudCodigoVendedor " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_LongitudCodigoCliente " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_PermitirEditarIVAenCxC_CxP " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_LongitudCodigoProveedor " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_ConfirmarImpresionPorSecciones " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_GenerarCxPDesdeCompra " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_PermitirSobregiro " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_SeResincronizaronLosSupervisores " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_DevolucionReversoSeGeneraComo " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_AvisoDeClienteConDeuda " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_MontoAPartirDelCualEnviarAvisoDeuda " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_OrdenamientoDeCodigoString " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_ImprimirComprobanteDeCxC " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_ImprimirComprobanteDeCxP " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoMonedaLocal " & gUtilSQL.getVarCharTypeForDB(4) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_CodigoMonedaExtranjera " & gUtilSQL.getVarCharTypeForDB(4) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_AvisarSiProveedorTieneAnticipos " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_PuedeUsarS10 " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_S10DBName " & gUtilSQL.getVarCharTypeForDB(128) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Ori_TimeStamp timestamp " & vbCrLf
   vResult = vResult & "   DECLARE @Up_FormaDeEscogerCompania " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_EscogerCompaniaAlEntrar " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_EscogerUltimaCompaniaUsada " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_usarZonaCobranza " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaCodigoClienteEnPantalla " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaCodigoVendedorEnPantalla " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsarCodigoProveedorEnPantalla " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaCodigoBancoEnPantalla " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_VerificarFacturasManualesFaltantes " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_UsaCodigoConceptoBancarioEnPantalla " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_GenerarMovBancarioDesdePago " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NombreMonedaLocal " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NombreMonedaExtranjera " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   DECLARE @Up_GenerarMovBancarioDesdeCobro " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_MandarMensajeNumeroDeMovimientoBancario " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_SugerirConsecutivoEnCobranza " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NumFacturasManualesFaltantes " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Up_LongitudCodigoVendedor " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Up_LongitudCodigoCliente " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Up_PermitirEditarIVAenCxC_CxP " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_LongitudCodigoProveedor " & gUtilSQL.getNumericTypeForDB(9, 0) & vbCrLf
   vResult = vResult & "   DECLARE @Up_ConfirmarImpresionPorSecciones " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_GenerarCxPDesdeCompra " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_PermitirSobregiro " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_SeResincronizaronLosSupervisores " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_DevolucionReversoSeGeneraComo " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_AvisoDeClienteConDeuda " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_MontoAPartirDelCualEnviarAvisoDeuda " & gUtilSQL.getNumericTypeForDB(12, 2) & vbCrLf
   vResult = vResult & "   DECLARE @Up_OrdenamientoDeCodigoString " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_ImprimirComprobanteDeCxC " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_ImprimirComprobanteDeCxP " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoMonedaLocal " & gUtilSQL.getVarCharTypeForDB(4) & vbCrLf
   vResult = vResult & "   DECLARE @Up_CodigoMonedaExtranjera " & gUtilSQL.getVarCharTypeForDB(4) & vbCrLf
   vResult = vResult & "   DECLARE @Up_AvisarSiProveedorTieneAnticipos " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_PuedeUsarS10 " & gUtilSQL.getCharTypeForDB(1) & vbCrLf
   vResult = vResult & "   DECLARE @Up_S10DBName " & gUtilSQL.getVarCharTypeForDB(128) & vbCrLf
   vResult = vResult & "   DECLARE @Up_NombreOperador " & gUtilSQL.getVarCharTypeForDB(10) & vbCrLf
   vResult = vResult & "   BEGIN TRAN " & vbCrLf
   vResult = vResult & "   SELECT @Ori_FormaDeEscogerCompania = FormaDeEscogerCompania, " & vbCrLf
   vResult = vResult & "         @Ori_EscogerCompaniaAlEntrar = EscogerCompaniaAlEntrar, " & vbCrLf
   vResult = vResult & "         @Ori_EscogerUltimaCompaniaUsada = EscogerUltimaCompaniaUsada, " & vbCrLf
   vResult = vResult & "         @Ori_usarZonaCobranza = usarZonaCobranza, " & vbCrLf
   vResult = vResult & "         @Ori_UsaCodigoClienteEnPantalla = UsaCodigoClienteEnPantalla, " & vbCrLf
   vResult = vResult & "         @Ori_UsaCodigoVendedorEnPantalla = UsaCodigoVendedorEnPantalla, " & vbCrLf
   vResult = vResult & "         @Ori_UsarCodigoProveedorEnPantalla = UsarCodigoProveedorEnPantalla, " & vbCrLf
   vResult = vResult & "         @Ori_UsaCodigoBancoEnPantalla = UsaCodigoBancoEnPantalla, " & vbCrLf
   vResult = vResult & "         @Ori_VerificarFacturasManualesFaltantes = VerificarFacturasManualesFaltantes, " & vbCrLf
   vResult = vResult & "         @Ori_UsaCodigoConceptoBancarioEnPantalla = UsaCodigoConceptoBancarioEnPantalla, " & vbCrLf
   vResult = vResult & "         @Ori_GenerarMovBancarioDesdePago = GenerarMovBancarioDesdePago, " & vbCrLf
   vResult = vResult & "         @Ori_NombreMonedaLocal = NombreMonedaLocal, " & vbCrLf
   vResult = vResult & "         @Ori_NombreMonedaExtranjera = NombreMonedaExtranjera, " & vbCrLf
   vResult = vResult & "         @Ori_GenerarMovBancarioDesdeCobro = GenerarMovBancarioDesdeCobro, " & vbCrLf
   vResult = vResult & "         @Ori_MandarMensajeNumeroDeMovimientoBancario = MandarMensajeNumeroDeMovimientoBancario, " & vbCrLf
   vResult = vResult & "         @Ori_SugerirConsecutivoEnCobranza = SugerirConsecutivoEnCobranza, " & vbCrLf
   vResult = vResult & "         @Ori_NumFacturasManualesFaltantes = NumFacturasManualesFaltantes, " & vbCrLf
   vResult = vResult & "         @Ori_LongitudCodigoVendedor = LongitudCodigoVendedor, " & vbCrLf
   vResult = vResult & "         @Ori_LongitudCodigoCliente = LongitudCodigoCliente, " & vbCrLf
   vResult = vResult & "         @Ori_PermitirEditarIVAenCxC_CxP = PermitirEditarIVAenCxC_CxP, " & vbCrLf
   vResult = vResult & "         @Ori_LongitudCodigoProveedor = LongitudCodigoProveedor, " & vbCrLf
   vResult = vResult & "         @Ori_ConfirmarImpresionPorSecciones = ConfirmarImpresionPorSecciones, " & vbCrLf
   vResult = vResult & "         @Ori_GenerarCxPDesdeCompra = GenerarCxPDesdeCompra, " & vbCrLf
   vResult = vResult & "         @Ori_PermitirSobregiro = PermitirSobregiro, " & vbCrLf
   vResult = vResult & "         @Ori_SeResincronizaronLosSupervisores = SeResincronizaronLosSupervisores, " & vbCrLf
   vResult = vResult & "         @Ori_DevolucionReversoSeGeneraComo = DevolucionReversoSeGeneraComo, " & vbCrLf
   vResult = vResult & "         @Ori_AvisoDeClienteConDeuda = AvisoDeClienteConDeuda, " & vbCrLf
   vResult = vResult & "         @Ori_MontoAPartirDelCualEnviarAvisoDeuda = MontoAPartirDelCualEnviarAvisoDeuda, " & vbCrLf
   vResult = vResult & "         @Ori_OrdenamientoDeCodigoString = OrdenamientoDeCodigoString, " & vbCrLf
   vResult = vResult & "         @Ori_ImprimirComprobanteDeCxC = ImprimirComprobanteDeCxC, " & vbCrLf
   vResult = vResult & "         @Ori_ImprimirComprobanteDeCxP = ImprimirComprobanteDeCxP, " & vbCrLf
   vResult = vResult & "         @Ori_CodigoMonedaLocal = CodigoMonedaLocal, " & vbCrLf
   vResult = vResult & "         @Ori_CodigoMonedaExtranjera = CodigoMonedaExtranjera, " & vbCrLf
   vResult = vResult & "         @Ori_AvisarSiProveedorTieneAnticipos = AvisarSiProveedorTieneAnticipos, " & vbCrLf
   vResult = vResult & "         @Ori_PuedeUsarS10 = PuedeUsarS10, " & vbCrLf
   vResult = vResult & "         @Ori_S10DBName = S10DBName, " & vbCrLf
   vResult = vResult & "         @Ori_NombreOperador = NombreOperador, " & vbCrLf
   vResult = vResult & "         @Ori_TimeStamp = fldTimeStamp " & vbCrLf
   vResult = vResult & "      FROM ParametrosAdministrativo " & vbCrLf
   vResult = vResult & "      WHERE Secuencial = @Secuencial " & vbCrLf
   vResult = vResult & "   IF @FormaDeEscogerCompania is null " & vbCrLf
   vResult = vResult & "      SET @Up_FormaDeEscogerCompania = @Ori_FormaDeEscogerCompania " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_FormaDeEscogerCompania = @FormaDeEscogerCompania " & vbCrLf
   vResult = vResult & "   IF @EscogerCompaniaAlEntrar is null " & vbCrLf
   vResult = vResult & "      SET @Up_EscogerCompaniaAlEntrar = @Ori_EscogerCompaniaAlEntrar " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_EscogerCompaniaAlEntrar = @EscogerCompaniaAlEntrar " & vbCrLf
   vResult = vResult & "   IF @EscogerUltimaCompaniaUsada is null " & vbCrLf
   vResult = vResult & "      SET @Up_EscogerUltimaCompaniaUsada = @Ori_EscogerUltimaCompaniaUsada " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_EscogerUltimaCompaniaUsada = @EscogerUltimaCompaniaUsada " & vbCrLf
   vResult = vResult & "   IF @usarZonaCobranza is null " & vbCrLf
   vResult = vResult & "      SET @Up_usarZonaCobranza = @Ori_usarZonaCobranza " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_usarZonaCobranza = @usarZonaCobranza " & vbCrLf
   vResult = vResult & "   IF @UsaCodigoClienteEnPantalla is null " & vbCrLf
   vResult = vResult & "      SET @Up_UsaCodigoClienteEnPantalla = @Ori_UsaCodigoClienteEnPantalla " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_UsaCodigoClienteEnPantalla = @UsaCodigoClienteEnPantalla " & vbCrLf
   vResult = vResult & "   IF @UsaCodigoVendedorEnPantalla is null " & vbCrLf
   vResult = vResult & "      SET @Up_UsaCodigoVendedorEnPantalla = @Ori_UsaCodigoVendedorEnPantalla " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_UsaCodigoVendedorEnPantalla = @UsaCodigoVendedorEnPantalla " & vbCrLf
   vResult = vResult & "   IF @UsarCodigoProveedorEnPantalla is null " & vbCrLf
   vResult = vResult & "      SET @Up_UsarCodigoProveedorEnPantalla = @Ori_UsarCodigoProveedorEnPantalla " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_UsarCodigoProveedorEnPantalla = @UsarCodigoProveedorEnPantalla " & vbCrLf
   vResult = vResult & "   IF @UsaCodigoBancoEnPantalla is null " & vbCrLf
   vResult = vResult & "      SET @Up_UsaCodigoBancoEnPantalla = @Ori_UsaCodigoBancoEnPantalla " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_UsaCodigoBancoEnPantalla = @UsaCodigoBancoEnPantalla " & vbCrLf
   vResult = vResult & "   IF @VerificarFacturasManualesFaltantes is null " & vbCrLf
   vResult = vResult & "      SET @Up_VerificarFacturasManualesFaltantes = @Ori_VerificarFacturasManualesFaltantes " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_VerificarFacturasManualesFaltantes = @VerificarFacturasManualesFaltantes " & vbCrLf
   vResult = vResult & "   IF @UsaCodigoConceptoBancarioEnPantalla is null " & vbCrLf
   vResult = vResult & "      SET @Up_UsaCodigoConceptoBancarioEnPantalla = @Ori_UsaCodigoConceptoBancarioEnPantalla " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_UsaCodigoConceptoBancarioEnPantalla = @UsaCodigoConceptoBancarioEnPantalla " & vbCrLf
   vResult = vResult & "   IF @GenerarMovBancarioDesdePago is null " & vbCrLf
   vResult = vResult & "      SET @Up_GenerarMovBancarioDesdePago = @Ori_GenerarMovBancarioDesdePago " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_GenerarMovBancarioDesdePago = @GenerarMovBancarioDesdePago " & vbCrLf
   vResult = vResult & "   IF @NombreMonedaLocal is null " & vbCrLf
   vResult = vResult & "      SET @Up_NombreMonedaLocal = @Ori_NombreMonedaLocal " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_NombreMonedaLocal = @NombreMonedaLocal " & vbCrLf
   vResult = vResult & "   IF @NombreMonedaExtranjera is null " & vbCrLf
   vResult = vResult & "      SET @Up_NombreMonedaExtranjera = @Ori_NombreMonedaExtranjera " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_NombreMonedaExtranjera = @NombreMonedaExtranjera " & vbCrLf
   vResult = vResult & "   IF @AvisarSiProveedorTieneAnticipos is null " & vbCrLf
   vResult = vResult & "      SET @Up_AvisarSiProveedorTieneAnticipos = @AvisarSiProveedorTieneAnticipos " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_AvisarSiProveedorTieneAnticipos = @AvisarSiProveedorTieneAnticipos " & vbCrLf
   vResult = vResult & "   IF @GenerarMovBancarioDesdeCobro is null " & vbCrLf
   vResult = vResult & "      SET @Up_GenerarMovBancarioDesdeCobro = @Ori_GenerarMovBancarioDesdeCobro " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_GenerarMovBancarioDesdeCobro = @GenerarMovBancarioDesdeCobro " & vbCrLf
   vResult = vResult & "   IF @MandarMensajeNumeroDeMovimientoBancario is null " & vbCrLf
   vResult = vResult & "      SET @Up_MandarMensajeNumeroDeMovimientoBancario = @Ori_MandarMensajeNumeroDeMovimientoBancario " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_MandarMensajeNumeroDeMovimientoBancario = @MandarMensajeNumeroDeMovimientoBancario " & vbCrLf
   vResult = vResult & "   IF @SugerirConsecutivoEnCobranza is null " & vbCrLf
   vResult = vResult & "      SET @Up_SugerirConsecutivoEnCobranza = @Ori_SugerirConsecutivoEnCobranza " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_SugerirConsecutivoEnCobranza = @SugerirConsecutivoEnCobranza " & vbCrLf
   vResult = vResult & "   IF @NumFacturasManualesFaltantes is null " & vbCrLf
   vResult = vResult & "      SET @Up_NumFacturasManualesFaltantes = @Ori_NumFacturasManualesFaltantes " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_NumFacturasManualesFaltantes = @NumFacturasManualesFaltantes " & vbCrLf
   vResult = vResult & "   IF @LongitudCodigoVendedor is null " & vbCrLf
   vResult = vResult & "      SET @Up_LongitudCodigoVendedor = @Ori_LongitudCodigoVendedor " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_LongitudCodigoVendedor = @LongitudCodigoVendedor " & vbCrLf
   vResult = vResult & "   IF @LongitudCodigoCliente is null " & vbCrLf
   vResult = vResult & "      SET @Up_LongitudCodigoCliente = @Ori_LongitudCodigoCliente " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_LongitudCodigoCliente = @LongitudCodigoCliente " & vbCrLf
   vResult = vResult & "   IF @PermitirEditarIVAenCxC_CxP is null " & vbCrLf
   vResult = vResult & "      SET @Up_PermitirEditarIVAenCxC_CxP = @Ori_PermitirEditarIVAenCxC_CxP " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_PermitirEditarIVAenCxC_CxP = @PermitirEditarIVAenCxC_CxP " & vbCrLf
   vResult = vResult & "   IF @LongitudCodigoProveedor is null " & vbCrLf
   vResult = vResult & "      SET @Up_LongitudCodigoProveedor = @Ori_LongitudCodigoProveedor " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_LongitudCodigoProveedor = @LongitudCodigoProveedor " & vbCrLf
   vResult = vResult & "   IF @ConfirmarImpresionPorSecciones is null " & vbCrLf
   vResult = vResult & "      SET @Up_ConfirmarImpresionPorSecciones = @Ori_ConfirmarImpresionPorSecciones " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_ConfirmarImpresionPorSecciones = @ConfirmarImpresionPorSecciones " & vbCrLf
   vResult = vResult & "   IF @GenerarCxPDesdeCompra is null " & vbCrLf
   vResult = vResult & "      SET @Up_GenerarCxPDesdeCompra = @Ori_GenerarCxPDesdeCompra " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_GenerarCxPDesdeCompra = @GenerarCxPDesdeCompra " & vbCrLf
   vResult = vResult & "   IF @PermitirSobregiro is null " & vbCrLf
   vResult = vResult & "      SET @Up_PermitirSobregiro = @Ori_PermitirSobregiro " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_PermitirSobregiro = @PermitirSobregiro " & vbCrLf
   vResult = vResult & "   IF @SeResincronizaronLosSupervisores is null " & vbCrLf
   vResult = vResult & "      SET @Up_SeResincronizaronLosSupervisores = @Ori_SeResincronizaronLosSupervisores " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_SeResincronizaronLosSupervisores = @SeResincronizaronLosSupervisores " & vbCrLf
   vResult = vResult & "   IF @DevolucionReversoSeGeneraComo is null " & vbCrLf
   vResult = vResult & "      SET @Up_DevolucionReversoSeGeneraComo = @Ori_DevolucionReversoSeGeneraComo " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_DevolucionReversoSeGeneraComo = @DevolucionReversoSeGeneraComo " & vbCrLf
   vResult = vResult & "   IF @AvisoDeClienteConDeuda is null " & vbCrLf
   vResult = vResult & "      SET @Up_AvisoDeClienteConDeuda = @Ori_AvisoDeClienteConDeuda " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_AvisoDeClienteConDeuda = @AvisoDeClienteConDeuda " & vbCrLf
   vResult = vResult & "   IF @MontoAPartirDelCualEnviarAvisoDeuda is null " & vbCrLf
   vResult = vResult & "      SET @Up_MontoAPartirDelCualEnviarAvisoDeuda = @Ori_MontoAPartirDelCualEnviarAvisoDeuda " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_MontoAPartirDelCualEnviarAvisoDeuda = @MontoAPartirDelCualEnviarAvisoDeuda " & vbCrLf
   vResult = vResult & "   IF @OrdenamientoDeCodigoString is null " & vbCrLf
   vResult = vResult & "      SET @Up_OrdenamientoDeCodigoString = @Ori_OrdenamientoDeCodigoString " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_OrdenamientoDeCodigoString = @OrdenamientoDeCodigoString " & vbCrLf
   vResult = vResult & "   IF @ImprimirComprobanteDeCxC is null " & vbCrLf
   vResult = vResult & "      SET @Up_ImprimirComprobanteDeCxC = @Ori_ImprimirComprobanteDeCxC " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_ImprimirComprobanteDeCxC = @ImprimirComprobanteDeCxC " & vbCrLf
   vResult = vResult & "   IF @ImprimirComprobanteDeCxP is null " & vbCrLf
   vResult = vResult & "      SET @Up_ImprimirComprobanteDeCxP = @Ori_ImprimirComprobanteDeCxP " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_ImprimirComprobanteDeCxP = @ImprimirComprobanteDeCxP " & vbCrLf
   vResult = vResult & "   IF @CodigoMonedaLocal is null " & vbCrLf
   vResult = vResult & "      SET @Up_CodigoMonedaLocal = @Ori_CodigoMonedaLocal " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_CodigoMonedaLocal = @CodigoMonedaLocal " & vbCrLf
   vResult = vResult & "   IF @CodigoMonedaExtranjera is null " & vbCrLf
   vResult = vResult & "      SET @Up_CodigoMonedaExtranjera = @Ori_CodigoMonedaExtranjera " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_CodigoMonedaExtranjera = @CodigoMonedaExtranjera " & vbCrLf
   vResult = vResult & "   IF @AvisarSiProveedorTieneAnticipos is null " & vbCrLf
   vResult = vResult & "      SET @Up_AvisarSiProveedorTieneAnticipos = @Ori_AvisarSiProveedorTieneAnticipos " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_AvisarSiProveedorTieneAnticipos = @AvisarSiProveedorTieneAnticipos " & vbCrLf
   vResult = vResult & "   IF @PuedeUsarS10 is null " & vbCrLf
   vResult = vResult & "      SET @Up_PuedeUsarS10 = @Ori_PuedeUsarS10 " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_PuedeUsarS10 = @PuedeUsarS10 " & vbCrLf
   vResult = vResult & "   IF @S10DBName is null " & vbCrLf
   vResult = vResult & "      SET @Up_S10DBName = @Ori_S10DBName " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_S10DBName = @S10DBName " & vbCrLf
   vResult = vResult & "   IF @NombreOperador is null " & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @Ori_NombreOperador " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      SET @Up_NombreOperador = @NombreOperador " & vbCrLf
   vResult = vResult & "   UPDATE ParametrosAdministrativo " & vbCrLf
   vResult = vResult & "         SET FormaDeEscogerCompania = @Up_FormaDeEscogerCompania, " & vbCrLf
   vResult = vResult & "         EscogerCompaniaAlEntrar = @Up_EscogerCompaniaAlEntrar, " & vbCrLf
   vResult = vResult & "         EscogerUltimaCompaniaUsada = @Up_EscogerUltimaCompaniaUsada, " & vbCrLf
   vResult = vResult & "         usarZonaCobranza = @Up_usarZonaCobranza, " & vbCrLf
   vResult = vResult & "         UsaCodigoClienteEnPantalla = @Up_UsaCodigoClienteEnPantalla, " & vbCrLf
   vResult = vResult & "         UsaCodigoVendedorEnPantalla = @Up_UsaCodigoVendedorEnPantalla, " & vbCrLf
   vResult = vResult & "         UsarCodigoProveedorEnPantalla = @Up_UsarCodigoProveedorEnPantalla, " & vbCrLf
   vResult = vResult & "         UsaCodigoBancoEnPantalla = @Up_UsaCodigoBancoEnPantalla, " & vbCrLf
   vResult = vResult & "         VerificarFacturasManualesFaltantes = @Up_VerificarFacturasManualesFaltantes, " & vbCrLf
   vResult = vResult & "         UsaCodigoConceptoBancarioEnPantalla = @Up_UsaCodigoConceptoBancarioEnPantalla, " & vbCrLf
   vResult = vResult & "         GenerarMovBancarioDesdePago = @Up_GenerarMovBancarioDesdePago, " & vbCrLf
   vResult = vResult & "         NombreMonedaLocal = @Up_NombreMonedaLocal, " & vbCrLf
   vResult = vResult & "         NombreMonedaExtranjera = @Up_NombreMonedaExtranjera, " & vbCrLf
   vResult = vResult & "         GenerarMovBancarioDesdeCobro = @Up_GenerarMovBancarioDesdeCobro, " & vbCrLf
   vResult = vResult & "         MandarMensajeNumeroDeMovimientoBancario = @Up_MandarMensajeNumeroDeMovimientoBancario, " & vbCrLf
   vResult = vResult & "         SugerirConsecutivoEnCobranza = @Up_SugerirConsecutivoEnCobranza, " & vbCrLf
   vResult = vResult & "         NumFacturasManualesFaltantes = @Up_NumFacturasManualesFaltantes, " & vbCrLf
   vResult = vResult & "         LongitudCodigoVendedor = @Up_LongitudCodigoVendedor, " & vbCrLf
   vResult = vResult & "         LongitudCodigoCliente = @Up_LongitudCodigoCliente, " & vbCrLf
   vResult = vResult & "         PermitirEditarIVAenCxC_CxP = @Up_PermitirEditarIVAenCxC_CxP, " & vbCrLf
   vResult = vResult & "         LongitudCodigoProveedor = @Up_LongitudCodigoProveedor, " & vbCrLf
   vResult = vResult & "         ConfirmarImpresionPorSecciones = @Up_ConfirmarImpresionPorSecciones, " & vbCrLf
   vResult = vResult & "         GenerarCxPDesdeCompra = @Up_GenerarCxPDesdeCompra, " & vbCrLf
   vResult = vResult & "         PermitirSobregiro = @Up_PermitirSobregiro, " & vbCrLf
   vResult = vResult & "         SeResincronizaronLosSupervisores = @Up_SeResincronizaronLosSupervisores, " & vbCrLf
   vResult = vResult & "         DevolucionReversoSeGeneraComo = @Up_DevolucionReversoSeGeneraComo, " & vbCrLf
   vResult = vResult & "         AvisoDeClienteConDeuda = @Up_AvisoDeClienteConDeuda, " & vbCrLf
   vResult = vResult & "         MontoAPartirDelCualEnviarAvisoDeuda = @Up_MontoAPartirDelCualEnviarAvisoDeuda, " & vbCrLf
   vResult = vResult & "         OrdenamientoDeCodigoString = @Up_OrdenamientoDeCodigoString, " & vbCrLf
   vResult = vResult & "         ImprimirComprobanteDeCxC = @Up_ImprimirComprobanteDeCxC, " & vbCrLf
   vResult = vResult & "         ImprimirComprobanteDeCxP = @Up_ImprimirComprobanteDeCxP, " & vbCrLf
   vResult = vResult & "         CodigoMonedaLocal = @Up_CodigoMonedaLocal, " & vbCrLf
   vResult = vResult & "         CodigoMonedaExtranjera = @Up_CodigoMonedaExtranjera, " & vbCrLf
   vResult = vResult & "         AvisarSiProveedorTieneAnticipos = @Up_AvisarSiProveedorTieneAnticipos, " & vbCrLf
   vResult = vResult & "         PuedeUsarS10 = @Up_PuedeUsarS10, " & vbCrLf
   vResult = vResult & "         S10DBName = @Up_S10DBName, " & vbCrLf
   vResult = vResult & "         NombreOperador = @Up_NombreOperador, " & vbCrLf
   vResult = vResult & "         FechaUltimaModificacion = CONVERT(" & gUtilSQL.getDateTypeForDB() & ", GETDATE(), 103) " & vbCrLf
   vResult = vResult & "      WHERE Secuencial = @Secuencial " & vbCrLf
   vResult = vResult & "         AND fldTimeStamp = @Ori_TimeStamp " & vbCrLf
   vResult = vResult & "   IF @@ERROR = 0 " & vbCrLf
   vResult = vResult & "      COMMIT TRAN " & vbCrLf
   vResult = vResult & "   ELSE " & vbCrLf
   vResult = vResult & "      ROLLBACK " & vbCrLf
   vResult = vResult & "END "
   fStatementsSpUPD = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSPUPD", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpActivarS10(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameActivarS10(), fSpParametersActivarS10(), fStatementsSPActivarS10(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpActivarS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpActivarS10 ", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropSpActivarS10(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameActivarS10(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpActivarS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpActivarS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameActivarS10() As String
   GetSpNameActivarS10 = "S10_ActivarS10"
End Function
Private Function fSpParametersActivarS10() As String
   Dim vResult As String
   vResult = " @DBNameOfS10 " & gUtilSQL.getVarCharTypeForDB(128) & vbCrLf
   fSpParametersActivarS10 = vResult
End Function
Private Function fStatementsSPActivarS10() As String
   Dim vResult As String
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Sec " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & "   DECLARE @UsuarioS10 " & gUtilSQL.getVarCharTypeForDB(10) & "" & vbCrLf
   vResult = vResult & "   DECLARE @CodigoRet " & gUtilSQL.getVarCharTypeForDB(6) & "" & vbCrLf
   vResult = vResult & "   DECLARE @Fecha " & gUtilSQL.getDateTypeForDB() & "" & vbCrLf
   vResult = vResult & "   SELECT TOP 1 @Sec = Secuencial FROM parametrosAdministrativo" & vbCrLf
   vResult = vResult & "   IF @Sec > 0" & vbCrLf
   vResult = vResult & "   BEGIN" & vbCrLf
   vResult = vResult & "      SET @UsuarioS10 = dbo.Gf_UsuarioS10()" & vbCrLf
   vResult = vResult & "      SET @Fecha = CONVERT(" & gUtilSQL.getDateTypeForDB() & ", GETDATE())" & vbCrLf
   vResult = vResult & "      IF NOT EXISTS(SELECT NombreDelUsuario FROM USUARIO WHERE NombreDelUsuario = @UsuarioS10)" & vbCrLf
   vResult = vResult & "         INSERT INTO USUARIO(" & vbCrLf
   vResult = vResult & "               NombreDelUsuario, Status, NombreUltimaCompaniaUsada, FldPassword," & vbCrLf
   vResult = vResult & "               NombreYApellido, Cargo, EsSupervisor, email," & vbCrLf
   vResult = vResult & "               UsaContabilidad, OpcionesCompania, Nivel1, Nivel2, Nivel3, Nivel4," & vbCrLf
   vResult = vResult & "               OpcionesFactura, OpcionesResumenDiarioVentas, OpcionesCajaRegistradora," & vbCrLf
   vResult = vResult & "               OpcionesCotizacion, OpcionesCliente, OpcionesAutorizacion, OpcionesLlamada," & vbCrLf
   vResult = vResult & "               OpcionesVisita, OpcionesCxC, OpcionesCobranza, OpcionesArtInventario, OpcionesAlmacen," & vbCrLf
   vResult = vResult & "               OpcionesNotaEntSalida, OpcionesExistXAlmacen, OpcionesTransferencia, OpcionesVendedor," & vbCrLf
   vResult = vResult & "               OpcionesTablas, OpcionesParametros, OpcionesUsuario, OpcionesDespacho," & vbCrLf
   vResult = vResult & "               OpcionesOrdenDeServicio, OpcionesCurso, OpcionesCxP, OpcionesPago," & vbCrLf
   vResult = vResult & "               OpcionesProveedor, OpcionesTablaRetencion, OpcionesContrato," & vbCrLf
   vResult = vResult & "               OpcionesCuentaBancaria, OpcionesMovimientoBancario, OpcionesConceptoBancario," & vbCrLf
   vResult = vResult & "               OpcionesAlicuotaIVA, OpcionesCompra, OpcionesConteoFisico," & vbCrLf
   vResult = vResult & "               OpcionesPeriodo, OpcionesCuenta, OpcionesComprobante, OpcionesCatalogoGeneral," & vbCrLf
   vResult = vResult & "               OpcionesActivoFijo, OpcionesConciliacion, OpcionesReglasDeContabilizacion," & vbCrLf
   vResult = vResult & "               OpcionesPlantillaRet, OpcionesARCV, OpcionesRA, OpcionesGestionDeCobranza," & vbCrLf
   vResult = vResult & "               OpcionesPlanillaForma00030, OpcionesLoteAdm, OpcionesAnticipoCobrado," & vbCrLf
   vResult = vResult & "               OpcionesAnticipoPagado, MenuDeBalances, MenuDeInformes, ReiniciarTablasTemporales," & vbCrLf
   vResult = vResult & "               EsUsuarioTipoCajero, OpcionesCajeroFactura, OpcionesCajeroCliente," & vbCrLf
   vResult = vResult & "               OpcionesCajeroArtInventario, OpcionesCajeroCaja, fechaUltimaModificacion," & vbCrLf
   vResult = vResult & "               OpcionesColor, OpcionesTalla, OpcionesGrupoTC," & vbCrLf
   vResult = vResult & "               OpcionesExportarImportarSAW, OpcionesParametrosActivoFijo" & vbCrLf
   vResult = vResult & "            )VALUES(" & vbCrLf
   vResult = vResult & "               @UsuarioS10, '0', '', @UsuarioS10," & vbCrLf
   vResult = vResult & "               @UsuarioS10, '', 'N', ''," & vbCrLf
   vResult = vResult & "               'N', '', 'N', 'N', 'N', 'N'," & vbCrLf
   vResult = vResult & "               '', '', '', '', '', '', '', '', '', '', '', '', '', '', ''," & vbCrLf
   vResult = vResult & "               '', '', '', '', '', '', '', '', '', '', '', '', '', '', ''," & vbCrLf
   vResult = vResult & "               '', '', '', '', '', '', '', '', '', '', '', '', '', '', ''," & vbCrLf
   vResult = vResult & "               '', '', '', 'N', 'N', 'N', 'N', '', '', '', '', @Fecha, ''," & vbCrLf
   vResult = vResult & "               '', '', '', '' )" & vbCrLf
   vResult = vResult & "      SELECT @CodigoRet = Codigo FROM TablaRetencion WHERE Codigo = 'NORET' AND TipoDePersona = '0'" & vbCrLf
   vResult = vResult & "      IF ((@CodigoRet = null) or (@CodigoRet = ''))" & vbCrLf
   vResult = vResult & "         EXEC Gp_TablaRetencionINS @TipoDePersona = '0', @Codigo = 'NORET', @TipoDePago = 'No aplica retención', @FechaAplicacion = @Fecha" & vbCrLf
   vResult = vResult & "      SELECT @CodigoRet = Codigo FROM TablaRetencion WHERE Codigo = 'NORET' AND TipoDePersona = '1'" & vbCrLf
   vResult = vResult & "      IF ((@CodigoRet = null) or (@CodigoRet = ''))" & vbCrLf
   vResult = vResult & "         EXEC Gp_TablaRetencionINS  @TipoDePersona = '1', @Codigo = 'NORET', @TipoDePago = 'No aplica retención', @FechaAplicacion = @Fecha" & vbCrLf
   vResult = vResult & "      SELECT @CodigoRet = Codigo FROM TablaRetencion WHERE Codigo = 'NORET' AND TipoDePersona = '2'" & vbCrLf
   vResult = vResult & "      IF ((@CodigoRet = null) or (@CodigoRet = ''))" & vbCrLf
   vResult = vResult & "         EXEC Gp_TablaRetencionINS @TipoDePersona = '2', @Codigo = 'NORET', @TipoDePago = 'No aplica retención', @FechaAplicacion = @Fecha" & vbCrLf
   vResult = vResult & "      SELECT @CodigoRet = Codigo FROM TablaRetencion WHERE Codigo = 'NORET' AND TipoDePersona = '3'" & vbCrLf
   vResult = vResult & "      IF ((@CodigoRet = null) or (@CodigoRet = ''))" & vbCrLf
   vResult = vResult & "         EXEC Gp_TablaRetencionINS @TipoDePersona = '3', @Codigo = 'NORET', @TipoDePago = 'No aplica retención', @FechaAplicacion = @Fecha" & vbCrLf
   vResult = vResult & "      EXEC Gp_ParametrosAdministrativoUPD @Secuencial = @Sec, @PuedeUsarS10 = 'S', @S10DBName = @DBNameOfS10, @NombreOperador = @UsuarioS10" & vbCrLf
   vResult = vResult & "   END" & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSPActivarS10 = vResult
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
Private Function fCreateSpDesactivarS10(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fCreateStoredProcedure(GetSpNameDesactivarS10(), fSpParametersDesactivarS10(), fStatementsSPDesactivarS10(), valShowMessage, valWithEncryption)
   Set insLibSp = Nothing
   fCreateSpDesactivarS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSpDesactivarS10 ", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropSpDesactivarS10(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibSp As clsUtilStoredProcedures
   On Error GoTo h_ERROR
   Set insLibSp = New clsUtilStoredProcedures
   vResult = insLibSp.fDropStoredProc(GetSpNameDesactivarS10(), valShowMessage)
   Set insLibSp = Nothing
   fDropSpDesactivarS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSpDesactivarS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameDesactivarS10() As String
   GetSpNameDesactivarS10 = "S10_DesactivarS10"
End Function
Private Function fSpParametersDesactivarS10() As String
   Dim vResult As String
   vResult = " @DBNameOfS10 " & gUtilSQL.getVarCharTypeForDB(128) & vbCrLf
   fSpParametersDesactivarS10 = vResult
End Function
Private Function fStatementsSPDesactivarS10() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "BEGIN " & vbCrLf
   vResult = vResult & "   DECLARE @Sec int " & vbCrLf
   vResult = vResult & "   DECLARE @Operador varchar(10) " & vbCrLf
   vResult = vResult & "   SELECT TOP 1 @Sec = Secuencial FROM parametrosAdministrativo " & vbCrLf
   vResult = vResult & "   IF @Sec > 0 " & vbCrLf
   vResult = vResult & "   BEGIN " & vbCrLf
   vResult = vResult & "      SET @Operador = dbo.Gf_UsuarioS10() " & vbCrLf
   vResult = vResult & "      EXEC Gp_ParametrosAdministrativoUPD @Secuencial = @Sec, @PuedeUsarS10 = " & gUtilSQL.fBooleanToStrSQL(False) & ", @S10DBName = " & gUtilSQL.fSimpleSqlValue("") & ", @NombreOperador = @Operador " & vbCrLf
   vResult = vResult & "   END " & vbCrLf
   vResult = vResult & "END " & vbCrLf
   fStatementsSPDesactivarS10 = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSPDesactivarS10", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *

Private Function fCreateFnSpaceBeforeCap(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibFn As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibFn = New clsUtilStoredFunctions
   vResult = insLibFn.fCreateScalarFunction(GetSpNameSpaceBeforeCap(), fSpParametersSpaceBeforeCap(), gUtilSQL.getVarCharTypeForDB(100), fStatementsFnSpaceBeforeCap(), valShowMessage, valWithEncryption)
   Set insLibFn = Nothing
   fCreateFnSpaceBeforeCap = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFnSpaceBeforeCap", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fDropFnSpaceBeforeCap(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibFn As clsUtilStoredFunctions
   On Error GoTo h_ERROR
   Set insLibFn = New clsUtilStoredFunctions
   vResult = insLibFn.fDropFunction(GetSpNameSpaceBeforeCap(), valShowMessage)
   Set insLibFn = Nothing
   fDropFnSpaceBeforeCap = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFnSpaceBeforeCap", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function GetSpNameSpaceBeforeCap() As String
   GetSpNameSpaceBeforeCap = "SpaceBeforeCap"
End Function
Private Function fSpParametersSpaceBeforeCap() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = ""
   vResult = vResult & "@Nombre varchar(50)"
   fSpParametersSpaceBeforeCap = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParametersSpaceBeforeCap", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fStatementsFnSpaceBeforeCap() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = " BEGIN" & vbCrLf
   vResult = vResult & " DECLARE @i " & gUtilSQL.getNumericTypeForDB(9, 0) & ", @j " & gUtilSQL.getNumericTypeForDB(9, 0) & "" & vbCrLf
   vResult = vResult & " DECLARE @returnval nvarchar(max)" & vbCrLf
   vResult = vResult & " SET @returnval = ''" & vbCrLf
   vResult = vResult & " SELECT @i = 1, @j = len(@Nombre)" & vbCrLf
   vResult = vResult & " DECLARE @w nvarchar(max)" & vbCrLf
   vResult = vResult & " WHILE @i <= @j" & vbCrLf
   vResult = vResult & " BEGIN" & vbCrLf
   vResult = vResult & "  IF substring(@Nombre,@i,1) = UPPER(substring(@Nombre,@i,1)) collate Latin1_General_CS_AS" & vbCrLf
   vResult = vResult & "  BEGIN" & vbCrLf
   vResult = vResult & "   IF @w is not NULL" & vbCrLf
   vResult = vResult & "   SET @returnval = @returnval + ' ' + @w" & vbCrLf
   vResult = vResult & "   SET @w = substring(@Nombre,@i,1)" & vbCrLf
   vResult = vResult & "  END" & vbCrLf
   vResult = vResult & "  ELSE" & vbCrLf
   vResult = vResult & "   SET @w = @w + substring(@Nombre,@i,1)" & vbCrLf
   vResult = vResult & "  SET @i = @i + 1" & vbCrLf
   vResult = vResult & " END" & vbCrLf
   vResult = vResult & " IF @w is not NULL" & vbCrLf
   vResult = vResult & "  SET @returnval = @returnval + ' ' + @w" & vbCrLf
   vResult = vResult & " return ltrim(@returnval)" & vbCrLf
   vResult = vResult & " END" & vbCrLf
   fStatementsFnSpaceBeforeCap = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFnSpaceBeforeCap", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
