using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.ImprentaDigital {
    public interface IWrpImprentaDigitalVb {
        bool Execute(eTipoDocumentoFactura vfwTipoDocumento,  string vfwNumeroFactura, eAccionSR vfwAction, string vfwCurrentParameters, ref string vfwNumeroControl);
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);       
    }
}