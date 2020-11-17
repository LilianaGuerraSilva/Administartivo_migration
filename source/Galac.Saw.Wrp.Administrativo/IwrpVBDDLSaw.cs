using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Wrp.DDL {
    public interface IwrpVBDDLSaw {
        void InitializeComponent(string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);        
        void CreateTables(string vfwListOfTablesToCreate);
        void CreateViewsAndSPs(string vfwListOfModulesToCreateDbObjects);
    }
}
