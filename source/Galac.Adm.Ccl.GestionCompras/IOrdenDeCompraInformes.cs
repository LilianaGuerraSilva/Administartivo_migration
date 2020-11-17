using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.GestionCompras {
    public interface IOrdenDeCompraInformes {
        System.Data.DataTable BuildCompra(int valConsecutivoCompania, int valConsecutivoCompra);
        System.Data.DataTable BuildOrdenesDeCompras(int valConsecutivoCompania, DateTime FechaInicial, DateTime FechaFinal, eStatusDeOrdenDeCompra StatusDeOrdenDeCompra);
        System.Data.DataTable BuildImprimirCotizacionOrdenDeCompra(int valConsecutivoCompania, string NumeroCotizacion);
    }
}


