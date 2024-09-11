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
    public class CompraDetalleArticuloInventario: IEquatable<CompraDetalleArticuloInventario>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoCompra;
        private int _Consecutivo;
        private string _CodigoArticulo;
        private string _DescripcionArticulo;
        private decimal _Cantidad;
        private decimal _PrecioUnitario;
        private decimal _CantidadRecibida;
        private decimal _PorcentajeDeDistribucion;
        private decimal _MontoDistribucion;
        private decimal _PorcentajeSeguro;
        private decimal _CostoUnitario;
        private string _CodigoArticuloInv;
        private decimal _PorcentajeSeguroLey;
        private decimal _PorcentajeArancel;
        private string _CodigoGrupo;
        private eTipoArticuloInv _TipoArticuloInv;
        private int _ConsecutivoLoteDeInventario;
        private string _CodigoLote;
        private DateTime _FechaDeElaboracion;
        private DateTime _FechaDeVencimiento;
        private int _ConsecutivoLoteDeInventarioMov;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int ConsecutivoCompra {
            get { return _ConsecutivoCompra; }
            set { _ConsecutivoCompra = value; }
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
            set { _DescripcionArticulo = LibString.Mid(value, 0, 30); }
        }

        public decimal Cantidad {
            get { return _Cantidad; }
            set { 
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        public decimal PrecioUnitario {
            get { return _PrecioUnitario; }
            set { 
                _PrecioUnitario = value;
                OnPropertyChanged("PrecioUnitario");
            }
        }

        public decimal CantidadRecibida {
            get { return _CantidadRecibida; }
            set { _CantidadRecibida = value; }
        }

        public decimal PorcentajeDeDistribucion {
            get { return _PorcentajeDeDistribucion; }
            set { _PorcentajeDeDistribucion = value; }
        }

        public decimal MontoDistribucion {
            get { return _MontoDistribucion; }
            set { _MontoDistribucion = value; }
        }

        public decimal PorcentajeSeguro {
            get { return _PorcentajeSeguro; }
            set { _PorcentajeSeguro = value; }
        }

        public decimal CostoUnitario {
            get { return _CostoUnitario; }
            set {
                _CostoUnitario = value;
            }
        }

        public string CodigoArticuloInv {
            get { return _CodigoArticuloInv; }
            set {
                _CodigoArticuloInv = LibString.Mid(value, 0, 30);
               
            }
        }

        public decimal PorcentajeSeguroLey {
            get { return _PorcentajeSeguroLey; }
            set {
                _PorcentajeSeguroLey = value;
            }
        }

        public decimal PorcentajeArancel {
            get { return _PorcentajeArancel; }
            set { _PorcentajeArancel = value; }
        }

        public string CodigoGrupo {
            get { return _CodigoGrupo; }
            set { _CodigoGrupo = value; }
        }

        public eTipoArticuloInv TipoArticuloInvAsEnum {
            get { return _TipoArticuloInv; }
            set { _TipoArticuloInv = value; }
        }

        public string TipoArticuloInv {
            set { _TipoArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(value); }
        }


        public int TipoDeAlicuota {
            get;
            set;
        }

        public int TipoDeArticulo {
            get;
            set;
        }
        public int ConsecutivoLoteDeInventario {
            get { return _ConsecutivoLoteDeInventario; }
            set { _ConsecutivoLoteDeInventario = value; }
        }
        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = value; }
        }
        public DateTime FechaDeElaboracion {
            get { return _FechaDeElaboracion; }
            set { _FechaDeElaboracion = value; }
        }
        public DateTime FechaDeVencimiento {
            get { return _FechaDeVencimiento; }
            set { _FechaDeVencimiento = value; }
        }

        #endregion //Propiedades
        #region Constructores

        public CompraDetalleArticuloInventario() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ConsecutivoCompra = 0;
            Consecutivo = 0;
            CodigoArticulo = string.Empty;
            DescripcionArticulo = string.Empty;
            Cantidad = 0;
            PrecioUnitario = 0;
            CantidadRecibida = 0;
            PorcentajeDeDistribucion = 0;
            MontoDistribucion = 0;
            PorcentajeSeguro = 0;
            CodigoArticuloInv = string.Empty;
            PorcentajeSeguroLey = 0;
            PorcentajeArancel = 0;
            CodigoGrupo = string.Empty;
            TipoArticuloInvAsEnum = eTipoArticuloInv.Simple;
            ConsecutivoLoteDeInventario = 0;
        }

        public CompraDetalleArticuloInventario Clone() {
            CompraDetalleArticuloInventario vResult = new CompraDetalleArticuloInventario();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoCompra = _ConsecutivoCompra;
            vResult.Consecutivo = _Consecutivo;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.DescripcionArticulo = _DescripcionArticulo;
            vResult.Cantidad = _Cantidad;
            vResult.PrecioUnitario = _PrecioUnitario;
            vResult.CantidadRecibida = _CantidadRecibida;
            vResult.PorcentajeDeDistribucion = _PorcentajeDeDistribucion;
            vResult.MontoDistribucion = _MontoDistribucion;
            vResult.PorcentajeSeguro = _PorcentajeSeguro;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Compra = " + _ConsecutivoCompra.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nCódigo Inventario = " + _CodigoArticulo +
               "\nCantidad = " + _Cantidad.ToString() +
               "\nPrecio Unitario = " + _PrecioUnitario.ToString() +
               "\nCantidad Recibida = " + _CantidadRecibida.ToString() +
               "\nPorcentaje De Distribucion = " + _PorcentajeDeDistribucion.ToString() +
               "\nMonto Distribucion = " + _MontoDistribucion.ToString() +
               "\nPorcentaje Seguro = " + _PorcentajeSeguro.ToString();
        }

        #region Miembros de IEquatable<CompraDetalleArticuloInventario>
        bool IEquatable<CompraDetalleArticuloInventario>.Equals(CompraDetalleArticuloInventario other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<CompraDetalleArticuloInventario>

        #region Miembros de ICloneable<CompraDetalleArticuloInventario>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<CompraDetalleArticuloInventario>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class CompraDetalleArticuloInventario

} //End of namespace Galac.Adm.Ccl.GestionCompras

