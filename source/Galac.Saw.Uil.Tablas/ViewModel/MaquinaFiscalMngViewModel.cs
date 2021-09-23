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
using Galac.Saw.Brl.Tablas;
using Galac.Saw.Brl.Tablas.Reportes;
using Galac.Saw.Ccl.Tablas;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.Uil.Tablas.ViewModel {

    public class MaquinaFiscalMngViewModel : LibMngViewModelMfc<MaquinaFiscalViewModel, MaquinaFiscal> {
        #region Propiedades

        public override string ModuleName {
            get { return "Máquina Fiscal"; }
        }

        public string ModuleNameEnSeguridad {
            get { return "Máquina Fiscal"; }
        }

        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public LibXmlMemInfo AppMemoryInfo { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores

        public MaquinaFiscalMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, ConsecutivoMaquinaFiscal";
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            OrderByDirection = "DESC";
            AppMemoryInfo = LibGlobalValues.Instance.GetAppMemInfo();
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override MaquinaFiscalViewModel CreateNewElement(MaquinaFiscal valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new MaquinaFiscal();
            }
            return new MaquinaFiscalViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_MaquinaFiscal_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessComponentWithSearch<IList<MaquinaFiscal>, IList<MaquinaFiscal>> GetBusinessComponent() {
            return new clsMaquinaFiscalNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsMaquinaFiscalRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ActivarCommand = new RelayCommand(ExecuteActivarCommand, CanExecuteActivarCommand);
            DesactivarCommand = new RelayCommand(ExecuteDesactivarCommand, CanExecuteDesactivarCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateActivarRibbonGroup());
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateDesactivarRibbonGroup());
                RibbonData.RemoveRibbonControl("Administrar", "Imprimir Lista");
            }
        }

        #endregion //Metodos Generados

        #region CanExecute
        protected override bool CanExecuteCreateCommand() {
           return LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Insertar");
        }

        protected override bool CanExecuteUpdateCommand() {
           return LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Modificar") && CurrentItem != null;
        }

        protected override bool CanExecuteDeleteCommand() {
           return LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Eliminar") && CurrentItem != null;
        }

        protected override bool CanExecuteReadCommand() {
           return LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Consultar") && CurrentItem != null;
        }

        protected override bool CanExecuteSearchCommand() {
           return LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Consultar");
        }

        protected override bool HasAccessToModule() {
           bool vResult = false;
           vResult = (LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Insertar") ||
               LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Eliminar") ||
               LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Modificar") ||
               LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Consultar"));
           return vResult;
        }
        #endregion
        #region Codigo Ejemplo
        

        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            ActivarCommand.RaiseCanExecuteChanged();
            DesactivarCommand.RaiseCanExecuteChanged();
        }

        // Activar -----------------------------

        private LibRibbonGroupData CreateActivarRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Activar");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Activar",
                Command = ActivarCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/edit.png", UriKind.Relative),
                ToolTipDescription = "Muestra el elemento actual en la lista en el editor para Activar.",
                ToolTipTitle = "Activar",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            });
            return vResult;
        }

        public RelayCommand ActivarCommand {
            get;
            private set;
        }

        private bool CanExecuteActivarCommand() {
            return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameEnSeguridad, "Activar") && CurrentItem.Status.Equals(eStatusMaquinaFiscal.Inactiva);
        }

        private void ExecuteActivarCommand() {
            try {
                MaquinaFiscalViewModel vViewModel = CreateNewElementForActivar(CurrentItem.GetModel(), eAccionSR.Activar);
                vViewModel.InitializeViewModel(eAccionSR.Activar);
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
        
        MaquinaFiscalViewModel CreateNewElementForActivar(MaquinaFiscal valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            return new MaquinaFiscalViewModel(vNewModel, valAction);
        }


        // Desactivar -----------------------------

        private LibRibbonGroupData CreateDesactivarRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Desactivar");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Desactivar",
                Command = DesactivarCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/edit.png", UriKind.Relative),
                ToolTipDescription = "Muestra el elemento actual en la lista en el editor para Desactivar.",
                ToolTipTitle = "Desactivar",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            });
            return vResult;
        }

        public RelayCommand DesactivarCommand {
            get;
            private set;
        }

        private bool CanExecuteDesactivarCommand() {
            return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameEnSeguridad, "Desactivar") && CurrentItem.Status.Equals(eStatusMaquinaFiscal.Activa);

        }

        private void ExecuteDesactivarCommand() {
            try {
                MaquinaFiscalViewModel vViewModel = CreateNewElementForDesactivar(CurrentItem.GetModel(), eAccionSR.Desactivar);
                vViewModel.InitializeViewModel(eAccionSR.Desactivar);
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

        MaquinaFiscalViewModel CreateNewElementForDesactivar(MaquinaFiscal valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            return new MaquinaFiscalViewModel(vNewModel, valAction);
        }
        #endregion //Codigo Ejemplo


    } //End of class MaquinaFiscalMngViewModel

} //End of namespace Galac.Saw.Uil.Tablas

