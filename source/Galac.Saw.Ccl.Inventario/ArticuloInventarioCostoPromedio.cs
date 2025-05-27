using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Base.Report;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class ArticuloInventarioCostoPromedio {
        #region Variables
        private eCantidadAImprimir _CantidadArtInv;
        private string _CodigoArticulo;
        private string _DescripcionArticulo;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public eCantidadAImprimir CantidadArtInvAsEnum {
            get { return _CantidadArtInv; }
            set { _CantidadArtInv = value; }
        }

        public string CantidadArtInv {
            set { _CantidadArtInv = (eCantidadAImprimir)LibConvert.DbValueToEnum(value); }
        }

        public string CantidadArtInvAsDB {
            get { return LibConvert.EnumToDbValue((int) _CantidadArtInv); }
        }

        public string CantidadArtInvAsString {
            get { return LibEnumHelper.GetDescription(_CantidadArtInv); }
        }

        public string CodigoArticulo {
            get { return _CodigoArticulo; }
            set { _CodigoArticulo = LibString.Mid(value, 0, 15); }
        }

        public string DescripcionArticulo {
            get { return _DescripcionArticulo; }
            set { _DescripcionArticulo = LibString.Mid(value, 0, 255); }
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

        public ArticuloInventarioCostoPromedio() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            CantidadArtInvAsEnum = eCantidadAImprimir.All;
            CodigoArticulo = string.Empty;
            DescripcionArticulo = string.Empty;
            fldTimeStamp = 0;
        }

        public ArticuloInventarioCostoPromedio Clone() {
            ArticuloInventarioCostoPromedio vResult = new ArticuloInventarioCostoPromedio();
            vResult.CantidadArtInvAsEnum = _CantidadArtInv;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.DescripcionArticulo = _DescripcionArticulo;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Cantidad de Artículos = " + _CantidadArtInv.ToString() +
                 "\nCodigo Articulo = " + _CodigoArticulo +
                 "\nDescripcion Articulo = " + _DescripcionArticulo;
        }
        #endregion //Metodos Generados


    } //End of class ArticuloInventarioCostoPromedio

} //End of namespace Galac.Saw.Ccl.Inventario

