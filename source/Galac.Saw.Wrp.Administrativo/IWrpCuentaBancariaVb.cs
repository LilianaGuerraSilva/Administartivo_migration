using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.Wrp.Banco {
    public interface IWrpCuentaBancariaVb {
        void GeneraCuentaBancariaGenericaSiCiaNoGeneraMovimientosBancarios(int vfwConsecutivoCompania, int vfwCodigoBanco, string valCodigoMonedaLocal, string valNombreMonedaLocal);
        void InsertaCuentaBancariaGenericaSiHaceFalta(int vfwConsecutivoCompania, int vfwCodigoBanco, string valCodigoMonedaLocal, string valNombreMonedaLocal);
        bool ActualizaSaldoDisponibleEnCuenta(int vfwConsecutivoCompania, string vfwCodigoCuenta, string vfwMonto, string vfwIngresoEgreso, string vfwmAction, string vfwMontoOriginal, bool vfwSeModificoTipoConcepto);

        void Execute(string vfwAction, string vfwCurrentCompany, string vfwCurrentParameters);
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
    }
}
