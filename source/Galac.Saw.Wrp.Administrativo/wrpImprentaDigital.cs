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
using Galac.Adm.Uil.ImprentaDigital;
using Galac.Saw.Ccl.SttDef;

#if IsExeBsF
namespace Galac.SawBsF.Wrp.ImprentaDigital {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.ImprentaDigital {
#else
namespace Galac.Saw.Wrp.ImprentaDigital {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpImprentaDigital: System.EnterpriseServices.ServicedComponent, IWrpImprentaDigitalVb {
        #region Variables
        string _Title = "Imprenta Dí­gital";
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

        bool IWrpImprentaDigitalVb.Execute(eTipoDocumentoFactura vfwTipoDocumento, string vfwNumeroFactura, eAccionSR vfwAction, string vfwCurrentParameters, ref string vfwNumeroControl) {
        bool IWrpImprentaDigitalVb.Execute(int vfwTipoDocumento, string vfwNumeroFactura, int vfwAction, string vfwCurrentParameters, bool vfwEsPorLote, ref string vfwNumeroControl) {
            try {
                bool vResult = false;
                eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)vfwTipoDocumento;
                eAccionSR vAction = (eAccionSR)vfwAction;
                CreateGlobalValues(vfwCurrentParameters);
                clsDocumentoDigitalMenu insMenu = new clsDocumentoDigitalMenu();
                vResult = insMenu.EjecutarAccion(vfwTipoDocumento, vfwNumeroFactura, vfwAction, ref vfwNumeroControl);
                clsDocumentoDigitalMenu insMenu = new clsDocumentoDigitalMenu();                
                vResult = insMenu.EjecutarAccion(vTipoDeDocumento, vfwNumeroFactura, vAction, vfwEsPorLote, ref vfwNumeroControl);
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
                return false;
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
                return false;
            }
        }   

        string IWrpImprentaDigitalVb.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                //if (Galac.Adm.Uil.ImprentaDigital.clsFacturaDigitalMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
                //    vResult = vXmlDocument.InnerXml;
                //}
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - Escoger");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }

        void IWrpImprentaDigitalVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpImprentaDigitalVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpImprentaDigitalVb.InitializeContext(string vfwInfo) {
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
            LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Periodo", "ConsecutivoPeriodo"));
        }

        
        #endregion //Metodos Generados
    } //End of class wrpImprentaDigital
} //End of namespace Galac.Saw.Wrp.ImprentaDigital

