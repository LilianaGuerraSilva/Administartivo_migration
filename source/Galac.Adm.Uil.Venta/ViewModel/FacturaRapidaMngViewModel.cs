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

namespace Galac.Adm.Uil.Venta.ViewModel {

    public class FacturaRapidaMngViewModel : LibMngMasterViewModelMfc<FacturaRapidaViewModel, FacturaRapida> {
        #region Variables
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

        private bool SeDefinieronParametrosBancarios() {
            bool SeDetectaronParametrosBancarios = false;
            SeDetectaronParametrosBancarios = false;
            try {
                string vCodigoCuentaBancaria = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "CuentaBancariaCobroDirecto");
                string vConceptoBancario = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "ConceptoBancarioCobroDirecto");
                if (!LibString.IsNullOrEmpty(vCodigoCuentaBancaria) && !LibString.IsNullOrEmpty(vConceptoBancario)) {
                    SeDetectaronParametrosBancarios = true;
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
            bool parametrosBancarios = SeDefinieronParametrosBancarios();
            bool vBalanzaIsValid = InitializeBalanza(ref vBalanzaTomarPesoViewModel, ref vUsarBalanza);
            RemoveColumnsIfNecesary();
            if (vBalanzaIsValid) {
                if (parametrosBancarios) {
                    FacturaRapidaViewModel vViewModel = CreateNewElement(new FacturaRapida(), eAccionSR.Insertar);
                    vViewModel.InitializeViewModel(eAccionSR.Insertar);
                    if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "SeMuestraTotalEnDivisas")
                        || LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaListaDePrecioEnMonedaExtranjera")) {
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
                    LibMessages.MessageBox.Information(this, "No se han definido los parametros bancarios de Cobro Directo, Debe configurar y asignarlos para continuar", "");
                }
            }
        }
    } //End of class FacturacionRapidaMngViewModel

} //End of namespace Galac.Adm.Uil.Venta

