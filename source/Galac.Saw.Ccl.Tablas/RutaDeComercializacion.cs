using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Ccl.Tablas {
    [Serializable]
    public class RutaDeComercializacion {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Descripcion;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 100); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 10); }
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

        public RutaDeComercializacion() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Consecutivo = 0;
            Descripcion = string.Empty;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public RutaDeComercializacion Clone() {
            RutaDeComercializacion vResult = new RutaDeComercializacion();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Descripcion = _Descripcion;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nDescripción = " + _Descripcion +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class RutaDeComercializacion

} //End of namespace Galac.Saw.Ccl.Tablas

