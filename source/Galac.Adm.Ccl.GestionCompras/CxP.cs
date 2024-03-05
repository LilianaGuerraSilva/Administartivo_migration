using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Lib;


namespace Galac.Adm.Ccl.GestionCompras {
    [Serializable]
    public class CxP {
       
        #region Variables

        private int _ConsecutivoCompania;
        private int _ConsecutivoCxP;
        private string _Numero;
        private eStatusDocumentoCxP _Status;
        private string _CodigoProveedor;
        private string _NombreProveedor;
        private DateTime _Fecha;
        private decimal _MontoAbonado;
        private string _CodigoMoneda;

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

        public string Numero {
            get { return _Numero; }
            set { _Numero = LibString.Mid(value, 0, 11); }
        }

        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = value; }
        }

        public eStatusDocumentoCxP StatusAsEnum {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Status {
            set { _Status = (eStatusDocumentoCxP)LibConvert.DbValueToEnum(value); }
        }

        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int)_Status); }
        }

        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
        }

        public string CodigoProveedor {
            get { return _CodigoProveedor; }
            set { _CodigoProveedor = LibString.Mid(value, 0, 10); }
        }

        public string NombreProveedor {
            get { return _NombreProveedor; }
            set { _NombreProveedor = LibString.Mid(value, 0, 60); }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value); }
        }

        public decimal MontoAbonado {
            get { return _MontoAbonado; }
            set { _MontoAbonado = value; }
        }

        #endregion //Propiedades

        #region Constructores

        public CxP() {
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
            StatusAsEnum = eStatusDocumentoCxP.PorCancelar;
            CodigoProveedor = string.Empty;
            NombreProveedor = string.Empty;
            Fecha = LibDate.Today();
            MontoAbonado = 0;
            ConsecutivoCxP = 0;
            CodigoMoneda = string.Empty;
        }

        public CxP Clone() {
            CxP vResult = new CxP();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Numero = _Numero;
            vResult.StatusAsEnum = _Status;
            vResult.CodigoProveedor = _CodigoProveedor;
            vResult.NombreProveedor = _NombreProveedor;
            vResult.Fecha = _Fecha;
            vResult.MontoAbonado = _MontoAbonado;
            vResult.ConsecutivoCxP = _ConsecutivoCxP;
            vResult.CodigoMoneda = _CodigoMoneda;
            return vResult;
        }

        public override string ToString() {
            return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
                "\nNúmero = " + _Numero +
                "\nStatus = " + _Status.ToString() +
                "\nCódigo del Proveedor = " + _CodigoProveedor +
                "\nFecha = " + _Fecha.ToShortDateString() +
                "\nMonto Abonado = " + _MontoAbonado.ToString() +
                "\nConsecutivo Cx P = " + _ConsecutivoCxP.ToString() +
                "\nCodigo Moneda = " + _CodigoMoneda.ToString();
        }

        #endregion //Metodos Generados


    } //End of class CxP

} //End of namespace Galac..Ccl.ComponenteNoEspecificado

