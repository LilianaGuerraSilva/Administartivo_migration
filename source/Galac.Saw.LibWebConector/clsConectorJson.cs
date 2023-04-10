using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Galac.Adm.Ccl.ImprentaDigital;
using LibGalac.Aos.Base;

namespace Galac.Saw.LibWebConnector {
    public class clsConectorJson {
        ILoginUser _LoginUser;
        string _Token;
        public string Token {
            get {
                return _Token;
            }
        }

        public clsConectorJson(ILoginUser valloginUser) {
            _LoginUser = valloginUser;
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

        private string FormatingJSON(ILoginUser valloginUser) {
            string vResult = "";
            stUserLoginCnn vUsrLgn = new stUserLoginCnn();
            vUsrLgn.usuario = valloginUser.User;
            vUsrLgn.clave = valloginUser.Password;
            vResult = vResult.Replace(nameof(vUsrLgn.usuario), valloginUser.UserKey);
            vResult = vResult.Replace(nameof(vUsrLgn.clave), valloginUser.PasswordKey);
            vResult = SerializeJSON(vUsrLgn);
            return vResult;
        }

        public string CheckConnection() {
            try {
                string vJsonStr = FormatingJSON(_LoginUser);
                var vRequest = SendPostJson(vJsonStr, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Autenticacion), "");
                _Token = vRequest.token;
                _LoginUser.MessageResult = vRequest.mensaje;
                return vRequest.mensaje;
            } catch (Exception) {
                throw;
            }
        }
        public stLoginResq SendPostJson(string valJsonStr, string valComandoApi, string valToken) {
            string vResultMessage = "";
            try {
                HttpClient vHttpClient = new HttpClient();
                vHttpClient.BaseAddress = new Uri(_LoginUser.URL);
                if (!LibString.IsNullOrEmpty(valToken)) {
                    vHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", valToken);
                }
                HttpContent vContent = new StringContent(valJsonStr, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> vHttpRespMsg = vHttpClient.PostAsync(valComandoApi, vContent);
                vHttpRespMsg.Wait();
                vResultMessage = vHttpRespMsg.Result.RequestMessage.ToString();
                vHttpRespMsg.Result.EnsureSuccessStatusCode();
                if (vHttpRespMsg.Result.Content is null || vHttpRespMsg.Result.Content.Headers.ContentType?.MediaType != "application/json") {
                    return new stLoginResq() { mensaje = "Error de conexión al Host", token = "", codigo = "402", expiracion = "" };
                } else {
                    Task<string> HttpResq = vHttpRespMsg.Result.Content.ReadAsStringAsync();
                    HttpResq.Wait();
                    stLoginResq infoReqs = JsonConvert.DeserializeObject<stLoginResq>(HttpResq.Result);
                    return infoReqs;
                }
            } catch (Exception vEx) {
                string vErrMensaje = vEx.Message + "\r\n" + vResultMessage+"\r\n"+ valJsonStr;                                
                throw new LibGalac.Aos.Catching.GalacException(vErrMensaje, LibGalac.Aos.Catching.eExceptionManagementType.Controlled);
            }
        }
    }
}