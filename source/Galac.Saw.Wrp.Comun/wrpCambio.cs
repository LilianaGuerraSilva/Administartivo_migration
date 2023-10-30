using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Controls ;
using System.Windows.Controls.Primitives ;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Uil;
using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Ccl.TablasGen;
using LibGalac.Aos.Vbwa;
using Galac.Comun.Uil.TablasGen;
using System.Runtime.InteropServices;
using Galac.Saw.Wrp.TablasGen;
#if IsExeBsF
namespace Galac.SawBsf.Wrp.TablasGen {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.TablasGen {
#else
namespace Galac.Saw.Wrp.TablasGen {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpCambio : IWrpCambio { 
        #region Variables
        string _Title = "Cambio";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpCs

        void IWrpCambio.Execute(string vfwAction) {
            try {
                ILibMenu insMenu = new Galac.Comun.Uil.TablasGen.clsCambioMenu();
                insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        string IWrpCambio.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Comun.Uil.TablasGen.clsCambioMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
                    vResult = vXmlDocument.InnerXml;
                }
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title +  " - Escoger");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }

        void IWrpCambio.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpCambio.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
            try {
                string vLogicUnitDir = LibGalac.Aos.Cnf.LibAppSettings.ULS;
                LibGalac.Aos.DefGen.LibDefGen.InitializeProgramInfo(vfwProgramInitials, vfwProgramVersion, vfwDbVersion, LibConvert.ToDate(vfwStrDateOfVersion), vfwStrHourOfVersion, "", vfwCountry, LibConvert.ToInt(vfwCMTO));
                LibGalac.Aos.DefGen.LibDefGen.InitializeWorkPaths("", vLogicUnitDir, LibApp.AppPath(), LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.ProgramInitials);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpCambio.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        #endregion //Miembros de IWrpCs

        bool IWrpCambio.ExisteTasaDeCambioParaElDia(string valCodigoMoneda, DateTime valFechaDeVigencia, out string outTasa) {
            ICambioPdn vCambio = new clsCambioNav();
            decimal vTasaDecimal = 0;
            bool vExiste = vCambio.ExisteTasaDeCambioParaElDia(valCodigoMoneda, valFechaDeVigencia, out vTasaDecimal);
            outTasa = vTasaDecimal.ToString();
            return vExiste;
        }

        string IWrpCambio.InsertaTasaDeCambioParaElDia(string valCodigoMoneda, DateTime valFechaDeVigencia, bool valUsarLimiteMaximoParaIngresoDeTasaDeCambio, decimal valMaximoLimitePermitidoParaLaTasaDeCambio, bool valEsModoAvanzado, bool valInsertarAutomaticamenteValorDeDolarDesdePortalBCV) {
            clsCambioMenu vCambioMenu = new clsCambioMenu();
            string vTasaDecimal = "0";
            string vMonedaDolar = "USD";

            if (((IWrpCambio)this).ExisteTasaDeCambioParaElDia(valCodigoMoneda, valFechaDeVigencia, out vTasaDecimal)) {
                return vTasaDecimal;
            } else {
                if (valCodigoMoneda == vMonedaDolar && valInsertarAutomaticamenteValorDeDolarDesdePortalBCV) {
                    vTasaDecimal = InsertaYDevuelveValorDolarDesdePortalBCV();
                }
                if (vTasaDecimal == "0") {
                    return vCambioMenu.MostrarPantallaParaInsertarCambio(valCodigoMoneda, valFechaDeVigencia, valUsarLimiteMaximoParaIngresoDeTasaDeCambio, valMaximoLimitePermitidoParaLaTasaDeCambio, valEsModoAvanzado);
                } else {
                    return vTasaDecimal;
                }
            }
        }

        bool IWrpCambio.InsertarTasaDeCambioDelDiaDesdeSunat(string valMoneda, DateTime valFechaVigencia, decimal valCambioAMonedaLocal, decimal valCambioAMonedaLocalVenta) {
            ICambioPdn vCambio = new clsCambioNav();
            bool vResult = vCambio.InsertarTasaDeCambioParaElDia(valMoneda, valFechaVigencia, valCambioAMonedaLocal, valCambioAMonedaLocalVenta);
            return vResult;
        }

        bool IWrpCambio.InsertarCambioDeMonLocalAnteriorAMonLocalActVigente(string valCodigoMonedaLocalAnterior, decimal valCambioAMonedaLocal) {
            ICambioPdn vCambio = new clsCambioNav();
            bool vResult = vCambio.InsertarCambioDeMonLocalAnteriorAMonLocalActVigente(valCodigoMonedaLocalAnterior, valCambioAMonedaLocal);
            return vResult;
        }

        bool IWrpCambio.BuscarUltimoCambioDeMoneda(string valCodigoMoneda, out DateTime outFechaDeVigencia, out decimal outCambioAMonedaLOcal) {
            ICambioPdn vCambio = new clsCambioNav();
            DateTime vFechaDeVigencia = LibDate.EmptyDate();
            decimal vCambioAMonedaLocal = 1;
            bool vResult = vCambio.BuscarUltimoCambioDeMoneda(valCodigoMoneda, out vFechaDeVigencia, out vCambioAMonedaLocal);
            outFechaDeVigencia = vFechaDeVigencia;
            outCambioAMonedaLOcal = vCambioAMonedaLocal;
            return vResult;
        }

        string InsertaYDevuelveValorDolarDesdePortalBCV() {
            ICambioPdn vCambio = new clsCambioNav();
            bool vUsarUrlDePruebas = false;
            vUsarUrlDePruebas = UsarUrlDePruebasApiBcv();
            string vResult = vCambio.InsertaYDevuelveTasaDeCambioDolarBCVDesdeAPI(vUsarUrlDePruebas) ;
            decimal vResultDec = LibImportData.ToDec(vResult, 4);
            return LibConvert.ToStr(vResultDec, 4);
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

        #endregion //Metodos Generados


    } //End of class wrpCambio

} //End of namespace Galac..Wrp.TablasGen

