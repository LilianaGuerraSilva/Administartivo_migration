using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.ImprentaDigital;
using Galac.Saw.Ccl.SttDef;
using System.Linq;

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

        public override stPostResq SendPostJson(string valJsonStr, string valComandoApi, string valToken, string valNumeroDocumento = "", eTipoDocumentoFactura valTipoDocumento = eTipoDocumentoFactura.NoAsignado) {
            string vResultMessage = "";
            stPostResq vReqs = new stPostResq();
            stRespuestaNV vReqNV = new stRespuestaNV();            
            try {
                strTipoDocumento = LibEnumHelper.GetDescription(valTipoDocumento);
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
                } else if (vHttpRespMsg.Result.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new Exception($"Revise su conexión a Internet y la URL del servicio.");
                }
                if (vHttpRespMsg.Result.Content is null) {
                    throw new Exception($"Revise su conexión a Internet y la URL del servicio.");
                } else {
                    Task<string> HttpResq = vHttpRespMsg.Result.Content.ReadAsStringAsync();
                    HttpResq.Wait();
                    if (LibString.S1IsEqualToS2(valComandoApi, eComandosPostNovus.EstadoDocumento.GetDescription())) {
                        vReqNV = ConvertSimpleRequestNV(HttpResq.Result.ToString(), valTipoDocumento);
                    } else {
                        vReqNV = JsonConvert.DeserializeObject<stRespuestaNV>(HttpResq.Result.ToString());
                    }
                    if (vReqNV.success) {
                        vReqs = new stPostResq() {
                            Aprobado = vReqNV.success,
                            mensaje = vReqNV.message ?? string.Empty,
                            resultados = new stRespuestaTF() {
                                Estado = !LibString.IsNullOrEmpty(vReqNV.data.Value.numerodocumento) ? "Enviado" : "No Existe",
                                tipoDocumento = vReqNV.data.Value.documento,
                                numeroControl = vReqNV.data.Value.numerodocumento,
                                fechaAsignacion = vReqNV.data.Value.fecha
                            },
                        };
                        return vReqs;
                    } else if (vReqNV.error.Value.code == null) {
                        vReqs.mensaje = vReqNV.error.Value.message;
                    } else if (LibString.S1IsEqualToS2(vReqNV.error.Value.code, "1")) {
                        vReqs.mensaje = vReqNV.error.Value.message + ".\r\nPor favor verifique los datos de conexión\r\ncon su Imprenta Digital.";
                    } else if (LibString.S1IsEqualToS2(vReqNV.error.Value.code, "2")) {
                        vReqs.mensaje = vReqNV.error.Value.message + ".\r\nPor favor verifique los datos del documento.";
                    } else if (LibString.S1IsEqualToS2(vReqNV.error.Value.code, "3")) {
                        vReqs.mensaje = vReqNV.error.Value.message + ".\r\nPor favor verifique los datos de conexión\r\n con su Imprenta Digital.";
                    } else if (LibString.S1IsEqualToS2(vReqNV.error.Value.code, "4")) {
                        vReqs.mensaje = vReqNV.error.Value.message + ".\r\nPor favor verifique los datos del documento.";
                    } else if (LibString.S1IsEqualToS2(vReqNV.error.Value.code, "5")) {
                        vReqs.mensaje = vReqNV.error.Value.message + ".\r\nPor favor verifique los datos del documento.";
                    } else {
                        vReqs.mensaje = vReqNV.error.Value.message;
                    }
                    GeneraLogDeErrores(vResultMessage + vReqs.mensaje, valJsonStr);
                    throw new Exception(vReqs.mensaje);
                }
            } catch (AggregateException vEx) {
                string vMensaje = vEx.InnerException.InnerException.Message;
                if (vEx.InnerException.InnerException.HResultPublic() == -2146233079) {
                    vMensaje = vMensaje + $"\r\nRevise su conexión a Internet y la URL del servicio.";
                }
                throw new Exception(vMensaje);
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        private stRespuestaNV ConvertSimpleRequestNV(string valHttpResq, eTipoDocumentoFactura valTipoDocumento) {
            stRespuestaNV vResult = new stRespuestaNV();
            string vTipoDocumentoNV = GetTipoDocumentoNV(valTipoDocumento);
            stRespuestaStatusNV vReqStNV = JsonConvert.DeserializeObject<stRespuestaStatusNV>(valHttpResq);
            if(vReqStNV.success) {
                stDataRespuestaStatusNV stDataNV = vReqStNV.data.Where(x => {
                    return x.Value.idtipodocumento == vTipoDocumentoNV;
                }).FirstOrDefault().Value;
                vResult = new stRespuestaNV() {
                    success = vReqStNV.success,
                    message = vReqStNV.message ?? string.Empty,
                    data = new stDataRespuestaNV {
                        numerodocumento = stDataNV.numerodocumento,
                        fecha = stDataNV.fecha,
                        documento = stDataNV.documento
                    }
                };
            } else {
                string vMensaje = vReqStNV.error.Value.message;
                int vPos = LibString.IndexOf(vMensaje, ".");
                if(vPos > 0) {
                    vMensaje = LibString.InsertAt(vMensaje, " en el servicio de imprenta", vPos);
                }
                vResult = new stRespuestaNV() {
                    error = new stErrorRespuestaNV {
                        message = vMensaje
                    }
                };
            }
            return vResult;
        }

        private string GetTipoDocumentoNV(eTipoDocumentoFactura valTipoDocumento) {
            switch (valTipoDocumento) {
                case eTipoDocumentoFactura.Factura:
                    return "1";
                case eTipoDocumentoFactura.NotaDeDebito:
                    return "2";
                case eTipoDocumentoFactura.NotaDeCredito:
                    return "3";
                case eTipoDocumentoFactura.NotaEntrega:
                    return "4";
                default:
                    return "1";
            }
        }
    }
}