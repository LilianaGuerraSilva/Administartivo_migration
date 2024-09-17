using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Inventario {

    public interface ILoteDeInventarioInformes {
        System.Data.DataTable BuildArticulosPorVencer(int valConsecutivoCompania, string valLineaDeProducto, string valCodigoArticulo, int valDiasPorVencer,eOrdenarFecha valOrdenarFecha);
    }
} //End of namespace Galac.Saw.Ccl.Inventario

