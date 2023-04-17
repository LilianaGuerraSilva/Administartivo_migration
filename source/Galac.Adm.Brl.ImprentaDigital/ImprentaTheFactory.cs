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
using System.Threading.Tasks;
using System.Xml.Schema;
using Galac.Saw.Ccl.Cliente;
using System.Linq;
using System.Text;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;

namespace Galac.Adm.Brl.ImprentaDigital {
    public class ImprentaTheFactory: clsImprentaDigitalBase {

        string _NumeroFactura;
        eTipoDocumentoFactura _TipoDeDocumento;
        XElement vDocumentoDigital;
        string _TipoDeProveedor;

        public ImprentaTheFactory(eTipoDocumentoFactura initTipoDeDocumento, string initNumeroFactura) : base(initTipoDeDocumento, initNumeroFactura) {
            _NumeroFactura = initNumeroFactura;
            _TipoDeDocumento = initTipoDeDocumento;
            _TipoDeProveedor = "";//NORMAL Según catalogo No 2 del layout
        }
        #region Métodos Basicos
        public override bool SincronizarDocumentos() {
            try {
                bool vResult = false;
                clsConectorJson vConectorJson = new clsConectorJson(LoginUser);
                string vRepuesta = vConectorJson.CheckConnection();
                if (!LibString.IsNullOrEmpty(vConectorJson.Token)) {
                    string vDocumentoJSON = clsConectorJson.SerializeJSON(""); //Construir XML o JSON Con datos 
                    var vReq = vConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.EstadoDocumento), vConectorJson.Token);
                }
                return vResult;
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public override bool EstadoLoteDocumentos() {
            try {
                bool vResult = false;
                clsConectorJson vConectorJson = new clsConectorJson(LoginUser);
                string vRepuesta = vConectorJson.CheckConnection();
                if (!LibString.IsNullOrEmpty(vConectorJson.Token)) {
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
            stPostResq vRespuesta;
            try {
                clsConectorJson vConectorJson = new clsConectorJson(LoginUser);
                ObtenerDatosDocumento();
                stSolicitudDeAccion vJsonDeConsulta = new stSolicitudDeAccion() {
                    Serie = "",
                    TipoDocumento = GetTipoDocumento(FacturaImprentaDigital.TipoDeDocumentoAsEnum),
                    NumeroDocumento = LibString.Right(NumeroFactura, 8)
                };
                string vRepuesta = vConectorJson.CheckConnection();
                if (!LibString.IsNullOrEmpty(vConectorJson.Token)) {
                    string vDocumentoJSON = clsConectorJson.SerializeJSON(vJsonDeConsulta); //Construir XML o JSON Con datos 
                    vRespuesta = vConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.EstadoDocumento), vConectorJson.Token);
                }
                return true;
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            } finally {
                CodigoRespuesta = vRespuesta.codigo ?? string.Empty;
                MensajeRespuesta = vRespuesta.mensaje ?? string.Empty;
            }
        }

        public override bool AnularDocumento() {
            try {
                stPostResq vRespuesta;
                clsConectorJson vConectorJson = new clsConectorJson(LoginUser);
                ObtenerDatosDocumento();
                stSolicitudDeAccion vSolicitudDeAnulacion = new stSolicitudDeAccion() {
                    Serie = "",
                    TipoDocumento = GetTipoDocumento(FacturaImprentaDigital.TipoDeDocumentoAsEnum),
                    NumeroDocumento = LibString.Right(NumeroFactura, 8),
                    MotivoAnulacion = FacturaImprentaDigital.MotivoDeAnulacion
                };
                string vRepuesta = vConectorJson.CheckConnection();
                if (!LibString.IsNullOrEmpty(vConectorJson.Token)) {
                    string vDocumentoJSON = clsConectorJson.SerializeJSON(vSolicitudDeAnulacion); //Construir XML o JSON Con datos 
                    vRespuesta = vConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Anular), vConectorJson.Token);
                }
                return (vRespuesta.codigo == "200");
            } catch (GalacException) {
                throw;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public override bool EnviarDocumento() {
            try {
                bool vResult = false;
                clsConectorJson vConectorJson = new clsConectorJson(LoginUser);
                string vRepuesta = vConectorJson.CheckConnection();
                if (!LibString.IsNullOrEmpty(vConectorJson.Token)) {
                    ConfigurarDocumento();
                    string vDocumentoJSON = clsConectorJson.SerializeJSON(vDocumentoDigital);
                    var vReq = vConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Emision), vConectorJson.Token, NumeroFactura, (int)TipoDeDocumento);
                    NumeroControl = vReq.resultados.numeroControl;
                    vResult = !LibString.IsNullOrEmpty(NumeroControl);
                    if (vResult) {
                        ActualizaNumeroDeControl(NumeroControl);
                    }
                } else {
                    throw new GalacException("Usuario o clave inválida.\r\nPor favor verifique los datos de conexión con su Imprenta Digital.", eExceptionManagementType.Controlled);
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
            var vObservaciones = GetDatosInfoAdicional().Descendants("InfoAdicional");
            var vDetalleFactura = GetDetalleFactura().Descendants("detallesItems");
            vDocumentoDigital = new XElement("documentoElectronico", new XElement("encabezado"));
            vDocumentoDigital.Element("encabezado").Add(vIdentificacionDocumento);
            vDocumentoDigital.Element("encabezado").Add(vVendedor);
            vDocumentoDigital.Element("encabezado").Add(vComprador);
            vDocumentoDigital.Element("encabezado").Add(vTotales);
            vDocumentoDigital.Add(vDetalleFactura);
            if (_TipoDeDocumento == eTipoDocumentoFactura.Factura) {
                vDocumentoDigital.Add(vObservaciones);
            }
        }
        #endregion Construye  Documento
        #region Identificacion de Documento
        private XElement GetIdentificacionDocumento() {
            string vHoraEmision = LibConvert.ToStrOnlyForHour(LibConvert.ToDate(FacturaImprentaDigital.HoraModificacion), "hh:mm:ss tt");
            vHoraEmision = LibString.Replace(vHoraEmision, ". ", "");
            vHoraEmision = LibString.Replace(vHoraEmision, "\u00A0", ""); // Caracter No imprimible que agrega el formato de hora de Windows para alguna config regional
            vHoraEmision = LibString.Replace(vHoraEmision, ".", "");
            XElement vResult = new XElement("identificacionDocumento",
                    new XElement("TipoDocumento", GetTipoDocumento(_TipoDeDocumento)),
                    new XElement("numeroDocumento", LibText.Right(_NumeroFactura, 8)),
                    new XElement("tipoproveedor", _TipoDeProveedor),
                    new XElement("tipoTransaccion", GetTipoTransaccion(FacturaImprentaDigital.TipoDeTransaccionAsEnum)),
                    new XElement("fechaEmision", LibConvert.ToStr(FacturaImprentaDigital.Fecha)),
                    new XElement("horaEmision", vHoraEmision),
                    //new XElement("anulado", false),
                    new XElement("tipoDePago", GetTipoDePago(FacturaImprentaDigital.FormaDePagoAsEnum)),
                    new XElement("serie", ""),
                    new XElement("sucursal", ""),
                    new XElement("tipoDeVenta", LibEnumHelper.GetDescription(eTipoDeVenta.Interna)),
                    new XElement("moneda", FacturaImprentaDigital.CodigoMoneda));
            if (_TipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito || _TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito) {
                vResult.Add(new XElement("fechaFacturaAfectada", LibConvert.ToStr(FacturaImprentaDigital.FechaDeFacturaAfectada)));
                vResult.Add(new XElement("numeroFacturaAfectada", LibString.Right(FacturaImprentaDigital.NumeroFacturaAfectada, 8)));
                vResult.Add(new XElement("serieFacturaAfectada", ""));
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
               new XElement("telefono", ClienteImprentaDigital.Telefono),
               new XElement("telefono", ""),
               new XElement("correo", ClienteImprentaDigital.Email),
               new XElement("correo", ""));
            return vResult;
        }

        private string DarFormatoYObtenerPrefijoRif(Cliente valCliente, ref string refPrefijoRif) {
            string vNumeroRif = "";
            string vPrefijo = LibString.Left(valCliente.NumeroRIF, 1);
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
            XElement vResult = new XElement("InfoAdicional",
                new XElement("InfoAdicional",
                    new XElement("Posicion", "1,1"),
                    new XElement("Campo", "Observaciones"),
                    new XElement("Valor", FacturaImprentaDigital.Observaciones)),
                new XElement("InfoAdicional",
                    new XElement("Posicion", "2,1"),
                    new XElement("Campo", "Información Adicional"),
                    new XElement("Valor", "")));
            return vResult;
        }
        #endregion InfoAdicional


        #region Impuestos
        private XElement GetTotalImpuestos() {
            XElement vResult = new XElement("impuestosSubtotal",
                new XElement("impuestosSubtotal",
                    new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Exento)),
                    new XElement("AlicuotaImp", 0m),
                    new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalMontoExento, 2))),
                    new XElement("ValorTotalImp", 0m)),
                new XElement("impuestosSubtotal",
                    new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.AlicuotaGeneral)),
                    new XElement("AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota1, 2)),
                    new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota1, 2))),
                    new XElement("ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota1, 2)))),
                new XElement("impuestosSubtotal",
                    new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Alicuota2)),
                    new XElement("AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota2, 2)),
                    new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota2, 2))),
                    new XElement("ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota2, 2)))),
                new XElement("impuestosSubtotal",
                    new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Alicuota3)),
                    new XElement("AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.PorcentajeAlicuota3, 2)),
                    new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoGravableAlicuota3, 2))),
                    new XElement("ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoIvaAlicuota3, 2)))),
                  new XElement("impuestosSubtotal",
                    new XElement("CodigoTotalImp", "P"), // Percibido -> SAW No lo maneja pero JSON lo requiere
                    new XElement("AlicuotaImp", 0m),
                    new XElement("BaseImponibleImp", 0m),
                    new XElement("ValorTotalImp", 0m)),
                new XElement("impuestosSubtotal",
                    new XElement("CodigoTotalImp", "IGTF"),
                    new XElement("AlicuotaImp", LibMath.RoundToNDecimals(FacturaImprentaDigital.AlicuotaIGTF, 2)),
                    new XElement("BaseImponibleImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.BaseImponibleIGTF, 2))),
                    new XElement("ValorTotalImp", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.IGTFML, 2)))));
            return vResult;
        }
        #endregion Impuestos
        #region formas de pago
        private XElement GetFormasPago() {
            // Discutir Funcionalidad
            XElement vResult = new XElement("formasPago");
            string vCodigoMoneda;
            decimal vMonto;
            decimal vMontoML;
            decimal vCambioBs;
            string vFormaDeCobro;
            if (FacturaImprentaDigital.BaseImponibleIGTF > 0) {
                vFormaDeCobro = FacturaImprentaDigital.FormaDeCobroAsEnum == eTipoDeFormaDeCobro.Efectivo ? "09" : "99";
                vCambioBs = LibMath.RoundToNDecimals(FacturaImprentaDigital.CambioABolivares, 2);
                vCodigoMoneda = FacturaImprentaDigital.CodigoMonedaDeCobro;
                vMonto = LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.BaseImponibleIGTF, 2));
                vMontoML = LibMath.Abs(LibMath.RoundToNDecimals((FacturaImprentaDigital.TotalFactura + FacturaImprentaDigital.IGTFML) - vMonto, 2));
                vResult.Add(
                    new XElement("formasPago",
                    new XElement("descripcion", LibEnumHelper.GetDescription(FacturaImprentaDigital.FormaDeCobroAsEnum) + " Divisas"),
                    new XElement("forma", vFormaDeCobro),
                    new XElement("monto", vMonto),
                    new XElement("moneda", vCodigoMoneda),
                    new XElement("tipoCambio", vCambioBs)));
                if (vMontoML > 0) {
                    vResult.Add(
                        new XElement("formasPago",
                        new XElement("descripcion", LibEnumHelper.GetDescription(FacturaImprentaDigital.FormaDeCobroAsEnum)),
                        new XElement("forma", "99"),
                        new XElement("monto", vMontoML),
                        new XElement("moneda", FacturaImprentaDigital.CodigoMoneda),
                        new XElement("tipoCambio", "1.00")));
                }
            } else if (FacturaImprentaDigital.BaseImponibleIGTF == 0) {
                vFormaDeCobro = FacturaImprentaDigital.FormaDeCobroAsEnum == eTipoDeFormaDeCobro.Efectivo ? "08" : "99";
                vCambioBs = LibMath.RoundToNDecimals(FacturaImprentaDigital.CambioABolivares, 2);
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
            if (vResult.Descendants("formasPago").Count() == 1) {
                vResult.Add(new XElement("forma", GetFormaDeCobro(FacturaImprentaDigital.FormaDeCobroAsEnum)),
                     new XElement("formasPago",
                        new XElement("forma", "99"),
                        new XElement("descripcion", ""),
                        new XElement("monto", "0"),
                        new XElement("moneda", "VED"),
                        new XElement("tipoCambio", "1.00")));
            }
            return vResult;
        }
        #endregion formas de pago
        #region Totales
        private XElement GetTotales() {
            // listaDescBonificacion -> otros cargos y descuentos se debe revisar. 
            XElement vResult = new XElement("totales",
               new XElement("nroItems", DetalleFacturaImprentaDigital.Count),
               new XElement("montoGravadoTotal", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalBaseImponible, 2))),
               new XElement("montoExentoTotal", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalMontoExento, 2))),
               new XElement("subtotal", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalBaseImponible + FacturaImprentaDigital.TotalMontoExento, 2))),
               new XElement("totalAPagar", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalFactura, 2))),
               new XElement("totalIVA", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalIVA, 2))),
               new XElement("montoTotalConIVA", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.TotalFactura, 2))),
               new XElement("montoEnLetras", LibConvert.ToNumberInLetters(FacturaImprentaDigital.TotalFactura, false, "")),
               new XElement("totalDescuento", LibMath.Abs(LibMath.RoundToNDecimals(FacturaImprentaDigital.MontoDescuento1, 2))));
            vResult.Add(GetTotalImpuestos().Descendants("impuestosSubtotal"));
            vResult.Add(GetFormasPago().Descendants("formasPago"));
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
                        string vSerial = vDetalle.Serial == "0" ? "" : vDetalle.Serial;
                        string vRollo = vDetalle.Rollo == "0" ? "" : vDetalle.Rollo;
                        vResultInfoAdicional = new XElement("infoAdicionalItem",
                            new XElement("infoAdicionalItem", new XElement("Serial", vSerial)),
                            new XElement("infoAdicionalItem", new XElement("Rollo", vRollo)),
                            new XElement("infoAdicionalItem", new XElement("CampoExtraEnRenglonFactura1", vDetalle.CampoExtraEnRenglonFactura1)),
                            new XElement("infoAdicionalItem", new XElement("CampoExtraEnRenglonFactura2", vDetalle.CampoExtraEnRenglonFactura2)));
                        vResult.Add(
                            new XElement("detallesItems",
                                new XElement("numeroLinea", vDetalle.ConsecutivoRenglon),
                                new XElement("codigoPLU", vDetalle.Articulo),
                                new XElement("indicadorBienoServicio", vDetalle.TipoDeArticuloAsEnum == eTipoDeArticulo.Servicio ? "2" : "1"),
                                new XElement("descripcion", vDetalle.Descripcion),
                                new XElement("cantidad", LibMath.Abs(LibMath.RoundToNDecimals(vDetalle.Cantidad, 2))),
                                new XElement("unidadMedida", "NIU"),
                                new XElement("precioUnitario", LibMath.Abs(LibMath.RoundToNDecimals(vDetalle.PrecioSinIVA, 2))),
                                new XElement("precioItem", LibMath.Abs(LibMath.RoundToNDecimals(vDetalle.PrecioSinIVA * vDetalle.Cantidad, 2))),
                                new XElement("codigoImpuesto", GetAlicuota(vDetalle.AlicuotaIvaAsEnum)),
                                new XElement("tasaIVA", LibMath.RoundToNDecimals(vDetalle.PorcentajeAlicuota, 2)),
                                new XElement("valorIVA", LibMath.Abs(LibMath.RoundToNDecimals(LibMath.RoundToNDecimals((vDetalle.PorcentajeAlicuota * vDetalle.PrecioSinIVA * vDetalle.Cantidad) / 100, 2), 2))),
                                new XElement("valorTotalItem", LibMath.Abs(LibMath.RoundToNDecimals(vDetalle.TotalRenglon, 2))),
                                vResultInfoAdicional.Descendants("infoAdicionalItem")));
                    }
                }
                if (DetalleFacturaImprentaDigital.Count == 1) { //Se Agrega una fila temporal para que al serializar el XML a JSON, se reconozca el detalle como elementos de un array (lista)
                    vResult.Add(
                                new XElement("detallesItems",
                                    new XElement("numeroLinea", "0"),
                                    new XElement("codigoPLU", ""),
                                    new XElement("indicadorBienoServicio", ""),
                                    new XElement("descripcion", ""),
                                    new XElement("cantidad", "1.00"),
                                    new XElement("unidadMedida", ""),
                                    new XElement("precioUnitario", "0"),
                                    new XElement("precioItem", "0"),
                                    new XElement("codigoImpuesto", ""),
                                    new XElement("tasaIVA", "0"),
                                    new XElement("valorIVA", "0"),
                                    new XElement("valorTotalItem", "0")));
                }
            }
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
                    vResult = "05";
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
                    break;
            }
            return vResult;
        }

        private string GetTipoDePago(eFormadePago valTipoDePago) {
            string vResult = "";
            switch (valTipoDePago) {
                case eFormadePago.Contado:
                    vResult = "inmediato";
                    break;
                case eFormadePago.Credito:
                    vResult = "crédito";
                    break;
                default:
                    vResult = "inmediato";
                    break;
            }
            return vResult;
        }       
        #endregion Conversion de Tipos
    }
}


