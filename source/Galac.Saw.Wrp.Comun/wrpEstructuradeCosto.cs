using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls ;
using System.Windows.Controls.Primitives ;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Vbwa;
using Galac.Contab.Core;

#if IsExeBsF
namespace Galac.SawBsF.Wrp.LeyCosto  {
#else
namespace Galac.Saw.Wrp.LeyCosto {
#endif

    [ClassInterface(ClassInterfaceType.None)]

    public class wrpEstructuradeCosto : System.EnterpriseServices.ServicedComponent, IWrpEstructuradeCosto {
        #region Variables
        string _Title = "Copiar Estructura de Costo";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpEstructuradeCosto

        void IWrpEstructuradeCosto.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                CreateGlobalValues(vfwCurrentParameters);
                ILibMenuMultiFile insMenu = new Galac.Comun.Uil.LeyCosto.clsEstructuradeCostoMenu();
                insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1, LibGlobalValues.Instance.GVDictionary);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        string IWrpEstructuradeCosto.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            throw new NotImplementedException();
        }

        void IWrpEstructuradeCosto.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpEstructuradeCosto.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpEstructuradeCosto.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }
        #endregion //Miembros de IWrpEstructuradeCosto

        private void CreateGlobalValues(string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania"));
        }
        #endregion //Metodos Generados

        bool IWrpEstructuradeCosto.CopiarEstructuradeCosto(int vfwConsecutivoCompaniaActual, int vfwConsecutivoCompaniaACopiar, string vfwCurrentParameters) {
            bool vresult = false;
            
            CreateGlobalValues(vfwCurrentParameters);
            try {           
                Galac.Comun.Ccl.LeyCosto.IEstructuraDeCostoProcesos vEstructuradeCostoProcesos = new Galac.Comun.Brl.LeyCosto.clsEstructuraDeCostoProcesos();
                vresult = vEstructuradeCostoProcesos.CopiarEstructuraDeCosto(vfwConsecutivoCompaniaActual, vfwConsecutivoCompaniaACopiar);
            
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "CopiaEstructuradeCosto");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vresult;
        }

    } //End of class wrpEstructuradeCosto

} //End of namespace Galac.Wco.Wrp.LeyCosto

