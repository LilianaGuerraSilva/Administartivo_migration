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
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Wrp.Administrativo;
using Galac.Saw.Uil.Inventario;
using Galac.Saw.Brl.Inventario;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Inventario {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Inventario {
#else
namespace Galac.Saw.Wrp.Inventario {
#endif

    [ClassInterface(ClassInterfaceType.None)]
    public class wrpTransferencia : System.EnterpriseServices.ServicedComponent, IWrpTransferencia {

        private string Title {
            get { return "Transferencia"; }
        }

        #region Miembros de IWrpTransferencia       

        void IWrpTransferencia.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpTransferencia.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        void IWrpTransferencia.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        
        #endregion //Miembros de IWrpTransferencia

        #region Metodos

        private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibConvert.ToInt(valCurrentMfc));
            LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("RecordName", "ConsecutivoPeriodo"));
            return LibGlobalValues.Instance;
        }
        bool IWrpTransferencia.ActualizarLoteDeInventario(int valConsecutivoCompania, string valNumeroDocumento, bool valEsInsertar) {
            ITransferenciaPdn transferenciaPdn = new clsTransferenciaNav();
            return transferenciaPdn.ActualizarLoteDeInventario(valConsecutivoCompania, valNumeroDocumento, valEsInsertar);
        }

        #endregion

    } //End of class WrpTransferencia

} //End of namespace Galac.Saw.Wrp.Inventario

