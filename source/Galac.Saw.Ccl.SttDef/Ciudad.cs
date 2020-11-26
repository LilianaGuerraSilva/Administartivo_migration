using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Ccl.SttDef {
    [Serializable]
    public class Ciudad {
        private string _GroupName = null;
        private string _Module = null;

        public string GroupName {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        public string Module {
            get { return _Module; }
            set { _Module = value; }
        }
        #region Variables
        private string _NombreCiudad;
        private string _NombreCiudadOriginal;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string NombreCiudad {
            get { return _NombreCiudad; }
            set { _NombreCiudad = LibString.Mid(value, 0, 100); }
        }

        public string NombreCiudadOriginal {
            get { return _NombreCiudadOriginal; }
            set { _NombreCiudadOriginal = LibString.Mid(value, 0, 100); }
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

        public Ciudad() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            NombreCiudad = "";
            fldTimeStamp = 0;
        }

        public Ciudad Clone() {
            Ciudad vResult = new Ciudad();
            vResult.NombreCiudad = _NombreCiudad;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Nombre de la Ciudad = " + _NombreCiudad;
        }
        #endregion //Metodos Generados


    } //End of class Ciudad

} //End of namespace Galac.Dbo.Ccl.Ciudad

