
using LibGalac.Aos.Base;

namespace Galac.Saw.Wrp.Inventario {
    public interface IWrpLoteDeInventarioVb {
        void Execute(string vfwAction, string vfwCurrentCompany, string vfwCurrentParameters);
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        string ChooseForConteoFisico(string vfwParamInitializationList, string vfwParamFixedList);
    }
}
