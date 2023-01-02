using System;
using System.Collections.Generic;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Brl.Vendedor;
using LibGalac.Aos.ARRpt.Reports;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.UI.Wpf;
using System.Collections.ObjectModel;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Uil.Vendedor.ViewModel {

    [LibMefInstallValuesMetadata(typeof(VendedorMngViewModel))]
    public class VendedorMngViewModel : LibMngMasterViewModelMfc<VendedorViewModel, Ccl.Vendedor.Vendedor>, ILibMefInstallValues {
        #region Propiedades

        public override string ModuleName {
            get { return "Vendedor"; }
        }

        public RelayCommand InformesCommand {
            get;
            private set;
        }

        public new ObservableCollection<LibGridColumModel> VisibleColumns {
            get;
            private set;
        }

        public RelayCommand ReinstallCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores

        public VendedorMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Consecutivo";
            VisibleColumns = LibGridColumModel.GetGridColumsFromType(typeof(VendedorViewModel));
            if (LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.IsCountryPeru()) {
                if (VisibleColumns != null && VisibleColumns.Count > 0) {
                    VisibleColumns.RemoveAt(3);
                }
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override VendedorViewModel CreateNewElement(Ccl.Vendedor.Vendedor valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new Ccl.Vendedor.Vendedor();
            }
            return new VendedorViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_Vendedor_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<Ccl.Vendedor.Vendedor>, IList<Ccl.Vendedor.Vendedor>> GetBusinessComponent() {
            return new clsVendedorNav();
        }

        //protected override ILibReportInfo GetDataRetrievesInstance() {
        //    return new clsVendedorRpt();
        //}

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ReinstallCommand = new RelayCommand(ExecuteReinstallCommand, CanExecuteReinstallCommand);
            //InformesCommand = new RelayCommand(ExecuteInformesCommand, CanExecuteInformesCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateInformesRibbonGroup());
            }
        }

        private LibRibbonButtonData CreateReinstallRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Reinstalar",
                Command = ReinstallCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/add.png", UriKind.Relative),
                ToolTipDescription = "Reinstalar datos",
                ToolTipTitle = "Reinstalar",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            };
        }

        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            InformesCommand.RaiseCanExecuteChanged();
        }

        private LibRibbonGroupData CreateInformesRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Informes",
                Command = InformesCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/report.png", UriKind.Relative),
                ToolTipDescription = "Informes",
                ToolTipTitle = "Informes"
            });
            return vResult;
        }

        //private void ExecuteInformesCommand() {
        //    try {
        //        if (LibMessages.ReportsView.ShowReportsView(new clsVendedorInformesViewModel(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()))) {
        //            DialogResult = true;
        //        }
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
        //    }
        //}

        private bool CanExecuteInformesCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Informes");
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            throw new NotImplementedException();
        }

        private bool CanExecuteReinstallCommand() {
            return HasAccessToModule();
        }

        private void ExecuteReinstallCommand() {
            try {
                InstallOrReInstallDataFromFile(eAccionSR.ReInstalar);
                LibMessages.RefreshList.Send(ModuleName);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        internal bool InstallOrReInstallDataFromFile(eAccionSR valAction) {
            bool vResult = false;
            string vFileName = System.IO.Path.Combine(LibWorkPaths.PathOfCommonTablesForCountry(""), "Vendedor.txt");
            if (valAction == eAccionSR.Instalar) {
                vResult = LibImportExport.InstallData(vFileName, "Vendedor", new clsVendedorImpExp(), LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
            } else {
                vResult = LibImportExport.ReInstallData(vFileName, "Vendedor", new clsVendedorImpExp(), LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
            }
            return vResult;
        }
        
        public bool InstallFromFile() {
            string vFileName = System.IO.Path.Combine(LibWorkPaths.PathOfCommonTablesForCountry(""), "Vendedor.txt");
            bool vResult = LibImportExport.InstallData(vFileName, "Vendedor", new clsVendedorImpExp(), LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
            return vResult;
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

        ANYRELATEDViewModel CreateNewElementForSUPROCESOPARTICULAR(Vendedor valModel, eAccionSR valAction) {
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

    } //End of class VendedorMngViewModel

} //End of namespace Galac.Adm.Uil.Vendedor

