using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Saw.Ccl.Integracion;

namespace Galac.Saw.Dal.Integracion {
    public interface IIntegracionSawDat {
        bool ConectarCompanias(string valCondigoCompania, string valCodigoConexion);
        bool DesConectarCompanias(string valCodigoConexion);
        bool ActualizaVersion(IList<IntegracionSaw> valRecord);
        bool VersionesCompatibles(string valVersion);
     
    }
}
