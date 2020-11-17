using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.Impuesto {
    public interface IWrpClasificadorActividadVb {
        void Execute(string vfwAction, string vfwCodigoMunicipio);
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        string CalculoRetencion(string valCodigoActividad, string valFechaAplicasion, string valMontoFactura);
        string PuedeActivarModulo(string valCondigoMunicipio);
    }
}
