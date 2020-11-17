using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.Contabilizacion;

namespace Galac.Saw.Ccl.Contabilizacion {
    [Serializable]
    public class ReglasDeContabilizacion {
        #region Variables
        private int _ConsecutivoCompania;
        private string _Numero;
        private string _CuentaIva1Credito;
        private string _CuentaIva1Debito;
        private string _CuentaRetencionIva;
        private eDondeEfectuoContabilizacionRetIVA _DondeContabilizarRetIva;
        private string _DiferenciaEnCambioyCalculo;
        private string _CuentaDiferenciaCambiaria;
        private eTipoDeContabilizacion _TipoContabilizacionCxC;
        private eContabilizacionIndividual _ContabIndividualCxc;
        private eContabilizacionPorLote _ContabPorLoteCxC;
        private string _CuentaCxCClientes;
        private string _CuentaCxCIngresos;
        private string _CxCTipoComprobante;
        private bool _EditarComprobanteAfterInsertCxC;
        private eTipoDeContabilizacion _TipoContabilizacionCxP;
        private eContabilizacionIndividual _ContabIndividualCxP;
        private eContabilizacionPorLote _ContabPorLoteCxP;
        private string _CuentaCxPGasto;
        private string _CuentaCxPProveedores;
        private string _CuentaRetencionImpuestoMunicipal;
        private string _CxPTipoComprobante;
        private bool _EditarComprobanteAfterInsertCxP;
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
        private string _CuentaCobranzaIvaDiferido;
        private string _CobranzaTipoComprobante;
        private bool _EditarComprobanteAfterInsertCobranza;
        private bool _ManejarDiferenciaCambiariaEnCobranza;
        private eTipoDeContabilizacion _TipoContabilizacionPagos;
        private eContabilizacionIndividual _ContabIndividualPagos;
        private eContabilizacionPorLote _ContabPorLotePagos;
        private string _CuentaPagosCxPProveedores;
        private string _CuentaPagosRetencionISLR;
        private string _CuentaPagosOtros;
        private string _CuentaPagosBanco;
        private string _CuentaPagosPagadoAnticipo;
        private string _PagoTipoComprobante;
        private bool _EditarComprobanteAfterInsertPagos;
        private bool _ManejarDiferenciaCambiariaEnPagos;
        private eTipoDeContabilizacion _TipoContabilizacionFacturacion;
        private eContabilizacionIndividual _ContabIndividualFacturacion;
        private eContabilizacionPorLote _ContabPorLoteFacturacion;
        private string _CuentaFacturacionCxCClientes;
        private string _CuentaFacturacionMontoTotalFactura;
        private string _CuentaFacturacionCargos;
        private string _CuentaFacturacionDescuentos;
        private string _CuentaFacturacionIvaDiferido;
        private bool _ContabilizarPorArticulo;
        private bool _AgruparPorCuentaDeArticulo;
        private bool _AgruparPorCargosDescuentos;
        private string _FacturaTipoComprobante;
        private bool _EditarComprobanteAfterInsertFactura;
        private eTipoDeContabilizacion _TipoContabilizacionRDVtas;
        private eContabilizacionIndividual _ContabIndividualRDVtas;
        private eContabilizacionPorLote _ContabPorLoteRDVtas;
        private string _CuentaRDVtasCaja;
        private string _CuentaRDVtasMontoTotal;
        private bool _ContabilizarPorArticuloRDVtas;
        private bool _AgruparPorCuentaDeArticuloRDVtas;
        private bool _EditarComprobanteAfterInsertResDia;
        private eTipoDeContabilizacion _TipoContabilizacionMovBancario;
        private eContabilizacionIndividual _ContabIndividualMovBancario;
        private eContabilizacionPorLote _ContabPorLoteMovBancario;
        private string _CuentaMovBancarioGasto;
        private string _CuentaMovBancarioBancosHaber;
        private string _CuentaMovBancarioBancosDebe;
        private string _CuentaMovBancarioIngresos;
        private string _MovimientoBancarioTipoComprobante;
        private bool _EditarComprobanteAfterInsertMovBan;
        private string _CuentaDebitoBancarioGasto;
        private string _CuentaDebitoBancarioBancos;
        private string _CuentaCreditoBancarioGasto;
        private string _CuentaCreditoBancarioBancos;
        private bool _EditarComprobanteAfterInsertImpTraBan;
        private eTipoDeContabilizacion _TipoContabilizacionAnticipo;
        private eContabilizacionIndividual _ContabIndividualAnticipo;
        private eContabilizacionPorLote _ContabPorLoteAnticipo;
        private string _CuentaAnticipoCaja;
        private string _CuentaAnticipoCobrado;
        private string _CuentaAnticipoOtrosIngresos;
        private string _CuentaAnticipoPagado;
        private string _CuentaAnticipoBanco;
        private string _CuentaAnticipoOtrosEgresos;
        private string _AnticipoTipoComprobante;
        private bool _EditarComprobanteAfterInsertAnticipo;
        private eTipoDeContabilizacion _TipoContabilizacionInventario;
        private string _CuentaCostoDeVenta;
        private string _CuentaInventario;
        private bool _AgruparPorCuentaDeArticuloInven;
        private string _InventarioTipoComprobante;
        private bool _EditarComprobanteAfterInsertInventario;
        private eTipoDeContabilizacion _TipoContabilizacionDePagosSueldos;
        private eContabilizacionIndividual _ContabIndividualPagosSueldos;
        private string _CtaDePagosSueldos;
        private string _CtaDePagosSueldosBanco;
        private string _PagosSueldosTipoComprobante;
        private bool _EditarComprobanteDePagosSueldos;
        private eContabilizacionIndividual _ContabIndividualCajaChica;
        private bool _MostrarDesglosadoCajaChica;
        private string _CuentaCajaChicaGasto;
        private string _CuentaCajaChicaBancoHaber;
        private string _CuentaCajaChicaBancoDebe;
        private string _CuentaCajaChicaBanco;
        private string _SiglasTipoComprobanteCajaChica;
        private bool _EditarComprobanteAfterInsertCajaChica;
        private eContabilizacionIndividual _ContabIndividualRendiciones;
        private bool _MostrarDesglosadoRendiciones;
        private string _CuentaRendicionesGasto;
        private string _CuentaRendicionesBanco;
        private string _CuentaRendicionesAnticipos;
        private string _SiglasTipoComprobanteRendiciones;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string Numero {
            get { return _Numero; }
            set { _Numero = LibString.Mid(value,0,11); }
        }

        public string CuentaIva1Credito {
            get { return _CuentaIva1Credito; }
            set { _CuentaIva1Credito = LibString.Mid(value,0,30); }
        }

        public string CuentaIva1Debito {
            get { return _CuentaIva1Debito; }
            set { _CuentaIva1Debito = LibString.Mid(value,0,30); }
        }

        public string CuentaRetencionIva {
            get { return _CuentaRetencionIva; }
            set { _CuentaRetencionIva = LibString.Mid(value,0,30); }
        }

        public eDondeEfectuoContabilizacionRetIVA DondeContabilizarRetIvaAsEnum {
            get { return _DondeContabilizarRetIva; }
            set { _DondeContabilizarRetIva = value; }
        }

        public string DondeContabilizarRetIva {
            set { _DondeContabilizarRetIva = (eDondeEfectuoContabilizacionRetIVA)LibConvert.DbValueToEnum(value); }
        }

        public string DondeContabilizarRetIvaAsDB {
            get { return LibConvert.EnumToDbValue((int)_DondeContabilizarRetIva); }
        }

        public string DondeContabilizarRetIvaAsString {
            get { return LibEnumHelper.GetDescription(_DondeContabilizarRetIva); }
        }

        public string DiferenciaEnCambioyCalculo {
            get { return _DiferenciaEnCambioyCalculo; }
            set { _DiferenciaEnCambioyCalculo = LibString.Mid(value,0,30); }
        }

        public string CuentaDiferenciaCambiaria {
            get { return _CuentaDiferenciaCambiaria; }
            set { _CuentaDiferenciaCambiaria = LibString.Mid(value,0,30); }
        }

        public eTipoDeContabilizacion TipoContabilizacionCxCAsEnum {
            get { return _TipoContabilizacionCxC; }
            set { _TipoContabilizacionCxC = value; }
        }

        public string TipoContabilizacionCxC {
            set { _TipoContabilizacionCxC = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoContabilizacionCxCAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoContabilizacionCxC); }
        }

        public string TipoContabilizacionCxCAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionCxC); }
        }

        public eContabilizacionIndividual ContabIndividualCxcAsEnum {
            get { return _ContabIndividualCxc; }
            set { _ContabIndividualCxc = value; }
        }

        public string ContabIndividualCxc {
            set { _ContabIndividualCxc = (eContabilizacionIndividual)LibConvert.DbValueToEnum(value); }
        }

        public string ContabIndividualCxcAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabIndividualCxc); }
        }

        public string ContabIndividualCxcAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualCxc); }
        }

        public eContabilizacionPorLote ContabPorLoteCxCAsEnum {
            get { return _ContabPorLoteCxC; }
            set { _ContabPorLoteCxC = value; }
        }

        public string ContabPorLoteCxC {
            set { _ContabPorLoteCxC = (eContabilizacionPorLote)LibConvert.DbValueToEnum(value); }
        }

        public string ContabPorLoteCxCAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabPorLoteCxC); }
        }

        public string ContabPorLoteCxCAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteCxC); }
        }

        public string CuentaCxCClientes {
            get { return _CuentaCxCClientes; }
            set { _CuentaCxCClientes = LibString.Mid(value,0,30); }
        }

        public string CuentaCxCIngresos {
            get { return _CuentaCxCIngresos; }
            set { _CuentaCxCIngresos = LibString.Mid(value,0,30); }
        }

        public string CxCTipoComprobante {
            get { return _CxCTipoComprobante; }
            set { _CxCTipoComprobante = LibString.Mid(value,0,2); }
        }

        public bool EditarComprobanteAfterInsertCxCAsBool {
            get { return _EditarComprobanteAfterInsertCxC; }
            set { _EditarComprobanteAfterInsertCxC = value; }
        }

        public string EditarComprobanteAfterInsertCxC {
            set { _EditarComprobanteAfterInsertCxC = LibConvert.SNToBool(value); }
        }


        public eTipoDeContabilizacion TipoContabilizacionCxPAsEnum {
            get { return _TipoContabilizacionCxP; }
            set { _TipoContabilizacionCxP = value; }
        }

        public string TipoContabilizacionCxP {
            set { _TipoContabilizacionCxP = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoContabilizacionCxPAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoContabilizacionCxP); }
        }

        public string TipoContabilizacionCxPAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionCxP); }
        }

        public eContabilizacionIndividual ContabIndividualCxPAsEnum {
            get { return _ContabIndividualCxP; }
            set { _ContabIndividualCxP = value; }
        }

        public string ContabIndividualCxP {
            set { _ContabIndividualCxP = (eContabilizacionIndividual)LibConvert.DbValueToEnum(value); }
        }

        public string ContabIndividualCxPAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabIndividualCxP); }
        }

        public string ContabIndividualCxPAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualCxP); }
        }

        public eContabilizacionPorLote ContabPorLoteCxPAsEnum {
            get { return _ContabPorLoteCxP; }
            set { _ContabPorLoteCxP = value; }
        }

        public string ContabPorLoteCxP {
            set { _ContabPorLoteCxP = (eContabilizacionPorLote)LibConvert.DbValueToEnum(value); }
        }

        public string ContabPorLoteCxPAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabPorLoteCxP); }
        }

        public string ContabPorLoteCxPAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteCxP); }
        }

        public string CuentaCxPGasto {
            get { return _CuentaCxPGasto; }
            set { _CuentaCxPGasto = LibString.Mid(value,0,30); }
        }

        public string CuentaCxPProveedores {
            get { return _CuentaCxPProveedores; }
            set { _CuentaCxPProveedores = LibString.Mid(value,0,30); }
        }

        public string CuentaRetencionImpuestoMunicipal {
            get { return _CuentaRetencionImpuestoMunicipal; }
            set { _CuentaRetencionImpuestoMunicipal = LibString.Mid(value,0,30); }
        }

        public string CxPTipoComprobante {
            get { return _CxPTipoComprobante; }
            set { _CxPTipoComprobante = LibString.Mid(value,0,2); }
        }

        public bool EditarComprobanteAfterInsertCxPAsBool {
            get { return _EditarComprobanteAfterInsertCxP; }
            set { _EditarComprobanteAfterInsertCxP = value; }
        }

        public string EditarComprobanteAfterInsertCxP {
            set { _EditarComprobanteAfterInsertCxP = LibConvert.SNToBool(value); }
        }


        public eTipoDeContabilizacion TipoContabilizacionCobranzaAsEnum {
            get { return _TipoContabilizacionCobranza; }
            set { _TipoContabilizacionCobranza = value; }
        }

        public string TipoContabilizacionCobranza {
            set { _TipoContabilizacionCobranza = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoContabilizacionCobranzaAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoContabilizacionCobranza); }
        }

        public string TipoContabilizacionCobranzaAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionCobranza); }
        }

        public eContabilizacionIndividual ContabIndividualCobranzaAsEnum {
            get { return _ContabIndividualCobranza; }
            set { _ContabIndividualCobranza = value; }
        }

        public string ContabIndividualCobranza {
            set { _ContabIndividualCobranza = (eContabilizacionIndividual)LibConvert.DbValueToEnum(value); }
        }

        public string ContabIndividualCobranzaAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabIndividualCobranza); }
        }

        public string ContabIndividualCobranzaAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualCobranza); }
        }

        public eContabilizacionPorLote ContabPorLoteCobranzaAsEnum {
            get { return _ContabPorLoteCobranza; }
            set { _ContabPorLoteCobranza = value; }
        }

        public string ContabPorLoteCobranza {
            set { _ContabPorLoteCobranza = (eContabilizacionPorLote)LibConvert.DbValueToEnum(value); }
        }

        public string ContabPorLoteCobranzaAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabPorLoteCobranza); }
        }

        public string ContabPorLoteCobranzaAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteCobranza); }
        }

        public string CuentaCobranzaCobradoEnEfectivo {
            get { return _CuentaCobranzaCobradoEnEfectivo; }
            set { _CuentaCobranzaCobradoEnEfectivo = LibString.Mid(value,0,30); }
        }

        public string CuentaCobranzaCobradoEnCheque {
            get { return _CuentaCobranzaCobradoEnCheque; }
            set { _CuentaCobranzaCobradoEnCheque = LibString.Mid(value,0,30); }
        }

        public string CuentaCobranzaCobradoEnTarjeta {
            get { return _CuentaCobranzaCobradoEnTarjeta; }
            set { _CuentaCobranzaCobradoEnTarjeta = LibString.Mid(value,0,30); }
        }

        public string cuentaCobranzaRetencionISLR {
            get { return _cuentaCobranzaRetencionISLR; }
            set { _cuentaCobranzaRetencionISLR = LibString.Mid(value,0,30); }
        }

        public string cuentaCobranzaRetencionIVA {
            get { return _cuentaCobranzaRetencionIVA; }
            set { _cuentaCobranzaRetencionIVA = LibString.Mid(value,0,30); }
        }

        public string CuentaCobranzaOtros {
            get { return _CuentaCobranzaOtros; }
            set { _CuentaCobranzaOtros = LibString.Mid(value,0,30); }
        }

        public string CuentaCobranzaCxCClientes {
            get { return _CuentaCobranzaCxCClientes; }
            set { _CuentaCobranzaCxCClientes = LibString.Mid(value,0,30); }
        }

        public string CuentaCobranzaCobradoAnticipo {
            get { return _CuentaCobranzaCobradoAnticipo; }
            set { _CuentaCobranzaCobradoAnticipo = LibString.Mid(value,0,30); }
        }

        public string CuentaCobranzaIvaDiferido {
            get { return _CuentaCobranzaIvaDiferido; }
            set { _CuentaCobranzaIvaDiferido = LibString.Mid(value,0,30); }
        }

        public string CobranzaTipoComprobante {
            get { return _CobranzaTipoComprobante; }
            set { _CobranzaTipoComprobante = LibString.Mid(value,0,2); }
        }

        public bool EditarComprobanteAfterInsertCobranzaAsBool {
            get { return _EditarComprobanteAfterInsertCobranza; }
            set { _EditarComprobanteAfterInsertCobranza = value; }
        }

        public string EditarComprobanteAfterInsertCobranza {
            set { _EditarComprobanteAfterInsertCobranza = LibConvert.SNToBool(value); }
        }


        public bool ManejarDiferenciaCambiariaEnCobranzaAsBool {
            get { return _ManejarDiferenciaCambiariaEnCobranza; }
            set { _ManejarDiferenciaCambiariaEnCobranza = value; }
        }

        public string ManejarDiferenciaCambiariaEnCobranza {
            set { _ManejarDiferenciaCambiariaEnCobranza = LibConvert.SNToBool(value); }
        }


        public eTipoDeContabilizacion TipoContabilizacionPagosAsEnum {
            get { return _TipoContabilizacionPagos; }
            set { _TipoContabilizacionPagos = value; }
        }

        public string TipoContabilizacionPagos {
            set { _TipoContabilizacionPagos = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoContabilizacionPagosAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoContabilizacionPagos); }
        }

        public string TipoContabilizacionPagosAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionPagos); }
        }

        public eContabilizacionIndividual ContabIndividualPagosAsEnum {
            get { return _ContabIndividualPagos; }
            set { _ContabIndividualPagos = value; }
        }

        public string ContabIndividualPagos {
            set { _ContabIndividualPagos = (eContabilizacionIndividual)LibConvert.DbValueToEnum(value); }
        }

        public string ContabIndividualPagosAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabIndividualPagos); }
        }

        public string ContabIndividualPagosAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualPagos); }
        }

        public eContabilizacionPorLote ContabPorLotePagosAsEnum {
            get { return _ContabPorLotePagos; }
            set { _ContabPorLotePagos = value; }
        }

        public string ContabPorLotePagos {
            set { _ContabPorLotePagos = (eContabilizacionPorLote)LibConvert.DbValueToEnum(value); }
        }

        public string ContabPorLotePagosAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabPorLotePagos); }
        }

        public string ContabPorLotePagosAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLotePagos); }
        }

        public string CuentaPagosCxPProveedores {
            get { return _CuentaPagosCxPProveedores; }
            set { _CuentaPagosCxPProveedores = LibString.Mid(value,0,30); }
        }

        public string CuentaPagosRetencionISLR {
            get { return _CuentaPagosRetencionISLR; }
            set { _CuentaPagosRetencionISLR = LibString.Mid(value,0,30); }
        }

        public string CuentaPagosOtros {
            get { return _CuentaPagosOtros; }
            set { _CuentaPagosOtros = LibString.Mid(value,0,30); }
        }

        public string CuentaPagosBanco {
            get { return _CuentaPagosBanco; }
            set { _CuentaPagosBanco = LibString.Mid(value,0,30); }
        }

        public string CuentaPagosPagadoAnticipo {
            get { return _CuentaPagosPagadoAnticipo; }
            set { _CuentaPagosPagadoAnticipo = LibString.Mid(value,0,30); }
        }

        public string PagoTipoComprobante {
            get { return _PagoTipoComprobante; }
            set { _PagoTipoComprobante = LibString.Mid(value,0,2); }
        }

        public bool EditarComprobanteAfterInsertPagosAsBool {
            get { return _EditarComprobanteAfterInsertPagos; }
            set { _EditarComprobanteAfterInsertPagos = value; }
        }

        public string EditarComprobanteAfterInsertPagos {
            set { _EditarComprobanteAfterInsertPagos = LibConvert.SNToBool(value); }
        }


        public bool ManejarDiferenciaCambiariaEnPagosAsBool {
            get { return _ManejarDiferenciaCambiariaEnPagos; }
            set { _ManejarDiferenciaCambiariaEnPagos = value; }
        }

        public string ManejarDiferenciaCambiariaEnPagos {
            set { _ManejarDiferenciaCambiariaEnPagos = LibConvert.SNToBool(value); }
        }


        public eTipoDeContabilizacion TipoContabilizacionFacturacionAsEnum {
            get { return _TipoContabilizacionFacturacion; }
            set { _TipoContabilizacionFacturacion = value; }
        }

        public string TipoContabilizacionFacturacion {
            set { _TipoContabilizacionFacturacion = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoContabilizacionFacturacionAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoContabilizacionFacturacion); }
        }

        public string TipoContabilizacionFacturacionAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionFacturacion); }
        }

        public eContabilizacionIndividual ContabIndividualFacturacionAsEnum {
            get { return _ContabIndividualFacturacion; }
            set { _ContabIndividualFacturacion = value; }
        }

        public string ContabIndividualFacturacion {
            set { _ContabIndividualFacturacion = (eContabilizacionIndividual)LibConvert.DbValueToEnum(value); }
        }

        public string ContabIndividualFacturacionAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabIndividualFacturacion); }
        }

        public string ContabIndividualFacturacionAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualFacturacion); }
        }

        public eContabilizacionPorLote ContabPorLoteFacturacionAsEnum {
            get { return _ContabPorLoteFacturacion; }
            set { _ContabPorLoteFacturacion = value; }
        }

        public string ContabPorLoteFacturacion {
            set { _ContabPorLoteFacturacion = (eContabilizacionPorLote)LibConvert.DbValueToEnum(value); }
        }

        public string ContabPorLoteFacturacionAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabPorLoteFacturacion); }
        }

        public string ContabPorLoteFacturacionAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteFacturacion); }
        }

        public string CuentaFacturacionCxCClientes {
            get { return _CuentaFacturacionCxCClientes; }
            set { _CuentaFacturacionCxCClientes = LibString.Mid(value,0,30); }
        }

        public string CuentaFacturacionMontoTotalFactura {
            get { return _CuentaFacturacionMontoTotalFactura; }
            set { _CuentaFacturacionMontoTotalFactura = LibString.Mid(value,0,30); }
        }

        public string CuentaFacturacionCargos {
            get { return _CuentaFacturacionCargos; }
            set { _CuentaFacturacionCargos = LibString.Mid(value,0,30); }
        }

        public string CuentaFacturacionDescuentos {
            get { return _CuentaFacturacionDescuentos; }
            set { _CuentaFacturacionDescuentos = LibString.Mid(value,0,30); }
        }

        public string CuentaFacturacionIvaDiferido {
            get { return _CuentaFacturacionIvaDiferido; }
            set { _CuentaFacturacionIvaDiferido = LibString.Mid(value,0,30); }
        }

        public bool ContabilizarPorArticuloAsBool {
            get { return _ContabilizarPorArticulo; }
            set { _ContabilizarPorArticulo = value; }
        }

        public string ContabilizarPorArticulo {
            set { _ContabilizarPorArticulo = LibConvert.SNToBool(value); }
        }


        public bool AgruparPorCuentaDeArticuloAsBool {
            get { return _AgruparPorCuentaDeArticulo; }
            set { _AgruparPorCuentaDeArticulo = value; }
        }

        public string AgruparPorCuentaDeArticulo {
            set { _AgruparPorCuentaDeArticulo = LibConvert.SNToBool(value); }
        }


        public bool AgruparPorCargosDescuentosAsBool {
            get { return _AgruparPorCargosDescuentos; }
            set { _AgruparPorCargosDescuentos = value; }
        }

        public string AgruparPorCargosDescuentos {
            set { _AgruparPorCargosDescuentos = LibConvert.SNToBool(value); }
        }


        public string FacturaTipoComprobante {
            get { return _FacturaTipoComprobante; }
            set { _FacturaTipoComprobante = LibString.Mid(value,0,2); }
        }

        public bool EditarComprobanteAfterInsertFacturaAsBool {
            get { return _EditarComprobanteAfterInsertFactura; }
            set { _EditarComprobanteAfterInsertFactura = value; }
        }

        public string EditarComprobanteAfterInsertFactura {
            set { _EditarComprobanteAfterInsertFactura = LibConvert.SNToBool(value); }
        }


        public eTipoDeContabilizacion TipoContabilizacionRDVtasAsEnum {
            get { return _TipoContabilizacionRDVtas; }
            set { _TipoContabilizacionRDVtas = value; }
        }

        public string TipoContabilizacionRDVtas {
            set { _TipoContabilizacionRDVtas = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoContabilizacionRDVtasAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoContabilizacionRDVtas); }
        }

        public string TipoContabilizacionRDVtasAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionRDVtas); }
        }

        public eContabilizacionIndividual ContabIndividualRDVtasAsEnum {
            get { return _ContabIndividualRDVtas; }
            set { _ContabIndividualRDVtas = value; }
        }

        public string ContabIndividualRDVtas {
            set { _ContabIndividualRDVtas = (eContabilizacionIndividual)LibConvert.DbValueToEnum(value); }
        }

        public string ContabIndividualRDVtasAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabIndividualRDVtas); }
        }

        public string ContabIndividualRDVtasAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualRDVtas); }
        }

        public eContabilizacionPorLote ContabPorLoteRDVtasAsEnum {
            get { return _ContabPorLoteRDVtas; }
            set { _ContabPorLoteRDVtas = value; }
        }

        public string ContabPorLoteRDVtas {
            set { _ContabPorLoteRDVtas = (eContabilizacionPorLote)LibConvert.DbValueToEnum(value); }
        }

        public string ContabPorLoteRDVtasAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabPorLoteRDVtas); }
        }

        public string ContabPorLoteRDVtasAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteRDVtas); }
        }

        public string CuentaRDVtasCaja {
            get { return _CuentaRDVtasCaja; }
            set { _CuentaRDVtasCaja = LibString.Mid(value,0,30); }
        }

        public string CuentaRDVtasMontoTotal {
            get { return _CuentaRDVtasMontoTotal; }
            set { _CuentaRDVtasMontoTotal = LibString.Mid(value,0,30); }
        }

        public bool ContabilizarPorArticuloRDVtasAsBool {
            get { return _ContabilizarPorArticuloRDVtas; }
            set { _ContabilizarPorArticuloRDVtas = value; }
        }

        public string ContabilizarPorArticuloRDVtas {
            set { _ContabilizarPorArticuloRDVtas = LibConvert.SNToBool(value); }
        }


        public bool AgruparPorCuentaDeArticuloRDVtasAsBool {
            get { return _AgruparPorCuentaDeArticuloRDVtas; }
            set { _AgruparPorCuentaDeArticuloRDVtas = value; }
        }

        public string AgruparPorCuentaDeArticuloRDVtas {
            set { _AgruparPorCuentaDeArticuloRDVtas = LibConvert.SNToBool(value); }
        }


        public bool EditarComprobanteAfterInsertResDiaAsBool {
            get { return _EditarComprobanteAfterInsertResDia; }
            set { _EditarComprobanteAfterInsertResDia = value; }
        }

        public string EditarComprobanteAfterInsertResDia {
            set { _EditarComprobanteAfterInsertResDia = LibConvert.SNToBool(value); }
        }


        public eTipoDeContabilizacion TipoContabilizacionMovBancarioAsEnum {
            get { return _TipoContabilizacionMovBancario; }
            set { _TipoContabilizacionMovBancario = value; }
        }

        public string TipoContabilizacionMovBancario {
            set { _TipoContabilizacionMovBancario = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoContabilizacionMovBancarioAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoContabilizacionMovBancario); }
        }

        public string TipoContabilizacionMovBancarioAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionMovBancario); }
        }

        public eContabilizacionIndividual ContabIndividualMovBancarioAsEnum {
            get { return _ContabIndividualMovBancario; }
            set { _ContabIndividualMovBancario = value; }
        }

        public string ContabIndividualMovBancario {
            set { _ContabIndividualMovBancario = (eContabilizacionIndividual)LibConvert.DbValueToEnum(value); }
        }

        public string ContabIndividualMovBancarioAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabIndividualMovBancario); }
        }

        public string ContabIndividualMovBancarioAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualMovBancario); }
        }

        public eContabilizacionPorLote ContabPorLoteMovBancarioAsEnum {
            get { return _ContabPorLoteMovBancario; }
            set { _ContabPorLoteMovBancario = value; }
        }

        public string ContabPorLoteMovBancario {
            set { _ContabPorLoteMovBancario = (eContabilizacionPorLote)LibConvert.DbValueToEnum(value); }
        }

        public string ContabPorLoteMovBancarioAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabPorLoteMovBancario); }
        }

        public string ContabPorLoteMovBancarioAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteMovBancario); }
        }

        public string CuentaMovBancarioGasto {
            get { return _CuentaMovBancarioGasto; }
            set { _CuentaMovBancarioGasto = LibString.Mid(value,0,30); }
        }

        public string CuentaMovBancarioBancosHaber {
            get { return _CuentaMovBancarioBancosHaber; }
            set { _CuentaMovBancarioBancosHaber = LibString.Mid(value,0,30); }
        }

        public string CuentaMovBancarioBancosDebe {
            get { return _CuentaMovBancarioBancosDebe; }
            set { _CuentaMovBancarioBancosDebe = LibString.Mid(value,0,30); }
        }

        public string CuentaMovBancarioIngresos {
            get { return _CuentaMovBancarioIngresos; }
            set { _CuentaMovBancarioIngresos = LibString.Mid(value,0,30); }
        }

        public string MovimientoBancarioTipoComprobante {
            get { return _MovimientoBancarioTipoComprobante; }
            set { _MovimientoBancarioTipoComprobante = LibString.Mid(value,0,2); }
        }

        public bool EditarComprobanteAfterInsertMovBanAsBool {
            get { return _EditarComprobanteAfterInsertMovBan; }
            set { _EditarComprobanteAfterInsertMovBan = value; }
        }

        public string EditarComprobanteAfterInsertMovBan {
            set { _EditarComprobanteAfterInsertMovBan = LibConvert.SNToBool(value); }
        }


        public string CuentaDebitoBancarioGasto {
            get { return _CuentaDebitoBancarioGasto; }
            set { _CuentaDebitoBancarioGasto = LibString.Mid(value,0,30); }
        }

        public string CuentaDebitoBancarioBancos {
            get { return _CuentaDebitoBancarioBancos; }
            set { _CuentaDebitoBancarioBancos = LibString.Mid(value,0,30); }
        }

        public string CuentaCreditoBancarioGasto {
            get { return _CuentaCreditoBancarioGasto; }
            set { _CuentaCreditoBancarioGasto = LibString.Mid(value,0,30); }
        }

        public string CuentaCreditoBancarioBancos {
            get { return _CuentaCreditoBancarioBancos; }
            set { _CuentaCreditoBancarioBancos = LibString.Mid(value,0,30); }
        }

        public bool EditarComprobanteAfterInsertImpTraBanAsBool {
            get { return _EditarComprobanteAfterInsertImpTraBan; }
            set { _EditarComprobanteAfterInsertImpTraBan = value; }
        }

        public string EditarComprobanteAfterInsertImpTraBan {
            set { _EditarComprobanteAfterInsertImpTraBan = LibConvert.SNToBool(value); }
        }


        public eTipoDeContabilizacion TipoContabilizacionAnticipoAsEnum {
            get { return _TipoContabilizacionAnticipo; }
            set { _TipoContabilizacionAnticipo = value; }
        }

        public string TipoContabilizacionAnticipo {
            set { _TipoContabilizacionAnticipo = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoContabilizacionAnticipoAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoContabilizacionAnticipo); }
        }

        public string TipoContabilizacionAnticipoAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionAnticipo); }
        }

        public eContabilizacionIndividual ContabIndividualAnticipoAsEnum {
            get { return _ContabIndividualAnticipo; }
            set { _ContabIndividualAnticipo = value; }
        }

        public string ContabIndividualAnticipo {
            set { _ContabIndividualAnticipo = (eContabilizacionIndividual)LibConvert.DbValueToEnum(value); }
        }

        public string ContabIndividualAnticipoAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabIndividualAnticipo); }
        }

        public string ContabIndividualAnticipoAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualAnticipo); }
        }

        public eContabilizacionPorLote ContabPorLoteAnticipoAsEnum {
            get { return _ContabPorLoteAnticipo; }
            set { _ContabPorLoteAnticipo = value; }
        }

        public string ContabPorLoteAnticipo {
            set { _ContabPorLoteAnticipo = (eContabilizacionPorLote)LibConvert.DbValueToEnum(value); }
        }

        public string ContabPorLoteAnticipoAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabPorLoteAnticipo); }
        }

        public string ContabPorLoteAnticipoAsString {
            get { return LibEnumHelper.GetDescription(_ContabPorLoteAnticipo); }
        }

        public string CuentaAnticipoCaja {
            get { return _CuentaAnticipoCaja; }
            set { _CuentaAnticipoCaja = LibString.Mid(value,0,30); }
        }

        public string CuentaAnticipoCobrado {
            get { return _CuentaAnticipoCobrado; }
            set { _CuentaAnticipoCobrado = LibString.Mid(value,0,30); }
        }

        public string CuentaAnticipoOtrosIngresos {
            get { return _CuentaAnticipoOtrosIngresos; }
            set { _CuentaAnticipoOtrosIngresos = LibString.Mid(value,0,30); }
        }

        public string CuentaAnticipoPagado {
            get { return _CuentaAnticipoPagado; }
            set { _CuentaAnticipoPagado = LibString.Mid(value,0,30); }
        }

        public string CuentaAnticipoBanco {
            get { return _CuentaAnticipoBanco; }
            set { _CuentaAnticipoBanco = LibString.Mid(value,0,30); }
        }

        public string CuentaAnticipoOtrosEgresos {
            get { return _CuentaAnticipoOtrosEgresos; }
            set { _CuentaAnticipoOtrosEgresos = LibString.Mid(value,0,30); }
        }

        public string AnticipoTipoComprobante {
            get { return _AnticipoTipoComprobante; }
            set { _AnticipoTipoComprobante = LibString.Mid(value,0,2); }
        }

        public bool EditarComprobanteAfterInsertAnticipoAsBool {
            get { return _EditarComprobanteAfterInsertAnticipo; }
            set { _EditarComprobanteAfterInsertAnticipo = value; }
        }

        public string EditarComprobanteAfterInsertAnticipo {
            set { _EditarComprobanteAfterInsertAnticipo = LibConvert.SNToBool(value); }
        }


        public eTipoDeContabilizacion TipoContabilizacionInventarioAsEnum {
            get { return _TipoContabilizacionInventario; }
            set { _TipoContabilizacionInventario = value; }
        }

        public string TipoContabilizacionInventario {
            set { _TipoContabilizacionInventario = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoContabilizacionInventarioAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoContabilizacionInventario); }
        }

        public string TipoContabilizacionInventarioAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionInventario); }
        }

        public string CuentaCostoDeVenta {
            get { return _CuentaCostoDeVenta; }
            set { _CuentaCostoDeVenta = LibString.Mid(value,0,30); }
        }

        public string CuentaInventario {
            get { return _CuentaInventario; }
            set { _CuentaInventario = LibString.Mid(value,0,30); }
        }

        public bool AgruparPorCuentaDeArticuloInvenAsBool {
            get { return _AgruparPorCuentaDeArticuloInven; }
            set { _AgruparPorCuentaDeArticuloInven = value; }
        }

        public string AgruparPorCuentaDeArticuloInven {
            set { _AgruparPorCuentaDeArticuloInven = LibConvert.SNToBool(value); }
        }


        public string InventarioTipoComprobante {
            get { return _InventarioTipoComprobante; }
            set { _InventarioTipoComprobante = LibString.Mid(value,0,2); }
        }

        public bool EditarComprobanteAfterInsertInventarioAsBool {
            get { return _EditarComprobanteAfterInsertInventario; }
            set { _EditarComprobanteAfterInsertInventario = value; }
        }

        public string EditarComprobanteAfterInsertInventario {
            set { _EditarComprobanteAfterInsertInventario = LibConvert.SNToBool(value); }
        }


        public eTipoDeContabilizacion TipoContabilizacionDePagosSueldosAsEnum {
            get { return _TipoContabilizacionDePagosSueldos; }
            set { _TipoContabilizacionDePagosSueldos = value; }
        }

        public string TipoContabilizacionDePagosSueldos {
            set { _TipoContabilizacionDePagosSueldos = (eTipoDeContabilizacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoContabilizacionDePagosSueldosAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoContabilizacionDePagosSueldos); }
        }

        public string TipoContabilizacionDePagosSueldosAsString {
            get { return LibEnumHelper.GetDescription(_TipoContabilizacionDePagosSueldos); }
        }

        public eContabilizacionIndividual ContabIndividualPagosSueldosAsEnum {
            get { return _ContabIndividualPagosSueldos; }
            set { _ContabIndividualPagosSueldos = value; }
        }

        public string ContabIndividualPagosSueldos {
            set { _ContabIndividualPagosSueldos = (eContabilizacionIndividual)LibConvert.DbValueToEnum(value); }
        }

        public string ContabIndividualPagosSueldosAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabIndividualPagosSueldos); }
        }

        public string ContabIndividualPagosSueldosAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualPagosSueldos); }
        }

        public string CtaDePagosSueldos {
            get { return _CtaDePagosSueldos; }
            set { _CtaDePagosSueldos = LibString.Mid(value,0,30); }
        }

        public string CtaDePagosSueldosBanco {
            get { return _CtaDePagosSueldosBanco; }
            set { _CtaDePagosSueldosBanco = LibString.Mid(value,0,30); }
        }

        public string PagosSueldosTipoComprobante {
            get { return _PagosSueldosTipoComprobante; }
            set { _PagosSueldosTipoComprobante = LibString.Mid(value,0,2); }
        }

        public bool EditarComprobanteDePagosSueldosAsBool {
            get { return _EditarComprobanteDePagosSueldos; }
            set { _EditarComprobanteDePagosSueldos = value; }
        }

        public string EditarComprobanteDePagosSueldos {
            set { _EditarComprobanteDePagosSueldos = LibConvert.SNToBool(value); }
        }


        public eContabilizacionIndividual ContabIndividualCajaChicaAsEnum {
            get { return _ContabIndividualCajaChica; }
            set { _ContabIndividualCajaChica = value; }
        }

        public string ContabIndividualCajaChica {
            set { _ContabIndividualCajaChica = (eContabilizacionIndividual)LibConvert.DbValueToEnum(value); }
        }

        public string ContabIndividualCajaChicaAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabIndividualCajaChica); }
        }

        public string ContabIndividualCajaChicaAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualCajaChica); }
        }

        public bool MostrarDesglosadoCajaChicaAsBool {
            get { return _MostrarDesglosadoCajaChica; }
            set { _MostrarDesglosadoCajaChica = value; }
        }

        public string MostrarDesglosadoCajaChica {
            set { _MostrarDesglosadoCajaChica = LibConvert.SNToBool(value); }
        }


        public string CuentaCajaChicaGasto {
            get { return _CuentaCajaChicaGasto; }
            set { _CuentaCajaChicaGasto = LibString.Mid(value,0,30); }
        }

        public string CuentaCajaChicaBancoHaber {
            get { return _CuentaCajaChicaBancoHaber; }
            set { _CuentaCajaChicaBancoHaber = LibString.Mid(value,0,30); }
        }

        public string CuentaCajaChicaBancoDebe {
            get { return _CuentaCajaChicaBancoDebe; }
            set { _CuentaCajaChicaBancoDebe = LibString.Mid(value,0,30); }
        }

        public string CuentaCajaChicaBanco {
            get { return _CuentaCajaChicaBanco; }
            set { _CuentaCajaChicaBanco = LibString.Mid(value,0,30); }
        }

        public string SiglasTipoComprobanteCajaChica {
            get { return _SiglasTipoComprobanteCajaChica; }
            set { _SiglasTipoComprobanteCajaChica = LibString.Mid(value,0,2); }
        }

        public bool EditarComprobanteAfterInsertCajaChicaAsBool {
            get { return _EditarComprobanteAfterInsertCajaChica; }
            set { _EditarComprobanteAfterInsertCajaChica = value; }
        }

        public string EditarComprobanteAfterInsertCajaChica {
            set { _EditarComprobanteAfterInsertCajaChica = LibConvert.SNToBool(value); }
        }


        public eContabilizacionIndividual ContabIndividualRendicionesAsEnum {
            get { return _ContabIndividualRendiciones; }
            set { _ContabIndividualRendiciones = value; }
        }

        public string ContabIndividualRendiciones {
            set { _ContabIndividualRendiciones = (eContabilizacionIndividual)LibConvert.DbValueToEnum(value); }
        }

        public string ContabIndividualRendicionesAsDB {
            get { return LibConvert.EnumToDbValue((int)_ContabIndividualRendiciones); }
        }

        public string ContabIndividualRendicionesAsString {
            get { return LibEnumHelper.GetDescription(_ContabIndividualRendiciones); }
        }

        public bool MostrarDesglosadoRendicionesAsBool {
            get { return _MostrarDesglosadoRendiciones; }
            set { _MostrarDesglosadoRendiciones = value; }
        }

        public string MostrarDesglosadoRendiciones {
            set { _MostrarDesglosadoRendiciones = LibConvert.SNToBool(value); }
        }


        public string CuentaRendicionesGasto {
            get { return _CuentaRendicionesGasto; }
            set { _CuentaRendicionesGasto = LibString.Mid(value,0,30); }
        }

        public string CuentaRendicionesBanco {
            get { return _CuentaRendicionesBanco; }
            set { _CuentaRendicionesBanco = LibString.Mid(value,0,30); }
        }

        public string CuentaRendicionesAnticipos {
            get { return _CuentaRendicionesAnticipos; }
            set { _CuentaRendicionesAnticipos = LibString.Mid(value,0,30); }
        }

        public string SiglasTipoComprobanteRendiciones {
            get { return _SiglasTipoComprobanteRendiciones; }
            set { _SiglasTipoComprobanteRendiciones = LibString.Mid(value,0,2); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value,0,10); }
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

        public ReglasDeContabilizacion() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Numero = string.Empty;
            CuentaIva1Credito = string.Empty;
            CuentaIva1Debito = string.Empty;
            CuentaRetencionIva = string.Empty;
            DondeContabilizarRetIvaAsEnum = eDondeEfectuoContabilizacionRetIVA.NoContabilizada;
            DiferenciaEnCambioyCalculo = string.Empty;
            CuentaDiferenciaCambiaria = string.Empty;
            TipoContabilizacionCxCAsEnum = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualCxcAsEnum = eContabilizacionIndividual.Inmediata;
            ContabPorLoteCxCAsEnum = eContabilizacionPorLote.Diaria;
            CuentaCxCClientes = string.Empty;
            CuentaCxCIngresos = string.Empty;
            CxCTipoComprobante = string.Empty;
            EditarComprobanteAfterInsertCxCAsBool = false;
            TipoContabilizacionCxPAsEnum = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualCxPAsEnum = eContabilizacionIndividual.Inmediata;
            ContabPorLoteCxPAsEnum = eContabilizacionPorLote.Diaria;
            CuentaCxPGasto = string.Empty;
            CuentaCxPProveedores = string.Empty;
            CuentaRetencionImpuestoMunicipal = string.Empty;
            CxPTipoComprobante = string.Empty;
            EditarComprobanteAfterInsertCxPAsBool = false;
            TipoContabilizacionCobranzaAsEnum = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualCobranzaAsEnum = eContabilizacionIndividual.Inmediata;
            ContabPorLoteCobranzaAsEnum = eContabilizacionPorLote.Diaria;
            CuentaCobranzaCobradoEnEfectivo = string.Empty;
            CuentaCobranzaCobradoEnCheque = string.Empty;
            CuentaCobranzaCobradoEnTarjeta = string.Empty;
            cuentaCobranzaRetencionISLR = string.Empty;
            cuentaCobranzaRetencionIVA = string.Empty;
            CuentaCobranzaOtros = string.Empty;
            CuentaCobranzaCxCClientes = string.Empty;
            CuentaCobranzaCobradoAnticipo = string.Empty;
            CuentaCobranzaIvaDiferido = string.Empty;
            CobranzaTipoComprobante = string.Empty;
            EditarComprobanteAfterInsertCobranzaAsBool = false;
            ManejarDiferenciaCambiariaEnCobranzaAsBool = false;
            TipoContabilizacionPagosAsEnum = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualPagosAsEnum = eContabilizacionIndividual.Inmediata;
            ContabPorLotePagosAsEnum = eContabilizacionPorLote.Diaria;
            CuentaPagosCxPProveedores = string.Empty;
            CuentaPagosRetencionISLR = string.Empty;
            CuentaPagosOtros = string.Empty;
            CuentaPagosBanco = string.Empty;
            CuentaPagosPagadoAnticipo = string.Empty;
            PagoTipoComprobante = string.Empty;
            EditarComprobanteAfterInsertPagosAsBool = false;
            ManejarDiferenciaCambiariaEnPagosAsBool = false;
            TipoContabilizacionFacturacionAsEnum = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualFacturacionAsEnum = eContabilizacionIndividual.Inmediata;
            ContabPorLoteFacturacionAsEnum = eContabilizacionPorLote.Diaria;
            CuentaFacturacionCxCClientes = string.Empty;
            CuentaFacturacionMontoTotalFactura = string.Empty;
            CuentaFacturacionCargos = string.Empty;
            CuentaFacturacionDescuentos = string.Empty;
            CuentaFacturacionIvaDiferido = string.Empty;
            ContabilizarPorArticuloAsBool = false;
            AgruparPorCuentaDeArticuloAsBool = false;
            AgruparPorCargosDescuentosAsBool = false;
            FacturaTipoComprobante = string.Empty;
            EditarComprobanteAfterInsertFacturaAsBool = false;
            TipoContabilizacionRDVtasAsEnum = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualRDVtasAsEnum = eContabilizacionIndividual.Inmediata;
            ContabPorLoteRDVtasAsEnum = eContabilizacionPorLote.Diaria;
            CuentaRDVtasCaja = string.Empty;
            CuentaRDVtasMontoTotal = string.Empty;
            ContabilizarPorArticuloRDVtasAsBool = false;
            AgruparPorCuentaDeArticuloRDVtasAsBool = false;
            EditarComprobanteAfterInsertResDiaAsBool = false;
            TipoContabilizacionMovBancarioAsEnum = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualMovBancarioAsEnum = eContabilizacionIndividual.Inmediata;
            ContabPorLoteMovBancarioAsEnum = eContabilizacionPorLote.Diaria;
            CuentaMovBancarioGasto = string.Empty;
            CuentaMovBancarioBancosHaber = string.Empty;
            CuentaMovBancarioBancosDebe = string.Empty;
            CuentaMovBancarioIngresos = string.Empty;
            MovimientoBancarioTipoComprobante = string.Empty;
            EditarComprobanteAfterInsertMovBanAsBool = false;
            CuentaDebitoBancarioGasto = string.Empty;
            CuentaDebitoBancarioBancos = string.Empty;
            CuentaCreditoBancarioGasto = string.Empty;
            CuentaCreditoBancarioBancos = string.Empty;
            EditarComprobanteAfterInsertImpTraBanAsBool = false;
            TipoContabilizacionAnticipoAsEnum = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualAnticipoAsEnum = eContabilizacionIndividual.Inmediata;
            ContabPorLoteAnticipoAsEnum = eContabilizacionPorLote.Diaria;
            CuentaAnticipoCaja = string.Empty;
            CuentaAnticipoCobrado = string.Empty;
            CuentaAnticipoOtrosIngresos = string.Empty;
            CuentaAnticipoPagado = string.Empty;
            CuentaAnticipoBanco = string.Empty;
            CuentaAnticipoOtrosEgresos = string.Empty;
            AnticipoTipoComprobante = string.Empty;
            EditarComprobanteAfterInsertAnticipoAsBool = false;
            TipoContabilizacionInventarioAsEnum = eTipoDeContabilizacion.CadaDocumento;
            CuentaCostoDeVenta = string.Empty;
            CuentaInventario = string.Empty;
            AgruparPorCuentaDeArticuloInvenAsBool = false;
            InventarioTipoComprobante = string.Empty;
            EditarComprobanteAfterInsertInventarioAsBool = false;
            TipoContabilizacionDePagosSueldosAsEnum = eTipoDeContabilizacion.CadaDocumento;
            ContabIndividualPagosSueldosAsEnum = eContabilizacionIndividual.Inmediata;
            CtaDePagosSueldos = string.Empty;
            CtaDePagosSueldosBanco = string.Empty;
            PagosSueldosTipoComprobante = string.Empty;
            EditarComprobanteDePagosSueldosAsBool = false;
            ContabIndividualCajaChicaAsEnum = eContabilizacionIndividual.Inmediata;
            MostrarDesglosadoCajaChicaAsBool = false;
            CuentaCajaChicaGasto = string.Empty;
            CuentaCajaChicaBancoHaber = string.Empty;
            CuentaCajaChicaBancoDebe = string.Empty;
            CuentaCajaChicaBanco = string.Empty;
            SiglasTipoComprobanteCajaChica = string.Empty;
            EditarComprobanteAfterInsertCajaChicaAsBool = false;
            ContabIndividualRendicionesAsEnum = eContabilizacionIndividual.Inmediata;
            MostrarDesglosadoRendicionesAsBool = false;
            CuentaRendicionesGasto = string.Empty;
            CuentaRendicionesBanco = string.Empty;
            CuentaRendicionesAnticipos = string.Empty;
            SiglasTipoComprobanteRendiciones = string.Empty;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public ReglasDeContabilizacion Clone() {
            ReglasDeContabilizacion vResult = new ReglasDeContabilizacion();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Numero = _Numero;
            vResult.CuentaIva1Credito = _CuentaIva1Credito;
            vResult.CuentaIva1Debito = _CuentaIva1Debito;
            vResult.CuentaRetencionIva = _CuentaRetencionIva;
            vResult.DondeContabilizarRetIvaAsEnum = _DondeContabilizarRetIva;
            vResult.DiferenciaEnCambioyCalculo = _DiferenciaEnCambioyCalculo;
            vResult.CuentaDiferenciaCambiaria = _CuentaDiferenciaCambiaria;
            vResult.TipoContabilizacionCxCAsEnum = _TipoContabilizacionCxC;
            vResult.ContabIndividualCxcAsEnum = _ContabIndividualCxc;
            vResult.ContabPorLoteCxCAsEnum = _ContabPorLoteCxC;
            vResult.CuentaCxCClientes = _CuentaCxCClientes;
            vResult.CuentaCxCIngresos = _CuentaCxCIngresos;
            vResult.CxCTipoComprobante = _CxCTipoComprobante;
            vResult.EditarComprobanteAfterInsertCxCAsBool = _EditarComprobanteAfterInsertCxC;
            vResult.TipoContabilizacionCxPAsEnum = _TipoContabilizacionCxP;
            vResult.ContabIndividualCxPAsEnum = _ContabIndividualCxP;
            vResult.ContabPorLoteCxPAsEnum = _ContabPorLoteCxP;
            vResult.CuentaCxPGasto = _CuentaCxPGasto;
            vResult.CuentaCxPProveedores = _CuentaCxPProveedores;
            vResult.CuentaRetencionImpuestoMunicipal = _CuentaRetencionImpuestoMunicipal;
            vResult.CxPTipoComprobante = _CxPTipoComprobante;
            vResult.EditarComprobanteAfterInsertCxPAsBool = _EditarComprobanteAfterInsertCxP;
            vResult.TipoContabilizacionCobranzaAsEnum = _TipoContabilizacionCobranza;
            vResult.ContabIndividualCobranzaAsEnum = _ContabIndividualCobranza;
            vResult.ContabPorLoteCobranzaAsEnum = _ContabPorLoteCobranza;
            vResult.CuentaCobranzaCobradoEnEfectivo = _CuentaCobranzaCobradoEnEfectivo;
            vResult.CuentaCobranzaCobradoEnCheque = _CuentaCobranzaCobradoEnCheque;
            vResult.CuentaCobranzaCobradoEnTarjeta = _CuentaCobranzaCobradoEnTarjeta;
            vResult.cuentaCobranzaRetencionISLR = _cuentaCobranzaRetencionISLR;
            vResult.cuentaCobranzaRetencionIVA = _cuentaCobranzaRetencionIVA;
            vResult.CuentaCobranzaOtros = _CuentaCobranzaOtros;
            vResult.CuentaCobranzaCxCClientes = _CuentaCobranzaCxCClientes;
            vResult.CuentaCobranzaCobradoAnticipo = _CuentaCobranzaCobradoAnticipo;
            vResult.CuentaCobranzaIvaDiferido = _CuentaCobranzaIvaDiferido;
            vResult.CobranzaTipoComprobante = _CobranzaTipoComprobante;
            vResult.EditarComprobanteAfterInsertCobranzaAsBool = _EditarComprobanteAfterInsertCobranza;
            vResult.ManejarDiferenciaCambiariaEnCobranzaAsBool = _ManejarDiferenciaCambiariaEnCobranza;
            vResult.TipoContabilizacionPagosAsEnum = _TipoContabilizacionPagos;
            vResult.ContabIndividualPagosAsEnum = _ContabIndividualPagos;
            vResult.ContabPorLotePagosAsEnum = _ContabPorLotePagos;
            vResult.CuentaPagosCxPProveedores = _CuentaPagosCxPProveedores;
            vResult.CuentaPagosRetencionISLR = _CuentaPagosRetencionISLR;
            vResult.CuentaPagosOtros = _CuentaPagosOtros;
            vResult.CuentaPagosBanco = _CuentaPagosBanco;
            vResult.CuentaPagosPagadoAnticipo = _CuentaPagosPagadoAnticipo;
            vResult.PagoTipoComprobante = _PagoTipoComprobante;
            vResult.EditarComprobanteAfterInsertPagosAsBool = _EditarComprobanteAfterInsertPagos;
            vResult.ManejarDiferenciaCambiariaEnPagosAsBool = _ManejarDiferenciaCambiariaEnPagos;
            vResult.TipoContabilizacionFacturacionAsEnum = _TipoContabilizacionFacturacion;
            vResult.ContabIndividualFacturacionAsEnum = _ContabIndividualFacturacion;
            vResult.ContabPorLoteFacturacionAsEnum = _ContabPorLoteFacturacion;
            vResult.CuentaFacturacionCxCClientes = _CuentaFacturacionCxCClientes;
            vResult.CuentaFacturacionMontoTotalFactura = _CuentaFacturacionMontoTotalFactura;
            vResult.CuentaFacturacionCargos = _CuentaFacturacionCargos;
            vResult.CuentaFacturacionDescuentos = _CuentaFacturacionDescuentos;
            vResult.CuentaFacturacionIvaDiferido = _CuentaFacturacionIvaDiferido;
            vResult.ContabilizarPorArticuloAsBool = _ContabilizarPorArticulo;
            vResult.AgruparPorCuentaDeArticuloAsBool = _AgruparPorCuentaDeArticulo;
            vResult.AgruparPorCargosDescuentosAsBool = _AgruparPorCargosDescuentos;
            vResult.FacturaTipoComprobante = _FacturaTipoComprobante;
            vResult.EditarComprobanteAfterInsertFacturaAsBool = _EditarComprobanteAfterInsertFactura;
            vResult.TipoContabilizacionRDVtasAsEnum = _TipoContabilizacionRDVtas;
            vResult.ContabIndividualRDVtasAsEnum = _ContabIndividualRDVtas;
            vResult.ContabPorLoteRDVtasAsEnum = _ContabPorLoteRDVtas;
            vResult.CuentaRDVtasCaja = _CuentaRDVtasCaja;
            vResult.CuentaRDVtasMontoTotal = _CuentaRDVtasMontoTotal;
            vResult.ContabilizarPorArticuloRDVtasAsBool = _ContabilizarPorArticuloRDVtas;
            vResult.AgruparPorCuentaDeArticuloRDVtasAsBool = _AgruparPorCuentaDeArticuloRDVtas;
            vResult.EditarComprobanteAfterInsertResDiaAsBool = _EditarComprobanteAfterInsertResDia;
            vResult.TipoContabilizacionMovBancarioAsEnum = _TipoContabilizacionMovBancario;
            vResult.ContabIndividualMovBancarioAsEnum = _ContabIndividualMovBancario;
            vResult.ContabPorLoteMovBancarioAsEnum = _ContabPorLoteMovBancario;
            vResult.CuentaMovBancarioGasto = _CuentaMovBancarioGasto;
            vResult.CuentaMovBancarioBancosHaber = _CuentaMovBancarioBancosHaber;
            vResult.CuentaMovBancarioBancosDebe = _CuentaMovBancarioBancosDebe;
            vResult.CuentaMovBancarioIngresos = _CuentaMovBancarioIngresos;
            vResult.MovimientoBancarioTipoComprobante = _MovimientoBancarioTipoComprobante;
            vResult.EditarComprobanteAfterInsertMovBanAsBool = _EditarComprobanteAfterInsertMovBan;
            vResult.CuentaDebitoBancarioGasto = _CuentaDebitoBancarioGasto;
            vResult.CuentaDebitoBancarioBancos = _CuentaDebitoBancarioBancos;
            vResult.CuentaCreditoBancarioGasto = _CuentaCreditoBancarioGasto;
            vResult.CuentaCreditoBancarioBancos = _CuentaCreditoBancarioBancos;
            vResult.EditarComprobanteAfterInsertImpTraBanAsBool = _EditarComprobanteAfterInsertImpTraBan;
            vResult.TipoContabilizacionAnticipoAsEnum = _TipoContabilizacionAnticipo;
            vResult.ContabIndividualAnticipoAsEnum = _ContabIndividualAnticipo;
            vResult.ContabPorLoteAnticipoAsEnum = _ContabPorLoteAnticipo;
            vResult.CuentaAnticipoCaja = _CuentaAnticipoCaja;
            vResult.CuentaAnticipoCobrado = _CuentaAnticipoCobrado;
            vResult.CuentaAnticipoOtrosIngresos = _CuentaAnticipoOtrosIngresos;
            vResult.CuentaAnticipoPagado = _CuentaAnticipoPagado;
            vResult.CuentaAnticipoBanco = _CuentaAnticipoBanco;
            vResult.CuentaAnticipoOtrosEgresos = _CuentaAnticipoOtrosEgresos;
            vResult.AnticipoTipoComprobante = _AnticipoTipoComprobante;
            vResult.EditarComprobanteAfterInsertAnticipoAsBool = _EditarComprobanteAfterInsertAnticipo;
            vResult.TipoContabilizacionInventarioAsEnum = _TipoContabilizacionInventario;
            vResult.CuentaCostoDeVenta = _CuentaCostoDeVenta;
            vResult.CuentaInventario = _CuentaInventario;
            vResult.AgruparPorCuentaDeArticuloInvenAsBool = _AgruparPorCuentaDeArticuloInven;
            vResult.InventarioTipoComprobante = _InventarioTipoComprobante;
            vResult.EditarComprobanteAfterInsertInventarioAsBool = _EditarComprobanteAfterInsertInventario;
            vResult.TipoContabilizacionDePagosSueldosAsEnum = _TipoContabilizacionDePagosSueldos;
            vResult.ContabIndividualPagosSueldosAsEnum = _ContabIndividualPagosSueldos;
            vResult.CtaDePagosSueldos = _CtaDePagosSueldos;
            vResult.CtaDePagosSueldosBanco = _CtaDePagosSueldosBanco;
            vResult.PagosSueldosTipoComprobante = _PagosSueldosTipoComprobante;
            vResult.EditarComprobanteDePagosSueldosAsBool = _EditarComprobanteDePagosSueldos;
            vResult.ContabIndividualCajaChicaAsEnum = _ContabIndividualCajaChica;
            vResult.MostrarDesglosadoCajaChicaAsBool = _MostrarDesglosadoCajaChica;
            vResult.CuentaCajaChicaGasto = _CuentaCajaChicaGasto;
            vResult.CuentaCajaChicaBancoHaber = _CuentaCajaChicaBancoHaber;
            vResult.CuentaCajaChicaBancoDebe = _CuentaCajaChicaBancoDebe;
            vResult.CuentaCajaChicaBanco = _CuentaCajaChicaBanco;
            vResult.SiglasTipoComprobanteCajaChica = _SiglasTipoComprobanteCajaChica;
            vResult.EditarComprobanteAfterInsertCajaChicaAsBool = _EditarComprobanteAfterInsertCajaChica;
            vResult.ContabIndividualRendicionesAsEnum = _ContabIndividualRendiciones;
            vResult.MostrarDesglosadoRendicionesAsBool = _MostrarDesglosadoRendiciones;
            vResult.CuentaRendicionesGasto = _CuentaRendicionesGasto;
            vResult.CuentaRendicionesBanco = _CuentaRendicionesBanco;
            vResult.CuentaRendicionesAnticipos = _CuentaRendicionesAnticipos;
            vResult.SiglasTipoComprobanteRendiciones = _SiglasTipoComprobanteRendiciones;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
                "\nNúmero = " + _Numero +
                "\nNombre Operador = " + _NombreOperador +
                "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados
    } //End of class ReglasDeContabilizacion
} //End of namespace Galac.Saw.Ccl.Contabilizacion

