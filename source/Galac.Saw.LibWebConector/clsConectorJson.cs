using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Galac.Adm.Ccl.ImprentaDigital;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.LibWebConnector {
    public class clsConectorJson {
        string strTipoDocumento;
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
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Alert);
            }
        }

        public stPostResq SendPostJson(string valJsonStr, string valComandoApi, string valToken, string valNumeroDocumento = "", int valTipoDocumento = 0) {
            string vResultMessage = "";
            try {
                strTipoDocumento = (valTipoDocumento == 8 ? "Nota de Entrega" : valTipoDocumento == 1 ? "Nota de Crédito" : valTipoDocumento == 2 ? "Nota de Débito" : "Factura");
                strTipoDocumento = "La " + strTipoDocumento + " No " + valNumeroDocumento;
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
                    throw new GalacException("Usuario o clave inválida.\r\nPor favor verifique los datos de conexión con su Imprenta Digital.", eExceptionManagementType.Alert);
                } else {
                    Task<string> HttpResq = vHttpRespMsg.Result.Content.ReadAsStringAsync();
                    HttpResq.Wait();
                    stPostResq infoReqs = JsonConvert.DeserializeObject<stPostResq>(HttpResq.Result);
                    if (LibString.S1IsEqualToS2(infoReqs.codigo, "403")) {
                        throw new GalacException("Usuario o clave inválida.\r\nPor favor verifique los datos de conexión con su Imprenta Digital.", eExceptionManagementType.Alert);
                    } else if (LibString.S1IsEqualToS2(infoReqs.codigo, "201")) {
                        throw new GalacException(strTipoDocumento + " ya existe en la Imprenta Digital.", eExceptionManagementType.Alert);
                    } else if (!LibString.S1IsEqualToS2(infoReqs.codigo, "200")) {
                        throw new GalacException(infoReqs.mensaje, eExceptionManagementType.Alert);
                    }
                    return infoReqs;
                }
            } catch (AggregateException) {
                throw new GalacException("Falla de conexión con su Imprenta Digital.\r\nPor favor verifique los datos de conexión o servicio de internet.", eExceptionManagementType.Alert);
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                string vPath = LibDirectory.GetProgramFilesGalacDir() + @"\" + LibDefGen.ProgramInfo.ProgramInitials + @"\ImprentaDigital";
                if (!LibDirectory.DirExists(vPath)) {
                    LibDirectory.CreateDir(vPath);
                }
                vPath = vPath + @"\ImprentaDigitalResult.txt";
                LibFile.WriteLineInFile(vPath, vEx.Message + "\r\n" + vResultMessage + "\r\n" + valJsonStr, false);
                throw new GalacException(strTipoDocumento + " tiene datos faltantes para ser enviados a la Imprenta Digital.", eExceptionManagementType.Alert);
            }
        }
    }
}