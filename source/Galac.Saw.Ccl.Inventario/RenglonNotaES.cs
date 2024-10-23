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
    public class RenglonNotaES: IEquatable<RenglonNotaES>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroDocumento;
        private int _ConsecutivoRenglon;
        private string _CodigoArticulo;
        private string _DescripcionArticulo;
        private decimal _Cantidad;
        private eTipoArticuloInv _TipoArticuloInv;
        private string _Serial;
        private string _Rollo;
        private decimal _CostoUnitario;
        private decimal _CostoUnitarioME;
        private string _LoteDeInventario;
        private DateTime _FechaDeElaboracion;
        private DateTime _FechaDeVencimiento;
        #endregion //Variables
        #region Propiedades
        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string NumeroDocumento {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = LibString.Mid(value, 0, 11); }
        }

        public int ConsecutivoRenglon {
            get { return _ConsecutivoRenglon; }
            set { _ConsecutivoRenglon = value; }
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
            set { 
                _DescripcionArticulo = LibString.Mid(value, 0, 255);
                OnPropertyChanged("DescripcionArticulo");
            }
        }

        public decimal Cantidad {
            get { return _Cantidad; }
            set { 
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        public eTipoArticuloInv TipoArticuloInvAsEnum {
            get { return _TipoArticuloInv; }
            set { _TipoArticuloInv = value; }
        }

        public string TipoArticuloInv {
            set { _TipoArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(value); }
        }

        public string TipoArticuloInvAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoArticuloInv); }
        }

        public string TipoArticuloInvAsString {
            get { return LibEnumHelper.GetDescription(_TipoArticuloInv); }
        }

        public string Serial {
            get { return _Serial; }
            set { _Serial = LibString.Mid(value, 0, 50); }
        }

        public string Rollo {
            get { return _Rollo; }
            set { _Rollo = LibString.Mid(value, 0, 20); }
        }

        public decimal CostoUnitario {
            get { return _CostoUnitario; }
            set { _CostoUnitario = value; }
        }

        public decimal CostoUnitarioME {
            get { return _CostoUnitarioME; }
            set { _CostoUnitarioME = value; }
        }

        public string LoteDeInventario {
            get { return _LoteDeInventario; }
            set { 
                _LoteDeInventario = LibString.Mid(value, 0, 30);
                OnPropertyChanged("LoteDeInventario");
            }
        }

        public DateTime FechaDeElaboracion {
            get { return _FechaDeElaboracion; }
            set { 
                _FechaDeElaboracion = LibConvert.DateToDbValue(value);
                OnPropertyChanged("FechaDeElaboracion");
            }
        }

        public DateTime FechaDeVencimiento {
            get { return _FechaDeVencimiento; }
            set { 
                _FechaDeVencimiento = LibConvert.DateToDbValue(value);
                OnPropertyChanged("FechaDeVencimiento");
            }
        }
        #endregion //Propiedades
        #region Constructores
        public RenglonNotaES() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            NumeroDocumento = string.Empty;
            ConsecutivoRenglon = 0;
            CodigoArticulo = string.Empty;
            DescripcionArticulo = string.Empty;
            Cantidad = 0;
            TipoArticuloInvAsEnum = eTipoArticuloInv.Simple;
            Serial = string.Empty;
            Rollo = string.Empty;
            CostoUnitario = 0;
            CostoUnitarioME = 0;
            LoteDeInventario = string.Empty;
            FechaDeElaboracion = LibDate.MinDateForDB();
            FechaDeVencimiento = LibDate.MaxDateForDB();
        }

        public RenglonNotaES Clone() {
            RenglonNotaES vResult = new RenglonNotaES();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.NumeroDocumento = _NumeroDocumento;
            vResult.ConsecutivoRenglon = _ConsecutivoRenglon;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.DescripcionArticulo = _DescripcionArticulo;
            vResult.Cantidad = _Cantidad;
            vResult.TipoArticuloInvAsEnum = _TipoArticuloInv;
            vResult.Serial = _Serial;
            vResult.Rollo = _Rollo;
            vResult.CostoUnitario = _CostoUnitario;
            vResult.CostoUnitarioME = _CostoUnitarioME;
            vResult.LoteDeInventario = _LoteDeInventario;
            vResult.FechaDeElaboracion = _FechaDeElaboracion;
            vResult.FechaDeVencimiento = _FechaDeVencimiento;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNumero Documento = " + _NumeroDocumento +
               "\nConsecutivo Renglon = " + _ConsecutivoRenglon.ToString() +
               "\nCodigo Articulo = " + _CodigoArticulo +
               "\nCantidad = " + _Cantidad.ToString() +
               "\nTipo Articulo Inv = " + _TipoArticuloInv.ToString() +
               "\nSerial = " + _Serial +
               "\nRollo = " + _Rollo +
               "\nCosto Unitario = " + _CostoUnitario.ToString() +
               "\nCosto Unitario ME = " + _CostoUnitarioME.ToString() +
               "\nLote De Inventario = " + _LoteDeInventario +
               "\nFecha De Elaboracion = " + _FechaDeElaboracion.ToShortDateString() +
               "\nFecha De Vencimiento = " + _FechaDeVencimiento.ToShortDateString();
        }

        #region Miembros de IEquatable<RenglonNotaES>
        bool IEquatable<RenglonNotaES>.Equals(RenglonNotaES other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<RenglonNotaES>

        #region Miembros de ICloneable<RenglonNotaES>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<RenglonNotaES>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class RenglonNotaES

} //End of namespace Galac.Dbo.Ccl.Inventario

