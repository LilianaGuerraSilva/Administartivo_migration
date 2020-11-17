using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Dbo.Ccl.CajaChica {
    [Serializable]
    public class OtrosImpuestosCxP: IEquatable<OtrosImpuestosCxP>, INotifyPropertyChanged {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoCxP;
        private int _ConsecutivoRenglonOI;
        private int _ConsecutivoOI;
        private string _Siglas;
        private decimal _Monto;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int ConsecutivoCxP {
            get { return _ConsecutivoCxP; }
            set { _ConsecutivoCxP = value; }
        }

        public int ConsecutivoRenglonOI {
            get { return _ConsecutivoRenglonOI; }
            set { _ConsecutivoRenglonOI = value; }
        }

        public int ConsecutivoOI {
            get { return _ConsecutivoOI; }
            set { _ConsecutivoOI = value; }
        }

        public string Siglas {
            get { return _Siglas; }
            set { _Siglas = LibString.Mid(value, 0, 12); }
        }

        public decimal Monto {
            get { return _Monto; }
            set { _Monto = value; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            ConsecutivoCxP = 0;
            ConsecutivoRenglonOI = 0;
            ConsecutivoOI = 0;
            Siglas = "";
            Monto = 0;
        }

        public OtrosImpuestosCxP Clone() {
            OtrosImpuestosCxP vResult = new OtrosImpuestosCxP();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoCxP = _ConsecutivoCxP;
            vResult.ConsecutivoRenglonOI = _ConsecutivoRenglonOI;
            vResult.ConsecutivoOI = _ConsecutivoOI;
            vResult.Siglas = _Siglas;
            vResult.Monto = _Monto;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Cx P = " + _ConsecutivoCxP.ToString() +
               "\nConsecutivo Renglon OI = " + _ConsecutivoRenglonOI.ToString() +
               "\nConsecutivo OI = " + _ConsecutivoOI.ToString() +
               "\nSiglas = " + _Siglas +
               "\nMonto = " + _Monto.ToString();
        }

        #region Miembros de IEquatable<OtrosImpuestosCxP>
        bool IEquatable<OtrosImpuestosCxP>.Equals(OtrosImpuestosCxP other) {
            if (this._ConsecutivoCompania == other.ConsecutivoCompania
                && this._ConsecutivoCxP == other.ConsecutivoCxP
                && this._ConsecutivoRenglonOI == other.ConsecutivoRenglonOI) {
                return true;
            } else {
                return false;
            }
        }
        #endregion //IEquatable<OtrosImpuestosCxP>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class OtrosImpuestosCxP

} //End of namespace Galac.Dbo.Ccl.CajaChica

