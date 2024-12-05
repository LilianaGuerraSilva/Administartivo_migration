using Galac.Adm.Ccl.CajaChica;
using System;

namespace Galac.Saw.Wrp.Venta {
    public interface IWrpCobroRapidoMultimoneda {
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void Execute(string vfwAction);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeContext(string vfwInfo);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        string CobrarFacturaEnMultimoneda(int valConsecutivoCompania, string NumeroDeFactura, string valFechaDelDocumento, decimal valTotalFactura, string valTipoDeDocumento, string valCodigoMonedaDeFactura, string valCodigoMonedaDeCobro, string valTipoDeContribuyenteDelIva, string vfwCurrentParameters, string valCedulaRif, ref string refIGTFParameters, ref string refListaVoucherMediosElectronicos);     
        string GenerarCobranzaYMovimientoBancarioDeCobroEnMultimoneda(int valConsecutivoCompania, string valNumeroFactura, string valTipoDeDocumento, string vfwCurrentParameters, string valNumeroCxC);
    }
}