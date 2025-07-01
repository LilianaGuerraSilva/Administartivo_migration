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
using Galac.Saw.Brl.Cliente;
using Galac.Saw.Brl.Cliente.Reportes;
using Galac.Saw.Ccl.Cliente;
using Entity = Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Uil.Cliente.ViewModel {

    public class ClienteMngViewModel : LibMngMasterViewModelMfc<ClienteViewModel, Entity.Cliente> {
        #region Propiedades

        public override string ModuleName {
            get { return "Cliente"; }
        }

        public RelayCommand InsertarClienteResumenDiarioCommand {
            get;
            private set;
        }

        private const string ModuleNameOriginal = "Cliente";

        #endregion //Propiedades
        #region Constructores

        public ClienteMngViewModel()
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

        protected override ClienteViewModel CreateNewElement(Entity.Cliente valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new Entity.Cliente();
            }
            return new ClienteViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_Cliente_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<Entity.Cliente>, IList<Entity.Cliente>> GetBusinessComponent() {
            return new clsClienteNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsClienteRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            InsertarClienteResumenDiarioCommand = new RelayCommand(ExecuteInsertarClienteResumenDiarioCommand, CanExecuteInsertarClienteResumenDiarioCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateOtrasAccionesRibbonGroup());
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

        ANYRELATEDViewModel CreateNewElementForSUPROCESOPARTICULAR(Cliente valModel, eAccionSR valAction) {
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

        private void CreaTabDeClienteResumen() {
            LibRibbonGroupData vRibbonGroupData = new LibRibbonGroupData("Cliente");
            vRibbonGroupData.ControlDataCollection.Add(CreateRibbonButtonData("Insertar Cliente de Resumen Diario", InsertarClienteResumenDiarioCommand, new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/edit.png", UriKind.Relative), "Cliente Resumen", "Cliente Resumen"));
        }


        private bool CanExecuteInsertarClienteResumenDiarioCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Insertar");
        }

        private void ExecuteInsertarClienteResumenDiarioCommand() {
            try {
                /* Metodo valida cliente existe sino crea
                 * brl 2 metodos 
                 *   buscar cliente resumen
                 *   insertar cliente resumen
                 *  ccl interfaz de los metodos
                 *       buscarClienteResumen
                 */
                //ClienteViewModel clienteVM = new ClienteViewModel();
                //clienteVM.ExecuteInsertarClienteResumenDiario();

                IClientePdn vCliente = new clsClienteNav();
                //if (vCliente.BuscarClienteResumenDiario(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"))) {
                if (vCliente.BuscarClienteResumenDiario()) {

                    LibMessages.MessageBox.Information(this, $"El Cliente de resumen diario ya existe .", ModuleName);
                } else {
                    vCliente.InsertarClienteResumenDiario();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private LibRibbonGroupData CreateOtrasAccionesRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Otras Acciones");
           
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Insertar Cliente de Resumen Diario",
                Command = InsertarClienteResumenDiarioCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/specializedUpdate.png", UriKind.Relative),
                ToolTipDescription = "Insertar Cliente de Resumen Diario",
                ToolTipTitle = "Cliente",
            });
            return vResult;
        }

    } //End of class ClienteMngViewModel

} //End of namespace Galac.Saw.Uil.Cliente

