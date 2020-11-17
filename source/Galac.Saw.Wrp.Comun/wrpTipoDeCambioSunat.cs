using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Vbwa;
using Galac.Saw.Lib;

namespace Galac.Saw.Wrp.TablasGen {
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpTipoDeCambioSunat : System.EnterpriseServices.ServicedComponent, IWrpTipoDeCambioSunat {

        #region Variables
        string _Title = "TipoCambioSunat";
        TipoCambioSunat _TipoCambioSunat;
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

        void IWrpTipoDeCambioSunat.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                _TipoCambioSunat = new TipoCambioSunat();
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);
            } catch (GalacException vEx) {
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpTipoDeCambioSunat.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpTipoDeCambioSunat.InitializeContext(string vfwInfo) {
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
            return LibGlobalValues.Instance;
        }

        void RegisterDefaultTypesIfMissing() {
            LibGalac.Aos.Uil.LibMessagesHandler.RegisterMessages();
        }

        #endregion //Miembros de IWrpMfVb


        //DateTime FechaCambioSunat();
        //decimal ObtenerCambioCompraSunat();
        //decimal ObtenerCambioVentaSunat();

        string IWrpTipoDeCambioSunat.ExecuteGetCambio() {
            StringBuilder vResult = new StringBuilder();
            TipoCambioSunat vTipoCambio;
            try {
                vTipoCambio = _TipoCambioSunat.GetCambio();
                vResult.AppendLine("<GpData>");
                vResult.AppendLine("   <GpResult>");
                vResult.AppendLine("      <CambioVenta>" + vTipoCambio.Venta.ToString() + "</CambioVenta>");
                vResult.AppendLine("      <CambioCompra>" + vTipoCambio.Compra.ToString() + "</CambioCompra>");
                vResult.AppendLine("      <FechaCambio>" + vTipoCambio.FechaEnCurso.ToString() + "</FechaCambio>");
                vResult.AppendLine("   </GpResult>");
                vResult.AppendLine("</GpData>");
            } catch (GalacException vEx) {
                _TipoCambioSunat.Compra = 0;
                _TipoCambioSunat.Compra = 0;
                _TipoCambioSunat.FechaEnCurso = LibDate.Today();
                throw new GalacWrapperException("Error de conexion en execute cambio " + vEx.Message, vEx);
            }
            return vResult.ToString();
        }

        //void IWrpTipoDeCambioSunat.ExecuteGetCambio () {
        //    try {
        //         _TipoCambioSunat.GetCambio ();
        //    } catch (GalacException vEx) {
        //        _TipoCambioSunat.Compra = 0;
        //        _TipoCambioSunat.Compra = 0;
        //        _TipoCambioSunat.FechaEnCurso = LibDate.Today();
        //        throw new GalacWrapperException("Error de conexion en execute cambio " + vEx.Message, vEx);
        //    }
        //}

        DateTime IWrpTipoDeCambioSunat.FechaCambioSunat() {
            try {
                return _TipoCambioSunat.FechaEnCurso;
            } catch (Exception vEx) {
                throw new GalacWrapperException("Obtener el monto del Cambio de Compra Sunat", vEx);
            }
        }

        decimal IWrpTipoDeCambioSunat.ObtenerCambioCompraSunat () {
            try {
                return _TipoCambioSunat.Compra ;
            } catch (Exception vEx) {
                throw new GalacWrapperException("Obtener el monto del Cambio de Compra Sunat", vEx);
            }
        }

        decimal IWrpTipoDeCambioSunat.ObtenerCambioVentaSunat () {
            try {
                return _TipoCambioSunat.Venta ;
            } catch (Exception vEx) {
                throw new GalacWrapperException("Obtener el monto del Cambio de Venta Sunat", vEx);
            }
        }
        #endregion //Metodos Generados



    }
}
