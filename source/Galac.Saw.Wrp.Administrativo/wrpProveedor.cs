using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Controls ;
using System.Runtime.InteropServices;
using System.Windows.Controls.Primitives ;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Vbwa;
using Galac.Saw.Wrp.GestionCompras;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.GestionCompras {
#else
namespace Galac.Saw.Wrp.GestionCompras {
#endif

    [ClassInterface(ClassInterfaceType.None)]
    public class wrpProveedor: System.EnterpriseServices.ServicedComponent, IWrpProveedorVb {
#region Variables
        string _Title = "Proveedor";
#endregion //Variables
#region Propiedades

        private string Title {
            get { return _Title; }
        }
#endregion //Propiedades
#region Constructores
#endregion //Constructores
#region Metodos Generados
#region Miembros de IWrpMfCs

        void IWrpProveedorVb.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                //CreateGlobalValues(vfwCurrentParameters);
 
               LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
                ILibMenu insMenu = new Galac.Adm.Uil.GestionCompras.clsProveedorMenu();
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

        string IWrpProveedorVb.Choose(string vfwParamInitializationList, string vfwParamFixedList, string valModulo = "") {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (valModulo == "ProveedorForPago") {
                   if (Galac.Adm.Uil.GestionCompras.clsProveedorMenu.ChooseFromInteropForPago(ref vXmlDocument, vSearchValues, vFixedValues)) {
                        vResult = vXmlDocument.InnerXml;
                    }
                } else {
				if (Galac.Adm.Uil.GestionCompras.clsProveedorMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
                        vResult = vXmlDocument.InnerXml;
                    }
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
#endregion //Miembros de IWrpMfCs

        private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
           LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
           LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibConvert.ToInt(valCurrentMfc));
           LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Proveedor", "ConsecutivoPeriodo"));
           return LibGlobalValues.Instance;
        }

#endregion //Metodos Generados


        void IWrpProveedorVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);
                LibApp.AssignGalacCultureToTheCurrentThread();
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpProveedorVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpProveedorVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        void RegisterDefaultTypesIfMissing() {
           LibGalac.Aos.Uil.LibMessagesHandler.RegisterMessages();
        }
    } //End of class wrpProveedor

} //End of namespace Galac.Saw.Wrp.GestionCompras

