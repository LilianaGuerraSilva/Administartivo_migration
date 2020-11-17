using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Vbwa;
using Galac.Contab.Core;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.WinCont {
#else
namespace Galac.Saw.Wrp.WinCont {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpParametrosContables : System.EnterpriseServices.ServicedComponent, IWrpParametrosContables  {
#region Variables
        string _Title = "Parámetros Contabilidad";
#endregion //Variables
#region Propiedades
        private string Title {
            get { return _Title; }
        }
#endregion //Propiedades
#region Constructores
#endregion //Constructores
#region Metodos Generados
#region Miembros de IWrpParametrosContables        
        void IWrpParametrosContables.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                CreateGlobalValues(vfwCurrentParameters);
                ILibMenuMultiFile insMenu = new Galac.Contab.Uil.WinCont.clsParametrosContablesMenu();
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
        void IWrpParametrosContables.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpParametrosContables.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpParametrosContables.InitializeContext(string vfwInfo) {
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

        private void CreateGlobalValues(string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania"));
            
        }
#endregion //Metodos Generados
        bool IWrpParametrosContables.InsertarValoresPorDefecto(int vfwConsecutivoCompania) {
            bool vresult = false;
            try {
                LibGlobalValues.Instance.GetMfcInfo().Add("Compania", vfwConsecutivoCompania);
                Galac.Contab.Ccl.WinCont.IParametrosConciliacionPdn insIParametrosConciliacionPdn = new Galac.Contab.Brl.WinCont.clsParametrosConciliacionNav();
                vresult = insIParametrosConciliacionPdn.InsertarValoresPorDefecto(vfwConsecutivoCompania);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "InsertarValoresPorDefecto");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vresult;
        }
    }
}
