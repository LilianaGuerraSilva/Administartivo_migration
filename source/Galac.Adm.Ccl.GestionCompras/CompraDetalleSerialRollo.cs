using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.GestionCompras {
    [Serializable]
    public class CompraDetalleSerialRollo: IEquatable<CompraDetalleSerialRollo>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoCompra;
        private int _Consecutivo;
        private string _CodigoArticulo;
        private string _Serial;
        private string _Rollo;
        private decimal _Cantidad;
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
            set { _CodigoArticulo = LibString.Mid(value, 0, 30); }
        }

        public string Serial {
            get { return _Serial; }
            set { _Serial = LibString.Mid(value, 0, 50); }
        }

        public string Rollo {
            get { return _Rollo; }
            set { _Rollo = LibString.Mid(value, 0, 20); }
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

        public CompraDetalleSerialRollo() {
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
            Serial = string.Empty;
            Rollo = string.Empty;
            Cantidad = 0;
        }

        public CompraDetalleSerialRollo Clone() {
            CompraDetalleSerialRollo vResult = new CompraDetalleSerialRollo();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoCompra = _ConsecutivoCompra;
            vResult.Consecutivo = _Consecutivo;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.Serial = _Serial;
            vResult.Rollo = _Rollo;
            vResult.Cantidad = _Cantidad;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Compra = " + _ConsecutivoCompra.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nCodigo Articulo = " + _CodigoArticulo +
               "\nSerial = " + _Serial +
               "\nRollo = " + _Rollo +
               "\nCantidad = " + _Cantidad.ToString();
        }

        #region Miembros de IEquatable<CompraDetalleSerialRollo>
        bool IEquatable<CompraDetalleSerialRollo>.Equals(CompraDetalleSerialRollo other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<CompraDetalleSerialRollo>

        #region Miembros de ICloneable<CompraDetalleSerialRollo>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<CompraDetalleSerialRollo>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class CompraDetalleSerialRollo

} //End of namespace Galac.Adm.Ccl.GestionCompras

