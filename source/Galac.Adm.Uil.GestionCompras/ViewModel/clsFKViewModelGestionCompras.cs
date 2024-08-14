using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Saw.Ccl.Tablas;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Comun.Ccl.TablasLey;
using Galac.Contab.Ccl.WinCont;
using LibGalac.Aos.Base;
using Galac.Adm.Uil.GestionCompras.Properties;
using Galac.Saw.Ccl.Inventario;
using Galac.Comun.Ccl.TablasGen;
using Galac.Saw.Uil.Inventario.ViewModel;
//using Galac.Adm.Ccl.Venta;
namespace Galac.Adm.Uil.GestionCompras.ViewModel {

    public class FkProveedorViewModel : IFkProveedorViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Código", IsForSearch = false)]
        public string CodigoProveedor { get; set; }
        [LibGridColum("Nombre Proveedor", Width = 200)]
        public string NombreProveedor { get; set; }
        [LibGridColum(HeaderResourceName = "lblEtiquetaNumero", HeaderResourceType = typeof(Resources))]
        public string NumeroRIF { get; set; }
        public string NumeroNIT { get; set; }
        //[LibGridColum("Código Retención")]
        public string CodigoRetencionUsual { get; set; }
        [LibGridColum("Contacto")]
        public string Contacto { get; set; }
        public Ccl.CajaChica.eTipodePersonaRetencion TipoDePersona { get; set; }
        public decimal PorcentajeRetencionIVA { get; set; }
        public string CuentaContableCxP { get; set; }
        public string CuentaContableGastos { get; set; }
        public string CuentaContableAnticipo { get; set; }
        public string Beneficiario { get; set; }
        public string UsarBeneficiarioImpCheq { get; set; }
        public string NumeroCuentaBancaria { get; set; }
        public string CodigoContribuyente { get; set; }
        public string NumeroRUC { get; set; }
        public string Direccion { get; set; }
        [LibGridColum("Teléfonos")]
        public string Telefonos { get; set; }
        public string Email { get; set; }
        public string NombrePaisResidencia { get; set; }
        public string PaisConveniosSunat { get; set; }
    }

    public class FkCuentaViewModel : IFkCuentaViewModel {
        public int ConsecutivoPeriodo { get; set; }
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Descripción")]
        public string Descripcion { get; set; }
        [LibGridColum("Naturaleza", eGridColumType.Enum, PrintingMemberPath = "NaturalezaDeLaCuentaStr")]
        public eNaturalezaDeLaCuenta NaturalezaDeLaCuenta { get; set; }
        [LibGridColum("De Titulo?", eGridColumType.YesNo)]
        public bool TieneSubCuentas { get; set; }
        [LibGridColum("Tiene Auxiliar", eGridColumType.YesNo)]
        public bool TieneAuxiliar { get; set; }
        [LibGridColum("Grupo Auxiliar", eGridColumType.Enum, PrintingMemberPath = "GrupoAuxiliarStr")]
        public eGrupoAuxiliar GrupoAuxiliar { get; set; }
        [LibGridColum("Es Activo Fijo", eGridColumType.YesNo)]
        public bool EsActivoFijo { get; set; }
        [LibGridColum("Codigo Grupo Activo")]
        public int CodigoGrupoActivo { get; set; }
        public eMetodoAjuste MetodoAjuste { get; set; }
    }

    public class FkTipoProveedorViewModel : IFkTipoProveedorViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Nombre", Width = 200)]
        public string Nombre { get; set; }
    }

    public class FkTablaRetencionViewModel : IFkTablaRetencionViewModel {
        [LibGridColum("Tipo De Persona", Width = 200, PrintingMemberPath = "TipoDePersonaStr", Type = eGridColumType.Enum)]
        public Ccl.CajaChica.eTipodePersonaRetencion TipoDePersona { get; set; }
        [LibGridColum("Código", Width = 150)]
        public string Codigo { get; set; }
        public string CodigoSeniat { get; set; }
        [LibGridColum("Tipo De Pago", Width = 300)]
        public string TipoDePago { get; set; }
        [LibGridColum("Base Imponible", Width = 100)]
        public decimal BaseImponible { get; set; }
        [LibGridColum("Tarifa", Width = 100)]
        public decimal Tarifa { get; set; }
        public decimal ParaPagosMayoresDe { get; set; }
        [LibGridColum("Fecha De Inicio De Vigencia", eGridColumType.DatePicker, Width = 150)]
        public DateTime FechaDeInicioDeVigencia { get; set; }
        public decimal Sustraendo { get; set; }
        public bool AcumulaParaPJND { get; set; }
        Comun.Ccl.TablasLey.eTipodePersonaRetencion IFkTablaRetencionViewModel.TipoDePersona { get; set; }
    }

    public class FkArticuloInventarioViewModel : IFkArticuloInventarioViewModel {
        public int ConsecutivoCompania { get; set; }
        public string CodigoCompuesto { get; set; }
        [LibGridColum("Código Articulo", DbMemberPath = "Codigo", Width = 180, ColumnOrder = 1)]
        public string CodigoArticulo {
            get { return Codigo; }
            set {
                if(Codigo != value) {
                    Codigo = value;
                }
            }
        }
        [LibGridColum("Descripción", Width = 400, Trimming = System.Windows.TextTrimming.CharacterEllipsis, ColumnOrder = 2)]
        public string Descripcion { get; set; }
        [LibGridColum("Linea De Producto", ColumnOrder = 3)]
        public string LineaDeProducto { get; set; }
        public decimal PrecioSinIVA { get; set; }
        public decimal PrecioConIVA { get; set; }
        public int AlicuotaIva { get; set; }
        public string CodigoGrupo { get; set; }
        public eTipoArticuloInv TipoArticuloInv { get; set; }
        public string Codigo { get; set; }
        [LibGridColum("Existencia", eGridColumType.Numeric, DbMemberPath = "dbo.Gv_ArticuloInventario_B1.Existencia", Width = 100, IsForSearch = false, ColumnOrder = 4)]
        public decimal Existencia { get; set; }
        [LibGridColum("Último Costo", eGridColumType.Numeric, Width = 100, IsForSearch = false, ColumnOrder = 5)]
        public decimal CostoUnitario { get; set; }
        public decimal PorcentajeBaseImponible { get; set; }
        public eStatusArticulo StatusdelArticulo { get; set; }
        public eTipoDeArticulo TipoDeArticulo { get; set; }
        public string ArancelesCodigo { get; set; }
        public string ArancelesDescripcion { get; set; }
        public decimal AdValorem { get; set; }
        public decimal Seguro { get; set; }
        public string Categoria { get; set; }
        public decimal Peso { get; set; }
        public string UnidadDeVenta { get; set; }
        public bool UsaBalanza { get; set; }
        public decimal CantidadMaxima { get; set; }
        public decimal CantidadMinima { get; set; }
        public decimal MePrecioSinIva { get; set; }
        public decimal MePrecioConIva { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 1, 6)]
        public string CampoDefinible1 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 2, 7)]
        public string CampoDefinible2 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 3, 8)]
        public string CampoDefinible3 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 4, 9)]
        public string CampoDefinible4 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 5, 10)]
        public string CampoDefinible5 { get; set; }
        //[ArtInvCamposLoteFechaDeVencimientoGridColumn("Lote Inv.", "CodigoLote", 11)]
        //public string CodigoLote { get; set; }
        //[ArtInvCamposLoteFechaDeVencimientoGridColumn("Fecha Elab.", "FechaDeElaboracion", 12)]
        //public DateTime FechaDeElaboracion { get; set; }
        //[ArtInvCamposLoteFechaDeVencimientoGridColumn("Fecha Vcto.", "FechaDeVencimiento", 13)]
        //public DateTime FechaDeVencimiento { get; set; }

    }

    public class FkAlmacenViewModel : IFkAlmacenViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Codigo")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre Almacen")]
        public string NombreAlmacen { get; set; }
        [LibGridColum("Tipo De Almacen")]
        public eTipoDeAlmacen TipoDeAlmacen { get; set; }
        [LibGridColum("ConsecutivoCliente")]
        public int ConsecutivoCliente { get; set; }
        [LibGridColum("Código del Cliente")]
        public string CodigoCliente { get; set; }
        [LibGridColum("Nombre Cliente")]
        public string NombreCliente { get; set; }
        [LibGridColum("Código Centro de Costos")]
        public string CodigoCc { get; set; }
        [LibGridColum("Descripción Centro de Costos")]
        public string Descripcion { get; set; }
    }

    public class FkCxPViewModel {
        public int ConsecutivoCompania { get; set; }
        public int ConsecutivoCxP { get; set; }
        public int ConsecutivoProveedor { get; set; }
        [LibGridColum("Número")]
        public string Numero { get; set; }
        public string CodigoMoneda { get; set; }
        [LibGridColum("Código del Proveedor")]
        public string CodigoProveedor { get; set; }
        [LibGridColum("Nombre Proveedor")]
        public string NombreProveedor { get; set; }
        [LibGridColum("Monto", Type = eGridColumType.Numeric)]
        public decimal Monto { get; set; }

    }

    public class FkCondicionesDePagoViewModel : IFkCondicionesDePagoViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Descripción")]
        public string Descripcion { get; set; }
    }
    public class FkCompraViewModel : IFkCompraViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Serie")]
        public string Serie { get; set; }
        [LibGridColum("Numero")]
        public string Numero { get; set; }
        [LibGridColum("Fecha")]
        public DateTime Fecha { get; set; }
        [LibGridColum("Código del Proveedor", DbMemberPath = "Gv_Compra_B1.CodigoProveedor")]
        public string CodigoProveedor { get; set; }
        [LibGridColum("Nombre Proveedor", DbMemberPath = "Gv_Compra_B1.NombreProveedor")]
        public string NombreProveedor { get; set; }
        [LibGridColum("Código Almacen")]
        public string CodigoAlmacen { get; set; }
        [LibGridColum("Status Compra")]
        public eStatusCompra StatusCompra { get; set; }
    }

    public class FkMonedaViewModel : IFkMonedaViewModel {
        [LibGridColum("Codigo")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre")]
        public string Nombre { get; set; }
        [LibGridColum("Simbolo")]
        public string Simbolo { get; set; }
    }

    public class FkOrdenDeCompraViewModel : IFkOrdenDeCompraViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Serie")]
        public string Serie { get; set; }
        [LibGridColum("Numero")]
        public string Numero { get; set; }
        [LibGridColum("Fecha")]
        public DateTime Fecha { get; set; }
        [LibGridColum("Código del Proveedor")]
        public string CodigoProveedor { get; set; }
        [LibGridColum("Nombre Proveedor")]
        public string NombreProveedor { get; set; }
        [LibGridColum("Status Orden De Compra")]
        public eStatusCompra StatusOrdenDeCompra { get; set; }
        public string Moneda { get; set; }
        public string CodigoMoneda { get; set; }
        public decimal CambioABolivares { get; set; }
        public string Comentarios { get; set; }

    }

    public class FkPaisSunatViewModel {
        [LibGridColum("Código", Width = 300)]
        public string Codigo { get; set; }
        [LibGridColum("Nombre", Width = 300)]
        public string Nombre { get; set; }
    }

    public class FkConveniosSunatViewModel {
        [LibGridColum("Código", Width = 300)]
        public string Codigo { get; set; }
        [LibGridColum("País", Width = 300)]
        public string Pais { get; set; }
    }

    public class FkCategoriaViewModel : IFkCategoriaViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Categoría", DbMemberPath = "Descripcion")]
        public string Descripcion { get; set; }
    }

    public class FkLineaDeProductoViewModel : IFkLineaDeProductoViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Nombre de la Línea", DbMemberPath = "dbo.LineaDeProducto")]
        public string Nombre { get; set; }
    }

    public class FkCargaInicialViewModel : IFkCargaInicialViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Código del Articulo", Width = 100)]
        public string CodigoArticulo { get; set; }
        [LibGridColum("Existencia", eGridColumType.Numeric)]
        public decimal Existencia { get; set; }
        [LibGridColum("Costo", eGridColumType.Numeric)]
        public decimal Costo { get; set; }
    }
    public class FkCotizacionViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Número")]
        public string Numero { get; set; }
        [LibGridColum("Nombre Cliente")]
        public string NombreCliente { get; set; }
        [LibGridColum("Nombre Vendedor")]
        public string NombreVendedor { get; set; }
        [LibGridColum("Moneda")]
        public string NombreMoneda { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoVendedor { get; set; }
    }
}
