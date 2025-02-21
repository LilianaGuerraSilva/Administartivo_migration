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
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;
using System.Xml.Linq;

namespace Galac.Saw.Brl.SttDef {
    public class clsImprentaDigitalSettings {
        #region Variables
        private string _DireccionURL;
        private string _CampoUsuario;
        private string _Usuario;
        private string _CampoClave;
        private string _Clave;
        private int _ConsecutivoCompania;
        #endregion //Variables
        #region Propiedades
        public string DireccionURL {
            get { return _DireccionURL; }
            set { _DireccionURL = LibString.Mid(value, 0, 500); }
        }

        public string CampoUsuario {
            get { return _CampoUsuario; }
            set { _CampoUsuario = LibString.Mid(value, 0, 50); }
        }

        public string Usuario {
            get { return _Usuario; }
            set { _Usuario = LibString.Mid(value, 0, 100); }
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
            _ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            CargarDatosDeConexionImprentaDigital();
        }
        #endregion //Constructor
        #region Metodos Generados
        public void Clear() {
            DireccionURL = string.Empty;
            CampoUsuario = string.Empty;
            CampoClave = string.Empty;
            Usuario = string.Empty;
            Clave = string.Empty;
        }

        private void CargarDatosDeConexionImprentaDigital() {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParm = new LibGpParams();
            vParm.AddInInteger("ConsecutivoCompania", _ConsecutivoCompania);
            vSql.AppendLine("SELECT ImprentaDigitalUrl, ImprentaDigitalNombreCampoUsuario, ImprentaDigitalNombreCampoClave, ImprentaDigitalUsuario, ImprentaDigitalClave ");
            vSql.AppendLine("FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania");
            XElement xElementResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParm.Get(), "", 0);
            if (xElementResult != null && xElementResult.HasElements) {
                DireccionURL = LibXml.GetPropertyString(xElementResult, "ImprentaDigitalUrl");
                CampoUsuario = LibXml.GetPropertyString(xElementResult, "ImprentaDigitalNombreCampoUsuario");
                CampoClave = LibXml.GetPropertyString(xElementResult, "ImprentaDigitalNombreCampoClave");
                Usuario = LibXml.GetPropertyString(xElementResult, "ImprentaDigitalUsuario");                
                Clave = LibXml.GetPropertyString(xElementResult, "ImprentaDigitalClave");
            }
        }

        public void ActualizarValores() {
            QAdvSql _insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParm = new LibGpParams();
            vParm.AddInInteger("ConsecutivoCompania", _ConsecutivoCompania);
            vSql.AppendLine("UPDATE Compania SET ImprentaDigitalUrl = " + _insSql.ToSqlValue(DireccionURL));
            vSql.AppendLine(", ImprentaDigitalNombreCampoUsuario = " + _insSql.ToSqlValue(CampoUsuario));
            vSql.AppendLine(", ImprentaDigitalNombreCampoClave = " + _insSql.ToSqlValue(CampoClave));
            vSql.AppendLine(", ImprentaDigitalUsuario = " + _insSql.ToSqlValue(Usuario));
            vSql.AppendLine(", ImprentaDigitalClave= " + _insSql.ToSqlValue(LibCryptography.SymEncryptDES(Clave)));
            vSql.AppendLine(" WHERE ConsecutivoCompania=@ConsecutivoCompania");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParm.Get(), "", 0);
        }
        #endregion //Metodos Generados
    }
}
