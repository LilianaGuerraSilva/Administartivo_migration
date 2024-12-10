using Galac.Adm.Brl.Banco;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Ccl.Venta;
using Galac.Comun.Brl.TablasGen;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Lib;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Catching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Galac.Adm.Brl.Venta {
    public class clsCobroDeFacturaNav : ILibBusinessSearch {

        public bool GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            return RetrieveListInfo(valModule, ref refXmlDocument, valXmlParamsExpression);
        }

        protected bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            switch (valModule) {
                case "Moneda":
                    return ((ILibPdn)new clsMonedaNav()).GetDataForList("Moneda", ref refXmlDocument, valXmlParamsExpression);
                default:
                    break;
            }
            throw new NotImplementedException("No se ha implementado el método RetrieveListInfo para el Módulo " + valModule);
        }

        public bool CobrarFactura(int ConsecutivoCompania, string NumeroFactura, eTipoDocumentoFactura TipoDeDocumento, List<RenglonCobroDeFactura> valRecord) {
            bool vResult = false;
            try {
                using (System.Transactions.TransactionScope vScope = LibBusiness.CreateScope()) {
                    vResult = InsertarRenglonCobroDeFactura(ConsecutivoCompania, NumeroFactura, TipoDeDocumento, valRecord);
                    if (vResult) {
                        vScope.Complete();
                    } else {
                        throw new GalacAlertException("Reintentando...");
                    }
                }
            } catch (GalacAlertException vEx) {
                throw vEx;
            } finally {
                LibBusiness.EndScopedBusinessProcess("");
            }
            return vResult;
        }

        private bool InsertarRenglonCobroDeFactura(int ConsecutivoCompania, string NumeroFactura, eTipoDocumentoFactura TipoDeDocumento, List<RenglonCobroDeFactura> valRecord) {
            bool vResult = false;
            try {
                clsRenglonCobroDeFacturaNav vRenglonCobro = new clsRenglonCobroDeFacturaNav();
                vRenglonCobro.InsertChildRenglonCobroDeFactura(ConsecutivoCompania, NumeroFactura, TipoDeDocumento, valRecord);
                vResult = true;
            } catch (Exception vEx) {
                throw vEx;
            }
            return vResult;
        }

        public bool GenerarCobranzaYMovimientoBancarioDeCobroEnMultimoneda(int valConsecutivoCompania, string valNumeroFactura, eTipoDocumentoFactura valTipoDeDocumento, out IList<string> outListaDeCobranzasGeneradas, string valNumeroCxC) {
            bool vResult = false;
            try {
                List<string> vNumerosDeCobranzas = new List<string>();
                List<MovimientoBancario> vMovimientosBancarios = new List<MovimientoBancario>();
                XElement vDataFactura = BuscarDatosDeFactura(valConsecutivoCompania, valNumeroFactura, (int)valTipoDeDocumento);
                XElement vDataRenglonCobro = BuscarDatosDeRenglonCobro(valConsecutivoCompania, valNumeroFactura, (int)valTipoDeDocumento);
                XElement vDataCXC = BuscarDatosDeCXC(valConsecutivoCompania, valNumeroCxC, valTipoDeDocumento);
                if (valTipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito || valTipoDeDocumento == eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) {
                    string outNumeroCobranza = "";
                    eTipoDeTransaccion vTipoDeCxc = TipoDocumentoFacturaToTipoCxCConverter(valTipoDeDocumento);
                    ((ICobranzaPdn)new clsCobranzaNav()).InsertarCobranzaDeNotaDeCredito(valConsecutivoCompania, vDataFactura, vDataRenglonCobro, vTipoDeCxc, out outNumeroCobranza);
                    vNumerosDeCobranzas.Add(outNumeroCobranza);
                    XElement vDataCobranzas = BuscarDatosDeCobranza(valConsecutivoCompania, vNumerosDeCobranzas);
                    vMovimientosBancarios = CrearListaDeMovimientosBancarios(vDataCobranzas);
                    vMovimientosBancarios[0].Monto = 0;
                } else {
                    ((ICobranzaPdn)new clsCobranzaNav()).InsertarCobranzasDeCobroEnMultimoneda(valConsecutivoCompania, vDataFactura, vDataRenglonCobro, vDataCXC, out vNumerosDeCobranzas);
                    XElement vDataCobranzas = BuscarDatosDeCobranza(valConsecutivoCompania, vNumerosDeCobranzas);
                    vMovimientosBancarios = CrearListaDeMovimientosBancarios(vDataCobranzas);
                }
                ((IMovimientoBancarioPuntoDeVentaPdn)new clsMovimientoBancarioPuntoDeVentaNav()).Insert(vMovimientosBancarios);
                foreach (var vMovimientoBancario in vMovimientosBancarios) {
                    ((ICuentaBancariaPdn)new clsCuentaBancariaNav()).ActualizaSaldoDisponibleEnCuenta(vMovimientoBancario.ConsecutivoCompania,
                        vMovimientoBancario.CodigoCtaBancaria, LibConvert.ToStr(vMovimientoBancario.Monto)
                        , ((int)Galac.Adm.Ccl.Banco.eIngresoEgreso.Ingreso).ToString(), (int)eAccionSR.Modificar, "0", false);
                }
                outListaDeCobranzasGeneradas = vNumerosDeCobranzas;
            } catch (GalacAlertException vEx) {
                throw vEx;
            }
            return vResult;
        }

        public int ObtenerCodigoBancoAsociadoACuentaBancaria(int valConsecutivoCompania, string valCodigoCuentaBancaria) {
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Codigo", valCodigoCuentaBancaria, 5);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vSQL.AppendLine("SELECT CodigoBanco FROM Saw.CuentaBancaria WHERE Codigo = @Codigo AND ConsecutivoCompania = @ConsecutivoCompania");
            XElement vCuentaBancaria = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            return vCuentaBancaria.Descendants("GpResult").Select(s => LibImportData.ToInt(s.Element("CodigoBanco").Value)).FirstOrDefault();
        }

        private XElement BuscarDatosDeFactura(int valConsecutivoCompania, string valNumeroFactura, int valTipoDeDocumento) {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT Numero, TipoDeDocumento, Fecha, CodigoCliente, ConsecutivoVendedor,CodigoVendedor, TotalFactura, CodigoMoneda, CambioABolivares FROM dbo.factura WHERE Numero = @NumeroFactura AND TipoDeDocumento = @TipoDeDocumento AND ConsecutivoCompania = @ConsecutivoCompania");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("NumeroFactura", valNumeroFactura, 11);
            vParams.AddInEnum("TipoDeDocumento", valTipoDeDocumento);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            XElement vFactura = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            return vFactura;
        }

        private XElement BuscarDatosDeRenglonCobro(int valConsecutivoCompania, string valNumeroFactura, int valTipoDeDocumento) {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT ConsecutivoRenglon, CodigoFormaDelCobro, Monto, CodigoMoneda, CambioAMonedaLocal FROM dbo.renglonCobroDeFactura WHERE NumeroFactura = @NumeroFactura AND TipoDeDocumento = @TipoDeDocumento AND ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro <> '00015'");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("NumeroFactura", valNumeroFactura, 11);
            vParams.AddInInteger("TipoDeDocumento", valTipoDeDocumento);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            XElement vRenglonCobro = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            return vRenglonCobro;
        }

        private XElement BuscarDatosDeCXC(int valConsecutivoCompania, string valNumeroCxC, eTipoDocumentoFactura valTipoDeDocumento) {
            StringBuilder vSQL = new StringBuilder();
            eTipoDeTransaccion vTipoCxc = TipoDocumentoFacturaToTipoCxCConverter(valTipoDeDocumento);
            vSQL.AppendLine("SELECT Numero, TipoCxc, MontoGravado, MontoIva,(MontoExento + MontoGravado + MontoIva) AS TotalCXC, CodigoMoneda , CambioABolivares FROM dbo.CxC WHERE Numero = @Numero AND TipoCxc = @TipoCxc AND ConsecutivoCompania = @ConsecutivoCompania AND VieneDeCreditoElectronico = 'N'");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Numero", valNumeroCxC, 20);
            vParams.AddInEnum("TipoCxc", (int)vTipoCxc);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            XElement vCxC = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            return vCxC;
        }

        private XElement BuscarDatosDeCobranza(int valConsecutivoCompania, List<string> valNumeroDeCobranzas) {
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            int vListCobranzasIndex = valNumeroDeCobranzas.Count();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vSQL.AppendLine("SELECT Cobranza.ConsecutivoCompania, Cobranza.Numero, Cobranza.Fecha, Cobranza.CodigoCliente, Cliente.Nombre AS NombreCliente, Cobranza.CodigoCuentaBancaria, Cobranza.CodigoConcepto, Cobranza.TotalCobrado, Cobranza.CambioABolivares FROM dbo.Cobranza");
            vSQL.AppendLine("INNER JOIN dbo.Cliente ON Cliente.Codigo = Cobranza.CodigoCliente AND Cobranza.ConsecutivoCompania = CLiente.ConsecutivoCompania");
            vSQL.AppendLine("WHERE Cobranza.Numero IN ('" + valNumeroDeCobranzas[0] + "'");
            if (vListCobranzasIndex > 1) {
                for (int i = 2; i <= vListCobranzasIndex; i++) {
                    vSQL.AppendLine(", '" + valNumeroDeCobranzas[i - 1] + "'");
                }
            }
            vSQL.AppendLine(") AND Cobranza.ConsecutivoCompania = @ConsecutivoCompania");
            XElement vCobranzas = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            return vCobranzas;
        }

        private List<MovimientoBancario> CrearListaDeMovimientosBancarios(XElement valDataDeCobranzas) {
            List<MovimientoBancario> vMovimientoBancarios = new List<MovimientoBancario>();
            int vConsecutivoCompania = valDataDeCobranzas.Descendants("GpResult").Select(s => (int)s.Element("ConsecutivoCompania")).FirstOrDefault();
            int vConsecutivoMovimiento = ((IMovimientoBancarioPdn)new clsMovimientoBancarioNav()).BuscarSiguienteConsecutivoMovimientoBancario(vConsecutivoCompania);
            int vIteration = 0; 
            foreach (XElement vItem in valDataDeCobranzas.Descendants("GpResult")) {
                vIteration += 1;
                vConsecutivoMovimiento = vIteration > 1 ? vConsecutivoMovimiento += 1 : vConsecutivoMovimiento;
                vMovimientoBancarios.Add(new MovimientoBancario() {
                    ConsecutivoCompania = vConsecutivoCompania,
                    ConsecutivoMovimiento = vConsecutivoMovimiento,
                    CodigoCtaBancaria = LibXml.GetElementValueOrEmpty(vItem, "CodigoCuentaBancaria"),
                    CodigoConcepto = LibXml.GetElementValueOrEmpty(vItem, "CodigoConcepto"),
                    Fecha = LibConvert.ToDate(LibXml.GetElementValueOrEmpty(vItem, "Fecha")),
                    TipoConcepto = "0",
                    Monto = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vItem, "TotalCobrado")),
                    NumeroDocumento = LibXml.GetElementValueOrEmpty(vItem, "Numero"),
                    Descripcion = "Cobro a " + LibXml.GetElementValueOrEmpty(vItem, "CodigoCliente") + " - " + LibXml.GetElementValueOrEmpty(vItem, "NombreCliente"),
                    NroMovimientoRelacionado = LibXml.GetElementValueOrEmpty(vItem, "Numero"),
                    GeneradoPor = "1",
                    CambioABolivares = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vItem, "CambioABolivares")),
                    ImprimirCheque = "N",
                    ConciliadoSN = "N",
                    NroConciliacion = string.Empty,
                    GenerarAsientoDeRetiroEnCuenta = "N",
                    NombreOperador = ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login,
                    FechaUltimaModificacion = LibDate.Today()
                });
            }
            return vMovimientoBancarios;
        }

        private eTipoDeTransaccion TipoDocumentoFacturaToTipoCxCConverter(eTipoDocumentoFactura valTipoDocumentoFactura) {
            eTipoDeTransaccion vTipoCxc = eTipoDeTransaccion.FACTURA;
            switch (valTipoDocumentoFactura) {
                case eTipoDocumentoFactura.Factura:
                    vTipoCxc = eTipoDeTransaccion.FACTURA;
                    break;
                case eTipoDocumentoFactura.NotaDeCredito:
                    vTipoCxc = eTipoDeTransaccion.NOTADECREDITO;
                    break;
                case eTipoDocumentoFactura.NotaDeDebito:
                    vTipoCxc = eTipoDeTransaccion.NOTADEDEBITO;
                    break;
                case eTipoDocumentoFactura.ComprobanteFiscal:
                    vTipoCxc = eTipoDeTransaccion.TICKETMAQUINAREGISTRADORA;
                    break;
                case eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal:
                    vTipoCxc = eTipoDeTransaccion.NOTADECREDITOCOMPROBANTEFISCAL;
                    break;
            }
            return vTipoCxc;
        }

    }
}
