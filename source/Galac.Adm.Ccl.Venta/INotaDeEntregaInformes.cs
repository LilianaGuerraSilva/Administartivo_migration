using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Galac.Saw.Lib;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;

namespace Galac.Adm.Ccl.Venta {
    public interface INotaDeEntregaInformes {
        System.Data.DataTable BuildNotaDeEntregaEntreFechasPorCliente(int valConsecutivoCompania, DateTime valtFechaDesde, DateTime valFechaHasta, bool valIncluirNotasDeEntregasAnuladas, eCantidadAImprimir valCantidadAImprimir, string valCodigoCliente);
        System.Data.DataTable BuildNotaDeEntregaEntreFechasPorClienteDetallado(int valConsecutivoCompania, DateTime valtFechaDesde, DateTime valFechaHasta, eCantidadAImprimir valCantidadAImprimir, string valCodigoCliente);
    }
} //End of namespace Galac..Ccl.ComponenteNoEspecificado
