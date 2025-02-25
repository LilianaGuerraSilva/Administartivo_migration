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
                } else if(vReqNV.error.Value.code == null && !LibString.IsNullOrEmpty(vReqNV.error.Value.message)) {
                    vReqNV.success = false;
                    vReqNV.message = vReqNV.error.Value.message;
                    return vReqNV;
                } else if(LibString.S1IsEqualToS2(vReqNV.error.Value.code, "1")) {
                    vReqNV.message = vReqNV.error.Value.message + ".\r\nPor favor verifique los datos de conexión\r\ncon su Imprenta Digital.";
                } else if(LibString.S1IsEqualToS2(vReqNV.error.Value.code, "2")) {
                    vReqNV.message = vReqNV.error.Value.message + ".\r\nPor favor verifique los datos del documento.";
                } else if(LibString.S1IsEqualToS2(vReqNV.error.Value.code, "3")) {
                    vReqNV.message = vReqNV.error.Value.message + ".\r\nPor favor verifique los datos de conexión\r\n con su Imprenta Digital.";
                } else if(LibString.S1IsEqualToS2(vReqNV.error.Value.code, "4")) {
                    vReqNV.message = vReqNV.error.Value.message + ".\r\nPor favor verifique los datos del documento.";
                } else if(LibString.S1IsEqualToS2(vReqNV.error.Value.code, "5")) {
                    vReqNV.message = vReqNV.error.Value.message + ".\r\nPor favor verifique los datos del documento.";
                } else if(LibString.S1IsEqualToS2(vReqNV.error.Value.code, "11")) {
                    vReqNV.message = vReqNV.error.Value.message + ".\r\nPor favor verifique los datos del documento.";
                } else {
                    vReqNV.message = vReqNV.error.Value.message;
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
                stDataRespuestaStatusNV? stDataNV = vReqStNV.data
                    .Where(x => x.HasValue && x.Value.idtipodocumento == vTipoDocumentoNV)
                    .FirstOrDefault();
                if(stDataNV.HasValue) {
                    vResult = new stRespuestaNV() {
                        success = vReqStNV.success,
                        message = vReqStNV.message ?? "Enviada",
                        data = new stDataRespuestaNV {
                            numerodocumento = stDataNV.Value.numerodocumento,
                            fecha = stDataNV.Value.fecha,
                            documento = stDataNV.Value.documento
                        }
                    };
                } else {
                    vResult = new stRespuestaNV() {
                        success = false,
                        message = "No Enviada",
                        error=new stErrorRespuestaNV { 
                            message= "No se encontró el documento solicitado."
                        }
                    };
                }
            } else {
                string vMensaje = vReqStNV.error.Value.message ?? "Error desconocido";                
                vResult = new stRespuestaNV() {
                    message = "Error desconocido",
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