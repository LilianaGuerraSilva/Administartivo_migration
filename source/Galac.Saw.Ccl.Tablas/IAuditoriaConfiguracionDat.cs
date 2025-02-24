using LibGalac.Aos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Ccl.Tablas {
    public interface IAuditoriaConfiguracionDat {
        LibResponse Auditar(AuditoriaConfiguracion refRecord);
    }
}
