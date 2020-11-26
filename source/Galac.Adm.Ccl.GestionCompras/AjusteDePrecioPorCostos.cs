using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class AjusteDePrecioPorCostos {
        #region Variables
        private int _ConsecutivoCompania;
        private bool _LineaDeProductoOption;
        private bool _CategoriaOption;
        private bool _MarcaOption;
        private string _Desde;
        private string _Hasta;
        private eBuscarPor _NombreLineaDeProductoCombo;
        private eBuscarPor _CategoriaCombo;
        private string _NombreLineaDeProducto;
        private string _Categoria;
        private string _Marca;
        private eNivelDePrecio _NivelDePrecio;
        private eRedondearPrecio _RedondearPrecioA;
        private bool _EstablecerNuevoMargen;
        private bool _UsarFormulaAlterna;
        private decimal _MargenUno;
        private decimal _MargenDos;
        private decimal _MargenTres;
        private decimal _MargenCuatro;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public bool LineaDeProductoOptionAsBool {
            get { return _LineaDeProductoOption; }
            set { _LineaDeProductoOption = value; }
        }

        public string LineaDeProductoOption {
            set { _LineaDeProductoOption = LibConvert.SNToBool(value); }
        }


        public bool CategoriaOptionAsBool {
            get { return _CategoriaOption; }
            set { _CategoriaOption = value; }
        }

        public string CategoriaOption {
            set { _CategoriaOption = LibConvert.SNToBool(value); }
        }


        public bool MarcaOptionAsBool {
            get { return _MarcaOption; }
            set { _MarcaOption = value; }
        }

        public string MarcaOption {
            set { _MarcaOption = LibConvert.SNToBool(value); }
        }


        public string Desde {
            get { return _Desde; }
            set { _Desde = LibString.Mid(value, 0, 30); }
        }

        public string Hasta {
            get { return _Hasta; }
            set { _Hasta = LibString.Mid(value, 0, 30); }
        }

        public eBuscarPor NombreLineaDeProductoComboAsEnum {
            get { return _NombreLineaDeProductoCombo; }
            set { _NombreLineaDeProductoCombo = value; }
        }

        public string NombreLineaDeProductoCombo {
            set { _NombreLineaDeProductoCombo = (eBuscarPor)LibConvert.DbValueToEnum(value); }
        }

        public string NombreLineaDeProductoComboAsDB {
            get { return LibConvert.EnumToDbValue((int) _NombreLineaDeProductoCombo); }
        }

        public string NombreLineaDeProductoComboAsString {
            get { return LibEnumHelper.GetDescription(_NombreLineaDeProductoCombo); }
        }

        public eBuscarPor CategoriaComboAsEnum {
            get { return _CategoriaCombo; }
            set { _CategoriaCombo = value; }
        }

        public string CategoriaCombo {
            set { _CategoriaCombo = (eBuscarPor)LibConvert.DbValueToEnum(value); }
        }

        public string CategoriaComboAsDB {
            get { return LibConvert.EnumToDbValue((int) _CategoriaCombo); }
        }

        public string CategoriaComboAsString {
            get { return LibEnumHelper.GetDescription(_CategoriaCombo); }
        }

        public string NombreLineaDeProducto {
            get { return _NombreLineaDeProducto; }
            set { _NombreLineaDeProducto = LibString.Mid(value, 0, 20); }
        }

        public string Categoria {
            get { return _Categoria; }
            set { _Categoria = LibString.Mid(value, 0, 20); }
        }

        public string Marca {
            get { return _Marca; }
            set { _Marca = LibString.Mid(value, 0, 20); }
        }

        public eNivelDePrecio NivelDePrecioAsEnum {
            get { return _NivelDePrecio; }
            set { _NivelDePrecio = value; }
        }

        public string NivelDePrecio {
            set { _NivelDePrecio = (eNivelDePrecio)LibConvert.DbValueToEnum(value); }
        }

        public string NivelDePrecioAsDB {
            get { return LibConvert.EnumToDbValue((int) _NivelDePrecio); }
        }

        public string NivelDePrecioAsString {
            get { return LibEnumHelper.GetDescription(_NivelDePrecio); }
        }

        public eRedondearPrecio RedondearPrecioAAsEnum {
            get { return _RedondearPrecioA; }
            set { _RedondearPrecioA = value; }
        }

        public string RedondearPrecioA {
            set { _RedondearPrecioA = (eRedondearPrecio)LibConvert.DbValueToEnum(value); }
        }

        public string RedondearPrecioAAsDB {
            get { return LibConvert.EnumToDbValue((int) _RedondearPrecioA); }
        }

        public string RedondearPrecioAAsString {
            get { return LibEnumHelper.GetDescription(_RedondearPrecioA); }
        }

        public bool EstablecerNuevoMargenAsBool {
            get { return _EstablecerNuevoMargen; }
            set { _EstablecerNuevoMargen = value; }
        }

        public string EstablecerNuevoMargen {
            set { _EstablecerNuevoMargen = LibConvert.SNToBool(value); }
        }


        public bool UsarFormulaAlternaAsBool {
            get { return _UsarFormulaAlterna; }
            set { _UsarFormulaAlterna = value; }
        }

        public string UsarFormulaAlterna {
            set { _UsarFormulaAlterna = LibConvert.SNToBool(value); }
        }


        public decimal MargenUno {
            get { return _MargenUno; }
            set { _MargenUno = value; }
        }

        public decimal MargenDos {
            get { return _MargenDos; }
            set { _MargenDos = value; }
        }

        public decimal MargenTres {
            get { return _MargenTres; }
            set { _MargenTres = value; }
        }

        public decimal MargenCuatro {
            get { return _MargenCuatro; }
            set { _MargenCuatro = value; }
        }
		
        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public AjusteDePrecioPorCostos() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            LineaDeProductoOptionAsBool = false;
            CategoriaOptionAsBool = false;
            MarcaOptionAsBool = false;
            Desde = string.Empty;
            Hasta = string.Empty;
            NombreLineaDeProductoComboAsEnum = eBuscarPor.Todas;
            CategoriaComboAsEnum = eBuscarPor.Todas;
            NombreLineaDeProducto = string.Empty;
            Categoria = string.Empty;
            Marca = string.Empty;
            NivelDePrecioAsEnum = eNivelDePrecio.Todos;
            RedondearPrecioAAsEnum = eRedondearPrecio.SinRedondear;
            EstablecerNuevoMargenAsBool = false;
            UsarFormulaAlternaAsBool = false;
            MargenUno = 0;
            MargenDos = 0;
            MargenTres = 0;
            MargenCuatro = 0;
        }

        //public AjusteDePreciosPorCosto Clone() {
        //    AjusteDePreciosPorCosto vResult = new AjusteDePreciosPorCosto();
        //    vResult.ConsecutivoCompania = _ConsecutivoCompania;
        //    vResult.LineaDeProductoOptionAsBool = _LineaDeProductoOption;
        //    vResult.CategoriaOptionAsBool = _CategoriaOption;
        //    vResult.MarcaOptionAsBool = _MarcaOption;
        //    vResult.Desde = _Desde;
        //    vResult.Hasta = _Hasta;
        //    vResult.NombreLineaDeProductoComboAsEnum = _NombreLineaDeProductoCombo;
        //    vResult.CategoriaComboAsEnum = _CategoriaCombo;
        //    vResult.NombreLineaDeProducto = _NombreLineaDeProducto;
        //    vResult.Categoria = _Categoria;
        //    vResult.Marca = _Marca;
        //    vResult.NivelDePrecioAsEnum = _NivelDePrecio;
        //    vResult.RedondearPrecioAAsEnum = _RedondearPrecioA;
        //    vResult.EstablecerNuevoMargenAsBool = _EstablecerNuevoMargen;
        //    vResult.UsarFormulaAlternaAsBool = _UsarFormulaAlterna;
        //    vResult.MargenUno = _MargenUno;
        //    vResult.MargenDos = _MargenDos;
        //    vResult.MargenTres = _MargenTres;
        //    vResult.MargenCuatro = _MargenCuatro;
        //    return vResult;
        //}

        public override string ToString() {
           return "Línea de producto = " + _LineaDeProductoOption +
               "\nCategoría = " + _CategoriaOption +
               "\nMarca = " + _MarcaOption +
               "\nDesde = " + _Desde +
               "\nHasta = " + _Hasta +
               "\nSeleccione = " + _NombreLineaDeProductoCombo.ToString() +
               "\nSeleccione = " + _CategoriaCombo.ToString() +
               "\nLínea de producto = " + _NombreLineaDeProducto +
               "\nCategoría = " + _Categoria +
               "\nMarca = " + _Marca +
               "\nNivel de precio = " + _NivelDePrecio.ToString() +
               "\nRedondear precio a = " + _RedondearPrecioA.ToString() +
               "\nEstablecer nuevo margen = " + _EstablecerNuevoMargen +
               "\nUsar fórmula alterna = " + _UsarFormulaAlterna +
               "\nMargen 1 = " + _MargenUno.ToString() +
               "\nMargen 2 = " + _MargenDos.ToString() +
               "\nMargen 3 = " + _MargenTres.ToString() +
               "\nMargen 4 = " + _MargenCuatro.ToString();
        }
        #endregion //Metodos Generados


    } //End of class AjusteDePreciosPorCosto

} //End of namespace Galac.Saw.Ccl.Inventario

