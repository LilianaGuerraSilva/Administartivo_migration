using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Ccl.Tablas {
    [Serializable]
    public class FormaDelCobro {
        #region Variables
        private string _Codigo;
        private string _Nombre;
        private eTipoDeFormaDePago _TipoDePago;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 5); }
        }

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 50); }
        }

        public eTipoDeFormaDePago TipoDePagoAsEnum {
            get { return _TipoDePago; }
            set { _TipoDePago = value; }
        }

        public string TipoDePago {
            set { _TipoDePago = (eTipoDeFormaDePago)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDePagoAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDePago); }
        }

        public string TipoDePagoAsString {
            get { return LibEnumHelper.GetDescription(_TipoDePago); }
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
            Codigo = string.Empty;
            Nombre = string.Empty;
            TipoDePagoAsEnum = eTipoDeFormaDePago.Efectivo;
            fldTimeStamp = 0;
        }

        public FormaDelCobro Clone() {
            FormaDelCobro vResult = new FormaDelCobro();
            vResult.Codigo = _Codigo;
            vResult.Nombre = _Nombre;
            vResult.TipoDePagoAsEnum = _TipoDePago;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Código = " + _Codigo +
               "\nNombre = " + _Nombre +
               "\nTipo De Pago = " + _TipoDePago.ToString();
        }
        #endregion //Metodos Generados


    } //End of class FormaDelCobro

} //End of namespace Galac.Saw.Ccl.Tablas

