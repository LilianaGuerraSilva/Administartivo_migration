using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Ccl.CajaChica {
    [Serializable]
    public class Anticipo:INotifyPropertyChanged {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoAnticipo;
        private eStatusAnticipo _Status;
        private eTipoDeAnticipo _Tipo;
        private DateTime _Fecha;
        private string _Numero;
        private string _CodigoBeneficiario;
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
        private int _ConsecutivoRendicion;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value;
            PropertyChanged(this, new PropertyChangedEventArgs("ConsecutivoCompania"));
            }
        }

        public int ConsecutivoAnticipo {
            get { return _ConsecutivoAnticipo; }
            set { _ConsecutivoAnticipo = value;
            PropertyChanged(this, new PropertyChangedEventArgs("ConsecutivoAnticipo"));
            }
        }

        public eStatusAnticipo StatusAsEnum {
            get { return _Status; }
            set { _Status = value;
            PropertyChanged(this, new PropertyChangedEventArgs("StatusAsEnum"));
            }
        }

        public string Status {
            set { _Status = (eStatusAnticipo)LibConvert.DbValueToEnum(value);
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }

        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int) _Status); }
        }

        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
        }

        public eTipoDeAnticipo TipoAsEnum {
            get { return _Tipo; }
            set { _Tipo = value;
            PropertyChanged(this, new PropertyChangedEventArgs("TipoAsEnum"));
            }
        }

        public string Tipo {
            set { _Tipo = (eTipoDeAnticipo)LibConvert.DbValueToEnum(value);
            PropertyChanged(this, new PropertyChangedEventArgs("Tipo"));
            }
        }

        public string TipoAsDB {
            get { return LibConvert.EnumToDbValue((int) _Tipo); }
        }

        public string TipoAsString {
            get { return LibEnumHelper.GetDescription(_Tipo); }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value);
            PropertyChanged(this, new PropertyChangedEventArgs("Fecha"));
            }
        }

        public string Numero {
            get { return _Numero; }
            set { _Numero = LibString.Mid(value, 0, 20);
            PropertyChanged(this, new PropertyChangedEventArgs("Numero"));
            }
        }

        public string CodigoBeneficiario {
            get { return _CodigoBeneficiario; }
            set { _CodigoBeneficiario = LibString.Mid(value, 0, 10);
            PropertyChanged(this, new PropertyChangedEventArgs("CodigoBeneficiario"));
            }
        }

        public string CodigoCliente {
            get { return _CodigoCliente; }
            set { _CodigoCliente = LibString.Mid(value, 0, 10);
            PropertyChanged(this, new PropertyChangedEventArgs("CodigoCliente"));
            }
        }

        public string NombreCliente {
            get { return _NombreCliente; }
            set { _NombreCliente = LibString.Mid(value, 0, 80);
            PropertyChanged(this, new PropertyChangedEventArgs("NombreCliente"));
            }
        }

        public string CodigoProveedor {
            get { return _CodigoProveedor; }
            set { _CodigoProveedor = LibString.Mid(value, 0, 10);
            PropertyChanged(this, new PropertyChangedEventArgs("CodigoProveedor"));
            }
        }

        public string NombreProveedor {
            get { return _NombreProveedor; }
            set { _NombreProveedor = LibString.Mid(value, 0, 60);
            PropertyChanged(this, new PropertyChangedEventArgs("NombreProveedor"));
            }
        }

        public string Moneda {
            get { return _Moneda; }
            set { _Moneda = LibString.Mid(value, 0, 80);
            PropertyChanged(this, new PropertyChangedEventArgs("Moneda"));
            }
        }

        public decimal Cambio {
            get { return _Cambio; }
            set { _Cambio = value;
            PropertyChanged(this, new PropertyChangedEventArgs("Cambio"));
            }
        }

        public bool GeneraMovBancarioAsBool {
            get { return _GeneraMovBancario; }
            set { _GeneraMovBancario = value;
            PropertyChanged(this, new PropertyChangedEventArgs("GenerarMovBancarioAsBool"));
            }
        }

        public string GeneraMovBancario {
            set { _GeneraMovBancario = LibConvert.SNToBool(value);
            PropertyChanged(this, new PropertyChangedEventArgs("GeneraMovBancario"));
            }
        }


        public string CodigoCuentaBancaria {
            get { return _CodigoCuentaBancaria; }
            set { _CodigoCuentaBancaria = LibString.Mid(value, 0, 5);
            PropertyChanged(this, new PropertyChangedEventArgs("CodigoCuentaBancaria"));
            }
        }

        public string NombreCuentaBancaria {
            get { return _NombreCuentaBancaria; }
            set { _NombreCuentaBancaria = LibString.Mid(value, 0, 40);
            PropertyChanged(this, new PropertyChangedEventArgs("NombreCuentaBancaria"));
            }
        }

        public string CodigoConceptoBancario {
            get { return _CodigoConceptoBancario; }
            set { _CodigoConceptoBancario = LibString.Mid(value, 0, 8);
            PropertyChanged(this, new PropertyChangedEventArgs("CodigoConceptoBancario"));
            }
        }

        public string NombreConceptoBancario {
            get { return _NombreConceptoBancario; }
            set { _NombreConceptoBancario = LibString.Mid(value, 0, 30);
            PropertyChanged(this, new PropertyChangedEventArgs("NombreConceptoBancario"));
            }
        }

        public bool GeneraImpuestoBancarioAsBool {
            get { return _GeneraImpuestoBancario; }
            set { _GeneraImpuestoBancario = value;
            PropertyChanged(this, new PropertyChangedEventArgs("GeneraImpuestoBancarioAsBool"));
            }
        }

        public string GeneraImpuestoBancario {
            set { _GeneraImpuestoBancario = LibConvert.SNToBool(value);
            PropertyChanged(this, new PropertyChangedEventArgs("GeneraImpuestoBancario"));
            }
        }


        public DateTime FechaAnulacion {
            get { return _FechaAnulacion; }
            set { _FechaAnulacion = LibConvert.DateToDbValue(value);
            PropertyChanged(this, new PropertyChangedEventArgs("FechaAnulacion"));
            }
        }

        public DateTime FechaCancelacion {
            get { return _FechaCancelacion; }
            set { _FechaCancelacion = LibConvert.DateToDbValue(value);
            PropertyChanged(this, new PropertyChangedEventArgs("FechaCancelacion"));
            }
        }

        public DateTime FechaDevolucion {
            get { return _FechaDevolucion; }
            set { _FechaDevolucion = LibConvert.DateToDbValue(value);
            PropertyChanged(this, new PropertyChangedEventArgs("FechaDevolucion"));
            }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 255);
            PropertyChanged(this, new PropertyChangedEventArgs("Descripcion"));
            }
        }

        public decimal MontoTotal {
            get { return _MontoTotal; }
            set { _MontoTotal = value;
            PropertyChanged(this, new PropertyChangedEventArgs("MontoTotal"));
            }
        }

        public decimal MontoUsado {
            get { return _MontoUsado; }
            set { _MontoUsado = value;
            PropertyChanged(this, new PropertyChangedEventArgs("MontoUsado"));
            }
        }

        public decimal MontoDevuelto {
            get { return _MontoDevuelto; }
            set { _MontoDevuelto = value;
            PropertyChanged(this, new PropertyChangedEventArgs("MontoDevuelto"));
            }
        }

        public decimal MontoDiferenciaEnDevolucion {
            get { return _MontoDiferenciaEnDevolucion; }
            set { _MontoDiferenciaEnDevolucion = value;
            PropertyChanged(this, new PropertyChangedEventArgs("MontoDiferenciaEnDevolucion"));
            }
        }

        public bool DiferenciaEsIDBAsBool {
            get { return _DiferenciaEsIDB; }
            set { _DiferenciaEsIDB = value;
            PropertyChanged(this, new PropertyChangedEventArgs("GeneraImpuestoBancario"));
            }
        }

        public string DiferenciaEsIDB {
            set { _DiferenciaEsIDB = LibConvert.SNToBool(value);
            PropertyChanged(this, new PropertyChangedEventArgs("DiferenciaEsIDB"));
            }
        }


        public bool EsUnaDevolucionAsBool {
            get { return _EsUnaDevolucion; }
            set { _EsUnaDevolucion = value;
            PropertyChanged(this, new PropertyChangedEventArgs("EsUnaDevolucionAsBool"));
            }
        }

        public string EsUnaDevolucion {
            set { _EsUnaDevolucion = LibConvert.SNToBool(value);
            PropertyChanged(this, new PropertyChangedEventArgs("EsUnaDevolucion"));
            }
        }


        public int NumeroDelAnticipoDevuelto {
            get { return _NumeroDelAnticipoDevuelto; }
            set { _NumeroDelAnticipoDevuelto = value;
            PropertyChanged(this, new PropertyChangedEventArgs("NumeroDelAnticipoDevuelto"));
            }
        }

        public string NumeroCheque {
            get { return _NumeroCheque; }
            set { _NumeroCheque = LibString.Mid(value, 0, 15);
            PropertyChanged(this, new PropertyChangedEventArgs("NumeroCheque"));
            }
        }

        public bool AsociarAnticipoACotizAsBool {
            get { return _AsociarAnticipoACotiz; }
            set { _AsociarAnticipoACotiz = value;
            PropertyChanged(this, new PropertyChangedEventArgs("AsociarAnticipoACotizAsBool"));
            }
        }

        public string AsociarAnticipoACotiz {
            set { _AsociarAnticipoACotiz = LibConvert.SNToBool(value);
            PropertyChanged(this, new PropertyChangedEventArgs("AsociarAnticipoACotiz"));
            }
        }


        public string NumeroCotizacion {
            get { return _NumeroCotizacion; }
            set { _NumeroCotizacion = LibString.Mid(value, 0, 11);
            PropertyChanged(this, new PropertyChangedEventArgs("NumeroCotizacion"));
            }
        }

        public int ConsecutivoRendicion {
            get { return _ConsecutivoRendicion; }
            set { _ConsecutivoRendicion = value;
            PropertyChanged(this, new PropertyChangedEventArgs("ConsecutivoRendicion"));
            }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 10);
            PropertyChanged(this, new PropertyChangedEventArgs("NombreOperador"));
            }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value);
            PropertyChanged(this, new PropertyChangedEventArgs("FechaUltimaModificacion"));
            }
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
            PropertyChanged = new PropertyChangedEventHandler(handler);
        }


        void handler(object o, PropertyChangedEventArgs arg) { 
        
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            ConsecutivoAnticipo = 0;
            StatusAsEnum = eStatusAnticipo.Vigente;
            TipoAsEnum = eTipoDeAnticipo.Cobrado;
            Fecha = LibDate.Today();
            Numero = "";
            CodigoBeneficiario = "";
            CodigoCliente = "";
            NombreCliente = "";
            CodigoProveedor = "";
            NombreProveedor = "";
            Moneda = "";
            Cambio = 0;
            GeneraMovBancarioAsBool = false;
            CodigoCuentaBancaria = "";
            NombreCuentaBancaria = "";
            CodigoConceptoBancario = "";
            NombreConceptoBancario = "";
            GeneraImpuestoBancarioAsBool = false;
            FechaAnulacion = LibDate.Today();
            FechaCancelacion = LibDate.Today();
            FechaDevolucion = LibDate.Today();
            Descripcion = "";
            MontoTotal = 0;
            MontoUsado = 0;
            MontoDevuelto = 0;
            MontoDiferenciaEnDevolucion = 0;
            DiferenciaEsIDBAsBool = false;
            EsUnaDevolucionAsBool = false;
            NumeroDelAnticipoDevuelto = 0;
            NumeroCheque = "";
            AsociarAnticipoACotizAsBool = false;
            NumeroCotizacion = "";
            ConsecutivoRendicion = 0;
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
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
            vResult.CodigoBeneficiario = _CodigoBeneficiario;
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
            vResult.ConsecutivoRendicion = _ConsecutivoRendicion;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
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
               "\nCódigo del        Beneficiario = " + _CodigoBeneficiario +
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
               "\nRendicion = " + _ConsecutivoRendicion.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados



        public event PropertyChangedEventHandler PropertyChanged;
    } //End of class Anticipo

} //End of namespace Galac.Adm.Ccl.CajaChica

