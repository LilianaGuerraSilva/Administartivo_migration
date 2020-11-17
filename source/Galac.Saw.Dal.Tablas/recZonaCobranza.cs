using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Services;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Saw.Dal.Tablas {
    [Serializable]
    public class recZonaCobranza {
        #region Variables
        private int _ConsecutivoCompania;
        private string _Nombre;
        private string _NombreOriginal;
        private string _NombreOperador;
        private string _NombreOperadorOriginal;
        private DateTime _FechaUltimaModificacion;
        private DateTime _FechaUltimaModificacionOriginal;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Zona Cobranza"; }
        }

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 100); }
        }

        public string NombreOriginal {
            get { return _NombreOriginal; }
            set { _NombreOriginal = LibString.Mid(value, 0, 100); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 10); }
        }

        public string NombreOperadorOriginal {
            get { return _NombreOperadorOriginal; }
            set { _NombreOperadorOriginal = LibString.Mid(value, 0, 10); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaUltimaModificacionOriginal {
            get { return _FechaUltimaModificacionOriginal; }
            set { _FechaUltimaModificacionOriginal = LibConvert.DateToDbValue(value); }
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
        public recZonaCobranza(){
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public recZonaCobranza Clone() {
            recZonaCobranza vResult = new recZonaCobranza();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Nombre = _Nombre;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNombre = " + _Nombre +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            Nombre = "";
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public void Fill(XmlDocument refResulset, bool valSetCurrent) {
            _datos = refResulset;
            LibXmlDataParse insParser = new LibXmlDataParse(refResulset);
            if (valSetCurrent && insParser.Count() > 0) {
                Clear();
                ConsecutivoCompania = insParser.GetInt(0, "ConsecutivoCompania", ConsecutivoCompania);
                Nombre = insParser.GetString(0, "Nombre", Nombre);
                NombreOperador = insParser.GetString(0, "NombreOperador", NombreOperador);
                FechaUltimaModificacion = insParser.GetDateTime(0, "FechaUltimaModificacion", FechaUltimaModificacion);
                fldTimeStamp = insParser.GetTimeStamp(0);
            }
        }
        #endregion //Metodos Generados


    } //End of class recZonaCobranza

} //End of namespace Galac.Saw.Dal.Tablas

