using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Inventario {

    public interface ILoteDeInventarioInformes {
        System.Data.DataTable BuildArticulosPorVencer(int valConsecutivoCompania, string valLineaDeProducto, string valCodigoArticulo, int valDiasPorVencer,eOrdenarFecha valOrdenarFecha);
		System.Data.DataTable BuildLoteDeInventarioVencidos(int valConsecutivoCompania, string valLineaDeProducto, string valCodigoArticulo,eOrdenarFecha valOrdenarFecha);
	    System.Data.DataTable BuildMovimientoDeLoteInventario(int valConsecutivoCompania, string valLoteDeInventario, string valCodigoArticulo, DateTime valFechaInicial, DateTime valFechaFinal);
	}	
} //End of namespace Galac.Saw.Ccl.Inventario

