using System;
using Newtonsoft.Json;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.IO;
using Galac.Saw.Ccl.SttDef;
using Galac.Adm.Ccl.ImprentaDigital;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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

        internal string PostRequest {
            get; private set;
        }

        public clsConectorJson(ILoginUser valloginUser) {
            LoginUser = valloginUser;
            _Token = string.Empty;
        }

        public static string SerializeJSON(object valElemento) {
            try {
                string vResult = JsonConvert.SerializeObject(valElemento, Formatting.Indented);
                return vResult;
            } catch(JsonException) {
                throw;
            } catch(Exception) {
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
            if(valProperty != null) {
                valProperty.First().Remove();
            }
        }

        public string GetJsonUser(ILoginUser valloginUser, eProveedorImprentaDigital valProveedorImprentaDigital) {
            string vResult = "";
            string vPassword = valloginUser.Password;
            if(valProveedorImprentaDigital == eProveedorImprentaDigital.Unidigital) {
                vPassword = ComputeSha256Hash(vPassword);
            }
            JObject vLoginUser = new JObject {
                        {valloginUser.UserKey, valloginUser.User},
                        {valloginUser.PasswordKey, vPassword}};
            vResult = vLoginUser.ToString();
            return vResult;
        }

        public void GeneraLogDeErrores(string valMensajeResultado, string valJSon) {
            try {
                string vPath = Path.Combine(LibDirectory.GetProgramFilesGalacDir(), Path.Combine(LibDefGen.ProgramInfo.ProgramInitials, "ImprentaDigital"));
                if(!LibDirectory.DirExists(vPath)) {
                    LibDirectory.CreateDir(vPath);
                }
                vPath = vPath + @"\ImprentaDigitalResult.txt";
                LibFile.WriteLineInFile(vPath, valMensajeResultado + "\r\n" + valJSon, false);
            } catch(Exception) {
                throw;
            }
        }

        private string ComputeSha256Hash(string rawData) {
            using(SHA512 sha256Hash = SHA512.Create()) {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for(int i = 0; i < bytes.Length; i++) {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public abstract bool CheckConnection(ref string refMensaje, string valComandoApi);
        public bool SendPostJson(string valJsonStr, string valComandoApi, string valToken, string valNumeroDocumento = "", eTipoDocumentoFactura valTipoDocumento = eTipoDocumentoFactura.NoAsignado) {
            string vResultMessage = "";
            try {
                string strTipoDocumento = LibEnumHelper.GetDescription(valTipoDocumento);
                strTipoDocumento = "La " + strTipoDocumento + " No. " + valNumeroDocumento;
                HttpClient vHttpClient = new HttpClient();
                vHttpClient.BaseAddress = new Uri(LoginUser.URL);
                if(!LibString.IsNullOrEmpty(valToken)) {
                    vHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", valToken);
                }
                HttpContent vContent = new StringContent(valJsonStr, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> vHttpRespMsg = vHttpClient.PostAsync(valComandoApi, vContent);
                vHttpRespMsg.Wait();
                vResultMessage = vHttpRespMsg.Result.RequestMessage.ToString();
                if(vHttpRespMsg.Result.StatusCode == System.Net.HttpStatusCode.OK) {
                    vHttpRespMsg.Result.EnsureSuccessStatusCode();
                } else if(vHttpRespMsg.Result.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new Exception($"Revise su conexión a Internet y la URL del servicio.");
                }
                if(vHttpRespMsg.Result.Content is null) {
                    throw new Exception($"Revise su conexión a Internet y la URL del servicio.");
                } else {
                    Task<string> HttpResq = vHttpRespMsg.Result.Content.ReadAsStringAsync();
                    HttpResq.Wait();
                    PostRequest = HttpResq.Result.ToString();
                    return true;
                }
            } catch(Exception vEx) {
                throw vEx;
            }
        }
    }
}