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

    public class LoteDeInventarioMngViewModel : LibMngMasterViewModelMfc<LoteDeInventarioViewModel, LoteDeInventario> {
        #region Propiedades

        public override string ModuleName {
            get { return "Lote de Inventario"; }
        }
        #endregion //Propiedades
        #region Constructores

        public LoteDeInventarioMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, FechaDeVencimiento, FechaDeElaboracion, Consecutivo";
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
                OrderByDirection = "DESC";
            */
            #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override LoteDeInventarioViewModel CreateNewElement(LoteDeInventario valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new LoteDeInventario();
            }
            return new LoteDeInventarioViewModel(vNewModel, valAction);
        }

        private LoteDeInventarioInsertarViewModel CreateNewElementInsertar(LoteDeInventario valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new LoteDeInventario();
            }
            return new LoteDeInventarioInsertarViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_LoteDeInventario_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<LoteDeInventario>, IList<LoteDeInventario>> GetBusinessComponent() {
            return new clsLoteDeInventarioNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return null; // new clsLoteDeInventarioRpt();
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
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0].IsVisible = false; // Botón Insertar Oculto
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[1].IsVisible = false; // Botón Modificar Oculto
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[2].IsVisible = false; // Botón Eliminar Oculto
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

        ANYRELATEDViewModel CreateNewElementForSUPROCESOPARTICULAR(LoteDeInventario valModel, eAccionSR valAction) {
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

        internal void ExecuteCreateCommandEspecial(string initCodigoLote, string initCodigoArticulo, eTipoArticuloInv initTipoArticuloInv) {
            try {
                LoteDeInventarioInsertarViewModel vViewModel = CreateNewElementInsertar(default(LoteDeInventario), eAccionSR.Insertar);
                vViewModel.InitializeViewModel(eAccionSR.Insertar);
                vViewModel.Consecutivo = (new LibGalac.Aos.Dal.LibDatabase()).NextLngConsecutive("Saw.LoteDeInventario", "Consecutivo", "ConsecutivoCompania = " + Mfc.GetInt("Compania").ToString());
                vViewModel.CodigoLote = initCodigoLote;
                vViewModel.CodigoArticulo = initCodigoArticulo;
                vViewModel.TipoArticuloInv = initTipoArticuloInv;                
                bool vResult = LibMessages.EditViewModel.ShowEditor(vViewModel,true);
            } catch (Exception) {
                throw;
            }
        }
    } //End of class LoteDeInventarioMngViewModel

} //End of namespace Galac.Saw.Uil.Inventario