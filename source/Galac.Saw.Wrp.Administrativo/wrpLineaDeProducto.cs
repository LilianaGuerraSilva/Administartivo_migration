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
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;
using Galac.Saw.Uil.Tablas;
using Galac.Saw.Wrp.Tablas;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Tablas {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Tablas {
#else
namespace Galac.Saw.Wrp.Tablas {
#endif

    [ClassInterface(ClassInterfaceType.None)]

    public class wrpLineaDeProducto : System.EnterpriseServices.ServicedComponent, IWrpLineaDeProducto {
        #region Variables
        string _Title = "Linea De Producto";
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
        void IWrpLineaDeProducto.Execute(string vfwAction, string vfwCurrentCompany, string vfwCurrentParameters) {
            try {
                LibGlobalValues insGV = CreateGlobalValues(vfwCurrentCompany, vfwCurrentParameters);
                ILibMenu insMenu = new Galac.Saw.Uil.Tablas.clsLineaDeProductoMenu();
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

        string IWrpLineaDeProducto.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Saw.Uil.Tablas.clsLineaDeProductoMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
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

        void IWrpLineaDeProducto.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {                
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibGalac.Aos.Vbwa.LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);                
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpLineaDeProducto.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpLineaDeProducto.InitializeContext(string vfwInfo) {
            try {                
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);                
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }
        #endregion //Metodos Generados
        #endregion

        void IWrpLineaDeProducto.InsertDefaultRecordLineaDeProducto(int valConsecutivoCompania) {
            try {
                ILineaDeProductoPdn insLineaDeProductoPdn = new Galac.Saw.Brl.Tablas.clsLineaDeProductoNav();
                insLineaDeProductoPdn.InsertaPrimeraLineaDeProducto(valConsecutivoCompania);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "InsertaLaPrimerLineaDeProducto");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        
        string IWrpLineaDeProducto.GetNextConsecutivoLineaDeProducto(int valConsecutivoCompania) {
            try {
                ILineaDeProductoPdn insLineaDeProductoPdn = new Galac.Saw.Brl.Tablas.clsLineaDeProductoNav();
                return insLineaDeProductoPdn.GetNextConsecutivoLineaDeProducto(valConsecutivoCompania);
            } catch (GalacException gEx) {                
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "GetNextConsecutivoLineaDeProducto");
                return "";
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
                return "";
            }
        }

        private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibConvert.ToInt(valCurrentMfc));
            LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Periodo", "ConsecutivoPeriodo"));
            //LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "UsaModuloDeContabilidad"));
            return LibGlobalValues.Instance;
        }
    }

} 

