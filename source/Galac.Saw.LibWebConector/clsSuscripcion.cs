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
using System.Collections.ObjectModel;

namespace Galac.Saw.LibWebConnector {
    public class clsSuscripcion {
        public bool RifInfotax {
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

        public clsSuscripcion() {
        }

        public DatosSuscripcion GetCaracteristicaGVentas(string valNumeroDeIdentificacion) {
            try {
                Uri vBaseUri = new Uri(GetEndPointGVentas());
                string vUrl = AddParametroNumeroDeIdentificacionFiscal(@"/api/saas/datos-suscripcion-por-numero-de-identificacion?", valNumeroDeIdentificacion);
                var vRequest = (HttpWebRequest)WebRequest.Create(new Uri(vBaseUri, vUrl));
                vRequest.ContentType = "application/json";
                vRequest.Method = "GET";
                HttpWebResponse vResponse = (HttpWebResponse)vRequest.GetResponse();
                if (vResponse.StatusCode == HttpStatusCode.OK) {
                    Stream vResponseStream = vResponse.GetResponseStream();
                    string vResponseStr = new StreamReader(vResponseStream).ReadToEnd();
                    return JsonConvert.DeserializeObject<DatosSuscripcion>(vResponseStr, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" });
                }

            } catch (Exception) {
                throw;
            }
            return null;
        }

        public int GetCantidadDeUsuariosActivos(string valNumeroDeIdentificacion) {
            int vResult = -1;
            try {
                Uri vBaseUri = new Uri(GetEndPointGVentas());
                string vUrl = AddParametroNumeroDeIdentificacionFiscal(@"/api/saas/usuarios-disponibles?", valNumeroDeIdentificacion);
                var vRequest = (HttpWebRequest)WebRequest.Create(new Uri(vBaseUri, vUrl));
                vRequest.ContentType = "application/json";
                vRequest.Method = "GET";
                HttpWebResponse response;
                response = (HttpWebResponse)vRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK) {
                    Stream vResponseStream = response.GetResponseStream();
                    string vResponseStr = new StreamReader(vResponseStream).ReadToEnd();
                    vResult = LibConvert.ToInt(vResponseStr);
                    return vResult;
                }
            } catch (Exception) {
                throw;
            }
            return vResult;
        }

        public ObservableCollection<string> GetCompaniaGVentas(string valNumeroDeIdentificacion) {
            ObservableCollection<string> vResult = new ObservableCollection<string>();
            try {
                /*
                Uri vBaseUri = new Uri(GetEndPointGVentas());

                //TODO:Falta Url real
                string vUrl = AddParametroNumeroDeIdentificacionFiscal(@"/api/saas/datos-suscripcion-por-numero-de-identificacion?", valNumeroDeIdentificacion);
                var vRequest = (HttpWebRequest)WebRequest.Create(new Uri(vBaseUri, vUrl));
                vRequest.ContentType = "application/json";
                vRequest.Method = "GET";
                HttpWebResponse response;
                response = (HttpWebResponse)vRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK) {
                    Stream vResponseStream = response.GetResponseStream();
                    string vResponseStr = new StreamReader(vResponseStream).ReadToEnd();
                */
                    //TODO:Resultado real
                    vResult = new ObservableCollection<string>();
                    return vResult;
                //}
            } catch (Exception) {
                throw;
            }
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
    }
}
