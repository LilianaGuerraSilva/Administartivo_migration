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
using LibGalac.Aos.UI.Contracts;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.Catching;
using LibGalac.Aos.ARRpt.Reports;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Brl.GestionCompras.Reportes;
using Galac.Adm.Ccl.GestionCompras;
using System.Collections.ObjectModel;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {

    [LibMefInstallValuesMetadata(typeof(ProveedorMngViewModel))]
    public class ProveedorMngViewModel : LibMngViewModelMfc<ProveedorViewModel, Proveedor> , ILibMefInstallValues{
        #region Propiedades

        public override string ModuleName {
            get { return "Proveedor"; }
        }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public LibXmlMemInfo AppMemoryInfo { get; set; }
        */
        #endregion //Codigo Ejemplo

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

        public ProveedorMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, CodigoProveedor";
            VisibleColumns = LibGridColumModel.GetGridColumsFromType(typeof(ProveedorViewModel));
            if (LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.IsCountryPeru()) {
                if (VisibleColumns != null && VisibleColumns.Count > 0) { 
                    VisibleColumns.RemoveAt(3);
                }
            }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            OrderByDirection = "DESC";
            AppMemoryInfo = LibGlobalValues.Instance.GetAppMemInfo();
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ProveedorViewModel CreateNewElement(Proveedor valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new Proveedor();
            }
            return new ProveedorViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_Proveedor_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessComponentWithSearch<IList<Proveedor>, IList<Proveedor>> GetBusinessComponent() {
            return new clsProveedorNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsProveedorRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ReinstallCommand = new RelayCommand(ExecuteReinstallCommand, CanExecuteReinstallCommand);
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            SUPROCESOPARTICULARCommand = new RelayCommand(ExecuteSUPROCESOPARTICULARCommand, CanExecuteSUPROCESOPARTICULARCommand);
        */
        #endregion //Codigo Ejemplo
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
        //    if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
        //        RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(CreateReinstallRibbonButtonData());
        //#region Codigo Ejemplo
        ///* Codigo de Ejemplo
        //        RibbonData.TabDataCollection[0].AddTabGroupData(CreateSUPROCESOPARTICULARRibbonGroup());
        //*/
        //#endregion //Codigo Ejemplo
        //    }
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

        bool ILibMefInstallValues.InstallFromFile() {
            string vFileName = System.IO.Path.Combine(LibWorkPaths.PathOfCommonTablesForCountry(""), "Proveedor.txt");
            bool vResult = LibImportExport.InstallData(vFileName, "Proveedor", new clsProveedorImpExp(), LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
            return vResult;
        }

        internal bool InstallOrReInstallDataFromFile(eAccionSR valAction) {
            bool vResult = false;
            string vFileName = System.IO.Path.Combine(LibWorkPaths.PathOfCommonTablesForCountry(""), "Proveedor.txt");
            if (valAction == eAccionSR.Instalar) {
                vResult = LibImportExport.InstallData(vFileName, "Proveedor", new clsProveedorImpExp(), LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
            }  else {
                vResult = LibImportExport.ReInstallData(vFileName, "Proveedor", new clsProveedorImpExp(), LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
            }
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

        ANYRELATEDViewModel CreateNewElementForSUPROCESOPARTICULAR(Proveedor valModel, eAccionSR valAction) {
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
        */
        #endregion //Codigo Ejemplo


    } //End of class ProveedorMngViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

