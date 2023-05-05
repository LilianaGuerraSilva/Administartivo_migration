using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.Cliente;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Ccl.Vendedor;
using Galac.Saw.LibWebConnector;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Brl.ImprentaDigital {
    public abstract class clsImprentaDigitalBase {
     
        #region Propiedades   
        public int ConsecutivoCompania { get; set; }
        public string NumeroFactura { get; set; }
        public string NumeroControl { get; set; }
        public eTipoDocumentoFactura TipoDeDocumento { get; set; }
        public string CodigoRespuesta { get; set; }
        public string EstadoDocumentoRespuesta { get; set; }
        public clsLoginUser LoginUser { get; set; }
        public string HoraAsignacion { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public Cliente ClienteImprentaDigital { get; set; }
        public Vendedor VendedorImprentaDigital { get; set; }
        public FacturaRapida FacturaImprentaDigital { get; set; }
        public List<FacturaRapidaDetalle> DetalleFacturaImprentaDigital { get; set; }       
        public eProveedorImprentaDigital ProveedorImprentaDigital { get; set; }       
        #endregion Propiedades
        public clsImprentaDigitalBase() {
           
        }

        public clsImprentaDigitalBase(eTipoDocumentoFactura initTipoDocumento, string initNumeroFactura) {
            LoginUser = new clsLoginUser();
            NumeroFactura = initNumeroFactura;
            TipoDeDocumento = initTipoDocumento;
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));            
            clsImprentaDigitalSettings vImprentaDigitalSettings = new clsImprentaDigitalSettings();
            switch (ProveedorImprentaDigital) {
                case eProveedorImprentaDigital.TheFactoryHKA:
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

        private void BuscarDatosDeDocumentoParaEmitir() {
            try {
                XElement vResult;              
                LibGpParams vParam = new LibGpParams();
                vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
                vParam.AddInString("Numero", NumeroFactura, 11);
                vParam.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
                StringBuilder vSql = new StringBuilder();
                vSql.AppendLine(" SELECT");
                vSql.AppendLine(" factura.Numero");
                vSql.AppendLine(" ,factura.Fecha");
                vSql.AppendLine(" ,factura.HoraModificacion");
                vSql.AppendLine(" ,factura.FechaDeFacturaAfectada");
                vSql.AppendLine(" ,factura.Observaciones");
                vSql.AppendLine(" ,factura.CodigoMoneda AS MonedaDelDocumento");
                vSql.AppendLine(" ,factura.CodigoMonedaDeCobro AS MonedaDelCobro");
                vSql.AppendLine(" ,factura.StatusFactura");
                vSql.AppendLine(" ,factura.CondicionesDePago");
                vSql.AppendLine(" ,factura.TipoDeDocumento");
                vSql.AppendLine(" ,factura.TipoDeVenta");
                vSql.AppendLine(" ,factura.TipoDeTransaccion");
                vSql.AppendLine(" ,factura.Talonario");
                vSql.AppendLine(" ,ROUND(factura.PorcentajeDescuento,2) AS PorcentajeDescuento");
                vSql.AppendLine(" ,ROUND(factura.CambioABolivares,2) AS CambioABolivares");
                vSql.AppendLine(" ,ROUND(factura.CambioMostrarTotalEnDivisas,2) AS CambioMostrarTotalEnDivisas");
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
                vSql.AppendLine(" ,ROUND(factura.BaseImponibleIGTF,2) AS BaseImponibleIGTF");
                vSql.AppendLine(" FROM factura");
                vSql.AppendLine(" WHERE factura.ConsecutivoCompania = @ConsecutivoCompania ");
                vSql.AppendLine(" AND factura.Numero = @Numero ");
                vSql.AppendLine(" AND TipoDeDocumento = @TipoDeDocumento ");
                vResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParam.Get(), "", 0);
                if (vResult != null && vResult.HasElements) {
                    FacturaImprentaDigital = new FacturaRapida();
                    FacturaImprentaDigital.Numero = LibXml.GetPropertyString(vResult, "Numero");
                    FacturaImprentaDigital.Fecha = LibConvert.ToDate(LibXml.GetPropertyString(vResult, "Fecha"));
                    FacturaImprentaDigital.HoraModificacion = LibXml.GetPropertyString(vResult, "HoraModificacion");
                    FacturaImprentaDigital.FechaDeFacturaAfectada = LibConvert.ToDate(LibXml.GetPropertyString(vResult, "FechaDeFacturaAfectada"));
                    FacturaImprentaDigital.Observaciones = LibXml.GetPropertyString(vResult, "Observaciones");
                    FacturaImprentaDigital.CodigoMoneda = LibXml.GetPropertyString(vResult, "MonedaDelDocumento");
                    FacturaImprentaDigital.CodigoMonedaDeCobro = LibXml.GetPropertyString(vResult, "MonedaDelCobro");
                    FacturaImprentaDigital.StatusFactura = LibXml.GetPropertyString(vResult, "StatusFactura");
                    FacturaImprentaDigital.CondicionesDePago = LibXml.GetPropertyString(vResult, "CondicionesDePago");
                    FacturaImprentaDigital.TipoDeDocumento = LibXml.GetPropertyString(vResult, "TipoDeDocumento");
                    FacturaImprentaDigital.TipoDeVenta = LibXml.GetPropertyString(vResult, "TipoDeVenta");
                    FacturaImprentaDigital.TipoDeTransaccion = LibXml.GetPropertyString(vResult, "TipoDeTransaccion");
                    FacturaImprentaDigital.Talonario = LibXml.GetPropertyString(vResult, "Talonario");
                    FacturaImprentaDigital.PorcentajeDescuento = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "PorcentajeDescuento"));
                    FacturaImprentaDigital.CambioABolivares = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "CambioABolivares"));
                    FacturaImprentaDigital.CambioMostrarTotalEnDivisas = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "CambioMostrarTotalEnDivisas"));
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
                    FacturaImprentaDigital.BaseImponibleIGTF = LibImportData.ToDec(LibXml.GetPropertyString(vResult, "BaseImponibleIGTF"));
                } else {
                    throw new GalacException($"No existe un documento para enviar con el número {NumeroFactura} ", eExceptionManagementType.Controlled);
                }
            } catch (GalacException) {
                throw;
            }
        }

        private void BuscarDatosDeDetalleDocumento() {           
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("NumeroFactura", NumeroFactura, 11);
            vParam.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine(" ConsecutivoRenglon");
            vSql.AppendLine(" ,Articulo");
            vSql.AppendLine(" ,Descripcion");
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
            XElement vResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParam.Get(), "", 0);
            if (vResult != null && vResult.HasElements) {
                List<XElement> ListDetaill = vResult.Descendants("GpResult").ToList();
                DetalleFacturaImprentaDigital = new List<FacturaRapidaDetalle>();
                foreach (XElement xRow in ListDetaill) {
                    FacturaRapidaDetalle iFacturaRapidaDetalle = new FacturaRapidaDetalle();
                    iFacturaRapidaDetalle.ConsecutivoRenglon = LibConvert.ToInt(LibXml.GetElementValueOrEmpty(xRow, "ConsecutivoRenglon"));
                    iFacturaRapidaDetalle.Articulo = LibXml.GetElementValueOrEmpty(xRow, "Articulo");
                    iFacturaRapidaDetalle.Descripcion = LibXml.GetElementValueOrEmpty(xRow, "Descripcion");
                    iFacturaRapidaDetalle.Cantidad = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(xRow, "Cantidad"));
                    iFacturaRapidaDetalle.PrecioSinIVA = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(xRow, "PrecioSinIVA"));
                    iFacturaRapidaDetalle.PrecioConIVA = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(xRow, "PrecioConIVA"));
                    iFacturaRapidaDetalle.PorcentajeDescuento = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(xRow, "PorcentajeDescuento"));
                    iFacturaRapidaDetalle.TotalRenglon = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(xRow, "TotalRenglon"));
                    iFacturaRapidaDetalle.PorcentajeBaseImponible = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(xRow, "PorcentajeBaseImponible"));
                    iFacturaRapidaDetalle.PorcentajeAlicuota = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(xRow, "PorcentajeAlicuota"));
                    iFacturaRapidaDetalle.AlicuotaIva = LibXml.GetElementValueOrEmpty(xRow, "AlicuotaIVA");
                    iFacturaRapidaDetalle.Serial = LibXml.GetElementValueOrEmpty(xRow, "Serial");
                    iFacturaRapidaDetalle.Rollo = LibXml.GetElementValueOrEmpty(xRow, "Rollo");
                    iFacturaRapidaDetalle.CampoExtraEnRenglonFactura1 = LibXml.GetElementValueOrEmpty(xRow, "CampoExtraEnRenglonFactura1");
                    iFacturaRapidaDetalle.CampoExtraEnRenglonFactura2 = LibXml.GetElementValueOrEmpty(xRow, "CampoExtraEnRenglonFactura2");
                    DetalleFacturaImprentaDigital.Add(iFacturaRapidaDetalle);
                }
            } else {
                throw new GalacException("No existen datos para el detalle del documento a enviar", eExceptionManagementType.Controlled);
            }
        }

        private string LimpiarCaracteresNoValidos(string valInput) {
            string vResult = "";
            System.Text.RegularExpressions.Regex replace_otherchar = new System.Text.RegularExpressions.Regex("[´|`|~|^]", System.Text.RegularExpressions.RegexOptions.Compiled);
            vResult = replace_otherchar.Replace(valInput, " ");
            return vResult;
        }

        private void BuscarDatosDeCliente() {           
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("CodigoCliente", FacturaImprentaDigital.CodigoCliente, 10);
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine(" Codigo");
            vSql.AppendLine(" ,Nombre");
            vSql.AppendLine(" ,NumeroRif");
            vSql.AppendLine(" ,Direccion");
            vSql.AppendLine(" ,Ciudad");
            vSql.AppendLine(" ,ZonaPostal");
            vSql.AppendLine(" ,Telefono");
            vSql.AppendLine(" ,Email");
            vSql.AppendLine(" FROM Cliente ");
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND Codigo = @CodigoCliente ");
            XElement vResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParam.Get(), "", 0);
            if (vResult != null && vResult.HasElements) {
                ClienteImprentaDigital = new Cliente();
                ClienteImprentaDigital.Codigo = LibXml.GetPropertyString(vResult, "Codigo");
                ClienteImprentaDigital.Nombre = LimpiarCaracteresNoValidos(LibXml.GetPropertyString(vResult, "Nombre"));
                ClienteImprentaDigital.NumeroRIF = LibXml.GetPropertyString(vResult, "NumeroRif");
                ClienteImprentaDigital.Direccion = LibXml.GetPropertyString(vResult, "Direccion");
                ClienteImprentaDigital.Ciudad = LibXml.GetPropertyString(vResult, "Ciudad");
                ClienteImprentaDigital.ZonaPostal = LibXml.GetPropertyString(vResult, "ZonaPostal");
                ClienteImprentaDigital.Telefono = LibXml.GetPropertyString(vResult, "Telefono");
                ClienteImprentaDigital.Email = LibXml.GetPropertyString(vResult, "Email");
            } else {
                throw new GalacException("No existen datos para el cliente del documento a enviar", eExceptionManagementType.Controlled);
            }
        }

        private void BuscarDatosDelVendedor() {          
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("CodigoVendedor", FacturaImprentaDigital.CodigoVendedor, 10);
            StringBuilder vSql = new StringBuilder();
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
            XElement vResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParam.Get(), "", 0);
            if (vResult != null && vResult.HasElements) {
                VendedorImprentaDigital = new Vendedor();
                VendedorImprentaDigital.Codigo = LibXml.GetPropertyString(vResult, "Codigo");
                VendedorImprentaDigital.Nombre = LibXml.GetPropertyString(vResult, "Nombre");
                VendedorImprentaDigital.RIF = LibXml.GetPropertyString(vResult, "RIF");
                VendedorImprentaDigital.Direccion = LibXml.GetPropertyString(vResult, "Direccion");
                VendedorImprentaDigital.Ciudad = LibXml.GetPropertyString(vResult, "Ciudad");
                VendedorImprentaDigital.ZonaPostal = LibXml.GetPropertyString(vResult, "ZonaPostal");
                VendedorImprentaDigital.Telefono = LibXml.GetPropertyString(vResult, "Telefono");
                VendedorImprentaDigital.email = LibXml.GetPropertyString(vResult, "Email");
            } else {
                throw new GalacException("No existen datos para el vendedor del documento a enviar", eExceptionManagementType.Controlled);
            }
        }

        public void ObtenerDatosDocumentoEmitido() {
            try {
                XElement vResult;
                QAdvSql vSqlUtil = new QAdvSql("");
                LibGpParams vParam = new LibGpParams();
                vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
                vParam.AddInString("Numero", NumeroFactura, 11);
                vParam.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
                StringBuilder vSql = new StringBuilder();
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
                vSql.AppendLine(" factura.CodigoAlmacen ");
                vSql.AppendLine(" FROM factura");
                vSql.AppendLine(" WHERE factura.ConsecutivoCompania = @ConsecutivoCompania ");
                vSql.AppendLine(" AND factura.Numero = @Numero ");
                vSql.AppendLine(" AND TipoDeDocumento = @TipoDeDocumento");
                vResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParam.Get(), "", 0);
                if (vResult != null && vResult.HasElements) {
                    FacturaImprentaDigital = new FacturaRapida();
                    FacturaImprentaDigital.Talonario = ""; // Hay que revisar NO APLICA como serie
                    FacturaImprentaDigital.TipoDeDocumento = LibXml.GetPropertyString(vResult, "TipoDeDocumento");
                    FacturaImprentaDigital.NumeroControl = LibXml.GetPropertyString(vResult, "NumeroControl");
                    FacturaImprentaDigital.Numero = LibXml.GetPropertyString(vResult, "Numero");
                    FacturaImprentaDigital.MotivoDeAnulacion = LibXml.GetPropertyString(vResult, "MotivoDeAnulacion");
                    FacturaImprentaDigital.StatusFactura = LibXml.GetPropertyString(vResult, "StatusFactura");
                    FacturaImprentaDigital.GeneradaPorNotaEntregaAsBool = LibConvert.ToInt(LibXml.GetPropertyString(vResult, "GeneradaPorNotaEntrega")) == 1; // Hay que corregir la clase base
                    FacturaImprentaDigital.ReservarMercanciaAsBool = LibConvert.SNToBool(LibXml.GetPropertyString(vResult, "ReservarMercancia"));
                    FacturaImprentaDigital.CodigoAlmacen = LibXml.GetPropertyString(vResult, "CodigoAlmacen");
                    FacturaImprentaDigital.EmitidaEnFacturaNumero = LibXml.GetPropertyString(vResult, "EmitidaEnFacturaNumero");
                } else {
                    throw new GalacException("El Documento N° " + LibConvert.ToStr(NumeroFactura) + " no existe.", eExceptionManagementType.Controlled);
                }
            } catch (GalacException) {
                throw;
            }
        }

        public virtual void ConfigurarDocumento() {
            BuscarDatosDeDocumentoParaEmitir();
            BuscarDatosDeDetalleDocumento();
            BuscarDatosDeCliente();
            BuscarDatosDelVendedor();
        }

        public bool SincronizarDocumentosBase() {
            bool vResult = false;
            if (!LibString.S1IsEqualToS2(NumeroControl, FacturaImprentaDigital.NumeroControl)) {
                vResult = ActualizaNroControlYProveedorImprentaDigital();
            } else if (LibString.S1IsEqualToS2(EstadoDocumentoRespuesta, "Enviada") && FacturaImprentaDigital.StatusFacturaAsEnum == eStatusFactura.Anulada) {
                vResult &= AnularDocumento();
            } else if (LibString.S1IsEqualToS2(EstadoDocumentoRespuesta, "Anulada") && FacturaImprentaDigital.StatusFacturaAsEnum == eStatusFactura.Emitida) {
                vResult &= AnularFacturasYCxC();
            } else {
                vResult = true;
            }
            return vResult;
        }

        public bool ActualizaNroControlYProveedorImprentaDigital() {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            QAdvSql insUtilSql = new QAdvSql("");
            vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", NumeroFactura, 11);
            vParams.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE factura ");
            vSql.AppendLine("SET NumeroControl = " + insUtilSql.ToSqlValue(NumeroControl) + ",");
            vSql.AppendLine("ProveedorImprentaDigital = " + insUtilSql.EnumToSqlValue((int)ProveedorImprentaDigital));
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania AND ");
            vSql.AppendLine("Numero = @NumeroFactura AND ");
            vSql.AppendLine("TipoDeDocumento = @TipoDeDocumento ");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) >= 0;
            vSql.Clear();
            // Actualiza Nro Control en CxC
            vParams = new LibGpParams();
            eTipoDeTransaccion vTipoCxC = TipoDocumentoFacturaToTipoTransaccionCxC(TipoDeDocumento);
            vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", NumeroFactura, 11);
            vParams.AddInEnum("TipoCxc", (int)vTipoCxC);
            vSql.AppendLine("UPDATE cxc ");
            vSql.AppendLine("SET NumeroControl = " + insUtilSql.ToSqlValue(NumeroControl));
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania AND ");
            vSql.AppendLine("NumeroDocumentoOrigen = @NumeroFactura AND ");
            vSql.AppendLine("TipoCxc = @TipoCxc ");
            vResult = vResult & LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) >= 0;
            return vResult;
        }

        private bool AnularFacturasYCxC() {
            bool vResult = false;
            string vListaCxC = string.Empty;
            vResult = ObtenerEstatusCxC(ref vListaCxC);
            if (vResult) {
                if (AnularCxCOrigenFactura()) {
                    vResult = vResult & AnularFactura();
                    if (vResult) {
                        if (FacturaImprentaDigital.GeneradaPorNotaEntregaAsBool) {
                            vResult = vResult & ActualizaFacturaGeneradaPorNE();
                        } else {
                            vResult = vResult & RecalcularExistenciaDeInventarioPorAnulacionDeFactura();
                        }
                        if (FacturaImprentaDigital.ReservarMercanciaAsBool) {
                            vResult = vResult & ActualizaReservaMercanciaAlAnularFactura();
                        }
                    }
                }
            }
            return vResult;
        }

        private bool ActualizaReservaMercanciaAlAnularFactura() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insUtilSql = new QAdvSql("");
            LibGpParams vParams = new LibGpParams();
            bool vResult = false;
            try {
                vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
                vParams.AddInString("Numero", NumeroFactura, 11);
                vParams.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
                vSql.AppendLine("IF EXISTS (select f.Numero ");
                vSql.AppendLine(" FROM cotizacion c INNER JOIN factura f ON");
                vSql.AppendLine(" (c.ConsecutivoCompania=f.ConsecutivoCompania AND c.numero = f.NoCotizacionDeOrigen)");
                vSql.AppendLine(" WHERE f.ConsecutivoCompania = @ConsecutivoCompania AND ");
                vSql.AppendLine(" f.Numero = @Numero AND ");
                vSql.AppendLine(" f.TipoDeDocumento = @TipoDeDocumento)");
                vSql.AppendLine(" BEGIN");
                vSql.AppendLine(" UPDATE ArticuloInventario ");
                vSql.AppendLine(" SET CantArtreservado = (CantArtreservado + rc.Cantidad)");
                vSql.AppendLine(" FROM cotizacion c INNER JOIN rengloncotizacion rc on");
                vSql.AppendLine(" (c.ConsecutivoCompania = rc.ConsecutivoCompania AND c.Numero = rc.NumeroCotizacion)");
                vSql.AppendLine(" INNER JOIN");
                vSql.AppendLine(" factura f ON");
                vSql.AppendLine(" (c.ConsecutivoCompania = f.ConsecutivoCompania AND c.numero = f.NoCotizacionDeOrigen)");
                vSql.AppendLine(" INNER JOIN renglonFactura rf ON");
                vSql.AppendLine(" (f.consecutivocompania = rf.consecutivocompania AND ");
                vSql.AppendLine(" f.Numero = rf.NumeroFactura AND f.TipoDeDocumento = rf.TipoDeDocumento)");
                vSql.AppendLine(" INNER JOIN ArticuloInventario a ON");
                vSql.AppendLine(" (rf.ConsecutivoCompania = a.ConsecutivoCompania AND ");
                vSql.AppendLine(" rf.Articulo = a.Codigo AND rc.CodigoArticulo = a.Codigo)");
                vSql.AppendLine(" WHERE f.ConsecutivoCompania = @ConsecutivoCompania AND");
                vSql.AppendLine(" f.Numero = @Numero AND");
                vSql.AppendLine(" f.TipoDeDocumento = @TipoDeDocumento");
                vSql.AppendLine(" UPDATE cotizacion ");
                vSql.AppendLine(" SET ReservarMercancia = " + insUtilSql.ToSqlValue(true));
                vSql.AppendLine(" FROM cotizacion c INNER JOIN factura f ON");
                vSql.AppendLine(" (c.ConsecutivoCompania = f.ConsecutivoCompania AND c.numero = f.NoCotizacionDeOrigen)");
                vSql.AppendLine(" WHERE f.ConsecutivoCompania = @ConsecutivoCompania AND");
                vSql.AppendLine(" f.Numero = @Numero AND");
                vSql.AppendLine(" f.TipoDeDocumento = @TipoDeDocumento");
                vSql.AppendLine(" END");
                vSql.AppendLine(" ELSE");
                vSql.AppendLine(" BEGIN");
                vSql.AppendLine(" UPDATE ArticuloInventario ");
                vSql.AppendLine(" SET CantArtreservado = (CantArtreservado + rf.Cantidad)");
                vSql.AppendLine(" FROM");
                vSql.AppendLine(" factura f");
                vSql.AppendLine(" INNER JOIN renglonFactura rf ON");
                vSql.AppendLine(" (f.consecutivocompania = rf.consecutivocompania AND ");
                vSql.AppendLine(" f.Numero = rf.NumeroFactura AND f.TipoDeDocumento = rf.TipoDeDocumento)");
                vSql.AppendLine(" INNER JOIN ArticuloInventario a ON");
                vSql.AppendLine(" (rf.ConsecutivoCompania = a.ConsecutivoCompania AND rf.Articulo = a.Codigo)");
                vSql.AppendLine(" WHERE f.ConsecutivoCompania = @ConsecutivoCompania AND ");
                vSql.AppendLine(" f.Numero = @Numero AND ");
                vSql.AppendLine(" f.TipoDeDocumento = @TipoDeDocumento");
                vSql.AppendLine(" END");
                vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) > 0;
                return vResult;
            } catch (GalacException) {
                throw;
            }
        }

        private bool AnularFactura() {
            bool vResult = false;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            QAdvSql insUtilSql = new QAdvSql("");
            try {
                vParams.AddInString("NumeroFactura", NumeroFactura, 11);
                vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
                vParams.AddInEnum("StatusFactura", (int)eStatusFactura.Emitida);
                vParams.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
                vSql.AppendLine(" UPDATE factura ");
                vSql.AppendLine(" Set StatusFactura = " + insUtilSql.EnumToSqlValue((int)eStatusFactura.Anulada));
                vSql.AppendLine(" ,EsDiferida = " + insUtilSql.ToSqlValue(false));
                vSql.AppendLine(" ,NombreOperador = " + insUtilSql.ToSqlValue(((CustomIdentity)Thread.CurrentPrincipal.Identity).Login));
                vSql.AppendLine(" ,FechaAnulacion = " + insUtilSql.ToSqlValue(FechaAsignacion));
                vSql.AppendLine(" ,FechaUltimaModificacion = " + insUtilSql.ToSqlValue(LibDate.Today()));
                vSql.AppendLine(" WHERE ");
                vSql.AppendLine(" Numero = @NumeroFactura ");
                vSql.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania ");
                vSql.AppendLine(" AND StatusFactura =  @StatusFactura");
                vSql.AppendLine(" AND TipoDeDocumento = @TipoDeDocumento");
                vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) > 0;
                return vResult;
            } catch (GalacException) {
                throw;
            }
        }

        private bool ActualizaFacturaGeneradaPorNE() {
            bool vResult = false;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            QAdvSql insUtilSql = new QAdvSql("");
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

        private bool AnularCxCOrigenFactura() {
            bool vResult = false;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            eTipoDeTransaccion vTipoCxC = TipoDocumentoFacturaToTipoTransaccionCxC(TipoDeDocumento);
            QAdvSql insUtilSql = new QAdvSql("");
            string vListaCxC = string.Empty;
            vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", NumeroFactura, 11);
            vParams.AddInEnum("Status", (int)eStatusCXC.PORCANCELAR);
            vParams.AddInEnum("TipoCxc", (int)vTipoCxC);
            vParams.AddInEnum("Origen", (int)Adm.Ccl.Venta.eOrigenFacturacionOManual.Factura);
            vSql.AppendLine(" UPDATE CxC");
            vSql.AppendLine(" SET Status = " + insUtilSql.EnumToSqlValue((int)eStatusCXC.ANULADO));
            vSql.AppendLine(" ,NombreOperador = " + insUtilSql.ToSqlValue(((CustomIdentity)Thread.CurrentPrincipal.Identity).Login));
            vSql.AppendLine(" ,FechaAnulacion = " + insUtilSql.ToSqlValue(FechaAsignacion));
            vSql.AppendLine(" ,FechaUltimaModificacion = " + insUtilSql.ToSqlValue(LibDate.Today()));
            vSql.AppendLine(" WHERE ");
            vSql.AppendLine(" NumeroDocumentoOrigen = @NumeroFactura ");
            vSql.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND TipoCxc = @TipoCxc");
            vSql.AppendLine(" AND Status = @Status");
            vSql.AppendLine(" AND Origen = @Origen");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) > 0;
            return vResult;
        }

        private bool RecalcularExistenciaDeInventarioPorAnulacionDeFactura() {
            bool vResult = false;
            IArticuloInventarioPdn vArticuloInventario = new clsArticuloInventarioNav();
            List<XElement> vListaDeArticulos = ListaDeArticulos();
            vResult = vArticuloInventario.RecalcularExistencia(ConsecutivoCompania, FacturaImprentaDigital.CodigoAlmacen, vListaDeArticulos);
            return vResult;
        }

        private List<XElement> ListaDeArticulos() {
            try {
                List<XElement> vResult = new List<XElement>();
                StringBuilder vSql = new StringBuilder();
                LibGpParams vParam = new LibGpParams();
                vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
                vParam.AddInString("Numero", NumeroFactura, 11);
                vParam.AddInEnum("TipoDeDocumento", (int)TipoDeDocumento);
                vSql.AppendLine("SELECT");
                vSql.AppendLine(" ArticuloInventario.Codigo, ");
                vSql.AppendLine(" ArticuloInventario.Descripcion, ");
                vSql.AppendLine(" ArticuloInventario.TipoDeArticulo, ");
                vSql.AppendLine(" ArticuloInventario.TipoArticuloInv, ");
                vSql.AppendLine(" renglonFactura.Rollo, ");
                vSql.AppendLine(" renglonFactura.Serial, ");
                vSql.AppendLine(" factura.CodigoAlmacen ");
                vSql.AppendLine(" FROM ArticuloInventario");
                vSql.AppendLine(" INNER JOIN renglonFactura ON ");
                vSql.AppendLine(" ArticuloInventario.Codigo = renglonFactura.Articulo AND ");
                vSql.AppendLine(" ArticuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania ");
                vSql.AppendLine(" INNER JOIN factura ON ");
                vSql.AppendLine(" factura.Numero =renglonFactura.NumeroFactura  AND ");
                vSql.AppendLine(" factura.ConsecutivoCompania =renglonFactura.ConsecutivoCompania AND ");
                vSql.AppendLine(" factura.TipoDeDocumento = renglonFactura.TipoDeDocumento ");
                vSql.AppendLine(" WHERE factura.ConsecutivoCompania = @ConsecutivoCompania ");
                vSql.AppendLine(" AND factura.Numero = @Numero ");
                vSql.AppendLine(" AND factura.TipoDeDocumento = @TipoDeDocumento ");
                XElement xResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParam.Get(), "", 0);
                if (xResult != null && xResult.HasElements) {
                    vResult = xResult.Descendants("GpResult").ToList();
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }

        private bool ObtenerEstatusCxC(ref string refListCxC) {
            bool vResult = false;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            QAdvSql insUtilSql = new QAdvSql("");
            int vCxCPorFacturaPorCancelar = 0;
            int vCxCPorFacturaEmitidas = 0;
            string vListaCxC = string.Empty;
            vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", NumeroFactura, 11);
            vParams.AddInEnum("Status", (int)eStatusCXC.PORCANCELAR);
            vSql.AppendLine(" SELECT");
            vSql.AppendLine(" COUNT(Numero) AS CXCPorCancelar ");
            vSql.AppendLine(" FROM CXC ");
            vSql.AppendLine(" WHERE NumeroDocumentoOrigen = @NumeroFactura ");
            vSql.AppendLine(" AND  ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND Status= @Status ");
            vSql.AppendLine(" AND NumeroControl <> " + insUtilSql.ToSqlValue(""));
            XElement xResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
            if (xResult != null && xResult.HasElements) {
                vCxCPorFacturaPorCancelar = LibConvert.ToInt(LibXml.GetPropertyString(xResult, "CXCPorCancelar"));
            }
            vSql.Clear();
            vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", NumeroFactura, 11);
            vSql.AppendLine(" SELECT");
            vSql.AppendLine(" COUNT(Numero) AS CXCPorCancelar ");
            vSql.AppendLine(" FROM CXC ");
            vSql.AppendLine(" WHERE NumeroDocumentoOrigen = @NumeroFactura ");
            vSql.AppendLine(" AND  ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND NumeroControl <> " + insUtilSql.ToSqlValue(""));
            xResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
            if (xResult != null && xResult.HasElements) {
                vCxCPorFacturaEmitidas = LibConvert.ToInt(LibXml.GetPropertyString(xResult, "CXCPorCancelar"));
            }
            vResult = (vCxCPorFacturaEmitidas - vCxCPorFacturaPorCancelar) == 0;
            if (vCxCPorFacturaPorCancelar > 0) {
                vSql.Clear();
                vParams = new LibGpParams();
                vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
                vParams.AddInString("NumeroFactura", NumeroFactura, 11);
                vSql.AppendLine(" SELECT");
                vSql.AppendLine(" Numero ");
                vSql.AppendLine(" FROM CXC ");
                vSql.AppendLine(" WHERE NumeroDocumentoOrigen = @NumeroFactura ");
                vSql.AppendLine(" AND  ConsecutivoCompania = @ConsecutivoCompania ");
                vSql.AppendLine(" AND Status IN(" + insUtilSql.EnumToSqlValue((int)eStatusCXC.CANCELADO) + "," + insUtilSql.EnumToSqlValue((int)eStatusCXC.ABONADO) + ")");
                vSql.AppendLine(" AND NumeroControl <> " + insUtilSql.ToSqlValue(""));
                xResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
                if (xResult != null && xResult.HasElements) {
                    List<XElement> xResultToList = xResult.Descendants("GpResult").ToList();
                    foreach (XElement vCxc in xResultToList) {
                        vListaCxC = LibXml.GetElementValueOrEmpty(vCxc, "Numero") + ", " + vListaCxC;
                    }
                    refListCxC = vListaCxC;
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

        public abstract bool EnviarDocumento();
        public abstract bool AnularDocumento();
        public abstract bool EstadoDocumento();
        public abstract bool EstadoLoteDocumentos();
        public abstract bool SincronizarDocumentos();
    }
}


