using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Wrp.Administrativo {
    public interface IWrpClienteInsercionRapida {
        void Execute(string vfwAction);
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool valUsePassOnline);
        void InitializeContext(string vfwInfo);
        void Show(string valCurrectParameters, string valNombre,string valRif, eTipoDocumentoFactura valTipoDocumentoFactura);
        bool SeInsertoCliente();
        string ObtenerCodigoCliente();
        string ObtenerNombreCliente();
        string ObtenerRifCliente();
    }
}
