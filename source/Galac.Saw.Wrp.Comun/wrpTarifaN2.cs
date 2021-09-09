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
using Galac.Saw.Wrp.TablasLey;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.TablasLey {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.TablasLey {
#else
namespace Galac.Saw.Wrp.TablasLey {
#endif
    [ClassInterface(ClassInterfaceType.None)]

    public class wrpTarifaN2 : System.EnterpriseServices.ServicedComponent, IWrpTarifaN2 {
        #region Variables
        string _Title = "Tarifa N° 2";
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

        void IWrpTarifaN2.Execute(string vfwAction, string vfwIsReInstall) {
            try {
                ILibMenu insMenu = new Galac.Comun.Uil.TablasLey.clsTarifaN2Menu();
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

        string IWrpTarifaN2.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Comun.Uil.TablasLey.clsTarifaN2Menu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
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

        void IWrpTarifaN2.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpTarifaN2.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpTarifaN2.InitializeContext(string vfwInfo) {
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

        decimal IWrpTarifaN2.CalculaMontoRetencionTarifaN2(int valConsecutivoCompania, string valCodigoProveedor, string valCodigoRetencion, DateTime valFecha, DateTime valFechaApertura, DateTime valFechaCierre, decimal valMontoBaseImponible) {
            decimal vResult = 0;
            try {
                Galac.Comun.Ccl.TablasLey.ITarifaN2Pdn insTarifaN2Pdn = new Galac.Comun.Brl.TablasLey.clsTarifaN2Nav();
                vResult = insTarifaN2Pdn.CalculaMontoRetencionTarifaN2 ( valConsecutivoCompania,  valCodigoProveedor,  valCodigoRetencion,  valFecha,  valFechaApertura, valFechaCierre, valMontoBaseImponible);
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "Calcula Tarifa N2");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vResult;
        }
        decimal IWrpTarifaN2.BuscaPorcentajeRetencionTarifaN2(int valConsecutivoCompania, string valCodigoProveedor, string valCodigoRetencion, DateTime valFecha, DateTime valFechaApertura, DateTime valFechaCierre, decimal valMontoBaseImponible) {
            decimal vResult = 0;
            try {
                Galac.Comun.Ccl.TablasLey.ITarifaN2Pdn insTarifaN2Pdn = new Galac.Comun.Brl.TablasLey.clsTarifaN2Nav();
                vResult = insTarifaN2Pdn.BuscaPorcentajeRetencionTarifaN2(valConsecutivoCompania, valCodigoProveedor, valCodigoRetencion, valFecha, valFechaApertura, valFechaCierre, valMontoBaseImponible);
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "Busca Porcentaje Retencion TarifaN2");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vResult;
        }

        decimal IWrpTarifaN2.BuscaSustraendoRetencionTarifaN2(int valConsecutivoCompania, string valCodigoProveedor, string valCodigoRetencion, DateTime valFecha, DateTime valFechaApertura, DateTime valFechaCierre, decimal valMontoBaseImponible) {
            decimal vResult = 0;
            try {
                Galac.Comun.Ccl.TablasLey.ITarifaN2Pdn insTarifaN2Pdn = new Galac.Comun.Brl.TablasLey.clsTarifaN2Nav();
                //vResult = insTarifaN2Pdn.BuscaSustraendoRetencionTarifaN2(valConsecutivoCompania, valCodigoProveedor, valCodigoRetencion, valFecha, valFechaApertura, valFechaCierre, valMontoBaseImponible);
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "Busca Sustraendo Retencion TarifaN2");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vResult;
        }                 
    } //End of class wrpTarifaN2

} //End of namespace Galac.Iva.Wrp.TablasLey

