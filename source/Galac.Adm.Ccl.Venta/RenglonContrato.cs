using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta
{
    [Serializable]
    public class RenglonContrato: IEquatable<RenglonContrato>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroContrato;
        private int _ConsecutivoContrato;
        private string _Articulo;
        private string _Descripcion;
        //private eValorDelRenglon _CambioDeValorDelItem;
        private decimal _Imponible;
        private decimal _Cantidad;
        private ePeriodicidadContratos _Periodicidad;
        private ePeriodoDeAplicacion _PeriodoDeAplicacion;
        private DateTime _FechaDeInicio;
        private DateTime _FechaFinal;
        private DateTime _FechaPrimeraFactura;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string NumeroContrato {
            get { return _NumeroContrato; }
            set { _NumeroContrato = LibString.Mid(value, 0, 5); }
        }

        public int ConsecutivoContrato {
            get { return _ConsecutivoContrato; }
            set { _ConsecutivoContrato = value; }
        }

        public string Articulo {
            get { return _Articulo; }
            set { 
                _Articulo = LibString.Mid(value, 0, 15);
                OnPropertyChanged("Articulo");
            }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { 
                _Descripcion = LibString.Mid(value, 0, 255);
                OnPropertyChanged("Descripcion");
            }
        }

        //public eValorDelRenglon CambioDeValorDelItemAsEnum {
        //    get { return _CambioDeValorDelItem; }
        //    set { 
        //        _CambioDeValorDelItem = value;
        //        OnPropertyChanged("CambioDeValorDelItem");
        //    }
        //}

        //public string CambioDeValorDelItem {
        //    set { _CambioDeValorDelItem = (eValorDelRenglon)LibConvert.DbValueToEnum(value); }
        //}

        //public string CambioDeValorDelItemAsDB {
            //get { return LibConvert.EnumToDbValue((int) _CambioDeValorDelItem); }
        //}

        //public string CambioDeValorDelItemAsString {
        //    get { return LibEnumHelper.GetDescription(_CambioDeValorDelItem); }
        //}

        public decimal Imponible {
            get { return _Imponible; }
            set { 
                _Imponible = value;
                OnPropertyChanged("Imponible");
            }
        }

        public decimal Cantidad {
            get { return _Cantidad; }
            set { 
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        public ePeriodicidadContratos PeriodicidadAsEnum {
            get { return _Periodicidad; }
            set { 
                _Periodicidad = value;
                OnPropertyChanged("Periodicidad");
            }
        }

        public string Periodicidad {
            set { _Periodicidad = (ePeriodicidadContratos)LibConvert.DbValueToEnum(value); }
        }

        public string PeriodicidadAsDB {
            get { return LibConvert.EnumToDbValue((int) _Periodicidad); }
        }

        public string PeriodicidadAsString {
            get { return LibEnumHelper.GetDescription(_Periodicidad); }
        }

        public ePeriodoDeAplicacion PeriodoDeAplicacionAsEnum {
            get { return _PeriodoDeAplicacion; }
            set { 
                _PeriodoDeAplicacion = value;
                OnPropertyChanged("PeriodoDeAplicacion");
            }
        }

        public string PeriodoDeAplicacion {
            set { _PeriodoDeAplicacion = (ePeriodoDeAplicacion)LibConvert.DbValueToEnum(value); }
        }

        public string PeriodoDeAplicacionAsDB {
            get { return LibConvert.EnumToDbValue((int) _PeriodoDeAplicacion); }
        }

        public string PeriodoDeAplicacionAsString {
            get { return LibEnumHelper.GetDescription(_PeriodoDeAplicacion); }
        }

        public DateTime FechaDeInicio {
            get { return _FechaDeInicio; }
            set { 
                _FechaDeInicio = LibConvert.DateToDbValue(value);
                OnPropertyChanged("FechaDeInicio");
            }
        }

        public DateTime FechaFinal {
            get { return _FechaFinal; }
            set { 
                _FechaFinal = LibConvert.DateToDbValue(value);
                OnPropertyChanged("FechaFinal");
            }
        }

        public DateTime FechaPrimeraFactura {
            get { return _FechaPrimeraFactura; }
            set { 
                _FechaPrimeraFactura = LibConvert.DateToDbValue(value);
                OnPropertyChanged("FechaPrimeraFactura");
            }
        }
        #endregion //Propiedades
        #region Constructores

        public RenglonContrato() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            NumeroContrato = string.Empty;
            ConsecutivoContrato = 0;
            Articulo = string.Empty;
            Descripcion = string.Empty;
            //CambioDeValorDelItemAsEnum = eValorDelRenglon.ValorenArchivoArticulos;
            Imponible = 0;
            Cantidad = 0;
            PeriodicidadAsEnum = ePeriodicidadContratos.Mensual;
            PeriodoDeAplicacionAsEnum = ePeriodoDeAplicacion.EldelContrato;
            FechaDeInicio = LibDate.Today();
            FechaFinal = LibDate.Today();
            FechaPrimeraFactura = LibDate.Today();
        }

        public RenglonContrato Clone() {
            RenglonContrato vResult = new RenglonContrato();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.NumeroContrato = _NumeroContrato;
            vResult.ConsecutivoContrato = _ConsecutivoContrato;
            vResult.Articulo = _Articulo;
            vResult.Descripcion = _Descripcion;
            //vResult.CambioDeValorDelItemAsEnum = _CambioDeValorDelItem;
            vResult.Imponible = _Imponible;
            vResult.Cantidad = _Cantidad;
            vResult.PeriodicidadAsEnum = _Periodicidad;
            vResult.PeriodoDeAplicacionAsEnum = _PeriodoDeAplicacion;
            vResult.FechaDeInicio = _FechaDeInicio;
            vResult.FechaFinal = _FechaFinal;
            vResult.FechaPrimeraFactura = _FechaPrimeraFactura;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNumero Contrato = " + _NumeroContrato +
               "\nConsecutivo Contrato = " + _ConsecutivoContrato.ToString() +
               "\nCódigo Inventario = " + _Articulo +
               "\nDescripción = " + _Descripcion +
               "\nImponible = " + _Imponible.ToString() +
               "\nCantidad = " + _Cantidad.ToString() +
               "\nPeriodicidad = " + _Periodicidad.ToString() +
               "\nPeriodo De Aplicacion = " + _PeriodoDeAplicacion.ToString() +
               "\nFecha De Inicio = " + _FechaDeInicio.ToShortDateString() +
               "\nFecha Final = " + _FechaFinal.ToShortDateString() +
               "\nFecha Primera Factura = " + _FechaPrimeraFactura.ToShortDateString();
        }

        #region Miembros de IEquatable<RenglonContrato>
        bool IEquatable<RenglonContrato>.Equals(RenglonContrato other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<RenglonContrato>

        #region Miembros de ICloneable<RenglonContrato>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<RenglonContrato>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class RenglonContrato

} //End of namespace Galac.Adm.Ccl.Venta

