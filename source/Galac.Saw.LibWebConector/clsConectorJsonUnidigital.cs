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
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Galac.Saw.LibWebConnector {
    public class clsConectorJsonUnidigital : clsConectorJson {
        string strTipoDocumento;
        public string StrongeId { get; private set; }       
        public clsConectorJsonUnidigital(ILoginUser valloginUser) : base(valloginUser) {
            LoginUser = valloginUser;
            Token = string.Empty;
        }

        public override bool CheckConnection(ref string refMensaje, string valComandoApi) {
            stRespuestaUD vRequest = new stRespuestaUD();
            try {
                bool vResult = false;
                string vJsonStr = GetJsonUser(LoginUser, eProveedorImprentaDigital.Unidigital);
                vRequest = SendPostJsonUD(vJsonStr, valComandoApi, "", "");                
                if(!vRequest.hasErrors) {                                      
                    Token = vRequest.tokenUD;
                    StrongeId = vRequest.StrongeID;
                    vResult = true;
                } else {
                    LoginUser.MessageResult = string.Join("\r\n", vRequest.errorsUD.FirstOrDefault().messageUD);
                    refMensaje = LoginUser.MessageResult;
                    vResult = false;
                }
                return vResult;
            } catch(GalacException) {
                throw;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Alert);
            }
        }

        public stRespuestaUD SendPostJsonUD(string valJsonStr, string valComandoApi, string valToken, string valNumeroDocumento = "", eTipoDocumentoFactura valTipoDocumento = eTipoDocumentoFactura.NoAsignado) {
            try {
                string vMensajeDeValidacion = string.Empty;
                string vPostRequest = ExecutePostJson(valJsonStr, valComandoApi, valToken, valNumeroDocumento, valTipoDocumento);
                stRespuestaUD infoReqs = new stRespuestaUD();
                if(LibString.S1IsEqualToS2(eComandosPostUnidigital.Autenticacion.GetDescription(), valComandoApi)) {
                    stRespuestaLoginUD LoginReqs = JsonConvert.DeserializeObject<stRespuestaLoginUD>(vPostRequest);
                    infoReqs.tokenUD = LoginReqs.accessToken;
                    infoReqs.hasErrors = LibString.IsNullOrEmpty(infoReqs.tokenUD);
                    if(infoReqs.hasErrors) {
                        infoReqs.hasErrors = true;
                        infoReqs.errorsUD = LoginReqs.errorsUD;
                    } else {
                        infoReqs.StrongeID = LoginReqs.seriesUD.FirstOrDefault().strongId;
                        infoReqs.tokenUD = LoginReqs.accessToken;
                    }
                } else if(LibString.S1IsEqualToS2(eComandosPostUnidigital.Emision.GetDescription(), valComandoApi)) {
                    stRespuestaEnvioUD infoReqEnvio = JsonConvert.DeserializeObject<stRespuestaEnvioUD>(vPostRequest);
                    if(infoReqEnvio.hasErrors) {
                        infoReqs.Exitoso = false;
                        infoReqs.MessageUD = infoReqEnvio.errorsUD[0].messageUD;
                        infoReqs.Codigo = infoReqEnvio.errorsUD[0].codeUD;
                        infoReqs.StrongeID = string.Empty;
                        return infoReqs;
                    } else {
                        infoReqs.Exitoso = !infoReqEnvio.hasErrors;
                        infoReqs.information = infoReqEnvio.information;
                        infoReqs.StrongeID = infoReqEnvio.result ?? "";
                    }
                } else if(LibString.S1IsEqualToS2(eComandosPostUnidigital.EstadoDocumento.GetDescription(), valComandoApi)) {
                    stRespuestaStatusUD infoReqStatus = JsonConvert.DeserializeObject<stRespuestaStatusUD>(vPostRequest);
                    if(infoReqStatus.hasErrors) {
                        infoReqs.Exitoso = false;
                        infoReqs.MessageUD = infoReqStatus.errorsUD[0].messageUD;
                        infoReqs.Codigo = infoReqStatus.errorsUD[0].codeUD;
                        infoReqs.StrongeID = string.Empty;
                        return infoReqs;
                    } else {
                        infoReqs.Exitoso = !infoReqStatus.hasErrors;
                        if(infoReqStatus.result != null && infoReqStatus.result.Count() > 0) {
                            infoReqs.StrongeID = infoReqStatus.result[0].strongId;
                            infoReqs.NumeroControl = infoReqStatus.result[0].controlUD;
                            infoReqs.TipoDocumento = infoReqStatus.result[0].documentType;
                            infoReqs.FechaAsignacion = infoReqStatus.result[0].emissionDate;
                            infoReqs.Codigo = "200";
                            infoReqs.MessageUD = "Enviada";
                        } else {
                            infoReqs.StrongeID = string.Empty;
                            infoReqs.NumeroControl = string.Empty;
                            infoReqs.TipoDocumento = string.Empty;
                            infoReqs.MessageUD = "documento no existe en el servicio";
                            infoReqs.Exitoso = false;
                            infoReqs.Codigo = "203";
                        }
                    }
                } else {

                }
                return infoReqs;
            } catch(AggregateException vEx) {
                string vMensaje = vEx.InnerException.InnerException.Message;
                if(vEx.InnerException.InnerException.HResultPublic() == -2146233079) {
                    vMensaje += "\r\nRevise su conexión a Internet. Valide que la URL del servicio sea la correcta.";
                    if(LibString.S1IsEqualToS2(eComandosPostUnidigital.Emision.GetDescription(), valComandoApi) || LibString.S1IsEqualToS2(eComandosPostUnidigital.EstadoDocumento.GetDescription(), valComandoApi)) {
                        vMensaje += ",\r\nDebe sincronizar el documento.";
                    }
                }
                throw new Exception(vMensaje);
            } catch(Exception vEx) {
                throw vEx;
            }
        }

        public stRespuestaUD SendGetJsonUD(string valContent, string valComandoApi, string valToken, string valNumeroDocumento = "", eTipoDocumentoFactura valTipoDocumento = eTipoDocumentoFactura.NoAsignado) {
            try {
                string vMensajeDeValidacion = string.Empty;
                string vPostRequest = ExecuteGetJson(valContent, valComandoApi, valToken, valNumeroDocumento, valTipoDocumento);
                stRespuestaUD infoReqs = new stRespuestaUD();
                if(LibString.S1IsEqualToS2(eComandosPostUnidigital.ObtenerNroControl.GetDescription(), valComandoApi)) {
                    stRespuestaStatusGUIDUD infoReqStatus = JsonConvert.DeserializeObject<stRespuestaStatusGUIDUD>(vPostRequest);
                    if(infoReqStatus.hasErrors) {
                        infoReqs.Exitoso = false;
                        infoReqs.MessageUD = infoReqStatus.errorsUD[0].messageUD;
                        infoReqs.Codigo = infoReqStatus.errorsUD[0].codeUD;
                        infoReqs.StrongeID = string.Empty;
                        return infoReqs;
                    } else {
                        infoReqs.Exitoso = !infoReqStatus.hasErrors;
                        if(infoReqStatus.result != null) {
                            infoReqs.StrongeID = infoReqStatus.result.Value.strongId;
                            infoReqs.NumeroControl = infoReqStatus.result.Value.controlNumber;
                            infoReqs.TipoDocumento = infoReqStatus.result.Value.codeName;
                            infoReqs.MessageUD = "enviado";
                            infoReqs.Codigo = "200";                           
                        } else {
                            infoReqs.StrongeID = string.Empty;
                            infoReqs.NumeroControl = string.Empty;
                            infoReqs.TipoDocumento = string.Empty;
                            infoReqs.MessageUD = "documento no existe en el servicio";
                            infoReqs.Exitoso = false;
                            infoReqs.Codigo = "203";
                        }
                    }
                } else {

                }
                return infoReqs;
            } catch(AggregateException vEx) {
                string vMensaje = vEx.InnerException.InnerException.Message;
                if(vEx.InnerException.InnerException.HResultPublic() == -2146233079) {
                    vMensaje = vMensaje + "\r\nRevise su conexión a Internet. Revise que la URL del servicio sea la correcta.";
                }
                throw new Exception(vMensaje + "\r\n" + vEx.InnerException.InnerException.Message);
            } catch(Exception vEx) {
                throw vEx;
            }
        }

        private string GetTipoDocumentoUD(eTipoDocumentoFactura valTipoDocumento) {
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