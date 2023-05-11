using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.Vendedor;

namespace Galac.Adm.Ccl.Vendedor{
    [Serializable]
    public class VendedorDetalleComisiones: IEquatable<VendedorDetalleComisiones>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoVendedor;
        private int _ConsecutivoRenglon;
        private int _CodigoVendedor;
        private string _NombreDeLineaDeProducto;
        private eTipoComision _TipoDeComision;
        private decimal _Monto;
        private decimal _Porcentaje;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int ConsecutivoVendedor {
            get { return _ConsecutivoVendedor; }
            set { _ConsecutivoVendedor = value; }
        }

        public int ConsecutivoRenglon {
            get { return _ConsecutivoRenglon; }
            set { _ConsecutivoRenglon = value; }
        }
        public int CodigoVendedor {
            get { return _CodigoVendedor; }
            set { _CodigoVendedor = value; }
        }
        public string NombreDeLineaDeProducto {
            get { return _NombreDeLineaDeProducto; }
            set { 
                _NombreDeLineaDeProducto = LibString.Mid(value, 0, 20);
                OnPropertyChanged("NombreDeLineaDeProducto");
            }
        }

        public eTipoComision TipoDeComisionAsEnum {
            get { return _TipoDeComision; }
            set { 
                _TipoDeComision = value;
                OnPropertyChanged("TipoDeComision");
            }
        }

        public string TipoDeComision {
            set { _TipoDeComision = (eTipoComision)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeComisionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeComision); }
        }

        public string TipoDeComisionAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeComision); }
        }

        public decimal Monto {
            get { return _Monto; }
            set { 
                _Monto = value;
                OnPropertyChanged("Monto");
            }
        }

        public decimal Porcentaje {
            get { return _Porcentaje; }
            set { 
                _Porcentaje = value;
                OnPropertyChanged("Porcentaje");
            }
        }
        #endregion //Propiedades
        #region Constructores

        public VendedorDetalleComisiones() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ConsecutivoVendedor = 0;
            ConsecutivoRenglon = 0;
            CodigoVendedor = 0;
            NombreDeLineaDeProducto = string.Empty;
            TipoDeComisionAsEnum = eTipoComision.PorPorcentaje;
            Monto = 0;
            Porcentaje = 0;
        }

        public VendedorDetalleComisiones Clone() {
            VendedorDetalleComisiones vResult = new VendedorDetalleComisiones();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoVendedor = _ConsecutivoVendedor;
            vResult.ConsecutivoRenglon = _ConsecutivoRenglon;
            vResult.CodigoVendedor = _CodigoVendedor;
            vResult.NombreDeLineaDeProducto = _NombreDeLineaDeProducto;
            vResult.TipoDeComisionAsEnum = _TipoDeComision;
            vResult.Monto = _Monto;
            vResult.Porcentaje = _Porcentaje;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Vendedor = " + _ConsecutivoVendedor.ToString() +
               "\nConsecutivo Renglon = " + _ConsecutivoRenglon.ToString() +
               "\nCodigo Vendedor = " + _CodigoVendedor.ToString() +
               "\nNombre De Linea De Producto = " + _NombreDeLineaDeProducto +
               "\nTipo De Comision = " + _TipoDeComision.ToString() +
               "\nMonto = " + _Monto.ToString() +
               "\nPorcentaje = " + _Porcentaje.ToString();
        }

        #region Miembros de IEquatable<VendedorDetalleComisiones>
        bool IEquatable<VendedorDetalleComisiones>.Equals(VendedorDetalleComisiones other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<VendedorDetalleComisiones>

        #region Miembros de ICloneable<VendedorDetalleComisiones>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<VendedorDetalleComisiones>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class VendedorDetalleComisiones

} //End of namespace Galac.Adm.Ccl.Vendedor

