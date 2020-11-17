using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.CAnticipo;

namespace Galac.Adm.Ccl.CAnticipo {
    [Serializable]
    public class Anticipo {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoAnticipo;
        private eStatusAnticipo _Status;
        private eTipoDeAnticipo _Tipo;
        private DateTime _Fecha;
        private string _Numero;
        private string _CodigoCliente;
        private string _NombreCliente;
        private string _CodigoProveedor;
        private string _NombreProveedor;
        private string _Moneda;
        private decimal _Cambio;
        private bool _GeneraMovBancario;
        private string _CodigoCuentaBancaria;
        private string _NombreCuentaBancaria;
        private string _CodigoConceptoBancario;
        private string _NombreConceptoBancario;
        private bool _GeneraImpuestoBancario;
        private DateTime _FechaAnulacion;
        private DateTime _FechaCancelacion;
        private DateTime _FechaDevolucion;
        private string _Descripcion;
        private decimal _MontoTotal;
        private decimal _MontoUsado;
        private decimal _MontoDevuelto;
        private decimal _MontoDiferenciaEnDevolucion;
        private bool _DiferenciaEsIDB;
        private bool _EsUnaDevolucion;
        private int _NumeroDelAnticipoDevuelto;
        private string _NumeroCheque;
        private bool _AsociarAnticipoACotiz;
        private string _NumeroCotizacion;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private string _CodigoMoneda;
        private string _NombreBeneficiario;
        private int _ConsecutivoRendicion;
        private string _CodigoBeneficiario;
        private bool _AsociarAnticipoACaja;
        private int _ConsecutivoCaja;
        private string _NombreCaja;
        private string _NumeroDeCobranzaAsociado;
        private eGeneradoPor _GeneradoPor;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int ConsecutivoAnticipo {
            get { return _ConsecutivoAnticipo; }
            set { _ConsecutivoAnticipo = value; }
        }

        public eStatusAnticipo StatusAsEnum {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Status {
            set { _Status = (eStatusAnticipo)LibConvert.DbValueToEnum(value); }
        }

        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int) _Status); }
        }

        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
        }

        public eTipoDeAnticipo TipoAsEnum {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        public string Tipo {
            set { _Tipo = (eTipoDeAnticipo)LibConvert.DbValueToEnum(value); }
        }

        public string TipoAsDB {
            get { return LibConvert.EnumToDbValue((int) _Tipo); }
        }

        public string TipoAsString {
            get { return LibEnumHelper.GetDescription(_Tipo); }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value); }
        }

        public string Numero {
            get { return _Numero; }
            set { _Numero = LibString.Mid(value, 0, 20); }
        }

        public string CodigoCliente {
            get { return _CodigoCliente; }
            set { _CodigoCliente = LibString.Mid(value, 0, 10); }
        }

        public string NombreCliente {
            get { return _NombreCliente; }
            set { _NombreCliente = LibString.Mid(value, 0, 80); }
        }

        public string CodigoProveedor {
            get { return _CodigoProveedor; }
            set { _CodigoProveedor = LibString.Mid(value, 0, 10); }
        }

        public string NombreProveedor {
            get { return _NombreProveedor; }
            set { _NombreProveedor = LibString.Mid(value, 0, 60); }
        }

        public string Moneda {
            get { return _Moneda; }
            set { _Moneda = LibString.Mid(value, 0, 10); }
        }

        public decimal Cambio {
            get { return _Cambio; }
            set { _Cambio = value; }
        }

        public bool GeneraMovBancarioAsBool {
            get { return _GeneraMovBancario; }
            set { _GeneraMovBancario = value; }
        }

        public string GeneraMovBancario {
            set { _GeneraMovBancario = LibConvert.SNToBool(value); }
        }


        public string CodigoCuentaBancaria {
            get { return _CodigoCuentaBancaria; }
            set { _CodigoCuentaBancaria = LibString.Mid(value, 0, 5); }
        }

        public string NombreCuentaBancaria {
            get { return _NombreCuentaBancaria; }
            set { _NombreCuentaBancaria = LibString.Mid(value, 0, 40); }
        }

        public string CodigoConceptoBancario {
            get { return _CodigoConceptoBancario; }
            set { _CodigoConceptoBancario = LibString.Mid(value, 0, 8); }
        }

        public string NombreConceptoBancario {
            get { return _NombreConceptoBancario; }
            set { _NombreConceptoBancario = LibString.Mid(value, 0, 30); }
        }

        public bool GeneraImpuestoBancarioAsBool {
            get { return _GeneraImpuestoBancario; }
            set { _GeneraImpuestoBancario = value; }
        }

        public string GeneraImpuestoBancario {
            set { _GeneraImpuestoBancario = LibConvert.SNToBool(value); }
        }


        public DateTime FechaAnulacion {
            get { return _FechaAnulacion; }
            set { _FechaAnulacion = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaCancelacion {
            get { return _FechaCancelacion; }
            set { _FechaCancelacion = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaDevolucion {
            get { return _FechaDevolucion; }
            set { _FechaDevolucion = LibConvert.DateToDbValue(value); }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 255); }
        }

        public decimal MontoTotal {
            get { return _MontoTotal; }
            set { _MontoTotal = value; }
        }

        public decimal MontoUsado {
            get { return _MontoUsado; }
            set { _MontoUsado = value; }
        }

        public decimal MontoDevuelto {
            get { return _MontoDevuelto; }
            set { _MontoDevuelto = value; }
        }

        public decimal MontoDiferenciaEnDevolucion {
            get { return _MontoDiferenciaEnDevolucion; }
            set { _MontoDiferenciaEnDevolucion = value; }
        }

        public bool DiferenciaEsIDBAsBool {
            get { return _DiferenciaEsIDB; }
            set { _DiferenciaEsIDB = value; }
        }

        public string DiferenciaEsIDB {
            set { _DiferenciaEsIDB = LibConvert.SNToBool(value); }
        }


        public bool EsUnaDevolucionAsBool {
            get { return _EsUnaDevolucion; }
            set { _EsUnaDevolucion = value; }
        }

        public string EsUnaDevolucion {
            set { _EsUnaDevolucion = LibConvert.SNToBool(value); }
        }


        public int NumeroDelAnticipoDevuelto {
            get { return _NumeroDelAnticipoDevuelto; }
            set { _NumeroDelAnticipoDevuelto = value; }
        }

        public string NumeroCheque {
            get { return _NumeroCheque; }
            set { _NumeroCheque = LibString.Mid(value, 0, 15); }
        }

        public bool AsociarAnticipoACotizAsBool {
            get { return _AsociarAnticipoACotiz; }
            set { _AsociarAnticipoACotiz = value; }
        }

        public string AsociarAnticipoACotiz {
            set { _AsociarAnticipoACotiz = LibConvert.SNToBool(value); }
        }


        public string NumeroCotizacion {
            get { return _NumeroCotizacion; }
            set { _NumeroCotizacion = LibString.Mid(value, 0, 11); }
        }

        //public int ConsecutivoRendicion {
        //    get { return _ConsecutivoRendicion; }
        //    set { _ConsecutivoRendicion = value; }
       // }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 10); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
        }

        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = LibString.Mid(value, 0, 4); }
        }

        public string NombreBeneficiario {
            get { return _NombreBeneficiario; }
            set { _NombreBeneficiario = LibString.Mid(value, 0, 60); }
        }

        public int ConsecutivoRendicion {
            get { return _ConsecutivoRendicion; }
            set { _ConsecutivoRendicion = value; }
        }

        public string CodigoBeneficiario {
            get { return _CodigoBeneficiario; }
            set { _CodigoBeneficiario = LibString.Mid(value, 0, 10); }
        }

        public bool AsociarAnticipoACajaAsBool {
            get { return _AsociarAnticipoACaja; }
            set { _AsociarAnticipoACaja = value; }
        }

        public string AsociarAnticipoACaja {
            set { _AsociarAnticipoACaja = LibConvert.SNToBool(value); }
        }


        public int ConsecutivoCaja {
            get { return _ConsecutivoCaja; }
            set { _ConsecutivoCaja = value; }
        }

        public string NombreCaja {
            get { return _NombreCaja; }
            set { _NombreCaja = LibString.Mid(value, 0, 60); }
        }

        public string NumeroDeCobranzaAsociado {
            get { return _NumeroDeCobranzaAsociado; }
            set { _NumeroDeCobranzaAsociado = LibString.Mid(value, 0, 12); }
        }

        public eGeneradoPor GeneradoPorAsEnum {
            get { return _GeneradoPor; }
            set { _GeneradoPor = value; }
        }

        public string GeneradoPor {
            set { _GeneradoPor = (eGeneradoPor)LibConvert.DbValueToEnum(value); }
        }

        public string GeneradoPorAsDB {
            get { return LibConvert.EnumToDbValue((int) _GeneradoPor); }
        }

        public string GeneradoPorAsString {
            get { return LibEnumHelper.GetDescription(_GeneradoPor); }
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

        public Anticipo() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ConsecutivoAnticipo = 0;
            StatusAsEnum = eStatusAnticipo.Vigente;
            TipoAsEnum = eTipoDeAnticipo.Cobrado;
            Fecha = LibDate.Today();
            Numero = string.Empty;
            CodigoCliente = string.Empty;
            NombreCliente = string.Empty;
            CodigoProveedor = string.Empty;
            NombreProveedor = string.Empty;
            Moneda = string.Empty;
            Cambio = 0;
            GeneraMovBancarioAsBool = false;
            CodigoCuentaBancaria = string.Empty;
            NombreCuentaBancaria = string.Empty;
            CodigoConceptoBancario = string.Empty;
            NombreConceptoBancario = string.Empty;
            GeneraImpuestoBancarioAsBool = false;
            FechaAnulacion = LibDate.Today();
            FechaCancelacion = LibDate.Today();
            FechaDevolucion = LibDate.Today();
            Descripcion = string.Empty;
            MontoTotal = 0;
            MontoUsado = 0;
            MontoDevuelto = 0;
            MontoDiferenciaEnDevolucion = 0;
            DiferenciaEsIDBAsBool = false;
            EsUnaDevolucionAsBool = false;
            NumeroDelAnticipoDevuelto = 0;
            NumeroCheque = string.Empty;
            AsociarAnticipoACotizAsBool = false;
            NumeroCotizacion = string.Empty;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            CodigoMoneda = string.Empty;
            NombreBeneficiario = string.Empty;
            ConsecutivoRendicion = 0;
            CodigoBeneficiario = string.Empty;
            AsociarAnticipoACajaAsBool = false;
            ConsecutivoCaja = 0;
            NombreCaja = string.Empty;
            NumeroDeCobranzaAsociado = string.Empty;
            GeneradoPorAsEnum = eGeneradoPor.Usuario;
            fldTimeStamp = 0;
        }

        public Anticipo Clone() {
            Anticipo vResult = new Anticipo();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoAnticipo = _ConsecutivoAnticipo;
            vResult.StatusAsEnum = _Status;
            vResult.TipoAsEnum = _Tipo;
            vResult.Fecha = _Fecha;
            vResult.Numero = _Numero;
            vResult.CodigoCliente = _CodigoCliente;
            vResult.NombreCliente = _NombreCliente;
            vResult.CodigoProveedor = _CodigoProveedor;
            vResult.NombreProveedor = _NombreProveedor;
            vResult.Moneda = _Moneda;
            vResult.Cambio = _Cambio;
            vResult.GeneraMovBancarioAsBool = _GeneraMovBancario;
            vResult.CodigoCuentaBancaria = _CodigoCuentaBancaria;
            vResult.NombreCuentaBancaria = _NombreCuentaBancaria;
            vResult.CodigoConceptoBancario = _CodigoConceptoBancario;
            vResult.NombreConceptoBancario = _NombreConceptoBancario;
            vResult.GeneraImpuestoBancarioAsBool = _GeneraImpuestoBancario;
            vResult.FechaAnulacion = _FechaAnulacion;
            vResult.FechaCancelacion = _FechaCancelacion;
            vResult.FechaDevolucion = _FechaDevolucion;
            vResult.Descripcion = _Descripcion;
            vResult.MontoTotal = _MontoTotal;
            vResult.MontoUsado = _MontoUsado;
            vResult.MontoDevuelto = _MontoDevuelto;
            vResult.MontoDiferenciaEnDevolucion = _MontoDiferenciaEnDevolucion;
            vResult.DiferenciaEsIDBAsBool = _DiferenciaEsIDB;
            vResult.EsUnaDevolucionAsBool = _EsUnaDevolucion;
            vResult.NumeroDelAnticipoDevuelto = _NumeroDelAnticipoDevuelto;
            vResult.NumeroCheque = _NumeroCheque;
            vResult.AsociarAnticipoACotizAsBool = _AsociarAnticipoACotiz;
            vResult.NumeroCotizacion = _NumeroCotizacion;

            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.CodigoMoneda = _CodigoMoneda;
            vResult.NombreBeneficiario = _NombreBeneficiario;
            vResult.ConsecutivoRendicion = _ConsecutivoRendicion;
            vResult.CodigoBeneficiario = _CodigoBeneficiario;
            vResult.AsociarAnticipoACajaAsBool = _AsociarAnticipoACaja;
            vResult.ConsecutivoCaja = _ConsecutivoCaja;
            vResult.NombreCaja = _NombreCaja;
            vResult.NumeroDeCobranzaAsociado = _NumeroDeCobranzaAsociado;
            vResult.GeneradoPorAsEnum = _GeneradoPor;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Anticipo = " + _ConsecutivoAnticipo.ToString() +
               "\nStatus = " + _Status.ToString() +
               "\nTipo = " + _Tipo.ToString() +
               "\nFecha = " + _Fecha.ToShortDateString() +
               "\nNumero = " + _Numero +
               "\nCódigo del Proveedor = " + _CodigoCliente +
               "\nCódigo del Proveedor = " + _CodigoProveedor +
               "\nNombre de la Moneda = " + _Moneda +
               "\nCambio = " + _Cambio.ToString() +
               "\nGenera Mov Bancario = " + _GeneraMovBancario +
               "\nCuenta Bancaria = " + _CodigoCuentaBancaria +
               "\nCódigo Concepto = " + _CodigoConceptoBancario +
               "\nGenera Impuesto Bancario = " + _GeneraImpuestoBancario +
               "\nFecha Anulacion = " + _FechaAnulacion.ToShortDateString() +
               "\nFecha Cancelacion = " + _FechaCancelacion.ToShortDateString() +
               "\nFecha Devolucion = " + _FechaDevolucion.ToShortDateString() +
               "\nDescripcion = " + _Descripcion +
               "\nMonto Total = " + _MontoTotal.ToString() +
               "\nMonto Usado = " + _MontoUsado.ToString() +
               "\nMonto Devuelto = " + _MontoDevuelto.ToString() +
               "\nMonto Diferencia En Devolucion = " + _MontoDiferenciaEnDevolucion.ToString() +
               "\nDiferencia Es IDB = " + _DiferenciaEsIDB +
               "\nEs Una Devolución = " + _EsUnaDevolucion +
               "\nNumero Del Anticipo Devuelto = " + _NumeroDelAnticipoDevuelto.ToString() +
               "\nNúmero Cheque = " + _NumeroCheque +
               "\nAsociar Anticipo ACotiz = " + _AsociarAnticipoACotiz +
               "\nNúmero de Cot. Asociada = " + _NumeroCotizacion +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString() +
               "\nCodigo Moneda = " + _CodigoMoneda +
               "\nNombre Beneficiario = " + _NombreBeneficiario +
               "\nConsecutivo Rendicion = " + _ConsecutivoRendicion.ToString() +
               "\nCodigo Beneficiario = " + _CodigoBeneficiario +
               "\nAsociar Anticipo ACaja = " + _AsociarAnticipoACaja +
               "\nCaja = " + _ConsecutivoCaja.ToString() +
               "\nNumero De Cobranza Asociado = " + _NumeroDeCobranzaAsociado +
               "\nGenerado Por = " + _GeneradoPor.ToString();
        }
        #endregion //Metodos Generados


    } //End of class Anticipo

} //End of namespace Galac.Adm.Ccl.CAnticipo

