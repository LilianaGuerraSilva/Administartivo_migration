using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Ccl.DispositivosExternos {
    public interface IGavetaPdn {
        bool AbrirGaveta(ePuerto valPuerto, string valComando);
    }
}
