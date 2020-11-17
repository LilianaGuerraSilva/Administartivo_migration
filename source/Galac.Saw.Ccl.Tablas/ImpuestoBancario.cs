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
            fldTimeStamp = 0;
        }

        public ImpuestoBancario Clone() {
            ImpuestoBancario vResult = new ImpuestoBancario();
            vResult.FechaDeInicioDeVigencia = _FechaDeInicioDeVigencia;
            vResult.AlicuotaAlDebito = _AlicuotaAlDebito;
            vResult.AlicuotaAlCredito = _AlicuotaAlCredito;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Fecha De Inicio De Vigencia = " + _FechaDeInicioDeVigencia.ToShortDateString() +
               "\nAlicuota Al Débito = " + _AlicuotaAlDebito.ToString() +
               "\nAlicuota Al Crédito = " + _AlicuotaAlCredito.ToString();
        }
        #endregion //Metodos Generados


    } //End of class ImpuestoBancario

} //End of namespace Galac.Saw.Ccl.Tablas

