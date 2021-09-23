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
using Galac.Contab.Core;
using Galac.Contab.Brl.WinCont;
using Galac.Contab.Ccl.WinCont;
using System.Xml.Linq;

#if IsExeBsF
namespace Galac.SawBsF.Wrp.WinCont {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.WinCont {
#else
namespace Galac.Saw.Wrp.WinCont {
#endif
    [ClassInterface(ClassInterfaceType.None)]

    public class wrpCentroDeCostos : System.EnterpriseServices.ServicedComponent, IWrpCentroDeCostos {
        #region Variables
        string _Title = "Centro De Costos";
        #endregion //Variables
        #region Propiedades
        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpTipoDeComprobante

        string IWrpCentroDeCostos.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Contab.Uil.WinCont.clsCentroDeCostosMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
                    vResult = vXmlDocument.InnerXml;
                }
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

        void IWrpCentroDeCostos.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpCentroDeCostos.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpCentroDeCostos.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }
        #endregion //Miembros de IWrpTipoDeComprobante

		private void CreateGlobalValues(string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania"));
            LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Periodo", "ConsecutivoPeriodo"));
        }

        #endregion //Metodos Generados

        bool IWrpCentroDeCostos.InsertRecord(int vfwConsecutivoCompania, int vfwConsecutivoPeriodo, string vfwCodigo, string vfwDescripcion, string vfwCurrentParameters) {
            CreateGlobalValues(vfwCurrentParameters);
            ICentroDeCostosPdn vCentroDeCostosNav = new clsCentroDeCostosNav();
            return vCentroDeCostosNav.InsertRecord(vfwConsecutivoCompania, vfwConsecutivoPeriodo,vfwCodigo, vfwDescripcion);
        }

        string IWrpCentroDeCostos.BuscarPorCodigo(int vfwConsecutivoCompania, int vfwConsecutivoPeriodo, string vfwCodigo) {
            string vResult = "";
            ICentroDeCostosPdn vCentroDeCostosNav = new clsCentroDeCostosNav();
            XElement vXmlResult = vCentroDeCostosNav.BuscarPorCodigoInterop(vfwConsecutivoCompania, vfwConsecutivoPeriodo, vfwCodigo);
            if (vXmlResult != null) {
                vResult = vXmlResult.ToString();
            }
            return vResult;
        }

        bool IWrpCentroDeCostos.TrasladarCentrosDeCostoAOtroPeriodo(int vfwConsecutivoCompania, int vfwConsecutivoPeriodoActual, int vfwConsecutivoPeriodoNuevo) {
            bool vresult = false;
            try {
                LibGlobalValues.Instance.GetMfcInfo().Add("Compania",vfwConsecutivoCompania);
                LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", vfwConsecutivoPeriodoActual);

                ICentroDeCostosPdn insTrasladarCentrosDeCostoAOtroPeriodoPdn = new clsCentroDeCostosNav();
                vresult = insTrasladarCentrosDeCostoAOtroPeriodoPdn.TrasladarCentrosDeCostoAOtroPeriodo(vfwConsecutivoCompania, vfwConsecutivoPeriodoActual, vfwConsecutivoPeriodoNuevo);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "TrasladarCentrosDeCostoAOtroPeriodo");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vresult;
        }

        string XmlValoresGlobalesParaControladoresMultiarchivoActual(int valConsecutivoCompaniaActual, int valConsecutivoPeriodoActual) {
            string vXmlValoresGlobales = "<GpParameters><Compania><ConsecutivoCompania>" + valConsecutivoCompaniaActual.ToString() + "</ConsecutivoCompania></Compania><Periodo><ConsecutivoPeriodo>" + valConsecutivoPeriodoActual.ToString() + "</ConsecutivoPeriodo></Periodo></GpParameters>";
            return vXmlValoresGlobales;
        }

    } //End of class wrpTipoDeComprobante
} //End of namespace Galac.Saw.Wrp.Tablas