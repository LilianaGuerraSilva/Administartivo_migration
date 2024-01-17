using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Saw.Lib;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;

namespace Galac.Adm.Ccl.Venta {

    public interface INotaDeEntregaInformes {
        System.Data.DataTable BuildNotaDeEntregaEntreFechasPorCliente(int valConsecutivoCompania, DateTime valtFechaDesde, DateTime valFechaHasta, bool valIncluirNotasDeEntregasAnuladas, eCantidadAImprimir valCantidadAImprimir, eMonedaParaImpresion valMonedaDelReporte, string valCodigoCliente);
    }
} //End of namespace Galac..Ccl.ComponenteNoEspecificado

