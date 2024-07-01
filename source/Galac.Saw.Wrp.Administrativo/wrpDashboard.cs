using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using System.Net.NetworkInformation;
using Galac.Comun.Uil.Dashboard;
using LibGalac.Aos.Vbwa;

namespace Galac.Saw.Wrp.Administrativo {
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpDashboard : System.EnterpriseServices.ServicedComponent, IWrpDashboard {
        private string Title = "Dashboard";
        void IWrpDashboard.Execute(string vfwAction, string vfwCurrentCompany, string vfwCurrentParameters) {
            try {
                LibGlobalValues insGV = CreateGlobalValues(vfwCurrentCompany, vfwCurrentParameters);
                if (NetworkInterface.GetIsNetworkAvailable()) {
                    ILibMenu insMenu = new clsDashboardMenu();
                    insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1);
                } else {
                    throw new LibGalac.Aos.Catching.GalacValidationException("Sin conexión a internet, para ejecutar esta opcón debe contar con una conexión a internet.");
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void IWrpDashboard.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);
                LibApp.AssignGalacCultureToTheCurrentThread();
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void IWrpDashboard.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void IWrpDashboard.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
            try {
                string vLogicUnitDir = LibGalac.Aos.Cnf.LibAppSettings.ULS;
                LibGalac.Aos.DefGen.LibDefGen.InitializeProgramInfo(vfwProgramInitials, vfwProgramVersion, vfwDbVersion, LibConvert.ToDate(vfwStrDateOfVersion), vfwStrHourOfVersion, "", vfwCountry, LibConvert.ToInt(vfwCMTO));
                LibGalac.Aos.DefGen.LibDefGen.InitializeWorkPaths("", vLogicUnitDir, LibApp.AppPath(), LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.ProgramInitials);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibConvert.ToInt(valCurrentMfc));            
            LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Periodo", "ConsecutivoPeriodo"));
            return LibGlobalValues.Instance;
        }

    }
}
