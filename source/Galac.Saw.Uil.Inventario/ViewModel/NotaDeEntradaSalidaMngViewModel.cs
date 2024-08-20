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
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Brl.Inventario.Reportes;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Uil.Inventario.ViewModel {

    public class NotaDeEntradaSalidaMngViewModel : LibMngMasterViewModelMfc<NotaDeEntradaSalidaViewModel, NotaDeEntradaSalida> {
        #region Propiedades
        public override string ModuleName {
            get { return "Nota de Entrada/Salida"; }
        }

        public RelayCommand AnularCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public NotaDeEntradaSalidaMngViewModel() : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, NumeroDocumento";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override NotaDeEntradaSalidaViewModel CreateNewElement(NotaDeEntradaSalida valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new NotaDeEntradaSalida();
            }
            return new NotaDeEntradaSalidaViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_NotaDeEntradaSalida_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>> GetBusinessComponent() {
            return new clsNotaDeEntradaSalidaNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return  new clsNotaDeEntradaSalidaRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            AnularCommand = new RelayCommand(ExecuteAnularCommand, CanExecuteAnularCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.RemoveRibbonControl("Administrar", "Modificar");
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateRibbonGroup());
                #region Codigo Ejemplo
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

        ANYRELATEDViewModel CreateNewElementForSUPROCESOPARTICULAR(NotaDeEntradaSalida valModel, eAccionSR valAction) {
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

        private LibRibbonGroupData CreateRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Especial");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Anular Retiro",
                Command = AnularCommand,
                CommandParameter = eAccionSR.Anular,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/specializedUpdate.png", UriKind.Relative),
                ToolTipDescription = "Anula Notas de Salida por Retiro.",
                ToolTipTitle = "Anular Notas de Salida por Retiro"
            });
            return vResult;
        }

        private void ExecuteAnularCommand() {
            try {
                NotaDeEntradaSalidaViewModel vViewModel = CreateNewElement(CurrentItem.GetModel(), eAccionSR.Anular);
                vViewModel.InitializeViewModel(eAccionSR.Anular);
                bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
                if (result) {
                    SearchItems();
                }
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private bool CanExecuteAnularCommand() {
            return true; //CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Anular Retiro") && CurrentItem.TipodeOperacion == eTipodeOperacion.Retiro;
        }

    } //End of class NotaDeEntradaSalidaMngViewModel

} //End of namespace Galac.Saw.Uil.Inventario

