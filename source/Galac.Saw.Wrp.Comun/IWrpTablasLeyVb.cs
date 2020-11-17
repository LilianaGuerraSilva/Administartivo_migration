using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Wrp.TablasLey {
    public interface IWrpTablasLeyVb {
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        void Execute(string vfwAction, string vfwCodigoMoneda);
        bool AgregarNuevaUT(string FechaEnGacetaOficial, string FechaDeInicioDeVigencia, string MontoUnidadTributaria,string MontoUTImpuestosMunicipales, string CodigoMoneda, string NombreOperador);
    }

}
