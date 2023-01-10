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
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Brl.GestionProduccion.Reportes;
using Galac.Adm.Ccl.GestionProduccion;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {

    public class OrdenDeProduccionMngViewModel : LibMngMasterViewModelMfc<OrdenDeProduccionViewModel, OrdenDeProduccion> {

        #region Propiedades

        public override string ModuleName {
            get { return "Orden de Producción"; }
        }

        public RelayCommand InformesCommand {
            get;
            private set;
        }

        public RelayCommand IniciarCommand {
            get;
            private set;
        }

        public RelayCommand AnularCommand {
            get;
            private set;
        }

        public RelayCommand CerrarCommand {
            get;
            private set;
        }

        #endregion //Propiedades

        #region Constructores e Inicializadores

        public OrdenDeProduccionMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Consecutivo";
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            InformesCommand = new RelayCommand(ExecuteInformesCommand, CanExecuteInformesCommand);
            IniciarCommand = new RelayCommand(ExecuteIniciarCommand, CanExecuteIniciarCommand);
            AnularCommand = new RelayCommand(ExecuteAnularCommand, CanExecuteAnularCommand);
            CerrarCommand = new RelayCommand(ExecuteCerrarCommand, CanExecuteCerrarCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateInformesRibbonGroup());
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateAccionesEspecialesRibbonGroup());
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

        private LibRibbonGroupData CreateAccionesEspecialesRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Especiales");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Iniciar",
                Command = IniciarCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/report.png", UriKind.Relative),
                ToolTipDescription = "Iniciar",
                ToolTipTitle = "Iniciar",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Anular",
                Command = AnularCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/report.png", UriKind.Relative),
                ToolTipDescription = "Anular",
                ToolTipTitle = "Anular",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Cerrar",
                Command = CerrarCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/report.png", UriKind.Relative),
                ToolTipDescription = "Cerrar",
                ToolTipTitle = "Cerrar",
                IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
            });
            return vResult;
        }

        #endregion //Constructores e Inicializadores

        #region Commands

        private bool CanExecuteIniciarCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Iniciar") && CurrentItem != null && CurrentItem.StatusOp == eTipoStatusOrdenProduccion.Ingresada;
        }

        private bool CanExecuteCerrarCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Cerrar") && CurrentItem != null && CurrentItem.StatusOp == eTipoStatusOrdenProduccion.Iniciada;
        }

        private bool CanExecuteAnularCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Anular") && CurrentItem != null && CurrentItem.StatusOp == eTipoStatusOrdenProduccion.Iniciada;
        }

        private bool CanExecuteInformesCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Orden de Producción", "Informes");
        }

        protected override bool CanExecuteUpdateCommand() {
            return base.CanExecuteUpdateCommand() && CurrentItem != null && CurrentItem.StatusOp == eTipoStatusOrdenProduccion.Ingresada;
        }

        protected override bool CanExecuteDeleteCommand() {
            return base.CanExecuteDeleteCommand() && CurrentItem != null && CurrentItem.StatusOp == eTipoStatusOrdenProduccion.Ingresada;
        }

        private void ExecuteInformesCommand() {
            try {
                if (LibMessages.ReportsView.ShowReportsView(new Galac.Adm.Uil.GestionProduccion.Reportes.clsOrdenDeProduccionInformesViewModel(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()))) {
                    DialogResult = true;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteIniciarCommand() {
            try {
                OrdenDeProduccionViewModel vViewModel = new OrdenDeProduccionViewModel(CurrentItem.GetModel(), eAccionSR.Custom);
                if (vViewModel.UsaMonedaExtranjera() && !((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(vViewModel.CodigoMonedaCostoProduccion, LibDate.Today(), out decimal vTasa) && !LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Ingresar Cambio del Día")) {
                    LibMessages.MessageBox.Alert(this, "Usted no posee permisos para ingresar tasa de cambio del día, por favor diríjase a su supervisor o ingrese al sistema con otro usuario y vuelva a intentar.", ModuleName);
                } else {
                    vViewModel.InitializeViewModel(eAccionSR.Custom);
                    bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
                    if (result) {
                        SearchItems();
                    }
                }

            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteAnularCommand() {
            try {
                OrdenDeProduccionViewModel vViewModel = new OrdenDeProduccionViewModel(CurrentItem.GetModel(), eAccionSR.Anular);
                vViewModel.InitializeViewModel(eAccionSR.Anular);
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

        private void ExecuteCerrarCommand() {
            try {
                OrdenDeProduccionViewModel vViewModel = new OrdenDeProduccionViewModel(CurrentItem.GetModel(), eAccionSR.Cerrar);
                if (vViewModel.UsaMonedaExtranjera() && !((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(vViewModel.CodigoMonedaCostoProduccion, LibDate.Today(), out decimal vTasa) && !LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Ingresar Cambio del Día")) {
                    LibMessages.MessageBox.Alert(this, "Usted no posee permisos para ingresar tasa de cambio del día, por favor diríjase a su supervisor o ingrese al sistema con otro usuario y vuelva a intentar.", ModuleName);
                } else {
                    vViewModel.InitializeViewModel(eAccionSR.Cerrar);
                    bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
                    if (result) {
                        SearchItems();
                    }
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
        #endregion //Commands

        #region Metodos Generados

        protected override OrdenDeProduccionViewModel CreateNewElement(OrdenDeProduccion valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new OrdenDeProduccion();
            }
            return new OrdenDeProduccionViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_OrdenDeProduccion_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>> GetBusinessComponent() {
            return new clsOrdenDeProduccionNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsOrdenDeProduccionRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        #endregion //Metodos Generados

        #region Metodos

        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            InformesCommand.RaiseCanExecuteChanged();
            IniciarCommand.RaiseCanExecuteChanged();
            AnularCommand.RaiseCanExecuteChanged();
            CerrarCommand.RaiseCanExecuteChanged();
        }

        protected override void ExecuteCreateCommand() {
            OrdenDeProduccionViewModel vViewModel = new OrdenDeProduccionViewModel(CurrentItem.GetModel(), eAccionSR.Insertar);
            if (vViewModel.UsaMonedaExtranjera() && !((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(vViewModel.AsignarCodigoDeLaMonedaAlInsertar(), LibDate.Today(), out decimal vTasa) && !LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Ingresar Cambio del Día")) {
                LibMessages.MessageBox.Alert(this, "Usted no posee permisos para ingresar tasa de cambio del día, por favor diríjase a su supervisor o ingrese al sistema con otro usuario y vuelva a intentar.", ModuleName);
            } else {
                base.ExecuteCreateCommand();
            }
        }

        protected override void ExecuteUpdateCommand() {
            OrdenDeProduccionViewModel vViewModel = new OrdenDeProduccionViewModel(CurrentItem.GetModel(), eAccionSR.Modificar);
            if (vViewModel.UsaMonedaExtranjera() && !((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(vViewModel.CodigoMonedaCostoProduccion, LibDate.Today(), out decimal vTasa) && !LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Ingresar Cambio del Día")) {
                LibMessages.MessageBox.Alert(this, "Usted no posee permisos para ingresar tasa de cambio del día, por favor diríjase a su supervisor o ingrese al sistema con otro usuario y vuelva a intentar.", ModuleName);
            } else {
                base.ExecuteUpdateCommand();
            }
        }

        private void MostrarMensajeParaUsuariosSinPermisosParaInsertarTasaDeCambio() {
            LibMessages.MessageBox.Alert(this, "Usted no posee permisos para ingresar la Tasa de Cambio del día. \nPor favor diríjase a su Supervisor o ingrese al sistema con otro usuario y vuelva a intentarlo.", ModuleName);
        }
        #endregion //Metodos

    } //End of class OrdenDeProduccionMngViewModel

} //End of namespace Galac.Adm.Uil. GestionProduccion

