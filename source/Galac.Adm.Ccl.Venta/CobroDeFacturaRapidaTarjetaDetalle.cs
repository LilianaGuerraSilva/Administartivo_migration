using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class CobroDeFacturaRapidaTarjetaDetalle: IEquatable<CobroDeFacturaRapidaTarjetaDetalle>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private string _CodigoFormaDelCobro;
        private string _NumeroDelDocumento;
        private int _CodigoBanco;
        private string _NombreBanco;
        private decimal _Monto;
        private int _CodigoPuntoDeVenta;
        private string _NombreBancoPuntoDeVenta;
        private string _NumeroDocumentoAprobacion;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string CodigoFormaDelCobro {
            get { return _CodigoFormaDelCobro; }
            set { _CodigoFormaDelCobro = LibString.Mid(value, 0, 5); }
        }

        public string NumeroDelDocumento {
            get { return _NumeroDelDocumento; }
            set { _NumeroDelDocumento = LibString.Mid(value, 0, 30); }
        }

        public int CodigoBanco {
            get { return _CodigoBanco; }
            set { _CodigoBanco = value; }
        }

        public string NombreBanco {
            get { return _NombreBanco; }
            set { 
                _NombreBanco = LibString.Mid(value, 0, 100);
                OnPropertyChanged("NombreBanco");
            }
        }

        public decimal Monto {
            get { return _Monto; }
            set { _Monto = value; }
        }

        public int CodigoPuntoDeVenta {
            get { return _CodigoPuntoDeVenta; }
            set { _CodigoPuntoDeVenta = value; }
        }

        public string NombreBancoPuntoDeVenta {
            get { return _NombreBancoPuntoDeVenta; }
            set { 
                _NombreBancoPuntoDeVenta = LibString.Mid(value, 0, 100);
                OnPropertyChanged("NombreBancoPuntoDeVenta");
            }
        }

        public string NumeroDocumentoAprobacion {
            get { return _NumeroDocumentoAprobacion; }
            set { _NumeroDocumentoAprobacion = LibString.Mid(value, 0, 30); }
        }
        #endregion //Propiedades
        #region Constructores

        public CobroDeFacturaRapidaTarjetaDetalle() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            CodigoFormaDelCobro = string.Empty;
            NumeroDelDocumento = string.Empty;
            CodigoBanco = 0;
            NombreBanco = string.Empty;
            Monto = 0;
            CodigoPuntoDeVenta = 0;
            NombreBancoPuntoDeVenta = string.Empty;
            NumeroDocumentoAprobacion = string.Empty;
        }

        public CobroDeFacturaRapidaTarjetaDetalle Clone() {
            CobroDeFacturaRapidaTarjetaDetalle vResult = new CobroDeFacturaRapidaTarjetaDetalle();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.CodigoFormaDelCobro = _CodigoFormaDelCobro;
            vResult.NumeroDelDocumento = _NumeroDelDocumento;
            vResult.CodigoBanco = _CodigoBanco;
            vResult.NombreBanco = _NombreBanco;
            vResult.Monto = _Monto;
            vResult.CodigoPuntoDeVenta = _CodigoPuntoDeVenta;
            vResult.NombreBancoPuntoDeVenta = _NombreBancoPuntoDeVenta;
            vResult.NumeroDocumentoAprobacion = _NumeroDocumentoAprobacion;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCodigo Forma Del Cobro = " + _CodigoFormaDelCobro +
               "\nNumero Del Documento = " + _NumeroDelDocumento +
               "\nCódigo del Banco = " + _CodigoBanco.ToString() +
               "\nMonto = " + _Monto.ToString() +
               "\nCódigo del Banco del Punto de Venta = " + _CodigoPuntoDeVenta.ToString() +
               "\nNumero Documento Aprobacion = " + _NumeroDocumentoAprobacion;
        }

        #region Miembros de IEquatable<CobroDeFacturaRapidaTarjetaDetalle>
        bool IEquatable<CobroDeFacturaRapidaTarjetaDetalle>.Equals(CobroDeFacturaRapidaTarjetaDetalle other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<CobroDeFacturaRapidaTarjetaDetalle>

        #region Miembros de ICloneable<CobroDeFacturaRapidaTarjetaDetalle>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<CobroDeFacturaRapidaTarjetaDetalle>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class CobroDeFacturaRapidaTarjetaDetalle

} //End of namespace Galac.Adm.Ccl.Venta

