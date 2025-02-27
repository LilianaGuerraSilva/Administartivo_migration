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
                refMensaje = vRequest.mensaje;
                if(vRequest.Aprobado) {
                    LoginUser.MessageResult = vRequest.mensaje;
                    vResult = vRequest.Aprobado;
                    Token = vRequest.token;
                    StrongeId = vRequest.strongId;
                } else {
                    LoginUser.MessageResult = vRequest.mensaje;
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
                XElement xmlReq = ConvertXmlDocumentToXElement(JsonConvert.DeserializeXmlNode(vPostRequest, "GpResult"));
                stRespuestaUD infoReqs = new stRespuestaUD();
                if(LibString.S1IsEqualToS2(eComandosPostUnidigital.Autenticacion.GetDescription(), valComandoApi)) {
                    infoReqs.token = LibXml.GetPropertyString(xmlReq, "accessToken");
                    infoReqs.Aprobado = !LibString.IsNullOrEmpty(infoReqs.token);
                    if(infoReqs.Aprobado) {
                        infoReqs.strongId = xmlReq.Descendants("series").FirstOrDefault().Descendants("strongId").FirstOrDefault().Value;
                    } else {
                        infoReqs.codigo = xmlReq.Descendants("errors").FirstOrDefault().Descendants("code").FirstOrDefault().Value;
                        infoReqs.mensaje = xmlReq.Descendants("errors").FirstOrDefault().Descendants("message").FirstOrDefault().Value;
                    }
                } else {
                    bool vOut = true;
                    infoReqs.Aprobado = !bool.Parse(LibXml.GetPropertyString(xmlReq, "hasErrors"));
                    if(infoReqs.Aprobado) {
                        infoReqs.IDGUID = xmlReq.Descendants("GpResult").FirstOrDefault()?.Descendants("result").FirstOrDefault().Value ?? "";
                    } else {
                        infoReqs.codigo = xmlReq.Descendants("errors").FirstOrDefault().Descendants("code").FirstOrDefault().Value;
                        infoReqs.mensaje = xmlReq.Descendants("errors").FirstOrDefault().Descendants("message").FirstOrDefault().Value;
                    }
                }
                if(infoReqs.Aprobado) {
                    infoReqs.mensaje = "Succes";
                    return infoReqs;
                } else if(LibString.S1IsEqualToS2(infoReqs.codigo, "0000")) {
                    return infoReqs;
                } else if(LibString.S1IsEqualToS2(infoReqs.codigo, "201")) {
                    infoReqs.Aprobado = false;
                    infoReqs.mensaje = vMensajeDeValidacion + "\r\n" + strTipoDocumento + " ya existe en la Imprenta Digital.";
                } else if(LibString.S1IsEqualToS2(infoReqs.codigo, "203")) {
                    infoReqs.Aprobado = false;
                    infoReqs.mensaje = infoReqs.mensaje + ".\r\n" + vMensajeDeValidacion + ".\r\n" + strTipoDocumento + " no pudo ser enviada a la Imprenta Digital, debe sincronizar el documento.";
                } else if(!LibString.S1IsEqualToS2(infoReqs.codigo, "200")) {
                    infoReqs.Aprobado = false;
                    infoReqs.mensaje = vMensajeDeValidacion + "\r\n." + strTipoDocumento + " no pudo ser enviada a la Imprenta Digital, debe sincronizar el documento.";
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

        private XElement ConvertXmlDocumentToXElement(XmlDocument xmlDoc) {
            using(var nodeReader = new XmlNodeReader(xmlDoc)) {
                nodeReader.MoveToContent();
                return new XElement("GpData",XElement.Load(nodeReader));
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
                    message="No Encontrado",                    
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