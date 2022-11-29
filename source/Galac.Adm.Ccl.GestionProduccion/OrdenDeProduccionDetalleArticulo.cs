using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;


namespace Galac.Adm.Ccl.GestionProduccion {
    [Serializable]
    public class OrdenDeProduccionDetalleArticulo: IEquatable<OrdenDeProduccionDetalleArticulo>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoOrdenDeProduccion;
        private int _Consecutivo;
        private int _ConsecutivoListaDeMateriales;
        private string _CodigoListaDeMateriales;
        private string _NombreListaDeMateriales;
        private int _ConsecutivoAlmacen;
        private string _CodigoAlmacen;
        private string _NombreAlmacen;
        private string _CodigoArticulo;
        private string _DescripcionArticulo;
        private decimal _CantidadSolicitada;
        private decimal _CantidadProducida;
        private decimal _CostoUnitario;
        private decimal _MontoSubTotal;
        private bool _AjustadoPostCierre;
        private decimal _CantidadAjustada;
        private long _fldTimeStamp;
		private ObservableCollection<OrdenDeProduccionDetalleMateriales> _DetailOrdenDeProduccionDetalleMateriales;
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

        public int ConsecutivoListaDeMateriales {
            get { return _ConsecutivoListaDeMateriales; }
            set { _ConsecutivoListaDeMateriales = value; }
        }

        public string CodigoListaDeMateriales {
            get { return _CodigoListaDeMateriales; }
            set { _CodigoListaDeMateriales = LibString.Mid(value, 0, 30); }
        }

        public string NombreListaDeMateriales {
            get { return _NombreListaDeMateriales; }
            set { _NombreListaDeMateriales = LibString.Mid(value, 0, 255); }
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

        public decimal CantidadSolicitada {
            get { return _CantidadSolicitada; }
            set { _CantidadSolicitada = value; }
        }

        public decimal CantidadProducida {
            get { return _CantidadProducida; }
            set { _CantidadProducida = value; }
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

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public ObservableCollection<OrdenDeProduccionDetalleMateriales> DetailOrdenDeProduccionDetalleMateriales {
            get { return _DetailOrdenDeProduccionDetalleMateriales; }
            set { _DetailOrdenDeProduccionDetalleMateriales = value; }
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
            ConsecutivoListaDeMateriales = 0;
            CodigoListaDeMateriales = string.Empty;
            NombreListaDeMateriales = string.Empty;
            ConsecutivoAlmacen = 0;
            CodigoAlmacen = string.Empty;
            NombreAlmacen = string.Empty;
            CodigoArticulo = string.Empty;
            DescripcionArticulo = string.Empty;
            CantidadSolicitada = 0;
            CantidadProducida = 0;
            CostoUnitario = 0;
            MontoSubTotal = 0;
            AjustadoPostCierreAsBool = false;
            CantidadAjustada = 0;
            fldTimeStamp = 0;
            DetailOrdenDeProduccionDetalleMateriales = new ObservableCollection<OrdenDeProduccionDetalleMateriales>();
        }

        public OrdenDeProduccionDetalleArticulo Clone() {
            OrdenDeProduccionDetalleArticulo vResult = new OrdenDeProduccionDetalleArticulo();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoOrdenDeProduccion = _ConsecutivoOrdenDeProduccion;
            vResult.Consecutivo = _Consecutivo;
            vResult.ConsecutivoListaDeMateriales = _ConsecutivoListaDeMateriales;
            vResult.CodigoListaDeMateriales = _CodigoListaDeMateriales;
            vResult.NombreListaDeMateriales = _NombreListaDeMateriales;
            vResult.ConsecutivoAlmacen = _ConsecutivoAlmacen;
            vResult.CodigoAlmacen = _CodigoAlmacen;
            vResult.NombreAlmacen = _NombreAlmacen;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.DescripcionArticulo = _DescripcionArticulo;
            vResult.CantidadSolicitada = _CantidadSolicitada;
            vResult.CantidadProducida = _CantidadProducida;
            vResult.CostoUnitario = _CostoUnitario;
            vResult.MontoSubTotal = _MontoSubTotal;
            vResult.AjustadoPostCierreAsBool = _AjustadoPostCierre;
            vResult.CantidadAjustada = _CantidadAjustada;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Orden De Produccion = " + _ConsecutivoOrdenDeProduccion.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nConsecutivo Lista De Materiales = " + _ConsecutivoListaDeMateriales.ToString() +
               "\nConsecutivo Almacen = " + _ConsecutivoAlmacen.ToString() +
               "\nCódigo de Artículo = " + _CodigoArticulo +
               "\nCantidad Solicitada = " + _CantidadSolicitada.ToString() +
               "\nCantidad Producida = " + _CantidadProducida.ToString() +
               "\nCostoUnitario = " + _CostoUnitario.ToString() +
               "\nACUM OrdeProduccionDetalleMateriales.MontoSubtotal = " + _MontoSubTotal.ToString() +
               "\nAjusta por Cierre = " + _AjustadoPostCierre +
               "\nCantidad Ajustada = " + _CantidadAjustada.ToString();
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

