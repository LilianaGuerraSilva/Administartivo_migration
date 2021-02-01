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

#if IsExeBsF
namespace Galac.SawBsF.Wrp.Tablas {
#else
namespace Galac.Saw.Wrp.Tablas {
#endif
    [ClassInterface(ClassInterfaceType.None)]

    public class wrpImpuestoBancario:System.EnterpriseServices.ServicedComponent, IWrpImpuestoBancarioVb {
        #region Variables
        string _Title = "Impuesto Bancario";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpImpuestoBancarioVb

        void IWrpImpuestoBancarioVb.Execute(string vfwAction, string vfwIsReInstall) {
            try {
                ILibMenu insMenu = new Galac.Saw.Uil.Tablas.clsImpuestoBancarioMenu();
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

        string IWrpImpuestoBancarioVb.Choose(string vfwParamInitializationList,string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if(Galac.Saw.Uil.Tablas.clsImpuestoBancarioMenu.ChooseFromInterop(ref vXmlDocument,vSearchValues,vFixedValues)) {
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

        void IWrpImpuestoBancarioVb.InitializeComponent(string vfwLogin,string vfwPassword,string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibGalac.Aos.Vbwa.LibWrpHelper.ConfigureRuntimeContext(vfwLogin,vfwPassword);
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar",vEx);
            }
        }

        void IWrpImpuestoBancarioVb.InitializeDefProg(string vfwProgramInitials,string vfwProgramVersion,string vfwDbVersion,string vfwStrDateOfVersion,string vfwStrHourOfVersion,string vfwValueSpecialCharacteristic,string vfwCountry,string vfwCMTO,bool vfwUsePASOnLine) {
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

        void IWrpImpuestoBancarioVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización",vEx);
            }
        }

        string IWrpImpuestoBancarioVb.BuscaAlicutoaImpTranscBancarias(string valFecha,string valAlicDebito) {
            string vResult = "";            
            try {                
                vResult = new Galac.Saw.Brl.Tablas.clsImpuestoBancarioNav().BuscaAlicutoaImpTranscBancarias(LibConvert.ToDate(valFecha),LibConvert.SNToBool(valAlicDebito));
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx,null,Title + " - " + "ImpuestoBancario");
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vResult;
        }
        #endregion //Miembros de IWrpVb
        #endregion //Metodos Generados
    } //End of class wrpImpuestoBancario

} //End of namespace Galac.Saw.Wrp.Tablas

