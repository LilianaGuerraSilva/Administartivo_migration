using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Adm.IntegracionMS.Venta {
    public class C2PMegasoftNav {
        string _UrlBase;
        const string _Urlpreregistr = "payment/action/v2-preregistr";
        const string _Urlprocesar_cambio_pagomovil = "/payment/action/v2-procesar-cambio-pagomovil";
        const string _Urlquerystatus = "/payment/action/v2-querystatus";
        public C2PMegasoftNav() {        
            _UrlBase = "http://payment.somee.com";
        }


        public Preregister.response ExecutePreregister(Preregister.request valrequestObject) {
            var requestXml = Serialize<Preregister.request>(valrequestObject);
            var result = Post(_Urlpreregistr, requestXml);
            if (!string.IsNullOrEmpty(result)) {
                return Deserialize<Preregister.response>(result);
            }
            return null;
        }

        public ProcesarCambioPagoMovil.response ExecuteProcesarCambioPagoMovil(ProcesarCambioPagoMovil.request valrequestObject) {
            var requestXml = Serialize<ProcesarCambioPagoMovil.request>(valrequestObject);
            var result = Post(_Urlprocesar_cambio_pagomovil, requestXml);
            if (!string.IsNullOrEmpty(result)) {
                return Deserialize<ProcesarCambioPagoMovil.response>(result);
            }
            return null;
        }

        public Querystatus.response ExecuteQuerystatus(Querystatus.request valrequestObject) {
            var requestXml = Serialize<Querystatus.request>(valrequestObject);
            var result = Post(_Urlquerystatus, requestXml);
            if (!string.IsNullOrEmpty(result)) {
                return Deserialize<Querystatus.response>(result);
            }
            return null;
        }

        string Serialize<T>(T obj) {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var sw = new StringWriter()) {
                using (XmlTextWriter writer = new XmlTextWriter(sw) { Formatting = Formatting.Indented }) {
                    serializer.Serialize(writer, obj);
                    return sw.ToString();
                }
            }
        }

        T Deserialize<T>(string xml) {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            TextReader sr = new StringReader(xml);
            return (T)serializer.Deserialize(sr);

        }

        private string Post(string Url, string requestXml) {
            Uri baseUri = new Uri(_UrlBase);
            var request = (HttpWebRequest)WebRequest.Create(new Uri(baseUri, Url));
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "application/xml";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK) {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }
    }
}
