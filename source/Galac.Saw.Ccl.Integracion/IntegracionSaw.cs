using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Ccl.Integracion {
    [Serializable]
    public class IntegracionSaw {
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

        public eTipoIntegracion TipoIntegracionAsEnum {
            get { return _TipoIntegracion; }
            set { _TipoIntegracion = value; }
        }

        public string TipoIntegracion {
            set { _TipoIntegracion = (eTipoIntegracion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoIntegracionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoIntegracion); }
        }

        public string TipoIntegracionAsString {
            get { return LibEnumHelper.GetDescription(_TipoIntegracion); }
        }

        public eTipoIntegracion TipoIntegracionOriginalAsEnum {
            get { return _TipoIntegracionOriginal; }
            set { _TipoIntegracionOriginal = value; }
        }

        public string TipoIntegracionOriginal {
            set { _TipoIntegracionOriginal = (eTipoIntegracion)LibConvert.DbValueToEnum(value); }
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

        public IntegracionSaw() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            TipoIntegracionAsEnum = eTipoIntegracion.NOMINA;
            version = string.Empty;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public IntegracionSaw Clone() {
            IntegracionSaw vResult = new IntegracionSaw();
            vResult.TipoIntegracionAsEnum = _TipoIntegracion;
            vResult.version = _version;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Tipo de Integración = " + _TipoIntegracion.ToString() +
               "\nVersion = " + _version +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class IntegracionSaw

} //End of namespace Galac.Saw.Ccl.Integracion

