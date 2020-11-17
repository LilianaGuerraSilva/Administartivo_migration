using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.TablasGen {
    public interface IWrpTipoDeCambioSunat {
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        // devolvemos cambio compra, cambio venta y fecha cambio 
        string ExecuteGetCambio();
        DateTime  FechaCambioSunat();
        decimal ObtenerCambioCompraSunat();
        decimal ObtenerCambioVentaSunat();
    }
}
