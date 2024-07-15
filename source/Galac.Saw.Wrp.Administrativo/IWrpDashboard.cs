using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.Administrativo {
    public interface IWrpDashboard {
        void Execute(string vfwAction, string vfwCurrentCompany, string vfwCurrentParameters);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
    }
}
