using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Ccl.Banco {
    [Serializable]
    public class ConceptoBancario {
        #region Variables
        private int _Consecutivo;
        private string _Codigo;
        private string _Descripcion;
        private eIngresoEgreso _Tipo;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades
        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 8); }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 30); }
        }

        public eIngresoEgreso TipoAsEnum {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        public string Tipo {
            set { _Tipo = (eIngresoEgreso)LibConvert.DbValueToEnum(value); }
        }

        public string TipoAsDB {
            get { return LibConvert.EnumToDbValue((int) _Tipo); }
        }

        public string TipoAsString {
            get { return LibEnumHelper.GetDescription(_Tipo); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 20); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
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

        public ConceptoBancario() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return string.Empty;
        }
        
        public void Clear() {
            Consecutivo = 0;
            Codigo = string.Empty;
            Descripcion = string.Empty;
            TipoAsEnum = eIngresoEgreso.Ingreso;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public ConceptoBancario Clone() {
            ConceptoBancario vResult = new ConceptoBancario();
            vResult.Consecutivo = _Consecutivo;
            vResult.Codigo = _Codigo;
            vResult.Descripcion = _Descripcion;
            vResult.TipoAsEnum = _Tipo;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo = " + _Consecutivo.ToString() +
               "\nCodigo = " + _Codigo +
               "\nDescripcion = " + _Descripcion +
               "\nTipo = " + _Tipo.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class ConceptoBancario

} //End of namespace Galac.Adm.Ccl.Banco

