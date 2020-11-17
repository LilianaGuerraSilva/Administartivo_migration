using System;
using System.Collections.Generic;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt.Reports;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Adm.Brl.DispositivosExternos.BalanzaElectronica;

namespace Galac.Adm.Uil.DispositivosExternos.ViewModel {

    [LibMefInstallValuesMetadata(typeof(BalanzaMngViewModel))]
    public class BalanzaMngViewModel : LibMngViewModelMfc<BalanzaViewModel, Balanza> {
        #region Propiedades
        
        public override string ModuleName {
            get { return "Balanza"; }
        }

        public RelayCommand EscogerBalanzaPOSCommand {
            get;
            private set;
        }


        #endregion //Propiedades
        #region Constructores

        public BalanzaMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Gestionar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Consecutivo";
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            OrderByDirection = "DESC";
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override BalanzaViewModel CreateNewElement(Balanza valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new Balanza();                
            }           
            return new BalanzaViewModel(vNewModel, valAction);
        }
        
        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_Balanza_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessComponentWithSearch<IList<Balanza>, IList<Balanza>> GetBusinessComponent() {
            return new clsBalanzaNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return null;//new clsBalanzaRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            EscogerBalanzaPOSCommand = new RelayCommand(ExecuteEscogerBalanzaPOSCommand,CanExecuteEscogerBalanzaPOSCommand);

       
        #endregion //Codigo Ejemplo
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection.Add(CreateAsignarBalanzaPOSRibbonGroup());
                RibbonData.RemoveRibbonControl("Consultas","Imprimir Lista");        
            }
        }      
        #region Codigo Ejemplo        

        private LibRibbonGroupData CreateAsignarBalanzaPOSRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Seleccionar Balanza");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Seleccionar Balanza en POS",
                Command = EscogerBalanzaPOSCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/settings.png",UriKind.Relative),
                ToolTipDescription = "Asignar Balanza a POS",
                ToolTipTitle = "Seleccionar Balanza en POS"
            });
            return vResult;
        }

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

        private bool CanExecuteEscogerBalanzaPOSCommand() {
            return true;
        }

        private void ExecuteEscogerBalanzaPOSCommand() {
            try {               
                BalanzaViewModel BalanzaViewModel = CreateNewElement(CurrentItem.GetModel(),eAccionSR.Cerrar);
                BalanzaViewModel.InitializeViewModel(eAccionSR.Escoger);
                LibMessages.EditViewModel.ShowEditor(BalanzaViewModel);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                if(LibString.S1IsInS2("Referencia a objeto no establecida como instancia de un objeto",vEx.Message)) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Alert(null,"No existe una balanza registrada","");
                } else {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
                }                
            }
        }

        /* Codigo de Ejemplo
        //para cambiar el mecanismo de activacion de los botones de operaciones CRUD, debes sobreescribirla y ajustarla las necesidades de tu negocio:        
        //para agregar una nueva accion en el Ribbon, debes agregar este conjunto de métodos (6 en total) y modificar las inicializaciones.
        //Por favor recuerda autodocumentar, el codigo es de ejemplo para que te sirva de guía, no código final:

        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
            base.ExecuteCommandsRaiseCanExecuteChanged();
            SUPROCESOPARTICULARCommand.RaiseCanExecuteChanged();
        }

        ANYRELATEDViewModel CreateNewElementForSUPROCESOPARTICULAR(Balanza valModel, eAccionSR valAction) {
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


    } //End of class BalanzaMngViewModel

} //End of namespace Galac.Adm.Uil.DispositivosExternos

