using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Inventario {

    public interface IArticuloInventarioInformes {
        System.Data.DataTable BuildListadoDePrecios(int valConsecutivoCompania,string valFiltro);
        System.Data.DataTable BuildListdoDeArticulosBalanza(int valConsecutivoCompania,string valLineaDeProducto,bool valFiltrarPorLineaDeProducto);
        System.Data.DataTable BuildValoracionDeInventario(int valConsecutivoCompania,string valCodigoDesde,string valCodigoHasta,string valLineaDeProducto,decimal valCambioMoneda,bool valUsaPrecioConIva);
    }
} //End of namespace Galac.Saw.Ccl.Inventario

