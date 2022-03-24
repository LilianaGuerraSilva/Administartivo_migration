VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsSawViews"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsSawViews"
Private Const CM_MESSAGE_NAME As String = "clsSawViews"

Public Function fCreateProjectViews(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean, ByVal valEsSistemaInterno As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibViews As clsUtilViews
   Dim insClienteVista As clsClienteVista
   Dim insCobranzaVista As clsCobranzaVista
   Dim insFacturaVista As clsFacturaVista
   Dim insCxPVista As clsCxPVista
   Dim insCxCVista As clsCxCVista
   Dim insBancoVista As clsBancoVista
   Dim insMovBanSQL As clsMovimientoBancarioSQL
   Dim insLibroVCVista As clsLibrosVentasYComprasVista
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim insPagoVista As clsPagoVista
   Dim insSawEnum As clsSawEnumVista
   Dim insPDTVista As clsPDTVista
   Dim insProveedor As clsProveedorVista
   Dim insAnticipoVista As clsAnticipoVista
   Dim insComprasVista As clsComprasVista
   Dim insWcoViews As clsWcoViews
   Dim insCambioVista As clsCambioVista
   Dim insBusinessintelligenceVistas As clsbusinessintelligenceVistas
   Dim insclsSolicitudesDePagoVista As clsSolicitudesDePagoVista
   Dim insControlDespachoVista As clsControlDespachoVista
   On Error GoTo h_ERROR
   Set insLibViews = New clsUtilViews
   Set insClienteVista = New clsClienteVista
   Set insMovBanSQL = New clsMovimientoBancarioSQL
   Set insCobranzaVista = New clsCobranzaVista
   Set insFacturaVista = New clsFacturaVista
   Set insCxPVista = New clsCxPVista
   Set insCxCVista = New clsCxCVista
   Set insBancoVista = New clsBancoVista
   Set insLibroVCVista = New clsLibrosVentasYComprasVista
   Set insArticuloInvVista = New clsArticuloInvVista
   Set insPagoVista = New clsPagoVista
   Set insSawEnum = New clsSawEnumVista
   Set insPDTVista = New clsPDTVista
   Set insProveedor = New clsProveedorVista
   Set insAnticipoVista = New clsAnticipoVista
   Set insComprasVista = New clsComprasVista
   Set insWcoViews = New clsWcoViews
   Set insCambioVista = New clsCambioVista
   Set insBusinessintelligenceVistas = New clsbusinessintelligenceVistas
   Set insclsSolicitudesDePagoVista = New clsSolicitudesDePagoVista
   Set insControlDespachoVista = New clsControlDespachoVista
   vResult = True
'vResult = vResult And insLibViews.fCreateView(GetViewNameX(), fSqlViewX(), valShowMessage, valWithEncryption)
'El orden de estas debe ser dependiendo más de las referencias y dependencias
'que se usen que si es o no la más nueva
   vResult = vResult And insSawEnum.fCreateProjectEnumViews(False, valWithEncryption)
   vResult = vResult And insWcoViews.fCreateProjectViews(False, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBancoVista.GetViewNameUnionMovBCtaB(), insBancoVista.fSqlViewUnionMovBCtaB(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insClienteVista.GetViewNameFichaCliente(), insClienteVista.fSqlViewFichaCliente(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insClienteVista.GetViewNameClientexFactura(), insClienteVista.fSqlViewClientexFactura(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCobranzaVista.GetViewNameMontoNoComisibleFactura(), insCobranzaVista.fSqlViewMontoNoComisibleFactura, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCobranzaVista.GetViewNameMontoComisionXCobranza(), insCobranzaVista.fSqlViewMontoComisionXCobranza, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCobranzaVista.GetViewNameComisionDeVendedoresPorCobroDetallado(), insCobranzaVista.fSqlViewComisionDeVendedoresPorCobroDetallado, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxCVista.GetViewNameVentasGeneradasCxC(), insCxCVista.fSqlViewVentasGeneradasCxC(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxPVista.GetViewNameComprasGeneradasCxP(), insCxPVista.fSqlViewComprasGeneradasCxP(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insFacturaVista.GetViewNameVentasGeneradasFactura(), insFacturaVista.fSqlViewVentasGeneradasFactura(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCobranzaVista.GetViewRetencionesDelPeriodo(), insCobranzaVista.fSQLViewRetencionesDelPeriodo(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBancoVista.GetViewNameMovimientosXCuentaBancaria0695(), insBancoVista.fSqlViewMovimientosXCuentaBancaria0695, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBancoVista.GetViewNameMovBCtaBGeneradaXAnticipoCobrado(), insBancoVista.fSqlViewMovBCtaBGeneradaXAnticipoCobrado, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBancoVista.GetViewNameMovBCtaBGeneradaXAnticipoPagado(), insBancoVista.fSqlViewMovBCtaBGeneradaXAnticipoPagado, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBancoVista.GetViewNameMovBCtaBGeneradaXCobranza(), insBancoVista.fSqlViewMovBCtaBGeneradaXCobranza, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBancoVista.GetViewNameMovBCtaBGeneradaXDebitoBancario(), insBancoVista.fSqlViewMovBCtaBGeneradaXDebitoBancario, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBancoVista.GetViewNameMovBCtaBGeneradaXPago(), insBancoVista.fSqlViewMovBCtaBGeneradaXPago, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBancoVista.GetViewNameMovBCtaBGeneradaXRestantes(), insBancoVista.fSqlViewMovBCtaBGeneradaXRestantes, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBancoVista.GetViewNameMovBCtaBGeneradaXUsuario(), insBancoVista.fSqlViewMovBCtaBGeneradaXUsuario, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBancoVista.GetViewNameMovBCtaBGeneradaXUsuarioSinCheque(), insBancoVista.fSqlViewMovBCtaBGeneradaXUsuarioSinCheque, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insLibroVCVista.GetViewNameFacturaConCliente(), insLibroVCVista.fSqlViewFacturaConCliente, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insLibroVCVista.GetViewNameCxcConCliente(), insLibroVCVista.fSqlViewCxcConCliente, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insLibroVCVista.GetViewNameFacturaAfectada(), insLibroVCVista.fSqlViewFacturaAfectada, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insLibroVCVista.GetViewNameUnionFacturaYCxc(), insLibroVCVista.fSqlViewUnionFacturaYCxc, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insLibroVCVista.GetViewNameRegistroVentasPeru(), insLibroVCVista.fSqlViewRegistroVentasPeru, valShowMessage, valWithEncryption)
'   vResult = vResult And insLibViews.fCreateView(insLibroVCVista.GetViewNameCxPparaRegistroComprasPeru(), insLibroVCVista.fSqlViewCxPparaRegistroComprasPeru, valShowMessage, valWithEncryption)
'   vResult = vResult And insLibViews.fCreateView(insLibroVCVista.GetViewNameCxPAfectada(), insLibroVCVista.fSqlViewCxPAfectada, valShowMessage, valWithEncryption)
   'vResult = vResult And insLibViews.fCreateView(insLibroVCVista.GetViewNameRegistroComprasPeru(), insLibroVCVista.fSqlViewRegistroComprasPeru, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1, insArticuloInvVista.fSqlViewNameArticulosDeInventarioConOSinExistenPorGrupoB1, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameCompraDeArticulo(), insArticuloInvVista.fSqlViewCompraDeArticulo, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameVentaDeArticulo(), insArticuloInvVista.fSqlViewVentaDeArticulo, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameNotaEntradaSalidaDeArticulo(), insArticuloInvVista.fSqlViewNotaEntradaSalidaDeArticulo, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameConteoFisicoDeArticulo(), insArticuloInvVista.fSqlViewConteoFisicoDeArticulo, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameRegistroInventarioPermanente(), insArticuloInvVista.fSqlViewRegistroInventarioPermanente, valShowMessage, valWithEncryption)
'   vResult = vResult And insLibViews.fCreateView(insPagoVista.GetViewNameRetencionXDocumentoPagado(), insPagoVista.fSqlViewRetencionXDocumentoPagado, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameRegistroInventarioPermanenteValorizado(), insArticuloInvVista.fSqlViewRegistroInventarioPermanenteValorizado, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insPagoVista.GetViewNameLibroDeRetencionesRenta(), insPagoVista.fSqlViewLibroDeRetencionesRenta, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insPDTVista.GetViewNamePDTVentasRentaMensual(), insPDTVista.fViewPDTVentasRentaMensual, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxCVista.GetViewNameDocCobradosPorMoneda(), insCxCVista.fSqlViewDocumentoCobrado, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxPVista.GetViewNameDocPagadosPorMoneda(), insCxPVista.fSqlViewDocumentoPagado, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxPVista.GetViewNameCompras(), insCxPVista.fSqlViewCompras, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insProveedor.GetViewNameProveedor(), insProveedor.fSqlViewProveedor, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxCVista.GetViewNameRetencionDocumentoCobrado(), insCxCVista.fSqlViewRetencionDocumentoCobrado, valShowMessage, valWithEncryption)
   
   vResult = vResult And insLibViews.fCreateView(insCxCVista.GetViewNameRetencionesEnVentas(), insCxCVista.fSqlViewRetencionesEnVentas, valShowMessage, valWithEncryption)
   
   
  
   vResult = vResult And insLibViews.fCreateView(insCxCVista.GetViewNameBaseimponibleAlicuotasVentasResumenDeVentas(), insCxCVista.fSqlViewBaseimponibleAlicuotasVentasResumenDeVentas, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxCVista.GetViewNameVentas(), insCxCVista.fSqlViewVentasIVA, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxCVista.GetViewNameFacturaBase(), insCxCVista.fSqlViewFacturaBase, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxCVista.GetViewNameVentasAgrupado(), insCxCVista.fSqlViewVentasIVAAgrupado, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxCVista.GetViewNameVentasDetallado(), insCxCVista.fSqlViewVentasIVADetallado, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxCVista.GetViewNameSaldoInicialCxCHistoricoCliente(), insCxCVista.fSqlViewSaldoInicialCxCHistoricoCliente, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxCVista.GetViewNameInfoCxCHistoricoCliente(), insCxCVista.fSqlViewInfoCxCHistoricoCliente, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxCVista.GetViewNameCxCHistoricoClientes(), insCxCVista.fSqlViewCxCHistoricoClientes, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insAnticipoVista.GetViewNameAnticipoHistoricoCliente(), insAnticipoVista.fSqlViewAnticipoHistoricoCliente, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insAnticipoVista.GetViewNameSaldoInicialAnticipoHistoricoCliente(), insAnticipoVista.fSqlViewSaldoInicialAnticipoHistoricoCliente, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores, insArticuloInvVista.fSqlViewNameExistenciaPorGrupoConTallasyColores, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1, insArticuloInvVista.fSqlViewNameExistenciaPorAlmacenArticulosDeInvenpIB1, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameUbicacionXAlmacenDeArticulo(), insArticuloInvVista.fSqlViewUbicacionXAlmacenDeArticulo, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insPagoVista.GetViewNamePagosDeclracionMensualXMLSalariosOtros(), insPagoVista.fsqlViewPagosDeclracionMensualXMLSalariosOtros, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insPagoVista.GetViewNamePagosDeclracionMensualXMLDividendoYAcciones(), insPagoVista.fsqlViewPagosDeclracionMensualXMLDividendoYAcciones, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insPagoVista.GetViewNamePagosDeclracionMensualXMLGananciasFortuitas(), insPagoVista.fsqlViewGananciasFortuitas, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insAnticipoVista.GetViewNameAnticiposCobradosAUnaFecha(), insAnticipoVista.fSqlViewAnticiposCobradosAUnaFecha, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insAnticipoVista.GetViewNameAnticiposPagadosAUnaFecha(), insAnticipoVista.fSqlViewAnticiposPagadosAUnaFecha, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCobranzaVista.GetViewNameCobranzaPorZona(), insCobranzaVista.fSqlViewCobranzaPorZona, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameInventarioExistenciaXGrupo(), insArticuloInvVista.fSqlViewInventarioExistenciaXGrupo, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameInventarioMercanciaEnTransito(), insArticuloInvVista.fSqlViewInventarioMercanciaEnTransito, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insFacturaVista.GetViewNameVendedorXRenglonFactura(), insFacturaVista.fSqlViewVendedorXRenglonFactura(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insComprasVista.GetViewNameRenglonComprasConOSinSerial(), insComprasVista.fSqlViewRenglonComprasConOSinSerial(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insComprasVista.GetViewNameComprasOtroGastosPorItem, insComprasVista.fSqlViewComprasOtroGastosPorItem, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos, insComprasVista.fSqlViewComprasTotalCompraSinGastos, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameArticuloInvMovimiento(), insArticuloInvVista.fSqlViewArticuloInvMovimiento(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameArticuloInvMovimientoDetallado(), insArticuloInvVista.fSqlViewArticuloInvMovimientoDetallado(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameInventarioExistenciaXGrupoMod_B1, insArticuloInvVista.fSqlViewInventarioExistenciaXGrupoMod_B1, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameArticuloXAlmacen_pIEB1, insArticuloInvVista.fSqlViewArticuloXAlmacen_pIEB1, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameCompraFechaUltimaCompra(), insArticuloInvVista.fSqlViewCompraFechaUltimaCompra(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameComprasSecUltCom(), insArticuloInvVista.fSqlViewComprasSecUltCom(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameComprasConOsinSeriales(), insArticuloInvVista.fSqlViewComprasConOsinSeriales(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales(), insArticuloInvVista.fSqlViewComprasUltimaConOsinSeriales(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insFacturaVista.GetViewNameNotasDeCreditoEntreFechasPorVendedor(), insFacturaVista.fSqlViewNotasDeCreditoEntreFechasPorVendedor(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insFacturaVista.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito(), insFacturaVista.fSqlViewGenerarOtrosCargosyDescuentosNotasDeCredito(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCambioVista.GetViewNameCambioMaxFecha(), insCambioVista.fSqlViewCambioMaxFecha(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCambioVista.GetViewNameCambioAlaFecha(), insCambioVista.fSqlViewCambioAlaFecha(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insComprasVista.GetViewNameComprasConCambioOriginal(), insComprasVista.fSqlViewComprasConCambioOriginal(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insComprasVista.GetViewNameComprasConCambioAlDia(), insComprasVista.fSqlViewComprasConCambioAlDia(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insComprasVista.GetViewNameComprasImprimirEtiquetasPorCompras(), insComprasVista.fSqlViewComprasImprimirEtiquetasPorCompras(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insPagoVista.GetViewNameMercantilCabecera(), insPagoVista.fsqlViewMercantilCabecera(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insPagoVista.GetViewNameMercantilPago(), insPagoVista.fsqlViewMercantilPago(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insPagoVista.GetViewNameMercantilOperaciones(), insPagoVista.fsqlViewMercantilOperaciones(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insPagoVista.GetViewNameBODCabecera(), insPagoVista.fsqlViewBODCabecera(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insPagoVista.GetViewNameBODPago(), insPagoVista.fsqlViewBODPago(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insPagoVista.GetViewNameBODOperaciones(), insPagoVista.fsqlViewBODOperaciones(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insComprasVista.GetViewNameComprasConOtroGastos(), insComprasVista.fSqlViewComprasConOtroGastos(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insComprasVista.GetViewNameComprasConCuentasArticulos(), insComprasVista.fSqlViewComprasConCuentasArticulos(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameArticuloInvMovimientoAlmacen(), insArticuloInvVista.fSqlViewArticuloInvMovimientosAlmacen(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameTransferenciasPorArticulo(), insArticuloInvVista.fSqlViewTransferenciasPorArticulo(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameTransferenciasPorAlmacen(), insArticuloInvVista.fSqlViewTransferenciasPorAlmacen(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameArticulosMovimientoTransferencia(), insArticuloInvVista.fSqlViewArticulosMovimientoTransferencia(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCobranzaVista.GetViewNameCobranzaUltimaFecha(), insCobranzaVista.fSqlViewCobranzaUltimaFecha(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameProductosCompuestos_B1(), insArticuloInvVista.fSqlViewProductosCompuestos_B1(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameArticulosProductosCompuestos(), insArticuloInvVista.fSqlViewArticulosCompuestos(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBusinessintelligenceVistas.GetViewNameBIDimensionVentas_B1(), insBusinessintelligenceVistas.fSqlViewBIDimensionVentas(valEsSistemaInterno), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameInventarioReservas(), insArticuloInvVista.fSqlViewInventarioReservas(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameInventarioConOSinReservas(), insArticuloInvVista.fSqlViewInventarioConOSinReservas, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insFacturaVista.GetViewNameVentasConVendedorCliente(), insFacturaVista.fSqlViewVentasConVendedorCliente(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insFacturaVista.GetViewNameVentasConDescripcionArticulos(), insFacturaVista.fSqlViewVentasConDescripcionArticulos(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBusinessintelligenceVistas.GetViewNameBIDimensionCotizacion_B1(), insBusinessintelligenceVistas.fSqlViewBIDimensionCotizacion(valEsSistemaInterno), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen(), insArticuloInvVista.fSqlViewArticulosReservadosPorAlmacen(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion(), insArticuloInvVista.fSqlViewArticulosReservadosPorCotizacion(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insBancoVista.GetViewNameMovimientosBancarios_B1(), insBancoVista.fSqlViewMovimientosBancarios_B1(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insclsSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1(), insclsSolicitudesDePagoVista.fSqlViewBeneficiarioSolicitudesDePago_B1(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCxPVista.GetViewNameRetencionImpuestoMunicipalCxP(), insCxPVista.fSqlViewRetencionImpuestoMunicipalCxP(), valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insCobranzaVista.GetViewNameComisionVendedorXCobranzaXArticulo(), insCobranzaVista.fSqlViewComisionVendedorXCobranzaXArticulo, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insPagoVista.GetViewNameCompraRetencionesPorConceptoISLR(), insPagoVista.fsqlViewCompraRetencionesPorConceptoISLR, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insControlDespachoVista.GetViewNameControlDespachoFactura, insControlDespachoVista.fSqlViewControlDespachoFactura, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(insControlDespachoVista.GetViewNameDetalleControlDespachoFactura, insControlDespachoVista.fSqlViewDetalleControlDespachoFactura, valShowMessage, valWithEncryption)

    If vResult And valShowMessage Then
      gMessage.ProgrammerMessage "Se Crearon con Exito las Vistas"
   End If
   fCreateProjectViews = vResult
   Set insPDTVista = Nothing
   Set insCobranzaVista = Nothing
   Set insLibViews = Nothing
   Set insClienteVista = Nothing
   Set insBancoVista = Nothing
   Set insMovBanSQL = Nothing
   Set insFacturaVista = Nothing
   Set insCxPVista = Nothing
   Set insCxCVista = Nothing
   Set insLibroVCVista = Nothing
   Set insArticuloInvVista = Nothing
   Set insPagoVista = Nothing
   Set insSawEnum = Nothing
   Set insProveedor = Nothing
   Set insComprasVista = Nothing
   Set insWcoViews = Nothing
   Set insAnticipoVista = Nothing
   Set insCambioVista = Nothing
   Set insBusinessintelligenceVistas = Nothing
  Set insclsSolicitudesDePagoVista = Nothing
  Set insControlDespachoVista = Nothing
h_EXIT:  On Error GoTo 0
   vResult = False
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateProjectViews", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fDropProjectViews(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
    Dim insLibViews As clsUtilViews
   Dim insClienteVista As clsClienteVista
   Dim insCobranzaVista As clsCobranzaVista
   Dim insFacturaVista As clsFacturaVista
   Dim insCxPVista As clsCxPVista
   Dim insCxCVista As clsCxCVista
   Dim insBancoVista As clsBancoVista
   Dim insMovBanSQL As clsMovimientoBancarioSQL
   Dim insLibroVCVista As clsLibrosVentasYComprasVista
   Dim insArticuloInvVista As clsArticuloInvVista
   Dim insPagoVista As clsPagoVista
   Dim insSawEnum As clsSawEnumVista
   Dim insPDTVista As clsPDTVista
   Dim insProveedor As clsProveedorVista
   Dim insAnticipoVista As clsAnticipoVista
   Dim insComprasVista As clsComprasVista
   Dim insWcoViews As clsWcoViews
   Dim insCambioVista As clsCambioVista
   Dim insBusinessintelligenceVistas As clsbusinessintelligenceVistas
   Dim insclsSolicitudesDePagoVista As clsSolicitudesDePagoVista
   Dim insControlDespachoVista As clsControlDespachoVista
   On Error GoTo h_ERROR
   Set insLibViews = New clsUtilViews
   Set insClienteVista = New clsClienteVista
   Set insMovBanSQL = New clsMovimientoBancarioSQL
   Set insCobranzaVista = New clsCobranzaVista
   Set insFacturaVista = New clsFacturaVista
   Set insCxPVista = New clsCxPVista
   Set insCxCVista = New clsCxCVista
   Set insBancoVista = New clsBancoVista
   Set insLibroVCVista = New clsLibrosVentasYComprasVista
   Set insArticuloInvVista = New clsArticuloInvVista
   Set insPagoVista = New clsPagoVista
   Set insSawEnum = New clsSawEnumVista
   Set insPDTVista = New clsPDTVista
   Set insProveedor = New clsProveedorVista
   Set insAnticipoVista = New clsAnticipoVista
   Set insComprasVista = New clsComprasVista
   Set insWcoViews = New clsWcoViews
   Set insCambioVista = New clsCambioVista
   Set insBusinessintelligenceVistas = New clsbusinessintelligenceVistas
   Set insclsSolicitudesDePagoVista = New clsSolicitudesDePagoVista
   Set insControlDespachoVista = New clsControlDespachoVista
   vResult = True
'vResult = vResult And insLibViews.fDropView(GetViewNameX(), valShowMessage)
'El orden de estas debe ser inverso al orden de fCreateProjectViews
   vResult = vResult And insLibViews.fDropView(insControlDespachoVista.GetViewNameDetalleControlDespachoFactura(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insControlDespachoVista.GetViewNameControlDespachoFactura(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insPagoVista.GetViewNameCompraRetencionesPorConceptoISLR(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxPVista.GetViewNameRetencionImpuestoMunicipalCxP(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insclsSolicitudesDePagoVista.GetViewNameBeneficiarioSolicitudesDePago_B1(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBancoVista.GetViewNameMovimientosBancarios_B1(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBusinessintelligenceVistas.GetViewNameBIDimensionCotizacion_B1(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insFacturaVista.GetViewNameVentasConDescripcionArticulos(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insFacturaVista.GetViewNameVentasConVendedorCliente(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameInventarioConOSinReservas(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameInventarioReservas(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBusinessintelligenceVistas.GetViewNameBIDimensionVentas_B1(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameArticulosProductosCompuestos(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameProductosCompuestos_B1(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCobranzaVista.GetViewNameCobranzaUltimaFecha(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameArticulosMovimientoTransferencia(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameTransferenciasPorAlmacen(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameTransferenciasPorArticulo(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameArticuloInvMovimientoAlmacen(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insPagoVista.GetViewNameBODOperaciones(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insPagoVista.GetViewNameBODPago(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insPagoVista.GetViewNameBODCabecera(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insPagoVista.GetViewNameMercantilOperaciones(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insPagoVista.GetViewNameMercantilPago(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insPagoVista.GetViewNameMercantilCabecera(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insComprasVista.GetViewNameComprasConCuentasArticulos(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insComprasVista.GetViewNameComprasConOtroGastos(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insComprasVista.GetViewNameComprasConCambioOriginal(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insComprasVista.GetViewNameComprasConCambioAlDia(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insComprasVista.fSqlViewComprasImprimirEtiquetasPorCompras(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCambioVista.GetViewNameCambioAlaFecha(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCambioVista.GetViewNameCambioMaxFecha(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameComprasUltimaConOsinSeriales(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameComprasConOsinSeriales(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameComprasSecUltCom(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameCompraFechaUltimaCompra(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insPagoVista.GetViewNamePagosDeclracionMensualXMLSalariosOtros(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insPagoVista.GetViewNamePagosDeclracionMensualXMLDividendoYAcciones(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insPagoVista.GetViewNamePagosDeclracionMensualXMLGananciasFortuitas(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameArticuloInvMovimientoDetallado(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameArticuloInvMovimiento(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos, valShowMessage)
   vResult = vResult And insLibViews.fDropView(insComprasVista.GetViewNameComprasOtroGastosPorItem, valShowMessage)
   vResult = vResult And insLibViews.fDropView(insComprasVista.GetViewNameRenglonComprasConOSinSerial(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insFacturaVista.GetViewNameVendedorXRenglonFactura(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameInventarioMercanciaEnTransito(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameInventarioExistenciaXGrupo(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameUbicacionXAlmacenDeArticulo(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameArticuloXAlmacen_pIEB1, valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameInventarioExistenciaXGrupoMod_B1, valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1, valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameExistenciaPorGrupoConTallasyColores, valShowMessage)
   vResult = vResult And insLibViews.fDropView(insAnticipoVista.GetViewNameSaldoInicialAnticipoHistoricoCliente(), valShowMessage)
   
   
   vResult = vResult And insLibViews.fDropView(insAnticipoVista.GetViewNameAnticipoHistoricoCliente(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxCVista.GetViewNameCxCHistoricoClientes(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxCVista.GetViewNameInfoCxCHistoricoCliente(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxCVista.GetViewNameSaldoInicialCxCHistoricoCliente(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxCVista.GetViewNameVentas(), valShowMessage)
   
   vResult = vResult And insLibViews.fDropView(insCxCVista.GetViewNameBaseimponibleAlicuotasVentasResumenDeVentas(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxCVista.GetViewNameRetencionesEnVentas(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxCVista.GetViewNameVentasAgrupado(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxCVista.GetViewNameVentasDetallado(), valShowMessage)
   
   vResult = vResult And insLibViews.fDropView(insCxCVista.GetViewNameRetencionDocumentoCobrado(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insProveedor.GetViewNameProveedor(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxPVista.GetViewNameCompras(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxPVista.GetViewNameDocPagadosPorMoneda(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxCVista.GetViewNameDocCobradosPorMoneda(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insPDTVista.GetViewNamePDTVentasRentaMensual(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insPagoVista.GetViewNameLibroDeRetencionesRenta(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameRegistroInventarioPermanenteValorizado(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameRegistroInventarioPermanente(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameConteoFisicoDeArticulo(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameNotaEntradaSalidaDeArticulo(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameVentaDeArticulo(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameCompraDeArticulo(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1, valShowMessage)
   vResult = vResult And insLibViews.fDropView(insLibroVCVista.GetViewNameRegistroVentasPeru(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insLibroVCVista.GetViewNameUnionFacturaYCxc(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insLibroVCVista.GetViewNameFacturaAfectada(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insLibroVCVista.GetViewNameCxcConCliente(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insLibroVCVista.GetViewNameFacturaConCliente(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBancoVista.GetViewNameMovBCtaBGeneradaXUsuarioSinCheque(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBancoVista.GetViewNameMovBCtaBGeneradaXUsuario(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBancoVista.GetViewNameMovBCtaBGeneradaXRestantes(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBancoVista.GetViewNameMovBCtaBGeneradaXPago(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBancoVista.GetViewNameMovBCtaBGeneradaXDebitoBancario(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBancoVista.GetViewNameMovBCtaBGeneradaXCobranza(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBancoVista.GetViewNameMovBCtaBGeneradaXAnticipoPagado(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBancoVista.GetViewNameMovBCtaBGeneradaXAnticipoCobrado(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBancoVista.GetViewNameMovimientosXCuentaBancaria0695(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCobranzaVista.GetViewRetencionesDelPeriodo(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insFacturaVista.GetViewNameVentasGeneradasFactura(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxPVista.GetViewNameComprasGeneradasCxP(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCxCVista.GetViewNameVentasGeneradasCxC(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCobranzaVista.GetViewNameComisionDeVendedoresPorCobroDetallado(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCobranzaVista.GetViewNameMontoComisionXCobranza(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCobranzaVista.GetViewNameMontoNoComisibleFactura(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insClienteVista.GetViewNameFichaCliente(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insClienteVista.GetViewNameClientexFactura(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insBancoVista.GetViewNameUnionMovBCtaB(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insAnticipoVista.GetViewNameAnticiposCobradosAUnaFecha, valShowMessage)
   vResult = vResult And insLibViews.fDropView(insAnticipoVista.GetViewNameAnticiposPagadosAUnaFecha, valShowMessage)
   vResult = vResult And insLibViews.fDropView(insCobranzaVista.GetViewNameCobranzaPorZona, valShowMessage)
   vResult = vResult And insLibViews.fDropView(insFacturaVista.GetViewNameNotasDeCreditoEntreFechasPorVendedor, valShowMessage)
   vResult = vResult And insLibViews.fDropView(insFacturaVista.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito, valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameArticulosReservadosPorAlmacen(), valShowMessage)
   vResult = vResult And insLibViews.fDropView(insArticuloInvVista.GetViewNameArticulosReservadosPorCotizacion(), valShowMessage)
   
   vResult = vResult And insWcoViews.fDropProjectViews(False)
   vResult = vResult And insSawEnum.fDropProjectViews(False)
   If vResult And valShowMessage Then
      gMessage.ProgrammerMessage "Se Eliminaron con Exito las Vistas"
   End If
   fDropProjectViews = vResult
   Set insLibViews = Nothing
   Set insClienteVista = Nothing
   Set insCobranzaVista = Nothing
   Set insFacturaVista = Nothing
   Set insCxPVista = Nothing
   Set insCxCVista = Nothing
   Set insBancoVista = Nothing
   Set insMovBanSQL = Nothing
   Set insLibroVCVista = Nothing
   Set insArticuloInvVista = Nothing
   Set insPagoVista = Nothing
   Set insSawEnum = Nothing
   Set insPDTVista = Nothing
   Set insProveedor = Nothing
   Set insAnticipoVista = Nothing
   Set insComprasVista = Nothing
   Set insWcoViews = Nothing
   Set insCambioVista = Nothing
   Set insBusinessintelligenceVistas = Nothing
   Set insclsSolicitudesDePagoVista = Nothing
   Set insControlDespachoVista = Nothing
h_EXIT:  On Error GoTo 0
   vResult = False
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropProjectViews", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'Private Function GetViewNameX() As String 'en caso de ser una vista usada en un informe es que la funcion va a estar "public".
''   GetViewNameX = "IGV_nombre_de_la_vista_X"
'   GetViewNameX = "IGV_un_nombre_de_vista_demasiado_largo_para_poder_ser_creada_ya_que_sino_nos_generaría_error_en_nuestras_bases_de_datos"
'End Function
'
'Private Function fSqlViewX() As String
'   fSqlViewX = ""
'End Function

Private Sub Class_Initialize()

End Sub
