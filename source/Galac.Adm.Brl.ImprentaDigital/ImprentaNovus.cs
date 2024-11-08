using System;
using System.Xml.Linq;
using Galac.Adm.Ccl.ImprentaDigital;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Inventario;
using System.Collections.Generic;
using Galac.Saw.LibWebConnector;
using Galac.Saw.Ccl.Cliente;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Cnf;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Galac.Adm.Brl.ImprentaDigital {
    public class ImprentaNovus : clsImprentaDigitalBase {

        string _NumeroFactura;
        eTipoDocumentoFactura _TipoDeDocumento;
        JObject vDocumentoDigital;
        string _TipoDeProveedor;
        clsConectorJson _ConectorJson;

        public ImprentaNovus(eTipoDocumentoFactura initTipoDeDocumento, string initNumeroFactura) : base(initTipoDeDocumento, initNumeroFactura) {
            _NumeroFactura = initNumeroFactura;
            _TipoDeDocumento = initTipoDeDocumento;
            _TipoDeProveedor = "";//NORMAL Según catalogo No 2 del layout
            _ConectorJson = new clsConectorJsonNovus(LoginUser);
        }
        #region Métodos Basicos
        public override bool SincronizarDocumento() {
            try {
                bool vResult = false;
                bool vDocumentoExiste = false;
                if (LibString.IsNullOrEmpty(EstatusDocumento)) {
                    vDocumentoExiste = EstadoDocumento();
                }
                if (vDocumentoExiste) { // Documento Existe en ID
                    vResult = base.SincronizarDocumento();
                } else if (LibString.S1IsEqualToS2(CodigoRespuesta, "203")) { // Documento No Existe en ID
                    vResult = EnviarDocumento();
                }
                return vResult;
            } catch (AggregateException gEx) {
                throw new GalacException(gEx.InnerException.Message, eExceptionManagementType.Controlled);
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public override bool EstadoLoteDocumentos() {
            try {
                bool vResult = false;
                string vMensaje = string.Empty;
                clsConectorJson vConectorJson = new clsConectorJsonNovus(LoginUser);
                bool vRepuestaConector = vConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Autenticacion));
                if (vRepuestaConector) {
                    string vDocumentoJSON = clsConectorJson.SerializeJSON(""); //Construir XML o JSON Con datos 
                    var vReq = vConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.EstadoLote), vConectorJson.Token);
                }
                return vResult;
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public override bool EstadoDocumento() {
            stPostResq vRespuestaConector = new stPostResq();
            string vMensaje = string.Empty;
            bool vChekConeccion;
            string vDocumentoJSON;
            try {
                string vSerie = LibAppSettings.ReadAppSettingsKey("SERIE");
                if (LibString.IsNullOrEmpty(_ConectorJson.Token)) {
                    ObtenerDatosDocumentoEmitido();
                    vChekConeccion = _ConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Autenticacion));
                } else {
                    vChekConeccion = true;
                }
                stSolicitudDeConsulta vJsonDeConsulta = new stSolicitudDeConsulta() {
                    Serie = vSerie,
                    TipoDocumento = LibConvert.ToStr(GetTipoDocumento(FacturaImprentaDigital.TipoDeDocumentoAsEnum)),
                    NumeroDocumento = NumeroFactura
                };
                if (vChekConeccion) {
                    vDocumentoJSON = clsConectorJson.SerializeJSON(vJsonDeConsulta);//Construir XML o JSON Con datos 
                    vRespuestaConector = _ConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.EstadoDocumento), _ConectorJson.Token, NumeroFactura, (int)TipoDeDocumento);
                    Mensaje = vRespuestaConector.mensaje;
                } else {
                    Mensaje = vMensaje;
                }
                return vRespuestaConector.Aprobado;
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            } finally {
                CodigoRespuesta = vRespuestaConector.codigo ?? string.Empty;
                EstatusDocumento = vRespuestaConector.estado.estadoDocumento ?? string.Empty;
                NumeroControl = vRespuestaConector.estado.numeroControl ?? string.Empty;
                FechaAsignacion = LibString.IsNullOrEmpty(vRespuestaConector.estado.fechaAsignacion) ? LibDate.MinDateForDB() : LibConvert.ToDate(vRespuestaConector.estado.fechaAsignacion);
                HoraAsignacion = vRespuestaConector.estado.horaAsignacion ?? string.Empty;
            }
        }

        public override bool AnularDocumento() {
            try {
                string vSerie = LibAppSettings.ReadAppSettingsKey("SERIE");
                bool vResult = false;
                stPostResq vRespuestaConector = new stPostResq();
                bool vDocumentoExiste = EstadoDocumento();
                if (LibString.IsNullOrEmpty(EstatusDocumento)) {
                    vDocumentoExiste = EstadoDocumento();
                }
                if (vDocumentoExiste) {
                    if (!LibString.S1IsEqualToS2(EstatusDocumento, "Anulada")) {
                        stSolicitudDeAccion vSolicitudDeAnulacion = new stSolicitudDeAccion() {
                            Serie = vSerie,
                            TipoDocumento = LibConvert.ToStr(GetTipoDocumento(FacturaImprentaDigital.TipoDeDocumentoAsEnum)),
                            NumeroDocumento = NumeroFactura,
                            MotivoAnulacion = FacturaImprentaDigital.MotivoDeAnulacion
                        };
                        string vDocumentoJSON = clsConectorJson.SerializeJSON(vSolicitudDeAnulacion); //Construir XML o JSON Con datos                         
                        vRespuestaConector = _ConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Anular), _ConectorJson.Token);
                        vResult = vRespuestaConector.Aprobado;
                        Mensaje = vRespuestaConector.mensaje;
                    } else {
                        Mensaje = $"No se pudo anular la {FacturaImprentaDigital.TipoDeDocumentoAsString} en la Imprenta Digital, debe sincronizar el documento.";
                    }
                } else {
                    if (!LibString.IsNullOrEmpty(Mensaje)) {
                        Mensaje = $"La {FacturaImprentaDigital.TipoDeDocumentoAsString} no pudo ser anulada.\r\n" + Mensaje;
                    }
                }
                return vResult;
            } catch (AggregateException gEx) {
                throw new GalacException(gEx.InnerException.Message, eExceptionManagementType.Controlled);
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public override bool EnviarDocumento() {
            try {
                bool vResult = false;
                string vMensaje = string.Empty;
                stPostResq vRespuestaConector;
                bool vChekConeccion;
                //if (LibString.IsNullOrEmpty(_ConectorJson.Token)) {
                //    vChekConeccion = _ConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Autenticacion));
                //} else {
                    vChekConeccion = true;
                //}
                if (vChekConeccion) {
                    ConfigurarDocumento();
                    string vDocumentoJSON = vDocumentoDigital.ToString();
                    vRespuestaConector = _ConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Emision), _ConectorJson.Token, NumeroFactura, (int)TipoDeDocumento);
                    NumeroControl = vRespuestaConector.resultados.numeroControl;
                    vResult = vRespuestaConector.Aprobado;
                    if (vResult) {
                        ActualizaNroControlYProveedorImprentaDigital();
                    } else {
                        Mensaje = vRespuestaConector.mensaje;
                    }
                } else {
                    Mensaje = vMensaje;
                }
                return vResult;
            } catch (AggregateException gEx) {
                throw new GalacException(gEx.InnerException.Message, eExceptionManagementType.Controlled);
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }
        #endregion Métodos Básicos
        #region Construcción de Documento
        #region Armar Documento Digital
        public override void ConfigurarDocumento() {
            base.ConfigurarDocumento();
            vDocumentoDigital = GetCuerpoDocumento();
            vDocumentoDigital.Add("cuerpofactura",GetDetalleFactura());
            vDocumentoDigital.Add("formasdepago",GetFormasPago());            
        }
        #endregion Construye  Documento
        #region Identificacion de Documento

        private string GeneraTrackingId() {
            int vSeed = DateTime.Now.Millisecond;
            int vNumberRnd = new Random(vSeed).Next(1, 1000000000);
            return LibText.FillWithCharToLeft(LibConvert.ToStr(vNumberRnd), "0", 10);
        }

        private JObject GetCuerpoDocumento() {
            JObject vJsonDoc = new JObject();
            string vTipoIdentficacion = string.Empty;
            string vNumeroRif = DarFormatoYObtenerTipoIdentficacion(ClienteImprentaDigital, ref vTipoIdentficacion);
            string vSerie = LibAppSettings.ReadAppSettingsKey("SERIE");
            string vHoraEmision = LibConvert.ToStrOnlyForHour(LibConvert.ToDate(FacturaImprentaDigital.HoraModificacion), "hh:mm:ss tt");
            vHoraEmision = LibString.Replace(vHoraEmision, ". ", "");
            vHoraEmision = LibString.Replace(vHoraEmision, "\u00A0", ""); // Caracter No imprimible que agrega el formato de hora de Windows para alguna config regional
            vHoraEmision = LibString.Replace(vHoraEmision, ".", "");
            vJsonDoc.Add("rif", LoginUser.User);
            vJsonDoc.Add("trackingid", GeneraTrackingId());
            vJsonDoc.Add("nombrecliente", ClienteImprentaDigital.Nombre);
            vJsonDoc.Add("rifcedulacliente", vNumeroRif);
            vJsonDoc.Add("emailcliente", ClienteImprentaDigital.Email);
            vJsonDoc.Add("idtipocedulacliente", vTipoIdentficacion);
            vJsonDoc.Add("direccioncliente", ClienteImprentaDigital.Direccion);
            vJsonDoc.Add("telefonocliente", ClienteImprentaDigital.Telefono);
            vJsonDoc.Add("idtipodocumento", GetTipoDocumento(_TipoDeDocumento));
            vJsonDoc.Add("subtotal", FacturaImprentaDigital.TotalRenglones);
            vJsonDoc.Add("exento", FacturaImprentaDigital.TotalMontoExento);
            vJsonDoc.Add("tasag", FacturaImprentaDigital.PorcentajeAlicuota1);
            vJsonDoc.Add("baseg", FacturaImprentaDigital.MontoGravableAlicuota1);
            vJsonDoc.Add("impuestog", FacturaImprentaDigital.MontoIvaAlicuota1);
            vJsonDoc.Add("tasaa", FacturaImprentaDigital.PorcentajeAlicuota2);
            vJsonDoc.Add("basea", FacturaImprentaDigital.MontoGravableAlicuota2);
            vJsonDoc.Add("impuestoa", FacturaImprentaDigital.MontoIvaAlicuota2);
            vJsonDoc.Add("tasar", FacturaImprentaDigital.PorcentajeAlicuota3);
            vJsonDoc.Add("baser", FacturaImprentaDigital.MontoGravableAlicuota3);
            vJsonDoc.Add("impuestor", FacturaImprentaDigital.MontoIvaAlicuota3);
            vJsonDoc.Add("tasaigtf", FacturaImprentaDigital.AlicuotaIGTF);
            vJsonDoc.Add("baseigtf", FacturaImprentaDigital.BaseImponibleIGTF);
            vJsonDoc.Add("impuestoigtf", FacturaImprentaDigital.IGTFML);
            vJsonDoc.Add("total", FacturaImprentaDigital.TotalFactura + FacturaImprentaDigital.IGTFML);
            if (_TipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito || _TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito) {
                vJsonDoc.Add("relacionado", FacturaImprentaDigital.NumeroFacturaAfectada);
            }
            vJsonDoc.Add("sendmail", "1"); // Siempre Envia Email
            vJsonDoc.Add("sucursal", ""); // No Aplica
            vJsonDoc.Add("numerointerno", FacturaImprentaDigital.Numero);
            vJsonDoc.Add("fecha_emision", FacturaImprentaDigital.Fecha);
            vJsonDoc.Add("moneda", FacturaImprentaDigital.CodigoMoneda == CodigoMonedaLocal ? "bs" : "usd");
            vJsonDoc.Add("tasacambio", CambioABolivares);
            vJsonDoc.Add("observacion", FacturaImprentaDigital.Observaciones);
            return vJsonDoc;
        }

        private string DarFormatoYObtenerTipoIdentficacion(Cliente valCliente, ref string refTipoIdentficacion) {
            string vPrefijo = LibString.Left(LibString.ToUpperWithoutAccents(valCliente.NumeroRIF), 1);
            string vNumeroRif = valCliente.NumeroRIF;
            if (LibString.Len(vNumeroRif) >= 10) {
                refTipoIdentficacion = "3"; // RIF
            } else {
                refTipoIdentficacion = "1"; // Cedula // Sistema no maneja Pasaporte como tal
            }
            if (LibString.S1IsInS2(vPrefijo, "VJEPG")) {
                vNumeroRif = LibString.Right(vNumeroRif, LibString.Len(vNumeroRif) - 1);
                vNumeroRif = LibString.Replace(vNumeroRif, "-", "");
            }
            return vNumeroRif;
        }
        #endregion Identificacion de Documento
        #region vendedor
        private XElement GetDatosVendedor() {
            XElement vResult = new XElement("vendedor",
                new XElement("codigo", VendedorImprentaDigital.Codigo),
                new XElement("nombre", VendedorImprentaDigital.Nombre),
                new XElement("numCajero", VendedorImprentaDigital.Codigo));
            return vResult;
        }
        #endregion vendedor     

        #region InfoAdicional
        private XElement GetDatosInfoAdicional() {
            string vTextoColetilla = "Este pago estará sujeto al cobro adicional del 3% del Impuesto a las Grandes Transacciones Financieras (IGTF), de conformidad con la Providencia Administrativa SNAT/2022/000013 publicada en la G.O.N 42.339 del 17-03- 2022, en caso de ser cancelado en divisas. Este impuesto no aplica en pago en Bs.";
            string vTextoColetilla2 = "En los casos en que la base imponible de la venta o prestación de servicio estuviere expresada en moneda extranjera, se establecerá la equivalencia en moneda nacional, al tipo de cambio corriente en el mercado del día en que ocurra el hecho imponible, salvo que éste ocurra en un día hábil para el sector financiero, en cuyo caso se aplicará el vigente en el día hábil inmediatamente siguiente al de la operación. (art 25 Ley de IVA G.O 6.152 de fecha 18/11/2014).";
            if (_TipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito) {
                vTextoColetilla = "";
            }

            string vObservaciones = string.Empty;
            if (FacturaImprentaDigital.TipoDeDocumentoAsEnum == eTipoDocumentoFactura.NotaDeCredito || FacturaImprentaDigital.TipoDeDocumentoAsEnum == eTipoDocumentoFactura.NotaDeDebito) {
                if (LibString.InStr(FacturaImprentaDigital.NumeroFacturaAfectada, "-") > 0) {
                    vObservaciones = "Nro.Fact.Afectada Original: " + FacturaImprentaDigital.NumeroFacturaAfectada + ". ";
                }
            }
            vObservaciones += FacturaImprentaDigital.Observaciones;

            XElement vVarios = new XElement("root",
                        new XElement("Atencion", ClienteImprentaDigital.Contacto),
                        new XElement("Ciudad", ClienteImprentaDigital.Ciudad),
                        new XElement("Envia", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Name
                        ));


            XElement vInfoAdicionalCliente = new XElement("root",
                        new XElement("Ciudad", InfoAdicionalClienteImprentaDigital.Ciudad),
                        new XElement("DireccionServicio", InfoAdicionalClienteImprentaDigital.Direccion));

            XElement vCampoPdf = new XElement("Campo", "PDF");

            var vColetilla1 = XmlToJsonWithoutRoot(new XElement("root", new XElement("Coletilla", vTextoColetilla)).ToString());
            var vColetilla2 = XmlToJsonWithoutRoot(new XElement("root", new XElement("Coletilla2", vTextoColetilla2)).ToString());
            var vColetilla3 = XmlToJsonWithoutRoot(new XElement("root", new XElement("Coletilla3", LibString.Left(vObservaciones, 250))).ToString());
            var vColetilla4 = XmlToJsonWithoutRoot(vVarios.ToString());
            var vColetilla5 = XmlToJsonWithoutRoot(vInfoAdicionalCliente.ToString());

            List<XElement> vInfoAdicional = new List<XElement>();
            vInfoAdicional.Add(new XElement("InfoAdicional", vCampoPdf, new XElement("Valor", vColetilla1)));
            vInfoAdicional.Add(new XElement("InfoAdicional", vCampoPdf, new XElement("Valor", vColetilla2)));
            vInfoAdicional.Add(new XElement("InfoAdicional", vCampoPdf, new XElement("Valor", vColetilla3)));
            vInfoAdicional.Add(new XElement("InfoAdicional", vCampoPdf, new XElement("Valor", vColetilla4)));
            vInfoAdicional.Add(new XElement("InfoAdicional", vCampoPdf, new XElement("Valor", vColetilla5)));

            XElement vResult = new XElement("InfoAdicional", vInfoAdicional);
            return vResult;
        }

        static string XmlToJsonWithoutRoot(string xml) {
            var doc = XDocument.Parse(xml);
            return JsonConvert.SerializeXNode(doc, Newtonsoft.Json.Formatting.None, omitRootObject: true);
        }
        #endregion InfoAdicional

        #region Impuestos
        private XElement GetTotalImpuestosML() {
            decimal vCambio = 0;
            XElement vResult = new XElement("impuestosSubtotal");
            if (LibString.S1IsInS2(FacturaImprentaDigital.CodigoMoneda, CodigoMonedaLocal)) {
                vCambio = 1;
            } else {
                vCambio = CambioABolivares;
            }
            vResult = new XElement("impuestosSubtotal",
               new XElement("impuestosSubtotal",
                   new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Exento)),
                   new XElement("AlicuotaImp", 0m),
                   new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalMontoExento * vCambio, 2))),
                   new XElement("ValorTotalImp", 0m)),
               new XElement("impuestosSubtotal",
                   new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.AlicuotaGeneral)),
                   new XElement("AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota1, 2)),
                   new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota1 * vCambio, 2))),
                   new XElement("ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota1 * vCambio, 2)))),
               new XElement("impuestosSubtotal",
                   new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Alicuota2)),
                   new XElement("AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota2, 2)),
                   new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota2 * vCambio, 2))),
                   new XElement("ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota2 * vCambio, 2)))),
               new XElement("impuestosSubtotal",
                   new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Alicuota3)),
                   new XElement("AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota3, 2)),
                   new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota3 * vCambio, 2))),
                   new XElement("ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota3 * vCambio, 2)))),
                 new XElement("impuestosSubtotal",
                   new XElement("CodigoTotalImp", "P"), // Percibido -> SAW No lo maneja pero JSON lo requiere
                   new XElement("AlicuotaImp", 0m),
                   new XElement("BaseImponibleImp", 0m),
                   new XElement("ValorTotalImp", 0m)),
               new XElement("impuestosSubtotal",
                   new XElement("CodigoTotalImp", "IGTF"),
                   new XElement("AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.AlicuotaIGTF, 2)),
                   new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.BaseImponibleIGTF * vCambio, 2))),
                   new XElement("ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.IGTFML, 2)))));
            return vResult;
        }

        private XElement GetTotalImpuestosME() {
            decimal vCambio = 0;
            XElement vResult = new XElement("impuestosSubtotal");
            if (LibString.S1IsInS2(FacturaImprentaDigital.CodigoMoneda, CodigoMonedaLocal)) {
                vCambio = CambioABolivares;
            } else {
                vCambio = 1;
            }
            vResult = new XElement("impuestosSubtotal",
               new XElement("impuestosSubtotal",
                   new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Exento)),
                   new XElement("AlicuotaImp", 0m),
                   new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalMontoExento / vCambio, 2))),
                   new XElement("ValorTotalImp", 0m)),
               new XElement("impuestosSubtotal",
                   new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.AlicuotaGeneral)),
                   new XElement("AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota1, 2)),
                   new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota1 / vCambio, 2))),
                   new XElement("ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota1 / vCambio, 2)))),
               new XElement("impuestosSubtotal",
                   new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Alicuota2)),
                   new XElement("AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota2, 2)),
                   new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota2 / vCambio, 2))),
                   new XElement("ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota2 / vCambio, 2)))),
               new XElement("impuestosSubtotal",
                   new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Alicuota3)),
                   new XElement("AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota3, 2)),
                   new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota3 / vCambio, 2))),
                   new XElement("ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota3 / vCambio, 2)))),
                 new XElement("impuestosSubtotal",
                   new XElement("CodigoTotalImp", "P"), // Percibido -> SAW No lo maneja pero JSON lo requiere
                   new XElement("AlicuotaImp", 0m),
                   new XElement("BaseImponibleImp", 0m),
                   new XElement("ValorTotalImp", 0m)),
               new XElement("impuestosSubtotal",
                   new XElement("CodigoTotalImp", "IGTF"),
                   new XElement("AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.AlicuotaIGTF, 2)),
                   new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.BaseImponibleIGTF / vCambio, 2))),
                   new XElement("ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.IGTFME, 2)))));
            return vResult;
        }


        #endregion Impuestos
        #region formas de pago
        private JArray GetFormasPago() {
            // Discutir Funcionalidad
            JArray vResult = new JArray();
            JObject vElement = new JObject();
            decimal vMonto;
            string vFormaDeCobro;
            if (FacturaImprentaDigital.BaseImponibleIGTF > 0) { // Pagos en ML
                vFormaDeCobro = LibEnumHelper.GetDescription(FacturaImprentaDigital.FormaDeCobroAsEnum); //  == eTipoDeFormaDeCobro.Efectivo ? "Contado" : "Crédito";                
                vMonto = LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalFactura + FacturaImprentaDigital.IGTFML, 2));
                vElement.Add("forma", vFormaDeCobro);
                vElement.Add("valor", vMonto);                
            } else {
                vFormaDeCobro = LibEnumHelper.GetDescription(FacturaImprentaDigital.FormaDeCobroAsEnum); // == eTipoDeFormaDeCobro.Efectivo ? "Contado" : "Crédito";                
                vMonto = LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalFactura, 2));
                vElement.Add("forma", vFormaDeCobro);
                vElement.Add("valor", vMonto);                
            }
            vResult.Add(vElement);
            return vResult;
        }
        #endregion formas de pago
        #region Totales
        private XElement GetTotales() { // Sección Es Fija Siempre en Bolívares
            // listaDescBonificacion -> otros cargos y descuentos se debe revisar.
            decimal vCambio = LibString.S1IsEqualToS2(FacturaImprentaDigital.CodigoMoneda, CodigoMonedaLocal) ? 1 : CambioABolivares;
            decimal vTotalFacturaMasIGTF = (FacturaImprentaDigital.TotalFactura * vCambio) + FacturaImprentaDigital.IGTFML;
            string vMontoEnLetrasME = MontoEnLetras(vTotalFacturaMasIGTF, FacturaImprentaDigital.CodigoMoneda);
            XElement vResult = new XElement("totales",
            new XElement("nroItems", DetalleFacturaImprentaDigital.Count),
            new XElement("montoGravadoTotal", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalBaseImponible * vCambio, 2))),
            new XElement("montoExentoTotal", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalMontoExento * vCambio, 2))),
            new XElement("subtotal", LibMath.Abs(LibMath.RoundToNDecimals((FacturaImprentaDigital.TotalBaseImponible + FacturaImprentaDigital.TotalMontoExento) * vCambio, 2))),
            new XElement("totalAPagar", LibMath.Abs(LibMath.RoundToNDecimals(vTotalFacturaMasIGTF, 2))),
            new XElement("totalIVA", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalIVA * vCambio, 2))),
            new XElement("montoTotalConIVA", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalFactura * vCambio, 2))),
            new XElement("montoEnLetras", vMontoEnLetrasME),
            new XElement("totalDescuento", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoDescuento1 * vCambio, 2))));
            vResult.Add(GetTotalImpuestosML().Descendants("impuestosSubtotal"));
            //vResult.Add(GetFormasPago().Descendants("formasPago"));
            return vResult;
        }

        private XElement GetTotalesME() { // Sección Es Fija Siempre en Otra Moneda                        
            decimal vCambio = LibString.S1IsEqualToS2(FacturaImprentaDigital.CodigoMoneda, CodigoMonedaLocal) ? CambioABolivares : 1;
            decimal vTotalFacturaMasIGTF = LibMath.RoundToNDecimals((FacturaImprentaDigital.TotalFactura / vCambio) + FacturaImprentaDigital.IGTFME, 2);
            string vCodigoMonedaLbl = LibString.S1IsEqualToS2(FacturaImprentaDigital.CodigoMoneda, CodigoMonedaLocal) ? CodigoMonedaME : FacturaImprentaDigital.CodigoMoneda;
            string vMontoEnLetrasME = MontoEnLetras(vTotalFacturaMasIGTF, "");
            XElement vResult = new XElement("TotalesOtraMoneda",
            new XElement("moneda", vCodigoMonedaLbl),
            new XElement("tipoCambio", LibMath.Abs(LibMath.RoundToNDecimals(CambioABolivares, 4))),
            new XElement("montoGravadoTotal", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalBaseImponible / vCambio, 2))),
            new XElement("montoExentoTotal", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalMontoExento / vCambio, 2))),
            new XElement("subtotal", LibMath.Abs(LibMath.RoundToNDecimals((FacturaImprentaDigital.TotalBaseImponible + FacturaImprentaDigital.TotalMontoExento) / vCambio, 2))),
            new XElement("totalAPagar", LibMath.Abs(LibMath.RoundToNDecimals(vTotalFacturaMasIGTF, 2))),
            new XElement("totalIVA", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalIVA / vCambio, 2))),
            new XElement("montoTotalConIVA", LibMath.Abs(LibMath.RoundToNDecimals((FacturaImprentaDigital.TotalFactura) / vCambio, 2))),
            new XElement("montoEnLetras", vMontoEnLetrasME),
            new XElement("totalDescuento", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoDescuento1 / vCambio, 2))));
            vResult.Add(GetTotalImpuestosME().Descendants("impuestosSubtotal"));
            return vResult;
        }

        private string MontoEnLetras(decimal valMonto, string valCodigoMoneda) {
            string vResult;
            valMonto = LibMath.Abs(valMonto);
            if (valMonto < 1 && valMonto > 0) {//0.xx
                string vMontoStr = LibConvert.ToStr(valMonto);
                string vCentimos = LibText.Mid(vMontoStr, 2);
                vResult = "Cero con " + vCentimos + "/100";
            } else {
                vResult = LibConvert.ToNumberInLetters(valMonto, false, "");
            }
            if (LibText.Len(valCodigoMoneda, true) > 0) {
                vResult += " " + valCodigoMoneda;
            }
            return vResult;
        }

        #endregion Totales
        #region Detalle RenglonFactura
        public JArray GetDetalleFactura() {
            // Pendiente : unidades de medida del item
            // Revisar otros cargos y descuentos, revisar y Serial Rollo  
            JArray vDetalleFactura = new JArray();
            if (DetalleFacturaImprentaDigital != null) {
                if (DetalleFacturaImprentaDigital.Count > 0) {
                    foreach (FacturaRapidaDetalle vDetalle in DetalleFacturaImprentaDigital) {
                        JObject vElement = new JObject();
                        string vSerial = LibString.S1IsEqualToS2(vDetalle.Serial, "0") ? "" : vDetalle.Serial;
                        string vRollo = LibString.S1IsEqualToS2(vDetalle.Rollo, "0") ? "" : vDetalle.Rollo;
                        string vInfoAdicional = string.Empty;
                        if (!LibString.IsNullOrEmpty(vSerial)) {
                            vInfoAdicional = "Serial:" + vSerial;
                        }
                        if (!LibString.IsNullOrEmpty(vRollo)) {
                            vInfoAdicional = vInfoAdicional + " Rollo:" + vRollo;
                        }
                        decimal vCantidad = (TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito && FacturaImprentaDigital.GeneradoPorAsEnum == eFacturaGeneradaPor.AjusteIGTF && vDetalle.Cantidad == 0) ? 1 : vDetalle.Cantidad;
                        vElement.Add("codigo", vDetalle.Articulo);
                        vElement.Add("descripcion", vDetalle.Descripcion);
                        vElement.Add("comentario", vInfoAdicional);
                        vElement.Add("precio", vDetalle.PrecioSinIVA);
                        vElement.Add("cantidad", vCantidad);
                        vElement.Add("tasa", vDetalle.PorcentajeAlicuota);
                        vElement.Add("impuesto", LibMath.RoundToNDecimals((vDetalle.PorcentajeAlicuota * vDetalle.PrecioSinIVA * vDetalle.Cantidad) / 100, 2));
                        decimal valorDescuento = LibMath.RoundToNDecimals((vDetalle.PorcentajeDescuento * vDetalle.PrecioSinIVA * vDetalle.Cantidad) / 100, 2);
                        vElement.Add("descuento", valorDescuento);
                        vElement.Add("exento", vDetalle.AlicuotaIvaAsEnum == eTipoDeAlicuota.Exento ? true : false);
                        decimal vMontoItem = LibMath.RoundToNDecimals(vDetalle.PrecioSinIVA * vDetalle.Cantidad, 2) - valorDescuento;
                        vElement.Add("monto", vMontoItem);
                        decimal vIva = LibMath.RoundToNDecimals(vMontoItem * vDetalle.PorcentajeAlicuota / 100, 2);
                        vElement.Add("iva", vIva);
                        vElement.Add("monto_neto", vMontoItem + vIva);
                        vDetalleFactura.Add(vElement);
                    }
                }
            }
            //
            return vDetalleFactura;
        }
        #endregion Detalle RenglonFactura      
        #endregion
        #region Conversion de Tipos

        private string GetAlicuota(eTipoDeAlicuota valAlicuotaEnum) {
            Dictionary<eTipoDeAlicuota, string> vAlicuota = new Dictionary<eTipoDeAlicuota, string>();
            vAlicuota.Add(eTipoDeAlicuota.AlicuotaGeneral, "G");
            vAlicuota.Add(eTipoDeAlicuota.Alicuota2, "R");
            vAlicuota.Add(eTipoDeAlicuota.Alicuota3, "A");
            vAlicuota.Add(eTipoDeAlicuota.Exento, "E");
            return vAlicuota[valAlicuotaEnum];
        }
        private string GetFormaDeCobro(eTipoDeFormaDeCobro valFormaDeCobro) {
            string vResult = string.Empty;
            switch (valFormaDeCobro) {
                case eTipoDeFormaDeCobro.Efectivo:
                    vResult = "08";
                    break;
                case eTipoDeFormaDeCobro.TarjetaDebito:
                    vResult = "05";
                    break;
                case eTipoDeFormaDeCobro.TarjetaCredito:
                case eTipoDeFormaDeCobro.Cheque:
                case eTipoDeFormaDeCobro.Otros:
                    vResult = "08";
                    break;
            }
            return vResult;
        }
        private string GetTipoTransaccion(eTipoDeTransaccionDeLibrosFiscales valTipoTransaccion) {
            string vResult = "";
            switch (valTipoTransaccion) {
                case eTipoDeTransaccionDeLibrosFiscales.Registro:
                    vResult = "01";
                    break;
                case eTipoDeTransaccionDeLibrosFiscales.Anulacion:
                    vResult = "02";
                    break;
                case eTipoDeTransaccionDeLibrosFiscales.Complemento:
                    vResult = "03";
                    break;
                case eTipoDeTransaccionDeLibrosFiscales.Ajuste:
                    vResult = "04";
                    break;
                default:
                    vResult = "01";
                    break;
            }
            return vResult;
        }

        private int GetTipoDocumento(eTipoDocumentoFactura valTipoDocumento) {
            int vResult = 1;
            switch (valTipoDocumento) {
                case eTipoDocumentoFactura.Factura:
                    vResult = 1;
                    break;
                case eTipoDocumentoFactura.NotaDeCredito:
                    vResult = 3;
                    break;
                case eTipoDocumentoFactura.NotaDeDebito:
                    vResult = 2;
                    break;
                case eTipoDocumentoFactura.NotaEntrega:
                    vResult = 4;
                    break;
                default:
                    vResult = 1;
                    break;
            }
            return vResult;
        }

        private string GetTipoDePago(eFormadePago valTipoDePago) {
            string vResult = "";
            switch (valTipoDePago) {
                case eFormadePago.Contado:
                    vResult = "Contado";
                    break;
                case eFormadePago.Credito:
                    vResult = "Crédito";
                    break;
                default:
                    vResult = "Contado";
                    break;
            }
            return vResult;
        }
        #endregion Conversion de Tipos
    }
}


