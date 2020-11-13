VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsParametrosCompaniaSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsParametrosCompaniaSQL"
Private Const CM_MESSAGE_NAME As String = "clsParametrosCompaniaSQL"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSQLSearchCamposRequired(ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim strCampos As String
   On Error GoTo h_ERROR
   strCampos = ""
   strCampos = strCampos & "ConsecutivoCompania, "
   strCampos = strCampos & "UsaMonedaExtranjera, "
   strCampos = strCampos & "UsaPrecioSinIva, "
   strCampos = strCampos & "EmitirDirecto, "
   strCampos = strCampos & "CantidadDeCopiasDeLaFacturaAlImprimir, "
   strCampos = strCampos & "UsaAlmacen, "
   strCampos = strCampos & "ConceptoReversoCobranza, "
   strCampos = strCampos & "ConceptoDebitoBancario, "
   strCampos = strCampos & "ModeloDeFactura, "
   strCampos = strCampos & "UsaCamposDefinibles, "
   strCampos = strCampos & "FacturaPreNumerada, "
   strCampos = strCampos & "PrimeraFactura, "
   strCampos = strCampos & "NumeroDeCerosAlaIzquierda, "
   strCampos = strCampos & "TipoDePrefijo, "
   strCampos = strCampos & "Prefijo, "
   strCampos = strCampos & "CodigoAlmacenGenerico, "
   strCampos = strCampos & "NoImprimirFactura, "
   strCampos = strCampos & "RellenaCerosAlaIzquierda, "
   strCampos = strCampos & "NumCopiasComprobanteRetencion, "
   strCampos = strCampos & "NumCopiasComprobantepago, "
   strCampos = strCampos & "UsaRetencion, "
   strCampos = strCampos & "InsertandoPorPrimeraVez, "
   strCampos = strCampos & "PermitirFacturarConCantidadCero, "
   strCampos = strCampos & "UsaCamposExtrasEnRenglonFactura, "
   strCampos = strCampos & "ConfirmarImpresionMovBancarioPorSecciones, "
   strCampos = strCampos & "ImprimirComprobanteDeMovBancario, "
   strCampos = strCampos & "ImprimirComprobanteContableDePago, "
   strCampos = strCampos & "ImprimirCentimosEnPlanilla, "
   strCampos = strCampos & "ModeloPlanillaForma00030, "
   strCampos = strCampos & "IvaEsCostoEnCompras, "
   strCampos = strCampos & "ImprimirCompraAlInsertar, "
   strCampos = strCampos & "ExigirRifdeClienteAlEmitirFactura " 'NO COLOCAR MÁS CAMPOS ACÁ
   SQL = "SELECT " & strCampos & " FROM " & "parametrosCompania" _
         & " WHERE ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   fSQLSearchCamposRequired = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchCamposRequired", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchCamposNoRequiredPartOne(ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim strCampos As String
   On Error GoTo h_ERROR
   strCampos = ""
   strCampos = strCampos & "ConsecutivoCompania, "
   strCampos = strCampos & "CodigoGenericoCliente, "
   strCampos = strCampos & "CodigoGenericoVendedor, "
   strCampos = strCampos & "NombreCampoDefinible1, "
   strCampos = strCampos & "NombreCampoDefinible2, "
   strCampos = strCampos & "NombreCampoDefinible3, "
   strCampos = strCampos & "NombreCampoDefinible4, "
   strCampos = strCampos & "NombreCampoDefinible5, "
   strCampos = strCampos & "NombreCampoDefinible6, "
   strCampos = strCampos & "NombreCampoDefinible7, "
   strCampos = strCampos & "NombreCampoDefinible8, "
   strCampos = strCampos & "NombreCampoDefinible9, "
   strCampos = strCampos & "NombreCampoDefinible10, "
   strCampos = strCampos & "NombreCampoDefinible11, "
   strCampos = strCampos & "NombreCampoDefinible12, "
   strCampos = strCampos & "NombreCampoDefinibleInventario1, "
   strCampos = strCampos & "NombreCampoDefinibleInventario2, "
   strCampos = strCampos & "NombreCampoDefinibleInventario3, "
   strCampos = strCampos & "NombreCampoDefinibleInventario4, "
   strCampos = strCampos & "NombreCampoDefinibleInventario5, "
   strCampos = strCampos & "NombrePlantillaComprobanteDePago, "
   strCampos = strCampos & "NombrePlantillaFactura, "
   strCampos = strCampos & "TipoDeOrdenDePagoAimprimir, "
   strCampos = strCampos & "CodigoGenericoCuentaBancaria, "
   strCampos = strCampos & "ImprimirFacturaConSubtotalesPorLineaDeProducto, "
   strCampos = strCampos & "NombrePlantillaCotizacion, "
   strCampos = strCampos & "NombrePlantillaCompobanteCobranza, "
   strCampos = strCampos & "ImprimirCombrobanteAlIngresarCobranza, "
   strCampos = strCampos & "NoImprimirComprobanteDePago, "
   strCampos = strCampos & "UsarOtrosCargoDeFactura, "
   strCampos = strCampos & "UsarBaseImponibleDiferenteA0Y100, "
   strCampos = strCampos & "SolicitarIngresoDeTasaDeCambioAlEmitir, "
   strCampos = strCampos & "NombrePlantillaOrdenDeCompra, "
   strCampos = strCampos & "ImprimirOrdenDeCompra, "
   strCampos = strCampos & "UsarDecimalesAlImprimirCantidad, " '35
   strCampos = strCampos & "NombrePlantillaNotaDeCredito, "
   strCampos = strCampos & "NombrePlantillaNotaDeDebito, "
   strCampos = strCampos & "ForzarFechaFacturaAmesEspecifico, "
   strCampos = strCampos & "MesFacturacionEnCurso, "
   strCampos = strCampos & "ImprimirComprobanteContableDePago "
   SQL = "SELECT " & strCampos & " FROM " & "parametrosCompania" _
         & " WHERE ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   fSQLSearchCamposNoRequiredPartOne = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchCamposNoRequiredPartOne", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchCamposNoRequiredPartTwo(ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim strCampos As String
   On Error GoTo h_ERROR
   strCampos = ""
   strCampos = strCampos & "ConsecutivoCompania, "
   strCampos = strCampos & "ImprimirReporteAlIngresarNotaEntradaSalida, "
   strCampos = strCampos & "FormaDeCalcularComisionesSobreCobranza, "
   strCampos = strCampos & "NombrePlantillaComisionSobreCobranza, "
   strCampos = strCampos & "NombrePlantillaNotaEntradaSalida, "
   strCampos = strCampos & "PrimeraNotaDeCredito, "
   strCampos = strCampos & "NCPreNumerada, "
   strCampos = strCampos & "TipoDePrefijoNC, "
   strCampos = strCampos & "PrefijoNC, "
   strCampos = strCampos & "PrimeraNotaDeDebito, "
   strCampos = strCampos & "NDPreNumerada, "
   strCampos = strCampos & "TipoDePrefijoND, "
   strCampos = strCampos & "PrefijoND, "
   strCampos = strCampos & "UsaMultiplesAlicuotas, "
   strCampos = strCampos & "FechaDeInicioContabilizacion, "
   strCampos = strCampos & "PermitirIncluirFacturacionHistorica, "
   strCampos = strCampos & "GenerarCxCalEmitirUnaFacturaHistorica, "
   strCampos = strCampos & "UltimaFechaDeFacturacionHistorica, "
   strCampos = strCampos & "UsarDosTalonarios, "
   strCampos = strCampos & "FacturaPreNumerada2, "
   strCampos = strCampos & "ModeloDeFactura2, "
   strCampos = strCampos & "PrimeraFactura2, "
   strCampos = strCampos & "TipoDePrefijo2, "
   strCampos = strCampos & "Prefijo2, "
   strCampos = strCampos & "NombrePlantillaFactura2, "
   strCampos = strCampos & "ConsideraConciliadosLosMovIngresadosAntesDeFecha, "
   strCampos = strCampos & "FechaDeInicioConciliacion, "
   strCampos = strCampos & "DiaDelCierreFiscal, "
   strCampos = strCampos & "MesDelCierreFiscal, "
   strCampos = strCampos & "NombreYapellidor, "
   strCampos = strCampos & "CodTelfr, "
   strCampos = strCampos & "Telefonor, "
   strCampos = strCampos & "Direccionr, "
   strCampos = strCampos & "NumeroRifr, "
   strCampos = strCampos & "PedirInformacionLibroVentasXlsalEmitirFactura, "
   strCampos = strCampos & "SugerirNumeroControlFactura, "
   strCampos = strCampos & "EnDondeRetenerIva, "
   strCampos = strCampos & "PrimerNumeroComprobanteRetIva, "
   strCampos = strCampos & "FormaDeReiniciarElNumeroDeComprobanteRetIva, "
   strCampos = strCampos & "NumeroDeCopiasComprobanteRetencionIva, "
   strCampos = strCampos & "UnComprobanteDeRetIvaporHoja, "
   strCampos = strCampos & "NombrePlantillaComprobanteDeRetIva, "
   strCampos = strCampos & "GenerarNumCompDeRetIvasoloSiPorcentajeEsMayorAcero,"
   strCampos = strCampos & "ExigirInformacionLibroDeCompras, "
   strCampos = strCampos & "NombrePlantillaCodigoDeBarras, "
   strCampos = strCampos & "MetodoDeCosteo,"
   strCampos = strCampos & "FechaDesdeUsoMetodoDeCosteo,"
   strCampos = strCampos & "CalculoAutomaticoDeCosto,"
   strCampos = strCampos & "ComprobanteCostoDetallado"
   
   SQL = "SELECT " & strCampos & " FROM " & "parametrosCompania" _
         & " WHERE ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   fSQLSearchCamposNoRequiredPartTwo = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchCamposNoRequiredPartTwo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchCamposNoRequiredPartThree(ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim strCampos As String
   On Error GoTo h_ERROR
   strCampos = ""
   strCampos = strCampos & "ConsecutivoCompania, "
   strCampos = strCampos & "CorreoElectronicoRepLegal, "
   strCampos = strCampos & "NombreContador, "
   strCampos = strCampos & "CedulaContador, "
   strCampos = strCampos & "NumeroCpc, "
   strCampos = strCampos & "CiudadRepLegal, "
   strCampos = strCampos & "ImprimirComprobanteDeRetIva, "
   strCampos = strCampos & "ImprimirCompContDespuesDeChequeMovBancario, "
   strCampos = strCampos & "NumCopiasComprobanteMovBancario, "
   strCampos = strCampos & "NombrePlantillaComprobanteDeMovBancario, "
   strCampos = strCampos & "NombrePlantillaCompra, "
   strCampos = strCampos & "UsarResumenDiarioDeVentas, "
   strCampos = strCampos & "UsaPrecioSinIvaEnResumenVtas, "
   strCampos = strCampos & "UsarRenglonesEnResumenVtas, "
   strCampos = strCampos & "ResumenVtasAfectaInventario, "
   strCampos = strCampos & "DetalleProdCompFactura, "
   strCampos = strCampos & "DetalleProdCompCotizacion, "
   strCampos = strCampos & "FormaDeOrdenarDetalleFactura, "
   strCampos = strCampos & "ImprimirBorradorAlInsertarFactura,"
   strCampos = strCampos & "CuentaBancariaAnticipo, "
   strCampos = strCampos & "NombrePlantillaReciboDeAnticipoCobrado, "
   strCampos = strCampos & "NombrePlantillaReciboDeAnticipoPagado, "
   strCampos = strCampos & "ConceptoBancarioAnticipoCobrado, "
   strCampos = strCampos & "ConceptoBancarioAnticipoPagado, "
   strCampos = strCampos & "ConceptoBancarioReversoAnticipoCobrado, "
   strCampos = strCampos & "ConceptoBancarioReversoAnticipoPagado, "
   strCampos = strCampos & "UsaCobroDirecto, "
   strCampos = strCampos & "CuentaBancariaCobroDirecto, "
   strCampos = strCampos & "ConceptoBancarioCobroDirecto, "
   strCampos = strCampos & "SugerirConsecutivoAnticipo, "
   strCampos = strCampos & "TipoComprobanteDeAnticipoAimprimir, "
   strCampos = strCampos & "TomarEnCuentaRetencionesCeroParaArcvyRa, "
   strCampos = strCampos & "TipoDeNivelDePrecios"
   SQL = "SELECT " & strCampos & " FROM " & "parametrosCompania" _
         & " WHERE ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   fSQLSearchCamposNoRequiredPartThree = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchCamposNoRequiredPartThree", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchCamposNoRequiredPartFour(ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim strCampos As String
   On Error GoTo h_ERROR
   strCampos = ""
   strCampos = strCampos & "ConsecutivoCompania, "
   strCampos = strCampos & "CambiarFechaEnCuotasLuegoDeFijarFechaEntrega, "
   strCampos = strCampos & "EsAsociadoEnCtaDeParticipacion, "
   strCampos = strCampos & "ComisionesEnFactura, "
   strCampos = strCampos & "ComisionesEnRenglones, "
   strCampos = strCampos & "ItemsMonto, "
   strCampos = strCampos & "NombreVendedorUno, "
   strCampos = strCampos & "NombreVendedorDos, "
   strCampos = strCampos & "NombreVendedorTres, "
   strCampos = strCampos & "MaximoDescuentoEnFactura, "
   strCampos = strCampos & "GenerarMovReversoSiAnulaPago, "
   strCampos = strCampos & "ConceptoBancarioReversoDePago, "
   strCampos = strCampos & "AccionAlAnularFactDeMesesAnt,"
   strCampos = strCampos & "ColocarEnFacturaElVendedorAsinagoAlCliente,"
   strCampos = strCampos & "ModeloFacturaModoTexto,"
   strCampos = strCampos & "ModeloFacturaModoTexto2,"
   strCampos = strCampos & "PermitirDobleDescuentoEnFactura, "
   strCampos = strCampos & "ComplConComodinEnBusqDeArtInv,"
   strCampos = strCampos & "ImprimirAnexoDeSerial,"
   strCampos = strCampos & "NombrePlantillaAnexoSeriales,"
   strCampos = strCampos & "SinonimoGrupo,"
   strCampos = strCampos & "SinonimoTalla,"
   strCampos = strCampos & "SinonimoColor,"
   strCampos = strCampos & "SinonimoSerial,"
   strCampos = strCampos & "SinonimoRollo,"
   strCampos = strCampos & "ImprimirDatosClienteEnCompFiscal,"
   strCampos = strCampos & "AsignarComisionDeVendedorEnCobranza,"
   strCampos = strCampos & "ConceptoCreditoBancario, "
   strCampos = strCampos & "ManejaDebitoBancario, "
   strCampos = strCampos & "ManejaCreditoBancario, "
   strCampos = strCampos & "RedondeaMontoDebitoBancario, "
   strCampos = strCampos & "RedondeaMontoCreditoBancario, "
   strCampos = strCampos & "NombrePlantillaBoleta, "
   strCampos = strCampos & "PrimeraBoleta, "
   strCampos = strCampos & "FormatoDeFecha, "
   strCampos = strCampos & "ConcatenaLetraEaArticuloExento, "
   strCampos = strCampos & "NumItemImprimirFactura, "
   strCampos = strCampos & "AccionLimiteItemsFactura, "
   strCampos = strCampos & "LimpiezaDeCotizacionXFactura, "
   strCampos = strCampos & "VerificarDocumentoSinContabilizar, "
   strCampos = strCampos & "ImprimeDireccionAlFinalDelComprobanteFiscal, "
   strCampos = strCampos & "BloquearNumeroCobranza, "
   strCampos = strCampos & "CambiarCobradorVendedor, "
   strCampos = strCampos & "AsociaCentroDeCostoyAlmacen, "
   strCampos = strCampos & "CantidadDeDecimales, "
   strCampos = strCampos & "TipoDeAgrupacionParaLibrosDeVenta, "
   strCampos = strCampos & "IntegracionRIS, "
   strCampos = strCampos & "AvisoDeFacturacionMenor, "
   strCampos = strCampos & "FechaMinimaIngresarDatos, "
   strCampos = strCampos & "NombreCampoDefinibleCliente1, "
   strCampos = strCampos & "OrdenarCxPPorFacturaDocumento, "
   strCampos = strCampos & "FechaContabilizacionDeCosteo, "
   strCampos = strCampos & "UsaMismoNumeroCompRetTodasCxP, "
   strCampos = strCampos & "BuscarArticuloXSerialAlFacturar, "
   strCampos = strCampos & "AvisoDeReservasvencidas, "
   strCampos = strCampos & "VerificarStock,"
   strCampos = strCampos & "ImprimirTipoCobroEnFactura, "
   strCampos = strCampos & "BeneficiarioGenerico,  "
   strCampos = strCampos & "ConceptoBancarioReversoSolicitudDePago, "
   strCampos = strCampos & "NombrePlantillaComprobanteDePagoSueldo,"
   strCampos = strCampos & "EnDondeRetenerISLR "
   SQL = "SELECT " & strCampos & " FROM " & "parametrosCompania" _
         & " WHERE ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
    fSQLSearchCamposNoRequiredPartFour = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchCamposNoRequiredPartFour", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchCamposNoRequiredPartFive(ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim strCampos As String
   On Error GoTo h_ERROR
   strCampos = ""
   strCampos = strCampos & "ConsecutivoCompania, "
   strCampos = strCampos & "TipoNegocio, "
   strCampos = strCampos & "ImprimirTransferenciaAlInsertar, "
   strCampos = strCampos & "ActivarFacturacionPorAlmacen, "
   strCampos = strCampos & "ImprimirNotaESconPrecio, "
   strCampos = strCampos & "AutorellenaResumenDiario, "
   strCampos = strCampos & "RetieneImpuestoMunicipal,"
   strCampos = strCampos & "NombrePlantillaRetencionImpuestoMunicipal,"
   strCampos = strCampos & "ModeloNotaEntrega, "
   strCampos = strCampos & "NotaEntregaPreNumerada , "
   strCampos = strCampos & "PrimeraNotaEntrega , "
   strCampos = strCampos & "TipoPrefijoNotaEntrega , "
   strCampos = strCampos & "PrefijoNotaEntrega , "
   strCampos = strCampos & "NombrePlantillaNotaEntrega,"
   strCampos = strCampos & "NombrePlantillaOrdenDeDespacho,"
   strCampos = strCampos & "NumCopiasOrdenDeDespacho,"
   strCampos = strCampos & "UsaNotaEntrega,"
   strCampos = strCampos & "ConsecutivoAlmacenGenerico, "
   strCampos = strCampos & "ValidarArticulosAlGenerarFactura, "
   strCampos = strCampos & "CampoCodigoAlternativoDeArticulo, "
   strCampos = strCampos & "ImprimeSerialRolloLuegoDeDescripArticulo"
   SQL = "SELECT " & strCampos & " FROM " & "parametrosCompania" _
         & " WHERE ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
    fSQLSearchCamposNoRequiredPartFive = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSearchCamposNoRequiredPartFive", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
    
