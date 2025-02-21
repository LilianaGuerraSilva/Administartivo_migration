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
    public class ListaDeMaterialesDetalleSalidas: IEquatable<ListaDeMaterialesDetalleSalidas>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoListaDeMateriales;
        private int _Consecutivo;
        private string _CodigoArticuloInventario;
        private string _DescripcionArticuloInventario;
        private decimal _Cantidad;
        private string _UnidadDeVenta;
        private decimal _PorcentajeDeCosto;
        private eTipoArticuloInv _TipoArticuloInv;
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
            set { _DescripcionArticuloInventario = LibString.Mid(value, 0, 7000); }
        }

        public decimal Cantidad {
            get { return _Cantidad; }
            set { 
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        public string UnidadDeVenta {
            get { return _UnidadDeVenta; }
            set { 
                _UnidadDeVenta = LibString.Mid(value, 0, 20);
            }
        }

        public decimal PorcentajeDeCosto {
            get { return _PorcentajeDeCosto; }
            set { 
                _PorcentajeDeCosto = value;
                OnPropertyChanged("PorcentajeDeCosto");
            }
        }

        public eTipoArticuloInv TipoArticuloInvAsEnum {
            get { return _TipoArticuloInv; }
            set { _TipoArticuloInv = value; }
        }
        public string TipoArticuloInv {
            set { _TipoArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(value); }
        }
        #endregion //Propiedades
        #region Constructores

        public ListaDeMaterialesDetalleSalidas() {
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
            PorcentajeDeCosto = 0;
        }

        public ListaDeMaterialesDetalleSalidas Clone() {
            ListaDeMaterialesDetalleSalidas vResult = new ListaDeMaterialesDetalleSalidas();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoListaDeMateriales = _ConsecutivoListaDeMateriales;
            vResult.Consecutivo = _Consecutivo;
            vResult.CodigoArticuloInventario = _CodigoArticuloInventario;
            vResult.DescripcionArticuloInventario = _DescripcionArticuloInventario;
            vResult.Cantidad = _Cantidad;
            vResult.UnidadDeVenta = _UnidadDeVenta;
            vResult.PorcentajeDeCosto = _PorcentajeDeCosto;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Lista De Materiales = " + _ConsecutivoListaDeMateriales.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nCódigo Inventario = " + _CodigoArticuloInventario +
               "\nCantidad = " + _Cantidad.ToString() +
               "\n%Costo = " + _PorcentajeDeCosto.ToString();
        }

        #region Miembros de IEquatable<ListaDeMaterialesDetalleSalidas>
        bool IEquatable<ListaDeMaterialesDetalleSalidas>.Equals(ListaDeMaterialesDetalleSalidas other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<ListaDeMaterialesDetalleSalidas>

        #region Miembros de ICloneable<ListaDeMaterialesDetalleSalidas>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<ListaDeMaterialesDetalleSalidas>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class ListaDeMaterialesDetalleSalidas

} //End of namespace Galac.Adm.Ccl.GestionProduccion

