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
using System.Diagnostics.Eventing.Reader;

namespace Galac.Saw.LibWebConnector {
    public class clsConectorJsonTheFactory: clsConectorJson {
        string strTipoDocumento;        

        public clsConectorJsonTheFactory(ILoginUser valloginUser) : base(valloginUser) {
            LoginUser = valloginUser;
            Token = string.Empty;
        }

        public override bool CheckConnection(ref string refMensaje, string valComandoApi) {
            stPostResq vRequest = new stPostResq();
            try {
                bool vResult = false;
                string vJsonStr = FormatingJSON(LoginUser);
                vRequest = SendPostJson(vJsonStr, valComandoApi, "");
                refMensaje = vRequest.mensaje;
                if (vRequest.Aprobado) {
                    Token = vRequest.token;
                    LoginUser.MessageResult = vRequest.mensaje;
                    vResult = !LibString.IsNullOrEmpty(Token);
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

        public override stPostResq SendPostJson(string valJsonStr, string valComandoApi, string valToken, string valNumeroDocumento = "", int valTipoDocumento = 0) {
            string vResultMessage = "";
            string vMensajeDeValidacion = "";
            stPostResq infoReqs = new stPostResq();
            try {
                strTipoDocumento = (valTipoDocumento == 8 ? "Nota de Entrega" : valTipoDocumento == 1 ? "Nota de Crédito" : valTipoDocumento == 2 ? "Nota de Débito" : "Factura");
                strTipoDocumento = "La " + strTipoDocumento + " No. " + valNumeroDocumento;
                HttpClient vHttpClient = new HttpClient();
                vHttpClient.BaseAddress = new Uri(LoginUser.URL);
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
                if (vHttpRespMsg.Result.Content is null) {
                    throw new Exception("Revise su conexión a Internet, Revise que la URL del servicio sea la correcta.");
                }else if (vHttpRespMsg.Result.StatusCode== System.Net.HttpStatusCode.Unauthorized) {
                    throw new Exception(vHttpRespMsg.Result.ReasonPhrase+ "\r\nToken Expirado.");
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
                        return infoReqs;
                    } else if (LibString.S1IsEqualToS2(infoReqs.codigo, "403")) {
                        infoReqs.mensaje = vMensajeDeValidacion + "\r\nUsuario o clave inválida.\r\nPor favor verifique los datos de conexión con su Imprenta Digital.";
                        infoReqs.Aprobado = false;
                    } else if (LibString.S1IsEqualToS2(infoReqs.codigo, "201")) {
                        infoReqs.Aprobado = false;
                        infoReqs.mensaje = vMensajeDeValidacion + "\r\n" + strTipoDocumento + " ya existe en la Imprenta Digital.";
                    } else if (LibString.S1IsEqualToS2(infoReqs.codigo, "203")) {
                        infoReqs.Aprobado = false;
                        infoReqs.mensaje = infoReqs.mensaje +".\r\n"+ vMensajeDeValidacion + "\r\n" + strTipoDocumento + " no pudo ser enviado a la Imprenta Digital, debe sincronizar el documento.";
                    } else if (!LibString.S1IsEqualToS2(infoReqs.codigo, "200")) {                        
                        infoReqs.Aprobado = false;
                        infoReqs.mensaje = vMensajeDeValidacion+ "\r\n." + strTipoDocumento + " no pudo ser enviado a la Imprenta Digital, debe sincronizar el documento.";
                    }
                    infoReqs.mensaje = vResultMessage + infoReqs.mensaje;
                    GeneraLogDeErrores(infoReqs.mensaje, valJsonStr);
                    throw new Exception(infoReqs.mensaje);
                }
            } catch (AggregateException vEx) {
                string vMensaje = vEx.InnerException.InnerException.Message;
                if (vEx.InnerException.InnerException.HResultPublic() == -2146233079) {
                    vMensaje = vMensaje + "\r\nRevise su conexión a Internet, Revise que la URL del servicio sea la correcta.\r\nDebe sincronizar el documento.";
                }
                throw new Exception(vMensaje); throw new Exception(vEx.InnerException.InnerException.Message);           
            } catch (Exception vEx) {
                throw vEx;                
            }            
        }
    }
}