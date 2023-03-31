using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.ImprentaDigital;
using Galac.Adm.Ccl.ImprentaDigital;
using Galac.Saw.Ccl.ImprentaDigital;
namespace Galac.Adm.Uil.ImprentaDigital.ViewModel {

    public class FkDocumentoDigitalViewModel : IFkDocumentoDigitalViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }
        public string NumeroControl { get; set; }
    }
    public class FkEnviarDocumentoViewModel : IFkEnviarDocumentoViewModel {
    }
}
