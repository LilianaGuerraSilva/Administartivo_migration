using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Services;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Saw.Dal.Vehiculo {
    [Serializable]
    public class recModelo {
        #region Variables
        private string _Nombre;
        private string _Marca;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Modelo"; }
        }

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 20); }
        }

        public string Marca {
            get { return _Marca; }
            set { _Marca = LibString.Mid(value, 0, 20); }
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
        public recModelo(){
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public recModelo Clone() {
            recModelo vResult = new recModelo();
            vResult.Nombre = _Nombre;
            vResult.Marca = _Marca;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Nombre = " + _Nombre +
               "\nMarca = " + _Marca;
        }

        public void Clear() {
            Nombre = "";
            Marca = "";
            fldTimeStamp = 0;
        }

        public void Fill(XmlDocument refResulset, bool valSetCurrent) {
            _datos = refResulset;
            LibXmlDataParse insParser = new LibXmlDataParse(refResulset);
            if (valSetCurrent && insParser.Count() > 0) {
                Clear();
                Nombre = insParser.GetString(0, "Nombre", Nombre);
                Marca = insParser.GetString(0, "Marca", Marca);
                fldTimeStamp = insParser.GetTimeStamp(0);
            }
        }
        #endregion //Metodos Generados


    } //End of class recModelo

} //End of namespace Galac.Saw.Dal.Vehiculo

