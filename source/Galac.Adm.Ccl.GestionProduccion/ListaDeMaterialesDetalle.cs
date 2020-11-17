using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class ListaDeMaterialesDetalle: IEquatable<ListaDeMaterialesDetalle>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoListaDeMateriales;
        private int _Consecutivo;
        private string _CodigoArticuloInventario;
        private string _DescripcionArticuloInventario;
        private decimal _Cantidad;
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
        #endregion //Propiedades
        #region Constructores

        public ListaDeMaterialesDetalle() {
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
        }

        public ListaDeMaterialesDetalle Clone() {
            ListaDeMaterialesDetalle vResult = new ListaDeMaterialesDetalle();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoListaDeMateriales = _ConsecutivoListaDeMateriales;
            vResult.Consecutivo = _Consecutivo;
            vResult.CodigoArticuloInventario = _CodigoArticuloInventario;
            vResult.DescripcionArticuloInventario = _DescripcionArticuloInventario;
            vResult.Cantidad = _Cantidad;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Lista De Materiales = " + _ConsecutivoListaDeMateriales.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nCódigo Inventario = " + _CodigoArticuloInventario +
               "\nCantidad = " + _Cantidad.ToString();
        }

        #region Miembros de IEquatable<ListaDeMaterialesDetalle>
        bool IEquatable<ListaDeMaterialesDetalle>.Equals(ListaDeMaterialesDetalle other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<ListaDeMaterialesDetalle>

        #region Miembros de ICloneable<ListaDeMaterialesDetalle>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<ListaDeMaterialesDetalle>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class ListaDeMaterialesDetalle

} //End of namespace Galac.Saw.Ccl.Inventario

