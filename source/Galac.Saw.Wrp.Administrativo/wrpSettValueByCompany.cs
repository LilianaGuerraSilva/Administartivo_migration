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
using LibGalac.Aos.Catching;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Vbwa;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Wrp.SttDef;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.SttDef {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.SttDef {
#else
namespace Galac.Saw.Wrp.SttDef {
#endif
    [ClassInterface(ClassInterfaceType.None)]
                 
    public class wrpSettValueByCompany : System.EnterpriseServices.ServicedComponent, IWrpSettValueByCompanyVb {
        #region Variables
        string _Title = "Sett Value By Company";
        ISettValueByCompanyPdn _Reglas;
        LibXmlMemInfo _initAppMemoryInfo; 
        LibXmlMFC _initMfc;
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }


        #endregion //Propiedades
        #region Constructores
        private void RegistraCliente() {
            _Reglas = new Galac.Saw.Brl.SttDef.clsSettValueByCompanyNav();
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpSettValueByCompanyVb

        void IWrpSettValueByCompanyVb.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
                ILibMenuMultiFile insMenu = new Galac.Saw.Uil.SttDef.clsSettValueByCompanyMenu();
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

        string IWrpSettValueByCompanyVb.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            return string.Empty;
        }

        void IWrpSettValueByCompanyVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpSettValueByCompanyVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpSettValueByCompanyVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }
        #endregion //Miembros de IWrpSettValueByCompanyVb


        private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibConvert.ToStr(valCurrentMfc));

            return LibGlobalValues.Instance;
        }
        #endregion //Metodos Generados


        string IWrpSettValueByCompanyVb.GetParametrosPorCompania(string vfwCurrentCompany) {
            string vResult = "";
            try {
                CreateGlobalValues(vfwCurrentCompany, "");
                RegistraCliente();
                vResult = _Reglas.ListadoParametros(LibGalac.Aos.Base.LibConvert.ToInt(vfwCurrentCompany));
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - Configuracion del sistema");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }


        string IWrpSettValueByCompanyVb.GetParametrosGenerales(string vfwCurrentCompany) {
            string vResult = "";
            try {
                CreateGlobalValues(vfwCurrentCompany, "");
                RegistraCliente();
                vResult = _Reglas.ListadoParametrosGenerales();
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - Configuracion del sistema");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }


        void IWrpSettValueByCompanyVb.InsertaValoresPorDefecto(string vfwConsecutivoCompania, string vfwCodigoMonedaLocal, string vfwNombreMonedaLocal, string vfwCodigoMonedaExtranjera, string vfwNombreMonedaExtranjera, string vfwCiudad) {
           try {
               CreateGlobalValues(vfwConsecutivoCompania, "");
               RegistraCliente();
               _Reglas.InsertaValoresPorDefecto(LibConvert.ToInt(vfwConsecutivoCompania), vfwCodigoMonedaLocal, vfwNombreMonedaLocal, vfwCodigoMonedaExtranjera, vfwNombreMonedaExtranjera, vfwCiudad);
           } catch (GalacException gEx) {
               LibExceptionDisplay.Show(gEx, null, Title + " - " + "Inserta Valores Por Defecto");
           } catch (Exception vEx) {
               if (vEx is AccessViolationException) {
                   throw;
               }
               LibExceptionDisplay.Show(vEx);
           }
        }

        
       bool IWrpSettValueByCompanyVb.ActualizaValoresMonedaLocal(string vfwConsecutivoCompania, string vfwCodigoMonedaLocal, string vfwNombreMonedaLocal, string vfwSimboloMonedaLocal, decimal vfwMontoAPartirDelCualEnviarAvisoDeuda) {
           bool vResult = false;
           try {
               CreateGlobalValues(vfwConsecutivoCompania, "");
               RegistraCliente();
               vResult = _Reglas.ActualizaValoresMonedaLocal(LibConvert.ToInt(vfwConsecutivoCompania), vfwCodigoMonedaLocal, vfwNombreMonedaLocal, vfwSimboloMonedaLocal, vfwMontoAPartirDelCualEnviarAvisoDeuda);
           } catch(GalacException gEx) {
               LibExceptionDisplay.Show(gEx, null, Title + " - " + "ActualizaValoresMonedaLocal");
           } catch(Exception vEx) {
               if(vEx is AccessViolationException) {
                   throw;
               }
               LibExceptionDisplay.Show(vEx);
           }
           return vResult;    
       }


       bool IWrpSettValueByCompanyVb.ActualizaValorEnDondeRetenerIVA(string vfwConsecutivoCompania, string vfwDondeRetenerIVA) {
           bool vResult = false;
           try {
               CreateGlobalValues(vfwConsecutivoCompania, "");
               RegistraCliente();
               vResult = _Reglas.ActualizaValorEnDondeRetenerIVA(LibConvert.ToInt(vfwConsecutivoCompania), vfwDondeRetenerIVA);
           } catch (GalacException gEx) {
               LibExceptionDisplay.Show(gEx, null, Title + " - " + "ActualizaValorEnDondeRetenerIVA");
           } catch (Exception vEx) {
               if (vEx is AccessViolationException) {
                   throw;
               }
               LibExceptionDisplay.Show(vEx);
           }
           return vResult;
       }


       bool IWrpSettValueByCompanyVb.ResetFechaDeInicioContabilizacion(string vfwConsecutivoCompania, string vfwFechaDeInicioContabilizacion) {
           bool vResult = false;
           try {
               CreateGlobalValues(vfwConsecutivoCompania, "");
               RegistraCliente();
               vResult = _Reglas.ResetFechaDeInicioContabilizacion(LibConvert.ToInt(vfwConsecutivoCompania),LibConvert.ToDate(vfwFechaDeInicioContabilizacion));
           } catch(GalacException gEx) {
               LibExceptionDisplay.Show(gEx, null, Title + " - " + "ResetFechaDeInicioContabilizacion");
           } catch(Exception vEx) {
               if(vEx is AccessViolationException) {
                   throw;
               }
               LibExceptionDisplay.Show(vEx);
           }
           return vResult;
       }

       bool IWrpSettValueByCompanyVb.SttUsaVendedor(int valConsecutivoCompania, string valCodigoVendedor) {
           bool vResult = false;
           try {               
               RegistraCliente();
               vResult = _Reglas.SttUsaVendedor(valConsecutivoCompania, valCodigoVendedor);
           } catch(GalacException gEx) {
               LibExceptionDisplay.Show(gEx, null, Title + " - " + "SttUsaVendedor");
           } catch(Exception vEx) {
               if(vEx is AccessViolationException) {
                   throw;
               }
               LibExceptionDisplay.Show(vEx);
           }
           return vResult;
       }

       void RegisterDefaultTypesIfMissing() {
           LibGalac.Aos.Uil.LibMessagesHandler.RegisterMessages();
       }

       int IWrpSettValueByCompanyVb.CopiarParametrosAdministrativos(string vfwConsecutivoCompaniaOrigen, string vfwConsecutivoCompaniaDestino) {
           int vResult = 1;
           try {
               RegistraCliente();
               vResult = _Reglas.CopiarParametrosAdministrativos(LibConvert.ToInt(vfwConsecutivoCompaniaOrigen), LibConvert.ToInt(vfwConsecutivoCompaniaDestino));
           } catch (GalacException gEx) {
               LibExceptionDisplay.Show(gEx, null, Title + " - " + "Copiar Parámetros Administrativos");
           } catch (Exception vEx) {
               if (vEx is AccessViolationException) {
                   throw;
               }
               LibExceptionDisplay.Show(vEx);
           }
           return vResult;
       }
    } //End of class wrpSettValueByCompany

}
