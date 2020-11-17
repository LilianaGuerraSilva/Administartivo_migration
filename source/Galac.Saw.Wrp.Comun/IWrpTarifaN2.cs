using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Saw.Wrp.TablasLey {
    public interface IWrpTarifaN2 {
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);        
        void Execute(string vfwAction);
        decimal CalculaMontoRetencionTarifaN2(int valConsecutivoCompania, string valCodigoProveedor, string valCodigoRetencion, DateTime valFecha, DateTime valFechaApertura, DateTime valFechaCierre, decimal valMontoBaseImponible);
        decimal BuscaPorcentajeRetencionTarifaN2(int valConsecutivoCompania, string valCodigoProveedor, string valCodigoRetencion, DateTime valFecha, DateTime valFechaApertura, DateTime valFechaCierre, decimal valMontoBaseImponible);
        decimal BuscaSustraendoRetencionTarifaN2(int valConsecutivoCompania, string valCodigoProveedor, string valCodigoRetencion, DateTime valFecha, DateTime valFechaApertura, DateTime valFechaCierre, decimal valMontoBaseImponible);             
    }

}
