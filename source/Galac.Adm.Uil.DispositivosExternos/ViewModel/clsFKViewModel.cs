using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.DispositivosExternos;

namespace Galac.Adm.Uil.DispositivosExternos.ViewModel {

    public class FkBalanzaViewModel : IFkBalanzaViewModel {
        public int ConsecutivoCompania { get; set; }
        [LibGridColum("Código")]
        public int Consecutivo { get; set; }
        [LibGridColum("Modelo")]
        public eModeloDeBalanza Modelo { get; set; }
        [LibGridColum("Nombre")]
        public string Nombre  { get; set; }
        [LibGridColum("Puerto")]
        public ePuerto Puerto  { get; set; }        
    }
}
