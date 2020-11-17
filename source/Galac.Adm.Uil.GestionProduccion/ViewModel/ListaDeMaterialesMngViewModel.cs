using System;
using System.Collections.Generic;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt.Reports;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Brl.GestionProduccion.Reportes;
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {

    public class ListaDeMaterialesMngViewModel : LibMngMasterViewModelMfc<ListaDeMaterialesViewModel, ListaDeMateriales> {
        
        #region Propiedades

        public string ModuleNameOriginal {
            get { return "Lista de Materiales"; }
        }

        public override string ModuleName {
            get {
                string vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreParaMostrarListaDeMateriales");
                if (LibString.IsNullOrEmpty(vResult)) {
                    vResult = "Lista de Materiales";
                }
                return vResult;
            }
        }

        public RelayCommand InformesCommand {
            get;
            private set;
        }

        #endregion //Propiedades

        #region Constructores e Inicializadores

        public ListaDeMaterialesMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania,FechaCreacion DESC";
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            InformesCommand = new RelayCommand(ExecuteInformesCommand, CanExecuteInformesCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateInformesRibbonGroup());
            }
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

        #endregion //Constructores

        #region Commands

        private bool CanExecuteInformesCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Informes");
        }

        protected override bool CanExecuteCreateCommand() {
            return CanCreate && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Insertar");
        }

        protected override bool CanExecuteDeleteCommand() {
            return CanDelete && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Eliminar") && CurrentItem != null;
        }

        protected override bool CanExecuteReadCommand() {
            return CanRead && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Consultar") && CurrentItem != null;
        }

        protected override bool CanExecuteUpdateCommand() {
            return CanUpdate && LibSecurityManager.CurrentUserHasAccessTo(ModuleNameOriginal, "Modificar") && CurrentItem != null;
        }

        protected override bool HasAccessToModule() {
            return LibSecurityManager.CurrentUserHasAccessToModule(ModuleNameOriginal);
        }

        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            InformesCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteInformesCommand() {
            try {
                if(LibMessages.ReportsView.ShowReportsView(new Galac.Adm.Uil.GestionProduccion.Reportes.clsListaDeMaterialesInformesViewModel(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()))) {
                    DialogResult = true;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        #endregion

        #region Metodos Generados

        protected override ListaDeMaterialesViewModel CreateNewElement(ListaDeMateriales valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new ListaDeMateriales();
            }
            return new ListaDeMaterialesViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_ListaDeMateriales_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<ListaDeMateriales>, IList<ListaDeMateriales>> GetBusinessComponent() {
            return new clsListaDeMaterialesNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsListaDeMaterialesRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        #endregion //Metodos Generados

    } //End of class ListaDeMaterialesMngViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion

