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
        const string MonedaBs = "VES";
        const string respAprobada = "00";
        const string _Urlprocesar_metodo_pago = "/vpos/metodo";
        public string infoAdicional;
        public string numeroReferencia;
        public string bancoTransaccion;

        public C2PMegasoftNav() {                    
            string vResult = LeerConfigKey("UrlVPOSLocal");
            _UrlBase = string.IsNullOrEmpty(vResult) ? "http://localhost:8085" : vResult;
        }

        public bool EjecutaProcesarCambioPagoMovil(string valCedula, string valVuelto) {            
            request request = new request() {
                accion = "cambio",
                montoTransaccion = valVuelto,
                cedula = valCedula,
                tipoMoneda = MonedaBs
            };
            var vExito = SendProcesar(request);
            if (vExito.Item1) {
                //vExito.Item2;
                // Procesar acá los valores que se van a enviar a la aplicación {Saw / G360}
            }
            return vExito.Item1;
        }
        public bool EjecutaProcesarTarjeta(string valCedula, string valMonto) {
            request request = new request() {
                accion = "tarjeta",
                montoTransaccion = valMonto,
                cedula = valCedula
            };
            var vExito = SendProcesar(request);
            if (vExito.Item1) {
                //vExito.Item2;
                // Procesar acá los valores que se van a enviar a la aplicación {Saw / G360}
            }
            return vExito.Item1;
        }

        public bool EjecutaProcesarZelle(string valCedula, string valMonto) {
                request request = new request() {
                    accion = "tarjeta",
                    montoTransaccion = valMonto,
                    cedula = valCedula
                };
            var vExito = SendProcesar(request);
            if (vExito.Item1) {
                //vExito.Item2;
                // Procesar acá los valores que se van a enviar a la aplicación {Saw / G360}
            }
            return vExito.Item1;
        }

        public bool EjecutaProcesarCompraP2C(string valCedula, string valMonto) {
            request request = new request() {
                accion = "tarjeta",
                montoTransaccion = valMonto,
                cedula = valCedula
            };
            var vExito = SendProcesar(request);
            if (vExito.Item1) {
                //vExito.Item2;
                // Procesar acá los valores que se van a enviar a la aplicación {Saw / G360}
            }
            return vExito.Item1;
        }

        public bool EjecutaProcesarCompraBiopago(string valCedula, string valMonto) {
            request request = new request() {
                accion = "tarjeta",
                montoTransaccion = valMonto,
                cedula = valCedula
            };
            var vExito = SendProcesar(request);
            if (vExito.Item1) {
                //vExito.Item2;
                // Procesar acá los valores que se van a enviar a la aplicación {Saw / G360}
            }
            return vExito.Item1;
        }

        public bool EjecutaUltimoVoucherAprobado() {
            request request = new request() {
                accion = "imprimeUltimoVoucher"
            };
            var vExito = SendProcesar(request);
            if (vExito.Item1) {
                //vExito.Item2;
                // Procesar acá los valores que se van a enviar a la aplicación {Saw / G360}
            }
            return vExito.Item1;
        }

        public bool EjecutaUltimoVoucherProcesado() {
            request request = new request() {
                accion = "imprimeUltimoVoucherP"
            };
            var vExito = SendProcesar(request);
            if (vExito.Item1) {
                //vExito.Item2;
                // Procesar acá los valores que se van a enviar a la aplicación {Saw / G360}
            }
            return vExito.Item1;
        }

        public bool EjecutaPrecierre() {
            request request = new request() {
                accion = "precierre"
            };
            var vExito = SendProcesar(request);
            if (vExito.Item1) {
                //vExito.Item2;
                // Procesar acá los valores que se van a enviar a la aplicación {Saw / G360}
            }
            return vExito.Item1;
        }

        public bool EjecutaCierre() {
            request request = new request() {
                accion = "cierre"
            };
            var vExito = SendProcesar(request);
            if (vExito.Item1) {
                //vExito.Item2;
                // Procesar acá los valores que se van a enviar a la aplicación {Saw / G360}
            }
            return vExito.Item1;
        }

        public bool EjecutaUltimoCierre() {
            request request = new request() {
                accion = "ultimoCierre"
            };
            var vExito = SendProcesar(request);
            if (vExito.Item1) {
                //vExito.Item2;
                // Procesar acá los valores que se van a enviar a la aplicación {Saw / G360}
            }
            return vExito.Item1;
        }

        private Tuple<bool, response> SendProcesar(request request) {
            try {
                bool vExito = false;
                var requestJson = Serialize<request>(request);
                var vResponse = Post(_Urlprocesar_metodo_pago, requestJson);
                if (vResponse != null) {
                    if (vResponse.codRespuesta == respAprobada) {
                        infoAdicional = LibFile.FileNameOf(vResponse.nombreVoucher);
                        numeroReferencia = vResponse.numeroReferencia;
                        vExito = true;
                    } else {
                        throw new LibGalac.Aos.Catching.GalacAlertException(vResponse.mensajeRespuesta);
                    }
                }
                return new Tuple<bool, response>(vExito, vResponse);
            } catch (System.Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacAlertException("Conexión con plataforma de pagos");
            }
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
            return JsonConvert.SerializeObject(obj);
        }
        T Deserialize<T>(string json) {
            return JsonConvert.DeserializeObject<T>(json);  
        }

        public static string LeerConfigKey(string valKey) {
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
    }

    

    
        public class request {
            public string accion { get; set; }
            public string montoTransaccion { get; set; }
            public string cedula { get; set; }
            public string tipoMoneda { get; set; }
        }

        public class response {
            public string codRespuesta { get; set; }
            public string mensajeRespuesta { get; set; }
            public string nombreVoucher { get; set; }
            public int numSeq { get; set; }
            public string numeroTarjeta { get; set; }
            public string cedula { get; set; }
            public string montoTransaccion { get; set; }
            public string montoAvance { get; set; }
            public string montoServicios { get; set; }
            public string montoServiciosAprobado { get; set; }
            public string montoDonativo { get; set; }
            public string tipoCuenta { get; set; }
            public string tipoTarjeta { get; set; }
            public string fechaExpiracion { get; set; }
            public bool existeCopiaVoucher { get; set; }
            public string fechaTransaccion { get; set; }
            public string horaTransaccion { get; set; }
            public string terminalVirtual { get; set; }
            public string tipoTransaccion { get; set; }
            public string numeroAutorizacion { get; set; }
            public string codigoAfiliacion { get; set; }
            public string tid { get; set; }
            public string numeroReferencia { get; set; }
            public string nombreAutorizador { get; set; }
            public string codigoAdquiriente { get; set; }
            public string numeroLote { get; set; }
            public string tipoProducto { get; set; }
            public string bancoEmisorCheque { get; set; }
            public string numeroCuenta { get; set; }
            public string numeroCheque { get; set; }
            public string tipoMonedaFiat { get; set; }
            public string descrMonedaFiat { get; set; }
            public string montoCriptomoneda { get; set; }
            public string tipoCriptomoneda { get; set; }
            public string descrCriptomoneda { get; set; }
            public string tipoMoneda { get; set; }
            public string montoDivisa { get; set; }
            public string descrMoneda { get; set; }
            public string archivoCierre { get; set; }
            public bool flagImpresion { get; set; }
            public int medioPago { get; set; }
            public string voucherServicios { get; set; }
            public object listaTransServiciosOLB { get; set; }
        }    
}
