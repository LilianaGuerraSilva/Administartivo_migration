using System;
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
using Newtonsoft.Json.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;

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
                if(LibString.IsNullOrEmpty(EstatusDocumento)) {
                    vDocumentoExiste = EstadoDocumento();
                }
                if(vDocumentoExiste) { // Documento Existe en ID
                    vResult = base.SincronizarDocumento();
                } else if(LibString.S1IsEqualToS2(EstatusDocumento, "Este registro no se encuentra en el servicio de imprenta.")) { // Documento No Existe en ID
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
                string vMensaje = string.Empty;
                clsConectorJson vConectorJson = new clsConectorJsonNovus(LoginUser);
                bool vRepuestaConector = vConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostNovus.Autenticacion));
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
            stRespuestaNV vRespuestaConector = new stRespuestaNV();
            string vMensaje = string.Empty;
            bool vChekConeccion;
            string vDocumentoJSON;
            try {
                if(LibString.IsNullOrEmpty(_ConectorJson.Token)) {
                    ObtenerDatosDocumentoEmitido();
                    vChekConeccion = _ConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostNovus.Autenticacion));
                } else {
                    vChekConeccion = true;
                }
                JObject vEstausJson = new JObject {
                     { "rif", LoginUser.User },
                     { "tipo", 2 },
                    { "numerointerno", NumeroDocumento() }};
                if(vChekConeccion) {
                    vDocumentoJSON = vEstausJson.ToString();
                    vRespuestaConector = ((clsConectorJsonNovus)_ConectorJson).SendPostJsonNV(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostNovus.EstadoDocumento), _ConectorJson.Token, NumeroDocumento(), TipoDeDocumento);
                    Mensaje = vRespuestaConector.message;
                } else {
                    Mensaje = vMensaje;
                }
                return vRespuestaConector.success;
            } catch(GalacException) {
                throw;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            } finally {
                EstatusDocumento = vRespuestaConector.message ?? string.Empty;
                if(vRespuestaConector.data != null) {
                    NumeroControl = vRespuestaConector.data.Value.numerodocumento ?? string.Empty;
                    FechaAsignacion = LibString.IsNullOrEmpty(vRespuestaConector.data.Value.fecha) ? LibDate.MinDateForDB() : LibConvert.ToDate(vRespuestaConector.data.Value.fecha);
                    HoraAsignacion = vRespuestaConector.data.Value.hora;
                }
            }
        }

        public override bool AnularDocumento() {
            try {
                bool vResult = false;
                string vMensaje = string.Empty;
                stRespuestaNV vRespuestaConector = new stRespuestaNV();
                bool vChekConeccion = false;
				bool vDocumentoExiste = EstadoDocumento();
                if (LibString.IsNullOrEmpty(EstatusDocumento)) {
                    vDocumentoExiste = EstadoDocumento();
                }
                if (LibString.IsNullOrEmpty(_ConectorJson.Token)) {
                    vChekConeccion = _ConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostNovus.Autenticacion));
                } else {
                    vChekConeccion = true;
                }
                ObtenerDatosDocumentoEmitido();
                if (!LibString.IsNullOrEmpty(FacturaImprentaDigital.NumeroControl)) {
                    if (FacturaImprentaDigital.StatusFacturaAsEnum == eStatusFactura.Anulada) {
                        JObject JObjectDoc = new JObject {
                            { "numerodocumento", FacturaImprentaDigital.NumeroControl },
                            { "observacion", FacturaImprentaDigital.MotivoDeAnulacion },
                            { "rif", LoginUser.User }
                        };
                        vRespuestaConector = ((clsConectorJsonNovus)_ConectorJson).SendPostJsonNV(JObjectDoc.ToString(), LibEnumHelper.GetDescription(eComandosPostNovus.Anular), _ConectorJson.Token, NumeroDocumento(), TipoDeDocumento);                        
                        vResult = vRespuestaConector.success;
                        Mensaje = vRespuestaConector.message;
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
                stRespuestaNV vRespuestaConector = new stRespuestaNV();
                bool vChekConeccion;
                if(LibString.IsNullOrEmpty(_ConectorJson.Token)) {
                    vChekConeccion = _ConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostNovus.Autenticacion));
                } else {
                    vChekConeccion = true;
                }
                if(vChekConeccion) {
                    ConfigurarDocumento();
                    string vDocumentoJSON = vDocumentoDigital.ToString();
                    vRespuestaConector = ((clsConectorJsonNovus)_ConectorJson).SendPostJsonNV(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostNovus.Emision), _ConectorJson.Token, NumeroDocumento(), TipoDeDocumento);
                    vResult = vRespuestaConector.success;
                    if(vResult) {
                        NumeroControl = vRespuestaConector.data.Value.numerodocumento;
                        HoraAsignacion = vRespuestaConector.data.Value.hora;
                        ActualizaNroControlYProveedorImprentaDigital();
                    } else {
                        Mensaje = vRespuestaConector.message;
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
            try {
                bool vResult = false;
                string vMensaje = string.Empty;
                stRespuestaNV vRespuestaConector = new stRespuestaNV();
                bool vChekConeccion = false;
                if (LibString.IsNullOrEmpty(_ConectorJson.Token)) {
                    vChekConeccion = _ConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostNovus.Autenticacion));
                } else {
                    vChekConeccion = true;
                }
                JObject JObjectDoc = new JObject {
                            { "numerodocumento", valNumeroControl },
                            { "rif", LoginUser.User },
                            { "email", valEmail }
                        };
                string vDocumentoJSON = JObjectDoc.ToString();
                vRespuestaConector = ((clsConectorJsonNovus)_ConectorJson).SendPostJsonNV(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostNovus.Email), _ConectorJson.Token, NumeroDocumento(), TipoDeDocumento);                                
                vResult = vRespuestaConector.success;
                Mensaje = vRespuestaConector.message;
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

        private string ObtenerNumeroControlFacturaAfectada() {
            string vResult = string.Empty;           
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT NumeroControl FROM Factura");
            vSql.AppendLine("WHERE ConsecutivoCompania=" + FacturaImprentaDigital.ConsecutivoCompania);
            vSql.AppendLine(" AND Numero = " + new QAdvSql("").ToSqlValue(FacturaImprentaDigital.NumeroFacturaAfectada));
            vSql.AppendLine(" AND TipoDeDocumento = " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura));
            var vDataResult = LibBusiness.ExecuteSelect(vSql.ToString(), null, "", 0);
            if (vDataResult != null && vDataResult.HasElements) {
                vResult = LibXml.GetPropertyString(vDataResult, "NumeroControl");
            }
            return vResult;
        }

        private string GeneraTrackingId() {
            int vSeed = DateTime.Now.Millisecond;
            int vNumberRnd = new Random(vSeed).Next(1, 1000000000); // Random de 10 carácteres
            return LibText.FillWithCharToLeft(LibConvert.ToStr(vNumberRnd), "0", 10);
        }

        private JObject GetCuerpoDocumento() {
            string vTipoIdentficacion = string.Empty;
            string vNumeroRif = DarFormatoYObtenerTipoIdentficacion(ClienteImprentaDigital, ref vTipoIdentficacion);
            decimal vImpuestoIGTF = FacturaImprentaDigital.CodigoMoneda == CodigoMonedaLocal ? FacturaImprentaDigital.IGTFML : FacturaImprentaDigital.IGTFME;
            vImpuestoIGTF = LibMath.Abs(vImpuestoIGTF);
            JObject vJsonDoc = new JObject {
                { "rif", LoginUser.User },
                { "trackingid",  GeneraTrackingId() },
                { "nombrecliente", ClienteImprentaDigital.Nombre },
                { "rifcedulacliente", vNumeroRif },
                { "emailcliente", ClienteImprentaDigital.Email },
                { "idtipocedulacliente", vTipoIdentficacion },
                { "direccioncliente", ClienteImprentaDigital.Direccion },
                { "telefonocliente", ClienteImprentaDigital.Telefono },
                { "idtipodocumento", GetTipoDocumento(_TipoDeDocumento) },
                { "subtotal", LibMath.Abs(FacturaImprentaDigital.TotalRenglones) },
                { "exento", LibMath.Abs(FacturaImprentaDigital.TotalMontoExento) },
                { "tasag", FacturaImprentaDigital.PorcentajeAlicuota1 },
                { "baseg", LibMath.Abs(FacturaImprentaDigital.MontoGravableAlicuota1) },
                { "impuestog", LibMath.Abs(FacturaImprentaDigital.MontoIvaAlicuota1) },
                { "tasar", FacturaImprentaDigital.PorcentajeAlicuota2 },
                { "baser", LibMath.Abs(FacturaImprentaDigital.MontoGravableAlicuota2) },
                { "impuestor", LibMath.Abs(FacturaImprentaDigital.MontoIvaAlicuota2) },
                { "tasaa", FacturaImprentaDigital.PorcentajeAlicuota3 },
                { "basea", LibMath.Abs(FacturaImprentaDigital.MontoGravableAlicuota3) },
                { "impuestoa", LibMath.Abs(FacturaImprentaDigital.MontoIvaAlicuota3) },
                { "tasaigtf", FacturaImprentaDigital.AlicuotaIGTF },
                { "baseigtf", LibMath.Abs(FacturaImprentaDigital.BaseImponibleIGTF) },
                { "impuestoigtf",  vImpuestoIGTF },
                { "total", LibMath.Abs(FacturaImprentaDigital.TotalFactura) + vImpuestoIGTF },
                {"sendmail", 1}, // Siempre Envia Email
                //{"sucursal", ""}, // No Aplica
                {"numerointerno", FacturaImprentaDigital.Numero},
                {"fecha_emision", FacturaImprentaDigital.Fecha},
                {"moneda", FacturaImprentaDigital.CodigoMoneda == CodigoMonedaLocal ? "bs" : "usd"},
                {"tasacambio", CambioABolivares},
                {"observacion", FacturaImprentaDigital.Observaciones} };
            if (_TipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito || _TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito) {
                vJsonDoc.Add("relacionado", ObtenerNumeroControlFacturaAfectada());
            }
            return vJsonDoc;
        }

        private string DarFormatoYObtenerTipoIdentficacion(Cliente valCliente, ref string refTipoIdentficacion) {
            string vPrefijo = LibString.Left(LibString.ToUpperWithoutAccents(valCliente.NumeroRIF), 1);
            string vNumeroRif = valCliente.NumeroRIF;
            if (LibString.Len(vNumeroRif) >= 10) {
                refTipoIdentficacion = "3"; // RIF
            } else {
                refTipoIdentficacion = "1"; // Cedula Por Defecto // Sistema no maneja Pasaporte como tal
            }
            if (LibString.S1IsInS2(vPrefijo, "VJEPG")) {
                vNumeroRif = LibString.Right(vNumeroRif, LibString.Len(vNumeroRif) - 1);
                vNumeroRif = LibString.Replace(vNumeroRif, "-", "");
            }
            return vNumeroRif;
        }
        #endregion Identificacion de Documento

        #region formas de pago
        private JArray GetFormasPago() {
            // Discutir Funcionalidad
            JArray vResult = new JArray();
            JObject vElement = new JObject();
            decimal vMonto;
            string vFormaDeCobro;
            if (FacturaImprentaDigital.BaseImponibleIGTF > 0) { // Pagos en ML
                decimal vImpuestoIGTF = FacturaImprentaDigital.CodigoMoneda == CodigoMonedaLocal ? FacturaImprentaDigital.IGTFML : FacturaImprentaDigital.IGTFME;
                vImpuestoIGTF = LibMath.Abs(vImpuestoIGTF);
                vFormaDeCobro = LibEnumHelper.GetDescription(FacturaImprentaDigital.FormaDePagoAsEnum);
                vMonto = LibMath.Abs(FacturaImprentaDigital.TotalFactura + vImpuestoIGTF);
                vElement.Add("forma", vFormaDeCobro);
                vElement.Add("valor", vMonto);
            } else {
                vFormaDeCobro = LibEnumHelper.GetDescription(FacturaImprentaDigital.FormaDePagoAsEnum);
                vMonto = LibMath.Abs(FacturaImprentaDigital.TotalFactura);
                vElement.Add("forma", vFormaDeCobro);
                vElement.Add("valor", vMonto);
            }
            vResult.Add(vElement);
            return vResult;
        }
        #endregion formas de pago        
        #region Detalle RenglonFactura
        public JArray GetDetalleFactura() {
            // Pendiente : unidades de medida del item
            // Revisar otros cargos y descuentos, revisar y Serial Rollo  
            JArray vDetalleFactura = new JArray();
            if (DetalleFacturaImprentaDigital != null) {
                if (DetalleFacturaImprentaDigital.Count > 0) {
                    foreach (FacturaRapidaDetalle vDetalle in DetalleFacturaImprentaDigital) {
                        string vSerial = LibString.S1IsEqualToS2(vDetalle.Serial, "0") ? "" : vDetalle.Serial;
                        string vRollo = LibString.S1IsEqualToS2(vDetalle.Rollo, "0") ? "" : vDetalle.Rollo;
                        string vInfoAdicional = string.Empty;
                        decimal vCantidad = (TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito && FacturaImprentaDigital.GeneradoPorAsEnum == eFacturaGeneradaPor.AjusteIGTF && vDetalle.Cantidad == 0) ? 1 : LibMath.Abs(vDetalle.Cantidad);
                        decimal vPorcentajeBaseImponible = vDetalle.PorcentajeBaseImponible == 0 ? 1 : vDetalle.PorcentajeBaseImponible;
                        decimal valorDescuento = LibMath.Abs(LibMath.RoundToNDecimals((vDetalle.PorcentajeDescuento * vDetalle.PrecioSinIVA * vCantidad) / 100, 2));
                        decimal vMontoItem = LibMath.Abs(vDetalle.TotalRenglon);
                        decimal vIva = LibMath.RoundToNDecimals(vMontoItem * vDetalle.PorcentajeAlicuota / vPorcentajeBaseImponible, 2);
                        JObject vElement = new JObject() {
                        {"codigo", vDetalle.Articulo},
                        {"descripcion", vDetalle.Descripcion},
                        {"precio", vDetalle.PrecioSinIVA},
                        {"cantidad", vCantidad},
                        {"tasa", vDetalle.PorcentajeAlicuota},
                        {"impuesto", vIva},
                        {"descuento", valorDescuento},
                        {"exento", vDetalle.AlicuotaIvaAsEnum == eTipoDeAlicuota.Exento ? true : false},
                        {"monto", vMontoItem},
                        {"iva", vIva},
                        { "monto_neto", vMontoItem + vIva} };
                        if (!LibString.IsNullOrEmpty(vSerial)) {
                            vInfoAdicional = "Serial:" + vSerial;
                        }
                        if (!LibString.IsNullOrEmpty(vRollo)) {
                            vInfoAdicional = vInfoAdicional + " Rollo:" + vRollo;
                        }
                        if (!LibString.IsNullOrEmpty(vInfoAdicional)) {
                            vElement.Add("comentario", vInfoAdicional);
                        }
                        vDetalleFactura.Add(vElement);
                    }
                }
            }
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


