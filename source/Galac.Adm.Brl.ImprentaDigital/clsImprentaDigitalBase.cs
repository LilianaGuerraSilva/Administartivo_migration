using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.Cliente;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Ccl.SttDef;
using Galac.Adm.Ccl.Vendedor;
using Galac.Saw.LibWebConnector;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Catching;
using Galac.Saw.Lib;
using Galac.Adm.Ccl.ImprentaDigital;
using LibGalac.Aos.DefGen;
using System.IO;
using System.Diagnostics.SymbolStore;

namespace Galac.Adm.Brl.ImprentaDigital {

    public abstract class clsImprentaDigitalBase {

        #region Propiedades   
        public int ConsecutivoCompania { get; set; }
        public string NumeroFactura { get; set; }

        public string NumeroControl { get; set; }
        public eTipoDocumentoFactura TipoDeDocumento { get; set; }
        public string CodigoRespuesta { get; set; }
        public string EstatusDocumento { get; set; }
        public clsLoginUser LoginUser { get; set; }
        public string HoraAsignacion { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public Cliente ClienteImprentaDigital { get; set; }
        public InfoAdicionalCliente InfoAdicionalClienteImprentaDigital { get; set; }
        public Vendedor VendedorImprentaDigital { get; set; }
        public FacturaRapida FacturaImprentaDigital { get; set; }
        public List<FacturaRapidaDetalle> DetalleFacturaImprentaDigital { get; set; }
        public eProveedorImprentaDigital ProveedorImprentaDigital { get; set; }
        public string CodigoMonedaME { get; private set; }
        public string CodigoMonedaLocal { get; private set; }
        public decimal CambioABolivares { get; private set; }
        public string Mensaje { get; set; }
        public string NumeroCxP { get; set; }
              
        public eTipoDocumentoImprentaDigital TipoDocumentoImprentaDigital { get; private set; }
        public ComprobanteRetIVA ComprobanteRetIVAImprentaDigital { get; private set; }

        public SujetoDeRetencion SujetoDeRetencionImpnretaDigital { get; private set; }

        QAdvSql insUtilSql;

        #endregion Propiedades
        public clsImprentaDigitalBase() {

        }

        public clsImprentaDigitalBase(eTipoDocumentoFactura initTipoDocumento, string initNumeroDocumento, eTipoDocumentoImprentaDigital valTipoDocumentoImprentaDigital) {
            LoginUser = new clsLoginUser();
            TipoDeDocumento = initTipoDocumento;
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
            CodigoMonedaME = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            CodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaLocal");
            clsImprentaDigitalSettings vImprentaDigitalSettings = new clsImprentaDigitalSettings();
            TipoDocumentoImprentaDigital = valTipoDocumentoImprentaDigital;
            NumeroControl = string.Empty;
            Mensaje = string.Empty;
            EstatusDocumento = string.Empty;
            CodigoRespuesta = string.Empty;
            FechaAsignacion = LibDate.MinDateForDB();
            HoraAsignacion = string.Empty;
            insUtilSql = new QAdvSql("");
            FacturaImprentaDigital = new FacturaRapida();
            ComprobanteRetIVAImprentaDigital = new ComprobanteRetIVA();
            if (TipoDocumentoImprentaDigital == eTipoDocumentoImprentaDigital.RetencionIVA) {
                NumeroCxP = initNumeroDocumento;
            } else {
                NumeroFactura = initNumeroDocumento;
            }
            switch (ProveedorImprentaDigital) {
                case eProveedorImprentaDigital.TheFactoryHKA:
                case eProveedorImprentaDigital.Novus:
                case eProveedorImprentaDigital.Unidigital:
                    LoginUser.URL = vImprentaDigitalSettings.DireccionURL;
                    LoginUser.User = vImprentaDigitalSettings.Usuario;
                    LoginUser.UserKey = vImprentaDigitalSettings.CampoUsuario;
                    LoginUser.Password = vImprentaDigitalSettings.Clave;
                    LoginUser.PasswordKey = vImprentaDigitalSettings.CampoClave;
                    break;
                case eProveedorImprentaDigital.NoAplica:
                    break;
                default:
                    break;
            }
        }

        private decimal GetCambio() {
            decimal vCambio = 1;
            if (LibString.S1IsEqualToS2(FacturaImprentaDigital.CodigoMoneda, CodigoMonedaLocal)) {
                vCambio = FacturaImprentaDigital.CambioMostrarTotalEnDivisas;
                if (vCambio == 1) {
                    vCambio = FacturaImprentaDigital.CambioMonedaCXC;
                    if (vCambio == 1) {
                        Comun.Ccl.TablasGen.ICambioPdn insCambio = new Comun.Brl.TablasGen.clsCambioNav();
                        if (!insCambio.ExisteTasaDeCambioParaElDia(CodigoMonedaME, FacturaImprentaDigital.Fecha, out vCambio)) {
                            vCambio = 1;
                        }
                    }
                }
            } else {
                vCambio = FacturaImprentaDigital.CambioABolivares;
            }
            if (vCambio == 0) {
                vCambio = 1;
            }
            return vCambio;
        }

        private string SqlDatosDeDocumentoParaEmitir(ref StringBuilder refParametros) {
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("Numero", NumeroFactura, 11);
            vParam.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
            refParametros = vParam.Get();
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(" SELECT");
            vSql.AppendLine(" factura.Numero");
            vSql.AppendLine(" ,factura.Fecha");
            vSql.AppendLine(" ,factura.HoraModificacion");
            vSql.AppendLine(" ,factura.FechaDeFacturaAfectada");
            vSql.AppendLine(" ,REPLACE(factura.Observaciones, 'total', 'tot..') AS Observaciones");
            vSql.AppendLine(" ,factura.CodigoMoneda AS MonedaDelDocumento");
            vSql.AppendLine(" ,factura.CodigoMonedaDeCobro AS MonedaDelCobro");
            vSql.AppendLine(" ,factura.StatusFactura");
            vSql.AppendLine(" ,factura.CondicionesDePago");
            vSql.AppendLine(" ,factura.TipoDeDocumento");
            vSql.AppendLine(" ,factura.TipoDeVenta");
            vSql.AppendLine(" ,factura.TipoDeTransaccion");
            vSql.AppendLine(" ,factura.Talonario");
            vSql.AppendLine(" ,ROUND(factura.MontoDescuento1,2) AS MontoDescuento1");
            vSql.AppendLine(" ,ROUND(factura.PorcentajeDescuento,2) AS PorcentajeDescuento");
            vSql.AppendLine(" ,ROUND(factura.CambioABolivares,4) AS CambioABolivares");
            vSql.AppendLine(" ,ROUND(factura.CambioMostrarTotalEnDivisas,4) AS CambioMostrarTotalEnDivisas");
            vSql.AppendLine(" ,ROUND(factura.CambioMonedaCXC,4) AS CambioMonedaCXC");
            vSql.AppendLine(" ,ROUND(factura.TotalRenglones,2) AS TotalRenglones");
            vSql.AppendLine(" ,ROUND(factura.TotalMontoExento,2) AS TotalMontoExento");
            vSql.AppendLine(" ,ROUND(factura.TotalBaseImponible,2) AS TotalBaseImponible");
            vSql.AppendLine(" ,ROUND(factura.TotalIVA,2) AS TotalIVA");
            vSql.AppendLine(" ,ROUND(factura.TotalFactura,2) AS TotalFactura");
            vSql.AppendLine(" ,factura.NumeroFacturaAfectada");
            vSql.AppendLine(" ,factura.NumeroControl");
            vSql.AppendLine(" ,factura.NumeroDesde");
            vSql.AppendLine(" ,factura.NumeroHasta");
            vSql.AppendLine(" ,factura.NumeroDeCuotas");
            vSql.AppendLine(" ,factura.FormaDeCobro");
            vSql.AppendLine(" ,factura.FormaDePago");
            vSql.AppendLine(" ,factura.CodigoVendedor");
            vSql.AppendLine(" ,factura.CodigoCliente");
            vSql.AppendLine(" ,factura.GeneradoPor");
            vSql.AppendLine(" ,ROUND(factura.MontoGravableAlicuota1,2) AS MontoGravableAlicuota1");
            vSql.AppendLine(" ,ROUND(factura.MontoGravableAlicuota2,2) AS MontoGravableAlicuota2");
            vSql.AppendLine(" ,ROUND(factura.MontoGravableAlicuota3,2) AS MontoGravableAlicuota3");
            vSql.AppendLine(" ,ROUND(factura.PorcentajeAlicuota1,2) AS PorcentajeAlicuota1");
            vSql.AppendLine(" ,ROUND(factura.PorcentajeAlicuota2,2) AS PorcentajeAlicuota2");
            vSql.AppendLine(" ,ROUND(factura.PorcentajeAlicuota3,2) AS PorcentajeAlicuota3");
            vSql.AppendLine(" ,ROUND(factura.MontoIVAAlicuota1,2) AS MontoIVAAlicuota1");
            vSql.AppendLine(" ,ROUND(factura.MontoIVAAlicuota2,2) AS MontoIVAAlicuota2");
            vSql.AppendLine(" ,ROUND(factura.MontoIVAAlicuota3,2) AS MontoIVAAlicuota3");
            vSql.AppendLine(" ,ROUND(factura.AlicuotaIGTF,2) AS AlicuotaIGTF");
            vSql.AppendLine(" ,ROUND(factura.IGTFML,2) AS IGTFML");
            vSql.AppendLine(" ,ROUND(factura.IGTFME,2) AS IGTFME");
            vSql.AppendLine(" ,ROUND(factura.BaseImponibleIGTF,2) AS BaseImponibleIGTF");
            vSql.AppendLine(" ,factura.UsarDireccionFiscal");
            vSql.AppendLine(" ,factura.NoDirDespachoAimprimir");
            vSql.AppendLine(" ,factura.ImprentaDigitalGUID");
            vSql.AppendLine(" FROM factura");
            vSql.AppendLine(" WHERE factura.ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND factura.Numero = @Numero ");
            vSql.AppendLine(" AND TipoDeDocumento = @TipoDeDocumento ");
            return vSql.ToString();
        }

        private void BuscarDatosDeDocumentoParaEmitir() {
            try {
                StringBuilder vParam = new StringBuilder();
                string vSql = SqlDatosDeDocumentoParaEmitir(ref vParam);
                XElement vResult = LibBusiness.ExecuteSelect(vSql, vParam, "", 0);
                if (vResult != null && vResult.HasElements) {
                    FacturaImprentaDigital = new FacturaRapida();
                    FacturaImprentaDigital.Numero = LibXml.GetPropertyString(vResult, "Numero");
                    FacturaImprentaDigital.Fecha = LibConvert.ToDate(LibXml.GetPropertyString(vResult, "Fecha"));
                    FacturaImprentaDigital.HoraModificacion = LibXml.GetPropertyString(vResult, "HoraModificacion");
                    FacturaImprentaDigital.FechaDeFacturaAfectada = LibConvert.ToDate(LibXml.GetPropertyString(vResult, "FechaDeFacturaAfectada"));
                    FacturaImprentaDigital.Observaciones = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Observaciones"));
                    FacturaImprentaDigital.CodigoMoneda = LibXml.GetPropertyString(vResult, "MonedaDelDocumento");
                    FacturaImprentaDigital.CodigoMonedaDeCobro = LibXml.GetPropertyString(vResult, "MonedaDelCobro");
                    FacturaImprentaDigital.StatusFactura = LibXml.GetPropertyString(vResult, "StatusFactura");
                    FacturaImprentaDigital.CondicionesDePago = LibXml.GetPropertyString(vResult, "CondicionesDePago");
                    FacturaImprentaDigital.TipoDeDocumento = LibXml.GetPropertyString(vResult, "TipoDeDocumento");
                    FacturaImprentaDigital.TipoDeVenta = LibXml.GetPropertyString(vResult, "TipoDeVenta");
                    FacturaImprentaDigital.TipoDeTransaccion = LibXml.GetPropertyString(vResult, "TipoDeTransaccion");
                    FacturaImprentaDigital.Talonario = LibXml.GetPropertyString(vResult, "Talonario");
                    FacturaImprentaDigital.PorcentajeDescuento = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "PorcentajeDescuento"));
                    FacturaImprentaDigital.MontoDescuento1 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoDescuento1"));
                    FacturaImprentaDigital.CambioABolivares = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "CambioABolivares"));
                    FacturaImprentaDigital.CambioMostrarTotalEnDivisas = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "CambioMostrarTotalEnDivisas"));
                    FacturaImprentaDigital.CambioMonedaCXC = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "CambioMonedaCXC"));
                    FacturaImprentaDigital.TotalRenglones = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "TotalRenglones"));
                    FacturaImprentaDigital.TotalMontoExento = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "TotalMontoExento"));
                    FacturaImprentaDigital.TotalBaseImponible = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "TotalBaseImponible"));
                    FacturaImprentaDigital.TotalIVA = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "TotalIVA"));
                    FacturaImprentaDigital.TotalFactura = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "TotalFactura"));
                    FacturaImprentaDigital.NumeroFacturaAfectada = LibXml.GetPropertyString(vResult, "NumeroFacturaAfectada");
                    FacturaImprentaDigital.NumeroControl = LibXml.GetPropertyString(vResult, "NumeroControl");
                    FacturaImprentaDigital.NumeroDesde = LibXml.GetPropertyString(vResult, "NumeroDesde");
                    FacturaImprentaDigital.NumeroHasta = LibXml.GetPropertyString(vResult, "NumeroHasta");
                    FacturaImprentaDigital.NumeroDeCuotas = LibConvert.ToInt(LibXml.GetPropertyString(vResult, "NumeroDeCuotas"));
                    FacturaImprentaDigital.FormaDeCobro = LibXml.GetPropertyString(vResult, "FormaDeCobro");
                    FacturaImprentaDigital.FormaDePago = LibXml.GetPropertyString(vResult, "FormaDePago");
                    FacturaImprentaDigital.CodigoVendedor = LibXml.GetPropertyString(vResult, "CodigoVendedor");
                    FacturaImprentaDigital.CodigoCliente = LibXml.GetPropertyString(vResult, "CodigoCliente");
                    FacturaImprentaDigital.MontoGravableAlicuota1 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoGravableAlicuota1"));
                    FacturaImprentaDigital.MontoGravableAlicuota2 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoGravableAlicuota2"));
                    FacturaImprentaDigital.MontoGravableAlicuota3 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoGravableAlicuota3"));
                    FacturaImprentaDigital.PorcentajeAlicuota1 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "PorcentajeAlicuota1"));
                    FacturaImprentaDigital.PorcentajeAlicuota2 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "PorcentajeAlicuota2"));
                    FacturaImprentaDigital.PorcentajeAlicuota3 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "PorcentajeAlicuota3"));
                    FacturaImprentaDigital.MontoIvaAlicuota1 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoIVAAlicuota1"));
                    FacturaImprentaDigital.MontoIvaAlicuota2 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoIVAAlicuota2"));
                    FacturaImprentaDigital.MontoIvaAlicuota3 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoIVAAlicuota3"));
                    FacturaImprentaDigital.AlicuotaIGTF = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "AlicuotaIGTF"));
                    FacturaImprentaDigital.IGTFML = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "IGTFML"));
                    FacturaImprentaDigital.IGTFME = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "IGTFME"));
                    FacturaImprentaDigital.BaseImponibleIGTF = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "BaseImponibleIGTF"));
                    FacturaImprentaDigital.GeneradoPor = LibXml.GetPropertyString(vResult, "GeneradoPor");
                    FacturaImprentaDigital.UsarDireccionFiscalAsBool = LibImportData.SNToBool(LibXml.GetPropertyString(vResult, "UsarDireccionFiscal"));
                    FacturaImprentaDigital.NoDirDespachoAimprimir = FacturaImprentaDigital.UsarDireccionFiscalAsBool ? 0 : LibImportData.ToInt(LibXml.GetPropertyString(vResult, "NoDirDespachoAimprimir"));
                    FacturaImprentaDigital.ImprentaDigitalGUID = LibXml.GetPropertyString(vResult, "ImprentaDigitalGUID");
                    CambioABolivares = GetCambio();
                } else {
                    throw new GalacException($"No existe un documento para enviar con el número {NumeroFactura} ", eExceptionManagementType.Controlled);
                }
            } catch (GalacException) {
                throw;
            }
        }

        private string SqlDatosDeDetalleDocumento(ref StringBuilder refParametros) {
            LibGpParams vParam = new LibGpParams();
            StringBuilder vSql = new StringBuilder();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("NumeroFactura", NumeroFactura, 11);
            vParam.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
            refParametros = vParam.Get();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine(" ConsecutivoRenglon");
            vSql.AppendLine(" ,Articulo");
            vSql.AppendLine(" ,REPLACE(Descripcion, 'total', 'tot..') AS Descripcion");
            vSql.AppendLine(" ,ROUND(Cantidad,2) AS Cantidad");
            vSql.AppendLine(" ,ROUND(PrecioSinIVA,2) AS PrecioSinIVA");
            vSql.AppendLine(" ,ROUND(PrecioConIVA,2) AS PrecioConIVA ");
            vSql.AppendLine(" ,ROUND(PorcentajeDescuento,2) AS PorcentajeDescuento");
            vSql.AppendLine(" ,ROUND(TotalRenglon,2) AS TotalRenglon");
            vSql.AppendLine(" ,ROUND(PorcentajeBaseImponible,2) AS PorcentajeBaseImponible");
            vSql.AppendLine(" ,ROUND(PorcentajeAlicuota,2) As PorcentajeAlicuota");
            vSql.AppendLine(" ,AlicuotaIVA");
            vSql.AppendLine(" ,Serial");
            vSql.AppendLine(" ,Rollo");
            vSql.AppendLine(" ,CampoExtraEnRenglonFactura1");
            vSql.AppendLine(" ,CampoExtraEnRenglonFactura2");
            vSql.AppendLine(" FROM renglonFactura ");
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND NumeroFactura = @NumeroFactura ");
            vSql.AppendLine(" AND TipoDeDocumento = @TipoDeDocumento ");
            return vSql.ToString();
        }

        private void BuscarDatosDeDetalleDocumento() {
            try {
                StringBuilder vParam = new StringBuilder();
                string vSql = SqlDatosDeDetalleDocumento(ref vParam);
                XElement vResult = LibBusiness.ExecuteSelect(vSql, vParam, "", 0);
                if (vResult != null && vResult.HasElements) {
                    List<XElement> ListDetaill = vResult.Descendants("GpResult").ToList();
                    DetalleFacturaImprentaDigital = new List<FacturaRapidaDetalle>();
                    foreach (XElement vRowDetaill in ListDetaill) {
                        FacturaRapidaDetalle insFacturaRapidaDetalle = new FacturaRapidaDetalle();
                        insFacturaRapidaDetalle.ConsecutivoRenglon = LibConvert.ToInt(LibXml.GetElementValueOrEmpty(vRowDetaill, "ConsecutivoRenglon"));
                        insFacturaRapidaDetalle.Articulo = LibXml.GetElementValueOrEmpty(vRowDetaill, "Articulo");
                        insFacturaRapidaDetalle.Descripcion = LimpiarCaracteresNoValidos(LibXml.GetElementValueOrEmpty(vRowDetaill, "Descripcion"));
                        insFacturaRapidaDetalle.Cantidad = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vRowDetaill, "Cantidad"));
                        insFacturaRapidaDetalle.PrecioSinIVA = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vRowDetaill, "PrecioSinIVA"));
                        insFacturaRapidaDetalle.PrecioConIVA = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vRowDetaill, "PrecioConIVA"));
                        insFacturaRapidaDetalle.PorcentajeDescuento = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vRowDetaill, "PorcentajeDescuento"));
                        insFacturaRapidaDetalle.TotalRenglon = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vRowDetaill, "TotalRenglon"));
                        insFacturaRapidaDetalle.PorcentajeBaseImponible = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vRowDetaill, "PorcentajeBaseImponible"));
                        insFacturaRapidaDetalle.PorcentajeAlicuota = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vRowDetaill, "PorcentajeAlicuota"));
                        insFacturaRapidaDetalle.AlicuotaIva = LibXml.GetElementValueOrEmpty(vRowDetaill, "AlicuotaIVA");
                        insFacturaRapidaDetalle.Serial = LibXml.GetElementValueOrEmpty(vRowDetaill, "Serial");
                        insFacturaRapidaDetalle.Rollo = LibXml.GetElementValueOrEmpty(vRowDetaill, "Rollo");
                        insFacturaRapidaDetalle.CampoExtraEnRenglonFactura1 = LimpiarCaracteresNoValidos(LibXml.GetElementValueOrEmpty(vRowDetaill, "CampoExtraEnRenglonFactura1"));
                        insFacturaRapidaDetalle.CampoExtraEnRenglonFactura2 = LimpiarCaracteresNoValidos(LibXml.GetElementValueOrEmpty(vRowDetaill, "CampoExtraEnRenglonFactura2"));
                        DetalleFacturaImprentaDigital.Add(insFacturaRapidaDetalle);
                    }
                } else {
                    throw new GalacException("No existen datos para el detalle del documento a enviar", eExceptionManagementType.Controlled);
                }
            } catch (GalacException) {
                throw;
            }
        }

        private string SqlDatosDeCliente(ref StringBuilder refParametros) {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("CodigoCliente", FacturaImprentaDigital.CodigoCliente, 10);
            refParametros = vParam.Get();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine(" Codigo");
            vSql.AppendLine(" ,Nombre");
            vSql.AppendLine(" ,NumeroRif");
            vSql.AppendLine(" ,Direccion");
            vSql.AppendLine(" ,Ciudad");
            vSql.AppendLine(" ,ZonaPostal");
            vSql.AppendLine(" ,Telefono");
            vSql.AppendLine(" ,Email, Contacto");
            vSql.AppendLine(" FROM Cliente ");
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND Codigo = @CodigoCliente ");
            return vSql.ToString();
        }

        private string SqlDatosAdicionalesCliente(ref StringBuilder refParametros) {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("CodigoCliente", FacturaImprentaDigital.CodigoCliente, 10);
            if (FacturaImprentaDigital.NoDirDespachoAimprimir > 0) {
                vParam.AddInInteger("ConsecutivoDireccion", FacturaImprentaDigital.NoDirDespachoAimprimir);
            }
            refParametros = vParam.Get();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine(" CodigoCliente");
            vSql.AppendLine(" ,PersonaContacto");
            vSql.AppendLine(" ,Direccion");
            vSql.AppendLine(" ,Ciudad");
            vSql.AppendLine(" ,ZonaPostal");
            vSql.AppendLine(" FROM DireccionDeDespacho ");
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND CodigoCliente = @CodigoCliente ");
            if (FacturaImprentaDigital.NoDirDespachoAimprimir > 0) {
                vSql.AppendLine(" AND ConsecutivoDireccion = @ConsecutivoDireccion");
            }
            return vSql.ToString();
        }

        private void BuscarDatosDeCliente() {
            try {
                StringBuilder vParam = new StringBuilder();
                string vSql = SqlDatosDeCliente(ref vParam);
                XElement vResult = LibBusiness.ExecuteSelect(vSql, vParam, "", 0);
                if (vResult != null && vResult.HasElements) {
                    ClienteImprentaDigital = new Cliente();
                    ClienteImprentaDigital.Codigo = LibXml.GetPropertyString(vResult, "Codigo");
                    ClienteImprentaDigital.Nombre = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Nombre"));
                    ClienteImprentaDigital.NumeroRIF = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "NumeroRif"));
                    ClienteImprentaDigital.Direccion = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Direccion"));
                    ClienteImprentaDigital.Ciudad = LibXml.GetPropertyString(vResult, "Ciudad");
                    ClienteImprentaDigital.ZonaPostal = LibXml.GetPropertyString(vResult, "ZonaPostal");
                    ClienteImprentaDigital.Telefono = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Telefono"));
                    ClienteImprentaDigital.Email = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Email"));
                    ClienteImprentaDigital.Contacto = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Contacto"));
                    BuscarDatosAdicionalesCliente();
                } else {
                    throw new GalacException("No existen datos para el cliente del documento a enviar", eExceptionManagementType.Controlled);
                }
            } catch (GalacException) {
                throw;
            }
        }

        private void BuscarDatosAdicionalesCliente() {
            try {
                InfoAdicionalClienteImprentaDigital = new InfoAdicionalCliente();
                StringBuilder vParam = new StringBuilder();
                string vSql = SqlDatosAdicionalesCliente(ref vParam);
                XElement vResult = LibBusiness.ExecuteSelect(vSql, vParam, "", 0);
                if (vResult != null && vResult.HasElements) {
                    InfoAdicionalClienteImprentaDigital.Codigo = LibXml.GetPropertyString(vResult, "CodigoCliente");
                    InfoAdicionalClienteImprentaDigital.PersonaContacto = LibXml.GetPropertyString(vResult, "PersonaContacto");
                    InfoAdicionalClienteImprentaDigital.Direccion = LibXml.GetPropertyString(vResult, "Direccion");
                    InfoAdicionalClienteImprentaDigital.Ciudad = LibXml.GetPropertyString(vResult, "Ciudad");
                    InfoAdicionalClienteImprentaDigital.ZonaPostal = LibXml.GetPropertyString(vResult, "ZonaPostal");
                }
            } catch (GalacException) {
                throw;
            }
        }

        private string SqlDatosDelVendedor(ref StringBuilder refParametros) {
            LibGpParams vParam = new LibGpParams();
            StringBuilder vSql = new StringBuilder();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("CodigoVendedor", FacturaImprentaDigital.CodigoVendedor, 10);
            refParametros = vParam.Get();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine(" Codigo");
            vSql.AppendLine(" ,Nombre");
            vSql.AppendLine(" ,Rif ");
            vSql.AppendLine(" ,Direccion");
            vSql.AppendLine(" ,Ciudad");
            vSql.AppendLine(" ,ZonaPostal");
            vSql.AppendLine(" ,Telefono");
            vSql.AppendLine(" ,Email");
            vSql.AppendLine(" FROM Vendedor ");
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine(" AND Codigo = @CodigoVendedor");
            return vSql.ToString();
        }

        private void BuscarDatosDelVendedor() {
            try {
                StringBuilder vParam = new StringBuilder();
                string vSql = SqlDatosDelVendedor(ref vParam);
                XElement vResult = LibBusiness.ExecuteSelect(vSql, vParam, "", 0);
                if (vResult != null && vResult.HasElements) {
                    VendedorImprentaDigital = new Vendedor();
                    VendedorImprentaDigital.Codigo = LibXml.GetPropertyString(vResult, "Codigo");
                    VendedorImprentaDigital.Nombre = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Nombre"));
                    VendedorImprentaDigital.RIF = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "RIF"));
                    VendedorImprentaDigital.Direccion = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Direccion"));
                    VendedorImprentaDigital.Ciudad = LibXml.GetPropertyString(vResult, "Ciudad");
                    VendedorImprentaDigital.ZonaPostal = LibXml.GetPropertyString(vResult, "ZonaPostal");
                    VendedorImprentaDigital.Telefono = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Telefono"));
                    VendedorImprentaDigital.Email = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Email"));
                } else {
                    throw new GalacException("No existen datos para el vendedor del documento a enviar", eExceptionManagementType.Controlled);
                }
            } catch (GalacException) {
                throw;
            }
        }

        private string SqlDatosDocumentoEmitido(ref StringBuilder refParametros) {
            LibGpParams vParam = new LibGpParams();
            StringBuilder vSql = new StringBuilder();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("Numero", NumeroFactura, 11);
            vParam.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
            refParametros = vParam.Get();
            vSql.AppendLine(" SELECT");
            vSql.AppendLine(" factura.Talonario,");
            vSql.AppendLine(" factura.TipoDeDocumento,");
            vSql.AppendLine(" factura.NumeroControl,");
            vSql.AppendLine(" factura.Numero,");
            vSql.AppendLine(" factura.MotivoDeAnulacion,");
            vSql.AppendLine(" factura.StatusFactura, ");
            vSql.AppendLine(" factura.GeneradaPorNotaEntrega,");
            vSql.AppendLine(" factura.EmitidaEnFacturaNumero,");
            vSql.AppendLine(" factura.ReservarMercancia,");
            vSql.AppendLine(" factura.CodigoAlmacen, ");
            vSql.AppendLine(" factura.CodigoCliente, ");
            vSql.AppendLine(" factura.HoraModificacion, ");
            vSql.AppendLine(" ISNULL(factura.ImprentaDigitalGUID,'') AS ImprentaDigitalGUID");
            vSql.AppendLine(" FROM factura");
            vSql.AppendLine(" WHERE factura.ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND factura.Numero = @Numero ");
            vSql.AppendLine(" AND TipoDeDocumento = @TipoDeDocumento");
            return vSql.ToString();
        }

        public void ObtenerDatosDocumentoEmitido() {
            if (TipoDocumentoImprentaDigital == eTipoDocumentoImprentaDigital.RetencionIVA) {
                ObtenerDatosComprobantRetEmitido();
            } else {                
                ObtenerDatoFacturaEmitida();
            }                      
        }

        public void ObtenerDatoFacturaEmitida() {
            try {
                StringBuilder vParam = new StringBuilder();
                string vSql = SqlDatosDocumentoEmitido(ref vParam);
                XElement vResult = LibBusiness.ExecuteSelect(vSql, vParam, "", 0);
                if (vResult != null && vResult.HasElements) {
                    FacturaImprentaDigital = new FacturaRapida();
                    FacturaImprentaDigital.Talonario = LibXml.GetPropertyString(vResult, "Talonario");
                    FacturaImprentaDigital.TipoDeDocumento = LibXml.GetPropertyString(vResult, "TipoDeDocumento");
                    FacturaImprentaDigital.NumeroControl = LibXml.GetPropertyString(vResult, "NumeroControl");
                    FacturaImprentaDigital.Numero = LibXml.GetPropertyString(vResult, "Numero");
                    FacturaImprentaDigital.MotivoDeAnulacion = LibXml.GetPropertyString(vResult, "MotivoDeAnulacion");
                    FacturaImprentaDigital.StatusFactura = LibXml.GetPropertyString(vResult, "StatusFactura");
                    FacturaImprentaDigital.GeneradaPorNotaEntregaAsBool = LibConvert.ToInt(LibXml.GetPropertyString(vResult, "GeneradaPorNotaEntrega")) == 1; // Hay que corregir la clase base
                    FacturaImprentaDigital.ReservarMercanciaAsBool = LibConvert.SNToBool(LibXml.GetPropertyString(vResult, "ReservarMercancia"));
                    FacturaImprentaDigital.CodigoAlmacen = LibXml.GetPropertyString(vResult, "CodigoAlmacen");
                    FacturaImprentaDigital.EmitidaEnFacturaNumero = LibXml.GetPropertyString(vResult, "EmitidaEnFacturaNumero");
                    FacturaImprentaDigital.CodigoCliente = LibXml.GetPropertyString(vResult, "CodigoCliente");
                    FacturaImprentaDigital.ImprentaDigitalGUID = LibXml.GetPropertyString(vResult, "ImprentaDigitalGUID");
                    FacturaImprentaDigital.HoraModificacion = LibXml.GetPropertyString(vResult, "HoraModificacion");
                } else {
                    throw new GalacException("El Documento N° " + LibConvert.ToStr(NumeroFactura) + " no existe.", eExceptionManagementType.Controlled);
                }
            } catch (GalacException) {
                throw;
            }
        }

        private string SqlDatosComprobanteRetEmitido(ref StringBuilder refParametros) {
            LibGpParams vParam = new LibGpParams();
            StringBuilder vSql = new StringBuilder();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("Numero", NumeroCxP, 11);           
            refParametros = vParam.Get();
            vSql.AppendLine(" SELECT"); 
            vSql.AppendLine(" NumeroComprobanteRetencion,");
            vSql.AppendLine(" CodigoProveedor, ");
            vSql.AppendLine(" RetencionIvaEnviadaImpDigital,");
            vSql.AppendLine(" MesDeAplicacion,");
            vSql.AppendLine(" AnoDeAplicacion,");
            vSql.AppendLine(" TipodeCxP,");
            vSql.AppendLine(" MotivoDeAnulacionDeComprobante,");
            vSql.AppendLine(" SeHizoLaRetencionIva,");
            vSql.AppendLine(" ProveedorImprentaDigital,");
            vSql.AppendLine(" NumeroControlRetencionIvaImpDigital,");
            vSql.AppendLine(" Status AS StatusCxP ");
            vSql.AppendLine(" FROM CxP");
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND Numero = @Numero ");            
            return vSql.ToString();
        }

        public void ObtenerDatosComprobantRetEmitido() {
            try {
                StringBuilder vParam = new StringBuilder();
                string vSql = SqlDatosComprobanteRetEmitido(ref vParam);
                XElement vResult = LibBusiness.ExecuteSelect(vSql, vParam, "", 0);
                if (vResult != null && vResult.HasElements) {
                    ComprobanteRetIVAImprentaDigital = new ComprobanteRetIVA();
                    ComprobanteRetIVAImprentaDigital.NumeroComprobanteRetencion = LibXml.GetPropertyString(vResult, "NumeroComprobanteRetencion");
                    ComprobanteRetIVAImprentaDigital.CodigoProveedor = LibXml.GetPropertyString(vResult, "CodigoProveedor");
                    ComprobanteRetIVAImprentaDigital.RetencionIvaEnviadaImpDigital = LibImportData.SNToBool(LibXml.GetPropertyString(vResult, "RetencionIvaEnviadaImpDigital"));
                    ComprobanteRetIVAImprentaDigital.SeHizoLaRetencionIVA = LibImportData.SNToBool(LibXml.GetPropertyString(vResult, "SeHizoLaRetencionIva"));
                    ComprobanteRetIVAImprentaDigital.ProveedorImprentaDigital = LibXml.GetPropertyString(vResult, "ProveedorImprentaDigital");
                    ComprobanteRetIVAImprentaDigital.TipoDeCxP = LibXml.GetPropertyString(vResult, "TipodeCxP");
                    ComprobanteRetIVAImprentaDigital.NumeroControlRetencionIvaImpDigital = LibXml.GetPropertyString(vResult, "NumeroControlRetencionIvaImpDigital");
                    ComprobanteRetIVAImprentaDigital.AnoAplicRetIVA= LibImportData.ToInt(LibXml.GetPropertyString(vResult, "AnoDeAplicacion"));
                    ComprobanteRetIVAImprentaDigital.MesAplicRetIVA = LibImportData.ToInt(LibXml.GetPropertyString(vResult, "MesDeAplicacion"));
                    ComprobanteRetIVAImprentaDigital.NumeroDeDocumento = GetNumeroComprobanteCompleto(ComprobanteRetIVAImprentaDigital.NumeroComprobanteRetencion, ComprobanteRetIVAImprentaDigital.AnoAplicRetIVA, ComprobanteRetIVAImprentaDigital.MesAplicRetIVA);
                    ComprobanteRetIVAImprentaDigital.MotivoDeAnulacionDeComprobante = LibXml.GetPropertyString(vResult, "MotivoDeAnulacionDeComprobante");
                    ComprobanteRetIVAImprentaDigital.StatusCxP = LibXml.GetPropertyString(vResult, "StatusCxP");
                } else {
                    throw new GalacException("El Documento N° " + LibConvert.ToStr(NumeroCxP) + " no existe.", eExceptionManagementType.Controlled);
                }
            } catch (GalacException) {
                throw;
            }
        }


        private string SqlLimpiarNroControl(ref StringBuilder refParametros) {
            DateTime vFechaInicioImprentaDigital = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Parametros", "FechaInicioImprentaDigital");
            LibGpParams vParam = new LibGpParams();
            StringBuilder vSql = new StringBuilder();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("Numero", NumeroFactura, 11);
            vParam.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
            refParametros = vParam.Get();
            vSql.AppendLine(" UPDATE");
            vSql.AppendLine(" factura ");
            vSql.AppendLine(" SET NumeroControl = ''");
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND Numero = @Numero ");
            vSql.AppendLine(" AND TipoDeDocumento = @TipoDeDocumento");
            vSql.AppendLine(" AND NumeroControl <> ''");
            vSql.AppendLine(" AND StatusFactura = " + insUtilSql.EnumToSqlValue((int)eStatusFactura.Emitida));
            vSql.AppendLine(" AND Fecha >= " + insUtilSql.ToSqlValue(vFechaInicioImprentaDigital));
            return vSql.ToString();
        }

        private string SqlDatosComprobanteRetencionIVA(ref StringBuilder refParametros) {
            LibGpParams vParam = new LibGpParams();
            StringBuilder vSql = new StringBuilder();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("NumeroCxP", NumeroCxP, 20);
            vParam.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
            refParametros = vParam.Get();
            vSql.AppendLine("SELECT");
            vSql.AppendLine("  ROUND(( cxP.MontoExento + cxP.MontoGravado + cxP.MontoIva  ) * cxP.CambioABolivares,2 ) AS TotalCXPComprobanteRetIva,");
            vSql.AppendLine("  ROUND(( cxP.MontoGravado + cxP.MontoIva  ) * cxP.CambioABolivares,2 ) AS TotalCXP,");
            vSql.AppendLine("  cxp.FechaAplicacionRetIVA,");
            vSql.AppendLine("  cxp.FechaVencimiento,");
            vSql.AppendLine("  cxp.MesDeAplicacion,");
            vSql.AppendLine("  cxp.AnoDeAplicacion,");
            vSql.AppendLine("  cxp.CodigoMoneda,");
            vSql.AppendLine("  cxp.TipoDeCxP,");
            vSql.AppendLine("  cxp.CodigoProveedor,");
            vSql.AppendLine("  cxp.NumeroComprobanteRetencion,");
            vSql.AppendLine("  cxP.PorcentajeRetencionAplicado,");
            vSql.AppendLine("  cxP.Fecha AS FechaDelDocOrigen,");
            vSql.AppendLine("  cxP.Numero AS NumeroDeDocumento,");
            vSql.AppendLine("  cxP.NumeroControl,");
            vSql.AppendLine("  (  CASE WHEN cxP.TipoDeCxP = '4' THEN cxP.Numero ELSE '' END ) AS NumeroDeNotaDebito,");
            vSql.AppendLine("  (  CASE WHEN cxP.TipoDeCxP = '3' THEN cxP.Numero ELSE '' END ) AS NumeroDeNotaCredito,");            
            vSql.AppendLine("  cxP.TipoDeTransaccion,");
            vSql.AppendLine("  cxP.MontoRetenido,");
            vSql.AppendLine("  cxP.NumeroDeFacturaAfectada,");
            vSql.AppendLine("  ROUND( cxP.MontoExento * cxP.CambioABolivares, 2) AS MontoExento,");
            vSql.AppendLine("  ROUND( cxP.MontoGravado * cxP.CambioABolivares,2 ) AS MontoGravado,");
            vSql.AppendLine("  ROUND( CASE WHEN cxP.MontoGravableAlicuotaGeneral <> 0 THEN cxP.MontoGravableAlicuotaGeneral ELSE CASE WHEN cxP.MontoGravableAlicuotaEspecial2 <> 0 THEN cxP.MontoGravableAlicuotaEspecial2 ELSE cxP.MontoGravableAlicuotaEspecial1 END END * cxP.CambioABolivares,2 ) AS MontoGravableAlicuotaGeneral,");
            vSql.AppendLine("  ROUND( cxP.MontoGravableAlicuota2 * cxP.CambioABolivares,2 ) AS MontoGravableAlicuota2,");
            vSql.AppendLine("  ROUND( cxP.MontoGravableAlicuota3 * cxP.CambioABolivares,2 ) AS MontoGravableAlicuota3 ,");
            vSql.AppendLine("  ( CASE WHEN cxp.AplicaIvaAlicuotaEspecial = 'N' THEN (");
            vSql.AppendLine("      SELECT");
            vSql.AppendLine("        TOP 1 alicuotaIVA.MontoAlicuotaGeneral");
            vSql.AppendLine("      FROM");
            vSql.AppendLine("        alicuotaIVA");
            vSql.AppendLine("      WHERE");
            vSql.AppendLine("        alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha");
            vSql.AppendLine("      ORDER BY");
            vSql.AppendLine("        alicuotaIVA.FechaDeInicioDeVigencia DESC");
            vSql.AppendLine("    ) ELSE CASE WHEN cxP.MontoGravableAlicuotaEspecial1 > 0 THEN cxP.PorcentajeIvaAlicuotaEspecial1 ELSE cxP.PorcentajeIvaAlicuotaEspecial2 END END ) As AlicuotaG,");
            vSql.AppendLine("  ( SELECT");
            vSql.AppendLine("      TOP 1 alicuotaIVA.MontoAlicuota2");
            vSql.AppendLine("    FROM");
            vSql.AppendLine("      alicuotaIVA");
            vSql.AppendLine("    WHERE");
            vSql.AppendLine("      alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha");
            vSql.AppendLine("    ORDER BY");
            vSql.AppendLine("      alicuotaIVA.FechaDeInicioDeVigencia DESC ) AS Alicuota2,");
            vSql.AppendLine("  ( SELECT");
            vSql.AppendLine("      TOP 1 alicuotaIVA.MontoAlicuota3");
            vSql.AppendLine("    FROM");
            vSql.AppendLine("      alicuotaIVA");
            vSql.AppendLine("    WHERE");
            vSql.AppendLine("      alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha");
            vSql.AppendLine("    ORDER BY");
            vSql.AppendLine("      alicuotaIVA.FechaDeInicioDeVigencia DESC ) AS Alicuota3,");
            vSql.AppendLine("  ROUND( cxP.MontoIva * cxP.CambioABolivares,2 ) AS MontoIva,");
            vSql.AppendLine("  ROUND( CASE WHEN cxP.MontoIVAAlicuotaGeneral <> 0 THEN cxP.MontoIVAAlicuotaGeneral ELSE CASE WHEN cxP.MontoIVAAlicuotaEspecial2 <> 0 THEN cxP.MontoIVAAlicuotaEspecial2 ELSE cxP.MontoIVAAlicuotaEspecial1 END END * cxP.CambioABolivares,2  ) AS MontoIVAAlicuotaGeneral,");
            vSql.AppendLine("  ROUND( cxP.MontoIVAAlicuota2 * cxP.CambioABolivares,2 ) AS MontoIVAAlicuota2,");
            vSql.AppendLine("  ROUND( cxP.MontoIVAAlicuota3 * cxP.CambioABolivares,2 ) AS MontoIVAAlicuota3,");
            vSql.AppendLine("  ROUND( cxP.MontoRetenido * cxP.CambioABolivares,2 ) AS MontoRetenido,");            
            vSql.AppendLine("  YEAR (cxP.FechaAplicacionRetIva) AS AnoAplicRetIVA,");
            vSql.AppendLine("  RIGHT( '0' + CAST( MONTH (cxP.FechaAplicacionRetIva) AS VARCHAR ), 2 ) AS MesAplicRetIVA,");
            vSql.AppendLine("  EsUnaCuentaATerceros,");
            vSql.AppendLine("  CodigoProveedorOriginalServicio");
            vSql.AppendLine(" FROM CxP");
            vSql.AppendLine("WHERE cxP.ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine(" AND cxP.Numero = @NumeroCxP");            
            return vSql.ToString();
        }

        private void BuscarDatosComprobanteRetencionIVA() {
            try {
                StringBuilder vParam = new StringBuilder();
                string vSql = SqlDatosComprobanteRetencionIVA(ref vParam);
                XElement vResult = LibBusiness.ExecuteSelect(vSql, vParam, "", 0);
                if (vResult != null && vResult.HasElements) {
                    ComprobanteRetIVAImprentaDigital = new ComprobanteRetIVA();
                    ComprobanteRetIVAImprentaDigital.TotalCXPComprobanteRetIva = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "TotalCXPComprobanteRetIva"), 2);
                    ComprobanteRetIVAImprentaDigital.TotalCXP = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "TotalCXP"), 2);
                    ComprobanteRetIVAImprentaDigital.CodigoProveedor = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "CodigoProveedor"));
                    ComprobanteRetIVAImprentaDigital.CodigoMoneda = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "CodigoMoneda"));
                    ComprobanteRetIVAImprentaDigital.FechaAplicacionRetIVA = LibImportData.ToDate(LibXml.GetPropertyString(vResult, "FechaAplicacionRetIVA"));
                    ComprobanteRetIVAImprentaDigital.FechaDeVencimiento = LibImportData.ToDate(LibXml.GetPropertyString(vResult, "FechaVencimiento"));
                    ComprobanteRetIVAImprentaDigital.MesDeAplicacion = LibImportData.ToInt(LibXml.GetPropertyString(vResult, "MesDeAplicacion"));
                    ComprobanteRetIVAImprentaDigital.AnoDeAplicacion = LibImportData.ToInt(LibXml.GetPropertyString(vResult, "AnoDeAplicacion"));
                    ComprobanteRetIVAImprentaDigital.NumeroComprobanteRetencion = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "NumeroComprobanteRetencion"));
                    ComprobanteRetIVAImprentaDigital.PorcentajeRetencionAplicado = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "PorcentajeRetencionAplicado"), 2);
                    ComprobanteRetIVAImprentaDigital.FechaDelDocOrigen = LibImportData.ToDate(LibXml.GetPropertyString(vResult, "FechaDelDocOrigen"));
                    ComprobanteRetIVAImprentaDigital.NumeroDeDocumento = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "NumeroDeDocumento"));
                    ComprobanteRetIVAImprentaDigital.NumeroDeNotaDebito = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "NumeroDeNotaDebito"));
                    ComprobanteRetIVAImprentaDigital.NumeroDeNotaCredito = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "NumeroDeNotaCredito"));
                    ComprobanteRetIVAImprentaDigital.NumeroControl = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "NumeroControl"));
                    ComprobanteRetIVAImprentaDigital.TipoDeTransaccion = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "TipoDeTransaccion"));
                    ComprobanteRetIVAImprentaDigital.TipoDeCxP = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "TipoDeCxP"));
                    ComprobanteRetIVAImprentaDigital.NumeroDeFacturaAfectada = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "NumeroDeFacturaAfectada"));
                    ComprobanteRetIVAImprentaDigital.MontoExento = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoExento"), 2);
                    ComprobanteRetIVAImprentaDigital.MontoGravado = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoGravado"), 2);
                    ComprobanteRetIVAImprentaDigital.MontoIva = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoIva"), 2);
                    ComprobanteRetIVAImprentaDigital.MontoGravableAlicuotaGeneral = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoGravableAlicuotaGeneral"), 2);
                    ComprobanteRetIVAImprentaDigital.MontoGravableAlicuota2 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoGravableAlicuota2"), 2);
                    ComprobanteRetIVAImprentaDigital.MontoGravableAlicuota3 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoGravableAlicuota3"), 2);
                    ComprobanteRetIVAImprentaDigital.AlicuotaG = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "AlicuotaG"), 2);
                    ComprobanteRetIVAImprentaDigital.Alicuota2 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "Alicuota2"), 2);
                    ComprobanteRetIVAImprentaDigital.Alicuota3 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "Alicuota2"), 3);
                    ComprobanteRetIVAImprentaDigital.MontoIVAAlicuotaGeneral = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoIVAAlicuotaGeneral"), 2);
                    ComprobanteRetIVAImprentaDigital.MontoIVAAlicuota2 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoIVAAlicuota2"), 2);
                    ComprobanteRetIVAImprentaDigital.MontoIVAAlicuota3 = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoIVAAlicuota3"), 2);
                    ComprobanteRetIVAImprentaDigital.MontoRetenido = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "MontoRetenido"), 2);
                    ComprobanteRetIVAImprentaDigital.AnoAplicRetIVA = LibImportData.ToInt(LibXml.GetPropertyString(vResult, "AnoDeAplicacion"));
                    ComprobanteRetIVAImprentaDigital.MesAplicRetIVA = LibImportData.ToInt(LibXml.GetPropertyString(vResult, "MesDeAplicacion"));
                    ComprobanteRetIVAImprentaDigital.EsUnacuentaDeTerceros = LibImportData.SNToBool(LibXml.GetPropertyString(vResult, "EsUnaCuentaATerceros"));
                    if (ComprobanteRetIVAImprentaDigital.EsUnacuentaDeTerceros) {
                        ComprobanteRetIVAImprentaDigital.CodigoProveedor = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "CodigoProveedorOriginalServicio"));
                    }
                    ComprobanteRetIVAImprentaDigital.NumeroComprobanteRetencion = GetNumeroComprobanteCompleto(ComprobanteRetIVAImprentaDigital.NumeroComprobanteRetencion, ComprobanteRetIVAImprentaDigital.AnoAplicRetIVA, ComprobanteRetIVAImprentaDigital.MesAplicRetIVA);
                } else {
                    throw new GalacException("No existen Datos de Retención de IVA para esta CxP, por favor revisar.", eExceptionManagementType.Controlled);
                }
            } catch (GalacException) {
                throw new GalacException("No existen Datos de Retención de IVA para esta CxP, por favor revisar.", eExceptionManagementType.Controlled);
            }
        }

        private string GetNumeroComprobanteCompleto(string valNumeroComprobante, int valAnoComprobante, int valMesComprobante) {
            return LibConvert.ToStr(valAnoComprobante) + LibString.Right("00" + LibConvert.ToStr(valMesComprobante), 2) + LibString.Right("00000000" + valNumeroComprobante, 8);
        }

        private string SqlDatosSujetoRetencionIVA(ref StringBuilder refParametros) {
            LibGpParams vParam = new LibGpParams();
            StringBuilder vSql = new StringBuilder();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("CodigoProveedor", ComprobanteRetIVAImprentaDigital.CodigoProveedor, 20);
            refParametros = vParam.Get();
            vSql.AppendLine("SELECT");
            vSql.AppendLine("  proveedor.Direccion,");
            vSql.AppendLine("  proveedor.Telefonos,");
            vSql.AppendLine("  proveedor.NumeroRIF,");
            vSql.AppendLine("  proveedor.NombreProveedor,");
            vSql.AppendLine("  proveedor.CodigoProveedor,");
            vSql.AppendLine("  proveedor.Email,");
            vSql.AppendLine("  proveedor.TipoDeProveedorDeLibrosFiscales ");
            vSql.AppendLine("FROM Adm.Proveedor");
            vSql.AppendLine("WHERE  Proveedor.ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine($"  AND Proveedor.CodigoProveedor = @CodigoProveedor");
            return vSql.ToString();
        }

        private void BuscarDatosSujetoDeRetencionIVA() {
            try {
                StringBuilder vParam = new StringBuilder();
                string vSql = SqlDatosSujetoRetencionIVA(ref vParam);
                XElement vResult = LibBusiness.ExecuteSelect(vSql, vParam, "", 0);
                if (vResult != null && vResult.HasElements) {
                    SujetoDeRetencionImpnretaDigital = new SujetoDeRetencion();
                    SujetoDeRetencionImpnretaDigital.CodigoProveedor = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "CodigoProveedor"));
                    SujetoDeRetencionImpnretaDigital.Direccion = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Direccion"));
                    SujetoDeRetencionImpnretaDigital.Telefono = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Telefono"));
                    SujetoDeRetencionImpnretaDigital.NumeroRIF = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "NumeroRIF"));
                    SujetoDeRetencionImpnretaDigital.NombreProveedor = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "NombreProveedor"));
                    SujetoDeRetencionImpnretaDigital.Email = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Email"));
                    SujetoDeRetencionImpnretaDigital.TipoDeProveedorDeLibrosFiscales = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "TipoDeProveedorDeLibrosFiscales"));
                }
            } catch (GalacException) {
                throw new GalacException("No existen datos del Sujeto de Retención de IVA para esta CxP, por favor revisar.", eExceptionManagementType.Controlled);
            }
        }

        public void LimpiarNroControl() {
            try {
                if (TipoDeDocumento == eTipoDocumentoFactura.Factura || TipoDeDocumento == eTipoDocumentoFactura.NotaDeCredito || TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito) {
                    StringBuilder vParam = new StringBuilder();
                    string vSql = SqlLimpiarNroControl(ref vParam);
                    LibBusiness.ExecuteUpdateOrDelete(vSql, vParam, "", 0);
                }
            } catch (GalacException) {
                throw;
            }
        }

        public virtual void ConfigurarDocumento() {
            if (TipoDocumentoImprentaDigital == eTipoDocumentoImprentaDigital.RetencionIVA) {
                BuscarDatosComprobanteRetencionIVA();
                BuscarDatosSujetoDeRetencionIVA();
            } else {
                BuscarDatosDeDocumentoParaEmitir();
                BuscarDatosDeDetalleDocumento();
                BuscarDatosDeCliente();
                BuscarDatosDelVendedor();
            }
        }

        private string LimpiarCaracteresNoValidos(string valInput) {
            string vResult = "";
            if (!LibString.IsNullOrEmpty(valInput)) {
                valInput = LibString.Trim(valInput);
                System.Text.RegularExpressions.Regex vListInvalidChars = new System.Text.RegularExpressions.Regex("[´|`|~|^|¨|'|\n|\r|\t]", System.Text.RegularExpressions.RegexOptions.Compiled);
                vResult = vListInvalidChars.Replace(valInput, "");
            }
            return vResult;
        }

        public virtual bool SincronizarDocumento() {
            bool vResult = false;
            if (TipoDocumentoImprentaDigital == eTipoDocumentoImprentaDigital.RetencionIVA) {
                vResult = !LibString.S1IsEqualToS2(NumeroControl, ComprobanteRetIVAImprentaDigital.NumeroControlRetencionIvaImpDigital);
            } else {
                vResult = !LibString.S1IsEqualToS2(NumeroControl, FacturaImprentaDigital.NumeroControl);
            }
            if (vResult) { //Emitida en ID, Emitida en SAW Sin Nro. Control                
                if (TipoDocumentoImprentaDigital == eTipoDocumentoImprentaDigital.RetencionIVA) {
                    vResult = ActualizaNroControYComprobantelEnCxP(vResult);
                } else {
                    vResult = ActualizaNroControlEnFactura();
                }
            } else if (LibString.S1IsEqualToS2(EstatusDocumento, "Enviada") && ComprobanteRetIVAImprentaDigital.StatusCxPAsEnum == eStatusDocumentoCxP.Anulado) { //Anulada en SAW, Emitida en ID                
                vResult = AnularDocumento();
            } else {
                vResult = true; // Todo al día
            }
            return vResult;
        }      

        private bool ActualizaNroControlEnCxC() {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            StringBuilder vSql = new StringBuilder();
            eTipoDeTransaccion vTipoCxC = TipoDocumentoFacturaToTipoTransaccionCxC(TipoDeDocumento);
            vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", NumeroFactura, 11);
            vParams.AddInEnum("TipoCxc", (int)vTipoCxC);
            vSql.AppendLine("UPDATE cxc ");
            vSql.AppendLine("SET NumeroControl = " + insUtilSql.ToSqlValue(NumeroControl));
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine(" AND NumeroDocumentoOrigen = @NumeroFactura");
            vSql.AppendLine(" AND TipoCxc = @TipoCxc ");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) >= 0;
            return vResult;
        }

        public bool ActualizaNroControlEnFactura() {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            StringBuilder vSql = new StringBuilder();
            HoraAsignacion = FormatoHora(HoraAsignacion);
            vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", NumeroFactura, 11);
            vParams.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
            vSql.AppendLine("UPDATE factura ");
            vSql.AppendLine("SET NumeroControl = " + insUtilSql.ToSqlValue(NumeroControl));
            vSql.AppendLine(", ProveedorImprentaDigital = " + insUtilSql.EnumToSqlValue((int)ProveedorImprentaDigital));
            vSql.AppendLine(", HoraModificacion = " + insUtilSql.ToSqlValue(HoraAsignacion));
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND Numero = @NumeroFactura");
            vSql.AppendLine(" AND TipoDeDocumento = @TipoDeDocumento ");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) >= 0;
            vResult &= ActualizaNroControlEnCxC();
            return vResult;
        }

        public bool ActualizaNroControYComprobantelEnCxP(bool valTodosLosValoresDeID ) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            StringBuilder vSql = new StringBuilder();            
            vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParams.AddInString("Numero", NumeroCxP, 11);
            vSql.AppendLine("UPDATE CxP ");
            vSql.AppendLine("SET RetencionIvaEnviadaImpDigital = " + insUtilSql.ToSqlValue(valTodosLosValoresDeID));
            if (valTodosLosValoresDeID) {
                vSql.AppendLine("   ,NumeroControlRetencionIvaImpDigital = " + insUtilSql.ToSqlValue(NumeroControl));
                vSql.AppendLine("   ,ProveedorImprentaDigital = " + insUtilSql.EnumToSqlValue((int)ProveedorImprentaDigital));
            }
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine(" AND Numero = @Numero");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) >= 0;
            return vResult;
        }

        string vSqlActualizaReservaMercanciaAlAnularFactura(ref StringBuilder refParams) {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParams.AddInString("Numero", NumeroFactura, 11);
            vParams.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
            refParams = vParams.Get();
            vSql.AppendLine("IF EXISTS (select f.Numero ");
            vSql.AppendLine("   FROM cotizacion c INNER JOIN factura f ON");
            vSql.AppendLine("   (c.ConsecutivoCompania=f.ConsecutivoCompania ");
            vSql.AppendLine("   AND c.numero = f.NoCotizacionDeOrigen)");
            vSql.AppendLine("   WHERE f.ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine("   AND f.Numero = @Numero ");
            vSql.AppendLine("   AND f.TipoDeDocumento = @TipoDeDocumento)");
            vSql.AppendLine("   BEGIN");
            vSql.AppendLine("       UPDATE ArticuloInventario ");
            vSql.AppendLine("       SET CantArtreservado = (CantArtreservado + rc.Cantidad)");
            vSql.AppendLine("       FROM cotizacion c INNER JOIN rengloncotizacion rc on");
            vSql.AppendLine("       (c.ConsecutivoCompania = rc.ConsecutivoCompania ");
            vSql.AppendLine("       AND c.Numero = rc.NumeroCotizacion)");
            vSql.AppendLine("       INNER JOIN");
            vSql.AppendLine("       factura f ON");
            vSql.AppendLine("       (c.ConsecutivoCompania = f.ConsecutivoCompania ");
            vSql.AppendLine("       AND c.numero = f.NoCotizacionDeOrigen)");
            vSql.AppendLine("       INNER JOIN renglonFactura rf ON");
            vSql.AppendLine("       (f.consecutivocompania = rf.consecutivocompania ");
            vSql.AppendLine("       AND f.Numero = rf.NumeroFactura ");
            vSql.AppendLine("       AND f.TipoDeDocumento = rf.TipoDeDocumento)");
            vSql.AppendLine("       INNER JOIN ArticuloInventario a ON");
            vSql.AppendLine("       (rf.ConsecutivoCompania = a.ConsecutivoCompania ");
            vSql.AppendLine("       AND rf.Articulo = a.Codigo ");
            vSql.AppendLine("       AND rc.CodigoArticulo = a.Codigo)");
            vSql.AppendLine("       WHERE f.ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine("       AND f.Numero = @Numero ");
            vSql.AppendLine("       AND f.TipoDeDocumento = @TipoDeDocumento");
            vSql.AppendLine("       UPDATE cotizacion ");
            vSql.AppendLine("       SET ReservarMercancia = " + insUtilSql.ToSqlValue(true));
            vSql.AppendLine("       FROM cotizacion c INNER JOIN factura f ON");
            vSql.AppendLine("       (c.ConsecutivoCompania = f.ConsecutivoCompania ");
            vSql.AppendLine("       AND c.numero = f.NoCotizacionDeOrigen)");
            vSql.AppendLine("       WHERE f.ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine("       AND f.Numero = @Numero ");
            vSql.AppendLine("       AND f.TipoDeDocumento = @TipoDeDocumento");
            vSql.AppendLine("   END ");
            vSql.AppendLine("ELSE");
            vSql.AppendLine("   BEGIN");
            vSql.AppendLine("       UPDATE ArticuloInventario ");
            vSql.AppendLine("       SET CantArtreservado = (CantArtreservado + rf.Cantidad)");
            vSql.AppendLine("       FROM");
            vSql.AppendLine("       factura f");
            vSql.AppendLine("       INNER JOIN renglonFactura rf ON");
            vSql.AppendLine("       (f.consecutivocompania = rf.consecutivocompania ");
            vSql.AppendLine("       AND f.Numero = rf.NumeroFactura ");
            vSql.AppendLine("       AND f.TipoDeDocumento = rf.TipoDeDocumento)");
            vSql.AppendLine("       INNER JOIN ArticuloInventario a ON");
            vSql.AppendLine("       (rf.ConsecutivoCompania = a.ConsecutivoCompania ");
            vSql.AppendLine("       AND rf.Articulo = a.Codigo)");
            vSql.AppendLine("       WHERE f.ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine("       AND f.Numero = @Numero ");
            vSql.AppendLine("       AND f.TipoDeDocumento = @TipoDeDocumento");
            vSql.AppendLine("   END");
            return vSql.ToString();
        }

        private bool ActualizaReservaMercanciaAlAnularFactura() {
            bool vResult = false;
            StringBuilder vParams = new StringBuilder();
            string vSql = vSqlActualizaReservaMercanciaAlAnularFactura(ref vParams);
            try {
                vResult = LibBusiness.ExecuteUpdateOrDelete(vSql, vParams, "", 0) > 0;
                return vResult;
            } catch (GalacException) {
                throw;
            }
        }


        private bool ActualizaFacturaGeneradaPorNE() {
            bool vResult = false;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            try {
                vParams.AddInString("NumeroBorrador", NumeroFactura, 11);
                vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
                vParams.AddInEnum("TipoDeDocumento", (int)eTipoDocumentoFactura.NotaEntrega);
                vSql.AppendLine(" UPDATE factura ");
                vSql.AppendLine(" Set EmitidaEnFacturaNumero = " + insUtilSql.ToSqlValue(""));
                vSql.AppendLine(" WHERE ");
                vSql.AppendLine(" EmitidaEnFacturaNumero = @NumeroBorrador ");
                vSql.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania ");
                vSql.AppendLine(" AND TipoDeDocumento = @TipoDeDocumento");
                vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) > 0;
                return vResult;
            } catch (GalacException) {
                throw;
            }
        }

        private bool RecalcularExistenciaDeInventarioPorAnulacionDeFactura() {
            bool vResult = false;
            IArticuloInventarioPdn vArticuloInventario = new clsArticuloInventarioNav();
            List<XElement> vListaDeArticulos = ListaDeArticulosParaRecalcularExistencia();
            vResult = vArticuloInventario.RecalcularExistencia(ConsecutivoCompania, FacturaImprentaDigital.CodigoAlmacen, vListaDeArticulos);
            return vResult;
        }

        private List<XElement> ListaDeArticulosParaRecalcularExistencia() {
            try {
                List<XElement> vResult = new List<XElement>();
                StringBuilder vSql = new StringBuilder();
                LibGpParams vParam = new LibGpParams();
                vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
                vParam.AddInString("Numero", NumeroFactura, 11);
                vParam.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
                vSql.AppendLine("SELECT");
                vSql.AppendLine(" articuloInventario.Codigo, ");
                vSql.AppendLine(" articuloInventario.TipoArticuloInv, ");
                vSql.AppendLine(" renglonFactura.Rollo, ");
                vSql.AppendLine(" renglonFactura.Serial, ");
                vSql.AppendLine(" factura.CodigoAlmacen ");
                vSql.AppendLine(" FROM ArticuloInventario");
                vSql.AppendLine(" INNER JOIN renglonFactura ON ");
                vSql.AppendLine(" articuloInventario.Codigo = renglonFactura.Articulo ");
                vSql.AppendLine(" AND articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania ");
                vSql.AppendLine(" INNER JOIN factura ON ");
                vSql.AppendLine(" factura.Numero = renglonFactura.NumeroFactura ");
                vSql.AppendLine(" AND factura.ConsecutivoCompania =renglonFactura.ConsecutivoCompania ");
                vSql.AppendLine(" AND factura.TipoDeDocumento = renglonFactura.TipoDeDocumento ");
                vSql.AppendLine(" WHERE factura.ConsecutivoCompania = @ConsecutivoCompania ");
                vSql.AppendLine(" AND factura.Numero = @Numero ");
                vSql.AppendLine(" AND factura.TipoDeDocumento = @TipoDeDocumento ");
                vSql.AppendLine(" AND ArticuloInventario.TipoDeArticulo <> " + insUtilSql.EnumToSqlValue((int)eTipoDeArticulo.Servicio));
                XElement xResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParam.Get(), "", 0);
                if (xResult != null && xResult.HasElements) {
                    vResult = xResult.Descendants("GpResult").ToList();
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }

        private string SqlBuscarCxC(bool valCxCPorCancelar, ref StringBuilder refParams) {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", NumeroFactura, 11);
            refParams = vParams.Get();
            vSql.AppendLine(" SELECT");
            vSql.AppendLine(" COUNT(Numero) AS CountCxC ");
            vSql.AppendLine(" FROM CXC ");
            vSql.AppendLine(" WHERE NumeroDocumentoOrigen = @NumeroFactura ");
            vSql.AppendLine(" AND  ConsecutivoCompania = @ConsecutivoCompania ");
            if (valCxCPorCancelar) {
                vSql.AppendLine(" AND (Status =" + insUtilSql.EnumToSqlValue((int)eStatusCXC.PORCANCELAR) + ")");
            }
            return vSql.ToString();
        }

        private int BuscarCxC(bool valCanceladas) {
            try {
                int vResult = 0;
                StringBuilder vParams = new StringBuilder();
                string vSql = SqlBuscarCxC(valCanceladas, ref vParams);
                XElement vData = LibBusiness.ExecuteSelect(vSql, vParams, "", 0);
                if (vData != null && vData.HasElements) {
                    vResult = LibConvert.ToInt(LibXml.GetPropertyString(vData, "CountCxC"));
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }

        private bool ExistenCxCPorCancelar() {
            bool vResult = false;
            int vCxCPorCancelar = BuscarCxC(true);
            int vCxCEmitidas = BuscarCxC(false);
            vResult = (vCxCEmitidas == vCxCPorCancelar);
            return vResult;
        }

        public string NumeroDocumento() {
            string vResult = FacturaImprentaDigital.Numero;
            if (FacturaImprentaDigital.TipoDeDocumentoAsEnum == eTipoDocumentoFactura.Factura) {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios")) {
                    vResult = LibString.SubString(vResult, LibString.IndexOf(vResult, '.') + 1);
                }
            }
            return vResult;
        }

        public string SerieDocumento() {
            string vResult = string.Empty;
            if (FacturaImprentaDigital.TipoDeDocumentoAsEnum == eTipoDocumentoFactura.Factura) {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios")) {
                    vResult = LibString.Left(FacturaImprentaDigital.Numero, LibString.IndexOf(FacturaImprentaDigital.Numero, '.'));
                }
            }
            return vResult;
        }

        public string NumeroDocumentoFacturaAfectada() {
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

        public string SerieDocumentoFacturaAfectada() {
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

        private eTipoDeTransaccion TipoDocumentoFacturaToTipoTransaccionCxC(eTipoDocumentoFactura valTipoDocumentoFactura) {
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
                default:
                    vTipoCxc = eTipoDeTransaccion.NOASIGNADO;
                    break;
            }
            return vTipoCxc;
        }

        string FormatoHora(string valHora) {
            string vResult = string.Empty;
            int vPosCorte = LibString.IndexOf(valHora, ":");
            if (vPosCorte < 2) {
                vResult = "00:00";
            } else if (!LibString.IsNullOrEmpty(valHora)) {
                vResult = LibString.Left(valHora, 2) + ":" + LibString.SubString(valHora, vPosCorte + 1, 2);
            }
            return vResult;
        }

        public string GeneraTrackingId() {
            int vSeed = DateTime.Now.Millisecond;
            int vNumberRnd = new Random(vSeed).Next(1, 1000000000); // Random de 10 carácteres
            return LibText.FillWithCharToLeft(LibConvert.ToStr(vNumberRnd), "0", 10);
        }

        public string GetFormatoDeHoraSimple(string valHora) {
            string vResult = string.Empty;
            int vPos = 0;
            if (LibString.S1IsInS2("a.", valHora)) {
                vPos = LibString.IndexOf(valHora, "a.") - 1;
                vResult = LibString.Left(valHora, vPos);
                vResult += ":00 am";
            } else if (LibString.S1IsInS2("p.", valHora)) {
                vPos = LibString.IndexOf(valHora, "p.") - 1;
                vResult = LibString.Left(valHora, vPos);
                vResult += ":00 pm";
            }
            return vResult;
        }

        public abstract bool EnviarDocumento();
        public abstract bool EnviarDocumentoPorEmail(string valNumeroControl, string valEmail);
        public abstract bool AnularDocumento();
        public abstract bool EstadoDocumento();
        public abstract bool EstadoLoteDocumentos();
    }
}


