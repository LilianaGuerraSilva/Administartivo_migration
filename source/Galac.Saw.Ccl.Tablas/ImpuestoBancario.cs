using System;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Tablas {    
    [Serializable]
    public class ImpuestoBancario {
        #region Variables
        private DateTime _FechaDeInicioDeVigencia;
        private decimal _AlicuotaAlDebito;
        private decimal _AlicuotaAlCredito;
        private decimal _AlicuotaC1Al4;
        private decimal _AlicuotaC5;
        private decimal _AlicuotaC6;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public DateTime FechaDeInicioDeVigencia {
            get { return _FechaDeInicioDeVigencia; }
            set { _FechaDeInicioDeVigencia = LibConvert.DateToDbValue(value); }
        }

        public decimal AlicuotaAlDebito {
            get { return _AlicuotaAlDebito; }
            set { _AlicuotaAlDebito = value; }
        }

        public decimal AlicuotaAlCredito {
            get { return _AlicuotaAlCredito; }
            set { _AlicuotaAlCredito = value; }
        }

        public decimal AlicuotaC1Al4 {
            get { return _AlicuotaC1Al4; }
            set { _AlicuotaC1Al4 = value; }
        }

        public decimal AlicuotaC5 {
            get { return _AlicuotaC5; }
            set { _AlicuotaC5 = value; }
        }

        public decimal AlicuotaC6 {
            get { return _AlicuotaC6; }
            set { _AlicuotaC6 = value; }
        }
        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public ImpuestoBancario() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            FechaDeInicioDeVigencia = LibDate.Today();
            AlicuotaAlDebito = 0;
            AlicuotaAlCredito = 0;
            AlicuotaC1Al4 = 0;
            AlicuotaC5 = 0;
            AlicuotaC6 = 0;
            fldTimeStamp = 0;
        }

        public ImpuestoBancario Clone() {
            ImpuestoBancario vResult = new ImpuestoBancario();
            vResult.FechaDeInicioDeVigencia = _FechaDeInicioDeVigencia;
            vResult.AlicuotaAlDebito = _AlicuotaAlDebito;
            vResult.AlicuotaAlCredito = _AlicuotaAlCredito;
            vResult.AlicuotaC1Al4 = _AlicuotaC1Al4;
            vResult.AlicuotaC5 = _AlicuotaC5;
            vResult.AlicuotaC6 = _AlicuotaC6;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Fecha De Inicio De Vigencia = " + _FechaDeInicioDeVigencia.ToShortDateString() +
               "\nAlícuota al Débito = " + _AlicuotaAlDebito.ToString() +
               "\nAlícuota al Crédito = " + _AlicuotaAlCredito.ToString() +
               "\nAlícuota al Crédito Cont. 1 al 4 = " + _AlicuotaC1Al4.ToString() +
               "\nAlícuota al Crédito Cont. 5 = " + _AlicuotaC5.ToString() +
               "\nAlícuota al Crédito Cont. 6 = " + _AlicuotaC6.ToString();
        }
        #endregion //Metodos Generados


    } //End of class ImpuestoBancario

} //End of namespace Galac.Saw.Ccl.Tablas

