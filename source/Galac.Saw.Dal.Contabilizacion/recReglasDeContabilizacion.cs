using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Services;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Contabilizacion;

namespace Galac.Saw.Dal.Contabilizacion {
    [Serializable]
    public class recReglasDeContabilizacion {
        #region Variables
        private int _ConsecutivoCompania;
        private string _Numero;
        private bool _EditarComprobanteAfterInsert;
        private string _DiferenciaEnCambioyCalculo;
        private string _CuentaIva1Credito;
        private string _CuentaIva1Debito;
        private eDondeEfectuoContabilizacionRetIVA _DondeContabilizarRetIva;
        private string _CuentaRetencionIva;
        private eTipoDeContabilizacion _TipoContabilizacionCxC;
        private eContabilizacionIndividual _ContabIndividualCxc;
        private eContabilizacionPorLote _ContabPorLoteCxC;
        private string _CuentaCxCClientes;
        private string _CuentaCxCIngresos;
        private eTipoDeContabilizacion _TipoContabilizacionCxP;
        private eContabilizacionIndividual _ContabIndividualCxP;
        private eContabilizacionPorLote _ContabPorLoteCxP;
        private string _CuentaCxPGasto;
        private string _CuentaCxPProveedores;
        private eTipoDeContabilizacion _TipoContabilizacionCobranza;
        private eContabilizacionIndividual _ContabIndividualCobranza;
        private eContabilizacionPorLote _ContabPorLoteCobranza;
        private string _CuentaCobranzaCobradoEnEfectivo;
        private string _CuentaCobranzaCobradoEnCheque;
        private string _CuentaCobranzaCobradoEnTarjeta;
        private string _cuentaCobranzaRetencionISLR;
        private string _cuentaCobranzaRetencionIVA;
        private string _CuentaCobranzaOtros;
        private string _CuentaCobranzaCxCClientes;
        private string _CuentaCobranzaCobradoAnticipo;
        private eTipoDeContabilizacion _TipoContabilizacionPagos;
        private eContabilizacionIndividual _ContabIndividualPagos;
        private eContabilizacionPorLote _ContabPorLotePagos;
        private string _CuentaPagosCxPProveedores;
        private string _CuentaPagosRetencionISLR;
        private string _CuentaPagosOtros;
        private string _CuentaPagosBanco;
        private string _CuentaPagosPagadoAnticipo;
        private eTipoDeContabilizacion _TipoContabilizacionFacturacion;
        private eContabilizacionIndividual _ContabIndividualFacturacion;
        private eContabilizacionPorLote _ContabPorLoteFacturacion;
        private string _CuentaFacturacionCxCClientes;
        private string _CuentaFacturacionMontoTotalFactura;
        private string _CuentaFacturacionCargos;
        private string _CuentaFacturacionDescuentos;
        private bool _ContabilizarPorArticulo;
        private bool _AgruparPorCuentaDeArticulo;
        private bool _AgruparPorCargosDescuentos;
        private eTipoDeContabilizacion _TipoContabilizacionRDVtas;
        private eContabilizacionIndividual _ContabIndividualRDVtas;
        private eContabilizacionPorLote _ContabPorLoteRDVtas;
        private string _CuentaRDVtasCaja;
        private string _CuentaRDVtasMontoTotal;
        private bool _ContabilizarPorArticuloRDVtas;
        private bool _AgruparPorCuentaDeArticuloRDVtas;
        private eTipoDeContabilizacion _TipoContabilizacionMovBancario;
        private eContabilizacionIndividual _ContabIndividualMovBancario;
        private eContabilizacionPorLote _ContabPorLoteMovBancario;
        private string _CuentaMovBancarioGasto;
        private string _CuentaMovBancarioBancosHaber;
        private string _CuentaMovBancarioBancosDebe;
        private string _CuentaMovBancarioIngresos;
        private string _CuentaDebitoBancarioGasto;
        private string _CuentaDebitoBancarioBancos;
        private string _CuentaCreditoBancarioGasto;
        private string _CuentaCreditoBancarioBancos;
        private eTipoDeContabilizacion _TipoContabilizacionAnticipo;
        private eContabilizacionIndividual _ContabIndividualAnticipo;
        private eContabilizacionPorLote _ContabPorLoteAnticipo;
        private string _CuentaAnticipoCaja;
        private string _CuentaAnticipoCobrado;
        private string _CuentaAnticipoOtrosIngresos;
        private string _CuentaAnticipoPagado;
        private string _CuentaAnticipoBanco;
        private string _CuentaAnticipoOtrosEgresos;
        private string _FacturaTipoComprobante;
        private string _CxCTipoComprobante;
        private string _CxPTipoComprobante;
        private string _CobranzaTipoComprobante;
        private string _PagoTipoComprobante;
        private string _MovimientoBancarioTipoComprobante;
        private string _AnticipoTipoComprobante;
        private string _CuentaCostoDeVenta;
        private string _CuentaInventario;
        private eTipoDeContabilizacion _TipoContabilizacionInventario;
        private bool _AgruparPorCuentaDeArticuloInven;
        private string _InventarioTipoComprobante;
        private string _CtaDePagosSueldos;
        private string _CtaDePagosSueldosBanco;
        private eContabilizacionIndividual _ContabIndividualPagosSueldos;
        private string _PagosSueldosTipoComprobante;
        private eTipoDeContabilizacion _TipoContabilizacionDePagosSueldos;
        private bool _EditarComprobanteDePagosSueldos;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Reglas de Contabilización"; }
        }

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string Numero {
            get { return _Numero; }
            set { _Numero = LibString.Mid(value, 0, 11); }
        }

        public bool EditarComprobanteAfterInsert {
            get { return _EditarComprobanteAfterInsert; }
            set { _EditarComprobanteAfterInsert = value; }
        }

        public string DiferenciaEnCambioyCalculo {
            get { return _DiferenciaEnCambioyCalculo; }
            set { _DiferenciaEnCambioyCalculo = LibString.Mid(value, 0, 30); }
        }

        public string CuentaIva1Credito {
            get { return _CuentaIva1Credito; }
            set { _CuentaIva1Credito = LibString.Mid(value, 0, 30); }
        }

        public string CuentaIva1Debito {
            get { return _CuentaIva1Debito; }
            set { _CuentaIva1Debito = LibString.Mid(value, 0, 30); }
        }

        public eDondeEfectuoContabilizacionRetIVA DondeContabilizarRetIva {
            get { return _DondeContabilizarRetIva; }
            set { _DondeContabilizarRetIva = value; }
        }

        public string DondeContabilizarRetIvaAsDB {
            get { return LibConvert.EnumToDbValue((int) _DondeContabilizarRetIva); }
        }

        public string DondeContabilizarRetIvaAsString {
            get { return LibEnumHelper.GetDescription(_DondeContabilizarRetIva); }
        }

        public string CuentaRetencionIva {
            get { return _CuentaRetencionIva; }
            set { _CuentaRetencionIva = LibString.Mid(value, 0, 30); }
        }

        public eTipoDeContabilizacion TipoContabilizacionCxC {
            get { return _TipoContabilizacionCxC; }
            set { _TipoContabilizacionCxC = value; }
        }

        public string TipoContabilizacionCxCAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoContabilizacionCxC); }
        }

        public string TipoContabilizacionCxCAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionCxC); }
        }

        public eContabilizacionIndividual ContabIndividualCxc {
            get { return _ContabIndividualCxc; }
            set { _ContabIndividualCxc = value; }
        }

        public string ContabIndividualCxcAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabIndividualCxc); }
        }

        public string ContabIndividualCxcAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualCxc); }
        }

        public eContabilizacionPorLote ContabPorLoteCxC {
            get { return _ContabPorLoteCxC; }
            set { _ContabPorLoteCxC = value; }
        }

        public string ContabPorLoteCxCAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabPorLoteCxC); }
        }

        public string ContabPorLoteCxCAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteCxC); }
        }

        public string CuentaCxCClientes {
            get { return _CuentaCxCClientes; }
            set { _CuentaCxCClientes = LibString.Mid(value, 0, 30); }
        }

        public string CuentaCxCIngresos {
            get { return _CuentaCxCIngresos; }
            set { _CuentaCxCIngresos = LibString.Mid(value, 0, 30); }
        }

        public eTipoDeContabilizacion TipoContabilizacionCxP {
            get { return _TipoContabilizacionCxP; }
            set { _TipoContabilizacionCxP = value; }
        }

        public string TipoContabilizacionCxPAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoContabilizacionCxP); }
        }

        public string TipoContabilizacionCxPAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionCxP); }
        }

        public eContabilizacionIndividual ContabIndividualCxP {
            get { return _ContabIndividualCxP; }
            set { _ContabIndividualCxP = value; }
        }

        public string ContabIndividualCxPAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabIndividualCxP); }
        }

        public string ContabIndividualCxPAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualCxP); }
        }

        public eContabilizacionPorLote ContabPorLoteCxP {
            get { return _ContabPorLoteCxP; }
            set { _ContabPorLoteCxP = value; }
        }

        public string ContabPorLoteCxPAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabPorLoteCxP); }
        }

        public string ContabPorLoteCxPAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteCxP); }
        }

        public string CuentaCxPGasto {
            get { return _CuentaCxPGasto; }
            set { _CuentaCxPGasto = LibString.Mid(value, 0, 30); }
        }

        public string CuentaCxPProveedores {
            get { return _CuentaCxPProveedores; }
            set { _CuentaCxPProveedores = LibString.Mid(value, 0, 30); }
        }

        public eTipoDeContabilizacion TipoContabilizacionCobranza {
            get { return _TipoContabilizacionCobranza; }
            set { _TipoContabilizacionCobranza = value; }
        }

        public string TipoContabilizacionCobranzaAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoContabilizacionCobranza); }
        }

        public string TipoContabilizacionCobranzaAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionCobranza); }
        }

        public eContabilizacionIndividual ContabIndividualCobranza {
            get { return _ContabIndividualCobranza; }
            set { _ContabIndividualCobranza = value; }
        }

        public string ContabIndividualCobranzaAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabIndividualCobranza); }
        }

        public string ContabIndividualCobranzaAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualCobranza); }
        }

        public eContabilizacionPorLote ContabPorLoteCobranza {
            get { return _ContabPorLoteCobranza; }
            set { _ContabPorLoteCobranza = value; }
        }

        public string ContabPorLoteCobranzaAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabPorLoteCobranza); }
        }

        public string ContabPorLoteCobranzaAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteCobranza); }
        }

        public string CuentaCobranzaCobradoEnEfectivo {
            get { return _CuentaCobranzaCobradoEnEfectivo; }
            set { _CuentaCobranzaCobradoEnEfectivo = LibString.Mid(value, 0, 30); }
        }

        public string CuentaCobranzaCobradoEnCheque {
            get { return _CuentaCobranzaCobradoEnCheque; }
            set { _CuentaCobranzaCobradoEnCheque = LibString.Mid(value, 0, 30); }
        }

        public string CuentaCobranzaCobradoEnTarjeta {
            get { return _CuentaCobranzaCobradoEnTarjeta; }
            set { _CuentaCobranzaCobradoEnTarjeta = LibString.Mid(value, 0, 30); }
        }

        public string cuentaCobranzaRetencionISLR {
            get { return _cuentaCobranzaRetencionISLR; }
            set { _cuentaCobranzaRetencionISLR = LibString.Mid(value, 0, 30); }
        }

        public string cuentaCobranzaRetencionIVA {
            get { return _cuentaCobranzaRetencionIVA; }
            set { _cuentaCobranzaRetencionIVA = LibString.Mid(value, 0, 30); }
        }

        public string CuentaCobranzaOtros {
            get { return _CuentaCobranzaOtros; }
            set { _CuentaCobranzaOtros = LibString.Mid(value, 0, 30); }
        }

        public string CuentaCobranzaCxCClientes {
            get { return _CuentaCobranzaCxCClientes; }
            set { _CuentaCobranzaCxCClientes = LibString.Mid(value, 0, 30); }
        }

        public string CuentaCobranzaCobradoAnticipo {
            get { return _CuentaCobranzaCobradoAnticipo; }
            set { _CuentaCobranzaCobradoAnticipo = LibString.Mid(value, 0, 30); }
        }

        public eTipoDeContabilizacion TipoContabilizacionPagos {
            get { return _TipoContabilizacionPagos; }
            set { _TipoContabilizacionPagos = value; }
        }

        public string TipoContabilizacionPagosAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoContabilizacionPagos); }
        }

        public string TipoContabilizacionPagosAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionPagos); }
        }

        public eContabilizacionIndividual ContabIndividualPagos {
            get { return _ContabIndividualPagos; }
            set { _ContabIndividualPagos = value; }
        }

        public string ContabIndividualPagosAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabIndividualPagos); }
        }

        public string ContabIndividualPagosAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualPagos); }
        }

        public eContabilizacionPorLote ContabPorLotePagos {
            get { return _ContabPorLotePagos; }
            set { _ContabPorLotePagos = value; }
        }

        public string ContabPorLotePagosAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabPorLotePagos); }
        }

        public string ContabPorLotePagosAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLotePagos); }
        }

        public string CuentaPagosCxPProveedores {
            get { return _CuentaPagosCxPProveedores; }
            set { _CuentaPagosCxPProveedores = LibString.Mid(value, 0, 30); }
        }

        public string CuentaPagosRetencionISLR {
            get { return _CuentaPagosRetencionISLR; }
            set { _CuentaPagosRetencionISLR = LibString.Mid(value, 0, 30); }
        }

        public string CuentaPagosOtros {
            get { return _CuentaPagosOtros; }
            set { _CuentaPagosOtros = LibString.Mid(value, 0, 30); }
        }

        public string CuentaPagosBanco {
            get { return _CuentaPagosBanco; }
            set { _CuentaPagosBanco = LibString.Mid(value, 0, 30); }
        }

        public string CuentaPagosPagadoAnticipo {
            get { return _CuentaPagosPagadoAnticipo; }
            set { _CuentaPagosPagadoAnticipo = LibString.Mid(value, 0, 30); }
        }

        public eTipoDeContabilizacion TipoContabilizacionFacturacion {
            get { return _TipoContabilizacionFacturacion; }
            set { _TipoContabilizacionFacturacion = value; }
        }

        public string TipoContabilizacionFacturacionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoContabilizacionFacturacion); }
        }

        public string TipoContabilizacionFacturacionAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionFacturacion); }
        }

        public eContabilizacionIndividual ContabIndividualFacturacion {
            get { return _ContabIndividualFacturacion; }
            set { _ContabIndividualFacturacion = value; }
        }

        public string ContabIndividualFacturacionAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabIndividualFacturacion); }
        }

        public string ContabIndividualFacturacionAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualFacturacion); }
        }

        public eContabilizacionPorLote ContabPorLoteFacturacion {
            get { return _ContabPorLoteFacturacion; }
            set { _ContabPorLoteFacturacion = value; }
        }

        public string ContabPorLoteFacturacionAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabPorLoteFacturacion); }
        }

        public string ContabPorLoteFacturacionAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteFacturacion); }
        }

        public string CuentaFacturacionCxCClientes {
            get { return _CuentaFacturacionCxCClientes; }
            set { _CuentaFacturacionCxCClientes = LibString.Mid(value, 0, 30); }
        }

        public string CuentaFacturacionMontoTotalFactura {
            get { return _CuentaFacturacionMontoTotalFactura; }
            set { _CuentaFacturacionMontoTotalFactura = LibString.Mid(value, 0, 30); }
        }

        public string CuentaFacturacionCargos {
            get { return _CuentaFacturacionCargos; }
            set { _CuentaFacturacionCargos = LibString.Mid(value, 0, 30); }
        }

        public string CuentaFacturacionDescuentos {
            get { return _CuentaFacturacionDescuentos; }
            set { _CuentaFacturacionDescuentos = LibString.Mid(value, 0, 30); }
        }

        public bool ContabilizarPorArticulo {
            get { return _ContabilizarPorArticulo; }
            set { _ContabilizarPorArticulo = value; }
        }

        public bool AgruparPorCuentaDeArticulo {
            get { return _AgruparPorCuentaDeArticulo; }
            set { _AgruparPorCuentaDeArticulo = value; }
        }

        public bool AgruparPorCargosDescuentos {
            get { return _AgruparPorCargosDescuentos; }
            set { _AgruparPorCargosDescuentos = value; }
        }

        public eTipoDeContabilizacion TipoContabilizacionRDVtas {
            get { return _TipoContabilizacionRDVtas; }
            set { _TipoContabilizacionRDVtas = value; }
        }

        public string TipoContabilizacionRDVtasAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoContabilizacionRDVtas); }
        }

        public string TipoContabilizacionRDVtasAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionRDVtas); }
        }

        public eContabilizacionIndividual ContabIndividualRDVtas {
            get { return _ContabIndividualRDVtas; }
            set { _ContabIndividualRDVtas = value; }
        }

        public string ContabIndividualRDVtasAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabIndividualRDVtas); }
        }

        public string ContabIndividualRDVtasAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualRDVtas); }
        }

        public eContabilizacionPorLote ContabPorLoteRDVtas {
            get { return _ContabPorLoteRDVtas; }
            set { _ContabPorLoteRDVtas = value; }
        }

        public string ContabPorLoteRDVtasAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabPorLoteRDVtas); }
        }

        public string ContabPorLoteRDVtasAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteRDVtas); }
        }

        public string CuentaRDVtasCaja {
            get { return _CuentaRDVtasCaja; }
            set { _CuentaRDVtasCaja = LibString.Mid(value, 0, 30); }
        }

        public string CuentaRDVtasMontoTotal {
            get { return _CuentaRDVtasMontoTotal; }
            set { _CuentaRDVtasMontoTotal = LibString.Mid(value, 0, 30); }
        }

        public bool ContabilizarPorArticuloRDVtas {
            get { return _ContabilizarPorArticuloRDVtas; }
            set { _ContabilizarPorArticuloRDVtas = value; }
        }

        public bool AgruparPorCuentaDeArticuloRDVtas {
            get { return _AgruparPorCuentaDeArticuloRDVtas; }
            set { _AgruparPorCuentaDeArticuloRDVtas = value; }
        }

        public eTipoDeContabilizacion TipoContabilizacionMovBancario {
            get { return _TipoContabilizacionMovBancario; }
            set { _TipoContabilizacionMovBancario = value; }
        }

        public string TipoContabilizacionMovBancarioAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoContabilizacionMovBancario); }
        }

        public string TipoContabilizacionMovBancarioAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionMovBancario); }
        }

        public eContabilizacionIndividual ContabIndividualMovBancario {
            get { return _ContabIndividualMovBancario; }
            set { _ContabIndividualMovBancario = value; }
        }

        public string ContabIndividualMovBancarioAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabIndividualMovBancario); }
        }

        public string ContabIndividualMovBancarioAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualMovBancario); }
        }

        public eContabilizacionPorLote ContabPorLoteMovBancario {
            get { return _ContabPorLoteMovBancario; }
            set { _ContabPorLoteMovBancario = value; }
        }

        public string ContabPorLoteMovBancarioAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabPorLoteMovBancario); }
        }

        public string ContabPorLoteMovBancarioAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteMovBancario); }
        }

        public string CuentaMovBancarioGasto {
            get { return _CuentaMovBancarioGasto; }
            set { _CuentaMovBancarioGasto = LibString.Mid(value, 0, 30); }
        }

        public string CuentaMovBancarioBancosHaber {
            get { return _CuentaMovBancarioBancosHaber; }
            set { _CuentaMovBancarioBancosHaber = LibString.Mid(value, 0, 30); }
        }

        public string CuentaMovBancarioBancosDebe {
            get { return _CuentaMovBancarioBancosDebe; }
            set { _CuentaMovBancarioBancosDebe = LibString.Mid(value, 0, 30); }
        }

        public string CuentaMovBancarioIngresos {
            get { return _CuentaMovBancarioIngresos; }
            set { _CuentaMovBancarioIngresos = LibString.Mid(value, 0, 30); }
        }

        public string CuentaDebitoBancarioGasto {
            get { return _CuentaDebitoBancarioGasto; }
            set { _CuentaDebitoBancarioGasto = LibString.Mid(value, 0, 30); }
        }

        public string CuentaDebitoBancarioBancos {
            get { return _CuentaDebitoBancarioBancos; }
            set { _CuentaDebitoBancarioBancos = LibString.Mid(value, 0, 30); }
        }

        public string CuentaCreditoBancarioGasto {
            get { return _CuentaCreditoBancarioGasto; }
            set { _CuentaCreditoBancarioGasto = LibString.Mid(value, 0, 30); }
        }

        public string CuentaCreditoBancarioBancos {
            get { return _CuentaCreditoBancarioBancos; }
            set { _CuentaCreditoBancarioBancos = LibString.Mid(value, 0, 30); }
        }

        public eTipoDeContabilizacion TipoContabilizacionAnticipo {
            get { return _TipoContabilizacionAnticipo; }
            set { _TipoContabilizacionAnticipo = value; }
        }

        public string TipoContabilizacionAnticipoAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoContabilizacionAnticipo); }
        }

        public string TipoContabilizacionAnticipoAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionAnticipo); }
        }

        public eContabilizacionIndividual ContabIndividualAnticipo {
            get { return _ContabIndividualAnticipo; }
            set { _ContabIndividualAnticipo = value; }
        }

        public string ContabIndividualAnticipoAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabIndividualAnticipo); }
        }

        public string ContabIndividualAnticipoAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualAnticipo); }
        }

        public eContabilizacionPorLote ContabPorLoteAnticipo {
            get { return _ContabPorLoteAnticipo; }
            set { _ContabPorLoteAnticipo = value; }
        }

        public string ContabPorLoteAnticipoAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabPorLoteAnticipo); }
        }

        public string ContabPorLoteAnticipoAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteAnticipo); }
        }

        public string CuentaAnticipoCaja {
            get { return _CuentaAnticipoCaja; }
            set { _CuentaAnticipoCaja = LibString.Mid(value, 0, 30); }
        }

        public string CuentaAnticipoCobrado {
            get { return _CuentaAnticipoCobrado; }
            set { _CuentaAnticipoCobrado = LibString.Mid(value, 0, 30); }
        }

        public string CuentaAnticipoOtrosIngresos {
            get { return _CuentaAnticipoOtrosIngresos; }
            set { _CuentaAnticipoOtrosIngresos = LibString.Mid(value, 0, 30); }
        }

        public string CuentaAnticipoPagado {
            get { return _CuentaAnticipoPagado; }
            set { _CuentaAnticipoPagado = LibString.Mid(value, 0, 30); }
        }

        public string CuentaAnticipoBanco {
            get { return _CuentaAnticipoBanco; }
            set { _CuentaAnticipoBanco = LibString.Mid(value, 0, 30); }
        }

        public string CuentaAnticipoOtrosEgresos {
            get { return _CuentaAnticipoOtrosEgresos; }
            set { _CuentaAnticipoOtrosEgresos = LibString.Mid(value, 0, 30); }
        }

        public string FacturaTipoComprobante {
            get { return _FacturaTipoComprobante; }
            set { _FacturaTipoComprobante = LibString.Mid(value, 0, 2); }
        }

        public string CxCTipoComprobante {
            get { return _CxCTipoComprobante; }
            set { _CxCTipoComprobante = LibString.Mid(value, 0, 2); }
        }

        public string CxPTipoComprobante {
            get { return _CxPTipoComprobante; }
            set { _CxPTipoComprobante = LibString.Mid(value, 0, 2); }
        }

        public string CobranzaTipoComprobante {
            get { return _CobranzaTipoComprobante; }
            set { _CobranzaTipoComprobante = LibString.Mid(value, 0, 2); }
        }

        public string PagoTipoComprobante {
            get { return _PagoTipoComprobante; }
            set { _PagoTipoComprobante = LibString.Mid(value, 0, 2); }
        }

        public string MovimientoBancarioTipoComprobante {
            get { return _MovimientoBancarioTipoComprobante; }
            set { _MovimientoBancarioTipoComprobante = LibString.Mid(value, 0, 2); }
        }

        public string AnticipoTipoComprobante {
            get { return _AnticipoTipoComprobante; }
            set { _AnticipoTipoComprobante = LibString.Mid(value, 0, 2); }
        }

        public string CuentaCostoDeVenta {
            get { return _CuentaCostoDeVenta; }
            set { _CuentaCostoDeVenta = LibString.Mid(value, 0, 30); }
        }

        public string CuentaInventario {
            get { return _CuentaInventario; }
            set { _CuentaInventario = LibString.Mid(value, 0, 30); }
        }

        public eTipoDeContabilizacion TipoContabilizacionInventario {
            get { return _TipoContabilizacionInventario; }
            set { _TipoContabilizacionInventario = value; }
        }

        public string TipoContabilizacionInventarioAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoContabilizacionInventario); }
        }

        public string TipoContabilizacionInventarioAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionInventario); }
        }

        public bool AgruparPorCuentaDeArticuloInven {
            get { return _AgruparPorCuentaDeArticuloInven; }
            set { _AgruparPorCuentaDeArticuloInven = value; }
        }

        public string InventarioTipoComprobante {
            get { return _InventarioTipoComprobante; }
            set { _InventarioTipoComprobante = LibString.Mid(value, 0, 2); }
        }

        public string CtaDePagosSueldos {
            get { return _CtaDePagosSueldos; }
            set { _CtaDePagosSueldos = LibString.Mid(value, 0, 30); }
        }

        public string CtaDePagosSueldosBanco {
            get { return _CtaDePagosSueldosBanco; }
            set { _CtaDePagosSueldosBanco = LibString.Mid(value, 0, 30); }
        }

        public eContabilizacionIndividual ContabIndividualPagosSueldos {
            get { return _ContabIndividualPagosSueldos; }
            set { _ContabIndividualPagosSueldos = value; }
        }

        public string ContabIndividualPagosSueldosAsDB {
            get { return LibConvert.EnumToDbValue((int) _ContabIndividualPagosSueldos); }
        }

        public string ContabIndividualPagosSueldosAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualPagosSueldos); }
        }

        public string PagosSueldosTipoComprobante {
            get { return _PagosSueldosTipoComprobante; }
            set { _PagosSueldosTipoComprobante = LibString.Mid(value, 0, 2); }
        }

        public eTipoDeContabilizacion TipoContabilizacionDePagosSueldos {
            get { return _TipoContabilizacionDePagosSueldos; }
            set { _TipoContabilizacionDePagosSueldos = value; }
        }

        public string TipoContabilizacionDePagosSueldosAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoContabilizacionDePagosSueldos); }
        }

        public string TipoContabilizacionDePagosSueldosAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionDePagosSueldos); }
        }

        public bool EditarComprobanteDePagosSueldos {
            get { return _EditarComprobanteDePagosSueldos; }
            set { _EditarComprobanteDePagosSueldos = value; }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 10); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores
        public recReglasDeContabilizacion(){
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public recReglasDeContabilizacion Clone() {
            recReglasDeContabilizacion vResult = new recReglasDeContabilizacion();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Numero = _Numero;
            vResult.EditarComprobanteAfterInsert = _EditarComprobanteAfterInsert;
            vResult.DiferenciaEnCambioyCalculo = _DiferenciaEnCambioyCalculo;
            vResult.CuentaIva1Credito = _CuentaIva1Credito;
            vResult.CuentaIva1Debito = _CuentaIva1Debito;
            vResult.DondeContabilizarRetIva = _DondeContabilizarRetIva;
            vResult.CuentaRetencionIva = _CuentaRetencionIva;
            vResult.TipoContabilizacionCxC = _TipoContabilizacionCxC;
            vResult.ContabIndividualCxc = _ContabIndividualCxc;
            vResult.ContabPorLoteCxC = _ContabPorLoteCxC;
            vResult.CuentaCxCClientes = _CuentaCxCClientes;
            vResult.CuentaCxCIngresos = _CuentaCxCIngresos;
            vResult.TipoContabilizacionCxP = _TipoContabilizacionCxP;
            vResult.ContabIndividualCxP = _ContabIndividualCxP;
            vResult.ContabPorLoteCxP = _ContabPorLoteCxP;
            vResult.CuentaCxPGasto = _CuentaCxPGasto;
            vResult.CuentaCxPProveedores = _CuentaCxPProveedores;
            vResult.TipoContabilizacionCobranza = _TipoContabilizacionCobranza;
            vResult.ContabIndividualCobranza = _ContabIndividualCobranza;
            vResult.ContabPorLoteCobranza = _ContabPorLoteCobranza;
            vResult.CuentaCobranzaCobradoEnEfectivo = _CuentaCobranzaCobradoEnEfectivo;
            vResult.CuentaCobranzaCobradoEnCheque = _CuentaCobranzaCobradoEnCheque;
            vResult.CuentaCobranzaCobradoEnTarjeta = _CuentaCobranzaCobradoEnTarjeta;
            vResult.cuentaCobranzaRetencionISLR = _cuentaCobranzaRetencionISLR;
            vResult.cuentaCobranzaRetencionIVA = _cuentaCobranzaRetencionIVA;
            vResult.CuentaCobranzaOtros = _CuentaCobranzaOtros;
            vResult.CuentaCobranzaCxCClientes = _CuentaCobranzaCxCClientes;
            vResult.CuentaCobranzaCobradoAnticipo = _CuentaCobranzaCobradoAnticipo;
            vResult.TipoContabilizacionPagos = _TipoContabilizacionPagos;
            vResult.ContabIndividualPagos = _ContabIndividualPagos;
            vResult.ContabPorLotePagos = _ContabPorLotePagos;
            vResult.CuentaPagosCxPProveedores = _CuentaPagosCxPProveedores;
            vResult.CuentaPagosRetencionISLR = _CuentaPagosRetencionISLR;
            vResult.CuentaPagosOtros = _CuentaPagosOtros;
            vResult.CuentaPagosBanco = _CuentaPagosBanco;
            vResult.CuentaPagosPagadoAnticipo = _CuentaPagosPagadoAnticipo;
            vResult.TipoContabilizacionFacturacion = _TipoContabilizacionFacturacion;
            vResult.ContabIndividualFacturacion = _ContabIndividualFacturacion;
            vResult.ContabPorLoteFacturacion = _ContabPorLoteFacturacion;
            vResult.CuentaFacturacionCxCClientes = _CuentaFacturacionCxCClientes;
            vResult.CuentaFacturacionMontoTotalFactura = _CuentaFacturacionMontoTotalFactura;
            vResult.CuentaFacturacionCargos = _CuentaFacturacionCargos;
            vResult.CuentaFacturacionDescuentos = _CuentaFacturacionDescuentos;
            vResult.ContabilizarPorArticulo = _ContabilizarPorArticulo;
            vResult.AgruparPorCuentaDeArticulo = _AgruparPorCuentaDeArticulo;
            vResult.AgruparPorCargosDescuentos = _AgruparPorCargosDescuentos;
            vResult.TipoContabilizacionRDVtas = _TipoContabilizacionRDVtas;
            vResult.ContabIndividualRDVtas = _ContabIndividualRDVtas;
            vResult.ContabPorLoteRDVtas = _ContabPorLoteRDVtas;
            vResult.CuentaRDVtasCaja = _CuentaRDVtasCaja;
            vResult.CuentaRDVtasMontoTotal = _CuentaRDVtasMontoTotal;
            vResult.ContabilizarPorArticuloRDVtas = _ContabilizarPorArticuloRDVtas;
            vResult.AgruparPorCuentaDeArticuloRDVtas = _AgruparPorCuentaDeArticuloRDVtas;
            vResult.TipoContabilizacionMovBancario = _TipoContabilizacionMovBancario;
            vResult.ContabIndividualMovBancario = _ContabIndividualMovBancario;
            vResult.ContabPorLoteMovBancario = _ContabPorLoteMovBancario;
            vResult.CuentaMovBancarioGasto = _CuentaMovBancarioGasto;
            vResult.CuentaMovBancarioBancosHaber = _CuentaMovBancarioBancosHaber;
            vResult.CuentaMovBancarioBancosDebe = _CuentaMovBancarioBancosDebe;
            vResult.CuentaMovBancarioIngresos = _CuentaMovBancarioIngresos;
            vResult.CuentaDebitoBancarioGasto = _CuentaDebitoBancarioGasto;
            vResult.CuentaDebitoBancarioBancos = _CuentaDebitoBancarioBancos;
            vResult.CuentaCreditoBancarioGasto = _CuentaCreditoBancarioGasto;
            vResult.CuentaCreditoBancarioBancos = _CuentaCreditoBancarioBancos;
            vResult.TipoContabilizacionAnticipo = _TipoContabilizacionAnticipo;
            vResult.ContabIndividualAnticipo = _ContabIndividualAnticipo;
            vResult.ContabPorLoteAnticipo = _ContabPorLoteAnticipo;
            vResult.CuentaAnticipoCaja = _CuentaAnticipoCaja;
            vResult.CuentaAnticipoCobrado = _CuentaAnticipoCobrado;
            vResult.CuentaAnticipoOtrosIngresos = _CuentaAnticipoOtrosIngresos;
            vResult.CuentaAnticipoPagado = _CuentaAnticipoPagado;
            vResult.CuentaAnticipoBanco = _CuentaAnticipoBanco;
            vResult.CuentaAnticipoOtrosEgresos = _CuentaAnticipoOtrosEgresos;
            vResult.FacturaTipoComprobante = _FacturaTipoComprobante;
            vResult.CxCTipoComprobante = _CxCTipoComprobante;
            vResult.CxPTipoComprobante = _CxPTipoComprobante;
            vResult.CobranzaTipoComprobante = _CobranzaTipoComprobante;
            vResult.PagoTipoComprobante = _PagoTipoComprobante;
            vResult.MovimientoBancarioTipoComprobante = _MovimientoBancarioTipoComprobante;
            vResult.AnticipoTipoComprobante = _AnticipoTipoComprobante;
            vResult.CuentaCostoDeVenta = _CuentaCostoDeVenta;
            vResult.CuentaInventario = _CuentaInventario;
            vResult.TipoContabilizacionInventario = _TipoContabilizacionInventario;
            vResult.AgruparPorCuentaDeArticuloInven = _AgruparPorCuentaDeArticuloInven;
            vResult.InventarioTipoComprobante = _InventarioTipoComprobante;
            vResult.CtaDePagosSueldos = _CtaDePagosSueldos;
            vResult.CtaDePagosSueldosBanco = _CtaDePagosSueldosBanco;
            vResult.ContabIndividualPagosSueldos = _ContabIndividualPagosSueldos;
            vResult.PagosSueldosTipoComprobante = _PagosSueldosTipoComprobante;
            vResult.TipoContabilizacionDePagosSueldos = _TipoContabilizacionDePagosSueldos;
            vResult.EditarComprobanteDePagosSueldos = _EditarComprobanteDePagosSueldos;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNúmero = " + _Numero +
               "\nEditar Comprobante After Insert = " + _EditarComprobanteAfterInsert +
               "\nDiferencia En Cambioy Calculo = " + _DiferenciaEnCambioyCalculo +
               "\nCuenta Iva 1Credito = " + _CuentaIva1Credito +
               "\nCuenta Iva 1Debito = " + _CuentaIva1Debito +
               "\nDonde Contabilizar Ret Iva = " + _DondeContabilizarRetIva.ToString() +
               "\nCuenta Retencion Iva = " + _CuentaRetencionIva +
               "\nTipo Contabilizacion Cx C = " + _TipoContabilizacionCxC.ToString() +
               "\nContab Individual Cxc = " + _ContabIndividualCxc.ToString() +
               "\nContab Por Lote Cx C = " + _ContabPorLoteCxC.ToString() +
               "\nCuenta Cx CClientes = " + _CuentaCxCClientes +
               "\nCuenta Cx CIngresos = " + _CuentaCxCIngresos +
               "\nTipo Contabilizacion Cx P = " + _TipoContabilizacionCxP.ToString() +
               "\nContab Individual Cx P = " + _ContabIndividualCxP.ToString() +
               "\nContab Por Lote Cx P = " + _ContabPorLoteCxP.ToString() +
               "\nCuenta Cx PGasto = " + _CuentaCxPGasto +
               "\nCuenta Cx PProveedores = " + _CuentaCxPProveedores +
               "\nTipo Contabilizacion Cobranza = " + _TipoContabilizacionCobranza.ToString() +
               "\nContab Individual Cobranza = " + _ContabIndividualCobranza.ToString() +
               "\nContab Por Lote Cobranza = " + _ContabPorLoteCobranza.ToString() +
               "\nCuenta Cobranza Cobrado En Efectivo = " + _CuentaCobranzaCobradoEnEfectivo +
               "\nCuenta Cobranza Cobrado En Cheque = " + _CuentaCobranzaCobradoEnCheque +
               "\nCuenta Cobranza Cobrado En Tarjeta = " + _CuentaCobranzaCobradoEnTarjeta +
               "\ncuenta Cobranza Retencion ISLR = " + _cuentaCobranzaRetencionISLR +
               "\ncuenta Cobranza Retencion IVA = " + _cuentaCobranzaRetencionIVA +
               "\nCuenta Cobranza Otros = " + _CuentaCobranzaOtros +
               "\nCuenta Cobranza Cx CClientes = " + _CuentaCobranzaCxCClientes +
               "\nCuenta Cobranza Cobrado Anticipo = " + _CuentaCobranzaCobradoAnticipo +
               "\nTipo Contabilizacion Pagos = " + _TipoContabilizacionPagos.ToString() +
               "\nContab Individual Pagos = " + _ContabIndividualPagos.ToString() +
               "\nContab Por Lote Pagos = " + _ContabPorLotePagos.ToString() +
               "\nCuenta Pagos Cx PProveedores = " + _CuentaPagosCxPProveedores +
               "\nCuenta Pagos Retencion ISLR = " + _CuentaPagosRetencionISLR +
               "\nCuenta Pagos Otros = " + _CuentaPagosOtros +
               "\nCuenta Pagos Banco = " + _CuentaPagosBanco +
               "\nCuenta Pagos Pagado Anticipo = " + _CuentaPagosPagadoAnticipo +
               "\nTipo Contabilizacion Facturacion = " + _TipoContabilizacionFacturacion.ToString() +
               "\nContab Individual Facturacion = " + _ContabIndividualFacturacion.ToString() +
               "\nContab Por Lote Facturacion = " + _ContabPorLoteFacturacion.ToString() +
               "\nCuenta Facturacion Cx CClientes = " + _CuentaFacturacionCxCClientes +
               "\nCuenta Facturacion Monto Total Factura = " + _CuentaFacturacionMontoTotalFactura +
               "\nCuenta Facturacion Cargos = " + _CuentaFacturacionCargos +
               "\nCuenta Facturacion Descuentos = " + _CuentaFacturacionDescuentos +
               "\nContabilizar Por Articulo = " + _ContabilizarPorArticulo +
               "\nAgrupar Por Cuenta De Articulo = " + _AgruparPorCuentaDeArticulo +
               "\nAgrupar Por Cargos Descuentos = " + _AgruparPorCargosDescuentos +
               "\nTipo Contabilizacion RDVtas = " + _TipoContabilizacionRDVtas.ToString() +
               "\nContab Individual RDVtas = " + _ContabIndividualRDVtas.ToString() +
               "\nContab Por Lote RDVtas = " + _ContabPorLoteRDVtas.ToString() +
               "\nCuenta RDVtas Caja = " + _CuentaRDVtasCaja +
               "\nCuenta RDVtas Monto Total = " + _CuentaRDVtasMontoTotal +
               "\nContabilizar Por Articulo RDVtas = " + _ContabilizarPorArticuloRDVtas +
               "\nAgrupar Por Cuenta De Articulo RDVtas = " + _AgruparPorCuentaDeArticuloRDVtas +
               "\nTipo Contabilizacion Mov Bancario = " + _TipoContabilizacionMovBancario.ToString() +
               "\nContab Individual Mov Bancario = " + _ContabIndividualMovBancario.ToString() +
               "\nContab Por Lote Mov Bancario = " + _ContabPorLoteMovBancario.ToString() +
               "\nCuenta Mov Bancario Gasto = " + _CuentaMovBancarioGasto +
               "\nCuenta Mov Bancario Bancos Haber = " + _CuentaMovBancarioBancosHaber +
               "\nCuenta Mov Bancario Bancos Debe = " + _CuentaMovBancarioBancosDebe +
               "\nCuenta Mov Bancario Ingresos = " + _CuentaMovBancarioIngresos +
               "\nCuenta Debito Bancario Gasto = " + _CuentaDebitoBancarioGasto +
               "\nCuenta Debito Bancario Bancos = " + _CuentaDebitoBancarioBancos +
               "\nCuenta Credito Bancario Gasto = " + _CuentaCreditoBancarioGasto +
               "\nCuenta Credito Bancario Bancos = " + _CuentaCreditoBancarioBancos +
               "\nTipo Contabilizacion Anticipo = " + _TipoContabilizacionAnticipo.ToString() +
               "\nContab Individual Anticipo = " + _ContabIndividualAnticipo.ToString() +
               "\nContab Por Lote Anticipo = " + _ContabPorLoteAnticipo.ToString() +
               "\nCuenta Anticipo Caja = " + _CuentaAnticipoCaja +
               "\nCuenta Anticipo Cobrado = " + _CuentaAnticipoCobrado +
               "\nCuenta Anticipo Otros Ingresos = " + _CuentaAnticipoOtrosIngresos +
               "\nCuenta Anticipo Pagado = " + _CuentaAnticipoPagado +
               "\nCuenta Anticipo Banco = " + _CuentaAnticipoBanco +
               "\nCuenta Anticipo Otros Egresos = " + _CuentaAnticipoOtrosEgresos +
               "\nFactura Tipo Comprobante = " + _FacturaTipoComprobante +
               "\nCx CTipo Comprobante = " + _CxCTipoComprobante +
               "\nCx PTipo Comprobante = " + _CxPTipoComprobante +
               "\nCobranza Tipo Comprobante = " + _CobranzaTipoComprobante +
               "\nPago Tipo Comprobante = " + _PagoTipoComprobante +
               "\nMovimiento Bancario Tipo Comprobante = " + _MovimientoBancarioTipoComprobante +
               "\nAnticipo Tipo Comprobante = " + _AnticipoTipoComprobante +
               "\nCuenta Costo De Venta = " + _CuentaCostoDeVenta +
               "\nCuenta Inventario = " + _CuentaInventario +
               "\nTipo Contabilizacion Inventario = " + _TipoContabilizacionInventario.ToString() +
               "\nAgrupar Por Cuenta De Articulo Inven = " + _AgruparPorCuentaDeArticuloInven +
               "\nInventario Tipo Comprobante = " + _InventarioTipoComprobante +
               "\nCta De Pagos Sueldos = " + _CtaDePagosSueldos +
               "\nCta De Pagos Sueldos Banco = " + _CtaDePagosSueldosBanco +
               "\nContab Individual Pagos Sueldos = " + _ContabIndividualPagosSueldos.ToString() +
               "\nPagos Sueldos Tipo Comprobante = " + _PagosSueldosTipoComprobante +
               "\nTipo Contabilizacion De Pagos Sueldos = " + _TipoContabilizacionDePagosSueldos.ToString() +
               "\nEditar Comprobante De Pagos Sueldos = " + _EditarComprobanteDePagosSueldos +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            Numero = "";
            EditarComprobanteAfterInsert = false;
            DiferenciaEnCambioyCalculo = "";
            CuentaIva1Credito = "";
            CuentaIva1Debito = "";
            DondeContabilizarRetIva = eDondeEfectuoContabilizacionRetIVA.NoContabilizada;
            CuentaRetencionIva = "";
            TipoContabilizacionCxC = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualCxc = eContabilizacionIndividual.Inmediata;
            ContabPorLoteCxC = eContabilizacionPorLote.Diaria;
            CuentaCxCClientes = "";
            CuentaCxCIngresos = "";
            TipoContabilizacionCxP = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualCxP = eContabilizacionIndividual.Inmediata;
            ContabPorLoteCxP = eContabilizacionPorLote.Diaria;
            CuentaCxPGasto = "";
            CuentaCxPProveedores = "";
            TipoContabilizacionCobranza = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualCobranza = eContabilizacionIndividual.Inmediata;
            ContabPorLoteCobranza = eContabilizacionPorLote.Diaria;
            CuentaCobranzaCobradoEnEfectivo = "";
            CuentaCobranzaCobradoEnCheque = "";
            CuentaCobranzaCobradoEnTarjeta = "";
            cuentaCobranzaRetencionISLR = "";
            cuentaCobranzaRetencionIVA = "";
            CuentaCobranzaOtros = "";
            CuentaCobranzaCxCClientes = "";
            CuentaCobranzaCobradoAnticipo = "";
            TipoContabilizacionPagos = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualPagos = eContabilizacionIndividual.Inmediata;
            ContabPorLotePagos = eContabilizacionPorLote.Diaria;
            CuentaPagosCxPProveedores = "";
            CuentaPagosRetencionISLR = "";
            CuentaPagosOtros = "";
            CuentaPagosBanco = "";
            CuentaPagosPagadoAnticipo = "";
            TipoContabilizacionFacturacion = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualFacturacion = eContabilizacionIndividual.Inmediata;
            ContabPorLoteFacturacion = eContabilizacionPorLote.Diaria;
            CuentaFacturacionCxCClientes = "";
            CuentaFacturacionMontoTotalFactura = "";
            CuentaFacturacionCargos = "";
            CuentaFacturacionDescuentos = "";
            ContabilizarPorArticulo = false;
            AgruparPorCuentaDeArticulo = false;
            AgruparPorCargosDescuentos = false;
            TipoContabilizacionRDVtas = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualRDVtas = eContabilizacionIndividual.Inmediata;
            ContabPorLoteRDVtas = eContabilizacionPorLote.Diaria;
            CuentaRDVtasCaja = "";
            CuentaRDVtasMontoTotal = "";
            ContabilizarPorArticuloRDVtas = false;
            AgruparPorCuentaDeArticuloRDVtas = false;
            TipoContabilizacionMovBancario = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualMovBancario = eContabilizacionIndividual.Inmediata;
            ContabPorLoteMovBancario = eContabilizacionPorLote.Diaria;
            CuentaMovBancarioGasto = "";
            CuentaMovBancarioBancosHaber = "";
            CuentaMovBancarioBancosDebe = "";
            CuentaMovBancarioIngresos = "";
            CuentaDebitoBancarioGasto = "";
            CuentaDebitoBancarioBancos = "";
            CuentaCreditoBancarioGasto = "";
            CuentaCreditoBancarioBancos = "";
            TipoContabilizacionAnticipo = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualAnticipo = eContabilizacionIndividual.Inmediata;
            ContabPorLoteAnticipo = eContabilizacionPorLote.Diaria;
            CuentaAnticipoCaja = "";
            CuentaAnticipoCobrado = "";
            CuentaAnticipoOtrosIngresos = "";
            CuentaAnticipoPagado = "";
            CuentaAnticipoBanco = "";
            CuentaAnticipoOtrosEgresos = "";
            FacturaTipoComprobante = "";
            CxCTipoComprobante = "";
            CxPTipoComprobante = "";
            CobranzaTipoComprobante = "";
            PagoTipoComprobante = "";
            MovimientoBancarioTipoComprobante = "";
            AnticipoTipoComprobante = "";
            CuentaCostoDeVenta = "";
            CuentaInventario = "";
            TipoContabilizacionInventario = eTipoDeContabilizacion.CadaDocumento;
            AgruparPorCuentaDeArticuloInven = false;
            InventarioTipoComprobante = "";
            CtaDePagosSueldos = "";
            CtaDePagosSueldosBanco = "";
            ContabIndividualPagosSueldos = eContabilizacionIndividual.Inmediata;
            PagosSueldosTipoComprobante = "";
            TipoContabilizacionDePagosSueldos = eTipoDeContabilizacion.CadaDocumento;
            EditarComprobanteDePagosSueldos = false;
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public void Fill(XmlDocument refResulset, bool valSetCurrent) {
            _datos = refResulset;
            LibXmlDataParse insParser = new LibXmlDataParse(refResulset);
            if (valSetCurrent && insParser.Count() > 0) {
                Clear();
                ConsecutivoCompania = insParser.GetInt(0, "ConsecutivoCompania", ConsecutivoCompania);
                Numero = insParser.GetString(0, "Numero", Numero);
                EditarComprobanteAfterInsert = insParser.GetBool(0, "EditarComprobanteAfterInsert", EditarComprobanteAfterInsert);
                DiferenciaEnCambioyCalculo = insParser.GetString(0, "DiferenciaEnCambioyCalculo", DiferenciaEnCambioyCalculo);
                CuentaIva1Credito = insParser.GetString(0, "CuentaIva1Credito", CuentaIva1Credito);
                CuentaIva1Debito = insParser.GetString(0, "CuentaIva1Debito", CuentaIva1Debito);
                DondeContabilizarRetIva = (eDondeEfectuoContabilizacionRetIVA) insParser.GetEnum(0, "DondeContabilizarRetIva", (int) DondeContabilizarRetIva);
                CuentaRetencionIva = insParser.GetString(0, "CuentaRetencionIva", CuentaRetencionIva);
                TipoContabilizacionCxC = (eTipoDeContabilizacion) insParser.GetEnum(0, "TipoContabilizacionCxC", (int) TipoContabilizacionCxC);
                ContabIndividualCxc = (eContabilizacionIndividual) insParser.GetEnum(0, "ContabIndividualCxc", (int) ContabIndividualCxc);
                ContabPorLoteCxC = (eContabilizacionPorLote) insParser.GetEnum(0, "ContabPorLoteCxC", (int) ContabPorLoteCxC);
                CuentaCxCClientes = insParser.GetString(0, "CuentaCxCClientes", CuentaCxCClientes);
                CuentaCxCIngresos = insParser.GetString(0, "CuentaCxCIngresos", CuentaCxCIngresos);
                TipoContabilizacionCxP = (eTipoDeContabilizacion) insParser.GetEnum(0, "TipoContabilizacionCxP", (int) TipoContabilizacionCxP);
                ContabIndividualCxP = (eContabilizacionIndividual) insParser.GetEnum(0, "ContabIndividualCxP", (int) ContabIndividualCxP);
                ContabPorLoteCxP = (eContabilizacionPorLote) insParser.GetEnum(0, "ContabPorLoteCxP", (int) ContabPorLoteCxP);
                CuentaCxPGasto = insParser.GetString(0, "CuentaCxPGasto", CuentaCxPGasto);
                CuentaCxPProveedores = insParser.GetString(0, "CuentaCxPProveedores", CuentaCxPProveedores);
                TipoContabilizacionCobranza = (eTipoDeContabilizacion) insParser.GetEnum(0, "TipoContabilizacionCobranza", (int) TipoContabilizacionCobranza);
                ContabIndividualCobranza = (eContabilizacionIndividual) insParser.GetEnum(0, "ContabIndividualCobranza", (int) ContabIndividualCobranza);
                ContabPorLoteCobranza = (eContabilizacionPorLote) insParser.GetEnum(0, "ContabPorLoteCobranza", (int) ContabPorLoteCobranza);
                CuentaCobranzaCobradoEnEfectivo = insParser.GetString(0, "CuentaCobranzaCobradoEnEfectivo", CuentaCobranzaCobradoEnEfectivo);
                CuentaCobranzaCobradoEnCheque = insParser.GetString(0, "CuentaCobranzaCobradoEnCheque", CuentaCobranzaCobradoEnCheque);
                CuentaCobranzaCobradoEnTarjeta = insParser.GetString(0, "CuentaCobranzaCobradoEnTarjeta", CuentaCobranzaCobradoEnTarjeta);
                cuentaCobranzaRetencionISLR = insParser.GetString(0, "cuentaCobranzaRetencionISLR", cuentaCobranzaRetencionISLR);
                cuentaCobranzaRetencionIVA = insParser.GetString(0, "cuentaCobranzaRetencionIVA", cuentaCobranzaRetencionIVA);
                CuentaCobranzaOtros = insParser.GetString(0, "CuentaCobranzaOtros", CuentaCobranzaOtros);
                CuentaCobranzaCxCClientes = insParser.GetString(0, "CuentaCobranzaCxCClientes", CuentaCobranzaCxCClientes);
                CuentaCobranzaCobradoAnticipo = insParser.GetString(0, "CuentaCobranzaCobradoAnticipo", CuentaCobranzaCobradoAnticipo);
                TipoContabilizacionPagos = (eTipoDeContabilizacion) insParser.GetEnum(0, "TipoContabilizacionPagos", (int) TipoContabilizacionPagos);
                ContabIndividualPagos = (eContabilizacionIndividual) insParser.GetEnum(0, "ContabIndividualPagos", (int) ContabIndividualPagos);
                ContabPorLotePagos = (eContabilizacionPorLote) insParser.GetEnum(0, "ContabPorLotePagos", (int) ContabPorLotePagos);
                CuentaPagosCxPProveedores = insParser.GetString(0, "CuentaPagosCxPProveedores", CuentaPagosCxPProveedores);
                CuentaPagosRetencionISLR = insParser.GetString(0, "CuentaPagosRetencionISLR", CuentaPagosRetencionISLR);
                CuentaPagosOtros = insParser.GetString(0, "CuentaPagosOtros", CuentaPagosOtros);
                CuentaPagosBanco = insParser.GetString(0, "CuentaPagosBanco", CuentaPagosBanco);
                CuentaPagosPagadoAnticipo = insParser.GetString(0, "CuentaPagosPagadoAnticipo", CuentaPagosPagadoAnticipo);
                TipoContabilizacionFacturacion = (eTipoDeContabilizacion) insParser.GetEnum(0, "TipoContabilizacionFacturacion", (int) TipoContabilizacionFacturacion);
                ContabIndividualFacturacion = (eContabilizacionIndividual) insParser.GetEnum(0, "ContabIndividualFacturacion", (int) ContabIndividualFacturacion);
                ContabPorLoteFacturacion = (eContabilizacionPorLote) insParser.GetEnum(0, "ContabPorLoteFacturacion", (int) ContabPorLoteFacturacion);
                CuentaFacturacionCxCClientes = insParser.GetString(0, "CuentaFacturacionCxCClientes", CuentaFacturacionCxCClientes);
                CuentaFacturacionMontoTotalFactura = insParser.GetString(0, "CuentaFacturacionMontoTotalFactura", CuentaFacturacionMontoTotalFactura);
                CuentaFacturacionCargos = insParser.GetString(0, "CuentaFacturacionCargos", CuentaFacturacionCargos);
                CuentaFacturacionDescuentos = insParser.GetString(0, "CuentaFacturacionDescuentos", CuentaFacturacionDescuentos);
                ContabilizarPorArticulo = insParser.GetBool(0, "ContabilizarPorArticulo", ContabilizarPorArticulo);
                AgruparPorCuentaDeArticulo = insParser.GetBool(0, "AgruparPorCuentaDeArticulo", AgruparPorCuentaDeArticulo);
                AgruparPorCargosDescuentos = insParser.GetBool(0, "AgruparPorCargosDescuentos", AgruparPorCargosDescuentos);
                TipoContabilizacionRDVtas = (eTipoDeContabilizacion) insParser.GetEnum(0, "TipoContabilizacionRDVtas", (int) TipoContabilizacionRDVtas);
                ContabIndividualRDVtas = (eContabilizacionIndividual) insParser.GetEnum(0, "ContabIndividualRDVtas", (int) ContabIndividualRDVtas);
                ContabPorLoteRDVtas = (eContabilizacionPorLote) insParser.GetEnum(0, "ContabPorLoteRDVtas", (int) ContabPorLoteRDVtas);
                CuentaRDVtasCaja = insParser.GetString(0, "CuentaRDVtasCaja", CuentaRDVtasCaja);
                CuentaRDVtasMontoTotal = insParser.GetString(0, "CuentaRDVtasMontoTotal", CuentaRDVtasMontoTotal);
                ContabilizarPorArticuloRDVtas = insParser.GetBool(0, "ContabilizarPorArticuloRDVtas", ContabilizarPorArticuloRDVtas);
                AgruparPorCuentaDeArticuloRDVtas = insParser.GetBool(0, "AgruparPorCuentaDeArticuloRDVtas", AgruparPorCuentaDeArticuloRDVtas);
                TipoContabilizacionMovBancario = (eTipoDeContabilizacion) insParser.GetEnum(0, "TipoContabilizacionMovBancario", (int) TipoContabilizacionMovBancario);
                ContabIndividualMovBancario = (eContabilizacionIndividual) insParser.GetEnum(0, "ContabIndividualMovBancario", (int) ContabIndividualMovBancario);
                ContabPorLoteMovBancario = (eContabilizacionPorLote) insParser.GetEnum(0, "ContabPorLoteMovBancario", (int) ContabPorLoteMovBancario);
                CuentaMovBancarioGasto = insParser.GetString(0, "CuentaMovBancarioGasto", CuentaMovBancarioGasto);
                CuentaMovBancarioBancosHaber = insParser.GetString(0, "CuentaMovBancarioBancosHaber", CuentaMovBancarioBancosHaber);
                CuentaMovBancarioBancosDebe = insParser.GetString(0, "CuentaMovBancarioBancosDebe", CuentaMovBancarioBancosDebe);
                CuentaMovBancarioIngresos = insParser.GetString(0, "CuentaMovBancarioIngresos", CuentaMovBancarioIngresos);
                CuentaDebitoBancarioGasto = insParser.GetString(0, "CuentaDebitoBancarioGasto", CuentaDebitoBancarioGasto);
                CuentaDebitoBancarioBancos = insParser.GetString(0, "CuentaDebitoBancarioBancos", CuentaDebitoBancarioBancos);
                CuentaCreditoBancarioGasto = insParser.GetString(0, "CuentaCreditoBancarioGasto", CuentaCreditoBancarioGasto);
                CuentaCreditoBancarioBancos = insParser.GetString(0, "CuentaCreditoBancarioBancos", CuentaCreditoBancarioBancos);
                TipoContabilizacionAnticipo = (eTipoDeContabilizacion) insParser.GetEnum(0, "TipoContabilizacionAnticipo", (int) TipoContabilizacionAnticipo);
                ContabIndividualAnticipo = (eContabilizacionIndividual) insParser.GetEnum(0, "ContabIndividualAnticipo", (int) ContabIndividualAnticipo);
                ContabPorLoteAnticipo = (eContabilizacionPorLote) insParser.GetEnum(0, "ContabPorLoteAnticipo", (int) ContabPorLoteAnticipo);
                CuentaAnticipoCaja = insParser.GetString(0, "CuentaAnticipoCaja", CuentaAnticipoCaja);
                CuentaAnticipoCobrado = insParser.GetString(0, "CuentaAnticipoCobrado", CuentaAnticipoCobrado);
                CuentaAnticipoOtrosIngresos = insParser.GetString(0, "CuentaAnticipoOtrosIngresos", CuentaAnticipoOtrosIngresos);
                CuentaAnticipoPagado = insParser.GetString(0, "CuentaAnticipoPagado", CuentaAnticipoPagado);
                CuentaAnticipoBanco = insParser.GetString(0, "CuentaAnticipoBanco", CuentaAnticipoBanco);
                CuentaAnticipoOtrosEgresos = insParser.GetString(0, "CuentaAnticipoOtrosEgresos", CuentaAnticipoOtrosEgresos);
                FacturaTipoComprobante = insParser.GetString(0, "FacturaTipoComprobante", FacturaTipoComprobante);
                CxCTipoComprobante = insParser.GetString(0, "CxCTipoComprobante", CxCTipoComprobante);
                CxPTipoComprobante = insParser.GetString(0, "CxPTipoComprobante", CxPTipoComprobante);
                CobranzaTipoComprobante = insParser.GetString(0, "CobranzaTipoComprobante", CobranzaTipoComprobante);
                PagoTipoComprobante = insParser.GetString(0, "PagoTipoComprobante", PagoTipoComprobante);
                MovimientoBancarioTipoComprobante = insParser.GetString(0, "MovimientoBancarioTipoComprobante", MovimientoBancarioTipoComprobante);
                AnticipoTipoComprobante = insParser.GetString(0, "AnticipoTipoComprobante", AnticipoTipoComprobante);
                CuentaCostoDeVenta = insParser.GetString(0, "CuentaCostoDeVenta", CuentaCostoDeVenta);
                CuentaInventario = insParser.GetString(0, "CuentaInventario", CuentaInventario);
                TipoContabilizacionInventario = (eTipoDeContabilizacion) insParser.GetEnum(0, "TipoContabilizacionInventario", (int) TipoContabilizacionInventario);
                AgruparPorCuentaDeArticuloInven = insParser.GetBool(0, "AgruparPorCuentaDeArticuloInven", AgruparPorCuentaDeArticuloInven);
                InventarioTipoComprobante = insParser.GetString(0, "InventarioTipoComprobante", InventarioTipoComprobante);
                CtaDePagosSueldos = insParser.GetString(0, "CtaDePagosSueldos", CtaDePagosSueldos);
                CtaDePagosSueldosBanco = insParser.GetString(0, "CtaDePagosSueldosBanco", CtaDePagosSueldosBanco);
                ContabIndividualPagosSueldos = (eContabilizacionIndividual) insParser.GetEnum(0, "ContabIndividualPagosSueldos", (int) ContabIndividualPagosSueldos);
                PagosSueldosTipoComprobante = insParser.GetString(0, "PagosSueldosTipoComprobante", PagosSueldosTipoComprobante);
                TipoContabilizacionDePagosSueldos = (eTipoDeContabilizacion) insParser.GetEnum(0, "TipoContabilizacionDePagosSueldos", (int) TipoContabilizacionDePagosSueldos);
                EditarComprobanteDePagosSueldos = insParser.GetBool(0, "EditarComprobanteDePagosSueldos", EditarComprobanteDePagosSueldos);
                NombreOperador = insParser.GetString(0, "NombreOperador", NombreOperador);
                FechaUltimaModificacion = insParser.GetDateTime(0, "FechaUltimaModificacion", FechaUltimaModificacion);
                fldTimeStamp = insParser.GetTimeStamp(0);
            }
        }
        #endregion //Metodos Generados


    } //End of class recReglasDeContabilizacion

} //End of namespace Galac.Saw.Dal.Contabilizacion

