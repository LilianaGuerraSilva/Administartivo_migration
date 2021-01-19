using System;
using System.Collections.Generic;
using System.Linq;
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
using Galac.Comun.Ccl.TablasLey;
using Galac.Saw.Wrp.TablasLey;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.TablasLey {
#else
namespace Galac.Saw.Wrp.TablasLey {
#endif

    [ClassInterface(ClassInterfaceType.None)]

    public class wrpTablaRetencion : System.EnterpriseServices.ServicedComponent, IWrpTablaRetencion{
        #region Variables
        string _Title = "Tabla Retención";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpVb

        void IWrpTablaRetencion.Execute(string vfwAction, string vfwIsReInstall) {
            try {
                ILibMenu insMenu = new Galac.Comun.Uil.TablasLey.clsTablaRetencionMenu();
                bool vIsInstall = (eAccionSR)new LibEAccionSR().ToInt(vfwAction) == eAccionSR.Instalar;
                if (vIsInstall) {
                    if (LibConvert.SNToBool(vfwIsReInstall)) {
                        insMenu.Ejecuta(eAccionSR.ReInstalar, 1);
                    } else {
                        insMenu.Ejecuta(eAccionSR.Instalar, 1);
                    }
                } else {
                    insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1);
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        string IWrpTablaRetencion.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
               vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
               vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
               System.Xml.XmlDocument vXmlDocument = null;
               //LibFile.WriteLineInFile(@"C:\Prueba\Text.txt", vfwParamInitializationList);

                if (Galac.Comun.Uil.TablasLey.clsTablaRetencionMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
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

        void IWrpTablaRetencion.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath)
        {
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

        void IWrpTablaRetencion.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine)
        {
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

        void IWrpTablaRetencion.InitializeContext(string vfwInfo)
        {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }
        #endregion //Miembros de IWrpVb

        #endregion //Metodos Generados                    

        bool IWrpTablaRetencion.BuscaSiCodigoAcumulaParaPJND(string vfwTipoPersona, string vfwCodigoRetencion, DateTime vfwFecha) {
            bool vresult = false;
            try {
                Galac.Comun.Ccl.TablasLey.ITablaRetencionPdn insBuscaSiCodigoAcumulaParaPJNDPdn = new Galac.Comun.Brl.TablasLey.clsTablaRetencionNav();
                vresult = insBuscaSiCodigoAcumulaParaPJNDPdn.BuscaSiCodigoAcumulaParaPJND(vfwTipoPersona, vfwCodigoRetencion, vfwFecha);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "BuscaSiCodigoAcumulaParaPJND");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vresult;
        }
        DateTime IWrpTablaRetencion.BuscaFechaDeInicioDeVigencia(DateTime vfwFecha) {
            DateTime vResult = LibDate.Today();
            try {
                Galac.Comun.Ccl.TablasLey.ITablaRetencionPdn insBuscaSiCodigoAcumulaParaPJNDPdn = new Galac.Comun.Brl.TablasLey.clsTablaRetencionNav();
                vResult = insBuscaSiCodigoAcumulaParaPJNDPdn.BuscaFechaDeInicioDeVigencia(vfwFecha);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "BuscaFechaDeInicioDeVigencia");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vResult;
        }
        bool IWrpTablaRetencion.BuscarSiExisteCodigoRetencion(string vfwTipoPersona, string vfwCodigoRetencion, DateTime vfwFecha) {
            bool vresult = false;
            try {
                Galac.Comun.Ccl.TablasLey.ITablaRetencionPdn insBuscarSiExisteCodigoRetencionPdn = new Galac.Comun.Brl.TablasLey.clsTablaRetencionNav();
                vresult = insBuscarSiExisteCodigoRetencionPdn.BuscaSiCodigoRetencionExiste(vfwTipoPersona, vfwCodigoRetencion, vfwFecha);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "BuscarSiExisteCodigoRetencionPdn");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vresult;
        }

    } //End of class wrpTablaRetencion

} //End of namespace Galac.Iva.Wrp.TablasLey

