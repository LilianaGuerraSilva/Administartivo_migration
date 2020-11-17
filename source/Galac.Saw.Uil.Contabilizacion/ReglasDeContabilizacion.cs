using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Security.Permissions;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.Contabilizacion;

namespace Galac.Saw.Uil.Contabilizacion {
    public class ReglasDeContabilizacion: LibMROMF {
        #region Variables
        ILibBusinessComponent _Reglas;
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
        #endregion //Propiedades
        #region Constructores

        public ReglasDeContabilizacion(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Parsing

        public void Parse(XmlReader valXml) {
            if (valXml != null) {
                XDocument xDoc = XDocument.Load(valXml);
                var vEntity = from vRecord in xDoc.Descendants("GpResult")
                              select vRecord;
                foreach (XElement vItem in vEntity) {
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null)) {
                        this.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Numero"), null)) {
                        this.Numero = vItem.Element("Numero").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("EditarComprobanteAfterInsert"), null)) {
                        this.EditarComprobanteAfterInsert = LibConvert.SNToBool(vItem.Element("EditarComprobanteAfterInsert"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("DiferenciaEnCambioyCalculo"), null)) {
                        this.DiferenciaEnCambioyCalculo = vItem.Element("DiferenciaEnCambioyCalculo").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaIva1Credito"), null)) {
                        this.CuentaIva1Credito = vItem.Element("CuentaIva1Credito").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaIva1Debito"), null)) {
                        this.CuentaIva1Debito = vItem.Element("CuentaIva1Debito").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("DondeContabilizarRetIva"), null)) {
                        this.DondeContabilizarRetIva = (eDondeEfectuoContabilizacionRetIVA)LibConvert.DbValueToEnum(vItem.Element("DondeContabilizarRetIva"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaRetencionIva"), null)) {
                        this.CuentaRetencionIva = vItem.Element("CuentaRetencionIva").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TipoContabilizacionCxC"), null)) {
                        this.TipoContabilizacionCxC = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(vItem.Element("TipoContabilizacionCxC"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabIndividualCxc"), null)) {
                        this.ContabIndividualCxc = (eContabilizacionIndividual)LibConvert.DbValueToEnum(vItem.Element("ContabIndividualCxc"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabPorLoteCxC"), null)) {
                        this.ContabPorLoteCxC = (eContabilizacionPorLote)LibConvert.DbValueToEnum(vItem.Element("ContabPorLoteCxC"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCxCClientes"), null)) {
                        this.CuentaCxCClientes = vItem.Element("CuentaCxCClientes").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCxCIngresos"), null)) {
                        this.CuentaCxCIngresos = vItem.Element("CuentaCxCIngresos").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TipoContabilizacionCxP"), null)) {
                        this.TipoContabilizacionCxP = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(vItem.Element("TipoContabilizacionCxP"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabIndividualCxP"), null)) {
                        this.ContabIndividualCxP = (eContabilizacionIndividual)LibConvert.DbValueToEnum(vItem.Element("ContabIndividualCxP"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabPorLoteCxP"), null)) {
                        this.ContabPorLoteCxP = (eContabilizacionPorLote)LibConvert.DbValueToEnum(vItem.Element("ContabPorLoteCxP"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCxPGasto"), null)) {
                        this.CuentaCxPGasto = vItem.Element("CuentaCxPGasto").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCxPProveedores"), null)) {
                        this.CuentaCxPProveedores = vItem.Element("CuentaCxPProveedores").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TipoContabilizacionCobranza"), null)) {
                        this.TipoContabilizacionCobranza = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(vItem.Element("TipoContabilizacionCobranza"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabIndividualCobranza"), null)) {
                        this.ContabIndividualCobranza = (eContabilizacionIndividual)LibConvert.DbValueToEnum(vItem.Element("ContabIndividualCobranza"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabPorLoteCobranza"), null)) {
                        this.ContabPorLoteCobranza = (eContabilizacionPorLote)LibConvert.DbValueToEnum(vItem.Element("ContabPorLoteCobranza"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCobranzaCobradoEnEfectivo"), null)) {
                        this.CuentaCobranzaCobradoEnEfectivo = vItem.Element("CuentaCobranzaCobradoEnEfectivo").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCobranzaCobradoEnCheque"), null)) {
                        this.CuentaCobranzaCobradoEnCheque = vItem.Element("CuentaCobranzaCobradoEnCheque").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCobranzaCobradoEnTarjeta"), null)) {
                        this.CuentaCobranzaCobradoEnTarjeta = vItem.Element("CuentaCobranzaCobradoEnTarjeta").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("cuentaCobranzaRetencionISLR"), null)) {
                        this.cuentaCobranzaRetencionISLR = vItem.Element("cuentaCobranzaRetencionISLR").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("cuentaCobranzaRetencionIVA"), null)) {
                        this.cuentaCobranzaRetencionIVA = vItem.Element("cuentaCobranzaRetencionIVA").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCobranzaOtros"), null)) {
                        this.CuentaCobranzaOtros = vItem.Element("CuentaCobranzaOtros").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCobranzaCxCClientes"), null)) {
                        this.CuentaCobranzaCxCClientes = vItem.Element("CuentaCobranzaCxCClientes").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCobranzaCobradoAnticipo"), null)) {
                        this.CuentaCobranzaCobradoAnticipo = vItem.Element("CuentaCobranzaCobradoAnticipo").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TipoContabilizacionPagos"), null)) {
                        this.TipoContabilizacionPagos = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(vItem.Element("TipoContabilizacionPagos"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabIndividualPagos"), null)) {
                        this.ContabIndividualPagos = (eContabilizacionIndividual)LibConvert.DbValueToEnum(vItem.Element("ContabIndividualPagos"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabPorLotePagos"), null)) {
                        this.ContabPorLotePagos = (eContabilizacionPorLote)LibConvert.DbValueToEnum(vItem.Element("ContabPorLotePagos"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaPagosCxPProveedores"), null)) {
                        this.CuentaPagosCxPProveedores = vItem.Element("CuentaPagosCxPProveedores").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaPagosRetencionISLR"), null)) {
                        this.CuentaPagosRetencionISLR = vItem.Element("CuentaPagosRetencionISLR").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaPagosOtros"), null)) {
                        this.CuentaPagosOtros = vItem.Element("CuentaPagosOtros").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaPagosBanco"), null)) {
                        this.CuentaPagosBanco = vItem.Element("CuentaPagosBanco").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaPagosPagadoAnticipo"), null)) {
                        this.CuentaPagosPagadoAnticipo = vItem.Element("CuentaPagosPagadoAnticipo").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TipoContabilizacionFacturacion"), null)) {
                        this.TipoContabilizacionFacturacion = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(vItem.Element("TipoContabilizacionFacturacion"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabIndividualFacturacion"), null)) {
                        this.ContabIndividualFacturacion = (eContabilizacionIndividual)LibConvert.DbValueToEnum(vItem.Element("ContabIndividualFacturacion"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabPorLoteFacturacion"), null)) {
                        this.ContabPorLoteFacturacion = (eContabilizacionPorLote)LibConvert.DbValueToEnum(vItem.Element("ContabPorLoteFacturacion"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaFacturacionCxCClientes"), null)) {
                        this.CuentaFacturacionCxCClientes = vItem.Element("CuentaFacturacionCxCClientes").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaFacturacionMontoTotalFactura"), null)) {
                        this.CuentaFacturacionMontoTotalFactura = vItem.Element("CuentaFacturacionMontoTotalFactura").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaFacturacionCargos"), null)) {
                        this.CuentaFacturacionCargos = vItem.Element("CuentaFacturacionCargos").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaFacturacionDescuentos"), null)) {
                        this.CuentaFacturacionDescuentos = vItem.Element("CuentaFacturacionDescuentos").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabilizarPorArticulo"), null)) {
                        this.ContabilizarPorArticulo = LibConvert.SNToBool(vItem.Element("ContabilizarPorArticulo"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("AgruparPorCuentaDeArticulo"), null)) {
                        this.AgruparPorCuentaDeArticulo = LibConvert.SNToBool(vItem.Element("AgruparPorCuentaDeArticulo"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("AgruparPorCargosDescuentos"), null)) {
                        this.AgruparPorCargosDescuentos = LibConvert.SNToBool(vItem.Element("AgruparPorCargosDescuentos"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TipoContabilizacionRDVtas"), null)) {
                        this.TipoContabilizacionRDVtas = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(vItem.Element("TipoContabilizacionRDVtas"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabIndividualRDVtas"), null)) {
                        this.ContabIndividualRDVtas = (eContabilizacionIndividual)LibConvert.DbValueToEnum(vItem.Element("ContabIndividualRDVtas"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabPorLoteRDVtas"), null)) {
                        this.ContabPorLoteRDVtas = (eContabilizacionPorLote)LibConvert.DbValueToEnum(vItem.Element("ContabPorLoteRDVtas"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaRDVtasCaja"), null)) {
                        this.CuentaRDVtasCaja = vItem.Element("CuentaRDVtasCaja").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaRDVtasMontoTotal"), null)) {
                        this.CuentaRDVtasMontoTotal = vItem.Element("CuentaRDVtasMontoTotal").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabilizarPorArticuloRDVtas"), null)) {
                        this.ContabilizarPorArticuloRDVtas = LibConvert.SNToBool(vItem.Element("ContabilizarPorArticuloRDVtas"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("AgruparPorCuentaDeArticuloRDVtas"), null)) {
                        this.AgruparPorCuentaDeArticuloRDVtas = LibConvert.SNToBool(vItem.Element("AgruparPorCuentaDeArticuloRDVtas"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TipoContabilizacionMovBancario"), null)) {
                        this.TipoContabilizacionMovBancario = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(vItem.Element("TipoContabilizacionMovBancario"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabIndividualMovBancario"), null)) {
                        this.ContabIndividualMovBancario = (eContabilizacionIndividual)LibConvert.DbValueToEnum(vItem.Element("ContabIndividualMovBancario"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabPorLoteMovBancario"), null)) {
                        this.ContabPorLoteMovBancario = (eContabilizacionPorLote)LibConvert.DbValueToEnum(vItem.Element("ContabPorLoteMovBancario"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaMovBancarioGasto"), null)) {
                        this.CuentaMovBancarioGasto = vItem.Element("CuentaMovBancarioGasto").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaMovBancarioBancosHaber"), null)) {
                        this.CuentaMovBancarioBancosHaber = vItem.Element("CuentaMovBancarioBancosHaber").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaMovBancarioBancosDebe"), null)) {
                        this.CuentaMovBancarioBancosDebe = vItem.Element("CuentaMovBancarioBancosDebe").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaMovBancarioIngresos"), null)) {
                        this.CuentaMovBancarioIngresos = vItem.Element("CuentaMovBancarioIngresos").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaDebitoBancarioGasto"), null)) {
                        this.CuentaDebitoBancarioGasto = vItem.Element("CuentaDebitoBancarioGasto").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaDebitoBancarioBancos"), null)) {
                        this.CuentaDebitoBancarioBancos = vItem.Element("CuentaDebitoBancarioBancos").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCreditoBancarioGasto"), null)) {
                        this.CuentaCreditoBancarioGasto = vItem.Element("CuentaCreditoBancarioGasto").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCreditoBancarioBancos"), null)) {
                        this.CuentaCreditoBancarioBancos = vItem.Element("CuentaCreditoBancarioBancos").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TipoContabilizacionAnticipo"), null)) {
                        this.TipoContabilizacionAnticipo = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(vItem.Element("TipoContabilizacionAnticipo"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabIndividualAnticipo"), null)) {
                        this.ContabIndividualAnticipo = (eContabilizacionIndividual)LibConvert.DbValueToEnum(vItem.Element("ContabIndividualAnticipo"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabPorLoteAnticipo"), null)) {
                        this.ContabPorLoteAnticipo = (eContabilizacionPorLote)LibConvert.DbValueToEnum(vItem.Element("ContabPorLoteAnticipo"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaAnticipoCaja"), null)) {
                        this.CuentaAnticipoCaja = vItem.Element("CuentaAnticipoCaja").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaAnticipoCobrado"), null)) {
                        this.CuentaAnticipoCobrado = vItem.Element("CuentaAnticipoCobrado").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaAnticipoOtrosIngresos"), null)) {
                        this.CuentaAnticipoOtrosIngresos = vItem.Element("CuentaAnticipoOtrosIngresos").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaAnticipoPagado"), null)) {
                        this.CuentaAnticipoPagado = vItem.Element("CuentaAnticipoPagado").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaAnticipoBanco"), null)) {
                        this.CuentaAnticipoBanco = vItem.Element("CuentaAnticipoBanco").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaAnticipoOtrosEgresos"), null)) {
                        this.CuentaAnticipoOtrosEgresos = vItem.Element("CuentaAnticipoOtrosEgresos").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("FacturaTipoComprobante"), null)) {
                        this.FacturaTipoComprobante = vItem.Element("FacturaTipoComprobante").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CxCTipoComprobante"), null)) {
                        this.CxCTipoComprobante = vItem.Element("CxCTipoComprobante").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CxPTipoComprobante"), null)) {
                        this.CxPTipoComprobante = vItem.Element("CxPTipoComprobante").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CobranzaTipoComprobante"), null)) {
                        this.CobranzaTipoComprobante = vItem.Element("CobranzaTipoComprobante").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("PagoTipoComprobante"), null)) {
                        this.PagoTipoComprobante = vItem.Element("PagoTipoComprobante").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("MovimientoBancarioTipoComprobante"), null)) {
                        this.MovimientoBancarioTipoComprobante = vItem.Element("MovimientoBancarioTipoComprobante").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("AnticipoTipoComprobante"), null)) {
                        this.AnticipoTipoComprobante = vItem.Element("AnticipoTipoComprobante").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaCostoDeVenta"), null)) {
                        this.CuentaCostoDeVenta = vItem.Element("CuentaCostoDeVenta").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaInventario"), null)) {
                        this.CuentaInventario = vItem.Element("CuentaInventario").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TipoContabilizacionInventario"), null)) {
                        this.TipoContabilizacionInventario = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(vItem.Element("TipoContabilizacionInventario"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("AgruparPorCuentaDeArticuloInven"), null)) {
                        this.AgruparPorCuentaDeArticuloInven = LibConvert.SNToBool(vItem.Element("AgruparPorCuentaDeArticuloInven"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("InventarioTipoComprobante"), null)) {
                        this.InventarioTipoComprobante = vItem.Element("InventarioTipoComprobante").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CtaDePagosSueldos"), null)) {
                        this.CtaDePagosSueldos = vItem.Element("CtaDePagosSueldos").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CtaDePagosSueldosBanco"), null)) {
                        this.CtaDePagosSueldosBanco = vItem.Element("CtaDePagosSueldosBanco").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ContabIndividualPagosSueldos"), null)) {
                        this.ContabIndividualPagosSueldos = (eContabilizacionIndividual)LibConvert.DbValueToEnum(vItem.Element("ContabIndividualPagosSueldos"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("PagosSueldosTipoComprobante"), null)) {
                        this.PagosSueldosTipoComprobante = vItem.Element("PagosSueldosTipoComprobante").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TipoContabilizacionDePagosSueldos"), null)) {
                        this.TipoContabilizacionDePagosSueldos = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(vItem.Element("TipoContabilizacionDePagosSueldos"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("EditarComprobanteDePagosSueldos"), null)) {
                        this.EditarComprobanteDePagosSueldos = LibConvert.SNToBool(vItem.Element("EditarComprobanteDePagosSueldos"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null)) {
                        this.NombreOperador = vItem.Element("NombreOperador").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null)) {
                        this.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
                    }
                    this.fldTimeStamp = LibConvert.ToLong(vItem.Element("fldTimeStampBigint").Value);
                }
            }
        }

        XmlReader ParseToXml(ReglasDeContabilizacion valEntidad) {
            List<ReglasDeContabilizacion> vListEntidades = new List<ReglasDeContabilizacion>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", Mfc.GetInt("Compania")),
                    new XElement("Numero", vEntity.Numero),
                    new XElement("EditarComprobanteAfterInsert", LibConvert.BoolToSN(vEntity.EditarComprobanteAfterInsert)),
                    new XElement("DiferenciaEnCambioyCalculo", vEntity.DiferenciaEnCambioyCalculo),
                    new XElement("CuentaIva1Credito", vEntity.CuentaIva1Credito),
                    new XElement("CuentaIva1Debito", vEntity.CuentaIva1Debito),
                    new XElement("DondeContabilizarRetIva", vEntity.DondeContabilizarRetIvaAsDB),
                    new XElement("CuentaRetencionIva", vEntity.CuentaRetencionIva),
                    new XElement("TipoContabilizacionCxC", vEntity.TipoContabilizacionCxCAsDB),
                    new XElement("ContabIndividualCxc", vEntity.ContabIndividualCxcAsDB),
                    new XElement("ContabPorLoteCxC", vEntity.ContabPorLoteCxCAsDB),
                    new XElement("CuentaCxCClientes", vEntity.CuentaCxCClientes),
                    new XElement("CuentaCxCIngresos", vEntity.CuentaCxCIngresos),
                    new XElement("TipoContabilizacionCxP", vEntity.TipoContabilizacionCxPAsDB),
                    new XElement("ContabIndividualCxP", vEntity.ContabIndividualCxPAsDB),
                    new XElement("ContabPorLoteCxP", vEntity.ContabPorLoteCxPAsDB),
                    new XElement("CuentaCxPGasto", vEntity.CuentaCxPGasto),
                    new XElement("CuentaCxPProveedores", vEntity.CuentaCxPProveedores),
                    new XElement("TipoContabilizacionCobranza", vEntity.TipoContabilizacionCobranzaAsDB),
                    new XElement("ContabIndividualCobranza", vEntity.ContabIndividualCobranzaAsDB),
                    new XElement("ContabPorLoteCobranza", vEntity.ContabPorLoteCobranzaAsDB),
                    new XElement("CuentaCobranzaCobradoEnEfectivo", vEntity.CuentaCobranzaCobradoEnEfectivo),
                    new XElement("CuentaCobranzaCobradoEnCheque", vEntity.CuentaCobranzaCobradoEnCheque),
                    new XElement("CuentaCobranzaCobradoEnTarjeta", vEntity.CuentaCobranzaCobradoEnTarjeta),
                    new XElement("cuentaCobranzaRetencionISLR", vEntity.cuentaCobranzaRetencionISLR),
                    new XElement("cuentaCobranzaRetencionIVA", vEntity.cuentaCobranzaRetencionIVA),
                    new XElement("CuentaCobranzaOtros", vEntity.CuentaCobranzaOtros),
                    new XElement("CuentaCobranzaCxCClientes", vEntity.CuentaCobranzaCxCClientes),
                    new XElement("CuentaCobranzaCobradoAnticipo", vEntity.CuentaCobranzaCobradoAnticipo),
                    new XElement("TipoContabilizacionPagos", vEntity.TipoContabilizacionPagosAsDB),
                    new XElement("ContabIndividualPagos", vEntity.ContabIndividualPagosAsDB),
                    new XElement("ContabPorLotePagos", vEntity.ContabPorLotePagosAsDB),
                    new XElement("CuentaPagosCxPProveedores", vEntity.CuentaPagosCxPProveedores),
                    new XElement("CuentaPagosRetencionISLR", vEntity.CuentaPagosRetencionISLR),
                    new XElement("CuentaPagosOtros", vEntity.CuentaPagosOtros),
                    new XElement("CuentaPagosBanco", vEntity.CuentaPagosBanco),
                    new XElement("CuentaPagosPagadoAnticipo", vEntity.CuentaPagosPagadoAnticipo),
                    new XElement("TipoContabilizacionFacturacion", vEntity.TipoContabilizacionFacturacionAsDB),
                    new XElement("ContabIndividualFacturacion", vEntity.ContabIndividualFacturacionAsDB),
                    new XElement("ContabPorLoteFacturacion", vEntity.ContabPorLoteFacturacionAsDB),
                    new XElement("CuentaFacturacionCxCClientes", vEntity.CuentaFacturacionCxCClientes),
                    new XElement("CuentaFacturacionMontoTotalFactura", vEntity.CuentaFacturacionMontoTotalFactura),
                    new XElement("CuentaFacturacionCargos", vEntity.CuentaFacturacionCargos),
                    new XElement("CuentaFacturacionDescuentos", vEntity.CuentaFacturacionDescuentos),
                    new XElement("ContabilizarPorArticulo", LibConvert.BoolToSN(vEntity.ContabilizarPorArticulo)),
                    new XElement("AgruparPorCuentaDeArticulo", LibConvert.BoolToSN(vEntity.AgruparPorCuentaDeArticulo)),
                    new XElement("AgruparPorCargosDescuentos", LibConvert.BoolToSN(vEntity.AgruparPorCargosDescuentos)),
                    new XElement("TipoContabilizacionRDVtas", vEntity.TipoContabilizacionRDVtasAsDB),
                    new XElement("ContabIndividualRDVtas", vEntity.ContabIndividualRDVtasAsDB),
                    new XElement("ContabPorLoteRDVtas", vEntity.ContabPorLoteRDVtasAsDB),
                    new XElement("CuentaRDVtasCaja", vEntity.CuentaRDVtasCaja),
                    new XElement("CuentaRDVtasMontoTotal", vEntity.CuentaRDVtasMontoTotal),
                    new XElement("ContabilizarPorArticuloRDVtas", LibConvert.BoolToSN(vEntity.ContabilizarPorArticuloRDVtas)),
                    new XElement("AgruparPorCuentaDeArticuloRDVtas", LibConvert.BoolToSN(vEntity.AgruparPorCuentaDeArticuloRDVtas)),
                    new XElement("TipoContabilizacionMovBancario", vEntity.TipoContabilizacionMovBancarioAsDB),
                    new XElement("ContabIndividualMovBancario", vEntity.ContabIndividualMovBancarioAsDB),
                    new XElement("ContabPorLoteMovBancario", vEntity.ContabPorLoteMovBancarioAsDB),
                    new XElement("CuentaMovBancarioGasto", vEntity.CuentaMovBancarioGasto),
                    new XElement("CuentaMovBancarioBancosHaber", vEntity.CuentaMovBancarioBancosHaber),
                    new XElement("CuentaMovBancarioBancosDebe", vEntity.CuentaMovBancarioBancosDebe),
                    new XElement("CuentaMovBancarioIngresos", vEntity.CuentaMovBancarioIngresos),
                    new XElement("CuentaDebitoBancarioGasto", vEntity.CuentaDebitoBancarioGasto),
                    new XElement("CuentaDebitoBancarioBancos", vEntity.CuentaDebitoBancarioBancos),
                    new XElement("CuentaCreditoBancarioGasto", vEntity.CuentaCreditoBancarioGasto),
                    new XElement("CuentaCreditoBancarioBancos", vEntity.CuentaCreditoBancarioBancos),
                    new XElement("TipoContabilizacionAnticipo", vEntity.TipoContabilizacionAnticipoAsDB),
                    new XElement("ContabIndividualAnticipo", vEntity.ContabIndividualAnticipoAsDB),
                    new XElement("ContabPorLoteAnticipo", vEntity.ContabPorLoteAnticipoAsDB),
                    new XElement("CuentaAnticipoCaja", vEntity.CuentaAnticipoCaja),
                    new XElement("CuentaAnticipoCobrado", vEntity.CuentaAnticipoCobrado),
                    new XElement("CuentaAnticipoOtrosIngresos", vEntity.CuentaAnticipoOtrosIngresos),
                    new XElement("CuentaAnticipoPagado", vEntity.CuentaAnticipoPagado),
                    new XElement("CuentaAnticipoBanco", vEntity.CuentaAnticipoBanco),
                    new XElement("CuentaAnticipoOtrosEgresos", vEntity.CuentaAnticipoOtrosEgresos),
                    new XElement("FacturaTipoComprobante", vEntity.FacturaTipoComprobante),
                    new XElement("CxCTipoComprobante", vEntity.CxCTipoComprobante),
                    new XElement("CxPTipoComprobante", vEntity.CxPTipoComprobante),
                    new XElement("CobranzaTipoComprobante", vEntity.CobranzaTipoComprobante),
                    new XElement("PagoTipoComprobante", vEntity.PagoTipoComprobante),
                    new XElement("MovimientoBancarioTipoComprobante", vEntity.MovimientoBancarioTipoComprobante),
                    new XElement("AnticipoTipoComprobante", vEntity.AnticipoTipoComprobante),
                    new XElement("CuentaCostoDeVenta", vEntity.CuentaCostoDeVenta),
                    new XElement("CuentaInventario", vEntity.CuentaInventario),
                    new XElement("TipoContabilizacionInventario", vEntity.TipoContabilizacionInventarioAsDB),
                    new XElement("AgruparPorCuentaDeArticuloInven", LibConvert.BoolToSN(vEntity.AgruparPorCuentaDeArticuloInven)),
                    new XElement("InventarioTipoComprobante", vEntity.InventarioTipoComprobante),
                    new XElement("CtaDePagosSueldos", vEntity.CtaDePagosSueldos),
                    new XElement("CtaDePagosSueldosBanco", vEntity.CtaDePagosSueldosBanco),
                    new XElement("ContabIndividualPagosSueldos", vEntity.ContabIndividualPagosSueldosAsDB),
                    new XElement("PagosSueldosTipoComprobante", vEntity.PagosSueldosTipoComprobante),
                    new XElement("TipoContabilizacionDePagosSueldos", vEntity.TipoContabilizacionDePagosSueldosAsDB),
                    new XElement("EditarComprobanteDePagosSueldos", LibConvert.BoolToSN(vEntity.EditarComprobanteDePagosSueldos)),
                    new XElement("fldTimeStamp", vEntity.fldTimeStamp)));
            XmlReader xmlReader = vXElement.CreateReader();
            return xmlReader;
        }
        #endregion //Parsing
        internal void Clear() {
            ConsecutivoCompania = Mfc.GetInt("Compania");
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

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponent)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Contabilizacion.clsReglasDeContabilizacionNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        [PrincipalPermission(SecurityAction.Demand, Role = "Reglas de Contabilización.Insertar")]
        internal bool InsertRecord(out string outErrorMsg) {
            bool vResult = false;
            XmlReader vXmlRecord;
            LibResponse vResponse;
            if (ValidateAll(eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                vXmlRecord = ParseToXml(this);
                vResponse = _Reglas.DoAction(vXmlRecord, eAccionSR.Insertar, null);
                vResult = vResponse.Success;
                if (vResult) {
                    Parse(vResponse.Info);
                }
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Reglas de Contabilización.Modificar")]
        internal bool UpdateRecord(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(valAction, out outErrorMessage)) {
                RegistraCliente();
                XmlReader vXmlRecord = ParseToXml(this);
                vResult = _Reglas.DoAction(vXmlRecord, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Reglas de Contabilización.Eliminar")]
        internal bool DeleteRecord() {
            bool vResult = false;
            RegistraCliente();
            XmlReader vXmlRecord = ParseToXml(this);
            vResult = _Reglas.DoAction(vXmlRecord, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, string valNumero) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Numero", valNumero, 11);
            XmlReader vXmlDoc = _Reglas.GetData(eProcessMessageType.SpName, "ReglasDeContabilizacionGET", vParams.Get());
            Parse(vXmlDoc);
        }

        public string GenerarProximoNumero() {
            string vResult = "";
            RegistraCliente();
            XmlReader vData = _Reglas.GetData(eProcessMessageType.Message, "ProximoNumero", Mfc.GetIntAsParam("Compania"));
            LibXmlDataParse insParser = new LibXmlDataParse(vData);
            if (insParser.CanRead()) {
                vResult = insParser.GetString(0, "Numero", vResult);
            }
            insParser.Dispose();
            return vResult;
        }

        public bool ValidateAll(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidNumero(valAction, Numero, false);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidNumero(eAccionSR valAction, string valNumero, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            valNumero = LibString.Trim(valNumero);
            if (LibString.IsNullOrEmpty(valNumero, true)) {
                BuildValidationInfo(MsgRequiredField("Número"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados
      
        private  XmlReader BuscarReglasDeContabilizacionParaEscoger() {
            LibGpParams vParams = new LibGpParams();
            LibXmlDataParse insDataParse = new LibXmlDataParse((LibXmlMemInfo)AppMemoryInfo);
            RegistraCliente();
            vParams.AddInInteger("ConsecutivoCompania", Mfc.GetInt("Compania"));
            vParams.AddInString("Numero",  insDataParse.GetString("RecordName", 0, "Numero", "*"), 11);
            XmlReader vResulset = _Reglas.GetData(eProcessMessageType.SpName, "ReglasDeContabilizacionGET", vParams.Get());
            return vResulset;
        }
        public bool EscogeReglasDeContabilizacionActual() {
            bool vResult = false;
            XmlReader vReglasDeContabilizacion = BuscarReglasDeContabilizacionParaEscoger();
            LibXmlDataParse insParser = new LibXmlDataParse(vReglasDeContabilizacion);
            if (insParser.Count() > 0) {
                vResult = true;
                Parse(insParser.ToReader());
            }
            
            return vResult;
        }



    } //End of class ReglasDeContabilizacion

} //End of namespace Galac.Saw.Uil.Contabilizacion

