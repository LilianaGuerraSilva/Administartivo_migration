using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Vbwa;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Usuario {
#else
namespace Galac.Saw.Wrp.Usuario {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpUsuario : System.EnterpriseServices.ServicedComponent, IWrpVbUser {
#region Variables
        string _Title = "Usuario";
#endregion //Variables
#region Propiedades

        private string Title {
            get { return _Title; }
        }
#endregion //Propiedades
#region Constructores
#endregion //Constructores
#region Metodos Generados
#region Miembros de IWrpVbUser

        void IWrpVbUser.Execute(string vfwAction) {
            try {
                LibGalac.Aos.Base.ILibMenu insMenu = new LibGalac.Aos.Uil.Usal.LibGUserMenu();
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

        string IWrpVbUser.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (LibGalac.Aos.Uil.Usal.LibGUserMenu.ChooseGUser( ref vXmlDocument, vSearchValues, vFixedValues)) {
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

        void IWrpVbUser.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                GSSearchCriteria SearchCriteria = new GSSearchCriteria();
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);
                Galac.Saw.clsNivelesDeSeguridad.DefinirPlantilla();
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpVbUser.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpVbUser.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        bool IWrpVbUser.MatchP(string valP1, string valP2) {
            bool vResul = false;
            vResul = string.Equals(valP1, LibCryptography.SymDecryptDES(valP2), StringComparison.CurrentCultureIgnoreCase);
            return vResul;
        }        

#endregion
        
#region Miembros de IWrpVbUser
        
        bool IWrpVbUser.ChooseCurrentUser() {
            bool vResult = false;
            _SelectedUserName = "";
            _SelectedUserP = "";
            LibGalac.Aos.Uil.Usal.GUserLogin _Login = new LibGalac.Aos.Uil.Usal.GUserLogin();
            if (_Login.ChooseUser()) {
                _SelectedUserName = _Login.LoginUserName;
                _SelectedUserP = _Login.LoginUserPwd;
                vResult = true;
            }
            return vResult;
        }

#endregion

#region Miembros de IWrpVbUser
        string _SelectedUserName;

        string IWrpVbUser.SelectedUserName {
            get {
                return _SelectedUserName;
            }
            set {
                _SelectedUserName = value;
            }
        }

#endregion

#region Miembros de IWrpVbUser

        string _SelectedUserP;
        string IWrpVbUser.SelectedUserP {
            get {
                return _SelectedUserP;
            }
            set {
                _SelectedUserP = value;
            }
        }
#endregion
#endregion


        public string RequestCredential(string vfwDescription, bool vfwCheckIsSupervisor) {
            throw new NotImplementedException();
        }

        string IWrpVbUser.RequestCredential(string vfwDescription, bool vfwCheckIsSupervisor) {
            throw new NotImplementedException();
        }
    }  
}
