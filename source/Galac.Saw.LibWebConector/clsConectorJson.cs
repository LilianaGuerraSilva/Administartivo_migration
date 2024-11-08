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
using System.Collections.Generic;

namespace Galac.Saw.LibWebConnector {
    public abstract class clsConectorJson {
        string strTipoDocumento;      
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
            get;set;
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

        public abstract bool CheckConnection(ref string refMensaje, string valComandoApi);
        public abstract stPostResq SendPostJson(string valJsonStr, string valComandoApi, string valToken, string valNumeroDocumento = "", int valTipoDocumento = 0);
        /*
        public bool CheckConnection11(ref string refMensaje, string valComandoApi) {
            stPostResq vRequest = new stPostResq();
            try {
                bool vResult = false;
                string vJsonStr = FormatingJSON(_LoginUser);
                vRequest = SendPostJson(vJsonStr, valComandoApi, "");
                refMensaje = vRequest.mensaje;
                if (vRequest.Aprobado) {
                    _Token = vRequest.token;
                    _LoginUser.MessageResult = vRequest.mensaje;
                    vResult = !LibString.IsNullOrEmpty(_Token);
                } else {
                    vResult = false;
                }
                return vResult;
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Alert);
            }
        }

        public stPostResq SendPostJson22(string valJsonStr, string valComandoApi, string valToken, string valNumeroDocumento = "", int valTipoDocumento = 0) {
            string vResultMessage = "";
            string vMensajeDeValidacion = "";
            stPostResq infoReqs = new stPostResq();
            try {
                strTipoDocumento = (valTipoDocumento == 8 ? "Nota de Entrega" : valTipoDocumento == 1 ? "Nota de Crédito" : valTipoDocumento == 2 ? "Nota de Débito" : "Factura");
                strTipoDocumento = "La " + strTipoDocumento + " No. " + valNumeroDocumento;
                HttpClient vHttpClient = new HttpClient();
                vHttpClient.BaseAddress = new Uri(_LoginUser.URL);
                if (!LibString.IsNullOrEmpty(valToken)) {
                    vHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", valToken);
                }
                HttpContent vContent = new StringContent(valJsonStr, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> vHttpRespMsg = vHttpClient.PostAsync(valComandoApi, vContent);
                vHttpRespMsg.Wait();
                vResultMessage = vHttpRespMsg.Result.RequestMessage.ToString();
                if (vHttpRespMsg.Result.StatusCode == System.Net.HttpStatusCode.OK) {
                    vHttpRespMsg.Result.EnsureSuccessStatusCode();
                }
                if (vHttpRespMsg.Result.Content is null || vHttpRespMsg.Result.Content.Headers.ContentType?.MediaType != "application/json") {
                    throw new GalacException("Usuario o clave inválida.\r\nPor favor verifique los datos de conexión con su Imprenta Digital.", eExceptionManagementType.Alert);
                } else {
                    Task<string> HttpResq = vHttpRespMsg.Result.Content.ReadAsStringAsync();
                    HttpResq.Wait();
                    infoReqs = JsonConvert.DeserializeObject<stPostResq>(HttpResq.Result);
                    List<string> listValidaciones = infoReqs.validaciones;
                    if (listValidaciones != null) {
                        vMensajeDeValidacion = string.Join(",", infoReqs.validaciones);
                    }
                    if (LibString.S1IsEqualToS2(infoReqs.codigo, "200")) {
                        infoReqs.Aprobado = true;
                    } else if (LibString.S1IsEqualToS2(infoReqs.codigo, "403")) {
                        infoReqs.mensaje = vMensajeDeValidacion + "\r\nUsuario o clave inválida.\r\nPor favor verifique los datos de conexión con su Imprenta Digital.";
                        infoReqs.Aprobado = false;
                    } else if (LibString.S1IsEqualToS2(infoReqs.codigo, "201")) {
                        infoReqs.Aprobado = false;
                        infoReqs.mensaje = vMensajeDeValidacion + "\r\n" + strTipoDocumento + " ya existe en la Imprenta Digital.";
                    } else if (LibString.S1IsEqualToS2(infoReqs.codigo, "203")) {
                        infoReqs.Aprobado = false;
                        infoReqs.mensaje = vMensajeDeValidacion + "\r\n" + strTipoDocumento + " no pudo ser enviado a la Imprenta Digital, debe sincronizar el documento.";
                    } else if (!LibString.S1IsEqualToS2(infoReqs.codigo, "200")) {
                        throw new Exception(vMensajeDeValidacion);
                    }
                }
            } catch (AggregateException) {
                infoReqs.Aprobado = false;
                infoReqs.mensaje = "Falla de conexión con su Imprenta Digital.\r\nPor favor verifique los datos de conexión o servicio de internet.";
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                string vPath = LibDirectory.GetProgramFilesGalacDir() + "\\" + LibDefGen.ProgramInfo.ProgramInitials + "\\ImprentaDigital";
                if (!LibDirectory.DirExists(vPath)) {
                    LibDirectory.CreateDir(vPath);
                }
                vPath = vPath + @"\ImprentaDigitalResult.txt";
                LibFile.WriteLineInFile(vPath, vEx.Message + "\r\n" + vResultMessage + "\r\n" + valJsonStr, false);
                infoReqs.Aprobado = false;
                infoReqs.mensaje = strTipoDocumento + " no pudo ser enviada a la Imprenta Digital, debe sincronizar el documento.";
            }
            return infoReqs;
        }
        */
    }
}