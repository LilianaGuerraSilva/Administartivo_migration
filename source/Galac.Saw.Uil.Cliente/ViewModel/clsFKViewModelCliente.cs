using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Ccl.TablasGen;
using Galac.Saw.Ccl.Cliente;
using Galac.Cliente.Ccl.Cliente;
namespace Galac.Saw.Uil.Cliente.ViewModel {

    public class FkCiudadViewModel : IFkCiudadViewModel {
        [LibGridColum("Nombre Ciudad")]
        public string NombreCiudad { get; set; }
    }
    public class FkSectorDeNegocioViewModel : IFkSectorDeNegocioViewModel {
        [LibGridColum("Sector de Negocio")]
        public string Descripcion { get; set; }
    }
    public class FkClienteViewModel : IFkClienteViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre")]
        public string Nombre { get; set; }
        [LibGridColum("N° R.I.F.")]
        public string NumeroRIF { get; set; }
        [LibGridColum("Ciudad")]
        public string Ciudad { get; set; }
        [LibGridColum("Zona Postal")]
        public string ZonaPostal { get; set; }
        [LibGridColum("Teléfonos")]
        public string Telefono { get; set; }
        [LibGridColum("Zona De Cobranza")]
        public string ZonaDeCobranza { get; set; }
        [LibGridColum("Código del Vendedor")]
        public string CodigoVendedor { get; set; }
        [LibGridColum("Nombre Vendedor")]
        public string NombreVendedor { get; set; }
        [LibGridColum("Info Galac")]
        public eInfoGalacModoEnvio InfoGalac { get; set; }
        [LibGridColum("Sector De Negocio")]
        public string SectorDeNegocio { get; set; }
    }
    public class FkZonaCobranzaViewModel : IFkZonaCobranzaViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Nombre")]
        public string Nombre { get; set; }
    }
    public class FkVendedorViewModel : IFkVendedorViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre")]
        public string Nombre { get; set; }
        [LibGridColum("N° R.I.F.")]
        public string RIF { get; set; }
        [LibGridColum("Status")]
        public eStatusVendedor StatusVendedor { get; set; }
        [LibGridColum("Ciudad")]
        public string Ciudad { get; set; }
        [LibGridColum("Zona Postal")]
        public string ZonaPostal { get; set; }
        [LibGridColum("Telefono")]
        public string Telefono { get; set; }
    }
    public class FkCuentaViewModel : IFkCuentaViewModel {
        public int ConsecutivoPeriodo { get; set; }
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Descripción")]
        public string Descripcion { get; set; }
    }
}
