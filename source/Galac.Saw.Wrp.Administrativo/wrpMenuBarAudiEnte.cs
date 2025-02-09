using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Uil.Usal;
using System.Threading;
using LibGalac.Aos.UI.WpfMainInterop;
using System.Xml.Linq;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.Vbwa;
using System.Reflection;
using System.Xml;
using LibGalac.Aos.DefGen;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.MenuBar {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.MenuBar {
#else
namespace Galac.Saw.Wrp.MenuBar {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpMenuBarAudiEnte : System.EnterpriseServices.ServicedComponent, IWrpMfMenuBarVb {
        #region Variables
        string _Title = "Auditoría SENIAT";
        #endregion //Variables
        private string Title {
            get {
                return _Title;
            }
        }
        #region IWrpMfMenuBarVb
        void IWrpMfMenuBarVb.CallMain(string vfwCurrentParameters) {
            try {
                LibGlobalValues.Instance.LoadCompleteAppMemInfo(vfwCurrentParameters);
                LibGlobalValues.Instance.LoadMFCInfoFromAppMemInfo("Compania", "ConsecutivoCompania");
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaModuloDeContabilidad")) {
                    LibGlobalValues.Instance.LoadMFCInfoFromAppMemInfo("Periodo", "ConsecutivoPeriodo");
                }
                LibMefBootstrapperForInterop vBootstrapper = new LibMefBootstrapperForInterop(true);
                LibInteropParameters vParams = new LibInteropParameters();
                vParams.AdmittedComponents = ComponentsNavigationTab();
                vParams.CurrentUserName = ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login;
                vParams.CurrentCompanyName = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre");
                vParams.ProgramImagePath = new System.Uri("/Images/Fondo Saw.jpg", System.UriKind.Relative);
                vBootstrapper.Components = ComponentsList();
                vBootstrapper.Run(vParams);
            } catch (AccessViolationException) {
                throw;
            } catch (ReflectionTypeLoadException vEx) {
                string[] vErrors = vEx.LoaderExceptions.Select(ex => ex.GetBaseException().Message).ToArray();
                string vMessage = string.Join(Environment.NewLine + Environment.NewLine, vErrors);
                LibExceptionDisplay.Show(new GalacException(vMessage, eExceptionManagementType.Uncontrolled, vEx));
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, Title);
            }

        }

        void IWrpMfMenuBarVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpMfMenuBarVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpMfMenuBarVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
                clsNivelesDeSeguridad.DefinirPlantilla();
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        #endregion IWrpMfMenuBarVb

        private XElement ComponentsNavigationTab() {
            XElement vResult = new XElement("Components", new XElement("UilComponents", new XElement("UilComponent", new XAttribute("Name", "UIMefParameters"), new XAttribute("Group", "Parametros"))));
            return vResult;
        }

        private List<string> ComponentsList() {
            List<string> vResult = new List<string>();
            vResult.Add("Galac.Adm.Uil.Venta");
            return vResult;
        }
    }
}