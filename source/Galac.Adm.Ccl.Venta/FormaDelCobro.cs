using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class FormaDelCobro {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Codigo;
        private string _Nombre;
        private eFormaDeCobro _TipoDeCobro;
        private string _CodigoCuentaBancaria;
        private string _NombreCuentaBancaria;
        private string _CodigoTheFactory;
        private eOrigen _Origen;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 5); }
        }

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 50); }
        }

        public eFormaDeCobro TipoDePagoAsEnum {
            get { return _TipoDeCobro; }
            set { _TipoDeCobro = value; }
        }

        public string TipoDePago {
            set { _TipoDeCobro = (eFormaDeCobro)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDePagoAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeCobro); }
        }

        public string TipoDePagoAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeCobro); }
        }

        public string CodigoCuentaBancaria {
            get { return _CodigoCuentaBancaria; }
            set { _CodigoCuentaBancaria = LibString.Mid(value, 0, 5); }
        }

        public string NombreCuentaBancaria {
            get { return _NombreCuentaBancaria; }
            set { _NombreCuentaBancaria = LibString.Mid(value, 0, 40); }
        }

        public string CodigoTheFactory {
            get { return _CodigoTheFactory; }
            set { _CodigoTheFactory = LibString.Mid(value, 0, 2); }
        }

        public eOrigen OrigenAsEnum {
            get { return _Origen; }
            set { _Origen = value; }
        }

        public string Origen {
            set { _Origen = (eOrigen)LibConvert.DbValueToEnum(value); }
        }

        public string OrigenAsDB {
            get { return LibConvert.EnumToDbValue((int) _Origen); }
        }

        public string OrigenAsString {
            get { return LibEnumHelper.GetDescription(_Origen); }
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

        public FormaDelCobro() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            Consecutivo = 0;
            Codigo = string.Empty;
            Nombre = string.Empty;
            TipoDePagoAsEnum = eFormaDeCobro.Efectivo;
            CodigoCuentaBancaria = string.Empty;
            NombreCuentaBancaria = string.Empty;
            CodigoTheFactory = string.Empty;
            OrigenAsEnum = eOrigen.Sistema;
            fldTimeStamp = 0;
        }

        public FormaDelCobro Clone() {
            FormaDelCobro vResult = new FormaDelCobro();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Codigo = _Codigo;
            vResult.Nombre = _Nombre;
            vResult.TipoDePagoAsEnum = _TipoDeCobro;
            vResult.CodigoCuentaBancaria = _CodigoCuentaBancaria;
            vResult.NombreCuentaBancaria = _NombreCuentaBancaria;
            vResult.CodigoTheFactory = _CodigoTheFactory;
            vResult.OrigenAsEnum = _Origen;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nCódigo = " + _Codigo +
               "\nNombre = " + _Nombre +
               "\nTipo De Pago = " + _TipoDeCobro.ToString() +
               "\nCuenta Bancaria = " + _CodigoCuentaBancaria +
               "\nCodigo The Factory = " + _CodigoTheFactory +
               "\nOrigen = " + _Origen.ToString();
        }
        #endregion //Metodos Generados


    } //End of class FormaDelCobro

} //End of namespace Galac.Adm.Ccl.Venta

