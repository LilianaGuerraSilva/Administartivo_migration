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
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Wrp.Venta;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Venta {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Venta {
#else
namespace Galac.Saw.Wrp.Venta {
#endif
    [ClassInterface(ClassInterfaceType.None)]

    public class wrpCajaApertura: System.EnterpriseServices.ServicedComponent, IWrpCajaApertura {
        #region Variables
        string _Title = "Caja Apertura";
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

        void IWrpCajaApertura.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                CreateGlobalValues(vfwCurrentParameters);
                ILibMenu insMenu = new Galac.Adm.Uil.Venta.clsCajaAperturaMenu();
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

        string IWrpCajaApertura.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Adm.Uil.Venta.clsCajaAperturaMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
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

        void IWrpCajaApertura.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpCajaApertura.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO,bool vfwUsePASOnLine) {
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

        void IWrpCajaApertura.InitializeContext(string vfwInfo) {
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

        private void CreateGlobalValues(string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania"));
        }

        bool IWrpCajaApertura.UsuarioFueAsignado(int valConsecutivoCompania,int valConsecutivoCaja,string valNombreDelUsuario,bool valCajaCerrada,bool valResumenDiario) {
            ICajaAperturaPdn CajaAperturaNav = new Galac.Adm.Brl.Venta.clsCajaAperturaNav() as ICajaAperturaPdn;
            try {
                return CajaAperturaNav.UsuarioFueAsignado( valConsecutivoCompania, valConsecutivoCaja, valNombreDelUsuario, valCajaCerrada, valResumenDiario);
            } catch(GalacException vEx) {
                LibExceptionDisplay.Show(vEx);
                return false;
            }
        }

        bool IWrpCajaApertura.CajasCerradas(int valConsecutivoCompania,int valConsecutivoCaja,bool valCajaCerrada) {
            ICajaAperturaPdn CajaAperturaNav = new Galac.Adm.Brl.Venta.clsCajaAperturaNav() as ICajaAperturaPdn;
            try {
                return CajaAperturaNav.GetCajaCerrada(valConsecutivoCompania,valConsecutivoCaja,valCajaCerrada);
            } catch(GalacException vEx) {
                LibExceptionDisplay.Show(vEx);
                return false;
            }
        }

        #endregion //Metodos Generados
    } //End of class wrpCajaApertura

} //End of namespace Galac.Saw.Wrp.Venta

