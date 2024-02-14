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
using LibGalac.Aos.Catching;
using System.Diagnostics.SymbolStore;
using System.Net.Http;
using System.Threading.Tasks;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.LibWebConnector {
    public class clsSuscripcion {
        bool RifInfotax {
            get { return LibString.S1IsEqualToS2(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRIF"), "J305125430"); }
        }

        public string GetRutaEndpointProduccionInterno { get { return ""; } }
        public string GetRutaEndpointProduccionClientes { get { return ""; } }
      
        public class DatosSuscripcion {
            [JsonProperty("TenantNombre")]
            public string TenantNombre { get; set; }
            [JsonProperty("EdicionNombre")]
            public string EdicionNombre { get; set; }
            [JsonProperty("NumeroMaximoDeUsuarios")]
            public int NumeroMaximoDeUsuarios { get; set; }
            [JsonProperty("EstaActivoPeriodoDeGracia")]
            public bool EstaActivoPeriodoDeGracia { get; set; }          
            [JsonProperty("fechaDeFinalizacionDeLaActivacion")]
            public DateTime fechaDeFinalizacionDeLaActivacion { get; set; }             
            [JsonProperty("CantidadDeUsuariosFacturados")]
            public int CantidadDeUsuariosFacturados { get; set; }
            public DatosSuscripcion() {
                TenantNombre = string.Empty;
                EdicionNombre = string.Empty;
                NumeroMaximoDeUsuarios = 0;
                EstaActivoPeriodoDeGracia = false;
                fechaDeFinalizacionDeLaActivacion = LibDate.MinDateForDB();
                CantidadDeUsuariosFacturados = 0;
            }
            public List<DatosSuscripcionCaracteristicas> Caracteristicas { get; set; } = new List<DatosSuscripcionCaracteristicas>();

        }

        public class CompaniasDelTenant {
            public string numeroDeIdentificacion { get;set; }
            public string nombre { get; set; }
            public bool conectadaConAdministraivo { get; set; }
            public CompaniasDelTenant() {
                numeroDeIdentificacion = string.Empty;
                nombre = string.Empty;
                conectadaConAdministraivo = false;
            }
        }        

        public class DatosSuscripcionCaracteristicas {
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
        }

        public class DatosDeConexion {
            [JsonProperty("rifDeLicencia")]
            public string rifDeLicencia { get; set; }
            [JsonProperty("serialDeConexion")]
            public string serialDeConexion { get; set; }
            [JsonProperty("servidor")]
            public string servidor { get; set; }
            [JsonProperty("baseDeDatos")]
            public string baseDeDatos { get; set; }
            [JsonProperty("consecutivoCompania")]
            public string consecutivoCompania { get; set; }
            [JsonProperty("companiaRif")]
            public string companiaRif { get; set; }
            [JsonProperty("companiaNombre")]
            public string companiaNombre { get; set; }
            [JsonProperty("usuario")]
            public string usuario { get; set; }
            public DatosDeConexion() {
                rifDeLicencia = string.Empty;
                serialDeConexion = string.Empty;
                servidor = string.Empty;
                baseDeDatos = string.Empty;
                consecutivoCompania = string.Empty;
                companiaRif = string.Empty;
                companiaNombre = string.Empty;
                usuario = string.Empty;
            }
        }
        #region Clase para el manejo de errores
        [JsonObject("error")]
        public class Error {
        [JsonProperty("code")]
        public   string code { get; set; }
        [JsonProperty("message")]
        public   string message { get; set; }
        [JsonProperty("details")]
        public    string details { get; set; }
        [JsonProperty("data")]
        public  data data { get; set; } 
        [JsonProperty("validationErrors")]
        public ValidationErrors[] validationErrors{ get; set; }         
        }

        public class data {
            [JsonProperty("additionalProp1")]
            public  string additionalProp1 { get; set; }
            [JsonProperty("additionalProp2")]
            public string additionalProp2 { get; set; }
            [JsonProperty("additionalProp3")]
            public  string additionalProp3 { get; set; }            
        }

        public class ValidationErrors {
            [JsonProperty("message")]
            public string message{ get; set; }
            [JsonProperty("members")]
            public string[] members{ get; set; }
        }
        #endregion Clase para el manejo de errores

        HttpWebResponse GetResponseGET(string valUrl) {
            try {
                HttpWebResponse vResult;
                Uri vBaseUri = new Uri(GetEndPointGVentas());
                var vRequest = (HttpWebRequest)WebRequest.Create(new Uri(vBaseUri, valUrl));
                vRequest.ContentType = "application/json";
                vRequest.Method = "GET";
                vResult = (HttpWebResponse)vRequest.GetResponse();
                return vResult;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message,eExceptionManagementType.Controlled);                
            }
        }

        public bool GetResponsePUT(string valUrl, string valJSonData, ref string refMensaje) {
            bool vResult = false;
            Error infoReqs = new Error();
            try {
                HttpClient vHttpClient = new HttpClient();
                vHttpClient.BaseAddress = new Uri(GetEndPointGVentas());
                HttpContent vContent = new StringContent(valJSonData, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> vHttpRespMsg = vHttpClient.PutAsync(valUrl, vContent);
                vHttpRespMsg.Wait();
                if (vHttpRespMsg.Result.StatusCode == HttpStatusCode.OK) {
                    vHttpRespMsg.Result.EnsureSuccessStatusCode();
                }
                Task<string> HttpResq = vHttpRespMsg.Result.Content.ReadAsStringAsync();
                HttpResq.Wait();
                if (HttpResq.Result == "true") {
                    vResult = true;
                } else {
                    infoReqs = JsonConvert.DeserializeObject<Error>(HttpResq.Result);
                    string vMensaje = HttpResq.Result;
                    int vCorte = LibString.IndexOf(vMensaje, "\"message\":");
                    vMensaje = LibString.SubString(vMensaje, vCorte);
                    vCorte = LibString.IndexOf(vMensaje, ":");
                    vMensaje = LibString.SubString(vMensaje, vCorte + 2);
                    vCorte = LibString.IndexOf(vMensaje, ",");
                    vMensaje = LibString.SubString(vMensaje, 0, vCorte - 1);
                    refMensaje = vMensaje;
                }
                return vResult;
            } catch (AggregateException) {
                throw;
            } catch (Exception) {
                throw;
            }
        }      

        string AddParametroNumeroDeIdentificacionFiscal(string valUrl, string valNumeroDeIDentificacionFiscal, string valKeyNumeroDeIdentificacion) {
            string vResult = valUrl;
            if (!LibString.IsNullOrEmpty(valNumeroDeIDentificacionFiscal)) {
                vResult += System.Uri.EscapeDataString(valKeyNumeroDeIdentificacion);
                vResult += "=";
                vResult += System.Uri.EscapeDataString(valNumeroDeIDentificacionFiscal);
            }
            return vResult;
        }

        string GetResultFromResponse(HttpWebResponse valResponse) {
            string vResult = string.Empty;
            if (valResponse != null && (valResponse.StatusCode == HttpStatusCode.OK)) {
                vResult = new StreamReader(valResponse.GetResponseStream()).ReadToEnd();
            }
            return vResult;
        }

        public DatosSuscripcion GetCaracteristicaGVentas(string valNumeroDeIdentificacion) {
            DatosSuscripcion vResult = new DatosSuscripcion();
            try {
                //TODO:Falta Url real
                HttpWebResponse vResponse = GetResponseGET(AddParametroNumeroDeIdentificacionFiscal(@"/api/saas/tenants/datos-suscripcion-por-numero-de-identificacion?", valNumeroDeIdentificacion, "numeroDeIdentificacion"));
                if (vResponse.StatusCode == HttpStatusCode.OK) {
                    vResult = JsonConvert.DeserializeObject<DatosSuscripcion>(GetResultFromResponse(vResponse), new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" });
                    return vResult;
                }
            } catch (JsonSerializationException) {
                new DatosSuscripcion();
            } catch (Exception vEx) {
                throw vEx;
            }
            return vResult;
        }

        public DatosSuscripcion GetCaracteristicaGVentas() {
            return GetCaracteristicaGVentas(GetNroDeIdentificacionFiscal());
        }

        public bool ActivarConexionGVentas(int valConsecutivoCompania, string valSerialConectorGVentas, string valRIFCompaniaAdministartivo, string valNombreCompaniaAdministartivo, string valNombreUsuarioOperaciones, string valDatabaseName, string valServerName) {
            bool vResult = false;
            string vMensaje = string.Empty;
            try {
                DatosDeConexion insDatosDeConexion = new DatosDeConexion();
                insDatosDeConexion.baseDeDatos = valDatabaseName;
                insDatosDeConexion.servidor = valServerName;
                insDatosDeConexion.usuario = valNombreUsuarioOperaciones;
                insDatosDeConexion.companiaRif = valRIFCompaniaAdministartivo;
                insDatosDeConexion.rifDeLicencia = GetNroDeIdentificacionFiscal();
                insDatosDeConexion.companiaNombre = valNombreCompaniaAdministartivo;
                insDatosDeConexion.serialDeConexion = valSerialConectorGVentas;
                insDatosDeConexion.consecutivoCompania = LibConvert.ToStr(valConsecutivoCompania);
                string JSonData = JsonConvert.SerializeObject(insDatosDeConexion, Formatting.Indented);
                if (GetResponsePUT(@"/api/saas/tenants/configurar-conexion-de-la-compania", JSonData, ref vMensaje)) {
                    vResult = true;
                } else {
                    throw new GalacException(vMensaje, eExceptionManagementType.Validation);
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }

        public int GetCantidadDeUsuariosActivos(string valNumeroDeIdentificacion) {
            int vResult = -1;
            //try {
            //    //TODO:Falta Url real              
            //    HttpWebResponse vResponse = GetResponseGET(AddParametroNumeroDeIdentificacionFiscal(@"/api/saas/tenants/usuarios-disponibles?", valNumeroDeIdentificacion, "numeroDeIdentificacionFiscal"));
            //    if (vResponse.StatusCode == HttpStatusCode.OK) {
            //        vResult = LibConvert.ToInt(new StreamReader(GetResultFromResponse(vResponse)).ReadToEnd());
            //        return vResult;
            //    }
            //} catch (Exception) {
            //    throw;
            //}
            //
            //
            return vResult;
        }

        public ObservableCollection<string> GetCompaniaGVentas(string valURI) {
            ObservableCollection<string> vResult = new ObservableCollection<string>();
            ObservableCollection<CompaniasDelTenant> vListaCompaniasDelTenant = new ObservableCollection<CompaniasDelTenant>();
            try {
                //  TODO:Falta Url real
                string vNumeroDeIdentificacion = GetNroDeIdentificacionFiscal();
                HttpWebResponse vResponse = GetResponseGET(AddParametroNumeroDeIdentificacionFiscal(@"/api/saas/tenants/companias-del-tenant?", vNumeroDeIdentificacion, "numeroDeIdentificacionFiscal"));
                if (vResponse.StatusCode == HttpStatusCode.OK) {
                    vListaCompaniasDelTenant = JsonConvert.DeserializeObject<ObservableCollection<CompaniasDelTenant>>(GetResultFromResponse(vResponse));
                    var vCompaniasParaMostrar = vListaCompaniasDelTenant.Where(CompaniaEnTenant => !CompaniaEnTenant.conectadaConAdministraivo).Select(CompaniaFueraDelTenant => CompaniaFueraDelTenant.nombre + " | " + CompaniaFueraDelTenant.numeroDeIdentificacion);
                    vResult = new ObservableCollection<string>(vCompaniasParaMostrar);                   
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
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
