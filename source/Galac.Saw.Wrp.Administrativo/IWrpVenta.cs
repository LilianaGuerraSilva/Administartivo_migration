using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if IsExeBsF 
namespace Galac.SawBsF.Wrp.Venta {
#else
namespace Galac.Saw.Wrp.Venta {
#endif
    public interface IWrpVenta {
        void Execute(string vfwAction, string vfwCurrentCompany, string vfwCurrentParameters);
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        void NotificaSiEsNecesario(string valActionStr, int valConsecutivoCompania, string valNumero, int valTipoDocumento);
    }
}