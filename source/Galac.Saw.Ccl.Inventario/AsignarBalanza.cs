using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class AsignarBalanza {
        #region Variables
        private int _ConsecutivoCompania;
        private string _LineaDeProducto;
        private string _ArticuloDesde;
        private string _ArticuloHasta;
        private eTipoDeAsignacion _TipoDeAsignacion;
        private eTipoDeAccion _Accion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string LineaDeProducto {
            get { return _LineaDeProducto; }
            set { _LineaDeProducto = LibString.Mid(value, 0, 20); }
        }

        public string ArticuloDesde {
            get { return _ArticuloDesde; }
            set { _ArticuloDesde = LibString.Mid(value, 0, 30); }
        }

        public string ArticuloHasta {
            get { return _ArticuloHasta; }
            set { _ArticuloHasta = LibString.Mid(value, 0, 30); }
        }

        public eTipoDeAsignacion TipoDeAsignacionAsEnum {
            get { return _TipoDeAsignacion; }
            set { _TipoDeAsignacion = value; }
        }

        public string TipoDeAsignacion {
            set { _TipoDeAsignacion = (eTipoDeAsignacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeAsignacionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeAsignacion); }
        }

        public string TipoDeAsignacionAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeAsignacion); }
        }

        public eTipoDeAccion AccionAsEnum {
            get { return _Accion; }
            set { _Accion = value; }
        }

        public string Accion {
            set { _Accion = (eTipoDeAccion)LibConvert.DbValueToEnum(value); }
        }

        public string AccionAsDB {
            get { return LibConvert.EnumToDbValue((int) _Accion); }
        }

        public string AccionAsString {
            get { return LibEnumHelper.GetDescription(_Accion); }
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

        public AsignarBalanza() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            LineaDeProducto = string.Empty;
            ArticuloDesde = string.Empty;
            ArticuloHasta = string.Empty;
            TipoDeAsignacionAsEnum = eTipoDeAsignacion.Todos;
            AccionAsEnum = eTipoDeAccion.Activar;
            fldTimeStamp = 0;
        }

        public AsignarBalanza Clone() {
            AsignarBalanza vResult = new AsignarBalanza();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.LineaDeProducto = _LineaDeProducto;
            vResult.ArticuloDesde = _ArticuloDesde;
            vResult.ArticuloHasta = _ArticuloHasta;
            vResult.TipoDeAsignacionAsEnum = _TipoDeAsignacion;
            vResult.AccionAsEnum = _Accion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nLinea de Producto = " + _LineaDeProducto +
               "\nDesde = " + _ArticuloDesde +
               "\nHasta = " + _ArticuloHasta +
               "\nTipo de Asignación = " + _TipoDeAsignacion.ToString() +
               "\nAccion = " + _Accion.ToString();
        }
        #endregion //Metodos Generados


    } //End of class InventarioAsignarBalanza

} //End of namespace Galac.Saw.Ccl.Inventario

