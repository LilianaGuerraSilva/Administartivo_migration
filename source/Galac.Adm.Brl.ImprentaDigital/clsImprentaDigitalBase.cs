using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.Cliente;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Ccl.Vendedor;
using Galac.Saw.LibWebConnector;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;

namespace Galac.Adm.Brl.ImprentaDigital {
    public abstract class clsImprentaDigitalBase {

        #region Variables
        string _NumeroFactura;
        string _NumeroControl;
        eTipoDocumentoFactura _TipoDeDocumento;
        int _ConsecutivoCompania;
        clsLoginUser _LoginUser;

        #endregion Variables
        #region Propiedades   

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            private set { _ConsecutivoCompania = value; }
        }
        public virtual string NumeroFactura {
            get { return _NumeroFactura; }
            set { _NumeroFactura = value; }
        }

        public virtual string NumeroControl {
            get { return _NumeroControl; }
            set { _NumeroControl = value; }
        }

        public virtual eTipoDocumentoFactura TipoDeDocumento {
            get { return _TipoDeDocumento; }
            set { _TipoDeDocumento = value; }
        }

        internal virtual clsLoginUser LoginUser {
            get { return _LoginUser; }
            set { _LoginUser = value; }
        }

        internal virtual Cliente ClienteImprentaDigital { get; set; }
        internal virtual Vendedor VendedorImprentaDigital { get; set; }
        internal virtual FacturaRapida FacturaImprentaDigital { get; set; }
        internal virtual List<FacturaRapidaDetalle> DetalleFacturaImprentaDigital { get; set; }


        #endregion Propiedades

        public clsImprentaDigitalBase() {

        }

        public clsImprentaDigitalBase(eTipoDocumentoFactura initTipoDocumento, string initNumeroFactura) {
            LoginUser = new clsLoginUser();
            _NumeroFactura = initNumeroFactura;
            _TipoDeDocumento = initTipoDocumento;
            _ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            eProveedorImprentaDigital vProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
            clsImprentaDigitalSettings vImprentaDigitalSettings = new clsImprentaDigitalSettings();
            switch (vProveedorImprentaDigital) {
                case eProveedorImprentaDigital.TheFactoryHKA:
                    LoginUser.URL = vImprentaDigitalSettings.DireccionURL;
                    LoginUser.User = vImprentaDigitalSettings.Usuario;
                    LoginUser.UserKey = vImprentaDigitalSettings.CampoClave;
                    LoginUser.Password = vImprentaDigitalSettings.Clave;
                    LoginUser.PasswordKey = vImprentaDigitalSettings.CampoClave;
                    break;
                case eProveedorImprentaDigital.NoAplica:
                default:
                    break;
            }
        }

        private void BuscarDatosDeFactura() {
            try {
                XElement vResult;
                QAdvSql vSqlUtil = new QAdvSql("");
                LibGpParams vParam = new LibGpParams();
                vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
                vParam.AddInString("Numero", NumeroFactura, 11);
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
                vSql.AppendLine($" AND TipoDeDocumento IN({vSqlUtil.EnumToSqlValue((int)eTipoDocumentoFactura.Factura)},{vSqlUtil.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito)},{vSqlUtil.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeDebito)},{vSqlUtil.EnumToSqlValue((int)eTipoDocumentoFactura.NotaEntrega)})");
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
                    FacturaImprentaDigital.PorcentajeDescuento = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "PorcentajeDescuento"));
                    FacturaImprentaDigital.CambioABolivares = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "CambioABolivares"));
                    FacturaImprentaDigital.TotalRenglones = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "TotalRenglones"));
                    FacturaImprentaDigital.TotalMontoExento = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "TotalMontoExento"));
                    FacturaImprentaDigital.TotalBaseImponible = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "TotalBaseImponible"));
                    FacturaImprentaDigital.TotalIVA = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "TotalIVA"), 2);
                    FacturaImprentaDigital.TotalFactura = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "TotalFactura"));
                    FacturaImprentaDigital.NumeroFacturaAfectada = LibXml.GetPropertyString(vResult, "NumeroFacturaAfectada");
                    FacturaImprentaDigital.NumeroControl = LibXml.GetPropertyString(vResult, "NumeroControl");
                    FacturaImprentaDigital.NumeroDesde = LibXml.GetPropertyString(vResult, "NumeroDesde");
                    FacturaImprentaDigital.NumeroHasta = LibXml.GetPropertyString(vResult, "NumeroHasta");
                    FacturaImprentaDigital.NumeroDeCuotas = LibConvert.ToInt(LibXml.GetPropertyString(vResult, "NumeroDeCuotas"));
                    FacturaImprentaDigital.FormaDeCobro = LibXml.GetPropertyString(vResult, "FormaDeCobro");
                    FacturaImprentaDigital.FormaDePago = LibXml.GetPropertyString(vResult, "FormaDePago");
                    FacturaImprentaDigital.CodigoVendedor = LibXml.GetPropertyString(vResult, "CodigoVendedor");
                    FacturaImprentaDigital.CodigoCliente = LibXml.GetPropertyString(vResult, "CodigoCliente");
                    FacturaImprentaDigital.MontoGravableAlicuota1 = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "MontoGravableAlicuota1"));
                    FacturaImprentaDigital.MontoGravableAlicuota2 = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "MontoGravableAlicuota2"));
                    FacturaImprentaDigital.MontoGravableAlicuota3 = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "MontoGravableAlicuota3"));
                    FacturaImprentaDigital.PorcentajeAlicuota1 = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "PorcentajeAlicuota1"));
                    FacturaImprentaDigital.PorcentajeAlicuota2 = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "PorcentajeAlicuota2"));
                    FacturaImprentaDigital.PorcentajeAlicuota3 = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "PorcentajeAlicuota3"));
                    FacturaImprentaDigital.MontoIvaAlicuota1 = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "MontoIVAAlicuota1"));
                    FacturaImprentaDigital.MontoIvaAlicuota2 = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "MontoIVAAlicuota2"));
                    FacturaImprentaDigital.MontoIvaAlicuota3 = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "MontoIVAAlicuota3"));
                    FacturaImprentaDigital.AlicuotaIGTF = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "AlicuotaIGTF"));
                    FacturaImprentaDigital.IGTFML = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "IGTFML"));
                    FacturaImprentaDigital.BaseImponibleIGTF = LibConvert.ToDec(LibXml.GetPropertyString(vResult, "BaseImponibleIGTF"));
                } else {
                    throw new System.Exception("No existen datos para el documento a enviar");
                }
            } catch (System.Exception) {
                throw;
            }
        }

        private void BuscarDatosDeDetalleFactura() {
            QAdvSql vSqlUtil = new QAdvSql("");
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParam.AddInString("NumeroFactura", NumeroFactura, 11);
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
            XElement vResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParam.Get(), "", 0);
            if (vResult != null && vResult.HasElements) {
                List<XElement> ListDetaill = vResult.Descendants("GpResult").ToList();
                DetalleFacturaImprentaDigital = new List<FacturaRapidaDetalle>();
                foreach (XElement xRow in ListDetaill) {
                    FacturaRapidaDetalle iFacturaRapidaDetalle = new FacturaRapidaDetalle();
                    iFacturaRapidaDetalle.ConsecutivoRenglon = LibConvert.ToInt(LibXml.GetElementValueOrEmpty(xRow, "ConsecutivoRenglon"));
                    iFacturaRapidaDetalle.Articulo = LibXml.GetElementValueOrEmpty(xRow, "ConsecutivoRenglon");
                    iFacturaRapidaDetalle.Descripcion = LibXml.GetElementValueOrEmpty(xRow, "Descripcion");
                    iFacturaRapidaDetalle.Cantidad = LibConvert.ToDec(LibXml.GetElementValueOrEmpty(xRow, "Cantidad"));
                    iFacturaRapidaDetalle.PrecioSinIVA = LibConvert.ToDec(LibXml.GetElementValueOrEmpty(xRow, "PrecioSinIVA"));
                    iFacturaRapidaDetalle.PrecioConIVA = LibConvert.ToDec(LibXml.GetElementValueOrEmpty(xRow, "PrecioConIVA"));
                    iFacturaRapidaDetalle.PorcentajeDescuento = LibConvert.ToDec(LibXml.GetElementValueOrEmpty(xRow, "PorcentajeDescuento"));
                    iFacturaRapidaDetalle.TotalRenglon = LibConvert.ToDec(LibXml.GetElementValueOrEmpty(xRow, "TotalRenglon"));
                    iFacturaRapidaDetalle.PorcentajeBaseImponible = LibConvert.ToDec(LibXml.GetElementValueOrEmpty(xRow, "PorcentajeBaseImponible"));
                    iFacturaRapidaDetalle.PorcentajeAlicuota = LibConvert.ToDec(LibXml.GetElementValueOrEmpty(xRow, "PorcentajeAlicuota"));
                    iFacturaRapidaDetalle.AlicuotaIva = LibXml.GetElementValueOrEmpty(xRow, "AlicuotaIVA");
                    iFacturaRapidaDetalle.Serial = LibXml.GetElementValueOrEmpty(xRow, "Serial");
                    iFacturaRapidaDetalle.Rollo = LibXml.GetElementValueOrEmpty(xRow, "Rollo");
                    iFacturaRapidaDetalle.CampoExtraEnRenglonFactura1 = LibXml.GetElementValueOrEmpty(xRow, "CampoExtraEnRenglonFactura1");
                    iFacturaRapidaDetalle.CampoExtraEnRenglonFactura2 = LibXml.GetElementValueOrEmpty(xRow, "CampoExtraEnRenglonFactura2");
                    DetalleFacturaImprentaDigital.Add(iFacturaRapidaDetalle);
                }
            } else {
                throw new System.Exception("No existen datos para el detalle del documento a enviar");
            }
        }

        public string LimpiarCaracteresNoValidos(string valInput) {
            string vResult = "";
            System.Text.RegularExpressions.Regex replace_otherchar = new System.Text.RegularExpressions.Regex("[´|`|~|^]", System.Text.RegularExpressions.RegexOptions.Compiled);
            vResult = replace_otherchar.Replace(valInput, " ");
            return vResult;
        }

        private void BuscarDatosDeCliente() {
            QAdvSql vSqlUtil = new QAdvSql("");
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
                throw new System.Exception("No existen datos para el cliente del documento a enviar");
            }
        }

        private void BuscarDatosDelVendedor() {
            QAdvSql vSqlUtil = new QAdvSql("");
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
                throw new System.Exception("No existen datos para el vendedor del documento a enviar");
            }
        }

        public void BuscarDatosParaAnulacion() {
            try {
                XElement vResult;
                QAdvSql vSqlUtil = new QAdvSql("");
                LibGpParams vParam = new LibGpParams();
                vParam.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
                vParam.AddInString("Numero", NumeroFactura, 11);
                StringBuilder vSql = new StringBuilder();
                vSql.AppendLine(" SELECT");
                vSql.AppendLine(" factura.Talonario,");
                vSql.AppendLine(" factura.TipoDeDocumento,");
                vSql.AppendLine(" factura.NumeroControl,");
                vSql.AppendLine(" factura.MotivoDeAnulacion");
                vSql.AppendLine(" FROM factura");
                vSql.AppendLine(" WHERE factura.ConsecutivoCompania = @ConsecutivoCompania ");
                vSql.AppendLine(" AND factura.Numero = @Numero ");
                vSql.AppendLine($" AND TipoDeDocumento IN({vSqlUtil.EnumToSqlValue((int)eTipoDocumentoFactura.Factura)},{vSqlUtil.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito)},{vSqlUtil.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeDebito)},{vSqlUtil.EnumToSqlValue((int)eTipoDocumentoFactura.NotaEntrega)})");
                vResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParam.Get(), "", 0);
                if (vResult != null && vResult.HasElements) {
                    FacturaImprentaDigital = new FacturaRapida();
                    FacturaImprentaDigital.Talonario = LibXml.GetPropertyString(vResult, "Numero");
                    FacturaImprentaDigital.TipoDeDocumento = LibXml.GetPropertyString(vResult, "TipoDeDocumento");
                    FacturaImprentaDigital.NumeroControl = LibXml.GetPropertyString(vResult, "NumeroControl");
                    FacturaImprentaDigital.MotivoDeAnulacion = LibXml.GetPropertyString(vResult, "MotivoDeAnulacion");
                } else {
                    throw new System.Exception("No existen datos para el documento a anular");
                }
            } catch (System.Exception) {
                throw;
            }
        }

        public virtual void ConfigurarDocumento() {
            BuscarDatosDeFactura();
            BuscarDatosDeDetalleFactura();
            BuscarDatosDeCliente();
            BuscarDatosDelVendedor();
        }
        public abstract Task<bool> EnviarDocumento();
        public abstract Task<bool> AnularDocumento();
        public abstract Task<stLoginResq> EstadoDocumento();
        public abstract Task<bool> EstadoLoteDocumentos();
        public abstract Task<bool> SincronizarDocumentos();

    }
}
