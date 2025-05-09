﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;

namespace Galac.Saw.Wrp.FormaDelCobro {
    public interface IWrpFormaDelCobroVb {
        void Execute(string vfwAction, string vfwCurrentCompany, string vfwCurrentParameters);
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        void InsertaValoresPorDefecto(string valCurrentMfc, string valCodigoMonedaLocal, string valCodigoMonedaExtranjera);
        XElement SearchByField(string valConsecutivoCompania, string valCodigo);
    }
}
    
    

