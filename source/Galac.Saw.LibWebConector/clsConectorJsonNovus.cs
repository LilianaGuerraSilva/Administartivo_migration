using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Saw.LibWebConnector {
    public class clsConectorJsonNovus : clsConectorJson {
        string strTipoDocumento;

        public clsConectorJsonNovus(ILoginUser valloginUser) : base(valloginUser) {
            LoginUser = valloginUser;
            Token = string.Empty;
        }

        public override bool CheckConnection(ref string refMensaje, string valComandoApi) {
            try {
                bool vResult = true;
                Token = LoginUser.Password;
                return vResult;
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Alert);
            }
        }

        public override stPostResq SendPostJson(string valJsonStr, string valComandoApi, string valToken, string valNumeroDocumento = "", int valTipoDocumento = 0) {
            string vResultMessage = "";
            //string vMensajeDeValidacion = "";
            stPostResq vReqs = new stPostResq();
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
                } else {
                    Task<string> HttpResq = vHttpRespMsg.Result.Content.ReadAsStringAsync();
                    HttpResq.Wait();
                    stRespuestaNV vReqsNV = JsonConvert.DeserializeObject<stRespuestaNV>(HttpResq.Result.ToString());
                    if (vReqsNV.success) {
                        vReqs = new stPostResq() {
                            Aprobado = vReqsNV.success,
                            mensaje = vReqsNV.message,
                            resultados = new stRespuestaTF() {
                                numeroControl = vReqsNV.data.Value.numerodocumento,
                                fechaAsignacion = vReqsNV.data.Value.fecha
                            }                            
                        };
                        return vReqs;
                    } else if (LibString.S1IsEqualToS2(vReqsNV.error.Value.code, "1")) {
                        vReqs.mensaje = vReqsNV.error.Value.message + " \r\nPor favor verifique los datos de conexión con su Imprenta Digital.";
                    } else if (LibString.S1IsEqualToS2(vReqsNV.error.Value.code, "2")) {
                        vReqs.mensaje = vReqsNV.error.Value.message + " \r\nPor favor verifique los datos del documento.";
                    } else if (LibString.S1IsEqualToS2(vReqsNV.error.Value.code, "3")) {
                        vReqs.mensaje = vReqsNV.error.Value.message + " \r\nPor favor verifique los datos de conexión con su Imprenta Digital.";
                    } else if (LibString.S1IsEqualToS2(vReqsNV.error.Value.code, "4")) {
                        vReqs.mensaje = vReqsNV.error.Value.message + " \r\nPor favor verifique los datos del documento.";
                    } else if (LibString.S1IsEqualToS2(vReqsNV.error.Value.code, "5")) {
                        vReqs.mensaje = vReqsNV.error.Value.message + " \r\nPor favor verifique los datos del documento.";
                    } else {
                        vReqs.mensaje = vReqsNV.error.Value.message;
                    }
                    GeneraLogDeErrores(vResultMessage + vReqs.mensaje, valJsonStr);
                    throw new Exception(vReqs.mensaje);
                }
            } catch (AggregateException vEx) {
                string vMensaje = vEx.InnerException.InnerException.Message;
                if (vEx.InnerException.InnerException.HResultPublic() == -2146233079) {
                    vMensaje = vMensaje + "\r\nRevise su conexión a Internet, Revise que la URL del servicio sea la correcta.";
                }
                throw new Exception(vMensaje);
            } catch (Exception vEx) {
                throw vEx;
            }
        }
    }
}