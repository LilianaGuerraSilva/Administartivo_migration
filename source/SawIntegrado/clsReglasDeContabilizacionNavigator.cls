VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsReglasDeContabilizacionNavigator"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private mrsReglasDeContabilizacion As ADODB.Recordset
Private mrsReglasDeContabilizacionQuery As ADODB.Recordset
Private mConsecutivoCompania As Long
Private mNumero As String
'Generales
Private mCuentaIva1Credito As String
Private mCuentaIva1Debito As String
Private mCuentaRetencionIVA As String
Private mDondeContabilizarRetIva As String
Private mDiferenciaEnCambioyCalculo As String
Private mCuentaDiferenciaCambiaria As String
'Impuestos a las Transacciones Bancarias
Private mCuentaDebitoBancarioGasto As String
Private mCuentaDebitoBancarioBancos As String
Private mCuentaCreditoBancarioGasto As String
Private mCuentaCreditoBancarioBancos As String
'Tipo de Comprobante
Private mCxCTipoComprobante As String
Private mCxPTipoComprobante As String
Private mCobranzaTipoComprobante As String
Private mPagoTipoComprobante As String
Private mFacturaTipoComprobante As String
Private mMovimientoBancarioTipoComprobante As String
Private mAnticipoTipoComprobante As String
Private mInventarioTipoComprobante As String
Private mPagosSueldosTipoComprobante As String
Private mSiglasTipoComprobanteCajaChica As String
Private mSiglasTipoComprobanteRendiciones As String
Private mTransfCtasSigasTipoComprobante As String
Private mOrdenDeProduccionTipoComprobante As String
'Tipo de Contabilización
Private mTipoContabilizacionCxC As String
Private mTipoContabilizacionCxP As String
Private mTipoContabilizacionCobranza As String
Private mTipoContabilizacionPagos As String
Private mTipoContabilizacionFacturacion As String
Private mTipoContabilizacionRDVtas As String
Private mTipoContabilizacionMovBancario As String
Private mTipoContabilizacionAnticipo As String
Private mTipoContabilizacionInventario As String
Private mTipoContabilizacionDePagosSueldos As String
Private mTipoContabilizacionCajaChica As String
Private mTipoContabilizacionRendicion As String
Private mTipoContabilizacionTransferenciaEntreCuenta As String
Private mTipoContabilizacionOrdenDeProduccion As String
'Editar Comprobante After Insert
Private mEditarComprobanteAfterInsertCxC As Boolean
Private mEditarComprobanteAfterInsertCxP As Boolean
Private mEditarComprobanteAfterInsertCobranza As Boolean
Private mEditarComprobanteAfterInsertPagos As Boolean
Private mEditarComprobanteAfterInsertFactura As Boolean
Private mEditarComprobanteAfterInsertResDia As Boolean
Private mEditarComprobanteAfterInsertMovBan As Boolean
Private mEditarComprobanteAfterInsertAnticipo As Boolean
Private mEditarComprobanteAfterInsertInventario As Boolean
Private mEditarComprobanteDePagosSueldos As Boolean
Private mEditarComprobanteAfterInsertCajaChica As Boolean
Private mEditarComprobanteAfterInsertTransfCtas As Boolean
Private mEditarComprobanteAfterInsertImpTraBan As Boolean
Private mEditarComprobanteAfterInsertOrdenDeProduccion As Boolean
'CxC
Private mContabIndividualCxc As String
Private mContabPorLoteCxC As String
Private mCuentaCxCClientes As String
Private mCuentaCxCIngresos As String
'CxP
Private mContabIndividualCxP As String
Private mContabPorLoteCxP As String
Private mCuentaCxPGasto As String
Private mCuentaCxPProveedores As String
Private mCuentaRetencionImpuestoMunicipal As String
'Cobranza
Private mContabIndividualCobranza As String
Private mContabPorLoteCobranza As String
Private mCuentaCobranzaCobradoEnEfectivo As String
Private mCuentaCobranzaCobradoEnCheque As String
Private mCuentaCobranzaCobradoEnTarjeta As String
Private mCuentaCobranzaRetencionISLR As String
Private mCuentaCobranzaRetencionIVA As String
Private mCuentaCobranzaOtros As String
Private mCuentaCobranzaCxCClientes As String
Private mCuentaCobranzaCobradoAnticipo As String
Private mCuentaCobranzaIvaDiferido As String
Private mManejarDiferenciaCambiariaEnCobranza As Boolean
'Pagos
Private mContabIndividualPagos As String
Private mContabPorLotePagos As String
Private mCuentaPagosCxPProveedores As String
Private mCuentaPagosRetencionISLR As String
Private mCuentaPagosOtros As String
Private mCuentaPagosBanco As String
Private mCuentaPagosPagadoAnticipo As String
Private mManejarDiferenciaCambiariaEnPagos As Boolean
'Factura
Private mContabIndividualFacturacion As String
Private mContabPorLoteFacturacion As String
Private mCuentaFacturacionCxCClientes As String
Private mCuentaFacturacionMontoTotalFactura As String
Private mCuentaFacturacionCargos As String
Private mCuentaFacturacionDescuentos As String
Private mCuentaFacturacionIvaDiferido As String
Private mContabilizarPorArticulo As Boolean
Private mAgruparPorCuentaDeArticulo As Boolean
Private mAgruparPorCargosDescuentos As Boolean
'Resumen Diario de Ventas
Private mContabIndividualRDVtas As String
Private mContabPorLoteRDVtas As String
Private mCuentaRDVtasCaja As String
Private mCuentaRDVtasMontoTotal As String
Private mContabilizarPorArticuloRDVtas As Boolean
Private mAgruparPorCuentaDeArticuloRDVtas As Boolean
Private mContabIndividualMovBancario As String
Private mContabPorLoteMovBancario As String
Private mCuentaMovBancarioGasto As String
Private mCuentaMovBancarioBancosHaber As String
Private mCuentaMovBancarioBancosDebe As String
Private mCuentaMovBancarioIngresos As String
'Anticipo
Private mContabIndividualAnticipo As String
Private mContabPorLoteAnticipo As String
Private mCuentaAnticipoCaja As String
Private mCuentaAnticipoCobrado As String
Private mCuentaAnticipoOtrosIngresos As String
Private mCuentaAnticipoPagado As String
Private mCuentaAnticipoBanco As String
Private mCuentaAnticipoOtrosEgresos As String
'Inventario
Private mCuentaCostoDeVenta As String
Private mCuentaInventario As String
Private mAgruparPorCuentaDeArticuloInven As Boolean
'Solicitud de Pago
Private mContabIndividualPagosSueldos As String
Private mCtaDePagosSueldos As String
Private mCtaDePagosSueldosBanco As String
'Caja Chica - Rendiciones
Private mContabIndividualCajaChica As String
Private mCuentaCajaChicaGasto As String
Private mCuentaCajaChicaBancoHaber As String
Private mCuentaCajaChicaBancoDebe As String
Private mCuentaCajaChicaBanco As String
Private mMostrarDesglosadoCajaChica As Boolean

Private mContabIndividualRendiciones As String
Private mCuentaRendicionesGasto As String
Private mCuentaRendicionesBanco As String
Private mCuentaRendicionesAnticipos As String
Private mMostrarDesglosadoRendiciones As Boolean
'Transferencia entre Cuentas Bancarias
Private mContabIndividualTransferenciaEntreCuenta As String
Private mContabPorLoteTransferenciaEntreCuenta As String
Private mCuentaTransfCtasBancoDestino As String
Private mCuentaTransfCtasGastoComOrigen As String
Private mCuentaTransfCtasGastoComDestino As String
Private mCuentaTransfCtasBancoOrigen As String
'Orden De Produccion
Private mContabIndividualOrdenDeProduccion As String
Private mContabPorLoteOrdenDeProduccion As String
Private mCuentaOrdenDeProduccionProductoTerminado As String
Private mCuentaOrdenDeProduccionMateriaPrima As String
Private mCuentaMermaAnormal As String
'Final
Private mNombreOperador As String
Private mFechaUltimaModificacion As Date
Private mSQLFromAndWhereStatementForList As String
'Usa Auxiliares
Private mCuentaIva1DebitoUsaAuxiliares As Boolean
Private mCuentaIva1CreditoUsaAuxiliares As Boolean
Private mCuentaRetencionIvaUsaAuxiliares As Boolean
Private mDiferenciaEnCambioyCalculoUsaAuxiliares As Boolean
Private mCuentaCxCClientesUsaAuxiliares As Boolean
Private mCuentaCxCIngresosUsaAuxiliares As Boolean
Private mCuentaCxPGastoUsaAuxiliares As Boolean
Private mCuentaCxPProveedoresUsaAuxiliares As Boolean
Private mCuentaCobranzaCobradoEnEfectivoUsaAuxiliares As Boolean
Private mCuentaCobranzaCobradoEnChequeUsaAuxiliares As Boolean
Private mCuentaCobranzaCobradoEnTarjetaUsaAuxiliares As Boolean
Private mCuentaCobranzaCobradoAnticipoUsaAuxiliares As Boolean
Private mCuentaCobranzaRetencionISLRUsaAuxiliares As Boolean
Private mCuentaCobranzaRetencionIVAUsaAuxiliares As Boolean
Private mCuentaCobranzaOtrosUsaAuxiliares As Boolean
Private mCuentaCobranzaCxCClientesUsaAuxiliares As Boolean
Private mCuentaPagosCxPProveedoresUsaAuxiliares As Boolean
Private mCuentaPagosRetencionISLRUsaAuxiliares As Boolean
Private mCuentaPagosOtrosUsaAuxiliares As Boolean
Private mCuentaPagosBancoUsaAuxiliares As Boolean
Private mCuentaFacturacionCxCClientesUsaAuxiliares As Boolean
Private mCuentaFacturacionMontoTotalFacturaUsaAuxiliares As Boolean
Private mCuentaFacturacionCargosUsaAuxiliares As Boolean
Private mCuentaFacturacionDescuentosUsaAuxiliares As Boolean
Private mCuentaMovBancarioGastoUsaAuxiliares As Boolean
Private mCuentaMovBancarioBancosHaberUsaAuxiliares As Boolean
Private mCuentaMovBancarioBancosDebeUsaAuxiliares As Boolean
Private mCuentaMovBancarioIngresosUsaAuxiliares As Boolean
Private mCuentaDebitoBancarioGastoUsaAuxiliares As Boolean
Private mCuentaDebitoBancarioBancosUsaAuxiliares As Boolean
Private mCuentaAnticipoCajaUsaAuxiliares As Boolean
Private mCuentaAnticipoCobradoUsaAuxiliares As Boolean
Private mCuentaAnticipoPagadoUsaAuxiliares As Boolean
Private mCuentaAnticipoBancoUsaAuxiliares As Boolean
Private mCuentaAnticipoOtrosIngresosUsaAuxiliares As Boolean
Private mCuentaAnticipoOtrosEgresosUsaAuxiliares As Boolean
Private mCuentaRetencionImpuestoMunicipalUsaAuxiliares   As Boolean
Private mCuentaCobranzaIvaDiferidoUsaAuxiliares As Boolean
Private mCuentaFacturacionIvaDiferidoUsaAuxiliares As Boolean
'

Private Const CM_FILE_NAME As String = "clsReglasDeContabilizacionNavigator"
Private Const CM_MESSAGE_NAME As String = "Reglas de Contabilización"

Public Sub sClrRecord()
   On Error GoTo h_ERROR
   SetConsecutivoCompania gProyCompaniaActual.GetConsecutivoCompania
   SetNumero ""
'General
   SetCuentaIva1Credito ""
   SetCuentaIva1Debito ""
   SetCuentaRetencionIVA ""
   SetDondeContabilizarRetIvaStr ""
   SetDiferenciaEnCambioyCalculo ""
   SetCuentaDiferenciaCambiaria ""
'Impuesto a las Transacciones Bancarias
   SetCuentaDebitoBancarioGasto ""
   SetCuentaDebitoBancarioBancos ""
   SetCuentaCreditoBancarioGasto ""
   SetCuentaCreditoBancarioBancos ""
'Tipo de Comprobante
   SetCxCTipoComprobante ""
   SetCxPTipoComprobante ""
   SetCobranzaTipoComprobante ""
   SetPagoTipoComprobante ""
   SetFacturaTipoComprobante ""
   SetMovimientoBancarioTipoComprobante ""
   SetAnticipoTipoComprobante ""
   SetInventarioTipoComprobante ""
   SetPagosSueldosTipoComprobante ""
   SetSiglasTipoComprobanteCajaChica ""
   SetSiglasTipoComprobanteRendiciones ""
   SetTransfCtasSigasTipoComprobante ""
   SetOrdenDeProduccionTipoComprobante ""
'Tipo de Contabilización
   SetTipoContabilizacionCxCStr ""
   SetTipoContabilizacionCxPStr ""
   SetTipoContabilizacionCobranzaStr ""
   SetTipoContabilizacionPagosStr ""
   SetTipoContabilizacionFacturacionStr ""
   SetTipoContabilizacionRDVtasStr ""
   SetTipoContabilizacionMovBancarioStr ""
   SetTipoContabilizacionAnticipoStr ""
   SetTipoContabilizacionInventarioStr ""
   SetTipoContabilizacionDePagosSueldosStr ""
   SetTipoContabilizacionCajaChicaStr ""
   SetTipoContabilizacionRendicionStr ""
   SetTipoContabilizacionTransferenciaEntreCuentaStr ""
   SetTipoContabilizacionOrdenDeProduccionStr ""
'Editar Comprobante After Insert
   SetEditarComprobanteAfterInsertCxC False
   SetEditarComprobanteAfterInsertCxP False
   SetEditarComprobanteAfterInsertCobranza False
   SetEditarComprobanteAfterInsertPagos False
   SetEditarComprobanteAfterInsertFactura False
   SetEditarComprobanteAfterInsertResDia False
   SetEditarComprobanteAfterInsertMovBan False
   SetEditarComprobanteAfterInsertAnticipo False
   SetEditarComprobanteAfterInsertInventario False
   SetEditarComprobanteDePagosSueldos False
   SetEditarComprobanteAfterInsertCajaChica False
   SetEditarComprobanteAfterInsertTransfCtas False
   SetEditarComprobanteAfterInsertImpTraBan False
   SetEditarComprobanteAfterInsertOrdenDeProduccion False
'CxC
   SetContabIndividualCxcStr ""
   SetContabPorLoteCxCStr ""
   SetCuentaCxCClientes ""
   SetCuentaCxCIngresos ""
'CxP
   SetContabIndividualCxPStr ""
   SetContabPorLoteCxPStr ""
   SetCuentaCxPGasto ""
   SetCuentaCxPProveedores ""
   SetCuentaRetencionImpuestoMunicipal ""
'Cobranza
   SetContabIndividualCobranzaStr ""
   SetContabPorLoteCobranzaStr ""
   SetCuentaCobranzaCobradoEnEfectivo ""
   SetCuentaCobranzaCobradoEnCheque ""
   SetCuentaCobranzaCobradoEnTarjeta ""
   SetCuentaCobranzaRetencionISLR ""
   SetCuentaCobranzaRetencionIVA ""
   SetCuentaCobranzaOtros ""
   SetCuentaCobranzaCxCClientes ""
   SetCuentaCobranzaCobradoAnticipo ""
   SetCuentaCobranzaIvaDiferido ""
   SetManejarDiferenciaCambiariaEnCobranza False
'Pagos
   SetContabIndividualPagosStr ""
   SetContabPorLotePagosStr ""
   SetCuentaPagosCxPProveedores ""
   SetCuentaPagosRetencionISLR ""
   SetCuentaPagosOtros ""
   SetCuentaPagosBanco ""
   SetCuentaPagosPagadoAnticipo ""
   SetManejarDiferenciaCambiariaEnPagos False
'Factura
   SetContabIndividualFacturacionStr ""
   SetContabPorLoteFacturacionStr ""
   SetCuentaFacturacionCxCClientes ""
   SetCuentaFacturacionMontoTotalFactura ""
   SetCuentaFacturacionCargos ""
   SetCuentaFacturacionDescuentos ""
   SetCuentaFacturacionIvaDiferido ""
   SetContabilizarPorArticulo False
   SetAgruparPorCuentaDeArticulo False
   SetAgruparPorCargosDescuentos False
'RDV
   SetContabIndividualRDVtasStr ""
   SetContabPorLoteRDVtasStr ""
   SetCuentaRDVtasCaja ""
   SetCuentaRDVtasMontoTotal ""
   SetContabilizarPorArticuloRDVtas False
   SetAgruparPorCuentaDeArticuloRDVtas False
'Movimiento Bancario
   SetContabIndividualMovBancarioStr ""
   SetContabPorLoteMovBancarioStr ""
   SetCuentaMovBancarioGasto ""
   SetCuentaMovBancarioBancosHaber ""
   SetCuentaMovBancarioBancosDebe ""
   SetCuentaMovBancarioIngresos ""
'Anticipo
   SetContabIndividualAnticipoStr ""
   SetContabPorLoteAnticipoStr ""
   SetCuentaAnticipoCaja ""
   SetCuentaAnticipoCobrado ""
   SetCuentaAnticipoOtrosIngresos ""
   SetCuentaAnticipoPagado ""
   SetCuentaAnticipoBanco ""
   SetCuentaAnticipoOtrosEgresos ""
'Inventario
   SetCuentaCostoDeVenta ""
   SetCuentaInventario ""
   SetAgruparPorCuentaDeArticuloInven False
'Solicitud de Pago
   SetContabIndividualPagosSueldosStr ""
   SetCtaDePagosSueldos ""
   SetCtaDePagosSueldosBanco ""
'Caja Chica - Rendiciones
   SetContabIndividualCajaChicaStr ""
   SetCuentaCajaChicaGasto ""
   SetCuentaCajaChicaBancoHaber ""
   SetCuentaCajaChicaBancoDebe ""
   SetCuentaCajaChicaBanco ""
   SetMostrarDesglosadoCajaChica False
   SetContabIndividualRendicionesStr ""
   SetCuentaRendicionesGasto ""
   SetCuentaRendicionesBanco ""
   SetCuentaRendicionesAnticipos ""
   SetMostrarDesglosadoRendiciones False
'Transferencia entre Cuentas Bancarias
   SetContabIndividualTransferenciaEntreCuentaStr ""
   SetContabPorLoteTransferenciaEntreCuentaStr ""
   SetCuentaTransfCtasBancoDestino ""
   SetCuentaTransfCtasGastoComOrigen ""
   SetCuentaTransfCtasGastoComDestino ""
   SetCuentaTransfCtasBancoOrigen ""
'Orden de Producción
   SetContabIndividualOrdenDeProduccionStr ""
   SetContabPorLoteOrdenDeProduccionStr ""
   SetCuentaOrdenDeProduccionProductoTerminado ""
   SetCuentaOrdenDeProduccionMateriaPrima ""
   SetCuentaMermaAnormal ""
'Final
   SetNombreOperador gProyUsuarioActual.GetNombreDelUsuario
   SetFechaUltimaModificacion gUtilDate.getFechaDeHoy
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sClrRecord", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

'
Private Sub SetCuentaIva1DebitoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaIva1DebitoUsaAuxiliares = valNewValue
End Sub

Private Sub SetCuentaIva1CreditoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaIva1CreditoUsaAuxiliares = valNewValue
End Sub

Private Sub SetCuentaRetencionIvaUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaRetencionIvaUsaAuxiliares = valNewValue
End Sub

Private Sub SetCuentaCxCClientesUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCxCClientesUsaAuxiliares = valNewValue
End Sub

Private Sub SetCuentaCxCIngresosUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCxCIngresosUsaAuxiliares = valNewValue
End Sub

Private Sub SetCuentaCxPGastoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCxPGastoUsaAuxiliares = valNewValue
End Sub

Private Sub SetCuentaCxPProveedoresUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCxPProveedoresUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaCobranzaCobradoEnEfectivoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCobranzaCobradoEnEfectivoUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaCobranzaCobradoEnChequeUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCobranzaCobradoEnChequeUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaCobranzaCobradoEnTarjetaUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCobranzaCobradoEnTarjetaUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaCobranzaCobradoAnticipoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCobranzaCobradoAnticipoUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaCobranzaRetencionISLRUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCobranzaRetencionISLRUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaCobranzaRetencionIVAUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCobranzaRetencionIVAUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaCobranzaOtrosUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCobranzaOtrosUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaCobranzaCxCClientesUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCobranzaCxCClientesUsaAuxiliares = valNewValue
End Sub

Private Sub SetCuentaCobranzaIvaDiferidoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaCobranzaIvaDiferidoUsaAuxiliares = valNewValue
End Sub

Private Sub SetCuentaFacturacionIvaDiferidoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaFacturacionIvaDiferidoUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaPagosCxPProveedoresUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaPagosCxPProveedoresUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaPagosRetencionISLRUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaPagosRetencionISLRUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaPagosOtrosUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaPagosOtrosUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaPagosBancoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaPagosBancoUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaFacturacionCxCClientesUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaFacturacionCxCClientesUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaFacturacionMontoTotalFacturaUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaFacturacionMontoTotalFacturaUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaFacturacionCargosUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaFacturacionCargosUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaFacturacionDescuentosUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaFacturacionDescuentosUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaMovBancarioGastoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaMovBancarioGastoUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaMovBancarioBancosHaberUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaMovBancarioBancosHaberUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaMovBancarioBancosDebeUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaMovBancarioBancosDebeUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaMovBancarioIngresosUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaMovBancarioIngresosUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaDebitoBancarioGastoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaDebitoBancarioGastoUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaDebitoBancarioBancosUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaDebitoBancarioBancosUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaAnticipoCajaUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaAnticipoCajaUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaAnticipoCobradoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaAnticipoCobradoUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaAnticipoPagadoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaAnticipoPagadoUsaAuxiliares = valNewValue
End Sub

Private Sub setCuentaAnticipoBancoUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaAnticipoBancoUsaAuxiliares = valNewValue
End Sub

Private Sub SetCuentaAnticipoOtrosIngresosUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaAnticipoOtrosIngresosUsaAuxiliares = valNewValue
End Sub

Private Sub SetCuentaRetencionImpuestoMunicipalUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaRetencionImpuestoMunicipalUsaAuxiliares = valNewValue
End Sub

Private Sub SetCuentaAnticipoOtrosEgresosUsaAuxiliares(ByVal valNewValue As Boolean)
   mCuentaAnticipoOtrosEgresosUsaAuxiliares = valNewValue
End Sub

'get
Public Function GetCuentaIva1DebitoUsaAuxiliares() As Boolean
   GetCuentaIva1DebitoUsaAuxiliares = mCuentaIva1DebitoUsaAuxiliares
End Function

Public Function GetCuentaIva1CreditoUsaAuxiliares() As Boolean
   GetCuentaIva1CreditoUsaAuxiliares = mCuentaIva1CreditoUsaAuxiliares
End Function

Public Function GetCuentaRetencionIvaUsaAuxiliares() As Boolean
   GetCuentaRetencionIvaUsaAuxiliares = mCuentaRetencionIvaUsaAuxiliares
End Function

Private Function GetCuentaCxCClientesUsaAuxiliares() As Boolean
   GetCuentaCxCClientesUsaAuxiliares = mCuentaCxCClientesUsaAuxiliares
End Function

Private Function GetCuentaCxCIngresosUsaAuxiliares() As Boolean
   GetCuentaCxCIngresosUsaAuxiliares = mCuentaCxCIngresosUsaAuxiliares
End Function

Private Function GetCuentaCxPGastoUsaAuxiliares() As Boolean
   GetCuentaCxPGastoUsaAuxiliares = mCuentaCxPGastoUsaAuxiliares
End Function

Private Function GetCuentaCxPProveedoresUsaAuxiliares() As Boolean
   GetCuentaCxPProveedoresUsaAuxiliares = mCuentaCxPProveedoresUsaAuxiliares
End Function

Public Function getCuentaCobranzaCobradoEnEfectivoUsaAuxiliares() As Boolean
   getCuentaCobranzaCobradoEnEfectivoUsaAuxiliares = mCuentaCobranzaCobradoEnEfectivoUsaAuxiliares
End Function

Public Function getCuentaCobranzaCobradoEnChequeUsaAuxiliares() As Boolean
   getCuentaCobranzaCobradoEnChequeUsaAuxiliares = mCuentaCobranzaCobradoEnChequeUsaAuxiliares
End Function

Public Function getCuentaCobranzaCobradoEnTarjetaUsaAuxiliares() As Boolean
   getCuentaCobranzaCobradoEnTarjetaUsaAuxiliares = mCuentaCobranzaCobradoEnTarjetaUsaAuxiliares
End Function

Public Function getCuentaCobranzaRetencionISLRUsaAuxiliares() As Boolean
   getCuentaCobranzaRetencionISLRUsaAuxiliares = mCuentaCobranzaRetencionISLRUsaAuxiliares
End Function

Public Function getCuentaCobranzaRetencionIVAUsaAuxiliares() As Boolean
   getCuentaCobranzaRetencionIVAUsaAuxiliares = mCuentaCobranzaRetencionIVAUsaAuxiliares
End Function

Public Function getCuentaCobranzaOtrosUsaAuxiliares() As Boolean
   getCuentaCobranzaOtrosUsaAuxiliares = mCuentaCobranzaOtrosUsaAuxiliares
End Function

Private Function getCuentaCobranzaCxCClientesUsaAuxiliares() As Boolean
   getCuentaCobranzaCxCClientesUsaAuxiliares = mCuentaCobranzaCxCClientesUsaAuxiliares
End Function

Private Function getCuentaPagosCxPProveedoresUsaAuxiliares() As Boolean
   getCuentaPagosCxPProveedoresUsaAuxiliares = mCuentaPagosCxPProveedoresUsaAuxiliares
End Function

Public Function getCuentaPagosRetencionISLRUsaAuxiliares() As Boolean
   getCuentaPagosRetencionISLRUsaAuxiliares = mCuentaPagosRetencionISLRUsaAuxiliares
End Function

Public Function getCuentaPagosOtrosUsaAuxiliares() As Boolean
   getCuentaPagosOtrosUsaAuxiliares = mCuentaPagosOtrosUsaAuxiliares
End Function

Private Function getCuentaFacturacionCxCClientesUsaAuxiliares() As Boolean
   getCuentaFacturacionCxCClientesUsaAuxiliares = mCuentaFacturacionCxCClientesUsaAuxiliares
End Function

Public Function getCuentaFacturacionMontoTotalFacturaUsaAuxiliares() As Boolean
   getCuentaFacturacionMontoTotalFacturaUsaAuxiliares = mCuentaFacturacionMontoTotalFacturaUsaAuxiliares
End Function

Private Function getCuentaAnticipoCobradoUsaAuxiliares() As Boolean
   getCuentaAnticipoCobradoUsaAuxiliares = mCuentaAnticipoCobradoUsaAuxiliares
End Function

Private Function getCuentaAnticipoPagadoUsaAuxiliares() As Boolean
   getCuentaAnticipoPagadoUsaAuxiliares = mCuentaAnticipoPagadoUsaAuxiliares
End Function

Public Function getCuentaRetencionImpuestoMunicipalUsaAuxiliares() As Boolean
   getCuentaRetencionImpuestoMunicipalUsaAuxiliares = mCuentaRetencionImpuestoMunicipalUsaAuxiliares
End Function

Public Function getCuentaCobranzaIvaDiferidoUsaAuxiliares() As Boolean
   getCuentaCobranzaIvaDiferidoUsaAuxiliares = mCuentaCobranzaIvaDiferidoUsaAuxiliares
End Function

Public Function getCuentaFacturacionIvaDiferidoUsaAuxiliares() As Boolean
   getCuentaFacturacionIvaDiferidoUsaAuxiliares = mCuentaFacturacionIvaDiferidoUsaAuxiliares
End Function

Public Function eTC_ReglaProvCxP() As Integer
   eTC_ReglaProvCxP = 0
End Function

Public Function eTC_ReglaProvGasto() As Integer
   eTC_ReglaProvGasto = 1
End Function
   
Public Function eTC_ReglaCliCxC() As Integer
   eTC_ReglaCliCxC = 2
End Function
   
Public Function eTC_ReglaCliIngreso() As Integer
   eTC_ReglaCliIngreso = 3
End Function

Public Function eTC_ReglaAntCobCliente() As Integer
   eTC_ReglaAntCobCliente = 4
End Function

Public Function eTC_ReglaAntPagProveedor() As Integer
   eTC_ReglaAntPagProveedor = 5
End Function
'
Public Function GetConsecutivoCompania() As Long
   GetConsecutivoCompania = mConsecutivoCompania
End Function

Private Sub SetConsecutivoCompania(ByVal valNewValue As Long)
   mConsecutivoCompania = valNewValue
End Sub

Public Function GetNumero() As String
   GetNumero = mNumero
End Function

Private Sub SetNumero(ByVal valNewValue As String)
   mNumero = gTexto.DfMid(valNewValue, 1, 11)
End Sub

'General
Public Function GetCuentaIva1Credito() As String
   GetCuentaIva1Credito = mCuentaIva1Credito
End Function

Private Sub SetCuentaIva1Credito(ByVal valNewValue As String)
   mCuentaIva1Credito = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaIva1Debito() As String
   GetCuentaIva1Debito = mCuentaIva1Debito
End Function

Private Sub SetCuentaIva1Debito(ByVal valNewValue As String)
   mCuentaIva1Debito = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaRetencionIVA() As String
   GetCuentaRetencionIVA = mCuentaRetencionIVA
End Function

Private Sub SetCuentaRetencionIVA(ByVal valNewValue As String)
   mCuentaRetencionIVA = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetDondeContabilizarRetIvaAsEnum() As enum_DondeEfectuoContabilizacionRetIVA
   GetDondeContabilizarRetIvaAsEnum = gEnumProyecto.strDondeEfectuoContabilizacionRetIVAToNum(mDondeContabilizarRetIva, False)
End Function

Private Sub SetDondeContabilizarRetIvaStr(ByVal valNewValue As String)
   mDondeContabilizarRetIva = valNewValue
End Sub

Public Function GetDiferenciaEnCambioyCalculo() As String
   GetDiferenciaEnCambioyCalculo = mDiferenciaEnCambioyCalculo
End Function

Private Sub SetDiferenciaEnCambioyCalculo(ByVal valNewValue As String)
   mDiferenciaEnCambioyCalculo = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaDiferenciaCambiaria() As String
   GetCuentaDiferenciaCambiaria = mCuentaDiferenciaCambiaria
End Function

Private Sub SetCuentaDiferenciaCambiaria(ByVal valNewValue As String)
   mCuentaDiferenciaCambiaria = gTexto.DfMid(valNewValue, 1, 30)
End Sub

'Impuestos a las Transacciones Bancarias
Public Function GetCuentaDebitoBancarioGasto() As String
   GetCuentaDebitoBancarioGasto = mCuentaDebitoBancarioGasto
End Function

Private Sub SetCuentaDebitoBancarioGasto(ByVal valNewValue As String)
   mCuentaDebitoBancarioGasto = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaDebitoBancarioBancos() As String
   GetCuentaDebitoBancarioBancos = mCuentaDebitoBancarioBancos
End Function

Private Sub SetCuentaDebitoBancarioBancos(ByVal valNewValue As String)
   mCuentaDebitoBancarioBancos = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCreditoBancarioGasto() As String
   GetCuentaCreditoBancarioGasto = mCuentaCreditoBancarioGasto
End Function

Private Sub SetCuentaCreditoBancarioGasto(ByVal valNewValue As String)
   mCuentaCreditoBancarioGasto = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCreditoBancarioBancos() As String
   GetCuentaCreditoBancarioBancos = mCuentaCreditoBancarioBancos
End Function

Private Sub SetCuentaCreditoBancarioBancos(ByVal valNewValue As String)
   mCuentaCreditoBancarioBancos = gTexto.DfMid(valNewValue, 1, 30)
End Sub

'Tipo de Comprobante
Public Function GetCxCTipoComprobante() As String
   GetCxCTipoComprobante = mCxCTipoComprobante
End Function

Private Sub SetCxCTipoComprobante(ByVal valNewValue As String)
   mCxCTipoComprobante = gTexto.DfMid(valNewValue, 1, 2)
End Sub

Public Function GetCxPTipoComprobante() As String
   GetCxPTipoComprobante = mCxPTipoComprobante
End Function

Private Sub SetCxPTipoComprobante(ByVal valNewValue As String)
   mCxPTipoComprobante = gTexto.DfMid(valNewValue, 1, 2)
End Sub

Public Function GetCobranzaTipoComprobante() As String
   GetCobranzaTipoComprobante = mCobranzaTipoComprobante
End Function

Private Sub SetCobranzaTipoComprobante(ByVal valNewValue As String)
   mCobranzaTipoComprobante = gTexto.DfMid(valNewValue, 1, 2)
End Sub

Public Function GetPagoTipoComprobante() As String
   GetPagoTipoComprobante = mPagoTipoComprobante
End Function

Private Sub SetPagoTipoComprobante(ByVal valNewValue As String)
   mPagoTipoComprobante = gTexto.DfMid(valNewValue, 1, 2)
End Sub

Public Function GetFacturaTipoComprobante() As String
   GetFacturaTipoComprobante = mFacturaTipoComprobante
End Function

Private Sub SetFacturaTipoComprobante(ByVal valNewValue As String)
   mFacturaTipoComprobante = gTexto.DfMid(valNewValue, 1, 2)
End Sub

Public Function GetMovimientoBancarioTipoComprobante() As String
   GetMovimientoBancarioTipoComprobante = mMovimientoBancarioTipoComprobante
End Function

Private Sub SetMovimientoBancarioTipoComprobante(ByVal valNewValue As String)
   mMovimientoBancarioTipoComprobante = gTexto.DfMid(valNewValue, 1, 2)
End Sub

Public Function GetAnticipoTipoComprobante() As String
   GetAnticipoTipoComprobante = mAnticipoTipoComprobante
End Function

Private Sub SetAnticipoTipoComprobante(ByVal valNewValue As String)
   mAnticipoTipoComprobante = gTexto.DfMid(valNewValue, 1, 2)
End Sub

Public Function GetInventarioTipoComprobante() As String
   GetInventarioTipoComprobante = mInventarioTipoComprobante
End Function

Private Sub SetInventarioTipoComprobante(ByVal valNewValue As String)
   mInventarioTipoComprobante = gTexto.DfMid(valNewValue, 1, 2)
End Sub

Public Function GetPagosSueldosTipoComprobante() As String
   GetPagosSueldosTipoComprobante = mPagosSueldosTipoComprobante
End Function

Private Sub SetPagosSueldosTipoComprobante(ByVal valNewValue As String)
   mPagosSueldosTipoComprobante = gTexto.DfMid(valNewValue, 1, 2)
End Sub

Public Function GetSiglasTipoComprobanteCajaChica() As String
   GetSiglasTipoComprobanteCajaChica = mSiglasTipoComprobanteCajaChica
End Function

Private Sub SetSiglasTipoComprobanteCajaChica(ByVal valNewValue As String)
   mSiglasTipoComprobanteCajaChica = gTexto.DfMid(valNewValue, 1, 2)
End Sub

Public Function GetSiglasTipoComprobanteRendiciones() As String
   GetSiglasTipoComprobanteRendiciones = mSiglasTipoComprobanteRendiciones
End Function

Private Sub SetSiglasTipoComprobanteRendiciones(ByVal valNewValue As String)
   mSiglasTipoComprobanteRendiciones = gTexto.DfMid(valNewValue, 1, 2)
End Sub

Public Function GetTransfCtasSigasTipoComprobante() As String
   GetTransfCtasSigasTipoComprobante = mTransfCtasSigasTipoComprobante
End Function

Private Sub SetTransfCtasSigasTipoComprobante(ByVal valNewValue As String)
   mTransfCtasSigasTipoComprobante = gTexto.DfMid(valNewValue, 1, 2)
End Sub

Public Function GetOrdenDeProduccionTipoComprobante() As String
   GetOrdenDeProduccionTipoComprobante = mOrdenDeProduccionTipoComprobante
End Function

Private Sub SetOrdenDeProduccionTipoComprobante(ByVal valNewValue As String)
   mOrdenDeProduccionTipoComprobante = gTexto.DfMid(valNewValue, 1, 2)
End Sub

'Tipo de Contabilización
Public Function GetTipoContabilizacionCxCAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionCxCAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionCxC, False)
End Function

Private Sub SetTipoContabilizacionCxCStr(ByVal valNewValue As String)
   mTipoContabilizacionCxC = valNewValue
End Sub

Public Function GetTipoContabilizacionCxPAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionCxPAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionCxP, False)
End Function

Private Sub SetTipoContabilizacionCxPStr(ByVal valNewValue As String)
   mTipoContabilizacionCxP = valNewValue
End Sub

Public Function GetTipoContabilizacionCobranzaAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionCobranzaAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionCobranza, False)
End Function

Private Sub SetTipoContabilizacionCobranzaStr(ByVal valNewValue As String)
   mTipoContabilizacionCobranza = valNewValue
End Sub

Public Function GetTipoContabilizacionPagosAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionPagosAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionPagos, False)
End Function

Private Sub SetTipoContabilizacionPagosStr(ByVal valNewValue As String)
   mTipoContabilizacionPagos = valNewValue
End Sub

Public Function GetTipoContabilizacionFacturacionAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionFacturacionAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionFacturacion, False)
End Function

Private Sub SetTipoContabilizacionFacturacionStr(ByVal valNewValue As String)
   mTipoContabilizacionFacturacion = valNewValue
End Sub

Public Function GetTipoContabilizacionRDVtasAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionRDVtasAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionRDVtas, False)
End Function

Private Sub SetTipoContabilizacionRDVtasStr(ByVal valNewValue As String)
   mTipoContabilizacionRDVtas = valNewValue
End Sub

Public Function GetTipoContabilizacionMovBancarioAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionMovBancarioAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionMovBancario, False)
End Function

Private Sub SetTipoContabilizacionMovBancarioStr(ByVal valNewValue As String)
   mTipoContabilizacionMovBancario = valNewValue
End Sub

Public Function GetTipoContabilizacionAnticipoAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionAnticipoAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionAnticipo, False)
End Function

Private Sub SetTipoContabilizacionAnticipoStr(ByVal valNewValue As String)
   mTipoContabilizacionAnticipo = valNewValue
End Sub

Public Function GetTipoContabilizacionInventarioAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionInventarioAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionInventario, False)
End Function

Private Sub SetTipoContabilizacionInventarioStr(ByVal valNewValue As String)
   mTipoContabilizacionInventario = valNewValue
End Sub

Public Function GetTipoContabilizacionDePagosSueldosAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionDePagosSueldosAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionDePagosSueldos, False)
End Function

Private Sub SetTipoContabilizacionDePagosSueldosStr(ByVal valNewValue As String)
   mTipoContabilizacionDePagosSueldos = valNewValue
End Sub

Public Function GetTipoContabilizacionCajaChicaAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionCajaChicaAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionCajaChica, False)
End Function

Private Sub SetTipoContabilizacionCajaChicaStr(ByVal valNewValue As String)
   mTipoContabilizacionCajaChica = valNewValue
End Sub

Public Function GetTipoContabilizacionRendicionAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionRendicionAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionRendicion, False)
End Function

Private Sub SetTipoContabilizacionRendicionStr(ByVal valNewValue As String)
   mTipoContabilizacionRendicion = valNewValue
End Sub

Public Function GetTipoContabilizacionTransferenciaEntreCuentaAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionTransferenciaEntreCuentaAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionTransferenciaEntreCuenta, False)
End Function

Private Sub SetTipoContabilizacionTransferenciaEntreCuentaStr(ByVal valNewValue As String)
   mTipoContabilizacionTransferenciaEntreCuenta = valNewValue
End Sub

Public Function GetTipoContabilizacionOrdenDeProduccionAsEnum() As enum_TipoDeContabilizacion
   GetTipoContabilizacionOrdenDeProduccionAsEnum = gEnumProyecto.strTipoDeContabilizacionToNum(mTipoContabilizacionOrdenDeProduccion, False)
End Function

Private Sub SetTipoContabilizacionOrdenDeProduccionStr(ByVal valNewValue As String)
   mTipoContabilizacionOrdenDeProduccion = valNewValue
End Sub

'Editar Comprobante After Insert
Public Function GetEditarComprobanteAfterInsertCxC() As Boolean
   GetEditarComprobanteAfterInsertCxC = mEditarComprobanteAfterInsertCxC
End Function

Public Sub SetEditarComprobanteAfterInsertCxC(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertCxC = valNewValue
End Sub

Public Function GetEditarComprobanteAfterInsertCxP() As Boolean
   GetEditarComprobanteAfterInsertCxP = mEditarComprobanteAfterInsertCxP
End Function

Public Sub SetEditarComprobanteAfterInsertCxP(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertCxP = valNewValue
End Sub

Public Function GetEditarComprobanteAfterInsertCobranza() As Boolean
   GetEditarComprobanteAfterInsertCobranza = mEditarComprobanteAfterInsertCobranza
End Function

Public Sub SetEditarComprobanteAfterInsertCobranza(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertCobranza = valNewValue
End Sub

Public Function GetEditarComprobanteAfterInsertPagos() As Boolean
   GetEditarComprobanteAfterInsertPagos = mEditarComprobanteAfterInsertPagos
End Function

Public Sub SetEditarComprobanteAfterInsertPagos(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertPagos = valNewValue
End Sub

Public Function GetEditarComprobanteAfterInsertFactura() As Boolean
   GetEditarComprobanteAfterInsertFactura = mEditarComprobanteAfterInsertFactura
End Function

Public Sub SetEditarComprobanteAfterInsertFactura(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertFactura = valNewValue
End Sub

Public Function GetEditarComprobanteAfterInsertResDia() As Boolean
   GetEditarComprobanteAfterInsertResDia = mEditarComprobanteAfterInsertResDia
End Function

Public Sub SetEditarComprobanteAfterInsertResDia(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertResDia = valNewValue
End Sub

Public Function GetEditarComprobanteAfterInsertMovBan() As Boolean
   GetEditarComprobanteAfterInsertMovBan = mEditarComprobanteAfterInsertMovBan
End Function

Public Sub SetEditarComprobanteAfterInsertMovBan(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertMovBan = valNewValue
End Sub

Public Function GetEditarComprobanteAfterInsertAnticipo() As Boolean
   GetEditarComprobanteAfterInsertAnticipo = mEditarComprobanteAfterInsertAnticipo
End Function

Public Sub SetEditarComprobanteAfterInsertAnticipo(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertAnticipo = valNewValue
End Sub

Public Function GetEditarComprobanteAfterInsertInventario() As Boolean
   GetEditarComprobanteAfterInsertInventario = mEditarComprobanteAfterInsertInventario
End Function

Public Sub SetEditarComprobanteAfterInsertInventario(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertInventario = valNewValue
End Sub

Public Function GetEditarComprobanteDePagosSueldos() As Boolean
   GetEditarComprobanteDePagosSueldos = mEditarComprobanteDePagosSueldos
End Function

Public Sub SetEditarComprobanteDePagosSueldos(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteDePagosSueldos = valNewValue
End Sub

Public Function GetEditarComprobanteAfterInsertCajaChica() As Boolean
   GetEditarComprobanteAfterInsertCajaChica = mEditarComprobanteAfterInsertCajaChica
End Function

Public Sub SetEditarComprobanteAfterInsertCajaChica(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertCajaChica = valNewValue
End Sub

Public Function GetEditarComprobanteAfterInsertTransfCtas() As Boolean
   GetEditarComprobanteAfterInsertTransfCtas = mEditarComprobanteAfterInsertTransfCtas
End Function

Public Sub SetEditarComprobanteAfterInsertTransfCtas(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertTransfCtas = valNewValue
End Sub

Public Function GetEditarComprobanteAfterInsertImpTraBan() As Boolean
   GetEditarComprobanteAfterInsertImpTraBan = mEditarComprobanteAfterInsertImpTraBan
End Function

Public Sub SetEditarComprobanteAfterInsertImpTraBan(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertImpTraBan = valNewValue
End Sub

Public Function GetEditarComprobanteAfterInsertOrdenDeProduccion() As Boolean
   GetEditarComprobanteAfterInsertOrdenDeProduccion = mEditarComprobanteAfterInsertOrdenDeProduccion
End Function

Public Sub SetEditarComprobanteAfterInsertOrdenDeProduccion(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mEditarComprobanteAfterInsertOrdenDeProduccion = valNewValue
End Sub

'CxC
Public Function GetContabIndividualCxcAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualCxcAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualCxc, False)
End Function

Private Sub SetContabIndividualCxcStr(ByVal valNewValue As String)
   mContabIndividualCxc = valNewValue
End Sub

Public Function GetContabPorLoteCxCAsEnum() As enum_ContabilizacionPorLote
   GetContabPorLoteCxCAsEnum = gEnumProyecto.strContabilizacionPorLoteToNum(mContabPorLoteCxC, False)
End Function

Private Sub SetContabPorLoteCxCStr(ByVal valNewValue As String)
   mContabPorLoteCxC = valNewValue
End Sub

Public Function GetCuentaCxCClientes() As String
   GetCuentaCxCClientes = mCuentaCxCClientes
End Function

Private Sub SetCuentaCxCClientes(ByVal valNewValue As String)
   mCuentaCxCClientes = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCxCIngresos() As String
   GetCuentaCxCIngresos = mCuentaCxCIngresos
End Function

Private Sub SetCuentaCxCIngresos(ByVal valNewValue As String)
   mCuentaCxCIngresos = gTexto.DfMid(valNewValue, 1, 30)
End Sub

'CxP
Public Function GetContabIndividualCxPAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualCxPAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualCxP, False)
End Function

Private Sub SetContabIndividualCxPStr(ByVal valNewValue As String)
   mContabIndividualCxP = valNewValue
End Sub

Public Function GetContabPorLoteCxPAsEnum() As enum_ContabilizacionPorLote
   GetContabPorLoteCxPAsEnum = gEnumProyecto.strContabilizacionPorLoteToNum(mContabPorLoteCxP, False)
End Function

Private Sub SetContabPorLoteCxPStr(ByVal valNewValue As String)
   mContabPorLoteCxP = valNewValue
End Sub

Public Function GetCuentaCxPGasto() As String
   GetCuentaCxPGasto = mCuentaCxPGasto
End Function

Private Sub SetCuentaCxPGasto(ByVal valNewValue As String)
   mCuentaCxPGasto = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCxPProveedores() As String
   GetCuentaCxPProveedores = mCuentaCxPProveedores
End Function

Private Sub SetCuentaCxPProveedores(ByVal valNewValue As String)
   mCuentaCxPProveedores = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaRetencionImpuestoMunicipal() As String
   GetCuentaRetencionImpuestoMunicipal = mCuentaRetencionImpuestoMunicipal
End Function

Private Sub SetCuentaRetencionImpuestoMunicipal(ByVal valNewValue As String)
   mCuentaRetencionImpuestoMunicipal = gTexto.DfMid(valNewValue, 1, 30)
End Sub

'Cobranza
Public Function GetContabIndividualCobranzaAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualCobranzaAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualCobranza, False)
End Function

Private Sub SetContabIndividualCobranzaStr(ByVal valNewValue As String)
   mContabIndividualCobranza = valNewValue
End Sub

Public Function GetContabPorLoteCobranzaAsEnum() As enum_ContabilizacionPorLote
   GetContabPorLoteCobranzaAsEnum = gEnumProyecto.strContabilizacionPorLoteToNum(mContabPorLoteCobranza, False)
End Function

Private Sub SetContabPorLoteCobranzaStr(ByVal valNewValue As String)
   mContabPorLoteCobranza = valNewValue
End Sub

Public Function GetCuentaCobranzaCobradoEnEfectivo() As String
   GetCuentaCobranzaCobradoEnEfectivo = mCuentaCobranzaCobradoEnEfectivo
End Function

Private Sub SetCuentaCobranzaCobradoEnEfectivo(ByVal valNewValue As String)
   mCuentaCobranzaCobradoEnEfectivo = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCobranzaCobradoEnCheque() As String
   GetCuentaCobranzaCobradoEnCheque = mCuentaCobranzaCobradoEnCheque
End Function

Private Sub SetCuentaCobranzaCobradoEnCheque(ByVal valNewValue As String)
   mCuentaCobranzaCobradoEnCheque = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCobranzaCobradoEnTarjeta() As String
   GetCuentaCobranzaCobradoEnTarjeta = mCuentaCobranzaCobradoEnTarjeta
End Function

Private Sub SetCuentaCobranzaCobradoEnTarjeta(ByVal valNewValue As String)
   mCuentaCobranzaCobradoEnTarjeta = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCobranzaRetencionISLR() As String
   GetCuentaCobranzaRetencionISLR = mCuentaCobranzaRetencionISLR
End Function

Private Sub SetCuentaCobranzaRetencionISLR(ByVal valNewValue As String)
   mCuentaCobranzaRetencionISLR = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCobranzaRetencionIVA() As String
   GetCuentaCobranzaRetencionIVA = mCuentaCobranzaRetencionIVA
End Function

Private Sub SetCuentaCobranzaRetencionIVA(ByVal valNewValue As String)
   mCuentaCobranzaRetencionIVA = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCobranzaOtros() As String
   GetCuentaCobranzaOtros = mCuentaCobranzaOtros
End Function

Private Sub SetCuentaCobranzaOtros(ByVal valNewValue As String)
   mCuentaCobranzaOtros = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCobranzaCxCClientes() As String
   GetCuentaCobranzaCxCClientes = mCuentaCobranzaCxCClientes
End Function

Private Sub SetCuentaCobranzaCxCClientes(ByVal valNewValue As String)
   mCuentaCobranzaCxCClientes = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCobranzaCobradoAnticipo() As String
   GetCuentaCobranzaCobradoAnticipo = mCuentaCobranzaCobradoAnticipo
End Function

Private Sub SetCuentaCobranzaCobradoAnticipo(ByVal valNewValue As String)
   mCuentaCobranzaCobradoAnticipo = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function getCuentaCobranzaIvaDiferido() As String
   getCuentaCobranzaIvaDiferido = mCuentaCobranzaIvaDiferido
End Function

Private Sub SetCuentaCobranzaIvaDiferido(ByVal valNewValue As String)
   mCuentaCobranzaIvaDiferido = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetManejarDiferenciaCambiariaEnCobranza() As Boolean
   GetManejarDiferenciaCambiariaEnCobranza = mManejarDiferenciaCambiariaEnCobranza
End Function

Private Sub SetManejarDiferenciaCambiariaEnCobranza(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mManejarDiferenciaCambiariaEnCobranza = valNewValue
End Sub

'Pagos
Public Function GetContabIndividualPagosAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualPagosAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualPagos, False)
End Function

Private Sub SetContabIndividualPagosStr(ByVal valNewValue As String)
   mContabIndividualPagos = valNewValue
End Sub

Public Function GetContabPorLotePagosAsEnum() As enum_ContabilizacionPorLote
   GetContabPorLotePagosAsEnum = gEnumProyecto.strContabilizacionPorLoteToNum(mContabPorLotePagos, False)
End Function

Private Sub SetContabPorLotePagosStr(ByVal valNewValue As String)
   mContabPorLotePagos = valNewValue
End Sub

Public Function GetCuentaPagosCxPProveedores() As String
   GetCuentaPagosCxPProveedores = mCuentaPagosCxPProveedores
End Function

Private Sub SetCuentaPagosCxPProveedores(ByVal valNewValue As String)
   mCuentaPagosCxPProveedores = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaPagosRetencionISLR() As String
   GetCuentaPagosRetencionISLR = mCuentaPagosRetencionISLR
End Function

Private Sub SetCuentaPagosRetencionISLR(ByVal valNewValue As String)
   mCuentaPagosRetencionISLR = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaPagosOtros() As String
   GetCuentaPagosOtros = mCuentaPagosOtros
End Function

Private Sub SetCuentaPagosOtros(ByVal valNewValue As String)
   mCuentaPagosOtros = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaPagosBanco() As String
   GetCuentaPagosBanco = mCuentaPagosBanco
End Function

Private Sub SetCuentaPagosBanco(ByVal valNewValue As String)
   mCuentaPagosBanco = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaPagosPagadoAnticipo() As String
   GetCuentaPagosPagadoAnticipo = mCuentaPagosPagadoAnticipo
End Function

Private Sub SetCuentaPagosPagadoAnticipo(ByVal valNewValue As String)
   mCuentaPagosPagadoAnticipo = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetManejarDiferenciaCambiariaEnPagos() As Boolean
   GetManejarDiferenciaCambiariaEnPagos = mManejarDiferenciaCambiariaEnPagos
End Function

Private Sub SetManejarDiferenciaCambiariaEnPagos(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mManejarDiferenciaCambiariaEnPagos = valNewValue
End Sub

'Facturación
Public Function GetContabIndividualFacturacionAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualFacturacionAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualFacturacion, False)
End Function

Private Sub SetContabIndividualFacturacionStr(ByVal valNewValue As String)
   mContabIndividualFacturacion = valNewValue
End Sub

Public Function GetContabPorLoteFacturacionAsEnum() As enum_ContabilizacionPorLote
   GetContabPorLoteFacturacionAsEnum = gEnumProyecto.strContabilizacionPorLoteToNum(mContabPorLoteFacturacion, False)
End Function

Private Sub SetContabPorLoteFacturacionStr(ByVal valNewValue As String)
   mContabPorLoteFacturacion = valNewValue
End Sub

Public Function GetCuentaFacturacionCxCClientes() As String
   GetCuentaFacturacionCxCClientes = mCuentaFacturacionCxCClientes
End Function

Private Sub SetCuentaFacturacionCxCClientes(ByVal valNewValue As String)
   mCuentaFacturacionCxCClientes = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaFacturacionMontoTotalFactura() As String
   GetCuentaFacturacionMontoTotalFactura = mCuentaFacturacionMontoTotalFactura
End Function

Private Sub SetCuentaFacturacionMontoTotalFactura(ByVal valNewValue As String)
   mCuentaFacturacionMontoTotalFactura = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaFacturacionCargos() As String
   GetCuentaFacturacionCargos = mCuentaFacturacionCargos
End Function

Private Sub SetCuentaFacturacionCargos(ByVal valNewValue As String)
   mCuentaFacturacionCargos = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaFacturacionDescuentos() As String
   GetCuentaFacturacionDescuentos = mCuentaFacturacionDescuentos
End Function

Private Sub SetCuentaFacturacionDescuentos(ByVal valNewValue As String)
   mCuentaFacturacionDescuentos = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaFacturacionIvaDiferido() As String
   GetCuentaFacturacionIvaDiferido = mCuentaFacturacionIvaDiferido
End Function

Private Sub SetCuentaFacturacionIvaDiferido(ByVal valNewValue As String)
   mCuentaFacturacionIvaDiferido = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetContabilizarPorArticulo() As Boolean
   GetContabilizarPorArticulo = mContabilizarPorArticulo
End Function

Private Sub SetContabilizarPorArticulo(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mContabilizarPorArticulo = valNewValue
End Sub

Public Function GetAgruparPorCuentaDeArticulo() As Boolean
   GetAgruparPorCuentaDeArticulo = mAgruparPorCuentaDeArticulo
End Function

Private Sub SetAgruparPorCuentaDeArticulo(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mAgruparPorCuentaDeArticulo = valNewValue
End Sub

Public Function GetAgruparPorCargosDescuentos() As Boolean
   GetAgruparPorCargosDescuentos = mAgruparPorCargosDescuentos
End Function

Private Sub SetAgruparPorCargosDescuentos(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mAgruparPorCargosDescuentos = valNewValue
End Sub

'Resumen Diario de Ventas
Public Function GetContabIndividualRDVtasAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualRDVtasAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualRDVtas, False)
End Function

Private Sub SetContabIndividualRDVtasStr(ByVal valNewValue As String)
   mContabIndividualRDVtas = valNewValue
End Sub

Public Function GetContabPorLoteRDVtasAsEnum() As enum_ContabilizacionPorLote
   GetContabPorLoteRDVtasAsEnum = gEnumProyecto.strContabilizacionPorLoteToNum(mContabPorLoteRDVtas, False)
End Function

Private Sub SetContabPorLoteRDVtasStr(ByVal valNewValue As String)
   mContabPorLoteRDVtas = valNewValue
End Sub

Public Function GetCuentaRDVtasCaja() As String
   GetCuentaRDVtasCaja = mCuentaRDVtasCaja
End Function

Private Sub SetCuentaRDVtasCaja(ByVal valNewValue As String)
   mCuentaRDVtasCaja = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaRDVtasMontoTotal() As String
   GetCuentaRDVtasMontoTotal = mCuentaRDVtasMontoTotal
End Function

Private Sub SetCuentaRDVtasMontoTotal(ByVal valNewValue As String)
   mCuentaRDVtasMontoTotal = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetContabilizarPorArticuloRDVtas() As Boolean
   GetContabilizarPorArticuloRDVtas = mContabilizarPorArticuloRDVtas
End Function

Private Sub SetContabilizarPorArticuloRDVtas(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mContabilizarPorArticuloRDVtas = valNewValue
End Sub

Public Function GetAgruparPorCuentaDeArticuloRDVtas() As Boolean
   GetAgruparPorCuentaDeArticuloRDVtas = mAgruparPorCuentaDeArticuloRDVtas
End Function

Private Sub SetAgruparPorCuentaDeArticuloRDVtas(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mAgruparPorCuentaDeArticuloRDVtas = valNewValue
End Sub

'Movimiento Bancario
Public Function GetContabIndividualMovBancarioAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualMovBancarioAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualMovBancario, False)
End Function

Private Sub SetContabIndividualMovBancarioStr(ByVal valNewValue As String)
   mContabIndividualMovBancario = valNewValue
End Sub

Public Function GetContabPorLoteMovBancarioAsEnum() As enum_ContabilizacionPorLote
   GetContabPorLoteMovBancarioAsEnum = gEnumProyecto.strContabilizacionPorLoteToNum(mContabPorLoteMovBancario, False)
End Function

Private Sub SetContabPorLoteMovBancarioStr(ByVal valNewValue As String)
   mContabPorLoteMovBancario = valNewValue
End Sub

Public Function GetCuentaMovBancarioGasto() As String
   GetCuentaMovBancarioGasto = mCuentaMovBancarioGasto
End Function

Private Sub SetCuentaMovBancarioGasto(ByVal valNewValue As String)
   mCuentaMovBancarioGasto = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaMovBancarioBancosHaber() As String
   GetCuentaMovBancarioBancosHaber = mCuentaMovBancarioBancosHaber
End Function

Private Sub SetCuentaMovBancarioBancosHaber(ByVal valNewValue As String)
   mCuentaMovBancarioBancosHaber = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaMovBancarioBancosDebe() As String
   GetCuentaMovBancarioBancosDebe = mCuentaMovBancarioBancosDebe
End Function

Private Sub SetCuentaMovBancarioBancosDebe(ByVal valNewValue As String)
   mCuentaMovBancarioBancosDebe = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaMovBancarioIngresos() As String
   GetCuentaMovBancarioIngresos = mCuentaMovBancarioIngresos
End Function

Private Sub SetCuentaMovBancarioIngresos(ByVal valNewValue As String)
   mCuentaMovBancarioIngresos = gTexto.DfMid(valNewValue, 1, 30)
End Sub

'Anticipo
Public Function GetContabIndividualAnticipoAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualAnticipoAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualAnticipo, False)
End Function

Private Sub SetContabIndividualAnticipoStr(ByVal valNewValue As String)
   mContabIndividualAnticipo = valNewValue
End Sub

Public Function GetContabPorLoteAnticipoAsEnum() As enum_ContabilizacionPorLote
   GetContabPorLoteAnticipoAsEnum = gEnumProyecto.strContabilizacionPorLoteToNum(mContabPorLoteAnticipo, False)
End Function

Private Sub SetContabPorLoteAnticipoStr(ByVal valNewValue As String)
   mContabPorLoteAnticipo = valNewValue
End Sub

Public Function GetCuentaAnticipoCaja() As String
   GetCuentaAnticipoCaja = mCuentaAnticipoCaja
End Function

Private Sub SetCuentaAnticipoCaja(ByVal valNewValue As String)
   mCuentaAnticipoCaja = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaAnticipoCobrado() As String
   GetCuentaAnticipoCobrado = mCuentaAnticipoCobrado
End Function

Private Sub SetCuentaAnticipoCobrado(ByVal valNewValue As String)
   mCuentaAnticipoCobrado = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaAnticipoOtrosIngresos() As String
   GetCuentaAnticipoOtrosIngresos = mCuentaAnticipoOtrosIngresos
End Function

Private Sub SetCuentaAnticipoOtrosIngresos(ByVal valNewValue As String)
   mCuentaAnticipoOtrosIngresos = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaAnticipoPagado() As String
   GetCuentaAnticipoPagado = mCuentaAnticipoPagado
End Function

Private Sub SetCuentaAnticipoPagado(ByVal valNewValue As String)
   mCuentaAnticipoPagado = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaAnticipoBanco() As String
   GetCuentaAnticipoBanco = mCuentaAnticipoBanco
End Function

Private Sub SetCuentaAnticipoBanco(ByVal valNewValue As String)
   mCuentaAnticipoBanco = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaAnticipoOtrosEgresos() As String
   GetCuentaAnticipoOtrosEgresos = mCuentaAnticipoOtrosEgresos
End Function

Private Sub SetCuentaAnticipoOtrosEgresos(ByVal valNewValue As String)
   mCuentaAnticipoOtrosEgresos = gTexto.DfMid(valNewValue, 1, 30)
End Sub

'Inventario
Public Function GetCuentaCostoDeVenta() As String
   GetCuentaCostoDeVenta = mCuentaCostoDeVenta
End Function

Private Sub SetCuentaCostoDeVenta(ByVal valNewValue As String)
   mCuentaCostoDeVenta = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaInventario() As String
   GetCuentaInventario = mCuentaInventario
End Function

Private Sub SetCuentaInventario(ByVal valNewValue As String)
   mCuentaInventario = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetAgruparPorCuentaDeArticuloInven() As Boolean
   GetAgruparPorCuentaDeArticuloInven = mAgruparPorCuentaDeArticuloInven
End Function

Private Sub SetAgruparPorCuentaDeArticuloInven(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mAgruparPorCuentaDeArticuloInven = valNewValue
End Sub

'Solicitud de Pago
Public Function GetContabIndividualPagosSueldosAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualPagosSueldosAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualPagosSueldos, False)
End Function

Private Sub SetContabIndividualPagosSueldosStr(ByVal valNewValue As String)
   mContabIndividualPagosSueldos = valNewValue
End Sub

Public Function GetCtaDePagosSueldos() As String
   GetCtaDePagosSueldos = mCtaDePagosSueldos
End Function

Private Sub SetCtaDePagosSueldos(ByVal valNewValue As String)
   mCtaDePagosSueldos = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCtaDePagosSueldosBanco() As String
   GetCtaDePagosSueldosBanco = mCtaDePagosSueldosBanco
End Function

Private Sub SetCtaDePagosSueldosBanco(ByVal valNewValue As String)
   mCtaDePagosSueldosBanco = gTexto.DfMid(valNewValue, 1, 30)
End Sub

'Caja chica - rendiciones
Public Function GetContabIndividualCajaChicaAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualCajaChicaAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualCajaChica, False)
End Function

Private Sub SetContabIndividualCajaChicaStr(ByVal valNewValue As String)
   mContabIndividualCajaChica = valNewValue
End Sub

Public Function GetCuentaCajaChicaGasto() As String
   GetCuentaCajaChicaGasto = mCuentaCajaChicaGasto
End Function

Private Sub SetCuentaCajaChicaGasto(ByVal valNewValue As String)
   mCuentaCajaChicaGasto = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCajaChicaBancoHaber() As String
   GetCuentaCajaChicaBancoHaber = mCuentaCajaChicaBancoHaber
End Function

Private Sub SetCuentaCajaChicaBancoHaber(ByVal valNewValue As String)
   mCuentaCajaChicaBancoHaber = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCajaChicaBancoDebe() As String
   GetCuentaCajaChicaBancoDebe = mCuentaCajaChicaBancoDebe
End Function

Private Sub SetCuentaCajaChicaBancoDebe(ByVal valNewValue As String)
   mCuentaCajaChicaBancoDebe = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaCajaChicaBanco() As String
   GetCuentaCajaChicaBanco = mCuentaCajaChicaBanco
End Function

Private Sub SetCuentaCajaChicaBanco(ByVal valNewValue As String)
   mCuentaCajaChicaBanco = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetMostrarDesglosadoCajaChica() As Boolean
   GetMostrarDesglosadoCajaChica = mMostrarDesglosadoCajaChica
End Function

Private Sub SetMostrarDesglosadoCajaChica(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mMostrarDesglosadoCajaChica = valNewValue
End Sub

Public Function GetContabIndividualRendicionesAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualRendicionesAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualRendiciones, False)
End Function

Private Sub SetContabIndividualRendicionesStr(ByVal valNewValue As String)
   mContabIndividualRendiciones = valNewValue
End Sub

Public Function GetCuentaRendicionesGasto() As String
   GetCuentaRendicionesGasto = mCuentaRendicionesGasto
End Function

Private Sub SetCuentaRendicionesGasto(ByVal valNewValue As String)
   mCuentaRendicionesGasto = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaRendicionesBanco() As String
   GetCuentaRendicionesBanco = mCuentaRendicionesBanco
End Function

Private Sub SetCuentaRendicionesBanco(ByVal valNewValue As String)
   mCuentaRendicionesBanco = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaRendicionesAnticipos() As String
   GetCuentaRendicionesAnticipos = mCuentaRendicionesAnticipos
End Function

Private Sub SetCuentaRendicionesAnticipos(ByVal valNewValue As String)
   mCuentaRendicionesAnticipos = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetMostrarDesglosadoRendiciones() As Boolean
   GetMostrarDesglosadoRendiciones = mMostrarDesglosadoRendiciones
End Function

Private Sub SetMostrarDesglosadoRendiciones(ByVal valNewValue As Boolean, Optional valueWasTrueOrFalseInSearch As Boolean = True)
   mMostrarDesglosadoRendiciones = valNewValue
End Sub

'Transferencias entre Cuentas Bancarias
Public Function GetContabIndividualTransferenciaEntreCuentaAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualTransferenciaEntreCuentaAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualTransferenciaEntreCuenta, False)
End Function

Private Sub SetContabIndividualTransferenciaEntreCuentaStr(ByVal valNewValue As String)
   mContabIndividualTransferenciaEntreCuenta = valNewValue
End Sub

Public Function GetContabPorLoteTransferenciaEntreCuentaAsEnum() As enum_ContabilizacionPorLote
   GetContabPorLoteTransferenciaEntreCuentaAsEnum = gEnumProyecto.strContabilizacionPorLoteToNum(mContabPorLoteTransferenciaEntreCuenta, False)
End Function

Private Sub SetContabPorLoteTransferenciaEntreCuentaStr(ByVal valNewValue As String)
   mContabPorLoteTransferenciaEntreCuenta = valNewValue
End Sub

Public Function GetCuentaTransfCtasBancoDestino() As String
   GetCuentaTransfCtasBancoDestino = mCuentaTransfCtasBancoDestino
End Function

Private Sub SetCuentaTransfCtasBancoDestino(ByVal valNewValue As String)
   mCuentaTransfCtasBancoDestino = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaTransfCtasGastoComOrigen() As String
   GetCuentaTransfCtasGastoComOrigen = mCuentaTransfCtasGastoComOrigen
End Function

Private Sub SetCuentaTransfCtasGastoComOrigen(ByVal valNewValue As String)
   mCuentaTransfCtasGastoComOrigen = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaTransfCtasGastoComDestino() As String
   GetCuentaTransfCtasGastoComDestino = mCuentaTransfCtasGastoComDestino
End Function

Private Sub SetCuentaTransfCtasGastoComDestino(ByVal valNewValue As String)
   mCuentaTransfCtasGastoComDestino = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaTransfCtasBancoOrigen() As String
   GetCuentaTransfCtasBancoOrigen = mCuentaTransfCtasBancoOrigen
End Function

Private Sub SetCuentaTransfCtasBancoOrigen(ByVal valNewValue As String)
   mCuentaTransfCtasBancoOrigen = gTexto.DfMid(valNewValue, 1, 30)
End Sub

'Orden de Producción
Public Function GetContabIndividualOrdenDeProduccionAsEnum() As enum_ContabilizacionIndividual
   GetContabIndividualOrdenDeProduccionAsEnum = gEnumProyecto.strContabilizacionIndividualToNum(mContabIndividualOrdenDeProduccion, False)
End Function

Private Sub SetContabIndividualOrdenDeProduccionStr(ByVal valNewValue As String)
   mContabIndividualOrdenDeProduccion = valNewValue
End Sub

Public Function GetContabPorLoteOrdenDeProduccionAsEnum() As enum_ContabilizacionPorLote
   GetContabPorLoteOrdenDeProduccionAsEnum = gEnumProyecto.strContabilizacionPorLoteToNum(mContabPorLoteOrdenDeProduccion, False)
End Function

Private Sub SetContabPorLoteOrdenDeProduccionStr(ByVal valNewValue As String)
   mContabPorLoteOrdenDeProduccion = valNewValue
End Sub

Public Function GetCuentaOrdenDeProduccionProductoTerminado() As String
   GetCuentaOrdenDeProduccionProductoTerminado = mCuentaOrdenDeProduccionProductoTerminado
End Function

Private Sub SetCuentaOrdenDeProduccionProductoTerminado(ByVal valNewValue As String)
   mCuentaOrdenDeProduccionProductoTerminado = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaOrdenDeProduccionMateriaPrima() As String
   GetCuentaOrdenDeProduccionMateriaPrima = mCuentaOrdenDeProduccionMateriaPrima
End Function

Private Sub SetCuentaOrdenDeProduccionMateriaPrima(ByVal valNewValue As String)
   mCuentaOrdenDeProduccionMateriaPrima = gTexto.DfMid(valNewValue, 1, 30)
End Sub

Public Function GetCuentaMermaAnormal() As String
   GetCuentaMermaAnormal = mCuentaMermaAnormal
End Function

Private Sub SetCuentaMermaAnormal(ByVal valNewValue As String)
   mCuentaMermaAnormal = gTexto.DfMid(valNewValue, 1, 30)
End Sub

'Final
Public Function GetNombreOperador() As String
   GetNombreOperador = mNombreOperador
End Function

Private Sub SetNombreOperador(ByVal valNewValue As String)
   mNombreOperador = gTexto.DfMid(valNewValue, 1, 10)
End Sub

Public Function GetFechaUltimaModificacion() As Date
   GetFechaUltimaModificacion = mFechaUltimaModificacion
End Function

Private Sub SetFechaUltimaModificacion(ByVal valNewValue As Date)
   mFechaUltimaModificacion = valNewValue
End Sub

Private Sub setValuesWithRS(Optional ByVal valRecordSet As ADODB.Recordset = Nothing)
   On Error GoTo h_ERROR
   If valRecordSet Is Nothing Then
      Set valRecordSet = mrsReglasDeContabilizacion
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ConsecutivoCompania") Then
      If Not IsNull(valRecordSet("ConsecutivoCompania")) Then
         SetConsecutivoCompania valRecordSet("ConsecutivoCompania")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "Numero") Then
      If Not IsNull(valRecordSet("Numero")) Then
         SetNumero valRecordSet("Numero")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaIva1Credito") Then
      If Not IsNull(valRecordSet("CuentaIva1Credito")) Then
         SetCuentaIva1Credito valRecordSet("CuentaIva1Credito")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaIva1Debito") Then
      If Not IsNull(valRecordSet("CuentaIva1Debito")) Then
         SetCuentaIva1Debito valRecordSet("CuentaIva1Debito")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaRetencionIva") Then
      If Not IsNull(valRecordSet("CuentaRetencionIva")) Then
         SetCuentaRetencionIVA valRecordSet("CuentaRetencionIva")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "DondeContabilizarRetIva") Then
      If Not IsNull(valRecordSet("DondeContabilizarRetIva")) Then
         SetDondeContabilizarRetIvaStr gEnumProyecto.enumDondeEfectuoContabilizacionRetIVAToString(gConvert.charAEnumerativoInt(valRecordSet("DondeContabilizarRetIva")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "DiferenciaEnCambioyCalculo") Then
      If Not IsNull(valRecordSet("DiferenciaEnCambioyCalculo")) Then
         SetDiferenciaEnCambioyCalculo valRecordSet("DiferenciaEnCambioyCalculo")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaDiferenciaCambiaria") Then
      If Not IsNull(valRecordSet("CuentaDiferenciaCambiaria")) Then
         SetCuentaDiferenciaCambiaria valRecordSet("CuentaDiferenciaCambiaria")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaDebitoBancarioGasto") Then
      If Not IsNull(valRecordSet("CuentaDebitoBancarioGasto")) Then
         SetCuentaDebitoBancarioGasto valRecordSet("CuentaDebitoBancarioGasto")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaDebitoBancarioBancos") Then
      If Not IsNull(valRecordSet("CuentaDebitoBancarioBancos")) Then
         SetCuentaDebitoBancarioBancos valRecordSet("CuentaDebitoBancarioBancos")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCreditoBancarioGasto") Then
      If Not IsNull(valRecordSet("CuentaCreditoBancarioGasto")) Then
         SetCuentaCreditoBancarioGasto valRecordSet("CuentaCreditoBancarioGasto")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCreditoBancarioBancos") Then
      If Not IsNull(valRecordSet("CuentaCreditoBancarioBancos")) Then
         SetCuentaCreditoBancarioBancos valRecordSet("CuentaCreditoBancarioBancos")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CxCtipoComprobante") Then
      If Not IsNull(valRecordSet("CxCtipoComprobante")) Then
         SetCxCTipoComprobante valRecordSet("CxCtipoComprobante")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CxPtipoComprobante") Then
      If Not IsNull(valRecordSet("CxPtipoComprobante")) Then
         SetCxPTipoComprobante valRecordSet("CxPtipoComprobante")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CobranzaTipoComprobante") Then
      If Not IsNull(valRecordSet("CobranzaTipoComprobante")) Then
         SetCobranzaTipoComprobante valRecordSet("CobranzaTipoComprobante")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "PagoTipoComprobante") Then
      If Not IsNull(valRecordSet("PagoTipoComprobante")) Then
         SetPagoTipoComprobante valRecordSet("PagoTipoComprobante")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "FacturaTipoComprobante") Then
      If Not IsNull(valRecordSet("FacturaTipoComprobante")) Then
         SetFacturaTipoComprobante valRecordSet("FacturaTipoComprobante")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "MovimientoBancarioTipoComprobante") Then
      If Not IsNull(valRecordSet("MovimientoBancarioTipoComprobante")) Then
         SetMovimientoBancarioTipoComprobante valRecordSet("MovimientoBancarioTipoComprobante")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "AnticipoTipoComprobante") Then
      If Not IsNull(valRecordSet("AnticipoTipoComprobante")) Then
         SetAnticipoTipoComprobante valRecordSet("AnticipoTipoComprobante")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "InventarioTipoComprobante") Then
      If Not IsNull(valRecordSet("InventarioTipoComprobante")) Then
         SetInventarioTipoComprobante valRecordSet("InventarioTipoComprobante")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "PagosSueldosTipoComprobante") Then
      If Not IsNull(valRecordSet("PagosSueldosTipoComprobante")) Then
         SetPagosSueldosTipoComprobante valRecordSet("PagosSueldosTipoComprobante")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "SiglasTipoComprobanteCajaChica") Then
      If Not IsNull(valRecordSet("SiglasTipoComprobanteCajaChica")) Then
         SetSiglasTipoComprobanteCajaChica valRecordSet("SiglasTipoComprobanteCajaChica")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "SiglasTipoComprobanteRendiciones") Then
      If Not IsNull(valRecordSet("SiglasTipoComprobanteRendiciones")) Then
         SetSiglasTipoComprobanteRendiciones valRecordSet("SiglasTipoComprobanteRendiciones")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TransfCtasSigasTipoComprobante") Then
      If Not IsNull(valRecordSet("TransfCtasSigasTipoComprobante")) Then
         SetTransfCtasSigasTipoComprobante valRecordSet("TransfCtasSigasTipoComprobante")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "OrdenDeProduccionTipoComprobante") Then
      If Not IsNull(valRecordSet("OrdenDeProduccionTipoComprobante")) Then
         SetOrdenDeProduccionTipoComprobante valRecordSet("OrdenDeProduccionTipoComprobante")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionCxc") Then
      If Not IsNull(valRecordSet("TipoContabilizacionCxc")) Then
         SetTipoContabilizacionCxCStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionCxc")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionCxp") Then
      If Not IsNull(valRecordSet("TipoContabilizacionCxp")) Then
         SetTipoContabilizacionCxPStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionCxp")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionCobranza") Then
      If Not IsNull(valRecordSet("TipoContabilizacionCobranza")) Then
         SetTipoContabilizacionCobranzaStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionCobranza")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionPagos") Then
      If Not IsNull(valRecordSet("TipoContabilizacionPagos")) Then
         SetTipoContabilizacionPagosStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionPagos")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionFacturacion") Then
      If Not IsNull(valRecordSet("TipoContabilizacionFacturacion")) Then
         SetTipoContabilizacionFacturacionStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionFacturacion")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionRdvtas") Then
      If Not IsNull(valRecordSet("TipoContabilizacionRdvtas")) Then
         SetTipoContabilizacionRDVtasStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionRdvtas")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionMovBancario") Then
      If Not IsNull(valRecordSet("TipoContabilizacionMovBancario")) Then
         SetTipoContabilizacionMovBancarioStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionMovBancario")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionAnticipo") Then
      If Not IsNull(valRecordSet("TipoContabilizacionAnticipo")) Then
         SetTipoContabilizacionAnticipoStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionAnticipo")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionInventario") Then
      If Not IsNull(valRecordSet("TipoContabilizacionInventario")) Then
         SetTipoContabilizacionInventarioStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionInventario")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionDePagosSueldos") Then
      If Not IsNull(valRecordSet("TipoContabilizacionDePagosSueldos")) Then
         SetTipoContabilizacionDePagosSueldosStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionDePagosSueldos")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionCajaChica") Then
      If Not IsNull(valRecordSet("TipoContabilizacionCajaChica")) Then
         SetTipoContabilizacionCajaChicaStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionCajaChica")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionRendicion") Then
      If Not IsNull(valRecordSet("TipoContabilizacionRendicion")) Then
         SetTipoContabilizacionRendicionStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionRendicion")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionTransferenciaEntreCuenta") Then
      If Not IsNull(valRecordSet("TipoContabilizacionTransferenciaEntreCuenta")) Then
         SetTipoContabilizacionTransferenciaEntreCuentaStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionTransferenciaEntreCuenta")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "TipoContabilizacionOrdenDeProduccion") Then
      If Not IsNull(valRecordSet("TipoContabilizacionOrdenDeProduccion")) Then
         SetTipoContabilizacionOrdenDeProduccionStr gEnumProyecto.enumTipoDeContabilizacionToString(gConvert.charAEnumerativoInt(valRecordSet("TipoContabilizacionOrdenDeProduccion")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertCxC") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertCxC")) Then
         SetEditarComprobanteAfterInsertCxC gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertCxC"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertCxP") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertCxP")) Then
         SetEditarComprobanteAfterInsertCxP gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertCxP"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertCobranza") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertCobranza")) Then
         SetEditarComprobanteAfterInsertCobranza gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertCobranza"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertPagos") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertPagos")) Then
         SetEditarComprobanteAfterInsertPagos gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertPagos"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertFactura") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertFactura")) Then
         SetEditarComprobanteAfterInsertFactura gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertFactura"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertResDia") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertResDia")) Then
         SetEditarComprobanteAfterInsertResDia gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertResDia"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertMovBan") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertMovBan")) Then
         SetEditarComprobanteAfterInsertMovBan gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertMovBan"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertAnticipo") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertAnticipo")) Then
         SetEditarComprobanteAfterInsertAnticipo gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertAnticipo"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertInventario") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertInventario")) Then
         SetEditarComprobanteAfterInsertInventario gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertInventario"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteDePagosSueldos") Then
      If Not IsNull(valRecordSet("EditarComprobanteDePagosSueldos")) Then
         SetEditarComprobanteDePagosSueldos gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteDePagosSueldos"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertCajaChica") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertCajaChica")) Then
         SetEditarComprobanteAfterInsertCajaChica gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertCajaChica"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertTransfCtas") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertTransfCtas")) Then
          SetEditarComprobanteAfterInsertTransfCtas gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertTransfCtas"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertImpTraBan") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertImpTraBan")) Then
         SetEditarComprobanteAfterInsertImpTraBan gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertImpTraBan"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "EditarComprobanteAfterInsertOrdenDeProduccion") Then
      If Not IsNull(valRecordSet("EditarComprobanteAfterInsertOrdenDeProduccion")) Then
          SetEditarComprobanteAfterInsertOrdenDeProduccion gConvert.ConvertStringToBoolean(valRecordSet("EditarComprobanteAfterInsertOrdenDeProduccion"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualCxc") Then
      If Not IsNull(valRecordSet("ContabIndividualCxc")) Then
         SetContabIndividualCxcStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualCxc")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabPorLoteCxc") Then
      If Not IsNull(valRecordSet("ContabPorLoteCxc")) Then
         SetContabPorLoteCxCStr gEnumProyecto.enumContabilizacionPorLoteToString(gConvert.charAEnumerativoInt(valRecordSet("ContabPorLoteCxc")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCxCclientes") Then
      If Not IsNull(valRecordSet("CuentaCxCclientes")) Then
         SetCuentaCxCClientes valRecordSet("CuentaCxCclientes")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCxCingresos") Then
      If Not IsNull(valRecordSet("CuentaCxCingresos")) Then
         SetCuentaCxCIngresos valRecordSet("CuentaCxCingresos")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualCxp") Then
      If Not IsNull(valRecordSet("ContabIndividualCxp")) Then
         SetContabIndividualCxPStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualCxp")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabPorLoteCxp") Then
      If Not IsNull(valRecordSet("ContabPorLoteCxp")) Then
         SetContabPorLoteCxPStr gEnumProyecto.enumContabilizacionPorLoteToString(gConvert.charAEnumerativoInt(valRecordSet("ContabPorLoteCxp")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCxPgasto") Then
      If Not IsNull(valRecordSet("CuentaCxPgasto")) Then
         SetCuentaCxPGasto valRecordSet("CuentaCxPgasto")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCxPproveedores") Then
      If Not IsNull(valRecordSet("CuentaCxPproveedores")) Then
         SetCuentaCxPProveedores valRecordSet("CuentaCxPproveedores")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaRetencionImpuestoMunicipal") Then
      If Not IsNull(valRecordSet("CuentaRetencionImpuestoMunicipal")) Then
         SetCuentaRetencionImpuestoMunicipal valRecordSet("CuentaRetencionImpuestoMunicipal")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualCobranza") Then
      If Not IsNull(valRecordSet("ContabIndividualCobranza")) Then
         SetContabIndividualCobranzaStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualCobranza")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabPorLoteCobranza") Then
      If Not IsNull(valRecordSet("ContabPorLoteCobranza")) Then
         SetContabPorLoteCobranzaStr gEnumProyecto.enumContabilizacionPorLoteToString(gConvert.charAEnumerativoInt(valRecordSet("ContabPorLoteCobranza")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCobranzaCobradoEnEfectivo") Then
      If Not IsNull(valRecordSet("CuentaCobranzaCobradoEnEfectivo")) Then
         SetCuentaCobranzaCobradoEnEfectivo valRecordSet("CuentaCobranzaCobradoEnEfectivo")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCobranzaCobradoEnCheque") Then
      If Not IsNull(valRecordSet("CuentaCobranzaCobradoEnCheque")) Then
         SetCuentaCobranzaCobradoEnCheque valRecordSet("CuentaCobranzaCobradoEnCheque")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCobranzaCobradoEnTarjeta") Then
      If Not IsNull(valRecordSet("CuentaCobranzaCobradoEnTarjeta")) Then
         SetCuentaCobranzaCobradoEnTarjeta valRecordSet("CuentaCobranzaCobradoEnTarjeta")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCobranzaRetencionIslr") Then
      If Not IsNull(valRecordSet("CuentaCobranzaRetencionIslr")) Then
         SetCuentaCobranzaRetencionISLR valRecordSet("CuentaCobranzaRetencionIslr")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCobranzaRetencionIva") Then
      If Not IsNull(valRecordSet("CuentaCobranzaRetencionIva")) Then
         SetCuentaCobranzaRetencionIVA valRecordSet("CuentaCobranzaRetencionIva")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCobranzaOtros") Then
      If Not IsNull(valRecordSet("CuentaCobranzaOtros")) Then
         SetCuentaCobranzaOtros valRecordSet("CuentaCobranzaOtros")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCobranzaCxCclientes") Then
      If Not IsNull(valRecordSet("CuentaCobranzaCxCclientes")) Then
         SetCuentaCobranzaCxCClientes valRecordSet("CuentaCobranzaCxCclientes")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCobranzaCobradoAnticipo") Then
      If Not IsNull(valRecordSet("CuentaCobranzaCobradoAnticipo")) Then
         SetCuentaCobranzaCobradoAnticipo valRecordSet("CuentaCobranzaCobradoAnticipo")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCobranzaIvaDiferido") Then
      If Not IsNull(valRecordSet("CuentaCobranzaIvaDiferido")) Then
         SetCuentaCobranzaIvaDiferido valRecordSet("CuentaCobranzaIvaDiferido")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ManejarDiferenciaCambiariaEnCobranza") Then
      If Not IsNull(valRecordSet("ManejarDiferenciaCambiariaEnCobranza")) Then
         SetManejarDiferenciaCambiariaEnCobranza gConvert.ConvertStringToBoolean(valRecordSet("ManejarDiferenciaCambiariaEnCobranza"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualPagos") Then
      If Not IsNull(valRecordSet("ContabIndividualPagos")) Then
         SetContabIndividualPagosStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualPagos")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabPorLotePagos") Then
      If Not IsNull(valRecordSet("ContabPorLotePagos")) Then
         SetContabPorLotePagosStr gEnumProyecto.enumContabilizacionPorLoteToString(gConvert.charAEnumerativoInt(valRecordSet("ContabPorLotePagos")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaPagosCxPproveedores") Then
      If Not IsNull(valRecordSet("CuentaPagosCxPproveedores")) Then
         SetCuentaPagosCxPProveedores valRecordSet("CuentaPagosCxPproveedores")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaPagosRetencionIslr") Then
      If Not IsNull(valRecordSet("CuentaPagosRetencionIslr")) Then
         SetCuentaPagosRetencionISLR valRecordSet("CuentaPagosRetencionIslr")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaPagosOtros") Then
      If Not IsNull(valRecordSet("CuentaPagosOtros")) Then
         SetCuentaPagosOtros valRecordSet("CuentaPagosOtros")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaPagosBanco") Then
      If Not IsNull(valRecordSet("CuentaPagosBanco")) Then
         SetCuentaPagosBanco valRecordSet("CuentaPagosBanco")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaPagosPagadoAnticipo") Then
      If Not IsNull(valRecordSet("CuentaPagosPagadoAnticipo")) Then
         SetCuentaPagosPagadoAnticipo valRecordSet("CuentaPagosPagadoAnticipo")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ManejarDiferenciaCambiariaEnPagos") Then
      If Not IsNull(valRecordSet("ManejarDiferenciaCambiariaEnPagos")) Then
         SetManejarDiferenciaCambiariaEnPagos gConvert.ConvertStringToBoolean(valRecordSet("ManejarDiferenciaCambiariaEnPagos"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualFacturacion") Then
      If Not IsNull(valRecordSet("ContabIndividualFacturacion")) Then
         SetContabIndividualFacturacionStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualFacturacion")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabPorLoteFacturacion") Then
      If Not IsNull(valRecordSet("ContabPorLoteFacturacion")) Then
         SetContabPorLoteFacturacionStr gEnumProyecto.enumContabilizacionPorLoteToString(gConvert.charAEnumerativoInt(valRecordSet("ContabPorLoteFacturacion")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaFacturacionCxCclientes") Then
      If Not IsNull(valRecordSet("CuentaFacturacionCxCclientes")) Then
         SetCuentaFacturacionCxCClientes valRecordSet("CuentaFacturacionCxCclientes")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaFacturacionMontoTotalFactura") Then
      If Not IsNull(valRecordSet("CuentaFacturacionMontoTotalFactura")) Then
         SetCuentaFacturacionMontoTotalFactura valRecordSet("CuentaFacturacionMontoTotalFactura")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaFacturacionCargos") Then
      If Not IsNull(valRecordSet("CuentaFacturacionCargos")) Then
         SetCuentaFacturacionCargos valRecordSet("CuentaFacturacionCargos")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaFacturacionDescuentos") Then
      If Not IsNull(valRecordSet("CuentaFacturacionDescuentos")) Then
         SetCuentaFacturacionDescuentos valRecordSet("CuentaFacturacionDescuentos")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaFacturacionIvaDiferido") Then
      If Not IsNull(valRecordSet("CuentaFacturacionIvaDiferido")) Then
         SetCuentaFacturacionIvaDiferido valRecordSet("CuentaFacturacionIvaDiferido")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabilizarPorArticulo") Then
      If Not IsNull(valRecordSet("ContabilizarPorArticulo")) Then
         SetContabilizarPorArticulo gConvert.ConvertStringToBoolean(valRecordSet("ContabilizarPorArticulo"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "AgruparPorCuentaDeArticulo") Then
      If Not IsNull(valRecordSet("AgruparPorCuentaDeArticulo")) Then
         SetAgruparPorCuentaDeArticulo gConvert.ConvertStringToBoolean(valRecordSet("AgruparPorCuentaDeArticulo"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "AgruparPorCargosDescuentos") Then
      If Not IsNull(valRecordSet("AgruparPorCargosDescuentos")) Then
         SetAgruparPorCargosDescuentos gConvert.ConvertStringToBoolean(valRecordSet("AgruparPorCargosDescuentos"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualRdvtas") Then
      If Not IsNull(valRecordSet("ContabIndividualRdvtas")) Then
         SetContabIndividualRDVtasStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualRdvtas")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabPorLoteRdvtas") Then
      If Not IsNull(valRecordSet("ContabPorLoteRdvtas")) Then
         SetContabPorLoteRDVtasStr gEnumProyecto.enumContabilizacionPorLoteToString(gConvert.charAEnumerativoInt(valRecordSet("ContabPorLoteRdvtas")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaRdvtasCaja") Then
      If Not IsNull(valRecordSet("CuentaRdvtasCaja")) Then
         SetCuentaRDVtasCaja valRecordSet("CuentaRdvtasCaja")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaRdvtasMontoTotal") Then
      If Not IsNull(valRecordSet("CuentaRdvtasMontoTotal")) Then
         SetCuentaRDVtasMontoTotal valRecordSet("CuentaRdvtasMontoTotal")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabilizarPorArticuloRdvtas") Then
      If Not IsNull(valRecordSet("ContabilizarPorArticuloRdvtas")) Then
         SetContabilizarPorArticuloRDVtas gConvert.ConvertStringToBoolean(valRecordSet("ContabilizarPorArticuloRdvtas"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "AgruparPorCuentaDeArticuloRdvtas") Then
      If Not IsNull(valRecordSet("AgruparPorCuentaDeArticuloRdvtas")) Then
         SetAgruparPorCuentaDeArticuloRDVtas gConvert.ConvertStringToBoolean(valRecordSet("AgruparPorCuentaDeArticuloRdvtas"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualMovBancario") Then
      If Not IsNull(valRecordSet("ContabIndividualMovBancario")) Then
         SetContabIndividualMovBancarioStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualMovBancario")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabPorLoteMovBancario") Then
      If Not IsNull(valRecordSet("ContabPorLoteMovBancario")) Then
         SetContabPorLoteMovBancarioStr gEnumProyecto.enumContabilizacionPorLoteToString(gConvert.charAEnumerativoInt(valRecordSet("ContabPorLoteMovBancario")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaMovBancarioGasto") Then
      If Not IsNull(valRecordSet("CuentaMovBancarioGasto")) Then
         SetCuentaMovBancarioGasto valRecordSet("CuentaMovBancarioGasto")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaMovBancarioBancosHaber") Then
      If Not IsNull(valRecordSet("CuentaMovBancarioBancosHaber")) Then
         SetCuentaMovBancarioBancosHaber valRecordSet("CuentaMovBancarioBancosHaber")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaMovBancarioBancosDebe") Then
      If Not IsNull(valRecordSet("CuentaMovBancarioBancosDebe")) Then
         SetCuentaMovBancarioBancosDebe valRecordSet("CuentaMovBancarioBancosDebe")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaMovBancarioIngresos") Then
      If Not IsNull(valRecordSet("CuentaMovBancarioIngresos")) Then
         SetCuentaMovBancarioIngresos valRecordSet("CuentaMovBancarioIngresos")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualAnticipo") Then
      If Not IsNull(valRecordSet("ContabIndividualAnticipo")) Then
         SetContabIndividualAnticipoStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualAnticipo")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabPorLoteAnticipo") Then
      If Not IsNull(valRecordSet("ContabPorLoteAnticipo")) Then
         SetContabPorLoteAnticipoStr gEnumProyecto.enumContabilizacionPorLoteToString(gConvert.charAEnumerativoInt(valRecordSet("ContabPorLoteAnticipo")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaAnticipoCaja") Then
      If Not IsNull(valRecordSet("CuentaAnticipoCaja")) Then
         SetCuentaAnticipoCaja valRecordSet("CuentaAnticipoCaja")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaAnticipoCobrado") Then
      If Not IsNull(valRecordSet("CuentaAnticipoCobrado")) Then
         SetCuentaAnticipoCobrado valRecordSet("CuentaAnticipoCobrado")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaAnticipoOtrosIngresos") Then
      If Not IsNull(valRecordSet("CuentaAnticipoOtrosIngresos")) Then
         SetCuentaAnticipoOtrosIngresos valRecordSet("CuentaAnticipoOtrosIngresos")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaAnticipoPagado") Then
      If Not IsNull(valRecordSet("CuentaAnticipoPagado")) Then
         SetCuentaAnticipoPagado valRecordSet("CuentaAnticipoPagado")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaAnticipoBanco") Then
      If Not IsNull(valRecordSet("CuentaAnticipoBanco")) Then
         SetCuentaAnticipoBanco valRecordSet("CuentaAnticipoBanco")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaAnticipoOtrosEgresos") Then
      If Not IsNull(valRecordSet("CuentaAnticipoOtrosEgresos")) Then
         SetCuentaAnticipoOtrosEgresos valRecordSet("CuentaAnticipoOtrosEgresos")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCostoDeVenta") Then
      If Not IsNull(valRecordSet("CuentaCostoDeVenta")) Then
         SetCuentaCostoDeVenta valRecordSet("CuentaCostoDeVenta")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaInventario") Then
      If Not IsNull(valRecordSet("CuentaInventario")) Then
         SetCuentaInventario valRecordSet("CuentaInventario")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "AgruparPorCuentaDeArticuloInven") Then
      If Not IsNull(valRecordSet("AgruparPorCuentaDeArticuloInven")) Then
         SetAgruparPorCuentaDeArticuloInven gConvert.ConvertStringToBoolean(valRecordSet("AgruparPorCuentaDeArticuloInven"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualPagosSueldos") Then
      If Not IsNull(valRecordSet("ContabIndividualPagosSueldos")) Then
         SetContabIndividualPagosSueldosStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualPagosSueldos")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CtaDePagosSueldos") Then
      If Not IsNull(valRecordSet("CtaDePagosSueldos")) Then
         SetCtaDePagosSueldos valRecordSet("CtaDePagosSueldos")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CtaDePagosSueldosBanco") Then
      If Not IsNull(valRecordSet("CtaDePagosSueldosBanco")) Then
         SetCtaDePagosSueldosBanco valRecordSet("CtaDePagosSueldosBanco")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualCajaChica") Then
      If Not IsNull(valRecordSet("ContabIndividualCajaChica")) Then
         SetContabIndividualCajaChicaStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualCajaChica")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCajaChicaGasto") Then
      If Not IsNull(valRecordSet("CuentaCajaChicaGasto")) Then
         SetCuentaCajaChicaGasto valRecordSet("CuentaCajaChicaGasto")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCajaChicaBancoHaber") Then
      If Not IsNull(valRecordSet("CuentaCajaChicaBancoHaber")) Then
         SetCuentaCajaChicaBancoHaber valRecordSet("CuentaCajaChicaBancoHaber")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCajaChicaBancoDebe") Then
      If Not IsNull(valRecordSet("CuentaCajaChicaBancoDebe")) Then
         SetCuentaCajaChicaBancoDebe valRecordSet("CuentaCajaChicaBancoDebe")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaCajaChicaBanco") Then
      If Not IsNull(valRecordSet("CuentaCajaChicaBanco")) Then
         SetCuentaCajaChicaBanco valRecordSet("CuentaCajaChicaBanco")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "MostrarDesglosadoCajaChica") Then
      If Not IsNull(valRecordSet("MostrarDesglosadoCajaChica")) Then
         SetMostrarDesglosadoCajaChica gConvert.ConvertStringToBoolean(valRecordSet("MostrarDesglosadoCajaChica"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualRendiciones") Then
      If Not IsNull(valRecordSet("ContabIndividualRendiciones")) Then
         SetContabIndividualRendicionesStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualRendiciones")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaRendicionesGasto") Then
      If Not IsNull(valRecordSet("CuentaRendicionesGasto")) Then
         SetCuentaRendicionesGasto valRecordSet("CuentaRendicionesGasto")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaRendicionesBanco") Then
      If Not IsNull(valRecordSet("CuentaRendicionesBanco")) Then
         SetCuentaRendicionesBanco valRecordSet("CuentaRendicionesBanco")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaRendicionesAnticipos") Then
      If Not IsNull(valRecordSet("CuentaRendicionesAnticipos")) Then
         SetCuentaRendicionesAnticipos valRecordSet("CuentaRendicionesAnticipos")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "MostrarDesglosadoRendiciones") Then
      If Not IsNull(valRecordSet("MostrarDesglosadoRendiciones")) Then
         SetMostrarDesglosadoRendiciones gConvert.ConvertStringToBoolean(valRecordSet("MostrarDesglosadoRendiciones"))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualTransferenciaEntreCuenta") Then
      If Not IsNull(valRecordSet("ContabIndividualTransferenciaEntreCuenta")) Then
         SetContabIndividualTransferenciaEntreCuentaStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualTransfCtas")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabPorLoteTransferenciaEntreCuenta") Then
      If Not IsNull(valRecordSet("ContabPorLoteTransferenciaEntreCuenta")) Then
         SetContabPorLoteTransferenciaEntreCuentaStr gEnumProyecto.enumContabilizacionPorLoteToString(gConvert.charAEnumerativoInt(valRecordSet("ContabPorLoteTransferenciaEntreCuenta")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaTransfCtasBancoDestino") Then
      If Not IsNull(valRecordSet("CuentaTransfCtasBancoDestino")) Then
         SetCuentaTransfCtasBancoDestino valRecordSet("CuentaTransfCtasBancoDestino")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaTransfCtasGastoComOrigen") Then
      If Not IsNull(valRecordSet("CuentaTransfCtasGastoComOrigen")) Then
         SetCuentaTransfCtasGastoComOrigen valRecordSet("CuentaTransfCtasGastoComOrigen")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaTransfCtasGastoComDestino") Then
      If Not IsNull(valRecordSet("CuentaTransfCtasGastoComDestino")) Then
         SetCuentaTransfCtasGastoComDestino valRecordSet("CuentaTransfCtasGastoComDestino")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaTransfCtasBancoOrigen") Then
      If Not IsNull(valRecordSet("CuentaTransfCtasBancoOrigen")) Then
         SetCuentaTransfCtasBancoOrigen valRecordSet("CuentaTransfCtasBancoOrigen")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabIndividualOrdenDeProduccion") Then
      If Not IsNull(valRecordSet("ContabIndividualOrdenDeProduccion")) Then
         SetContabIndividualOrdenDeProduccionStr gEnumProyecto.enumContabilizacionIndividualToString(gConvert.charAEnumerativoInt(valRecordSet("ContabIndividualOrdenDeProduccion")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "ContabPorLoteOrdenDeProduccion") Then
      If Not IsNull(valRecordSet("ContabPorLoteOrdenDeProduccion")) Then
         SetContabPorLoteOrdenDeProduccionStr gEnumProyecto.enumContabilizacionPorLoteToString(gConvert.charAEnumerativoInt(valRecordSet("ContabPorLoteOrdenDeProduccion")))
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaOrdenDeProduccionProductoTerminado") Then
      If Not IsNull(valRecordSet("CuentaOrdenDeProduccionProductoTerminado")) Then
         SetCuentaOrdenDeProduccionProductoTerminado valRecordSet("CuentaOrdenDeProduccionProductoTerminado")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaOrdenDeProduccionMateriaPrima") Then
      If Not IsNull(valRecordSet("CuentaOrdenDeProduccionMateriaPrima")) Then
         SetCuentaOrdenDeProduccionMateriaPrima valRecordSet("CuentaOrdenDeProduccionMateriaPrima")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "CuentaMermaAnormal") Then
      If Not IsNull(valRecordSet("CuentaMermaAnormal")) Then
         SetCuentaMermaAnormal valRecordSet("CuentaMermaAnormal")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "NombreOperador") Then
      If Not IsNull(valRecordSet("NombreOperador")) Then
         SetNombreOperador valRecordSet("NombreOperador")
      End If
   End If
   If gDbUtil.FieldInRecordSet(valRecordSet, "FechaUltimaModificacion") Then
      If Not gDbUtil.fDateIsNull(valRecordSet("FechaUltimaModificacion")) Then
         SetFechaUltimaModificacion valRecordSet("FechaUltimaModificacion")
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "setValuesWithRS", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitRS(ByVal valAction As AccionSobreRecord)
   On Error GoTo h_ERROR
   gDbUtil.sCloseIfOpened mrsReglasDeContabilizacion
   If mrsReglasDeContabilizacion Is Nothing Then
      Set mrsReglasDeContabilizacion = New ADODB.Recordset
   End If
   If (valAction = insertar) Then
      If gDbUtil.fOpenRecordSetAllParameters(mrsReglasDeContabilizacion, "SELECT * FROM Saw.ReglasDeContabilizacion WHERE ConsecutivoCompania=-1", gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitRS", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fRsRecordCount() As Long
   On Error GoTo h_ERROR
   If gDbUtil.fRsOpened(mrsReglasDeContabilizacion) Then
      fRsRecordCount = mrsReglasDeContabilizacion.RecordCount
   Else
      fRsRecordCount = 0
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fRsRecordCount = 0
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fRsRecordCount", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub Class_Initialize()
   On Error GoTo h_ERROR
   sClrRecord
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Class_Initialize", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Class_Terminate()
   On Error GoTo h_ERROR
   If Not (mrsReglasDeContabilizacion Is Nothing) Then
      gDbUtil.sCloseIfOpened mrsReglasDeContabilizacion
      Set mrsReglasDeContabilizacion = Nothing
   End If
   If Not (mrsReglasDeContabilizacionQuery Is Nothing) Then
      gDbUtil.sCloseIfOpened mrsReglasDeContabilizacionQuery
      Set mrsReglasDeContabilizacionQuery = Nothing
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Class_Terminate", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInsertaElRecordDeReglasDeContabilizacion(ByVal valConsecutivoCompania As Long)
   Dim objWrp As Object
   On Error GoTo h_ERROR
   Set objWrp = gUtilWrp.fCreateWrpForModule(gUtilWrp.fWrpClassName("Contabilizacion", "wrpReglasDeContabilizacion"), gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword)
   objWrp.InsertarRegistroPorDefecto gConvert.fConvierteAString(valConsecutivoCompania)
   Set objWrp = Nothing
h_ERROR:  On Error GoTo 0
Exit Sub
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInsertaElRecordDeReglasDeContabilizacion", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSearchReglasDeContabilizacionActual(ByVal valChangeRecordSet As Boolean) As Boolean
   On Error GoTo h_ERROR
   fSearchReglasDeContabilizacionActual = False
   sInitRS Buscar
   If gDbUtil.fOpenRecordSetAllParameters(mrsReglasDeContabilizacion, SQLSearchByFieldNumericAndConsecutivo("Saw.ReglasDeContabilizacion", gProyCompaniaActual.GetConsecutivoCompania), gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If mrsReglasDeContabilizacion.State = adStateOpen Then
         If mrsReglasDeContabilizacion.RecordCount = 0 Then
            Exit Function
         ElseIf valChangeRecordSet Then
            fSearchReglasDeContabilizacionActual = True
            setValuesWithRS
            sBuscaSiLasCuentasTienenAuxiliares
         Else
            fSearchReglasDeContabilizacionActual = True
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSearchReglasDeContabilizacionActual = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSearchReglasDeContabilizacionActual", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fLasCuentasDeDebitoBancarioEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = False
   If GetCuentaDebitoBancarioBancos <> "" And GetCuentaDebitoBancarioGasto <> "" Then
      EstanCompletas = True
   End If
h_EXIT:
   fLasCuentasDeDebitoBancarioEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeDebitoBancarioEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fLasCuentasDeCreditoBancarioEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = False
   If GetCuentaCreditoBancarioBancos <> "" And GetCuentaCreditoBancarioGasto <> "" Then
      EstanCompletas = True
   End If
h_EXIT:
   fLasCuentasDeCreditoBancarioEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeCreditoBancarioEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fLasCuentasDeIvaEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = True
   If GetCuentaIva1Credito = "" Or GetCuentaIva1Debito = "" Then
      EstanCompletas = False
   ElseIf GetDiferenciaEnCambioyCalculo = "" Then
      EstanCompletas = False
   End If
h_EXIT:
   fLasCuentasDeIvaEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeIvaEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fLasCuentasDeCxCEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = GetCuentaCxCClientes <> "" And GetCuentaCxCIngresos <> ""
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      EstanCompletas = EstanCompletas And GetCxCTipoComprobante <> ""
   End If
h_EXIT:
   fLasCuentasDeCxCEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeCxCEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fLasCuentasDeCxPEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = GetCuentaCxPGasto <> "" And GetCuentaCxPProveedores <> ""
   If EstanCompletas Then
      If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial Then
         If GetDondeContabilizarRetIvaAsEnum = eDC_NoContabilizada Then
            EstanCompletas = False
         ElseIf GetDondeContabilizarRetIvaAsEnum = eDC_CxP Then
            EstanCompletas = EstanCompletas And GetCuentaRetencionIVA <> ""
         End If
      End If
      If gProyParametrosCompania.GetRetieneImpuestoMunicipal() Then
         EstanCompletas = EstanCompletas And GetCuentaRetencionImpuestoMunicipal <> ""
      End If
   End If
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      EstanCompletas = EstanCompletas And GetCxPTipoComprobante <> ""
   End If
h_EXIT:
   fLasCuentasDeCxPEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeCxPEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
   
Private Function fLasCuentasDeCobranzaEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = GetCuentaCobranzaCobradoEnEfectivo <> ""
   EstanCompletas = EstanCompletas And GetCuentaCobranzaCobradoEnCheque <> ""
   EstanCompletas = EstanCompletas And GetCuentaCobranzaCobradoEnTarjeta <> ""
   EstanCompletas = EstanCompletas And GetCuentaCobranzaRetencionISLR <> ""
   EstanCompletas = EstanCompletas And GetCuentaCobranzaRetencionIVA <> ""
   EstanCompletas = EstanCompletas And GetCuentaCobranzaOtros <> ""
   EstanCompletas = EstanCompletas And GetCuentaCobranzaCxCClientes <> ""
   EstanCompletas = EstanCompletas And GetCuentaCobranzaCobradoAnticipo <> ""
   If gProyParametrosCompania.GetUsarVentasConIvaDiferido Then
      EstanCompletas = EstanCompletas And getCuentaCobranzaIvaDiferido <> ""
   End If
   If (gProyParametrosCompania.GetManejaCreditoBancario) Then
      EstanCompletas = EstanCompletas And fLasCuentasDeCreditoBancarioEstanCompletas()
   End If
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      EstanCompletas = EstanCompletas And GetCobranzaTipoComprobante <> ""
   End If
h_EXIT:
   fLasCuentasDeCobranzaEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeCobranzaEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fLasCuentasDePagosEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = GetCuentaPagosCxPProveedores <> ""
   EstanCompletas = EstanCompletas And GetCuentaPagosRetencionISLR <> ""
   EstanCompletas = EstanCompletas And GetCuentaPagosOtros <> ""
   EstanCompletas = EstanCompletas And GetCuentaPagosBanco <> ""
   EstanCompletas = EstanCompletas And GetCuentaPagosPagadoAnticipo <> ""
   If gProyParametrosCompania.GetManejaDebitoBancario Then
      EstanCompletas = EstanCompletas And fLasCuentasDeDebitoBancarioEstanCompletas()
   End If
   If EstanCompletas Then
      If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial Then
         If GetDondeContabilizarRetIvaAsEnum = eDC_NoContabilizada Then
            EstanCompletas = False
         ElseIf GetDondeContabilizarRetIvaAsEnum = eDC_Pago Then
            EstanCompletas = EstanCompletas And GetCuentaRetencionIVA <> ""
         End If
      End If
   End If
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      EstanCompletas = EstanCompletas And GetPagoTipoComprobante <> ""
   End If
h_EXIT:
   fLasCuentasDePagosEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDePagosEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fLasCuentasDeMovBancarioEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = GetCuentaMovBancarioGasto <> ""
   EstanCompletas = EstanCompletas And GetCuentaMovBancarioBancosHaber <> ""
   EstanCompletas = EstanCompletas And GetCuentaMovBancarioBancosDebe <> ""
   EstanCompletas = EstanCompletas And GetCuentaMovBancarioIngresos <> ""
   If gProyParametrosCompania.GetManejaDebitoBancario Then
      EstanCompletas = EstanCompletas And fLasCuentasDeDebitoBancarioEstanCompletas()
   End If
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      EstanCompletas = EstanCompletas And GetMovimientoBancarioTipoComprobante <> ""
   End If
h_EXIT:
   fLasCuentasDeMovBancarioEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeMovBancarioEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fLasCuentasDeFacturacionEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = GetCuentaFacturacionCxCClientes <> "" And GetCuentaFacturacionMontoTotalFactura <> "" And GetCuentaFacturacionCargos <> "" And GetCuentaFacturacionDescuentos <> ""
   If gProyParametrosCompania.GetUsarVentasConIvaDiferido Then
      EstanCompletas = EstanCompletas And GetCuentaFacturacionIvaDiferido <> ""
   End If
   If gProyCompaniaActual.GetTipoDeContribuyenteIVAStr = gEnumProyecto.enumTipoDeContribuyenteIvaToString(enum_TipoDeContribuyenteIVA.eTD_ESPECIAL) Then
      EstanCompletas = EstanCompletas And GetCuentaCreditoBancarioGasto <> ""
   End If
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      EstanCompletas = EstanCompletas And GetFacturaTipoComprobante <> ""
   End If
h_EXIT:
   fLasCuentasDeFacturacionEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeFacturacionEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fLasCuentasDeResumenDeVentasEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = GetCuentaRDVtasCaja <> "" And GetCuentaRDVtasMontoTotal <> ""
h_EXIT:
   fLasCuentasDeResumenDeVentasEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeResumenDeVentasEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fLasCuentasDeAnticipoEstanCompletas(ByVal valTipoDocumento As Integer) As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   If valTipoDocumento = enum_TipoDeAnticipo.eTDA_COBRADO Then
      EstanCompletas = GetCuentaAnticipoCaja <> ""
      EstanCompletas = EstanCompletas And GetCuentaAnticipoCobrado <> ""
      EstanCompletas = EstanCompletas And GetCuentaAnticipoOtrosIngresos <> ""
      If (gProyParametrosCompania.GetManejaCreditoBancario) Then
         EstanCompletas = EstanCompletas And fLasCuentasDeCreditoBancarioEstanCompletas()
      End If
   ElseIf valTipoDocumento = enum_TipoDeAnticipo.eTDA_PAGADO Then
      EstanCompletas = GetCuentaAnticipoBanco <> ""
      EstanCompletas = EstanCompletas And GetCuentaAnticipoPagado <> ""
      EstanCompletas = EstanCompletas And GetCuentaAnticipoOtrosEgresos <> ""
      If (gProyParametrosCompania.GetManejaDebitoBancario) Then
         EstanCompletas = EstanCompletas And fLasCuentasDeDebitoBancarioEstanCompletas()
      End If
   Else
      EstanCompletas = GetCuentaAnticipoCaja <> ""
      EstanCompletas = EstanCompletas And GetCuentaAnticipoCobrado <> ""
      EstanCompletas = EstanCompletas And GetCuentaAnticipoBanco <> ""
      EstanCompletas = EstanCompletas And GetCuentaAnticipoPagado <> ""
      EstanCompletas = EstanCompletas And GetCuentaAnticipoOtrosIngresos <> ""
      EstanCompletas = EstanCompletas And GetCuentaAnticipoOtrosEgresos <> ""
      EstanCompletas = EstanCompletas And fLasCuentasDeCreditoBancarioEstanCompletas()
      EstanCompletas = EstanCompletas And fLasCuentasDeDebitoBancarioEstanCompletas()
   End If
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      EstanCompletas = EstanCompletas And GetAnticipoTipoComprobante <> ""
   End If
h_EXIT:
   fLasCuentasDeAnticipoEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeAnticipoEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fLasCuentasDeReglasDeContabilizacionEstanCompletas(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valConMensaje As Boolean, ByVal valTipoDocumento As Integer) As Boolean
   Dim EstanCompletas As Boolean
   Dim mensaje As String
   On Error GoTo h_ERROR
   EstanCompletas = fLasCuentasDeIvaEstanCompletas
   If EstanCompletas Then
      Select Case valModulo
         Case eCG_CXC: EstanCompletas = fLasCuentasDeCxCEstanCompletas
         Case eCG_FACTURA: EstanCompletas = fLasCuentasDeFacturacionEstanCompletas
         Case eCG_CXP: EstanCompletas = fLasCuentasDeCxPEstanCompletas
         Case eCG_COBRANZA: EstanCompletas = fLasCuentasDeCobranzaEstanCompletas
         Case eCG_PAGOS: EstanCompletas = fLasCuentasDePagosEstanCompletas
         Case eCG_MOVIMIENTO_BANCARIO: EstanCompletas = fLasCuentasDeMovBancarioEstanCompletas
         Case eCG_RESUMEN_DIARIO_VENTAS: EstanCompletas = fLasCuentasDeResumenDeVentasEstanCompletas
         Case eCG_ANTICIPO: EstanCompletas = fLasCuentasDeAnticipoEstanCompletas(valTipoDocumento)
         Case eCG_INVENTARIO: EstanCompletas = fLasCuentasDeInventarioEstanCompletas()
         Case eCG_PAGO_SUELDOS: EstanCompletas = fLasCuentasDePagoSueldoEstanCompletas()
         Case eCG_REPOSICION: EstanCompletas = fLasCuentasDeReposicionEstanCompletas()
         Case eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA: EstanCompletas = fLasCuentasDeTransfCtasEstanCompletas()
         Case eCG_ORDEN_DE_PRODUCCION: EstanCompletas = fLasCuentasDeOrdenDeProduccionEstanCompletas()
      End Select
   End If
   If Not EstanCompletas And valConMensaje Then
      mensaje = "Faltan definir Cuentas Contables"
      If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
         mensaje = mensaje & " o Tipos de Comprobante"
      End If
      mensaje = mensaje & " en las Reglas de Contabilización para " & gEnumProyectoWincont.enumComprobanteGeneradoPorToString(valModulo) & "." & vbCr & vbCr
      mensaje = mensaje & "Vaya al módulo de Reglas de Contabilización y complételas, por favor."
      gMessage.Advertencia mensaje
   End If
h_EXIT:
   fLasCuentasDeReglasDeContabilizacionEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeReglasDeContabilizacionEstanCompletas ", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fBuscaLasReglasDeContabilizacion(ByVal valShowMessage As Boolean, ByVal valChangeRecordSet As Boolean) As Boolean
   Dim Encontradas As Boolean
   On Error GoTo h_ERROR
   Encontradas = True
   If gProyCompaniaActual.GetTieneAccesoACaracteristicaContabilidadActiva Then
      Encontradas = fSearchReglasDeContabilizacionActual(valChangeRecordSet)
      If Not Encontradas And valShowMessage Then
         gMessage.Advertencia "No se pudieron leer las Reglas de Contabilización. " & vbCr & vbCr & "Comuníquese con " & gDefgen.fNombreInformaticaGalac
         GoTo h_EXIT
      End If
   End If
h_EXIT:
   fBuscaLasReglasDeContabilizacion = Encontradas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fBuscaLasReglasDeContabilizacion", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sBuscaSiLasCuentasTienenAuxiliares()
   Dim insCuenta As clsCuentaNavigator
   Dim vListaCuentas As String
   Dim vResultCuentas As String
   On Error GoTo h_ERROR
   Set insCuenta = New clsCuentaNavigator
   vResultCuentas = ""
   vListaCuentas = fGetListaCuentasAVerificar
   vResultCuentas = insCuenta.fDevuelveCuentasQueManejanAuxiliar(vListaCuentas)
   If gTexto.DfLen(vResultCuentas) > 0 Then
      sBuscarCuentaUsaAuxiliar vResultCuentas
   End If
   Set insCuenta = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sBuscaSiLasCuentasTienenAuxiliares", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fLaFechaEsValidaParaContabilizacion(ByVal valAction As AccionSobreRecord, ByVal valFechaDocumento As Date, ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valShowMessage As Boolean) As Boolean
   Dim varEsValida As Boolean
   Dim insComprobanteNavigator As clsComprobanteNavigator
   On Error GoTo h_ERROR
   varEsValida = False
   If (valAction <> consultar) And (valAction <> imprimir) And (valAction <> reimprimir) Then
      varEsValida = gContPeriodoActual.fBuscaElPeriodoDeFechaYsiExisteLoColocaComoPeriodoActual(valFechaDocumento, valShowMessage)
      If varEsValida Then
         If gContPeriodoActual.ElPeriodoEstaCerrado(gProyReglasDeContabilizacion.fLaAccionRequiereUnPeriodoYMesAbierto(valAction, valModulo)) And gProyReglasDeContabilizacion.fLaAccionRequiereUnPeriodoYMesAbierto(valAction, valModulo) Then
            varEsValida = False
            GoTo h_EXIT
         End If
      Else
         GoTo h_EXIT
      End If
      Set insComprobanteNavigator = New clsComprobanteNavigator
      If insComprobanteNavigator.fElComprobanteResumenYaExiste(valFechaDocumento, valModulo, True, 0, True) Then 'para revision(ConsecutivoDocumento para Comprobante -> 0,True)
         varEsValida = False
      End If
      Set insComprobanteNavigator = Nothing
   Else
      varEsValida = True
   End If
h_EXIT: On Error GoTo 0
   fLaFechaEsValidaParaContabilizacion = varEsValida
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLaFechaEsValidaParaContabilizacion", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fBuscaElTipoDeContabilizacion(ByVal valModulo As enum_ComprobanteGeneradoPor) As enum_TipoDeContabilizacion
   On Error GoTo h_ERROR
   Select Case valModulo
      Case eCG_CXC: fBuscaElTipoDeContabilizacion = GetTipoContabilizacionCxCAsEnum
      Case eCG_FACTURA: fBuscaElTipoDeContabilizacion = GetTipoContabilizacionFacturacionAsEnum
      Case eCG_CXP: fBuscaElTipoDeContabilizacion = GetTipoContabilizacionCxPAsEnum
      Case eCG_COBRANZA: fBuscaElTipoDeContabilizacion = GetTipoContabilizacionCobranzaAsEnum
      Case eCG_PAGOS: fBuscaElTipoDeContabilizacion = GetTipoContabilizacionPagosAsEnum
      Case eCG_MOVIMIENTO_BANCARIO: fBuscaElTipoDeContabilizacion = GetTipoContabilizacionMovBancarioAsEnum
      Case eCG_RESUMEN_DIARIO_VENTAS: fBuscaElTipoDeContabilizacion = GetTipoContabilizacionRDVtasAsEnum
      Case eCG_ANTICIPO: fBuscaElTipoDeContabilizacion = GetTipoContabilizacionAnticipoAsEnum
      Case eCG_PAGO_SUELDOS: fBuscaElTipoDeContabilizacion = GetTipoContabilizacionDePagosSueldosAsEnum()
      Case eCG_REPOSICION: fBuscaElTipoDeContabilizacion = GetTipoContabilizacionCajaChicaAsEnum()
      Case eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA: fBuscaElTipoDeContabilizacion = enum_TipoDeContabilizacion.eTD_CADA_DOCUMENTO
      Case eCG_ORDEN_DE_PRODUCCION: fBuscaElTipoDeContabilizacion = enum_TipoDeContabilizacion.eTD_CADA_DOCUMENTO
      Case Else: gMessage.Advertencia "Falta Buscar tipo de Contabilizacion en fBuscaElTipoDeContabilizacion clsReglasDeContabilizacionNavigator - Case Else"
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fBuscaElTipoDeContabilizacion", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fBuscaElTipoDeContabilizacionIndividual(ByVal valModulo As enum_ComprobanteGeneradoPor) As enum_ContabilizacionIndividual
   On Error GoTo h_ERROR
   Select Case valModulo
      Case eCG_CXC: fBuscaElTipoDeContabilizacionIndividual = GetContabIndividualCxcAsEnum
      Case eCG_FACTURA: fBuscaElTipoDeContabilizacionIndividual = GetContabIndividualFacturacionAsEnum
      Case eCG_CXP: fBuscaElTipoDeContabilizacionIndividual = GetContabIndividualCxPAsEnum
      Case eCG_COBRANZA: fBuscaElTipoDeContabilizacionIndividual = GetContabIndividualCobranzaAsEnum
      Case eCG_PAGOS: fBuscaElTipoDeContabilizacionIndividual = GetContabIndividualPagosAsEnum
      Case eCG_MOVIMIENTO_BANCARIO: fBuscaElTipoDeContabilizacionIndividual = GetContabIndividualMovBancarioAsEnum
      Case eCG_RESUMEN_DIARIO_VENTAS: fBuscaElTipoDeContabilizacionIndividual = GetContabIndividualRDVtasAsEnum
      Case eCG_ANTICIPO: fBuscaElTipoDeContabilizacionIndividual = GetContabIndividualAnticipoAsEnum
      Case eCG_PAGO_SUELDOS: fBuscaElTipoDeContabilizacionIndividual = GetContabIndividualPagosSueldosAsEnum
      Case eCG_REPOSICION: fBuscaElTipoDeContabilizacionIndividual = GetContabIndividualCajaChicaAsEnum
      Case eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA: fBuscaElTipoDeContabilizacionIndividual = GetContabIndividualTransferenciaEntreCuentaAsEnum
      Case eCG_ORDEN_DE_PRODUCCION: fBuscaElTipoDeContabilizacionIndividual = eCI_POSPUESTA
     Case Else: gMessage.Advertencia "Falta Buscar tipo de Contabilizacion en fBuscaElTipoDeContabilizacionIndividual clsReglasDeContabilizacionNavigator - Case Else"
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fBuscaElTipoDeContabilizacionIndividual", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sEscogeReglasDeContabilizacionDeLaCiaActualSiNoExisteLoCrea(ByVal valShowMessage As Boolean)
   On Error GoTo h_ERROR
   If Not fBuscaLasReglasDeContabilizacion(valShowMessage, False) Then
      sInsertaElRecordDeReglasDeContabilizacion gProyCompaniaActual.GetConsecutivoCompania
      If Not fBuscaLasReglasDeContabilizacion(True, True) Then
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEscogeReglasDeContabilizacionDeLaCiaActualSiNoExisteLoCrea", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fSePuedeModificarLaFechaDelDocumentoAdministrativo(ByVal valModulo As enum_ComprobanteGeneradoPor) As Boolean
   On Error GoTo h_ERROR
   fSePuedeModificarLaFechaDelDocumentoAdministrativo = True
   If fBuscaElTipoDeContabilizacion(valModulo) = eTD_CADA_DOCUMENTO And fBuscaElTipoDeContabilizacionIndividual(valModulo) = eCI_INMEDIATA Then
      fSePuedeModificarLaFechaDelDocumentoAdministrativo = False
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSePuedeModificarLaFechaDelDocumentoAdministrativo", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fCumpleConLasCondicionesParaContabilizarSiEsNecesario(ByVal valAction As AccionSobreRecord, ByVal valFechaDocumento As Date, ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valMostrarMensaje As Boolean) As Boolean
   Dim varCumple As Boolean
   On Error GoTo h_ERROR
   varCumple = True
   If gProyCompaniaActual.GetTieneAccesoACaracteristicaContabilidadActiva Then
      If valFechaDocumento >= gProyParametrosCompania.GetFechaDeInicioContabilizacion Then
         varCumple = fLaFechaEsValidaParaContabilizacion(valAction, valFechaDocumento, valModulo, valMostrarMensaje)
         If varCumple Then
            If gContPeriodoActual.fLaFechaPerteneceAunMesCerrado(valFechaDocumento, False) And valAction <> consultar And valAction <> imprimir And valAction <> reimprimir Then
               fMostrarMensaje valMostrarMensaje, valAction, varCumple, valModulo
            End If
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   fCumpleConLasCondicionesParaContabilizarSiEsNecesario = varCumple
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCumpleConLasCondicionesParaContabilizarSiEsNecesario", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen() As String
   On Error Resume Next
   fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen = gUtilSQL.CharConcat & gUtilSQL.DfChar(9) & gUtilSQL.CharConcat
   On Error GoTo 0
End Function

Public Function fImprimeReglasDeContabilizacion() As Boolean
   Dim SqlReporte As String
   Dim rptReporteReglas As DDActiveReports2.ActiveReport
   Dim insCuenta As clsCuentaNavigator
   Dim RutaFileXML As String
   On Error GoTo h_ERROR
   fImprimeReglasDeContabilizacion = False
   Set rptReporteReglas = New ActiveReport
   RutaFileXML = gUtilReports.getCompletePathDelFileDeImpresionRpx("rpxReglasDeContabilizacion", True, "Reglas de Contabilización")
   If gUtilReports.fLoadLayout(rptReporteReglas, RutaFileXML) Then
      Set insCuenta = New clsCuentaNavigator
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblNombreDeLaCompania", gProyCompaniaActual.GetNombreCompaniaParaInformes(True, False)
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva1Credito", gProyReglasDeContabilizacion.GetCuentaIva1Debito
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva2Credito", gProyReglasDeContabilizacion.GetCuentaIva1Debito
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaIva1Debito, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva1CreditoDescripcion", insCuenta.GetDescripcion
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva2CreditoDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva1CreditoDescripcion", ""
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva2CreditoDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva1Debito", gProyReglasDeContabilizacion.GetCuentaIva1Credito
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva2Debito", gProyReglasDeContabilizacion.GetCuentaIva1Credito
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva3Debito", gProyReglasDeContabilizacion.GetCuentaIva1Credito
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaIva1Credito, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva1DebitoDescripcion", insCuenta.GetDescripcion
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva2DebitoDescripcion", insCuenta.GetDescripcion
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva3DebitoDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva1DebitoDescripcion", ""
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva2DebitoDescripcion", ""
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaIva3DebitoDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaRetencionIva", gProyReglasDeContabilizacion.GetCuentaRetencionIVA
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaRetencionIVA, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaRetencionIvaDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaRetencionIvaDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDiferenciaEnCambioyCalculo", gProyReglasDeContabilizacion.GetDiferenciaEnCambioyCalculo
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetDiferenciaEnCambioyCalculo, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDiferenciaEnCambioyCalculoDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDiferenciaEnCambioyCalculoDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCxCClientes", gProyReglasDeContabilizacion.GetCuentaCxCClientes
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaCxCClientes, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCxCClientesDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCxCClientesDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCxCIngresos", gProyReglasDeContabilizacion.GetCuentaCxCIngresos
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaCxCIngresos, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCxCIngresosDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCxCIngresosDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCxPGasto", gProyReglasDeContabilizacion.GetCuentaCxPGasto
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaCxPGasto, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCxPGastoDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCxPGastoDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCxPProveedores", gProyReglasDeContabilizacion.GetCuentaCxPProveedores
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaCxPProveedores, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCxPProveedoresDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCxPProveedoresDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCobradoEnEfectivo", gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoEnEfectivo
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoEnEfectivo, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCobradoEnEfectivoDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCobradoEnEfectivoDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCobradoEnCheque", gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoEnCheque
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoEnCheque, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCobradoEnChequeDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCobradoEnChequeDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCobradoAnticipo", gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoAnticipo
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoAnticipo, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCobradoAnticipoDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCobradoAnticipoDescribcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaOtros", gProyReglasDeContabilizacion.GetCuentaCobranzaOtros
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaCobranzaOtros, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaOtrosDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaOtrosDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblcuentaCobranzaRetencionISLR", gProyReglasDeContabilizacion.GetCuentaCobranzaRetencionISLR
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaCobranzaRetencionISLR, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblcuentaCobranzaRetencionISLRDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblcuentaCobranzaRetencionISLRDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblcuentaCobranzaRetencionIVA", gProyReglasDeContabilizacion.GetCuentaCobranzaRetencionIVA
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaCobranzaRetencionIVA, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblcuentaCobranzaRetencionIVADescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblcuentaCobranzaRetencionIVADescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCobradoEnTarjeta", gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoEnTarjeta
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaCobranzaCobradoEnTarjeta, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCobradoEnTarjetaDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCobradoEnTarjetaDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCxCClientes", gProyReglasDeContabilizacion.GetCuentaCobranzaCxCClientes
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaCobranzaCxCClientes, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCxCClientesDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCobranzaCxCClientesDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaPagosCxPProveedores", gProyReglasDeContabilizacion.GetCuentaPagosCxPProveedores
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaPagosCxPProveedores, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaPagosCxPProveedoresDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaPagosCxPProveedoresDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaPagosBanco", gProyReglasDeContabilizacion.GetCuentaPagosBanco
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaPagosBanco, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaPagosBancoDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaPagosBancoDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaPagosOtros", gProyReglasDeContabilizacion.GetCuentaPagosOtros
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaPagosOtros, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaPagosOtrosDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaPagosOtrosDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaPagosRetencionISLR", gProyReglasDeContabilizacion.GetCuentaPagosRetencionISLR
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaPagosRetencionISLR, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaPagosRetencionISLRDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaPagosRetencionISLRDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentasPagosPagadosAnticipo", gProyReglasDeContabilizacion.GetCuentaPagosPagadoAnticipo
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaPagosPagadoAnticipo, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentasPagosPagadosAnticipoDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentasPagosPagadosAnticipoDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaMovBancarioBancosDebe", gProyReglasDeContabilizacion.GetCuentaMovBancarioBancosDebe
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaMovBancarioBancosDebe, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaMovBancarioBancosDebeDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaMovBancarioBancosDebeDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaMovBancarioIngresos", gProyReglasDeContabilizacion.GetCuentaMovBancarioIngresos
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaMovBancarioIngresos, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaMovBancarioIngresosDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaMovBancarioIngresosDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaMovBancarioGasto", gProyReglasDeContabilizacion.GetCuentaMovBancarioGasto
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaMovBancarioGasto, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaMovBancarioGastoDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaMovBancarioGastoIngresosDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaMovBancarioBancosHaber", gProyReglasDeContabilizacion.GetCuentaMovBancarioBancosHaber
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaMovBancarioBancosHaber, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaMovBancarioBancosHaberDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaMovBancarioBancosHaberDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaFacturacionCxCClientes", gProyReglasDeContabilizacion.GetCuentaFacturacionCxCClientes
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaFacturacionCxCClientes, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaFacturacionCxCClientesDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaFacturacionCxCClientesDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaFacturacionDescuentos", gProyReglasDeContabilizacion.GetCuentaFacturacionDescuentos
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaFacturacionDescuentos, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaFacturacionDescuentosDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaFacturacionDescuentosDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaFacturacionMontoTotalFactura", gProyReglasDeContabilizacion.GetCuentaFacturacionMontoTotalFactura
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaFacturacionMontoTotalFactura, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaFacturacionMontoTotalFacturaDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaFacturacionMontoTotalFacturaDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaDebitoBancarioGasto", gProyReglasDeContabilizacion.GetCuentaDebitoBancarioGasto
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioGasto, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaDebitoBancarioGastoDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaDebitoBancarioGastoDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaDebitoBancarioBancos", gProyReglasDeContabilizacion.GetCuentaDebitoBancarioBancos
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaDebitoBancarioBancos, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaDebitoBancarioBancosDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaDebitoBancarioBancosDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaFacturacionCargos", gProyReglasDeContabilizacion.GetCuentaFacturacionCargos
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaFacturacionCargos, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaFacturacionCargosDescripcion", insCuenta.GetDescripcion
      Else
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaFacturacionCargosDescripcion", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaCajaAnticipo", gProyReglasDeContabilizacion.GetCuentaAnticipoCaja
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaAnticipoCaja, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDescripcionDeAnticipo", insCuenta.GetDescripcion
      Else
          gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDescripcionDeAnticipo", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaAnticipoCobrado", gProyReglasDeContabilizacion.GetCuentaAnticipoCobrado
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaAnticipoCobrado, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDescripcionDeAnticipocobrado", insCuenta.GetDescripcion
      Else
          gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDescripcionDeAnticipocobrado", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaOtrosIngresosAnticipo", gProyReglasDeContabilizacion.GetCuentaAnticipoOtrosIngresos
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaAnticipoOtrosIngresos, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDescripcionCuentaOtrosIngresosAnticipo", insCuenta.GetDescripcion
      Else
          gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDescripcionCuentaOtrosIngresosAnticipo", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaAnticipoPagado", gProyReglasDeContabilizacion.GetCuentaAnticipoPagado
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaAnticipoPagado, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDescripcionAnticipoPagado", insCuenta.GetDescripcion
      Else
          gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDescripcionAnticipoPagado", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaOtrosEgresosAnticipo", gProyReglasDeContabilizacion.GetCuentaAnticipoOtrosEgresos
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaAnticipoOtrosEgresos, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDescripcionOtrosEgresosAnticipo", insCuenta.GetDescripcion
      Else
          gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDescripcionOtrosEgresosAnticipo", ""
      End If
      gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblCuentaBancoAnticipo", gProyReglasDeContabilizacion.GetCuentaAnticipoBanco
      If insCuenta.fSearchCuentaByCodigo(gProyReglasDeContabilizacion.GetCuentaAnticipoBanco, True) Then
         gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDescripcionBancoAnticipo", insCuenta.GetDescripcion
      Else
          gUtilReports.sDefaultConfigurationForLabels rptReporteReglas, "lblDescripcionBancoAnticipo", ""
      End If
      Set insCuenta = Nothing
      gUtilMargins.sAsignarMargenesGenerales rptReporteReglas
      gUtilReports.sMostrarOImprimirReporte rptReporteReglas, 1, eDI_PANTALLA, "Impresión de Reglas de Contabilización"
      fImprimeReglasDeContabilizacion = True
   End If
'   completar
   Set rptReporteReglas = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fImprimeReglasDeContabilizacion", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fLaReglaGeneralParaEsteModuloUsaAuxiliar(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valCtaTipoContabilizacion As Integer, ByVal valTipoDocumento As Integer) As Boolean
   Dim varUsaAuxiliar As Boolean
   On Error GoTo h_ERROR
   fLaReglaGeneralParaEsteModuloUsaAuxiliar = False
   Select Case valModulo
      Case eCG_CXP
           If valCtaTipoContabilizacion = eTC_ReglaProvGasto Then
              varUsaAuxiliar = GetCuentaCxPGastoUsaAuxiliares
           Else
              varUsaAuxiliar = GetCuentaCxPProveedoresUsaAuxiliares
           End If
      Case eCG_PAGOS: varUsaAuxiliar = getCuentaPagosCxPProveedoresUsaAuxiliares
      Case eCG_FACTURA: varUsaAuxiliar = getCuentaFacturacionCxCClientesUsaAuxiliares
      Case eCG_CXC
           If valCtaTipoContabilizacion = eTC_ReglaCliCxC Then
              varUsaAuxiliar = GetCuentaCxCClientesUsaAuxiliares
           Else
              varUsaAuxiliar = GetCuentaCxCIngresosUsaAuxiliares
           End If
      Case eCG_COBRANZA: varUsaAuxiliar = getCuentaCobranzaCxCClientesUsaAuxiliares
      Case eCG_RESUMEN_DIARIO_VENTAS: varUsaAuxiliar = False
      Case eCG_ANTICIPO
         If valTipoDocumento = enum_TipoDeAnticipo.eTDA_COBRADO Then
            varUsaAuxiliar = getCuentaAnticipoCobradoUsaAuxiliares
         Else
            varUsaAuxiliar = getCuentaAnticipoPagadoUsaAuxiliares
         End If
   End Select
   fLaReglaGeneralParaEsteModuloUsaAuxiliar = varUsaAuxiliar
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLaReglaGeneralParaEsteModuloUsaAuxiliar", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fLasCuentasDeInventarioEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = (GetCuentaCostoDeVenta() <> "" And GetCuentaInventario() <> "")
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      EstanCompletas = EstanCompletas And GetInventarioTipoComprobante <> ""
   End If
h_EXIT:
   fLasCuentasDeInventarioEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeFacturacionEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSeContabilizoElUltimoMesDeInv() As Boolean
   Dim insComprobanteSQL As clsComprobanteSQL
   Dim vResult As Boolean
   Dim vRegistros As Long
   On Error GoTo h_ERROR
   Set insComprobanteSQL = New clsComprobanteSQL
   vRegistros = gDbUtil.fRecordCountOfSql(insComprobanteSQL.fSQLFechaUltimoComprobanteDelPeriodo(gContPeriodoActual.GetConsecutivoPeriodo, gContPeriodoActual.GetFechaCierreDelPeriodo))
   vResult = (vRegistros > 0)
   fSeContabilizoElUltimoMesDeInv = vResult
   Set insComprobanteSQL = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
 fSeContabilizoElUltimoMesDeInv = vResult
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSeContabilizoElUltimoMesDeInv", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fPuedoContabilizarInv() As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = False
   If fSeContabilizoElUltimoMesDeInv() Then
      gMessage.Advertencia "Ya se contabilizaron todos los meses del Periodo."
      vResult = False
   Else
      vResult = True
   End If
   fPuedoContabilizarInv = vResult
h_EXIT:      On Error GoTo 0
   Exit Function
fPuedoContabilizarInv = vResult
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fPuedoContabilizarInv", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fMostrarMensaje(ByVal valMostrarMensaje As Boolean, ByVal valAction As AccionSobreRecord, ByRef refCumple As Boolean, ByVal valModulo As enum_ComprobanteGeneradoPor)
   On Error GoTo h_ERROR
   refCumple = False
   If valMostrarMensaje Then
      If valAction = Anular Then
         If gMessage.ConfirmDefaultNO("La Fecha del documento corresponde a un Mes que está CERRADO." _
            & vbCrLf & "Si desea continuar, se podrá generar un comprobante con una fecha posterior a la fecha del mes cerrado o con la fecha del día de hoy." _
            & vbCrLf & vbCrLf & "¿Desea continuar con la Anulación?") Then
            refCumple = True
         End If
      ElseIf valAction = Devolver And valModulo = eCG_ANTICIPO Then
         If gMessage.ConfirmDefaultNO("La Fecha del documento corresponde a un Mes que está CERRADO." _
            & vbCrLf & "Si desea continuar, se podrá generar un comprobante con una fecha posterior a la fecha del mes cerrado o con la fecha del día de hoy." _
            & vbCrLf & vbCrLf & "¿Desea continuar con la Devolución?") Then
            refCumple = True
         End If
      Else
         gMessage.Advertencia "No se puede ejecutar la acción " & gDefgen.AccionSobreRecordStr(valAction) & ". La Fecha corresponde a un Mes Cerrado."
      End If
   End If
h_EXIT:      On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fMostrarMensaje", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fLaAccionRequiereUnPeriodoYMesAbierto(ByVal valAction As AccionSobreRecord, Optional ByVal valGeneradoPor As enum_ComprobanteGeneradoPor = eCG_USUARIO) As Boolean
   Dim varRequiere As Boolean
   On Error GoTo h_ERROR
   fLaAccionRequiereUnPeriodoYMesAbierto = True
   If gProyCompaniaActual.GetUsaModuloDeContabilidad Then
      Select Case valAction
         Case consultar: varRequiere = False
         Case imprimir: varRequiere = False
         Case Exportar: varRequiere = False
         Case reimprimir: varRequiere = False
         Case InformesPantalla: varRequiere = False
         Case ReImprimirRetencion: varRequiere = False
         Case ReImprimirRetencionIVA: varRequiere = False
         Case Actualizar: varRequiere = False
         Case Abrir: varRequiere = False
         Case Cerrar: varRequiere = False
         Case Anular:
            varRequiere = True
            If valGeneradoPor = eCG_COBRANZA Or valGeneradoPor = eCG_PAGOS Then
               varRequiere = False
            End If
         Case Devolver:
            varRequiere = True
            If valGeneradoPor = eCG_ANTICIPO Then
               varRequiere = False
            End If
         Case Else
            varRequiere = True
      End Select
   Else
      varRequiere = False
   End If
h_EXIT:
   fLaAccionRequiereUnPeriodoYMesAbierto = varRequiere
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLaAccionRequiereUnPeriodoYMesAbierto", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sRestaurarDatos(ByVal valRecordSet As ADODB.Recordset, ByVal valConsecutivoCompaniaOld As Long, ByVal valConsecutivoCompaniaNew As Long)
   Dim insUtAdoCmd As clsUtilAdoCmd
   Dim vCmd As ADODB.Command
   On Error GoTo h_ERROR
   Set insUtAdoCmd = New clsUtilAdoCmd
   valRecordSet.filter = "ConsecutivoCompania = " & valConsecutivoCompaniaOld
   While Not valRecordSet.EOF
      Set vCmd = insUtAdoCmd.fCreateCmd("Saw.Gp_ReglasDeContabilizacionINS", gDefDatabase.Conexion)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterInteger("@ConsecutivoCompania", valConsecutivoCompaniaNew)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@Numero", 11, valRecordSet("Numero").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@DiferenciaEnCambioyCalculo", 30, valRecordSet("DiferenciaEnCambioyCalculo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaIva1Credito", 30, valRecordSet("CuentaIva1Credito").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaIva1Debito", 30, valRecordSet("CuentaIva1Debito").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@DondeContabilizarRetIva", 1, valRecordSet("DondeContabilizarRetIva").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaRetencionIva", 30, valRecordSet("CuentaRetencionIva").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CxCTipoComprobante", 2, valRecordSet("CxCTipoComprobante").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("CuentaDiferenciaCambiaria", 30, valRecordSet("CuentaDiferenciaCambiaria").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoContabilizacionCxC", 1, valRecordSet("TipoContabilizacionCxC").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualCxc", 1, valRecordSet("ContabIndividualCxc").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabPorLoteCxC", 1, valRecordSet("ContabPorLoteCxC").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCxCClientes", 30, valRecordSet("CuentaCxCClientes").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCxCIngresos", 30, valRecordSet("CuentaCxCIngresos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CxPTipoComprobante", 2, valRecordSet("CxPTipoComprobante").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoContabilizacionCxP", 1, valRecordSet("TipoContabilizacionCxP").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualCxP", 1, valRecordSet("ContabIndividualCxP").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabPorLoteCxP", 1, valRecordSet("ContabPorLoteCxP").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCxPGasto", 30, valRecordSet("CuentaCxPGasto").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCxPProveedores", 30, valRecordSet("CuentaCxPProveedores").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaRetencionImpuestoMunicipal", 30, valRecordSet("CuentaRetencionImpuestoMunicipal").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CobranzaTipoComprobante", 2, valRecordSet("CobranzaTipoComprobante").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoContabilizacionCobranza", 1, valRecordSet("TipoContabilizacionCobranza").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualCobranza", 1, valRecordSet("ContabIndividualCobranza").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabPorLoteCobranza", 1, valRecordSet("ContabPorLoteCobranza").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCobranzaCobradoEnEfectivo", 30, valRecordSet("CuentaCobranzaCobradoEnEfectivo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCobranzaCobradoEnCheque", 30, valRecordSet("CuentaCobranzaCobradoEnCheque").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCobranzaCobradoEnTarjeta", 30, valRecordSet("CuentaCobranzaCobradoEnTarjeta").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@cuentaCobranzaRetencionISLR", 30, valRecordSet("cuentaCobranzaRetencionISLR").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@cuentaCobranzaRetencionIVA", 30, valRecordSet("cuentaCobranzaRetencionIVA").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCobranzaOtros", 30, valRecordSet("CuentaCobranzaOtros").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCobranzaCxCClientes", 30, valRecordSet("CuentaCobranzaCxCClientes").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCobranzaCobradoAnticipo", 30, valRecordSet("CuentaCobranzaCobradoAnticipo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCobranzaCobradoIvaDiferido", 30, valRecordSet("CuentaCobranzaIvaDiferido").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@PagoTipoComprobante", 2, valRecordSet("PagoTipoComprobante").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterBooleanSN("ManejarDiferenciaCambiariaEnCobranza", gConvert.ConvertStringToBoolean(valRecordSet("ManejarDiferenciaCambiariaEnCobranza").value))
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoContabilizacionPagos", 1, valRecordSet("TipoContabilizacionPagos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualPagos", 1, valRecordSet("ContabIndividualPagos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabPorLotePagos", 1, valRecordSet("ContabPorLotePagos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaPagosCxPProveedores", 30, valRecordSet("CuentaPagosCxPProveedores").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaPagosRetencionISLR", 30, valRecordSet("CuentaPagosRetencionISLR").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaPagosOtros", 30, valRecordSet("CuentaPagosOtros").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaPagosBanco", 30, valRecordSet("CuentaPagosBanco").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaPagosPagadoAnticipo", 30, valRecordSet("CuentaPagosPagadoAnticipo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterBooleanSN("ManejarDiferenciaCambiariaEnPagos", gConvert.ConvertStringToBoolean(valRecordSet("ManejarDiferenciaCambiariaEnPagos").value))
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoContabilizacionFacturacion", 1, valRecordSet("TipoContabilizacionFacturacion").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualFacturacion", 1, valRecordSet("ContabIndividualFacturacion").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabPorLoteFacturacion", 1, valRecordSet("ContabPorLoteFacturacion").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaFacturacionCxCClientes", 30, valRecordSet("CuentaFacturacionCxCClientes").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaFacturacionMontoTotalFactura", 30, valRecordSet("CuentaFacturacionMontoTotalFactura").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaFacturacionCargos", 30, valRecordSet("CuentaFacturacionCargos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaFacturacionDescuentos", 30, valRecordSet("CuentaFacturacionDescuentos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaFacturacionIvaDiferido", 30, valRecordSet("CuentaFacturacionIvaDiferido").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabilizarPorArticulo", 1, valRecordSet("ContabilizarPorArticulo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@AgruparPorCuentaDeArticulo", 1, valRecordSet("AgruparPorCuentaDeArticulo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@AgruparPorCargosDescuentos", 1, valRecordSet("AgruparPorCargosDescuentos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@FacturaTipoComprobante", 2, valRecordSet("FacturaTipoComprobante").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoContabilizacionRDVtas", 1, valRecordSet("TipoContabilizacionRDVtas").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualRDVtas", 1, valRecordSet("ContabIndividualRDVtas").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabPorLoteRDVtas", 1, valRecordSet("ContabPorLoteRDVtas").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaRDVtasCaja", 30, valRecordSet("CuentaRDVtasCaja").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaRDVtasMontoTotal", 30, valRecordSet("CuentaRDVtasMontoTotal").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabilizarPorArticuloRDVtas", 1, valRecordSet("ContabilizarPorArticuloRDVtas").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@AgruparPorCuentaDeArticuloRDVtas", 1, valRecordSet("AgruparPorCuentaDeArticuloRDVtas").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@MovimientoBancarioTipoComprobante", 2, valRecordSet("MovimientoBancarioTipoComprobante").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoContabilizacionMovBancario", 1, valRecordSet("TipoContabilizacionMovBancario").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualMovBancario", 1, valRecordSet("ContabIndividualMovBancario").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabPorLoteMovBancario", 1, valRecordSet("ContabPorLoteMovBancario").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaMovBancarioGasto", 30, valRecordSet("CuentaMovBancarioGasto").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaMovBancarioBancosHaber", 30, valRecordSet("CuentaMovBancarioBancosHaber").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaMovBancarioBancosDebe", 30, valRecordSet("CuentaMovBancarioBancosDebe").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaMovBancarioIngresos", 30, valRecordSet("CuentaMovBancarioIngresos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaDebitoBancarioGasto", 30, valRecordSet("CuentaDebitoBancarioGasto").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaDebitoBancarioBancos", 30, valRecordSet("CuentaDebitoBancarioBancos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCreditoBancarioGasto", 30, valRecordSet("CuentaCreditoBancarioGasto").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCreditoBancarioBancos", 30, valRecordSet("CuentaCreditoBancarioBancos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@AnticipoTipoComprobante", 2, valRecordSet("AnticipoTipoComprobante").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoContabilizacionAnticipo", 1, valRecordSet("TipoContabilizacionAnticipo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualAnticipo", 1, valRecordSet("ContabIndividualAnticipo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabPorLoteAnticipo", 1, valRecordSet("ContabPorLoteAnticipo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaAnticipoCaja", 30, valRecordSet("CuentaAnticipoCaja").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaAnticipoCobrado", 30, valRecordSet("CuentaAnticipoCobrado").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaAnticipoOtrosIngresos", 30, valRecordSet("CuentaAnticipoOtrosIngresos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaAnticipoPagado", 30, valRecordSet("CuentaAnticipoPagado").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaAnticipoBanco", 30, valRecordSet("CuentaAnticipoBanco").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaAnticipoOtrosEgresos", 30, valRecordSet("CuentaAnticipoOtrosEgresos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCostoDeVenta", 30, valRecordSet("CuentaCostoDeVenta").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaInventario", 30, valRecordSet("CuentaInventario").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoContabilizacionInventario", 1, valRecordSet("TipoContabilizacionInventario").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@AgruparPorCuentaDeArticuloInven", 1, valRecordSet("AgruparPorCuentaDeArticuloInven").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@InventarioTipoComprobante", 2, valRecordSet("InventarioTipoComprobante").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CtaDePagosSueldos", 30, valRecordSet("CtaDePagosSueldos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CtaDePagosSueldosBanco", 30, valRecordSet("CtaDePagosSueldosBanco").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualPagosSueldos", 1, valRecordSet("ContabIndividualPagosSueldos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@PagosSueldosTipoComprobante", 2, valRecordSet("PagosSueldosTipoComprobante").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoContabilizacionDePagosSueldos", 1, valRecordSet("TipoContabilizacionDePagosSueldos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteDePagosSueldos", 1, valRecordSet("EditarComprobanteDePagosSueldos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertCxC", 1, valRecordSet("EditarComprobanteAfterInsertCxC").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertCxP", 1, valRecordSet("EditarComprobanteAfterInsertCxP").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertCobranza", 1, valRecordSet("EditarComprobanteAfterInsertCobranza").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertPagos", 1, valRecordSet("EditarComprobanteAfterInsertPagos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertFactura", 1, valRecordSet("EditarComprobanteAfterInsertFactura").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertResDia", 1, valRecordSet("EditarComprobanteAfterInsertResDia").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertMovBan", 1, valRecordSet("EditarComprobanteAfterInsertMovBan").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertImpTraBan", 1, valRecordSet("EditarComprobanteAfterInsertImpTraBan").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertAnticipo", 1, valRecordSet("EditarComprobanteAfterInsertAnticipo").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertInventario", 1, valRecordSet("EditarComprobanteAfterInsertInventario").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertCajaChica", 1, valRecordSet("EditarComprobanteAfterInsertCajaChica").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@SiglasTipoComprobanteCajaChica", 2, valRecordSet("SiglasTipoComprobanteCajaChica").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualCajaChica", 1, valRecordSet("ContabIndividualCajaChica").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCajaChicaGasto", 30, valRecordSet("CuentaCajaChicaGasto").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@MostrarDesglosadoCajaChica", 1, valRecordSet("MostrarDesglosadoCajaChica").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCajaChicaBancoHaber", 30, valRecordSet("CuentaCajaChicaBancoHaber").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCajaChicaBancoDebe", 30, valRecordSet("CuentaCajaChicaBancoDebe").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaCajaChicaBanco", 30, valRecordSet("CuentaCajaChicaBanco").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@SiglasTipoComprobanteRendiciones", 2, valRecordSet("SiglasTipoComprobanteRendiciones").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualRendiciones", 1, valRecordSet("ContabIndividualRendiciones").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaRendicionesGasto", 30, valRecordSet("CuentaRendicionesGasto").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaRendicionesBanco", 30, valRecordSet("CuentaRendicionesBanco").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaRendicionesAnticipos", 30, valRecordSet("CuentaRendicionesAnticipos").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@MostrarDesglosadoRendiciones", 1, valRecordSet("MostrarDesglosadoRendiciones").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoContabilizacionTransfCtas", 1, valRecordSet("TipoContabilizacionTransfCtas").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualTransfCtas", 1, valRecordSet("ContabIndividualTransfCtas").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabPorLoteTransfCtas", 1, valRecordSet("ContabPorLoteTransfCtas").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaTransfCtasBancoDestino", 30, valRecordSet("CuentaTransfCtasBancoDestino").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaTransfCtasGastoComOrigen", 30, valRecordSet("CuentaTransfCtasGastoComOrigen").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaTransfCtasGastoComDestino", 30, valRecordSet("CuentaTransfCtasGastoComDestino").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaTransfCtasBancoOrigen", 30, valRecordSet("CuentaTransfCtasBancoOrigen").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@TransfCtasSigasTipoComprobante", 2, valRecordSet("TransfCtasSigasTipoComprobante").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertTransfCtas", 1, valRecordSet("EditarComprobanteAfterInsertTransfCtas").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@TipoContabilizacionOrdenDeProduccion", 1, valRecordSet("TipoContabilizacionOrdenDeProduccion").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabIndividualOrdenDeProduccion", 1, valRecordSet("ContabIndividualOrdenDeProduccion").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@ContabPorLoteOrdenDeProduccion", 1, valRecordSet("ContabPorLoteOrdenDeProduccion").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaOrdenDeProduccionProductoTerminado", 30, valRecordSet("CuentaOrdenDeProduccionProductoTerminado").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@CuentaOrdenDeProduccionMateriaPrima", 30, valRecordSet("CuentaOrdenDeProduccionMateriaPrima").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@OrdenDeProduccionTipoComprobante", 2, valRecordSet("OrdenDeProduccionTipoComprobante").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterChar("@EditarComprobanteAfterInsertOrdenDeProduccion", 1, valRecordSet("EditarComprobanteAfterInsertOrdenDeProduccion").value)
'revisar
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterString("@NombreOperador", 10, valRecordSet("NombreOperador").value)
      vCmd.Parameters.Append insUtAdoCmd.fCreateInputParameterDate("@FechaUltimaModificacion", valRecordSet("FechaUltimaModificacion").value)
      insUtAdoCmd.fExecCmd vCmd
      Set vCmd = Nothing
      gUtilRespaldo.sAddProgress
      valRecordSet.MoveNext
   Wend
   valRecordSet.filter = adFilterNone
   Set insUtAdoCmd = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sRestaurarDatos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInsertarReglas(ByVal valRecordSet As ADODB.Recordset, ByVal valConsecutivoCompania As Long, ByVal valConsecutivoCompaniaDestino As Long)
   Dim objWrp As Object
   Dim vNumero As String
   On Error GoTo h_ERROR
   vNumero = valRecordSet("Numero").value
   Set objWrp = gUtilWrp.fCreateWrpForModule(gUtilWrp.fWrpClassName("Contabilizacion", "wrpReglasDeContabilizacion"), gProyUsuarioActual.GetNombreDelUsuario, gProyUsuarioActual.GetPassword)
   objWrp.CopiarReglasDeContabilizacion gConvert.fConvierteAString(valConsecutivoCompania), vNumero, gConvert.fConvierteAString(valConsecutivoCompaniaDestino)
   Set objWrp = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInsertarReglas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function SQLSearchByFieldNumericAndConsecutivo(ByVal valTableName As String, ByVal valConsecutivoCompania As Long) As String
   On Error GoTo h_ERROR
  SQLSearchByFieldNumericAndConsecutivo = "SELECT * FROM " & valTableName & " " & gUtilSQL.getWhereSQL(gUtilSQL.fSQLNumberValueWithAnd("", "ConsecutivoCompania", valConsecutivoCompania))
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQLSearchByFieldNumericAndConsecutivo = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "SQLSearchByFieldNumericAndConsecutivo", CM_FILE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function sCopiarReglasDeContabilizacion(ByVal valConsecutivoCompania As Long, ByVal valConsecutivoCompaniaEscojida As Long) As Boolean
   Dim mrsRecord As ADODB.Recordset
   On Error GoTo h_ERROR
   sCopiarReglasDeContabilizacion = False
   Set mrsRecord = New ADODB.Recordset
   If gDbUtil.fOpenRecordSetAllParameters(mrsRecord, SQLSearchByFieldNumericAndConsecutivo("Saw.ReglasDeContabilizacion", valConsecutivoCompaniaEscojida), gDefDatabase.Conexion, adLockOptimistic, adUseClient, adOpenStatic) Then
      If mrsRecord.RecordCount > 0 Then
         sInsertarReglas mrsRecord, valConsecutivoCompaniaEscojida, valConsecutivoCompania
      End If
   End If
   gDbUtil.sDestroyRecordSet mrsRecord
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: sCopiarReglasDeContabilizacion = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCopiarReglasDeContabilizacion", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fExistCodigoCuentaInAnyFieldOfReglas(ByVal valCodigoCuentaContable As String) As Boolean
   Dim Existe As Boolean
   Dim varField As ADODB.Field
   Dim longitudCuenta As Long
   On Error GoTo h_ERROR
   longitudCuenta = 30
   Existe = False
   If Not mrsReglasDeContabilizacion Is Nothing And gDbUtil.fRecordCount(mrsReglasDeContabilizacion) > 0 Then
      For Each varField In mrsReglasDeContabilizacion.Fields
         If varField.Type = adVarChar Then
            If varField.Name = "Numero" Or varField.Name = "NombreOperador" Or varField.DefinedSize < longitudCuenta Then
            Else
               If varField.value = valCodigoCuentaContable Then
                  Existe = True
                  Exit For
               End If
            End If
         End If
      Next varField
   End If
h_EXIT: On Error GoTo 0
   fExistCodigoCuentaInAnyFieldOfReglas = Existe
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fExistCodigoCuentaInAnyFieldOfReglas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fLasCuentasDePagoSueldoEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = (GetCtaDePagosSueldosBanco() <> "" And GetCtaDePagosSueldos() <> "")
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      EstanCompletas = EstanCompletas And GetPagosSueldosTipoComprobante <> ""
   End If
h_EXIT:
   fLasCuentasDePagoSueldoEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "ffLasCuentasDePagoSueldoEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fEditaComprobanteDespuesDeInsertar(ByVal valModulo As enum_ComprobanteGeneradoPor) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   fEditaComprobanteDespuesDeInsertar = False
   Select Case valModulo
      Case eCG_CXC: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertCxC
      Case eCG_CXP: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertCxP
      Case eCG_COBRANZA: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertCobranza
      Case eCG_PAGOS: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertPagos
      Case eCG_FACTURA: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertFactura
      Case eCG_RESUMEN_DIARIO_VENTAS: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertResDia
      Case eCG_MOVIMIENTO_BANCARIO: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertMovBan
      Case eCG_IMPORTADO: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertImpTraBan
      Case eCG_ANTICIPO: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertAnticipo
      Case eCG_INVENTARIO: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertInventario
      Case eCG_PAGO_SUELDOS: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteDePagosSueldos
      Case eCG_REPOSICION: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertCajaChica
      Case eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertTransfCtas
      Case eCG_ORDEN_DE_PRODUCCION: fEditaComprobanteDespuesDeInsertar = gProyReglasDeContabilizacion.GetEditarComprobanteAfterInsertOrdenDeProduccion
   End Select
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEditaComprobanteDespuesDeInsertar", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Sub sSetEditaComprobanteDespuesDeInsertar(ByVal valModulo As enum_ComprobanteGeneradoPor, ByVal valEdita As Boolean)
   On Error GoTo h_ERROR
   Select Case valModulo
      Case eCG_CXC: gProyReglasDeContabilizacion.SetEditarComprobanteAfterInsertCxC valEdita
      Case eCG_CXP: gProyReglasDeContabilizacion.SetEditarComprobanteAfterInsertCxP valEdita
      Case eCG_COBRANZA: gProyReglasDeContabilizacion.SetEditarComprobanteAfterInsertCobranza valEdita
      Case eCG_PAGOS: gProyReglasDeContabilizacion.SetEditarComprobanteAfterInsertPagos valEdita
      Case eCG_FACTURA: gProyReglasDeContabilizacion.SetEditarComprobanteAfterInsertFactura valEdita
      Case eCG_RESUMEN_DIARIO_VENTAS: gProyReglasDeContabilizacion.SetEditarComprobanteAfterInsertResDia valEdita
      Case eCG_MOVIMIENTO_BANCARIO: gProyReglasDeContabilizacion.SetEditarComprobanteAfterInsertMovBan valEdita
      Case eCG_IMPORTADO: gProyReglasDeContabilizacion.SetEditarComprobanteAfterInsertImpTraBan valEdita
      Case eCG_ANTICIPO: gProyReglasDeContabilizacion.SetEditarComprobanteAfterInsertAnticipo valEdita
      Case eCG_INVENTARIO: gProyReglasDeContabilizacion.SetEditarComprobanteAfterInsertInventario valEdita
      Case eCG_PAGO_SUELDOS: gProyReglasDeContabilizacion.SetEditarComprobanteDePagosSueldos valEdita
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sSetEditaComprobanteDespuesDeInsertar", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fLasCuentasDeReposicionEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = (Replace(GetCuentaCajaChicaGasto(), " ", "") <> "" And Replace(GetCuentaCajaChicaBanco(), " ", "") <> "" And Replace(GetCuentaCajaChicaBancoDebe(), " ", "") <> "" And Replace(GetCuentaCajaChicaBancoHaber(), " ", "") <> "" And Replace(GetCuentaPagosCxPProveedores(), " ", "") <> "")
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      EstanCompletas = EstanCompletas And GetSiglasTipoComprobanteCajaChica <> ""
   End If
h_EXIT:
   fLasCuentasDeReposicionEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeReposicionEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGetListaCuentasAVerificar() As String
   Dim vCuentasAVerificar As String
   On Error GoTo h_ERROR
   'Iva
   vCuentasAVerificar = GetCuentaIva1Credito
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaIva1Debito
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaRetencionIVA
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaFacturacionIvaDiferido
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & getCuentaCobranzaIvaDiferido
   'Debito Bancario
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaDebitoBancarioBancos
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaDebitoBancarioGasto
   'cxC
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaCxCClientes
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaCxCIngresos
   'CxP
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaCxPGasto
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaCxPProveedores
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaRetencionImpuestoMunicipal
   'Cobranza
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaCobranzaCobradoEnEfectivo
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaCobranzaCobradoEnCheque
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaCobranzaCobradoEnTarjeta
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaCobranzaCobradoAnticipo
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaCobranzaRetencionISLR
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaCobranzaRetencionIVA
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaCobranzaOtros
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaCobranzaCxCClientes
   'Pagos
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaPagosCxPProveedores
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaPagosRetencionISLR
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaPagosOtros
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaPagosBanco
   'Facturacion
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaFacturacionCxCClientes
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaFacturacionMontoTotalFactura
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaFacturacionCargos
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaFacturacionDescuentos
   'Mov Bancario
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaMovBancarioGasto
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaMovBancarioBancosHaber
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaMovBancarioBancosDebe
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaMovBancarioIngresos
   'Anticipo
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaAnticipoBanco
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaAnticipoCaja
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaAnticipoCobrado
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaAnticipoPagado
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaAnticipoOtrosIngresos
   vCuentasAVerificar = vCuentasAVerificar & gTexto.fSeparadorStandardDeElementosString & GetCuentaAnticipoOtrosEgresos

h_EXIT:
   fGetListaCuentasAVerificar = vCuentasAVerificar
On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGetListaCuentasAVerificar", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sBuscarCuentaUsaAuxiliar(ByVal valXmlCuentas As String)
   Dim vDOM As MSXML2.DOMDocument
   Dim insLibWincont As clsLibWincont
   On Error GoTo h_ERROR
   Set vDOM = New MSXML2.DOMDocument
   Set insLibWincont = New clsLibWincont
   vDOM.LoadXML valXmlCuentas
   'Iva
   SetCuentaIva1CreditoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaIva1Credito)
   SetCuentaIva1DebitoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaIva1Debito)
   SetCuentaRetencionIvaUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaRetencionIVA)
   SetCuentaFacturacionIvaDiferidoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaFacturacionIvaDiferido)
   SetCuentaCobranzaIvaDiferidoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", getCuentaCobranzaIvaDiferido)
   'Debito Bancario
   setCuentaDebitoBancarioBancosUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaDebitoBancarioBancos)
   setCuentaDebitoBancarioGastoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaDebitoBancarioGasto)
   'cxC
   SetCuentaCxCClientesUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaCxCClientes)
   SetCuentaCxCIngresosUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaCxCIngresos)
   'CxP
   SetCuentaCxPGastoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaCxPGasto)
   SetCuentaCxPProveedoresUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaCxPProveedores)
   SetCuentaRetencionImpuestoMunicipalUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaRetencionImpuestoMunicipal)
   'Cobranza
   setCuentaCobranzaCobradoEnEfectivoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaCobranzaCobradoEnEfectivo)
   setCuentaCobranzaCobradoEnChequeUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaCobranzaCobradoEnCheque)
   setCuentaCobranzaCobradoEnTarjetaUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaCobranzaCobradoEnTarjeta)
   setCuentaCobranzaCobradoAnticipoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaCobranzaCobradoAnticipo)
   setCuentaCobranzaRetencionISLRUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaCobranzaRetencionISLR)
   setCuentaCobranzaRetencionIVAUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaCobranzaRetencionIVA)
   setCuentaCobranzaOtrosUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaCobranzaOtros)
   setCuentaCobranzaCxCClientesUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaCobranzaCxCClientes)
   'Pagos
   setCuentaPagosCxPProveedoresUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaPagosCxPProveedores)
   setCuentaPagosOtrosUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaPagosOtros)
   setCuentaPagosBancoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaPagosBanco)
   'Facturacion
   setCuentaFacturacionCxCClientesUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaFacturacionCxCClientes)
   setCuentaFacturacionCargosUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaFacturacionCargos)
   setCuentaFacturacionDescuentosUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaFacturacionDescuentos)
   'Mov Bancario
   setCuentaMovBancarioGastoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaMovBancarioGasto)
   setCuentaMovBancarioBancosHaberUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaMovBancarioBancosHaber)
   setCuentaMovBancarioBancosDebeUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaMovBancarioBancosDebe)
   setCuentaMovBancarioIngresosUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaMovBancarioIngresos)
   'Anticipo
   setCuentaAnticipoBancoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaAnticipoBanco)
   setCuentaAnticipoCajaUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaAnticipoCaja)
   setCuentaAnticipoCobradoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaAnticipoCobrado)
   setCuentaAnticipoPagadoUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaAnticipoPagado)
   SetCuentaAnticipoOtrosIngresosUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaAnticipoOtrosIngresos)
   SetCuentaAnticipoOtrosEgresosUsaAuxiliares insLibWincont.fExisteElValorDelElementoEnElXML(vDOM, "Codigo", GetCuentaAnticipoOtrosEgresos)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sBuscarCuentaUsaAuxiliar", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fLasCuentasDeTransfCtasEstanCompletas() As Boolean
   Dim EstanCompletas As Boolean
   On Error GoTo h_ERROR
   EstanCompletas = GetCuentaTransfCtasBancoDestino <> ""
   EstanCompletas = EstanCompletas And GetCuentaTransfCtasBancoOrigen <> ""
   EstanCompletas = EstanCompletas And GetCuentaTransfCtasGastoComDestino <> ""
   EstanCompletas = EstanCompletas And GetCuentaTransfCtasGastoComOrigen <> ""
   If (gProyParametrosCompania.GetManejaDebitoBancario) Then
      EstanCompletas = EstanCompletas And fLasCuentasDeDebitoBancarioEstanCompletas()
   End If
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      EstanCompletas = EstanCompletas And GetTransfCtasSigasTipoComprobante <> ""
   End If
h_EXIT:
   fLasCuentasDeTransfCtasEstanCompletas = EstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeTransfCtasEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fLasCuentasDeOrdenDeProduccionEstanCompletas() As Boolean
   Dim vEstanCompletas As Boolean
   On Error GoTo h_ERROR
   vEstanCompletas = GetCuentaOrdenDeProduccionMateriaPrima <> ""
   vEstanCompletas = vEstanCompletas And GetCuentaOrdenDeProduccionProductoTerminado <> ""
   vEstanCompletas = vEstanCompletas And GetCuentaMermaAnormal <> ""
   If gContPeriodoActual.GetTipoDeNumeracionAsEnum = eTN_TIPOMESCONSECUTIVO Then
      vEstanCompletas = vEstanCompletas And GetOrdenDeProduccionTipoComprobante <> ""
   End If
h_EXIT:
   fLasCuentasDeOrdenDeProduccionEstanCompletas = vEstanCompletas
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fLasCuentasDeOrdenDeProduccionEstanCompletas", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
