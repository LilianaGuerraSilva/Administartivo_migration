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

namespace Galac.Adm.IntegracionMS.Venta {
    public class C2PMegasoftNav {
        string _UrlBase;
        const string _Urlpreregistr = "payment/action/v2-preregistr";
        const string _Urlprocesar_cambio_pagomovil = "/payment/action/v2-procesar-cambio-pagomovil";
        const string _Urlquerystatus = "/payment/action/v2-querystatus";
        public C2PMegasoftNav() {        
            _UrlBase = "http://payment.somee.com";
        }

        internal LibResponse EjecutaCambioPagoMovil(string valCedula, string valTelefono, string valCodigoBanco, string valVuelto, string valNroFactura, ref string refNumeroControl) {
            LibResponse vResult = new LibResponse();
            try {
                string valCodigoAfiliacion = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoAfiliacionC2PMegasoft");
                refNumeroControl = EjecutaPreRegistro(valCodigoAfiliacion);
                if (!LibString.IsNullOrEmpty(refNumeroControl)) {
                    vResult.Success  = EjecutaProcesarCambio(valCodigoAfiliacion, refNumeroControl, valCedula, valTelefono, valCodigoBanco, valVuelto, valNroFactura);
                }
            } catch (Exception ex) {
                throw ex;
            }
            return vResult;
        }

        string EjecutaPreRegistro(string valCodAfiliacion) {
            string vResult;
            try {
                Preregister.request request = new Preregister.request() { cod_afiliacion = valCodAfiliacion };
                Preregister.response vResponse = SendPreregister(request);
                if (vResponse != null) {
                    if (vResponse.codigo == Preregister.Constantes.valido) {
                        vResult = vResponse.control;
                    } else if (vResponse.codigo == Preregister.Constantes.invalido) {
                        throw new LibGalac.Aos.Catching.GalacAlertException(vResponse.descripcion);
                    } else {
                        throw new LibGalac.Aos.Catching.GalacAlertException("No fue posible establecer conexión con el sistema de pago. Por favor intente nuevamente.");
                    }
                } else {
                    throw new LibGalac.Aos.Catching.GalacAlertException("No fue posible establecer conexión con el sistema de pago. Por favor intente nuevamente.");
                }
            } catch {
                throw new LibGalac.Aos.Catching.GalacAlertException("No fue posible establecer conexión con el sistema de pago. Por favor intente nuevamente.");
            }
            return vResult;
        }

        bool EjecutaProcesarCambio(string valCodAfiliacion, string valCodigoControl, string valCedula, string valTelefono, string valCodigoBanco, string valVuelto, string valNroFactura) {
            const string MonedaBs = "0";
            bool vExito = false;
            try {
                ProcesarCambioPagoMovil.request request2 = new ProcesarCambioPagoMovil.request() {
                    cod_afiliacion = valCodAfiliacion,
                    control = valCodigoControl,
                    cid = valCedula,
                    telefono = valTelefono,
                    codigobanco = valCodigoBanco,
                    tipo_moneda = MonedaBs,
                    amount = valVuelto,
                    factura = valNroFactura
                };
                ProcesarCambioPagoMovil.response vResponse = SendProcesarCambioPagoMovil(request2);
                if (vResponse != null) {
                    if (vResponse.codigo == ProcesarCambioPagoMovil.Constantes.valido) {
                        vExito = true;
                    } else {
                        throw new LibGalac.Aos.Catching.GalacAlertException(vResponse.descripcion);
                    }
                }
            } catch {
                throw new LibGalac.Aos.Catching.GalacAlertException("No se recibió respuesta. Por favor valide. Número de Control: " + valCodigoControl);
            }
            return vExito;

        }

        Preregister.response SendPreregister(Preregister.request valrequestObject) {
            var requestXml = Serialize<Preregister.request>(valrequestObject);
            var result = Post(_Urlpreregistr, requestXml);
            if (!string.IsNullOrEmpty(result)) {
                return Deserialize<Preregister.response>(result);
            }
            return null;
        }

        ProcesarCambioPagoMovil.response SendProcesarCambioPagoMovil(ProcesarCambioPagoMovil.request valrequestObject) {
            var requestXml = Serialize<ProcesarCambioPagoMovil.request>(valrequestObject);
            var result = Post(_Urlprocesar_cambio_pagomovil, requestXml);
            if (!string.IsNullOrEmpty(result)) {
                return Deserialize<ProcesarCambioPagoMovil.response>(result);
            }
            return null;
        }

        Querystatus.response SendQuerystatus(Querystatus.request valrequestObject) {
            var requestXml = Serialize<Querystatus.request>(valrequestObject);
            var result = Post(_Urlquerystatus, requestXml);
            if (!string.IsNullOrEmpty(result)) {
                return Deserialize<Querystatus.response>(result);
            }
            return null;
        }

        string Post(string Url, string requestXml) {
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

    }
}
