using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Services;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Integracion;

namespace Galac.Saw.Dal.Integracion {
    [Serializable]
    public class recIntegracion {
        #region Variables
        private eTipoIntegracion _TipoIntegracion;
        private eTipoIntegracion _TipoIntegracionOriginal;
        private string _version;
        private string _versionOriginal;
        private string _NombreOperador;
        private string _NombreOperadorOriginal;
        private DateTime _FechaUltimaModificacion;
        private DateTime _FechaUltimaModificacionOriginal;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Integracion"; }
        }

        public eTipoIntegracion TipoIntegracion {
            get { return _TipoIntegracion; }
            set { _TipoIntegracion = value; }
        }

        public string TipoIntegracionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoIntegracion); }
        }

        public string TipoIntegracionAsString {
            get { return LibEnumHelper.GetDescription(_TipoIntegracion); }
        }

        public eTipoIntegracion TipoIntegracionOriginal {
            get { return _TipoIntegracionOriginal; }
            set { _TipoIntegracionOriginal = value; }
        }

        public string TipoIntegracionOriginalAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoIntegracionOriginal); }
        }

        public string TipoIntegracionOriginalAsString {
            get { return LibEnumHelper.GetDescription(_TipoIntegracionOriginal); }
        }

        public string version {
            get { return _version; }
            set { _version = LibString.Mid(value, 0, 8); }
        }

        public string versionOriginal {
            get { return _versionOriginal; }
            set { _versionOriginal = LibString.Mid(value, 0, 8); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 20); }
        }

        public string NombreOperadorOriginal {
            get { return _NombreOperadorOriginal; }
            set { _NombreOperadorOriginal = LibString.Mid(value, 0, 20); }
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
        public recIntegracion(){
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public recIntegracion Clone() {
            recIntegracion vResult = new recIntegracion();
            vResult.TipoIntegracion = _TipoIntegracion;
            vResult.version = _version;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Tipo de Integracion = " + _TipoIntegracion.ToString() +
               "\nversion = " + _version +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }

        public void Clear() {
            TipoIntegracion = eTipoIntegracion.NOMINA;
            version = "";
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public void Fill(XmlDocument refResulset, bool valSetCurrent) {
            _datos = refResulset;
            LibXmlDataParse insParser = new LibXmlDataParse(refResulset);
            if (valSetCurrent && insParser.Count() > 0) {
                Clear();
                TipoIntegracion = (eTipoIntegracion) insParser.GetEnum(0, "TipoIntegracion", (int) TipoIntegracion);
                version = insParser.GetString(0, "version", version);
                NombreOperador = insParser.GetString(0, "NombreOperador", NombreOperador);
                FechaUltimaModificacion = insParser.GetDateTime(0, "FechaUltimaModificacion", FechaUltimaModificacion);
                fldTimeStamp = insParser.GetTimeStamp(0);
            }
        }
        #endregion //Metodos Generados


    } //End of class recIntegracion

} //End of namespace Galac.Saw.Dal.Integracion

