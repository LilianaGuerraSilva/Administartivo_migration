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
using LibGalac.Aos.Cnf;

namespace Galac.Adm.IntegracionMS.Venta {
    public class C2PMegasoftNav {
        string _UrlBase;
        const string _Urlprocesar_metodo_pago = "/vpos/metodo";
        public string infoAdicional;
        public string numeroReferencia;
        public string bancoTransaccion;
        public decimal montoTransaccion;
        public string numeroAutorizacion;

        public C2PMegasoftNav() {
            string vResult = LeerConfigKey("UrlVPOSLocal");
            _UrlBase = string.IsNullOrEmpty(vResult) ? "http://localhost:8085" : vResult;
        }

        public bool EjecutaProcesarCambioPagoMovil(string valCedula, decimal valVuelto) {            
            request request = new request() {
                accion = "cambio",
                montoTransaccion = valVuelto,
                cedula = valCedula,
                tipoMoneda = Constantes.MonedaBs
            };
            var vExito = SendProcesar(request);
            return vExito.Item1;
        }
        public bool EjecutaProcesarTarjeta(string valCedula, decimal valMonto) {
            request request = new request() {
                accion = "tarjeta",
                montoTransaccion = valMonto,
                cedula = valCedula
            };
            var vExito = SendProcesar(request);
            return vExito.Item1;
        }

        public bool EjecutaProcesarZelle(string valCedula, decimal valMonto) {
                request request = new request() {
                    accion = "tarjeta",
                    montoTransaccion = valMonto,
                    cedula = valCedula
                };
            var vExito = SendProcesar(request);
            return vExito.Item1;
        }

        public bool EjecutaProcesarCompraP2C(string valCedula, decimal valMonto) {
            request request = new request() {
                accion = "tarjeta",
                montoTransaccion = valMonto,
                cedula = valCedula
            };
            var vExito = SendProcesar(request);
            return vExito.Item1;
        }

        public bool EjecutaProcesarCompraBiopago(string valCedula, decimal valMonto) {
            request request = new request() {
                accion = "tarjeta",
                montoTransaccion = valMonto,
                cedula = valCedula
            };
            var vExito = SendProcesar(request);
            return vExito.Item1;
        }
        public bool EjecutaAnularTransaccion() {
            request  request = new request() {
                accion = "anulacion"
            };
            var vExito = SendProcesar(request);
            return vExito.Item1;
        }
        public bool EjecutaUltimoVoucherAprobado() {
            request request = new request() {
                accion = "imprimeUltimoVoucher"
            };
            var vExito = SendProcesar(request);
            return vExito.Item1;
        }

        public bool EjecutaUltimoVoucherProcesado() {
            request request = new request() {
                accion = "imprimeUltimoVoucherP"
            };
            var vExito = SendProcesar(request);
            return vExito.Item1;
        }

        public bool EjecutaPrecierre() {
            request request = new request() {
                accion = "precierre"
            };
            var vExito = SendProcesar(request);
            return vExito.Item1;
        }

        public bool EjecutaCierre() {
            request request = new request() {
                accion = "cierre"
            };
            var vExito = SendProcesar(request);
            return vExito.Item1;
        }

        public bool EjecutaUltimoCierre() {
            request request = new request() {
                accion = "ultimoCierre"
            };
            var vExito = SendProcesar(request);
            return vExito.Item1;
        }

        private Tuple<bool, response> SendProcesar(request request) {

            bool vExito = false;
            var requestJson = Serialize<request>(request);
            var vResponse = Post(_Urlprocesar_metodo_pago, requestJson);
            if (vResponse != null) {
                if (vResponse.codRespuesta == Constantes.valido) {
                    infoAdicional = LibFile.FileNameOf(vResponse.nombreVoucher);
                    numeroReferencia = vResponse.numeroReferencia;
                    numeroAutorizacion = vResponse.numeroAutorizacion;
                    montoTransaccion = LibImportData.ToDec(vResponse.montoTransaccion, 2) * 0.01m;
                    vExito = true;
                } else {
                    throw new LibGalac.Aos.Catching.GalacAlertException(vResponse.mensajeRespuesta);
                }
            }
            return new Tuple<bool, response>(vExito, vResponse);
        }

        response Post(string Url, string requestjson) {
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
                if (!string.IsNullOrEmpty(responseStr)) {
                    return Deserialize<response>(responseStr);
                }
            }
            return null;
        }
        string Serialize<T>(T obj) {
            return JsonConvert.SerializeObject(obj, new CustomDecimalJsonConverter());
        }
        T Deserialize<T>(string json) {
            return JsonConvert.DeserializeObject<T>(json);  
        }

        string LeerConfigKey(string valKey) {
            string vResult = string.Empty;
            try {
                if (!string.IsNullOrEmpty(LibAppSettings.ReadAppSettingsKey(valKey))) {
                    vResult = LibAppSettings.ReadAppSettingsKey(valKey) ?? string.Empty;
                }
            } catch (Exception) {
            }
            return vResult;
        }
        public static bool EsVisiblePM() {
            return true;
        }

        public static bool EsVisibleTDDTDC() {
            return true;
        }
    }  
}
