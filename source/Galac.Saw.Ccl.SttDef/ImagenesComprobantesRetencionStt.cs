using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Ccl.SttDef {
    [Serializable]
    public class ImagenesComprobantesRetencionStt:ISettDefinition {
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

        private string _NombreLogo;
        private string _NombreFirma;
        private string _NombreSello;

        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string NombreLogo {
            get { return _NombreLogo; }
            set { _NombreLogo = value; }
        }

        public string NombreFirma {
            get { return _NombreFirma; }
            set { _NombreFirma = value; }
        }

        public string NombreSello {
            get { return _NombreSello; }
            set { _NombreSello = value; }
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

        public ImagenesComprobantesRetencionStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            NombreFirma = string.Empty;
            NombreLogo = string.Empty;
            NombreSello = string.Empty;
            fldTimeStamp = 0;
        }

        public ImagenesComprobantesRetencionStt Clone() {
            ImagenesComprobantesRetencionStt vResult = new ImagenesComprobantesRetencionStt();
            vResult.NombreSello = _NombreSello;
            vResult.NombreLogo = _NombreLogo;
            vResult.NombreFirma = _NombreFirma;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Nombre del Sello = " + _NombreSello +
                "\nNombre del Logo = " + _NombreLogo +
                "\nNombre de la Firma = " + _NombreFirma;
        }
        #endregion //Metodos Generados
    } //End of class ImagenesComprobantesRetencionStt
} //End of namespace Galac.Saw.Ccl.SttDef

