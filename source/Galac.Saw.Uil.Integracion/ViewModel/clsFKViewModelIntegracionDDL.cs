using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Saw.Ccl.Integracion;
namespace Galac.Saw.Uil.Integracion.ViewModel {

    public class FkIntegracionViewModel : IFkIntegracionViewModel {
        public eTipoIntegracion TipoIntegracion { get; set; }
        [LibGridColum("Version")]
        public string version { get; set; }
    }
}
