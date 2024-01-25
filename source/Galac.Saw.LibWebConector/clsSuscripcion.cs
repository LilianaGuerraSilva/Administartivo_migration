using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using LibGalac.Aos.Base;
using Newtonsoft.Json.Converters;
using LibGalac.Aos.Cnf;
using LibGalac.Aos.Ccl.Settings;
using LibGalac.Aos.Brl.Settings;
using System.Collections.ObjectModel;

namespace Galac.Saw.LibWebConnector {
    public class clsSuscripcion {
        bool RifInfotax {
            get { return LibString.S1IsEqualToS2(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRIF"), "J305125430"); }
        }

        public string GetRutaEndpointProduccionInterno { get { return ""; } }
        public string GetRutaEndpointProduccionClientes { get { return ""; } }

        public class DatosSuscripcion {
            public string TenantNombre { get; set; }
            public string EdicionNombre { get; set; }
            public int NumeroMaximoDeUsuarios { get; set; }
            public bool EstaActivoPeriodoDeGracia { get; set; }
            public DateTime fechaDeFinalizacionDeLaActivacion { get; set; }
            public int CantidadDeUsuariosFacturados { get; set; }
            public List<DatosSuscripcionCaracteristicas> Caracteristicas { get; set; } = new List<DatosSuscripcionCaracteristicas>();

        }

        public class DatosSuscripcionCaracteristicas {
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
        }

        HttpWebResponse GetResponseGET(string valUrl) {
            HttpWebResponse vResult;
            Uri vBaseUri = new Uri(GetEndPointGVentas());
            var vRequest = (HttpWebRequest)WebRequest.Create(new Uri(vBaseUri, valUrl));
            vRequest.ContentType = "application/json";
            vRequest.Method = "GET";
            vResult = (HttpWebResponse)vRequest.GetResponse();
            return vResult;
        }

        string AddParametroNumeroDeIdentificacionFiscal(string valUrl, string valNumeroDeIDentificacionFiscal) {
            string vResult = valUrl;
            if (!LibString.IsNullOrEmpty(valNumeroDeIDentificacionFiscal)) {
                vResult += System.Uri.EscapeDataString("numeroDeIdentificacionFiscal");
                vResult += "=";
                vResult += System.Uri.EscapeDataString(valNumeroDeIDentificacionFiscal);
            }
            return vResult;
        }

        string GetResultFromResponse(HttpWebResponse valResponse) {
            string vResult = string.Empty;
            if (valResponse != null && (valResponse.StatusCode== HttpStatusCode.OK)) {
                vResult = new StreamReader(valResponse.GetResponseStream()).ReadToEnd();
            }
            return vResult;
        }

        public DatosSuscripcion GetCaracteristicaGVentas(string valNumeroDeIdentificacion) {
            DatosSuscripcion vResult = new DatosSuscripcion();
            //try {
            //    //TODO:Falta Url real
            //    HttpWebResponse vResponse = GetResponseGET(AddParametroNumeroDeIdentificacionFiscal(@"/api/saas/datos-suscripcion-por-numero-de-identificacion?", valNumeroDeIdentificacion));
            //    if (vResponse.StatusCode == HttpStatusCode.OK) {
            //        vResult = JsonConvert.DeserializeObject<DatosSuscripcion>(GetResultFromResponse(vResponse), new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" });
            //        return vResult;
            //    }
            //} catch (Exception) {
            //    throw;
            //}
            return vResult;
        }

        public DatosSuscripcion GetCaracteristicaGVentas() {
            return GetCaracteristicaGVentas(GetNroDeIdentificacionFiscal());
        }

        public int GetCantidadDeUsuariosActivos(string valNumeroDeIdentificacion) {
            int vResult = -1;
            //try {
            //    //TODO:Falta Url real              
            //    HttpWebResponse vResponse = GetResponseGET(AddParametroNumeroDeIdentificacionFiscal(@"/api/saas/usuarios-disponibles?", valNumeroDeIdentificacion));
            //    if (vResponse.StatusCode == HttpStatusCode.OK) {
            //        vResult = LibConvert.ToInt(new StreamReader(GetResultFromResponse(vResponse)).ReadToEnd());
            //        return vResult;
            //    }
            //} catch (Exception) {
            //    throw;
            //}
            return vResult;
        }

        public ObservableCollection<string> GetCompaniaGVentas(string valNumeroDeIdentificacion) {
            ObservableCollection<string> vResult = new ObservableCollection<string>();
            //try {
            //    //TODO:Falta Url real
            //    HttpWebResponse vResponse = GetResponseGET(AddParametroNumeroDeIdentificacionFiscal(@"/api/saas/datos-suscripcion-por-numero-de-identificacion?", valNumeroDeIdentificacion));
            //    if (vResponse.StatusCode == HttpStatusCode.OK) {
            //        string vResponseStr = GetResultFromResponse(vResponse);
            //        //TODO:Resultado real
            //        return vResult;
            //    }
            //} catch (Exception) {
            //    throw;
            //}
            return vResult;
        }

        private string GetEndPointGVentas() {
            string vEndpoint = LibAppSettings.ReadAppSettingsKey("URLENDPOINTGVENTAS");
            if (LibString.IsNullOrEmpty(vEndpoint)) { //Si el setting existe sabemos que vamos contra otro endpoint
                if (RifInfotax) { //Validamos si la compañia actual es infotax
                    return GetRutaEndpointProduccionInterno;
                } else {
                    return GetRutaEndpointProduccionClientes;
                }
            } else {
                return vEndpoint;
            }
        }

        static string GetNroDeIdentificacionFiscal() {
            string vNroRif = LibAppSettings.ReadAppSettingsKey("NRORIFQA");
            if (!LibString.IsNullOrEmpty(vNroRif)) { //Si el setting existe sabemos que vamos a utilizar el rif del setting y no la licencia
                return vNroRif;
            } else {
                return IDFiscal();
            }
        }

        private static string IDFiscal() {
            return ((IParametersLibPdn)new LibParametersLibNav()).GetIdFiscal();
        }
    }
}