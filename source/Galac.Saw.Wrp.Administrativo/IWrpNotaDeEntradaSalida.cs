using LibGalac.Aos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.Administrativo {
    public interface IWrpNotaDeEntradaSalida: IWrpMfVb {

        string EjecutaProcesoDeReversar(int valConsecutivoCompania, string valNumeroDocumento);
    }
}
