using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        bool ExistenCxPGeneradasDesdeCompra(int valConsecutivoCompania);
        string SiguienteNumeroDocumentoAntesDeImprentaDigital(eTipoDocumentoFactura valTipoDeDocumento, eTalonario valTalonarioEnum, eTipoDePrefijo valTipoPrefijo);
        void ConfiguracionImprentaDigitalPorTipoDeDocumentoFactura(eTipoDocumentoFactura valTipoDeDocumento, string valPrimerNumeroTalonario1);
        void MoverDocumentosDeTalonario(eTipoDocumentoFactura valTipoDeDocumento, eTalonario valTalonarioOrigen, eTalonario valTalonarioDestino);
        void ConfigurarImprentaDigital(eProveedorImprentaDigital valProveedorImprentaDigital, DateTime valFechaDeInicioDeUsoDeImprentaDigital);
        bool SonValidosLosSiguienteNumerosDeDocumentosParaImprentaDigital(string valPrimerNumeroFacturaT1, string valPrimerNumeroNotaDeCredito, string valPrimerNumeroNotaDeDebito, out StringBuilder outMessage);
        void GuardarDatosImprentaDigitalAppSettings(eProveedorImprentaDigital valProveedor, string valUsuario, string valClave, string valUrl, string valCampoUsuario, string valCampoClave);
        ObservableCollection<string> ListaDeUsuariosSupervisoresActivos();
        bool EjecutaConexionConGVentas(int valConsecutivoCompania, string valParametroSuscripcionGVentas, string valSerialConectorGVentas, string valNombreCompaniaGVentas, string valNombreUsuarioOperaciones, eAccionSR valAction);
        bool ExistenArticulosMercanciaNoSimpleNoLoteFDV(int valConsecutivoCompania);
        bool ExistenArticulosLoteFdV(int valConsecutivoCompania);
        bool ExisteCajaConMaquinaFiscal(int valConsecutivoCompania);
        string VersionHomologada();
    }
}


