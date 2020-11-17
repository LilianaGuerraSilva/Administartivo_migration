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
using Galac.Saw.Ccl.Contabilizacion;
using Galac.Saw.Wrp.Contabilizacion;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Contabilizacion {
#else
namespace Galac.Saw.Wrp.Contabilizacion {
#endif

    [ClassInterface(ClassInterfaceType.None)]

    public class wrpReglasDeContabilizacion : System.EnterpriseServices.ServicedComponent, IWrpReglasDeContabilizacionVb {
#region Variables
        string _Title = "Reglas De Contabilizacion";

        ILibBusinessComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>> _Reglas;

#endregion //Variables
#region Propiedades

        private string Title {
            get { return _Title; }
        }
        private void RegistraCliente() {
            _Reglas = new Galac.Saw.Brl.Contabilizacion.clsReglasDeContabilizacionNav();
        }
#endregion //Propiedades
#region Constructores
#endregion //Constructores
#region Metodos Generados
#region Miembros deIWrpReglasDeContabilizacionVb

        void IWrpReglasDeContabilizacionVb.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
                ILibMenuMultiFile insMenu = new Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionMenu();
                insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1, insGV.GVDictionary);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        string IWrpReglasDeContabilizacionVb.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            return "";
        }

      void IWrpReglasDeContabilizacionVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

      void IWrpReglasDeContabilizacionVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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
        void IWrpReglasDeContabilizacionVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }
#endregion //Miembros de IWrpReglasDeContabilizacionVb

      private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {    
         LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
         LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibConvert.ToInt(valCurrentMfc));
         LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("ReglasDeContabilizacion", "ConsecutivoPeriodo"));
         return LibGlobalValues.Instance;
      }

        
#endregion //Metodos Generados



        void IWrpReglasDeContabilizacionVb.InsertarRegistroPorDefecto(string vfwCurrentCompany) {
            try {
                RegistraCliente();
                ((IReglasDeContabilizacionPdn)_Reglas).InsertarRegistroPorDefecto(LibGalac.Aos.Base.LibConvert.ToInt(vfwCurrentCompany));
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "Insertar Por Defecto");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }





        void IWrpReglasDeContabilizacionVb.CopiarReglasDeContabilizacion(string vfwCurrentCompany, string vfwNumero, string  vfwConsecutivoCompaniaDestino) {
            try {
                RegistraCliente();
                ((IReglasDeContabilizacionPdn)_Reglas).CopiarReglasDeContabilizacion(LibGalac.Aos.Base.LibConvert.ToInt(vfwCurrentCompany), vfwNumero, LibGalac.Aos.Base.LibConvert.ToInt(vfwConsecutivoCompaniaDestino));
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "Copiar Reglas De Contabilizacion");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
    } //End of class wrpReglasDeContabilizacion

} //End of namespace Galac.Saw.Wrp.Contabilizacion

