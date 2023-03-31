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

        #region Metdos Basicos
        public override async Task<bool> SincronizarDocumentos() {
            try {
                bool vResult = false;
                clsConectorJson vConectorJson = new clsConectorJson(LoginUser);
                string vRepuesta = await vConectorJson.CheckConnection();
                if (!LibString.IsNullOrEmpty(vConectorJson.Token)) {
                    string vDocumentoJSON = clsConectorJson.SerializeJSON(""); //Construir XML o JSON Con datos 
                    var vReq = await vConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.EstadoDocumento), vConectorJson.Token);
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }

        public override async Task<bool> EstadoLoteDocumentos() {
            try {
                bool vResult = false;
                clsConectorJson vConectorJson = new clsConectorJson(LoginUser);
                string vRepuesta = await vConectorJson.CheckConnection();
                if (!LibString.IsNullOrEmpty(vConectorJson.Token)) {
                    string vDocumentoJSON = clsConectorJson.SerializeJSON(""); //Construir XML o JSON Con datos 
                    var vReq = await vConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.EstadoLote), vConectorJson.Token);
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }

        public override async Task<bool> EstadoDocumento() {
            try {
                bool vResult = false;
                clsConectorJson vConectorJson = new clsConectorJson(LoginUser);
                string vRepuesta = await vConectorJson.CheckConnection();
                if (!LibString.IsNullOrEmpty(vConectorJson.Token)) {
                    string vDocumentoJSON = clsConectorJson.SerializeJSON(""); //Construir XML o JSON Con datos 
                    var vReq = await vConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.EstadoDocumento), vConectorJson.Token);
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }

        public override async Task<bool> AnularDocumento() {
            try {
                bool vResult = false;
                clsConectorJson vConectorJson = new clsConectorJson(LoginUser);
                string vRepuesta = await vConectorJson.CheckConnection();
                if (!LibString.IsNullOrEmpty(vConectorJson.Token)) {
                    string vDocumentoJSON = clsConectorJson.SerializeJSON(""); //Construir XML o JSON Con datos 
                    var vReq = await vConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Anular), vConectorJson.Token);
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }

        public override async Task<bool> EnviarDocumento() {
            try {
                bool vResult = false;
                clsConectorJson vConectorJson = new clsConectorJson(LoginUser);
                string vRepuesta = await vConectorJson.CheckConnection();
                if (!LibString.IsNullOrEmpty(vConectorJson.Token)) {
                    ConfigurarDocumento();
                    string vDocumentoJSON = clsConectorJson.SerializeJSON(vDocumentoDigital);
                    var vReq = await vConectorJson.SendPostJson(vDocumentoJSON, LibEnumHelper.GetDescription(eComandosPostTheFactoryHKA.Emision), vConectorJson.Token);
                    NumeroControl = vReq.resultados.numeroControl;
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }
        #endregion Metdos Basicos
        #region Construcccion de Documento
        #region Armar  Documento Digital
        public override void ConfigurarDocumento() {
            base.ConfigurarDocumento();
            XElement vIdentificacionDocumento = GetIdentificacionDocumento();
            XElement vVendedor = GetDatosVendedor();
            XElement vComprador = GetDatosComprador();
            XElement vTotales = GetTotales();
            var vDetalleFactura = GetDetalleFactura().Descendants("detallesItems");
            vDocumentoDigital = new XElement("documentoElectronico", new XElement("encabezado"));
            vDocumentoDigital.Element("encabezado").Add(vIdentificacionDocumento);
            vDocumentoDigital.Element("encabezado").Add(vVendedor);
            vDocumentoDigital.Element("encabezado").Add(vComprador);
            vDocumentoDigital.Element("encabezado").Add(vTotales);
            vDocumentoDigital.Add(vDetalleFactura);
        }
        #endregion Construye  Documento
        #region Identificacion de Documento
        private XElement GetIdentificacionDocumento() {
            string vHoraEmision = LibConvert.ToStrOnlyForHour(LibConvert.ToDate(FacturaImprentaDigital.HoraModificacion), "hh:mm:ss tt");
            vHoraEmision = LibString.Replace(vHoraEmision, ". ", "");
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
                vResult.Add(new XElement("numeroFacturaAfectada", FacturaImprentaDigital.NumeroFacturaAfectada));
                vResult.Add(new XElement("serieFacturaAfectada", ""));
                vResult.Add(new XElement("montoFacturaAfectada", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.TotalFactura))));
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
            XElement vResult = new XElement("comprador",
               new XElement("tipoIdentificacion", "V"),
               new XElement("numeroIdentificacion", ClienteImprentaDigital.NumeroRIF),
               new XElement("razonSocial", ClienteImprentaDigital.Nombre),
               new XElement("direccion", ClienteImprentaDigital.Direccion),
               new XElement("pais", "VE"),
               new XElement("telefono", ClienteImprentaDigital.Telefono),
               new XElement("telefono", ""),
               new XElement("correo", ClienteImprentaDigital.Email),
               new XElement("correo", ""));
            return vResult;
        }
        #endregion clientes
        #region Impuestos
        private XElement GetTotalImpuestos() {
            XElement vResult = new XElement("impuestosSubtotal",
                new XElement("impuestosSubtotal",
                    new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Exento)),
                    new XElement("AlicuotaImp", 0m),
                    new XElement("BaseImponibleImp", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.TotalMontoExento))),
                    new XElement("ValorTotalImp", 0m)),
                new XElement("impuestosSubtotal",
                    new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.AlicuotaGeneral)),
                    new XElement("AlicuotaImp", LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.PorcentajeAlicuota1)),
                    new XElement("BaseImponibleImp", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.MontoGravableAlicuota1))),
                    new XElement("ValorTotalImp", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.MontoIvaAlicuota1)))),
                new XElement("impuestosSubtotal",
                    new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Alicuota2)),
                    new XElement("AlicuotaImp", LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.PorcentajeAlicuota2)),
                    new XElement("BaseImponibleImp", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.MontoGravableAlicuota2))),
                    new XElement("ValorTotalImp", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.MontoIvaAlicuota2)))),
                new XElement("impuestosSubtotal",
                    new XElement("CodigoTotalImp", GetAlicuota(eTipoDeAlicuota.Alicuota3)),
                    new XElement("AlicuotaImp", LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.PorcentajeAlicuota3)),
                    new XElement("BaseImponibleImp", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.MontoGravableAlicuota3))),
                    new XElement("ValorTotalImp", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.MontoIvaAlicuota3)))),
                  new XElement("impuestosSubtotal",
                    new XElement("CodigoTotalImp", "P"), // Percibido -> SAW No lo maneja pero JSON lo requiere
                    new XElement("AlicuotaImp", 0m),
                    new XElement("BaseImponibleImp", 0m),
                    new XElement("ValorTotalImp", 0m)),
                new XElement("impuestosSubtotal",
                    new XElement("CodigoTotalImp", "IGTF"),
                    new XElement("AlicuotaImp", LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.AlicuotaIGTF)),
                    new XElement("BaseImponibleImp", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.BaseImponibleIGTF))),
                    new XElement("ValorTotalImp", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.IGTFML)))));
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
                vCambioBs = LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.CambioABolivares);
                vCodigoMoneda = FacturaImprentaDigital.CodigoMonedaDeCobro;
                vMonto = LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.BaseImponibleIGTF));
                vMontoML = LibMath.Abs(LibMath.TruncTo2DecAndRoudCents((FacturaImprentaDigital.TotalFactura + FacturaImprentaDigital.IGTFML) - vMonto));
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
            }
            if (FacturaImprentaDigital.BaseImponibleIGTF == 0) {
                vFormaDeCobro = FacturaImprentaDigital.FormaDeCobroAsEnum == eTipoDeFormaDeCobro.Efectivo ? "08" : "99";
                vCambioBs = LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.CambioABolivares);
                vCodigoMoneda = FacturaImprentaDigital.CodigoMoneda;
                vMonto = LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.TotalFactura));
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
                        new XElement("moneda", "BSD"),
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
               new XElement("montoGravadoTotal", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.TotalBaseImponible))),
               new XElement("montoExentoTotal", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.TotalMontoExento))),
               new XElement("subtotal", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.TotalBaseImponible + FacturaImprentaDigital.TotalMontoExento))),
               new XElement("totalAPagar", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.TotalFactura))),
               new XElement("totalIVA", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.TotalIVA))),
               new XElement("montoTotalConIVA", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.TotalFactura))),
               new XElement("montoEnLetras", LibConvert.ToNumberInLetters(FacturaImprentaDigital.TotalFactura, false, "")),
               new XElement("totalDescuento", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(FacturaImprentaDigital.MontoDescuento1))));
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
            if (DetalleFacturaImprentaDigital != null && DetalleFacturaImprentaDigital.Count > 0) {
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
                            new XElement("codigoPLU", ""),
                            new XElement("indicadorBienoServicio", vDetalle.TipoDeArticuloAsEnum == eTipoDeArticulo.Servicio ? "2" : "1"),
                            new XElement("descripcion", vDetalle.Descripcion),
                            new XElement("cantidad", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(vDetalle.Cantidad))),
                            new XElement("unidadMedida", "NIU"),
                            new XElement("precioUnitario", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(vDetalle.PrecioSinIVA))),
                            new XElement("precioItem", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(vDetalle.PrecioSinIVA * vDetalle.Cantidad))),
                            new XElement("codigoImpuesto", GetAlicuota(vDetalle.AlicuotaIvaAsEnum)),
                            new XElement("tasaIVA", LibMath.TruncTo2DecAndRoudCents(vDetalle.PorcentajeAlicuota)),
                            new XElement("valorIVA", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(LibMath.RoundToNDecimals((vDetalle.PorcentajeAlicuota * vDetalle.PrecioSinIVA * vDetalle.Cantidad) / 100, 2)))),
                            new XElement("valorTotalItem", LibMath.Abs(LibMath.TruncTo2DecAndRoudCents(vDetalle.TotalRenglon))),
                            vResultInfoAdicional.Descendants("infoAdicionalItem")));
                }
            }
            if (DetalleFacturaImprentaDigital.Count == 1) {
                vResult.Add(
                            new XElement("detallesItems",
                                new XElement("numeroLinea", "0"),
                                new XElement("codigoCIIU", ""),
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


