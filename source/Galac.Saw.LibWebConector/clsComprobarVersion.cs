using LibGalac.Aos.Base;
using LibGalac.Aos.Cnf;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Galac.Saw.LibWebConnector.clsSuscripcion;

namespace Galac.Saw.LibWebConnector {
    public class clsComprobarVersion {
        
        private static readonly HttpClient client = new HttpClient();

        public  string ObtenerToken() {
            var url =  GetUrlApiVersion() + "/authorize/Token";
            var json = "{\"username\": \"userGalac\", \"password\": \"galac20.\"}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try {
                Task<HttpResponseMessage> response = client.PostAsync(url, content);
                response.Wait();
                if (response.Result.StatusCode == HttpStatusCode.OK) {
                    response.Result.EnsureSuccessStatusCode();
                    string responseBody = ReadAsString(response);
                    var token = Newtonsoft.Json.Linq.JObject.Parse(responseBody)["token"].ToString();
                    return token;
                } else
                    return string.Empty;

            }
            catch (HttpRequestException e) {
                return null;
            }
        }
        
        public string ReadAsString(Task<HttpResponseMessage> vHttpRespMsg) {
            Task<string> HttpResq = vHttpRespMsg.Result.Content.ReadAsStringAsync();
            HttpResq.Wait();
            if (HttpResq.Result != "true") {
                var infoReqs = JsonConvert.DeserializeObject<Error>(HttpResq.Result);
                string vMensaje = HttpResq.Result;
                int vCorte = LibString.IndexOf(vMensaje, "\"message\":");
                vMensaje = LibString.SubString(vMensaje, vCorte);
                vCorte = LibString.IndexOf(vMensaje, ":");
                vMensaje = LibString.SubString(vMensaje, vCorte + 2);
                vCorte = LibString.IndexOf(vMensaje, ",");
                vMensaje = LibString.SubString(vMensaje, 0, vCorte - 1);
            }
            return HttpResq.Result.ToString();
        }
        public string ObtenerVersion(string token) {
            var url = GetUrlApiVersion() + "/api/Version";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url);
                responseTask.Wait();
                HttpResponseMessage response =  responseTask.Result;
                response.EnsureSuccessStatusCode();
                Task <string> responseBody =  response.Content.ReadAsStringAsync();
                responseBody.Wait();
                return responseBody.Result;
            }
            catch (HttpRequestException e) {
                return null;
            }
        }
        public string ObtenerVersionHomologada() {
            var token = ObtenerToken();
            if (token != null) {
                return ObtenerVersion(token);
            }
            return null;
        }
        private string GetUrlApiVersion()        {
            string vUrlApiVersion = LibAppSettings.ReadAppSettingsKey("URLAPIVERSION");
            if (LibString.IsNullOrEmpty(vUrlApiVersion))            { 
                vUrlApiVersion = "https://apiversion.galac.com:9544";
            }
            // Validar y limpiar el último carácter si es '/'
            if (vUrlApiVersion.EndsWith("/")) {
                vUrlApiVersion = vUrlApiVersion.TrimEnd('/');
            }
            return vUrlApiVersion;
        }
    }
}
