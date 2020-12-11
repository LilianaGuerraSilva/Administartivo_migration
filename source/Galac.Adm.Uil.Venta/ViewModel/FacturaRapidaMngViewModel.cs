using System;
using System.Collections.Generic;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.Catching;
using LibGalac.Aos.ARRpt.Reports;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal;
using System.Xml.Linq;
using Galac.Adm.Uil.DispositivosExternos.ViewModel;
using Galac.Saw.Lib;
using System.Text;
using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Ccl.TablasGen;

namespace Galac.Adm.Uil.Venta.ViewModel {

    public class FacturaRapidaMngViewModel : LibMngMasterViewModelMfc<FacturaRapidaViewModel, FacturaRapida> {
        #region Variables
        private clsNoComunSaw _clsNoComun = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get {
                return "Punto de Venta";
            }
        }
        #endregion //Propiedades
        #region Constructores

        public FacturaRapidaMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Numero, TipoDeDocumento";
            _clsNoComun = new clsNoComunSaw();
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
            OrderByDirection = "DESC";
        */
            #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override FacturaRapidaViewModel CreateNewElement(FacturaRapida valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new FacturaRapida();
            }
            return new FacturaRapidaViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_FacturaRapida_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<FacturaRapida>, IList<FacturaRapida>> GetBusinessComponent() {
            return new clsFacturaRapidaNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return null; // new clsFacturaRapidaRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
            SUPROCESOPARTICULARCommand = new RelayCommand(ExecuteSUPROCESOPARTICULARCommand, CanExecuteSUPROCESOPARTICULARCommand);
        */
            #endregion //Codigo Ejemplo
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateSUPROCESOPARTICULARRibbonGroup());
        */
                #endregion //Codigo Ejemplo
            }
        }
        #endregion //Metodos Generados

        protected override bool CanExecuteCreateCommand() {
            return true;
        }

        protected override bool HasAccessToModule() {
            return true;
        }

        protected override void SearchItems() {

        }

        private bool InitializeBalanza(ref BalanzaTomarPesoViewModel valBalanzaTomarPesoViewModel, ref bool refUsaBalanzaEnPOS) {
            bool vResult = false;
            refUsaBalanzaEnPOS = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "UsarBalanza"));
            try {
                if (refUsaBalanzaEnPOS && valBalanzaTomarPesoViewModel == null) {
                    valBalanzaTomarPesoViewModel = new BalanzaTomarPesoViewModel();
                    vResult = valBalanzaTomarPesoViewModel.ComprobarEstado();
                    if (!vResult) {
                        if (LibMessages.MessageBox.YesNo(this, "No hay comunicación con la balanza, debe verificar configuración y cableados. Desea continuar sin tomar el peso en los artículos?", "")) {
                            refUsaBalanzaEnPOS = vResult;
                            vResult = !vResult;
                        }
                    } else {
                        refUsaBalanzaEnPOS = vResult;
                    }
                } else {
                    return true;
                }
            } catch (GalacException vEx) {
                if (vEx.ExceptionManagementType == eExceptionManagementType.Validation) {
                    if (LibMessages.MessageBox.YesNo(this, vEx.Message + ".\r\nDesea continuar sin tomar el peso en los artículos?", "")) {
                        refUsaBalanzaEnPOS = false;
                        vResult = true;
                    }
                } else {
                    LibMessages.MessageBox.Information(this, vEx.Message, "");
                }
            } catch (Exception vEx) {
                LibMessages.MessageBox.Information(this, vEx.Message, "");
            }
            return vResult;
        }        

        private bool SeDefinieronParametrosBancariosValidos() {
            bool SeDetectaronParametrosBancarios = false;
            try {
                string vCodigoMonedaExtranjera = string.Empty;
                bool vUsaCobroDirectoMultimoneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaCobroDirectoEnMultimoneda");
                if (EmpresaUsaMonedaExtranjeraComoPredeterminada(out vCodigoMonedaExtranjera)) {
                    if(vUsaCobroDirectoMultimoneda) {
                        if (EsPosibleCobroDirectoMultimoneda(vCodigoMonedaExtranjera)) {
                            SeDetectaronParametrosBancarios = true;
                        }
                    } else {
                        NotificarQueEsNecesarioCobroDirectoMultimoneda(vCodigoMonedaExtranjera);
                    }
                } else {
                    if (vUsaCobroDirectoMultimoneda) {
                        if (EsPosibleCobroDirectoMultimoneda(vCodigoMonedaExtranjera)) {
                            SeDetectaronParametrosBancarios = true;
                        }
                    } else {
                        if (EsPosibleCobroDirecto()) {
                            SeDetectaronParametrosBancarios = true;
                        }
                    }
                }
            } catch (GalacException vEx) {
                SeDetectaronParametrosBancarios = false;
                throw vEx;
            }
            return SeDetectaronParametrosBancarios;
        }

        protected override void ExecuteCreateCommand() {                   
            BalanzaTomarPesoViewModel vBalanzaTomarPesoViewModel = null;
            bool vUsarBalanza = false;
            bool vBalanzaIsValid = InitializeBalanza(ref vBalanzaTomarPesoViewModel, ref vUsarBalanza);
            bool vUsaTotalEnDivisas = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "SeMuestraTotalEnDivisas");
            bool vUsaListaDePrecioEnMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaListaDePrecioEnMonedaExtranjera");
            RemoveColumnsIfNecesary();
            if (vBalanzaIsValid) {
                if (SeDefinieronParametrosBancariosValidos()) {
                    FacturaRapidaViewModel vViewModel = CreateNewElement(new FacturaRapida(), eAccionSR.Insertar);
                    vViewModel.InitializeViewModel(eAccionSR.Insertar);
                    if(vUsaTotalEnDivisas || vUsaListaDePrecioEnMonedaExtranjera || EmpresaUsaMonedaExtranjeraComoPredeterminada(out string vCodigoMonedaExtranjeraPredeterminada)) {
                        if(!vViewModel.AsignarTasaDeCambioDeMonedaDeCobroYParaMostrarTotales()) {
                            LibMessages.MessageBox.Information(this,"Esta usando la opción de punto de venta y no se ha ingresado la tasa de cambio del día, favor ingrese un cambio válido para continuar","Punto de Venta");
                            return;
                        }
                    }
                    if (vUsarBalanza) {
                        vViewModel.BalanzaTomarPesoViewModel = vBalanzaTomarPesoViewModel;
                        vViewModel.UsaBalanzaEnPOS = vUsarBalanza;
                    }                   
                    bool result = LibMessages.EditViewModel.ShowTopmostEditor(vViewModel);
                    if (result) {
                        SearchItems();
                    }
                } else {
                    return;
                }
            }
        }

        private bool EmpresaUsaMonedaExtranjeraComoPredeterminada(out string outCodigoMonedaExtranjera) {
            bool vResult = false;
            bool vUsaMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMonedaExtranjera");
            bool vUsaMonedaExtranjeraComoMonedaPredeterminada = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos");
            string vCodigoMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            if (vUsaMonedaExtranjera && vUsaMonedaExtranjeraComoMonedaPredeterminada && !LibString.IsNullOrEmpty(vCodigoMonedaExtranjera)) {
                vResult = true;
            }
            outCodigoMonedaExtranjera = vCodigoMonedaExtranjera;
            return vResult;
        }

        private bool EsPosibleCobroDirecto() {
            bool vResult = false;
            string vCodigoCuentaBancaria = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "CuentaBancariaCobroDirecto");
            string vConceptoBancario = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "ConceptoBancarioCobroDirecto");
            if (!LibString.IsNullOrEmpty(vCodigoCuentaBancaria) && !LibString.IsNullOrEmpty(vConceptoBancario)) {
                IFacturaRapidaPdn insFacturaRapida = new clsFacturaRapidaNav();
                string vCodigoMonedaLocal = _clsNoComun.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
                if (insFacturaRapida.EsCuentaBancariaValidaParaCobro(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), vCodigoCuentaBancaria, vCodigoMonedaLocal, out string vNombreMonedaCuentaBancaria)) {
                    vResult = true;
                } else {
                    if(!LibString.IsNullOrEmpty(vNombreMonedaCuentaBancaria)) {
                        string vNombreMonedaParametroCobroDirecto = _clsNoComun.InstanceMonedaLocalActual.NombreMoneda(LibDate.Today());
                        NotificarQueParametrosBancariosDeCobroDirectoSonIncorrectos(vNombreMonedaParametroCobroDirecto, vNombreMonedaCuentaBancaria, false);
                    } else {
                        NotificarQueFaltaDefinirParametrosBancarios();
                    }
                }
            } else {
                NotificarQueFaltaDefinirParametrosBancarios();
            }
            return vResult;
        }

        private bool EsPosibleCobroDirectoMultimoneda(string valCodigoMonedaExtranjeraPredeterminada) {
            bool vResult = false;
            string vCodigoCuentaBancaria = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CuentaBancariaCobroMultimoneda");
            string vConceptoBancario = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ConceptoBancarioCobroMultimoneda");
            if (!LibString.IsNullOrEmpty(vCodigoCuentaBancaria) && !LibString.IsNullOrEmpty(vConceptoBancario) && !LibString.IsNullOrEmpty(valCodigoMonedaExtranjeraPredeterminada)) {
                IFacturaRapidaPdn insFacturaRapida = new clsFacturaRapidaNav();
                string vNombreMonedaCuentaBancaria = string.Empty;
                if (insFacturaRapida.EsCuentaBancariaValidaParaCobro(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), vCodigoCuentaBancaria, valCodigoMonedaExtranjeraPredeterminada, out vNombreMonedaCuentaBancaria)) {
                    vResult = true;
                } else {
                    if(!LibString.IsNullOrEmpty(vNombreMonedaCuentaBancaria)) {
                        string vNombreMonedaExtrajeraParametro = ((IMonedaPdn)new clsMonedaNav()).GetNombreMoneda(valCodigoMonedaExtranjeraPredeterminada);
                        NotificarQueParametrosBancariosDeCobroDirectoSonIncorrectos(vNombreMonedaExtrajeraParametro, vNombreMonedaCuentaBancaria, true);
                    } else {
                        NotificarQueFaltaDefinirParametrosBancarios();
                    }
                }
            } else {
                NotificarQueFaltaDefinirParametrosBancarios();
            }
            return vResult;
        }

        private void NotificarQueFaltaDefinirParametrosBancarios() {
            string vMensajeParametrosNoDefinidos = "No se han definido los parametros bancarios de Cobro Directo validos, Debe configurar y asignarlos para continuar";
            LibMessages.MessageBox.Information(this, vMensajeParametrosNoDefinidos, "Punto de Venta");
        }

        private void NotificarQueParametrosBancariosDeCobroDirectoSonIncorrectos(string valNombreMonedaDeParametro, string valNombreMonedaCuentaBancaria, bool EsCobroDirectoMultimoneda) {
            StringBuilder vMensajeMonedaDeCuentaBancariaInvalida = new StringBuilder();
            clsLibSaw insLibSaw = new clsLibSaw();
            vMensajeMonedaDeCuentaBancariaInvalida.AppendLine("Los parámetros bancarios definidos son incorrectos.");
            string vNombreMonedaCuentaBancaria = insLibSaw.Plural(valNombreMonedaCuentaBancaria).ToLower();
            string vNombreMonedaDeParametro = insLibSaw.Plural(valNombreMonedaDeParametro).ToLower();
            if(EsCobroDirectoMultimoneda) {
                vMensajeMonedaDeCuentaBancariaInvalida.AppendLine($"La moneda de la cuenta bancaria seleccionada para el cobro directo multimoneda es {vNombreMonedaCuentaBancaria} y se espera que la moneda de la cuenta bancaria sea {vNombreMonedaDeParametro}.");
                vMensajeMonedaDeCuentaBancariaInvalida.AppendLine($"Debe configurar una cuenta bancaria cuya moneda sea {vNombreMonedaDeParametro}, para poder continuar.");
            } else {
                vMensajeMonedaDeCuentaBancariaInvalida.AppendLine($"La moneda de la cuenta bancaria seleccionada para el cobro directo es {vNombreMonedaCuentaBancaria} y se espera que la moneda de la cuenta bancaria sea {vNombreMonedaDeParametro}.");
                vMensajeMonedaDeCuentaBancariaInvalida.AppendLine($"Debe configurar una cuenta bancaria cuya moneda sea {vNombreMonedaDeParametro}, para poder continuar.");
            }
            LibMessages.MessageBox.Warning(this, vMensajeMonedaDeCuentaBancariaInvalida.ToString(), "Punto de Venta");
        }

        private void NotificarQueEsNecesarioCobroDirectoMultimoneda(string valCodigoMonedaExtranjeraPredeterminada) {
            StringBuilder vSolicitudDelCobroMultimoneda = new StringBuilder();
            string vNombreMonedaExtrajeraParametro = ((IMonedaPdn)new clsMonedaNav()).GetNombreMoneda(valCodigoMonedaExtranjeraPredeterminada);
            clsLibSaw insLibSaw = new clsLibSaw();
            string vNombreMonedaExtranjeraFormateada = insLibSaw.Plural(vNombreMonedaExtrajeraParametro).ToLower();
            vSolicitudDelCobroMultimoneda.Append($"Su moneda predeterminada es {vNombreMonedaExtranjeraFormateada}, ");
            vSolicitudDelCobroMultimoneda.AppendLine("por lo tanto, es necesario que active el parámetro \"cobro directo multimoneda\", para poder iniciar el punto de venta con su moneda extranjera predeterminada");
            LibMessages.MessageBox.Warning(this, vSolicitudDelCobroMultimoneda.ToString(), "Punto de Venta");
        }
    } //End of class FacturacionRapidaMngViewModel

} //End of namespace Galac.Adm.Uil.Venta

