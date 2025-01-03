using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Ccl.Tablas {
    public interface IAuditoriaConfiguracionPdn {
        bool Auditar(string valMotivo, string valAccion, string valConfiguracionOriginal, string valConfiguracionNueva);
    }
}
