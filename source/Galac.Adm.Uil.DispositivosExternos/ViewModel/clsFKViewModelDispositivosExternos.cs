using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Saw.Ccl.DispositivosExternos;
namespace Galac.Adm.Uil.DispositivosExternos.ViewModel {

    public class FkBalanzaViewModel : IFkBalanzaViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
    }
}
