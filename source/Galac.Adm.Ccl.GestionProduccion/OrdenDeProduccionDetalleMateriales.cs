using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Ccl.GestionProduccion {
    [Serializable]
    public class OrdenDeProduccionDetalleMateriales: IEquatable<OrdenDeProduccionDetalleMateriales>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoOrdenDeProduccion;
        private int _Consecutivo;
        private int _ConsecutivoAlmacen;
        private string _CodigoAlmacen;
        private string _NombreAlmacen;
        private string _CodigoArticulo;
        private string _DescripcionArticulo;
        private string _UnidadDeVenta;
        private decimal _Cantidad;
        private decimal _CantidadReservadaInventario;
        private decimal _CantidadConsumida;
        private decimal _CostoUnitarioArticuloInventario;
        private decimal _CostoUnitarioMEArticuloInventario;
        private decimal _MontoSubtotal;
        private bool _AjustadoPostCierre;
        private decimal _CantidadAjustada;
        private eTipoDeArticulo  _TipoDeArticulo;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int ConsecutivoOrdenDeProduccion {
            get { return _ConsecutivoOrdenDeProduccion; }
            set { _ConsecutivoOrdenDeProduccion = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public int ConsecutivoAlmacen {
            get { return _ConsecutivoAlmacen; }
            set { _ConsecutivoAlmacen = value; }
        }

        public string CodigoAlmacen {
            get { return _CodigoAlmacen; }
            set { _CodigoAlmacen = LibString.Mid(value, 0, 5); }
        }

        public string NombreAlmacen {
            get { return _NombreAlmacen; }
            set { _NombreAlmacen = LibString.Mid(value, 0, 40); }
        }

        public string CodigoArticulo {
            get { return _CodigoArticulo; }
            set { 
                _CodigoArticulo = LibString.Mid(value, 0, 30);
                OnPropertyChanged("CodigoArticulo");
            }
        }

        public string DescripcionArticulo {
            get { return _DescripcionArticulo; }
            set { 
                _DescripcionArticulo = LibString.Mid(value, 0, 255);
                OnPropertyChanged("DescripcionArticulo");
            }
        }

        public string UnidadDeVenta {
            get { return _UnidadDeVenta; }
            set { 
                _UnidadDeVenta = LibString.Mid(value, 0, 20);
                OnPropertyChanged("UnidadDeVenta");
            }
        }

        public decimal Cantidad {
            get { return _Cantidad; }
            set { 
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        public decimal CantidadReservadaInventario {
            get { return _CantidadReservadaInventario; }
            set { 
                _CantidadReservadaInventario = value;
                OnPropertyChanged("CantidadReservadaInventario");
            }
        }

        public decimal CantidadConsumida {
            get { return _CantidadConsumida; }
            set { 
                _CantidadConsumida = value;
                OnPropertyChanged("CantidadConsumida");
            }
        }

        public decimal CostoUnitarioArticuloInventario {
            get { return _CostoUnitarioArticuloInventario; }
            set { _CostoUnitarioArticuloInventario = value; }
        }

        public decimal CostoUnitarioMEArticuloInventario {
            get { return _CostoUnitarioMEArticuloInventario; }
            set { _CostoUnitarioMEArticuloInventario = value; }
        }

        public decimal MontoSubtotal {
            get { return _MontoSubtotal; }
            set { _MontoSubtotal = value; }
        }

        public bool AjustadoPostCierreAsBool {
            get { return _AjustadoPostCierre; }
            set { _AjustadoPostCierre = value; }
        }

        public string AjustadoPostCierre {
            set { _AjustadoPostCierre = LibConvert.SNToBool(value); }
        }


        public decimal CantidadAjustada {
            get { return _CantidadAjustada; }
            set { _CantidadAjustada = value; }
        }

        public eTipoDeArticulo TipoDeArticuloAsEnum {
            get { return _TipoDeArticulo; }
            set { _TipoDeArticulo = value; }
        }
		
        public string TipoDeArticulo {
            set { _TipoDeArticulo = (eTipoDeArticulo)LibConvert.DbValueToEnum(value); }
        }
        #endregion //Propiedades
        #region Constructores

        public OrdenDeProduccionDetalleMateriales() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ConsecutivoOrdenDeProduccion = 0;
            Consecutivo = 0;
            ConsecutivoAlmacen = 0;
            CodigoAlmacen = string.Empty;
            NombreAlmacen = string.Empty;
            CodigoArticulo = string.Empty;
            DescripcionArticulo = string.Empty;
            UnidadDeVenta = string.Empty;
            Cantidad = 0;
            CantidadReservadaInventario = 0;
            CantidadConsumida = 0;
            CostoUnitarioArticuloInventario = 0;
            CostoUnitarioMEArticuloInventario = 0;
            MontoSubtotal = 0;
            AjustadoPostCierreAsBool = false;
            CantidadAjustada = 0;
            TipoDeArticuloAsEnum = eTipoDeArticulo.Mercancia;
        }

        public OrdenDeProduccionDetalleMateriales Clone() {
            OrdenDeProduccionDetalleMateriales vResult = new OrdenDeProduccionDetalleMateriales();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoOrdenDeProduccion = _ConsecutivoOrdenDeProduccion;
            vResult.Consecutivo = _Consecutivo;
            vResult.ConsecutivoAlmacen = _ConsecutivoAlmacen;
            vResult.CodigoAlmacen = _CodigoAlmacen;
            vResult.NombreAlmacen = _NombreAlmacen;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.DescripcionArticulo = _DescripcionArticulo;
            vResult.UnidadDeVenta = _UnidadDeVenta;
            vResult.Cantidad = _Cantidad;
            vResult.CantidadReservadaInventario = _CantidadReservadaInventario;
            vResult.CantidadConsumida = _CantidadConsumida;
            vResult.CostoUnitarioArticuloInventario = _CostoUnitarioArticuloInventario;
            vResult.CostoUnitarioMEArticuloInventario = _CostoUnitarioMEArticuloInventario;
            vResult.MontoSubtotal = _MontoSubtotal;
            vResult.AjustadoPostCierreAsBool = _AjustadoPostCierre;
            vResult.CantidadAjustada = _CantidadAjustada;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Orden De Produccion = " + _ConsecutivoOrdenDeProduccion.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nConsecutivo Almacen = " + _ConsecutivoAlmacen.ToString() +
               "\nCodigoArticulo = " + _CodigoArticulo +
               "\n CantidadListaDeMateriales = " + _Cantidad.ToString() +
               "\nCantidad Reservada Inventario = " + _CantidadReservadaInventario.ToString() +
               "\nCantidad Consumida = " + _CantidadConsumida.ToString() +
               "\nCosto Unitario Articulo Inventario = " + _CostoUnitarioArticuloInventario.ToString() +
               "\nCosto Unitario En Moneda Extranjera Articulo Inventario = " + _CostoUnitarioMEArticuloInventario.ToString() +
               "\nMonto Subtotal = " + _MontoSubtotal.ToString() +
               "\nAjusta por Cierre = " + _AjustadoPostCierre +
               "\nCantidad Ajustada = " + _CantidadAjustada.ToString();
        }

        #region Miembros de IEquatable<OrdenDeProduccionDetalleMateriales>
        bool IEquatable<OrdenDeProduccionDetalleMateriales>.Equals(OrdenDeProduccionDetalleMateriales other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<OrdenDeProduccionDetalleMateriales>

        #region Miembros de ICloneable<OrdenDeProduccionDetalleMateriales>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<OrdenDeProduccionDetalleMateriales>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados

    } //End of class OrdenDeProduccionDetalleMateriales

} //End of namespace Galac.Adm.Ccl.GestionProduccion

