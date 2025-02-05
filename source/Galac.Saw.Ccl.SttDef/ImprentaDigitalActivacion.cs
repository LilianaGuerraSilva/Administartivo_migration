using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Ccl.SttDef {
    [Serializable]
    public class ImprentaDigitalActivacion {
        #region Variables
        private eProveedorImprentaDigital _Proveedor;
        private string _Url;
        private string _Usuario;
        private string _Clave;
        private string _FacturaT1;
        private string _FacturaT2;
        private string _NotaDeCredito;
        private string _NotaDeDebito;
        private string _NotaDeEntrega;
        private DateTime _FechaDeInicioDeUso;
        #endregion //Variables
        #region Propiedades
        public eProveedorImprentaDigital ProveedorAsEnum {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }

        public string Proveedor {
            set { _Proveedor = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(value); }
        }

        public string ProveedorAsDB {
            get { return LibConvert.EnumToDbValue((int) _Proveedor); }
        }

        public string ProveedorAsString {
            get { return LibEnumHelper.GetDescription(_Proveedor); }
        }

        public string Url {
            get { return _Url; }
            set { _Url = LibString.Mid(value, 0, 255); }
        }

        public string Usuario {
            get { return _Usuario; }
            set { _Usuario = LibString.Mid(value, 0, 100); }
        }

        public string Clave {
            get { return _Clave; }
            set { _Clave = LibString.Mid(value, 0, 1000); }
        }

        public string FacturaT1 {
            get { return _FacturaT1; }
            set { _FacturaT1 = LibString.Mid(value, 0, 11); }
        }

        public string FacturaT2 {
            get { return _FacturaT2; }
            set { _FacturaT2 = LibString.Mid(value, 0, 11); }
        }

        public string NotaDeCredito {
            get { return _NotaDeCredito; }
            set { _NotaDeCredito = LibString.Mid(value, 0, 11); }
        }

        public string NotaDeDebito {
            get { return _NotaDeDebito; }
            set { _NotaDeDebito = LibString.Mid(value, 0, 11); }
        }

        public string NotaDeEntrega {
            get { return _NotaDeEntrega; }
            set { _NotaDeEntrega = LibString.Mid(value, 0, 11); }
        }

        public DateTime FechaDeInicioDeUso {
            get { return _FechaDeInicioDeUso; }
            set { _FechaDeInicioDeUso = LibConvert.DateToDbValue(value); }
        }
        #endregion //Propiedades
        #region Constructores
        public ImprentaDigitalActivacion() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ProveedorAsEnum = eProveedorImprentaDigital.NoAplica;
            Url = string.Empty;
            Usuario = string.Empty;
            Clave = string.Empty;
            FacturaT1 = string.Empty;
            FacturaT2 = string.Empty;
            NotaDeCredito = string.Empty;
            NotaDeDebito = string.Empty;
            NotaDeEntrega = string.Empty;
            FechaDeInicioDeUso = LibDate.Today();
        }

        public ImprentaDigitalActivacion Clone() {
            ImprentaDigitalActivacion vResult = new ImprentaDigitalActivacion();
            vResult.ProveedorAsEnum = _Proveedor;
            vResult.Url = _Url;
            vResult.Usuario = _Usuario;
            vResult.Clave = _Clave;
            vResult.FacturaT1 = _FacturaT1;
            vResult.FacturaT2 = _FacturaT2;
            vResult.NotaDeCredito = _NotaDeCredito;
            vResult.NotaDeDebito = _NotaDeDebito;
            vResult.NotaDeEntrega = _NotaDeEntrega;
            vResult.FechaDeInicioDeUso = _FechaDeInicioDeUso;
            return vResult;
        }

        public override string ToString() {
           return "Proveedor = " + _Proveedor.ToString() +
               "\nUrl = " + _Url +
               "\nUsuario = " + _Usuario +
               "\nClave = " + _Clave +
               "\nFactura T1 = " + _FacturaT1 +
               "\nFactura T2 = " + _FacturaT2 +
               "\nNota De Credito = " + _NotaDeCredito +
               "\nNota De Debito = " + _NotaDeDebito +
               "\nNota De Entrega = " + _NotaDeEntrega +
               "\nFecha De Inicio De Uso = " + _FechaDeInicioDeUso.ToShortDateString();
        }
        #endregion //Metodos Generados
    } //End of class ImprentaDigitalActivacion

} //End of namespace Galac..Ccl.SttDef

