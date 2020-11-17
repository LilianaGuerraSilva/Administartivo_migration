using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class Talla {
        #region Variables
        private int _ConsecutivoCompania;
        private string _CodigoTalla;
        private string _CodigoTallaOriginal;
        private string _DescripcionTalla;
        private string _CodigoLote;
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

        public string CodigoTalla {
            get { return _CodigoTalla; }
            set { _CodigoTalla = LibString.Mid(value, 0, 3); }
        }

        public string CodigoTallaOriginal {
            get { return _CodigoTallaOriginal ; }
            set { _CodigoTallaOriginal  = LibString.Mid(value, 0, 3); }
        }

        public string DescripcionTalla {
            get { return _DescripcionTalla; }
            set { _DescripcionTalla = LibString.Mid(value, 0, 20); }
        }

        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = LibString.Mid(value, 0, 10); }
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

        public Talla() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            CodigoTalla = "";
            DescripcionTalla = "";
            CodigoLote = "";
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public Talla Clone() {
            Talla vResult = new Talla();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.CodigoTalla = _CodigoTalla;
            vResult.DescripcionTalla = _DescripcionTalla;
            vResult.CodigoLote = _CodigoLote;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCodigo Talla = " + _CodigoTalla +
               "\nDescripcion Talla = " + _DescripcionTalla +
               "\nCodigo Lote = " + _CodigoLote +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Talla

} //End of namespace Galac.Saw.Ccl.Inventario

