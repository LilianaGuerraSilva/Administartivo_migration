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

namespace Galac.Saw.LibWebConnector {
    public class clsConectorJsonUnidigital : clsConectorJson {
        string strTipoDocumento;

        public clsConectorJsonUnidigital(ILoginUser valloginUser) : base(valloginUser) {
            LoginUser = valloginUser;
            Token = string.Empty;
        }

        public override bool CheckConnection(ref string refMensaje, string valComandoApi) {
            stPostResq vRequest = new stPostResq();
            try {
                bool vResult = false;
                string vJsonStr = GetJsonUser(LoginUser, eProveedorImprentaDigital.Unidigital);
                vRequest = SendPostJsonUD(vJsonStr, valComandoApi,"", "");
                refMensaje = vRequest.mensaje;
                if(vRequest.Aprobado) {
                    Token = vRequest.token;
                    LoginUser.MessageResult = vRequest.mensaje;
                    vResult = !LibString.IsNullOrEmpty(Token);
                } else {
                    vResult = false;
                }
                return vResult;
            } catch(GalacException) {
                throw;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Alert);
            }
        }

        public stPostResq SendPostJsonUD(string valJsonStr, string valComandoApi, string valToken, string valNumeroDocumento = "", eTipoDocumentoFactura valTipoDocumento = eTipoDocumentoFactura.NoAsignado) {                      
            try {
                string vMensajeDeValidacion = string.Empty;
                string vPostRequest = ExecutePostJson(valJsonStr, valComandoApi, valToken, valNumeroDocumento, valTipoDocumento);
                stPostResq infoReqs = JsonConvert.DeserializeObject<stPostResq>(vPostRequest);
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
                    infoReqs.mensaje = infoReqs.mensaje + ".\r\n" + vMensajeDeValidacion + ".\r\n" + strTipoDocumento + " no pudo ser enviada a la Imprenta Digital, debe sincronizar el documento.";
                } else if (!LibString.S1IsEqualToS2(infoReqs.codigo, "200")) {
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