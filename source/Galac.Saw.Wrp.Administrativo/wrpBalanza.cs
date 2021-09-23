using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Uil;
using System.Runtime.InteropServices;
using LibGalac.Aos.Vbwa;
using Galac.Adm.Uil.DispositivosExternos;
using Galac.Saw.Wrp.DispositivosExternos;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.DispositivosExternos {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.DispositivosExternos {
#else
namespace Galac.Saw.Wrp.DispositivosExternos {
#endif

    [ClassInterface(ClassInterfaceType.None)]
    public class wrpBalanza :System.EnterpriseServices.ServicedComponent, IWrpBalanzaVb {
        #region Variables
        string _Title = "Balanza";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpBalanzaVb

        void IWrpBalanzaVb.Execute(string vfwAction,string vfwCurrentMfc,string vfwCurrentParameters) {
            try {
                CreateGlobalValues(vfwCurrentParameters);
                ILibMenu insMenu = new Galac.Adm.Uil.DispositivosExternos.clsBalanzaMenu();
                insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction),1);
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx,null,Title + " - " + vfwAction);
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        string IWrpBalanzaVb.Choose(string vfwParamInitializationList,string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if(Galac.Adm.Uil.DispositivosExternos.clsBalanzaMenu.ChooseFromInterop(ref vXmlDocument,vSearchValues,vFixedValues)) {
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

        void IWrpBalanzaVb.InitializeComponent(string vfwLogin,string vfwPassword,string vfwPath) {
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

        void IWrpBalanzaVb.InitializeDefProg(string vfwProgramInitials,string vfwProgramVersion,string vfwDbVersion,string vfwStrDateOfVersion,string vfwStrHourOfVersion,string vfwValueSpecialCharacteristic,string vfwCountry,string vfwCMTO,bool vfwUsePASOnLine) {
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

        void IWrpBalanzaVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización",vEx);
            }
        }
        #endregion //Miembros de IWrpBalanzaVb

        private void CreateGlobalValues(string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("DispositivosExternos","ConsecutivoCompania"));
        }
        #endregion //Metodos Generados


    } //End of class wrpBalanza

} //End of namespace Galac.Saw.Wrp.DispositivosExternos

