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
using Galac.Comun.Ccl.TablasLey;
using Galac.Saw.Wrp.TablasLey;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.TablasLey {
#else
namespace Galac.Saw.Wrp.TablasLey {
#endif

    [ClassInterface(ClassInterfaceType.None)]

    public class wrpValorUT:System.EnterpriseServices.ServicedComponent, IWrpTablasLeyVb {
        #region Variables
        string _Title = "Unidad Tributaria";
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

        void IWrpTablasLeyVb.Execute(string vfwAction,string vfwCodigoMoneda, string vfwIsReInstall) {
            try {
                ILibMenu insMenu = new Galac.Comun.Uil.TablasLey.clsValorUTMenu();
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
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx,null,Title + " - " + vfwAction);
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        string IWrpTablasLeyVb.Choose(string vfwParamInitializationList,string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if(Galac.Comun.Uil.TablasLey.clsValorUTMenu.ChooseFromInterop(ref vXmlDocument,vSearchValues,vFixedValues)) {
                    vResult = vXmlDocument.InnerXml;
                }
                return vResult;
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx,null,Title + " - Escoger");
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }

        void IWrpTablasLeyVb.InitializeComponent(string vfwLogin,string vfwPassword,string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin,vfwPassword);
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar",vEx);
            }
        }

        void IWrpTablasLeyVb.InitializeDefProg(string vfwProgramInitials,string vfwProgramVersion,string vfwDbVersion,string vfwStrDateOfVersion,string vfwStrHourOfVersion,string vfwValueSpecialCharacteristic,string vfwCountry,string vfwCMTO,bool vfwUsePASOnLine) {
            try {
                string vLogicUnitDir = LibGalac.Aos.Cnf.LibAppSettings.ULS;
                LibGalac.Aos.DefGen.LibDefGen.InitializeProgramInfo(vfwProgramInitials,vfwProgramVersion,vfwDbVersion,LibConvert.ToDate(vfwStrDateOfVersion),vfwStrHourOfVersion,"",vfwCountry,LibConvert.ToInt(vfwCMTO));
                LibGalac.Aos.DefGen.LibDefGen.InitializeWorkPaths("",vLogicUnitDir,LibApp.AppPath(),LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.ProgramInitials);
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar",vEx);
            }
        }
        void IWrpTablasLeyVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización",vEx);
            }
        }
        #endregion //Miembros de IWrpVb

        #endregion //Metodos Generados     


        bool IWrpTablasLeyVb.AgregarNuevaUT(string FechaEnGacetaOficial,string FechaDeInicioDeVigencia,string MontoUnidadTributaria,string MontoUTImpuestosMunicipales,string CodigoMoneda,string NombreOperador) {
            try {
                IValorUTPdn insValorUTNav = new Comun.Brl.TablasLey.clsValorUTNav();
                decimal ValorUT = LibConvert.ToDec(LibXml.ValueToXElement(MontoUnidadTributaria,"Valor"));
                return insValorUTNav.AgregarNuevaUT(LibConvert.ToDate(FechaEnGacetaOficial),LibConvert.ToDate(FechaDeInicioDeVigencia),LibConvert.ToDec(ValorUT),LibConvert.ToDec(MontoUTImpuestosMunicipales),CodigoMoneda,NombreOperador);
            } catch(GalacException gEx) {
                return false;
            } catch(Exception vEx) {
                throw vEx;
            }
        }

        void RegisterDefaultTypesIfMissing() {
            LibGalac.Aos.Uil.LibMessagesHandler.RegisterMessages();
        }
    } //End of class wrpValorUT

} //End of namespace Galac.Comun.Wrp.TablasLey

