using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.SttDef {
    public interface ISettValueByCompanyPdn: ILibPdn {
        string ListadoParametrosGenerales();
        string ListadoParametros(int valConsecutivoCompania);
        bool InsertaValoresPorDefecto(int valConsecutivoCompania, string valCodigoMonedaLocal, string valNombreMonedaLocal, string valCodigoMonedaExtranjera, string valNombreMonedaExtranjera, string valCiudad);
        List<Parametros> ParametrosList(int valConsecutivoCompania);
        List<Module> GetModuleList(int valConsecutivoCompania);
        bool SpecializedUpdate(List<Module> valModules);
        bool SePuedeModificarParametrosDeConciliacion();
        string GeneraPriemraNotaDeCredito(int valConsecutivoCompania, int valPrimerDocumento);
        string GeneraPriemraNotaDeDebito(int valConsecutivoCompania, int valPrimerDocumento);
        string GeneraPriemraBoleta(int valConsecutivoCompania, int valPrimerDocumento);
        bool ActualizaValoresMonedaLocal(int valConsecutivoCompania, string valCodigoMonedaLocal, string valNombreMonedaLocal, string valSimboloMonedaLocal, decimal valMontoAPartirDelCualEnviarAvisoDeuda);
        bool ActualizaValorEnDondeRetenerIVA(int valConsecutivoCompania, string valDondeRetenerIVA);
        bool ResetFechaDeInicioContabilizacion(int valConsecutivoCompania, DateTime valFechaDeInicioContabilizacion);
        bool SttUsaVendedor(int valConsecutivoCompania, string valCodigoVendedor);
        int DefaultLongitudCodigoCliente();
        int DefaultLongitudCodigoProveedor();
        int DefaultLongitudCodigoVendedor();
        string ConsultaCampoSettValueByCompany(string valCampo, int valConsecutivoCompania);
        bool ExisteMunicipio(int valConsecutivoMunicipio, string valNombreCiudad);
        bool PuedeActivarModulo(string valCodigoMunicipio);
        string BuscaNombreMoneda(string valCodigoMoneda);
        int CopiarParametrosAdministrativos(int valConsecutivoCompaniaOrigen, int valConsecutivoCompaniaDestino);
    }
}


