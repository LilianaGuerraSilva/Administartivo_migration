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
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Wrp.Administrativo;
using Galac.Saw.Uil.Inventario;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Inventario {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Inventario {
#else
namespace Galac.Saw.Wrp.Inventario {
#endif

    [ClassInterface(ClassInterfaceType.None)]
    public class wrpArticuloInventario : IWrpArticuloInventario {

        private string Title {
            get { return "Artículo Inventario"; }
        }

        #region Miembros de IWrpArticuloInventario

        void IWrpArticuloInventario.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
                ILibMenu insMenu = new Galac.Saw.Uil.Inventario.clsArticuloInventarioMenu();
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
		
        string IWrpArticuloInventario.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Saw.Uil.Inventario.clsArticuloInventarioMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
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

        void IWrpArticuloInventario.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpArticuloInventario.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        void IWrpArticuloInventario.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpArticuloInventario.ImportarPreciosDesdeArchivo(string valfwCurrentMfc, string vfwCurrentParameters) {
            CreateGlobalValues(valfwCurrentMfc, vfwCurrentParameters);
            clsArticuloInventarioMenu vArticuloInventarioMenu = new clsArticuloInventarioMenu();
            vArticuloInventarioMenu.MostrarPantallaDeImportarPreciosDesdeArchivo();
        }

        #endregion //Miembros de IWrpArticuloInventario

        #region Metodos

        private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibConvert.ToInt(valCurrentMfc));
            LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("RecordName", "ConsecutivoPeriodo"));
            return LibGlobalValues.Instance;
        }

        #endregion

    } //End of class wrpArticuloInventario

} //End of namespace Galac.Saw.Wrp.Inventario

