using LibGalac.Aos.Base;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Galac.Adm.Brl.Venta {
    public class clsCajaProcesos {
        public const string UrlApiMFiscalPlataformaQA = "https://localhost:51120/api/estahomologada";//"https://mfiscalapi-qa.galac.com/api/estahomologada";
        public const string UrlApiMFiscalPlataformaProduccion = "https://mfiscalapi.galac.com/api/estahomologada";


        public bool SendPostEstaHomologadaMaquinaFiscal(string valFabricante, string valModelo) {
            bool vresult = false;
            string url = UrlApiMFiscalPlataformaQA;
            string json = "{\"Fabricante\":\"" + valFabricante + "\",\"Modelo\":\"" + valModelo + "\"}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = json.Length;
            try {
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream())) {
                writer.Write(json);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
                        string result = reader.ReadToEnd();
                        JObject jsonObject = JObject.Parse(result);
                        string success = jsonObject["success"].ToString();
                        string code = jsonObject["code"].ToString();
                        string message = jsonObject["message"].ToString();
                        vresult = LibConvert.ToBool(success);
                    }
                }
            } catch (Exception) {
                throw;
            }
            return vresult;
        }
    }

    public class RequestData {
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
    }
}
