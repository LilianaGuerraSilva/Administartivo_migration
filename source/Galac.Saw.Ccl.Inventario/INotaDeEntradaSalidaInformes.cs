using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Inventario {

    public interface INotaDeEntradaSalidaInformes {
        System.Data.DataTable BuildNotaDeEntradaSalidaDeInventario(int valConsecutivoCompania, string valNumeroDocumento);
    }
} //End of namespace Galac.Dbo.Ccl.Inventario

