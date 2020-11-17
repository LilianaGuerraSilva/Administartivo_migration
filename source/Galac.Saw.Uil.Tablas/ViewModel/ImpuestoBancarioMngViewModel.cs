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
using Galac.Saw.Brl.Tablas;
using Galac.Saw.Brl.Tablas.Reportes;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Uil.Tablas.ViewModel {

    [LibMefInstallValuesMetadata(typeof(ImpuestoBancarioMngViewModel))]
    public class ImpuestoBancarioMngViewModel:LibMngViewModel<ImpuestoBancarioViewModel,ImpuestoBancario>, ILibMefInstallValues {
        #region Propiedades

        public override string ModuleName {
            get { return "Alícuota ITF"; }
        }

        public RelayCommand ReinstallCommand {
            get;
            private set;
        }


        public RelayCommand GetAlicuotaITFCommand {
            get;
            private set;
        }

        #endregion //Propiedades
        #region Constructores

        public ImpuestoBancarioMngViewModel() {
            Title = "Buscar " + ModuleName;
            OrderByMember = "FechaDeInicioDeVigencia";
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
                OrderByDirection = "DESC";
            */
            #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ImpuestoBancarioViewModel CreateNewElement(ImpuestoBancario valModel,eAccionSR valAction) {
            var vNewModel = valModel;
            if(vNewModel == null) {
                vNewModel = new ImpuestoBancario();
            }
            return new ImpuestoBancarioViewModel(vNewModel,valAction);
        }

        protected override ILibBusinessComponentWithSearch<IList<ImpuestoBancario>,IList<ImpuestoBancario>> GetBusinessComponent() {
            return new clsImpuestoBancarioNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return null;
        }

        protected override ILibRpt GetReportForList(string valModuleName,ILibReportInfo valReportInfo,LibCollFieldFormatForGrid valFieldsFormat,LibGpParams valParams) {
            return new LibGenericList(valModuleName,valReportInfo,valFieldsFormat,valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ReinstallCommand = new RelayCommand(ExecuteReinstallCommand,CanExecuteReinstallCommand);            
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(CreateReinstallRibbonButtonData());                
                RibbonData.RemoveRibbonControl("Administrar","Modificar");
                RibbonData.RemoveRibbonControl("Consultas","Imprimir Lista");
            }
        }

        private LibRibbonButtonData CreateReinstallRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Reinstalar",
                Command = ReinstallCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/importExport.png",UriKind.Relative),
                ToolTipDescription = "Reinstalar datos",
                ToolTipTitle = "Reinstalar"
            };
        }
       

        private void ExecuteReinstallCommand() {
            try {
                InstallOrReInstallDataFromFile(eAccionSR.ReInstalar);
                LibMessages.RefreshList.Send(ModuleName);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        bool ILibMefInstallValues.InstallFromFile() {
            string vFileName = System.IO.Path.Combine(LibWorkPaths.PathOfCommonTablesForCountry(""),"ImpuestoBancario.txt");
            bool vResult = LibImportExport.InstallData(vFileName,ModuleName,new clsImpuestoBancarioImpExp(),LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
            return vResult;
        }

        internal bool InstallOrReInstallDataFromFile(eAccionSR valAction) {
            bool vResult = false;
            string vFileName = System.IO.Path.Combine(LibWorkPaths.TablesDir,"ImpuestoBancario.txt");
            if(valAction == eAccionSR.Instalar) {
                vResult = LibImportExport.InstallData(vFileName,ModuleName,new clsImpuestoBancarioImpExp(),LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
            } else {
                vResult = LibImportExport.ReInstallData(vFileName,ModuleName,new clsImpuestoBancarioImpExp(),LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
            }
            return vResult;
        }

        protected override bool CanExecuteCreateCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Tablas","Insertar");
        }

        private bool CanExecuteReinstallCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Tablas","Insertar");
        }


        protected override bool CanExecuteDeleteCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Tablas","Eliminar") && CurrentItem != null;
        }

        protected override bool CanExecuteReadCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Tablas","Consultar") && CurrentItem != null;
        }       

        protected override bool HasAccessToModule() {
            bool vResult = LibSecurityManager.CurrentUserHasAccessTo("Tablas","Consultar") ||
               LibSecurityManager.CurrentUserHasAccessTo("Tablas","Reinstalar");
            return vResult;
        }      

        private void ExecuteGetAlicuotaITFCommand() {
            string vResult="";            
            vResult = new Galac.Saw.Brl.Tablas.clsImpuestoBancarioNav().BuscaAlicutoaImpTranscBancarias(LibDate.Today(),true);
        }

        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
        //para cambiar el mecanismo de activacion de los botones de operaciones CRUD, debes sobreescribirla y ajustarla las necesidades de tu negocio:

       
        //para agregar una nueva accion en el Ribbon, debes agregar este conjunto de métodos (6 en total) y modificar las inicializaciones.
        //Por favor recuerda autodocumentar, el codigo es de ejemplo para que te sirva de guía, no código final:

        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            SUPROCESOPARTICULARCommand.RaiseCanExecuteChanged();
        }

        ANYRELATEDViewModel CreateNewElementForSUPROCESOPARTICULAR(ImpuestoBancario valModel, eAccionSR valAction) {
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


    } //End of class ImpuestoBancarioMngViewModel

} //End of namespace Galac.Comun.Uil.TablasLey

