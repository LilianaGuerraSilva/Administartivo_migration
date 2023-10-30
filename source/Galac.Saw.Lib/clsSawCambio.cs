using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Uil.TablasGen;
using LibGalac.Aos.Base;
using System;

namespace Galac.Saw.Lib {
    public class clsSawCambio {     
        public static decimal InsertaTasaDeCambioParaElDia(string valCodigoMoneda, DateTime valFechaDeVigencia, bool valUsarLimiteMaximoParaIngresoDeTasaDeCambio, decimal valMaximoLimitePermitidoParaLaTasaDeCambio, bool valEsModoAvanzado, bool valInsertarAutomaticamenteValorDeDolarDesdePortalBCV) {
            clsCambioMenu vCambioMenu = new clsCambioMenu();
            decimal vTasaDecimal = 0m;
            const string vMonedaDolar = "USD";
            if (ExisteTasaDeCambioParaElDia(valCodigoMoneda, valFechaDeVigencia, out vTasaDecimal)) {
                return vTasaDecimal;
            } else {
                if (valCodigoMoneda == vMonedaDolar && valInsertarAutomaticamenteValorDeDolarDesdePortalBCV) {
                    vTasaDecimal = InsertaYDevuelveValorDolarDesdePortalBCV();
                }
                if (vTasaDecimal == 0m) {
                    string vCambiosStr = vCambioMenu.MostrarPantallaParaInsertarCambio(valCodigoMoneda, valFechaDeVigencia, valUsarLimiteMaximoParaIngresoDeTasaDeCambio, valMaximoLimitePermitidoParaLaTasaDeCambio, valEsModoAvanzado);
                    return LibConvert.ToDec(vCambiosStr);
                } else {
                    return vTasaDecimal;
                }
            }
        }

        static bool ExisteTasaDeCambioParaElDia(string valCodigoMoneda, DateTime valFechaDeVigencia, out decimal outTasa) {
            ICambioPdn vCambio = new Galac.Comun.Brl.TablasGen.clsCambioNav();
            decimal vTasaDecimal = 0;
            bool vExiste = vCambio.ExisteTasaDeCambioParaElDia(valCodigoMoneda, valFechaDeVigencia, out vTasaDecimal);
            outTasa = vTasaDecimal;
            return vExiste;
        }

        static decimal InsertaYDevuelveValorDolarDesdePortalBCV() {
            ICambioPdn vCambio = new Galac.Comun.Brl.TablasGen.clsCambioNav();
            bool vUsarUrlDePruebas = false;
            vUsarUrlDePruebas = UsarUrlDePruebasApiBcv();
            decimal vResult = LibImportData.ToDec(vCambio.InsertaYDevuelveTasaDeCambioDolarBCVDesdeAPI(vUsarUrlDePruebas), 4);
            return vResult;
        }

        private static bool UsarUrlDePruebasApiBcv() {
            try {
                string vPersonalizar = LibGalac.Aos.Cnf.LibAppSettings.ReadAppSettingsKey("UsarUrlDePruebasApiBcv");
                bool vResult = LibString.IsNullOrEmpty(vPersonalizar) ? false : LibConvert.SNToBool(vPersonalizar);
                return vResult;
            } catch (Exception) {
                throw;
            }
        }
    }
}
