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
    public class CobroDeFacturaRapidaDetalle: IEquatable<CobroDeFacturaRapidaDetalle>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private string _CodigoFormaDelCobro;
        private decimal _MontoEfectivo;
        private decimal _MontoCheque;
        private decimal _MontoTarjeta;
        private decimal _MontoDeposito;
        private decimal _MontoAnticipo;
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

        public decimal MontoEfectivo {
            get { return _MontoEfectivo; }
            set { _MontoEfectivo = value; }
        }

        public decimal MontoCheque {
            get { return _MontoCheque; }
            set { _MontoCheque = value; }
        }

        public decimal MontoTarjeta {
            get { return _MontoTarjeta; }
            set { _MontoTarjeta = value; }
        }

        public decimal MontoDeposito {
            get { return _MontoDeposito; }
            set { _MontoDeposito = value; }
        }

        public decimal MontoAnticipo {
            get { return _MontoAnticipo; }
            set { _MontoAnticipo = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public CobroDeFacturaRapidaDetalle() {
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
            MontoEfectivo = 0;
            MontoCheque = 0;
            MontoTarjeta = 0;
            MontoDeposito = 0;
            MontoAnticipo = 0;
        }

        public CobroDeFacturaRapidaDetalle Clone() {
            CobroDeFacturaRapidaDetalle vResult = new CobroDeFacturaRapidaDetalle();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.CodigoFormaDelCobro = _CodigoFormaDelCobro;
            vResult.MontoEfectivo = _MontoEfectivo;
            vResult.MontoCheque = _MontoCheque;
            vResult.MontoTarjeta = _MontoTarjeta;
            vResult.MontoDeposito = _MontoDeposito;
            vResult.MontoAnticipo = _MontoAnticipo;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCodigo Forma Del Cobro = " + _CodigoFormaDelCobro +
               "\nMonto Efectivo = " + _MontoEfectivo.ToString() +
               "\nMonto Cheque = " + _MontoCheque.ToString() +
               "\nMonto Tarjeta = " + _MontoTarjeta.ToString() +
               "\nMonto Deposito = " + _MontoDeposito.ToString() +
               "\nMonto Anticipo = " + _MontoAnticipo.ToString();
        }

        #region Miembros de IEquatable<CobroDeFacturaRapidaDetalle>
        bool IEquatable<CobroDeFacturaRapidaDetalle>.Equals(CobroDeFacturaRapidaDetalle other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<CobroDeFacturaRapidaDetalle>

        #region Miembros de ICloneable<CobroDeFacturaRapidaDetalle>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<CobroDeFacturaRapidaDetalle>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class CobroDeFacturaRapidaDetalle

} //End of namespace Galac.Adm.Ccl.Venta

