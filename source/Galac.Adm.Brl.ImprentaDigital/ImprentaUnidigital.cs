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
using System.Globalization;

namespace Galac.Adm.Brl.ImprentaDigital {
    public class ImprentaUnidigital : clsImprentaDigitalBase {

        string _NumeroFactura;
        eTipoDocumentoFactura _TipoDeDocumento;
        JObject vDocumentoDigital;
        string _TipoDeProveedor;
        clsConectorJson _ConectorJson;
        string _StrongeID = "";        

        public ImprentaUnidigital(eTipoDocumentoFactura initTipoDeDocumento, string initNumeroFactura) : base(initTipoDeDocumento, initNumeroFactura) {
            _NumeroFactura = initNumeroFactura;
            _TipoDeDocumento = initTipoDeDocumento;
            _TipoDeProveedor = "";//NORMAL Según catalogo No 2 del layout
            _ConectorJson = new clsConectorJsonUnidigital(LoginUser);
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
                    ActualizaGUIDYProveedorImprentaDigital(_StrongeID);
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
            stRespuestaUD vRespuestaConector = new stRespuestaUD();
            string vMensaje = string.Empty;
            bool vChekConeccion;
            try {
                if(LibString.IsNullOrEmpty(_ConectorJson.Token)) {
                    ObtenerDatosDocumentoEmitido();
                    vChekConeccion = _ConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostUnidigital.Autenticacion));
                } else {
                    vChekConeccion = true;
                }
                if(vChekConeccion) {
                    if(LibString.IsNullOrEmpty(FacturaImprentaDigital.ImprentaDigitalGUID) && LibString.IsNullOrEmpty(FacturaImprentaDigital.NumeroControl)) {
                        JObject IdDocumento = new JObject {
                            { "Number", 10 },
                            { "Serie", "0" },
                            { "DocumentType",GetTipoDocumento( FacturaImprentaDigital.TipoDeDocumentoAsEnum) }};
                        //vRespuestaConector = ((clsConectorJsonUnidigital)_ConectorJson).SendPostJsonUD(IdDocumento.ToString(), LibEnumHelper.GetDescription(eComandosPostUnidigital.EstadoDocumento), _ConectorJson.Token, NumeroDocumento(), TipoDeDocumento);
                        //Mensaje = vRespuestaConector.mensaje;
                    } else if(!LibString.IsNullOrEmpty(FacturaImprentaDigital.ImprentaDigitalGUID) && LibString.IsNullOrEmpty(FacturaImprentaDigital.NumeroControl)) {
                        //vRespuestaConector = ((clsConectorJsonUnidigital)_ConectorJson).SendGetJsonUD(FacturaImprentaDigital.ImprentaDigitalGUID, LibEnumHelper.GetDescription(eComandosPostUnidigital.EstadoDocumento), _ConectorJson.Token, NumeroDocumento(), TipoDeDocumento);
                        //Mensaje = vRespuestaConector.mensaje;
                    } else {
                        //vRespuestaConector.Aprobado = true;
                        Mensaje = "Enviada";
                    }
                } else {
                    Mensaje = vMensaje;
                }
                return vRespuestaConector.Exitoso;
            } catch(GalacException) {
                throw;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            } finally {
                //CodigoRespuesta = vRespuestaConector.codigo ?? string.Empty;
                //_StrongeID = vRespuestaConector.IDGUID;
                //if(vRespuestaConector.Aprobado) {
                //    EstatusDocumento = vRespuestaConector.estado.estadoDocumento ?? string.Empty;
                //    NumeroControl = vRespuestaConector.estado.numeroControl ?? string.Empty;
                //    FechaAsignacion = LibString.IsNullOrEmpty(vRespuestaConector.estado.fechaAsignacion) ? LibDate.MinDateForDB() : LibConvert.ToDate(vRespuestaConector.estado.fechaAsignacion);
                //    HoraAsignacion = LibConvert.ToStr(FechaAsignacion, "hh:mm");
                //}
            }
        }

        public override bool EnviarDocumento() {
            try {
                bool vResult = false;
                string vMensaje = string.Empty;
                stRespuestaUD vRespuestaConector;
                bool vChekConeccion;
                if(LibString.IsNullOrEmpty(_ConectorJson.Token)) {
                    vChekConeccion = _ConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostUnidigital.Autenticacion));
                } else {
                    vChekConeccion = true;
                }
                if(vChekConeccion) {
                    _StrongeID = ((clsConectorJsonUnidigital)_ConectorJson).StrongeId;
                    ConfigurarDocumento();
                    string vDocumentoJSON = vDocumentoDigital.ToString();
                    vRespuestaConector = ((clsConectorJsonUnidigital)_ConectorJson).SendPostJsonUD(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostUnidigital.Emision), _ConectorJson.Token, NumeroDocumento(), TipoDeDocumento);
                    string vGUID = vRespuestaConector.StrongeID;
                    vResult = vRespuestaConector.Exitoso;
                    if(vResult) {
                        ActualizaGUIDYProveedorImprentaDigital(vGUID);
                    } else {
                        Mensaje = vRespuestaConector.MessageUD;
                    }
                } else {
                    Mensaje = vMensaje;
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

        private bool ActualizaGUIDYProveedorImprentaDigital(string valGUID) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            QAdvSql insUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();           
            vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", NumeroFactura, 11);
            vParams.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
            vSql.AppendLine("UPDATE factura ");
            vSql.AppendLine("SET ImprentaDigitalGUID = " + insUtilSql.ToSqlValue(valGUID));
            vSql.AppendLine(", ProveedorImprentaDigital = " + insUtilSql.EnumToSqlValue((int)ProveedorImprentaDigital));            
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND Numero = @NumeroFactura");
            vSql.AppendLine(" AND TipoDeDocumento = @TipoDeDocumento ");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) >= 0;
            return vResult;
        }

        public override bool EnviarDocumentoPorEmail(string valNumeroControl, string valEmail) {
            try {
                bool vResult = false;
                string vMensaje = string.Empty;
                stRespuestaUD vRespuestaConector = new stRespuestaUD();
                bool vChekConeccion = false;
                if(LibString.IsNullOrEmpty(_ConectorJson.Token)) {
                    vChekConeccion = _ConectorJson.CheckConnection(ref vMensaje, LibEnumHelper.GetDescription(eComandosPostUnidigital.Autenticacion));
                } else {
                    vChekConeccion = true;
                }
                JObject JObjectDoc = new JObject {
                            { "numerodocumento", valNumeroControl },
                            { "rif", LoginUser.User },
                            { "email", valEmail }
                        };
                string vDocumentoJSON = JObjectDoc.ToString();
                vRespuestaConector = ((clsConectorJsonUnidigital)_ConectorJson).SendPostJsonUD(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostUnidigital.Email), _ConectorJson.Token, NumeroDocumento(), TipoDeDocumento);
                //vResult = vRespuestaConector.Aprobado;
                //Mensaje = vRespuestaConector.mensaje;
                return vResult;
            } catch(AggregateException gEx) {
                throw new GalacException(gEx.InnerException.Message, eExceptionManagementType.Controlled);
            } catch(GalacException) {
                throw;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }
        #endregion Métodos Básicos
        #region Construcción de Documento
        #region Armar Documento Digital
        public override void ConfigurarDocumento() {
            base.ConfigurarDocumento();
            vDocumentoDigital = GetCuerpoDocumento();
            vDocumentoDigital.Add("Details", GetDetalleFactura());
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
            if(vDataResult != null && vDataResult.HasElements) {
                vResult = LibXml.GetPropertyString(vDataResult, "NumeroControl");
            }
            return vResult;
        }

        private JObject GetCuerpoDocumento() {
            //decimal vImpuestoIGTF = FacturaImprentaDigital.CodigoMoneda == CodigoMonedaLocal ? FacturaImprentaDigital.IGTFML : FacturaImprentaDigital.IGTFME;
            //vImpuestoIGTF = LibMath.Abs(vImpuestoIGTF);
            JObject vJsonDoc = new JObject {
                { "SerieStrongId",_StrongeID },
                { "DocumentType", GetTipoDocumento(FacturaImprentaDigital.TipoDeDocumentoAsEnum) },
                { "Number", LibConvert.ToInt(NumeroDocumento()) },
                { "EmissionDateAndTime", GetFechaHoraEmision(FacturaImprentaDigital.Fecha,FacturaImprentaDigital.HoraModificacion) },
                { "Name", ClienteImprentaDigital.Nombre},
                { "FiscalRegistry",DarFormatoIdentficacionCliente(ClienteImprentaDigital.NumeroRIF) },
                { "Address", ClienteImprentaDigital.Direccion },
                { "Phone", ClienteImprentaDigital.Telefono },
                { "EmailTo", ClienteImprentaDigital.Email },
                { "PaymentType", FacturaImprentaDigital.FormaDePagoAsString },
                { "Currency", FacturaImprentaDigital.CodigoMoneda == "VED" ? "VES": FacturaImprentaDigital.CodigoMoneda },
                { "Discount",FacturaImprentaDigital.MontoDescuento1 },
                { "PreviousBalance", FacturaImprentaDigital.MontoGravableAlicuota1 +FacturaImprentaDigital.MontoGravableAlicuota2 +FacturaImprentaDigital.MontoGravableAlicuota3 },
                { "ExemptAmount",LibMath.Abs( FacturaImprentaDigital.TotalMontoExento) },
                { "TaxBase", LibMath.Abs(FacturaImprentaDigital.MontoGravableAlicuota1) },
                { "TaxAmount",LibMath.Abs(FacturaImprentaDigital.MontoIvaAlicuota1) },
                { "TaxPercent", FacturaImprentaDigital.PorcentajeAlicuota1 },
                { "TaxPercentReduced", FacturaImprentaDigital.PorcentajeAlicuota2 },
                { "TaxBaseReduced", LibMath.Abs(FacturaImprentaDigital.MontoGravableAlicuota2) },
                { "TaxAmountReduced", LibMath.Abs(FacturaImprentaDigital.MontoIvaAlicuota2) },
                { "TaxPercentSumptuary", FacturaImprentaDigital.PorcentajeAlicuota3 },
                { "TaxBaseSumptuary", LibMath.Abs(FacturaImprentaDigital.MontoGravableAlicuota3) },
                { "TaxAmountSumptuary", LibMath.Abs(FacturaImprentaDigital.MontoIvaAlicuota3) },
                { "Total", LibMath.Abs(FacturaImprentaDigital.TotalFactura)},
                { "IGTFBaseAmount", FacturaImprentaDigital.BaseImponibleIGTF },
                { "IGTFAmount", FacturaImprentaDigital.IGTFML },
                { "IGTFPercentage", FacturaImprentaDigital.AlicuotaIGTF },
                { "GrandTotal",  LibMath.Abs(FacturaImprentaDigital.TotalFactura+FacturaImprentaDigital.IGTFML)},
                { "AmountLetters", LibConvert.ToNumberInLetters(LibMath.Abs(FacturaImprentaDigital.TotalFactura)+FacturaImprentaDigital.IGTFML,false,"") },
                { "ConversionCurrency",CodigoMonedaME =="VED" ? "VES":CodigoMonedaME },
                { "DiscountVES",GetMontoME( FacturaImprentaDigital.MontoDescuento1) } ,
                { "ExemptAmountVES", GetMontoME( FacturaImprentaDigital.TotalMontoExento) },
                { "TaxBaseVES",  GetMontoME( FacturaImprentaDigital.MontoGravableAlicuota1) },
                { "TaxAmountVES",  GetMontoME( FacturaImprentaDigital.MontoIvaAlicuota1) },
                { "TaxPercentVES", FacturaImprentaDigital.PorcentajeAlicuota1 },
                { "TaxPercentReducedVES", FacturaImprentaDigital.PorcentajeAlicuota2 },
                { "TaxBaseReducedVES", GetMontoME(FacturaImprentaDigital.MontoGravableAlicuota2) },
                { "TaxAmountReducedVES", GetMontoME(FacturaImprentaDigital.MontoGravableAlicuota2) },
                { "TaxPercentSumptuaryVES", FacturaImprentaDigital.PorcentajeAlicuota3 },
                { "TaxBaseSumptuaryVES", FacturaImprentaDigital.MontoGravableAlicuota3 },
                { "TaxAmountSumptuaryVES", FacturaImprentaDigital.MontoIvaAlicuota3 },
                { "TotalVES", GetMontoME(FacturaImprentaDigital.TotalFactura) },
                { "IGTFBaseAmountVES", GetMontoME(FacturaImprentaDigital.BaseImponibleIGTF) },
                { "IGTFAmountVES", GetMontoME(FacturaImprentaDigital.AlicuotaIGTF) },
                { "GrandTotalVES", GetMontoME(FacturaImprentaDigital.TotalFactura+FacturaImprentaDigital.AlicuotaIGTF) },
                { "AmountLettersVES", LibConvert.ToNumberInLetters(GetMontoME(FacturaImprentaDigital.TotalFactura+FacturaImprentaDigital.AlicuotaIGTF) ,false,"") },
                { "ExchangeRate", CambioABolivares },
                { "Note1", FacturaImprentaDigital.Observaciones}
                //{ "SystemReference", "" }
                //{ "Note2", "" },
                //{ "Note3", "" },
                //{ "Extra", new JObject() },
                //{ "ShippingAddress", "" }
            };
            if(FacturaImprentaDigital.TipoDeDocumentoAsEnum == eTipoDocumentoFactura.NotaDeCredito || FacturaImprentaDigital.TipoDeDocumentoAsEnum == eTipoDocumentoFactura.NotaDeDebito) {
                vJsonDoc.Add("AffectedDocumentNumber", LibConvert.ToInt(FacturaImprentaDigital.NumeroFacturaAfectada));
            }
            return vJsonDoc;
        }

        private decimal GetMontoME(decimal valMonto) {
            decimal vResult = 0;
            valMonto=LibMath.Abs(valMonto); 
            if(LibString.S1IsEqualToS2(FacturaImprentaDigital.CodigoMoneda, CodigoMonedaME)) {
                vResult = LibMath.RoundToNDecimals(valMonto * CambioABolivares, 2);
            } else {
                vResult = LibMath.RoundToNDecimals(valMonto / CambioABolivares, 2);
            }
            return vResult;
        }

        private string DarFormatoIdentficacionCliente(string valRifCliente) {
            string vPrefijo = LibString.Left(LibString.ToUpperWithoutAccents(valRifCliente), 1);
            string vNumeroRif = valRifCliente;
            if(LibString.S1IsInS2(vPrefijo, "VJEPG")) {
                vNumeroRif = LibString.Right(vNumeroRif, LibString.Len(vNumeroRif) - 1);
                if(!LibString.S1IsInS2("-", vNumeroRif)) {
                    vNumeroRif = vPrefijo + "-" + vNumeroRif;
                }
            }
            return vNumeroRif;
        }

        private string GetFechaHoraEmision(DateTime valFecha, string valHora) {
            string vISO8601Format = "yyyy-MM-ddTHH:mm:ssZ";
            string vResult = LibConvert.ToStr(valFecha, "dd-MM-yyyy") + " " + valHora;
            DateTime vTimeISO8601 = LibConvert.ToDate(vResult);
            return vTimeISO8601.ToString(vISO8601Format, CultureInfo.InvariantCulture);
        }

        #endregion Identificacion de Documento

        #region Detalle RenglonFactura
        public JArray GetDetalleFactura() {
            // Pendiente : unidades de medida del item
            // Revisar otros cargos y descuentos, revisar y Serial Rollo  
            JArray vDetalleFactura = new JArray();
            if(DetalleFacturaImprentaDigital != null) {
                if(DetalleFacturaImprentaDigital.Count > 0) {
                    foreach(FacturaRapidaDetalle vDetalle in DetalleFacturaImprentaDigital) {
                        string vSerial = LibString.S1IsEqualToS2(vDetalle.Serial, "0") ? "" : vDetalle.Serial;
                        string vRollo = LibString.S1IsEqualToS2(vDetalle.Rollo, "0") ? "" : vDetalle.Rollo;
                        string vInfoAdicional = string.Empty;
                        decimal vCantidad = ((TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito || TipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito) && FacturaImprentaDigital.GeneradoPorAsEnum == eFacturaGeneradaPor.AjusteIGTF && vDetalle.Cantidad == 0) ? 1 : LibMath.Abs(vDetalle.Cantidad);
                        decimal vPorcentajeBaseImponible = vDetalle.PorcentajeBaseImponible == 0 ? 1 : vDetalle.PorcentajeBaseImponible;
                        decimal valorDescuento = LibMath.Abs(LibMath.RoundToNDecimals((vDetalle.PorcentajeDescuento * vDetalle.PrecioSinIVA * vCantidad) / 100, 2));
                        decimal vMontoItem = LibMath.Abs(vDetalle.TotalRenglon);
                        decimal vIva = LibMath.RoundToNDecimals(vMontoItem * vDetalle.PorcentajeAlicuota / vPorcentajeBaseImponible, 2);
                        JObject vElement = new JObject() {
                        {"OperationCode",  vDetalle.Articulo},
                        {"Description", vDetalle.Descripcion},
                        {"Quantity", LibMath.Abs(vCantidad)},
                        {"UnitPrice", vDetalle.PrecioSinIVA},
                        {"Amount", vMontoItem},
                        {"TaxAmount", vIva},
                        {"TaxPercent", vDetalle.PorcentajeAlicuota},
                        {"IsExempt", vDetalle.AlicuotaIvaAsEnum == eTipoDeAlicuota.Exento} };
                        vDetalleFactura.Add(vElement);
                    }
                }
            }
            return vDetalleFactura;
        }
        #endregion Detalle RenglonFactura      
        #endregion
        #region Conversion de Tipos       

        private string GetTipoDocumento(eTipoDocumentoFactura valTipoDocumento) {
            string vResult = "";
            switch(valTipoDocumento) {
                case eTipoDocumentoFactura.Factura:
                    vResult = "FA";
                    break;
                case eTipoDocumentoFactura.NotaDeCredito:
                    vResult = "NC";
                    break;
                case eTipoDocumentoFactura.NotaDeDebito:
                    vResult = "ND";
                    break;
                case eTipoDocumentoFactura.NotaEntrega: // GUIA DE DESPACHO
                    vResult = "GD";
                    break;
                default:
                    vResult = "FA";
                    break;
            }
            return vResult;
        }

        public override bool AnularDocumento() {
            throw new NotImplementedException();
        }
        #endregion Conversion de Tipos
    }
}


