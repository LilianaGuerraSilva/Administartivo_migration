using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Uil.Usal;
using System.Threading;
using LibGalac.Aos.UI.WpfMainInterop;
using System.Xml.Linq;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.Vbwa;
using System.Reflection;
using System.Xml;
using LibGalac.Aos.DefGen;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.MenuBar {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.MenuBar {
#else
namespace Galac.Saw.Wrp.MenuBar {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpMenuBarTablas : System.EnterpriseServices.ServicedComponent, IWrpMfMenuBarVb {
        #region Variables
        string _Title = "Menú Tablas - Contabilidad";
        #endregion //Variables
        private string Title {
            get {
                return _Title;
            }
        }
        void IWrpMfMenuBarVb.CallMain(string vfwCurrentParameters) {
            try {
                LibGlobalValues.Instance.LoadCompleteAppMemInfo(vfwCurrentParameters);
                LibGlobalValues.Instance.LoadMFCInfoFromAppMemInfo("Compania", "ConsecutivoCompania");
                LibMefBootstrapperForInterop vBootstrapper = new LibMefBootstrapperForInterop(false);
                LibInteropParameters vParams = new LibInteropParameters();
                if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                    vParams.AdmittedComponents = ComponentsNavigationTab();
                } else if(LibDefGen.ProgramInfo.IsCountryPeru())  {
                    vParams.AdmittedComponents = ComponentsNavigationTabPeru();
                }
                vParams.CurrentUserName = ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login;
                vParams.CurrentCompanyName = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre");
                vParams.ProgramImagePath = new System.Uri("/Images/Fondo Saw.jpg", System.UriKind.Relative);
                vBootstrapper.Components = ComponentsList();
                vBootstrapper.Run(vParams);
            } catch (AccessViolationException) {
                throw;
            } catch (ReflectionTypeLoadException vEx) {
                string[] vErrors = vEx.LoaderExceptions.Select(ex => ex.GetBaseException().Message).ToArray();
                string vMessage = string.Join(Environment.NewLine + Environment.NewLine, vErrors);
                LibExceptionDisplay.Show(new GalacException(vMessage, eExceptionManagementType.Uncontrolled, vEx));
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, Title);
            }
        }

        void IWrpMfMenuBarVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpMfMenuBarVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpMfMenuBarVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        private XElement ComponentsNavigationTab() {
            XElement vResult = new XElement("Components",
                  new XElement("UilComponents"
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefImpuestoAlicuotaImpuestoEspecial"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasImpuestoBancario"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenBanco"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenCambio"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefInventarioCategoria"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenCIIU"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenCiudad"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefImpuestoClasificadorActividadEconomica"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasCondicionesDePago"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasFormaDelCobro"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefImpuestoFormatosImpMunicipales"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasLineaDeProducto"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasMaquinaFiscal"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenMoneda"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenMonedaLocal"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenMunicipio"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenMunicipioCiudad"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasNotaFinal"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenPais"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasPropAnalisisVenc"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasRutaDeComercializacion"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenSectorDeNegocio"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasLeyTarifaN2"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasTipoProveedor"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasUrbanizacionZP"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasUnidadDeVenta"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasLeyValorUT"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasZonaCobranza"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasOtrosCargosDeFactura"), new XAttribute("Module", "Tablas"))
                   ));
            return vResult;
        }

        private XElement ComponentsNavigationTabPeru() {
            XElement vResult = new XElement("Components",
                  new XElement("UilComponents",
                      new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenBanco"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefInventarioCategoria"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenCiudad"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefVehiculoConductor"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasFormaDelCobro"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasMaquinaFiscal"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefVehiculoMarca"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenMoneda"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenMonedaLocal"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefVentaMotivoDeTraslado"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasNotaFinal"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenPais"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasPropAnalisisVenc"), new XAttribute("Module", "Tablas"))                     
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenSectorDeNegocio"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefImpuestoTablaDetraccion"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasTipoProveedor"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasUnidadDeVenta"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasUrbanizacionZP"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasZonaCobranza"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefImpuestoAranceles"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenUbigeo"), new XAttribute("Module", "Tablas"))
                      , new XElement("UilComponent", new XAttribute("Name", "UIMefTablasGenCambio"), new XAttribute("Module", "Tablas"))
                   ));
            return vResult;
        }

        private List<string> ComponentsList() {
            List<string> vResult = new List<string>();
            vResult.Add("Galac.Saw.Uil.Tablas");
            vResult.Add("Galac.Saw.Uil.Inventario");
            vResult.Add("Galac.Saw.Uil.Vehiculo");
            vResult.Add("Galac.Adm.Uil.Venta");
            vResult.Add("Galac.Comun.Uil.TablasGen");
            vResult.Add("Galac.Comun.Uil.Impuesto");
            vResult.Add("Galac.Comun.Uil.TablasLey");
            return vResult;
        }
    }
}
