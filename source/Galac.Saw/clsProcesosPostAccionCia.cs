using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Messaging;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using Galac.Contab.Ccl.WinCont;
using Galac.Contab.Brl.WinCont;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.Brl;

namespace Galac.Saw {
    internal class clsProcesosPostAccionCia {
        #region Variables
        private static clsProcesosPostAccionCia _Instance = new clsProcesosPostAccionCia();
        private static ILibMainProcess _MainProcess;
        #endregion

        #region Constructor
        private clsProcesosPostAccionCia() {
        }
        #endregion

        #region Métodos
        public static void RegisterMessages(ILibMainProcess valMainProcess) {
            _MainProcess = valMainProcess;
            LibBusinessProcess.Register(_Instance, "ProcesosPostInsertarCompania", EjecutarProcesosPostInsertarCompania);
            LibBusinessProcess.Register(_Instance, "ProcesosPostModificarCompania", EjecutarProcesosPostModificarCompania);
            LibBusinessProcess.Register(_Instance, "ProcesosPostEliminarCompania", EjecutarProcesosPostEliminarCompania);
            LibBusinessProcess.Register(_Instance, "CompaniaUsaEstructuraDeCostos", EjecutarCompaniaUsaEstructuraDeCostos);
            LibBusinessProcess.Register(_Instance, "CompaniaDetallarCostoPorElementosDelCosto", EjecutarCompaniaDetallarCostoPorElementosDelCosto);
            LibBusinessProcess.Register(_Instance, "ActualizarParametrosConciliacion", EjecutarActualizarParametrosConciliacion);
            LibBusinessProcess.Register(_Instance, "ProcesosChooseCompanyPostEliminarCompania", EjecutarProcesosChooseCompanyPostEliminarCompania);
            LibBusinessProcess.Register(_Instance, "ProcesosBeforeEliminarCompania", EjecutarProcesosBeforeEliminarCompania);            
            LibBusinessProcess.Register(_Instance, "BuscarSiExisteAlmenosUnAuxiliar", EjecutarBuscarSiExisteAlmenosUnAuxiliar);
            LibBusinessProcess.Register(_Instance, "ActivarDesactivarUsaCierreDeMes", EjecutarActivarDesactivarUsaCierreDeMes);
            LibBusinessProcess.Register(_Instance, "ActivarModuloActivoFijo", EjecutarActivarModuloActivoFijo);
            LibBusinessProcess.Register(_Instance, "BuscarSiExisteAlgunaCuentaConActivoFijo", EjecutarBuscarSiExisteAlgunaCuentaConActivoFijo);
            LibBusinessProcess.Register(_Instance, "ActivarModuloConexionAxi", EjecutarActivarModuloConexionAxi);
            LibBusinessProcess.Register(_Instance, "DesactivarModuloConexionAxi", EjecutarDesactivarModuloConexionAxi);
            LibBusinessProcess.Register(_Instance, "ActivarModuloCostoDeVenta", EjecutarActivarModuloCostoDeVenta);
            LibBusinessProcess.Register(_Instance, "DesactivarModuloCostoDeVenta", EjecutarDesactivarModuloCostoDeVenta);
            LibBusinessProcess.Register(_Instance, "RefrescarValoresGlobales", EjecutarRefrescarValoresGlobales);
            
        }

        #region ProcesosPostInsertarCompania
        private static void EjecutarProcesosPostInsertarCompania(LibBusinessProcessMessage valMessage) {
            int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            InsertarValoresPorDefectoDeElementoDelCosto(vConsecutivoCompania);
            InsertarValoresPorDefectoDeParametrosDeActivosFijo(vConsecutivoCompania);
            _MainProcess.TryExecuteCreateMfc("Periodo");
            string vNombre = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreCorto");
            _MainProcess.ChooseCompany(vNombre);
            _MainProcess.LoadGlobalValuesForComponents();
        }

        private static void InsertarValoresPorDefectoDeElementoDelCosto(int valConsecutivoCompania) {
            //IElementoDelCostoPdn vPdn = new clsElementoDelCostoNav();
            //vPdn.InsertarRegistroPorDefecto(valConsecutivoCompania);
        }

        private static void InsertarValoresPorDefectoDeParametrosDeActivosFijo(int valConsecutivoCompania) {
            //IParametrosActivoFijoPdn vPdn = new clsParametrosActivoFijoNav();
            //vPdn.InsertarValoresPorDefecto(valConsecutivoCompania);
        }

        private static void InsertarParametrosConciliacion(int valConsecutivoCompania) {
            //IParametrosConciliacionPdn vPdn = new clsParametrosConciliacionNav();
            //vPdn.InsertarValoresPorDefecto(valConsecutivoCompania);
        }

        private static void ActualizarParametrosConciliacion(XElement valCompaniaElement) {
            if (valCompaniaElement == null || !valCompaniaElement.HasElements) {
                return;
            }
            int vConsecutivoCompania = LibConvert.ToInt(LibXml.GetPropertyString(valCompaniaElement, "ConsecutivoCompania"));
            bool vUsaEstructuraDeCostos = LibConvert.SNToBool(LibXml.GetPropertyString(valCompaniaElement, "GpResult", "UsaEstructuraDeCostos"));
            bool vDetallarCostoPorElementosDelCosto = LibConvert.SNToBool(LibXml.GetPropertyString(valCompaniaElement, "GpResult", "DetallarCostoPorElementosDelCosto"));
            IParametrosConciliacionPdn vPdn = new clsParametrosConciliacionNav();
            vPdn.ActualizarValorParametroDetallarCostoPorElementodelCosto(vConsecutivoCompania, vDetallarCostoPorElementosDelCosto);
            vPdn.ActualizarValorParametroUsaLeyCosto(vConsecutivoCompania, vUsaEstructuraDeCostos);
        }
        #endregion

        #region ProcesosPostModificarCompania
        private static void EjecutarProcesosPostModificarCompania(LibBusinessProcessMessage valMessage) {
            string vNombre = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreCorto");
            _MainProcess.ChooseCompany(vNombre);
            _MainProcess.LoadGlobalValuesForComponents();
        }
        #endregion

        #region ProcesosPostEliminarCompania
        private static void EjecutarProcesosPostEliminarCompania(LibBusinessProcessMessage valMessage) {

        }

        private static void EjecutarProcesosChooseCompanyPostEliminarCompania(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*ICompaniaPdn vPdn = new clsCompaniaNav();
            int vConsecutivoCompania = LibConvert.ToInt(valMessage.Content);
            int vConsecutivoCompaniaActual = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            if (vConsecutivoCompania == vConsecutivoCompaniaActual) {
                string vNombre = vPdn.PrimeraCompaniaConAcceso();
                _MainProcess.ChooseCompany(vNombre);
                if (!LibString.IsNullOrEmpty(vNombre)) {
                    _MainProcess.LoadGlobalValuesForComponents();
                }
            }
            */
        }

        private static void EjecutarProcesosBeforeEliminarCompania(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*
            int vConsecutivoCompania = LibConvert.ToInt(valMessage.Content);
            IContabilidadProcesos vContabilidadProcesos = new clsContabilidadIntegradaHelper();
            vContabilidadProcesos.EliminarMultiarchivosDeEmpresa(vConsecutivoCompania);
            */
        }
        #endregion

        private static void EjecutarCompaniaUsaEstructuraDeCostos(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*
            int vConsecutivoCompania = LibConvert.ToInt(valMessage.Content);
            IParametrosConciliacionPdn vPdn = new clsParametrosConciliacionNav();
            bool vUsaEstructuraDeCostos = vPdn.CompaniaUsaEstructuraDeCostos(vConsecutivoCompania);
            if (valMessage.Callback != null) {
                valMessage.Result = vUsaEstructuraDeCostos;
                valMessage.Callback();
            }
            */
        }

        private static void EjecutarCompaniaDetallarCostoPorElementosDelCosto(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*
            int vConsecutivoCompania = LibConvert.ToInt(valMessage.Content);
            IParametrosConciliacionPdn vPdn = new clsParametrosConciliacionNav();
            bool vDetallarCostoPorElementosDelCosto = vPdn.CompaniaDetallarCostoPorElementosDelCosto(vConsecutivoCompania);
            if (valMessage.Callback != null) {
                valMessage.Result = vDetallarCostoPorElementosDelCosto;
                valMessage.Callback();
            }
            */
        }

        private static void EjecutarActualizarParametrosConciliacion(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*
            XElement vElement = valMessage.Content as XElement;
            int vConsecutivoCompania = LibConvert.ToInt(LibXml.GetPropertyString(vElement, "ConsecutivoCompania"));
            bool vUsaEstructuraDeCostos = LibConvert.SNToBool(LibXml.GetPropertyString(vElement, "GpResult", "UsaEstructuraDeCostos"));
            bool vDetallarCostoPorElementosDelCosto = LibConvert.SNToBool(LibXml.GetPropertyString(vElement, "GpResult", "DetallarCostoPorElementosDelCosto"));
            IParametrosConciliacionPdn vPdn = new clsParametrosConciliacionNav();
            vPdn.InsertaOActualizaParametrosConciliacion(vConsecutivoCompania, vUsaEstructuraDeCostos, vDetallarCostoPorElementosDelCosto);
            */
        }

        private static void EjecutarBuscarSiExisteAlmenosUnAuxiliar(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*
            IAuxiliarPdn insAuxiliar = new clsAuxiliarNav();
            int vConsecutivoCompania = LibConvert.ToInt(valMessage.Content);
            bool vExisteAlMenosUnAuxiliar = insAuxiliar.ExisteAlMenosUnAuxiliar(vConsecutivoCompania);
            if(valMessage.Callback != null){
                valMessage.Result = vExisteAlMenosUnAuxiliar;
                valMessage.Callback();
            } 
            */
        }

        private static void EjecutarActivarDesactivarUsaCierreDeMes(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*
            XElement vElement = valMessage.Content as XElement;
            int vConsecutivoCompania = LibConvert.ToInt(LibXml.GetPropertyString(vElement, "ConsecutivoCompania"));
            int vConsecutivoPeriodo = LibConvert.ToInt(LibXml.GetPropertyString(vElement, "ConsecutivoPeriodo"));
            bool vActivar = LibConvert.ToBool(LibXml.GetPropertyString(vElement, "GpResult", "Activar"));
            IPeriodoPdn insPeriodo = new clsPeriodoNav();
            insPeriodo.ActivarDesactivarCierreDeMes(vConsecutivoCompania, vConsecutivoPeriodo, vActivar);            
            */
        }

        private static void EjecutarActivarModuloActivoFijo(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*
            IPeriodoPdn insPeriodo = new clsPeriodoNav();
            XElement vElement = valMessage.Content as XElement;
            IParametrosActivoFijoPdn insParametros = new clsParametrosActivoFijoNav();
            int vConsecutivoCompania = LibConvert.ToInt(LibXml.GetPropertyString(vElement, "ConsecutivoCompania"));
            InsertarValoresPorDefectoDeParametrosDeActivosFijo(vConsecutivoCompania); //Inserto los parámetros
            insPeriodo.InsertarGruposDeActivosPorDefectoAlActivarModulo(vConsecutivoCompania);            
            */
        }

        private static void EjecutarBuscarSiExisteAlgunaCuentaConActivoFijo(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*
            ICuentaPdn insCuenta = new clsCuentaNav();
            int vConsecutivoCompania = LibConvert.ToInt(valMessage.Content);
            bool vExisteCuentaConActivoFijo = insCuenta.BuscarSiExisteAlgunaCuentaConActivoFijo(vConsecutivoCompania);
            if (valMessage.Callback != null) {
                valMessage.Result = vExisteCuentaConActivoFijo;
                valMessage.Callback();
            }
            */
        }

        private static void EjecutarActivarModuloConexionAxi(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*
            XElement vElement = valMessage.Content as XElement;
            ICompaniaDefinirCuentasFlujoDeEfectivoPdn insCompaniaDefinir = new clsCompaniaDefinirCuentasFlujoDeEfectivoNav();
            ICuentaPdn insCuenta = new clsCuentaNav();
            IComprobanteDetallePdn insComprobanteDetalle = new clsComprobanteDetalleNav();
            int vConsecutivoCompania = LibConvert.ToInt(LibXml.GetPropertyString(vElement, "ConsecutivoCompania"));            
            EjecutarActivarModuloActivoFijo(valMessage);
            insCompaniaDefinir.InsertarValoresPorDefecto(vConsecutivoCompania);
            insCuenta.AsignarValoresPorDefectosACamposDeConexionConAXI(vConsecutivoCompania, true);            
            */
        }

        private static void EjecutarDesactivarModuloConexionAxi(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*
            XElement vElement = valMessage.Content as XElement;
            IComprobanteDetallePdn insComprobanteDetalle = new clsComprobanteDetalleNav();
            ICompaniaDefinirCuentasFlujoDeEfectivoPdn insCompaniaDef = new clsCompaniaDefinirCuentasFlujoDeEfectivoNav();
            int vConsecutivoCompania = LibConvert.ToInt(LibXml.GetPropertyString(vElement, "ConsecutivoCompania"));
            insComprobanteDetalle.AsignaNoAlCampoAfectaEfectivoDeConexionConAXI(vConsecutivoCompania);
            insCompaniaDef.EliminaRecordCuentasFlujoEfectivo(vConsecutivoCompania);            
            */
        }

        private static void EjecutarActivarModuloCostoDeVenta(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*
            if (LibWindowsManager.IsThereAnyOpenInput()) {
                LibMessages.MessageBox.Alert(null, "Debe cerrar todas las pantallas para poder Definir las Cuentas Informes.", "Compañía");
            } else {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(null, "A continuación se le pedirá que introduzca los valores de las cuentas de Costo de Ventas para los informes.", "Información");
                Periodo vModel = new Periodo();
                vModel.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
                vModel.ConsecutivoPeriodo = LibGlobalValues.Instance.GetMfcInfo().GetInt("Periodo");
                PeriodoViewModel vViewModel = new PeriodoViewModel(vModel, eAccionSR.Modificar);
                vViewModel.InitializeViewModel(eAccionSR.Modificar);
                vViewModel.InitializeViewModelParaDefinirCuentasInformes();
                vViewModel.ActivarModuloCostoDeVenta = true; 
                LibMessages.EditViewModel.ShowEditor(vViewModel, true);            
            }
            */
        }

        private static void EjecutarDesactivarModuloCostoDeVenta(LibBusinessProcessMessage valMessage) {
            throw new NotImplementedException();
            /*
            IPeriodoPdn insPeriodo = new clsPeriodoNav();
            XElement vElement = valMessage.Content as XElement;
            LibResponse vResult = new LibResponse();
            int vConsecutivoCompania = LibConvert.ToInt(LibXml.GetPropertyString(vElement, "ConsecutivoCompania"));
            int vConsecutivoPeriodo = LibConvert.ToInt(LibXml.GetPropertyString(vElement, "ConsecutivoPeriodo"));
            vResult = insPeriodo.PasarCtasDeCostoDeVentaADefinicionGasto(vConsecutivoCompania, vConsecutivoPeriodo);
            if (vResult.Success) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Information(null, "Las Cuentas que estaban en 'Costo de Venta' fueron reubicadas como 'Cuentas de Gasto'.", "Información");
            }
            */
        }

        private static void EjecutarRefrescarValoresGlobales(LibBusinessProcessMessage valMessage) {
            string vNombre = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreCorto");
            _MainProcess.ChooseCompany(vNombre);
            _MainProcess.LoadGlobalValuesForComponents();
        }

       
        #endregion
    }
}
