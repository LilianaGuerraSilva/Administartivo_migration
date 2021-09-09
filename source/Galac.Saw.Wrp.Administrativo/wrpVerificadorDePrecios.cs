using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Uil.Usal;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Inventario {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Inventario {
#else
namespace Galac.Saw.Wrp.Inventario {
#endif    
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpVerificadorDePrecios : System.EnterpriseServices.ServicedComponent, IWrpVerificadorDePrecios {
        private string _Title = "Consultor de Precios";
        public string Title { get; private set; }
        #region Metodos Generados
        #region Miembros de IWrpMfVb

        void IWrpVerificadorDePrecios.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                CreateGlobalValues(vfwCurrentParameters);
                ILibMenu insMenu = new Uil.Inventario.clsVerificadorDePreciosMenu();
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

        string IWrpVerificadorDePrecios.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                //if (Galac.Adm.Uil.Venta.clsFacturaElectronicaMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
                //    vResult = vXmlDocument.InnerXml;
                //}
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

        void IWrpVerificadorDePrecios.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                AddUserSecurity(vfwLogin, vfwPassword);
                RegisterDefaultTypesIfMissing();
                LibSessionParameters.PlatformArchitecture = 1;
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpVerificadorDePrecios.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpVerificadorDePrecios.InitializeContext(string vfwInfo) {
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

        private void AddUserSecurity(string valLogin, string valPassword) {
            GUserLogin vUserLogin = new GUserLogin();
            if (vUserLogin.ChooseUser(valLogin, valPassword)) {
                Thread.CurrentPrincipal = vUserLogin.CurrentPermissions;
            }
        }

        private void CreateGlobalValues(string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "ConsecutivoCompania"));
        }

        void RegisterDefaultTypesIfMissing() {
            LibGalac.Aos.Uil.LibMessagesHandler.RegisterMessages();
        }
        #endregion //Metodos Generados
    }
}
