using System;
using System.Runtime.InteropServices;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Vbwa;
using Galac.Saw.Wrp.VentaInformes;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.VentaInformes {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.VentaInformes {
#else
namespace Galac.Saw.Wrp.VentaInformes {
#endif

    [ClassInterface(ClassInterfaceType.None)]
    public class wrpVVentaInformes : System.EnterpriseServices.ServicedComponent, IWrpInformesVb {
        #region Variables
        string _Title = "Informes";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades       
        #region Metodos Generados
        #region Miembros de IWrpMfVb

        void IWrpInformesVb.Execute(string vfwAction, int vfwModule, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
                ILibMenu insMenu = new Galac.Adm.Uil.Venta.clsVentaInformesMenu();
                insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), vfwModule);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void IWrpInformesVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpInformesVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpInformesVb.InitializeContext(string vfwInfo) {
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
            return LibGlobalValues.Instance;
        }
        #endregion //Metodos Generados
    } //End of class wrpVentaInformes

} //End of namespace Galac.Saw.Wrp.Inventario

