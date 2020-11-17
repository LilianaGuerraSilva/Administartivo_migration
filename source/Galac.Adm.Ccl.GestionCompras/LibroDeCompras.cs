using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Ccl.GestionCompras {
    [Serializable]
    public class LibroDeCompras {
        #region Variables
        private int _ConsecutivoCompania;
        private string _Numero;
        private string _Mes;
        private string _Ano;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string Numero {
            get { return _Numero; }
            set { _Numero = LibString.Mid(value, 0, 11); }
        }

        public string Mes {
            get { return _Mes; }
            set { _Mes = LibString.Mid(value, 0, 2); }
        }

        public string Ano {
            get { return _Ano; }
            set { _Ano = LibString.Mid(value, 0, 4); }
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

        public LibroDeCompras() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Numero = string.Empty;
            Mes = string.Empty;
            Ano = string.Empty;
            fldTimeStamp = 0;
        }

        public LibroDeCompras Clone() {
            LibroDeCompras vResult = new LibroDeCompras();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Numero = _Numero;
            vResult.Mes = _Mes;
            vResult.Ano = _Ano;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
                "\nN° de Factura = " + _Numero +
                "\nMes = " + _Mes +
                "\nAño = " + _Ano;
        }
        #endregion //Metodos Generados


    } //End of class LibroDeCompras

} //End of namespace Galac.Adm.Ccl.GestionCompras

