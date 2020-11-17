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

namespace Galac.Saw.Wrp.MenuBar {
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpMenuBarCaja:System.EnterpriseServices.ServicedComponent, IWrpMfMenuBarVb {
        #region Variables
        string _Title = "Caja Registradora";
        #endregion //Variables
        private string Title {
            get {
                return _Title;
            }
        }
        void IWrpMfMenuBarVb.CallMain(string vfwCurrentParameters) {
            try {
                bool EsUsuarioCajero= LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","EsUsuarioCajero"));
                LibGlobalValues.Instance.LoadCompleteAppMemInfo(vfwCurrentParameters);
                LibGlobalValues.Instance.LoadMFCInfoFromAppMemInfo("Compania","ConsecutivoCompania");
                LibMefBootstrapperForInterop vBootstrapper = new LibMefBootstrapperForInterop(true);
                LibInteropParameters vParams = new LibInteropParameters();
                vParams.CurrentUserName = ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login;
                vParams.CurrentCompanyName = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania","Nombre");
                vParams.ProgramImagePath = new System.Uri("/Images/Fondo Saw.jpg",System.UriKind.Relative);
                vParams.AdmittedComponents = ComponentsNavigationTab(EsUsuarioCajero);
                vBootstrapper.Components = ComponentsList();
                vBootstrapper.Run(vParams);
            } catch(AccessViolationException) {
                throw;
            } catch(ReflectionTypeLoadException vEx) {
                string[] vErrors = vEx.LoaderExceptions.Select(ex => ex.GetBaseException().Message).ToArray();
                string vMessage = string.Join(Environment.NewLine + Environment.NewLine,vErrors);
                LibExceptionDisplay.Show(new GalacException(vMessage,eExceptionManagementType.Uncontrolled,vEx));
            } catch(Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx,Title);
            }
        }

        void IWrpMfMenuBarVb.InitializeComponent(string vfwLogin,string vfwPassword,string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin,vfwPassword);
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar",vEx);
            }
        }

        void IWrpMfMenuBarVb.InitializeDefProg(string vfwProgramInitials,string vfwProgramVersion,string vfwDbVersion,string vfwStrDateOfVersion,string vfwStrHourOfVersion,string vfwValueSpecialCharacteristic,string vfwCountry,string vfwCMTO,bool vfwUsePASOnLine) {
            try {
                string vLogicUnitDir = LibGalac.Aos.Cnf.LibAppSettings.ULS;
                LibGalac.Aos.DefGen.LibDefGen.InitializeProgramInfo(vfwProgramInitials,vfwProgramVersion,vfwDbVersion,LibConvert.ToDate(vfwStrDateOfVersion),vfwStrHourOfVersion,"",vfwCountry,LibConvert.ToInt(vfwCMTO));
                LibGalac.Aos.DefGen.LibDefGen.InitializeWorkPaths("",vLogicUnitDir,LibApp.AppPath(),LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.ProgramInitials);
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar",vEx);
            }
        }

        void IWrpMfMenuBarVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar",vEx);
            }
        }

        private XElement ComponentsNavigationTab(bool valEsUsuarioCajero) {
            XElement vResult = null;
            if(!valEsUsuarioCajero) {
                vResult = new XElement("Components",
                             new XElement("UilComponents",
                                 new XElement("UilComponent",new XAttribute("Name","UIMefVentaCaja"),new XAttribute("Module","Caja")),
                                 new XElement("UilComponent",new XAttribute("Name","UIMefVentaCajaApertura"),new XAttribute("Module","Caja"))));
                //vResult.Descendants("UilComponents").Descendants("UilComponent").First().Add(new XElement("UilComponent",new XAttribute("Name","UIMefVentaCaja"),new XAttribute("Module","Caja")));
            } else {
                vResult = new XElement("Components",
                             new XElement("UilComponents",
                                 new XElement("UilComponent",new XAttribute("Name","UIMefVentaCajaApertura"),new XAttribute("Module","Caja"))));
            }
            return vResult;
        }

        private List<string> ComponentsList() {
            List<string> vResult = new List<string>();
            vResult.Add("Galac.Adm.Uil.Venta");
            return vResult;
        }
    }
}
