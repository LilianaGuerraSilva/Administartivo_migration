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
using LibGalac.Aos.Brl;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Brl.Banco;
using LibGalac.Aos.Vbwa;
using Galac.Saw.Wrp.Banco;

#if IsExeBsF
namespace Galac.SawBsF.Wrp.Banco {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Banco {
#else
namespace Galac.Saw.Wrp.Banco {
#endif

    [ClassInterface(ClassInterfaceType.None)]

    public class wrpCuentaBancaria: System.EnterpriseServices.ServicedComponent, IWrpCuentaBancariaVb {
        #region Variables
        string _Title = "Cuenta Bancaria";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpCuentaBancariaVb

        void IWrpCuentaBancariaVb.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
                ILibMenuMultiFile insMenu = new Galac.Adm.Uil.Banco.clsCuentaBancariaMenu();
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

        string IWrpCuentaBancariaVb.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Adm.Uil.Banco.clsCuentaBancariaMenu.ChooseCuentaBancariaFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
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

        void IWrpCuentaBancariaVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpCuentaBancariaVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpCuentaBancariaVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        void IWrpCuentaBancariaVb.GeneraCuentaBancariaGenericaSiCiaNoGeneraMovimientosBancarios(int vfwConsecutivoCompania, int vfwCodigoBanco, string valCodigoMonedaLocal, string valNombreMonedaLocal) {
            try {
                ICuentaBancariaPdn insCuentaBancariaPdn = new clsCuentaBancariaNav();
                insCuentaBancariaPdn.GeneraCuentaBancariaGenericaSiCiaNoGeneraMovimientosBancarios(vfwConsecutivoCompania, vfwCodigoBanco, valCodigoMonedaLocal, valNombreMonedaLocal);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "Genera Cuenta Bancaria Generica si Compañia no genera movimientos bancarios");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void IWrpCuentaBancariaVb.InsertaCuentaBancariaGenericaSiHaceFalta(int vfwConsecutivoCompania, int vfwCodigoBanco, string valCodigoMonedaLocal, string valNombreMonedaLocal) {
            try {
                ICuentaBancariaPdn insCuentaBancariaPdn = new clsCuentaBancariaNav();
                insCuentaBancariaPdn.InsertaCuentaBancariaGenericaSiHaceFalta(vfwConsecutivoCompania, vfwCodigoBanco, valCodigoMonedaLocal, valNombreMonedaLocal);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "Inserta Cuenta Bancaria Generica si hace falta");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        bool IWrpCuentaBancariaVb.ActualizaSaldoDisponibleEnCuenta(int vfwConsecutivoCompania, string vfwCodigoCuenta, string vfwMonto, string vfwIngresoEgreso, string vfwmAction, string vfwMontoOriginal, bool vfwSeModificoTipoConcepto) {
            try {
                ICuentaBancariaPdn insCuentaBancariaPdn = new clsCuentaBancariaNav();
                return insCuentaBancariaPdn.ActualizaSaldoDisponibleEnCuenta(vfwConsecutivoCompania, vfwCodigoCuenta, vfwMonto, vfwIngresoEgreso, new LibEAccionSR().ToInt(vfwmAction), vfwMontoOriginal, vfwSeModificoTipoConcepto);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "Actualiza Saldo Disponible En Cuenta");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return false;
        }
        #endregion //Miembros de IWrpCuentaBancariaVb


        private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibConvert.ToInt(valCurrentMfc));
            LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("RecordName", "ConsecutivoPeriodo"));
            return LibGlobalValues.Instance;
        }

        #endregion //Metodos Generados


    } //End of class wrpCuentaBancaria

} //End of namespace Galac.Saw.Wrp.Banco

