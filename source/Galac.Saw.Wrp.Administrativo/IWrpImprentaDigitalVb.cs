
namespace Galac.Saw.Wrp.ImprentaDigital {
    public interface IWrpImprentaDigitalVb {
        bool EnviarDocumento(int vfwTipoDocumento, string vfwNumeroFactura, string vfwCurrentParameters, ref string vfwNumeroControl, ref string vfwMensaje);
        bool AnularDocumento(int vfwTipoDocumento, string vfwNumeroFactura, string vfwCurrentParameters, ref string vMensaje);
        bool SincronizarDocumento(int vfwTipoDocumento, string vfwNumeroFactura, string vfwCurrentParameters, ref string vfwNumeroControl, ref string vfwMensaje);       
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        bool ValidarEmail(string vfwEmmailAddress);
    }
}