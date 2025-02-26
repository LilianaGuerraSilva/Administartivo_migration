using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Galac.Adm.Ccl.ImprentaDigital;

namespace Galac.Saw.LibHttp {
    public class clsLibHttp : ILibHttp {
        string ILibHttp.HttpExecutePost(string valJsonStr, string valUrl, string valComandoApi, string valToken) {
            try {
                string vResult = string.Empty;
                HttpClient vHttpClient = new HttpClient();
                vHttpClient.BaseAddress = new Uri(valUrl);
                if(!string.IsNullOrEmpty(valToken)) {
                    vHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", valToken);
                }
                HttpContent vContent = new StringContent(valJsonStr, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, valUrl + valComandoApi) {
                    Version = HttpVersion.Version11,
                    Content = vContent
                };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Tls13;
                HttpResponseMessage vHttpRespMsg = vHttpClient.SendAsync(request).Result;
                vResult = vHttpRespMsg.RequestMessage.ToString();
                if(vHttpRespMsg.StatusCode == System.Net.HttpStatusCode.OK) {
                    vHttpRespMsg.EnsureSuccessStatusCode();
                } else if(vHttpRespMsg.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new Exception($"{vHttpRespMsg.StatusCode.ToString()}\r\nRevise su conexión a Internet y la URL del servicio.");
                } else if(vHttpRespMsg.StatusCode == System.Net.HttpStatusCode.BadGateway) {
                    throw new Exception($"{vHttpRespMsg.StatusCode.ToString()}\r\nRevise su conexión a Internet y la URL del servicio.");
                }
                if(vHttpRespMsg.Content is null) {
                    throw new Exception($"{vResult}Revise su conexión a Internet y la URL del servicio.");
                } else {
                    Task<string> HttpResq = vHttpRespMsg.Content.ReadAsStringAsync();
                    HttpResq.Wait();
                    vResult = HttpResq.Result.ToString();
                    return vResult;
                }
            } catch(HttpRequestException rex) {
                throw new Exception(rex.Message);
            } catch(Exception vEx) {
                throw vEx;
            }
        }
    }
}
