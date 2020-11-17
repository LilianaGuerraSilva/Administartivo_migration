using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Galac.Contab.Core {
    public interface IWrpParametrosContables {
        void Execute(string vfwAction, string vfwCurrentCompany, string vfwCurrentParameters);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO);
        void InitializeContext(string vfwInfo);
        bool InsertarValoresPorDefecto(int vfwConsecutivoCompania);
    }
}