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

        public RelayCommand ImportCommand {
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
            if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                if (VisibleColumns != null && VisibleColumns.Count > 0) {
                    VisibleColumns.RemoveAt(3);
                }
            }
            if (!LibDefGen.IsInternalSystem()) {
                VisibleColumns.RemoveAt(5); //Se oculta Ruta de Comercialización para sistemas distintos al interno
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
            ImportCommand = new RelayCommand(ExecuteImportCommand, CanExecuteImportCommand);
            //InformesCommand = new RelayCommand(ExecuteInformesCommand, CanExecuteInformesCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                //RibbonData.TabDataCollection[0].AddTabGroupData(CreateImportarRibbonGroup());
                //RibbonData.TabDataCollection[0].AddTabGroupData(CreateInformesRibbonGroup());
            }
        }
		
        private LibRibbonGroupData CreateImportarRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Importar/Exportar");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Importar/Exportar Vendedores",
                Command = ImportCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/importExport.png", UriKind.Relative),
                ToolTipDescription = "Importar/Exportar Vendedores",
                ToolTipTitle = "Importar/Exportar Vendedores"
            });
            return vResult;
        }
		
        private bool CanExecuteImportCommand() {
            bool vResult = false;
            vResult = (LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Insertar") &&
                LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Modificar"));
            return vResult;
            //return CanCreate && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Importar") && CurrentItem != null;
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

        private void ExecuteImportCommand() {
            try {
                clsVendedorImpExp vInstancia = new clsVendedorImpExp();
                VendedorImportarExportarViewModel vViewModel = new VendedorImportarExportarViewModel(ModuleName, vInstancia);
                vViewModel.InitializeViewModel(eAccionSR.Importar);
                bool result = LibMessages.EditViewModel.ShowEditor(vViewModel, true);

            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx);
            }
        }

        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            //InformesCommand.RaiseCanExecuteChanged();
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

        private void ExecuteInformesCommand() {
            try {
                //if (LibMessages.ReportsView.ShowReportsView(new clsVendedorInformesViewModel(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()))) {
                //    DialogResult = true;
                //}
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx);
            }
        }

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
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx);
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

    } //End of class VendedorMngViewModel

} //End of namespace Galac.Adm.Uil.Vendedor

