using System;
using System.Collections.Generic;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt.Reports;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Brl.GestionCompras.Reportes;
using Galac.Adm.Ccl.GestionCompras;
using System.Collections.ObjectModel;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {

    public class CompraMngViewModel : LibMngMasterViewModelMfc<CompraViewModel, Compra> {

        #region Constantes

        private const string ModuleNameOriginal = "Compra";
        private const string ModuleNameCompra = "Compra Nacional";
        private const string ModuleNameImportacion = "Importación";

        #endregion

        #region Propiedades

        eTipoCompra TipoDeCompra { get; set; }

        public override string ModuleName {
            get {
                if(TipoDeCompra == eTipoCompra.Importacion) {
                    return ModuleNameImportacion;
                }
                return ModuleNameCompra;
            }
        }

        public RelayCommand<eAccionSR?> AnularReAbrirCommand {
            get;
            private set;
        }

        public RelayCommand InformesCommand {
            get;
            private set;
        }

        public RelayCommand ReImprimirCommand {
            get;
            private set;
        }

        public RelayCommand InsertarDesdeOrdendeCompraCommand {
            get;
            private set;
        }

        public new ObservableCollection<LibGridColumModel> VisibleColumns {
            get;
            private set;
        }

        #endregion //Propiedades

        #region Constructores e Inicializadores

        public CompraMngViewModel(eTipoCompra initTipoDeCompra)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            TipoDeCompra = initTipoDeCompra;
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Consecutivo";
            if(LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                RemoveColumnByDisplayMemberPath("Serie");
            }

            VisibleColumns = LibGridColumModel.GetGridColumsFromType(typeof(CompraViewModel));
            if(LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                VisibleColumns.RemoveAt(0);
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            AnularReAbrirCommand = new RelayCommand<eAccionSR?>(ExecuteAnularReAbrirCommand, CanExecuteAnularReAbrirCommand);
            InformesCommand = new RelayCommand(ExecuteInformesCommand, CanExecuteInformesCommand);
            ReImprimirCommand = new RelayCommand(ExecuteReImprimirCommand, CanExecuteReImprimirCommand);
            InsertarDesdeOrdendeCompraCommand = new RelayCommand(ExecuteInsertarDesdeOrdendeCompraCommand, CanExecuteInsertarDesdeOrdendeCompraCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateAnularReAbrirRibbonGroup());
            }
        }

        #endregion //Constructores

        #region Comandos

        private LibRibbonGroupData CreateAnularReAbrirRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Especial");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Abrir",
                Command = AnularReAbrirCommand,
                CommandParameter = eAccionSR.Abrir,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/specializedUpdate.png", UriKind.Relative),
                ToolTipDescription = "Abrir",
                ToolTipTitle = "Compra",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Anular",
                Command = AnularReAbrirCommand,
                CommandParameter = eAccionSR.Anular,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/specializedUpdate.png", UriKind.Relative),
                ToolTipDescription = "Anular",
                ToolTipTitle = "Compra",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Informes",
                Command = InformesCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/report.png", UriKind.Relative),
                ToolTipDescription = "Informes",
                ToolTipTitle = "Informes"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "ReImprimir",
                Command = ReImprimirCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/print.png", UriKind.Relative),
                ToolTipDescription = "ReImprimir",
                ToolTipTitle = "Compra"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Insertar desde Orden de Compra",
                Command = InsertarDesdeOrdendeCompraCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/specializedUpdate.png", UriKind.Relative),
                ToolTipDescription = "Insertar desde Orden de Compra",
                ToolTipTitle = "Compra",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            });
            return vResult;
        }

        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            AnularReAbrirCommand.RaiseCanExecuteChanged();
            ReImprimirCommand.RaiseCanExecuteChanged();
            InsertarDesdeOrdendeCompraCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteAnularReAbrirCommand(eAccionSR? valAction) {
            try {
                CompraCambiarStatusViewModel vViewModel = CreateNewElementParacambioStatus(CurrentItem.GetModel(), valAction.Value);
                vViewModel.TipoModulo = TipoDeCompra;
                vViewModel.InitializeViewModel(valAction.Value);
                bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
                if(result) {
                    SearchItems();
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteInformesCommand() {
            try {
                if(LibMessages.ReportsView.ShowReportsView(new Galac.Adm.Uil.GestionCompras.Reportes.clsCompraInformesViewModel(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()))) {
                    DialogResult = true;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteReImprimirCommand() {
            try {
                CompraViewModel vViewModel = CreateNewElement(CurrentItem.GetModel(), eAccionSR.ReImprimir);
                vViewModel.InitializeViewModel(eAccionSR.ReImprimir);
                LibMessages.EditViewModel.ShowEditor(vViewModel);

            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        protected override void ExecuteUpdateCommand() {
            if(CurrentItem.StatusCompra == eStatusCompra.Anulada) {
                LibMessages.MessageBox.Alert(this, "No se puede modificar una compra con estatus anulada", ModuleName);
            } else {
                base.ExecuteUpdateCommand();
            }
        }

        protected override void ExecuteDeleteCommand() {
            if(CurrentItem.StatusCompra == eStatusCompra.Anulada) {
                LibMessages.MessageBox.Alert(this, "No se puede eliminar una compra con estatus anulada", ModuleName);
            } else {
                base.ExecuteDeleteCommand();
            }
        }

        private void ExecuteInsertarDesdeOrdendeCompraCommand() {
            try {
                CompraViewModel vViewModel = CreateNewElementDesdeOrdenDeCompra(new Compra(), eAccionSR.Insertar);
                vViewModel.InitializeViewModel(eAccionSR.Insertar);
                bool result = ShowEditor(vViewModel);
                if(result) {
                    SearchItems();
                }

            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        protected override void ExecuteCreateCommand() {
            CompraViewModel vViewModel = CreateNewElement(new Compra(), eAccionSR.Insertar);
            string vCodigoMonedaExtranjera = vViewModel.TipoModulo == eTipoCompra.Importacion ? "USD" : LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            bool vUsaDivisaComoMonedaPrincipalDeIngresoDeDatos = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos"));
            if(!vUsaDivisaComoMonedaPrincipalDeIngresoDeDatos) {
                base.ExecuteCreateCommand();
            } else if((vUsaDivisaComoMonedaPrincipalDeIngresoDeDatos || vViewModel.TipoModulo == eTipoCompra.Importacion) && vViewModel.AsignaTasaDelDia(vCodigoMonedaExtranjera)) {
                base.ExecuteCreateCommand();
            } else {
                LibMessages.MessageBox.Information(this, "No se puede continuar sin establecer la Tasa de Cambio del día.", "Debe insertar un de Cambio válido.");
            }
        }

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

        private bool CanExecuteAnularReAbrirCommand(eAccionSR? valAction) {
            if(valAction == eAccionSR.Anular) {
                return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Anular") && CurrentItem.StatusCompra == eStatusCompra.Vigente;
            } else if(valAction == eAccionSR.Abrir) {
                return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Anular") && CurrentItem.StatusCompra == eStatusCompra.Anulada;
            }
            return false;
        }

        private bool CanExecuteInformesCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Informes");
        }

        private bool CanExecuteReImprimirCommand() {
            return CurrentItem != null;
        }

        private bool CanExecuteInsertarDesdeOrdendeCompraCommand() {
            return CanExecuteCreateCommand();
        }

        #endregion //Comandos

        #region Metodos

        protected override CompraViewModel CreateNewElement(Compra valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if(vNewModel == null) {
                vNewModel = new Compra();
            }
            CompraViewModel vViewModel = new CompraViewModel(vNewModel, valAction);
            vViewModel.TipoModulo = TipoDeCompra;
            vViewModel.VieneDeOrdenDeCompra = false;
            return vViewModel;
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            LibSearchCriteria vCriteria = LibSearchCriteria.CreateCriteria("Gv_Compra_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
            vCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_Compra_B1.TipoDeCompra", LibConvert.EnumToDbValue((int)TipoDeCompra)), eLogicOperatorType.And);
            return vCriteria;
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<Compra>, IList<Compra>> GetBusinessComponent() {
            return new clsCompraNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsCompraRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override bool HasAccessToModule() {
            return LibSecurityManager.CurrentUserHasAccessToModule(ModuleNameOriginal);
        }

        private CompraCambiarStatusViewModel CreateNewElementParacambioStatus(Compra valCompra, eAccionSR valAccion) {
            CompraCambiarStatusViewModel vViewModel = new CompraCambiarStatusViewModel(valCompra, valAccion);
            return vViewModel;
        }

        CompraViewModel CreateNewElementDesdeOrdenDeCompra(Compra valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if(vNewModel == null) {
                vNewModel = new Compra();
            }
            CompraViewModel vViewModel = new CompraViewModel(vNewModel, valAction);
            vViewModel.TipoModulo = TipoDeCompra;
            vViewModel.VieneDeOrdenDeCompra = true;
            return vViewModel;
        }

        #endregion //Metodos

    } //End of class CompraMngViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

