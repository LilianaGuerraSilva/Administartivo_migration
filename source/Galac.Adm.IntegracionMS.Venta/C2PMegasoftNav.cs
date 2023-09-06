using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm.Messaging;
using Newtonsoft.Json;

namespace Galac.Adm.IntegracionMS.Venta {
    public class C2PMegasoftNav {
        string _UrlBase;
        //const string _Urlpreregistr = "payment/action/v2-preregistr";
        //const string _Urlprocesar_cambio_pagomovil = "/payment/action/v2-procesar-cambio-pagomovil";
        //const string _Urlquerystatus = "/payment/action/v2-querystatus";

        const string _Urlprocesar_cambio_pagomovil = "/vpos/metodo";

        public C2PMegasoftNav() {        
            //_UrlBase = "http://payment.somee.com";
            _UrlBase = "http://localhost:8085";
        }

        public bool EjecutaProcesarCambioPagoMovil(string valCedula, string valVuelto) {
            const string MonedaBs = "VES";
            bool vExito = false;
            try {
                ProcesarMetodoPago.request request2 = new ProcesarMetodoPago.request() {
                    accion = "cambio",
                    montoTransaccion = valVuelto,
                    cedula = valCedula,
                    tipoMoneda = MonedaBs
                };
                ProcesarMetodoPago.response vResponse = SendProcesarMetodoDePago(request2);
                if (vResponse != null) {
                    if (vResponse.codRespuesta == ProcesarCambioPagoMovil.Constantes.valido) {
                        vExito = true;
                    } else {
                        throw new LibGalac.Aos.Catching.GalacAlertException(vResponse.mensajeRespuesta);
                    }
                }
            } catch {
                throw new LibGalac.Aos.Catching.GalacAlertException("No se recibió respuesta. Por favor valide su conexión a internet. ");
            }
            return vExito;
        }

        ProcesarMetodoPago.response SendProcesarMetodoDePago(ProcesarMetodoPago.request valrequestObject) {
            var requestJson = Serialize<ProcesarMetodoPago.request>(valrequestObject);
            var result = Post(_Urlprocesar_cambio_pagomovil, requestJson);
            if (!string.IsNullOrEmpty(result)) {
                return Deserialize<ProcesarMetodoPago.response>(result);
            }
            return null;
        }
        string Post(string Url, string requestjson) {
            Uri baseUri = new Uri(_UrlBase);
            var request = (HttpWebRequest)WebRequest.Create(new Uri(baseUri, Url));            
            request.ContentType = "application/json";
            request.Method = "POST";
            using (var streamWriter = new StreamWriter(request.GetRequestStream())) {
                streamWriter.Write(requestjson);
            }
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK) {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }

        string Serialize<T>(T obj) {
            return JsonConvert.SerializeObject(obj);
        }

        T Deserialize<T>(string json) {
            return JsonConvert.DeserializeObject<T>(json);  
        }

        public static bool EsVisiblePM() {
            return false;
        }
    }
}
