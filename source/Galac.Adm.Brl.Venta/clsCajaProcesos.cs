using LibGalac.Aos.Base;
using Newtonsoft.Json;
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


        public bool SendMFiscalHomologada(string valFabricante, string valModelo) {
            bool vresult = false;
            string url = UrlApiMFiscalPlataformaQA;
            var requestData = new RequestData {
                Fabricante = valFabricante,
                Modelo = valModelo
            };
             string json = "{\"Modelo\":\"" + valModelo + "\",\"Fabricante\":\"" + valFabricante + "\"}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = json.Length;

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream())) {
                writer.Write(json);
            }

            try {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
                        string result = reader.ReadToEnd();
                        vresult = LibConvert.ToBool(result);
                       // Console.WriteLine("Response: " + result);
                    }
                }
            } catch (WebException ex) {
                using (HttpWebResponse response = (HttpWebResponse)ex.Response) {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
                        string error = reader.ReadToEnd();
                        vresult = false;
                        //Console.WriteLine("Error: " + error);
                    }
                }
            }
            return vresult;
        }
    }

    public class RequestData {
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
    }
}
