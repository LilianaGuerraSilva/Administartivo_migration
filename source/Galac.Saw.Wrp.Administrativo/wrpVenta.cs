using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Uil.GestionProduccion;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Cnf;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Vbwa;
using LibGalac.Aos.Wrp;
using LibGalac.Aos.Wrp.Saw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

#if IsExeBsF 
namespace Galac.SawBsF.Wrp.Venta {
#else
namespace Galac.Saw.Wrp.Venta {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpVenta : System.EnterpriseServices.ServicedComponent, IWrpVenta {
        #region Variables
        string _Title = "Venta";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades

        void IWrpVenta.Execute(string vfwAction, string vfwCurrentCompany, string vfwCurrentParameters) {
            try {
                CreateGlobalValues(vfwCurrentCompany, vfwCurrentParameters);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        string IWrpVenta.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            try {
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - Escoger");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vResult;
        }

        void IWrpVenta.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpVenta.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpVenta.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibConvert.ToInt(valCurrentMfc));
            LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("RecordName", "ConsecutivoPeriodo"));
            return LibGlobalValues.Instance;
        }

        void IWrpVenta.NotificaSiEsNecesario(string valActionStr, int valConsecutivoCompania, string valNumero, int valTipoDocumento) {
            try {
                StringBuilder vChisme = new StringBuilder();
                IFacturaPdn insFactura = new clsFacturaNav();
                vChisme.Append(insFactura.MensajeDeNotificacionSiEsNecesario(valActionStr, valConsecutivoCompania, valNumero, valTipoDocumento));
                if (!LibString.IsNullOrEmpty(vChisme.ToString())) {
                    ILibSendMessage wrpSaw = new wrpSendMessage();
                    if (LibDate.F1IsEqualToF2(LibDate.Today(), LibConvert.ToDate(LibAppSettings.ReadAppSettingsKey("DEVQABACKDOOR")))) {
                        wrpSaw.SendMailFromGalac("desasaw@galac.com", "Notificación", vChisme.ToString(), "juan.garcia@galac.com");
                    } else {
                        //wrpSaw.SendMailFromGalac("controlfacturacion@seniat.gob.ve", "Notificación", vChisme.ToString(), "");
                    }
                }
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Notificación", vEx);
            }
        }
    }
}