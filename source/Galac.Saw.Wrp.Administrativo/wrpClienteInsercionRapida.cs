using System;
using System.Threading;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using System.Runtime.InteropServices;
using Galac.Adm.Uil.Venta;
using Galac.Saw.Wrp.Administrativo;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Wrp.Venta {
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpClienteInsercionRapida : System.EnterpriseServices.ServicedComponent, IWrpClienteInsercionRapida  {
        #region Variables
        ILibClienteInsercionRapidaMenu insMenu;
        #endregion //Variables

        #region Propiedades
        private string Title { get; set; } 
        #endregion //Propiedades

        #region Constructores
        public wrpClienteInsercionRapida() {
            Title = "Cliente Insercion Rapida";
        }
        #endregion //Constructores

        #region Miembros de IWrpClienteInsercionRapida
        #region Metodos Generados
        public void Execute(string vfwAction) {
        }

        string IWrpClienteInsercionRapida.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            //string vResult = "";
            //LibSearch insLibSearch = new LibSearch();
            //List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            //List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            //try {
            //    vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
            //    vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
            //    System.Xml.XmlDocument vXmlDocument = null;
            //    if (Galac.Adm.Uil.Venta.clsClienteInsercionRapidaMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
            //        vResult = vXmlDocument.InnerXml;
            //    }
            //    return vResult;
            //} catch (GalacException gEx) {
            //    LibExceptionDisplay.Show(gEx, null, Title +  " - Escoger");
            //} catch (Exception vEx) {
            //    if (vEx is AccessViolationException) {
            //        throw;
            //    }
            //    LibExceptionDisplay.Show(vEx);
            //}
            return "";
        }

        void IWrpClienteInsercionRapida.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                AddUserSecurity(vfwLogin, vfwPassword);
                LibSessionParameters.PlatformArchitecture = 1;
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpClienteInsercionRapida.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool valUsePassOnline) {
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

        void IWrpClienteInsercionRapida.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        private void AddUserSecurity(string valLogin, string valPassword) {
            GUserLogin vUserLogin = new GUserLogin();
            if (vUserLogin.ChooseUser(valLogin, valPassword)) {
                Thread.CurrentPrincipal = vUserLogin.CurrentPermissions;
            }
        }
        #endregion //Metodos Generados

        #region Métodos Programados
        void IWrpClienteInsercionRapida.Show(string valCurrentParameters, string valNombre,string valRif, eTipoDocumentoFactura valTipoDocumentoFactura) {
            try {                
                LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
                insMenu = new clsClienteInsercionRapidaMenu(valNombre,valRif,valTipoDocumentoFactura);
                insMenu.Ejecuta(eAccionSR.Insertar,1);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        bool IWrpClienteInsercionRapida.SeInsertoCliente() {
            return insMenu.DialogResult;
        }

        string IWrpClienteInsercionRapida.ObtenerCodigoCliente() {
            return insMenu.CodigoCliente;
        }

        string IWrpClienteInsercionRapida.ObtenerNombreCliente() {
            return insMenu.NombreCliente;
        }

        string IWrpClienteInsercionRapida.ObtenerRifCliente() {
            return insMenu.RifCliente;
        }

        #endregion
        #endregion //Miembros de IWrpClienteInsercionRapida
    } //End of class wrpClienteInsercionRapida
} //End of namespace Galac.Saw.Wrp.Venta

