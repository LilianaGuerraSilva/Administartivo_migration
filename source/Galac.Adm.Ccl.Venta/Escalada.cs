using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;


namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class Escalada {
        #region Variables
        private string _Id;
        private int _Escalada41;
        private DateTime _Escalada32;
        private string _Escalada73;
        private string _Escalada24;
        private string _Escalada85;
        private string _Escalada100;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string Id {
            get { return _Id; }
            set { _Id = value; }
        }

        public int Escalada41 {
            get { return _Escalada41; }
            set { _Escalada41 = value; }
        }

        public DateTime Escalada32 {
            get { return _Escalada32; }
            set { _Escalada32 = LibConvert.DateToDbValue(value); }
        }

        public string Escalada73 {
            get { return _Escalada73; }
            set { _Escalada73 = LibString.Mid(value, 0, 40); }
        }

        public string Escalada24 {
            get { return _Escalada24; }
            set { _Escalada24 = LibString.Mid(value, 0, 40); }
        }

        public string Escalada85 {
            get { return _Escalada85; }
            set { _Escalada85 = LibString.Mid(value, 0, 30); }
        }

        public string Escalada100 {
            get { return _Escalada100; }
            set { _Escalada100 = LibString.Mid(value, 0, 255); }
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

        public Escalada() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            Id = string.Empty;
            Escalada41 = 0;
            Escalada32 = LibDate.Today();
            Escalada73 = string.Empty;
            Escalada24 = string.Empty;
            Escalada85 = string.Empty;
            Escalada100 = string.Empty;
            fldTimeStamp = 0;
        }

        public Escalada Clone() {
            Escalada vResult = new Escalada();
            vResult.Id = _Id;
            vResult.Escalada41 = _Escalada41;
            vResult.Escalada32 = _Escalada32;
            vResult.Escalada73 = _Escalada73;
            vResult.Escalada24 = _Escalada24;
            vResult.Escalada85 = _Escalada85;
            vResult.Escalada100 = _Escalada100;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Id = " + _Id +
               "\nEscalada 41 = " + _Escalada41.ToString() +
               "\nEscalada 32 = " + _Escalada32.ToShortDateString() +
               "\nEscalada 73 = " + _Escalada73 +
               "\nEscalada 24 = " + _Escalada24 +
               "\nEscalada 85 = " + _Escalada85 +
               "\nEscalada 100 = " + _Escalada100;
        }
        #endregion //Metodos Generados


    } //End of class Escalada

} //End of namespace Galac..Ccl.ComponenteNoEspecificado

