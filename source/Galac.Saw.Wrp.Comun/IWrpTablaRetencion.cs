using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Wrp.TablasLey {
    public interface IWrpTablaRetencion {
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        void Execute(string vfwAction, string vfwIsReInstall = "N");
        DateTime BuscaFechaDeInicioDeVigencia(DateTime vfwFecha);
        bool BuscaSiCodigoAcumulaParaPJND(String vfwTipoPersona, String vfwCodigoRetencion, DateTime vfwFecha);
        bool BuscarSiExisteCodigoRetencion(String vfwTipoPersona, String vfwCodigoRetencion, DateTime vfwFecha);
    }

}
