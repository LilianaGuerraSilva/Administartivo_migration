using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Ccl.CajaChica {
    [Serializable]
    public class AlicuotaIVA {
        #region Variables
        private DateTime _FechaDeInicioDeVigencia;
        private decimal _MontoAlicuotaGeneral;
        private decimal _MontoAlicuota2;
        private decimal _MontoAlicuota3;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public DateTime FechaDeInicioDeVigencia {
            get { return _FechaDeInicioDeVigencia; }
            set { _FechaDeInicioDeVigencia = LibConvert.DateToDbValue(value); }
        }

        public decimal MontoAlicuotaGeneral {
            get { return _MontoAlicuotaGeneral; }
            set { _MontoAlicuotaGeneral = value; }
        }

        public decimal MontoAlicuota2 {
            get { return _MontoAlicuota2; }
            set { _MontoAlicuota2 = value; }
        }

        public decimal MontoAlicuota3 {
            get { return _MontoAlicuota3; }
            set { _MontoAlicuota3 = value; }
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

        public AlicuotaIVA() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            FechaDeInicioDeVigencia = LibDate.Today();
            MontoAlicuotaGeneral = 0;
            MontoAlicuota2 = 0;
            MontoAlicuota3 = 0;
            fldTimeStamp = 0;
        }

        public AlicuotaIVA Clone() {
            AlicuotaIVA vResult = new AlicuotaIVA();
            vResult.FechaDeInicioDeVigencia = _FechaDeInicioDeVigencia;
            vResult.MontoAlicuotaGeneral = _MontoAlicuotaGeneral;
            vResult.MontoAlicuota2 = _MontoAlicuota2;
            vResult.MontoAlicuota3 = _MontoAlicuota3;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Fecha De Inicio De Vigencia = " + _FechaDeInicioDeVigencia.ToShortDateString() +
               "\nMonto Alicuota General = " + _MontoAlicuotaGeneral.ToString() +
               "\nMonto Alicuota 2 = " + _MontoAlicuota2.ToString() +
               "\nMonto Alicuota 3 = " + _MontoAlicuota3.ToString();
        }
        #endregion //Metodos Generados


    } //End of class AlicuotaIVA

} //End of namespace Galac.Dbo.Ccl.CajaChica

