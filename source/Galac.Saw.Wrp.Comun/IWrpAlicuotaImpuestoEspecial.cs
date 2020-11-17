using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.Impuesto {
    public interface IWrpAlicuotaImpuestoEspecial {
        void Execute(string vfwAction);
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        string ObtenerAlicuotaEspecial(DateTime valFecha);
        string ObtenerAlicuotaEspecial(DateTime valFecha, string valSqlWhereAND);
        string ObtenerAlicuotaEspecialConFiltro(DateTime valFecha, string valSqlWhereAND);
    }
}
