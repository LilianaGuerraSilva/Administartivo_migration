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
    public class UrbanizacionZP {
        #region Variables
        private string _Urbanizacion;
        private string _UrbanizacionOriginal;
        private string _ZonaPostal;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string Urbanizacion {
            get { return _Urbanizacion; }
            set { _Urbanizacion = LibString.Mid(value, 0, 30); }
        }
        public string UrbanizacionOriginal {
            get { return _UrbanizacionOriginal; }
            set { _UrbanizacionOriginal = LibString.Mid(value, 0, 30); }
        }

        public string ZonaPostal {
            get { return _ZonaPostal; }
            set { _ZonaPostal = LibString.Mid(value, 0, 7); }
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

        public UrbanizacionZP() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            Urbanizacion = string.Empty;
            ZonaPostal = string.Empty;
            fldTimeStamp = 0;
        }

        public UrbanizacionZP Clone() {
            UrbanizacionZP vResult = new UrbanizacionZP();
            vResult.Urbanizacion = _Urbanizacion;
            vResult.ZonaPostal = _ZonaPostal;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Urbanización = " + _Urbanizacion +
               "\nZona Postal = " + _ZonaPostal;
        }
        #endregion //Metodos Generados


    } //End of class UrbanizacionZP

} //End of namespace Galac.Saw.Ccl.Tablas

