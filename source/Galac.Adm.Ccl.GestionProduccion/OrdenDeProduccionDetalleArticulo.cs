using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Inventario;


namespace Galac.Adm.Ccl.GestionProduccion {
    [Serializable]
    public class OrdenDeProduccionDetalleArticulo: IEquatable<OrdenDeProduccionDetalleArticulo>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoOrdenDeProduccion;
        private int _Consecutivo;
        private int _ConsecutivoAlmacen;
        private int _ConsecutivoLoteDeInventario;
        private string _CodigoAlmacen;
        private string _NombreAlmacen;
        private string _CodigoArticulo;
        private string _DescripcionArticulo;
        private string _UnidadDeVenta;
        private decimal _CantidadOriginalLista;
        private decimal _CantidadSolicitada;
        private decimal _CantidadProducida;
        private decimal _CostoUnitario;
        private decimal _MontoSubTotal;
        private bool _AjustadoPostCierre;
        private decimal _CantidadAjustada;
        private decimal _PorcentajeCostoEstimado;
        private decimal _PorcentajeCostoCierre;
        private decimal _Costo;
        private string _CodigoLote;
        private eTipoArticuloInv _TipoArticuloInv;
        private DateTime _FechaDeElaboracion;
        private DateTime _FechaDeVencimiento;
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
            set { _CodigoArticulo = LibString.Mid(value, 0, 30); }
        }

        public string DescripcionArticulo {
            get { return _DescripcionArticulo; }
            set { _DescripcionArticulo = LibString.Mid(value, 0, 255); }
        }

        public string UnidadDeVenta {
            get { return _UnidadDeVenta; }
            set { 
                _UnidadDeVenta = LibString.Mid(value, 0, 20);
                OnPropertyChanged("UnidadDeVenta");
            }
        }

        public decimal CantidadOriginalLista {
            get { return _CantidadOriginalLista; }
            set { 
                _CantidadOriginalLista = value;
            }
        }
        public decimal CantidadSolicitada {
            get { return _CantidadSolicitada; }
            set { 
                _CantidadSolicitada = value;
                OnPropertyChanged("CantidadSolicitada");
            }
        }

        public decimal CantidadProducida {
            get { return _CantidadProducida; }
            set { 
                _CantidadProducida = value;
                OnPropertyChanged("CantidadProducida");
            }
        }

        public decimal CostoUnitario {
            get { return _CostoUnitario; }
            set { _CostoUnitario = value; }
        }

        public decimal MontoSubTotal {
            get { return _MontoSubTotal; }
            set { _MontoSubTotal = value; }
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

        public decimal PorcentajeCostoEstimado {
            get { return _PorcentajeCostoEstimado; }
            set { 
                _PorcentajeCostoEstimado = value;
                OnPropertyChanged("PorcentajeCostoEstimado");
            }
        }
        public decimal PorcentajeCostoCierre {
            get { return _PorcentajeCostoCierre; }
            set { 
                _PorcentajeCostoCierre = value;
                OnPropertyChanged("PorcentajeCostoCierre");
            }
        }

        public decimal Costo {
            get { return _Costo; }
            set { 
                _Costo = value;
                OnPropertyChanged("Costo");
            }
        }

        public int ConsecutivoLoteDeInventario {
            get { return _ConsecutivoLoteDeInventario; }
            set { _ConsecutivoLoteDeInventario = value; }
        }

        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = value; }
        }

        public eTipoArticuloInv TipoArticuloInvAsEnum {
            get { return _TipoArticuloInv; }
            set { _TipoArticuloInv = value; }
        }

        public string TipoArticuloInv {
            set { _TipoArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(value); }
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

        public OrdenDeProduccionDetalleArticulo() {
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
            ConsecutivoLoteDeInventario = 0;
            CodigoAlmacen = string.Empty;
            NombreAlmacen = string.Empty;
            CodigoArticulo = string.Empty;
            DescripcionArticulo = string.Empty;
            UnidadDeVenta = string.Empty;
            CantidadOriginalLista = 0;
            CantidadSolicitada = 0;
            CantidadProducida = 0;
            CostoUnitario = 0;
            MontoSubTotal = 0;
            AjustadoPostCierreAsBool = false;
            CantidadAjustada = 0;
            PorcentajeCostoEstimado = 0;
            PorcentajeCostoCierre = 0;
            Costo = 0;
        }

        public OrdenDeProduccionDetalleArticulo Clone() {
            OrdenDeProduccionDetalleArticulo vResult = new OrdenDeProduccionDetalleArticulo();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoOrdenDeProduccion = _ConsecutivoOrdenDeProduccion;
            vResult.Consecutivo = _Consecutivo;
            vResult.ConsecutivoAlmacen = _ConsecutivoAlmacen;
            vResult.ConsecutivoLoteDeInventario = _ConsecutivoLoteDeInventario;
            vResult.CodigoAlmacen = _CodigoAlmacen;
            vResult.NombreAlmacen = _NombreAlmacen;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.DescripcionArticulo = _DescripcionArticulo;
            vResult.UnidadDeVenta = _UnidadDeVenta;
            vResult.CantidadOriginalLista = _CantidadOriginalLista;
            vResult.CantidadSolicitada = _CantidadSolicitada;
            vResult.CantidadProducida = _CantidadProducida;
            vResult.CostoUnitario = _CostoUnitario;
            vResult.MontoSubTotal = _MontoSubTotal;
            vResult.AjustadoPostCierreAsBool = _AjustadoPostCierre;
            vResult.CantidadAjustada = _CantidadAjustada;
            vResult.PorcentajeCostoEstimado = _PorcentajeCostoEstimado;
            vResult.PorcentajeCostoCierre = _PorcentajeCostoCierre;
            vResult.Costo = _Costo;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Orden De Produccion = " + _ConsecutivoOrdenDeProduccion.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nConsecutivo Almacen = " + _ConsecutivoAlmacen.ToString() +
               "\nConsecutivo Lote De Inventario = " + _ConsecutivoLoteDeInventario.ToString() +
               "\nCódigo de Artículo = " + _CodigoArticulo +
               "\nCantidad Original Lista = " + _CantidadOriginalLista.ToString() +
               "\nCantidad Solicitada = " + _CantidadSolicitada.ToString() +
               "\nCantidad Producida = " + _CantidadProducida.ToString() +
               "\nMontoSubTotal/CantidadProducidad = " + _CostoUnitario.ToString() +
               "\nACUM OrdenDeProduccionDetalleMateriales.MontoSubTotal = " + _MontoSubTotal.ToString() +
               "\nAjusta por Cierre = " + _AjustadoPostCierre +
               "\nCantidad Ajustada = " + _CantidadAjustada.ToString() +
               "\n% Costo Est. = " + _PorcentajeCostoEstimado.ToString() +
               "\n% Costo Cierre = " + _PorcentajeCostoCierre.ToString() +
               "\nCosto = " + _Costo.ToString();
        }

        #region Miembros de IEquatable<OrdenDeProduccionDetalleArticulo>
        bool IEquatable<OrdenDeProduccionDetalleArticulo>.Equals(OrdenDeProduccionDetalleArticulo other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<OrdenDeProduccionDetalleArticulo>

        #region Miembros de ICloneable<OrdenDeProduccionDetalleArticulo>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<OrdenDeProduccionDetalleArticulo>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class OrdenDeProduccionDetalleArticulo

} //End of namespace Galac.Adm.Ccl.GestionProduccion

