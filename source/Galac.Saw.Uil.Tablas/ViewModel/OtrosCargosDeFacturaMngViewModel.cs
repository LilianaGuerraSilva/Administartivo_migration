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
using Galac.Saw.Ccl.Tablas.Reportes;

namespace Galac.Saw.Uil.Tablas.ViewModel {

    public class OtrosCargosDeFacturaMngViewModel : LibMngViewModelMfc<OtrosCargosDeFacturaViewModel, OtrosCargosDeFactura> {
        #region Propiedades

        public override string ModuleName {
            get { return "Otros Cargos de Factura"; }
        }
        #endregion //Propiedades
        #region Constructores

        public OtrosCargosDeFacturaMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Codigo";
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            OrderByDirection = "DESC";
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override OtrosCargosDeFacturaViewModel CreateNewElement(OtrosCargosDeFactura valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new OtrosCargosDeFactura();
            }
            return new OtrosCargosDeFacturaViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_OtrosCargosDeFactura_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessComponentWithSearch<IList<OtrosCargosDeFactura>, IList<OtrosCargosDeFactura>> GetBusinessComponent() {
            return new clsOtrosCargosDeFacturaNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsOtrosCargosDeFacturaRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            SUPROCESOPARTICULARCommand = new RelayCommand(ExecuteSUPROCESOPARTICULARCommand, CanExecuteSUPROCESOPARTICULARCommand);
        */
        #endregion //Codigo Ejemplo
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateSUPROCESOPARTICULARRibbonGroup());
        */
        #endregion //Codigo Ejemplo
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
        //para agregar una nueva accion en el Ribbon, debes agregar este conjunto de m�todos (6 en total) y modificar las inicializaciones.
        //Por favor recuerda autodocumentar, el codigo es de ejemplo para que te sirva de gu�a, no c�digo final:

        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            SUPROCESOPARTICULARCommand.RaiseCanExecuteChanged();
        }

        ANYRELATEDViewModel CreateNewElementForSUPROCESOPARTICULAR(OtrosCargosDeFactura valModel, eAccionSR valAction) {
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


    } //End of class OtrosCargosDeFacturaMngViewModel

} //End of namespace Galac.Saw.Uil.Tablas

