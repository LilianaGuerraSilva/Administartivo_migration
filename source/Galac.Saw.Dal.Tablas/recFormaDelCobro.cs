using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Services;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Saw.Dal.Tablas {
    [Serializable]
    public class recFormaDelCobro {
        #region Variables
        private string _Codigo;
        private string _Nombre;
        private eTipoDeFormaDePago _TipoDePago;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Forma Del Cobro"; }
        }

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 5); }
        }

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 50); }
        }

        public eTipoDeFormaDePago TipoDePago {
            get { return _TipoDePago; }
            set { _TipoDePago = value; }
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
        public recFormaDelCobro(){
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public recFormaDelCobro Clone() {
            recFormaDelCobro vResult = new recFormaDelCobro();
            vResult.Codigo = _Codigo;
            vResult.Nombre = _Nombre;
            vResult.TipoDePago = _TipoDePago;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Código = " + _Codigo +
               "\nNombre = " + _Nombre +
               "\nTipo De Pago = " + _TipoDePago.ToString();
        }

        public void Clear() {
            Codigo = "";
            Nombre = "";
            TipoDePago = eTipoDeFormaDePago.Efectivo;
            fldTimeStamp = 0;
        }

        public void Fill(XmlDocument refResulset, bool valSetCurrent) {
            _datos = refResulset;
            LibXmlDataParse insParser = new LibXmlDataParse(refResulset);
            if (valSetCurrent && insParser.Count() > 0) {
                Clear();
                Codigo = insParser.GetString(0, "Codigo", Codigo);
                Nombre = insParser.GetString(0, "Nombre", Nombre);
                TipoDePago = (eTipoDeFormaDePago) insParser.GetEnum(0, "TipoDePago", (int) TipoDePago);
                fldTimeStamp = insParser.GetTimeStamp(0);
            }
        }
        #endregion //Metodos Generados


    } //End of class recFormaDelCobro

} //End of namespace Galac.Saw.Dal.Tablas

