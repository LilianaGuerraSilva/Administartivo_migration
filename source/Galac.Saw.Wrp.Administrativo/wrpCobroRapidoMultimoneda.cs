using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Vbwa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Galac.Adm.Brl.Venta;
using Galac.Saw.Ccl.SttDef;
using System.Xml.Linq;
using Galac.Saw.Wrp.Venta;
using Galac.Adm.Ccl.CajaChica;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Venta {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Venta {
#else
namespace Galac.Saw.Wrp.Venta {
#endif

    [ClassInterface(ClassInterfaceType.None)]
    public class wrpCobroRapidoMultimoneda : IWrpCobroRapidoMultimoneda {

        string _Title = "Cobro Rápido en Multimoneda";


        private string Title {
            get { return _Title; }
        }

        void IWrpCobroRapidoMultimoneda.Execute(string vfwAction) {
            try {
                ILibMenu insMenu = new Galac.Adm.Uil.Venta.clsCobroRapidoMultimonedaMenu();
                insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        string IWrpCobroRapidoMultimoneda.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Adm.Uil.Venta.clsCobroRapidoMultimonedaMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
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

        void IWrpCobroRapidoMultimoneda.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpCobroRapidoMultimoneda.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpCobroRapidoMultimoneda.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        string IWrpCobroRapidoMultimoneda.CobrarFacturaEnMultimoneda(int valConsecutivoCompania, string valNumeroFactura, string valFecha, decimal valTotalFactura, string valTipoDeDocumento, string valCodigoMonedaDelDocumento, string valCodigoMonedaDeCobro, string valTipoDeContribuyenteDelIva, string vfwCurrentParameters, ref string refIGTFParameters) {
            try {
                var insMenu = new Galac.Adm.Uil.Venta.clsCobroRapidoMultimonedaMenu();
                CreateGlobalValues(vfwCurrentParameters);
                DateTime vFechaDelDocumento = LibConvert.ToDate(valFecha);
                return insMenu.MostrarPantallaDeCobroRapidoEnMultimoneda(valConsecutivoCompania, valNumeroFactura, vFechaDelDocumento, valTotalFactura, valTipoDeDocumento, valCodigoMonedaDelDocumento, valCodigoMonedaDeCobro, valCodigoMonedaDeCobro, valTipoDeContribuyenteDelIva, ref refIGTFParameters);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return string.Empty;
        }

        string IWrpCobroRapidoMultimoneda.GenerarCobranzaYMovimientoBancarioDeCobroEnMultimoneda(int valConsecutivoCompania, string valNumeroFactura, string valTipoDeDocumento, string vfwCurrentParameters) {
            try {
                eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(valTipoDeDocumento);
                clsCobroDeFacturaNav vCobroFactura = new clsCobroDeFacturaNav();
                IList<string> vListaDeCobranzasGeneradas = new List<string>();
                CreateGlobalValues(vfwCurrentParameters);
                vCobroFactura.GenerarCobranzaYMovimientoBancarioDeCobroEnMultimoneda(valConsecutivoCompania, valNumeroFactura, vTipoDeDocumento, out vListaDeCobranzasGeneradas);
                XElement vXmlCobranzasGeneradas = new XElement("GpData");
                foreach (string Cobranza in vListaDeCobranzasGeneradas) {
                    vXmlCobranzasGeneradas.Add(new XElement("GpResult",
                            new XElement("NumeroCobranza", Cobranza)));
                }
                return vXmlCobranzasGeneradas.ToString();
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return string.Empty;
        }

        private LibGlobalValues CreateGlobalValues(string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            return LibGlobalValues.Instance;
        }

    }
}