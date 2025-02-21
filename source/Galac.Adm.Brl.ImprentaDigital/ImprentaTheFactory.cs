using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

namespace Galac.Adm.Brl.ImprentaDigital {
    public class ImprentaTheFactory : clsImprentaDigitalBase {

        string _NumeroFactura;
        eTipoDocumentoFactura _TipoDeDocumento;
        XElement vDocumentoDigital;
        string _TipoDeProveedor;
        clsConectorJson _ConectorJson;

        public ImprentaTheFactory(eTipoDocumentoFactura initTipoDeDocumento, string initNumeroFactura) : base(initTipoDeDocumento, initNumeroFactura) {
            _NumeroFactura = initNumeroFactura;
            _TipoDeDocumento = initTipoDeDocumento;
            _TipoDeProveedor = "";//NORMAL Según catalogo No 2 del layout
            _ConectorJson = new clsConectorJson(LoginUser);
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
                clsConectorJson vConectorJson = new clsConectorJson(LoginUser);
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
                if (LibString.IsNullOrEmpty(_ConectorJson.Token)) {
                    ObtenerDatosDocumentoEmitido();
                    vChekConeccion = _ConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Autenticacion));
                } else {
                    vChekConeccion = true;
                }
                stSolicitudDeConsulta vJsonDeConsulta = new stSolicitudDeConsulta() {
                    Serie = SerieDocumento(),
                    TipoDocumento = GetTipoDocumento(FacturaImprentaDigital.TipoDeDocumentoAsEnum),
                    NumeroDocumento = NumeroDocumento()
                };
                if (vChekConeccion) {
                    vDocumentoJSON = clsConectorJson.SerializeJSON(vJsonDeConsulta);//Construir XML o JSON Con datos 
                    vRespuestaConector = _ConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.EstadoDocumento), _ConectorJson.Token, NumeroDocumento(), (int)TipoDeDocumento);
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
                bool vResult = false;
                stPostResq vRespuestaConector = new stPostResq();
                bool vDocumentoExiste = EstadoDocumento();
                if (LibString.IsNullOrEmpty(EstatusDocumento)) {
                    vDocumentoExiste = EstadoDocumento();
                }
                if (vDocumentoExiste) {
                    if (!LibString.S1IsEqualToS2(EstatusDocumento, "Anulada")) {
                        stSolicitudDeAccion vSolicitudDeAnulacion = new stSolicitudDeAccion() {
                            Serie = SerieDocumento(),
                            TipoDocumento = GetTipoDocumento(FacturaImprentaDigital.TipoDeDocumentoAsEnum),
                            NumeroDocumento = NumeroDocumento(),
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
                if (LibString.IsNullOrEmpty(_ConectorJson.Token)) {
                    vChekConeccion = _ConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Autenticacion));
                } else {
                    vChekConeccion = true;
                }
                if (vChekConeccion) {
                    ConfigurarDocumento();
                    string vDocumentoJSON = clsConectorJson.SerializeJSON(vDocumentoDigital);
                    vDocumentoJSON = clsConectorJson.LimpiaRegistrosTempralesEnJSON(vDocumentoJSON);
                    vRespuestaConector = _ConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Emision), _ConectorJson.Token, NumeroDocumento(), (int)TipoDeDocumento);
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
            XElement vIdentificacionDocumento = GetIdentificacionDocumento();
            XElement vVendedor = GetDatosVendedor();
            XElement vComprador = GetDatosComprador();
            XElement vTotales = GetTotales();
            XElement vTotalesME = GetTotalesME();
            var vObservaciones = GetDatosInfoAdicional().Descendants("InfoAdicional");
            var vDetalleFactura = GetDetalleFactura().Descendants("detallesItems");
            vDocumentoDigital = new XElement("documentoElectronico", new XElement("encabezado"));
            vDocumentoDigital.Element("encabezado").Add(vIdentificacionDocumento);
            vDocumentoDigital.Element("encabezado").Add(vVendedor);
            vDocumentoDigital.Element("encabezado").Add(vComprador);
            vDocumentoDigital.Element("encabezado").Add(vTotales);
            vDocumentoDigital.Element("encabezado").Add(vTotalesME);
            vDocumentoDigital.Add(vDetalleFactura);
            vDocumentoDigital.Add(vObservaciones);           
        }
        #endregion Construye  Documento
        #region Identificacion de Documento

        private string NumeroDocumento() {
            string vResult = FacturaImprentaDigital.Numero;
            if (FacturaImprentaDigital.TipoDeDocumentoAsEnum == eTipoDocumentoFactura.Factura) {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios")) {
                    vResult = LibString.SubString(vResult, LibString.IndexOf(vResult, '.') + 1);
                }
            }
            return vResult;
        }


        private string SerieDocumento() {
            string vResult = string.Empty;
            if (FacturaImprentaDigital.TipoDeDocumentoAsEnum == eTipoDocumentoFactura.Factura) {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios")) {
                    vResult = LibString.Left(FacturaImprentaDigital.Numero, LibString.IndexOf(FacturaImprentaDigital.Numero, '.'));
                }
            }
            return vResult;
        }

        private string NumeroDocumentoFacturaAfectada() {
            string vResult = LibString.Replace(FacturaImprentaDigital.NumeroFacturaAfectada, "-", "");
            if (FacturaImprentaDigital.TipoDeDocumentoAsEnum != eTipoDocumentoFactura.Factura) {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios")) {
                    int vPosPunto = LibString.IndexOf(vResult, '.');
                    if (vPosPunto >= 0) {
                        vResult = LibString.SubString(vResult, vPosPunto + 1);
                    }
                }
            }
            return vResult;
        }

        private string SerieDocumentoFacturaAfectada() {
            string vResult = "";
            if (FacturaImprentaDigital.TipoDeDocumentoAsEnum != eTipoDocumentoFactura.Factura) {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios")) {
                    int vPosPunto = LibString.IndexOf(FacturaImprentaDigital.NumeroFacturaAfectada, '.');
                    if (vPosPunto >= 0) {
                        vResult = LibString.Left(FacturaImprentaDigital.NumeroFacturaAfectada, vPosPunto);
                    }
                }
            }
            return vResult;
        }

        private XElement GetIdentificacionDocumento() {            
            string vHoraEmision = LibConvert.ToStrOnlyForHour(LibConvert.ToDate(FacturaImprentaDigital.HoraModificacion), "hh:mm:ss tt");
            vHoraEmision = LibString.Replace(vHoraEmision, ". ", "");
            vHoraEmision = LibString.Replace(vHoraEmision, "\u00A0", ""); // Caracter No imprimible que agrega el formato de hora de Windows para alguna config regional
            vHoraEmision = LibString.Replace(vHoraEmision, ".", "");
            XElement vResult = new XElement("identificacionDocumento",
                    new XElement("TipoDocumento", GetTipoDocumento(_TipoDeDocumento)),
                    new XElement("numeroDocumento", NumeroDocumento()),
                    new XElement("tipoproveedor", _TipoDeProveedor),
                    new XElement("tipoTransaccion", GetTipoTransaccion(FacturaImprentaDigital.TipoDeTransaccionAsEnum)),
                    new XElement("fechaEmision", LibConvert.ToStr(FacturaImprentaDigital.Fecha)),
                    new XElement("horaEmision", vHoraEmision),
                    //new XElement("anulado", false),
                    new XElement("tipoDePago", GetTipoDePago(FacturaImprentaDigital.FormaDePagoAsEnum)),
                    new XElement("serie", SerieDocumento()),
                    new XElement("sucursal", ""),
                    new XElement("tipoDeVenta", LibEnumHelper.GetDescription(eTipoDeVenta.Interna)),
                    new XElement("moneda", FacturaImprentaDigital.CodigoMoneda));
            if (_TipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito || _TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito) {
                vResult.Add(new XElement("fechaFacturaAfectada", LibConvert.ToStr(FacturaImprentaDigital.FechaDeFacturaAfectada)));
                vResult.Add(new XElement("numeroFacturaAfectada", NumeroDocumentoFacturaAfectada()));
                vResult.Add(new XElement("serieFacturaAfectada", SerieDocumentoFacturaAfectada()));
                vResult.Add(new XElement("montoFacturaAfectada", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalFactura, 2))));
                vResult.Add(new XElement("comentarioFacturaAfectada", FacturaImprentaDigital.Observaciones));
            }
            return vResult;
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
        #region clientes
        private XElement GetDatosComprador() {
            string vPrefijo = string.Empty;
            string vNumeroRif = DarFormatoYObtenerPrefijoRif(ClienteImprentaDigital, ref vPrefijo);
            XElement vResult = new XElement("comprador",
               new XElement("tipoIdentificacion", vPrefijo),
               new XElement("numeroIdentificacion", vNumeroRif),
               new XElement("razonSocial", ClienteImprentaDigital.Nombre),
               new XElement("direccion", ClienteImprentaDigital.Direccion),
               new XElement("pais", "VE"),
               new XElement("telefono", ""),
               new XElement("telefono", ClienteImprentaDigital.Telefono),
               new XElement("correo", ""),
               new XElement("correo", LibString.Trim(ClienteImprentaDigital.Email)));
            return vResult;
        }

        private string DarFormatoYObtenerPrefijoRif(Cliente valCliente, ref string refPrefijoRif) {
            string vNumeroRif = "";
            string vPrefijo = LibString.Left(LibString.ToUpperWithoutAccents(valCliente.NumeroRIF), 1);
            if (LibString.S1IsInS2(vPrefijo, "VJEPG")) {
                vNumeroRif = LibString.Right(valCliente.NumeroRIF, LibString.Len(valCliente.NumeroRIF) - 1);
                vNumeroRif = LibString.Replace(vNumeroRif, "-", "");
            } else {
                vNumeroRif = valCliente.NumeroRIF;
                if (valCliente.EsExtranjeroAsBool) {
                    vPrefijo = "E";
                } else {
                    vPrefijo = "V";
                }
            }
            refPrefijoRif = vPrefijo;
            return vNumeroRif;
        }
        #endregion clientes


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

            //XElement vAtencion = new XElement("Atencion", ClienteImprentaDigital.Contacto);
            //XElement vCiudad = new XElement("Ciudad", ClienteImprentaDigital.Ciudad);
            //XElement vEnvia = new XElement("Envia", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Name);
            //XElement vColetilla = new XElement("Coletilla", vTextoColetilla);
            //XElement vColetilla2 = new XElement("Coletilla2", vTextoColetilla2);
            //XElement vObservaciones = new XElement("Coletilla3", FacturaImprentaDigital.Observaciones);
            //var vColetilla4 = vAtencion + "," + vCiudad + "," + vEnvia;

            //XElement vResult = new XElement("InfoAdicional",
            //    new XElement("InfoAdicional",
            //        new XElement("InfoAdicional",
            //            new XElement("Campo", "PDF"),
            //            new XElement("Valor", LibString.Replace(LibString.Replace(LibString.Replace(clsConectorJson.SerializeJSON(vColetilla), @"\r", ""), @"\n", ""), @" \", @"\")
            //            )),
            //        new XElement("InfoAdicional",
            //            new XElement("Campo", "PDF"),
            //            new XElement("Valor", LibString.Replace(LibString.Replace(LibString.Replace(clsConectorJson.SerializeJSON(vColetilla2), @"\r", ""), @"\n", ""), @" \", @"\")
            //            )),
            //        new XElement("InfoAdicional",
            //            new XElement("Campo", "PDF"),
            //            new XElement("Valor", LibString.Replace(LibString.Replace(LibString.Replace(clsConectorJson.SerializeJSON(vObservaciones), @"\r", ""), @"\n", ""), @" \", @"\")
            //            )),
            //        new XElement("InfoAdicional",
            //            new XElement("Campo", "PDF"),
            //            new XElement("Valor", LibString.Replace(LibString.Replace(LibString.Replace(clsConectorJson.SerializeJSON(vColetilla4), @"\r", ""), @"\n", ""), @" \", @"\")
            //            ))
            //        ));
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
        private XElement GetFormasPago() {
            // Discutir Funcionalidad
            XElement vResult = new XElement("formasPago");
            string vCodigoMoneda;
            decimal vMonto;
            decimal vCambioBs;
            string vFormaDeCobro;
            if (FacturaImprentaDigital.BaseImponibleIGTF > 0) { // Pagos en ML
                vFormaDeCobro = FacturaImprentaDigital.FormaDeCobroAsEnum == eTipoDeFormaDeCobro.Efectivo ? "09" : "99";
                vCambioBs = LibMath.RoundToNDecimals(FacturaImprentaDigital.CambioMostrarTotalEnDivisas, 4);
                vCodigoMoneda = FacturaImprentaDigital.CodigoMonedaDeCobro == CodigoMonedaME ? FacturaImprentaDigital.CodigoMonedaDeCobro : CodigoMonedaME;
                vMonto = LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalFactura + FacturaImprentaDigital.IGTFML, 2));
                vResult.Add(
                    new XElement("formasPago",
                    new XElement("descripcion", LibEnumHelper.GetDescription(FacturaImprentaDigital.FormaDeCobroAsEnum) + " Divisas"),
                    new XElement("forma", vFormaDeCobro),
                    new XElement("monto", vMonto),
                    new XElement("moneda", vCodigoMoneda),
                    new XElement("tipoCambio", vCambioBs)));
            } else {
                vFormaDeCobro = FacturaImprentaDigital.FormaDeCobroAsEnum == eTipoDeFormaDeCobro.Efectivo ? "08" : "99";
                vCambioBs = LibMath.RoundToNDecimals(FacturaImprentaDigital.CambioMostrarTotalEnDivisas, 4);
                //vCodigoMoneda = LibString.S1IsEqualToS2(FacturaImprentaDigital.CodigoMoneda, "VED") ? "VEF" : FacturaImprentaDigital.CodigoMoneda; // Según ISO 3166-1 | códigos de países | by mucattu.com (Código moneda VEF No esta actualizado)
                vCodigoMoneda = FacturaImprentaDigital.CodigoMoneda;
                vMonto = LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalFactura, 2));
                vResult.Add(new XElement("forma", GetFormaDeCobro(FacturaImprentaDigital.FormaDeCobroAsEnum)),
                 new XElement("formasPago",
                    new XElement("forma", vFormaDeCobro),
                    new XElement("descripcion", LibEnumHelper.GetDescription(FacturaImprentaDigital.FormaDeCobroAsEnum)),
                    new XElement("monto", vMonto),
                    new XElement("moneda", vCodigoMoneda),
                    new XElement("tipoCambio", vCambioBs)));
            }
            // Se agrega un renglón temporal para formar una lista (array) en el XELEMENT cuando se serializa a JSON
            vResult.AddFirst(new XElement("forma", GetFormaDeCobro(FacturaImprentaDigital.FormaDeCobroAsEnum)),
                 new XElement("formasPago",
                    new XElement("forma", "99"),
                    new XElement("descripcion", "formaDePagoTemp"),
                    new XElement("monto", "0"),
                    new XElement("moneda", "VED"),
                    new XElement("tipoCambio", "1.00")));
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
            vResult.Add(GetFormasPago().Descendants("formasPago"));
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
        public XElement GetDetalleFactura() {
            // Pendiente : unidades de medida del item
            // Revisar otros cargos y descuentos, revisar y Serial Rollo  
            XElement vResult = new XElement("detallesItems");
            XElement vResultInfoAdicional = new XElement("InfoAdicional");
            if (DetalleFacturaImprentaDigital != null) {
                if (DetalleFacturaImprentaDigital.Count > 0) {
                    foreach (FacturaRapidaDetalle vDetalle in DetalleFacturaImprentaDigital) {
                        string vSerial = LibString.S1IsEqualToS2(vDetalle.Serial, "0") ? "" : vDetalle.Serial;
                        string vRollo = LibString.S1IsEqualToS2(vDetalle.Rollo, "0") ? "" : vDetalle.Rollo;
                        decimal vCantidad = (TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito && FacturaImprentaDigital.GeneradoPorAsEnum == eFacturaGeneradaPor.AjusteIGTF && vDetalle.Cantidad == 0) ? 1 : vDetalle.Cantidad;
                        vResultInfoAdicional = new XElement("infoAdicionalItem",
                            new XElement("infoAdicionalItem", new XElement("Serial", vSerial)),
                            new XElement("infoAdicionalItem", new XElement("Rollo", vRollo)),
                            new XElement("infoAdicionalItem", new XElement("CampoExtraEnRenglonFactura1", vDetalle.CampoExtraEnRenglonFactura1)),
                            new XElement("infoAdicionalItem", new XElement("CampoExtraEnRenglonFactura2", vDetalle.CampoExtraEnRenglonFactura2)));
                        vResult.Add(
                            new XElement("detallesItems",
                                new XElement("numeroLinea", vDetalle.ConsecutivoRenglon),
                                new XElement("codigoPLU", LibString.Left(vDetalle.Articulo, 20)),
                                new XElement("indicadorBienoServicio", vDetalle.TipoDeArticuloAsEnum == eTipoDeArticulo.Servicio ? "2" : "1"),
                                new XElement("descripcion", LibString.Left(vDetalle.Descripcion, 254)),
                                new XElement("cantidad", LibMath.Abs(LibMath.RoundToNDecimals(vCantidad, 2))),
                                new XElement("precioUnitarioDescuento", LibMath.Abs(LibMath.RoundToNDecimals(vDetalle.PorcentajeDescuento, 2))),
                                new XElement("descuentoMonto", LibMath.Abs(LibMath.RoundToNDecimals((vDetalle.Cantidad * vDetalle.PrecioSinIVA * vDetalle.PorcentajeDescuento) / 100, 2))),
                                new XElement("unidadMedida", "NIU"),
                                new XElement("precioUnitario", LibMath.Abs(LibMath.RoundToNDecimals(vDetalle.PrecioSinIVA, 2))),
                                new XElement("precioItem", LibMath.Abs(LibMath.RoundToNDecimals(vDetalle.TotalRenglon, 2))),
                                new XElement("codigoImpuesto", GetAlicuota(vDetalle.AlicuotaIvaAsEnum)),
                                new XElement("tasaIVA", LibMath.RoundToNDecimals(vDetalle.PorcentajeAlicuota, 2)),
                                new XElement("valorIVA", LibMath.Abs(LibMath.RoundToNDecimals(LibMath.RoundToNDecimals((vDetalle.PorcentajeAlicuota * vDetalle.PrecioSinIVA * vDetalle.Cantidad) / 100, 2), 2))),
                                new XElement("valorTotalItem", LibMath.Abs(LibMath.RoundToNDecimals(vDetalle.TotalRenglon, 2))),
                                vResultInfoAdicional.Descendants("infoAdicionalItem")));
                    }
                }
                //Se Agrega una fila temporal para que al serializar el XML a JSON, se reconozca el detalle como elementos de un array (lista)
                vResult.AddFirst(
                            new XElement("detallesItems",
                                new XElement("numeroLinea", "0"),
                                new XElement("codigoPLU", ""),
                                new XElement("indicadorBienoServicio", ""),
                                new XElement("descripcion", "DetalleFacturaTemp"),
                                new XElement("cantidad", "1.00"),
                                new XElement("unidadMedida", ""),
                                new XElement("precioUnitario", "0"),
                                new XElement("precioItem", "0"),
                                new XElement("codigoImpuesto", ""),
                                new XElement("tasaIVA", "0"),
                                new XElement("valorIVA", "0"),
                                new XElement("valorTotalItem", "0")));
            }
            //
            return vResult;
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

        private string GetTipoDocumento(eTipoDocumentoFactura valTipoDocumento) {
            string vResult = "";
            switch (valTipoDocumento) {
                case eTipoDocumentoFactura.Factura:
                    vResult = "01";
                    break;
                case eTipoDocumentoFactura.NotaDeCredito:
                    vResult = "02";
                    break;
                case eTipoDocumentoFactura.NotaDeDebito:
                    vResult = "03";
                    break;
                case eTipoDocumentoFactura.NotaEntrega:
                    vResult = "04";
                    break;
                case eTipoDocumentoFactura.NoAsignado:
                    vResult = "05";
                    break;
                default:
                    vResult = "01";
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


