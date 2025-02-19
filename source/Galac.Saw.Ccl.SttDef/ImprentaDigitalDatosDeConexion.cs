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
    public class ImprentaDigitalDatosDeConexion {
        #region Variables
        private eProveedorImprentaDigital _Proveedor;
        private string _Url;
        private string _Usuario;
        private string _Clave;
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
            get { return LibConvert.EnumToDbValue((int)_Proveedor); }
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
        #endregion //Propiedades
        #region Constructores
        public ImprentaDigitalDatosDeConexion() {
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
        }
        public ImprentaDigitalActivacion Clone() {
            ImprentaDigitalActivacion vResult = new ImprentaDigitalActivacion();
            vResult.ProveedorAsEnum = _Proveedor;
            vResult.Url = _Url;
            vResult.Usuario = _Usuario;
            vResult.Clave = _Clave;
            return vResult;
        }

        public override string ToString() {
            return "Proveedor = " + _Proveedor.ToString() +
                "\nUrl = " + _Url +
                "\nUsuario = " + _Usuario +
                "\nClave = " + _Clave;
        }
        #endregion //Metodos Generados    
    }//End of class ImprentaDigitalDatosDeConexion {
}//End of namespace Galac.Saw.Ccl.SttDef
