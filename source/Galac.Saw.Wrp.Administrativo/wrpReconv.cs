using System;
using System.Runtime.InteropServices;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Vbwa;
using Galac.Saw.Wrp.Reconv;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Reconv {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Reconv {
#else
namespace Galac.Saw.Wrp.Reconv {
#endif
    [ClassInterface(ClassInterfaceType.None)]

    public class wrpReconv : System.EnterpriseServices.ServicedComponent, IWrpReconvVb {
        #region Variables
        string _Title = "Reconversión";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpMfVb

        void IWrpReconvVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpReconvVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpReconvVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        #endregion //Miembros de IWrpMfVb

        string IWrpReconvVb.GetFechaReconversion() {
            string vFechaReconversion = "";
            try {
                vFechaReconversion = LibConvert.ToStr(Galac.Saw.Reconv.clsUtilReconv.GetFechaReconversion());
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "FechaReconversion");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vFechaReconversion;
        }

        string IWrpReconvVb.GetFactorReconversion() {
            string vFactorReconversion = "";
            try {
                vFactorReconversion = LibConvert.ToStr(Galac.Saw.Reconv.clsUtilReconv.GetFactorDeConversion());
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "FactorReconversion");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vFactorReconversion;
        }

        private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibConvert.ToInt(valCurrentMfc));
            LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("RecordName", "ConsecutivoPeriodo"));
            return LibGlobalValues.Instance;
        }

        void RegisterDefaultTypesIfMissing() {
            LibGalac.Aos.Uil.LibMessagesHandler.RegisterMessages();
        }
        string IWrpReconvVb.GetFechaDisposicionesTransitorias() {
            string vFechaDisposicionesTransitorias = "";
            try {
                vFechaDisposicionesTransitorias = LibConvert.ToStr(Galac.Saw.Reconv.clsUtilReconv.GetFechaDisposicionesTransitorias());
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "FechaDisposicionesTransitorias");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vFechaDisposicionesTransitorias;
        }

        #endregion //Metodos Generados
    } //End of class wrpReconv
} //End of namespace Galac.Saw.Wrp.Reconv

