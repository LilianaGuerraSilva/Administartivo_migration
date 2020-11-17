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
    public class Color {
        #region Variables
        private int _ConsecutivoCompania;
        private string _CodigoColor;
        private string _CodigoColorOriginal;
        private string _DescripcionColor;
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

        public string CodigoColor {
            get { return _CodigoColor; }
            set { _CodigoColor = LibString.Mid(value, 0, 3); }
        }

        public string CodigoColorOriginal {
            get { return _CodigoColorOriginal; }
            set { _CodigoColorOriginal = LibString.Mid(value, 0, 3); }            
        }
        public string DescripcionColor {
            get { return _DescripcionColor; }
            set { _DescripcionColor = LibString.Mid(value, 0, 20); }
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

        public Color() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            CodigoColor = "";
            DescripcionColor = "";
            CodigoLote = "";
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public Color Clone() {
            Color vResult = new Color();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.CodigoColor = _CodigoColor;
            vResult.DescripcionColor = _DescripcionColor;
            vResult.CodigoLote = _CodigoLote;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCodigo Color = " + _CodigoColor +
               "\nDescripcion Color = " + _DescripcionColor +
               "\nCodigo Lote = " + _CodigoLote +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Color

} //End of namespace Galac.Saw.Ccl.Inventario

