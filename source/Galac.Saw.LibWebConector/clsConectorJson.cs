using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.IO;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.LibWebConnector {
    public abstract class clsConectorJson {
        string _Token;
        public string Token {
            get {
                return _Token;
            }
            internal set {
                _Token = value;
            }
        }

        internal ILoginUser LoginUser {
            get; set;
        }

        public clsConectorJson(ILoginUser valloginUser) {
            LoginUser = valloginUser;
            _Token = string.Empty;
        }

        public static string SerializeJSON(object valElemento) {
            try {
                string vResult = JsonConvert.SerializeObject(valElemento, Formatting.Indented);
                return vResult;
            } catch (JsonException) {
                throw;
            } catch (Exception) {
                throw;
            }
        }

        public static string LimpiaRegistrosTempralesEnJSON(string valDocJSon) {
            string vResult = "";
            JObject vResponse = JObject.Parse(valDocJSon);
            RemoveItemArray(vResponse.SelectToken("documentoElectronico.encabezado.totales.formasPago"));
            RemoveItemArray(vResponse.SelectToken("documentoElectronico.encabezado.comprador.correo"));
            RemoveItemArray(vResponse.SelectToken("documentoElectronico.encabezado.comprador.telefono"));
            RemoveItemArray(vResponse.SelectToken("documentoElectronico.detallesItems"));
            RemoveItemArray(vResponse.SelectToken("documentoElectronico.InfoAdicional"));
            vResult = vResponse.ToString(Formatting.Indented);
            return vResult;
        }

        private static void RemoveItemArray(JToken valProperty) {
            if (valProperty != null) {
                valProperty.First().Remove();
            }
        }

        public string FormatingJSON(ILoginUser valloginUser) {
            string vResult = "";
            stUserLoginCnn vUsrLgn = new stUserLoginCnn();
            vUsrLgn.usuario = valloginUser.User;
            vUsrLgn.clave = valloginUser.Password;
            vResult = vResult.Replace(nameof(vUsrLgn.usuario), valloginUser.UserKey);
            vResult = vResult.Replace(nameof(vUsrLgn.clave), valloginUser.PasswordKey);
            vResult = SerializeJSON(vUsrLgn);
            return vResult;
        }

        public void GeneraLogDeErrores(string valMensajeResultado, string valJSon) {
            try {
                string vPath = Path.Combine(LibDirectory.GetProgramFilesGalacDir(), Path.Combine(LibDefGen.ProgramInfo.ProgramInitials, "ImprentaDigital"));
                if (!LibDirectory.DirExists(vPath)) {
                    LibDirectory.CreateDir(vPath);
                }
                vPath = vPath + @"\ImprentaDigitalResult.txt";
                LibFile.WriteLineInFile(vPath, valMensajeResultado + "\r\n" + valJSon, false);
            } catch (Exception) {
                throw;
            }
        }

        public abstract bool CheckConnection(ref string refMensaje, string valComandoApi);
        public abstract stPostResq SendPostJson(string valJsonStr, string valComandoApi, string valToken, string valNumeroDocumento = "", eTipoDocumentoFactura valTipoDocumento = eTipoDocumentoFactura.NoAsignado);
    }
}