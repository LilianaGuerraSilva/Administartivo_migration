using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.GestionCompras {
    [Serializable]
    public class CompraDetalleGasto: IEquatable<CompraDetalleGasto>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoCompra;
        private int _ConsecutivoCxP;
        private int _ConsecutivoRenglon;
        string _CxpNumero;
        private eTipoDeCosto _TipoDeCosto;
        private decimal _Monto;
        private string _CodigoProveedor;
        private string _NombreProveedor;
        private string _CodigoMoneda;
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

        public int ConsecutivoCxP {
            get { return _ConsecutivoCxP; }
            set { _ConsecutivoCxP = value; }
        }

        public string CxpNumero {
            get { return _CxpNumero; }
            set { 
                _CxpNumero = LibString.Mid(value, 0, 13);
                OnPropertyChanged("CxpNumero");
            }
        }

        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = value;}
        }

        public int ConsecutivoRenglon {
            get { return _ConsecutivoRenglon; }
            set { _ConsecutivoRenglon = value; }
        }

        public eTipoDeCosto TipoDeCostoAsEnum {
            get { return _TipoDeCosto; }
            set { _TipoDeCosto = value; }
        }

        public string TipoDeCosto {
            set { _TipoDeCosto = (eTipoDeCosto)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeCostoAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeCosto); }
        }

        public string TipoDeCostoAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeCosto); }
        }

        public decimal Monto {
            get { return _Monto; }
            set { _Monto = value; }
        }

        public string CodigoProveedor {
            get { return _CodigoProveedor; }
            set { 
                _CodigoProveedor = LibString.Mid(value, 0, 10);
                OnPropertyChanged("CodigoProveedor");
            }
        }

        public string NombreProveedor {
            get { return _NombreProveedor; }
            set { 
                _NombreProveedor = LibString.Mid(value, 0, 60);
                OnPropertyChanged("NombreProveedor");
            }
        }
        #endregion //Propiedades
        #region Constructores

        public CompraDetalleGasto() {
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
            ConsecutivoCxP = 0;
            CxpNumero = string.Empty;
            ConsecutivoRenglon = 0;
            TipoDeCostoAsEnum = eTipoDeCosto.FleteInternacional;
            Monto = 0;
            CodigoProveedor = string.Empty;
            NombreProveedor = string.Empty;
            CodigoMoneda = string.Empty;
        }

        public CompraDetalleGasto Clone() {
            CompraDetalleGasto vResult = new CompraDetalleGasto();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoCompra = _ConsecutivoCompra;
            vResult.ConsecutivoCxP = _ConsecutivoCxP;
            vResult.CxpNumero = _CxpNumero;
            vResult.ConsecutivoRenglon = _ConsecutivoRenglon;
            vResult.TipoDeCostoAsEnum = _TipoDeCosto;
            vResult.Monto = _Monto;
            vResult.CodigoProveedor = _CodigoProveedor;
            vResult.NombreProveedor = _NombreProveedor;
            vResult.CodigoMoneda = _CodigoMoneda;
            return vResult;
        }

        public override string ToString() {
            return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
                "\nConsecutivo Compra = " + _ConsecutivoCompra.ToString() +
                "\nConsecutivo Cx P = " + _ConsecutivoCxP.ToString() +
                "\nConsecutivo Renglon = " + _ConsecutivoRenglon.ToString() +
                "\nTipo De Costo = " + _TipoDeCosto.ToString() +
                "\nMonto = " + _Monto.ToString() +
                "\nCodigoMoneda = " + _CodigoMoneda.ToString();
        }

        #region Miembros de IEquatable<CompraDetalleGasto>
        bool IEquatable<CompraDetalleGasto>.Equals(CompraDetalleGasto other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<CompraDetalleGasto>

        #region Miembros de ICloneable<CompraDetalleGasto>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<CompraDetalleGasto>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class CompraDetalleGasto

} //End of namespace Galac.Adm.Ccl.GestionCompras

