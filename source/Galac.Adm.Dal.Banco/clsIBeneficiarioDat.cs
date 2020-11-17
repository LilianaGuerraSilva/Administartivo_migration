using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Dal.Banco {
    public interface IBeneficiarioDat {
        int BeneficiarioGenerico(int valConsecutivoCompania);
        int BeneficiarioGenericoParaCrearEmpresa(int valConsecutivoCompania);

    }
}
