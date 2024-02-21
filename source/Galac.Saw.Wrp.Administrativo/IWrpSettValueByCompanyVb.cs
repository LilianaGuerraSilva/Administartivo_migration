using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.SttDef {
    public interface IWrpSettValueByCompanyVb {
        void Execute(string vfwAction, string vfwCurrentCompany, string vfwCurrentParameters);
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        string GetParametrosGenerales(string vfwCurrentCompany);
        string GetParametrosPorCompania(string vfwCurrentCompany);
        void InsertaValoresPorDefecto(string vfwConsecutivoCompania, string vfwCodigoMonedaLocal, string vfwNombreMonedaLocal, string vfwCodigoMonedaExtranjera, string vfwNombreMonedaExtranjera, string vfwCiudad);
        bool ActualizaValoresMonedaLocal(string vfwConsecutivoCompania, string vfwCodigoMonedaLocal, string vfwNombreMonedaLocal, string vfwSimboloMonedaLocal, decimal vfwMontoAPartirDelCualEnviarAvisoDeuda);
        bool ActualizaValorEnDondeRetenerIVA(string vfwConsecutivoCompania, string vfwDondeRetenerIVA);
        bool ResetFechaDeInicioContabilizacion(string vfwConsecutivoCompania, string vfwFechaDeInicioContabilizacion);
        bool SttUsaVendedor(int valConsecutivoCompania, string valCodigoVendedor);
        int CopiarParametrosAdministrativos(string vfwConsecutivoCompaniaOrigen, string vfwConsecutivoCompaniaDestino);
        void ConexionGVentas(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters);
    }
}
