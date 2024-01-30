using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Lib;

namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class Cobranza {
        #region Variables
        private int _ConsecutivoCompania;
        private string _Numero;
        private eStatusCobranza _StatusCobranza;
        private DateTime _Fecha;
        private DateTime _FechaAnulacion;
        private string _CodigoCliente;
        private string _CodigoCobrador;
        private decimal _TotalDocumentos;
        private decimal _RetencionIslr;
        private decimal _TotalCobrado;
        private decimal _CobradoEfectivo;
        private decimal _CobradoCheque;
        private string _NumerodelCheque;
        private decimal _CobradoTarjeta;
        private eTipoDeTarjeta _CualTarjeta;
        private string _NroDeLaTarjeta;
        private eOrigenFacturacionOManual _Origen;
        private decimal _TotalOtros;
        private string _NombreBanco;
        private string _CodigoCuentaBancaria;
        private string _CodigoConcepto;
        private string _Moneda;
        private decimal _CambioAbolivares;
        private decimal _RetencionIva;
        private string _NroComprobanteRetIva;
        private eStatusRetencionIVACobranza _StatusRetencionIVA;
        private bool _GeneraMovBancario;
        private decimal _CobradoAnticipo;
        private decimal _Vuelto;
        private decimal _DescProntoPago;
        private decimal _DescProntoPagoPorc;
        private decimal _ComisionVendedor;
        private bool _AplicaCreditoBancario;
        private string _CodigoMoneda;
        private int _NumeroDeComprobanteISLR;
        private eTipoDeTransaccion _TipoDeDocumento;
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
            set { _Numero = LibString.Mid(value, 0, 10); }
        }

        public eStatusCobranza StatusCobranzaAsEnum {
            get { return _StatusCobranza; }
            set { _StatusCobranza = value; }
        }

        public string StatusCobranza {
            set { _StatusCobranza = (eStatusCobranza)LibConvert.DbValueToEnum(value); }
        }

        public string StatusCobranzaAsDB {
            get { return LibConvert.EnumToDbValue((int) _StatusCobranza); }
        }

        public string StatusCobranzaAsString {
            get { return LibEnumHelper.GetDescription(_StatusCobranza); }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaAnulacion {
            get { return _FechaAnulacion; }
            set { _FechaAnulacion = LibConvert.DateToDbValue(value); }
        }

        public string CodigoCliente {
            get { return _CodigoCliente; }
            set { _CodigoCliente = LibString.Mid(value, 0, 10); }
        }

        public string CodigoCobrador {
            get { return _CodigoCobrador; }
            set { _CodigoCobrador = LibString.Mid(value, 0, 5); }
        }

        public decimal TotalDocumentos {
            get { return _TotalDocumentos; }
            set { _TotalDocumentos = value; }
        }

        public decimal RetencionIslr {
            get { return _RetencionIslr; }
            set { _RetencionIslr = value; }
        }

        public decimal TotalCobrado {
            get { return _TotalCobrado; }
            set { _TotalCobrado = value; }
        }

        public decimal CobradoEfectivo {
            get { return _CobradoEfectivo; }
            set { _CobradoEfectivo = value; }
        }

        public decimal CobradoCheque {
            get { return _CobradoCheque; }
            set { _CobradoCheque = value; }
        }

        public string NumerodelCheque {
            get { return _NumerodelCheque; }
            set { _NumerodelCheque = LibString.Mid(value, 0, 10); }
        }

        public decimal CobradoTarjeta {
            get { return _CobradoTarjeta; }
            set { _CobradoTarjeta = value; }
        }

        public eTipoDeTarjeta CualTarjetaAsEnum {
            get { return _CualTarjeta; }
            set { _CualTarjeta = value; }
        }

        public string CualTarjeta {
            set { _CualTarjeta = (eTipoDeTarjeta)LibConvert.DbValueToEnum(value); }
        }

        public string CualTarjetaAsDB {
            get { return LibConvert.EnumToDbValue((int) _CualTarjeta); }
        }

        public string CualTarjetaAsString {
            get { return LibEnumHelper.GetDescription(_CualTarjeta); }
        }

        public string NroDeLaTarjeta {
            get { return _NroDeLaTarjeta; }
            set { _NroDeLaTarjeta = LibString.Mid(value, 0, 20); }
        }

        public eOrigenFacturacionOManual OrigenAsEnum {
            get { return _Origen; }
            set { _Origen = value; }
        }

        public string Origen {
            set { _Origen = (eOrigenFacturacionOManual)LibConvert.DbValueToEnum(value); }
        }

        public string OrigenAsDB {
            get { return LibConvert.EnumToDbValue((int) _Origen); }
        }

        public string OrigenAsString {
            get { return LibEnumHelper.GetDescription(_Origen); }
        }

        public decimal TotalOtros {
            get { return _TotalOtros; }
            set { _TotalOtros = value; }
        }

        public string NombreBanco {
            get { return _NombreBanco; }
            set { _NombreBanco = LibString.Mid(value, 0, 20); }
        }

        public string CodigoCuentaBancaria {
            get { return _CodigoCuentaBancaria; }
            set { _CodigoCuentaBancaria = LibString.Mid(value, 0, 5); }
        }

        public string CodigoConcepto {
            get { return _CodigoConcepto; }
            set { _CodigoConcepto = LibString.Mid(value, 0, 8); }
        }

        public string Moneda {
            get { return _Moneda; }
            set { _Moneda = LibString.Mid(value, 0, 80); }
        }

        public decimal CambioAbolivares {
            get { return _CambioAbolivares; }
            set { _CambioAbolivares = value; }
        }

        public decimal RetencionIva {
            get { return _RetencionIva; }
            set { _RetencionIva = value; }
        }

        public string NroComprobanteRetIva {
            get { return _NroComprobanteRetIva; }
            set { _NroComprobanteRetIva = LibString.Mid(value, 0, 20); }
        }

        public eStatusRetencionIVACobranza StatusRetencionIVAAsEnum {
            get { return _StatusRetencionIVA; }
            set { _StatusRetencionIVA = value; }
        }

        public string StatusRetencionIVA {
            set { _StatusRetencionIVA = (eStatusRetencionIVACobranza)LibConvert.DbValueToEnum(value); }
        }

        public string StatusRetencionIVAAsDB {
            get { return LibConvert.EnumToDbValue((int) _StatusRetencionIVA); }
        }

        public string StatusRetencionIVAAsString {
            get { return LibEnumHelper.GetDescription(_StatusRetencionIVA); }
        }

        public bool GeneraMovBancarioAsBool {
            get { return _GeneraMovBancario; }
            set { _GeneraMovBancario = value; }
        }

        public string GeneraMovBancario {
            set { _GeneraMovBancario = LibConvert.SNToBool(value); }
        }


        public decimal CobradoAnticipo {
            get { return _CobradoAnticipo; }
            set { _CobradoAnticipo = value; }
        }

        public decimal Vuelto {
            get { return _Vuelto; }
            set { _Vuelto = value; }
        }

        public decimal DescProntoPago {
            get { return _DescProntoPago; }
            set { _DescProntoPago = value; }
        }

        public decimal DescProntoPagoPorc {
            get { return _DescProntoPagoPorc; }
            set { _DescProntoPagoPorc = value; }
        }

        public decimal ComisionVendedor {
            get { return _ComisionVendedor; }
            set { _ComisionVendedor = value; }
        }

        public bool AplicaCreditoBancarioAsBool {
            get { return _AplicaCreditoBancario; }
            set { _AplicaCreditoBancario = value; }
        }

        public string AplicaCreditoBancario {
            set { _AplicaCreditoBancario = LibConvert.SNToBool(value); }
        }


        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = LibString.Mid(value, 0, 4); }
        }

        public int NumeroDeComprobanteISLR {
            get { return _NumeroDeComprobanteISLR; }
            set { _NumeroDeComprobanteISLR = value; }
        }

        public eTipoDeTransaccion TipoDeDocumentoAsEnum {
            get { return _TipoDeDocumento; }
            set { _TipoDeDocumento = value; }
        }

        public string TipoDeDocumento {
            set { _TipoDeDocumento = (eTipoDeTransaccion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeDocumentoAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeDocumento); }
        }

        public string TipoDeDocumentoAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeDocumento); }
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

        public Cobranza() {
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
            StatusCobranzaAsEnum = eStatusCobranza.Vigente;
            Fecha = LibDate.Today();
            FechaAnulacion = LibDate.Today();
            CodigoCliente = string.Empty;
            CodigoCobrador = string.Empty;
            TotalDocumentos = 0;
            RetencionIslr = 0;
            TotalCobrado = 0;
            CobradoEfectivo = 0;
            CobradoCheque = 0;
            NumerodelCheque = string.Empty;
            CobradoTarjeta = 0;
            CualTarjetaAsEnum = eTipoDeTarjeta.Visa;
            NroDeLaTarjeta = string.Empty;
            OrigenAsEnum = eOrigenFacturacionOManual.Factura;
            TotalOtros = 0;
            NombreBanco = string.Empty;
            CodigoCuentaBancaria = string.Empty;
            CodigoConcepto = string.Empty;
            Moneda = string.Empty;
            CambioAbolivares = 0;
            RetencionIva = 0;
            NroComprobanteRetIva = string.Empty;
            StatusRetencionIVAAsEnum = eStatusRetencionIVACobranza.NoAplica;
            GeneraMovBancarioAsBool = false;
            CobradoAnticipo = 0;
            Vuelto = 0;
            DescProntoPago = 0;
            DescProntoPagoPorc = 0;
            ComisionVendedor = 0;
            AplicaCreditoBancarioAsBool = false;
            CodigoMoneda = string.Empty;
            NumeroDeComprobanteISLR = 0;
            TipoDeDocumentoAsEnum = eTipoDeTransaccion.FACTURA;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public Cobranza Clone() {
            Cobranza vResult = new Cobranza();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Numero = _Numero;
            vResult.StatusCobranzaAsEnum = _StatusCobranza;
            vResult.Fecha = _Fecha;
            vResult.FechaAnulacion = _FechaAnulacion;
            vResult.CodigoCliente = _CodigoCliente;
            vResult.CodigoCobrador = _CodigoCobrador;
            vResult.TotalDocumentos = _TotalDocumentos;
            vResult.RetencionIslr = _RetencionIslr;
            vResult.TotalCobrado = _TotalCobrado;
            vResult.CobradoEfectivo = _CobradoEfectivo;
            vResult.CobradoCheque = _CobradoCheque;
            vResult.NumerodelCheque = _NumerodelCheque;
            vResult.CobradoTarjeta = _CobradoTarjeta;
            vResult.CualTarjetaAsEnum = _CualTarjeta;
            vResult.NroDeLaTarjeta = _NroDeLaTarjeta;
            vResult.OrigenAsEnum = _Origen;
            vResult.TotalOtros = _TotalOtros;
            vResult.NombreBanco = _NombreBanco;
            vResult.CodigoCuentaBancaria = _CodigoCuentaBancaria;
            vResult.CodigoConcepto = _CodigoConcepto;
            vResult.Moneda = _Moneda;
            vResult.CambioAbolivares = _CambioAbolivares;
            vResult.RetencionIva = _RetencionIva;
            vResult.NroComprobanteRetIva = _NroComprobanteRetIva;
            vResult.StatusRetencionIVAAsEnum = _StatusRetencionIVA;
            vResult.GeneraMovBancarioAsBool = _GeneraMovBancario;
            vResult.CobradoAnticipo = _CobradoAnticipo;
            vResult.Vuelto = _Vuelto;
            vResult.DescProntoPago = _DescProntoPago;
            vResult.DescProntoPagoPorc = _DescProntoPagoPorc;
            vResult.ComisionVendedor = _ComisionVendedor;
            vResult.AplicaCreditoBancarioAsBool = _AplicaCreditoBancario;
            vResult.CodigoMoneda = _CodigoMoneda;
            vResult.NumeroDeComprobanteISLR = _NumeroDeComprobanteISLR;
            vResult.TipoDeDocumentoAsEnum = _TipoDeDocumento;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNúmero Cobranza = " + _Numero +
               "\nStatus Cobranza = " + _StatusCobranza.ToString() +
               "\nFecha = " + _Fecha.ToShortDateString() +
               "\nFecha Anulacion = " + _FechaAnulacion.ToShortDateString() +
               "\nCodigo Cliente = " + _CodigoCliente +
               "\nCodigo Cobrador = " + _CodigoCobrador +
               "\nTotal Documentos = " + _TotalDocumentos.ToString() +
               "\nRetencion Islr = " + _RetencionIslr.ToString() +
               "\nTotal Cobrado = " + _TotalCobrado.ToString() +
               "\nCobrado Efectivo = " + _CobradoEfectivo.ToString() +
               "\nCobrado Cheque = " + _CobradoCheque.ToString() +
               "\nNumerodel Cheque = " + _NumerodelCheque +
               "\nCobrado Tarjeta = " + _CobradoTarjeta.ToString() +
               "\nCual Tarjeta = " + _CualTarjeta.ToString() +
               "\nNro De La Tarjeta = " + _NroDeLaTarjeta +
               "\nOrigen = " + _Origen.ToString() +
               "\nTotal Otros = " + _TotalOtros.ToString() +
               "\nNombre Banco = " + _NombreBanco +
               "\nCodigo Cuenta Bancaria = " + _CodigoCuentaBancaria +
               "\nCodigo Concepto = " + _CodigoConcepto +
               "\nMoneda = " + _Moneda +
               "\nCambio Abolivares = " + _CambioAbolivares.ToString() +
               "\nRetencion Iva = " + _RetencionIva.ToString() +
               "\nNro Comprobante Ret Iva = " + _NroComprobanteRetIva +
               "\nStatus Retencion IVA = " + _StatusRetencionIVA.ToString() +
               "\nGenera Mov Bancario = " + _GeneraMovBancario +
               "\nCobrado Anticipo = " + _CobradoAnticipo.ToString() +
               "\nVuelto = " + _Vuelto.ToString() +
               "\nDesc Pronto Pago = " + _DescProntoPago.ToString() +
               "\nDesc Pronto Pago Porc = " + _DescProntoPagoPorc.ToString() +
               "\nComision Vendedor = " + _ComisionVendedor.ToString() +
               "\nAplica Credito Bancario = " + _AplicaCreditoBancario +
               "\nCodigo Moneda = " + _CodigoMoneda +
               "\nNumero De Comprobante ISLR = " + _NumeroDeComprobanteISLR.ToString() +
               "\nTipo de Documento = " + _TipoDeDocumento.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Cobranza

} //End of namespace Galac.Adm.Ccl.Venta

