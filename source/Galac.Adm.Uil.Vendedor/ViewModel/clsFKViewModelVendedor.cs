using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.Vendedor;
using Galac.Saw.Ccl.Vendedor;
namespace Galac.Adm.Uil.Vendedor.ViewModel {

    public class FkVendedorViewModel : IFkVendedorViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
    }
}
