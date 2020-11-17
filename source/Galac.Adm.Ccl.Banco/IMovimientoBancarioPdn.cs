using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Banco {
    public  interface IMovimientoBancarioPdn : ILibPdn {
        bool Insert(List<MovimientoBancario> list);
        int BuscarSiguienteConsecutivoMovimientoBancario(int valConsecutivoCompania);
    }
}
