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
                        infoReqs.Codigo= infoReqEnvio.errorsUD[0].codeUD;
                        return infoReqs;
                    } else {                        
                        infoReqs.Exitoso = !infoReqs.hasErrors;
                        infoReqs.information= infoReqEnvio.information;
                        infoReqs.StrongeID = infoReqEnvio.result ?? "";
                    }
                } else if(LibString.S1IsEqualToS2(eComandosPostUnidigital.EstadoDocumento.GetDescription(), valComandoApi)) {
                    infoReqs = JsonConvert.DeserializeObject<stRespuestaUD>(vPostRequest);
                    if(infoReqs.hasErrors) {
                        infoReqs.Exitoso = false;
                        return infoReqs;
                    } else {
                        infoReqs.Exitoso = !infoReqs.hasErrors;
                        infoReqs.StrongeID = infoReqs.result.FirstOrDefault();
                    }
                } else {

                }
                return infoReqs;
            } catch(AggregateException vEx) {
                string vMensaje = vEx.InnerException.InnerException.Message;
                if(vEx.InnerException.InnerException.HResultPublic() == -2146233079) {
                    vMensaje = vMensaje + "\r\nRevise su conexión a Internet, Revise que la URL del servicio sea la correcta.\r\nDebe sincronizar el documento.";
                }
                throw new Exception(vEx.InnerException.InnerException.Message);
            } catch(Exception vEx) {
                throw vEx;
            }
        }

        //public stRespuestaUD SendGetJsonUD(string valContent, string valComandoApi, string valToken, string valNumeroDocumento = "", eTipoDocumentoFactura valTipoDocumento = eTipoDocumentoFactura.NoAsignado) {
        //    try {
        //        string vMensajeDeValidacion = string.Empty;
        //        string vPostRequest = ExecutePostJson(valContent, valComandoApi, valToken, valNumeroDocumento, valTipoDocumento);               
        //        //stRespuestaUD infoReqs = new stRespuestaUD();
        //        if(LibString.S1IsEqualToS2(eComandosPostUnidigital.Autenticacion.GetDescription(), valComandoApi)) {
        //            infoReqs.token = LibXml.GetPropertyString(xmlReq, "accessToken");
        //            infoReqs.Aprobado = !LibString.IsNullOrEmpty(infoReqs.token);
        //            if(infoReqs.Aprobado) {
        //                infoReqs.strongId = xmlReq.Descendants("series").FirstOrDefault().Descendants("strongId").FirstOrDefault().Value;
        //            } else {
        //                infoReqs.codigo = xmlReq.Descendants("errors").FirstOrDefault().Descendants("code").FirstOrDefault().Value;
        //                infoReqs.mensaje = xmlReq.Descendants("errors").FirstOrDefault().Descendants("message").FirstOrDefault().Value;
        //            }
        //        } else {
        //            bool vOut = true;
        //            infoReqs.Aprobado = !bool.TryParse(LibXml.GetPropertyString(xmlReq, "hasErrors"), out vOut);
        //            if(infoReqs.Aprobado) {
        //                infoReqs.IDGUID = xmlReq.Descendants("GpResult").FirstOrDefault()?.Descendants("result")?.FirstOrDefault().Value ?? "";
        //            } else {
        //                infoReqs.codigo = xmlReq.Descendants("errors").FirstOrDefault()?.Descendants("code")?.FirstOrDefault().Value ?? "";
        //                infoReqs.mensaje = xmlReq.Descendants("errors").FirstOrDefault()?.Descendants("message")?.FirstOrDefault().Value ?? "";
        //            }
        //        }
        //        if(infoReqs.Aprobado) {
        //            infoReqs.mensaje = "Succes";
        //            return infoReqs;
        //        } else if(LibString.S1IsEqualToS2(infoReqs.codigo, "0000")) {
        //            return infoReqs;
        //        } else if(LibString.S1IsEqualToS2(infoReqs.codigo, "201")) {
        //            infoReqs.Aprobado = false;
        //            infoReqs.mensaje = vMensajeDeValidacion + "\r\n" + strTipoDocumento + " ya existe en la Imprenta Digital.";
        //        } else if(LibString.S1IsEqualToS2(infoReqs.codigo, "203")) {
        //            infoReqs.Aprobado = false;
        //            infoReqs.mensaje = infoReqs.mensaje + ".\r\n" + vMensajeDeValidacion + ".\r\n" + strTipoDocumento + " no pudo ser enviada a la Imprenta Digital, debe sincronizar el documento.";
        //        } else if(!LibString.S1IsEqualToS2(infoReqs.codigo, "200")) {
        //            infoReqs.Aprobado = false;
        //            infoReqs.mensaje = vMensajeDeValidacion + "\r\n." + strTipoDocumento + " no pudo ser enviada a la Imprenta Digital, debe sincronizar el documento.";
        //        }
        //        return infoReqs;
        //    } catch(AggregateException vEx) {
        //        string vMensaje = vEx.InnerException.InnerException.Message;
        //        if(vEx.InnerException.InnerException.HResultPublic() == -2146233079) {
        //            vMensaje = vMensaje + "\r\nRevise su conexión a Internet, Revise que la URL del servicio sea la correcta.\r\nDebe sincronizar el documento.";
        //        }
        //        throw new Exception(vEx.InnerException.InnerException.Message);
        //    } catch(Exception vEx) {
        //        throw vEx;
        //    }
        //}

        //private XElement ConvertXmlDocumentToXElement(XmlDocument xmlDoc) {
        //    using(var nodeReader = new XmlNodeReader(xmlDoc)) {
        //        nodeReader.MoveToContent();
        //        return new XElement("GpData",XElement.Load(nodeReader));
        //    }
        //}

       
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