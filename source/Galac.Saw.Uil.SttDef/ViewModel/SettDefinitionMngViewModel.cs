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
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.UI.Wpf;

namespace Galac.Saw.Uil.SttDef.ViewModel {

    public class SettDefinitionMngViewModel : LibMngViewModel<SettDefinitionViewModel, SettDefinition> {
        #region Propiedades

        public override string ModuleName {
            get { return "Sett Definition"; }
        }
        #endregion //Propiedades
        #region Constructores

        public SettDefinitionMngViewModel() {
            Title = "Buscar " + ModuleName;
            OrderByMember = "Name";
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            OrderByDirection = "DESC";
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override SettDefinitionViewModel CreateNewElement(SettDefinition valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new SettDefinition();
            }
            return new SettDefinitionViewModel(vNewModel, valAction);
        }

        protected override ILibBusinessComponentWithSearch<IList<SettDefinition>, IList<SettDefinition>> GetBusinessComponent() {
            return null;
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return null;
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return null;
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

        internal bool InstallOrReInstallDataFromFile(eAccionSR valAction) {
           bool vResult = false;
           string vFileName = System.IO.Path.Combine(LibWorkPaths.PathOfCommonTablesForCountry(""), "SettDefinition.txt");
           if(valAction == eAccionSR.Instalar) {
              vResult = LibImportExport.InstallData(vFileName, "SettDefinition", new clsSettDefinitionImpExp(), LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
           } else {
              vResult = LibImportExport.ReInstallData(vFileName, "SettDefinition", new clsSettDefinitionImpExp(), LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
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
        //para agregar una nueva accion en el Ribbon, debes agregar este conjunto de m�todos (6 en total) y modificar las inicializaciones.
        //Por favor recuerda autodocumentar, el codigo es de ejemplo para que te sirva de gu�a, no c�digo final:

        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            SUPROCESOPARTICULARCommand.RaiseCanExecuteChanged();
        }

        ANYRELATEDViewModel CreateNewElementForSUPROCESOPARTICULAR(SettDefinition valModel, eAccionSR valAction) {
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


    } //End of class SettDefinitionMngViewModel

} //End of namespace Galac.Saw.Uil.SttDef

