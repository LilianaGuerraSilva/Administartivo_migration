using System;

namespace Galac.Adm.Ccl.Venta {

    public interface IContratoInformes {
        System.Data.DataTable BuildContratoPorNumero(int valConsecutivoCompania, string valNumeroContrato);
        System.Data.DataTable BuildContratoEntreFechas(int valConsecutivoCompania, bool valFiltrarPorStatus, bool valFiltrarPorFechaFinal, DateTime valFechaInicio, DateTime valFechaFinal, eStatusContrato eStatusContrato);
    }
} //End of namespace Galac.Dbo.Ccl.Venta

