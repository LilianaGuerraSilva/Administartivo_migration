using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Ccl.CajaChica {
    [Serializable]
    public class DetalleDeRendicion : IEquatable<DetalleDeRendicion>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoRendicion;
        private int _ConsecutivoRenglon;
        private string _NumeroDocumento;
        private string _NumeroControl;
        private DateTime _Fecha;
        private string _CodigoProveedor;
        private string _NombreProveedor; 
        private decimal _MontoExento;
        private decimal _MontoGravable;
        private decimal _MontoIVA;
        private decimal _MontoRetencion;
        private bool _AplicaParaLibroDeCompras;
        private string _ObservacionesCxP;
        private eGeneradoPor _GeneradaPor;
		private bool _AplicaIvaAlicuotaEspecial;
        private decimal _MontoGravableAlicuotaEspecial1;
        private decimal _MontoIVAAlicuotaEspecial1;
        private decimal _PorcentajeIvaAlicuotaEspecial1;
        private decimal _MontoGravableAlicuotaEspecial2;
        private decimal _MontoIVAAlicuotaEspecial2;
        private decimal _PorcentajeIvaAlicuotaEspecial2;
        #region Variables Logicas
        private bool _Valido = true;
        private string _ErrorMsj; 
        #endregion
        
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { 
                _ConsecutivoCompania = value;
                OnPropertyChanged("ConsecutivoCompania");
            }
        }

        public int ConsecutivoRendicion {
            get { return _ConsecutivoRendicion; }
            set { 
                _ConsecutivoRendicion = value;
                OnPropertyChanged("ConsecutivoRendicion");
            }
        }

        public int ConsecutivoRenglon {
            get { return _ConsecutivoRenglon; }
            set { 
                _ConsecutivoRenglon = value;
                OnPropertyChanged("ConsecutivoRenglon");
            }
        }

        public string NumeroDocumento {
            get { return _NumeroDocumento; }
            set { 
                _NumeroDocumento =   LibString.Mid(value, 0, 25).TrimEnd();
                OnPropertyChanged("NumeroDocumento");
            }
        }

        public string NumeroControl {
            get { return _NumeroControl; }
            set { 
                _NumeroControl = LibString.Mid(value, 0, 20);
                OnPropertyChanged("NumeroControl");
            }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { 
                _Fecha = LibConvert.DateToDbValue(value);
                OnPropertyChanged("Fecha");
            }
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

        public decimal MontoExento {
            get { return _MontoExento; }
            set { 
                _MontoExento = value;
                OnPropertyChanged("MontoExento");
            }
        }

        public decimal MontoGravable {
            get { return _MontoGravable; }
            set { 
                _MontoGravable = value;
                OnPropertyChanged("MontoGravable");
            }
        }

        public decimal MontoIVA {
            get { return _MontoIVA; }
            set { 
                _MontoIVA = value;
                OnPropertyChanged("MontoIVA");
            }
        }

        public decimal MontoRetencion {
            get { return _MontoRetencion; }
            set { 
                _MontoRetencion = value;
                OnPropertyChanged("MontoRetencion");
            }
        }
		
        public bool AplicaParaLibroDeComprasAsBool {
            get { return _AplicaParaLibroDeCompras; }
            set { 
                _AplicaParaLibroDeCompras = value;
                OnPropertyChanged("AplicaParaLibroDeComprasAsBool");
        }

        }

        public string AplicaParaLibroDeCompras {
            set { _AplicaParaLibroDeCompras = LibConvert.SNToBool(value); }
        }


        public string ObservacionesCxP {
            get { return _ObservacionesCxP; }
            set {
                _ObservacionesCxP = LibString.Mid(value, 0, 50);
                OnPropertyChanged("ObservacionesCxP");
            }
        }
        #region Propiedades Logicas
        public bool ValidoAsBool {
            get { return _Valido; }
            set {
                _Valido = value;
                OnPropertyChanged("ValidoAsBool");
            }
        }

        public string ErrorMsj {
            get { return _ErrorMsj; }
            set {
                _ErrorMsj = value;
                OnPropertyChanged("ErrorMsj");
            }
            
        }
		public eGeneradoPor GeneradaPorAsEnum {
            get { return _GeneradaPor; }
            set { _GeneradaPor = value; }
        }

        public string GeneradaPor {
            set { _GeneradaPor = (eGeneradoPor)LibConvert.DbValueToEnum(value); }
        }

        public string GeneradaPorAsDB {
            get { return LibConvert.EnumToDbValue((int) _GeneradaPor); }
        }

        public string GeneradaPorAsString {
            get { return LibEnumHelper.GetDescription(_GeneradaPor); }
        } 
		
        public bool AplicaIvaAlicuotaEspecialAsBool {
            get { return _AplicaIvaAlicuotaEspecial; }
            set { _AplicaIvaAlicuotaEspecial = value; }
        }

        public string AplicaIvaAlicuotaEspecial {
            set { _AplicaIvaAlicuotaEspecial = LibConvert.SNToBool(value); }
        }


        public decimal MontoGravableAlicuotaEspecial1 {
            get { return _MontoGravableAlicuotaEspecial1; }
            set { _MontoGravableAlicuotaEspecial1 = value; }
        }

        public decimal MontoIVAAlicuotaEspecial1 {
            get { return _MontoIVAAlicuotaEspecial1; }
            set { _MontoIVAAlicuotaEspecial1 = value; }
        }

        public decimal PorcentajeIvaAlicuotaEspecial1 {
            get { return _PorcentajeIvaAlicuotaEspecial1; }
            set { _PorcentajeIvaAlicuotaEspecial1 = value; }
        }

        public decimal MontoGravableAlicuotaEspecial2 {
            get { return _MontoGravableAlicuotaEspecial2; }
            set { _MontoGravableAlicuotaEspecial2 = value; }
        }

        public decimal MontoIVAAlicuotaEspecial2 {
            get { return _MontoIVAAlicuotaEspecial2; }
            set { _MontoIVAAlicuotaEspecial2 = value; }
        }

        public decimal PorcentajeIvaAlicuotaEspecial2 {
            get { return _PorcentajeIvaAlicuotaEspecial2; }
            set { _PorcentajeIvaAlicuotaEspecial2 = value; }
        }
        #endregion

        #endregion //Propiedades
        #region Constructores

        public DetalleDeRendicion() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ConsecutivoRendicion = 0;
            ConsecutivoRenglon = 0;
            NumeroDocumento = string.Empty;
            NumeroControl = string.Empty;
            Fecha = LibDate.Today();
            CodigoProveedor = string.Empty;
            NombreProveedor = string.Empty;
            MontoExento = 0;
            MontoGravable = 0;
            MontoIVA = 0;
            MontoRetencion = 0;
            AplicaParaLibroDeComprasAsBool = false;
            ObservacionesCxP = string.Empty;
            ValidoAsBool = true;
			GeneradaPorAsEnum = eGeneradoPor.Rendicion;
            AplicaIvaAlicuotaEspecialAsBool = false;
            MontoGravableAlicuotaEspecial1 = 0;
            MontoIVAAlicuotaEspecial1 = 0;
            PorcentajeIvaAlicuotaEspecial1 = 0;
            MontoGravableAlicuotaEspecial2 = 0;
            MontoIVAAlicuotaEspecial2 = 0;
            PorcentajeIvaAlicuotaEspecial2 = 0;
        }

        public DetalleDeRendicion Clone() {
            DetalleDeRendicion vResult = new DetalleDeRendicion();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoRendicion = _ConsecutivoRendicion;
            vResult.ConsecutivoRenglon = _ConsecutivoRenglon;
            vResult.NumeroDocumento = _NumeroDocumento;
            vResult.NumeroControl = _NumeroControl;
            vResult.Fecha = _Fecha;
            vResult.CodigoProveedor = _CodigoProveedor;
            vResult.NombreProveedor = _NombreProveedor;
            vResult.MontoExento = _MontoExento;
            vResult.MontoGravable = _MontoGravable;
            vResult.MontoIVA = _MontoIVA;
            vResult.MontoRetencion = _MontoRetencion;
            vResult.AplicaParaLibroDeComprasAsBool = _AplicaParaLibroDeCompras;
            vResult.ObservacionesCxP = _ObservacionesCxP;
            vResult.ValidoAsBool = _Valido;
            vResult.GeneradaPorAsEnum = _GeneradaPor;
            vResult.AplicaIvaAlicuotaEspecialAsBool = _AplicaIvaAlicuotaEspecial;
            vResult.MontoGravableAlicuotaEspecial1 = _MontoGravableAlicuotaEspecial1;
            vResult.MontoIVAAlicuotaEspecial1 = _MontoIVAAlicuotaEspecial1;
            vResult.PorcentajeIvaAlicuotaEspecial1 = _PorcentajeIvaAlicuotaEspecial1;
            vResult.MontoGravableAlicuotaEspecial2 = _MontoGravableAlicuotaEspecial2;
            vResult.MontoIVAAlicuotaEspecial2 = _MontoIVAAlicuotaEspecial2;
            vResult.PorcentajeIvaAlicuotaEspecial2 = _PorcentajeIvaAlicuotaEspecial2;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Rendicion = " + _ConsecutivoRendicion.ToString() +
               "\nConsecutivo Renglon = " + _ConsecutivoRenglon.ToString() +
               "\nNúmero del Documento = " + _NumeroDocumento +
               "\nNúmero de Control = " + _NumeroControl +
               "\nFecha = " + _Fecha.ToShortDateString() +
               "\nCódigo del Proveedor = " + _CodigoProveedor +
               "\nMonto Exento = " + _MontoExento.ToString() +
               "\nMonto Gravable = " + _MontoGravable.ToString() +
               "\nMonto IVA = " + _MontoIVA.ToString() +
               "\nAplica para Libro de Compras = " + _AplicaParaLibroDeCompras +
               "\nObservaciones = " + _ObservacionesCxP;
        }

        #region Miembros de IEquatable<DetalleDeRendicion>
        bool IEquatable<DetalleDeRendicion>.Equals(DetalleDeRendicion other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<DetalleDeRendicion>

        #region Miembros de ICloneable<DetalleDeRendicion>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<DetalleDeRendicion>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class DetalleDeRendicion

} //End of namespace Galac.Saw.Ccl.Rendicion

