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
using LibGalac.Aos.Vbwa;
using Galac.Axi.Ccl.Balances;
using Galac.Axi.Uil.Balances;
using LibGalac.Aos.Catching;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Balance {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Balance {
#else
namespace Galac.Saw.Wrp.Balance {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpBalance : System.EnterpriseServices.ServicedComponent, IWrpMfVbWithReports {
#region Variables
        string _Title = "Esquemas Balances";
        clsWrpBalance _Wrapper;
#endregion Variables

#region Propiedades
        public string Title {
            get { return _Title; }
        }
#endregion Propiedades

#region Constructores
        public wrpBalance() {
            _Wrapper = new clsWrpBalance();
        }
#endregion Constructores

#region Metodos
#region Miembros de IWrpBalances

        void IWrpMfVbWithReports.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                _Wrapper.Execute(vfwAction, vfwCurrentMfc, vfwCurrentParameters);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - " + vfwAction, vEx);
            }
        }

        string IWrpMfVbWithReports.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            try {
                return string.Empty;
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + "  - Escoger", vEx);
            }
        }

        void IWrpMfVbWithReports.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);
                InicializaValoresGlobales("");

            } catch (Exception vEx) {
                if (vEx is AccessViolationException || vEx is SystemException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpMfVbWithReports.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
            try {
                string vLogicUnitDir = System.Configuration.ConfigurationManager.AppSettings["UNIDADLOGICASERVIDOR"];
                LibGalac.Aos.DefGen.LibDefGen.InitializeProgramInfo(vfwProgramInitials, vfwProgramVersion, vfwDbVersion, LibConvert.ToDate(vfwStrDateOfVersion), vfwStrHourOfVersion, "", vfwCountry, LibConvert.ToInt(vfwCMTO));
                LibGalac.Aos.DefGen.LibDefGen.InitializeWorkPaths("", vLogicUnitDir, LibApp.AppPath(), LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.ProgramInitials);
                new LibGalac.DB.LibWrpMain().InitProgramInfo(vfwProgramInitials, vfwProgramVersion, vfwDbVersion, vfwStrDateOfVersion, vfwStrHourOfVersion, vfwValueSpecialCharacteristic, vfwCountry, vfwCMTO, vLogicUnitDir);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpMfVbWithReports.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización",vEx);
            }
        }
#endregion Miembros de IWrpBalances

#endregion Metodos

#region Programador Agregue aqui Codigo Adicionalf
        void InicializaValoresGlobales(string vfwSettings) {
            LibGalac.Base.LibApp.AssignGalacCultureToTheCurrentThread();
            new LibGalac.DB.LibWrpMain().InitVbDataAccess("");
            new LibGalac.DB.LibWrpMain().ReadVbCurrentPrinter(vfwSettings);
        }

        void InicializaValoresGlobalesDDL() {
            LibGalac.Base.LibApp.AssignGalacCultureToTheCurrentThread();
            new LibGalac.DB.LibWrpMain().InitVbDataAccess("");            
        }
#endregion //Programador Agregue aqui Codigo Adicional

        void IWrpMfVbWithReports.InitializeComponentForReports(string vfwLogin, string vfwPassword, string vfwPath, string vfwPrnAdvancedOptionsSessionSettings) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);
                InicializaValoresGlobales(vfwPrnAdvancedOptionsSessionSettings);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException || vEx is SystemException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }
        
        void IWrpMfVbWithReports.InitializeComponentForDDL(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);
                InicializaValoresGlobalesDDL();
            } catch(Exception vEx) {
                if(vEx is AccessViolationException || vEx is SystemException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }
    }
}
