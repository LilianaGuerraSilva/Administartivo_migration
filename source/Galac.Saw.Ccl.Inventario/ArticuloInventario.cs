using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class ArticuloInventario {
        #region Variables
        private int _ConsecutivoCompania;
        private string _Codigo;
        private string _Descripcion;
        private string _LineaDeProducto;
        private eStatusArticulo _StatusdelArticulo;
        private eTipoDeArticulo _TipoDeArticulo;
        private eTipoDeAlicuota _AlicuotaIVA;
        private decimal _PrecioSinIVA;
        private decimal _PrecioConIVA;
        private decimal _Existencia;
        private string _Categoria;
        private string _Marca;
        private DateTime _FechaDeVencimiento;
        private string _UnidadDeVenta;
        private string _NombreOperador;
        private eTipoArticuloInv _TipoArticuloInv;
        private bool _UsaBalanza;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        //private ObservableCollection<ProductoCompuesto> _DetailProductoCompuesto;
        //private ObservableCollection<ExistenciaPorGrupo> _DetailExistenciaPorGrupo;
        //private ObservableCollection<CodigoDeBarras> _DetailCodigoDeBarras;
        XmlDocument _datos;
        private decimal _MePrecioSinIVA;
        private decimal _MePrecioConIVA;

        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 15); }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 255); }
        }

        public string LineaDeProducto {
            get { return _LineaDeProducto; }
            set { _LineaDeProducto = LibString.Mid(value, 0, 20); }
        }

        public eStatusArticulo StatusdelArticuloAsEnum {
            get { return _StatusdelArticulo; }
            set { _StatusdelArticulo = value; }
        }

        public string StatusdelArticulo {
            set { _StatusdelArticulo = (eStatusArticulo)LibConvert.DbValueToEnum(value); }
        }

        public string StatusdelArticuloAsDB {
            get { return LibConvert.EnumToDbValue((int) _StatusdelArticulo); }
        }

        public string StatusdelArticuloAsString {
            get { return LibEnumHelper.GetDescription(_StatusdelArticulo); }
        }

        public eTipoDeArticulo TipoDeArticuloAsEnum {
            get { return _TipoDeArticulo; }
            set { _TipoDeArticulo = value; }
        }

        public string TipoDeArticulo {
            set { _TipoDeArticulo = (eTipoDeArticulo)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeArticuloAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeArticulo); }
        }

        public string TipoDeArticuloAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeArticulo); }
        }

        public eTipoDeAlicuota AlicuotaIVAAsEnum {
            get { return _AlicuotaIVA; }
            set { _AlicuotaIVA = value; }
        }

        public string AlicuotaIVA {
            set { _AlicuotaIVA = (eTipoDeAlicuota)LibConvert.DbValueToEnum(value); }
        }

        public string AlicuotaIVAAsDB {
            get { return LibConvert.EnumToDbValue((int) _AlicuotaIVA); }
        }

        public string AlicuotaIVAAsString {
            get { return LibEnumHelper.GetDescription(_AlicuotaIVA); }
        }

        public decimal PrecioSinIVA {
            get { return _PrecioSinIVA; }
            set { _PrecioSinIVA = value; }
        }

        public decimal PrecioConIVA {
            get { return _PrecioConIVA; }
            set { _PrecioConIVA = value; }
        }

        public decimal Existencia {
            get { return _Existencia; }
            set { _Existencia = value; }
        }

        public string Categoria {
            get { return _Categoria; }
            set { _Categoria = LibString.Mid(value, 0, 20); }
        }

        public string Marca {
            get { return _Marca; }
            set { _Marca = LibString.Mid(value, 0, 30); }
        }

        public DateTime FechaDeVencimiento {
            get { return _FechaDeVencimiento; }
            set { _FechaDeVencimiento = LibConvert.DateToDbValue(value); }
        }

        public string UnidadDeVenta {
            get { return _UnidadDeVenta; }
            set { _UnidadDeVenta = LibString.Mid(value, 0, 20); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 10); }
        }

        public eTipoArticuloInv TipoArticuloInvAsEnum {
            get { return _TipoArticuloInv; }
            set { _TipoArticuloInv = value; }
        }

        public string TipoArticuloInv {
            set { _TipoArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(value); }
        }

        public string TipoArticuloInvAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoArticuloInv); }
        }

        public string TipoArticuloInvAsString {
            get { return LibEnumHelper.GetDescription(_TipoArticuloInv); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
        }

        public bool UsaBalanzaAsBool {
            get { return _UsaBalanza; }
            set { _UsaBalanza = value; }
        }

        public string UsaBalanza {
            set { _UsaBalanza = LibConvert.SNToBool(value); }
        }
        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        //public ObservableCollection<ProductoCompuesto> DetailProductoCompuesto {
        //    get { return _DetailProductoCompuesto; }
        //    set { _DetailProductoCompuesto = value; }
        //}

        //public ObservableCollection<ExistenciaPorGrupo> DetailExistenciaPorGrupo {
        //    get { return _DetailExistenciaPorGrupo; }
        //    set { _DetailExistenciaPorGrupo = value; }
        //}

        //public ObservableCollection<CodigoDeBarras> DetailCodigoDeBarras {
        //    get { return _DetailCodigoDeBarras; }
        //    set { _DetailCodigoDeBarras = value; }
        //}

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }

        public decimal MePrecioSinIVA {
            get { return _MePrecioSinIVA; }
            set { _MePrecioSinIVA = value; }
        }

        public decimal MePrecioConIVA {
            get { return _MePrecioConIVA; }
            set { _MePrecioConIVA = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public ArticuloInventario() {
            //_DetailProductoCompuesto = new ObservableCollection<ProductoCompuesto>();
            //_DetailExistenciaPorGrupo = new ObservableCollection<ExistenciaPorGrupo>();
            //_DetailCodigoDeBarras = new ObservableCollection<CodigoDeBarras>();
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Codigo = string.Empty;
            Descripcion = string.Empty;
            LineaDeProducto = string.Empty;
            StatusdelArticuloAsEnum = eStatusArticulo.Vigente;
            TipoDeArticuloAsEnum = eTipoDeArticulo.Mercancia;
            AlicuotaIVAAsEnum = eTipoDeAlicuota.Exento;
            PrecioSinIVA = 0;
            PrecioConIVA = 0;
            Existencia = 0;
            Categoria = string.Empty;
            Marca = string.Empty;
            FechaDeVencimiento = LibDate.Today();
            UnidadDeVenta = string.Empty;
            NombreOperador = string.Empty;
            TipoArticuloInvAsEnum = eTipoArticuloInv.Simple;
            UsaBalanzaAsBool = false;
            MePrecioSinIVA = 0;
            MePrecioConIVA = 0;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            //DetailProductoCompuesto = new ObservableCollection<ProductoCompuesto>();
            //DetailExistenciaPorGrupo = new ObservableCollection<ExistenciaPorGrupo>();
            //DetailCodigoDeBarras = new ObservableCollection<CodigoDeBarras>();
        }

        public ArticuloInventario Clone() {
            ArticuloInventario vResult = new ArticuloInventario();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Codigo = _Codigo;
            vResult.Descripcion = _Descripcion;
            vResult.LineaDeProducto = _LineaDeProducto;
            vResult.StatusdelArticuloAsEnum = _StatusdelArticulo;
            vResult.TipoDeArticuloAsEnum = _TipoDeArticulo;
            vResult.AlicuotaIVAAsEnum = _AlicuotaIVA;
            vResult.PrecioSinIVA = _PrecioSinIVA;
            vResult.PrecioConIVA = _PrecioConIVA;
            vResult.Existencia = _Existencia;
            vResult.Categoria = _Categoria;
            vResult.Marca = _Marca;
            vResult.FechaDeVencimiento = _FechaDeVencimiento;
            vResult.UnidadDeVenta = _UnidadDeVenta;
            vResult.NombreOperador = _NombreOperador;
            vResult.TipoArticuloInvAsEnum = _TipoArticuloInv;
            vResult.UsaBalanzaAsBool = _UsaBalanza;
            vResult.MePrecioSinIVA = _MePrecioSinIVA;
            vResult.MePrecioConIVA = _MePrecioConIVA;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCódigo del Artículo = " + _Codigo +
               "\nDescripción = " + _Descripcion +
               "\nLinea De Producto = " + _LineaDeProducto +
               "\nStatusdel Articulo = " + _StatusdelArticulo.ToString() +
               "\nTipo de Artículo = " + _TipoDeArticulo.ToString() +
               "\nAlícuota IVA = " + _AlicuotaIVA.ToString() +
               "\nPrecio Sin IVA = " + _PrecioSinIVA.ToString() +
               "\nPrecio Con IVA = " + _PrecioConIVA.ToString() +
               "\nExistencia = " + _Existencia.ToString() +
               "\nCategoria = " + _Categoria +
               "\nMarca = " + _Marca +
               "\nFecha De Vencimiento = " + _FechaDeVencimiento.ToShortDateString() +
               "\nUnidad De Venta = " + _UnidadDeVenta +
               "\nNombre Operador = " + _NombreOperador +
               "\nTipo de Artículo = " + _TipoArticuloInv.ToString() +
               "\nUsa Balanza = " + _UsaBalanza +
               "\nPrecio Sin IVA Divisa  = " + _MePrecioSinIVA +
               "\nPrecio Con IVA Divisa = " + _MePrecioConIVA +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class ArticuloInventario

} //End of namespace Galac.Saw.Ccl.Inventario

