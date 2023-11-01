using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
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
using Galac.Adm.Uil.DispositivosExternos.ViewModel;
using Galac.Saw.Wrp.DispositivosExternos;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.DispositivosExternos {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.DispositivosExternos {
#else
namespace Galac.Saw.Wrp.DispositivosExternos {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpImpresoraFiscal:System.EnterpriseServices.ServicedComponent, IWrpImpresoraFisaclVb {

        #region Variables
        string _Title = "Impresora Fiscal";
        #endregion //Variables

        #region Propiedades
        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades

        #region Miembros de IWrpBalanzaVb

        void IWrpImpresoraFisaclVb.InitializeComponent(string vfwLogin,string vfwPassword,string vfwPath) {
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

        void IWrpImpresoraFisaclVb.InitializeDefProg(string vfwProgramInitials,string vfwProgramVersion,string vfwDbVersion,string vfwStrDateOfVersion,string vfwStrHourOfVersion,string vfwValueSpecialCharacteristic,string vfwCountry,string vfwCMTO,bool vfwUsePASOnLine) {
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

        void IWrpImpresoraFisaclVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización",vEx);
            }

        }                                 

        private LibGlobalValues CreateGlobalValues(string valCurrentMfc,string valCurrentParameters) {
            LibGlobalValues insGV = new LibGlobalValues();
            insGV.LoadCompleteAppMemInfo(valCurrentParameters);
            ((LibXmlMFC)insGV.GVDictionary[LibGlobalValues.NameMFCInfo]).Add("Compania",LibConvert.ToInt(valCurrentMfc));
            return insGV;
        }


        string IWrpImpresoraFisaclVb.ObtenerSerialMaquinaFiscal(string vfwImpresoraFiscal) {
            try {
                string vSerial = "";
                clsImpresoraFiscalMenu insImpresoraFiscalMenu = new clsImpresoraFiscalMenu();
                vSerial = insImpresoraFiscalMenu.ObtenerSerial(true,vfwImpresoraFiscal);
                return vSerial;
            } catch(Exception vEx) {                
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,"Impresora Fiscal");
            }
            return "";
        }

        string IWrpImpresoraFisaclVb.ObtenerUltimoNumeroFacturaImpresa(string vfwImpresoraFiscal) {
            try {
                string vUltimaFactura = "";
                clsImpresoraFiscalMenu insImpresoraFiscalMenu = new clsImpresoraFiscalMenu();
                vUltimaFactura = insImpresoraFiscalMenu.ObtenerUltimoNumeroFactura(true,vfwImpresoraFiscal);
                return vUltimaFactura;
            } catch(Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,"Impresora Fiscal");
            }
            return "";
        }

        string IWrpImpresoraFisaclVb.ObtenerUltimoNumeroNotaDeCreditoImpresa(string vfwImpresoraFiscal) {
            try {
                string vUltimaNC = "";
                clsImpresoraFiscalMenu insImpresoraFiscalMenu = new clsImpresoraFiscalMenu();
                vUltimaNC = insImpresoraFiscalMenu.ObtenerUltimoNumeroNotaDeCredito(true,vfwImpresoraFiscal);
                return vUltimaNC;
            } catch(Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,"Impresora Fiscal");
            }
            return "";
        }

        string IWrpImpresoraFisaclVb.ObtenerUltimoNumeroReporteZ(string vfwImpresoraFiscal) {
            try {
                string vUltimoRptZ = "";
                clsImpresoraFiscalMenu insImpresoraFiscalMenu = new clsImpresoraFiscalMenu();
                vUltimoRptZ = insImpresoraFiscalMenu.ObtenerUltimoNumeroReporteZ(true,vfwImpresoraFiscal);
                return vUltimoRptZ;
            } catch(Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,"Impresora Fiscal");
            }
            return "";
        }

        bool IWrpImpresoraFisaclVb.RealizarCierreX(string vfwImpresoraFiscal) {
            try {
                bool vReady = false;
                clsImpresoraFiscalMenu insImpresoraFiscalMenu = new clsImpresoraFiscalMenu();
                vReady = insImpresoraFiscalMenu.RealizarReporteX(true,vfwImpresoraFiscal);
                return vReady;
            } catch(Exception vEx) {                
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,"Impresora Fiscal");
            }
            return false;
        }

        bool IWrpImpresoraFisaclVb.AnularDocumentoFiscal(string vfwImpresoraFiscal,bool vfwAbrirConexion) {
            try {
                bool vReady = false;
                clsImpresoraFiscalMenu insImpresoraFiscalMenu = new clsImpresoraFiscalMenu();
                vReady = insImpresoraFiscalMenu.AnularDocumentoFiscal(vfwImpresoraFiscal,vfwAbrirConexion);
                return vReady;
            } catch(Exception vEx) {               
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,"Impresora Fiscal");
            }
            return false;
        }

        bool IWrpImpresoraFisaclVb.ImprimirVenta(string vfwImpresoraFiscal,string vfwXmlDocumentoFiscal,ref string NumDocument) {
            try {
                bool vReady = false;        
                clsImpresoraFiscalMenu insImpresoraFiscalMenu = new clsImpresoraFiscalMenu();
                vReady = insImpresoraFiscalMenu.ImprimirFacturaFiscal(vfwImpresoraFiscal,vfwXmlDocumentoFiscal);
                return vReady;
            } catch(Exception vEx) {                
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,"Impresora Fiscal");
            }
            return false;
        }

        bool IWrpImpresoraFisaclVb.ImprimirNotaDeCredito(string vfwImpresoraFiscal,string vfwXmlDocumentoFiscal,ref string NumDocumento) {
            try {
                bool vReady = false;                
                clsImpresoraFiscalMenu insImpresoraFiscalMenu = new clsImpresoraFiscalMenu();
                vReady = insImpresoraFiscalMenu.ImprimirNotaCredito(vfwImpresoraFiscal,vfwXmlDocumentoFiscal);
                return vReady;
            } catch(Exception vEx) {               
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,"Impresora Fiscal");
            }
            return false;
        }

        bool IWrpImpresoraFisaclVb.RealizarCierreZ(string vfwImpresoraFiscal,ref string NumDocumento) {
            try {
                bool vReady = false;              
                clsImpresoraFiscalMenu insImpresoraFiscalMenu = new clsImpresoraFiscalMenu();
                vReady = insImpresoraFiscalMenu.RealizarReporteZ(true,vfwImpresoraFiscal);
                return vReady;
            } catch(Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,"Impresora Fiscal");
            }
            return false;
        }

        bool IWrpImpresoraFisaclVb.ImprimirDocumentoNoFiscal(string vfwXmlImpresoraFiscal, string valTextoNoFiscal, string valDescripcion) {
            try {
                bool vReady = false;
                clsImpresoraFiscalMenu insImpresoraFiscalMenu = new clsImpresoraFiscalMenu();
                vReady = insImpresoraFiscalMenu.RealizarReporteZ(true, vfwXmlImpresoraFiscal);
                return vReady;
            } catch (Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, "Impresora Fiscal");
            }
            return false;        }

        #endregion //Miembros de IWrpVb    
    }
}
