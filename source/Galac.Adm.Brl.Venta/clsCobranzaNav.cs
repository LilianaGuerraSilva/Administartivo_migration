using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.Venta;
using System.Threading;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;
using System.Xml;
using Galac.Comun.Brl.TablasGen;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.Venta {
    public class clsCobranzaNav : LibBaseNav<IList<Cobranza>, IList<Cobranza>>, ICobranzaPdn {

        bool ICobranzaPdn.InsertarCobranzaDesdePuntoDeVenta(int valConsecutivoCompania, XElement valData, string valNumeroCobranza) {
            StringBuilder sql = new StringBuilder();
            QAdvSql vQAdvSql = new QAdvSql("");
            LibGpParams vParams = new LibGpParams();
            bool vResult = false;
            decimal TotalCobradoEfectivo = 0;
            decimal TotalCobradoCheque = 0;
            string vNumeroDelCheque = string.Empty;
            decimal TotalCobradoTarjeta = 0;
            string vNroDeLaTarjeta = string.Empty;
            string vNombreBanco = string.Empty;

            DateTime vFecha = LibConvert.ToDate(LibXml.GetPropertyString(valData, "Fecha"));
            string vCodigoCliente = LibXml.GetPropertyString(valData, "CodigoCliente");
            string vCodigoCobrador = LibXml.GetPropertyString(valData, "CodigoVendedor");
            decimal vTotalFactura = LibImportData.ToDec(LibXml.GetPropertyString(valData, "TotalFactura"), 2);
            decimal vTotalCobrado = vTotalFactura;
            DateTime vFechaUltimaModificacion = LibDate.Today();

            if(valData.Descendants("GpResultDetailRenglonCobro") != null) {
                decimal SumarTotalCobrado = valData.Descendants("GpResultDetailRenglonCobro").Sum(s => LibConvert.ToDec(s.Element("Monto")));
                decimal vVuelto = SumarTotalCobrado - vTotalFactura;

                decimal CobradoEfectivo = valData.Descendants("GpResultDetailRenglonCobro").Where(p => p.Element("CodigoFormaDelCobro").Value == "00001")
                    .Sum(s => LibConvert.ToDec(s.Element("Monto")));
                TotalCobradoEfectivo = CobradoEfectivo - vVuelto;
                if(TotalCobradoEfectivo < 0) {
                    TotalCobradoEfectivo = 0;
                }

                TotalCobradoCheque = valData.Descendants("GpResultDetailRenglonCobro").Where(p => p.Element("CodigoFormaDelCobro").Value == "00002" || p.Element("CodigoFormaDelCobro").Value == "00004")
                    .Sum(s => LibConvert.ToDec(s.Element("Monto")));
                TotalCobradoTarjeta = valData.Descendants("GpResultDetailRenglonCobro").Where(p => p.Element("CodigoFormaDelCobro").Value == "00003")
                .Sum(s => LibConvert.ToDec(s.Element("Monto")));



                var vXmlFormaCobro00002 = valData.Descendants("GpResultDetailRenglonCobro")
                    .Where(p => p.Element("CodigoFormaDelCobro") != null && (LibString.S1IsEqualToS2(p.Element("CodigoFormaDelCobro").Value, "00002") || LibString.S1IsEqualToS2(p.Element("CodigoFormaDelCobro").Value, "00004")) && p.Element("NumeroDelDocumento") != null)
                    .Select(q => new { NumeroDelDocumento = q.Element("NumeroDelDocumento").Value });
                if(vXmlFormaCobro00002 != null) {
                    foreach(var item in vXmlFormaCobro00002) {
                        vNumeroDelCheque = item.NumeroDelDocumento;
                    }
                }

                var vXmlFormaCobro00003 = valData.Descendants("GpResultDetailRenglonCobro")
                    .Where(p => p.Element("CodigoFormaDelCobro") != null && LibString.S1IsEqualToS2(p.Element("CodigoFormaDelCobro").Value, "00003") && p.Element("NumeroDelDocumento") != null)
                    .Select(q => new { NumeroDelDocumento = q.Element("NumeroDelDocumento").Value });
                if(vXmlFormaCobro00003 != null) {
                    foreach(var item in vXmlFormaCobro00003) {
                        vNroDeLaTarjeta = item.NumeroDelDocumento;
                        break;
                    }
                }


                var xmlNombreBanco = valData.Descendants("GpResultDetailRenglonCobro")
                    .Where(p => p.Element("CodigoFormaDelCobro") != null && !LibString.S1IsEqualToS2(p.Element("CodigoFormaDelCobro").Value, "00001") && p.Element("NombreBanco") != null)
                    .Select(q => new {
                        NombreBanco = q.Element("NombreBanco").Value
                    });
                if(xmlNombreBanco != null) {
                    foreach(var item in xmlNombreBanco) {
                        vNombreBanco = item.NombreBanco;
                        break;
                    }
                }


            }
            string vCodigoCuentaBancaria = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "CuentaBancariaCobroDirecto");
            string vConceptoBancario = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "ConceptoBancarioCobroDirecto");
            string vMoneda = LibXml.GetPropertyString(valData, "Moneda");
            decimal vVueltoDelCobroDirecto = LibImportData.ToDec(LibXml.GetPropertyString(valData, "VueltoDelCobroDirecto"));
            string vCodigoMoneda = LibXml.GetPropertyString(valData, "CodigoMoneda");

            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroCobranza", valNumeroCobranza, 12);
            vParams.AddInEnum("StatusCobranza", (int)eStatusCobranza.Vigente);
            vParams.AddInDateTime("Fecha", vFecha);
            vParams.AddInDateTime("FechaAnulacion", vFecha);
            vParams.AddInString("CodigoCliente", vCodigoCliente, 10);
            vParams.AddInString("CodigoCobrador", vCodigoCobrador, 5);
            vParams.AddInDecimal("TotalFactura", vTotalFactura, 2);
            vParams.AddInDecimal("RetencionIslr", 0, 2);
            vParams.AddInDecimal("SumarTotalCobrado", vTotalCobrado, 2);
            vParams.AddInDecimal("TotalCobradoEfectivo", TotalCobradoEfectivo, 2);
            vParams.AddInDecimal("TotalCobradoCheque", TotalCobradoCheque, 2);
            vParams.AddInString("NumeroDelCheque", vNumeroDelCheque, 10);
            vParams.AddInDecimal("TotalCobradoTarjeta", TotalCobradoTarjeta, 2);
            vParams.AddInString("CualTarjeta", string.Empty, 1);
            vParams.AddInString("NroDeLaTarjeta", vNroDeLaTarjeta.ToString(), 20);
            vParams.AddInEnum("OrigenFacturacionOManual", (int)eOrigenFacturacionOManual.Factura);
            vParams.AddInDecimal("TotalOtrosCargos", 0, 2);
            vParams.AddInString("NombreBanco", vNombreBanco, 20);
            vParams.AddInString("CodigoCuentaBancaria", vCodigoCuentaBancaria, 5);
            vParams.AddInString("ConceptoBancario", vConceptoBancario, 8);
            vParams.AddInString("Moneda", vMoneda, 80);
            vParams.AddInDecimal("CambioAbolivares", 1, 2);
            vParams.AddInDecimal("RetencionIva", 0, 2);
            vParams.AddInString("NroComprobanteRetIva", string.Empty, 20);
            vParams.AddInEnum("StatusRetencionIva", (int)eStatusRetencionIVACobranza.NoAplica);
            vParams.AddInBoolean("GeneraMovBancario", true);
            vParams.AddInDecimal("CobradoAnticipo", 0, 2);
            vParams.AddInDecimal("VueltoDelCobroDirecto", vVueltoDelCobroDirecto, 2);
            vParams.AddInDecimal("DescProntoPago", 0, 2);
            vParams.AddInDecimal("DescProntoPagoPorc", 0, 2);
            vParams.AddInDecimal("ComisionVendedor", 0, 2);
            vParams.AddInBoolean("AplicaCreditoBancario", true);
            vParams.AddInString("CodigoMoneda", vCodigoMoneda, 4);
            vParams.AddInInteger("NumeroDeComprobanteISLR", 0);
            vParams.AddInEnum("TipoDeDocumento", (int)eTipoDeDocumentoCobranza.CobranzaDeFactura);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", vFechaUltimaModificacion);

            sql.AppendLine(" INSERT INTO Cobranza (");
            sql.AppendLine("ConsecutivoCompania");
            sql.AppendLine(", Numero");
            sql.AppendLine(", StatusCobranza");
            sql.AppendLine(", Fecha");
            sql.AppendLine(", FechaAnulacion ");
            sql.AppendLine(", CodigoCliente");
            sql.AppendLine(", CodigoCobrador");
            sql.AppendLine(", TotalDocumentos ");
            sql.AppendLine(", RetencionIslr ");
            sql.AppendLine(", TotalCobrado ");
            sql.AppendLine(", CobradoEfectivo ");
            sql.AppendLine(", CobradoCheque");
            sql.AppendLine(", NumerodelCheque ");
            sql.AppendLine(", CobradoTarjeta");
            sql.AppendLine(", CualTarjeta");
            sql.AppendLine(", NroDeLaTarjeta ");
            sql.AppendLine(", Origen");
            sql.AppendLine(", TotalOtros ");
            sql.AppendLine(", NombreBanco");
            sql.AppendLine(", CodigoCuentaBancaria");
            sql.AppendLine(", CodigoConcepto ");
            sql.AppendLine(", Moneda");
            sql.AppendLine(", CambioAbolivares ");
            sql.AppendLine(", RetencionIva");
            sql.AppendLine(", NroComprobanteRetIva");
            sql.AppendLine(", StatusRetencionIva");
            sql.AppendLine(", GeneraMovBancario");
            sql.AppendLine(", CobradoAnticipo");
            sql.AppendLine(", Vuelto");
            sql.AppendLine(", DescProntoPago");
            sql.AppendLine(", DescProntoPagoPorc");
            sql.AppendLine(", ComisionVendedor");
            sql.AppendLine(", AplicaCreditoBancario");
            sql.AppendLine(", CodigoMoneda, NumeroDeComprobanteISLR");
            sql.AppendLine(", TipoDeDocumento");
            sql.AppendLine(", NombreOperador");
            sql.AppendLine(", FechaUltimaModificacion) ");
            sql.AppendLine(" VALUES (");
            sql.AppendLine(" @ConsecutivoCompania");
            sql.AppendLine(", @NumeroCobranza");
            sql.AppendLine(", @StatusCobranza");
            sql.AppendLine(", @Fecha");
            sql.AppendLine(", @FechaAnulacion");
            sql.AppendLine(", @CodigoCliente");
            sql.AppendLine(", @CodigoCobrador");
            sql.AppendLine(", @TotalFactura");
            sql.AppendLine(", @RetencionIslr ");
            sql.AppendLine(", @SumarTotalCobrado");
            sql.AppendLine(", @TotalCobradoEfectivo");
            sql.AppendLine(", @TotalCobradoCheque");
            sql.AppendLine(", @NumeroDelCheque");
            sql.AppendLine(", @TotalCobradoTarjeta");
            sql.AppendLine(", @CualTarjeta");
            sql.AppendLine(", @NroDeLaTarjeta");
            sql.AppendLine(", @OrigenFacturacionOManual");
            sql.AppendLine(", @TotalOtrosCargos");
            sql.AppendLine(", @NombreBanco");
            sql.AppendLine(", @CodigoCuentaBancaria");
            sql.AppendLine(", @ConceptoBancario");
            sql.AppendLine(", @Moneda");
            sql.AppendLine(", @CambioAbolivares");
            sql.AppendLine(", @RetencionIva");
            sql.AppendLine(", @NroComprobanteRetIva");
            sql.AppendLine(", @StatusRetencionIva");
            sql.AppendLine(", @GeneraMovBancario");
            sql.AppendLine(", @CobradoAnticipo");
            sql.AppendLine(", @VueltoDelCobroDirecto");
            sql.AppendLine(", @DescProntoPago");
            sql.AppendLine(", @DescProntoPagoPorc");
            sql.AppendLine(", @ComisionVendedor");
            sql.AppendLine(", @AplicaCreditoBancario");
            sql.AppendLine(", @CodigoMoneda");
            sql.AppendLine(", @NumeroDeComprobanteISLR");
            sql.AppendLine(", @TipoDeDocumento");
            sql.AppendLine(", @NombreOperador");
            sql.AppendLine(", @FechaUltimaModificacion");
            sql.AppendLine(")");
            vResult = (LibBusiness.ExecuteUpdateOrDelete(sql.ToString(), vParams.Get(), string.Empty, 0) >= 0);
            return vResult;

        }

        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Dbo.Dal.Venta.clsCobranzaDat();
            switch(valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCobranzaDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Dbo.Gp_CobranzaSCH", valXmlParamsExpression);
        }

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch(valModule) {
                case "Vendedor":
                    vPdnModule = new Galac.Saw.Brl.Vendedor.clsVendedorNav();
                    vResult = vPdnModule.GetDataForList("Cobranzas", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cliente":
                    vPdnModule = new Galac.Saw.Brl.Cliente.clsClienteNav();
                    vResult = vPdnModule.GetDataForList("Cobranzas", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "CuentaBancaria":
                    vPdnModule = new Galac.Adm.Brl.Banco.clsCuentaBancariaNav();
                    vResult = vPdnModule.GetDataForList("Cobranzas", ref refXmlDocument, valXmlParamsExpression);
                    break;
            }
            return vResult;
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<Cobranza>, IList<Cobranza>> instanciaDal = new Galac.Adm.Dal.Venta.clsCobranzaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Dbo.Gp_CobranzaGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn


        bool ICobranzaPdn.InsertarDocumentoCobradoDesdePuntoDeVenta(int valConsecutivoCompania, XElement valData, string valNumeroCobranza) {
            StringBuilder sql = new StringBuilder();
            QAdvSql vQAdvSql = new QAdvSql("");
            LibGpParams vParams = new LibGpParams();
            bool vResult = false;

            decimal vTotalFactura = LibImportData.ToDec(LibXml.GetPropertyString(valData, "TotalFactura"), 2);
            string vNumeroDelDocumento = LibXml.GetPropertyString(valData, "Numero");
            string vMoneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "Moneda");
            DateTime vFecha = LibConvert.ToDate(LibXml.GetPropertyString(valData, "Fecha"));
            string vCodigoMoneda = LibXml.GetPropertyString(valData, "CodigoMoneda");

            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroCobranza", valNumeroCobranza, 12);
            vParams.AddInString("NumeroDelDocumentoCobrado", vNumeroDelDocumento, 12);
            vParams.AddInEnum("TipoDeDocumentoCobrado", (int)eTipoDeTransaccion.TICKETMAQUINAREGISTRADORA);
            vParams.AddInDecimal("MontoOriginalRestanteAlDiaDelc", vTotalFactura, 2);
            vParams.AddInDecimal("MontoAbonado", vTotalFactura, 2);
            vParams.AddInString("SimboloMonedaDeCxC", "Bs", 4);
            vParams.AddInDecimal("MontoTotalDeCxC", vTotalFactura, 2);
            vParams.AddInString("CodigoMonedaDeCxC", vCodigoMoneda, 12);
            vParams.AddInDecimal("CambioAMonedaDeCobranza", 1, 2);
            vParams.AddInDecimal("MontoEnMonedaOriginalDeCxC", vTotalFactura, 2);
            vParams.AddInDecimal("MontoIvaDeCxC", 0, 2);
            vParams.AddInDecimal("MontoIvaRetenido", 0, 2);
            vParams.AddInInteger("NumeroComprobanteRetIva", 0);
            vParams.AddInDateTime("FechaComprobanteRetIva", vFecha);
            vParams.AddInBoolean("SeRetuvoIva", false);
            vParams.AddInBoolean("SeContabilizoIvaDiferido", false);
            vParams.AddInDecimal("MontoAbonadoEnMonedaOriginal", vTotalFactura, 2);
            vParams.AddInDecimal("CambioAMonedaLocal", 1, 2);
            vParams.AddInString("SimboloMonedaDelAbono", new clsNoComunSaw().InstanceMonedaLocalActual.GetHoySimboloMoneda(), 2);

            string vFechaComprobanteRetIva = LibXml.GetPropertyString(valData, "Fecha");
            sql.AppendLine(" INSERT INTO DocumentoCobrado (");
            sql.AppendLine(" ConsecutivoCompania");
            sql.AppendLine(", NumeroCobranza");
            sql.AppendLine(", NumeroDelDocumentoCobrado");
            sql.AppendLine(", TipoDeDocumentoCobrado");
            sql.AppendLine(", MontoOriginalRestanteAlDiaDelc");
            sql.AppendLine(", MontoAbonado");
            sql.AppendLine(", SimboloMonedaDeCxC");
            sql.AppendLine(", MontoTotalDeCxC");
            sql.AppendLine(", CodigoMonedaDeCxC");
            sql.AppendLine(", CambioAMonedaDeCobranza");
            sql.AppendLine(", MontoEnMonedaOriginalDeCxC");
            sql.AppendLine(", MontoIvaDeCxC");
            sql.AppendLine(", MontoIvaRetenido");
            sql.AppendLine(", NumeroComprobanteRetIva");
            sql.AppendLine(", FechaComprobanteRetIva");
            sql.AppendLine(", SeRetuvoIva");
            sql.AppendLine(", SeContabilizoIvaDiferido");
            sql.AppendLine(", MontoAbonadoEnMonedaOriginal");
            sql.AppendLine(", CambioAMonedaLocal");
            sql.AppendLine(", SimboloMonedaDelAbono) ");
            sql.AppendLine(" VALUES(");
            sql.AppendLine(" @ConsecutivoCompania ");
            sql.AppendLine(", @NumeroCobranza");
            sql.AppendLine(", @NumeroDelDocumentoCobrado");
            sql.AppendLine(", @TipoDeDocumentoCobrado");
            sql.AppendLine(", @MontoOriginalRestanteAlDiaDelc");
            sql.AppendLine(", @MontoAbonado");
            sql.AppendLine(", @SimboloMonedaDeCxC");
            sql.AppendLine(", @MontoTotalDeCxC");
            sql.AppendLine(", @CodigoMonedaDeCxC");
            sql.AppendLine(", @CambioAMonedaDeCobranza");
            sql.AppendLine(", @MontoEnMonedaOriginalDeCxC");
            sql.AppendLine(", @MontoIvaDeCxC");
            sql.AppendLine(", @MontoIvaRetenido");
            sql.AppendLine(", @NumeroComprobanteRetIva");
            sql.AppendLine(", @FechaComprobanteRetIva");
            sql.AppendLine(", @SeRetuvoIva");
            sql.AppendLine(", @SeContabilizoIvaDiferido");
            sql.AppendLine(", @MontoAbonadoEnMonedaOriginal");
            sql.AppendLine(", @CambioAMonedaLocal");
            sql.AppendLine(", @SimboloMonedaDelAbono");
            sql.AppendLine(")");
            vResult = (LibBusiness.ExecuteUpdateOrDelete(sql.ToString(), vParams.Get(), string.Empty, 0) >= 0);
            return vResult;
        }

        string ICobranzaPdn.GenerarProximoNumeroCobranza(int valConsecutivoCompania) {
            StringBuilder sql = new StringBuilder();
            QAdvSql vQAdvSql = new QAdvSql("");
            LibGpParams vParams = new LibGpParams();
            XElement vResultSet;
            int vNumeroMaximo = 0;
            string vResult;
            string Comodin = "D%";
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Numero", Comodin, 12);
            sql.AppendLine("SELECT MAX(Numero) AS Maximo FROM Cobranza ");
            sql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
            sql.AppendLine(" AND Numero LIKE @Numero");

            vResultSet = LibBusiness.ExecuteSelect(sql.ToString(), vParams.Get(), string.Empty, 0);
            if(vResultSet != null) {
                string vNumero = LibXml.GetPropertyString(vResultSet, "Maximo");
                int vPosicion = LibText.InStr(vNumero, "-") + 1;
                vNumeroMaximo = LibConvert.ToInt(LibText.Mid(vNumero, vPosicion)) + 1;
            } else {
                vNumeroMaximo = 1;
            }
            string UltimoNumero = LibText.FillWithCharToLeft(vNumeroMaximo.ToString(), "0", 6);
            return vResult = SiglasDelCobroDirecto() + UltimoNumero;
        }

        string ICobranzaPdn.GenerarSiguienteNumeroDeCobranzaAPartirDe(string valNumeroDeCobranza) {
            int vNumeroMaximo = 0;
            string vResult;
            string vNumero = valNumeroDeCobranza;
            int vPosicion = LibText.InStr(vNumero, "-") + 1;
            vNumeroMaximo = LibConvert.ToInt(LibText.Mid(vNumero, vPosicion)) + 1;
            string UltimoNumero = LibText.FillWithCharToLeft(vNumeroMaximo.ToString(), "0", 6);
            return vResult = SiglasDelCobroDirecto() + UltimoNumero;
        }

        private string SiglasDelCobroDirecto() {
            string vResult = "D" + SiglasDelCobroDirectoMesYAno() + "-";
            return vResult;
        }

        private string SiglasDelCobroDirectoMesYAno() {
            string Year = LibText.Right(LibConvert.ToStr(LibDate.Today().Year), 2);
            string Month = LibText.FillWithCharToLeft(LibConvert.ToStr(LibDate.Today().Month), "0", 2);
            string vResult = Year + Month;
            return vResult;
        }

        void ICobranzaPdn.InsertarCobranzasDeCobroEnMultimoneda(int valConsecutivoCompania, XElement valDatosFactura, XElement valDatosRenglonCobro, XElement valDataCxC, out List<string> outNumerosDeCobranzas) {
            DateTime vFecha = LibConvert.ToDate(LibXml.GetPropertyString(valDatosFactura, "Fecha"));
            string vCodigoCliente = LibXml.GetPropertyString(valDatosFactura, "CodigoCliente");
            string vCodigoCobrador = LibXml.GetPropertyString(valDatosFactura, "CodigoVendedor");
            string vNumeroFactura = LibXml.GetPropertyString(valDatosFactura, "Numero");
            int vTipoDeCXC = LibImportData.ToInt(LibXml.GetPropertyString(valDataCxC, "TipoCxc"));
            string vCodigoMonedaFactura = LibXml.GetPropertyString(valDatosFactura, "CodigoMoneda");
            string vCodigoMonedaLocal = new Saw.Lib.clsNoComunSaw().InstanceMonedaLocalActual.GetHoyCodigoMoneda();
            DateTime vFechaUltimaModificacion = LibDate.Today();
            string refNumeroDeCobranza = "";
            decimal vTotalCobrado = 0;
            decimal vTotalPorCobrar = 0;
            decimal vCambioAMonedaLocal = 1;
            decimal vTotalFactura = 0;
            decimal vTotalFacturaEnDivisas = 0;
            string vCodigoMonedaCobranza = string.Empty;
            if(vCodigoMonedaFactura == vCodigoMonedaLocal) {
                vTotalFactura = LibImportData.ToDec(LibXml.GetPropertyString(valDatosFactura, "TotalFactura"), 2);
                vTotalPorCobrar = vTotalFactura;
            } else {
                vTotalFacturaEnDivisas = LibImportData.ToDec(LibXml.GetPropertyString(valDatosFactura, "TotalFactura"), 2);
                vCambioAMonedaLocal = LibImportData.ToDec(LibXml.GetPropertyString(valDatosFactura, "CambioABolivares"), 4);
                vTotalFactura = LibMath.RoundToNDecimals((vTotalFacturaEnDivisas * vCambioAMonedaLocal), 2);
                vTotalPorCobrar = vTotalFacturaEnDivisas;
            }
            List<string> vNumeroDeCobranzas = new List<string>();

            var vRenglonesDeCobroPorMoneda = valDatosRenglonCobro.Descendants("GpResult")
                                        .GroupBy(renglon => renglon.Element("CodigoMoneda").Value)
                                        .ToDictionary(t => t.Key, t => t.ToList());

            IEnumerable<XElement> vRenglonesEnMonedaLocal = null;
            IEnumerable<XElement> vRenglonesEnDivisa = null;
            if(vRenglonesDeCobroPorMoneda.ContainsKey(vCodigoMonedaLocal)) {
                vRenglonesEnMonedaLocal = vRenglonesDeCobroPorMoneda[vCodigoMonedaLocal];
            }
            string vCodigoMonedaDivisa = vRenglonesDeCobroPorMoneda.Keys.FirstOrDefault(t => t != vCodigoMonedaLocal);
            if(vCodigoMonedaDivisa != null) {
                vRenglonesEnDivisa = vRenglonesDeCobroPorMoneda[vCodigoMonedaDivisa];
                if(vCodigoMonedaFactura == vCodigoMonedaLocal) {
                    vCambioAMonedaLocal = LibImportData.ToDec(vRenglonesDeCobroPorMoneda[vRenglonesDeCobroPorMoneda.Keys.FirstOrDefault(t => t != vCodigoMonedaLocal)].FirstOrDefault().Element("CambioAMonedaLocal").Value);
                    vTotalFacturaEnDivisas = LibMath.RoundToNDecimals((vTotalFactura / vCambioAMonedaLocal), 2);
                }
            }
            if(vRenglonesEnDivisa != null) {
                vCodigoMonedaCobranza = vRenglonesEnDivisa.Select(s => (string)s.Element("CodigoMoneda")).FirstOrDefault();
                InsertarCobranza(CrearXmlDeDatosCobranza(valConsecutivoCompania, vFecha, vCodigoCliente, vCodigoCobrador, vTotalFacturaEnDivisas, vTotalFacturaEnDivisas, vRenglonesEnDivisa, ref refNumeroDeCobranza, out vTotalCobrado));
                InsertarDocumentoCobrado(CrearXmlDeDatosDocumentoCobrado(valConsecutivoCompania, refNumeroDeCobranza, vNumeroFactura, vTotalFactura, vTotalFacturaEnDivisas, vCambioAMonedaLocal, vFecha, vTotalCobrado, valDataCxC, vCodigoMonedaCobranza));
                vNumeroDeCobranzas.Add(refNumeroDeCobranza);
                vTotalPorCobrar = vTotalFactura - vTotalCobrado * vCambioAMonedaLocal;
            }
            if(vRenglonesEnMonedaLocal != null) {
                vCambioAMonedaLocal = 1;
                vCodigoMonedaCobranza = new clsNoComunSaw().InstanceMonedaLocalActual.GetHoyCodigoMoneda();
                InsertarCobranza(CrearXmlDeDatosCobranza(valConsecutivoCompania, vFecha, vCodigoCliente, vCodigoCobrador, vTotalFactura, vTotalPorCobrar, vRenglonesEnMonedaLocal, ref refNumeroDeCobranza, out vTotalCobrado));
                InsertarDocumentoCobrado(CrearXmlDeDatosDocumentoCobrado(valConsecutivoCompania, refNumeroDeCobranza, vNumeroFactura, vTotalFactura, vTotalFacturaEnDivisas, vCambioAMonedaLocal, vFecha, vTotalCobrado, valDataCxC, vCodigoMonedaCobranza));
                vNumeroDeCobranzas.Add(refNumeroDeCobranza);
            }
            if(vCodigoMonedaFactura == vCodigoMonedaLocal) {
                ((ICXCPdn)new clsCXCNav()).CambiarStatusDeCxcACancelada(valConsecutivoCompania, vNumeroFactura, vTipoDeCXC, vTotalFactura);
            } else {
                ((ICXCPdn)new clsCXCNav()).CambiarStatusDeCxcACancelada(valConsecutivoCompania, vNumeroFactura, vTipoDeCXC, vTotalFacturaEnDivisas);
            }
            outNumerosDeCobranzas = vNumeroDeCobranzas;
        }

        private XElement CrearXmlDeDatosCobranza(int valConsecutivoCompania, DateTime valFecha, string valCodigoCliente, string valCodigoCobrador, decimal valTotalFactura, decimal valTotalPorCobrar, IEnumerable<XElement> valRenglonesDeCobro, ref string refNumeroDeCobranza, out decimal outTotalCobrado) {
            if(refNumeroDeCobranza == string.Empty) {
                refNumeroDeCobranza = ((ICobranzaPdn)new clsCobranzaNav()).GenerarProximoNumeroCobranza(valConsecutivoCompania);
            } else {
                refNumeroDeCobranza = ((ICobranzaPdn)new clsCobranzaNav()).GenerarSiguienteNumeroDeCobranzaAPartirDe(refNumeroDeCobranza);
            }
            decimal vTotalRenglones = valRenglonesDeCobro.Sum(t => LibImportData.ToDec(t.Element("Monto").Value));
            decimal vVuelto = vTotalRenglones - valTotalPorCobrar;
            outTotalCobrado = vTotalRenglones >= valTotalPorCobrar ? vTotalRenglones - vVuelto : vTotalRenglones;

            decimal vCobradoEfectivo = valRenglonesDeCobro.Where(w => w.Element("CodigoFormaDelCobro").Value == "00001").Sum(s => LibImportData.ToDec(s.Element("Monto").Value));
            vCobradoEfectivo = vVuelto > 0 ? (vCobradoEfectivo - vVuelto) : vCobradoEfectivo;
            decimal vCobradoTarjetas = valRenglonesDeCobro.Where(w => w.Element("CodigoFormaDelCobro").Value == "00003").Sum(s => LibImportData.ToDec(s.Element("Monto").Value));
            decimal vCobradoTransferencia = valRenglonesDeCobro.Where(w => w.Element("CodigoFormaDelCobro").Value == "00006").Sum(s => LibImportData.ToDec(s.Element("Monto").Value));
            string vCodigoMoneda = valRenglonesDeCobro.Select(s => (string)s.Element("CodigoMoneda")).FirstOrDefault();
            XElement vSimboloYNombreMoneda = ObtenerSimboloYNombreMonedaDesdeCodigo(vCodigoMoneda);
            string vNombreMoneda = vSimboloYNombreMoneda.Descendants("GpResult").Select(s => s.Element("Nombre").Value).FirstOrDefault();
            decimal vCambioAMonedaLocal = valRenglonesDeCobro.Select(s => (decimal)s.Element("CambioAMonedaLocal")).FirstOrDefault();
            string vCodigoCuentaBancaria = "";
            string vConceptoBancario = "";
            if(vCambioAMonedaLocal > 1) {
                vCodigoCuentaBancaria = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CuentaBancariaCobroMultimoneda");
                vConceptoBancario = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ConceptoBancarioCobroMultimoneda");
            } else {
                vCodigoCuentaBancaria = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CuentaBancariaCobroDirecto");
                vConceptoBancario = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ConceptoBancarioCobroDirecto");
            }
            XElement vXmlCobranza = new XElement("GpData");
            vXmlCobranza.Add(new XElement("GpResult",
                new XElement("ConsecutivoCompania", valConsecutivoCompania),
                new XElement("NumeroCobranza", refNumeroDeCobranza),
                new XElement("StatusCobranza", (int)eStatusCobranza.Vigente),
                new XElement("Fecha", valFecha),
                new XElement("FechaAnulacion", valFecha),
                new XElement("FechaAnulacion", valFecha),
                new XElement("CodigoCliente", valCodigoCliente),
                new XElement("CodigoCobrador", valCodigoCobrador),
                new XElement("TotalDocumentos", outTotalCobrado),
                new XElement("RetencionIslr", 0),
                new XElement("TotalCobrado", outTotalCobrado),
                new XElement("CobradoEfectivo", vCobradoEfectivo),
                new XElement("CobradoCheque", vCobradoTransferencia),
                new XElement("NumeroDelCheque", string.Empty),
                new XElement("CobradoTarjetas", vCobradoTarjetas),
                new XElement("CualTarjeta", string.Empty),
                new XElement("NroDeLaTarjeta", string.Empty),
                new XElement("Origen", (int)eOrigenFacturacionOManual.Factura),
                new XElement("TotalOtros", 0),
                new XElement("NombreBanco", string.Empty),
                new XElement("CodigoCuentaBancaria", vCodigoCuentaBancaria),
                new XElement("CodigoConcepto", vConceptoBancario),
                new XElement("Moneda", vNombreMoneda),
                new XElement("CambioAMonedaLocal", vCambioAMonedaLocal),
                new XElement("RetencionIva", 0),
                new XElement("NroComprobanteRetIva", string.Empty),
                new XElement("StatusRetencionIva", (int)eStatusRetencionIVACobranza.NoAplica),
                new XElement("GeneraMovBancario", true),
                new XElement("CobradoAnticipo", 0),
                new XElement("Vuelto", 0),
                new XElement("DescProntoPago", 0),
                new XElement("DescProntoPagoPorc", 0),
                new XElement("ComisionVendedor", 0),
                new XElement("AplicaCreditoBancario", true),
                new XElement("CodigoMoneda", vCodigoMoneda),
                new XElement("NumeroDeComprobanteISLR", 0),
                new XElement("TipoDeDocumento", (int)eTipoDeDocumentoCobranza.CobranzaDeFactura),
                new XElement("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login)));
            return vXmlCobranza;
        }

        private XElement CrearXmlDeDatosDocumentoCobrado(int valConsecutivoCompania, string valNumeroCobranza, string NumeroDocumentoCobrado, decimal valTotalFactura, decimal valTotalFacturaEnDivisa, decimal valCambioAMonedaDeLaCobranza, DateTime valFecha, decimal valTotalCobrado, XElement valDataCXC, string valCodigoMonedaCobranza) {
            decimal vMontoOriginalRestanteAlDiaDelC = 0;
            decimal vMontoTotalDeCxC = 0;
            decimal vMontoEnMonedaOriginalDeCxC = 0;
            decimal vMontoAbonadoEnMonedaOriginal = 0;
            decimal vCambioAMonedaLocal = 0;
            string vSimboloMonedaDelAbono = string.Empty;
            string vSimboloMonedaCxC = string.Empty;
            string vCodigoMonedaCxC = LibXml.GetPropertyString(valDataCXC, "CodigoMoneda");
            string vCodigoMonedaLocal = new clsNoComunSaw().InstanceMonedaLocalActual.GetHoyCodigoMoneda();
            int vTipoDocumentoCobrado = LibImportData.ToInt(LibXml.GetPropertyString(valDataCXC, "TipoCxc"));
            vSimboloMonedaCxC = ObtenerSimboloYNombreMonedaDesdeCodigo(vCodigoMonedaCxC).Descendants("GpResult").Select(s => s.Element("Simbolo").Value).FirstOrDefault();
            if(vCodigoMonedaCxC == vCodigoMonedaLocal) {
                vMontoOriginalRestanteAlDiaDelC = valCambioAMonedaDeLaCobranza > 1 ? valTotalFactura : (valTotalFactura - valTotalCobrado) == 0 ? valTotalFactura : valTotalCobrado;
                vMontoTotalDeCxC = valCambioAMonedaDeLaCobranza > 1 ? LibMath.RoundToNDecimals((vMontoOriginalRestanteAlDiaDelC / valCambioAMonedaDeLaCobranza), 2) : vMontoOriginalRestanteAlDiaDelC;
                vMontoEnMonedaOriginalDeCxC = valTotalFactura;
            } else {
                vMontoOriginalRestanteAlDiaDelC = valCambioAMonedaDeLaCobranza > 1 ? valTotalFacturaEnDivisa : (valTotalFacturaEnDivisa - valTotalCobrado) == 0 ? valTotalFacturaEnDivisa : valTotalCobrado;
                vMontoTotalDeCxC = valCambioAMonedaDeLaCobranza == 1 ? LibMath.RoundToNDecimals((vMontoOriginalRestanteAlDiaDelC * valCambioAMonedaDeLaCobranza), 2) : vMontoOriginalRestanteAlDiaDelC;
                vMontoEnMonedaOriginalDeCxC = valTotalFacturaEnDivisa;
            }
            vMontoAbonadoEnMonedaOriginal = vCodigoMonedaCxC == valCodigoMonedaCobranza ? valTotalCobrado : LibMath.RoundToNDecimals((valTotalCobrado * valCambioAMonedaDeLaCobranza), 2);
            vCambioAMonedaLocal =  LibImportData.ToDec(LibXml.GetPropertyString(valDataCXC, "CambioABolivares"));
            vSimboloMonedaDelAbono = valCodigoMonedaCobranza == vCodigoMonedaLocal ? new clsNoComunSaw().InstanceMonedaLocalActual.GetHoySimboloMoneda()
                : ObtenerSimboloYNombreMonedaDesdeCodigo(valCodigoMonedaCobranza).Descendants("GpResult").Select(s => s.Element("Simbolo").Value).FirstOrDefault();
            XElement vXmlDocumentoCobrado = new XElement("GpData");
            vXmlDocumentoCobrado.Add(new XElement("GpResult",
                new XElement("ConsecutivoCompania", valConsecutivoCompania),
                new XElement("NumeroCobranza", valNumeroCobranza),
                new XElement("NumeroDelDocumentoCobrado", NumeroDocumentoCobrado),
                new XElement("TipoDeDocumentoCobrado", vTipoDocumentoCobrado),
                new XElement("MontoOriginalRestanteAlDiaDelc", vMontoOriginalRestanteAlDiaDelC),
                new XElement("MontoAbonado", valTotalCobrado),
                new XElement("SimboloMonedaDeCxC", vSimboloMonedaCxC),
                new XElement("MontoTotalDeCxC", vMontoTotalDeCxC),
                new XElement("CodigoMonedaDeCxC", vCodigoMonedaCxC),
                new XElement("CambioAMonedaDeCobranza", valCambioAMonedaDeLaCobranza),
                new XElement("MontoEnMonedaOriginalDeCxC", vMontoEnMonedaOriginalDeCxC),
                new XElement("MontoIvaDeCxC", 0),
                new XElement("MontoIvaRetenido", 0),
                new XElement("NumeroComprobanteRetIva", 0),
                new XElement("FechaComprobanteRetIva", valFecha),
                new XElement("SeRetuvoIva", "N"),
                new XElement("SeContabilizoIvaDiferido", "N"),
                new XElement("MontoAbonadoEnMonedaOriginal", vMontoAbonadoEnMonedaOriginal),
                new XElement("CambioAMonedaLocal", vCambioAMonedaLocal),
                new XElement("SimboloMonedaDelAbono", vSimboloMonedaDelAbono)));
            return vXmlDocumentoCobrado;
        }

        void ICobranzaPdn.InsertarCobranzaDeNotaDeCredito(int valConsecutivoCompania, XElement valDatosFactura, XElement valDatosRenglonCobro, eTipoDeTransaccion valTipoDeCxc, out string outNumeroDeCobranza) {
            outNumeroDeCobranza = ((ICobranzaPdn)new clsCobranzaNav()).GenerarProximoNumeroCobranza(valConsecutivoCompania);
            DateTime vFecha = LibDate.Today();
            string vCodigoCliente = LibXml.GetPropertyString(valDatosFactura, "CodigoCliente");
            string vCodigoCobrador = LibXml.GetPropertyString(valDatosFactura, "CodigoVendedor");
            string vNumeroFactura = LibXml.GetPropertyString(valDatosFactura, "Numero");
            decimal vTotalFactura = LibImportData.ToDec(LibXml.GetPropertyString(valDatosFactura, "TotalFactura"));
            decimal vTotalCobrado = LibImportData.ToDec(LibXml.GetPropertyString(valDatosRenglonCobro, "Monto"));
            string vCodigoCuentaBancaria = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CuentaBancariaCobroDirecto");
            string vConceptoBancario = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ConceptoBancarioCobroDirecto");
            XElement vXmlCobranza = new XElement("GpData");
            vXmlCobranza.Add(new XElement("GpResult",
                new XElement("ConsecutivoCompania", valConsecutivoCompania),
                new XElement("NumeroCobranza", outNumeroDeCobranza),
                new XElement("StatusCobranza", (int)eStatusCobranza.Vigente),
                new XElement("Fecha", vFecha),
                new XElement("FechaAnulacion", vFecha),
                new XElement("FechaAnulacion", vFecha),
                new XElement("CodigoCliente", vCodigoCliente),
                new XElement("CodigoCobrador", vCodigoCobrador),
                new XElement("TotalDocumentos", vTotalCobrado),
                new XElement("RetencionIslr", 0),
                new XElement("TotalCobrado", vTotalCobrado),
                new XElement("CobradoEfectivo", 0),
                new XElement("CobradoCheque", 0),
                new XElement("NumeroDelCheque", string.Empty),
                new XElement("CobradoTarjetas", 0),
                new XElement("CualTarjeta", string.Empty),
                new XElement("NroDeLaTarjeta", string.Empty),
                new XElement("Origen", (int)eOrigenFacturacionOManual.Factura),
                new XElement("TotalOtros", 0),
                new XElement("NombreBanco", string.Empty),
                new XElement("CodigoCuentaBancaria", vCodigoCuentaBancaria),
                new XElement("CodigoConcepto", vConceptoBancario),
                new XElement("Moneda", "Bolívar"),
                new XElement("CambioAMonedaLocal", "1"),
                new XElement("RetencionIva", 0),
                new XElement("NroComprobanteRetIva", string.Empty),
                new XElement("StatusRetencionIva", (int)eStatusRetencionIVACobranza.NoAplica),
                new XElement("GeneraMovBancario", true),
                new XElement("CobradoAnticipo", 0),
                new XElement("Vuelto", -vTotalCobrado),
                new XElement("DescProntoPago", 0),
                new XElement("DescProntoPagoPorc", 0),
                new XElement("ComisionVendedor", 0),
                new XElement("AplicaCreditoBancario", false),
                new XElement("CodigoMoneda", "VES"),
                new XElement("NumeroDeComprobanteISLR", 0),
                new XElement("TipoDeDocumento", ""),
                new XElement("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login)));
            InsertarCobranza(vXmlCobranza);
            InsertarDocumentoCobradoDeCobranzaParaNotaDeCredito(valConsecutivoCompania, outNumeroDeCobranza, vNumeroFactura, vTotalFactura, vTotalCobrado, valTipoDeCxc);
            ((ICXCPdn)new clsCXCNav()).CambiarStatusDeCxcACancelada(valConsecutivoCompania, vNumeroFactura, (int)valTipoDeCxc, vTotalFactura);
        }

        private void InsertarDocumentoCobradoDeCobranzaParaNotaDeCredito(int valConsecutivoCompania, string valNumeroCobranza, string valNumeroFactura, decimal valTotalFactura, decimal valTotalCobrado, eTipoDeTransaccion valTipoDeCxc) {
            XElement vXmlDocumentoCobrado = new XElement("GpData");
            vXmlDocumentoCobrado.Add(new XElement("GpResult",
                new XElement("ConsecutivoCompania", valConsecutivoCompania),
                new XElement("NumeroCobranza", valNumeroCobranza),
                new XElement("NumeroDelDocumentoCobrado", valNumeroFactura),
                new XElement("TipoDeDocumentoCobrado", LibConvert.EnumToDbValue((int)valTipoDeCxc)),
                new XElement("MontoOriginalRestanteAlDiaDelc", valTotalFactura),
                new XElement("MontoAbonado", valTotalCobrado),
                new XElement("SimboloMonedaDeCxC", "Bs"),
                new XElement("MontoTotalDeCxC", valTotalFactura),
                new XElement("CodigoMonedaDeCxC", "VES"),
                new XElement("CambioAMonedaDeCobranza", "1"),
                new XElement("MontoEnMonedaOriginalDeCxC", valTotalFactura),
                new XElement("MontoIvaDeCxC", 0),
                new XElement("MontoIvaRetenido", 0),
                new XElement("NumeroComprobanteRetIva", 0),
                new XElement("FechaComprobanteRetIva", LibDate.Today()),
                new XElement("SeRetuvoIva", "N"),
                new XElement("SeContabilizoIvaDiferido", "N"),
                new XElement("MontoAbonadoEnMonedaOriginal", valTotalCobrado),
                new XElement("CambioAMonedaLocal", "1"),
                new XElement("SimboloMonedaDelAbono", new clsNoComunSaw().InstanceMonedaLocalActual.GetHoySimboloMoneda())));
            InsertarDocumentoCobrado(vXmlDocumentoCobrado);
        }

        private XElement ObtenerSimboloYNombreMonedaDesdeCodigo(string valCodigoMoneda) {
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParametros = new LibGpParams();
            vParametros.AddInString("Codigo", valCodigoMoneda, 4);
            vSQL.AppendLine("SELECT Nombre, Simbolo FROM dbo.Moneda WHERE Codigo = @Codigo");
            XElement vMoneda = LibBusiness.ExecuteSelect(vSQL.ToString(), vParametros.Get(), "", 0);
            return vMoneda;
        }

        #region Metodos para Insertar desde XML

        private void InsertarCobranza(XElement valXmlCobranza) {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", LibConvert.ToInt(LibXml.GetPropertyString(valXmlCobranza, "ConsecutivoCompania")));
            vParams.AddInString("NumeroCobranza", LibXml.GetPropertyString(valXmlCobranza, "NumeroCobranza"), 12);
            vParams.AddInString("StatusCobranza", LibXml.GetPropertyString(valXmlCobranza, "StatusCobranza"), 1);
            vParams.AddInDateTime("Fecha", LibConvert.ToDate(LibXml.GetPropertyString(valXmlCobranza, "Fecha")));
            vParams.AddInDateTime("FechaAnulacion", LibConvert.ToDate(LibXml.GetPropertyString(valXmlCobranza, "Fecha")));
            vParams.AddInString("CodigoCliente", LibXml.GetPropertyString(valXmlCobranza, "CodigoCliente"), 10);
            vParams.AddInString("CodigoCobrador", LibXml.GetPropertyString(valXmlCobranza, "CodigoCobrador"), 5);
            vParams.AddInDecimal("TotalFactura", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "TotalDocumentos")), 2);
            vParams.AddInDecimal("RetencionIslr", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "RetencionIslr")), 2);
            vParams.AddInDecimal("TotalCobrado", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "TotalCobrado")), 2);
            vParams.AddInDecimal("CobradoEfectivo", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "CobradoEfectivo")), 2);
            vParams.AddInDecimal("CobradoCheque", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "CobradoCheque")), 2);
            vParams.AddInString("NumeroDelCheque", LibXml.GetPropertyString(valXmlCobranza, "NumeroDelCheque"), 10);
            vParams.AddInDecimal("CobradoTarjeta", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "CobradoTarjetas")), 2);
            vParams.AddInString("CualTarjeta", LibXml.GetPropertyString(valXmlCobranza, "CualTarjeta"), 1);
            vParams.AddInString("NroDeLaTarjeta", LibXml.GetPropertyString(valXmlCobranza, "NroDeLaTarjeta"), 20);
            vParams.AddInString("Origen", LibXml.GetPropertyString(valXmlCobranza, "Origen"), 1);
            vParams.AddInDecimal("TotalOtrosCargos", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "TotalOtros")), 2);
            vParams.AddInString("NombreBanco", LibXml.GetPropertyString(valXmlCobranza, "NombreBanco"), 20);
            vParams.AddInString("CodigoCuentaBancaria", LibXml.GetPropertyString(valXmlCobranza, "CodigoCuentaBancaria"), 5);
            vParams.AddInString("ConceptoBancario", LibXml.GetPropertyString(valXmlCobranza, "CodigoConcepto"), 8);
            vParams.AddInString("Moneda", LibXml.GetPropertyString(valXmlCobranza, "Moneda"), 80);
            vParams.AddInDecimal("CambioAMonedaLocal", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "CambioAMonedaLocal")), 2);
            vParams.AddInDecimal("RetencionIva", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "RetencionIva")), 2);
            vParams.AddInString("NroComprobanteRetIva", LibXml.GetPropertyString(valXmlCobranza, "NroComprobanteRetIva"), 20);
            vParams.AddInString("StatusRetencionIva", LibXml.GetPropertyString(valXmlCobranza, "StatusRetencionIva"), 1);
            vParams.AddInBoolean("GeneraMovBancario", LibConvert.ToBool(LibXml.GetPropertyString(valXmlCobranza, "GeneraMovBancario")));
            vParams.AddInDecimal("CobradoAnticipo", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "CobradoAnticipo")), 2);
            vParams.AddInDecimal("VueltoDelCobroDirecto", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "Vuelto")), 2);
            vParams.AddInDecimal("DescProntoPago", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "DescProntoPago")), 2);
            vParams.AddInDecimal("DescProntoPagoPorc", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "DescProntoPagoPorc")), 2);
            vParams.AddInDecimal("ComisionVendedor", LibImportData.ToDec(LibXml.GetPropertyString(valXmlCobranza, "ComisionVendedor")), 2);
            vParams.AddInBoolean("AplicaCreditoBancario", LibConvert.ToBool(LibXml.GetPropertyString(valXmlCobranza, "AplicaCreditoBancario")));
            vParams.AddInString("CodigoMoneda", LibXml.GetPropertyString(valXmlCobranza, "CodigoMoneda"), 4);
            vParams.AddInInteger("NumeroDeComprobanteISLR", LibImportData.ToInt(LibXml.GetPropertyString(valXmlCobranza, "NumeroDeComprobanteISLR")));
            vParams.AddInString("TipoDeDocumento", LibXml.GetPropertyString(valXmlCobranza, "TipoDeDocumento"), 1);
            vParams.AddInString("NombreOperador", LibXml.GetPropertyString(valXmlCobranza, "NombreOperador"), 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            vSql.AppendLine(" INSERT INTO Cobranza (");
            vSql.AppendLine("ConsecutivoCompania");
            vSql.AppendLine(", Numero");
            vSql.AppendLine(", StatusCobranza");
            vSql.AppendLine(", Fecha");
            vSql.AppendLine(", FechaAnulacion ");
            vSql.AppendLine(", CodigoCliente");
            vSql.AppendLine(", CodigoCobrador");
            vSql.AppendLine(", TotalDocumentos ");
            vSql.AppendLine(", RetencionIslr ");
            vSql.AppendLine(", TotalCobrado ");
            vSql.AppendLine(", CobradoEfectivo ");
            vSql.AppendLine(", CobradoCheque");
            vSql.AppendLine(", NumerodelCheque ");
            vSql.AppendLine(", CobradoTarjeta");
            vSql.AppendLine(", CualTarjeta");
            vSql.AppendLine(", NroDeLaTarjeta ");
            vSql.AppendLine(", Origen");
            vSql.AppendLine(", TotalOtros ");
            vSql.AppendLine(", NombreBanco");
            vSql.AppendLine(", CodigoCuentaBancaria");
            vSql.AppendLine(", CodigoConcepto ");
            vSql.AppendLine(", Moneda");
            vSql.AppendLine(", CambioAbolivares ");
            vSql.AppendLine(", RetencionIva");
            vSql.AppendLine(", NroComprobanteRetIva");
            vSql.AppendLine(", StatusRetencionIva");
            vSql.AppendLine(", GeneraMovBancario");
            vSql.AppendLine(", CobradoAnticipo");
            vSql.AppendLine(", Vuelto");
            vSql.AppendLine(", DescProntoPago");
            vSql.AppendLine(", DescProntoPagoPorc");
            vSql.AppendLine(", ComisionVendedor");
            vSql.AppendLine(", AplicaCreditoBancario");
            vSql.AppendLine(", CodigoMoneda, NumeroDeComprobanteISLR");
            vSql.AppendLine(", TipoDeDocumento");
            vSql.AppendLine(", NombreOperador");
            vSql.AppendLine(", FechaUltimaModificacion) ");
            vSql.AppendLine(" VALUES (");
            vSql.AppendLine(" @ConsecutivoCompania");
            vSql.AppendLine(", @NumeroCobranza");
            vSql.AppendLine(", @StatusCobranza");
            vSql.AppendLine(", @Fecha");
            vSql.AppendLine(", @FechaAnulacion");
            vSql.AppendLine(", @CodigoCliente");
            vSql.AppendLine(", @CodigoCobrador");
            vSql.AppendLine(", @TotalFactura");
            vSql.AppendLine(", @RetencionIslr ");
            vSql.AppendLine(", @TotalCobrado");
            vSql.AppendLine(", @CobradoEfectivo");
            vSql.AppendLine(", @CobradoCheque");
            vSql.AppendLine(", @NumeroDelCheque");
            vSql.AppendLine(", @CobradoTarjeta");
            vSql.AppendLine(", @CualTarjeta");
            vSql.AppendLine(", @NroDeLaTarjeta");
            vSql.AppendLine(", @Origen");
            vSql.AppendLine(", @TotalOtrosCargos");
            vSql.AppendLine(", @NombreBanco");
            vSql.AppendLine(", @CodigoCuentaBancaria");
            vSql.AppendLine(", @ConceptoBancario");
            vSql.AppendLine(", @Moneda");
            vSql.AppendLine(", @CambioAMonedaLocal");
            vSql.AppendLine(", @RetencionIva");
            vSql.AppendLine(", @NroComprobanteRetIva");
            vSql.AppendLine(", @StatusRetencionIva");
            vSql.AppendLine(", @GeneraMovBancario");
            vSql.AppendLine(", @CobradoAnticipo");
            vSql.AppendLine(", @VueltoDelCobroDirecto");
            vSql.AppendLine(", @DescProntoPago");
            vSql.AppendLine(", @DescProntoPagoPorc");
            vSql.AppendLine(", @ComisionVendedor");
            vSql.AppendLine(", @AplicaCreditoBancario");
            vSql.AppendLine(", @CodigoMoneda");
            vSql.AppendLine(", @NumeroDeComprobanteISLR");
            vSql.AppendLine(", @TipoDeDocumento");
            vSql.AppendLine(", @NombreOperador");
            vSql.AppendLine(", @FechaUltimaModificacion");
            vSql.AppendLine(")");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), string.Empty, 0);
        }

        private void InsertarDocumentoCobrado(XElement valXmlDocumentoCobrado) {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", LibImportData.ToInt(LibXml.GetPropertyString(valXmlDocumentoCobrado, "ConsecutivoCompania")));
            vParams.AddInString("NumeroCobranza", LibXml.GetPropertyString(valXmlDocumentoCobrado, "NumeroCobranza"), 12);
            vParams.AddInString("NumeroDelDocumentoCobrado", LibXml.GetPropertyString(valXmlDocumentoCobrado, "NumeroDelDocumentoCobrado"), 12);
            vParams.AddInString("TipoDeDocumentoCobrado", LibXml.GetPropertyString(valXmlDocumentoCobrado, "TipoDeDocumentoCobrado"), 1);
            vParams.AddInDecimal("MontoOriginalRestanteAlDiaDelc", LibImportData.ToDec(LibXml.GetPropertyString(valXmlDocumentoCobrado, "MontoOriginalRestanteAlDiaDelc")), 2);
            vParams.AddInDecimal("MontoAbonado", LibImportData.ToDec(LibXml.GetPropertyString(valXmlDocumentoCobrado, "MontoAbonado")), 2);
            vParams.AddInString("SimboloMonedaDeCxC", LibXml.GetPropertyString(valXmlDocumentoCobrado, "SimboloMonedaDeCxC"), 4);
            vParams.AddInDecimal("MontoTotalDeCxC", LibImportData.ToDec(LibXml.GetPropertyString(valXmlDocumentoCobrado, "MontoTotalDeCxC")), 2);
            vParams.AddInString("CodigoMonedaDeCxC", LibXml.GetPropertyString(valXmlDocumentoCobrado, "CodigoMonedaDeCxC"), 12);
            vParams.AddInDecimal("CambioAMonedaDeCobranza", LibImportData.ToDec(LibXml.GetPropertyString(valXmlDocumentoCobrado, "CambioAMonedaDeCobranza")), 2);
            vParams.AddInDecimal("MontoEnMonedaOriginalDeCxC", LibImportData.ToDec(LibXml.GetPropertyString(valXmlDocumentoCobrado, "MontoEnMonedaOriginalDeCxC")), 2);
            vParams.AddInDecimal("MontoIvaDeCxC", LibImportData.ToDec(LibXml.GetPropertyString(valXmlDocumentoCobrado, "MontoIvaDeCxC")), 2);
            vParams.AddInDecimal("MontoIvaRetenido", LibImportData.ToDec(LibXml.GetPropertyString(valXmlDocumentoCobrado, "MontoIvaRetenido")), 2);
            vParams.AddInInteger("NumeroComprobanteRetIva", LibImportData.ToInt(LibXml.GetPropertyString(valXmlDocumentoCobrado, "NumeroComprobanteRetIva")));
            vParams.AddInDateTime("FechaComprobanteRetIva", LibConvert.ToDate(LibXml.GetPropertyString(valXmlDocumentoCobrado, "FechaComprobanteRetIva")));
            vParams.AddInString("SeRetuvoIva", LibXml.GetPropertyString(valXmlDocumentoCobrado, "SeRetuvoIva"), 1);
            vParams.AddInString("SeContabilizoIvaDiferido", LibXml.GetPropertyString(valXmlDocumentoCobrado, "SeContabilizoIvaDiferido"), 1);
            vParams.AddInDecimal("MontoAbonadoEnMonedaOriginal", LibImportData.ToDec(LibXml.GetPropertyString(valXmlDocumentoCobrado, "MontoAbonadoEnMonedaOriginal")), 2);
            vParams.AddInDecimal("CambioAMonedaLocal", LibImportData.ToDec(LibXml.GetPropertyString(valXmlDocumentoCobrado, "CambioAMonedaLocal")), 2);
            vParams.AddInString("SimboloMonedaDelAbono", LibXml.GetPropertyString(valXmlDocumentoCobrado, "SimboloMonedaDelAbono"), 2);
            vSql.AppendLine(" INSERT INTO dbo.DocumentoCobrado (");
            vSql.AppendLine(" ConsecutivoCompania");
            vSql.AppendLine(", NumeroCobranza");
            vSql.AppendLine(", NumeroDelDocumentoCobrado");
            vSql.AppendLine(", TipoDeDocumentoCobrado");
            vSql.AppendLine(", MontoOriginalRestanteAlDiaDelc");
            vSql.AppendLine(", MontoAbonado");
            vSql.AppendLine(", SimboloMonedaDeCxC");
            vSql.AppendLine(", MontoTotalDeCxC");
            vSql.AppendLine(", CodigoMonedaDeCxC");
            vSql.AppendLine(", CambioAMonedaDeCobranza");
            vSql.AppendLine(", MontoEnMonedaOriginalDeCxC");
            vSql.AppendLine(", MontoIvaDeCxC");
            vSql.AppendLine(", MontoIvaRetenido");
            vSql.AppendLine(", NumeroComprobanteRetIva");
            vSql.AppendLine(", FechaComprobanteRetIva");
            vSql.AppendLine(", SeRetuvoIva");
            vSql.AppendLine(", SeContabilizoIvaDiferido");
            vSql.AppendLine(", MontoAbonadoEnMonedaOriginal");
            vSql.AppendLine(", CambioAMonedaLocal");
            vSql.AppendLine(", SimboloMonedaDelAbono) ");
            vSql.AppendLine(" VALUES(");
            vSql.AppendLine(" @ConsecutivoCompania ");
            vSql.AppendLine(", @NumeroCobranza");
            vSql.AppendLine(", @NumeroDelDocumentoCobrado");
            vSql.AppendLine(", @TipoDeDocumentoCobrado");
            vSql.AppendLine(", @MontoOriginalRestanteAlDiaDelc");
            vSql.AppendLine(", @MontoAbonado");
            vSql.AppendLine(", @SimboloMonedaDeCxC");
            vSql.AppendLine(", @MontoTotalDeCxC");
            vSql.AppendLine(", @CodigoMonedaDeCxC");
            vSql.AppendLine(", @CambioAMonedaDeCobranza");
            vSql.AppendLine(", @MontoEnMonedaOriginalDeCxC");
            vSql.AppendLine(", @MontoIvaDeCxC");
            vSql.AppendLine(", @MontoIvaRetenido");
            vSql.AppendLine(", @NumeroComprobanteRetIva");
            vSql.AppendLine(", @FechaComprobanteRetIva");
            vSql.AppendLine(", @SeRetuvoIva");
            vSql.AppendLine(", @SeContabilizoIvaDiferido");
            vSql.AppendLine(", @MontoAbonadoEnMonedaOriginal");
            vSql.AppendLine(", @CambioAMonedaLocal");
            vSql.AppendLine(", @SimboloMonedaDelAbono");
            vSql.AppendLine(")");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), string.Empty, 0);
        }

        XElement ICobranzaPdn.FindByNumero(int valConsecutivoCompania, string valNumero) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Numero", valNumero, 10);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Dbo.Cobranza");
            SQL.AppendLine("WHERE Numero = @Numero");
            SQL.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }

        #endregion

    }
}
