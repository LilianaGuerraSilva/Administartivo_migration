using System;

namespace Galac.Saw.Wrp.TablasGen {
    public interface IWrpCambio {
        string Choose(string vfwParamInitializationList, string vfwParamFixedList);
        void Execute(string vfwAction);
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeContext(string vfwInfo);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        string InsertaTasaDeCambioParaElDia(string valMoneda, DateTime valFechaVigencia,bool valUsarLimiteMaximoParaIngresoDeTasaDeCambio, decimal valMaximoLimitePermitidoParaLaTasaDeCambio, bool valEsModoAvanzado, bool valInsertarAutomaticamenteValorDeDolarDesdePortalBCV);
        bool InsertarTasaDeCambioDelDiaDesdeSunat(string valMoneda, DateTime valFechaVigencia, decimal valCambioAMonedaLocal, decimal valCambioAMonedaLocalVenta);
        bool InsertarCambioDeMonLocalAnteriorAMonLocalActVigente(string valCodigoMonedaLocalAnterior, decimal valCambioAMonedaLocal);
        bool BuscarUltimoCambioDeMoneda(string valCodigoMoneda, out DateTime outFechaDeVigencia, out decimal outCambioAMonedaLOcal);        
        bool ExisteTasaDeCambioParaElDia(string valMoneda, DateTime valFecha, out string outTasa);
    }
}