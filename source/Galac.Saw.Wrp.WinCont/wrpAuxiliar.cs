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
using Galac.Contab.Uil.WinCont;
using Galac.Contab.Brl.WinCont;
using Galac.Contab.Core;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.WinCont {
#else
namespace Galac.Saw.Wrp.WinCont {
#endif

    [ClassInterface(ClassInterfaceType.None)]

    public class wrpAuxiliar: System.EnterpriseServices.ServicedComponent, IWrpAuxiliar  {
        #region Variables
        string _Title = "Auxiliar";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpAuxiliar

        void IWrpAuxiliar.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            try {
                LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
                ILibMenuMultiFile insMenu = new Galac.Contab.Uil.WinCont.clsAuxiliarMenu();
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

        string IWrpAuxiliar.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Contab.Uil.WinCont.clsAuxiliarMenu.ChooseAuxiliarFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
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

        void IWrpAuxiliar.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpAuxiliar.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpAuxiliar.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }
        #endregion //Miembros de IWrpAuxiliar

        private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
            LibGlobalValues insGV = new LibGlobalValues();
            insGV.LoadCompleteAppMemInfo(valCurrentParameters);
            ((LibXmlMFC)insGV.GVDictionary[LibGlobalValues.NameMFCInfo]).Add("Periodo",LibConvert.ToInt(valCurrentMfc));
            return insGV;
        }
        #endregion //Metodos Generados

        bool IWrpAuxiliar.GenerarAuxiliarDesdeRecordExterno(int vfwConsecutivoPeriodo, string vfwCodigo, string vfwTipoAuxiliar, string vfwNombre) {
            bool vresult = false;            
            try {

                Galac.Contab.Ccl.WinCont.IAuxiliarPdn insGenerarAuxiliarDesdeRecordExternoPdn = new Galac.Contab.Brl.WinCont.clsAuxiliarNav();
                vresult = insGenerarAuxiliarDesdeRecordExternoPdn.GenerarAuxiliarDesdeRecordExterno(vfwConsecutivoPeriodo, vfwCodigo, vfwTipoAuxiliar, vfwNombre);                                               
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "GenerarAuxiliarDesdeRecordExterno");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vresult; 
        }
        bool IWrpAuxiliar.BuscarPorCodigoYTipo(int vfwConsecutivoPeriodo, string vfwCodigo, string vfwTipoAuxiliar) {
            bool vresult = false;
            try {
                Galac.Contab.Ccl.WinCont.IAuxiliarPdn insBuscarPorCodigoYTipoPdn = new Galac.Contab.Brl.WinCont.clsAuxiliarNav();
                vresult = insBuscarPorCodigoYTipoPdn.BuscarPorCodigoYTipo(vfwConsecutivoPeriodo, vfwCodigo, vfwTipoAuxiliar);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "BuscarPorCodigoYTipo");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vresult;
        }
        bool IWrpAuxiliar.ExisteAuxiliarEnAsiento(int vfwConsecutivoPeriodo, string vfwCodigo, string vfwTipoAuxiliar) {
            bool vresult = false;
            try {
                Galac.Contab.Ccl.WinCont.IAuxiliarPdn insExisteAuxiliarEnAsientoPdn = new Galac.Contab.Brl.WinCont.clsAuxiliarNav();
                vresult = insExisteAuxiliarEnAsientoPdn.ExisteAuxiliarEnAsiento(vfwConsecutivoPeriodo, vfwCodigo, vfwTipoAuxiliar);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "ExisteAuxiliarEnAsiento");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vresult;
        }
        bool IWrpAuxiliar.ExecuteAccionSobreRecordFromExternalModule(string vfwAction, string vfwCurrentParameters) {
            bool vresult = false;
            try {
                Galac.Contab.Ccl.WinCont.IAuxiliarPdn insExecuteAccionSobreRecordFromExternalModulePdn = new Galac.Contab.Brl.WinCont.clsAuxiliarNav();
                vresult = insExecuteAccionSobreRecordFromExternalModulePdn.ExecuteAccionSobreRecordFromExternalModule(vfwAction, vfwCurrentParameters);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "ExecuteAccionSobreRecordFromExternalModule");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vresult;
        }
        //void IWrpAuxiliar.ExecuteSustituirElCodigo(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
        //    try {
        //        LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
        //        ILibMenuMultiFile insMenu = new Galac.Contab.Uil.WinCont.clsCambiarCodigoAuxiliarMenu();
        //        insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1, insGV.GVDictionary);
        //    } catch (GalacException gEx) {
        //        LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
        //    } catch (Exception vEx) {
        //        if (vEx is AccessViolationException) {
        //            throw;
        //        }
        //        LibExceptionDisplay.Show(vEx);
        //    }
        //}
        //void IWrpAuxiliar.ExecuteUnificarAuxiliares(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
        //    try {
        //        LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
        //        ILibMenuMultiFile insMenu = new Galac.Contab.Uil.WinCont.clsUnificarAuxiliarMenu();
        //        insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1, insGV.GVDictionary);
        //    } catch (GalacException gEx) {
        //        LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
        //    } catch (Exception vEx) {
        //        if (vEx is AccessViolationException) {
        //            throw;
        //        }
        //        LibExceptionDisplay.Show(vEx);
        //    }
        //}
        bool IWrpAuxiliar.GenerarAuxiliares(int vfwConsecutivoPeriodo,int vfwConsecutivoCompania, string vfwTipoAuxiliar) {
            bool vresult = false;
            try {
                Galac.Contab.Ccl.WinCont.IAuxiliarPdn insGenerarAuxiliaresPdn = new Galac.Contab.Brl.WinCont.clsAuxiliarNav();
                vresult = insGenerarAuxiliaresPdn.GenerarAuxiliares(vfwConsecutivoPeriodo, vfwConsecutivoCompania, vfwTipoAuxiliar);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "GenerarAuxiliares");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vresult;
        }
        bool IWrpAuxiliar.ExisteAlMenosUnAuxiliar(int vfwConsecutivoCompania) {
            bool vresult = false;
            try {
                Galac.Contab.Ccl.WinCont.IAuxiliarPdn insExisteAlMenosUnAuxiliarPdn = new Galac.Contab.Brl.WinCont.clsAuxiliarNav();
                vresult = insExisteAlMenosUnAuxiliarPdn.ExisteAlMenosUnAuxiliar(vfwConsecutivoCompania);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "ExisteAlMenosUnAuxiliar");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vresult;
        }
        bool IWrpAuxiliar.TrasladarAuxiliaresAOtroPeriodo(int vfwConsecutivoPeriodoActual, int vfwConsecutivoPeriodoNuevo) {            
            bool vresult = false;
            try {
                Galac.Contab.Ccl.WinCont.IAuxiliarPdn insTrasladarAuxiliaresAOtroPeriodoPdn = new Galac.Contab.Brl.WinCont.clsAuxiliarNav();
                vresult = insTrasladarAuxiliaresAOtroPeriodoPdn.TrasladarAuxiliaresAOtroPeriodo(vfwConsecutivoPeriodoActual, vfwConsecutivoPeriodoNuevo);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "TrasladarAuxiliaresAOtroPeriodo");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vresult;
        }
    } //End of class wrpAuxiliar

} //End of namespace Galac.Contab.Wrp.WinCont

