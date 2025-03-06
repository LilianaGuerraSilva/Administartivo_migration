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

        public stRespuestaNV SendPostJsonNV(string valJsonStr, string valComandoApi, string valToken, string valNumeroDocumento = "", eTipoDocumentoFactura valTipoDocumento = eTipoDocumentoFactura.NoAsignado) {          
            stRespuestaNV vReqNV = new stRespuestaNV();
            try {
                strTipoDocumento = LibEnumHelper.GetDescription(valTipoDocumento);
                strTipoDocumento = "La " + strTipoDocumento + " No. " + valNumeroDocumento;
                string vPostRequest = ExecutePostJson(valJsonStr, valComandoApi, valToken, valNumeroDocumento, valTipoDocumento);
                if(LibString.S1IsEqualToS2(valComandoApi, eComandosPostNovus.EstadoDocumento.GetDescription())) {
                    vReqNV = ConvertSimpleRequestNV(vPostRequest, valTipoDocumento);
                } else {
                    vReqNV = JsonConvert.DeserializeObject<stRespuestaNV>(vPostRequest);
                }
                if(vReqNV.success) {                    
                    return vReqNV;
                } else if(vReqNV.errorNV.Value.codeNV == null && !LibString.IsNullOrEmpty(vReqNV.errorNV.Value.messageNV)) {
                    vReqNV.success = false;
                    vReqNV.messageNV = vReqNV.errorNV.Value.messageNV;
                    return vReqNV;
                } else if(LibString.S1IsEqualToS2(vReqNV.errorNV.Value.codeNV, "1")) {
                    vReqNV.messageNV = vReqNV.errorNV.Value.messageNV + ".\r\nPor favor verifique los datos de conexión\r\ncon su Imprenta Digital.";
                } else if(LibString.S1IsEqualToS2(vReqNV.errorNV.Value.codeNV, "2")) {
                    vReqNV.messageNV = vReqNV.errorNV.Value.messageNV + ".\r\nPor favor verifique los datos del documento.";
                } else if(LibString.S1IsEqualToS2(vReqNV.errorNV.Value.codeNV, "3")) {
                    vReqNV.messageNV = vReqNV.errorNV.Value.messageNV + ".\r\nPor favor verifique los datos de conexión\r\n con su Imprenta Digital.";
                } else if(LibString.S1IsEqualToS2(vReqNV.errorNV.Value.codeNV, "4")) {
                    vReqNV.messageNV = vReqNV.errorNV.Value.messageNV + ".\r\nPor favor verifique los datos del documento.";
                } else if(LibString.S1IsEqualToS2(vReqNV.errorNV.Value.codeNV, "5")) {
                    vReqNV.messageNV = vReqNV.errorNV.Value.messageNV + ".\r\nPor favor verifique los datos del documento.";
                } else if(LibString.S1IsEqualToS2(vReqNV.errorNV.Value.codeNV, "11")) {
                    vReqNV.messageNV = vReqNV.errorNV.Value.messageNV + ".\r\nPor favor verifique los datos del documento.";
                } else {
                    vReqNV.messageNV = vReqNV.errorNV.Value.messageNV;
                }
                return vReqNV;
            } catch(AggregateException vEx) {
                string vMensaje = vEx.InnerException.InnerException.Message;
                if(vEx.InnerException.InnerException.HResultPublic() == -2146233079) {
                    vMensaje = vMensaje + $"\r\nRevise su conexión a Internet y la URL del servicio.";
                }
                throw new Exception(vMensaje);
            } catch(Exception vEx) {
                throw vEx;
            }
        }

        private stRespuestaNV ConvertSimpleRequestNV(string valHttpResq, eTipoDocumentoFactura valTipoDocumento) {
            stRespuestaNV vResult = new stRespuestaNV();
            string vTipoDocumentoNV = GetTipoDocumentoNV(valTipoDocumento);
            stRespuestaStatusNV vReqStNV = JsonConvert.DeserializeObject<stRespuestaStatusNV>(valHttpResq);
            if(vReqStNV.success) {
                stDataRespuestaStatusNV? stDataNV = vReqStNV.dataNV
                    .Where(x => x.HasValue && x.Value.idtipodocumento == vTipoDocumentoNV)
                    .FirstOrDefault();
                if(stDataNV.HasValue) {
                    vResult = new stRespuestaNV() {
                        success = vReqStNV.success,
                        messageNV = vReqStNV.messageNV ?? "Enviada",
                        dataNV = new stDataRespuestaNV {
                            numerodocumento = stDataNV.Value.numerodocumento,
                            fecha = stDataNV.Value.fecha,
                            documento = stDataNV.Value.documento
                        }
                    };
                } else {
                    vResult = new stRespuestaNV() {
                        success = false,
                        messageNV = "No Enviada",
                        errorNV=new stErrorRespuestaNV {
                            messageNV = "Este registro no se encuentra."
                        }
                    };
                }
            } else {
                string vMensaje = vReqStNV.errorNV.Value.messageNV ?? "error desconocido";                
                vResult = new stRespuestaNV() {
                    messageNV = "Error desconocido",
                    errorNV = new stErrorRespuestaNV {
                        messageNV = vMensaje
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