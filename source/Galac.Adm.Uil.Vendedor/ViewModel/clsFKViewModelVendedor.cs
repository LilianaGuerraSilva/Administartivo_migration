using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.Vendedor;
using Galac.Adm.Ccl.Vendedor;
namespace Galac.Adm.Uil.Vendedor.ViewModel {

    public class FkVendedorViewModel : IFkVendedorViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string RIF { get; set; }
        public eStatusVendedor StatusVendedor { get; set; }
        public string Ciudad { get; set; }
        public string ZonaPostal { get; set; }
        public string Telefono { get; set; }
    }
}
