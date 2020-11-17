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
    public class CobroDeFacturaRapidaAnticipoDetalle: IEquatable<CobroDeFacturaRapidaAnticipoDetalle>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private string _CodigoFormaDelCobro;
        private int _CodigoAnticipo;
        private string _NumeroAnticipo;
        private string _MontoDisponible;
        private decimal _Monto;
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

        public int CodigoAnticipo {
            get { return _CodigoAnticipo; }
            set { _CodigoAnticipo = value; }
        }

        public string NumeroAnticipo {
            get { return _NumeroAnticipo; }
            set { _NumeroAnticipo = LibString.Mid(value, 0, 20); }
        }

        public string MontoDisponible {
            get { return _MontoDisponible; }
            set { 
                _MontoDisponible = LibString.Mid(value, 0, 20);
                OnPropertyChanged("MontoDisponible");
            }
        }

        public decimal Monto {
            get { return _Monto; }
            set { _Monto = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public CobroDeFacturaRapidaAnticipoDetalle() {
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
            CodigoAnticipo = 0;
            NumeroAnticipo = string.Empty;
            MontoDisponible = string.Empty;
            Monto = 0;
        }

        public CobroDeFacturaRapidaAnticipoDetalle Clone() {
            CobroDeFacturaRapidaAnticipoDetalle vResult = new CobroDeFacturaRapidaAnticipoDetalle();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.CodigoFormaDelCobro = _CodigoFormaDelCobro;
            vResult.CodigoAnticipo = _CodigoAnticipo;
            vResult.NumeroAnticipo = _NumeroAnticipo;
            vResult.MontoDisponible = _MontoDisponible;
            vResult.Monto = _Monto;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCodigo Forma Del Cobro = " + _CodigoFormaDelCobro +
               "\nCódigo del Anticipo = " + _CodigoAnticipo.ToString() +
               "\nNumero Anticipo = " + _NumeroAnticipo +
               "\nMonto = " + _Monto.ToString();
        }

        #region Miembros de IEquatable<CobroDeFacturaRapidaAnticipoDetalle>
        bool IEquatable<CobroDeFacturaRapidaAnticipoDetalle>.Equals(CobroDeFacturaRapidaAnticipoDetalle other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<CobroDeFacturaRapidaAnticipoDetalle>

        #region Miembros de ICloneable<CobroDeFacturaRapidaAnticipoDetalle>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<CobroDeFacturaRapidaAnticipoDetalle>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class CobroDeFacturaRapidaAnticipoDetalle

} //End of namespace Galac.Adm.Ccl.Venta

