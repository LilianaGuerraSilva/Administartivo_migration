using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Lib;

namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class CxC {
        #region Variables
        private int _ConsecutivoCompania;
        private string _Numero;
        private eStatusCXC _Status;
        private eTipoDeCxC _TipoCxC;
        private string _CodigoCliente;
        private string _NombreCliente;
        private int _ConsecutivoVendedor;
        private string _CodigoVendedor;
        private string _NombreVendedor;
        private eOrigenFacturacionOManual _Origen;
        private DateTime _Fecha;
        private DateTime _FechaCancelacion;
        private DateTime _FechaVencimiento;
        private DateTime _FechaAnulacion;
        private decimal _MontoExento;
        private decimal _MontoGravado;
        private decimal _MontoIVA;
        private decimal _MontoAbonado;
        private string _Descripcion;
        private string _Moneda;
        private decimal _CambioABolivares;
        private bool _SeRetuvoIva;
        private string _NumeroDocumentoOrigen;
        private bool _NoAplicaParaLibroDeVentas;
        private string _CodigoLote;
        private string _CodigoTipoDeDocumentoLey;
        private bool _AplicaDetraccion;
        private string _NumeroDetraccion;
        private string _CodigoDetraccion;
        private string _DescripcionDeDetraccion;
        private decimal _PorcentajeDetraccion;
        private decimal _TotalDetraccion;
        private int _ConsecutivoMovimiento;
        private decimal _TotalOtrosImpuestos;
        private string _CodigoMoneda;
        private string _NumeroControl;
        private string _NumeroComprobanteFiscal;
        private bool _VieneDeCreditoElectronico;
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
            set { _Numero = LibString.Mid(value, 0, 20); }
        }

        public eStatusCXC StatusAsEnum {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Status {
            set { _Status = (eStatusCXC)LibConvert.DbValueToEnum(value); }
        }

        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int) _Status); }
        }

        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
        }

        public eTipoDeCxC TipoCxCAsEnum {
            get { return _TipoCxC; }
            set { _TipoCxC = value; }
        }

        public string TipoCxC {
            set { _TipoCxC = (eTipoDeCxC)LibConvert.DbValueToEnum(value); }
        }

        public string TipoCxCAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoCxC); }
        }

        public string TipoCxCAsString {
            get { return LibEnumHelper.GetDescription(_TipoCxC); }
        }

        public string CodigoCliente {
            get { return _CodigoCliente; }
            set { _CodigoCliente = LibString.Mid(value, 0, 10); }
        }

        public string NombreCliente {
            get { return _NombreCliente; }
            set { _NombreCliente = LibString.Mid(value, 0, 80); }
        }
				
        public string CodigoVendedor {
            get { return _CodigoVendedor; }
            set { _CodigoVendedor = LibString.Mid(value, 0, 5); }
        }

        public int ConsecutivoVendedor {
            get { return _ConsecutivoVendedor; }
            set { _ConsecutivoVendedor = value; }
        }

        public string NombreVendedor {
            get { return _NombreVendedor; }
            set { _NombreVendedor = LibString.Mid(value, 0, 35); }
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

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaCancelacion {
            get { return _FechaCancelacion; }
            set { _FechaCancelacion = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaVencimiento {
            get { return _FechaVencimiento; }
            set { _FechaVencimiento = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaAnulacion {
            get { return _FechaAnulacion; }
            set { _FechaAnulacion = LibConvert.DateToDbValue(value); }
        }

        public decimal MontoExento {
            get { return _MontoExento; }
            set { _MontoExento = value; }
        }

        public decimal MontoGravado {
            get { return _MontoGravado; }
            set { _MontoGravado = value; }
        }

        public decimal MontoIVA {
            get { return _MontoIVA; }
            set { _MontoIVA = value; }
        }

        public decimal MontoAbonado {
            get { return _MontoAbonado; }
            set { _MontoAbonado = value; }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 255); }
        }

        public string Moneda {
            get { return _Moneda; }
            set { _Moneda = LibString.Mid(value, 0, 10); }
        }

        public decimal CambioABolivares {
            get { return _CambioABolivares; }
            set { _CambioABolivares = value; }
        }

        public bool SeRetuvoIvaAsBool {
            get { return _SeRetuvoIva; }
            set { _SeRetuvoIva = value; }
        }

        public string SeRetuvoIva {
            set { _SeRetuvoIva = LibConvert.SNToBool(value); }
        }

        public string NumeroDocumentoOrigen {
            get { return _NumeroDocumentoOrigen; }
            set { _NumeroDocumentoOrigen = LibString.Mid(value, 0, 20); }
        }

        public bool NoAplicaParaLibroDeVentasAsBool {
            get { return _NoAplicaParaLibroDeVentas; }
            set { _NoAplicaParaLibroDeVentas = value; }
        }

        public string NoAplicaParaLibroDeVentas {
            set { _NoAplicaParaLibroDeVentas = LibConvert.SNToBool(value); }
        }

        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = LibString.Mid(value, 0, 10); }
        }

        public string CodigoTipoDeDocumentoLey {
            get { return _CodigoTipoDeDocumentoLey; }
            set { _CodigoTipoDeDocumentoLey = LibString.Mid(value, 0, 10); }
        }

        public bool AplicaDetraccionAsBool {
            get { return _AplicaDetraccion; }
            set { _AplicaDetraccion = value; }
        }

        public string AplicaDetraccion {
            set { _AplicaDetraccion = LibConvert.SNToBool(value); }
        }

        public string NumeroDetraccion {
            get { return _NumeroDetraccion; }
            set { _NumeroDetraccion = LibString.Mid(value, 0, 15); }
        }

        public string CodigoDetraccion {
            get { return _CodigoDetraccion; }
            set { _CodigoDetraccion = LibString.Mid(value, 0, 10); }
        }

        public string DescripcionDeDetraccion {
            get { return _DescripcionDeDetraccion; }
            set { _DescripcionDeDetraccion = LibString.Mid(value, 0, 100); }
        }

        public decimal PorcentajeDetraccion {
            get { return _PorcentajeDetraccion; }
            set { _PorcentajeDetraccion = value; }
        }

        public decimal TotalDetraccion {
            get { return _TotalDetraccion; }
            set { _TotalDetraccion = value; }
        }

        public int ConsecutivoMovimiento {
            get { return _ConsecutivoMovimiento; }
            set { _ConsecutivoMovimiento = value; }
        }

        public decimal TotalOtrosImpuestos {
            get { return _TotalOtrosImpuestos; }
            set { _TotalOtrosImpuestos = value; }
        }

        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = LibString.Mid(value, 0, 4); }
        }

        public string NumeroControl {
            get { return _NumeroControl; }
            set { _NumeroControl = LibString.Mid(value, 0, 11); }
        }

        public string NumeroComprobanteFiscal {
            get { return _NumeroComprobanteFiscal; }
            set { _NumeroComprobanteFiscal = LibString.Mid(value, 0, 12); }
        }

        public bool VieneDeCreditoElectronicoAsBool {
            get { return _VieneDeCreditoElectronico; }
            set { _VieneDeCreditoElectronico = value; }
        }

        public string VieneDeCreditoElectronico {
            set { _VieneDeCreditoElectronico = LibConvert.SNToBool(value); }
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

        #endregion //Constructores

        #region Metodos Generados
        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Numero = string.Empty;
            StatusAsEnum = eStatusCXC.PORCANCELAR;
            TipoCxCAsEnum = eTipoDeCxC.Factura;
            CodigoCliente = string.Empty;
            NombreCliente = string.Empty;
            CodigoVendedor = string.Empty;
            ConsecutivoVendedor = 0;
            NombreVendedor = string.Empty;
            OrigenAsEnum = eOrigenFacturacionOManual.Factura;
            Fecha = LibDate.Today();
            FechaCancelacion = LibDate.Today();
            FechaVencimiento = LibDate.Today();
            FechaAnulacion = LibDate.Today();
            MontoExento = 0;
            MontoGravado = 0;
            MontoIVA = 0;
            MontoAbonado = 0;
            Descripcion = string.Empty;
            Moneda = string.Empty;
            CambioABolivares = 0;
            SeRetuvoIvaAsBool = false;
            NumeroDocumentoOrigen = string.Empty;
            NoAplicaParaLibroDeVentasAsBool = false;
            CodigoLote = string.Empty;
            CodigoTipoDeDocumentoLey = string.Empty;
            AplicaDetraccionAsBool = false;
            NumeroDetraccion = string.Empty;
            CodigoDetraccion = string.Empty;
            DescripcionDeDetraccion = string.Empty;
            PorcentajeDetraccion = 0;
            TotalDetraccion = 0;
            ConsecutivoMovimiento = 0;
            TotalOtrosImpuestos = 0;
            CodigoMoneda = string.Empty;
            NumeroControl = string.Empty;
            NumeroComprobanteFiscal = string.Empty;
            VieneDeCreditoElectronicoAsBool = false;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public CxC Clone() {
            CxC vResult = new CxC();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Numero = _Numero;
            vResult.StatusAsEnum = _Status;
            vResult.TipoCxCAsEnum = _TipoCxC;
            vResult.CodigoCliente = _CodigoCliente;
            vResult.NombreCliente = _NombreCliente;
            vResult.CodigoVendedor = _CodigoVendedor;
            vResult.ConsecutivoVendedor = _ConsecutivoVendedor;
            vResult.NombreVendedor = _NombreVendedor;
            vResult.OrigenAsEnum = _Origen;
            vResult.Fecha = _Fecha;
            vResult.FechaCancelacion = _FechaCancelacion;
            vResult.FechaVencimiento = _FechaVencimiento;
            vResult.FechaAnulacion = _FechaAnulacion;
            vResult.MontoExento = _MontoExento;
            vResult.MontoGravado = _MontoGravado;
            vResult.MontoIVA = _MontoIVA;
            vResult.MontoAbonado = _MontoAbonado;
            vResult.Descripcion = _Descripcion;
            vResult.Moneda = _Moneda;
            vResult.CambioABolivares = _CambioABolivares;
            vResult.SeRetuvoIvaAsBool = _SeRetuvoIva;
            vResult.NumeroDocumentoOrigen = _NumeroDocumentoOrigen;
            vResult.NoAplicaParaLibroDeVentasAsBool = _NoAplicaParaLibroDeVentas;
            vResult.CodigoLote = _CodigoLote;
            vResult.CodigoTipoDeDocumentoLey = _CodigoTipoDeDocumentoLey;
            vResult.AplicaDetraccionAsBool = _AplicaDetraccion;
            vResult.NumeroDetraccion = _NumeroDetraccion;
            vResult.CodigoDetraccion = _CodigoDetraccion;
            vResult.DescripcionDeDetraccion = _DescripcionDeDetraccion;
            vResult.PorcentajeDetraccion = _PorcentajeDetraccion;
            vResult.TotalDetraccion = _TotalDetraccion;
            vResult.ConsecutivoMovimiento = _ConsecutivoMovimiento;
            vResult.TotalOtrosImpuestos = _TotalOtrosImpuestos;
            vResult.CodigoMoneda = _CodigoMoneda;
            vResult.NumeroControl = _NumeroControl;
            vResult.NumeroComprobanteFiscal = _NumeroComprobanteFiscal;
            vResult.VieneDeCreditoElectronicoAsBool = _VieneDeCreditoElectronico;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNúmero = " + _Numero +
               "\nStatus = " + _Status.ToString() +
               "\nTipo Cx C = " + _TipoCxC.ToString() +
               "\nCódigo del Cliente = " + _CodigoCliente +
               "\nCódigo del Vendedor = " + _CodigoVendedor +
               "\nConsecutivo del Vendedor = " + _ConsecutivoVendedor.ToString() +
               "\nOrigen = " + _Origen.ToString() +
               "\nFecha = " + _Fecha.ToShortDateString() +
               "\nFecha de Cancelación = " + _FechaCancelacion.ToShortDateString() +
               "\nFecha de Vencimiento = " + _FechaVencimiento.ToShortDateString() +
               "\nFecha de Anulación = " + _FechaAnulacion.ToShortDateString() +
               "\nMonto Exento = " + _MontoExento.ToString() +
               "\nMonto Gravado = " + _MontoGravado.ToString() +
               "\nMonto IVA = " + _MontoIVA.ToString() +
               "\nMonto Abonado = " + _MontoAbonado.ToString() +
               "\nDescripcion = " + _Descripcion +
               "\nNombre de la Moneda = " + _Moneda +
               "\nCambio ABolivares = " + _CambioABolivares.ToString() +
               "\nSe Retuvo Iva = " + _SeRetuvoIva +
               "\nNumero Documento Origen = " + _NumeroDocumentoOrigen +
               "\nNo Aplica para el Libro de Ventas = " + _NoAplicaParaLibroDeVentas +
               "\nCodigo Lote = " + _CodigoLote +
               "\nTipo de Documento = " + _CodigoTipoDeDocumentoLey +
               "\nAplica Detraccion = " + _AplicaDetraccion +
               "\nNumero Detraccion = " + _NumeroDetraccion +
               "\nCodigo Detraccion = " + _CodigoDetraccion +
               "\nDescripcion De Detraccion = " + _DescripcionDeDetraccion +
               "\nPorcentaje Detraccion = " + _PorcentajeDetraccion.ToString() +
               "\nTotal Detraccion = " + _TotalDetraccion.ToString() +
               "\nConsecutivo Movimiento = " + _ConsecutivoMovimiento.ToString() +
               "\nTotal Otros Impuestos = " + _TotalOtrosImpuestos.ToString() +
               "\nCodigo Moneda = " + _CodigoMoneda +
               "\nNúmero de Control = " + _NumeroControl +
               "\nNumero Comprobante Fiscal = " + _NumeroComprobanteFiscal +
               "\nViene De Credito Electronico = " + _VieneDeCreditoElectronico +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados

    } //End of class CxC

} //End of namespace Galac.Adm.Ccl.Venta

