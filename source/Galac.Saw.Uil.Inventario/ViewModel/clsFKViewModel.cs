using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Saw.Ccl.Cliente;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Ccl.Tablas;
using Galac.Comun.Ccl.TablasGen;
using LibGalac.Aos.Base;

namespace Galac.Saw.Uil.Inventario.ViewModel {

    public class FkAlmacenViewModel : IFkAlmacenViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código", DbMemberPath = "Gv_Almacen_B1.Codigo")]
        public string Codigo { get; set; }
        public string CodigoCc { get; set; }
        public string CodigoCliente { get; set; }
        public int Consecutivo { get; set; }
        public int ConsecutivoCliente { get; set; }
        public string Descripcion { get; set; }
        [LibGridColum("Nombre", Width = 300)]
        public string NombreAlmacen { get; set; }
        public string NombreCliente { get; set; }
        [LibGridColum("Tipo")]
        public eTipoDeAlmacen TipoDeAlmacen { get; set; }
    }

    public class FkCentroDeCostosViewModel : IFkCentroDeCostosViewModel {
        public int ConsecutivoPeriodo { get; set; }
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Descripción")]
        public string Descripcion { get; set; }
    }

    public class FkClienteViewModel : IFkClienteViewModel {
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre", Width = 300)]
        public string Nombre { get; set; }
        [LibGridColum("N° R.I.F.")]
        public string NumeroRIF { get; set; }
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        public string StatusStr { get; set; }
        public string Direccion { get; set; }
        public DateTime ClienteDesdeFecha { get; set; }
        public string TipoDeContribuyente { get; set; }
    }

    public class FkCategoriaViewModel : IFkCategoriaViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Categoría")]
        public string Descripcion { get; set; }
    }

    public class FkColorViewModel : IFkColorViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código")]
        public string CodigoColor { get; set; }
        [LibGridColum("Descripción")]
        public string DescripcionColor { get; set; }

    }

    public class FkTallaViewModel : IFkTallaViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código")]
        public string CodigoTalla { get; set; }
        [LibGridColum("Descripción")]
        public string DescripcionTalla { get; set; }
    }

    public class FkLineaDeProductoViewModel : IFkLineaDeProductoViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Nombre de la Línea")]
        public string Nombre { get; set; }
    }

    public class FkArticuloInventarioViewModel : IFkArticuloInventarioViewModel {
        private decimal _PrecioSinIVA;
        private decimal _PrecioConIVA;
        private decimal _MePrecioSinIva;
        private decimal _MePrecioConIva;
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código del Artículo", DbMemberPath = "Gv_ArticuloInventario_B1.Codigo")]
        public string Codigo { get; set; }
        [LibGridColum("Descripción", DbMemberPath = "Gv_ArticuloInventario_B1.Descripcion")]
        public string Descripcion { get; set; }
        [LibGridColum("Precio Sin IVA", Alignment = eTextAlignment.Right)]
        public decimal PrecioSinIVA {
            get { return _PrecioSinIVA; }
            set {
                _PrecioSinIVA = value;
                UpdateImpuesto();
            }
        }
        public decimal PrecioSinIVA2 { get; set; }
        public decimal PrecioSinIVA3 { get; set; }
        public decimal PrecioSinIVA4 { get; set; }
        [LibGridColum("Precio Con IVA", Alignment = eTextAlignment.Right)]
        public decimal PrecioConIVA {
            get { return _PrecioConIVA; }
            set {
                _PrecioConIVA = value;
                UpdateImpuesto();
            }
        }
        public decimal PrecioConIVA2 { get; set; }
        public decimal PrecioConIVA3 { get; set; }
        public decimal PrecioConIVA4 { get; set; }
        public string LineaDeProducto { get; set; }
        public string Categoria { get; set; }
        public decimal Existencia { get; set; }
        public int AlicuotaIva { get; set; }
        public decimal PorcentajeBaseImponible { get; set; }
        public eTipoDeArticulo TipoDeArticulo { get; set; }
        public eTipoArticuloInv TipoArticuloInv { get; set; }
        public bool UsaBalanza { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Peso { get; set; }
        public string UnidadDeVenta { get; set; }
        public decimal Impuesto { get; set; }
        public decimal ImpuestoMe { get; set; }
        private void UpdateImpuesto() {
            Impuesto = PrecioConIVA - PrecioSinIVA;
        }

        [LibGridColum("Precio Sin IVA (ME)", Alignment = eTextAlignment.Right)]
        public decimal MePrecioSinIva {
            get { return _MePrecioSinIva; }
            set {
                _MePrecioSinIva = value;
                UpdateImpuestoMe();
            }
        }
        public decimal MePrecioSinIva2 { get; set; }
        public decimal MePrecioSinIva3 { get; set; }
        public decimal MePrecioSinIva4 { get; set; }
        [LibGridColum("Precio Con IVA (ME)", Alignment = eTextAlignment.Right)]
        public decimal MePrecioConIva {
            get { return _MePrecioConIva; }
            set {
                _MePrecioConIva = value;
                UpdateImpuestoMe();
            }
        }
        public decimal MePrecioConIva2 { get; set; }
        public decimal MePrecioConIva3 { get; set; }
        public decimal MePrecioConIva4 { get; set; }
        private void UpdateImpuestoMe() {
            ImpuestoMe = MePrecioConIva - MePrecioSinIva;
        }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 1)]
        public string CampoDefinible1 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 2)]
        public string CampoDefinible2 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 3)]
        public string CampoDefinible3 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 4)]
        public string CampoDefinible4 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 5)]
        public string CampoDefinible5 { get; set; }
        //[ArtInvCamposLoteFechaDeVencimientoGridColumn("Lote Inv.", "CodigoLote", 11)]
        //public string CodigoLote { get; set; }
        //[ArtInvCamposLoteFechaDeVencimientoGridColumn("Fecha Elab.", "FechaDeElaboracion", 12)]
        //public DateTime FechaDeElaboracion { get; set; }
        //[ArtInvCamposLoteFechaDeVencimientoGridColumn("Fecha Vcto.", "FechaDeVencimiento", 13)]
        //public DateTime FechaDeVencimiento { get; set; }
    }

    public class FkArticuloInventarioRptViewModel : IFkArticuloInventarioViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código del Artículo", DbMemberPath = "dbo.Gv_ArticuloInventario_B1.Codigo", Width = 150)]
        public string Codigo { get; set; }
        [LibGridColum("Descripción", DbMemberPath = "dbo.Gv_ArticuloInventario_B1.Descripcion", Width = 200)]
        public string Descripcion { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal PrecioConIVA { get; set; }
        public decimal PrecioSinIVA { get; set; }
        [LibGridColum("Linea De Producto")]
        public string LineaDeProducto { get; set; }
        public string Categoria { get; set; }
        public decimal Existencia { get; set; }
        public int AlicuotaIva { get; set; }
        public decimal PorcentajeBaseImponible { get; set; }
        public eTipoDeArticulo TipoDeArticulo { get; set; }
        public eTipoArticuloInv TipoArticuloInv { get; set; }
        public bool UsaBalanza { get; set; }
        public decimal Peso { get; set; }
        public string UnidadDeVenta { get; set; }
        public decimal MePrecioSinIva { get; set; }
        public decimal MePrecioConIva { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 1)]
        public string CampoDefinible1 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 2)]
        public string CampoDefinible2 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 3)]
        public string CampoDefinible3 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 4)]
        public string CampoDefinible4 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribute("", 5)]
        public string CampoDefinible5 { get; set; }
        //[ArtInvCamposLoteFechaDeVencimientoGridColumn("Lote Inv.", "", 11)]
        //public string CodigoLote { get; set; }
        //[ArtInvCamposLoteFechaDeVencimientoGridColumn("Fecha Elab.", "", 12)]
        //public DateTime FechaDeElaboracion { get; set; }
        //[ArtInvCamposLoteFechaDeVencimientoGridColumn("Fecha Vcto.", "", 13)]
        //public DateTime FechaDeVencimiento { get; set; }

    }

    public class FkMonedaViewModel : IFkMonedaViewModel {
        [LibGridColum("Codigo")]
        public string Codigo {
            get; set;
        }
        [LibGridColum("Nombre")]
        public string Nombre {
            get; set;
        }
        [LibGridColum("Simbolo")]
        public string Simbolo {
            get; set;
        }
    }

    public class FkLoteDeInventarioViewModel : IFkLoteDeInventarioViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Código de Lote")]
        public string CodigoLote { get; set; }
        public string CodigoArticulo { get; set; }
        [LibGridColum("Fecha Elab.", eGridColumType.DatePicker, BindingStringFormat = "dd/MM/yyyy")]
        public DateTime FechaDeElaboracion { get; set; }
        [LibGridColum("Fecha Vcto.", eGridColumType.DatePicker, BindingStringFormat = "dd/MM/yyyy")]
        public DateTime FechaDeVencimiento { get; set; }
        [LibGridColum("Existencia", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal Existencia { get; set; }
        [LibGridColum("Status", eGridColumType.Enum, PrintingMemberPath = "StatusLoteInvStr", DbMemberPath = "Saw.Gv_LoteDeInventario_B1.StatusLoteInv", Width = 75)]
        public eStatusLoteDeInventario StatusLoteInv { get; set; }
    }

    public class FkLoteDeInventarioConteoFisicoViewModel : IFkLoteDeInventarioViewModel
    {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Código de Artículo")]
        public string CodigoArticulo { get; set; }
        [LibGridColum("Código de Lote")]
        public string CodigoLote { get; set; }
        public DateTime FechaDeElaboracion { get; set; }
        public DateTime FechaDeVencimiento { get; set; }
        [LibGridColum("Existencia", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal Existencia { get; set; }
        [LibGridColum("Status", eGridColumType.Enum, PrintingMemberPath = "StatusLoteInvStr", DbMemberPath = "Saw.Gv_LoteDeInventario_B1.StatusLoteInv", Width = 75)]
        public eStatusLoteDeInventario StatusLoteInv { get; set; }
    }

    public class ArtInvCamposDefiniblesGridColumnAttribute : LibGridColumAttribute {
        public ArtInvCamposDefiniblesGridColumnAttribute(string initHeader, int initOrdinalEntre1y5, int initColumnOrder = 0) : base(initHeader) {
            IsForList = false;
            IsForSearch = false;
            if (initOrdinalEntre1y5 >= 1 && initOrdinalEntre1y5 <= 5) {
                string vEncabezado = string.Empty;
                string vDBPath = string.Empty;
                int vColumnOrder = 0;
                switch (initOrdinalEntre1y5) {
                    case 1:
                        vEncabezado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreCampoDefinibleInventario1");
                        vDBPath = "CampoDefinible1";
                        break;
                    case 2:
                        vEncabezado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreCampoDefinibleInventario2");
                        vDBPath = "CampoDefinible2";
                        break;
                    case 3:
                        vEncabezado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreCampoDefinibleInventario3");
                        vDBPath = "CampoDefinible3";
                        break;
                    case 4:
                        vEncabezado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreCampoDefinibleInventario4");
                        vDBPath = "CampoDefinible4";
                        break;
                    case 5:
                        vEncabezado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreCampoDefinibleInventario5");
                        vDBPath = "CampoDefinible5";
                        break;
                }
                if (initColumnOrder > 0) {
                    vColumnOrder = initColumnOrder;
                }
                if (!LibString.IsNullOrEmpty(vEncabezado, true) && !LibString.IsNullOrEmpty(vDBPath, true)) {
                    Header = vEncabezado;
                    DbMemberPath = vDBPath;
                    Type = eGridColumType.Generic;
                    Width = 100;
                    IsForList = true;
                    IsForSearch = true;
                    ColumnOrder = vColumnOrder;
                }
            }
        }
    }

    //public class ArtInvCamposLoteFechaDeVencimientoGridColumnAttribute : LibGridColumAttribute {
    //    public ArtInvCamposLoteFechaDeVencimientoGridColumnAttribute(string initHeader, string initDBPath, int initColumnOrder) : base(initHeader) {
    //        if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaLoteFechaDeVencimiento") && !LibString.IsNullOrEmpty(initHeader) && !LibString.IsNullOrEmpty(initDBPath)) {
    //            IsForList = true;
    //            IsForSearch = LibString.S1IsInS2("Lote", initHeader);
    //            Header = initHeader;
    //            DbMemberPath = initDBPath;
    //            Type = LibString.S1IsInS2("Fecha", initHeader) ? eGridColumType.DatePicker : eGridColumType.Generic;
    //            Width = 100;
    //            ColumnOrder = initColumnOrder;
    //            if (Type == eGridColumType.DatePicker) {
    //                BindingStringFormat = "dd/MM/yyyy";
    //            } else {
    //                BindingStringFormat = "";
    //            }
    //        }
    //    }
    //}
}
