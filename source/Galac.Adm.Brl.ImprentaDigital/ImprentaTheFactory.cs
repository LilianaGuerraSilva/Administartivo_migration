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
    public class ImprentaTheFactory : clsImprentaDigitalBase {

        string _NumeroFactura;
        eTipoDocumentoFactura _TipoDeDocumento;
        JObject vDocumentoDigital;
        string _TipoDeProveedor;
        clsConectorJson _ConectorJson;

        public ImprentaTheFactory(eTipoDocumentoFactura initTipoDeDocumento, string initNumeroFactura) : base(initTipoDeDocumento, initNumeroFactura) {
            _NumeroFactura = initNumeroFactura;
            _TipoDeDocumento = initTipoDeDocumento;
            _TipoDeProveedor = "";//NORMAL Según catalogo No 2 del layout
            _ConectorJson = new clsConectorJsonTheFactory(LoginUser);
        }
        #region Métodos Basicos
        public override bool SincronizarDocumento() {
            try {
                bool vResult = false;
                bool vDocumentoExiste = false;
                if(LibString.IsNullOrEmpty(EstatusDocumento)) {
                    vDocumentoExiste = EstadoDocumento();
                }
                if(vDocumentoExiste) { // Documento Existe en ID
                    vResult = base.SincronizarDocumento();
                } else if(LibString.S1IsEqualToS2(CodigoRespuesta, "203")) { // Documento No Existe en ID
                    vResult = EnviarDocumento();
                }
                return vResult;
            } catch(AggregateException gEx) {
                throw new GalacException(gEx.InnerException.Message, eExceptionManagementType.Controlled);
            } catch(GalacException) {
                throw;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public override bool EstadoLoteDocumentos() {
            try {
                bool vResult = false;
                return vResult;
            } catch(GalacException) {
                throw;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public override bool EstadoDocumento() {
            stRespuestaTF vRespuestaConector = new stRespuestaTF();
            string vMensaje = string.Empty;
            bool vChekConeccion;
            string vDocumentoJSON;
            try {
                if(LibString.IsNullOrEmpty(_ConectorJson.Token)) {
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
                if(vChekConeccion) {
                    vDocumentoJSON = clsConectorJson.SerializeJSON(vJsonDeConsulta);//Construir XML o JSON Con datos                                                                                    
                    vRespuestaConector = ((clsConectorJsonTheFactory)_ConectorJson).SendPostJsonTF(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.EstadoDocumento), _ConectorJson.Token, NumeroDocumento(), TipoDeDocumento);
                    Mensaje = vRespuestaConector.mensaje;
                } else {
                    Mensaje = vMensaje;
                }
                return vRespuestaConector.Aprobado;
            } catch(GalacException) {
                throw;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            } finally {
                CodigoRespuesta = vRespuestaConector.codigo ?? string.Empty;
                if(vRespuestaConector.Aprobado) {
                    EstatusDocumento = vRespuestaConector.estado.estadoDocumento ?? string.Empty;
                    NumeroControl = vRespuestaConector.estado.numeroControl ?? string.Empty;
                    FechaAsignacion = LibString.IsNullOrEmpty(vRespuestaConector.estado.fechaAsignacion) ? LibDate.MinDateForDB() : LibConvert.ToDate(vRespuestaConector.estado.fechaAsignacion);
                    HoraAsignacion = vRespuestaConector.estado.horaAsignacion ?? string.Empty;
                }
            }
        }

        public override bool AnularDocumento() {
            try {
                bool vResult = false;
                stRespuestaTF vRespuestaConector = new stRespuestaTF();
                bool vDocumentoExiste = EstadoDocumento();
                if(LibString.IsNullOrEmpty(EstatusDocumento)) {
                    vDocumentoExiste = EstadoDocumento();
                }
                if(vDocumentoExiste) {
                    if(!LibString.S1IsEqualToS2(EstatusDocumento, "Anulada")) {
                        stSolicitudDeAccion vSolicitudDeAnulacion = new stSolicitudDeAccion() {
                            Serie = SerieDocumento(),
                            TipoDocumento = GetTipoDocumento(FacturaImprentaDigital.TipoDeDocumentoAsEnum),
                            NumeroDocumento = NumeroDocumento(),
                            MotivoAnulacion = FacturaImprentaDigital.MotivoDeAnulacion
                        };
                        string vDocumentoJSON = clsConectorJson.SerializeJSON(vSolicitudDeAnulacion); //Construir XML o JSON Con datos                         
                        vRespuestaConector = ((clsConectorJsonTheFactory)_ConectorJson).SendPostJsonTF(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Anular), _ConectorJson.Token);
                        vResult = vRespuestaConector.Aprobado;
                        Mensaje = vRespuestaConector.mensaje;
                    } else {
                        Mensaje = $"No se pudo anular la {FacturaImprentaDigital.TipoDeDocumentoAsString} en la Imprenta Digital, debe sincronizar el documento.";
                    }
                } else {
                    if(!LibString.IsNullOrEmpty(Mensaje)) {
                        Mensaje = $"La {FacturaImprentaDigital.TipoDeDocumentoAsString} no pudo ser anulada.\r\n" + Mensaje;
                    }
                }
                return vResult;
            } catch(AggregateException gEx) {
                throw new GalacException(gEx.InnerException.Message, eExceptionManagementType.Controlled);
            } catch(GalacException) {
                throw;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public override bool EnviarDocumento() {
            try {
                bool vResult = false;
                string vMensaje = string.Empty;
                stRespuestaTF vRespuestaConector;
                bool vChekConeccion;
                if(LibString.IsNullOrEmpty(_ConectorJson.Token)) {
                    vChekConeccion = _ConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Autenticacion));
                } else {
                    vChekConeccion = true;
                }
                if(vChekConeccion) {
                    ConfigurarDocumento();
                    string vDocumentoJSON = vDocumentoDigital.ToString();                    
                    vRespuestaConector = ((clsConectorJsonTheFactory)_ConectorJson).SendPostJsonTF(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Emision), _ConectorJson.Token, NumeroDocumento(), TipoDeDocumento);
                    vResult = vRespuestaConector.Aprobado;
                    if(vResult) {
                        HoraAsignacion = vRespuestaConector.resultados.fechaAsignacionNumeroControl;
                        NumeroControl = vRespuestaConector.resultados.numeroControl;
                        ActualizaNroControlYProveedorImprentaDigital();
                    } else {
                        Mensaje = vRespuestaConector.mensaje;
                        throw new Exception(Mensaje);
                    }
                } else {
                    Mensaje = vMensaje;
                    throw new Exception(Mensaje);
                }
                return vResult;
            } catch(AggregateException gEx) {
                throw new GalacException(gEx.InnerException.Message, eExceptionManagementType.Controlled);
            } catch(GalacException) {
                throw;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public override bool EnviarDocumentoPorEmail(string valNumeroControl, string valEmail) {
            throw new NotImplementedException();
        }
        #endregion Métodos Básicos
        #region Construcción de Documento
        #region Armar Documento Digital
        public override void ConfigurarDocumento() {
            base.ConfigurarDocumento();
            vDocumentoDigital = new JObject();
            JObject vEncabezadoElements = new JObject {
                { "identificacionDocumento", GetIdentificacionDocumento() },
                { "vendedor", GetDatosVendedor() },
                { "comprador", GetDatosComprador() },
                { "totales",  GetTotales() },
                { "TotalesOtraMoneda", GetTotalesME()}};
            JObject vEncabezado = new JObject { { "encabezado", vEncabezadoElements } };
            vDocumentoDigital.Add("documentoElectronico", vEncabezado);
            vDocumentoDigital.Add("detallesItems", GetDetalleFactura());
            vDocumentoDigital.Add("InfoAdicional", GetDatosInfoAdicional());            
        }
        #endregion Construye  Documento
        #region Identificacion de Documento

        private string NumeroDocumento() {
            string vResult = FacturaImprentaDigital.Numero;
            if(FacturaImprentaDigital.TipoDeDocumentoAsEnum == eTipoDocumentoFactura.Factura) {
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios")) {
                    vResult = LibString.SubString(vResult, LibString.IndexOf(vResult, '.') + 1);
                }
            }
            return vResult;
        }


        private string SerieDocumento() {
            string vResult = string.Empty;
            if(FacturaImprentaDigital.TipoDeDocumentoAsEnum == eTipoDocumentoFactura.Factura) {
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios")) {
                    vResult = LibString.Left(FacturaImprentaDigital.Numero, LibString.IndexOf(FacturaImprentaDigital.Numero, '.'));
                }
            }
            return vResult;
        }

        private string NumeroDocumentoFacturaAfectada() {
            string vResult = LibString.Replace(FacturaImprentaDigital.NumeroFacturaAfectada, "-", "");
            if(FacturaImprentaDigital.TipoDeDocumentoAsEnum != eTipoDocumentoFactura.Factura) {
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios")) {
                    int vPosPunto = LibString.IndexOf(vResult, '.');
                    if(vPosPunto >= 0) {
                        vResult = LibString.SubString(vResult, vPosPunto + 1);
                    }
                }
            }
            return vResult;
        }

        private string SerieDocumentoFacturaAfectada() {
            string vResult = "";
            if(FacturaImprentaDigital.TipoDeDocumentoAsEnum != eTipoDocumentoFactura.Factura) {
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios")) {
                    int vPosPunto = LibString.IndexOf(FacturaImprentaDigital.NumeroFacturaAfectada, '.');
                    if(vPosPunto >= 0) {
                        vResult = LibString.Left(FacturaImprentaDigital.NumeroFacturaAfectada, vPosPunto);
                    }
                }
            }
            return vResult;
        }

        private JObject GetIdentificacionDocumento() {
            string vHoraEmision = LibConvert.ToStrOnlyForHour(LibConvert.ToDate(FacturaImprentaDigital.HoraModificacion), "hh:mm:ss tt");
            vHoraEmision = LibString.Replace(vHoraEmision, ". ", "");
            vHoraEmision = LibString.Replace(vHoraEmision, "\u00A0", ""); // Caracter No imprimible que agrega el formato de hora de Windows para alguna config regional
            vHoraEmision = LibString.Replace(vHoraEmision, ".", "");
            JObject vResult = new JObject {
                {"TipoDocumento",GetTipoDocumento(_TipoDeDocumento) },
                {"numeroDocumento", NumeroDocumento()},
                {"tipoproveedor", _TipoDeProveedor},
                {"tipoTransaccion", GetTipoTransaccion(FacturaImprentaDigital.TipoDeTransaccionAsEnum)},
                {"fechaEmision", LibConvert.ToStr(FacturaImprentaDigital.Fecha)},
                {"horaEmision", vHoraEmision},
                {"tipoDePago", GetTipoDePago(FacturaImprentaDigital.FormaDePagoAsEnum)},
                {"serie", SerieDocumento()},
                {"sucursal", ""},
                {"tipoDeVenta", LibEnumHelper.GetDescription(eTipoDeVenta.Interna)},
                { "moneda", FacturaImprentaDigital.CodigoMoneda}};
            if(_TipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito || _TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito) {
                vResult.Add("fechaFacturaAfectada", LibConvert.ToStr(FacturaImprentaDigital.FechaDeFacturaAfectada));
                vResult.Add("numeroFacturaAfectada", NumeroDocumentoFacturaAfectada());
                vResult.Add("serieFacturaAfectada", SerieDocumentoFacturaAfectada());
                vResult.Add("montoFacturaAfectada", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalFactura, 2)));
                vResult.Add("comentarioFacturaAfectada", FacturaImprentaDigital.Observaciones);
            }
            return vResult;
        }

        #endregion Identificacion de Documento
        #region vendedor
        private JObject GetDatosVendedor() {
            JObject vResult = new JObject {
                {"codigo", VendedorImprentaDigital.Codigo },
                {"nombre", VendedorImprentaDigital.Nombre },
                {"numCajero", VendedorImprentaDigital.Codigo} };
            return vResult;
        }
        #endregion vendedor
        #region clientes
        private JObject GetDatosComprador() {
            string vPrefijo = string.Empty;//comprador
            string vNumeroRif = DarFormatoYObtenerPrefijoRif(ClienteImprentaDigital, ref vPrefijo);
            JArray vListaCorreos = ListaSimpleDeElementos(new string[] { ClienteImprentaDigital.Email });
            JArray vListaTelefonos = ListaSimpleDeElementos(new string[] { ClienteImprentaDigital.Telefono });
            JObject vResult = new JObject {
               {"tipoIdentificacion", vPrefijo },
               {"numeroIdentificacion", vNumeroRif },
               {"razonSocial", ClienteImprentaDigital.Nombre },
               {"direccion", ClienteImprentaDigital.Direccion },
               {"pais", "VE" } };
            vResult["correo"] = vListaCorreos;
            vResult["telefono"] = vListaTelefonos;
            return vResult;
        }

        private JArray ListaSimpleDeElementos(string[] valArrayElementos) {
            JArray vResult = new JArray();
            foreach(string vElemento in valArrayElementos) {
                vResult.Add(vElemento);
            }
            return vResult;
        }

        private string DarFormatoYObtenerPrefijoRif(Cliente valCliente, ref string refPrefijoRif) {
            string vNumeroRif = "";
            string vPrefijo = LibString.Left(LibString.ToUpperWithoutAccents(valCliente.NumeroRIF), 1);
            if(LibString.S1IsInS2(vPrefijo, "VJEPG")) {
                vNumeroRif = LibString.Right(valCliente.NumeroRIF, LibString.Len(valCliente.NumeroRIF) - 1);
                vNumeroRif = LibString.Replace(vNumeroRif, "-", "");
            } else {
                vNumeroRif = valCliente.NumeroRIF;
                if(valCliente.EsExtranjeroAsBool) {
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
        private JArray GetDatosInfoAdicional() {
            JArray vResult = new JArray();//"InfoAdicional"
            string vTextoColetilla = "Este pago estará sujeto al cobro adicional del 3% del Impuesto a las Grandes Transacciones Financieras (IGTF), de conformidad con la Providencia Administrativa SNAT/2022/000013 publicada en la G.O.N 42.339 del 17-03- 2022, en caso de ser cancelado en divisas. Este impuesto no aplica en pago en Bs.";
            string vTextoColetilla2 = "En los casos en que la base imponible de la venta o prestación de servicio estuviere expresada en moneda extranjera, se establecerá la equivalencia en moneda nacional, al tipo de cambio corriente en el mercado del día en que ocurra el hecho imponible, salvo que éste ocurra en un día hábil para el sector financiero, en cuyo caso se aplicará el vigente en el día hábil inmediatamente siguiente al de la operación. (art 25 Ley de IVA G.O 6.152 de fecha 18/11/2014).";
            if(_TipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito) {
                vTextoColetilla = "";
            }

            string vObservaciones = string.Empty;
            if(FacturaImprentaDigital.TipoDeDocumentoAsEnum == eTipoDocumentoFactura.NotaDeCredito || FacturaImprentaDigital.TipoDeDocumentoAsEnum == eTipoDocumentoFactura.NotaDeDebito) {
                if(LibString.InStr(FacturaImprentaDigital.NumeroFacturaAfectada, "-") > 0) {
                    vObservaciones = "Nro.Fact.Afectada Original: " + FacturaImprentaDigital.NumeroFacturaAfectada + ". ";
                }
            }
            vObservaciones += FacturaImprentaDigital.Observaciones;

            JObject vColetilla1 = new JObject { { "Coletilla",vTextoColetilla} };
            JObject vFiled1 = CreateFieldAndValue(vColetilla1);                

            JObject vColetilla2 = new JObject { { "Coletilla2", vTextoColetilla2 } };
            JObject vFiled2 = CreateFieldAndValue(vColetilla2);

            JObject vColetilla3= new JObject { { "Coletilla3", vObservaciones } };
            JObject vFiled3 = CreateFieldAndValue(vColetilla3);

            JObject vReceptor = new JObject {
                { "Atencion", ClienteImprentaDigital.Contacto },                
                { "Ciudad", ClienteImprentaDigital.Ciudad },
                { "Envia", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Name }};
            JObject vFiled4 = CreateFieldAndValue(vReceptor);

            JObject vInfoAddCliente = new JObject {               
                { "Ciudad", InfoAdicionalClienteImprentaDigital.Ciudad },
                { "DireccionServicio", InfoAdicionalClienteImprentaDigital.Direccion}};
            JObject vFiled5 = CreateFieldAndValue(vInfoAddCliente);

            vResult.Add(vFiled1);
            vResult.Add(vFiled2);
            vResult.Add(vFiled3);
            vResult.Add(vFiled4);
            vResult.Add(vFiled5);
            return vResult;           
        }

        static JObject CreateFieldAndValue(JObject valElement) {
            JObject vResult = new JObject {
                {"Campo","PDF" },
                {"Valor",valElement}
            };
            return vResult;
        }
        #endregion InfoAdicional

        #region Impuestos
        private JArray GetTotalImpuestosML() {
            decimal vCambio = 0;
            JArray vListaImpuesto = new JArray();
            if(LibString.S1IsInS2(FacturaImprentaDigital.CodigoMoneda, CodigoMonedaLocal)) {
                vCambio = 1;
            } else {
                vCambio = CambioABolivares;
            }
            vListaImpuesto.Add(
                new JObject {{ "CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Exento)},
                {"AlicuotaImp", 0m },
                {"BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalMontoExento * vCambio, 2)) },
                { "ValorTotalImp", 0m} });
            vListaImpuesto.Add(
                new JObject {{"CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.AlicuotaGeneral) },
                {"AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota1, 2)},
                {"BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota1 * vCambio, 2))},
                { "ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota1 * vCambio, 2)) } });
            vListaImpuesto.Add(
                new JObject{{"CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Alicuota2) },
                {"AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota2, 2) },
                {"BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota2 * vCambio, 2)) },
                { "ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota2 * vCambio, 2)) } });
            vListaImpuesto.Add(
                new JObject { { "CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Alicuota3) },
                {"AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota3, 2) },
                {"BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota3 * vCambio, 2)) },
                { "ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota3 * vCambio, 2)) } });
            vListaImpuesto.Add(
                new JObject { { "CodigoTotalImp", "P" }, // Percibido -> SAW No lo maneja pero JSON lo requiere
                {"AlicuotaImp", 0m },
                {"BaseImponibleImp", 0m },
                { "ValorTotalImp", 0m} });
            vListaImpuesto.Add(
                new JObject { { "CodigoTotalImp", "IGTF" },
                { "AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.AlicuotaIGTF, 2)},
                { "BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.BaseImponibleIGTF * vCambio, 2))},
                { "ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.IGTFML, 2))} });
            return vListaImpuesto;
        }

        private JArray GetTotalImpuestosME() {
            decimal vCambio = 0;
            JArray vListaImpuesto = new JArray();
            if(LibString.S1IsInS2(FacturaImprentaDigital.CodigoMoneda, CodigoMonedaLocal)) {
                vCambio = CambioABolivares;
            } else {
                vCambio = 1;
            }
            vListaImpuesto.Add(
                new JObject{{ "CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Exento) },
                {"AlicuotaImp", 0m},
                {"BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalMontoExento / vCambio, 2))},
                { "ValorTotalImp", 0m} });
            vListaImpuesto.Add(
                new JObject { { "CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.AlicuotaGeneral) },
                {"AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota1, 2) },
                {"BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota1 / vCambio, 2)) },
                { "ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota1 / vCambio, 2)) } });
            vListaImpuesto.Add(
                new JObject { { "CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Alicuota2) },
                { "AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota2, 2)},
                {"BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota2 / vCambio, 2)) },
                { "ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota2 / vCambio, 2)) } });
            vListaImpuesto.Add(
                new JObject { { "CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Alicuota3) },
                {"AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota3, 2)},
                {"BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota3 / vCambio, 2))},
                { "ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota3 / vCambio, 2)) }});
            vListaImpuesto.Add(
                new JObject { { "CodigoTotalImp", "P" }, // Percibido -> SAW No lo maneja pero JSON lo requiere
                {"AlicuotaImp", 0m},
                {"BaseImponibleImp", 0m},
                { "ValorTotalImp", 0m}});
            vListaImpuesto.Add(
                new JObject { { "CodigoTotalImp", "IGTF" },
                {"AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.AlicuotaIGTF, 2) },
                {"BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.BaseImponibleIGTF / vCambio, 2)) },
                { "ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.IGTFME, 2))} });
            return vListaImpuesto;
        }


        #endregion Impuestos
        #region formas de pago
        private JArray GetFormasPago() {
            // Discutir Funcionalidad
            JArray vResult = new JArray();
            string vCodigoMoneda;
            decimal vMonto;
            decimal vCambioBs;
            string vFormaDeCobro;
            if(FacturaImprentaDigital.BaseImponibleIGTF > 0) { // Pagos en ML
                vFormaDeCobro = FacturaImprentaDigital.FormaDeCobroAsEnum == eTipoDeFormaDeCobro.Efectivo ? "09" : "99";
                vCambioBs = LibMath.RoundToNDecimals(FacturaImprentaDigital.CambioMostrarTotalEnDivisas, 4);
                vCodigoMoneda = FacturaImprentaDigital.CodigoMonedaDeCobro == CodigoMonedaME ? FacturaImprentaDigital.CodigoMonedaDeCobro : CodigoMonedaME;
                vMonto = LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalFactura + FacturaImprentaDigital.IGTFML, 2));
                vResult.Add(
                    new JObject {{"descripcion", LibEnumHelper.GetDescription(FacturaImprentaDigital.FormaDeCobroAsEnum) + " Divisas" },
                    {"forma", vFormaDeCobro },
                    {"monto", vMonto },
                    {"moneda", vCodigoMoneda },
                    { "tipoCambio", vCambioBs } });
            } else {
                vFormaDeCobro = FacturaImprentaDigital.FormaDeCobroAsEnum == eTipoDeFormaDeCobro.Efectivo ? "08" : "99";
                vCambioBs = LibMath.RoundToNDecimals(FacturaImprentaDigital.CambioMostrarTotalEnDivisas, 4);
                //vCodigoMoneda = LibString.S1IsEqualToS2(FacturaImprentaDigital.CodigoMoneda, "VED") ? "VEF" : FacturaImprentaDigital.CodigoMoneda; // Según ISO 3166-1 | códigos de países | by mucattu.com (Código moneda VEF No esta actualizado)
                vCodigoMoneda = FacturaImprentaDigital.CodigoMoneda;
                vMonto = LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalFactura, 2));
                //vResult.Add(new XElement("forma", GetFormaDeCobro(FacturaImprentaDigital.FormaDeCobroAsEnum)),
                // new XElement("formasPago",
                vResult.Add(
                    new JObject { { "descripcion", LibEnumHelper.GetDescription(FacturaImprentaDigital.FormaDeCobroAsEnum) },
                    {"forma", vFormaDeCobro },
                    {"monto", vMonto },
                    {"moneda", vCodigoMoneda },
                    { "tipoCambio", vCambioBs } });
            }
            return vResult;
        }
        #endregion formas de pago
        #region Totales
        private JObject GetTotales() { // Sección Es Fija Siempre en Bolívares
            // listaDescBonificacion -> otros cargos y descuentos se debe revisar.            
            decimal vCambio = LibString.S1IsEqualToS2(FacturaImprentaDigital.CodigoMoneda, CodigoMonedaLocal) ? 1 : CambioABolivares;
            decimal vTotalFacturaMasIGTF = (FacturaImprentaDigital.TotalFactura * vCambio) + FacturaImprentaDigital.IGTFML;
            string vMontoEnLetrasME = MontoEnLetras(vTotalFacturaMasIGTF, FacturaImprentaDigital.CodigoMoneda);
            JObject vResult = new JObject{
                { "nroItems", DetalleFacturaImprentaDigital.Count},
                { "montoGravadoTotal", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalBaseImponible * vCambio, 2))},
                { "montoExentoTotal", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalMontoExento * vCambio, 2))},
                { "subtotal", LibMath.Abs(LibMath.RoundToNDecimals((FacturaImprentaDigital.TotalBaseImponible + FacturaImprentaDigital.TotalMontoExento) * vCambio, 2))},
                { "totalAPagar", LibMath.Abs(LibMath.RoundToNDecimals(vTotalFacturaMasIGTF, 2))},
                { "totalIVA", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalIVA * vCambio, 2))},
                { "montoTotalConIVA", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalFactura * vCambio, 2))},
                { "montoEnLetras", vMontoEnLetrasME},
                { "totalDescuento", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoDescuento1 * vCambio, 2))},
                { "impuestosSubtotal",GetTotalImpuestosML()},
                { "formasPago",GetFormasPago()} };
            return vResult;
        }

        private JObject GetTotalesME() { // Sección Es Fija Siempre en Otra Moneda                        
            decimal vCambio = LibString.S1IsEqualToS2(FacturaImprentaDigital.CodigoMoneda, CodigoMonedaLocal) ? CambioABolivares : 1;
            decimal vTotalFacturaMasIGTF = LibMath.RoundToNDecimals((FacturaImprentaDigital.TotalFactura / vCambio) + FacturaImprentaDigital.IGTFME, 2);
            string vCodigoMonedaLbl = LibString.S1IsEqualToS2(FacturaImprentaDigital.CodigoMoneda, CodigoMonedaLocal) ? CodigoMonedaME : FacturaImprentaDigital.CodigoMoneda;
            string vMontoEnLetrasME = MontoEnLetras(vTotalFacturaMasIGTF, "");
            JObject vResult = new JObject {
                {"moneda", vCodigoMonedaLbl },
                {"tipoCambio", LibMath.Abs(LibMath.RoundToNDecimals(CambioABolivares, 4)) },
                {"montoGravadoTotal", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalBaseImponible / vCambio, 2)) },
                {"montoExentoTotal", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalMontoExento / vCambio, 2)) },
                {"subtotal", LibMath.Abs(LibMath.RoundToNDecimals((FacturaImprentaDigital.TotalBaseImponible + FacturaImprentaDigital.TotalMontoExento) / vCambio, 2)) },
                {"totalAPagar", LibMath.Abs(LibMath.RoundToNDecimals(vTotalFacturaMasIGTF, 2)) },
                {"totalIVA", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalIVA / vCambio, 2)) },
                {"montoTotalConIVA", LibMath.Abs(LibMath.RoundToNDecimals((FacturaImprentaDigital.TotalFactura) / vCambio, 2)) },
                {"montoEnLetras", vMontoEnLetrasME },
                {"totalDescuento", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoDescuento1 / vCambio, 2)) },
                {"impuestosSubtotal", GetTotalImpuestosME()}};
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
            JArray vResult = new JArray();
            JArray vResultInfoAdicional = new JArray();
            if(DetalleFacturaImprentaDigital != null) {
                if(DetalleFacturaImprentaDigital.Count > 0) {
                    foreach(FacturaRapidaDetalle vDetalle in DetalleFacturaImprentaDigital) {
                        string vSerial = LibString.S1IsEqualToS2(vDetalle.Serial, "0") ? "" : vDetalle.Serial;
                        string vRollo = LibString.S1IsEqualToS2(vDetalle.Rollo, "0") ? "" : vDetalle.Rollo;
                        decimal vCantidad = ((TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito || TipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito) && FacturaImprentaDigital.GeneradoPorAsEnum == eFacturaGeneradaPor.AjusteIGTF && vDetalle.Cantidad == 0) ? 1 : vDetalle.Cantidad;
                        vResultInfoAdicional.Add(new JObject { { "Serial", vSerial } });
                        vResultInfoAdicional.Add(new JObject { { "Rollo", vRollo } });
                        vResultInfoAdicional.Add(new JObject { { "CampoExtraEnRenglonFactura1", vDetalle.CampoExtraEnRenglonFactura1 } });
                        vResultInfoAdicional.Add(new JObject { { "CampoExtraEnRenglonFactura2", vDetalle.CampoExtraEnRenglonFactura2 } });
                        JObject vElement = new JObject() {
                                {"numeroLinea", vDetalle.ConsecutivoRenglon },
                                {"codigoPLU", LibString.Left(vDetalle.Articulo, 20) },
                                {"indicadorBienoServicio", vDetalle.TipoDeArticuloAsEnum == eTipoDeArticulo.Servicio ? "2" : "1" },
                                {"descripcion", LibString.Left(vDetalle.Descripcion, 254) },
                                {"cantidad", LibMath.Abs(LibMath.RoundToNDecimals(vCantidad, 2)) },
                                {"precioUnitarioDescuento", LibMath.Abs(LibMath.RoundToNDecimals(vDetalle.PorcentajeDescuento, 2)) },
                                {"descuentoMonto", LibMath.Abs(LibMath.RoundToNDecimals((vDetalle.Cantidad * vDetalle.PrecioSinIVA * vDetalle.PorcentajeDescuento) / 100, 2))},
                                {"unidadMedida", "NIU" },
                                {"precioUnitario", LibMath.Abs(LibMath.RoundToNDecimals(vDetalle.PrecioSinIVA, 2)) },
                                {"precioItem", LibMath.Abs(LibMath.RoundToNDecimals(vDetalle.TotalRenglon, 2)) },
                                {"codigoImpuesto", GetAlicuota(vDetalle.AlicuotaIvaAsEnum) },
                                {"tasaIVA", LibMath.RoundToNDecimals(vDetalle.PorcentajeAlicuota, 2) },
                                {"valorIVA", LibMath.Abs(LibMath.RoundToNDecimals(LibMath.RoundToNDecimals((vDetalle.PorcentajeAlicuota * vDetalle.PrecioSinIVA * vDetalle.Cantidad) / 100, 2), 2)) },
                                { "valorTotalItem", LibMath.Abs(LibMath.RoundToNDecimals(vDetalle.TotalRenglon, 2))  },
                                {"infoAdicional",vResultInfoAdicional } };
                        vResult.Add(vElement);
                    }
                }
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


