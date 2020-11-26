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
    public class CamposDefiniblesStt : ISettDefinition {
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
        private bool _UsaCamposDefinibles;
        private string _NombreCampoDefinible1;
        private string _NombreCampoDefinible2;
        private string _NombreCampoDefinible3;
        private string _NombreCampoDefinible4;
        private string _NombreCampoDefinible5;
        private string _NombreCampoDefinible6;
        private string _NombreCampoDefinible7;
        private string _NombreCampoDefinible8;
        private string _NombreCampoDefinible9;
        private string _NombreCampoDefinible10;
        private string _NombreCampoDefinible11;
        private string _NombreCampoDefinible12;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool UsaCamposDefiniblesAsBool {
            get { return _UsaCamposDefinibles; }
            set { _UsaCamposDefinibles = value; }
        }

        public string UsaCamposDefinibles {
            set { _UsaCamposDefinibles = LibConvert.SNToBool(value); }
        }


        public string NombreCampoDefinible1 {
            get { return _NombreCampoDefinible1; }
            set { _NombreCampoDefinible1 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinible2 {
            get { return _NombreCampoDefinible2; }
            set { _NombreCampoDefinible2 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinible3 {
            get { return _NombreCampoDefinible3; }
            set { _NombreCampoDefinible3 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinible4 {
            get { return _NombreCampoDefinible4; }
            set { _NombreCampoDefinible4 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinible5 {
            get { return _NombreCampoDefinible5; }
            set { _NombreCampoDefinible5 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinible6 {
            get { return _NombreCampoDefinible6; }
            set { _NombreCampoDefinible6 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinible7 {
            get { return _NombreCampoDefinible7; }
            set { _NombreCampoDefinible7 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinible8 {
            get { return _NombreCampoDefinible8; }
            set { _NombreCampoDefinible8 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinible9 {
            get { return _NombreCampoDefinible9; }
            set { _NombreCampoDefinible9 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinible10 {
            get { return _NombreCampoDefinible10; }
            set { _NombreCampoDefinible10 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinible11 {
            get { return _NombreCampoDefinible11; }
            set { _NombreCampoDefinible11 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinible12 {
            get { return _NombreCampoDefinible12; }
            set { _NombreCampoDefinible12 = LibString.Mid(value, 0, 20); }
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

        public CamposDefiniblesStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            UsaCamposDefiniblesAsBool = false;
            NombreCampoDefinible1 = "";
            NombreCampoDefinible2 = "";
            NombreCampoDefinible3 = "";
            NombreCampoDefinible4 = "";
            NombreCampoDefinible5 = "";
            NombreCampoDefinible6 = "";
            NombreCampoDefinible7 = "";
            NombreCampoDefinible8 = "";
            NombreCampoDefinible9 = "";
            NombreCampoDefinible10 = "";
            NombreCampoDefinible11 = "";
            NombreCampoDefinible12 = "";
            fldTimeStamp = 0;
        }

        public CamposDefiniblesStt Clone() {
            CamposDefiniblesStt vResult = new CamposDefiniblesStt();
            vResult.UsaCamposDefiniblesAsBool = _UsaCamposDefinibles;
            vResult.NombreCampoDefinible1 = _NombreCampoDefinible1;
            vResult.NombreCampoDefinible2 = _NombreCampoDefinible2;
            vResult.NombreCampoDefinible3 = _NombreCampoDefinible3;
            vResult.NombreCampoDefinible4 = _NombreCampoDefinible4;
            vResult.NombreCampoDefinible5 = _NombreCampoDefinible5;
            vResult.NombreCampoDefinible6 = _NombreCampoDefinible6;
            vResult.NombreCampoDefinible7 = _NombreCampoDefinible7;
            vResult.NombreCampoDefinible8 = _NombreCampoDefinible8;
            vResult.NombreCampoDefinible9 = _NombreCampoDefinible9;
            vResult.NombreCampoDefinible10 = _NombreCampoDefinible10;
            vResult.NombreCampoDefinible11 = _NombreCampoDefinible11;
            vResult.NombreCampoDefinible12 = _NombreCampoDefinible12;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Usa Campos Definibles = " + _UsaCamposDefinibles +
               "\nNombre Campo Definible 1 = " + _NombreCampoDefinible1 +
               "\nNombre Campo Definible 2 = " + _NombreCampoDefinible2 +
               "\nNombre Campo Definible 3 = " + _NombreCampoDefinible3 +
               "\nNombre Campo Definible 4 = " + _NombreCampoDefinible4 +
               "\nNombre Campo Definible 5 = " + _NombreCampoDefinible5 +
               "\nNombre Campo Definible 6 = " + _NombreCampoDefinible6 +
               "\nNombre Campo Definible 7 = " + _NombreCampoDefinible7 +
               "\nNombre Campo Definible 8 = " + _NombreCampoDefinible8 +
               "\nNombre Campo Definible 9 = " + _NombreCampoDefinible9 +
               "\nNombre Campo Definible 10 = " + _NombreCampoDefinible10 +
               "\nNombre Campo Definible 11 = " + _NombreCampoDefinible11 +
               "\nNombre Campo Definible 12 = " + _NombreCampoDefinible12;
        }
        #endregion //Metodos Generados


    } //End of class CamposDefiniblesStt

} //End of namespace Galac.Saw.Ccl.SttDef

