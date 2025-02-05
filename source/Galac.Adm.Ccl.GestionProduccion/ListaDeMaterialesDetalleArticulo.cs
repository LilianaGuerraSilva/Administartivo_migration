using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Ccl.GestionProduccion {
    [Serializable]
    public class ListaDeMaterialesDetalleArticulo: IEquatable<ListaDeMaterialesDetalleArticulo>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoListaDeMateriales;
        private int _Consecutivo;
        private string _CodigoArticuloInventario;
        private string _DescripcionArticuloInventario;
        private decimal _Cantidad;
        private eTipoDeArticulo _TipoDeArticulo;
		private string _UnidadDeVenta;
        private eTipoArticuloInv _TipoArticuloInv;
		private decimal _MermaNormal;
        private decimal _PorcentajeMermaNormal;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int ConsecutivoListaDeMateriales {
            get { return _ConsecutivoListaDeMateriales; }
            set { _ConsecutivoListaDeMateriales = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public string CodigoArticuloInventario {
            get { return _CodigoArticuloInventario; }
            set { 
                _CodigoArticuloInventario = LibString.Mid(value, 0, 30);
                OnPropertyChanged("CodigoArticuloInventario");
            }
        }

        public string DescripcionArticuloInventario {
            get { return _DescripcionArticuloInventario; }
            set { 
                _DescripcionArticuloInventario = LibString.Mid(value, 0, 7000);
                OnPropertyChanged("DescripcionArticuloInventario");
            }
        }

        public decimal Cantidad {
            get { return _Cantidad; }
            set { 
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        public eTipoDeArticulo TipoDeArticuloAsEnum {
            get { return _TipoDeArticulo; }
            set { _TipoDeArticulo = value; }
        }       
        public string TipoDeArticulo {
            set { _TipoDeArticulo = (eTipoDeArticulo)LibConvert.DbValueToEnum(value); }
        }
		
		public string UnidadDeVenta {
            get { return _UnidadDeVenta; }
            set { 
                _UnidadDeVenta = LibString.Mid(value, 0, 20);
                OnPropertyChanged("UnidadDeVenta");
            }
        }

        public eTipoArticuloInv TipoArticuloInvAsEnum {
            get { return _TipoArticuloInv; }
            set { _TipoArticuloInv = value; }
        }
        public string TipoArticuloInv {
            set { _TipoArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(value); }
        }
		
		public decimal MermaNormal {
            get { return _MermaNormal; }
            set { 
                _MermaNormal = value;
                OnPropertyChanged("MermaNormal");
            }
        }

        public decimal PorcentajeMermaNormal {
            get { return _PorcentajeMermaNormal; }
            set { 
                _PorcentajeMermaNormal = value;
                OnPropertyChanged("PorcentajeMermaNormal");
            }
        }
        #endregion //Propiedades
        #region Constructores

        public ListaDeMaterialesDetalleArticulo() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ConsecutivoListaDeMateriales = 0;
            Consecutivo = 0;
            CodigoArticuloInventario = string.Empty;
            DescripcionArticuloInventario = string.Empty;
            Cantidad = 0;
            UnidadDeVenta = string.Empty;
            MermaNormal = 0;
            PorcentajeMermaNormal = 0;
        }

        public ListaDeMaterialesDetalleArticulo Clone() {
            ListaDeMaterialesDetalleArticulo vResult = new ListaDeMaterialesDetalleArticulo();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoListaDeMateriales = _ConsecutivoListaDeMateriales;
            vResult.Consecutivo = _Consecutivo;
            vResult.CodigoArticuloInventario = _CodigoArticuloInventario;
            vResult.DescripcionArticuloInventario = _DescripcionArticuloInventario;
            vResult.Cantidad = _Cantidad;
            vResult.UnidadDeVenta = _UnidadDeVenta;
            vResult.MermaNormal = _MermaNormal;
            vResult.PorcentajeMermaNormal = _PorcentajeMermaNormal;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Lista De Materiales = " + _ConsecutivoListaDeMateriales.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nCódigo Inventario = " + _CodigoArticuloInventario +
               "\nCantidad = " + _Cantidad.ToString() +
               "\nMerma Normal (esperada) = " + _MermaNormal.ToString() +
               "\n%Merma = " + _PorcentajeMermaNormal.ToString();
        }

        #region Miembros de IEquatable<ListaDeMaterialesDetalleArticulo>
        bool IEquatable<ListaDeMaterialesDetalleArticulo>.Equals(ListaDeMaterialesDetalleArticulo other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<ListaDeMaterialesDetalleArticulo>

        #region Miembros de ICloneable<ListaDeMaterialesDetalleArticulo>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<ListaDeMaterialesDetalleArticulo>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class ListaDeMaterialesDetalleArticulo

} //End of namespace Galac.Adm.Ccl.GestionProduccion

