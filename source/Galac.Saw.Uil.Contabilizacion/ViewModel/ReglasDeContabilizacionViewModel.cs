using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Brl.Contabilizacion;
using Galac.Saw.Ccl.Contabilizacion;
using Galac.Adm.Ccl.CajaChica;
using System.Text;
using Galac.Contab.Ccl.WinCont;
using System.Windows;


namespace Galac.Saw.Uil.Contabilizacion.ViewModel {
    public class ReglasDeContabilizacionViewModel:LibInputViewModelMfc<ReglasDeContabilizacion> {
        #region Constantes
        public const string GeneralPropertyName = "General";
        public const string CuentaIva1CreditoPropertyName = "CuentaIva1Credito";
        public const string CuentaIva1DebitoPropertyName = "CuentaIva1Debito";
        public const string CuentaRetencionIvaPropertyName = "CuentaRetencionIva";
        public const string DondeContabilizarRetIvaPropertyName = "DondeContabilizarRetIva";
        public const string DiferenciaEnCambioyCalculoPropertyName = "DiferenciaEnCambioyCalculo";
        public const string CuentasPorCobrarPropertyName = "CuentasPorCobrar";
        public const string CuentaDiferenciaCambiariaPropertyName = "CuentaDiferenciaCambiaria";
        public const string TipoContabilizacionCxCPropertyName = "TipoContabilizacionCxC";
        public const string ContabIndividualCxCPropertyName = "ContabIndividualCxC";
        public const string ContabPorLoteCxCPropertyName = "ContabPorLoteCxC";
        public const string CuentaCxCClientesPropertyName = "CuentaCxCClientes";
        public const string CuentaCxCIngresosPropertyName = "CuentaCxCIngresos";
        public const string CxCTipoComprobantePropertyName = "CxCTipoComprobante";
        public const string EditarComprobanteAfterInsertCxCPropertyName = "EditarComprobanteAfterInsertCxC";
        public const string CuentasPorPagarPropertyName = "CuentasPorPagar";
        public const string TipoContabilizacionCxPPropertyName = "TipoContabilizacionCxP";
        public const string ContabIndividualCxPPropertyName = "ContabIndividualCxP";
        public const string ContabPorLoteCxPPropertyName = "ContabPorLoteCxP";
        public const string CuentaCxPGastoPropertyName = "CuentaCxPGasto";
        public const string CuentaCxPProveedoresPropertyName = "CuentaCxPProveedores";
        public const string CuentaRetencionImpuestoMunicipalPropertyName = "CuentaRetencionImpuestoMunicipal";
        public const string CxPTipoComprobantePropertyName = "CxPTipoComprobante";
        public const string EditarComprobanteAfterInsertCxPPropertyName = "EditarComprobanteAfterInsertCxP";
        public const string CobranzaPropertyName = "Cobranza";
        public const string TipoContabilizacionCobranzaPropertyName = "TipoContabilizacionCobranza";
        public const string ContabIndividualCobranzaPropertyName = "ContabIndividualCobranza";
        public const string ContabPorLoteCobranzaPropertyName = "ContabPorLoteCobranza";
        public const string CuentaCobranzaCobradoEnEfectivoPropertyName = "CuentaCobranzaCobradoEnEfectivo";
        public const string CuentaCobranzaCobradoEnChequePropertyName = "CuentaCobranzaCobradoEnCheque";
        public const string CuentaCobranzaCobradoEnTarjetaPropertyName = "CuentaCobranzaCobradoEnTarjeta";
        public const string CuentaCobranzaRetencionISLRPropertyName = "CuentaCobranzaRetencionISLR";
        public const string CuentaCobranzaRetencionIVAPropertyName = "CuentaCobranzaRetencionIVA";
        public const string CuentaCobranzaOtrosPropertyName = "CuentaCobranzaOtros";
        public const string CuentaCobranzaCxCClientesPropertyName = "CuentaCobranzaCxCClientes";
        public const string CuentaCobranzaCobradoAnticipoPropertyName = "CuentaCobranzaCobradoAnticipo";
        public const string CuentaCobranzaIvaDiferidoPropertyName = "CuentaCobranzaIvaDiferido";
        public const string CobranzaTipoComprobantePropertyName = "CobranzaTipoComprobante";
        public const string EditarComprobanteAfterInsertCobranzaPropertyName = "EditarComprobanteAfterInsertCobranza";
        public const string ManejarDiferenciaCambiariaEnCobranzaPropertyName = "ManejarDiferenciaCambiariaEnCobranza";
        public const string PagosPropertyName = "Pagos";
        public const string TipoContabilizacionPagosPropertyName = "TipoContabilizacionPagos";
        public const string ContabIndividualPagosPropertyName = "ContabIndividualPagos";
        public const string ContabPorLotePagosPropertyName = "ContabPorLotePagos";
        public const string CuentaPagosCxPProveedoresPropertyName = "CuentaPagosCxPProveedores";
        public const string CuentaPagosRetencionISLRPropertyName = "CuentaPagosRetencionISLR";
        public const string CuentaPagosOtrosPropertyName = "CuentaPagosOtros";
        public const string CuentaPagosBancoPropertyName = "CuentaPagosBanco";
        public const string CuentaPagosPagadoAnticipoPropertyName = "CuentaPagosPagadoAnticipo";
        public const string PagoTipoComprobantePropertyName = "PagoTipoComprobante";
        public const string EditarComprobanteAfterInsertPagosPropertyName = "EditarComprobanteAfterInsertPagos";
        public const string ManejarDiferenciaCambiariaEnPagosPropertyName = "ManejarDiferenciaCambiariaEnPagos";
        public const string FacturaPropertyName = "Factura";
        public const string TipoContabilizacionFacturacionPropertyName = "TipoContabilizacionFacturacion";
        public const string ContabIndividualFacturacionPropertyName = "ContabIndividualFacturacion";
        public const string ContabPorLoteFacturacionPropertyName = "ContabPorLoteFacturacion";
        public const string CuentaFacturacionCxCClientesPropertyName = "CuentaFacturacionCxCClientes";
        public const string CuentaFacturacionMontoTotalFacturaPropertyName = "CuentaFacturacionMontoTotalFactura";
        public const string CuentaFacturacionCargosPropertyName = "CuentaFacturacionCargos";
        public const string CuentaFacturacionDescuentosPropertyName = "CuentaFacturacionDescuentos";
        public const string CuentaFacturacionIvaDiferidoPropertyName = "CuentaFacturacionIvaDiferido";
        public const string ContabilizarPorArticuloPropertyName = "ContabilizarPorArticulo";
        public const string AgruparPorCuentaDeArticuloPropertyName = "AgruparPorCuentaDeArticulo";
        public const string AgruparPorCargosDescuentosPropertyName = "AgruparPorCargosDescuentos";
        public const string FacturaTipoComprobantePropertyName = "FacturaTipoComprobante";
        public const string EditarComprobanteAfterInsertFacturaPropertyName = "EditarComprobanteAfterInsertFactura";
        public const string ResumenDiarioDeVentasPropertyName = "ResumenDiarioDeVentas";
        public const string TipoContabilizacionRDVtasPropertyName = "TipoContabilizacionRDVtas";
        public const string ContabIndividualRDVtasPropertyName = "ContabIndividualRDVtas";
        public const string ContabPorLoteRDVtasPropertyName = "ContabPorLoteRDVtas";
        public const string CuentaRDVtasCajaPropertyName = "CuentaRDVtasCaja";
        public const string CuentaRDVtasMontoTotalPropertyName = "CuentaRDVtasMontoTotal";
        public const string ContabilizarPorArticuloRDVtasPropertyName = "ContabilizarPorArticuloRDVtas";
        public const string AgruparPorCuentaDeArticuloRDVtasPropertyName = "AgruparPorCuentaDeArticuloRDVtas";
        public const string EditarComprobanteAfterInsertResDiaPropertyName = "EditarComprobanteAfterInsertResDia";
        public const string MovimientoBancarioPropertyName = "MovimientoBancario";
        public const string TipoContabilizacionMovBancarioPropertyName = "TipoContabilizacionMovBancario";
        public const string ContabIndividualMovBancarioPropertyName = "ContabIndividualMovBancario";
        public const string ContabPorLoteMovBancarioPropertyName = "ContabPorLoteMovBancario";
        public const string CuentaMovBancarioGastoPropertyName = "CuentaMovBancarioGasto";
        public const string CuentaMovBancarioBancosHaberPropertyName = "CuentaMovBancarioBancosHaber";
        public const string CuentaMovBancarioBancosDebePropertyName = "CuentaMovBancarioBancosDebe";
        public const string CuentaMovBancarioIngresosPropertyName = "CuentaMovBancarioIngresos";
        public const string MovimientoBancarioTipoComprobantePropertyName = "MovimientoBancarioTipoComprobante";
        public const string EditarComprobanteAfterInsertMovBanPropertyName = "EditarComprobanteAfterInsertMovBan";
        public const string ImpuestosALasTransaccionesFinancierasPropertyName = "ImpuestosALasTransaccionesFinancieras";
        public const string CuentaDebitoBancarioGastoPropertyName = "CuentaDebitoBancarioGasto";
        public const string CuentaDebitoBancarioBancosPropertyName = "CuentaDebitoBancarioBancos";
        public const string CuentaCreditoBancarioGastoPropertyName = "CuentaCreditoBancarioGasto";
        public const string CuentaCreditoBancarioBancosPropertyName = "CuentaCreditoBancarioBancos";
        public const string EditarComprobanteAfterInsertImpTraBanPropertyName = "EditarComprobanteAfterInsertImpTraBan";
        public const string AnticipoPropertyName = "Anticipo";
        public const string TipoContabilizacionAnticipoPropertyName = "TipoContabilizacionAnticipo";
        public const string ContabIndividualAnticipoPropertyName = "ContabIndividualAnticipo";
        public const string ContabPorLoteAnticipoPropertyName = "ContabPorLoteAnticipo";
        public const string CuentaAnticipoCajaPropertyName = "CuentaAnticipoCaja";
        public const string CuentaAnticipoCobradoPropertyName = "CuentaAnticipoCobrado";
        public const string CuentaAnticipoOtrosIngresosPropertyName = "CuentaAnticipoOtrosIngresos";
        public const string CuentaAnticipoPagadoPropertyName = "CuentaAnticipoPagado";
        public const string CuentaAnticipoBancoPropertyName = "CuentaAnticipoBanco";
        public const string CuentaAnticipoOtrosEgresosPropertyName = "CuentaAnticipoOtrosEgresos";
        public const string AnticipoTipoComprobantePropertyName = "AnticipoTipoComprobante";
        public const string EditarComprobanteAfterInsertAnticipoPropertyName = "EditarComprobanteAfterInsertAnticipo";
        public const string InventarioPropertyName = "Inventario";
        public const string TipoContabilizacionInventarioPropertyName = "TipoContabilizacionInventario";
        public const string CuentaCostoDeVentaPropertyName = "CuentaCostoDeVenta";
        public const string CuentaInventarioPropertyName = "CuentaInventario";
        public const string AgruparPorCuentaDeArticuloInvenPropertyName = "AgruparPorCuentaDeArticuloInven";
        public const string InventarioTipoComprobantePropertyName = "InventarioTipoComprobante";
        public const string EditarComprobanteAfterInsertInventarioPropertyName = "EditarComprobanteAfterInsertInventario";
        public const string SolicitudDePagoPropertyName = "SolicitudDePago";
        public const string TipoContabilizacionDePagosSueldosPropertyName = "TipoContabilizacionDePagosSueldos";
        public const string ContabIndividualPagosSueldosPropertyName = "ContabIndividualPagosSueldos";
        public const string CtaDePagosSueldosPropertyName = "CtaDePagosSueldos";
        public const string CtaDePagosSueldosBancoPropertyName = "CtaDePagosSueldosBanco";
        public const string PagosSueldosTipoComprobantePropertyName = "PagosSueldosTipoComprobante";
        public const string EditarComprobanteDePagosSueldosPropertyName = "EditarComprobanteDePagosSueldos";
        public const string CajaChicaPropertyName = "CajaChica";
        public const string ContabIndividualCajaChicaPropertyName = "ContabIndividualCajaChica";
        public const string MostrarDesglosadoCajaChicaPropertyName = "MostrarDesglosadoCajaChica";
        public const string CuentaCajaChicaGastoPropertyName = "CuentaCajaChicaGasto";
        public const string CuentaCajaChicaBancoHaberPropertyName = "CuentaCajaChicaBancoHaber";
        public const string CuentaCajaChicaBancoDebePropertyName = "CuentaCajaChicaBancoDebe";
        public const string CuentaCajaChicaBancoPropertyName = "CuentaCajaChicaBanco";
        public const string SiglasTipoComprobanteCajaChicaPropertyName = "SiglasTipoComprobanteCajaChica";
        public const string EditarComprobanteAfterInsertCajaChicaPropertyName = "EditarComprobanteAfterInsertCajaChica";
        public const string RendicionesPropertyName = "Rendiciones";
        public const string ContabIndividualRendicionesPropertyName = "ContabIndividualRendiciones";
        public const string MostrarDesglosadoRendicionesPropertyName = "MostrarDesglosadoRendiciones";
        public const string CuentaRendicionesGastoPropertyName = "CuentaRendicionesGasto";
        public const string CuentaRendicionesBancoPropertyName = "CuentaRendicionesBanco";
        public const string CuentaRendicionesAnticiposPropertyName = "CuentaRendicionesAnticipos";
        public const string SiglasTipoComprobanteRendicionesPropertyName = "SiglasTipoComprobanteRendiciones";
        public const string ContabIndividualTransfCtasPropertyName = "ContabIndividualTransfCtas";
        public const string TipoContabilizacionTransfCtasPropertyName = "TipoContabilizacionTransfCtas";
        public const string CuentaTransfCtasBancoDestinoPropertyName = "CuentaTransfCtasBancoDestino";
        public const string CuentaTransfCtasGastoComOrigenPropertyName = "CuentaTransfCtasGastoComOrigen";
        public const string CuentaTransfCtasGastoComDestinoPropertyName = "CuentaTransfCtasGastoComDestino";
        public const string CuentaTransfCtasBancoOrigenPropertyName = "CuentaTransfCtasBancoOrigen";
        public const string TransfCtasSigasTipoComprobantePropertyName = "TransfCtasSigasTipoComprobante";
        public const string EditarComprobanteAfterInsertTransfCtasPropertyName = "EditarComprobanteAfterInsertTransfCtas";
        public const string TipoContabilizacionOrdenDeProduccionPropertyName = "TipoContabilizacionOrdenDeProduccion";
        public const string ContabIndividualOrdenDeProduccionPropertyName = "ContabIndividualOrdenDeProduccion";
        public const string ContabPorLoteOrdenDeProduccionPropertyName = "ContabPorLoteOrdenDeProduccion";
        public const string CuentaOrdenDeProduccionProductoTerminadoPropertyName = "CuentaOrdenDeProduccionProductoTerminado";
        public const string CuentaOrdenDeProduccionMateriaPrimaPropertyName = "CuentaOrdenDeProduccionMateriaPrima";
        public const string OrdenDeProduccionTipoComprobantePropertyName = "OrdenDeProduccionTipoComprobante";
        public const string EditarComprobanteAfterInsertOrdenDeProduccionPropertyName = "EditarComprobanteAfterInsertOrdenDeProduccion";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        public const string IsEnabledAgruparPorCuentaDeArticuloPropertyName = "IsEnabledAgruparPorCuentaDeArticulo";
        public const string LeyendaFacturaVisiblePropertyName = "LeyendaFacturaVisible";
        public const string CuentaCajaChicaCxPProveedoresPropertyName = "CuentaCajaChicaCxPProveedores";
        public const string CuentaDiferenciaCambiariaDescripcionPropertyName = "CuentaDiferenciaCambiariaDescripcion";

        #endregion
        #region Variables
        private FkCuentaViewModel _ConexionCuentaIva1Credito = null;
        private FkCuentaViewModel _ConexionCuentaIva1Debito = null;
        private FkCuentaViewModel _ConexionCuentaRetencionIva = null;
        private FkCuentaViewModel _ConexionDiferenciaEnCambioyCalculo = null;
        private FkCuentaViewModel _ConexionCuentaDiferenciaCambiaria = null;
        private FkCuentaViewModel _ConexionCuentaCxCClientes = null;
        private FkCuentaViewModel _ConexionCuentaCxCIngresos = null;
        private FkTipoDeComprobanteViewModel _ConexionCxCTipoComprobante = null;
        private FkCuentaViewModel _ConexionCuentaCxPGasto = null;
        private FkCuentaViewModel _ConexionCuentaCxPProveedores = null;
        private FkCuentaViewModel _ConexionCuentaRetencionImpuestoMunicipal = null;
        private FkTipoDeComprobanteViewModel _ConexionCxPTipoComprobante = null;
        private FkCuentaViewModel _ConexionCuentaCobranzaCobradoEnEfectivo = null;
        private FkCuentaViewModel _ConexionCuentaCobranzaCobradoEnCheque = null;
        private FkCuentaViewModel _ConexionCuentaCobranzaCobradoEnTarjeta = null;
        private FkCuentaViewModel _ConexionCuentaCobranzaRetencionISLR = null;
        private FkCuentaViewModel _ConexionCuentaCobranzaRetencionIVA = null;
        private FkCuentaViewModel _ConexionCuentaCobranzaOtros = null;
        private FkCuentaViewModel _ConexionCuentaCobranzaCxCClientes = null;
        private FkCuentaViewModel _ConexionCuentaCobranzaCobradoAnticipo = null;
        private FkCuentaViewModel _ConexionCuentaCobranzaIvaDiferido = null;
        private FkTipoDeComprobanteViewModel _ConexionCobranzaTipoComprobante = null;
        private FkCuentaViewModel _ConexionCuentaPagosCxPProveedores = null;
        private FkCuentaViewModel _ConexionCuentaPagosRetencionISLR = null;
        private FkCuentaViewModel _ConexionCuentaPagosOtros = null;
        private FkCuentaViewModel _ConexionCuentaPagosBanco = null;
        private FkCuentaViewModel _ConexionCuentaPagosPagadoAnticipo = null;
        private FkCuentaViewModel _ConexionCuentaCajaChicaCxPProveedores = null;
        private FkTipoDeComprobanteViewModel _ConexionPagoTipoComprobante = null;
        private FkCuentaViewModel _ConexionCuentaFacturacionCxCClientes = null;
        private FkCuentaViewModel _ConexionCuentaFacturacionMontoTotalFactura = null;
        private FkCuentaViewModel _ConexionCuentaFacturacionCargos = null;
        private FkCuentaViewModel _ConexionCuentaFacturacionDescuentos = null;
        private FkCuentaViewModel _ConexionCuentaFacturacionIvaDiferido = null;
        private FkTipoDeComprobanteViewModel _ConexionFacturaTipoComprobante = null;
        private FkCuentaViewModel _ConexionCuentaRDVtasCaja = null;
        private FkCuentaViewModel _ConexionCuentaRDVtasMontoTotal = null;
        private FkCuentaViewModel _ConexionCuentaMovBancarioGasto = null;
        private FkCuentaViewModel _ConexionCuentaMovBancarioBancosHaber = null;
        private FkCuentaViewModel _ConexionCuentaMovBancarioBancosDebe = null;
        private FkCuentaViewModel _ConexionCuentaMovBancarioIngresos = null;
        private FkTipoDeComprobanteViewModel _ConexionMovimientoBancarioTipoComprobante = null;
        private FkCuentaViewModel _ConexionCuentaDebitoBancarioGasto = null;
        private FkCuentaViewModel _ConexionCuentaDebitoBancarioBancos = null;
        private FkCuentaViewModel _ConexionCuentaCreditoBancarioGasto = null;
        private FkCuentaViewModel _ConexionCuentaCreditoBancarioBancos = null;
        private FkCuentaViewModel _ConexionCuentaAnticipoCaja = null;
        private FkCuentaViewModel _ConexionCuentaAnticipoCobrado = null;
        private FkCuentaViewModel _ConexionCuentaAnticipoOtrosIngresos = null;
        private FkCuentaViewModel _ConexionCuentaAnticipoPagado = null;
        private FkCuentaViewModel _ConexionCuentaAnticipoBanco = null;
        private FkCuentaViewModel _ConexionCuentaAnticipoOtrosEgresos = null;
        private FkTipoDeComprobanteViewModel _ConexionAnticipoTipoComprobante = null;
        private FkCuentaViewModel _ConexionCuentaCostoDeVenta = null;
        private FkCuentaViewModel _ConexionCuentaInventario = null;
        private FkTipoDeComprobanteViewModel _ConexionInventarioTipoComprobante = null;
        private FkCuentaViewModel _ConexionCtaDePagosSueldos = null;
        private FkCuentaViewModel _ConexionCtaDePagosSueldosBanco = null;
        private FkTipoDeComprobanteViewModel _ConexionPagosSueldosTipoComprobante = null;
        private FkCuentaViewModel _ConexionCuentaCajaChicaGasto = null;
        private FkCuentaViewModel _ConexionCuentaCajaChicaBancoHaber = null;
        private FkCuentaViewModel _ConexionCuentaCajaChicaBancoDebe = null;
        private FkCuentaViewModel _ConexionCuentaCajaChicaBanco = null;
        private FkTipoDeComprobanteViewModel _ConexionSiglasTipoComprobanteCajaChica = null;
        private FkCuentaViewModel _ConexionCuentaRendicionesGasto = null;
        private FkCuentaViewModel _ConexionCuentaRendicionesBanco = null;
        private FkCuentaViewModel _ConexionCuentaRendicionesAnticipos = null;
        private FkTipoDeComprobanteViewModel _ConexionSiglasTipoComprobanteRendiciones = null;
        private FkCuentaViewModel _ConexionCuentaTransfCtasBancoDestino = null;
        private FkCuentaViewModel _ConexionCuentaTransfCtasGastoComOrigen = null;
        private FkCuentaViewModel _ConexionCuentaTransfCtasGastoComDestino = null;
        private FkCuentaViewModel _ConexionCuentaTransfCtasBancoOrigen = null;
        private FkTipoDeComprobanteViewModel _ConexionTransfCtasSigasTipoComprobante = null;
        private FkCuentaViewModel _ConexionCuentaOrdenDeProduccionProductoTerminado = null;
        private FkCuentaViewModel _ConexionCuentaOrdenDeProduccionMateriaPrima = null;
        private FkTipoDeComprobanteViewModel _ConexionOrdenDeProduccionTipoComprobante = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Reglas de Contabilización"; }
        }

        public int ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if(Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        public string Numero {
            get {
                return Model.Numero;
            }
            set {
                if(Model.Numero != value) {
                    Model.Numero = value;
                }
            }
        }

        public string CuentaIva1Credito {
            get {
                return Model.CuentaIva1Credito;
            }
            set {
                if(Model.CuentaIva1Credito != value) {
                    Model.CuentaIva1Credito = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaIva1CreditoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaIva1Credito,true)) {
                        ConexionCuentaIva1Credito = null;
                    }
                }
            }
        }

        public string CuentaIva1Debito {
            get {
                return Model.CuentaIva1Debito;
            }
            set {
                if(Model.CuentaIva1Debito != value) {
                    Model.CuentaIva1Debito = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaIva1DebitoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaIva1Debito,true)) {
                        ConexionCuentaIva1Debito = null;
                    }
                }
            }
        }

        public string CuentaRetencionIva {
            get {
                return Model.CuentaRetencionIva;
            }
            set {
                if(Model.CuentaRetencionIva != value) {
                    Model.CuentaRetencionIva = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaRetencionIvaPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaRetencionIva,true)) {
                        ConexionCuentaRetencionIva = null;
                    }
                }
            }
        }

        public eDondeEfectuoContabilizacionRetIVA DondeContabilizarRetIva {
            get {
                return Model.DondeContabilizarRetIvaAsEnum;
            }
            set {
                if(Model.DondeContabilizarRetIvaAsEnum != value) {
                    Model.DondeContabilizarRetIvaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(DondeContabilizarRetIvaPropertyName);
                }
            }
        }

        public string DiferenciaEnCambioyCalculo {
            get {
                return Model.DiferenciaEnCambioyCalculo;
            }
            set {
                if(Model.DiferenciaEnCambioyCalculo != value) {
                    Model.DiferenciaEnCambioyCalculo = value;
                    IsDirty = true;
                    RaisePropertyChanged(DiferenciaEnCambioyCalculoPropertyName);
                    if(LibString.IsNullOrEmpty(DiferenciaEnCambioyCalculo,true)) {
                        ConexionDiferenciaEnCambioyCalculo = null;
                    }
                }
            }
        }

        [LibCustomValidation("CuentaDiferenciaCambiariaValidating")]
        public string CuentaDiferenciaCambiaria {
            get {
                return Model.CuentaDiferenciaCambiaria;
            }
            set {
                if(Model.CuentaDiferenciaCambiaria != value) {
                    Model.CuentaDiferenciaCambiaria = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaDiferenciaCambiariaPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaDiferenciaCambiaria,true)) {
                        ConexionCuentaDiferenciaCambiaria = null;
                    }
                }
            }
        }       

        public eTipoDeContabilizacion TipoContabilizacionCxC {
            get {
                return Model.TipoContabilizacionCxCAsEnum;
            }
            set {
                if(Model.TipoContabilizacionCxCAsEnum != value) {
                    Model.TipoContabilizacionCxCAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoContabilizacionCxCPropertyName);
                }
            }
        }

        public eContabilizacionIndividual ContabIndividualCxC {
            get {
                return Model.ContabIndividualCxcAsEnum;
            }
            set {
                if(Model.ContabIndividualCxcAsEnum != value) {
                    Model.ContabIndividualCxcAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualCxCPropertyName);
                }
            }
        }

        public eContabilizacionPorLote ContabPorLoteCxC {
            get {
                return Model.ContabPorLoteCxCAsEnum;
            }
            set {
                if(Model.ContabPorLoteCxCAsEnum != value) {
                    Model.ContabPorLoteCxCAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabPorLoteCxCPropertyName);
                }
            }
        }

        public string CuentaCxCClientes {
            get {
                return Model.CuentaCxCClientes;
            }
            set {
                if(Model.CuentaCxCClientes != value) {
                    Model.CuentaCxCClientes = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCxCClientesPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCxCClientes,true)) {
                        ConexionCuentaCxCClientes = null;
                    }
                }
            }
        }

        public string CuentaCxCIngresos {
            get {
                return Model.CuentaCxCIngresos;
            }
            set {
                if(Model.CuentaCxCIngresos != value) {
                    Model.CuentaCxCIngresos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCxCIngresosPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCxCIngresos,true)) {
                        ConexionCuentaCxCIngresos = null;
                    }
                }
            }
        }

        public string CxCTipoComprobante {
            get {
                return Model.CxCTipoComprobante;
            }
            set {
                if(Model.CxCTipoComprobante != value) {
                    Model.CxCTipoComprobante = value;
                    IsDirty = true;
                    RaisePropertyChanged(CxCTipoComprobantePropertyName);
                    if(LibString.IsNullOrEmpty(CxCTipoComprobante,true)) {
                        ConexionCxCTipoComprobante = null;
                    }
                }
            }
        }

        public bool EditarComprobanteAfterInsertCxC {
            get {
                return Model.EditarComprobanteAfterInsertCxCAsBool;
            }
            set {
                if(Model.EditarComprobanteAfterInsertCxCAsBool != value) {
                    Model.EditarComprobanteAfterInsertCxCAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertCxCPropertyName);
                }
            }
        }

        public eTipoDeContabilizacion TipoContabilizacionCxP {
            get {
                return Model.TipoContabilizacionCxPAsEnum;
            }
            set {
                if(Model.TipoContabilizacionCxPAsEnum != value) {
                    Model.TipoContabilizacionCxPAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoContabilizacionCxPPropertyName);
                }
            }
        }

        public eContabilizacionIndividual ContabIndividualCxP {
            get {
                return Model.ContabIndividualCxPAsEnum;
            }
            set {
                if(Model.ContabIndividualCxPAsEnum != value) {
                    Model.ContabIndividualCxPAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualCxPPropertyName);
                }
            }
        }

        public eContabilizacionPorLote ContabPorLoteCxP {
            get {
                return Model.ContabPorLoteCxPAsEnum;
            }
            set {
                if(Model.ContabPorLoteCxPAsEnum != value) {
                    Model.ContabPorLoteCxPAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabPorLoteCxPPropertyName);
                }
            }
        }

        public string CuentaCxPGasto {
            get {
                return Model.CuentaCxPGasto;
            }
            set {
                if(Model.CuentaCxPGasto != value) {
                    Model.CuentaCxPGasto = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCxPGastoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCxPGasto,true)) {
                        ConexionCuentaCxPGasto = null;
                    }
                }
            }
        }

        public string CuentaCxPProveedores {
            get {
                return Model.CuentaCxPProveedores;
            }
            set {
                if(Model.CuentaCxPProveedores != value) {
                    Model.CuentaCxPProveedores = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCxPProveedoresPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCxPProveedores,true)) {
                        ConexionCuentaCxPProveedores = null;
                    }
                }
            }
        }

        public string CuentaRetencionImpuestoMunicipal {
            get {
                return Model.CuentaRetencionImpuestoMunicipal;
            }
            set {
                if(Model.CuentaRetencionImpuestoMunicipal != value) {
                    Model.CuentaRetencionImpuestoMunicipal = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaRetencionImpuestoMunicipalPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaRetencionImpuestoMunicipal,true)) {
                        ConexionCuentaRetencionImpuestoMunicipal = null;
                    }
                }
            }
        }

        public string CxPTipoComprobante {
            get {
                return Model.CxPTipoComprobante;
            }
            set {
                if(Model.CxPTipoComprobante != value) {
                    Model.CxPTipoComprobante = value;
                    IsDirty = true;
                    RaisePropertyChanged(CxPTipoComprobantePropertyName);
                    if(LibString.IsNullOrEmpty(CxPTipoComprobante,true)) {
                        ConexionCxPTipoComprobante = null;
                    }
                }
            }
        }

        public bool EditarComprobanteAfterInsertCxP {
            get {
                return Model.EditarComprobanteAfterInsertCxPAsBool;
            }
            set {
                if(Model.EditarComprobanteAfterInsertCxPAsBool != value) {
                    Model.EditarComprobanteAfterInsertCxPAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertCxPPropertyName);
                }
            }
        }

        public eTipoDeContabilizacion TipoContabilizacionCobranza {
            get {
                return Model.TipoContabilizacionCobranzaAsEnum;
            }
            set {
                if(Model.TipoContabilizacionCobranzaAsEnum != value) {
                    Model.TipoContabilizacionCobranzaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoContabilizacionCobranzaPropertyName);
                }
            }
        }

        public eContabilizacionIndividual ContabIndividualCobranza {
            get {
                return Model.ContabIndividualCobranzaAsEnum;
            }
            set {
                if(Model.ContabIndividualCobranzaAsEnum != value) {
                    Model.ContabIndividualCobranzaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualCobranzaPropertyName);
                }
            }
        }

        public eContabilizacionPorLote ContabPorLoteCobranza {
            get {
                return Model.ContabPorLoteCobranzaAsEnum;
            }
            set {
                if(Model.ContabPorLoteCobranzaAsEnum != value) {
                    Model.ContabPorLoteCobranzaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabPorLoteCobranzaPropertyName);
                }
            }
        }

        public string CuentaCobranzaCobradoEnEfectivo {
            get {
                return Model.CuentaCobranzaCobradoEnEfectivo;
            }
            set {
                if(Model.CuentaCobranzaCobradoEnEfectivo != value) {
                    Model.CuentaCobranzaCobradoEnEfectivo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCobranzaCobradoEnEfectivoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCobranzaCobradoEnEfectivo,true)) {
                        ConexionCuentaCobranzaCobradoEnEfectivo = null;
                    }
                }
            }
        }

        public string CuentaCobranzaCobradoEnCheque {
            get {
                return Model.CuentaCobranzaCobradoEnCheque;
            }
            set {
                if(Model.CuentaCobranzaCobradoEnCheque != value) {
                    Model.CuentaCobranzaCobradoEnCheque = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCobranzaCobradoEnChequePropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCobranzaCobradoEnCheque,true)) {
                        ConexionCuentaCobranzaCobradoEnCheque = null;
                    }
                }
            }
        }

        public string CuentaCobranzaCobradoEnTarjeta {
            get {
                return Model.CuentaCobranzaCobradoEnTarjeta;
            }
            set {
                if(Model.CuentaCobranzaCobradoEnTarjeta != value) {
                    Model.CuentaCobranzaCobradoEnTarjeta = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCobranzaCobradoEnTarjetaPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCobranzaCobradoEnTarjeta,true)) {
                        ConexionCuentaCobranzaCobradoEnTarjeta = null;
                    }
                }
            }
        }

        public string CuentaCobranzaRetencionISLR {
            get {
                return Model.cuentaCobranzaRetencionISLR;
            }
            set {
                if(Model.cuentaCobranzaRetencionISLR != value) {
                    Model.cuentaCobranzaRetencionISLR = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCobranzaRetencionISLRPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCobranzaRetencionISLR,true)) {
                        ConexionCuentaCobranzaRetencionISLR = null;
                    }
                }
            }
        }

        public string CuentaCobranzaRetencionIVA {
            get {
                return Model.cuentaCobranzaRetencionIVA;
            }
            set {
                if(Model.cuentaCobranzaRetencionIVA != value) {
                    Model.cuentaCobranzaRetencionIVA = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCobranzaRetencionIVAPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCobranzaRetencionIVA,true)) {
                        ConexionCuentaCobranzaRetencionIVA = null;
                    }
                }
            }
        }

        public string CuentaCobranzaOtros {
            get {
                return Model.CuentaCobranzaOtros;
            }
            set {
                if(Model.CuentaCobranzaOtros != value) {
                    Model.CuentaCobranzaOtros = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCobranzaOtrosPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCobranzaOtros,true)) {
                        ConexionCuentaCobranzaOtros = null;
                    }
                }
            }
        }

        public string CuentaCobranzaCxCClientes {
            get {
                return Model.CuentaCobranzaCxCClientes;
            }
            set {
                if(Model.CuentaCobranzaCxCClientes != value) {
                    Model.CuentaCobranzaCxCClientes = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCobranzaCxCClientesPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCobranzaCxCClientes,true)) {
                        ConexionCuentaCobranzaCxCClientes = null;
                    }
                }
            }
        }

        public string CuentaCobranzaCobradoAnticipo {
            get {
                return Model.CuentaCobranzaCobradoAnticipo;
            }
            set {
                if(Model.CuentaCobranzaCobradoAnticipo != value) {
                    Model.CuentaCobranzaCobradoAnticipo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCobranzaCobradoAnticipoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCobranzaCobradoAnticipo,true)) {
                        ConexionCuentaCobranzaCobradoAnticipo = null;
                    }
                }
            }
        }

        public string CuentaCobranzaIvaDiferido {
            get {
                return Model.CuentaCobranzaIvaDiferido;
            }
            set {
                if(Model.CuentaCobranzaIvaDiferido != value) {
                    Model.CuentaCobranzaIvaDiferido = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCobranzaIvaDiferidoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCobranzaIvaDiferido,true)) {
                        ConexionCuentaCobranzaIvaDiferido = null;
                    }
                }
            }
        }

        public string CobranzaTipoComprobante {
            get {
                return Model.CobranzaTipoComprobante;
            }
            set {
                if(Model.CobranzaTipoComprobante != value) {
                    Model.CobranzaTipoComprobante = value;
                    IsDirty = true;
                    RaisePropertyChanged(CobranzaTipoComprobantePropertyName);
                    if(LibString.IsNullOrEmpty(CobranzaTipoComprobante,true)) {
                        ConexionCobranzaTipoComprobante = null;
                    }
                }
            }
        }

        public bool EditarComprobanteAfterInsertCobranza {
            get {
                return Model.EditarComprobanteAfterInsertCobranzaAsBool;
            }
            set {
                if(Model.EditarComprobanteAfterInsertCobranzaAsBool != value) {
                    Model.EditarComprobanteAfterInsertCobranzaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertCobranzaPropertyName);
                }
            }
        }

        public bool ManejarDiferenciaCambiariaEnCobranza {
            get {
                return Model.ManejarDiferenciaCambiariaEnCobranzaAsBool;
            }
            set {
                if(Model.ManejarDiferenciaCambiariaEnCobranzaAsBool != value) {
                    Model.ManejarDiferenciaCambiariaEnCobranzaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ManejarDiferenciaCambiariaEnCobranzaPropertyName);
                }
            }
        }

        public eTipoDeContabilizacion TipoContabilizacionPagos {
            get {
                return Model.TipoContabilizacionPagosAsEnum;
            }
            set {
                if(Model.TipoContabilizacionPagosAsEnum != value) {
                    Model.TipoContabilizacionPagosAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoContabilizacionPagosPropertyName);
                }
            }
        }

        public eContabilizacionIndividual ContabIndividualPagos {
            get {
                return Model.ContabIndividualPagosAsEnum;
            }
            set {
                if(Model.ContabIndividualPagosAsEnum != value) {
                    Model.ContabIndividualPagosAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualPagosPropertyName);
                }
            }
        }

        public eContabilizacionPorLote ContabPorLotePagos {
            get {
                return Model.ContabPorLotePagosAsEnum;
            }
            set {
                if(Model.ContabPorLotePagosAsEnum != value) {
                    Model.ContabPorLotePagosAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabPorLotePagosPropertyName);
                }
            }
        }

        public string CuentaPagosCxPProveedores {
            get {
                return Model.CuentaPagosCxPProveedores;
            }
            set {
                if(Model.CuentaPagosCxPProveedores != value) {
                    Model.CuentaPagosCxPProveedores = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaPagosCxPProveedoresPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaPagosCxPProveedores,true)) {
                        ConexionCuentaPagosCxPProveedores = null;
                    }
                }
            }
        }

        public string CuentaPagosRetencionISLR {
            get {
                return Model.CuentaPagosRetencionISLR;
            }
            set {
                if(Model.CuentaPagosRetencionISLR != value) {
                    Model.CuentaPagosRetencionISLR = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaPagosRetencionISLRPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaPagosRetencionISLR,true)) {
                        ConexionCuentaPagosRetencionISLR = null;
                    }
                }
            }
        }

        public string CuentaPagosOtros {
            get {
                return Model.CuentaPagosOtros;
            }
            set {
                if(Model.CuentaPagosOtros != value) {
                    Model.CuentaPagosOtros = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaPagosOtrosPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaPagosOtros,true)) {
                        ConexionCuentaPagosOtros = null;
                    }
                }
            }
        }

        public string CuentaPagosBanco {
            get {
                return Model.CuentaPagosBanco;
            }
            set {
                if(Model.CuentaPagosBanco != value) {
                    Model.CuentaPagosBanco = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaPagosBancoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaPagosBanco,true)) {
                        ConexionCuentaPagosBanco = null;
                    }
                }
            }
        }

        public string CuentaPagosPagadoAnticipo {
            get {
                return Model.CuentaPagosPagadoAnticipo;
            }
            set {
                if(Model.CuentaPagosPagadoAnticipo != value) {
                    Model.CuentaPagosPagadoAnticipo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaPagosPagadoAnticipoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaPagosPagadoAnticipo,true)) {
                        ConexionCuentaPagosPagadoAnticipo = null;
                    }
                }
            }
        }

        public string PagoTipoComprobante {
            get {
                return Model.PagoTipoComprobante;
            }
            set {
                if(Model.PagoTipoComprobante != value) {
                    Model.PagoTipoComprobante = value;
                    IsDirty = true;
                    RaisePropertyChanged(PagoTipoComprobantePropertyName);
                    if(LibString.IsNullOrEmpty(PagoTipoComprobante,true)) {
                        ConexionPagoTipoComprobante = null;
                    }
                }
            }
        }

        public bool EditarComprobanteAfterInsertPagos {
            get {
                return Model.EditarComprobanteAfterInsertPagosAsBool;
            }
            set {
                if(Model.EditarComprobanteAfterInsertPagosAsBool != value) {
                    Model.EditarComprobanteAfterInsertPagosAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertPagosPropertyName);
                }
            }
        }

        public bool ManejarDiferenciaCambiariaEnPagos {
            get {
                return Model.ManejarDiferenciaCambiariaEnPagosAsBool;
            }
            set {
                if(Model.ManejarDiferenciaCambiariaEnPagosAsBool != value) {
                    Model.ManejarDiferenciaCambiariaEnPagosAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ManejarDiferenciaCambiariaEnPagosPropertyName);
                }
            }
        }

        public eTipoDeContabilizacion TipoContabilizacionFacturacion {
            get {
                return Model.TipoContabilizacionFacturacionAsEnum;
            }
            set {
                if(Model.TipoContabilizacionFacturacionAsEnum != value) {
                    Model.TipoContabilizacionFacturacionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoContabilizacionFacturacionPropertyName);
                }
            }
        }

        public eContabilizacionIndividual ContabIndividualFacturacion {
            get {
                return Model.ContabIndividualFacturacionAsEnum;
            }
            set {
                if(Model.ContabIndividualFacturacionAsEnum != value) {
                    Model.ContabIndividualFacturacionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualFacturacionPropertyName);
                }
            }
        }

        public eContabilizacionPorLote ContabPorLoteFacturacion {
            get {
                return Model.ContabPorLoteFacturacionAsEnum;
            }
            set {
                if(Model.ContabPorLoteFacturacionAsEnum != value) {
                    Model.ContabPorLoteFacturacionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabPorLoteFacturacionPropertyName);
                }
            }
        }

        public string CuentaFacturacionCxCClientes {
            get {
                return Model.CuentaFacturacionCxCClientes;
            }
            set {
                if(Model.CuentaFacturacionCxCClientes != value) {
                    Model.CuentaFacturacionCxCClientes = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaFacturacionCxCClientesPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaFacturacionCxCClientes,true)) {
                        ConexionCuentaFacturacionCxCClientes = null;
                    }
                }
            }
        }

        public string CuentaFacturacionMontoTotalFactura {
            get {
                return Model.CuentaFacturacionMontoTotalFactura;
            }
            set {
                if(Model.CuentaFacturacionMontoTotalFactura != value) {
                    Model.CuentaFacturacionMontoTotalFactura = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaFacturacionMontoTotalFacturaPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaFacturacionMontoTotalFactura,true)) {
                        ConexionCuentaFacturacionMontoTotalFactura = null;
                    }
                }
            }
        }

        public string CuentaFacturacionCargos {
            get {
                return Model.CuentaFacturacionCargos;
            }
            set {
                if(Model.CuentaFacturacionCargos != value) {
                    Model.CuentaFacturacionCargos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaFacturacionCargosPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaFacturacionCargos,true)) {
                        ConexionCuentaFacturacionCargos = null;
                    }
                }
            }
        }

        public string CuentaFacturacionDescuentos {
            get {
                return Model.CuentaFacturacionDescuentos;
            }
            set {
                if(Model.CuentaFacturacionDescuentos != value) {
                    Model.CuentaFacturacionDescuentos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaFacturacionDescuentosPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaFacturacionDescuentos,true)) {
                        ConexionCuentaFacturacionDescuentos = null;
                    }
                }
            }
        }

        public string CuentaFacturacionIvaDiferido {
            get {
                return Model.CuentaFacturacionIvaDiferido;
            }
            set {
                if(Model.CuentaFacturacionIvaDiferido != value) {
                    Model.CuentaFacturacionIvaDiferido = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaFacturacionIvaDiferidoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaFacturacionIvaDiferido,true)) {
                        ConexionCuentaFacturacionIvaDiferido = null;
                    }
                }
            }
        }

        public bool ContabilizarPorArticulo {
            get {
                return Model.ContabilizarPorArticuloAsBool;
            }
            set {
                if(Model.ContabilizarPorArticuloAsBool != value) {
                    Model.ContabilizarPorArticuloAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabilizarPorArticuloPropertyName);
                    RaisePropertyChanged(IsEnabledAgruparPorCuentaDeArticuloPropertyName);
                    RaisePropertyChanged(LeyendaFacturaVisiblePropertyName);
                    if(!value) {
                        AgruparPorCuentaDeArticulo = false;
                    }
                }
            }
        }

        public bool AgruparPorCuentaDeArticulo {
            get {
                return Model.AgruparPorCuentaDeArticuloAsBool;
            }
            set {
                if(Model.AgruparPorCuentaDeArticuloAsBool != value) {
                    Model.AgruparPorCuentaDeArticuloAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AgruparPorCuentaDeArticuloPropertyName);
                }
            }
        }

        public bool AgruparPorCargosDescuentos {
            get {
                return Model.AgruparPorCargosDescuentosAsBool;
            }
            set {
                if(Model.AgruparPorCargosDescuentosAsBool != value) {
                    Model.AgruparPorCargosDescuentosAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AgruparPorCargosDescuentosPropertyName);
                }
            }
        }

        public string FacturaTipoComprobante {
            get {
                return Model.FacturaTipoComprobante;
            }
            set {
                if(Model.FacturaTipoComprobante != value) {
                    Model.FacturaTipoComprobante = value;
                    IsDirty = true;
                    RaisePropertyChanged(FacturaTipoComprobantePropertyName);
                    if(LibString.IsNullOrEmpty(FacturaTipoComprobante,true)) {
                        ConexionFacturaTipoComprobante = null;
                    }
                }
            }
        }

        public bool EditarComprobanteAfterInsertFactura {
            get {
                return Model.EditarComprobanteAfterInsertFacturaAsBool;
            }
            set {
                if(Model.EditarComprobanteAfterInsertFacturaAsBool != value) {
                    Model.EditarComprobanteAfterInsertFacturaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertFacturaPropertyName);
                }
            }
        }

        public eTipoDeContabilizacion TipoContabilizacionRDVtas {
            get {
                return Model.TipoContabilizacionRDVtasAsEnum;
            }
            set {
                if(Model.TipoContabilizacionRDVtasAsEnum != value) {
                    Model.TipoContabilizacionRDVtasAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoContabilizacionRDVtasPropertyName);
                }
            }
        }

        public eContabilizacionIndividual ContabIndividualRDVtas {
            get {
                return Model.ContabIndividualRDVtasAsEnum;
            }
            set {
                if(Model.ContabIndividualRDVtasAsEnum != value) {
                    Model.ContabIndividualRDVtasAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualRDVtasPropertyName);
                }
            }
        }

        public eContabilizacionPorLote ContabPorLoteRDVtas {
            get {
                return Model.ContabPorLoteRDVtasAsEnum;
            }
            set {
                if(Model.ContabPorLoteRDVtasAsEnum != value) {
                    Model.ContabPorLoteRDVtasAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabPorLoteRDVtasPropertyName);
                }
            }
        }

        public string CuentaRDVtasCaja {
            get {
                return Model.CuentaRDVtasCaja;
            }
            set {
                if(Model.CuentaRDVtasCaja != value) {
                    Model.CuentaRDVtasCaja = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaRDVtasCajaPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaRDVtasCaja,true)) {
                        ConexionCuentaRDVtasCaja = null;
                    }
                }
            }
        }

        public string CuentaRDVtasMontoTotal {
            get {
                return Model.CuentaRDVtasMontoTotal;
            }
            set {
                if(Model.CuentaRDVtasMontoTotal != value) {
                    Model.CuentaRDVtasMontoTotal = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaRDVtasMontoTotalPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaRDVtasMontoTotal,true)) {
                        ConexionCuentaRDVtasMontoTotal = null;
                    }
                }
            }
        }

        public bool ContabilizarPorArticuloRDVtas {
            get {
                return Model.ContabilizarPorArticuloRDVtasAsBool;
            }
            set {
                if(Model.ContabilizarPorArticuloRDVtasAsBool != value) {
                    Model.ContabilizarPorArticuloRDVtasAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabilizarPorArticuloRDVtasPropertyName);
                }
            }
        }

        public bool AgruparPorCuentaDeArticuloRDVtas {
            get {
                return Model.AgruparPorCuentaDeArticuloRDVtasAsBool;
            }
            set {
                if(Model.AgruparPorCuentaDeArticuloRDVtasAsBool != value) {
                    Model.AgruparPorCuentaDeArticuloRDVtasAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AgruparPorCuentaDeArticuloRDVtasPropertyName);
                }
            }
        }

        public bool EditarComprobanteAfterInsertResDia {
            get {
                return Model.EditarComprobanteAfterInsertResDiaAsBool;
            }
            set {
                if(Model.EditarComprobanteAfterInsertResDiaAsBool != value) {
                    Model.EditarComprobanteAfterInsertResDiaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertResDiaPropertyName);
                }
            }
        }

        public eTipoDeContabilizacion TipoContabilizacionMovBancario {
            get {
                return Model.TipoContabilizacionMovBancarioAsEnum;
            }
            set {
                if(Model.TipoContabilizacionMovBancarioAsEnum != value) {
                    Model.TipoContabilizacionMovBancarioAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoContabilizacionMovBancarioPropertyName);
                }
            }
        }

        public eContabilizacionIndividual ContabIndividualMovBancario {
            get {
                return Model.ContabIndividualMovBancarioAsEnum;
            }
            set {
                if(Model.ContabIndividualMovBancarioAsEnum != value) {
                    Model.ContabIndividualMovBancarioAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualMovBancarioPropertyName);
                }
            }
        }

        public eContabilizacionPorLote ContabPorLoteMovBancario {
            get {
                return Model.ContabPorLoteMovBancarioAsEnum;
            }
            set {
                if(Model.ContabPorLoteMovBancarioAsEnum != value) {
                    Model.ContabPorLoteMovBancarioAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabPorLoteMovBancarioPropertyName);
                }
            }
        }

        public string CuentaMovBancarioGasto {
            get {
                return Model.CuentaMovBancarioGasto;
            }
            set {
                if(Model.CuentaMovBancarioGasto != value) {
                    Model.CuentaMovBancarioGasto = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaMovBancarioGastoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaMovBancarioGasto,true)) {
                        ConexionCuentaMovBancarioGasto = null;
                    }
                }
            }
        }

        public string CuentaMovBancarioBancosHaber {
            get {
                return Model.CuentaMovBancarioBancosHaber;
            }
            set {
                if(Model.CuentaMovBancarioBancosHaber != value) {
                    Model.CuentaMovBancarioBancosHaber = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaMovBancarioBancosHaberPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaMovBancarioBancosHaber,true)) {
                        ConexionCuentaMovBancarioBancosHaber = null;
                    }
                }
            }
        }

        public string CuentaMovBancarioBancosDebe {
            get {
                return Model.CuentaMovBancarioBancosDebe;
            }
            set {
                if(Model.CuentaMovBancarioBancosDebe != value) {
                    Model.CuentaMovBancarioBancosDebe = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaMovBancarioBancosDebePropertyName);
                    if(LibString.IsNullOrEmpty(CuentaMovBancarioBancosDebe,true)) {
                        ConexionCuentaMovBancarioBancosDebe = null;
                    }
                }
            }
        }

        public string CuentaMovBancarioIngresos {
            get {
                return Model.CuentaMovBancarioIngresos;
            }
            set {
                if(Model.CuentaMovBancarioIngresos != value) {
                    Model.CuentaMovBancarioIngresos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaMovBancarioIngresosPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaMovBancarioIngresos,true)) {
                        ConexionCuentaMovBancarioIngresos = null;
                    }
                }
            }
        }

        public string MovimientoBancarioTipoComprobante {
            get {
                return Model.MovimientoBancarioTipoComprobante;
            }
            set {
                if(Model.MovimientoBancarioTipoComprobante != value) {
                    Model.MovimientoBancarioTipoComprobante = value;
                    IsDirty = true;
                    RaisePropertyChanged(MovimientoBancarioTipoComprobantePropertyName);
                    if(LibString.IsNullOrEmpty(MovimientoBancarioTipoComprobante,true)) {
                        ConexionMovimientoBancarioTipoComprobante = null;
                    }
                }
            }
        }

        public bool EditarComprobanteAfterInsertMovBan {
            get {
                return Model.EditarComprobanteAfterInsertMovBanAsBool;
            }
            set {
                if(Model.EditarComprobanteAfterInsertMovBanAsBool != value) {
                    Model.EditarComprobanteAfterInsertMovBanAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertMovBanPropertyName);
                }
            }
        }

        public string CuentaDebitoBancarioGasto {
            get {
                return Model.CuentaDebitoBancarioGasto;
            }
            set {
                if(Model.CuentaDebitoBancarioGasto != value) {
                    Model.CuentaDebitoBancarioGasto = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaDebitoBancarioGastoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaDebitoBancarioGasto,true)) {
                        ConexionCuentaDebitoBancarioGasto = null;
                    }
                }
            }
        }

        public string CuentaDebitoBancarioBancos {
            get {
                return Model.CuentaDebitoBancarioBancos;
            }
            set {
                if(Model.CuentaDebitoBancarioBancos != value) {
                    Model.CuentaDebitoBancarioBancos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaDebitoBancarioBancosPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaDebitoBancarioBancos,true)) {
                        ConexionCuentaDebitoBancarioBancos = null;
                    }
                }
            }
        }

        public string CuentaCreditoBancarioGasto {
            get {
                return Model.CuentaCreditoBancarioGasto;
            }
            set {
                if(Model.CuentaCreditoBancarioGasto != value) {
                    Model.CuentaCreditoBancarioGasto = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCreditoBancarioGastoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCreditoBancarioGasto,true)) {
                        ConexionCuentaCreditoBancarioGasto = null;
                    }
                }
            }
        }

        public string CuentaCreditoBancarioBancos {
            get {
                return Model.CuentaCreditoBancarioBancos;
            }
            set {
                if(Model.CuentaCreditoBancarioBancos != value) {
                    Model.CuentaCreditoBancarioBancos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCreditoBancarioBancosPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCreditoBancarioBancos,true)) {
                        ConexionCuentaCreditoBancarioBancos = null;
                    }
                }
            }
        }

        public bool EditarComprobanteAfterInsertImpTraBan {
            get {
                return Model.EditarComprobanteAfterInsertImpTraBanAsBool;
            }
            set {
                if(Model.EditarComprobanteAfterInsertImpTraBanAsBool != value) {
                    Model.EditarComprobanteAfterInsertImpTraBanAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertImpTraBanPropertyName);
                }
            }
        }

        public eTipoDeContabilizacion TipoContabilizacionAnticipo {
            get {
                return Model.TipoContabilizacionAnticipoAsEnum;
            }
            set {
                if(Model.TipoContabilizacionAnticipoAsEnum != value) {
                    Model.TipoContabilizacionAnticipoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoContabilizacionAnticipoPropertyName);
                }
            }
        }

        public eContabilizacionIndividual ContabIndividualAnticipo {
            get {
                return Model.ContabIndividualAnticipoAsEnum;
            }
            set {
                if(Model.ContabIndividualAnticipoAsEnum != value) {
                    Model.ContabIndividualAnticipoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualAnticipoPropertyName);
                }
            }
        }

        public eContabilizacionPorLote ContabPorLoteAnticipo {
            get {
                return Model.ContabPorLoteAnticipoAsEnum;
            }
            set {
                if(Model.ContabPorLoteAnticipoAsEnum != value) {
                    Model.ContabPorLoteAnticipoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabPorLoteAnticipoPropertyName);
                }
            }
        }

        public string CuentaAnticipoCaja {
            get {
                return Model.CuentaAnticipoCaja;
            }
            set {
                if(Model.CuentaAnticipoCaja != value) {
                    Model.CuentaAnticipoCaja = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaAnticipoCajaPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaAnticipoCaja,true)) {
                        ConexionCuentaAnticipoCaja = null;
                    }
                }
            }
        }

        public string CuentaAnticipoCobrado {
            get {
                return Model.CuentaAnticipoCobrado;
            }
            set {
                if(Model.CuentaAnticipoCobrado != value) {
                    Model.CuentaAnticipoCobrado = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaAnticipoCobradoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaAnticipoCobrado,true)) {
                        ConexionCuentaAnticipoCobrado = null;
                    }
                }
            }
        }

        public string CuentaAnticipoOtrosIngresos {
            get {
                return Model.CuentaAnticipoOtrosIngresos;
            }
            set {
                if(Model.CuentaAnticipoOtrosIngresos != value) {
                    Model.CuentaAnticipoOtrosIngresos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaAnticipoOtrosIngresosPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaAnticipoOtrosIngresos,true)) {
                        ConexionCuentaAnticipoOtrosIngresos = null;
                    }
                }
            }
        }

        public string CuentaAnticipoPagado {
            get {
                return Model.CuentaAnticipoPagado;
            }
            set {
                if(Model.CuentaAnticipoPagado != value) {
                    Model.CuentaAnticipoPagado = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaAnticipoPagadoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaAnticipoPagado,true)) {
                        ConexionCuentaAnticipoPagado = null;
                    }
                }
            }
        }

        public string CuentaAnticipoBanco {
            get {
                return Model.CuentaAnticipoBanco;
            }
            set {
                if(Model.CuentaAnticipoBanco != value) {
                    Model.CuentaAnticipoBanco = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaAnticipoBancoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaAnticipoBanco,true)) {
                        ConexionCuentaAnticipoBanco = null;
                    }
                }
            }
        }

        public string CuentaAnticipoOtrosEgresos {
            get {
                return Model.CuentaAnticipoOtrosEgresos;
            }
            set {
                if(Model.CuentaAnticipoOtrosEgresos != value) {
                    Model.CuentaAnticipoOtrosEgresos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaAnticipoOtrosEgresosPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaAnticipoOtrosEgresos,true)) {
                        ConexionCuentaAnticipoOtrosEgresos = null;
                    }
                }
            }
        }

        public string AnticipoTipoComprobante {
            get {
                return Model.AnticipoTipoComprobante;
            }
            set {
                if(Model.AnticipoTipoComprobante != value) {
                    Model.AnticipoTipoComprobante = value;
                    IsDirty = true;
                    RaisePropertyChanged(AnticipoTipoComprobantePropertyName);
                    if(LibString.IsNullOrEmpty(AnticipoTipoComprobante,true)) {
                        ConexionAnticipoTipoComprobante = null;
                    }
                }
            }
        }

        public bool EditarComprobanteAfterInsertAnticipo {
            get {
                return Model.EditarComprobanteAfterInsertAnticipoAsBool;
            }
            set {
                if(Model.EditarComprobanteAfterInsertAnticipoAsBool != value) {
                    Model.EditarComprobanteAfterInsertAnticipoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertAnticipoPropertyName);
                }
            }
        }

        public eTipoDeContabilizacion TipoContabilizacionInventario {
            get {
                return Model.TipoContabilizacionInventarioAsEnum;
            }
            set {
                if(Model.TipoContabilizacionInventarioAsEnum != value) {
                    Model.TipoContabilizacionInventarioAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoContabilizacionInventarioPropertyName);
                }
            }
        }

        public string CuentaCostoDeVenta {
            get {
                return Model.CuentaCostoDeVenta;
            }
            set {
                if(Model.CuentaCostoDeVenta != value) {
                    Model.CuentaCostoDeVenta = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCostoDeVentaPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCostoDeVenta,true)) {
                        ConexionCuentaCostoDeVenta = null;
                    }
                }
            }
        }

        public string CuentaInventario {
            get {
                return Model.CuentaInventario;
            }
            set {
                if(Model.CuentaInventario != value) {
                    Model.CuentaInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaInventarioPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaInventario,true)) {
                        ConexionCuentaInventario = null;
                    }
                }
            }
        }

        public bool AgruparPorCuentaDeArticuloInven {
            get {
                return Model.AgruparPorCuentaDeArticuloInvenAsBool;
            }
            set {
                if(Model.AgruparPorCuentaDeArticuloInvenAsBool != value) {
                    Model.AgruparPorCuentaDeArticuloInvenAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AgruparPorCuentaDeArticuloInvenPropertyName);
                }
            }
        }

        public string InventarioTipoComprobante {
            get {
                return Model.InventarioTipoComprobante;
            }
            set {
                if(Model.InventarioTipoComprobante != value) {
                    Model.InventarioTipoComprobante = value;
                    IsDirty = true;
                    RaisePropertyChanged(InventarioTipoComprobantePropertyName);
                    if(LibString.IsNullOrEmpty(InventarioTipoComprobante,true)) {
                        ConexionInventarioTipoComprobante = null;
                    }
                }
            }
        }

        public bool EditarComprobanteAfterInsertInventario {
            get {
                return Model.EditarComprobanteAfterInsertInventarioAsBool;
            }
            set {
                if(Model.EditarComprobanteAfterInsertInventarioAsBool != value) {
                    Model.EditarComprobanteAfterInsertInventarioAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertInventarioPropertyName);
                }
            }
        }

        public eTipoDeContabilizacion TipoContabilizacionDePagosSueldos {
            get {
                return Model.TipoContabilizacionDePagosSueldosAsEnum;
            }
            set {
                if(Model.TipoContabilizacionDePagosSueldosAsEnum != value) {
                    Model.TipoContabilizacionDePagosSueldosAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoContabilizacionDePagosSueldosPropertyName);
                }
            }
        }

        public eContabilizacionIndividual ContabIndividualPagosSueldos {
            get {
                return Model.ContabIndividualPagosSueldosAsEnum;
            }
            set {
                if(Model.ContabIndividualPagosSueldosAsEnum != value) {
                    Model.ContabIndividualPagosSueldosAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualPagosSueldosPropertyName);
                }
            }
        }

        public string CtaDePagosSueldos {
            get {
                return Model.CtaDePagosSueldos;
            }
            set {
                if(Model.CtaDePagosSueldos != value) {
                    Model.CtaDePagosSueldos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CtaDePagosSueldosPropertyName);
                    if(LibString.IsNullOrEmpty(InventarioTipoComprobante,true)) {
                        ConexionInventarioTipoComprobante = null;
                    }
                }
            }
        }

        public string CtaDePagosSueldosBanco {
            get {
                return Model.CtaDePagosSueldosBanco;
            }
            set {
                if(Model.CtaDePagosSueldosBanco != value) {
                    Model.CtaDePagosSueldosBanco = value;
                    IsDirty = true;
                    RaisePropertyChanged(CtaDePagosSueldosBancoPropertyName);
                    if(LibString.IsNullOrEmpty(CtaDePagosSueldosBanco,true)) {
                        ConexionCtaDePagosSueldosBanco = null;
                    }
                }
            }
        }

        public string PagosSueldosTipoComprobante {
            get {
                return Model.PagosSueldosTipoComprobante;
            }
            set {
                if(Model.PagosSueldosTipoComprobante != value) {
                    Model.PagosSueldosTipoComprobante = value;
                    IsDirty = true;
                    RaisePropertyChanged(PagosSueldosTipoComprobantePropertyName);
                    if(LibString.IsNullOrEmpty(PagosSueldosTipoComprobante,true)) {
                        ConexionPagosSueldosTipoComprobante = null;
                    }
                }
            }
        }

        public bool EditarComprobanteDePagosSueldos {
            get {
                return Model.EditarComprobanteDePagosSueldosAsBool;
            }
            set {
                if(Model.EditarComprobanteDePagosSueldosAsBool != value) {
                    Model.EditarComprobanteDePagosSueldosAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteDePagosSueldosPropertyName);
                }
            }
        }

        public eContabilizacionIndividual ContabIndividualCajaChica {
            get {
                return Model.ContabIndividualCajaChicaAsEnum;
            }
            set {
                if(Model.ContabIndividualCajaChicaAsEnum != value) {
                    Model.ContabIndividualCajaChicaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualCajaChicaPropertyName);
                }
            }
        }

        public bool MostrarDesglosadoCajaChica {
            get {
                return Model.MostrarDesglosadoCajaChicaAsBool;
            }
            set {
                if(Model.MostrarDesglosadoCajaChicaAsBool != value) {
                    Model.MostrarDesglosadoCajaChicaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(MostrarDesglosadoCajaChicaPropertyName);
                }
            }
        }

        public string CuentaCajaChicaGasto {
            get {
                return Model.CuentaCajaChicaGasto;
            }
            set {
                if(Model.CuentaCajaChicaGasto != value) {
                    Model.CuentaCajaChicaGasto = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCajaChicaGastoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCajaChicaGasto,true)) {
                        ConexionCuentaCajaChicaGasto = null;
                    }
                }
            }
        }

        public string CuentaCajaChicaBancoHaber {
            get {
                return Model.CuentaCajaChicaBancoHaber;
            }
            set {
                if(Model.CuentaCajaChicaBancoHaber != value) {
                    Model.CuentaCajaChicaBancoHaber = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCajaChicaBancoHaberPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCajaChicaBancoHaber,true)) {
                        ConexionCuentaCajaChicaBancoHaber = null;
                    }
                }
            }
        }

        public string CuentaCajaChicaBancoDebe {
            get {
                return Model.CuentaCajaChicaBancoDebe;
            }
            set {
                if(Model.CuentaCajaChicaBancoDebe != value) {
                    Model.CuentaCajaChicaBancoDebe = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCajaChicaBancoDebePropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCajaChicaBancoDebe,true)) {
                        ConexionCuentaCajaChicaBancoDebe = null;
                    }
                }
            }
        }

        public string CuentaCajaChicaBanco {
            get {
                return Model.CuentaCajaChicaBanco;
            }
            set {
                if(Model.CuentaCajaChicaBanco != value) {
                    Model.CuentaCajaChicaBanco = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaCajaChicaBancoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaCajaChicaBanco,true)) {
                        ConexionCuentaCajaChicaBanco = null;
                    }
                }
            }
        }

        public string SiglasTipoComprobanteCajaChica {
            get {
                return Model.SiglasTipoComprobanteCajaChica;
            }
            set {
                if(Model.SiglasTipoComprobanteCajaChica != value) {
                    Model.SiglasTipoComprobanteCajaChica = value;
                    IsDirty = true;
                    RaisePropertyChanged(SiglasTipoComprobanteCajaChicaPropertyName);
                    if(LibString.IsNullOrEmpty(SiglasTipoComprobanteCajaChica,true)) {
                        ConexionSiglasTipoComprobanteCajaChica = null;
                    }
                }
            }
        }

        public bool EditarComprobanteAfterInsertCajaChica {
            get {
                return Model.EditarComprobanteAfterInsertCajaChicaAsBool;
            }
            set {
                if(Model.EditarComprobanteAfterInsertCajaChicaAsBool != value) {
                    Model.EditarComprobanteAfterInsertCajaChicaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertCajaChicaPropertyName);
                }
            }
        }

        public eContabilizacionIndividual ContabIndividualRendiciones {
            get {
                return Model.ContabIndividualRendicionesAsEnum;
            }
            set {
                if(Model.ContabIndividualRendicionesAsEnum != value) {
                    Model.ContabIndividualRendicionesAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualRendicionesPropertyName);
                }
            }
        }

        public bool MostrarDesglosadoRendiciones {
            get {
                return Model.MostrarDesglosadoRendicionesAsBool;
            }
            set {
                if(Model.MostrarDesglosadoRendicionesAsBool != value) {
                    Model.MostrarDesglosadoRendicionesAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(MostrarDesglosadoRendicionesPropertyName);
                }
            }
        }

        public string CuentaRendicionesGasto {
            get {
                return Model.CuentaRendicionesGasto;
            }
            set {
                if(Model.CuentaRendicionesGasto != value) {
                    Model.CuentaRendicionesGasto = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaRendicionesGastoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaRendicionesGasto,true)) {
                        ConexionCuentaRendicionesGasto = null;
                    }
                }
            }
        }

        public string CuentaRendicionesBanco {
            get {
                return Model.CuentaRendicionesBanco;
            }
            set {
                if(Model.CuentaRendicionesBanco != value) {
                    Model.CuentaRendicionesBanco = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaRendicionesBancoPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaRendicionesBanco,true)) {
                        ConexionCuentaRendicionesBanco = null;
                    }
                }
            }
        }

        public string CuentaRendicionesAnticipos {
            get {
                return Model.CuentaRendicionesAnticipos;
            }
            set {
                if(Model.CuentaRendicionesAnticipos != value) {
                    Model.CuentaRendicionesAnticipos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaRendicionesAnticiposPropertyName);
                    if(LibString.IsNullOrEmpty(CuentaRendicionesAnticipos,true)) {
                        ConexionCuentaRendicionesAnticipos = null;
                    }
                }
            }
        }

        public string SiglasTipoComprobanteRendiciones {
            get {
                return Model.SiglasTipoComprobanteRendiciones;
            }
            set {
                if(Model.SiglasTipoComprobanteRendiciones != value) {
                    Model.SiglasTipoComprobanteRendiciones = value;
                    IsDirty = true;
                    RaisePropertyChanged(SiglasTipoComprobanteRendicionesPropertyName);
                    if(LibString.IsNullOrEmpty(SiglasTipoComprobanteRendiciones,true)) {
                        ConexionSiglasTipoComprobanteRendiciones = null;
                    }
                }
            }
        }

        public eContabilizacionIndividual  ContabIndividualTransfCtas {
            get {
                return eContabilizacionIndividual.Pospuesta;
            }
            set {
                if (Model.ContabIndividualTransfCtasAsEnum != value) {
                    Model.ContabIndividualTransfCtasAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualTransfCtasPropertyName);
                }
            }
        }
        public eTipoDeContabilizacion  TipoContabilizacionTransfCtas {
            get {
                return Model.TipoContabilizacionTransfCtasAsEnum;
            }
            set {
                if (Model.TipoContabilizacionTransfCtasAsEnum != value) {
                    Model.TipoContabilizacionTransfCtasAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoContabilizacionTransfCtasPropertyName);
                }
            }
        }
        public eContabilizacionPorLote  ContabPorLoteTransfCtas {
            get {
                return Model.ContabPorLoteTransfCtasAsEnum;
            }
            set {
                if (Model.ContabPorLoteTransfCtasAsEnum != value) {
                    Model.ContabPorLoteTransfCtasAsEnum = value;
                }
            }
        }
        public string  CuentaTransfCtasBancoDestino {
            get {
                return Model.CuentaTransfCtasBancoDestino;
            }
            set {
                if (Model.CuentaTransfCtasBancoDestino != value) {
                    Model.CuentaTransfCtasBancoDestino = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaTransfCtasBancoDestinoPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaTransfCtasBancoDestino, true)) {
                        ConexionCuentaTransfCtasBancoDestino = null;
                    }
                }
            }
        }
        public string  CuentaTransfCtasGastoComOrigen {
            get {
                return Model.CuentaTransfCtasGastoComOrigen;
            }
            set {
                if (Model.CuentaTransfCtasGastoComOrigen != value) {
                    Model.CuentaTransfCtasGastoComOrigen = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaTransfCtasGastoComOrigenPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaTransfCtasGastoComOrigen, true)) {
                        ConexionCuentaTransfCtasGastoComOrigen = null;
                    }
                }
            }
        }
        public string  CuentaTransfCtasGastoComDestino {
            get {
                return Model.CuentaTransfCtasGastoComDestino;
            }
            set {
                if (Model.CuentaTransfCtasGastoComDestino != value) {
                    Model.CuentaTransfCtasGastoComDestino = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaTransfCtasGastoComDestinoPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaTransfCtasGastoComDestino, true)) {
                        ConexionCuentaTransfCtasGastoComDestino = null;
                    }
                }
            }
        }
        public string  CuentaTransfCtasBancoOrigen {
            get {
                return Model.CuentaTransfCtasBancoOrigen;
            }
            set {
                if (Model.CuentaTransfCtasBancoOrigen != value) {
                    Model.CuentaTransfCtasBancoOrigen = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaTransfCtasBancoOrigenPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaTransfCtasBancoOrigen, true)) {
                        ConexionCuentaTransfCtasBancoOrigen = null;
                    }
                }
            }
        }
        public string  TransfCtasSigasTipoComprobante {
            get {
                return Model.TransfCtasSigasTipoComprobante;
            }
            set {
                if (Model.TransfCtasSigasTipoComprobante != value) {
                    Model.TransfCtasSigasTipoComprobante = value;
                    IsDirty = true;
                    RaisePropertyChanged(TransfCtasSigasTipoComprobantePropertyName);
                    if (LibString.IsNullOrEmpty(TransfCtasSigasTipoComprobante, true)) {
                        ConexionTransfCtasSigasTipoComprobante = null;
                    }
                }
            }
        }
        public bool  EditarComprobanteAfterInsertTransfCtas {
            get {
                return Model.EditarComprobanteAfterInsertTransfCtasAsBool;
            }
            set {
                if (Model.EditarComprobanteAfterInsertTransfCtasAsBool != value) {
                    Model.EditarComprobanteAfterInsertTransfCtasAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertTransfCtasPropertyName);
                }
            }
        }

        public eTipoDeContabilizacion  TipoContabilizacionOrdenDeProduccion {
            get {
                return Model.TipoContabilizacionOrdenDeProduccionAsEnum;
            }
            set {
                if (Model.TipoContabilizacionOrdenDeProduccionAsEnum != value) {
                    Model.TipoContabilizacionOrdenDeProduccionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoContabilizacionOrdenDeProduccionPropertyName);
                }
            }
        }

        public eContabilizacionIndividual  ContabIndividualOrdenDeProduccion {
            get {
                return Model.ContabIndividualOrdenDeProduccionAsEnum;
            }
            set {
                if (Model.ContabIndividualOrdenDeProduccionAsEnum != value) {
                    Model.ContabIndividualOrdenDeProduccionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabIndividualOrdenDeProduccionPropertyName);
                }
            }
        }

        public eContabilizacionPorLote  ContabPorLoteOrdenDeProduccion {
            get {
                return Model.ContabPorLoteOrdenDeProduccionAsEnum;
            }
            set {
                if (Model.ContabPorLoteOrdenDeProduccionAsEnum != value) {
                    Model.ContabPorLoteOrdenDeProduccionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ContabPorLoteOrdenDeProduccionPropertyName);
                }
            }
        }

        public string  CuentaOrdenDeProduccionProductoTerminado {
            get {
                return Model.CuentaOrdenDeProduccionProductoTerminado;
            }
            set {
                if (Model.CuentaOrdenDeProduccionProductoTerminado != value) {
                    Model.CuentaOrdenDeProduccionProductoTerminado = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaOrdenDeProduccionProductoTerminadoPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaOrdenDeProduccionProductoTerminado, true)) {
                        ConexionCuentaOrdenDeProduccionProductoTerminado = null;
                    }
                }
            }
        }

        public string  CuentaOrdenDeProduccionMateriaPrima {
            get {
                return Model.CuentaOrdenDeProduccionMateriaPrima;
            }
            set {
                if (Model.CuentaOrdenDeProduccionMateriaPrima != value) {
                    Model.CuentaOrdenDeProduccionMateriaPrima = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaOrdenDeProduccionMateriaPrimaPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaOrdenDeProduccionMateriaPrima, true)) {
                        ConexionCuentaOrdenDeProduccionMateriaPrima = null;
                    }
                }
            }
        }

        public string  OrdenDeProduccionTipoComprobante {
            get {
                return Model.OrdenDeProduccionTipoComprobante;
            }
            set {
                if (Model.OrdenDeProduccionTipoComprobante != value) {
                    Model.OrdenDeProduccionTipoComprobante = value;
                    IsDirty = true;
                    RaisePropertyChanged(OrdenDeProduccionTipoComprobantePropertyName);
                    if (LibString.IsNullOrEmpty(OrdenDeProduccionTipoComprobante, true)) {
                        ConexionOrdenDeProduccionTipoComprobante = null;
                    }
                }
            }
        }

        public bool  EditarComprobanteAfterInsertOrdenDeProduccion {
            get {
                return Model.EditarComprobanteAfterInsertOrdenDeProduccionAsBool;
            }
            set {
                if (Model.EditarComprobanteAfterInsertOrdenDeProduccionAsBool != value) {
                    Model.EditarComprobanteAfterInsertOrdenDeProduccionAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EditarComprobanteAfterInsertOrdenDeProduccionPropertyName);
                }
            }
        }

        public string  NombreOperador {
            get {
                return Model.NombreOperador;
            }
            set {
                if(Model.NombreOperador != value) {
                    Model.NombreOperador = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreOperadorPropertyName);
                }
            }
        }

        public DateTime FechaUltimaModificacion {
            get {
                return Model.FechaUltimaModificacion;
            }
            set {
                if(Model.FechaUltimaModificacion != value) {
                    Model.FechaUltimaModificacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        public eDondeEfectuoContabilizacionRetIVA[] ArrayDondeEfectuoContabilizacionRetIVA {
            get {
                return LibEnumHelper<eDondeEfectuoContabilizacionRetIVA>.GetValuesInArray();
            }
        }

        public eTipoDeContabilizacion[] ArrayTipoDeContabilizacion {
            get {
                return LibEnumHelper<eTipoDeContabilizacion>.GetValuesInArray();
            }
        }

        public eContabilizacionIndividual[] ArrayContabilizacionIndividual {
            get {
                return LibEnumHelper<eContabilizacionIndividual>.GetValuesInArray();
            }
        }

        public eContabilizacionPorLote[] ArrayContabilizacionPorLote {
            get {
                return LibEnumHelper<eContabilizacionPorLote>.GetValuesInArray();
            }
        }

        #endregion //Propiedades

        #region PropiedadesConexiones

        public FkCuentaViewModel ConexionCuentaIva1Credito {
            get {
                return _ConexionCuentaIva1Credito;
            }
            set {
                if(_ConexionCuentaIva1Credito != value) {
                    _ConexionCuentaIva1Credito = value;
                    if(ConexionCuentaIva1Credito != null) {
                        CuentaIva1Credito = ConexionCuentaIva1Credito.Codigo;
                        CuentaIva1CreditoDescripcion = ConexionCuentaIva1Credito.Descripcion;
                    }
                    RaisePropertyChanged(CuentaIva1CreditoPropertyName);
                    RaisePropertyChanged(CuentaIva1CreditoDescripcionPropertyName);
                }
                if(ConexionCuentaIva1Credito == null) {
                    CuentaIva1Credito = string.Empty;
                    CuentaIva1CreditoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaIva1CreditoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaIva1Debito {
            get {
                return _ConexionCuentaIva1Debito;
            }
            set {
                if(_ConexionCuentaIva1Debito != value) {
                    _ConexionCuentaIva1Debito = value;
                    if(ConexionCuentaIva1Debito != null) {
                        CuentaIva1Debito = ConexionCuentaIva1Debito.Codigo;
                        CuentaIva1DebitoDescripcion = ConexionCuentaIva1Debito.Descripcion;
                    }
                    RaisePropertyChanged(CuentaIva1DebitoPropertyName);
                    RaisePropertyChanged(CuentaIva1DebitoDescripcionPropertyName);
                }
                if(ConexionCuentaIva1Debito == null) {
                    CuentaIva1Debito = string.Empty;
                    CuentaIva1DebitoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaIva1DebitoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaRetencionIva {
            get {
                return _ConexionCuentaRetencionIva;
            }
            set {
                if(_ConexionCuentaRetencionIva != value) {
                    _ConexionCuentaRetencionIva = value;
                    if(ConexionCuentaRetencionIva != null) {
                        CuentaRetencionIva = ConexionCuentaRetencionIva.Codigo;
                        CuentaRetencionIvaDescripcion = ConexionCuentaRetencionIva.Descripcion;
                    }
                    RaisePropertyChanged(CuentaRetencionIvaPropertyName);
                    RaisePropertyChanged(CuentaRetencionIvaDescripcionPropertyName);
                }
                if(ConexionCuentaRetencionIva == null) {
                    CuentaRetencionIva = string.Empty;
                    CuentaRetencionIvaDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaRetencionIvaDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionDiferenciaEnCambioyCalculo {
            get {
                return _ConexionDiferenciaEnCambioyCalculo;
            }
            set {
                if(_ConexionDiferenciaEnCambioyCalculo != value) {
                    _ConexionDiferenciaEnCambioyCalculo = value;
                    if(ConexionDiferenciaEnCambioyCalculo != null) {
                        DiferenciaEnCambioyCalculo = ConexionDiferenciaEnCambioyCalculo.Codigo;
                        DiferenciaEnCambioyCalculoDescripcion = ConexionDiferenciaEnCambioyCalculo.Descripcion;
                    }
                    RaisePropertyChanged(DiferenciaEnCambioyCalculoPropertyName);
                    RaisePropertyChanged(DiferenciaEnCambioyCalculoDescripcionPropertyName);
                }
                if(ConexionDiferenciaEnCambioyCalculo == null) {
                    DiferenciaEnCambioyCalculo = string.Empty;
                    DiferenciaEnCambioyCalculoDescripcion = string.Empty;
                    RaisePropertyChanged(DiferenciaEnCambioyCalculoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaDiferenciaCambiaria {
            get {
                return _ConexionCuentaDiferenciaCambiaria;
            }
            set {
                if(_ConexionCuentaDiferenciaCambiaria != value) {
                    _ConexionCuentaDiferenciaCambiaria = value;
                    if(_ConexionCuentaDiferenciaCambiaria!=null){
                        CuentaDiferenciaCambiaria = ConexionCuentaDiferenciaCambiaria.Codigo;
                        CuentaDiferenciaCambiariaDescripcion = ConexionCuentaDiferenciaCambiaria.Descripcion;

                    }
                    RaisePropertyChanged(CuentaDiferenciaCambiariaPropertyName);
                    RaisePropertyChanged(CuentaDiferenciaCambiariaDescripcionPropertyName);
                }
                if(_ConexionCuentaDiferenciaCambiaria == null) {
                    CuentaDiferenciaCambiaria = string.Empty;
                    CuentaDiferenciaCambiariaDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaDiferenciaCambiariaPropertyName);
                }
            }
        }


        public FkCuentaViewModel ConexionCuentaCxCClientes {
            get {
                return _ConexionCuentaCxCClientes;
            }
            set {
                if(_ConexionCuentaCxCClientes != value) {
                    _ConexionCuentaCxCClientes = value;
                    if(ConexionCuentaCxCClientes != null) {
                        CuentaCxCClientes = ConexionCuentaCxCClientes.Codigo;
                        CuentaCxCClientesDescripcion = ConexionCuentaCxCClientes.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCxCClientesPropertyName);
                    RaisePropertyChanged(CuentaCxCClientesDescripcionPropertyName);
                }
                if(ConexionCuentaCxCClientes == null) {
                    CuentaCxCClientes = string.Empty;
                    CuentaCxCClientesDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCxCClientesDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCxCIngresos {
            get {
                return _ConexionCuentaCxCIngresos;
            }
            set {
                if(_ConexionCuentaCxCIngresos != value) {
                    _ConexionCuentaCxCIngresos = value;
                    if(ConexionCuentaCxCIngresos != null) {
                        CuentaCxCIngresos = ConexionCuentaCxCIngresos.Codigo;
                        CuentaCxCIngresosDescripcion = ConexionCuentaCxCIngresos.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCxCIngresosPropertyName);
                    RaisePropertyChanged(CuentaCxCIngresosDescripcionPropertyName);
                }
                if(ConexionCuentaCxCIngresos == null) {
                    CuentaCxCIngresos = string.Empty;
                    CuentaCxCIngresosDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCxCIngresosDescripcionPropertyName);
                }
            }
        }

        public FkTipoDeComprobanteViewModel ConexionCxCTipoComprobante {
            get {
                return _ConexionCxCTipoComprobante;
            }
            set {
                if(_ConexionCxCTipoComprobante != value) {
                    _ConexionCxCTipoComprobante = value;
                    RaisePropertyChanged(CxCTipoComprobantePropertyName);
                    if(ConexionCxCTipoComprobante != null) {
                        CxCTipoComprobante = ConexionCxCTipoComprobante.Codigo;
                    }
                }
                if(ConexionCxCTipoComprobante == null) {
                    CxCTipoComprobante = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCxPGasto {
            get {
                return _ConexionCuentaCxPGasto;
            }
            set {
                if(_ConexionCuentaCxPGasto != value) {
                    _ConexionCuentaCxPGasto = value;
                    if(ConexionCuentaCxPGasto != null) {
                        CuentaCxPGasto = ConexionCuentaCxPGasto.Codigo;
                        CuentaCxPGastoDescripcion = ConexionCuentaCxPGasto.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCxPGastoPropertyName);
                    RaisePropertyChanged(CuentaCxPGastoDescripcionPropertyName);
                }
                if(ConexionCuentaCxPGasto == null) {
                    CuentaCxPGasto = string.Empty;
                    CuentaCxPGastoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCxPGastoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCxPProveedores {
            get {
                return _ConexionCuentaCxPProveedores;
            }
            set {
                if(_ConexionCuentaCxPProveedores != value) {
                    _ConexionCuentaCxPProveedores = value;
                    if(ConexionCuentaCxPProveedores != null) {
                        CuentaCxPProveedores = ConexionCuentaCxPProveedores.Codigo;
                        CuentaCxPProveedoresDescripcion = ConexionCuentaCxPProveedores.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCxPProveedoresPropertyName);
                    RaisePropertyChanged(CuentaCxPProveedoresDescripcionPropertyName);
                }
                if(ConexionCuentaCxPProveedores == null) {
                    CuentaCxPProveedores = string.Empty;
                    CuentaCxPProveedoresDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCxPProveedoresDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaRetencionImpuestoMunicipal {
            get {
                return _ConexionCuentaRetencionImpuestoMunicipal;
            }
            set {
                if(_ConexionCuentaRetencionImpuestoMunicipal != value) {
                    _ConexionCuentaRetencionImpuestoMunicipal = value;
                    if(ConexionCuentaRetencionImpuestoMunicipal != null) {
                        CuentaRetencionImpuestoMunicipal = ConexionCuentaRetencionImpuestoMunicipal.Codigo;
                        CuentaRetencionImpuestoMunicipalDescripcion = ConexionCuentaRetencionImpuestoMunicipal.Descripcion;
                    }
                    RaisePropertyChanged(CuentaRetencionImpuestoMunicipalPropertyName);
                    RaisePropertyChanged(CuentaRetencionImpuestoMunicipalDescripcionPropertyName);
                }
                if(ConexionCuentaRetencionImpuestoMunicipal == null) {
                    CuentaRetencionImpuestoMunicipal = string.Empty;
                    CuentaRetencionImpuestoMunicipalDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaRetencionImpuestoMunicipalDescripcionPropertyName);
                }
            }
        }

        public FkTipoDeComprobanteViewModel ConexionCxPTipoComprobante {
            get {
                return _ConexionCxPTipoComprobante;
            }
            set {
                if(_ConexionCxPTipoComprobante != value) {
                    _ConexionCxPTipoComprobante = value;
                    RaisePropertyChanged(CxPTipoComprobantePropertyName);
                    if(ConexionCxPTipoComprobante != null) {
                        CxPTipoComprobante = ConexionCxPTipoComprobante.Codigo;
                    }
                }
                if(ConexionCxPTipoComprobante == null) {
                    CxPTipoComprobante = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCobranzaCobradoEnEfectivo {
            get {
                return _ConexionCuentaCobranzaCobradoEnEfectivo;
            }
            set {
                if(_ConexionCuentaCobranzaCobradoEnEfectivo != value) {
                    _ConexionCuentaCobranzaCobradoEnEfectivo = value;
                    if(ConexionCuentaCobranzaCobradoEnEfectivo != null) {
                        CuentaCobranzaCobradoEnEfectivo = ConexionCuentaCobranzaCobradoEnEfectivo.Codigo;
                        CuentaCobranzaCobradoEnEfectivoDescripcion = ConexionCuentaCobranzaCobradoEnEfectivo.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCobranzaCobradoEnEfectivoPropertyName);
                    RaisePropertyChanged(CuentaCobranzaCobradoEnEfectivoDescripcionPropertyName);
                }
                if(ConexionCuentaCobranzaCobradoEnEfectivo == null) {
                    CuentaCobranzaCobradoEnEfectivo = string.Empty;
                    CuentaCobranzaCobradoEnEfectivoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCobranzaCobradoEnEfectivoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCobranzaCobradoEnCheque {
            get {
                return _ConexionCuentaCobranzaCobradoEnCheque;
            }
            set {
                if(_ConexionCuentaCobranzaCobradoEnCheque != value) {
                    _ConexionCuentaCobranzaCobradoEnCheque = value;
                    if(ConexionCuentaCobranzaCobradoEnCheque != null) {
                        CuentaCobranzaCobradoEnCheque = ConexionCuentaCobranzaCobradoEnCheque.Codigo;
                        CuentaCobranzaCobradoEnChequeDescripcion = ConexionCuentaCobranzaCobradoEnCheque.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCobranzaCobradoEnChequePropertyName);
                    RaisePropertyChanged(CuentaCobranzaCobradoEnChequeDescripcionPropertyName);
                }
                if(ConexionCuentaCobranzaCobradoEnCheque == null) {
                    CuentaCobranzaCobradoEnCheque = string.Empty;
                    CuentaCobranzaCobradoEnChequeDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCobranzaCobradoEnChequeDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCobranzaCobradoEnTarjeta {
            get {
                return _ConexionCuentaCobranzaCobradoEnTarjeta;
            }
            set {
                if(_ConexionCuentaCobranzaCobradoEnTarjeta != value) {
                    _ConexionCuentaCobranzaCobradoEnTarjeta = value;
                    if(ConexionCuentaCobranzaCobradoEnTarjeta != null) {
                        CuentaCobranzaCobradoEnTarjeta = ConexionCuentaCobranzaCobradoEnTarjeta.Codigo;
                        CuentaCobranzaCobradoEnTarjetaDescripcion = ConexionCuentaCobranzaCobradoEnTarjeta.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCobranzaCobradoEnTarjetaPropertyName);
                    RaisePropertyChanged(CuentaCobranzaCobradoEnTarjetaDescripcionPropertyName);
                }
                if(ConexionCuentaCobranzaCobradoEnTarjeta == null) {
                    CuentaCobranzaCobradoEnTarjeta = string.Empty;
                    CuentaCobranzaCobradoEnTarjetaDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCobranzaCobradoEnTarjetaDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCobranzaRetencionISLR {
            get {
                return _ConexionCuentaCobranzaRetencionISLR;
            }
            set {
                if(_ConexionCuentaCobranzaRetencionISLR != value) {
                    _ConexionCuentaCobranzaRetencionISLR = value;
                    if(ConexionCuentaCobranzaRetencionISLR != null) {
                        CuentaCobranzaRetencionISLR = ConexionCuentaCobranzaRetencionISLR.Codigo;
                        CuentaCobranzaRetencionISLRDescripcion = ConexionCuentaCobranzaRetencionISLR.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCobranzaRetencionISLRPropertyName);
                    RaisePropertyChanged(CuentaCobranzaRetencionISLRDescripcionPropertyName);
                }
                if(ConexionCuentaCobranzaRetencionISLR == null) {
                    CuentaCobranzaRetencionISLR = string.Empty;
                    CuentaCobranzaRetencionISLRDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCobranzaRetencionISLRDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCobranzaRetencionIVA {
            get {
                return _ConexionCuentaCobranzaRetencionIVA;
            }
            set {
                if(_ConexionCuentaCobranzaRetencionIVA != value) {
                    _ConexionCuentaCobranzaRetencionIVA = value;
                    if(ConexionCuentaCobranzaRetencionIVA != null) {
                        CuentaCobranzaRetencionIVA = ConexionCuentaCobranzaRetencionIVA.Codigo;
                        CuentaCobranzaRetencionIVADescripcion = ConexionCuentaCobranzaRetencionIVA.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCobranzaRetencionIVAPropertyName);
                    RaisePropertyChanged(CuentaCobranzaRetencionIVADescripcionPropertyName);
                }
                if(ConexionCuentaCobranzaRetencionIVA == null) {
                    CuentaCobranzaRetencionIVA = string.Empty;
                    CuentaCobranzaRetencionIVADescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCobranzaRetencionIVADescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCobranzaOtros {
            get {
                return _ConexionCuentaCobranzaOtros;
            }
            set {
                if(_ConexionCuentaCobranzaOtros != value) {
                    _ConexionCuentaCobranzaOtros = value;
                    if(ConexionCuentaCobranzaOtros != null) {
                        CuentaCobranzaOtros = ConexionCuentaCobranzaOtros.Codigo;
                        CuentaCobranzaOtrosDescripcion = ConexionCuentaCobranzaOtros.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCobranzaOtrosPropertyName);
                    RaisePropertyChanged(CuentaCobranzaOtrosDescripcionPropertyName);
                }
                if(ConexionCuentaCobranzaOtros == null) {
                    CuentaCobranzaOtros = string.Empty;
                    CuentaCobranzaOtrosDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCobranzaOtrosDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCobranzaCxCClientes {
            get {
                return _ConexionCuentaCobranzaCxCClientes;
            }
            set {
                if(_ConexionCuentaCobranzaCxCClientes != value) {
                    _ConexionCuentaCobranzaCxCClientes = value;
                    if(ConexionCuentaCobranzaCxCClientes != null) {
                        CuentaCobranzaCxCClientes = ConexionCuentaCobranzaCxCClientes.Codigo;
                        CuentaCobranzaCxCClientesDescripcion = ConexionCuentaCobranzaCxCClientes.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCobranzaCxCClientesPropertyName);
                    RaisePropertyChanged(CuentaCobranzaCxCClientesDescripcionPropertyName);
                }
                if(ConexionCuentaCobranzaCxCClientes == null) {
                    CuentaCobranzaCxCClientes = string.Empty;
                    CuentaCobranzaCxCClientesDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCobranzaCxCClientesDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCobranzaCobradoAnticipo {
            get {
                return _ConexionCuentaCobranzaCobradoAnticipo;
            }
            set {
                if(_ConexionCuentaCobranzaCobradoAnticipo != value) {
                    _ConexionCuentaCobranzaCobradoAnticipo = value;
                    if(ConexionCuentaCobranzaCobradoAnticipo != null) {
                        CuentaCobranzaCobradoAnticipo = ConexionCuentaCobranzaCobradoAnticipo.Codigo;
                        CuentaCobranzaCobradoAnticipoDescripcion = ConexionCuentaCobranzaCobradoAnticipo.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCobranzaCobradoAnticipoPropertyName);
                    RaisePropertyChanged(CuentaCobranzaCobradoAnticipoDescripcionPropertyName);
                }
                if(ConexionCuentaCobranzaCobradoAnticipo == null) {
                    CuentaCobranzaCobradoAnticipo = string.Empty;
                    CuentaCobranzaCobradoAnticipoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCobranzaCobradoAnticipoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCobranzaIvaDiferido {
            get {
                return _ConexionCuentaCobranzaIvaDiferido;
            }
            set {
                if(_ConexionCuentaCobranzaIvaDiferido != value) {
                    _ConexionCuentaCobranzaIvaDiferido = value;
                    if(ConexionCuentaCobranzaIvaDiferido != null) {
                        CuentaCobranzaIvaDiferido = ConexionCuentaCobranzaIvaDiferido.Codigo;
                        CuentaCobranzaIvaDiferidoDescripcion = ConexionCuentaCobranzaIvaDiferido.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCobranzaIvaDiferidoPropertyName);
                    RaisePropertyChanged(CuentaCobranzaIvaDiferidoDescripcionPropertyName);
                }
                if(_ConexionCuentaCobranzaIvaDiferido == null) {
                    CuentaCobranzaIvaDiferido = string.Empty;
                    CuentaCobranzaIvaDiferidoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCobranzaIvaDiferidoDescripcionPropertyName);
                }
            }
        }

        public FkTipoDeComprobanteViewModel ConexionCobranzaTipoComprobante {
            get {
                return _ConexionCobranzaTipoComprobante;
            }
            set {
                if(_ConexionCobranzaTipoComprobante != value) {
                    _ConexionCobranzaTipoComprobante = value;
                    RaisePropertyChanged(CobranzaTipoComprobantePropertyName);
                    if(ConexionCobranzaTipoComprobante != null) {
                        CobranzaTipoComprobante = ConexionCobranzaTipoComprobante.Codigo;
                    }
                }
                if(ConexionCobranzaTipoComprobante == null) {
                    CobranzaTipoComprobante = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaPagosCxPProveedores {
            get {
                return _ConexionCuentaPagosCxPProveedores;
            }
            set {
                if(_ConexionCuentaPagosCxPProveedores != value) {
                    _ConexionCuentaPagosCxPProveedores = value;
                    if(ConexionCuentaPagosCxPProveedores != null) {
                        CuentaPagosCxPProveedores = ConexionCuentaPagosCxPProveedores.Codigo;
                        CuentaPagosCxPProveedoresDescripcion = ConexionCuentaPagosCxPProveedores.Descripcion;
                    }
                    RaisePropertyChanged(CuentaPagosCxPProveedoresPropertyName);
                    RaisePropertyChanged(CuentaPagosCxPProveedoresDescripcionPropertyName);
                }
                if(ConexionCuentaPagosCxPProveedores == null) {
                    CuentaPagosCxPProveedores = string.Empty;
                    CuentaPagosCxPProveedoresDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaPagosCxPProveedoresDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaPagosRetencionISLR {
            get {
                return _ConexionCuentaPagosRetencionISLR;
            }
            set {
                if(_ConexionCuentaPagosRetencionISLR != value) {
                    _ConexionCuentaPagosRetencionISLR = value;
                    if(ConexionCuentaPagosRetencionISLR != null) {
                        CuentaPagosRetencionISLR = ConexionCuentaPagosRetencionISLR.Codigo;
                        CuentaPagosRetencionISLRDescripcion = ConexionCuentaPagosRetencionISLR.Descripcion;
                    }
                    RaisePropertyChanged(CuentaPagosRetencionISLRPropertyName);
                    RaisePropertyChanged(CuentaPagosRetencionISLRDescripcionPropertyName);
                }
                if(ConexionCuentaPagosRetencionISLR == null) {
                    CuentaPagosRetencionISLR = string.Empty;
                    CuentaPagosRetencionISLRDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaPagosRetencionISLRDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaPagosOtros {
            get {
                return _ConexionCuentaPagosOtros;
            }
            set {
                if(_ConexionCuentaPagosOtros != value) {
                    _ConexionCuentaPagosOtros = value;
                    if(ConexionCuentaPagosOtros != null) {
                        CuentaPagosOtros = ConexionCuentaPagosOtros.Codigo;
                        CuentaPagosOtrosDescripcion = ConexionCuentaPagosOtros.Descripcion;
                    }
                    RaisePropertyChanged(CuentaPagosOtrosPropertyName);
                    RaisePropertyChanged(CuentaPagosOtrosDescripcionPropertyName);
                }
                if(ConexionCuentaPagosOtros == null) {
                    CuentaPagosOtros = string.Empty;
                    CuentaPagosOtrosDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaPagosOtrosDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaPagosBanco {
            get {
                return _ConexionCuentaPagosBanco;
            }
            set {
                if(_ConexionCuentaPagosBanco != value) {
                    _ConexionCuentaPagosBanco = value;
                    if(ConexionCuentaPagosBanco != null) {
                        CuentaPagosBanco = ConexionCuentaPagosBanco.Codigo;
                        CuentaPagosBancoDescripcion = ConexionCuentaPagosBanco.Descripcion;
                    }
                    RaisePropertyChanged(CuentaPagosBancoPropertyName);
                    RaisePropertyChanged(CuentaPagosBancoDescripcionPropertyName);
                }
                if(ConexionCuentaPagosBanco == null) {
                    CuentaPagosBanco = string.Empty;
                    CuentaPagosBancoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaPagosBancoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaPagosPagadoAnticipo {
            get {
                return _ConexionCuentaPagosPagadoAnticipo;
            }
            set {
                if(_ConexionCuentaPagosPagadoAnticipo != value) {
                    _ConexionCuentaPagosPagadoAnticipo = value;
                    if(ConexionCuentaPagosPagadoAnticipo != null) {
                        CuentaPagosPagadoAnticipo = ConexionCuentaPagosPagadoAnticipo.Codigo;
                        CuentaPagosPagadoAnticipoDescripcion = ConexionCuentaPagosPagadoAnticipo.Descripcion;
                    }
                    RaisePropertyChanged(CuentaPagosPagadoAnticipoPropertyName);
                    RaisePropertyChanged(CuentaPagosPagadoAnticipoDescripcionPropertyName);
                }
                if(ConexionCuentaPagosPagadoAnticipo == null) {
                    CuentaPagosPagadoAnticipo = string.Empty;
                    CuentaPagosPagadoAnticipoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaPagosPagadoAnticipoDescripcionPropertyName);
                }
            }
        }

        public FkTipoDeComprobanteViewModel ConexionPagoTipoComprobante {
            get {
                return _ConexionPagoTipoComprobante;
            }
            set {
                if(_ConexionPagoTipoComprobante != value) {
                    _ConexionPagoTipoComprobante = value;
                    RaisePropertyChanged(PagoTipoComprobantePropertyName);
                    if(ConexionPagoTipoComprobante != null) {
                        PagoTipoComprobante = ConexionPagoTipoComprobante.Codigo;
                    }
                }
                if(ConexionPagoTipoComprobante == null) {
                    PagoTipoComprobante = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaFacturacionCxCClientes {
            get {
                return _ConexionCuentaFacturacionCxCClientes;
            }
            set {
                if(_ConexionCuentaFacturacionCxCClientes != value) {
                    _ConexionCuentaFacturacionCxCClientes = value;
                    if(ConexionCuentaFacturacionCxCClientes != null) {
                        CuentaFacturacionCxCClientes = ConexionCuentaFacturacionCxCClientes.Codigo;
                        CuentaFacturacionCxCClientesDescripcion = ConexionCuentaFacturacionCxCClientes.Descripcion;
                    }
                    RaisePropertyChanged(CuentaFacturacionCxCClientesPropertyName);
                    RaisePropertyChanged(CuentaFacturacionCxCClientesDescripcionPropertyName);
                }
                if(ConexionCuentaFacturacionCxCClientes == null) {
                    CuentaFacturacionCxCClientes = string.Empty;
                    CuentaFacturacionCxCClientesDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaFacturacionCxCClientesDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaFacturacionMontoTotalFactura {
            get {
                return _ConexionCuentaFacturacionMontoTotalFactura;
            }
            set {
                if(_ConexionCuentaFacturacionMontoTotalFactura != value) {
                    _ConexionCuentaFacturacionMontoTotalFactura = value;
                    if(ConexionCuentaFacturacionMontoTotalFactura != null) {
                        CuentaFacturacionMontoTotalFactura = ConexionCuentaFacturacionMontoTotalFactura.Codigo;
                        CuentaFacturacionMontoTotalFacturaDescripcion = ConexionCuentaFacturacionMontoTotalFactura.Descripcion;
                    }
                    RaisePropertyChanged(CuentaFacturacionMontoTotalFacturaPropertyName);
                    RaisePropertyChanged(CuentaFacturacionMontoTotalFacturaDescripcionPropertyName);
                }
                if(ConexionCuentaFacturacionMontoTotalFactura == null) {
                    CuentaFacturacionMontoTotalFactura = string.Empty;
                    CuentaFacturacionMontoTotalFacturaDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaFacturacionMontoTotalFacturaDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaFacturacionCargos {
            get {
                return _ConexionCuentaFacturacionCargos;
            }
            set {
                if(_ConexionCuentaFacturacionCargos != value) {
                    _ConexionCuentaFacturacionCargos = value;
                    if(ConexionCuentaFacturacionCargos != null) {
                        CuentaFacturacionCargos = ConexionCuentaFacturacionCargos.Codigo;
                        CuentaFacturacionCargosDescripcion = ConexionCuentaFacturacionCargos.Descripcion;
                    }
                    RaisePropertyChanged(CuentaFacturacionCargosPropertyName);
                    RaisePropertyChanged(CuentaFacturacionCargosDescripcionPropertyName);
                }
                if(ConexionCuentaFacturacionCargos == null) {
                    CuentaFacturacionCargos = string.Empty;
                    CuentaFacturacionCargosDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaFacturacionCargosDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaFacturacionDescuentos {
            get {
                return _ConexionCuentaFacturacionDescuentos;
            }
            set {
                if(_ConexionCuentaFacturacionDescuentos != value) {
                    _ConexionCuentaFacturacionDescuentos = value;
                    if(ConexionCuentaFacturacionDescuentos != null) {
                        CuentaFacturacionDescuentos = ConexionCuentaFacturacionDescuentos.Codigo;
                        CuentaFacturacionDescuentosDescripcion = ConexionCuentaFacturacionDescuentos.Descripcion;
                    }
                    RaisePropertyChanged(CuentaFacturacionDescuentosPropertyName);
                    RaisePropertyChanged(CuentaFacturacionDescuentosDescripcionPropertyName);
                }
                if(ConexionCuentaFacturacionDescuentos == null) {
                    CuentaFacturacionDescuentos = string.Empty;
                    CuentaFacturacionDescuentosDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaFacturacionDescuentosDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaFacturacionIvaDiferido {
            get {
                return _ConexionCuentaFacturacionIvaDiferido;
            }
            set {
                if(_ConexionCuentaFacturacionIvaDiferido != value) {
                    _ConexionCuentaFacturacionIvaDiferido = value;
                    if(ConexionCuentaFacturacionIvaDiferido != null) {
                        CuentaFacturacionIvaDiferido = ConexionCuentaFacturacionIvaDiferido.Codigo;
                        CuentaFacturacionIvaDiferidoDescripcion = ConexionCuentaFacturacionIvaDiferido.Descripcion;
                    }
                    RaisePropertyChanged(CuentaFacturacionIvaDiferidoPropertyName);
                    RaisePropertyChanged(CuentaFacturacionIvaDiferidoDescripcionPropertyName);
                }
                if(_ConexionCuentaFacturacionIvaDiferido == null) {
                    CuentaFacturacionIvaDiferido = string.Empty;
                    CuentaFacturacionIvaDiferidoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaFacturacionIvaDiferidoDescripcionPropertyName);
                }
            }
        }

        public FkTipoDeComprobanteViewModel ConexionFacturaTipoComprobante {
            get {
                return _ConexionFacturaTipoComprobante;
            }
            set {
                if(_ConexionFacturaTipoComprobante != value) {
                    _ConexionFacturaTipoComprobante = value;
                    RaisePropertyChanged(FacturaTipoComprobantePropertyName);
                    if(ConexionFacturaTipoComprobante != null) {
                        FacturaTipoComprobante = ConexionFacturaTipoComprobante.Codigo;
                    }
                }
                if(ConexionFacturaTipoComprobante == null) {
                    FacturaTipoComprobante = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaRDVtasCaja {
            get {
                return _ConexionCuentaRDVtasCaja;
            }
            set {
                if(_ConexionCuentaRDVtasCaja != value) {
                    _ConexionCuentaRDVtasCaja = value;
                    if(ConexionCuentaRDVtasCaja != null) {
                        CuentaRDVtasCaja = ConexionCuentaRDVtasCaja.Codigo;
                        CuentaRDVtasCajaDescripcion = ConexionCuentaRDVtasCaja.Descripcion;
                    }
                    RaisePropertyChanged(CuentaRDVtasCajaPropertyName);
                    RaisePropertyChanged(CuentaRDVtasCajaDescripcionPropertyName);
                }
                if(ConexionCuentaRDVtasCaja == null) {
                    CuentaRDVtasCaja = string.Empty;
                    CuentaRDVtasCajaDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaRDVtasCajaDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaRDVtasMontoTotal {
            get {
                return _ConexionCuentaRDVtasMontoTotal;
            }
            set {
                if(_ConexionCuentaRDVtasMontoTotal != value) {
                    _ConexionCuentaRDVtasMontoTotal = value;
                    if(ConexionCuentaRDVtasMontoTotal != null) {
                        CuentaRDVtasMontoTotal = ConexionCuentaRDVtasMontoTotal.Codigo;
                        CuentaRDVtasMontoTotalDescripcion = ConexionCuentaRDVtasMontoTotal.Descripcion;
                    }
                    RaisePropertyChanged(CuentaRDVtasMontoTotalPropertyName);
                    RaisePropertyChanged(CuentaRDVtasMontoTotalDescripcionPropertyName);
                }
                if(ConexionCuentaRDVtasMontoTotal == null) {
                    CuentaRDVtasMontoTotal = string.Empty;
                    CuentaRDVtasMontoTotalDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaRDVtasMontoTotalDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaMovBancarioGasto {
            get {
                return _ConexionCuentaMovBancarioGasto;
            }
            set {
                if(_ConexionCuentaMovBancarioGasto != value) {
                    _ConexionCuentaMovBancarioGasto = value;
                    if(ConexionCuentaMovBancarioGasto != null) {
                        CuentaMovBancarioGasto = ConexionCuentaMovBancarioGasto.Codigo;
                        CuentaMovBancarioGastoDescripcion = ConexionCuentaMovBancarioGasto.Descripcion;
                    }
                    RaisePropertyChanged(CuentaMovBancarioGastoPropertyName);
                    RaisePropertyChanged(CuentaMovBancarioGastoDescripcionPropertyName);
                }
                if(ConexionCuentaMovBancarioGasto == null) {
                    CuentaMovBancarioGasto = string.Empty;
                    CuentaMovBancarioGastoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaMovBancarioGastoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaMovBancarioBancosHaber {
            get {
                return _ConexionCuentaMovBancarioBancosHaber;
            }
            set {
                if(_ConexionCuentaMovBancarioBancosHaber != value) {
                    _ConexionCuentaMovBancarioBancosHaber = value;
                    if(ConexionCuentaMovBancarioBancosHaber != null) {
                        CuentaMovBancarioBancosHaber = ConexionCuentaMovBancarioBancosHaber.Codigo;
                        CuentaMovBancarioBancosHaberDescripcion = ConexionCuentaMovBancarioBancosHaber.Descripcion;
                    }
                    RaisePropertyChanged(CuentaMovBancarioBancosHaberPropertyName);
                    RaisePropertyChanged(CuentaMovBancarioBancosHaberDescripcionPropertyName);
                }
                if(ConexionCuentaMovBancarioBancosHaber == null) {
                    CuentaMovBancarioBancosHaber = string.Empty;
                    CuentaMovBancarioBancosHaberDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaMovBancarioBancosHaberDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaMovBancarioBancosDebe {
            get {
                return _ConexionCuentaMovBancarioBancosDebe;
            }
            set {
                if(_ConexionCuentaMovBancarioBancosDebe != value) {
                    _ConexionCuentaMovBancarioBancosDebe = value;
                    if(ConexionCuentaMovBancarioBancosDebe != null) {
                        CuentaMovBancarioBancosDebe = ConexionCuentaMovBancarioBancosDebe.Codigo;
                        CuentaMovBancarioBancosDebeDescripcion = ConexionCuentaMovBancarioBancosDebe.Descripcion;
                    }
                    RaisePropertyChanged(CuentaMovBancarioBancosDebePropertyName);
                    RaisePropertyChanged(CuentaMovBancarioBancosDebeDescripcionPropertyName);
                }
                if(ConexionCuentaMovBancarioBancosDebe == null) {
                    CuentaMovBancarioBancosDebe = string.Empty;
                    CuentaMovBancarioBancosDebeDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaMovBancarioBancosDebeDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaMovBancarioIngresos {
            get {
                return _ConexionCuentaMovBancarioIngresos;
            }
            set {
                if(_ConexionCuentaMovBancarioIngresos != value) {
                    _ConexionCuentaMovBancarioIngresos = value;
                    if(ConexionCuentaMovBancarioIngresos != null) {
                        CuentaMovBancarioIngresos = ConexionCuentaMovBancarioIngresos.Codigo;
                        CuentaMovBancarioIngresosDescripcion = ConexionCuentaMovBancarioIngresos.Descripcion;
                    }
                    RaisePropertyChanged(CuentaMovBancarioIngresosPropertyName);
                    RaisePropertyChanged(CuentaMovBancarioIngresosDescripcionPropertyName);
                }
                if(ConexionCuentaMovBancarioIngresos == null) {
                    CuentaMovBancarioIngresos = string.Empty;
                    CuentaMovBancarioIngresosDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaMovBancarioIngresosDescripcionPropertyName);
                }
            }
        }

        public FkTipoDeComprobanteViewModel ConexionMovimientoBancarioTipoComprobante {
            get {
                return _ConexionMovimientoBancarioTipoComprobante;
            }
            set {
                if(_ConexionMovimientoBancarioTipoComprobante != value) {
                    _ConexionMovimientoBancarioTipoComprobante = value;
                    RaisePropertyChanged(MovimientoBancarioTipoComprobantePropertyName);
                    if(ConexionMovimientoBancarioTipoComprobante != null) {
                        MovimientoBancarioTipoComprobante = ConexionMovimientoBancarioTipoComprobante.Codigo;
                    }
                }
                if(ConexionMovimientoBancarioTipoComprobante == null) {
                    MovimientoBancarioTipoComprobante = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaDebitoBancarioGasto {
            get {
                return _ConexionCuentaDebitoBancarioGasto;
            }
            set {
                if(_ConexionCuentaDebitoBancarioGasto != value) {
                    _ConexionCuentaDebitoBancarioGasto = value;
                    if(ConexionCuentaDebitoBancarioGasto != null) {
                        CuentaDebitoBancarioGasto = ConexionCuentaDebitoBancarioGasto.Codigo;
                        CuentaDebitoBancarioGastoDescripcion = ConexionCuentaDebitoBancarioGasto.Descripcion;
                    }
                    RaisePropertyChanged(CuentaDebitoBancarioGastoPropertyName);
                    RaisePropertyChanged(CuentaDebitoBancarioGastoDescripcionPropertyName);
                }
                if(ConexionCuentaDebitoBancarioGasto == null) {
                    CuentaDebitoBancarioGasto = string.Empty;
                    CuentaDebitoBancarioGastoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaDebitoBancarioGastoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaDebitoBancarioBancos {
            get {
                return _ConexionCuentaDebitoBancarioBancos;
            }
            set {
                if(_ConexionCuentaDebitoBancarioBancos != value) {
                    _ConexionCuentaDebitoBancarioBancos = value;
                    if(ConexionCuentaDebitoBancarioBancos != null) {
                        CuentaDebitoBancarioBancos = ConexionCuentaDebitoBancarioBancos.Codigo;
                        CuentaDebitoBancarioBancosDescripcion = ConexionCuentaDebitoBancarioBancos.Descripcion;
                    }
                    RaisePropertyChanged(CuentaDebitoBancarioBancosPropertyName);
                    RaisePropertyChanged(CuentaDebitoBancarioBancosDescripcionPropertyName);
                }
                if(ConexionCuentaDebitoBancarioBancos == null) {
                    CuentaDebitoBancarioBancos = string.Empty;
                    CuentaDebitoBancarioBancosDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaDebitoBancarioBancosDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCreditoBancarioGasto {
            get {
                return _ConexionCuentaCreditoBancarioGasto;
            }
            set {
                if(_ConexionCuentaCreditoBancarioGasto != value) {
                    _ConexionCuentaCreditoBancarioGasto = value;
                    if(ConexionCuentaCreditoBancarioGasto != null) {
                        CuentaCreditoBancarioGasto = ConexionCuentaCreditoBancarioGasto.Codigo;
                        CuentaCreditoBancarioGastoDescripcion = ConexionCuentaCreditoBancarioGasto.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCreditoBancarioGastoPropertyName);
                    RaisePropertyChanged(CuentaCreditoBancarioGastoDescripcionPropertyName);
                }
                if(ConexionCuentaCreditoBancarioGasto == null) {
                    CuentaCreditoBancarioGasto = string.Empty;
                    CuentaCreditoBancarioGastoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCreditoBancarioGastoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCreditoBancarioBancos {
            get {
                return _ConexionCuentaCreditoBancarioBancos;
            }
            set {
                if(_ConexionCuentaCreditoBancarioBancos != value) {
                    _ConexionCuentaCreditoBancarioBancos = value; ;
                    if(ConexionCuentaCreditoBancarioBancos != null) {
                        CuentaCreditoBancarioBancos = ConexionCuentaCreditoBancarioBancos.Codigo;
                        CuentaCreditoBancarioBancosDescripcion = ConexionCuentaCreditoBancarioBancos.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCreditoBancarioBancosPropertyName);
                    RaisePropertyChanged(CuentaCreditoBancarioBancosDescripcionPropertyName);
                }
                if(ConexionCuentaCreditoBancarioBancos == null) {
                    CuentaCreditoBancarioBancos = string.Empty;
                    CuentaCreditoBancarioBancosDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCreditoBancarioBancosDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaAnticipoCaja {
            get {
                return _ConexionCuentaAnticipoCaja;
            }
            set {
                if(_ConexionCuentaAnticipoCaja != value) {
                    _ConexionCuentaAnticipoCaja = value;
                    if(ConexionCuentaAnticipoCaja != null) {
                        CuentaAnticipoCaja = ConexionCuentaAnticipoCaja.Codigo;
                        CuentaAnticipoCajaDescripcion = ConexionCuentaAnticipoCaja.Descripcion;
                    }
                    RaisePropertyChanged(CuentaAnticipoCajaPropertyName);
                    RaisePropertyChanged(CuentaAnticipoCajaDescripcionPropertyName);
                }
                if(ConexionCuentaAnticipoCaja == null) {
                    CuentaAnticipoCaja = string.Empty;
                    CuentaAnticipoCajaDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaAnticipoCajaDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaAnticipoCobrado {
            get {
                return _ConexionCuentaAnticipoCobrado;
            }
            set {
                if(_ConexionCuentaAnticipoCobrado != value) {
                    _ConexionCuentaAnticipoCobrado = value;
                    if(ConexionCuentaAnticipoCobrado != null) {
                        CuentaAnticipoCobrado = ConexionCuentaAnticipoCobrado.Codigo;
                        CuentaAnticipoCobradoDescripcion = ConexionCuentaAnticipoCobrado.Descripcion;
                    }
                    RaisePropertyChanged(CuentaAnticipoCobradoPropertyName);
                    RaisePropertyChanged(CuentaAnticipoCobradoDescripcionPropertyName);
                }
                if(ConexionCuentaAnticipoCobrado == null) {
                    CuentaAnticipoCobrado = string.Empty;
                    CuentaAnticipoCobradoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaAnticipoCobradoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaAnticipoOtrosIngresos {
            get {
                return _ConexionCuentaAnticipoOtrosIngresos;
            }
            set {
                if(_ConexionCuentaAnticipoOtrosIngresos != value) {
                    _ConexionCuentaAnticipoOtrosIngresos = value;
                    if(ConexionCuentaAnticipoOtrosIngresos != null) {
                        CuentaAnticipoOtrosIngresos = ConexionCuentaAnticipoOtrosIngresos.Codigo;
                        CuentaAnticipoOtrosIngresosDescripcion = ConexionCuentaAnticipoOtrosIngresos.Descripcion;
                    }
                    RaisePropertyChanged(CuentaAnticipoOtrosIngresosPropertyName);
                    RaisePropertyChanged(CuentaAnticipoOtrosIngresosDescripcionPropertyName);
                }
                if(ConexionCuentaAnticipoOtrosIngresos == null) {
                    CuentaAnticipoOtrosIngresos = string.Empty;
                    CuentaAnticipoOtrosIngresosDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaAnticipoOtrosIngresosDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaAnticipoPagado {
            get {
                return _ConexionCuentaAnticipoPagado;
            }
            set {
                if(_ConexionCuentaAnticipoPagado != value) {
                    _ConexionCuentaAnticipoPagado = value;
                    if(ConexionCuentaAnticipoPagado != null) {
                        CuentaAnticipoPagado = ConexionCuentaAnticipoPagado.Codigo;
                        CuentaAnticipoPagadoDescripcion = ConexionCuentaAnticipoPagado.Descripcion;
                    }
                    RaisePropertyChanged(CuentaAnticipoPagadoPropertyName);
                    RaisePropertyChanged(CuentaAnticipoPagadoDescripcionPropertyName);
                }
                if(ConexionCuentaAnticipoPagado == null) {
                    CuentaAnticipoPagado = string.Empty;
                    CuentaAnticipoPagadoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaAnticipoPagadoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaAnticipoBanco {
            get {
                return _ConexionCuentaAnticipoBanco;
            }
            set {
                if(_ConexionCuentaAnticipoBanco != value) {
                    _ConexionCuentaAnticipoBanco = value;
                    if(ConexionCuentaAnticipoBanco != null) {
                        CuentaAnticipoBanco = ConexionCuentaAnticipoBanco.Codigo;
                        CuentaAnticipoBancoDescripcion = ConexionCuentaAnticipoBanco.Descripcion;
                    }
                    RaisePropertyChanged(CuentaAnticipoBancoPropertyName);
                    RaisePropertyChanged(CuentaAnticipoBancoDescripcionPropertyName);
                }
                if(ConexionCuentaAnticipoBanco == null) {
                    CuentaAnticipoBanco = string.Empty;
                    CuentaAnticipoBancoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaAnticipoBancoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaAnticipoOtrosEgresos {
            get {
                return _ConexionCuentaAnticipoOtrosEgresos;
            }
            set {
                if(_ConexionCuentaAnticipoOtrosEgresos != value) {
                    _ConexionCuentaAnticipoOtrosEgresos = value;
                    if(ConexionCuentaAnticipoOtrosEgresos != null) {
                        CuentaAnticipoOtrosEgresos = ConexionCuentaAnticipoOtrosEgresos.Codigo;
                        CuentaAnticipoOtrosEgresosDescripcion = ConexionCuentaAnticipoOtrosEgresos.Descripcion;
                    }
                    RaisePropertyChanged(CuentaAnticipoOtrosEgresosPropertyName);
                    RaisePropertyChanged(CuentaAnticipoOtrosEgresosDescripcionPropertyName);
                }
                if(ConexionCuentaAnticipoOtrosEgresos == null) {
                    CuentaAnticipoOtrosEgresos = string.Empty;
                    CuentaAnticipoOtrosEgresosDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaAnticipoOtrosEgresosDescripcionPropertyName);
                }
            }
        }

        public FkTipoDeComprobanteViewModel ConexionAnticipoTipoComprobante {
            get {
                return _ConexionAnticipoTipoComprobante;
            }
            set {
                if(_ConexionAnticipoTipoComprobante != value) {
                    _ConexionAnticipoTipoComprobante = value;
                    RaisePropertyChanged(AnticipoTipoComprobantePropertyName);
                    if(ConexionAnticipoTipoComprobante != null) {
                        AnticipoTipoComprobante = ConexionAnticipoTipoComprobante.Codigo;
                    }
                }
                if(ConexionAnticipoTipoComprobante == null) {
                    AnticipoTipoComprobante = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCostoDeVenta {
            get {
                return _ConexionCuentaCostoDeVenta;
            }
            set {
                if(_ConexionCuentaCostoDeVenta != value) {
                    _ConexionCuentaCostoDeVenta = value;
                    if(ConexionCuentaCostoDeVenta != null) {
                        CuentaCostoDeVenta = ConexionCuentaCostoDeVenta.Codigo;
                        CuentaCostoDeVentaDescripcion = ConexionCuentaCostoDeVenta.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCostoDeVentaPropertyName);
                    RaisePropertyChanged(CuentaCostoDeVentaDescripcionPropertyName);
                }
                if(ConexionCuentaCostoDeVenta == null) {
                    CuentaCostoDeVenta = string.Empty;
                    CuentaCostoDeVentaDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCostoDeVentaDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaInventario {
            get {
                return _ConexionCuentaInventario;
            }
            set {
                if(_ConexionCuentaInventario != value) {
                    _ConexionCuentaInventario = value;
                    if(ConexionCuentaInventario != null) {
                        CuentaInventario = ConexionCuentaInventario.Codigo;
                        CuentaInventarioDescripcion = ConexionCuentaInventario.Descripcion;
                    }
                    RaisePropertyChanged(CuentaInventarioPropertyName);
                    RaisePropertyChanged(CuentaInventarioDescripcionPropertyName);
                }
                if(ConexionCuentaInventario == null) {
                    CuentaInventario = string.Empty;
                    CuentaInventarioDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaInventarioDescripcionPropertyName);
                }
            }
        }

        public FkTipoDeComprobanteViewModel ConexionInventarioTipoComprobante {
            get {
                return _ConexionInventarioTipoComprobante;
            }
            set {
                if(_ConexionInventarioTipoComprobante != value) {
                    _ConexionInventarioTipoComprobante = value;
                    RaisePropertyChanged(InventarioTipoComprobantePropertyName);
                    if(ConexionInventarioTipoComprobante != null) {
                        InventarioTipoComprobante = ConexionInventarioTipoComprobante.Codigo;
                    }
                }
                if(ConexionInventarioTipoComprobante == null) {
                    InventarioTipoComprobante = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCtaDePagosSueldos {
            get {
                return _ConexionCtaDePagosSueldos;
            }
            set {
                if(_ConexionCtaDePagosSueldos != value) {
                    _ConexionCtaDePagosSueldos = value;
                    if(ConexionCtaDePagosSueldos != null) {
                        CtaDePagosSueldos = ConexionCtaDePagosSueldos.Codigo;
                        CtaDePagosSueldosDescripcion = ConexionCtaDePagosSueldos.Descripcion;
                    }
                    RaisePropertyChanged(CtaDePagosSueldosPropertyName);
                    RaisePropertyChanged(CtaDePagosSueldosDescripcionPropertyName);
                }
                if(ConexionCtaDePagosSueldos == null) {
                    CtaDePagosSueldos = string.Empty;
                    CtaDePagosSueldosDescripcion = string.Empty;
                    RaisePropertyChanged(CtaDePagosSueldosDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCtaDePagosSueldosBanco {
            get {
                return _ConexionCtaDePagosSueldosBanco;
            }
            set {
                if(_ConexionCtaDePagosSueldosBanco != value) {
                    _ConexionCtaDePagosSueldosBanco = value;
                    if(ConexionCtaDePagosSueldosBanco != null) {
                        CtaDePagosSueldosBanco = ConexionCtaDePagosSueldosBanco.Codigo;
                        CtaDePagosSueldosBancoDescripcion = ConexionCtaDePagosSueldosBanco.Descripcion;
                    }
                    RaisePropertyChanged(CtaDePagosSueldosBancoPropertyName);
                    RaisePropertyChanged(CtaDePagosSueldosBancoDescripcionPropertyName);
                }
                if(ConexionCtaDePagosSueldosBanco == null) {
                    CtaDePagosSueldosBanco = string.Empty;
                    CtaDePagosSueldosBancoDescripcion = string.Empty;
                    RaisePropertyChanged(CtaDePagosSueldosBancoDescripcionPropertyName);
                }
            }
        }

        public FkTipoDeComprobanteViewModel ConexionPagosSueldosTipoComprobante {
            get {
                return _ConexionPagosSueldosTipoComprobante;
            }
            set {
                if(_ConexionPagosSueldosTipoComprobante != value) {
                    _ConexionPagosSueldosTipoComprobante = value;
                    RaisePropertyChanged(PagosSueldosTipoComprobantePropertyName);
                    if(ConexionPagosSueldosTipoComprobante != null) {
                        PagosSueldosTipoComprobante = ConexionPagosSueldosTipoComprobante.Codigo;
                    }
                }
                if(ConexionPagosSueldosTipoComprobante == null) {
                    PagosSueldosTipoComprobante = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCajaChicaGasto {
            get {
                return _ConexionCuentaCajaChicaGasto;
            }
            set {
                if(_ConexionCuentaCajaChicaGasto != value) {
                    _ConexionCuentaCajaChicaGasto = value;
                    if(ConexionCuentaCajaChicaGasto != null) {
                        CuentaCajaChicaGasto = ConexionCuentaCajaChicaGasto.Codigo;
                        CuentaCajaChicaGastoDescripcion = ConexionCuentaCajaChicaGasto.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCajaChicaGastoPropertyName);
                    RaisePropertyChanged(CuentaCajaChicaGastoDescripcionPropertyName);
                }
                if(ConexionCuentaCajaChicaGasto == null) {
                    CuentaCajaChicaGasto = string.Empty;
                    CuentaCajaChicaGastoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCajaChicaGastoDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCajaChicaBancoHaber {
            get {
                return _ConexionCuentaCajaChicaBancoHaber;
            }
            set {
                if(_ConexionCuentaCajaChicaBancoHaber != value) {
                    _ConexionCuentaCajaChicaBancoHaber = value;
                    if(ConexionCuentaCajaChicaBancoHaber != null) {
                        CuentaCajaChicaBancoHaber = ConexionCuentaCajaChicaBancoHaber.Codigo;
                        CuentaCajaChicaBancoHaberDescripcion = ConexionCuentaCajaChicaBancoHaber.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCajaChicaBancoHaberPropertyName);
                    RaisePropertyChanged(CuentaCajaChicaBancoHaberDescripcionPropertyName);
                }
                if(ConexionCuentaCajaChicaBancoHaber == null) {
                    CuentaCajaChicaBancoHaber = string.Empty;
                    CuentaCajaChicaBancoHaberDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCajaChicaBancoHaberDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCajaChicaBancoDebe {
            get {
                return _ConexionCuentaCajaChicaBancoDebe;
            }
            set {
                if(_ConexionCuentaCajaChicaBancoDebe != value) {
                    _ConexionCuentaCajaChicaBancoDebe = value;
                    if(ConexionCuentaCajaChicaBancoDebe != null) {
                        CuentaCajaChicaBancoDebe = ConexionCuentaCajaChicaBancoDebe.Codigo;
                        CuentaCajaChicaBancoDebeDescripcion = ConexionCuentaCajaChicaBancoDebe.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCajaChicaBancoDebePropertyName);
                    RaisePropertyChanged(CuentaCajaChicaBancoDebeDescripcionPropertyName);
                }
                if(ConexionCuentaCajaChicaBancoDebe == null) {
                    CuentaCajaChicaBancoDebe = string.Empty;
                    CuentaCajaChicaBancoDebeDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCajaChicaBancoDebeDescripcionPropertyName);
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaCajaChicaBanco {
            get {
                return _ConexionCuentaCajaChicaBanco;
            }
            set {
                if(_ConexionCuentaCajaChicaBanco != value) {
                    _ConexionCuentaCajaChicaBanco = value;
                    if(ConexionCuentaCajaChicaBanco != null) {
                        CuentaCajaChicaBanco = ConexionCuentaCajaChicaBanco.Codigo;
                        CuentaCajaChicaBancoDescripcion = ConexionCuentaCajaChicaBanco.Descripcion;
                    }
                    RaisePropertyChanged(CuentaCajaChicaBancoPropertyName);
                    RaisePropertyChanged(CuentaCajaChicaBancoDescripcionPropertyName);
                }
                if(ConexionCuentaCajaChicaBanco == null) {
                    CuentaCajaChicaBanco = string.Empty;
                    CuentaCajaChicaBancoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaCajaChicaBancoDescripcionPropertyName);
                }
            }
        }

        public FkTipoDeComprobanteViewModel ConexionSiglasTipoComprobanteCajaChica {
            get {
                return _ConexionSiglasTipoComprobanteCajaChica;
            }
            set {
                if(_ConexionSiglasTipoComprobanteCajaChica != value) {
                    _ConexionSiglasTipoComprobanteCajaChica = value;
                    RaisePropertyChanged(SiglasTipoComprobanteCajaChicaPropertyName);
                    if(ConexionSiglasTipoComprobanteCajaChica != null) {
                        SiglasTipoComprobanteCajaChica = ConexionSiglasTipoComprobanteCajaChica.Codigo;
                    }
                }
                if(ConexionSiglasTipoComprobanteCajaChica == null) {
                    SiglasTipoComprobanteCajaChica = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaRendicionesGasto {
            get {
                return _ConexionCuentaRendicionesGasto;
            }
            set {
                if(_ConexionCuentaRendicionesGasto != value) {
                    _ConexionCuentaRendicionesGasto = value;
                    if(ConexionCuentaRendicionesGasto != null) {
                        CuentaRendicionesGasto = ConexionCuentaRendicionesGasto.Codigo;
                    }
                    RaisePropertyChanged(CuentaRendicionesGastoPropertyName);
                }
                if(ConexionCuentaRendicionesGasto == null) {
                    CuentaRendicionesGasto = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaRendicionesBanco {
            get {
                return _ConexionCuentaRendicionesBanco;
            }
            set {
                if(_ConexionCuentaRendicionesBanco != value) {
                    _ConexionCuentaRendicionesBanco = value;
                    RaisePropertyChanged(CuentaRendicionesBancoPropertyName);
                    if(ConexionCuentaRendicionesBanco != null) {
                        CuentaRendicionesBanco = ConexionCuentaRendicionesBanco.Codigo;
                    }
                }
                if(ConexionCuentaRendicionesBanco == null) {
                    CuentaRendicionesBanco = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaRendicionesAnticipos {
            get {
                return _ConexionCuentaRendicionesAnticipos;
            }
            set {
                if(_ConexionCuentaRendicionesAnticipos != value) {
                    _ConexionCuentaRendicionesAnticipos = value;
                    RaisePropertyChanged(CuentaRendicionesAnticiposPropertyName);
                    if(ConexionCuentaRendicionesAnticipos != null) {
                        CuentaRendicionesAnticipos = ConexionCuentaRendicionesAnticipos.Codigo;
                    }
                }
                if(ConexionCuentaRendicionesAnticipos == null) {
                    CuentaRendicionesAnticipos = string.Empty;
                }
            }
        }

        public FkTipoDeComprobanteViewModel ConexionSiglasTipoComprobanteRendiciones {
            get {
                return _ConexionSiglasTipoComprobanteRendiciones;
            }
            set {
                if(_ConexionSiglasTipoComprobanteRendiciones != value) {
                    _ConexionSiglasTipoComprobanteRendiciones = value;
                    RaisePropertyChanged(SiglasTipoComprobanteRendicionesPropertyName);
                    if(ConexionSiglasTipoComprobanteRendiciones != null) {
                        SiglasTipoComprobanteRendiciones = ConexionSiglasTipoComprobanteRendiciones.Codigo;
                    }
                }
                if(ConexionSiglasTipoComprobanteRendiciones == null) {
                    SiglasTipoComprobanteRendiciones = string.Empty;
                }
            }
        }
		public FkCuentaViewModel ConexionCuentaTransfCtasBancoDestino {
            get {
                return _ConexionCuentaTransfCtasBancoDestino;
            }
            set {
                if (_ConexionCuentaTransfCtasBancoDestino != value) {
                    _ConexionCuentaTransfCtasBancoDestino = value;
                    if (ConexionCuentaTransfCtasBancoDestino != null) {
                        CuentaTransfCtasBancoDestino = ConexionCuentaTransfCtasBancoDestino.Codigo;
                        CuentaTransfCtasBancoDestinoDescripcion = ConexionCuentaTransfCtasBancoDestino.Descripcion;
                    }
                    RaisePropertyChanged(CuentaTransfCtasBancoDestinoPropertyName);
                    RaisePropertyChanged(CuentaTransfCtasBancoDestinoDescripcionPropertyName);
                }
                if (_ConexionCuentaTransfCtasBancoDestino == null) {
                    CuentaTransfCtasBancoDestino = string.Empty;
                    CuentaTransfCtasBancoDestinoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaTransfCtasBancoDestinoDescripcionPropertyName);
                }
            }
        }
        public FkCuentaViewModel ConexionCuentaTransfCtasGastoComOrigen {
            get {
                return _ConexionCuentaTransfCtasGastoComOrigen;
            }
            set {
                if (_ConexionCuentaTransfCtasGastoComOrigen != value) {
                    _ConexionCuentaTransfCtasGastoComOrigen = value;
                    if (ConexionCuentaTransfCtasGastoComOrigen != null) {
                        CuentaTransfCtasGastoComOrigen = ConexionCuentaTransfCtasGastoComOrigen.Codigo;
                        CuentaTransfCtasGastoComOrigenDescripcion = ConexionCuentaTransfCtasGastoComOrigen.Descripcion;
                    }
                    RaisePropertyChanged(CuentaTransfCtasGastoComOrigenPropertyName);
                    RaisePropertyChanged(CuentaTransfCtasGastoComOrigenDescripcionPropertyName);
                }
                if (_ConexionCuentaTransfCtasGastoComOrigen == null) {
                    CuentaTransfCtasGastoComOrigen = string.Empty;
                    CuentaTransfCtasGastoComOrigenDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaTransfCtasGastoComOrigenDescripcionPropertyName);
                }
            }
        }
        public FkCuentaViewModel ConexionCuentaTransfCtasGastoComDestino {
            get {
                return _ConexionCuentaTransfCtasGastoComDestino;
            }
            set {
                if (_ConexionCuentaTransfCtasGastoComDestino != value) {
                    _ConexionCuentaTransfCtasGastoComDestino = value;
                    if (ConexionCuentaTransfCtasGastoComDestino != null) {
                        CuentaTransfCtasGastoComDestino = ConexionCuentaTransfCtasGastoComDestino.Codigo;
                        CuentaTransfCtasGastoComDestinoDescripcion = ConexionCuentaTransfCtasGastoComDestino.Descripcion;
                    }
                    RaisePropertyChanged(CuentaTransfCtasGastoComDestinoPropertyName);
                    RaisePropertyChanged(CuentaTransfCtasGastoComDestinoDescripcionPropertyName);
                }
                if (_ConexionCuentaTransfCtasGastoComDestino == null) {
                    CuentaTransfCtasGastoComDestino = string.Empty;
                    CuentaTransfCtasGastoComDestinoDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaTransfCtasGastoComDestinoDescripcionPropertyName);
                }
            }
        }
        public FkCuentaViewModel ConexionCuentaTransfCtasBancoOrigen {
            get {
                return _ConexionCuentaTransfCtasBancoOrigen;
            }
            set {
                if (_ConexionCuentaTransfCtasBancoOrigen != value) {
                    _ConexionCuentaTransfCtasBancoOrigen = value;
                    CuentaTransfCtasBancoOrigen = ConexionCuentaTransfCtasBancoOrigen.Codigo;
                    CuentaTransfCtasBancoOrigenDescripcion = ConexionCuentaTransfCtasBancoOrigen.Descripcion;
                }
                RaisePropertyChanged(CuentaTransfCtasBancoOrigenPropertyName);
                RaisePropertyChanged(CuentaTransfCtasBancoOrigenDescripcionPropertyName);

                if (_ConexionCuentaTransfCtasBancoOrigen == null) {
                    CuentaTransfCtasBancoOrigen = string.Empty;
                    CuentaTransfCtasBancoOrigenDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaTransfCtasBancoOrigenDescripcionPropertyName);
                }
            }
        }
        public FkTipoDeComprobanteViewModel ConexionTransfCtasSigasTipoComprobante {
            get {
                return _ConexionTransfCtasSigasTipoComprobante;
            }
            set {
                if (_ConexionTransfCtasSigasTipoComprobante != value) {
                    _ConexionTransfCtasSigasTipoComprobante = value;
                    if (_ConexionTransfCtasSigasTipoComprobante != null) {
                        TransfCtasSigasTipoComprobante = _ConexionTransfCtasSigasTipoComprobante.Codigo;
                    }
                    RaisePropertyChanged(TransfCtasSigasTipoComprobantePropertyName);
                }
                if (_ConexionTransfCtasSigasTipoComprobante == null) {
                    TransfCtasSigasTipoComprobante = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaOrdenDeProduccionProductoTerminado {
            get {
                return _ConexionCuentaOrdenDeProduccionProductoTerminado;
            }
            set {
                if (_ConexionCuentaOrdenDeProduccionProductoTerminado != value) {
                    _ConexionCuentaOrdenDeProduccionProductoTerminado = value;
                    RaisePropertyChanged(CuentaOrdenDeProduccionProductoTerminadoPropertyName);
                }
                if (_ConexionCuentaOrdenDeProduccionProductoTerminado == null) {
                    CuentaOrdenDeProduccionProductoTerminado = string.Empty;
                }
            }
        }

        public FkCuentaViewModel ConexionCuentaOrdenDeProduccionMateriaPrima {
            get {
                return _ConexionCuentaOrdenDeProduccionMateriaPrima;
            }
            set {
                if (_ConexionCuentaOrdenDeProduccionMateriaPrima != value) {
                    _ConexionCuentaOrdenDeProduccionMateriaPrima = value;
                    RaisePropertyChanged(CuentaOrdenDeProduccionMateriaPrimaPropertyName);
                }
                if (_ConexionCuentaOrdenDeProduccionMateriaPrima == null) {
                    CuentaOrdenDeProduccionMateriaPrima = string.Empty;
                }
            }
        }

        public FkTipoDeComprobanteViewModel ConexionOrdenDeProduccionTipoComprobante {
            get {
                return _ConexionOrdenDeProduccionTipoComprobante;
            }
            set {
                if (_ConexionOrdenDeProduccionTipoComprobante != value) {
                    _ConexionOrdenDeProduccionTipoComprobante = value;
                    RaisePropertyChanged(OrdenDeProduccionTipoComprobantePropertyName);
                }
                if (_ConexionOrdenDeProduccionTipoComprobante == null) {
                    OrdenDeProduccionTipoComprobante = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCuentaIva1CreditoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaIva1DebitoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaRetencionIvaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseDiferenciaEnCambioyCalculoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaDiferenciaCambiariaCommand {
            get;
            private set;
        }



        public RelayCommand<string> ChooseCuentaCxCClientesCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCxCIngresosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCxCTipoComprobanteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCxPGastoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCxPProveedoresCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaRetencionImpuestoMunicipalCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCxPTipoComprobanteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCobranzaCobradoEnEfectivoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCobranzaCobradoEnChequeCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCobranzaCobradoEnTarjetaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCobranzaRetencionISLRCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCobranzaRetencionIVACommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCobranzaOtrosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCobranzaCxCClientesCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCobranzaCobradoAnticipoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCobranzaIvaDiferidoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCobranzaTipoComprobanteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaPagosCxPProveedoresCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaPagosRetencionISLRCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaPagosOtrosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaPagosBancoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaPagosPagadoAnticipoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChoosePagoTipoComprobanteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaFacturacionCxCClientesCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaFacturacionMontoTotalFacturaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaFacturacionCargosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaFacturacionDescuentosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaFacturacionIvaDiferidoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseFacturaTipoComprobanteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaRDVtasCajaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaRDVtasMontoTotalCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaMovBancarioGastoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaMovBancarioBancosHaberCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaMovBancarioBancosDebeCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaMovBancarioIngresosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseMovimientoBancarioTipoComprobanteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaDebitoBancarioGastoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaDebitoBancarioBancosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCreditoBancarioGastoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCreditoBancarioBancosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaAnticipoCajaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaAnticipoCobradoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaAnticipoOtrosIngresosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaAnticipoPagadoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaAnticipoBancoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaAnticipoOtrosEgresosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseAnticipoTipoComprobanteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCostoDeVentaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaInventarioCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseInventarioTipoComprobanteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCtaDePagosSueldosCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCtaDePagosSueldosBancoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChoosePagosSueldosTipoComprobanteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCajaChicaGastoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCajaChicaBancoHaberCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCajaChicaBancoDebeCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaCajaChicaBancoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseSiglasTipoComprobanteCajaChicaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaRendicionesGastoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaRendicionesBancoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaRendicionesAnticiposCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseSiglasTipoComprobanteRendicionesCommand {
            get;
            private set;
        }
        public RelayCommand<string> ChooseCuentaTransfCtasBancoDestinoCommand {
            get;
            private set;
        }
        public RelayCommand<string> ChooseCuentaTransfCtasGastoComOrigenCommand {
            get;
            private set;
        }
        public RelayCommand<string> ChooseCuentaTransfCtasGastoComDestinoCommand {
            get;
            private set;
        }
        public RelayCommand<string> ChooseCuentaTransfCtasBancoOrigenCommand {
            get;
            private set;
        }
        public RelayCommand<string> ChooseTransfCtasSigasTipoComprobanteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaOrdenDeProduccionProductoTerminadoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaOrdenDeProduccionMateriaPrimaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseOrdenDeProduccionTipoComprobanteCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public ReglasDeContabilizacionViewModel()
           : this(new ReglasDeContabilizacion(),eAccionSR.Insertar) {
        }
        public ReglasDeContabilizacionViewModel(ReglasDeContabilizacion initModel,eAccionSR initAction)
           : base(initModel,initAction,LibGlobalValues.Instance.GetAppMemInfo(),LibGlobalValues.Instance.GetMfcInfo()) {
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            InicializarVariablesGlobales();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(ReglasDeContabilizacion valModel) {
            base.InitializeLookAndFeel(valModel);
            if(LibString.IsNullOrEmpty(Numero,true)) {
                Numero = GenerarProximoNumero();
            }
        }

        protected override ReglasDeContabilizacion FindCurrentRecord(ReglasDeContabilizacion valModel) {
            if(valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valModel.ConsecutivoCompania);
            return BusinessComponent.GetData(eProcessMessageType.SpName,"ReglasDeContabilizacionGET",vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<ReglasDeContabilizacion>,IList<ReglasDeContabilizacion>> GetBusinessComponent() {
            return new clsReglasDeContabilizacionNav();
        }

        private string GenerarProximoNumero() {
            string vResult = string.Empty;
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message,"ProximoNumero",Mfc.GetIntAsParam("Compania"));
            vResult = LibXml.GetPropertyString(vResulset,"Numero");
            return vResult;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCuentaIva1CreditoCommand = new RelayCommand<string>(ExecuteChooseCuentaIva1CreditoCommand);
            ChooseCuentaIva1DebitoCommand = new RelayCommand<string>(ExecuteChooseCuentaIva1DebitoCommand);
            ChooseCuentaRetencionIvaCommand = new RelayCommand<string>(ExecuteChooseCuentaRetencionIvaCommand);
            ChooseDiferenciaEnCambioyCalculoCommand = new RelayCommand<string>(ExecuteChooseDiferenciaEnCambioyCalculoCommand);
            ChooseCuentaDiferenciaCambiariaCommand = new RelayCommand<string>(ExecuteChooseCuentaDiferenciaCambiariaCommand);
            ChooseCuentaCxCClientesCommand = new RelayCommand<string>(ExecuteChooseCuentaCxCClientesCommand);
            ChooseCuentaCxCIngresosCommand = new RelayCommand<string>(ExecuteChooseCuentaCxCIngresosCommand);
            ChooseCxCTipoComprobanteCommand = new RelayCommand<string>(ExecuteChooseCxCTipoComprobanteCommand);
            ChooseCuentaCxPGastoCommand = new RelayCommand<string>(ExecuteChooseCuentaCxPGastoCommand);
            ChooseCuentaCxPProveedoresCommand = new RelayCommand<string>(ExecuteChooseCuentaCxPProveedoresCommand);
            ChooseCuentaRetencionImpuestoMunicipalCommand = new RelayCommand<string>(ExecuteChooseCuentaRetencionImpuestoMunicipalCommand);
            ChooseCxPTipoComprobanteCommand = new RelayCommand<string>(ExecuteChooseCxPTipoComprobanteCommand);
            ChooseCuentaCobranzaCobradoEnEfectivoCommand = new RelayCommand<string>(ExecuteChooseCuentaCobranzaCobradoEnEfectivoCommand);
            ChooseCuentaCobranzaCobradoEnChequeCommand = new RelayCommand<string>(ExecuteChooseCuentaCobranzaCobradoEnChequeCommand);
            ChooseCuentaCobranzaCobradoEnTarjetaCommand = new RelayCommand<string>(ExecuteChooseCuentaCobranzaCobradoEnTarjetaCommand);
            ChooseCuentaCobranzaRetencionISLRCommand = new RelayCommand<string>(ExecuteChooseCuentaCobranzaRetencionISLRCommand);
            ChooseCuentaCobranzaRetencionIVACommand = new RelayCommand<string>(ExecuteChooseCuentaCobranzaRetencionIVACommand);
            ChooseCuentaCobranzaOtrosCommand = new RelayCommand<string>(ExecuteChooseCuentaCobranzaOtrosCommand);
            ChooseCuentaCobranzaCxCClientesCommand = new RelayCommand<string>(ExecuteChooseCuentaCobranzaCxCClientesCommand);
            ChooseCuentaCobranzaCobradoAnticipoCommand = new RelayCommand<string>(ExecuteChooseCuentaCobranzaCobradoAnticipoCommand);
            ChooseCuentaCobranzaIvaDiferidoCommand = new RelayCommand<string>(ExecuteChooseCuentaCobranzaIvaDiferidoCommand);
            ChooseCobranzaTipoComprobanteCommand = new RelayCommand<string>(ExecuteChooseCobranzaTipoComprobanteCommand);
            ChooseCuentaPagosCxPProveedoresCommand = new RelayCommand<string>(ExecuteChooseCuentaPagosCxPProveedoresCommand);
            ChooseCuentaPagosRetencionISLRCommand = new RelayCommand<string>(ExecuteChooseCuentaPagosRetencionISLRCommand);
            ChooseCuentaPagosOtrosCommand = new RelayCommand<string>(ExecuteChooseCuentaPagosOtrosCommand);
            ChooseCuentaPagosBancoCommand = new RelayCommand<string>(ExecuteChooseCuentaPagosBancoCommand);
            ChooseCuentaPagosPagadoAnticipoCommand = new RelayCommand<string>(ExecuteChooseCuentaPagosPagadoAnticipoCommand);
            ChoosePagoTipoComprobanteCommand = new RelayCommand<string>(ExecuteChoosePagoTipoComprobanteCommand);
            ChooseCuentaFacturacionCxCClientesCommand = new RelayCommand<string>(ExecuteChooseCuentaFacturacionCxCClientesCommand);
            ChooseCuentaFacturacionMontoTotalFacturaCommand = new RelayCommand<string>(ExecuteChooseCuentaFacturacionMontoTotalFacturaCommand);
            ChooseCuentaFacturacionCargosCommand = new RelayCommand<string>(ExecuteChooseCuentaFacturacionCargosCommand);
            ChooseCuentaFacturacionDescuentosCommand = new RelayCommand<string>(ExecuteChooseCuentaFacturacionDescuentosCommand);
            ChooseCuentaFacturacionIvaDiferidoCommand = new RelayCommand<string>(ExecuteChooseCuentaFacturacionIvaDiferidoCommand);
            ChooseFacturaTipoComprobanteCommand = new RelayCommand<string>(ExecuteChooseFacturaTipoComprobanteCommand);
            ChooseCuentaRDVtasCajaCommand = new RelayCommand<string>(ExecuteChooseCuentaRDVtasCajaCommand);
            ChooseCuentaRDVtasMontoTotalCommand = new RelayCommand<string>(ExecuteChooseCuentaRDVtasMontoTotalCommand);
            ChooseCuentaMovBancarioGastoCommand = new RelayCommand<string>(ExecuteChooseCuentaMovBancarioGastoCommand);
            ChooseCuentaMovBancarioBancosHaberCommand = new RelayCommand<string>(ExecuteChooseCuentaMovBancarioBancosHaberCommand);
            ChooseCuentaMovBancarioBancosDebeCommand = new RelayCommand<string>(ExecuteChooseCuentaMovBancarioBancosDebeCommand);
            ChooseCuentaMovBancarioIngresosCommand = new RelayCommand<string>(ExecuteChooseCuentaMovBancarioIngresosCommand);
            ChooseMovimientoBancarioTipoComprobanteCommand = new RelayCommand<string>(ExecuteChooseMovimientoBancarioTipoComprobanteCommand);
            ChooseCuentaDebitoBancarioGastoCommand = new RelayCommand<string>(ExecuteChooseCuentaDebitoBancarioGastoCommand);
            ChooseCuentaDebitoBancarioBancosCommand = new RelayCommand<string>(ExecuteChooseCuentaDebitoBancarioBancosCommand);
            ChooseCuentaCreditoBancarioGastoCommand = new RelayCommand<string>(ExecuteChooseCuentaCreditoBancarioGastoCommand);
            ChooseCuentaCreditoBancarioBancosCommand = new RelayCommand<string>(ExecuteChooseCuentaCreditoBancarioBancosCommand);
            ChooseCuentaAnticipoCajaCommand = new RelayCommand<string>(ExecuteChooseCuentaAnticipoCajaCommand);
            ChooseCuentaAnticipoCobradoCommand = new RelayCommand<string>(ExecuteChooseCuentaAnticipoCobradoCommand);
            ChooseCuentaAnticipoOtrosIngresosCommand = new RelayCommand<string>(ExecuteChooseCuentaAnticipoOtrosIngresosCommand);
            ChooseCuentaAnticipoPagadoCommand = new RelayCommand<string>(ExecuteChooseCuentaAnticipoPagadoCommand);
            ChooseCuentaAnticipoBancoCommand = new RelayCommand<string>(ExecuteChooseCuentaAnticipoBancoCommand);
            ChooseCuentaAnticipoOtrosEgresosCommand = new RelayCommand<string>(ExecuteChooseCuentaAnticipoOtrosEgresosCommand);
            ChooseAnticipoTipoComprobanteCommand = new RelayCommand<string>(ExecuteChooseAnticipoTipoComprobanteCommand);
            ChooseCuentaCostoDeVentaCommand = new RelayCommand<string>(ExecuteChooseCuentaCostoDeVentaCommand);
            ChooseCuentaInventarioCommand = new RelayCommand<string>(ExecuteChooseCuentaInventarioCommand);
            ChooseInventarioTipoComprobanteCommand = new RelayCommand<string>(ExecuteChooseInventarioTipoComprobanteCommand);
            ChooseCtaDePagosSueldosCommand = new RelayCommand<string>(ExecuteChooseCtaDePagosSueldosCommand);
            ChooseCtaDePagosSueldosBancoCommand = new RelayCommand<string>(ExecuteChooseCtaDePagosSueldosBancoCommand);
            ChoosePagosSueldosTipoComprobanteCommand = new RelayCommand<string>(ExecuteChoosePagosSueldosTipoComprobanteCommand);
            ChooseCuentaCajaChicaGastoCommand = new RelayCommand<string>(ExecuteChooseCuentaCajaChicaGastoCommand);
            ChooseCuentaCajaChicaBancoHaberCommand = new RelayCommand<string>(ExecuteChooseCuentaCajaChicaBancoHaberCommand);
            ChooseCuentaCajaChicaBancoDebeCommand = new RelayCommand<string>(ExecuteChooseCuentaCajaChicaBancoDebeCommand);
            ChooseCuentaCajaChicaBancoCommand = new RelayCommand<string>(ExecuteChooseCuentaCajaChicaBancoCommand);
            ChooseSiglasTipoComprobanteCajaChicaCommand = new RelayCommand<string>(ExecuteChooseSiglasTipoComprobanteCajaChicaCommand);
            ChooseCuentaRendicionesGastoCommand = new RelayCommand<string>(ExecuteChooseCuentaRendicionesGastoCommand);
            ChooseCuentaRendicionesBancoCommand = new RelayCommand<string>(ExecuteChooseCuentaRendicionesBancoCommand);
            ChooseCuentaRendicionesAnticiposCommand = new RelayCommand<string>(ExecuteChooseCuentaRendicionesAnticiposCommand);
            ChooseSiglasTipoComprobanteRendicionesCommand = new RelayCommand<string>(ExecuteChooseSiglasTipoComprobanteRendicionesCommand);
            ChooseCuentaTransfCtasBancoDestinoCommand = new RelayCommand<string>(ExecuteChooseCuentaTransfCtasBancoDestinoCommand);
            ChooseCuentaTransfCtasGastoComOrigenCommand = new RelayCommand<string>(ExecuteChooseCuentaTransfCtasGastoComOrigenCommand);
            ChooseCuentaTransfCtasGastoComDestinoCommand = new RelayCommand<string>(ExecuteChooseCuentaTransfCtasGastoComDestinoCommand);
            ChooseCuentaTransfCtasBancoOrigenCommand = new RelayCommand<string>(ExecuteChooseCuentaTransfCtasBancoOrigenCommand);
            ChooseTransfCtasSigasTipoComprobanteCommand = new RelayCommand<string>(ExecuteChooseTransfCtasSigasTipoComprobanteCommand);
            ChooseCuentaOrdenDeProduccionProductoTerminadoCommand = new RelayCommand<string>(ExecuteChooseCuentaOrdenDeProduccionProductoTerminadoCommand);
            ChooseCuentaOrdenDeProduccionMateriaPrimaCommand = new RelayCommand<string>(ExecuteChooseCuentaOrdenDeProduccionMateriaPrimaCommand);
        	ChooseOrdenDeProduccionTipoComprobanteCommand = new RelayCommand<string>(ExecuteChooseOrdenDeProduccionTipoComprobanteCommand);
		}

        private void ReloadRelatedConnectionsGeneral() {
            if(!LibString.IsNullOrEmpty(CuentaIva1Credito))
                ConexionCuentaIva1Credito = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaIva1Credito));
            if(!LibString.IsNullOrEmpty(CuentaIva1Debito))
                ConexionCuentaIva1Debito = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaIva1Debito));
            if(!LibString.IsNullOrEmpty(CuentaRetencionIva))
                ConexionCuentaRetencionIva = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaRetencionIva));
            if(!LibString.IsNullOrEmpty(DiferenciaEnCambioyCalculo))
                ConexionDiferenciaEnCambioyCalculo = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(DiferenciaEnCambioyCalculo));
            if(!LibString.IsNullOrEmpty(CuentaDiferenciaCambiaria)) {
                ConexionCuentaDiferenciaCambiaria = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",LibSearchCriteria.CreateCriteria("Codigo",CuentaDiferenciaCambiaria));
            }
        }

        private void ReloadRelatedConnectionsCxC() {
            if(!LibString.IsNullOrEmpty(CuentaCxCClientes))
                ConexionCuentaCxCClientes = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCxCClientes));
            if(!LibString.IsNullOrEmpty(CuentaCxCIngresos))
                ConexionCuentaCxCIngresos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCxCIngresos));

            if(!LibString.IsNullOrEmpty(CxCTipoComprobante))
                ConexionCxCTipoComprobante = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",LibSearchCriteria.CreateCriteria("Codigo",CxCTipoComprobante));
        }

        private void ReloadRelatedConnectionsCxP() {
            if(!LibString.IsNullOrEmpty(CuentaCxPGasto))
                ConexionCuentaCxPGasto = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCxPGasto));
            if(!LibString.IsNullOrEmpty(CuentaCxPProveedores))
                ConexionCuentaCxPProveedores = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCxPProveedores));
            if(!LibString.IsNullOrEmpty(CuentaRetencionImpuestoMunicipal))
                ConexionCuentaRetencionImpuestoMunicipal = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaRetencionImpuestoMunicipal));

            if(!LibString.IsNullOrEmpty(CxPTipoComprobante))
                ConexionCxPTipoComprobante = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",LibSearchCriteria.CreateCriteria("Codigo",CxPTipoComprobante));
        }

        private void ReloadRelatedConnectionsCobranza() {
            if(!LibString.IsNullOrEmpty(CuentaCobranzaCobradoEnEfectivo))
                ConexionCuentaCobranzaCobradoEnEfectivo = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCobranzaCobradoEnEfectivo));
            if(!LibString.IsNullOrEmpty(CuentaCobranzaCobradoEnCheque))
                ConexionCuentaCobranzaCobradoEnCheque = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCobranzaCobradoEnCheque));
            if(!LibString.IsNullOrEmpty(CuentaCobranzaCobradoEnTarjeta))
                ConexionCuentaCobranzaCobradoEnTarjeta = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCobranzaCobradoEnTarjeta));
            if(!LibString.IsNullOrEmpty(CuentaCobranzaRetencionISLR))
                ConexionCuentaCobranzaRetencionISLR = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCobranzaRetencionISLR));
            if(!LibString.IsNullOrEmpty(CuentaCobranzaRetencionIVA))
                ConexionCuentaCobranzaRetencionIVA = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCobranzaRetencionIVA));
            if(!LibString.IsNullOrEmpty(CuentaCobranzaOtros))
                ConexionCuentaCobranzaOtros = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCobranzaOtros));
            if(!LibString.IsNullOrEmpty(CuentaCobranzaCxCClientes))
                ConexionCuentaCobranzaCxCClientes = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCobranzaCxCClientes));
            if(!LibString.IsNullOrEmpty(CuentaCobranzaCobradoAnticipo))
                ConexionCuentaCobranzaCobradoAnticipo = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCobranzaCobradoAnticipo));
            if(!LibString.IsNullOrEmpty(CuentaCobranzaIvaDiferido))
                ConexionCuentaCobranzaIvaDiferido = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCobranzaIvaDiferido));

            if(!LibString.IsNullOrEmpty(CobranzaTipoComprobante))
                ConexionCobranzaTipoComprobante = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",LibSearchCriteria.CreateCriteria("Codigo",CobranzaTipoComprobante));
        }

        private void ReloadRelatedConnectionsPago() {
            if(!LibString.IsNullOrEmpty(CuentaPagosCxPProveedores))
                ConexionCuentaPagosCxPProveedores = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaPagosCxPProveedores));
            if(!LibString.IsNullOrEmpty(CuentaPagosRetencionISLR))
                ConexionCuentaPagosRetencionISLR = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaPagosRetencionISLR));
            if(!LibString.IsNullOrEmpty(CuentaPagosOtros))
                ConexionCuentaPagosOtros = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaPagosOtros));
            if(!LibString.IsNullOrEmpty(CuentaPagosBanco))
                ConexionCuentaPagosBanco = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaPagosBanco));
            if(!LibString.IsNullOrEmpty(CuentaPagosPagadoAnticipo))
                ConexionCuentaPagosPagadoAnticipo = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaPagosPagadoAnticipo));

            if(!LibString.IsNullOrEmpty(PagoTipoComprobante))
                ConexionPagoTipoComprobante = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",LibSearchCriteria.CreateCriteria("Codigo",PagoTipoComprobante));
        }

        private void ReloadRelatedConnectionsFacturacion() {
            if(!LibString.IsNullOrEmpty(CuentaFacturacionCxCClientes))
                ConexionCuentaFacturacionCxCClientes = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaFacturacionCxCClientes));
            if(!LibString.IsNullOrEmpty(CuentaFacturacionMontoTotalFactura))
                ConexionCuentaFacturacionMontoTotalFactura = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaFacturacionMontoTotalFactura));
            if(!LibString.IsNullOrEmpty(CuentaFacturacionCargos))
                ConexionCuentaFacturacionCargos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaFacturacionCargos));
            if(!LibString.IsNullOrEmpty(CuentaFacturacionDescuentos))
                ConexionCuentaFacturacionDescuentos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaFacturacionDescuentos));
            if(!LibString.IsNullOrEmpty(CuentaFacturacionIvaDiferido))
                ConexionCuentaFacturacionIvaDiferido = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaFacturacionIvaDiferido));

            if(!LibString.IsNullOrEmpty(FacturaTipoComprobante))
                ConexionFacturaTipoComprobante = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",LibSearchCriteria.CreateCriteria("Codigo",FacturaTipoComprobante));
        }

        private void ReloadRelatedConnectionsRDVtas() {
            if(!LibString.IsNullOrEmpty(CuentaRDVtasCaja))
                ConexionCuentaRDVtasCaja = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaRDVtasCaja));
            if(!LibString.IsNullOrEmpty(CuentaRDVtasMontoTotal))
                ConexionCuentaRDVtasMontoTotal = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaRDVtasMontoTotal));
        }

        private void ReloadRelatedConnectionsMovBancario() {
            if(!LibString.IsNullOrEmpty(CuentaMovBancarioGasto))
                ConexionCuentaMovBancarioGasto = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaMovBancarioGasto));
            if(!LibString.IsNullOrEmpty(CuentaMovBancarioBancosHaber))
                ConexionCuentaMovBancarioBancosHaber = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaMovBancarioBancosHaber));
            if(!LibString.IsNullOrEmpty(CuentaMovBancarioBancosDebe))
                ConexionCuentaMovBancarioBancosDebe = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaMovBancarioBancosDebe));
            if(!LibString.IsNullOrEmpty(CuentaMovBancarioIngresos))
                ConexionCuentaMovBancarioIngresos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaMovBancarioIngresos));

            if(!LibString.IsNullOrEmpty(MovimientoBancarioTipoComprobante))
                ConexionMovimientoBancarioTipoComprobante = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",LibSearchCriteria.CreateCriteria("Codigo",MovimientoBancarioTipoComprobante));
        }

        private void ReloadRelatedConnectionsImpTrnBnc() {
            if(!LibString.IsNullOrEmpty(CuentaDebitoBancarioGasto))
                ConexionCuentaDebitoBancarioGasto = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaDebitoBancarioGasto));
            if(!LibString.IsNullOrEmpty(CuentaDebitoBancarioBancos))
                ConexionCuentaDebitoBancarioBancos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaDebitoBancarioBancos));
            if(!LibString.IsNullOrEmpty(CuentaCreditoBancarioGasto))
                ConexionCuentaCreditoBancarioGasto = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCreditoBancarioGasto));
            if(!LibString.IsNullOrEmpty(CuentaCreditoBancarioBancos))
                ConexionCuentaCreditoBancarioBancos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCreditoBancarioBancos));
        }

        private void ReloadRelatedConnectionsAnticipo() {
            if(!LibString.IsNullOrEmpty(CuentaAnticipoCaja))
                ConexionCuentaAnticipoCaja = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaAnticipoCaja));
            if(!LibString.IsNullOrEmpty(CuentaAnticipoCobrado))
                ConexionCuentaAnticipoCobrado = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaAnticipoCobrado));
            if(!LibString.IsNullOrEmpty(CuentaAnticipoOtrosIngresos))
                ConexionCuentaAnticipoOtrosIngresos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaAnticipoOtrosIngresos));
            if(!LibString.IsNullOrEmpty(CuentaAnticipoPagado))
                ConexionCuentaAnticipoPagado = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaAnticipoPagado));
            if(!LibString.IsNullOrEmpty(CuentaAnticipoBanco))
                ConexionCuentaAnticipoBanco = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaAnticipoBanco));
            if(!LibString.IsNullOrEmpty(CuentaAnticipoOtrosEgresos))
                ConexionCuentaAnticipoOtrosEgresos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaAnticipoOtrosEgresos));

            if(!LibString.IsNullOrEmpty(AnticipoTipoComprobante))
                ConexionAnticipoTipoComprobante = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",LibSearchCriteria.CreateCriteria("Codigo",AnticipoTipoComprobante));
        }

        private void ReloadRelatedConnectionsInventario() {
            if(!LibString.IsNullOrEmpty(CuentaCostoDeVenta))
                ConexionCuentaCostoDeVenta = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCostoDeVenta));
            if(!LibString.IsNullOrEmpty(CuentaInventario))
                ConexionCuentaInventario = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaInventario));

            if(!LibString.IsNullOrEmpty(InventarioTipoComprobante))
                ConexionInventarioTipoComprobante = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",LibSearchCriteria.CreateCriteria("Codigo",InventarioTipoComprobante));
        }

        private void ReloadRelatedConnectionsSolicitudPago() {
            if(!LibString.IsNullOrEmpty(CtaDePagosSueldos))
                ConexionCtaDePagosSueldos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CtaDePagosSueldos));
            if(!LibString.IsNullOrEmpty(CtaDePagosSueldosBanco))
                ConexionCtaDePagosSueldosBanco = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CtaDePagosSueldosBanco));

            if(!LibString.IsNullOrEmpty(PagosSueldosTipoComprobante))
                ConexionPagosSueldosTipoComprobante = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",LibSearchCriteria.CreateCriteria("Codigo",PagosSueldosTipoComprobante));
        }

        private void ReloadRelatedConnectionsCajaChica() {
            if(!LibString.IsNullOrEmpty(CuentaCajaChicaGasto))
                ConexionCuentaCajaChicaGasto = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCajaChicaGasto));
            if(!LibString.IsNullOrEmpty(CuentaCajaChicaBancoHaber))
                ConexionCuentaCajaChicaBancoHaber = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCajaChicaBancoHaber));
            if(!LibString.IsNullOrEmpty(CuentaCajaChicaBancoDebe))
                ConexionCuentaCajaChicaBancoDebe = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCajaChicaBancoDebe));
            if(!LibString.IsNullOrEmpty(CuentaCajaChicaBanco))
                ConexionCuentaCajaChicaBanco = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaCajaChicaBanco));

            if(!LibString.IsNullOrEmpty(SiglasTipoComprobanteCajaChica))
                ConexionSiglasTipoComprobanteCajaChica = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",LibSearchCriteria.CreateCriteria("Codigo",SiglasTipoComprobanteCajaChica));
        }

        private void ReloadRelatedConnectionsRendiciones() {
            if(!LibString.IsNullOrEmpty(CuentaRendicionesGasto))
                ConexionCuentaRendicionesGasto = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaRendicionesGasto));
            if(!LibString.IsNullOrEmpty(CuentaRendicionesBanco))
                ConexionCuentaRendicionesBanco = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaRendicionesBanco));
            if(!LibString.IsNullOrEmpty(CuentaRendicionesAnticipos))
                ConexionCuentaRendicionesAnticipos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta",SearchCriteriaConexionCuenta(CuentaRendicionesAnticipos));

            if(!LibString.IsNullOrEmpty(SiglasTipoComprobanteRendiciones))
                ConexionSiglasTipoComprobanteRendiciones = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",LibSearchCriteria.CreateCriteria("Codigo",SiglasTipoComprobanteRendiciones));
        }
		private void ReloadRelatedConnectionsTransfCtas() {
            if(!LibString.IsNullOrEmpty(CuentaTransfCtasBancoDestino))
                ConexionCuentaTransfCtasBancoDestino = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", SearchCriteriaConexionCuenta(CuentaTransfCtasBancoDestino));
            if(!LibString.IsNullOrEmpty(CuentaTransfCtasGastoComOrigen))
                ConexionCuentaTransfCtasGastoComOrigen = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", SearchCriteriaConexionCuenta(CuentaTransfCtasGastoComOrigen));
            if(!LibString.IsNullOrEmpty(CuentaTransfCtasGastoComDestino))
                ConexionCuentaTransfCtasGastoComDestino = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", SearchCriteriaConexionCuenta(CuentaTransfCtasGastoComDestino));
            if(!LibString.IsNullOrEmpty(CuentaTransfCtasBancoOrigen))
			    ConexionCuentaTransfCtasBancoOrigen = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", SearchCriteriaConexionCuenta(CuentaTransfCtasBancoOrigen));
            if(!LibString.IsNullOrEmpty(TransfCtasSigasTipoComprobante))
			    ConexionTransfCtasSigasTipoComprobante = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante", LibSearchCriteria.CreateCriteria("Codigo", TransfCtasSigasTipoComprobante));
        }
		private void ReloadRelatedConnectionsProduccion() {
            if(!LibString.IsNullOrEmpty(CuentaOrdenDeProduccionProductoTerminado))
				ConexionCuentaOrdenDeProduccionProductoTerminado = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", LibSearchCriteria.CreateCriteria("Codigo", CuentaOrdenDeProduccionProductoTerminado));
            if(!LibString.IsNullOrEmpty(CuentaOrdenDeProduccionMateriaPrima))
                ConexionCuentaOrdenDeProduccionMateriaPrima = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", LibSearchCriteria.CreateCriteria("Codigo", CuentaOrdenDeProduccionMateriaPrima));
            if(!LibString.IsNullOrEmpty(OrdenDeProduccionTipoComprobante))
                ConexionOrdenDeProduccionTipoComprobante = FirstConnectionRecordOrDefault<FkTipoDeComprobanteViewModel>("Tipo de Comprobante", LibSearchCriteria.CreateCriteria("Codigo", OrdenDeProduccionTipoComprobante));				
        }
        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ReloadRelatedConnectionsGeneral();
            ReloadRelatedConnectionsCxC();
            ReloadRelatedConnectionsCxP();
            ReloadRelatedConnectionsCobranza();
            ReloadRelatedConnectionsPago();
            ReloadRelatedConnectionsFacturacion();
            ReloadRelatedConnectionsRDVtas();
            ReloadRelatedConnectionsMovBancario();
            ReloadRelatedConnectionsImpTrnBnc();
            ReloadRelatedConnectionsAnticipo();
            ReloadRelatedConnectionsInventario();
            ReloadRelatedConnectionsSolicitudPago();
            ReloadRelatedConnectionsCajaChica();
            ReloadRelatedConnectionsRendiciones();
			ReloadRelatedConnectionsTransfCtas();
        }

        private void ExecuteChooseCuentaIva1CreditoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                ConexionCuentaIva1Credito = null;
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaIva1Credito = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaIva1DebitoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                ConexionCuentaIva1Debito = null;
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaIva1Debito = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaRetencionIvaCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                ConexionCuentaRetencionIva = null;
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaRetencionIva = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseDiferenciaEnCambioyCalculoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                ConexionDiferenciaEnCambioyCalculo = null;
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionDiferenciaEnCambioyCalculo = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }
		
        private void ExecuteChooseCuentaDiferenciaCambiariaCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaDiferenciaCambiaria = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);               
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        private void ExecuteChooseCuentaCxCClientesCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                ConexionCuentaCxCClientes = null;
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCxCClientes = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCxCIngresosCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                ConexionCuentaCxCIngresos = null;
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCxCIngresos = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCxCTipoComprobanteCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                ConexionCxCTipoComprobante = null;
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
            */
                #endregion //Codigo Ejemplo
                ConexionCxCTipoComprobante = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCxPGastoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                ConexionCuentaCxPGasto = null;
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCxPGasto = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCxPProveedoresCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCxPProveedores = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaRetencionImpuestoMunicipalCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaRetencionImpuestoMunicipal = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCxPTipoComprobanteCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
            */
                #endregion //Codigo Ejemplo
                ConexionCxPTipoComprobante = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCobranzaCobradoEnEfectivoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCobranzaCobradoEnEfectivo = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCobranzaCobradoEnChequeCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCobranzaCobradoEnCheque = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCobranzaCobradoEnTarjetaCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCobranzaCobradoEnTarjeta = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCobranzaRetencionISLRCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCobranzaRetencionISLR = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCobranzaRetencionIVACommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCobranzaRetencionIVA = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCobranzaOtrosCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCobranzaOtros = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCobranzaCxCClientesCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCobranzaCxCClientes = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCobranzaCobradoAnticipoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCobranzaCobradoAnticipo = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCobranzaIvaDiferidoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCobranzaIvaDiferido = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCobranzaTipoComprobanteCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
            */
                #endregion //Codigo Ejemplo
                ConexionCobranzaTipoComprobante = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaPagosCxPProveedoresCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaPagosCxPProveedores = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaPagosRetencionISLRCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaPagosRetencionISLR = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaPagosOtrosCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaPagosOtros = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaPagosBancoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaPagosBanco = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaPagosPagadoAnticipoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaPagosPagadoAnticipo = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChoosePagoTipoComprobanteCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
            */
                #endregion //Codigo Ejemplo
                ConexionPagoTipoComprobante = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaFacturacionCxCClientesCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaFacturacionCxCClientes = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaFacturacionMontoTotalFacturaCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaFacturacionMontoTotalFactura = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaFacturacionCargosCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaFacturacionCargos = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaFacturacionDescuentosCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaFacturacionDescuentos = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaFacturacionIvaDiferidoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaFacturacionIvaDiferido = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseFacturaTipoComprobanteCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
            */
                #endregion //Codigo Ejemplo
                ConexionFacturaTipoComprobante = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaRDVtasCajaCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaRDVtasCaja = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaRDVtasMontoTotalCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaRDVtasMontoTotal = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaMovBancarioGastoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaMovBancarioGasto = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaMovBancarioBancosHaberCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaMovBancarioBancosHaber = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaMovBancarioBancosDebeCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaMovBancarioBancosDebe = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaMovBancarioIngresosCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaMovBancarioIngresos = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseMovimientoBancarioTipoComprobanteCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
            */
                #endregion //Codigo Ejemplo
                ConexionMovimientoBancarioTipoComprobante = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaDebitoBancarioGastoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaDebitoBancarioGasto = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaDebitoBancarioBancosCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaDebitoBancarioBancos = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCreditoBancarioGastoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCreditoBancarioGasto = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCreditoBancarioBancosCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCreditoBancarioBancos = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaAnticipoCajaCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaAnticipoCaja = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaAnticipoCobradoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaAnticipoCobrado = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaAnticipoOtrosIngresosCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaAnticipoOtrosIngresos = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaAnticipoPagadoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaAnticipoPagado = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaAnticipoBancoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaAnticipoBanco = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaAnticipoOtrosEgresosCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaAnticipoOtrosEgresos = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseAnticipoTipoComprobanteCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
            */
                #endregion //Codigo Ejemplo
                ConexionAnticipoTipoComprobante = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCostoDeVentaCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCostoDeVenta = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaInventarioCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaInventario = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseInventarioTipoComprobanteCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
            */
                #endregion //Codigo Ejemplo
                ConexionInventarioTipoComprobante = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCtaDePagosSueldosCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCtaDePagosSueldos = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCtaDePagosSueldosBancoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCtaDePagosSueldosBanco = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChoosePagosSueldosTipoComprobanteCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
            */
                #endregion //Codigo Ejemplo
                ConexionPagosSueldosTipoComprobante = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCajaChicaGastoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCajaChicaGasto = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCajaChicaBancoHaberCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCajaChicaBancoHaber = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCajaChicaBancoDebeCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCajaChicaBancoDebe = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaCajaChicaBancoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaCajaChicaBanco = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseSiglasTipoComprobanteCajaChicaCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
            */
                #endregion //Codigo Ejemplo
                ConexionSiglasTipoComprobanteCajaChica = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaRendicionesGastoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaRendicionesGasto = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaRendicionesBancoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaRendicionesBanco = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCuentaRendicionesAnticiposCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaRendicionesAnticipos = ChooseRecord<FkCuentaViewModel>("Cuenta",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseSiglasTipoComprobanteRendicionesCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                    vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
            */
                #endregion //Codigo Ejemplo
                ConexionSiglasTipoComprobanteRendiciones = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseTransfCtasSigasTipoComprobanteCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                ConexionTransfCtasSigasTipoComprobante = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        private void ExecuteChooseCuentaTransfCtasBancoOrigenCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaTransfCtasBancoOrigen = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        private void ExecuteChooseCuentaTransfCtasGastoComDestinoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaTransfCtasGastoComDestino = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        private void ExecuteChooseCuentaTransfCtasGastoComOrigenCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaTransfCtasGastoComOrigen = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        private void ExecuteChooseCuentaTransfCtasBancoDestinoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", AjustaCodigoCuenta(valCodigo));
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaTransfCtasBancoDestino = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        private void ExecuteChooseOrdenDeProduccionTipoComprobanteCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                ConexionOrdenDeProduccionTipoComprobante = ChooseRecord<FkTipoDeComprobanteViewModel>("Tipo de Comprobante", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        private void ExecuteChooseCuentaOrdenDeProduccionMateriaPrimaCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaOrdenDeProduccionMateriaPrima = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        private void ExecuteChooseCuentaOrdenDeProduccionProductoTerminadoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = SearchCriteriaConexionCuenta();
                ConexionCuentaOrdenDeProduccionProductoTerminado = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos Generados

        public string CuentaInventarioDescripcion { get; set; }
        public string CuentaIva1CreditoDescripcion { get; set; }
        public string CuentaIva1DebitoDescripcion { get; set; }
        public string CuentaRetencionIvaDescripcion { get; set; }
        public string DiferenciaEnCambioyCalculoDescripcion { get; set; }       
        public string CuentaCxCClientesDescripcion { get; set; }
        public string CuentaCxCIngresosDescripcion { get; set; }
        public string CuentaCxPGastoDescripcion { get; set; }
        public string CuentaCxPProveedoresDescripcion { get; set; }
        public string CuentaRetencionImpuestoMunicipalDescripcion { get; set; }
        public string CuentaCostoDeVentaDescripcion { get; set; }
        public string CtaDePagosSueldosDescripcion { get; set; }
        public string CtaDePagosSueldosBancoDescripcion { get; set; }
        public string CuentaRDVtasCajaDescripcion { get; set; }
        public string CuentaRDVtasMontoTotalDescripcion { get; set; }
        public string CuentaDebitoBancarioGastoDescripcion { get; set; }
        public string CuentaDebitoBancarioBancosDescripcion { get; set; }
        public string CuentaCreditoBancarioGastoDescripcion { get; set; }
        public string CuentaCreditoBancarioBancosDescripcion { get; set; }
        public string CuentaCajaChicaGastoDescripcion { get; set; }
        public string CuentaCajaChicaBancoHaberDescripcion { get; set; }
        public string CuentaCajaChicaBancoDebeDescripcion { get; set; }
        public string CuentaCajaChicaBancoDescripcion { get; set; }
        public string CuentaMovBancarioBancosDebeDescripcion { get; set; }
        public string CuentaMovBancarioIngresosDescripcion { get; set; }
        public string CuentaMovBancarioBancosHaberDescripcion { get; set; }
        public string CuentaMovBancarioGastoDescripcion { get; set; }
        public string CuentaAnticipoCajaDescripcion { get; set; }
        public string CuentaAnticipoCobradoDescripcion { get; set; }
        public string CuentaAnticipoOtrosIngresosDescripcion { get; set; }
        public string CuentaAnticipoPagadoDescripcion { get; set; }
        public string CuentaAnticipoOtrosEgresosDescripcion { get; set; }
        public string CuentaAnticipoBancoDescripcion { get; set; }
        public string CuentaFacturacionCxCClientesDescripcion { get; set; }
        public string CuentaFacturacionDescuentosDescripcion { get; set; }
        public string CuentaFacturacionIvaDiferidoDescripcion { get; set; }
        public string CuentaFacturacionCargosDescripcion { get; set; }
        public string CuentaFacturacionMontoTotalFacturaDescripcion { get; set; }
        public string CuentaPagosCxPProveedoresDescripcion { get; set; }
        public string CuentaPagosBancoDescripcion { get; set; }
        public string CuentaPagosOtrosDescripcion { get; set; }
        public string CuentaPagosRetencionISLRDescripcion { get; set; }
        public string CuentaPagosPagadoAnticipoDescripcion { get; set; }
        public string CuentaCobranzaCobradoEnEfectivoDescripcion { get; set; }
        public string CuentaCobranzaCobradoEnChequeDescripcion { get; set; }
        public string CuentaCobranzaCobradoEnTarjetaDescripcion { get; set; }
        public string CuentaCobranzaOtrosDescripcion { get; set; }
        public string CuentaCobranzaRetencionISLRDescripcion { get; set; }
        public string CuentaCobranzaRetencionIVADescripcion { get; set; }
        public string CuentaCobranzaCobradoAnticipoDescripcion { get; set; }
        public string CuentaCobranzaIvaDiferidoDescripcion { get; set; }
        public string CuentaCobranzaCxCClientesDescripcion { get; set; }
        public string CuentaCxPProveedoresCajaChicaDescripcion { get; set; }
        public string CuentaDiferenciaCambiariaDescripcion { get; set; }
        public string CuentaTransfCtasBancoDestinoDescripcion { get; set; }
        public string CuentaTransfCtasBancoOrigenDescripcion { get; set; }
        public string CuentaTransfCtasGastoComDestinoDescripcion { get; set; }
        public string CuentaTransfCtasGastoComOrigenDescripcion { get; set; }
        public string CuentaOrdenDeProduccionProductoTerminadoDescripcion { get; set; }
        public string CuentaOrdenDeProduccionMateriaPrimaDescripcion { get; set; }

        public const string CuentaIva1DebitoDescripcionPropertyName = "CuentaIva1DebitoDescripcion";
        public const string CuentaIva1CreditoDescripcionPropertyName = "CuentaIva1CreditoDescripcion";
        public const string CuentaInventarioDescripcionPropertyName = "CuentaInventarioDescripcion";
        public const string CuentaRetencionIvaDescripcionPropertyName = "CuentaRetencionIvaDescripcion";
        public const string DiferenciaEnCambioyCalculoDescripcionPropertyName = "DiferenciaEnCambioyCalculoDescripcion";
        public const string CuentaCxCClientesDescripcionPropertyName = "CuentaCxCClientesDescripcion";
        public const string CuentaCxCIngresosDescripcionPropertyName = "CuentaCxCIngresosDescripcion";
        public const string CuentaCxPGastoDescripcionPropertyName = "CuentaCxPGastoDescripcion";
        public const string CuentaCxPProveedoresDescripcionPropertyName = "CuentaCxPProveedoresDescripcion";
        public const string CuentaRetencionImpuestoMunicipalDescripcionPropertyName = "CuentaRetencionImpuestoMunicipalDescripcion";
        public const string CuentaCostoDeVentaDescripcionPropertyName = "CuentaCostoDeVentaDescripcion";
        public const string CtaDePagosSueldosDescripcionPropertyName = "CtaDePagosSueldosDescripcion";
        public const string CtaDePagosSueldosBancoDescripcionPropertyName = "CtaDePagosSueldosBancoDescripcion";
        public const string CuentaRDVtasCajaDescripcionPropertyName = "CuentaRDVtasCajaDescripcion";
        public const string CuentaRDVtasMontoTotalDescripcionPropertyName = "CuentaRDVtasMontoTotalDescripcion";
        public const string CuentaDebitoBancarioGastoDescripcionPropertyName = "CuentaDebitoBancarioGastoDescripcion";
        public const string CuentaDebitoBancarioBancosDescripcionPropertyName = "CuentaDebitoBancarioBancosDescripcion";
        public const string CuentaCreditoBancarioGastoDescripcionPropertyName = "CuentaCreditoBancarioGastoDescripcion";
        public const string CuentaCreditoBancarioBancosDescripcionPropertyName = "CuentaCreditoBancarioBancosDescripcion";
        public const string CuentaCajaChicaGastoDescripcionPropertyName = "CuentaCajaChicaGastoDescripcion";
        public const string CuentaCajaChicaBancoHaberDescripcionPropertyName = "CuentaCajaChicaBancoHaberDescripcion";
        public const string CuentaCajaChicaBancoDebeDescripcionPropertyName = "CuentaCajaChicaBancoDebeDescripcion";
        public const string CuentaCajaChicaBancoDescripcionPropertyName = "CuentaCajaChicaBancoDescripcion";
        public const string CuentaMovBancarioBancosDebeDescripcionPropertyName = "CuentaMovBancarioBancosDebeDescripcion";
        public const string CuentaMovBancarioIngresosDescripcionPropertyName = "CuentaMovBancarioIngresosDescripcion";
        public const string CuentaMovBancarioBancosHaberDescripcionPropertyName = "CuentaMovBancarioBancosHaberDescripcion";
        public const string CuentaMovBancarioGastoDescripcionPropertyName = "CuentaMovBancarioGastoDescripcion";
        public const string CuentaAnticipoCajaDescripcionPropertyName = "CuentaAnticipoCajaDescripcion";
        public const string CuentaAnticipoCobradoDescripcionPropertyName = "CuentaAnticipoCobradoDescripcion";
        public const string CuentaAnticipoOtrosIngresosDescripcionPropertyName = "CuentaAnticipoOtrosIngresosDescripcion";
        public const string CuentaAnticipoPagadoDescripcionPropertyName = "CuentaAnticipoPagadoDescripcion";
        public const string CuentaAnticipoOtrosEgresosDescripcionPropertyName = "CuentaAnticipoOtrosEgresosDescripcion";
        public const string CuentaAnticipoBancoDescripcionPropertyName = "CuentaAnticipoBancoDescripcion";
        public const string CuentaFacturacionCxCClientesDescripcionPropertyName = "CuentaFacturacionCxCClientesDescripcion";
        public const string CuentaFacturacionDescuentosDescripcionPropertyName = "CuentaFacturacionDescuentosDescripcion";
        public const string CuentaFacturacionIvaDiferidoDescripcionPropertyName = "CuentaFacturacionIvaDiferidoDescripcion";
        public const string CuentaFacturacionCargosDescripcionPropertyName = "CuentaFacturacionCargosDescripcion";
        public const string CuentaFacturacionMontoTotalFacturaDescripcionPropertyName = "CuentaFacturacionMontoTotalFacturaDescripcion";
        public const string CuentaPagosCxPProveedoresDescripcionPropertyName = "CuentaPagosCxPProveedoresDescripcion";
        public const string CuentaPagosBancoDescripcionPropertyName = "CuentaPagosBancoDescripcion";
        public const string CuentaPagosOtrosDescripcionPropertyName = "CuentaPagosOtrosDescripcion";
        public const string CuentaPagosRetencionISLRDescripcionPropertyName = "CuentaPagosRetencionISLRDescripcion";
        public const string CuentaPagosPagadoAnticipoDescripcionPropertyName = "CuentaPagosPagadoAnticipoDescripcion";
        public const string CuentaCobranzaCobradoEnEfectivoDescripcionPropertyName = "CuentaCobranzaCobradoEnEfectivoDescripcion";
        public const string CuentaCobranzaCobradoEnChequeDescripcionPropertyName = "CuentaCobranzaCobradoEnChequeDescripcion";
        public const string CuentaCobranzaCobradoEnTarjetaDescripcionPropertyName = "CuentaCobranzaCobradoEnTarjetaDescripcion";
        public const string CuentaCobranzaOtrosDescripcionPropertyName = "CuentaCobranzaOtrosDescripcion";
        public const string CuentaCobranzaRetencionISLRDescripcionPropertyName = "CuentaCobranzaRetencionISLRDescripcion";
        public const string CuentaCobranzaRetencionIVADescripcionPropertyName = "CuentaCobranzaRetencionIVADescripcion";
        public const string CuentaCobranzaCobradoAnticipoDescripcionPropertyName = "CuentaCobranzaCobradoAnticipoDescripcion";
        public const string CuentaCobranzaIvaDiferidoDescripcionPropertyName = "CuentaCobranzaIvaDiferidoDescripcion";
        public const string CuentaCobranzaCxCClientesDescripcionPropertyName = "CuentaCobranzaCxCClientesDescripcion";
        public const string CuentaCxPProveedoresCajaChicaDescripcionPropertyName = "CuentaCxPProveedoresCajaChicaDescripcion";
        public const string CuentaTransfCtasBancoDestinoDescripcionPropertyName = "CuentaTransfCtasBancoDestinoDescripcion";
        public const string CuentaTransfCtasBancoOrigenDescripcionPropertyName = "CuentaTransfCtasBancoOrigenDescripcion";
        public const string CuentaTransfCtasGastoComDestinoDescripcionPropertyName = "CuentaTransfCtasGastoComDestinoDescripcion";
        public const string CuentaTransfCtasGastoComOrigenDescripcionPropertyName = "CuentaTransfCtasGastoComOrigenDescripcion";

        private LibSearchCriteria SearchCriteriaConexionCuenta(string codigo) {
            LibSearchCriteria vSearchcriteria;
            vSearchcriteria = LibSearchCriteria.CreateCriteria("Codigo",codigo);
            vSearchcriteria.Add(SearchCriteriaConexionCuenta(),eLogicOperatorType.And);
            return vSearchcriteria;
        }

        private LibSearchCriteria SearchCriteriaConexionCuenta() {
            LibSearchCriteria vPeriodoCriteria;
            LibSearchCriteria vTituloCriteria;
            LibSearchCriteria vActivoFijoCriteria;
            vPeriodoCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("ReglasDeContabilizacion","ConsecutivoPeriodo"));
            vTituloCriteria = LibSearchCriteria.CreateCriteria("TieneSubCuentas",false);
            vActivoFijoCriteria = LibSearchCriteria.CreateCriteria("EsActivoFijo",false);

            vPeriodoCriteria.Add(vTituloCriteria,eLogicOperatorType.And);
            vPeriodoCriteria.Add(vActivoFijoCriteria,eLogicOperatorType.And);
            vPeriodoCriteria.Add("Codigo",eBooleanOperatorType.IdentityInequality,LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("ReglasDeContabilizacion","GetCierreDelEjercicio"),eLogicOperatorType.And);
            vPeriodoCriteria.Add("TipoCuenta",eBooleanOperatorType.IdentityInequality,eTipoCuenta.OrdenDeudora,eLogicOperatorType.And);
            vPeriodoCriteria.Add("TipoCuenta",eBooleanOperatorType.IdentityInequality,eTipoCuenta.OrdenAcreedora,eLogicOperatorType.And);
            return vPeriodoCriteria;

        }

        private string AjustaCodigoCuenta(string valCodigo) {
            return ((IReglasDeContabilizacionPdn)ReglasDeContabilizacionNav).CorrigeYAjustaLaCuenta(valCodigo);
        }

        private void InicializarVariablesGlobales() {
            ReglasDeContabilizacionNav = new clsReglasDeContabilizacionNav();
        }

        public clsReglasDeContabilizacionNav ReglasDeContabilizacionNav { get; set; }

        public bool TabCajaChicaVisible {
            get {
                return true;
            }
        }

        public bool TabResumenDiarioDeVentasVisible {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("ReglasDeContabilizacion","UsarResumenDiarioDeVentas");
            }
        }

        public bool TabImpuestosALasTransaccionesFinancierasVisible {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("ReglasDeContabilizacion","UsarImpuestoTransaccionesFinancieras");
            }
        }

        public bool LeyendaFacturaVisible {
            get {
                return ContabilizarPorArticulo;
            }
        }

        public bool IsEnabledAgruparPorCuentaDeArticulo {
            get {
                return IsEnabled && ContabilizarPorArticulo;
            }
        }

        public bool IsEnabledDondeContabilizarRetIva {
            get {
                return IsEnabled && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("ReglasDeContabilizacion","EsContribuyenteEspecial") && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("ReglasDeContabilizacion","DondeRetenerIVA") != (int)eDondeSeEfectuaLaRetencionIVA.NoRetenida;
            }
        }

        public bool IsEnabledCuentaRetencionIva {
            get {
                return IsEnabled && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("ReglasDeContabilizacion","EsContribuyenteEspecial");
            }
        }

        private bool CuentasNulasVacias() {
            bool vResult = false;
            vResult = vResult || LibString.IsNullOrEmpty(CuentaInventario);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaIva1Credito);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaIva1Debito);
            if(TipoContribuyenteEspecial) {
                vResult = vResult || LibString.IsNullOrEmpty(CuentaRetencionIva);
            }
            vResult = vResult || LibString.IsNullOrEmpty(DiferenciaEnCambioyCalculo);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCxCClientes);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCxCIngresos);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCxPGasto);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCxPProveedores);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaRetencionImpuestoMunicipal);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCostoDeVenta);
            vResult = vResult || LibString.IsNullOrEmpty(CtaDePagosSueldos);
            vResult = vResult || LibString.IsNullOrEmpty(CtaDePagosSueldosBanco);
            if(TabResumenDiarioDeVentasVisible) {
                vResult = vResult || LibString.IsNullOrEmpty(CuentaRDVtasCaja);
                vResult = vResult || LibString.IsNullOrEmpty(CuentaRDVtasMontoTotal);
            }
            if(TabImpuestosALasTransaccionesFinancierasVisible) {
                vResult = vResult || LibString.IsNullOrEmpty(CuentaDebitoBancarioGasto);
                vResult = vResult || LibString.IsNullOrEmpty(CuentaDebitoBancarioBancos);
                //vResult = vResult || LibString.IsNullOrEmpty(CuentaCreditoBancarioBancos);
            }
            if(TabCajaChicaVisible) {
                vResult = vResult || LibString.IsNullOrEmpty(CuentaCajaChicaGasto);
                vResult = vResult || LibString.IsNullOrEmpty(CuentaCajaChicaBancoHaber);
                vResult = vResult || LibString.IsNullOrEmpty(CuentaCajaChicaBancoDebe);
                vResult = vResult || LibString.IsNullOrEmpty(CuentaCajaChicaBanco);
            }
            vResult = vResult || LibString.IsNullOrEmpty(CuentaMovBancarioBancosDebe);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaMovBancarioIngresos);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaMovBancarioBancosHaber);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaMovBancarioGasto);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaAnticipoCaja);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaAnticipoCobrado);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaAnticipoOtrosIngresos);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaAnticipoPagado);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaAnticipoOtrosEgresos);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaAnticipoBanco);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaFacturacionCxCClientes);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaFacturacionDescuentos);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaFacturacionCargos);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCreditoBancarioGasto);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaFacturacionMontoTotalFactura);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaPagosCxPProveedores);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaPagosBanco);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaPagosOtros);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaPagosRetencionISLR);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaPagosPagadoAnticipo);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCobranzaCobradoEnEfectivo);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCobranzaCobradoEnCheque);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCobranzaCobradoEnTarjeta);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCobranzaOtros);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCobranzaRetencionISLR);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCobranzaRetencionIVA);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCobranzaCobradoAnticipo);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaCobranzaCxCClientes);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaTransfCtasBancoDestino);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaTransfCtasBancoOrigen);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaTransfCtasGastoComDestino);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaTransfCtasGastoComOrigen);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaOrdenDeProduccionProductoTerminado);
            vResult = vResult || LibString.IsNullOrEmpty(CuentaOrdenDeProduccionMateriaPrima);
            if((LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("ReglasDeContabilizacion","UsarVentasConIvaDiferido"))) {
                vResult = vResult || LibString.IsNullOrEmpty(CuentaFacturacionIvaDiferido);
                vResult = vResult || LibString.IsNullOrEmpty(CuentaCobranzaIvaDiferido);
            }
            return vResult;
        }


        protected override bool UpdateRecord() {
            ValidationResult vResult = ValidationResult.Success;
            eDondeSeEfectuaLaRetencionIVA DondeRetenerIVA = (eDondeSeEfectuaLaRetencionIVA)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("ReglasDeContabilizacion","DondeRetenerIVA");

            if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("ReglasDeContabilizacion","EsContribuyenteEspecial") && (int)DondeContabilizarRetIva != (int)DondeRetenerIVA) {
                StringBuilder vMensaje = new StringBuilder();
                vMensaje.AppendLine("El momento en el cual se efectúa la Retención de IVA");
                vMensaje.AppendLine("(Parámetros Administrativos -> Efectuar la Retención del IVA en: " + LibEnumHelper.GetDescription(DondeRetenerIVA) + ")");
                vMensaje.AppendLine("es diferente al momento en el cual se está contabilizando dicha retención");
                vMensaje.AppendLine("(Reglas de Contabilización -> Contabilizar la Retención de IVA en: " + LibEnumHelper.GetDescription(DondeContabilizarRetIva) + ").");
                LibMessages.MessageBox.Alert(null,vMensaje.ToString(),"Advertencia");
            }

            if(CuentasNulasVacias())
                LibMessages.MessageBox.Alert(null,"No se ha completado la selección de todas las cuentas","Advertencia");

            return base.UpdateRecord();
        }

        public bool TipoComprobanteVisible {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("ReglasDeContabilizacion","MuestraTipoComprobante");
            }
        }

        public bool TipoContribuyenteEspecial {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("ReglasDeContabilizacion","EsContribuyenteEspecial");
            }
        }

        public bool IsEnabledCxCTipoComprobante {
            get {
                return IsEnabled && LibString.IsNullOrEmpty(CxCTipoComprobante);
            }
        }

        public bool IsEnabledCxPTipoComprobante {
            get {
                return IsEnabled && LibString.IsNullOrEmpty(CxPTipoComprobante);
            }
        }

        public bool IsEnabledCobranzaTipoComprobante {
            get {
                return IsEnabled && LibString.IsNullOrEmpty(CobranzaTipoComprobante);
            }
        }

        public bool IsEnabledPagoTipoComprobante {
            get {
                return IsEnabled && LibString.IsNullOrEmpty(PagoTipoComprobante);
            }
        }

        public bool IsEnabledFacturaTipoComprobante {
            get {
                return IsEnabled && LibString.IsNullOrEmpty(FacturaTipoComprobante);
            }
        }

        public bool VentasDiferidasVisible {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("ReglasDeContabilizacion","UsarVentasConIvaDiferido");
            }
        }

        public bool IsEnabledMovimientoBancarioTipoComprobante {
            get {
                return IsEnabled && LibString.IsNullOrEmpty(MovimientoBancarioTipoComprobante);
            }
        }

        public bool IsEnabledAnticipoTipoComprobante {
            get {
                return IsEnabled && LibString.IsNullOrEmpty(AnticipoTipoComprobante);
            }
        }

        public bool IsEnabledInventarioTipoComprobante {
            get {
                return IsEnabled && LibString.IsNullOrEmpty(InventarioTipoComprobante);
            }
        }

        public bool IsEnabledPagosSueldosTipoComprobante {
            get {
                return IsEnabled && LibString.IsNullOrEmpty(PagosSueldosTipoComprobante);
            }
        }

        public bool IsEnabledSiglasTipoComprobanteCajaChica {
            get {
                return IsEnabled && LibString.IsNullOrEmpty(SiglasTipoComprobanteCajaChica);
            }
        }

        public bool IsEnabledTransfCtasSigasTipoComprobante {
            get {
                return IsEnabled && LibString.IsNullOrEmpty(TransfCtasSigasTipoComprobante);
            }
        }

        // Los elementos de Reglas de Contabilización que estén condicionados
        // por esta propiedad no serán visibles para el usuario provisionalmente.
        public bool IsVisibleContabilizacionOrdenDeProduccion {
            get {
                return false;
            }
        }

        public bool EsModuloDeProduccion {
            get {
                return EsModuloProduccion();
            }
        }

        private ValidationResult CuentaDiferenciaCambiariaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibString.IsNullOrEmpty(CuentaDiferenciaCambiaria) && (ManejarDiferenciaCambiariaEnCobranza || ManejarDiferenciaCambiariaEnPagos)) {
                    vResult = new ValidationResult("La Cuenta de Ganancia / Pérdida Cambiaria es requerida.");
                }
            }
            return vResult;
        }

        private bool EsModuloProduccion() {
            return LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "EsModuloDeProduccion"));
        }
    } //End of class ReglasDeContabilizacionViewModel
} //End of namespace Galac.Saw.Uil.Contabilizacion

