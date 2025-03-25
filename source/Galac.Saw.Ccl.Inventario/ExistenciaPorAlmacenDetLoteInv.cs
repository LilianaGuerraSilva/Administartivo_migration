using System;
using System.ComponentModel;
using LibGalac.Aos.Base;


namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class ExistenciaPorAlmacenDetLoteInv: IEquatable<ExistenciaPorAlmacenDetLoteInv>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private string _CodigoAlmacen;
        private string _CodigoArticulo;
        private int _ConsecutivoLoteInventario;
        private decimal _Cantidad;
        private string _Ubicacion;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string CodigoAlmacen {
            get { return _CodigoAlmacen; }
            set { _CodigoAlmacen = LibString.Mid(value, 0, 5); }
        }

        public string CodigoArticulo {
            get { return _CodigoArticulo; }
            set { _CodigoArticulo = LibString.Mid(value, 0, 30); }
        }

        public int ConsecutivoLoteInventario {
            get { return _ConsecutivoLoteInventario; }
            set { _ConsecutivoLoteInventario = value; }
        }

        public decimal Cantidad {
            get { return _Cantidad; }
            set { 
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        public string Ubicacion {
            get { return _Ubicacion; }
            set { _Ubicacion = LibString.Mid(value, 0, 30); }
        }
        #endregion //Propiedades
        #region Constructores

        public ExistenciaPorAlmacenDetLoteInv() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            CodigoAlmacen = string.Empty;
            CodigoArticulo = string.Empty;
            ConsecutivoLoteInventario = 0;
            Cantidad = 0;
            Ubicacion = string.Empty;
        }

        public ExistenciaPorAlmacenDetLoteInv Clone() {
            ExistenciaPorAlmacenDetLoteInv vResult = new ExistenciaPorAlmacenDetLoteInv();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.CodigoAlmacen = _CodigoAlmacen;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.ConsecutivoLoteInventario = _ConsecutivoLoteInventario;
            vResult.Cantidad = _Cantidad;
            vResult.Ubicacion = _Ubicacion;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCodigo Almacen = " + _CodigoAlmacen +
               "\nCodigo Articulo = " + _CodigoArticulo +
               "\nConsecutivo Lote Inventario = " + _ConsecutivoLoteInventario.ToString() +
               "\nCantidad = " + _Cantidad.ToString() +
               "\nUbicación = " + _Ubicacion;
        }

        #region Miembros de IEquatable<ExistenciaPorAlmacenDetLoteInv>
        bool IEquatable<ExistenciaPorAlmacenDetLoteInv>.Equals(ExistenciaPorAlmacenDetLoteInv other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<ExistenciaPorAlmacenDetLoteInv>

        #region Miembros de ICloneable<ExistenciaPorAlmacenDetLoteInv>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<ExistenciaPorAlmacenDetLoteInv>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class ExistenciaPorAlmacenDetLoteInv

} //End of namespace Galac.Saw.Ccl.Inventario

