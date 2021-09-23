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
using System.Xml.Linq;
using Galac.Saw.Wrp.CajaChica;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.CajaChica {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.CajaChica {
#else
namespace Galac.Saw.Wrp.CajaChica {
#endif

    [ClassInterface(ClassInterfaceType.None)]

    public class wrpReposicion : System.EnterpriseServices.ServicedComponent, IWrpRendicionVb {
#region Variables
        string _Title = "Reposición de Caja Chica";
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

        void IWrpRendicionVb.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
                ILibMenuMultiFile insMenu = new Galac.Adm.Uil.CajaChica.clsRendicionMenu();
                insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1, insGV.GVDictionary);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        string IWrpRendicionVb.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Adm.Uil.CajaChica.clsRendicionMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
                    vResult = vXmlDocument.InnerXml;
                }
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - Escoger");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }

        void IWrpRendicionVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpRendicionVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpRendicionVb.InitializeContext(string vfwInfo) {
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

        private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibConvert.ToInt(valCurrentMfc));
            LibGlobalValues.Instance.LoadMFCInfoFromAppMemInfo("Periodo", "Consecutivo");            
            //LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Periodo", "ConsecutivoPeriodo"));
            return LibGlobalValues.Instance;
        }
#endregion //Metodos Generados

        string IWrpRendicionVb.ExecuteAndReturnValue(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            string vResult = "";
            LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
            ILibMenuMultiFile insMenu = new Galac.Adm.Uil.CajaChica.clsRendicionMenu();
            insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1, insGV.GVDictionary);
            vResult = ((Galac.Adm.Uil.CajaChica.clsRendicionMenu)insMenu).ViewModelStr;            
            return vResult;;
        }
    } //End of class wrpRendicion

} //End of namespace Galac.Saw.Wrp.CajaChica

