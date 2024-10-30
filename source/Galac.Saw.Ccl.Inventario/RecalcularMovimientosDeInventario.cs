using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Base.Report;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class RecalcularMovimientosDeInventario {
        #region Variables
        private eCantidadAImprimir _ArticuloUnoTodos;
        private string _CodigoArticulo;
        private eCantidadAImprimir _LineaDeProductoUnoTodos;
        private string _LineaDeProducto;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public eCantidadAImprimir ArticuloUnoTodosAsEnum {
            get { return _ArticuloUnoTodos; }
            set { _ArticuloUnoTodos = value; }
        }

        public string ArticuloUnoTodos {
            set { _ArticuloUnoTodos = (eCantidadAImprimir)LibConvert.DbValueToEnum(value); }
        }

        public string ArticuloUnoTodosAsDB {
            get { return LibConvert.EnumToDbValue((int) _ArticuloUnoTodos); }
        }

        public string ArticuloUnoTodosAsString {
            get { return LibEnumHelper.GetDescription(_ArticuloUnoTodos); }
        }

        public string CodigoArticulo {
            get { return _CodigoArticulo; }
            set { _CodigoArticulo = LibString.Mid(value, 0, 15); }
        }

        public eCantidadAImprimir LineaDeProductoUnoTodosAsEnum {
            get { return _LineaDeProductoUnoTodos; }
            set { _LineaDeProductoUnoTodos = value; }
        }

        public string LineaDeProductoUnoTodos {
            set { _LineaDeProductoUnoTodos = (eCantidadAImprimir)LibConvert.DbValueToEnum(value); }
        }

        public string LineaDeProductoUnoTodosAsDB {
            get { return LibConvert.EnumToDbValue((int) _LineaDeProductoUnoTodos); }
        }

        public string LineaDeProductoUnoTodosAsString {
            get { return LibEnumHelper.GetDescription(_LineaDeProductoUnoTodos); }
        }

        public string LineaDeProducto {
            get { return _LineaDeProducto; }
            set { _LineaDeProducto = LibString.Mid(value, 0, 20); }
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
        public RecalcularMovimientosDeInventario() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ArticuloUnoTodosAsEnum = eCantidadAImprimir.All;
            CodigoArticulo = string.Empty;
            LineaDeProductoUnoTodosAsEnum = eCantidadAImprimir.All;
            LineaDeProducto = string.Empty;
            fldTimeStamp = 0;
        }

        public RecalcularMovimientosDeInventario Clone() {
            RecalcularMovimientosDeInventario vResult = new RecalcularMovimientosDeInventario();
            vResult.ArticuloUnoTodosAsEnum = _ArticuloUnoTodos;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.LineaDeProductoUnoTodosAsEnum = _LineaDeProductoUnoTodos;
            vResult.LineaDeProducto = _LineaDeProducto;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Cantidad de Artículos = " + _ArticuloUnoTodos.ToString() +
               "\nCodigo Articulo = " + _CodigoArticulo +
               "\nCantidad de Líneas = " + _LineaDeProductoUnoTodos.ToString() +
               "\nLinea De Producto = " + _LineaDeProducto;
        }
        #endregion //Metodos Generados

    } //End of class RecalcularMovimientosDeInventario

} //End of namespace Galac.Saw.Ccl.Inventario

