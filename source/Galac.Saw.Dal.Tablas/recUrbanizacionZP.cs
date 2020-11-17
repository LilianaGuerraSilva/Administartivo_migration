using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Services;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Saw.Dal.Tablas {
    [Serializable]
    public class recUrbanizacionZP {
        #region Variables
        private string _Urbanizacion;
        private string _UrbanizacionOriginal;
        private string _ZonaPostal;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Urbanización - Zona Postal"; }
        }

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
        public recUrbanizacionZP(){
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public recUrbanizacionZP Clone() {
            recUrbanizacionZP vResult = new recUrbanizacionZP();
            vResult.Urbanizacion = _Urbanizacion;
            vResult.ZonaPostal = _ZonaPostal;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Urbanización = " + _Urbanizacion +
               "\nZona Postal = " + _ZonaPostal;
        }

        public void Clear() {
            Urbanizacion = "";
            ZonaPostal = "";
            fldTimeStamp = 0;
        }

        public void Fill(XmlDocument refResulset, bool valSetCurrent) {
            _datos = refResulset;
            LibXmlDataParse insParser = new LibXmlDataParse(refResulset);
            if (valSetCurrent && insParser.Count() > 0) {
                Clear();
                Urbanizacion = insParser.GetString(0, "Urbanizacion", Urbanizacion);
                ZonaPostal = insParser.GetString(0, "ZonaPostal", ZonaPostal);
                fldTimeStamp = insParser.GetTimeStamp(0);
            }
        }
        #endregion //Metodos Generados


    } //End of class recUrbanizacionZP

} //End of namespace Galac.Saw.Dal.Tablas

