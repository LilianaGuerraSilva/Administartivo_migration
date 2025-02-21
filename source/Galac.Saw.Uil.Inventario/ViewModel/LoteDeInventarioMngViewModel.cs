using System;
using System.Collections.Generic;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt.Reports;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Brl.Inventario.Reportes;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Uil.Inventario.ViewModel {

    public class LoteDeInventarioMngViewModel : LibMngMasterViewModelMfc<LoteDeInventarioViewModel, LoteDeInventario> {
        #region Propiedades

        public override string ModuleName {
            get { return "Lote de Inventario"; }
        }

        public RelayCommand InformesCommand {
            get;
            private set;
        }

        #endregion //Propiedades
        #region Constructores

        public LoteDeInventarioMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, FechaDeVencimiento, FechaDeElaboracion, Consecutivo";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override LoteDeInventarioViewModel CreateNewElement(LoteDeInventario valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new LoteDeInventario();
            }
            return new LoteDeInventarioViewModel(vNewModel, valAction);
        }

        private LoteDeInventarioInsertarViewModel CreateNewElementInsertar(LoteDeInventario valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new LoteDeInventario();
            }
            return new LoteDeInventarioInsertarViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_LoteDeInventario_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<LoteDeInventario>, IList<LoteDeInventario>> GetBusinessComponent() {
            return new clsLoteDeInventarioNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsLoteDeInventarioRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override bool HasAccessToModule() {
            return LibSecurityManager.CurrentUserHasAccessToModule(ModuleName.Substring(0, 18));
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            InformesCommand = new RelayCommand(ExecuteInformesCommand, CanExecuteInformesCommand);
        }



        protected override void InitializeRibbon() {
            CanPrint = true;
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.RemoveRibbonControl("Administrar", "Insertar");
                RibbonData.TabDataCollection[0].GroupDataCollection[1].AddRibbonControlData(CreateRibbonControlImprimirLista());
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateEspecialRibbonGroup());
            }
        }

        private LibRibbonControlData CreateRibbonControlImprimirLista() {
            return new LibRibbonButtonData() {
                Label = "Imprimir Lista",
                Command = PrintCommand,
                CommandParameter = eAccionSR.Imprimir,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/printList.png", UriKind.Relative),
                ToolTipDescription = "Imprimir Lista.",
                ToolTipTitle = "Imprimir Lista",
                IsVisible = true
            };
        }

        private LibRibbonGroupData CreateEspecialRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Especial");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Informes",
                Command = InformesCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/report.png", UriKind.Relative),
                ToolTipDescription = "Informes",
                ToolTipTitle = "Informes"
            });
            return vResult;
        }


        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
        }
        #endregion //Metodos Generados     

        internal void ExecuteCreateCommandEspecial(ref string initCodigoLote, string initCodigoArticulo, eTipoArticuloInv initTipoArticuloInv) {
            try {
                LoteDeInventarioInsertarViewModel vViewModel = CreateNewElementInsertar(default(LoteDeInventario), eAccionSR.Insertar);
                vViewModel.InitializeViewModel(eAccionSR.Insertar);
                vViewModel.Consecutivo = (new LibGalac.Aos.Dal.LibDatabase()).NextLngConsecutive("Saw.LoteDeInventario", "Consecutivo", "ConsecutivoCompania = " + Mfc.GetInt("Compania").ToString());
                vViewModel.CodigoLote = initCodigoLote;
                vViewModel.CodigoArticulo = initCodigoArticulo;
                vViewModel.TipoArticuloInv = initTipoArticuloInv;
                bool vResult = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                initCodigoLote = vViewModel.ReturnCodigoLote;
            } catch (Exception) {
                throw;
            }
        }

        protected override bool CanExecuteUpdateCommand() {
            return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleName.Substring(0, 18), "Modificar");
        }

        protected override bool CanExecuteReadCommand() {
            return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleName.Substring(0, 18), "Consultar");
        }

        protected override bool CanExecuteDeleteCommand() {
            return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleName.Substring(0, 18), "Eliminar");
        }

        private bool CanExecuteInformesCommand() {
            return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleName.Substring(0, 18), "Informes");
        }

        private void ExecuteInformesCommand() {
            try {
                if (LibMessages.ReportsView.ShowReportsView(new Galac.Saw.Uil.Inventario.Reportes.clsLoteDeInventarioInformesViewModel(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()))) {
                    DialogResult = true;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
    } //End of class LoteDeInventarioMngViewModel
} //End of namespace Galac.Saw.Uil.Inventario