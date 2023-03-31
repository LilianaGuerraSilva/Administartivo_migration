using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Cnf;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Lib;

namespace Galac.Saw.Brl.SttDef {
    public class clsImprentaDigitalSettings {
        #region Variables
        private string _DireccionURL;
        private string _CampoUsuario;
        private string _Usuario;
        private string _CampoClave;
        private string _Clave;
        #endregion //Variables
        #region Propiedades
        public string DireccionURL {
            get { return _DireccionURL; }
            set { _DireccionURL = LibString.Mid(value, 0, 100); }
        }

        public string CampoUsuario {
            get { return _CampoUsuario; }
            set { _CampoUsuario = LibString.Mid(value, 0, 50); }
        }

        public string Usuario {
            get { return _Usuario; }
            set { _Usuario = LibString.Mid(value, 0, 50); }
        }

        public string CampoClave {
            get { return _CampoClave; }
            set { _CampoClave = LibString.Mid(value, 0, 100); }
        }

        public string Clave {
            get { return LibCryptography.SymDecryptDES(_Clave); }
            set { _Clave = value; }
        }
        #endregion //Propiedades
        #region Constructor
        public clsImprentaDigitalSettings() {
            Clear();
            DireccionURL = LibAppSettings.ReadAppSettingsKey("DIRECCIONURL");
            CampoUsuario = LibAppSettings.ReadAppSettingsKey("CAMPOUSUARIO");
            Usuario = LibAppSettings.ReadAppSettingsKey("USUARIO");
            CampoClave = LibAppSettings.ReadAppSettingsKey("CAMPOCLAVE");
            Clave = ObtenerClaveEncriptada();
        }
        #endregion //Constructor
        #region Metodos Generados
        public void Clear() {
            DireccionURL = string.Empty;
            CampoUsuario = string.Empty;
            Usuario = string.Empty;
            CampoClave = string.Empty;
            Clave = string.Empty;
        }
        public string ObtenerClaveEncriptada() {
            if (LaClaveEstaEncriptada()) {
                return LibAppSettings.ReadAppSettingsKey("CLAVE-E");
            } else {
                return EncriptarClave();
            }
        }
        private static bool LaClaveEstaEncriptada() {
            return LibString.S1IsEqualToS2(LibAppSettings.ReadAppSettingsKey("CLAVE"), "") && !LibString.S1IsEqualToS2(LibAppSettings.ReadAppSettingsKey("CLAVE-E"), "");
        }
        private string EncriptarClave() {
            string vClaveEncriptada = LibCryptography.SymEncryptDES(LibAppSettings.ReadAppSettingsKey("CLAVE"));
            LibAppConfig vAppConfig = new LibAppConfig();
            ConfigHelper.AddKeyToAppSettings("CLAVE", string.Empty);
            vAppConfig.AddKeyToAppSettings("CLAVE-E", vClaveEncriptada, true);
            return vClaveEncriptada;
        }
        #endregion //Metodos Generados
    }
}
