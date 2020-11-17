using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.Catching;
using LibGalac.Aos.ARRpt.Reports;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Brl.GestionCompras.Reportes;
using Galac.Adm.Ccl.GestionCompras;
using System.Collections.ObjectModel;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {

    public class OrdenDeCompraMngViewModel : LibMngMasterViewModelMfc<OrdenDeCompraViewModel, OrdenDeCompra> {
        #region Constantes
        const string ModuleNameOriginal = "Orden De Compra";
        const string ModuleNameNacional = "Orden De Compra Nacional";
        const string ModuleNameImportacion = "Orden De Compra Importación";
        #endregion
        #region Propiedades
        eTipoCompra TipoDeCompra { get; set; }

        public override string ModuleName {
            get {
                if (TipoDeCompra == eTipoCompra.Importacion) {
                    return ModuleNameImportacion;
                }
                return ModuleNameNacional;
            }
        }
        public RelayCommand<eAccionSR?> AnularReAbrirCommand {
            get;
            private set;
        }

        public RelayCommand ReImprimirCommand {
            get;
            private set;
        }

        public new ObservableCollection<LibGridColumModel> VisibleColumns {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores

        public OrdenDeCompraMngViewModel(eTipoCompra initTipoDeCompra)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            TipoDeCompra = initTipoDeCompra;
            Title = "Buscar " + ModuleName;            
            OrderByMember = "ConsecutivoCompania, Consecutivo";                    
            VisibleColumns = LibGridColumModel.GetGridColumsFromType(typeof(OrdenDeCompraViewModel));           
            if (LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                var vItem = VisibleColumns.Where(P => P.Header == "Serie").FirstOrDefault();
                if(vItem != null) {
                    VisibleColumns.Remove(vItem);
                }
            }
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
                OrderByDirection = "DESC";
            */
            #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override OrdenDeCompraViewModel CreateNewElement(OrdenDeCompra valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new OrdenDeCompra();
            }
           
			OrdenDeCompraViewModel vViewModel = new OrdenDeCompraViewModel(vNewModel, valAction);
            vViewModel.TipoModulo = TipoDeCompra;
            return vViewModel;
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            LibSearchCriteria vCriteria = LibSearchCriteria.CreateCriteria("Gv_OrdenDeCompra_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
            vCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_OrdenDeCompra_B1.TipoDeCompra", LibConvert.EnumToDbValue((int)TipoDeCompra)), eLogicOperatorType.And);
            return vCriteria;
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<OrdenDeCompra>, IList<OrdenDeCompra>> GetBusinessComponent() {
            return new clsOrdenDeCompraNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return null;
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            AnularReAbrirCommand = new RelayCommand<eAccionSR?>(ExecuteAnularReAbrirCommand, CanExecuteAnularReAbrirCommand);
            ReImprimirCommand = new RelayCommand(ExecuteReImprimirCommand, CanExecuteReImprimirCommand);
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
                SUPROCESOPARTICULARCommand = new RelayCommand(ExecuteSUPROCESOPARTICULARCommand, CanExecuteSUPROCESOPARTICULARCommand);
            */
            #endregion //Codigo Ejemplo
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateAnularReAbrirRibbonGroup());
                #region Codigo Ejemplo
                /* Codigo de Ejemplo
                        RibbonData.TabDataCollection[0].AddTabGroupData(CreateSUPROCESOPARTICULARRibbonGroup());
                */
                #endregion //Codigo Ejemplo
            }
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
        //para cambiar el mecanismo de activacion de los botones de operaciones CRUD, debes sobreescribirla y ajustarla las necesidades de tu negocio:

        protected override bool CanExecuteCreateCommand() {
            return CanCreate;
        }

        protected override bool CanExecuteUpdateCommand() {
            return CanUpdate && CurrentItem != null;
        }

        protected override bool CanExecuteDeleteCommand() {
            return CanDelete && CurrentItem != null;
        }

        protected override bool CanExecuteReadCommand() {
            return CanRead && CurrentItem != null;
        }
        //para agregar una nueva accion en el Ribbon, debes agregar este conjunto de métodos (6 en total) y modificar las inicializaciones.
        //Por favor recuerda autodocumentar, el codigo es de ejemplo para que te sirva de guía, no código final:

        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            SUPROCESOPARTICULARCommand.RaiseCanExecuteChanged();
        }

        ANYRELATEDViewModel CreateNewElementForSUPROCESOPARTICULAR(OrdenDeCompra valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            return new ANYRELATEDViewModel(vNewModel, valAction);
        }

        public RelayCommand SUPROCESOPARTICULARCommand {
            get;
            private set;
        }

        private LibRibbonGroupData CreateSUPROCESOPARTICULARRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("SU PROCESO PARTICULAR");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "LO QUE SE LEE EN EL RIBBON",
                Command = SUPROCESOPARTICULARCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/edit.png", UriKind.Relative),
                ToolTipDescription = "LO QUE SE LEE AL PASAR EL MOUSE SOBRE EL NUEVO BOTON.",
                ToolTipTitle = "TITULO PARA EL TOOLTIP"
            });
            return vResult;
        }

        private bool CanExecuteSUPROCESOPARTICULARCommand() {
            return CurrentItem != null
                && LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Su nivel de permiso asociado");
        }

        private void ExecuteSUPROCESOPARTICULARCommand() {
            try {
                ANYRELATEDViewModel vViewModel = CreateNewElementForSUPROCESOPARTICULAR(CurrentItem.GetModel(), eAccionSR.Cerrar);
                vViewModel.InitializeViewModel(eAccionSR.SUACCION);
                LibMessages.EditViewModel.ShowEditor(vViewModel);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
        */
        #endregion //Codigo Ejemplo

        protected override bool CanExecuteCreateCommand() {
            return CanCreate && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Insertar");            
        }

        protected override bool CanExecuteDeleteCommand() {
            return CanDelete && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Eliminar") && CurrentItem != null;
        }

        protected override bool CanExecuteUpdateCommand() {
            return CanUpdate && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Modificar") && CurrentItem != null;
        }

        protected override bool CanExecuteReadCommand() {
            return CanRead && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Consultar") && CurrentItem != null;
        }

        protected override bool HasAccessToModule() {
            return LibSecurityManager.CurrentUserHasAccessToModule(ModuleNameOriginal);
        }



        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            AnularReAbrirCommand.RaiseCanExecuteChanged();
            ReImprimirCommand.RaiseCanExecuteChanged();
        }



        private LibRibbonGroupData CreateAnularReAbrirRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Especial");
            //vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
            //    Label = "Abrir",
            //    Command = AnularReAbrirCommand,
            //    CommandParameter = eAccionSR.Abrir,   
            //    LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/specializedUpdate.png", UriKind.Relative),
            //    ToolTipDescription = "Abrir",
            //    ToolTipTitle = "Orden De Compra"
            //});
            //vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
            //    Label = "Anular",
            //    Command = AnularReAbrirCommand,
            //    CommandParameter = eAccionSR.Anular,
            //    LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/specializedUpdate.png", UriKind.Relative),
            //    ToolTipDescription = "Anular",
            //    ToolTipTitle = "Orden De Compra"
            //});

            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "ReImprimir",
                Command = ReImprimirCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/print.png", UriKind.Relative),
                ToolTipDescription = "ReImprimir",
                ToolTipTitle = "Compra"
            });
            return vResult;
        }

        private bool CanExecuteAnularReAbrirCommand(eAccionSR? valAction) {
            if (valAction == eAccionSR.Anular) {
                return CurrentItem != null && CurrentItem.StatusOrdenDeCompra == eStatusCompra.Vigente;
            } else if (valAction == eAccionSR.Abrir) {
                return CurrentItem != null && CurrentItem.StatusOrdenDeCompra == eStatusCompra.Anulada;
            }
            return false;
        }



        private void ExecuteAnularReAbrirCommand(eAccionSR? valAction) {
            try {
                CompraCambiarStatusViewModel vViewModel = CreateNewElementParacambioStatus(CurrentItem.GetModel(), valAction.Value);
                vViewModel.TipoModulo = TipoDeCompra;
                vViewModel.InitializeViewModel(valAction.Value);
                bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
                if (result) {
                    SearchItems();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private CompraCambiarStatusViewModel CreateNewElementParacambioStatus(OrdenDeCompra valCompra, eAccionSR valAccion) {
           return null;
        }

        private bool CanExecuteReImprimirCommand() {
            return CurrentItem != null;
        }



        private void ExecuteReImprimirCommand() {
            try {
                OrdenDeCompraViewModel vViewMode = CreateNewElement(CurrentItem.GetModel(), eAccionSR.ReImprimir);
                vViewMode.InitializeViewModel(eAccionSR.ReImprimir);
                LibMessages.EditViewModel.ShowEditor(vViewMode);

            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        protected override void ExecuteUpdateCommand() {
            if (CurrentItem.StatusOrdenDeCompra == eStatusCompra.Anulada) {
                LibMessages.MessageBox.Alert(this, "No se puede modificar una Orden de compra con estatus anulada", ModuleName);
            } else {
                base.ExecuteUpdateCommand();
            }
        }

        protected override void ExecuteDeleteCommand() {
            if (CurrentItem.StatusOrdenDeCompra == eStatusCompra.Anulada) {
                LibMessages.MessageBox.Alert(this, "No se puede eliminar una Orden de compra con estatus anulada", ModuleName);
            } else {
                base.ExecuteDeleteCommand();
            }
        }

    } //End of class CompraMngViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

