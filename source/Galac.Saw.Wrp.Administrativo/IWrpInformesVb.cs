using Galac.Saw.Ccl.SttDef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.VentaInformes {
    public interface IWrpInformesVb {
        void Execute(string vfwAction, int vfwSystemModule, string vfwCurrentCompany, string vfwCurrentParameters);        
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        void BorradorDoc(string valNombreCompania, int valConsecutivoCompania, string valNumeroDocumento, int valTipoDocumento, int valStatusDocumento, int valTalonario, int valFormaDeOrdenarDetalleFactura, bool valImprimirFacturaConSubtotalesPorLineaDeProducto, string valCiudadCompania, string valNombreOperador);

    }
}
