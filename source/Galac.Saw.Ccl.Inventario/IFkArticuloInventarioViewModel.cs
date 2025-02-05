using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Ccl.Inventario {

    public interface IFkArticuloInventarioViewModel {
        #region Propiedades
        int ConsecutivoCompania { get; set; }
        string Codigo { get; set; }
        string Descripcion { get; set; }
        decimal CostoUnitario { get; set; }
        decimal PrecioConIVA { get; set; }
        decimal PrecioSinIVA { get; set; }
        string LineaDeProducto { get; set; }
        string Categoria { get; set; }
        decimal Existencia { get; set; }
        int AlicuotaIva { get; set; }
        decimal PorcentajeBaseImponible { get; set; }
        eTipoDeArticulo TipoDeArticulo { get; set; }
        eTipoArticuloInv TipoArticuloInv { get; set; }
        bool UsaBalanza { get; set; }          
        decimal Peso { get; set; }
        string UnidadDeVenta { get; set; }
        decimal MePrecioSinIva { get; set; }
        decimal MePrecioConIva { get; set; }
        string CampoDefinible1 { get; set; }
        string CampoDefinible2 { get; set; }
        string CampoDefinible3 { get; set; }
        string CampoDefinible4 { get; set; }
        string CampoDefinible5 { get; set; }
        #endregion //Propiedades
    } //End of class IFkArticuloInventarioViewModel
} //End of namespace Galac.Saw.Ccl.Inventario