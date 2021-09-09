using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls ;
using System.Windows.Controls.Primitives ;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Vbwa;
using Galac.Contab.Ccl.WinCont;
using Galac.Contab.Brl.WinCont;
using Galac.Contab.Core;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.WinCont {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.WinCont {
#else
namespace Galac.Saw.Wrp.WinCont {
#endif

    [ClassInterface(ClassInterfaceType.None)]

    public class wrpCuenta : System.EnterpriseServices.ServicedComponent, IWrpCuenta {
        #region Variables
        string _Title = "Cuenta";
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
        
        void IWrpCuenta.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                CreateGlobalValues(vfwCurrentParameters);
                ILibMenuMultiFile insMenu = new Galac.Contab.Uil.WinCont.clsCuentaMenu();
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


        string IWrpCuenta.Choose(string vfwParamInitializationList, string vfwParamFixedList, string vfwCurrentParameters) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            CreateGlobalValues(vfwCurrentParameters);
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Contab.Uil.WinCont.clsCuentaMenu.ChooseCuenta(null ,ref vXmlDocument, vSearchValues, vFixedValues)) {
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

        void IWrpCuenta.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpCuenta.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePasOnLine) {
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
        #endregion //Miembros de IWrpMfVb


        private void CreateGlobalValues(string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania"));
            LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Periodo", "ConsecutivoPeriodo"));            
        }        
        #endregion //Metodos Generados

        string IWrpCuenta.ChooseRegla(string vfwParamInitializationList, string vfwParamFixedList,string vfwCurrentParameters) {

            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            CreateGlobalValues(vfwCurrentParameters);            
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Contab.Uil.WinCont.clsCuentaMenu.ChooseCuentaReglas(null, ref vXmlDocument, vSearchValues, vFixedValues)) {
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

        void IWrpCuenta.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }
        string IWrpCuenta.BuscarCuenta(string vfwCurrentParameters) {
            string vResult = "";
             try {                 
                 System.Xml.XmlDocument vXmlDocument = null;
                 Galac.Contab.Ccl.WinCont.ICuentaPdn insCuentaPdn = new Galac.Contab.Brl.WinCont.clsCuentaNav();
                 if (insCuentaPdn.BuscarCuenta(vfwCurrentParameters, ref vXmlDocument)) {
                   vResult = vXmlDocument.InnerXml;
                 }
                 return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "Buscar Por Código");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
             return "";
        }


        void IWrpCuenta.AsignarEstructuraDeCosto(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                CreateGlobalValues(vfwCurrentParameters);
                ILibMenuMultiFile insMenu = new Galac.Contab.Uil.WinCont.clsAsignarEstructuraDeCostoMenu();
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

        string IWrpCuenta.DevuelveCuentasQueManejanAuxiliar(int valConsecutivoCompania, int valConsecutivoPeriodo, string valListaDeCuentas) {
            string vResult = string.Empty;
            ICuentaPdn insPdn = new clsCuentaNav();
            XElement vXmlResulset = insPdn.DevuelveCuentasQueManejanAuxiliar(valConsecutivoCompania, valConsecutivoPeriodo, valListaDeCuentas);
            if (vXmlResulset != null) {
                vResult = vXmlResulset.ToString();
            }
            return vResult;
        }
    } //End of class wrpCuenta

} //End of namespace Galac.Iva.Wrp.Cuenta

