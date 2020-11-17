using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.Vehiculo;

namespace Galac.Saw.Ccl.Vehiculo {
    [Serializable]
    public class Modelo {
        #region Variables
        private string _Nombre;
        private string _NombreOriginal;
        private string _Marca;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 20); }
        }

        public string Marca {
            get { return _Marca; }
            set { _Marca = LibString.Mid(value, 0, 20); }
        }
       
        public string NombreOriginal {
            get { return _NombreOriginal; }
            set { _NombreOriginal = LibString.Mid(value, 0, 20); }
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

        public Modelo() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            Nombre = string.Empty;
            Marca = string.Empty;
            fldTimeStamp = 0;
        }

        public Modelo Clone() {
            Modelo vResult = new Modelo();
            vResult.Nombre = _Nombre;
            vResult.Marca = _Marca;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Nombre = " + _Nombre +
               "\nMarca = " + _Marca;
        }
        #endregion //Metodos Generados


    } //End of class Modelo

} //End of namespace Galac.Saw.Ccl.Vehiculo

