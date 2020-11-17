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
using Galac.Saw.Uil.Inventario.Reportes;

namespace Galac.Saw.Uil.Inventario.ViewModel {

    public class ArticuloInventarioMngViewModel : LibMngMasterViewModelMfc<ArticuloInventarioViewModel, ArticuloInventario> {
        #region Propiedades
        public override string ModuleName {
            get { return "Artículo Inventario"; }
        }

        public RelayCommand InformesCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public ArticuloInventarioMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Codigo";
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override ArticuloInventarioViewModel CreateNewElement(ArticuloInventario valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new ArticuloInventario();
            }
            return new ArticuloInventarioViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("ArticuloInventario.CodigoDelArticulo", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<ArticuloInventario>, IList<ArticuloInventario>> GetBusinessComponent() {
            return new clsArticuloInventarioNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsArticuloInventarioRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            InformesCommand = new RelayCommand(ExecuteInformesCommand, CanExecuteInformesCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateInformesRibbonGroup());
            }
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

        private void ExecuteInformesCommand() {
            try {
                if (LibMessages.ReportsView.ShowReportsView(new clsArticuloInventarioInformesViewModel(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()))) {
                    DialogResult = true;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private bool CanExecuteInformesCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Informes");
        }
        #endregion //Metodos Generados
    } //End of class ArticuloInventarioMngViewModel

} //End of namespace Galac.Saw.Uil.Inventario

