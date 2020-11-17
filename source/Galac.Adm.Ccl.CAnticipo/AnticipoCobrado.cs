using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.CAnticipo;

namespace Galac.Adm.Ccl.CAnticipo {
    [Serializable]
    public class AnticipoCobrado {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroCobranza;
        private int _Secuencial;
        private int _ConsecutivoAnticipoUsado;
        private string _NumeroAnticipo;
        private decimal _MontoOriginal;
        private decimal _MontoRestanteAlDia;
        private string _SimboloMoneda;
        private string _CodigoMoneda;
        private decimal _MontoTotalDelAnticipo;
        private decimal _MontoAplicado;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string NumeroCobranza {
            get { return _NumeroCobranza; }
            set { _NumeroCobranza = LibString.Mid(value, 0, 10); }
        }

        public int Secuencial {
            get { return _Secuencial; }
            set { _Secuencial = value; }
        }

        public int ConsecutivoAnticipoUsado {
            get { return _ConsecutivoAnticipoUsado; }
            set { _ConsecutivoAnticipoUsado = value; }
        }

        public string NumeroAnticipo {
            get { return _NumeroAnticipo; }
            set { _NumeroAnticipo = LibString.Mid(value, 0, 20); }
        }

        public decimal MontoOriginal {
            get { return _MontoOriginal; }
            set { _MontoOriginal = value; }
        }

        public decimal MontoRestanteAlDia {
            get { return _MontoRestanteAlDia; }
            set { _MontoRestanteAlDia = value; }
        }

        public string SimboloMoneda {
            get { return _SimboloMoneda; }
            set { _SimboloMoneda = LibString.Mid(value, 0, 4); }
        }

        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = LibString.Mid(value, 0, 4); }
        }

        public decimal MontoTotalDelAnticipo {
            get { return _MontoTotalDelAnticipo; }
            set { _MontoTotalDelAnticipo = value; }
        }

        public decimal MontoAplicado {
            get { return _MontoAplicado; }
            set { _MontoAplicado = value; }
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

        public AnticipoCobrado() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            NumeroCobranza = string.Empty;
            Secuencial = 0;
            ConsecutivoAnticipoUsado = 0;
            NumeroAnticipo = string.Empty;
            MontoOriginal = 0;
            MontoRestanteAlDia = 0;
            SimboloMoneda = string.Empty;
            CodigoMoneda = string.Empty;
            MontoTotalDelAnticipo = 0;
            MontoAplicado = 0;
            fldTimeStamp = 0;
        }

        public AnticipoCobrado Clone() {
            AnticipoCobrado vResult = new AnticipoCobrado();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.NumeroCobranza = _NumeroCobranza;
            vResult.Secuencial = _Secuencial;
            vResult.ConsecutivoAnticipoUsado = _ConsecutivoAnticipoUsado;
            vResult.NumeroAnticipo = _NumeroAnticipo;
            vResult.MontoOriginal = _MontoOriginal;
            vResult.MontoRestanteAlDia = _MontoRestanteAlDia;
            vResult.SimboloMoneda = _SimboloMoneda;
            vResult.CodigoMoneda = _CodigoMoneda;
            vResult.MontoTotalDelAnticipo = _MontoTotalDelAnticipo;
            vResult.MontoAplicado = _MontoAplicado;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNumero Cobranza = " + _NumeroCobranza +
               "\nSecuencial = " + _Secuencial.ToString() +
               "\nConsecutivo Anticipo Usado = " + _ConsecutivoAnticipoUsado.ToString() +
               "\nNumero Anticipo = " + _NumeroAnticipo +
               "\nMonto Original = " + _MontoOriginal.ToString() +
               "\nMonto Restante Al Dia = " + _MontoRestanteAlDia.ToString() +
               "\nSimbolo Moneda = " + _SimboloMoneda +
               "\nCodigo Moneda = " + _CodigoMoneda +
               "\nMonto Total Del Anticipo = " + _MontoTotalDelAnticipo.ToString() +
               "\nMonto Aplicado = " + _MontoAplicado.ToString();
        }
        #endregion //Metodos Generados


    } //End of class AnticipoCobrado

} //End of namespace Galac.Adm.Ccl.CAnticipo

