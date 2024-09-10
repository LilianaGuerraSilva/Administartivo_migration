using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Ccl.GestionCompras {
    [Serializable]
    public class OrdenDeCompraDetalleArticuloInventario : IEquatable<OrdenDeCompraDetalleArticuloInventario>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoOrdenDeCompra;
        private int _Consecutivo;
        private string _CodigoArticulo;
        private string _DescripcionArticulo;
        private decimal _Cantidad;
        private decimal _CostoUnitario;
        private decimal _CantidadRecibida;
        private eTipoArticuloInv _TipoArticuloInv;
        private string _CodigoGrupo;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int ConsecutivoOrdenDeCompra {
            get { return _ConsecutivoOrdenDeCompra; }
            set { _ConsecutivoOrdenDeCompra = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
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
                _DescripcionArticulo = LibString.Mid(value, 0, 7000);
                //                OnPropertyChanged("DescripcionArticulo");
            }
        }

        public decimal Cantidad {
            get { return _Cantidad; }
            set {
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        public decimal CostoUnitario {
            get { return _CostoUnitario; }
            set {
                _CostoUnitario = value;
                OnPropertyChanged("CostoUnitario");
            }
        }

        public decimal CantidadRecibida {
            get { return _CantidadRecibida; }
            set { _CantidadRecibida = value; }
        }
		
        public eTipoArticuloInv TipoArticuloInv {
            get { return _TipoArticuloInv; }
            set { _TipoArticuloInv = value; }
        }

        public string CodigoGrupo {
            get { return _CodigoGrupo; }
            set { _CodigoGrupo = value; }
        }

        public int TipoDeAlicuota {
            get;
            set;
        }

        public int TipoDeArticulo {
            get;
            set;
        }
        public string TipoArticuloInvStr
        {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores

        public OrdenDeCompraDetalleArticuloInventario() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ConsecutivoOrdenDeCompra = 0;
            Consecutivo = 0;
            CodigoArticulo = string.Empty;
            DescripcionArticulo = string.Empty;
            Cantidad = 0;
            CostoUnitario = 0;
            CantidadRecibida = 0;
            TipoArticuloInv = eTipoArticuloInv.Simple;
            TipoArticuloInvStr = string.Empty;
        }

        public OrdenDeCompraDetalleArticuloInventario Clone() {
            OrdenDeCompraDetalleArticuloInventario vResult = new OrdenDeCompraDetalleArticuloInventario();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoOrdenDeCompra = _ConsecutivoOrdenDeCompra;
            vResult.Consecutivo = _Consecutivo;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.DescripcionArticulo = _DescripcionArticulo;
            vResult.Cantidad = _Cantidad;
            vResult.CostoUnitario = _CostoUnitario;
            vResult.CantidadRecibida = _CantidadRecibida;
            vResult.TipoArticuloInvStr = "";
            return vResult;
        }

        public override string ToString() {
            return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
                "\nConsecutivo Orden De Compra = " + _ConsecutivoOrdenDeCompra.ToString() +
                "\nConsecutivo = " + _Consecutivo.ToString() +
                "\nCódigo Inventario = " + _CodigoArticulo +
                "\nDescripción = " + _DescripcionArticulo +
                "\nCantidad = " + _Cantidad.ToString() +
                "\nCosto Unitario = " + _CostoUnitario.ToString() +
                "\nCantidad Recibida = " + _CantidadRecibida.ToString()+
                "\nTipo Articulo Inventario = " + TipoArticuloInvStr.ToString();
        }

        #region Miembros de IEquatable<OrdenDeCompraDetalleArticuloInventario>
        bool IEquatable<OrdenDeCompraDetalleArticuloInventario>.Equals(OrdenDeCompraDetalleArticuloInventario other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<OrdenDeCompraDetalleArticuloInventario>

        #region Miembros de ICloneable<OrdenDeCompraDetalleArticuloInventario>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<OrdenDeCompraDetalleArticuloInventario>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class OrdenDeCompraDetalleArticuloInventario

} //End of namespace Galac.Adm.Ccl.GestionCompras

