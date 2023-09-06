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
        const string respExitosa = "00";
        const string _Urlprocesar_metodo_pago = "/vpos/metodo";

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
                    if (vResponse.codRespuesta == respExitosa) {
                        vExito = true;
                    } else {
                        throw new LibGalac.Aos.Catching.GalacAlertException(vResponse.mensajeRespuesta);
                        //LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vResponse.mensajeRespuesta);
                    }
                }
            } catch (System.Exception vEx) {
                //throw new LibGalac.Aos.Catching.GalacAlertException("No se recibió respuesta. Por favor valide su conexión a internet. ");
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Alert(null, vEx.Message, "Luis G.");
            }
            return vExito;
        }

        ProcesarMetodoPago.response SendProcesarMetodoDePago(ProcesarMetodoPago.request valrequestObject) {
            var requestJson = Serialize<ProcesarMetodoPago.request>(valrequestObject);
            var result = Post(_Urlprocesar_metodo_pago, requestJson);
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

    namespace ProcesarMetodoPago
    {
        public class request
        {
            public string accion { get; set; }
            public string montoTransaccion { get; set; }
            public string cedula { get; set; }
            public string tipoMoneda { get; set; }
        }

        public class response
        {
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

        //public class voucher
        //{
        //    public string linea { get; set; }
        //}

        //public static class Constantes
        //{
        //    public const string valido = "00";
        //    public const string invalido = "99";

        //}
    }
}
