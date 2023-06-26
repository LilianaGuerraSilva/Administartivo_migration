using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {
    public class FkArticuloInventarioViewModel : IFkArticuloInventarioViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código Articulo", DbMemberPath = "Gv_ArticuloInventario_B2.Codigo", Width = 180, ColumnOrder = 1)]
        public string Codigo { get; set; }
        [LibGridColum("Descripción", Width = 400, Trimming = System.Windows.TextTrimming.CharacterEllipsis, ColumnOrder = 2, DbMemberPath = "Gv_ArticuloInventario_B2.Descripcion")]
        public string Descripcion { get; set; }
        public decimal PrecioConIVA { get; set; }
        public decimal PrecioSinIVA { get; set; }
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
        public decimal MePrecioSinIva { get; set; }
        public decimal MePrecioConIva { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribue("", 1, ColumnOrder = 3)]
        public string CampoDefinible1 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribue("", 2, ColumnOrder = 4)]
        public string CampoDefinible2 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribue("", 3, ColumnOrder = 5)]
        public string CampoDefinible3 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribue("", 4, ColumnOrder = 6)]
        public string CampoDefinible4 { get; set; }
        [ArtInvCamposDefiniblesGridColumnAttribue("", 5, ColumnOrder = 7)]
        public string CampoDefinible5 { get; set; }
    }
    public class FkListaDeMaterialesViewModel : IFkListaDeMaterialesViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Código", DbMemberPath = "Adm.Gv_ListaDeMateriales_B1.Codigo")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre Lista Materiales", Width = 400)]
        public string Nombre { get; set; }
        [LibGridColum("Código Inventario a producir", Width = 400)]
        public string CodigoArticuloInventario { get; set; }
        [LibGridColum("Descripcion Articulo", Width = 400)]
        public string DescripcionArticuloInventario { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
    public class FkListaDeMaterialesConexionViewModel : IFkListaDeMaterialesViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [LibGridColum("Código Inventario a producir", DbMemberPath = "Adm.Gv_ListaDeMateriales_B1.CodigoArticuloInventario")]
        public string CodigoArticuloInventario { get; set; }
        [LibGridColum("Descripcion Articulo", Width = 400)]
        public string DescripcionArticuloInventario { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
    public class FkAlmacenViewModel : IFkAlmacenViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Código", DbMemberPath = "Gv_Almacen_B1.Codigo")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre", Width = 300)]
        public string NombreAlmacen { get; set; }
        [LibGridColum("Tipo")]
        public eTipoDeAlmacen TipoDeAlmacen { get; set; }
        public int ConsecutivoCliente { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string CodigoCc { get; set; }
        public string Descripcion { get; set; }
    }
    public class FkOrdenDeProduccionViewModel : IFkOrdenDeProduccionViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        [LibGridColum("Código", DbMemberPath = "Adm.Gv_OrdenDeProduccion_B1.Codigo")]
        public string Codigo { get; set; }
        [LibGridColum("Descripción", DbMemberPath = "Adm.Gv_OrdenDeProduccion_B1.Descripcion", Width = 250)]
        public string Descripcion { get; set; }
        [LibGridColum("Estado", eGridColumType.Enum, PrintingMemberPath = "StatusOpStr", DbMemberPath = "Adm.Gv_OrdenDeProduccion_B1.StatusOp", Width = 80)]
        public eTipoStatusOrdenProduccion StatusOp { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public DateTime FechaAnulacion { get; set; }
        public string ConsecutivoAlmacenProductoTerminado { get; set; }
        public string ConsecutivoAlmacenMateriales { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Observacion { get; set; }
        public string CodigoMonedaCostoProduccion { get; set; }
        public decimal CambioCostoProduccion { get; set; }
    }
}